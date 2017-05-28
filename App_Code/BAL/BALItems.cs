using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BALItems
/// </summary>
public class BALItems
{
    public int ItemID { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public bool ItemIsActive { get; set; }

    /* List To DataTable */
    public static DataTable fnConvertListToDataTable(List<BALItems> lstBALItems)
    {
        DataTable dtable = new DataTable();

        dtable.Columns.Add("ItemID", typeof(int));
        dtable.Columns.Add("ItemName", typeof(string));
        dtable.Columns.Add("ItemDescription", typeof(string));
        dtable.Columns.Add("ItemIsActive", typeof(bool));
        
        DataRow dr;

        foreach (var listItem in lstBALItems)
        {
            dr = dtable.NewRow();
            dr["ItemID"] = listItem.ItemID;
            dr["ItemName"] = listItem.ItemName;
            dr["ItemDescription"] = listItem.ItemDescription;
            dr["ItemIsActive"] = listItem.ItemIsActive;

            dtable.Rows.Add(dr);
        }

        return dtable;
    }

    public static string ValidateItem(string strInput_ItemName, string strInput_ItemDescription)
    {
        string zReturnErrorMessage = string.Empty;
        
        try
        {
            if (strInput_ItemName.Length > 99)
            {
                zReturnErrorMessage = "'Item Name' cannot be this long. Please reduce the input content and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_ItemDescription.Length > 499)
            {
                zReturnErrorMessage = "'Item Description' cannot be this long. Please reduce the input content and try again.";
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