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
using System.Net;

public partial class LandDispute_Report_ApplicationConsolidateBlockRpt : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null|| Session["Role"].ToString() !="")
        {
            
            if (!IsPostBack)
            {
                ViewState["PageIndex"] = "1";
                ViewState["finaldata"] = "3";
                ViewState["DivisionCode"] = "";
                ViewState["DistrictCode"] = "";
                ViewState["SubDivisionCode"] = "";
                ViewState["BlockCode"] = "";
                ViewState["ThanaCode"] = "";
                ViewState["Matter_Status"] = "0";
                BindRole();
            }
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            ViewState.Clear();
            Response.Redirect("~/Login_Default.aspx.aspx");
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

        if (Session["Commsionary_Code"] != null)
        {
            if (Session["Commsionary_Code"].ToString() != "")
            {
                ddlCommissionary.SelectedValue = Session["Commsionary_Code"].ToString();
                ddlCommissionary.Enabled = false;
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
    private void bindRange()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("0"));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType });
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
    private void bindCommissionary()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType });
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
    private void bindDistrict()
    {
        ddlDistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString()));
        SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, CommissionaryCode, Rangeid });
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
    void bindDivision()
    {
        try
        {
            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
            SqlParameter GetComm_Code = new SqlParameter("@DivisionCode", Convert.ToInt32(ddlCommissionary.SelectedValue.Trim()));
            SqlParameter GetRange_Code = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.Trim()));
            SqlParameter GetDistrict_Code = new SqlParameter("@DistrictCode", Convert.ToInt32(ddlDistrict.SelectedValue.Trim()));
            SqlParameter GetSub_DivCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ddlSubDivision.SelectedValue.Trim()));
            SqlParameter GetBlock_Code = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.Trim()));
            SqlParameter GetThana_Code = new SqlParameter("@ThanCode", Convert.ToInt32(ddlThana.SelectedValue.Trim()));
            SqlParameter GetEntry_Mode = new SqlParameter("@EntryMode", ddlentrymode.SelectedValue.Trim());
            SqlParameter getfromdate = new SqlParameter("@FromDate", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter gettodate = new SqlParameter("@ToDate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetBlockAppConsolidateRpt", new SqlParameter[] { QueryType, GetComm_Code,GetDistrict_Code, GetSub_DivCode, GetBlock_Code, GetThana_Code, GetEntry_Mode, getfromdate, gettodate, GetRange_Code });
           
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                //lblDetail.Visible = true;
                griddata.Columns[5].FooterText = "Total :";
                griddata.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                griddata.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                griddata.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                griddata.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                griddata.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                griddata.Columns[11].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                griddata.Columns[12].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                griddata.Columns[13].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                griddata.Columns[14].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();

                griddata.DataSource = dt;
                griddata.DataBind();
            }
            else
            {
                griddata.DataSource = dt;
                griddata.DataBind();
            }                             
        }
        catch (Exception ex)
        { }
    }
    private void bindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, District });
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
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, @SubDivision });
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
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("9"));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        SqlParameter block = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, District, @SubDivision, block });
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
            if(griddata.Visible==true)
            {
                ExportExcel(griddata, pnlgrid);
            }
            else
            {
                ExportExcel(GridView1, Pnlsearch);
            }
               
            
           
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
        bindDivision();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindthana();
    }

    protected void lnkDivision()
    {
        DataTable dt = new DataTable();
        SqlParameter GetQuery = new SqlParameter("@QueryType", 2);
        SqlParameter Getfinaldata = new SqlParameter("@finaldata", ViewState["finaldata"].ToString());
        SqlParameter GetDivisioncode = new SqlParameter("@DivisionCode", ViewState["DivisionCode"].ToString());
        SqlParameter GetDistrictcode = new SqlParameter("@DistrictCode", ViewState["DistrictCode"].ToString());
        SqlParameter GetSub_DivCode = new SqlParameter("@SubDivisionCode", ViewState["SubDivisionCode"].ToString());
        SqlParameter GetBlock_Code = new SqlParameter("@BlockCode", ViewState["BlockCode"].ToString());
        SqlParameter GetThana_Code = new SqlParameter("@ThanCode", ViewState["ThanaCode"].ToString());
        SqlParameter GetEntry_Mode = new SqlParameter("@EntryMode", ddlentrymode.SelectedValue.Trim());
        SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", Convert.ToInt32(ViewState["Matter_Status"].ToString()));
        SqlParameter GetFromDate = new SqlParameter("@FromDate", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetToDate = new SqlParameter("@ToDate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
        SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
        SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
        SqlParameter _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
        _RecordCount.Direction = System.Data.ParameterDirection.Output;
        dt = clsData.GetDataTableWithProc("Sp_GetBlockAppConsolidateRpt", new SqlParameter[]
        { GetQuery, Getfinaldata,GetDistrictcode, GetSub_DivCode, GetBlock_Code,GetThana_Code,GetEntry_Mode,
                GetMatter_Status,GetFromDate,GetToDate,_PageSize,_PageIndex,_RecordCount });
        int totalRecord = 0;
        if (_RecordCount.Value != null)
        {
            int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
        }

        GridView1.DataSource = dt;
        GridView1.DataBind();

        int recordCount = Convert.ToInt32(_RecordCount.Value);
        this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), "50");


        div_Back.Visible = true;
        pnlgrid.Visible = false;
        griddata.Visible = false;
        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }
    protected void lnkDivisionTotal_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "0";
            ViewState["finaldata"] = "3";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDivisionFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "0";
            ViewState["finaldata"] = "1";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDivisionUnFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "0";
            ViewState["finaldata"] = "2";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDivisionNirast_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "1";
            ViewState["finaldata"] = "3";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDivisionFinalNirast_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "5";
            ViewState["finaldata"] = "3";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDivisionPrakriyadhin_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "3";
            ViewState["finaldata"] = "3";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDivisionAshwikrit_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "4";
            ViewState["finaldata"] = "3";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDivisionMapi_Nirdharit_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "2";
            ViewState["finaldata"] = "3";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDivisionvaadi_ki_vaad_sankhya_varsh_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "1";
            ViewState["finaldata"] = "";
            ViewState["DivisionCode"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["Matter_Status"] = "";

            LinkButton lnk = sender as LinkButton;
            ViewState["DivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string total = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["Matter_Status"] = "6";
            ViewState["finaldata"] = "3";
            if (Convert.ToInt32(total) > 0)
            {
                lnkDivision();
            }
        }
        catch (Exception)
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
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["PageIndex"] = pageIndex;
        lnkDivision();
    }


    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string Getpdf(string url)
    {
        Encryptor enc = new Encryptor(Encryptor.PrivateKey);
        string urlpath = "";
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
            //return true;
        }


        else
        {
            return false;
        }

        // return true;
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "1";
        ViewState["finaldata"] = "3";
        ViewState["DivisionCode"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["Matter_Status"] = "0";
        rptPager.DataSource = null;
        rptPager.DataBind();
        div_Back.Visible = false;
        pnlgrid.Visible = true;
        griddata.Visible = true;
        Pnlsearch.Visible = false;
        GridView1.Visible = false;
    }

}