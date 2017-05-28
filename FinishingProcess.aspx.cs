using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class FinishingProcess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fnCheckIsPageRefresh(); // This function checks whether page-refresh was caused due to F5 or actual postback

        if (!Page.IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            BindFinishingProcessGrid(DALFinishingProcess.GetAllFinishingProcess());            
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

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];
        if (!bIsPageRefresh)
        {
            MultiView1.ActiveViewIndex = 1;
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        #region Validate FinishingProcess
        string zErrorMessage = BALFinishingProcess.ValidateFinishingProcess(txtFPTotalRate.Text, txtFPGivenBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblFinishingProcessMessage.ForeColor = Color.Red;
            lblFinishingProcessMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALFinishingProcess oBALFinishingProcess = new BALFinishingProcess();
                oBALFinishingProcess.VendorID = Convert.ToInt32(VendorsList.DrpVendors.SelectedValue);
                oBALFinishingProcess.ItemID = Convert.ToInt32(ItemsList.DrpItems.SelectedValue);
                oBALFinishingProcess.FPNoOfPieces = Convert.ToInt32(txtPieceCount.Text.Trim());
                oBALFinishingProcess.FPTotalRate = Convert.ToDouble(txtFPTotalRate.Text);
                oBALFinishingProcess.FPGivenByName = txtFPGivenBy.Text.Trim();
                oBALFinishingProcess.CreatedOn = DateTime.Now;
                oBALFinishingProcess.LastModifiedOn = DateTime.Now;
                oBALFinishingProcess.LastModifiedBy = Environment.UserName;

                BindFinishingProcessGrid(DALFinishingProcess.AddFinishingProcess(oBALFinishingProcess));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindFinishingProcessGrid(DALFinishingProcess.GetAllFinishingProcess()); 
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    private void BindFinishingProcessGrid(List<BALFinishingProcess> lstBALFinishingProcess)
    {
        try
        {
            Show_Hide_FinishingProcessDivTags(lstBALFinishingProcess);

            if (lstBALFinishingProcess != null)
            {
                if (lstBALFinishingProcess.Count > 0)
                {
                    gvFinishingProcessDetails.DataSource = lstBALFinishingProcess;
                    gvFinishingProcessDetails.DataBind();
                    SessionManager.SetSession(SessionManager.LIST_BALFinishingProcess, lstBALFinishingProcess);

                    if (gvFinishingProcessDetails.PageSize > 10)
                    {
                        divPager.Visible = true;
                        switch (gvFinishingProcessDetails.PageSize)
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

    private void Show_Hide_FinishingProcessDivTags(List<BALFinishingProcess> lstBALFinishingProcess)
    {
        if (lstBALFinishingProcess != null)
        {
            if (lstBALFinishingProcess.Count > 0)
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
        txtFPTotalRate.Text = "";
        txtFPGivenBy.Text = "";
        lblFinishingProcessMessage.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields();
        MultiView1.ActiveViewIndex = 0;
        Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Validate FinishingProcess
        string zErrorMessage = BALFinishingProcess.ValidateFinishingProcess(txtFPTotalRate.Text, txtFPGivenBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblFinishingProcessMessage.ForeColor = Color.Red;
            lblFinishingProcessMessage.Text = zErrorMessage;
            return;
        }
        #endregion        

        if (SessionManager.GetSession(SessionManager.HIMSFinishingProcess_OnEdit_FPID) == null)
        {
            return;
        }

        int intFPID = (int)SessionManager.GetSession(SessionManager.HIMSFinishingProcess_OnEdit_FPID);

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                BALFinishingProcess oBALFinishingProcess = new BALFinishingProcess();
                oBALFinishingProcess.FPID = intFPID;
                oBALFinishingProcess.VendorID = Convert.ToInt32(VendorsList.DrpVendors.SelectedValue);
                oBALFinishingProcess.ItemID = Convert.ToInt32(ItemsList.DrpItems.SelectedValue);
                oBALFinishingProcess.FPNoOfPieces = Convert.ToInt32(txtPieceCount.Text.Trim());
                oBALFinishingProcess.FPTotalRate = Convert.ToDouble(txtFPTotalRate.Text);
                oBALFinishingProcess.FPGivenByName = txtFPGivenBy.Text.Trim();
                oBALFinishingProcess.LastModifiedOn = DateTime.Now;
                oBALFinishingProcess.LastModifiedBy = Environment.UserName;

                BindFinishingProcessGrid(DALFinishingProcess.UpdateFinishingProcess(oBALFinishingProcess));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindFinishingProcessGrid(DALFinishingProcess.GetAllFinishingProcess());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvFinishingProcessDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "cnEdit")
            {
                bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

                if (!bIsPageRefresh)
                {
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    Label lblFPID = (Label)gvFinishingProcessDetails.Rows[gvr.RowIndex].FindControl("lblFPID");
                    Label lblVendorDisplayName = (Label)gvFinishingProcessDetails.Rows[gvr.RowIndex].FindControl("lblVendorDisplayName");
                    Label lblItemDisplayName = (Label)gvFinishingProcessDetails.Rows[gvr.RowIndex].FindControl("lblItemDisplayName");
                    Label lblFPNoOfPieces = (Label)gvFinishingProcessDetails.Rows[gvr.RowIndex].FindControl("lblFPNoOfPieces");
                    Label lblFPTotalRate = (Label)gvFinishingProcessDetails.Rows[gvr.RowIndex].FindControl("lblFPTotalRate");
                    Label lblFPGivenByName = (Label)gvFinishingProcessDetails.Rows[gvr.RowIndex].FindControl("lblFPGivenByName");

                    SessionManager.SetSession(SessionManager.HIMSFinishingProcess_OnEdit_FPID, Convert.ToInt32(lblFPID.Text));

                    MultiView1.ActiveViewIndex = 1;

                    VendorsList.DrpVendors.SelectedValue = VendorsList.DrpVendors.Items.FindByText(lblVendorDisplayName.Text).Value;
                    ItemsList.DrpItems.SelectedValue = ItemsList.DrpItems.Items.FindByText(lblItemDisplayName.Text).Value;
                    txtPieceCount.Text = lblFPNoOfPieces.Text;
                    txtFPTotalRate.Text = lblFPTotalRate.Text;
                    txtFPGivenBy.Text = lblFPGivenByName.Text;

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
                    Label lblFPID = (Label)gvFinishingProcessDetails.Rows[gvr.RowIndex].FindControl("lblFPID");
                    BindFinishingProcessGrid(DALFinishingProcess.DeleteFinishingProcess(Convert.ToInt32(lblFPID.Text)));
                }
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }

    protected void gvFinishingProcessDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvFinishingProcessDetails.PageIndex = e.NewPageIndex;
            BindFinishingProcessGrid(DALFinishingProcess.GetAllFinishingProcess());
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvFinishingProcessDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<BALFinishingProcess> lstBALFinishingProcess = (List<BALFinishingProcess>)SessionManager.GetSession(SessionManager.LIST_BALFinishingProcess);

            if (lstBALFinishingProcess != null)
            {
                DataTable dt = BALFinishingProcess.fnConvertListToDataTable(lstBALFinishingProcess);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvFinishingProcessDetails.DataSource = dt;
                gvFinishingProcessDetails.DataBind();
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

    protected void gvFinishingProcessDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
            ListItem li = ddl.Items.FindByText(gvFinishingProcessDetails.PageSize.ToString());
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
        gvFinishingProcessDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindFinishingProcessGrid(DALFinishingProcess.GetAllFinishingProcess());
    }

    protected void drpPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvFinishingProcessDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindFinishingProcessGrid(DALFinishingProcess.GetAllFinishingProcess());
    }
}