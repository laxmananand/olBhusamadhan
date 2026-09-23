using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EO.Web.Internal;
using System.Text.RegularExpressions;

public partial class CLG_ChangePass : System.Web.UI.Page
{
    //SMSNewService21.SMSWebServiceSoapClient smsSend = new SMSNewService21.SMSWebServiceSoapClient();
    NICMailService.NICMailService eclient = new NICMailService.NICMailService();
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)

    {
        if (Session["hidkey"] == null)
            setHidKey();
        reloadcaptcha.ImageUrl = "~/images/refresh.png";
        if (Session["Role"] == null && Session["Block_Code"] == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/DeptDefault.aspx");
        }
        else
        {

            if (!IsPostBack)
            {
                setHidKey();
            }
        }
    }
    public void setHidKey()
    {
        Random rand = new Random();
        long randnum2 = (long)(rand.NextDouble() * 900000000000000) + 8000000000000000;
        Session["hidkey"] = randnum2.ToString();
       // HidKey3.Value = Session["hidkey"].ToString();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {

    }
    public void resetFields()
    {
        txtCaptha.Text = "";
        txtNewPwd.Text = "";
        txtRePwd.Text = "";
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        Encryptor enc = new Encryptor(Encryptor.PrivateKey);


        //Kiran 30012025

        string rsapubkey = "-----BEGIN RSA PRIVATE KEY-----\r\nMIICWwIBAAKBgQCFJImKlY1Eptl/UqxKqSnZNZqGisDBYA541SuXkVOjbQ68BndT\r\ncSCl3PAEnxkRMyrNuXteyktrQQzNE7ZkDCkeLVAXAHIUYHcFC6gOfN6cPkbEft7k\r\nhIFLNiF0nK0ofHzajJgicQsBurBpx25v9xwSoNKt+p6RQ3uJNXd4FpHKtwIDAQAB\r\nAoGABNKsAM8mDOSWYFxYTWQPVgTxEgqYrOiT3aCrMZRcGUFsFTeOvwEME7YLI7+3\r\nor3dwRIE/AwL61ix5APgyYL/QkEx8fYlwz0Xwt9DAzvJ8zuSs/zogD3psu7XzsGb\r\nFXujZ6rn6Rh84JmSYf+4sxdgK7LzuLVSXAytljWMdjTWCVECQQDBn5VyGSSIty1q\r\ntudGQFzVusle7NGnTckZeYMPsVH1yew19/5XFupVQW3HG7x9A+Wo54BpTBOkEN4q\r\nVy8COPTdAkEAsAkFiTN5/q2Dn3Hax6Lh2+/iJiW13aOQuBL1fvuBQjxjMqd5P3uh\r\nYF3+JY8AP0vSDOavgQgnBvo1YYmPCD5KowJAULJkJMOZ2GkPwrQsO7lDd8h7eNaW\r\nonXm5HkiSH4MvLXFftC27jEcxR+Z0ywaKLhhFT425KvSW8Sz802WyARY+QJAGIdp\r\nShDuMtqiuJj4z1CXADqQ3Jfsm5GrHLsnkiYhgMVBXVGQx5PGjNMMEte2RRufjUSn\r\nt+cW6RX8UjcEwzVK2QJANkrZuC8+llPOFxX2YdqHpiEoxcbo2Z0nApt1kEiFkSwP\r\n4iS+iyiRaHK5CB9dPjJ+9FvFU7N5gx7QLQF4EE5pwg==\r\n-----END RSA PRIVATE KEY-----\r\n";

        string newPWD = RSA_EncDec.RSA_Decrypt(rsapubkey, HidKey.Value);
        string changenewPWD = RSA_EncDec.RSA_Decrypt(rsapubkey, HidKey2.Value);
        string oldPWD = RSA_EncDec.RSA_Decrypt(rsapubkey, HidKey4.Value);

        //if (HidKey3.Value != Session["hidkey"].ToString())
        //{
        //    lblMsg.Text = "Malacious Login attempt";
        //    return;
        //}

        newPWD = newPWD.Remove(0, 6);
        changenewPWD = changenewPWD.Remove(0, 6);
        oldPWD = oldPWD.Remove(0, 6);

        Session["password"] = enc.Encrypt(newPWD);
        if (changenewPWD != newPWD)
        {
            Utility.showMessage(this, "New Password does not match");
            resetFields();
            return;

        }


        if (!ValidatePassword(newPWD))
        {
            Utility.showMessage(this, "New Password must be 8-10 characters long with  At least one upper case english letter [A-Z], At least one lower case english letter[a-z], at least one numeric character (0-9) and one special Character[# ? ! @ $ % *].");
            resetFields();
            return;
        }

        if (txtCaptha.Text.Trim() == "")
        {
            Utility.showMessage(this, "Please Enter Text Shown In Code!");
            txtCaptha.Focus();
            return;
        }

        if (Session["CaptchaCode"] != null && txtCaptha.Text != Session["CaptchaCode"].ToString())
        {
            txtCaptha.Focus();
            lblMsg.Text = "Incorrect Captcha, Please Try Again!";
            resetFields();
            return;
        }
        if (!Getpassword())
        {
            MsgUtility.showMessage(this, "Password cannot be last three Password!");
            resetFields();
            return;
        }

        string old = enc.Encrypt(oldPWD);
        string npwd = enc.Encrypt(newPWD);
        string strQuery = string.Empty;



        strQuery = "update UserLogin set Password=@Password,IsStrong='Y'  where UserID='" + Session["UserID"] + "' and  Password=@oldpass";
        SqlParameter prmPWD = new SqlParameter("@Password", npwd);
        SqlParameter prmoldPWD = new SqlParameter("@oldpass", old);
        if (clsData.ExecuteSql(strQuery, new SqlParameter[] { prmPWD, prmoldPWD }) > 0)
        {
            string ip = GetIpValue();
            string date = Convert.ToString(DateTime.Now);
            Insertpasswordhistory(Session["UserID"].ToString(), npwd, date, ip);
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();
            Utility.showMessageNavigate(this, "Password Changed Successfully. Please Re-login With Your New Password.", "Login_Default.aspx");
        }
        else

            Utility.showMessage(this, "Could not change password, please try after some time...");
    }

    public static bool ValidatePassword(string password)
    {
        var regexPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        if (!Regex.IsMatch(password, regexPattern))
        {
            return false;
        }

        if (Regex.IsMatch(password, @"(.)\1{2,}"))
        {
            return false;
        }

        string[] commonPasswords = { "password123", "12345678", "qwerty", "letmein", "Dgrc@123", "qwert123", "abc@123" };
        if (Array.Exists(commonPasswords, p => p.Equals(password, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        return true;
    }

    protected void reloadcaptcha_Click(object sender, ImageClickEventArgs e)
    {
        imgCaptcha.ImageUrl = "CreateCaptcha.aspx";
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
        string tk = Session["UserID"].ToString();
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