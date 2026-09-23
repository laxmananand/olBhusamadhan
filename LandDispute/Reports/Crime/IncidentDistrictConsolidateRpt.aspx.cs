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

public partial class LandDispute_Reports_Crime_IncidentDistrictConsolidateRpt : System.Web.UI.Page
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
                    getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
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
                    Response.Redirect("~/Default.aspx");
                }
                //Subdivision wise login role
                else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
                {
                    ViewState["RoleStepBack"] = 3 + "";
                    getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
                }
                //circle wise login role
                else if (Session["Role"].ToString() == "COOPT")
                {
                    ViewState["RoleStepBack"] = 2 + "";
                    getThanaWiseRpt(Session["Block_Code"].ToString());
                }
                //thana wise login role
                else if (Session["Role"].ToString() == "SHOOPT")
                {
                    ViewState["RoleStepBack"] = 1 + "";
                    getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"]));
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
    protected void getDistrictWiseRpt(string DIVISIONCODE)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "1");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", DIVISIONCODE.Trim()==""?"0": DIVISIONCODE.Trim());
            SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(Session["RangeCode"].ToString() == "" ? "0" : Session["RangeCode"].ToString()));

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");         
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");           

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCrimeDistrictIncidentConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetBlockCode, GetThanCode, Rangeid });

            lblPrintDateForDistrict.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {

                grdDistrict.Columns[1].FooterText = "Total :";
                grdDistrict.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalIncident")).Sum().ToString();
                grdDistrict.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("praathamikee")).Sum().ToString();
                grdDistrict.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("apraathamikee")).Sum().ToString();
                grdDistrict.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sanaha")).Sum().ToString();


                grdDistrict.DataSource = dt;
                grdDistrict.DataBind();
            }
            else
            {

                grdDistrict.DataSource = null;
                grdDistrict.DataBind();
            }

          

            pnlDistrict.Visible = true;
            grdDistrict.Visible = true;

         

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
            Label TotalIncident = ((Label)gvr.FindControl("lblTotalIncident"));
            if (TotalIncident.Text.ToString() != "0")
            {
                string Distcode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["DistrictName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                if ((ViewState["RoleStepBack"].ToString() == "5") || (ViewState["RoleStepBack"].ToString() == "4"))
                {
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }
                lbltext.Visible = true;
                btnback.Visible = true;
                getCircleWiseRpt(Distcode);
            }
        }
    }  

    protected void getCircleWiseRpt(string Distcode)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "2");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Distcode.Trim());
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");           
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCrimeDistrictIncidentConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode,GetBlockCode, GetThanCode});
            lblPrintDateForCircle.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalIncident")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("praathamikee")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("apraathamikee")).Sum().ToString();
                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sanaha")).Sum().ToString();

                grdCircle.DataSource = dt;
                grdCircle.DataBind();
            }
            else
            {

                grdCircle.DataSource = null;
                grdCircle.DataBind();
            }

    

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

           

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
            Label TotalIncident = ((Label)gvr.FindControl("lblTotalIncident"));
            if (TotalIncident.Text.ToString() != "0")
            {
                string BlockCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["BlockName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["BlockCode"] = BlockCode;
                if ((ViewState["RoleStepBack"].ToString() == "5") || (ViewState["RoleStepBack"].ToString() == "4"))
                {
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                                   ",Circle /Block :-" + ViewState["BlockName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
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
            SqlParameter GetQuery = new SqlParameter("@QueryType", "3");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");            
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCrimeDistrictIncidentConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetBlockCode, GetThanCode });
            lblPrintDateForThana.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalIncident")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("praathamikee")).Sum().ToString();
                grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sanaha")).Sum().ToString();
                grdThana.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("apraathamikee")).Sum().ToString();

                grdThana.DataSource = dt;
                grdThana.DataBind();
            }
            else
            {

                grdThana.DataSource = null;
                grdThana.DataBind();
            }

           

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

         

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
    
    protected void lnkpraathamikee_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnk = sender as LinkButton;
            string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            //string SubDivisionCode = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            //string Blockcode = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            //string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[3];

            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("8"));
            SqlParameter _Type = new SqlParameter("@Type", "praathamikee");
            //SqlParameter _Districtcode = new SqlParameter("@DISTRICTCODE", Districtcode);
            //SqlParameter _SubDivisionCode = new SqlParameter("@subDivision", SubDivisionCode);
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", Session["Block_code"]);
            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", ThanaCode);
            DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _Type, _Blockcode, _ThanaCode });

            if (dt.Rows.Count > 0)
            {

                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
            Panel1.Visible = true;
            //pnlgrid.Visible = false;
            btnback.Visible = true;
            pnlthana.Visible = false;
            grdThana.Visible = false;
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
            string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            //string SubDivisionCode = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            //string Blockcode = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            //string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[3];

            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("8"));
            SqlParameter _Type = new SqlParameter("@Type", "apraathamikee");
            //SqlParameter _Districtcode = new SqlParameter("@DISTRICTCODE", Districtcode);
            //SqlParameter _SubDivisionCode = new SqlParameter("@subDivision", SubDivisionCode);
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", Session["Block_code"].ToString());
            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", ThanaCode);


            DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _Type,  _Blockcode, _ThanaCode });

            if (dt.Rows.Count > 0)
            {

                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
            Panel1.Visible = true;
        
            btnback.Visible = true;
            pnlthana.Visible = false;
            grdThana.Visible = false;
            //pnlgrid.Visible = false;
            //btnBack_div.Visible = true;
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
            string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            //string SubDivisionCode = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            //string Blockcode = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            //string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[3];

            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("8"));
            SqlParameter _Type = new SqlParameter("@Type", "snaha");
            //SqlParameter _Districtcode = new SqlParameter("@DISTRICTCODE", Districtcode);
            //SqlParameter _SubDivisionCode = new SqlParameter("@subDivision", SubDivisionCode);
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", Session["Block_code"].ToString());
            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", ThanaCode);


            DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _Type,  _Blockcode, _ThanaCode });

            if (dt.Rows.Count > 0)
            {

                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
            Panel1.Visible = true;
           
            btnback.Visible = true;
            pnlthana.Visible = false;
            grdThana.Visible = false;
            //pnlgrid.Visible = false;
            //btnBack_div.Visible = true;
        }
        catch (Exception ex)
        {

        }

    }
    //protected void lnkTotalincident_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnk = sender as LinkButton;
    //        string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[0];
    //        //string SubDivisionCode = lnk.CommandArgument.ToString().Trim().Split(',')[1];
    //        //string Blockcode = lnk.CommandArgument.ToString().Trim().Split(',')[2];
    //        //string ThanaCode = lnk.CommandArgument.ToString().Trim().Split(',')[3];

    //        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("8"));
    //        SqlParameter _Type = new SqlParameter("@Type", "TotalIncident");
    //        //SqlParameter _Districtcode = new SqlParameter("@DISTRICTCODE", Districtcode);
    //        //SqlParameter _SubDivisionCode = new SqlParameter("@subDivision", SubDivisionCode);
    //        SqlParameter _Blockcode = new SqlParameter("@BlockCode", Session["Block_code"].ToString());
    //        SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", ThanaCode);


    //        DataTable dt = clsData.GetDataTableWithProc("sp_AllIncidentConsolidateReport", new SqlParameter[] { QueryType, _Type, _Blockcode, _ThanaCode });

    //        if (dt.Rows.Count > 0)
    //        {

    //            GridView1.DataSource = dt;
    //            GridView1.DataBind();
    //        }
    //        else
    //        {
    //            GridView1.DataSource = null;
    //            GridView1.DataBind();
    //        }
    //        Pnlsearch.Visible = true;
    //        //pnlgrid.Visible = false;
    //        //btnBack_div.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //}
    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ThanaClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label TotalIncident = ((Label)gvr.FindControl("lblTotalIncident"));
            if (TotalIncident.Text.ToString() != "0")
            {
                string thanaCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["thana"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["ThanaCode"] = thanaCode;
                if ((ViewState["RoleStepBack"].ToString() == "5") || (ViewState["RoleStepBack"].ToString() == "4"))
                {
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                    ",Circle /Block :-" + ViewState["BlockName"].ToString()
                    + "<br>Thana :- " + ViewState["thana"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    lbltext.Text = "Circle/Block :-" + ViewState["BlockName"].ToString()
                    + ",Thana :- " + ViewState["thana"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
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
            SqlParameter GetQuery = new SqlParameter("@QueryType", "4");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Session["Block_code"].ToString());
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", thanaCode.Trim());          
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCrimeDistrictIncidentConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetBlockCode, GetThanCode });
            lblPrintDateForPanchayat.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdPanchayat.Columns[1].FooterText = "Total :";
                grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalIncident")).Sum().ToString();
                grdPanchayat.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("praathamikee")).Sum().ToString();
                grdPanchayat.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("apraathamikee")).Sum().ToString();
                grdPanchayat.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("sanaha")).Sum().ToString();

                grdPanchayat.DataSource = dt;
                grdPanchayat.DataBind();
            }
            else
            {

                grdPanchayat.DataSource = null;
                grdPanchayat.DataBind();
            }

          

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

          

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
            Label TotalIncident = ((Label)gvr.FindControl("lblTotalIncident"));
            if (TotalIncident.Text.ToString() != "0")
            {
                char[] seprator = { ',' };
                string Panchayatcode = e.CommandArgument.ToString().Trim().Split(seprator)[0];
                string thanaCode = ViewState["ThanaCode"].ToString() == "" ? Session["Thana_Code"].ToString() : ViewState["ThanaCode"].ToString();
                ViewState["PanchayatName"] = e.CommandArgument.ToString().Trim().Split(seprator)[1];
                //ViewState["thana"]= ViewState["thana"].ToString() == "" ? "" : "<br>Thana :-" + ViewState["thana"].ToString();
                if ((ViewState["RoleStepBack"].ToString() == "5") || (ViewState["RoleStepBack"].ToString() == "4"))
                {
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                                    ",Circle /Block :-" + ViewState["BlockName"].ToString()
                                    + "<br>Thana :- " + ViewState["thana"].ToString() + ",Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    lbltext.Text = "Circle /Block :-" + ViewState["BlockName"].ToString() + ",Thana :- " + ViewState["thana"].ToString() + "<br>Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    lbltext.Text = "Thana :- " + ViewState["thana"].ToString() + ",Panchayat :-" + ViewState["PanchayatName"].ToString();

                }
                else if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    lbltext.Text = "Panchayat :-" + ViewState["PanchayatName"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }

                lbltext.Visible = true;

                btnback.Visible = true;
                getPanchayatWiseData(thanaCode,Panchayatcode);
            }
        }
    }
    private void getPanchayatWiseData(string thanacode, string Panchayatcode)
    {
        try
        {
            Session["mySearchAppData03"] = null;
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
            SqlParameter GetThana_code = new SqlParameter("@ThanCode", Convert.ToInt32(thanacode));
            SqlParameter GetPanchayat_Code = new SqlParameter("@PanchayatCode", Convert.ToInt32(Panchayatcode));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetCrimeDistrictIncidentConsolidateRpt", new SqlParameter[] { GetQueryType, GetThana_code, GetPanchayat_Code });
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

       

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

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
            if (pnlDistrict.Visible == true && grdDistrict.Visible == true)
            {
                ExportExcel(grdDistrict, pnlDistrict);
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
          
            if (pnlCircle.Visible == true && grdCircle.Visible == true)
            {
                btn_Export.Visible = true;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;
                if ((ViewState["RoleStepBack"].ToString() == "5" || ViewState["RoleStepBack"].ToString() == "4"))
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
            else if (pnlthana.Visible == true && grdThana.Visible == true)
            {

                lbltext.Visible = true;

                btn_Export.Visible = true;

                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5" || ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = true;
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString();
                    pnlCircle.Visible = true;
                    grdCircle.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
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

                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5" || ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = true;
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString();
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = false;
                    lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString();
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlthana.Visible = true;
                    grdThana.Visible = true;
                }
            }
            else if (Pnlsearch.Visible == true && GridView1.Visible == true)
            {
                lbltext.Visible = true;

                btn_Export.Visible = true;


                pnlDistrict.Visible = false;
                grdDistrict.Visible = false;


                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5" || ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = true;
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString() + "<br/>" + "Thana:-" + ViewState["thana"].ToString(); ;
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = true;
                    lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString() + "" + "Thana:-" + ViewState["thana"].ToString(); ;
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Thana:-" + ViewState["thana"].ToString(); ;
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlpanchayat.Visible = true;
                    grdPanchayat.Visible = true;
                }
            }
            if (Panel1.Visible == true)
            {
                Panel1.Visible = false;

                btnback.Visible = false;
                pnlthana.Visible = true;
                grdThana.Visible = true;

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