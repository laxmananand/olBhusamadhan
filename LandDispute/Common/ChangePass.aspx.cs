using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
public partial class CLG_ChangePass : System.Web.UI.Page
{
    clsDataAccessLandDispute cls = new clsDataAccessLandDispute();
    //SMSNewService21.SMSWebServiceSoapClient smsSend = new SMSNewService21.SMSWebServiceSoapClient();
    NICMailService.NICMailService eclient = new NICMailService.NICMailService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {            
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../DeptDefault.aspx");
        }
        else
        {            
            if (!IsPostBack)
            {
                bindData();
                //UserName.InnerHtml = Session["Username"].ToString().Trim();
            }
        }
    }

    protected void bindData()
    {
        try
        {
            string sql = @"select UserID, UserName,Password,Created_date,PwdUpdatedDate,Thana_Code,Block_Code,District_Code,Userrole,Islock,PwdResetBy,PwdResetDate,StateCode,IMEI,RangeCode,Sub_DivCode,Commsionary_Code,IsChangePassword from UserLogin where UserID=@UserID";
            DataTable dt = cls.GetDataTable(sql, new SqlParameter[] { new SqlParameter("@UserID", Convert.ToString(Session["UserID"])) });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["IsChangePassword"].ToString().Trim() == "Y")
                {
                    UserName.InnerHtml = dt.Rows[0]["UserName"].ToString().Trim();
                    //txtName.Text = dt.Rows[0]["CollegeName"].ToString().Trim();
                    txtMobileNo.Text = dt.Rows[0]["contact_person_mobile"].ToString().Trim();
                    txtEmailID.Text = dt.Rows[0]["Email"].ToString().Trim();
                    ViewState["MobileNo"] = txtMobileNo.Text.Trim();
                    ViewState["isVerifyMobile"] = "Y";

                    MVerifyStatus.Text = "<i class='fa fa-x fa-check-circle'></i> Verified";
                    MpnlEdit.Visible = true;
                    MpnlVerify.Visible = false;
                    txtMobileNo.Enabled = false;
                    txtMobileNo.BackColor = System.Drawing.Color.Green;
                    txtMobileNo.ForeColor = System.Drawing.Color.White;
                    MGetOtpBtn.Visible = false;


                    ViewState["isVerifyEmail"] = "Y";
                    ViewState["EmailId"] = txtEmailID.Text.Trim();

                    EVerifyStatus.Text = " <i class='fa fa-x fa-check-circle'></i> Verified";
                    EpnlEdit.Visible = true;
                    EpnlVerify.Visible = false;
                    txtEmailID.Enabled = false;
                    txtEmailID.BackColor = System.Drawing.Color.Green;
                    txtEmailID.ForeColor = System.Drawing.Color.White;
                    EGetOtpBtn.Visible = false;

                }
                else
                {
                    UserName.InnerHtml = dt.Rows[0]["UserName"].ToString().Trim();
                    ViewState["isVerifyMobile"] = "";
                    ViewState["isVerifyEmail"] = "";
                }
            }
        }
        catch
        {

        }


    }

    private string GetUserIP()
    {
        string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipList))
        {
            return ipList.Split(',')[0];
        }

        return Request.ServerVariables["REMOTE_ADDR"];
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (txtCaptha.Text.Trim() == "")
        {
            Utility.showMessage(this, "Please Enter Text Shown In Code!");
            txtCaptha.Focus();
            return;
        }
        ccJoin.ValidateCaptcha(txtCaptha.Text.Trim());
        if (!ccJoin.UserValidated)
        {
            Utility.showMessage(this, "Please Enter Text Again Shown In Code!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtCaptha.Focus();
            return;
        }

        if (txtName.Text.Trim() == "")
        {
            Utility.showMessage(this, "Please Enter Name!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtName.Focus();
            return;
        }
        if (txtMobileNo.Text.Trim() == "" || txtMobileNo.Text.Length!=10)
        {
            Utility.showMessage(this, "Please Enter Mobile No!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtMobileNo.Focus();
            return;
        }
        if (txtEmailID.Text.Trim() == "" )
        {
            Utility.showMessage(this, "Please Enter E-Mail Id!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtMobileNo.Focus();
            return;
        }
        if (txtNewPwd.Text == "" || txtRePwd.Text== "" )
        {
            Utility.showMessage(this, "Please Enter New Password !");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtNewPwd.Focus();
            return;
        }
        if (ViewState["MobileNo"] == null)
        {
            Utility.showMessage(this, "Please Enter MobileNo Number!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtMobileNo.Focus();
            return;
        }
        if (ViewState["isVerifyMobile"] == null)
        {
            Utility.showMessage(this, "Please Verify MobileNo Number!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            MtxtOTP.Focus();
            return;
        }
        if (Convert.ToString(ViewState["isVerifyMobile"]) != "Y")
        {
            Utility.showMessage(this, "Please Verify MobileNo Number!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            MtxtOTP.Focus();
            return;
        }
        if (ViewState["EmailId"] == null)
        {
            Utility.showMessage(this, "Please Enter Email ID !");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtMobileNo.Focus();
            return;
        }
        if (ViewState["isVerifyEmail"] == null)
        {
            Utility.showMessage(this, "Please Verify Email ID !");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            EtxtOTP.Focus();
            return;
        }
        if (Convert.ToString(ViewState["isVerifyEmail"]) != "Y")
        {
            Utility.showMessage(this, "Please Verify Email ID!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            EtxtOTP.Focus();
            return;
        }

        try
        {
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            DataTable dtm = cls.GetDataTable("select count(1) from UserLogin  where MobileNo=@MobileNo and UserID=@UserID", new SqlParameter[] { new SqlParameter("@MobileNo", Convert.ToString(txtMobileNo.Text).Trim()), new SqlParameter("@UserID", Convert.ToString(Session["UserID"])) });
            if (dtm.Rows[0][0].ToString() != "0")
            {
                Utility.showMessage(this, "This Mobile Number Is Already Register, Please Try Again!");
                txtMobileNo.Focus();
                txtMobileNo.Enabled = true;
                return;

            }

            DataTable dte = cls.GetDataTable("select count(1) from UserLogin where EmailId=@EmailId and UserID=@UserID", new SqlParameter[] { new SqlParameter("@EmailId", Convert.ToString(txtEmailID.Text).Trim()), new SqlParameter("@UserID", Convert.ToString(Session["UserID"])) });
            if (dte.Rows[0][0].ToString() != "0")
            {
                Utility.showMessage(this, "This Email ID Is Already Register, Please Try Again!");
                txtEmailID.Focus();
                return;
            }
            string sql = @"select password from UserLogin where UserID=@UserID";
            DataTable dt = cls.GetDataTable(sql, new SqlParameter[] { new SqlParameter("@UserID", Convert.ToString(Session["UserID"])) });
            // Session["dt"] = dt;
            if (dt.Rows.Count != 0)
            {
                string PWD = dt.Rows[0]["password"].ToString().Trim();

                if (PWD == null || PWD.ToString() == enc.Encrypt(txtNewPwd.Text))
                {
                    Utility.showMessage(this, "Old Password And New Password Is Matched, Please Re-Enter New Password.");
                    return;
                }
                string strQuery = "UPDATE UserLogin SET Password=@Password,PwdUpdatedDate=GETDATE(),IsChangePassword='Y' where UserID=@UserID";

                SqlParameter UserID = new SqlParameter("@UserID", Convert.ToString(Session["UserID"]));              
                SqlParameter Password = new SqlParameter("@Password", enc.Encrypt(txtNewPwd.Text)); 
                if (cls.ExecuteSql(strQuery, new SqlParameter[] { UserID, Password }) > 0)
                {
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.Abandon();
                    Utility.showMessageNavigate(this, "Password Changed Successfully. Please Re-login With Your New Password.", "User_Login.aspx");

                }
                else
                    Utility.showMessage(this, "Could not change password, please try after some time...");
            }
        }
        catch (Exception Ex)
        { }

    }
    private string generatePassword()
    {
        try
        {
            int lenthofpass = 6;
            string allowedChars = "";

            allowedChars += "1,2,3,4,5,6,7,8,9";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < lenthofpass; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }
        catch
        {
            return null;
        }
    }
    protected void MGetOtpBtn_Click(object sender, System.EventArgs e)
    {
        if (txtMobileNo.Text == "")
        {
            Utility.showMessage(this, "Please Enter Mobile No.");
            return;
        }

        if (txtMobileNo.Text.Length != 10)
        {
            Utility.showMessage(this, "Please Enter 10 deigit Mobile No");
            return;
        }
        string MOTP = generatePassword();
        ViewState["MOTP"] = MOTP;
        ViewState["MobileNo"] = txtMobileNo.Text.Trim();
        try
        {
            sendOTP(ViewState["MobileNo"].ToString());
        }
        catch
        { }
    }
    protected void sendOTP(string mob)
    {
        string Msg = string.Empty;
        string otp = "";
        string message = "";
        if (ViewState["MOTP"].ToString() != "")
        {    
            message = "OTP is " + ViewState["MOTP"].ToString().Trim() + " sent on your registered mobile number " + mob + ", Education Dept. -Bihar Government";
        }
        else if (ViewState["MOTP"].ToString() == "")
        {
            txtMobileNo.Enabled = true;
            MpnlVerify.Visible = false;
            MGetOtpBtn.Visible = true;
            MpnlEdit.Visible = true;
        }
       // SMSReferenceWithID.SMSWebServiceSoapClient sendSMS = new SMSReferenceWithID.SMSWebServiceSoapClient();
        string send = "";
        try
        {
            string mobile = mob;
            //string mobile = "9504245424";
            SMSReferenceWithID.SMSWebService smsSend = new SMSReferenceWithID.SMSWebService();
            send = smsSend.sendSMSSingle(message, mobile, "BIHAREDISTRICT-eduse", "edusemiddle@123", "0812947c-1c6a-4778-921b-c65038f8315e", "LRD12345SMS", "1307161158541624210");
            //send = sendSMS.SendSMSUnicodeTestwithSenderid(message, mobile, "BIHAREDISTRICT-eduse", "edusemiddle@123", "0812947c-1c6a-4778-921b-c65038f8315e", "LRD12345SMS", "BRGOVT");
            if (send == "Message Sent Successfully..." || send.ToString().Split(',')[0]=="402")
            {

                Utility.showMessage(Page, "OTP has been sent on  (" + mob + ") , Please Match It.");
                txtMobileNo.Enabled = false;
                MpnlVerify.Visible = true;
                MGetOtpBtn.Visible = false;
                MpnlEdit.Visible = false;
            }
        }
        catch (Exception esms)
        {
            Utility.showMessage(Page, "OTP not sent. Kindly Try Again...");
        }
    }
    protected void MlnkResend_Click(object sender, System.EventArgs e)
    {
        try
        {
            sendOTP(ViewState["MobileNo"].ToString());
        }
        catch
        { }
        MlnkResend.Visible = false;
    }
    protected void btnVerify_Click(object sender, System.EventArgs e)
    {
        if (Convert.ToString(ViewState["MOTP"]).Trim() == MtxtOTP.Text.Trim())
        {
            ViewState["isVerifyMobile"] = "Y";
            txtMobileNo.Text = Convert.ToString(ViewState["MobileNo"]);
            MVerifyStatus.Text = "<i class='fa fa-x fa-check-circle'></i> Verified";
            MpnlEdit.Visible = true;
            MpnlVerify.Visible = false;
            txtMobileNo.Enabled = false;
            txtMobileNo.BackColor = System.Drawing.Color.Green;
            txtMobileNo.ForeColor = System.Drawing.Color.White;
            MGetOtpBtn.Visible = false;

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('OTP is wrong , Please Try Again !');", true);
            return;
        }
    }
    protected void EGetOtpBtn_Click(object sender, System.EventArgs e)
    {
        if (txtEmailID.Text == "")
        {
            Utility.showMessage(this, "Please Enter Email ID.");
            return;
        }

        string EOTP = generatePassword();
        ViewState["EOTP"] = EOTP;
        ViewState["EmailId"] = txtEmailID.Text.Trim();
        try
        {
            sendOTPEMAIL(ViewState["EmailId"].ToString());
        }
        catch
        {
        }
        txtEmailID.Enabled = false;
        EpnlVerify.Visible = true;
        EGetOtpBtn.Visible = false;
        EpnlEdit.Visible = false;
    }
    protected void ElnkResend_Click(object sender, System.EventArgs e)
    {
        try
        {
            sendOTPEMAIL(ViewState["EmailId"].ToString());
        }
        catch
        {
        }
        ElnkResend.Visible = false;
    }

    protected void sendOTPEMAIL(string email)
    {
        string Msg = string.Empty;
        string otp = "";
        string message = "";
        if (ViewState["EOTP"].ToString() != "")
        {
            message = "OTP: " + ViewState["EOTP"].ToString().Trim() + ", Please do not share for Security purpose,Education Department, Govt. Of Bihar";
        }
        else
        {
            txtEmailID.Enabled = false;
            EpnlVerify.Visible = true;
            //EGetOtpBtn.Visible = false;
            EpnlEdit.Visible = false;
        }

       
        string message1 = "To<br/> Shri / Smt / Ms '" + txtName.Text + "'  <br />  OTP: " + ViewState["EOTP"].ToString() + ".<br /> Please Enter This OTP Before Finalise Only For First Time Secure Login Step In This Application. Govt. of Bihar";
        message1 = message1 + "This is a system generated email, please do not revert back.<br/>";
        string Email = email.Trim();
        string res = eclient.SendMessage("support.dgrc-bih@gov.in", "G6%vR0&vV6", Email.Trim(), "Regarding Validate Email ID", message, "nicmail$2018#");
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('OTP Has Been Send On Registered Email ID.')", true);
    }
    protected void EbtnVerify_Click(object sender, System.EventArgs e)
    {
        if (Convert.ToString(ViewState["EOTP"]).Trim() == EtxtOTP.Text.Trim())
        {
            ViewState["isVerifyEmail"] = "Y";
            txtEmailID.Text = Convert.ToString(ViewState["EmailId"]);
            EVerifyStatus.Text = "<i class='fa fa-x fa-check-circle'></i> Verified";
            EpnlEdit.Visible = true;
            EpnlVerify.Visible = false;
            txtEmailID.Enabled = false;
            txtEmailID.BackColor = System.Drawing.Color.Green;
            txtEmailID.ForeColor = System.Drawing.Color.White;
           // EGetOtpBtn.Visible = false;

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('OTP is wrong , Please Try Again !');", true);
            return;
        }

    }

}