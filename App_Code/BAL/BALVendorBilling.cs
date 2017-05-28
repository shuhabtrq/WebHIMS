using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BALVendorBilling
/// </summary>
public class BALVendorBilling
{
    public int VBID { get; set; }
    public int VendorID { get; set; }
    public double TotalSUMToBePaid { get; set; } // SUM of Total Rates of Vendor Allocation and Finishing Process
    public double VBAmountPaid { get; set; }   
    public double VBDiscount { get; set; }
    public double VBWriteOff { get; set; }
    public double VBTotalAmountPaid { get; set; } // VBTotalAmountPaid (in dbase) = VBAmountPaid + VBDiscount - VBWriteOff   
    public double BalanceAmount { get; set; }     // TotalSUMToBePaid - VBTotalAmountPaid
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public bool VendorIsActive { get; set; }
    public string VendorDisplayName { get; set; }

    public static DataTable fnConvertListToDataTable(List<BALVendorBilling> lstBALVendorBilling)
    {
        DataTable dtable = new DataTable();

        dtable.Columns.Add("VBID", typeof(int));
        dtable.Columns.Add("VendorID", typeof(int));
        dtable.Columns.Add("TotalSUMToBePaid", typeof(double));
        dtable.Columns.Add("VBAmountPaid", typeof(double));
        dtable.Columns.Add("VBDiscount", typeof(double));
        dtable.Columns.Add("VBWriteOff", typeof(double));
        dtable.Columns.Add("VBTotalAmountPaid", typeof(double));
        dtable.Columns.Add("BalanceAmount", typeof(double));
        dtable.Columns.Add("CreatedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedOn", typeof(DateTime));
        dtable.Columns.Add("LastModifiedBy", typeof(string));
        dtable.Columns.Add("VendorIsActive", typeof(bool));
        dtable.Columns.Add("VendorDisplayName", typeof(string));

        DataRow dr;

        foreach (var listItem in lstBALVendorBilling)
        {
            dr = dtable.NewRow();
            dr["VBID"] = listItem.VBID;
            dr["VendorID"] = listItem.VendorID;
            dr["TotalSUMToBePaid"] = listItem.TotalSUMToBePaid;
            dr["VBAmountPaid"] = listItem.VBAmountPaid;
            dr["VBDiscount"] = listItem.VBDiscount;
            dr["VBWriteOff"] = listItem.VBWriteOff;
            dr["VBTotalAmountPaid"] = listItem.VBTotalAmountPaid;
            dr["BalanceAmount"] = listItem.BalanceAmount; 
            dr["CreatedOn"] = listItem.CreatedOn;
            dr["LastModifiedOn"] = listItem.LastModifiedOn;
            dr["LastModifiedBy"] = listItem.LastModifiedBy;
            dr["VendorIsActive"] = listItem.VendorIsActive;
            dr["VendorDisplayName"] = listItem.VendorDisplayName;
            dtable.Rows.Add(dr);
        }

        return dtable;
    }

    public static string ValidateVendorBilling(string strInput_VBAmountPaid, string strInput_VBDiscount, string strInput_VBWriteOff)
    {
        string zReturnErrorMessage = string.Empty;

        int iCountOfDots = 0;

        try
        {
            iCountOfDots = strInput_VBAmountPaid.Count(f => f == '.');

            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Amount entered. Please check your input and try again.";
                return zReturnErrorMessage;
            }

            iCountOfDots = strInput_VBDiscount.Count(f => f == '.');

            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Discount Rate entered. Please check your input and try again.";
                return zReturnErrorMessage;
            }

            iCountOfDots = strInput_VBWriteOff.Count(f => f == '.');

            if (iCountOfDots > 1)
            {
                zReturnErrorMessage = "*Invalid Write-Off Rate entered. Please check your input and try again.";
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