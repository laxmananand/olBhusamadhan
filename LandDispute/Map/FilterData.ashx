<%@ WebHandler Language="C#" Class="FilterData" %>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class FilterData : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string Data = "{}";

        string fromdate = context.Request.QueryString["FromDate"].ToString();
        string todate= context.Request.QueryString["ToDate"].ToString();
        string BhumiVivadType= context.Request.QueryString["BhumiVivadType"].ToString();
        string dis = context.Request.QueryString["District"].ToString();
        string Block = context.Request.QueryString["BlockCode"].ToString();
        string panchayatcode = context.Request.QueryString["panchayatcode"].ToString();
        string VillageWard = context.Request.QueryString["VillageWard"].ToString();
        string Type = context.Request.QueryString["Type"].ToString();
        string savedansheelta= context.Request.QueryString["savedansheelta"].ToString();
        string proc= context.Request.QueryString["Procedure"].ToString();
        string Matter_Status= context.Request.QueryString["Matter_Status"].ToString();
        string con1 = "";
        string con2 = "";
        if(Type=="Panchayat")
        {
            dis = Block;
        }
        if(Type=="ग्राम" ||Type=="वार्ड")
        {
            dis = panchayatcode;
        }
        string parameter = dis;
        if(savedansheelta!="0")
        {
            con1 = @" and((a.Bhumi_savedansheelta is not null and a.Bhumi_savedansheelta="+savedansheelta+")"+
                            "or"+
                            "( a.Bhumi_savedansheelta is  null and   m.Bhumi_savedansheelta="+savedansheelta+"))";
        }
        if(Matter_Status!="0")
        {
            con1 += @" and a.Matter_Status= "+Matter_Status;
        }
        if(Block!="0")
        {
            con2 = @" and a.BlockCode="+Block;
        }
        if(panchayatcode!="0")
        {
            con2 = @" and a.panchayatcode="+panchayatcode;
        }
        if(VillageWard!="0")
        {
            con2 = @" and a.village_WardCode="+VillageWard;
        }
        try
        {

            Data = "{\"title\":\"Bihar\",\"version\":\"1.1.2\",\"type\":\"FeatureCollection\", \"features\": [";

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("exec "+proc+" '"+ BhumiVivadType+"','"+fromdate.ToString()+"','"+todate.ToString()+"','"+parameter+"','"+con1+"','"+con2+"'", con);


            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {

                string extrapPrperty = ",\"BlockCode\":\"" + dr["Code"].ToString() + "\"";
                extrapPrperty = extrapPrperty+ ",\"BlockName\":\"" + dr["Name"].ToString() + "\"";
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