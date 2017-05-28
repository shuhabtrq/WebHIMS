using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyEntityDataModel; 

/// <summary>
/// Summary description for DALVendorAllocation
/// </summary>
public class DALVendorAllocation
{
    public static List<BALVendorAllocation> AddVendorAllocation(BALVendorAllocation oBALVendorAllocation)
    {
        List<BALVendorAllocation> lstBALVendorAllocation = new List<BALVendorAllocation>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                HIMSTrnVendorAllocation tblHIMSTrnVendorAllocation = new HIMSTrnVendorAllocation();

                tblHIMSTrnVendorAllocation.intVendorID = oBALVendorAllocation.VendorID;
                tblHIMSTrnVendorAllocation.fltVAWeight = oBALVendorAllocation.VAWeight;
                tblHIMSTrnVendorAllocation.fltVARate = oBALVendorAllocation.VARate;
                tblHIMSTrnVendorAllocation.txtVAGivenByName = oBALVendorAllocation.VAGivenByName;
                tblHIMSTrnVendorAllocation.dtCreatedOn = oBALVendorAllocation.CreatedOn;
                tblHIMSTrnVendorAllocation.dtLastModifiedOn = oBALVendorAllocation.LastModifiedOn;
                tblHIMSTrnVendorAllocation.txtLastModifiedBy = oBALVendorAllocation.LastModifiedBy;
                               
                HIMSEntity.HIMSTrnVendorAllocations.Add(tblHIMSTrnVendorAllocation);
                HIMSEntity.SaveChanges();
            }

            lstBALVendorAllocation = GetAllVendorAllocation();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendorAllocation;
    }

    public static List<BALVendorAllocation> UpdateVendorAllocation(BALVendorAllocation oBALVendorAllocation)
    {
        List<BALVendorAllocation> lstBALVendorAllocation = new List<BALVendorAllocation>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var tblRecordEntry = (from o in dbHIMSEntity.HIMSTrnVendorAllocations
                                      where o.intVAID == oBALVendorAllocation.VAID
                                      select o).FirstOrDefault();

                if (tblRecordEntry != null)
                {
                    tblRecordEntry.intVendorID = oBALVendorAllocation.VendorID;
                    tblRecordEntry.fltVAWeight = oBALVendorAllocation.VAWeight;
                    tblRecordEntry.fltVARate = oBALVendorAllocation.VARate;
                    tblRecordEntry.txtVAGivenByName = oBALVendorAllocation.VAGivenByName;
                    tblRecordEntry.dtLastModifiedOn = oBALVendorAllocation.LastModifiedOn;
                    tblRecordEntry.txtLastModifiedBy = oBALVendorAllocation.LastModifiedBy;
                }

                dbHIMSEntity.SaveChanges();
            }

            lstBALVendorAllocation = GetAllVendorAllocation();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendorAllocation;
    }

    public static List<BALVendorAllocation> DeleteVendorAllocation(int iVAID)
    {
        List<BALVendorAllocation> lstBALVendorAllocation = new List<BALVendorAllocation>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                HIMSTrnVendorAllocation tblHIMSTrnVendorAllocation_SingleRecordEntry = dbHIMSEntity.HIMSTrnVendorAllocations.Find(iVAID);
                dbHIMSEntity.HIMSTrnVendorAllocations.Remove(tblHIMSTrnVendorAllocation_SingleRecordEntry);
                dbHIMSEntity.SaveChanges();
            }

            lstBALVendorAllocation = GetAllVendorAllocation();
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendorAllocation;

    }

    public static List<BALVendorAllocation> GetAllVendorAllocation()
    {
        List<BALVendorAllocation> lstBALVendorAllocation = new List<BALVendorAllocation>();
        
        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                var result = from t1 in HIMSEntity.HIMSTrnVendorAllocations
                             join t2 in HIMSEntity.HIMSMstVendors 
                             on t1.intVendorID equals t2.intVendorID
                             orderby t1.intVAID descending
                             select new BALVendorAllocation
                             {
                                 VAID = t1.intVAID,
                                 VendorID = t1.intVendorID,
                                 VendorDisplayName = t2.txtVendorName + " (" + t2.txtVendorOrgName + ")",
                                 VAWeight = t1.fltVAWeight,
                                 VARate = t1.fltVARate,                                
                                 VAGivenByName = t1.txtVAGivenByName,
                                 CreatedOn = t1.dtCreatedOn,
                                 LastModifiedOn = t1.dtLastModifiedOn,
                                 LastModifiedBy = t1.txtLastModifiedBy
                             };
                //var finalrslt = result.AsEnumerable().ToList();
                lstBALVendorAllocation = result.ToList<BALVendorAllocation>();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALVendorAllocation;

    }

    public static double GetTotalRawMaterialAllocatedToVendor(int iVendorID)
    {
        double dblTotalRMProcured = 0.0;

        try
        {
            using (MyEntityDataModel.HIMSEntities HIMSEntity = new MyEntityDataModel.HIMSEntities())
            {
                dblTotalRMProcured = (from s in HIMSEntity.HIMSTrnVendorAllocations
                                      where s.intVendorID == iVendorID
                                      select s.fltVAWeight).Sum();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return Math.Round(dblTotalRMProcured, 2);
    }    

}