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


public partial class LandDispute_Dashboard_View: System.Web.UI.Page
{
    DataSet ds;
    DataTable dtRepeaterTable;
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    PagedDataSource pageDataSource = new PagedDataSource();


    protected void Page_Load(object sender, EventArgs e)
    {
        string A_ID = Request.QueryString["A_ID"].ToString();
        if (!IsPostBack)
        {
            ViewState["A_ID"] = A_ID;
            A_ID = A_ID.Replace(' ', '+');
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            string a_id = enc.Decrypt(A_ID);
            LoadAllApplication(a_id);
        }
    }
    public int CurrentPage
    {
        get
        {
            // look for current page in ViewState  
            object o = this.ViewState["_CurrentPage"];
            if (o == null)
                return 0; // default to showing the first page  
            else
                return (int)o;
        }
        set
        {
            this.ViewState["_CurrentPage"] = value;
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
        DataTable dt = clsData.GetDataTableWithProc("getEntryPage_print_Information", new SqlParameter[] { _fn2, _a_id });
        parentRepeater.DataSource = dt;
        parentRepeater.DataBind();
    }
    private void Wadi_ka_vivran(string a_id, object sender, RepeaterItemEventArgs e)
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
           // ((Label)e.Item.FindControl("lblPrativadiKabhumiVivaran")).Text = ": " + dt_Wadi_ka_vivran.Rows[0]["PrativadiVivarani"].ToString();
        }
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

    protected void parentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
            //भूमि का खाता-खेसरा का विवरण
            bhumikhata_Shekher_Ka_Vivaran(lbla_id.Value, sender, e);
            //वादी द्वारा प्रस्तुत साक्ष्य का विवरण
            vadiEvidanceKa_Vivaran(lbla_id.Value, sender, e);
            //प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण
            LoadDayAssign(lbla_id.Value, sender, e);
        }
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
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=TestPage.pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        this.Page.RenderControl(hw);
        StringReader sr = new StringReader(sw.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.Write(pdfDoc);
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}