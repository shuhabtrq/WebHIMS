using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SessionManager
/// </summary>
public class SessionManager
{
    #region "Admin LogIn"

    public static string HIMSLogin_Admin = "HIMSLogin_Admin";

    #endregion

    #region "HIMS Raw Material"

    public static string HIMSRawMaterial_OnEdit_RMID = "HIMSRawMaterial_OnEdit_RMID";
    public static string LIST_BALRawMaterial = "LIST_BALRawMaterial";
    public static string TotalRawMaterialProcured = "TotalRawMaterialProcured";

    #endregion

    #region "HIMS Vendor"

    public static string HIMSVendor_OnEdit_VendorID = "HIMSVendor_OnEdit_VendorID";
    public static string LIST_BALVendors = "LIST_BALVendors";

    #endregion

    #region "HIMS Item"

    public static string HIMSItem_OnEdit_ItemID = "HIMSItem_OnEdit_ItemID";
    public static string LIST_BALItems = "LIST_BALItems";

    #endregion

    #region "HIMS Vendor Allocation"

    public static string HIMSVendorAllocation_OnEdit_VAID = "HIMSVendorAllocation_OnEdit_VAID";
    public static string LIST_BALVendorAllocation = "LIST_BALVendorAllocation";

    #endregion

    #region "HIMS Unfinished Inventory"

    public static string HIMSUnfinishedInventory_OnEdit_UIID = "HIMSUnfinishedInventory_OnEdit_UIID";
    public static string LIST_BALUnfinishedInventory = "LIST_BALUnfinishedInventory";

    #endregion

    #region "HIMS Finishing Process"

    public static string HIMSFinishingProcess_OnEdit_FPID = "HIMSFinishingProcess_OnEdit_FPID";
    public static string LIST_BALFinishingProcess = "LIST_BALFinishingProcess";

    #endregion

    #region "HIMS Finished Inventory"

    public static string HIMSFinishedInventory_OnEdit_FIID = "HIMSFinishedInventory_OnEdit_FIID";
    public static string LIST_BALFinishedInventory = "LIST_BALFinishedInventory";

    #endregion

    #region "HIMS Vendor Billing"

    public static string HIMSVendorBilling_OnEdit_VendorID = "HIMSVendorBilling_OnEdit_VendorID";
    public static string LIST_BALVendorBilling = "LIST_BALVendorBilling";

    #endregion

    #region "Application Level Error"

    public static string ApplicationErrorObject = "ApplicationErrorObject";

    #endregion
        
    public static object SetSession(string sessionString, object obj)
    {
        HttpContext.Current.Session[sessionString] = obj;
        return obj;
    }

    public static object GetSession(string sessionString)
    {
        return HttpContext.Current.Session[sessionString];
    }

}
