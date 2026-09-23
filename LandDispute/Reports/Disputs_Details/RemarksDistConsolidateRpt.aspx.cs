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

public partial class LandDispute_Reports_Disputs_Details_DisputeDistConsolidateRpt : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null)
        {
            if (!IsPostBack)
            {
             
                ViewState["RoleStepBack"] = "";
                ViewState["Distcode"] = "";
                ViewState["BlockCode"] = "";
                ViewState["ThanaCode"] = "";
                ViewState["lnkClick"] = "";
                ViewState["PageIndex"] = "1";

               
                txtFromdate.Attributes.Add("readonly", "readonly");
                txTodate.Attributes.Add("readonly", "readonly");
               
                if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR")
                {
                    ViewState["RoleStepBack"] = 5 + "";
                    bindDivision();
                }
           
                else if (Session["Role"].ToString() == "COM")
                {
                    ViewState["RoleStepBack"] = 4 + "";
                    bindDivision();
                }
             
                else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
                {
                    ViewState["RoleStepBack"] = 3 + "";
                    getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
                }
              
                else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
                {
                    Response.Redirect("~/Default.aspx");
                }
             
                else if (Session["Role"].ToString() == "COOPT")
                {
                    ViewState["RoleStepBack"] = 2 + "";
                    getThanaWiseRpt(Convert.ToString(Session["Block_Code"]));
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

    void bindDivision()
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "1");
            SqlParameter getCommisionary = new SqlParameter("@DIVISIONCODE", Session["Commsionary_Code"].ToString() == "" ? 0 : Convert.ToInt32(Session["Commsionary_Code"].ToString()));
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
           
            DataTable dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQuery,getCommisionary ,FromDate,Todate});
            lblDateTime1.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grd_District.Columns[1].FooterText = "Total :";
                grd_District.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_District.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DMOPT")).Sum().ToString();
                grd_District.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SSPOPT")).Sum().ToString();
                grd_District.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ADM")).Sum().ToString();
                grd_District.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DPGRO")).Sum().ToString();
                grd_District.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SDOOPT")).Sum().ToString();
                grd_District.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DSPOPT")).Sum().ToString();
                grd_District.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SDPGRO")).Sum().ToString();
                grd_District.DataSource = dt;
                grd_District.DataBind();
            }
            else
            {
                grd_District.DataSource = dt;
                grd_District.DataBind();
            }
            pnlDist.Visible = true;
            grd_District.Visible = true;


            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = true;
            grdPanchayat.Visible = true;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            btn_Export.Visible = true;
        }
        catch (Exception ex)
        { }

    }
    protected void grd_District_RowCommand(object sender, GridViewCommandEventArgs e)
    {      
        if (e.CommandName == "DstClick")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            LinkButton lnk = (LinkButton)gvrow.FindControl("lnkDistrict");      
            int Total= int.Parse(lnk.CommandArgument.Split(',')[2]);
            if (Total != 0)
            {
                string Distcode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["Distcode"] = Distcode;
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

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Distcode.Trim());

            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
         

            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
         
            DataTable dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode,
                GetBlockCode, GetThanCode, GetPanchayatCode, FromDate, Todate});

            lblDateTime2.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DMOPT")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SSPOPT")).Sum().ToString();
                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ADM")).Sum().ToString();
                grdCircle.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DPGRO")).Sum().ToString();
                grdCircle.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SDOOPT")).Sum().ToString();
                grdCircle.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DSPOPT")).Sum().ToString();
                grdCircle.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SDPGRO")).Sum().ToString();
                grdCircle.DataSource = dt;
                grdCircle.DataBind();
            }
            else
            {
                grdCircle.DataSource = dt;
                grdCircle.DataBind();
            }
            pnlDist.Visible = false;
            grd_District.Visible = false;


            pnlCircle.Visible = true;
            grdCircle.Visible = true;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
            btn_Export.Visible = true;
        }
        catch (Exception ex)
        { }
    }
    protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        if (e.CommandName == "CircleClick")
        {           
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            LinkButton lnk = (LinkButton)gvrow.FindControl("lnkCircle");
            int Total = int.Parse(lnk.CommandArgument.Split(',')[2]);
            if (Total != 0)
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

    protected void getThanaWiseRpt(string Blockcode)//Panchayat Records
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "3");

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");

            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
           
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
           
            DataTable dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode,
                GetBlockCode, GetThanCode, GetPanchayatCode, FromDate, Todate });

            lblDateTime3.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DMOPT")).Sum().ToString();
                grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SSPOPT")).Sum().ToString();
                grdThana.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ADM")).Sum().ToString();
                grdThana.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DPGRO")).Sum().ToString();
                grdThana.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SDOOPT")).Sum().ToString();
                grdThana.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DSPOPT")).Sum().ToString();
                grdThana.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SDPGRO")).Sum().ToString();
                grdThana.DataSource = dt;
                grdThana.DataBind();
            }
            else
            {
                grdThana.DataSource = dt;
                grdThana.DataBind();
            }
            pnlDist.Visible = false;
            grd_District.Visible = false;


            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = true;
            grdThana.Visible = true;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            btn_Export.Visible = true;
        }
        catch (Exception ex)
        { }
    }
    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ThanaClick")
        {
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            LinkButton lnk = (LinkButton)gvrow.FindControl("lnkThana");
            int Total = int.Parse(lnk.CommandArgument.Split(',')[2]);
            if (Total != 0)
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

    protected void getPanchayatWiseRpt(string ThanCode)
    {
        try
        {
         
            SqlParameter GetQuery = new SqlParameter("@QueryType", "4");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", ThanCode.Trim());
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
          
            DataTable dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode, 
                GetBlockCode, GetThanCode, GetPanchayatCode,  FromDate, Todate});

            lblDateTime4.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                grdPanchayat.Columns[1].FooterText = "Total :";
                grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayat.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DMOPT")).Sum().ToString();
                grdPanchayat.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SSPOPT")).Sum().ToString();
                grdPanchayat.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ADM")).Sum().ToString();
                grdPanchayat.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DPGRO")).Sum().ToString();
                grdPanchayat.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SDOOPT")).Sum().ToString();
                grdPanchayat.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("DSPOPT")).Sum().ToString();
                grdPanchayat.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SDPGRO")).Sum().ToString();
                grdPanchayat.DataSource = dt;
                grdPanchayat.DataBind();

            }
            else
            {
                grdPanchayat.DataSource = dt;
                grdPanchayat.DataBind();
            }
            pnlDist.Visible = false;
            grd_District.Visible = false;


            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = true;
            grdPanchayat.Visible = true;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

            btn_Export.Visible = true;
        }
        catch (Exception ex)
        { }
    }
    protected void grdPanchayat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PanchayatClick")
        {           
            GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            LinkButton lnk = (LinkButton)gvrow.FindControl("lnkPanchayat");
            int Total = int.Parse(lnk.CommandArgument.Split(',')[2]);
            if (Total != 0)
            {
                char[] seprator = { ',' };
                string Panchayatcode = e.CommandArgument.ToString().Trim().Split(seprator)[0];
                string ThanaCode = ViewState["ThanaCode"].ToString() == "" ? Session["Thana_Code"].ToString() : ViewState["ThanaCode"].ToString();
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
                getPanchayatWiseData(ThanaCode, Panchayatcode);
            }           
        }
    }
    private void getPanchayatWiseData(string ThanaCode, string PanchayatCode)
    {
        try
        {
            Session["mySearchAppData03"] = null;
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
            SqlParameter GetThana_Code = new SqlParameter("@ThanCode", Convert.ToInt32(ThanaCode));
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Convert.ToInt32(PanchayatCode));
           
            DataTable dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType, GetThana_Code, 
                GetPanchayatCode });
            if (dt.Rows.Count > 0)
            {
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
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;

            Pnlsearch.Visible = true;
            GridView1.Visible = true;

            btn_Export.Visible = true;
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
    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {


            if (pnlDist.Visible == true)
            {
                ExportExcel(grd_District, pnlDist);
            }
            if (pnlCircle.Visible == true)
            {
                ExportExcel(grdCircle, pnlCircle);
            }
            if (pnlthana.Visible == true)
            {
                ExportExcel(grdThana, pnlthana);
            }
            if (pnlpanchayat.Visible == true)
            {
                ExportExcel(grdPanchayat, pnlpanchayat);
            }
            if (Pnlsearch.Visible == true)
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
        ViewState["DistrictCode"] = "";
        ViewState["Block_Code"] = "";
        ViewState["Thana_Code"] = "";
        ViewState["Panchayat_Code"] = "";
        ViewState["PageIndex"] = "1";
        rptPager.DataSource = null;
        rptPager.DataBind();
        if (ViewState["lnkClick"].ToString() == "lnkTotalDistrict_Click"
                     || (ViewState["lnkClick"].ToString() == "lnkDMOPT_District_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkSSPOPTDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkADMDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkDPGRODistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkSDOOPTDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkDSPOPTDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkSDPGRODistrict_Click")
                    )
        {
            ViewState["lnkClick"] = "";
            if (ViewState["RoleStepBack"].ToString() == "5" || ViewState["RoleStepBack"].ToString() == "4")
            {
                btnback.Visible = false;
            }
            else
            {
                btnback.Visible = true;
            }
            pnlDist.Visible = true;
            grd_District.Visible = true;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if (
                   (ViewState["lnkClick"].ToString() == "lnkSDPGROBlock_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkDSPOPTBlock_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkSDOOPTBlock_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkDPGROBlock_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkADMBlock_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkSSPOPTBlock_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkDMOPT_Block_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkTotalBlock_Click"))
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
            grd_District.Visible = false;

            pnlCircle.Visible = true;
            grdCircle.Visible = true;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if (
                (ViewState["lnkClick"].ToString() == "lnkSDPGROThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDSPOPTThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSDOOPTThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDPGROThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkADMThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSSPOPTThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDMOPT_Thana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkTotalThana_Click"))
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
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = true;
            grdThana.Visible = true;

            pnlpanchayat.Visible = false;
            grdPanchayat.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if ((ViewState["lnkClick"].ToString() == "lnkSDPGROpanchayat_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkDSPOPTpanchayat_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkSDOOPTpanchayat_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkDPGROpanchayat_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkADMpanchayat_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkSSPOPTpanchayat_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkDMOPT_panchayat_Click")
                    ||(ViewState["lnkClick"].ToString() == "lnkTotalpanchayat_Click"))
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
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            pnlpanchayat.Visible = true;
            grdPanchayat.Visible = true;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if (pnlCircle.Visible == true && grdCircle.Visible == true)
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
                pnlDist.Visible = true;
                grd_District.Visible = true;
            }
            else
            {
                btnback.Visible = false;
                lbltext.Text = "";
                pnlDist.Visible = false;
                grd_District.Visible = false;
            }
        }
        else if (pnlthana.Visible == true && grdThana.Visible == true)
        {

            lbltext.Visible = true;

            btn_Export.Visible = true;

            pnlDist.Visible = false;
            grd_District.Visible = false;

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

            pnlDist.Visible = false;
            grd_District.Visible = false;

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


            pnlDist.Visible = false;
            grd_District.Visible = false;


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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString() == "COM" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR")
        {
            bindDivision();
        }
        else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
        {
            getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
        }
        else if (Session["Role"].ToString() == "COOPT")
        {
            getThanaWiseRpt(Convert.ToString(Session["Block_Code"]));
        }
        else if (Session["Role"].ToString() == "SHOOPT")
        {
            getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"]));
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
        }

        txtFromdate.Attributes.Add("readonly", "readonly");
        txTodate.Attributes.Add("readonly", "readonly");
    }

    protected void lnkDistrcict()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;  
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["DistrictCode"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalDistrict_Click")
        {
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDistrictCode,FromDate,Todate,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDMOPT_District_Click")              
        { 
            SqlParameter GetuserID = new SqlParameter("@userID", "DMOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDistrictCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSSPOPTDistrict_Click")             
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "SSPOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDistrictCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkADMDistrict_Click")                   
        {
           
    
            SqlParameter GetuserID = new SqlParameter("@userID", "ADM");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDistrictCode,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDPGRODistrict_Click")         
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DPGRO");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDistrictCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSDOOPTDistrict_Click")
        {
          
   
            SqlParameter GetuserID = new SqlParameter("@userID", "SDOOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDistrictCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }     
        else if (ViewState["lnkClick"].ToString() == "lnkDSPOPTDistrict_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DSPOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDistrictCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSDPGRODistrict_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "SDPGRO ");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDistrictCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
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
        grd_District.Visible = false;

        pnlCircle.Visible = false;
        grdCircle.Visible = false;

        pnlthana.Visible = false;
        grdThana.Visible = false;

        pnlpanchayat.Visible = false;
        grdPanchayat.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }
    protected void lnkTotalDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkTotalDistrict_Click";
                lnkDistrcict();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkDMOPT_District_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDMOPT_District_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSSPOPTDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
               
                ViewState["lnkClick"] = "lnkSSPOPTDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkADMDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
               
                ViewState["lnkClick"] = "lnkADMDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDPGRODistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
               
                ViewState["lnkClick"] = "lnkDPGRODistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSDOOPTDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                
                ViewState["lnkClick"] = "lnkSDOOPTDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkDSPOPTDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
               
                ViewState["lnkClick"] = "lnkDSPOPTDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSDPGRODistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

            ViewState["DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                
                ViewState["lnkClick"] = "lnkSDPGRODistrict_Click";
                lnkDistrcict();
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
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ViewState["Block_Code"].ToString()));
      
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalBlock_Click")
        {
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetBlockCode,FromDate,Todate,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDMOPT_Block_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DMOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetBlockCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSSPOPTBlock_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "SSPOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetBlockCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkADMBlock_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "ADM");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetBlockCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDPGROBlock_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DPGRO");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetBlockCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSDOOPTBlock_Click")
        {


            SqlParameter GetuserID = new SqlParameter("@userID", "SDOOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetBlockCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDSPOPTBlock_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DSPOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetBlockCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSDPGROBlock_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "SDPGRO ");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetBlockCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
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
        grd_District.Visible = false;

        pnlCircle.Visible = false;
        grdCircle.Visible = false;

        pnlthana.Visible = false;
        grdThana.Visible = false;

        pnlpanchayat.Visible = false;
        grdPanchayat.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }
    protected void lnkTotalBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkTotalBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkDMOPT_Block_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDMOPT_Block_Click";
            lnkBlock();
        }
    }
    protected void lnkSSPOPTBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSSPOPTBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkADMBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkADMBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkDPGROBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDPGROBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkSDOOPTBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSDOOPTBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkDSPOPTBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDSPOPTBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkSDPGROBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSDPGROBlock_Click";
            lnkBlock();
        }
    }
   
    protected void lnkThana()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter GetThana_Code = new SqlParameter("@ThanCode", Convert.ToInt32(ViewState["Thana_Code"].ToString()));
        
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalThana_Click")
        {
           
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetThana_Code,FromDate,Todate,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDMOPT_Thana_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DMOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetThana_Code,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSSPOPTThana_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "SSPOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetThana_Code,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkADMThana_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "ADM");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetThana_Code,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDPGROThana_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DPGRO");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetThana_Code,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSDOOPTThana_Click")
        {


            SqlParameter GetuserID = new SqlParameter("@userID", "SDOOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetThana_Code,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDSPOPTThana_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DSPOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetThana_Code,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSDPGROThana_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "SDPGRO ");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetThana_Code,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
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
        grd_District.Visible = false;

        pnlCircle.Visible = false;
        grdCircle.Visible = false;

        pnlthana.Visible = false;
        grdThana.Visible = false;

        pnlpanchayat.Visible = false;
        grdPanchayat.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }
    protected void lnkTotalThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkTotalThana_Click";
            lnkThana();
        }
    }
    protected void lnkDMOPT_Thana_Click(object sender, EventArgs e)
    {

        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDMOPT_Thana_Click";
            lnkThana();
        }
    }
    protected void lnkSSPOPTThana_Click(object sender, EventArgs e)
    {

        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSSPOPTThana_Click";
            lnkThana();
        }
    }
    protected void lnkADMThana_Click(object sender, EventArgs e)
    {

        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkADMThana_Click";
            lnkThana();
        }
    }
    protected void lnkDPGROThana_Click(object sender, EventArgs e)
    {

        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDPGROThana_Click";
            lnkThana();
        }
    }
    protected void lnkSDOOPTThana_Click(object sender, EventArgs e)
    {

        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSDOOPTThana_Click";
            lnkThana();
        }
    }
    protected void lnkDSPOPTThana_Click(object sender, EventArgs e)
    {

        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDSPOPTThana_Click";
            lnkThana();
        }
    }
    protected void lnkSDPGROThana_Click(object sender, EventArgs e)
    {

        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSDPGROThana_Click";
            lnkThana();
        }
    }
    

    protected void lnkPanchayats()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;
        Session["mySearchAppData03"] = null;
        string discode= ViewState["Distcode"].ToString() == "" ? Session["District_Code"].ToString() : ViewState["Distcode"].ToString();
        string blockcode= ViewState["BlockCode"].ToString() == "" ? Session["Block_Code"].ToString() : ViewState["BlockCode"].ToString(); 
        string thancode = ViewState["ThanaCode"].ToString() == "" ? Session["Thana_Code"].ToString() : ViewState["ThanaCode"].ToString();
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(discode));
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(blockcode));
        SqlParameter GetThana_Code = new SqlParameter("@ThanCode", Convert.ToInt32(thancode));
        SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Convert.ToInt32(ViewState["Panchayat_Code"].ToString()));
       
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalpanchayat_Click")
        {

            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,GetDistrictCode,
                GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDMOPT_panchayat_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "DMOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,GetDistrictCode,
                GetBlockCode,GetThana_Code,
                GetPanchayatCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSSPOPTpanchayat_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "SSPOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,GetDistrictCode,
                GetBlockCode,GetThana_Code,
                GetPanchayatCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkADMpanchayat_Click")
        {
           

            SqlParameter GetuserID = new SqlParameter("@userID", "ADM");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,GetDistrictCode,
                GetBlockCode,GetThana_Code,
                GetPanchayatCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDPGROpanchayat_Click")
        {

            SqlParameter GetuserID = new SqlParameter("@userID", "DPGRO");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,GetDistrictCode,
                GetBlockCode,GetThana_Code,
                GetPanchayatCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSDOOPTpanchayat_Click")
        {


            SqlParameter GetuserID = new SqlParameter("@userID", "SDOOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,GetDistrictCode,
                GetBlockCode,GetThana_Code,
                GetPanchayatCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDSPOPTpanchayat_Click")
        {
        
            SqlParameter GetuserID = new SqlParameter("@userID", "DSPOPT");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,GetDistrictCode,
                GetBlockCode,GetThana_Code,
                GetPanchayatCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSDPGROpanchayat_Click")
        {
            SqlParameter GetuserID = new SqlParameter("@userID", "SDPGRO ");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetRemarksDistConsolidateRpt", new SqlParameter[] { GetQueryType,GetDistrictCode,
                GetBlockCode,GetThana_Code,
                GetPanchayatCode,FromDate,Todate,GetuserID,_PageSize,_PageIndex,_RecordCount });
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
        grd_District.Visible = false;

        pnlCircle.Visible = false;
        grdCircle.Visible = false;

        pnlthana.Visible = false;
        grdThana.Visible = false;

        pnlpanchayat.Visible = false;
        grdPanchayat.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }
    protected void lnkTotalpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkTotalpanchayat_Click";
            lnkPanchayats();
        }

    }
    protected void lnkDMOPT_panchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDMOPT_panchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkSSPOPTpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSSPOPTpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkADMpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkADMpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkDPGROpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDPGROpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkSDOOPTpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSDOOPTpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkDSPOPTpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkDSPOPTpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkSDPGROpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
        ViewState["Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSDPGROpanchayat_Click";
            lnkPanchayats();
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
        if (ViewState["lnkClick"].ToString() == "lnkTotalDistrict_Click"
                     || (ViewState["lnkClick"].ToString() == "lnkDMOPT_District_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkSSPOPTDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkADMDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkDPGRODistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkSDOOPTDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkDSPOPTDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkSDPGRODistrict_Click")
                     )
                {
                    lnkDistrcict();
                }
        else if ( (ViewState["lnkClick"].ToString() == "lnkSDPGROBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkDSPOPTBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkSDOOPTBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkDPGROBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkADMBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkSSPOPTBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkDMOPT_Block_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkTotalBlock_Click"))
                {
                    lnkBlock();
                }
        else if ((ViewState["lnkClick"].ToString() == "lnkSDPGROThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDSPOPTThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSDOOPTThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDPGROThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkADMThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSSPOPTThana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDMOPT_Thana_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkTotalThana_Click"))
        {
            lnkThana();

        }
        else if ((ViewState["lnkClick"].ToString() == "lnkSDPGROpanchayat_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDSPOPTpanchayat_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSDOOPTpanchayat_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDPGROpanchayat_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkADMpanchayat_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkSSPOPTpanchayat_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkDMOPT_panchayat_Click")
                ||(ViewState["lnkClick"].ToString() == "lnkTotalpanchayat_Click"))
        {
            lnkPanchayats();
        }
    }
}