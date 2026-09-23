using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EOC_dmd_floodmapdata_Map : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            txtdatefrom.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
            var year = DateTime.Now.ToString("yyyy");
            txtdatefrom.Text = DateTime.Now.ToString("dd-MM-yyyy"); 

            txtDateTo.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
            txtDateTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
    }
}