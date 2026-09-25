using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

// Opens the uploaded PDF of an ADMHOME / ADMLR file in the browser: ADMHOME_ViewDocument.aspx?file=HDSB10001
public partial class LandDispute_Entry_ADMHOME_ViewDocument : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();

    protected void Page_Load(object sender, EventArgs e)
    {
        // ADMHOME / ADMLR: their own department's files. DM (DMOPT) / SP (SSPOPT): only files forwarded to their role in their district.
        string role = Convert.ToString(Session["Role"]).Trim();
        long districtCode = 0;
        bool isRecipient = (role == "DMOPT" || role == "SSPOPT") && long.TryParse(Convert.ToString(Session["District_Code"]), out districtCode);
        bool isDept = role == "ADMHOME" || role == "ADMLR";   // own department's files only (CreatedRole)
        if (Convert.ToString(Session["UserID"]) == "" || (!isDept && !isRecipient))
        {
            Response.StatusCode = 403;
            Response.Write("Access denied.");
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        // ?temp=<DocKey>: a PDF in the New Entry temporary list, not finalised yet. It only exists
        // in this user's own Session (Entry_Page GetVadiADMHOMEDocs), so nobody else can open it.
        string tempKey = Convert.ToString(Request.QueryString["temp"]).Trim();
        if (tempKey != "")
        {
            System.Collections.Generic.Dictionary<string, byte[]> docs =
                Session["vadiDocsADMHOME"] as System.Collections.Generic.Dictionary<string, byte[]>;
            byte[] tempData;
            if (!isDept || !Regex.IsMatch(tempKey, @"^[0-9a-f]{32}$") || docs == null || !docs.TryGetValue(tempKey, out tempData))
            {
                NotFound();
                return;
            }
            WritePdf(tempData, "document.pdf");
            return;
        }

        string fileNo = Convert.ToString(Request.QueryString["file"]).Trim();
        if (!Regex.IsMatch(fileNo, @"^HDSB[0-9]{5}$"))
        {
            NotFound();
            return;
        }

        DataTable dt = clsData.GetDataTable(@"
            SELECT d.FileName, d.ContentType, d.FileData
            FROM dbo.ADMHOME_VadiApplication a
            INNER JOIN dbo.ADMHOME_VadiApplicationDoc d ON d.ApplicationId = a.ApplicationId
            WHERE a.FileNo = @FileNo
              AND (a.CreatedRole = @Role
                   OR EXISTS (SELECT 1 FROM dbo.ADMHOME_VadiApplicationForward f
                              WHERE f.ApplicationId = a.ApplicationId AND f.ForwardedToRole = @Role AND f.DistrictCode = @DistrictCode))",
            new SqlParameter[] {
                new SqlParameter("@FileNo", fileNo),
                new SqlParameter("@Role", role),
                new SqlParameter("@DistrictCode", districtCode) });

        if (dt.Rows.Count == 0 || dt.Rows[0]["FileData"] == DBNull.Value)
        {
            NotFound();
            return;
        }

        byte[] data = (byte[])dt.Rows[0]["FileData"];
        // keep the header safe: only plain characters in the suggested file name
        string fileName = Regex.Replace(Path.GetFileName(Convert.ToString(dt.Rows[0]["FileName"])), @"[^A-Za-z0-9._\- ]", "_");
        if (fileName == "") fileName = fileNo + ".pdf";
        WritePdf(data, fileNo + "_" + fileName);
    }

    void WritePdf(byte[] data, string fileName)
    {
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "inline; filename=\"" + fileName + "\"");
        Response.AddHeader("X-Content-Type-Options", "nosniff");
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.BinaryWrite(data);
        Response.Flush();
        Context.ApplicationInstance.CompleteRequest();
    }

    void NotFound()
    {
        Response.StatusCode = 404;
        Response.Write("Document not found.");
        Context.ApplicationInstance.CompleteRequest();
    }
}
