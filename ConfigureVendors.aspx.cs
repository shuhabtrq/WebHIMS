using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class ConfigureVendors : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        fnCheckIsPageRefresh(); // This function checks whether page-refresh was caused due to F5 or actual postback

        if (!Page.IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            BindVendorGrid(DALVendors.GetAllVendors());
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
        #region Validate Vendor
        string zErrorMessage = BALVendors.ValidateVendor(txtVendorName.Text, txtVendorOrgName.Text, txtVendorEmailID.Text,
                                                         txtVendorPhone.Text, txtVendorAddress.Text, txtVendorAdditionalInfo.Text);

        if (zErrorMessage != string.Empty)
        {
            lblVendorMessage.ForeColor = Color.Red;
            lblVendorMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALVendors oBALVendor = new BALVendors();
                oBALVendor.VendorName = txtVendorName.Text.Trim();
                oBALVendor.VendorOrgName = txtVendorOrgName.Text.Trim();
                oBALVendor.VendorEmailID = txtVendorEmailID.Text.Trim();
                oBALVendor.VendorPhone = txtVendorPhone.Text.Trim();
                oBALVendor.VendorAddress = txtVendorAddress.Text.Trim();
                oBALVendor.VendorAdditionalInfo = txtVendorAdditionalInfo.Text.Trim();
                oBALVendor.VendorIsActive = true;

                BindVendorGrid(DALVendors.AddVendor(oBALVendor));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindVendorGrid(DALVendors.GetAllVendors());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    private void BindVendorGrid(List<BALVendors> lstBALVendors)
    {
        try
        {
            Show_Hide_VendorDivTags(lstBALVendors);

            if (lstBALVendors != null)
            {
                if (lstBALVendors.Count > 0)
                {
                    gvVendorDetails.DataSource = lstBALVendors;
                    gvVendorDetails.DataBind();
                    SessionManager.SetSession(SessionManager.LIST_BALVendors, lstBALVendors);

                    if (gvVendorDetails.PageSize > 10)
                    {
                        divPager.Visible = true;
                        switch (gvVendorDetails.PageSize)
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

    private void Show_Hide_VendorDivTags(List<BALVendors> lstBALVendors)
    {
        if (lstBALVendors != null)
        {
            if (lstBALVendors.Count > 0)
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
        txtVendorName.Text = "";
        txtVendorOrgName.Text = "";
        txtVendorEmailID.Text = "";
        txtVendorPhone.Text = ""; 
        txtVendorAddress.Text = "";
        txtVendorAdditionalInfo.Text = ""; 
        lblVendorMessage.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        MultiView1.ActiveViewIndex = 0;
        Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Validate Vendor
        string zErrorMessage = BALVendors.ValidateVendor(txtVendorName.Text, txtVendorOrgName.Text, txtVendorEmailID.Text,
                                                         txtVendorPhone.Text, txtVendorAddress.Text, txtVendorAdditionalInfo.Text);

        if (zErrorMessage != string.Empty)
        {
            lblVendorMessage.ForeColor = Color.Red;
            lblVendorMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        if (SessionManager.GetSession(SessionManager.HIMSVendor_OnEdit_VendorID) == null)
        {
            return;
        }

        int intVendorID = (int)SessionManager.GetSession(SessionManager.HIMSVendor_OnEdit_VendorID);

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALVendors oBALVendor = new BALVendors();
                oBALVendor.VendorID = intVendorID;
                oBALVendor.VendorName = txtVendorName.Text.Trim();
                oBALVendor.VendorOrgName = txtVendorOrgName.Text.Trim();
                oBALVendor.VendorEmailID = txtVendorEmailID.Text.Trim();
                oBALVendor.VendorPhone = txtVendorPhone.Text.Trim();
                oBALVendor.VendorAddress = txtVendorAddress.Text.Trim();
                oBALVendor.VendorAdditionalInfo = txtVendorAdditionalInfo.Text.Trim();
                oBALVendor.VendorIsActive = true;

                BindVendorGrid(DALVendors.UpdateVendor(oBALVendor));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindVendorGrid(DALVendors.GetAllVendors());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvVendorDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "cnEdit")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblVendorID = (Label)gvVendorDetails.Rows[gvr.RowIndex].FindControl("lblVendorID");
                Label lblVendorName = (Label)gvVendorDetails.Rows[gvr.RowIndex].FindControl("lblVendorName");
                Label lblVendorOrgName = (Label)gvVendorDetails.Rows[gvr.RowIndex].FindControl("lblVendorOrgName");
                Label lblVendorEmailID = (Label)gvVendorDetails.Rows[gvr.RowIndex].FindControl("lblVendorEmailID");
                Label lblVendorPhone = (Label)gvVendorDetails.Rows[gvr.RowIndex].FindControl("lblVendorPhone");
                Label lblVendorAddress = (Label)gvVendorDetails.Rows[gvr.RowIndex].FindControl("lblVendorAddress");
                Label lblVendorAdditionalInfo = (Label)gvVendorDetails.Rows[gvr.RowIndex].FindControl("lblVendorAdditionalInfo");

                SessionManager.SetSession(SessionManager.HIMSVendor_OnEdit_VendorID, Convert.ToInt32(lblVendorID.Text));

                MultiView1.ActiveViewIndex = 1;

                txtVendorName.Text = lblVendorName.Text;
                txtVendorOrgName.Text = lblVendorOrgName.Text;
                txtVendorEmailID.Text = lblVendorEmailID.Text;
                txtVendorPhone.Text = lblVendorPhone.Text;
                txtVendorAddress.Text = lblVendorAddress.Text;
                txtVendorAdditionalInfo.Text = lblVendorAdditionalInfo.Text;  

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
                        Label lblVendorID = (Label)gvVendorDetails.Rows[gvr.RowIndex].FindControl("lblVendorID");
                        BindVendorGrid(DALVendors.SoftDeleteVendor(Convert.ToInt32(lblVendorID.Text)));
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

    protected void gvVendorDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvVendorDetails.PageIndex = e.NewPageIndex;
            BindVendorGrid(DALVendors.GetAllVendors());
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvVendorDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<BALVendors> lstBALVendors = (List<BALVendors>)SessionManager.GetSession(SessionManager.LIST_BALVendors);

            if (lstBALVendors != null)
            {
                DataTable dt = BALVendors.fnConvertListToDataTable(lstBALVendors);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvVendorDetails.DataSource = dt;
                gvVendorDetails.DataBind();
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

    protected void gvVendorDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
            ListItem li = ddl.Items.FindByText(gvVendorDetails.PageSize.ToString());
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
        gvVendorDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);        
        BindVendorGrid(DALVendors.GetAllVendors());        
    }

    protected void drpPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvVendorDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindVendorGrid(DALVendors.GetAllVendors());       
    }
}