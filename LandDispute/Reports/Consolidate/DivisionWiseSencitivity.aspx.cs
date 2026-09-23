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
using iTextSharp.text.pdf.qrcode;

public partial class LandDispute_Report_Consolidate_DivisionWiseSencitivity : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null)
        {
            
            if (!IsPostBack)
            {
                ViewState["BlockCode"] = "";
                ViewState["RoleStepBack"] = "";
                ViewState["ThanaCode"] = "";
                //9 division wise login role
                if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR" || Session["Role"].ToString() == "ADGLAW") //
                {
                    ViewState["RoleStepBack"] = 5 + "";
                    bindDivision();
                }
                //Commsionary login role
                else if (Session["Role"].ToString() == "COM")
                {
                    ViewState["RoleStepBack"] = 4 + "";
                    getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
                }
                //District wise login role
                else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
                {
                    ViewState["RoleStepBack"] = 3 + "";
                    getSubDivisionWiseRpt(Convert.ToString(Session["District_Code"]));
                }
                //Subdivision wise login role
                else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
                {
                    ViewState["RoleStepBack"] = 2 + "";
                    getCircleWiseRpt(Convert.ToString(Session["Sub_DivCode"]));
                }
                //circle wise login role
                else if (Session["Role"].ToString() == "COOPT")
                {
                    ViewState["RoleStepBack"] = 1 + "";
                    getThanaWiseRpt(Session["Block_Code"].ToString());
                }
                //thana wise login role
                else if (Session["Role"].ToString() == "SHOOPT")
                {
                    ViewState["RoleStepBack"] = 0 + "";
                    getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"].ToString()));
                }
                else
                {                   
                    Response.Redirect("~/Default.aspx");
                }
            }          
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
        }
    }
    
    void bindDivision()
    {
        try
        {

            SqlParameter GetQuery = new SqlParameter("@QueryType", "1");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
           

            DataTable dt = clsData.GetDataTableWithProc("sp_AllSensitivity_Division", new SqlParameter[]
            { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode,
                GetThanCode, GetPanchayatCode});
            lblPrintDateForDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {                             
                grd_Division.Columns[1].FooterText = "Total :";
                grd_Division.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_Division.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity1")).Sum().ToString();
                grd_Division.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity2")).Sum().ToString();
                grd_Division.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity3")).Sum().ToString();

                grd_Division.DataSource = dt;
                grd_Division.DataBind();
            }
            else
            {
                grd_Division.DataSource = null;
                grd_Division.DataBind();
            }
            pnlDist.Visible = true;
            grd_Division.Visible = true;

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

            pnlSubDivision.Visible = false;
            grdSubDivision.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    
    protected void grd_Division_RowCommand(object sender, GridViewCommandEventArgs e)
    {             
        if (e.CommandName == "DivClick")
        {            
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotalDivision"));
            if (Total.Text.ToString() != "0")
            {
                string DivisionCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["DIVISIONName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }
                lbltext.Visible = true;
                btnback.Visible = true;
                getDistrictWiseRpt(DivisionCode);
            }   
        }
        if (e.CommandName == "TotalClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotal"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "DivisionSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Division", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity1_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity1"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "DivisionSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Division", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity2_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity2"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "DivisionSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Division", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity3_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity3"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "DivisionSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Division", SencitivityCode);
            }
        }
    }
  
    protected void getDistrictWiseRpt(string DIVISIONCODE)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "2");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", DIVISIONCODE.Trim());
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_AllSensitivity_Division", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode});

            lblPrintDateForDistrict.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {

                grdDistrict.Columns[1].FooterText = "Total :";
                grdDistrict.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdDistrict.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity1")).Sum().ToString();
                grdDistrict.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity2")).Sum().ToString();
                grdDistrict.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity3")).Sum().ToString();


                grdDistrict.DataSource = dt;
                grdDistrict.DataBind();
            }
            else
            {

                grdDistrict.DataSource = null;
                grdDistrict.DataBind();
            }

            pnlDist.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = true;
            grdDistrict.Visible = true;

            pnlSubDivision.Visible = false;
            grdSubDivision.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    
    protected void grdDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "DstClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotalDistrict"));
            if (Total.Text.ToString() != "0")
            {
                string Distcode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["DistrictName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + ",District Name :- " + ViewState["DistrictName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }
                lbltext.Visible = true;
                btnback.Visible = true;
                getSubDivisionWiseRpt(Distcode);
            }          
        }
        if (e.CommandName == "TotalClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotal"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "DistrictSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "District", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity1_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity1"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "DistrictSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "District", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity2_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity2"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "DistrictSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "District", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity3_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity3"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "DistrictSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "District", SencitivityCode);
            }
        }
    }

    protected void getSubDivisionWiseRpt(string DistCode)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "3");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", DistCode.Trim());
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_AllSensitivity_Division", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode });
            lblPrintDateForSubDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdSubDivision.Columns[1].FooterText = "Total :";
                grdSubDivision.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdSubDivision.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity1")).Sum().ToString();
                grdSubDivision.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity2")).Sum().ToString();
                grdSubDivision.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity3")).Sum().ToString();

                grdSubDivision.DataSource = dt;
                grdSubDivision.DataBind();
            }
            else
            {
                
                grdSubDivision.DataSource = null;
                grdSubDivision.DataBind();
            }
            pnlDist.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

            pnlSubDivision.Visible = true;
            grdSubDivision.Visible = true;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    
    protected void grdSubDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {
         if (e.CommandName == "sdClick")
           {            
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotalSubDivision"));
            if (Total.Text.ToString() != "0")
            {
                string subDivsionCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["SubDivisionName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + "," + "District Name :- " + ViewState["DistrictName"].ToString() +
                        "<br>Sub-DivisionName :- " + ViewState["SubDivisionName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() + ",SubDivisionName :- " +
                    ViewState["SubDivisionName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    lbltext.Text = "Sub-DivisionName :- " +
                    ViewState["SubDivisionName"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }
                lbltext.Visible = true;
                btnback.Visible = true;
                getCircleWiseRpt(subDivsionCode);
            }         
        }
         if (e.CommandName == "TotalClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotal"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "SubDivisionSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "subDivision", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity1_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity1"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "SubDivisionSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "subDivision", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity2_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity2"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "SubDivisionSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "subDivision", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity3_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity3"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "SubDivisionSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "subDivision", SencitivityCode);
            }
        }
    }

    protected void getCircleWiseRpt(string SubDivcode)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "4");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", SubDivcode.Trim());
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_AllSensitivity_Division", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode });
            lblPrintDateForCircle.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity1")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity2")).Sum().ToString();
                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity3")).Sum().ToString();

                grdCircle.DataSource = dt;
                grdCircle.DataBind();
            }
            else
            {

                grdCircle.DataSource = null;
                grdCircle.DataBind();
            }

            pnlDist.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

            pnlSubDivision.Visible = false;
            grdSubDivision.Visible = false;

            pnlCircle.Visible = true;
            grdCircle.Visible = true;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
   
    protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
            if (e.CommandName == "BlockClick")
            {            
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotalBlock"));
            if (Total.Text.ToString() != "0")
            {
                string BlockCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["BlockName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["BlockCode"] = BlockCode;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + "," + "District Name :- " + ViewState["DistrictName"].ToString() +
                    "<br>Sub- DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle /Block :-" + ViewState["BlockName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                    ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + "<br>Circle /Block :-" + ViewState["BlockName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle/Block :-" + ViewState["BlockName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    lbltext.Text = "Circle/Block :-" + ViewState["BlockName"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }
                lbltext.Visible = true;
                btnback.Visible = true;
                getThanaWiseRpt(BlockCode);
            }     
        }
        if (e.CommandName == "TotalClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotal"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "BlockSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Block", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity1_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity1"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "BlockSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Block", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity2_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity2"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "BlockSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Block", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity3_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity3"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "BlockSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Block", SencitivityCode);
            }
        }
    }

    protected void getThanaWiseRpt(string Blockcode)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "5");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_AllSensitivity_Division", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode });
            lblPrintDateForThana.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText =dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdThana.Columns[3].FooterText =dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity1")).Sum().ToString();
                grdThana.Columns[4].FooterText =dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity2")).Sum().ToString();
                grdThana.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity3")).Sum().ToString();

                grdThana.DataSource = dt;
                grdThana.DataBind();
            }
            else
            {

                grdThana.DataSource = null;
                grdThana.DataBind();
            }

            pnlDist.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

            pnlSubDivision.Visible = false;
            grdSubDivision.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = true;
            grdThana.Visible = true;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    
    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {
          if (e.CommandName == "ThanaClick")
           {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotalThana"));
            if (Total.Text.ToString() != "0")
            {
                string thanaCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["thana"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["ThanaCode"] = thanaCode;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + "," + ",District Name :- " + ViewState["DistrictName"].ToString() +
                    "<br>Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle /Block :-" + ViewState["BlockName"].ToString()
                    + "<br>Thana :- " + ViewState["thana"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    lbltext.Text = ",District Name :- " + ViewState["DistrictName"].ToString() +
                    ",Sub- DivisionName :-" + ViewState["SubDivisionName"].ToString() + "<br>Circle /Block :-" + ViewState["BlockName"].ToString()
                    + ",Thana :- " + ViewState["thana"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle /Block :-" + ViewState["BlockName"].ToString()
                    + "<br>Thana :- " + ViewState["thana"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    lbltext.Text = "Circle/Block :-" + ViewState["BlockName"].ToString()
                    + ",Thana :- " + ViewState["thana"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    lbltext.Text = "Thana :- " + ViewState["thana"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }
                lbltext.Visible = true;

                btnback.Visible = true;              
                getPanchayatWiseRpt(thanaCode);
            }
        }
        if (e.CommandName == "TotalClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotal"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "ThanaSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Thana", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity1_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity1"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "ThanaSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Thana", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity2_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity2"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "ThanaSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Thana", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity3_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity3"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "ThanaSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Thana", SencitivityCode);
            }
        }
    }

    protected void getPanchayatWiseRpt(string thanaCode)
    {
       
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "6");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", thanaCode.Trim());
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_AllSensitivity_Division", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode });
            lblPrintDateForPanchayat.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdPanchayat.Columns[1].FooterText = "Total :";
                grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayat.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity1")).Sum().ToString();
                grdPanchayat.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity2")).Sum().ToString();
                grdPanchayat.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Sensitivity3")).Sum().ToString();

                grdPanchayat.DataSource = dt;
                grdPanchayat.DataBind();
            }
            else
            {

                grdPanchayat.DataSource = null;
                grdPanchayat.DataBind();
            }

            pnlDist.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

            pnlSubDivision.Visible = false;
            grdSubDivision.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = true;
            grdPanchayat.Visible = true;
        }
        catch (Exception ex)
        { }
    }
    
    protected void grdPanchayat_RowCommand(object sender, GridViewCommandEventArgs e)
    {      
        if (e.CommandName == "PanchayatClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotalPanchayat"));
            if (Total.Text.ToString() != "0")
            {
                char[] seprator = { ',' };
                string Panchayatcode = e.CommandArgument.ToString().Trim().Split(seprator)[0];
                string thanacode = ViewState["ThanaCode"].ToString() == "" ? Session["Thana_Code"].ToString() : ViewState["ThanaCode"].ToString();
                ViewState["PanchayatName"] = e.CommandArgument.ToString().Trim().Split(seprator)[1];
                //ViewState["thana"]= ViewState["thana"].ToString() == "" ? "" : "<br>Thana :-" + ViewState["thana"].ToString();
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + "," + ",District Name :- " + ViewState["DistrictName"].ToString() +
                    "<br>Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle /Block :-" + ViewState["BlockName"].ToString()
                    + "<br>Thana :- " + ViewState["thana"].ToString() + ",Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                    ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + "<br/>Circle /Block :-" + ViewState["BlockName"].ToString()
                    + ",Thana :- " + ViewState["thana"].ToString() + "<br>Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle /Block :-" + ViewState["BlockName"].ToString()
                    + "<br>Thana :- " + ViewState["thana"].ToString() + ",Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    lbltext.Text = "Circle /Block :-" + ViewState["BlockName"].ToString()
                    + "Thana :- " + ViewState["thana"].ToString() + "<br>Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    lbltext.Text = "Thana :- " + ViewState["thana"].ToString() + ",Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    lbltext.Text = "Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }

                lbltext.Visible = true;

                btnback.Visible = true;
                //getPanchayatWiseData(thanacode,Panchayatcode);
                getPanchayatWiseData(Panchayatcode, "Panchayat", "0");
            }   
        }
        if (e.CommandName == "TotalClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblTotal"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "PanchayatSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Panchayat", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity1_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity1"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "PanchayatSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Panchayat", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity2_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity2"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "PanchayatSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Panchayat", SencitivityCode);
            }
        }
        if (e.CommandName == "Sensitivity3_Click")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label Total = ((Label)gvr.FindControl("lblSensitivity3"));
            if (Total.Text.ToString() != "0")
            {
                string Code = e.CommandArgument.ToString().Trim().Split(',')[0];
                string SencitivityCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Sencitivity"] = "PanchayatSencitivity";
                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Code, "Panchayat", SencitivityCode);
            }
        }
    }
     
    private void getPanchayatWiseData(string Code, string type,string sensitivityType)
    {
        try
        {
            Session["mySearchAppData03"] = null;
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
            SqlParameter Getcode = new SqlParameter("@Code", Convert.ToInt32(Code));
            SqlParameter Gettype = new SqlParameter("@type", type.Trim());
            SqlParameter GetsensitivityType = new SqlParameter("@sensitivityType", Convert.ToInt32(sensitivityType));
            DataTable dt = clsData.GetDataTableWithProc("sp_AllSensitivity_Division", new SqlParameter[] { GetQueryType, Getcode, Gettype , GetsensitivityType });
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

            pnlDist.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

            pnlSubDivision.Visible = false;
            grdSubDivision.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;

            Pnlsearch.Visible = true;
            GridView1.Visible = true;
        }
        catch (Exception)
        {

        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    
    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {
            if (pnlDist.Visible == true && grd_Division.Visible == true)
            {
                ExportExcel(grd_Division, pnlDist);
            }
            if (pnlDistrict.Visible == true && grdDistrict.Visible == true)
            {
                ExportExcel(grdDistrict, pnlDistrict);
            }
            if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
            {
                ExportExcel(grdSubDivision, pnlSubDivision);
            }
            if (pnlCircle.Visible == true && grdCircle.Visible == true)
            {
                ExportExcel(grdCircle, pnlCircle);
            }
            if (pnlthana.Visible == true && grdThana.Visible == true)
            {
                ExportExcel(grdThana, pnlthana);
            }
            if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
            {
                ExportExcel(grdPanchayat, pnlpanchayat);

            }
            if (Pnlsearch.Visible == true && GridView1.Visible == true)
            {
                ExportExcel(GridView1, Pnlsearch);

            }
        }
        catch (Exception)
        {

            throw;
        }

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
                for (int k = 0; k < gvrow.Cells.Count; k++)
                {
                    gvrow.Cells[k].Style.Add("border-style", "Solid");
                    gvrow.Cells[k].Style.Add("border-color", "Black");
                    gvrow.Cells[k].ForeColor = System.Drawing.Color.Black;
                }
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
    
    protected void btnback_Click(object sender, EventArgs e)
    {
        lbltext.Text = "";
        try
        {
            if (ViewState["Sencitivity"].ToString() == "DivisionSencitivity")
            {
                ViewState["Sencitivity"] = "";
                pnlDist.Visible = true;
                grd_Division.Visible = true;
                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
           else if (ViewState["Sencitivity"].ToString() == "DistrictSencitivity")
            {
                ViewState["Sencitivity"] = "";
                pnlDistrict.Visible = true;
                grdDistrict.Visible = true;
                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
           else if (ViewState["Sencitivity"].ToString() == "SubDivisionSencitivity")
            {
                ViewState["Sencitivity"] = "";
                pnlSubDivision.Visible = true;
                grdSubDivision.Visible = true;
                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["Sencitivity"].ToString() == "BlockSencitivity")
            {
                ViewState["Sencitivity"] = "";
                pnlCircle.Visible = true;
                grdCircle.Visible = true;
                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }           
            else if (ViewState["Sencitivity"].ToString() == "ThanaSencitivity")
            {
                ViewState["Sencitivity"] = "";
               

                pnlthana.Visible = true;
                grdThana.Visible = true;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["Sencitivity"].ToString() == "PanchayatSencitivity")
            {
                ViewState["Sencitivity"] = "";
                pnlpanchayat.Visible = true;
                grdPanchayat.Visible = true;
                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if(pnlDistrict.Visible == true && grdDistrict.Visible == true)
            {
                lbltext.Visible = false;
                btn_Export.Visible = true;

                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlDist.Visible = true;
                    grd_Division.Visible = true;
                }
                else
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlDist.Visible = false;
                    grd_Division.Visible = false;
                }
            }
            else if(pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
            {

                btn_Export.Visible = true;
                lbltext.Visible = true;

                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString();
                    pnlDistrict.Visible = true;
                    grdDistrict.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlDistrict.Visible = true;
                    grdDistrict.Visible = true;
                }
                else
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlDistrict.Visible = false;
                    grdDistrict.Visible = false;
                }

            }
            else if(pnlCircle.Visible == true && grdCircle.Visible == true)
            {
                btn_Export.Visible = true;

                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString()
                        + ",District Name :- " + ViewState["DistrictName"].ToString();
                    pnlSubDivision.Visible = true;
                    grdSubDivision.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = true;
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString();
                    pnlSubDivision.Visible = true;
                    grdSubDivision.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlSubDivision.Visible = true;
                    grdSubDivision.Visible = true;
                }
                else
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlSubDivision.Visible = false;
                    grdSubDivision.Visible = false;
                }
            }
            else if(pnlthana.Visible == true && grdThana.Visible == true)
            {

                lbltext.Visible = true;

                btn_Export.Visible = true;

                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;


                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + ",District Name :- " + ViewState["DistrictName"].ToString() +
                                         "<br>Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString();
                    pnlCircle.Visible = true;
                    grdCircle.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = true;
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                                         ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString();
                    pnlCircle.Visible = true;
                    grdCircle.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString();
                    pnlCircle.Visible = true;
                    grdCircle.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlCircle.Visible = true;
                    grdCircle.Visible = true;
                }
                else
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlCircle.Visible = false;
                    grdCircle.Visible = false;
                }
            }
            else if(pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
            {
                lbltext.Visible = true;

                btn_Export.Visible = true;

                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = "DivisionName:-" + ViewState["DIVISIONName"].ToString() + "," + "DistrictName:-" + ViewState["DistrictName"].ToString()
                        + "<br/>SubDivisionName:-" + ViewState["SubDivisionName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString();
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = true;
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                                         ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString()
                                         + "<br/>CircleBlock:-" + ViewState["BlockName"].ToString();
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() +
                                   ",CircleBlock:-" + ViewState["BlockName"].ToString();
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = true;
                    lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString();
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
                else
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlthana.Visible = false;
                    grdThana.Visible = false;
                }
            }
            else if(Pnlsearch.Visible == true && GridView1.Visible == true)
            {


                lbltext.Visible = true;

                btn_Export.Visible = true;

                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = lbltext.Text = "Division Name:-" + ViewState["DIVISIONName"].ToString() + ",District Name:-" + ViewState["DistrictName"].ToString() + "<br/>SubDivisionName:-"
                       + ViewState["SubDivisionName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString() + "<br/>" + "Thana:-" + ViewState["thana"].ToString();
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = true;
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                                         ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString()
                                         + "<br/>CircleBlock:-" + ViewState["BlockName"].ToString()
                                         + ",Thana:-" + ViewState["thana"].ToString();
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() +
                                   ",CircleBlock:-" + ViewState["BlockName"].ToString()
                                   + "<br/>Thana:-" + ViewState["thana"].ToString();
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = true;
                    lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString()
                                    + ",Thana:-" + ViewState["thana"].ToString();
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Thana:-" + ViewState["thana"].ToString();
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
            }
        }
        catch (Exception er)
        {

        }
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
}