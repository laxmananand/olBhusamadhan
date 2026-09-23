using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Net;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class LandDispute_Entry_Add_Holiday : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["Role"] == null)
        //{
        //    Session.Clear();
        //    Session.Abandon();
        //    Response.Redirect("../../Login_Default.aspx");


        //}

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        SqlParameter _HolidayDate = new SqlParameter("@HolidayDate", txtHolidayDate.Text);

        SqlParameter _Remark = new SqlParameter("@Remark",
            string.IsNullOrEmpty(txtRemark.Text) ? (object)DBNull.Value : txtRemark.Text);

        DataTable dt = clsData.GetDataTableWithProc(
            "sp_AddHoliday",
            new SqlParameter[] { _HolidayDate, _Remark }
        );

        // 🔥 SAME STYLE AS YOUR CODE
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["mStatus"].ToString() == "1")
            {
                Utility.showMessage(Page, dt.Rows[0]["Message"].ToString() + " ✅");
                txtHolidayDate.Text = string.Empty;
                txtRemark.Text = string.Empty;
            }
            else
            {
                Utility.showMessage(Page, dt.Rows[0]["Message"].ToString() + " ❌");
            }
        }
        
             else
            {
                Utility.showMessage(Page, "Technical error");
            }
        

    }
   


}