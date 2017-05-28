using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class ReceiveFinishedInventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fnCheckIsPageRefresh(); // This function checks whether page-refresh was caused due to F5 or actual postback

        if (!Page.IsPostBack)
        {
            VendorsList.DrpVendors.AutoPostBack = true;
            ItemsList.DrpItems.AutoPostBack = true;
            MultiView1.ActiveViewIndex = 0;
            BindFinishedInventoryGrid(DALFinishedInventory.GetAllFinishedInventory());
        }
        else
        {
            string postbackControlName = Page.Request.Params.Get("__EVENTTARGET"); // Get control that caused postback

            if (postbackControlName != null)
            {
                if (postbackControlName == "ctl00$cntBody$VendorsList$drpVendors"
                    || postbackControlName == "ctl00$cntBody$ItemsList$drpItems")
                {
                    //txtUIReceivedWeight.Text = "";
                    CalculateAndDisplay_TotalSIGivenToVendor();
                    CalculateAndDisplay_TotalSIReceivedFromVendor();
                }
            }
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
        try
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
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
        finally
        {

        }

    }

    private void CalculateAndDisplay_TotalSIGivenToVendor()
    {
        if (VendorsList.DrpVendors.SelectedIndex > 0 && ItemsList.DrpItems.SelectedIndex > 0)
        {
            int intSum = DALFinishingProcess.GetTotalSumOfItemPiecesGivenToVendor(Convert.ToInt32(VendorsList.DrpVendors.SelectedValue), Convert.ToInt32(ItemsList.DrpItems.SelectedValue));
            lblTotalSIGivenToVendor.Text = intSum.ToString();
        }
        else
        {
            lblTotalSIGivenToVendor.Text = "NA";            
        }
    }

    private void CalculateAndDisplay_TotalSIReceivedFromVendor()
    {
        if (VendorsList.DrpVendors.SelectedIndex > 0 && ItemsList.DrpItems.SelectedIndex > 0)
        {
            double intSum = DALFinishedInventory.GetTotalSumOfItemPiecesReceivedFromVendor(Convert.ToInt32(VendorsList.DrpVendors.SelectedValue), Convert.ToInt32(ItemsList.DrpItems.SelectedValue));
            lblTotalSIReceivedTillNow.Text = intSum.ToString();

            lblBalance.Text = (Convert.ToDouble(lblTotalSIGivenToVendor.Text) - intSum).ToString();
        }
        else
        {
            lblTotalSIReceivedTillNow.Text = "NA";
            lblBalance.Text = "NA";
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];
        if (!bIsPageRefresh)
        {
            MultiView1.ActiveViewIndex = 1;
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            lblTotalSIGivenToVendor.Text = "NA";
            lblTotalSIReceivedTillNow.Text = "NA";
            lblBalance.Text = "NA";
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Validate FinishedInventory
        string zErrorMessage = BALFinishedInventory.ValidateFinishedInventory(txtFIReceivedBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblFinishedInventoryMessage.ForeColor = Color.Red;
            lblFinishedInventoryMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALFinishedInventory oBALFinishedInventory = new BALFinishedInventory();
                oBALFinishedInventory.VendorID = Convert.ToInt32(VendorsList.DrpVendors.SelectedValue);
                oBALFinishedInventory.ItemID = Convert.ToInt32(ItemsList.DrpItems.SelectedValue);
                oBALFinishedInventory.FINoOfPieces = Convert.ToInt32(txtPieceCount.Text.Trim());                
                oBALFinishedInventory.FIReceivedBy = txtFIReceivedBy.Text.Trim();
                oBALFinishedInventory.CreatedOn = DateTime.Now;
                oBALFinishedInventory.LastModifiedOn = DateTime.Now;
                oBALFinishedInventory.LastModifiedBy = Environment.UserName;

                BindFinishedInventoryGrid(DALFinishedInventory.AddFinishedInventory(oBALFinishedInventory));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindFinishedInventoryGrid(DALFinishedInventory.GetAllFinishedInventory());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    private void BindFinishedInventoryGrid(List<BALFinishedInventory> lstBALFinishedInventory)
    {
        try
        {
            Show_Hide_FinishedInventoryDivTags(lstBALFinishedInventory);

            if (lstBALFinishedInventory != null)
            {
                if (lstBALFinishedInventory.Count > 0)
                {
                    gvFinishedInventoryDetails.DataSource = lstBALFinishedInventory;
                    gvFinishedInventoryDetails.DataBind();
                    SessionManager.SetSession(SessionManager.LIST_BALFinishedInventory, lstBALFinishedInventory);

                    if (gvFinishedInventoryDetails.PageSize > 10)
                    {
                        divPager.Visible = true;
                        switch (gvFinishedInventoryDetails.PageSize)
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

    private void Show_Hide_FinishedInventoryDivTags(List<BALFinishedInventory> lstBALFinishedInventory)
    {
        if (lstBALFinishedInventory != null)
        {
            if (lstBALFinishedInventory.Count > 0)
            {
                divWithFPItem.Visible = true;
                divNoFPItem.Visible = false;
            }
            else
            {
                divWithFPItem.Visible = false;
                divNoFPItem.Visible = true;
            }
        }
        else
        {
            divWithFPItem.Visible = false;
            divNoFPItem.Visible = true;
        }
    }

    private void ClearFields()
    {
        VendorsList.DrpVendors.SelectedIndex = 0;
        ItemsList.DrpItems.SelectedIndex = 0;
        txtPieceCount.Text = "";       
        txtFIReceivedBy.Text = "";
        lblFinishedInventoryMessage.Text = "";
        lblTotalSIGivenToVendor.Text = "NA";
        lblTotalSIReceivedTillNow.Text = "NA";
        lblBalance.Text = "NA";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        MultiView1.ActiveViewIndex = 0;
        Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Validate FinishedInventory
        string zErrorMessage = BALFinishedInventory.ValidateFinishedInventory(txtFIReceivedBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblFinishedInventoryMessage.ForeColor = Color.Red;
            lblFinishedInventoryMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        if (SessionManager.GetSession(SessionManager.HIMSFinishedInventory_OnEdit_FIID) == null)
        {
            return;
        }

        int intFPID = (int)SessionManager.GetSession(SessionManager.HIMSFinishedInventory_OnEdit_FIID);

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALFinishedInventory oBALFinishedInventory = new BALFinishedInventory();
                oBALFinishedInventory.FIID = intFPID;
                oBALFinishedInventory.VendorID = Convert.ToInt32(VendorsList.DrpVendors.SelectedValue);
                oBALFinishedInventory.ItemID = Convert.ToInt32(ItemsList.DrpItems.SelectedValue);
                oBALFinishedInventory.FINoOfPieces = Convert.ToInt32(txtPieceCount.Text.Trim());                
                oBALFinishedInventory.FIReceivedBy = txtFIReceivedBy.Text.Trim();
                oBALFinishedInventory.LastModifiedOn = DateTime.Now;
                oBALFinishedInventory.LastModifiedBy = Environment.UserName;

                BindFinishedInventoryGrid(DALFinishedInventory.UpdateFinishedInventory(oBALFinishedInventory));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindFinishedInventoryGrid(DALFinishedInventory.GetAllFinishedInventory());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvFinishedInventoryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "cnEdit")
            {
                bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

                if (!bIsPageRefresh)
                {
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Label lblFIID = (Label)gvFinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblFIID");
                    Label lblVendorDisplayName = (Label)gvFinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblVendorDisplayName");
                    Label lblItemDisplayName = (Label)gvFinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblItemDisplayName");
                    Label lblFINoOfPieces = (Label)gvFinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblFINoOfPieces");
                    Label lblFIReceivedBy = (Label)gvFinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblFIReceivedBy");

                    SessionManager.SetSession(SessionManager.HIMSFinishedInventory_OnEdit_FIID, Convert.ToInt32(lblFIID.Text));

                    MultiView1.ActiveViewIndex = 1;

                    VendorsList.DrpVendors.SelectedValue = VendorsList.DrpVendors.Items.FindByText(lblVendorDisplayName.Text).Value;
                    ItemsList.DrpItems.SelectedValue = ItemsList.DrpItems.Items.FindByText(lblItemDisplayName.Text).Value;
                    txtPieceCount.Text = lblFINoOfPieces.Text;                    
                    txtFIReceivedBy.Text = lblFIReceivedBy.Text;
                    CalculateAndDisplay_TotalSIGivenToVendor();
                    CalculateAndDisplay_TotalSIReceivedFromVendor();

                    btnAdd.Visible = false;
                    btnUpdate.Visible = true;
                }

            }

            else if (e.CommandName == "cnDelete")
            {
                bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

                if (!bIsPageRefresh)
                {
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label lblFIID = (Label)gvFinishedInventoryDetails.Rows[gvr.RowIndex].FindControl("lblFIID");
                    BindFinishedInventoryGrid(DALFinishedInventory.DeleteFinishedInventory(Convert.ToInt32(lblFIID.Text)));
                }
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    protected void gvFinishedInventoryDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFinishedInventoryDetails.PageIndex = e.NewPageIndex;
            BindFinishedInventoryGrid(DALFinishedInventory.GetAllFinishedInventory());
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvFinishedInventoryDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<BALFinishedInventory> lstBALFinishedInventory = (List<BALFinishedInventory>)SessionManager.GetSession(SessionManager.LIST_BALFinishedInventory);

            if (lstBALFinishedInventory != null)
            {
                DataTable dt = BALFinishedInventory.fnConvertListToDataTable(lstBALFinishedInventory);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvFinishedInventoryDetails.DataSource = dt;
                gvFinishedInventoryDetails.DataBind();
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

    protected void gvFinishedInventoryDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
            ListItem li = ddl.Items.FindByText(gvFinishedInventoryDetails.PageSize.ToString());
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
        gvFinishedInventoryDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindFinishedInventoryGrid(DALFinishedInventory.GetAllFinishedInventory());
    }

    protected void drpPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvFinishedInventoryDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindFinishedInventoryGrid(DALFinishedInventory.GetAllFinishedInventory());
    }
}