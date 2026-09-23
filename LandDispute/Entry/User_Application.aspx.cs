using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Police_Station_New_Entry : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    whiteList wl = new whiteList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null || Session["Block_Code"] == null || Session["Thana_Code"] == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
        }

        if (!IsPostBack)
        {
            yearOfBirth();
            bindVadiDistrict();
            bindVadiSubDivision();
            bindVadiCircle();
            bindVadiPS();
            bindVadiPanchayat();
            bindVadiVillage();
            bindVadiWard();
            bindDepartment();
            BindVadi_Prativadi_Anya_Type();
            BindVadi_Sanstha_Anya_Type();

            bindPvadiDistrict();
            bindPvadiSubDivision();
            bindPvadiCircle();
            bindPvadiPS();
            bindPvadiPS();
            bindPvadiPanchayat();
            bindPvadiVillage();
            bindPvadiWard();
        }
    }
    private void bindVadiDistrict()
    {
        //clsData.FillDropDownList(ddlVadiDistrict, "SELECT DISTRICTCODE,DISTRICTNAME FROM mst_Commissionary_Districts order by DISTRICTNAME", "DISTRICTCODE", "DISTRICTNAME", "--Select--", new SqlParameter[] { });
    }
    private void bindVadiSubDivision()
    {
        //clsData.FillDropDownList(ddlVadiSubDiv, "select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En", "SubDivisionCode", "SubDivisionName", "--Select--", new SqlParameter[] { new SqlParameter("@District_Code", ddlVadiDistrict.SelectedValue) });
    }
    private void bindVadiCircle()
    {
        //clsData.FillDropDownList(ddlVadiCircle, "select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName", "BlockCode", "BlockName", "--Select--", new SqlParameter[] { new SqlParameter("@District_Code", ddlVadiDistrict.SelectedValue), new SqlParameter("@Subdivision_Code", ddlVadiSubDiv.SelectedValue) });
    }

    private void bindVadiPS()
    {
        //clsData.FillDropDownList(ddlVadiPS, "Select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t left join MstThanaMapping m on m.Thana_Code = t.PS_Code left join Blocks b on b.BlockCode = m.Code and m.Type = 'Block' where District_code = @District_Code and  b.SubDivCode is not null and m.code = @Circle_Code and b.SubDivCode = @Subdivision_Code ORDER BY Police_Station", "PS_Code", "Police_Station", "--Select--", new SqlParameter[] { new SqlParameter("@District_Code", ddlVadiDistrict.SelectedValue), new SqlParameter("@Subdivision_Code", ddlVadiSubDiv.SelectedValue),new SqlParameter("@Circle_Code", ddlVadiCircle.SelectedValue) });
    }
    private void bindVadiPanchayat()
    {
        //clsData.FillDropDownList(ddlVadiPanchayat, "select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatName", "PanchayatCode", "PanchayatName", "--Select--", new SqlParameter[] { new SqlParameter("@BlockCode", ddlVadiCircle.SelectedValue), new SqlParameter("@AreaType", ddlVadiAreaType.SelectedValue) });
    }
    private void bindVadiVillage()
    {
        //clsData.FillDropDownList(ddlVadiVillage, "select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p inner join PanchayatVillage pv on p.PanchayatCode = pv.PanchayatCode inner join mst_VillageMaster v on v.VILLCODE = pv.VillageCode where p.PanchayatCode = @PanchayatCode order by v.VILLNAME", "VILLCODE", "VILLNAME", "--Select--", new SqlParameter[] { new SqlParameter("@PanchayatCode", ddlVadiPanchayat.SelectedValue) });
    }
    private void bindVadiWard()
    {
        //clsData.FillDropDownList(ddlVadiWard, "select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME", "WARDCODE", "WARDNAME", "--Select--", new SqlParameter[] { new SqlParameter("@Panchayat", ddlVadiPanchayat.SelectedValue), new SqlParameter("@AreaType", ddlVadiAreaType.SelectedValue) });
    }


    protected void ddlVadiDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVadiSubDivision();
        bindVadiCircle();
        bindVadiPS();
        bindVadiPanchayat();
        bindVadiVillage();
        bindVadiWard();
        ddlVadiAreaType.SelectedIndex = 0;
    }

    protected void ddlVadiSubDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVadiCircle();
        bindVadiPS();
        bindVadiPanchayat();
        bindVadiVillage();
        bindVadiWard();
        ddlVadiAreaType.SelectedIndex = 0;
    }
    protected void ddlVadiCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVadiPS();
        bindVadiPanchayat();
        bindVadiVillage();
        bindVadiWard();
        ddlVadiAreaType.SelectedIndex = 0;
    }
    protected void ddlVadiPanchayat_SelectedIndexChanged (object sender, EventArgs e)
    {
        bindVadiVillage();
        bindVadiWard();
    }

    protected void ddlVadiVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindVadiWard();
    }

    





    protected void ddlVadiPS_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindPS();
    }


    protected void ddlVadiAreaType_SelectedIndexChanged (object sender, EventArgs e)
    {
        if (ddlVadiAreaType.SelectedIndex == 2)
        {
            lblPan.Text = "नगर निकाय";
            divVadiMoh.Visible = true;
            divVadiVill.Visible = false;
            //UWard.Visible = true;
        }
        else
        {
            lblPan.Text = "ग्राम पंचायत";
            divVadiMoh.Visible = false;
            divVadiVill.Visible = true;
            //UWard.Visible = false;
        }
        bindVadiPanchayat();
        bindVadiVillage();
        bindVadiWard();
    }

    void BindVadi_Prativadi_Anya_Type()
    {
        try
        {


            string sql = @"select id, name from Vadi_Prativadi_Anya_Type order by id asc";

            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { });


            ddlIsVadiSanstha_type.DataSource = dt;
            ddlIsVadiSanstha_type.DataTextField = "name";
            ddlIsVadiSanstha_type.DataValueField = "id";
            ddlIsVadiSanstha_type.DataBind();
            ddlIsVadiSanstha_type.Items.Insert(0, new ListItem("--Select--", "0"));


            //ddlIsVadiSanstha_relation.DataSource = dt;
            //ddlIsVadiSanstha_relation.DataTextField = "name";
            //ddlIsVadiSanstha_relation.DataValueField = "id";
            //ddlIsVadiSanstha_relation.DataBind();
            //ddlIsVadiSanstha_relation.Items.Insert(0, new ListItem("--Select--", "0"));


            dt.Dispose();


        }
        catch { }
    }

    void BindVadi_Sanstha_Anya_Type()
    {
        try
        {


            string sql = @"SELECT id, name FROM mst_sanstha_ka_sambandh_type order by id asc";

            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { });


            ddlIsVadiSanstha_relation.DataSource = dt;
            ddlIsVadiSanstha_relation.DataTextField = "name";
            ddlIsVadiSanstha_relation.DataValueField = "id";
            ddlIsVadiSanstha_relation.DataBind();
            ddlIsVadiSanstha_relation.Items.Insert(0, new ListItem("--Select--", "0"));


            //ddlPsanshaanya_naam.DataSource = dt;
            //ddlPsanshaanya_naam.DataTextField = "name";
            //ddlPsanshaanya_naam.DataValueField = "id";
            //ddlPsanshaanya_naam.DataBind();
            //ddlPsanshaanya_naam.Items.Insert(0, new ListItem("--Select--", "0"));


            dt.Dispose();


        }
        catch { }
    }
    protected void ddlIsVadiDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIsVadiDept.SelectedValue == "Y")
        {
            divVadiDptName.Visible = true;
            divVadiDesig.Visible = true;
            ddlIsVadiSanstha.SelectedValue = "N";
            divSanstha.Visible = false;
            ddlIsVadiSanstha_type.SelectedIndex = 0;
            ddlIsVadiSanstha_relation.SelectedIndex = 0;
            txtVadiSansthaName.Text = "";
            txtVadiSansthaDesig.Text = "";
        }
        else if (ddlIsVadiDept.SelectedValue == "N")
        {
            divVadiDptName.Visible = false;
            divVadiDesig.Visible = false;
            ddlVadiDept.SelectedIndex = 0;
            txtVadiDesig.Text = "";
        }
    }

    protected void ddlIsVadiSanstha_SelectedIndexChanged (object sender, EventArgs e)
    {
        if (ddlIsVadiSanstha.SelectedValue == "Y")
        {
            divVadiDptName.Visible = false;
            divVadiDesig.Visible = false;
            divSanstha.Visible = true;
            ddlIsVadiDept.SelectedValue = "N";
            ddlVadiDept.SelectedIndex = 0;
            txtVadiDesig.Text = "";
        }
        else if (ddlIsVadiSanstha.SelectedValue == "N")
        {
            divVadiDptName.Visible = false;
            divVadiDesig.Visible = false;
            divSanstha.Visible = false;
            ddlIsVadiSanstha_type.SelectedIndex = 0;
            ddlIsVadiSanstha_relation.SelectedIndex = 0;
            txtVadiSansthaName.Text = "";
            txtVadiSansthaDesig.Text = "";
        }
    }
    protected void bindDepartment()//   departmnt
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_BindDepartment", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {

                ddlVadiDept.DataSource = dt;
                ddlVadiDept.DataTextField = "name";
                ddlVadiDept.DataValueField = "id";
                ddlVadiDept.DataBind();
                ddlVadiDept.Items.Insert(0, new ListItem("--Select--", "0"));

                //ddlPvibhaag_naam.DataSource = dt;
                //ddlPvibhaag_naam.DataTextField = "name";
                //ddlPvibhaag_naam.DataValueField = "id";
                //ddlPvibhaag_naam.DataBind();
                //ddlPvibhaag_naam.Items.Insert(0, new ListItem("--Select--", "0"));


            }
        }
        catch (Exception ee)
        {


        }
    }
    protected void yearOfBirth ()
    {
        string[] retVal = new string[122]; ;
        int index = 0;
        for (int i = 2021; i >= 1900; i--)
        {
            retVal[index] = i.ToString();
            index = index + 1;
        }

        ddlYear.DataSource = retVal;
        ddlYear.DataBind();
        ddlYear.Items.Insert(0, new ListItem("--Select--", "0"));

        return;
    }
    



    private void bindPvadiDistrict()
    {
        //clsData.FillDropDownList(ddlPvadiDist, "SELECT DISTRICTCODE,DISTRICTNAME FROM mst_Commissionary_Districts order by DISTRICTNAME", "DISTRICTCODE", "DISTRICTNAME", "--Select--", new SqlParameter[] { });
    }
    private void bindPvadiSubDivision()
    {
        //clsData.FillDropDownList(ddlPvadiSub, "select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En", "SubDivisionCode", "SubDivisionName", "--Select--", new SqlParameter[] { new SqlParameter("@District_Code", ddlPvadiDist.SelectedValue) });
    }
    private void bindPvadiCircle()
    {
        //clsData.FillDropDownList(ddlPvadiCircle, "select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName", "BlockCode", "BlockName", "--Select--", new SqlParameter[] { new SqlParameter("@District_Code", ddlPvadiDist.SelectedValue), new SqlParameter("@Subdivision_Code", ddlPvadiSub.SelectedValue) });
    }

    private void bindPvadiPS()
    {
       // clsData.FillDropDownList(ddlPvadiPS, "Select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t left join MstThanaMapping m on m.Thana_Code = t.PS_Code left join Blocks b on b.BlockCode = m.Code and m.Type = 'Block' where District_code = @District_Code and  b.SubDivCode is not null and m.code = @Circle_Code and b.SubDivCode = @Subdivision_Code ORDER BY Police_Station", "PS_Code", "Police_Station", "--Select--", new SqlParameter[] { new SqlParameter("@District_Code", ddlPvadiDist.SelectedValue), new SqlParameter("@Subdivision_Code", ddlPvadiSub.SelectedValue), new SqlParameter("@Circle_Code", ddlPvadiCircle.SelectedValue) });
    }
    private void bindPvadiPanchayat()
    {
        //clsData.FillDropDownList(ddlPvadiPanch, "select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatName", "PanchayatCode", "PanchayatName", "--Select--", new SqlParameter[] { new SqlParameter("@BlockCode", ddlPvadiCircle.SelectedValue), new SqlParameter("@AreaType", ddlPvadiAreaType.SelectedValue) });
    }
    private void bindPvadiVillage()
    {
        //clsData.FillDropDownList(ddlPvadiVill, "select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p inner join PanchayatVillage pv on p.PanchayatCode = pv.PanchayatCode inner join mst_VillageMaster v on v.VILLCODE = pv.VillageCode where p.PanchayatCode = @PanchayatCode order by v.VILLNAME", "VILLCODE", "VILLNAME", "--Select--", new SqlParameter[] { new SqlParameter("@PanchayatCode", ddlPvadiPanch.SelectedValue) });
    }
    private void bindPvadiWard()
    {
       // clsData.FillDropDownList(ddlPvadiWard, "select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME", "WARDCODE", "WARDNAME", "--Select--", new SqlParameter[] { new SqlParameter("@Panchayat", ddlPvadiPanch.SelectedValue), new SqlParameter("@AreaType", ddlPvadiAreaType.SelectedValue) });
    }


    protected void ddlPvadiDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPvadiSubDivision();
        bindPvadiCircle();
        bindPvadiPS();
        bindPvadiPanchayat();
        bindPvadiVillage();
        bindPvadiWard();
        ddlPvadiAreaType.SelectedIndex = 0;
    }

    protected void ddlPvadiSubDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPvadiCircle();
        bindPvadiPS();
        bindPvadiPanchayat();
        bindPvadiVillage();
        bindPvadiWard();
        ddlPvadiAreaType.SelectedIndex = 0;
    }
    protected void ddlPvadiCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPvadiPS();
        bindPvadiPanchayat();
        bindPvadiVillage();
        bindPvadiWard();
        ddlPvadiAreaType.SelectedIndex = 0;
    }
    protected void ddlPvadiPanchayat_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPvadiVillage();
        bindPvadiWard();
    }

    protected void ddlPvadiVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindPvadiWard();
    }

    protected void ddlVadiDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindPvadiWard();
    }







    protected void ddlPvadiPS_SelectedIndexChanged(object sender, EventArgs e)
    {
        //bindPS();
    }

    protected void ddlPvadiAreaType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPvadiAreaType.SelectedIndex == 2)
        {
            lblPvadiPan.Text = "नगर निकाय";
            divPvadiMoh.Visible = true;
            divPvadiVill.Visible = false;
            //UWard.Visible = true;
        }
        else
        {
            lblPvadiPan.Text = "ग्राम पंचायत";
            divPvadiMoh.Visible = false;
            divPvadiVill.Visible = true;
            //UWard.Visible = false;
        }
        bindPvadiPanchayat();
        bindPvadiVillage();
        bindPvadiWard();
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        int sn = 0;
        DataTable dt = new DataTable();
        dt.Columns.Add("SlNo", typeof(Int32));
        dt.Columns.Add("Qualification", typeof(string));
        dt.Columns.Add("Qualificationnew", typeof(string));
        dt.Columns.Add("QualificationType", typeof(Int32));
        dt.Columns.Add("Marks", typeof(float));

        DataRow dr = dt.NewRow();
        dr["SlNo"] = ++sn;
        dr["Qualification"] = "Matric";
        dr["Qualificationnew"] = "Inter";
        dr["QualificationType"] = "1";
        dr["Marks"] = "72";
        dt.Rows.Add(dr);

      

        gvFinal.DataSource = dt;
        gvFinal.DataBind();

    }



}