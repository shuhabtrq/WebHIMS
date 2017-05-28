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
/// Summary description for DALVendors
/// </summary>
public class DALVendors
{
    public static List<BALVendors> AddVendor(BALVendors oBALVendor)
    {
        List<BALVendors> lstBALVendors = new List<BALVendors>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {    
                HIMSMstVendor tblHIMSMstVendors = new HIMSMstVendor();

                tblHIMSMstVendors.txtVendorName = oBALVendor.VendorName;
                tblHIMSMstVendors.txtVendorOrgName = oBALVendor.VendorOrgName;
                tblHIMSMstVendors.txtVendorEmailID = oBALVendor.VendorEmailID;
                tblHIMSMstVendors.txtVendorAddress = oBALVendor.VendorAddress;
                tblHIMSMstVendors.txtVendorPhone = oBALVendor.VendorPhone;
                tblHIMSMstVendors.txtVendorAdditionalInfo = oBALVendor.VendorAdditionalInfo;
                tblHIMSMstVendors.flgVendorIsActive = oBALVendor.VendorIsActive;
              
                HIMSEntity.HIMSMstVendors.Add(tblHIMSMstVendors);
                HIMSEntity.SaveChanges();
            }

            lstBALVendors = GetAllVendors();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendors;
    }

    public static List<BALVendors> UpdateVendor(BALVendors oBALVendor)
    {
        List<BALVendors> lstBALVendors = new List<BALVendors>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var tblRecordEntry = (from o in dbHIMSEntity.HIMSMstVendors
                                      where o.intVendorID == oBALVendor.VendorID
                                      select o).FirstOrDefault();

                if (tblRecordEntry != null)
                {
                    tblRecordEntry.txtVendorName = oBALVendor.VendorName;
                    tblRecordEntry.txtVendorOrgName = oBALVendor.VendorOrgName;
                    tblRecordEntry.txtVendorEmailID = oBALVendor.VendorEmailID;
                    tblRecordEntry.txtVendorAddress = oBALVendor.VendorAddress;
                    tblRecordEntry.txtVendorPhone = oBALVendor.VendorPhone;
                    tblRecordEntry.txtVendorAdditionalInfo = oBALVendor.VendorAdditionalInfo;
                    tblRecordEntry.flgVendorIsActive = oBALVendor.VendorIsActive;
                }

                dbHIMSEntity.SaveChanges();
            }

            lstBALVendors = GetAllVendors();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendors;
    }

    public static List<BALVendors> SoftDeleteVendor(int iVendorID)
    {
        List<BALVendors> lstBALVendors = new List<BALVendors>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                HIMSMstVendor tblHIMSMstVendors_SingleRecordEntry = dbHIMSEntity.HIMSMstVendors.Find(iVendorID);
                //dbHIMSEntity.HIMSMstVendors.Remove(tblHIMSMstVendors_SingleRecordEntry);
                tblHIMSMstVendors_SingleRecordEntry.flgVendorIsActive = false;   // Soft Deleting Vendor
                dbHIMSEntity.SaveChanges();

                /* Also make the corresponding record in table 'HIMSTrnVendorBilling' as inactive */
                HIMSTrnVendorBilling tblHIMSTrnVendorBilling_SingleRecordEntry = dbHIMSEntity.HIMSTrnVendorBillings.Find(iVendorID);
                tblHIMSTrnVendorBilling_SingleRecordEntry.flgVendorIsActive = false; // Soft Delete Vendor Bill
                dbHIMSEntity.SaveChanges();
            }

            lstBALVendors = GetAllVendors();
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendors;

    }

    public static List<BALVendors> GetAllVendors()
    {
        List<BALVendors> lstBALVendors = new List<BALVendors>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var result = from s in dbHIMSEntity.HIMSMstVendors where s.flgVendorIsActive == true
                             orderby s.intVendorID descending
                             select new BALVendors
                             {
                                 VendorID = s.intVendorID,
                                 VendorName = s.txtVendorName,
                                 VendorOrgName = s.txtVendorOrgName,
                                 VendorDisplayName = s.txtVendorName + " (" + s.txtVendorOrgName + ")",
                                 VendorEmailID = s.txtVendorEmailID,
                                 VendorPhone = s.txtVendorPhone,
                                 VendorAddress = s.txtVendorAddress,
                                 VendorAdditionalInfo = s.txtVendorAdditionalInfo, 
                                 VendorIsActive = s.flgVendorIsActive
                             };
                //var finalrslt = result.AsEnumerable().ToList();
                lstBALVendors = result.ToList<BALVendors>();
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendors;

    }

}