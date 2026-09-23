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

public partial class LandDispute_Report_ApplicationDistConsolidateRpt : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null)
        {
            if (!IsPostBack)
            {
                txtFromdate.Attributes.Add("readonly", "readonly");
                txTodate.Attributes.Add("readonly", "readonly");
                ViewState["lnkClick"] = "";
                ViewState["stepBack"] = "";
                ViewState["DistrictName"] = "";
                ViewState["Block_Name"] = "";
                ViewState["Block_Code"] = "";
                ViewState["PanchayatName"] = "";
                ViewState["ThanaCode"] = "";
                //9 division wise login role
                if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR" || Session["Role"].ToString() == "ADGLAW") //
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
                else if (Session["Role"].ToString() == "DIG")
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
                    ViewState["stepBack"] = "3";
                    getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
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
                    Response.Redirect("~/Login_Default.aspx");
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
            SqlParameter _Commissionary = new SqlParameter("@Divisioncode", Convert.ToString(Session["Commsionary_Code"].ToString()));
            SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(Session["RangeCode"].ToString()==""?"0": Session["RangeCode"].ToString()));
            SqlParameter _FromDate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter _Todate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));        
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[] { GetQuery ,_FromDate,_Todate, _Commissionary, Rangeid });
            lblPrintDateforDistrict.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grd_District.Columns[1].FooterText = "Total :";
                grd_District.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_District.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();               
                grd_District.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();
               
               
                grd_District.DataSource = dt;
                grd_District.DataBind();
            }
            else
            {
                grd_District.DataSource = null;
                grd_District.DataBind();
            }
            pnlDist.Visible = true;
            grd_District.Visible = true;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

        }
        catch (Exception ex)
        { }

    }
    protected void grd_District_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DistrictClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label total = ((Label)gvr.FindControl("lblTotal"));
            if (Convert.ToInt32(total.Text.ToString()) != 0)
            {
                string Distcode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["DistrictName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                if ((ViewState["stepBack"].ToString() == "5")|| (ViewState["stepBack"].ToString() == "4"))
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
            SqlParameter GetSubdivision = new SqlParameter("@Subdivision", Session["Sub_DivCode"].ToString()==""?"0": Session["Sub_DivCode"].ToString());
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null :Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null: Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[] { GetQuery, GetDistrictCode, FromDate, Todate , GetSubdivision });

            lblPrintDateforCircle.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {

                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();
               
                grdCircle.DataSource = dt;
                grdCircle.DataBind();

            }
            else
            {

                grdCircle.DataSource = null;
                grdCircle.DataBind();
            }
            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = true;
            grdCircle.Visible = true;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

        }
        catch (Exception ex)
        { }
    }
    protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "BlockClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label total = ((Label)gvr.FindControl("lblTotal"));
            if (Convert.ToInt32(total.Text.ToString()) != 0)
            {
                string BlockCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["Block_Name"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["BlockCode"] = BlockCode;
                if (ViewState["stepBack"].ToString() == "5" || ViewState["stepBack"].ToString() == "4")
                {
                    lbltext.Text = " District Name :- " + ViewState["DistrictName"].ToString() + " ,Circle/Block :-" + ViewState["Block_Name"].ToString();
                }
                else if (ViewState["stepBack"].ToString() == "3")
                {
                    lbltext.Text = "Circle/Block :-" + ViewState["Block_Name"].ToString();
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
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null :Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[] { GetQuery,  GetBlockCode,  FromDate, Todate });

            lblPrintDateforThana.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();
                
                grdThana.DataSource = dt;
                grdThana.DataBind();

            }
            else
            {

                grdThana.DataSource = null;
                grdThana.DataBind();
            }
            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = true;
            grdThana.Visible = true;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        catch (Exception ex)
        { }
    }
    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PoliceStationClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label total = ((Label)gvr.FindControl("lblTotal"));
            if (Convert.ToInt32(total.Text.ToString()) != 0)
            {
                string thanaCode = e.CommandArgument.ToString().Trim().Split(',')[0];
                ViewState["Thana_Name"] = e.CommandArgument.ToString().Trim().Split(',')[1];
                ViewState["ThanaCode"] = thanaCode;
                if (ViewState["stepBack"].ToString() == "5" || ViewState["stepBack"].ToString() == "4")
                {
                    lbltext.Text = " District Name :- " + ViewState["DistrictName"].ToString() + " ,Circle/Block :-" + ViewState["Block_Name"].ToString() + ",Thana :- " + ViewState["Thana_Name"].ToString();
                }
                else if (ViewState["stepBack"].ToString() == "3")
                {
                    lbltext.Text = " Circle/Block :-" + ViewState["Block_Name"].ToString() + " ,Thana :- " + ViewState["Thana_Name"].ToString();
                }
                else if (ViewState["stepBack"].ToString() == "2")
                {
                    lbltext.Text = "Thana :- " + ViewState["Thana_Name"].ToString();
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
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", ThanCode.Trim());   
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[] { GetQuery,  GetThanCode,  FromDate, Todate });

            lblPrintDateforPanchayat.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

                grdPanchayats.Columns[1].FooterText = "Total :";


                grdPanchayats.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayats.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdPanchayats.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();
               
                grdPanchayats.DataSource = dt;
                grdPanchayats.DataBind();

            }
            else
            {

                grdPanchayats.DataSource = null;
                grdPanchayats.DataBind();
            }

            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = true;
            grdPanchayats.Visible = true;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

        }
        catch (Exception ex)
        { }
    }
    protected void grdPanchayats_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PanchayatClick")
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label total = ((Label)gvr.FindControl("lblTotal"));
            if (Convert.ToInt32(total.Text.ToString()) != 0)
            {
                char[] seprator = { ',' };
                string Panchayatcode = e.CommandArgument.ToString().Trim().Split(seprator)[0];
                string Thanacode = ViewState["ThanaCode"].ToString() == "" ? Session["Thana_Code"].ToString() : ViewState["ThanaCode"].ToString();
                ViewState["PanchayatName"] = e.CommandArgument.ToString().Trim().Split(seprator)[1];
                //ViewState["thana"]= ViewState["thana"].ToString() == "" ? "" : "<br>Thana :-" + ViewState["thana"].ToString();
                if (ViewState["stepBack"].ToString() == "5" || ViewState["stepBack"].ToString() == "4")
                {
                    lbltext.Text = " District Name :- " + ViewState["DistrictName"].ToString()
                    + " ,Circle/Block :-" + ViewState["Block_Name"].ToString()
                    + " ,Thana :- " + ViewState["Thana_Name"].ToString() + ",Panchayat :- " + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["stepBack"].ToString() == "3")
                {
                    lbltext.Text = "Circle/Block :-" + ViewState["Block_Name"].ToString()
                    + "Thana :- " + ViewState["Thana_Name"].ToString() + ",Panchayat :- " + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["stepBack"].ToString() == "2")
                {
                    lbltext.Text = "Thana :- " + ViewState["Thana_Name"].ToString() + ",Panchayat :- " + ViewState["PanchayatName"].ToString();
                }
                else if (ViewState["stepBack"].ToString() == "1")
                {
                    lbltext.Text = "Panchayat :- " + ViewState["PanchayatName"].ToString();
                }
                else
                {
                    lbltext.Text = "";
                }

                lbltext.Visible = true;
                btnback.Visible = true;
                getPanchayatWiseData(Thanacode, Panchayatcode);
            }
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
            if (Panel_Panchayats.Visible == true)
            {
                ExportExcel(grdPanchayats, Panel_Panchayats);
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
        try
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

            Gv.RenderControl(htw);
            string dd = sw.ToString();
            dd = dd.Replace("href=", "");
            string Headrer = lbltext.Text;
            Response.Write("<h3><center>" + Headrer + "</center></h3>");
            Response.Write(dd);
            Response.End();
        }
        catch (Exception ex)
        { }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
       if (ViewState["lnkClick"].ToString()== "lnkDistrictClick")
        {
            ViewState["lnkClick"] = "";
            btnback.Visible = false;

            pnlDist.Visible = true;
            grd_District.Visible = true;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if(ViewState["lnkClick"].ToString() == "lnkBlockClick")
        {
            if (ViewState["stepBack"].ToString() == "3")
            {

                btnback.Visible = false;
            }
            else
            {
                btnback.Visible = true;
            }
            ViewState["lnkClick"] = "";
            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = true;
            grdCircle.Visible = true;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if(ViewState["lnkClick"].ToString() == "lnkThanaClick")
        {
            if (ViewState["stepBack"].ToString() == "2")
            {

                btnback.Visible = false;
            }
            else
            {
                btnback.Visible = true;
            }
            ViewState["lnkClick"] = "";
            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = true;
            grdThana.Visible = true;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatClick")
        {
            if (ViewState["stepBack"].ToString() == "1")
            {

                btnback.Visible = false;
            }
            else
            {
                btnback.Visible = true;
            }
            ViewState["lnkClick"] = "";
            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = true;
            grdPanchayats.Visible = true;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if (pnlCircle.Visible == true || grdCircle.Visible == true)
        {
            lbltext.Text = "";
            btnback.Visible = false;
            pnlDist.Visible = true;
            grd_District.Visible = true;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;

        }
        else if (pnlthana.Visible == true || grdThana.Visible == true)
        {
            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = true;
            grdCircle.Visible = true;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
            if (ViewState["stepBack"].ToString() == "5" || ViewState["stepBack"].ToString() == "4")
            {
                lbltext.Text = " District Name :- " + ViewState["DistrictName"].ToString();
                btnback.Visible = true;
            }
            else if (ViewState["stepBack"].ToString() == "3")
            {
                btnback.Visible = false;
                lbltext.Text = "";
            }
        }
        else if (Panel_Panchayats.Visible == true || grdPanchayats.Visible == true)
        {
            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = true;
            grdThana.Visible = true;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
            if (ViewState["stepBack"].ToString() == "5" || ViewState["stepBack"].ToString() == "4")
            {
                lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString()
                     + ",Circle/Block :-" + ViewState["Block_Name"].ToString();
                btnback.Visible = true;
            }
            else if (ViewState["stepBack"].ToString() == "3")
            {
                lbltext.Text = "Circle/Block :-" + ViewState["Block_Name"].ToString();
                btnback.Visible = true;
            }
            else if (ViewState["stepBack"].ToString() == "2")
            {
                lbltext.Text = "";
                btnback.Visible = false;
            }
        }
        else if (Pnlsearch.Visible == true || GridView1.Visible == true)
        {
            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = true;
            grdPanchayats.Visible = true;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
            if (ViewState["stepBack"].ToString() == "5" || ViewState["stepBack"].ToString() == "4")
            {
                lbltext.Text = " District Name :- " + ViewState["DistrictName"].ToString()
               + " ,Circle/Block :-" + ViewState["Block_Name"].ToString()
               + " ,Thana :- " + ViewState["Thana_Name"].ToString();
                btnback.Visible = true;
            }
            else if (ViewState["stepBack"].ToString() == "3")
            {
                lbltext.Text = "Circle/Block :-" + ViewState["Block_Name"].ToString()
                                  + " ,Thana :- " + ViewState["Thana_Name"].ToString();

                btnback.Visible = true;
            }
            else if (ViewState["stepBack"].ToString() == "2")
            {
                lbltext.Text = "Thana :- " + ViewState["Thana_Name"].ToString();

                btnback.Visible = true;
            }
            else if (ViewState["stepBack"].ToString() == "1")
            {
                lbltext.Text = "";
                btnback.Visible = false;
            }
        }
    }       
    private void getPanchayatWiseData(string Thanacode, string Panchayatcode)
    {
        try
        {
            Session["mySearchAppData03"] = null;
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
            SqlParameter GetThanacode = new SqlParameter("@ThanCode", Convert.ToInt32(Thanacode));
            SqlParameter getType = new SqlParameter("@Type", "Total");
            SqlParameter GetPanchayat_Code = new SqlParameter("@PanchayatCode", Convert.ToInt32(Panchayatcode));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[] { GetQueryType, GetThanacode, getType, GetPanchayat_Code });
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

            //btnback.Visible = true;

            pnlDist.Visible = false;
            grd_District.Visible = false;

            pnlCircle.Visible = false;
            grdCircle.Visible = false;

            pnlthana.Visible = false;
            grdThana.Visible = false;

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;
      
            Pnlsearch.Visible = true;
            GridView1.Visible = true;


        }
        catch (Exception)
        {

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
        txtFromdate.Attributes.Add("readonly", "readonly");
        txTodate.Attributes.Add("readonly", "readonly");
        ViewState["stepBack"] = "";
        ViewState["DistrictName"] = "";
        ViewState["Block_Name"] = "";
        ViewState["Block_Code"] = "";
        ViewState["PanchayatName"] = "";
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
            ViewState["stepBack"] = "3";
            getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
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
    protected void GetTotal(string code,string gridtype,string final)
    {
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter getType = new SqlParameter("@Type", "Total");
        SqlParameter getfinal = new SqlParameter("@final", final);
        DataTable dt = null;
        if (gridtype=="District")
        {
            ViewState["lnkClick"] = "lnkDistrictClick";
            SqlParameter Getdiscode = new SqlParameter("@DistrictCode", code);
             dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[] { GetQueryType, Getdiscode, getType , getfinal });
        }
        else if (gridtype == "Block")
        {
            ViewState["lnkClick"] = "lnkBlockClick";
            SqlParameter block = new SqlParameter("@BlockCode", code);
             dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[] { GetQueryType, block, getType, getfinal });
        }
        else if (gridtype == "Thana")
        {
            ViewState["lnkClick"] = "lnkThanaClick";
            SqlParameter Thana = new SqlParameter("@ThanCode", code);
             dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[] { GetQueryType, Thana, getType, getfinal });
        }
        else if (gridtype == "Panchayat")
        {
            ViewState["lnkClick"] = "lnkPanchayatClick";
            string Thanacode = ViewState["ThanaCode"].ToString() == "" ? Session["Thana_Code"].ToString() : ViewState["ThanaCode"].ToString();
            SqlParameter _Thanacode = new SqlParameter("@ThanCode", Thanacode);
            SqlParameter Panchayat = new SqlParameter("@PanchayatCode", code);
             dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateDashboard", new SqlParameter[]{ GetQueryType, _Thanacode, Panchayat, getType, getfinal });
        }
       
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
        grd_District.Visible = false;

        pnlCircle.Visible = false;
        grdCircle.Visible = false;

        pnlthana.Visible = false;
        grdThana.Visible = false;

        Panel_Panchayats.Visible = false;
        grdPanchayats.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }
    protected void lnkTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string discode =linkbtn.CommandArgument;
        GetTotal(discode, "District","0");
        btnback.Visible = true;
    }
    protected void lnkBlockTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string block = linkbtn.CommandArgument;
        GetTotal(block, "Block", "0");
        btnback.Visible = true;
    }
    protected void lnkThanaTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string thana = linkbtn.CommandArgument;
        GetTotal(thana, "Thana", "0");
        btnback.Visible = true;
    }
    protected void lnkPanchayaTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string thana = linkbtn.CommandArgument;
        GetTotal(thana, "Panchayat", "0");
        btnback.Visible = true;
    }

    protected void lnkTotalFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string discode = linkbtn.CommandArgument;
        GetTotal(discode, "District", "1");
        btnback.Visible = true;
    }

    protected void lnkTotalUnfinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string discode = linkbtn.CommandArgument;
        GetTotal(discode, "District", "2");
        btnback.Visible = true;
    }

    protected void lnkBlockTotalFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string block = linkbtn.CommandArgument;
        GetTotal(block, "Block", "1");
        btnback.Visible = true;
    }

    protected void lnkBlockTotalUnfinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string block = linkbtn.CommandArgument;
        GetTotal(block, "Block", "2");
        btnback.Visible = true;
    }

    protected void lnkThanaTotalFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string thana = linkbtn.CommandArgument;
        GetTotal(thana, "Thana", "1");
        btnback.Visible = true;
    }

    protected void lnkThanaTotalUnfinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string thana = linkbtn.CommandArgument;
        GetTotal(thana, "Thana", "2");
        btnback.Visible = true;
    }

    protected void lnkPanchayaTotalFinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string thana = linkbtn.CommandArgument;
        GetTotal(thana, "Panchayat", "1");
        btnback.Visible = true;
    }

    protected void lnkPanchayaTotalUnfinalize_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string thana = linkbtn.CommandArgument;
        GetTotal(thana, "Panchayat", "2");
        btnback.Visible = true;
    }
}