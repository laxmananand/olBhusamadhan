using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Runtime.Remoting.Contexts;

/// <summary>
/// Summary description for Dashboardgraph
/// </summary>
public class Dashboardgraph
{
    public Dashboardgraph()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string filedname { get; set; }
    public string filedvalue { get; set; }

    public string filedsamanay { get; set; }
    public string filedsavedansheel { get; set; }
    public string filedatisavedansheel { get; set; }
    public string yeard { get; set; }
    public string monthd { get; set; }
    public static List<Dashboardgraph> BindData(string District, string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate,string datatype)
    {
        clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
        List<Dashboardgraph> colResult = new List<Dashboardgraph>();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", "0");
        SqlParameter _District = new SqlParameter("@District", District);
        SqlParameter _BlockCode = new SqlParameter("@BlockCode", BlockCode);
        SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", PanchayatCode);
        SqlParameter _thana_code = new SqlParameter("@thana_code", thana_code);
        SqlParameter _savedansheelta = new SqlParameter("@savedansheelta", savedansheelta);
        SqlParameter _Matter_Status = new SqlParameter("@Matter_Status", Matter_Status);

        SqlParameter _FromDate = new SqlParameter("@FromDate", FromDate);
        SqlParameter _ToDate = new SqlParameter("@ToDate", ToDate);

        string proc = "newDashboardGraph";
        if (datatype == "0")
        {
            proc = "newDashboardTotalApplicationGraph";
        }
        else if (datatype == "1")
        {
            proc = "newDashboardGraph";
        }
        else if ( datatype == "2" )
        {
            proc = "newDashboardsavedansheeltaGraph";
        }
        else if (datatype == "3")
        {
            proc = "newDashboardActionGraph";
        }
        else if (datatype == "4")
        {
            proc = "newDashboardLandDisputeGraph";
        }
        else if (datatype == "5")
        {
            proc = "newDashboardLanddesciptionGraph";
        }
        else if (datatype == "6")
        {
            proc = "newDashboardVadi_Evidence_EntryGraph";
        }
        else if (datatype == "7")
        {
            proc = "newDashboardPratiVadi_Evidence_EntryGraph";
        }
        else if (datatype == "8")
        {
            proc = "newDashboardLandDisputeDetailsEntryGraph";
        }
        else if (datatype == "9")
        {
            proc = "newDashboardCourtDisputeDetailsEntryGraph";
        }       
        DataTable dt = clsData.GetDataTableWithProc(proc, new SqlParameter[] { QueryType, CommissionaryCode, _District, _BlockCode, _PanchayatCode, _thana_code, _savedansheelta, _Matter_Status, _FromDate, _ToDate });          
            if(dt.Rows.Count>0)
            {
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    Dashboardgraph d = new Dashboardgraph();
                    d.filedname = dt.Rows[i]["datafield"].ToString();
                    d.filedvalue = dt.Rows[i]["datavalue"].ToString();
                    colResult.Add(d);
                }
            }
        

