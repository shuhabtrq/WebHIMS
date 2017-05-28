using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared; 
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;

public partial class Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnGenerateUIReport_Click(object sender, EventArgs e)
    {
        divReportImage.Visible = false;
        BindReport("Reports/VAUnfinished.rpt");
    }

    protected void btnGenerateFIReport_Click(object sender, EventArgs e)
    {
        divReportImage.Visible = false;
        BindReport("Reports/VAFinished.rpt");
    }

    private void BindReport(string strReportPathPlusFileName)
    {
        string strServerName = ConfigurationManager.AppSettings["ServerName"].ToString();
        string strDatabaseName = ConfigurationManager.AppSettings["DatabaseName"].ToString();
        string strUserID = ConfigurationManager.AppSettings["UserID"].ToString();
        string strPassword = ConfigurationManager.AppSettings["Password"].ToString();

        try
        {
            ReportDocument myReportDocument = new ReportDocument();

            string filepath = Server.MapPath(strReportPathPlusFileName);
            myReportDocument.Load(filepath);
            
            ConnectionInfo ConnInfo = new CrystalDecisions.Shared.ConnectionInfo();
            ConnInfo.ServerName = strServerName;
            ConnInfo.DatabaseName = strDatabaseName;
            ConnInfo.UserID = strUserID;
            ConnInfo.Password = strPassword;

            Tables crTables = myReportDocument.Database.Tables;

            TableLogOnInfo crTableLogOnInfo = new TableLogOnInfo();

            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                crTableLogOnInfo = crTable.LogOnInfo;
                ConnInfo.ServerName = strServerName;
                ConnInfo.DatabaseName = strDatabaseName;
                ConnInfo.UserID = strUserID;
                ConnInfo.Password = strPassword;
                crTableLogOnInfo.ConnectionInfo = ConnInfo;
                crTable.ApplyLogOnInfo(crTableLogOnInfo);
            }

            CrystalReportViewer1.EnableViewState = true;
            CrystalReportViewer1.ReportSource = myReportDocument;
            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.RefreshReport();
            
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

    }


}