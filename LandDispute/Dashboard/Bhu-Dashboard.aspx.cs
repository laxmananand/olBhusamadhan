using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text.pdf;

public partial class Bhu_Dashboard : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login_Default.aspx");


        }
        if (!IsPostBack)
        {
            bindDistrict();
            if (Session["District_Code"] != null)
            {
                if (Session["District_Code"].ToString() != "")
                {
                    ddDistrict.SelectedValue = Session["District_Code"].ToString();
                    ddDistrict.Enabled = false;
                    hdnDistrict.Value= Session["District_Code"].ToString();
                }
            }
            if (Session["Block_Code"] != null)
            {
                if (Session["Block_Code"].ToString() != "")
                {
                    hdnBlockCode.Value = Session["Block_Code"].ToString();
                   
                }
            }
            hdnThanaCode.Value = "0";
            if (Session["Thana_Code"] != null)
            {
                if (Session["Block_Code"].ToString() != "" && Session["Thana_Code"].ToString() != "")
                {
                    hdnThanaCode.Value = Session["Thana_Code"].ToString();
                }
            }
            bind_BhumiSanvedanshilta();
        }
    }   
    private void bindDistrict()
    {
        ddDistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", "0");
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionaryNew", new SqlParameter[] { QueryType, CommissionaryCode });
        if (dt.Rows.Count > 0)
        {
            ddDistrict.DataSource = dt;
            ddDistrict.DataTextField = "DISTRICTNAME";
            ddDistrict.DataValueField = "DISTRICTCODE";
            ddDistrict.DataBind();
            ddDistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddDistrict.DataSource = null;
            ddDistrict.DataTextField = "DISTRICTNAME";
            ddDistrict.DataValueField = "DISTRICTCODE";
            ddDistrict.DataBind();
            ddDistrict.Items.Insert(0, new ListItem("All", "0"));
        }
    }  
    private void bind_BhumiSanvedanshilta()
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_SensitivityType", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddSensivity.DataSource = dt;
                    ddSensivity.DataTextField = "SensitivityType";
                    ddSensivity.DataValueField = "id";
                    ddSensivity.DataBind();
                    ddSensivity.Items.Insert(0, new ListItem("All", "0"));
                }
            }
        }
        catch (Exception ee)
        {


        }
    }

    public void BindData1()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", "0");
        SqlParameter District = new SqlParameter("@District", ddDistrict.SelectedValue.ToString());
        SqlParameter BlockCode = new SqlParameter("@BlockCode", ddBlock.SelectedValue.ToString());
        SqlParameter PanchayatCode = new SqlParameter("@PanchayatCode", ddPanchayat.SelectedValue.ToString());
        SqlParameter thana_code = new SqlParameter("@thana_code", ddlPoliceSation.SelectedValue.ToString());
        SqlParameter savedansheelta = new SqlParameter("@savedansheelta", ddSensivity.SelectedValue.ToString());
        SqlParameter Matter_Status = new SqlParameter("@Matter_Status", ddlaction.SelectedValue.ToString());

        SqlParameter FromDate = new SqlParameter("@FromDate", txtdatefrom.Text.ToString());
        SqlParameter ToDate = new SqlParameter("@ToDate", txtDateTo.Text.ToString());

        DataTable dt = clsData.GetDataTableWithProc("newDashboard", new SqlParameter[] { QueryType, CommissionaryCode, District, BlockCode, PanchayatCode, thana_code, savedansheelta, Matter_Status, FromDate , ToDate });
        if (dt.Rows.Count > 0)
        {
            lbltotalapplication.Text = dt.Rows[0]["Total"].ToString();
            lblFinalize.Text = dt.Rows[0]["Finalize"].ToString();
            lblUnFinalize.Text = dt.Rows[0]["Unfinalize"].ToString();

            lblsaamaany.Text = dt.Rows[0]["saamaany"].ToString();
            lblsavedansheel.Text = dt.Rows[0]["sanvedanasheel"].ToString();
            lblatiSavedansheel.Text = dt.Rows[0]["atisanvedanasheel"].ToString();

            lblnispadan.Text = dt.Rows[0]["Nirast"].ToString();
            lblmapikenirdharit.Text = dt.Rows[0]["Mapi_Nirdharit"].ToString();
            lblprakreeyadheen.Text = dt.Rows[0]["Prakriyadhin"].ToString();
            lblashvikrit.Text = dt.Rows[0]["Ashwikrit"].ToString();
            lblFinaldisposal.Text = dt.Rows[0]["FinalNirast"].ToString();

            BhumiVivadType1.Text = dt.Rows[0]["BhumiVivadType1"].ToString();
            BhumiVivadType2.Text = dt.Rows[0]["BhumiVivadType2"].ToString();
            BhumiVivadType3.Text = dt.Rows[0]["BhumiVivadType3"].ToString();
            BhumiVivadType4.Text = dt.Rows[0]["BhumiVivadType4"].ToString();
            BhumiVivadType5.Text = dt.Rows[0]["BhumiVivadType5"].ToString();
            BhumiVivadType6.Text = dt.Rows[0]["BhumiVivadType6"].ToString();
            BhumiVivadType7.Text = dt.Rows[0]["BhumiVivadType7"].ToString();
            BhumiVivadType8.Text = dt.Rows[0]["BhumiVivadType8"].ToString();
            BhumiVivadType9.Text = dt.Rows[0]["BhumiVivadType9"].ToString();
            BhumiVivadType10.Text = dt.Rows[0]["BhumiVivadType10"].ToString();
            BhumiVivadType11.Text = dt.Rows[0]["BhumiVivadType11"].ToString();
            BhumiVivadType12.Text = dt.Rows[0]["BhumiVivadType12"].ToString();
            BhumiVivadType13.Text = dt.Rows[0]["BhumiVivadType13"].ToString();
            BhumiVivadType15.Text = dt.Rows[0]["BhumiVivadType15"].ToString();
            BhumiVivadType20.Text = dt.Rows[0]["BhumiVivadType20"].ToString();

            SqlParameter QueryType1 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
            SqlParameter CommissionaryCode1 = new SqlParameter("@CommissionaryCode", "0");
            SqlParameter District1 = new SqlParameter("@District", ddDistrict.SelectedValue.ToString());
            SqlParameter BlockCode1 = new SqlParameter("@BlockCode", ddBlock.SelectedValue.ToString());
            SqlParameter PanchayatCode1 = new SqlParameter("@PanchayatCode", ddPanchayat.SelectedValue.ToString());
            SqlParameter thana_code1 = new SqlParameter("@thana_code", ddlPoliceSation.SelectedValue.ToString());
            SqlParameter savedansheelta1 = new SqlParameter("@savedansheelta", ddSensivity.SelectedValue.ToString());
            SqlParameter Matter_Status1 = new SqlParameter("@Matter_Status", ddlaction.SelectedValue.ToString());

            SqlParameter FromDate1 = new SqlParameter("@FromDate", txtdatefrom.Text.ToString());
            SqlParameter ToDate1 = new SqlParameter("@ToDate", txtDateTo.Text.ToString());
            dt = clsData.GetDataTableWithProc("newDashboardLanddesciption", new SqlParameter[] { QueryType1, CommissionaryCode1, District1, BlockCode1, PanchayatCode1, thana_code1, savedansheelta1, Matter_Status1, FromDate1, ToDate1 });
            if (dt.Rows.Count > 0)
            {
                Landdesciption1.Text = dt.Rows[0]["Landdesciption1"].ToString();
                Landdesciption2.Text = dt.Rows[0]["Landdesciption2"].ToString();
                Landdesciption3.Text = dt.Rows[0]["Landdesciption3"].ToString();
                Landdesciption5.Text = dt.Rows[0]["Landdesciption5"].ToString();
                Landdesciption6.Text = dt.Rows[0]["Landdesciption6"].ToString();
                Landdesciption11.Text = dt.Rows[0]["Landdesciption11"].ToString();
                Landdesciption12.Text = dt.Rows[0]["Landdesciption12"].ToString();
            }
            //lbltotalapplication.Text = dt.Rows[0]["Landdesciption13"].ToString();
            SqlParameter QueryType2 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
            SqlParameter CommissionaryCode2 = new SqlParameter("@CommissionaryCode", "0");
            SqlParameter District2 = new SqlParameter("@District", ddDistrict.SelectedValue.ToString());
            SqlParameter BlockCode2 = new SqlParameter("@BlockCode", ddBlock.SelectedValue.ToString());
            SqlParameter PanchayatCode2 = new SqlParameter("@PanchayatCode", ddPanchayat.SelectedValue.ToString());
            SqlParameter thana_code2 = new SqlParameter("@thana_code", ddlPoliceSation.SelectedValue.ToString());
            SqlParameter savedansheelta2 = new SqlParameter("@savedansheelta", ddSensivity.SelectedValue.ToString());
            SqlParameter Matter_Status2 = new SqlParameter("@Matter_Status", ddlaction.SelectedValue.ToString());

            SqlParameter FromDate2 = new SqlParameter("@FromDate", txtdatefrom.Text.ToString());
            SqlParameter ToDate2 = new SqlParameter("@ToDate", txtDateTo.Text.ToString());
            dt = clsData.GetDataTableWithProc("newDashboardVadi_Evidence_Entry", new SqlParameter[] { QueryType2, CommissionaryCode2, District2, BlockCode2, PanchayatCode2, thana_code2, savedansheelta2, Matter_Status2, FromDate2, ToDate2 });

            if (dt.Rows.Count > 0)
            {
                vadiEvidence1.Text = dt.Rows[0]["vadiEvidence1"].ToString();
                vadiEvidence2.Text = dt.Rows[0]["vadiEvidence2"].ToString();
                vadiEvidence3.Text = dt.Rows[0]["vadiEvidence3"].ToString();
                vadiEvidence4.Text = dt.Rows[0]["vadiEvidence4"].ToString();
                vadiEvidence5.Text = dt.Rows[0]["vadiEvidence5"].ToString();
                vadiEvidence6.Text = dt.Rows[0]["vadiEvidence6"].ToString();
                vadiEvidence7.Text = dt.Rows[0]["vadiEvidence7"].ToString();
                vadiEvidence8.Text = dt.Rows[0]["vadiEvidence8"].ToString();
                vadiEvidence9.Text = dt.Rows[0]["vadiEvidence9"].ToString();
            }
            SqlParameter QueryType3 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
            SqlParameter CommissionaryCode3 = new SqlParameter("@CommissionaryCode", "0");
            SqlParameter District3 = new SqlParameter("@District", ddDistrict.SelectedValue.ToString());
            SqlParameter BlockCode3 = new SqlParameter("@BlockCode", ddBlock.SelectedValue.ToString());
            SqlParameter PanchayatCode3 = new SqlParameter("@PanchayatCode", ddPanchayat.SelectedValue.ToString());
            SqlParameter thana_code3 = new SqlParameter("@thana_code", ddlPoliceSation.SelectedValue.ToString());
            SqlParameter savedansheelta3 = new SqlParameter("@savedansheelta", ddSensivity.SelectedValue.ToString());
            SqlParameter Matter_Status3 = new SqlParameter("@Matter_Status", ddlaction.SelectedValue.ToString());

            SqlParameter FromDate3 = new SqlParameter("@FromDate", txtdatefrom.Text.ToString());
            SqlParameter ToDate3 = new SqlParameter("@ToDate", txtDateTo.Text.ToString());
            dt = clsData.GetDataTableWithProc("newDashboardPratiVadi_Evidence_Entry", new SqlParameter[] { QueryType3, CommissionaryCode3, District3, BlockCode3, PanchayatCode3, thana_code3, savedansheelta3, Matter_Status3, FromDate3, ToDate3 });

            if (dt.Rows.Count > 0)
            {
                PrativadiEvidence1.Text = dt.Rows[0]["PrativadiEvidence1"].ToString();
                PrativadiEvidence2.Text = dt.Rows[0]["PrativadiEvidence2"].ToString();
                PrativadiEvidence3.Text = dt.Rows[0]["PrativadiEvidence3"].ToString();
                PrativadiEvidence4.Text = dt.Rows[0]["PrativadiEvidence4"].ToString();
                PrativadiEvidence5.Text = dt.Rows[0]["PrativadiEvidence5"].ToString();
                PrativadiEvidence6.Text = dt.Rows[0]["PrativadiEvidence6"].ToString();
                PrativadiEvidence7.Text = dt.Rows[0]["PrativadiEvidence7"].ToString();
                PrativadiEvidence8.Text = dt.Rows[0]["PrativadiEvidence8"].ToString();
                PrativadiEvidence9.Text = dt.Rows[0]["PrativadiEvidence9"].ToString();
            }

            SqlParameter QueryType4 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
            SqlParameter CommissionaryCode4 = new SqlParameter("@CommissionaryCode", "0");
            SqlParameter District4 = new SqlParameter("@District", ddDistrict.SelectedValue.ToString());
            SqlParameter BlockCode4 = new SqlParameter("@BlockCode", ddBlock.SelectedValue.ToString());
            SqlParameter PanchayatCode4 = new SqlParameter("@PanchayatCode", ddPanchayat.SelectedValue.ToString());
            SqlParameter thana_code4 = new SqlParameter("@thana_code", ddlPoliceSation.SelectedValue.ToString());
            SqlParameter savedansheelta4 = new SqlParameter("@savedansheelta", ddSensivity.SelectedValue.ToString());
            SqlParameter Matter_Status4 = new SqlParameter("@Matter_Status", ddlaction.SelectedValue.ToString());

            SqlParameter FromDate4 = new SqlParameter("@FromDate", txtdatefrom.Text.ToString());
            SqlParameter ToDate4 = new SqlParameter("@ToDate", txtDateTo.Text.ToString());
            dt = clsData.GetDataTableWithProc("newDashboardLandDisputeDetailsEntry", new SqlParameter[] { QueryType4, CommissionaryCode4, District4, BlockCode4, PanchayatCode4, thana_code4, savedansheelta4, Matter_Status4, FromDate4, ToDate4 });
            if (dt.Rows.Count > 0)
            {
                praathamikee.Text = dt.Rows[0]["praathamikee"].ToString();
                apraathamikee.Text = dt.Rows[0]["apraathamikee"].ToString();
                sanaha.Text = dt.Rows[0]["sanaha"].ToString();
            }



            SqlParameter QueryType5 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
            SqlParameter CommissionaryCode5 = new SqlParameter("@CommissionaryCode", "0");
            SqlParameter District5 = new SqlParameter("@District", ddDistrict.SelectedValue.ToString());
            SqlParameter BlockCode5 = new SqlParameter("@BlockCode", ddBlock.SelectedValue.ToString());
            SqlParameter PanchayatCode5 = new SqlParameter("@PanchayatCode", ddPanchayat.SelectedValue.ToString());
            SqlParameter thana_code5 = new SqlParameter("@thana_code", ddlPoliceSation.SelectedValue.ToString());
            SqlParameter savedansheelta5 = new SqlParameter("@savedansheelta", ddSensivity.SelectedValue.ToString());
            SqlParameter Matter_Status5 = new SqlParameter("@Matter_Status", ddlaction.SelectedValue.ToString());

            SqlParameter FromDate5 = new SqlParameter("@FromDate", txtdatefrom.Text.ToString());
            SqlParameter ToDate5 = new SqlParameter("@ToDate", txtDateTo.Text.ToString());
            dt = clsData.GetDataTableWithProc("newDashboardCourtDisputeDetailsEntry", new SqlParameter[] { QueryType5, CommissionaryCode5, District5, BlockCode5, PanchayatCode5, thana_code5, savedansheelta5, Matter_Status5, FromDate5, ToDate5 });
            if (dt.Rows.Count > 0)
            {
                raajasv_nyaayaalay.Text = dt.Rows[0]["raajasv_nyaayaalay"].ToString();
                vyavahaara_nyaayaalay.Text = dt.Rows[0]["vyavahaara_nyaayaalay"].ToString();
                LokShikayat_Nivaran_nyaayaalay.Text = dt.Rows[0]["LokShikayat_Nivaran_nyaayaalay"].ToString();
                uchcha_nyaayaalay.Text = dt.Rows[0]["uchcha_nyaayaalay"].ToString();
                sarvochcha_nyaayaalay.Text = dt.Rows[0]["sarvochcha_nyaayaalay"].ToString();
            }

        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //BindData();
        }
        catch (Exception ex)
        {
        }
    }

    [System.Web.Services.WebMethod()]
    public static List<MapControle.BlockList> GetBlock(string DistrictId,string ThanaCode)
    {
        List<MapControle.BlockList> block = new List<MapControle.BlockList>();
        block = MapControle.BlockList.GetBlock(DistrictId, ThanaCode);
        return block;
    }
    [System.Web.Services.WebMethod()]
    public static List<MapControle.PanchayatList> GetPanchayat(string BlockCode)
    {
        List<MapControle.PanchayatList> panchayat = new List<MapControle.PanchayatList>();
        panchayat = MapControle.PanchayatList.GetPanchayat(BlockCode);
        return panchayat;
    }

    [System.Web.Services.WebMethod()]
    public static List<MapControle.ThanaList> GetThana(string DistrictId, string BlockCode)
    {
        List<MapControle.ThanaList> thana = new List<MapControle.ThanaList>();
        thana = MapControle.ThanaList.GetThana(DistrictId, BlockCode);
        return thana;
    }

    [System.Web.Services.WebMethod()]
    public static List<MapControle.Details> GetDetails(string DistrictId,string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate)
    {
        List<MapControle.Details> details = new List<MapControle.Details>();
        details = MapControle.Details.BindData(DistrictId, BlockCode,PanchayatCode, thana_code, savedansheelta,Matter_Status,FromDate,ToDate);
        return details;
    }

}