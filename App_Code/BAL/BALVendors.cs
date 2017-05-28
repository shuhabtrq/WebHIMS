using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BALVendors
/// </summary>
public class BALVendors
{
    public int VendorID { get; set; }
    public string VendorName { get; set; }
    public string VendorOrgName { get; set; }
    public string VendorDisplayName { get; set; }
    public string VendorEmailID { get; set; }
    public string VendorAddress { get; set; }
    public string VendorPhone { get; set; }
    public string VendorAdditionalInfo { get; set; }
    public bool VendorIsActive { get; set; }

    /* List To DataTable */
    public static DataTable fnConvertListToDataTable(List<BALVendors> lstBALVendors)
    {
        DataTable dtable = new DataTable();

        dtable.Columns.Add("VendorID", typeof(int));
        dtable.Columns.Add("VendorName", typeof(string));
        dtable.Columns.Add("VendorOrgName", typeof(string));
        dtable.Columns.Add("VendorDisplayName", typeof(string));
        dtable.Columns.Add("VendorEmailID", typeof(string));
        dtable.Columns.Add("VendorAddress", typeof(string));
        dtable.Columns.Add("VendorPhone", typeof(string));
        dtable.Columns.Add("VendorAdditionalInfo", typeof(string));
        dtable.Columns.Add("VendorIsActive", typeof(bool));

        DataRow dr;

        foreach (var listItem in lstBALVendors)
        {
            dr = dtable.NewRow();
            dr["VendorID"] = listItem.VendorID;
            dr["VendorName"] = listItem.VendorName;
            dr["VendorOrgName"] = listItem.VendorOrgName;
            dr["VendorDisplayName"] = listItem.VendorDisplayName;
            dr["VendorEmailID"] = listItem.VendorEmailID;
            dr["VendorAddress"] = listItem.VendorAddress;
            dr["VendorPhone"] = listItem.VendorPhone;
            dr["VendorAdditionalInfo"] = listItem.VendorAdditionalInfo;
            dr["VendorIsActive"] = listItem.VendorIsActive;
            dtable.Rows.Add(dr);
        }

        return dtable;
    }

    public static string ValidateVendor(string strInput_VName, string strInput_VOrgName, string strInput_VEmail,
                                        string strInput_VPhone, string strInput_VAddress, string strInput_VAdditionalInfo)
    {
        string zReturnErrorMessage = string.Empty;

        try
        {
            if (strInput_VName.Length > 49)
            {
                zReturnErrorMessage = "'Vendor Name' cannot be this long. Please reduce the input content and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_VOrgName.Length > 99)
            {
                zReturnErrorMessage = "'Vendor Organisation Name' cannot be this long. Please reduce the input content and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_VEmail.Length > 49)
            {
                zReturnErrorMessage = "'Email' cannot be this long. Please reduce the input content and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_VPhone.Length > 99)
            {
                zReturnErrorMessage = "'Phone/Mob Number' cannot be this long. Please reduce the input content and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_VAddress.Length > 499)
            {
                zReturnErrorMessage = "'Address' cannot be this long. Please reduce the input content and try again.";
                return zReturnErrorMessage;
            }

            if (strInput_VAdditionalInfo.Length > 499)
            {
                zReturnErrorMessage = "'Additional Info' cannot be this long. Please reduce the input content and try again.";
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