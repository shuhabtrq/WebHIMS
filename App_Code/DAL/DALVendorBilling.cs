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
/// Summary description for DALVendorBilling
/// </summary>
public class DALVendorBilling
{
    public static List<BALVendorBilling> UpdateVendorBilling(BALVendorBilling oBALVendorBilling)
    {
        List<BALVendorBilling> lstBALVendorBilling = new List<BALVendorBilling>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var tblRecordEntry = (from o in dbHIMSEntity.HIMSTrnVendorBillings
                                      where o.intVendorID == oBALVendorBilling.VendorID
                                      select o).FirstOrDefault();

                if (tblRecordEntry != null)
                {
                    tblRecordEntry.fltVBAmountPaid = oBALVendorBilling.VBAmountPaid;
                    tblRecordEntry.fltVBDiscount = oBALVendorBilling.VBDiscount;
                    tblRecordEntry.fltVBWriteOff = oBALVendorBilling.VBWriteOff;
                    tblRecordEntry.fltVBTotalAmountPaid = oBALVendorBilling.VBTotalAmountPaid;
                    tblRecordEntry.dtLastModifiedOn = oBALVendorBilling.LastModifiedOn;
                    tblRecordEntry.txtLastModifiedBy = oBALVendorBilling.LastModifiedBy;
                    tblRecordEntry.flgVendorIsActive = oBALVendorBilling.VendorIsActive;
                }

                dbHIMSEntity.SaveChanges();
            }

            lstBALVendorBilling = GetAllVendorBilling();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendorBilling;
    }

    public static List<BALVendorBilling> GetAllVendorBilling()
    {
        List<BALVendorBilling> lstBALVendorBilling = new List<BALVendorBilling>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                var result = from s in HIMSEntity.spHIMS_GetAllVendorBills()
                             orderby s.intVendorID descending
                             select new BALVendorBilling
                             {
                                 VBID = s.intVBID,                                 
                                 VendorID = s.intVendorID,
                                 VendorDisplayName = s.txtVendorName + " (" + s.txtVendorOrgName + ")",
                                 TotalSUMToBePaid = Convert.ToDouble(s.VendorAllocationSUM) + Convert.ToDouble(s.FinishingProcessSUM),
                                 VBAmountPaid = s.fltVBAmountPaid,
                                 VBDiscount = s.fltVBDiscount,
                                 VBWriteOff = s.fltVBWriteOff,
                                 VBTotalAmountPaid = s.fltVBTotalAmountPaid,
                                 BalanceAmount = Convert.ToDouble(s.VendorAllocationSUM) + Convert.ToDouble(s.FinishingProcessSUM) - Convert.ToDouble(s.fltVBTotalAmountPaid),
                                 CreatedOn = s.dtCreatedOn,
                                 LastModifiedOn = s.dtLastModifiedOn,
                                 LastModifiedBy = s.txtLastModifiedBy,
                                 VendorIsActive = s.flgVendorIsActive
                             };
             
                lstBALVendorBilling = result.ToList<BALVendorBilling>();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendorBilling;

    }

    public static List<BALVendorBilling> GetSingleVendorBill(int intVendorID)
    {
        List<BALVendorBilling> lstBALVendorBilling = new List<BALVendorBilling>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                var result = from s in HIMSEntity.HIMSTrnVendorBillings where s.intVendorID == intVendorID && s.flgVendorIsActive == true
                             select new BALVendorBilling
                             {
                                 VBID = s.intVBID,
                                 VendorID = s.intVendorID,
                                 VBAmountPaid = s.fltVBAmountPaid,
                                 VBDiscount = s.fltVBDiscount,
                                 VBWriteOff = s.fltVBWriteOff,
                                 VBTotalAmountPaid = s.fltVBTotalAmountPaid,                                
                                 CreatedOn = s.dtCreatedOn,
                                 LastModifiedOn = s.dtLastModifiedOn,
                                 LastModifiedBy = s.txtLastModifiedBy,
                                 VendorIsActive = s.flgVendorIsActive
                             };

                lstBALVendorBilling = result.ToList<BALVendorBilling>();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendorBilling;

    }

}