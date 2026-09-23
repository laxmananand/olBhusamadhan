using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RaC_MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

        if (!IsPostBack)
        {
            if (Session["UserId"] == null || Convert.ToString(Session["UserId"]) == "")
            {
                Session.Clear();
                Session.Abandon();
                Response.Redirect("/Login_Default.aspx");


            }
            else
            {
                lblusername.Text = Session["Username"].ToString();
                lbluserid.Text = Session["UserId"].ToString();
            }
        }
    }
}
