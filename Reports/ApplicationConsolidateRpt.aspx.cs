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
                try
                {
                    ViewState["DIVISIONName"] = " N/A";

                    if (Session["DistName"] != null)
                    {
                        ViewState["DistrictName"] = Session["DistName"].ToString();
                    }
                    else {
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
                    getSubDivisionWiseRpt(Convert.ToString(ViewState["DistCode"]));
                }
                else if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString() == "COM"|| Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR")
                {
                    bindDivision();
                }
                else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
                {
                    getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
                }

                else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
                {
                    getSubDivisionWiseRpt(Convert.ToString(Session["District_Code"]));
                }
                else if (Session["Role"].ToString() == "COOPT")
                {
                    getCircleWiseRpt(Convert.ToString(Session["Sub_DivCode"]));
                }
                else if (Session["Role"].ToString() == "SHOOPT")
                {
                    getThanaWiseRpt(Convert.ToString(Session["Block_Code"]));
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
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });
            lblPrintDate.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                lblDateTime1.Visible = false;
                lblDateTime2.Visible = false;
                lblDateTime3.Visible = false;
                lblDateTime4.Visible = false;
                lblDetail.Visible = true;
                lblDetail2.Visible = false;
                lblDetail3.Visible = false;
                lblDetail4.Visible = false;
                lblDetail5.Visible = false;
                grd_Division.Columns[1].FooterText = "Total :";
                grd_Division.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_Division.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grd_Division.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grd_Division.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grd_Division.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grd_Division.DataSource = dt;
                grd_Division.DataBind();
            }
            else
            {


                grd_Division.DataSource = dt;
                grd_Division.DataBind();
            }
            btnback.Visible = false;

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
        string DivisionCode = e.CommandArgument.ToString().Trim().Split(',')[0];

        //        string sql = @"SELECT DIVISIONCODE,DISTRICTCODE,DISTRICTNAME
        //       FROM mst_Commissionary_Districts where DIVISIONCODE='" + DivisionCode.ToString().Trim() + "' order by DISTRICTNAME";
        //        DataTable dt = clsData.GetDataTable(sql);
        ViewState["DIVISIONName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString();
        lbltext.Visible = true;
        if (e.CommandName == "DivClick")
        {
            getDistrictWiseRpt(DivisionCode);
        }
        btnback.Visible = true;
    }

    protected void getDistrictWiseRpt(string DIVISIONCODE)
    {
        try
        {
            /*           string sql = @"select D.DistCode,D.DistName,D.Division_Code,
            isnull(App.TotalApplication,0)TotalApplication
            from Districts as D left outer join
            (  select  count( MR.ApplicationNo) as TotalApplication,MR.Comm_Code,MR.District_Code
               from Matter_Registration as MR
               group by MR.ApplicationNo,MR.Comm_Code,MR.District_Code
            ) as App on App.District_Code=D.DistCode where D.Division_Code=@DIVISIONCODE order by D.DistName";

                        SqlParameter _DIVISIONCODE = new SqlParameter("@DIVISIONCODE", DIVISIONCODE.ToString().Trim());
                        DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DIVISIONCODE }); */
            SqlParameter GetQuery = new SqlParameter("@QueryType", "2");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", DIVISIONCODE.Trim());
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });

            lblDateTime2.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                lblPrintDate.Visible = false;
                lblDateTime1.Visible = false;
                lblDateTime2.Visible = true;
                lblDateTime3.Visible = false;
                lblDateTime4.Visible = false;
                lblDetail.Visible = false;
                lblDetail2.Visible = true;
                lblDetail3.Visible = false;
                lblDetail4.Visible = false;
                lblDetail5.Visible = false;

                grdDistrict.Columns[1].FooterText = "Total :";
                grdDistrict.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdDistrict.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdDistrict.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdDistrict.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdDistrict.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdDistrict.DataSource = dt;
                grdDistrict.DataBind();
            }
            else
            {

                grdDistrict.DataSource = dt;
                grdDistrict.DataBind();
            }
            btnback.Visible = true;

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
        string Distcode = e.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString() + ",District Name :- " + ViewState["DistrictName"].ToString();
        lbltext.Visible = true;
        //string sql = @"SELECT DistCode,Sd_Code2,Sd_Name_En FROM SubDivisions where DistCode='" + Distcode.ToString().Trim() + "' order by Sd_Name_En";
        //DataTable dt = clsData.GetDataTable(sql);
        //ViewState["Sd_Name_En"] = dt.Rows[0]["Sd_Name_En"].ToString().Trim();

        if (e.CommandName == "DstClick")
        {
            getSubDivisionWiseRpt(Distcode);
        }


    }

    protected void getSubDivisionWiseRpt(string DistCode)
    {
        try
        {
            /*
            string sql = @"select SD.DistCode,SD.Sd_Code2,SD.Sd_Name_En,
isnull(App.TotalApplication,0)TotalApplication
from SubDivisions as SD left outer join
(  select  count( MR.ApplicationNo) as TotalApplication,MR.Comm_Code,MR.District_Code,MR.Sub_DivCode
   from Matter_Registration as MR
   group by MR.ApplicationNo,MR.Comm_Code,MR.District_Code,MR.Sub_DivCode
) as App on App.Sub_DivCode=SD.Sd_Code2 where SD.DistCode=@DistCode order by SD.Sd_Name_En";

            SqlParameter _DistCode = new SqlParameter("@DistCode", DistCode.ToString().Trim());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DistCode });*/
            SqlParameter GetQuery = new SqlParameter("@QueryType", "3");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", DistCode.Trim());
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });

            lblDateTime3.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

                lblPrintDate.Visible = false;
                lblDateTime1.Visible = false;
                lblDateTime2.Visible = false;
                lblDateTime4.Visible = false;
                lblDetail.Visible = false;
                lblDetail2.Visible = false;
                lblDetail3.Visible = true;
                lblDetail4.Visible = false;
                lblDetail5.Visible = false;
                grdSubDivision.Columns[1].FooterText = "Total :";
                grdSubDivision.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdSubDivision.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdSubDivision.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdSubDivision.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdSubDivision.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdSubDivision.DataSource = dt;
                grdSubDivision.DataBind();
            }
            else
            {
                
                grdSubDivision.DataSource = dt;
                grdSubDivision.DataBind();
            }
            btnback.Visible = true;

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
        string SubDivcode = e.CommandArgument.ToString().Trim().Split(',')[0];

        //string sql = @"Select SubDivCode,BlockCode,BlockName from Blocks where SubDivCode ='" + SubDivcode.ToString().Trim() + "' order by BlockName";
        //DataTable dt = clsData.GetDataTable(sql);
        //ViewState["BlockName"] = dt.Rows[0]["BlockName"].ToString().Trim();
        ViewState["SubDivisionName"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString() + ",District Name :- " + ViewState["DistrictName"].ToString() + ",<br> Sub-DivisionName :- " + ViewState["SubDivisionName"].ToString();
        lbltext.Visible = true;
        if (e.CommandName == "sdClick")
        {
            getCircleWiseRpt(SubDivcode);
        }
        btnback.Visible = true;
    }

    protected void getCircleWiseRpt(string SubDivcode)
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
            SqlParameter GetQuery = new SqlParameter("@QueryType", "4");
            SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", SubDivcode.Trim());
            SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");
            SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });

            lblDateTime3.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                lblDetail.Visible = false;
                lblDetail2.Visible = false;
                lblDetail3.Visible = true;
                lblPrintDate.Visible = false;
                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdCircle.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
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
        string Blockcode = e.CommandArgument.ToString().Trim().Split(',')[0];

        ViewState["CircleBlock"] = e.CommandArgument.ToString().Trim().Split(',')[1];
        lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString() + ",District Name :- " + ViewState["DistrictName"].ToString() + ",<br> Sub-DivisionName :- " + ViewState["SubDivisionName"].ToString() + ",Circle/Block :-" + ViewState["CircleBlock"].ToString();
        lbltext.Visible = true;

        if (e.CommandName == "CircleClick")
        {
            //getThanaWiseRpt(Blockcode);
            getPanchayatWiseRpt(Blockcode);
        }
        btnback.Visible = true;
    }

    protected void getThanaWiseRpt(string Blockcode)//Panchayat Records
    {
        try
        {

            string sql = @"select MT.Circle_Code,MT.PS_Code,MT.Police_Station,
isnull(App.TotalApplication,0)TotalApplication
from mst_Thana as MT left outer join
(  select  count( MR.ApplicationNo) as TotalApplication,MR.Sub_DivCode,MR.Thana_code
   from Matter_Registration as MR
   group by MR.ApplicationNo,MR.Sub_DivCode,MR.Thana_code
) as App on App.Thana_code=MT.PS_Code where MT.Circle_Code=@Blockcode order by MT.Police_Station";

            SqlParameter _Blockcode = new SqlParameter("@Blockcode", Blockcode.ToString().Trim());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _Blockcode });


            lblDateTime3.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                pnlthana.Visible = true;
                pnlCircle.Visible = false;
                pnlDistrict.Visible = false;
                pnlSubDivision.Visible = false;
                lblDetail3.Visible = true;
                lblDetail2.Visible = false;
                btn_Export.Visible = false;
                lblPrintDate.Visible = false;
                lblDetail.Visible = false;

            }
            else
                btn_Export.Visible = false;
            pnlDist.Visible = false;
            pnlDistrict.Visible = false;
            pnlSubDivision.Visible = false;
            pnlCircle.Visible = false;
            pnlthana.Visible = true;

            grdThana.Columns[1].FooterText = "Total :";
            grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalApplication")).Sum().ToString();


            grdThana.DataSource = dt;
            grdThana.DataBind();
        }
        catch (Exception ex)
        { }
    }

    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string Blockcode = e.CommandArgument.ToString().Trim();

        ////string sql = @"Select PS_Code,Circle_Code,Police_Station from mst_Thana where Circle_Code='" + Blockcode.ToString().Trim() + "' order by Police_Station";
        //string sql = @"Select PS_Code,Circle_Code,Police_Station from mst_Thana where PS_Code='" + Blockcode.ToString().Trim() + "' order by Police_Station";

        //DataTable dt = clsData.GetDataTable(sql);
        //ViewState["Police_StationName"] = dt.Rows[0]["Police_Station"].ToString().Trim();

        if (e.CommandName == "CircleClick")
        {
            getPanchayatWiseRpt(Blockcode);
        }
        btnback.Visible = true;
    }

    protected void getPanchayatWiseRpt(string Blockcode)
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

            DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });

            lblDateTime4.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                lblDetail3.Visible = false;
                lblDetail5.Visible = true;
                lblDetail2.Visible = false;

                lblPrintDate.Visible = false;
                lblDetail.Visible = false;
                grdPanchayat.Columns[1].FooterText = "Total :";


                grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayat.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Nirast")).Sum().ToString();
                grdPanchayat.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Prakriyadhin")).Sum().ToString();
                grdPanchayat.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Ashwikrit")).Sum().ToString();
                grdPanchayat.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Mapi_Nirdharit")).Sum().ToString();
                grdPanchayat.DataSource = dt;
                grdPanchayat.DataBind();
            }
            else
            {
                grdPanchayat.DataSource = dt;
                grdPanchayat.DataBind();
            }
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

            pnlpanchayat.Visible = true;
            grdPanchayat.Visible = true;


        }
        catch (Exception ex)
        { }
    }
    protected void grdPanchayat_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // string Panchayatcode = e.CommandArgument.ToString().Trim()
        char[] seprator ={','};
        string Panchayatcode = e.CommandArgument.ToString().Trim().Split(seprator)[0];
        string Blockcode = e.CommandArgument.ToString().Trim().Split(seprator)[1];

        ViewState["PanchayatName"] = e.CommandArgument.ToString().Trim().Split(seprator)[2];
        lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString() + ",District Name :- " + ViewState["DistrictName"].ToString() + ",<br> Sub-DivisionName :- " + ViewState["SubDivisionName"].ToString() + ", Circle/Block :-" + ViewState["CircleBlock"].ToString() + ", Panchayat :-" + ViewState["PanchayatName"].ToString();
        lbltext.Visible = true;

        //// string sql = @"Select PS_Code,Subdivision_Code,Police_Station from mst_Thana where Circle_Code='" + Blockcode.ToString().Trim() + "' order by Police_Station";
        // string sql = @"Select DistrictCode,BlockCode,PanchayatCode,Police_Station from mst_Panchayats where Subdivision_Code='" + Blockcode.ToString().Trim() + "' order by Police_Station";
        // DataTable dt = clsData.GetDataTable(sql);
        // //ViewState["BlockName"] = dt.Rows[0]["BlockName"].ToString().Trim();
        // ViewState["Police_StationName"] = dt.Rows[0]["Police_Station"].ToString().Trim();

        if (e.CommandName == "CircleClick")
        {
            //getThanaWiseRpt(Blockcode);
            //getVillageWiseRpt(Panchayatcode);
            getPanchayatWiseData(Panchayatcode, Blockcode);
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
                lblDetail3.Visible = false;
                lblDetail5.Visible = true;
                lblDetail2.Visible = false;

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

    protected void getVillageWiseRpt(string Panchayatcode)
    {
        try
        {

            //SqlParameter GetQuery = new SqlParameter("@QueryType", "5");
            //SqlParameter GetDivisionCode = new SqlParameter("@DivisionCode", "0");
            //SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", "0");
            //SqlParameter GetSubDivisionCode = new SqlParameter("@SubDivisionCode", "0");
            //SqlParameter GetBlockCode = new SqlParameter("@BlockCode", "0");
            //SqlParameter GetThanCode = new SqlParameter("@ThanCode", "0");
            //SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Panchayatcode.Trim());
            //SqlParameter GetVillageCode = new SqlParameter("@VillageCode", "0");
            //SqlParameter GetWardCode = new SqlParameter("@WardCode", "0");

            //DataTable dt = clsData.GetDataTableWithProc("Sp_GetApplicationConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, GetDistrictCode, GetSubDivisionCode, GetBlockCode, GetThanCode, GetPanchayatCode, GetVillageCode, GetWardCode });

            //lblDateTime4.Text = DateTime.Now.ToString();
            //if (dt.Rows.Count > 0)
            //{
            //    pnlpanchayat.Visible = true;
            //    pnlthana.Visible = false;
            //    pnlCircle.Visible = false;
            //    pnlDistrict.Visible = false;
            //    pnlSubDivision.Visible = false;
            //    lblDetail3.Visible = false;
            //    lblDetail5.Visible = true;
            //    lblDetail2.Visible = false;
            //    btn_Export.Visible = false;
            //    lblPrintDate.Visible = false;
            //    lblDetail.Visible = false;
            //    grdPanchayat.Columns[1].FooterText = "Total :";
            //    grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalApplication")).Sum().ToString();


            //    grdPanchayat.DataSource = dt;
            //    grdPanchayat.DataBind();
            //}
            //else
            //{

            //    grdPanchayat.DataSource = dt;
            //    grdPanchayat.DataBind();
            //}
            //btnback.Visible = true;

            //pnlDist.Visible = false;
            //grd_Division.Visible = false;

            //pnlDistrict.Visible = false;
            //grdDistrict.Visible = false;

            //pnlSubDivision.Visible = false;
            //grdSubDivision.Visible = false;

            //pnlCircle.Visible = false;
            //grdCircle.Visible = false;

            //pnlthana.Visible = false;
            //grdThana.Visible = false;

            //pnlpanchayat.Visible = true;
            //grdPanchayat.Visible = true;



        }
        catch (Exception ex)
        { }
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
        for (int i = 0; i < grd_Division.FooterRow.Cells.Count; i++)
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
            if (j <= grd_Division.Rows.Count)
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

            if (pnlDistrict.Visible == true && grdDistrict.Visible == true)
            {
                lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString();

                lbltext.Visible = false;

                btnback.Visible = false;
                btn_Export.Visible = true;

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
            if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
            {
                btnback.Visible = true;
                btn_Export.Visible = true;
                lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString() + ",District Name :- " + ViewState["DistrictName"].ToString();

                lbltext.Visible = true;

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
            if (pnlCircle.Visible == true && grdCircle.Visible == true)
            {
                lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString() + ",District Name :- " + ViewState["DistrictName"].ToString() + ",<br> Sub-DivisionName :- " + ViewState["SubDivisionName"].ToString();

                lbltext.Visible = true;
                btnback.Visible = true;
                btn_Export.Visible = true;

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
            if (pnlpanchayat.Visible == true && grdPanchayat.Visible == true)
            {
                lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString() + ",District Name :- " + ViewState["DistrictName"].ToString() + ",<br> Sub-DivisionName :- " + ViewState["SubDivisionName"].ToString() + ",Circle/Block :-" + ViewState["CircleBlock"].ToString();

                lbltext.Visible = true;
                btnback.Visible = true;
                btn_Export.Visible = true;

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

            if (Pnlsearch.Visible == true && GridView1.Visible == true)
            {
                lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString() + ",District Name :- " + ViewState["DistrictName"].ToString() + ",<br> Sub-DivisionName :- " + ViewState["SubDivisionName"].ToString() + ",Circle/Block :-" + ViewState["CircleBlock"].ToString();

                lbltext.Visible = true;
                btnback.Visible = true;
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

                pnlpanchayat.Visible = true;
                grdPanchayat.Visible = true;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
            }
        }
        catch (Exception er)
        {

        }
    }

    protected void btnExpToExl2_Click(object sender, EventArgs e)
    {
        btnback.Visible = false;
        lblDateTime1.Visible = true;
        lblDetail2.Visible = false;
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", DateTime.Now.ToString("ddMMyyhhmmss") + "DistrictWiseApplicationReport.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        for (int i = 0; i < grdDistrict.HeaderRow.Cells.Count; i++)
        {
            grdDistrict.HeaderRow.Cells[i].Style.Add("border-style", "Solid");
            grdDistrict.HeaderRow.Cells[i].Style.Add("border-color", "Black");
            grdDistrict.HeaderRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            grdDistrict.HeaderRow.Cells[i].Style.Add("Font-Bold", "True");
            grdDistrict.HeaderRow.Cells[i].Style.Add("Fore-Color", "Black");

            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
        }
        for (int i = 0; i < grdDistrict.FooterRow.Cells.Count; i++)
        {
            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
            grdDistrict.FooterRow.Cells[i].Style.Add("border-style", "Solid");
            grdDistrict.FooterRow.Cells[i].Style.Add("border-color", "Black");
            grdDistrict.FooterRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            grdDistrict.FooterRow.Cells[i].Style.Add("Font-Bold", "True");
            grdDistrict.FooterRow.Cells[i].Style.Add("Fore-Color", "Black");
        }
        int j = 1;
        //   This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in grdDistrict.Rows)
        {
            gvrow.BackColor = System.Drawing.Color.White;
            if (j <= grdDistrict.Rows.Count)
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
        pnlDistrict.RenderControl(htw);
        string dd = sw.ToString();
        dd = dd.Replace("href=", "");
        Response.Write(dd);
        Response.End();
    }



    protected void btnExpToExl3_Click(object sender, EventArgs e)
    {
        btnback.Visible = false;
        lblDateTime2.Visible = true;
        btn_Export.Visible = false;
        lblDetail3.Visible = false;
        lblDetail2.Visible = false;
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", DateTime.Now.ToString("ddMMyyhhmmss") + "SubDivisionWiseApplicationReport.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        for (int i = 0; i < grdSubDivision.HeaderRow.Cells.Count; i++)
        {
            grdSubDivision.HeaderRow.Cells[i].Style.Add("border-style", "Solid");
            grdSubDivision.HeaderRow.Cells[i].Style.Add("border-color", "Black");
            grdSubDivision.HeaderRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            grdSubDivision.HeaderRow.Cells[i].Style.Add("Font-Bold", "True");
            grdSubDivision.HeaderRow.Cells[i].Style.Add("Fore-Color", "Black");

            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
        }
        for (int i = 0; i < grdSubDivision.FooterRow.Cells.Count; i++)
        {
            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
            grdSubDivision.FooterRow.Cells[i].Style.Add("border-style", "Solid");
            grdSubDivision.FooterRow.Cells[i].Style.Add("border-color", "Black");
            grdSubDivision.FooterRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            grdSubDivision.FooterRow.Cells[i].Style.Add("Font-Bold", "True");
            grdSubDivision.FooterRow.Cells[i].Style.Add("Fore-Color", "Black");
        }
        int j = 1;
        //   This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in grdSubDivision.Rows)
        {
            gvrow.BackColor = System.Drawing.Color.White;
            if (j <= grdSubDivision.Rows.Count)
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

        pnlSubDivision.RenderControl(htw);
        string dd = sw.ToString();
        dd = dd.Replace("href=", "");
        Response.Write(dd);
        Response.End();
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

}