<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        string json = "";
        try
        {
            string fromdate = context.Request.QueryString["FromDate"].ToString();
            string todate = context.Request.QueryString["ToDate"].ToString();
            string BhumiVivadType = context.Request.QueryString["BhumiVivadType"].ToString();
            string District_Code = context.Request.QueryString["District_Code"].ToString();
            string Block_Code = context.Request.QueryString["Block"].ToString();



            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.CommandText =  "usp_getward";
            cmd.Parameters.AddWithValue("@BhumiVivadType", BhumiVivadType);
            cmd.Parameters.AddWithValue("@FromDate", fromdate);
            cmd.Parameters.AddWithValue("@ToDate", todate);
            cmd.Parameters.AddWithValue("@District_Code", District_Code);
            cmd.Parameters.AddWithValue("@Block_Code", Block_Code);

            SqlDataAdapter da = new SqlDataAdapter(cmd);




            DataTable dt = new DataTable();
            da.Fill(dt);

            SqlCommand cmdw = new SqlCommand();
            cmdw.Connection = con;
            cmdw.CommandType = CommandType.StoredProcedure;



            cmdw.CommandText =  "usp_getvillage";
            cmdw.Parameters.AddWithValue("@BhumiVivadType", BhumiVivadType);
            cmdw.Parameters.AddWithValue("@FromDate", fromdate);
            cmdw.Parameters.AddWithValue("@ToDate", todate);
            cmdw.Parameters.AddWithValue("@District_Code", District_Code);
            cmdw.Parameters.AddWithValue("@Block_Code", Block_Code);

            SqlDataAdapter daw = new SqlDataAdapter(cmdw);




            DataTable dtw = new DataTable();
            daw.Fill(dtw);


            dt.Merge(dtw);

            //SqlDataReader rd = cmd.ExecuteReader();

            int rowcount = 0, columncount;



            columncount = dt.Columns.Count;


            string[][] str = new string[dt.Rows.Count][];
            int i = 0;
            for(int l=0;l<dt.Rows.Count;l++)
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

        catch (Exception ex) { }
        context.Response.ContentType = "application/json";
        context.Response.Write(json);
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}