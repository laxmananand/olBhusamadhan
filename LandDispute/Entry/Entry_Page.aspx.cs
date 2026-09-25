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
using System.Collections.Generic;
public partial class LandDispute_Entry_Entry_Page : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    whiteList wl = new whiteList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Role"]) == null || Convert.ToString(Session["Block_Code"]) == null || Convert.ToString(Session["Thana_Code"]) == null)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
        }
        ApplyADMHOMELayout();
        if (!IsPostBack)
        {
            txtPrativadiVivarani.Attributes.Add("maxlength", txtPrativadiVivarani.MaxLength.ToString());
            txtVadiVivarani.Attributes.Add("maxlength", txtVadiVivarani.MaxLength.ToString());

            txtuttari_chohaddi.Attributes.Add("maxlength", txtuttari_chohaddi.MaxLength.ToString());
            txtdakshini_chohaddi.Attributes.Add("maxlength", txtdakshini_chohaddi.MaxLength.ToString());
            txtpurvi_chohaddi.Attributes.Add("maxlength", txtpurvi_chohaddi.MaxLength.ToString());
            txtpashchimi_chohaddi.Attributes.Add("maxlength", txtpashchimi_chohaddi.MaxLength.ToString());

            txtpulis_padadhikari_vivarani.Attributes.Add("maxlength", txtpulis_padadhikari_vivarani.MaxLength.ToString());
            txthalkakarmchari_prativedan.Attributes.Add("maxlength", txthalkakarmchari_prativedan.MaxLength.ToString());
            txtbhukhand_reason.Attributes.Add("maxlength", txtbhukhand_reason.MaxLength.ToString());


            txtCancelReason.Attributes.Add("maxlength", txtCancelReason.MaxLength.ToString());
            txtfalafal.Attributes.Add("maxlength", txtfalafal.MaxLength.ToString());
            txtabhiyukt_anchaladhikari.Attributes.Add("maxlength", txtabhiyukt_anchaladhikari.MaxLength.ToString());
            txtabhiyukt_thaanprabhaaree.Attributes.Add("maxlength", txtabhiyukt_thaanprabhaaree.MaxLength.ToString());



            txtUserMohalla.Attributes.Add("maxlength", txtUserMohalla.MaxLength.ToString());
            txtghatanaDate.Attributes.Add("readonly", "readonly");
            txtbaithakDate.Attributes.Add("readonly", "readonly");
            txtAgalaDate.Attributes.Add("readonly", "readonly");
            txtMapiKeNirdharit_tithi.Attributes.Add("readonly", "readonly");
            
            txtAwadenKiTithi.Attributes.Add("readonly", "readonly");
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
            bindLandUnit();
            bind_khatiyan_Type();
            bindLandEvidence();
            BindSubDivision_wadi();
            BindNyayalaya();
            BindNyayalayaType();
            BindNyayalayaType_Vibhag();
            bind_BhumiSanvedanshilta();
            BindNyayalayaType_dist();
            BindNyayalayaType_SubDivision();
            lblbhukand_prativedan.Visible = false;
            lblbhukand_karan.Visible = false;
            file_bhukand_prativedan.Visible = false;
            Label15.Visible = false;

            txtbhukhand_reason.Visible = false;

            BindBlock_Wadi();
            BindPolice_wadi();
            BindPanchyat_Wadi();
            BindVillage_Wadi();
            bindward_Wadi();
            BindPanchyat();
            BindVillage();
            bindward();
            BindSubDivision_Pratiwadi();
            BindBlock_Pratiwadi();
            BindPolice_Prtiwadi();
            BindVillage_Pratiwadi();
            BindPanchyat_Prtiwadi();
            bindward_Pratiwadi();
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
            ViewState["BhumiKaViVaranDT"] = BhumiKaViVaranDT();


            ViewState["VadiEvidenceDetailsDT"] = VadiEvidenceDetailsDT();
            ViewState["PrativadiEvidenceDetailsDT"] = PrativadiEvidenceDetailsDT();

            ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
            ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();


            ViewState["a_id"] = 0;
            ViewState["Step"] = 1;

            //change start
            //ViewState["Step"] = 6;
            //step1.Visible = false;
            //step2.Visible = false;
            //step3.Visible = false;
            //step4.Visible = false;
            //step5.Visible = false;
            //step6.Visible = true;
            //step7.Visible = false;
            //change end

            BindDist();
            //pratiwadi
            BindSubDivision_Pratiwadi();

            onestar.Visible = false;
            twostar.Visible = false;
            threestar.Visible = false;
            fourstar.Visible = false;
            txtbhumivivad_Anya.Enabled = false;

            // ADMHOME (state level) has no district in session, so keep district selectable for them
            string sessionDistrict = Convert.ToString(Session["District_Code"]).Trim();
            if (sessionDistrict != "" && sessionDistrict != "0")
            {
                ddlDistrict.SelectedValue = sessionDistrict;
                ddlDistrict.Enabled = false;
            }
            BindSubDivision();
            if (Session["Sub_DivCode"] != null)
            {
                ddlSubdivision.SelectedValue = Session["Sub_DivCode"].ToString();
                // ddlSubdivision.Enabled = false;
            }
            BindBlock();
            if (Session["Block_Code"] != null)
            {
                ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                // ddlBlock.Enabled = false;
            }
            BindPolice();
            if (Session["Thana_Code"] != null)
            {
                ddlPolice.SelectedValue = Session["Thana_Code"].ToString();
                if (ddlPolice.SelectedValue.Trim() != "0")
                {
                    ddlPolice.Enabled = false;
                }
            }
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            if (Request.QueryString["RegId"] != null)
            {
                string Id = Request.QueryString["RegId"].ToString();
                //Id = enc.Decrypt(Id);

                SqlParameter _a_id = new SqlParameter("@a_id", Id);
                SqlParameter _CUUser = new SqlParameter("@Thana_code", Session["Thana_Code"].ToString());
                DataTable dtwadi = clsData.GetDataTableWithProc("usp_checkVlaidUser", new SqlParameter[] { _a_id, _CUUser });
                if (dtwadi.Rows.Count > 0)
                {
                    ViewState["a_id"] = Id;
                    ViewState["Step"] = 1;
                    MatterRegistrationGet();
                }
                else
                {
                    Response.Redirect("viewdata.aspx");
                }

            }


        }
    }
    // ADMHOME login sees only "वादी का विवरण": no step tabs, no department/organisation
    // questions, no Save, no "भूमि विवाद का विवरण" and no Preview / Save & Next
    void ApplyADMHOMELayout()
    {
        if (Convert.ToString(Session["Role"]).Trim() != "ADMHOME")
            return;

        divStepTabs.Visible = false;
        phVadiDeptOrg.Visible = false;
        divVadiSave.Visible = false;
        phBhumiVivad.Visible = false;
        btnBack.Visible = false;
        LinkBtnPreview.Visible = false;
        btnNext.Visible = false;

        // ADMHOME-only वादी fields: no birth year; pincode, document upload and remarks added
        divVadiBirthYear.Visible = false;
        phADMHOMEExtra.Visible = true;

        // ADMHOME wording: शिकायतकर्ता instead of वादी (SHO keeps वादी)
        litVadiSectionHeader.Text = "शिकायतकर्ता का विवरण";
        Label2.Text = "शिकायतकर्ता का नाम";
        txtNamePerAadhaar.Attributes["placeholder"] = "शिकायतकर्ता का नाम";

        // not mandatory for ADMHOME: पिता/पति का नाम, क्षेत्र का प्रकार, ग्राम पंचायत, राजस्व ग्राम, मोबाइल नंबर
        // (the mobile 10-digit format check stays, it only fires when a number is entered)
        RequiredFieldValidator12.Enabled = false;
        RequiredFieldValidator4.Enabled = false;
        RequiredFieldValidator5.Enabled = false;
        RequiredFieldValidator7.Enabled = false;
        RequiredFieldValidator14.Enabled = false;
    }

    // Hide the mandatory stars for ADMHOME. Done at PreRender because ddlUserAreatype_SelectedIndexChanged
    // turns the वार्ड star (UWard) back on for Urban areas after Page_Load has run.
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (Convert.ToString(Session["Role"]).Trim() == "ADMHOME")
        {
            starFName.Visible = false;
            starUserAreatype.Visible = false;
            starUserPanchyat.Visible = false;
            starUserVillage.Visible = false;
            starVadiMobile.Visible = false;
            UWard.Visible = false;
        }
    }

    // ADMHOME layout: move मोबाइल नंबर into the first row (the slot freed by the hidden birth year)
    // so no row is left half empty. Done in OnInit, before view state is loaded, so every request
    // builds the same control tree. Other roles keep the original layout.
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (Session != null && Convert.ToString(Session["Role"]).Trim() == "ADMHOME")
        {
            rowVadiMobile.Controls.Remove(divVadiMobile);
            rowVadiBasic.Controls.Add(divVadiMobile);
            rowVadiMobile.Visible = false;
        }
    }

    const int ADMHOME_MAX_FILE_BYTES = 5 * 1024 * 1024;
    const int ADMHOME_MAX_REMARK_WORDS = 500;

    // Server-side check of the ADMHOME-only fields (pincode, 5 MB pdf, 500-word remarks).
    // Returns "" when valid, otherwise the message to show. Call it from the ADMHOME save.
    string ValidateADMHOMEFields()
    {
        // pincode is optional; when entered it must be a valid 6-digit pincode
        // Indian pincode: exactly 6 digits, not starting with 0 (same rules as validateADMHOMEPincode in the page)
        string pincode = txtVadiPincode.Text.Trim();
        string pincodeError = "";
        if (pincode != "")
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(pincode, @"^[0-9]+$"))
                pincodeError = "पिनकोड में केवल अंक (0-9) होने चाहिए...";
            else if (pincode.Length != 6)
                pincodeError = "पिनकोड ठीक 6 अंकों का होना चाहिए (अभी " + pincode.Length + " अंक हैं).";
            else if (pincode[0] == '0')
                pincodeError = "पिनकोड 0 से शुरू नहीं हो सकता...";
        }
        if (pincodeError != "")
        {
            // show the message just below the पिनकोड box as well
            cvVadiPincode.ErrorMessage = pincodeError;
            cvVadiPincode.IsValid = false;
            return pincodeError;
        }

        // document is mandatory for ADMHOME
        if (!fuADMHOMEDoc.HasFile)
            return "कृपया दस्तावेज़ अपलोड करें...!";
        if (fuADMHOMEDoc.PostedFile.ContentLength > ADMHOME_MAX_FILE_BYTES)
            return "फाइल का आकार 5 MB से अधिक नहीं होना चाहिए...!";
        if (fuADMHOMEDoc.PostedFile.IsPdf(5120, 0) != "OK")
            return "कृपया केवल सही .pdf फाइल अपलोड करें...!";

        string remarks = txtADMHOMERemarks.Text.Trim();
        if (remarks.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length > ADMHOME_MAX_REMARK_WORDS)
            return "टिप्पणी अधिकतम 500 शब्दों की हो सकती है...!";

        return "";
    }

    #region ADMHOME वादी temporary list

    // Temporary (not yet in DB) वादी rows entered by ADMHOME. Rows live in ViewState, the uploaded
    // PDFs in Session (keyed by DocKey) so the page does not carry up to 5 MB per row in ViewState.
    DataTable GetVadiADMHOMETable()
    {
        DataTable dt = ViewState["vadiDetailsADMHOME"] as DataTable;
        if (dt != null)
        {
            // a list started before these columns existed
            if (!dt.Columns.Contains("IsFinalised"))
            {
                dt.Columns.Add("IsFinalised", typeof(bool));
                foreach (DataRow r in dt.Rows) r["IsFinalised"] = false;
            }
            if (dt.Columns.Contains("ApplicationNo") && !dt.Columns.Contains("FileNo"))
                dt.Columns["ApplicationNo"].ColumnName = "FileNo";   // list started before the File No. rename
            if (!dt.Columns.Contains("FileNo"))
                dt.Columns.Add("FileNo", typeof(string));
            return dt;
        }

        dt = new DataTable();
        dt.Columns.Add("vadi_Name", typeof(string));
        dt.Columns.Add("Vadi_Father_Husband_Name", typeof(string));
        dt.Columns.Add("SexAsPerAadhaar", typeof(string));
        dt.Columns.Add("Vadi_District_Code", typeof(string));
        dt.Columns.Add("Vadi_Sub_DivCode", typeof(string));
        dt.Columns.Add("Vadi_Block_Code", typeof(string));
        dt.Columns.Add("Vadi_Thana_code", typeof(string));
        dt.Columns.Add("Vadi_AreaType", typeof(string));
        dt.Columns.Add("Vadi_Panchayat_Code", typeof(string));
        dt.Columns.Add("Vadi_Village_Code", typeof(string));
        dt.Columns.Add("Vadi_WardNo", typeof(string));
        dt.Columns.Add("mohalla", typeof(string));
        dt.Columns.Add("Vadi_MobileNo", typeof(string));
        dt.Columns.Add("Pincode", typeof(string));
        dt.Columns.Add("Remarks", typeof(string));
        dt.Columns.Add("DocKey", typeof(string));
        dt.Columns.Add("DocName", typeof(string));
        // display text
        dt.Columns.Add("gender", typeof(string));
        dt.Columns.Add("dist", typeof(string));
        dt.Columns.Add("sub_division", typeof(string));
        dt.Columns.Add("block", typeof(string));
        dt.Columns.Add("thana", typeof(string));
        dt.Columns.Add("area_type", typeof(string));
        dt.Columns.Add("panchayt", typeof(string));
        dt.Columns.Add("village", typeof(string));
        dt.Columns.Add("WardNo", typeof(string));
        dt.Columns.Add("IsFinalised", typeof(bool));
        dt.Columns.Add("FileNo", typeof(string));   // File No. HDSBxxxxx, set when finalised (saved to DB)
        return dt;
    }

    Dictionary<string, byte[]> GetVadiADMHOMEDocs()
    {
        Dictionary<string, byte[]> docs = Session["vadiDocsADMHOME"] as Dictionary<string, byte[]>;
        if (docs == null)
        {
            docs = new Dictionary<string, byte[]>();
            Session["vadiDocsADMHOME"] = docs;
        }
        return docs;
    }

    static string SelectedValueOrEmpty(DropDownList ddl)
    {
        string v = ddl.SelectedValue.Trim();
        return (v == "" || v == "0") ? "" : v;
    }

    static string SelectedTextOrEmpty(DropDownList ddl)
    {
        return SelectedValueOrEmpty(ddl) == "" ? "" : ddl.SelectedItem.Text;
    }

    void AlertADMHOME(string message)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
    }

    // Fields still mandatory for ADMHOME: वादी का नाम, लिंग, जिला, अनुमंडल, अंचल, थाना (+ मोहल्ला for Urban)
    bool ValidateVadiADMHOME()
    {
        if (string.IsNullOrWhiteSpace(txtNamePerAadhaar.Text)) { AlertADMHOME("कृपया शिकायतकर्ता का नाम अंकित करें...!"); txtNamePerAadhaar.Focus(); return false; }
        if (ddlgender.SelectedIndex == 0) { AlertADMHOME("कृपया लिंग चुनें...!"); ddlgender.Focus(); return false; }
        if (SelectedValueOrEmpty(ddlUserDist) == "") { AlertADMHOME("कृपया जिला चुनें...!"); ddlUserDist.Focus(); return false; }
        if (SelectedValueOrEmpty(ddlUserSubdivision) == "") { AlertADMHOME("कृपया अनुमंडल चुनें...!"); ddlUserSubdivision.Focus(); return false; }
        if (SelectedValueOrEmpty(ddlUserBlock) == "") { AlertADMHOME("कृपया अंचल चुनें...!"); ddlUserBlock.Focus(); return false; }
        if (SelectedValueOrEmpty(ddlUserThana) == "") { AlertADMHOME("कृपया थाना चुनें...!"); ddlUserThana.Focus(); return false; }
        if (ddlUserAreatype.SelectedValue == "U" && string.IsNullOrWhiteSpace(txtUserMohalla.Text)) { AlertADMHOME("कृपया मोहल्ला का नाम अंकित करें...!"); txtUserMohalla.Focus(); return false; }

        string mobile = txtvadimobile.Text.Trim();
        if (mobile != "" && !System.Text.RegularExpressions.Regex.IsMatch(mobile, @"^[0-9]{10}$")) { AlertADMHOME("कृपया 10 अंकों का सही मोबाइल नंबर डालें...!"); txtvadimobile.Focus(); return false; }

        // column sizes of dbo.ADMHOME_VadiApplication
        if (txtNamePerAadhaar.Text.Trim().Length > 100) { AlertADMHOME("शिकायतकर्ता का नाम अधिकतम 100 अक्षरों का हो सकता है...!"); txtNamePerAadhaar.Focus(); return false; }
        if (txtFName.Text.Trim().Length > 100) { AlertADMHOME("पिता/ पति का नाम अधिकतम 100 अक्षरों का हो सकता है...!"); txtFName.Focus(); return false; }

        string msg = ValidateADMHOMEFields();
        if (msg != "") { AlertADMHOME(msg); return false; }
        return true;
    }

    static object DbValueOrNull(object value)
    {
        string s = Convert.ToString(value).Trim();
        return s == "" ? (object)DBNull.Value : s;
    }

    // Saves one temporary row (and its PDF) via dbo.usp_ADMHOME_InsertVadiApplication.
    // Returns the new application no. (HDSBxxxxx), or "" on failure (error is logged).
    string SaveVadiApplicationADMHOME(DataRow row, byte[] docBytes)
    {
        try
        {
            string cs = ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            using (SqlCommand cmd = new SqlCommand("dbo.usp_ADMHOME_InsertVadiApplication", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vadi_Name", Convert.ToString(row["vadi_Name"]));
                cmd.Parameters.AddWithValue("@Vadi_Father_Husband_Name", DbValueOrNull(row["Vadi_Father_Husband_Name"]));
                cmd.Parameters.AddWithValue("@SexAsPerAadhaar", Convert.ToString(row["SexAsPerAadhaar"]));
                cmd.Parameters.AddWithValue("@Vadi_District_Code", Convert.ToInt64(row["Vadi_District_Code"]));
                cmd.Parameters.AddWithValue("@Vadi_Sub_DivCode", Convert.ToInt64(row["Vadi_Sub_DivCode"]));
                cmd.Parameters.AddWithValue("@Vadi_Block_Code", Convert.ToInt64(row["Vadi_Block_Code"]));
                cmd.Parameters.AddWithValue("@Vadi_Thana_code", Convert.ToInt64(row["Vadi_Thana_code"]));
                cmd.Parameters.AddWithValue("@Vadi_AreaType", DbValueOrNull(row["Vadi_AreaType"]));
                cmd.Parameters.AddWithValue("@Vadi_Panchayat_Code", DbValueOrNull(row["Vadi_Panchayat_Code"]));
                cmd.Parameters.AddWithValue("@Vadi_Village_Code", DbValueOrNull(row["Vadi_Village_Code"]));
                cmd.Parameters.AddWithValue("@Vadi_WardNo", DbValueOrNull(row["Vadi_WardNo"]));
                cmd.Parameters.AddWithValue("@mohalla", DbValueOrNull(row["mohalla"]));
                cmd.Parameters.AddWithValue("@Vadi_MobileNo", DbValueOrNull(row["Vadi_MobileNo"]));
                cmd.Parameters.AddWithValue("@PinCode", DbValueOrNull(row["Pincode"]));
                cmd.Parameters.AddWithValue("@Remarks", DbValueOrNull(row["Remarks"]));
                cmd.Parameters.AddWithValue("@CreatedBy", Convert.ToString(Session["UserID"]));
                cmd.Parameters.AddWithValue("@CreatedIP", GetUserIP());
                cmd.Parameters.AddWithValue("@FileName", Convert.ToString(row["DocName"]));
                cmd.Parameters.Add("@FileData", SqlDbType.VarBinary, -1).Value = docBytes;

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return Convert.ToString(dr["FileNo"]);
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
        }
        return "";
    }

    protected void btnAddVadiADMHOME_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["Role"]).Trim() != "ADMHOME" || !ValidateVadiADMHOME())
            return;

        string docKey = "";
        string docName = "";
        if (fuADMHOMEDoc.HasFile)
        {
            docKey = Guid.NewGuid().ToString("N");
            docName = Path.GetFileName(fuADMHOMEDoc.FileName);
            GetVadiADMHOMEDocs()[docKey] = fuADMHOMEDoc.FileBytes;
        }

        DataTable dt = GetVadiADMHOMETable();
        DataRow row = dt.NewRow();
        row["vadi_Name"] = txtNamePerAadhaar.Text.Trim();
        row["Vadi_Father_Husband_Name"] = txtFName.Text.Trim();
        row["SexAsPerAadhaar"] = ddlgender.SelectedValue.ToUpper();
        row["Vadi_District_Code"] = SelectedValueOrEmpty(ddlUserDist);
        row["Vadi_Sub_DivCode"] = SelectedValueOrEmpty(ddlUserSubdivision);
        row["Vadi_Block_Code"] = SelectedValueOrEmpty(ddlUserBlock);
        row["Vadi_Thana_code"] = SelectedValueOrEmpty(ddlUserThana);
        row["Vadi_AreaType"] = SelectedValueOrEmpty(ddlUserAreatype);
        row["Vadi_Panchayat_Code"] = SelectedValueOrEmpty(ddlUserPanchyat);
        row["Vadi_Village_Code"] = SelectedValueOrEmpty(ddlUserVillage);
        row["Vadi_WardNo"] = SelectedValueOrEmpty(ddlUserWard);
        row["mohalla"] = txtUserMohalla.Text.Trim();
        row["Vadi_MobileNo"] = txtvadimobile.Text.Trim();
        row["Pincode"] = txtVadiPincode.Text.Trim();
        row["Remarks"] = txtADMHOMERemarks.Text.Trim();
        row["DocKey"] = docKey;
        row["DocName"] = docName;
        row["gender"] = ddlgender.SelectedItem.Text;
        row["dist"] = SelectedTextOrEmpty(ddlUserDist);
        row["sub_division"] = SelectedTextOrEmpty(ddlUserSubdivision);
        row["block"] = SelectedTextOrEmpty(ddlUserBlock);
        row["thana"] = SelectedTextOrEmpty(ddlUserThana);
        row["area_type"] = SelectedTextOrEmpty(ddlUserAreatype);
        row["panchayt"] = SelectedTextOrEmpty(ddlUserPanchyat);
        row["village"] = SelectedTextOrEmpty(ddlUserVillage);
        row["WardNo"] = SelectedTextOrEmpty(ddlUserWard);
        row["IsFinalised"] = false;
        dt.Rows.Add(row);

        ViewState["vadiDetailsADMHOME"] = dt;
        BindVadiADMHOMEGrid();
        ClearVadiADMHOMEForm();
    }

    protected void gvVadiADMHOME_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Remove" && e.CommandName != "FinaliseRow")
            return;

        DataTable dt = GetVadiADMHOMETable();
        int index = Convert.ToInt32(e.CommandArgument);
        if (index < 0 || index >= dt.Rows.Count)
            return;

        if (e.CommandName == "FinaliseRow")
        {
            FinaliseClickedRowADMHOME(e, dt, index);
            return;
        }

        // Remove: finalised rows are locked
        if (Convert.ToBoolean(dt.Rows[index]["IsFinalised"]))
        {
            AlertADMHOME("फाइनल किया गया आवेदन हटाया नहीं जा सकता...!");
            return;
        }

        string docKey = Convert.ToString(dt.Rows[index]["DocKey"]);
        if (docKey != "")
            GetVadiADMHOMEDocs().Remove(docKey);

        dt.Rows.RemoveAt(index);
        ViewState["vadiDetailsADMHOME"] = dt;
        BindVadiADMHOMEGrid();
    }

    // row Submit: finalises (saves to DB with a new HDSB application no.) that row, only if its tick box is ticked
    void FinaliseClickedRowADMHOME(GridViewCommandEventArgs e, DataTable dt, int index)
    {
        if (Convert.ToString(Session["Role"]).Trim() != "ADMHOME")
            return;

        // the tick box in the same row as the clicked Submit (checked on the server as well as in the browser)
        GridViewRow gr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        CheckBox chk = gr.FindControl("chkFinaliseADMHOME") as CheckBox;
        if (chk == null || !chk.Checked)
        {
            AlertADMHOME("कृपया पहले इस आवेदन के बॉक्स पर टिक करें...!");
            return;
        }

        string result = FinaliseVadiADMHOMERow(dt.Rows[index]);
        if (result == "NO_DOC")
        {
            AlertADMHOME("इस आवेदन का दस्तावेज़ नहीं मिला (सत्र समाप्त हो गया हो सकता है)। कृपया पंक्ति हटाकर पुनः जोड़ें...!");
            return;
        }
        if (result == "")
        {
            AlertADMHOME("तकनीकी त्रुटि: आवेदन सहेजा नहीं जा सका, कृपया पुनः प्रयास करें...!");
            return;
        }

        ViewState["vadiDetailsADMHOME"] = dt;
        BindVadiADMHOMEGrid();
        AlertADMHOME("फाइल संख्या " + result + " के साथ आवेदन सफलतापूर्वक फाइनल किया गया। यह फाइल View & Forward Application में देखी जा सकती है...!");
    }

    // Saves one temporary row. Returns the new application no., "NO_DOC" if its PDF is no longer
    // in Session, or "" if the database save failed (the row then stays ticked-able for a retry).
    string FinaliseVadiADMHOMERow(DataRow row)
    {
        if (Convert.ToBoolean(row["IsFinalised"]))
            return Convert.ToString(row["FileNo"]);

        string docKey = Convert.ToString(row["DocKey"]);
        byte[] docBytes;
        if (docKey == "" || !GetVadiADMHOMEDocs().TryGetValue(docKey, out docBytes))
            return "NO_DOC";

        string applicationNo = SaveVadiApplicationADMHOME(row, docBytes);
        if (applicationNo == "")
            return "";

        row["IsFinalised"] = true;
        row["FileNo"] = applicationNo;
        GetVadiADMHOMEDocs().Remove(docKey);   // saved in DB now
        return applicationNo;
    }

    void BindVadiADMHOMEGrid()
    {
        gvVadiADMHOME.DataSource = GetVadiADMHOMETable();
        gvVadiADMHOME.DataBind();
    }

    void ClearVadiADMHOMEForm()
    {
        txtNamePerAadhaar.Text = "";
        txtFName.Text = "";
        ddlgender.SelectedIndex = 0;
        ddlUserDist.SelectedIndex = 0;
        ddlUserAreatype.SelectedIndex = 0;
        // reload the dependent lists for "no district" (same binders Page_Load uses)
        BindSubDivision_wadi();
        BindBlock_Wadi();
        BindPolice_wadi();
        BindPanchyat_Wadi();
        BindVillage_Wadi();
        bindward_Wadi();
        txtUserMohalla.Text = "";
        txtvadimobile.Text = "";
        txtVadiPincode.Text = "";
        txtADMHOMERemarks.Text = "";

        // back to the Rural layout (same as ddlUserAreatype_SelectedIndexChanged for non-Urban)
        labUVillage.Text = "ग्राम पंचायत";
        divUserMohalla.Visible = false;
        divUserVillageCol.Visible = true;
    }

    #endregion

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
                         ddlWsanshaanya_naam.SelectedValue.Trim() != "0" ? ddlWsanshaanya_naam.SelectedItem.Text : null);
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


            //string sql = @" select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where sd.DistCode=@District_Code order by sd.Sd_Name_En";
            // thana filter applies only when the login has a thana (SHO); ADMHOME sees all subdivisions of the district
            string sql = @"select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions sd where (@thana_code in ('','0') or Sd_Code2 in (select SubDivCode from Blocks where BlockCode in(select code from MstThanaMapping where thana_code=@thana_code))) and sd.DistCode=@District_Code";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _thana_code = new SqlParameter("@thana_code", Convert.ToString(Session["Thana_Code"]).Trim());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _thana_code });


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


            //string sql = @" select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) order by BlockName";
            // thana filter applies only when the login has a thana (SHO); ADMHOME sees all blocks
            string sql = @"select DISTINCT t.BlockName,t.BlockCode from Blocks t where t.DistCode=@District_Code And (@Subdivision_Code=0 Or t.SubDivCode=@Subdivision_Code) and (@thana_code in ('','0') or BlockCode in (select code from MstThanaMapping where thana_code=@thana_code))  order by BlockName";
            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString());
            SqlParameter _thana_code = new SqlParameter("@thana_code", Convert.ToString(Session["Thana_Code"]).Trim());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode, _thana_code });


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

            SqlParameter _DCode = new SqlParameter("@District_Code", ddlDistrict.SelectedValue.ToString());
            SqlParameter _SCode = new SqlParameter("@Subdivision_Code", ddlSubdivision.SelectedValue.ToString());
            //SqlParameter _RCode = new SqlParameter("@Range_code", ddlrange.SelectedValue.ToString());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _DCode, _SCode });
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
    // true for logins without a fixed thana (e.g. ADMHOME) - they must pick the thana themselves
    bool IsThanaSelectable()
    {
        string thana = Convert.ToString(Session["Thana_Code"]).Trim();
        return thana == "" || thana == "0";
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubDivision();
        BindBlock();
        //BindPolice();
        if (IsThanaSelectable()) BindPolice();
        BindVillage();
        BindPanchyat();
        bindward();
    }
    protected void ddlSubdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock();
        //BindPolice();
        if (IsThanaSelectable()) BindPolice();
        BindVillage();
        BindPanchyat();
        bindward();
        ddlUserAreatype.SelectedIndex = 0;
        ddlareatype.SelectedIndex = 0;
    }
    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindPolice();
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
        if (wadi_grid.Rows.Count == 0)
        {
            Utility.showMessage(this, "कृपया वादी जोड़ें..!");
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
        if (PrativadiDoc.HasFile)
        {
            if (!validateFile(PrativadiDoc, "doc"))
            {
                Utility.showMessage(this, "प्रतिवादी द्वारा प्रस्तुत आवेदन पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे..");
                lblMsg.Text = "(प्रतिवादी द्वारा प्रस्तुत आवेदन पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                flag = false;
            }
            else
            {
                string m = FileUploadValidator.IsPdf(PrativadiDoc.PostedFile, 1024, 1024);
                if (m == "OK")
                {


                }
                else
                {
                    Utility.showMessage(this, "प्रतिवादी द्वारा प्रस्तुत आवेदन पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे..");
                    lblMsg.Text = "(प्रतिवादी द्वारा प्रस्तुत आवेदन पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    flag = false;
                }
            }

        }

        return flag;
    }
    public void MatterRegistration()
    {
        if (validationMatterRegistration() == true)
        {
            string Vadi_sakshya_FilePath = "";
            string Prativadi_sakshya_FilePath = "";
            Guid obj = Guid.NewGuid();
            string id = obj.ToString();
            if (AppDoc.HasFile)
            {

                string m = FileUploadValidator.IsPdf(AppDoc.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    Vadi_sakshya_FilePath = "~/LandDoc/Upload/ID"+ id + "/Vadi_sakshya_File.pdf";

                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    return;
                }
            }

            if (PrativadiDoc.HasFile)
            {

                string m = FileUploadValidator.IsPdf(PrativadiDoc.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    Prativadi_sakshya_FilePath = "~/LandDoc/Upload/ID"+id+"/Prativadi_sakshya_File.pdf";

                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    return;
                }
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
            if (PrativadiDoc.HasFile)
            {
                string p = InsSaveFile("Prativadi_sakshya_File", PrativadiDoc, id);
                string pa = "~/LandDoc/Upload/ID" + id + "/Prativadi_sakshya_File.pdf";
                if (p == "0" || p != pa)
                {
                   
                    lblMsg.Text = "Technical Error";
                    return;
                }
            }



            SqlParameter Range_Code = new SqlParameter("@Range_Code", "");
            SqlParameter Comm_Code = new SqlParameter("@Comm_Code", Session["Commsionary_Code"].ToString());
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
            SqlParameter PrativadiVivarani = new SqlParameter("@PrativadiVivarani", txtPrativadiVivarani.Text.ToString());

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
            DataTable dt = clsData.GetDataTableWithProc("usp_SaveMatterRegisteration", new SqlParameter[]
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
                ,PrativadiVivarani
                ,VadiDetailEntryTable
                ,a_id
                ,Guid_id
            });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {
                    Utility.showMessage(Page, "वादी और भूमि विवाद का विवरण सफलतापूर्वक सहेजा गया"+ message);
                    btnBack.Visible = true;
                    //ViewState["hfGetdateAvadanvalue"] = txtAwadenKiTithi.Text;
                    hfGetdateAvadan.Value = txtAwadenKiTithi.Text;
                    //ViewState["hfGetApplicationID"] = dt.Rows[0]["registration"].ToString();
                    hfGetApplicationno.Value = dt.Rows[0]["registration"].ToString();
                    //ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                    clearFirstStep();

                    ViewState["a_id"] = dt.Rows[0]["id"].ToString();
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
        txtPrativadiVivarani.Text = "";
        wadi_grid.DataSource = vadiDetails;
        wadi_grid.DataBind();
        //lblMsg.Text = "";     
    }

    #endregion

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
                ddlPSubdivision.SelectedValue.Trim() != "0" ? ddlPSubdivision.SelectedValue.Trim() : null,
                ddlPBlock.SelectedValue.Trim() != "0" ? ddlPBlock.SelectedValue.Trim() : null,
                ddlPThana.SelectedValue.Trim() != "0" ? ddlPThana.SelectedValue.Trim() : null,
                ddlPAreatype.SelectedValue.Trim() != "0" ? ddlPAreatype.SelectedValue.Trim() : null,
                ddlPPanchyat.SelectedValue.Trim() != "0" ? ddlPPanchyat.SelectedValue.Trim() : null,
                ddlPVillage.SelectedValue.Trim() != "0" ? ddlPVillage.SelectedValue.Trim() : null,
                ddlPWard.SelectedValue.Trim() != "0" ? ddlPWard.SelectedValue.Trim() : null,
                txtprativadi_Mobile.Text.Trim(),
                txtPPanchyat_Anya.Text.Trim(),
                txtPVillage_Anya.Text.Trim(),
                txtPWard_Anya.Text.Trim(),
                txtPMohalla.Text.Trim(),
                ddlPsanshaanya_naam.SelectedValue.Trim() != "0" ? ddlPsanshaanya_naam.SelectedValue.Trim() : null,
                // ddlPvibhaag_naam.SelectedValue.Trim() == "Y" ? ddlPvibhaag_naam.SelectedItem.Text.Trim(): ddl_is_pratiVadi_from_an_org.SelectedValue.Trim() == "Y" ? ddlPsanstha_naam.SelectedItem.Text.Trim() : "",

                ddl_is_pratiVadi_from_an_dept.SelectedValue == "Y" ? ddlPvibhaag_naam.SelectedItem.Text : ddl_is_pratiVadi_from_an_org.SelectedValue == "Y" ? ddlPsanstha_naam.SelectedItem.Text : "",



                ddlPDistrict.SelectedValue.Trim() != "0" ? ddlPDistrict.SelectedItem.Text : "",
                ddlPSubdivision.SelectedValue.Trim() != "0" ? ddlPSubdivision.SelectedItem.Text : "",
                ddlPWard.SelectedValue.Trim() != "0" ? ddlPBlock.SelectedItem.Text : "",
                ddlPThana.SelectedValue.Trim() != "0" ? ddlPThana.SelectedItem.Text : "",
                ddlPAreatype.SelectedValue.Trim() != "0" ? ddlPAreatype.SelectedItem.Text : "",
                ddlPPanchyat.SelectedValue.Trim() != "0" ? ddlPPanchyat.SelectedItem.Text : "",
                ddlPVillage.SelectedValue.Trim() != "0" ? ddlPVillage.SelectedItem.Text : "",
                ddlPWard.SelectedValue.Trim() != "0" ? ddlPWard.SelectedItem.Text : "",
                ddlPsanshaanya_naam.SelectedValue.Trim() != "0" ? ddlPsanshaanya_naam.SelectedItem.Text : "");
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
            ddlPSubdivision.SelectedIndex = 0;
            ddlPBlock.SelectedIndex = 0;
            ddlPThana.SelectedIndex = 0;
            ddlPAreatype.SelectedIndex = 0;
            ddlPPanchyat.SelectedIndex = 0;
            ddlPVillage.SelectedIndex = 0;
            ddlPWard.SelectedIndex = 0;
            txtprativadi_Mobile.Text = "";
        }
    }
    public bool validationPrativadiNVivaran()
    {
        bool flag = true;
        if (pratiWadi_grid.Rows.Count == 0)
        {
            Utility.showMessage(this, "कृपया प्रतिवादी जोड़ें..!");
            flag = false;
            btnAddPratiVadiDetail.Focus();
        }
        if (ddlwadi_pratiwadi_sunwai.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया प्रतिवादी को सूचित किया गया है या नहीं चुनें...!");
            ddlwadi_pratiwadi_sunwai.Focus();
            flag = false;
        }
        if (ddlKiskeduwara_bhejagaya.Visible == true && ddlKiskeduwara_bhejagaya.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया प्रतिवादी के सुचना का माध्यम चुनें...!");
            ddlKiskeduwara_bhejagaya.Focus();
            flag = false;
        }
        if (txtsunwaiHetuNoticKaKaran.Visible == true && txtsunwaiHetuNoticKaKaran.Text.Trim() == "")
        {
            Utility.showMessage(this, "कृपया कारण स्पष्ट करें...!");
            txtsunwaiHetuNoticKaKaran.Focus();
            flag = false;
        }

        if (ddlSuchana_ka_tamila.Visible == true && ddlSuchana_ka_tamila.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया प्रतिवादी को सूचना का तामिला प्राप्त है या नहीं चुनें...!");
            ddlSuchana_ka_tamila.Focus();
            flag = false;
        }
        if (ddlSuchana_ka_upasthiti.Visible == true && ddlSuchana_ka_upasthiti.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया प्रतिवादी उपस्थित हुआ है या नहीं चुनें...!");
            ddlSuchana_ka_upasthiti.Focus();
            flag = false;
        }
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
            DataTable dt = clsData.GetDataTableWithProc("usp_SavePrativadiNAnyvivran", new SqlParameter[]
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
                    Utility.showMessage(Page, "प्रतिवादी और अन्य विवरण सफलतापूर्वक सहेजा गया");
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
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
            DataTable dt = clsData.GetDataTableWithProc("SP_GetKhatiyan_Type", new SqlParameter[] { });
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

    protected void btnsaveBhumiKaVivaran_Click(object sender, EventArgs e)
    {
        if (ValidateBhumiKaVivaran())
        {
            DataTable dt = (DataTable)ViewState["BhumiKaViVaranDT"];
            dt.Rows.Add(
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
                txtrakabasankhya.Text.Trim() + " " + ddlrakabasankhya.SelectedItem.ToString() + ", " + txtrakabasankhya1.Text.Trim() + " " + ddlrakabasankhya1.SelectedItem.ToString() + ", " + txtrakabasankhya2.Text.Trim() + " " + ddlrakabasankhya2.SelectedItem.ToString());
            ViewState["BhumiKaViVaranDT"] = dt;
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

        return dt;
    }
    protected void BindGridhumiKaVivaran()
    {

        try
        {
            grd_bhumivivad.DataSource = (DataTable)ViewState["BhumiKaViVaranDT"];
            grd_bhumivivad.DataBind();
        }
        catch { }




    }
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
        DataTable BhumiKaViVaranDT1 = (DataTable)ViewState["BhumiKaViVaranDT"];
        DataTable BhumiKaViVaranDT = BhumiKaViVaranDT1.Copy();
        try
        {
            if (BhumiKaViVaranDT.Rows.Count == 0)
            {
                Utility.showMessage(this, "कृपया खाता खेसरा का विवरण अंकित करें...!");
                return;
            }

            BhumiKaViVaranDT.Columns.Remove("LandTypesInKhatianDesc");
            BhumiKaViVaranDT.Columns.Remove("Rakba");
        }
        catch { }
        SqlParameter a_id = new SqlParameter("@a_id", Convert.ToInt64(ViewState["a_id"]));
        SqlParameter LandDetailsEntryTable = new SqlParameter("@LandDetailsEntryTable", BhumiKaViVaranDT);
        DataTable dt = clsData.GetDataTableWithProc("usp_SaveBhumiKaKhataKhesraKaVivaran", new SqlParameter[]
            {
                a_id
                ,LandDetailsEntryTable

            });

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
            {

                //lblMsg.Text = "आवेदनकर्ता का विवरण सफलतापूर्वक सहेजा गया";
                Utility.showMessage(Page, "भूमि का खाता खेसरा का विवरण सफलतापूर्वक सहेजा गया");
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                ClearPrativadiNAnyaVivaran();
                ViewState["a_id"] = dt.Rows[0]["id"].ToString();
                ViewState["Step"] = 4;
                GetVadiPrativadiEvidanceDocument();
                step3.Visible = false;
                step4.Visible = true;
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

    #region Vadi prativadi Evidance Document

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
        if (fileupload != "0" && fileupload!="" && fileupload== pathcheck)
        {
            dt.Rows.Add(ddlVadiEvidenceType.SelectedValue.Trim(), ddlVadiEvidenceType.SelectedValue.Trim() != "9" ? ddlVadiEvidenceType.SelectedItem.Text.Trim() : txtVadiEvidenceType.Text, txtVadiEvidenceType.Text, _Vadi_sakshya_File);
            ViewState["VadiEvidenceDetailsDT"] = dt;
            BindGridVadiPrativadiEvidence();

            txtVadiEvidenceType.Text = "";
            ddlVadiEvidenceType.SelectedIndex = 0;
            file_vadi_dastavej_new.Dispose();
        }
        else if(fileupload == "")
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

        try
        {
            gdPrativadiEvidence.DataSource = (DataTable)ViewState["PrativadiEvidenceDetailsDT"];
            gdPrativadiEvidence.DataBind();
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
    protected void bindLandEvidence()//   LandEvidence
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

                    ddlPrativadiEvidenceType.DataSource = dt;
                    ddlPrativadiEvidenceType.DataTextField = "name";
                    ddlPrativadiEvidenceType.DataValueField = "id";
                    ddlPrativadiEvidenceType.DataBind();
                    ddlPrativadiEvidenceType.Items.Insert(0, new ListItem("--Select--", "0"));

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
    protected void ddlPrativadiEvidenceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divddlPrativadiEvidenceType.Attributes.Add("class", "col-md-6");
        divtxtPrativadiEvidenceType.Visible = false;
        if (ddlPrativadiEvidenceType.SelectedValue == "9")
        {
            //divddlPrativadiEvidenceType.Attributes.Add("class", "col-md-3");
            divtxtPrativadiEvidenceType.Visible = true;
        }
    }



    public bool ValidatePrativadiEvidenceDetail()
    {
        // Validate "Is Pvadi Evidence Available?"
        string ddlIsPvadiEvi1 = ddlIsPvadiEvi.SelectedValue.Trim();
        if (ddlIsPvadiEvi1 == "0")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ? चुनें...!');", true);
            ddlIsPvadiEvi.Focus();
            return false;
        }

        // Validate Evidence Type Selection
        if (ddlPrativadiEvidenceType.SelectedIndex == 0 && ddlIsPvadiEvi1 == "Y")
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया साक्ष्य का प्रकार चुनें...!');", true);
            ddlPrativadiEvidenceType.Focus();
            return false;
        }

        // Validate if Other Evidence Type is specified
        if (ddlPrativadiEvidenceType.SelectedIndex == 9 && ddlIsPvadiEvi1 == "Y")
        {
            if (txtPrativadiEvidenceType != null)
            {
                if (string.IsNullOrWhiteSpace(txtPrativadiEvidenceType.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('कृपया अन्य साक्ष्य का प्रकार अंकित करें...!');", true);
                    txtPrativadiEvidenceType.Focus();
                    return false;
                }
            }
        }

        // Validate File Upload
        HttpPostedFile uploadedFile = file_Prativadi_dastavej_new.PostedFile;
        if (uploadedFile != null && uploadedFile.FileName != "")
        {
            string fileExtension = Path.GetExtension(uploadedFile.FileName).ToLower();
            string[] validExtensions = { ".pdf" }; // Assuming valid files are PDFs only
            if (!validExtensions.Contains(fileExtension))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please Upload Document in Pdf File |');", true);
                file_Prativadi_dastavej_new.Focus();
                return false;
            }

            // Validate File Size (max 3 MB)
            if (uploadedFile.ContentLength > (3 * 1024 * 1024))  // File size limit: 3 MB
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('File size must be less than or equal to 3 MB |');", true);
                file_Prativadi_dastavej_new.Focus();
                return false;
            }
        }

        // All validations passed
        return true;
    }

    protected void btnAddPrativadiEvidence_Click(object sender, EventArgs e)
    {
        if (ValidatePrativadiEvidenceDetail())
        {
            DataTable dt = (DataTable)ViewState["PrativadiEvidenceDetailsDT"];

            string _Prativadi_sakshya_File = string.Empty;



            string fileupload = "";
            string a_id = ViewState["a_id"].ToString();
            if (file_Prativadi_dastavej_new.HasFile)
            {

                if (file_Prativadi_dastavej_new.HasFile)
                {
                    if (!validateFile(file_Prativadi_dastavej_new, "doc"))
                        return;
                    string m = FileUploadValidator.IsPdf(file_Prativadi_dastavej_new.PostedFile, 1024, 1024);
                    if (m == "OK")
                    {
                        _Prativadi_sakshya_File = "~/LandDoc/Upload/PrativadiEvidence" + a_id + "/PrativadiEvidence" + (dt.Rows.Count + 1) + ".pdf";
                        string path = "~/LandDoc/Upload/PrativadiEvidence" + a_id + "/";
                        string v = InsSaveFile("PrativadiEvidence" + (dt.Rows.Count + 1), file_Prativadi_dastavej_new, a_id, path);
                        fileupload = v;
                    }
                    else
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return;
                    }
                }
            }

            string pathcheck = "~/LandDoc/Upload/PrativadiEvidence" + a_id + "/PrativadiEvidence" + (dt.Rows.Count + 1) + ".pdf";
            if (fileupload != "0" && fileupload != "" && fileupload == pathcheck)
            {
                dt.Rows.Add(ddlPrativadiEvidenceType.SelectedValue.Trim(), ddlPrativadiEvidenceType.SelectedValue.Trim() != "9" ? ddlPrativadiEvidenceType.SelectedItem.Text.Trim() : txtPrativadiEvidenceType.Text, txtPrativadiEvidenceType.Text, _Prativadi_sakshya_File);
                ViewState["PrativadiEvidenceDetailsDT"] = dt;
                BindGridVadiPrativadiEvidence();

                txtPrativadiEvidenceType.Text = "";
                ddlPrativadiEvidenceType.SelectedIndex = 0;
                file_Prativadi_dastavej_new.Dispose();
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
    protected void gdPrativadiEvidence_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            string rowIndex = e.CommandArgument.ToString();
            DataTable dt = (DataTable)ViewState["PrativadiEvidenceDetailsDT"];
            dt.Rows.RemoveAt(Convert.ToInt32(rowIndex));
            ViewState["PrativadiEvidenceDetailsDT"] = dt;
            BindGridVadiPrativadiEvidence();


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
    protected void gdPrativadiEvidence_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton('" + image1.ClientID + "')");


        }
    }
    public void VadiPrativadiEvidanceDocument()
    {
        DataTable VadiEvidenceDetailsDT1 = (DataTable)ViewState["VadiEvidenceDetailsDT"];
        DataTable VadiEvidenceDetailsDT = VadiEvidenceDetailsDT1.Copy();
        try
        {
            VadiEvidenceDetailsDT.Columns.Remove("evidence_name");
        }
        catch { }
        DataTable PrativadiEvidenceDetailsDT1 = (DataTable)ViewState["PrativadiEvidenceDetailsDT"];
        DataTable PrativadiEvidenceDetailsDT = PrativadiEvidenceDetailsDT1.Copy();
        try
        {
            PrativadiEvidenceDetailsDT.Columns.Remove("evidence_name");
        }
        catch { }
        SqlParameter VadiEvidenceEntryTable = new SqlParameter("@VadiEvidenceEntryTable", VadiEvidenceDetailsDT);
        SqlParameter PrativadiEvidenceEntryTable = new SqlParameter("@PrativadiEvidenceEntryTable", PrativadiEvidenceDetailsDT);

        SqlParameter a_id = new SqlParameter("@a_id", Convert.ToInt64(ViewState["a_id"]));

        DataTable dt = clsData.GetDataTableWithProc("usp_SaveVadiPrativadiEvidanceDocument", new SqlParameter[]
            {
                a_id
                ,VadiEvidenceEntryTable
                ,PrativadiEvidenceEntryTable

            });

        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
            {
                //lblMsg.Text = "वादी और प्रतिवादी द्वारा प्रस्तुत साक्ष्य की विवरणी सफलतापूर्वक सहेजा गया";
                Utility.showMessage(Page, "वादी और प्रतिवादी द्वारा प्रस्तुत साक्ष्य की विवरणी सफलतापूर्वक सहेजा गया");
                ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                ViewState["a_id"] = dt.Rows[0]["id"].ToString();
                GetsavePrastuthSakchaKiVivarni();
                ViewState["Step"] = 5;
                step4.Visible = false;
                step5.Visible = true;
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

    #region प्रस्तुत साक्ष्य की विवरणी

    protected void ddlbhukhand_mapi_SelectedIndexChanged(object sender, EventArgs e)
    {
        //divbhukhand_mapi.Attributes.Add("class", "col-md-9");
        divbhukhand_Copy.Visible = false;
        ddlbhukhand_Copy.SelectedIndex = 0;
        ddlbhukhand_Copy_SelectedIndexChanged(sender, e);

        if (ddlbhukhand_mapi.SelectedIndex == 1)
        {
            //divbhukhand_mapi.Attributes.Add("class", "col-md-6");
            divbhukhand_Copy.Visible = true;
            ddlbhukhand_Copy.SelectedIndex = 0;
        }
    }
    protected void ddlbhukhand_Copy_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtMapiKeNirdharit_tithi.Text = "";
        txtbhukhand_reason.Text = "";

        if (ddlbhukhand_Copy.SelectedValue == "Y")
        {
            lblbhukand_prativedan.Visible = true;
            lblbhukand_karan.Visible = false;
            file_bhukand_prativedan.Visible = true;
            Label15.Visible = true;
            txtbhukhand_reason.Visible = false;
            divMapiKeNirdharit_tithi.Visible = false;
        }
        else if (ddlbhukhand_Copy.SelectedValue == "N")
        {
            lblbhukand_prativedan.Visible = false;
            lblbhukand_karan.Visible = true;
            file_bhukand_prativedan.Visible = false;
            Label15.Visible = false;
            txtbhukhand_reason.Visible = true;
            divMapiKeNirdharit_tithi.Visible = true;
        }
        else
        {
            lblbhukand_prativedan.Visible = false;
            lblbhukand_karan.Visible = false;
            file_bhukand_prativedan.Visible = false;
            Label15.Visible = false;
            txtbhukhand_reason.Visible = false;
            divMapiKeNirdharit_tithi.Visible = false;
        }
    }
    public bool validationPrastuthSakchaKiVivarni()
    {
        bool flag = true;
        if (ddlbhukhand_mapi.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया विवादित भू-खंड की मापी चुनें...!");
            ddlbhukhand_mapi.Focus();
            flag = false;
        }


        if (ddlbhukhand_mapi.SelectedIndex == 1 && ddlbhukhand_Copy.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया विवादित भू-खंड की मापी चुनें...!");
            ddlbhukhand_Copy.Focus();
            flag = false;
        }

        if (ddlbhukhand_mapi.SelectedIndex == 1 && ddlbhukhand_Copy.SelectedIndex == 2)
        {
            if (txtMapiKeNirdharit_tithi.Text.Trim() == "")
            {
                Utility.showMessage(this, "कृपया मापी के लिए निर्धारित तिथि अंकित करें...!");
                txtMapiKeNirdharit_tithi.Focus();
                flag = false;
            }

        }
        return flag;
    }
    public void ClearPrastuthSakchaKiVivarni()
    {
        txtpulis_padadhikari_vivarani.Text = "";

        txthalkakarmchari_prativedan.Text = "";

        ddlbhukhand_mapi.SelectedIndex = 0;
        ddlbhukhand_Copy.SelectedIndex = 0;
        txtMapiKeNirdharit_tithi.Text = "";
        txtbhukhand_reason.Text = "";
        pulis_padadhikari_Patr_file.Dispose();
        file_halkakarmchari_praptr.Dispose();
        file_bhukand_prativedan.Dispose();
    }
    public void savePrastuthSakchaKiVivarni()
    {
        if (validationPrastuthSakchaKiVivarni() == true)
        {
            string _pulis_padadhikar_Patr_file = string.Empty;
            string _HalkaKarmchari_Patr_file = string.Empty;
            string _vivaadit_bhukhand_Mapi_File = string.Empty;
            if (pulis_padadhikari_Patr_file.HasFile)
            {
                if (!validateFile(pulis_padadhikari_Patr_file, "doc"))
                    return;
                string m = FileUploadValidator.IsPdf(pulis_padadhikari_Patr_file.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    _pulis_padadhikar_Patr_file = "~/LandDoc/Upload/PulisPadadhikariPatr#a_id#/PulisPadadhikariPatr.pdf";

                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    return;
                }
            }


            if (file_halkakarmchari_praptr.HasFile)
            {
                if (!validateFile(file_halkakarmchari_praptr, "doc"))
                    return;
                string m = FileUploadValidator.IsPdf(file_halkakarmchari_praptr.PostedFile, 1024, 1024);
                if (m == "OK")
                {
                    _HalkaKarmchari_Patr_file = "~/LandDoc/Upload/FileHalkakarmchariPraptr#a_id#/FileHalkakarmchariPraptr.pdf";

                }
                else
                {
                    lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                    return;
                }
            }

            if (ddlbhukhand_Copy.SelectedValue == "Y")
            {


                if (file_bhukand_prativedan.HasFile)
                {
                    if (!validateFile(file_bhukand_prativedan, "doc"))
                        return;
                    string m = FileUploadValidator.IsPdf(file_bhukand_prativedan.PostedFile, 1024, 1024);
                    if (m == "OK")
                    {
                        _vivaadit_bhukhand_Mapi_File = "~/LandDoc/Upload/BhukhandPrativedanPatra#a_id#/BhukhandPrativedanPatra.pdf";

                    }
                    else
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return;
                    }
                }


            }

            string message = "";
            if (pulis_padadhikari_Patr_file.HasFile)
            {
                string path = "~/LandDoc/Upload/PulisPadadhikariPatr" + Convert.ToInt64(ViewState["a_id"]) + "/";
                string v = InsSaveFile("PulisPadadhikariPatr", pulis_padadhikari_Patr_file, Convert.ToInt64(ViewState["a_id"]).ToString(), path);
                string pathcheck = "~/LandDoc/Upload/PulisPadadhikariPatr" + Convert.ToInt64(ViewState["a_id"]).ToString() + "/PulisPadadhikariPatr.pdf";
                if (v == "0" || v!= pathcheck)
                {
                    _pulis_padadhikar_Patr_file = "";
                    message = " PulisPadadhikariPatr not uplaad";
                    lblMsg.Text = "Technical Error";
                    return;
                }
            }

            if (file_halkakarmchari_praptr.HasFile)
            {

                string path = "~/LandDoc/Upload/FileHalkakarmchariPraptr" + Convert.ToInt64(ViewState["a_id"]) + "/";
                string v = InsSaveFile("FileHalkakarmchariPraptr", file_halkakarmchari_praptr, Convert.ToInt64(ViewState["a_id"]).ToString(), path);
                string pathcheck = "~/LandDoc/Upload/FileHalkakarmchariPraptr" + Convert.ToInt64(ViewState["a_id"]) + "/FileHalkakarmchariPraptr.pdf";
                if (v == "0" || v!=pathcheck)
                {
                    _HalkaKarmchari_Patr_file = "";
                    message = message + " ;FileHalkakarmchariPraptr not uplaad";
                    lblMsg.Text = "Technical Error";
                    return;
                }
            }
            if (ddlbhukhand_Copy.SelectedValue == "Y")
            {
                if (file_bhukand_prativedan.HasFile)
                {
                    string path = "~/LandDoc/Upload/BhukhandPrativedanPatra" + Convert.ToInt64(ViewState["a_id"]) + "/";
                    string v = InsSaveFile("BhukhandPrativedanPatra", file_bhukand_prativedan, Convert.ToInt64(ViewState["a_id"]).ToString(), path);
                    string pathcheck = "~/LandDoc/Upload/BhukhandPrativedanPatra" + Convert.ToInt64(ViewState["a_id"]) + "/BhukhandPrativedanPatra.pdf";
                    if (v == "0" || v != pathcheck)
                    {
                        _vivaadit_bhukhand_Mapi_File = "";
                        message = message + " ;BhukhandPrativedanPatra not uplaad";
                        lblMsg.Text = "Technical Error";
                        return;
                    }
                }
            }


            SqlParameter pulis_padadhikari_vivarani = new SqlParameter("@pulis_padadhikari_vivarani", txtpulis_padadhikari_vivarani.Text.Trim());
            SqlParameter pulis_padadhikar_Patr_file = new SqlParameter("@pulis_padadhikar_Patr_file", _pulis_padadhikar_Patr_file);
            SqlParameter HalkaKarmchari_vivran = new SqlParameter("@HalkaKarmchari_vivran", txthalkakarmchari_prativedan.Text.Trim());
            SqlParameter HalkaKarmchari_Patr_file = new SqlParameter("@HalkaKarmchari_Patr_file", _HalkaKarmchari_Patr_file.ToString().Trim());


            SqlParameter vivadit_bhukhand_Mapi_ki_avashyakta_hai = new SqlParameter("@vivadit_bhukhand_Mapi_ki_avashyakta_hai", ddlbhukhand_mapi.SelectedValue.Trim());
            SqlParameter vivadit_bhukhand_Mapi = new SqlParameter("@vivadit_bhukhand_Mapi", ddlbhukhand_Copy.SelectedValue.Trim());
            SqlParameter maapee_ke_lie_nirdhaarit_tithi = new SqlParameter("@maapee_ke_lie_nirdhaarit_tithi", txtMapiKeNirdharit_tithi.Text.Trim() != "" ? txtMapiKeNirdharit_tithi.Text.Trim() : "01-01-1900");



            SqlParameter vivaadit_bhukhand_Mapi_File = new SqlParameter("@vivaadit_bhukhand_Mapi_File", _vivaadit_bhukhand_Mapi_File.ToString().Trim());
            SqlParameter vivaadit_bhukhand_Mapi_Reason = new SqlParameter("@vivaadit_bhukhand_Mapi_Reason", txtbhukhand_reason.Text.Trim());
            SqlParameter a_id = new SqlParameter("@a_id", Convert.ToInt64(ViewState["a_id"]));
            DataTable dt = clsData.GetDataTableWithProc("usp_PrastutSakchaKiVivarni", new SqlParameter[]
                    {
                a_id
                ,pulis_padadhikari_vivarani
                ,pulis_padadhikar_Patr_file
                ,HalkaKarmchari_vivran
                ,HalkaKarmchari_Patr_file
                ,vivadit_bhukhand_Mapi_ki_avashyakta_hai
                ,vivadit_bhukhand_Mapi
                ,maapee_ke_lie_nirdhaarit_tithi
                ,vivaadit_bhukhand_Mapi_File
                ,vivaadit_bhukhand_Mapi_Reason
                    });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {
                    //lblMsg.Text = "प्रस्तुत साक्ष्य की विवरणी  सफलतापूर्वक सहेजा गया";
                    Utility.showMessage(Page, "प्रस्तुत साक्ष्य की विवरणी  सफलतापूर्वक सहेजा गया"+message);
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");

                    ViewState["a_id"] = dt.Rows[0]["id"].ToString();
                    ViewState["Step"] = 6;

                    //if (pulis_padadhikari_Patr_file.HasFile)
                    //{
                    //    string path = "~/LandDoc/Upload/PulisPadadhikariPatr" + dt.Rows[0]["id"].ToString() + "/";
                    //    string v = InsSaveFile("PulisPadadhikariPatr", pulis_padadhikari_Patr_file, dt.Rows[0]["id"].ToString(), path);
                    //}

                    //if (file_halkakarmchari_praptr.HasFile)
                    //{

                    //    string path = "~/LandDoc/Upload/FileHalkakarmchariPraptr" + dt.Rows[0]["id"].ToString() + "/";
                    //    string v = InsSaveFile("FileHalkakarmchariPraptr", file_halkakarmchari_praptr, dt.Rows[0]["id"].ToString(), path);
                    //}
                    //if (ddlbhukhand_Copy.SelectedValue == "Y")
                    //{
                    //    if (file_bhukand_prativedan.HasFile)
                    //    {
                    //        string path = "~/LandDoc/Upload/BhukhandPrativedanPatra" + dt.Rows[0]["id"].ToString() + "/";
                    //        string v = InsSaveFile("BhukhandPrativedanPatra", file_bhukand_prativedan, dt.Rows[0]["id"].ToString(), path);
                    //    }
                    //}



                    GetBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
                    step5.Visible = false;
                    step6.Visible = true;
                    ClearPrastuthSakchaKiVivarni();
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

    #region 1)भूमि विवाद सें संबंधित घटना/ वारदात का विवरण 2)न्यायालय में प्रक्रियाधीन वाद का विवरण

    //private DataTable DetailsOfIncidentDT()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Clear();
    //    dt.Columns.Add("Ghatna_Vardat_date", typeof(string));
    //    dt.Columns.Add("Ghatna_Short_vivran", typeof(string));
    //    dt.Columns.Add("is_FIR_registered", typeof(string));
    //    dt.Columns.Add("praathamiki_sankhya", typeof(string));
    //    dt.Columns.Add("praathamiki_ka_vivaran", typeof(string));
    //    dt.Columns.Add("is_complaint_filed", typeof(string));
    //    dt.Columns.Add("dhaara", typeof(string));
    //    dt.Columns.Add("apraathamiki_sankhya", typeof(string));
    //    dt.Columns.Add("apraathamiki_ka_vivaran", typeof(string));
    //    dt.Columns.Add("Abhiyukt", typeof(string));

    //    dt.Columns.Add("is_Sanha_recorded", typeof(string));
    //    dt.Columns.Add("sanha_sankhya", typeof(string));

    //    return dt;
    //}
    private DataTable DetailsOfIncidentDT()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("Ghatna_Vardat_date", typeof(string));
        dt.Columns.Add("Ghatna_Short_vivran", typeof(string));
        dt.Columns.Add("is_FIR_registered", typeof(string));
        dt.Columns.Add("praathamiki_sankhya", typeof(string));
        dt.Columns.Add("praathamiki_ka_vivaran", typeof(string));
        dt.Columns.Add("is_complaint_filed", typeof(string));
        dt.Columns.Add("dhaara", typeof(string));
        dt.Columns.Add("apraathamiki_sankhya", typeof(string));
        dt.Columns.Add("apraathamiki_ka_vivaran", typeof(string));
        dt.Columns.Add("Abhiyukt", typeof(string));

        dt.Columns.Add("is_Sanha_recorded", typeof(string));
        dt.Columns.Add("sanha_sankhya", typeof(string));
        dt.Columns.Add("bnm", typeof(string));
        dt.Columns.Add("newdhara", typeof(string));
        dt.Columns.Add("bnm1", typeof(string));
        dt.Columns.Add("newdhara1", typeof(string));

        return dt;
    }
    private DataTable CourtDisputeDetailsDT()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add("courtID", typeof(string));
        dt.Columns.Add("courtTypeID", typeof(string));
        dt.Columns.Add("District_Code", typeof(string));
        dt.Columns.Add("Sub_DivCode", typeof(string));
        dt.Columns.Add("Vibhag_code", typeof(string));
        dt.Columns.Add("vaadi_ki_vaad_sankhya_varsh", typeof(string));
        dt.Columns.Add("vadi_name", typeof(string));
        dt.Columns.Add("prativadi_name", typeof(string));
        dt.Columns.Add("vaad_ki_addhatan_sthiti_vivaran", typeof(string));

        dt.Columns.Add("court", typeof(string));
        dt.Columns.Add("courtType", typeof(string));
        dt.Columns.Add("Dst", typeof(string));
        dt.Columns.Add("SubDiv", typeof(string));
        dt.Columns.Add("Vibhag", typeof(string));
        return dt;
    }
    protected void dd_IsBhumiVivad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dd_IsBhumiVivad.SelectedValue == "Y")
        {
            btnbhumivivad.Visible = true;
            btnBhumiVivadVivran1.Visible = true;
            btnBhumiVivadVivran2.Visible = true;
            btnBhumiVivadVivran3.Visible = true;
            btnBhumiVivadVivran4.Visible = true;
            btnBhumiVivadVivran5.Visible = true;
            btnBhumiVivadVivran6.Visible = true;
            ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
            //this.BindGrid();
        }
        else
        {
            btnBhumiVivadVivran1.Visible = false;
            btnBhumiVivadVivran2.Visible = false;
            btnBhumiVivadVivran3.Visible = false;
            btnBhumiVivadVivran4.Visible = false;
            btnBhumiVivadVivran5.Visible = false;
            btnBhumiVivadVivran6.Visible = false;
            btnbhumivivad.Visible = false;
            ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();
            //this.BindGrid();
        }

    }
    protected void ddlPrathmiki_huyee_hai_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFIR_sankhya.Text = "";
        txtPrathmik_vivran.Text = "";
        if (ddlPrathmiki_huyee_hai.SelectedIndex == 1)
        {
            divPrathmiki_sankhiyan.Visible = true;
            divPrathmiki_vivaran.Visible = true;
        }
        else
        {
            divPrathmiki_sankhiyan.Visible = false;
            divPrathmiki_vivaran.Visible = false;
        }
    }
    protected void ddlAprathmiki_huyee_hai_SelectedIndexChanged(object sender, EventArgs e)
    {
        //chk107.Checked = false;
        //chk109.Checked = false;
        //chk110.Checked = false;
        //chk113.Checked = false;
        //chk116.Checked = false;
        //chk133.Checked = false;
        //chk144.Checked = false;
        //chk145.Checked = false;
        //chk147.Checked = false;
        //txtAFIR_sankhya.Text = "";
        //txtAprathmik_vivran.Text = "";

        //if (ddlAprathmiki_huyee_hai.SelectedIndex == 1)
        //{
        //    divDhara.Visible = true;
        //    divAPrathmiki_sankhiyan.Visible = true;
        //    divAPrathmiki_vivaran.Visible = true;
        //}
        //else
        //{
        //    divAPrathmiki_sankhiyan.Visible = false;
        //    divAPrathmiki_vivaran.Visible = false;
        //    divDhara.Visible = false;
        //}
        chk107.Checked = false;
        chk109.Checked = false;
        chk110.Checked = false;
        chk113.Checked = false;
        chk116.Checked = false;
        chk133.Checked = false;
        chk144.Checked = false;
        chk145.Checked = false;
        chk147.Checked = false;
        txtAFIR_sankhya.Text = "";
        txtAprathmik_vivran.Text = "";

        if (ddlAprathmiki_huyee_hai.SelectedIndex == 1)
        {
            Bindbsndhara();
            divdharabsn.Visible = true;
            //divDhara.Visible = true;
            divAPrathmiki_sankhiyan.Visible = true;
            divAPrathmiki_vivaran.Visible = true;
            rdoOld.Checked = false;
            rdoNew.Checked = false;
            txtbnm.Text = "";
            txtdhara.Text = "";
            ddldhara1.ClearSelection();
            ddlbsn_dhara_hai.ClearSelection();

            //divbsn.Visible = true;
            //divdhara1.Visible = true;

            //checkboxolddhara
            chk107.Checked = false;
            chk109.Checked = false;
            chk110.Checked = false;
            chk113.Checked = false;
            chk116.Checked = false;
            chk133.Checked = false;
            chk144.Checked = false;
            chk145.Checked = false;
            chk147.Checked = false;
            //endcheckboxolddhara
        }
        else
        {
            divAPrathmiki_sankhiyan.Visible = false;
            divAPrathmiki_vivaran.Visible = false;
            divdharabsn.Visible = false;
            divDhara.Visible = false;
            divbsn.Visible = false;
            divdhara1.Visible = false;
            rdoOld.Checked = false;
            rdoNew.Checked = false;
            txtbnm.Text = "";
            txtdhara.Text = "";
            ddldhara1.ClearSelection();
            ddlbsn_dhara_hai.ClearSelection();
            //checkboxolddhara
            chk107.Checked = false;
            chk109.Checked = false;
            chk110.Checked = false;
            chk113.Checked = false;
            chk116.Checked = false;
            chk133.Checked = false;
            chk144.Checked = false;
            chk145.Checked = false;
            chk147.Checked = false;
            //endcheckboxolddhara

        }
    }
    void Bindbsndhara()
    {
        try
        {
            string sql = @"select ID, BNS_Sec from Bns_dhara";
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { });

            ddlbsn_dhara_hai.DataSource = dt;
            ddlbsn_dhara_hai.DataTextField = "BNS_Sec";
            ddlbsn_dhara_hai.DataValueField = "ID";
            ddlbsn_dhara_hai.DataBind();

            dt.Dispose();
        }
        catch
        {
        }
    }
    protected void DharaChanged(object sender, EventArgs e)
    {
        if (rdoOld.Checked)
        {
            divDhara.Visible = true;
            divbsn.Visible = false;
            divdhara1.Visible = false;
            div_tbnm.Visible = false;
            div_tdhara.Visible = false;
        }
        else if (rdoNew.Checked)
        {

            divDhara.Visible = false;
            divbsn.Visible = true;
            divdhara1.Visible = true;
        }
    }

    //protected void ddlbsn_dhara_hai_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    var selectedItems = ddlbsn_dhara_hai.Items
    //                        .Cast<ListItem>()
    //                        .Where(i => i.Selected)
    //                        .ToList();

    //    bool contains41 = selectedItems.Any(i => i.Value == "41");
    //    int selectedCount = selectedItems.Count;

    //    // CASE 1 → Only 41 selected
    //    if (contains41 && selectedCount == 1)
    //    {
    //        div_tbnm.Visible = true;
    //        div_tdhara.Visible = true;
    //        divdhara1.Visible = false;
    //    }
    //    // CASE 2 → 41 + other IDs selected
    //    else if (contains41 && selectedCount > 1)
    //    {
    //        div_tbnm.Visible = true;
    //        div_tdhara.Visible = true;
    //        divdhara1.Visible = true;
    //    }
    //    // CASE 3 → 41 not selected
    //    else
    //    {
    //        div_tbnm.Visible = false;
    //        div_tdhara.Visible = false;
    //        divdhara1.Visible = true;
    //    }

    //    Binddhara();
    //}

    protected void ddlbsn_dhara_hai_SelectedIndexChanged(object sender, EventArgs e)
    {
        // 🔥 STEP 0: SAVE selected IPC values (HiddenField priority)
        List<string> selectedIPC = new List<string>();

        if (!string.IsNullOrEmpty(hdnSelectedIPC.Value))
        {
            selectedIPC = hdnSelectedIPC.Value.Split(',').ToList();
        }
        else
        {
            foreach (ListItem item in ddldhara1.Items)
            {
                if (item.Selected)
                {
                    selectedIPC.Add(item.Value);
                }
            }
        }

        ViewState["SelectedIPC"] = selectedIPC;

        // 🔹 STEP 1: Get selected BNS items
        var selectedItems = ddlbsn_dhara_hai.Items
                            .Cast<ListItem>()
                            .Where(i => i.Selected)
                            .ToList();

        bool contains41 = selectedItems.Any(i => i.Value == "41");
        int selectedCount = selectedItems.Count;

        // 🔹 STEP 2: UI Visibility Logic
        if (contains41 && selectedCount == 1)
        {
            div_tbnm.Visible = true;
            div_tdhara.Visible = true;
            divdhara1.Visible = false;
        }
        else if (contains41 && selectedCount > 1)
        {
            div_tbnm.Visible = true;
            div_tdhara.Visible = true;
            divdhara1.Visible = true;
        }
        else
        {
            div_tbnm.Visible = false;
            div_tdhara.Visible = false;
            divdhara1.Visible = true;
        }

        // 🔥 STEP 3: Bind IPC based on BNS
        Binddhara();
    }

    void Binddhara()
    {
        try
        {
            // 🔥 GUARD (MOST IMPORTANT FIX)
            string ctrl = Request["__EVENTTARGET"];

            if (ctrl != ddlbsn_dhara_hai.UniqueID)
            {
                return;
            }

            // 🔹 Step 0: ViewState se lo
            List<string> previouslySelected = new List<string>();

            if (ViewState["SelectedIPC"] != null)
            {
                previouslySelected = (List<string>)ViewState["SelectedIPC"];
            }

            // 🔹 Step 1: BNS selected IDs
            List<string> selectedIds = new List<string>();

            foreach (ListItem item in ddlbsn_dhara_hai.Items)
            {
                if (item.Selected)
                {
                    selectedIds.Add(item.Value);
                }
            }

            if (selectedIds.Count == 0)
            {
                ddldhara1.Items.Clear();
                return;
            }

            string ids = string.Join(",", selectedIds);

            string sql = @"SELECT ID, IPC_Sec 
                       FROM Bns_dhara 
                       WHERE ID IN (" + ids + ")";

            DataTable dt = clsData.GetDataTable(sql);

            // 🔥 IMPORTANT: selection bind se pehle clear mat karo
            ddldhara1.DataSource = dt;
            ddldhara1.DataTextField = "IPC_Sec";
            ddldhara1.DataValueField = "IPC_Sec";
            ddldhara1.DataBind();

            // 🔥 restore selection
            foreach (ListItem item in ddldhara1.Items)
            {
                if (previouslySelected.Contains(item.Value))
                {
                    item.Selected = true;
                }
            }

            // 🔥 NEW: updated selection ko dobara ViewState me save karo
            List<string> updatedSelected = new List<string>();

            foreach (ListItem item in ddldhara1.Items)
            {
                if (item.Selected)
                {
                    updatedSelected.Add(item.Value);
                }
            }

            ViewState["SelectedIPC"] = updatedSelected;

            dt.Dispose();
        }
        catch { }
    }
    //void Binddhara()
    //{
    //    try
    //    {
    //        // 🔹 Step 0: Save already selected values of second listbox
    //        List<string> previouslySelected = new List<string>();

    //        foreach (ListItem item in ddldhara1.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                previouslySelected.Add(item.Value);
    //            }
    //        }

    //        // 🔹 Step 1: Get all selected IDs from first listbox
    //        List<string> selectedIds = new List<string>();

    //        foreach (ListItem item in ddlbsn_dhara_hai.Items)
    //        {
    //            if (item.Selected)
    //            {
    //                selectedIds.Add(item.Value);
    //            }
    //        }

    //        if (selectedIds.Count == 0)
    //        {
    //            ddldhara1.Items.Clear();
    //            return;
    //        }

    //        // 🔹 Step 2: Create comma separated IDs
    //        string ids = string.Join(",", selectedIds);

    //        // 🔹 Step 3: Fetch data
    //        string sql = @"SELECT ID, IPC_Sec 
    //                   FROM Bns_dhara 
    //                   WHERE ID IN (" + ids + ")";

    //        DataTable dt = clsData.GetDataTable(sql);

    //        // 🔹 Step 4: Bind
    //        ddldhara1.DataSource = dt;
    //        ddldhara1.DataTextField = "IPC_Sec";
    //        ddldhara1.DataValueField = "ID";
    //        ddldhara1.DataBind();
    //        ddldhara1.Items.Insert(0, new ListItem("--Select--", "0"));

    //        // 🔹 Step 5: Restore previous selections
    //        foreach (ListItem item in ddldhara1.Items)
    //        {
    //            if (previouslySelected.Contains(item.Value))
    //            {
    //                item.Selected = true;
    //            }
    //        }

    //        dt.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        // Handle error if needed
    //    }
    //}
    protected void ddlSanhaStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtSanahaSankhiyan.Text = "";
        if (ddlSanhaStatus.SelectedIndex == 1)
        {
            divSanahaSankhiyan1.Visible = true;
            divSanahaSankhiyan2.Visible = true;

        }
        else
        {
            divSanahaSankhiyan1.Visible = false;
            divSanahaSankhiyan2.Visible = false;

        }
    }
    //protected void btnbhumivivad_Click(object sender, EventArgs e)
    //{

    //    string dhaara = "";

    //    if (chk107.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk107.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk107.Text;
    //        }
    //    }
    //    if (chk109.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk109.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk109.Text;
    //        }
    //    }
    //    if (chk110.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk110.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk110.Text;
    //        }
    //    }
    //    if (chk113.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk113.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk113.Text;
    //        }
    //    }
    //    if (chk116.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk116.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk116.Text;
    //        }
    //    }
    //    if (chk133.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk133.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk133.Text;
    //        }
    //    }
    //    if (chk144.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk144.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk144.Text;
    //        }
    //    }

    //    if (chk145.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk145.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk145.Text;
    //        }
    //    }

    //    if (chk147.Checked)
    //    {
    //        if (dhaara != "")
    //        {
    //            dhaara = dhaara + ", " + chk147.Text;
    //        }
    //        else
    //        {
    //            dhaara = chk147.Text;
    //        }
    //    }

    //    DataTable dt = (DataTable)ViewState["DetailsOfIncidentDT"];
    //    dt.Rows.Add(txtghatanaDate.Text,
    //        txtghatanavivran.Text.Trim(), ddlPrathmiki_huyee_hai.SelectedValue.Trim(), txtFIR_sankhya.Text.Trim(), txtPrathmik_vivran.Text.Trim(),
    //        ddlAprathmiki_huyee_hai.SelectedValue.Trim(), dhaara, txtAFIR_sankhya.Text.Trim(), txtAprathmik_vivran.Text.Trim(), txtabhiyukt_vaad.Text.Trim(), ddlSanhaStatus.SelectedValue.Trim(), txtSanahaSankhiyan.Text.Trim());
    //    ViewState["DetailsOfIncidentDT"] = dt;
    //    BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
    //    txtghatanaDate.Text = "";
    //    txtghatanavivran.Text = "";
    //    ddlPrathmiki_huyee_hai.SelectedIndex = 0;
    //    ddlDescriptionType.Text = "";
    //    txtFIR_sankhya.Text = "";
    //    txtPrathmik_vivran.Text = "";
    //    ddlAprathmiki_huyee_hai.SelectedIndex = 0;
    //    txtAFIR_sankhya.Text = "";
    //    txtAprathmik_vivran.Text = "";
    //    txtabhiyukt_vaad.Text = "";
    //}

    protected void btnbhumivivad_Click(object sender, EventArgs e)
    {


        string dhaara = "";

        if (chk107.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk107.Text;
            }
            else
            {
                dhaara = chk107.Text;
            }
        }
        if (chk109.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk109.Text;
            }
            else
            {
                dhaara = chk109.Text;
            }
        }
        if (chk110.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk110.Text;
            }
            else
            {
                dhaara = chk110.Text;
            }
        }
        if (chk113.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk113.Text;
            }
            else
            {
                dhaara = chk113.Text;
            }
        }
        if (chk116.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk116.Text;
            }
            else
            {
                dhaara = chk116.Text;
            }
        }
        if (chk133.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk133.Text;
            }
            else
            {
                dhaara = chk133.Text;
            }
        }
        if (chk144.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk144.Text;
            }
            else
            {
                dhaara = chk144.Text;
            }
        }

        if (chk145.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk145.Text;
            }
            else
            {
                dhaara = chk145.Text;
            }
        }

        if (chk147.Checked)
        {
            if (dhaara != "")
            {
                dhaara = dhaara + ", " + chk147.Text;
            }
            else
            {
                dhaara = chk147.Text;
            }
        }

        //change validation start
        // ===== RADIO VALIDATION =====
        if (ddlAprathmiki_huyee_hai.SelectedValue == "Y")
        {
            if (!rdoOld.Checked && !rdoNew.Checked)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                    "alert('Please select Old Dhara or New Dhara');", true);
                return;
            }
            // ===== OLD DHARA VALIDATION =====
            if (rdoOld.Checked)
            {
                bool anyChecked = chk107.Checked || chk109.Checked || chk110.Checked ||
                                  chk113.Checked || chk116.Checked || chk133.Checked ||
                                  chk144.Checked || chk145.Checked || chk147.Checked;

                if (!anyChecked)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "alert('Please select at least one Old Dhara');", true);
                    return;
                }
            }
            // ===== NEW DHARA VALIDATION =====
            if (rdoNew.Checked)
            {
                // 1️⃣ BNM selected?
                bool anyBNMSelected = ddlbsn_dhara_hai.Items
                                        .Cast<ListItem>()
                                        .Any(i => i.Selected);

                if (!anyBNMSelected)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "alert('Please select at least one BNM');", true);
                    return;
                }

                // 2️⃣ NewDhara selected?
                bool anyDharaSelected = ddldhara1.Items
                                           .Cast<ListItem>()
                                           .Any(i => i.Selected);

                if (!anyDharaSelected)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                        "alert('Please select at least one New Dhara');", true);
                    return;
                }
                // 3️⃣ If 41 (Other) selected in BNM
                bool contains41 = ddlbsn_dhara_hai.Items
                                    .Cast<ListItem>()
                                    .Any(i => i.Selected && i.Value == "41");

                if (contains41)
                {
                    if (string.IsNullOrWhiteSpace(txtbnm.Text) ||
                        string.IsNullOrWhiteSpace(txtdhara.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('Please fill Other BNM and Other Dhara details');", true);
                        return;
                    }
                }
            }
            if (txtAFIR_sankhya.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                       "alert('कृपया अप्राथमिकी संख्या अंकित करें...!');", true);
                return;

            }
        }

        //change validation end

        // ===== NEW PART START =====

        string bnm = "";
        string newdhara = "";
        string bnm1 = "";
        string newdhara1 = "";

        if (rdoNew.Checked)
        {
            bnm = GetSelectedValues(ddlbsn_dhara_hai);
            newdhara = GetSelectedValues(ddldhara1);

            bool contains41 = ddlbsn_dhara_hai.Items
                .Cast<ListItem>()
                .Any(i => i.Selected && i.Value == "41");

            if (contains41)
            {
                bnm1 = txtbnm.Text.Trim();
                newdhara1 = txtdhara.Text.Trim();
            }
        }


        DataTable dt = (DataTable)ViewState["DetailsOfIncidentDT"];
        dt.Rows.Add(txtghatanaDate.Text,
            txtghatanavivran.Text.Trim(), ddlPrathmiki_huyee_hai.SelectedValue.Trim(), txtFIR_sankhya.Text.Trim(), txtPrathmik_vivran.Text.Trim(),
            ddlAprathmiki_huyee_hai.SelectedValue.Trim(), dhaara, txtAFIR_sankhya.Text.Trim(), txtAprathmik_vivran.Text.Trim(), txtabhiyukt_vaad.Text.Trim(), ddlSanhaStatus.SelectedValue.Trim(), txtSanahaSankhiyan.Text.Trim(), bnm,
        newdhara,
        bnm1,
        newdhara1);
        ViewState["DetailsOfIncidentDT"] = dt;
        BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
        txtghatanaDate.Text = "";
        txtghatanavivran.Text = "";
        ddlPrathmiki_huyee_hai.SelectedIndex = 0;
        ddlDescriptionType.Text = "";
        txtFIR_sankhya.Text = "";
        txtPrathmik_vivran.Text = "";
        ddlAprathmiki_huyee_hai.SelectedIndex = 0;
        txtAFIR_sankhya.Text = "";
        txtAprathmik_vivran.Text = "";
        txtabhiyukt_vaad.Text = "";
        rdoOld.Checked = false;
        rdoNew.Checked = false;
        txtbnm.Text = "";
        txtdhara.Text = "";
        ddldhara1.ClearSelection();
        ddlbsn_dhara_hai.ClearSelection();
        div_tbnm.Visible = false;

        div_tdhara.Visible = false;

        divdhara1.Visible = false;

        divbsn.Visible = false;
        divdharabsn.Visible = false;
        divAPrathmiki_sankhiyan.Visible = false;

        divAPrathmiki_vivaran.Visible = false;
        //forold dhara checkbox
        chk107.Checked = false;
        chk109.Checked = false;
        chk110.Checked = false;
        chk113.Checked = false;
        chk116.Checked = false;
        chk133.Checked = false;
        chk144.Checked = false;
        chk145.Checked = false;
        chk147.Checked = false;
        divDhara.Visible = false;
        //end
    }
    private string GetSelectedValues(ListBox listBox)
    {
        return string.Join(", ",
            listBox.Items.Cast<ListItem>()
            .Where(i => i.Selected)
            .Select(i => i.Text));
    }
    protected void BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin()
    {
        try
        {
            grdbhumivivad.DataSource = (DataTable)ViewState["DetailsOfIncidentDT"];
            grdbhumivivad.DataBind();
        }
        catch { }
        try
        {
            grdnyayalay_vivran.DataSource = (DataTable)ViewState["CourtDisputeDetailsDT"];
            grdnyayalay_vivran.DataBind();
        }
        catch { }
    }
    protected void grdbhumivivad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            string rowIndex = e.CommandArgument.ToString();
            DataTable dt = (DataTable)ViewState["DetailsOfIncidentDT"];
            dt.Rows.RemoveAt(Convert.ToInt32(rowIndex));
            ViewState["DetailsOfIncidentDT"] = dt;
            BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
        }
    }
    protected void ddl_Isbhumi_Viviad_available_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Isbhumi_Viviad_available.SelectedValue == "Y")
        {
            btnnyayalay1.Visible = true;
            //btnnyayalay2.Visible = true;
            btnnyayalay3.Visible = true;
            btnnyayalay4.Visible = true;
            btnnyayalay5.Visible = true;
            btnnyayalay6.Visible = true;
            btnnyayalay7.Visible = true;
            btnnayaylaysave.Visible = true;
            ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
            this.BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
        }
        else
        {
            btnnyayalay1.Visible = false;
            btnnyayalay3.Visible = false;
            btnnyayalay4.Visible = false;
            btnnyayalay5.Visible = false;
            btnnyayalay6.Visible = false;
            btnnyayalay7.Visible = false;
            btnnayaylaysave.Visible = false;
            divDist_nyayalaya_type.Visible = false;
            divSubdivision_nyayalaya_type.Visible = false;
            ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
            this.BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
        }
    }
    protected void ddlnyayalaya_SelectedIndexChanged(object sender, EventArgs e)
    {
        div_rajasw_vevhar_nyalay.Visible = false;
        divSubdivision_nyayalaya_type.Visible = false;
        divDist_nyayalaya_type.Visible = false;
        //divVibhag_nyayalay_type.Visible = false;
        if (ddlnyayalaya.SelectedIndex == 1)
        {
            BindNyayalayaType();
            div_rajasw_vevhar_nyalay.Visible = true;
            labNyayalaya_type.Text = "राजस्व न्यायालय का प्रकार";
        }

        else if (ddlnyayalaya.SelectedIndex == 2)
        {
            BindNyayalayaType();
            div_rajasw_vevhar_nyalay.Visible = true;
            labNyayalaya_type.Text = "व्यवहार न्यायालय का प्रकार";
        }
        else if (ddlnyayalaya.SelectedIndex == 3)
        {
            BindNyayalayaType_dist();
            BindNyayalayaType_SubDivision();
            divDist_nyayalaya_type.Visible = true;
            divSubdivision_nyayalaya_type.Visible = true;
        }
        else if (ddlnyayalaya.SelectedIndex == 4)
        {
            //BindNyayalayaType();
            //div_rajasw_vevhar_nyalay.Visible = true;
            //labNyayalaya_type.Text = "लोक शिकायत निवारण न्यायालय का प्रकार";
        }
        else
        {
            BindNyayalayaType();
            ddlnyayalaya_type.SelectedIndex = 0;
        }
    }
    void BindNyayalayaType_dist()
    {
        try
        {
            string sql = @"SELECT distinct DISTRICTNAME,DISTRICTCODE from mst_Commissionary_Districts ORDER BY DISTRICTNAME ";
            //SqlParameter _Commisionary = new SqlParameter("@DIVISIONCODE", Commisionary);
            DataTable dt = clsData.GetDataTable(sql);
            ddlDist_nyayalaya_type.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                ddlDist_nyayalaya_type.DataSource = dt;
                ddlDist_nyayalaya_type.DataTextField = "DISTRICTNAME";
                ddlDist_nyayalaya_type.DataValueField = "DISTRICTCODE";
                ddlDist_nyayalaya_type.DataBind();
                ddlDist_nyayalaya_type.Items.Insert(0, new ListItem("--Select--", "0"));
                dt.Dispose();
            }

        }
        catch { }
    }
    void BindNyayalayaType_SubDivision()
    {
        try
        {


            string sql = @"select DISTINCT sd.Sd_Name_En as SubDivisionName,sd.Sd_Code2 as SubDivisionCode, sd.Sd_Name_En from SubDivisions 
                           sd order by sd.Sd_Name_En";
            DataTable dt = clsData.GetDataTable(sql);


            ddlSubdivision_nyayalaya_type.DataSource = dt;
            ddlSubdivision_nyayalaya_type.DataTextField = "SubDivisionName";
            ddlSubdivision_nyayalaya_type.DataValueField = "SubDivisionCode";
            ddlSubdivision_nyayalaya_type.DataBind();
            ddlSubdivision_nyayalaya_type.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }

    }
    void BindNyayalayaType()
    {
        try
        {
            string sql = @"select t.id, t.name from mst_CourtType t inner join mst_court c on t.court_id = c.id
                           where t.court_id=@court_id and isnull(t.IsActive,'N')='Y' order by t.id, t.name asc";

            SqlParameter _court_id = new SqlParameter("@court_id", ddlnyayalaya.SelectedValue.Trim());
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { _court_id });

            ddlnyayalaya_type.DataSource = dt;
            ddlnyayalaya_type.DataTextField = "name";
            ddlnyayalaya_type.DataValueField = "id";
            ddlnyayalaya_type.DataBind();
            ddlnyayalaya_type.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }
    }
    void BindNyayalayaType_Vibhag()
    {
        try
        {


            string sql = @" select DISTINCT sd.name ,sd.id from mst_Deptl_Public_Grievance_Redressal_Court_Type sd order by sd.id";
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { });
            ddlVibhag_nyayalay_type.DataSource = dt;
            ddlVibhag_nyayalay_type.DataTextField = "name";
            ddlVibhag_nyayalay_type.DataValueField = "id";
            ddlVibhag_nyayalay_type.DataBind();
            ddlVibhag_nyayalay_type.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();


        }
        catch { }

    }
    protected void ddlnyayalaya_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        divDist_nyayalaya_type.Visible = false;
        divSubdivision_nyayalaya_type.Visible = false;
        //divVibhag_nyayalay_type.Visible = false;
        if (ddlnyayalaya_type.SelectedIndex == 1)
        {
            BindNyayalayaType_Vibhag();
            //divVibhag_nyayalay_type.Visible = true;
        }
        else if (ddlnyayalaya_type.SelectedIndex == 2)
        {
            BindNyayalayaType_SubDivision();
            divSubdivision_nyayalaya_type.Visible = true;
        }
        else if (ddlnyayalaya_type.SelectedIndex == 3)
        {
            BindNyayalayaType_dist();
            divDist_nyayalaya_type.Visible = true;
        }
    }
    protected void ddlDist_nyayalaya_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNyayalayaType_SubDivision();
    }
    void BindNyayalaya()
    {
        try
        {
            string sql = @"select id, name from mst_court order by id asc";
            DataTable dt = clsData.GetDataTable(sql, new SqlParameter[] { });
            ddlnyayalaya.DataSource = dt;
            ddlnyayalaya.DataTextField = "name";
            ddlnyayalaya.DataValueField = "id";
            ddlnyayalaya.DataBind();
            ddlnyayalaya.Items.Insert(0, new ListItem("--Select--", "0"));
            dt.Dispose();
        }
        catch { }
    }
    protected void btnnayaylaysave_Click(object sender, EventArgs e)
    {


        DataTable dt = (DataTable)ViewState["CourtDisputeDetailsDT"];
        dt.Rows.Add(ddlnyayalaya.SelectedValue.Trim(),
        ddlnyayalaya_type.SelectedValue.Trim(),
        ddlDist_nyayalaya_type.SelectedValue.Trim(),
        ddlSubdivision_nyayalaya_type.SelectedValue.Trim(),
        ddlVibhag_nyayalay_type.SelectedValue.Trim(),
        txtdayarvaadsankhya_nayalay.Text.Trim(),
        txtvaadiname_nayaylay.Text.Trim(),
        txtprativadi_nayaylay.Text.Trim(),
        txtwadKiAddhatan_Sthiti_nayayaly.Text.Trim(),

        ddlnyayalaya.SelectedItem != null ? ddlnyayalaya.SelectedItem.Text : "",
        ddlnyayalaya_type.SelectedItem.Text,
        ddlDist_nyayalaya_type.SelectedValue != "0" ? ddlDist_nyayalaya_type.SelectedItem.Text : "",
        ddlSubdivision_nyayalaya_type.SelectedValue != "0" ? ddlSubdivision_nyayalaya_type.SelectedItem.Text : "",
        ddlVibhag_nyayalay_type.SelectedValue != "0" ? ddlVibhag_nyayalay_type.SelectedItem.Text : "");
        ViewState["CourtDisputeDetailsDT"] = dt;
        BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
        ddlnyayalaya.SelectedIndex = 0;
        ddlnyayalaya_type.SelectedIndex = 0;
        if (ddlDist_nyayalaya_type.SelectedItem != null)
        { ddlDist_nyayalaya_type.SelectedIndex = 0; }

        if (ddlSubdivision_nyayalaya_type.SelectedItem != null)
        { ddlSubdivision_nyayalaya_type.SelectedIndex = 0; }

        if (ddlVibhag_nyayalay_type.SelectedItem != null)
        { ddlVibhag_nyayalay_type.SelectedIndex = 0; }

        txtdayarvaadsankhya_nayalay.Text = "";
        txtvaadiname_nayaylay.Text = "";
        txtdayaryear_nayayaly.Text = "";
        txtprativadi_nayaylay.Text = "";
        txtwadKiAddhatan_Sthiti_nayayaly.Text = "";

    }
    protected void grdnyayalay_vivran_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            string rowIndex = e.CommandArgument.ToString();
            DataTable dt = (DataTable)ViewState["CourtDisputeDetailsDT"];
            dt.Rows.RemoveAt(Convert.ToInt32(rowIndex));
            ViewState["CourtDisputeDetailsDT"] = dt;
            BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();



        }
    }
    public bool valifBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin()
    {
        bool flag = true;
        if (dd_IsBhumiVivad.SelectedIndex == 0)
        {
            Utility.showMessage(this, "क्या भूमि विवाद सें संबंधित प्राथमिकी/अप्राथमिकी दर्ज है ? हां/नहीं चुनें...!");
            dd_IsBhumiVivad.Focus();
            flag = false;
        }

        if (dd_IsBhumiVivad.SelectedIndex == 1 && grdbhumivivad.Rows.Count == 0)
        {
            Utility.showMessage(this, "कृपया विवाद सें संबंधित घटना/ वारदात का विवरण जोड़ें...!");
            btnBhumiVivadVivran1.Focus();
            btnBhumiVivadVivran2.Focus();
            btnBhumiVivadVivran3.Focus();
            btnBhumiVivadVivran4.Focus();
            btnBhumiVivadVivran5.Focus();
            btnBhumiVivadVivran6.Focus();
            flag = false;
        }
        if (ddl_Isbhumi_Viviad_available.SelectedIndex == 0)
        {
            Utility.showMessage(this, "क्या न्यायालय में प्रक्रियाधीन वाद का विवरण उपलब्ध है ? हां/नहीं चुनें...!");
            ddl_Isbhumi_Viviad_available.Focus();
            flag = false;
        }

        if (ddl_Isbhumi_Viviad_available.SelectedIndex == 1 && grdnyayalay_vivran.Rows.Count == 0)
        {
            Utility.showMessage(this, "कृपया न्यायालय में प्रक्रियाधीन वाद का विवरण जोड़ें...!");
            btnnayaylaysave.Focus();
            flag = false;
        }
        return flag;
    }
    public void SaveBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin()
    {
        if (valifBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin() == true)
        {

            SqlParameter bhumi_vivad_Vivran_Available = new SqlParameter("@bhumi_vivad_Vivran_Available", dd_IsBhumiVivad.SelectedValue.Trim());
            SqlParameter dispute_in_court_available = new SqlParameter("@dispute_in_court_available", ddl_Isbhumi_Viviad_available.SelectedValue.Trim());
            DataTable DetailsOfIncidentDT = (DataTable)ViewState["DetailsOfIncidentDT"];
            try
            {
                if (dd_IsBhumiVivad.SelectedIndex == 1 && DetailsOfIncidentDT.Rows.Count == 0)
                {
                    Utility.showMessage(this, "कृपया भूमि विवाद सें संबंधित प्राथमिकी/अप्राथमिकी दर्ज करें...!");
                    return;
                }
            }
            catch { }

            DataTable CourtDisputeDetailsDT1 = (DataTable)ViewState["CourtDisputeDetailsDT"];
            DataTable CourtDisputeDetailsDT = CourtDisputeDetailsDT1.Copy();
            try
            {
                if (ddl_Isbhumi_Viviad_available.SelectedIndex == 1 && CourtDisputeDetailsDT.Rows.Count == 0)
                {
                    Utility.showMessage(this, "कृपया न्यायालय में प्रक्रियाधीन वाद का विवरण दर्ज करें...!");
                    return;
                }
                CourtDisputeDetailsDT.Columns.Remove("court");
                CourtDisputeDetailsDT.Columns.Remove("courtType");
                CourtDisputeDetailsDT.Columns.Remove("Dst");
                CourtDisputeDetailsDT.Columns.Remove("SubDiv");
                CourtDisputeDetailsDT.Columns.Remove("Vibhag");
            }
            catch { }
            SqlParameter LandDisputeDetailsEntryTable = new SqlParameter("@LandDisputeDetailsEntryTable", DetailsOfIncidentDT);
            SqlParameter CourtDisputeDetailsEntryTable = new SqlParameter("@CourtDisputeDetailsEntryTable", CourtDisputeDetailsDT);
            SqlParameter a_id = new SqlParameter("@a_id", Convert.ToInt64(ViewState["a_id"]));

            DataTable dt = clsData.GetDataTableWithProc("usp_SaveBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin", new SqlParameter[]
                       {
                a_id
                ,bhumi_vivad_Vivran_Available
                ,dispute_in_court_available
                ,LandDisputeDetailsEntryTable
                ,CourtDisputeDetailsEntryTable
                       });

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                {
                    //lblMsg.Text = "आवेदनकर्ता का विवरण सफलतापूर्वक सहेजा गया";
                    Utility.showMessage(Page, "भूमि विवाद सें संबंधित घटना/ वारदात और न्यायालय में प्रक्रियाधीन वाद का विवरण सफलतापूर्वक सहेजा गया");
                    GetAnchalaAdhikari();
                    ClearBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
                    ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                    ViewState["a_id"] = dt.Rows[0]["id"].ToString();
                    ViewState["Step"] = 7;
                    step6.Visible = false;
                    step7.Visible = true;
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
    public void ClearBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin()
    {
        DataTable DetailsOfIncidentDT = (DataTable)ViewState["DetailsOfIncidentDT"];
        DetailsOfIncidentDT.Clear();
        grdbhumivivad.DataSource = DetailsOfIncidentDT;
        grdbhumivivad.DataBind();

        DataTable CourtDisputeDetailsDT = (DataTable)ViewState["CourtDisputeDetailsDT"];
        CourtDisputeDetailsDT.Clear();
        grdnyayalay_vivran.DataSource = CourtDisputeDetailsDT;
        grdnyayalay_vivran.DataBind();
        ddlYear.SelectedIndex = 0;

        dd_IsBhumiVivad.SelectedIndex = 0;
        ddl_Isbhumi_Viviad_available.SelectedIndex = 0;
    }

    #endregion

    #region अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी

    protected void bind_BhumiSanvedanshilta()// भूमि विवाद कि सवेदनशीलता
    {
        try
        {
            DataTable dt = clsData.GetDataTableWithProc("SP_SensitivityType", new SqlParameter[] { });
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    ddlbhumivivadki_sanvedanshilta.DataSource = dt;
                    ddlbhumivivadki_sanvedanshilta.DataTextField = "SensitivityType";
                    ddlbhumivivadki_sanvedanshilta.DataValueField = "id";
                    ddlbhumivivadki_sanvedanshilta.DataBind();
                    ddlbhumivivadki_sanvedanshilta.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
        }
        catch (Exception ee)
        {


        }
    }
    protected void ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "0")
        {
            onestar.Visible = false;
            twostar.Visible = false;
            threestar.Visible = false;
            fourstar.Visible = false;
        }
        else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "1")
        {
            onestar.Visible = true;
            twostar.Visible = false;
            threestar.Visible = false;
            fourstar.Visible = false;
        }
        else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "2")
        {
            onestar.Visible = false;
            twostar.Visible = true;
            threestar.Visible = false;
            fourstar.Visible = false;
        }
        else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "3")
        {
            onestar.Visible = false;
            twostar.Visible = false;
            threestar.Visible = true;
            fourstar.Visible = false;
        }
        else if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "4")
        {
            onestar.Visible = false;
            twostar.Visible = false;
            threestar.Visible = false;
            fourstar.Visible = true;
        }
    }
    protected void ddlaction_SelectedIndexChanged(object sender, EventArgs e)
    {
        divNextDateDef.Visible = true;
        //divlabNextDate.Visible = false;
        divNextDate.Visible = false;
        divCancelReason.Visible = false;
        divvadkavars.Visible = false;
        txtAgalaDate.Text = "";
        txtCancelReason.Text = "";
        txtvadkavars.Text = "";
        //txtAgalaDate,txtCancelReason, txtAgalaDate,labNextDate

        if (ddlaction.SelectedIndex == 1)
        {
            divNextDateDef.Visible = false;
            //divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "प्रारंभिक निष्पादन की तिथि";
        }
        else if (ddlaction.SelectedIndex == 2)
        {
            divNextDateDef.Visible = false;
            //divlabNextDate.Visible = true;
            //labNextDate.Text = "अस्वीकृति का कारण";
            divCancelReason.Visible = true;

        }
        else if (ddlaction.SelectedIndex == 3)
        {
            divNextDateDef.Visible = false;
            //divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "मापी की तिथि";
        }
        else if (ddlaction.SelectedIndex == 4)
        {
            divNextDateDef.Visible = false;
            //divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "अगली सुनवाई की तिथि";
        }
        else if (ddlaction.SelectedIndex == 5)
        {
            divNextDateDef.Visible = false;
            //divlabNextDate.Visible = true;
            divNextDate.Visible = true;
            labNextDate.Text = "अंतिम निष्पादन की तिथि";
        }
        else if (ddlaction.SelectedIndex == 6)
        {
            divNextDateDef.Visible = false;
            divvadkavars.Visible = true;
            
            labNextDate.Text = "वादी की वाद संख्या / वर्ष";
        }

    }

    public bool validationAnchalaAdhikari()
    {
        bool flag = true;
        if (ddlbhumivivadki_sanvedanshilta.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया भूमि की संवेदनशीलता चुनें...!");
            ddlbhumivivadki_sanvedanshilta.Focus();
            flag = false;
        }

        if (txtbaithakDate.Text.Trim() == "")
        {
            Utility.showMessage(this, "कृपया बैठक की तिथि अंकित करें...!");
            txtbaithakDate.Focus();
            flag = false;
        }

        if (ddlIsVadiAvailable.SelectedIndex == 0)
        {
            Utility.showMessage(this, "क्या वादी उपस्थित है ? हां/नहीं चुनें...!");
            ddlIsVadiAvailable.Focus();
            flag = false;
        }

        if (ddl_IsprativadiAvailable.SelectedIndex == 0)
        {
            Utility.showMessage(this, "क्या प्रतिवादी उपस्थित है ? हां/नहीं चुनें...!");
            ddl_IsprativadiAvailable.Focus();
            flag = false;
        }

        if (ddlaction.SelectedIndex == 0)
        {
            Utility.showMessage(this, "कृपया बैठक का निष्कर्ष चुनें...!");
            ddlaction.Focus();
            flag = false;
        }

        if (ddlaction.SelectedIndex == 1)
        {
            if (txtAgalaDate.Text.Trim() == "")
            {
                Utility.showMessage(this, "कृपया प्रारंभिक निष्पादन की तिथि अंकित करें...!");
                txtAgalaDate.Focus();
                flag = false;
            }
        }

        if (ddlaction.SelectedIndex == 2)
        {
            if (txtCancelReason.Text.Trim() == "")
            {
                Utility.showMessage(this, "कृपया अस्वीकृति का कारण अंकित करें...!");
                txtCancelReason.Focus();
                flag = false;
            }
        }

        if (ddlaction.SelectedIndex == 3)
        {
            if (txtAgalaDate.Text.Trim() == "")
            {
                Utility.showMessage(this, "कृपया मापी की तिथि अंकित करें...!");
                txtAgalaDate.Focus();
                flag = false;
            }
        }

        if (ddlaction.SelectedIndex == 4)
        {
            if (txtAgalaDate.Text.Trim() == "")
            {
                Utility.showMessage(this, "कृपया अगली सुनवाई की तिथि अंकित करें...!");
                txtAgalaDate.Focus();
                flag = false;
            }
        }
        if (ddlaction.SelectedIndex == 5)
        {
            if (txtAgalaDate.Text.Trim() == "")
            {
                Utility.showMessage(this, "कृपया अंतिम निष्पादन की तिथि अंकित करें...!");
                txtAgalaDate.Focus();
                flag = false;
            }
        }
        if (ddlaction.SelectedIndex == 6)
        {
            if (txtvadkavars.Text.Trim() == "")
            {
                Utility.showMessage(this, "कृपया वादी की वाद संख्या / वर्ष अंकित करें...!");
                txtvadkavars.Focus();
                flag = false;
            }
        }
        


        return flag;
    }
    public void saveAnchalaAdhikari()
    {
        if(Session["UserId"]!=null)
        {
            if (validationAnchalaAdhikari() == true)
            {
                string landDoc = string.Empty;
                string _CircleOfficer_letterOfIntent = string.Empty;
                string _PoliceOfficer_letterOfIntent = string.Empty;
                string sql = @"select * from ActionDetailsEntry where a_id=@a_id";
                SqlParameter _a_id = new SqlParameter("@a_id", Convert.ToInt64(ViewState["a_id"]).ToString());
                DataTable dtActionGetdata = clsData.GetDataTable(sql, new SqlParameter[] { _a_id });

                if (LandDoc.HasFile)
                {
                    if (!validateFile(LandDoc, "doc"))
                        return;
                    string m = FileUploadValidator.IsPdf(LandDoc.PostedFile, 1024, 1024);
                    if (m == "OK")
                    {
                        landDoc = "~/LandDoc/Upload/LandDocuments" + Convert.ToInt64(ViewState["a_id"]) + "/LandDocuments" + dtActionGetdata.Rows.Count + 1 + ".pdf";
                    }
                    else
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return;
                    }
                }

                if (CircleOfficer_letterOfIntent.HasFile)
                {
                    if (!validateFile(CircleOfficer_letterOfIntent, "doc"))
                        return;
                    string m = FileUploadValidator.IsPdf(CircleOfficer_letterOfIntent.PostedFile, 1024, 1024);
                    if (m == "OK")
                    {
                        _CircleOfficer_letterOfIntent = "~/LandDoc/Upload/CirclePulisPadadhikariPatr" + Convert.ToInt64(ViewState["a_id"]) + "/CirclePulisPadadhikariPatr" + dtActionGetdata.Rows.Count + 1 + ".pdf";
                    }
                    else
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return;
                    }
                }


                if (PoliceOfficer_letterOfIntent.HasFile)
                {
                    if (!validateFile(PoliceOfficer_letterOfIntent, "doc"))
                        return;
                    string m = FileUploadValidator.IsPdf(PoliceOfficer_letterOfIntent.PostedFile, 1024, 1024);
                    if (m == "OK")
                    {
                        _PoliceOfficer_letterOfIntent = "~/LandDoc/Upload/PulisPadadhikariPatr" + Convert.ToInt64(ViewState["a_id"]) + "/PulisPadadhikariPatr" + dtActionGetdata.Rows.Count + 1 + ".pdf";
                    }
                    else
                    {
                        lblMsg.Text = "(पत्र केवल .pdf प्रारूप में 3 MB तक में अपलोड करे)";
                        return;
                    }
                }

                string message = "";
                if (LandDoc.HasFile)
                {
                    string path = "~/LandDoc/Upload/LandDocuments" + ViewState["a_id"].ToString() + "/";
                    string v = InsSaveFile("LandDocuments" + dtActionGetdata.Rows.Count + 1, LandDoc, ViewState["a_id"].ToString(), path);
                    string pathcheck = "~/LandDoc/Upload/LandDocuments" + ViewState["a_id"].ToString() + "/LandDocuments" + dtActionGetdata.Rows.Count + 1 + ".pdf";
                    if (v == "0" || v != pathcheck)
                    {
                        landDoc = "";
                        message = "LandDocuments not upload";
                    }
                }

                if (CircleOfficer_letterOfIntent.HasFile)
                {
                    string path = "~/LandDoc/Upload/CirclePulisPadadhikariPatr" + ViewState["a_id"].ToString() + "/";
                    string v = InsSaveFile("CirclePulisPadadhikariPatr" + dtActionGetdata.Rows.Count + 1, CircleOfficer_letterOfIntent, ViewState["a_id"].ToString(), path);
                    string pathcheck = "~/LandDoc/Upload/CirclePulisPadadhikariPatr" + Convert.ToInt64(ViewState["a_id"]) + "/CirclePulisPadadhikariPatr" + dtActionGetdata.Rows.Count + 1 + ".pdf";
                    if (v == "0" || v != pathcheck)
                    {
                        _CircleOfficer_letterOfIntent = "";
                        message = message + "; CirclePulisPadadhikariPatr not upload";
                        lblMsg.Text = "Technical Error";
                        return;
                    }
                }
                if (PoliceOfficer_letterOfIntent.HasFile)
                {
                    string path = "~/LandDoc/Upload/PulisPadadhikariPatr" + ViewState["a_id"].ToString() + "/";
                    string v = InsSaveFile("PulisPadadhikariPatr" + dtActionGetdata.Rows.Count + 1, PoliceOfficer_letterOfIntent, ViewState["a_id"].ToString(), path);
                    string pathcheck = "~/LandDoc/Upload/PulisPadadhikariPatr" + Convert.ToInt64(ViewState["a_id"]) + "/PulisPadadhikariPatr" + dtActionGetdata.Rows.Count + 1 + ".pdf";
                    if (v == "0" || v != pathcheck)
                    {
                        _PoliceOfficer_letterOfIntent = "";
                        message = message + "; PulisPadadhikariPatr not upload";
                        lblMsg.Text = "Technical Error";
                        return;
                    }
                }

                //SqlParameter PoliceOfficerletterOfIntent = new SqlParameter("@PoliceOfficer_letterOfIntent", _PoliceOfficer_letterOfIntent);



                DataTable dtAction = new DataTable();
                dtAction.Clear();

                dtAction.Columns.Add("Meeting_date", typeof(DateTime));
                dtAction.Columns.Add("Is_Vadi_Present", typeof(string));
                dtAction.Columns.Add("Is_PratiVadi_Present", typeof(string));
                dtAction.Columns.Add("conclusion_of_the_meeting", typeof(string));
                dtAction.Columns.Add("anchala_dhikari_mantavy", typeof(string));
                dtAction.Columns.Add("thana_prabhari_mantavy", typeof(string));
                dtAction.Columns.Add("Joint_report_SHO_Circle_Officer_file", typeof(string));
                dtAction.Columns.Add("Matter_Status", typeof(string));
                dtAction.Columns.Add("Matter_Status_by", typeof(string));
                dtAction.Columns.Add("Matter_Status_date", typeof(DateTime));
                dtAction.Columns.Add("date_of_disposal", typeof(DateTime));
                dtAction.Columns.Add("reason_for_rejection", typeof(string));
                dtAction.Columns.Add("mapi_ki_tithi", typeof(DateTime));
                dtAction.Columns.Add("agali_sunavaee_ki_tithi", typeof(DateTime));
                dtAction.Columns.Add("CircleOfficer_letterOfIntent", typeof(string));
                dtAction.Columns.Add("PoliceOfficer_letterOfIntent", typeof(string));
                dtAction.Columns.Add("Bhumi_savedansheelta", typeof(string));
                dtAction.Columns.Add("vaadi_ki_vaad_sankhya_varsh", typeof(string));
                dtAction.Rows.Add(
                    txtbaithakDate.Text.Trim() != "" ? txtbaithakDate.Text.Trim() : "01-01-1900",
                    ddlIsVadiAvailable.SelectedValue.Trim(),
                    ddl_IsprativadiAvailable.SelectedValue.Trim(),
                    txtfalafal.Text.Trim(),
                    txtabhiyukt_anchaladhikari.Text.Trim(),
                    txtabhiyukt_thaanprabhaaree.Text.Trim(),
                    landDoc.ToString().Trim(),
                    ddlaction.SelectedValue.Trim(),
                    Session["UserId"].ToString(),
                    DateTime.Now,
                    txtAgalaDate.Text.Trim() != "" ? txtAgalaDate.Text.Trim() : "01-01-1900",
                    txtCancelReason.Text.Trim(),
                    txtAgalaDate.Text.Trim() != "" ? txtAgalaDate.Text.Trim() : "01-01-1900",
                    txtAgalaDate.Text.Trim() != "" ? txtAgalaDate.Text.Trim() : "01-01-1900",
                   _CircleOfficer_letterOfIntent,
                   _PoliceOfficer_letterOfIntent,
                   ddlbhumivivadki_sanvedanshilta.SelectedValue.Trim(),
                   txtvadkavars.Text.Trim()
                   );

                SqlParameter ActionDetailsEntryTable = new SqlParameter("@ActionDetailsEntryTable", dtAction);


                SqlParameter a_id = new SqlParameter("@a_id", Convert.ToInt64(ViewState["a_id"]));

                DataTable dt = clsData.GetDataTableWithProc("usp_AnchalaAdhekariNew", new SqlParameter[]
                           {
                a_id
                ,ActionDetailsEntryTable
                           });
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["mStatus"].ToString().Equals("1"))
                    {
                        //lblMsg.Text = "अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी सफलतापूर्वक सहेजा गया";
                        //Utility.showMessage(Page, "अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी सफलतापूर्वक सहेजा गया");
                        ClientScript.RegisterStartupScript(this.GetType(), "HideLabel", "<script type=\"text/javascript\">setTimeout(\"document.getElementById('" + lblMsg.ClientID + "').style.display='none'\",2000)</script>");
                        clearAnchalaAdhikari();
                        ViewState["a_id"] = dt.Rows[0]["id"].ToString();
                        //if (LandDoc.HasFile)
                        //{
                        //    string path = "~/LandDoc/Upload/LandDocuments" + dt.Rows[0]["id"].ToString() + "/";
                        //    string v = InsSaveFile("LandDocuments" + dtActionGetdata.Rows.Count + 1, LandDoc, dt.Rows[0]["id"].ToString(), path);
                        //}

                        //if (CircleOfficer_letterOfIntent.HasFile)
                        //{
                        //    string path = "~/LandDoc/Upload/CirclePulisPadadhikariPatr" + dt.Rows[0]["id"].ToString() + "/";
                        //    string v = InsSaveFile("CirclePulisPadadhikariPatr" + dtActionGetdata.Rows.Count + 1, CircleOfficer_letterOfIntent, dt.Rows[0]["id"].ToString(), path);
                        //}
                        //if (PoliceOfficer_letterOfIntent.HasFile)
                        //{
                        //    string path = "~/LandDoc/Upload/PulisPadadhikariPatr" + dt.Rows[0]["id"].ToString() + "/";
                        //    string v = InsSaveFile("PulisPadadhikariPatr" + dtActionGetdata.Rows.Count + 1, PoliceOfficer_letterOfIntent, dt.Rows[0]["id"].ToString(), path);
                        //}
                        Encryptor enc = new Encryptor(Encryptor.PrivateKey);
                        string UrlRedirect = enc.Encrypt(ViewState["a_id"].ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('आवेदनकर्ता का विवरण सफलतापूर्वक सहेजा गया" + message + "');window.location='getData.aspx?RegId=" + UrlRedirect + "';", true);
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
        else
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Login_Default.aspx");
        }
       

    }
    public void clearAnchalaAdhikari()
    {
        ddlbhumivivadki_sanvedanshilta.SelectedIndex = 0;
        txtbaithakDate.Text = "";
        ddlIsVadiAvailable.SelectedIndex = 0;
        ddl_IsprativadiAvailable.SelectedIndex = 0;
        txtfalafal.Text = "";
        txtabhiyukt_anchaladhikari.Text = "";
        txtabhiyukt_thaanprabhaaree.Text = "";
        LandDoc.Dispose();
        ddlaction.SelectedIndex = 0;
        txtAgalaDate.Text = "";
        txtCancelReason.Text = "";
        CircleOfficer_letterOfIntent.Dispose();
        PoliceOfficer_letterOfIntent.Dispose();
    }

    #endregion

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
                                // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
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
                            //case ".jMD":
                            //case ".jpeg":
                            case ".pdf":

                                break;
                            default:
                                lblMsg.Text = "This file type is not allowed.";
                                // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
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
                                // ClientScript.ALLIMMisterStartupScript(this.GetType(), "msgFu", "alert('This file type is not allowed.');", true);
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
    public void MatterRegistrationGet() //for step=1
    {
        try
        {

            SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);
            SqlParameter _fn = new SqlParameter("@fn", "GetMatterregistration");
            DataTable dt_wadi = clsData.GetDataTableWithProc("getMatterRegisterationNVadi", new SqlParameter[] { _a_id, _fn });
            if (dt_wadi.Rows.Count > 0)
            {

                ddlSubdivision.SelectedValue = dt_wadi.Rows[0]["Sub_DivCode"].ToString();
                BindBlock();
                ddlBlock.SelectedValue = dt_wadi.Rows[0]["Block_Code"].ToString();
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
                txtPrativadiVivarani.Text = dt_wadi.Rows[0]["PrativadiVivarani"].ToString();

                if (dt_wadi.Rows[0]["Vadi_sakshya_File"].ToString() != "")
                {
                    lnkAppDoc.Visible = true;
                    lnkAppDoc.Attributes.Add("path", dt_wadi.Rows[0]["Vadi_sakshya_File"].ToString());
                }
                else
                {
                    lnkAppDoc.Visible = false;
                }
                if (dt_wadi.Rows[0]["Prativadi_sakshya_File"].ToString() != "")
                {
                    lnkPrativadiDoc.Visible = true;
                    lnkPrativadiDoc.Attributes.Add("path", dt_wadi.Rows[0]["Prativadi_sakshya_File"].ToString());
                }
                else
                {
                    lnkPrativadiDoc.Visible = false;
                }


                SqlParameter _a_id1 = new SqlParameter("@a_id", ViewState["a_id"]);
                SqlParameter _fn1 = new SqlParameter("@fn", "GetVadi");
                DataTable dtwadi = clsData.GetDataTableWithProc("getMatterRegisterationNVadi", new SqlParameter[] { _a_id1, _fn1 });
                ViewState["vadiDetails"] = dtwadi;
                wadi_grid.DataSource = dtwadi;
                wadi_grid.DataBind();
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
    public void GetKhataKhesrabhumikavivaran()
    {
        SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);

        DataTable dtbhumivivad = clsData.GetDataTableWithProc("usp_GetBhumiKaKhataKhesraKaVivaran", new SqlParameter[] { _a_id });
        ViewState["BhumiKaViVaranDT"] = dtbhumivivad;
        grd_bhumivivad.DataSource = dtbhumivivad;
        grd_bhumivivad.DataBind();
    }
    public void GetVadiPrativadiEvidanceDocument()
    {
        SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);
        SqlParameter fn = new SqlParameter("@fn", "Vadi");
        DataTable dtvadi = clsData.GetDataTableWithProc("usp_GetVadiPrativadiEvidanceDocument", new SqlParameter[] { _a_id, fn });
        ViewState["VadiEvidenceDetailsDT"] = dtvadi;
        gdVadiEvidence.DataSource = dtvadi;
        gdVadiEvidence.DataBind();

        SqlParameter _a_id1 = new SqlParameter("@a_id", ViewState["a_id"]);
        SqlParameter _fn = new SqlParameter("@fn", "Prativadi");
        DataTable dtprativadi = clsData.GetDataTableWithProc("usp_GetVadiPrativadiEvidanceDocument", new SqlParameter[] { _a_id1, _fn });
        ViewState["PrativadiEvidenceDetailsDT"] = dtprativadi;
        gdPrativadiEvidence.DataSource = dtprativadi;
        gdPrativadiEvidence.DataBind();
    }
    public void GetsavePrastuthSakchaKiVivarni()
    {
        SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);

        DataTable dtPrastuthSakchaKiVivarni = clsData.GetDataTableWithProc("usp_GetPrastutSakchaKiVivarni", new SqlParameter[] { _a_id });
        if (dtPrastuthSakchaKiVivarni.Rows.Count > 0)
        {
            txtpulis_padadhikari_vivarani.Text = dtPrastuthSakchaKiVivarni.Rows[0]["pulis_padadhikari_vivarani"].ToString();
            txthalkakarmchari_prativedan.Text = dtPrastuthSakchaKiVivarni.Rows[0]["HalkaKarmchari_vivran"].ToString();
            ddlbhukhand_mapi.SelectedValue = dtPrastuthSakchaKiVivarni.Rows[0]["vivadit_bhukhand_Mapi_ki_avashyakta_hai"].ToString();
            if (ddlbhukhand_mapi.SelectedIndex == 1)
            {

                divbhukhand_Copy.Visible = true;
                //ddlbhukhand_Copy.SelectedIndex = 0;
                ddlbhukhand_Copy.SelectedValue = dtPrastuthSakchaKiVivarni.Rows[0]["vivadit_bhukhand_Mapi"].ToString();
            }

            if (ddlbhukhand_Copy.SelectedValue == "Y")
            {
                lblbhukand_prativedan.Visible = true;
                lblbhukand_karan.Visible = false;
                file_bhukand_prativedan.Visible = true;
                Label15.Visible = true;
                txtbhukhand_reason.Visible = false;
                divMapiKeNirdharit_tithi.Visible = false;
            }
            else if (ddlbhukhand_Copy.SelectedValue == "N")
            {
                lblbhukand_prativedan.Visible = false;
                lblbhukand_karan.Visible = true;
                file_bhukand_prativedan.Visible = false;
                Label15.Visible = false;
                txtbhukhand_reason.Visible = true;
                divMapiKeNirdharit_tithi.Visible = true;
            }
            else
            {
                lblbhukand_prativedan.Visible = false;
                lblbhukand_karan.Visible = false;
                file_bhukand_prativedan.Visible = false;
                Label15.Visible = false;
                txtbhukhand_reason.Visible = false;
                divMapiKeNirdharit_tithi.Visible = false;
            }


            txtMapiKeNirdharit_tithi.Text = dtPrastuthSakchaKiVivarni.Rows[0]["maapee_ke_lie_nirdhaarit_tithi"].ToString();
            txtbhukhand_reason.Text = dtPrastuthSakchaKiVivarni.Rows[0]["vivaadit_bhukhand_Mapi_Reason"].ToString();

            if (dtPrastuthSakchaKiVivarni.Rows[0]["pulis_padadhikar_Patr_file"].ToString() != "")
            {
                lnkpulis_padadhikari_Patr_file.Visible = true;
                lnkpulis_padadhikari_Patr_file.Attributes.Add("path", dtPrastuthSakchaKiVivarni.Rows[0]["pulis_padadhikar_Patr_file"].ToString());
            }
            else
            {
                lnkpulis_padadhikari_Patr_file.Visible = false;
            }
            if (dtPrastuthSakchaKiVivarni.Rows[0]["HalkaKarmchari_Patr_file"].ToString() != "")
            {
                lnkfile_halkakarmchari_praptr.Visible = true;
                lnkfile_halkakarmchari_praptr.Attributes.Add("path", dtPrastuthSakchaKiVivarni.Rows[0]["HalkaKarmchari_Patr_file"].ToString());
            }
            else
            {
                lnkfile_halkakarmchari_praptr.Visible = false;
            }
            if (dtPrastuthSakchaKiVivarni.Rows[0]["vivaadit_bhukhand_Mapi_File"].ToString() != "")
            {
                lnkfile_bhukand_prativedan.Visible = true;
                lnkfile_bhukand_prativedan.Attributes.Add("path", dtPrastuthSakchaKiVivarni.Rows[0]["vivaadit_bhukhand_Mapi_File"].ToString());
            }
            else
            {
                lnkfile_bhukand_prativedan.Visible = false;
            }
        }
    }
    public void GetBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin()
    {
        SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);
        SqlParameter fn = new SqlParameter("@fn", "Matter");
        DataTable dtmatter = clsData.GetDataTableWithProc("usp_GetBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin", new SqlParameter[] { _a_id, fn });
        if (dtmatter.Rows.Count > 0)
        {
            dd_IsBhumiVivad.SelectedValue = dtmatter.Rows[0]["bhumi_vivad_Vivran_Available"].ToString();
            ddl_Isbhumi_Viviad_available.SelectedValue = dtmatter.Rows[0]["dispute_in_court_available"].ToString();
            if (dd_IsBhumiVivad.SelectedValue == "Y")
            {
                btnbhumivivad.Visible = true;
                btnBhumiVivadVivran1.Visible = true;
                btnBhumiVivadVivran2.Visible = true;
                btnBhumiVivadVivran3.Visible = true;
                btnBhumiVivadVivran4.Visible = true;
                btnBhumiVivadVivran5.Visible = true;
                btnBhumiVivadVivran6.Visible = true;
                SqlParameter _a_id1 = new SqlParameter("@a_id", ViewState["a_id"]);
                SqlParameter fn1 = new SqlParameter("@fn", "LandDisputeDetailsEntry");
                DataTable dtDetailsOfIncident = clsData.GetDataTableWithProc("usp_GetBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin", new SqlParameter[] { _a_id1, fn1 });
                ViewState["DetailsOfIncidentDT"] = dtDetailsOfIncident;
                grdbhumivivad.DataSource = dtDetailsOfIncident;
                grdbhumivivad.DataBind();

            }
            else
            {
                btnBhumiVivadVivran1.Visible = false;
                btnBhumiVivadVivran2.Visible = false;
                btnBhumiVivadVivran3.Visible = false;
                btnBhumiVivadVivran4.Visible = false;
                btnBhumiVivadVivran5.Visible = false;
                btnBhumiVivadVivran6.Visible = false;
                btnbhumivivad.Visible = false;
                ViewState["DetailsOfIncidentDT"] = DetailsOfIncidentDT();

            }

            if (ddl_Isbhumi_Viviad_available.SelectedValue == "Y")
            {
                btnnyayalay1.Visible = true;
                btnnyayalay3.Visible = true;
                btnnyayalay4.Visible = true;
                btnnyayalay5.Visible = true;
                btnnyayalay6.Visible = true;
                btnnyayalay7.Visible = true;
                btnnayaylaysave.Visible = true;
                SqlParameter _a_id1 = new SqlParameter("@a_id", ViewState["a_id"]);
                SqlParameter fn1 = new SqlParameter("@fn", "CourtDisputeDetailsEntry");
                DataTable dtCourtDisputeDetails = clsData.GetDataTableWithProc("usp_GetBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin", new SqlParameter[] { _a_id1, fn1 });
                ViewState["CourtDisputeDetailsDT"] = dtCourtDisputeDetails;
                grdnyayalay_vivran.DataSource = (DataTable)ViewState["CourtDisputeDetailsDT"];
                grdnyayalay_vivran.DataBind();
            }
            else
            {
                btnnyayalay1.Visible = false;
                btnnyayalay3.Visible = false;
                btnnyayalay4.Visible = false;
                btnnyayalay5.Visible = false;
                btnnyayalay6.Visible = false;
                btnnyayalay7.Visible = false;
                btnnayaylaysave.Visible = false;
                ViewState["CourtDisputeDetailsDT"] = CourtDisputeDetailsDT();
                this.BindBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
            }
        }
    }
    public void GetAnchalaAdhikari()
    {
        SqlParameter _a_id = new SqlParameter("@a_id", ViewState["a_id"]);

        DataTable dtAnchalaAdhikari = clsData.GetDataTableWithProc("usp_GetAnchalaAdhekari", new SqlParameter[] { _a_id });
        if (dtAnchalaAdhikari.Rows.Count > 0)
        {
            onestar.Visible = false;
            twostar.Visible = false;
            threestar.Visible = false;
            fourstar.Visible = false;
            if (dtAnchalaAdhikari.Rows[0]["Bhumi_savedansheelta"].ToString() != "")
            {
                ddlbhumivivadki_sanvedanshilta.SelectedValue = dtAnchalaAdhikari.Rows[0]["Bhumi_savedansheelta"].ToString();
            }
            if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "1")
            {
                onestar.Visible = true;
            }
            if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "2")
            {
                twostar.Visible = true;
            }
            if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "3")
            {
                threestar.Visible = true;
            }
            if (ddlbhumivivadki_sanvedanshilta.SelectedValue == "4")
            {
                fourstar.Visible = true;
            }
            txtbaithakDate.Text = dtAnchalaAdhikari.Rows[0]["Meeting_date"].ToString();
            ddlIsVadiAvailable.SelectedValue = dtAnchalaAdhikari.Rows[0]["Is_Vadi_Present"].ToString();
            ddl_IsprativadiAvailable.SelectedValue = dtAnchalaAdhikari.Rows[0]["Is_PratiVadi_Present"].ToString();
            txtfalafal.Text = dtAnchalaAdhikari.Rows[0]["conclusion_of_the_meeting"].ToString();
            txtabhiyukt_anchaladhikari.Text = dtAnchalaAdhikari.Rows[0]["anchala_dhikari_mantavy"].ToString();
            txtabhiyukt_thaanprabhaaree.Text = dtAnchalaAdhikari.Rows[0]["thana_prabhari_mantavy"].ToString();
            ddlaction.SelectedValue = dtAnchalaAdhikari.Rows[0]["Matter_Status"].ToString();


            divNextDateDef.Visible = true;
            // divlabNextDate.Visible = false;
            divNextDate.Visible = false;
            divCancelReason.Visible = false;
            txtAgalaDate.Text = "";
            txtCancelReason.Text = "";
            //txtAgalaDate,txtCancelReason, txtAgalaDate,labNextDate

            if (ddlaction.SelectedIndex == 1)
            {
                divNextDateDef.Visible = false;
                // divlabNextDate.Visible = true;
                divNextDate.Visible = true;
                labNextDate.Text = "प्रारंभिक निष्पादन की तिथि";
            }
            else if (ddlaction.SelectedIndex == 2)
            {
                divNextDateDef.Visible = false;
                //divlabNextDate.Visible = true;
                //labNextDate.Text = "अस्वीकृति का कारण";
                divCancelReason.Visible = true;

            }
            else if (ddlaction.SelectedIndex == 3)
            {
                divNextDateDef.Visible = false;
                //divlabNextDate.Visible = true;
                divNextDate.Visible = true;
                labNextDate.Text = "मापी की तिथि";
            }
            else if (ddlaction.SelectedIndex == 4)
            {
                divNextDateDef.Visible = false;
                //divlabNextDate.Visible = true;
                divNextDate.Visible = true;
                labNextDate.Text = "अगली सुनवाई की तिथि";
            }

            else if (ddlaction.SelectedIndex == 5)
            {
                divNextDateDef.Visible = false;
                //divlabNextDate.Visible = true;
                divNextDate.Visible = true;
                labNextDate.Text = "अंतिम निष्पादन की तिथि";
            }
            else if (ddlaction.SelectedIndex == 6)
            {
                divNextDateDef.Visible = false;
                //divlabNextDate.Visible = true;
                divNextDate.Visible = false;
                divvadkavars.Visible = true;
                labNextDate.Text = "वादी की वाद संख्या / वर्ष";
            }
            txtCancelReason.Text = dtAnchalaAdhikari.Rows[0]["reason_for_rejection"].ToString();
            txtAgalaDate.Text = dtAnchalaAdhikari.Rows[0]["mapi_ki_tithi"].ToString();
            txtAgalaDate.Text = dtAnchalaAdhikari.Rows[0]["agali_sunavaee_ki_tithi"].ToString();
            txtvadkavars.Text = dtAnchalaAdhikari.Rows[0]["vaadi_ki_vaad_sankhya_varsh"].ToString();

            if (dtAnchalaAdhikari.Rows[0]["Joint_report_SHO_Circle_Officer_file"].ToString() != "")
            {
                lnkLandDoc.Visible = true;
                lnkLandDoc.Attributes.Add("path", dtAnchalaAdhikari.Rows[0]["Joint_report_SHO_Circle_Officer_file"].ToString());
            }
            else
            {
                lnkLandDoc.Visible = false;
            }
            if (dtAnchalaAdhikari.Rows[0]["CircleOfficer_letterOfIntent"].ToString() != "")
            {
                lnkCircleOfficer_letterOfIntent.Visible = true;
                lnkCircleOfficer_letterOfIntent.Attributes.Add("path", dtAnchalaAdhikari.Rows[0]["CircleOfficer_letterOfIntent"].ToString());
            }
            else
            {
                lnkCircleOfficer_letterOfIntent.Visible = false;
            }
            if (dtAnchalaAdhikari.Rows[0]["PoliceOfficer_letterOfIntent"].ToString() != "")
            {
                lnkPoliceOfficer_letterOfIntent.Visible = true;
                lnkPoliceOfficer_letterOfIntent.Attributes.Add("path", dtAnchalaAdhikari.Rows[0]["PoliceOfficer_letterOfIntent"].ToString());
            }
            else
            {
                lnkPoliceOfficer_letterOfIntent.Visible = false;
            }
        }
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
        else if (ViewState["Step"].ToString() == "3")
        {
            step2.Visible = true;
            step3.Visible = false;
            ViewState["Step"] = "2";
            PrativadiAnyVivranGet();
        }
        else if (ViewState["Step"].ToString() == "4")
        {
            step3.Visible = true;
            step4.Visible = false;
            ViewState["Step"] = "3";
            GetKhataKhesrabhumikavivaran();
        }
        else if (ViewState["Step"].ToString() == "5")
        {
            step4.Visible = true;
            step5.Visible = false;
            ViewState["Step"] = "4";
            GetVadiPrativadiEvidanceDocument();
        }
        else if (ViewState["Step"].ToString() == "6")
        {
            step5.Visible = true;
            step6.Visible = false;
            ViewState["Step"] = "5";
            GetsavePrastuthSakchaKiVivarni();
        }
        else if (ViewState["Step"].ToString() == "7")
        {
            step6.Visible = true;
            step7.Visible = false;
            ViewState["Step"] = "6";
            GetBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
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
        encPathgov = Aes256CbcEncrypterApp.Encrypt(encPathgov, System.Web.HttpContext.Current.Session["aes256key"].ToString());
        urlpath = encPathgov;
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
        hstep4.Attributes.Add("class", hstep4.Attributes["class"].ToString().Replace("ActiveClass", "RemoveClass"));
        hstep5.Attributes.Add("class", hstep5.Attributes["class"].ToString().Replace("ActiveClass", "RemoveClass"));
        hstep6.Attributes.Add("class", hstep6.Attributes["class"].ToString().Replace("ActiveClass", "RemoveClass"));
        hstep7.Attributes.Add("class", hstep7.Attributes["class"].ToString().Replace("ActiveClass", "RemoveClass"));
        hIstep1.Style.Add("color", "lightgray");
        hIstep2.Style.Add("color", "lightgray");
        hIstep3.Style.Add("color", "lightgray");
        hIstep4.Style.Add("color", "lightgray");
        hIstep5.Style.Add("color", "lightgray");
        hIstep6.Style.Add("color", "lightgray");
        hIstep7.Style.Add("color", "lightgray");
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
            hstep4.Attributes.Add("class", hstep4.Attributes["class"].ToString().Replace("RemoveClass", "ActiveClass"));
            hIstep4.Style.Add("color", "darkblue");
        }
        else if (ViewState["Step"].ToString() == "5")
        {
            hstep5.Attributes.Add("class", hstep5.Attributes["class"].ToString().Replace("RemoveClass", "ActiveClass"));
            hIstep5.Style.Add("color", "darkblue");
        }
        else if (ViewState["Step"].ToString() == "6")
        {
            hstep6.Attributes.Add("class", hstep6.Attributes["class"].ToString().Replace("RemoveClass", "ActiveClass"));
            hIstep6.Style.Add("color", "darkblue");
        }
        else if (ViewState["Step"].ToString() == "7")
        {
            hstep7.Attributes.Add("class", hstep7.Attributes["class"].ToString().Replace("RemoveClass", "ActiveClass"));
            hIstep7.Style.Add("color", "darkblue");
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
    protected void ddlIsVadiEvi_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtVadiEvidenceType.Text = "";
        if (ddlIsVadiEvi.SelectedValue == "Y")
        {
            divVadiEvidenceType.Visible = true;
            divvadi_dastavej.Visible = true;

        }
        else if (ddlIsPvadiEvi.SelectedValue == "N" || ddlIsPvadiEvi.SelectedValue == "0")
        {
            divVadiEvidenceType.Visible = false;
            divvadi_dastavej.Visible = false;
            divtxtVadiEvidenceType.Visible = false;

        }
    }
    protected void ddlIsPvadiEvi_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPrativadiEvidenceType.Text = "";
        if (ddlIsPvadiEvi.SelectedValue == "Y")
        {
            divPrativadiEvidence.Visible = true;
            divPrativadi_dastavej_new.Visible = true;
        }
        else if (ddlIsPvadiEvi.SelectedValue == "N" || ddlIsPvadiEvi.SelectedValue == "0")
        {
            divPrativadiEvidence.Visible = false;
            divPrativadi_dastavej_new.Visible = false;
            divtxtPrativadiEvidenceType.Visible = false;
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ViewState["Step"]) == 1)
        {
            MatterRegistration();           
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
        else if (Convert.ToInt16(ViewState["Step"]) == 5)
        {
             savePrastuthSakchaKiVivarni();
        }
        else if (Convert.ToInt16(ViewState["Step"]) == 6)
        {
             SaveBhumiVivadSaGhatnaAndNayalayMePrakiriyaAadhin();
        }
        else if (Convert.ToInt16(ViewState["Step"]) == 7)
        {
             saveAnchalaAdhikari();           
        }     
        Headingstep();
    }
    protected void LinkBtnstep1_Click(object sender, EventArgs e)
    {

    }
    protected void btnclose_Click(object sender, EventArgs e)
    {

    }

}
   