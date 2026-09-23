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
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Security.Application;
using VerhoeffCheckDigitLibrary;
using System.IO;
using System.Globalization;

public partial class EOC_New_Record : System.Web.UI.Page
{
    clsDataAccessEoc clsData = new clsDataAccessEoc();
    whiteList wl = new whiteList();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Role"] == null || Session["Block_Code"] == null || Session["Thana_Code"]==null)
        //{
        //    Session.Clear();
        //    Session.Abandon();
        //    Response.Redirect("~/DeptDefault.aspx");
        //}

        Session["UserId"] = "dm1001";
        Session["District_Code"] = "230";

        if (!IsPostBack)
        {

            this.ddlDistrict_affected.SelectedIndex = 0;
            this.ddlBlock.SelectedIndex = 0;
            this.ddlBlock_affected.SelectedIndex = 0;
            divBlock.Visible = false;


            BindDist();
            if (Session["District_Code"] != null)
            {
                ddlDistrict.SelectedValue = Session["District_Code"].ToString();
                ddlDistrict.Enabled = false;
            }
            //BindSubDivision();
            //if (Session["Sub_DivCode"] != null)
            //{
            //    ddlSubdivision.SelectedValue = Session["Sub_DivCode"].ToString();
            //    ddlSubdivision.Enabled = false;
            //}
            BindBlock();
            if (Session["Block_Code"] != null)
            {
                ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                ddlBlock.Enabled = false;
            }

            //if (Session["Thana_Code"] != null)
            //{
            //    ddlPolice.SelectedValue = Session["Thana_Code"].ToString();
            //    if (ddlPolice.SelectedValue.Trim() != "0")
            //    {
            //        ddlPolice.Enabled = false;
            //    }
            //}




            //
        }
    }


    //USER22-250122


    void BindDist()
    {
        try
        {
            string sql = @"SELECT distinct DistrictName,DistrictCode from mst_Districts ORDER BY DISTRICTNAME ";
            DataTable dt = clsData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                ddlDistrict.DataSource = dt;
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "DistrictCode";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
                dt.Dispose();
            }
        }
        catch (Exception ex) { }
        BindBlock();


    }



    void BindSubDivision()
    {
        try
        {


            string sql = @" select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En";

            //SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            //DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode });


            //ddlSubdivision.DataSource = dt;
            //ddlSubdivision.DataTextField = "SubDivisionName";
            //ddlSubdivision.DataValueField = "SubDivisionCode";
            //ddlSubdivision.DataBind();
            //ddlSubdivision.Items.Insert(0, new ListItem("--Select--", "0"));
            //dt.Dispose();


        }
        catch { }

    }




    void BindBlock()
    {
        try
        {


            string sql = @" select DISTINCT t.BlockName,t.BlockCode from mst_Blocks t where t.DistrictCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by t.BlockName";

            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", "0");
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode });


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











    void BindPanchyat()
    {
        try
        {
            this.panchayt_grid.DataSource = null;
            this.panchayt_grid.DataBind();

            string sql = @" select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join mst_Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatName";

            //SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _BlockCode = new SqlParameter("@BlockCode", ddlBlock.SelectedValue.ToString());
            SqlParameter _AreaType = new SqlParameter("@AreaType", "");
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _BlockCode, _AreaType });

            this.panchayt_grid.DataSource = dt;
            this.panchayt_grid.DataBind();


        }
        catch { }


    }





    void bindward()
    {
        try
        {

            //SqlParameter Pan = new SqlParameter("@Panchayat", ddlPanchyat.SelectedValue);
            //SqlParameter AreaType = new SqlParameter("@AreaType", ddlareatype.SelectedValue);
            //string sql = @"  select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME";
            //DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { Pan, AreaType });

            //if (dt.Rows.Count > 0)
            //{
            //ddlWard.DataSource = dt;
            //ddlWard.DataTextField = "WARDNAME";
            //ddlWard.DataValueField = "WARDCODE";
            //ddlWard.DataBind();
            //ddlWard.Items.Insert(0, new ListItem("--चुनें--", "0"));
            //ddlWard.Items.Add(new ListItem("अन्य", "-1"));
            //}

            //else
            //{

            //    dt = new DataTable();
            //ddlWard.DataSource = dt;
            //ddlWard.DataTextField = "WARDNAME";
            //ddlWard.DataValueField = "WARDCODE";
            //ddlWard.DataBind();
            //ddlWard.Items.Insert(0, new ListItem("--चुनें--", "0"));
            //ddlWard.Items.Add(new ListItem("अन्य", "-1"));
            //}
        }
        catch (Exception)
        {

        }
    }






    void BindVillage()
    {
        try
        {


            string sql = @"select DISTINCT VILLCODE, VILLNAME from mst_VillageMaster v inner join Blocks b on v.BLOCKCODE = b.BlockCode
                           inner join mst_Panchayats p on p.BlockCode=b.BlockCode where b.BlockCode=@BlockCode and p.AreaType=@AreaType order by VILLNAME asc";

            //SqlParameter _BlockCode = new SqlParameter("@BlockCode", Session["Block_Code"].ToString());
            //SqlParameter _AreaType = new SqlParameter("@AreaType", ddlareatype.SelectedValue.ToString());
            //DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _BlockCode, _AreaType });


            //ddlVillage.DataSource = dt;
            //ddlVillage.DataTextField = "VILLNAME";
            //ddlVillage.DataValueField = "VILLCODE";
            //ddlVillage.DataBind();
            //ddlVillage.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlVillage.Items.Add(new ListItem("अन्य", "-1"));
            //dt.Dispose();


        }
        catch { }
    }




    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlDistrict_affected.SelectedIndex = 0;
        this.ddlBlock.SelectedIndex = 0;
        this.ddlBlock_affected.SelectedIndex = 0;
        divBlock.Visible = false;
    }

    protected void ddlDistrict_affected_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlBlock.SelectedIndex = 0;
        this.ddlBlock_affected.SelectedIndex = 0;
        if (ddlDistrict_affected.SelectedValue == "Y")
        {

            divBlock.Visible = true;
        }
        else
        {
            divBlock.Visible = false;

        }

    }

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlBlock_affected.SelectedIndex = 0;
    }

    protected void ddlBlock_affected_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlBlock_affected.SelectedValue == "Y" && ddlBlock.SelectedIndex !=0)
        //{
        //    BindPanchyat();
        //}
        //else {
        //    this.panchayt_grid.DataSource = null;
        //    this.panchayt_grid.DataBind();
        //}


    }

    //protected void ddlDistrict_step1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    //protected void ddlBlock_step1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    protected void Submit_Click(object sender, EventArgs e)
    {
        this.pnlPanchayt.Visible = false;
        if (ddlBlock_affected.SelectedValue == "Y" && ddlBlock.SelectedIndex != 0)
        {
            this.pnlPanchayt.Visible = true;
            BindPanchyat();
            this.txtdate1.Enabled = false;
            this.ddlDistrict.Enabled = false;
            this.ddlDistrict_affected.Enabled = false;
            this.ddlBlock.Enabled = false;
            this.ddlBlock_affected.Enabled = false;
            this.btnSubmit.Enabled = false;

        }
        else
        {
            this.panchayt_grid.DataSource = null;
            this.panchayt_grid.DataBind();
            this.pnlPanchayt.Visible = false;
        }
    }

    protected void panchayt_grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            //string rowIndex = e.CommandArgument.ToString();
            //DataTable dt = (DataTable)ViewState["vadiDetails"];
            //dt.Rows.RemoveAt(Convert.ToInt32(rowIndex));
            //ViewState["vadiDetails"] = dt;
            //BindGrid();

        }
    }


    protected void validateStep2()
    {
        if (string.IsNullOrEmpty(txtHumanAffected.Text.Trim()))
        {
            txtHumanAffected.Focus();
            Utility.showMessage(this, "कृप्या प्रभावित मनुष्यो की संख्या दर्ज़ करे");
            return;
        }

        if (string.IsNullOrEmpty(txtFamilyAffected.Text.Trim()))
        {
            txtFamilyAffected.Focus();
            Utility.showMessage(this, "कृप्या प्रभावित परिवार की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtMigratedPopulation.Text.Trim()))
        {
            txtMigratedPopulation.Focus();
            Utility.showMessage(this, "कृप्या निष्क्रमित आबादी की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtAffectedAgriLand.Text.Trim()))
        {
            txtAffectedAgriLand.Focus();
            Utility.showMessage(this, "कृप्या कृषि योग्य प्रभावित क्षेत्रफल (हेक्टेयर) दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtAffectedNonAgriLand.Text.Trim()))
        {
            txtAffectedNonAgriLand.Focus();
            Utility.showMessage(this, "कृप्या ग़ैर कृषि योग्य प्रभावित क्षेत्रफल (हेक्टेयर) दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtDamagedCropArea.Text.Trim()))
        {
            txtDamagedCropArea.Focus();
            Utility.showMessage(this, "कृप्या क्षतिग्रस्त फसल क्षेत्रफल (हेक्टेयर) दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtDamagedCropValue.Text.Trim()))
        {
            txtDamagedCropValue.Focus();
            Utility.showMessage(this, "कृप्या क्षतिग्रस्त फसलों का अनुमानित मूल्य (रु) दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtFullyAffectedKutchaHouses.Text.Trim()))
        {
            txtFullyAffectedKutchaHouses.Focus();
            Utility.showMessage(this, "कृप्या कच्चा घर (पूर्ण रूप से क्षतिग्रस्त) की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtPartlyAffectedKutchaHouses.Text.Trim()))
        {
            txtPartlyAffectedKutchaHouses.Focus();
            Utility.showMessage(this, "कृप्या कच्चा घर (आंशिक रूप से क्षतिग्रस्त) की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtFullyAffectedPakkaHouses.Text.Trim()))
        {
            txtFullyAffectedPakkaHouses.Focus();
            Utility.showMessage(this, "कृप्या पक्का घर (पूर्ण रूप से क्षतिग्रस्त) की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtPartlyAffectedPakkaHouses.Text.Trim()))
        {
            txtPartlyAffectedPakkaHouses.Focus();
            Utility.showMessage(this, "कृप्या पक्का घर (आंशिक रूप से क्षतिग्रस्त) की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtAffectedHuts.Text.Trim()))
        {
            txtAffectedHuts.Focus();
            Utility.showMessage(this, "कृप्या क्षतिग्रस्त झोपडी की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtAffectedAnimalSheds.Text.Trim()))
        {
            txtAffectedAnimalSheds.Focus();
            Utility.showMessage(this, "कृप्या क्षतिग्रस्त पशु शेड की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtDamagedHousesValue.Text.Trim()))
        {
            txtDamagedHousesValue.Focus();
            Utility.showMessage(this, "कृप्या क्षतिग्रस्त मकानों का अनुमानित मूल्य (रु) दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtDamagedPublicPropVal.Text.Trim()))
        {
            txtDamagedPublicPropVal.Focus();
            Utility.showMessage(this, "कृप्या क्षतिग्रस्त सार्वजनिक सम्पत्ति का अनुमानित मूल्य (रु) दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtWaterEncircledVillages.Text.Trim()))
        {
            txtWaterEncircledVillages.Focus();
            Utility.showMessage(this, "कृप्या पानी से घिरे गांव की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtDeadPeoples.Text.Trim()))
        {
            txtDeadPeoples.Focus();
            Utility.showMessage(this, "कृप्या मृतकों की संख्या दर्ज़ करे");
            return;
        }

    }
    protected void btnSaveStep2_Click(object sender, EventArgs e)
    {
        ViewState["FloodID"] = "3";


        try
        {
            int FloodID = 0;
            if (ViewState["FloodID"] != null)
            {
                FloodID = Convert.ToInt32(ViewState["FloodID"].ToString());
            }
            validateStep2();
            SqlParameter _FloodID = new SqlParameter("@FloodID", FloodID);
            SqlParameter _HumanAffected = new SqlParameter("@HumanAffected", txtHumanAffected.Text.Trim());
            SqlParameter _FamilyAffected = new SqlParameter("@FamilyAffected", txtFamilyAffected.Text.Trim());
            SqlParameter _MigratedPopulation = new SqlParameter("@MigratedPopulation", txtMigratedPopulation.Text.Trim());
            SqlParameter _AffectedAgriLand = new SqlParameter("@AffectedAgriLand", txtAffectedAgriLand.Text.Trim());
            SqlParameter _AffectedNonAgriLand = new SqlParameter("@AffectedNonAgriLand", txtAffectedNonAgriLand.Text.Trim());
            SqlParameter _DamagedCropArea = new SqlParameter("@DamagedCropArea", txtDamagedCropArea.Text.Trim());
            SqlParameter _DamagedCropValue = new SqlParameter("@DamagedCropValue", txtDamagedCropValue.Text.Trim());
            SqlParameter _FullyAffectedKutchaHouses = new SqlParameter("@FullyAffectedKutchaHouses", txtFullyAffectedKutchaHouses.Text.Trim());
            SqlParameter _PartlyAffectedKutchaHouses = new SqlParameter("@PartlyAffectedKutchaHouses", txtPartlyAffectedKutchaHouses.Text.Trim());
            SqlParameter _FullyAffectedPakkaHouses = new SqlParameter("@FullyAffectedPakkaHouses", txtFullyAffectedPakkaHouses.Text.Trim());
            SqlParameter _PartlyAffectedPakkaHouses = new SqlParameter("@PartlyAffectedPakkaHouses", txtPartlyAffectedPakkaHouses.Text.Trim());
            SqlParameter _AffectedHuts = new SqlParameter("@AffectedHuts", txtAffectedHuts.Text.Trim());
            SqlParameter _AffectedAnimalSheds = new SqlParameter("@AffectedAnimalSheds", txtAffectedAnimalSheds.Text.Trim());
            SqlParameter _DamagedHousesValue = new SqlParameter("@DamagedHousesValue", txtDamagedHousesValue.Text.Trim());
            SqlParameter _DamagedPublicPropVal = new SqlParameter("@DamagedPublicPropVal", txtDamagedPublicPropVal.Text.Trim());
            SqlParameter _WaterEncircledVillages = new SqlParameter("@WaterEncircledVillages", txtWaterEncircledVillages.Text.Trim());
            SqlParameter _DeadPeoples = new SqlParameter("@DeadPeoples", txtDeadPeoples.Text.Trim());

            DataTable dt = clsData.GetDataTableWithProc("sp_insertFloodDetailsStep2", new SqlParameter[] 
                { _FloodID,_HumanAffected, _FamilyAffected, _MigratedPopulation, _AffectedAgriLand, _AffectedNonAgriLand, _DamagedCropArea, _DamagedCropValue,
                  _FullyAffectedKutchaHouses, _PartlyAffectedKutchaHouses, _FullyAffectedPakkaHouses, _PartlyAffectedPakkaHouses, _AffectedHuts,
                  _AffectedAnimalSheds, _DamagedHousesValue, _DamagedPublicPropVal, _WaterEncircledVillages, _DeadPeoples });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["msg"].ToString() == "1")
                {
                    string str = dt.Rows[0]["FloodID"].ToString();
                    Session["FloodID"] = str;
                    Utility.showMessage(this, "Submitted successfully");
                }
                else if (dt.Rows[0]["msg"].ToString() == "2")
                {
                    string str = dt.Rows[0]["FloodID"].ToString();
                    Session["FloodID"] = str;
                    Utility.showMessage(this, "Update successfully");
                }
                else
                {
                    Utility.showMessage(this, " not Submitted successfully");
                }
            }


        }
        catch (Exception ex)
        { }
    }
    protected void validateStep3()
    {
        if (string.IsNullOrEmpty(txtAffectedAnimals.Text.Trim()))
        {
            txtAffectedAnimals.Focus();
            Utility.showMessage(this, "कृप्या प्रभावित पशुओं की संख्या दर्ज़ करे");
            return;
        }

        if (string.IsNullOrEmpty(txtAnimalCamps.Text.Trim()))
        {
            txtAnimalCamps.Focus();
            Utility.showMessage(this, "कृप्या पशु कैंपो की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtAnimalsTreated.Text.Trim()))
        {
            txtAnimalsTreated.Focus();
            Utility.showMessage(this, "कृप्या उपचार किये गए पशुओं की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtSmallMilchAnimals.Text.Trim()))
        {
            txtSmallMilchAnimals.Focus();
            Utility.showMessage(this, "कृप्या मृत पशुओं की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtBigMilchAnimals.Text.Trim()))
        {
            txtBigMilchAnimals.Focus();
            Utility.showMessage(this, "कृप्या दुधारू पशु (बड़े) की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtSmallNonMilchAnimals.Text.Trim()))
        {
            txtSmallNonMilchAnimals.Focus();
            Utility.showMessage(this, "कृप्या ग़ैर-दुधारू पशु (छोटे) की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtBigNonMilchAnimals.Text.Trim()))
        {
            txtBigNonMilchAnimals.Focus();
            Utility.showMessage(this, "कृप्या ग़ैर-दुधारू पशु (बड़े) की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtDamagedFishSeedFarms.Text.Trim()))
        {
            txtDamagedFishSeedFarms.Focus();
            Utility.showMessage(this, "कृप्या मछली जीरा फार्म क्षतिग्रस्त (हेक्टेयर) दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtFullyDamagedBoats.Text.Trim()))
        {
            txtFullyDamagedBoats.Focus();
            Utility.showMessage(this, "कृप्या पूरी तरह से क्षतिग्रस्त नाव की संख्या दर्ज़ करे");
            return;
        }
        if (string.IsNullOrEmpty(txtPartlyDamagedBoats.Text.Trim()))
        {
            txtPartlyDamagedBoats.Focus();
            Utility.showMessage(this, "कृप्या आंशिक रूप से नाव क्षतिग्रस्त की संख्या दर्ज़ करे");
            return;
        }

    }
    protected void btnSaveStep3_Click(object sender, EventArgs e)
    {
        try
        {
            int FloodID = 0;
            if (ViewState["FloodID"] != null)
            {
                FloodID = Convert.ToInt32(ViewState["FloodID"].ToString());
            }
            validateStep2();
            SqlParameter _FloodID = new SqlParameter("@FloodID", FloodID);
            SqlParameter _AffectedAnimals = new SqlParameter("@AffectedAnimals", txtAffectedAnimals.Text.Trim());
            SqlParameter _AnimalCamps = new SqlParameter("@AnimalCamps", txtAnimalCamps.Text.Trim());
            SqlParameter _AnimalsTreated = new SqlParameter("@AnimalsTreated", txtAnimalsTreated.Text.Trim());
            SqlParameter _SmallMilchAnimals = new SqlParameter("@SmallMilchAnimals", txtSmallMilchAnimals.Text.Trim());
            SqlParameter _BigMilchAnimals = new SqlParameter("@BigMilchAnimals", txtBigMilchAnimals.Text.Trim());
            SqlParameter _SmallNonMilchAnimals = new SqlParameter("@SmallNonMilchAnimals", txtSmallNonMilchAnimals.Text.Trim());
            SqlParameter _BigNonMilchAnimals = new SqlParameter("@BigNonMilchAnimals", txtBigNonMilchAnimals.Text.Trim());
            SqlParameter _DamagedFishSeedFarms = new SqlParameter("@DamagedFishSeedFarms", txtDamagedFishSeedFarms.Text.Trim());
            SqlParameter _FullyDamagedBoats = new SqlParameter("@FullyDamagedBoats", txtFullyDamagedBoats.Text.Trim());
            SqlParameter _PartlyDamagedBoats = new SqlParameter("@PartlyDamagedBoats", txtPartlyDamagedBoats.Text.Trim());

            DataTable dt = clsData.GetDataTableWithProc("sp_insertFloodDetailsStep3", new SqlParameter[] 
                { _FloodID,_AffectedAnimals, _AnimalCamps, _AnimalsTreated, _SmallMilchAnimals, _BigMilchAnimals, _SmallNonMilchAnimals, _BigNonMilchAnimals,
                  _DamagedFishSeedFarms, _FullyDamagedBoats, _PartlyDamagedBoats });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["msg"].ToString() == "1")
                {
                    string str = dt.Rows[0]["FloodID"].ToString();
                    Session["FloodID"] = str;
                    Utility.showMessage(this, "Submitted successfully");
                }
                else if (dt.Rows[0]["msg"].ToString() == "2")
                {
                    string str = dt.Rows[0]["FloodID"].ToString();
                    Session["FloodID"] = str;
                    Utility.showMessage(this, "Update successfully");
                }
                else
                {
                    Utility.showMessage(this, " not Submitted successfully");
                }
            }


        }
        catch (Exception ex)
        { }
    }


}