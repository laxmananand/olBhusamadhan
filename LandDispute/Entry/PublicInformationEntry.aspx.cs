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
using System.Net;
using EO.Web.Internal;

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
            ViewState["a_id"] = a_id;
            LoadAllApplication(a_id);
            //LoadDayAssign(a_id);
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
        DataTable  dt = clsData.GetDataTableWithProc("getEntryPage_print_Information_public", new SqlParameter[] { _fn2, _a_id });
        parentRepeater.DataSource = dt;
        parentRepeater.DataBind();
    }
    private void LoadDayAssign(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        DataTable dt = clsData.GetDataTableWithProc("Sp_assigndatesat", new SqlParameter[] { _a_id });

        if (dt.Rows.Count > 0)  
        {
            // Get dynamic values from SP result
            string appointmentDate = Convert.ToDateTime(dt.Rows[0]["FinalAppointmentDate"]).ToString("dd-MM-yyyy");
            string blockName = dt.Rows[0]["BlockName"].ToString();

            // Message template with placeholders
            string messageTemplate =
         "आपका दस्तावेज़ सफलतापूर्वक अपलोड हो गया है। कृपया दिनांक " +
         "<span style='font-weight:600;font-size:14px;color:#E74C3C'>{0}</span> " +
         "(शनिवार) को अपने " +
         "<span style='font-weight:600;font-size:14px;color:#2E86C1'>{1}</span> " +
         "अंचल कार्यालय में जाकर अपलोड किए गए भूमि संबंधी दस्तावेज़ की छायाप्रति तथा शिकायत पत्र " +
         "सर्किल अधिकारी एवं थाना प्रभारी के समक्ष प्रस्तुत करें, ताकि आपके आवेदन पर आगे की कार्रवाई सुनिश्चित की जा सके। " +
         "- <span style='color:#7F8C8D'>गृह विभाग, बिहार सरकार</span>";
            //string messageTemplate = "आपका दस्तावेज़ सफलतापूर्वक अपलोड हो गया है। कृपया दिनांक {0} (शनिवार) को अपने {1} अंचल कार्यालय में जाकर अपलोड किए गए भूमि संबंधी दस्तावेज़ की छायाप्रति तथा शिकायत पत्र सर्किल अधिकारी एवं थाना प्रभारी के समक्ष प्रस्तुत करें, ताकि आपके आवेदन पर आगे की कार्रवाई सुनिश्चित की जा सके। - गृह विभाग, बिहार सरकार";
            //Label lbl = (Label)e.Item.FindControl("lbldayassign");
            //if (lbl != null)
            //{
            //e.Item.FindControl("div_id_dayassign").Visible = true;
            ((Label)e.Item.FindControl("lblassignday")).Text = string.Format(messageTemplate, appointmentDate, blockName);
            //}
            // Inject values into the template
            //lblassignday.Text = string.Format(messageTemplate, appointmentDate, blockName);
        }
    }
    private void FinalisedBen(string a_id, object sender, RepeaterItemEventArgs e)
    {
        


            // 🔹 Repeater ke controls
            Button btnSave = (Button)e.Item.FindControl("btnSave");
            Button btnEdit = (Button)e.Item.FindControl("btnEdit");
            Button btnPrint = (Button)e.Item.FindControl("btnPrint");

            HtmlGenericControl divInsert =
                (HtmlGenericControl)e.Item.FindControl("div_insert");

            HtmlGenericControl divUpdate =
                (HtmlGenericControl)e.Item.FindControl("div_update");

            HtmlGenericControl divPrint =
                (HtmlGenericControl)e.Item.FindControl("div_print");

           
                // ✔ FINALIZED CASE
                btnSave.Visible = false;
                btnEdit.Visible = false;
                btnPrint.Visible = true;

                divInsert.Visible = false;
                divUpdate.Visible = false;
                divPrint.Visible = true;

            
          
    }

    //private void FinalisedBen(string a_id, object sender, RepeaterItemEventArgs e)
    //{
    //    try
    //    {
    //        SqlParameter _a_id2 = new SqlParameter("@a_id", ViewState["a_id"].ToString());

    //        DataTable dt = clsData.GetDataTableWithProc(
    //                            "usp_Finalised_Data_Public",
    //                            new SqlParameter[] { _a_id2 });

    //        if (dt.Rows.Count > 0)
    //        {
    //            if (dt.Rows[0]["mStatus"].ToString() == "1")
    //            {


    //                //Utility.showMessage(this,"Your application has been saved and finalised.");
    //                Utility.showMessage(this, "Your application has been saved and finalised.");


    //            }
    //            else
    //            {
    //                Utility.showMessage(this, "Some error occurred, your application was not finalised.");
    //                return;
    //            }
    //        }
    //        else
    //        {
    //            Utility.showMessage(this, "Some error occurred, your application was not finalised.");
    //            return;
    //        }
    //    }
    //    catch
    //    {

    //    }

    //}

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        RepeaterItem item = (RepeaterItem)btn.NamingContainer;

        HiddenField lbla_id =
            (HiddenField)item.FindControl("lbla_id");

        Button btnSave = (Button)item.FindControl("btnSave");
        Button btnEdit = (Button)item.FindControl("btnEdit");
        Button btnPrint = (Button)item.FindControl("btnPrint");

        //HtmlGenericControl divInsert =
        //    (HtmlGenericControl)item.FindControl("div_insert");
        //HtmlGenericControl divUpdate =
        //    (HtmlGenericControl)item.FindControl("div_update");
        //HtmlGenericControl divPrint =
        //    (HtmlGenericControl)item.FindControl("div_print");

        SqlParameter _a_id =
            new SqlParameter("@a_id", lbla_id.Value);

        DataTable dt = clsData.GetDataTableWithProc(
                            "usp_Finalised_Data_Public",
                            new SqlParameter[] { _a_id });

        if (dt.Rows.Count > 0 && dt.Rows[0]["mStatus"].ToString() == "1")
        {
            Utility.showMessage(this,
                "Your application has been saved and finalised.");

            // ✅ VISIBILITY LOGIC — NOW WORKS
            btnSave.Visible = false;
            btnEdit.Visible = false;
            btnPrint.Visible = true;

            //divInsert.Visible = false;
            //divUpdate.Visible = false;
            //divPrint.Visible = true;

            //Response.Redirect("PublicInformationEntry.aspx", false);
        }
        else
        {
            Utility.showMessage(this,
                "Some error occurred, your application was not finalised.");
            btnSave.Visible = true;
            btnEdit.Visible = true;
            btnPrint.Visible = false;
        }
    }



    //protected void btnSave_Click(object sender, EventArgs e)
    //{
    //    SqlParameter _a_id2 = new SqlParameter("@a_id", ViewState["a_id"].ToString());
    //    DataTable dt = clsData.GetDataTableWithProc("usp_SaveBhumiKaKhataKhesraKaVivaranPublic", new SqlParameter[] { _a_id2 });
    //    if(dt.Rows.Count > 0)
    //    {
    //        if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
    //        {
    //            Utility.showMessage();

    //        }
    //        else
    //        {
    //            Utility.showMessage();

    //        }

    //    }
    //    else
    //    {
    //        Utility.showMessage();

    //    }



    //}

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //Response.Redirect("PublicInformationEntry.aspx");
        Response.Redirect("Public_Entry_Page.aspx?RegId=" + ViewState["a_id"].ToString(), false);

    }

    private void Wadi_ka_vivran(string a_id, object  sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi");
        GridView gvWadi = (GridView)e.Item.FindControl("gvWadi");
        DataTable dt_wadi = clsData.GetDataTableWithProc("getEntryPage_print_Information_public", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_wadi;
        gvWadi.DataSource = dt_wadi;
        gvWadi.DataBind();
    }
    private void WadikaVivaranDetails(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadi_Vivaran");
        DataTable dt_Wadi_ka_vivran = clsData.GetDataTableWithProc("getEntryPage_print_Information_public", new SqlParameter[] { _a_id, _fn2 });
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
            
            HtmlGenericControl div37 = (HtmlGenericControl)e.Item.FindControl("div37");
            HtmlGenericControl div38 = (HtmlGenericControl)e.Item.FindControl("div38");

            if (dt_Wadi_ka_vivran.Rows[0]["Vadi_sakshya_File"].ToString() != "")
            {
                div37.Visible = true;
                if (BindGetpdf(dt_Wadi_ka_vivran.Rows[0]["Vadi_sakshya_File"].ToString()) != "0")
                {
                    
                    ((ImageButton)e.Item.FindControl("lnkAppDoc")).Attributes.Add("path", dt_Wadi_ka_vivran.Rows[0]["Vadi_sakshya_File"].ToString());
                }
            }
            //if (dt_Wadi_ka_vivran.Rows[0]["FullfileName"].ToString() != "")
            //{
            //    if (BindGetpdf(dt_Wadi_ka_vivran.Rows[0]["FullfileName"].ToString()) != "0")
            //    {
            //        div38.Visible = true;
            //        ((ImageButton)e.Item.FindControl("lnkAppDoc1")).Attributes.Add("path", dt_Wadi_ka_vivran.Rows[0]["Prativadi_sakshya_File"].ToString());
            //    }
            //}
        }
    }

    private void PratiWadi_ka_vivran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getPrativadi");
        GridView gvPratiWadi = (GridView)e.Item.FindControl("pratiWadi_grid");
        DataTable dt_Pratiwadi = clsData.GetDataTableWithProc("getEntryPage_print_Information_public", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_Pratiwadi;
        gvPratiWadi.DataSource = dt_Pratiwadi;
        gvPratiWadi.DataBind();
    }
    private void bhumikhata_Shekher_Ka_Vivaran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetBhumiKaKhataKhesraKaVivaran");
        GridView gvbhumivivad = (GridView)e.Item.FindControl("grd_bhumivivad");
        DataTable dt_bhumikhata_Shekher_Ka_Vivaran = clsData.GetDataTableWithProc("getEntryPage_print_Information_public", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_bhumikhata_Shekher_Ka_Vivaran;
        gvbhumivivad.DataSource = dt_bhumikhata_Shekher_Ka_Vivaran;
        gvbhumivivad.DataBind();
    }

    private void vadiEvidanceKa_Vivaran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "GetVadiEvidance");
        GridView gvvadiEvidance = (GridView)e.Item.FindControl("gdVadiEvidence");
        DataTable dt_vadiEvidance = clsData.GetDataTableWithProc("getEntryPage_print_Information_public", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_vadiEvidance;
        gvvadiEvidance.DataSource = dt_vadiEvidance;
        gvvadiEvidance.DataBind();
    }
    protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewDoc")
        {
            string filePath = e.CommandArgument.ToString();

            if (filePath.StartsWith("~"))
            {
                filePath = filePath.Replace("~", "http://10.133.20.157:85");
            }

            // 🔹 Pehle Getpdf() ko call karke URL encrypt karna
            string encUrl = Getpdf(filePath);

            // 🔹 Ab naye tab me open karna
            string url = "../IDoc.aspx?url=" + Server.UrlEncode(encUrl);
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenDoc", script, true);
        }
    }
    protected void parentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                e.Item.FindControl("div_Vadi_Svarajaya_Label").Visible = true;
                e.Item.FindControl("div_Vadi_Svarajaya").Visible = true;

                HiddenField lbla_id =
                    (HiddenField)e.Item.FindControl("lbla_id");

                Button btnSave = (Button)e.Item.FindControl("btnSave");
                Button btnEdit = (Button)e.Item.FindControl("btnEdit");
                Button btnPrint = (Button)e.Item.FindControl("btnPrint");

                // 🔹 DB se jo already finalised hai wahi dikhega
                //bool isFinalised = Convert.ToBoolean(
                //    DataBinder.Eval(e.Item.DataItem, "IsFinalised"));

                //btnSave.Visible = !isFinalised;
                //btnEdit.Visible = !isFinalised;
                //btnPrint.Visible = isFinalised;

                Wadi_ka_vivran(lbla_id.Value, sender, e);
                WadikaVivaranDetails(lbla_id.Value, sender, e);
                PratiWadi_ka_vivran(lbla_id.Value, sender, e);
                bhumikhata_Shekher_Ka_Vivaran(lbla_id.Value, sender, e);
                vadiEvidanceKa_Vivaran(lbla_id.Value, sender, e);
                LoadDayAssign(lbla_id.Value, sender, e);

                // 🔥 FINAL STATUS CHECK
                //FinalisedBen(lbla_id.Value, sender, e);
            }
        }
        catch (Exception ex)
        {
            // log ex if needed
        }
    }


    //protected void parentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //        {
                
    //            e.Item.FindControl("div_Vadi_Svarajaya_Label").Visible = true;
    //            e.Item.FindControl("div_Vadi_Svarajaya").Visible = true;
    //            HiddenField lbla_id = (HiddenField)e.Item.FindControl("lbla_id");
    //            //वादी का विवरण
    //            Wadi_ka_vivran(lbla_id.Value, sender, e);
    //            //भूमि विवाद का विवरण
    //            WadikaVivaranDetails(lbla_id.Value, sender, e);
    //            //प्रतिवादी का विवरण   
                
    //            PratiWadi_ka_vivran(lbla_id.Value, sender, e);
    //            //भूमि का खाता-खेसरा का विवरण
    //            bhumikhata_Shekher_Ka_Vivaran(lbla_id.Value, sender, e);
    //            //वादी द्वारा प्रस्तुत साक्ष्य का विवरण
    //            vadiEvidanceKa_Vivaran(lbla_id.Value, sender, e);
    //            //प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण

    //            LoadDayAssign(lbla_id.Value, sender, e);
    //            FinalisedBen(lbla_id.Value, sender, e);
                

    //        }
    //    }
    //    catch(Exception ex)
    //    { }
    //}
   
  
   
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
        Response.Redirect("ViewEntryPublic.aspx?A_ID=" + ViewState["A_ID"].ToString(), false);
    }
    [System.Web.Services.WebMethod()]
    public static string Getpdf(string url)
    {
        //string urlpath = url;
        //try
        //{
        //    using (var webClient = new WebClient())
        //    {
        //        var bytes = webClient.DownloadData(urlpath);
        //        string base64String = Convert.ToBase64String(bytes);

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
        Encryptor enc = new Encryptor(Encryptor.PrivateKey);
        string urlpath = "";
        string encPathgov = enc.EncodeTo64(url);
        encPathgov = Aes256CbcEncrypterApp.Encrypt(encPathgov, System.Web.HttpContext.Current.Session["aes256key"].ToString());
        urlpath = encPathgov;

        return urlpath;
    }
    public static string BindGetpdf(string url)
    {
        string p = url.Replace("~", "");
        //url = "http://10.133.20.157:85" + p;
        url = "http://10.133.20.157:85" + p;
        string urlpath = "0";
        try
        {
            using (var webClient = new WebClient())
            {
                var bytes = webClient.DownloadData(url);
                string base64String = Convert.ToBase64String(bytes);

                byte[] imageBytes = webClient.DownloadData(url);
                string imreBase64Data = Convert.ToBase64String(imageBytes);
                string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);
                urlpath = imgDataURL;
            }
        }
        catch (Exception ex)
        {
            urlpath = "0";
        }

        return urlpath;
    }
    //public string BindGetpdf(string url)
    //{
    //    string urlpath = "";
    //    try
    //    {
    //        //Byte[] bytes = File.ReadAllBytes(url);
    //        //String file = Convert.ToBase64String(bytes);

    //        using (var webClient = new WebClient())
    //        {
    //            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
    //            string pathurl = Convert.ToString(Request.QueryString["url"]).Trim();
    //            pathurl = Aes256CbcEncrypterApp.Decrypt(pathurl, System.Web.HttpContext.Current.Session["aes256key"].ToString());
    //            string path = enc.DecodeFrom64(pathurl);
    //            hdnpath.Value = path;
    //            byte[] imageBytes = webClient.DownloadData(path.ToString());
    //            string imreBase64Data = Convert.ToBase64String(imageBytes);


    //            Response.AddHeader("Content-Type", "application/pdf");
    //            Response.AddHeader("Content-Length", imreBase64Data.Length.ToString());
    //            Response.AddHeader("Content-Disposition", "inline;");
    //            Response.AddHeader("Cache-Control", "private, max-age=0, must-revalidate");
    //            Response.AddHeader("Pragma", "public");
    //            Response.BinaryWrite(Convert.FromBase64String(imreBase64Data));

    //        }



    //    }
    //    catch (Exception ex)
    //    {
    //        urlpath = ex.Message;
    //    }
    //    return urlpath;
    //}
}