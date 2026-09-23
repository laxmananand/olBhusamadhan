using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Security.Application;

public partial class LDHD_UserApplicationStatus : System.Web.UI.Page
{
    string sqlQuery = "";
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    Encryptor enc = new Encryptor(Encryptor.PrivateKey);
    string BenId = "";
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            //Session["Application_ID"] = "LD212124065";
            
            if (Session["Application_ID"] != null && Session["Application_ID"].ToString().Trim().Length > 0)
            {
                string txtApplicationNo = "";
                txtApplicationNo = Session["Application_ID"].ToString();
                GetApplicantDetail(txtApplicationNo);

            }
            else
            {
                Utility.showMessage(this.Page, "Please Select Application To View Status.");
                Response.Redirect("default.aspx");
            }

          

        }
        
    }



    void GetApplicantDetail(string txtApplicationNo)
    {
        PnlGrid.Visible = false;
        string sqlQuery = string.Empty;
        SqlParameter filter = new SqlParameter("@filter", '1');
        //SqlParameter ApplicationNo = new SqlParameter("@ApplicationNo", txtregno.Text.Trim());
        SqlParameter ApplicationNo = new SqlParameter("@ApplicationNo", txtApplicationNo);
        DataTable dt = clsData.GetDataTableWithProc("Sp_GetRecords", new SqlParameter[] { filter, ApplicationNo });

            if (dt.Rows.Count > 0)
            {
                PnlGrid.Visible = true;
                DataRow r;
                r = dt.Rows[0];

                DvApplicationId.InnerText = r["ApplicationNo"].ToString();
                dvStudentName.InnerText = r["UserName"].ToString(); //+ " [" + r["user_id"].ToString() + "]";
                dvFather.InnerText = r["Father_Husband_Name"].ToString();
                dvMother.InnerText = r["MotherName"].ToString();
                dvDob.InnerText = Convert.ToDateTime(r["YearOfBirthAsPerAadhaar"]).ToString("dd MMM yyyy");
                dvGender.InnerText = r["Gender"].ToString();
                //dvCategory.InnerText = r["category"].ToString();
                //dvEmail.InnerText = r["emailid"].ToString();
                dvMobile.InnerText = r["MobileNo"].ToString();
                var aaInput = r["AadharNo"].ToString();

                var aalength = aaInput.Length;
                var NewAadhar = new String('X', aalength - 4) + aaInput.Substring(aalength - 4);
                dvAadhar.InnerText = NewAadhar;//r["AadharNo"].ToString();
               
                dvHomeDist.InnerText = r["DISTRICTNAME"].ToString();
               
                dvPerAdd.InnerText = r["Address"].ToString();
                dvPoliceStation.InnerText = r["Police_Station"].ToString();

                dvHomeBlock.InnerHtml = r["BlockName"].ToString();
                dvPanchayatName.InnerHtml = r["PanchayatName"].ToString();
                dvVillage.InnerHtml = r["Village"].ToString();
                dvWard.InnerHtml = r["WARDNAME"].ToString();
                dvAreaType.InnerHtml = r["AreaType"].ToString();

                dvkhataNo.InnerHtml = r["khataNo"].ToString();
                dvkhesraNo.InnerHtml = r["khesraNo"].ToString();
                dvLandDisputeType.InnerHtml = r["LandDisputeType"].ToString();
                dvSensitivity.InnerHtml = r["SensitivityType"].ToString();
                dvLandDisputeDescription.InnerHtml = r["LandDisputeDescription"].ToString();

                dvSentTo.InnerHtml = r["Matter_current_forward_status"].ToString();

              

              

                //aaInput = r["BankAccount"].ToString();
                //aalength = aaInput.Length;
                //var NewAC= new String('X', aalength - 4) + aaInput.Substring(aalength - 4);
                dvdateOnTitle.InnerHtml = "";
                dvStatusRemarks.InnerHtml = r["Remarks"].ToString();
                dvdateOnTitle.InnerHtml = r["Matter_Rejected_date"].ToString() == "" ? "Resolved On:" : "Rejected On:";
                dvOndate.InnerHtml = r["Matter_Rejected_date"].ToString() == "" ? r["Matter_Rsolved_date"].ToString() : r["Matter_Rejected_date"].ToString();

                //dvBankAC.InnerText = NewAC;// r["BankAccount"].ToString();
                
                //Institution Verification
                //dvInstStatus.InnerHtml = Convert.ToString(r["IsInstitutionVerify"]) == "Accepted" ? "<span class='btn btn-success cert'> Verified</span>" : "<span class='btn btn-danger cert'> " + Convert.ToString(r["IsInstitutionVerify"]) + "</span>";
                //dvInstStatusRemarks.InnerText = Convert.ToString(r["InstitutionVerifyRemarks"]);
                //Physical Verification
                dvMatterStatus.InnerHtml = Convert.ToString(r["Matter_status"]) == "Resolved" ? "<span class='btn btn-success cert'> Resolved</span>" : "<span class='btn btn-danger cert'> " + Convert.ToString(r["Matter_status"]) + "</span>";
                //dvTPAStatusRemarks.InnerText = Convert.ToString(r["Matter_current_forward_status"]);
                
                //Comittee Verification
                //dvCommitteeStatus.InnerHtml = Convert.ToString(r["CommitteeStatus"]) == "Accepted" ? "<span class='btn btn-success cert'> Verified</span>" : "<span class='btn btn-danger cert'> " + Convert.ToString(r["CommitteeStatus"]) + "</span>";
                //dvCommitteeStatusRemarks.InnerText = Convert.ToString(r["CommitteeStatusRemarks"]) ;

                //Verifier Verification
                //dvVerifier.InnerHtml = Convert.ToString(r["VerifierStatus"]) == "Accepted" ? "<span class='btn btn-success cert'> Verified</span>" : "<span class='btn btn-danger cert'> " + Convert.ToString(r["VerifierStatus"]) + "</span>";
                //dvVerifierRemarks.InnerText = Convert.ToString(r["VerifierStatusRemarks"]) ;      



                if (r["Application_Upload_url"].ToString().Trim() != "")
                {
                    //string encPath = enc.EncodeTo64(r["Application_Upload_url"].ToString().Replace("http://10.133.24.51", "~"));
                    //lnkTenthCertNo.HRef = "IDoc.aspx?PATH=" + encPath;
                    string Path = r["Application_Upload_url"].ToString();
                    lnkTenthCertNo.HRef = "IDoc.aspx?PATH=" + Path;
                    lnkTenthCertNo.InnerText = "Application";
                    lnkTenthCertNo.Target = "_blank";
                    lnkTenthCertNo.Visible = true;
                    dlnkTenthCertNo.Style.Add("display", "block");
                }

                if (r["Document_Upload_Url"].ToString().Trim() != "")
                {
                    //string encPath = enc.EncodeTo64(r["Document_Upload_Url"].ToString().Replace("http://10.133.24.51", "~"));
                    //lnkTwelfthDoc.HRef = "IDoc.aspx?PATH=" + encPath;
                    string Path = r["Document_Upload_Url"].ToString();
                    lnkTwelfthDoc.HRef = "IDoc.aspx?PATH=" + Path;
                    lnkTwelfthDoc.InnerText = "Document";
                    lnkTwelfthDoc.Target = "_blank";
                    lnkTwelfthDoc.Visible = true;
                    dlnkTwelfthDoc.Style.Add("display", "block");
                }

               

            }
        
    }

    
}