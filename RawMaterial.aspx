<%@ Page Title="Raw Material | Configuration" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RawMaterial.aspx.cs" Inherits="RawMaterial" MaintainScrollPositionOnPostback="true" %>

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
                             Configure Raw Material
                         </td>
                     </tr>
                     <tr>
                         <td style="width:70%; padding-left:50px; font-family:Verdana; font-size:16px; 
                            font-family:'Century Gothic'">
                             <asp:Label ID="lblTotalRawMaterial" runat="server" Text="" Font-Bold="true"></asp:Label>
                         </td>
                         <td style="width:30%; text-align:right;">
                             <asp:Button ID="btnAddNew" runat="server" Text="Add Raw Material" OnClick="btnAddNew_Click" />
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <div id="divNoRMItem" runat="server">
                                 No Item Added
                             </div>
                             <div id="divWithRMItem" runat="server">
                                 <asp:GridView ID="gvRawMaterialDetails" runat="server" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                                     GridLines="None" Width="100%" AllowPaging="true" AllowSorting="true" OnRowCommand="gvRawMaterialDetails_RowCommand" OnPageIndexChanging="gvRawMaterialDetails_PageIndexChanging" OnSorting="gvRawMaterialDetails_Sorting" OnRowCreated="gvRawMaterialDetails_RowCreated">
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
                                        <asp:TemplateField HeaderText="ID" SortExpression="RMID" HeaderStyle-Width="5px" 
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                          <ItemTemplate>
                                             <asp:Label ID="lblRMID" runat="server" Text='<%#Eval("RMID") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Weight (kgs)" SortExpression="RMWeight" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblRMWeight" runat="server" Text='<%#Eval("RMWeight") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Purchase Rate (Rs)" SortExpression="RMPurchaseRate" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblRMPurchaseRate" runat="server" Text='<%#Eval("RMPurchaseRate") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Origin" SortExpression="RMOrigin" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblRMOrigin" runat="server" Text='<%#Eval("RMOrigin") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received By" SortExpression="RMReceivedBy" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblRMReceivedBy" runat="server" Text='<%#Eval("RMReceivedBy") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created On (Date/Time)" SortExpression="CreatedOn" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblCreatedOn" runat="server" Text='<%#Eval("CreatedOn") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Modified On (Date/Time)" SortExpression="LastModifiedOn" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblLastModifiedOn" runat="server" Text='<%#Eval("LastModifiedOn") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Updated By" SortExpression="LastModifiedBy" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                             <asp:Label ID="lblLastModifiedBy" runat="server" Text='<%#Eval("LastModifiedBy") %>'></asp:Label>
                                          </ItemTemplate>            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
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
                             Add/Update Raw Material
                         </td>                         
                     </tr>                    
                     <tr>
                         <td class="tblRawMat_td1">
                             <br /><br />
                             Weight:
                         </td>
                         <td class="tblRawMat_td2">
                              <br /><br />
                             <asp:TextBox ID="txtRMWeight" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>&nbsp; kg                            
                         </td>
                         <td class="tblRawMat_td3">
                              <br /><br />
                              <asp:RequiredFieldValidator ID="rfvRMWeight" runat="server" ErrorMessage="*Please enter Weight (in kgs)" Font-Italic="true" 
                                 ControlToValidate="txtRMWeight" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revRMWeight" runat="server" Font-Italic="true"   
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtRMWeight" Display="Dynamic" 
                                 ValidationExpression="^[0-9.]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Purchase Rate (Rs):
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtRMPurchaseRate" runat="server" MaxLength="10" Font-Size="16px" Width="300px"></asp:TextBox>&nbsp; /kg
                         </td>
                         <td class="tblRawMat_td3">
                             <asp:RequiredFieldValidator ID="rfvRMPurchaseRate" runat="server" ErrorMessage="*Please enter Purchase Rate (per kg)" Font-Italic="true"
                                 ControlToValidate="txtRMPurchaseRate" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>                                        
                             <asp:RegularExpressionValidator ID="revRMPurchaseRate" runat="server"    
                                 ErrorMessage="*Please enter a valid input" ControlToValidate="txtRMPurchaseRate" Display="Dynamic" Font-Italic="true"
                                 ValidationExpression="^[0-9.]*$" ForeColor="Red"></asp:RegularExpressionValidator>
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Origin:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtRMOrigin" runat="server" MaxLength="50" Font-Size="16px" Width="300px"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
                         </td>
                     </tr>
                     <tr>
                         <td class="tblRawMat_td1">
                             Received By:
                         </td>
                         <td class="tblRawMat_td2">
                             <asp:TextBox ID="txtRMReceivedBy" runat="server" MaxLength="50" Font-Size="16px" Width="300px"></asp:TextBox>
                         </td>
                         <td class="tblRawMat_td3">
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
                             <asp:Label ID="lblRMMessage" runat="server" Text="" Font-Italic="true"></asp:Label>
                             <br /><br /><br /><br />
                         </td>                         
                     </tr>
                 </table>
             </asp:View>
         </asp:MultiView>
     </div>
</asp:Content>


