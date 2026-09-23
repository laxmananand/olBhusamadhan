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
using System.Drawing;
using System.Threading;

public partial class LandDispute_THANA_ViewDetailsIsLogin : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    static string key = Encryptor.PrivateKey.ToString();
    Encryptor enc = new Encryptor(key);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/DeptDefault.aspx");
        }
        if (!IsPostBack)
        {

            bindDistrict();
             bindSubDivision();
                bindBlock();
                bindThana();
            if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString() == "COM" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR")
            {
               
                
            }
            else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
            {
                ddlDistrict.SelectedValue = Session["District_Code"].ToString();
                ddlDistrict.Enabled = false;
                bindSubDivision();
               
            }

            else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
            {
                ddlDistrict.SelectedValue = Session["District_Code"].ToString();
                ddlDistrict.Enabled = false;
                bindSubDivision();
                ddlSubDivision.SelectedValue = Session["Sub_DivCode"].ToString();
                ddlSubDivision.Enabled = false;
                bindBlock();
                bindThana();
            }
            else if (Session["Role"].ToString() == "COOPT")
            {
                ddlDistrict.SelectedValue = Session["District_Code"].ToString();
                ddlDistrict.Enabled = false;
                bindSubDivision();
                ddlSubDivision.SelectedValue = Session["Sub_DivCode"].ToString();
                ddlSubDivision.Enabled = false;
                bindBlock();
                ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                ddlBlock.Enabled = false;
                bindThana();
            }
            else if (Session["Role"].ToString() == "SHOOPT")
            {
                ddlDistrict.SelectedValue = Session["District_Code"].ToString();             
                ddlDistrict.Enabled = false;

                bindSubDivision();
                ddlSubDivision.SelectedValue = Session["Sub_DivCode"].ToString();               
                ddlSubDivision.Enabled = false;

                bindBlock();
                ddlBlock.SelectedValue = Session["Block_Code"].ToString();               
                ddlBlock.Enabled = false;

                bindThana();
                ddlThana.SelectedValue = Session["Thana_Code"].ToString();               
                ddlThana.Enabled = false;
            }
       
            ViewState["PS_Code"] = "";
            bind_grd_viewDetails();
        }
    }

    public void bind_grd_viewDetails()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter _DisCode = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlDistrict.SelectedValue));
        SqlParameter _subcode = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlSubDivision.SelectedValue));
        SqlParameter _circode = new SqlParameter("@CircleCode", Convert.ToInt32(ddlBlock.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlBlock.SelectedValue));
        SqlParameter _thanacode = new SqlParameter("@PS_Code", Convert.ToInt32(ddlThana.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlThana.SelectedValue));
        SqlParameter _islogin = new SqlParameter("@islogin",      ddlIsLogin.SelectedValue == "y" ? "y" : ddlIsLogin.SelectedValue);
        DataTable dt_viewDetails = clsData.GetDataTableWithProc("SP_ViewDetailsIsLogin",
            new SqlParameter[] { QueryType, _DisCode, _subcode, _circode, _thanacode, _islogin });
        if (dt_viewDetails.Rows.Count > 0)
        {
            grd_viewDetails.DataSource = dt_viewDetails;
            ViewState["dt_viewDetails"] = dt_viewDetails;
            grd_viewDetails.DataBind();
        }
        else
        {
            grd_viewDetails.DataSource = null;
            grd_viewDetails.DataBind();
        }
    }
    private void bindDistrict()
    {
        ddlDistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        
        DataTable dt = clsData.GetDataTableWithProc("SP_ViewDetailsIsLogin", new SqlParameter[] { QueryType });
        if (dt.Rows.Count > 0)
        {
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--All--", "0"));
        }
        else
        {
            ddlDistrict.DataSource = null;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--All--", "0"));
        }


    }
    private void bindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("2"));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_ViewDetailsIsLogin", new SqlParameter[] { QueryType, District });
        if (dt.Rows.Count > 0)
        {
            ddlSubDivision.DataSource = dt;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("--All--", "0"));
        }
        else
        {
            ddlSubDivision.DataSource = null;
            ddlSubDivision.DataTextField = "";
            ddlSubDivision.DataValueField = "";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("--All--", "0"));
        }
    }
    private void bindBlock()
    {
        ddlBlock.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter _SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_ViewDetailsIsLogin", new SqlParameter[] { QueryType, _SubDivision });
        if (dt.Rows.Count > 0)
        {
            ddlBlock.DataSource = dt;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("--All--", "0"));
        }
        else
        {
            ddlBlock.DataSource = null;
            ddlBlock.DataTextField = "";
            ddlBlock.DataValueField = "";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("--All--", "0"));
        }
    }    
    private void bindThana()
    {
        ddlThana.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter _District = new SqlParameter("@District", ddlDistrict.SelectedValue);
        SqlParameter _SubDivision = new SqlParameter("@SubDivision", ddlSubDivision.SelectedValue);
        SqlParameter _CircleCode = new SqlParameter("@CircleCode", ddlBlock.SelectedValue);
        DataTable dt_thana = clsData.GetDataTableWithProc("SP_ViewDetailsIsLogin", new SqlParameter[] 
        { QueryType,_District, _SubDivision, _CircleCode });
        if (dt_thana.Rows.Count > 0)
        {
            ddlThana.DataSource = dt_thana;
            ddlThana.DataTextField = "Police_Station";
            ddlThana.DataValueField = "PS_Code";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, new ListItem("--All--", "0"));
        }
        else
        {
            ddlThana.DataSource = null;
            ddlThana.DataTextField = "";
            ddlThana.DataValueField = "";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, new ListItem("--All--", "0"));
        }

    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubDivision();
        bindBlock();
        bindThana();
    }
    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindBlock();
        bindThana();
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindThana();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bind_grd_viewDetails();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExpInExl(grd_viewDetails, pnlviewDetails);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //
    }


    void ExpInExl(GridView gv, Panel pnl)
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", DateTime.Now.ToString("ddMMyyhhmmss") + "ApplicationConsolidateRpt.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
            {
                gv.HeaderRow.Cells[i].Style.Add("border-style", "Solid");
                gv.HeaderRow.Cells[i].Style.Add("border-color", "Black");
                gv.HeaderRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
                gv.HeaderRow.Cells[i].Style.Add("Font-Bold", "True");
                gv.HeaderRow.Cells[i].Style.Add("Fore-Color", "Black");

                // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
            }
            for (int i = 0; i < gv.FooterRow.Cells.Count; i++)
            {
                // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
                gv.FooterRow.Cells[i].Style.Add("border-style", "Solid");
                gv.FooterRow.Cells[i].Style.Add("border-color", "Black");
                gv.FooterRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
                gv.FooterRow.Cells[i].Style.Add("Font-Bold", "True");
                gv.FooterRow.Cells[i].Style.Add("Fore-Color", "Black");
            }
            int j = 1;
            //   This loop is used to apply stlye to cells based on particular row
            foreach (GridViewRow gvrow in gv.Rows)
            {
                gvrow.BackColor = System.Drawing.Color.White;
                if (j <= gv.Rows.Count)
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

            gv.RenderControl(htw);
            string dd = sw.ToString();
            dd = dd.Replace("href=", "");
            string Headrer = lbltext.Text;
            Response.Write(dd);
            Response.End();
        }
        catch (Exception ex)
        { }
        
    }


}