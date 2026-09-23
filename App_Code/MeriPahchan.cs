using Microsoft.ReportingServices.DataProcessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;


/// <summary>
/// Summary description for MeriPahchan
/// </summary>
public class MeriPahchan
{
	public MeriPahchan()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public class MeriPehchaanSSO_Library
    {
        public static string GenerateRandonAlphanumeric(int length)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string text = "1234567890";
            string text2 = text;
            text2 = text2 + str + text;
            string text3 = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string text4 = string.Empty;
                do
                {
                    int num = new Random().Next(0, text2.Length);
                    text4 = text2.ToCharArray()[num].ToString();
                }
                while (text3.IndexOf(text4) != -1);
                text3 += text4;
            }
            return text3;
        }
        public static string GenerateRandomNumber(int length)
        {
            string text = "1234567890";
            string text2 = text;
            string text3 = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string text4 = string.Empty;
                do
                {
                    int num = new Random().Next(0, text2.Length);
                    text4 = text2.ToCharArray()[num].ToString();
                }
                while (text3.IndexOf(text4) != -1);
                text3 += text4;
            }
            return text3;
        }
    }



    public class MeriPehchaanSSO_LoginResponse
    {
        public string Response
        {
            get;
            set;
        }
        public short Status
        {
            get;
            set;
        }
        public short DistrictCode
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string LoginUserType
        {
            get;
            set;
        }
        public string DistrictName
        {
            get;
            set;
        }
        public string AadhaarNo
        {
            get;
            set;
        }
        public string MobileNo
        {
            get;
            set;
        }
        public string HindiName
        {
            get;
            set;
        }
        public string LoginId
        {
            get;
            set;
        }
        public string IsFillApplicant
        {
            get;
            set;
        }
        public string Area
        {
            get;
            set;
        }
        public string ApplicationId
        {
            get;
            set;
        }
        public string IsFullSubmit
        {
            get;
            set;
        }
        public string IsResetPwd
        {
            get;
            set;
        }
        public string RegistrationAadhaarNo
        {
            get;
            set;
        }
    }


    public class Validate_Token
    {
        public Decryption_Response GetCookiesValue()
        {
            Decryption_Response result = new Decryption_Response();
            if (HttpContext.Current.Request.Cookies["MeriPehchaanCookies"] != null)
            {
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies["MeriPehchaanCookies"];
                string value = string.Empty;
                if (httpCookie != null)
                {
                    value = httpCookie["MeriPehchaanCookies_Decryption"].ToString();
                    result = JsonConvert.DeserializeObject<Decryption_Response>(value);
                }
            }
            return result;
        }
        private Token_Validate_Response Validate_Is_Client_Token_Valid()
        {
            AppConfiguration appConfiguration = new AppConfiguration();
            Token_Validate_Response token_Validate_Response = new Token_Validate_Response();
            Decryption_Response decryption_Response = new Decryption_Response();
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies["MeriPehchaanCookies"];
            string value = string.Empty;
            if (httpCookie != null)
            {
                value = httpCookie["MeriPehchaanCookies_Decryption"].ToString();
                decryption_Response = JsonConvert.DeserializeObject<Decryption_Response>(value);
                if (decryption_Response != null && !string.IsNullOrEmpty(decryption_Response.data.signature.sessionId) && !string.IsNullOrEmpty(decryption_Response.data.signature.browserId) && !string.IsNullOrEmpty(decryption_Response.data.signature.clientToken))
                {
                    string validate_token_service_url = string.Concat(new string[]
				{
						appConfiguration.CLIENT_URL_PROTOCOL,
						appConfiguration.CLIENT_URL,
						":",
						appConfiguration.CLIENT_URL_PORT,
						"/isTokenValid?clientToken=",
						decryption_Response.data.signature.clientToken,
						"&sid=",
						appConfiguration.JAN_PARICHAY_SERVICE_ID,
						"&sessionId=",
						decryption_Response.data.signature.sessionId,
						"&browserId=",
						decryption_Response.data.signature.browserId
				});
                    MeriPehchaanSSO_Execute_Login meriPehchaanSSO_Execute_Login = new MeriPehchaanSSO_Execute_Login();
                    LoginResponse loginResponse = new LoginResponse();
                    loginResponse = (LoginResponse)HttpContext.Current.Session["LoginInfo"];
                    string text = string.Empty;
                    if (HttpContext.Current.Session["LoginInfo"] != null)
                    {
                        LoginResponse loginResponse2 = new LoginResponse();
                        loginResponse2 = (LoginResponse)HttpContext.Current.Session["LoginInfo"];
                        text = loginResponse2.LoginId;
                    }
                    token_Validate_Response = meriPehchaanSSO_Execute_Login.Call_Check_Token_Valid(validate_token_service_url, string.IsNullOrEmpty(text) ? "" : text);
                }
                else
                {
                    token_Validate_Response.Status = "500";
                    token_Validate_Response.tokenValid = "false";
                }
            }
            else
            {
                token_Validate_Response.Status = "500";
                token_Validate_Response.tokenValid = "false";
            }
            return token_Validate_Response;
        }
        public string Check_PeriPehchaan_Token_Validity()
        {
            string result = string.Empty;
            Token_Validate_Response token_Validate_Response = this.Validate_Is_Client_Token_Valid();
            if (token_Validate_Response.tokenValid == "false")
            {
                MeriPehchaanSSO_Execute_Logout meriPehchaanSSO_Execute_Logout = new MeriPehchaanSSO_Execute_Logout();
                Decryption_Response decryption_Response = new Decryption_Response();
                AppConfiguration appConfiguration = new AppConfiguration();
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies["MeriPehchaanCookies"];
                string value = string.Empty;
                if (httpCookie != null)
                {
                    value = httpCookie["MeriPehchaanCookies_Decryption"].ToString();
                    decryption_Response = JsonConvert.DeserializeObject<Decryption_Response>(value);
                    if (decryption_Response != null && !string.IsNullOrEmpty(decryption_Response.data.signature.sessionId) && !string.IsNullOrEmpty(decryption_Response.data.signature.browserId) && !string.IsNullOrEmpty(decryption_Response.data.signature.clientToken))
                    {
                        LoginResponse loginResponse = new LoginResponse();
                        loginResponse = (LoginResponse)HttpContext.Current.Session["LoginInfo"];
                        string registrationLoginId = string.Empty;
                        if (HttpContext.Current.Session["LoginInfo"] != null)
                        {
                            LoginResponse loginResponse2 = new LoginResponse();
                            loginResponse2 = (LoginResponse)HttpContext.Current.Session["LoginInfo"];
                            registrationLoginId = loginResponse2.LoginId;
                        }
                        APIResponse aPIResponse = meriPehchaanSSO_Execute_Logout.Create_JanParichay_Session_Clear_Url(decryption_Response, registrationLoginId);
                        if (aPIResponse.Status == "200")
                        {
                            string message = aPIResponse.Message;
                            HttpContext.Current.Response.Cookies["MeriPehchaanCookies"].Expires = DateTime.Now.AddDays(-1.0);
                            result = "~/LandDispute/BLKADM/Default.aspx";
                        }
                    }
                }
                else
                {
                    result = "~/LandDispute/BLKADM/Default.aspx";
                }
            }
            return result;
        }
        public string Check_PeriPehchaan_Token_Validity_To_DirectRedirect()
        {
            string result = string.Empty;
            Token_Validate_Response token_Validate_Response = this.Validate_Is_Client_Token_Valid();
            if (token_Validate_Response.tokenValid == "false")
            {
                MeriPehchaanSSO_Execute_Logout meriPehchaanSSO_Execute_Logout = new MeriPehchaanSSO_Execute_Logout();
                Decryption_Response decryption_Response = new Decryption_Response();
                AppConfiguration appConfiguration = new AppConfiguration();
                HttpCookie httpCookie = HttpContext.Current.Request.Cookies["MeriPehchaanCookies"];
                string value = string.Empty;
                if (httpCookie != null)
                {
                    value = httpCookie["MeriPehchaanCookies_Decryption"].ToString();
                    decryption_Response = JsonConvert.DeserializeObject<Decryption_Response>(value);
                    if (decryption_Response != null && !string.IsNullOrEmpty(decryption_Response.data.signature.sessionId) && !string.IsNullOrEmpty(decryption_Response.data.signature.browserId) && !string.IsNullOrEmpty(decryption_Response.data.signature.clientToken))
                    {
                        LoginResponse loginResponse = new LoginResponse();
                        loginResponse = (LoginResponse)HttpContext.Current.Session["LoginInfo"];
                        string registrationLoginId = string.Empty;
                        if (HttpContext.Current.Session["LoginInfo"] != null)
                        {
                            LoginResponse loginResponse2 = new LoginResponse();
                            loginResponse2 = (LoginResponse)HttpContext.Current.Session["LoginInfo"];
                            registrationLoginId = loginResponse2.LoginId;
                        }
                        APIResponse aPIResponse = meriPehchaanSSO_Execute_Logout.Create_JanParichay_Session_Clear_Url(decryption_Response, registrationLoginId);
                        if (aPIResponse.Status == "200")
                        {
                            string message = aPIResponse.Message;
                            HttpContext.Current.Response.Cookies["MeriPehchaanCookies"].Expires = DateTime.Now.AddDays(-1.0);
                            result = "~/LandDispute/BLKADM/Default.aspx";
                        }
                    }
                }
                else
                {
                    result = "~/LandDispute/BLKADM/Default.aspx";
                }
            }
            else
            {
                result = token_Validate_Response.tokenValid;
            }
            return result;
        }
    }


    public class AES_Encryption_Request
    {
        public string AESString
        {
            get;
            set;
        }
    }



    public class All_Logout_Request
    {
        public string clientToken
        {
            get;
            set;
        }
        public string sid
        {
            get;
            set;
        }
        public string sessionId
        {
            get;
            set;
        }
        public string browserId
        {
            get;
            set;
        }
        public string ua
        {
            get;
            set;
        }
    }


    public class All_Logout_Response
    {
        public string status
        {
            get;
            set;
        }
        public string message
        {
            get;
            set;
        }
    }


    public class APIRequestResponse
    {
        public long RowId
        {
            get;
            set;
        }
        public string API_Url
        {
            get;
            set;
        }
        public short API_ID
        {
            get;
            set;
        }
        public string API_Request
        {
            get;
            set;
        }
        public string API_Response
        {
            get;
            set;
        }
        public string IPAddress
        {
            get;
            set;
        }
        public string RCMS_Login_Id
        {
            get;
            set;
        }
        public string GUID
        {
            get;
            set;
        }
        public short TypeId
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
    }


    public class APIResponse
    {
        public string Status
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
    }


    public class AppConfiguration
    {
        public string CLIENT_URL_PROTOCOL
        {
            get
            {
                return ConfigurationManager.AppSettings["CLIENT_URL_PROTOCOL"].ToString();
            }
        }
        public string CLIENT_URL
        {
            get
            {
                return ConfigurationManager.AppSettings["CLIENT_URL"].ToString();
            }
        }
        public string CLIENT_URL_PORT
        {
            get
            {
                return ConfigurationManager.AppSettings["CLIENT_URL_PORT"].ToString();
            }
        }
        public string JAN_PARICHAY_SERVICE_ID
        {
            get
            {
                return ConfigurationManager.AppSettings["JAN_PARICHAY_SERVICE_ID"].ToString();
            }
        }
        public string CLIENT_TID
        {
            get
            {
                return ConfigurationManager.AppSettings["CLIENT_TID"].ToString();
            }
        }
        public string JAN_PARICHAY_URL
        {
            get
            {
                return ConfigurationManager.AppSettings["JAN_PARICHAY_URL"].ToString();
            }
        }
        public string CLIENT_FULL_URL
        {
            get
            {
                return ConfigurationManager.AppSettings["CLIENT_FULL_URL"].ToString();
            }
        }
        public string ACCESS_DB
        {
            get
            {
                return (ConfigurationManager.AppSettings["ACCESS_DB"] == null) ? "NO" : ConfigurationManager.AppSettings["ACCESS_DB"].ToString();
            }
        }
    }




    public class CommonResponse
    {
        public string status
        {
            get;
            set;
        }
        public string message
        {
            get;
            set;
        }
        public Data data
        {
            get;
            set;
        }
        public CommonResponse()
        {
            this.data = new Data();
        }
    }


    public class Data
    {
        public string signature
        {
            get;
            set;
        }
    }


    public class DB_ACCESS
    {
        public const string YES = "YES";
        public const string NO = "NO";
    }


    public sealed class DBConfiguration
    {
        public const string YES = "YES";
        public const string NO = "NO";
        public string MeriPehchaanSSO_DBCXN_ConnectionString = ConfigurationManager.ConnectionStrings["MeriPehchaanSSO_DB"].ConnectionString;
        private int _connectionTimeOut;
        public int ConnectionTimeout
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionTimeOut_DB"]))
                {
                    this._connectionTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["ConnectionTimeOut_DB"].ToString());
                }
                else
                {
                    this._connectionTimeOut = 300;
                }
                return this._connectionTimeOut;
            }
        }
        public string ACCESS_DB
        {
            get
            {
                return string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.AppSettings["ACCESS_DB"])) ? "NO" : Convert.ToString(ConfigurationManager.AppSettings["ACCESS_DB"]).Trim();
            }
        }
        //public IDbConnection MeriPehchaanSSO_DBCXN
        //{
        //    get
        //    {
        //        return new SqlConnection(ConfigurationManager.ConnectionStrings["MeriPehchaanSSO_DB"].ConnectionString);
        //    }
        //}
        //public void OpenConnection(IDbConnection dbConnection)
        //{
        //    if (dbConnection.State == ConnectionState.Closed)
        //    {
        //        dbConnection.Open();
        //    }
        //}
        //public void CloseConnection(IDbConnection dbConnection)
        //{
        //    if (dbConnection.State == ConnectionState.Open)
        //    {
        //        dbConnection.Close();
        //    }
        //}
    }



    public class Decryption_Data
    {
        public Decryption_Signature signature
        {
            get;
            set;
        }
        public Decryption_Data()
        {
            this.signature = new Decryption_Signature();
        }
    }


    public class Decryption_Request
    {
        public string EncryptedString
        {
            get;
            set;
        }
    }


    public class Decryption_Response
    {
        public string status
        {
            get;
            set;
        }
        public string message
        {
            get;
            set;
        }
        public Decryption_Data data
        {
            get;
            set;
        }
        public Decryption_Response()
        {
            this.data = new Decryption_Data();
        }
    }



    public class Decryption_Signature
    {
        public string address
        {
            get;
            set;
        }
        public string authRole
        {
            get;
            set;
        }
        public string browserId
        {
            get;
            set;
        }
        public string city
        {
            get;
            set;
        }
        public string clientToken
        {
            get;
            set;
        }
        public string country
        {
            get;
            set;
        }
        public string departmentName
        {
            get;
            set;
        }
        public string designation
        {
            get;
            set;
        }
        public string dob
        {
            get;
            set;
        }
        public string email
        {
            get;
            set;
        }
        public string employeeCode
        {
            get;
            set;
        }
        public string expiresAt
        {
            get;
            set;
        }
        public string firstName
        {
            get;
            set;
        }
        public string fullName
        {
            get;
            set;
        }
        public string gender
        {
            get;
            set;
        }
        public string handShakingId
        {
            get;
            set;
        }
        public string ip
        {
            get;
            set;
        }
        public string issuer
        {
            get;
            set;
        }
        public string lastName
        {
            get;
            set;
        }
        public string localTokenId
        {
            get;
            set;
        }
        public string location
        {
            get;
            set;
        }
        public string loginId
        {
            get;
            set;
        }
        public List<string> mailAlternateAddress
        {
            get;
            set;
        }
        public List<string> mailEquivalentAddress
        {
            get;
            set;
        }
        public string mobileNo
        {
            get;
            set;
        }
        public string nicaccountexpdate
        {
            get;
            set;
        }
        public string parentToken
        {
            get;
            set;
        }
        public string parichayId
        {
            get;
            set;
        }
        public object profilePic
        {
            get;
            set;
        }
        public string role
        {
            get;
            set;
        }
        public string serviceId
        {
            get;
            set;
        }
        public string serviceState
        {
            get;
            set;
        }
        public string sessionId
        {
            get;
            set;
        }
        public string ssoType
        {
            get;
            set;
        }
        public string state
        {
            get;
            set;
        }
        public string stateRole
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
        public string subservice
        {
            get;
            set;
        }
        public string ua
        {
            get;
            set;
        }
        public string userId
        {
            get;
            set;
        }
        public string userName
        {
            get;
            set;
        }
        public string userRole
        {
            get;
            set;
        }
        public string userType
        {
            get;
            set;
        }
        public string user_id
        {
            get;
            set;
        }
        public VerificationIds verificationIds
        {
            get;
            set;
        }
        public string verificationParameters
        {
            get;
            set;
        }
        public string zimOtp
        {
            get;
            set;
        }
        public Decryption_Signature()
        {
            this.verificationIds = new VerificationIds();
        }
    }


    public class HandShakeRequest
    {
        public string handshakingId
        {
            get;
            set;
        }
        public string sid
        {
            get;
            set;
        }
    }


    public class HMAC_Request
    {
        private AppConfiguration APPCONFIGURATION;
        public string HmacString
        {
            get;
            set;
        }
        public HMAC_Request()
        {
            this.APPCONFIGURATION = new AppConfiguration();
            this.HmacString = string.Concat(new string[]
			{
				"JanParichay",
				this.APPCONFIGURATION.CLIENT_TID,
				this.APPCONFIGURATION.JAN_PARICHAY_URL,
				"v1/api/login",
				this.APPCONFIGURATION.JAN_PARICHAY_SERVICE_ID
			});
        }
    }


    public class STATUS
    {
        public const string SUCCESS = "200";
        public const string FAILED = "500";
    }

    public class Token_Validate_Message
    {
        public string status
        {
            get;
            set;
        }
        public string tokenValid
        {
            get;
            set;
        }
    }


    public class Token_Validate_Request
    {
        public string clientToken
        {
            get;
            set;
        }
        public string sessionId
        {
            get;
            set;
        }
        public string sid
        {
            get;
            set;
        }
        public string browserId
        {
            get;
            set;
        }
        public string ua
        {
            get;
            set;
        }
    }


    public class Token_Validate_Response
    {
        public string Status
        {
            get;
            set;
        }
        public string tokenValid
        {
            get;
            set;
        }
    }


    public class VerificationIds
    {
        public string mobile
        {
            get;
            set;
        }
        public string userid
        {
            get;
            set;
        }
        public string rcmsonlinedata
        {
            get;
            set;
        }
    }


    public class ApiRequestResponseDB
    {
        private DBConfiguration DBConfiguration;
        public ApiRequestResponseDB()
        {
            this.DBConfiguration = new DBConfiguration();
        }
        //public void SaveRequestResponse(APIRequestResponse apiRequestResponse)
        //{
        //    using (IDbConnection meriPehchaanSSO_DBCXN = this.DBConfiguration.MeriPehchaanSSO_DBCXN)
        //    {
        //        Microsoft.ReportingServices.DataProcessing.CommandType? commandType = new Microsoft.ReportingServices.DataProcessing.CommandType?(CommandType.StoredProcedure);
        //        SqlMapper.Execute(meriPehchaanSSO_DBCXN, "USP_SaveRequestResponse", new
        //        {
        //            apiRequestResponse.RowId,
        //            apiRequestResponse.API_Url,
        //            apiRequestResponse.API_Request,
        //            apiRequestResponse.API_Response,
        //            apiRequestResponse.GUID,
        //            apiRequestResponse.IPAddress,
        //            apiRequestResponse.RCMS_Login_Id,
        //            apiRequestResponse.TypeId,
        //            apiRequestResponse.API_ID
        //        }, null, null, commandType);
        //    }
        //}

        public void SaveRequestResponse(APIRequestResponse apiRequestResponse)
        { 
        }
    }


    public sealed class MeriPehchaanSSO_DBConnection
    {
        public SqlConnection MeriPehchaanSSO = new SqlConnection(ConfigurationManager.ConnectionStrings["MeriPehchaanSSO"].ConnectionString);
        public static string MeriPehchaanSSOConnectionString = ConfigurationManager.ConnectionStrings["MeriPehchaanSSO"].ConnectionString;
    }


    public class MeriPehchaanSSO_Execute_Login
    {
        private POST_API POST_API;
        private GET_API GET_API;
        private AppConfiguration APP_CONFIGURATION;
        private ApiRequestResponseDB ApiRequestResponseDB;
        private APIRequestResponse APIRequestResponse;
        public MeriPehchaanSSO_Execute_Login()
        {
            this.POST_API = new POST_API();
            this.GET_API = new GET_API();
            this.APP_CONFIGURATION = new AppConfiguration();
            this.ApiRequestResponseDB = new ApiRequestResponseDB();
            this.APIRequestResponse = new APIRequestResponse();
        }
        public string Generate_HMAC_String()
        {
            HMAC_Request hMAC_Request = new HMAC_Request();
            return hMAC_Request.HmacString;
        }
        public CommonResponse Call_HMAC_API(string _hmac_string)
        {
            HMAC_Request hMAC_Request = new HMAC_Request();
            hMAC_Request.HmacString = _hmac_string;
            CommonResponse result = new CommonResponse();
            string gUID = Guid.NewGuid().ToString();
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 1;
                this.APIRequestResponse.API_Request = JsonConvert.SerializeObject(hMAC_Request);
                this.APIRequestResponse.API_Url = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
					this.APP_CONFIGURATION.CLIENT_URL,
					":",
					this.APP_CONFIGURATION.CLIENT_URL_PORT,
					"/hmac"
				});
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 1;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            APIResponse aPIResponse = this.POST_API.CallAPI(string.Concat(new string[]
			{
				this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
				this.APP_CONFIGURATION.CLIENT_URL,
				":",
				this.APP_CONFIGURATION.CLIENT_URL_PORT,
				"/hmac"
			}), JsonConvert.SerializeObject(hMAC_Request));
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 1;
                this.APIRequestResponse.API_Response = (string.IsNullOrEmpty(aPIResponse.Message) ? "" : aPIResponse.Message);
                this.APIRequestResponse.API_Url = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
					this.APP_CONFIGURATION.CLIENT_URL,
					":",
					this.APP_CONFIGURATION.CLIENT_URL_PORT,
					"/hmac"
				});
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 2;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            if (aPIResponse.Status == "200")
            {
                result = JsonConvert.DeserializeObject<CommonResponse>(aPIResponse.Message);
            }
            return result;
        }
        public CommonResponse Call_AESString(AES_Encryption_Request aes_Encryption_Request)
        {
            CommonResponse result = new CommonResponse();
            string gUID = Guid.NewGuid().ToString();
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 2;
                this.APIRequestResponse.API_Request = JsonConvert.SerializeObject(aes_Encryption_Request);
                this.APIRequestResponse.API_Url = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
					this.APP_CONFIGURATION.CLIENT_URL,
					":",
					this.APP_CONFIGURATION.CLIENT_URL_PORT,
					"/encryption"
				});
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 1;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            APIResponse aPIResponse = this.POST_API.CallAPI(string.Concat(new string[]
			{
				this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
				this.APP_CONFIGURATION.CLIENT_URL,
				":",
				this.APP_CONFIGURATION.CLIENT_URL_PORT,
				"/encryption"
			}), JsonConvert.SerializeObject(aes_Encryption_Request));
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 2;
                this.APIRequestResponse.API_Response = (string.IsNullOrEmpty(aPIResponse.Message) ? "" : aPIResponse.Message);
                this.APIRequestResponse.API_Url = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
					this.APP_CONFIGURATION.CLIENT_URL,
					":",
					this.APP_CONFIGURATION.CLIENT_URL_PORT,
					"/encryption"
				});
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 2;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            if (aPIResponse.Status == "200")
            {
                result = JsonConvert.DeserializeObject<CommonResponse>(aPIResponse.Message);
            }
            return result;
        }
        public Decryption_Response Call_DecryptionString(Decryption_Request decryption_Request)
        {
            Decryption_Response result = new Decryption_Response();
            string gUID = Guid.NewGuid().ToString();
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 3;
                this.APIRequestResponse.API_Request = JsonConvert.SerializeObject(decryption_Request);
                this.APIRequestResponse.API_Url = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
					this.APP_CONFIGURATION.CLIENT_URL,
					":",
					this.APP_CONFIGURATION.CLIENT_URL_PORT,
					"/decryption"
				});
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 1;
                this.APIRequestResponse.Remarks = "";
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            APIResponse aPIResponse = this.POST_API.CallAPI(string.Concat(new string[]
			{
				this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
				this.APP_CONFIGURATION.CLIENT_URL,
				":",
				this.APP_CONFIGURATION.CLIENT_URL_PORT,
				"/decryption"
			}), JsonConvert.SerializeObject(decryption_Request));
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 3;
                this.APIRequestResponse.API_Response = (string.IsNullOrEmpty(aPIResponse.Message) ? "" : aPIResponse.Message);
                this.APIRequestResponse.API_Url = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
					this.APP_CONFIGURATION.CLIENT_URL,
					":",
					this.APP_CONFIGURATION.CLIENT_URL_PORT,
					"/decryption"
				});
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 2;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            if (aPIResponse.Status == "200")
            {
                result = JsonConvert.DeserializeObject<Decryption_Response>(aPIResponse.Message);
            }
            return result;
        }
        public APIResponse Call_HandShake(string url)
        {
            string gUID = Guid.NewGuid().ToString();
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 4;
                this.APIRequestResponse.API_Request = url;
                this.APIRequestResponse.API_Url = url;
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 1;
                this.APIRequestResponse.Remarks = "GET-API";
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            APIResponse aPIResponse = this.GET_API.CallAPI(url);
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 4;
                this.APIRequestResponse.API_Response = (string.IsNullOrEmpty(aPIResponse.Message) ? "" : aPIResponse.Message);
                this.APIRequestResponse.API_Url = url;
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 2;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            return aPIResponse;
        }
        public Token_Validate_Response Call_Check_Token_Valid(string _validate_token_service_url, string _registrationLoginId)
        {
            Token_Validate_Response result = new Token_Validate_Response();
            string gUID = Guid.NewGuid().ToString();
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 5;
                this.APIRequestResponse.API_Request = _validate_token_service_url;
                this.APIRequestResponse.API_Url = _validate_token_service_url;
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 1;
                this.APIRequestResponse.Remarks = "Validate-Token-GET-API";
                this.APIRequestResponse.RCMS_Login_Id = _registrationLoginId;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            APIResponse aPIResponse = this.GET_API.CallAPI(_validate_token_service_url);
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 5;
                this.APIRequestResponse.API_Response = (string.IsNullOrEmpty(aPIResponse.Message) ? "" : aPIResponse.Message);
                this.APIRequestResponse.API_Url = _validate_token_service_url;
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 2;
                this.APIRequestResponse.RCMS_Login_Id = _registrationLoginId;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            if (aPIResponse.Status == "200")
            {
                result = JsonConvert.DeserializeObject<Token_Validate_Response>(aPIResponse.Message);
            }
            return result;
        }
    }


    public class MeriPehchaanSSO_Execute_Logout
    {
        private POST_API POST_API;
        private GET_API GET_API;
        private AppConfiguration APP_CONFIGURATION;
        private ApiRequestResponseDB ApiRequestResponseDB = new ApiRequestResponseDB();
        private APIRequestResponse APIRequestResponse = new APIRequestResponse();
        public MeriPehchaanSSO_Execute_Logout()
        {
            this.POST_API = new POST_API();
            this.GET_API = new GET_API();
            this.APP_CONFIGURATION = new AppConfiguration();
        }
        public string Generate_HMAC_String()
        {
            HMAC_Request hMAC_Request = new HMAC_Request();
            return hMAC_Request.HmacString;
        }
        public CommonResponse Call_HMAC_API(string _hmac_string, string _registrationLoginId)
        {
            HMAC_Request hMAC_Request = new HMAC_Request();
            hMAC_Request.HmacString = _hmac_string;
            CommonResponse result = new CommonResponse();
            string gUID = Guid.NewGuid().ToString();
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 1;
                this.APIRequestResponse.API_Request = JsonConvert.SerializeObject(hMAC_Request);
                this.APIRequestResponse.API_Url = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
					this.APP_CONFIGURATION.CLIENT_URL,
					":",
					this.APP_CONFIGURATION.CLIENT_URL_PORT,
					"/hmac"
				});
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 1;
                this.APIRequestResponse.Remarks = "HMAC-LOGOUT";
                this.APIRequestResponse.RCMS_Login_Id = _registrationLoginId;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            APIResponse aPIResponse = this.POST_API.CallAPI(string.Concat(new string[]
			{
				this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
				this.APP_CONFIGURATION.CLIENT_URL,
				":",
				this.APP_CONFIGURATION.CLIENT_URL_PORT,
				"/hmac"
			}), JsonConvert.SerializeObject(hMAC_Request));
            if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
            {
                this.APIRequestResponse = new APIRequestResponse();
                this.APIRequestResponse.API_ID = 1;
                this.APIRequestResponse.API_Response = (string.IsNullOrEmpty(aPIResponse.Message) ? "" : aPIResponse.Message);
                this.APIRequestResponse.API_Url = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
					this.APP_CONFIGURATION.CLIENT_URL,
					":",
					this.APP_CONFIGURATION.CLIENT_URL_PORT,
					"/hmac"
				});
                this.APIRequestResponse.GUID = gUID;
                this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                this.APIRequestResponse.TypeId = 2;
                this.APIRequestResponse.Remarks = "HMAC-LOGOUT";
                this.APIRequestResponse.RCMS_Login_Id = _registrationLoginId;
                this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
            }
            if (aPIResponse.Status == "200")
            {
                result = JsonConvert.DeserializeObject<CommonResponse>(aPIResponse.Message);
            }
            return result;
        }
        public CommonResponse Call_AESString(AES_Encryption_Request aes_Encryption_Request)
        {
            CommonResponse result = new CommonResponse();
            APIResponse aPIResponse = this.POST_API.CallAPI(string.Concat(new string[]
			{
				this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
				this.APP_CONFIGURATION.CLIENT_URL,
				":",
				this.APP_CONFIGURATION.CLIENT_URL_PORT,
				"/encryption"
			}), JsonConvert.SerializeObject(aes_Encryption_Request));
            if (aPIResponse.Status == "200")
            {
                result = JsonConvert.DeserializeObject<CommonResponse>(aPIResponse.Message);
            }
            return result;
        }
        public APIResponse Create_JanParichay_Session_Clear_Url(Decryption_Response decryption_Response, string _registrationLoginId)
        {
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.Status = "500";
            string hmac_string = string.Concat(new string[]
			{
				"JanParichay",
				this.APP_CONFIGURATION.CLIENT_TID,
				this.APP_CONFIGURATION.JAN_PARICHAY_URL,
				"v1/salt/api/client/logout",
				decryption_Response.data.signature.clientToken,
				this.APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID,
				decryption_Response.data.signature.sessionId
			});
            CommonResponse commonResponse = this.Call_HMAC_API(hmac_string, _registrationLoginId);
            if (!string.IsNullOrEmpty(commonResponse.status) && commonResponse.status == "success")
            {
                string text = string.Concat(new string[]
				{
					this.APP_CONFIGURATION.JAN_PARICHAY_URL,
					"v1/salt/api/client/logout?clientToken=",
					decryption_Response.data.signature.clientToken,
					"&sid=",
					this.APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID,
					"&sessionId=",
					decryption_Response.data.signature.sessionId,
					"&browserId=",
					decryption_Response.data.signature.browserId,
					"&ua=",
					decryption_Response.data.signature.ua.Replace("||", ";"),
					"&tid=",
					this.APP_CONFIGURATION.CLIENT_TID,
					"&cs=",
					commonResponse.data.signature
				});
                if (!string.IsNullOrEmpty(text))
                {
                    aPIResponse.Status = "200";
                    aPIResponse.Message = text;
                    string gUID = Guid.NewGuid().ToString();
                    if (this.APP_CONFIGURATION.ACCESS_DB == "YES")
                    {
                        this.APIRequestResponse.API_ID = 6;
                        this.APIRequestResponse.API_Request = text;
                        this.APIRequestResponse.API_Url = text;
                        this.APIRequestResponse.GUID = gUID;
                        this.APIRequestResponse.IPAddress = MeriPehchaanLibraryServices.Get_IP_Address_Service();
                        this.APIRequestResponse.TypeId = 1;
                        this.APIRequestResponse.Remarks = "CLEAR-SESSION-OF-JANPARICHAY-LOGOUT";
                        this.APIRequestResponse.RCMS_Login_Id = _registrationLoginId;
                        this.ApiRequestResponseDB.SaveRequestResponse(this.APIRequestResponse);
                    }
                    this.GET_API.CallAPI(text);
                }
            }
            return aPIResponse;
        }
        public All_Logout_Response JanParichay_Client_LogoutAll(Decryption_Response decryption_Response)
        {
            All_Logout_Response all_Logout_Response = new All_Logout_Response();
            APIResponse aPIResponse = new APIResponse();
            aPIResponse.Status = "200";
            All_Logout_Request all_Logout_Request = new All_Logout_Request();
            all_Logout_Request.browserId = decryption_Response.data.signature.browserId;
            all_Logout_Request.clientToken = decryption_Response.data.signature.clientToken;
            all_Logout_Request.sid = this.APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID;
            all_Logout_Request.ua = decryption_Response.data.signature.ua.Replace("||", ";");
            all_Logout_Request.sessionId = decryption_Response.data.signature.sessionId;
            string text = string.Concat(new string[]
			{
				this.APP_CONFIGURATION.CLIENT_URL_PROTOCOL,
				this.APP_CONFIGURATION.CLIENT_URL,
				":",
				this.APP_CONFIGURATION.CLIENT_URL_PORT,
				"/logoutAll?clientToken=",
				all_Logout_Request.clientToken,
				"&sid=",
				this.APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID,
				"&sessionId=",
				all_Logout_Request.sessionId,
				"&browserId=",
				all_Logout_Request.browserId,
				"&ua=",
				all_Logout_Request.ua
			});
            aPIResponse = this.GET_API.CallAPI(text);
            if (aPIResponse != null && aPIResponse.Status == "200")
            {
                all_Logout_Response = JsonConvert.DeserializeObject<All_Logout_Response>(aPIResponse.Message);
    //            File.AppendAllText("E:\\RCMSLogFile\\alllogout_request.txt", string.Concat(new string[]
				//{
				//	DateTime.Now.ToString(),
				//	Environment.NewLine,
				//	Environment.NewLine,
				//	"URL: ",
				//	text,
				//	Environment.NewLine,
				//	JsonConvert.SerializeObject(all_Logout_Request),
				//	Environment.NewLine,
				//	"Response: ",
				//	Environment.NewLine,
				//	JsonConvert.SerializeObject(all_Logout_Response)
				//}));
            }
            return all_Logout_Response;
        }
    }



    public class APIPolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
        {
            return true;
        }
    }

    public class GET_API
    {
        public APIResponse CallAPI(string url)
        {
            APIResponse aPIResponse = new APIResponse();
            string message = string.Empty;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.CertificatePolicy = new APIPolicy();
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Method = "GET";
                webRequest.Timeout = -1;
                using (WebResponse response = webRequest.GetResponse())
                {
                    using (response.GetResponseStream())
                    {
                        StreamReader streamReader = new StreamReader(response.GetResponseStream());
                        message = streamReader.ReadToEnd();
                        aPIResponse.Message = message;
                        aPIResponse.Status = "200";
                    }
                }
            }
            catch (WebException ex)
            {
                try
                {
                    using (WebResponse response = ex.Response)
                    {
                        HttpWebResponse httpWebResponse = (HttpWebResponse)response;
                        using (Stream responseStream2 = response.GetResponseStream())
                        {
                            using (StreamReader streamReader2 = new StreamReader(responseStream2))
                            {
                                message = streamReader2.ReadToEnd();
                                aPIResponse.Message = message;
                                aPIResponse.Status = "500";
                            }
                        }
                    }
                }
                catch (Exception ex2)
                {
                    message = ex2.Message;
                    aPIResponse.Message = message;
                    aPIResponse.Status = "500";
                }
            }
            return aPIResponse;
        }
    }


    public class ManageCookies
    {
        public void Set_Decryption_Object_Cookie(string _cookie_object_key, string _cookie_key, string _cookie_value, char time_out_unit, int time_out_value)
        {
            HttpCookie httpCookie = new HttpCookie(_cookie_key);
            httpCookie[_cookie_key] = _cookie_value;
            if (time_out_unit != 'H')
            {
                if (time_out_unit == 'M')
                {
                    httpCookie.Expires.Add(new TimeSpan(0, time_out_value, 0));
                }
            }
            else
            {
                httpCookie.Expires.Add(new TimeSpan(time_out_value, 0, 0));
            }
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }
        public ResponseObject Get_Cookie_Object<ResponseObject>(ResponseObject responseObject, string cookie_object_key, string cookie_key)
        {
            HttpCookie httpCookie = HttpContext.Current.Request.Cookies[cookie_object_key];
            string value = string.Empty;
            if (httpCookie != null)
            {
                value = httpCookie[cookie_key].ToString();
                responseObject = JsonConvert.DeserializeObject<ResponseObject>(value);
            }
            return (ResponseObject)((object)Convert.ChangeType(responseObject, typeof(ResponseObject)));
        }
    }



    public class MeriPehchaanLibraryServices
    {
        public static string Get_IP_Address_Service()
        {
            string text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(text))
            {
                text = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return text;
        }
    }


    public class POST_API
    {
        public APIResponse CallAPI(string url, string request_data)
        {
            APIResponse aPIResponse = new APIResponse();
            string message = string.Empty;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.CertificatePolicy = new APIPolicy();
            HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
            httpWebRequest.ContentType = "application/json;charset=utf-8";
            httpWebRequest.Headers.Add("Access-Control-Allow-Origin", "*");
            httpWebRequest.MediaType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = -1;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(request_data);
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                    message = streamReader.ReadToEnd();
                    aPIResponse.Message = message;
                    aPIResponse.Status = "200";
                }
            }
            catch (WebException ex)
            {
                try
                {
                    using (WebResponse response = ex.Response)
                    {
                        HttpWebResponse httpWebResponse2 = (HttpWebResponse)response;
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader streamReader2 = new StreamReader(responseStream))
                            {
                                message = streamReader2.ReadToEnd();
                                aPIResponse.Message = message;
                                aPIResponse.Status = "500";
                            }
                        }
                    }
                }
                catch (Exception ex2)
                {
                    message = ex2.Message;
                    aPIResponse.Message = message;
                    aPIResponse.Status = "500";
                }
            }
            return aPIResponse;
        }
    }


    public class LoginResponse
    {
        public string Response
        {
            get;
            set;
        }
        public short Status
        {
            get;
            set;
        }
        public short DistrictCode
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string LoginUserType
        {
            get;
            set;
        }
        public string DistrictName
        {
            get;
            set;
        }
        public string AadhaarNo
        {
            get;
            set;
        }
        public string MobileNo
        {
            get;
            set;
        }
        public string HindiName
        {
            get;
            set;
        }
        public string LoginId
        {
            get;
            set;
        }
        public string IsFillApplicant
        {
            get;
            set;
        }
        public string Area
        {
            get;
            set;
        }
        public string ApplicationId
        {
            get;
            set;
        }
        public string IsFullSubmit
        {
            get;
            set;
        }
        public string IsResetPwd
        {
            get;
            set;
        }
        public string RegistrationAadhaarNo
        {
            get;
            set;
        }
    }



    public class AdminLoginResponse
    {
        public string Response
        {
            get;
            set;
        }
        public short Status
        {
            get;
            set;
        }
        public short DistrictCode
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }
        public string UserType
        {
            get;
            set;
        }
        public string LoginId
        {
            get;
            set;
        }
    }




    public class ExecutionResponse
    {
        public string Status
        {
            get;
            set;
        }
        public string Response
        {
            get;
            set;
        }
    }


    public class LibrarayService
    {
        public static class StaticItems
        {
            public static string EndPoint = "http://gd.geobytes.com/";
        }
        private EncryptionAlgo EncryptionAlgo;
        public static string EncryptValue(string plainText)
        {
            EncryptionAlgo encryptionAlgo = new EncryptionAlgo();
            string text = ConfigurationManager.AppSettings["EncryptionKey"].ToString();
            string salttedPassword = EncryptionAlgo.convertToMD5(text);
            return EncryptionAlgo.Encrypt(plainText, salttedPassword, text).Trim();
        }
        public static string SetHindiYesNoValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = ((value.Trim() == "N" || value == "No") ? "नहीं" : "हाँ");
            }
            else
            {
                value = "";
            }
            return value;
        }
        public static string DecryptValue(string encryptedText)
        {
            EncryptionAlgo encryptionAlgo = new EncryptionAlgo();
            string text = ConfigurationManager.AppSettings["EncryptionKey"].ToString();
            string salttedPassword = EncryptionAlgo.convertToMD5(text);
            return EncryptionAlgo.Decrypt(encryptedText, salttedPassword, text).Trim();
        }
        public static string SetValue(string value)
        {
            return string.IsNullOrEmpty(value) ? "" : value;
        }
        public static bool ValidateMobileNo(string mobileNo)
        {
            bool result = true;
            if (string.IsNullOrEmpty(mobileNo) || mobileNo.Length > 10 || mobileNo.Length < 10 || mobileNo.Trim() == "0000000000" || mobileNo.Trim() == "1111111111" || mobileNo.Trim() == "2222222222" || mobileNo.Trim() == "3333333333" || mobileNo.Trim() == "4444444444" || mobileNo.Trim() == "5555555555" || mobileNo.Trim() == "6666666666" || mobileNo.Trim() == "7777777777" || mobileNo.Trim() == "8888888888" || mobileNo.Trim() == "9999999999")
            {
                result = false;
            }
            return result;
        }
        public static GeoInfo GetSetIP()
        {
            GeoInfo geoInfo = new GeoInfo();
            string empty = string.Empty;
            if (string.IsNullOrEmpty(geoInfo.catchingIPAddress))
            {
                geoInfo.catchingIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(geoInfo.catchingIPAddress))
                {
                    geoInfo.catchingIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                if (!string.IsNullOrEmpty(geoInfo.catchingIPAddress) && geoInfo.catchingIPAddress == "::1")
                {
                    geoInfo.catchingIPAddress = "10.133.17.116";
                }
            }
            return geoInfo;
        }
        public static string GenerateCaptcha(int length)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string text = "1234567890";
            string text2 = text;
            text2 = text2 + str + text;
            string text3 = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string text4 = string.Empty;
                do
                {
                    int num = new Random().Next(0, text2.Length);
                    text4 = text2.ToCharArray()[num].ToString();
                }
                while (text3.IndexOf(text4) != -1);
                text3 += text4;
            }
            return text3;
        }
        public static string GenerateRandomNumber(int length)
        {
            string text = "1234567890";
            string text2 = text;
            string text3 = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string text4 = string.Empty;
                do
                {
                    int num = new Random().Next(0, text2.Length);
                    text4 = text2.ToCharArray()[num].ToString();
                }
                while (text3.IndexOf(text4) != -1);
                text3 += text4;
            }
            return text3;
        }
        public static string HindiYesAndNo(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Trim() == "N" || value.Trim() == "No" || value.Trim() == "NO")
                {
                    value = "नहीं";
                }
                else
                {
                    if (value.Trim() == "Y" || value.Trim() == "Yes" || value.Trim() == "YES")
                    {
                        value = "हाँ";
                    }
                }
            }
            else
            {
                value = "";
            }
            return value;
        }
        public static string ConvertDateFormat(string date)
        {
            return Convert.ToDateTime(date, CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat).ToString("yyyy-MM-dd");
        }
        public static bool ValidateImage(string imgFilePath)
        {
            bool result = false;
            try
            {
                using (FileStream fileStream = new FileStream(imgFilePath, FileMode.Open, FileAccess.Read))
                {
                    Image image = Image.FromStream(fileStream);
                    result = true;
                }
            }
            catch (Exception var_3_30)
            {
                result = false;
            }
            return result;
        }
        public static IMPDSDeduplicationResponse CheckAadhaarOnIMPDS(string planAdhaarNo, long batchId)
        {
            IMPDSDeduplicationResponse iMPDSDeduplicationResponse = new IMPDSDeduplicationResponse();
            List<CheckAdhaarRequest> list = new List<CheckAdhaarRequest>();
            list.Add(new CheckAdhaarRequest
            {
                encryptedUID = LibrarayService.IMPDSEncryptionProcess(planAdhaarNo),
                remarks = ""
            });
            HttpWebRequest httpWebRequest = WebRequest.Create(Convert.ToString(ConfigurationManager.AppSettings["IMPDSDuplicateUidCheckUrl"])) as HttpWebRequest;
            httpWebRequest.ContentType = "application/json;charset=utf-8";
            httpWebRequest.Headers.Add("Access-Control-Allow-Origin", "*");
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = -1;
            string text = JsonConvert.SerializeObject(list);
            string value = "10" + DateTime.Now.ToString("ddMMyyyyHHmmss") + (10000L * batchId + (long)Convert.ToInt32(batchId)).ToString();
            httpWebRequest.Headers.Add("token", value);
            httpWebRequest.Headers.Add("userAuthentication", Convert.ToString(ConfigurationManager.AppSettings["IMPDSAuthentication"]));
            httpWebRequest.Headers.Add("statecode", "10");
            text = "{\"parms\":" + text + "}";
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                    string text2 = streamReader.ReadToEnd();
                    if (!string.IsNullOrEmpty(text2))
                    {
                        iMPDSDeduplicationResponse = JsonConvert.DeserializeObject<IMPDSDeduplicationResponse>(text2);
                    }
                }
            }
            catch (WebException ex)
            {
                try
                {
                    using (WebResponse response = ex.Response)
                    {
                        HttpWebResponse httpWebResponse2 = (HttpWebResponse)response;
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (StreamReader streamReader2 = new StreamReader(responseStream))
                            {
                                string text2 = streamReader2.ReadToEnd();
                                iMPDSDeduplicationResponse.responseList.Add(new IMPDSCardInfo
                                {
                                    response = "Error"
                                });
                                iMPDSDeduplicationResponse.errorText = text2;
                            }
                        }
                    }
                }
                catch (Exception ex2)
                {
                    iMPDSDeduplicationResponse.responseList.Add(new IMPDSCardInfo
                    {
                        response = "Error"
                    });
                    iMPDSDeduplicationResponse.errorText = ex2.Message;
                }
            }
            catch (Exception ex2)
            {
                string text2 = ex2.Message;
                iMPDSDeduplicationResponse.responseList.Add(new IMPDSCardInfo
                {
                    response = "Error"
                });
                iMPDSDeduplicationResponse.errorText = ex2.Message;
            }
            return iMPDSDeduplicationResponse;
        }
        public static string IMPDSEncryptionProcess(string value)
        {
            EncryptionAlgo encryptionAlgo = new EncryptionAlgo();
            string text = ConfigurationManager.AppSettings["IMPDSEncryptionKey"].ToString();
            string salttedPassword = EncryptionAlgo.convertToMD5(text);
            return EncryptionAlgo.Encrypt(value, salttedPassword, text).Trim();
        }
    }



    internal sealed class EncryptionAlgo
    {
        public static string convertToMD5(string source)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(source);
            MD5 mD = MD5.Create();
            byte[] data = mD.ComputeHash(bytes);
            return EncryptionAlgo.convertToHex(data).ToLower();
        }
        public static string convertToHex(byte[] data)
        {
            char[] array = new char[data.Length * 2];
            for (int i = 0; i < data.Length; i++)
            {
                int num = data[i] >> 4;
                array[i * 2] = (char)(55 + num + (num - 10 >> 31 & -7));
                num = (int)(data[i] & 15);
                array[i * 2 + 1] = (char)(55 + num + (num - 10 >> 31 & -7));
            }
            return new string(array);
        }
        private static byte[] ComputeSha256Hash(string rawData)
        {
            byte[] result;
            using (SHA256 sHA = SHA256.Create())
            {
                byte[] array = sHA.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                Array.Resize<byte>(ref array, 16);
                result = array;
            }
            return result;
        }
        public static RijndaelManaged GetRijndaelManaged(string saltedValue, string pwd)
        {
            byte[] key = new byte[16];
            key = EncryptionAlgo.ComputeSha256Hash(saltedValue);
            byte[] bytes = Encoding.UTF8.GetBytes(pwd);
            Array.Resize<byte>(ref bytes, 16);
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = key,
                IV = bytes
            };
        }
        public static byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        public static string Encrypt(string strUID, string salttedPassword, string strIV2)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(strUID);
            string s = Convert.ToBase64String(EncryptionAlgo.Encrypt(bytes, EncryptionAlgo.GetRijndaelManaged(salttedPassword, strIV2)));
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }
        public static string Encrypt(string strUID, string salttedPassword)
        {
            string result;
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.GenerateIV();
                string @string = Encoding.UTF8.GetString(rijndaelManaged.IV, 0, rijndaelManaged.IV.Length);
                byte[] bytes = Encoding.UTF8.GetBytes(strUID);
                string s = Convert.ToBase64String(EncryptionAlgo.Encrypt(bytes, EncryptionAlgo.GetRijndaelManaged(salttedPassword, @string)));
                string text = Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
                result = text;
            }
            return result;
        }
        public static byte[] Decrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }
        public static string Decrypt(string strUID, string salttedPassword, string strIV2)
        {
            byte[] bytes = Convert.FromBase64String(strUID);
            string @string = Encoding.UTF8.GetString(bytes);
            byte[] plainBytes = Convert.FromBase64String(@string);
            return Encoding.UTF8.GetString(EncryptionAlgo.Decrypt(plainBytes, EncryptionAlgo.GetRijndaelManaged(salttedPassword, strIV2)));
        }
        public static string getMd5Hash(string input)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] array = mD5CryptoServiceProvider.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                stringBuilder.Append(array[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }




    public class GeoInfo
    {
        public string geobytesforwarderfor
        {
            get;
            set;
        }
        public string geobytesremoteip
        {
            get;
            set;
        }
        public string geobytesipaddress
        {
            get;
            set;
        }
        public string geobytescertainty
        {
            get;
            set;
        }
        public string geobytesinternet
        {
            get;
            set;
        }
        public string geobytescountry
        {
            get;
            set;
        }
        public string geobytesregionlocationcode
        {
            get;
            set;
        }
        public string geobytesregion
        {
            get;
            set;
        }
        public string geobytescode
        {
            get;
            set;
        }
        public string geobyteslocationcode
        {
            get;
            set;
        }
        public string geobytesdma
        {
            get;
            set;
        }
        public string geobytescity
        {
            get;
            set;
        }
        public string geobytescityid
        {
            get;
            set;
        }
        public string geobytesfqcn
        {
            get;
            set;
        }
        public string geobyteslatitude
        {
            get;
            set;
        }
        public string geobyteslongitude
        {
            get;
            set;
        }
        public string geobytescapital
        {
            get;
            set;
        }
        public string geobytestimezone
        {
            get;
            set;
        }
        public string geobytesnationalitysingular
        {
            get;
            set;
        }
        public string geobytespopulation
        {
            get;
            set;
        }
        public string geobytesnationalityplural
        {
            get;
            set;
        }
        public string geobytesmapreference
        {
            get;
            set;
        }
        public string geobytescurrency
        {
            get;
            set;
        }
        public string geobytescurrencycode
        {
            get;
            set;
        }
        public string geobytestitle
        {
            get;
            set;
        }
        public string catchingIPAddress
        {
            get;
            set;
        }
    }

    public class IMPDSCardInfo
    {
        public string DistrictName
        {
            get;
            set;
        }
        public string uid
        {
            get;
            set;
        }
        public string FPSid
        {
            get;
            set;
        }
        public string Statename
        {
            get;
            set;
        }
        public string response
        {
            get;
            set;
        }
        public string CardType
        {
            get;
            set;
        }
        public string MemberName
        {
            get;
            set;
        }
        public string RationCardNo
        {
            get;
            set;
        }
    }



    public class IMPDSDeduplicationResponse
    {
        [JsonProperty("responseList ")]
        public List<IMPDSCardInfo> responseList
        {
            get;
            set;
        }
        public string ack_id
        {
            get;
            set;
        }
        public string statecode
        {
            get;
            set;
        }
        public string token
        {
            get;
            set;
        }
        public string errorText
        {
            get;
            set;
        }
        public IMPDSDeduplicationResponse()
        {
            this.responseList = new List<IMPDSCardInfo>();
        }
    }

    public class CheckAdhaarRequest
    {
        public string encryptedUID
        {
            get;
            set;
        }
        public string remarks
        {
            get;
            set;
        }
    }


}