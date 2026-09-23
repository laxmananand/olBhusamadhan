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

public partial class LandDispute_Reports_Crime_All_IncidentConsolidateReport : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"] != null && Session["Role"].ToString() != "")
            {
                BindRole();
                btnBack_div.Visible = false;
                Pnlsearch.Visible = false;
                pnlgrid.Visible = true;
                //bindIncident_Grid();
            }
        }
    }

    private void BindRole()
    {
        bindRange();
        bindCommissionary();
        if (Session["Commsionary_Code"] != null)
        {
            if (Session["Commsionary_Code"].ToString() != "")
            {
                ddlCommissionary.SelectedValue = Session["Commsionary_Code"].ToString();
                ddlCommissionary.Enabled = false;
            }
        }
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
    private void bindRange()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("0"));
        DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType });
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
    protected void bindCommissionary()
    {
        ddlCommissionary.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType });
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

    protected void bindDistrict()
    {
        ddldistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("2"));
        SqlParameter _CommissionaryCode = new SqlParameter("@DivisionCode", ddlCommissionary.SelectedValue);
        SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _CommissionaryCode , Rangeid });
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
        DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _DistCode });
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
        SqlParameter _SubDivision = new SqlParameter("@subDivision", ddlsubdivision.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _SubDivision });
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
        DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _Blockcode });
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

    protected void bindIncident_Grid()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("6"));
        SqlParameter _CommissionaryCode = new SqlParameter("@DivisionCode", ddlCommissionary.SelectedValue == "0" ? "0" : ddlCommissionary.SelectedValue);
        SqlParameter GetRange_Code = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.Trim()));


        SqlParameter _DistCode = new SqlParameter("@DISTRICTCODE", ddldistrict.SelectedValue == "0" ? "0" : ddldistrict.SelectedValue);
        SqlParameter _SubDivision = new SqlParameter("@subDivision", ddlsubdivision.SelectedValue == "0" ? "0" : ddlsubdivision.SelectedValue);
        SqlParameter _Blockcode = new SqlParameter("@BlockCode", ddlcircle.SelectedValue == "0" ? "0" : ddlcircle.SelectedValue);
        SqlParameter _pscode = new SqlParameter("@ThanaCode", ddlthana.SelectedValue == "0" ? "0" : ddlthana.SelectedValue);
        SqlParameter _fromdate = new SqlParameter("@fromdate", txtfrmdate.Text=="" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter _ToDate = new SqlParameter("@ToDate", txtTodate.Text=="" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
        DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _CommissionaryCode, _DistCode, _SubDivision, _Blockcode, _pscode, _fromdate, _ToDate, GetRange_Code });
        if (dt.Rows.Count > 0)
        {
            grvAllIncident.Columns[5].FooterText = "Total :";
            grvAllIncident.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("praathamikee")).Sum().ToString();
            grvAllIncident.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("apraathamikee")).Sum().ToString();
            grvAllIncident.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("snaha")).Sum().ToString();        
            grvAllIncident.DataSource = dt;
            grvAllIncident.DataBind();
        }
        else
        {
            grvAllIncident.DataSource = null;
            grvAllIncident.DataBind();
        }

    }

    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {

            ExportExcel(grvAllIncident, pnlgrid);


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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindIncident_Grid();
        btnBack_div.Visible = false;
        Pnlsearch.Visible = false;
        pnlgrid.Visible = true;
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

    protected void lnkpraathamikee_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string Districtcode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string SubDivisionCode = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            string Blockcode = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[3];
           
            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("7"));
            SqlParameter _Type = new SqlParameter("@Type", "praathamikee");
            SqlParameter _Districtcode = new SqlParameter("@DISTRICTCODE", Districtcode);
            SqlParameter _SubDivisionCode = new SqlParameter("@subDivision", SubDivisionCode);
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", Blockcode);
            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", ThanaCode);                    
            DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _Type, _Districtcode, _SubDivisionCode, _Blockcode, _ThanaCode});

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
            Pnlsearch.Visible = true;      
            pnlgrid.Visible = false;
            btnBack_div.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnksnaha_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string Districtcode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string SubDivisionCode = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            string Blockcode = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[3];

            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("7"));
            SqlParameter _Type = new SqlParameter("@Type", "snaha");
            SqlParameter _Districtcode = new SqlParameter("@DISTRICTCODE", Districtcode);
            SqlParameter _SubDivisionCode = new SqlParameter("@subDivision", SubDivisionCode);
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", Blockcode);
            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", ThanaCode);

           
            DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _Type, _Districtcode, _SubDivisionCode, _Blockcode, _ThanaCode });

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
            Pnlsearch.Visible = true;
            pnlgrid.Visible = false;
            btnBack_div.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkapraathamikee_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string Districtcode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string SubDivisionCode = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            string Blockcode = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[3];

            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("7"));
            SqlParameter _Type = new SqlParameter("@Type", "apraathamikee");
            SqlParameter _Districtcode = new SqlParameter("@DISTRICTCODE", Districtcode);
            SqlParameter _SubDivisionCode = new SqlParameter("@subDivision", SubDivisionCode);
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", Blockcode);
            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", ThanaCode);

            
            DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _Type, _Districtcode, _SubDivisionCode, _Blockcode, _ThanaCode });

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
            Pnlsearch.Visible = true;
            pnlgrid.Visible = false;
            btnBack_div.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Pnlsearch.Visible = false;
        pnlgrid.Visible = true;
        btnBack_div.Visible = false;
    }
}