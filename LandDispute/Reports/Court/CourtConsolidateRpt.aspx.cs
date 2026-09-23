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

public partial class LandDispute_Report_ApplicationConsolidateRpt : System.Web.UI.Page
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
                ViewState["GetNayalaya"] = "";
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
                //DIG login role
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
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });
            lblPrintDateForDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;              
                grd_Division.Columns[1].FooterText = "Total :";
                grd_Division.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_Division.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("raajasv_nyaayaalay")).Sum().ToString();
                grd_Division.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("vyavahaara_nyaayaalay")).Sum().ToString();           
                grd_Division.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LokShikayat_Nivaran_nyaayaalay")).Sum().ToString();
                grd_Division.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("uchcha_nyaayaalay")).Sum().ToString();
                grd_Division.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sarvochcha_nyaayaalay")).Sum().ToString();
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

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;

        }
        catch (Exception ex)
        { }
    }
    protected void grd_Division_RowCommand(object sender, GridViewCommandEventArgs e)
    {      if (e.CommandName == "Division_Raajasv_nyaayaalay_Click")
             {                
                    GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    string DivisionCode =  ((HiddenField)grv.FindControl("hfDivisionCode")).Value;
                    Label lblRN = ((Label)grv.FindControl("lblDivisionraajasv_nyaayaalay"));
                    if(Convert.ToInt32(lblRN.Text.ToString())!=0)
                    {
                        ViewState["DivisionCode"] = DivisionCode;
                        btnback.Visible = true;
                        getRaajasv_nyaayaalay(Convert.ToInt32(DivisionCode));
                        ViewState["GetNayalaya"] = "Raajasv_nyaayaalay_Division";
                    }                  
             }
            if (e.CommandName == "Division_Vyavahaara_nyaayaalayClick")
                {
                    GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    string DivisionCode = ((HiddenField)grv.FindControl("hfDivisionCode")).Value;
                    Label lblVN = ((Label)grv.FindControl("lblDivisionvyavahaara_nyaayaalay"));
                    if (Convert.ToInt32(lblVN.Text.ToString()) != 0)
                    {
                        ViewState["DivisionCode"] = DivisionCode;
                        btnback.Visible = true;
                        getVyavahaara_nyaayaalay(Convert.ToInt32(DivisionCode));
                        ViewState["GetNayalaya"] = "Vyavahaara_nyaayaalay_Division";
                    }
               }       
            if (e.CommandName == "DivisionClick")
            {
                    lbltext.Text = "";
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label lblRN = ((Label)gvr.FindControl("lblDivisionraajasv_nyaayaalay"));
                    Label lblVN = ((Label)gvr.FindControl("lblDivisionvyavahaara_nyaayaalay"));          
                    Label lblLSNN = ((Label)gvr.FindControl("lblDivisionLokShikayat_Nivaran_nyaayaalay"));
                    Label LblUN = ((Label)gvr.FindControl("lblDivisionuchcha_nyaayaalay"));
                    Label lblSN = ((Label)gvr.FindControl("lblDivisionsarvochcha_nyaayaalay"));
                    int total = Convert.ToInt32(lblRN.Text.ToString())+ Convert.ToInt32(lblVN.Text.ToString())
                                + Convert.ToInt32(lblLSNN.Text.ToString())
                                + Convert.ToInt32(LblUN.Text.ToString())+ Convert.ToInt32(lblSN.Text.ToString());
                    if (total!=0)
                    {
                        string DivisionCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                        ViewState["DIVISIONName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                        ViewState["DivisionCode"] = DivisionCode;
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
    protected void getRaajasv_nyaayaalay(int Code)
    {
        DataTable dt = new DataTable();
        if (pnlDist.Visible == true && grd_Division.Visible ==true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "7");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode });
        }
        if (pnlDistrict.Visible == true && pnlDistrict.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "8");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDistrictCode });
        }
        if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "9");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetSubDivisionCode });
        }
        if (pnlCircle.Visible == true && grdCircle.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "10");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetBlockCode });
        }
        if (pnlthana.Visible == true && grdThana.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "11");
            SqlParameter GetThanaCode = new SqlParameter("@ThanCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetThanaCode });
        }
        if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "12");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetPanchayatCode });
        }      
        if (dt.Rows.Count > 0)
        {
            grdraajasv_nyaayaalay.Columns[1].FooterText = "Total :";
            grdraajasv_nyaayaalay.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("total")).Sum().ToString();
            grdraajasv_nyaayaalay.DataSource = dt;
            grdraajasv_nyaayaalay.DataBind();
        }
        else
        {
            grdraajasv_nyaayaalay.DataSource = null;
            grdraajasv_nyaayaalay.DataBind();
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

        Pnlsearch.Visible = false;
        GridView1.Visible = false;

        PanelVyavahaara_nyaayaalay.Visible = false;
        grvVyavahaara_nyaayaalay.Visible = false;

        Panelraajasv_nyaayaalay.Visible = true;
        grdraajasv_nyaayaalay.Visible = true;
    }
    protected void getVyavahaara_nyaayaalay(int Code)
    {
        DataTable dt = new DataTable();
        if (pnlDist.Visible == true && grd_Division.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "13");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode });
        }
        if (pnlDistrict.Visible == true && pnlDistrict.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "14");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDistrictCode });
        }
        if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "15");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetSubDivisionCode });
        }
        if (pnlCircle.Visible == true && grdCircle.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "16");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetBlockCode });
        }
        if (pnlthana.Visible == true && grdThana.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "17");
            SqlParameter GetThanaCode = new SqlParameter("@ThanCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetThanaCode });
        }
        if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "18");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Code);
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetPanchayatCode });
        }
        if (dt.Rows.Count > 0)
        {
            grvVyavahaara_nyaayaalay.Columns[1].FooterText = "Total :";
            grvVyavahaara_nyaayaalay.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("total")).Sum().ToString();
            grvVyavahaara_nyaayaalay.DataSource = dt;
            grvVyavahaara_nyaayaalay.DataBind();
        }
        else
        {
            grvVyavahaara_nyaayaalay.DataSource = null;
            grvVyavahaara_nyaayaalay.DataBind();
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

        Pnlsearch.Visible = false;
        GridView1.Visible = false;

        Panelraajasv_nyaayaalay.Visible = false;
        grdraajasv_nyaayaalay.Visible = false;
      
        PanelVyavahaara_nyaayaalay.Visible = true;
        grvVyavahaara_nyaayaalay.Visible = true;
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
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode, Rangeid });
            lblPrintDateForDistrict.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdDistrict.Columns[1].FooterText = "Total :";
                grdDistrict.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdDistrict.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("raajasv_nyaayaalay")).Sum().ToString();
                grdDistrict.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("vyavahaara_nyaayaalay")).Sum().ToString();            
                grdDistrict.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LokShikayat_Nivaran_nyaayaalay")).Sum().ToString();
                grdDistrict.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("uchcha_nyaayaalay")).Sum().ToString();
                grdDistrict.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sarvochcha_nyaayaalay")).Sum().ToString();
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

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "District_Raajasv_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string DistrictCode = ((HiddenField)grv.FindControl("hfDistrictCode")).Value;
            Label lblRN = ((Label)grv.FindControl("lblDistrictRaajasv_nyaayaalay"));
            if (Convert.ToInt32(lblRN.Text.ToString()) != 0)
            {
                ViewState["DistcodeCode"] = DistrictCode;
                btnback.Visible = true;
                getRaajasv_nyaayaalay(Convert.ToInt32(DistrictCode));
                ViewState["GetNayalaya"] = "Raajasv_nyaayaalay_District";
            }            
        }
        if (e.CommandName == "Districtvyavahaara_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string DistrictCode = ((HiddenField)grv.FindControl("hfDistrictCode")).Value;
            Label lblVN = ((Label)grv.FindControl("lblDistrictVyavahaara_nyaayaalay"));
            if (Convert.ToInt32(lblVN.Text.ToString()) != 0)
            {
                ViewState["DistcodeCode"] = DistrictCode;
                btnback.Visible = true;
                getVyavahaara_nyaayaalay(Convert.ToInt32(DistrictCode));
                ViewState["GetNayalaya"] = "Vyavahaara_nyaayaalay_District";
            }
        }
        if (e.CommandName == "DistrictClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRN = ((Label)gvr.FindControl("lblDistrictRaajasv_nyaayaalay"));
            Label lblVN = ((Label)gvr.FindControl("lblDistrictVyavahaara_nyaayaalay"));      
            Label lblLSNN = ((Label)gvr.FindControl("lblDistrictLokShikayat_Nivaran_nyaayaalay"));
            Label LblUN = ((Label)gvr.FindControl("lblDistrictUchcha_nyaayaalay"));
            Label lblSN = ((Label)gvr.FindControl("lblDistrictSarvochcha_nyaayaalay"));
            int total = Convert.ToInt32(lblRN.Text.ToString()) + Convert.ToInt32(lblVN.Text.ToString())
                        + Convert.ToInt32(lblLSNN.Text.ToString())
                        + Convert.ToInt32(LblUN.Text.ToString()) + Convert.ToInt32(lblSN.Text.ToString());
            if (total != 0)
            {
                string Distcode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["DistrictName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["DistcodeCode"] = Distcode;
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
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode","0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", DistCode.Trim());
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });
            lblPrintDateforSubDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdSubDivision.Columns[1].FooterText = "Total :";
                grdSubDivision.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdSubDivision.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("raajasv_nyaayaalay")).Sum().ToString();
                grdSubDivision.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("vyavahaara_nyaayaalay")).Sum().ToString();            
                grdSubDivision.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LokShikayat_Nivaran_nyaayaalay")).Sum().ToString();
                grdSubDivision.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("uchcha_nyaayaalay")).Sum().ToString();
                grdSubDivision.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sarvochcha_nyaayaalay")).Sum().ToString();
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

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdSubDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SubDivisionRaajasv_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string SubDivisionCode = ((HiddenField)grv.FindControl("hfSubDivision")).Value;
            Label lblRN = ((Label)grv.FindControl("lblSubDivisionRaajasv_nyaayaalay"));
            if (Convert.ToInt32(lblRN.Text.ToString()) != 0)
            {
                ViewState["subDivsionCode"] = SubDivisionCode;
                btnback.Visible = true;
                getRaajasv_nyaayaalay(Convert.ToInt32(SubDivisionCode));
                ViewState["GetNayalaya"] = "Raajasv_nyaayaalay_SubDivision";
            }           
        }
        if (e.CommandName == "SubDivision_Vyavahaara_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string SubDivisionCode = ((HiddenField)grv.FindControl("hfSubDivision")).Value;
            Label lblVN = ((Label)grv.FindControl("lblSubDivisionVyavahaara_nyaayaalay"));
            if (Convert.ToInt32(lblVN.Text.ToString()) != 0)
            {
                ViewState["subDivsionCode"] = SubDivisionCode;
                btnback.Visible = true;
                getVyavahaara_nyaayaalay(Convert.ToInt32(SubDivisionCode));
                ViewState["GetNayalaya"] = "Vyavahaara_nyaayaalay_SubDivision";
            }
        }
        if (e.CommandName == "SubDivisionClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRN = ((Label)gvr.FindControl("lblSubDivisionRaajasv_nyaayaalay"));
            Label lblVN = ((Label)gvr.FindControl("lblSubDivisionVyavahaara_nyaayaalay"));            
            Label lblLSNN = ((Label)gvr.FindControl("lblSubDivisionLokShikayat_Nivaran_nyaayaalay"));
            Label LblUN = ((Label)gvr.FindControl("lblSubDivisionUchcha_nyaayaalay"));
            Label lblSN = ((Label)gvr.FindControl("lblSubDivisionSarvochcha_nyaayaalay"));
            int total = Convert.ToInt32(lblRN.Text.ToString()) + Convert.ToInt32(lblVN.Text.ToString())
                       + Convert.ToInt32(lblLSNN.Text.ToString())
                        + Convert.ToInt32(LblUN.Text.ToString()) + Convert.ToInt32(lblSN.Text.ToString());
            if (total != 0)
            {
                string subDivsionCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["SubDivisionName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["subDivsionCode"] = subDivsionCode;
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
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });
            lblPrintDateForCircle.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("raajasv_nyaayaalay")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("vyavahaara_nyaayaalay")).Sum().ToString();
                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LokShikayat_Nivaran_nyaayaalay")).Sum().ToString();
                grdCircle.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("uchcha_nyaayaalay")).Sum().ToString();
                grdCircle.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sarvochcha_nyaayaalay")).Sum().ToString();
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

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "BlockRaajasv_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string BlockCode = ((HiddenField)grv.FindControl("hfBlockCode")).Value;
            Label lblRN = ((Label)grv.FindControl("lblBlockRaajasv_nyaayaalay"));
            if (Convert.ToInt32(lblRN.Text.ToString()) != 0)
            {
                ViewState["BlockCode"] = BlockCode;
                btnback.Visible = true;
                getRaajasv_nyaayaalay(Convert.ToInt32(BlockCode));
                ViewState["GetNayalaya"] = "Raajasv_nyaayaalay_Block";
            }           
        }
        if (e.CommandName == "BlockVyavahaara_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string BlockCode = ((HiddenField)grv.FindControl("hfBlockCode")).Value;
            Label lblVN = ((Label)grv.FindControl("lblBlockVyavahaara_nyaayaalay"));
            if (Convert.ToInt32(lblVN.Text.ToString()) != 0)
            {
                ViewState["BlockCode"] = BlockCode;
                btnback.Visible = true;
                getVyavahaara_nyaayaalay(Convert.ToInt32(BlockCode));
                ViewState["GetNayalaya"] = "Vyavahaara_nyaayaalay_Block";
            }
        }
        if (e.CommandName == "BlockClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRN = ((Label)gvr.FindControl("lblBlockRaajasv_nyaayaalay"));
            Label lblVN = ((Label)gvr.FindControl("lblBlockVyavahaara_nyaayaalay"));        
            Label lblLSNN = ((Label)gvr.FindControl("lblBlockLokShikayat_Nivaran_nyaayaalay"));
            Label LblUN = ((Label)gvr.FindControl("lblBlockUchcha_nyaayaalay"));
            Label lblSN = ((Label)gvr.FindControl("lblBlockSarvochcha_nyaayaalay"));
            int total = Convert.ToInt32(lblRN.Text.ToString()) + Convert.ToInt32(lblVN.Text.ToString())
                       + Convert.ToInt32(lblLSNN.Text.ToString())
                        + Convert.ToInt32(LblUN.Text.ToString()) + Convert.ToInt32(lblSN.Text.ToString());
            if (total != 0)
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
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });
            lblPrintDateForPoliceStation.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("raajasv_nyaayaalay")).Sum().ToString();
                grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("vyavahaara_nyaayaalay")).Sum().ToString();
                grdThana.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LokShikayat_Nivaran_nyaayaalay")).Sum().ToString();
                grdThana.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("uchcha_nyaayaalay")).Sum().ToString();
                grdThana.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sarvochcha_nyaayaalay")).Sum().ToString();
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

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "PoliceStationRaajasv_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string ThanaCode = ((HiddenField)grv.FindControl("hfThanaCode")).Value;
            Label lblRN = ((Label)grv.FindControl("lblThanaRaajasv_nyaayaalay"));
            if (Convert.ToInt32(lblRN.Text.ToString()) != 0)
            {
                ViewState["ThanaCode"] = ThanaCode;
                btnback.Visible = true;
                getRaajasv_nyaayaalay(Convert.ToInt32(ThanaCode));
                ViewState["GetNayalaya"] = "Raajasv_nyaayaalay_Thana";
            }        
        }
        if (e.CommandName == "Thana_Vyavahaara_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string ThanaCode = ((HiddenField)grv.FindControl("hfThanaCode")).Value;
            Label lblVN = ((Label)grv.FindControl("lblThanaVyavahaara_nyaayaalay"));
            if (Convert.ToInt32(lblVN.Text.ToString()) != 0)
            {
                ViewState["ThanaCode"] = ThanaCode;
                btnback.Visible = true;
                getVyavahaara_nyaayaalay(Convert.ToInt32(ThanaCode));
                ViewState["GetNayalaya"] = "Vyavahaara_nyaayaalay_Thana";
            }
        }
        if (e.CommandName == "PoliceStationClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRN = ((Label)gvr.FindControl("lblThanaRaajasv_nyaayaalay"));
            Label lblVN = ((Label)gvr.FindControl("lblThanaVyavahaara_nyaayaalay"));          
            Label lblLSNN = ((Label)gvr.FindControl("lblThanaLokShikayat_Nivaran_nyaayaalay"));
            Label LblUN = ((Label)gvr.FindControl("lblThanaUchcha_nyaayaalay"));
            Label lblSN = ((Label)gvr.FindControl("lblThanaSarvochcha_nyaayaalay"));
            int total = Convert.ToInt32(lblRN.Text.ToString()) + Convert.ToInt32(lblVN.Text.ToString())
                        + Convert.ToInt32(lblLSNN.Text.ToString())
                        + Convert.ToInt32(LblUN.Text.ToString()) + Convert.ToInt32(lblSN.Text.ToString());
            if (total != 0)
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
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });
            lblPrintDateForPanchayat.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdPanchayat.Columns[1].FooterText = "Total :";
                grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayat.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("raajasv_nyaayaalay")).Sum().ToString();
                grdPanchayat.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("vyavahaara_nyaayaalay")).Sum().ToString();
                grdPanchayat.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LokShikayat_Nivaran_nyaayaalay")).Sum().ToString();
                grdPanchayat.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("uchcha_nyaayaalay")).Sum().ToString();
                grdPanchayat.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sarvochcha_nyaayaalay")).Sum().ToString();
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

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdPanchayat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Panchayat_Raajasv_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string PanchayatCode = ((HiddenField)grv.FindControl("hfPanchayatCode")).Value;
            Label lblRN = ((Label)grv.FindControl("lblPanchayatRaajasv_nyaayaalay"));
            if (Convert.ToInt32(lblRN.Text.ToString()) != 0)
            {
                ViewState["Panchayatcode"] = PanchayatCode;
                btnback.Visible = true;
                getRaajasv_nyaayaalay(Convert.ToInt32(PanchayatCode));
                ViewState["GetNayalaya"] = "Raajasv_nyaayaalay_Panchayat";
            }
        }
        if (e.CommandName == "Panchayat_Vyavahaara_nyaayaalayClick")
        {
            GridViewRow grv = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            string PanchayatCode = ((HiddenField)grv.FindControl("hfPanchayatCode")).Value;
            Label lblVN = ((Label)grv.FindControl("lblPanchayatVyavahaara_nyaayaalay"));
            if (Convert.ToInt32(lblVN.Text.ToString()) != 0)
            {
                ViewState["Panchayatcode"] = PanchayatCode;
                btnback.Visible = true;
                getVyavahaara_nyaayaalay(Convert.ToInt32(PanchayatCode));
                ViewState["GetNayalaya"] = "Vyavahaara_nyaayaalay_Panchayat";
            }
        }
        if (e.CommandName == "PanchayatClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblRN = ((Label)gvr.FindControl("lblPanchayatRaajasv_nyaayaalay"));
            Label lblVN = ((Label)gvr.FindControl("lblPanchayatVyavahaara_nyaayaalay"));         
            Label lblLSNN = ((Label)gvr.FindControl("lblPanchayatLokShikayat_Nivaran_nyaayaalay"));
            Label LblUN = ((Label)gvr.FindControl("lblPanchayatUchcha_nyaayaalay"));
            Label lblSN = ((Label)gvr.FindControl("lblPanchayatSarvochcha_nyaayaalay"));
            int total = Convert.ToInt32(lblRN.Text.ToString()) + Convert.ToInt32(lblVN.Text.ToString())
                        + Convert.ToInt32(lblLSNN.Text.ToString())
                        + Convert.ToInt32(LblUN.Text.ToString()) + Convert.ToInt32(lblSN.Text.ToString());
            if (total != 0)
            {
                char[] seprator = { ',' };
                string Panchayatcode = e.CommandArgument.ToString().Trim().Split(seprator)[0];
                string Thanacode = ViewState["ThanaCode"].ToString() == "" ? Session["Thana_Code"].ToString() : ViewState["ThanaCode"].ToString();
                ViewState["PanchayatName"] = e.CommandArgument.ToString().Trim().Split(seprator)[1];
                ViewState["Panchayatcode"] = Panchayatcode;
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
                getPanchayatWiseData(Thanacode,Panchayatcode);
            }
        }
    }

    private void getPanchayatWiseData(string Blockcode, string Panchayatcode)
    {
        try
        {
            Session["mySearchAppData03"] = null;
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "19");            
            SqlParameter GetThana_code = new SqlParameter("@ThanCode", Convert.ToInt32(Blockcode));
            SqlParameter GetPanchayat_Code = new SqlParameter("@PanchayatCode", Convert.ToInt32(Panchayatcode));          
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQueryType, GetThana_code, GetPanchayat_Code});
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

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
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
            if(ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Division" && Panelraajasv_nyaayaalay.Visible == true && grdraajasv_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_District" && Panelraajasv_nyaayaalay.Visible == true && grdraajasv_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
                pnlDist.Visible = false;
                grd_Division.Visible = false;
                if (ViewState["RoleStepBack"].ToString()=="4")
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

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_SubDivision" && Panelraajasv_nyaayaalay.Visible == true && grdraajasv_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Block" && Panelraajasv_nyaayaalay.Visible == true && grdraajasv_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Thana" && Panelraajasv_nyaayaalay.Visible == true && grdraajasv_nyaayaalay.Visible == true)
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

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Panchayat" && Panelraajasv_nyaayaalay.Visible == true && grdraajasv_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }

            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Division" && PanelVyavahaara_nyaayaalay.Visible == true && grvVyavahaara_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_District" && PanelVyavahaara_nyaayaalay.Visible == true && grvVyavahaara_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_SubDivision" && PanelVyavahaara_nyaayaalay.Visible == true && grvVyavahaara_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Block" && PanelVyavahaara_nyaayaalay.Visible == true && grvVyavahaara_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Thana" && PanelVyavahaara_nyaayaalay.Visible == true && grvVyavahaara_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Panchayat" && PanelVyavahaara_nyaayaalay.Visible == true && grvVyavahaara_nyaayaalay.Visible == true)
            {
                ViewState["GetNayalaya"] = "";
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

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }

            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Division" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = true;
                grdraajasv_nyaayaalay.Visible = true;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;               
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_District" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = true;
                grdraajasv_nyaayaalay.Visible = true;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_SubDivision" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = true;
                grdraajasv_nyaayaalay.Visible = true;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Block" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = true;
                grdraajasv_nyaayaalay.Visible = true;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Thana" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = true;
                grdraajasv_nyaayaalay.Visible = true;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Panchayat" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = true;
                grdraajasv_nyaayaalay.Visible = true;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }

            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Division" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = true;
                grvVyavahaara_nyaayaalay.Visible = true;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_District" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = true;
                grvVyavahaara_nyaayaalay.Visible = true;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_SubDivision" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = true;
                grvVyavahaara_nyaayaalay.Visible = true;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Block" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = true;
                grvVyavahaara_nyaayaalay.Visible = true;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Thana" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = true;
                grvVyavahaara_nyaayaalay.Visible = true;
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Panchayat" && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                btnback.Visible = true;
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = true;
                grvVyavahaara_nyaayaalay.Visible = true;
            }

            else if (((ViewState["GetNayalaya"].ToString() == "DivisionLokShikayat_Nivaran_nyaayaalay")|| (ViewState["GetNayalaya"].ToString() == "Divisionuchcha_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Divisionsarvochcha_nyaayaalay")) && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = false;
                }
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (((ViewState["GetNayalaya"].ToString() == "DistrictLokShikayat_Nivaran_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Districtnuchcha_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Districtsarvochcha_nyaayaalay")) && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (((ViewState["GetNayalaya"].ToString() == "SubDivisionLokShikayat_Nivaran_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "SubDivisionnuchcha_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "SubDivisionsarvochcha_nyaayaalay")) && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (((ViewState["GetNayalaya"].ToString() == "BlockLokShikayat_Nivaran_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Blockuchcha_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Blocksarvochcha_nyaayaalay")) && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (((ViewState["GetNayalaya"].ToString() == "ThanaLokShikayat_Nivaran_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Thanauchcha_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Thanasarvochcha_nyaayaalay")) && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
            }
            else if (((ViewState["GetNayalaya"].ToString() == "PanchayatLokShikayat_Nivaran_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Panchayatuchcha_nyaayaalay") || (ViewState["GetNayalaya"].ToString() == "Panchayatsarvochcha_nyaayaalay")) && Pnlsearch.Visible == true && GridView1.Visible == true)
            {
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
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

                Pnlsearch.Visible = false;
                GridView1.Visible = false;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
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

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = lbltext.Text = "Division Name:-" + ViewState["DIVISIONName"].ToString() + ",District Name" + ViewState["DistrictName"].ToString() + "<br/>SubDivisionName:-"
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

    protected void lnkTotalRajashvnayala_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;     
        string CourtypeID = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        if (Convert.ToInt32(Total) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "20");
            if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Division")
            {
               
                SqlParameter _Type = new SqlParameter("@Type", "Division");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["DivisionCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "1");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_District")
            {
              
                SqlParameter _Type = new SqlParameter("@Type", "District");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["DistcodeCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "1");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_SubDivision")
            {            
                SqlParameter _Type = new SqlParameter("@Type", "SubDivision");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["subDivsionCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "1");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Block")
            {

              
                SqlParameter _Type = new SqlParameter("@Type", "Block");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["BlockCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "1");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
           else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Thana")
            {
               
                SqlParameter _Type = new SqlParameter("@Type", "Thana");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["ThanaCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "1");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Raajasv_nyaayaalay_Panchayat")
            {

               
                SqlParameter _Type = new SqlParameter("@Type", "Panchayat");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["Panchayatcode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "1");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
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

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
           
        }

    }
    protected void lnkTotalVahavarnayala_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string CourtypeID = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        if (Convert.ToInt32(Total) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "20");
            if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Division")
            {
                
                SqlParameter _Type = new SqlParameter("@Type", "Division");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["DivisionCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "2");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_District")
            {
                
                SqlParameter _Type = new SqlParameter("@Type", "District");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["DistcodeCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "2");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_SubDivision")
            {
               
                SqlParameter _Type = new SqlParameter("@Type", "SubDivision");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["subDivsionCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "2");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Block")
            {
                
                SqlParameter _Type = new SqlParameter("@Type", "Block");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["BlockCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "2");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Thana")
            {
               
                SqlParameter _Type = new SqlParameter("@Type", "Thana");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["ThanaCode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "2");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
            else if (ViewState["GetNayalaya"].ToString() == "Vyavahaara_nyaayaalay_Panchayat")
            {
               
                SqlParameter _Type = new SqlParameter("@Type", "Panchayat");
                SqlParameter _Code = new SqlParameter("@Code", ViewState["Panchayatcode"].ToString());
                SqlParameter _courtID = new SqlParameter("@courtID", "2");
                SqlParameter _courtTypeID = new SqlParameter("@courtTypeID", CourtypeID);
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID, _courtTypeID });
            }
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

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;

        }

    }

    protected void lnkDivisionLokShikayat_Nivaran_nyaayaalay_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string DivCode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        if (Convert.ToInt32(Total) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
            SqlParameter _Type = new SqlParameter("@Type", "Division");
            SqlParameter _Code = new SqlParameter("@Code", DivCode);
            SqlParameter _courtID = new SqlParameter("@courtID", "4");          
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

            btnback.Visible = true;
            Pnlsearch.Visible = true;
            GridView1.Visible = true;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
            ViewState["GetNayalaya"] = "DivisionLokShikayat_Nivaran_nyaayaalay";
        }
    }
    protected void lnkDivisionuchcha_nyaayaalay_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string DivCode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        if (Convert.ToInt32(Total) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
            SqlParameter _Type = new SqlParameter("@Type", "Division");
            SqlParameter _Code = new SqlParameter("@Code", DivCode);
            SqlParameter _courtID = new SqlParameter("@courtID", "5");
          
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

            btnback.Visible = true;
            Pnlsearch.Visible = true;
            GridView1.Visible = true;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
            ViewState["GetNayalaya"] = "Divisionuchcha_nyaayaalay";
        }
    }
    protected void lnkDivisionsarvochcha_nyaayaalay_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string DivCode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        if (Convert.ToInt32(Total) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
            SqlParameter _Type = new SqlParameter("@Type", "Division");
            SqlParameter _Code = new SqlParameter("@Code", DivCode);
            SqlParameter _courtID = new SqlParameter("@courtID", "6");
           
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

            btnback.Visible = true;
            Pnlsearch.Visible = true;
            GridView1.Visible = true;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
            ViewState["GetNayalaya"] = "Divisionsarvochcha_nyaayaalay";
        }
    }


    protected void lnkDistrictLokShikayat_Nivaran_nyaayaalay_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string DISTRICTCODE = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        if (Convert.ToInt32(Total) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
            SqlParameter _Type = new SqlParameter("@Type", "District");
            SqlParameter _Code = new SqlParameter("@Code", DISTRICTCODE);
            SqlParameter _courtID = new SqlParameter("@courtID", "4");
            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

            btnback.Visible = true;
            Pnlsearch.Visible = true;
            GridView1.Visible = true;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
            ViewState["GetNayalaya"] = "DistrictLokShikayat_Nivaran_nyaayaalay";
        }
    }
    protected void lnkDistrictUchcha_nyaayaalay_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string DISTRICTCODE = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        if (Convert.ToInt32(Total) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
            SqlParameter _Type = new SqlParameter("@Type", "District");
            SqlParameter _Code = new SqlParameter("@Code", DISTRICTCODE);
            SqlParameter _courtID = new SqlParameter("@courtID", "5");

            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

            btnback.Visible = true;
            Pnlsearch.Visible = true;
            GridView1.Visible = true;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
            ViewState["GetNayalaya"] = "Districtnuchcha_nyaayaalay";
        }
    }
    protected void lnkDistrictSarvochcha_nyaayaalay_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        string DISTRICTCODE = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        if (Convert.ToInt32(Total) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
            SqlParameter _Type = new SqlParameter("@Type", "District");
            SqlParameter _Code = new SqlParameter("@Code", DISTRICTCODE);
            SqlParameter _courtID = new SqlParameter("@courtID", "6");

            dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

            btnback.Visible = true;
            Pnlsearch.Visible = true;
            GridView1.Visible = true;

            Panelraajasv_nyaayaalay.Visible = false;
            grdraajasv_nyaayaalay.Visible = false;

            PanelVyavahaara_nyaayaalay.Visible = false;
            grvVyavahaara_nyaayaalay.Visible = false;
            ViewState["GetNayalaya"] = "Districtsarvochcha_nyaayaalay";
        }
    }

    protected void lnkSubDivisionLokShikayat_Nivaran_nyaayaalay_Click(object sender, EventArgs e)
    {      
            LinkButton lnk = sender as LinkButton;
            string Sd_Code = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "SubDivision");
                SqlParameter _Code = new SqlParameter("@Code", Sd_Code);
                SqlParameter _courtID = new SqlParameter("@courtID", "4");
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "SubDivisionLokShikayat_Nivaran_nyaayaalay";
            }
    }
    protected void lnkSubDivisionUchcha_nyaayaalay_Click(object sender, EventArgs e)
    {
       
            LinkButton lnk = sender as LinkButton;
            string Sd_Code = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "SubDivision");
                SqlParameter _Code = new SqlParameter("@Code", Sd_Code);
                SqlParameter _courtID = new SqlParameter("@courtID", "5");

                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "SubDivisionnuchcha_nyaayaalay";
            }
      
    }
    protected void lnkSubDivisionSarvochcha_nyaayaalay_Click(object sender, EventArgs e)
    {      
            LinkButton lnk = sender as LinkButton;
            string Sd_Code = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "SubDivision");
                SqlParameter _Code = new SqlParameter("@Code", Sd_Code);
                SqlParameter _courtID = new SqlParameter("@courtID", "6");

                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "SubDivisionsarvochcha_nyaayaalay";
            }      
    }

   
    protected void lnkBlockLokShikayat_Nivaran_nyaayaalay_Click(object sender, EventArgs e)
    {
       
            LinkButton lnk = sender as LinkButton;
            string Block = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Block");
                SqlParameter _Code = new SqlParameter("@Code", Block);
                SqlParameter _courtID = new SqlParameter("@courtID", "4");
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "BlockLokShikayat_Nivaran_nyaayaalay";
            }
     
    }
    protected void lnkBlockUchcha_nyaayaalay_Click(object sender, EventArgs e)
    {
    
            LinkButton lnk = sender as LinkButton;
            string Block = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Block");
                SqlParameter _Code = new SqlParameter("@Code", Block);
                SqlParameter _courtID = new SqlParameter("@courtID", "5");

                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "Blockuchcha_nyaayaalay";
            }
    }
    protected void lnkBlockSarvochcha_nyaayaalay_Click(object sender, EventArgs e)
    {      
            LinkButton lnk = sender as LinkButton;
            string Block = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Block");
                SqlParameter _Code = new SqlParameter("@Code", Block);
                SqlParameter _courtID = new SqlParameter("@courtID", "6");

                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "Blocksarvochcha_nyaayaalay";
            }
      
    }


    protected void lnkThanaLokShikayat_Nivaran_nyaayaalay_Click(object sender, EventArgs e)
    {
        
            LinkButton lnk = sender as LinkButton;
            string Thana = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Thana");
                SqlParameter _Code = new SqlParameter("@Code", Thana);
                SqlParameter _courtID = new SqlParameter("@courtID", "4");
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "ThanaLokShikayat_Nivaran_nyaayaalay";
            }

      
    }
    protected void lnkThanaUchcha_nyaayaalay_Click(object sender, EventArgs e)
    {

            LinkButton lnk = sender as LinkButton;
            string Thana = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Thana");
                SqlParameter _Code = new SqlParameter("@Code", Thana);
                SqlParameter _courtID = new SqlParameter("@courtID", "5");

                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "Thanauchcha_nyaayaalay";
            }
    
    }
    protected void lnkThanaSarvochcha_nyaayaalay_Click(object sender, EventArgs e)
    {
      
            LinkButton lnk = sender as LinkButton;
            string Thana = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Thana");
                SqlParameter _Code = new SqlParameter("@Code", Thana);
                SqlParameter _courtID = new SqlParameter("@courtID", "6");

                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "Thanasarvochcha_nyaayaalay";
            }

    
    }

    protected void lnkPanchayatLokShikayat_Nivaran_nyaayaalay_Click(object sender, EventArgs e)
    {

            LinkButton lnk = sender as LinkButton;
            string Panchayat = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Panchayat");
                SqlParameter _Code = new SqlParameter("@Code", Panchayat);
                SqlParameter _courtID = new SqlParameter("@courtID", "4");
                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "PanchayatLokShikayat_Nivaran_nyaayaalay";
            }

      
    }
    protected void lnkPanchayatUchcha_nyaayaalay_Click(object sender, EventArgs e)
    {
            LinkButton lnk = sender as LinkButton;
            string Panchayat = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Panchayat");
                SqlParameter _Code = new SqlParameter("@Code", Panchayat);
                SqlParameter _courtID = new SqlParameter("@courtID", "5");

                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "Panchayatuchcha_nyaayaalay";
            }
    
    }
    protected void lnkPanchayatSarvochcha_nyaayaalay_Click(object sender, EventArgs e)
    {
      
            LinkButton lnk = sender as LinkButton;
            string Panchayat = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            string Total = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            if (Convert.ToInt32(Total) > 0)
            {
                DataTable dt = new DataTable();
                SqlParameter GetQuery = new SqlParameter("@QueryType", "21");
                SqlParameter _Type = new SqlParameter("@Type", "Panchayat");
                SqlParameter _Code = new SqlParameter("@Code", Panchayat);
                SqlParameter _courtID = new SqlParameter("@courtID", "6");

                dt = clsData.GetDataTableWithProc("Sp_GetCourtConsolidateReport", new SqlParameter[] { GetQuery, _Type, _Code, _courtID });
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

                btnback.Visible = true;
                Pnlsearch.Visible = true;
                GridView1.Visible = true;

                Panelraajasv_nyaayaalay.Visible = false;
                grdraajasv_nyaayaalay.Visible = false;

                PanelVyavahaara_nyaayaalay.Visible = false;
                grvVyavahaara_nyaayaalay.Visible = false;
                ViewState["GetNayalaya"] = "Panchayatsarvochcha_nyaayaalay";
            }
     }

}