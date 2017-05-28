using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.EntityModel;
using System.Data.Entity;
using MyEntityDataModel;

/// <summary>
/// Summary description for DALBilling
/// </summary>
public class DALBilling
{
    public static bool AddNewBillingTransaction(BALBilling oBALBilling)
    {
        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                HIMSTrnBilling tblHIMSTrnBilling = new HIMSTrnBilling();

                tblHIMSTrnBilling.intVendorID = oBALBilling.VendorID;
                tblHIMSTrnBilling.fltAmountPaid = oBALBilling.AmountPaid;
                tblHIMSTrnBilling.fltDiscount = oBALBilling.Discount;
                tblHIMSTrnBilling.fltWriteOff = oBALBilling.WriteOff;
                tblHIMSTrnBilling.flgIsActive = oBALBilling.IsActive;
                tblHIMSTrnBilling.dtCreatedOn = oBALBilling.CreatedOn;
                tblHIMSTrnBilling.dtLastModifiedOn = oBALBilling.LastModifiedOn;
                tblHIMSTrnBilling.txtLastModifiedBy = oBALBilling.LastModifiedBy;

                HIMSEntity.HIMSTrnBillings.Add(tblHIMSTrnBilling);
                HIMSEntity.SaveChanges();
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
            return false;
        }

        return true;
    }

}