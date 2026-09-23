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
using System.Security.Policy;

public partial class LandDispute_Report_ApplicationConsolidateRpt : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
   
    int RoleStepBack=0;
    protected void Page_UnLoad(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
            if (!IsPostBack)
            {
            if (Session["Role"] != null)
                {
                txtFromdate.Attributes.Add("readonly", "readonly");
                txTodate.Attributes.Add("readonly", "readonly");
                ViewState["PageIndex"] = "1";
                ViewState["lnkClick"] = "";
                ViewState["DIVISIONName"] = "";
                ViewState["DistrictName"] = "";
                ViewState["SubDivisionName"] = "";
                ViewState["BlockName"] = "";
                ViewState["thana"] = "";
                ViewState["PanchayatName"] = "";
                ViewState["RoleStepBack"] = "";
                ViewState["ThanaCode"] = "";
                //9 division wise login role
                if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR" || Session["Role"].ToString() == "ADGLAW") //
                {
                    ViewState["RoleStepBack"] = 5 + "";
                    bindDivision();
                }
                //Commsionary login role
                else if ( Session["Role"].ToString() == "COM") 
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
                    getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"]));
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("~/Login_Default.aspx");
            }
        }    
    }

    void bindDivision()
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "1");
            SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text==""?null: Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetFromdate, GetTodate });
            lblPrintDateforDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grd_Division.Columns[1].FooterText = "Total :";                
                grd_Division.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_Division.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grd_Division.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grd_Division.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grd_Division.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                grd_Division.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grd_Division.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grd_Division.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grd_Division.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();

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
        if (e.CommandName == "DivisionClick")
        {
            //GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //Label total = ((Label)gvr.FindControl("lblTotal"));
            string total = e.CommandArgument.ToString().Trim().Split(',')[2];
            if (Convert.ToInt32(total) != 0)
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
    protected void getDistrictWiseRpt(string DIVISIONCODE)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "2");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", DIVISIONCODE.Trim());
            SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(Session["RangeCode"].ToString() == "" ? "0" : Session["RangeCode"].ToString()));
            SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetFromdate, GetTodate, Rangeid });

            lblPrintDateforDistrict.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdDistrict.Columns[1].FooterText = "Total :";
                grdDistrict.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdDistrict.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdDistrict.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdDistrict.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdDistrict.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                grdDistrict.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdDistrict.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdDistrict.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdDistrict.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();

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
        if (e.CommandName == "DistrictClick")
        {
            // GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            // Label total = ((Label)gvr.FindControl("lblTotal"));
            string total = e.CommandArgument.ToString().Trim().Split(',')[2];
            if (Convert.ToInt32(total) != 0)
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
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", DistCode.Trim());
            SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery,  GetDistrictCode, GetFromdate, GetTodate });
            lblPrintDateforSubDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdSubDivision.Columns[1].FooterText = "Total :";
                grdSubDivision.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdSubDivision.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdSubDivision.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdSubDivision.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdSubDivision.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                grdSubDivision.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdSubDivision.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdSubDivision.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdSubDivision.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();

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
        
        if (e.CommandName == "SubDivisionClick")
        {
            //GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //Label total = ((Label)gvr.FindControl("lblTotal"));
            string total = e.CommandArgument.ToString().Trim().Split(',')[2];
            if (Convert.ToInt32(total) != 0)
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
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", SubDivcode.Trim());
            SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetSubDivisionCode, GetFromdate, GetTodate });

            lblPrintDateforCircle.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {

                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdCircle.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                grdCircle.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdCircle.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdCircle.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdCircle.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();

                grdCircle.DataSource = dt;
                grdCircle.DataBind();

            }
            else
            {

                grdCircle.DataSource = null;
                grdCircle.DataBind();
            }
            //btnback.Visible = true;

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
            //GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //Label total = ((Label)gvr.FindControl("lblTotal"));
            string total = e.CommandArgument.ToString().Trim().Split(',')[2];
            if (Convert.ToInt32(total) != 0)
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

    protected void getThanaWiseRpt(string Blockcode)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "5");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetEntry_Mode = new SqlParameter("@EntryMode", ddlentrymode.SelectedValue.Trim());
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery,  GetBlockCode, GetFromdate, GetTodate, GetEntry_Mode });

            lblPrintDateforThana.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {


                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdThana.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdThana.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                grdThana.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdThana.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdThana.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdThana.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();

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
        if (e.CommandName == "PoliceStationClick")
        {
            //GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            // Label total = ((Label)gvr.FindControl("lblTotal"));
            string total = e.CommandArgument.ToString().Trim().Split(',')[2];
            if (Convert.ToInt32(total) != 0)
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
    protected void getPanchayatWiseRpt(string thanaCode)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "6");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", thanaCode.Trim());
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Session["Block_Code"]);
            SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetEntry_Mode = new SqlParameter("@EntryMode", ddlentrymode.SelectedValue.Trim());
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery,  GetThanCode, GetBlockCode, GetFromdate, GetTodate, GetEntry_Mode });

            lblPrintDateforPanchayat.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
               grdPanchayat.Columns[1].FooterText = "Total :";
               grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
               grdPanchayat.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
               grdPanchayat.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
               grdPanchayat.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
               grdPanchayat.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
               grdPanchayat.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
               grdPanchayat.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
               grdPanchayat.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
               grdPanchayat.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();

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
            string Total = e.CommandArgument.ToString().Trim().Split(',')[7];
            if (Convert.ToInt32(Total) != 0)
            {

                char[] seprator = { ',' }; 
                string DivisionCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                string DistrictCode = e.CommandArgument.ToString().Trim().Split(',')[1];
                string SubDivisionCode = e.CommandArgument.ToString().Trim().Split(',')[2];
                string BlockCode = e.CommandArgument.ToString().Trim().Split(',')[3];
                string ThanaCode = e.CommandArgument.ToString().Trim().Split(',')[4];
                string Panchayatcode = e.CommandArgument.ToString().Trim().Split(',')[5];
                string panchayatname= e.CommandArgument.ToString().Trim().Split(seprator)[6];
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
                getPanchayatWiseData(DivisionCode, DistrictCode, SubDivisionCode, BlockCode, ThanaCode, Panchayatcode);
            }
        }
    }
    private void   getPanchayatWiseData(string DivisionCode, string DistrictCode, string SubDivisionCode, string BlockCode, string Thanacode, string Panchayatcode)
    {
        try
        {
            ViewState["lnkClick"] = "lnkPanchayatsTotal_Click";
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            ViewState["Block_Code"] = BlockCode;
            ViewState["Thana_Code"] = Thanacode;
            ViewState["Panchayat_Code"] = Panchayatcode;
            ViewState["PageIndex"] = "1";
            lnkPanchayats();
        }
        catch (Exception)
        {

        }
    }
    

    public     override void VerifyRenderingInServerForm(Control control)
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
            ViewState["Division_Code"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivision_Code"] = "";
            ViewState["Block_Code"] = "";
            ViewState["Thana_Code"] = "";
            ViewState["Panchayat_Code"] = "";
            ViewState["PageIndex"] = "1";
            rptPager.DataSource = null;
            rptPager.DataBind();
            if ((ViewState["lnkClick"].ToString() == "lnkDivisionTotal_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkDivisionFinalize_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkDivisionUnFinalize_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkDivisionNirast_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkDivisionFinalNirast_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkDivisionPrakriyadhin_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkDivisionAshwikrit_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkDivisionMapi_Nirdharit_Click")
                 || (ViewState["lnkClick"].ToString() == "lnkDivisionvaadi_ki_vaad_sankhya_varsh_Click"))
            {
                ViewState["lnkClick"] = "";
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                pnlDist.Visible = true;
                grd_Division.Visible = true;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;


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
            }
            else if (ViewState["lnkClick"].ToString() == "lnkDistrictTotal_Click"
                    || ViewState["lnkClick"].ToString() == "lnkDistrictFinalize_Click"
                    || ViewState["lnkClick"].ToString() == "lnkDistrictUnFinalize_Click"
                    || ViewState["lnkClick"].ToString() == "lnkDistrictNirast_Click"
                    || ViewState["lnkClick"].ToString() == "lnkDistrictFinalNirast_Click"
                    || ViewState["lnkClick"].ToString() == "lnkDistrictPrakriyadhin_Click"
                    || ViewState["lnkClick"].ToString() == "lnkDistrictAshwikrit_Click"
                    || ViewState["lnkClick"].ToString() == "lnkDistrictMapi_Nirdharit_Click"
                    || ViewState["lnkClick"].ToString() == "lnkDistrictvaadi_ki_vaad_sankhya_varsh_Click")
            {
                ViewState["lnkClick"] = "";
                if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;

                pnlDistrict.Visible = true;
                grdDistrict.Visible = true;

                

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if ((ViewState["lnkClick"].ToString() == "lnkSubDivisionTotal_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalize_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionUnFinalize_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionNirast_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalNirast_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionPrakriyadhin_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionAshwikrit_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionMapi_Nirdharit_Click"
                || ViewState["lnkClick"].ToString() == "lnkSubDivisionvaadi_ki_vaad_sankhya_varsh_Click"))
                   {
                ViewState["lnkClick"] = "";
                if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlSubDivision.Visible = true;
                grdSubDivision.Visible = true;


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
            }
            else if (ViewState["lnkClick"].ToString() == "lnkBlockTotal_Click"
                    || ViewState["lnkClick"].ToString() == "lnkBlockFinalize_Click"
                    || ViewState["lnkClick"].ToString() == "lnkBlockUnFinalize_Click"
                    || ViewState["lnkClick"].ToString() == "lnkBlockNirast_Click"
                    || ViewState["lnkClick"].ToString() == "lnkBlockFinalNirast_Click"
                    || ViewState["lnkClick"].ToString() == "lnkBlockPrakriyadhin_Click"
                    || ViewState["lnkClick"].ToString() == "lnkBlockAshwikrit_Click"
                    || ViewState["lnkClick"].ToString() == "lnkBlockMapi_Nirdharit_Click"
                    || ViewState["lnkClick"].ToString() == "lnkBlock_ki_vaad_sankhya_varsh_Click")
            {
                ViewState["lnkClick"] = "";
                if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;


                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlCircle.Visible = true;
                grdCircle.Visible = true;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["lnkClick"].ToString() == "lnkThanaTotal_Click"
                     || ViewState["lnkClick"].ToString() == "lnkThanaFinalize_Click"
                     || ViewState["lnkClick"].ToString() == "lnkThanaUnFinalize_Click"
                     || ViewState["lnkClick"].ToString() == "lnkThanaNirast_Click"
                     || ViewState["lnkClick"].ToString() == "lnkThanaFinalNirast_Click"
                     || ViewState["lnkClick"].ToString() == "lnkThanaPrakriyadhin_Click"
                     || ViewState["lnkClick"].ToString() == "lnkThanaAshwikrit_Click"
                     || ViewState["lnkClick"].ToString() == "lnkThanaMapi_Nirdharit_Click"
                     || ViewState["lnkClick"].ToString() == "lnkThana_ki_vaad_sankhya_varsh_Click")
            {
                ViewState["lnkClick"] = "";
                if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;


                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = true;
                grdThana.Visible = true;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;


                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
            else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsTotal_Click"
                    || ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalize_Click"
                    || ViewState["lnkClick"].ToString() == "lnkPanchayatsUnFinalize_Click"
                    || ViewState["lnkClick"].ToString() == "lnkPanchayatsNirast_Click"
                    || ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalNirast_Click"
                    || ViewState["lnkClick"].ToString() == "lnkPanchayatsPrakriyadhin_Click"
                    || ViewState["lnkClick"].ToString() == "lnkPanchayatsAshwikrit_Click"
                    || ViewState["lnkClick"].ToString() == "lnkPanchayatsMapi_Nirdharit_Click"
                    || ViewState["lnkClick"].ToString() == "lnkPanchayats_ki_vaad_sankhya_varsh_Click")
            {
                ViewState["lnkClick"] = "";
                if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                pnlDist.Visible = false;
                grd_Division.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;


                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = true;
                grdPanchayat.Visible = true;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
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
                if (ViewState["RoleStepBack"].ToString() == "4" || ViewState["RoleStepBack"].ToString() == "3" || ViewState["RoleStepBack"].ToString() == "2" || ViewState["RoleStepBack"].ToString() == "1" || ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                    pnlDist.Visible = false;
                    grd_Division.Visible  = false;
                }
                else
                {
                    btnback.Visible = false;
                    pnlDist.Visible = true;
                    grd_Division.Visible = true;
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
                if (ViewState["RoleStepBack"].ToString() == "3" || ViewState["RoleStepBack"].ToString() == "2" || ViewState["RoleStepBack"].ToString() == "1" || ViewState["RoleStepBack"].ToString() == "0")
                {
                    //lbltext.Text=ViewState[""]
                    btnback.Visible = false;
                    pnlDistrict.Visible = false;
                    grdDistrict.Visible = false;
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {

                    btnback.Visible = false;
                    pnlDistrict.Visible = true;
                    grdDistrict.Visible = true;
                }
                else
                {

                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString();
                    btnback.Visible = true;
                    pnlDistrict.Visible = true;
                    grdDistrict.Visible = true;
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
                if (ViewState["RoleStepBack"].ToString() == "2" || ViewState["RoleStepBack"].ToString() == "1" || ViewState["RoleStepBack"].ToString() == "0")
                {
                    lbltext.Visible = false;
                    btnback.Visible = false;
                    pnlSubDivision.Visible = false;
                    grdSubDivision.Visible = false;
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = false;
                    lbltext.Visible = false;
                    pnlSubDivision.Visible = true;
                    grdSubDivision.Visible = true;
                }
                else
                {
                    btnback.Visible = true;
                    lbltext.Visible = true;
                    pnlSubDivision.Visible = true;
                    grdSubDivision.Visible = true;
                    if (ViewState["RoleStepBack"].ToString() == "4")
                    {
                        lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString();
                    }
                    else
                    {
                        lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + "," + "District Name :- " + ViewState["DistrictName"].ToString();
                    }
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
                if (ViewState["RoleStepBack"].ToString() == "1" || ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                    pnlCircle.Visible = false;
                    grdCircle.Visible = false;
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = false;
                    pnlCircle.Visible = true;
                    grdCircle.Visible = true;
                }
                else
                {
                    btnback.Visible = true;
                    pnlCircle.Visible = true;
                    grdCircle.Visible = true;
                    if (ViewState["RoleStepBack"].ToString() == "3")
                    {
                        lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString();
                    }
                    else if (ViewState["RoleStepBack"].ToString() == "4")
                    {
                        lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() + ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString();
                    }
                    else if (ViewState["RoleStepBack"].ToString() == "5")
                    {
                        lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + ",District Name :- " + ViewState["DistrictName"].ToString() +
                                         "<br>Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString();
                    }
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
                if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                    pnlthana.Visible = false;
                    grdThana.Visible = false;
                }
                else if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = false;
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
                else
                {
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                    grdThana.Visible = true;

                    if (ViewState["RoleStepBack"].ToString() == "2")
                    {
                        lbltext.Text = "Circle / Block :-" + ViewState["BlockName"].ToString();

                    }
                    else if (ViewState["RoleStepBack"].ToString() == "3")
                    {
                        lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle /Block :-" + ViewState["BlockName"].ToString();
                    }
                    else if (ViewState["RoleStepBack"].ToString() == "4")
                    {
                        lbltext.Text = "DistrictName:-" + ViewState["DistrictName"].ToString() + ",SubDivisionName:-" + ViewState["SubDivisionName"].ToString()
                            + "<br/>CircleBlock:-" + ViewState["BlockName"].ToString();
                    }
                    else if (ViewState["RoleStepBack"].ToString() == "5")
                    {
                        lbltext.Text = "DivisionName:-" + ViewState["DIVISIONName"].ToString() + "," + "DistrictName:-" + ViewState["DistrictName"].ToString()
                        + "<br/>SubDivisionName:-" + ViewState["SubDivisionName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString();
                    }

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
                if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else
                {
                    btnback.Visible = true;
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                    if (ViewState["RoleStepBack"].ToString() == "1")
                    {
                        lbltext.Text = "Thana:-" + ViewState["thana"].ToString();
                    }
                    else if (ViewState["RoleStepBack"].ToString() == "2")
                    {
                        lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString() + ",Thana:-" + ViewState["thana"].ToString();
                    }
                    else if (ViewState["RoleStepBack"].ToString() == "3")
                    {
                        lbltext.Text = "SubDivisionName:- " + ViewState["SubDivisionName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString() + "<br/>" + "Thana:- " + ViewState["thana"].ToString();
                    }
                    else if (ViewState["RoleStepBack"].ToString() == "4")
                    {
                        lbltext.Text = "DistrictName:-" + ViewState["DistrictName"].ToString() + "SubDivisionName:-"
                        + ViewState["SubDivisionName"].ToString() + "<br/>CircleBlock:-" + ViewState["BlockName"].ToString() + "," + "Thana:-" + ViewState["thana"].ToString();
                    }
                    else if (ViewState["RoleStepBack"].ToString() == "5")
                    {
                        lbltext.Text = "Division Name:-" + ViewState["DIVISIONName"].ToString() + ",District Name" + ViewState["DistrictName"].ToString() + "<br/>SubDivisionName:-"
                       + ViewState["SubDivisionName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString() + "<br/>" + "Thana:-" + ViewState["thana"].ToString();
                    }
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

    protected void lnkDivision()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "9");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["Division_Code"].ToString()));
        SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkDivisionTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionvaadi_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        int totalRecord = 0;
        if (_RecordCount.Value != null)
        {
            int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
        }

        if (dt.Rows.Count > 0)
        {
            Session["mySearchAppData03"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            Session["mySearchAppData03"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        int recordCount = Convert.ToInt32(_RecordCount.Value);
        this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), "50");

        btnback.Visible = true;

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
    protected void lnkDivisionTotal_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
             ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionTotal_Click";
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
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionFinalize_Click";
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
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionUnFinalize_Click";
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
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionNirast_Click";
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
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionFinalNirast_Click";
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
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionPrakriyadhin_Click";
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
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionAshwikrit_Click";
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
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionMapi_Nirdharit_Click";
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
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["Division_Code"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDivisionvaadi_ki_vaad_sankhya_varsh_Click";
                lnkDivision();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void lnkDistrcict()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;
      
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "9");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["Division_Code"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["DistrictCode"].ToString()));
        SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkDistrictTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }//
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictvaadi_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        int totalRecord = 0;
        if (_RecordCount.Value != null)
        {
            int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
        }

        if (dt.Rows.Count > 0)
        {
            Session["mySearchAppData03"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            Session["mySearchAppData03"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        int recordCount = Convert.ToInt32(_RecordCount.Value);
        this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), "50");

        btnback.Visible = true;

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
    protected void lnkDistrictTotal_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictTotal_Click";
                lnkDistrcict();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDistrictFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictFinalize_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDistrictUnFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictUnFinalize_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDistrictNirast_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictNirast_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDistrictFinalNirast_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictNirast_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDistrictPrakriyadhin_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictPrakriyadhin_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDistrictAshwikrit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictAshwikrit_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDistrictMapi_Nirdharit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictMapi_Nirdharit_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDistrictvaadi_ki_vaad_sankhya_varsh_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictvaadi_ki_vaad_sankhya_varsh_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void lnkSubDivision()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;      
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "9");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["Division_Code"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["DistrictCode"].ToString()));
        SqlParameter GetSubdivisionCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ViewState["SubDivision_Code"].ToString()));
        SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkSubDivisionTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionvaadi_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,GetSubdivisionCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        int totalRecord = 0;
        if (_RecordCount.Value != null)
        {
            int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
        }

        if (dt.Rows.Count > 0)
        {
            Session["mySearchAppData03"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            Session["mySearchAppData03"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        int recordCount = Convert.ToInt32(_RecordCount.Value);
        this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), "50");

        btnback.Visible = true;

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
    protected void lnkSubDivisionTotal_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionTotal_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSubDivisionFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionFinalize_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSubDivisionUnFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionUnFinalize_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSubDivisionNirast_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionNirast_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSubDivisionFinalNirast_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionFinalNirast_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSubDivisionPrakriyadhin_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionPrakriyadhin_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSubDivisionAshwikrit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionAshwikrit_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSubDivisionMapi_Nirdharit_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionMapi_Nirdharit_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSubDivisionvaadi_ki_vaad_sankhya_varsh_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["Division_Code"] = DivisionCode;
            ViewState["DistrictCode"] = DistrictCode;
            ViewState["SubDivision_Code"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionvaadi_ki_vaad_sankhya_varsh_Click";
                lnkSubDivision();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void lnkBlock()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;
     
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "9");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["Division_Code"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["DistrictCode"].ToString()));
        SqlParameter GetSubdivisionCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ViewState["SubDivision_Code"].ToString()));
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ViewState["Block_Code"].ToString()));
        SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkBlockTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
               GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }//
        else if (ViewState["lnkClick"].ToString() == "lnkBlock_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        int totalRecord = 0;
        if (_RecordCount.Value != null)
        {
            int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
        }

        if (dt.Rows.Count > 0)
        {
            Session["mySearchAppData03"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            Session["mySearchAppData03"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        int recordCount = Convert.ToInt32(_RecordCount.Value);
        this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), "50");

        btnback.Visible = true;

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
    protected void lnkBlockTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockTotal_Click";
            lnkBlock();
        }
    }
    protected void lnkBlockFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockFinalize_Click";
            lnkBlock();
        }
    }
    protected void lnkBlockUnFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockUnFinalize_Click";
            lnkBlock();
        }

    }
    protected void lnkBlockNirast_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockNirast_Click";
            lnkBlock();
        }

    }
    protected void lnkBlockFinalNirast_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockFinalNirast_Click";
            lnkBlock();
        }

    }
    protected void lnkBlockPrakriyadhin_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockPrakriyadhin_Click";
            lnkBlock();
        }
    }
    protected void lnkBlockAshwikrit_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockAshwikrit_Click";
            lnkBlock();
        }
    }
    protected void lnkBlockMapi_Nirdharit_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockMapi_Nirdharit_Click";
            lnkBlock();
        }

    }
    protected void lnkBlock_ki_vaad_sankhya_varsh_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlock_ki_vaad_sankhya_varsh_Click";
            lnkBlock();
        }
    }

    protected void lnkThana()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;      
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "9");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["Division_Code"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["DistrictCode"].ToString()));
        SqlParameter GetSubdivisionCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ViewState["SubDivision_Code"].ToString()));
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ViewState["Block_Code"].ToString()));
        SqlParameter GetThana = new SqlParameter("@ThanCode", Convert.ToInt32(ViewState["Thana_Code"].ToString()));
        SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetEntry_Mode = new SqlParameter("@EntryMode", ddlentrymode.SelectedValue.Trim());
        if (ViewState["lnkClick"].ToString() == "lnkThanaTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana,GetFromdate,GetTodate,GetEntry_Mode,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana,GetFromdate,GetTodate,GetEntry_Mode,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana,GetFromdate,GetTodate,GetEntry_Mode,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana,GetFromdate,GetTodate,GetEntry_Mode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana,GetFromdate,GetTodate,GetEntry_Mode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana,GetFromdate,GetTodate,Getfinaldata,GetEntry_Mode,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana,GetFromdate,GetTodate,GetEntry_Mode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana,GetFromdate,GetTodate,GetEntry_Mode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }//
        else if (ViewState["lnkClick"].ToString() == "lnkThana_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
               GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana,GetFromdate,GetTodate,GetEntry_Mode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        int totalRecord = 0;
        if (_RecordCount.Value != null)
        {
            int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
        }

        if (dt.Rows.Count > 0)
        {
            Session["mySearchAppData03"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            Session["mySearchAppData03"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        int recordCount = Convert.ToInt32(_RecordCount.Value);
        this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), "50");

        btnback.Visible = true;

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
    protected void lnkThanaTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaTotal_Click";
            lnkThana();
        }

    }
    protected void lnkThanaFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaFinalize_Click";
            lnkThana();
        }

    }
    protected void lnkThanaUnFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaUnFinalize_Click";
            lnkThana();
        }

    }
    protected void lnkThanaNirast_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaNirast_Click";
            lnkThana();
        }

    }
    protected void lnkThanaFinalNirast_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaFinalNirast_Click";
            lnkThana();
        }

    }
    protected void lnkThanaPrakriyadhin_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaPrakriyadhin_Click";
            lnkThana();
        }

    }
    protected void lnkThanaAshwikrit_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaAshwikrit_Click";
            lnkThana();
        }

    }
    protected void lnkThanaMapi_Nirdharit_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaMapi_Nirdharit_Click";
            lnkThana();
        }

    }
    protected void lnkThana_ki_vaad_sankhya_varsh_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThana_ki_vaad_sankhya_varsh_Click";
            lnkThana();
        }
    }

    protected void lnkPanchayats()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;   
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "9");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["Division_Code"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["DistrictCode"].ToString()));
        SqlParameter GetSubdivisionCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ViewState["SubDivision_Code"].ToString()));
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ViewState["Block_Code"].ToString()));
        SqlParameter GetThana = new SqlParameter("@ThanCode", Convert.ToInt32(ViewState["Thana_Code"].ToString()));
        SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Convert.ToInt32(ViewState["Panchayat_Code"].ToString()));
        SqlParameter GetFromdate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter GetTodate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkPanchayatsTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana, GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana,GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana, GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana, GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana, GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana, GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana, GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana, GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayats_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana, GetPanchayatCode,GetFromdate,GetTodate,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount });
        }
        int totalRecord = 0;
        if (_RecordCount.Value != null)
        {
            int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
        }

        if (dt.Rows.Count > 0)
        {
            Session["mySearchAppData03"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            Session["mySearchAppData03"] = null;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        int recordCount = Convert.ToInt32(_RecordCount.Value);
        this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), "50");

        btnback.Visible = true;

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
    protected void lnkPanchayatsTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsTotal_Click";
            lnkPanchayats();
        }

    }
    protected void lnkPanchayatsFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsFinalize_Click";
            lnkPanchayats();
        }

    }
    protected void lnkPanchayatsUnFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsUnFinalize_Click";
            lnkPanchayats();
        }

    }
    protected void lnkPanchayatsNirast_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsNirast_Click";
            lnkPanchayats();
        }

    }
    protected void lnkPanchayatsFinalNirast_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsFinalNirast_Click";
            lnkPanchayats();
        }
    }
    protected void lnkPanchayatsPrakriyadhin_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsPrakriyadhin_Click";
            lnkPanchayats();
        }
    }
    protected void lnkPanchayatsAshwikrit_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsAshwikrit_Click";
            lnkPanchayats();
        }
    }
    protected void lnkPanchayatsMapi_Nirdharit_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsMapi_Nirdharit_Click";
            lnkPanchayats();
        }
    }
    protected void lnkPanchayats_ki_vaad_sankhya_varsh_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["Division_Code"] = DivisionCode;
        ViewState["DistrictCode"] = DistrictCode;
        ViewState["SubDivision_Code"] = SubDivisionCode;
        ViewState["Block_Code"] = BlockCode;
        ViewState["Thana_Code"] = ThanaCode;
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayats_ki_vaad_sankhya_varsh_Click";
            lnkPanchayats();
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        txtFromdate.Attributes.Add("readonly", "readonly");
        txTodate.Attributes.Add("readonly", "readonly");
        ViewState["PageIndex"] = "1";
        ViewState["lnkClick"] = "";
        ViewState["DIVISIONName"] = "";
        ViewState["DistrictName"] = "";
        ViewState["SubDivisionName"] = "";
        ViewState["BlockName"] = "";
        ViewState["thana"] = "";
        ViewState["PanchayatName"] = "";
        ViewState["RoleStepBack"] = "";
        ViewState["ThanaCode"] = "";

        //9 division wise login role
        if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR") //
        {
            ViewState["stepBack"] = "5";
            bindDivision();
        }
        //Commsionary login role
        else if (Session["Role"].ToString() == "COM")
        {
            ViewState["stepBack"] = "4";
            bindDivision();
        }
        //District wise login role
        else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
        {
            ViewState["stepBack"] = "3";
            getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
        }
        //Subdivision wise login role
        else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
        {
            Response.Redirect("~/Default.aspx");
        }
        //circle wise login role
        else if (Session["Role"].ToString() == "COOPT")
        {
            ViewState["stepBack"] = "2";
            getThanaWiseRpt(Convert.ToString(Session["Block_Code"]));
        }
        //thana wise login role
        else if (Session["Role"].ToString() == "SHOOPT")
        {
            ViewState["stepBack"] = "1";
            getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"]));
        }
        else
        {
            Response.Redirect("Default.aspx");
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
     if ((ViewState["lnkClick"].ToString() == "lnkDivisionTotal_Click")
         ||(ViewState["lnkClick"].ToString() == "lnkDivisionFinalize_Click")
         ||(ViewState["lnkClick"].ToString() == "lnkDivisionUnFinalize_Click")
         ||(ViewState["lnkClick"].ToString() == "lnkDivisionNirast_Click")
         ||(ViewState["lnkClick"].ToString() == "lnkDivisionFinalNirast_Click")
         ||(ViewState["lnkClick"].ToString() == "lnkDivisionPrakriyadhin_Click")
         ||(ViewState["lnkClick"].ToString() == "lnkDivisionAshwikrit_Click")
         ||(ViewState["lnkClick"].ToString() == "lnkDivisionMapi_Nirdharit_Click")
         || (ViewState["lnkClick"].ToString() == "lnkDivisionvaadi_ki_vaad_sankhya_varsh_Click"))
        {
            lnkDivision();
        }
        else  if (ViewState["lnkClick"].ToString() == "lnkDistrictTotal_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictFinalize_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictUnFinalize_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictNirast_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictFinalNirast_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictPrakriyadhin_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictAshwikrit_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictMapi_Nirdharit_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictvaadi_ki_vaad_sankhya_varsh_Click")
        {
            lnkDistrcict();
        }
        else if ((ViewState["lnkClick"].ToString() == "lnkSubDivisionTotal_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalize_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionUnFinalize_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionNirast_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalNirast_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionPrakriyadhin_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionAshwikrit_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionMapi_Nirdharit_Click")
                 ||(ViewState["lnkClick"].ToString() == "lnkSubDivisionvaadi_ki_vaad_sankhya_varsh_Click"))
        {
            lnkSubDivision();
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockTotal_Click"
                || ViewState["lnkClick"].ToString() == "lnkBlockFinalize_Click"
                || ViewState["lnkClick"].ToString() == "lnkBlockUnFinalize_Click"
                || ViewState["lnkClick"].ToString() == "lnkBlockNirast_Click"
                || ViewState["lnkClick"].ToString() == "lnkBlockFinalNirast_Click"
                || ViewState["lnkClick"].ToString() == "lnkBlockPrakriyadhin_Click"
                || ViewState["lnkClick"].ToString() == "lnkBlockAshwikrit_Click"
                || ViewState["lnkClick"].ToString() == "lnkBlockMapi_Nirdharit_Click"
                || ViewState["lnkClick"].ToString() == "lnkBlock_ki_vaad_sankhya_varsh_Click")
        {
            lnkBlock();
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaTotal_Click"
                || ViewState["lnkClick"].ToString() == "lnkThanaFinalize_Click"
                || ViewState["lnkClick"].ToString() == "lnkThanaUnFinalize_Click"
                || ViewState["lnkClick"].ToString() == "lnkThanaNirast_Click"
                || ViewState["lnkClick"].ToString() == "lnkThanaFinalNirast_Click"
                || ViewState["lnkClick"].ToString() == "lnkThanaPrakriyadhin_Click"
                || ViewState["lnkClick"].ToString() == "lnkThanaAshwikrit_Click"
                || ViewState["lnkClick"].ToString() == "lnkThanaMapi_Nirdharit_Click"
                || ViewState["lnkClick"].ToString() == "lnkThana_ki_vaad_sankhya_varsh_Click")
        {
            lnkThana();

        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsTotal_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalize_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayatsUnFinalize_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayatsNirast_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalNirast_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayatsPrakriyadhin_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayatsAshwikrit_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayatsMapi_Nirdharit_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayats_ki_vaad_sankhya_varsh_Click")
        {

            lnkPanchayats();
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
        }


        else
        {
            return false;
        }


    }

}