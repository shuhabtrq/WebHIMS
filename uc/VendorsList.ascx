<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VendorsList.ascx.cs" Inherits="uc_VendorsList" %>
<asp:DropDownList ID="drpVendors" runat="server" AutoPostBack="false" Width="100%"></asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvDrpVendors" runat="server" ErrorMessage="" Font-Italic="true" 
    ControlToValidate="drpVendors" InitialValue="0" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>