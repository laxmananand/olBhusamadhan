<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        Application["HitCount"] = 0;

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started
        Application.Lock();
        Application["HitCount"] = Application["HitCount"] != null ? Convert.ToInt32(Application["HitCount"]) + 1 : 1;
        Application["OnlineUsers"] = Application["OnlineUsers"] != null ? (int)Application["OnlineUsers"] + 1 : 1;
        // Application["ActiveUsers"] = System.Convert.ToInt32(Application["ActiveUsers"]) + 1;
        Application.UnLock();
        Session["Culture"] = "hi-IN";
    }

    void Application_End(object sender, EventArgs e)
    {


    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException();
        string err = "Error in: " + Request.Url.ToString() + ". Error Message:" + objErr.Message.ToString();
        //Email email = new Email();
        //email.SendMailOld("Eservices Error", err, "amitkumarmca3@gmail.com", "sk.shrivastava@nic.in", "sk.shrivastava@nic.in", "", false);

        //SMSServices.UserCredentials Credential = new SMSServices.UserCredentials();
        //Credential.userName = "12345";
        //Credential.password = "67890";
        //SMSServices.SMSService SMS = new SMSServices.SMSService();
        //SMS.UserCredentialsValue = Credential;

        // Log the error

     Response.Redirect("~/Error.aspx");

    }

    // void Session_Start(object sender, EventArgs e)
    // {
    //    // Code that runs when a new session is started


    //  }

    void Session_End(object sender, EventArgs e)
    {

        Application.Lock();
 Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;
 Application.UnLock();

 string userID = Session["User_ID"] as string;
 if (userID == null) { return; }
 System.Collections.Generic.Dictionary<string, string> activeSessions = Application["ActiveSessions"] as System.Collections.Generic.Dictionary<string, string>;

 if (activeSessions != null && activeSessions.ContainsKey(userID))
 {
     activeSessions.Remove(userID);
 }
 //end 060824
    }


    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.User.Identity is FormsIdentity)
                {
                    FormsIdentity id =
                        (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = id.Ticket;

                    // Get the stored user-data, in this case, our roles
                    string userData = ticket.UserData;
                    string[] roles = userData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                }
            }
        }

    }

    //Start kiran 05112024

    protected void Application_presendRequestHeaders(object sender, EventArgs e)
    {


string path = Request.Path.ToLower();
   if (path.EndsWith(".css") || path.EndsWith(".js") ||
        path.EndsWith(".jpg") || path.EndsWith(".jpeg") ||
        path.EndsWith(".png") || path.EndsWith(".gif") ||
        path.EndsWith(".svg") || path.EndsWith(".webp") )
    {
        Response.Cache.SetExpires(DateTime.UtcNow.AddDays(30));
        Response.Cache.SetCacheability(HttpCacheability.Public);
        Response.Cache.SetValidUntilExpires(true);
    }
else
{
Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-10));
        Response.Cache.SetNoStore();
}







    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-10));
    Response.Cache.SetNoStore();



    // HttpContext.Current.Response.Headers.Remove("Server");

    //Response.AddHeader("X-Frame-Options", "SAMEORIGIN");
    Response.AddHeader("X-Frame-Options", "DENY");
    //Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
    // Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
    //Response.AppendHeader("Expires", "-1"); // Proxies.
    var application = sender as HttpApplication;
    //if (application != null && application.Context != null)
    //{
    //    application.Context.Response.Headers.Remove("Server");
    //}
    //HttpContext.Current.Response.Headers.Remove("Server");

    if (HttpContext.Current != null)
    {
    
        HttpResponse htpr = HttpContext.Current.Response;
        if (htpr != null)
        {
            try
            {
                var _header = htpr.Headers;
                if (_header != null)
                {
                    //_header.Remove("X-AspNet-Version");
                    ////   _header.Remove("SAMEORIGIN");
                    //_header.Remove("X-Powered-By");
                    _header.Remove("Server");
                    // _header.Set("Server", "xxx");                   
                    _header.Set("MicrosoftOfficeWebServer", "xxx");
                    _header.Set("MS-Author-Via", "xxx");
                    _header.Set("X-AspNet-Version", "xxx");
                    _header.Set("X-Powered-By", "xxx");
                    // _header.Remove("Server");
                    _header.Remove("X-AspNet-Version"); //alternative to above solution
                    _header.Remove("X-AspNetMvc-Version"); //alternative to above 
    
                    _header.Remove("MicrosoftOfficeWebServer");
                    _header.Remove("MS-Author-Via");
                    _header.Remove("X-AspNet-Version");
                    _header.Remove("X-Powered-By");
                    _header.Remove("X-AspNetMvc-Version");
                    _header.Remove("ETag");
    
                }
    
    
            }
            catch { }
        }
        }
           

    }

    protected void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
    {
        System.Web.Configuration.SessionStateSection sessionState = (System.Web.Configuration.SessionStateSection)ConfigurationManager.GetSection("system.web/sessionState");
        if (sessionState != null)
        {
            //sessionState.CookieSecure = HttpCookieSecurePolicy.Always; // This sets the Secure flag
            HttpCookie sessionCookie = Request.Cookies[sessionState.CookieName];
            if (sessionCookie != null)
            {
                //if(HttpContext.Current.Session!=null) sessionCookie.Value = HttpContext.Current.Session.SessionID;
                sessionCookie.HttpOnly = true;
                sessionCookie.Secure = true;
                sessionCookie.Path = "/landdispute";
                //sessionCookie.Path = Request.Url.AbsolutePath;
                sessionCookie.Path = "/";
                 sessionCookie.SameSite = SameSiteMode.Strict;
                //sessionCookie.SameSite = System.Web.SameSiteMode.Lax;
                sessionCookie.Expires = DateTime.Now.AddMinutes(30);
                sessionCookie.Domain = HttpContext.Current.Request.Url.Host;
                Response.AppendHeader(sessionCookie.Name, "SameSite=Lax; HttpOnly; Secure");
                Response.Cookies.Set(sessionCookie);
            }
        }

    }

 protected void Application_BeginRequest()
{

    Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
    Response.AddHeader("Pragma", "no-cache");
    Response.AddHeader("Expires", "0");

}


void Application_BeginRequest(Object sender, EventArgs e)
{

    HttpContext context = HttpContext.Current;

    // Get the requested URL
    string url = context.Request.Url.ToString();

    Uri uri = new Uri(url);

    // Get the absolute path from the Uri
    string path = uri.AbsolutePath;

    // Extract the file name from the path
    string fileName = System.IO.Path.GetFileName(path);

    Uri referrer = context.Request.UrlReferrer;

   
   

}
</script>
