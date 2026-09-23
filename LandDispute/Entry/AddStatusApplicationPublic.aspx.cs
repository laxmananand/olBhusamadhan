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
using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;

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
           
           
           
           
            //bindAnchaladhikari(Id);
           // RemarkDetails(Id);
            
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
        DataTable dt_Wadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id, _fn2 });
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
        DataTable dt_wadi = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_wadi;
        gvWadi.DataSource = dt_wadi;
        gvWadi.DataBind();
    }
    private void PratiWadi_ka_vivran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadi");
        DataTable dt_Pratiwadi = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_Pratiwadi;
        pratiWadi_grid.DataSource = dt_Pratiwadi;
        pratiWadi_grid.DataBind();
    }
  
    private void bhumikhata_Shekher_Ka_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetBhumiKaKhataKhesraKaVivaran");
       
        DataTable dt_bhumikhata_Shekher_Ka_Vivaran = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_bhumikhata_Shekher_Ka_Vivaran;
        grd_bhumivivad.DataSource = dt_bhumikhata_Shekher_Ka_Vivaran;
        grd_bhumivivad.DataBind();
    }
    private void vadiEvidanceKa_Vivaran(string a_id)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt64(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadiEvidance");       
        DataTable dt_vadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
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
        SaveStatus();
    }
    public bool validateFile(FileUpload fuFile, string FileType)
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
   
    private void SaveStatus()
    {
        if(validation()==true)
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Id.Replace(' ', '+');
            Id = enc.Decrypt(Id);

            SqlParameter GetApplicationId = new SqlParameter("@a_id", Convert.ToInt64(Id));
            SqlParameter _Status = new SqlParameter("@Status", ddlStatus.SelectedValue.ToString());

            SqlParameter _IsExists = new SqlParameter();
            _IsExists.ParameterName = "@IsExists";
            _IsExists.Value = DBNull.Value;

            SqlParameter _IsCourtCase = new SqlParameter();
            _IsCourtCase.ParameterName = "@IsCourtCase";
            _IsCourtCase.Value = DBNull.Value;

            SqlParameter _RemarksForReturn = new SqlParameter();
            _RemarksForReturn.ParameterName = "@RemarksForReturn";
            _RemarksForReturn.Value = DBNull.Value;
            //ExistsApplication_a_id
            SqlParameter _ExistsApplication_a_id = new SqlParameter();
            _ExistsApplication_a_id.ParameterName = "@ExistsApplication_a_id";
            _ExistsApplication_a_id.Value = DBNull.Value;



            if (ddlStatus.SelectedValue.ToString() == "2")
            {
               
                if (ddlReason.SelectedValue == "1")
                {
                    _ExistsApplication_a_id.Value = Convert.ToInt32(txtApplicationNo.Text.Trim().ToString());

                }
                else
                {
                    _IsCourtCase.Value = Convert.ToInt32(1);
                }
            }
            if (ddlStatus.SelectedValue.ToString() == "3")
            {
                _RemarksForReturn.Value =txtRemarks.Text.Trim().ToString();

            }

            DataTable dt = clsData.GetDataTableWithProc("usp_updatestatus", new SqlParameter[] { GetApplicationId, _Status , _IsExists, _IsCourtCase, _RemarksForReturn, _ExistsApplication_a_id });
            if (dt.Rows.Count > 0)
            {
               
                    lblMsg.Text = dt.Rows[0][0].ToString();
                Utility.showMessageNavigate(this, "Status Change", "ViewPublic.aspx");

            }
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

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        divRejected.Visible = false;
        divReturn.Visible = false;
        if(ddlStatus.SelectedValue.ToString()=="2")
        {
            divRejected.Visible = true;
        }
        if (ddlStatus.SelectedValue.ToString() == "3")
        {
            divReturn.Visible = true;
        }
    }

    protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
    {
        divApplication.Visible = false;
        if (ddlReason.SelectedValue=="1")
        {
            divApplication.Visible = true;
        }
    }

    public bool validation()
    {
        bool flag = true;
        if (ddlStatus.SelectedValue.ToString() == "0")
        {
            flag = false;
            lblMsg.Text = "Please Select Status";
        }
        if (ddlStatus.SelectedValue.ToString() == "2")
        {
            if (ddlReason.SelectedValue == "0")
            {
                flag = false;
                lblMsg.Text = "Please Select Reason";
            }
            if (ddlReason.SelectedValue == "1")
            {
                if(txtApplicationNo.Text.Trim()=="")
                {
                    flag = false;
                    lblMsg.Text = "Please Application No";
                }
                if(getApplicationNo()=="")
                {
                    flag = false;
                    lblMsg.Text = "Application No Not Exists";
                }
                
            }
        }
       if(ddlStatus.SelectedValue.ToString() == "3")
        {
            if(txtRemarks.Text=="")
            {
                flag = false;
                lblMsg.Text = "Please enter Remarks";
            }
        }
            return flag;
    }

    public string getApplicationNo()
    {
        string applicationno = "";
        string sql = "select a_id,ApplicationNo from Matter_Registration where ApplicationNo=@ApplicationNo";
        SqlParameter _ApplicationNo = new SqlParameter("@ApplicationNo", txtApplicationNo.Text);
        DataTable dt = clsData.GetDataTable("getEntryPage_print", new SqlParameter[] { _ApplicationNo });
        if(dt.Rows.Count>0)
        {
            applicationno = dt.Rows[0][1].ToString();
        }

        return applicationno;
    }
}