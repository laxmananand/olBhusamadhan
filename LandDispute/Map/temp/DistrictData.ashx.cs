using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FloodMap
{
    /// <summary>
    /// Summary description for DistrictData
    /// </summary>
    public class DistrictData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Data = "";
            string fromdate = context.Request.QueryString["FromDate"].ToString();
            string todate= context.Request.QueryString["ToDate"].ToString();
            try
            {

                
                Data = "{\"title\":\"Bihar\",\"version\":\"1.1.2\",\"type\":\"FeatureCollection\", \"features\": [";

                SqlConnection con = new SqlConnection("Data Source=10.133.20.165;Initial Catalog=eoc;User Id=sa;Password=sa123;");
                fromdate = context.Request.QueryString["fromdate"].ToString();
                todate = context.Request.QueryString["todate"].ToString();
                SqlDataAdapter da = new SqlDataAdapter("exec sp_map_DistrictReport '"+ fromdate.ToString()+"','"+ todate.ToString()+"'", con);
                

                 // SqlDataAdapter da = new SqlDataAdapter("exec sp_map_DistrictReport '+"" '2022-01-01', '2022-12-31'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    string extrapPrperty = ",\"TotalBlock\":\"" + dr["TotalBlock"].ToString() + "\"";
                    extrapPrperty = extrapPrperty+ ",\"TotalEffectedBlock\":\"" + dr["TotalEffectedBlock"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"TotalEffectedPanchayat\":\"" + dr["TotalEffectedPanchayat"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"Per\":\"" + dr["Per"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"color\":\"" + dr["color"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"TotalKitchens\":\"" + dr["TotalKitchens"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"TotalReliefCentres\":\"" + dr["TotalReliefCentres"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"TotalDeaths\":\"" + dr["TotalDeaths"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"MigratedPopulation\":\"" + dr["MigratedPopulation"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"KitchenColor\":\"" + dr["KitchenColor"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"ReliefCentreColor\":\"" + dr["ReliefCentreColor"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"totalDamagedHousesValue\":\"" + dr["totalDamagedHousesValue"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"totalGovtBoatToday\":\"" + dr["totalGovtBoatToday"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"totalMotorBoatToday\":\"" + dr["totalMotorBoatToday"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"totalPolytheneSheetDist\":\"" + dr["totalPolytheneSheetDist"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"totalPvtBoatToday\":\"" + dr["totalPvtBoatToday"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"totalFloodEntry\":\"" + dr["totalFloodEntry"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"EntryPer\":\"" + dr["EntryPer"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + ",\"Entrycolor\":\"" + dr["Entrycolor"].ToString() + "\"";
                    extrapPrperty = extrapPrperty + "}";
                    string prp = dr["Properties"].ToString().Replace("}", extrapPrperty);
                    Data = Data + "{\"type\":\"Feature\",\"geometry\":" + dr["Geometry"].ToString() + ",\"properties\": " + prp + "},";

                }
                Data = Data.Remove(Data.Length - 1, 1);
                Data = Data + "]}";

            }
            catch (Exception ex) { }   
            context.Response.ContentType = "application/json";
            context.Response.Write(Data);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}