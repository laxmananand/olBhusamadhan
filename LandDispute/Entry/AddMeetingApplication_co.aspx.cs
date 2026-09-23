using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class LandDispute_Entry_AddMeetingApplication_co : System.Web.UI.Page
{
    DataSet ds;
    DataTable dtRepeaterTable;
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    PagedDataSource pageDataSource = new PagedDataSource();
    whiteList wl = new whiteList();
    Encryptor enc = new Encryptor(Encryptor.PrivateKey);
    String Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
        }
        if (!IsPostBack)
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Id.Replace(' ', '+');
            Id = enc.Decrypt(Id);
            bindMatterRegistration(Id);
            Wadi_ka_vivran(Id);
            PratiWadi_ka_vivran(Id);
            PratiWadikaVivaranDetails(Id);
            bhumikhata_Shekher_Ka_Vivaran(Id);
            vadiEvidanceKa_Vivaran(Id);
            PrativadiEvidanceKa_Vivaran(Id);
            PoliceAdhikariVivranDetails(Id);
            LandDisputeDetails_ka_vivran(Id);
            CourtDisputeDetails_ka_vivran(Id);
            bindAnchaladhikari(Id);
            RemarkDetails(Id);
            bind_BhumiSanvedanshilta();
            txtbaithakDate.Attributes.Add("readonly", "readonly");
            txtAgalaDate.Attributes.Add("readonly", "readonly");
            txtabhiyukt_anchaladhikari.Attributes.Add("maxlength", txtabhiyukt_anchaladhikari.MaxLength.ToString());
            txtCancelReason.Attributes.Add("maxlength", txtCancelReason.MaxLength.ToString());
            txtMapikiNirdharitThiti.Attributes.Add("readonly", "readonly");
            txtabhiyukt_thaanprabhaaree.Attributes.Add("maxlength", txtabhiyukt_thaanprabhaaree.MaxLength.ToString());
        }

    }
    public void bindMatterRegistration(string a_id)
    {
        //Id = Request.QueryString["RegId"].ToString();
        //Id = Id.Replace(' ', '+');
        //Id = enc.Decrypt(Id);
        // Id = "1082";

        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi_Vivaran");
        DataTable dt_Wadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id, _fn2 });
        if (dt_Wadi_ka_vivran.Rows.Count > 0)
        {
            lblApplicationNo.Text = "आवेदन संख्या : " + dt_Wadi_ka_vivran.Rows[0]["ApplicationNo"].ToString();
            lblAppDate.Text = "आवेदन की तिथि  : " + dt_Wadi_ka_vivran.Rows[0]["AavedanKiTithi"].ToString();
            lblDistrict.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["DISTRICTNAME"].ToString();
            lblSubdivision.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["Sd_Name_En"].ToString();
            lblBlock.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["BlockName"].ToString();
            lblPolice_Station.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["Police_Station"].ToString();
            lblAreaType.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["AreaType"].ToString();
            if (dt_Wadi_ka_vivran.Rows[0]["AreaType"].ToString() == "Rural")
            {
                lblVillage.Text = "ग्राम पंचायत";
                div_Vadi_Svarajaya_Label.Visible = true;
                div_Vadi_Svarajaya.Visible = true;
                lblVILLNAME.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["VILLNAME"].ToString();
            }
            else
            {
                lblVillage.Text = "नगर निकाय";
                div_Vadi_Svarajaya_Label.Visible = false;
                div_Vadi_Svarajaya.Visible = false;
            }
            lblPanchayatName.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["PanchayatName"].ToString();
            lblWARDNAME.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["WARDNAME"].ToString();
            lblvadi_Vivad_Ka_Vighatan.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["status_name"].ToString();
            lblvadi_rajashv_sankhaya.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["rajasv_thaana_sankhya"].ToString();
            lblVadi_BhumiKaPrakar.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["Bhumitype_Ka_Prakar"].ToString();
            if (dt_Wadi_ka_vivran.Rows[0]["Bhumitype_Ka_PrakarID"].ToString() == "1")
            {
                div_vadi_sarkari_bhumi_ka_prakar_Label.Visible = false;
                div_Preview_vadi_sarkari_bhumi_ka_prakar.Visible = false;
                div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label.Visible = false;
                div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya.Visible = false;
            }
            else
            {
                div_vadi_sarkari_bhumi_ka_prakar_Label.Visible = true;
                div_Preview_vadi_sarkari_bhumi_ka_prakar.Visible = true;
                lblvadi_sarkari_bhumi_ka_prakar_Label.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["Bhumitype_Ka_Prakar"].ToString();
                if (dt_Wadi_ka_vivran.Rows[0]["Bhumitype_Ka_Prakar"].ToString() == "6")
                {
                    div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label.Visible = true;
                    div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya.Visible = true;
                    lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["SarkariBhumiType_Anya"].ToString();
                }
                else
                {
                    div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label.Visible = false;
                    div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya.Visible = false;
                }
            }
            lblBhumiKa_VivadPrakar.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["bhumivivadtypename"].ToString();
            if (dt_Wadi_ka_vivran.Rows[0]["bhumivivadtypeID"].ToString() == "20")
            {
                div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label.Visible = true;
                div_Preview_vadi_Bhumivivad_Prakar_Anaya.Visible = true;
                lblvadi_Bhumivivad_Prakar_Anaya.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["BhumiVivadType_Anya"].ToString();
            }
            else
            {
                div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label.Visible = false;
                div_Preview_vadi_Bhumivivad_Prakar_Anaya.Visible = false;
            }
            lblVadiKabhumiVivaran.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["VadiVivarani"].ToString();
            lblPrativadiKabhumiVivaran.Text = ": " + dt_Wadi_ka_vivran.Rows[0]["PrativadiVivarani"].ToString();


            if (dt_Wadi_ka_vivran.Rows[0]["Vadi_sakshya_File"].ToString() != "")
            {
                lnkAppDoc.Visible = true;
                lnkAppDoc.Attributes.Add("path", dt_Wadi_ka_vivran.Rows[0]["Vadi_sakshya_File"].ToString());
            }
            else
            {
                lnkAppDoc.Visible = false;
            }
            if (dt_Wadi_ka_vivran.Rows[0]["Prativadi_sakshya_File"].ToString() != "")
            {
                lnkPrativadiDoc.Visible = true;
                lnkPrativadiDoc.Attributes.Add("path", dt_Wadi_ka_vivran.Rows[0]["Prativadi_sakshya_File"].ToString());
            }
            else
            {
                lnkPrativadiDoc.Visible = false;
            }
        }
    }
    private void Wadi_ka_vivran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi");
        DataTable dt_wadi = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_wadi;
        gvWadi.DataSource = dt_wadi;
        gvWadi.DataBind();
    }
    private void PratiWadi_ka_vivran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadi");
        DataTable dt_Pratiwadi = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_Pratiwadi;
        pratiWadi_grid.DataSource = dt_Pratiwadi;
        pratiWadi_grid.DataBind();
    }
    private void PratiWadikaVivaranDetails(string a_id)
    {
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadiAnyaVivaran");
        DataTable dt_PratiWadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id, _fn2 });
        if (dt_PratiWadi_ka_vivran.Rows.Count > 0)
        {
            lblprativadi_ka_suchit.Text = ": " + dt_PratiWadi_ka_vivran.Rows[0]["prativadi_ko_suchit_kiya_gaya_hai_InHindi"].ToString();
            if (dt_PratiWadi_ka_vivran.Rows[0]["prativadi_ko_suchit_kiya_gaya_hai"].ToString() == "Y")
            {
                divprativadi_ka_Karan_Label.Visible = false;
                divprativadi_ka_Karan.Visible = false;
                divprativadi_ka_madham_Label.Visible = true;
                divprativadi_ka_madham.Visible = true;
                divprativadi_ka_Suchna_Label.Visible = true;
                divprativadi_ka_Suchna.Visible = true;
                divprativadi_ka_Upashtith_Label.Visible = true;
                divprativadi_ka_Upashtith.Visible = true;
                lblprativadi_ka_madham.Text = ": " + dt_PratiWadi_ka_vivran.Rows[0]["given_info_type"].ToString();
                lblprativadi_ka_Suchna.Text = ": " + dt_PratiWadi_ka_vivran.Rows[0]["prativadi_ko_suchana_ka_taamila_praapt_hai_InHindi"].ToString();
                lblprativadi_ka_Upashtith.Text = ": " + dt_PratiWadi_ka_vivran.Rows[0]["prativadi_upasthit_hua_hai_InHindi"].ToString();
            }
            else
            {
                divprativadi_ka_Karan_Label.Visible = true;
                divprativadi_ka_Karan.Visible = true;
                lblprativadi_ka_Karan.Text = dt_PratiWadi_ka_vivran.Rows[0]["given_info_desc"].ToString();
                divprativadi_ka_madham_Label.Visible = false;
                divprativadi_ka_madham.Visible = false;
                divprativadi_ka_Suchna_Label.Visible = false;
                divprativadi_ka_Suchna.Visible = false;
                divprativadi_ka_Upashtith_Label.Visible = false;
                divprativadi_ka_Upashtith.Visible = false;
            }
        }

    }
    private void bhumikhata_Shekher_Ka_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetBhumiKaKhataKhesraKaVivaran");

        DataTable dt_bhumikhata_Shekher_Ka_Vivaran = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_bhumikhata_Shekher_Ka_Vivaran;
        grd_bhumivivad.DataSource = dt_bhumikhata_Shekher_Ka_Vivaran;
        grd_bhumivivad.DataBind();
    }
    private void vadiEvidanceKa_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadiEvidance");
        DataTable dt_vadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_vadiEvidance;
        gdVadiEvidence.DataSource = dt_vadiEvidance;
        gdVadiEvidence.DataBind();
    }
    private void PrativadiEvidanceKa_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetPrativadiEvidance");
        DataTable dt_PrativadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_PrativadiEvidance;
        gdPrativadiEvidence.DataSource = dt_PrativadiEvidance;
        gdPrativadiEvidence.DataBind();
    }
    protected void gdVadiEvidence_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");


        }
    }
    protected void gdPrativadiEvidence_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");


        }
    }
    private void PoliceAdhikariVivranDetails(string a_id)
    {
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPoliceAdhikariVivran");
        DataTable dt_PoliceAdhikariVivran = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id, _fn2 });
        if (dt_PoliceAdhikariVivran.Rows.Count > 0)
        {
            lblPoliceAdhikari.Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["pulis_padadhikari_vivarani"].ToString();
            lblHalkaKarmchariValue.Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["HalkaKarmchari_vivran"].ToString();
            lblVivaditBhukandValue.Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["vivadit_bhukhand_Mapi_ki_avashyakta_hai_Inhindi"].ToString();
            if (dt_PoliceAdhikariVivran.Rows[0]["vivadit_bhukhand_Mapi_ki_avashyakta_hai"].ToString() == "Y")
            {
                divMapi.Visible = true;
                lblMapiValue.Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["vivadit_bhukhand_Mapi_InHindi"].ToString();
                if (dt_PoliceAdhikariVivran.Rows[0]["vivadit_bhukhand_Mapi"].ToString() == "N")
                {
                    divVivaditBhukandKaMapi.Visible = true;
                    MapiKeNirdharnKiThithi.Visible = true;
                    lblVivaditBhukandKaMapiValue.Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["vivaadit_bhukhand_Mapi_Reason"].ToString();
                    lblMapiKeNirdharnKiThithiValue.Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["maapee_ke_lie_nirdhaarit_tithi"].ToString();
                }
                else
                {
                    divVivaditBhukandKaMapi.Visible = false;
                    MapiKeNirdharnKiThithi.Visible = false;
                }
            }
            else
            {
                divMapi.Visible = false;
                divVivaditBhukandKaMapi.Visible = false;
                MapiKeNirdharnKiThithi.Visible = false;
            }


            if (dt_PoliceAdhikariVivran.Rows[0]["pulis_padadhikar_Patr_file"].ToString() != "")
            {
                lnkpulis_padadhikari_Patr_file.Visible = true;
                lnkpulis_padadhikari_Patr_file.Attributes.Add("path", dt_PoliceAdhikariVivran.Rows[0]["pulis_padadhikar_Patr_file"].ToString());
            }
            else
            {
                lnkpulis_padadhikari_Patr_file.Visible = false;
            }
            if (dt_PoliceAdhikariVivran.Rows[0]["HalkaKarmchari_Patr_file"].ToString() != "")
            {
                lnkfile_halkakarmchari_praptr.Visible = true;
                lnkfile_halkakarmchari_praptr.Attributes.Add("path", dt_PoliceAdhikariVivran.Rows[0]["HalkaKarmchari_Patr_file"].ToString());
            }
            else
            {
                lnkfile_halkakarmchari_praptr.Visible = false;
            }
            if (dt_PoliceAdhikariVivran.Rows[0]["vivaadit_bhukhand_Mapi_File"].ToString() != "")
            {
                lnkfile_bhukand_prativedan.Visible = true;
                lnkfile_bhukand_prativedan.Attributes.Add("path", dt_PoliceAdhikariVivran.Rows[0]["vivaadit_bhukhand_Mapi_File"].ToString());
            }
            else
            {
                lnkfile_bhukand_prativedan.Visible = false;
            }

        }
    }
    private void LandDisputeDetails_ka_vivran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetLandDisputeDetailsEntry");
        DataTable dt_bhumivivad = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_bhumivivad;
        grdbhumivivad.DataSource = dt_bhumivivad;
        grdbhumivivad.DataBind();
        if (dt_bhumivivad.Rows.Count > 0)
        {
            lblPrathamik.Text = dt_bhumivivad.Rows[0]["bhumi_vivad_Vivran_Available_Inhindi"].ToString();
        }
    }
    private void CourtDisputeDetails_ka_vivran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetCourtDisputeDetailsEntry");
        DataTable dt_nyayalay_vivran = clsData.GetDataTableWithProc("getEntryPage_print_co", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_nyayalay_vivran;
        grdnyayalay_vivran.DataSource = dt_nyayalay_vivran;
        grdnyayalay_vivran.DataBind();
        if (dt_nyayalay_vivran.Rows.Count > 0)
        {
            lblPrakiriyaVad.Text = dt_nyayalay_vivran.Rows[0]["dispute_in_court_available"].ToString();
        }
    }
    private void bindAnchaladhikari(string Id)
    {
        try
        {

            SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication_co", new SqlParameter[] { GetQueryType, GetApplicationId });


            if (dt.Rows.Count > 0)
            {
                GVAnchalaDhakari.DataSource = dt;
                GVAnchalaDhakari.DataBind();
                lastAction.Value = dt.Rows[0]["Matter_Status"].ToString();

            }
            else
            {
                GVAnchalaDhakari.DataSource = dt;
                GVAnchalaDhakari.DataBind();
            }

        }
        catch (Exception ex)
        {

        }

    }
    protected void RemarkDetails(string Id)
    {
        try
        {
            //SqlParameter GetQueryType = new SqlParameter("@QueryType", "4");
            SqlParameter GetApplicationId = new SqlParameter("@Id", Convert.ToInt64(Id));
            SqlParameter Userrole = new SqlParameter("@Userrole", Session["Role"].ToString());

            DataTable dt = clsData.GetDataTableWithProc("Sp_getRemarkDetails_co", new SqlParameter[] { GetApplicationId, Userrole });



            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception)
        {

        }

    }
    protected void GridViewRemarks1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        DataRowView data = (DataRowView)e.Row.DataItem;

        if (string.IsNullOrWhiteSpace((string)data[0]))
        {
            e.Row.Visible = false;
        }


        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");



        }


    }
    public bool CheckNull(object myValue)
    {
        if (myValue == null)
        {
            return false;
        }

        if (myValue is DBNull)
        {
            return false;
        }

        return true;
    }
    protected void bind_BhumiSanvedanshilta()// भूमि विवाद कि सवेदनशीलता
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_SensitivityType", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlbhumivivadki_sanvedanshilta.DataSource = dt;
                    ddlbhumivivadki_sanvedanshilta.DataTextField = "SensitivityType";
                    ddlbhumivivadki_sanvedanshilta.DataValueField = "id";
                    ddlbhumivivadki_sanvedanshilta.DataBind();
                    ddlbhumivivadki_sanvedanshilta.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
        }
        catch (Exception ee)
        {


        }
    }
    protected void ddlaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        divNextDateDef.Visible = true;
        divlabNextDate.Visible = false;
        divNextDate.Visible = false;
        divCancelReason.Visible = false;
        divvadkavars.Visible = false;
        txtAgalaDate.Text = "";
        txtCancelReason.Text = "";
        //txtAgalaDate,txtCancelReason, txtAgalaDate,labNextDate

        if (ddlaction.SelectedIndex == 1)
        {
            divNextDateDef.Visible = false;
            divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "प्रारंभिक निष्पादन की तिथि";
        }
        else if (ddlaction.SelectedIndex == 2)
        {
            divNextDateDef.Visible = false;
            divlabNextDate.Visible = true;
            labNextDate.Text = "अस्वीकृति का कारण";
            divCancelReason.Visible = true;

        }
        else if (ddlaction.SelectedIndex == 3)
        {
            divNextDateDef.Visible = false;
            divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "मापी की तिथि";
        }
        else if (ddlaction.SelectedIndex == 4)
        {
            divNextDateDef.Visible = false;
            divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "अगली सुनवाई की तिथि";
        }
        else if (ddlaction.SelectedIndex == 5)
        {
            divNextDateDef.Visible = false;
            divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "अंतिम निष्पादन की तिथि";
        }
        else if (ddlaction.SelectedIndex == 6)
        {
            divNextDateDef.Visible = false;
            divvadkavars.Visible = true;
            divlabNextDate.Visible = true;
            labNextDate.Text = "वादी की वाद संख्या / वर्ष";
        }

        if ((lastAction.Value == "2" && ddlaction.SelectedIndex == 1) || (lastAction.Value == "2" && ddlaction.SelectedIndex == 5))
        {
            lastActionMapi.Visible = true;
        }


    }
    protected void ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "0")
        {
            onestar.Visible = false;
            twostar.Visible = false;
            threestar.Visible = false;
            fourstar.Visible = false;
        }
        else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "1")
        {
            onestar.Visible = true;
            twostar.Visible = false;
            threestar.Visible = false;
            fourstar.Visible = false;
        }
        else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "2")
        {
            onestar.Visible = false;
            twostar.Visible = true;
            threestar.Visible = false;
            fourstar.Visible = false;
        }
        else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "3")
        {
            onestar.Visible = false;
            twostar.Visible = false;
            threestar.Visible = true;
            fourstar.Visible = false;
        }
        else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "4")
        {
            onestar.Visible = false;
            twostar.Visible = false;
            threestar.Visible = false;
            fourstar.Visible = true;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        if (Session["REFRESH_METTING_SEARCH"] != null)
        {
            Response.Redirect("SearchAppForMetting.aspx", false);
            Session["REFRESH_METTING_SEARCH"] = null;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session["REFRESH_METTING_SEARCH"] = null;
        SaveAnotherMetting();
    }
    bool validateFile(FileUpload fuFile, string FileType)
    {
        if (fuFile.HasFile)
        {
            int contentLength = fuFile.PostedFile.ContentLength;
            string extension = Path.GetExtension(fuFile.PostedFile.FileName);
            string mimeType = fuFile.PostedFile.ContentType;
            string allowedMimeType = "application/pdf";
            if (mimeType == allowedMimeType)
            {
                switch (FileType)
                {
                    case "zip":
                        switch (extension.ToLower())
                        {
                            case ".zip":
                                break;
                            default:
                                lblMsg.Text = "This file type is not allowed.";
                                // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                return false;
                        }

                        if (contentLength > (1 * 1024 * 1024))
                        {
                            lblMsg.Text = "File size must be less than or equal to 1 MB";
                            return false;
                        }
                        break;
                    case "doc":

                        switch (extension.ToLower())
                        {
                            //case ".jMD":
                            //case ".jpeg":
                            case ".pdf":

                                break;
                            default:
                                lblMsg.Text = "This file type is not allowed.";
                                // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                return false;
                        }
                        if (contentLength > (0.2 * 1024 * 1024))
                        {
                            lblMsg.Text = "File size must be less than or equal to 400KB";
                            // return false;
                        }
                        break;
                    case "Image":

                        switch (extension.ToLower())
                        {

                            case ".png":
                            case ".PNG":
                            case ".jpg":
                            case ".JPG":
                            case ".jpeg":
                            case ".JPEG":


                                break;
                            default:
                                lblMsg.Text = "This file type is not allowed.";
                                // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                return false;
                        }
                        if (contentLength > (0.4 * 1024 * 1024))
                        {
                            lblMsg.Text = "File size must be less than or equal to 400KB";
                            return false;
                        }
                        break;



                    default:
                        lblMsg.Text = "Unknown File Type !!";
                        return false;
                }
            }
            else
            {
                lblMsg.Text = "Invalid file type. Only PDF files are allowed.";
                return false;
            }
        }
        return true;
    }
    private string InsSaveFile(string fileName, FileUpload fuFile, string a_id, string path)
    {
        string uploadDirectory = string.Empty;
        string pdfpath = FileSaveServer.getBase64(fuFile);
        string extension = string.Empty;
        extension = Path.GetExtension(fuFile.FileName).ToLower();
        uploadDirectory = path;
        string resi = FileSaveServer.InsertPDFNew(uploadDirectory, pdfpath, fileName, extension);
        return uploadDirectory + fileName + extension;
    }
    public bool Validation()
    {
        bool flag = true;
        if ((lastAction.Value == "2" && ddlaction.SelectedIndex == 1) || (lastAction.Value == "2" && ddlaction.SelectedIndex == 5))
        {
            if (!lastActionMapiKaPrativadan.HasFile)
            {
                flag = false;
            }
            if (txtMapikiNirdharitThiti.Text == "")
            {
                flag = false;
            }
        }
        return flag;
    }
    private void SaveAnotherMetting()
    {
        if (Validation())
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Id.Replace(' ', '+');
            Id = enc.Decrypt(Id);
            // Id = "1082";
            string _CircleOfficer_letterOfIntent = string.Empty;
            string _PoliceOfficer_letterOfIntent = string.Empty;
            string _mapikinirdharitprativadan = string.Empty;
            string sql = @"select * from ActionDetailsEntry where a_id=@a_id";
            SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt64(Id));
            DataTable dtActionGetdata = clsData.GetDataTable(sql, new SqlParameter[] { _a_id });
            string landDoc_STR = string.Empty;
            string message = "";
            if (LandDoc.HasFile)
            {
                if (!validateFile(LandDoc, "doc"))
                    return;
                string m = FileUploadValidator.IsPdf(LandDoc.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    landDoc_STR = "~/LandDoc/Upload/LandDocuments" + Convert.ToInt64(Id) + "/LandDocuments" + (dtActionGetdata.Rows.Count + 1) + ".pdf";
                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)";
                    return;
                }
            }

            if (CircleOfficer_letterOfIntent.HasFile)
            {
                if (!validateFile(CircleOfficer_letterOfIntent, "doc"))
                    return;
                string m = FileUploadValidator.IsPdf(CircleOfficer_letterOfIntent.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    _CircleOfficer_letterOfIntent = "~/LandDoc/Upload/CirclePulisPadadhikariPatr" + Convert.ToInt64(Id) + "/CirclePulisPadadhikariPatr" + (dtActionGetdata.Rows.Count + 1) + ".pdf";
                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)";
                    return;
                }
            }


            if (PoliceOfficer_letterOfIntent.HasFile)
            {
                if (!validateFile(PoliceOfficer_letterOfIntent, "doc"))
                    return;
                string m = FileUploadValidator.IsPdf(PoliceOfficer_letterOfIntent.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    _PoliceOfficer_letterOfIntent = "~/LandDoc/Upload/PulisPadadhikariPatr" + Convert.ToInt64(Id) + "/PulisPadadhikariPatr" + (dtActionGetdata.Rows.Count + 1) + ".pdf";
                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)";
                    return;
                }
            }
            if (lastActionMapiKaPrativadan.HasFile)
            {
                if (!validateFile(lastActionMapiKaPrativadan, "doc"))
                    return;
                string m = FileUploadValidator.IsPdf(lastActionMapiKaPrativadan.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    _mapikinirdharitprativadan = "~/LandDoc/Upload/MapiKaPrativadan" + Convert.ToInt64(Id) + "/MapiKaPrativadan" + (dtActionGetdata.Rows.Count + 1) + ".pdf";
                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)";
                    return;
                }
            }

            if (LandDoc.HasFile)
            {
                string path = "~/LandDoc/Upload/LandDocuments" + Id + "/";
                string v = InsSaveFile("LandDocuments" + (dtActionGetdata.Rows.Count + 1), LandDoc, Id, path);
                if (v == "0")
                {
                    landDoc_STR = "";
                    message = "LandDocuments not upload";
                }
            }

            if (CircleOfficer_letterOfIntent.HasFile)
            {
                string path = "~/LandDoc/Upload/CirclePulisPadadhikariPatr" + Id + "/";
                string v = InsSaveFile("CirclePulisPadadhikariPatr" + (dtActionGetdata.Rows.Count + 1), CircleOfficer_letterOfIntent, Id, path);
                if (v == "0")
                {
                    _CircleOfficer_letterOfIntent = "";
                    message = message + "; CirclePulisPadadhikariPatr not upload";
                }
            }
            if (PoliceOfficer_letterOfIntent.HasFile)
            {
                string path = "~/LandDoc/Upload/PulisPadadhikariPatr" + Id + "/";
                string v = InsSaveFile("PulisPadadhikariPatr" + (dtActionGetdata.Rows.Count + 1), PoliceOfficer_letterOfIntent, Id, path);
                if (v == "0")
                {
                    _PoliceOfficer_letterOfIntent = "";
                    message = message + "; PulisPadadhikariPatr not upload";
                }
            }
            if ((lastAction.Value == "2" && ddlaction.SelectedIndex == 1) || (lastAction.Value == "2" && ddlaction.SelectedIndex == 5))
            {
                if (lastActionMapiKaPrativadan.HasFile)
                {
                    string path = "~/LandDoc/Upload/MapiKaPrativadan" + Id + "/";
                    string v = InsSaveFile("MapiKaPrativadan" + (dtActionGetdata.Rows.Count + 1), lastActionMapiKaPrativadan, Id, path);
                    if (v == "0")
                    {
                        return;
                        _mapikinirdharitprativadan = "";
                        message = message + "; Mapi ki nirdharit prativadan not upload";
                    }
                }
            }





            SqlParameter GetApplicationId = new SqlParameter("@a_id", Convert.ToInt64(Id));

            SqlParameter Meeting_date = new SqlParameter("@Meeting_date", Convert.ToDateTime(DateTime.ParseExact(txtbaithakDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture)));
            SqlParameter Is_Vadi_Present = new SqlParameter("@Is_Vadi_Present", ddlIsVadiAvailable.SelectedValue.Trim());
            SqlParameter Is_PratiVadi_Present = new SqlParameter("@Is_PratiVadi_Present", ddl_IsprativadiAvailable.SelectedValue.Trim());
            SqlParameter conclusion_of_the_meeting = new SqlParameter("@conclusion_of_the_meeting", txtfalafal.Text.Trim());
            SqlParameter anchala_dhikari_mantavy = new SqlParameter("@anchala_dhikari_mantavy", txtabhiyukt_anchaladhikari.Text.Trim());
            SqlParameter thana_prabhari_mantavy = new SqlParameter("@thana_prabhari_mantavy", txtabhiyukt_thaanprabhaaree.Text.Trim());
            SqlParameter Joint_report_SHO_Circle_Officer_file = new SqlParameter("@Joint_report_SHO_Circle_Officer_file", landDoc_STR.Trim());
            SqlParameter Matter_Status = new SqlParameter("@Matter_Status", ddlaction.SelectedValue.Trim());
            SqlParameter Matter_Status_by = new SqlParameter("@Matter_Status_by", Session["UserId"].ToString());
            SqlParameter Matter_Status_date = new SqlParameter("@Matter_Status_date", DateTime.Now);

            //SqlParameter date_of_disposal = new SqlParameter("@date_of_disposal", Convert.ToDateTime(DateTime.ParseExact(txtAgalaDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture)));
            //SqlParameter reason_for_rejection = new SqlParameter("@reason_for_rejection", txtCancelReason.Text.Trim());
            //SqlParameter mapi_ki_tithi = new SqlParameter("@mapi_ki_tithi", Convert.ToDateTime(DateTime.ParseExact(txtAgalaDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture)));
            //SqlParameter agali_sunavaee_ki_tithi = new SqlParameter("@agali_sunavaee_ki_tithi", Convert.ToDateTime(DateTime.ParseExact(txtAgalaDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture)));


            SqlParameter date_of_disposal = new SqlParameter("@date_of_disposal", txtAgalaDate.Text.Trim() != "" ? txtAgalaDate.Text.Trim() : "01-01-1900");

            SqlParameter reason_for_rejection = new SqlParameter("@reason_for_rejection", txtCancelReason.Text.Trim());
            SqlParameter mapi_ki_tithi = new SqlParameter("@mapi_ki_tithi", txtAgalaDate.Text.Trim() != "" ? txtAgalaDate.Text.Trim() : "01-01-1900");
            SqlParameter agali_sunavaee_ki_tithi = new SqlParameter("@agali_sunavaee_ki_tithi", txtAgalaDate.Text.Trim() != "" ? txtAgalaDate.Text.Trim() : "01-01-1900");

            SqlParameter _CircleOfficer_letterOfIntentFile = new SqlParameter("@CircleOfficer_letterOfIntent", _CircleOfficer_letterOfIntent);
            SqlParameter _PoliceOfficer_letterOfIntentFile = new SqlParameter("@PoliceOfficer_letterOfIntent", _PoliceOfficer_letterOfIntent);

            SqlParameter _bhumivivadki_sanvedanshilta = new SqlParameter("@Bhumi_savedansheelta", ddlbhumivivadki_sanvedanshilta.SelectedValue.Trim());

            SqlParameter _MapikaPrativadan = new SqlParameter("@MapikaPrativadan", _mapikinirdharitprativadan);
            SqlParameter _MapiKiNirdharitThith = new SqlParameter("@MapiKiNirdharitThith", txtMapikiNirdharitThiti.Text.Trim() != "" ? txtMapikiNirdharitThiti.Text.Trim() : "01-01-1900");

            DataTable dt = clsData.GetDataTableWithProc("Insert_Another_Metting_co", new SqlParameter[] { GetApplicationId, Meeting_date, Is_Vadi_Present, Is_PratiVadi_Present, conclusion_of_the_meeting, anchala_dhikari_mantavy, thana_prabhari_mantavy, Joint_report_SHO_Circle_Officer_file, Matter_Status, Matter_Status_by, Matter_Status_date, date_of_disposal, reason_for_rejection, mapi_ki_tithi, agali_sunavaee_ki_tithi, _CircleOfficer_letterOfIntentFile, _PoliceOfficer_letterOfIntentFile, _bhumivivadki_sanvedanshilta, _MapikaPrativadan, _MapiKiNirdharitThith });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {



                    lblMsg.Text = "नई बैठक के अनुसार अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी सफलतापूर्वक सहेजा गया";
                    // Utility.showMessage(UpdatePanel2, "नई बैठक के अनुसार अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा " + "\n" + " भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी सफलतापूर्वक सहेजा गया");
                    //Response.Redirect("SearchAppForMetting.aspx");
                    //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                    //Utility.showMessage(Page, "आवेदनकर्ता का विवरण सफलतापूर्वक सहेजा गया");

                    ddlbhumivivadki_sanvedanshilta.SelectedIndex = 0;
                    txtbaithakDate.Text = "";
                    ddlIsVadiAvailable.SelectedIndex = 0;
                    ddl_IsprativadiAvailable.SelectedIndex = 0;
                    ddlaction.SelectedIndex = 0;
                    txtfalafal.Text = "";
                    txtabhiyukt_thaanprabhaaree.Text = "";
                    txtCancelReason.Text = "";
                    labNextDate.Text = "";
                    txtAgalaDate.Text = "";

                    txtabhiyukt_anchaladhikari.Text = "";

                    ddlaction_SelectedIndexChanged(ddlaction, EventArgs.Empty);
                    ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged(ddlbhumivivadki_sanvedanshilta, EventArgs.Empty);



                    bindMatterRegistration(Id);
                    Wadi_ka_vivran(Id);
                    PratiWadi_ka_vivran(Id);
                    PratiWadikaVivaranDetails(Id);
                    bhumikhata_Shekher_Ka_Vivaran(Id);
                    vadiEvidanceKa_Vivaran(Id);
                    PrativadiEvidanceKa_Vivaran(Id);
                    PoliceAdhikariVivranDetails(Id);
                    LandDisputeDetails_ka_vivran(Id);
                    CourtDisputeDetails_ka_vivran(Id);
                    bindAnchaladhikari(Id);

                    Session["REFRESH_METTING_SEARCH"] = 1;

                    return;
                }
                else
                {

                    lblMsg.Text = dt.Rows[0]["Message"].ToString();
                    Utility.showMessage(Page, dt.Rows[0]["Message"].ToString());
                    return;
                }

            }


        }
        else
        {
            Utility.showMessage(Page, "कृपया मापी का प्रतिवेदन चुनें एवं मापी क़े लिए निर्धारित तिथि अंकित करें");
        }











    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }
        DataRowView data = (DataRowView)e.Row.DataItem;

        if (string.IsNullOrWhiteSpace((string)data[0]))
        {
            e.Row.Visible = false;
        }


        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");



        }


    }
    protected void GVBhumiVivran_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {


    }
    [System.Web.Services.WebMethod()]
    public static string Getpdf(string url)
    {
        string urlpath = "";
        Encryptor enc = new Encryptor(Encryptor.PrivateKey);

        string encPathgov = enc.EncodeTo64(url);
        encPathgov = Aes256CbcEncrypterApp.Encrypt(encPathgov, System.Web.HttpContext.Current.Session["aes256key"].ToString());
        urlpath = encPathgov;
        //try
        //{
        //    using (var webClient = new WebClient())
        //    {
        //        byte[] imageBytes = webClient.DownloadData(url);
        //        string imreBase64Data = Convert.ToBase64String(imageBytes);
        //        string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);
        //        urlpath = imgDataURL;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    urlpath = ex.Message;
        //}

        return urlpath;
    }
    public bool CheckImage(object url)
    {
        if (url.ToString() != "")
        {
            string p = (url.ToString()).Replace("~", "");
            url = "http://localhost:8080" + p;
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(url.ToString());
                    string imreBase64Data = Convert.ToBase64String(imageBytes);
                    string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);

                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        else
        {
            return false;
        }


    }
}