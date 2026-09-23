using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// Summary description for CookiesManager
/// </summary>
namespace CookiesManager
{
    public class FarmerInfo
    {
        HttpCookie farmerInfo = new HttpCookie("farmerInfo");



        public string AppStatus
        {
            get
            {
                return getCookiesValue("AppStatus", false);
            }
            set
            {
                farmerInfo["AppStatus"] = ((string)value);
                //HttpContext.Current.Session["ConnStr"] = ((string)value);
            }
        }

        string getCookiesValue(string CookieName, bool IsUsehtmlEncode = true)
        {
            return farmerInfo[CookieName];
        }

        bool AddCookies()
        {

            farmerInfo.Expires.Add(new TimeSpan(0, 1, 0));
           // Response.Cookies.Add(farmerInfo);

            return true;
        }
    }
}