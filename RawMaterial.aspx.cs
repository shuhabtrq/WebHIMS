using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

public partial class RawMaterial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        fnCheckIsPageRefresh(); // This function checks whether page-refresh was caused due to F5 or actual postback

        if (!Page.IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            BindRawMaterialGrid(DALRawMaterial.GetAllRawMaterial());
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
        #region Validate Raw Material
        string zErrorMessage = BALRawMaterial.ValidateRawMaterial(txtRMWeight.Text, txtRMPurchaseRate.Text,
                                                                  txtRMOrigin.Text, txtRMReceivedBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblRMMessage.ForeColor = Color.Red;
            lblRMMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                double dblRMWeight = Convert.ToDouble(txtRMWeight.Text);
                double dblRMPurchaseRatePerKg = Convert.ToDouble(txtRMPurchaseRate.Text);
                double dblTotalPurchaseRate = dblRMWeight * dblRMPurchaseRatePerKg;

                BALRawMaterial oBALRawMaterial = new BALRawMaterial();
                oBALRawMaterial.RMWeight = dblRMWeight;
                oBALRawMaterial.RMPurchaseRate = dblTotalPurchaseRate;
                oBALRawMaterial.RMOrigin = txtRMOrigin.Text.Trim();
                oBALRawMaterial.RMReceivedBy = txtRMReceivedBy.Text.Trim();
                oBALRawMaterial.CreatedOn = DateTime.Now;
                oBALRawMaterial.LastModifiedOn = DateTime.Now;
                oBALRawMaterial.LastModifiedBy = Environment.UserName;

                BindRawMaterialGrid(DALRawMaterial.AddRawMaterial(oBALRawMaterial));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindRawMaterialGrid(DALRawMaterial.GetAllRawMaterial());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    private void BindRawMaterialGrid(List<BALRawMaterial> lstBALRawMaterial)
    {
        try
        {
            Show_Hide_RMDivTags(lstBALRawMaterial);

            if (lstBALRawMaterial != null)
            {
                if (lstBALRawMaterial.Count > 0)
                {
                    gvRawMaterialDetails.DataSource = lstBALRawMaterial;
                    gvRawMaterialDetails.DataBind();
                    SessionManager.SetSession(SessionManager.LIST_BALRawMaterial, lstBALRawMaterial);

                    if (gvRawMaterialDetails.PageSize > 10)
                    {
                        divPager.Visible = true;
                        switch (gvRawMaterialDetails.PageSize)
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
            CalculateAndDisplayTotalRawMaterial(lstBALRawMaterial);
        }

    }

    private void CalculateAndDisplayTotalRawMaterial(List<BALRawMaterial> lstBALRawMaterial)
    {
        if (lstBALRawMaterial != null)
        {
            if (lstBALRawMaterial.Count > 0)
            {
                double dblTotalRMWeight = lstBALRawMaterial.Sum(e => e.RMWeight);
                lblTotalRawMaterial.Text = "Total Raw Material Procured: " + dblTotalRMWeight + " kgs";
                SessionManager.SetSession(SessionManager.TotalRawMaterialProcured, dblTotalRMWeight); 
            }
            else
            {
                lblTotalRawMaterial.Text = "Total Raw Material Procured: 0.00 kgs";
            }
        }
        else
        {
            lblTotalRawMaterial.Text = "Total Raw Material Procured: 0.00 kgs";
        }  
     
    }

    private void Show_Hide_RMDivTags(List<BALRawMaterial> lstBALRawMaterial)
    {   
        if (lstBALRawMaterial != null)
        {
            if (lstBALRawMaterial.Count > 0)
            {
                divWithRMItem.Visible = true;
                divNoRMItem.Visible = false;
            }
            else
            {
                divWithRMItem.Visible = false;
                divNoRMItem.Visible = true;
            }
        }
        else
        {
            divWithRMItem.Visible = false;
            divNoRMItem.Visible = true;
        }
    }

    private void ClearFields()
    {
        txtRMWeight.Text = "";
        txtRMPurchaseRate.Text = "";
        txtRMOrigin.Text = "";
        txtRMReceivedBy.Text = "";
        lblRMMessage.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearFields(); 
        MultiView1.ActiveViewIndex = 0;
        Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        #region Validate Raw Material
        string zErrorMessage = BALRawMaterial.ValidateRawMaterial(txtRMWeight.Text, txtRMPurchaseRate.Text,
                                                                  txtRMOrigin.Text, txtRMReceivedBy.Text);

        if (zErrorMessage != string.Empty)
        {
            lblRMMessage.ForeColor = Color.Red;
            lblRMMessage.Text = zErrorMessage;
            return;
        }
        #endregion

        if (SessionManager.GetSession(SessionManager.HIMSRawMaterial_OnEdit_RMID) == null)
        {
            return;
        }

        int intRMID = (int)SessionManager.GetSession(SessionManager.HIMSRawMaterial_OnEdit_RMID);

        bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

        try
        {
            if (!bIsPageRefresh)
            {
                double dblRMWeight = Convert.ToDouble(txtRMWeight.Text);
                double dblRMPurchaseRatePerKg = Convert.ToDouble(txtRMPurchaseRate.Text);
                double dblTotalPurchaseRate = dblRMWeight * dblRMPurchaseRatePerKg;

                BALRawMaterial oBALRawMaterial = new BALRawMaterial();
                oBALRawMaterial.RMID = intRMID;
                oBALRawMaterial.RMWeight = dblRMWeight;
                oBALRawMaterial.RMPurchaseRate = dblTotalPurchaseRate;
                oBALRawMaterial.RMOrigin = txtRMOrigin.Text.Trim();
                oBALRawMaterial.RMReceivedBy = txtRMReceivedBy.Text.Trim();
                oBALRawMaterial.LastModifiedOn = DateTime.Now;
                oBALRawMaterial.LastModifiedBy = Environment.UserName;

                BindRawMaterialGrid(DALRawMaterial.UpdateRawMaterial(oBALRawMaterial));

                ClearFields();

                MultiView1.ActiveViewIndex = 0;
                Session["ACTIVE_VIEW_INDEX"] = MultiView1.ActiveViewIndex;
            }
            else
            {
                BindRawMaterialGrid(DALRawMaterial.GetAllRawMaterial());
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvRawMaterialDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "cnEdit")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                Label lblRMID = (Label)gvRawMaterialDetails.Rows[gvr.RowIndex].FindControl("lblRMID");
                Label lblRMWeight = (Label)gvRawMaterialDetails.Rows[gvr.RowIndex].FindControl("lblRMWeight");
                Label lblRMPurchaseRate = (Label)gvRawMaterialDetails.Rows[gvr.RowIndex].FindControl("lblRMPurchaseRate");
                Label lblRMOrigin = (Label)gvRawMaterialDetails.Rows[gvr.RowIndex].FindControl("lblRMOrigin");
                Label lblRMReceivedBy = (Label)gvRawMaterialDetails.Rows[gvr.RowIndex].FindControl("lblRMReceivedBy");

                SessionManager.SetSession(SessionManager.HIMSRawMaterial_OnEdit_RMID, Convert.ToInt32(lblRMID.Text));

                MultiView1.ActiveViewIndex = 1;

                txtRMWeight.Text = lblRMWeight.Text;
                txtRMPurchaseRate.Text = (Convert.ToDouble(lblRMPurchaseRate.Text) / Convert.ToDouble(lblRMWeight.Text)).ToString("0.00");
                txtRMOrigin.Text = lblRMOrigin.Text;
                txtRMReceivedBy.Text = lblRMReceivedBy.Text;

                btnAdd.Visible = false;
                btnUpdate.Visible = true;

            }

            else if (e.CommandName == "cnDelete")
            {
                bool bIsPageRefresh = (bool)Session["IsPageRefresh"];

                if (!bIsPageRefresh)
                {
                    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    Label lblRMID = (Label)gvRawMaterialDetails.Rows[gvr.RowIndex].FindControl("lblRMID");
                    BindRawMaterialGrid(DALRawMaterial.DeleteRawMaterial(Convert.ToInt32(lblRMID.Text)));
                }
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message); 
        }

    }

    protected void gvRawMaterialDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvRawMaterialDetails.PageIndex = e.NewPageIndex;            
            BindRawMaterialGrid(DALRawMaterial.GetAllRawMaterial());           
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }
    }

    protected void gvRawMaterialDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            List<BALRawMaterial> lstBALRawMaterial = (List<BALRawMaterial>)SessionManager.GetSession(SessionManager.LIST_BALRawMaterial);

            if (lstBALRawMaterial != null)
            {
                DataTable dt = BALRawMaterial.fnConvertListToDataTable(lstBALRawMaterial);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                gvRawMaterialDetails.DataSource = dt;
                gvRawMaterialDetails.DataBind();
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

    protected void gvRawMaterialDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
            ListItem li = ddl.Items.FindByText(gvRawMaterialDetails.PageSize.ToString());
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
        gvRawMaterialDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        //binds data source
        BindRawMaterialGrid(DALRawMaterial.GetAllRawMaterial());
        //gvRawMaterialDetails.DataSource = DALRawMaterial.GetAllRawMaterial();
        //gvRawMaterialDetails.DataBind();
    }

    protected void drpPager_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvRawMaterialDetails.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
        BindRawMaterialGrid(DALRawMaterial.GetAllRawMaterial());
        //gvRawMaterialDetails.DataSource = DALRawMaterial.GetAllRawMaterial();
        //gvRawMaterialDetails.DataBind();
    }
}