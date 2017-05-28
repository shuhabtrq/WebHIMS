<%@ Page Title="Configure Items" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConfigureItems.aspx.cs" Inherits="ConfigureItems" MaintainScrollPositionOnPostback="true" %>

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
                             Configure Items
                         </td>
                     </tr>
                     <tr>
                         <td style="width:70%; padding-left:50px; text-align:left;">
                             <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="12px" Font-Italic="true"></asp:Label>
                         </td>
                         <td style="width:30%; text-align:right;">
                             <asp:Button ID="btnAddNew" runat="server" Text="Add New Item" OnClick="btnAddNew_Click" />
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <div id="divNoItem" runat="server">
                                 No Item Added
                             </div>
                             <div id="divWithItem" runat="server">
                                 <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                                     GridLines="None" Width="100%" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gvItemDetails_PageIndexChanging" OnRowCommand="gvItemDetails_RowCommand" OnRowCreated="gvItemDetails_RowCreated" OnSorting="gvItemDetails_Sorting" style="margin-top: 0px">
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
                                        <asp:TemplateField HeaderText="ID" SortExpression="ItemID" HeaderStyle-Width="5px" 
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblItemID" runat="server" Text='<%#Eval("ItemID") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name" SortExpression="ItemName" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Description" HeaderStyle-Width="60%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("ItemDescription") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>                                                                        
                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
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
                             Add/Update Item
                         </td>                         
                     </tr>                    
                     <tr>
                         <td class="tblRawMat_td1">
                             <br /><br />
                             Item Name:
                         </td>
                         <td class="tblRawMat_td2">
                              <br /><br />
                             <asp:TextBox ID="txtItemName" runat="server" MaxLength="99" Font-Size="16px" Width="300px"></asp:TextBox>                          
                         </td>
                         <td class="tblRawMat_td3">
                              <br /><br />
                              <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ErrorMessage="*Item Name cannot be left blank" Font-Italic="true" 
                                 ControlToValidate="txtItemName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revItemName" runat="server" Font-Italic="true"   
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtItemName" Display="Dynamic" 
                                 ValidationExpression="^[a-zA-Z0-9 _.\-&@]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>             
                     <tr>
                         <td class="tblRawMat_td1">
                             Item Description:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtItemDescription" runat="server" MaxLength="499" Font-Size="16px" TextMode="MultiLine" Width="300px" Font-Names="Verdana"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RegularExpressionValidator ID="revItemDescription" runat="server"    
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtItemDescription" Display="Dynamic" Font-Italic="true"
                                 ValidationExpression="^[a-zA-Z0-9 _.\-&£$/(){}?,'@;:#]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                      <tr>
                         <td class="tblRawMat_td1">
                              <br />
                             <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="true" OnClick="btnAdd_Click" />
                             <asp:Button ID="btnUpdate" runat="server" Text="Update" CausesValidation="true" OnClick="btnUpdate_Click" />
                             <br /><br /><br /><br /> 
                         </td>
                         <td class="tblRawMat_td2" colspan="2">
                              <br />
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
                             &nbsp;&nbsp;&nbsp;
                             <asp:Label ID="lblItemMessage" runat="server" Text="" Font-Italic="true"></asp:Label>
                             <br /><br /><br /><br />
                         </td>                         
                     </tr>
                 </table>
             </asp:View>
         </asp:MultiView>
     </div>
</asp:Content>

