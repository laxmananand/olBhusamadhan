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

/// <summary>
/// Summary description for clsDataAccessLayer
/// </summary>
public class ClsDataAccesseReport
{
    SqlConnection con = new SqlConnection();
    SqlCommand cmd = new SqlCommand();
    public string FieldName;
    public string[] ArrFieldName;
    public object[] FieldValue;
    public ClsDataAccesseReport()
    {
        con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["LandDisputeConnectionString"].ConnectionString;

    }
    public DataTable GetDataTable(string query)
    {
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            SqlDataAdapter adap1 = new SqlDataAdapter();
            cmd.Connection = con;
            cmd.CommandTimeout = 90;//
            adap1.SelectCommand = cmd;
            adap1.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {
            return dt;
        }
        finally
        {
            con.Close();
        }

    }
    public DataSet GetDataset(string query, List<SqlParameter> param)
    {
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            if (param != null)
            {
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
            }
            SqlDataAdapter adap1 = new SqlDataAdapter();
            cmd.Connection = con;
            adap1.SelectCommand = cmd;
            adap1.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            return ds;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet getDataSet()
    {
        DataSet ds = new DataSet();
        try
        {
            ParameterAsArray();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            return ds;
        }
        // Dispose(false);
        return ds;
    }
    public DataSet getDataSet(string ProcName)
    {
        DataSet ds = new DataSet();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(ProcName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Prepare();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

        }
        catch (Exception ex)
        {
            con.Close();
            return ds;

        }

        return ds;
    }
    public DataSet getDataSet(string ProcName, object fieldValue)
    {

        DataSet ds = new DataSet();
        try
        {
            con.Open();
            //  SqlCommand cmd = new SqlCommand();
            // cmd = new SqlCommand(ProcName, con);  
            string Query = "Exec " + ProcName + " '" + fieldValue + "'";

            SqlDataAdapter da = new SqlDataAdapter(Query.Trim(), con);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            con.Close();
            return ds;
        }

        return ds;
    }
    public DataSet getDataSet(string ProcName, object[] fieldValue)
    {

        DataSet ds = new DataSet();
        try
        {
            con.Open();
            // SqlCommand cmd = new SqlCommand();
            // cmd = new SqlCommand(ProcName, con);

            string Query = "Exec " + ProcName;

            for (int i = 0; i < fieldValue.Length; i++)
            {
                if (i == 0)
                    Query += " '" + fieldValue[i] + "'";
                else
                    Query += ",'" + fieldValue[i] + "'";
            }

            SqlDataAdapter da = new SqlDataAdapter(Query.Trim(), con);
            da.Fill(ds);

        }
        catch (Exception ex)
        {
            con.Close();
            return ds;
        }

        return ds;
    }
    public DataSet getDataSet(string ProcName, string fieldName, object[] fieldValue)
    {
        DataSet ds = new DataSet();
        try
        {

            string[] FName = fieldName.Split(',');
            if (FName.Length == fieldValue.Length)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand(ProcName, con);

                for (int i = 0; i < FName.Length; i++)
                {
                    if (cmd == null)
                    {
                        cmd = new SqlCommand("", con);
                    }
                    if (fieldName[0].ToString() == "@")
                        cmd.Parameters.Add(Convert.ToString(FName[i]), SqlDbType.VarChar).Value = fieldValue[i];
                    else
                    {
                        cmd.Parameters.Add("@" + Convert.ToString(FName[i]), SqlDbType.VarChar).Value = fieldValue[i];
                    }
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Prepare();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            else
            {
                //Message = "Field name and value are not matched.";
            }

        }
        catch (Exception ex)
        {
            con.Close();
            return ds;
        }

        return ds;
    }

    public DataTable GetDataTable(string query, List<SqlParameter> param)
    {

        DataTable dt = new DataTable();
        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            if (param != null)
            {
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
            }
            SqlDataAdapter adap1 = new SqlDataAdapter();
            cmd.Connection = con;
            cmd.CommandTimeout = 90;
            adap1.SelectCommand = cmd;
            adap1.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {

            return dt;
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
            cmd.CommandTimeout = 90;
            adap1.SelectCommand = cmd;
            adap1.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {

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
            if (param != null)
            {
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
            }
            SqlDataAdapter adap1 = new SqlDataAdapter();
            cmd.Connection = con;
            adap1.SelectCommand = cmd;
            adap1.Fill(dt);
            return dt;
        }
        catch (Exception ex)
        {

            return dt;
        }

        finally
        {
            con.Close();
        }

    }

    void ParameterAsArray()
    {

        if (FieldName.Trim().Length > 0)
            ArrFieldName = FieldName.Split(',');
        if (ArrFieldName != null && ArrFieldName != null)
        {
            if (ArrFieldName.Length > 0 && ArrFieldName.Length > 0)
            {
                if (ArrFieldName.Length != FieldValue.Length)
                {
                    // Message = "No. of field and values are not same";
                }
                else
                {
                    for (int i = 0; i < ArrFieldName.Length; i++)
                    {
                        if (ArrFieldName[0].ToString() == "@")
                            cmd.Parameters.Add(ArrFieldName[i], SqlDbType.VarChar).Value = FieldValue[i];
                        else
                        {
                            cmd.Parameters.Add("@" + ArrFieldName[i], SqlDbType.VarChar).Value = FieldValue[i];
                        }
                    }
                    cmd.CommandTimeout = 90;//
                }
            }
            else
            {
            }
        }
    }
    public void NewStoreProcedure(string ProcName)
    {
        cmd = new SqlCommand(ProcName, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Prepare();
    }

    public DataTable getData()
    {
        DataTable dt = new DataTable();
        try
        {
            ParameterAsArray();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (ConnectionState.Open == con.State || ConnectionState.Broken == con.State)
            {
                con.Close();
            }
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            return dt;
        }

        return dt;
    }
    public DataTable getData(string ProcName)
    {
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            NewStoreProcedure(ProcName);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (ConnectionState.Open == con.State || ConnectionState.Broken == con.State)
            {
                con.Close();
            }
            da.Fill(dt);

        }
        catch (Exception ex)
        {
            con.Close();
            return dt;
        }
        return dt;
    }
    public DataTable getData(string ProcName, object fieldValue)
    {

        DataTable dt = new DataTable();
        try
        {
            con.Open();
            string Query = "Exec " + ProcName + " '" + fieldValue + "'";

            cmd = new SqlCommand(Query, con);
            cmd.Prepare();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (ConnectionState.Open == con.State || ConnectionState.Broken == con.State)
            {
                con.Close();
            }
            da.Fill(dt);

        }
        catch (Exception ex)
        {
            con.Close();
            return dt;
        }
        return dt;
    }
    public DataTable getData(string ProcName, object[] fieldValue)
    {
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            string Query = "Exec " + ProcName;
            for (int i = 0; i < fieldValue.Length; i++)
            {
                if (i == 0)
                    Query += " '" + fieldValue[i] + "'";
                else
                    Query += ",'" + fieldValue[i] + "'";
            }

            cmd = new SqlCommand(Query, con);
            cmd.Prepare();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            if (ConnectionState.Open == con.State || ConnectionState.Broken == con.State)
            {
                con.Close();
            }
            da.Fill(dt);
        }

        catch (Exception ex)
        {
            con.Close();
            return dt;
        }
        return dt;
    }
    public DataTable getData(string ProcName, string fieldName, object[] fieldValue)
    {
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(ProcName, con);
            string[] FName = fieldName.Split(',');
            if (FName.Length == fieldValue.Length)
            {
                NewStoreProcedure(ProcName);
                for (int i = 0; i < FName.Length; i++)
                {
                    cmd.Parameters.Add(Convert.ToString(FName[i]), SqlDbType.VarChar).Value = fieldValue[i];
                }
            }
            else
            {
                // Message = "Field name and value are not matched.";
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Prepare();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
        }
        catch (Exception ex)
        {
            con.Close();
            return dt;
        }

        return dt;
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



    public int ExecuteSql(string Query, SqlParameter[] param)
    {

        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = Query;
            cmd.Parameters.Clear();
            if (param != null)
            {
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);
                }
            }
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            // HttpContext.Current.Response.Write(ex.Message);

            return 0;

        }

        finally
        {
            con.Close();
        }



    }