        return colResult;
    }

    public static List<Dashboardgraph> BindBarData(string District, string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate, string datatype, string proc,string condi)
    {
        DataTable dt = new DataTable();
        List<Dashboardgraph> colResult = new List<Dashboardgraph>();
       
        
        //string savedansheelta1 = savedansheelta;

        if(District=="0")
        {
            string fromdate = FromDate;
            string todate = ToDate;
            string BhumiVivadType = "0";
            string DistCode = District;
            string condition = "";
            if (savedansheelta != "0")
            {
                condition = @" and((a.Bhumi_savedansheelta is not null and a.Bhumi_savedansheelta=" + savedansheelta + ")" +
                                "or" +
                                "( a.Bhumi_savedansheelta is  null and   m.Bhumi_savedansheelta=" + savedansheelta + "))";

                if(savedansheelta=="4")
                {
                    condition = @" and(((a.Bhumi_savedansheelta is not null and a.Bhumi_savedansheelta=2)" +
                                "or" +
                                "( a.Bhumi_savedansheelta is  null and   m.Bhumi_savedansheelta=2))"+
                                " or ((a.Bhumi_savedansheelta is not null and a.Bhumi_savedansheelta=3)"+
                                 "or" +
                                "( a.Bhumi_savedansheelta is  null and   m.Bhumi_savedansheelta=3)))" ;
                }
            }
            if (Matter_Status != "0")
            {
                condition = condition+ @" and a.Matter_Status="+ Matter_Status;
            }
            condition += condi;
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString);

                SqlDataAdapter da = new SqlDataAdapter("exec " + proc + " '" + BhumiVivadType + "','" + fromdate.ToString() + "','" + todate.ToString() + "','" + DistCode.ToString() + "','" + condition.ToString() + "'", con);

                da.Fill(dt);
            }
            catch (Exception ex)
            {

            }

        }
        else
        {
            string fromdate = FromDate;
            string todate = ToDate;
            string BhumiVivadType = "0";
            string dis = District;
            string Block = BlockCode;
            string panchayatcode = PanchayatCode;
            string VillageWard ="0";
           // string Type = context.Request.QueryString["Type"].ToString();
           // string savedansheelta = savedansheelta;
           // string proc = proc;
            //string Matter_Status = context.Request.QueryString["Matter_Status"].ToString();
            string Thanacode = thana_code;

            string con1 = "";
            string con2 = "";
            //if (Type == "Panchayat")
            //{
            //    dis = Block;
            //}
            //if (Type == "ग्राम" || Type == "वार्ड")
            //{
            //    dis = panchayatcode;
            //}
            string parameter = dis;
            if (savedansheelta != "0")
            {
                con1 = @" and((a.Bhumi_savedansheelta is not null and a.Bhumi_savedansheelta=" + savedansheelta + ")" +
                                "or" +
                                "( a.Bhumi_savedansheelta is  null and   m.Bhumi_savedansheelta=" + savedansheelta + "))";

                if (savedansheelta == "4")
                {
                    con1 = @" and(((a.Bhumi_savedansheelta is not null and a.Bhumi_savedansheelta=2)" +
                                "or" +
                                "( a.Bhumi_savedansheelta is  null and   m.Bhumi_savedansheelta=2))" +
                                " or ((a.Bhumi_savedansheelta is not null and a.Bhumi_savedansheelta=3)" +
                                 "or" +
                                "( a.Bhumi_savedansheelta is  null and   m.Bhumi_savedansheelta=3)))";
                }
            }
            if (Matter_Status != "0")
            {
                con1 += @" and a.Matter_Status= " + Matter_Status;
            }
            if (Thanacode != "0")
            {
                con1 += @" and m.Thana_code= " + Thanacode;
            }
            if (Block != "0")
            {
                con2 = @" and a.BlockCode=" + Block;
                parameter = Block;
            }
            if (panchayatcode != "0")
            {
                con2 = @" and a.panchayatcode=" + panchayatcode;
                //parameter = panchayatcode;
            }
            if (VillageWard != "0")
            {
                con2 = @" and a.village_WardCode=" + VillageWard;
            }
            con1 += condi;
            try
            {
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString);
                SqlDataAdapter da = new SqlDataAdapter("exec " + proc + " '0','" + fromdate.ToString() + "','" + todate.ToString() + "','" + parameter + "','" + con1 + "','" + con2 + "'", con);               
                da.Fill(dt);
            }
            catch (Exception ex) { }
        }

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dashboardgraph d = new Dashboardgraph();
                d.filedname = dt.Rows[i]["datafield"].ToString();
                d.filedvalue = dt.Rows[i]["datavalue"].ToString();
                colResult.Add(d);
            }
        }


        return colResult;
    }

    public static List<Dashboardgraph> Bindline(string District, string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate, string datatype)
    {
        clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
        List<Dashboardgraph> colResult = new List<Dashboardgraph>();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", "0");
        SqlParameter _District = new SqlParameter("@District", District);
        SqlParameter _BlockCode = new SqlParameter("@BlockCode", BlockCode);
        SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", PanchayatCode);
        SqlParameter _thana_code = new SqlParameter("@thana_code", thana_code);
        SqlParameter _savedansheelta = new SqlParameter("@savedansheelta", savedansheelta);
        SqlParameter _Matter_Status = new SqlParameter("@Matter_Status", Matter_Status);

        SqlParameter _FromDate = new SqlParameter("@FromDate", FromDate);
        SqlParameter _ToDate = new SqlParameter("@ToDate", ToDate);

        string proc = "newDashboardsavedansheeltalineGraph";
        
        DataTable dt = clsData.GetDataTableWithProc(proc, new SqlParameter[] { QueryType, CommissionaryCode, _District, _BlockCode, _PanchayatCode, _thana_code, _savedansheelta, _Matter_Status, _FromDate, _ToDate });


        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dashboardgraph d = new Dashboardgraph();
                d.filedname = dt.Rows[i]["date"].ToString();
                d.filedsamanay = dt.Rows[i]["samanay"].ToString();
                d.filedsavedansheel = dt.Rows[i]["savedansheel"].ToString();
                d.filedatisavedansheel = dt.Rows[i]["atisavedansheel"].ToString();
                d.yeard= dt.Rows[i]["yeard"].ToString();
                d.monthd = dt.Rows[i]["monthd"].ToString();
                colResult.Add(d);
            }
        }


        return colResult;
    }

    public static List<Dashboardgraph> BindTotalData(string District, string BlockCode, string PanchayatCode, string thana_code, string savedansheelta, string Matter_Status, string FromDate, string ToDate, string datatype)
    {
        clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
        List<Dashboardgraph> colResult = new List<Dashboardgraph>();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", "0");
        SqlParameter _District = new SqlParameter("@District", District);
        SqlParameter _BlockCode = new SqlParameter("@BlockCode", BlockCode);
        SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", PanchayatCode);
        SqlParameter _thana_code = new SqlParameter("@thana_code", thana_code);
        SqlParameter _savedansheelta = new SqlParameter("@savedansheelta", savedansheelta);
        SqlParameter _Matter_Status = new SqlParameter("@Matter_Status", Matter_Status);

        SqlParameter _FromDate = new SqlParameter("@FromDate", FromDate);
        SqlParameter _ToDate = new SqlParameter("@ToDate", ToDate);

        string proc = "newDashboardTotalApplicationGraph";
        if (datatype == "1")
        {
            proc = "newDashboardTotalApplicationGraph";
        }
        else if (datatype == "2")
        {
            proc = "newDashboardTotalApplicationSensivityGraph";
        }
        


        DataTable dt = clsData.GetDataTableWithProc(proc, new SqlParameter[] { QueryType, CommissionaryCode, _District, _BlockCode, _PanchayatCode, _thana_code, _savedansheelta, _Matter_Status, _FromDate, _ToDate });


        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dashboardgraph d = new Dashboardgraph();
                d.filedname = dt.Rows[i]["datafield"].ToString();
                d.filedvalue = dt.Rows[i]["datavalue"].ToString();
                colResult.Add(d);
            }
        }


        return colResult;
    }
}