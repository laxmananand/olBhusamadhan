using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LandDispute_IDoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["url"] != null && Session["UserID"] != null)
        {
             string url = Request.QueryString["url"].ToString();
            Getpdf(url);
          
        }
        //Getpdf();
    }
    public string Getpdf( string url)
    {
        //string url = "https://bboseonline.bihar.gov.in/bboseapp/Document/TOCDocument/B66BFBAS.pdf";
        string urlpath = "";
        try
        {
            using (var webClient = new WebClient())
            {
                
                Encryptor enc = new Encryptor(Encryptor.PrivateKey);
                string pathurl = Convert.ToString(url).Trim();
                pathurl = Aes256CbcEncrypterApp.Decrypt(pathurl, System.Web.HttpContext.Current.Session["aes256key"].ToString());
                string path = enc.DecodeFrom64(pathurl);
//Response.Write(pathurl  );
                
                hdnpath.Value = url;
                byte[] imageBytes = webClient.DownloadData(path.ToString());
                string imreBase64Data = Convert.ToBase64String(imageBytes);
                string[] extention = path.Split('.');
                string extenstion = extention[extention.Length - 1];
                if (extenstion.ToLower() == "pdf")
                {
                    Response.AddHeader("Content-Type", "application/pdf");
                }
                else
                {
                    Response.AddHeader("Content-Type", "image/" + extenstion.ToLower());
                }
                Response.AddHeader("Content-Length", imreBase64Data.Length.ToString());
                Response.AddHeader("Content-Disposition", "inline;");
                Response.AddHeader("Cache-Control", "private, max-age=0, must-revalidate");
                Response.AddHeader("Pragma", "public");
                Response.BinaryWrite(Convert.FromBase64String(imreBase64Data));

                //using (var webClient = new WebClient())
                //{
                //    string[] extention = url.Split('.');
                //    string extenstion = extention[extention.Length - 1];


                //    hdnpath.Value = url;
                //    byte[] imageBytes = webClient.DownloadData(url);
                //    string imreBase64Data = Convert.ToBase64String(imageBytes);

                //    if (extenstion.ToLower() == "pdf")
                //    {
                //        Response.AddHeader("Content-Type", "application/pdf");
                //    }
                //    else
                //    {
                //        Response.AddHeader("Content-Type", "image/" + extenstion.ToLower());
                //    }

                //    Response.AddHeader("Content-Length", imreBase64Data.Length.ToString());
                //    Response.AddHeader("Content-Disposition", "inline;");
                //    Response.AddHeader("Cache-Control", "private, max-age=0, must-revalidate");
                //    Response.AddHeader("Pragma", "public");
                //    Response.BinaryWrite(Convert.FromBase64String(imreBase64Data));
                //}


            }
        }
        catch (Exception ex)
        {
            urlpath = ex.Message;
        }
        return urlpath;
    }
}