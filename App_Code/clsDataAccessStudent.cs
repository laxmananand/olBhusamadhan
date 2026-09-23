using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Security.Application;

/// <summary>
/// Summary description for clsDataAccessBSPCL
/// </summary>
public class clsDataAccessStudent
{
    SqlConnection con = new SqlConnection();
    SqlTransaction Trans;

    public clsDataAccessStudent()
	{
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["StudentConnectionString"].ConnectionString;
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
            adap1.Dispose();
            return dt;
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
            con.Close();
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
            adap1.Dispose();
            return dt;

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
            con.Close();
            return dt;
        }

        finally
        {
            con.Close();
        }

    }


    public DataTable GetDataTableWithProc(string ProcName, SqlParameter[] param)
    {
        DataTable dt = new DataTable();
        try
        {
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            foreach (SqlParameter prm in param)
            {
                cmd.Parameters.Add(prm);
            }
            SqlDataAdapter adap1 = new SqlDataAdapter();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            adap1.SelectCommand = cmd;
            adap1.Fill(dt);
            adap1.Dispose();
            return dt;

        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");           
            return dt;
        }

       

    }

    public int ExecuteProcedure(string ProcedureName, SqlParameter[] param)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcedureName;
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
            cmd.Parameters.Clear();
            cmd.Dispose();
            cmd = null;
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
            ExceptionLogging.SendErrorToText(ex);
            HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
            con.Close();
            return 0;

        }

        finally
        {
            con.Close();
        }
    }

    

    public int ExecuteSql(string Query, SqlParameter[] param, Label lblMsg)
    {

        try
        {


            SqlCommand SqlCmd = new SqlCommand();
            SqlCmd.Connection = con;
            SqlCmd.Transaction = Trans;

            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandText = Query;
            SqlCmd.Parameters.Clear();
            foreach (SqlParameter prm in param)
            {
                SqlCmd.Parameters.Add(prm);
            }
            SqlCmd.Connection = con;
            return SqlCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lblMsg.Text = Encoder.HtmlEncode(ex.Message);
            con.Close();
            return 0;
        }
    }

    public string ExecuteScalar(string strSql)
    {
        
        try
        {
            SqlCommand cmd = new SqlCommand();
            //cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.Connection.Open();
            return cmd.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {

            ExceptionLogging.SendErrorToText(ex);
            HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
            con.Close();
            return "";
        }
        finally
        {
            
            con.Close();
        }
    }

    public object ExecuteScalar1(string strSql, SqlParameter[] param)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            con.Open();



            cmd.CommandText = strSql;
            foreach (SqlParameter prm in param)
            {
                cmd.Parameters.Add(prm);
            }
            cmd.Connection = con;
            //cmd.Connection.Open();
            return (object)cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
            con.Close();
            return null;
        }
        finally
        {
            //cmd.Connection.Close();
            con.Close();
        }
    }

    public byte[] ExecuteScalar(string strSql, SqlParameter[] param)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            con.Open();
            
          

            cmd.CommandText = strSql;
            foreach (SqlParameter prm in param)
            {
                cmd.Parameters.Add(prm);
            }
            cmd.Connection = con;
            //cmd.Connection.Open();
            return (byte[])cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
            con.Close();
            return null;
        }
        finally
        {
            //cmd.Connection.Close();
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
        catch (Exception ex)
        {
            return 0;

        }

        finally
        {
            con.Close();
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

    public int ExecuteSqlTrans(string SqlStr)
    {
        SqlCommand cmd = new SqlCommand();
        try
        {
            
            //cmd.CommandType = CommandType.Text;
            cmd.CommandText = SqlStr;
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ExceptionLogging.SendErrorToText(ex);
            HttpContext.Current.Response.Write("Some Technical Error occurred,Please visit after some time");
            con.Close();
            return 0;
        }
        finally
        {
            //cmd.Connection.Close();
            con.Close();
        }
    }

    public string getPath()
    {
        string Eurl = "http://fts.bih.nic.in";
        return Eurl;
    }
}