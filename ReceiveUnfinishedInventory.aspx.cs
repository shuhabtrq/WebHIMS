using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class ReceiveUnfinishedInventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fnCheckIsPageRefresh(); // This function checks whether page-refresh was caused due to F5 or actual postback

        if (!Page.IsPostBack)
        {           
            VendorsList.DrpVendors.AutoPostBack = true;
            MultiView1.ActiveViewIndex = 0;
            BindUnfinishedInventoryGrid(DALUnfinishedInventory.GetAllUnfinishedInventory());
        }
        else
        {
            string postbackControlName = Page.Request.Params.Get("__EVENTTARGET"); // Get control that caused postback

            if (postbackControlName != null)
            {
                if (postbackControlName == "ctl00$cntBody$VendorsList$drpVendors")
                {
                    txtUIReceivedWeight.Text = "";
                    CalculateAndDisplay_TotalRMAllocatedToSelectedVendor();
                    CalculateAndDisplay_TotalUIReceivedFromSelectedVendor();
                }
            }

            bool bIsPageRefresh = (bool)Session["IsPageRefresh"];
            if (bIsPageRefresh)
            {
                // If it is a F5 refresh
                MultiView1.ActiveViewIndex = Convert.ToInt32(Session["ACTIVE_VIEW_INDEX"]);
                return;
            }

        } // end of ispostback else block

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

    private void CalculateAndDisplay_TotalRMAllocatedToSelectedVendor()
    {
        if (VendorsList.DrpVendors.SelectedIndex > 0)
        {
            double dblSum = DALVendorAllocation.GetTotalRawMaterialAllocatedToVendor(Convert.ToInt32(VendorsList.DrpVendors.SelectedValue));
            lblTotalRMAllocatedToVendor.Text = dblSum.ToString();
        }
        else
        {
            lblTotalRMAllocatedToVendor.Text = "NA";
        }
    }

    private void CalculateAndDisplay_TotalUIReceivedFromSelectedVendor()
    {
        if (VendorsList.DrpVendors.SelectedIndex > 0)
        {
            double dblSum = DALUnfinishedInventory.GetTotalUIReceivedFromVendor(Convert.ToInt32(VendorsList.DrpVendors.SelectedValue));
            lblTotalWeightReceivedTillNow.Text = dblSum.ToString();

            lblBalanceWeight.Text = (Convert.ToDouble(lblTotalRMAllocatedToVendor.Text) - dblSum).ToString();
        }
        else
        {
            lblTotalWeightReceivedTillNow.Text = "NA";
            lblBalanceWeight.Text = "NA"; 
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        lblTotalRMAllocatedToVendor.Text = "NA";
        lblTotalWeightReceivedTillNow.Text = "NA";
        lblBalanceWeight.Text = "NA";
        btnAdd.Visible = true;
        btnUpdate.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Validate UnfinishedInventory
        string zErrorMessage = BALUnfinishedInventory.ValidateUnfinishedInventory(txtUIReceivedWeight.Text, txtUIReceivedBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblUnfinishedInventoryMessage.ForeColor = Color.Red;
            lblUnfinishedInventoryMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALUnfinishedInventory oBALUnfinishedInventory = new BALUnfinishedInventory();
                oBALUnfinishedInventory.VendorID = Convert.ToInt32(VendorsList.DrpVendors.SelectedValue);
                oBALUnfinishedInventory.ItemID = Convert.ToInt32(ItemsList.DrpItems.SelectedValue);
                oBALUnfinishedInventory.UINoOfPieces = Convert.ToInt32(txtPieceCount.Text.Trim());
                oBALUnfinishedInventory.UIWeightReceived = Convert.ToDouble(txtUIReceivedWeight.Text);
                oBALUnfinishedInventory.UIReceivedBy = txtUIReceivedBy.Text.Trim();
                oBALUnfinishedInventory.CreatedOn = DateTime.Now;
                oBALUnfinishedInventory.LastModifiedOn = DateTime.Now;
                oBALUnfinishedInventory.LastModifiedBy = Environment.UserName;

                BindUnfinishedInventoryGrid(DALUnfinishedInventory.AddUnfinishedInventory(oBALUnfinishedInventory));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindUnfinishedInventoryGrid(DALUnfinishedInventory.GetAllUnfinishedInventory()); 
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    private void BindUnfinishedInventoryGrid(List<BALUnfinishedInventory> lstBALUnfinishedInventory)
    {
        try
        {
            Show_Hide_UnfinishedInventoryDivTags(lstBALUnfinishedInventory);

            if (lstBALUnfinishedInventory != null)
            {
                if (lstBALUnfinishedInventory.Count > 0)
                {
                    gvUnfinishedInventoryDetails.DataSource = lstBALUnfinishedInventory;
                    gvUnfinishedInventoryDetails.DataBind();
                    SessionManager.SetSession(SessionManager.LIST_BALUnfinishedInventory, lstBALUnfinishedInventory);

                    if (gvUnfinishedInventoryDetails.PageSize > 10)
                    {
                        divPager.Visible = true;
                        switch (gvUnfinishedInventoryDetails.PageSize)
                        {
                            case 10: drpPager.SelectedIndex = 0; break;
                            case 20: drpPager.SelectedIndex = 1; break;
                            case 50: drpPager.SelectedIndex = 2; break;
                            case 100: drpPager.SelectedIndex = 3; break;
                            case 500: drpPager.SelectedIndex = 4; break;
                            case 1000: drpPager.SelectedIndex = 5; break;
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

    private void Show_Hide_UnfinishedInventoryDivTags(List<BALUnfinishedInventory> lstBALUnfinishedInventory)
    {
        if (lstBALUnfinishedInventory != null)
        {
            if (lstBALUnfinishedInventory.Count > 0)
            {
                divWithUIItem.Visible = true;
                divNoUIItem.Visible = false;
            }
            else
            {
                divWithUIItem.Visible = false;
                divNoUIItem.Visible = true;
            }
        }
        else
        {
            divWithUIItem.Visible = false;
            divNoUIItem.Visible = true;
        }
    }

    private void ClearFields()
    {
        VendorsList.DrpVendors.SelectedIndex = 0;
        ItemsList.DrpItems.SelectedIndex = 0; 
        txtPieceCount.Text = "";
        lblTotalRMAllocatedToVendor.Text = "NA";
        lblTotalWeightReceivedTillNow.Text = "NA";
        lblBalanceWeight.Text = "NA";
        txtUIReceivedWeight.Text = "";
        txtUIReceivedBy.Text = "";        
        lblUnfinishedInventoryMessage.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        MultiView1.ActiveViewIndex = 0;
        Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Validate UnfinishedInventory
        string zErrorMessage = BALUnfinishedInventory.ValidateUnfinishedInventory(txtUIReceivedWeight.Text, txtUIReceivedBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblUnfinishedInventoryMessage.ForeColor = Color.Red;
            lblUnfinishedInventoryMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        if (SessionManager.GetSession(SessionManager.HIMSUnfinishedInventory_OnEdit_UIID) == null)
        {
            return;
        }

        int intUIID = (int)SessionManager.GetSession(SessionManager.HIMSUnfinishedInventory_OnEdit_UIID);

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALUnfinishedInventory oBALUnfinishedInventory = new BALUnfinishedInventory();
                oBALUnfinishedInventory.UIID = intUIID;
                oBALUnfinishedInventory.VendorID = Convert.ToInt32(VendorsList.DrpVendors.SelectedValue);
                oBALUnfinishedInventory.ItemID = Convert.ToInt32(ItemsList.DrpItems.SelectedValue);
                oBALUnfinishedInventory.UINoOfPieces = Convert.ToInt32(txtPieceCount.Text.Trim());
                oBALUnfinishedInventory.UIWeightReceived = Convert.ToDouble(txtUIReceivedWeight.Text);
                oBALUnfinishedInventory.UIReceivedBy = txtUIReceivedBy.Text.Trim();
                oBALUnfinishedInventory.LastModifiedOn = DateTime.Now;
                oBALUnfinishedInventory.LastModifiedBy = Environment.UserName;

                BindUnfinishedInventoryGrid(DALUnfinishedInventory.UpdateUnfinishedInventory(oBALUnfinishedInventory));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindUnfinishedInventoryGrid(DALUnfinishedInventory.GetAllUnfinishedInventory());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvUnfinishedInventoryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "cnEdit")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblUIID = (Label)gvUnfinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblUIID");
                Label lblVendorDisplayName = (Label)gvUnfinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblVendorDisplayName");
                Label lblTotalAllocationRM = (Label)gvUnfinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblTotalAllocationRM");
                Label lblItemDisplayName = (Label)gvUnfinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblItemDisplayName");
                Label lblUINoOfPieces = (Label)gvUnfinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblUINoOfPieces");
                Label lblUIWeightReceived = (Label)gvUnfinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblUIWeightReceived");
                Label lblReceivedBy = (Label)gvUnfinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblReceivedBy");

                SessionManager.SetSession(SessionManager.HIMSUnfinishedInventory_OnEdit_UIID, Convert.ToInt32(lblUIID.Text));

                MultiView1.ActiveViewIndex = 1;

                VendorsList.DrpVendors.SelectedValue = VendorsList.DrpVendors.Items.FindByText(lblVendorDisplayName.Text).Value;
                ItemsList.DrpItems.SelectedValue = ItemsList.DrpItems.Items.FindByText(lblItemDisplayName.Text).Value;
                lblTotalRMAllocatedToVendor.Text = lblTotalAllocationRM.Text;
                CalculateAndDisplay_TotalUIReceivedFromSelectedVendor();
                txtPieceCount.Text = lblUINoOfPieces.Text;
                txtUIReceivedWeight.Text = lblUIWeightReceived.Text;
                txtUIReceivedBy.Text = lblReceivedBy.Text;

                btnAdd.Visible = false;
                btnUpdate.Visible = true;

            }

            else if (e.CommandName == "cnDelete")
            {
                bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

                if (!bIsPageRefresh)
                {
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label lblUIID = (Label)gvUnfinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblUIID");
                    BindUnfinishedInventoryGrid(DALUnfinishedInventory.DeleteUnfinishedInventory(Convert.ToInt32(lblUIID.Text)));
                }
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    protected void gvUnfinishedInventoryDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvUnfinishedInventoryDetails.PageIndex = e.NewPageIndex;
            BindUnfinishedInventoryGrid(DALUnfinishedInventory.GetAllUnfinishedInventory());
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvUnfinishedInventoryDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<BALUnfinishedInventory> lstBALUnfinishedInventory = (List<BALUnfinishedInventory>)SessionManager.GetSession(SessionManager.LIST_BALUnfinishedInventory);

            if (lstBALUnfinishedInventory != null)
            {
                DataTable dt = BALUnfinishedInventory.fnConvertListToDataTable(lstBALUnfinishedInventory);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvUnfinishedInventoryDetails.DataSource = dt;
                gvUnfinishedInventoryDetails.DataBind();
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

    protected void gvUnfinishedInventoryDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            DropDownList ddl = new DropDownList();
            //adds variants of pager size            
            ddl.Items.Add("10");
            ddl.Items.Add("20");
            ddl.Items.Add("50");
            ddl.Items.Add("100");
            ddl.Items.Add("500");
            ddl.Items.Add("1000");
            ddl.AutoPostBack = true;
            //selects item due to the GridView current page size
            ListItem li = ddl.Items.FindByText(gvUnfinishedInventoryDetails.PageSize.ToString());
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
        gvUnfinishedInventoryDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindUnfinishedInventoryGrid(DALUnfinishedInventory.GetAllUnfinishedInventory());
    }

    protected void drpPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvUnfinishedInventoryDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindUnfinishedInventoryGrid(DALUnfinishedInventory.GetAllUnfinishedInventory());
    }
}