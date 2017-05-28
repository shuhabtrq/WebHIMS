using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BALVendorAllocation
/// </summary>
public class BALVendorAllocation
{
    public int VAID { get; set; }
    public int VendorID { get; set; }
    public double VAWeight { get; set; }
    public double VARate { get; set; }    
    public string VAGivenByName { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public string VendorDisplayName { get; set; }

    /* List To DataTable */
    public static DataTable fnConvertListToDataTable(List<BALVendorAllocation> lstBALVendorAllocation)
    {
        DataTable dtable = new DataTable();

        dtable.Columns.Add("VAID", typeof(int));
        dtable.Columns.Add("VendorID", typeof(int));
        dtable.Columns.Add("VAWeight", typeof(double));
        dtable.Columns.Add("VARate", typeof(double));
        dtable.Columns.Add("VAGivenByName", typeof(string));
        dtable.Columns.Add("VendorDisplayName", typeof(string));
        dtable.Columns.Add("CreatedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedBy", typeof(string));

        DataRow dr;

        foreach (var listItem in lstBALVendorAllocation)
        {
            dr = dtable.NewRow();
            dr["VAID"] = listItem.VAID;
            dr["VendorID"] = listItem.VendorID;
            dr["VAWeight"] = listItem.VAWeight;
            dr["VARate"] = listItem.VARate;
            dr["VAGivenByName"] = listItem.VAGivenByName;
            dr["VendorDisplayName"] = listItem.VendorDisplayName;
            dr["CreatedOn"] = listItem.CreatedOn;
            dr["LastModifiedOn"] = listItem.LastModifiedOn;
            dr["LastModifiedBy"] = listItem.LastModifiedBy;
            dtable.Rows.Add(dr);
        }

        return dtable;
    }

    public static string ValidateVendorAllocationFields(string strInput_VAWeight, string strInput_VARate, string strInput_VAGivenByName)
    {
        string zReturnErrorMessage = string.Empty;

        int iCountOfDots = 0;

        try
        {
            iCountOfDots = strInput_VAWeight.Count(f => f == '.');

            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Weight entered. Please check your input and try again.";
                return zReturnErrorMessage;
            }

            iCountOfDots = strInput_VARate.Count(f => f == '.');

            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Rate entered. Please check your input and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_VAGivenByName.Length > 49)
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