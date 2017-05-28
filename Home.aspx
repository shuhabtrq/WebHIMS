<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Handicrafts Inventory Management System</title>
    <link href="css/HIMS-Home.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
      <div class="def-wrapper">
        <div class="heading">
          Handicrafts Inventory Management System [HIMS]
        </div>
        <div id="table-div-wrapper">
          <table id="tblNav">
              <tr>
                  <td class="td-internal" style="background:url('../images/black.png') repeat-x; color:white">                      
                      <asp:LinkButton ID="lbConfigureVendors" runat="server" ForeColor="White" ToolTip="Configure Vendors" OnClick="lbConfigureVendors_Click">
                      Configure Vendors</asp:LinkButton>                      
                  </td>
                  <td class="td-internal" style="background:url('../images/black.png') repeat-x; color:white">
                      <asp:LinkButton ID="lbConfigureItems" runat="server" ForeColor="White" ToolTip="Configure Items" OnClick="lbConfigureItems_Click">
                      Configure Items</asp:LinkButton>                       
                  </td>
              </tr>
              <tr>
                  <td colspan="2" class="td-internal" style="background:url('../images/maroon.png') repeat-x; color:#333333">
                      1.&nbsp;<asp:LinkButton ID="lbConfigureRawMaterial" runat="server" ForeColor="#333333" ToolTip="Configure Raw Material"
                          OnClick="lbConfigureRawMaterial_Click">Add Raw Material</asp:LinkButton>
                  </td>
              </tr>
              <tr>
                  <td class="td-internal" style="background:url('../images/red.png') repeat-x; color:#333333;">
                      2.&nbsp;<asp:LinkButton ID="lbVendorAllocation" runat="server" ForeColor="#333333" ToolTip="Click here for Vendor Allocation of Raw Material" 
                          OnClick="lbVendorAllocation_Click">Vendor Allocation (Send)</asp:LinkButton>
                  </td>
                  <td class="td-internal" style="background:url('../images/green.png') repeat-x; color:#333333;">                     
                      3.&nbsp;<asp:LinkButton ID="lbUnfinishedInventory" runat="server" ForeColor="#333333" ToolTip="Click here to receive Unfinished Inventory" 
                          OnClick="lbUnfinishedInventory_Click">Unfinished Inventory (Receive)</asp:LinkButton>
                  </td>
              </tr>
              <tr>
                  <td class="td-internal" style="background:url('../images/blue.png') repeat-x; color:#333333;">                      
                      4.&nbsp;<asp:LinkButton ID="lbFinishingProcess" runat="server" ForeColor="#333333" ToolTip="Click here to send inventory for Finishing Process" 
                          OnClick="lbFinishingProcess_Click">Finishing Process (Send)</asp:LinkButton>
                  </td>
                  <td class="td-internal" style="background:url('../images/yellow.png') repeat-x; color:#333333;">                     
                      5.&nbsp;<asp:LinkButton ID="lbFinishedInventory" runat="server" ForeColor="#333333" ToolTip="Click here to receive Finished Inventory" 
                          OnClick="lbFinishedInventory_Click">Finished Inventory (Receive)</asp:LinkButton>
                  </td>
              </tr>
              <tr>
                  <td class="td-internal" style="background:url('../images/black.png') repeat-x; color:white">
                      <asp:LinkButton ID="lbVendorBills" runat="server" ForeColor="White" ToolTip="Click here for Vendor Billing" 
                          OnClick="lbVendorBills_Click">Vendor Billing</asp:LinkButton>
                  </td>              
                  <td class="td-internal" style="background:url('../images/black.png') repeat-x; color:white">
                      <asp:LinkButton ID="lbReports" runat="server" ForeColor="White" ToolTip="Click here to view Reports" 
                          OnClick="lbReports_Click">Reports</asp:LinkButton>
                  </td>
              </tr>
          </table>
        </div>
       </div>
       <div id="def-footer">
                HIMS - Powered by Echelon Technology Services Pvt Ltd&nbsp;[<a href="http://www.ets-it.com" target="_blank" style="color:#ffffff;">ets-it.com</a>]&nbsp;|&nbsp;<a href="mailto:support@ets-it.com" target="_blank" style="color:#ffffff;">Support</a>
       </div>
    </form>
</body>
</html>

