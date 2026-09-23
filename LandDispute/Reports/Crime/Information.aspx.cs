using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class LandDispute_Dashboard_Information : System.Web.UI.Page
{
   
    DataSet ds;
    DataTable dtRepeaterTable;
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    PagedDataSource pageDataSource = new PagedDataSource();
  
  
    protected void Page_Load(object sender, EventArgs e)
    {
        string A_ID = Request.QueryString["RegId"].ToString();
              
        if (!IsPostBack)
        {
            ViewState["A_ID"] = A_ID;
            A_ID = A_ID.Replace(' ', '+');
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            string a_id = enc.Decrypt(A_ID);
            LoadAllApplication(a_id);
        }
            
    }
 
    /// <summary>  
    /// This function is used for loading all the current and active order which  
    /// needs to be delivered  
    /// </summary>  
    private void LoadAllApplication(string A_ID)
    {
        SqlParameter _fn2 = new SqlParameter("@fn", "getIDForViewData");
        SqlParameter _a_id = new SqlParameter("@a_id", A_ID);
        DataTable  dt = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _fn2, _a_id });
        parentRepeater.DataSource = dt;
        parentRepeater.DataBind();
    }
    private void Wadi_ka_vivran(string a_id, object  sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi");
        GridView gvWadi = (GridView)e.Item.FindControl("gvWadi");
        DataTable dt_wadi = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_wadi;
        gvWadi.DataSource = dt_wadi;
        gvWadi.DataBind();
    }
    private void WadikaVivaranDetails(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi_Vivaran");
        DataTable dt_Wadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id, _fn2 });
        if (dt_Wadi_ka_vivran.Rows.Count > 0)
        {
            ((Label)e.Item.FindControl("lblDistrict")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["DISTRICTNAME"].ToString();
            ((Label)e.Item.FindControl("lblSubdivision")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["Sd_Name_En"].ToString();
            ((Label)e.Item.FindControl("lblBlock")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["BlockName"].ToString();
            ((Label)e.Item.FindControl("lblPolice_Station")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["Police_Station"].ToString();
            ((Label)e.Item.FindControl("lblAreaType")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["AreaType"].ToString();
            if (dt_Wadi_ka_vivran.Rows[0]["AreaType"].ToString() == "Rural")
            {
                ((Label)e.Item.FindControl("lblVillage")).Text = "ग्राम पंचायत";
                e.Item.FindControl("div_Vadi_Svarajaya_Label").Visible = true;
                e.Item.FindControl("div_Vadi_Svarajaya").Visible = true;
                ((Label)e.Item.FindControl("lblVILLNAME")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["VILLNAME"].ToString();
            }
            else
            {
                ((Label)e.Item.FindControl("lblVillage")).Text = "नगर निकाय";
                e.Item.FindControl("div_Vadi_Svarajaya_Label").Visible = false;
                e.Item.FindControl("div_Vadi_Svarajaya").Visible = false;
                ((Label)e.Item.FindControl("lblVILLNAME")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["VILLNAME"].ToString();
            }
                ((Label)e.Item.FindControl("lblPanchayatName")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["PanchayatName"].ToString();
            ((Label)e.Item.FindControl("lblWARDNAME")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["WARDNAME"].ToString();
            ((Label)e.Item.FindControl("lblvadi_Vivad_Ka_Vighatan")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["status_name"].ToString();
            ((Label)e.Item.FindControl("lblvadi_rajashv_sankhaya")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["rajasv_thaana_sankhya"].ToString();
            ((Label)e.Item.FindControl("lblVadi_BhumiKaPrakar")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["Bhumitype_Ka_Prakar"].ToString();
            if (dt_Wadi_ka_vivran.Rows[0]["Bhumitype_Ka_PrakarID"].ToString() == "1")
            {
                e.Item.FindControl("div_vadi_sarkari_bhumi_ka_prakar_Label").Visible = false;
                e.Item.FindControl("div_Preview_vadi_sarkari_bhumi_ka_prakar").Visible = false;
                e.Item.FindControl("div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label").Visible = false;
                e.Item.FindControl("div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya").Visible = false;
            }
            else
            {
                e.Item.FindControl("div_vadi_sarkari_bhumi_ka_prakar_Label").Visible = true;
                e.Item.FindControl("div_Preview_vadi_sarkari_bhumi_ka_prakar").Visible = true;
                ((Label)e.Item.FindControl("lblvadi_sarkari_bhumi_ka_prakar_Label")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["Bhumitype_Ka_Prakar"].ToString();
                if (dt_Wadi_ka_vivran.Rows[0]["Bhumitype_Ka_Prakar"].ToString() == "6")
                {
                    e.Item.FindControl("div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label").Visible = true;
                    e.Item.FindControl("div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya").Visible = true;
                    ((Label)e.Item.FindControl("lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["SarkariBhumiType_Anya"].ToString();
                }
                else
                {
                    e.Item.FindControl("div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label").Visible = false;
                    e.Item.FindControl("div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya").Visible = false;
                }
            }
                ((Label)e.Item.FindControl("lblBhumiKa_VivadPrakar")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["bhumivivadtypename"].ToString();
            if (dt_Wadi_ka_vivran.Rows[0]["bhumivivadtypeID"].ToString() == "20")
            {
                e.Item.FindControl("div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label").Visible = true;
                e.Item.FindControl("div_Preview_vadi_Bhumivivad_Prakar_Anaya").Visible = true;
                ((Label)e.Item.FindControl("lblvadi_Bhumivivad_Prakar_Anaya")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["BhumiVivadType_Anya"].ToString();
            }
            else
            {
                e.Item.FindControl("div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label").Visible = false;
                e.Item.FindControl("div_Preview_vadi_Bhumivivad_Prakar_Anaya").Visible = false;
            }
                 ((Label)e.Item.FindControl("lblVadiKabhumiVivaran")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["VadiVivarani"].ToString();
            ((Label)e.Item.FindControl("lblPrativadiKabhumiVivaran")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["PrativadiVivarani"].ToString();
        }
    }
    private void PratiWadi_ka_vivran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadi");
        GridView gvPratiWadi = (GridView)e.Item.FindControl("pratiWadi_grid");
        DataTable dt_Pratiwadi = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_Pratiwadi;
        gvPratiWadi.DataSource = dt_Pratiwadi;
        gvPratiWadi.DataBind();
    }
    private void PratiWadikaVivaranDetails(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadiAnyaVivaran");
        DataTable dt_PratiWadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id, _fn2 });
        if(dt_PratiWadi_ka_vivran.Rows.Count > 0)
        {
            ((Label)e.Item.FindControl("lblprativadi_ka_suchit")).Text = ": " + dt_PratiWadi_ka_vivran.Rows[0]["prativadi_ko_suchit_kiya_gaya_hai_InHindi"].ToString();
            if (dt_PratiWadi_ka_vivran.Rows[0]["prativadi_ko_suchit_kiya_gaya_hai"].ToString() == "Y")
            {
                e.Item.FindControl("divprativadi_ka_Karan_Label").Visible = false;
                e.Item.FindControl("divprativadi_ka_Karan").Visible = false;
                e.Item.FindControl("divprativadi_ka_madham_Label").Visible = true;
                e.Item.FindControl("divprativadi_ka_madham").Visible = true;
                e.Item.FindControl("divprativadi_ka_Suchna_Label").Visible = true;
                e.Item.FindControl("divprativadi_ka_Suchna").Visible = true;
                e.Item.FindControl("divprativadi_ka_Upashtith_Label").Visible = true;
                e.Item.FindControl("divprativadi_ka_Upashtith").Visible = true;
                ((Label)e.Item.FindControl("lblprativadi_ka_madham")).Text = ": " + dt_PratiWadi_ka_vivran.Rows[0]["given_info_type"].ToString();
                ((Label)e.Item.FindControl("lblprativadi_ka_Suchna")).Text = ": " + dt_PratiWadi_ka_vivran.Rows[0]["prativadi_ko_suchana_ka_taamila_praapt_hai_InHindi"].ToString();
                ((Label)e.Item.FindControl("lblprativadi_ka_Upashtith")).Text = ": " + dt_PratiWadi_ka_vivran.Rows[0]["prativadi_upasthit_hua_hai_InHindi"].ToString();
            }
            else
            {
                e.Item.FindControl("divprativadi_ka_Karan_Label").Visible = true;
                e.Item.FindControl("divprativadi_ka_Karan").Visible = true;
                ((Label)e.Item.FindControl("lblprativadi_ka_Karan")).Text = dt_PratiWadi_ka_vivran.Rows[0]["given_info_desc"].ToString();
                e.Item.FindControl("divprativadi_ka_madham_Label").Visible = false;
                e.Item.FindControl("divprativadi_ka_madham").Visible = false;
                e.Item.FindControl("divprativadi_ka_Suchna_Label").Visible = false;
                e.Item.FindControl("divprativadi_ka_Suchna").Visible = false;
                e.Item.FindControl("divprativadi_ka_Upashtith_Label").Visible = false;
                e.Item.FindControl("divprativadi_ka_Upashtith").Visible = false;
            }
        }
       
    }
    private void bhumikhata_Shekher_Ka_Vivaran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetBhumiKaKhataKhesraKaVivaran");
        GridView gvbhumivivad = (GridView)e.Item.FindControl("grd_bhumivivad");
        DataTable dt_bhumikhata_Shekher_Ka_Vivaran = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_bhumikhata_Shekher_Ka_Vivaran;
        gvbhumivivad.DataSource = dt_bhumikhata_Shekher_Ka_Vivaran;
        gvbhumivivad.DataBind();
    }

    private void vadiEvidanceKa_Vivaran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadiEvidance");
        GridView gvvadiEvidance = (GridView)e.Item.FindControl("gdVadiEvidence");
        DataTable dt_vadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_vadiEvidance;
        gvvadiEvidance.DataSource = dt_vadiEvidance;
        gvvadiEvidance.DataBind();
    }

    private void PrativadiEvidanceKa_Vivaran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetPrativadiEvidance");
        GridView gvPrativadiEvidance = (GridView)e.Item.FindControl("gdPrativadiEvidence");
        DataTable dt_PrativadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_PrativadiEvidance;
        gvPrativadiEvidance.DataSource = dt_PrativadiEvidance;
        gvPrativadiEvidance.DataBind();
    }

    private void PoliceAdhikariVivranDetails(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPoliceAdhikariVivran");
        DataTable dt_PoliceAdhikariVivran = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id, _fn2 });
        if (dt_PoliceAdhikariVivran.Rows.Count > 0)
        {
            ((Label)e.Item.FindControl("lblPoliceAdhikari")).Text = ": "+ dt_PoliceAdhikariVivran.Rows[0]["pulis_padadhikari_vivarani"].ToString();
            ((Label)e.Item.FindControl("lblHalkaKarmchariValue")).Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["HalkaKarmchari_vivran"].ToString();
            ((Label)e.Item.FindControl("lblVivaditBhukandValue")).Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["vivadit_bhukhand_Mapi_ki_avashyakta_hai_Inhindi"].ToString();
            if (dt_PoliceAdhikariVivran.Rows[0]["vivadit_bhukhand_Mapi_ki_avashyakta_hai"].ToString() == "Y")
            {
                e.Item.FindControl("divMapi").Visible = true;
                ((Label)e.Item.FindControl("lblMapiValue")).Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["vivadit_bhukhand_Mapi_InHindi"].ToString();
                if (dt_PoliceAdhikariVivran.Rows[0]["vivadit_bhukhand_Mapi"].ToString() == "N")
                {
                    e.Item.FindControl("divVivaditBhukandKaMapi").Visible = true;
                    e.Item.FindControl("MapiKeNirdharnKiThithi").Visible = true;
                    ((Label)e.Item.FindControl("lblVivaditBhukandKaMapiValue")).Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["vivaadit_bhukhand_Mapi_Reason"].ToString();
                    ((Label)e.Item.FindControl("lblMapiKeNirdharnKiThithiValue")).Text = ": " + dt_PoliceAdhikariVivran.Rows[0]["maapee_ke_lie_nirdhaarit_tithi"].ToString();
                }
                else
                {
                    e.Item.FindControl("divVivaditBhukandKaMapi").Visible = false;
                    e.Item.FindControl("MapiKeNirdharnKiThithi").Visible = false;
                }
            }
            else
            {
                e.Item.FindControl("divMapi").Visible = false;
                e.Item.FindControl("divVivaditBhukandKaMapi").Visible = false;
                e.Item.FindControl("MapiKeNirdharnKiThithi").Visible = false;
            }
        }
    }

    private void LandDisputeDetails_ka_vivran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetLandDisputeDetailsEntry");
        GridView grdbhumivivad = (GridView)e.Item.FindControl("grdbhumivivad");
        DataTable dt_bhumivivad = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_bhumivivad;
        grdbhumivivad.DataSource = dt_bhumivivad;
        grdbhumivivad.DataBind();
        if(dt_bhumivivad.Rows.Count>0)
        {
            ((Label)e.Item.FindControl("lblPrathamik")).Text = dt_bhumivivad.Rows[0]["bhumi_vivad_Vivran_Available_Inhindi"].ToString();
        }       
    }

    private void CourtDisputeDetails_ka_vivran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetCourtDisputeDetailsEntry");
        GridView grdnyayalay_vivran = (GridView)e.Item.FindControl("grdnyayalay_vivran");
        DataTable dt_nyayalay_vivran = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_nyayalay_vivran;
        grdnyayalay_vivran.DataSource = dt_nyayalay_vivran;
        grdnyayalay_vivran.DataBind();
        if (dt_nyayalay_vivran.Rows.Count > 0)
        {
            ((Label)e.Item.FindControl("lblPrakiriyaVad")).Text = dt_nyayalay_vivran.Rows[0]["dispute_in_court_available"].ToString();
        }         
    }
    private void ActionDetailsEntryDetails_ka_vivran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getActionDetailsEntry");     
        DataTable dt_nyayalay_vivran = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_nyayalay_vivran;
       
        if (dt_nyayalay_vivran.Rows.Count > 0)
        {
            ((Label)e.Item.FindControl("lblVivaadKiSanvedanasheelata")).Text = ":"+ dt_nyayalay_vivran.Rows[0]["SensitivityType"].ToString();
            ((Label)e.Item.FindControl("lblBaithakKiTithi")).Text = ":" + dt_nyayalay_vivran.Rows[0]["Meeting_date"].ToString();
            ((Label)e.Item.FindControl("lblkyaVaadeeUpasthitHai")).Text = ":" + dt_nyayalay_vivran.Rows[0]["Is_Vadi_Present"].ToString();
            ((Label)e.Item.FindControl("lblKyaPrativaadeeUpasthitHai")).Text = ":" + dt_nyayalay_vivran.Rows[0]["Is_PratiVadi_Present"].ToString();
            ((Label)e.Item.FindControl("lblBaithakKaNishkarsh")).Text = ":" + dt_nyayalay_vivran.Rows[0]["baithak_ka_nishkarsh"].ToString();
            if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "4")
            {
                e.Item.FindControl("divAsveekrtiKaKaaranLabel").Visible = true;
                e.Item.FindControl("divtithi").Visible = false;                
                ((Label)e.Item.FindControl("lblAsveekrtiKaKaaran")).Text = ":" + dt_nyayalay_vivran.Rows[0]["reason_for_rejection"].ToString();
            }
            else
            {
                e.Item.FindControl("divtithi").Visible = true;
                e.Item.FindControl("divAsveekrtiKaKaaranLabel").Visible = false;
                if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "1")
                {
                    ((Label)e.Item.FindControl("lbltithi")).Text = "प्रारंभिक निष्पादन की तिथि";
                    ((Label)e.Item.FindControl("lbltithivalue")).Text = ":" + dt_nyayalay_vivran.Rows[0]["Matter_Status_date"].ToString();
                }
                else if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "2")
                {
                    ((Label)e.Item.FindControl("lbltithi")).Text = "मापी की तिथि";
                    ((Label)e.Item.FindControl("lbltithivalue")).Text = ":" + dt_nyayalay_vivran.Rows[0]["mapi_ki_tithi"].ToString();
                }
                else if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "3")
                {
                    ((Label)e.Item.FindControl("lbltithi")).Text = "अगली सुनवाई की तिथि ";
                    ((Label)e.Item.FindControl("lbltithivalue")).Text = ":"+ dt_nyayalay_vivran.Rows[0]["agali_sunavaee_ki_tithi"].ToString();
                }
                else if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "5")
                {
                    ((Label)e.Item.FindControl("lbltithi")).Text = "अंतिम निष्पादन की तिथि ";
                    ((Label)e.Item.FindControl("lbltithivalue")).Text = ":" + dt_nyayalay_vivran.Rows[0]["date_of_disposal"].ToString();
                }
                    
            }
            ((Label)e.Item.FindControl("lblBaithakMeinLiyaGayaNirnay")).Text = ":" + dt_nyayalay_vivran.Rows[0]["conclusion_of_the_meeting"].ToString();
            ((Label)e.Item.FindControl("lblAnchalaadhikaareeKaMantavy")).Text = ":" + dt_nyayalay_vivran.Rows[0]["anchala_dhikari_mantavy"].ToString();
            ((Label)e.Item.FindControl("lblThaanaadhyakshKaMantavy")).Text = ":" + dt_nyayalay_vivran.Rows[0]["thana_prabhari_mantavy"].ToString();
					
        }
    }
    protected void parentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.FindControl("div_Vadi_Svarajaya_Label").Visible = true;
                e.Item.FindControl("div_Vadi_Svarajaya").Visible = true;
                HiddenField lbla_id = (HiddenField)e.Item.FindControl("lbla_id");
                //वादी का विवरण
                Wadi_ka_vivran(lbla_id.Value, sender, e);
                //भूमि विवाद का विवरण
                WadikaVivaranDetails(lbla_id.Value, sender, e);
                //प्रतिवादी का विवरण   
                PratiWadi_ka_vivran(lbla_id.Value, sender, e);
                //प्रतिवादी का अन्य विवरण
                PratiWadikaVivaranDetails(lbla_id.Value, sender, e);
                //भूमि का खाता-खेसरा का विवरण
                bhumikhata_Shekher_Ka_Vivaran(lbla_id.Value, sender, e);
                //वादी द्वारा प्रस्तुत साक्ष्य का विवरण
                vadiEvidanceKa_Vivaran(lbla_id.Value, sender, e);
                //प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण
                PrativadiEvidanceKa_Vivaran(lbla_id.Value, sender, e);
                //पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी 
                PoliceAdhikariVivranDetails(lbla_id.Value, sender, e);

                //भूमि विवाद सें संबंधित घटना/ वारदात का विवरण
                LandDisputeDetails_ka_vivran(lbla_id.Value, sender, e);
                //न्यायालय में प्रक्रियाधीन वाद का विवरण
                CourtDisputeDetails_ka_vivran(lbla_id.Value, sender, e);
                //अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण
                ActionDetailsEntryDetails_ka_vivran(lbla_id.Value, sender, e);
            }
        }
        catch(Exception ex)
        { }
    }
   
  
   
    #region Pagination Events  
    protected void CommonButton_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        //if (b.ID == "btnFirst")
        //{
        //    CurrentPage = 0;
        //    LoadAllCurrentOrders();
        //}
        //else if (b.ID == "btnPrevious")
        //{
        //    CurrentPage -= 1;
        //    LoadAllCurrentOrders();
        //}
        //else if (b.ID == "btnNext")
        //{
        //    CurrentPage += 1;
        //    LoadAllCurrentOrders();
        //}
        //else if (b.ID == "btnLast")
        //{
        //    CurrentPage = Convert.ToInt32(ViewState["totalCount"].ToString()) - 1;
        //    LoadAllCurrentOrders();
        //}
    }
    #endregion

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //Response.Redirect("print.aspx?A_ID=" + ViewState["A_ID"],false);
        Response.Redirect("View.aspx?A_ID=" + ViewState["A_ID"].ToString(), false);
    }
  
}