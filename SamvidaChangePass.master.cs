using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Samvida : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");    
       
        if (!IsPostBack)
        {
            if (Session["UserId"] == null)
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("../../DeptDefault.aspx");
            }
        }        
    }   
}
