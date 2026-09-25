using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

// DM (DMOPT) / SP (SSPOPT): applications the Home Department (ADMHOME) forwarded to this login's
// role in this login's district (dbo.ADMHOME_VadiApplicationForward). Read-only.
public partial class LandDispute_Entry_ForwardedApplications : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();

    const string ForwardedSelect = @"
        SELECT a.FileNo, a.vadi_Name, a.Vadi_Father_Husband_Name,
               CASE a.SexAsPerAadhaar WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' ELSE 'Other' END AS Gender,
               d.DISTRICTNAME AS DistrictName, s.Sd_Name_En AS SubDivisionName, b.BlockName, t.Police_Station AS ThanaName,
               CASE a.Vadi_AreaType WHEN 'R' THEN 'Rural' WHEN 'U' THEN 'Urban' ELSE '' END AS AreaType,
               p.PanchayatName, v.VILLNAME AS VillageName, w.WARDNAME AS WardName, a.mohalla,
               a.Vadi_MobileNo, a.PinCode, a.Remarks,
               f.ForwardedOn, f.ForwardRemarks, f.ForwardedBy,
               -- sending department (N'' keeps the Hindi text)
               CASE a.CreatedRole WHEN 'ADMLR' THEN N'राजस्व एवं भूमि सुधार विभाग' ELSE N'गृह विभाग' END AS DeptName
        FROM dbo.ADMHOME_VadiApplicationForward f
        INNER JOIN dbo.ADMHOME_VadiApplication a ON a.ApplicationId = f.ApplicationId
        OUTER APPLY (SELECT TOP 1 DISTRICTNAME FROM dbo.mst_Commissionary_Districts WHERE DISTRICTCODE = a.Vadi_District_Code) d
        OUTER APPLY (SELECT TOP 1 Sd_Name_En FROM dbo.SubDivisions WHERE Sd_Code2 = a.Vadi_Sub_DivCode) s
        OUTER APPLY (SELECT TOP 1 BlockName FROM dbo.Blocks WHERE BlockCode = a.Vadi_Block_Code) b
        OUTER APPLY (SELECT TOP 1 Police_Station FROM dbo.mst_thana WHERE PS_Code = a.Vadi_Thana_code) t
        OUTER APPLY (SELECT TOP 1 PanchayatName FROM dbo.mst_Panchayats WHERE PanchayatCode = a.Vadi_Panchayat_Code) p
        OUTER APPLY (SELECT TOP 1 VILLNAME FROM dbo.mst_VillageMaster WHERE VILLCODE = a.Vadi_Village_Code) v
        OUTER APPLY (SELECT TOP 1 WARDNAME FROM dbo.mst_Wards WHERE WARDCODE = a.Vadi_WardNo) w
        WHERE f.ForwardedToRole = @Role AND f.DistrictCode = @DistrictCode ";

    string Role { get { return Convert.ToString(Session["Role"]).Trim(); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["UserID"]) == "")
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
            return;
        }
        long districtCode;
        if ((Role != "DMOPT" && Role != "SSPOPT") || !long.TryParse(Convert.ToString(Session["District_Code"]), out districtCode))
        {
            Response.Redirect("~/Default.aspx");
            return;
        }

        if (!IsPostBack)
            BindApplications();
    }

    SqlParameter[] RecipientParams(params SqlParameter[] extra)
    {
        SqlParameter[] p = new SqlParameter[2 + extra.Length];
        p[0] = new SqlParameter("@Role", Role);
        p[1] = new SqlParameter("@DistrictCode", Convert.ToInt64(Session["District_Code"]));
        extra.CopyTo(p, 2);
        return p;
    }

    void BindApplications()
    {
        string search = EscapeLike(txtSearch.Text.Trim());
        DataTable dt = clsData.GetDataTable(ForwardedSelect + @"
            AND (@Search = '' OR a.FileNo LIKE '%' + @Search + '%' ESCAPE '\'
                              OR a.vadi_Name LIKE '%' + @Search + '%' ESCAPE '\'
                              OR a.Vadi_MobileNo LIKE '%' + @Search + '%' ESCAPE '\')
            ORDER BY f.ForwardedOn DESC",
            RecipientParams(new SqlParameter("@Search", search)));

        gvApplications.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        gvApplications.DataSource = dt;
        gvApplications.DataBind();
        lblCount.Text = "कुल अग्रेषित आवेदन: " + dt.Rows.Count;
    }

    static string EscapeLike(string value)
    {
        return value.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gvApplications.PageIndex = 0;
        BindApplications();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtSearch.Text = "";
        btnSearch_Click(sender, e);
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvApplications.PageIndex = 0;
        BindApplications();
    }

    protected void gvApplications_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvApplications.PageIndex = e.NewPageIndex;
        BindApplications();
    }

    protected void gvApplications_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewApp")
            ShowDetails(Convert.ToString(e.CommandArgument));
    }

    void ShowDetails(string fileNo)
    {
        // same recipient filter, so a DM / SP can only open what was forwarded to them
        DataTable dt = clsData.GetDataTable(ForwardedSelect + " AND a.FileNo = @FileNo",
            RecipientParams(new SqlParameter("@FileNo", fileNo)));
        if (dt.Rows.Count == 0)
            return;

        DataRow r = dt.Rows[0];
        lblDFileNo.Text = Enc(r["FileNo"]);
        lblDName.Text = Enc(r["vadi_Name"]);
        lblDFather.Text = Enc(r["Vadi_Father_Husband_Name"]);
        lblDGender.Text = Enc(r["Gender"]);
        lblDMobile.Text = Enc(r["Vadi_MobileNo"]);
        lblDDistrict.Text = Enc(r["DistrictName"]);
        lblDSubdivision.Text = Enc(r["SubDivisionName"]);
        lblDBlock.Text = Enc(r["BlockName"]);
        lblDThana.Text = Enc(r["ThanaName"]);
        lblDAreaType.Text = Enc(r["AreaType"]);
        lblDPanchayat.Text = Enc(r["PanchayatName"]);
        lblDVillage.Text = Enc(r["VillageName"]);
        string ward = Convert.ToString(r["WardName"]);
        string mohalla = Convert.ToString(r["mohalla"]);
        lblDWard.Text = Enc(ward != "" && mohalla != "" ? ward + " / " + mohalla : ward + mohalla);
        lblDPincode.Text = Enc(r["PinCode"]);
        lblDForwardedOn.Text = Convert.ToDateTime(r["ForwardedOn"]).ToString("dd/MM/yyyy hh:mm tt");
        lblDForwardedBy.Text = Enc(Convert.ToString(r["DeptName"]) + " (" + Convert.ToString(r["ForwardedBy"]) + ")");
        lblDForwardRemarks.Text = Enc(r["ForwardRemarks"]);
        lblDRemarks.Text = Enc(r["Remarks"]);
        lnkDDocument.NavigateUrl = "ADMHOME_ViewDocument.aspx?file=" + Server.UrlEncode(fileNo);
        ClientScript.RegisterStartupScript(GetType(), "showModal", "showADMHOMEModal('modalAppDetails');", true);
    }

    string Enc(object value)
    {
        string s = Convert.ToString(value);
        return s == "" ? "-" : Server.HtmlEncode(s);
    }
}
