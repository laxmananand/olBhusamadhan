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
using System.Net;
using System.Text;
using System.Linq;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
public partial class LandDispute_Entry_Entry_Page : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    whiteList wl = new whiteList();
    protected void Page_Load(object sender, EventArgs e)
    {
        //ViewState["BhumiKaViVaranDT"] = BhumiKaViVaranDT();
        if (Convert.ToString(Session["Role"]) == null || Convert.ToString(Session["Block_Code"]) == null || Convert.ToString(Session["Thana_Code"]) == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
        }
        if(Convert.ToString(Session["Role"])!="PUBLIC")
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
        }
        if (!IsPostBack)
        {
            //checkuserentry();

            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
           // string UrlRedirect = enc.Encrypt("149878");
           //ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('आवेदनकर्ता का विवरण सफलतापूर्वक सहेजा गया'); window.location='getDataPublic.aspx?RegId=" + UrlRedirect + "';", true);
            //return;
            txtVadiVivarani.Attributes.Add("maxlength", txtVadiVivarani.MaxLength.ToString());

          
            txtUserMohalla.Attributes.Add("maxlength", txtUserMohalla.MaxLength.ToString());
           
            
            txtAwadenKiTithi.Attributes.Add("readonly", "readonly");
            txtAwadenKiTithi.Text = DateTime.Now.ToString("dd-MM-yyyy");


            bindDepartment();
            BindVadi_Prativadi_Anya_Type();
            BindVadi_Sanstha_Anya_Type();
            BindDist_Wadi_Pratiwadi();
            AdharYearsBind();
            bindDepartment();
            BindVadi_Prativadi_Anya_Type();
            bind_bhumivivad_ki_adyatan_sthiti();
            bindbumitype();
            bindSarkariBumitype();
            bind_bhumivivad_Type();
            
          
           

            BindBlock_Wadi();
            BindPolice_wadi();
            BindPanchyat_Wadi();
            BindVillage_Wadi();
            bindward_Wadi();
            BindPanchyat();
            BindVillage();
            bindward();
           
            divSarkaribhumitype.Visible = false;
            //labSarkaribhumitype.Visible = false;
            ImgSarkaribhumitype.Visible = false;
            //ddlsarkaribhumitype.Enabled = false;
            //ddlsarkaribhumitype.Visible = false;

            divBhumivivad_Anya.Visible = false;
            divBhumivivad_Anya.Visible = false;
            //txtbhumivivad_Anya.Visible = false;
            txtbhumivivad_Anya.Enabled = false;
            txtbhumivivad_Anya.Text = "";



            divsarkaribhumitype_Anya.Visible = false;
            divsarkaribhumitype_Anya.Visible = false;
            txtsarkaribhumitype_Anya.Visible = false;
            txtsarkaribhumitype_Anya.Enabled = false;
            txtsarkaribhumitype_Anya.Text = "";



            ViewState["vadiDetails"] = vadiDetails();
            ViewState["pratiVadiDetails"] = pratiVadiDetails();
            //satrt
            ViewState["a_id"] = 0;
            ViewState["Step"] = 1;

            //end
            //ViewState["a_id"] = 0;
            //ViewState["Step"] = 3;
            //step1.Visible = false;
            //step2.Visible = false;
            //step3.Visible = true;


            BindDist();
            //pratiwadi
          

         
            txtbhumivivad_Anya.Enabled = false;

            if (Session["District_Code"] != null)
            {
                //ddlDistrict.SelectedValue = Session["District_Code"].ToString();
               // ddlDistrict.Enabled = false;
            }
            BindSubDivision();
            if (Session["Sub_DivCode"] != null)
            {
               // ddlSubdivision.SelectedValue = Session["Sub_DivCode"].ToString();
                // ddlSubdivision.Enabled = false;
            }
            BindBlock();
            if (Session["Block_Code"] != null)
            {
                //ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                // ddlBlock.Enabled = false;
            }
            BindPolice();
            if (Session["Thana_Code"] != null)
            {
                //ddlPolice.SelectedValue = Session["Thana_Code"].ToString();
                //if (ddlPolice.SelectedValue.Trim() != "0")
                //{
                //    ddlPolice.Enabled = false;
                //}
            }
            txtNamePerAadhaar.Text = Session["Username"].ToString();
           txtvadimobile.Text = Session["UserID"].ToString();

            //step 2
            bindLandUnit();
            bind_khatiyan_Type();

            ViewState["BhumiKaViVaranDT"] = BhumiKaViVaranDT();
            BindGridhumiKaVivaran();
            ViewState["VadiEvidenceDetailsDT"] = VadiEvidenceDetailsDT();
            bindLandEvidence();
            BindDist2();
           // Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            if (Request.QueryString["RegId"] != null)
            {
                string Id = Request.QueryString["RegId"].ToString();
                //Id = enc.Decrypt(Id);

                SqlParameter _a_id = new SqlParameter("@a_id", Id);
                SqlParameter _CUUser = new SqlParameter("@CUUser", Session["UserID"].ToString());
                DataTable dtwadi = clsData.GetDataTableWithProc("usp_checkVlaidUserPublic", new SqlParameter[] { _a_id, _CUUser });
                if (dtwadi.Rows.Count > 0)
                {
                    hfGetApplicationno.Value = dtwadi.Rows[0]["ApplicationNo"].ToString();
                    hfGetdateAvadan.Value = dtwadi.Rows[0]["AavedanKiTithi"].ToString();
                    ViewState["a_id"] = Id;
                    //ViewState["Step"] = 4;
                    ViewState["Step"] = 1;
                    MatterRegistrationGetVadi();
                }
                else
                {
                    Response.Redirect("viewdata.aspx");
                }

            }


        }
    }
    private DataTable BhumiDT
    {
        get
        {
            DataTable dt = ViewState["BhumiKaViVaranDT"] as DataTable;

            // 🔥 structure validation
            if (dt == null ||
                dt.Columns["Vivadith_halka"].DataType != typeof(string))
            {
                dt = BhumiKaViVaranDT();
                ViewState["BhumiKaViVaranDT"] = dt;
            }

            return dt;
        }
    }

    #region Wadi Details
    private string GetUserIP()
    {
        string iMainpaddress = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(iMainpaddress))
        {
            return iMainpaddress.Split(',')[0];
        }

        return Request.ServerVariables["REMOTE_ADDR"];
    }
    private string checkAadhaarNo(string AadhaarNo)
    {
        string Ad = string.Empty;

        try
        {
            string sql = "";
            sql = @"select AadharNo from VadiDetailEntry where AadharNo= @AadharNo";
            SqlParameter AadharNo = new SqlParameter("@AadharNo", AadhaarNo);
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { AadharNo });
            if (dt.Rows.Count > 0)
            {
                Ad = Microsoft.Security.Application.Encoder.HtmlEncode(Convert.ToString(dt.Rows[0]["AadharNo"]));
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msgSuccContEdit", "alert('" + ex.Message.ToString() + "');", true);

        }
        return Ad;
    }
    protected string changeDate(string dateFormate)
    {
        string[] a = dateFormate.Split('/');
        string date = a[2] + '-' + a[1] + '-' + a[0];
        return date;

    }
    protected void ddl_is_vadi_from_an_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_is_vadi_from_an_dept.SelectedValue == "Y")
        {
            divWVibhag_details.Visible = true;
            divWvibhaag_padanaam.Visible = true;
            ddl_is_vadi_from_an_org.SelectedValue = "N";
            ddl_is_vadi_from_an_org.Enabled = false;
            divWSanstha_details.Visible = false;
        }
        else if (ddl_is_vadi_from_an_dept.SelectedValue == "N")
        {
            divWVibhag_details.Visible = false;
            divWvibhaag_padanaam.Visible = false;
            ddl_is_vadi_from_an_org.SelectedValue = "0";
            ddl_is_vadi_from_an_org.Enabled = true;
            //divWSanstha_details.Visible = false;
        }
        else if (ddl_is_vadi_from_an_dept.SelectedValue == "0")
        {
            divWVibhag_details.Visible = false;
            divWvibhaag_padanaam.Visible = false;
            ddl_is_vadi_from_an_org.SelectedValue = "0";
            ddl_is_vadi_from_an_org.Enabled = true;
            divWSanstha_details.Visible = false;
        }
    }
    protected void checkuserentry()
    {

        string count=string.Empty;
        SqlParameter _CUUser = new SqlParameter("@CUUser", Session["UserID"].ToString());
        DataTable dt = clsData.GetDataTableWithProc("sp_checkuserentry", new SqlParameter[] {  _CUUser });
        if (dt.Rows.Count > 0)
        {
             count = dt.Rows[0]["Totalcount"].ToString();
            int totalCount = 0;
            int.TryParse(count, out totalCount);

            if (totalCount >= 1)
             {
                Utility.showMessage(Page, "You have already registered. Please wait until your previous registered application’s first meeting concludes.");

                // Ensure user stays on DefaultPublic.aspx
                Response.Redirect("~/DefaultPublic.aspx?msg=already", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            
          
        }   
    }
    protected bool CheckKhataKeshraEntry()
    {
        if (ViewState["BhumiKaViVaranDT"] == null)
            return false;

        DataTable src = (DataTable)ViewState["BhumiKaViVaranDT"];

        if (src.Rows.Count == 0)
            return false;

        // TVP ke structure ka DataTable
        DataTable dtCheck = new DataTable();
        dtCheck.Columns.Add("Halka", typeof(string));   // ✅ string
        dtCheck.Columns.Add("Mouja", typeof(string));   // ✅ string
        dtCheck.Columns.Add("Khata", typeof(string));
        dtCheck.Columns.Add("Khesra", typeof(string));

        foreach (DataRow r in src.Rows)
        {
            dtCheck.Rows.Add(
                r["Vivadith_halka"].ToString().Trim(),
                r["Vivadith_Mauza"].ToString().Trim(),
                r["khataNo"].ToString().Trim(),
                r["khesraNo"].ToString().Trim()
            );
        }

        // TVP parameter
        SqlParameter p = new SqlParameter("@BhumiData", dtCheck);
        p.SqlDbType = SqlDbType.Structured;
        p.TypeName = "dbo.BhumiDuplicateType1"; // SQL TVP type

        DataTable dt = clsData.GetDataTableWithProc(
            "sp_checkkhaatakeshra",
            new SqlParameter[] { p }
        );

        if (dt.Rows.Count > 0)
        {
            int totalCount;
            int.TryParse(dt.Rows[0]["Totalcount"].ToString(), out totalCount);

            if (totalCount > 0)
                return true; // ❌ duplicate found
        }

        return false; // ✅ safe
    }


    //protected bool CheckKhataKeshraEntry()
    //{
    //    SqlParameter _halka = new SqlParameter("@halka", ddlhalka_old.SelectedValue);
    //    SqlParameter _mouja = new SqlParameter("@mouja", ddlmowja_old.SelectedValue);
    //    SqlParameter _khata = new SqlParameter("@khata", txtkhatasankhya.Text.Trim());
    //    SqlParameter _keshra = new SqlParameter("@keshra", txtkhesarasankhya.Text.Trim());

    //    DataTable dt = clsData.GetDataTableWithProc("sp_checkkhaatakeshra", new SqlParameter[] { _halka, _mouja, _khata, _keshra });

    //    if (dt.Rows.Count > 0)
    //    {
    //        int totalCount = 0;
    //        int.TryParse(dt.Rows[0]["Totalcount"].ToString(), out totalCount);

    //        if (totalCount >= 1)
    //        {
    //            return true; // duplicate exists
    //        }
    //    }

    //    return false; // no duplicate found
    //}
    protected void bindDepartment()//   departmnt
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_BindDepartment", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlWvibhaag_naam.DataSource = dt;
                    ddlWvibhaag_naam.DataTextField = "name";
                    ddlWvibhaag_naam.DataValueField = "id";
                    ddlWvibhaag_naam.DataBind();
                    ddlWvibhaag_naam.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlPvibhaag_naam.DataSource = dt;
                    ddlPvibhaag_naam.DataTextField = "name";
                    ddlPvibhaag_naam.DataValueField = "id";
                    ddlPvibhaag_naam.DataBind();
                    ddlPvibhaag_naam.Items.Insert(0, new ListItem("--Select--", "0"));
                }
            }
        }
        catch (Exception ee)
        {


        }
    }
    protected void ddlWsanstha_naam_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    void BindVadi_Prativadi_Anya_Type()
    {
        try
        {


            string sql = @"select id, name from Vadi_Prativadi_Anya_Type order by id asc";

            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { });


            ddlWsanstha_naam.DataSource = dt;
            ddlWsanstha_naam.DataTextField = "name";
            ddlWsanstha_naam.DataValueField = "id";
            ddlWsanstha_naam.DataBind();
            ddlWsanstha_naam.Items.Insert(0, new ListItem("--Select--", "0"));


            ddlPsanstha_naam.DataSource = dt;
            ddlPsanstha_naam.DataTextField = "name";
            ddlPsanstha_naam.DataValueField = "id";
            ddlPsanstha_naam.DataBind();
            ddlPsanstha_naam.Items.Insert(0, new ListItem("--Select--", "0"));


            dt.Dispose();


        }
        catch { }
    }
    protected void ddlWsanshaanya_naam_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    void BindVadi_Sanstha_Anya_Type()
    {
        try
        {


            string sql = @"SELECT id, name FROM mst_sanstha_ka_sambandh_type order by id asc";

            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { });


            ddlWsanshaanya_naam.DataSource = dt;
            ddlWsanshaanya_naam.DataTextField = "name";
            ddlWsanshaanya_naam.DataValueField = "id";
            ddlWsanshaanya_naam.DataBind();
            ddlWsanshaanya_naam.Items.Insert(0, new ListItem("--Select--", "0"));


            ddlPsanshaanya_naam.DataSource = dt;
            ddlPsanshaanya_naam.DataTextField = "name";
            ddlPsanshaanya_naam.DataValueField = "id";
            ddlPsanshaanya_naam.DataBind();
            ddlPsanshaanya_naam.Items.Insert(0, new ListItem("--Select--", "0"));


            dt.Dispose();


        }
        catch { }
    }
    protected void ddlUserDist_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubDivision_wadi();
        BindBlock_Wadi();
        BindPolice_wadi();
        BindVillage_Wadi();
        BindPanchyat_Wadi();
        bindward_Wadi();
        ddlUserAreatype.SelectedIndex = 0;
    }
    void BindBlock_Wadi()
    {
        try
        {


            string sql = @" select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName";

            SqlParameter _DCode = new SqlParameter("@District_Code", ddlUserDist.SelectedValue.Trim());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlUserSubdivision.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode });


            ddlUserBlock.DataSource = dt;
            ddlUserBlock.DataTextField = "BlockName";
            ddlUserBlock.DataValueField = "BlockCode";
            ddlUserBlock.DataBind();
            ddlUserBlock.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();
        }
        catch { }


    }
    void BindPolice_wadi()
    {
        try
        {

            //string sql = @" select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t inner join Blocks p on t.Subdivision_Code =p.SubDivCode where p.DistCode=@District_Code  order by t.Police_Station";
            //string sql = @" select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t inner join SubDivisions p on t.Subdivision_Code =p.Sd_Code2 where p.Sd_Code2=@Subdivision_Code and p.DistCode=@District_Code and t.Circle_Code=@Circle_Code   order by Police_Station";
            string sql = @"select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t
	                        left join MstThanaMapping m on m.Thana_Code=t.PS_Code 
	                        left join Blocks b on b.BlockCode=m.Code and m.Type='Block'
	                        where District_code=@District_Code and  b.SubDivCode is not null and m.code=@Circle_Code and b.SubDivCode=@Subdivision_Code
                            ORDER BY Police_Station";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddlUserDist.SelectedValue.ToString());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlUserSubdivision.SelectedValue.ToString());
            SqlParameter _Circle_Code = new SqlParameter("@Circle_Code", ddlUserBlock.SelectedValue.ToString());
            //SqlParameter _RCode = new SqlParameter("@Range_code", ddlrange.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode, _Circle_Code });
            ddlUserThana.DataSource = dt;
            ddlUserThana.DataTextField = "Police_Station";
            ddlUserThana.DataValueField = "PS_Code";
            ddlUserThana.DataBind();
            ddlUserThana.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }
    }
    void BindVillage_Wadi()
    {
        try
        {



            string sql = @"select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p 
                            inner join PanchayatVillage pv on p.PanchayatCode=pv.PanchayatCode
                            inner join mst_VillageMaster v on v.VILLCODE=pv.VillageCode
                            where p.PanchayatCode=@PanchayatCode order by 	 v.VILLNAME	";



            SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", ddlUserPanchyat.SelectedValue.Trim());

            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _PanchayatCode });


            ddlUserVillage.DataSource = dt;
            ddlUserVillage.DataTextField = "VILLNAME";
            ddlUserVillage.DataValueField = "VILLCODE";
            ddlUserVillage.DataBind();
            ddlUserVillage.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlUserVillage.Items.Add(new ListItem("अन्य", "-1"));
            dt.Dispose();


        }
        catch { }
    }
    void BindPanchyat_Wadi()
    {
        try
        {


            string sql = @" select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatName";

            //SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _BlockCode = new SqlParameter("@BlockCode", ddlUserBlock.SelectedValue.Trim());
            SqlParameter _AreaType = new SqlParameter("@AreaType", ddlUserAreatype.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _BlockCode, _AreaType });


            ddlUserPanchyat.DataSource = dt;
            ddlUserPanchyat.DataTextField = "PanchayatName";
            ddlUserPanchyat.DataValueField = "PanchayatCode";
            ddlUserPanchyat.DataBind();
            ddlUserPanchyat.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlUserPanchyat.Items.Add(new ListItem("अन्य", "-1"));


        }
        catch { }

        // bindward_Wadi();
    }
    void bindward_Wadi()
    {
        try
        {
            SqlParameter Pan = new SqlParameter("@Panchayat", ddlUserPanchyat.SelectedValue);
            SqlParameter AreaType = new SqlParameter("@AreaType", ddlUserAreatype.SelectedValue);
            string sql = @"select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME";
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { Pan, AreaType });

            ddlUserWard.DataSource = dt;
            ddlUserWard.DataTextField = "WARDNAME";
            ddlUserWard.DataValueField = "WARDCODE";
            ddlUserWard.DataBind();
            ddlUserWard.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlUserWard.Items.Add(new ListItem("अन्य", "-1"));
        }
        catch (Exception)
        {

        }
    }
    protected void ddlUserSubdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock_Wadi();
        BindPolice_wadi();
        BindVillage_Wadi();
        BindPanchyat_Wadi();
        bindward_Wadi();
        ddlUserAreatype.SelectedIndex = 0;
    }
    protected void ddlUserBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPolice_wadi();
        BindVillage_Wadi();
        BindPanchyat_Wadi();
        bindward_Wadi();
        ddlUserAreatype_SelectedIndexChanged(sender, e);
        ddlUserAreatype.SelectedIndex = 0;
    }
    protected void ddlUserAreatype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlUserAreatype.SelectedIndex == 2)
        {
            labUVillage.Text = "नगर निकाय";
            divUserMohalla.Visible = true;
            divUserVillageCol.Visible = false;
            UWard.Visible = true;
        }
        else
        {
            labUVillage.Text = "ग्राम पंचायत";
            divUserMohalla.Visible = false;
            divUserVillageCol.Visible = true;
            UWard.Visible = false;
        }
        BindVillage_Wadi();
        BindPanchyat_Wadi();
        bindward_Wadi();

        ddlUserVillage_SelectedIndexChanged(sender, e);
        ddlUserWard_SelectedIndexChanged(sender, e);
        ddlUserPanchyat_SelectedIndexChanged(sender, e);
    }
    protected void ddlUserPanchyat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVillage_Wadi();
        bindward_Wadi();
        divUserPanchyat.Attributes.Add("class", "col-md-3");
        // divUserPanchyat_Anya.Visible = false;
        if (ddlUserPanchyat.SelectedValue == "-1")
        {
            //divUserPanchyat.Attributes.Add("class", "col-md-5");
            // divUserPanchyat_Anya.Visible = true;
        }

        ddlUserVillage_SelectedIndexChanged(sender, e);
        ddlUserWard_SelectedIndexChanged(sender, e);
    }
    protected void ddlUserVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        divUserVillage.Attributes.Add("class", "col-md-12");
        //divUserVillage_Anya.Visible = false;
        if (ddlUserVillage.SelectedValue == "-1")
        {
            //divUserVillage.Attributes.Add("class", "col-md-5");
            //divUserVillage_Anya.Visible = true;
        }

        ddlUserWard_SelectedIndexChanged(sender, e);

    }
    protected void ddlUserWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        divUserWard.Attributes.Add("class", "col-md-3");
        //divUserWard_Anya.Visible = false;
        if (ddlUserWard.SelectedValue == "-1")
        {
            // divUserWard.Attributes.Add("class", "col-md-5");
            //divUserWard_Anya.Visible = true;
        }
    }
    protected void ddl_is_vadi_from_an_org_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlWsanstha_naam.SelectedIndex = 0;
        txtWsanstha_padanaam.Text = "";
        divWSanstha_details.Visible = false;
        txtWsanstha_naam.Text = "";
        if (ddl_is_vadi_from_an_org.SelectedIndex == 1)
        {
            divWSanstha_details.Visible = true;
        }
    }
    public bool ValidateVadiDetail()
    {
        if (string.IsNullOrWhiteSpace(txtNamePerAadhaar.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया वादी का नाम अंकित करें...!');", true);
            txtNamePerAadhaar.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtFName.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया पिता/ पति का नाम अंकित करें...!');", true);
            txtFName.Focus();
            return false;
        }

        if (ddlgender.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया लिंग चुनें...!');", true);
            ddlgender.Focus();
            return false;
        }
        if (ddlUserDist.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया जिला चुनें...!');", true);
            ddlUserDist.Focus();
            return false;
        }

        if (ddlUserSubdivision.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अनुमंडल चुनें...!');", true);
            ddlUserSubdivision.Focus();
            return false;
        }

        if (ddlUserBlock.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अंचल चुनें...!');", true);
            ddlUserBlock.Focus();
            return false;
        }

        if (ddlUserThana.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया थाना चुनें...!');", true);
            ddlUserThana.Focus();
            return false;
        }

        if (ddlUserAreatype.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया क्षेत्र का प्रकार चुनें...!');", true);
            ddlUserAreatype.Focus();
            return false;
        }

        if (ddlUserPanchyat.SelectedIndex == 0)
        {

            if (labUVillage.Text == "ग्राम पंचायत")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया ग्राम पंचायत चुनें...!');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया नगर निकाय चुनें...!');", true);
            }
            ddlUserPanchyat.Focus();
            return false;
        }
        if (ddlUserAreatype.SelectedIndex == 1 && ddlUserVillage.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया राजस्व ग्राम चुनें...!');", true);
            ddlUserVillage.Focus();
            return false;
        }
        if (ddlUserAreatype.SelectedIndex == 2 && ddlUserWard.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया वार्ड चुनें...!');", true);
            ddlUserWard.Focus();
            return false;
        }
        //if (string.IsNullOrWhiteSpace(txtUserMohalla.Text))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया मोहल्ला संख्या अंकित करें...!');", true);
        //    txtUserMohalla.Focus();
        //    return false;
        //}

        if (string.IsNullOrWhiteSpace(txtvadimobile.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया मोबाइल संख्या अंकित करें...!');", true);
            txtvadimobile.Focus();
            return false;
        }

        if (txtvadimobile.Text.Length != 10)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Enter valid mobile no...!');", true);
            txtvadimobile.Focus();
            return false;
        }
        if (ddl_is_vadi_from_an_dept.SelectedIndex == 0 || ddl_is_vadi_from_an_dept.SelectedIndex == 1)
        {
            if (ddl_is_vadi_from_an_dept.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('क्या वादी किसी विभाग का प्रतिनिधि है कृपया चुनें...!');", true);
                ddl_is_vadi_from_an_dept.Focus();
                return false;
            }
            if (ddlWvibhaag_naam.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया विभाग का नाम चुनें...!');", true);
                ddlWvibhaag_naam.Focus();
                return false;
            }
        }
        if (ddl_is_vadi_from_an_org.SelectedIndex == 0 || ddl_is_vadi_from_an_org.SelectedIndex == 1)
        {
            if (ddl_is_vadi_from_an_org.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('क्या वादी किसी संस्था का प्रतिनिधि है कृपया चुनें...!');", true);
                ddl_is_vadi_from_an_org.Focus();
                return false;
            }
            if (ddlWsanstha_naam.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया संस्था का प्रकार चुनें...!');", true);
                ddlWsanstha_naam.Focus();
                return false;
            }
            if (ddlWsanshaanya_naam.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया संस्था का सम्बन्ध चुनें...!');", true);
                ddlWsanshaanya_naam.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtWsanstha_naam.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया संस्था का नाम अंकित करें...!');", true);
                txtWsanstha_naam.Focus();
                return false;
            }
        }
        return true;
    }
    protected void btnAddVadiDetail_Click(object sender, EventArgs e)
    {
        if (ValidateVadiDetail())
        {
            try
            {

                DataTable dt = (DataTable)ViewState["vadiDetails"];
                dt.Rows.Add(
                   

                          txtNamePerAadhaar.Text.Trim(),
                          ddl_is_vadi_from_an_org.SelectedValue.Trim(),
                          ddlWsanstha_naam.SelectedValue.Trim() != "0" ? ddlWsanstha_naam.SelectedValue.Trim() : null,
                           txtWsanstha_naam.Text.Trim(),
                          txtWsanstha_padanaam.Text.Trim(),
                          ddl_is_vadi_from_an_dept.SelectedValue.Trim(),
                          ddlWvibhaag_naam.SelectedValue.Trim() != "0" ? ddlWvibhaag_naam.SelectedValue.Trim() : null,
                          txtWvibhaag_padanaam.Text.Trim(),
                          txtFName.Text.Trim(),
                          txtNamePerAadhaar.Text.Trim(),
                          "",//txtAadhaarNumber.Text.Trim(), 
                          ddlYear.SelectedValue.Trim() != "0" ? ddlYear.SelectedValue.Trim() : null,
                          ddlgender.SelectedValue.ToUpper(),
                          ddlUserDist.SelectedValue.Trim(),
                          ddlUserSubdivision.SelectedValue.Trim(),
                          ddlUserBlock.SelectedValue.Trim(),
                          ddlUserThana.SelectedValue.Trim(),
                          ddlUserAreatype.SelectedValue.Trim(),
                          ddlUserPanchyat.SelectedValue.Trim(),
                          ddlUserVillage.SelectedValue.Trim(),
                          ddlUserWard.SelectedValue.Trim(),
                          txtvadimobile.Text.Trim(),
                          'N',
                         txtUserPanchyat_Anya.Text.Trim(),
                         txtUserVillage_Anya.Text.Trim(),
                         txtUserWard_Anya.Text.Trim(),
                         txtUserMohalla.Text.Trim(),
                         ddlWsanshaanya_naam.SelectedValue.Trim() != "0" ? ddlWsanshaanya_naam.SelectedValue.Trim() : null,
                       



                         ddl_is_vadi_from_an_dept.SelectedValue == "Y" ? ddlWvibhaag_naam.SelectedItem.Text : ddl_is_vadi_from_an_org.SelectedValue == "Y" ? ddlWvibhaag_naam.SelectedItem.Text : "",
                         ddlUserDist.SelectedItem.Text == "" ? "" : ddlUserDist.SelectedItem.Text,
                         ddlUserSubdivision.SelectedItem.Text == "" ? "" : ddlUserSubdivision.SelectedItem.Text,
                         ddlUserBlock.SelectedItem.Text == "" ? "" : ddlUserBlock.SelectedItem.Text,
                         ddlUserThana.SelectedItem.Text == "" ? "" : ddlUserThana.SelectedItem.Text,
                         ddlUserAreatype.SelectedItem.Text == "" ? "" : ddlUserAreatype.SelectedItem.Text,
                         ddlUserPanchyat.SelectedItem.Text == "" ? "" : ddlUserPanchyat.SelectedItem.Text,
                         ddlUserVillage.SelectedValue == "0" ? "" : ddlUserVillage.SelectedItem.Text,
                         ddlUserWard.SelectedItem.Text == "" ? "" : ddlUserWard.SelectedItem.Text,
                         ddlWsanshaanya_naam.SelectedValue.Trim() != "0" ? ddlWsanshaanya_naam.SelectedItem.Text : null,

                           txtPostOffice.Text,
                        txtPinCode.Text


                         );
                ViewState["vadiDetails"] = dt;
                BindWadiGrid();
                hfwadiprint.Value = "Printstep1";
                //string str = hfwadiprint.Value;
                ddl_is_vadi_from_an_dept.SelectedValue = "0";
                divWVibhag_details.Visible = false;
                divWvibhaag_padanaam.Visible = false;
                ddl_is_vadi_from_an_org.Enabled = true;
                ddl_is_vadi_from_an_org.SelectedValue = "0";
                divWSanstha_details.Visible = false;
                ddl_is_vadi_from_an_org.SelectedIndex = 0;
                ddlWsanstha_naam.SelectedIndex = 0;
                txtWsanstha_naam.Text = "";
                txtWsanstha_padanaam.Text = "";
                ddl_is_vadi_from_an_dept.SelectedIndex = 0;
                txtWvibhaag_padanaam.Text = "";

                ddlWvibhaag_naam.SelectedIndex = 0;
                ddlWsanshaanya_naam.SelectedIndex = 0;
                txtNamePerAadhaar.Text = "";

                ddlgender.SelectedIndex = 0;
                txtFName.Text = "";
                ddlUserDist.SelectedIndex = 0;
                ddlUserSubdivision.SelectedIndex = 0;
                ddlUserBlock.SelectedIndex = 0;
                ddlUserThana.SelectedIndex = 0;
                ddlUserAreatype.SelectedIndex = 0;
                ddlUserPanchyat.SelectedIndex = 0;
                ddlUserVillage.SelectedIndex = 0;
                ddlUserWard.SelectedIndex = 0;
                txtvadimobile.Text = "";

                txtUserPanchyat_Anya.Text = "";
                txtUserVillage_Anya.Text = "";
                txtUserWard_Anya.Text = "";
                txtUserMohalla.Text = "";

                DtxtNamePerAadhaar.Text = txtNamePerAadhaar.Text;
                DtxtNamePerAadhaar.Visible = false;
                txtNamePerAadhaar.Visible = true;
                Dtxtdatebirth.Text = ddlYear.SelectedValue.Trim();
                Dtxtdatebirth.Visible = false;
                ddlYear.Visible = true;
                Dddlgender.Text = ddlgender.SelectedItem.Text;
                Dddlgender.Visible = false;
                ddlgender.Visible = true;

                pnlupdate1.Update();
            }
            catch (Exception ex)
            {

            }
        }
    }
    protected void btnClearAadhaar_Click(object sender, EventArgs e)
    {

        txtNamePerAadhaar.Visible = true;
        txtNamePerAadhaar.Text = "";



        ddlgender.SelectedIndex = 0;
        ddlgender.Visible = true;

        txtFName.Text = "";

        txtvadimobile.Text = "";


        DtxtNamePerAadhaar.Text = "";
        DtxtNamePerAadhaar.Visible = false;
        Dtxtdatebirth.Text = "";
        Dtxtdatebirth.Visible = false;
        ddlYear.Visible = true;
        ddlYear.SelectedIndex = 0;
        Dddlgender.Text = "";
        Dddlgender.Visible = false;

    }
    protected void wadi_grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            string rowIndex = e.CommandArgument.ToString();
            DataTable dt = (DataTable)ViewState["vadiDetails"];
            dt.Rows.RemoveAt(Convert.ToInt32(rowIndex));
            ViewState["vadiDetails"] = dt;
            BindWadiGrid();

        }
    }
    protected void BindWadiGrid()
    {
        try
        {
            wadi_grid.DataSource = (DataTable)ViewState["vadiDetails"];
            wadi_grid.DataBind();
        }
        catch { }
    }
    private DataTable vadiDetails()
    {
        DataTable dt = new DataTable();
        dt.Clear();
       

        dt.Columns.Add("vadi_Name", typeof(string));
        dt.Columns.Add("is_vadi_from_an_org", typeof(string));
        dt.Columns.Add("vadi_org_type", typeof(int));
        dt.Columns.Add("vadi_org_name", typeof(string));
        dt.Columns.Add("vadi_org_pad_name", typeof(string));
        dt.Columns.Add("is_vadi_from_an_dept", typeof(string));
        dt.Columns.Add("vadi_dept_name", typeof(string));
        dt.Columns.Add("vadi_dept_pad_name", typeof(string));
        dt.Columns.Add("Vadi_Father_Husband_Name", typeof(string));
        dt.Columns.Add("NameAsPerAadhaar", typeof(string));
        dt.Columns.Add("AadharNo", typeof(string));
        dt.Columns.Add("YearOfBirthAsPerAadhaar", typeof(int));
        dt.Columns.Add("SexAsPerAadhaar", typeof(string));
        dt.Columns.Add("Vadi_District_Code", typeof(string));
        dt.Columns.Add("Vadi_Sub_DivCode", typeof(string));
        dt.Columns.Add("Vadi_Block_Code", typeof(string));
        dt.Columns.Add("Vadi_Thana_code", typeof(string));
        dt.Columns.Add("Vadi_AreaType", typeof(string));
        dt.Columns.Add("Vadi_Panchayat_Code", typeof(string));
        dt.Columns.Add("Vadi_Village_Code", typeof(string));
        dt.Columns.Add("Vadi_WardNo", typeof(string));
        dt.Columns.Add("Vadi_MobileNo", typeof(string));
        dt.Columns.Add("IsVerifyAadhaa", typeof(string));
        dt.Columns.Add("Vadi_Panchayat_Anya", typeof(string));
        dt.Columns.Add("Vadi_Village_Anya", typeof(string));
        dt.Columns.Add("Vadi_WardNo_Anya", typeof(string));
        dt.Columns.Add("mohalla", typeof(string));
        dt.Columns.Add("sanstha_sambandh_type", typeof(int));
       

        //Remove Cooolumn Save Time
        dt.Columns.Add("org_type", typeof(string));
        dt.Columns.Add("dist", typeof(string));
        dt.Columns.Add("sub_division", typeof(string));
        dt.Columns.Add("block", typeof(string));
        dt.Columns.Add("thana", typeof(string));
        dt.Columns.Add("area_type", typeof(string));
        dt.Columns.Add("panchayt", typeof(string));
        dt.Columns.Add("village", typeof(string));
        dt.Columns.Add("WardNo", typeof(string));
        dt.Columns.Add("associationName", typeof(string));


        dt.Columns.Add("PostOffice", typeof(string));
        dt.Columns.Add("PinCode", typeof(string));

        return dt;
    }
    #endregion
    #region upload file on server
    private string InsSaveFile_old(string fileName, FileUpload fuFile, string a_id)
    {
        string uploadDirectory = string.Empty;
        string pdfpath = FileSaveServer.getBase64(fuFile);
        string extension = string.Empty;
        extension = Path.GetExtension(fuFile.FileName).ToLower();
        string fn = "ID" + a_id;
        uploadDirectory = "~/LandDoc/Upload/" + fn + "/";
        string resi = FileSaveServer.InsertPDFNew(uploadDirectory, pdfpath, fileName, extension);
        // Utility.showMessage(Page, resi);
        return resi;
        //return uploadDirectory + fileName + extension;
    }
    private string InsSaveFile_old(string fileName, FileUpload fuFile, string a_id, string path)
    {
        string uploadDirectory = string.Empty;
        string pdfpath = FileSaveServer.getBase64(fuFile);
        string extension = string.Empty;
        extension = Path.GetExtension(fuFile.FileName).ToLower();
        uploadDirectory = path;
        string resi = FileSaveServer.InsertPDFNew(uploadDirectory, pdfpath, fileName, extension);
        return uploadDirectory + fileName + extension;
    }

    private string InsSaveFile(string fileName, FileUpload fuFile, string a_id)
    {
        string uploadDirectory = string.Empty;
        string pdfpath = FileSaveServer.getBase64(fuFile);
        string extension = string.Empty;
        extension = Path.GetExtension(fuFile.FileName).ToLower();
        string fn = "ID"+a_id;
        uploadDirectory = "~/LandDoc/Upload/" + fn + "/";
        string resi = FileSaveServer.InsertPDFNew(uploadDirectory, pdfpath, fileName, extension);
        //Console.WriteLine("Result: " + resi);
        //Response.Write("<pre style='color:red'>" + resi + "</pre>");
        // Utility.showMessage(Page, resi);
        return resi;
        //return uploadDirectory + fileName + extension;
    }
    private string InsSaveFile(string fileName, FileUpload fuFile, string a_id, string path)
    {
        string uploadDirectory = string.Empty;
        string pdfpath = FileSaveServer.getBase64(fuFile);
        string extension = string.Empty;
        extension = Path.GetExtension(fuFile.FileName).ToLower();
        uploadDirectory = path;
        string resi = FileSaveServer.InsertPDFNew(uploadDirectory, pdfpath, fileName, extension);
        //if (resi == "0")
        //{
        //    return resi;
        //}
        //else
        //{
        //    return uploadDirectory + fileName + extension;
        //}
        return resi;
    }

    #endregion

    #region save Date FirstStep

    public bool validation()
    {
        bool flag = true;


        return flag;
    }

    #endregion

    #region

    void BindDist()
    {
        try
        {
            string sql = @"SELECT distinct DISTRICTNAME,DISTRICTCODE from mst_Commissionary_Districts ORDER BY DISTRICTNAME ";
            DataTable dt = clsData.GetDataTable(sql);
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

    }
    void BindSubDivision()
    {
        try
        {


            string sql = @" select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En";
            //string sql = @"select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where Sd_Code2 in (select SubDivCode from Blocks where BlockCode in(select code from MstThanaMapping where thana_code=@thana_code)) and sd.DistCode=@District_Code";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
           // SqlParameter _thana_code = new SqlParameter("@thana_code", Session["Thana_Code"].ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode });


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
            //string sql = @"select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and BlockCode in (select code from MstThanaMapping where thana_code=@thana_code)  order by BlockName";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString());
            //SqlParameter _thana_code = new SqlParameter("@thana_code", Session["Thana_Code"].ToString());
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
    void BindPolice()
    {
        try
        {
            string sql = @" select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t inner join SubDivisions p on t.Subdivision_Code =p.Sd_Code2 where p.Sd_Code2=@Subdivision_Code and p.DistCode=@District_Code  order by Police_Station";

            sql = @"select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t 
                    where t.Subdivision_Code=@Subdivision_Code and t.District_code=@District_Code ";

            sql = @"select PS_Code,Police_Station from mst_thana m
                    inner join MstThanaMapping mp on m.PS_Code=mp.Thana_Code
                    inner join Blocks b on b.BlockCode=mp.Code
                    where b.BlockCode=@BlockCode
                    order by Police_Station
                    ";

            SqlParameter _BlockCode = new SqlParameter("@BlockCode", ddlBlock.SelectedValue.ToString());
            
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _BlockCode });
            ddlPolice.DataSource = dt;
            ddlPolice.DataTextField = "Police_Station";
            ddlPolice.DataValueField = "PS_Code";
            ddlPolice.DataBind();
            ddlPolice.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }
    }
    void BindVillage()
    {
        try
        {


            string sql = @"select DISTINCT VILLCODE, VILLNAME from mst_VillageMaster v inner join Blocks b on v.BLOCKCODE = b.BlockCode
                           inner join mst_Panchayats p on p.BlockCode=b.BlockCode where b.BlockCode=@BlockCode and p.AreaType=@AreaType order by VILLNAME asc";


            sql = @"select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p 
                            inner join PanchayatVillage pv on p.PanchayatCode=pv.PanchayatCode
                            inner join mst_VillageMaster v on v.VILLCODE=pv.VillageCode
                            where p.PanchayatCode=@PanchayatCode order by 	 v.VILLNAME
";
            // SqlParameter _BlockCode = new SqlParameter("@BlockCode", Session["Block_Code"].ToString());
            //SqlParameter _AreaType = new SqlParameter("@AreaType", ddlareatype.SelectedValue.ToString());
            SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", ddlPanchyat.SelectedValue.ToString());

            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _PanchayatCode });


            ddlVillage.DataSource = dt;
            ddlVillage.DataTextField = "VILLNAME";
            ddlVillage.DataValueField = "VILLCODE";
            ddlVillage.DataBind();
            ddlVillage.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlVillage.Items.Add(new ListItem("अन्य", "-1"));
            dt.Dispose();


        }
        catch { }
    }
    void BindPanchyat()
    {
        try
        {


            string sql = @" select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatNameHnd";

            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _BlockCode = new SqlParameter("@BlockCode", ddlBlock.SelectedValue.ToString());//Session["Block_Code"].ToString());
            SqlParameter _AreaType = new SqlParameter("@AreaType", ddlareatype.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _BlockCode, _AreaType });
            ddlPanchyat.DataSource = dt;
            ddlPanchyat.DataTextField = "PanchayatName";
            ddlPanchyat.DataValueField = "PanchayatCode";
            ddlPanchyat.DataBind();
            ddlPanchyat.Items.Insert(0, new ListItem("--Select--", "0"));
            // ddlPanchyat.Items.Add(new ListItem("अन्य", "-1"));
            dt.Dispose();
        }
        catch { }

    }
    void bindward()
    {
        try
        {

            SqlParameter Pan = new SqlParameter("@Panchayat", ddlPanchyat.SelectedValue);
            SqlParameter AreaType = new SqlParameter("@AreaType", ddlareatype.SelectedValue);
            string sql = @"  select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME";
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { Pan, AreaType });
            if (dt.Rows.Count > 0)
            {
                ddlWard.DataSource = dt;
                ddlWard.DataTextField = "WARDNAME";
                ddlWard.DataValueField = "WARDCODE";
                ddlWard.DataBind();
                ddlWard.Items.Insert(0, new ListItem("--Select--", "0"));
                // ddlWard.Items.Add(new ListItem("अन्य", "-1"));
            }

            else
            {

                dt = new DataTable();
                ddlWard.DataSource = dt;
                ddlWard.DataTextField = "WARDNAME";
                ddlWard.DataValueField = "WARDCODE";
                ddlWard.DataBind();
                ddlWard.Items.Insert(0, new ListItem("--Select--", "0"));
                // ddlWard.Items.Add(new ListItem("अन्य", "-1"));
            }
        }
        catch (Exception)
        {

        }
    }
    protected void bind_bhumivivad_ki_adyatan_sthiti()// भूमि विवाद की अद्यतन स्थिति  
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_GetBhumi_Vivad_adyatan_sthiti", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddl_vivad_adyatan_sthiti.DataSource = dt;
                    ddl_vivad_adyatan_sthiti.DataTextField = "status_name";
                    ddl_vivad_adyatan_sthiti.DataValueField = "id";
                    ddl_vivad_adyatan_sthiti.DataBind();
                    ddl_vivad_adyatan_sthiti.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
        }
        catch (Exception ee)
        {


        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubDivision();
        BindBlock();
        //BindPolice();
        BindVillage();
        BindPanchyat();
        bindward();
    }
    protected void ddlSubdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock();
        //BindPolice();
        BindVillage();
        BindPanchyat();
        bindward();
        ddlUserAreatype.SelectedIndex = 0;
        ddlareatype.SelectedIndex = 0;
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPolice();
        BindVillage();
        BindPanchyat();
        bindward();
        ddlareatype_SelectedIndexChanged(sender, e);
        ddlareatype.SelectedIndex = 0;
    }
    protected void ddlareatype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlareatype.SelectedIndex == 2)
        //{
        //    labVillage.Text = "नगर निकाय";
        //    imgWard_bhumiVivaran.Visible = true;
        //}
        //else
        //{
        //    labVillage.Text = "ग्राम पंचायत";
        //    imgWard_bhumiVivaran.Visible = false;
        //}


        if (ddlareatype.SelectedIndex == 2)
        {
            labVillage.Text = "नगर निकाय";
            imgWard_bhumiVivaran.Visible = true;
            //divUserMohalla.Visible = true;
            divVillage.Visible = false;
            //UWard.Visible = true;
        }
        else
        {
            labVillage.Text = "ग्राम पंचायत";
            imgWard_bhumiVivaran.Visible = false;
            // divUserMohalla.Visible = false;
            divVillage.Visible = true;
            // UWard.Visible = false;
        }


        BindVillage();
        BindPanchyat();
        bindward();
        ddlVillage_SelectedIndexChanged(sender, e);
        ddlWard_SelectedIndexChanged(sender, e);
        ddlPanchyat_SelectedIndexChanged(sender, e);



    }
    protected void ddlPanchyat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVillage();
        bindward();
        // divPanchyat.Attributes.Add("class", "col-md-12");
        //divPanchyat_Anya.Visible = false;
        if (ddlPanchyat.SelectedValue == "-1")
        {
            // divPanchyat.Attributes.Add("class", "col-md-5");
            //divPanchyat_Anya.Visible = true;
        }

        ddlVillage_SelectedIndexChanged(sender, e);
        ddlWard_SelectedIndexChanged(sender, e);
    }
    protected void ddlVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divVillage.Attributes.Add("class", "col-md-12");
        //divVillage_Anya.Visible = false;
        if (ddlVillage.SelectedValue == "-1")
        {
            // divVillage.Attributes.Add("class", "col-md-5");
            //divVillage_Anya.Visible = true;
        }
        ddlWard_SelectedIndexChanged(sender, e);
    }
    protected void ddlWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divWard.Attributes.Add("class", "col-md-12");
        //divWard_Anya.Visible = false;
        if (ddlWard.SelectedValue == "-1")
        {
            // divWard.Attributes.Add("class", "col-md-5");
            // divWard_Anya.Visible = true;
        }
    }
    protected void ddlbhumitype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbhumitype.SelectedIndex == 2)
        {
            divSarkaribhumitype.Visible = true;
            //labSarkaribhumitype.Visible = true;
            ImgSarkaribhumitype.Visible = true;
            divSarkaribhumitype.Visible = true;
            ddlsarkaribhumitype.Enabled = true;
            //ddlsarkaribhumitype.Visible = true;
            ddlsarkaribhumitype.SelectedIndex = 0;
        }
        else
        {
            divSarkaribhumitype.Visible = false;
            //labSarkaribhumitype.Visible = false;
            ImgSarkaribhumitype.Visible = false;
            divSarkaribhumitype.Visible = false;
            ddlsarkaribhumitype.Enabled = false;
            // ddlsarkaribhumitype.Visible = false;
            ddlsarkaribhumitype.SelectedIndex = 0;

        }

        ddlsarkaribhumitype_SelectedIndexChanged(sender, e);
    }
    protected void ddlsarkaribhumitype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsarkaribhumitype.SelectedValue == "6")
        {

            divsarkaribhumitype_Anya.Visible = true;
            divsarkaribhumitype_Anya.Visible = true;
            txtsarkaribhumitype_Anya.Visible = true;
            txtsarkaribhumitype_Anya.Enabled = true;
            txtsarkaribhumitype_Anya.Text = "";
        }
        else
        {

            divsarkaribhumitype_Anya.Visible = false;
            divsarkaribhumitype_Anya.Visible = false;
            txtsarkaribhumitype_Anya.Visible = false;
            txtsarkaribhumitype_Anya.Enabled = false;
            txtsarkaribhumitype_Anya.Text = "";

        }

    }
    protected void ddlbhumivivadtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbhumivivadtype.SelectedValue == "20")
        {
            //  divbhumivivad.Visible = true;
            divBhumivivad_Anya.Visible = true;
            divBhumivivad_Anya.Visible = true;
            //txtbhumivivad_Anya.Visible = true;
            txtbhumivivad_Anya.Enabled = true;
            txtbhumivivad_Anya.Text = "";
            //ddlbhumivivadtype.Focus();
        }
        else
        {
            //divbhumivivad.Visible = false;
            divBhumivivad_Anya.Visible = false;
            divBhumivivad_Anya.Visible = false;
            //txtbhumivivad_Anya.Visible = false;
            txtbhumivivad_Anya.Enabled = false;
            txtbhumivivad_Anya.Text = "";

        }
        //mp1.Show();
    }

    #endregion

    #region
    public bool validationMatterRegistration()
    {
        bool flag = true;

        //if (ddldistrict_old.SelectedIndex  == 0)
        //{
        //    Utility.showMessage(this, "कृपया जिला चुनें...");
        //    flag = false;
        //}
        //if (ddl_anchalold.SelectedIndex == 0)
        //{
        //    Utility.showMessage(this, "कृपया अंचल  चुनें...");
        //    flag = false;
        //}
        //if (ddlhalka_old.SelectedIndex == 0)
        //{
        //    Utility.showMessage(this, "कृपया हल्का चुनें...");
        //    flag = false;
        //}
        //if (ddlmowja_old.SelectedIndex == 0)
        //{
        //    Utility.showMessage(this, "कृपया मौजा चुनें...");
        //    flag = false;
        //}

        if (grd_bhumivivad.Rows.Count == 0)
        {
            Utility.showMessage(this, "कृपया खाता खेसरा का विवरण अंकित करें...");
            flag = false;
        }
        if (ddlDistrict.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया जिला चुनें...!");
            ddlDistrict.Focus();
            flag = false;
        }

        if (ddlSubdivision.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया सब डिवीज़न चुनें...!");
            ddlSubdivision.Focus();
            flag = false;
        }

        if (ddlBlock.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया ब्लाक का चुनाव करें...!");
            ddlBlock.Focus();
            flag = false;
        }

        if (ddlPolice.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया पुलिस स्टेशन का चुनाव करें...!");
            ddlPolice.Focus();
            flag = false;
        }

        if (ddlareatype.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया क्षेत्र का प्रकार का चुनाव करें...!");
            ddlareatype.Focus();
            flag = false;
        }

        if (ddlareatype.SelectedIndex == 1)
        {
            if (ddlPanchyat.SelectedIndex == 0)
            {
                Utility.showMessage(this, "कृपया ग्राम पंचायत करें...!");
                ddlPanchyat.Focus();
                flag = false;
            }
        }

        if (ddlareatype.SelectedIndex == 2)
        {
            if (ddlPanchyat.SelectedIndex == 0)
            {
                Utility.showMessage(this, "कृपया नगर निकाय करें...!");
                ddlPanchyat.Focus();
                flag = false;
            }
        }

        if (ddlVillage.SelectedIndex == 0 && ddlareatype.SelectedValue.ToUpper() == "R")
        {
            Utility.showMessage(this, "कृपया गाँव का चुनाव करें...!");
            ddlVillage.Focus();
            flag = false;
        }

        if (ddlWard.SelectedIndex == 0 && ddlareatype.SelectedValue.ToUpper() == "U")
        {
            Utility.showMessage(this, "कृपया वार्ड का चुनाव करें...!");
            ddlWard.Focus();
            flag = false;
        }

        if (ddl_vivad_adyatan_sthiti.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया विवाद का अद्यतन कारक का चुनाव करें...!");
            ddl_vivad_adyatan_sthiti.Focus();
            flag = false;
        }



        if (ddlbhumitype.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया भूमि का प्रकार चुनें...!");
            ddlbhumitype.Focus();
            flag = false;
        }

        if (ddlbhumitype.SelectedIndex == 2)
        {
            if (ddlsarkaribhumitype.SelectedIndex == 0)
            {
                Utility.showMessage(this, "कृपया सरकारी भूमि का प्रकार चुनें...!");
                ddlbhumitype.Focus();
                flag = false;
            }
        }

        if (txtsarkaribhumitype_Anya.Visible == true && txtsarkaribhumitype_Anya.Text.Trim() == "")
        {
            Utility.showMessage(this, "कृपया सरकारी भूमि का प्रकार (अगर अन्य है) अंकित करें...!");
            ddlbhumitype.Focus();
            flag = false;
        }

        if (ddlbhumivivadtype.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया भूमि के विवाद का प्रकार चुनें...!");
            ddlbhumivivadtype.Focus();
            flag = false;
        }

        if (txtbhumivivad_Anya.Visible == true && txtbhumivivad_Anya.Text.Trim() == "")
        {
            Utility.showMessage(this, "कृपया भूमि विवाद का प्रकार (अगर अन्य है) अंकित करें..");
            txtbhumivivad_Anya.Focus();
            flag = false;
        }
        if (txtAwadenKiTithi.Text == "")
        {
            Utility.showMessage(this, "कृपया आवेदन की तिथि अंकित करें..");
            txtAwadenKiTithi.Focus();
            flag = false;
        }
        if (txtVadiVivarani.Text == "")
        {
            Utility.showMessage(this, "कृपया वादी द्वारा संक्षिप्त विवरणी अंकित करें..");
            txtVadiVivarani.Focus();
            flag = false;
        }
        if (AppDoc.HasFile)
        {
            if (!validateFile(AppDoc, "doc"))
            {
                Utility.showMessage(this, "वादी द्वारा प्रस्तुत आवेदन पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे..");
                lblMsg.Text = "(वादी द्वारा प्रस्तुत आवेदन पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                flag = false;
            }
            else
            {
                string m = FileUploadValidator.IsPdf(AppDoc.PostedFile, 1024, 1024);
                if (m == "OK")
                {


                }
                else
                {
                    Utility.showMessage(this, "वादी द्वारा प्रस्तुत आवेदन पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे..");
                    lblMsg.Text = "(वादी द्वारा प्रस्तुत आवेदन पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    flag = false;
                }
            }

        }


        return flag;
    }
    //public void MatterRegistration()
    //{
    //    if (validationMatterRegistration() == true)
    //    {
    //        string Vadi_sakshya_FilePath = "";
    //        string Prativadi_sakshya_FilePath = "";
    //        Guid obj = Guid.NewGuid();
    //        string id = obj.ToString();
    //        if (AppDoc.HasFile)
    //        {

    //            string m = FileUploadValidator.IsPdf(AppDoc.PostedFile, 1024, 1024);
    //            if (m == "OK")
    //            {
    //                Vadi_sakshya_FilePath = "~/LandDoc/Upload/ID" + id + "/Vadi_sakshya_File.pdf";

    //            }
    //            else
    //            {
    //                lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
    //                return;
    //            }
    //        }



    //        string message = "";
    //        if (AppDoc.HasFile)
    //        {
    //            string v = InsSaveFile("Vadi_sakshya_File", AppDoc, id);
    //            string pa = "~/LandDoc/Upload/ID" + id + "/Vadi_sakshya_File.pdf";
    //            pathfile.Value = v;
    //            if (v == "0" || v != pa)
    //            {
    //                lblMsg.Text = "Technical Error";
    //                return;
    //            }
    //        }




    //        SqlParameter Range_Code = new SqlParameter("@Range_Code", "");
    //        SqlParameter Comm_Code = new SqlParameter("@Comm_Code", Session["Commsionary_Code"].ToString());
    //        SqlParameter rajasv_thaana_sankhya = new SqlParameter("@rajasv_thaana_sankhya", txtrajaswa_sankhya.Text.Trim());
    //        SqlParameter Bhumitype = new SqlParameter("@Bhumitype", ddlbhumitype.SelectedValue.Trim());
    //        SqlParameter SarkariBhumiType = new SqlParameter("@SarkariBhumiType", ddlsarkaribhumitype.SelectedValue.Trim());
    //        SqlParameter SarkariBhumiType_Anya = new SqlParameter("@SarkariBhumiType_Anya", txtsarkaribhumitype_Anya.Text.Trim());
    //        SqlParameter BhumiVivadType = new SqlParameter("@BhumiVivadType", ddlbhumivivadtype.SelectedValue.Trim());
    //        SqlParameter BhumiVivadType_Anya = new SqlParameter("@BhumiVivadType_Anya", txtbhumivivad_Anya.Text.Trim());
    //        SqlParameter bhumi_vivad_ka_adyatan_sthiti = new SqlParameter("@bhumi_vivad_ka_adyatan_sthiti", ddl_vivad_adyatan_sthiti.SelectedValue.Trim());
    //        //Save in ActionDetailsEntry
    //        // Not Save SqlParameter Bhumi_savedansheelta = new SqlParameter("@Bhumi_savedansheelta", ddlbhumivivadki_sanvedanshilta.SelectedValue.Trim());
    //        SqlParameter District_Code = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.Trim());
    //        SqlParameter Sub_DivCode = new SqlParameter("@Sub_DivCode", ddlSubdivision.SelectedValue.Trim());
    //        SqlParameter Block_Code = new SqlParameter("@Block_Code", ddlBlock.SelectedValue.Trim());
    //        SqlParameter Thana_code = new SqlParameter("@Thana_code", ddlPolice.SelectedValue.Trim());
    //        SqlParameter Panchayat_Code = new SqlParameter("@Panchayat_Code", ddlPanchyat.SelectedValue.Trim());
    //        SqlParameter Panchayat_Anya = new SqlParameter("@Panchayat_Anya", txtPanchyat_Anya.Text.Trim());
    //        SqlParameter AreaType = new SqlParameter("@AreaType", ddlareatype.SelectedValue.Trim());
    //        SqlParameter Village = new SqlParameter("@Village", ddlVillage.SelectedValue.Trim());
    //        SqlParameter Village_Anya = new SqlParameter("@Village_Anya", txtVillage_Anya.Text.Trim());
    //        SqlParameter WardNo = new SqlParameter("@WardNo", ddlWard.SelectedValue.Trim());
    //        SqlParameter WardNo_Anya = new SqlParameter("@WardNo_Anya", txtWard_Anya.Text.Trim());

    //        SqlParameter Vadi_sakshya_File = new SqlParameter("@Vadi_sakshya_File", Vadi_sakshya_FilePath);
    //        SqlParameter Prativadi_sakshya_File = new SqlParameter("@Prativadi_sakshya_File", Prativadi_sakshya_FilePath);

    //        SqlParameter CUUser = new SqlParameter("@CUUser", Session["UserId"].ToString());
    //        SqlParameter CUIPAddress = new SqlParameter("@CUIPAddress", GetUserIP().ToString());
    //        SqlParameter AavedanKiTithi = new SqlParameter("@AavedanKiTithi", Convert.ToDateTime(txtAwadenKiTithi.Text.ToString()));

    //        SqlParameter VadiVivarani = new SqlParameter("@VadiVivarani", txtVadiVivarani.Text.ToString());
    //        //  SqlParameter PrativadiVivarani = new SqlParameter("@PrativadiVivarani", txtPrativadiVivarani.Text.ToString());

    //        SqlParameter a_id = new SqlParameter("@a_id", ViewState["a_id"]);
    //        SqlParameter Guid_id = new SqlParameter("@Guid", id);
    //        DataTable vadiDetails1 = (DataTable)ViewState["vadiDetails"];
    //        DataTable vadiDetails = vadiDetails1.Copy();
    //        try
    //        {
    //            if (vadiDetails.Rows.Count == 0)
    //            {
    //                Utility.showMessage(this, "कृपया वादी का विवरण अंकित करें...!");
    //                return;
    //            }

    //            vadiDetails.Columns.Remove("org_type");
    //            vadiDetails.Columns.Remove("dist");
    //            vadiDetails.Columns.Remove("sub_division");
    //            vadiDetails.Columns.Remove("block");
    //            vadiDetails.Columns.Remove("thana");
    //            vadiDetails.Columns.Remove("area_type");
    //            vadiDetails.Columns.Remove("panchayt");
    //            vadiDetails.Columns.Remove("village");
    //            vadiDetails.Columns.Remove("WardNo");
    //            vadiDetails.Columns.Remove("associationName");
    //        }
    //        catch { }
    //        SqlParameter VadiDetailEntryTable = new SqlParameter("@VadiDetailEntryTable", vadiDetails);
    //        DataTable dt = clsData.GetDataTableWithProc("usp_SaveMatterRegisterationPublic", new SqlParameter[]
    //        {
    //            Range_Code
    //            ,Comm_Code
    //            ,rajasv_thaana_sankhya
    //            ,Bhumitype
    //            ,SarkariBhumiType
    //            ,SarkariBhumiType_Anya
    //            ,BhumiVivadType
    //            ,BhumiVivadType_Anya
    //            ,bhumi_vivad_ka_adyatan_sthiti
    //            ,District_Code
    //            ,Sub_DivCode
    //            ,Block_Code
    //            ,Thana_code
    //            ,Panchayat_Code
    //            ,Panchayat_Anya
    //            ,AreaType
    //            ,Village
    //            ,Village_Anya
    //            ,WardNo
    //            ,WardNo_Anya
    //            ,Vadi_sakshya_File
    //            ,Prativadi_sakshya_File
    //            ,CUUser
    //            ,CUIPAddress
    //            ,AavedanKiTithi
    //            ,VadiVivarani

    //            ,VadiDetailEntryTable
    //            ,a_id
    //            ,Guid_id
    //        });

    //        if (dt.Rows.Count > 0)
    //        {
    //            if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
    //            {
    //                Utility.showMessage(Page, "वादी और भूमि विवाद का विवरण सफलतापूर्वक सहेजा गया" + message);
    //                btnBack.Visible = true;
    //                //ViewState["hfGetdateAvadanvalue"] = txtAwadenKiTithi.Text;
    //                hfGetdateAvadan.Value = txtAwadenKiTithi.Text;
    //                //ViewState["hfGetApplicationID"] = dt.Rows[0]["registration"].ToString();
    //                hfGetApplicationno.Value = dt.Rows[0]["registration"].ToString();
    //                //ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
    //                clearFirstStep();

    //                ViewState["a_id"] = dt.Rows[0]["id"].ToString();
    //                ViewState["Step"] = 2;
    //                step1.Visible = false;
    //                step2.Visible = true;
    //                PrativadiAnyVivranGet();
    //                return;
    //            }
    //            else
    //            {

    //                lblMsg.Text = dt.Rows[0]["Message"].ToString();
    //                Utility.showMessage(Page, dt.Rows[0]["Message"].ToString());
    //                return;
    //            }
    //        }
    //    }
    //}
    public bool validationMatterRegistrationVadi()
    {
        bool flag = true;
        if (wadi_grid.Rows.Count == 0)
        {
            Utility.showMessage(this, "कृपया वादी जोड़ें..!");
            flag = false;
        }

       

        return flag;
    }
    public void MatterRegistrationVadi()
    {
        if (validationMatterRegistrationVadi() == true)
        {         
            Guid obj = Guid.NewGuid();
            string id = obj.ToString();
       
            string message = "";
            SqlParameter CUUser = new SqlParameter("@CUUser", Session["UserId"].ToString());
            SqlParameter CUIPAddress = new SqlParameter("@CUIPAddress", GetUserIP().ToString());           
            SqlParameter a_id = new SqlParameter("@a_id", ViewState["a_id"]);
            SqlParameter Guid_id = new SqlParameter("@Guid", id);
            DataTable vadiDetails1 = (DataTable)ViewState["vadiDetails"];
            DataTable vadiDetails = vadiDetails1.Copy();
            try
            {
                if (vadiDetails.Rows.Count == 0)
                {
                    Utility.showMessage(this, "कृपया वादी का विवरण अंकित करें...!");
                    return;
                }

                vadiDetails.Columns.Remove("org_type");
                vadiDetails.Columns.Remove("dist");
                vadiDetails.Columns.Remove("sub_division");
                vadiDetails.Columns.Remove("block");
                vadiDetails.Columns.Remove("thana");
                vadiDetails.Columns.Remove("area_type");
                vadiDetails.Columns.Remove("panchayt");
                vadiDetails.Columns.Remove("village");
                vadiDetails.Columns.Remove("WardNo");
                vadiDetails.Columns.Remove("associationName");
            }
            catch { }
            SqlParameter VadiDetailEntryTable = new SqlParameter("@VadiDetailEntryTable", vadiDetails);
            DataTable dt = clsData.GetDataTableWithProc("usp_SaveMatterRegisterationPublic", new SqlParameter[]
            {    CUUser,CUIPAddress,
                VadiDetailEntryTable
                ,a_id
                ,Guid_id
            });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {
                    Utility.showMessage(Page, "वादी विवरण सफलतापूर्वक सहेजा गया"+ message);
                    btnBack.Visible = true;                   
                    hfGetdateAvadan.Value = txtAwadenKiTithi.Text;
                    clearFirstStepVadi();
                    ViewState["a_id"] = dt.Rows[0]["id"].ToString();
                    hfGetApplicationno.Value = dt.Rows[0]["registration"].ToString();
                    ViewState["Step"] = 2;
                    step1.Visible = false;
                    step2.Visible = true;
                    PrativadiAnyVivranGet();
                    return;
                }
                else
                {

                    lblMsg.Text = dt.Rows[0]["Message"].ToString();
                    Utility.showMessage(Page, dt.Rows[0]["Message"].ToString());
                    return;
                }
            }
        }
    }
    protected void bindbumitype()//   भूमि का प्रकार
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_BindBhumitype", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlbhumitype.DataSource = dt;
                    ddlbhumitype.DataTextField = "bhumitype";
                    ddlbhumitype.DataValueField = "id";
                    ddlbhumitype.DataBind();
                    ddlbhumitype.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
        }
        catch (Exception ee)
        {


        }
    }
    protected void bindSarkariBumitype()//  सरकारी भूमि का प्रकार
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_GetSarkariBhumi_type", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {

                ddlsarkaribhumitype.DataSource = dt;
                ddlsarkaribhumitype.DataTextField = "bhumitype";
                ddlsarkaribhumitype.DataValueField = "id";
                ddlsarkaribhumitype.DataBind();
                ddlsarkaribhumitype.Items.Insert(0, new ListItem("--Select--", "0"));


            }
        }
        catch (Exception ee)
        {


        }
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
    protected void clearFirstStep()
    {
        txtrajaswa_sankhya.Text = "";
        ddlbhumitype.SelectedIndex = 0;
        ddlsarkaribhumitype.SelectedIndex = 0;
        txtsarkaribhumitype_Anya.Text = "";
        ddlbhumivivadtype.SelectedIndex = 0;
        txtbhumivivad_Anya.Text = "";
        //ddlDistrict.SelectedIndex = 0;
        //ddlSubdivision.SelectedIndex = 0;
        //ddlBlock.SelectedIndex = 0;
        // ddlPolice.SelectedIndex = 0;
        ddlPanchyat.SelectedIndex = 0;
        txtPanchyat_Anya.Text = "";
        ddlareatype.SelectedIndex = 0;
        ddlVillage.SelectedIndex = 0;
        txtVillage_Anya.Text = "";
        ddlWard.SelectedIndex = 0;
        txtWard_Anya.Text = "";
        DataTable vadiDetails = (DataTable)ViewState["vadiDetails"];
        vadiDetails.Clear();
        txtVadiVivarani.Text = "";
        //txtPrativadiVivarani.Text = "";
        wadi_grid.DataSource = vadiDetails;
        wadi_grid.DataBind();
        //lblMsg.Text = "";     
    }
    protected void clearFirstStepVadi()
    {
       
        DataTable vadiDetails = (DataTable)ViewState["vadiDetails"];
       
        wadi_grid.DataSource = vadiDetails;
        wadi_grid.DataBind();
        //lblMsg.Text = "";     
    }
    #endregion


    #region Prativadi ka vivara

    void BindSubDivision_Pratiwadi()
    {
        try
        {


            string sql = @" select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En";

            SqlParameter _DCode = new SqlParameter("@District_Code", ddlPDistrict.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode });


            ddlPSubdivision.DataSource = dt;
            ddlPSubdivision.DataTextField = "SubDivisionName";
            ddlPSubdivision.DataValueField = "SubDivisionCode";
            ddlPSubdivision.DataBind();
            ddlPSubdivision.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }

    }
    void BindBlock_Pratiwadi()
    {
        try
        {


            string sql = @" select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName";

            SqlParameter _DCode = new SqlParameter("@District_Code", ddlPDistrict.SelectedValue.Trim());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlPSubdivision.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode });
            ddlPBlock.DataSource = dt;
            ddlPBlock.DataTextField = "BlockName";
            ddlPBlock.DataValueField = "BlockCode";
            ddlPBlock.DataBind();
            ddlPBlock.Items.Insert(0, new ListItem("--Select--", "0"));

            dt.Dispose();


        }
        catch { }

        //BindVillage_Pratiwadi();
        //BindPanchyat_Prtiwadi();


    }
    void BindPolice_Prtiwadi()
    {
        try
        {
            //string sql = @" select DISTINCT t.Police_Station,t.PS_Code from mst_Thana t inner join SubDivisions p on t.Subdivision_Code =p.Sd_Code2 where p.Sd_Code2=@Subdivision_Code and p.DistCode=@District_Code and t.Circle_Code=@Circle_Code order by Police_Station";
            string sql = @"select DISTINCT  t.Police_Station,t.PS_Code from mst_thana t

                            left join MstThanaMapping m on m.Thana_Code = t.PS_Code

                            left join Blocks b on b.BlockCode = m.Code and m.Type = 'Block'

                            where District_code = @District_Code and b.SubDivCode is not null and m.code = @Circle_Code and b.SubDivCode = @Subdivision_Code
                            ORDER BY Police_Station";


            SqlParameter _DCode = new SqlParameter("@District_Code", ddlPDistrict.SelectedValue.ToString());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlPSubdivision.SelectedValue.ToString());
            SqlParameter _Circle_Code = new SqlParameter("@Circle_Code", ddlPBlock.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode, _Circle_Code });
            ddlPThana.DataSource = dt;
            ddlPThana.DataTextField = "Police_Station";
            ddlPThana.DataValueField = "PS_Code";
            ddlPThana.DataBind();
            ddlPThana.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }
    }
    void BindVillage_Pratiwadi()
    {
        try
        {
            string sql = @"select DISTINCT v.VILLCODE, v.VILLNAME  from mst_Panchayats p 
                    inner join PanchayatVillage pv on p.PanchayatCode=pv.PanchayatCode
                    inner join mst_VillageMaster v on v.VILLCODE=pv.VillageCode
                    where p.PanchayatCode=@PanchayatCode
                    order by 	 v.VILLNAME";
            SqlParameter _PanchayatCode = new SqlParameter("@PanchayatCode", ddlPPanchyat.SelectedValue.Trim());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _PanchayatCode });
            ddlPVillage.DataSource = dt;
            ddlPVillage.DataTextField = "VILLNAME";
            ddlPVillage.DataValueField = "VILLCODE";
            ddlPVillage.DataBind();
            ddlPVillage.Items.Insert(0, new ListItem("--Select--", "0"));
            // ddlPVillage.Items.Add(new ListItem("अन्य", "-1"));
            dt.Dispose();


        }
        catch { }
    }
    void BindPanchyat_Prtiwadi()
    {
        try
        {
            string sql = @" select DISTINCT PanchayatCode,PanchayatNameHnd,PanchayatName from mst_Panchayats t inner join Blocks p on t.BlockCode = p.BlockCode where p.BlockCode=@BlockCode and (@AreaType='' or t.AreaType=@AreaType) order by PanchayatNameHnd";
            SqlParameter _BlockCode = new SqlParameter("@BlockCode", ddlPBlock.SelectedValue.Trim());
            SqlParameter _AreaType = new SqlParameter("@AreaType", ddlPAreatype.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _BlockCode, _AreaType });

            ddlPPanchyat.DataSource = dt;
            ddlPPanchyat.DataTextField = "PanchayatName";
            ddlPPanchyat.DataValueField = "PanchayatCode";
            ddlPPanchyat.DataBind();
            ddlPPanchyat.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlPPanchyat.Items.Add(new ListItem("अन्य", "-1"));

            dt.Dispose();


        }
        catch { }
    }
    void bindward_Pratiwadi()
    {
        try
        {

            SqlParameter Pan = new SqlParameter("@Panchayat", ddlPPanchyat.SelectedValue);
            SqlParameter AreaType = new SqlParameter("@AreaType", ddlPAreatype.SelectedValue);
            string sql = @"  select DISTINCT t.WARDNAME,WARDCODE,t.AreaType from mst_Wards t left join mst_Panchayats p on t.PANCHAYATCODE = p.PanchayatCode where p.PANCHAYATCODE=@Panchayat and p.AreaType=@AreaType order by WARDNAME";
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { Pan, AreaType });

            ddlPWard.DataSource = dt;
            ddlPWard.DataTextField = "WARDNAME";
            ddlPWard.DataValueField = "WARDCODE";
            ddlPWard.DataBind();
            ddlPWard.Items.Insert(0, new ListItem("--चुनें--", "0"));
            //ddlPWard.Items.Add(new ListItem("अन्य", "-1"));

        }
        catch (Exception)
        {

        }
    }
    protected void ddlPDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubDivision_Pratiwadi();
        BindBlock_Pratiwadi();
        BindPolice_Prtiwadi();
        BindVillage_Pratiwadi();
        BindPanchyat_Prtiwadi();
        bindward_Pratiwadi();
        ddlPAreatype.SelectedIndex = 0;
    }
    protected void ddlPSubdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock_Pratiwadi();
        BindPolice_Prtiwadi();
        BindVillage_Pratiwadi();
        BindPanchyat_Prtiwadi();
        bindward_Pratiwadi();
        ddlPAreatype.SelectedIndex = 0;
    }
    protected void ddlPBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPolice_Prtiwadi();
        BindVillage_Pratiwadi();
        BindPanchyat_Prtiwadi();
        bindward_Pratiwadi();
        ddlPAreatype_SelectedIndexChanged(sender, e);
        ddlPAreatype.SelectedIndex = 0;
    }
    protected void ddlPAreatype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPAreatype.SelectedIndex == 2)
        {
            labPVillage.Text = "नगर निकाय";
            divPMohalla.Visible = true;
            divPVillageCol.Visible = false;
        }
        else
        {
            labPVillage.Text = "ग्राम पंचायत";
            divPMohalla.Visible = false;
            divPVillageCol.Visible = true;
        }
        BindVillage_Pratiwadi();
        BindPanchyat_Prtiwadi();
        bindward_Pratiwadi();
        ddlPVillage_SelectedIndexChanged(sender, e);
        ddlPWard_SelectedIndexChanged(sender, e);
        ddlPPanchyat_SelectedIndexChanged(sender, e);
    }
    protected void ddlPPanchyat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVillage_Pratiwadi();
        bindward_Pratiwadi();
        //divPPanchyat.Attributes.Add("class", "col-md-12");
        divPPanchyat_Anya.Visible = false;
        if (ddlPPanchyat.SelectedValue == "-1")
        {
            //divPPanchyat.Attributes.Add("class", "col-md-5");
            divPPanchyat_Anya.Visible = true;
        }
        ddlPVillage_SelectedIndexChanged(sender, e);
        ddlPWard_SelectedIndexChanged(sender, e);
    }
    protected void ddlPVillage_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divPVillage.Attributes.Add("class", "col-md-12");
        divPVillage_Anya.Visible = false;
        if (ddlPVillage.SelectedValue == "-1")
        {
            //divPVillage.Attributes.Add("class", "col-md-5");
            divPVillage_Anya.Visible = true;
        }

        ddlPWard_SelectedIndexChanged(sender, e);

    }
    protected void ddlPWard_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divPWard.Attributes.Add("class", "col-md-12");
        divPWard_Anya.Visible = false;
        if (ddlPWard.SelectedValue == "-1")
        {
            // divPWard.Attributes.Add("class", "col-md-5");
            divPWard_Anya.Visible = true;
        }
    }
    protected void ddl_is_pratiVadi_from_an_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_is_pratiVadi_from_an_dept.SelectedValue == "Y")
        {
            divPVibhag_details.Visible = true;
            divPVibhag_details2.Visible = true;
            ddlPvibhaag_naam.SelectedValue = "0";
            txtPvibhaag_padanaam.Text = "";
            ddl_is_pratiVadi_from_an_org.SelectedValue = "N";
            ddl_is_pratiVadi_from_an_org.Enabled = false;
            divPSanstha_details.Visible = false;
        }
        else if (ddl_is_pratiVadi_from_an_dept.SelectedValue == "N")
        {
            divPVibhag_details.Visible = false;
            divPVibhag_details2.Visible = false;
            ddl_is_pratiVadi_from_an_org.SelectedValue = "0";
            ddl_is_pratiVadi_from_an_org.Enabled = true;
        }
        else if (ddl_is_pratiVadi_from_an_dept.SelectedValue == "0")
        {
            divPVibhag_details.Visible = false;
            divPVibhag_details2.Visible = false;
            ddl_is_pratiVadi_from_an_org.SelectedValue = "0";
            ddl_is_pratiVadi_from_an_org.Enabled = true;
            divPSanstha_details.Visible = false;
        }
        //if (ddl_is_pratiVadi_from_an_dept.SelectedValue == "Y")
        //{
        //    ddlPvibhaag_naam.SelectedIndex = 0;
        //    txtPvibhaag_padanaam.Visible = true;
        //    txtPvibhaag_padanaam.Text = "";
        //    ddlPvibhaag_naam.SelectedIndex = 0;
        //    divPVibhag_details.Visible = false;
        //    ddl_is_pratiVadi_from_an_org.SelectedValue = "N";
        //    ddl_is_pratiVadi_from_an_org.Enabled = false;
        //    divPSanstha_details.Visible = false;

        //}
        //else if (ddl_is_pratiVadi_from_an_dept.SelectedValue == "N")
        //{
        //    divPVibhag_details.Visible = false;
        //    txtPvibhaag_padanaam.Visible = false;
        //    ddl_is_pratiVadi_from_an_org.SelectedIndex = 0;
        //    ddl_is_pratiVadi_from_an_org.Enabled = true;
        //}



        //if (ddl_is_pratiVadi_from_an_dept.SelectedIndex == 1)
        //{
        //    divPVibhag_details.Visible = true;

        //}



    }
    protected void ddl_is_pratiVadi_from_an_org_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPsanstha_naam.SelectedIndex = 0;
        txtPsanstha_padanaam.Text = "";
        divPSanstha_details.Visible = false;
        if (ddl_is_pratiVadi_from_an_org.SelectedIndex == 1)
        {
            divPSanstha_details.Visible = true;

        }
    }
    protected void ddlPsanshaanya_naam_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BindPratiwadiGrid()
    {
        try
        {
            pratiWadi_grid.DataSource = (DataTable)ViewState["pratiVadiDetails"];
            pratiWadi_grid.DataBind();
        }
        catch { }
    }
    private DataTable pratiVadiDetails()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("pratiVadi_Name", typeof(string));
        dt.Columns.Add("is_pratiVadi_from_an_dept", typeof(string));
        dt.Columns.Add("pratiVadi_dept_name", typeof(string));
        dt.Columns.Add("pratiVadi_dept_pad_name", typeof(string));
        dt.Columns.Add("is_pratiVadi_from_an_org", typeof(string));
        dt.Columns.Add("pratiVadi_org_type", typeof(int));
        dt.Columns.Add("pratiVadi_org_name", typeof(string));
        dt.Columns.Add("pratiVadi_org_pad_name", typeof(string));
        dt.Columns.Add("pratiVadi_Father_Husband_Name", typeof(string));
        dt.Columns.Add("pratiVadi_District_Code", typeof(string));
        dt.Columns.Add("pratiVadi_Sub_DivCode", typeof(string));
        dt.Columns.Add("pratiVadi_Block_Code", typeof(string));
        dt.Columns.Add("pratiVadi_Thana_code", typeof(string));
        dt.Columns.Add("pratiVadi_AreaType", typeof(string));
        dt.Columns.Add("pratiVadi_Panchayat_Code", typeof(string));
        dt.Columns.Add("pratiVadi_Village_Code", typeof(string));
        dt.Columns.Add("pratiVadi_WardNo", typeof(string));
        dt.Columns.Add("pratiVadi_MobileNo", typeof(string));
        dt.Columns.Add("pratiVadi_Panchayat_Anya", typeof(string));
        dt.Columns.Add("pratiVadi_Village_Anya", typeof(string));
        dt.Columns.Add("pratiVadi_WardNo_Anya", typeof(string));
        dt.Columns.Add("mohalla", typeof(string));
        dt.Columns.Add("sanstha_sambandh_type", typeof(int));

        dt.Columns.Add("org_type", typeof(string));
        dt.Columns.Add("dist", typeof(string));
        dt.Columns.Add("sub_division", typeof(string));
        dt.Columns.Add("block", typeof(string));
        dt.Columns.Add("thana", typeof(string));
        dt.Columns.Add("area_type", typeof(string));
        dt.Columns.Add("panchayt", typeof(string));
        dt.Columns.Add("village", typeof(string));
        dt.Columns.Add("WardNo", typeof(string));
        dt.Columns.Add("associationName", typeof(string));
        return dt;
    }
    protected void pratiWadi_grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            string rowIndex = e.CommandArgument.ToString();
            DataTable dt = (DataTable)ViewState["pratiVadiDetails"];
            dt.Rows.RemoveAt(Convert.ToInt32(rowIndex));
            ViewState["pratiVadiDetails"] = dt;
            BindPratiwadiGrid();

        }
    }
    public bool ValidatePratiVadiDetail()
    {

        if (string.IsNullOrWhiteSpace(txtPName.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया प्रतिवादी का नाम अंकित करें...!');", true);
            txtPName.Focus();
            return false;
        }


        var dept = ddl_is_pratiVadi_from_an_dept.SelectedIndex;
        if (dept == 0 || dept == 1)
        {
            if (dept == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('क्या प्रतिवादी किसी विभाग का प्रतिनिधि है कृपया चुनें...!');", true);
                ddl_is_pratiVadi_from_an_dept.Focus();
                return false;
            }

            if (ddlPvibhaag_naam.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया विभाग का नाम चुनें...!');", true);
                ddlPvibhaag_naam.Focus();
                return false;
            }
        }


        var org = ddl_is_pratiVadi_from_an_org.SelectedIndex;
        if (org == 0 || org == 1)
        {
            if (org == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('क्या प्रतिवादी किसी संस्था का प्रतिनिधि है कृपया चुनें...!');", true);
                ddl_is_pratiVadi_from_an_org.Focus();
                return false;
            }


            if (ddlPsanstha_naam.SelectedIndex == 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया संस्था का प्रकार चुनें...!');", true);
                ddlPsanstha_naam.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(txtPsanstha_naam.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया संस्था का नाम अंकित करें...!');", true);
                txtPsanstha_naam.Focus();
                return false;
            }

        }


        //if (txtPPanchyat_Anya != null && string.IsNullOrWhiteSpace(txtPPanchyat_Anya.Text))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अन्य पंचायत अंकित करें...!');", true);
        //    txtPPanchyat_Anya.Focus();
        //    return false;
        //}


        //if (txtPVillage_Anya != null && string.IsNullOrWhiteSpace(txtPVillage_Anya.Text))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अन्य ग्राम अंकित करें...!');", true);
        //    txtPVillage_Anya.Focus();
        //    return false;
        //}

        //if (txtPWard_Anya != null && string.IsNullOrWhiteSpace(txtPWard_Anya.Text))
        //{
        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अन्य वार्ड अंकित करें...!');", true);
        //    txtPWard_Anya.Focus();
        //    return false;
        //}


        return true;
    }
    protected void btnAddPratiVadi_Click(object sender, EventArgs e)
    {
        if (ValidatePratiVadiDetail())
        {
            DataTable dt = (DataTable)ViewState["pratiVadiDetails"];
            dt.Rows.Add(
                txtPName.Text.Trim(),
                ddl_is_pratiVadi_from_an_dept.SelectedValue.Trim(),
                ddlPvibhaag_naam.SelectedValue.Trim() != "0" ? ddlPvibhaag_naam.SelectedValue.Trim() : null,
                txtPvibhaag_padanaam.Text.Trim(),
                ddl_is_pratiVadi_from_an_org.SelectedValue.Trim(),
                ddlPsanstha_naam.SelectedValue.Trim() != "0" ? ddlPsanstha_naam.SelectedValue.Trim() : null,
                txtPsanstha_naam.Text.Trim(),
                txtPsanstha_padanaam.Text.Trim(),
                txtPFName.Text.Trim(),
                ddlPDistrict.SelectedValue.Trim() != "0" ? ddlPDistrict.SelectedValue.Trim() : null,
                !string.IsNullOrWhiteSpace(ddlPSubdivision.SelectedValue.Trim()) && ddlPSubdivision.SelectedValue.Trim() != "0"
                ? (object)ddlPSubdivision.SelectedValue.Trim()
                : (object)DBNull.Value,

                 !string.IsNullOrWhiteSpace(ddlPBlock.SelectedValue.Trim()) && ddlPBlock.SelectedValue.Trim() != "0"
    ? (object)ddlPBlock.SelectedValue.Trim()
    : (object)DBNull.Value,
                          !string.IsNullOrWhiteSpace(ddlPThana.SelectedValue.Trim()) && ddlPThana.SelectedValue.Trim() != "0"
    ? (object)ddlPThana.SelectedValue.Trim()
    : (object)DBNull.Value,


                //ddlPSubdivision.SelectedValue.Trim() != "0" ? ddlPSubdivision.SelectedValue.Trim() : null,
                //ddlPBlock.SelectedValue.Trim() != "0" ? ddlPBlock.SelectedValue.Trim() : null,
                //ddlPThana.SelectedValue.Trim() != "0" ? ddlPThana.SelectedValue.Trim() : null,
                ddlPAreatype.SelectedValue.Trim() != "0" ? ddlPAreatype.SelectedValue.Trim() : null,
                                      !string.IsNullOrWhiteSpace(ddlPPanchyat.SelectedValue.Trim()) && ddlPPanchyat.SelectedValue.Trim() != "0"
    ? (object)ddlPPanchyat.SelectedValue.Trim()
    : (object)DBNull.Value,
                                                !string.IsNullOrWhiteSpace(ddlPVillage.SelectedValue.Trim()) && ddlPVillage.SelectedValue.Trim() != "0"
    ? (object)ddlPVillage.SelectedValue.Trim()
    : (object)DBNull.Value,
                                                            !string.IsNullOrWhiteSpace(ddlPWard.SelectedValue.Trim()) && ddlPWard.SelectedValue.Trim() != "0"
    ? (object)ddlPWard.SelectedValue.Trim()
    : (object)DBNull.Value,
                //ddlPPanchyat.SelectedValue.Trim() != "0" ? ddlPPanchyat.SelectedValue.Trim() : null,
                //ddlPVillage.SelectedValue.Trim() != "0" ? ddlPVillage.SelectedValue.Trim() : null,
                //ddlPWard.SelectedValue.Trim() != "0" ? ddlPWard.SelectedValue.Trim() : null,
                txtprativadi_Mobile.Text.Trim(),
                txtPPanchyat_Anya.Text.Trim(),
                txtPVillage_Anya.Text.Trim(),
                txtPWard_Anya.Text.Trim(),
                txtPMohalla.Text.Trim(),
                ddlPsanshaanya_naam.SelectedValue.Trim() != "0" ? ddlPsanshaanya_naam.SelectedValue.Trim() : null,
                // ddlPvibhaag_naam.SelectedValue.Trim() == "Y" ? ddlPvibhaag_naam.SelectedItem.Text.Trim(): ddl_is_pratiVadi_from_an_org.SelectedValue.Trim() == "Y" ? ddlPsanstha_naam.SelectedItem.Text.Trim() : "",

                ddl_is_pratiVadi_from_an_dept.SelectedValue == "Y" ? ddlPvibhaag_naam.SelectedItem.Text : ddl_is_pratiVadi_from_an_org.SelectedValue == "Y" ? ddlPsanstha_naam.SelectedItem.Text : "",



                ddlPDistrict.SelectedValue.Trim() != "0" ? ddlPDistrict.SelectedItem.Text : "",
                   !string.IsNullOrWhiteSpace(ddlPSubdivision.SelectedValue.Trim()) && ddlPSubdivision.SelectedValue.Trim() != "0"
                ? (object)ddlPSubdivision.SelectedValue.Trim()
                : (object)DBNull.Value,

                 !string.IsNullOrWhiteSpace(ddlPBlock.SelectedValue.Trim()) && ddlPBlock.SelectedValue.Trim() != "0"
    ? (object)ddlPBlock.SelectedValue.Trim()
    : (object)DBNull.Value,
                          !string.IsNullOrWhiteSpace(ddlPThana.SelectedValue.Trim()) && ddlPThana.SelectedValue.Trim() != "0"
    ? (object)ddlPThana.SelectedValue.Trim()
    : (object)DBNull.Value,
                //ddlPSubdivision.SelectedValue.Trim() != "0" ? ddlPSubdivision.SelectedItem.Text : "",
                //ddlPWard.SelectedValue.Trim() != "0" ? ddlPBlock.SelectedItem.Text : "",
                //ddlPThana.SelectedValue.Trim() != "0" ? ddlPThana.SelectedItem.Text : "",
                ddlPAreatype.SelectedValue.Trim() != "0" ? ddlPAreatype.SelectedItem.Text : "",
                                             !string.IsNullOrWhiteSpace(ddlPPanchyat.SelectedValue.Trim()) && ddlPPanchyat.SelectedValue.Trim() != "0"
    ? (object)ddlPPanchyat.SelectedValue.Trim()
    : (object)DBNull.Value,
                                                !string.IsNullOrWhiteSpace(ddlPVillage.SelectedValue.Trim()) && ddlPVillage.SelectedValue.Trim() != "0"
    ? (object)ddlPVillage.SelectedValue.Trim()
    : (object)DBNull.Value,
                                                            !string.IsNullOrWhiteSpace(ddlPWard.SelectedValue.Trim()) && ddlPWard.SelectedValue.Trim() != "0"
    ? (object)ddlPWard.SelectedValue.Trim()
    : (object)DBNull.Value,
                //ddlPPanchyat.SelectedValue.Trim() != "0" ? ddlPPanchyat.SelectedItem.Text : "",
                //ddlPVillage.SelectedValue.Trim() != "0" ? ddlPVillage.SelectedItem.Text : "",
                //ddlPWard.SelectedValue.Trim() != "0" ? ddlPWard.SelectedItem.Text : "",
                ddlPsanshaanya_naam.SelectedValue.Trim() != "0" ? ddlPsanshaanya_naam.SelectedItem.Text : "")  ;
            ViewState["pratiVadiDetails"] = dt;
            BindPratiwadiGrid();
            txtPName.Text = "";

            txtPsanstha_padanaam.Text = "";
            ddl_is_pratiVadi_from_an_dept.SelectedValue = "0";
            divPVibhag_details.Visible = false;
            ddlPvibhaag_naam.SelectedValue = "0";
            divPVibhag_details2.Visible = false;
            txtPvibhaag_padanaam.Text = "";
            ddl_is_pratiVadi_from_an_org.SelectedValue = "0";
            ddl_is_pratiVadi_from_an_org.Enabled = true;
            divPSanstha_details.Visible = false;
            ddlPsanstha_naam.SelectedValue = "0";
            ddlPsanshaanya_naam.SelectedValue = "0";
            txtPsanstha_naam.Text = "";
            txtPsanstha_padanaam.Text = "";

            txtPFName.Text = "";
            txtPPanchyat_Anya.Text = "";
            txtPVillage_Anya.Text = "";
            txtPWard_Anya.Text = "";
            txtPMohalla.Text = "";

            ddlPDistrict.SelectedIndex = 0;
            if (ddlPSubdivision.Items.Count > 0)
            {
                ddlPSubdivision.SelectedIndex = 0;
            }
            if (ddlPBlock.Items.Count > 0)
            {
                ddlPBlock.SelectedIndex = 0;
            }
            if (ddlPThana.Items.Count > 0)
            {
                ddlPThana.SelectedIndex = 0;
            }
            //ddlPSubdivision.SelectedIndex = 0;
            //ddlPBlock.SelectedIndex = 0;
            //ddlPThana.SelectedIndex = 0;
            ddlPAreatype.SelectedIndex = 0;
            if (ddlPPanchyat.Items.Count > 0)
            {
                ddlPPanchyat.SelectedIndex = 0;
            }
            if (ddlPVillage.Items.Count > 0)
            {
                ddlPVillage.SelectedIndex = 0;
            }
            if (ddlPWard.Items.Count > 0)
            {
                ddlPWard.SelectedIndex = 0;
            }
            //ddlPPanchyat.SelectedIndex = 0;
            //ddlPVillage.SelectedIndex = 0;
            //ddlPWard.SelectedIndex = 0;
            txtprativadi_Mobile.Text = "";
        }
    }
    public bool validationPrativadiNVivaran()
    {
        bool flag = true;
        //if (pratiWadi_grid.Rows.Count == 0)
        //{
        //    Utility.showMessage(this, "कृपया प्रतिवादी जोड़ें..!");
        //    flag = false;
        //    btnAddPratiVadiDetail.Focus();
        //}
        //if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 0)
        //{
        //    Utility.showMessage(this, "कृपया प्रतिवादी को सूचित किया गया है या नहीं चुनें...!");
        //    ddlwadi_pratiwadi_sunwai.Focus();
        //    flag = false;
        //}
        //if (ddlKiskeduwara_bhejagaya.Visible == true && ddlKiskeduwara_bhejagaya.SelectedIndex == 0)
        //{
        //    Utility.showMessage(this, "कृपया प्रतिवादी के सुचना का माध्यम चुनें...!");
        //    ddlKiskeduwara_bhejagaya.Focus();
        //    flag = false;
        //}
        //if (txtsunwaiHetuNoticKaKaran.Visible == true && txtsunwaiHetuNoticKaKaran.Text.Trim() == "")
        //{
        //    Utility.showMessage(this, "कृपया कारण स्पष्ट करें...!");
        //    txtsunwaiHetuNoticKaKaran.Focus();
        //    flag = false;
        //}

        //if (ddlSuchana_ka_tamila.Visible == true && ddlSuchana_ka_tamila.SelectedIndex == 0)
        //{
        //    Utility.showMessage(this, "कृपया प्रतिवादी को सूचना का तामिला प्राप्त है या नहीं चुनें...!");
        //    ddlSuchana_ka_tamila.Focus();
        //    flag = false;
        //}
        //if (ddlSuchana_ka_upasthiti.Visible == true && ddlSuchana_ka_upasthiti.SelectedIndex == 0)
        //{
        //    Utility.showMessage(this, "कृपया प्रतिवादी उपस्थित हुआ है या नहीं चुनें...!");
        //    ddlSuchana_ka_upasthiti.Focus();
        //    flag = false;
        //}
        return flag;
    }
    public void ClearPrativadiNAnyaVivaran()
    {
        DataTable pratiVadiDetails = (DataTable)ViewState["pratiVadiDetails"];
        pratiVadiDetails.Clear();
        pratiWadi_grid.DataSource = pratiVadiDetails;
        pratiWadi_grid.DataBind();
        ddlwadi_pratiwadi_sunwai.SelectedIndex = 0;
        ddlKiskeduwara_bhejagaya.SelectedIndex = 0;
        txtsunwaiHetuNoticKaKaran.Text = "";
        ddlSuchana_ka_tamila.SelectedIndex = 0;
        ddlSuchana_ka_tamila.SelectedIndex = 0;
        ddlSuchana_ka_upasthiti.SelectedIndex = 0;
    }
    public void PrativadiAnyVivran()
    {
        if (validationPrativadiNVivaran() == true)
        {
            //अन्य विवरण next Action
            SqlParameter a_id = new SqlParameter("@a_id", Convert.ToInt64(ViewState["a_id"]));
            SqlParameter prativadi_ko_suchit_kiya_gaya_hai = new SqlParameter("@prativadi_ko_suchit_kiya_gaya_hai", ddlwadi_pratiwadi_sunwai.SelectedValue.Trim());
            SqlParameter given_info_type = new SqlParameter("@given_info_type", ddlKiskeduwara_bhejagaya.SelectedValue.Trim());
            SqlParameter given_info_desc = new SqlParameter("@given_info_desc", txtsunwaiHetuNoticKaKaran.Text.Trim());
            SqlParameter prativadi_ko_suchana_ka_taamila_praapt_hai = new SqlParameter("@prativadi_ko_suchana_ka_taamila_praapt_hai", ddlSuchana_ka_tamila.SelectedValue != null ? ddlSuchana_ka_tamila.SelectedValue.Trim() : "");
            SqlParameter prativadi_upasthit_hua_hai = new SqlParameter("@prativadi_upasthit_hua_hai", ddlSuchana_ka_upasthiti.SelectedValue != null ? ddlSuchana_ka_upasthiti.SelectedValue.Trim() : "");

            DataTable pratiVadiDetails1 = (DataTable)ViewState["pratiVadiDetails"];
            DataTable pratiVadiDetails = pratiVadiDetails1.Copy();
            try
            {
                if (pratiVadiDetails.Rows.Count == 0)
                {
                    Utility.showMessage(this, "कृपया प्रतिवादी का विवरण अंकित करें...!");
                    return;
                }

                pratiVadiDetails.Columns.Remove("org_type");
                pratiVadiDetails.Columns.Remove("dist");
                pratiVadiDetails.Columns.Remove("sub_division");
                pratiVadiDetails.Columns.Remove("block");
                pratiVadiDetails.Columns.Remove("thana");
                pratiVadiDetails.Columns.Remove("area_type");
                pratiVadiDetails.Columns.Remove("panchayt");
                pratiVadiDetails.Columns.Remove("village");
                pratiVadiDetails.Columns.Remove("WardNo");
                pratiVadiDetails.Columns.Remove("associationName");
            }

            catch { }
            SqlParameter PrtiVadiDetailEntryTable = new SqlParameter("@PrtiVadiDetailEntryTable", pratiVadiDetails);
            DataTable dt = clsData.GetDataTableWithProc("usp_SavePrativadiNAnyvivran_public   ", new SqlParameter[]
            {
                a_id
                ,prativadi_ko_suchit_kiya_gaya_hai
                ,given_info_type
                ,given_info_desc
                ,prativadi_ko_suchana_ka_taamila_praapt_hai
                ,prativadi_upasthit_hua_hai
                ,PrtiVadiDetailEntryTable
            });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {
                    //lblMsg.Text = "प्रतिवादी और अन्य विवरण सफलतापूर्वक सहेजा गया";
                    Utility.showMessage(Page, "प्रतिवादी विवरण सफलतापूर्वक सहेजा गया");
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                    MatterRegistrationGet();
                    GetKhataKhesrabhumikavivaran();
                    ClearPrativadiNAnyaVivaran();
                    ViewState["a_id"] = dt.Rows[0]["id"].ToString();
                    ViewState["Step"] = 3;
                    step2.Visible = false;
                    step3.Visible = true;
                    return;
                }
                else
                {

                    lblMsg.Text = dt.Rows[0]["Message"].ToString();
                    Utility.showMessage(Page, dt.Rows[0]["Message"].ToString());
                    return;
                }

            }
        }
    }
    #endregion
    public void GetKhataKhesrabhumikavivaran()
    {
        SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);

        DataTable dtbhumivivad = clsData.GetDataTableWithProc("usp_GetBhumiKaKhataKhesraKaVivaranPublic", new SqlParameter[] { _a_id });
        ViewState["BhumiKaViVaranDT"] = dtbhumivivad;
        grd_bhumivivad.DataSource = dtbhumivivad;
        grd_bhumivivad.DataBind();
    }
    bool validateFile(FileUpload fuFile, string FileType)
    {
        if (fuFile.HasFile)
        {
            int contentLength = fuFile.PostedFile.ContentLength;
            string extension = Path.GetExtension(fuFile.PostedFile.FileName);
            long maxFileSize = 5000000;

            string mimeType = fuFile.PostedFile.ContentType;
            string allowedMimeType = "application/pdf";

            if (mimeType == allowedMimeType)
            {
                switch (FileType)
                {
                    case "zip":
                        switch (extension.ToLower())
                        {
                            case ".zip":
                                break;
                            default:
                                lblMsg.Text = "This file type is not allowed.";
                                //ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                return false;
                        }

                        if (contentLength > (1 * 1024 * 1024))
                        {
                            lblMsg.Text = "File size must be less than or equal to 3 MB";
                            return false;
                        }
                        break;
                    case "doc":

                        switch (extension.ToLower())
                        {
                            case ".jMD":
                            case ".jpeg":
                            case ".pdf":

                                break;
                            default:
                                lblMsg.Text = "This file type is not allowed.";
                                //ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                return false;
                        }
                        if (contentLength > (3 * 1024 * 1024))
                        {
                            lblMsg.Text = "File size must be less than or equal to 3 MB";
                            return false;
                        }
                        break;
                    case "Image":

                        switch (extension.ToLower())
                        {

                            case ".png":
                            case ".PNG":
                            case ".jpg":
                            case ".JPG":
                            case ".jpeg":
                            case ".JPEG":


                                break;
                            default:
                                lblMsg.Text = "This file type is not allowed.";
                                //ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
                                return false;
                        }
                        if (contentLength > (0.4 * 1024 * 1024))
                        {
                            lblMsg.Text = "File size must be less than or equal to 400KB";
                            return false;
                        }
                        break;



                    default:
                        lblMsg.Text = "Unknown File Type !!";
                        return false;
                }
            }
            else
            {

                lblMsg.Text = "Invalid file type. Only PDF files are allowed.";
                return false;
            }
        }
        return true;
    }
    bool validateFile1(FileUpload fuFile, string FileType)
    {
        if (fuFile.HasFile)
        {
            int contentLength = fuFile.PostedFile.ContentLength;
            string extension = Path.GetExtension(fuFile.PostedFile.FileName).ToLower();

            string mimeType = fuFile.PostedFile.ContentType;

            // allowed image mime types
            if (mimeType == "image/png" || mimeType == "image/jpeg")
            {
                switch (FileType)
                {
                    case "Image":

                        switch (extension)
                        {
                            case ".png":
                            case ".jpg":
                            case ".jpeg":
                                break;

                            default:
                                lblMsg.Text = "Only PNG, JPG or JPEG image files are allowed.";
                                return false;
                        }

                        if (contentLength > (400 * 1024))
                        {
                            lblMsg.Text = "Image size must be less than or equal to 400 KB";
                            return false;
                        }
                        break;

                    default:
                        lblMsg.Text = "Unknown File Type !!";
                        return false;
                }
            }
            else
            {
                lblMsg.Text = "Invalid image format. Only PNG, JPG or JPEG files are allowed.";
                return false;
            }
        }
        else
        {
            lblMsg.Text = "Please upload an image file.";
            return false;
        }

        return true;
    }

    //bool validateFile1(FileUpload fuFile, string FileType)
    //{
    //    if (fuFile.HasFile)
    //    {
    //        int contentLength = fuFile.PostedFile.ContentLength;
    //        string extension = Path.GetExtension(fuFile.PostedFile.FileName);
    //        long maxFileSize = 5000000;

    //        string mimeType = fuFile.PostedFile.ContentType;
    //        string allowedMimeType = "image/png";

    //        if (mimeType == allowedMimeType)
    //        {
    //            switch (FileType)
    //            {
    //                case "zip":
    //                    switch (extension.ToLower())
    //                    {
    //                        case ".zip":
    //                            break;
    //                        default:
    //                            lblMsg.Text = "This file type is not allowed.";
    //                            // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
    //                            return false;
    //                    }

    //                    if (contentLength > (1 * 1024 * 1024))
    //                    {
    //                        lblMsg.Text = "File size must be less than or equal to 3 MB";
    //                        return false;
    //                    }
    //                    break;
    //                case "doc":

    //                    switch (extension.ToLower())
    //                    {
    //                        //case ".jMD":
    //                        //case ".jpeg":
    //                        case ".pdf":

    //                            break;
    //                        default:
    //                            lblMsg.Text = "This file type is not allowed.";
    //                            // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
    //                            return false;
    //                    }
    //                    if (contentLength > (3 * 1024 * 1024))
    //                    {
    //                        lblMsg.Text = "File size must be less than or equal to 3 MB";
    //                        return false;
    //                    }
    //                    break;
    //                case "Image":

    //                    switch (extension.ToLower())
    //                    {

    //                        case ".png":
    //                        case ".PNG":
    //                        case ".jpg":
    //                        case ".JPG":
    //                        case ".jpeg":
    //                        case ".JPEG":


    //                            break;
    //                        default:
    //                            lblMsg.Text = "This file type is not allowed.";
    //                            // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
    //                            return false;
    //                    }
    //                    if (contentLength > (0.4 * 1024 * 1024))
    //                    {
    //                        lblMsg.Text = "File size must be less than or equal to 400KB";
    //                        return false;
    //                    }
    //                    break;



    //                default:
    //                    lblMsg.Text = "Unknown File Type !!";
    //                    return false;
    //            }
    //        }
    //        else
    //        {

    //            lblMsg.Text = "Invalid file type. Only PDF files are allowed.";
    //            return false;
    //        }
    //    }
    //    return true;
    //}
    public void MatterRegistrationGet() //for step=1
    {
        try
        {

            SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);
            SqlParameter _fn = new SqlParameter("@fn", "GetMatterregistration");
            DataTable dt_wadi = clsData.GetDataTableWithProc("getMatterRegisterationNVadi", new SqlParameter[] { _a_id, _fn });
            if (dt_wadi.Rows.Count > 0)
            {
                BindDist();
                ddlDistrict.SelectedValue = dt_wadi.Rows[0]["District_Code"].ToString();

                BindSubDivision();

                ddlSubdivision.SelectedValue = dt_wadi.Rows[0]["Sub_DivCode"].ToString();
                BindBlock();
                ddlBlock.SelectedValue = dt_wadi.Rows[0]["Block_Code"].ToString();

                BindPolice();
                ddlPolice.SelectedValue = dt_wadi.Rows[0]["Thana_Code"].ToString(); 

                ddlareatype.SelectedValue = dt_wadi.Rows[0]["AreaType"].ToString();

                if (ddlareatype.SelectedIndex == 2)
                {
                    labVillage.Text = "निकाय पंचायत";
                    imgWard_bhumiVivaran.Visible = true;
                }
                else
                {
                    labVillage.Text = "ग्राम पंचायत";
                    imgWard_bhumiVivaran.Visible = false;
                }
                BindVillage();
                BindPanchyat();
                bindward();




                ddlPanchyat.SelectedValue = dt_wadi.Rows[0]["Panchayat_Code"].ToString();


                BindVillage();
                bindward();

                divPanchyat_Anya.Visible = false;
                if (ddlPanchyat.SelectedValue == "-1")
                {

                    divPanchyat_Anya.Visible = true;
                    txtPanchyat_Anya.Text = dt_wadi.Rows[0]["Panchayat_Anya"].ToString();
                }


                ddlVillage.SelectedValue = dt_wadi.Rows[0]["Village"].ToString();
                if (ddlVillage.SelectedValue == "-1")
                {
                    txtVillage_Anya.Text = dt_wadi.Rows[0]["Village_Anya"].ToString();
                }
                ddlVillage_SelectedIndexChanged(this, EventArgs.Empty);
                ddlWard.SelectedValue = dt_wadi.Rows[0]["WardNo"].ToString();
                ddlWard_SelectedIndexChanged(this, EventArgs.Empty);
                if (ddlWard.SelectedValue == "-1")
                {
                    txtWard_Anya.Text = dt_wadi.Rows[0]["WardNo_Anya"].ToString();
                }

                ddl_vivad_adyatan_sthiti.SelectedValue = dt_wadi.Rows[0]["bhumi_vivad_ka_adyatan_sthiti"].ToString();
                txtrajaswa_sankhya.Text = dt_wadi.Rows[0]["rajasv_thaana_sankhya"].ToString();
                ddlbhumitype.SelectedValue = dt_wadi.Rows[0]["Bhumitype"].ToString();


                if (ddlbhumitype.SelectedIndex == 2)
                {
                    divSarkaribhumitype.Visible = true;
                    //labSarkaribhumitype.Visible = true;
                    //ImgSarkaribhumitype.Visible = true;
                    // divSarkaribhumitype.Visible = true;
                    ddlsarkaribhumitype.Enabled = true;
                    //ddlsarkaribhumitype.Visible = true;
                    ddlsarkaribhumitype.SelectedIndex = 0;
                    ddlsarkaribhumitype.SelectedValue = dt_wadi.Rows[0]["SarkariBhumiType"].ToString();
                    ddlsarkaribhumitype_SelectedIndexChanged(this, EventArgs.Empty);
                    if (ddlsarkaribhumitype.SelectedValue == "6")
                    {
                        txtsarkaribhumitype_Anya.Text = dt_wadi.Rows[0]["SarkariBhumiType_Anya"].ToString();
                    }
                }
                else
                {
                    divSarkaribhumitype.Visible = false;
                    //labSarkaribhumitype.Visible = false;
                    //ImgSarkaribhumitype.Visible = false;
                    divSarkaribhumitype.Visible = false;
                    ddlsarkaribhumitype.Enabled = false;
                    // ddlsarkaribhumitype.Visible = false;
                    ddlsarkaribhumitype.SelectedValue = dt_wadi.Rows[0]["SarkariBhumiType"].ToString();

                }

                if (ddlsarkaribhumitype.SelectedValue == "6")
                {

                    divsarkaribhumitype_Anya.Visible = true;
                    divsarkaribhumitype_Anya.Visible = true;
                    txtsarkaribhumitype_Anya.Visible = true;
                    txtsarkaribhumitype_Anya.Enabled = true;
                    txtsarkaribhumitype_Anya.Text = "";
                }
                else
                {
                    divsarkaribhumitype_Anya.Visible = false;
                    divsarkaribhumitype_Anya.Visible = false;
                    txtsarkaribhumitype_Anya.Visible = false;
                    txtsarkaribhumitype_Anya.Enabled = false;
                    txtsarkaribhumitype_Anya.Text = "";

                }


                ddlbhumivivadtype.SelectedValue = dt_wadi.Rows[0]["BhumiVivadType"].ToString();

                if (ddlbhumivivadtype.SelectedValue == "20")
                {

                    divBhumivivad_Anya.Visible = true;
                    divBhumivivad_Anya.Visible = true;
                    // txtbhumivivad_Anya.Visible = true;
                    txtbhumivivad_Anya.Enabled = true;
                    txtbhumivivad_Anya.Text = "";
                    txtbhumivivad_Anya.Text = dt_wadi.Rows[0]["BhumiVivadType_Anya"].ToString();

                }
                else
                {
                    //divbhumivivad.Visible = false;
                    divBhumivivad_Anya.Visible = false;
                    divBhumivivad_Anya.Visible = false;
                    //txtbhumivivad_Anya.Visible = false;
                    txtbhumivivad_Anya.Enabled = false;
                    txtbhumivivad_Anya.Text = "";

                }



                txtAwadenKiTithi.Text = dt_wadi.Rows[0]["AavedanKiTithi"].ToString();

                txtVadiVivarani.Text = dt_wadi.Rows[0]["VadiVivarani"].ToString();
                //txtPrativadiVivarani.Text = dt_wadi.Rows[0]["PrativadiVivarani"].ToString();

                if (dt_wadi.Rows[0]["Vadi_sakshya_File"].ToString() != "")
                {
                    lnkAppDoc.Visible = true;
                    lnkAppDoc.Attributes.Add("path", dt_wadi.Rows[0]["Vadi_sakshya_File"].ToString());
                }
                else
                {
                    lnkAppDoc.Visible = false;
                }
                
               


              
            }
        }
        catch (Exception ex)
        { }
    }
    public void MatterRegistrationGetVadi() //for step=1
    {
        try
        {

            SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);
            SqlParameter _fn = new SqlParameter("@fn", "GetMatterregistration");
            DataTable dt_wadi = clsData.GetDataTableWithProc("getMatterRegisterationNVadiPublic", new SqlParameter[] { _a_id, _fn });
            if (dt_wadi.Rows.Count > 0)
            {
                SqlParameter _a_id1 = new SqlParameter("@a_id", ViewState["a_id"]);
                SqlParameter _fn1 = new SqlParameter("@fn", "GetVadi");
                DataTable dtwadi = clsData.GetDataTableWithProc("getMatterRegisterationNVadiPublic", new SqlParameter[] { _a_id1, _fn1 });
                ViewState["vadiDetails"] = dtwadi;
                wadi_grid.DataSource = dtwadi;
                wadi_grid.DataBind();
                hfwadiprint.Value = "Printstep1";  
            }
        }
        catch (Exception ex)
        { }
    }
    public void PrativadiAnyVivranGet()
    {
        SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);
        SqlParameter _fn = new SqlParameter("@fn", "AnyVivaran");
        DataTable dtAnyaVivaran = clsData.GetDataTableWithProc("usp_PrativadiAnyVivran", new SqlParameter[] { _a_id, _fn });
        if (dtAnyaVivaran.Rows.Count > 0)
        {
            ddlwadi_pratiwadi_sunwai.SelectedValue = dtAnyaVivaran.Rows[0]["prativadi_ko_suchit_kiya_gaya_hai"].ToString();
            ddlKiskeduwara_bhejagaya.SelectedIndex = 0;
            txtsunwaiHetuNoticKaKaran.Text = "";
            divSuchana_ka_tamila.Visible = false;
            divSuchana_ka_upasthiti.Visible = false;
            ddlSuchana_ka_tamila.SelectedIndex = 0;
            ddlSuchana_ka_upasthiti.SelectedIndex = 0;
            if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 1)
            {
                ddlKiskeduwara_bhejagaya.Visible = true;
                txtsunwaiHetuNoticKaKaran.Visible = false;
                labNotice.Text = "माध्यम";
                div_sunwaiHetuNoticKaKaran.Visible = false;
                divSuchana_ka_upasthiti.Visible = true;
                divSuchana_ka_tamila.Visible = true;
            }
            else if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 2)
            {
                ddlKiskeduwara_bhejagaya.Visible = false;
                txtsunwaiHetuNoticKaKaran.Visible = true;
                labNotice.Text = "कारण स्पष्ट करें";
                div_sunwaiHetuNoticKaKaran.Visible = true;
            }
            else
            {
                ddlKiskeduwara_bhejagaya.Visible = false;
                txtsunwaiHetuNoticKaKaran.Visible = false;
                labNotice.Text = "";
                div_sunwaiHetuNoticKaKaran.Visible = false;

            }

            if (ddlKiskeduwara_bhejagaya.Visible == true)
            {
                ddlKiskeduwara_bhejagaya.SelectedValue = dtAnyaVivaran.Rows[0]["given_info_type"].ToString();

            }
            if (txtsunwaiHetuNoticKaKaran.Visible == true)
            {
                txtsunwaiHetuNoticKaKaran.Text = dtAnyaVivaran.Rows[0]["given_info_desc"].ToString();

            }


            if (divSuchana_ka_tamila.Visible == true)
            {
                ddlSuchana_ka_tamila.SelectedValue = dtAnyaVivaran.Rows[0]["prativadi_ko_suchana_ka_taamila_praapt_hai"].ToString();
            }
            if (ddlSuchana_ka_upasthiti.Visible == true)
            {
                ddlSuchana_ka_upasthiti.SelectedValue = dtAnyaVivaran.Rows[0]["prativadi_upasthit_hua_hai"].ToString();

            }
        }

        SqlParameter _a_id1 = new SqlParameter("@a_id", ViewState["a_id"]);
        SqlParameter _fn1 = new SqlParameter("@fn", "getPrativadi");
        DataTable dtPratiwadi = clsData.GetDataTableWithProc("usp_PrativadiAnyVivran", new SqlParameter[] { _a_id1, _fn1 });
        ViewState["pratiVadiDetails"] = dtPratiwadi;
        pratiWadi_grid.DataSource = dtPratiwadi;
        pratiWadi_grid.DataBind();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {

        if (ViewState["Step"].ToString() == "2")
        {
            step1.Visible = true;
            step2.Visible = false;
            ViewState["Step"] = "1";
            MatterRegistrationGet();
            btnBack.Visible = false;
        }
        if (ViewState["Step"].ToString() == "3")
        {
            step2.Visible = true;
            step3.Visible = false;
            ViewState["Step"] = "2";
            PrativadiAnyVivranGet();
         
        }
        if (ViewState["Step"].ToString() == "4")
        {
            step3.Visible = true;
            step4.Visible = false;
            ViewState["Step"] = "3";
            GetKhataKhesrabhumikavivaran();
          
        }
        Headingstep();
    }
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    [System.Web.Services.WebMethod()]
    public static string Getpdf(string url)
    {
        Encryptor enc = new Encryptor(Encryptor.PrivateKey);
        string urlpath = "";
        string encPathgov = enc.EncodeTo64(url);
        if (System.Web.HttpContext.Current.Session["aes256key"].ToString() != null || System.Web.HttpContext.Current.Session["aes256key"].ToString() != null)
        {
            encPathgov = Aes256CbcEncrypterApp.Encrypt(encPathgov, System.Web.HttpContext.Current.Session["aes256key"].ToString());
            urlpath = encPathgov;
        }
       
        //string urlpath = "";
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
    //=====================
    public void Headingstep()
    {
        hstep1.Attributes.Add("class", hstep1.Attributes["class"].ToString().Replace("ActiveClass", "RemoveClass"));
        hstep2.Attributes.Add("class", hstep2.Attributes["class"].ToString().Replace("ActiveClass", "RemoveClass"));
        hstep3.Attributes.Add("class", hstep3.Attributes["class"].ToString().Replace("ActiveClass", "RemoveClass"));
       
        hIstep1.Style.Add("color", "lightgray");
        hIstep2.Style.Add("color", "lightgray");
        hIstep3.Style.Add("color", "lightgray");
        
        ///// hstep8.Attributes.Add("class", hstep1.Attributes["class"].ToString().Replace("ActiveClass", "RemoveClass"));

        if (ViewState["Step"].ToString() == "1")
        {
            hstep1.Attributes.Add("class", hstep1.Attributes["class"].ToString().Replace("RemoveClass", "ActiveClass"));
            hIstep1.Style.Add("color", "darkblue");
        }
        if (ViewState["Step"].ToString() == "2")
        {
            hstep2.Attributes.Add("class", hstep2.Attributes["class"].ToString().Replace("RemoveClass", "ActiveClass"));
            hIstep2.Style.Add("color", "darkblue");
        }
        
        else if (ViewState["Step"].ToString() == "3")
        {
            hstep3.Attributes.Add("class", hstep3.Attributes["class"].ToString().Replace("RemoveClass", "ActiveClass"));
            hIstep3.Style.Add("color", "darkblue");
        }
        else if (ViewState["Step"].ToString() == "4")
        {
            hstep4.Attributes.Add("class", hstep3.Attributes["class"].ToString().Replace("RemoveClass", "ActiveClass"));
            hIstep4.Style.Add("color", "darkblue");
        }

    }
    protected void AdharYearsBind()
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
    void BindDist_Wadi_Pratiwadi()
    {
        try
        {
            string sql = @"SELECT distinct DISTRICTNAME,DISTRICTCODE from mst_Commissionary_Districts ORDER BY DISTRICTNAME ";
            DataTable dt = clsData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                ddlUserDist.DataSource = dt;
                ddlUserDist.DataTextField = "DISTRICTNAME";
                ddlUserDist.DataValueField = "DISTRICTCODE";
                ddlUserDist.DataBind();
                ddlUserDist.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlPDistrict.DataSource = dt;
                ddlPDistrict.DataTextField = "DISTRICTNAME";
                ddlPDistrict.DataValueField = "DISTRICTCODE";
                ddlPDistrict.DataBind();
                ddlPDistrict.Items.Insert(0, new ListItem("--Select--", "0"));

                dt.Dispose();
            }
        }
        catch (Exception ex) { }


    }
    void BindSubDivision_wadi()
    {
        try
        {

            string sql = @" select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddlUserDist.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode });
            ddlUserSubdivision.DataSource = dt;
            ddlUserSubdivision.DataTextField = "SubDivisionName";
            ddlUserSubdivision.DataValueField = "SubDivisionCode";
            ddlUserSubdivision.DataBind();
            ddlUserSubdivision.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }

    }

    #region Khata Khesra bhumi ka vivaran

    protected void bindLandUnit()
    {
        try
        {
            SqlParameter fl = new SqlParameter("@filter", 1);
            DataTable dt = clsData.GetDataTableWithProc("SP_GetLandUnit", new SqlParameter[] { fl });

            SqlParameter fl1 = new SqlParameter("@filter", 2);
            DataTable dt1 = clsData.GetDataTableWithProc("SP_GetLandUnit", new SqlParameter[] { fl1 });

            SqlParameter fl2 = new SqlParameter("@filter", 3);
            DataTable dt2 = clsData.GetDataTableWithProc("SP_GetLandUnit", new SqlParameter[] { fl2 });


            ddlrakabasankhya.DataSource = dt;
            ddlrakabasankhya.DataTextField = "name_hin";
            ddlrakabasankhya.DataValueField = "id";
            ddlrakabasankhya.DataBind();
            ddlrakabasankhya.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();

            ddlrakabasankhya1.DataSource = dt1;
            ddlrakabasankhya1.DataTextField = "name_hin";
            ddlrakabasankhya1.DataValueField = "id";
            ddlrakabasankhya1.DataBind();
            ddlrakabasankhya1.Items.Insert(0, new ListItem("--Select--", "0"));
            dt1.Dispose();

            ddlrakabasankhya2.DataSource = dt2;
            ddlrakabasankhya2.DataTextField = "name_hin";
            ddlrakabasankhya2.DataValueField = "id";
            ddlrakabasankhya2.DataBind();
            ddlrakabasankhya2.Items.Insert(0, new ListItem("--Select--", "0"));
            dt2.Dispose();


        }
        catch (Exception ee)
        {


        }
    }
    protected void bind_khatiyan_Type()// खतियन में किस्म जमीन की विवरणी :
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_GetKhatiyan_Type_public1", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlkhatiyan_me_jaminvivran.DataSource = dt;
                    ddlkhatiyan_me_jaminvivran.DataTextField = "Landdesciption";
                    ddlkhatiyan_me_jaminvivran.DataValueField = "id";
                    ddlkhatiyan_me_jaminvivran.DataBind();
                    ddlkhatiyan_me_jaminvivran.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
        }
        catch (Exception ee)
        {


        }
    }

    protected bool validationsaveBhumiKaVivaran()
    {
        bool chk = true;
        if (txtkhatasankhya.Text == "")
        {
            chk = false;
        }
        if (txtkhesarasankhya.Text == "")
        {
            chk = false;
        }
        if (txtrakabasankhya.Text == "")
        {
            chk = false;
        }
        if (txtrakabasankhya1.Text == "")
        {
            chk = false;
        }
        if (ddlrakabasankhya.SelectedValue == "0")
        {
            chk = false;
        }
        if (ddlrakabasankhya1.SelectedValue == "0")
        {
            chk = false;
        }
        if (ddlrakabasankhya2.SelectedValue == "0")
        {
            chk = false;
        }
        if (ddlkhatiyan_me_jaminvivran.SelectedValue == "0")
        {
            chk = false;
        }
        return chk;
    }

    public bool ValidateBhumiKaVivaran()
    {
      
        if (ddldistrict_old.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया जिला चुनें......!');", true);
            ddldistrict_old.Focus();
            return false;
        }
        if (ddl_anchalold.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अंचल  चुनें...!');", true);
            ddl_anchalold.Focus();
            return false;
        }
        if (ddlhalka_old.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया हल्का चुनें...!');", true);
            ddlhalka_old.Focus();
            return false;
        }
        if (ddlmowja_old.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया मौजा चुनें...!');", true);
            ddlmowja_old.Focus();
            return false;
        }
        // Validate Khata Number
        if (string.IsNullOrWhiteSpace(txtkhatasankhya.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया खाता संख्या अंकित करें...!');", true);
            txtkhatasankhya.Focus();
            return false;
        }

        // Validate Khesra Number
        if (string.IsNullOrWhiteSpace(txtkhesarasankhya.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया खेसरा संख्या अंकित करें...!');", true);
            txtkhesarasankhya.Focus();
            return false;
        }

        // Validate Rukba (Land Area) Number
        if (string.IsNullOrWhiteSpace(txtrakabasankhya.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया रकबा अंकित करें...!');", true);
            txtrakabasankhya.Focus();
            return false;
        }

        // Validate Rukba Unit for First Rukba
        if (ddlrakabasankhya.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया रकबा का मात्रक चुनें...!');", true);
            ddlrakabasankhya.Focus();
            return false;
        }

        // Validate Second Rukba
        if (string.IsNullOrWhiteSpace(txtrakabasankhya1.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया रकबा अंकित करें...!');", true);
            txtrakabasankhya1.Focus();
            return false;
        }

        // Validate Rukba Unit for Second Rukba
        if (ddlrakabasankhya1.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया रकबा का मात्रक चुनें...!');", true);
            ddlrakabasankhya1.Focus();
            return false;
        }

        // Validate Third Rukba
        if (string.IsNullOrWhiteSpace(txtrakabasankhya2.Text))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया रकबा अंकित करें...!');", true);
            txtrakabasankhya2.Focus();
            return false;
        }

        // Validate Rukba Unit for Third Rukba
        if (ddlrakabasankhya2.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया रकबा का मात्रक चुनें...!');", true);
            ddlrakabasankhya2.Focus();
            return false;
        }

        // Validate Land Type in Khatiyan
        if (ddlkhatiyan_me_jaminvivran.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया खतियान में जमीन की किस्म का विवरण चुनें...!');", true);
            ddlkhatiyan_me_jaminvivran.Focus();
            return false;
        }

        // If all validations pass, return true
        return true;
    }
    private bool CheckDuplicateInViewState()
    {
        if (ViewState["BhumiKaViVaranDT"] == null)
            return false;

        DataTable dt = (DataTable)ViewState["BhumiKaViVaranDT"];
        if (dt.Rows.Count == 0)
            return false;

        string halka = ddlhalka_old.SelectedValue.Trim();
        string mauza = ddlmowja_old.SelectedValue.Trim();
        string khata = txtkhatasankhya.Text.Trim();
        string khesra = txtkhesarasankhya.Text.Trim();

        foreach (DataRow row in dt.Rows)
        {
            if (
                row["Vivadith_Halka"].ToString().Trim() == halka &&
                row["Vivadith_Mauza"].ToString().Trim() == mauza &&
                row["khataNo"].ToString().Trim() == khata &&
                row["khesraNo"].ToString().Trim() == khesra
               )
            {
                return true; // 🔴 DUPLICATE FOUND
            }
        }

        return false;
    }

    protected void btnsaveBhumiKaVivaran_Click(object sender, EventArgs e)
    {
        if (CheckDuplicateInViewState())
        {
            Utility.showMessage(Page,
                "यह हल्का, मौजा, खाता एवं खेसरा संख्या पहले से जोड़ी जा चुकी है।");
            return;
        }
        //if (CheckKhataKeshraEntry())
        //{
        //    // Duplicate found → show message and stop execution
        //    Utility.showMessage(Page, "Someone has already registered with this same Mouja, Khata, Khesra number.");
        //    return; // do not execute rest of the function
        //}

        if (ValidateBhumiKaVivaran())
        {
            //ViewState["BhumiKaViVaranDT"] = BhumiKaViVaranDT();
            //BindGridhumiKaVivaran();
            DataTable dt = BhumiDT;
            //DataTable dt = (DataTable)ViewState["BhumiKaViVaranDT"];
            Response.Write("Columns in DataTable: " + dt.Columns.Count);
            //Response.Write(dt.Columns["Vivadith_Mauza"].DataType);
            dt.Rows.Add(

             ddldistrict_old.SelectedValue.ToString(),
             ddl_anchalold.SelectedValue.ToString(),
             ddlhalka_old.SelectedValue.ToString(),
              ddlmowja_old.SelectedValue.ToString(),


                txtkhatasankhya.Text.Trim(),
                txtkhesarasankhya.Text.Trim(),
                txtrakabasankhya.Text.Trim(),
                ddlrakabasankhya.SelectedValue.Trim(),
                txtrakabasankhya1.Text.Trim(),
                ddlrakabasankhya1.SelectedValue.Trim(),
                txtrakabasankhya2.Text.Trim(),
                ddlrakabasankhya2.SelectedValue.Trim(),
                ddlkhatiyan_me_jaminvivran.SelectedValue,
                txtkhatiyan_me_jaminvivran_text.Text.Trim(),
                txtuttari_chohaddi.Text.Trim(),
                txtdakshini_chohaddi.Text.Trim(),
                txtpurvi_chohaddi.Text.Trim(),
                txtpashchimi_chohaddi.Text.Trim(),
                ddlkhatiyan_me_jaminvivran.SelectedItem.Text.Trim(),
                txtrakabasankhya.Text.Trim() + " " + ddlrakabasankhya.SelectedItem.ToString() + ", " + txtrakabasankhya1.Text.Trim() + " " + ddlrakabasankhya1.SelectedItem.ToString() + ", " + txtrakabasankhya2.Text.Trim() + " " + ddlrakabasankhya2.SelectedItem.ToString(),

                 ddldistrict_old.SelectedItem.Text,
                ddl_anchalold.SelectedItem.Text,
                 ddlhalka_old.SelectedItem.Text,
                ddlmowja_old.SelectedItem.Text





                );
           
            //if (validationsaveBhumiKaVivaran() == true)
            //{
            //    
            //}
            BindGridhumiKaVivaran();
            ddlrakabasankhya.SelectedIndex = 0;
            txtrakabasankhya1.Text = "";
            ddlrakabasankhya1.SelectedIndex = 0;
            txtrakabasankhya2.Text = "";
            ddlrakabasankhya2.SelectedIndex = 0;

            txtkhatasankhya.Text = "";
            txtkhesarasankhya.Text = "";
            txtrakabasankhya.Text = "";
            ddlkhatiyan_me_jaminvivran.SelectedIndex = 0;
            txtkhatiyan_me_jaminvivran_text.Text = "";
            txtuttari_chohaddi.Text = "";
            txtdakshini_chohaddi.Text = "";
            txtpurvi_chohaddi.Text = "";
            txtpashchimi_chohaddi.Text = "";
        }
    }
    private DataTable BhumiKaViVaranDT()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        //ViewState.Remove("BhumiKaViVaranDT");



        dt.Columns.Add("Vivadith_District_Code", typeof(string));
        dt.Columns.Add("Vivadith_Block_Code", typeof(string));
        dt.Columns.Add("Vivadith_halka", typeof(string));
        dt.Columns.Add("Vivadith_Mauza", typeof(string));

        dt.Columns.Add("khataNo", typeof(string));
        dt.Columns.Add("khesraNo", typeof(string));
       

        dt.Columns.Add("RakbaNo1", typeof(string));
        dt.Columns.Add("Rakba_unit1", typeof(int));
        dt.Columns.Add("RakbaNo2", typeof(string));
        dt.Columns.Add("Rakba_unit2", typeof(int));
        dt.Columns.Add("RakbaNo3", typeof(string));
        dt.Columns.Add("Rakba_unit3", typeof(int));

        dt.Columns.Add("LandTypesInKhatian", typeof(int));
        dt.Columns.Add("LandDetailsInKhatian", typeof(string));
        dt.Columns.Add("North_chauhaddee", typeof(string));
        dt.Columns.Add("South_chauhaddee", typeof(string));
        dt.Columns.Add("East_chauhaddee", typeof(string));
        dt.Columns.Add("West_chauhaddee", typeof(string));

        dt.Columns.Add("LandTypesInKhatianDesc", typeof(string));
        dt.Columns.Add("Rakba", typeof(string));


        dt.Columns.Add("District", typeof(string));
        dt.Columns.Add("Block", typeof(string));
        dt.Columns.Add("Halka", typeof(string));
        dt.Columns.Add("Mauza", typeof(string));



        return dt;
    }
    protected void BindGridhumiKaVivaran()
    {
        grd_bhumivivad.DataSource = (DataTable)ViewState["BhumiKaViVaranDT"]; ;
        grd_bhumivivad.DataBind();
    }
    //protected void BindGridhumiKaVivaran()
    //{

    //    try
    //    {
    //        grd_bhumivivad.DataSource = (DataTable)ViewState["BhumiKaViVaranDT"];
    //        grd_bhumivivad.DataBind();
    //    }
    //    catch { }




    //}
    protected void grd_bhumivivad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            string rowIndex = e.CommandArgument.ToString();
            DataTable dt = (DataTable)ViewState["BhumiKaViVaranDT"];
            dt.Rows.RemoveAt(Convert.ToInt32(rowIndex));
            ViewState["BhumiKaViVaranDT"] = dt;
            BindGridhumiKaVivaran();

        }
    }
    public void KhataKhesrabhumikavivaran()
    {
        if (CheckKhataKeshraEntry())
        {
            // Duplicate found → show message and stop execution
            Utility.showMessage(Page, "Someone has already registered with this same Mouja, Khata, Khesra number.");
            return; // do not execute rest of the function
        }
        if (validationMatterRegistration() == true)
        {
            string Vadi_sakshya_FilePath = "";
            string Prativadi_sakshya_FilePath = "";
            Guid obj = Guid.NewGuid();
            string id = obj.ToString();

            string userid = ViewState["a_id"].ToString();
            if (AppDoc.HasFile)
            {

                string m = FileUploadValidator.IsPdf(AppDoc.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    Vadi_sakshya_FilePath = "~/LandDoc/Upload/ID" + id + "/Vadi_sakshya_File.pdf";

                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    return;
                }
            }
            else
            {
                Utility.showMessage(this,"please Upload the vadi file");
                return;
            }




            string message = "";
            if (AppDoc.HasFile)
            {
                string v = InsSaveFile("Vadi_sakshya_File", AppDoc, id);
                string pa = "~/LandDoc/Upload/ID" + id + "/Vadi_sakshya_File.pdf";
                pathfile.Value = v;
                if (v == "0" || v != pa)
                {
                    lblMsg.Text = "Technical Error";
                    return;
                }
            }
            string sql = @"select DIVISIONCODE from mst_Commissionary_Districts where DISTRICTCODE=@DistrictCode";
            SqlParameter _DistrictCode = new SqlParameter("@DistrictCode", ddlDistrict.SelectedValue.Trim());
            DataTable dt1 = clsData.GetDataTable(sql, new SqlParameter[] { _DistrictCode });
            if(dt1.Rows.Count > 0)
            {
                Session["Commsionary_Code1"] = dt1.Rows[0]["DIVISIONCODE"].ToString();
            }





            SqlParameter Range_Code = new SqlParameter("@Range_Code", "");
            SqlParameter Comm_Code = new SqlParameter("@Comm_Code", Session["Commsionary_Code1"].ToString());
            SqlParameter rajasv_thaana_sankhya = new SqlParameter("@rajasv_thaana_sankhya", txtrajaswa_sankhya.Text.Trim());
            SqlParameter Bhumitype = new SqlParameter("@Bhumitype", ddlbhumitype.SelectedValue.Trim());
            SqlParameter SarkariBhumiType = new SqlParameter("@SarkariBhumiType", ddlsarkaribhumitype.SelectedValue.Trim());
            SqlParameter SarkariBhumiType_Anya = new SqlParameter("@SarkariBhumiType_Anya", txtsarkaribhumitype_Anya.Text.Trim());
            SqlParameter BhumiVivadType = new SqlParameter("@BhumiVivadType", ddlbhumivivadtype.SelectedValue.Trim());
            SqlParameter BhumiVivadType_Anya = new SqlParameter("@BhumiVivadType_Anya", txtbhumivivad_Anya.Text.Trim());
            SqlParameter bhumi_vivad_ka_adyatan_sthiti = new SqlParameter("@bhumi_vivad_ka_adyatan_sthiti", ddl_vivad_adyatan_sthiti.SelectedValue.Trim());
            //Save in ActionDetailsEntry
            // Not Save SqlParameter Bhumi_savedansheelta = new SqlParameter("@Bhumi_savedansheelta", ddlbhumivivadki_sanvedanshilta.SelectedValue.Trim());
            SqlParameter District_Code = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.Trim());
            SqlParameter Sub_DivCode = new SqlParameter("@Sub_DivCode", ddlSubdivision.SelectedValue.Trim());
            SqlParameter Block_Code = new SqlParameter("@Block_Code", ddlBlock.SelectedValue.Trim());
            SqlParameter Thana_code = new SqlParameter("@Thana_code", ddlPolice.SelectedValue.Trim());
            SqlParameter Panchayat_Code = new SqlParameter("@Panchayat_Code", ddlPanchyat.SelectedValue.Trim());
            SqlParameter Panchayat_Anya = new SqlParameter("@Panchayat_Anya", txtPanchyat_Anya.Text.Trim());
            SqlParameter AreaType = new SqlParameter("@AreaType", ddlareatype.SelectedValue.Trim());
            SqlParameter Village = new SqlParameter("@Village", ddlVillage.SelectedValue.Trim());
            SqlParameter Village_Anya = new SqlParameter("@Village_Anya", txtVillage_Anya.Text.Trim());
            SqlParameter WardNo = new SqlParameter("@WardNo", ddlWard.SelectedValue.Trim());
            SqlParameter WardNo_Anya = new SqlParameter("@WardNo_Anya", txtWard_Anya.Text.Trim());

            SqlParameter Vadi_sakshya_File = new SqlParameter("@Vadi_sakshya_File", Vadi_sakshya_FilePath);
            SqlParameter Prativadi_sakshya_File = new SqlParameter("@Prativadi_sakshya_File", Prativadi_sakshya_FilePath);

            SqlParameter CUUser = new SqlParameter("@CUUser", Session["UserId"].ToString());
            SqlParameter CUIPAddress = new SqlParameter("@CUIPAddress", GetUserIP().ToString());
            SqlParameter AavedanKiTithi = new SqlParameter("@AavedanKiTithi", Convert.ToDateTime(txtAwadenKiTithi.Text.ToString()));

            SqlParameter VadiVivarani = new SqlParameter("@VadiVivarani", txtVadiVivarani.Text.ToString());
            //  SqlParameter PrativadiVivarani = new SqlParameter("@PrativadiVivarani", txtPrativadiVivarani.Text.ToString());

            SqlParameter a_id = new SqlParameter("@a_id", ViewState["a_id"]);
            SqlParameter Guid_id = new SqlParameter("@Guid", id);
            DataTable BhumiKaViVaranDT1 = (DataTable)ViewState["BhumiKaViVaranDT"];
            DataTable BhumiKaViVaranDT = BhumiKaViVaranDT1.Copy();
            try
            {

                BhumiKaViVaranDT.Columns.Remove("LandTypesInKhatianDesc");
                BhumiKaViVaranDT.Columns.Remove("Rakba");

                BhumiKaViVaranDT.Columns.Remove("District");
                BhumiKaViVaranDT.Columns.Remove("Block");
                BhumiKaViVaranDT.Columns.Remove("Halka");
                BhumiKaViVaranDT.Columns.Remove("Mauza");


            }
            catch { }
           
            SqlParameter LandDetailsEntryTable = new SqlParameter("@LandDetailsEntryTable", BhumiKaViVaranDT);
            DataTable dt = clsData.GetDataTableWithProc("usp_SaveBhumiKaKhataKhesraKaVivaranPublic", new SqlParameter[]
            {
                Range_Code
                ,Comm_Code
                ,rajasv_thaana_sankhya
                ,Bhumitype
                ,SarkariBhumiType
                ,SarkariBhumiType_Anya
                ,BhumiVivadType
                ,BhumiVivadType_Anya
                ,bhumi_vivad_ka_adyatan_sthiti
                ,District_Code
                ,Sub_DivCode
                ,Block_Code
                ,Thana_code
                ,Panchayat_Code
                ,Panchayat_Anya
                ,AreaType
                ,Village
                ,Village_Anya
                ,WardNo
                ,WardNo_Anya
                ,Vadi_sakshya_File
                ,Prativadi_sakshya_File
                ,CUUser
                ,CUIPAddress
                ,AavedanKiTithi
                ,VadiVivarani

                ,LandDetailsEntryTable
                ,a_id
                ,Guid_id
            });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {
                    Utility.showMessage(Page, "भूमि और भूमि का खाता खेसरा का विवरण सफलतापूर्वक सहेजा गया" + message);
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                    //  ClearPrativadiNAnyaVivaran();
                    ViewState["a_id"] = dt.Rows[0]["id"].ToString();
                    ViewState["Step"] = 4;
                    GetVadiPrativadiEvidanceDocument();
                    step3.Visible = false;
                    step4.Visible = true;
                    return;
                    ;
                }
                else
                {

                    lblMsg.Text = dt.Rows[0]["Message"].ToString();
                    Utility.showMessage(Page, dt.Rows[0]["Message"].ToString());
                    return;
                }
            }
        }



    }
    #endregion
    public void GetVadiPrativadiEvidanceDocument()
    {
        SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);
        SqlParameter fn = new SqlParameter("@fn", "Vadi");
        DataTable dtvadi = clsData.GetDataTableWithProc("usp_GetVadiPrativadiEvidanceDocument", new SqlParameter[] { _a_id, fn });
        ViewState["VadiEvidenceDetailsDT"] = dtvadi;
        gdVadiEvidence.DataSource = dtvadi;
        gdVadiEvidence.DataBind();

      
    }
    #region Vadi prativadi Evidance Document
    #region ANy Vivaran
    protected void ddlwadi_pratiwadi_sunwai_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlKiskeduwara_bhejagaya.SelectedIndex = 0;
        txtsunwaiHetuNoticKaKaran.Text = "";
        divSuchana_ka_tamila.Visible = false;
        divSuchana_ka_upasthiti.Visible = false;
        ddlSuchana_ka_tamila.SelectedIndex = 0;
        ddlSuchana_ka_upasthiti.SelectedIndex = 0;

        if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 1)
        {
            ddlKiskeduwara_bhejagaya.Visible = true;
            txtsunwaiHetuNoticKaKaran.Visible = false;
            labNotice.Text = "माध्यम";
            div_sunwaiHetuNoticKaKaran.Visible = false;
            divSuchana_ka_tamila.Visible = true;
        }
        else if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 2)
        {
            ddlKiskeduwara_bhejagaya.Visible = false;
            txtsunwaiHetuNoticKaKaran.Visible = true;
            labNotice.Text = "कारण स्पष्ट करें";
            div_sunwaiHetuNoticKaKaran.Visible = true;
        }
        else
        {
            ddlKiskeduwara_bhejagaya.Visible = false;
            txtsunwaiHetuNoticKaKaran.Visible = false;
            labNotice.Text = "";
            div_sunwaiHetuNoticKaKaran.Visible = false;

        }
    }
    protected void ddlSuchana_ka_tamila_SelectedIndexChanged(object sender, EventArgs e)
    {
        divSuchana_ka_upasthiti.Visible = true;
        ddlSuchana_ka_upasthiti.SelectedIndex = 0;
        if (ddlSuchana_ka_tamila.SelectedIndex == 1)
        {
            divSuchana_ka_upasthiti.Visible = true;
        }
    }

    #endregion
    public bool ValidateVadiEvidenceDetail()
    {
        // Validate "Is Vadi Evidence Available?"
        string ddlIsVadiEvi1 = ddlIsVadiEvi.SelectedValue.Trim();
        if (ddlIsVadiEvi1 == "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?  चुनें...!');", true);
            ddlIsVadiEvi.Focus();
            return false;
        }

        // Validate Evidence Type Selection
        if (ddlVadiEvidenceType.SelectedIndex == 0 && ddlIsVadiEvi1 == "Y")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया साक्ष्य का प्रकार चुनें...!');", true);
            ddlVadiEvidenceType.Focus();
            return false;
        }

        // Validate if Other Evidence Type is specified
        if (ddlVadiEvidenceType.SelectedValue == "9" && ddlIsVadiEvi1 == "Y")
        {
            if (txtVadiEvidenceType != null)
            {
                if (string.IsNullOrWhiteSpace(txtVadiEvidenceType.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अन्य साक्ष्य का प्रकार अंकित करें...!');", true);
                    txtVadiEvidenceType.Focus();
                    return false;
                }
            }
        }

        // Validate File Upload
        HttpPostedFile uploadedFile = file_vadi_dastavej_new.PostedFile;
        if (uploadedFile != null && uploadedFile.FileName != "")
        {
            string fileExtension = Path.GetExtension(uploadedFile.FileName).ToLower();
            string[] validExtensions = { ".pdf" }; // Assuming valid files are PDFs only
            if (!validExtensions.Contains(fileExtension))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Upload Document in Pdf File |');", true);
                file_vadi_dastavej_new.Focus();
                return false;
            }

            // Validate File Size (max 3 MB)
            if (uploadedFile.ContentLength > (3 * 1024 * 1024))  // File size limit: 3 MB
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('File size must be less than or equal to 3 MB |');", true);
                file_vadi_dastavej_new.Focus();
                return false;
            }
        }

        // All validations passed
        return true;
    }

    protected void btnAddVadiEvidence_Click(object sender, EventArgs e)
    {
        if (ValidateVadiEvidenceDetail())
        {
            DataTable dt = (DataTable)ViewState["VadiEvidenceDetailsDT"];
            string _Vadi_sakshya_File = string.Empty;
            string a_id = ViewState["a_id"].ToString();
            string fileupload = "";
            if (file_vadi_dastavej_new.HasFile)
            {

                if (file_vadi_dastavej_new.HasFile)
                {
                    if (!validateFile(file_vadi_dastavej_new, "doc"))
                        return;
                    string m = FileUploadValidator.IsPdf(file_vadi_dastavej_new.PostedFile, 1024, 1024);
                    if (m == "OK")
                    {
                        _Vadi_sakshya_File = "~/LandDoc/Upload/VadiEvidence" + a_id + "/VadiEvidence" + (dt.Rows.Count + 1) + ".pdf";
                        string path = "~/LandDoc/Upload/VadiEvidence" + a_id + "/";
                        string v = InsSaveFile("VadiEvidence" + (dt.Rows.Count + 1), file_vadi_dastavej_new, a_id, path);
                        fileupload = v;


                    }
                    else
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return;
                    }
                }
            }

            string pathcheck = "~/LandDoc/Upload/VadiEvidence" + a_id + "/VadiEvidence" + (dt.Rows.Count + 1) + ".pdf";
            if (fileupload != "0" && fileupload != "" && fileupload == pathcheck)
            {
                dt.Rows.Add(ddlVadiEvidenceType.SelectedValue.Trim(), ddlVadiEvidenceType.SelectedValue.Trim() != "9" ? ddlVadiEvidenceType.SelectedItem.Text.Trim() : txtVadiEvidenceType.Text, txtVadiEvidenceType.Text, _Vadi_sakshya_File);
                ViewState["VadiEvidenceDetailsDT"] = dt;
                BindGridVadiPrativadiEvidence();

                txtVadiEvidenceType.Text = "";
                ddlVadiEvidenceType.SelectedIndex = 0;
                file_vadi_dastavej_new.Dispose();
            }
            else if (fileupload == "")
            {
                lblMsg.Text = "Plz select file";
                return;
            }
            else
            {
                lblMsg.Text = "Technical Error";
                return;
            }
        }
    }
    protected void BindGridVadiPrativadiEvidence()
    {
        try
        {
            gdVadiEvidence.DataSource = (DataTable)ViewState["VadiEvidenceDetailsDT"];
            gdVadiEvidence.DataBind();
        }
        catch { }

       


    }
    protected void gdVadiEvidence_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            string rowIndex = e.CommandArgument.ToString();
            DataTable dt = (DataTable)ViewState["VadiEvidenceDetailsDT"];
            dt.Rows.RemoveAt(Convert.ToInt32(rowIndex));
            ViewState["VadiEvidenceDetailsDT"] = dt;
            BindGridVadiPrativadiEvidence();


        }
        if (e.CommandName == "ViewDoc")
        {
            string filePath = e.CommandArgument.ToString();

            if (filePath.StartsWith("~"))
            {
                filePath = filePath.Replace("~", "http://10.133.20.157:85");
            }

            // 🔹 Pehle Getpdf() ko call karke URL encrypt karna
            string encUrl = Getpdf(filePath);

            // 🔹 Ab naye tab me open karna
            string url = "../IDoc.aspx?url=" + Server.UrlEncode(encUrl);
            string script = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenDoc", script, true);
        }
    }
    private DataTable VadiEvidenceDetailsDT()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("evidence_id", typeof(string));
        dt.Columns.Add("evidence_name", typeof(string));
        dt.Columns.Add("evidence_any_name", typeof(string));
        dt.Columns.Add("FullfileName", typeof(string));
        return dt;
    }
    private DataTable PrativadiEvidenceDetailsDT()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("evidence_id", typeof(string));
        dt.Columns.Add("evidence_name", typeof(string));
        dt.Columns.Add("evidence_any_name", typeof(string));
        dt.Columns.Add("FullfileName", typeof(string));
        return dt;
    }
    protected void bindLandEvidence()
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_BindLandEvidence", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlVadiEvidenceType.DataSource = dt;
                    ddlVadiEvidenceType.DataTextField = "name";
                    ddlVadiEvidenceType.DataValueField = "id";
                    ddlVadiEvidenceType.DataBind();
                    ddlVadiEvidenceType.Items.Insert(0, new ListItem("--Select--", "0"));


                }
            }
        }
        catch (Exception ee)
        {


        }
    }
    protected void ddlEvidenceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divddlVadiEvidenceType.Attributes.Add("class", "col-md-6");
        divtxtVadiEvidenceType.Visible = false;
        if (ddlVadiEvidenceType.SelectedValue == "9")
        {
            // divddlVadiEvidenceType.Attributes.Add("class", "col-md-3");
            divtxtVadiEvidenceType.Visible = true;
        }
    }
    protected void ddlIsVadiEvi_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtVadiEvidenceType.Text = "";
        if (ddlIsVadiEvi.SelectedValue == "Y")
        {
            divVadiEvidenceType.Visible = true;
            divvadi_dastavej.Visible = true;

        }
        if (ddlIsVadiEvi.SelectedValue == "N")
        {
            divVadiEvidenceType.Visible = false;
            divvadi_dastavej.Visible = false;

        }

    }
    protected void gdVadiEvidence_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton('" + image1.ClientID + "')");


        }
    }

    protected string InsPicFile(string fileName, FileUpload fuFile, string a_id, string path)
    {
        string uploadDirectory = string.Empty;
        string pdfpath = FileSaveServer.getBase64(fuFile);
        string extension = string.Empty;
        extension = Path.GetExtension(fuFile.FileName).ToLower();
        uploadDirectory = path;
        string resi = FileSaveServer.InsertPicNew(uploadDirectory, pdfpath, fileName);      
        return resi;
    }
    public void CheckFile(string fileupload,string pathcheck,FileUpload photo)
    {
        if (fileupload != "0" && fileupload != "" && fileupload == pathcheck)
        {
            photo.Dispose();
            
        }
        else if (fileupload == "")
        {
            lblMsg.Text = "Plz select file";
            return;
        }
        else
        {
            lblMsg.Text = "Technical Error";
            return;
        }
    }
    public void VadiPrativadiEvidanceDocument()
    {
        //string _photo1 = "";
        //string _photo2 = "";
        //string _photo3 = "";
        //Int64 a_id = Convert.ToInt64(ViewState["a_id"]);

        //if (filePhoto1.HasFile)
        //{
        //    if (!validateFile(filePhoto1, "Image"))
        //        return;
        //    string m = FileUploadValidator.IsImage(filePhoto1.PostedFile, 1024, 1024);
        //    if (m == "OK")
        //    {
        //        _photo1 = "~/LandDoc/Upload/photo1" + a_id + "/photo1.jpeg";
        //        string path = "~/LandDoc/Upload/photo1" + a_id + "/";
        //        string v = InsPicFile("photo1", filePhoto1, a_id.ToString(), path);               
        //        CheckFile(_photo1,v, filePhoto1);
        //    }
        //    else
        //    {
        //        lblMsg.Text = "(पत्र केवल .jpeg,.png,.jpg प्रारूप में 3 MB तक में अपलोड करे)";
        //        return;
        //    }
        //}
        //if (filePhoto2.HasFile)
        //{
        //    if (!validateFile(filePhoto2, "Image"))
        //        return;
        //    string m = FileUploadValidator.IsImage(filePhoto2.PostedFile, 1024, 1024);
        //    if (m == "OK")
        //    {
        //        _photo2 = "~/LandDoc/Upload/photo2" + a_id + "/photo2.jpeg";
        //        string path = "~/LandDoc/Upload/photo2" + a_id + "/";
        //        string v = InsPicFile("photo2", filePhoto2, a_id.ToString(), path);
        //        CheckFile(_photo2, v, filePhoto2);
        //    }
        //    else
        //    {
        //        lblMsg.Text = "(पत्र केवल .jpeg,.png,.jpg प्रारूप में 3 MB तक में अपलोड करे)";
        //        return;
        //    }
        //}
        //if (filePhoto3.HasFile)
        //{
        //    if (!validateFile(filePhoto3, "Image"))
        //        return;
        //    string m = FileUploadValidator.IsImage(filePhoto3.PostedFile, 1024, 1024);
        //    if (m == "OK")
        //    {
        //        _photo3 = "~/LandDoc/Upload/photo3" + a_id + "/photo2.jpeg";
        //        string path = "~/LandDoc/Upload/photo3" + a_id + "/";
        //        string v = InsPicFile("photo3", filePhoto3, a_id.ToString(), path);
        //        CheckFile(_photo3, v, filePhoto3);
        //    }
        //    else
        //    {
        //        lblMsg.Text = "(पत्र केवल .jpeg,.png,.jpg प्रारूप में 3 MB तक में अपलोड करे)";
        //        return;
        //    }
        //}

        //starnew
        string _photo1 = "";
        string _photo2 = "";
        string _photo3 = "";
        Int64 a_id = Convert.ToInt64(ViewState["a_id"]);

        // ---------------------------
        // Photo 1 (Mandatory)
        // ---------------------------
        if (!filePhoto1.HasFile)
        {
            lblMsg.Text = "कृपया पहला फोटो अपलोड करें (3 MB तक, .jpeg, .png, .jpg)(कम से कम एक फोटो अपलोड करना अनिवार्य है)";
            return;
        }
        if (!validateFile1(filePhoto1, "Image"))
            return;

        string m1 = FileUploadValidator.IsImage(filePhoto1.PostedFile, 3, 5); // 3 MB max, 5 KB min
        if (m1 == "OK")
        {
            _photo1 = "~/LandDoc/Upload/photo1" + a_id + "/photo1.jpeg";
            string path = "~/LandDoc/Upload/photo1" + a_id + "/";
            string v = InsPicFile("photo1", filePhoto1, a_id.ToString(), path);
            CheckFile(_photo1, v, filePhoto1);
        }
        else
        {
            lblMsg.Text = "(केवल .jpeg, .png, .jpg प्रारूप में 3 MB तक अपलोड करें)";
            return;
        }

        // ---------------------------
        // Photo 2 (Optional)
        // ---------------------------
        if (filePhoto2.HasFile)
        {
            if (!validateFile1(filePhoto2, "Image"))
                return;

            string m2 = FileUploadValidator.IsImage1(filePhoto2.PostedFile, 3, 5);
            if (m2 == "OK")
            {
                _photo2 = "~/LandDoc/Upload/photo2" + a_id + "/photo2.jpeg";
                string path = "~/LandDoc/Upload/photo2" + a_id + "/";
                string v = InsPicFile("photo2", filePhoto2, a_id.ToString(), path);
                CheckFile(_photo2, v, filePhoto2);
            }
            else
            {
                lblMsg.Text = "(केवल .jpeg, .png, .jpg प्रारूप में 3 MB तक अपलोड करें)";
                return;
            }
        }

        // ---------------------------
        // Photo 3 (Optional)
        // ---------------------------
        if (filePhoto3.HasFile)
        {
            if (!validateFile1(filePhoto3, "Image"))
                return;

            string m3 = FileUploadValidator.IsImage1(filePhoto3.PostedFile, 3, 5);
            if (m3 == "OK")
            {
                _photo3 = "~/LandDoc/Upload/photo3" + a_id + "/photo3.jpeg"; // fixed wrong name "photo2.jpeg"
                string path = "~/LandDoc/Upload/photo3" + a_id + "/";
                string v = InsPicFile("photo3", filePhoto3, a_id.ToString(), path);
                CheckFile(_photo3, v, filePhoto3);
            }
            else
            {
                lblMsg.Text = "(केवल .jpeg, .png, .jpg प्रारूप में 3 MB तक अपलोड करें)";
                return;
            }
        }

        //end new


        DataTable VadiEvidenceDetailsDT1 = (DataTable)ViewState["VadiEvidenceDetailsDT"];
        DataTable VadiEvidenceDetailsDT = VadiEvidenceDetailsDT1.Copy();
        try
        {
            VadiEvidenceDetailsDT.Columns.Remove("evidence_name");
        }
        catch { }
       
        SqlParameter VadiEvidenceEntryTable = new SqlParameter("@VadiEvidenceEntryTable", VadiEvidenceDetailsDT);
        SqlParameter Photo1 = new SqlParameter("@Photo1", _photo1);
        SqlParameter Photo2 = new SqlParameter("@Photo2", _photo2);
        SqlParameter Photo3 = new SqlParameter("@Photo3", _photo3);
       

        SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt64(ViewState["a_id"]));

        DataTable dt = clsData.GetDataTableWithProc("usp_SaveVadiPrativadiEvidanceDocumentPublic", new SqlParameter[]
            {
                _a_id
                ,VadiEvidenceEntryTable
               ,Photo1
               ,Photo2
               ,Photo3

            });

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
            {
                //lblMsg.Text = "वादी और प्रतिवादी द्वारा प्रस्तुत साक्ष्य की विवरणी सफलतापूर्वक सहेजा गया";
                Utility.showMessage(Page, "वादी द्वारा प्रस्तुत साक्ष्य की विवरणी सफलतापूर्वक सहेजा गया");
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                ViewState["a_id"] = dt.Rows[0]["id"].ToString();
               
                ViewState["Step"] = 4;
                //step3.Visible = false;
                //step4.Visible = true;

                Encryptor enc = new Encryptor(Encryptor.PrivateKey);
                string UrlRedirect = enc.Encrypt(ViewState["a_id"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('आवेदनकर्ता का विवरण सफलतापूर्वक सहेजा गया');window.location='getDataPublic.aspx?RegId=" + UrlRedirect + "';", true);
                return;
            }
            else
            {

                lblMsg.Text = dt.Rows[0]["Message"].ToString();
                Utility.showMessage(Page, dt.Rows[0]["Message"].ToString());
                return;
            }

        }
    }
    #endregion
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ViewState["Step"]) == 1)
        {
            MatterRegistrationVadi();           
        }
        else if (Convert.ToInt16(ViewState["Step"]) == 2)
        {
            PrativadiAnyVivran();
        }
        else if (Convert.ToInt16(ViewState["Step"]) == 3)
        {
            KhataKhesrabhumikavivaran();
        }
        else if (Convert.ToInt16(ViewState["Step"]) == 4)
        {
            VadiPrativadiEvidanceDocument();
        }
        Headingstep();
    }
    protected void LinkBtnstep1_Click(object sender, EventArgs e)
    {

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {

    }
    protected void ddldistrict_old_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAncal_old();

    }
    protected void ddl_anchalold_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bindhalka_old();

    }
    protected void ddlhalka_old_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindMauja_old();
    }
    protected void ddlmowja_old_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    void BindDist2()
    {
        try
        {
            string sql = @" select distinct dist_code,distname from mst_district_old order by distname ";
            DataTable dt = clsData.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                ddldistrict_old.DataSource = dt;
                ddldistrict_old.DataTextField = "distname";
                ddldistrict_old.DataValueField = "dist_code";
                ddldistrict_old.DataBind();
                ddldistrict_old.Items.Insert(0, new ListItem("--Select--", "0"));
                dt.Dispose();
            }
        }
        catch (Exception ex) { }

    }
    void BindAncal_old()
    {
        try
        {


            string sql = @"select  distinct circlename,circle_code from mst_district_old where Dist_Code=@District_Code order by circlename";
            //string sql = @"select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and BlockCode in (select code from MstThanaMapping where thana_code=@thana_code)  order by BlockName";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddldistrict_old.SelectedValue.ToString());
            //SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString());
            //SqlParameter _thana_code = new SqlParameter("@thana_code", Session["Thana_Code"].ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode });


            ddl_anchalold.DataSource = dt;
            ddl_anchalold.DataTextField = "circlename";
            ddl_anchalold.DataValueField = "circle_code";
            ddl_anchalold.DataBind();
            ddl_anchalold.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }

        // BindVillage();
        // BindPanchyat();

    }

    void Bindhalka_old()
    {
        try
        {


            string sql = @"select  distinct halka_name,halka_code from mst_district_old where circle_code=@circlecode and Dist_Code=@District_Code order by halka_name";
            //string sql = @"select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and BlockCode in (select code from MstThanaMapping where thana_code=@thana_code)  order by BlockName";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddldistrict_old.SelectedValue.ToString());
            SqlParameter _CCode = new SqlParameter("@circlecode", ddl_anchalold.SelectedValue.ToString());
            //SqlParameter _thana_code = new SqlParameter("@thana_code", Session["Thana_Code"].ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _CCode });


            ddlhalka_old.DataSource = dt;
            ddlhalka_old.DataTextField = "halka_name";
            ddlhalka_old.DataValueField = "halka_code";
            ddlhalka_old.DataBind();
            ddlhalka_old.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }

        // BindVillage();
        // BindPanchyat();

    }
    void BindMauja_old()
    {
        try
        {


            string sql = @"select  distinct Mauja_code,Mauja_name from mst_district_old where circle_code=@circlecode and Dist_Code=@District_Code AND halka_code=@Halkacode order by Mauja_name";
            //string sql = @"select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and BlockCode in (select code from MstThanaMapping where thana_code=@thana_code)  order by BlockName";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddldistrict_old.SelectedValue.ToString());
            SqlParameter _CCode = new SqlParameter("@circlecode", ddl_anchalold.SelectedValue.ToString());
            SqlParameter _HCode = new SqlParameter("@Halkacode", ddlhalka_old.SelectedValue.ToString());
            //SqlParameter _thana_code = new SqlParameter("@thana_code", Session["Thana_Code"].ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _CCode, _HCode });


            ddlmowja_old.DataSource = dt;
            ddlmowja_old.DataTextField = "Mauja_name";
            ddlmowja_old.DataValueField = "Mauja_code";
            ddlmowja_old.DataBind();
            ddlmowja_old.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }

        // BindVillage();
        // BindPanchyat();

    }
}
   