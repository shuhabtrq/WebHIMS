<%@ Page Title="Configure Vendors" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConfigureVendors.aspx.cs" Inherits="ConfigureVendors" MaintainScrollPositionOnPostback="true" %>

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
                             Configure Vendors
                         </td>
                     </tr>
                     <tr>
                         <td style="width:70%; padding-left:50px; text-align:left;">
                             <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="12px" Font-Italic="true"></asp:Label>
                         </td>
                         <td style="width:30%; text-align:right;">
                             <asp:Button ID="btnAddNew" runat="server" Text="Add New Vendor" OnClick="btnAddNew_Click" />
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <div id="divNoVendorItem" runat="server">
                                 No Vendor Added
                             </div>
                             <div id="divWithVendorItem" runat="server">
                                 <asp:GridView ID="gvVendorDetails" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                                     GridLines="None" Width="100%" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gvVendorDetails_PageIndexChanging" OnRowCommand="gvVendorDetails_RowCommand" OnRowCreated="gvVendorDetails_RowCreated" OnSorting="gvVendorDetails_Sorting" style="margin-top: 0px">
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
                                        <asp:TemplateField HeaderText="ID" SortExpression="VendorID" HeaderStyle-Width="5px" 
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorID" runat="server" Text='<%#Eval("VendorID") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" SortExpression="VendorName" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorName" runat="server" Text='<%#Eval("VendorName") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Organisation" SortExpression="VendorOrgName" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorOrgName" runat="server" Text='<%#Eval("VendorOrgName") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email-ID" SortExpression="VendorEmailID" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorEmailID" runat="server" Text='<%#Eval("VendorEmailID") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorPhone" runat="server" Text='<%#Eval("VendorPhone") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorAddress" runat="server" Text='<%#Eval("VendorAddress") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Additional Information" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorAdditionalInfo" runat="server" Text='<%#Eval("VendorAdditionalInfo") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>                                   
                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
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
                             Add/Update Vendor
                         </td>                         
                     </tr>                    
                     <tr>
                         <td class="tblRawMat_td1">
                             <br /><br />
                             Name:
                         </td>
                         <td class="tblRawMat_td2">
                              <br /><br />
                             <asp:TextBox ID="txtVendorName" runat="server" MaxLength="49" Font-Size="16px" Width="300px"></asp:TextBox>                          
                         </td>
                         <td class="tblRawMat_td3">
                              <br /><br />
                              <asp:RequiredFieldValidator ID="rfvVendorName" runat="server" ErrorMessage="*Vendor Name cannot be left blank" Font-Italic="true" 
                                 ControlToValidate="txtVendorName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revVendorName" runat="server" Font-Italic="true"   
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtVendorName" Display="Dynamic" 
                                 ValidationExpression="^[a-zA-Z0-9 _.\-&@]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Organisation Name:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtVendorOrgName" runat="server" MaxLength="99" Font-Size="16px" Width="300px"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RequiredFieldValidator ID="rfvVendorOrgName" runat="server" ErrorMessage="*Vendor Org Name cannot be left blank" Font-Italic="true"
                                 ControlToValidate="txtVendorOrgName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revVendorOrgName" runat="server"    
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtVendorOrgName" Display="Dynamic" Font-Italic="true"
                                 ValidationExpression="^[a-zA-Z0-9 _.\-&@]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Email ID:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtVendorEmailID" runat="server" MaxLength="49" Font-Size="16px" Width="300px"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RegularExpressionValidator ID="revVendorEmailID" runat="server"    
                                 ErrorMessage="*Please enter a valid email" ControlToValidate="txtVendorEmailID" Display="Dynamic" Font-Italic="true"
                                 ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Phone:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtVendorPhone" runat="server" MaxLength="99" Font-Size="16px" Width="300px"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RegularExpressionValidator ID="revVendorPhone" runat="server"    
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtVendorPhone" Display="Dynamic" Font-Italic="true"
                                 ValidationExpression="^[0-9 +,()\-]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                         <tr>
                         <td class="tblRawMat_td1">
                             Address:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtVendorAddress" runat="server" MaxLength="499" Font-Size="16px" TextMode="MultiLine" Width="300px" Font-Names="Verdana"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RegularExpressionValidator ID="revVendorAddress" runat="server"    
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtVendorAddress" Display="Dynamic" Font-Italic="true"
                                 ValidationExpression="^[a-zA-Z0-9 _.\-&£$/(){}?,'@;:#]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Addtional Info:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtVendorAdditionalInfo" runat="server" MaxLength="499" Font-Size="16px" TextMode="MultiLine" Width="300px" Font-Names="Verdana"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RegularExpressionValidator ID="revVendorAdditionalInfo" runat="server"    
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtVendorAdditionalInfo" Display="Dynamic" Font-Italic="true"
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
                             <asp:Label ID="lblVendorMessage" runat="server" Text="" Font-Italic="true"></asp:Label>
                             <br /><br /><br /><br />
                         </td>                         
                     </tr>
                 </table>
             </asp:View>
         </asp:MultiView>
     </div>
</asp:Content>

