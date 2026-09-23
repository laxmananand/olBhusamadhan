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

public partial class Police_Station_add : System.Web.UI.Page
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
            //bind_BhumiSanvedanshilta();


            txtabhiyukt_thaanprabhaaree.Attributes.Add("maxlength", txtabhiyukt_thaanprabhaaree.MaxLength.ToString());
        }
        
    }
    public void bindMatterRegistration(string a_id)
    {
        //Id = Request.QueryString["RegId"].ToString();
        //Id = Id.Replace(' ', '+');
        //Id = enc.Decrypt(Id);
       // Id = "1082";

        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi_Vivaran");
        DataTable dt_Wadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id, _fn2 });
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
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi");
        DataTable dt_wadi = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_wadi;
        gvWadi.DataSource = dt_wadi;
        gvWadi.DataBind();
    }
    private void PratiWadi_ka_vivran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadi");
        DataTable dt_Pratiwadi = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_Pratiwadi;
        pratiWadi_grid.DataSource = dt_Pratiwadi;
        pratiWadi_grid.DataBind();
    }
    private void PratiWadikaVivaranDetails(string a_id)
    {
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadiAnyaVivaran");
        DataTable dt_PratiWadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id, _fn2 });
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
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetBhumiKaKhataKhesraKaVivaran");
       
        DataTable dt_bhumikhata_Shekher_Ka_Vivaran = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_bhumikhata_Shekher_Ka_Vivaran;
        grd_bhumivivad.DataSource = dt_bhumikhata_Shekher_Ka_Vivaran;
        grd_bhumivivad.DataBind();
    }
    private void vadiEvidanceKa_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadiEvidance");       
        DataTable dt_vadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_vadiEvidance;
        gdVadiEvidence.DataSource = dt_vadiEvidance;
        gdVadiEvidence.DataBind();
    }
    private void PrativadiEvidanceKa_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetPrativadiEvidance");       
        DataTable dt_PrativadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
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
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPoliceAdhikariVivran");
        DataTable dt_PoliceAdhikariVivran = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id, _fn2 });
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
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetLandDisputeDetailsEntry");       
        DataTable dt_bhumivivad = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
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
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetCourtDisputeDetailsEntry");
        DataTable dt_nyayalay_vivran = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
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

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });


            if (dt.Rows.Count > 0)
            {
                GVAnchalaDhakari.DataSource = dt;
                GVAnchalaDhakari.DataBind();

            }
            else
            {
                GVAnchalaDhakari.DataSource = dt;
                GVAnchalaDhakari.DataBind();
            }

        }
        catch (Exception)
        {

        }

    }

    protected void RemarkDetails(string Id)
    {
        try
        {            
            //SqlParameter GetQueryType = new SqlParameter("@QueryType", "4");
            SqlParameter GetApplicationId = new SqlParameter("@Id", Convert.ToInt32(Id));
            SqlParameter Userrole = new SqlParameter("@Userrole", Session["Role"].ToString());

            DataTable dt = clsData.GetDataTableWithProc("Sp_getRemarkDetails", new SqlParameter[] { GetApplicationId, Userrole });



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
        if (myValue == null || myValue.ToString()=="")
        {
            return false;
        }

        if (myValue is DBNull)
        {
            return false;
        }

        return true;
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
        SaveAnotherRemarks();
    }
    bool validateFile(FileUpload fuFile, string FileType)
    {
        if (fuFile.HasFile)
        {
            int contentLength = fuFile.PostedFile.ContentLength;
            string extension = Path.GetExtension(fuFile.PostedFile.FileName);

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
    private void SaveAnotherRemarks()
    {
        Id = Request.QueryString["RegId"].ToString();
        Id = Id.Replace(' ', '+');
        Id = enc.Decrypt(Id);
        string sql = @"select Count(*)+1 as Total from RemarksDetailsEntry where a_id=@a_id";
        DataTable dtremarks = new DataTable();
        SqlParameter A_Id = new SqlParameter("@a_id", Convert.ToInt64(Id));
        dtremarks = clsData.GetDataTable(sql, new SqlParameter[] { A_Id });
        string f = dtremarks.Rows.Count > 0 ? dtremarks.Rows[0][0].ToString() : "0";
        string landDoc_STR = string.Empty;
        if (LandDoc.HasFile)
        {
            string f1 = string.Empty;
            f1 = System.IO.Path.GetExtension(LandDoc.FileName);
            if (f1.ToString() == ".pdf")
            {
                string m = FileUploadValidator.IsPdf(LandDoc.PostedFile, 1024, 1024);
                if (m == "OK")
                {

                    landDoc_STR = "~/LandDoc/Upload/Remarks" + Id + "/Remarks" + f + ".pdf";
                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)";
                    return;
                }

            }
            else
            {
                Utility.showMessage(this, "Please Select only PDF File");
                return;
            }
        }


        SqlParameter GetApplicationId = new SqlParameter("@a_id", Convert.ToInt64(Id));

        SqlParameter Remarks = new SqlParameter("@Remarks", txtabhiyukt_thaanprabhaaree.Text.Trim());
        SqlParameter Remarks_file = new SqlParameter("@Remarks_file", landDoc_STR.Trim());

        SqlParameter Remarks_by = new SqlParameter("@Remarks_by", Session["UserId"].ToString());
        SqlParameter Created_date = new SqlParameter("@Created_date", DateTime.Now);

        DataTable dt = clsData.GetDataTableWithProc("Insert_Another_Remarks", new SqlParameter[] { GetApplicationId, Remarks, Remarks_file, Remarks_by, Created_date });
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
            {


                if (LandDoc.HasFile)
                {
                    string path = "~/LandDoc/Upload/Remarks" + Id + "/";
                    string v = InsSaveFile("Remarks" + f, LandDoc, path);
                }





                lblMsg.Text = "मंतव्य विवरण सफलतापूर्वक सहेजा गया";
                //Utility.showMessage(UpdatePanel2, "नई बैठक के अनुसार अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा " + "\n" + " भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी सफलतापूर्वक सहेजा गया");

                txtabhiyukt_thaanprabhaaree.Text = "";


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
    private string InsSaveFile(string fileName, FileUpload fuFile, string path)
    {
        string uploadDirectory = string.Empty;
        string pdfpath = FileSaveServer.getBase64(fuFile);
        string extension = string.Empty;
        extension = Path.GetExtension(fuFile.FileName).ToLower();
        uploadDirectory = path;
        string resi = FileSaveServer.InsertPDFNew(uploadDirectory, pdfpath, fileName, extension);
        return uploadDirectory + fileName + extension;
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
    protected void GVBhumiVivran_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GVBhumiVivran_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       

    }

    [System.Web.Services.WebMethod()]
    public static string Getpdf(string url)
    {
        Encryptor enc = new Encryptor(Encryptor.PrivateKey);
        string urlpath = "";
        string encPathgov = enc.EncodeTo64(url);
        encPathgov = Aes256CbcEncrypterApp.Encrypt(encPathgov, System.Web.HttpContext.Current.Session["aes256key"].ToString());
        urlpath = encPathgov;
        //string urlpath = "";
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
}