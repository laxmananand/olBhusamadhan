using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class LandDispute_Entry_SearchAppForMetting : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login_Default.aspx");
        }

        if (!IsPostBack)
        {
            bindRange();
            bindCommissionary();
            if (Session["Commsionary_Code"] != null)
            {
                if (Session["Commsionary_Code"].ToString() != "")
                {
                    ddlCommissionary.SelectedValue = Session["Commsionary_Code"].ToString();
                    ddlCommissionary.Enabled = false;
                }
            }
            if (Session["RangeCode"] != null)
            {
                if (Session["RangeCode"].ToString() != "")
                {
                    ddlRange.SelectedValue = Session["RangeCode"].ToString();
                    ddlRange.Enabled = false;

                }
            }
            if (Session["Role"].ToString() == "DIG")
            {
              
                divLabRange.Visible = true;
                divddlCommissionary.Visible = false;
                

            }
            else
            {
               
                divLabRange.Visible = false;
                divddlCommissionary.Visible = true;
               

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
                    ddlSubDivision.Enabled = false;
                }
            }
            bindBlock();
            if (Session["Block_Code"] != null)
            {
                if (Session["Block_Code"].ToString() != "")
                {
                    ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                    ddlBlock.Enabled = false;
                }
            }
            bindPoliceStation();
            if (Session["Thana_Code"] != null)
            {
                if (Session["Block_Code"].ToString() != "")
                {
                    ddlPoliceStation.SelectedValue = Session["Thana_Code"].ToString();
                    if (ddlPoliceStation.SelectedValue.Trim() != "0")
                    {
                        ddlPoliceStation.Enabled = false;
                    }
                }
            }
            bindPanchayat();
            bindVillage();
            bindWard();
            bind_bhumivivad_Type();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindGridData(1);
    }

    protected void bind_bhumivivad_Type()// भूमि विवाद का प्रकार 
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_GetBhumi_VivadType", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlbhumivivadtype.DataSource = dt;
                    ddlbhumivivadtype.DataTextField = "vivadtype";
                    ddlbhumivivadtype.DataValueField = "id";
                    ddlbhumivivadtype.DataBind();
                    ddlbhumivivadtype.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
        }
        catch (Exception ee)
        {


        }
    }
    private void bindGridData_old()
    {
        try
        {
            Session["mySearchAppData02"] = null;

            SqlParameter GetQueryType = new SqlParameter("@QueryType", "2");
            SqlParameter GetComm_Code = new SqlParameter("@Comm_Code", Convert.ToInt32(ddlCommissionary.SelectedValue.Trim()));
            SqlParameter GetDistrict_Code = new SqlParameter("@District_Code", Convert.ToInt32(ddlDistrict.SelectedValue.Trim()));
            SqlParameter GetSub_DivCode = new SqlParameter("@Sub_DivCode", Convert.ToInt32(ddlSubDivision.SelectedValue.Trim()));
            SqlParameter GetBlock_Code = new SqlParameter("@Block_Code", Convert.ToInt32(ddlBlock.SelectedValue.Trim()));
            SqlParameter GetThana_code = new SqlParameter("@Thana_code", Convert.ToInt32(ddlPoliceStation.SelectedValue.Trim()));
            SqlParameter GetPanchayat_Code = new SqlParameter("@Panchayat_Code", Convert.ToInt32(ddlPanchayat.SelectedValue.Trim()));
            SqlParameter GetVillage = new SqlParameter("@Village", Convert.ToInt32(ddlVillage.SelectedValue.Trim()));
            SqlParameter GetWardNo = new SqlParameter("@WardNo", Convert.ToInt32(ddlWard.SelectedValue.Trim()));
            DataTable dt = clsData.GetDataTableWithProc("SP_SearchMatterRegistration", new SqlParameter[] { GetQueryType, GetComm_Code, GetDistrict_Code, GetSub_DivCode, GetBlock_Code, GetThana_code, GetPanchayat_Code, GetVillage, GetWardNo });
            if (dt.Rows.Count > 0)
            {
                Session["mySearchAppData02"] = dt;
                GridView1.PageIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Session["mySearchAppData02"] = null;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception)
        {

        }


    }
   

    protected void PageSize_Changed(object sender, EventArgs e)
    {
        this.bindGridData(1);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.bindGridData(pageIndex);
    }
    private void bindGridData(int pageIndex)
    {
        if (ddlAction.SelectedValue.ToString() == "0" || ddlbhumivivadtype.SelectedValue.ToString() == "0" || ddlSensivity.SelectedValue.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('Plz select Sensivity,action and Disputes Type ')", true);
        }
        else
        {
            DataTable dt = new DataTable();
            SqlParameter[] p = new SqlParameter[15];
            try
            {
                p[0] = new SqlParameter("@QueryType", "3");
                p[1] = new SqlParameter("@Comm_Code", Convert.ToInt32(ddlCommissionary.SelectedValue.Trim()));
                p[2] = new SqlParameter("@District_Code", Convert.ToInt32(ddlDistrict.SelectedValue.Trim()));
                p[3] = new SqlParameter("@Sub_DivCode", Convert.ToInt32(ddlSubDivision.SelectedValue.Trim()));
                p[4] = new SqlParameter("@Block_Code", Convert.ToInt32(ddlBlock.SelectedValue.Trim()));
                p[5] = new SqlParameter("@Thana_code", Convert.ToInt32(ddlPoliceStation.SelectedValue.Trim()));
                p[6] = new SqlParameter("@Panchayat_Code", Convert.ToInt32(ddlPanchayat.SelectedValue.Trim()));
                p[7] = new SqlParameter("@Village", Convert.ToInt32(ddlVillage.SelectedValue.Trim()));
                p[8] = new SqlParameter("@WardNo", Convert.ToInt32(ddlWard.SelectedValue.Trim()));
                p[9] = new SqlParameter("@PageIndex", pageIndex);
                p[10] = new SqlParameter("@PageSize", int.Parse(ddlPageSize.SelectedValue));
                p[11] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
                p[11].Direction = System.Data.ParameterDirection.Output;
                p[12] = new SqlParameter("@Matter_Status", int.Parse(ddlbhumivivadtype.SelectedValue));
                p[13] = new SqlParameter("@sensitivity", int.Parse(ddlSensivity.SelectedValue));
                p[14] = new SqlParameter("@BhumiVivadType", int.Parse(ddlbhumivivadtype.SelectedValue));
                dt = clsData.GetDataTableWithProc("SP_SearchActionDetails", p);
                int totalRecord = 0;
                if (p[11].Value != null)
                {
                    int.TryParse(p[11].Value.ToString(), out totalRecord);
                }
                GridView1.DataSource = dt;
                GridView1.DataBind();
                int recordCount = Convert.ToInt32(p[11].Value);
                this.PopulatePager(recordCount, pageIndex);
            }
            catch (Exception ex)
            {

            }
        }
       
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", currentPage > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            //LinkButton lnkAreaCode = sender as LinkButton;
            //string btnName = lnkAreaCode.ID;
            LinkButton linkbtn = sender as LinkButton;
            string UrlRedirect = enc.Encrypt(linkbtn.CommandArgument);
            Response.Redirect("VerifyByCo.aspx?RegId=" + UrlRedirect,false);

            //if (btnName.Contains("View"))
            //{
            //    string getId = lnkAreaCode.CommandArgument;




            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    private void bindWard()
    {
        ddlWard.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("8"));
        SqlParameter PanchayatCode = new SqlParameter("@PanchayatCode", Convert.ToInt32(ddlPanchayat.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, PanchayatCode });
        if (dt.Rows.Count > 0)
        {
            ddlWard.DataSource = dt;
            ddlWard.DataTextField = "WardName";
            ddlWard.DataValueField = "WardCode";
            ddlWard.DataBind();
            ddlWard.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlWard.DataSource = null;
            ddlWard.DataTextField = "WardName";
            ddlWard.DataValueField = "WardCode";
            ddlWard.DataBind();
            ddlWard.Items.Insert(0, new ListItem("All", "0"));
        }

    }
    private void bindVillage()
    {
        ddlVillage.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("7"));
        SqlParameter BlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, BlockCode });
        if (dt.Rows.Count > 0)
        {
            ddlVillage.DataSource = dt;
            ddlVillage.DataTextField = "VILLNAME";
            ddlVillage.DataValueField = "VILLCODE";
            ddlVillage.DataBind();
            ddlVillage.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlVillage.DataSource = null;
            ddlVillage.DataTextField = "VILLNAME";
            ddlVillage.DataValueField = "VILLCODE";
            ddlVillage.DataBind();
            ddlVillage.Items.Insert(0, new ListItem("All", "0"));
        }

    }
    private void bindPanchayat()
    {
        ddlPanchayat.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("6"));
        SqlParameter BlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, BlockCode });
        if (dt.Rows.Count > 0)
        {
            ddlPanchayat.DataSource = dt;
            ddlPanchayat.DataTextField = "PanchayatName";
            ddlPanchayat.DataValueField = "PanchayatCode";
            ddlPanchayat.DataBind();
            ddlPanchayat.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlPanchayat.DataSource = null;
            ddlPanchayat.DataTextField = "PanchayatName";
            ddlPanchayat.DataValueField = "PanchayatCode";
            ddlPanchayat.DataBind();
            ddlPanchayat.Items.Insert(0, new ListItem("All", "0"));
        }

    }
    private void bindPoliceStation()
    {
        ddlPoliceStation.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("9"));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        SqlParameter BlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, SubDivision, District, BlockCode });
        if (dt.Rows.Count > 0)
        {
            ddlPoliceStation.DataSource = dt;
            ddlPoliceStation.DataTextField = "Police_Station";
            ddlPoliceStation.DataValueField = "PS_Code";
            ddlPoliceStation.DataBind();
            ddlPoliceStation.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlPoliceStation.DataSource = null;
            ddlPoliceStation.DataTextField = "Block_Name";
            ddlPoliceStation.DataValueField = "BlockCode";
            ddlPoliceStation.DataBind();
            ddlPoliceStation.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    private void bindBlock()
    {
        ddlBlock.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        string thana_code = "0";
        if (Session["Thana_Code"] != null && Session["Thana_Code"].ToString()!="")
        {
            thana_code = Session["Thana_Code"].ToString();

        }
        SqlParameter _thana_code = new SqlParameter("@thana_code", Convert.ToInt32(thana_code));

        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, @SubDivision, _thana_code });
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
    private void bindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        string thana_code = "0";
        if (Session["Thana_Code"] != null && Session["Thana_Code"].ToString()!="")
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
    private void bindDistrict()
    {
        ddlDistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString()));
        SqlParameter Rangeid = new SqlParameter("@Rangeid", Convert.ToInt32(ddlRange.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, CommissionaryCode, Rangeid });
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
    private void bindRange()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("0"));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType });
        if (dt.Rows.Count > 0)
        {
            ddlRange.DataSource = dt;
            ddlRange.DataTextField = "RangeName";
            ddlRange.DataValueField = "Rangeid";
            ddlRange.DataBind();
            ddlRange.Items.Insert(0, new ListItem("All", "0"));

        }
        else
        {
            ddlRange.DataSource = null;
            ddlRange.DataTextField = "RangeName";
            ddlRange.DataValueField = "Rangeid";
            ddlRange.DataBind();
            //ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
    }
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {      
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView grv = (GridView)e.Row.FindControl("grdAction");
            string a_id = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt32(a_id));
            SqlParameter _Matter_Status = new SqlParameter("@Matter_Status", int.Parse(ddlbhumivivadtype.SelectedValue));
            SqlParameter _sensitivity = new SqlParameter("@sensitivity", int.Parse(ddlSensivity.SelectedValue));
            SqlParameter QueryType = new SqlParameter("@QueryType", 4);

            DataTable dt = clsData.GetDataTableWithProc("SP_SearchActionDetails", new SqlParameter[] { QueryType, _a_id, _Matter_Status, _sensitivity });
            grv.DataSource = dt;
            grv.DataBind();
        }
    }
    protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindRange();
        bindDistrict();
        bindSubDivision();
        bindBlock();
        bindPoliceStation();
        bindPanchayat();
        bindVillage();
        bindWard();
    }
    protected void ddlBlock_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindPoliceStation();
        bindPanchayat();
        bindVillage();
    }
    protected void ddlPanchayat_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindWard();
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubDivision();
        bindBlock();
        bindPoliceStation();
        bindPanchayat();
        bindVillage();
        bindWard();
    }
    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {




        bindBlock();


    }
    protected void lnkRead_Click(object sender, EventArgs e)
    {
        //int Rowindex = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
        //string filelocation = GridView1.Rows[Rowindex].Cells[21].Text;
        LinkButton lnkAreaCode = sender as LinkButton;
        string btnName = lnkAreaCode.ID;
        string getId = lnkAreaCode.CommandArgument;
        string filepath = Server.MapPath("fuIdDocumentLD212095111.pdf");
        WebClient user = new WebClient();
        Byte[] FileBuffer = user.DownloadData(filepath);
        if (FileBuffer != null)
        {
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-length", FileBuffer.Length.ToString());
            Response.BinaryWrite(FileBuffer);
        }


    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //Get the link button of each row
        //    LinkButton lnkbutton = (LinkButton)e.Row.FindControl("lb1");
        //    //Attach javascript function to each linkbutton
        //    lnkbutton.Attributes.Add("onclick", "return fnLinkbutton('" + lnkbutton.ClientID + "')");

        //}
    }
    [System.Web.Services.WebMethod()]
    public static string Getpdf(string url)
    {
        string urlpath = "";
        try
        {
            using (var webClient = new WebClient())
            {
                byte[] imageBytes = webClient.DownloadData(url);
                string imreBase64Data = Convert.ToBase64String(imageBytes);
                string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);
                urlpath = imgDataURL;
            }
        }
        catch (Exception ex)
        {
            urlpath = ex.Message;
        }

        return urlpath;
    }


    
}