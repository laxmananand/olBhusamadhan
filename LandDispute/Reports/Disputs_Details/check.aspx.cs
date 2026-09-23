using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using EO.Web.Internal;

public partial class LandDispute_Reports_Disputs_Details_DisputeConsolidateBlockRpt : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    Encryptor enc = new Encryptor(Encryptor.PrivateKey);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] != null || Session["Role"].ToString() != "")
        {
            if (!IsPostBack)
            {
                BindRole();
                //bindDivision();
                ViewState["PageIndex"] = "";
                ViewState["total"] = "";
                ViewState["DistrictCode"] = "";
                ViewState["SubDivisionCode"] = "";
                ViewState["BlockCode"] = "";
                ViewState["ThanaCode"] = "";
                ViewState["PageIndex"] = "";
                ViewState["BhumiVivadType"] = "0";
            }
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            ViewState.Clear();
           
            Response.Redirect("~/Login_Default.aspx.aspx");
        }
    }
    private void bindbaithak()
    {
        SqlParameter GetQuery = new SqlParameter("@QueryType", "8");
        DataTable dt = clsData.GetDataTableWithProc("stp_GetDisputeDistConsolidateRpt", new SqlParameter[] { GetQuery });
        if (dt.Rows.Count > 0)
        {
            ddlbaithak.DataSource = dt;
            ddlbaithak.DataTextField = "Description";
            ddlbaithak.DataValueField = "StatusCode";
            ddlbaithak.DataBind();
            ddlbaithak.Items.Insert(0, new ListItem("मापी क़े लिए निर्धारित+प्रक्रियाधीन", "7"));
            ddlbaithak.Items.Insert(0, new ListItem("All", "0"));
            ddlbaithak.SelectedValue = "7";
        }
        else
        {
            ddlbaithak.DataSource = null;
            ddlbaithak.DataTextField = "Description";
            ddlbaithak.DataValueField = "StatusCode";
            ddlbaithak.DataBind();
            //ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    //Bind Role
    private void BindRole()
    {
        bindCommissionary();
        if (Session["Commsionary_Code"] != null)
        {
            if (Session["Commsionary_Code"].ToString() != "")
            {
                ddlCommissionary.SelectedValue = Session["Commsionary_Code"].ToString();
                ddlCommissionary.Enabled = false;
            }
        }
        bindDistrict();
        if (Session["District_Code"] != null)
        {
            if (Session["District_Code"].ToString() != "")
            {
                ddlDistrict.SelectedValue = Session["District_Code"].ToString();
                ddlDistrict.Enabled = false;
            }
        }
        bindSubDivision();
        if (Session["Sub_DivCode"] != null)
        {
            if (Session["Sub_DivCode"].ToString() != "")
            {
                ddlSubDivision.SelectedValue = Session["Sub_DivCode"].ToString();
                if(Session["Thana_Code"].ToString()=="0")
                {
                    ddlSubDivision.Enabled = false;
                }
               
            }
        }
        bindBlock();
        if (Session["Block_Code"] != null)
        {
            if (Session["Block_Code"].ToString() != "")
            {
                ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                if (Session["Thana_Code"].ToString() == "0")
                {
                    ddlBlock.Enabled = false;
                }
                
            }
        }
        bindthana();
        if (Session["Thana_Code"] != null)
        {
            if (Session["Thana_Code"].ToString() != "")
            {
                ddlThana.SelectedValue = Session["Thana_Code"].ToString();
                ddlThana.Enabled = false;
            }
        }
        bindbaithak();
    }

    //Bind Commissionary
    private void bindCommissionary()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType });
        if (dt.Rows.Count > 0)
        {
            ddlCommissionary.DataSource = dt;
            ddlCommissionary.DataTextField = "DIVISIONAME";
            ddlCommissionary.DataValueField = "DIVISIONCODE";
            ddlCommissionary.DataBind();
            ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlCommissionary.DataSource = null;
            ddlCommissionary.DataTextField = "DIVISIONAME";
            ddlCommissionary.DataValueField = "DIVISIONCODE";
            ddlCommissionary.DataBind();
            //ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    //Bind District
    private void bindDistrict()
    {
        ddlDistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, CommissionaryCode });
        if (dt.Rows.Count > 0)
        {
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlDistrict.DataSource = null;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    //Bind Division
    private void bindDivision()
    {
        try
        {
            SqlParameter _QueryType = new SqlParameter("@QueryType", 3);
            SqlParameter GetComm_Code = new SqlParameter("@Comm_Code", Convert.ToInt32(ddlCommissionary.SelectedValue.Trim()));
            SqlParameter GetDistrict_Code = new SqlParameter("@District_Code", Convert.ToInt32(ddlDistrict.SelectedValue.Trim()));
            SqlParameter GetSub_DivCode = new SqlParameter("@Sub_DivCode", Convert.ToInt32(ddlSubDivision.SelectedValue.Trim()));
            SqlParameter GetBlock_Code = new SqlParameter("@Block_Code", Convert.ToInt32(ddlBlock.SelectedValue.Trim()));        
            SqlParameter GetThana_Code = new SqlParameter("@Thana_Code", Convert.ToInt32(ddlThana.SelectedValue.Trim()));
            SqlParameter Getmatter_status = new SqlParameter("@Matter_Status", Convert.ToInt32(ddlbaithak.SelectedValue.Trim()));
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType", Convert.ToInt32(ddlDisputesType.SelectedValue.Trim()));
            SqlParameter getfromdate = new SqlParameter("@FromDate", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter gettodate = new SqlParameter("@ToDate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            DataTable dt = clsData.GetDataTableWithProc("stp_GetBlockDisputeConsolidateRpt", new SqlParameter[] { _QueryType,GetComm_Code, GetDistrict_Code, GetSub_DivCode, GetBlock_Code, GetThana_Code, GetBhumiVivadType, getfromdate, gettodate, Getmatter_status });
            if (dt.Rows.Count > 0)
            {
                btn_Export.Visible = true;

                //lblDetail.Visible = true;

                griddata.Columns[5].FooterText = "Total :";
                griddata.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Total")).Sum().ToString();
                griddata.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                griddata.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("UnFinalize")).Sum().ToString();
                griddata.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("ParchadhariBedakli")).Sum().ToString();
                griddata.Columns[10].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("SarkariBhumiKabza")).Sum().ToString();
                griddata.Columns[11].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiSeema")).Sum().ToString();
                griddata.Columns[12].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("NijiRastaNali")).Sum().ToString();
                griddata.Columns[13].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("JalStrot")).Sum().ToString();
                griddata.Columns[14].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("PatrikBhumiBatwara")).Sum().ToString();
                griddata.Columns[15].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("KhetiVivad")).Sum().ToString();
                griddata.Columns[16].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VaasVivad")).Sum().ToString();
                griddata.Columns[17].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("LaganNirdharan")).Sum().ToString();
                griddata.Columns[18].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("VyawsaikBhumi")).Sum().ToString();
                griddata.Columns[19].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BadlenBhumi")).Sum().ToString();
                griddata.Columns[20].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuArjan")).Sum().ToString();
                griddata.Columns[21].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("BhuHdBandi")).Sum().ToString();
                griddata.Columns[22].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("RaitiBhumiKabza")).Sum().ToString();
                griddata.Columns[23].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Anya")).Sum().ToString();
                griddata.DataSource = dt;
                griddata.DataBind();
                if (ddlDisputesType.SelectedValue == "1")
                {
                     griddata.Columns[9].Visible  = true;
                     griddata.Columns[10].Visible =  false;
                     griddata.Columns[11].Visible =  false;
                     griddata.Columns[12].Visible = false;
                     griddata.Columns[13].Visible = false;
                     griddata.Columns[14].Visible = false;
                     griddata.Columns[15].Visible = false;
                     griddata.Columns[16].Visible = false;
                     griddata.Columns[17].Visible = false;
                     griddata.Columns[18].Visible = false;
                     griddata.Columns[19].Visible = false;
                     griddata.Columns[20].Visible = false;
                     griddata.Columns[21].Visible = false;
                     griddata.Columns[22].Visible = false;
                     griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "2")
                {
                    griddata.Columns[9].Visible  =false;
                    griddata.Columns[10].Visible = true;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "3")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = true;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "4")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = true;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "5")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = true;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "6")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = true;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "7")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = true;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "8")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = true;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "9")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = true;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "10")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = true;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "11")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = true;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "12")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = true;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "13")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = true;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "15")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = true;
                    griddata.Columns[23].Visible = false;
                }
                else if (ddlDisputesType.SelectedValue == "20")
                {
                    griddata.Columns[9].Visible = false;
                    griddata.Columns[10].Visible = false;
                    griddata.Columns[11].Visible = false;
                    griddata.Columns[12].Visible = false;
                    griddata.Columns[13].Visible = false;
                    griddata.Columns[14].Visible = false;
                    griddata.Columns[15].Visible = false;
                    griddata.Columns[16].Visible = false;
                    griddata.Columns[17].Visible = false;
                    griddata.Columns[18].Visible = false;
                    griddata.Columns[19].Visible = false;
                    griddata.Columns[20].Visible = false;
                    griddata.Columns[21].Visible = false;
                    griddata.Columns[22].Visible = false;
                    griddata.Columns[23].Visible = true;
                }
                else
                {
                    griddata.Columns[9].Visible =  true;
                    griddata.Columns[10].Visible = true;
                    griddata.Columns[11].Visible = true;
                    griddata.Columns[12].Visible = true;
                    griddata.Columns[13].Visible = true;
                    griddata.Columns[14].Visible = true;
                    griddata.Columns[15].Visible = true;
                    griddata.Columns[16].Visible = true;
                    griddata.Columns[17].Visible = true;
                    griddata.Columns[18].Visible = true;
                    griddata.Columns[19].Visible = true;
                    griddata.Columns[20].Visible = true;
                    griddata.Columns[21].Visible = true;
                    griddata.Columns[22].Visible = true;
                    griddata.Columns[23].Visible = true;
                }
            }
            else
            {
                griddata.DataSource = dt;
                griddata.DataBind();
            }
            pnlgrid.Visible = true;
            Pnlsearch.Visible = false;
            btnback.Visible = false;
            rptPager.DataSource = null;
            rptPager.DataBind();
        }
        catch (Exception ex)
        { }
    }

    //Bind SubDivision
    private void bindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        string thana_code = "0";
        if (Session["Thana_Code"] != null && Session["Thana_Code"].ToString() != "")
        {
            thana_code = Session["Thana_Code"].ToString();

        }
        SqlParameter _thana_code = new SqlParameter("@thana_code", Convert.ToInt32(thana_code));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, District, _thana_code });
        if (dt.Rows.Count > 0)
        {
            ddlSubDivision.DataSource = dt;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlSubDivision.DataSource = null;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    //Bind Block
    private void bindBlock()
    {
        ddlBlock.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        string thana_code = "0";
        if (Session["Thana_Code"] != null && Session["Thana_Code"].ToString() != "")
        {
            thana_code = Session["Thana_Code"].ToString();

        }
        SqlParameter _thana_code = new SqlParameter("@thana_code", Convert.ToInt32(thana_code));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, SubDivision, _thana_code });
        if (dt.Rows.Count > 0)
        {
            ddlBlock.DataSource = dt;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlBlock.DataSource = null;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    //Bind Thana
    private void bindthana()
    {
        ddlThana.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("9"));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        SqlParameter block = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, District, SubDivision, block });
        if (dt.Rows.Count > 0)
        {
            ddlThana.DataSource = dt;
            ddlThana.DataTextField = "Police_Station";
            ddlThana.DataValueField = "PS_Code";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlThana.DataSource = null;
            ddlThana.DataTextField = "";
            ddlThana.DataValueField = "";
            ddlThana.DataBind();
            ddlThana.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    
    protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDistrict();
        bindSubDivision();
        bindBlock();
        bindthana();
    }    
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubDivision();
        bindBlock();
        bindthana();
    }    
    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindBlock();
        bindthana();
    }   
    protected void btnSearch_Click(object sender, EventArgs e)
    {      
        bindDivision();
    }    
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindthana();
    }
    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {
            ExportExcel(griddata, pnlgrid);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    protected void ExportExcel(GridView Gv, Panel Pnl)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", DateTime.Now.ToString("ddMMyyhhmmss") + "DisputeConsolidateRpt.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        for (int i = 0; i < Gv.HeaderRow.Cells.Count; i++)
        {
            Gv.HeaderRow.Cells[i].Style.Add("border-style", "Solid");
            Gv.HeaderRow.Cells[i].Style.Add("border-color", "Black");
            Gv.HeaderRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            Gv.HeaderRow.Cells[i].Style.Add("Font-Bold", "True");
            Gv.HeaderRow.Cells[i].Style.Add("Fore-Color", "Black");

            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
        }
        for (int i = 0; i < griddata.FooterRow.Cells.Count; i++)
        {
            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
            Gv.FooterRow.Cells[i].Style.Add("border-style", "Solid");
            Gv.FooterRow.Cells[i].Style.Add("border-color", "Black");
            Gv.FooterRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            Gv.FooterRow.Cells[i].Style.Add("Font-Bold", "True");
            Gv.FooterRow.Cells[i].Style.Add("Fore-Color", "Black");
        }
        int j = 1;
        //   This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in Gv.Rows)
        {
            gvrow.BackColor = System.Drawing.Color.White;
            if (j <= griddata.Rows.Count)
            {
                //if (j % 1 != 0)
                //{
                for (int k = 0; k < gvrow.Cells.Count; k++)
                {
                    gvrow.Cells[k].Style.Add("border-style", "Solid");
                    gvrow.Cells[k].Style.Add("border-color", "Black");
                    gvrow.Cells[k].ForeColor = System.Drawing.Color.Black;
                }
                // }
            }
            j++;
        }
        string style = @"<style> TD { mso-number-format:\@; } </style>";
        Response.Write(style);
        Pnl.RenderControl(htw);
        string dd = sw.ToString();
        dd = dd.Replace("href=", "");
        string Headrer = lbltext.Text;
        Response.Write("<h3><center>" + Headrer + "</center></h3>");
        Response.Write(dd);
        Response.End();
    }
    public bool CheckNull(object myValue)
    {
        if (myValue == null)
        {
            return false;
        }

        if (myValue is DBNull)
        {
            return false;
        }

        return true;
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            LinkButton linkbtn = sender as LinkButton;
            string UrlRedirect = enc.Encrypt(linkbtn.CommandArgument);
            string strFilePath = "Information.aspx?RegId=" + UrlRedirect;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + strFilePath + "','_blank')", true);

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");


            ImageButton image2 = (ImageButton)e.Row.FindControl("Image2");
            image2.Attributes.Add("onclick", "return fnLinkbutton1('" + image2.ClientID + "')");



            ImageButton image3 = (ImageButton)e.Row.FindControl("Image3");
            image3.Attributes.Add("onclick", "return fnLinkbutton1('" + image3.ClientID + "')");


            ImageButton image4 = (ImageButton)e.Row.FindControl("Image4");
            image4.Attributes.Add("onclick", "return fnLinkbutton1('" + image4.ClientID + "')");


            ImageButton image5 = (ImageButton)e.Row.FindControl("Image5");
            image5.Attributes.Add("onclick", "return fnLinkbutton1('" + image5.ClientID + "')");


            ImageButton image6 = (ImageButton)e.Row.FindControl("Image6");
            image6.Attributes.Add("onclick", "return fnLinkbutton1('" + image6.ClientID + "')");
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)Session["mySearchAppData03"];
        }
        catch (Exception ex) { return; }

        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }
    protected void bindApllication(string index)
    {         
        ViewState["PageIndex"] = ViewState["PageIndex"].ToString() == "" ? index : ViewState["PageIndex"].ToString();
        if (Convert.ToInt32(ViewState["total"].ToString()) > 0)
        {
            DataTable dt = new DataTable();
            SqlParameter GetQuery = new SqlParameter("@QueryType", 2);
            SqlParameter Getfinaldata = new SqlParameter("@finaldata", ViewState["finaldata"].ToString());
            SqlParameter GetDistrictcode = new SqlParameter("@District_Code", ViewState["DistrictCode"].ToString());
            SqlParameter GetSub_DivCode = new SqlParameter("@Sub_DivCode", ViewState["SubDivisionCode"].ToString());
            SqlParameter GetBlock_Code = new SqlParameter("@Block_Code", ViewState["BlockCode"].ToString());
            SqlParameter GetThana_Code = new SqlParameter("@Thana_Code", ViewState["ThanaCode"].ToString());
            SqlParameter GetBhumiVivadType = new SqlParameter("@BhumiVivadType",Convert.ToInt32(ViewState["BhumiVivadType"].ToString()));
            SqlParameter GetFromDate = new SqlParameter("@FromDate", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter GetToDate = new SqlParameter("@ToDate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter _PageSize = new SqlParameter("@PageSize", 50);
            SqlParameter _PageIndex = new SqlParameter("@PageIndex", Convert.ToInt32(ViewState["PageIndex"].ToString()));
            SqlParameter _RecordCount = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            SqlParameter Getmatter_status = new SqlParameter("@Matter_Status", Convert.ToInt32(ddlbaithak.SelectedValue.Trim()));
            _RecordCount.Direction = System.Data.ParameterDirection.Output;
            dt = clsData.GetDataTableWithProc("stp_GetBlockDisputeConsolidateRpt", new SqlParameter[]
            { GetQuery, Getfinaldata,GetDistrictcode, GetSub_DivCode, GetBlock_Code,GetThana_Code,
                GetBhumiVivadType,GetFromDate,GetToDate,_PageSize,_PageIndex,_RecordCount,Getmatter_status });
            int totalRecord = 0;
            if (_RecordCount.Value != null)
            {
                int.TryParse(_RecordCount.Value.ToString(), out totalRecord);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
            int recordCount = Convert.ToInt32(_RecordCount.Value);
            this.PopulatePager(recordCount, Convert.ToInt32(ViewState["PageIndex"].ToString()), "50");
            pnlgrid.Visible = false;
            Pnlsearch.Visible = true;
            btnback.Visible = true;
        }
    }
    protected void lnkTotal_Click(object sender, EventArgs e)
    {

        ViewState["PageIndex"] = "";
        ViewState["finaldata"] = "";
        ViewState["total"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";

        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = ddlDisputesType.SelectedValue;
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }

    }
    protected void lnkFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "";
            ViewState["total"] = "";
            ViewState["finaldata"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["PageIndex"] = "";
            ViewState["BhumiVivadType"] = "0";

            LinkButton lnk = sender as LinkButton;
            ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["BhumiVivadType"] = ddlDisputesType.SelectedValue;
            ViewState["finaldata"] = "1";
          
            if (Convert.ToInt32(ViewState["total"]) > 0)
            {
                bindApllication(index);
            }

        }
        catch (Exception ex)
        {

        }
    }    
    protected void lnkUnFinalize_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["PageIndex"] = "";
            ViewState["total"] = "";
            ViewState["finaldata"] = "";
            ViewState["DistrictCode"] = "";
            ViewState["SubDivisionCode"] = "";
            ViewState["BlockCode"] = "";
            ViewState["ThanaCode"] = "";
            ViewState["PageIndex"] = "";
            ViewState["BhumiVivadType"] = "0";

            LinkButton lnk = sender as LinkButton;
            ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
            ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
            ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
            ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
            ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
            string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
            ViewState["BhumiVivadType"] = ddlDisputesType.SelectedValue;
            ViewState["finaldata"] = "2";
          
            if (Convert.ToInt32(ViewState["total"]) > 0)
            {
                bindApllication(index);
            }

        }
        catch (Exception ex)
        {

        }
    }    
    protected void lnkParchadhariBedakli_Click(object sender, EventArgs e)
    {

        ViewState["PageIndex"] = "";
        ViewState["finaldata"] = "";
        ViewState["total"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";

        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "1";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }

    }
    protected void lnkSarkariBhumiKabza_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";

        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "2";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkRaitiBhumiSeema_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";

        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "3";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }


    }
    protected void lnkNijiRastaNali_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "4";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkJalStrot_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "5";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkPatrikBhumiBatwara_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "6";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkKhetiVivad_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "7";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkVaasVivad_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "8";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkLaganNirdharan_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "9";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkVyawsaikBhumi_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "10";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkBadlenBhumi_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "11";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkBhuArjan_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "12";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkBhuHdBandi_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "13";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkRaitiBhumiKabza_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "15";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void lnkAnya_Click(object sender, EventArgs e)
    {
        ViewState["PageIndex"] = "";
        ViewState["total"] = "";
        ViewState["finaldata"] = "";
        ViewState["DistrictCode"] = "";
        ViewState["SubDivisionCode"] = "";
        ViewState["BlockCode"] = "";
        ViewState["ThanaCode"] = "";
        ViewState["PageIndex"] = "";
        ViewState["BhumiVivadType"] = "0";


        LinkButton lnk = sender as LinkButton;
        ViewState["total"] = lnk.CommandArgument.ToString().Trim().Split(',')[0];
        ViewState["DistrictCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[1];
        ViewState["SubDivisionCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[2];
        ViewState["BlockCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[3];
        ViewState["ThanaCode"] = lnk.CommandArgument.ToString().Trim().Split(',')[4];
        string index = lnk.CommandArgument.ToString().Trim().Split(',')[5];
        ViewState["BhumiVivadType"] = "20";
        ViewState["finaldata"] = "3";
        if (Convert.ToInt32(ViewState["total"]) > 0)
        {
            bindApllication(index);
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        pnlgrid.Visible = true;
        Pnlsearch.Visible = false;
        btnback.Visible = false;
        rptPager.DataSource = null;
        rptPager.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        ViewState["PageIndex"] = pageIndex;
        bindApllication(ViewState["PageIndex"].ToString());
    }
    protected void setBackColorOfLinkButton()
    {
        foreach (RepeaterItem item in rptPager.Items)
        {

            LinkButton lnkButton = (LinkButton)item.FindControl("lnkPage");
            string value = lnkButton.Text;
            if (lnkButton.Enabled == false)
            {
                lnkButton.BackColor = System.Drawing.Color.FromName("#1eb089");
            }
            else
            {
                lnkButton.BackColor = System.Drawing.Color.FromName("#afa8a8ed");
            }

        }
    }
    private void PopulatePager(int recordCount, int currentPage, string pagesize)
    {

        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(pagesize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            int showMax = 10;
            int startPage;
            int endPage;
            if (((pageCount - currentPage) < 10) || ((pageCount - currentPage) == 0))
            {
               if(pageCount<=9)
                {
                    startPage = 1;
                    pages.Add(new ListItem("First", "1", currentPage > 1));
                    int i = 0;
                    for (i = startPage; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
                }
                else
                {
                    startPage = pageCount - 10 + 1;
                    pages.Add(new ListItem("First", "1", currentPage > 1));
                    int i = 0;
                    for (i = startPage; i <= pageCount; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                    pages.Add(new ListItem("Last", pageCount.ToString(), (i - 1) != currentPage));
                }
            }
            else if (pageCount - currentPage >= 10)
            {
                startPage = currentPage;
                endPage = currentPage + showMax - 1;
                pages.Add(new ListItem("First", "1", currentPage > 1));

                for (int i = startPage; i <= endPage; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
                pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
            }
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
        setBackColorOfLinkButton();
    }
   // [System.Web.Services.WebMethod()]
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string Getpdf(string url)
    {
        Encryptor enc = new Encryptor(Encryptor.PrivateKey);
        string urlpath = "";
        string encPathgov = enc.EncodeTo64(url);
        encPathgov = Aes256CbcEncrypterApp.Encrypt(encPathgov, System.Web.HttpContext.Current.Session["aes256key"].ToString());
        urlpath = encPathgov;
        //try
        //{
        //    using (var webClient = new WebClient())
        //    {
        //        byte[] imageBytes = webClient.DownloadData(url);
        //        string imreBase64Data = Convert.ToBase64String(imageBytes);
        //        string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);
        //        urlpath = imgDataURL;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    urlpath = ex.Message;
        //}

        return urlpath;
    }
    public bool CheckImage(object url)
    {
        if (url.ToString() != "")
        {
            string p = (url.ToString()).Replace("~", "");
            url = "http://localhost:8080" + p;
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(url.ToString());
                    string imreBase64Data = Convert.ToBase64String(imageBytes);
                    string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);

                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
            //return true;
        }


        else
        {
            return false;
        }


    }
}