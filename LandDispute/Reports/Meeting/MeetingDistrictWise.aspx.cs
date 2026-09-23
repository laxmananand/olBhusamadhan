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

public partial class LandDispute_Reports_Meeting_Report_MeetingDistrictWise : System.Web.UI.Page
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
                ViewState["Distcode"] = "";
                //division role login view 38 District wise date
                if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR" || Session["Role"].ToString() == "ADGLAW") //
                {
                    ViewState["RoleStepBack"] = 5 + "";
                    getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
                }
                //Commsionary role login view-Commsionary-wise District date
                else if (Session["Role"].ToString() == "COM")
                {
                    ViewState["RoleStepBack"] = 4 + "";
                    getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
                }
                //DIG role login view-Commsionary-wise District date
                else if (Session["Role"].ToString() == "DIG")
                {
                    ViewState["RoleStepBack"] = 4 + "";
                    getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
                }
                //District role login view SubDivision wise date
                else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM" || Session["Role"].ToString() == "DIO")
                {
                    Response.Redirect("~/Default.aspx");
                }
                //Subdivision role login view Block wise date
                else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
                {
                    ViewState["RoleStepBack"] = 3 + "";
                    getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
                }
                //circle role login view Thana wise date
                else if (Session["Role"].ToString() == "COOPT")
                {
                    ViewState["RoleStepBack"] = 2 + "";
                    getThanaWiseRpt(Session["Block_Code"].ToString());
                }
                //thana role login view panchayat wise date
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

    protected void getDistrictWiseRpt(string DIVISIONCODE)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "1");
            SqlParameter getFromDate = new SqlParameter("@from_date", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter getTodate = new SqlParameter("@Todate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", DIVISIONCODE.Trim()=="" ? "0" : DIVISIONCODE.Trim());
            SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(Session["RangeCode"].ToString() == "" ? "0" : Session["RangeCode"].ToString()));

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_MeetingDistrictConsolidateRpt", new SqlParameter[] { GetQuery, getFromDate, getTodate, GetDivisionCode, GetDistrictCode, GetBlockCode, GetThanCode, Rangeid });

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

          

            pnlDistrict.Visible = true;
            grdDistrict.Visible = true;

           

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
            SqlParameter getFromDate = new SqlParameter("@from_date", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter getTodate = new SqlParameter("@Todate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Distcode.Trim());
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_MeetingDistrictConsolidateRpt", new SqlParameter[] { GetQuery, getFromDate, getTodate, GetDivisionCode, GetDistrictCode, GetBlockCode, GetThanCode });
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

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

           
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
            SqlParameter getFromDate = new SqlParameter("@from_date", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter getTodate = new SqlParameter("@Todate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("sp_MeetingDistrictConsolidateRpt", new SqlParameter[] { GetQuery, getFromDate, getTodate, GetDivisionCode, GetDistrictCode, GetBlockCode, GetThanCode });
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

           

            pnlDistrict.Visible = false;
            grdDistrict.Visible = false;

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
            if (pnlCircle.Visible == true && grdCircle.Visible == true)
            {
                btn_Export.Visible = true;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                //pnlpanchayat.Visible = false;
                //grdPanchayat.Visible = false;
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

                //pnlpanchayat.Visible = false;
                //grdPanchayat.Visible = false;
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
            //else if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
            //{
            //    lbltext.Visible = true;

            //    btn_Export.Visible = true;

            //    pnlDistrict.Visible = false;
            //    grdDistrict.Visible = false;

            //    pnlCircle.Visible = false;
            //    grdCircle.Visible = false;

            //    pnlpanchayat.Visible = false;
            //    grdPanchayat.Visible = false;
            //    if (ViewState["RoleStepBack"].ToString() == "5" || ViewState["RoleStepBack"].ToString() == "4")
            //    {
            //        btnback.Visible = true;
            //        lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString();
            //        pnlthana.Visible = true;
            //        grdThana.Visible = true;
            //    }
            //    else if (ViewState["RoleStepBack"].ToString() == "3")
            //    {
            //        btnback.Visible = false;
            //        lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString();
            //        pnlthana.Visible = true;
            //        grdThana.Visible = true;
            //    }
            //    else if (ViewState["RoleStepBack"].ToString() == "2")
            //    {
            //        btnback.Visible = false;
            //        lbltext.Text = "";
            //        pnlthana.Visible = true;
            //        grdThana.Visible = true;
            //    }
            //}
            //else if (Pnlsearch.Visible == true && GridView1.Visible == true)
            //{
            //    lbltext.Visible = true;

            //    btn_Export.Visible = true;


            //    pnlDistrict.Visible = false;
            //    grdDistrict.Visible = false;


            //    pnlCircle.Visible = false;
            //    grdCircle.Visible = false;

            //    pnlthana.Visible = false;
            //    grdThana.Visible = false;

            //    Pnlsearch.Visible = false;
            //    GridView1.Visible = false;
            //    if (ViewState["RoleStepBack"].ToString() == "5" || ViewState["RoleStepBack"].ToString() == "4")
            //    {
            //        btnback.Visible = true;
            //        lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString() + "<br/>" + "Thana:-" + ViewState["thana"].ToString(); ;
            //        pnlpanchayat.Visible = true;
            //        grdPanchayat.Visible = true;
            //    }
            //    else if (ViewState["RoleStepBack"].ToString() == "3")
            //    {
            //        btnback.Visible = true;
            //        lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString() + "" + "Thana:-" + ViewState["thana"].ToString(); ;
            //        pnlpanchayat.Visible = true;
            //        grdPanchayat.Visible = true;
            //    }
            //    else if (ViewState["RoleStepBack"].ToString() == "2")
            //    {
            //        btnback.Visible = true;
            //        lbltext.Text = "Thana:-" + ViewState["thana"].ToString(); ;
            //        pnlpanchayat.Visible = true;
            //        grdPanchayat.Visible = true;
            //    }
            //    else if (ViewState["RoleStepBack"].ToString() == "1")
            //    {
            //        btnback.Visible = false;
            //        lbltext.Text = "";
            //        pnlpanchayat.Visible = true;
            //        grdPanchayat.Visible = true;
            //    }
            //}
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
            getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
        }
        else if (Session["Role"].ToString() == "COM")
        {
            getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
        }
        else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
        {
            Response.Redirect("~/Default.aspx");
        }
        else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
        {
            ViewState["Distcode"] = ViewState["Distcode"].ToString() == "" ? Session["District_Code"].ToString() : ViewState["Distcode"].ToString();
            getCircleWiseRpt(Convert.ToString(ViewState["Distcode"]));
        }
        else if (Session["Role"].ToString() == "COOPT")
        {
            ViewState["BlockCode"] = ViewState["BlockCode"].ToString() == "" ? Session["Block_Code"].ToString() : ViewState["BlockCode"].ToString();
            getThanaWiseRpt(ViewState["BlockCode"].ToString());
        }
        else if (Session["Role"].ToString() == "SHOOPT")
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}