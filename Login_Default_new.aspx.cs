using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.Security.Application;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
//using static MeriPahchan;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
//using static System.Net.Mime.MediaTypeNames;



public partial class Login_Default : System.Web.UI.Page
{
      clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();

      MeriPahchan objpahchan=new MeriPahchan();

      private MeriPahchan.AppConfiguration APP_CONFIGURATION;
      private MeriPahchan.Validate_Token Validate_Token;
    
     protected void Page_Load(object sender, EventArgs e)
       {
       

           APP_CONFIGURATION = new MeriPahchan.AppConfiguration();
           ScriptManager1.AsyncPostBackTimeout = 360000;


         try
        {
            if (Session["hidkey"] == null)
                setHidKey();
        }
        catch (Exception ex)
        {
        }
        this.Form.DefaultButton = btnLogin.UniqueID;

           if (!IsPostBack)
           {
               setHidKey();
               MeriPahchan.Validate_Token   Validate_Token = new MeriPahchan.Validate_Token();
               string validate_Token_Value = Validate_Token.Check_PeriPehchaan_Token_Validity_To_DirectRedirect();
               if (!String.IsNullOrEmpty(validate_Token_Value) && validate_Token_Value == "true")
               {
                   MeriPahchan.Decryption_Response _decrypt_Response = new MeriPahchan.Decryption_Response();
                   MeriPahchan.Validate_Token validate_Token = new MeriPahchan.Validate_Token();
                   _decrypt_Response = validate_Token.GetCookiesValue();

                   string _meri_pehchaan_Id = string.Empty;
                   if (_decrypt_Response != null)
                   {
                       if (!String.IsNullOrEmpty(_decrypt_Response.data.signature.verificationIds.rcmsonlinedata))
                       {
                           _meri_pehchaan_Id = _decrypt_Response.data.signature.verificationIds.rcmsonlinedata;
                            //UpdateMeriPehchaanId(_meri_pehchaan_Id, _decrypt_Response.data.signature.verificationIds.userid);
                          
                       }
                       else if (!String.IsNullOrEmpty(_decrypt_Response.data.signature.verificationIds.userid))
                       {
                           _meri_pehchaan_Id = _decrypt_Response.data.signature.verificationIds.userid;
                       }                       
					MeriPahchan.ExecutionResponse _meri_pehchaan_existance_response = CheckMeriPehchaanId(_meri_pehchaan_Id);
					if (_meri_pehchaan_existance_response.Status == "200")
					{
						
						JanParichay_Login(_decrypt_Response.data.signature.verificationIds.userid);
					}
					else if (_meri_pehchaan_existance_response.Status == "500")
					{
						string script = "alert('You are not registered with BHU-SAMADAHAN Portal, Please Register and Proceed.');";

						System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", script, true);
						string _securetoken;
						string hdnRegSecurity;
						_securetoken = MeriPahchan.LibrarayService.GenerateRandomNumber(5);
						Context.Session["_securetoken"] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((_securetoken), "MD5").ToLower().ToString();
						hdnRegSecurity = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((_securetoken), "MD5").ToLower().ToString();
						ScriptManager.RegisterStartupScript(this, this.GetType(), "Information", "alert('You are not registered with BHU-SAMADAHAN Portal, Please Register and Proceed.!'); location.href = 'MeriPehchan_Registration.aspx';", true);
					}

				}

               }
               CallOnPageLoad();
           }

       }


    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["hidkey"] == null)
    //            setHidKey();
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //   // string pwd = txtPassword.Text;
    //    //txtPassword.Attributes.Add("value", pwd);

    //    this.Form.DefaultButton = btnLogin.UniqueID;
    //    if (!IsPostBack)
    //    {
    //        setHidKey();
    //    }
    //}




    public void setHidKey()
    {

        Random rand = new Random();
        long randnum2 = (long)(rand.NextDouble() * 900000000000000) + 8000000000000000;

        Session["hidkey"] = randnum2.ToString();

        // HidKey.Value = "8080808080808080";
        HidKey.Value = Session["hidkey"].ToString();
        Session["aes256key"] = Session["hidkey"].ToString() + Session["hidkey"].ToString();
    }
    public void login()
    {
        string key = Encryptor.PrivateKey.ToString();
        Encryptor enc = new Encryptor(key);
        String PWD = AESEncryptDecrypt.DecryptStringAES(txtPassword.Text.Trim());
        string salt =PWD.Substring(0, 6);

        PWD = PWD.Remove(0, 6);


        string UserName = AESEncryptDecrypt.DecryptStringAES(txtUserName.Text);
        // string salt = PWD.Substring(0, 6);
        UserName = UserName.Remove(0, 6);

        DataTable dtRole = clsData.GetDataTable(@"SELECT isnull(UserLogin.IsStrong,'N')as 'IsStrong',UserLogin.Userrole, UserLogin.UserID, mst_Role.RoleDesc, UserLogin.Userrole AS UserRoleID, UserLogin.UserName,UserLogin.RangeCode,
                                                    UserLogin.Password, UserLogin.District_Code, UserLogin.Block_Code,mst_Panchayats.PanchayatCode,
                                            UserLogin.Sub_DivCode,
											UserLogin.Thana_Code,
											 UserLogin.Commsionary_Code,
											UserLogin.Created_date, 
                                            UserLogin.PwdUpdatedDate, UserLogin.Islock, UserLogin.IMEI, isnull(UserLogin.IsPassChange,'N') as IsChangePassword
                                            FROM    UserLogin LEFT OUTER JOIN
                                               mst_Commissionary_Districts ON UserLogin.District_Code = mst_Commissionary_Districts.DISTRICTCODE
                            LEFT OUTER JOIN  ( select RangeID from Range_Master group by RangeName,RangeID)Range_Master ON UserLogin.RangeCode = Range_Master.RangeID 
                             LEFT OUTER JOIN  mst_Role ON UserLogin.Userrole = mst_Role.Role 
                             left outer join mst_Panchayats on mst_Panchayats.BlockCode=UserLogin.Block_Code  
							 
         where UserLogin.UserID=@UserID", new SqlParameter[] { new SqlParameter("@UserID", UserName) });
        if (dtRole.Rows.Count > 0)
        {

            if (dtRole.Rows.Count > 0 && dtRole.Rows[0]["Password"] != null && dtRole.Rows[0]["Password"] != DBNull.Value && Encoder.HtmlEncode(dtRole.Rows[0]["Password"].ToString()) == enc.Encrypt(PWD))
            {

                FormsAuthentication.Initialize();
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UserName, DateTime.Now, DateTime.Now.AddMinutes(30), false, Encoder.HtmlEncode(dtRole.Rows[0]["Userrole"].ToString()), FormsAuthentication.FormsCookiePath);

                // Encrypt the cookie using the machine key for secure transport
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash); // Hashed ticket

                cookie.HttpOnly = true;

                // Set the cookie's expiration time to the tickets expiration time
                if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                // Add the cookie to the list for outgoing response
                Response.Cookies.Add(cookie);
                //Response.Cookies["cookie"].Expires = DateTime.Now.AddHours(4);
                Session["UserID"] = Encoder.HtmlEncode(dtRole.Rows[0]["UserID"].ToString());
                Session["Username"] = Encoder.HtmlEncode(dtRole.Rows[0]["UserName"].ToString());
                Session["Role"] = Encoder.HtmlEncode(dtRole.Rows[0]["Userrole"].ToString());
                Session["UserId"] = Encoder.HtmlEncode(dtRole.Rows[0]["UserID"].ToString());

                Session["Commsionary_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["Commsionary_Code"].ToString());
                Session["District_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["District_Code"].ToString());
                Session["Block_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["Block_Code"].ToString());
                
                Session["Sub_DivCode"] = Encoder.HtmlEncode(dtRole.Rows[0]["Sub_DivCode"].ToString());
                Session["Panchayat_code"]= Encoder.HtmlEncode(dtRole.Rows[0]["PanchayatCode"].ToString());
                Session["Thana_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["Thana_Code"].ToString());

                Session["Thana_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["Thana_Code"].ToString());
                Session["IsStrong"] = Encoder.HtmlEncode(dtRole.Rows[0]["IsStrong"].ToString());


                Session["RangeCode"] = Encoder.HtmlEncode(dtRole.Rows[0]["RangeCode"].ToString());


                string ipaddress = clsData.GetIpValue();
                Login(Session["UserID"].ToString(), Session["Username"].ToString(), Session["Role"].ToString(), ipaddress,"S");
               
                // Redirect to requested URL, or homepage if no previous page
                // requested
                //if(Session["IsStrong"].ToString()=="N")
                //{
                //    Response.Redirect("ChangePassword.aspx");
                //}
                //else
                //{
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    switch (Encoder.HtmlEncode(dtRole.Rows[0]["Userrole"].ToString().Trim()))
                    {


                        case "COOPT":

                            /*
                             returnUrl = "LandDispute/BLKADM/Default.aspx";
                             Response.Redirect(returnUrl);                        
                             break;
                             */

                            if (dtRole.Rows[0]["IsChangePassword"].ToString() == "N")
                            {
                                Response.Redirect("ChangePass.aspx");
                            }
                            else
                            {
                                returnUrl = "Default.aspx";
                                Response.Redirect(returnUrl);
                            }
                            break;

                        case "SHOOPT":
                            if (dtRole.Rows[0]["IsChangePassword"].ToString() == "N")
                            {
                                Response.Redirect("ChangePass.aspx");
                            }
                            else
                            {
                                returnUrl = "Default.aspx";
                                Response.Redirect(returnUrl);
                            }
                            break;

                        case "DPGRO":
                        case "DMOPT":
                        case "DSTNIC1":
                        case "SSPOPT":
                        case "ADM":

                            returnUrl = "Default.aspx";
                            Response.Redirect(returnUrl);
                            break;

                        case "HQ":
                        case "ADMHOME":
                        case "ADMLR":
                    case "LAWORDER":
                            returnUrl = "Default.aspx";
                            Response.Redirect(returnUrl);
                            break;

                        case "COM":
                        case "DIG":
                        returnUrl = "Default.aspx";
                            Response.Redirect(returnUrl);
                            break;

                        case "DSPOPT":
                        case "SDPGRO":
                        case "SDOOPT":
                            returnUrl = "Default.aspx";
                            Response.Redirect(returnUrl);
                            break;
                    }
                //}
              
            }
            else
            {

                string ipaddress = clsData.GetIpValue();
                // InsertUserLog(Convert.ToString(Session["UserId"]), Convert.ToString(Session["Role"]), "F", ipaddress, DateTime.Now.ToString("dd-MM-yyyy,hh:mm:ss tt"), "Login Failed");
                 lblmsgerror.Text = "Invalid UserID / Password";
            txtUserName.Text= "";
            txtCaptha.Text = "";
            txtPassword.Text = "";
            txtUserName.Focus();
            }
        }
        else
        {

            string ipaddress = clsData.GetIpValue();
            //InsertUserLog(Convert.ToString(Session["UserId"]), Convert.ToString(Session["Role"]), "I", ipaddress, DateTime.Now.ToString("dd-MM-yyyy,hh:mm:ss tt"), "Invalid Password");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Invalid UserID/Password')", true);
            //MsgUtility.showMessage(this, "Invalid UserID/Password");
            lblmsgerror.Text = "Invalid UserID / Password";
            txtUserName.Text= "";
            txtCaptha.Text = "";
            txtPassword.Text = "";
            txtUserName.Focus();
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //ccJoin.ValidateCaptcha(txtCaptha.Text.Trim());
        //if (txtCaptha.Text.Trim() != Convert.ToString(Session["CaptchaCode"]))
        //{
        //    MsgUtility.showMessage(this, "Enter Text Again Shown in Code!");
        //    txtCaptha.Focus();
        //    txtUserName.Text = "";
        //    txtCaptha.Text = "";
        //}
        login();
        //JanParichay_Login("atul74559@janparichay.gov.in");
        //JanParichay_Login("priya.kumari602@janparichay.gov.in");
    }
    private void InsertUserLog(string _UserId, string _Role, string _IsLogIn, string _IpAddress, string _EntryDate, string _Remarks)
    {
        SqlParameter UserId = new SqlParameter("@UserId", _UserId);
        SqlParameter Role = new SqlParameter("@Role", _Role);
        SqlParameter LoginAttempt = new SqlParameter("@LoginAttempt", _IsLogIn);
        SqlParameter IpAddress = new SqlParameter("@IpAddress", _IpAddress);
        SqlParameter EntryDate = new SqlParameter("@EntryDate", DateTime.Now.ToString("dd-MM-yyyy,hh:mm:ss tt"));
        SqlParameter Remarks = new SqlParameter("@Remarks", _Remarks);
        DataTable dt = clsData.GetDataTableWithProc("insert_UserLog", new SqlParameter[] { UserId, Role, LoginAttempt, IpAddress, EntryDate, Remarks });
        if (dt.Rows.Count > 0)
        {
        }
        else
        {
            //lblmsg.Text = "Please Try Again!";
        }
    }

    private void Login(string _UserID, string _Username, string _Role, string _IpAddress ,string _Status)
    {
        SqlParameter UserId = new SqlParameter("@UserId", _UserID);
        SqlParameter Username = new SqlParameter("@Username", _Username);
        SqlParameter Role = new SqlParameter("@Role", _Role);
        SqlParameter IpAddress = new SqlParameter("@IpAddress", _IpAddress);
        SqlParameter Status = new SqlParameter("@Status", _Status);
        DataTable dt = clsData.GetDataTableWithProc("insert_UserLog", new SqlParameter[] { UserId, Username, Role, IpAddress, Status });
        if (dt.Rows.Count > 0)
        {
        }
        else
        {
            //lblmsg.Text = "Please Try Again!";
        }
    }


    [System.Web.Services.WebMethod()]
    public static void captcha()
    {
        //var page = HttpContext.Current.Handler as Page;
        //if (page != null)
        //{
        //    var imgCaptcha = page.FindControl("imgCaptcha")as Image;
        //    imgCaptcha.ImageUrl = "CreateCaptcha.aspx";
        //}
       

    }

	protected void rblogintype_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (rblogintype.SelectedValue == "0")
		{
			dvpublic.Visible = false;
			dvdept.Visible = true;
		}
		else
		{
			dvpublic.Visible = true;
			dvdept.Visible = false;
		}
	}




#region janparichay Login code         
       private void CallMeriPehchaan()
       {
           MeriPahchan.MeriPehchaanSSO_Execute_Login meriPehchaanSSO_Execute_Login = new MeriPahchan.MeriPehchaanSSO_Execute_Login();
           MeriPahchan.HMAC_Request hmac = new MeriPahchan.HMAC_Request();
           string _hmac_string = meriPehchaanSSO_Execute_Login.Generate_HMAC_String();//Generate String for HMAC        
           MeriPahchan.CommonResponse hmac_response = new MeriPahchan.CommonResponse();//Creating object for HMAC Call

           hmac_response = meriPehchaanSSO_Execute_Login.Call_HMAC_API(_hmac_string);//Call HMAC API
           if (!String.IsNullOrEmpty(hmac_response.status) && hmac_response.status == "success") //Checking HMAC API RESPONSE
           {
               MeriPahchan.CommonResponse aes_response = new MeriPahchan.CommonResponse(); //Creating object for AES Call
               MeriPahchan.AES_Encryption_Request aes_Encryption_Request = new MeriPahchan.AES_Encryption_Request(); //Creating object for request
               aes_Encryption_Request.AESString = _hmac_string;
               aes_response = meriPehchaanSSO_Execute_Login.Call_AESString(aes_Encryption_Request); //Call HMAC API

               if (aes_response != null && aes_response.status == "success")
               {
                   //Creating url for login in janparichay portal
                   string _jan_parichay_url = APP_CONFIGURATION.JAN_PARICHAY_URL + @"v1/api/login?sid=" + APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID + @"&tid=" + APP_CONFIGURATION.CLIENT_TID + @"&cs=" + Convert.ToString(hmac_response.data.signature) + "&string=" + Convert.ToString(aes_response.data.signature);
                   #region API Request Save

                   Guid _guid = Guid.NewGuid();
                   string _guidString = _guid.ToString();
                  
                   #endregion
                   Response.Redirect(_jan_parichay_url);
               }
           }
       }
       private MeriPahchan.ExecutionResponse CheckMeriPehchaanId(string _registrationId)
       {
           DataTable dt = new DataTable();
           MeriPahchan.ExecutionResponse _executionResponse = new MeriPahchan.ExecutionResponse();
           SqlConnection con;
           string ConnectionString = ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString;
           con = new SqlConnection(ConnectionString);

           using (SqlCommand cmd = new SqlCommand(@"DECLARE @status as varchar(3) = '500'
                                               IF EXISTS(SELECT  * FROM UserLogin where LTRIM(RTRIM(LOWER(JanparichayId))) = LTRIM(RTRIM(LOWER(@JanParichay_Meri_Pehchaan_ID))))
                                               BEGIN
                                                SET @status = '200';
                                               END
											ELSE IF EXISTS(SELECT  * FROM UserLogin where LTRIM(RTRIM(UserID)) = LTRIM(RTRIM(LOWER(@JanParichay_Meri_Pehchaan_ID))))
                                               BEGIN
                                                SET @status = '200';
                                               END
                                               ELSE
                                               BEGIN
                                                SET @status = '500';
                                               END
                                               SELECT @status AS [Status]"))
           {
               using (SqlDataAdapter sda = new SqlDataAdapter())
               {
                   cmd.Connection = con;
                   cmd.Parameters.AddWithValue("@JanParichay_Meri_Pehchaan_ID", _registrationId);
                   cmd.CommandTimeout = 0;
                   sda.SelectCommand = cmd;
                   sda.SelectCommand.CommandTimeout = 0;
                   sda.Fill(dt);
                   if (dt != null && dt.Rows.Count > 0)
                   {
                       _executionResponse.Status = Convert.ToString(dt.Rows[0]["Status"]);
                   }
               }
           }
           return _executionResponse;
       }
       private void UpdateMeriPehchaanId(string _loginId, string _janparichayId)
       {
           SqlConnection con;
           string ConnectionString = ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString;
           con = new SqlConnection(ConnectionString);

           using (SqlCommand cmd = new SqlCommand(@"USP_Update_JanparichayId"))
           {
               cmd.Connection = con;
               cmd.Parameters.AddWithValue("@janparichayId", _janparichayId);
               cmd.Parameters.AddWithValue("@loginId", _loginId);
               cmd.CommandTimeout = 0;
               cmd.CommandType = CommandType.StoredProcedure;
               con.Open();
               cmd.ExecuteNonQuery();
               con.Close();
           }

       }    
       protected void btnpubliclogin_Click(object sender, EventArgs e)
       {
           
           CallOnLoad();
        
       }


     

       private void CallOnPageLoad()
       {
           string _current_page_url = HttpContext.Current.Request.Url.AbsoluteUri;
           if (Request.QueryString["string"] != null)
           {
               string someText = _current_page_url;

               #region API Request Save
               Guid _guid = Guid.NewGuid();
               string _guidString = _guid.ToString();
         
               #endregion
               //File.AppendAllText(@"E:\RCMSLogFile\handshakingurl.txt", DateTime.Now.ToString() + Environment.NewLine + someText + Environment.NewLine);

               string _server_handshakingId = Convert.ToString(Request.QueryString["string"]);
               MeriPahchan.MeriPehchaanSSO_Execute_Login meriPehchaanSSO_Execute_Login = new MeriPahchan.MeriPehchaanSSO_Execute_Login();
               MeriPahchan.HandShakeRequest handShakeRequest = new MeriPahchan.HandShakeRequest();
               handShakeRequest.handshakingId = _server_handshakingId;
               handShakeRequest.sid = APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID;

               string handshakeUrl = APP_CONFIGURATION.CLIENT_URL_PROTOCOL
                                                            + APP_CONFIGURATION.CLIENT_URL
                                                            + ":" + APP_CONFIGURATION.CLIENT_URL_PORT + @"/handshake?handshakingId=" + _server_handshakingId + "&sid=" + APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID;


               MeriPahchan.APIResponse apiResponse1 = meriPehchaanSSO_Execute_Login.Call_HandShake(handshakeUrl);


               MeriPahchan.Decryption_Request decryption_Request = new MeriPahchan.Decryption_Request();
               decryption_Request.EncryptedString = apiResponse1.Message;


               string d_url = APP_CONFIGURATION.CLIENT_URL_PROTOCOL
                                                            + APP_CONFIGURATION.CLIENT_URL
                                                            + ":" + APP_CONFIGURATION.CLIENT_URL_PORT
                                                            + "/decryption";
               //File.AppendAllText(@"E:\RCMSLogFile\d_url.txt", DateTime.Now.ToString() + Environment.NewLine + d_url + Environment.NewLine);

               //Calling api to decryption
               MeriPahchan.Decryption_Response _decryption_Response = meriPehchaanSSO_Execute_Login.Call_DecryptionString(decryption_Request);
               //Storing decryption value in browser cookies
               if (_decryption_Response != null
                   && !String.IsNullOrEmpty(_decryption_Response.status)
                   && _decryption_Response.status == "success")
               {
                   _decryption_Response.data.signature.ua = (String.IsNullOrEmpty(_decryption_Response.data.signature.ua) ? "" : _decryption_Response.data.signature.ua.Replace(";", "||"));

                   HttpCookie _meriPehchaanCookies = new HttpCookie("MeriPehchaanCookies");
                   _meriPehchaanCookies["MeriPehchaanCookies_Decryption"] = Newtonsoft.Json.JsonConvert.SerializeObject(_decryption_Response);
                   _meriPehchaanCookies.Expires.Add(new TimeSpan(24, 0, 0));
                   HttpContext.Current.Response.Cookies.Add(_meriPehchaanCookies);

                   MeriPahchan.Validate_Token validate_Token = new MeriPahchan.Validate_Token();
                   validate_Token.GetCookiesValue();

                   string _meri_pehchaan_Id = string.Empty;
                   if (!String.IsNullOrEmpty(_decryption_Response.data.signature.verificationIds.rcmsonlinedata))
                   {
                       _meri_pehchaan_Id = _decryption_Response.data.signature.verificationIds.rcmsonlinedata;
                      // UpdateMeriPehchaanId(_meri_pehchaan_Id, _decryption_Response.data.signature.verificationIds.userid);
                   }
                   else if (!String.IsNullOrEmpty(_decryption_Response.data.signature.verificationIds.userid))
                   {
                       _meri_pehchaan_Id = _decryption_Response.data.signature.verificationIds.userid;
                   }

                   MeriPahchan.ExecutionResponse _meri_pehchaan_existance_response = CheckMeriPehchaanId(_meri_pehchaan_Id);

                   if (_meri_pehchaan_existance_response.Status == "200")
                   {
                       //File.WriteAllText(@"E:\RCMS_Applications\RCMSLogFile\meri_login_status.txt", _meri_pehchaan_existance_response.Status);
                       JanParichay_Login(_decryption_Response.data.signature.userId);
                   }
                   else if (_meri_pehchaan_existance_response.Status == "500")
                   {
                       string script = "alert('You are not registered with BHU-SAMADAHAN Portal, Please Register and Proceed.');";

                       System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", script, true);
                       string _securetoken;
                       string hdnRegSecurity;
                       _securetoken = MeriPahchan.LibrarayService.GenerateRandomNumber(5);
                       Context.Session["_securetoken"] = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((_securetoken), "MD5").ToLower().ToString();
                       hdnRegSecurity = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile((_securetoken), "MD5").ToLower().ToString();
                       ScriptManager.RegisterStartupScript(this, this.GetType(), "Information", "alert('You are not registered with BHU-SAMADAHAN Portal, Please Register and Proceed.!'); location.href = 'MeriPehchan_Registration.aspx';", true);

                       //Response.Redirect("~/MeriPehchan_Registration.aspx");
                   }
               }



           }
       }




       public void CallOnLoad()
       {
           string _current_page_url = HttpContext.Current.Request.Url.AbsoluteUri;
           if (Request.QueryString["string"] != null)
           {
               string someText = _current_page_url;

               #region API Request Save
               Guid _guid = Guid.NewGuid();
               string _guidString = _guid.ToString();
              
               #endregion
            

               string _server_handshakingId = Convert.ToString(Request.QueryString["string"]);
               MeriPahchan.MeriPehchaanSSO_Execute_Login meriPehchaanSSO_Execute_Login = new MeriPahchan.MeriPehchaanSSO_Execute_Login();
               MeriPahchan.HandShakeRequest handShakeRequest = new MeriPahchan.HandShakeRequest();
               handShakeRequest.handshakingId = _server_handshakingId;
               handShakeRequest.sid = APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID;

               string handshakeUrl = APP_CONFIGURATION.CLIENT_URL_PROTOCOL
                                                            + APP_CONFIGURATION.CLIENT_URL
                                                            + ":" + APP_CONFIGURATION.CLIENT_URL_PORT + @"/handshake?handshakingId=" + _server_handshakingId + "&sid=" + APP_CONFIGURATION.JAN_PARICHAY_SERVICE_ID;


               MeriPahchan.APIResponse apiResponse1 = meriPehchaanSSO_Execute_Login.Call_HandShake(handshakeUrl);


               MeriPahchan.Decryption_Request decryption_Request = new MeriPahchan.Decryption_Request();
               decryption_Request.EncryptedString = apiResponse1.Message;


               string d_url = APP_CONFIGURATION.CLIENT_URL_PROTOCOL
                                                            + APP_CONFIGURATION.CLIENT_URL
                                                            + ":" + APP_CONFIGURATION.CLIENT_URL_PORT
                                                            + "/decryption";
               //File.AppendAllText(@"E:\RCMSLogFile\d_url.txt", DateTime.Now.ToString() + Environment.NewLine + d_url + Environment.NewLine);

               //Calling api to decryption
               MeriPahchan.Decryption_Response _decryption_Response = meriPehchaanSSO_Execute_Login.Call_DecryptionString(decryption_Request);
               //Storing decryption value in browser cookies
               if (_decryption_Response != null
                   && !String.IsNullOrEmpty(_decryption_Response.status)
                   && _decryption_Response.status == "success")
               {
                   _decryption_Response.data.signature.ua = (String.IsNullOrEmpty(_decryption_Response.data.signature.ua) ? "" : _decryption_Response.data.signature.ua.Replace(";", "||"));

                   HttpCookie _meriPehchaanCookies = new HttpCookie("MeriPehchaanCookies");
                   _meriPehchaanCookies["MeriPehchaanCookies_Decryption"] = Newtonsoft.Json.JsonConvert.SerializeObject(_decryption_Response);
                   _meriPehchaanCookies.Expires.Add(new TimeSpan(24, 0, 0));
                   HttpContext.Current.Response.Cookies.Add(_meriPehchaanCookies);

                   MeriPahchan.Validate_Token validate_Token = new MeriPahchan.Validate_Token();
                   validate_Token.GetCookiesValue();

                   string _meri_pehchaan_Id = string.Empty;
                   if (!String.IsNullOrEmpty(_decryption_Response.data.signature.verificationIds.rcmsonlinedata))
                   {
                       _meri_pehchaan_Id = _decryption_Response.data.signature.verificationIds.rcmsonlinedata;
                      // UpdateMeriPehchaanId(_meri_pehchaan_Id, _decryption_Response.data.signature.verificationIds.userid);
                   }
                   else if (!String.IsNullOrEmpty(_decryption_Response.data.signature.verificationIds.userid))
                   {
                       _meri_pehchaan_Id = _decryption_Response.data.signature.verificationIds.userid;
                   }


				if (String.IsNullOrEmpty(_meri_pehchaan_Id) == false)
				{
					//set all session Redirect To Landing Page
					Session["UserId"] = "Test";
					Response.Redirect("Default.aspx");
				}
			}

           }
           else
           {
               CallMeriPehchaan();
           }
       }





    private void JanParichay_Login(string _registrationId)
    {

        DataTable dtRole = clsData.GetDataTable(@"SELECT isnull(UserLogin.IsStrong,'N')as 'IsStrong',UserLogin.Userrole,UserLogin.Name, UserLogin.UserID, mst_Role.RoleDesc, UserLogin.Userrole AS UserRoleID, UserLogin.UserName,UserLogin.RangeCode,
                                                    UserLogin.Password, UserLogin.District_Code, UserLogin.Block_Code,mst_Panchayats.PanchayatCode,
                                            UserLogin.Sub_DivCode,
											UserLogin.Thana_Code,
											 UserLogin.Commsionary_Code,
											UserLogin.Created_date, 
                                            UserLogin.PwdUpdatedDate, UserLogin.Islock, UserLogin.IMEI, isnull(UserLogin.IsPassChange,'N') as IsChangePassword
                                            FROM    UserLogin LEFT OUTER JOIN
                                               mst_Commissionary_Districts ON UserLogin.District_Code = mst_Commissionary_Districts.DISTRICTCODE
                            LEFT OUTER JOIN  ( select RangeID from Range_Master group by RangeName,RangeID)Range_Master ON UserLogin.RangeCode = Range_Master.RangeID 
                             LEFT OUTER JOIN  mst_Role ON UserLogin.Userrole = mst_Role.Role 
                             left outer join mst_Panchayats on mst_Panchayats.BlockCode=UserLogin.Block_Code  
							 
         where UserLogin.JanparichayId=@JanparichayId", new SqlParameter[] { new SqlParameter("@JanparichayId", _registrationId) });
        if (dtRole.Rows.Count > 0)
        {

			FormsAuthentication.Initialize();
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, dtRole.Rows[0]["UserName"].ToString(), DateTime.Now, DateTime.Now.AddMinutes(30), false, Encoder.HtmlEncode(dtRole.Rows[0]["Userrole"].ToString()), FormsAuthentication.FormsCookiePath);

			// Encrypt the cookie using the machine key for secure transport
			string hash = FormsAuthentication.Encrypt(ticket);
			HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash); // Hashed ticket

			cookie.HttpOnly = true;

			// Set the cookie's expiration time to the tickets expiration time
			if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

			// Add the cookie to the list for outgoing response
			Response.Cookies.Add(cookie);
			//Response.Cookies["cookie"].Expires = DateTime.Now.AddHours(4);
			Session["UserID"] = Encoder.HtmlEncode(dtRole.Rows[0]["UserID"].ToString());


			Session["Username"] = Encoder.HtmlEncode(dtRole.Rows[0]["Userrole"].ToString())!= "PUBLIC" ? Encoder.HtmlEncode(dtRole.Rows[0]["UserName"].ToString()): Encoder.HtmlEncode(dtRole.Rows[0]["Name"].ToString());


			Session["Role"] = Encoder.HtmlEncode(dtRole.Rows[0]["Userrole"].ToString());
			Session["UserId"] = Encoder.HtmlEncode(dtRole.Rows[0]["UserID"].ToString());

			Session["Commsionary_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["Commsionary_Code"].ToString());
			Session["District_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["District_Code"].ToString());
			Session["Block_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["Block_Code"].ToString());

			Session["Sub_DivCode"] = Encoder.HtmlEncode(dtRole.Rows[0]["Sub_DivCode"].ToString());
			Session["Panchayat_code"] = Encoder.HtmlEncode(dtRole.Rows[0]["PanchayatCode"].ToString());
			Session["Thana_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["Thana_Code"].ToString());

			Session["Thana_Code"] = Encoder.HtmlEncode(dtRole.Rows[0]["Thana_Code"].ToString());
			Session["IsStrong"] = Encoder.HtmlEncode(dtRole.Rows[0]["IsStrong"].ToString());


			Session["RangeCode"] = Encoder.HtmlEncode(dtRole.Rows[0]["RangeCode"].ToString());


			string ipaddress = clsData.GetIpValue();
			Login(Session["UserID"].ToString(), Session["Username"].ToString(), Session["Role"].ToString(), ipaddress, "S");
			string returnUrl = "DefaultPublic.aspx";
            Response.Redirect(returnUrl);
        }
        else
        {

            string returnUrl = "https://homeonline.bihar.gov.in/landdispute/Login_Default_new.aspx";
            Response.Redirect(returnUrl);
        }

    }



#endregion









}