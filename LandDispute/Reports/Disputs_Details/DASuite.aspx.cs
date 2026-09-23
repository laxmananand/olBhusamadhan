using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LandDispute_Reports_Disputs_Details_DASuite : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    public class Root
    {
        public string appId { get; set; }
        public string appName { get; set; }
        public string serviceNature { get; set; }
        public string district { get; set; }
        public int districtId { get; set; }
        public int stateId { get; set; }
        public string stateName { get; set; }
        public int subdivId { get; set; }
        public string subdivName { get; set; }
        public int blockId { get; set; }
        public string blockName { get; set; }
        public int totalReceived { get; set; }
        public int totalDelivered { get; set; }
        public int totalRejected { get; set; }
        public int totalPending { get; set; }
        public string createdOn { get; set; }
    }
    public class ResponseData
    {
        public int statusCode { get; set; }
        public string status { get; set; }
        public object remarks { get; set; }
        public string message { get; set; }
        public int size { get; set; }
        public object data { get; set; }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string dtt = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");
        DataTable dt = new DataTable();
        string sql = @" with cte as    
	  (  
	  select  a_id,Matter_Status,Bhumi_savedansheelta,Matter_Status_date,ROW_NUMBER() over ( partition by a_id order by  a_id,Matter_Status_date desc )  as 'rowvalue' from [ActionDetailsEntry]  
	  where Id in (select max(id) from ActionDetailsEntry  group by a_id)  
	  ) 
select 
mt.DISTRICTCODE,mt.DISTRICTNAME,
ta.appId,ta.appName,
ta.serviceNature,
isnull(ta.TotalReceived,0)as TotalReceived,
isnull(ta.TotalDelivered,0)as TotalDelivered,
isnull(ta.TotalRejected,0)as TotalRejected,
isnull(ta.TotalPending,0)as TotalPending 
from mst_Commissionary_Districts mt
left join
(
select mst.DISTRICTCODE,111 as appId,'Bhu-Samadhan' as appName,'' as serviceNature,count(*)TotalReceived,
sum(case when (cte.Matter_Status=6 or cte.Matter_Status=1)then 1 else 0 end)TotalDelivered,
sum(case when (cte.Matter_Status=4)then 1 else 0 end)TotalRejected,
sum(case when (cte.Matter_Status=2 or cte.Matter_Status=3 or cte.Matter_Status=6)then 1 else 0 end)TotalPending
from Matter_Registration mr
inner join mst_thana th on mr.Thana_code=th.PS_Code
left join cte on mr.a_id=cte.a_id and rowvalue=1
right join mst_Commissionary_Districts mst on mst.DISTRICTCODE=mr.District_Code
group by mst.DISTRICTCODE
) ta on mt.DISTRICTCODE=ta.DISTRICTCODE";

        dt = clsData.GetDataTable(sql);

        foreach (DataRow dr in dt.Rows)
        {
            try
            {
                Root data = new Root();
                data.appId = dr["appId"].ToString();
                data.appName = dr["appName"].ToString();
                data.district = dr["DISTRICTNAME"].ToString();
                data.districtId = Convert.ToInt32(dr["DISTRICTCODE"].ToString());
                data.serviceNature = dr["serviceNature"].ToString();
                data.totalReceived = Convert.ToInt32(dr["TotalReceived"].ToString());
                data.totalDelivered = Convert.ToInt32(dr["TotalDelivered"].ToString());
                data.totalRejected = Convert.ToInt32(dr["TotalRejected"].ToString());
                data.totalPending = Convert.ToInt32(dr["TotalPending"].ToString());
                data.createdOn = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");
                var request = (HttpWebRequest)WebRequest.Create("http://dasuite.bih.nic.in/das/Extdata/extData");
                var postData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                var requestdata = System.Text.Encoding.ASCII.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = requestdata.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(requestdata, 0, requestdata.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                ResponseData dResponseData = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseData>(responseString);
                hdnMessage.Value = dResponseData.status + " message" + dResponseData.message;
            }
            catch(Exception ex)
            {
                hdnMessage.Value =ex.Message;
            }
            
        }

    }
}