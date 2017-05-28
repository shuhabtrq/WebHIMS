<%@ Page Title="Vendor Billing" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VendorBilling.aspx.cs" Inherits="VendorBilling" MaintainScrollPositionOnPostback="true" %>

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
                             Vendor Billing
                         </td>
                     </tr>
                     <tr>
                         <td style="width:70%; padding-left:50px; text-align:left;">
                             <asp:Label ID="lblMessage" runat="server" Text="" Font-Names="Verdana" Font-Size="12px" Font-Italic="true"></asp:Label>
                         </td>
                         <td style="width:30%; text-align:right;">
                             <!-- ADD NEW Button -->
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <div id="divNoVendorItem" runat="server">
                                 No Vendor Added
                             </div>
                             <div id="divWithVendorItem" runat="server">
                                 <asp:GridView ID="gvVendorBillingDetails" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                                     GridLines="None" Width="100%" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gvVendorBillingDetails_PageIndexChanging" OnRowCommand="gvVendorBillingDetails_RowCommand" OnRowCreated="gvVendorBillingDetails_RowCreated" OnSorting="gvVendorBillingDetails_Sorting" style="margin-top: 0px">
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
                                        <asp:TemplateField HeaderText="VendorID" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorID" runat="server" Text='<%#Eval("VendorID") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="VBAmountPaid" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVBAmountPaid" runat="server" Text='<%#Eval("VBAmountPaid") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VBDiscount" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVBDiscount" runat="server" Text='<%#Eval("VBDiscount") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VBWriteOff" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVBWriteOff" runat="server" Text='<%#Eval("VBWriteOff") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vendor" SortExpression="VendorDisplayName" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorDisplayName" runat="server" Text='<%#Eval("VendorDisplayName") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount to be paid (Rs)" SortExpression="TotalSUMToBePaid" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblTotalSUMToBePaid" runat="server" Text='<%#Eval("TotalSUMToBePaid") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Amount Paid (Rs)" SortExpression="VBTotalAmountPaid" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVBTotalAmountPaid" runat="server" Text='<%#Eval("VBTotalAmountPaid") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balance (Rs)" SortExpression="BalanceAmount" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#Eval("BalanceAmount") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="Created On" SortExpression="CreatedOn" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblCreatedOn" runat="server" Text='<%#Eval("CreatedOn") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Modified On" SortExpression="LastModifiedOn" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblLastModifiedOn" runat="server" Text='<%#Eval("LastModifiedOn") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Updated By" SortExpression="LastModifiedBy" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblLastModifiedBy" runat="server" Text='<%#Eval("LastModifiedBy") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>                                                                           
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="lbEdit" runat="server" CommandName="cnEdit">Pay Now</asp:LinkButton>                                              
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
                             Add/Update Vendor Bill
                         </td>                         
                     </tr>                    
                     <tr>
                         <td class="tblRawMat_td1">
                             <br /><br />
                             <b>Vendor:</b>
                         </td>
                         <td colspan="2" class="tblRawMat_td2"> 
                             <br /><br />                             
                             <asp:Label ID="lblFullVendorDisplayName" runat="server" Text="" Font-Bold="true"></asp:Label>                         
                         </td>                         
                     </tr>
                     <tr>                        
                        <td class="tblRawMat_td2" colspan="3" style="font-style:italic; color:maroon; padding-left:5%;">                            
                            Total Amount to be paid (Rs): <asp:Label ID="lblTotalAmountToBePaidToVendor" runat="server" Text="" Font-Bold="true"></asp:Label> 
                            &nbsp;&nbsp;|&nbsp;&nbsp;
                            Total Amount paid till now (Rs): <asp:Label ID="lblTotalAmountPaidTillNowToVendor" runat="server" Text="" Font-Bold="true"></asp:Label>
                            &nbsp;&nbsp;|&nbsp;&nbsp;
                            Balance Amount (Rs): <asp:Label ID="lblTotalBalanceAmount" runat="server" Text="" Font-Bold="true"></asp:Label>
                        </td>
                     </tr>
                     <tr>                        
                        <td class="tblRawMat_td2" colspan="3" style="font-style:italic; color:maroon; padding-left:10%;">                            
                            Total Amount paid till now (Rs)&nbsp;&nbsp;<b>=</b>&nbsp;&nbsp;Amount Paid (Cummulative) + Discount (Cummulative) - Write-Off (Cummulative) 
                        </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td2" colspan="3" style="font-style:italic; color:maroon; padding-left:10%;"> 
                            <br />
                            Amount Paid (Rs): <asp:Label ID="lblAmountPaidTillNow" runat="server" Text="" Font-Bold="true"></asp:Label>
                            &nbsp;&nbsp;|&nbsp;&nbsp;
                            Total Discount (Rs): <asp:Label ID="lblTotalDiscountTillNow" runat="server" Text="" Font-Bold="true"></asp:Label>
                            &nbsp;&nbsp;|&nbsp;&nbsp;
                            Total Write-Off (Rs): <asp:Label ID="lblTotalWriteOffTillNow" runat="server" Text="" Font-Bold="true"></asp:Label>    
                         </td> 
                     </tr>                     
                     <tr>
                         <td class="tblRawMat_td1">                             
                             Amount Paid (Rs):
                         </td>
                         <td class="tblRawMat_td2">                             
                             <asp:TextBox ID="txtVBAmountPaid" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>                            
                         </td>
                         <td class="tblRawMat_td3">                           
                             <asp:RequiredFieldValidator ID="rfvVBAmountPaid" runat="server" ErrorMessage="*Please enter Amount (Rs)" Font-Italic="true" 
                                 ControlToValidate="txtVBAmountPaid" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revVBAmountPaid" runat="server" Font-Italic="true"   
                                 ErrorMessage="*Please enter a valid amount (Rs)" ControlToValidate="txtVBAmountPaid" Display="Dynamic" 
                                 ValidationExpression="^[0-9.]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">                             
                             Discount (Rs):
                         </td>
                         <td class="tblRawMat_td2">                             
                             <asp:TextBox ID="txtVBDiscount" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>                            
                         </td>
                         <td class="tblRawMat_td3">                  
                             <asp:RegularExpressionValidator ID="revVBDiscount" runat="server" Font-Italic="true"   
                                 ErrorMessage="*Please enter a valid discount amount (Rs)" ControlToValidate="txtVBDiscount" Display="Dynamic" 
                                 ValidationExpression="^[0-9.]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">                             
                             Write Off (Rs):
                         </td>
                         <td class="tblRawMat_td2">                             
                             <asp:TextBox ID="txtVBWriteOff" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>                            
                         </td>
                         <td class="tblRawMat_td3">                  
                             <asp:RegularExpressionValidator ID="revVBWriteOff" runat="server" Font-Italic="true"   
                                 ErrorMessage="*Please enter a valid write-off amount (Rs)" ControlToValidate="txtVBWriteOff" Display="Dynamic" 
                                 ValidationExpression="^[0-9.]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>                     
                     <tr>
                         <td class="tblRawMat_td1">
                             <br />                             
                             <asp:Button ID="btnUpdate" runat="server" Text="Update" CausesValidation="true" OnClick="btnUpdate_Click" />
                             <br /><br /><br /><br /> 
                         </td>
                         <td class="tblRawMat_td2" colspan="2">
                              <br />
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
                             &nbsp;&nbsp;&nbsp;
                             <asp:Label ID="lblVendorBillingMessage" runat="server" Text="" Font-Italic="true"></asp:Label>
                             <br /><br /><br /><br />
                         </td>                         
                     </tr>
                 </table>
             </asp:View>
         </asp:MultiView>
     </div>
</asp:Content>


