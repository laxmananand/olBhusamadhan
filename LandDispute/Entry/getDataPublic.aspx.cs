using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LandDispute_Entry_getData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string A_ID = Request.QueryString["RegId"].ToString();

        if (!IsPostBack)
        {
            ViewState["A_ID"] = A_ID;
            A_ID = A_ID.Replace(' ', '+');
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            string a_id = enc.Decrypt(A_ID);           
        }
    }

    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), "alert('धन्यवाद');window.location='../../Default.aspx';", true);
        
    //}

    protected void btnGetData_Click(object sender, EventArgs e)
    {
        string UrlRedirect = ViewState["A_ID"].ToString();
        string strFilePath = "InformationEntry.aspx?RegId=" + UrlRedirect;
        if (Session["Role"].ToString()=="PUBLIC")
        {
            strFilePath = "PublicInformationEntry.aspx?RegId=" + UrlRedirect;            
        }
       
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + strFilePath + "','_blank')", true);
    }
}