<%@ Page Title="HIMS Reports" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/HIMS-Content-Styles.css" rel="stylesheet" />
    <script src="crystalreportviewers13/js/crviewer/crv.js" type="text/javascript"></script>
    <style type="text/css">
        #cntBody_divReportImage {
            padding-top:50px; padding-bottom:50px; font-family: 'Century Gothic'; font-size: 36px; text-align:center; 
            border: 1px solid #C0C0C0; color:rgba(0,0,0,0.3); cursor:default;
        }     
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cntBody" Runat="Server">
    <div class="outer-container">
        <table style="width: 100%; border: 1px solid #C0C0C0;">
            <tr>
                <td class="page-header" colspan="2">HIMS Reports
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">
                    <asp:Button ID="btnGenerateUIReport" runat="server" Text="Generate Unfinished Inventory Report" OnClick="btnGenerateUIReport_Click" Height="30px" />
                    &nbsp;
                    <asp:Button ID="btnGenerateFIReport" runat="server" Text="Generate Finished Inventory Report" OnClick="btnGenerateFIReport_Click" Height="30px" />
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <div id="divReportImage" runat="server">                        
                        <span class="latest_img">View Reports</span>
                        <br />
                        <img src="images/report.png" class="latest_img" />
                    </div>
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" Width="100%" Height="1202px"
                        GroupTreeImagesFolderUrl="" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" 
                        DisplayToolbar="True" DisplayStatusbar="true" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cntFooter" Runat="Server">
</asp:Content>   


