using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for sqlinjec
/// </summary>
public class sqlinjec
{
    public sqlinjec()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public bool sqlinj_postmethod(int injectiontype)
    {
        //bool b;
        int formlen = 0;
        int count = 0;
        String all_post_values = "";
        String all_get_values = "";
        //int i = 99;
        String[] mychar = new String[99]; { }


        String[] mychar1 = new String[] { "!", "%", "^", "*", "=", "<", ">", "\"" };


        String[] mycharsk22 = new String[] { "!", "%", "*", "=", "<", ">", "'", "\"", "(", ")", "~", "?", ";", "{", "}", "[", "]", "|", "'" };


        String[] mysql = new String[] { "drop", "insert", "exec", "sp_", "delete" };


        String[] ReportCharArray = new String[] { "@", "!", "$", "%", "^", "*", "~" };







        if (injectiontype == 1)
        {
            mychar = mychar1;
        }

        if (injectiontype == 2)
        {
            mychar = ReportCharArray;
        }


        if (injectiontype == 3)
        {
            mychar = mycharsk22;
        }




        all_get_values = System.Web.HttpContext.Current.Request.QueryString.ToString();


        for (int j = 0; j <= mychar.Length - 1; j++)
        {
            if (all_get_values.IndexOf(mychar[j]) > -1)
            {
                count = 1;
                break;
            }
        }
        for (int j = 0; j <= mysql.Length - 1; j++)
        {
            if (all_get_values.IndexOf(mysql[j]) > -1)
            {
                count = 1;
                break;
            }
        }

        formlen = System.Web.HttpContext.Current.Request.Form.AllKeys.Length;

        for (int i = 0; i <= formlen - 1; i++)
        {

            if (System.Web.HttpContext.Current.Request.Form.AllKeys[i].Substring(0, 1) == "_")
            {
                // break;
                //goto 10;
                // string s = "s";
            }









            else
            {


                all_post_values = System.Web.HttpContext.Current.Request.Form.Get(i).ToLower();


                for (int j = 0; j <= mychar.Length - 1; j++)
                {
                    if (all_post_values.IndexOf(mychar[j]) > -1)
                    {
                        count = 1;
                        break;
                    }
                }
                for (int j = 0; j <= mysql.Length - 1; j++)
                {
                    if (all_post_values.IndexOf(mysql[j]) > -1)
                    {
                        count = 1;
                        break;
                    }
                }

            }


        }

        if (count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


}