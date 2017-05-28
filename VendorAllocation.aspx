<%@ Page Title="Vendor Allocation [Send]" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VendorAllocation.aspx.cs" Inherits="VendorAllocation" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/uc/VendorsList.ascx" TagPrefix="uc1" TagName="VendorsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/HIMS-Content-Styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntBody" Runat="Server">
     <div class="outer-container">
         <asp:MultiView ID="MultiView1" runat="server">
             <asp:View ID="View1" runat="server">
                 <table style="border:1px solid #C0C0C0; width:100%">
                     <tr>
                         <td class="page-header" colspan="2">
                             Vendor Allocation <i>[Send]</i>
                         </td>
                     </tr>
                     <tr>
                         <td style="width:70%; padding-left:50px; font-size:16px; font-family:'Century Gothic'">
                             <asp:Label ID="lblTotalRawMaterialProcured" runat="server" Text="" Font-Bold="true"></asp:Label>
                             &nbsp;&nbsp;|&nbsp;&nbsp;
                             <asp:Label ID="lblTotalRawMaterialAllocated" runat="server" Text="" Font-Bold="true"></asp:Label>
                             &nbsp;&nbsp;|&nbsp;&nbsp;<b>Current Stock:</b>&nbsp;
                             <asp:Label ID="lblRawMaterialLeftAfterAllocation" runat="server" Text="" Font-Bold="true"></asp:Label>&nbsp;<b>kgs</b>
                         </td>
                         <td style="width:30%; text-align:right;">
                             <asp:Button ID="btnAddNew" runat="server" Text="Allocate Raw Material to Vendor"  OnClientClick="javascript:return fnRMOutOfStockFirst();" OnClick="btnAddNew_Click" />
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <div id="divNoVAItem" runat="server">
                                 No Vendor Allocations of Raw Material
                             </div>
                             <div id="divWithVAItem" runat="server">
                                 <asp:GridView ID="gvVendorAllocationDetails" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                                     GridLines="None" Width="100%" AllowPaging="true" AllowSorting="true" OnRowCommand="gvVendorAllocationDetails_RowCommand" OnPageIndexChanging="gvVendorAllocationDetails_PageIndexChanging" OnSorting="gvVendorAllocationDetails_Sorting" OnRowCreated="gvVendorAllocationDetails_RowCreated">
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
                                        <asp:TemplateField HeaderText="ID" SortExpression="VAID" 
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVAID" runat="server" Text='<%#Eval("VAID") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vendor Name" SortExpression="VendorDisplayName" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Left">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVendorDisplayName" runat="server" Text='<%#Eval("VendorDisplayName") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Allocated Weight (kgs)" SortExpression="VAWeight" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVAWeight" runat="server" Text='<%#Eval("VAWeight") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate (Rs)" SortExpression="VARate" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVARate" runat="server" Text='<%#Eval("VARate") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Given By" SortExpression="VAGivenByName" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblVAGivenByName" runat="server" Text='<%#Eval("VAGivenByName") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created On" SortExpression="CreatedOn" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblCreatedOn" runat="server" Text='<%#Eval("CreatedOn") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Modified On" SortExpression="LastModifiedOn" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblLastModifiedOn" runat="server" Text='<%#Eval("LastModifiedOn") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Updated By" SortExpression="LastModifiedBy" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblLastModifiedBy" runat="server" Text='<%#Eval("LastModifiedBy") %>'></asp:Label>
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
                             Add/Update <i>[Sent]</i> Vendor Allocation
                         </td>                         
                     </tr>    
                     <tr>
                         <td class="tblRawMat_td1">
                             <br /><br />
                             Vendor:
                         </td>
                         <td class="tblRawMat_td2" colspan="2">
                             <br /> <br />
                             <div style="width:306px;">
                             <uc1:VendorsList runat="server" id="VendorsList" />
                             </div>
                         </td>                         
                     </tr>                
                     <tr>
                         <td class="tblRawMat_td1">                             
                             Weight:
                         </td>
                         <td class="tblRawMat_td2">                             
                             <asp:TextBox ID="txtVAWeight" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>&nbsp; kg                            
                         </td>
                         <td class="tblRawMat_td3">                           
                             <asp:RequiredFieldValidator ID="rfvVAWeight" runat="server" ErrorMessage="*Please enter Weight (in kgs)" Font-Italic="true" 
                                 ControlToValidate="txtVAWeight" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revVAWeight" runat="server" Font-Italic="true"   
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtVAWeight" Display="Dynamic" 
                                 ValidationExpression="^[0-9.]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Rate (Rs):
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtVARate" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>&nbsp; /kg
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RequiredFieldValidator ID="rfvVARate" runat="server" ErrorMessage="*Please enter Rate (per kg)" Font-Italic="true"
                                 ControlToValidate="txtVARate" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revVARate" runat="server"    
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtVARate" Display="Dynamic" Font-Italic="true"
                                 ValidationExpression="^[0-9.]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>                   
                     <tr>
                         <td class="tblRawMat_td1">
                             Given By:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtVAGivenBy" runat="server" MaxLength="49" Font-Size="16px" Width="300px"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                         </td>
                     </tr>
                      <tr>
                         <td class="tblRawMat_td1">
                              <br />
                             <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="true" OnClientClick="javascript:return fnRMOutOfStockSecond();" OnClick="btnAdd_Click" />
                             <asp:Button ID="btnUpdate" runat="server" Text="Update" CausesValidation="true" OnClick="btnUpdate_Click" />
                             <br /><br /><br /><br /> 
                         </td>
                         <td class="tblRawMat_td2" colspan="2">
                              <br />
                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />
                             &nbsp;&nbsp;&nbsp;
                             <asp:Label ID="lblVendorAllocationMessage" runat="server" Text="" Font-Italic="true"></asp:Label>
                             <asp:Label ID="lblCurrentStockHidden" runat="server" Text="" Font-Size="XX-Small" ForeColor="#FFFBD6"></asp:Label>
                             <br /><br /><br /><br />
                         </td>                         
                     </tr>
                 </table>
             </asp:View>
         </asp:MultiView>
     </div>
</asp:Content>


