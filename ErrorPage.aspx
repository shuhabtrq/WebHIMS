<%@ Page Title="HIMS - Error Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/HIMS-Content-Styles.css" rel="stylesheet" />
    
    <style type="text/css">       
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntBody" Runat="Server">
    <div class="outer-container">
        <table style="width: 100%; border: 1px solid #C0C0C0; font-family:Verdana; font-size:12px;">
            <tr>
                <td class="page-header" colspan="2">HIMS - Error Page</td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left:20px;">
                    <br />
                    Sorry for inconvenience. In case of any issues or to report a problem, <a target="_blank"
                    href="http://www.ets-it.com">click here</a> to contact support.&nbsp;
                    <a href="Home.aspx"><b>Click Here To Refresh</b></a>
                    <br /><br />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left:20px;">
                    <br />
                    <asp:Label ID="lblErrDescValue" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left:20px;">
                    <br />
                    <asp:Label ID="lblErrReasonValue" runat="server" ForeColor="#000066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: left; padding-left:20px;">
                    <br />
                    <asp:Label ID="lblErrSolValue" runat="server" ForeColor="#003300"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
  

