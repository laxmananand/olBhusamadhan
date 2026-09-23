using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Select
/// </summary>
public class clsDataAccessLMS
{
    //private DataTable _dt;
    //private int _vic;
    //private Page _page;


    SqlConnection con = new SqlConnection();
    SqlTransaction Trans;
    public clsDataAccessLMS()
    {
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EocConnectionString"].ConnectionString;

    }

    public DataTable GetDataTable(string query)
    {
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            //con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            SqlDataAdapter adap1 = new SqlDataAdapter();
            cmd.Connection = con;
            adap1.SelectCommand = cmd;
            adap1.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            //return ex.Message.ToString();
            return dt;
        }

        finally
        {
            con.Close();
        }

    }


    public DataTable GetDataTable(string query, SqlParameter[] param)
    {
        DataTable dt = new DataTable();
        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            foreach (SqlParameter prm in param)
            {
                cmd.Parameters.Add(prm);
            }
            SqlDataAdapter adap1 = new SqlDataAdapter();
            cmd.Connection = con;
            adap1.SelectCommand = cmd;
            adap1.Fill(dt);
            return dt;
        }
        catch (Exception)
        {
            //return ex.Message.ToString();
            return dt;
        }

        finally
        {
            con.Close();
        }

    }

    public int ExecuteSql(string Query)
    {


        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();

            string strCommand = Query;
            cmd.CommandText = strCommand;
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            return 0;

        }

        finally
        {
            con.Close();
        }

    }

    public DataTable GetDataTableSp(string procname, SqlParameter[] param)
    {

        DataTable dt = new DataTable();
        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = procname;
            if (param != null)
            {
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
            }
            SqlDataAdapter adap1 = new SqlDataAdapter();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            adap1.SelectCommand = cmd;
            adap1.Fill(dt);
            return dt;
        }
        catch (Exception)
        {

            return dt;
        }

        finally
        {
            con.Close();
        }

    }

    public int ExecuteSql(string Query, SqlParameter[] param)
    {

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = Query;
            foreach (SqlParameter prm in param)
            {
                cmd.Parameters.Add(prm);
            }
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);

            return 0;

        }

        finally
        {
            con.Close();
        }



    }

    public int ExecuteSql(string Query, List<SqlParameter> param, Label lblMsg)
    {

        try
        {
            con.Open();
            //string strwhere = string.Empty;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = Query;
            foreach (SqlParameter prm in param)
            {
                cmd.Parameters.Add(prm);
                //strwhere = strwhere + "," + prm.Value;
            }
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message, "Error");
            lblMsg.Text = ex.Message;
            //lblMsg.Text = Query;
            return 0;

        }

        finally
        {
            con.Close();
        }



    }

    public string ExecuteScalar(string strSql)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;
            cmd.Connection = con;
            cmd.Connection.Open();
            return cmd.ExecuteScalar().ToString();
        }
        catch (Exception)
        {
            cmd.Connection.Close();
            return "";
        }
        finally
        {
            cmd.Connection.Close();

        }
    }

    public void OpenConnection()
    {
        SqlCommand cmd = new SqlCommand();
        con.Open();
        cmd.Connection = con;
    }
    public void CloseConnection()
    {
        con.Close();
    }

    public void BeginTransaction()
    {
        SqlCommand cmd = new SqlCommand();
        Trans = con.BeginTransaction(IsolationLevel.Serializable);
        cmd.Transaction = Trans;

    }
    public void BeginTransaction(IsolationLevel level)
    {
        SqlCommand cmd = new SqlCommand();
        Trans = con.BeginTransaction(level);
        cmd.Transaction = Trans;

    }

    public void Commit()
    {
        Trans.Commit();

    }
    public void Rollback()
    {
        Trans.Rollback();

    }

    //public void BindGrid(GridView gvShow, string sql,int totRecCount)
    //{




    //    FillGridView(gvShow);
    //}

    //public void FillGridView(GridView gvShow, string sql, int totRecCount)
    //{
    //    _dt = GetDataTable(sql);
    //    _vic = totRecCount;

    //    ObjectDataSource ods = new ObjectDataSource();

    //    ods.ID = "ods" + gvShow.ID;

    //    ods.EnablePaging = gvShow.AllowPaging;
    //    ods.TypeName = "clsPagingProperty";
    //    ods.SelectMethod = "GetData";
    //    ods.SelectCountMethod = "VirtualItemCount";
    //    ods.StartRowIndexParameterName = "startRow";
    //    ods.MaximumRowsParameterName = "maxRows";
    //    ods.EnableViewState = false;

    //    ods.ObjectCreating += new ObjectDataSourceObjectEventHandler(ods_ObjectCreating);

    //    gvShow.DataSource = ods;
    //    gvShow.DataBind();
    //}
    //private void ods_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
    //{

    //    e.ObjectInstance = new clsPagingProperty(_dt, _vic);

    //}
    public object ExecuteScalar(string Query, SqlParameter[] param)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = Query;
            cmd.Connection = con;
            cmd.Connection.Open();
            if (param != null)
            {
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
            }
            cmd.Connection = con;
            if (cmd.Connection.State == ConnectionState.Closed)
                cmd.Connection.Open();

            object objRet = cmd.ExecuteScalar();
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();

            return objRet;


        }
        catch (Exception)
        {
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();
            return null;
        }
        finally
        {
            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();


        }
    }

    
    
    public static void sendSMS(string msg, string mobile)
    {
        try
        {
            //COPSMSService.SMSServiceTestSoapClient sendSMS = new COPSMSService.SMSServiceTestSoapClient();
            //string a = sendSMS.SendSMSUnicodeTestwithSenderid(msg, mobile, "BIHAREDISTRICT-rcs", "Cooperative@fsy1", "a96e628b-473c-4db8-9cac-573bb12b6505", "COP12345SMS", "BRGOVT ");
            //sendsmsservices.SMSService sendSMS = new sendsmsservices.SMSService();
            //string a = sendSMS.NewSendSMSUnicode(msg, mobile, "67890", "BRFSY_FSY");
        }
        catch
        {
        }

    }

}
