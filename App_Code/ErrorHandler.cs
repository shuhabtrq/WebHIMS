using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ErrorHandler
/// </summary>
public class ErrorHandler
{
    /// Handles error by accepting the error message
    /// Displays the page on which the error occured
    public static void WriteError(string errorMessage)
    {
        try
        {
            fnCheckDirectoryExists("~/Errors");
            string path = "~/Errors/" + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
            if (!System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                System.IO.File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
            }
            using (System.IO.StreamWriter w = System.IO.File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine("\r\nLog Entry : ");
                w.WriteLine("{0}", DateTime.Now.ToString(System.Globalization.CultureInfo.InvariantCulture));
                string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                              ". Error Message: " + errorMessage;
                w.WriteLine(err);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }
        }
        catch (Exception ex)
        {
            WriteError(ex.Message);
        }

    }

    public static void fnCheckDirectoryExists(string zDirectoryPathAndName)
    {
        bool IsExists = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(zDirectoryPathAndName));

        if (!IsExists)
        {
            //If folder/directory at given path does not exist, then create it.
            System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(zDirectoryPathAndName));
        }
    }

}
