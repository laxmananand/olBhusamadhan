using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

// ADMHOME: list of finalised शिकायतकर्ता (वादी) applications (dbo.ADMHOME_VadiApplication) with details and PDF.
public partial class LandDispute_Entry_ADMHOME_ViewForward : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();

    // names are looked up with OUTER APPLY ... TOP 1 so duplicate master rows can never duplicate an application
    const string ApplicationSelect = @"
        SELECT a.ApplicationId, a.FileNo, a.vadi_Name, a.Vadi_Father_Husband_Name,
               CASE a.SexAsPerAadhaar WHEN 'M' THEN 'Male' WHEN 'F' THEN 'Female' ELSE 'Other' END AS Gender,
               d.DISTRICTNAME AS DistrictName, s.Sd_Name_En AS SubDivisionName, b.BlockName, t.Police_Station AS ThanaName,
               CASE a.Vadi_AreaType WHEN 'R' THEN 'Rural' WHEN 'U' THEN 'Urban' ELSE '' END AS AreaType,
               p.PanchayatName, v.VILLNAME AS VillageName, w.WARDNAME AS WardName, a.mohalla,
               a.Vadi_MobileNo, a.PinCode, a.Remarks, a.Status, a.CreatedBy, a.CreatedOn, a.Vadi_District_Code,
               -- e.g. ""BHAGALPUR - DM, BHAGALPUR - SP"" (FOR XML PATH instead of STRING_AGG: works before SQL 2017)
               STUFF((SELECT ', ' + ISNULL(fd.DISTRICTNAME, CAST(f.DistrictCode AS varchar(20))) + ' - '
                             + CASE f.ForwardedToRole WHEN 'DMOPT' THEN 'DM' ELSE 'SP' END
                      FROM dbo.ADMHOME_VadiApplicationForward f
                      OUTER APPLY (SELECT TOP 1 DISTRICTNAME FROM dbo.mst_Commissionary_Districts WHERE DISTRICTCODE = f.DistrictCode) fd
                      WHERE f.ApplicationId = a.ApplicationId
                      ORDER BY f.ForwardId
                      FOR XML PATH(''), TYPE).value('.', 'nvarchar(max)'), 1, 2, '') AS ForwardedTo
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
        // ADMHOME (Home Dept.) and ADMLR (Land & Revenue Dept.); each sees only its own files
        if (DeptRole != "ADMHOME" && DeptRole != "ADMLR")
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
        BindApplications();
    }

    #endregion

    // the logged-in department; every query on this page is limited to a.CreatedRole = @CreatedRole
    string DeptRole { get { return Convert.ToString(Session["Role"]).Trim(); } }

    void BindApplications()
    {
        // LIKE wildcards typed by the user are matched literally
        string name = EscapeLike(txtFilterName.Text.Trim());
        string search = EscapeLike(txtSearch.Text.Trim());
        long districtCode = Convert.ToInt64(ddlFilterDistrict.SelectedValue);
        long blockCode = Convert.ToInt64(ddlFilterBlock.SelectedValue);

        string sql = ApplicationSelect + @"
        WHERE a.CreatedRole = @CreatedRole
          AND (@DistrictCode = 0 OR a.Vadi_District_Code = @DistrictCode)
          AND (@BlockCode = 0 OR a.Vadi_Block_Code = @BlockCode)
          AND (@Name = '' OR a.vadi_Name LIKE '%' + @Name + '%' ESCAPE '\')
          AND (@Search = '' OR a.FileNo LIKE '%' + @Search + '%' ESCAPE '\'
                            OR a.Vadi_MobileNo LIKE '%' + @Search + '%' ESCAPE '\')
        ORDER BY a.ApplicationId DESC";

        DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] {
            new SqlParameter("@DistrictCode", districtCode),
            new SqlParameter("@BlockCode", blockCode),
            new SqlParameter("@Name", name),
            new SqlParameter("@Search", search),
            new SqlParameter("@CreatedRole", DeptRole) });
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
        else if (e.CommandName == "ForwardApp")
            ShowForwardForm(Convert.ToString(e.CommandArgument));
    }

    void ShowDetails(string fileNo)
    {
        DataTable dt = clsData.GetDataTable(ApplicationSelect + " WHERE a.FileNo = @FileNo AND a.CreatedRole = @CreatedRole",
            new SqlParameter[] { new SqlParameter("@FileNo", fileNo), new SqlParameter("@CreatedRole", DeptRole) });
        if (dt.Rows.Count == 0)
            return;

        DataRow r = dt.Rows[0];
        // Labels render raw HTML, so every value is encoded
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
        lblDCreatedOn.Text = Convert.ToDateTime(r["CreatedOn"]).ToString("dd/MM/yyyy hh:mm tt");
        lblDCreatedBy.Text = Enc(r["CreatedBy"]);
        lblDRemarks.Text = Enc(r["Remarks"]);
        lnkDDocument.NavigateUrl = "ADMHOME_ViewDocument.aspx?file=" + Server.UrlEncode(fileNo);

        gvForwardHistory.DataSource = clsData.GetDataTable(@"
            SELECT fd.DISTRICTNAME AS DistrictName,
                   -- N'' = Unicode literal; without N the Hindi text becomes '?'
                   CASE f.ForwardedToRole WHEN 'DMOPT' THEN N'DM (जिलाधिकारी)' ELSE N'SP (पुलिस अधीक्षक)' END AS RoleName,
                   f.ForwardedToUserID, f.ForwardRemarks, f.ForwardedBy, f.ForwardedOn
            FROM dbo.ADMHOME_VadiApplicationForward f
            INNER JOIN dbo.ADMHOME_VadiApplication a ON a.ApplicationId = f.ApplicationId
            OUTER APPLY (SELECT TOP 1 DISTRICTNAME FROM dbo.mst_Commissionary_Districts WHERE DISTRICTCODE = f.DistrictCode) fd
            WHERE a.FileNo = @FileNo AND a.CreatedRole = @CreatedRole
            ORDER BY f.ForwardId",
            new SqlParameter[] { new SqlParameter("@FileNo", fileNo), new SqlParameter("@CreatedRole", DeptRole) });
        gvForwardHistory.DataBind();

        ShowModal("modalAppDetails");
    }

    #region Forward

    void ShowForwardForm(string fileNo)
    {
        DataTable dt = clsData.GetDataTable("SELECT Vadi_District_Code FROM dbo.ADMHOME_VadiApplication WHERE FileNo = @FileNo AND CreatedRole = @CreatedRole",
            new SqlParameter[] { new SqlParameter("@FileNo", fileNo), new SqlParameter("@CreatedRole", DeptRole) });
        if (dt.Rows.Count == 0)
            return;

        hfFwdFileNo.Value = fileNo;
        lblFwdFileNo.Text = Server.HtmlEncode(fileNo);
        txtFwdRemarks.Text = "";

        // district list; the application's own district is pre-selected (it can be changed)
        ddlFwdDistrict.DataSource = clsData.GetDataTable("SELECT DISTINCT DISTRICTNAME, DISTRICTCODE FROM dbo.mst_Commissionary_Districts ORDER BY DISTRICTNAME");
        ddlFwdDistrict.DataTextField = "DISTRICTNAME";
        ddlFwdDistrict.DataValueField = "DISTRICTCODE";
        ddlFwdDistrict.DataBind();
        ddlFwdDistrict.Items.Insert(0, new ListItem("--जिला चुनें--", "0"));
        string appDistrict = Convert.ToString(dt.Rows[0]["Vadi_District_Code"]);
        if (ddlFwdDistrict.Items.FindByValue(appDistrict) != null)
            ddlFwdDistrict.SelectedValue = appDistrict;

        UpdateForwardTargets();
        ShowModal("modalForward");
    }

    protected void ddlFwdDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateForwardTargets();
    }

    // shows the DM / SP login of the chosen district and locks a target the application was already forwarded to
    void UpdateForwardTargets()
    {
        chkFwdDM.Checked = chkFwdSP.Checked = false;
        chkFwdDM.Enabled = chkFwdSP.Enabled = true;
        lblFwdDMInfo.Text = lblFwdSPInfo.Text = "";
        if (ddlFwdDistrict.SelectedValue == "0")
            return;

        DataTable dt = clsData.GetDataTable(@"
            SELECT r.Role,
                   (SELECT TOP 1 u.UserID FROM dbo.UserLogin u
                    WHERE u.Userrole = r.Role AND u.District_Code = @DistrictCode ORDER BY u.UserID) AS UserID,
                   (SELECT TOP 1 f.ForwardedOn FROM dbo.ADMHOME_VadiApplicationForward f
                    INNER JOIN dbo.ADMHOME_VadiApplication a ON a.ApplicationId = f.ApplicationId
                    WHERE a.FileNo = @FileNo AND f.DistrictCode = @DistrictCode AND f.ForwardedToRole = r.Role) AS ForwardedOn
            FROM (VALUES ('DMOPT'), ('SSPOPT')) r(Role)",
            new SqlParameter[] {
                new SqlParameter("@DistrictCode", Convert.ToInt64(ddlFwdDistrict.SelectedValue)),
                new SqlParameter("@FileNo", hfFwdFileNo.Value) });

        foreach (DataRow r in dt.Rows)
        {
            bool isDM = Convert.ToString(r["Role"]) == "DMOPT";
            CheckBox chk = isDM ? chkFwdDM : chkFwdSP;
            Label info = isDM ? lblFwdDMInfo : lblFwdSPInfo;
            string user = Convert.ToString(r["UserID"]);

            if (r["ForwardedOn"] != DBNull.Value)
            {
                chk.Checked = true;
                chk.Enabled = false;
                info.Text = "पहले ही अग्रेषित: " + Convert.ToDateTime(r["ForwardedOn"]).ToString("dd/MM/yyyy hh:mm tt");
            }
            else
            {
                info.Text = user != "" ? "Login: " + Server.HtmlEncode(user) : "इस जिले में लॉगिन उपलब्ध नहीं है";
            }
        }
    }

    protected void btnFwdSubmit_Click(object sender, EventArgs e)
    {
        string fileNo = hfFwdFileNo.Value;
        // a disabled checkbox = already forwarded, so only enabled + ticked ones are new targets
        bool toDM = chkFwdDM.Enabled && chkFwdDM.Checked;
        bool toSP = chkFwdSP.Enabled && chkFwdSP.Checked;
        string remarks = txtFwdRemarks.Text.Trim();

        // on a validation error the pop-up is opened again with what the user entered
        if (ddlFwdDistrict.SelectedValue == "0") { Alert("कृपया जिला चुनें...!"); ShowModal("modalForward"); return; }
        if (!toDM && !toSP) { Alert("कृपया DM या SP (या दोनों) चुनें...!"); ShowModal("modalForward"); return; }
        if (remarks.Length > 500) { Alert("टिप्पणी अधिकतम 500 अक्षरों की हो सकती है...!"); ShowModal("modalForward"); return; }

        DataTable result = new DataTable();
        try
        {
            string cs = ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ADMHOME_ForwardVadiApplication", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileNo", fileNo);
                cmd.Parameters.AddWithValue("@DistrictCode", Convert.ToInt64(ddlFwdDistrict.SelectedValue));
                cmd.Parameters.AddWithValue("@ToDM", toDM);
                cmd.Parameters.AddWithValue("@ToSP", toSP);
                cmd.Parameters.AddWithValue("@ForwardRemarks", remarks == "" ? (object)DBNull.Value : remarks);
                cmd.Parameters.AddWithValue("@ForwardedBy", Convert.ToString(Session["UserID"]));
                cmd.Parameters.AddWithValue("@ForwardedIP", Request.UserHostAddress);
                cmd.Parameters.AddWithValue("@OwnerRole", DeptRole);   // the procedure refuses another department's file
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    da.Fill(result);
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            Alert("तकनीकी त्रुटि: आवेदन अग्रेषित नहीं हो सका, कृपया पुनः प्रयास करें...!");
            ShowModal("modalForward");
            return;
        }

        string districtName = ddlFwdDistrict.SelectedItem.Text;
        string done = "", already = "";
        foreach (DataRow r in result.Rows)
        {
            string who = Convert.ToString(r["Role"]) == "DMOPT" ? "DM" : "SP";
            if (Convert.ToString(r["Result"]) == "FORWARDED") done += (done == "" ? "" : ", ") + who;
            else already += (already == "" ? "" : ", ") + who;
        }

        string msg = done != "" ? "फाइल संख्या " + fileNo + " सफलतापूर्वक " + districtName + " के " + done + " को अग्रेषित किया गया।" : "";
        if (already != "") msg += (msg == "" ? "" : " ") + already + " को यह आवेदन पहले ही अग्रेषित किया जा चुका है।";
        Alert(msg);

        BindApplications();
    }

    // opens a Bootstrap pop-up once the page has loaded (see showADMHOMEModal in the .aspx)
    void ShowModal(string modalId)
    {
        ClientScript.RegisterStartupScript(GetType(), "showModal", "showADMHOMEModal('" + modalId + "');", true);
    }

    void Alert(string message)
    {
        // message only contains fixed text, district names and HDSB numbers; escape quotes anyway
        ClientScript.RegisterStartupScript(GetType(), "alert",
            "alert(" + System.Web.HttpUtility.JavaScriptStringEncode(message, true) + ");", true);
    }

    #endregion

    string Enc(object value)
    {
        string s = Convert.ToString(value);
        return s == "" ? "-" : Server.HtmlEncode(s);
    }
}
