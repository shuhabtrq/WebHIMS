using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BALUnfinishedInventory
/// </summary>
public class BALUnfinishedInventory
{
    public int UIID { get; set; }
    public int VendorID { get; set; }
    public int ItemID { get; set; }
    public int UINoOfPieces { get; set; }
    public double UIWeightReceived { get; set; }
    public string UIReceivedBy { get; set; }
    public string VendorDisplayName { get; set; }
    public string ItemDisplayName { get; set; }
    public double TotalRMWeightAllocatedToAVendor { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; }    

    /* List To DataTable */
    public static DataTable fnConvertListToDataTable(List<BALUnfinishedInventory> lstBALUnfinishedInventory)
    {
        DataTable dtable = new DataTable();

        dtable.Columns.Add("UIID", typeof(int));
        dtable.Columns.Add("VendorID", typeof(int));
        dtable.Columns.Add("ItemID", typeof(int));
        dtable.Columns.Add("UINoOfPieces", typeof(int));
        dtable.Columns.Add("UIWeightReceived", typeof(double));
        dtable.Columns.Add("UIReceivedBy", typeof(string));
        dtable.Columns.Add("VendorDisplayName", typeof(string));
        dtable.Columns.Add("ItemDisplayName", typeof(string));
        dtable.Columns.Add("TotalRMWeightAllocatedToAVendor", typeof(double));
        dtable.Columns.Add("CreatedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedBy", typeof(string));

        DataRow dr;

        foreach (var listItem in lstBALUnfinishedInventory)
        {
            dr = dtable.NewRow();
            dr["UIID"] = listItem.UIID;
            dr["VendorID"] = listItem.VendorID;
            dr["ItemID"] = listItem.ItemID;
            dr["UINoOfPieces"] = listItem.UINoOfPieces;
            dr["UIWeightReceived"] = listItem.UIWeightReceived;
            dr["UIReceivedBy"] = listItem.UIReceivedBy;
            dr["VendorDisplayName"] = listItem.VendorDisplayName;
            dr["ItemDisplayName"] = listItem.ItemDisplayName;
            dr["TotalRMWeightAllocatedToAVendor"] = listItem.TotalRMWeightAllocatedToAVendor;
            dr["CreatedOn"] = listItem.CreatedOn;
            dr["LastModifiedOn"] = listItem.LastModifiedOn;
            dr["LastModifiedBy"] = listItem.LastModifiedBy;
            dtable.Rows.Add(dr);
        }

        return dtable;
    }

    public static string ValidateUnfinishedInventory(string strInput_UIWeight, string strInput_UIReceivedBy)
    {
        string zReturnErrorMessage = string.Empty;

        int iCountOfDots = 0;

        try
        {
            iCountOfDots = strInput_UIWeight.Count(f => f == '.');

            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Weight entered. Please check your input and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_UIReceivedBy.Length > 99)
            {
                zReturnErrorMessage = "'Received By Name' cannot be this long. Please reduce the input content and try again.";
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