    public int ExecuteSql(string Query, List<SqlParameter> param)
    {

        try
        {
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = Query;
            if (param != null)
            {
                foreach (SqlParameter prm in param)
                {


                    cmd.Parameters.Add(prm);

                }
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

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = Query;
            if (param != null)
            {
                foreach (SqlParameter prm in param)
                {
                    cmd.Parameters.Add(prm);

                }
            }
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
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
        catch (Exception ex)
        {
            cmd.Connection.Close();
            return "";
        }
        finally
        {
            cmd.Connection.Close();

        }
    }
    public object ExecuteScalarObject(string strSql)
    {

        SqlCommand cmd = new SqlCommand();
        try
        {

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;
            cmd.Connection = con;
            cmd.Connection.Open();

            return cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            cmd.Connection.Close();
            return null;
        }
        finally
        {
            cmd.Connection.Close();

        }
    }


    public byte[] ExecuteScalar(string strSql, SqlParameter[] param)
    {

        SqlCommand cmd = new SqlCommand();
        try
        {
            con.Open();


            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSql;
            foreach (SqlParameter prm in param)
            {
                cmd.Parameters.Add(prm);
            }
            cmd.Connection = con;

            return (byte[])cmd.ExecuteScalar();
        }
        catch (Exception ex)
        {
            cmd.Connection.Close();
            return null;
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



    public int ExecuteSqlTrans(string SqlStr)
    {
        try
        {

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = SqlStr;
            return cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            return 0;
        }
    }
    public void PopulateDropdown(string strQuery, DropDownList ddl, SqlParameter[] param, string Prompt)
    {
        PopulateDropdowndt(new ClsDataAccesseReport().GetDataTable(strQuery, param), ddl, Prompt);
    }
    public void PopulateDropdown(string strQuery, DropDownList ddl, string Prompt)
    {
        PopulateDropdowndt(new ClsDataAccesseReport().GetDataTable(strQuery), ddl, Prompt);
    }
    public void PopulateDropdowndt(DataTable dt, DropDownList ddl, string Prompt)
    {
        ddl.DataSource = dt;
        ddl.DataValueField = dt.Columns[0].ColumnName;
        ddl.DataTextField = dt.Columns[1].ColumnName;

        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(Prompt, "0"));

    }

}