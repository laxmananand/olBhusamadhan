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

public partial class LandDispute_Reports_Meeting_Report_MeetingDivisionWise : System.Web.UI.Page
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
                ViewState["DivisionCode"] = "";
                ViewState["Distcode"] = "";
                ViewState["subDivsionCode"] = "";
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
                    //ViewState["RoleStepBack"] = 0 + "";
                    //getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"].ToString()));
                    Response.Redirect("~/Default.aspx");
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
            SqlParameter getFromDate = new SqlParameter("@from_date", txtfrmdate.Text=="" ? null:Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter getTodate = new SqlParameter("@Todate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_MeetingConsolidateRpt", new SqlParameter[] { GetQuery, getFromDate, getTodate, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode });
            lblPrintDateForDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grd_Division.Columns[1].FooterText = "Total :";
                grd_Division.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_application")).Sum().ToString();
                grd_Division.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_Meeting")).Sum().ToString();              

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

            //pnlpanchayat.Visible = false;
            //grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grd_Division_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DivClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //Label Total_application = ((Label)gvr.FindControl("lblTotal_application"));
            Label Total_Meeting = ((Label)gvr.FindControl("lblTotal_Meeting"));
            string Total = (int.Parse(Total_Meeting.Text.ToString())).ToString();
            if (Total!= "0" && Total!="")
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

    protected void getDistrictWiseRpt(string DIVISIONCODE)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "2");
            SqlParameter getFromDate = new SqlParameter("@from_date", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter getTodate = new SqlParameter("@Todate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", DIVISIONCODE.Trim());
            SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(Session["RangeCode"].ToString() == "" ? "0" : Session["RangeCode"].ToString()));

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_MeetingConsolidateRpt", new SqlParameter[] { GetQuery, getFromDate, getTodate, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, Rangeid });

            lblPrintDateForDistrict.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {

                grdDistrict.Columns[1].FooterText = "Total :";
                grdDistrict.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_application")).Sum().ToString();
                grdDistrict.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_Meeting")).Sum().ToString();                

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

            //pnlpanchayat.Visible = false;
            //grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "DstClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //Label Total_application = ((Label)gvr.FindControl("lblTotal_application"));
            Label Total_Meeting = ((Label)gvr.FindControl("lblTotal_Meeting"));
            string Total = (int.Parse(Total_Meeting.Text.ToString())).ToString();
            if (Total != "0" && Total != "")
            {
                string Distcode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["DistrictName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["Distcode"] = Distcode;
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
            SqlParameter getFromDate = new SqlParameter("@from_date", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter getTodate = new SqlParameter("@Todate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", DistCode.Trim());
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_MeetingConsolidateRpt", new SqlParameter[] { GetQuery, getFromDate, getTodate, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode });
            lblPrintDateForSubDivision.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdSubDivision.Columns[1].FooterText = "Total :";
                grdSubDivision.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_application")).Sum().ToString();
                grdSubDivision.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_Meeting")).Sum().ToString();             

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

            //pnlpanchayat.Visible = false;
            //grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdSubDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "sdClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //Label Total_application = ((Label)gvr.FindControl("lblTotal_application"));
            Label Total_Meeting = ((Label)gvr.FindControl("lblTotal_Meeting"));
            string Total = (int.Parse(Total_Meeting.Text.ToString())).ToString();
            if (Total != "0" && Total != "")
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
            SqlParameter getFromDate = new SqlParameter("@from_date", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter getTodate = new SqlParameter("@Todate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", SubDivcode.Trim());
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_MeetingConsolidateRpt", new SqlParameter[] { GetQuery, getFromDate, getTodate, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode });
            lblPrintDateForCircle.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_application")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_Meeting")).Sum().ToString();            

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

            //pnlpanchayat.Visible = false;
            //grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "BlockClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //Label Total_application = ((Label)gvr.FindControl("lblTotal_application"));
            Label Total_Meeting = ((Label)gvr.FindControl("lblTotal_Meeting"));
            string Total = (int.Parse(Total_Meeting.Text.ToString())).ToString();
            if (Total != "0" && Total != "")
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
            SqlParameter getFromDate = new SqlParameter("@from_date", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter getTodate = new SqlParameter("@Todate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_MeetingConsolidateRpt", new SqlParameter[] { GetQuery, getFromDate, getTodate, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode });
            lblPrintDateForThana.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_application")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total_Meeting")).Sum().ToString();             

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

            //pnlpanchayat.Visible = false;
            //grdPanchayat.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "ThanaClick")
        //{
        //    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
        //    Label TotalIncident = ((Label)gvr.FindControl("lblTotalIncident"));
        //    if (TotalIncident.Text.ToString() != "0")
        //    {
        //        string thanaCode = e.CommandArgument.ToString().Trim().Split(',')[0];
        //        ViewState["thana"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        //        ViewState["ThanaCode"] = thanaCode;
        //        if (ViewState["RoleStepBack"].ToString() == "5")
        //        {
        //            lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"] + "," + ",District Name :- " + ViewState["DistrictName"].ToString() +
        //            "<br>Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle /Block :-" + ViewState["BlockName"].ToString()
        //            + "<br>Thana :- " + ViewState["thana"].ToString();
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "4")
        //        {
        //            lbltext.Text = ",District Name :- " + ViewState["DistrictName"].ToString() +
        //            ",Sub- DivisionName :-" + ViewState["SubDivisionName"].ToString() + "<br>Circle /Block :-" + ViewState["BlockName"].ToString()
        //            + ",Thana :- " + ViewState["thana"].ToString();
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "3")
        //        {
        //            lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() + ",Circle /Block :-" + ViewState["BlockName"].ToString()
        //            + "<br>Thana :- " + ViewState["thana"].ToString();
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "2")
        //        {
        //            lbltext.Text = "Circle/Block :-" + ViewState["BlockName"].ToString()
        //            + ",Thana :- " + ViewState["thana"].ToString();
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "1")
        //        {
        //            lbltext.Text = "Thana :- " + ViewState["thana"].ToString();
        //        }
        //        else
        //        {
        //            lbltext.Text = "";
        //        }
        //        lbltext.Visible = true;

        //        btnback.Visible = true;
        //        getPanchayatWiseRpt(thanaCode);
        //    }
        //}
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
            //if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
            //{
            //    ExportExcel(grdPanchayat, pnlpanchayat);

            //}
            //if (Pnlsearch.Visible == true && GridView1.Visible == true)
            //{
            //    ExportExcel(GridView1, Pnlsearch);

            //}
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
            if (pnlDistrict.Visible == true && grdDistrict.Visible == true)
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

                //pnlpanchayat.Visible = false;
                //grdPanchayat.Visible = false;
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
            if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
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

                //pnlpanchayat.Visible = false;
                //grdPanchayat.Visible = false;
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
            if (pnlCircle.Visible == true && grdCircle.Visible == true)
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

                //pnlpanchayat.Visible = false;
                //grdPanchayat.Visible = false;
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
            if (pnlthana.Visible == true && grdThana.Visible == true)
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

                //pnlpanchayat.Visible = false;
                //grdPanchayat.Visible = false;
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
        //    if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
        //    {
        //        lbltext.Visible = true;

        //        btn_Export.Visible = true;

        //        pnlDist.Visible = false;
        //        grd_Division.Visible = false;

        //        pnlDistrict.Visible = false;
        //        grdDistrict.Visible = false;

        //        pnlSubDivision.Visible = false;
        //        grdSubDivision.Visible = false;

        //        pnlCircle.Visible = false;
        //        grdCircle.Visible = false;

        //        //pnlpanchayat.Visible = false;
        //        //grdPanchayat.Visible = false;
        //        if (ViewState["RoleStepBack"].ToString() == "5")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = "DivisionName:-" + ViewState["DIVISIONName"].ToString() + "," + "DistrictName:-" + ViewState["DistrictName"].ToString()
        //                + "<br/>SubDivisionName:-" + ViewState["SubDivisionName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString();
        //            pnlthana.Visible = true;
        //            grdThana.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "4")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
        //                                 ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString()
        //                                 + "<br/>CircleBlock:-" + ViewState["BlockName"].ToString();
        //            pnlthana.Visible = true;
        //            grdThana.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "3")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() +
        //                           ",CircleBlock:-" + ViewState["BlockName"].ToString();
        //            pnlthana.Visible = true;
        //            grdThana.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "2")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString();
        //            pnlthana.Visible = true;
        //            grdThana.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "1")
        //        {
        //            btnback.Visible = false;
        //            lbltext.Text = "";
        //            pnlthana.Visible = true;
        //            grdThana.Visible = true;
        //        }
        //        else
        //        {
        //            btnback.Visible = false;
        //            lbltext.Text = "";
        //            pnlthana.Visible = false;
        //            grdThana.Visible = false;
        //        }
        //    }
        //    if (Pnlsearch.Visible == true && GridView1.Visible == true)
        //    {


        //        lbltext.Visible = true;

        //        btn_Export.Visible = true;

        //        pnlDist.Visible = false;
        //        grd_Division.Visible = false;

        //        pnlDistrict.Visible = false;
        //        grdDistrict.Visible = false;

        //        pnlSubDivision.Visible = false;
        //        grdSubDivision.Visible = false;

        //        pnlCircle.Visible = false;
        //        grdCircle.Visible = false;

        //        pnlthana.Visible = false;
        //        grdThana.Visible = false;

        //        Pnlsearch.Visible = false;
        //        GridView1.Visible = false;
        //        if (ViewState["RoleStepBack"].ToString() == "5")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = lbltext.Text = "Division Name:-" + ViewState["DIVISIONName"].ToString() + ",District Name:-" + ViewState["DistrictName"].ToString() + "<br/>SubDivisionName:-"
        //               + ViewState["SubDivisionName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString() + "<br/>" + "Thana:-" + ViewState["thana"].ToString();
        //            pnlpanchayat.Visible = true;
        //            grdPanchayat.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "4")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
        //                                 ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString()
        //                                 + "<br/>CircleBlock:-" + ViewState["BlockName"].ToString()
        //                                 + ",Thana:-" + ViewState["thana"].ToString();
        //            pnlpanchayat.Visible = true;
        //            grdPanchayat.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "3")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() +
        //                           ",CircleBlock:-" + ViewState["BlockName"].ToString()
        //                           + "<br/>Thana:-" + ViewState["thana"].ToString();
        //            pnlpanchayat.Visible = true;
        //            grdPanchayat.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "2")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString()
        //                            + ",Thana:-" + ViewState["thana"].ToString();
        //            pnlpanchayat.Visible = true;
        //            grdPanchayat.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "1")
        //        {
        //            btnback.Visible = true;
        //            lbltext.Text = "Thana:-" + ViewState["thana"].ToString();
        //            pnlpanchayat.Visible = true;
        //            grdPanchayat.Visible = true;
        //        }
        //        else if (ViewState["RoleStepBack"].ToString() == "0")
        //        {
        //            btnback.Visible = false;
        //            lbltext.Text = "";
        //            pnlpanchayat.Visible = true;
        //            grdPanchayat.Visible = true;
        //        }
        //    }
        }
        catch (Exception er)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        btnback.Visible = false;
        if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR")
        {
            //ViewState["RoleStepBack"] = 5 + "";
            bindDivision();
        }
        else if (Session["Role"].ToString() == "COM")
        {
            //ViewState["RoleStepBack"] = 4 + "";
            ViewState["DivisionCode"] = ViewState["DivisionCode"].ToString() == "" ? Session["Commsionary_Code"].ToString() : ViewState["DivisionCode"].ToString();
            getDistrictWiseRpt(Convert.ToString(ViewState["DivisionCode"]));
        }
        else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
        {
            //ViewState["RoleStepBack"] = 3 + "";
            ViewState["Distcode"] = ViewState["Distcode"].ToString() == "" ? Session["District_Code"].ToString() : ViewState["Distcode"].ToString();
            getSubDivisionWiseRpt(Convert.ToString(ViewState["Distcode"]));
        }
        else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
        {
            //ViewState["RoleStepBack"] = 2 + "";
            ViewState["subDivsionCode"] = ViewState["subDivsionCode"].ToString() == "" ? Session["Sub_DivCode"].ToString() : ViewState["subDivsionCode"].ToString();
            getCircleWiseRpt(Convert.ToString(ViewState["subDivsionCode"]));
        }
        else if (Session["Role"].ToString() == "COOPT")
        {
            // ViewState["RoleStepBack"] = 1 + "";
            ViewState["BlockCode"] = ViewState["BlockCode"].ToString() == "" ? Session["Block_Code"].ToString() : ViewState["BlockCode"].ToString();
            getThanaWiseRpt(ViewState["BlockCode"].ToString());
        }
        else if (Session["Role"].ToString() == "SHOOPT")
        {
            //ViewState["RoleStepBack"] = 0 + "";
            //getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"].ToString()));
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}