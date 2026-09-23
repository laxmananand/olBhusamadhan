using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for FileSaveServer
/// </summary>
public class FileSaveServer
{
    public FileSaveServer()
    {
        //
        // TODO: Add constructor logic here
        //
    }      
    public static string InsertPDFNew(string _path, string _Photo, string _filename, string _fileExt)
    {
        string msg = "";
        try
        {
            UploadImageService.ImageWebService service1 = new UploadImageService.ImageWebService();           
            string a = service1.InsertPDF(_path, _Photo, _filename, _fileExt);          
            msg = a;
        }
        catch (Exception ee)
        {
            msg = ee.Message;
        }
        return msg;
    }
    public static string InsertPicNew(string _path, string _Photo, string _filename)
    {
        string msg = "";
        try
        {
            UploadImageService.ImageWebService service1 = new UploadImageService.ImageWebService();
            string a = service1.InsertImage(_path, _Photo, _filename);
            msg = a;
        }
        catch (Exception ee)
        {
            msg = ee.Message;
        }
        return msg;
    }
    public static string getBase64(FileUpload FileUpload1)
    {
        BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream);
        byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);
        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        return base64String;
    }
}