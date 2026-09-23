<%@ WebHandler Language="C#" Class="Block" %>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Block : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string Data = "{}";

        string fromdate = context.Request.QueryString["FromDate"].ToString();
        string todate= context.Request.QueryString["ToDate"].ToString();
        string BhumiVivadType= context.Request.QueryString["BhumiVivadType"].ToString();
        string dis = context.Request.QueryString["District"].ToString();
        try
        {

            Data = "{\"title\":\"Bihar\",\"version\":\"1.1.2\",\"type\":\"FeatureCollection\", \"features\": [";

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("exec usp_getBlockMap '"+ BhumiVivadType+"','"+fromdate.ToString()+"','"+todate.ToString()+"','"+dis+"'", con);

            
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                 string extrapPrperty = ",\"BlockCode\":\"" + dr["BlockCode"].ToString() + "\"";
                extrapPrperty = extrapPrperty+ ",\"BlockName\":\"" + dr["BlockName"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"Total\":\"" + dr["Total"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"Samanya\":\"" + dr["Samanya"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"Sumvadansheel\":\"" + dr["Sumvadansheel"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"AtiSumvadansheel\":\"" + dr["AtiSumvadansheel"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"Nirast\":\"" + dr["Nirast"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"Ashwikrit\":\"" + dr["Ashwikrit"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"Mapi_Nirdharit\":\"" + dr["Mapi_Nirdharit"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"Prakriyadhin\":\"" + dr["Prakriyadhin"].ToString() + "\"";
                extrapPrperty = extrapPrperty + ",\"FinalNirast\":\"" + dr["FinalNirast"].ToString() + "\"";
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

    public bool IsReusable {
        get {
            return false;
        }
    }

}