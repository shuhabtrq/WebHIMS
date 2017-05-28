using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for HIMSBusinessEntities
/// </summary>
public class BALRawMaterial
{
    public int RMID { get; set; }
    public double RMWeight { get; set; }
    public double RMPurchaseRate { get; set; }
    public string RMOrigin { get; set; }
    public string RMReceivedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; }
        
    /* List To DataTable */
    public static DataTable fnConvertListToDataTable(List<BALRawMaterial> lstBALRawMaterial)
    {
        DataTable dtable = new DataTable();

        dtable.Columns.Add("RMID", typeof(int));
        dtable.Columns.Add("RMWeight", typeof(double));
        dtable.Columns.Add("RMPurchaseRate", typeof(double));
        dtable.Columns.Add("RMOrigin", typeof(string));
        dtable.Columns.Add("RMReceivedBy", typeof(string));
        dtable.Columns.Add("CreatedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedBy", typeof(string));

        DataRow dr;

        foreach (var listItem in lstBALRawMaterial)
        {
            dr = dtable.NewRow();
            dr["RMID"] = listItem.RMID;
            dr["RMWeight"] = listItem.RMWeight;
            dr["RMPurchaseRate"] = listItem.RMPurchaseRate;
            dr["RMOrigin"] = listItem.RMOrigin;
            dr["RMReceivedBy"] = listItem.RMReceivedBy;
            dr["CreatedOn"] = listItem.CreatedOn;
            dr["LastModifiedOn"] = listItem.LastModifiedOn;
            dr["LastModifiedBy"] = listItem.LastModifiedBy;
            dtable.Rows.Add(dr);
        }

        return dtable;
    }

    public static string ValidateRawMaterial(string strInput_RMWeight, string strInput_RMPurchaseRate,
                                             string strInput_RMOrigin, string strInput_RMReceivedBy)
    {
        string zReturnErrorMessage = string.Empty;
 
        int iCountOfDots = 0;
        
        try
        {
            iCountOfDots = strInput_RMWeight.Count(f => f == '.');
 
            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Weight entered. Please check your input and try again.";
                return zReturnErrorMessage;
            }

            iCountOfDots = strInput_RMPurchaseRate.Count(f => f == '.');

            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Purchase Rate entered. Please check your input and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_RMOrigin.Length > 99)
            {
                zReturnErrorMessage = "'Origin' cannot be this long. Please reduce the input content and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_RMReceivedBy.Length > 99)
            {
                zReturnErrorMessage = "'Received By' cannot be this long. Please reduce the input content and try again.";
                return zReturnErrorMessage;
            } 
           
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
            zReturnErrorMessage = "*This action couldn't be completed. Please check your input and try again.";
        }

        return zReturnErrorMessage;

    }

}