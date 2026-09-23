using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LandDispute_Common_LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        ViewState.Clear();
 
            // 3. Clear MeriPehchaan/JanParichay cookies if set
            if (Request.Cookies["MeriPehchaanCookies"] != null)
            {
                HttpCookie meriCookie = new HttpCookie("MeriPehchaanCookies", "");
                meriCookie.Expires = DateTime.Now.AddYears(-1);
                Response.Cookies.Add(meriCookie);
            }

           
        Response.Redirect("../../Login_Default.aspx");
    }
}