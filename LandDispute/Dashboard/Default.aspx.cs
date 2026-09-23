using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LandDispute_Dashboard_Default : System.Web.UI.Page
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