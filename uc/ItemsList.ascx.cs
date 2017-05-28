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

public partial class uc_ItemsList : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!Page.IsPostBack)
        {
            BindItems();
        }
    }

    public DropDownList DrpItems
    {
        get { return drpItems; }

        set { drpItems = value; }
    }

    protected void BindItems()
    {
        List<BALItems> lstBALItems = DALItems.GetAllItems();

        if (lstBALItems != null)
        {
            if (lstBALItems.Count > 0)
            {
                lstBALItems.OrderBy(x => x.ItemName);

                drpItems.DataSource = lstBALItems;
                drpItems.DataValueField = "ItemID";
                drpItems.DataTextField = "ItemName";
                drpItems.DataBind();
                drpItems.Items.Insert(0, new ListItem("-- Select Item --", "0"));
            }
        }

    }       

}