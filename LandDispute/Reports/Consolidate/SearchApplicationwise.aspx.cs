using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class LandDispute_Reports_Consolidate_SearchApplicationwise : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"] != null && Session["Role"].ToString() != "")
            {
                BindRole();
            }
        }
    }
    private void BindRole()
    {
        bindRande();
        bindCommissionary();
        if (Session["RangeCode"] != null)
        {
            if (Session["RangeCode"].ToString() != "")
            {
                ddlRange.SelectedValue = Session["RangeCode"].ToString();
                ddlRange.Enabled = false;
                
            }
        }
        
        if (Session["Commsionary_Code"] != null)
        {
            if (Session["Commsionary_Code"].ToString() != "")
            {
                ddlCommissionary.SelectedValue = Session["Commsionary_Code"].ToString();
                ddlCommissionary.Enabled = false;
            }
        }
        if(Session["Role"].ToString()=="DIG")
        {
            divddlRange.Visible = true;
            divLabRange.Visible = true;
            divddlCommissionary.Visible = false;
            divLabCommissionary.Visible = false;

        }
        else 
        {
            divddlRange.Visible = false;
            divLabRange.Visible = false;
            divddlCommissionary.Visible = true;
            divLabCommissionary.Visible = true;

        }
        //if(Session["RangeCode"].ToString() != "")
        //{
        //    divddlCommissionary.Visible = false;
        //    divLabCommissionary.Visible = false;

        //    divddlRange.Visible = false;
        //    divLabRange.Visible = false;
        //}
        //else
        //{
        //    ddlCommissionary.Enabled = true;
        //    if (Session["Commsionary_Code"].ToString() != "")
        //    {
        //        ddlCommissionary.SelectedValue = Session["Commsionary_Code"].ToString();
        //        ddlCommissionary.Enabled = false;
        //    }
        //}



        bindDistrict();
        if (Session["District_Code"] != null)
        {
            if (Session["District_Code"].ToString() != "")
            {
                ddlDistrict.SelectedValue = Session["District_Code"].ToString();
                ddlDistrict.Enabled = false;
            }
        }
        bindSubDivision();
        if (Session["Sub_DivCode"] != null)
        {
            if (Session["Sub_DivCode"].ToString() != "")
            {
                ddlSubDivision.SelectedValue = Session["Sub_DivCode"].ToString();
                ddlSubDivision.Enabled = false;
            }
        }
        bindBlock();
        if (Session["Block_Code"] != null)
        {
            if (Session["Block_Code"].ToString() != "")
            {
                ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                ddlBlock.Enabled = false;
            }
        }
        bindthana();
        if (Session["Thana_Code"] != null)
        {
            if (Session["Thana_Code"].ToString() != "")
            {
                ddlThana.SelectedValue = Session["Thana_Code"].ToString();
                ddlThana.Enabled = false;
            }
        }
    }
    private void bindCommissionary()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        DataTable dt = clsData.GetDataTableWithProc("SearchApplicationWise", new SqlParameter[] { QueryType });
        if (dt.Rows.Count > 0)
        {
            ddlCommissionary.DataSource = dt;
            ddlCommissionary.DataTextField = "DIVISIONAME";
            ddlCommissionary.DataValueField = "DIVISIONCODE";
            ddlCommissionary.DataBind();
            ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));

        }
        else
        {
            ddlCommissionary.DataSource = null;
            ddlCommissionary.DataTextField = "DIVISIONAME";
            ddlCommissionary.DataValueField = "DIVISIONCODE";
            ddlCommissionary.DataBind();
            //ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    private void bindRande()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("0"));
        DataTable dt = clsData.GetDataTableWithProc("SearchApplicationWise", new SqlParameter[] { QueryType });
        if (dt.Rows.Count > 0)
        {
            ddlRange.DataSource = dt;
            ddlRange.DataTextField = "RangeName";
            ddlRange.DataValueField = "Rangeid";
            ddlRange.DataBind();
            ddlRange.Items.Insert(0, new ListItem("All", "0"));

        }
        else
        {
            ddlRange.DataSource = null;
            ddlRange.DataTextField = "RangeName";
            ddlRange.DataValueField = "Rangeid";
            ddlRange.DataBind();
            //ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    private void bindDistrict()
    {
        ddlDistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("2"));
        SqlParameter CommissionaryCode = new SqlParameter("@DivisionCode", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString()));
        SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("[SearchApplicationWise_new]", new SqlParameter[] { QueryType, CommissionaryCode, Rangeid });
        if (dt.Rows.Count > 0)
        {
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlDistrict.DataSource = null;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
        }


    }
    private void bindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter District = new SqlParameter("@DISTRICTCODE", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SearchApplicationWise", new SqlParameter[] { QueryType, District });
        if (dt.Rows.Count > 0)
        {
            ddlSubDivision.DataSource = dt;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlSubDivision.DataSource = null;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    private void bindBlock()
    {
        ddlBlock.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter SubDivision = new SqlParameter("@subDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SearchApplicationWise", new SqlParameter[] { QueryType, @SubDivision });
        if (dt.Rows.Count > 0)
        {
            ddlBlock.DataSource = dt;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlBlock.DataSource = null;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("All", "0"));
        }

    }
    private void bindthana()
    {
        ddlThana.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter District = new SqlParameter("@DISTRICTCODE", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        SqlParameter SubDivision = new SqlParameter("@subDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        SqlParameter block = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SearchApplicationWise", new SqlParameter[] { QueryType, District, @SubDivision, block });
        if (dt.Rows.Count > 0)
        {
            ddlThana.DataSource = dt;
            ddlThana.DataTextField = "Police_Station";
            ddlThana.DataValueField = "PS_Code";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlThana.DataSource = null;
            ddlThana.DataTextField = "";
            ddlThana.DataValueField = "";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {

            ExportExcel(GridView1, Pnlsearch);


        }
        catch (Exception)
        {

            throw;
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    protected void ExportExcel(GridView Gv, Panel Pnl)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", DateTime.Now.ToString("ddMMyyhhmmss") + "ApplicationConsolidateRpt.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        for (int i = 0; i < Gv.HeaderRow.Cells.Count; i++)
        {
            Gv.HeaderRow.Cells[i].Style.Add("border-style", "Solid");
            Gv.HeaderRow.Cells[i].Style.Add("border-color", "Black");
            Gv.HeaderRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            Gv.HeaderRow.Cells[i].Style.Add("Font-Bold", "True");
            Gv.HeaderRow.Cells[i].Style.Add("Fore-Color", "Black");

            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
        }
        for (int i = 0; i < Gv.FooterRow.Cells.Count; i++)
        {
            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
            Gv.FooterRow.Cells[i].Style.Add("border-style", "Solid");
            Gv.FooterRow.Cells[i].Style.Add("border-color", "Black");
            Gv.FooterRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            Gv.FooterRow.Cells[i].Style.Add("Font-Bold", "True");
            Gv.FooterRow.Cells[i].Style.Add("Fore-Color", "Black");
        }
        int j = 1;
        //   This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in Gv.Rows)
        {
            gvrow.BackColor = System.Drawing.Color.White;
            if (j <= Gv.Rows.Count)
            {
                //if (j % 1 != 0)
                //{
                for (int k = 0; k < gvrow.Cells.Count; k++)
                {
                    gvrow.Cells[k].Style.Add("border-style", "Solid");
                    gvrow.Cells[k].Style.Add("border-color", "Black");
                    gvrow.Cells[k].ForeColor = System.Drawing.Color.Black;
                }
                // }
            }
            j++;
        }
        string style = @"<style> TD { mso-number-format:\@; } </style>";
        Response.Write(style);
        Pnl.RenderControl(htw);
        string dd = sw.ToString();
        dd = dd.Replace("href=", "");
        string Headrer = lbltext.Text;
        Response.Write("<h3><center>" + Headrer + "</center></h3>");
        Response.Write(dd);
        Response.End();
    }

    protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDistrict();
        bindSubDivision();
        bindBlock();
        bindthana();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubDivision();
        bindBlock();
        bindthana();
    }
    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindBlock();
        bindthana();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "1";
        bindDivision();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindthana();
    }

    void bindDivision()
    {
        try
        {
            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("6"));
            SqlParameter GetComm_Code = new SqlParameter("@DIVISIONCODE", Convert.ToInt32(ddlCommissionary.SelectedValue.Trim()));
            SqlParameter GetRange_Code = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.Trim()));

            SqlParameter GetDistrict_Code = new SqlParameter("@DISTRICTCODE", Convert.ToInt32(ddlDistrict.SelectedValue.Trim()));
            SqlParameter GetSub_DivCode = new SqlParameter("@subDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.Trim()));
            SqlParameter GetBlock_Code = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.Trim()));
            SqlParameter GetThana_Code = new SqlParameter("@ThanaCode", Convert.ToInt32(ddlThana.SelectedValue.Trim()));

            SqlParameter GetApplicationNo = new SqlParameter("@ApplicationNo", ddlSearchby.SelectedValue == "1" ? txtsearch.Text.ToString().Trim() : "");
            SqlParameter Getvadi_Name = new SqlParameter("@vadi_Name", ddlSearchby.SelectedValue=="2" ? txtsearch.Text.ToString().Trim():"");        
            SqlParameter GetpratiVadi_Name = new SqlParameter("@pratiVadi_Name", ddlSearchby.SelectedValue == "3" ? txtsearch.Text.ToString().Trim():"");
            SqlParameter GetVadi_MobileNo = new SqlParameter("@Vadi_MobileNo ", ddlSearchby.SelectedValue == "4" ? txtsearch.Text.ToString().Trim() : "");
            SqlParameter GetpratiVadi_MobileNo = new SqlParameter("@pratiVadi_MobileNo", ddlSearchby.SelectedValue == "5" ? txtsearch.Text.ToString().Trim():"");


            SqlParameter getfromdate = new SqlParameter("@fromdate", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter gettodate = new SqlParameter("@ToDate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));           
            SqlParameter _PageSize = new SqlParameter("@PageSize", ddlPageSize.SelectedValue);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));                 
            SqlParameter _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            DataTable dt = clsData.GetDataTableWithProc("SearchApplicationWise_new", new SqlParameter[] { QueryType, GetComm_Code,GetRange_Code, GetDistrict_Code, GetSub_DivCode, GetBlock_Code,
                GetThana_Code,GetApplicationNo,Getvadi_Name,GetpratiVadi_Name,GetVadi_MobileNo,GetpratiVadi_MobileNo,
                getfromdate,gettodate,_PageSize,_PageIndex,_RecordCount });

            int totalRecord = 0;
            if (_RecordCount.Value != null)
            {
                int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();

            int recordCount = Convert.ToInt32(_RecordCount.Value);
            this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), ddlPageSize.SelectedValue);
        }
        catch (Exception ex)
        {

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

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            LinkButton linkbtn = sender as LinkButton;
            string UrlRedirect = enc.Encrypt(linkbtn.CommandArgument);
            string strFilePath = "Information.aspx?RegId=" + UrlRedirect;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + strFilePath + "','_blank')", true);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
        setBackColorOfLinkButton();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Get the link button of each row
            //LinkButton lnkbutton = (LinkButton)e.Row.FindControl("lb2");
            ////Attach javascript function to each linkbutton
            //lnkbutton.Attributes.Add("onclick", "return fnLinkbutton('" + lnkbutton.ClientID + "')");
            //LinkButton lnkbutton3 = (LinkButton)e.Row.FindControl("lb3");

            //lnkbutton3.Attributes.Add("onclick", "return fnLinkbutton1('" + lnkbutton3.ClientID + "')");
            //LinkButton lnkbutton4 = (LinkButton)e.Row.FindControl("lb4");


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


    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["PageIndex"] = pageIndex;
        bindDivision();
    }

    protected void setBackColorOfLinkButton()
    {
        foreach (RepeaterItem item in rptPager.Items)
        {

            LinkButton lnkButton = (LinkButton)item.FindControl("lnkPage");
            string value = lnkButton.Text;
            if (lnkButton.Enabled == false)
            {
                lnkButton.BackColor = System.Drawing.Color.FromName("#1eb089");
            }
            else
            {
                lnkButton.BackColor = System.Drawing.Color.FromName("#afa8a8ed");
            }

        }
    }

    private void PopulatePager(int recordCount, int currentPage, string pagesize)
    {
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(pagesize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            int showMax = 10;
            int startPage;
            int endPage;
            if (((pageCount - currentPage) < 10) || ((pageCount - currentPage) == 0))
            {
                if (pageCount <= 9)
                {
                    startPage = 1;
                    pages.Add(new ListItem("First", "1", currentPage > 1));
                    int i = 0;
                    for (i = startPage; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
                }
                else
                {
                    startPage = pageCount - 10 + 1;
                    pages.Add(new ListItem("First", "1", currentPage > 1));
                    int i = 0;
                    for (i = startPage; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("Last", pageCount.ToString(), (i - 1) != currentPage));
                }
            }
            else if (pageCount - currentPage >= 10)
            {
                startPage = currentPage;
                endPage = currentPage + showMax - 1;
                pages.Add(new ListItem("First", "1", currentPage > 1));

                for (int i = startPage; i <= endPage; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
        setBackColorOfLinkButton();
    }

    protected void ddlSearchby_SelectedIndexChanged(object sender, EventArgs e)
    {
        divlblsearchType.Visible = true;
        divtxtsearchType.Visible = true;
        if (ddlSearchby.SelectedValue=="1")
        {
            lblsearchType.Text = "Application No";
            txtsearch.Text = "";
        }
        else if (ddlSearchby.SelectedValue == "2")
        {
            lblsearchType.Text = "Vadi Name";
            txtsearch.Text = "";
        }
        else if (ddlSearchby.SelectedValue == "3")
        {
            lblsearchType.Text = "Prativadi Name";
            txtsearch.Text = "";
        }
        else if (ddlSearchby.SelectedValue == "4")
        {
            lblsearchType.Text = "Mobile Number";
        }
        else if (ddlSearchby.SelectedValue == "5")
        {
            lblsearchType.Text = "Mobile Number";
            txtsearch.Text = "";
        }
        else
        {
            divlblsearchType.Visible = false;
            divtxtsearchType.Visible = false;
            txtsearch.Text = "";
        }

    }


    protected void ddlRange_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}