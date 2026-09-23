using System;
using System.Web.UI;


public class SwitchCulture : Page
{
    protected override void InitializeCulture()
    {
        try
        {
            if (Session["Culture"] != null)
            {
                Culture = Convert.ToString(Session["Culture"]);
                UICulture = Convert.ToString(Session["Culture"]);
            }
            else
            {
                ////Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            ex.ToString();

        }
        base.InitializeCulture();
    }
}
//8579823365