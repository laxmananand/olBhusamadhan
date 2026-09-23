using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Security.Cryptography;
using System.Net;

/// <summary>
/// Summary description for cls_ApplicationModule
/// </summary>

public class ApplicationModule
{
    static DESCryptoServiceProvider key = new DESCryptoServiceProvider();
    static string PrivateKey = "LlU9JJLJAu8=";

    public static string htmlEncode(string _Val)
    {
        if (_Val == null)
            return "";
        else
            return Microsoft.Security.Application.Encoder.HtmlEncode(_Val.Trim());
    }
    public static string htmlEncode(object _Val)
    {
        return Microsoft.Security.Application.Encoder.HtmlEncode(Convert.ToString(_Val).Trim());
    }
    //public static string htmlEncode(object _Val)
    //{
    //    return Microsoft.Security.Application.Encoder.Html(Convert.ToString(_Val).Trim());
    //}
    public static void showMessage(Page page, string Message)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "Msg", "alert('" + Message + "');", true);
    }
    public static void showMessageNavigate(Page page, string Message, string navigateUrl)
    {
        string s = "alert('" + Message + "');var vers = navigator.appVersion;if(vers.indexOf('MSIE 7.0') != -1) { window.location.href='" + navigateUrl + "';} else{ window.location.href='" + navigateUrl + "';}";
        page.ClientScript.RegisterStartupScript(page.GetType(), "Information", s, true);

    }
    public static string GetClientIp()
    {
        IPAddress[] arrIPAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList;
        if (arrIPAddress.Length > 1)
            return arrIPAddress[1].ToString();
        else if (arrIPAddress.Length > 0)
            return arrIPAddress[0].ToString();
        else
            return "";
    }
    
    public static string Encrypt(string PlainText)
    {
        key.IV = Convert.FromBase64String("x7hkVXoObeI=");
        key.Key = Convert.FromBase64String(PrivateKey);
        MemoryStream ms = new MemoryStream();
        CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);
        StreamWriter sw = new StreamWriter(encStream);
        sw.WriteLine(PlainText);
        sw.Close();
        encStream.Close();
        byte[] buffer = ms.ToArray();
        ms.Close();

        return Convert.ToBase64String(buffer);
    }
    public static string Decrypt(string EncryptText)
    {
        key.IV = Convert.FromBase64String("x7hkVXoObeI=");
        key.Key = Convert.FromBase64String(PrivateKey);

        byte[] bText = Convert.FromBase64String(EncryptText);
        MemoryStream ms = new MemoryStream(bText);
        CryptoStream encStream = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(encStream);
        string val = sr.ReadLine();
        sr.Close();
        encStream.Close();
        ms.Close();
        return val;
    }

}
