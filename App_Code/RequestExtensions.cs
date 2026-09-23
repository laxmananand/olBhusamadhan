using System;
using System.Web;

/// <summary>
/// Summary description for RequestExtensions
/// </summary>
public class RequestExtensions
{
    System.Web.HttpRequest request = HttpContext.Current.Request;
    public RequestExtensions()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool IsLocalUrl(string url)
    {
        if (string.IsNullOrEmpty(url))
        {
            return false;
        }

        Uri absoluteUri;
        if (Uri.TryCreate(url, UriKind.Absolute, out absoluteUri))
        {
            return String.Equals(request.Url.Host, absoluteUri.Host,
                        StringComparison.OrdinalIgnoreCase);
        }
        else
        {
            bool isLocal = !url.StartsWith("http:", StringComparison.OrdinalIgnoreCase)
                && !url.StartsWith("https:", StringComparison.OrdinalIgnoreCase)
                && Uri.IsWellFormedUriString(url, UriKind.Relative);
            return isLocal;
        }
    }

}