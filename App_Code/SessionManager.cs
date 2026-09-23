using System.Web;
using System.Web.Services;
public class SessionManager
{

    public static void Clear()
    {
        SessionManager.UserID = null;
        SessionManager.UserRole = null;
        SessionManager.UserName = null;
    }

    [WebMethod]
    public void CheckSession()
    {
        

    }
    public static bool IsSessionOut()
    {
        if (HttpContext.Current.Session == null)
            return true;
        else
        {
            //if (UseRole.isADM())
            //{
            //    if (ConnStr.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0)
            //        return false;
            //    else
            //        return true;
            //}
            //else 
            if (UseRole.isCND())
            {
                if (UserID.Length > 0 && ConnStr.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0)
                    return false;
                else
                    return true;
            }
            else if (UseRole.isDEV())
            {
                if (UserID.Length > 0 && ConnStr.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0)
                    return false;
                else
                    return true;
            }
            else if (UseRole.isHelpDesk())
            {
                if (UserID.Length > 0 && ConnStr.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0)
                    return false;
                else
                    return true;
            }
            else if (UseRole.isDPT())
            {
                if (UserID.Length > 0 && ConnStr.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0)
                    return false;
                else
                    return true;
            }
            else if (UseRole.isHQ())
            {
                if (ConnStr.Length > 0 && DeptId.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0 && UserID.Length > 0 && UserRole.Length > 0 && UserName.Length > 0)
                    return false;
                else
                    return true;
            }
            else if (UseRole.isDSTADM() || UseRole.isDSTOPT())
            {
                if (ConnStr.Length > 0 && DeptId.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0 && DistrictCode.Length > 0 && DistrictName.Length > 0 && UserID.Length > 0 && UserRole.Length > 0 && UserName.Length > 0)
                    return false;
                else
                    return true;
            }
            else if (UseRole.isBLKADM() || UseRole.isBLKOPT())
            {
                if (ConnStr.Length > 0 && DeptId.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0 && DistrictCode.Length > 0 && DistrictName.Length > 0 && BlockCode.Length > 0 && BlockName.Length > 0 && UserID.Length > 0 && UserRole.Length > 0)
                    return false;
                else
                    return true;
            }
            else if (UseRole.isHOSADM() || UseRole.isHOSOPT())
            {
                if (ConnStr.Length > 0 && DeptId.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0 && DistrictCode.Length > 0 && DistrictName.Length > 0 && BlockCode.Length > 0 && BlockName.Length > 0 && UserID.Length > 0 && UserRole.Length > 0)//&& UserName.Length > 0
                    return false;
                else
                    return true;
            }
            else
            {
                if (ConnStr.Length > 0 && DeptId.Length > 0 && DeptCode.Length > 0 && DeptName.Length > 0 && DeptNameRef.Length > 0 && DistrictCode.Length > 0 && DistrictName.Length > 0 && BlockCode.Length > 0 && BlockName.Length > 0 && UserID.Length > 0 && UserRole.Length > 0)//&& UserName.Length > 0
                    return false;
                else
                    return true;
            }
        }
    }
    public static string ConnStr
    {
        get
        {
            return getSession("ConnStr", false);
        }
        set
        {
            HttpContext.Current.Session["ConnStr"] = ((string)value);
        }
    }
    public static string DeptId
    {
        get
        {
            return getSession("DeptId");
        }
        set
        {
            HttpContext.Current.Session["DeptId"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string DeptCode
    {
        get
        {
            return getSession("DeptCode");
        }
        set
        {
            HttpContext.Current.Session["DeptCode"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string DeptName
    {
        get
        {
            return getSession("DeptName", false);
        }
        set
        {
            HttpContext.Current.Session["DeptName"] = ((string)value);
        }
    }
    public static string DeptNameRef
    {
        get
        {
            return getSession("DeptNameRef", false);
        }
        set
        {
            HttpContext.Current.Session["DeptNameRef"] = ((string)value);
        }
    }
   
    public static string DistrictCode
    {
        get
        {
            return getSession("DistrictCode");
        }
        set
        {
            HttpContext.Current.Session["DistrictCode"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string DistrictName
    {
        get
        {
            return getSession("DistrictName", false);
        }
        set
        {
            HttpContext.Current.Session["DistrictName"] = ((string)value);
        }
    }
    public static string BlockCode
    {
        get
        {
            return getSession("BlockCode");
        }
        set
        {
            HttpContext.Current.Session["BlockCode"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string BlockName
    {
        get
        {
            return getSession("BlockName", false);
        }
        set
        {
            HttpContext.Current.Session["BlockName"] = ((string)value);
        }
    }
    public static string BlockCode_O
    {
        get
        {
            return getSession("BlockCode_O");
        }
        set
        {
            HttpContext.Current.Session["BlockCode_O"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    //public static string HostalCode
    //{
    //    get
    //    {
    //        return getSession("HostalCode");
    //    }
    //    set
    //    {
    //        HttpContext.Current.Session["HostalCode"] = ApplicationModule.htmlEncode((string)value);
    //    }
    //}
    //public static string HostalName
    //{
    //    get
    //    {
    //        return getSession("HostalName");
    //    }
    //    set
    //    {
    //        HttpContext.Current.Session["HostalName"] = ApplicationModule.htmlEncode((string)value);
    //    }
    //}
    public static string PanchayatCode
    {
        get
        {
            return getSession("PanchayatCode");
        }
        set
        {
            HttpContext.Current.Session["PanchayatCode"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string UserID
    {
        get
        {
            return getSession("UserID");
        }
        set
        {
            HttpContext.Current.Session["UserID"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string UserName
    {
        get
        {
            return getSession("UserName",false);
        }
        set
        {
            HttpContext.Current.Session["UserName"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string UserRole
    {
        get
        {
            return getSession("Role");
        }
        set
        {
            HttpContext.Current.Session["Role"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string RoleDescription
    {
        get
        {
            return getSession("RoleDescription");
        }
        set
        {
            HttpContext.Current.Session["RoleDescription"] = ((string)value);
        }
    }
    public static string SanctionedLength
    {
        get
        {
            return getSession("SanctionedLength");
        }
        set
        {
            HttpContext.Current.Session["SanctionedLength"] = ApplicationModule.htmlEncode((string)value);
        }
    }
    public static string HostalType
    {
        get
        {
            return getSession("HostalType", false);
        }
        set
        {
            HttpContext.Current.Session["HostalType"] = ((string)value);
        }
    }
    public static string IsHostal
    {
        get
        {
            return getSession("IsHostal", false);
        }
        set
        {
            HttpContext.Current.Session["IsHostal"] = ((string)value);
        }
    }



    public static string ClientIp
    {
        get
        {
            return getSession("ClientIp");
        }
        set
        {
            HttpContext.Current.Session["ClientIp"] = ApplicationModule.htmlEncode((string)value);
        }
    }

    static string getSession(string SessionName, bool IsUsehtmlEncode = true)
    {
        if (HttpContext.Current.Session != null && HttpContext.Current.Session[SessionName] != null)
        {
            if (IsUsehtmlEncode)
                return ApplicationModule.htmlEncode((string)HttpContext.Current.Session[SessionName]);
            else
                return ((string)HttpContext.Current.Session[SessionName]);
        }
        else
            return "";
    }
}


public class UseRole
{
    public static string DEPT = "DPT";
    public static string HQ = "HQ";
    public static string ADM = "ADM";
    public static string DEV = "DEV";
    public static string HELPDESK = "HELPDESK";
    public static string BLKADM = "BLKADM";
    public static string BLKOPT = "BLKOPT";
    public static string DSTADM = "DSTADM";
    public static string DSTOPT = "DSTOPT";
    public static string HOSAPV = "HOSAPV";
    public static string HOSADM = "HOSADM";
    public static string HOSOPT = "HOSOPT";
    public static string CND = "CND";

    //----------------------------------------------------------------------------------------------
    public static bool isCND()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.CND);
    }
    public static bool isDPT()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.DEPT);
    }
    public static bool isHQ()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.HQ);
    }
    public static bool isADM()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.ADM);
    }
    public static bool isDEV()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.DEV);
    }
    public static bool isHelpDesk()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.HELPDESK);
    }

    //----------------------------------------------------------------------------------------------
    public static bool isDSTOPT()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.DSTOPT);
    }
    public static bool isDSTADM()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.DSTADM);
    }

    //----------------------------------------------------------------------------------------------
    public static bool isOPT()
    {
        bool retVal = false;
        if (SessionManager.UserRole != null)
        {
            retVal = (SessionManager.UserRole == UseRole.BLKOPT);
            if (!retVal)
                retVal = (SessionManager.UserRole == UseRole.HOSOPT);
        }
        return retVal;
    }
    public static bool isOPTADM()
    {
        bool retVal = false;
        if (SessionManager.UserRole != null)
        {
            retVal = (SessionManager.UserRole == UseRole.BLKADM);
            if (!retVal)
                retVal = (SessionManager.UserRole == UseRole.HOSADM);
        }
        return retVal;
    }
    public static bool isAPV()
    {
        bool retVal = false;
        if (SessionManager.UserRole != null)
        {
            retVal = (SessionManager.UserRole == UseRole.DSTOPT);
        }
        return retVal;
    }
    public static bool isAPVADM()
    {
        bool retVal = false;
        if (SessionManager.UserRole != null)
        {
            retVal = (SessionManager.UserRole == UseRole.DSTADM);
        }
        return retVal;
    }

    //----------------------------------------------------------------------------------------------
    public static bool isBLKOPT()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.BLKOPT);
    }
    public static bool isBLKADM()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.BLKADM);
    }
    public static bool isHOSOPT()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.HOSOPT);
    }
    public static bool isHOSADM()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.HOSADM);
    }
    public static bool isHOSAPV()
    {
        if (SessionManager.UserRole == null)
            return false;
        else
            return (SessionManager.UserRole == UseRole.HOSAPV);
    }
    //----------------------------------------------------------------------------------------------
}


