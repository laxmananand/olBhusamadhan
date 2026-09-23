using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class MapvillagepointFilter : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtdatefrom.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
            var year = DateTime.Now.ToString("yyyy");
            txtdatefrom.Text = DateTime.Now.ToString("dd-MM-yyyy");

            txtDateTo.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
            txtDateTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
            BindDistrict();
            bind_BhumiSanvedanshilta();
        }
    }
    void BindDistrict()
    {
        try
        {
            string sql = @"SELECT distinct DISTRICTNAME,DISTRICTCODE from mst_Commissionary_Districts ORDER BY DISTRICTNAME ";
            DataTable dt = clsData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                ddDistrict.DataSource = dt;
                ddDistrict.DataTextField = "DISTRICTNAME";
                ddDistrict.DataValueField = "DISTRICTCODE";
                ddDistrict.DataBind();
                ddDistrict.Items.Insert(0, new ListItem("All", "0"));



                dt.Dispose();
            }
        }
        catch (Exception ex) { }


    }
    protected void bind_BhumiSanvedanshilta()// भूमि विवाद कि सवेदनशीलता
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
                    //ddSensivity.Items.Insert(0, new ListItem("All", "0"));

                }
            }
        }
        catch (Exception ee)
        {


        }
    }
    [System.Web.Services.WebMethod()]
    public static List<MapControle.BlockList> GetBlock(string DistrictId)
    {
        List<MapControle.BlockList> block = new List<MapControle.BlockList>();
        block = MapControle.BlockList.GetBlock(DistrictId);
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
    public static List<MapControle.WardVillageList> GetWardVillage(string PanchayatCode,string AreaType)
    {
        List<MapControle.WardVillageList> panchayat = new List<MapControle.WardVillageList>();
        panchayat = MapControle.WardVillageList.GetPanchayat(PanchayatCode, AreaType);
        return panchayat;
    }
    [System.Web.Services.WebMethod()]
    public static string get(string DistrictId)
    {
        return "hello";
    }
}