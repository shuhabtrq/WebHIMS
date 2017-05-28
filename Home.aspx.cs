using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            AddAuthorMetaTag();
        }
    }

    protected void AddAuthorMetaTag()
    {
        System.Web.UI.HtmlControls.HtmlMeta metatagAuthor = new System.Web.UI.HtmlControls.HtmlMeta();
        metatagAuthor.Name = "author";
        metatagAuthor.Content = "Shuhab (www.shuhab.com)";
        Page.Header.Controls.Add(metatagAuthor);
    }

    protected void lbConfigureRawMaterial_Click(object sender, EventArgs e)
    {
        Response.Redirect("RawMaterial.aspx", true);
    }
    protected void lbConfigureVendors_Click(object sender, EventArgs e)
    {
        if (SessionManager.GetSession(SessionManager.HIMSLogin_Admin) == null)
        {
            Response.Redirect("Login.aspx?q=ConfigureVendors", true);
        }
        else
        {
            Response.Redirect("ConfigureVendors.aspx", true);
        }
    }

    protected void lbConfigureItems_Click(object sender, EventArgs e)
    {
        if (SessionManager.GetSession(SessionManager.HIMSLogin_Admin) == null)
        {
            Response.Redirect("Login.aspx?q=ConfigureItems", true);
        }
        else
        {
            Response.Redirect("ConfigureItems.aspx", true);
        }
    }
    
    protected void lbVendorAllocation_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendorAllocation.aspx", true);
    }
    protected void lbUnfinishedInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiveUnfinishedInventory.aspx", true);
    }
    protected void lbFinishingProcess_Click(object sender, EventArgs e)
    {
        Response.Redirect("FinishingProcess.aspx", true);
    }
    protected void lbFinishedInventory_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReceiveFinishedInventory.aspx", true);
    }
    protected void lbVendorBills_Click(object sender, EventArgs e)
    {
        Response.Redirect("VendorBilling.aspx", true);
    }
    protected void lbReports_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reports.aspx", true);
    }
}