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

public partial class LandDispute_Reports_VadiDeatils_VadiConsolidateRpt : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["BlockCode"] = "";
                ViewState["ThanaCode"] = "";
                ViewState["RoleStepBack"] = "";
                ViewState["GetRole"] = "";             
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
                //Commsionary login role
                else if (Session["Role"].ToString() == "DIG")
                {
                    ViewState["RoleStepBack"] = 4 + "";
                    getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
                }
                //District wise login role
                else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM" || Session["Role"].ToString() == "DIO")
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

            DataTable dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode});
            lblPrintDateForDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grd_Division.Columns[1].FooterText = "Total :";
                grd_Division.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_Division.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("General_Vadi")).Sum().ToString();
                grd_Division.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Vibhag_Vadi")).Sum().ToString();
                grd_Division.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("institute")).Sum().ToString();            
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

            PanelVibhag_Vadi.Visible = false;
            grdVibhag_Vadi.Visible = false;

        }
        catch (Exception ex)
        { }
    }
    protected void grd_Division_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Division_Vibhag_Vadi_Click")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string DivisionCode = ((HiddenField)grv.FindControl("hfDivisionCode")).Value;
            Label lblDT = ((Label)grv.FindControl("lblDivisionVibhag_Vadi"));
            if (Convert.ToInt32(lblDT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getVibhag_Vadi(Convert.ToInt32(DivisionCode));
                ViewState["GetRole"] = "Vibhag_Vadi_Division";
            }
        }
        if (e.CommandName == "Division_InstituteClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string DivisionCode = ((HiddenField)grv.FindControl("hfDivisionCode")).Value;
            Label lblIT = ((Label)grv.FindControl("lblDivisionInstitute"));
            if (Convert.ToInt32(lblIT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getInstitute(Convert.ToInt32(DivisionCode));
                ViewState["GetRole"] = "Institute_Division";
            }
        }
        if (e.CommandName == "DivisionClick")
        {
            lbltext.Text = "";
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblTot = ((Label)gvr.FindControl("lblDivisionTotal"));            
            if (Convert.ToInt32(lblTot.Text.ToString()) != 0)
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
    }

    protected void getVibhag_Vadi(int Code)
    {
        DataTable dt = new DataTable();
        if (pnlDist.Visible == true && grd_Division.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "7");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode });
        }
        if (pnlDistrict.Visible == true && pnlDistrict.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "8");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDistrictCode });
        }
        if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "9");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetSubDivisionCode });
        }
        if (pnlCircle.Visible == true && grdCircle.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "10");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetBlockCode });
        }
        if (pnlthana.Visible == true && grdThana.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "11");
            SqlParameter GetThanaCode = new SqlParameter("@ThanCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetThanaCode });
        }
        if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "12");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetPanchayatCode });
        }
        if (dt.Rows.Count > 0)
        {
            grdVibhag_Vadi.Columns[1].FooterText = "Total :";
            grdVibhag_Vadi.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
            grdVibhag_Vadi.DataSource = dt;
            grdVibhag_Vadi.DataBind();          
        }
        else
        {
            grdVibhag_Vadi.Columns[1].FooterText = "Total :";
            grdVibhag_Vadi.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
            grdVibhag_Vadi.DataSource = null;
            grdVibhag_Vadi.DataBind();
           
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

        PanelVibhag_Vadi.Visible = true;
        grdVibhag_Vadi.Visible = true;
    }
    protected void getInstitute(int Code)
    {
        DataTable dt = new DataTable();
        if (pnlDist.Visible == true && grd_Division.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "13");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode });
        }
        if (pnlDistrict.Visible == true && pnlDistrict.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "14");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDistrictCode });
        }
        if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "15");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetSubDivisionCode });
        }
        if (pnlCircle.Visible == true && grdCircle.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "16");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetBlockCode });
        }
        if (pnlthana.Visible == true && grdThana.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "17");
            SqlParameter GetThanaCode = new SqlParameter("@ThanCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetThanaCode });
        }
        if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "18");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Code);
            dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetPanchayatCode });
        }
        if (dt.Rows.Count > 0)
        {
            grvInstitute.Columns[1].FooterText = "Total :";
            grvInstitute.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
            grvInstitute.DataSource = dt;
            grvInstitute.DataBind();
        }
        else
        {
            grvInstitute.Columns[1].FooterText = "Total :";
            grvInstitute.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
            grvInstitute.DataSource = null;
            grvInstitute.DataBind();
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

        PanelInstitute.Visible = true;
        grvInstitute.Visible = true;
    }

    protected void getDistrictWiseRpt(string DIVISIONCODE)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "2");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", DIVISIONCODE.Trim());

            SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(Session["RangeCode"].ToString() == "" ? "0" : Session["RangeCode"].ToString()));

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");          

            DataTable dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, Rangeid });
            lblPrintDateForDistrict.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdDistrict.Columns[1].FooterText = "Total :";
                grdDistrict.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdDistrict.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("General_Vadi")).Sum().ToString();
                grdDistrict.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Vibhag_Vadi")).Sum().ToString();
                grdDistrict.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("institute")).Sum().ToString();

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
        if (e.CommandName == "District_Vibhag_Vadi_Click")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string DistrictCode = ((HiddenField)grv.FindControl("hfDistrictCode")).Value;
            Label lblDT = ((Label)grv.FindControl("lblDistrictVibhag_Vadi"));
            if (Convert.ToInt32(lblDT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getVibhag_Vadi(Convert.ToInt32(DistrictCode));
                ViewState["GetRole"] = "Vibhag_Vadi_District";
            }
        }

        if (e.CommandName == "District_InstituteClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string DistrictCode = ((HiddenField)grv.FindControl("hfDistrictCode")).Value;
            Label lblIT = ((Label)grv.FindControl("lblDistrictInstitute"));
            if (Convert.ToInt32(lblIT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getInstitute(Convert.ToInt32(DistrictCode));
                ViewState["GetRole"] = "Institute_District";
            }
        }
        if (e.CommandName == "DistrictClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblTot = ((Label)gvr.FindControl("lblDistrictTotal"));            
            if (Convert.ToInt32(lblTot.Text.ToString()) != 0)
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

            DataTable dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode});
            lblPrintDateforSubDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdSubDivision.Columns[1].FooterText = "Total :";
                grdSubDivision.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdSubDivision.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("General_Vadi")).Sum().ToString();
                grdSubDivision.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Vibhag_Vadi")).Sum().ToString();
                grdSubDivision.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("institute")).Sum().ToString();

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
        if (e.CommandName == "SubDivision_Vibhag_Vadi_Click")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string SubDivisionCode = ((HiddenField)grv.FindControl("hfSubDivision")).Value;
            Label lblDT = ((Label)grv.FindControl("lblSubDivisionVibhag_Vadi"));
            if (Convert.ToInt32(lblDT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getVibhag_Vadi(Convert.ToInt32(SubDivisionCode));
                ViewState["GetRole"] = "Vibhag_Vadi_SubDivision";
            }
        }

        if (e.CommandName == "SubDivision_InstituteClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string SubDivisionCode = ((HiddenField)grv.FindControl("hfSubDivision")).Value;
            Label lblIT = ((Label)grv.FindControl("lblSubDivisioninstitute"));
            if (Convert.ToInt32(lblIT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getInstitute(Convert.ToInt32(SubDivisionCode));
                ViewState["GetRole"] = "Institute_SubDivision";
            }
        }
        if (e.CommandName == "SubDivisionClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblTot = ((Label)gvr.FindControl("lblSubDivisionTotal"));
            if (Convert.ToInt32(lblTot.Text.ToString()) != 0)
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

            DataTable dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode });
            lblPrintDateForCircle.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("General_Vadi")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Vibhag_Vadi")).Sum().ToString();
                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("institute")).Sum().ToString();
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
        if (e.CommandName == "Block_Vibhag_Vadi_Click")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string BlockCode = ((HiddenField)grv.FindControl("hfBlockCode")).Value;
            Label lblDT = ((Label)grv.FindControl("lblBlockVibhag_Vadi"));
            if (Convert.ToInt32(lblDT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getVibhag_Vadi(Convert.ToInt32(BlockCode));
                ViewState["GetRole"] = "Vibhag_Vadi_Block";
            }
        }

        if (e.CommandName == "Block_InstituteClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string BlockCode = ((HiddenField)grv.FindControl("hfBlockCode")).Value;
            Label lblIT = ((Label)grv.FindControl("lblBlockInstitute"));
            if (Convert.ToInt32(lblIT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getInstitute(Convert.ToInt32(BlockCode));
                ViewState["GetRole"] = "Institute_Block";
            }
        }

        if (e.CommandName == "BlockClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblTot = ((Label)gvr.FindControl("lblBlockTotal"));
            if (Convert.ToInt32(lblTot.Text.ToString()) != 0)
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
    }

    protected void getThanaWiseRpt(string Blockcode)//Panchayat Records
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

            DataTable dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode});
            lblPrintDateForPoliceStation.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("General_Vadi")).Sum().ToString();
                grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Vibhag_Vadi")).Sum().ToString();
                grdThana.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("institute")).Sum().ToString();

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

        if (e.CommandName == "Thana_Vibhag_Vadi_Click")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string ThanaCode = ((HiddenField)grv.FindControl("hfThanaCode")).Value;
            Label lblDT = ((Label)grv.FindControl("lblThanaVibhag_Vadi"));
            if (Convert.ToInt32(lblDT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getVibhag_Vadi(Convert.ToInt32(ThanaCode));
                ViewState["GetRole"] = "Vibhag_Vadi_Thana";
            }
        }

        if (e.CommandName == "Thana_InstituteClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string ThanaCode = ((HiddenField)grv.FindControl("hfThanaCode")).Value;
            Label lblIT = ((Label)grv.FindControl("lblThanaInstitute"));
            if (Convert.ToInt32(lblIT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getInstitute(Convert.ToInt32(ThanaCode));
                ViewState["GetRole"] = "Institute_Thana";
            }
        }
        if (e.CommandName == "PoliceStationClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblTot = ((Label)gvr.FindControl("lblThanaTotal"));
            if (Convert.ToInt32(lblTot.Text.ToString()) != 0)
            {
                string thanaCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["thana"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["ThanaCode"] = thanaCode;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + "," + "District Name :- " + ViewState["DistrictName"].ToString() +
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
    }

    protected void getPanchayatWiseRpt(string ThanaCode)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "6");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", ThanaCode.Trim());
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");      

            DataTable dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode});
            lblPrintDateForPanchayat.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdPanchayat.Columns[1].FooterText = "Total :";
                grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayat.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("General_Vadi")).Sum().ToString();
                grdPanchayat.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Vibhag_Vadi")).Sum().ToString();
                grdPanchayat.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("institute")).Sum().ToString();

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
        if (e.CommandName == "Panchayat_Vibhag_Vadi_Click")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string DivisionCode = ((HiddenField)grv.FindControl("hfPanchayatCode")).Value;
            Label lblDT = ((Label)grv.FindControl("lblPanchayatVibhag_Vadi"));
            if (Convert.ToInt32(lblDT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getVibhag_Vadi(Convert.ToInt32(DivisionCode));
                ViewState["GetRole"] = "Vibhag_Vadi_Panchayat";
            }
        }

        if (e.CommandName == "Panchayat_InstituteClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string DivisionCode = ((HiddenField)grv.FindControl("hfPanchayatCode")).Value;
            Label lblIT = ((Label)grv.FindControl("lblPanchayatInstitute"));
            if (Convert.ToInt32(lblIT.Text.ToString()) != 0)
            {
                btnback.Visible = true;
                getInstitute(Convert.ToInt32(DivisionCode));
                ViewState["GetRole"] = "Institute_Panchayat";
            }
        }
        if (e.CommandName == "PanchayatClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblTot = ((Label)gvr.FindControl("lblPanchayatTotal"));
            if (Convert.ToInt32(lblTot.Text.ToString()) != 0)
            {
                char[] seprator = { ',' };
                string Panchayatcode = e.CommandArgument.ToString().Trim().Split(seprator)[0];
                string ThanaCode = ViewState["ThanaCode"].ToString() == "" ? Session["Thana_Code"].ToString() : ViewState["ThanaCode"].ToString();
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
                    + ",Thana :- " + ViewState["thana"].ToString() + "<br>Panchayat :-" + ViewState["PanchayatName"].ToString();
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
                getPanchayatWiseData(ThanaCode, Panchayatcode);
            }
        }
    }

    private void getPanchayatWiseData(string ThanaCode, string PanchayatCode)
    {
        try
        {
            Session["mySearchAppData03"] = null;
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "19");
            SqlParameter GetThana_Code = new SqlParameter("@ThanCode", Convert.ToInt32(ThanaCode));
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Convert.ToInt32(PanchayatCode));
            DataTable dt = clsData.GetDataTableWithProc("sp_VadiConsolidateReport", new SqlParameter[] { GetQueryType, GetThana_Code, GetPanchayatCode });
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
        try
        {
            if (ViewState["GetRole"].ToString() == "Vibhag_Vadi_Division" && PanelVibhag_Vadi.Visible == true && grdVibhag_Vadi.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = true;
                grd_Division.Visible = true;

                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = false;
                }

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

                PanelVibhag_Vadi.Visible = false;
                grdVibhag_Vadi.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Vibhag_Vadi_District" && PanelVibhag_Vadi.Visible == true && grdVibhag_Vadi.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelVibhag_Vadi.Visible = false;
                grdVibhag_Vadi.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Vibhag_Vadi_SubDivision" && PanelVibhag_Vadi.Visible == true && grdVibhag_Vadi.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelVibhag_Vadi.Visible = false;
                grdVibhag_Vadi.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Vibhag_Vadi_Block" && PanelVibhag_Vadi.Visible == true && grdVibhag_Vadi.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelVibhag_Vadi.Visible = false;
                grdVibhag_Vadi.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Vibhag_Vadi_Thana" && PanelVibhag_Vadi.Visible == true && grdVibhag_Vadi.Visible == true)
            {
                pnlDist.Visible = false;
                grd_Division.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelVibhag_Vadi.Visible = false;
                grdVibhag_Vadi.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Vibhag_Vadi_Panchayat" && PanelVibhag_Vadi.Visible == true && grdVibhag_Vadi.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelVibhag_Vadi.Visible = false;
                grdVibhag_Vadi.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Institute_Division" && PanelInstitute.Visible == true && grvInstitute.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = true;
                grd_Division.Visible = true;

                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = false;
                }

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

                PanelInstitute.Visible = false;
                grvInstitute.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Institute_District" && PanelInstitute.Visible == true && grvInstitute.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelInstitute.Visible = false;
                grvInstitute.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Institute_SubDivision" && PanelInstitute.Visible == true && grvInstitute.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelInstitute.Visible = false;
                grvInstitute.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Institute_Block" && PanelInstitute.Visible == true && grvInstitute.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelInstitute.Visible = false;
                grvInstitute.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Institute_Thana" && PanelInstitute.Visible == true && grvInstitute.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelInstitute.Visible = false;
                grvInstitute.Visible = false;
            }
            else if (ViewState["GetRole"].ToString() == "Institute_Panchayat" && PanelInstitute.Visible == true && grvInstitute.Visible == true)
            {
                ViewState["GetRole"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
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

                PanelInstitute.Visible = false;
                grvInstitute.Visible = false;
            }
            else if (pnlDistrict.Visible == true && grdDistrict.Visible == true)
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
            else if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
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
            else if (pnlCircle.Visible == true && grdCircle.Visible == true)
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
            else if (pnlthana.Visible == true && grdThana.Visible == true)
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
            else if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
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
            else if (Pnlsearch.Visible == true && GridView1.Visible == true)
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
}