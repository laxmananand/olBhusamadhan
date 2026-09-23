using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class WebUserControlAction : System.Web.UI.UserControl
{
    DataSet ds;
    DataTable dtRepeaterTable;
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    PagedDataSource pageDataSource = new PagedDataSource();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("a_id",typeof(string));
        DataRow workRow = dt.NewRow();
        workRow["a_id"] = Session["a_id"].ToString();
        dt.Rows.Add(workRow);
        parentRepeater.DataSource = dt;
        parentRepeater.DataBind();
    }
    protected void parentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //e.Item.FindControl("div_Vadi_Svarajaya_Label").Visible = true;
           // e.Item.FindControl("div_Vadi_Svarajaya").Visible = true;
            HiddenField lbla_id = (HiddenField)e.Item.FindControl("lbla_idc");
            
            //अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण
            ActionDetailsEntryDetails_ka_vivran(lbla_id.Value, sender, e);
           
        }
    }
    private void ActionDetailsEntryDetails_ka_vivran(string a_id, object sender, RepeaterItemEventArgs e)
    {
        SqlParameter _a_id2 = new SqlParameter("@a_id", Convert.ToInt32(a_id));
        SqlParameter _fn2 = new SqlParameter("@fn", "getActionDetailsEntry");
        DataTable dt_nyayalay_vivran = clsData.GetDataTableWithProc("getEntryPage_print", new SqlParameter[] { _a_id2, _fn2 });
        ViewState["gridTable"] = dt_nyayalay_vivran;

        if (dt_nyayalay_vivran.Rows.Count > 0)
        {
            ((Label)e.Item.FindControl("lblVivaadKiSanvedanasheelata")).Text = ":" + dt_nyayalay_vivran.Rows[0]["SensitivityType"].ToString();
            ((Label)e.Item.FindControl("lblBaithakKiTithi")).Text = ":" + dt_nyayalay_vivran.Rows[0]["Meeting_date"].ToString();
            ((Label)e.Item.FindControl("lblkyaVaadeeUpasthitHai")).Text = ":" + dt_nyayalay_vivran.Rows[0]["Is_Vadi_Present"].ToString();
            ((Label)e.Item.FindControl("lblKyaPrativaadeeUpasthitHai")).Text = ":" + dt_nyayalay_vivran.Rows[0]["Is_PratiVadi_Present"].ToString();
            ((Label)e.Item.FindControl("lblBaithakKaNishkarsh")).Text = ":" + dt_nyayalay_vivran.Rows[0]["baithak_ka_nishkarsh"].ToString();
            if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "4")
            {
                e.Item.FindControl("divAsveekrtiKaKaaranLabel").Visible = true;
                e.Item.FindControl("divtithi").Visible = false;
                ((Label)e.Item.FindControl("lblAsveekrtiKaKaaran")).Text = ":" + dt_nyayalay_vivran.Rows[0]["reason_for_rejection"].ToString();
            }
            else
            {
                e.Item.FindControl("divtithi").Visible = true;
                e.Item.FindControl("divAsveekrtiKaKaaranLabel").Visible = false;
                if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "1")
                {
                    ((Label)e.Item.FindControl("lbltithi")).Text = "प्रारंभिक निष्पादन की तिथि";
                    ((Label)e.Item.FindControl("lbltithivalue")).Text = ":" + dt_nyayalay_vivran.Rows[0]["Matter_Status_date"].ToString();
                }
                else if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "2")
                {
                    ((Label)e.Item.FindControl("lbltithi")).Text = "मापी की तिथि";
                    ((Label)e.Item.FindControl("lbltithivalue")).Text = ":" + dt_nyayalay_vivran.Rows[0]["mapi_ki_tithi"].ToString();
                }
                else if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "3")
                {
                    ((Label)e.Item.FindControl("lbltithi")).Text = "अगली सुनवाई की तिथि";
                    ((Label)e.Item.FindControl("lbltithivalue")).Text = ":" + dt_nyayalay_vivran.Rows[0]["agali_sunavaee_ki_tithi"].ToString();
                }
                else if (dt_nyayalay_vivran.Rows[0]["Matter_Status"].ToString() == "5")
                {
                    ((Label)e.Item.FindControl("lbltithi")).Text = "अंतिम निष्पादन की तिथि ";
                    ((Label)e.Item.FindControl("lbltithivalue")).Text = ":" + dt_nyayalay_vivran.Rows[0]["date_of_disposal"].ToString();
                }

            }
            ((Label)e.Item.FindControl("lblBaithakMeinLiyaGayaNirnay")).Text = ":" + dt_nyayalay_vivran.Rows[0]["conclusion_of_the_meeting"].ToString();
            ((Label)e.Item.FindControl("lblAnchalaadhikaareeKaMantavy")).Text = ":" + dt_nyayalay_vivran.Rows[0]["anchala_dhikari_mantavy"].ToString();
            ((Label)e.Item.FindControl("lblThaanaadhyakshKaMantavy")).Text = ":" + dt_nyayalay_vivran.Rows[0]["thana_prabhari_mantavy"].ToString();

        }
    }
}