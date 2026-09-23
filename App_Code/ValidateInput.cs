using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for ValidateInput
/// </summary>
public class ValidateInput
{
	public ValidateInput()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static Boolean CheckSqlInjection(string str)
    {
        if (str.Contains("'") || str.Contains(";")) return false;
        else return true;

    }
    public bool CheckSqlInjectionNew(string str)
    {
        String[] mychar = new String[99];

        String[] sqlInjTextBox = new String[] { "--", ";--", ";", "/*", "*/", "@@",  "char", "nchar", "varchar", "nvarchar", "alter", "begin", "cast", "create", "cursor", "declare", "delete", "drop",   
                                            "exec", "execute", "fetch", "insert", "select", "sys", "sysobjects", "syscolumns", "table", "update","drop","truncate","alter", "sleep", "table_name",      
                                            "table_schema", "information_schema", "sp_", "!", "%", "*", "<", ">", "'", "\"" , "{", "}", "[", "]", "|", "'", "\""};

        mychar = sqlInjTextBox;
        for (int j = 0; j <= mychar.Length - 1; j++)
        {
            string Val = Convert.ToString(str);
            if (Val.IndexOf(mychar[j]) > -1)
                return false;
        }
        return true;


        //if (str.Contains("'") || str.Contains(";")) return false;
        //else return true;

    }
}