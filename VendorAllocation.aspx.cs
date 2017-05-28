using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class VendorAllocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fnCheckIsPageRefresh(); // This function checks whether page-refresh was caused due to F5 or actual postback

        if (!Page.IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            GetSetAndDisplay_TotalRawMaterialProcured();
            BindVendorAllocationGrid(DALVendorAllocation.GetAllVendorAllocation());            
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

        lblCurrentStockHidden.Style.Add("display", "none"); 

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

    private void GetSetAndDisplay_TotalRawMaterialProcured()
    {
        try
        {
            double dblTotalRMProcuredWeight;

            if (SessionManager.GetSession(SessionManager.TotalRawMaterialProcured) != null)
            {
                dblTotalRMProcuredWeight = (double)SessionManager.GetSession(SessionManager.TotalRawMaterialProcured);
                lblTotalRawMaterialProcured.Text = "Total Raw Material Procured: " + dblTotalRMProcuredWeight + " kgs";
            }
            else
            {
                // If null, then get latest from database
                dblTotalRMProcuredWeight = DALRawMaterial.GetSumOfTotalRawMaterialProcured();
                lblTotalRawMaterialProcured.Text = "Total Raw Material Procured: " + dblTotalRMProcuredWeight + " kgs";
                SessionManager.SetSession(SessionManager.TotalRawMaterialProcured, dblTotalRMProcuredWeight);
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
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
        #region Validate Vendor Allocation
        string zErrorMessage = BALVendorAllocation.ValidateVendorAllocationFields(txtVAWeight.Text, txtVARate.Text, txtVAGivenBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblVendorAllocationMessage.ForeColor = Color.Red;
            lblVendorAllocationMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                double dblVAWeight = Convert.ToDouble(txtVAWeight.Text);
                double dblVARatePerKg = Convert.ToDouble(txtVARate.Text);
                double dblTotalRate = dblVAWeight * dblVARatePerKg;

                BALVendorAllocation oBALVendorAllocation = new BALVendorAllocation();
                oBALVendorAllocation.VendorID = Convert.ToInt32(VendorsList.DrpVendors.SelectedValue);
                oBALVendorAllocation.VAWeight = dblVAWeight;
                oBALVendorAllocation.VARate = dblTotalRate;
                oBALVendorAllocation.VAGivenByName = txtVAGivenBy.Text.Trim();
                oBALVendorAllocation.CreatedOn = DateTime.Now;
                oBALVendorAllocation.LastModifiedOn = DateTime.Now;
                oBALVendorAllocation.LastModifiedBy = Environment.UserName;

                BindVendorAllocationGrid(DALVendorAllocation.AddVendorAllocation(oBALVendorAllocation));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindVendorAllocationGrid(DALVendorAllocation.GetAllVendorAllocation()); 
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    private void BindVendorAllocationGrid(List<BALVendorAllocation> lstBALVendorAllocation)
    {
        try
        {
            Show_Hide_VADivTags(lstBALVendorAllocation);

            if (lstBALVendorAllocation != null)
            {
                if (lstBALVendorAllocation.Count > 0)
                {
                    gvVendorAllocationDetails.DataSource = lstBALVendorAllocation;
                    gvVendorAllocationDetails.DataBind();
                    SessionManager.SetSession(SessionManager.LIST_BALVendorAllocation, lstBALVendorAllocation);

                    if (gvVendorAllocationDetails.PageSize > 10)
                    {
                        divPager.Visible = true;
                        switch (gvVendorAllocationDetails.PageSize)
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

        finally
        {
            CalculateAndDisplayTotalVendorAllocation(lstBALVendorAllocation);
        }

    }

    private void CalculateAndDisplayTotalVendorAllocation(List<BALVendorAllocation> lstBALVendorAllocation)
    {
        try
        {
            double dblTotalVAWeight = 0.0;

            if (lstBALVendorAllocation != null)
            {
                if (lstBALVendorAllocation.Count > 0)
                {
                    dblTotalVAWeight = lstBALVendorAllocation.Sum(e => e.VAWeight);
                    lblTotalRawMaterialAllocated.Text = "Total Raw Material Allocated: " + dblTotalVAWeight + " kgs";
                }
                else
                {
                    lblTotalRawMaterialAllocated.Text = "Total Raw Material Allocated: 0.00 kgs";
                }
            }
            else
            {
                lblTotalRawMaterialAllocated.Text = "Total Raw Material Allocated: 0.00 kgs";
            }

            CalculateAndDisplayCurrentRMStock(dblTotalVAWeight);
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message); 
        }

    }

    private void CalculateAndDisplayCurrentRMStock(double dblTotalVAWeight)
    {
        try
        {
            if (dblTotalVAWeight == 0.0)
            {
                lblRawMaterialLeftAfterAllocation.Text = "0.00";
                return;
            }

            double dblTotalRMProcuredWeight = (double)SessionManager.GetSession(SessionManager.TotalRawMaterialProcured);

            double dblCurrentStock = dblTotalRMProcuredWeight - dblTotalVAWeight;

            lblRawMaterialLeftAfterAllocation.Text = dblCurrentStock.ToString();
            lblCurrentStockHidden.Text = dblCurrentStock.ToString();

            if (dblCurrentStock < 0.0)
            {
                lblRawMaterialLeftAfterAllocation.BackColor = Color.Red; 
                lblRawMaterialLeftAfterAllocation.ForeColor = Color.White;
            }
            else
            {
                lblRawMaterialLeftAfterAllocation.BackColor = Color.Transparent; 
                lblRawMaterialLeftAfterAllocation.ForeColor = Color.Black;                
            }
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    private void Show_Hide_VADivTags(List<BALVendorAllocation> lstBALVendorAllocation)
    {
        if (lstBALVendorAllocation != null)
        {
            if (lstBALVendorAllocation.Count > 0)
            {
                divWithVAItem.Visible = true;
                divNoVAItem.Visible = false;
            }
            else
            {
                divWithVAItem.Visible = false;
                divNoVAItem.Visible = true;
            }
        }
        else
        {
            divWithVAItem.Visible = false;
            divNoVAItem.Visible = true;
        }
    }

    private void ClearFields()
    {
        VendorsList.DrpVendors.SelectedIndex = 0;
        txtVAWeight.Text = "";
        txtVARate.Text = "";        
        txtVAGivenBy.Text = "";
        lblVendorAllocationMessage.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        MultiView1.ActiveViewIndex = 0;
        Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Validate Vendor Allocation
        string zErrorMessage = BALVendorAllocation.ValidateVendorAllocationFields(txtVAWeight.Text, txtVARate.Text, txtVAGivenBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblVendorAllocationMessage.ForeColor = Color.Red;
            lblVendorAllocationMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        if (SessionManager.GetSession(SessionManager.HIMSVendorAllocation_OnEdit_VAID) == null)
        {
            return;
        }

        int intVAID = (int)SessionManager.GetSession(SessionManager.HIMSVendorAllocation_OnEdit_VAID);

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                double dblVAWeight = Convert.ToDouble(txtVAWeight.Text);
                double dblVARatePerKg = Convert.ToDouble(txtVARate.Text);
                double dblTotalRate = dblVAWeight * dblVARatePerKg;

                BALVendorAllocation oBALVendorAllocation = new BALVendorAllocation();
                oBALVendorAllocation.VAID = intVAID;
                oBALVendorAllocation.VendorID = Convert.ToInt32(VendorsList.DrpVendors.SelectedValue);
                oBALVendorAllocation.VAWeight = dblVAWeight;
                oBALVendorAllocation.VARate = dblTotalRate;
                oBALVendorAllocation.VAGivenByName = txtVAGivenBy.Text.Trim();
                oBALVendorAllocation.LastModifiedOn = DateTime.Now;
                oBALVendorAllocation.LastModifiedBy = Environment.UserName;

                BindVendorAllocationGrid(DALVendorAllocation.UpdateVendorAllocation(oBALVendorAllocation));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindVendorAllocationGrid(DALVendorAllocation.GetAllVendorAllocation());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvVendorAllocationDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "cnEdit")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblVAID = (Label)gvVendorAllocationDetails.Rows[gvr.RowIndex].FindControl("lblVAID");
                Label lblVendorDisplayName = (Label)gvVendorAllocationDetails.Rows[gvr.RowIndex].FindControl("lblVendorDisplayName");
                Label lblVAWeight = (Label)gvVendorAllocationDetails.Rows[gvr.RowIndex].FindControl("lblVAWeight");
                Label lblVARate = (Label)gvVendorAllocationDetails.Rows[gvr.RowIndex].FindControl("lblVARate");
                Label lblVAGivenByName = (Label)gvVendorAllocationDetails.Rows[gvr.RowIndex].FindControl("lblVAGivenByName");

                SessionManager.SetSession(SessionManager.HIMSVendorAllocation_OnEdit_VAID, Convert.ToInt32(lblVAID.Text));

                MultiView1.ActiveViewIndex = 1;

                VendorsList.DrpVendors.SelectedValue = VendorsList.DrpVendors.Items.FindByText(lblVendorDisplayName.Text).Value;
                txtVAWeight.Text = lblVAWeight.Text;
                txtVARate.Text = (Convert.ToDouble(lblVARate.Text) / Convert.ToDouble(lblVAWeight.Text)).ToString("0.00");
                txtVAGivenBy.Text = lblVAGivenByName.Text;

                btnAdd.Visible = false;
                btnUpdate.Visible = true;

            }

            else if (e.CommandName == "cnDelete")
            {
                bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

                if (!bIsPageRefresh)
                {
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label lblVAID = (Label)gvVendorAllocationDetails.Rows[gvr.RowIndex].FindControl("lblVAID");
                    BindVendorAllocationGrid(DALVendorAllocation.DeleteVendorAllocation(Convert.ToInt32(lblVAID.Text)));
                }
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    protected void gvVendorAllocationDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvVendorAllocationDetails.PageIndex = e.NewPageIndex;
            BindVendorAllocationGrid(DALVendorAllocation.GetAllVendorAllocation());
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvVendorAllocationDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<BALVendorAllocation> lstBALVendorAllocation = (List<BALVendorAllocation>)SessionManager.GetSession(SessionManager.LIST_BALVendorAllocation);

            if (lstBALVendorAllocation != null)
            {
                DataTable dt = BALVendorAllocation.fnConvertListToDataTable(lstBALVendorAllocation);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvVendorAllocationDetails.DataSource = dt;
                gvVendorAllocationDetails.DataBind();
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

    protected void gvVendorAllocationDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
            ListItem li = ddl.Items.FindByText(gvVendorAllocationDetails.PageSize.ToString());
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
        gvVendorAllocationDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        //binds data source
        BindVendorAllocationGrid(DALVendorAllocation.GetAllVendorAllocation());
        //gvVendorAllocationDetails.DataSource = DALVendorAllocation.GetAllVendorAllocation();
        //gvVendorAllocationDetails.DataBind();
    }

    protected void drpPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvVendorAllocationDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindVendorAllocationGrid(DALVendorAllocation.GetAllVendorAllocation());
        //gvVendorAllocationDetails.DataSource = DALVendorAllocation.GetAllVendorAllocation();
        //gvVendorAllocationDetails.DataBind();
    }
}