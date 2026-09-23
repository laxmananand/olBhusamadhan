using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class LandDispute_Entry_Unfinalize : System.Web.UI.Page
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
        if (Session["Role"] != null && !string.IsNullOrEmpty(Session["Role"].ToString()))
            {
            if (Session["Role"].ToString() == "SHOOPT")
            {
                if (Session["District_Code"] != null)
                {
                    if (!IsPostBack)
                    {
                        Session["SendEmailOTP_1"] = "0";
                        Session["SendMobileOTP_1"] = "0";
                        Session["MobileVerified"] = "N";
                        lbtnEdit.Enabled = false;
                        if (Request.QueryString["dst"] != null)
                        {
                            ViewState["DistCode"] = null;
                            ViewState["DistCode"] = Request.QueryString["dst"].ToString();
                        }
                        ShowUserDetails();
                        bindSubDivision();
                        bindBlock();
                        LoadgvShowAddBlockDetails();
                    }
                }
                else
                {
                    Response.Redirect("~/DeptDefault.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("~/DeptDefault.aspx");
        }
    }

    protected void lnkSendOTPMob_Click(object sender, EventArgs e)
    {
        if (Session["SendMobileOTP_1"].ToString() == "0")
        {
            if (txtAuthorizedPersonMobileNo.Text.Length != 10)
            {
                Utility.showMessage(this, "Only one mobile number is allowed for OTP");
                return;
            }

            Session["MobOTP"] = "";
            Session["Mob"] = txtAuthorizedPersonMobileNo.Text.Trim();
            Session["timeDataMobile"] = "0";
            Session["MainTimeMobile"] = "180000";
            Timer2.Enabled = true;
            SendOTPOnMobile();
        }
        else
        {
            Utility.showMessage(this, "Please wait before regenerating new Mobile OTP");
            return;
        }
    }

    protected void SendOTPOnMobile()
    {
        if (Session["SendMobileOTP_1"].ToString() != "0")
            return;

        if (txtAuthorizedPersonMobileNo.Text.Length == 10)
        {
            string querystring = @"select count(*) from UserLogin where Mobile = @Mobile";
            DataTable dt = clsData.GetDataTable(querystring, new SqlParameter[] {
            new SqlParameter("@Mobile", txtAuthorizedPersonMobileNo.Text.ToString()) });
            if (dt != null && dt.Rows.Count > 0)
            {
                int userCount = Convert.ToInt32(dt.Rows[0][0]);
                if (userCount > 0)
                {
                    Session["timeDataMobile"] = "0";
                    Session["MainTimeMobile"] = "0";
                    Timer2.Enabled = false;
                    lblMobileTime.Text = "";
                    Utility.showMessage(this, "This mobile number is already registered, please try another mobile number.");
                    return;
                }
            }

            string OTP_MobileNo = generateOTP_Mobile();
            //if (OTP_MobileNo == "0")
            //{
            //    MsgUtility.showMessage(this, "Please wait for 3 minutes before re-generating Mobile OTP");
            //    return;
            //}
            try
            {
                Session["Mobile"] = txtAuthorizedPersonMobileNo.Text;
                if (Session["MobOTP"].ToString() == null || Session["MobOTP"].ToString() == "")
                {
                    //Session["MobOTP"] = OTP_MobileNo;
                    Session["MobOTP"] = "1234";
                }
                txtMobOTP.Visible = true;
                lnkSendOTPMob.Visible = false;
                divMob.Visible = true;
                SMSReferenceWithID.SMSWebService sendSMS = new SMSReferenceWithID.SMSWebService();
                string mobile = Session["Mobile"].ToString();
                //string message = "OTP is " + Session["MobOTP"].ToString() + " sent on your registered mobile number " + mobile + ", Education Dept. -Bihar Government";
                string message = "OTP: " + Session["MobOTP"].ToString() + ". Please Enter This OTP Before Submit Your Application Form. Health Department - Bihar Government";

                //String res = sendSMS.sendSMSSingle(message, mobile, "BIHAREDISTRICT-panchayatiraj", "Prd@sms2007", "11c0d4a5-5425-4ca6-b46b-1d162ee9cc2d", "LRD12345SMS", "1307168741328534278");

                Session["SendMobileOTP_1"] = "1";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('OTP Has Been Sent On Registered Mobile No..')", true);
                Session["MobOTPSendOn"] = System.DateTime.Now.ToString();

            }
            catch (Exception ex)
            { }
        }
        else
        {           
        }
    }

    private string generateOTP_Mobile()
    {
        try
        {
            int lenthofpass = 4;
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

    protected void linkMobResendOTP_Click(object sender, EventArgs e)
    {
        if (Session["SendMobileOTP_1"].ToString() == "0")
        {
            SendOTPOnMobile();
            Timer2.Enabled = true;
            linkMobResendOTP.Visible = false;
        }
        else
        {
            Utility.showMessage(this, "Please wait before regenerating new Mobile OTP");
            return;
        }
    }

    protected void Timer2_Tick(object sender, EventArgs e)
    {
        if (Session["SendMobileOTP_1"] != null)
        {
            double timeInterval = 120000 - Convert.ToInt64(Session["SendMobileOTP_1"].ToString());
            TimeSpan t = TimeSpan.FromMilliseconds(timeInterval);
            string answer = string.Format(" {0:D2} min:{1:D2} Sec", t.Minutes, t.Seconds);
            lblMobileTime.Text = "Resend OTP in:" + answer;
            Session["SendMobileOTP_1"] = Convert.ToInt64(Session["SendMobileOTP_1"].ToString()) + 1000;
            if (t.Minutes <= 0 && t.Seconds <= 0)
            {
                Session["SendMobileOTP_1"] = "0";
                Timer2.Enabled = false;
                linkMobResendOTP.Visible = true;
                lblMobileTime.Text = "";
            }
        }
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        Msg = CheckValidation();
        if (Msg == "")
        {
            try
            {
                SqlParameter _UserId = new SqlParameter("@Name", txtName.Text.Trim());
                SqlParameter _Email = new SqlParameter("@Email", txtEmail.Text.Trim());
                SqlParameter _Ps = new SqlParameter("@PoliceStation", txtPolice_Station.Text.Trim());
                SqlParameter _Mobile = new SqlParameter("@Mobile", txtAuthorizedPersonMobileNo.Text.Trim());              
                SqlParameter _PsCode = new SqlParameter("@ThanaCode", Session["Thana_Code"]);              
                DataTable dt = clsData.GetDataTableWithProc("Sp_SaveEditUserDetails", new SqlParameter[] { _UserId, _Email, _Ps, _Mobile , _PsCode });
                if (dt.Rows.Count > 0)
                {
                    Utility.showMessage(this, "User has been updated Successfully.");
                    return;
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

    public string CheckValidation()
    {
        string s = "";

        if (string.IsNullOrEmpty(txtAuthorizedPersonMobileNo.Text.ToString()))
        {
            s = "Please enter mobile number";
            Clear();
            return s;
        }
        if (txtAuthorizedPersonMobileNo.Text.Length != 10)
        {
            s = "Please enter 10-digit mobile number";
            Clear();
            return s;
        }
     
        if (txtAuthorizedPersonMobileNo.Text != Session["Mob"].ToString())
        {

            s = "Please check your Mobile Number";
            Clear();
            return s;
        }
       
        if (Session["MobileVerified"].ToString() != "Y")
        {
            s = "Mobile No Not verified";
            Clear();
            return s;
        }
        return s;

    }

    public void Clear()
    {
        txtEmail.Text = "";
        txtName.Text = "";
        txtUserName.Text = "";
        txtAuthorizedPersonMobileNo.Text = "";
    }

    protected void lnkVerifyMobOTP_Click(object sender, EventArgs e)
    {
        try
        {
            Session["MobileVerified"] = "N";
            if (txtMobOTP.Text.Trim() == "")
            {
                Utility.showMessage(this, "Please Enter Right OTP Send On Registered Mobile No..");
                return;
            }
            else
            {
                Session["VelidateMobOTP"] = txtMobOTP.Text.Trim();
                if (Convert.ToInt32(Session["MobOTP"].ToString().Trim()) == Convert.ToInt32(Session["VelidateMobOTP"].ToString().Trim()))
                {
                    DateTime OtpCrtDate = Convert.ToDateTime(Session["MobOTPSendOn"].ToString());
                    TimeSpan timeSub = DateTime.Now - OtpCrtDate;
                    if (timeSub.TotalMinutes < 5)
                    {
                        txtAuthorizedPersonMobileNo.Enabled = false;
                        txtMobOTP.Enabled = false;
                        lnkSendOTPMob.Visible = true;
                        lnkSendOTPMob.Text = "Verified";
                        lnkSendOTPMob.BackColor = System.Drawing.Color.Green;
                        lnkSendOTPMob.ForeColor = System.Drawing.Color.White;
                        lnkSendOTPMob.Enabled = false;
                        divMob.Visible = false;
                        Timer2.Enabled = false;
                        lblMobileTime.Text = "";
                        Utility.showMessage(this, "OTP Send On Your Registered Mobile No. Has Been Verified Successfully.");
                        Session["MobileVerified"] = "Y";
                        lbtnEdit.Enabled = true;
                    }
                    else
                    {
                        Session["MobOTP"] = "";
                        Session["VelidateMobOTP"] = "";
                        Session["MobOTPSendOn"] = "";
                        Utility.showMessage(this, "Sorry but your Mobile OTP Verification Timeout. Get a new one");
                        return;
                    }
                }
                else
                {
                    Utility.showMessage(this, "Sorry, Your Mobile OTP is Invalid. Try again, please.");
                    return;
                }
            }

        }
        catch (Exception ex)
        { }
    }

    private void ShowUserDetails()
    {
        try
        {
            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", Session["Thana_Code"]);
            String Querystring = @"
                select UserName, Name, Email, Mobile,m.Police_Station from UserLogin u
                left join mst_thana m on u.District_Code=u.District_Code and m.PS_Code=u.Thana_Code
                where u.Thana_Code=@ThanaCode"; 
            DataTable dt = clsData.GetDataTable(Querystring, new SqlParameter[] {  _ThanaCode });
            if (dt.Rows.Count > 0) 
            {
                txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAuthorizedPersonMobileNo.Text = dt.Rows[0]["Mobile"].ToString();
                txtPolice_Station.Text = dt.Rows[0]["Police_Station"].ToString();
            }
        }
        catch(Exception ex)
        {

        }
    }
    private void bindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        SqlParameter _District = new SqlParameter("@District", Session["District_Code"]);
        String Querystring = @"select Sd_Code2, Sd_Name_Hn, Sd_Name_En  from SubDivisions where DistCode = @District order by Sd_Name_En";
        DataTable dt = clsData.GetDataTable(Querystring, new SqlParameter[] { _District });
        if (dt.Rows.Count > 0)
        {
            ddlSubDivision.DataSource = dt;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        else
        {
            ddlSubDivision.DataSource = null;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("-Select-", "0"));
        }
    }

    private void bindBlock()
    {
        ddlBlock.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, @SubDivision });
        if (dt.Rows.Count > 0)
        {
            ddlBlock.DataSource = dt;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("-Select-", "0"));
        }
        else
        {
            ddlBlock.DataSource = null;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("-Select-", "0"));
        }

    }

    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindBlock();
    }
    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBlock.SelectedIndex == 0)
            {
                Label1.Text = "Select Block";
                return;
            }
            if (ddlSubDivision.SelectedIndex == 0)
            {
                Label1.Text = "Select Sub Division";
                return;
            }

            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", Session["Thana_Code"]);
            SqlParameter _Block = new SqlParameter("@Block", ddlBlock.SelectedValue);
            SqlParameter _subdivision = new SqlParameter("@Subdivision", ddlSubDivision.SelectedValue);          
            DataTable dt = clsData.GetDataTableWithProc("InsertOrUpdateThanaMappingBlock", new SqlParameter[] { _ThanaCode, _Block , _subdivision });
            if (dt != null && dt.Rows.Count > 0)
            {
                int BlkCount = Convert.ToInt32(dt.Rows[0][0]);
                if (BlkCount > 0)
                {
                    ddlBlock.SelectedIndex = 0;
                    ddlSubDivision.SelectedIndex = 0;
                    Utility.showMessage(this, "Block has been Added Successfully.");
                }
                else
                {
                    ddlBlock.SelectedIndex = 0;
                    ddlSubDivision.SelectedIndex = 0;
                    Utility.showMessage(this, "Block has been found in Database.");                   
                }
            }
            
            LoadgvShowAddBlockDetails();
        }
        catch (Exception ex)
        {

        }
    }

    private void LoadgvShowAddBlockDetails()
    {    
        string Query = @"  select  b.BlockName as BlockName,b.Sd_Name_En as Subdivision_Name from MstThanaMapping t
                    inner join(
                    select SubDivCode,BlockCode,BlockName,s.Sd_Name_En from Blocks b
                    inner join SubDivisions s on s.Sd_Code2=b.SubDivCode
                    ) b on  b.BlockCode=t.Code and t.Type='Block' and t.Thana_Code='" + Session["Thana_Code"] .ToString()+ "'; ";
        DataTable dt = clsData.GetDataTable(Query, new SqlParameter[] { });
        if (dt.Rows.Count > 0)
        {          
                gvShowAddBlockDetails.DataSource = dt;
                gvShowAddBlockDetails.DataBind();         
        }
        else
        {
            //Label1.Text = "Data has been found in Database";
        }
    }

    protected void gvShowAddBlockDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShowAddBlockDetails.PageIndex = e.NewPageIndex;
        LoadgvShowAddBlockDetails();
    }

}