using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using esms_client;
public partial class CLG_ChangePass : System.Web.UI.Page
{
    clsDataAccessLandDispute cls = new clsDataAccessLandDispute();
    //SMSNewService21.SMSWebServiceSoapClient smsSend = new SMSNewService21.SMSWebServiceSoapClient();
    NICMailService.NICMailService eclient = new NICMailService.NICMailService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null && Session["Block_Code"]==null)
        {            
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/DeptDefault.aspx");
        }
        else
        {
            if (Convert.ToString(Session["Role"]) == "SHOOPT")
            {
                labName.Text = "SHO Name[*] :";
                divThana.Visible = true;

            }
            else
            {
                labName.Text = "CO Name[*] :";
                divThana.Visible = false;
            }

            if (!IsPostBack)
            {
                BindDist();
                if (Session["District_Code"] != null)
                {
                    ddlDistrict.SelectedValue = Session["District_Code"].ToString();
                    ddlDistrict.Enabled = false;
                }
                BindSubDivision();
                if (Session["Sub_DivCode"] != null)
                {
                    ddlSubdivision.SelectedValue = Session["Sub_DivCode"].ToString();
                    ddlSubdivision.Enabled = false;
                }
                BindBlock();
                if (Session["Block_Code"] != null)
                {
                    ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                    if (ddlBlock.SelectedValue.Trim() != "0")
                    {
                        ddlBlock.Enabled = false;
                    }
                }

                if (Convert.ToString( Session["Role"]) == "SHOOPT")
                {
                    BindPolice();
                    if (Session["Thana_Code"] != null)
                    {
                        ddlPolice.SelectedValue = Session["Thana_Code"].ToString();
                        if (ddlPolice.SelectedValue.Trim() != "0")
                        {
                            ddlPolice.Enabled = false;
                        }
                    }
                }
                bindData();
                //UserName.InnerHtml = Session["Username"].ToString().Trim();
            }
        }
    }

    void BindDist()
    {
        try
        {
            string sql = @"SELECT distinct DISTRICTNAME,DISTRICTCODE from mst_Commissionary_Districts ORDER BY DISTRICTNAME ";
            //SqlParameter _Commisionary = new SqlParameter("@DIVISIONCODE", Commisionary);
            DataTable dt = cls.GetDataTable(sql);
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
        // BindBlock();
        // BindPolice();

    }

    void BindSubDivision()
    {
        try
        {


            string sql = @" select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En";

            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            DataTable dt = cls.GetDataTable(sql, new SqlParameter[] { _DCode });


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

            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString());
            DataTable dt = cls.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode });


            ddlBlock.DataSource = dt;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }

        // BindVillage();
        // BindPanchyat();

    }

    void BindPolice()
    {
        try
        {          
            
            string sql = @" select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t inner join SubDivisions p on t.Subdivision_Code =p.Sd_Code2 where p.Sd_Code2=@Subdivision_Code and p.DistCode=@District_Code  order by Police_Station";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString());
            //SqlParameter _RCode = new SqlParameter("@Range_code", ddlrange.SelectedValue.ToString());
            DataTable dt = cls.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode });
            ddlPolice.DataSource = dt;
            ddlPolice.DataTextField = "Police_Station";
            ddlPolice.DataValueField = "PS_Code";
            ddlPolice.DataBind();
            ddlPolice.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubDivision();
        BindBlock();
        BindPolice();        
    }
    protected void ddlSubdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock();
        BindPolice();
       
    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void bindData()
    {
        try
        {
            string sql = @"select UserID, UserName,Password,Created_date,PwdUpdatedDate,Thana_Code,Block_Code,District_Code,Userrole,Islock,PwdResetBy,PwdResetDate,StateCode,IMEI,RangeCode,Sub_DivCode,Commsionary_Code,isnull(UserLogin.IsPassChange,'N') as IsChangePassword from UserLogin where UserID=@UserID";
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

                    //EVerifyStatus.Text = " <i class='fa fa-x fa-check-circle'></i> Verified";
                    //EpnlEdit.Visible = true;
                    //EpnlVerify.Visible = false;
                    txtEmailID.Enabled = false;
                    txtEmailID.BackColor = System.Drawing.Color.Green;
                    txtEmailID.ForeColor = System.Drawing.Color.White;
                    //EGetOtpBtn.Visible = false;

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
        if (ddlBlock.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया अंचल का चुनाव करें...!");
            ddlBlock.Focus();
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

        



        /*
        if (txtEmailID.Text.Trim() == "" )
        {
            Utility.showMessage(this, "Please Enter E-Mail Id!");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtMobileNo.Focus();
            return;
        }*/

        if (txtNewPwd.Text == "" || txtRePwd.Text== "" )
        {
            Utility.showMessage(this, "Please Enter New Password !");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtNewPwd.Focus();
            return;
        }

        if (!(new System.Text.RegularExpressions.Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,10}$")).IsMatch(txtNewPwd.Text.Trim()))
        {

            Utility.showMessage(this, "New Password must be 8-10 characters long with  At least one upper case english letter [A-Z], At least one lower case english letter[a-z], at least one numeric character (0-9) and one special Character[# ? ! @ $ % *].");
            
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

        /*
        if (ViewState["EmailId"] == null)
        {
            Utility.showMessage(this, "Please Enter Email ID !");
            //lblMsg.Text = "Please Enter Text Again Shown in Code!";
            txtEmailID.Focus();
            return;
        }*/

        //if (txtEmailID.Text.Trim() != "" && ViewState["isVerifyEmail"] == null)
        //{
        //    Utility.showMessage(this, "Please Verify Email ID !");
        //    //lblMsg.Text = "Please Enter Text Again Shown in Code!";
        //    EtxtOTP.Focus();
        //    return;
        //}
        //if (txtEmailID.Text.Trim() != "" && Convert.ToString(ViewState["isVerifyEmail"]) != "Y")
        //{
        //    Utility.showMessage(this, "Please Verify Email ID!");
        //    //lblMsg.Text = "Please Enter Text Again Shown in Code!";
        //    EtxtOTP.Focus();
        //    return;
        //}

        try
        {
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            DataTable dtm = cls.GetDataTable("select count(1) from UserLogin  where MobileNo=@MobileNo and UserID=@UserID", new SqlParameter[] { new SqlParameter("@MobileNo", Convert.ToString(txtMobileNo.Text).Trim()), new SqlParameter("@UserID", Convert.ToString(Session["UserID"])) });

            if (dtm.Rows.Count > 0)
            {
                if (dtm.Rows[0][0].ToString() != "0")
                {
                    Utility.showMessage(this, "This Mobile Number Is Already Register, Please Try Again!");
                    txtMobileNo.Focus();
                    txtMobileNo.Enabled = true;
                    return;

                }
            }

            DataTable dte = cls.GetDataTable("select count(1) from UserLogin where EmailId=@EmailId and UserID=@UserID", new SqlParameter[] { new SqlParameter("@EmailId", Convert.ToString(txtEmailID.Text).Trim()), new SqlParameter("@UserID", Convert.ToString(Session["UserID"])) });
            if (dte.Rows.Count > 0)
            {
                if (dte.Rows[0][0].ToString() != "0")
                {
                    Utility.showMessage(this, "This Email ID Is Already Register, Please Try Again!");
                    txtEmailID.Focus();
                    return;
                }
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


                string strQuery = "UPDATE UserLogin SET Name=@Name, Mobile=@Mobile, Email=@Email, Password=@Password,PwdUpdatedDate=GETDATE(), IsPassChange='Y',IsStrong='Y', Block_Code=@Block_Code where UserID=@UserID";

                SqlParameter Name = new SqlParameter("@Name", txtName.Text.Trim() );
                SqlParameter Mobile = new SqlParameter("@Mobile", txtMobileNo.Text.Trim());
                SqlParameter Email = new SqlParameter("@Email", txtEmailID.Text.Trim());

                SqlParameter UserID = new SqlParameter("@UserID", Convert.ToString(Session["UserID"]));              
                SqlParameter Password = new SqlParameter("@Password", enc.Encrypt(txtNewPwd.Text));
                SqlParameter Block_Code = new SqlParameter("@Block_Code", ddlBlock.SelectedValue != null ? ddlBlock.SelectedValue.Trim():null);

                if (cls.ExecuteSql(strQuery, new SqlParameter[] { Name, Mobile, Email, UserID, Password, Block_Code }) > 0)
                {
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.Abandon();
                    Utility.showMessageNavigate(this, "Password Changed Successfully. Please Re-login With Your New Password.", "Login_Default.aspx");

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
            
            message = "OTP is "+ ViewState["MOTP"].ToString().Trim()+" sent on your registered mobile number " + mob + " -  Home Dept. ,Bihar Government";
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
            SMSHttpPostClient smsSend = new SMSHttpPostClient();
            string mobile = mob;
            //string mobile = "9504245424";
           // SMSReferenceWithID.SMSWebService smsSend = new SMSReferenceWithID.SMSWebService();
           
             //send = smsSend.sendSMSSingle(message, mobile, "BIHAREDISTRICT-policeerss", "erss@123456", "bac4e6cd-ac88-43bd-86f2-aa0d04171e79", "LRD12345SMS", "1307166261966926791");
            send= smsSend.sendSingleSMS("BIHAREDISTRICT-policeerss", "erss@123456", "BRGOVT", mobile, message, "bac4e6cd-ac88-43bd-86f2-aa0d04171e79", "1307166261966926791");

            //SMSHttpPostClient objdata= new SMSHttpPostClient(); //new implement
            ////String username, String password, String senderid, String mobileNo, String message, String secureKey, String templateid
            //send = objdata.sendUnicodeSMS("BIHAREDISTRICT-building", "abc@123456789", "1d7068a4-32b2-42f1-991a-f710f4051bfb", message, mobile, "BRGOVT", "1307169631740739971");//new implement


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
    //protected void EGetOtpBtn_Click(object sender, System.EventArgs e)
    //{
    //    if (txtEmailID.Text == "")
    //    {
    //        Utility.showMessage(this, "Please Enter Email ID.");
    //        return;
    //    }

    //    string EOTP = generatePassword();
    //    ViewState["EOTP"] = EOTP;
    //    ViewState["EmailId"] = txtEmailID.Text.Trim();
    //    try
    //    {
    //        sendOTPEMAIL(ViewState["EmailId"].ToString());
    //    }
    //    catch
    //    {
    //    }
    //    txtEmailID.Enabled = false;
    //    //EpnlVerify.Visible = true;
    //    //EGetOtpBtn.Visible = false;
    //    //EpnlEdit.Visible = false;
    //}
    //protected void ElnkResend_Click(object sender, System.EventArgs e)
    //{
    //    try
    //    {
    //        sendOTPEMAIL(ViewState["EmailId"].ToString());
    //    }
    //    catch
    //    {
    //    }
    //   // ElnkResend.Visible = false;
    //}

    //protected void sendOTPEMAIL(string email)
    //{
    //    string Msg = string.Empty;
    //    string otp = "";
    //    string message = "";
    //    if (ViewState["EOTP"].ToString() != "")
    //    {
    //        message = "OTP: " + ViewState["EOTP"].ToString().Trim() + ", Please do not share for Security purpose, Home Department, Govt. Of Bihar";
    //    }
    //    else
    //    {
    //        txtEmailID.Enabled = false;
    //        EpnlVerify.Visible = true;
    //        //EGetOtpBtn.Visible = false;
    //        EpnlEdit.Visible = false;
    //    }

       
    //    string message1 = "To<br/> Shri / Smt / Ms '" + txtName.Text + "'  <br />  OTP: " + ViewState["EOTP"].ToString() + ".<br /> Please Enter This OTP Before Finalise Only For First Time Secure Login Step In This Application. Govt. of Bihar";
    //    message1 = message1 + "This is a system generated email, please do not revert back.<br/>";
    //    string Email = email.Trim();
    //    string res = eclient.SendMessage("support.dgrc-bih@gov.in", "G6%vR0&vV6", Email.Trim(), "Regarding Validate Email ID", message, "nicmail$2018#");
        
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('OTP Has Been Send On Registered Email ID.')", true);
    //}
    //protected void EbtnVerify_Click(object sender, System.EventArgs e)
    //{
    //    if (Convert.ToString(ViewState["EOTP"]).Trim() == EtxtOTP.Text.Trim())
    //    {
    //     //   ViewState["isVerifyEmail"] = "Y";
    //        txtEmailID.Text = Convert.ToString(ViewState["EmailId"]);
    //        //EVerifyStatus.Text = "<i class='fa fa-x fa-check-circle'></i> Verified";
    //        //EpnlEdit.Visible = true;
    //        //EpnlVerify.Visible = false;
    //        txtEmailID.Enabled = false;
    //        txtEmailID.BackColor = System.Drawing.Color.Green;
    //        txtEmailID.ForeColor = System.Drawing.Color.White;
    //       // EGetOtpBtn.Visible = false;

    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('OTP is wrong , Please Try Again !');", true);
    //        return;
    //    }

    //}

    protected void btnClose_Click(object sender, System.EventArgs e) 
    {
        Session.Clear();
        Session.Abandon();
        Response.Redirect("~/DeptDefault.aspx");    
    }

}