<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/HIMS-Content-Styles.css" rel="stylesheet" />
    <style type="text/css">
        #cntBody_txtAdminKey { width:500px; height:30px; font-family:'Century Gothic'; font-size:16px; text-align:center; }
        #cntBody_btnAdminLogin { height:36px; cursor:pointer;}       
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntBody" Runat="Server">
    <table style="border: 1px solid #C0C0C0; width: 100%;">
        <tr>
            <td class="page-header" colspan="2">Login
            </td>
        </tr>
        <tr>
            <td style="width: 100%; text-align:center;">               
                <table style="margin:auto; left:0px; right:0px; margin-top:50px; margin-bottom:50px; border:solid 1px #333333;">
                    <tr>
                        <td colspan="2" style="text-align:left; padding:10px; padding-left:20px; font-family:'Century Gothic'; font-size:16px; background-color:#333333; color:#ffffff;">
                            Login as Administrator
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">                            
                            <input id="txtAdminKey" runat="server" type="password" placeholder="Enter Admin Key" maxlength="12" title="Login as Admin | Enter Admin Key" />
                        </td>
                        <td style="text-align: left">                           
                            <asp:Button ID="btnAdminLogin" runat="server" Text="Login" ToolTip="Click here to login as Admin" OnClick="btnAdminLogin_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br /><br />
                            <table style="width:100%;">
                                <tr>
                                    <td style="text-align:center; padding:10px; padding-left:20px; 
                                        font-family:'Century Gothic'; font-size:16px; background-color:#8d0100; color:#ffffff; letter-spacing:2px;">
                                       <asp:LinkButton ID="lbUserLogin" runat="server" ForeColor="White" Font-Bold="true"
                                           ToolTip="Click here to login as User" OnClick="lbUserLogin_Click">GUEST USER LOGIN</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>            
            </td>          
        </tr>
   </table>  
</asp:Content>


