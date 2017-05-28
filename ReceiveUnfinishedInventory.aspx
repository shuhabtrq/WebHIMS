<%@ Page Title="Unfinished Inventory [Receive]" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReceiveUnfinishedInventory.aspx.cs" Inherits="ReceiveUnfinishedInventory" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/uc/VendorsList.ascx" TagPrefix="uc1" TagName="VendorsList" %>
<%@ Register Src="~/uc/ItemsList.ascx" TagPrefix="uc1" TagName="ItemsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/HIMS-Content-Styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntBody" Runat="Server">
     <div class="outer-container">
         <asp:MultiView ID="MultiView1" runat="server">
             <asp:View ID="View1" runat="server">
                 <table style="border:1px solid #C0C0C0; width:100%;">
                     <tr>
                         <td class="page-header" colspan="2">
                             Unfinished Inventory <i>[Receive]</i>
                         </td>
                     </tr>
                     <tr>
                         <td style="width:70%;">
                             
                         </td>
                         <td style="width:30%; text-align:right;">
                             <asp:Button ID="btnAddNew" runat="server" Text="Receive Unfinished Inventory" OnClick="btnAddNew_Click" />
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <div id="divNoUIItem" runat="server">
                                 No Item Received
                             </div>
                             <div id="divWithUIItem" runat="server">
                                 <asp:GridView ID="gvUnfinishedInventoryDetails" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                                     GridLines="None" Width="100%" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gvUnfinishedInventoryDetails_PageIndexChanging" OnRowCommand="gvUnfinishedInventoryDetails_RowCommand" OnRowCreated="gvUnfinishedInventoryDetails_RowCreated" OnSorting="gvUnfinishedInventoryDetails_Sorting" style="margin-top: 0px">
                                     <AlternatingRowStyle BackColor="White" />
                                     <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                     <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                     <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" Font-Names="Verdana" Font-Size="11px" />
                                     <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                     <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                     <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                     <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                     <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                     <SortedDescendingHeaderStyle BackColor="#820000" />
                                     <HeaderStyle Font-Names="Verdana" Font-Size="12px" />
                                     <RowStyle Font-Names="Verdana" Font-Size="12px" />
                                     <Columns>
                                        <asp:TemplateField HeaderText="ID" ItemStyle-HorizontalAlign="Center" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblUIID" runat="server" Text='<%#Eval("UIID") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vendor" SortExpression="VendorDisplayName" HeaderStyle-Width="23%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorDisplayName" runat="server" Text='<%#Eval("VendorDisplayName") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Allocated RM" SortExpression="TotalRMWeightAllocatedToAVendor" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblTotalAllocationRM" runat="server" Text='<%#Eval("TotalRMWeightAllocatedToAVendor") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Received Item" SortExpression="ItemDisplayName" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblItemDisplayName" runat="server" Text='<%#Eval("ItemDisplayName") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pieces" SortExpression="UINoOfPieces" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblUINoOfPieces" runat="server" Text='<%#Eval("UINoOfPieces") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Wt(kgs)" SortExpression="UIWeightReceived" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblUIWeightReceived" runat="server" Text='<%#Eval("UIWeightReceived") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received By" SortExpression="UIReceivedBy" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblReceivedBy" runat="server" Text='<%#Eval("UIReceivedBy") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Created On" SortExpression="CreatedOn" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblCreatedOn" runat="server" Text='<%#Eval("CreatedOn") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Modified On" SortExpression="LastModifiedOn" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblLastModifiedOn" runat="server" Text='<%#Eval("LastModifiedOn") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Updated By" SortExpression="LastModifiedBy" HeaderStyle-Width="9%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblLastModifiedBy" runat="server" Text='<%#Eval("LastModifiedBy") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>            
                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="7%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="lbEdit" runat="server" CommandName="cnEdit">Edit</asp:LinkButton>
                                              <asp:LinkButton ID="lbDelete" runat="server" CommandName="cnDelete" OnClientClick="javascript:return fnDeleteConfirm();">Delete</asp:LinkButton>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                     </Columns>
                                 </asp:GridView>
                                 <div id="divPager" runat="server" style="width:100%; margin:auto; left:0px; right:0px; padding-top:6px; padding-bottom:6px; 
                                      background-color:#FFCC66; text-align:center; font-family:Verdana; font-size:11px;">
                                     Page Size:
                                     <asp:DropDownList ID="drpPager" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpPager_SelectedIndexChanged">
                                         <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                                         <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                         <asp:ListItem Text="50" Value="50"></asp:ListItem>
                                         <asp:ListItem Text="100" Value="100"></asp:ListItem>
                                         <asp:ListItem Text="500" Value="500"></asp:ListItem>
                                         <asp:ListItem Text="1000" Value="1000"></asp:ListItem>                                       
                                     </asp:DropDownList>
                                 </div>
                             </div>
                         </td>
                     </tr>
                 </table>
             </asp:View>
             <asp:View ID="View2" runat="server">
                 <table id="tblRawMat" border="0">
                     <tr>
                         <td class="page-header" colspan="3">
                             Add/Update <i>[Received]</i> Unfinished Inventory
                         </td>                         
                     </tr>
                     <tr>                        
                        <td class="tblRawMat_td2" colspan="3" style="font-style:italic; color:maroon; padding-left:8%;">
                             <br /><br />
                            Total RM Allocated to this vendor (kgs): <asp:Label ID="lblTotalRMAllocatedToVendor" runat="server" Text="" Font-Bold="true"></asp:Label> 
                            &nbsp;&nbsp;|&nbsp;&nbsp;
                            Total UI Received from this vendor till date (kgs): <asp:Label ID="lblTotalWeightReceivedTillNow" runat="server" Text="" Font-Bold="true"></asp:Label>
                            &nbsp;&nbsp;|&nbsp;&nbsp;
                            Balance Weight (kgs): <asp:Label ID="lblBalanceWeight" runat="server" Text="" Font-Bold="true"></asp:Label> 
                        </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">                            
                              <br />
                             Vendor:
                         </td>
                         <td class="tblRawMat_td2" colspan="2">                             
                             <br />
                             <div style="width:306px;">
                                <uc1:VendorsList runat="server" ID="VendorsList" />                                                                 
                             </div>
                         </td>                         
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">                             
                             Item:
                         </td>
                         <td class="tblRawMat_td2" colspan="2">                            
                             <div style="width:306px;">
                                 <uc1:ItemsList runat="server" ID="ItemsList" />
                             </div>
                         </td>                         
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             No.Of Pieces:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtPieceCount" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RegularExpressionValidator ID="revPieceCount" runat="server"    
                                 ErrorMessage="*Please enter a valid number" ControlToValidate="txtPieceCount" Display="Dynamic" Font-Italic="true"
                                 ValidationExpression="^[0-9]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>                                    
                     <tr>
                         <td class="tblRawMat_td1">                             
                             Received UI (Weight):
                         </td>
                         <td class="tblRawMat_td2">                             
                             <asp:TextBox ID="txtUIReceivedWeight" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>&nbsp; kg                            
                         </td>
                         <td class="tblRawMat_td3">                           
                             <asp:RequiredFieldValidator ID="rfvUIReceivedWeight" runat="server" ErrorMessage="*Please enter Weight (in kgs)" Font-Italic="true" 
                                 ControlToValidate="txtUIReceivedWeight" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revUIReceivedWeight" runat="server" Font-Italic="true"   
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtUIReceivedWeight" Display="Dynamic" 
                                 ValidationExpression="^[0-9.]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Received By:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtUIReceivedBy" runat="server" MaxLength="50" Font-Size="16px" Width="300px"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                              <br />
                             <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="true" OnClientClick="javascript:return fnReceivedUIStockExceeded();" OnClick="btnAdd_Click" />
                             <asp:Button ID="btnUpdate" runat="server" Text="Update" CausesValidation="true" OnClientClick="javascript:return fnReceivedUIStockExceeded();" OnClick="btnUpdate_Click" />
                             <br /><br /><br /><br /> 
                         </td>
                         <td class="tblRawMat_td2" colspan="2">
                              <br />
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
                             &nbsp;&nbsp;&nbsp;
                             <asp:Label ID="lblUnfinishedInventoryMessage" runat="server" Text="" Font-Italic="true"></asp:Label>
                             <br /><br /><br /><br />
                         </td>                         
                     </tr>
                 </table>
             </asp:View>
         </asp:MultiView>
     </div>
</asp:Content>



