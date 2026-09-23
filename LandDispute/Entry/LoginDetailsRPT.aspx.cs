using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Data.SqlClient;
using System.IO;

public partial class LandDispute_BLKADM_test : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    static string key = Encryptor.PrivateKey.ToString();
    Encryptor enc = new Encryptor(key);
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Role"] == null )
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("../../Login_Default.aspx");
        }

        if (!IsPostBack)
        {
           
            bindCommissionary();
            if (Session["Commsionary_Code"] != null)
            {
                if (Session["Commsionary_Code"].ToString() != "")
                {
                    ddlCommissionary.SelectedValue = Session["Commsionary_Code"].ToString();
                    ddlCommissionary.Enabled = false;
                }
            }


            bindDistrict();

            if (Session["District_Code"] != null)
            {
                if (Session["District_Code"].ToString() != "")
                {
                    ddlDistrict.SelectedValue = Session["District_Code"].ToString();
                    ddlDistrict.Enabled = false;
                }
            }

            bindSubDivision();

            if (Session["Sub_DivCode"] != null)
            {
                if (Session["Sub_DivCode"].ToString() != "")
                {
                    ddlSubDivision.SelectedValue = Session["Sub_DivCode"].ToString();
                    ddlSubDivision.Enabled = false;
                }
            }

            bindBlock();

            if (Session["Block_Code"] != null)
            {
                if (Session["Block_Code"].ToString() != "")
                {
                    ddlBlock.SelectedValue = Session["Block_Code"].ToString();
                    ddlBlock.Enabled = false;
                }
            }

            bindPoliceStation();

            if (Session["Thana_Code"] != null)
            {
                if (Session["Block_Code"].ToString() != "")
                {
                    ddlPoliceStation.SelectedValue = Session["Thana_Code"].ToString();
                    if (ddlPoliceStation.SelectedValue.Trim() != "0")
                    {
                        ddlPoliceStation.Enabled = false;
                    }
                }
            }

            bindRole();
            if (Session["ID"] != null)
            {
                if (Session["RoleDesc"].ToString() != "")
                {
                    ddlRole.SelectedValue = Session["RoleDesc"].ToString();
                    if (ddlRole.SelectedValue.Trim() != "0")
                    {
                        ddlRole.Enabled = false;
                    }
                }
            }
            
            

        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (Session["Role"] != "ADMHOME" || Session["Role"] != "HQ")
        {
            GridView1.Columns[7].Visible = true;
            GridView1.Columns[10].Visible = false;
        }
        else
        {
            GridView1.Columns[7].Visible = false;
            GridView1.Columns[10].Visible = false;

        }
        bindGridData();
    }

   
    private void bindGridData()
    {
        try
        {
            Session["mySearchLoginData01"] = null;

            SqlParameter GetQueryType = new SqlParameter("@QueryType", "1");
            SqlParameter GetComm_Code = new SqlParameter("@Comm_Code", Convert.ToInt32(ddlCommissionary.SelectedValue.Trim()));
            SqlParameter GetDistrict_Code = new SqlParameter("@District_Code", Convert.ToInt32(ddlDistrict.SelectedValue.Trim()));
            SqlParameter GetSub_DivCode = new SqlParameter("@Sub_DivCode", Convert.ToInt32(ddlSubDivision.SelectedValue.Trim()));
            SqlParameter GetBlock_Code = new SqlParameter("@Block_Code", Convert.ToInt32(ddlBlock.SelectedValue.Trim()));
            SqlParameter GetThana_code = new SqlParameter("@Thana_code", Convert.ToInt32(ddlPoliceStation.SelectedValue.Trim()));
            SqlParameter Role = new SqlParameter("@Role", Convert.ToInt32(ddlRole.SelectedValue.Trim()));


            hdn1.Value = "@Comm_Code" + Convert.ToInt32(ddlCommissionary.SelectedValue.Trim()) + "@District_Code" + Convert.ToInt32(ddlDistrict.SelectedValue.Trim()) + "@Sub_DivCode" + Convert.ToInt32(ddlSubDivision.SelectedValue.Trim() )+ "@Block_Code" + Convert.ToInt32(ddlBlock.SelectedValue.Trim()) + "@Thana_code" + Convert.ToInt32(ddlPoliceStation.SelectedValue.Trim()) + "@Role"+ Convert.ToInt32(ddlRole.SelectedValue.Trim());


            DataTable dt = clsData.GetDataTableWithProc("SP_GetUserLoginDetails", new SqlParameter[] { GetQueryType, GetComm_Code, GetDistrict_Code, GetSub_DivCode, GetBlock_Code, GetThana_code, Role });
            if (dt.Rows.Count > 0)
            {
                string EPWD = string.Empty;
                string DPWD = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["pwd"] != null)
                    {
                        if (dt.Rows[i]["pwd"].ToString() != "")
                        {
                            EPWD = dt.Rows[i]["pwd"].ToString();
                            dt.Rows[i]["pwd"] = enc.Decrypt(EPWD);
                        }

                    }

                }

               Session["mySearchLoginData01"] = dt;
                GridView1.PageIndex = 0;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                Session["mySearchLoginData01"] = null;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            hdn1.Value = hdn1.Value + ex.Message;
        }


    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)Session["mySearchLoginData01"];
        }
        catch (Exception ex) { return; }
        
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = dt;
        GridView1.DataBind();

    }

    
    

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            //LinkButton lnkAreaCode = sender as LinkButton;
            //string btnName = lnkAreaCode.ID;
            LinkButton linkbtn = sender as LinkButton;
            string UrlRedirect = enc.Encrypt(linkbtn.CommandArgument);
            Response.Redirect("ViewApplicationDetails.aspx?RegId=" + UrlRedirect);

            //if (btnName.Contains("View"))
            //{
            //    string getId = lnkAreaCode.CommandArgument;




            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {



            ExportExcel(GridView1, Pnldata);
            
           
        }
        catch (Exception ee)
        {

            throw;
        }

    }
    public override void VerifyRenderingInServerForm(Control control)
    {

        /* Verifies that the control is rendered */

    }
    protected void ExportExcel(GridView Gv, Panel Pnl)
    {
        Response.ClearContent();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", DateTime.Now.ToString("ddMMyyhhmmss") + "ApplicationConsolidateRpt.xls"));
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        for (int i = 0; i < Gv.HeaderRow.Cells.Count; i++)
        {
            Gv.HeaderRow.Cells[i].Style.Add("border-style", "Solid");
            Gv.HeaderRow.Cells[i].Style.Add("border-color", "Black");
            Gv.HeaderRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            Gv.HeaderRow.Cells[i].Style.Add("Font-Bold", "True");
            Gv.HeaderRow.Cells[i].Style.Add("Fore-Color", "Black");

            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
        }
        for (int i = 0; i < GridView1.FooterRow.Cells.Count; i++)
        {
            // gvData.HeaderRow.Cells[i].Style.Add("background-color", "Green");
            Gv.FooterRow.Cells[i].Style.Add("border-style", "Solid");
            Gv.FooterRow.Cells[i].Style.Add("border-color", "Black");
            Gv.FooterRow.Cells[i].Style.Add("background-color", "DarkSeaGreen");
            Gv.FooterRow.Cells[i].Style.Add("Font-Bold", "True");
            Gv.FooterRow.Cells[i].Style.Add("Fore-Color", "Black");
        }
        int j = 1;
        //   This loop is used to apply stlye to cells based on particular row
        foreach (GridViewRow gvrow in Gv.Rows)
        {
            gvrow.BackColor = System.Drawing.Color.White;
            if (j <= GridView1.Rows.Count)
            {
                //if (j % 1 != 0)
                //{
                for (int k = 0; k < gvrow.Cells.Count; k++)
                {
                    gvrow.Cells[k].Style.Add("border-style", "Solid");
                    gvrow.Cells[k].Style.Add("border-color", "Black");
                    gvrow.Cells[k].ForeColor = System.Drawing.Color.Black;
                }
                // }
            }
            j++;
        }
        string style = @"<style> TD { mso-number-format:\@; } </style>";
        Response.Write(style);
        Pnl.RenderControl(htw);
        string dd = sw.ToString();
        dd = dd.Replace("href=", "");
        string Headrer = "User Credentials";
        Response.Write("<h3><center>" + Headrer + "</center></h3>");
        Response.Write(dd);
        Response.End();
    }
    private void bindRole()
    {
        //ddlPoliceStation.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("9"));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTable("Select Id,RoleDesc From mst_Role  WHERE ID NOT IN(13,1,2) order by ID", new SqlParameter[] { QueryType, SubDivision });
        if (dt.Rows.Count > 0)
        {
            ddlRole.DataSource = dt;
            ddlRole.DataTextField = "RoleDesc";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlRole.DataSource = null;
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    private void bindPoliceStation()
    {
        ddlPoliceStation.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("9"));
        SqlParameter _SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        SqlParameter _District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        SqlParameter _BlockCode = new SqlParameter("@BlockCode", Convert.ToInt32(ddlBlock.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, _SubDivision, _District, _BlockCode });
        if (dt.Rows.Count > 0)
        {
            ddlPoliceStation.DataSource = dt;
            ddlPoliceStation.DataTextField = "Police_Station";
            ddlPoliceStation.DataValueField = "PS_Code";
            ddlPoliceStation.DataBind();
            ddlPoliceStation.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlPoliceStation.DataSource = null;
            ddlPoliceStation.DataTextField = "Block_Name";
            ddlPoliceStation.DataValueField = "BlockCode";
            ddlPoliceStation.DataBind();
            ddlPoliceStation.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    private void bindBlock()
    {
        ddlBlock.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter SubDivision = new SqlParameter("@SubDivision", Convert.ToInt32(ddlSubDivision.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, @SubDivision });
        if (dt.Rows.Count > 0)
        {
            ddlBlock.DataSource = dt;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlBlock.DataSource = null;
            ddlBlock.DataTextField = "BlockName";
            ddlBlock.DataValueField = "BlockCode";
            ddlBlock.DataBind();
            ddlBlock.Items.Insert(0, new ListItem("All", "0"));
        }

    }

    private void bindSubDivision()
    {
        ddlSubDivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter District = new SqlParameter("@District", Convert.ToInt32(ddlDistrict.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, District });
        if (dt.Rows.Count > 0)
        {
            ddlSubDivision.DataSource = dt;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlSubDivision.DataSource = null;
            ddlSubDivision.DataTextField = "Sd_Name_En";
            ddlSubDivision.DataValueField = "Sd_Code2";
            ddlSubDivision.DataBind();
            ddlSubDivision.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    private void bindDistrict()
    {
        ddlDistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter CommissionaryCode = new SqlParameter("@CommissionaryCode", Convert.ToInt32(ddlCommissionary.SelectedValue.ToString()));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType, CommissionaryCode });
        if (dt.Rows.Count > 0)
        {
            ddlDistrict.DataSource = dt;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlDistrict.DataSource = null;
            ddlDistrict.DataTextField = "DISTRICTNAME";
            ddlDistrict.DataValueField = "DISTRICTCODE";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, new ListItem("All", "0"));
        }


    }



    private void bindCommissionary()
    {
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        DataTable dt = clsData.GetDataTableWithProc("SP_commissionary", new SqlParameter[] { QueryType });
        if (dt.Rows.Count > 0)
        {
            ddlCommissionary.DataSource = dt;
            ddlCommissionary.DataTextField = "DIVISIONAME";
            ddlCommissionary.DataValueField = "DIVISIONCODE";
            ddlCommissionary.DataBind();
            ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlCommissionary.DataSource = null;
            ddlCommissionary.DataTextField = "DIVISIONAME";
            ddlCommissionary.DataValueField = "DIVISIONCODE";
            ddlCommissionary.DataBind();
            //ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    public bool CheckNull(object myValue)
    {
        if (myValue == null)
        {
            return false;
        }

        if (myValue is DBNull)
        {
            return false;
        }

        return true;
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //Get the link button of each row
            //LinkButton lnkbutton = (LinkButton)e.Row.FindControl("lb2");
            ////Attach javascript function to each linkbutton
            //lnkbutton.Attributes.Add("onclick", "return fnLinkbutton('" + lnkbutton.ClientID + "')");
            //LinkButton lnkbutton3 = (LinkButton)e.Row.FindControl("lb3");

            //lnkbutton3.Attributes.Add("onclick", "return fnLinkbutton1('" + lnkbutton3.ClientID + "')");
            //LinkButton lnkbutton4 = (LinkButton)e.Row.FindControl("lb4");

            //Comment

            //ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            //image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");


            //ImageButton image2 = (ImageButton)e.Row.FindControl("Image2");
            //image2.Attributes.Add("onclick", "return fnLinkbutton1('" + image2.ClientID + "')");



            //ImageButton image3 = (ImageButton)e.Row.FindControl("Image3");
            //image3.Attributes.Add("onclick", "return fnLinkbutton1('" + image3.ClientID + "')");


            //ImageButton image4 = (ImageButton)e.Row.FindControl("Image4");
            //image4.Attributes.Add("onclick", "return fnLinkbutton1('" + image4.ClientID + "')");


            //ImageButton image5 = (ImageButton)e.Row.FindControl("Image5");
            //image5.Attributes.Add("onclick", "return fnLinkbutton1('" + image5.ClientID + "')");


            //ImageButton image6 = (ImageButton)e.Row.FindControl("Image6");
            //image6.Attributes.Add("onclick", "return fnLinkbutton1('" + image6.ClientID + "')");

            //Comment

            ////Attach javascript function to each linkbutton
            //lnkbutton4.Attributes.Add("onclick", "return fnLinkbutton1('" + lnkbutton4.ClientID + "')");
            //LinkButton lnkbutton5 = (LinkButton)e.Row.FindControl("lb5");
            ////Attach javascript function to each linkbutton
            //lnkbutton5.Attributes.Add("onclick", "return fnLinkbutton1('" + lnkbutton5.ClientID + "')");
            //LinkButton lnkbutton6 = (LinkButton)e.Row.FindControl("lb6");
            ////Attach javascript function to each linkbutton
            //lnkbutton6.Attributes.Add("onclick", "return fnLinkbutton1('" + lnkbutton6.ClientID + "')");
            //LinkButton lnkbutton7 = (LinkButton)e.Row.FindControl("lb7");
            ////Attach javascript function to each linkbutton
            //lnkbutton7.Attributes.Add("onclick", "return fnLinkbutton1('" + lnkbutton7.ClientID + "')");
            //LinkButton lnkbutton8 = (LinkButton)e.Row.FindControl("lb8");
            ////Attach javascript function to each linkbutton
            //lnkbutton8.Attributes.Add("onclick", "return fnLinkbutton1('" + lnkbutton8.ClientID + "')");

        }
    }
    
    protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
    {
        // bindRange();

        bindDistrict();
        bindSubDivision();
        bindBlock();
        bindPoliceStation();  
    }

    protected void ddlBlock_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindPoliceStation();
    }

    protected void ddlRole_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        bindRole();
    }
    
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubDivision();
        bindBlock();
        bindPoliceStation();
        
    }

    protected void ddlSubDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindBlock();
    }



    protected void lnkRead_Click(object sender, EventArgs e)
    {
        //int Rowindex = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
        //string filelocation = GridView1.Rows[Rowindex].Cells[21].Text;
        LinkButton lnkAreaCode = sender as LinkButton;
        string btnName = lnkAreaCode.ID;
        string getId = lnkAreaCode.CommandArgument;
        string filepath = Server.MapPath("fuIdDocumentLD212095111.pdf");
            WebClient user = new WebClient();
        Byte[] FileBuffer = user.DownloadData(filepath);
        if (FileBuffer != null)
        {
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-length", FileBuffer.Length.ToString());
            Response.BinaryWrite(FileBuffer);
        }


    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Check if the row is datarow
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //Get the link button of each row
        //    LinkButton lnkbutton = (LinkButton)e.Row.FindControl("lb1");
        //    //Attach javascript function to each linkbutton
        //    lnkbutton.Attributes.Add("onclick", "return fnLinkbutton('" + lnkbutton.ClientID + "')");
            
        //}
    }

//    private void WinOpen(string msg)
//    {
//        string strScript = string.Format("window.open('" + msg + "','name','type=resizable=Yes,height=900,width=700,toolbar=0,addressbar =0, scrollbars=yes');", msg);
//        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "strScript", strScript, true);
//    }
//    private string Showletter(string UploadLetter)
//    {
//        var path = "http://localhost:63356/LDHOME/LandDispute/BLKADM/" + UploadLetter;   //fuIdDocumentLD212095111.pdf
//        return path;
//    }

//    protected void btnShow_Click(object sender, EventArgs e)
//    {
//        LinkButton btn = sender as LinkButton;
//        string path = btn.CommandArgument;
//        WinOpen(Showletter(path));       
//    }


}