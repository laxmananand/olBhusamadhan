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

                // bindDivision();
                try
                {
                    ViewState["DIVISIONName"] = " N/A";

                    if (Session["DistName"] != null)
                    {
                        ViewState["DistrictName"] = Session["DistName"].ToString();
                    }
                    else
                    {
                        ViewState["DistrictName"] = " N/A";
                    }

                    ViewState["SubDivisionName"] = " N/A";

                    if (Request.QueryString["dst"] != null)
                    {
                        ViewState["DistCode"] = null;
                        ViewState["DistCode"] = Request.QueryString["dst"].ToString();
                    }

                }
                catch (Exception ex) { }
            }
            if (!IsPostBack)
            {
                if ((Session["Role"].ToString() == "HQ" || Session["Role"].ToString() == "COM" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR") && ViewState["DistCode"] != null)
                {
                    // getSubDivisionWiseRpt(Convert.ToString(ViewState["DistCode"]));
                   
                }
                else if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString() == "COM" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR")
                {
                    bindDivision();
                }
                else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
                {
                   
                    getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
                }

                else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
                {
                   // getSubDivisionWiseRpt(Convert.ToString(Session["District_Code"]));
                }
                else if (Session["Role"].ToString() == "COOPT")
                {
                    getThanaWiseRpt(Convert.ToString(Session["Block_Code"]));
                   
                }
                else if (Session["Role"].ToString() == "SHOOPT")
                {
              
                }
                else
                {
                    Session.Clear();
                    Session.Abandon();
                    Response.Redirect("~/DeptDefault.aspx");
                }
            }

        }
        else
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/DeptDefault.aspx");
        }
    }
    void bindDivision()
    {
        try
        {

            SqlParameter GetQuery = new SqlParameter("@QueryType", "1");
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString()==""?"01-01-2000" : txtFromdate.Text);
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? DateTime.Now.ToString("dd-MM-yyyy") : txTodate.Text);

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDisApplicationConsolidateRpt", new SqlParameter[] { GetQuery ,FromDate,Todate
                });
            lblPrintDate.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
             
                lblDetail.Visible = true;
            
                grd_District.Columns[1].FooterText = "Total :";
                grd_District.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_District.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grd_District.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();

                grd_District.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grd_District.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grd_District.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grd_District.DataSource = dt;
                grd_District.DataBind();
            }
            else
            {


                grd_District.DataSource = dt;
                grd_District.DataBind();
            }
            btnback.Visible = false;

            pnlDist.Visible = true;
            grd_District.Visible = true;

           


        }
        catch (Exception ex)
        { }

    }
    protected void grd_District_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Distcode = e.CommandArgument.ToString().Trim().Split(',')[0];
       
        //string sql = @"Select SubDivCode,BlockCode,BlockName from Blocks where SubDivCode ='" + SubDivcode.ToString().Trim() + "' order by BlockName";
        //DataTable dt = clsData.GetDataTable(sql);
        //ViewState["BlockName"] = dt.Rows[0]["BlockName"].ToString().Trim();
        ViewState["DistrictName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString();
        lbltext.Visible = true;
        if (e.CommandName == "DstClick")
        {
            
            getCircleWiseRpt(Distcode);
            pnlCircle.Visible = true;
            grdCircle.Visible = true;
            grdThana.Visible = false;
        }

        btnback.Visible = true;
    }
    protected void getCircleWiseRpt(string Distcode)
    {
        try
        {
            /*
            string sql = @"select B.SubDivCode,B.BlockCode,B.BlockName,
isnull(App.TotalApplication,0)TotalApplication
from Blocks as B left outer join
(  select  count( MR.ApplicationNo) as TotalApplication,MR.Sub_DivCode,MR.Block_Code
   from Matter_Registration as MR
   group by MR.ApplicationNo,MR.Sub_DivCode,MR.Block_Code
) as App on App.Block_Code=B.BlockCode where B.SubDivCode=@SubDivCode order by B.BlockName";

            SqlParameter _SubDivcode = new SqlParameter("@SubDivcode", SubDivcode.ToString().Trim());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _SubDivcode });
            */
            SqlParameter GetQuery = new SqlParameter("@QueryType", "2");
  
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Distcode.Trim());

            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? "01-01-2000" : txtFromdate.Text);
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? DateTime.Now.ToString("dd-MM-yyyy") : txTodate.Text);

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDisApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode, FromDate,Todate });

            lblDateTime3.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                lblDetail.Visible = false;
               
               // lblDetail2.Visible = false;
                //lblDetail3.Visible = true;
                lblPrintDate.Visible = false;
                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();


                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdCircle.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdCircle.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdCircle.DataSource = dt;
                grdCircle.DataBind();

            }
            else
            {

                grdCircle.DataSource = dt;
                grdCircle.DataBind();
            }
            btnback.Visible = true;

            pnlDist.Visible = false;
           // grd_Division.Visible = false;

           // pnlDistrict.Visible = false;
          grd_District.Visible = false;

           // pnlSubDivision.Visible = false;
          //  grdSubDivision.Visible = false;

            pnlCircle.Visible = true;
            grdCircle.Visible = true;

           // pnlthana.Visible = false;
           // grdThana.Visible = false;

          //  pnlpanchayat.Visible = false;
            //grdPanchayat.Visible = false;


        }
        catch (Exception ex)
        { }
    }
    protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Blockcode = e.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["Block_Code"] = Blockcode+"";
        ViewState["CircleBlock"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        lbltext.Text = " District Name :- " + ViewState["DistrictName"].ToString() + " ,Circle/Block :-" + ViewState["CircleBlock"].ToString();
        lbltext.Visible = true;

        if (e.CommandName == "CircleClick")
        {
            //getThanaWiseRpt(Blockcode);
            getThanaWiseRpt(Blockcode);
            grdCircle.Visible = false;
            pnlCircle.Visible = false;
        }
        btnback.Visible = true;
    }
    protected void getThanaWiseRpt(string Blockcode)//Panchayat Records
    {
        try
        {
            /*
            string sql = @"select B.SubDivCode,B.BlockCode,B.BlockName,
isnull(App.TotalApplication,0)TotalApplication
from Blocks as B left outer join
(  select  count( MR.ApplicationNo) as TotalApplication,MR.Sub_DivCode,MR.Block_Code
   from Matter_Registration as MR
   group by MR.ApplicationNo,MR.Sub_DivCode,MR.Block_Code
) as App on App.Block_Code=B.BlockCode where B.SubDivCode=@SubDivCode order by B.BlockName";

            SqlParameter _SubDivcode = new SqlParameter("@SubDivcode", SubDivcode.ToString().Trim());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _SubDivcode });
            */
            SqlParameter GetQuery = new SqlParameter("@QueryType", "3");

            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode","0" );

            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Blockcode.Trim());
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? "01-01-2000" : txtFromdate.Text);
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? DateTime.Now.ToString("dd-MM-yyyy") : txTodate.Text);

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDisApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode,FromDate,Todate });

            lblDateTime4.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                lblDetail.Visible = false;
                // lblDetail2.Visible = false;
                //lblDetail3.Visible = true;
                lblPrintDate.Visible = false;
                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();

                grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();
                grdThana.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdThana.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdThana.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdThana.DataSource = dt;
                grdThana.DataBind();

            }
            else
            {

                grdThana.DataSource = dt;
                grdThana.DataBind();
            }
            btnback.Visible = true;

            pnlDist.Visible = false;
            // grd_Division.Visible = false;

            // pnlDistrict.Visible = false;
            // grdDistrict.Visible = false;

            // pnlSubDivision.Visible = false;
            //  grdSubDivision.Visible = false;

            pnlthana.Visible = true;
            grdThana.Visible = true;

           


        }
        catch (Exception ex)
        { }
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


            if (pnlDist.Visible == true )
            {
                ExportExcel(grd_District, pnlDist);
            }
            if (pnlCircle.Visible == true )
            {
                ExportExcel(grdCircle, pnlCircle);
            }
            if (pnlthana.Visible == true )
            {
                ExportExcel(grdThana, pnlthana);
            }
            if (Panel_Panchayats.Visible == true )
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
    protected void btnExpToExl2_Click(object sender, EventArgs e)
    {
        btnback.Visible = false;
        //lblDateTime1.Visible = true;
       // lblDetail2.Visible = false;
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", DateTime.Now.ToString("ddMMyyhhmmss") + "DistrictWiseApplicationReport.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        for (int i = 0; i < grdCircle.HeaderRow.Cells.Count; i++)
        {
            grdCircle.HeaderRow.Cells[i].Style.Add("border-style", "Solid");
            grdCircle.HeaderRow.Cells[i].Style.Add("border-color", "Black");
            grdCircle.HeaderRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            grdCircle.HeaderRow.Cells[i].Style.Add("Font-Bold", "True");
            grdCircle.HeaderRow.Cells[i].Style.Add("Fore-Color", "Black");

            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
        }
        for (int i = 0; i < grdCircle.FooterRow.Cells.Count; i++)
        {
            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
            grdCircle.FooterRow.Cells[i].Style.Add("border-style", "Solid");
            grdCircle.FooterRow.Cells[i].Style.Add("border-color", "Black");
            grdCircle.FooterRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            grdCircle.FooterRow.Cells[i].Style.Add("Font-Bold", "True");
            grdCircle.FooterRow.Cells[i].Style.Add("Fore-Color", "Black");
        }
        int j = 1;
        //   This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in grdCircle.Rows)
        {
            gvrow.BackColor = System.Drawing.Color.White;
            if (j <= grd_District.Rows.Count)
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
        pnlCircle.RenderControl(htw);
        string dd = sw.ToString();
        dd = dd.Replace("href=", "");
        Response.Write(dd);
        Response.End();
    }



    protected void btnExpToExl3_Click(object sender, EventArgs e)
    {
        btnback.Visible = false;
        //lblDateTime2.Visible = true;
        btn_Export.Visible = false;
        //lblDetail3.Visible = false;
        //lblDetail2.Visible = false;
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", DateTime.Now.ToString("ddMMyyhhmmss") + "SubDivisionWiseApplicationReport.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        for (int i = 0; i < grdThana.HeaderRow.Cells.Count; i++)
        {
            grdThana.HeaderRow.Cells[i].Style.Add("border-style", "Solid");
            grdThana.HeaderRow.Cells[i].Style.Add("border-color", "Black");
            grdThana.HeaderRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            grdThana.HeaderRow.Cells[i].Style.Add("Font-Bold", "True");
            grdThana.HeaderRow.Cells[i].Style.Add("Fore-Color", "Black");

            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
        }
        for (int i = 0; i < grdThana.FooterRow.Cells.Count; i++)
        {
            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
            grdThana.FooterRow.Cells[i].Style.Add("border-style", "Solid");
            grdThana.FooterRow.Cells[i].Style.Add("border-color", "Black");
            grdThana.FooterRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            grdThana.FooterRow.Cells[i].Style.Add("Font-Bold", "True");
            grdThana.FooterRow.Cells[i].Style.Add("Fore-Color", "Black");
        }
        int j = 1;
        //   This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in grdThana.Rows)
        {
            gvrow.BackColor = System.Drawing.Color.White;
            if (j <= grdThana.Rows.Count)
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

        grdThana.RenderControl(htw);
        string dd = sw.ToString();
        dd = dd.Replace("href=", "");
        Response.Write(dd);
        Response.End();
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
            for (int i = 0; i < grd_District.FooterRow.Cells.Count; i++)
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
                if (j <= grd_District.Rows.Count)
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
        if (pnlCircle.Visible == true)
        {
            pnlCircle.Visible = false;
            pnlDist.Visible = true;
            grd_District.Visible = true;
            btnback.Visible = false;
            lbltext.Visible = false;
            pnlthana.Visible = false;
        }
        else if(pnlthana.Visible==true)
        {
            pnlthana.Visible = false;
            pnlCircle.Visible = true;
            grdCircle.Visible = true;
            pnlDist.Visible = false;
            lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString();
        }
        else if (Panel_Panchayats.Visible == true)
        {
            Panel_Panchayats.Visible = false;
            pnlCircle.Visible = false;
            pnlthana.Visible = true;
            grdThana.Visible = true;
            pnlDist.Visible = false;
            lbltext.Text = " Thana :- " + ViewState["Thana"].ToString();
        }
        else if (Pnlsearch.Visible == true)
        {
            Pnlsearch.Visible = false;
            pnlCircle.Visible = false;
            pnlthana.Visible = false;
            pnlDist.Visible = false;
            Panel_Panchayats.Visible = true;
            grdPanchayats.Visible = true;           
            lbltext.Text = " Panchayat :- " + ViewState["Panchayat"].ToString();
        }
    }

    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Thanacode = e.CommandArgument.ToString().Trim().Split(',')[0];

        ViewState["Thana"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        lbltext.Text = " District Name :- " + ViewState["DistrictName"].ToString() 
            + " ,Circle/Block :-" + ViewState["CircleBlock"].ToString()+ " ,Thana :- " + ViewState["Thana"].ToString();
        lbltext.Visible = true;

        if (e.CommandName == "ThanaClick")
        {
            //getThanaWiseRpt(Blockcode);
            getPanchayatWiseRpt(Thanacode);
            pnlthana.Visible = false;
            grdThana.Visible = false;
        }
        btnback.Visible = true;
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
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? "01-01-2000" : txtFromdate.Text);
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? DateTime.Now.ToString("dd-MM-yyyy") : txTodate.Text);
            DataTable dt = clsData.GetDataTableWithProc("Sp_GetDisApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode,FromDate,Todate });

            lblDateTime5.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                lblDetail.Visible = false;
                // lblDetail2.Visible = false;
                //lblDetail3.Visible = true;
                lblPrintDate.Visible = false;
                grdPanchayats.Columns[1].FooterText = "Total :";
                grdPanchayats.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayats.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdPanchayats.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("FinalNirast")).Sum().ToString();

                grdPanchayats.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdPanchayats.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdPanchayats.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdPanchayats.DataSource = dt;
                grdPanchayats.DataBind();

            }
            else
            {

                grdPanchayats.DataSource = dt;
                grdPanchayats.DataBind();
            }
            btnback.Visible = true;

            //pnlDist.Visible = false;
            // grd_Division.Visible = false;

            // pnlDistrict.Visible = false;
            // grdDistrict.Visible = false;

            // pnlSubDivision.Visible = false;
            //  grdSubDivision.Visible = false;

            Panel_Panchayats.Visible = true;
            grdPanchayats.Visible = true;




        }
        catch (Exception ex)
        { }
    }

    protected void grdPanchayats_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Panchayatcode = e.CommandArgument.ToString().Trim().Split(',')[0];

        ViewState["Panchayat"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        lbltext.Text = " District Name :- " + ViewState["DistrictName"].ToString()
            + " ,Circle/Block :-" + ViewState["CircleBlock"].ToString() 
            + " ,Thana :- " + ViewState["Thana"].ToString()+ "Panchayat :- "+ ViewState["Panchayat"].ToString();
        lbltext.Visible = true;

        if (e.CommandName == "PanchayatClick")
        {
            getPanchayatWiseData(Panchayatcode, ViewState["Block_Code"].ToString());
            Panel_Panchayats.Visible = false;
            grdPanchayats.Visible = false;
        }
        btnback.Visible = true;
    }
    private void getPanchayatWiseData(string Panchayatcode, string Blockcode)
    {
        try
        {
            Session["mySearchAppData03"] = null;

            SqlParameter GetQueryType = new SqlParameter("@QueryType", "1");
            SqlParameter GetComm_Code = new SqlParameter("@Comm_Code", "0");
            SqlParameter GetDistrict_Code = new SqlParameter("@District_Code", "0");
            SqlParameter GetSub_DivCode = new SqlParameter("@Sub_DivCode", "0");
            SqlParameter GetBlock_Code = new SqlParameter("@Block_Code", Convert.ToInt32(Blockcode));
            SqlParameter GetThana_code = new SqlParameter("@Thana_code", "0");
            SqlParameter GetPanchayat_Code = new SqlParameter("@Panchayat_Code", Convert.ToInt32(Panchayatcode));
            SqlParameter GetVillage = new SqlParameter("@Village", "0");
            SqlParameter GetWardNo = new SqlParameter("@WardNo", "0");
            DataTable dt = clsData.GetDataTableWithProc("SP_SearchMatterRegistration", new SqlParameter[] { GetQueryType, GetComm_Code, GetDistrict_Code, GetSub_DivCode, GetBlock_Code, GetThana_code, GetPanchayat_Code, GetVillage, GetWardNo });
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                //lblDetail3.Visible = false;
                lblDetail5.Visible = true;
                //lblDetail2.Visible = false;

                lblPrintDate.Visible = false;
                lblDetail.Visible = false;


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

            btnback.Visible = true;
            
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
            //LinkButton lnkAreaCode = sender as LinkButton;
            //string btnName = lnkAreaCode.ID;
            LinkButton linkbtn = sender as LinkButton;
            string UrlRedirect = enc.Encrypt(linkbtn.CommandArgument);
            Response.Redirect("ViewApplicationDetails.aspx?RegId=" + UrlRedirect);

            //if (btnName.Contains("View"))
            //{
            //    string getId = lnkAreaCode.CommandArgument;




            //}
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
        if ((Session["Role"].ToString() == "HQ" || Session["Role"].ToString() == "COM" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR") && ViewState["DistCode"] != null)
        {
            // getSubDivisionWiseRpt(Convert.ToString(ViewState["DistCode"]));

        }
        else if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString() == "COM" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR")
        {
            bindDivision();
        }
        else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
        {

            getCircleWiseRpt(Convert.ToString(Session["District_Code"]));
        }

        else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
        {
            // getSubDivisionWiseRpt(Convert.ToString(Session["District_Code"]));
        }
        else if (Session["Role"].ToString() == "COOPT")
        {
            getThanaWiseRpt(Convert.ToString(Session["Block_Code"]));

        }
        else if (Session["Role"].ToString() == "SHOOPT")
        {

        }
        else
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/DeptDefault.aspx");
        }
        txtFromdate.Attributes.Add("readonly", "readonly");
        txTodate.Attributes.Add("readonly", "readonly");
    }
}