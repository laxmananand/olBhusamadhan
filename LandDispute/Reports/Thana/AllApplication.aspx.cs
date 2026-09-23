using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.IO;

public partial class LandDispute_THANA_Thana_Entry : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    static string key = Encryptor.PrivateKey.ToString();
    Encryptor enc = new Encryptor(key);
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
        bindCommissionary();
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
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType });
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
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _CommissionaryCode });
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
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _DistCode });
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
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _SubDivision });
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
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _Blockcode });
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

    protected void bindAllApplication_Grid()
    {
        try
        {
            int lessCount = 0;
            int morecount = 0;
            if (int.Parse(ddlcount.SelectedValue) == 0)
            {
                lessCount = 0;
                morecount = 0;
            }
            if (int.Parse(ddlcount.SelectedValue) == 1)
            {
                lessCount = 0;
                morecount = 0;
            }
            if (int.Parse(ddlcount.SelectedValue) == 2)
            {
                lessCount = 1;
                morecount = 5;
            }
            if (int.Parse(ddlcount.SelectedValue) == 3)
            {
                lessCount = 6;
                morecount = 10;
            }
            if (int.Parse(ddlcount.SelectedValue) == 4)
            {
                lessCount = 11;
                morecount = 15;
            }
            if (int.Parse(ddlcount.SelectedValue) == 5)
            {
                lessCount = 16;
                morecount = 20;
            }
            if (int.Parse(ddlcount.SelectedValue) == 6)
            {
                lessCount = 21;
                morecount = 30;
            }
            if (int.Parse(ddlcount.SelectedValue) == 7)
            {
                lessCount = 31;
                morecount = 0;
            }

            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("6"));
            SqlParameter _CommissionaryCode = new SqlParameter("@DivisionCode", ddlCommissionary.SelectedValue == "0" ? "0" : ddlCommissionary.SelectedValue);
            SqlParameter _DistCode = new SqlParameter("@DISTRICTCODE", ddldistrict.SelectedValue == "0" ? "0" : ddldistrict.SelectedValue);
            SqlParameter _SubDivision = new SqlParameter("@SubDivisionCode", ddlsubdivision.SelectedValue == "0" ? "0" : ddlsubdivision.SelectedValue);
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", ddlcircle.SelectedValue == "0" ? "0" : ddlcircle.SelectedValue);
            SqlParameter _pscode = new SqlParameter("@ThanaCode", ddlthana.SelectedValue == "0" ? "0" : ddlthana.SelectedValue);
            SqlParameter _fromdate = new SqlParameter("@fromDate", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter _ToDate = new SqlParameter("@ToDate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter _lessCount = new SqlParameter("@lessCount1", lessCount);
            SqlParameter _morecount = new SqlParameter("@morecount", morecount);
            DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _CommissionaryCode, _DistCode, _SubDivision, _Blockcode, _pscode, _fromdate, _ToDate, _lessCount, _morecount });



            if (dt.Rows.Count > 0)
            {
                grvAllApplication.Columns[5].FooterText = "Total :";
                grvAllApplication.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalEntry")).Sum().ToString();
                grvAllApplication.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grvAllApplication.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();
                grvAllApplication.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalMetting")).Sum().ToString();

                grvAllApplication.DataSource = dt;
                grvAllApplication.DataBind();
            }
            else
            {
                grvAllApplication.Columns[5].FooterText = "Total :";
                grvAllApplication.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalEntry")).Sum().ToString();
                grvAllApplication.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grvAllApplication.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();
                grvAllApplication.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalMetting")).Sum().ToString();

                grvAllApplication.DataSource = null;
                grvAllApplication.DataBind();
            }
        }
        catch(Exception ex)
        {

        }
        

    }

    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {

            ExportExcel(grvAllApplication, pnlgrid);
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
        
        bindAllApplication_Grid();
    }
}