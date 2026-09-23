using esms_client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LandDispute_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Utility.showMessage(Page, "OTP has been sent on  () , Please Match It.");
            //SendSMS("7482041465");
            setHidKey();
        }
    }
    public void SendSMS( string Mobile)
    {
        try
        {
            SMSHttpPostClient smsSend = new SMSHttpPostClient();         
            string message = "OTP is 1234 sent on your registered mobile number " + Mobile + " -  Home Dept. ,Bihar Government";
            // message = "OTP is 1234 sent on your registered mobile number " + Mobile + " -  Home Dept. ,Bihar Government";
            //"Dear:" + txtUserName.Text.Trim() + ",Your OTP for Mobile No Verification is " + ViewState["OTP"].ToString() + ". -Regards RCD, Govt. of Bihar.";
            string Message = smsSend.sendSingleSMS("BIHAREDISTRICT-policeerss", "erss@123456", "BRGOVT", Mobile.Trim(), message, "bac4e6cd-ac88-43bd-86f2-aa0d04171e79", "1307166261966926791");

            lbl.Text = Message+" return";

            if (Message.Contains("402"))
            {
                Utility.showMessage(this, "OTP Send Successfully at Your Registered Mobile No: " + Mobile);
               
            }
            else
            {
                
            }

        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message.ToString()+ " error";
        }
    }
    protected void sendOTP(string mob)
    {
        string Msg = string.Empty;
        string otp = "";
        string message = "";
        message = "OTP is 1234 sent on your registered mobile number " + mob + " -  Home Dept. ,Bihar Government";
       
        
        // SMSReferenceWithID.SMSWebServiceSoapClient sendSMS = new SMSReferenceWithID.SMSWebServiceSoapClient();
        string send = "";
        try
        {
            string mobile = mob;
            //string mobile = "9504245424";
            SMSReferenceWithID.SMSWebService smsSend = new SMSReferenceWithID.SMSWebService();

            send = smsSend.sendSMSSingle(message, mobile, "BIHAREDISTRICT-policeerss", "erss@123456", "bac4e6cd-ac88-43bd-86f2-aa0d04171e79", "LRD12345SMS", "1307166261966926791");
            //SMSHttpPostClient objdata= new SMSHttpPostClient(); //new implement
            ////String username, String password, String senderid, String mobileNo, String message, String secureKey, String templateid
            //send = objdata.sendUnicodeSMS("BIHAREDISTRICT-building", "abc@123456789", "1d7068a4-32b2-42f1-991a-f710f4051bfb", message, mobile, "BRGOVT", "1307169631740739971");//new implement
            Utility.showMessage(Page, "OTP has been sent on  (" + send + ") , Please Match It.");
            lbl.Text= send; 
            if (send == "Message Sent Successfully..." || send.ToString().Split(',')[0] == "402")
            {
                Utility.showMessage(Page, "OTP has been sent on  (" + mob + ") , Please Match It.");
               
            }
        }
        catch (Exception esms)
        {
            lbl.Text = esms.Message;
            Utility.showMessage(Page, "OTP not sent. Kindly Try Again...");
        }
    }


    public void setHidKey()
    {
        Random rand = new Random();
        long randnum2 = (long)(rand.NextDouble() * 900000000000000) + 8000000000000000;
        Session["hidkey"] = randnum2.ToString();
        // HidKey.Value = "8080808080808080";      
        Session["aes256key"] = Session["hidkey"].ToString() + Session["hidkey"].ToString();
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
}