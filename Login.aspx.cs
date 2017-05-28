using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtAdminKey.Focus();
    }
    protected void lbUserLogin_Click(object sender, EventArgs e)
    {
        try
        {
            SessionManager.SetSession(SessionManager.HIMSLogin_Admin, null);
            CheckQueryStringAndRedirect();
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        finally
        {
            SessionManager.SetSession(SessionManager.HIMSLogin_Admin, null);  
        }

    }

    private void CheckQueryStringAndRedirect()
    {
        try
        {
            if (Request.QueryString == null)
            {
                SessionManager.SetSession(SessionManager.HIMSLogin_Admin, null);
                return;
            }

            if (Request.QueryString["q"] == null && Request.QueryString["q"] == "")
            {
                SessionManager.SetSession(SessionManager.HIMSLogin_Admin, null);
                Response.Redirect("Home.aspx", true);
            }
            else
            {
                if (Request.QueryString["q"] == "ConfigureVendors")
                {
                    Response.Redirect("ConfigureVendors.aspx", true);
                }
                else if (Request.QueryString["q"] == "ConfigureItems")
                {
                    Response.Redirect("ConfigureItems.aspx", true);
                }
                else
                {
                    SessionManager.SetSession(SessionManager.HIMSLogin_Admin, null);
                    Response.Redirect("Home.aspx", true);
                }

            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message); 
        }

    }

    protected void btnAdminLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateAdminKey())
            {
                return;
            }

            string zAdminKey = ConfigurationManager.AppSettings["AdminLoginKey"];

            if(string.Equals(zAdminKey, txtAdminKey.Value.Trim(), StringComparison.Ordinal))
            {
                SessionManager.SetSession(SessionManager.HIMSLogin_Admin, true);
                CheckQueryStringAndRedirect();
            }
            
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    private bool ValidateAdminKey()
    {
        if (txtAdminKey.Value == "")
        {
            txtAdminKey.Focus();
            return false;
        }

        if (txtAdminKey.MaxLength > 12)
        {
            txtAdminKey.Value = "";
            txtAdminKey.Focus();
            return false;
        }

        return true;
    }
}