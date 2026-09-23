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
                    hdnDistrict.Value = Session["District_Code"].ToString();
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
    private void bind_BhumiSanvedanshilta()
    {
        try
        {
            //DataTable dt = clsData.GetDataTableWithProc("SP_SensitivityType", new SqlParameter[] { });
            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        ddSensivity.DataSource = dt;
            //        ddSensivity.DataTextField = "SensitivityType";
                   
            //        ddSensivity.DataValueField = "id";
            //        ddSensivity.DataBind();
            //        ddSensivity.Items.Insert(0, new ListItem("All", "0"));
            //    }
            //}
        }
        catch (Exception ee)
        {


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
    public static List<Dashboardgraph> GetDetails(string DistrictId,string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate,string step)
    {
        
        List<Dashboardgraph> details = new List<Dashboardgraph>();
        details = Dashboardgraph.BindData(DistrictId, BlockCode,PanchayatCode, thana_code, savedansheelta,Matter_Status,FromDate,ToDate, step);
        return details;
    }
    [System.Web.Services.WebMethod()]
    public static List<Dashboardgraph> GetForBarDetails(string DistrictId, string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate, string step,string proc,string condi)
    {

        List<Dashboardgraph> details = new List<Dashboardgraph>();
        details = Dashboardgraph.BindBarData(DistrictId, BlockCode, PanchayatCode, thana_code, savedansheelta, Matter_Status, FromDate, ToDate, step, proc, condi);
        return details;
    }

    [System.Web.Services.WebMethod()]
    public static List<Dashboardgraph> GetForLineGraphDetails(string DistrictId, string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate, string step)
    {

        List<Dashboardgraph> details = new List<Dashboardgraph>();
        details = Dashboardgraph.Bindline(DistrictId, BlockCode, PanchayatCode, thana_code, savedansheelta, Matter_Status, FromDate, ToDate, step);
        return details;
    }
    [System.Web.Services.WebMethod()]
    public static List<Dashboardgraph> GetTotalDetails(string DistrictId, string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate, string step)
    {

        List<Dashboardgraph> details = new List<Dashboardgraph>();
        details = Dashboardgraph.BindTotalData(DistrictId, BlockCode, PanchayatCode, thana_code, savedansheelta, Matter_Status, FromDate, ToDate, step);
        return details;
    }
}