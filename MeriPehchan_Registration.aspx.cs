using EO.Web.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
//using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;
//using static MeriPahchan;

public partial class MeriPehchan_Registration : System.Web.UI.Page
{
	clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
	whiteList wl = new whiteList();

	public enum MessageType { success, error, info, warning };
	protected void ShowMessage(string Message, MessageType type, string Url)
	{
		ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "ShowMessage('" + Message.Replace("'", "") + "','" + type + "','" + Url + "');", true);
	}

	string Msg = "", Password = "", mailBody = "";
	protected void Page_Load(object sender, EventArgs e)
	{
		MeriPahchan.Decryption_Response _decrypt_Response = new MeriPahchan.Decryption_Response();
		HttpCookie _reqCookies = Request.Cookies["MeriPehchaanCookies"];
		string _cookies_values = string.Empty;
		if (_reqCookies != null)
		{
			_cookies_values = _reqCookies["MeriPehchaanCookies_Decryption"].ToString();
			_decrypt_Response = Newtonsoft.Json.JsonConvert.DeserializeObject<MeriPahchan.Decryption_Response>(_cookies_values);
		}

	    Session["Role"] = "PUBLIC";
		Session["UserID"] = _decrypt_Response.data.signature.loginId.Split('@')[0].ToString();
		Session["Username"] = _decrypt_Response.data.signature.loginId.Split('@')[0].ToString();
		txtUserName.Text= _decrypt_Response.data.signature.loginId.Split('@')[0].ToString();
		txtName.Text = _decrypt_Response.data.signature.fullName.ToString();
		txtmobile.Text = _decrypt_Response.data.signature.mobileNo.ToString();
		Session["Janparichayid"] = _decrypt_Response.data.signature.verificationIds.userid.ToString();

		if (Session["Role"] != null && !string.IsNullOrEmpty(Session["Role"].ToString()))
		{

			if (!IsPostBack)
			{
				BindRange();
				
			}

		}
		else
		{
			Response.Redirect("~/Login_Default.aspx");
		}
	}


	void BindRange()
	{

		try
		{
			string sql = @"SELECT distinct RANGE_NAME,RANGE_CODE from mst_Range ORDER BY RANGE_NAME ";
			DataTable dt = clsData.GetDataTable(sql);
			if (dt.Rows.Count > 0)
			{
				ddlRange.DataSource = dt;
				ddlRange.DataTextField = "RANGE_NAME";
				ddlRange.DataValueField = "RANGE_CODE";
				ddlRange.DataBind();
				ddlRange.Items.Insert(0, new ListItem("--Select--", "0"));
				dt.Dispose();
			}
		}
		catch (Exception ex) { }
	}


	void BindDist()
	{
		try
		{
			//string sql = @"SELECT distinct DISTRICTNAME,concat(DISTRICTCODE,'#',DIVISIONCODE)DISTRICTCODE from mst_Commissionary_Districts where RANGECODE=@Range_code ORDER BY DISTRICTNAME ";
            string sql = @"SELECT distinct DISTRICTNAME,DISTRICTCODE as DISTRICTCODE from mst_Commissionary_Districts where RANGECODE=@Range_code ORDER BY DISTRICTNAME ";
            SqlParameter _DCode = new SqlParameter("@Range_code", ddlRange.SelectedValue.ToString());
			DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode });
			if (dt.Rows.Count > 0)
			{
				ddlDistrict.DataSource = dt;
				ddlDistrict.DataTextField = "DISTRICTNAME";
				ddlDistrict.DataValueField = "DISTRICTCODE";
				ddlDistrict.DataBind();
				ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
				dt.Dispose();
			}
		}
		catch (Exception ex) { }

	}
	void BindSubDivision()
	{
		try
		{


			//string sql = @" select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En";
			string sql = @"select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code";
			//SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.Split('#')[0].ToString());
            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            //SqlParameter _thana_code = new SqlParameter("@thana_code", Session["Thana_Code"].ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode });


			ddlSubdivision.DataSource = dt;
			ddlSubdivision.DataTextField = "SubDivisionName";
			ddlSubdivision.DataValueField = "SubDivisionCode";
			ddlSubdivision.DataBind();
			ddlSubdivision.Items.Insert(0, new ListItem("--Select--", "0"));
			dt.Dispose();


		}
		catch { }

	}
	void BindBlock()
	{
		try
		{


			string sql = @" select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName";
			//string sql = @"select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and BlockCode in (select code from MstThanaMapping where thana_code=@thana_code)  order by BlockName";
			SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
			SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString());
			//SqlParameter _thana_code = new SqlParameter("@thana_code", Session["Thana_Code"].ToString());
			DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode });


			ddlBlock.DataSource = dt;
			ddlBlock.DataTextField = "BlockName";
			ddlBlock.DataValueField = "BlockCode";
			ddlBlock.DataBind();
			ddlBlock.Items.Insert(0, new ListItem("--Select--", "0"));
			dt.Dispose();


		}
		catch { }

	//	// BindVillage();
	//	// BindPanchyat();

	}
	void BindPolice()
	{
		try
		{
			string sql = @" select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t inner join SubDivisions p on t.Subdivision_Code =p.Sd_Code2 where p.Sd_Code2=@Subdivision_Code and p.DistCode=@District_Code  order by Police_Station";

			//sql = @"select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t 
   //                 where t.Subdivision_Code=@Subdivision_Code and t.District_code=@District_Code ";

             sql = @"select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t
	                        left join MstThanaMapping m on m.Thana_Code=t.PS_Code 
	                        left join Blocks b on b.BlockCode=m.Code and m.Type='Block'
	                        where District_code=@District_Code and  b.SubDivCode is not null and m.code=@Circle_Code and b.SubDivCode=@Subdivision_Code
                            ORDER BY Police_Station";


            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
			SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString());
            SqlParameter _CircleCode = new SqlParameter("@Circle_Code", ddlBlock.SelectedValue.ToString());
            //SqlParameter _RCode = new SqlParameter("@Range_code", ddlrange.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode, _CircleCode });
			ddlPolice.DataSource = dt;
			ddlPolice.DataTextField = "Police_Station";
			ddlPolice.DataValueField = "PS_Code";
			ddlPolice.DataBind();
			ddlPolice.Items.Insert(0, new ListItem("--Select--", "0"));
			dt.Dispose();


		}
		catch { }
	}

	


	public void Clear()
	{
		txtEmail.Text = "";
		txtName.Text = "";
		txtUserName.Text = "";
		//txtAuthorizedPersonMobileNo.Text = "";
	}


	protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindSubDivision();
		//BindBlock();
	}
	protected void ddlSubdivision_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindBlock();
		//BindPolice();

	}
	protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindPolice();

	}

 public  string Get_IP_Address_Service()
 {
     string text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
     if (string.IsNullOrEmpty(text))
     {
         text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
     }
     return text;
 }

	protected void btnAddDetail_Click(object sender, EventArgs e)
	{	
		if (CheckValidation() == true)
		{
			try
			{
				// SqlParameter _UserId = new SqlParameter("@Userid", "0");
				SqlParameter _UserName = new SqlParameter("@UserName",txtUserName.Text.Trim());
				SqlParameter _Name = new SqlParameter("@Name",txtName.Text);
				SqlParameter _Email = new SqlParameter("@Email", txtEmail.Text.Trim());
				SqlParameter _Mobile = new SqlParameter("@Mobile", txtmobile.Text.Trim());
				SqlParameter _PsCode = new SqlParameter("@Thana_Code", ddlPolice.SelectedValue.ToString().Trim());
				SqlParameter _block = new SqlParameter("@Block_Code", ddlBlock.SelectedValue.ToString().Trim());
				SqlParameter _District = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString().Trim());
				SqlParameter _UserRole = new SqlParameter("@Userrole", "PUBLIC");
				SqlParameter _Rangecode = new SqlParameter("@RangeCode", ddlRange.SelectedValue.ToString().Trim());
				SqlParameter _Subdivision = new SqlParameter("@Sub_DivCode", ddlSubdivision.SelectedValue.ToString().Trim());
				//SqlParameter _Commsionary = new SqlParameter("@Commsionary_Code", ddlDistrict.SelectedValue.ToString().Trim());
				
				SqlParameter _MeripahchanId = new SqlParameter("@JanparichayId", Session["Janparichayid"].ToString().Trim());
				SqlParameter _IPAddress = new SqlParameter("@IPAddress", Get_IP_Address_Service());
				
				DataTable dt = clsData.GetDataTableWithProc("usp_SaveCitizenRegisteration", new SqlParameter[] { _UserName, _Name, _Email, _Mobile, _PsCode, _block, _District, _UserRole, _Rangecode, _Subdivision,  _MeripahchanId, _IPAddress });
				if (dt.Rows.Count > 0)
				{
					Utility.showMessage(this, "User has been updated Successfully.");
					Response.Redirect("~/Login_Default.aspx");
				}
				else
				{
					Utility.showMessage(this, "User has not been updated Successfully.");
					return;
				}				
			}
			catch (Exception ex)
			{
			}
		}
	}



	public bool CheckValidation()
	{
		bool flag = true;
		if (ddlSubdivision.SelectedIndex == 0)
		{
			Utility.showMessage(this, "कृपया सब डिवीज़न चुनें...!");
			ddlSubdivision.Focus();
			flag = false;
		}

		if (ddlBlock.SelectedIndex == 0)
		{
			Utility.showMessage(this, "कृपया ब्लाक का चुनाव करें...!");
			ddlBlock.Focus();
			flag = false;
		}

		if (ddlPolice.SelectedIndex == 0)
		{
			Utility.showMessage(this, "कृपया पुलिस स्टेशन का चुनाव करें...!");
			ddlPolice.Focus();
			flag = false;
		}
		return flag;
	}



	public class Data
	{
		public Signature signature { get; set; }
	}

	public class Root
	{
		public string status { get; set; }
		public string message { get; set; }
		public Data data { get; set; }
	}

	public class Signature
	{
		public string address { get; set; }
		public string authRole { get; set; }
		public string browserId { get; set; }
		public string city { get; set; }
		public string clientToken { get; set; }
		public string country { get; set; }
		public string departmentName { get; set; }
		public string designation { get; set; }
		public string dob { get; set; }
		public string email { get; set; }
		public string employeeCode { get; set; }
		public object expiresAt { get; set; }
		public string firstName { get; set; }
		public string fullName { get; set; }
		public string gender { get; set; }
		public string handShakingId { get; set; }
		public string ip { get; set; }
		public string issuer { get; set; }
		public string lastName { get; set; }
		public string localTokenId { get; set; }
		public string location { get; set; }
		public string loginId { get; set; }
		public List<object> mailAlternateAddress { get; set; }
		public List<object> mailEquivalentAddress { get; set; }
		public string mobileNo { get; set; }
		public object nicaccountexpdate { get; set; }
		public string parentToken { get; set; }
		public string parichayId { get; set; }
		public string profilePic { get; set; }
		public string role { get; set; }
		public string serviceId { get; set; }
		public string serviceState { get; set; }
		public string sessionId { get; set; }
		public string ssoType { get; set; }
		public string state { get; set; }
		public string stateRole { get; set; }
		public string status { get; set; }
		public string subservice { get; set; }
		public string ua { get; set; }
		public string userId { get; set; }
		public string userName { get; set; }
		public object userRole { get; set; }
		public string userType { get; set; }
		public string user_id { get; set; }
		public VerificationIds verificationIds { get; set; }
		public string verificationParameters { get; set; }
		public string zimOtp { get; set; }
	}

	public class VerificationIds
	{
		public string mobile { get; set; }
		public string userid { get; set; }
		public object rcmsonlinedata { get; set; }
	}





	protected void ddlRange_SelectedIndexChanged(object sender, EventArgs e)
	{
		BindDist();
	}
}