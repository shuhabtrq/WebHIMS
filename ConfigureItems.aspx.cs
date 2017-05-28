using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class ConfigureItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        fnCheckIsPageRefresh(); // This function checks whether page-refresh was caused due to F5 or actual postback

        if (!Page.IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            BindItemGrid(DALItems.GetAllItems());
        }
        else
        {
            bool bIsPageRefresh = (bool)Session["IsPageRefresh"];
            if (bIsPageRefresh)
            {
                // If it is a F5 refresh
                MultiView1.ActiveViewIndex = Convert.ToInt32(Session["ACTIVE_VIEW_INDEX"]);
                return;
            }
        }
    }

    /// <summary>
    /// Sometimes F5(browser-refresh) is clicked on a page by mistake. 
    /// This function 'fnCheckIsPageRefresh()' checks whether page-refresh was caused due to F5(browser-refresh) or actual postback
    /// </summary>
    private void fnCheckIsPageRefresh()
    {
        bool IsPageRefresh = false;
        Session["IsPageRefresh"] = IsPageRefresh;

        if (!Page.IsPostBack)
        {
            ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
            Session["SessionId"] = ViewState["ViewStateId"].ToString();
        }
        else
        {
            if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
            {
                IsPageRefresh = true;
                Session["IsPageRefresh"] = IsPageRefresh;
            }

            Session["SessionId"] = System.Guid.NewGuid().ToString();
            ViewState["ViewStateId"] = Session["SessionId"].ToString();
        }

    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Validate Item
        string zErrorMessage = BALItems.ValidateItem(txtItemName.Text, txtItemDescription.Text);

        if (zErrorMessage != string.Empty)
        {
            lblItemMessage.ForeColor = Color.Red;
            lblItemMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALItems oBALItem = new BALItems();
                oBALItem.ItemName = txtItemName.Text.Trim();
                oBALItem.ItemDescription = txtItemDescription.Text.Trim();
                oBALItem.ItemIsActive = true;

                BindItemGrid(DALItems.AddItem(oBALItem));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindItemGrid(DALItems.GetAllItems());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    private void BindItemGrid(List<BALItems> lstBALItems)
    {
        try
        {
            Show_Hide_ItemDivTags(lstBALItems);

            if (lstBALItems != null)
            {
                if (lstBALItems.Count > 0)
                {
                    gvItemDetails.DataSource = lstBALItems;
                    gvItemDetails.DataBind();
                    SessionManager.SetSession(SessionManager.LIST_BALItems, lstBALItems);

                    if (gvItemDetails.PageSize > 10)
                    {
                        divPager.Visible = true;
                        switch (gvItemDetails.PageSize)
                        {
                            case 10: drpPager.SelectedIndex = 0; break;
                            case 20: drpPager.SelectedIndex = 1; break;
                            case 50: drpPager.SelectedIndex = 2; break;
                            default: drpPager.SelectedIndex = 0; break;
                        }
                    }
                    else
                    {
                        divPager.Visible = false;
                    }
                }
                else
                {
                    divPager.Visible = false;
                }
            }
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    private void Show_Hide_ItemDivTags(List<BALItems> lstBALItems)
    {
        if (lstBALItems != null)
        {
            if (lstBALItems.Count > 0)
            {
                divWithItem.Visible = true;
                divNoItem.Visible = false;
            }
            else
            {
                divWithItem.Visible = false;
                divNoItem.Visible = true;
            }
        }
        else
        {
            divWithItem.Visible = false;
            divNoItem.Visible = true;
        }
    }

    private void ClearFields()
    {
        txtItemName.Text = "";        
        txtItemDescription.Text = "";
        lblItemMessage.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        MultiView1.ActiveViewIndex = 0;
        Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Validate Item
        string zErrorMessage = BALItems.ValidateItem(txtItemName.Text, txtItemDescription.Text);

        if (zErrorMessage != string.Empty)
        {
            lblItemMessage.ForeColor = Color.Red;
            lblItemMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        if (SessionManager.GetSession(SessionManager.HIMSItem_OnEdit_ItemID) == null)
        {
            return;
        }

        int intItemID = (int)SessionManager.GetSession(SessionManager.HIMSItem_OnEdit_ItemID);

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALItems oBALItem = new BALItems();
                oBALItem.ItemID = intItemID;
                oBALItem.ItemName = txtItemName.Text.Trim();
                oBALItem.ItemDescription = txtItemDescription.Text.Trim();
                oBALItem.ItemIsActive = true;

                BindItemGrid(DALItems.UpdateItem(oBALItem));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindItemGrid(DALItems.GetAllItems());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvItemDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "cnEdit")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblItemID = (Label)gvItemDetails.Rows[gvr.RowIndex].FindControl("lblItemID");
                Label lblItemName = (Label)gvItemDetails.Rows[gvr.RowIndex].FindControl("lblItemName");
                Label lblItemDescription = (Label)gvItemDetails.Rows[gvr.RowIndex].FindControl("lblItemDescription");                

                SessionManager.SetSession(SessionManager.HIMSItem_OnEdit_ItemID, Convert.ToInt32(lblItemID.Text));

                MultiView1.ActiveViewIndex = 1;

                txtItemName.Text = lblItemName.Text;
                txtItemDescription.Text = lblItemDescription.Text;                

                btnAdd.Visible = false;
                btnUpdate.Visible = true;

            }

            else if (e.CommandName == "cnDelete")
            {
                bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

                if (!bIsPageRefresh)
                {
                    if (SessionManager.GetSession(SessionManager.HIMSLogin_Admin) != null)
                    {
                        GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                        Label lblItemID = (Label)gvItemDetails.Rows[gvr.RowIndex].FindControl("lblItemID");
                        BindItemGrid(DALItems.SoftDeleteItem(Convert.ToInt32(lblItemID.Text)));
                    }
                    else
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "You do not have necessary permissions to perform this action. Please login as administrator and try again!";
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    protected void gvItemDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvItemDetails.PageIndex = e.NewPageIndex;
            BindItemGrid(DALItems.GetAllItems());
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvItemDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<BALItems> lstBALItems = (List<BALItems>)SessionManager.GetSession(SessionManager.LIST_BALItems);

            if (lstBALItems != null)
            {
                DataTable dt = BALItems.fnConvertListToDataTable(lstBALItems);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvItemDetails.DataSource = dt;
                gvItemDetails.DataBind();
            }
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    private string GetSortDirection(string column)
    {
        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";

        // Retrieve the last column that was sorted.
        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null)
        {
            // Check if the same column is being sorted.
            // Otherwise, the default value can be returned.
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        // Save new values in ViewState.
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;

        return sortDirection;

    }

    protected void gvItemDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            DropDownList ddl = new DropDownList();
            //adds variants of pager size            
            ddl.Items.Add("10");
            ddl.Items.Add("20");
            ddl.Items.Add("50");
            ddl.AutoPostBack = true;
            //selects item due to the GridView current page size
            ListItem li = ddl.Items.FindByText(gvItemDetails.PageSize.ToString());
            if (li != null)
            {
                ddl.SelectedIndex = ddl.Items.IndexOf(li);
            }
            ddl.SelectedIndexChanged += new EventHandler(ddl_SelectedIndexChanged);

            //adds dropdownlist in the additional cell to the pager table
            Table pagerTable = e.Row.Cells[0].Controls[0] as Table;
            TableCell cell = new TableCell();
            cell.Style["padding-left"] = "10px";
            cell.Style.Add("forecolor", "black");
            cell.Controls.Add(new LiteralControl(" | Page Size: "));
            cell.ForeColor = System.Drawing.Color.Black;
            cell.Controls.Add(ddl);
            pagerTable.Rows[0].Cells.Add(cell);
        }
    }

    protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        //changes page size
        gvItemDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindItemGrid(DALItems.GetAllItems());
    }

    protected void drpPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvItemDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindItemGrid(DALItems.GetAllItems());
    }
}