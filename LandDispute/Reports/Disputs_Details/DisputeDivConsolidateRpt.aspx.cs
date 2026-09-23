using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class LandDispute_Reports_Disputs_Details_DisputeDivConsolidateRpt : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"] != null)
            {
                ViewState["lnk_DivisionCode"] = "";
                ViewState["lnk_DistrictCode"] = "";
                ViewState["lnk_SubDivisionCode"] = "";
                ViewState["lnk_Block_Code"] = "";
                ViewState["lnk_Thana_Code"] = "";
                ViewState["lnk_Panchayat_Code"] = "";
                ViewState["lnkClick"] = "";
                ViewState["PageIndex"] = "1";
                ViewState["BlockCode"] = "";
                ViewState["ThanaCode"] = "";
                ViewState["RoleStepBack"] = "";
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
                    getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"].ToString()));
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
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

            DataTable dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQuery, FromDate , Todate });
            lblPrintDate.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;             
                lblDetail.Visible = true;
                grd_Division.Columns[1].FooterText = "Total :";
                grd_Division.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_Division.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grd_Division.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grd_Division.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ParchadhariBedakli")).Sum().ToString();
                grd_Division.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SarkariBhumiKabza")).Sum().ToString();
                grd_Division.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiSeema")).Sum().ToString();
                grd_Division.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("NijiRastaNali")).Sum().ToString();
                grd_Division.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("JalStrot")).Sum().ToString();
                grd_Division.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("PatrikBhumiBatwara")).Sum().ToString();
                grd_Division.Columns[11].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("KhetiVivad")).Sum().ToString();
                grd_Division.Columns[12].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VaasVivad")).Sum().ToString();
                grd_Division.Columns[13].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LaganNirdharan")).Sum().ToString();
                grd_Division.Columns[14].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VyawsaikBhumi")).Sum().ToString();
                grd_Division.Columns[15].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BadlenBhumi")).Sum().ToString();
                grd_Division.Columns[16].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuArjan")).Sum().ToString();
                grd_Division.Columns[17].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuHdBandi")).Sum().ToString();
                grd_Division.Columns[18].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiKabza")).Sum().ToString();
                grd_Division.Columns[19].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Anya")).Sum().ToString();
                grd_Division.DataSource = dt;
                grd_Division.DataBind();
            }
            else
            {
                grd_Division.DataSource = dt;
                grd_Division.DataBind();
            }
        

            pnlDivision.Visible = true;
            grd_Division.Visible = true;

            pnlDistrict.Visible = false;
            grd_District.Visible = false;

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
        }
        catch (Exception ex)
        { }
    }
    protected void grd_Division_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DivClick")
        {
            string Total = e.CommandArgument.ToString().Split(',')[2];
            if (Convert.ToInt64(Total)!= 0)
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
            SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToString(Session["RangeCode"]));
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQuery, GetDivisionCode, FromDate, Todate , Rangeid });
            lblDateTime1.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

                grd_District.Columns[1].FooterText = "Total :";
                grd_District.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grd_District.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grd_District.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grd_District.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ParchadhariBedakli")).Sum().ToString();
                grd_District.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SarkariBhumiKabza")).Sum().ToString();
                grd_District.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiSeema")).Sum().ToString();
                grd_District.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("NijiRastaNali")).Sum().ToString();
                grd_District.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("JalStrot")).Sum().ToString();
                grd_District.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("PatrikBhumiBatwara")).Sum().ToString();
                grd_District.Columns[11].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("KhetiVivad")).Sum().ToString();
                grd_District.Columns[12].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VaasVivad")).Sum().ToString();
                grd_District.Columns[13].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LaganNirdharan")).Sum().ToString();
                grd_District.Columns[14].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VyawsaikBhumi")).Sum().ToString();
                grd_District.Columns[15].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BadlenBhumi")).Sum().ToString();
                grd_District.Columns[16].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuArjan")).Sum().ToString();
                grd_District.Columns[17].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuHdBandi")).Sum().ToString();
                grd_District.Columns[18].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiKabza")).Sum().ToString();
                grd_District.Columns[19].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Anya")).Sum().ToString();
                grd_District.DataSource = dt;
                grd_District.DataBind();
            }
            else
            {

                grd_District.DataSource = dt;
                grd_District.DataBind();
            }
          
            pnlDivision.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = true;
            grd_District.Visible = true;

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

        }
        catch (Exception ex)
        { }
    }
    protected void grd_District_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
        if (e.CommandName == "DstClick")
        {
            string Total = e.CommandArgument.ToString().Split(',')[2];
            if (Convert.ToInt64(Total) != 0)
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
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQuery, GetDistrictCode, FromDate, Todate });
            lblDateTime2.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdSubDivision.Columns[1].FooterText = "Total :";
                grdSubDivision.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdSubDivision.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdSubDivision.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdSubDivision.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ParchadhariBedakli")).Sum().ToString();
                grdSubDivision.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SarkariBhumiKabza")).Sum().ToString();
                grdSubDivision.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiSeema")).Sum().ToString();
                grdSubDivision.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("NijiRastaNali")).Sum().ToString();
                grdSubDivision.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("JalStrot")).Sum().ToString();
                grdSubDivision.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("PatrikBhumiBatwara")).Sum().ToString();
                grdSubDivision.Columns[11].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("KhetiVivad")).Sum().ToString();
                grdSubDivision.Columns[12].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VaasVivad")).Sum().ToString();
                grdSubDivision.Columns[13].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LaganNirdharan")).Sum().ToString();
                grdSubDivision.Columns[14].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VyawsaikBhumi")).Sum().ToString();
                grdSubDivision.Columns[15].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BadlenBhumi")).Sum().ToString();
                grdSubDivision.Columns[16].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuArjan")).Sum().ToString();
                grdSubDivision.Columns[17].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuHdBandi")).Sum().ToString();
                grdSubDivision.Columns[18].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiKabza")).Sum().ToString();
                grdSubDivision.Columns[19].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Anya")).Sum().ToString();
                grdSubDivision.DataSource = dt;
                grdSubDivision.DataBind();
            }
            else
            {

                grdSubDivision.DataSource = dt;
                grdSubDivision.DataBind();
            }
          

            pnlDivision.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grd_District.Visible = false;

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
        }
        catch (Exception ex)
        { }
    }
    protected void grdSubDivision_RowCommand(object sender, GridViewCommandEventArgs e)
    {      
        if (e.CommandName == "sdClick")
        {
            string Total = e.CommandArgument.ToString().Split(',')[2];
            if (Convert.ToInt64(Total) != 0)
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
            SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", "0");          
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

            DataTable dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQuery,  GetSubDivisionCode, GetPanchayatCode, FromDate, Todate });

            lblDateTime3.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

                grdCircle.Columns[1].FooterText = "Total :";
                grdCircle.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdCircle.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdCircle.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdCircle.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ParchadhariBedakli")).Sum().ToString();
                grdCircle.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SarkariBhumiKabza")).Sum().ToString();
                grdCircle.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiSeema")).Sum().ToString();
                grdCircle.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("NijiRastaNali")).Sum().ToString();
                grdCircle.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("JalStrot")).Sum().ToString();
                grdCircle.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("PatrikBhumiBatwara")).Sum().ToString();
                grdCircle.Columns[11].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("KhetiVivad")).Sum().ToString();
                grdCircle.Columns[12].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VaasVivad")).Sum().ToString();
                grdCircle.Columns[13].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LaganNirdharan")).Sum().ToString();
                grdCircle.Columns[14].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VyawsaikBhumi")).Sum().ToString();
                grdCircle.Columns[15].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BadlenBhumi")).Sum().ToString();
                grdCircle.Columns[16].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuArjan")).Sum().ToString();
                grdCircle.Columns[17].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuHdBandi")).Sum().ToString();
                grdCircle.Columns[18].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiKabza")).Sum().ToString();
                grdCircle.Columns[19].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Anya")).Sum().ToString();
                grdCircle.DataSource = dt;
                grdCircle.DataBind();

            }
            else
            {

                grdCircle.DataSource = dt;
                grdCircle.DataBind();
            }
         
            pnlDivision.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grd_District.Visible = false;

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

        }
        catch (Exception ex)
        { }
    }
    protected void grdCircle_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CircleClick")
        {
            string Total = e.CommandArgument.ToString().Split(',')[2];
            if (Convert.ToInt64(Total) != 0)
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
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

            DataTable dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQuery,  GetBlockCode, FromDate, Todate });

            lblDateTime4.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
                grdThana.Columns[1].FooterText = "Total :";
                grdThana.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdThana.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdThana.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdThana.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ParchadhariBedakli")).Sum().ToString();
                grdThana.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SarkariBhumiKabza")).Sum().ToString();
                grdThana.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiSeema")).Sum().ToString();
                grdThana.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("NijiRastaNali")).Sum().ToString();
                grdThana.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("JalStrot")).Sum().ToString();
                grdThana.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("PatrikBhumiBatwara")).Sum().ToString();
                grdThana.Columns[11].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("KhetiVivad")).Sum().ToString();
                grdThana.Columns[12].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VaasVivad")).Sum().ToString();
                grdThana.Columns[13].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LaganNirdharan")).Sum().ToString();
                grdThana.Columns[14].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VyawsaikBhumi")).Sum().ToString();
                grdThana.Columns[15].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BadlenBhumi")).Sum().ToString();
                grdThana.Columns[16].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuArjan")).Sum().ToString();
                grdThana.Columns[17].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuHdBandi")).Sum().ToString();
                grdThana.Columns[18].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiKabza")).Sum().ToString();
                grdThana.Columns[19].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Anya")).Sum().ToString();
                grdThana.DataSource = dt;
                grdThana.DataBind();

            }
            else
            {

                grdThana.DataSource = dt;
                grdThana.DataBind();
            }
            ////btnback.Visible = true;

            pnlDivision.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grd_District.Visible = false;

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
        }
        catch (Exception ex)
        { }
    }
    protected void grdThana_RowCommand(object sender, GridViewCommandEventArgs e)
    {      
        if (e.CommandName == "ThanaClick")
        {
            string Total = e.CommandArgument.ToString().Split(',')[2];
            if (Convert.ToInt64(Total) != 0)
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

    protected void getPanchayatWiseRpt(string thanacode)
    {
        try
        {
            SqlParameter GetQuery = new SqlParameter("@QueryType", "6");
            SqlParameter GetThanCode = new SqlParameter("@ThanCode", thanacode.Trim());       
            SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));

            DataTable dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQuery,  GetThanCode,  FromDate, Todate });

            lblDateTime5.Text = DateTime.Now.ToString();
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;
              
                grdPanchayat.Columns[1].FooterText = "Total :";
                grdPanchayat.Columns[2].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                grdPanchayat.Columns[3].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grdPanchayat.Columns[4].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                grdPanchayat.Columns[5].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ParchadhariBedakli")).Sum().ToString();
                grdPanchayat.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SarkariBhumiKabza")).Sum().ToString();
                grdPanchayat.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiSeema")).Sum().ToString();
                grdPanchayat.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("NijiRastaNali")).Sum().ToString();
                grdPanchayat.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("JalStrot")).Sum().ToString();
                grdPanchayat.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("PatrikBhumiBatwara")).Sum().ToString();
                grdPanchayat.Columns[11].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("KhetiVivad")).Sum().ToString();
                grdPanchayat.Columns[12].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VaasVivad")).Sum().ToString();
                grdPanchayat.Columns[13].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LaganNirdharan")).Sum().ToString();
                grdPanchayat.Columns[14].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VyawsaikBhumi")).Sum().ToString();
                grdPanchayat.Columns[15].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BadlenBhumi")).Sum().ToString();
                grdPanchayat.Columns[16].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuArjan")).Sum().ToString();
                grdPanchayat.Columns[17].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuHdBandi")).Sum().ToString();
                grdPanchayat.Columns[18].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiKabza")).Sum().ToString();
                grdPanchayat.Columns[19].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Anya")).Sum().ToString();
                grdPanchayat.DataSource = dt;
                grdPanchayat.DataBind();
            }
            else
            {
                grdPanchayat.DataSource = dt;
                grdPanchayat.DataBind();
            }
         

            pnlDivision.Visible = false;
            grd_Division.Visible = false;

            pnlDistrict.Visible = false;
            grd_District.Visible = false;

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
        catch (Exception ex)
        { }
    }
    protected void grdPanchayat_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        if (e.CommandName == "PanchayatClick")
        {
            string DivisionCode = e.CommandArgument.ToString().Trim().Split(',')[0];
            string DistrictCode = e.CommandArgument.ToString().Trim().Split(',')[1];
            string SubDivisionCode = e.CommandArgument.ToString().Trim().Split(',')[2];
            string BlockCode = e.CommandArgument.ToString().Trim().Split(',')[3];
            string ThanaCode = e.CommandArgument.ToString().Trim().Split(',')[4];
            string PanchayatCode = e.CommandArgument.ToString().Trim().Split(',')[5];
            string Total = e.CommandArgument.ToString().Trim().Split(',')[7];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            ViewState["lnk_Block_Code"] = BlockCode;
            ViewState["lnk_Thana_Code"] = ThanaCode;
            ViewState["lnk_Panchayat_Code"] = PanchayatCode;
            if (Convert.ToInt64(Total) != 0)
            {
                char[] seprator = { ',' };
                ViewState["PanchayatName"] = e.CommandArgument.ToString().Trim().Split(seprator)[6];
                //ViewState["thana"]= ViewState["thana"].ToString() == "" ? "" : "<br>Thana :-" + ViewState["thana"].ToString();
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
                getPanchayatWiseData();
            }
        }
    }

    private void getPanchayatWiseData()
    {
        try
        {
            ViewState["lnkClick"] = "lnkTotalpanchayat_Click";
            lnkPanchayats();
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

    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }

    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {
            if (pnlDivision.Visible == true && grd_Division.Visible == true)
            {
                ExportExcel(grd_Division, pnlDivision);
            }
            if (pnlDistrict.Visible == true && grd_District.Visible == true)
            {
                ExportExcel(grd_District, pnlDistrict);
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
            ViewState["lnk_DivisionCode"] = "";
            ViewState["lnk_DistrictCode"] = "";
            ViewState["lnk_SubDivisionCode"] = "";
            ViewState["lnk_Block_Code"] = "";
            ViewState["lnk_Thana_Code"] = "";
            ViewState["lnk_Panchayat_Code"] = "";
            ViewState["PageIndex"] = "1";
            rptPager.DataSource = null;
            rptPager.DataBind();
            if ((ViewState["lnkClick"].ToString() == "lnkTotalDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkDivisionFinalize_Click")
                || (ViewState["lnkClick"].ToString() == "lnkDivisionUnFinalize_Click")
                || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Division_Click")
                || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkJalStrotDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkVaasVivadDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBhuArjanDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkAnyaDivision_Click"))
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
                pnlDivision.Visible = true;
                grd_Division.Visible = true;


                pnlDistrict.Visible = false;
                grd_District.Visible = false;

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
            }
            else if (ViewState["lnkClick"].ToString() == "lnkTotalDistrict_Click"
                        || (ViewState["lnkClick"].ToString() == "lnkDistrictFinalize_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkDistrictUnFinalize_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_District_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkJalStrotDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkVaasVivadDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBhuArjanDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaDistrict_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkAnyaDistrict_Click"))
            {
                ViewState["lnkClick"] = "";
                if ( ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = false;
                }
                else
                {
                    btnback.Visible = true;
                }
                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = true;
                grd_District.Visible = true;


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
            }
            else if ((ViewState["lnkClick"].ToString() == "lnkTotalSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalize_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkSubDivisionUnFinalize_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_SubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkJalStrotSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkVaasVivadSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkBhuArjanSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkAnyaSubDivision_Click")
                     )
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
                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;

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
            }
            else if ((ViewState["lnkClick"].ToString() == "lnkAnyaBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBlockFinalize_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBlockUnFinalize_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBhuArjanBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkVaasVivadBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkJalStrotBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaBlock_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Block_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkTotalBlock_Click"))
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
                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;

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
            }
            else if ((ViewState["lnkClick"].ToString() == "lnkAnyaThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkThanaFinalize_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkThanaUnFinalize_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkBhuArjanThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkVaasVivadThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkJalStrotThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaThana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Thana_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkTotalThana_Click"))
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
                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;


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
            }
            else if ((ViewState["lnkClick"].ToString() == "lnkAnyapanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzapanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandipanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBhuArjanpanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumipanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumipanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanpanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkVaasVivadpanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadpanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwarapanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkJalStrotpanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNalipanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemapanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzapanchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_panchayat_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkPanchayatsUnFinalize_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalize_Click")
                        || (ViewState["lnkClick"].ToString() == "lnkTotalpanchayat_Click"))
            {
                ViewState["lnkClick"] = "";
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = lbltext.Text = "Division Name:-" + ViewState["DIVISIONName"].ToString() + ",District Name:-" + ViewState["DistrictName"].ToString() + "<br/>SubDivisionName:-"
                       + ViewState["SubDivisionName"].ToString() + ",CircleBlock:-" + ViewState["BlockName"].ToString() + "<br/>" + "Thana:-" + ViewState["thana"].ToString();
                   
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = true;
                    lbltext.Text = "District Name :- " + ViewState["DistrictName"].ToString() +
                                         ",Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString()
                                         + "<br/>CircleBlock:-" + ViewState["BlockName"].ToString()
                                         + ",Thana:-" + ViewState["thana"].ToString();
                    
                }
                else if (ViewState["RoleStepBack"].ToString() == "3")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Sub-DivisionName :-" + ViewState["SubDivisionName"].ToString() +
                                   ",CircleBlock:-" + ViewState["BlockName"].ToString()
                                   + "<br/>Thana:-" + ViewState["thana"].ToString();
                  
                }
                else if (ViewState["RoleStepBack"].ToString() == "2")
                {
                    btnback.Visible = true;
                    lbltext.Text = "CircleBlock:-" + ViewState["BlockName"].ToString()
                                    + ",Thana:-" + ViewState["thana"].ToString();
                   
                }
                else if (ViewState["RoleStepBack"].ToString() == "1")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Thana:-" + ViewState["thana"].ToString();
                   
                }
                else if (ViewState["RoleStepBack"].ToString() == "0")
                {
                    btnback.Visible = false;
                }
                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;


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
            else if (pnlDistrict.Visible == true && grd_District.Visible == true)
            {
                lbltext.Visible = false;
                btn_Export.Visible = true;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;

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
                    lbltext.Text = "";
                    pnlDivision.Visible = true;
                    grd_Division.Visible = true;
                }
                else
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlDivision.Visible = false;
                    grd_Division.Visible = false;
                }
            }
            else if (pnlSubDivision.Visible == true && grdSubDivision.Visible == true)
            {

                btn_Export.Visible = true;
                lbltext.Visible = true;

                pnlDivision.Visible = false;
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
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = "Division Name :- " + ViewState["DIVISIONName"].ToString();
                    pnlDistrict.Visible = true;
                    grd_District.Visible = true;
                }
                else if (ViewState["RoleStepBack"].ToString() == "4")
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlDistrict.Visible = true;
                    grd_District.Visible = true;
                }
                else
                {
                    btnback.Visible = false;
                    lbltext.Text = "";
                    pnlDistrict.Visible = false;
                    grd_District.Visible = false;
                }

            }
            else if (pnlCircle.Visible == true && grdCircle.Visible == true)
            {
                btn_Export.Visible = true;

                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;

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

                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;


                pnlthana.Visible = false;
                grdThana.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
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

                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlpanchayat.Visible = false;
                grdPanchayat.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
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

                pnlDivision.Visible = false;
                grd_Division.Visible = false;

                pnlDistrict.Visible = false;
                grd_District.Visible = false;

                pnlSubDivision.Visible = false;
                grdSubDivision.Visible = false;

                pnlCircle.Visible = false;
                grdCircle.Visible = false;

                pnlthana.Visible = false;
                grdThana.Visible = false;

                Pnlsearch.Visible = false;
                GridView1.Visible = false;
                if (ViewState["RoleStepBack"].ToString() == "5")
                {
                    btnback.Visible = true;
                    lbltext.Text = lbltext.Text = "Division Name:-" + ViewState["DIVISIONName"].ToString() + ",District Name:-" + ViewState["DistrictName"].ToString() + "<br/>SubDivisionName:-"
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
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["lnk_DivisionCode"] = "";
        ViewState["lnk_DistrictCode"] = "";
        ViewState["lnk_SubDivisionCode"] = "";
        ViewState["lnk_Block_Code"] = "";
        ViewState["lnk_Thana_Code"] = "";
        ViewState["lnk_Panchayat_Code"] = "";
        ViewState["lnkClick"] = "";
        ViewState["PageIndex"] = "1";
        if (Session["Role"].ToString() == "HQ" || Session["Role"].ToString().Trim() == "ADMHOME" || Session["Role"].ToString() == "ADMLR")
        {
            bindDivision();
        }
        else if (Session["Role"].ToString() == "COM")
        {
            getDistrictWiseRpt(Convert.ToString(Session["Commsionary_Code"]));
        }
        else if (Session["Role"].ToString() == "DPGRO" || Session["Role"].ToString() == "DMOPT" || Session["Role"].ToString() == "SSPOPT" || Session["Role"].ToString() == "ADM")
        {
            getSubDivisionWiseRpt(Convert.ToString(Session["District_Code"]));
        }
        else if (Session["Role"].ToString() == "SDPGRO" || Session["Role"].ToString() == "SDOOPT" || Session["Role"].ToString() == "DSPOPT")
        {
            getCircleWiseRpt(Convert.ToString(Session["Sub_DivCode"]));
        }
        else if (Session["Role"].ToString() == "COOPT")
        {
            getThanaWiseRpt(Session["Block_Code"].ToString());
        }
        else if (Session["Role"].ToString() == "SHOOPT")
        {
            getPanchayatWiseRpt(Convert.ToString(Session["Thana_Code"].ToString()));
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

    protected void lnkDivision()
    {

        DataTable dt = null;
        SqlParameter _RecordCount = null;
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["lnk_DivisionCode"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalDivision_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
               FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDivisionUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Division_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaDivision_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkJalStrotDivision_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkKhetiVivadDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "7");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVaasVivadDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "8");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "9");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiDivision_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "10");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiDivision_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "11");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuArjanDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "12");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "13");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "15");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkAnyaDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "20");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
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

        pnlDivision.Visible = false;
        grd_Division.Visible = false;

        pnlSubDivision.Visible = false;
        grdSubDivision.Visible = false;

        pnlDistrict.Visible = false;
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
    protected void lnkTotalDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkTotalDivision_Click";
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
            ViewState["lnk_DivisionCode"] = DivisionCode;
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
            ViewState["lnk_DivisionCode"] = DivisionCode;
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
    protected void lnkParchadhariBedakli_Division_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkParchadhariBedakli_Division_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkSarkariBhumiKabzaDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSarkariBhumiKabzaDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkRaitiBhumiSeemaDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkRaitiBhumiSeemaDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkNijiRastaNaliDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkNijiRastaNaliDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkJalStrotDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkJalStrotDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkPatrikBhumiBatwaraDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkPatrikBhumiBatwaraDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkKhetiVivadDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkKhetiVivadDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkVaasVivadDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkVaasVivadDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkLaganNirdharanDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkLaganNirdharanDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkVyawsaikBhumiDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkVyawsaikBhumiDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkBadlenBhumiDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkBadlenBhumiDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkBhuArjanDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkBhuArjanDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkBhuHdBandiDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkBhuHdBandiDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkRaitiBhumiKabzaDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkRaitiBhumiKabzaDivision_Click";
                lnkDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkAnyaDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string Total = linkbtn.CommandArgument.Split(',')[1];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkAnyaDivision_Click";
                lnkDivision();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void lnkDistrcict()
    {

        DataTable dt = null;
        SqlParameter _RecordCount = null;
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["lnk_DivisionCode"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["lnk_DistrictCode"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalDistrict_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
               GetDistrictCode,FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkDistrictUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_District_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaDistrict_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkJalStrotDistrict_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkKhetiVivadDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "7");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVaasVivadDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "8");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "9");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiDistrict_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "10");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiDistrict_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "11");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuArjanDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "12");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "13");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "15");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkAnyaDistrict_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "20");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
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

        pnlDivision.Visible = false;
        grd_Division.Visible = false;

        pnlSubDivision.Visible = false;
        grdSubDivision.Visible = false;

        pnlDistrict.Visible = false;
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
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
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
    protected void lnkDistrictFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictFinalize_Click";
                lnkDistrcict();
            }

        }
        catch (Exception ex)
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
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkDistrictUnFinalize_Click";
                lnkDistrcict();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkParchadhariBedakli_District_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkParchadhariBedakli_District_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkSarkariBhumiKabzaDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkSarkariBhumiKabzaDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkRaitiBhumiSeemaDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkRaitiBhumiSeemaDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkNijiRastaNaliDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkNijiRastaNaliDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkJalStrotDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkJalStrotDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkPatrikBhumiBatwaraDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkPatrikBhumiBatwaraDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkKhetiVivadDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkKhetiVivadDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkVaasVivadDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkVaasVivadDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkLaganNirdharanDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkLaganNirdharanDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }

    }
    protected void lnkVyawsaikBhumiDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkVyawsaikBhumiDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkBadlenBhumiDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkBadlenBhumiDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkBhuArjanDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkBhuArjanDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkBhuHdBandiDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {

                ViewState["lnkClick"] = "lnkBhuHdBandiDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkRaitiBhumiKabzaDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkRaitiBhumiKabzaDistrict_Click";
                lnkDistrcict();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void lnkAnyaDistrict_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string Total = linkbtn.CommandArgument.Split(',')[2];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkAnyaDistrict_Click";
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
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
        SqlParameter GetDivisionCode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["lnk_DivisionCode"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["lnk_DistrictCode"].ToString()));
        SqlParameter GetSubdivisionCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ViewState["lnk_SubDivisionCode"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalSubDivision_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
               GetDistrictCode,GetSubdivisionCode,FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSubDivisionUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_SubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaSubDivision_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkJalStrotSubDivision_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkKhetiVivadSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "7");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVaasVivadSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "8");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "9");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiSubDivision_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "10");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiSubDivision_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "11");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuArjanSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "12");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "13");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "15");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkAnyaSubDivision_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "20");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,GetDivisionCode,
                GetDistrictCode,GetSubdivisionCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
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

        pnlDivision.Visible = false;
        grd_Division.Visible = false;

        pnlSubDivision.Visible = false;
        grdSubDivision.Visible = false;

        pnlDistrict.Visible = false;
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
    protected void lnkTotalSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkTotalSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
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
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionFinalize_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
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
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSubDivisionUnFinalize_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkParchadhariBedakli_SubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkParchadhariBedakli_SubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkSarkariBhumiKabzaSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkSarkariBhumiKabzaSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkRaitiBhumiSeemaSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkRaitiBhumiSeemaSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkNijiRastaNaliSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkNijiRastaNaliSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkJalStrotSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkJalStrotSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkPatrikBhumiBatwaraSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkPatrikBhumiBatwaraSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkKhetiVivadSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkKhetiVivadSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkVaasVivadSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkVaasVivadSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkLaganNirdharanSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkLaganNirdharanSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkVyawsaikBhumiSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkVyawsaikBhumiSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkBadlenBhumiSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkBadlenBhumiSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkBhuArjanSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkBhuArjanSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkBhuHdBandiSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkBhuHdBandiSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkRaitiBhumiKabzaSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkRaitiBhumiKabzaSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void lnkAnyaSubDivision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton linkbtn = sender as LinkButton;
            string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
            string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
            string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
            string Total = linkbtn.CommandArgument.Split(',')[3];
            ViewState["lnk_DivisionCode"] = DivisionCode;
            ViewState["lnk_DistrictCode"] = DistrictCode;
            ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
            if (Convert.ToInt32(Total) > 0)
            {
                ViewState["lnkClick"] = "lnkAnyaSubDivision_Click";
                lnkSubDivision();
            }

        }
        catch (Exception ex)
        {

        }
    }

 

    protected void lnkBlock()
    {

        DataTable dt = null;
        SqlParameter _RecordCount = null;
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
        SqlParameter GetDivisioncode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["lnk_DivisionCode"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["lnk_DistrictCode"].ToString()));
        SqlParameter GetSubdivisionCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ViewState["lnk_SubDivisionCode"].ToString()));
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ViewState["lnk_Block_Code"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalBlock_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
               GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBlockUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
               GetDivisioncode,GetDistrictCode,GetSubdivisionCode,  GetBlockCode,
                FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Block_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode
                ,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
               GetDivisioncode,GetDistrictCode,GetSubdivisionCode,  GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkJalStrotBlock_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkKhetiVivadBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "7");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVaasVivadBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "8");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "9");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiBlock_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "10");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "11");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuArjanBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "12");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "13");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaBlock_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "15");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkAnyaBlock_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "20");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode, GetBlockCode,
                FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
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

        pnlDivision.Visible = false;
        grd_Division.Visible = false;

        pnlSubDivision.Visible = false;
        grdSubDivision.Visible = false;

        pnlDistrict.Visible = false;
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
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkTotalBlock_Click";
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
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
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
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBlockUnFinalize_Click";
            lnkBlock();
        }

    }
    protected void lnkParchadhariBedakli_Block_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkParchadhariBedakli_Block_Click";
            lnkBlock();
        }
    }
    protected void lnkSarkariBhumiKabzaBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSarkariBhumiKabzaBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkRaitiBhumiSeemaBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkRaitiBhumiSeemaBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkNijiRastaNaliBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkNijiRastaNaliBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkJalStrotBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkJalStrotBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkPatrikBhumiBatwaraBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPatrikBhumiBatwaraBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkKhetiVivadBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkKhetiVivadBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkVaasVivadBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkVaasVivadBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkLaganNirdharanBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkLaganNirdharanBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkVyawsaikBhumiBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkVyawsaikBhumiBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkBadlenBhumiBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBadlenBhumiBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkBhuArjanBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBhuArjanBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkBhuHdBandiBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBhuHdBandiBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkRaitiBhumiKabzaBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkRaitiBhumiKabzaBlock_Click";
            lnkBlock();
        }
    }
    protected void lnkAnyaBlock_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string Total = linkbtn.CommandArgument.Split(',')[4];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkAnyaBlock_Click";
            lnkBlock();
        }
    }

    protected void lnkThana()
    {

        DataTable dt = null;
        SqlParameter _RecordCount = null;
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
        SqlParameter GetDivisioncode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["lnk_DivisionCode"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["lnk_DistrictCode"].ToString()));
        SqlParameter GetSubdivisionCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ViewState["lnk_SubDivisionCode"].ToString()));
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ViewState["lnk_Block_Code"].ToString()));
        SqlParameter GetThana_Code = new SqlParameter("@ThanCode", Convert.ToInt32(ViewState["lnk_Thana_Code"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalThana_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,
                FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,
                Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkThanaUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Thana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode, GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkJalStrotThana_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkKhetiVivadThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "7");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVaasVivadThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "8");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "9");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiThana_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "10");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "11");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuArjanThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "12");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "13");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaThana_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "15");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                 GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkAnyaThana_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "20");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
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

        pnlDivision.Visible = false;
        grd_Division.Visible = false;

        pnlSubDivision.Visible = false;
        grdSubDivision.Visible = false;

        pnlDistrict.Visible = false;
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
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkTotalThana_Click";
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
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
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
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkThanaUnFinalize_Click";
            lnkThana();
        }

    }
    protected void lnkParchadhariBedakli_Thana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkParchadhariBedakli_Thana_Click";
            lnkThana();
        }
    }
    protected void lnkSarkariBhumiKabzaThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSarkariBhumiKabzaThana_Click";
            lnkThana();
        }
    }
    protected void lnkRaitiBhumiSeemaThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkRaitiBhumiSeemaThana_Click";
            lnkThana();
        }
    }
    protected void lnkNijiRastaNaliThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkNijiRastaNaliThana_Click";
            lnkThana();
        }
    }
    protected void lnkJalStrotThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkJalStrotThana_Click";
            lnkThana();
        }
    }
    protected void lnkPatrikBhumiBatwaraThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPatrikBhumiBatwaraThana_Click";
            lnkThana();
        }
    }
    protected void lnkKhetiVivadThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkKhetiVivadThana_Click";
            lnkThana();
        }
    }
    protected void lnkVaasVivadThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkVaasVivadThana_Click";
            lnkThana();
        }
    }
    protected void lnkLaganNirdharanThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkLaganNirdharanThana_Click";
            lnkThana();
        }
    }
    protected void lnkVyawsaikBhumiThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkVyawsaikBhumiThana_Click";
            lnkThana();
        }
    }
    protected void lnkBadlenBhumiThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBadlenBhumiThana_Click";
            lnkThana();
        }
    }
    protected void lnkBhuArjanThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBhuArjanThana_Click";
            lnkThana();
        }
    }
    protected void lnkBhuHdBandiThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBhuHdBandiThana_Click";
            lnkThana();
        }
    }
    protected void lnkRaitiBhumiKabzaThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkRaitiBhumiKabzaThana_Click";
            lnkThana();
        }
    }
    protected void lnkAnyaThana_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string Total = linkbtn.CommandArgument.Split(',')[5];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkAnyaThana_Click";
            lnkThana();
        }
    }

    protected void lnkPanchayats()
    {
        DataTable dt = null;
        SqlParameter _RecordCount = null;
        Session["mySearchAppData03"] = null;
        SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
        SqlParameter GetDivisioncode = new SqlParameter("@Divisioncode", Convert.ToInt32(ViewState["lnk_DivisionCode"].ToString()));
        SqlParameter GetDistrictCode = new SqlParameter("@DistrictCode", Convert.ToInt32(ViewState["lnk_DistrictCode"].ToString()));
        SqlParameter GetSubdivisionCode = new SqlParameter("@SubDivisionCode", Convert.ToInt32(ViewState["lnk_SubDivisionCode"].ToString()));
        SqlParameter GetBlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ViewState["lnk_Block_Code"].ToString()));
        SqlParameter GetThana_Code = new SqlParameter("@ThanCode", Convert.ToInt32(ViewState["lnk_Thana_Code"].ToString()));
        SqlParameter GetPanchayatCode = new SqlParameter("@PanchayatCode", Convert.ToInt32(ViewState["lnk_Panchayat_Code"].ToString()));
        SqlParameter FromDate = new SqlParameter("@FromDate", txtFromdate.Text.ToString() == "" ? null : Convert.ToDateTime(txtFromdate.Text).ToString("yyyy-MM-dd"));
        SqlParameter Todate = new SqlParameter("@ToDate", txTodate.Text.ToString() == "" ? null : Convert.ToDateTime(txTodate.Text).ToString("yyyy-MM-dd"));
        if (ViewState["lnkClick"].ToString() == "lnkTotalpanchayat_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
              GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode, FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPanchayatsUnFinalize_Click")
        {
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
               GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode, FromDate,Todate,Getfinaldata,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_panchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "1");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzapanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "2");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemapanchayat_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "3");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkNijiRastaNalipanchayat_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "4");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkJalStrotpanchayat_Click")
        {


            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "5");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwarapanchayat_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "6");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkKhetiVivadpanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "7");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVaasVivadpanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "8");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanpanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "9");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumipanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "10");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBadlenBhumipanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "11");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuArjanpanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "12");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkBhuHdBandipanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "13");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzapanchayat_Click")
        {
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "15");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
        }
        else if (ViewState["lnkClick"].ToString() == "lnkAnyapanchayat_Click")
        {

            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", "20");
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetDisputeConsolidateRpt", new SqlParameter[] { GetQueryType,
                GetDivisioncode,GetDistrictCode,GetSubdivisionCode,GetBlockCode,GetThana_Code,GetPanchayatCode,FromDate,Todate,GetBhumiVivadType,_PageSize,_PageIndex,_RecordCount });
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

        pnlDivision.Visible = false;
        grd_Division.Visible = false;

        pnlSubDivision.Visible = false;
        grdSubDivision.Visible = false;

        pnlDistrict.Visible = false;
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
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkTotalpanchayat_Click";
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
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
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
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPanchayatsUnFinalize_Click";
            lnkPanchayats();
        }

    }
    protected void lnkParchadhariBedakli_panchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkParchadhariBedakli_panchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkSarkariBhumiKabzapanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkSarkariBhumiKabzapanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkRaitiBhumiSeemapanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkRaitiBhumiSeemapanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkNijiRastaNalipanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkNijiRastaNalipanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkJalStrotpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkJalStrotpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkPatrikBhumiBatwarapanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkPatrikBhumiBatwarapanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkKhetiVivadpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkKhetiVivadpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkVaasVivadpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkVaasVivadpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkLaganNirdharanpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkLaganNirdharanpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkVyawsaikBhumipanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkVyawsaikBhumipanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkBadlenBhumipanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBadlenBhumipanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkBhuArjanpanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBhuArjanpanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkBhuHdBandipanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkBhuHdBandipanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkRaitiBhumiKabzapanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkRaitiBhumiKabzapanchayat_Click";
            lnkPanchayats();
        }
    }
    protected void lnkAnyapanchayat_Click(object sender, EventArgs e)
    {
        LinkButton linkbtn = sender as LinkButton;
        string DivisionCode = linkbtn.CommandArgument.Split(',')[0];
        string DistrictCode = linkbtn.CommandArgument.Split(',')[1];
        string SubDivisionCode = linkbtn.CommandArgument.Split(',')[2];
        string BlockCode = linkbtn.CommandArgument.Split(',')[3];
        string ThanaCode = linkbtn.CommandArgument.Split(',')[4];
        string PanchayatCode = linkbtn.CommandArgument.Split(',')[5];
        string Total = linkbtn.CommandArgument.Split(',')[6];
        ViewState["lnk_DivisionCode"] = DivisionCode;
        ViewState["lnk_DistrictCode"] = DistrictCode;
        ViewState["lnk_SubDivisionCode"] = SubDivisionCode;
        ViewState["lnk_Block_Code"] = BlockCode;
        ViewState["lnk_Thana_Code"] = ThanaCode;
        ViewState["lnk_Panchayat_Code"] = PanchayatCode;
        if (Convert.ToInt32(Total) > 0)
        {
            ViewState["lnkClick"] = "lnkAnyapanchayat_Click";
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
        if ((ViewState["lnkClick"].ToString() == "lnkTotalDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkDivisionFinalize_Click")
                || (ViewState["lnkClick"].ToString() == "lnkDivisionUnFinalize_Click")
                || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Division_Click")
                || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkJalStrotDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkVaasVivadDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBhuArjanDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaDivision_Click")
                || (ViewState["lnkClick"].ToString() == "lnkAnyaDivision_Click"))
        {
            lnkDivision();
        }
        else if(ViewState["lnkClick"].ToString() == "lnkTotalDistrict_Click"
                     || ViewState["lnkClick"].ToString() == "lnkDistrictFinalize_Click"
                     || ViewState["lnkClick"].ToString() == "lnkDistrictUnFinalize_Click"
                     || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_District_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkJalStrotDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkVaasVivadDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkBhuArjanDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaDistrict_Click")
                     || (ViewState["lnkClick"].ToString() == "lnkAnyaDistrict_Click"))
        {
            lnkDistrcict();
        }
        else if ((ViewState["lnkClick"].ToString() == "lnkTotalSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkSubDivisionFinalize_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkSubDivisionUnFinalize_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_SubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkJalStrotSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkVaasVivadSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkBhuArjanSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaSubDivision_Click")
                      || (ViewState["lnkClick"].ToString() == "lnkAnyaSubDivision_Click"))
        {
            lnkSubDivision();
        }
        else if ((ViewState["lnkClick"].ToString() == "lnkAnyaBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkBlockFinalize_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkBlockUnFinalize_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkBhuArjanBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkVaasVivadBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkJalStrotBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaBlock_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Block_Click")
                    || (ViewState["lnkClick"].ToString() == "lnkTotalBlock_Click"))
        {
            lnkBlock();
        }
        else if ((ViewState["lnkClick"].ToString() == "lnkAnyaThana_Click")
              || (ViewState["lnkClick"].ToString() == "lnkThanaFinalize_Click")
                || (ViewState["lnkClick"].ToString() == "lnkThanaUnFinalize_Click")
                || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzaThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandiThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBhuArjanThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumiThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumiThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkVaasVivadThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwaraThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkJalStrotThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNaliThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemaThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzaThana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_Thana_Click")
                || (ViewState["lnkClick"].ToString() == "lnkTotalThana_Click"))
        {
          lnkThana();

        }

        else if ((ViewState["lnkClick"].ToString() == "lnkAnyapanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkPanchayatsFinalize_Click")
                || (ViewState["lnkClick"].ToString() == "lnkPanchayatsUnFinalize_Click")
                || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiKabzapanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBhuHdBandipanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBhuArjanpanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkBadlenBhumipanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkVyawsaikBhumipanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkLaganNirdharanpanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkVaasVivadpanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkKhetiVivadpanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkPatrikBhumiBatwarapanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkJalStrotpanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkNijiRastaNalipanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkRaitiBhumiSeemapanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkSarkariBhumiKabzapanchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkParchadhariBedakli_panchayat_Click")
                || (ViewState["lnkClick"].ToString() == "lnkTotalpanchayat_Click"))
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