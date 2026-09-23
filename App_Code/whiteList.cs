using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for whiteList
/// </summary>
public class whiteList
{
	public whiteList()
	{
		//
		// TODO: Add constructor logic here
		//
    }
    private const string _patternAlpha = @"^[A-Za-z\.\s]*$";
    private const string _patternNumeric = @"^[0-9\.]*$";
    private const string _patterneMail = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
    private const string _patterneAlphaNumeric = @"^[a-zA-Z0-9]+$";
   // private const string _patterneDate = @"(0[1-9]|[12][0-9]|3[01])[-](0[1-9]|1[012])[-]\d{4}";
    private const string _patterneDate = @"(0[1-9]|1[012])[/](0[1-9]|[12][0-9]|3[01])[/]\d{4}";

    public bool isAlpha(string txtToCheck)
    {
        if (Regex.IsMatch(txtToCheck, _patternAlpha))
            return true;
        else
            return false;
    }

    public bool isNumeric(string txtToCheck)
    {
        if (Regex.IsMatch(txtToCheck, _patternNumeric))
            return true;
        else
            return false;
    }
    public bool isEMail(string txtToCheck)
    {
        if (Regex.IsMatch(txtToCheck, _patterneMail))
            return true;
        else
            return false;
    }
    public bool isAlphaNumeric(string txtToCheck)
    {
        if (Regex.IsMatch(txtToCheck, _patterneAlphaNumeric))
            return true;
        else
            return false;
    }
    public bool isDate(string txtToCheck)
    {
        if (Regex.IsMatch(txtToCheck, _patterneDate))
            return true;
        else
            return false;
    }
}