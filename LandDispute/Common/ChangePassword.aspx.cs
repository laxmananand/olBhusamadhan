using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class LandDispute_Common_ChangePassword : System.Web.UI.Page
{    
    private Random random = new Random();
    clsDataAccessLandDispute cls = new clsDataAccessLandDispute();
    
    NICMailService.NICMailService eclient = new NICMailService.NICMailService();
    static string key = Encryptor.PrivateKey.ToString();
    Encryptor enc = new Encryptor(key);
    ValidateInput valInp = new ValidateInput();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../DeptDefault.aspx");
        }
        if (!IsPostBack)
        {

            bindUserData();
        }
    }
    private void bindUserData()
    {
        string sqlQuery = @"  select UserID, UserName,Password,Mobile,Created_date,PwdUpdatedDate,Thana_Code,Block_Code,District_Code,Userrole,Islock,PwdResetBy,PwdResetDate,StateCode,IMEI, RangeCode,Sub_DivCode,Commsionary_Code,IsPassChange from UserLogin where UserID=@UserID";
        DataTable dt = cls.GetDataTable(sqlQuery, new SqlParameter[] { new SqlParameter("@UserID", Convert.ToString(Session["UserID"])) });
        
        txtName.Text = Session["UserName"].ToString();
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["Mobile"].ToString().Trim() != "")
            {
                txtMobileNo.Text = dt.Rows[0]["Mobile"].ToString().Trim();

            }
            else
            {
                txtMobileNo.Text = "";
                txtMobileNo.Enabled = true;
            }


        }
        else
        {
            txtMobileNo.Text = "";
            txtMobileNo.Enabled = true;
        }
        txtName.Enabled = false;
        txtMobileNo.Enabled = true;
        //
    }
    private string GenerateRandomCode()
    {
        string s = "";
        for (int i = 0; i < 3; i++)
            s = String.Concat(s, this.random.Next(10).ToString());
        return s;
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
        if (txtMobileNo.Text.Trim().Length == 10)
        {
            if (!Regex.IsMatch(txtMobileNo.Text.Trim(), @"^[6-9]{1}[0-9]{9}$"))
            {
                txtMobileNo.Focus();
                Utility.showMessage(this, "Please Enter 10 Digit Mobile Number.");
                return;
            }
        }
        else
        {
            txtMobileNo.Focus();
            Utility.showMessage(this, "Please Enter valid Mobile Number.");
            return;
        }
        if (txtNewPwd.Text.Trim().Length == 0)
        {
            Utility.showMessage(this, "Please enter New Password");
            return;
        }
        else if (txtRePwd.Text.Trim().Length == 0)
        {
            Utility.showMessage(this, "Please enter Re-enter Confirm Password");
            return;
        }
        
        else if (txtNewPwd.Text.Trim() == txtRePwd.Text.Trim())
        {

            string pwd = enc.Encrypt(txtNewPwd.Text.Trim());


           SqlParameter _UserId = new SqlParameter("@UserId", Session["UserID"].ToString());
            SqlParameter _Password = new SqlParameter("@Password", pwd.ToString());
            SqlParameter _PasswordResetBy = new SqlParameter("@PwdResetBy", Session["UserId"].ToString());
            SqlParameter _PasswordResetDate = new SqlParameter("@PwdUpdatedDate", DateTime.Now);
            SqlParameter _MobileNo = new SqlParameter("@Mobile", txtMobileNo.Text.Trim());

            string sql = @"UPDATE UserLogin SET Password=@Password,Mobile=@Mobile,PwdUpdatedDate=GETDATE(),PwdResetBy=@PwdResetBy,IsPassChange='Y' where UserID=@UserId";

            int x = cls.ExecuteSql(sql, new SqlParameter[] {  _UserId, _Password,_MobileNo, _PasswordResetBy, _PasswordResetDate });
            if (x > 0)
            {
                Utility.showMessageNavigate(this, "Password changed successfully", "../../DeptDefault.aspx");

              

            }
            else
            {


                ScriptManager.RegisterStartupScript(Page, GetType(), "Info", "alert('Password is not Updated');", true);
                return;
              
            }
        }
        else
        {
            Utility.showMessage(this, "Password  & Confirm is not Same");
            return;
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
}