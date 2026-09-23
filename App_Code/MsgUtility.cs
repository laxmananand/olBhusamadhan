using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MsgUtility
/// </summary>
public class MsgUtility
{
	public MsgUtility()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string UnicodeHexadecimal(string input)
    {
        string result = string.Empty;
        foreach (char c in input)
        {
            int tmp = c;
            result += String.Format("{0:x4}", (uint)System.Convert.ToUInt32(tmp.ToString()));
        }
        result = result.ToUpper();
        return result;
    }

    public static void showMessage(Page page, string Message)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "Msg", "alert('" + Message + "');", true);
    }
    public static void showMessageNavigate(Page page, string Message, string navigateUrl)
    {
        string s = "alert('" + Message + "');var vers = navigator.appVersion;if(vers.indexOf('MSIE 7.0') != -1) { window.location.href='" + navigateUrl + "';} else{ window.location.href='" + navigateUrl + "';}";
        page.ClientScript.RegisterStartupScript(page.GetType(), "Information", s, true);

    }


    public static string Base64Encode(string str)
    {
        byte[] encbuff = Encoding.UTF8.GetBytes(str);
        return HttpServerUtility.UrlTokenEncode(encbuff);
    }

    public static string Base64Decode(string str)
    {
        byte[] decbuff = HttpServerUtility.UrlTokenDecode(str);
        return Encoding.UTF8.GetString(decbuff);
    }
}