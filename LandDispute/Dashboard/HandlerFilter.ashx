<%@ WebHandler Language="C#" Class="HandlerFilter" %>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
public class HandlerFilter : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string json = "";
        try
        {
            string fromdate = context.Request.QueryString["FromDate"].ToString();
            string todate = context.Request.QueryString["ToDate"].ToString();
            string BhumiVivadType = context.Request.QueryString["BhumiVivadType"].ToString();
            string District_Code = context.Request.QueryString["District_Code"].ToString();
            string Block_Code = context.Request.QueryString["Block"].ToString();
            string panchayatcode = context.Request.QueryString["panchayatcode"].ToString();
            string savedansheelta= context.Request.QueryString["savedansheelta"].ToString();
            string VillageWard=context.Request.QueryString["VillageWard"].ToString();
            string Matter_Status= context.Request.QueryString["Matter_Status"].ToString();
           string Thanacode= context.Request.QueryString["thanacode"].ToString();
            string condition = "";
            string condition2 = "1=1 ";
        if(savedansheelta!="0")
        {
        condition = @" and((a.Bhumi_savedansheelta is not null and a.Bhumi_savedansheelta="+savedansheelta+")"+
	                    "or"+
	                    "( a.Bhumi_savedansheelta is  null and   m.Bhumi_savedansheelta="+savedansheelta+"))";
        }
        if(Matter_Status!="0")
        {
            condition += @" and a.Matter_Status= "+Matter_Status;
        }
        if(Thanacode!="0")
        {
            condition += @" and m.Thana_code= "+Thanacode;
        }
        //WardNo
        if(VillageWard!="0")
        {
            condition += @" and (m.WardNo= "+VillageWard +" or m.Village="+VillageWard+")";
        }
        if(District_Code!="0")
        {
            condition2 += @" and District_Code= "+District_Code;
        }
        
        if(Block_Code!="0")
        {
            condition2 += @" and Block_Code= "+Block_Code;
        }
        if(panchayatcode!="0")
        {
            condition2+= @" and Panchayat_Code= "+panchayatcode;
        }
        
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.CommandText =  "usp_getWardFilter";
            cmd.Parameters.AddWithValue("@BhumiVivadType", "0");
            cmd.Parameters.AddWithValue("@FromDate", fromdate);
            cmd.Parameters.AddWithValue("@ToDate", todate);
            cmd.Parameters.AddWithValue("@District_Code", District_Code);
            cmd.Parameters.AddWithValue("@Block_Code", Block_Code);
            cmd.Parameters.AddWithValue("@Panchayat_Code", panchayatcode);
            cmd.Parameters.AddWithValue("@condition", condition);
            cmd.Parameters.AddWithValue("@condition2", condition2);
            SqlDataAdapter da = new SqlDataAdapter(cmd);




            DataTable dt = new DataTable();
            da.Fill(dt);

            SqlCommand cmdw = new SqlCommand();
            cmdw.Connection = con;
            cmdw.CommandType = CommandType.StoredProcedure;



            cmdw.CommandText =  "usp_getvillageFilter";
            cmdw.Parameters.AddWithValue("@BhumiVivadType", "0");
            cmdw.Parameters.AddWithValue("@FromDate", fromdate);
            cmdw.Parameters.AddWithValue("@ToDate", todate);
            cmdw.Parameters.AddWithValue("@District_Code", District_Code);
            cmdw.Parameters.AddWithValue("@Block_Code", Block_Code);
            cmdw.Parameters.AddWithValue("@Panchayat_Code", panchayatcode);
            cmdw.Parameters.AddWithValue("@condition", condition);
            cmdw.Parameters.AddWithValue("@condition2", condition2);
            SqlDataAdapter daw = new SqlDataAdapter(cmdw);




            DataTable dtw = new DataTable();
            daw.Fill(dtw);


            dt.Merge(dtw);

            //SqlDataReader rd = cmd.ExecuteReader();

            int rowcount = 0, columncount;



            columncount = dt.Columns.Count;


            string[][] str = new string[dt.Rows.Count][];
            int i = 0;
            //for(int l=0;l<dt.Rows.Count;l++)
            for(int l=0; l<dt.Rows.Count;l++)
            {
                str[i] = new string[columncount];
                for (int j = 0; j < columncount; j++)
                {
                    str[i][j] = dt.Rows[i][j].ToString();
                }
                i++;
            }








            JavaScriptSerializer js = new JavaScriptSerializer();
            json = js.Serialize((object)str);
            //rd.Close();
            //con.Close();
        }

        catch (Exception ex) {
            }
        context.Response.ContentType = "application/json";
        context.Response.Write(json);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}