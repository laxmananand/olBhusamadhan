using System;
//using System.Web.UI.WebControls;
using System.Data;
using System.Text;
//using System.Collections;
using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Web.UI.HtmlControls;
public class Utility : System.Web.UI.Page
{
    public static void showMessage(Page page, string Message)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "Msg", "alert('" + Message + "');", true);
    }
    public static void showMessageNavigate(Page page, string Message, string navigateUrl)
    {
        string s = "alert('" + Message + "');var vers = navigator.appVersion;if(vers.indexOf('MSIE 7.0') != -1) { window.location.href='" + navigateUrl + "';} else{ window.location.href='" + navigateUrl + "';}";
        page.ClientScript.RegisterStartupScript(page.GetType(), "Information", s, true);
    }
    public static void showMessage(UpdatePanel updatePnl, string message) 
    {
        ScriptManager.RegisterStartupScript(updatePnl, updatePnl.GetType(), "Pop up", "alert('" + message + "');", true);
    }
}