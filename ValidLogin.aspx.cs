using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.SessionState;
using EO.Web.Internal;
using System.Linq;
using System.Text;
using Microsoft.Security.Application;
using Org.BouncyCastle.Ocsp;

public partial class ValidLogin : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Uri refer = Request.UrlReferrer;
            if (refer == null)
            {
                goBack("5.3");
            }

            if (Request.Url.Host != refer.Host)
            {
                goBack("5.4");
            }

            string t = HttpUtility.UrlDecode(Request.Url.ToString());
            t = Request.Url.ToString();

            //  return HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["url"]);
            string t1 = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["1"]);
            Uri uu = new Uri(t);

            t = uu.Query;
            t = t.Remove(0, 1);

            t = Aes256CbcEncrypterApp.Decrypt(t, "85934784000243428593478400024342");

            var queryDictionary = System.Web.HttpUtility.ParseQueryString(t);
            Session["hidkey"] = queryDictionary["4"];

            if (String.IsNullOrEmpty(queryDictionary["1"]))
            {
                goBack("1");
            }
            if (String.IsNullOrEmpty(queryDictionary["2"]))
            {
                goBack("2");
            }
            if (String.IsNullOrEmpty(queryDictionary["3"]))
            {
                goBack("3");
            }
            if (String.IsNullOrEmpty(queryDictionary["4"]))
            {
                goBack("3");
            }

            if (!checktime(queryDictionary["1"].ToString(), queryDictionary["2"].ToString()))
            {
                if (queryDictionary["5"] != null)
                {
                    Session["FailedAttempt"] = queryDictionary["5"].ToString();
                }
                Response.Redirect("Login_Default.aspx?msg=T", false);
                return;
            }

            if (!validTimeToken(queryDictionary["3"].ToString()))
            {
                goBack("4");
            }
           
            if (!login(queryDictionary["1"].ToString(), queryDictionary["2"].ToString()))
            {
                //Session["FailedAttempt"] = queryDictionary["5"].ToString();
                Response.Redirect("Login_default.aspx?msg=W", false);
            }
        }
        catch (Exception ee)
        {
        }

        if (Page.IsPostBack)
        {

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = "";
            }
            if (Request.Cookies["EservicesAppNameAuth"] != null)
            {
                Response.Cookies["EservicesAppNameAuth"].Path = Request.ApplicationPath;
                Response.Cookies["EservicesAppNameAuth"].Value = "";
            }
            Session.Clear();

            Session.RemoveAll();
            Session.Abandon();
            goBack("9");

        }
    }
    public bool checktime(string i, string p)
    {
        bool result = true;
        string key = Encryptor.PrivateKey.ToString();
        Encryptor enc = new Encryptor(key);
        String PWD = enc.Encrypt(p);
        string UserName = i;

        SqlParameter UserId = new SqlParameter("@UserId", UserName);
        string strQuery = @"Select LockoutTime from UserOTPBlocked where UserID=@userid";
        DataTable dt = clsData.GetDataTable(strQuery, new SqlParameter[] { UserId });
        if (dt.Rows.Count > 0)
        {
            DateTime lockoutTime;
            string lockdatetime = dt.Rows[0]["LockoutTime"].ToString();
            if (DateTime.TryParse(lockdatetime, out lockoutTime))
            {

                DateTime currentTime = DateTime.Now;
                TimeSpan timeDifference = lockoutTime.AddMinutes(5) - currentTime;

                if (timeDifference.TotalMinutes > 0)
                {
                    Console.WriteLine("You will be able to log in after {Math.Ceiling(timeDifference.TotalMinutes)} minute(s).");
                    result = false;
                }
            }
        }
        return result;
    }
    public void goBack(string msg)
    {
        try
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["EservicesAppNameAuth"] != null)
            {
                Response.Cookies["EservicesAppNameAuth"].Path = Request.ApplicationPath;
                Response.Cookies["EservicesAppNameAuth"].Value = "";
            }

            if ((msg == "W") || (msg == "4") || (msg == "9"))

                Response.Redirect("Default.aspx?msg=" + msg, true);
            else
                Response.Redirect("Default.aspx", true);
        }
        catch (Exception ex) { }
    }

    public bool validTimeToken(string t)
    {
        try
        {
            if ((Server.MachineName.ToString()) != (t.Remove(0, 14)))
                return false;
            else
                t = t.Substring(0, 14);
        }
        catch
        {
            return false;
        }

        DateTime dt;
        try
        {
            dt = DateTime.ParseExact(t, "ddMMyyyyHHmmss", null);
 
        }
        catch
        {
            return false;
        }

        TimeSpan duration = System.DateTime.Now - dt;
        TimeSpan test = TimeSpan.FromSeconds(90);
        TimeSpan test1 = TimeSpan.FromSeconds(0);
        //  if ((0 <= duration.TotalSeconds) && (duration.TotalSeconds <= 3))
        if ((duration <= TimeSpan.FromSeconds(90)) && (duration >= TimeSpan.FromSeconds(0)))
            return true;
        else
            return false;

    }

    public static bool validateHashKey(string input, string hash)
    {
        string tempHash = computeHashKey(input);
        return (string.Compare(tempHash, hash) == 0);

    }

    public static string computeHashKey(string InputValue)
    {
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
        byte[] byteInputValue = System.Text.Encoding.ASCII.GetBytes(InputValue);

        using (SHA256 md5 = SHA256.Create())
        {
            byte[] computeHash = md5.ComputeHash(byteInputValue);
            for (int i = 0; i < computeHash.Length; i++)
            {
                stringBuilder.Append(computeHash[i].ToString("x2"));
            }
        }

        return stringBuilder.ToString();
    }

    public bool login(string i, string p)
    {
        int res = 0;
        try
        {         
            string PWD = p;
            string UserName = i;

            if (ValidateInput.CheckSqlInjection(UserName) == false || ValidateInput.CheckSqlInjection(PWD) == false)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Info", "alert('Invalid Character : Username and Password should not contain characters ' and ; ');", true);
                return false;
            }

            string key = Encryptor.PrivateKey.ToString();
            Encryptor enc = new Encryptor(key);
     
            checktime(i, p);
            DataTable dtRole = clsData.GetDataTable(@"SELECT isnull(UserLogin.IsStrong,'N')as 'IsStrong',UserLogin.Userrole, UserLogin.UserID, mst_Role.RoleDesc, UserLogin.Userrole AS UserRoleID, UserLogin.UserName,UserLogin.RangeCode,
                                                    UserLogin.Password, UserLogin.District_Code, UserLogin.Block_Code,
                                            UserLogin.Sub_DivCode,
											UserLogin.Thana_Code,
											 UserLogin.Commsionary_Code,
											UserLogin.Created_date, 
                                            UserLogin.PwdUpdatedDate, UserLogin.Islock, UserLogin.IMEI, isnull(UserLogin.IsPassChange,'N') as IsChangePassword
                                            FROM    UserLogin LEFT OUTER JOIN
                                               mst_Commissionary_Districts ON UserLogin.District_Code = mst_Commissionary_Districts.DISTRICTCODE
                            LEFT OUTER JOIN  ( select RangeID from Range_Master group by RangeName,RangeID)Range_Master ON UserLogin.RangeCode = Range_Master.RangeID 
                             LEFT OUTER JOIN  mst_Role ON UserLogin.Userrole = mst_Role.Role 
                             
            where UserLogin.UserID=@UserID ", new SqlParameter[] { new SqlParameter("@UserID", UserName) });

            if (dtRole.Rows.Count > 0)
            {
               if (dtRole.Rows.Count > 0 && dtRole.Rows[0]["Password"] != null && dtRole.Rows[0]["Password"] != DBNull.Value && dtRole.Rows[0]["Password"].ToString() == enc.Encrypt(PWD.Trim()))
                {

                    FormsAuthentication.Initialize();
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, UserName, DateTime.Now, DateTime.Now.AddMinutes(30), false, dtRole.Rows[0]["Userrole"].ToString(), FormsAuthentication.FormsCookiePath);

                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash); 

                    cookie.HttpOnly = true;

                    if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;

                    Response.Cookies.Add(cookie);
                    Session["UserID"] = dtRole.Rows[0]["UserID"].ToString();
                    Session["Username"] = dtRole.Rows[0]["UserName"].ToString();
                    Session["Role"] = dtRole.Rows[0]["Userrole"].ToString();
                    
                    Session["Commsionary_Code"] = dtRole.Rows[0]["Commsionary_Code"].ToString();
                    Session["District_Code"] =dtRole.Rows[0]["District_Code"].ToString();
                    Session["Block_Code"] = dtRole.Rows[0]["Block_Code"].ToString();

                    Session["Sub_DivCode"] = dtRole.Rows[0]["Sub_DivCode"].ToString();
                   // Session["Panchayat_code"] = dtRole.Rows[0]["PanchayatCode"].ToString();
                    Session["Thana_Code"] = dtRole.Rows[0]["Thana_Code"].ToString();

                    
                    Session["IsStrong"] = dtRole.Rows[0]["IsStrong"].ToString();
                    Session["RangeCode"] = dtRole.Rows[0]["RangeCode"].ToString();
                    Session["loginToken"] = RandomString(11);
                    string updatelogintoken = "update UserLogin set logintoken=@logintoken where UserID=@username";
                    SqlParameter username = new SqlParameter("@username", Session["UserID"].ToString());
                    SqlParameter logintoken = new SqlParameter("@logintoken", Session["loginToken"].ToString());

                    res = clsData.ExecuteSql(updatelogintoken, new SqlParameter[] { username, logintoken });

                    string ipaddress = clsData.GetIpValue();
                    Login(Session["UserID"].ToString(), Session["Username"].ToString(), Session["Role"].ToString(), ipaddress, "S");
              
                        string returnUrl = Request.QueryString["ReturnUrl"];
                        switch (dtRole.Rows[0]["Userrole"].ToString().Trim())
                        {
                            case "COOPT":
                               
                                if (dtRole.Rows[0]["IsChangePassword"].ToString() == "N")
                                {
                                    Response.Redirect("ChangePass.aspx");
                                }
                                else
                                {
                                    returnUrl = "Default.aspx";
                                    Response.Redirect(returnUrl, false);
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
                                    Response.Redirect(returnUrl, false);
                                }
                                break;

                            case "DPGRO":
                            case "DMOPT":
                            case "SSPOPT":
                            case "ADM":
			    case "DSTNIC1":

                                returnUrl = "Default.aspx";
                                Response.Redirect(returnUrl, false);
                                break;

                            case "HQ":
                            case "ADMHOME":
                            case "ADMLR":
                            case "LAWORDER":
                            returnUrl = "Default.aspx";
                                Response.Redirect(returnUrl, false);
                                break;

                            case "COM":
                        case "DIG":
                            returnUrl = "Default.aspx";
                                Response.Redirect(returnUrl, false);
                                break;

                            case "DSPOPT":
                            case "SDPGRO":
                            case "SDOOPT":
                                returnUrl = "Default.aspx";
                                Response.Redirect(returnUrl, false);
                                break;
                        }                 
                }
                else
                {
                    string ipaddress = clsData.GetIpValue();
                    return false;
                }
                return true;
            }
            else
            {
                Utility.showMessage(this, "Please enter Correct UserID and Password!");
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private void Login(string _UserID, string _Username, string _Role, string _IpAddress, string _Status)
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
    private string GetReturnUrlByRole(string roleId, DataRow userRow)
    {
        if (roleId == "HQ" || roleId == "DST" || roleId == "FIRE" || roleId == "PS" || roleId == "PRIS")
            return "~/GD/ADM/Default.aspx";
        else if (roleId == "BLKADM")
            return "~/GD/BLKADM/Default.aspx";
        else if (roleId == "EE" && userRow["IsChangePassword"].ToString() == "N")
            return "~/GD/ChangePass.aspx";
        else if (roleId == "EE")
            return "~/GD/EEADM/Default.aspx";

        return "~/Login.aspx"; // Default to login if role does not match
    }

    private static Random Random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}