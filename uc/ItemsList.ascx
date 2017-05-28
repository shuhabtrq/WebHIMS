<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItemsList.ascx.cs" Inherits="uc_ItemsList" %>
<asp:DropDownList ID="drpItems" runat="server" AutoPostBack="false" Width="100%"></asp:DropDownList>
<asp:RequiredFieldValidator ID="rfvDrpItems" runat="server" ErrorMessage="" Font-Italic="true" 
    ControlToValidate="drpItems" InitialValue="0" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>