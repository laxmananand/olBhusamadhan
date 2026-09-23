using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.IO;

public partial class LandDispute_THANA_Thana_Entry : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    static string key = Encryptor.PrivateKey.ToString();
    Encryptor enc = new Encryptor(key);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null)
        {
            if (Session["Role"].ToString() == "SHOOPT")
            {
                if (Session["District_Code"] != null)
                {

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

        if (!IsPostBack)
        {
            bindCommissionary();
            bindDistrict();
            bindSubDivision();
            bindBlock();
            ViewState["PS_Code"] = "";
        }
    }
    private void bindCommissionary()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        DataTable dt = clsData.GetDataTableWithProc("SP_thanaEntry", new SqlParameter[] { QueryType });
        if (dt.Rows.Count > 0)
        {
            ddlCommissionary.DataSource = dt;
            ddlCommissionary.DataTextField = "DIVISIONAME";
            ddlCommissionary.DataValueField = "DIVISIONCODE";
            ddlCommissionary.DataBind();
            ddlCommissionary.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddlCommissionary.DataSource = null;
            ddlCommissionary.DataTextField = "";
            ddlCommissionary.DataValueField = "";
            ddlCommissionary.DataBind();
            ddlCommissionary.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void bindDistrict()
    {
        ddlDistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("2"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_thanaEntry", new SqlParameter[] { QueryType, CommissionaryCode });
        if (dt.Rows.Count > 0)
        {
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddlDistrict.DataSource = null;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
        }


    }
    private void bindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_thanaEntry", new SqlParameter[] { QueryType, District });
        if (dt.Rows.Count > 0)
        {
            ddlSubDivision.DataSource = dt;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("--select--", "0"));
        }
        else
        {
            ddlSubDivision.DataSource = null;
            ddlSubDivision.DataTextField = "";
            ddlSubDivision.DataValueField = "";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void bindBlock()
    {
        ddlBlock.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_thanaEntry", new SqlParameter[] { QueryType, SubDivision });
        if (dt.Rows.Count > 0)
        {
            ddlBlock.DataSource = dt;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        else
        {
            ddlBlock.DataSource = null;
            ddlBlock.DataTextField = "";
            ddlBlock.DataValueField = "";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("--Select--", "0"));
        }

    }

    protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDistrict();
        bindSubDivision();
        bindBlock();
    }

    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubDivision();
        bindBlock();
    }

    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindBlock();
    }

    public void insertThana()
    {
        string sqlQuery = @"select max(ps_code) as ps_code  from mst_thana where District_code='" + ddlDistrict.SelectedValue+"'";
        DataTable dt_thana = clsData.GetDataTable(sqlQuery);
        if (dt_thana.Rows.Count > 0)
        {
            ViewState["PS_Code"] = dt_thana.Rows[0]["ps_code"].ToString();
            int newPs_code = (Convert.ToInt32(ViewState["PS_Code"].ToString()) + 1);
            SqlParameter _QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
            SqlParameter _DistrictName = new SqlParameter("@DistrictName", ddlDistrict.SelectedItem.Text.ToString());
            SqlParameter _District = new SqlParameter("@District", ddlDistrict.SelectedValue);
            SqlParameter _Subdivision_Name = new SqlParameter("@Subdivision_Name", ddlSubDivision.SelectedItem.Text.ToString());
            SqlParameter _SubDivision = new SqlParameter("@SubDivision", ddlSubDivision.SelectedValue);
            SqlParameter _Police_Station = new SqlParameter("@Police_Station", txtThanaName.Text);
            SqlParameter _PS_Code = new SqlParameter("@PS_Code", newPs_code);
            SqlParameter _CircleName = new SqlParameter("@CircleName", ddlBlock.SelectedItem.Text.ToString());
            SqlParameter _CircleCode = new SqlParameter("@CircleCode", ddlBlock.SelectedValue);
            DataTable dt = clsData.GetDataTableWithProc("SP_thanaEntry", new SqlParameter[] { _QueryType, _DistrictName, _District,
                _Subdivision_Name, _SubDivision,_Police_Station,_PS_Code,_CircleName,_CircleCode});
            if (dt.Rows.Count>0)
            {
                DataTable dtUser = insertUser();
                if (dt.Rows[0]["Result"].ToString().Equals("1")&& dtUser.Rows[0]["Result"].ToString().Equals("1"))
                {
                    ScriptManager.RegisterStartupScript(Page, GetType(), "", "alert('Record Save Successfully');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "", "alert('Record Not Save Successfully');", true);
                return;
            }
        }  
    }

    public DataTable insertUser()
    {
        string key = Encryptor.PrivateKey.ToString();
        Encryptor enc = new Encryptor(key);
        string PWD = enc.Encrypt("1234");
        int newPs_code = Convert.ToInt32(ViewState["PS_Code"].ToString()) + 1;
        SqlParameter _QueryType = new SqlParameter("@QueryType", Convert.ToInt32("6"));
        SqlParameter _USER_ID = new SqlParameter("@USER_ID", ("SHOOPT" + newPs_code.ToString()));
        SqlParameter _USER_NAME = new SqlParameter("@USER_NAME",("SHOOPT"+" " +txtThanaName.Text));
        SqlParameter _Password = new SqlParameter("@Password", PWD);
        SqlParameter _PS_Code = new SqlParameter("@PS_Code", newPs_code);
        SqlParameter _CircleCode = new SqlParameter("@CircleCode", ddlBlock.SelectedValue);
        SqlParameter _District = new SqlParameter("@District", ddlDistrict.SelectedValue);
        SqlParameter _Userrole = new SqlParameter("@Userrole", "SHOOPT");
        SqlParameter _SubDivision = new SqlParameter("@SubDivision", ddlSubDivision.SelectedValue);
        SqlParameter _CommissionaryCode = new SqlParameter("@CommissionaryCode", ddlCommissionary.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("SP_thanaEntry", new SqlParameter[] { _QueryType, _USER_ID,
            _USER_NAME,  _Password, _PS_Code,_CircleCode,_District,_Userrole,_SubDivision,_CommissionaryCode});
        return dt;
    }
    public void validation()
    {
        if (ddlCommissionary.SelectedValue == "0")
        {
            Utility.showMessage(this, "Please select Commissionary Name ");
            ddlCommissionary.Focus();
            return;
        }
        if (ddlDistrict.SelectedValue == "0")
        {
            Utility.showMessage(this, "Please select District Name ");
            ddlDistrict.Focus();
            return;
        }
        if (ddlSubDivision.SelectedValue == "0")
        {
            Utility.showMessage(this, "Please select Sub-Division Name ");
            ddlSubDivision.Focus();
            return;
        }
        if (ddlBlock.SelectedValue == "0")
        {
            Utility.showMessage(this, "Please select Block Name ");
            ddlBlock.Focus();
            return;
        }
        if (txtThanaName.Text.Trim() == "")
        {
            Utility.showMessage(this, "Please Enter Thana Name ");
            txtThanaName.Focus();
            return;
        }
    }

    public void clear_control(object sender, EventArgs e)
    {
        ddlCommissionary.SelectedValue = "0";
        ddlCommissionary_SelectedIndexChanged(sender, e);
        txtThanaName.Text = "";
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        validation();
        insertThana();
        clear_control(sender,e);
    }
}