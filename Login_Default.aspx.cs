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
using Microsoft.Security.Application;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using EO.Web.Internal;

public partial class Login_Default : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    string key = Encryptor.PrivateKey.ToString();
    Encryptor enc = new Encryptor(Encryptor.PrivateKey);
    protected void Page_Load(object sender, EventArgs e)
    {
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
            Uri refer = Request.UrlReferrer;

            if ((refer != null) && (!(Request.Url.Host != refer.Host)))
            {
                try
                {

                    if (!String.IsNullOrEmpty(Request.QueryString["msg"]))
                    {
                        string msg = Request.QueryString["msg"].ToString();
                        if (msg == "W")
                        {
                            lblmsgerror.Text = "Wrong User ID or Password";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtCaptha.Text = "";
                        }
                        if (msg == "T")
                        {
                            lblmsgerror.Text = "User is Locked, Try after sometime.";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtCaptha.Text = "";
                        }
                        if (msg == "4")
                        {
                            lblmsgerror.Text = "Request Timed Out";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtCaptha.Text = "";
                        }
                        if (msg == "9")
                        {
                            lblmsgerror.Text = "Security Alert!";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtCaptha.Text = "";
                        }
                        if (msg == "C")
                        {
                            lblmsgerror.Text = "Password Successfully Changed";
                        }
                        if (msg == "L")
                        {
                            lblmsgerror.Text = "Successfully LoggedOut";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtCaptha.Text = "";
                        }

                        if (msg == "cncl")
                        {
                            lblmsgerror.Text = "Concurrent Logins not allowed";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtCaptha.Text = "";
                        }

                        if (Request.Cookies["EservicesAppNameAuth"] != null)
                        {
                            Response.Cookies["EservicesAppNameAuth"].Path = Request.ApplicationPath;
                            Response.Cookies["EservicesAppNameAuth"].Value = "";
                            Response.Cookies["EservicesAppNameAuth"].Expires = DateTime.Now.AddDays(-1);
                        }
                    }
                    else
                    {
                        lblmsgerror.Text = "";
                    }
                }
                catch
                {  }
            }
            else
            {
                lblmsgerror.Text = "";
            }
            setHidKey();
        }
    }

    public void setHidKey()
    {
        Random rand = new Random();
        long randnum2 = (long)(rand.NextDouble() * 900000000000000) + 8000000000000000;
        Session["hidkey"] = randnum2.ToString();
        //HidKey3.Value = Session["hidkey"].ToString();
    }
   
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Session["FailedAttempt"] == null)
        {
            Session["FailedAttempt"] = "0";
        }

        if (Convert.ToInt32(Session["FailedAttempt"].ToString()) <= 5)
        {
            int loginattempt = 0;

            loginattempt = Convert.ToInt32(Session["FailedAttempt"].ToString()) + 1;
        }

        else
        {
            Label1.Text = "Account has been temproraly locked for 15 minutes due to several failed attempts.";
        }
        if (string.IsNullOrEmpty(txtUserName.Text))
        {
            lblmsgerror.Visible = true;
            lblmsgerror.Text = "UserName required..!";
            txtUserName.Focus();       
            Clear();
            return;
        }

        if (Session["CaptchaCode"] != null && txtCaptha.Text != Session["CaptchaCode"].ToString())
        {
            txtCaptha.Focus();
            lblmsgerror.Visible = true;
            lblmsgerror.Text = "Incorrect Captcha, Please Try Again!";
            Clear();
            return;
        }
        else
        {
            verifyLogin();
        }      
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
    public void Clear()
    {
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtCaptha.Text = "";
        Session["CaptchaCode"] = "";
    }
    protected void rblogintype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rblogintype.SelectedValue == "0")
        //{
        //    //dvpublic.Visible = false;
        //    dvdept.Visible = true;
        //}
        //else
        //{
        //    Response.Redirect("Login_Default_new.aspx");
        //    //dvpublic.Visible = true;
        //    //dvdept.Visible = false;
        //}
    }

    public void verifyLogin()
    {
        try
        {

            string rsapubkey = "-----BEGIN RSA PRIVATE KEY-----\r\nMIICWwIBAAKBgQCFJImKlY1Eptl/UqxKqSnZNZqGisDBYA541SuXkVOjbQ68BndT\r\ncSCl3PAEnxkRMyrNuXteyktrQQzNE7ZkDCkeLVAXAHIUYHcFC6gOfN6cPkbEft7k\r\nhIFLNiF0nK0ofHzajJgicQsBurBpx25v9xwSoNKt+p6RQ3uJNXd4FpHKtwIDAQAB\r\nAoGABNKsAM8mDOSWYFxYTWQPVgTxEgqYrOiT3aCrMZRcGUFsFTeOvwEME7YLI7+3\r\nor3dwRIE/AwL61ix5APgyYL/QkEx8fYlwz0Xwt9DAzvJ8zuSs/zogD3psu7XzsGb\r\nFXujZ6rn6Rh84JmSYf+4sxdgK7LzuLVSXAytljWMdjTWCVECQQDBn5VyGSSIty1q\r\ntudGQFzVusle7NGnTckZeYMPsVH1yew19/5XFupVQW3HG7x9A+Wo54BpTBOkEN4q\r\nVy8COPTdAkEAsAkFiTN5/q2Dn3Hax6Lh2+/iJiW13aOQuBL1fvuBQjxjMqd5P3uh\r\nYF3+JY8AP0vSDOavgQgnBvo1YYmPCD5KowJAULJkJMOZ2GkPwrQsO7lDd8h7eNaW\r\nonXm5HkiSH4MvLXFftC27jEcxR+Z0ywaKLhhFT425KvSW8Sz802WyARY+QJAGIdp\r\nShDuMtqiuJj4z1CXADqQ3Jfsm5GrHLsnkiYhgMVBXVGQx5PGjNMMEte2RRufjUSn\r\nt+cW6RX8UjcEwzVK2QJANkrZuC8+llPOFxX2YdqHpiEoxcbo2Z0nApt1kEiFkSwP\r\n4iS+iyiRaHK5CB9dPjJ+9FvFU7N5gx7QLQF4EE5pwg==\r\n-----END RSA PRIVATE KEY-----\r\n";

            string PWD = RSA_EncDec.RSA_Decrypt(rsapubkey, HidKey.Value);
            string UserName = RSA_EncDec.RSA_Decrypt(rsapubkey, HidKey2.Value);

            //if (HidKey3.Value != Session["hidkey"].ToString())
            //{
            //    lblmsgerror.Text = "Malacious Login attempt";
            //    return;
            //}

            PWD = PWD.Remove(0, 6);
            UserName = UserName.Remove(0, 6);

            if (true)
            {
                
                string dtt = System.DateTime.Now.ToString("ddMMyyyyHHmmss");
                dtt = dtt + Server.MachineName.ToString();

                int loginattempt = 0;

                try
                {
                    loginattempt = Convert.ToInt32(Session["FailedAttempt"].ToString()) + 1;
                }
                catch
                {

                }
                string url = "1=" + UserName + "&2=" + PWD + "&3=" + dtt + "&4=" + HidKey.Value + "&5=" + Session["FailedAttempt"].ToString();

                url = Aes256CbcEncrypterApp.Encrypt(url, "85934784000243428593478400024342");

                HttpCookie httpCookie;
                int iCookieCount = HttpContext.Current.Request.Cookies.Count;
                for (int i = 0; i < iCookieCount; i++)
                {
                    httpCookie = new HttpCookie(HttpContext.Current.Request.Cookies[i].Name);
                    httpCookie.Expires = DateTime.Now.AddDays(-1);
                    httpCookie.Path = HttpContext.Current.Request.Cookies[i].Path;
                    httpCookie.Domain = HttpContext.Current.Request.Cookies[i].Domain;
                    httpCookie.Values.Clear();
                    HttpContext.Current.Response.Cookies.Add(httpCookie);
                }
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("ValidLogin.aspx?" + url, false);
            }
           
        }
        catch (Exception ex)
        {
            Utility.showMessage(this, "Please enter Correct UserID and Password!");

        }
    }

    protected void reloadcaptcha_Click(object sender, ImageClickEventArgs e)
    {
        imgCaptcha.ImageUrl = "CreateCaptcha.aspx";
    }
}