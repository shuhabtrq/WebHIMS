using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class VendorBilling : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        fnCheckIsPageRefresh(); // This function checks whether page-refresh was caused due to F5 or actual postback

        if (!Page.IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            BindVendorBillingGrid(DALVendorBilling.GetAllVendorBilling());
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
    
    private void BindVendorBillingGrid(List<BALVendorBilling> lstBALVendorBilling)
    {
        try
        {
            Show_Hide_VendorDivTags(lstBALVendorBilling);

            if (lstBALVendorBilling != null)
            {
                if (lstBALVendorBilling.Count > 0)
                {
                    gvVendorBillingDetails.DataSource = lstBALVendorBilling;
                    gvVendorBillingDetails.DataBind();
                    SessionManager.SetSession(SessionManager.LIST_BALVendorBilling, lstBALVendorBilling);

                    if (gvVendorBillingDetails.PageSize > 10)
                    {
                        divPager.Visible = true;
                        switch (gvVendorBillingDetails.PageSize)
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

    private void Show_Hide_VendorDivTags(List<BALVendorBilling> lstBALVendorBilling)
    {
        if (lstBALVendorBilling != null)
        {
            if (lstBALVendorBilling.Count > 0)
            {
                divWithVendorItem.Visible = true;
                divNoVendorItem.Visible = false;
            }
            else
            {
                divWithVendorItem.Visible = false;
                divNoVendorItem.Visible = true;
            }
        }
        else
        {
            divWithVendorItem.Visible = false;
            divNoVendorItem.Visible = true;
        }
    }

    private void ClearFields()
    {
        txtVBAmountPaid.Text = "";
        txtVBDiscount.Text = "";
        txtVBWriteOff.Text = "";        
        lblVendorBillingMessage.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        MultiView1.ActiveViewIndex = 0;
        Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Validate Vendor Billing

        if (txtVBAmountPaid.Text.Trim() == "")
        {
            txtVBAmountPaid.Text = "0";
        }

        if (txtVBDiscount.Text.Trim() == "")
        {
            txtVBDiscount.Text = "0";
        }

        if (txtVBWriteOff.Text.Trim() == "")
        {
            txtVBWriteOff.Text = "0";
        }

        string zErrorMessage = BALVendorBilling.ValidateVendorBilling(txtVBAmountPaid.Text, txtVBDiscount.Text, txtVBWriteOff.Text);

        if (zErrorMessage != string.Empty)
        {
            lblVendorBillingMessage.ForeColor = Color.Red;
            lblVendorBillingMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        if (SessionManager.GetSession(SessionManager.HIMSVendorBilling_OnEdit_VendorID) == null)
        {
            return;
        }

        int intVendorID = (int)SessionManager.GetSession(SessionManager.HIMSVendorBilling_OnEdit_VendorID);
        List<BALVendorBilling> lstVendorBill = DALVendorBilling.GetSingleVendorBill(intVendorID); 

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                double dblVBAmountPaid = Convert.ToDouble(txtVBAmountPaid.Text.Trim());
                double dblVBDiscount = Convert.ToDouble(txtVBDiscount.Text.Trim());
                double dblVBWriteOff = Convert.ToDouble(txtVBWriteOff.Text.Trim());

                double dblVBAmountPaidTillNow = dblVBAmountPaid + lstVendorBill[0].VBAmountPaid;
                double dblVBDiscountTillNow = dblVBDiscount + lstVendorBill[0].VBDiscount;
                double dblVBWriteOffTillNow = dblVBWriteOff + lstVendorBill[0].VBWriteOff;

                BALVendorBilling oBALVendorBilling = new BALVendorBilling();
                oBALVendorBilling.VendorID = intVendorID;
                oBALVendorBilling.VBAmountPaid = dblVBAmountPaidTillNow;
                oBALVendorBilling.VBDiscount = dblVBDiscountTillNow;
                oBALVendorBilling.VBWriteOff = dblVBWriteOffTillNow;
                oBALVendorBilling.VBTotalAmountPaid = dblVBAmountPaidTillNow + dblVBDiscountTillNow - dblVBWriteOffTillNow; // x = a + b -c
                oBALVendorBilling.LastModifiedOn = DateTime.Now;
                oBALVendorBilling.LastModifiedBy = Environment.UserName;
                oBALVendorBilling.VendorIsActive = true;

                BindVendorBillingGrid(DALVendorBilling.UpdateVendorBilling(oBALVendorBilling));

                bool bOk = AddNewBillingTransactionRecordEntry(intVendorID);

                if (!bOk)
                {
                    lblVendorBillingMessage.Text = "Error: Please check your connection and try again!";
                    return;
                }
                else
                {
                    lblVendorBillingMessage.Text = "";
                }

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindVendorBillingGrid(DALVendorBilling.GetAllVendorBilling());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected bool AddNewBillingTransactionRecordEntry(int intVendorID)
    {
        try
        {
            BALBilling oBilling = new BALBilling();
            oBilling.VendorID = intVendorID;
            oBilling.AmountPaid = Convert.ToDouble(txtVBAmountPaid.Text.Trim());
            oBilling.Discount = Convert.ToDouble(txtVBDiscount.Text.Trim());
            oBilling.WriteOff = Convert.ToDouble(txtVBWriteOff.Text.Trim());
            oBilling.IsActive = true;
            oBilling.CreatedOn = DateTime.Now;
            oBilling.LastModifiedOn = DateTime.Now;
            oBilling.LastModifiedBy = Environment.UserName;

            bool bOk = DALBilling.AddNewBillingTransaction(oBilling);
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
            return false;
        }

        return true;

    }

    protected void gvVendorBillingDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "cnEdit")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblVendorID = (Label)gvVendorBillingDetails.Rows[gvr.RowIndex].FindControl("lblVendorID");
                Label lblVendorDisplayName = (Label)gvVendorBillingDetails.Rows[gvr.RowIndex].FindControl("lblVendorDisplayName");
                Label lblVBAmountPaid = (Label)gvVendorBillingDetails.Rows[gvr.RowIndex].FindControl("lblVBAmountPaid");
                Label lblVBDiscount = (Label)gvVendorBillingDetails.Rows[gvr.RowIndex].FindControl("lblVBDiscount");
                Label lblVBWriteOff = (Label)gvVendorBillingDetails.Rows[gvr.RowIndex].FindControl("lblVBWriteOff");
                Label lblTotalSUMToBePaid = (Label)gvVendorBillingDetails.Rows[gvr.RowIndex].FindControl("lblTotalSUMToBePaid");
                Label lblVBTotalAmountPaid = (Label)gvVendorBillingDetails.Rows[gvr.RowIndex].FindControl("lblVBTotalAmountPaid");
                Label lblBalanceAmt = (Label)gvVendorBillingDetails.Rows[gvr.RowIndex].FindControl("lblBalanceAmount");

                SessionManager.SetSession(SessionManager.HIMSVendorBilling_OnEdit_VendorID, Convert.ToInt32(lblVendorID.Text));

                MultiView1.ActiveViewIndex = 1;

                lblFullVendorDisplayName.Text = lblVendorDisplayName.Text; // Vendor Display Name  
                lblTotalAmountToBePaidToVendor.Text = lblTotalSUMToBePaid.Text;      // Total amount to be paid to vendor
                lblTotalAmountPaidTillNowToVendor.Text = lblVBTotalAmountPaid.Text;  // Total amount paid till now to vendor (x=a+b-c)

                lblAmountPaidTillNow.Text = lblVBAmountPaid.Text;   // Amount Paid (Rs) 
                lblTotalDiscountTillNow.Text = lblVBDiscount.Text;  // Total Discount (Rs)
                lblTotalWriteOffTillNow.Text = lblVBWriteOff.Text;  // Total Write Off(Rs)
                lblTotalBalanceAmount.Text = lblBalanceAmt.Text;    // Balance(Rs)

                txtVBAmountPaid.Text = "0";
                txtVBDiscount.Text = "0";
                txtVBWriteOff.Text = "0"; 
                
                btnUpdate.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    protected void gvVendorBillingDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvVendorBillingDetails.PageIndex = e.NewPageIndex;
            BindVendorBillingGrid(DALVendorBilling.GetAllVendorBilling());
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvVendorBillingDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<BALVendorBilling> lstBALVendorBilling = (List<BALVendorBilling>)SessionManager.GetSession(SessionManager.LIST_BALVendorBilling);

            if (lstBALVendorBilling != null)
            {
                DataTable dt = BALVendorBilling.fnConvertListToDataTable(lstBALVendorBilling);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvVendorBillingDetails.DataSource = dt;
                gvVendorBillingDetails.DataBind();
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

    protected void gvVendorBillingDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
            ListItem li = ddl.Items.FindByText(gvVendorBillingDetails.PageSize.ToString());
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
        gvVendorBillingDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindVendorBillingGrid(DALVendorBilling.GetAllVendorBilling());
    }

    protected void drpPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvVendorBillingDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindVendorBillingGrid(DALVendorBilling.GetAllVendorBilling());
    }
}