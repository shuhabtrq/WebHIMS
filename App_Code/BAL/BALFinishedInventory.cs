using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BALFinishedInventory
/// </summary>
public class BALFinishedInventory
{
    public int FIID { get; set; }
    public int VendorID { get; set; }
    public int ItemID { get; set; }
    public int FINoOfPieces { get; set; }    
    public string FIReceivedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public string VendorDisplayName { get; set; }
    public string ItemDisplayName { get; set; }

    /* List To DataTable */
    public static DataTable fnConvertListToDataTable(List<BALFinishedInventory> lstBALFinishedInventory)
    {
        DataTable dtable = new DataTable();

        dtable.Columns.Add("FIID", typeof(int));
        dtable.Columns.Add("VendorID", typeof(int));
        dtable.Columns.Add("ItemID", typeof(int));
        dtable.Columns.Add("FINoOfPieces", typeof(int));
        dtable.Columns.Add("FIReceivedBy", typeof(string));
        dtable.Columns.Add("VendorDisplayName", typeof(string));
        dtable.Columns.Add("ItemDisplayName", typeof(string));
        dtable.Columns.Add("CreatedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedBy", typeof(string));

        DataRow dr;

        foreach (var listItem in lstBALFinishedInventory)
        {
            dr = dtable.NewRow();
            dr["FIID"] = listItem.FIID;
            dr["VendorID"] = listItem.VendorID;
            dr["ItemID"] = listItem.ItemID;
            dr["FINoOfPieces"] = listItem.FINoOfPieces;
            dr["FIReceivedBy"] = listItem.FIReceivedBy;
            dr["VendorDisplayName"] = listItem.VendorDisplayName;
            dr["ItemDisplayName"] = listItem.ItemDisplayName;
            dr["CreatedOn"] = listItem.CreatedOn;
            dr["LastModifiedOn"] = listItem.LastModifiedOn;
            dr["LastModifiedBy"] = listItem.LastModifiedBy;
            dtable.Rows.Add(dr);
        }

        return dtable;
    }

    public static string ValidateFinishedInventory(string strInput_FIReceivedBy)
    {
        string zReturnErrorMessage = string.Empty;

        try
        {
            if (strInput_FIReceivedBy.Length > 49)
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