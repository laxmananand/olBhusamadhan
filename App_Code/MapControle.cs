using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text.pdf;

/// <summary>
/// Summary description for MapControle
/// </summary>
public class MapControle
{
    public MapControle()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public class BlockList
    {
        public string BlockCode { set; get; }
        public string BlockName { set; get; }
        public static List<BlockList> GetBlock(string DistrictCode ,string thana_code)
        {
            clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
            List<BlockList> colResult = new List<BlockList>();

            System.Data.DataTable dt = new DataTable();
            string sql = @"select BlockCode,BlockName from Blocks where DistCode='" + DistrictCode + "'  order by BlockName";
            if (thana_code != "0")
            {
                sql = @"select BlockCode,BlockName from Blocks  where BlockCode in (select code from MstThanaMapping where Thana_Code='"+ thana_code + "') ";
            }
               
            dt = clsData.GetDataTable(sql);


            


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BlockList tmpBlock = new BlockList();
                tmpBlock.BlockCode = dt.Rows[i]["BlockCode"].ToString();
                tmpBlock.BlockName = dt.Rows[i]["BlockName"].ToString();
                colResult.Add(tmpBlock);
            }
            return colResult;
        }
    }
    public class ThanaList
    {
        public string PS_Code { set; get; }
        public string Police_Station { set; get; }
      
        public static List<ThanaList> GetThana(string District, string BlockCode)
        {
            clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
            List<ThanaList> colResult = new List<ThanaList>();

            System.Data.DataTable dt = new DataTable();
            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("9"));
            SqlParameter _District = new SqlParameter("@District", Convert.ToInt32(District));
            
            SqlParameter block = new SqlParameter("@BlockCode", Convert.ToInt32(BlockCode));
            dt = clsData.GetDataTableWithProc("SP_commissionaryNew", new SqlParameter[] { QueryType, _District, block });
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ThanaList tmpthana = new ThanaList();
                tmpthana.PS_Code = dt.Rows[i]["PS_Code"].ToString();
                tmpthana.Police_Station = dt.Rows[i]["Police_Station"].ToString();
               
                colResult.Add(tmpthana);
            }
            return colResult;
        }
    }

    public class Details
    {
        public string Total         { set; get; }
        public string Finalize      { set; get; }
        public string Unfinalize    { set; get; }

        public string saamaany { set; get; }
        public string sanvedanasheel { set; get; }
        public string atisanvedanasheel { set; get; }

        public string Nirast   { set; get; }
        public string Mapi_Nirdharit   { set; get; }
        public string Prakriyadhin     { set; get; }
        public string Ashwikrit        { set; get; }
        public string FinalNirast      { set; get; }

        public string BhumiVivadType1  { set; get; }
        public string BhumiVivadType2  { set; get; }
        public string BhumiVivadType3  { set; get; }
        public string BhumiVivadType4  { set; get; }
        public string BhumiVivadType5  { set; get; }
        public string BhumiVivadType6  { set; get; }
        public string BhumiVivadType7  { set; get; }
        public string BhumiVivadType8  { set; get; }
        public string BhumiVivadType9  { set; get; }
        public string BhumiVivadType10 { set; get; }
        public string BhumiVivadType11 { set; get; }
        public string BhumiVivadType12 { set; get; }
        public string BhumiVivadType13 { set; get; }
        public string BhumiVivadType15 { set; get; }
        public string BhumiVivadType20 { set; get; }

        public string Landdesciption1  { set; get; }
        public string Landdesciption2  { set; get; }
        public string Landdesciption3  { set; get; }
        public string Landdesciption5  { set; get; }
        public string Landdesciption6  { set; get; }
        public string Landdesciption11 { set; get; }
        public string Landdesciption12 { set; get; }
        public string Landdesciption13 { set; get; }

        public string vadiEvidence1    { set; get; }
        public string vadiEvidence2    { set; get; }
        public string vadiEvidence3    { set; get; }
        public string vadiEvidence4    { set; get; }
        public string vadiEvidence5    { set; get; }
        public string vadiEvidence6    { set; get; }
        public string vadiEvidence7    { set; get; }
        public string vadiEvidence8    { set; get; }
        public string vadiEvidence9 { set; get; }

        public string PrativadiEvidence1{ set; get; }
        public string PrativadiEvidence2        { set; get; }
        public string PrativadiEvidence3        { set; get; }
        public string PrativadiEvidence4        { set; get; }
        public string PrativadiEvidence5        { set; get; }
        public string PrativadiEvidence6        { set; get; }
        public string PrativadiEvidence7        { set; get; }
        public string PrativadiEvidence8        { set; get; }
        public string PrativadiEvidence9        { set; get; }

        public string praathamikee              { set; get; }
        public string apraathamikee             { set; get; }
        public string sanaha                    { set; get; }

        public string raajasv_nyaayaalay        { set; get; }
        public string vyavahaara_nyaayaalay { set; get; }
        public string LokShikayat_Nivaran_nyaayaalay { set; get; }
        public string uchcha_nyaayaalay { set; get; }
        public string sarvochcha_nyaayaalay { set; get; }
        public static List<Details> BindData(string District,string BlockCode,string PanchayatCode,string thana_code, string savedansheelta,string Matter_Status,string FromDate,string ToDate)
        {
            clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
            List<Details> colResult = new List<Details>();
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

            DataTable dt = clsData.GetDataTableWithProc("newDashboard", new SqlParameter[] { QueryType, CommissionaryCode, _District, _BlockCode, _PanchayatCode, _thana_code, _savedansheelta, _Matter_Status, _FromDate, _ToDate });
            Details d = new Details();
            if (dt.Rows.Count > 0)
            {
                
                d.Total = dt.Rows[0]["Total"].ToString();
                d.Finalize = dt.Rows[0]["Finalize"].ToString();
                d.Unfinalize = dt.Rows[0]["Unfinalize"].ToString();

                d.saamaany = dt.Rows[0]["saamaany"].ToString();
                d.sanvedanasheel = dt.Rows[0]["sanvedanasheel"].ToString();
                d.atisanvedanasheel = dt.Rows[0]["atisanvedanasheel"].ToString();

                d.Nirast = dt.Rows[0]["Nirast"].ToString();
                d.Mapi_Nirdharit = dt.Rows[0]["Mapi_Nirdharit"].ToString();
                d.Prakriyadhin = dt.Rows[0]["Prakriyadhin"].ToString();
                d.Ashwikrit = dt.Rows[0]["Ashwikrit"].ToString();
                d.FinalNirast = dt.Rows[0]["FinalNirast"].ToString();

                d.BhumiVivadType1 = dt.Rows[0]["BhumiVivadType1"].ToString();
                d.BhumiVivadType2 = dt.Rows[0]["BhumiVivadType2"].ToString();
                d.BhumiVivadType3 = dt.Rows[0]["BhumiVivadType3"].ToString();
                d.BhumiVivadType4 = dt.Rows[0]["BhumiVivadType4"].ToString();
                d.BhumiVivadType5 = dt.Rows[0]["BhumiVivadType5"].ToString();
                d.BhumiVivadType6 = dt.Rows[0]["BhumiVivadType6"].ToString();
                d.BhumiVivadType7 = dt.Rows[0]["BhumiVivadType7"].ToString();
                d.BhumiVivadType8 = dt.Rows[0]["BhumiVivadType8"].ToString();
                d.BhumiVivadType9 = dt.Rows[0]["BhumiVivadType9"].ToString();
                d.BhumiVivadType10 = dt.Rows[0]["BhumiVivadType10"].ToString();
                d.BhumiVivadType11 = dt.Rows[0]["BhumiVivadType11"].ToString();
                d.BhumiVivadType12 = dt.Rows[0]["BhumiVivadType12"].ToString();
                d.BhumiVivadType13 = dt.Rows[0]["BhumiVivadType13"].ToString();
                d.BhumiVivadType15 = dt.Rows[0]["BhumiVivadType15"].ToString();
                d.BhumiVivadType20 = dt.Rows[0]["BhumiVivadType20"].ToString();

                SqlParameter QueryType1 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
                SqlParameter CommissionaryCode1 = new SqlParameter("@CommissionaryCode", "0");
                SqlParameter District1 = new SqlParameter("@District", District);
                SqlParameter BlockCode1 = new SqlParameter("@BlockCode", BlockCode);
                SqlParameter PanchayatCode1 = new SqlParameter("@PanchayatCode", PanchayatCode);
                SqlParameter thana_code1 = new SqlParameter("@thana_code", thana_code);
                SqlParameter savedansheelta1 = new SqlParameter("@savedansheelta", savedansheelta);
                SqlParameter Matter_Status1 = new SqlParameter("@Matter_Status", Matter_Status);

                SqlParameter FromDate1 = new SqlParameter("@FromDate", FromDate);
                SqlParameter ToDate1 = new SqlParameter("@ToDate",ToDate);
                dt = clsData.GetDataTableWithProc("newDashboardLanddesciption", new SqlParameter[] { QueryType1, CommissionaryCode1, District1, BlockCode1, PanchayatCode1, thana_code1, savedansheelta1, Matter_Status1, FromDate1, ToDate1 });
                if (dt.Rows.Count > 0)
                {
                    d.Landdesciption1= dt.Rows[0]["Landdesciption1"].ToString();
                    d.Landdesciption2= dt.Rows[0]["Landdesciption2"].ToString();
                    d.Landdesciption3= dt.Rows[0]["Landdesciption3"].ToString();
                    d.Landdesciption5= dt.Rows[0]["Landdesciption5"].ToString();
                    d.Landdesciption6= dt.Rows[0]["Landdesciption6"].ToString();
                    d.Landdesciption11 = dt.Rows[0]["Landdesciption11"].ToString();
                    d.Landdesciption12 = dt.Rows[0]["Landdesciption12"].ToString();
                }
                //lbltotalapplication.Text = dt.Rows[0]["Landdesciption13"].ToString();
                SqlParameter QueryType2 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
                SqlParameter CommissionaryCode2 = new SqlParameter("@CommissionaryCode", "0");
                SqlParameter District2 = new SqlParameter("@District", District);
                SqlParameter BlockCode2 = new SqlParameter("@BlockCode", BlockCode);
                SqlParameter PanchayatCode2 = new SqlParameter("@PanchayatCode", PanchayatCode);
                SqlParameter thana_code2 = new SqlParameter("@thana_code", thana_code);
                SqlParameter savedansheelta2 = new SqlParameter("@savedansheelta", savedansheelta);
                SqlParameter Matter_Status2 = new SqlParameter("@Matter_Status", Matter_Status);

                SqlParameter FromDate2 = new SqlParameter("@FromDate", FromDate);
                SqlParameter ToDate2 = new SqlParameter("@ToDate", ToDate);
                dt = clsData.GetDataTableWithProc("newDashboardVadi_Evidence_Entry", new SqlParameter[] { QueryType2, CommissionaryCode2, District2, BlockCode2, PanchayatCode2, thana_code2, savedansheelta2, Matter_Status2, FromDate2, ToDate2 });

                if (dt.Rows.Count > 0)
                {
                    d.vadiEvidence1 = dt.Rows[0]["vadiEvidence1"].ToString();
                    d.vadiEvidence2 = dt.Rows[0]["vadiEvidence2"].ToString();
                    d.vadiEvidence3 = dt.Rows[0]["vadiEvidence3"].ToString();
                    d.vadiEvidence4 = dt.Rows[0]["vadiEvidence4"].ToString();
                    d.vadiEvidence5 = dt.Rows[0]["vadiEvidence5"].ToString();
                    d.vadiEvidence6 = dt.Rows[0]["vadiEvidence6"].ToString();
                    d.vadiEvidence7 = dt.Rows[0]["vadiEvidence7"].ToString();
                    d.vadiEvidence8 = dt.Rows[0]["vadiEvidence8"].ToString();
                    d.vadiEvidence9 = dt.Rows[0]["vadiEvidence9"].ToString();
                }
                SqlParameter QueryType3 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
                SqlParameter CommissionaryCode3 = new SqlParameter("@CommissionaryCode", "0");
                SqlParameter District3 = new SqlParameter("@District", District);
                SqlParameter BlockCode3 = new SqlParameter("@BlockCode", BlockCode);
                SqlParameter PanchayatCode3 = new SqlParameter("@PanchayatCode", PanchayatCode);
                SqlParameter thana_code3 = new SqlParameter("@thana_code", thana_code);
                SqlParameter savedansheelta3 = new SqlParameter("@savedansheelta", savedansheelta);
                SqlParameter Matter_Status3 = new SqlParameter("@Matter_Status", Matter_Status);

                SqlParameter FromDate3 = new SqlParameter("@FromDate", FromDate);
                SqlParameter ToDate3 = new SqlParameter("@ToDate", ToDate);
                dt = clsData.GetDataTableWithProc("newDashboardPratiVadi_Evidence_Entry", new SqlParameter[] { QueryType3, CommissionaryCode3, District3, BlockCode3, PanchayatCode3, thana_code3, savedansheelta3, Matter_Status3, FromDate3, ToDate3 });

                if (dt.Rows.Count > 0)
                {
                    d.PrativadiEvidence1 = dt.Rows[0]["PrativadiEvidence1"].ToString();
                    d.PrativadiEvidence2 = dt.Rows[0]["PrativadiEvidence2"].ToString();
                    d.PrativadiEvidence3 = dt.Rows[0]["PrativadiEvidence3"].ToString();
                    d.PrativadiEvidence4 = dt.Rows[0]["PrativadiEvidence4"].ToString();
                    d.PrativadiEvidence5 = dt.Rows[0]["PrativadiEvidence5"].ToString();
                    d.PrativadiEvidence6 = dt.Rows[0]["PrativadiEvidence6"].ToString();
                    d.PrativadiEvidence7 = dt.Rows[0]["PrativadiEvidence7"].ToString();
                    d.PrativadiEvidence8 = dt.Rows[0]["PrativadiEvidence8"].ToString();
                    d.PrativadiEvidence9 = dt.Rows[0]["PrativadiEvidence9"].ToString();
                }

                SqlParameter QueryType4 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
                SqlParameter CommissionaryCode4 = new SqlParameter("@CommissionaryCode", "0");
                SqlParameter District4 = new SqlParameter("@District", District);
                SqlParameter BlockCode4 = new SqlParameter("@BlockCode", BlockCode);
                SqlParameter PanchayatCode4 = new SqlParameter("@PanchayatCode", PanchayatCode);
                SqlParameter thana_code4 = new SqlParameter("@thana_code", thana_code);
                SqlParameter savedansheelta4 = new SqlParameter("@savedansheelta", savedansheelta);
                SqlParameter Matter_Status4 = new SqlParameter("@Matter_Status", Matter_Status);

                SqlParameter FromDate4 = new SqlParameter("@FromDate", FromDate);
                SqlParameter ToDate4 = new SqlParameter("@ToDate", ToDate);
                dt = clsData.GetDataTableWithProc("newDashboardLandDisputeDetailsEntry", new SqlParameter[] { QueryType4, CommissionaryCode4, District4, BlockCode4, PanchayatCode4, thana_code4, savedansheelta4, Matter_Status4, FromDate4, ToDate4 });
                if (dt.Rows.Count > 0)
                {
                    d.praathamikee = dt.Rows[0]["praathamikee"].ToString();
                    d.apraathamikee = dt.Rows[0]["apraathamikee"].ToString();
                    d.sanaha = dt.Rows[0]["sanaha"].ToString();
                }



                SqlParameter QueryType5 = new SqlParameter("@QueryType", Convert.ToInt32("3"));
                SqlParameter CommissionaryCode5 = new SqlParameter("@CommissionaryCode", "0");
                SqlParameter District5 = new SqlParameter("@District", District);
                SqlParameter BlockCode5 = new SqlParameter("@BlockCode", BlockCode);
                SqlParameter PanchayatCode5 = new SqlParameter("@PanchayatCode", PanchayatCode);
                SqlParameter thana_code5 = new SqlParameter("@thana_code", thana_code);
                SqlParameter savedansheelta5 = new SqlParameter("@savedansheelta", savedansheelta);
                SqlParameter Matter_Status5 = new SqlParameter("@Matter_Status", Matter_Status);

                SqlParameter FromDate5 = new SqlParameter("@FromDate", FromDate);
                SqlParameter ToDate5 = new SqlParameter("@ToDate", ToDate);
                dt = clsData.GetDataTableWithProc("newDashboardCourtDisputeDetailsEntry", new SqlParameter[] { QueryType5, CommissionaryCode5, District5, BlockCode5, PanchayatCode5, thana_code5, savedansheelta5, Matter_Status5, FromDate5, ToDate5 });
                if (dt.Rows.Count > 0)
                {
                    d.raajasv_nyaayaalay= dt.Rows[0]["raajasv_nyaayaalay"].ToString();
                    d.vyavahaara_nyaayaalay= dt.Rows[0]["vyavahaara_nyaayaalay"].ToString();
                    d.LokShikayat_Nivaran_nyaayaalay = dt.Rows[0]["LokShikayat_Nivaran_nyaayaalay"].ToString();
                    d.uchcha_nyaayaalay = dt.Rows[0]["uchcha_nyaayaalay"].ToString();
                    d.sarvochcha_nyaayaalay = dt.Rows[0]["sarvochcha_nyaayaalay"].ToString();
                }

            }
            else
            {
                d.Total = "0";
                d.Finalize ="0";
                d.Unfinalize ="0";

                d.saamaany ="0";
                d.sanvedanasheel ="0";
                d.atisanvedanasheel ="0";

                d.Nirast ="0";
                d.Mapi_Nirdharit ="0";
                d.Prakriyadhin ="0";
                d.Ashwikrit ="0";
                d.FinalNirast ="0";

                d.BhumiVivadType1 ="0";
                d.BhumiVivadType2 ="0";
                d.BhumiVivadType3 ="0";
                d.BhumiVivadType4 ="0";
                d.BhumiVivadType5 ="0";
                d.BhumiVivadType6 ="0";
                d.BhumiVivadType7 ="0";
                d.BhumiVivadType8 ="0";
                d.BhumiVivadType9 ="0";
                d.BhumiVivadType10 ="0";
                d.BhumiVivadType11 ="0";
                d.BhumiVivadType12 ="0";
                d.BhumiVivadType13 ="0";
                d.BhumiVivadType15 ="0";
                d.BhumiVivadType20 ="0";

                d.Landdesciption1 ="0";
                d.Landdesciption2 ="0";
                d.Landdesciption3 ="0";
                d.Landdesciption5 ="0";
                d.Landdesciption6 ="0";
                d.Landdesciption11 ="0";
                d.Landdesciption12 ="0";
                d.Landdesciption13 ="0";

                d.vadiEvidence1 ="0";
                d.vadiEvidence2 ="0";
                d.vadiEvidence3 ="0";
                d.vadiEvidence4 ="0";
                d.vadiEvidence5 ="0";
                d.vadiEvidence6 ="0";
                d.vadiEvidence7 ="0";
                d.vadiEvidence8 ="0";
                d.vadiEvidence9 ="0";

                d.PrativadiEvidence1 ="0";
                d.PrativadiEvidence2 ="0";
                d.PrativadiEvidence3 ="0";
                d.PrativadiEvidence4 ="0";
                d.PrativadiEvidence5 ="0";
                d.PrativadiEvidence6 ="0";
                d.PrativadiEvidence7 ="0";
                d.PrativadiEvidence8 ="0";
                d.PrativadiEvidence9 ="0";

                d.praathamikee ="0";
                d.apraathamikee ="0";
                d.sanaha ="0";

                d.raajasv_nyaayaalay ="0";
                d.vyavahaara_nyaayaalay ="0";
                d.LokShikayat_Nivaran_nyaayaalay ="0";
                d.uchcha_nyaayaalay ="0";
                d.sarvochcha_nyaayaalay ="0";
    }
            colResult.Add(d);
            return colResult;
        }
    }


    public class PanchayatList
    {
        public string PanchayatCode { set; get; }
        public string PanchayatName { set; get; }
        public string AreaType { set; get; }
        public static List<PanchayatList> GetPanchayat(string BlockCode)
        {
            clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
            List<PanchayatList> colResult = new List<PanchayatList>();

            System.Data.DataTable dt = new DataTable();
            dt = clsData.GetDataTable("select AreaType, PanchayatCode,PanchayatName from mst_Panchayats where BlockCode='" + BlockCode + "'  order by PanchayatName");
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PanchayatList tmpBlock = new PanchayatList();
                tmpBlock.PanchayatCode = dt.Rows[i]["PanchayatCode"].ToString();
                tmpBlock.PanchayatName = dt.Rows[i]["PanchayatName"].ToString();
                tmpBlock.AreaType = dt.Rows[i]["AreaType"].ToString();
                colResult.Add(tmpBlock);
            }
            return colResult;
        }
    }
    public class WardVillageList
    {
        public string Code { set; get; }
        public string Name { set; get; }
        public string AreaType { set; get; }
        public static List<WardVillageList> GetPanchayat(string PanchayatCode, string AreaType)
        {
            clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
            List<WardVillageList> colResult = new List<WardVillageList>();

            System.Data.DataTable dt = new DataTable();
            string sql = "";
            if(AreaType=="U")
            {
                sql = @"select PANCHAYATCODE,WARDCODE as Code,WARDNAME as Name from mst_Wards  where PANCHAYATCODE=@PanchayatCode order by WARDCODE";
            }
            else if(AreaType=="R")
            {
                sql = @"select DISTINCT v.VILLCODE as Code, v.VILLNAME as Name,p.PanchayatCode as PANCHAYATCODE  from mst_Panchayats p 
                            inner join PanchayatVillage pv on p.PanchayatCode=pv.PanchayatCode
                            inner join mst_VillageMaster v on v.VILLCODE=pv.VillageCode
                            where p.PanchayatCode=@PanchayatCode order by 	 v.VILLNAME	";
            }
            SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", PanchayatCode);
            dt = clsData.GetDataTable(sql, new SqlParameter[] { _PanchayatCode });

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                WardVillageList tmpBlock = new WardVillageList();
                tmpBlock.Code = dt.Rows[i]["Code"].ToString();
                tmpBlock.Name = dt.Rows[i]["Name"].ToString();
                tmpBlock.AreaType = AreaType;
                colResult.Add(tmpBlock);
            }
            return colResult;
        }
    }
}