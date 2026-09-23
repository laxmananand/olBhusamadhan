using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class LandDispute_Entry_SearchAppForMetting : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login_Default.aspx");
        }

        if (!IsPostBack)
        {
           
            ViewState["QueryType"] = "";
            ViewState["PageIndex"] = "1";
            ViewState["PageSize"] = ddlPageSize.SelectedValue;//Request.QueryString["PageSize"].ToString();
            ViewState["checktype"] = Request.QueryString["checkType"].ToString();
            ViewState["District"] = Request.QueryString["District"].ToString();
            ViewState["Block"] = Request.QueryString["Block"].ToString();
            ViewState["Panchayat"] = Request.QueryString["Panchayat"].ToString();
            ViewState["PoliceSation"] = Request.QueryString["PoliceSation"].ToString();
            ViewState["Sensivity"] = Request.QueryString["Sensivity"].ToString();
            ViewState["action"] = Request.QueryString["action"].ToString();
            ViewState["datefrom"] = Request.QueryString["datefrom"].ToString();
            ViewState["DateTo"] = Request.QueryString["DateTo"].ToString();
            ViewState["datavivadtype"] = Request.QueryString["datavivadtype"].ToString();
            ViewState["datatype"] = Request.QueryString["datatype"].ToString();
            string textval = Request.QueryString["textval"].ToString();
            if (ViewState["datatype"] .ToString()== "BhumiVivadType")
            {
                lblDetailtype.Text = "भूमि विवाद का प्रकार (" + textval.Replace(":","")+")";
                ViewState["QueryType"] = "1";
            }
            else if (ViewState["datatype"].ToString() == "Jameen_Ki_Qism_Ka_Vivaran")
            {
                lblDetailtype.Text = "जमीन की क़िस्म का विवरण (" + textval.Replace(":", "") + ")";
                ViewState["QueryType"] = "2";
            }
            else if (ViewState["datatype"].ToString() == "Saakshy_Ka_Prakaar_Vadi")
            {
                lblDetailtype.Text = "साक्ष्य का प्रकार  (वादी " + textval.Replace(":", "") + ")";
                ViewState["QueryType"] = "3";
            }
            else if (ViewState["datatype"].ToString() == "Saakshy_Ka_Prakaar_Prativadi")
            {
                lblDetailtype.Text = "साक्ष्य का प्रकार  (प्रतिवादी " + textval.Replace(":", "") + ")";
                ViewState["QueryType"] = "4";
            }
            else if (ViewState["datatype"].ToString() == "Vaaradaat_Ka_Vivaran")
            {
                lblDetailtype.Text = "वारदात का विवरण (" + textval.Replace(":", "") + ")";
                ViewState["QueryType"] = "5";
            }
            else if (ViewState["datatype"].ToString() == "Nyaayaalay_Ka_Vivaran")
            {
                lblDetailtype.Text = "न्यायालय का विवरण (" + textval.Replace(":", "") + ")";
                ViewState["QueryType"] = "6";
            }
            else if(ViewState["datatype"].ToString() == "BhumiVivad")
            {
                ViewState["QueryType"] = "7";
                if (ViewState["datavivadtype"] .ToString()== "1")
                {
                    lblDetailtype.Text = "कुल";
                    ViewState["checktype"] = "and m.a_id is not null";
                }
               else if (ViewState["datavivadtype"].ToString() == "2")
                {
                    lblDetailtype.Text = "अंतिम रूप";
                    ViewState["checktype"] = "and isnull( m.Final,1)=1";
                }
                else if (ViewState["datavivadtype"].ToString() == "3")
                {
                    lblDetailtype.Text = "अधूरा";
                    ViewState["checktype"] = "and isnull( m.Final,1)=0";
                }
                else if (ViewState["datavivadtype"].ToString() == "4")
                {
                    lblDetailtype.Text = "प्रारंभिक निष्पादन";
                    ViewState["checktype"] = "and  e.Matter_Status = 1";
                }
                else if (ViewState["datavivadtype"].ToString() == "5")
                {
                    lblDetailtype.Text = "अंतिम निष्पादन";
                    ViewState["checktype"] = "and  e.Matter_Status = 5";
                }
                else if (ViewState["datavivadtype"].ToString() == "6")
                {
                    lblDetailtype.Text = "प्रक्रियाधीन";
                    ViewState["checktype"] = "and  e.Matter_Status = 3";
                }
                else if (ViewState["datavivadtype"].ToString() == "7")
                {
                    lblDetailtype.Text = "मापी के लिए निर्धारित";
                    ViewState["checktype"] = "and  e.Matter_Status = 2";
                }
                else if (ViewState["datavivadtype"].ToString() == "8")
                {
                    lblDetailtype.Text = "अस्वीकृत";
                    ViewState["checktype"] = "and  e.Matter_Status = 4";
                }
                else if (ViewState["datavivadtype"].ToString() == "9")
                {
                    lblDetailtype.Text = "सामान्य";
                    ViewState["checktype"] = "and (case when e.Bhumi_savedansheelta is not null then e.Bhumi_savedansheelta when e.Bhumi_savedansheelta is  null then  m.Bhumi_savedansheelta end)=1";
                }
                else if (ViewState["datavivadtype"].ToString() == "10")
                {
                    lblDetailtype.Text = "संवेदनशील";
                    ViewState["checktype"] = "and (case when e.Bhumi_savedansheelta is not null then e.Bhumi_savedansheelta when e.Bhumi_savedansheelta is  null then  m.Bhumi_savedansheelta end)=2";
                }
                else if (ViewState["datavivadtype"].ToString() == "11")
                {
                    lblDetailtype.Text = "अतिसंवेदनशील";
                    ViewState["checktype"] = "and (case when e.Bhumi_savedansheelta is not null then e.Bhumi_savedansheelta when e.Bhumi_savedansheelta is  null then  m.Bhumi_savedansheelta end)=3"; 
                }
                
            }
            bindGridData(ViewState["QueryType"].ToString(), ViewState["checktype"].ToString(), ViewState["PageSize"].ToString()
                , ViewState["PageIndex"].ToString(), ViewState["District"].ToString(), ViewState["Block"].ToString()
                , ViewState["Panchayat"].ToString(), ViewState["PoliceSation"].ToString(), ViewState["Sensivity"].ToString()
                , ViewState["action"].ToString(), ViewState["datefrom"].ToString(), ViewState["DateTo"].ToString()
                , ViewState["datavivadtype"].ToString());
        }
    }

       private void bindGridData(string QueryType,string checktype,string PageSize,string PageIndex,
         string District, string Block, string Panchayat, string PoliceSation, 
        string Sensivity, string action, string datefrom, string DateTo, string datavivadtype)
    {

        try
        {
            
            SqlParameter _QueryType = new SqlParameter("@QueryType", Convert.ToInt32(QueryType));
            SqlParameter _checktype = new SqlParameter("@checktype", checktype);
            SqlParameter _PageSize = new SqlParameter("@PageSize", PageSize);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", PageIndex);
            SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", "0");
            SqlParameter _District = new SqlParameter("@District", District);
            SqlParameter _BlockCode = new SqlParameter("@BlockCode", Block);
            SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", Panchayat);
            SqlParameter _thana_code = new SqlParameter("@thana_code", PoliceSation);
            SqlParameter _savedansheelta = new SqlParameter("@savedansheelta", Sensivity);
            SqlParameter _Matter_Status = new SqlParameter("@Matter_Status", action);

            SqlParameter _FromDate = new SqlParameter("@FromDate", datefrom);
            SqlParameter _ToDate = new SqlParameter("@ToDate", DateTo);
            SqlParameter _datavivadtype = new SqlParameter("@datavivadtype", datavivadtype);

            SqlParameter _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;

            DataTable dt = clsData.GetDataTableWithProc("usp_getdata", new SqlParameter[] { _QueryType,
                _checktype,_PageSize,_PageIndex,CommissionaryCode, _District, _BlockCode,
                _PanchayatCode, _thana_code, _savedansheelta, _Matter_Status,
                _FromDate, _ToDate, _datavivadtype,_RecordCount });
            int totalRecord = 0;
            if (_RecordCount.Value != null)
            {
                int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();

            int recordCount = Convert.ToInt32(_RecordCount.Value);
            this.PopulatePager(recordCount, Convert.ToInt32(PageIndex),PageSize);
        }
        catch (Exception ex)
        {

        }
    }

    private void PopulatePager(int recordCount, int currentPage, string pagesize)
    {
        
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(pagesize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", currentPage > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
        setBackColorOfLinkButton();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["PageIndex"] = pageIndex;
        this.bindGridData(ViewState["QueryType"].ToString(), ViewState["checktype"].ToString(), ViewState["PageSize"].ToString()
                , pageIndex.ToString(), ViewState["District"].ToString(), ViewState["Block"].ToString()
                , ViewState["Panchayat"].ToString(), ViewState["PoliceSation"].ToString(), ViewState["Sensivity"].ToString()
                , ViewState["action"].ToString(), ViewState["datefrom"].ToString(), ViewState["DateTo"].ToString()
                , ViewState["datavivadtype"].ToString());

    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            LinkButton linkbtn = sender as LinkButton;
            string UrlRedirect = enc.Encrypt(linkbtn.CommandArgument);
            //Server.Transfer("Information.aspx?RegId=" + UrlRedirect);
           Response.Redirect("Information.aspx?RegId=" + UrlRedirect,false);           
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    public bool CheckNull(object myValue)
    {
        if (myValue == null || myValue.ToString() == "")
        {
            return false;
        }

        if (myValue is DBNull)
        {
            return false;
        }

        return true;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            


            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");


            ImageButton image2 = (ImageButton)e.Row.FindControl("Image2");
            image2.Attributes.Add("onclick", "return fnLinkbutton1('" + image2.ClientID + "')");



            ImageButton image3 = (ImageButton)e.Row.FindControl("Image3");
            image3.Attributes.Add("onclick", "return fnLinkbutton1('" + image3.ClientID + "')");


            ImageButton image4 = (ImageButton)e.Row.FindControl("Image4");
            image4.Attributes.Add("onclick", "return fnLinkbutton1('" + image4.ClientID + "')");


            ImageButton image5 = (ImageButton)e.Row.FindControl("Image5");
            image5.Attributes.Add("onclick", "return fnLinkbutton1('" + image5.ClientID + "')");


            ImageButton image6 = (ImageButton)e.Row.FindControl("Image6");
            image6.Attributes.Add("onclick", "return fnLinkbutton1('" + image6.ClientID + "')");


        }
    }

    protected void setBackColorOfLinkButton()
    {
        foreach (RepeaterItem item in rptPager.Items)
        {
           
            LinkButton lnkButton = (LinkButton)item.FindControl("lnkPage");
            if(lnkButton.Enabled==false)
              {
                lnkButton.BackColor = System.Drawing.Color.FromName("#1eb089");
              }
            else
            {
                lnkButton.BackColor = System.Drawing.Color.FromName("#afa8a8ed");
            }
            
        }
    }
    
    [System.Web.Services.WebMethod()]
    public static string Getpdf(string url)
    {
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
        Encryptor enc = new Encryptor(Encryptor.PrivateKey);
        string urlpath = "";
        string encPathgov = enc.EncodeTo64(url);
        encPathgov = Aes256CbcEncrypterApp.Encrypt(encPathgov, System.Web.HttpContext.Current.Session["aes256key"].ToString());
        urlpath = encPathgov;
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
            //return true;
        }


        else
        {
            return false;
        }


    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        ViewState["PageSize"] = ddlPageSize.SelectedValue;
        ViewState["PageIndex"] = 1;
        bindGridData(ViewState["QueryType"].ToString(), ViewState["checktype"].ToString(), ddlPageSize.SelectedValue
                , ViewState["PageIndex"].ToString(), ViewState["District"].ToString(), ViewState["Block"].ToString()
                , ViewState["Panchayat"].ToString(), ViewState["PoliceSation"].ToString(), ViewState["Sensivity"].ToString()
                , ViewState["action"].ToString(), ViewState["datefrom"].ToString(), ViewState["DateTo"].ToString()
                , ViewState["datavivadtype"].ToString());
    }
}