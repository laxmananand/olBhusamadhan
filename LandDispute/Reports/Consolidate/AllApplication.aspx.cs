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





public partial class LandDispute_THANA_Thana_Entry : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    static string key = Encryptor.PrivateKey.ToString();
    Encryptor enc = new Encryptor(key);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Role"] != null && Session["Role"].ToString() != "")
            {
                BindRole();
                divmsg.Visible= false;
            }
        }
    }
    private void BindRole()
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
                ddldistrict.SelectedValue = Session["District_Code"].ToString();
                ddldistrict.Enabled = false;
            }
        }
        bindSubDivision();
        if (Session["Sub_DivCode"] != null)
        {
            if (Session["Sub_DivCode"].ToString() != "")
            {
                ddlsubdivision.SelectedValue = Session["Sub_DivCode"].ToString();
                ddlsubdivision.Enabled = false;
            }
        }
        bindCircle();
        if (Session["Block_Code"] != null)
        {
            if (Session["Block_Code"].ToString() != "")
            {
                ddlcircle.SelectedValue = Session["Block_Code"].ToString();
                ddlcircle.Enabled = false;
            }
        }
        bindThana();
        if (Session["Thana_Code"] != null)
        {
            if (Session["Thana_Code"].ToString() != "")
            {
                ddlthana.SelectedValue = Session["Thana_Code"].ToString();
                ddlthana.Enabled = false;
            }
        }
    }
    protected void bindCommissionary()
    {
        ddlCommissionary.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("1"));
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType });
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
            ddlCommissionary.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void bindDistrict()
    {
        ddldistrict.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("2"));
        SqlParameter _CommissionaryCode = new SqlParameter("@DivisionCode", ddlCommissionary.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _CommissionaryCode });
        if (dt.Rows.Count > 0)
        {
            ddldistrict.DataSource = dt;
            ddldistrict.DataTextField = "DISTRICTNAME";
            ddldistrict.DataValueField = "DISTRICTCODE";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));

        }
        else
        {
            ddldistrict.DataSource = null;
            ddldistrict.DataTextField = "DISTRICTNAME";
            ddldistrict.DataValueField = "DISTRICTCODE";
            ddldistrict.DataBind();
            ddldistrict.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void bindSubDivision()
    {
        ddlsubdivision.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("3"));
        SqlParameter _DistCode = new SqlParameter("@DISTRICTCODE", ddldistrict.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _DistCode });
        if (dt.Rows.Count > 0)
        {
            ddlsubdivision.DataSource = dt;
            ddlsubdivision.DataTextField = "Sd_Name_En";
            ddlsubdivision.DataValueField = "Sd_Code2";
            ddlsubdivision.DataBind();
            ddlsubdivision.Items.Insert(0, new ListItem("All", "0"));

        }
        else
        {
            ddlsubdivision.DataSource = null;
            ddlsubdivision.DataTextField = "Sd_Name_En";
            ddlsubdivision.DataValueField = "Sd_Code2";
            ddlsubdivision.DataBind();
            ddlsubdivision.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void bindCircle()
    {
        ddlcircle.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("4"));
        SqlParameter _SubDivision = new SqlParameter("@SubDivisionCode", ddlsubdivision.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _SubDivision });
        if (dt.Rows.Count > 0)
        {
            ddlcircle.DataSource = dt;
            ddlcircle.DataTextField = "BlockName";
            ddlcircle.DataValueField = "BlockCode";
            ddlcircle.DataBind();
            ddlcircle.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlcircle.DataSource = null;
            ddlcircle.DataTextField = "BlockName";
            ddlcircle.DataValueField = "BlockCode";
            ddlcircle.DataBind();
            ddlcircle.Items.Insert(0, new ListItem("All", "0"));
        }
    }
    protected void bindThana()
    {
        ddlthana.Items.Clear();
        SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("5"));
        SqlParameter _Blockcode = new SqlParameter("@BlockCode", ddlcircle.SelectedValue);
        DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _Blockcode });
        if (dt.Rows.Count > 0)
        {
            ddlthana.DataSource = dt;
            ddlthana.DataTextField = "Police_Station";
            ddlthana.DataValueField = "PS_Code";
            ddlthana.DataBind();
            ddlthana.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            ddlthana.DataSource = null;
            ddlthana.DataTextField = "Police_Station";
            ddlthana.DataValueField = "PS_Code";
            ddlthana.DataBind();
            ddlthana.Items.Insert(0, new ListItem("All", "0"));
        }
    }

    protected void ddlCommissionary_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDistrict();
        bindSubDivision();
        bindCircle();
        bindThana();
    }

    protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindSubDivision();
        bindCircle();
        bindThana();

    }

    protected void ddlsubdivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindCircle();
        bindThana();

    }

    protected void ddlcircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindThana();

    }

    protected void bindAllApplication_Grid()
    {
        try
        {
            pnlgrid.Visible = true;
            //btnback.Visible = true;
            Panel1.Visible = false;
            grvAllApplication.Visible = true;
            gvDisputeDetailsReport.Visible = false;
            int lessCount = 0;
            int morecount = 0;
            if (int.Parse(ddlcount.SelectedValue) == 0)
            {
                lessCount = 0;
                morecount = 0;
            }
            if (int.Parse(ddlcount.SelectedValue) == 1)
            {
                lessCount = 1;
                morecount = 1;
            }
            if (int.Parse(ddlcount.SelectedValue) == 2)
            {
                lessCount = 1;
                morecount = 5;
            }
            if (int.Parse(ddlcount.SelectedValue) == 3)
            {
                lessCount = 6;
                morecount = 10;
            }
            if (int.Parse(ddlcount.SelectedValue) == 4)
            {
                lessCount = 11;
                morecount = 15;
            }
            if (int.Parse(ddlcount.SelectedValue) == 5)
            {
                lessCount = 16;
                morecount = 20;
            }
            if (int.Parse(ddlcount.SelectedValue) == 6)
            {
                lessCount = 21;
                morecount = 30;
            }
            if (int.Parse(ddlcount.SelectedValue) == 7)
            {
                lessCount = 31;
                morecount = 0;
            }

            SqlParameter QueryType = new SqlParameter("@QueryType", Convert.ToInt32("6"));
            SqlParameter _CommissionaryCode = new SqlParameter("@DivisionCode", ddlCommissionary.SelectedValue == "0" ? "0" : ddlCommissionary.SelectedValue);
            SqlParameter _DistCode = new SqlParameter("@DISTRICTCODE", ddldistrict.SelectedValue == "0" ? "0" : ddldistrict.SelectedValue);
            SqlParameter _SubDivision = new SqlParameter("@SubDivisionCode", ddlsubdivision.SelectedValue == "0" ? "0" : ddlsubdivision.SelectedValue);
            SqlParameter _Blockcode = new SqlParameter("@BlockCode", ddlcircle.SelectedValue == "0" ? "0" : ddlcircle.SelectedValue);
            SqlParameter _pscode = new SqlParameter("@ThanaCode", ddlthana.SelectedValue == "0" ? "0" : ddlthana.SelectedValue);
            SqlParameter _fromdate = new SqlParameter("@fromDate", txtfrmdate.Text == "" ? null : Convert.ToDateTime(txtfrmdate.Text).ToString("yyyy-MM-dd"));
            SqlParameter _ToDate = new SqlParameter("@ToDate", txtTodate.Text == "" ? null : Convert.ToDateTime(txtTodate.Text).ToString("yyyy-MM-dd"));
            SqlParameter _lessCount = new SqlParameter("@lessCount1", lessCount);
            SqlParameter _morecount = new SqlParameter("@morecount", morecount);
            DataTable dt = clsData.GetDataTableWithProc("Sp_AllApplication_with_Meeting", new SqlParameter[] { QueryType, _CommissionaryCode, _DistCode, _SubDivision, _Blockcode, _pscode, _fromdate, _ToDate, _lessCount, _morecount });

            lbltotalthana.Text = "Total Police Station : " + dt.Rows.Count;

            if (dt.Rows.Count > 0)
            {
                grvAllApplication.Columns[5].FooterText = "Total :";
                grvAllApplication.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalEntry")).Sum().ToString();
                grvAllApplication.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grvAllApplication.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();
                grvAllApplication.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalMetting")).Sum().ToString();

                grvAllApplication.DataSource = dt;
                grvAllApplication.DataBind();
                divmsg.Visible = false;
            }
            else
            {
                grvAllApplication.Columns[5].FooterText = "Total :";
                grvAllApplication.Columns[6].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalEntry")).Sum().ToString();
                grvAllApplication.Columns[7].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Finalize")).Sum().ToString();
                grvAllApplication.Columns[8].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("Unfinalize")).Sum().ToString();
                grvAllApplication.Columns[9].FooterText = dt.AsEnumerable().Select(x => x.Field<int>("TotalMetting")).Sum().ToString();

                grvAllApplication.DataSource = null;
                grvAllApplication.DataBind();
                divmsg.Visible = false;
            }
          
        }
        catch (Exception ex)
        {

        }


    }

    protected void btnExpToExl_Click(object sender, EventArgs e)
    {
        try
        {

            ExportExcel(grvAllApplication, pnlgrid);
        }
        catch (Exception)
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
        for (int i = 0; i < Gv.FooterRow.Cells.Count; i++)
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
            if (j <= Gv.Rows.Count)
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
        string Headrer = lbltext.Text;
        Response.Write("<h3><center>" + Headrer + "</center></h3>");
        Response.Write(dd);
        Response.End();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        bindAllApplication_Grid();
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in grvAllApplication.Rows)
        {
            CheckBox chklist = (CheckBox)gvr.FindControl("chkselect");
            chklist.Checked = ((CheckBox)sender).Checked;
        }
    }

    protected void btnSentMsg_Click(object sender, EventArgs e)
    {
        bool flag = false;
        string msgsent = "";
        foreach (GridViewRow row in grvAllApplication.Rows)
        {
            CheckBox chkDetails = (CheckBox)row.FindControl("chkselect");
            if (chkDetails.Checked)
            {
                string policestation = row.Cells[5].Text;
                string total = row.Cells[6].Text;
                string mob = grvAllApplication.DataKeys[row.RowIndex].Values["mobile"].ToString();
                if (mob != "" && mob.Length == 10)
                {
                    string fromdate = txtfrmdate.Text == "" ? "25/03/2022" : txtfrmdate.Text;
                    string todate = txtTodate.Text == "" ? DateTime.Now.ToString("dd/MM/yyyy") : txtTodate.Text;
                    SMSReferenceWithID.SMSWebService smsSend = new SMSReferenceWithID.SMSWebService();
                    //string msg = "भू-समाधान पोर्टल पर "+ policestation + " थाना द्वारा दिनांक  "+ fromdate + " से  दिनांक "+ todate + " तक "+total+" मामलों की प्रविष्टि की गयी है।  प्रविष्ट किये गये सभी मामलों को 30 दिनों के अन्दर निष्पादन करने हेतु आवश्यक कार्रवाई सुनिश्चित की जाय। - गृह विभाग, बिहार सरकार";
                    string msg = "भू-समाधान पोर्टल पर  " + policestation + " थाना  द्वारा  दिनांक  " + fromdate + " से दिनांक " + todate + " तक " + total + " मामलों की प्रविष्टि की गयी है, जो कि अपेक्षा से कम है। अतः थानों/अंचलों में प्राप्त  सभी भूमि विवाद संबंधी मामलों की प्रविष्टि करते हुए सभी मामलों को 30 दिनों के अन्दर निष्पादन करने हेतु आवश्यक कार्रवाई सुनिश्चित की जाय। - गृह विभाग, बिहार सरकार";
                    string send = smsSend.sendSMSUnicode(msg, mob, "BIHAREDISTRICT-policeerss", "erss@123456", "bac4e6cd-ac88-43bd-86f2-aa0d04171e79", "LRD12345SMS", "1307168266273365557");
                    msgsent = send;
                }
            }
        }
        if (msgsent == "Message Sent Successfully..." || msgsent.ToString().Split(',')[0] == "402")
        {

            Utility.showMessage(Page, "Message  sent");

        }
    }

    protected void grvAllApplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;
            string Mobile = Convert.ToString(this.grvAllApplication.DataKeys[rowIndex]["mobile"]);
            string DivisionCode = Convert.ToString(this.grvAllApplication.DataKeys[rowIndex]["DIVISIONCODE"]);
            string DistCode = Convert.ToString(this.grvAllApplication.DataKeys[rowIndex]["DISTRICTCODE"]);
            string BlockCode = Convert.ToString(this.grvAllApplication.DataKeys[rowIndex]["BlockCode"]);
            string SubDivisionCode = Convert.ToString(this.grvAllApplication.DataKeys[rowIndex]["Sd_Code2"]);
            string PoliceStationCode = Convert.ToString(this.grvAllApplication.DataKeys[rowIndex]["PS_Code"]);

            DataTable dt = new DataTable();
             LinkButton linkBtn = (LinkButton)e.CommandSource;

            SqlParameter _DivisionCode = new SqlParameter("@DivisionCode", DivisionCode);
            SqlParameter _DistCode = new SqlParameter("@DISTRICTCODE", DistCode);
            SqlParameter _SubDivisionCode = new SqlParameter("@SubDivisionCode", SubDivisionCode);
            SqlParameter _BlockCode = new SqlParameter("@BlockCode", BlockCode);
            SqlParameter _ThanaCode = new SqlParameter("@ThanaCode", PoliceStationCode);
            if (e.CommandName == "Entry")
            {
                SqlParameter _commandName = new SqlParameter("@cmdName", "Entry");             
                dt = clsData.GetDataTableWithProc("SpShowDisputeDetails", new SqlParameter[] { _DivisionCode, _DistCode, _SubDivisionCode, _BlockCode, _ThanaCode, _commandName, });
                
            }
           
            if (e.CommandName == "Finalize")
            {
                SqlParameter _commandName = new SqlParameter("@cmdName", "Finalize");           
                dt = clsData.GetDataTableWithProc("SpShowDisputeDetails", new SqlParameter[] { _DivisionCode, _DistCode, _SubDivisionCode, _BlockCode, _ThanaCode, _commandName, });
            }

            if (e.CommandName == "UnFinalize")
            {
                SqlParameter _commandName = new SqlParameter("@cmdName", "UnFinalize");
                
                dt = clsData.GetDataTableWithProc("SpShowDisputeDetails", new SqlParameter[] {  _DivisionCode, _DistCode, _SubDivisionCode, _BlockCode, _ThanaCode , _commandName ,});
            }

            if (e.CommandName == "AllMeeting")
            {
                SqlParameter _commandName = new SqlParameter("@cmdName", "AllMeeting");
                dt = clsData.GetDataTableWithProc("SpShowDisputeDetails", new SqlParameter[] { _DivisionCode, _DistCode, _SubDivisionCode, _BlockCode, _ThanaCode, _commandName, });
            }

            if (dt.Rows.Count > 0)
            {
                gvDisputeDetailsReport.DataSource = dt;
                gvDisputeDetailsReport.DataBind();
                grvAllApplication.Visible = false;
                gvDisputeDetailsReport.Visible = true;
                pnlgrid.Visible = false;
                //btnback.Visible = true;
                Panel1.Visible = true;
            }
        }
        catch (Exception ex) { }
    }

    protected void gvDisputeDetailsReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");


            ImageButton image2 = (ImageButton)e.Row.FindControl("Image2");
            image2.Attributes.Add("onclick", "return fnLinkbutton1('" + image2.ClientID + "')");



            ImageButton image3 = (ImageButton)e.Row.FindControl("Image3");
            image3.Attributes.Add("onclick", "return fnLinkbutton1('" + image3.ClientID + "')");


            ImageButton image4 = (ImageButton)e.Row.FindControl("Image4");
            image4.Attributes.Add("onclick", "return fnLinkbutton1('" + image4.ClientID + "')");


            ImageButton image5 = (ImageButton)e.Row.FindControl("Image5");
            image5.Attributes.Add("onclick", "return fnLinkbutton1('" + image5.ClientID + "')");


            ImageButton image6 = (ImageButton)e.Row.FindControl("Image6");
            image6.Attributes.Add("onclick", "return fnLinkbutton1('" + image6.ClientID + "')");


        }
    }

    protected void lnkView_Click(object sender, EventArgs e)
    {
        try
        {
            Encryptor enc = new Encryptor(Encryptor.PrivateKey);
            LinkButton linkbtn = sender as LinkButton;
            string UrlRedirect = enc.Encrypt(linkbtn.CommandArgument);
            string strFilePath = "Information.aspx?RegId=" + UrlRedirect;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + strFilePath + "','_blank')", true);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
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

    public bool CheckImage(object url)
    {
        if (url.ToString() != "")
        {
            string p = (url.ToString()).Replace("~", "");
            url = "http://localhost:8080" + p;
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(url.ToString());
                    string imreBase64Data = Convert.ToBase64String(imageBytes);
                    string imgDataURL = string.Format("data:Application/pdf;base64,{0}", imreBase64Data);

                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        else
        {
            return false;
        }


    }
    //fix it
    //protected void btnback_Click(object sender, EventArgs e)
    //{
    //    pnlgrid.Visible = true;

    //    bindAllApplication_Grid();
    //    gvDisputeDetailsReport.Visible = false;

    //}
}