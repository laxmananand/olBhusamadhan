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
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using System.Text.RegularExpressions;
using esms_client;
using System.Reflection;
public partial class LandDispute_ForgotPassword : System.Web.UI.Page
{

    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    string key = Encryptor.PrivateKey.ToString();

    Encryptor enc = new Encryptor(Encryptor.PrivateKey);
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
       
        if (!IsPostBack)
        {
            if (Session["hidkey"] == null)
                setHidKey();
            reloadcaptcha.ImageUrl = "~/images/refresh.png";
            Session["SendMobileOTP_1"] = "0";
            Session["otp"] = string.Empty;
            Panel2.Visible = false;
            Panel1.Visible = true;
            Page.Validate();
            setHidKey();
        }

    }

    public void setHidKey()
    {
        Random rand = new Random();
        long randnum2 = (long)(rand.NextDouble() * 900000000000000) + 8000000000000000;
        Session["hidkey"] = randnum2.ToString();
       // HidKey3.Value = Session["hidkey"].ToString();
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUserID.Text) && string.IsNullOrWhiteSpace(txtMobile.Text) && string.IsNullOrWhiteSpace(txtCaptha.Text))
        {
            lblMSG.Text = "All fields cannot be empty. Please fill in the required fields.";
            lblMSG.Visible = true;
            txtUserID.Focus();
            return;
        }
        string mobileNumber = txtMobile.Text.Trim();

        string pattern = @"^\d{10}$";

        if (System.Text.RegularExpressions.Regex.IsMatch(mobileNumber, pattern))
        {
            // Mobile number is valid
            //lblMSG.ForeColor = System.Drawing.Color.Green;
            //lblMSG.Text = "Mobile number is valid.";
        }
        else
        {

            lblMSG.ForeColor = System.Drawing.Color.Red;
            lblMSG.Text = "Invalid mobile number. Please enter exactly 10 digits.";
        }
        if (Session["CaptchaCode"] != null && txtCaptha.Text != Session["CaptchaCode"].ToString())
        {
            lblMSG.Text = "Enter Text Again Shown in Code!";
            txtCaptha.Focus();
            txtCaptha.Text = "";
            txtMobile.Text = "";
            txtUserID.Text = "";
            return;
        }

        if (txtUserID.Text == "")
        {
            lblMSG.Text = "Enter UserId!";
            txtCaptha.Focus();
            txtCaptha.Text = "";
            txtMobile.Text = "";
            txtUserID.Text = "";
            return;
        }

        if (txtMobile.Text.Trim() != "")
        {
            try
            {

                SqlParameter UserId = new SqlParameter("@UserId", txtUserID.Text.Trim());
                SqlParameter Mobile = new SqlParameter("@Mobile", txtMobile.Text.Trim());
                string strQuery = @"SELECT  U.UserID, U.Password, M.RoleDesc, U.Userrole AS UserRoleID, U.UserName, U.logintoken
                                    FROM UserLogin U
			                        LEFT OUTER JOIN mst_Commissionary_Districts ON U.District_Code = mst_Commissionary_Districts.DISTRICTCODE
                                    LEFT OUTER JOIN  mst_Role M ON U.Userrole = M.Role 
                                    left outer join mst_Panchayats on mst_Panchayats.BlockCode=U.Block_Code  
                                    where U.UserID=@UserId AND Mobile=@Mobile";

                DataTable dt = clsData.GetDataTable(strQuery, new SqlParameter[] { UserId, Mobile });
                if (dt.Rows.Count == 0)
                {
                    lblMSG.Text = "User not found";
                    txtCaptha.Text = "";
                    txtMobile.Text = "";
                    txtUserID.Text = "";
                    return;
                }
                else
                {
                    Session["_LoginToken"] = RandomString(11);
                    Session["_UserID"] = txtUserID.Text.ToString();
                    Session["mobile_no"] = txtMobile.Text.ToString();
                    sendOTP(Session["mobile_no"].ToString().Trim());
                    Timer1.Enabled = true;
                    btnResent.Visible = false;
                    Session["timeData"] = "0";
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        else
            lblMSG.Text = "Enter mobile number!";
        txtCaptha.Text = "";
        txtMobile.Text = "";
        txtUserID.Text = "";
        return;
    }
    

    protected void sendOTP(string mob)
    {
       
        
        string Msg = string.Empty;
        if (Session["mobile_no"].ToString() == "")
        {
            Utility.showMessage(this, "You are not registered User");
            mpe.Hide();
            return;
        }
        string otp;
        string message = "";

        if (Session["otp"] != null && !string.IsNullOrEmpty(Session["otp"].ToString()))
        {
            otp = Session["otp"].ToString();
           //message = "Your One Time Password is =" + otp + " ,Bihar Government.";

            message = "OTP is " + otp.Trim() + " sent on your registered mobile number " + mob + " -  Home Dept. ,Bihar Government";


        }

        // else if (Session["otp"].ToString() == "")
        else
        
            //if (Session["otp"] != null && Session["otp"].ToString() == "")

            {
               //otp = "1234";
               otp = GetRandomNumID();
                Session["otp"] = otp.ToString().Trim();

               // message = "USER ID :" + txtUserID.Text + " and Password: OTP. " + otp + ", Bihar Government";
            message = "OTP is " + otp.Trim() + " sent on your registered mobile number " + mob + " -  Home Dept. ,Bihar Government";
        }
    
      
        string mb = "";
        try
        {
            string mobile = mob;
             mb = "******" + mob.ToString().Substring(mob.ToString().Length - 4, 4);

            SendWBSMS(mobile, message);

            Session["SendMobileOTP_1"] = "1";
          

        }
        catch (Exception)
        {
        }
        mpe.Show();
        Utility.showMessage(this, "OTP Has Been Send On Registered Mobile No. - (" + mb + ")" );
    }

    public string GetRandomNumID()
    {
        Random RandomClass = new Random();
        string RandomNumber, RandomNumber1;
        RandomNumber = RandomClass.Next(1000, 9999).ToString();
        RandomNumber1 = RandomClass.Next(1000, 9999).ToString();
        Session["otp"] = RandomNumber;
        return RandomNumber;
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["SendMobileOTP_1"] != null)
        {
            double timeInterval = 120000 - Convert.ToInt64(Session["SendMobileOTP_1"].ToString());
            TimeSpan t = TimeSpan.FromMilliseconds(timeInterval);
            string answer = string.Format(" {0:D2} min:{1:D2} Sec", t.Minutes, t.Seconds);
            lblTime.Text = "Resend OTP in:" + answer;
            Session["SendMobileOTP_1"] = Convert.ToInt64(Session["SendMobileOTP_1"].ToString()) + 1000;
            if (t.Minutes <= 0 && t.Seconds <= 0)
            {
                Session["SendMobileOTP_1"] = "0";
                Timer1.Enabled = false;
                btnResent.Visible = true;
                lblTime.Text = "";
            }

        }
    }
    protected void btnResent_Click(object sender, EventArgs e)
    {
        if (Session["SendMobileOTP_1"].ToString() == "0")
        {
            mpe.Show();
            sendOTP(Session["mobile_no"].ToString().Trim());
            Session["timeData"] = "0";
            Timer1.Enabled = true;
            btnResent.Visible = false;
        }
        else
        {
            Utility.showMessage(this, "Please wait before regenerating new Mobile OTP");
            return;
        }
    }
    public void resetFields()
    {
        string str = null;
        string[] mySessionVar = new string[HttpContext.Current.Session.Keys.Count];
        int cnt = 0;
        foreach (string key in HttpContext.Current.Session.Keys)
        {
            str += string.Format("{0}: {1}<br />", key, HttpContext.Current.Session[key].ToString());
            mySessionVar[cnt] = key.ToString();
            cnt++;
        }

        for (int i = 0; i < cnt; i++)
        {
            HttpContext.Current.Session.Remove(mySessionVar[i]);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            const int MaxAttempts = 3;
            const int LockoutMinutes = 5;

            if (Session["otp"] != null)
            {

                if (Session["OtpAttempts"] == null)
                {
                    Session["OtpAttempts"] = 0;
                }
                if (Session["OtpLockoutTime"] == null)
                {
                    Session["OtpLockoutTime"] = DateTime.MinValue;
                }

                int otpAttempts = (int)Session["OtpAttempts"];
                DateTime lockoutTime = (DateTime)Session["OtpLockoutTime"];

                if (Session["otp"].ToString() == txtOTP_Mob.Text.Trim())
                {

                    Session["OtpAttempts"] = 0;
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    txtUserId1.Text = Session["_UserID"].ToString();
                }
                else
                {
                    otpAttempts++;
                    Session["OtpAttempts"] = otpAttempts;

                    if (otpAttempts >= MaxAttempts)
                    {
                        Session["OtpLockoutTime"] = DateTime.Now.AddMinutes(LockoutMinutes);
                        lblMSG.Text = "";
                        SqlParameter UserId = new SqlParameter("@UserId", Session["_UserID"].ToString());
                        SqlParameter OtpLockoutTime = new SqlParameter("@OtpLockoutTime", Session["OtpLockoutTime"]);

                        DataTable dt = clsData.GetDataTableWithProc("InsertWrongOTPUserBlocked", new SqlParameter[] { UserId, OtpLockoutTime });
                        MsgUtility.showMessageNavigate(this, "Too many failed attempts. Try again after Sometime..", "Login_Default.aspx");
                    }
                    else
                    {
                        lblMSG.Text = "";
                        mpe.Show();
                        Utility.showMessage(this, "Invalid OTP. Try again.");
                    }
                }
            }
            else
            {
                Utility.showMessage(this, "OTP session expired. Please request a new OTP.");
            }
        }
        catch (Exception ex) { }
    }
   
    void SendWBSMS(string MobileNo, string message)
    {

        SMSHttpPostClient smsSend = new SMSHttpPostClient();

        //string Message = SendSms.sendSMSUnicode(message, MobileNo, "BIHAREDISTRICT-disastmgmt", "Dmdseoc@24", "c93d6db5-a313-45ef-9509-e6b105afddcd", "LRD12345SMS", "1307161173312959078");


        string Message = smsSend.sendSingleSMS("BIHAREDISTRICT-policeerss", "erss@123456", "BRGOVT", MobileNo, message, "bac4e6cd-ac88-43bd-86f2-aa0d04171e79", "1307166261966926791");

    }




  




    private static Random Random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }

    protected void btnChangePWD_Click(object sender, EventArgs e)
    {
        try
        {
            string rsapubkey = "-----BEGIN RSA PRIVATE KEY-----\r\nMIICWwIBAAKBgQCFJImKlY1Eptl/UqxKqSnZNZqGisDBYA541SuXkVOjbQ68BndT\r\ncSCl3PAEnxkRMyrNuXteyktrQQzNE7ZkDCkeLVAXAHIUYHcFC6gOfN6cPkbEft7k\r\nhIFLNiF0nK0ofHzajJgicQsBurBpx25v9xwSoNKt+p6RQ3uJNXd4FpHKtwIDAQAB\r\nAoGABNKsAM8mDOSWYFxYTWQPVgTxEgqYrOiT3aCrMZRcGUFsFTeOvwEME7YLI7+3\r\nor3dwRIE/AwL61ix5APgyYL/QkEx8fYlwz0Xwt9DAzvJ8zuSs/zogD3psu7XzsGb\r\nFXujZ6rn6Rh84JmSYf+4sxdgK7LzuLVSXAytljWMdjTWCVECQQDBn5VyGSSIty1q\r\ntudGQFzVusle7NGnTckZeYMPsVH1yew19/5XFupVQW3HG7x9A+Wo54BpTBOkEN4q\r\nVy8COPTdAkEAsAkFiTN5/q2Dn3Hax6Lh2+/iJiW13aOQuBL1fvuBQjxjMqd5P3uh\r\nYF3+JY8AP0vSDOavgQgnBvo1YYmPCD5KowJAULJkJMOZ2GkPwrQsO7lDd8h7eNaW\r\nonXm5HkiSH4MvLXFftC27jEcxR+Z0ywaKLhhFT425KvSW8Sz802WyARY+QJAGIdp\r\nShDuMtqiuJj4z1CXADqQ3Jfsm5GrHLsnkiYhgMVBXVGQx5PGjNMMEte2RRufjUSn\r\nt+cW6RX8UjcEwzVK2QJANkrZuC8+llPOFxX2YdqHpiEoxcbo2Z0nApt1kEiFkSwP\r\n4iS+iyiRaHK5CB9dPjJ+9FvFU7N5gx7QLQF4EE5pwg==\r\n-----END RSA PRIVATE KEY-----\r\n";

            string PWD = RSA_EncDec.RSA_Decrypt(rsapubkey, HidKey.Value);
            string changenewPWD = RSA_EncDec.RSA_Decrypt(rsapubkey, HidKey2.Value);

            //if (HidKey3.Value != Session["hidkey"].ToString())
            //{
            //    lblMSG.Text = "Malacious Login attempt";
            //    return;
            //}

            PWD = PWD.Remove(0, 6);
            changenewPWD = changenewPWD.Remove(0, 6);

            //String PWD = AESEncryptDecrypt.DecryptStringAES(txtNew.Text);
            //txtNew.Text = PWD.Remove(0, 6);
            //string changenewPWD = AESEncryptDecrypt.DecryptStringAES(TxtReNew.Text);
            //TxtReNew.Text = changenewPWD.Remove(0, 6);
            Session["password"] = enc.Encrypt(PWD);

           

           

            if (PWD != changenewPWD)
            {
                MsgUtility.showMessage(this, "Password and Confirm Password should be same.");
                txtNew.Text = "";
                TxtReNew.Text = "";
                TxtReNew.Focus();
                return;
            }

            

            if (!Getpassword())
            {
                MsgUtility.showMessage(this, "Password cannot be last three Password!");
                txtNew.Text = "";
                TxtReNew.Text = "";
               
                return;
            }

            SqlParameter UID = new SqlParameter("@userid", txtUserId1.Text.Trim());
            SqlParameter updatedate = new SqlParameter("@PwdUpdateDate", DateTime.Now);
            SqlParameter pass = new SqlParameter("@password", enc.Encrypt(PWD));   

            string strsql = "Update UserLogin set Password=@password,PwdUpdatedDate=@PwdUpdateDate, logintoken=NULL where UserID=@userid ";
            int i = clsData.ExecuteSql(strsql, new SqlParameter[] { pass, updatedate, UID });

            if (i > 0)
            {
                string ip = GetIpValue();
                string date = Convert.ToString(DateTime.Now);
                Session["user"] = txtUserId1.Text.Trim();
                Insertpasswordhistory(Session["user"].ToString(), enc.Encrypt(PWD), date, ip);
                MsgUtility.showMessageNavigate(this, "Password Updated Successfully", "Login_Default.aspx");
            }
            else
            {
                mpe.Show();
                MsgUtility.showMessage(this, "Please Try Again.");
            }

        }
        catch(Exception ex) { }
    }
    private void Insertpasswordhistory(string _UserId, string _pwd, string _dt, string _Ip)
    {
        try
        {
            string sql = "insert into tbl_useridpasswordhistory(username, pswd, date,ip) values(@username, @pswd,@date,@ip)";
            SqlParameter UserId = new SqlParameter("@username", _UserId);
            SqlParameter IsLogIn = new SqlParameter("@pswd", _pwd);
            SqlParameter date = new SqlParameter("@date", Convert.ToDateTime(_dt));
            SqlParameter IpAddress = new SqlParameter("@ip", _Ip);

            int i = clsData.ExecuteSql(sql, new SqlParameter[] { UserId, IsLogIn, date, IpAddress });
        }
        catch (Exception ex)
        {
        }
    }
    private bool Getpassword()
    {
        string ip = GetIpValue();
        string dtdate = DateTime.Now.ToString("dd-MMM-yyyy");
        bool result = true;
        string NewPwd = string.Empty;
        if (Session["password"] != null)
        {
            NewPwd = Session["password"].ToString();

        }
        string tk = Session["_UserID"].ToString();
        string my_String = Regex.Replace(tk, " ", "+");

        string strQuery = @"select Top 3  username,pswd,Date from tbl_useridpasswordhistory  WHERE username=@username order by date desc";
        DataTable dt = clsData.GetDataTable(strQuery, new SqlParameter[] { new SqlParameter("@username", tk) });

        if (dt.Rows.Count > 0)
        {
  
            foreach (DataRow dr in dt.Rows)
            {
                string pswd = dr["pswd"].ToString();
                if (NewPwd == pswd)
                {                  
                    result = false;
                    return result;
                }

            }
        }
        return result;
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login_Default.aspx");
    }
    public string GetIpValue()
    {
        string ipAdd = "Not Available";
        ipAdd = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (string.IsNullOrEmpty(ipAdd))
        {
            ipAdd = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        return ipAdd;
    }
}