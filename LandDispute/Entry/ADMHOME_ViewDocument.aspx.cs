using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

// Opens the uploaded PDF of an ADMHOME application in the browser: ADMHOME_ViewDocument.aspx?app=HDSB10001
public partial class LandDispute_Entry_ADMHOME_ViewDocument : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();

    protected void Page_Load(object sender, EventArgs e)
    {
        // ADMHOME: any application. DM (DMOPT) / SP (SSPOPT): only applications forwarded to their role in their district.
        string role = Convert.ToString(Session["Role"]).Trim();
        long districtCode = 0;
        bool isRecipient = (role == "DMOPT" || role == "SSPOPT") && long.TryParse(Convert.ToString(Session["District_Code"]), out districtCode);
        if (Convert.ToString(Session["UserID"]) == "" || (role != "ADMHOME" && !isRecipient))
        {
            Response.StatusCode = 403;
            Response.Write("Access denied.");
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        string applicationNo = Convert.ToString(Request.QueryString["app"]).Trim();
        if (!Regex.IsMatch(applicationNo, @"^HDSB[0-9]{5}$"))
        {
            NotFound();
            return;
        }

        DataTable dt = clsData.GetDataTable(@"
            SELECT d.FileName, d.ContentType, d.FileData
            FROM dbo.ADMHOME_VadiApplication a
            INNER JOIN dbo.ADMHOME_VadiApplicationDoc d ON d.ApplicationId = a.ApplicationId
            WHERE a.ApplicationNo = @ApplicationNo
              AND (@Role = 'ADMHOME'
                   OR EXISTS (SELECT 1 FROM dbo.ADMHOME_VadiApplicationForward f
                              WHERE f.ApplicationId = a.ApplicationId AND f.ForwardedToRole = @Role AND f.DistrictCode = @DistrictCode))",
            new SqlParameter[] {
                new SqlParameter("@ApplicationNo", applicationNo),
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
        if (fileName == "") fileName = applicationNo + ".pdf";

        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "inline; filename=\"" + applicationNo + "_" + fileName + "\"");
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
