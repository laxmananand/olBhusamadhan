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
                ViewState["PageIndex"] = "1";
                ViewState["stepBack"] = "";
                ViewState["DistrictName"] = "";
                ViewState["Distcode"] = "";
                ViewState["Block_Name"] = "";
                ViewState["BlockCode"] = "";
                ViewState["PanchayatName"] = "";
                ViewState["Panchayatcode"] = "";
                ViewState["ThanaCode"] = "";
                ViewState["lnkClick"] = "";
               
                //9 division wise login role
                if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR" || Session["Role"].ToString() == "LAWORDER") //
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
                //Commsionary login role
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
                    //Response.Redirect("~/Default.aspx");
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
            ViewState["Distcode"] = ViewState["Distcode"].ToString() == "" ? Session["District_Code"] : ViewState["Distcode"].ToString();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "1");
            SqlParameter _Commissionary = new SqlParameter("@Divisioncode", Convert.ToString(Session["Commsionary_Code"].ToString()));
            SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(Session["RangeCode"].ToString() == "" ? "0" : Session["RangeCode"].ToString()));

            SqlParameter _FromDate = new SqlParameter("@FromDate", txtFromdate.Text == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter _Todate = new SqlParameter("@ToDate", txTodate.Text == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));        
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQuery ,_FromDate,_Todate, _Commissionary, Rangeid });
            lblPrintDateforDistrict.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grd_District.Columns[1].FooterText = "Total :";
                grd_District.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_District.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grd_District.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grd_District.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grd_District.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                grd_District.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grd_District.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grd_District.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grd_District.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();

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
                ViewState["Distcode"] = Distcode;
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
            SqlParameter GetSubdivision = new SqlParameter("@Subdivision", Session["Sub_DivCode"].ToString() == "" ? "0" : Session["Sub_DivCode"].ToString());
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null :Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null: Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode, GetSubdivision, GetBlockCode, GetThanCode, GetPanchayatCode,  FromDate, Todate });

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

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");

            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
           
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null :Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode, GetBlockCode, GetThanCode, GetPanchayatCode, FromDate, Todate });

            lblPrintDateforThana.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

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
            //ViewState["Panchayatcode"] = ViewState["Panchayatcode"].ToString() == "" ? Session["Panchayat_code"] : ViewState["Panchayatcode"].ToString();
            SqlParameter GetQuery = new SqlParameter("@QueryType", "4");

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");

            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", ThanCode.Trim());
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");    
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode, GetBlockCode, GetThanCode, GetPanchayatCode, FromDate, Todate });

            lblPrintDateforPanchayat.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

                grdPanchayats.Columns[1].FooterText = "Total :";


                grdPanchayats.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayats.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdPanchayats.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdPanchayats.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdPanchayats.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                
                grdPanchayats.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdPanchayats.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdPanchayats.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdPanchayats.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("CourtInPending")).Sum().ToString();
                //grdPanchayats.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                //grdPanchayats.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                //grdPanchayats.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                //grdPanchayats.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                //grdPanchayats.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                //grdPanchayats.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
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
                ViewState["Panchayatcode"] = Panchayatcode;
                ViewState["ThanaCode"] = Thanacode;
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
        ViewState["DistrictCode"] = "";
        ViewState["Block_Code"] = "";
        ViewState["Thana_Code"] = "";
        ViewState["Panchayat_Code"] = "";
        ViewState["PageIndex"] = "1";
        rptPager.DataSource = null;
        rptPager.DataBind();
        if (ViewState["lnkClick"].ToString() == "lnkDistrictTotal_Click"
                ||ViewState["lnkClick"].ToString() == "lnkDistrictFinalize_Click"
                ||ViewState["lnkClick"].ToString() == "lnkDistrictUnFinalize_Click"
                ||ViewState["lnkClick"].ToString() == "lnkDistrictNirast_Click"
                ||ViewState["lnkClick"].ToString() == "lnkDistrictFinalNirast_Click"
                ||ViewState["lnkClick"].ToString() == "lnkDistrictPrakriyadhin_Click"
                ||ViewState["lnkClick"].ToString() == "lnkDistrictAshwikrit_Click"
                ||ViewState["lnkClick"].ToString() == "lnkDistrictMapi_Nirdharit_Click"
                || ViewState["lnkClick"].ToString() == "lnkDistrictvaadi_ki_vaad_sankhya_varsh_Click")
          {
            ViewState["lnkClick"] = "";
            if (ViewState["stepBack"].ToString() == "5" || ViewState["stepBack"].ToString() == "4")
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

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

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
                | ViewState["lnkClick"].ToString() == "lnkBlock_ki_vaad_sankhya_varsh_Click")
            {
            ViewState["lnkClick"] = "";
            if (ViewState["stepBack"].ToString() == "3")
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

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if (ViewState["lnkClick"] .ToString()== "lnkThanaTotal_Click"
                 ||ViewState["lnkClick"].ToString() == "lnkThanaFinalize_Click"
                 ||ViewState["lnkClick"].ToString() == "lnkThanaUnFinalize_Click"
                 ||ViewState["lnkClick"].ToString() == "lnkThanaNirast_Click"
                 ||ViewState["lnkClick"].ToString() == "lnkThanaFinalNirast_Click"
                 ||ViewState["lnkClick"].ToString() == "lnkThanaPrakriyadhin_Click"
                 ||ViewState["lnkClick"].ToString() == "lnkThanaAshwikrit_Click"
                 ||ViewState["lnkClick"].ToString() == "lnkThanaMapi_Nirdharit_Click"
                 || ViewState["lnkClick"].ToString() == "lnkThana_ki_vaad_sankhya_varsh_Click")
        {
            ViewState["lnkClick"] = "";
            if (ViewState["stepBack"].ToString() == "2")
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

            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;

            Pnlsearch.Visible = false;
            GridView1.Visible = false;
        }
        else if (ViewState["lnkClick"].ToString()== "lnkPanchayatsTotal_Click"
                ||ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalize_Click"
                ||ViewState["lnkClick"].ToString() == "lnkPanchayatsUnFinalize_Click"
                ||ViewState["lnkClick"].ToString() == "lnkPanchayatsNirast_Click"
                ||ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalNirast_Click"
                ||ViewState["lnkClick"].ToString() == "lnkPanchayatsPrakriyadhin_Click"
                ||ViewState["lnkClick"].ToString() == "lnkPanchayatsAshwikrit_Click"
                ||ViewState["lnkClick"].ToString() == "lnkPanchayatsMapi_Nirdharit_Click"
                || ViewState["lnkClick"].ToString() == "lnkPanchayats_ki_vaad_sankhya_varsh_Click")
        {
            ViewState["lnkClick"] = "";
            if (ViewState["stepBack"].ToString() == "1")
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
        else if (Panel_Panchayats.Visible == true|| grdPanchayats.Visible == true)
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
            SqlParameter GetPanchayat_Code = new SqlParameter("@PanchayatCode", Convert.ToInt32(Panchayatcode));
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType, GetThanacode, GetPanchayat_Code, Getfinaldata });
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


    protected void lnkDistrcict()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;
        string divisioncode = Session["Commsionary_Code"].ToString() == "" ? "0" : Session["Commsionary_Code"].ToString();
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(divisioncode));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["DistrictCode"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

        if (ViewState["lnkClick"].ToString() == "lnkDistrictTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount,FromDate,Todate });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
             dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate });
        }
       else if(ViewState["lnkClick"].ToString() == "lnkDistrictvaadi_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetDistrictCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,FromDate,Todate  });
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

        Panel_Panchayats.Visible = false;
        grdPanchayats.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }

    protected void lnkDistrictTotal_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

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
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

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
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];

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
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
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
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
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
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
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
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
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
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
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
            string DistrictCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
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
    protected void lnkBlock()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;
        string divisioncode = Session["Commsionary_Code"].ToString() == "" ? "0" : Session["Commsionary_Code"].ToString();
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(divisioncode));
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ViewState["Block_Code"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

        if (ViewState["lnkClick"].ToString() == "lnkBlockTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount,FromDate,Todate });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,FromDate,Todate  });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate  });
        }
        else if(ViewState["lnkClick"].ToString() == "lnkBlock_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetBlockCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate  });
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

        Panel_Panchayats.Visible = false;
        grdPanchayats.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }

    protected void lnkBlockTotal_Click(object sender, EventArgs e)
    {
       
        LinkButton linkbtn = sender as LinkButton;
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string BlockCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string divisioncode = Session["Commsionary_Code"].ToString() == "" ? "0" : Session["Commsionary_Code"].ToString();
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(divisioncode));
        SqlParameter getBlock = new SqlParameter("@BlockCode", Session["Block_Code"]==null || Session["Block_Code"].ToString()==""?"0":Convert.ToString(Session["Block_Code"]));
        SqlParameter GetThana = new SqlParameter("@ThanCode", Convert.ToInt32(ViewState["Thana_Code"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

        if (ViewState["lnkClick"].ToString() == "lnkThanaTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock, GetThana,Getfinaldata,_PageSize,_PageIndex,_RecordCount,FromDate,Todate });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock, GetThana,Getfinaldata,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock, GetThana,Getfinaldata,_PageSize,_PageIndex,_RecordCount,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock, GetThana,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock, GetThana,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock, GetThana,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,FromDate,Todate });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock, GetThana,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock, GetThana,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate});
        }
        else if(ViewState["lnkClick"].ToString() == "lnkThana_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode,getBlock,GetThana,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount ,FromDate,Todate});
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

        Panel_Panchayats.Visible = false;
        grdPanchayats.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }

    protected void lnkThanaTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string ThanaCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string divisioncode = Session["Commsionary_Code"].ToString() == "" ? "0" : Session["Commsionary_Code"].ToString();
        Session["mySearchAppData03"] = null;
        string thanacode =Convert.ToString(Session["Thana_Code"])==""?"0": Convert.ToString(Session["Thana_Code"]);
        SqlParameter thana = new SqlParameter("@ThanCode", thanacode);
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(divisioncode));
        SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Convert.ToInt32(ViewState["Panchayat_Code"].ToString()==""?"0": ViewState["Panchayat_Code"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

        if (ViewState["lnkClick"].ToString() == "lnkPanchayatsTotal_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount,thana,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount,thana ,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,_PageSize,_PageIndex,_RecordCount,thana,FromDate,Todate });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,thana ,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalNirast_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,thana,FromDate,Todate });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsPrakriyadhin_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,thana ,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsAshwikrit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,thana ,FromDate,Todate});
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsMapi_Nirdharit_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,thana ,FromDate,Todate});
        }
        else if(ViewState["lnkClick"].ToString() == "lnkPanchayats_ki_vaad_sankhya_varsh_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter GetMatter_Status = new SqlParameter("@Matter_Status", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("Sp_GetDistrictApplicationConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisionCode, GetPanchayatCode,Getfinaldata,GetMatter_Status,_PageSize,_PageIndex,_RecordCount,thana ,FromDate,Todate});
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

        Panel_Panchayats.Visible = false;
        grdPanchayats.Visible = false;

        Pnlsearch.Visible = true;
        GridView1.Visible = true;
    }
    protected void lnkPanchayatsTotal_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[0];
        string Total = linkbtn.CommandArgument.Split(',')[1];
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
        ViewState["stepBack"] = "";
        ViewState["DistrictName"] = "";
        ViewState["Distcode"] = "";
        ViewState["Block_Name"] = "";
        ViewState["BlockCode"] = "";
        ViewState["PanchayatName"] = "";
        ViewState["Panchayatcode"] = "";
        ViewState["ThanaCode"] = "";

        ViewState["lnkClick"] = "";
        ViewState["lnkClick"] = "";
        ViewState["lnkClick"] = "";
        ViewState["lnkClick"] = "";

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
        if (ViewState["lnkClick"].ToString() == "lnkDistrictTotal_Click"
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
        else  if (ViewState["lnkClick"].ToString() == "lnkThanaTotal_Click"
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