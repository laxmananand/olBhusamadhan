using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.HtmlControls;
using Microsoft.Security.Application;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;


public partial class HQ_ApplicationDetails : System.Web.UI.Page
{
    clsDataAccessLandDispute clsData = new clsDataAccessLandDispute();
    whiteList wl = new whiteList();
    Encryptor enc = new Encryptor(Encryptor.PrivateKey);
    String Id="";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindBhumiVivadVivran();
            BindVadiVivran();
            BindPratiVadiVivran();
            BindChauhadi();
            BindPrastutSakshaya();
            bindAnchaladhikari();
            bindBhumiVivadGhatna();
            bindCourtVadVivaran();
            //TXT_LenderName.Attributes["AttributeName"]= value
            // Button1.Attributes["Path"] = "~/LandDoc/Upload/Application_Doc/ap_220324231534Folio-Privacy-Policy.pdf";



        }

    }

    private void bindCourtVadVivaran()
    {
        try
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Regex.Replace(Id, " ", "+");
            Id = enc.Decrypt(Id);

            SqlParameter GetQueryType = new SqlParameter("@QueryType", "9");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });


            if (dt.Rows.Count > 0)
            {
                GVCourtVad.DataSource = dt;
                GVCourtVad.DataBind();

            }
            else
            {
                GVCourtVad.DataSource = null;
                GVCourtVad.DataBind();
            }

        }
        catch (Exception)
        {

        }
    }

    private void bindBhumiVivadGhatna()
    {
        try
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Regex.Replace(Id, " ", "+");
            Id = enc.Decrypt(Id);
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "8");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });


            if (dt.Rows.Count > 0)
            {
                GVBhumiVivadGhatna.DataSource = dt;
                GVBhumiVivadGhatna.DataBind();

            }
            else
            {
                GVBhumiVivadGhatna.DataSource = null;
                GVBhumiVivadGhatna.DataBind();
            }

        }
        catch (Exception)
        {

        }
    }

    private void bindAnchaladhikari()
    {
        try
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Regex.Replace(Id, " ", "+");
            Id = enc.Decrypt(Id);
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "7");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });


            if (dt.Rows.Count > 0)
            {
                GVAnchalaDhakari.DataSource = dt;
                GVAnchalaDhakari.DataBind();

            }
            else
            {
                GVAnchalaDhakari.DataSource = dt;
                GVAnchalaDhakari.DataBind();
            }

        }
        catch (Exception)
        {

        }

    }
    private void BindPrastutSakshaya()
    {
        try
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Regex.Replace(Id, " ", "+");
            Id = enc.Decrypt(Id);
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "6");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });


            if (dt.Rows.Count > 0)
            {

                lblVadiKhatiyan.Text = dt.Rows[0]["Vadi_Khatiyaan"].ToString();
                lblVadikewala.Text = dt.Rows[0]["Vadi_Kevaala"].ToString();
                lblVadiJamabandi.Text = dt.Rows[0]["Vadi_CopyOfJamabandi"].ToString();
                lblVadiRasid.Text = dt.Rows[0]["Vadi_LagaanRaseed"].ToString();
                lblVadiVanshawali.Text = dt.Rows[0]["Vadi_Vanshaavalee"].ToString();
                lblVadiBatwara.Text = dt.Rows[0]["Vadi_Batavaara"].ToString();
                lblVadiParcha.Text = dt.Rows[0]["Vadi_Parcha"].ToString();
                lblVadiNyayalayKaAadesh.Text = dt.Rows[0]["vadi_nyayaalay_aadesh"].ToString();

                //img1.ImageUrl = dt.Rows[0]["Vadi_sakshya_File"].ToString();
                try
                {
                    img1.Attributes["Path"] = dt.Rows[0]["Vadi_sakshya_File"].ToString();

                    if (dt.Rows[0]["Vadi_sakshya_File"].ToString() == "")
                        img1.Visible = false;

                }
                catch (Exception ex)
                {
                    img1.Visible = false;
                }


                lblPrativadikhatiyan.Text = dt.Rows[0]["prativadi_Khatiyaan"].ToString();
                lblPrativadiKewala.Text = dt.Rows[0]["prativadi_Kevaala"].ToString();
                lblPrativadiJamabandi.Text = dt.Rows[0]["prativadi_CopyOfJamabandi"].ToString();
                lblPrativadiLanan.Text = dt.Rows[0]["prativadi_LagaanRaseed"].ToString();
                lblPrativadiVanshawali.Text = dt.Rows[0]["prativadi_Vanshaavalee"].ToString();
                lblPrativadiBatwara.Text = dt.Rows[0]["prativadi_Batavaara"].ToString();
                lblPrativadiParcha.Text = dt.Rows[0]["prativadi_Parcha"].ToString();
                lblPrativadiNyayalayAadesh.Text = dt.Rows[0]["prativadi_nyayaalay_aadesh"].ToString();
                
                // img2.ImageUrl = dt.Rows[0]["Prativadi_sakshya_File"].ToString();
                try
                {
                    img2.Attributes["Path"] = dt.Rows[0]["Prativadi_sakshya_File"].ToString();

                    if (dt.Rows[0]["Prativadi_sakshya_File"].ToString() == "")
                        img2.Visible = false;

                }
                catch (Exception ex)
                {
                    img2.Visible = false;
                }

                lblPulisPadadhikariPrathamDristya.Text = dt.Rows[0]["pulis_padadhikari_vivarani"].ToString();
                // lblPulisPadadhikariJanch.Text = dt.Rows[0]["pulis_padadhikar_Patr_file"].ToString();
                //Button2.Attributes["Path"] = dt.Rows[0]["pulis_padadhikar_Patr_file"].ToString();

                try
                {
                    Image2.Attributes["Path"] = dt.Rows[0]["pulis_padadhikar_Patr_file"].ToString();

                    if (dt.Rows[0]["pulis_padadhikar_Patr_file"].ToString() == "")
                        Image2.Visible = false;

                }
                catch (Exception ex)
                {
                    Image2.Visible = false;
                }

                lblHalkaKramchariViverani.Text = dt.Rows[0]["HalkaKarmchari_vivran"].ToString();
                //lblHalkaKaramchariPrativedan.Text = dt.Rows[0]["HalkaKarmchari_Patr_file"].ToString();
                //Button3.Attributes["Path"] = dt.Rows[0]["HalkaKarmchari_Patr_file"].ToString();

                try
                {
                    Image3.Attributes["Path"] = dt.Rows[0]["HalkaKarmchari_Patr_file"].ToString();

                    if (dt.Rows[0]["HalkaKarmchari_Patr_file"].ToString() == "")
                        Image3.Visible = false;

                }
                catch (Exception ex)
                {
                    Image3.Visible = false;
                }

                lblVivaditBhukhandkiMapi.Text = dt.Rows[0]["vivadit_bhukhand_Mapi_ki_avashyakta_hai"].ToString() + " " + dt.Rows[0]["vivadit_bhukhand_Mapi"].ToString();

                try
                {
                    Image4.Attributes["Path"] = dt.Rows[0]["vivaadit_bhukhand_Mapi_File"].ToString();

                    if (dt.Rows[0]["vivaadit_bhukhand_Mapi_File"].ToString() == "")
                        Image4.Visible = false;

                }
                catch (Exception ex)
                {
                    Image4.Visible = false;
                }
            }

        }
        catch (Exception)
        {

        }
    }

    private void BindChauhadi()
    {
        try
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Regex.Replace(Id, " ", "+");
            Id = enc.Decrypt(Id);
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "5");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });

            if (dt.Rows.Count > 0)
            {

                lblIsPrativadiInformed.Text = dt.Rows[0]["prativadi_ko_suchit_kiya_gaya_hai"].ToString();
                lblMadhayam.Text = dt.Rows[0]["given_info_type"].ToString();
                lblPrativadiTaamil.Text = dt.Rows[0]["prativadi_ko_suchana_ka_taamila_praapt_hai"].ToString();
                lblPrativadipresent.Text = dt.Rows[0]["prativadi_upasthit_hua_hai"].ToString();
                //lblNotice.Text = dt.Rows[0]["is_given_info_to_appear_in_hearing"].ToString();
                //lblNoticePrapt.Text = dt.Rows[0]["suchana_kaTamila_prapt_hai"].ToString();


            }

        }
        catch (Exception)
        {

        }

    }
    protected void BindBhumiVivadVivran()
    {
        try
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Regex.Replace(Id, " ", "+");
            Id = enc.Decrypt(Id);
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "1");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });
            if (dt.Rows.Count > 0)
            {
                lblDistrict.Text = dt.Rows[0]["DISTRICTNAME"].ToString();
                lblSubdivision.Text = dt.Rows[0]["Sd_Name_En"].ToString();
                lblBlock.Text = dt.Rows[0]["BlockName"].ToString();
                lblPolice.Text = dt.Rows[0]["Police_Station"].ToString();

                lblareatype.Text = dt.Rows[0]["AreaType"].ToString();
                lblPanchyat.Text = dt.Rows[0]["PanchayatName"].ToString();
                lblVillage.Text = dt.Rows[0]["VILLNAME"].ToString();
                lblWard.Text = dt.Rows[0]["WARDNAME"].ToString();
                lblrajaswa_sankhya.Text = dt.Rows[0]["rajasv_thaana_sankhya"].ToString();
                lblbhumitype.Text = dt.Rows[0]["Bhumitype"].ToString();
                lblsarkaribhumitype.Text = dt.Rows[0]["SarkariBhumiType"].ToString();
                //lblbhumivivad_Anya.Text = dt.Rows[0]["SarkariBhumiType_Anya"].ToString();
                lblbhumivivadtype.Text = dt.Rows[0]["BhumiVivadType"].ToString();
                //lblbhumivivad_Anya.Text = dt.Rows[0]["BhumiVivadType_Anya"].ToString();
                //   lblAppDoc.Text = dt.Rows[0]["ApplicationFile"].ToString();
                //  Button1.Attributes["Path"] = dt.Rows[0]["ApplicationFile"].ToString();
                try
                {
                    lblvivadKakarak.Text = dt.Rows[0]["status_name"].ToString();
                    if (dt.Rows[0]["ApplicationFile"].ToString() == "")
                        Image1.Visible = false;

                    Image1.Attributes["Path"] = dt.Rows[0]["ApplicationFile"].ToString();
                }
                catch (Exception ex)
                {
                    Image1.Visible = false;
                }


            }
            else
            {
                Utility.showMessage(this, "Invalid Application Id");
                return;
            }
            GetQueryType = new SqlParameter("@QueryType", "2");
            GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });
            if (dt.Rows.Count > 0)
            {
                GVBhumiVivran.DataSource = dt;
                GVBhumiVivran.DataBind();
            }
            else
            {
                GVBhumiVivran.DataSource = null;
                GVBhumiVivran.DataBind();
            }
        }
        catch (Exception)
        {

        }
    }
    protected void BindVadiVivran()
    {
        try
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Regex.Replace(Id, " ", "+");
            Id = enc.Decrypt(Id);
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "3");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt64(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });

            // dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });
            if (dt.Rows.Count > 0)
            {
                GVVadi.DataSource = dt;
                GVVadi.DataBind();
            }
            else
            {
                GVVadi.DataSource = null;
                GVVadi.DataBind();
            }
        }
        catch (Exception)
        {

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
        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        DataRowView data = (DataRowView)e.Row.DataItem;

        if (string.IsNullOrWhiteSpace((string)data[0]))
        {
            e.Row.Visible = false;
        }

        //Check if the row is datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            ImageButton image1 = (ImageButton)e.Row.FindControl("Image1");
            image1.Attributes.Add("onclick", "return fnLinkbutton1('" + image1.ClientID + "')");



        }
    }

    protected void BindPratiVadiVivran()
    {
        try
        {
            Id = Request.QueryString["RegId"].ToString();
            Id = Regex.Replace(Id, " ", "+");
            Id = enc.Decrypt(Id);
            SqlParameter GetQueryType = new SqlParameter("@QueryType", "4");
            SqlParameter GetApplicationId = new SqlParameter("@ApplicationId", Convert.ToInt32(Id));

            DataTable dt = clsData.GetDataTableWithProc("SP_ViewSearchApplication", new SqlParameter[] { GetQueryType, GetApplicationId });



            if (dt.Rows.Count > 0)
            {
                GvpratiVadi.DataSource = dt;
                GvpratiVadi.DataBind();
            }
            else
            {
                GvpratiVadi.DataSource = null;
                GvpratiVadi.DataBind();
            }
        }
        catch (Exception)
        {

        }

    }
    protected void GVBhumiVivran_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GVBhumiVivran_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVBhumiVivran.PageIndex = e.NewPageIndex;

    }

    protected void btnViewDocument_Click(object sender, EventArgs e)
    {
        string FilePath = Server.MapPath("fuIdDocumentLD212095111.pdf");
        WebClient User = new WebClient();
        Byte[] FileBuffer = User.DownloadData(FilePath);
        if (FileBuffer != null)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", FileBuffer.Length.ToString());
            Response.BinaryWrite(FileBuffer);
        }
    }

    protected void GDV_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType != DataControlRowType.DataRow)
        {
            return;
        }

        DataRowView data = (DataRowView)e.Row.DataItem;

        if (string.IsNullOrWhiteSpace((string)data[0]))
        {
            e.Row.Visible = false;
        }
    }
}