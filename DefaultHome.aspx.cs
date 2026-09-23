using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RaC_DefaultHome : System.Web.UI.Page
{
    clsDataAccessLandDispute cls = new clsDataAccessLandDispute();
    string DistrictCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Session["Role"]!=null)
            {
                if (Session["Role"].ToString() == "SHOOPT")
                {
                    TotalApplication1.Visible = true;
                    TotalApplication2.Visible = false;

                    Finalize1.Visible = true;
                    Finalize2.Visible = false;

                    UnFinalize1.Visible = true;
                    UnFinalize2.Visible = false;
                }
                else
                {
                    TotalApplication1.Visible = false;
                    TotalApplication2.Visible = true;

                    Finalize1.Visible = false;
                    Finalize2.Visible = true;

                    UnFinalize1.Visible = false;
                    UnFinalize2.Visible = true;
                }
                getdata();

            }
            else
            {
                Response.Redirect("Login_Default.aspx");
            }
        }
    }
    protected void getdata()
    {
        try
        {

            SqlParameter QueryType = new SqlParameter("@QuertType", "HQ");
            SqlParameter _RangeCode = new SqlParameter("@RangeCode", Session["RangeCode"] != null ? Session["RangeCode"].ToString() : "0");
            SqlParameter _CommissionaryCode = new SqlParameter("@DivisionCode", Session["Commsionary_Code"] != null? Session["Commsionary_Code"].ToString():"0");
            SqlParameter _DistCode = new SqlParameter("@DISTRICTCODE", Session["District_Code"] != null ? Session["District_Code"].ToString() : "0");
            SqlParameter _SubDivision = new SqlParameter("@SubDivisionCode", Session["Sub_DivCode"] != null ? Session["Sub_DivCode"].ToString() : "0");
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", Session["Block_Code"] != null ? Session["Block_Code"].ToString() : "0");
            SqlParameter _pscode = new SqlParameter("@ThanaCode", Session["Thana_Code"] != null ? Session["Thana_Code"].ToString() : "0");
            
            DataTable dt = cls.GetDataTableWithProc("sp_GetDasboardData_ForDistrictWiseNew", new SqlParameter[] { QueryType, _RangeCode, _CommissionaryCode, _DistCode, _SubDivision, _Blockcode, _pscode });
            //SqlParameter QuertType = new SqlParameter("@QuertType", "HQ");
            //DataTable dt = cls.GetDataTableWithProc("sp_GetDasboardData_ForDistrictWise", new SqlParameter[] { QuertType });
            if (dt.Rows.Count > 0)
            {
                lbltotalapplication1.Text = dt.Rows[0]["Total"].ToString();
                lbltotalapplication2.Text = dt.Rows[0]["Total"].ToString();

                lblFinalize1.Text = dt.Rows[0]["Finalize"].ToString();
                lblFinalize2.Text = dt.Rows[0]["Finalize"].ToString();

                lblFinalize1.Text = dt.Rows[0]["Finalize"].ToString();
                lblFinalize2.Text = dt.Rows[0]["Finalize"].ToString();

                lblUnFinalize1.Text = dt.Rows[0]["Unfinalize"].ToString();
                lblUnFinalize2.Text = dt.Rows[0]["Unfinalize"].ToString();

                lblatiSavedansheel.Text = dt.Rows[0]["atisanvedanasheel"].ToString();
                lblsavedansheel.Text = dt.Rows[0]["sanvedanasheel"].ToString();
                lblsamanya.Text = dt.Rows[0]["saamaany"].ToString();

                lblnispadan.Text = dt.Rows[0]["Nirast"].ToString();
                lblFinaldisposal.Text = dt.Rows[0]["FinalNirast"].ToString();
                lblprakreeyadheen.Text = dt.Rows[0]["Prakriyadhin"].ToString();
                lblmapikenirdharit.Text = dt.Rows[0]["Mapi_Nirdharit"].ToString();
                lblashvikrit.Text = dt.Rows[0]["Ashwikrit"].ToString();
                lblNaylayNilambit.Text = dt.Rows[0]["NayalayNilambit"].ToString();
            }
            else
            {
                lbltotalapplication1.Text = "0";
                lbltotalapplication2.Text = "0";
                lblatiSavedansheel.Text = "0";
                lblsavedansheel.Text = "0";
                lblsamanya.Text = "0";

                lblnispadan.Text = "0";
                lblprakreeyadheen.Text = "0";
                lblmapikenirdharit.Text = "0";
                lblashvikrit.Text = "0";
                lblNaylayNilambit.Text= "0";
            }
        }
        catch (Exception ee)
        {


        }
    }
}