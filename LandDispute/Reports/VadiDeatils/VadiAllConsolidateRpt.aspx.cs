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


public partial class LandDispute_Reports_VadiDeatils_VadiAllConsolidateRpt : System.Web.UI.Page
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
        bindRange();
        bindCommissionary();
        if (Session["RangeCode"] != null)
        {
            if (Session["RangeCode"].ToString() != "")
            {
                ddlRange.SelectedValue = Session["RangeCode"].ToString();
                ddlRange.Enabled = false;

            }
        }
        if (Session["Role"].ToString() == "DIG")
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
        if (Session["Commsionary_Code"] != null)
        {
            if (Session["Commsionary_Code"].ToString() != "")
            {
                ddlCommissionary.SelectedValue = Session["Commsionary_Code"].ToString();
                ddlCommissionary.Enabled = false;
            }
        }
        bindDistrict();
        if (Session["District_Code"] != null)
        {
            if (Session["District_Code"].ToString() != "")
            {
                ddldistrict.SelectedValue = Session["District_Code"].ToString();
                ddldistrict.Enabled = false;
            }
        }
        bindSubDivision();
        if (Session["Sub_DivCode"] != null)
        {
            if (Session["Sub_DivCode"].ToString() != "")
            {
                ddlsubdivision.SelectedValue = Session["Sub_DivCode"].ToString();
                ddlsubdivision.Enabled = false;
            }
        }
        bindCircle();
        if (Session["Block_Code"] != null)
        {
            if (Session["Block_Code"].ToString() != "")
            {
                ddlcircle.SelectedValue = Session["Block_Code"].ToString();
                ddlcircle.Enabled = false;
            }
        }
        bindThana();
        if (Session["Thana_Code"] != null)
        {
            if (Session["Thana_Code"].ToString() != "")
            {
                ddlthana.SelectedValue = Session["Thana_Code"].ToString();
                ddlthana.Enabled = false;
            }
        }
    }

    protected void bindCommissionary()
    {
        ddlCommissionary.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        DataTable dt = clsData.GetDataTableWithProc("sp_VadiAllConsolidateReport", new SqlParameter[] { QueryType });
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
            ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    private void bindRange()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("0"));
        DataTable dt = clsData.GetDataTableWithProc("sp_VadiAllConsolidateReport", new SqlParameter[] { QueryType });
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
    protected void bindDistrict()
    {
        ddldistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("2"));
        SqlParameter _CommissionaryCode = new SqlParameter("@DivisionCode", ddlCommissionary.SelectedValue);
        SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("sp_VadiAllConsolidateReport", new SqlParameter[] { QueryType, _CommissionaryCode, Rangeid });
        if (dt.Rows.Count > 0)
        {
            ddldistrict.DataSource = dt;
            ddldistrict.DataTextField = "DISTRICTNAME";
            ddldistrict.DataValueField = "DISTRICTCODE";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddldistrict.DataSource = null;
            ddldistrict.DataTextField = "DISTRICTNAME";
            ddldistrict.DataValueField = "DISTRICTCODE";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void bindSubDivision()
    {
        ddlsubdivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter _DistCode = new SqlParameter("@DISTRICTCODE", ddldistrict.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("sp_VadiAllConsolidateReport", new SqlParameter[] { QueryType, _DistCode });
        if (dt.Rows.Count > 0)
        {
            ddlsubdivision.DataSource = dt;
            ddlsubdivision.DataTextField = "Sd_Name_En";
            ddlsubdivision.DataValueField = "Sd_Code2";
            ddlsubdivision.DataBind();
            ddlsubdivision.Items.Insert(0, new ListItem("All", "0"));

        }
        else
        {
            ddlsubdivision.DataSource = null;
            ddlsubdivision.DataTextField = "Sd_Name_En";
            ddlsubdivision.DataValueField = "Sd_Code2";
            ddlsubdivision.DataBind();
            ddlsubdivision.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void bindCircle()
    {
        ddlcircle.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter _SubDivision = new SqlParameter("@SubDivisionCode", ddlsubdivision.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("sp_VadiAllConsolidateReport", new SqlParameter[] { QueryType, _SubDivision });
        if (dt.Rows.Count > 0)
        {
            ddlcircle.DataSource = dt;
            ddlcircle.DataTextField = "BlockName";
            ddlcircle.DataValueField = "BlockCode";
            ddlcircle.DataBind();
            ddlcircle.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlcircle.DataSource = null;
            ddlcircle.DataTextField = "BlockName";
            ddlcircle.DataValueField = "BlockCode";
            ddlcircle.DataBind();
            ddlcircle.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void bindThana()
    {
        ddlthana.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter _Blockcode = new SqlParameter("@BlockCode", ddlcircle.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("sp_VadiAllConsolidateReport", new SqlParameter[] { QueryType, _Blockcode });
        if (dt.Rows.Count > 0)
        {
            ddlthana.DataSource = dt;
            ddlthana.DataTextField = "Police_Station";
            ddlthana.DataValueField = "PS_Code";
            ddlthana.DataBind();
            ddlthana.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlthana.DataSource = null;
            ddlthana.DataTextField = "Police_Station";
            ddlthana.DataValueField = "PS_Code";
            ddlthana.DataBind();
            ddlthana.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDistrict();
        bindSubDivision();
        bindCircle();
        bindThana();

    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubDivision();
        bindCircle();
        bindThana();

    }

    protected void ddlsubdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindCircle();
        bindThana();

    }

    protected void ddlcircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindThana();
    }

    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {

            ExportExcel(grvAllVadiConsolidate, pnlgrid);
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

    protected void bindAllVadiConsolidate_Grid()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("6"));
        SqlParameter _CommissionaryCode = new SqlParameter("@DivisionCode", ddlCommissionary.SelectedValue == "0" ? "0" : ddlCommissionary.SelectedValue);
        SqlParameter GetRange_Code = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.Trim()));
        SqlParameter _DistCode = new SqlParameter("@DISTRICTCODE", ddldistrict.SelectedValue == "0" ? "0" : ddldistrict.SelectedValue);
        SqlParameter _SubDivision = new SqlParameter("@SubDivisionCode", ddlsubdivision.SelectedValue == "0" ? "0" : ddlsubdivision.SelectedValue);
        SqlParameter _Blockcode = new SqlParameter("@BlockCode", ddlcircle.SelectedValue == "0" ? "0" : ddlcircle.SelectedValue);
        SqlParameter _pscode = new SqlParameter("@ThanaCode", ddlthana.SelectedValue == "0" ? "0" : ddlthana.SelectedValue);
        SqlParameter _fromdate = new SqlParameter("@fromDate", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter _ToDate = new SqlParameter("@ToDate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetEntry_Mode = new SqlParameter("@EntryMode", ddlentrymode.SelectedValue.Trim());
        DataTable dt = clsData.GetDataTableWithProc("sp_VadiAllConsolidateReport", new SqlParameter[] { QueryType, _CommissionaryCode, _DistCode, _SubDivision, _Blockcode, _pscode, _fromdate, _ToDate, GetEntry_Mode, GetRange_Code });
        if (dt.Rows.Count > 0)
        {
            grvAllVadiConsolidate.Columns[5].FooterText = "Total :";
            grvAllVadiConsolidate.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("total")).Sum().ToString();
            grvAllVadiConsolidate.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("General_Vadi")).Sum().ToString();
            grvAllVadiConsolidate.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Vibhag_Vadi")).Sum().ToString();
            grvAllVadiConsolidate.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("institute")).Sum().ToString();

            grvAllVadiConsolidate.DataSource = dt;
            grvAllVadiConsolidate.DataBind();
        }
        else
        {
            grvAllVadiConsolidate.Columns[5].FooterText = "Total :";
            grvAllVadiConsolidate.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("total")).Sum().ToString();
            grvAllVadiConsolidate.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("General_Vadi")).Sum().ToString();
            grvAllVadiConsolidate.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Vibhag_Vadi")).Sum().ToString();
            grvAllVadiConsolidate.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("institute")).Sum().ToString();

            grvAllVadiConsolidate.DataSource = null;
            grvAllVadiConsolidate.DataBind();
        }
        Pnlsearch.Visible = false;
        GridView1.Visible = false;
    }
    private void getPanchayatWiseData(string thanacode,string gettype)   
    {
        try
        {
            Session["mySearchAppData03"] = null;
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
            SqlParameter GetThana_code = new SqlParameter("@ThanaCode", Convert.ToInt32(thanacode.Trim()));
            SqlParameter GetBlock_code = new SqlParameter("@BlockCode ", Session["Block_Code"]);
            SqlParameter GetType = new SqlParameter("@GetType", gettype.Trim());
            SqlParameter GetEntry_Mode = new SqlParameter("@EntryMode", ddlentrymode.SelectedValue.Trim());

            DataTable dt = clsData.GetDataTableWithProc("sp_VadiAllConsolidateReport", new SqlParameter[] { GetQueryType, GetThana_code, GetBlock_code, GetType, GetEntry_Mode });
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

                Session["mySearchAppData03"] = dt;
                GridView1.PageIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Session["mySearchAppData03"] = null;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

            pnlgrid.Visible = false;
            grvAllVadiConsolidate.Visible = false;

            Pnlsearch.Visible = true;
            GridView1.Visible = true;
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindAllVadiConsolidate_Grid();
    }

    protected void grvAllVadiConsolidate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName== "General_VadiClick")
        {
            string thanaCode = e.CommandArgument.ToString().Trim().Split(',')[0];
            string TotalGeneral_Vadi = e.CommandArgument.ToString().Trim().Split(',')[1];
            if(Convert.ToInt32(TotalGeneral_Vadi)>0)
            {
                getPanchayatWiseData(thanaCode, "General_Vadi");
                div_btnback.Visible = true;
            }
        }
        else if (e.CommandName == "Vibhag_VadiClick")
        {
            string thanaCode = e.CommandArgument.ToString().Trim().Split(',')[0];
            string TotalVibhag_Vadi = e.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(TotalVibhag_Vadi) > 0)
            {
                getPanchayatWiseData(thanaCode, "Vibhag_Vadi");
                div_btnback.Visible = true;
            }
        }
        else if (e.CommandName == "instituteClick")
        {
            string thanaCode = e.CommandArgument.ToString().Trim().Split(',')[0];
            string Totalinstitute = e.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Totalinstitute) > 0)
            {
                getPanchayatWiseData(thanaCode, "institute");
                div_btnback.Visible = true;
            }
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)Session["mySearchAppData03"];
        }
        catch (Exception ex) { return; }

        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        if(Pnlsearch.Visible==true && GridView1.Visible==true)
        {
            div_btnback.Visible = false;
            pnlgrid.Visible = true;
            grvAllVadiConsolidate.Visible = true;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
    }
}