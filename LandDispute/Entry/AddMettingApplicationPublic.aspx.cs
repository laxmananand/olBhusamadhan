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
          
            bhumikhata_Shekher_Ka_Vivaran(Id);
            vadiEvidanceKa_Vivaran(Id);
           
           
           
           
            bindAnchaladhikari(Id);
            //RemarkDetails(Id);
            bind_BhumiSanvedanshilta();
            BindPolice_wadi();
            BindPanchyat_Wadi();
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
        DataTable dt_Wadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print_public", new SqlParameter[] { _a_id, _fn2 });
        if (dt_Wadi_ka_vivran.Rows.Count > 0)
        {
            lblApplicationNo.Text= "आवेदन संख्या : " + dt_Wadi_ka_vivran.Rows[0]["ApplicationNo"].ToString();
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
               
             
            }
            else
            {
              
            }
        }
    }
    private void Wadi_ka_vivran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi");
        DataTable dt_wadi = clsData.GetDataTableWithProc("getEntryPage_print_public", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_wadi;
        gvWadi.DataSource = dt_wadi;
        gvWadi.DataBind();
    }
    private void PratiWadi_ka_vivran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadi");
        DataTable dt_Pratiwadi = clsData.GetDataTableWithProc("getEntryPage_print_public", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_Pratiwadi;
        pratiWadi_grid.DataSource = dt_Pratiwadi;
        pratiWadi_grid.DataBind();
    }
  
    private void bhumikhata_Shekher_Ka_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetBhumiKaKhataKhesraKaVivaran");
       
        DataTable dt_bhumikhata_Shekher_Ka_Vivaran = clsData.GetDataTableWithProc("getEntryPage_print_public", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_bhumikhata_Shekher_Ka_Vivaran;
        grd_bhumivivad.DataSource = dt_bhumikhata_Shekher_Ka_Vivaran;
        grd_bhumivivad.DataBind();
    }
    private void vadiEvidanceKa_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadiEvidance");       
        DataTable dt_vadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print_public", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_vadiEvidance;
        gdVadiEvidence.DataSource = dt_vadiEvidance;
        gdVadiEvidence.DataBind();
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
   
   
  
    private void bindAnchaladhikari(string Id)
    {
        try
        {
           
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication_public", new SqlParameter[] { GetQueryType, GetApplicationId });


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
    //protected void RemarkDetails(string Id)
    //{
    //    try
    //    {
    //        //SqlParameter GetQueryType = new SqlParameter("@QueryType", "4");
    //        SqlParameter GetApplicationId = new SqlParameter("@Id", Convert.ToInt64(Id));
    //        SqlParameter Userrole = new SqlParameter("@Userrole", Session["Role"].ToString());

    //        DataTable dt = clsData.GetDataTableWithProc("Sp_getRemarkDetails", new SqlParameter[] { GetApplicationId, Userrole });



    //        if (dt.Rows.Count > 0)
    //        {
    //            GridView1.DataSource = dt;
    //            GridView1.DataBind();
    //        }
    //        else
    //        {
    //            GridView1.DataSource = null;
    //            GridView1.DataBind();
    //        }
    //    }
    //    catch (Exception)
    //    {

    //    }

    //}
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
        divRejectReason.Visible = false;
        divOtherReason.Visible = false;
        divApplicationAlreadyEntered.Visible= false;
        divReturnReason.Visible = false;
        divReturnOther.Visible = false;

        if (ddlaction.SelectedValue == "4") // Aswikrit
        {
            divRejectReason.Visible = true;
        }
        if (ddlaction.SelectedValue == "7") // Reject
        {
            divReturnReason.Visible = true;
        }
        if (ddlaction.SelectedIndex == 1)
        {
            divNextDateDef.Visible = false;
            divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "प्रारंभिक निष्पादन की तिथि";
        }
        //else if (ddlaction.SelectedIndex == 2)
        //{
        //    divNextDateDef.Visible = false;
        //    divlabNextDate.Visible = true;
        //    labNextDate.Text = "अस्वीकृति का कारण";
        //    divCancelReason.Visible = true;

        //}
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

        if((lastAction.Value=="2" && ddlaction.SelectedIndex==1)|| (lastAction.Value == "2" && ddlaction.SelectedIndex == 5) )
        {
            lastActionMapi.Visible=true;
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
    protected void btnSave_Click1(object sender, EventArgs e)
    {

        if (!savevalidate())
        {
            return;
        }
        try
        {
            string Id = Request.QueryString["RegId"].ToString();
            Id = Id.Replace(' ', '+');
            Id = enc.Decrypt(Id);
            string Remark = string.Empty;
            string ThanaCode=string.Empty;
            string Panchayat = string.Empty;
            string ApplicationNo = string.Empty;

            string status = ddlDecision.SelectedValue;

            // Reject reason handling
            if (status == "R")
            {
                if (ddlRejectReason.SelectedValue == "2" ) // Other
                {
                    if (string.IsNullOrWhiteSpace(txtRejectOther.Text))
                    {
                        Utility.showMessage(Page, "कृपया Reject के लिए Remark भरें");
                        return;
                    }
                    Remark =  txtRejectOther.Text.Trim();
                }
                else
                {
                    Remark =  ddlRejectReason.SelectedItem.Text;
                }
                if(ddlRejectReason.SelectedValue == "1")
                    
                {
                    if (ddlThana.SelectedValue == "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Please select Thana');", true);
                        return;
                    }
                    if (ddlPanchayat.SelectedValue == "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Please select Panchayat');", true);
                        return;
                    }
                    if (ddlApplicationNo.SelectedValue == "0")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('Please select The Application NO');", true);
                        return;
                    }
                    ThanaCode=ddlThana.SelectedValue;
                    Panchayat=ddlPanchayat.SelectedValue;
                    ApplicationNo=ddlApplicationNo.SelectedValue;


                }
            }

            // Return reason handling
            if (status == "T")
            {
                if (ddlReturnReason.SelectedValue == "2" || ddlReturnReason.SelectedValue=="1") // Other
                {
                    if (string.IsNullOrWhiteSpace(txtReturnOther.Text))
                    {
                        Utility.showMessage(Page, "कृपया Return के लिए Remark भरें");
                        return;
                    }
                    Remark =  txtReturnOther.Text.Trim()+"/"+ ddlReturnReason.SelectedItem.Text;
                }
                
            }


            // Prepare SQL parameters
            SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt64(Id));
            SqlParameter _status = new SqlParameter("@status", status);
            //SqlParameter _Remark = new SqlParameter("@Remark", Remark);
            SqlParameter _Remark = new SqlParameter("@Remark",
  string.IsNullOrEmpty(Remark) ? (object)DBNull.Value : Remark);
            SqlParameter _Thanacode = new SqlParameter("@Thanacode",
     string.IsNullOrEmpty(ThanaCode) ? (object)DBNull.Value : ThanaCode);

            SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode ",
                string.IsNullOrEmpty(Panchayat) ? (object)DBNull.Value : Panchayat);

            SqlParameter _ApplicationNo = new SqlParameter("@ApplicationNo",
                string.IsNullOrEmpty(ApplicationNo) ? (object)DBNull.Value : ApplicationNo);
            SqlParameter _Cuby = new SqlParameter("@Cuby", Session["UserId"].ToString());
            SqlParameter _Cuipaddress = new SqlParameter("@CUIPAddress", GetUserIP().ToString());
            // Call stored procedure
            DataTable dt = clsData.GetDataTableWithProc("SP_InsertApplicationAccpRetRej",
                new SqlParameter[] { _a_id, _status, _Remark, _Thanacode, _PanchayatCode, _ApplicationNo, _Cuby, _Cuipaddress });
          
           
            // Handle response
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {
                    Utility.showMessage(Page, "Application status saved successfully ✅");

                    ddlDecision.SelectedIndex = 0;
                    ddlRejectReason.SelectedIndex = 0;
                    ddlReturnReason.SelectedIndex = 0;
                    txtRejectOther.Text = "";
                    txtReturnOther.Text = "";
                    ddlDecision.Enabled = false;
                    ddlRejectReason.Enabled = false;
                    ddlReturnReason.Enabled = false;
                    txtRejectOther.Enabled = false;
                    txtReturnOther.Enabled = false;
                    if (status == "Y")
                    {
                       
                        div_meeting.Visible = true;
                    }
                    else
                    {
                        div_meeting.Visible = false;
                    }
                }
                else
                {
                    Utility.showMessage(Page, dt.Rows[0]["Message"].ToString());
                }
            }
            else
            {
                Utility.showMessage(Page, "No response from database.");
            }
        }     
        catch (Exception ex)
        {
            Utility.showMessage(Page, "Error: " + ex.Message);
        }

    }
    private string GetUserIP()
    {
        string iMainpaddress = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(iMainpaddress))
        {
            return iMainpaddress.Split(',')[0];
        }

        return Request.ServerVariables["REMOTE_ADDR"];
    }

    protected bool savevalidate()
    {
        string decision = ddlDecision.SelectedValue;
        string remark = "";

        // 1. Validate main dropdown
        if (decision == "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('कृपया कोई विकल्प चुनें (Accept / Reject / Return)');", true);
            return false;
        }

        // 2. If Reject
        if (decision == "R")
        {
            if (ddlRejectReason.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('कृपया Reject का कारण चुनें');", true);
                return false;
            }
            else if (ddlRejectReason.SelectedValue == "2") // Other
            {
                if (string.IsNullOrWhiteSpace(txtRejectOther.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('कृपया Reject के लिए Remark भरें');", true);
                    return false;
                }
                remark = txtRejectOther.Text.Trim();
            }
            else
            {
                remark = ddlRejectReason.SelectedItem.Text;
            }
        }

        // 3. If Return
        else if (decision == "T")
        {
            if (ddlReturnReason.SelectedValue == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('कृपया Return का कारण चुनें');", true);
                return false;
            }
            else if (ddlReturnReason.SelectedValue == "2") // Other
            {
                if (string.IsNullOrWhiteSpace(txtReturnOther.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "alert('कृपया Return के लिए Remark भरें');", true);
                    return false;
                }
                remark = txtReturnOther.Text.Trim();
            }
            else
            {
                remark = ddlReturnReason.SelectedItem.Text;
            }
        }

        // 4. If Accept
        else if (decision == "Y")
        {
            remark = "Application Accepted";
        }

        return true; // ✅ Passed all validations
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
        bool flag=true;
        if ((lastAction.Value == "2" && ddlaction.SelectedIndex == 1) || (lastAction.Value == "2" && ddlaction.SelectedIndex == 5))
        {
            if (!lastActionMapiKaPrativadan.HasFile)
            {
                flag=false;
            }
           if(txtMapikiNirdharitThiti.Text=="")
            {
                flag = false;
            }
        }
        return flag;
    }
    private void SaveAnotherMetting()
    {
       if(Validation())
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
            if(ddlaction.SelectedValue=="5")
            {
                if (!CircleOfficer_letterOfIntent.HasFile)
                {
                    Utility.showMessage(Page, "कृपया अंचलाधिकारी का मंतव्य पत्र अपलोड करें।");


                    return; // Stop further execution
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
            if(txtabhiyukt_anchaladhikari.Text=="")
            {
                Utility.showMessage(Page, "कृपया अंचलाधिकारी का मंतव्य दर्ज करें");
                return;

            }
         
            if (txtfalafal.Text == "")
            {
                Utility.showMessage(Page, "कृपया बैठक में लिया गया निर्णय दर्ज करें");
                return;

            }
            string Remark = string.Empty;
            string ThanaCode = string.Empty;
            string Panchayat = string.Empty;
            string ApplicationNo = string.Empty;
            //string Remark = string.Empty;
            if (ddlaction.SelectedValue=="4")
            {
               if(ddlRejectReason.SelectedValue=="0")
                {
                    Utility.showMessage(Page, "Reject Reason select");
                    return;
                }
            }
            if(ddlRejectReason.SelectedValue == "1")
            {
                if (ddlThana.SelectedValue == "0")
                {
                    Utility.showMessage(Page, "select thana");
                    return;
                }
                if (ddlPanchayat.SelectedValue == "0")
                {
                    Utility.showMessage(Page, "select panchayat");
                    return;
                }
                if (ddlApplicationNo.SelectedValue == "0")
                {
                    Utility.showMessage(Page, "select Application");
                    return;
                }
                Remark = ddlRejectReason.SelectedItem.Text;
                ThanaCode = ddlThana.SelectedValue;
                Panchayat = ddlPanchayat.SelectedValue;
                ApplicationNo = ddlApplicationNo.SelectedValue;

            }
            if(ddlRejectReason.SelectedValue == "2")
            {
                if(txtRejectOther.Text=="")
                {
                    Utility.showMessage(Page, "Please fill remark for rejection");
                    return;
                }
                Remark = txtRejectOther.Text.Trim() + "/"+ ddlRejectReason.SelectedItem.Text;
            }
            if (ddlaction.SelectedValue == "7")
            {
                if (ddlReturnReason.SelectedValue == "0")
                {
                    Utility.showMessage(Page, "Return Reason select");
                    return;
                }
            }
            if(ddlReturnReason.SelectedValue == "1" || ddlReturnReason.SelectedValue == "2") 
            {
                if (txtReturnOther.Text == "")
                {
                    Utility.showMessage(Page, "Please fill remark for Return");
                    return;
                }
                Remark = txtReturnOther.Text.Trim() + "/" + ddlReturnReason.SelectedItem.Text;
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
            //SqlParameter reason_for_rejection = new SqlParameter("@reason_for_rejection", Remark);
            //SqlParameter reason_for_rejection = new SqlParameter("@reason_for_rejection", txtCancelReason.Text.Trim());
            SqlParameter mapi_ki_tithi = new SqlParameter("@mapi_ki_tithi", txtAgalaDate.Text.Trim() != "" ? txtAgalaDate.Text.Trim() : "01-01-1900");
            SqlParameter agali_sunavaee_ki_tithi = new SqlParameter("@agali_sunavaee_ki_tithi", txtAgalaDate.Text.Trim() != "" ? txtAgalaDate.Text.Trim() : "01-01-1900");

            SqlParameter _CircleOfficer_letterOfIntentFile = new SqlParameter("@CircleOfficer_letterOfIntent", _CircleOfficer_letterOfIntent);
            SqlParameter _PoliceOfficer_letterOfIntentFile = new SqlParameter("@PoliceOfficer_letterOfIntent", _PoliceOfficer_letterOfIntent);

            SqlParameter _bhumivivadki_sanvedanshilta = new SqlParameter("@Bhumi_savedansheelta", ddlbhumivivadki_sanvedanshilta.SelectedValue.Trim());

            SqlParameter _MapikaPrativadan = new SqlParameter("@MapikaPrativadan", _mapikinirdharitprativadan);
            SqlParameter _MapiKiNirdharitThith = new SqlParameter("@MapiKiNirdharitThith", txtMapikiNirdharitThiti.Text.Trim() != "" ? txtMapikiNirdharitThiti.Text.Trim() : "01-01-1900");
            SqlParameter _Remark = new SqlParameter("@Remark",
string.IsNullOrEmpty(Remark) ? (object)DBNull.Value : Remark);
            SqlParameter _Thanacode = new SqlParameter("@Thanacode",
     string.IsNullOrEmpty(ThanaCode) ? (object)DBNull.Value : ThanaCode);

            SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode ",
                string.IsNullOrEmpty(Panchayat) ? (object)DBNull.Value : Panchayat);

            SqlParameter _ApplicationNo = new SqlParameter("@ApplicationNo",
                string.IsNullOrEmpty(ApplicationNo) ? (object)DBNull.Value : ApplicationNo);

            DataTable dt = clsData.GetDataTableWithProc("Insert_Another_Metting_public", new SqlParameter[] 
            { GetApplicationId, Meeting_date, Is_Vadi_Present, Is_PratiVadi_Present, conclusion_of_the_meeting, anchala_dhikari_mantavy, thana_prabhari_mantavy, Joint_report_SHO_Circle_Officer_file, Matter_Status, Matter_Status_by, Matter_Status_date, date_of_disposal,  mapi_ki_tithi, agali_sunavaee_ki_tithi, _CircleOfficer_letterOfIntentFile
            , _PoliceOfficer_letterOfIntentFile, _bhumivivadki_sanvedanshilta, _MapikaPrativadan, _MapiKiNirdharitThith,_Remark,_Thanacode,_PanchayatCode,_ApplicationNo });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {
                    Utility.showMessage(Page, "नई बैठक के अनुसार अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी सफलतापूर्वक सहेजा गया");



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
                  
                    bhumikhata_Shekher_Ka_Vivaran(Id);
                    vadiEvidanceKa_Vivaran(Id);
                   
                  
                   
                  
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
    protected void ddlDecision_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Reset all hidden fields
        divRejectReason.Visible = false;
        divRejectOther.Visible = false;
        divReturnReason.Visible = false;
        divReturnOther.Visible = false;

        if (ddlDecision.SelectedValue == "R") // Reject
        {
            divRejectReason.Visible = true;
            div_meeting.Visible = false;
        }
        else if (ddlDecision.SelectedValue == "T") // Return
        {
            divReturnReason.Visible = true;
            div_meeting.Visible = false;
            divApplicationAlreadyEntered.Visible = false;
        }
        else if (ddlDecision.SelectedValue == "Y") // Return
        {
            //divReturnReason.Visible = true;
            div_meeting.Visible = false;
            divApplicationAlreadyEntered.Visible = false;
        }
        // Accept = nothing extra, only Save
    }

    protected void ddlRejectReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        divOtherReason.Visible = false;
        divApplicationAlreadyEntered.Visible=false;

        if (ddlRejectReason.SelectedValue == "2") // Other
        {
            divOtherReason.Visible = true;
        }
        if (ddlRejectReason.SelectedValue == "1") // Other
        {
            divApplicationAlreadyEntered.Visible = true;
        }
    }
    void BindPolice_wadi()
    {
        try
        {

            //string sql = @" select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t inner join Blocks p on t.Subdivision_Code =p.SubDivCode where p.DistCode=@District_Code  order by t.Police_Station";
            //string sql = @" select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t inner join SubDivisions p on t.Subdivision_Code =p.Sd_Code2 where p.Sd_Code2=@Subdivision_Code and p.DistCode=@District_Code and t.Circle_Code=@Circle_Code   order by Police_Station";
            //            string sql = @"select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t
            //--inner join Matter_Registration mm on t.PS_Code=mm.Thana_code
            //	                        left join MstThanaMapping m on m.Thana_Code=t.PS_Code 
            //	                        left join Blocks b on b.BlockCode=m.Code 
            //	                        where District_code=@District_Code and  b.SubDivCode is not null and m.code=@Circle_Code and b.SubDivCode=@Subdivision_Code
            //                            ORDER BY Police_Station";

            string sql = @"	select distinct b.BlockCode,b.BlockName,mt.PS_Code,mt.Police_Station from Matter_Registration mm
							inner join Blocks b on b.BlockCode=mm.Block_Code
							inner join mst_thana mt on mt.PS_Code=mm.Thana_code

							where mm.District_Code=@District_Code and Block_Code=@Circle_Code   ORDER BY Police_Station";
            SqlParameter _DCode = new SqlParameter("@District_Code", Session["District_Code"].ToString());
            //SqlParameter _SCode = new SqlParameter("@Subdivision_Code", Session["Sub_DivCode"].ToString());
            SqlParameter _Circle_Code = new SqlParameter("@Circle_Code", Session["Block_Code"].ToString());
            //SqlParameter _RCode = new SqlParameter("@Range_code", ddlrange.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _Circle_Code });
            ddlThana.DataSource = dt;
            ddlThana.DataTextField = "Police_Station";
            ddlThana.DataValueField = "PS_Code";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }
    }
    void BindPanchyat_Wadi()
    {
        try
        {


            string sql = @" 	select distinct mp.PanchayatCode,mp.PanchayatName from Matter_Registration mm
							inner join Blocks t on mm.Block_Code=t.BlockCode
							inner join mst_Panchayats mp on mm.Panchayat_Code=mp.PanchayatCode
							where Block_Code=@BlockCode order by PanchayatName ";

            //SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _BlockCode = new SqlParameter("@BlockCode", Session["Block_Code"].ToString());
            //SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", Session["Panchayat_code"].ToString());
            //SqlParameter _AreaType = new SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _BlockCode });


            ddlPanchayat.DataSource = dt;
            ddlPanchayat.DataTextField = "PanchayatName";
            ddlPanchayat.DataValueField = "PanchayatCode";
            ddlPanchayat.DataBind();
            ddlPanchayat.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlUserPanchyat.Items.Add(new ListItem("अन्य", "-1"));


        }
        catch { }

        // bindward_Wadi();
    }
    void BindApplicationNo()
    {
        try
        {


            string sql = @" select mm.Thana_code,mm.Panchayat_Code,mm.ApplicationNo from Matter_Registration mm
							inner join mst_thana b on mm.Thana_code=b.PS_Code
							inner join mst_Panchayats mp on mp.PanchayatCode=mm.Panchayat_Code
							where mm.Thana_code=@Thanacode and mm.Panchayat_Code=@Panchayatcode";

            //SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _Thanacode = new SqlParameter("@Thanacode", ddlThana.SelectedValue);
            SqlParameter _PanchayatCode = new SqlParameter("@Panchayatcode", ddlPanchayat.SelectedValue);
            //SqlParameter _AreaType = new SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _Thanacode, _PanchayatCode });


            ddlApplicationNo.DataSource = dt;
            ddlApplicationNo.DataTextField = "ApplicationNo";
            ddlApplicationNo.DataValueField = "ApplicationNo";
            ddlApplicationNo.DataBind();
            ddlApplicationNo.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlUserPanchyat.Items.Add(new ListItem("अन्य", "-1"));


        }
        catch { }

        // bindward_Wadi();
    }

    protected void ddlReturnReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        divReturnOther.Visible = (ddlReturnReason.SelectedValue == "1" || ddlReturnReason.SelectedValue == "2");
    }

    protected void ddlThana_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindApplicationNo();
    }

    protected void GVAnchalaDhakari_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton image3 = (ImageButton)e.Row.FindControl("Image3");
            image3.Attributes.Add("onclick", "return fnLinkbutton1('" + image3.ClientID + "')");


        }
    }
}


