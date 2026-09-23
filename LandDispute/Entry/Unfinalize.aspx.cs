using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LandDispute_Entry_Unfinalize : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    whiteList wl = new whiteList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"].ToString()!=null)
        {
            if (Session["Role"].ToString() == "SHOOPT" || Session["Role"].ToString() == "COOPT")
            {
                if (Session["District_Code"] != null)
                {
                    if (!IsPostBack)
                    {
                        GV_GetThanaDeatilsPageWise(1);
                    }
                }
                else
                {
                    Response.Redirect("~/DeptDefault.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("~/DeptDefault.aspx");
        }
    }

    protected void PageSize_Changed(object sender, EventArgs e)
    {
        this.GV_GetThanaDeatilsPageWise(1);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GV_GetThanaDeatilsPageWise(pageIndex);
    }
    private void GV_GetThanaDeatilsPageWise(int pageIndex)
    {
        DataTable dt = new DataTable();
        SqlParameter[] p = new SqlParameter[6];
        try
        {
            p[0] = new SqlParameter("@PageIndex", pageIndex);
            p[1] = new SqlParameter("@PageSize", int.Parse(ddlPageSize.SelectedValue));


            p[2] = new SqlParameter("@RecordCount", SqlDbType.Int, 4);
            p[2].Direction = System.Data.ParameterDirection.Output;

            p[3] = new SqlParameter("@Thana_code ", Session["Thana_Code"].ToString());
            p[4] = new SqlParameter("@search", txtSearch.Text.ToString());
            p[5] = new SqlParameter("@Block_code", Session["Block_code"].ToString());

            dt = clsData.GetDataTableWithProc("usp_GetSaveDataUnFinalize", p);
            int totalRecord = 0;
            if (p[2].Value != null)
            {
                int.TryParse(p[2].Value.ToString(), out totalRecord);
            }
            grdMatterRegistration.DataSource = dt;
            grdMatterRegistration.DataBind();
            int recordCount = Convert.ToInt32(p[2].Value);
            this.PopulatePager(recordCount, pageIndex);
        }
        catch (Exception ex)
        {

        }
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(ddlPageSize.SelectedValue));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", currentPage > 1));
            for (int i = 1; i <= pageCount; i++)
            {
                pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }







    private string GetUserIP()
    {
        string iMainpaddress = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(iMainpaddress))
        {
            return iMainpaddress.Split(',')[0];
        }

        return Request.ServerVariables["REMOTE_ADDR"];
    }




    protected void grdMatterRegistration_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
      protected void lnkView_Click(object sender, EventArgs e)
  {
      try
      {
          Encryptor enc = new Encryptor(Encryptor.PrivateKey);

          LinkButton linkbtn = sender as LinkButton;
          string UrlRedirect = linkbtn.CommandArgument;

          // Check Session
          if (Session["Thana_Code"] == null ||
              string.IsNullOrWhiteSpace(Session["Thana_Code"].ToString()))
          {
              Response.Redirect("Entry_Page_com.aspx?RegId=" + UrlRedirect, false);
          }
          else
          {
              Response.Redirect("Entry_Page.aspx?RegId=" + UrlRedirect, false);
          }

          Context.ApplicationInstance.CompleteRequest();
      }
      catch (Exception ex)
      {
          Response.Write(ex.Message.ToString());
      }
  }
  //protected void lnkView_Click(object sender, EventArgs e)
  //{
  //    try
  //    {
  //        Encryptor enc = new Encryptor(Encryptor.PrivateKey);

  //        LinkButton linkbtn = sender as LinkButton;
  //        string UrlRedirect = linkbtn.CommandArgument;

  //        Response.Redirect("Entry_Page.aspx?RegId=" + UrlRedirect, false);
  //        Context.ApplicationInstance.CompleteRequest();
  //    }
  //    catch (Exception ex)
  //    {
  //        Response.Write(ex.Message.ToString());
  //    }
  //}
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        GV_GetThanaDeatilsPageWise(1);
    }
}