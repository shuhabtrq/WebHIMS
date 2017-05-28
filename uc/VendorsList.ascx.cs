using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.EntityModel;
using System.Data.Entity;
using MyEntityDataModel;

public partial class uc_VendorsList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindVendors();
        }
    }

    public DropDownList DrpVendors { 
        
        get { return drpVendors; }

        set { drpVendors = value; }
    }

    protected void BindVendors()
    {
        try
        {
            List<BALVendors> lstBALVendors = DALVendors.GetAllVendors();            

            if (lstBALVendors != null)
            {
                if (lstBALVendors.Count > 0)
                {
                    lstBALVendors.OrderBy(x => x.VendorName);

                    drpVendors.DataSource = lstBALVendors;
                    drpVendors.DataValueField = "VendorID";
                    drpVendors.DataTextField = "VendorDisplayName";
                    drpVendors.DataBind();
                    drpVendors.Items.Insert(0, new ListItem("-- Select Vendor --", "0"));
                }
            }
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message); 
        }

    }

}