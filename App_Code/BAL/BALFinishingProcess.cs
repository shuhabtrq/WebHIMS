using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BALFinishingProcess
/// </summary>
public class BALFinishingProcess
{
    public int FPID { get; set; }
    public int VendorID { get; set; }
    public int ItemID { get; set; }
    public int FPNoOfPieces { get; set; }
    public double FPTotalRate { get; set; }
    public string FPGivenByName { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public string VendorDisplayName { get; set; }
    public string ItemDisplayName { get; set; }

    /* List To DataTable */
    public static DataTable fnConvertListToDataTable(List<BALFinishingProcess> lstBALFinishingProcess)
    {
        DataTable dtable = new DataTable();

        dtable.Columns.Add("FPID", typeof(int));
        dtable.Columns.Add("VendorID", typeof(int));
        dtable.Columns.Add("ItemID", typeof(int));
        dtable.Columns.Add("FPNoOfPieces", typeof(int));
        dtable.Columns.Add("FPTotalRate", typeof(double));
        dtable.Columns.Add("FPGivenByName", typeof(string));
        dtable.Columns.Add("VendorDisplayName", typeof(string));
        dtable.Columns.Add("ItemDisplayName", typeof(string));       
        dtable.Columns.Add("CreatedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedBy", typeof(string));

        DataRow dr;

        foreach (var listItem in lstBALFinishingProcess)
        {
            dr = dtable.NewRow();
            dr["FPID"] = listItem.FPID;
            dr["VendorID"] = listItem.VendorID;
            dr["ItemID"] = listItem.ItemID;
            dr["FPNoOfPieces"] = listItem.FPNoOfPieces;
            dr["FPTotalRate"] = listItem.FPTotalRate;
            dr["FPGivenByName"] = listItem.FPGivenByName;
            dr["VendorDisplayName"] = listItem.VendorDisplayName;
            dr["ItemDisplayName"] = listItem.ItemDisplayName;           
            dr["CreatedOn"] = listItem.CreatedOn;
            dr["LastModifiedOn"] = listItem.LastModifiedOn;
            dr["LastModifiedBy"] = listItem.LastModifiedBy;
            dtable.Rows.Add(dr);
        }

        return dtable;
    }

    public static string ValidateFinishingProcess(string strInput_FPTotalRate, string strInput_FPGivenByName)
    {
        string zReturnErrorMessage = string.Empty;

        int iCountOfDots = 0;

        try
        {
            iCountOfDots = strInput_FPTotalRate.Count(f => f == '.');

            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Rate entered. Please check your input and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_FPGivenByName.Length > 49)
            {
                zReturnErrorMessage = "'Given By Name' cannot be this long. Please reduce the input content and try again.";
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