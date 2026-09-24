using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

// ADMHOME: list of finalised शिकायतकर्ता (वादी) applications (dbo.ADMHOME_VadiApplication) with details and PDF.
public partial class LandDispute_Entry_ADMHOME_ViewForward : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();

    // names are looked up with OUTER APPLY ... TOP 1 so duplicate master rows can never duplicate an application
    const string ApplicationSelect = @"
        SELECT a.ApplicationId, a.ApplicationNo, a.vadi_Name, a.Vadi_Father_Husband_Name,
               CASE a.SexAsPerAadhaar WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' ELSE 'Other' END AS Gender,
               d.DISTRICTNAME AS DistrictName, s.Sd_Name_En AS SubDivisionName, b.BlockName, t.Police_Station AS ThanaName,
               CASE a.Vadi_AreaType WHEN 'R' THEN 'Rural' WHEN 'U' THEN 'Urban' ELSE '' END AS AreaType,
               p.PanchayatName, v.VILLNAME AS VillageName, w.WARDNAME AS WardName, a.mohalla,
               a.Vadi_MobileNo, a.PinCode, a.Remarks, a.Status, a.CreatedBy, a.CreatedOn
        FROM dbo.ADMHOME_VadiApplication a
        OUTER APPLY (SELECT TOP 1 DISTRICTNAME FROM dbo.mst_Commissionary_Districts WHERE DISTRICTCODE = a.Vadi_District_Code) d
        OUTER APPLY (SELECT TOP 1 Sd_Name_En FROM dbo.SubDivisions WHERE Sd_Code2 = a.Vadi_Sub_DivCode) s
        OUTER APPLY (SELECT TOP 1 BlockName FROM dbo.Blocks WHERE BlockCode = a.Vadi_Block_Code) b
        OUTER APPLY (SELECT TOP 1 Police_Station FROM dbo.mst_thana WHERE PS_Code = a.Vadi_Thana_code) t
        OUTER APPLY (SELECT TOP 1 PanchayatName FROM dbo.mst_Panchayats WHERE PanchayatCode = a.Vadi_Panchayat_Code) p
        OUTER APPLY (SELECT TOP 1 VILLNAME FROM dbo.mst_VillageMaster WHERE VILLCODE = a.Vadi_Village_Code) v
        OUTER APPLY (SELECT TOP 1 WARDNAME FROM dbo.mst_Wards WHERE WARDCODE = a.Vadi_WardNo) w ";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["UserID"]) == "")
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
            return;
        }
        if (Convert.ToString(Session["Role"]).Trim() != "ADMHOME")
        {
            Response.Redirect("~/Default.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindFilterDistricts();
            BindFilterBlocks();
            BindApplications();
        }
    }

    #region Filters

    void BindFilterDistricts()
    {
        DataTable dt = clsData.GetDataTable("SELECT DISTINCT DISTRICTNAME, DISTRICTCODE FROM dbo.mst_Commissionary_Districts ORDER BY DISTRICTNAME");
        ddlFilterDistrict.DataSource = dt;
        ddlFilterDistrict.DataTextField = "DISTRICTNAME";
        ddlFilterDistrict.DataValueField = "DISTRICTCODE";
        ddlFilterDistrict.DataBind();
        ddlFilterDistrict.Items.Insert(0, new ListItem("--सभी जिले--", "0"));
    }

    // अंचल of the selected जिला; disabled until a जिला is chosen
    void BindFilterBlocks()
    {
        ddlFilterBlock.Items.Clear();
        if (ddlFilterDistrict.SelectedValue != "0")
        {
            DataTable dt = clsData.GetDataTable("SELECT DISTINCT BlockName, BlockCode FROM dbo.Blocks WHERE DistCode = @DistCode ORDER BY BlockName",
                new SqlParameter[] { new SqlParameter("@DistCode", Convert.ToInt32(ddlFilterDistrict.SelectedValue)) });
            ddlFilterBlock.DataSource = dt;
            ddlFilterBlock.DataTextField = "BlockName";
            ddlFilterBlock.DataValueField = "BlockCode";
            ddlFilterBlock.DataBind();
        }
        ddlFilterBlock.Items.Insert(0, new ListItem("--सभी अंचल--", "0"));
        ddlFilterBlock.Enabled = ddlFilterDistrict.SelectedValue != "0";
    }

    protected void ddlFilterDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFilterBlocks();
        ApplyFilters();
    }

    protected void ddlFilterBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        ApplyFilters();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ApplyFilters();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlFilterDistrict.SelectedIndex = 0;
        BindFilterBlocks();
        txtFilterName.Text = "";
        txtSearch.Text = "";
        ApplyFilters();
    }

    void ApplyFilters()
    {
        gvApplications.PageIndex = 0;
        pnlDetails.Visible = false;
        BindApplications();
    }

    #endregion

    void BindApplications()
    {
        // LIKE wildcards typed by the user are matched literally
        string name = EscapeLike(txtFilterName.Text.Trim());
        string search = EscapeLike(txtSearch.Text.Trim());
        long districtCode = Convert.ToInt64(ddlFilterDistrict.SelectedValue);
        long blockCode = Convert.ToInt64(ddlFilterBlock.SelectedValue);

        string sql = ApplicationSelect + @"
        WHERE (@DistrictCode = 0 OR a.Vadi_District_Code = @DistrictCode)
          AND (@BlockCode = 0 OR a.Vadi_Block_Code = @BlockCode)
          AND (@Name = '' OR a.vadi_Name LIKE '%' + @Name + '%' ESCAPE '\')
          AND (@Search = '' OR a.ApplicationNo LIKE '%' + @Search + '%' ESCAPE '\'
                            OR a.Vadi_MobileNo LIKE '%' + @Search + '%' ESCAPE '\')
        ORDER BY a.ApplicationId DESC";

        DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] {
            new SqlParameter("@DistrictCode", districtCode),
            new SqlParameter("@BlockCode", blockCode),
            new SqlParameter("@Name", name),
            new SqlParameter("@Search", search) });
        gvApplications.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        gvApplications.DataSource = dt;
        gvApplications.DataBind();
        lblCount.Text = "कुल आवेदन: " + dt.Rows.Count;
    }

    static string EscapeLike(string value)
    {
        return value.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[");
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

    void ShowDetails(string applicationNo)
    {
        DataTable dt = clsData.GetDataTable(ApplicationSelect + " WHERE a.ApplicationNo = @ApplicationNo",
            new SqlParameter[] { new SqlParameter("@ApplicationNo", applicationNo) });
        if (dt.Rows.Count == 0)
        {
            pnlDetails.Visible = false;
            return;
        }

        DataRow r = dt.Rows[0];
        // Labels render raw HTML, so every value is encoded
        lblDApplicationNo.Text = Enc(r["ApplicationNo"]);
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
        lblDCreatedOn.Text = Convert.ToDateTime(r["CreatedOn"]).ToString("dd/MM/yyyy hh:mm tt");
        lblDCreatedBy.Text = Enc(r["CreatedBy"]);
        lblDRemarks.Text = Enc(r["Remarks"]);
        lnkDDocument.NavigateUrl = "ADMHOME_ViewDocument.aspx?app=" + Server.UrlEncode(applicationNo);
        pnlDetails.Visible = true;
    }

    string Enc(object value)
    {
        string s = Convert.ToString(value);
        return s == "" ? "-" : Server.HtmlEncode(s);
    }

    protected void btnCloseDetails_Click(object sender, EventArgs e)
    {
        pnlDetails.Visible = false;
    }
}
