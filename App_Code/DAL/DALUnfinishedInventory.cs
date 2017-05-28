using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyEntityDataModel; 

/// <summary>
/// Summary description for DALUnfinishedInventory
/// </summary>
public class DALUnfinishedInventory
{
    public static List<BALUnfinishedInventory> AddUnfinishedInventory(BALUnfinishedInventory oBALUnfinishedInventory)
    {
        List<BALUnfinishedInventory> lstBALUnfinishedInventory = new List<BALUnfinishedInventory>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                HIMSTrnUnfinishedInventory tblHIMSTrnUnfinishedInventory = new HIMSTrnUnfinishedInventory();

                tblHIMSTrnUnfinishedInventory.intVendorID = oBALUnfinishedInventory.VendorID;
                tblHIMSTrnUnfinishedInventory.intItemID = oBALUnfinishedInventory.ItemID;
                tblHIMSTrnUnfinishedInventory.intUINoOfPieces = oBALUnfinishedInventory.UINoOfPieces;
                tblHIMSTrnUnfinishedInventory.fltUIWeightReceived = oBALUnfinishedInventory.UIWeightReceived;
                tblHIMSTrnUnfinishedInventory.txtUIReceivedBy = oBALUnfinishedInventory.UIReceivedBy;               
                tblHIMSTrnUnfinishedInventory.dtCreatedOn = oBALUnfinishedInventory.CreatedOn;
                tblHIMSTrnUnfinishedInventory.dtLastModifiedOn = oBALUnfinishedInventory.LastModifiedOn;
                tblHIMSTrnUnfinishedInventory.txtLastModifiedBy = oBALUnfinishedInventory.LastModifiedBy;

                HIMSEntity.HIMSTrnUnfinishedInventories.Add(tblHIMSTrnUnfinishedInventory);
                HIMSEntity.SaveChanges();
            }

            lstBALUnfinishedInventory = GetAllUnfinishedInventory();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALUnfinishedInventory;
    }

    public static List<BALUnfinishedInventory> UpdateUnfinishedInventory(BALUnfinishedInventory oBALUnfinishedInventory)
    {
        List<BALUnfinishedInventory> lstBALUnfinishedInventory = new List<BALUnfinishedInventory>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var tblRecordEntry = (from o in dbHIMSEntity.HIMSTrnUnfinishedInventories
                                      where o.intUIID == oBALUnfinishedInventory.UIID
                                      select o).FirstOrDefault();

                if (tblRecordEntry != null)
                {
                    tblRecordEntry.intVendorID = oBALUnfinishedInventory.VendorID;
                    tblRecordEntry.intItemID = oBALUnfinishedInventory.ItemID;
                    tblRecordEntry.intUINoOfPieces = oBALUnfinishedInventory.UINoOfPieces;
                    tblRecordEntry.fltUIWeightReceived = oBALUnfinishedInventory.UIWeightReceived;
                    tblRecordEntry.txtUIReceivedBy = oBALUnfinishedInventory.UIReceivedBy; 
                    tblRecordEntry.dtLastModifiedOn = oBALUnfinishedInventory.LastModifiedOn;
                    tblRecordEntry.txtLastModifiedBy = oBALUnfinishedInventory.LastModifiedBy;
                }

                dbHIMSEntity.SaveChanges();
            }

            lstBALUnfinishedInventory = GetAllUnfinishedInventory();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALUnfinishedInventory;
    }

    public static List<BALUnfinishedInventory> DeleteUnfinishedInventory(int iUIID)
    {
        List<BALUnfinishedInventory> lstBALUnfinishedInventory = new List<BALUnfinishedInventory>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                HIMSTrnUnfinishedInventory tblHIMSTrnUnfinishedInventory_SingleRecordEntry = dbHIMSEntity.HIMSTrnUnfinishedInventories.Find(iUIID);
                dbHIMSEntity.HIMSTrnUnfinishedInventories.Remove(tblHIMSTrnUnfinishedInventory_SingleRecordEntry);
                dbHIMSEntity.SaveChanges();
            }

            lstBALUnfinishedInventory = GetAllUnfinishedInventory();
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALUnfinishedInventory;

    }

    public static List<BALUnfinishedInventory> GetAllUnfinishedInventory()
    {
         /* SELECT t1.intUIID, t1.intVendorID, t1.intItemID, t2.txtVendorName, t3.txtItemName, t4.intVendorID, SUM(t4.fltVAWeight) AS 'SUM'
            FROM HIMSTrnUnfinishedInventory t1 
            inner join HIMSMstVendors t2 on t2.intVendorID = t1.intVendorID
            inner join HIMSMstItems t3 on t3.intItemID = t1.intItemID
            inner join HIMSTrnVendorAllocation t4 on t4.intVendorID = t1.intVendorID
            group by t4.intVendorID, t1.intUIID, t1.intVendorID, t1.intItemID, t2.txtVendorName, t3.txtItemName */

        List<BALUnfinishedInventory> lstBALUnfinishedInventory = new List<BALUnfinishedInventory>();
        
        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                var result = from t1 in HIMSEntity.HIMSTrnUnfinishedInventories
                             join t2 in HIMSEntity.HIMSMstVendors
                             on t1.intVendorID equals t2.intVendorID
                             join t3 in HIMSEntity.HIMSMstItems
                             on t1.intItemID equals t3.intItemID
                             join t4 in HIMSEntity.HIMSTrnVendorAllocations
                             on t1.intVendorID equals t4.intVendorID
                             group new { t1, t2, t3, t4 } by new
                             {
                                 t4.intVendorID,
                                 t2.txtVendorName,
                                 t2.txtVendorOrgName,
                                 t3.txtItemName,
                                 t1.intUIID,
                                 t1.intItemID,
                                 t1.intUINoOfPieces,
                                 t1.fltUIWeightReceived,
                                 t1.txtUIReceivedBy,
                                 t1.dtCreatedOn,
                                 t1.dtLastModifiedOn,
                                 t1.txtLastModifiedBy
                             } into grp
                             orderby grp.Key.intUIID descending
                             select new BALUnfinishedInventory
                            {
                                UIID = grp.Key.intUIID,
                                VendorID = grp.Key.intVendorID,
                                ItemID = grp.Key.intItemID,
                                UINoOfPieces = grp.Key.intUINoOfPieces,
                                UIWeightReceived = grp.Key.fltUIWeightReceived,
                                UIReceivedBy = grp.Key.txtUIReceivedBy,
                                VendorDisplayName = grp.Key.txtVendorName + " (" + grp.Key.txtVendorOrgName + ")",
                                ItemDisplayName = grp.Key.txtItemName,
                                TotalRMWeightAllocatedToAVendor = grp.Sum(x => x.t4.fltVAWeight),
                                CreatedOn = grp.Key.dtCreatedOn,
                                LastModifiedOn = grp.Key.dtLastModifiedOn,
                                LastModifiedBy = grp.Key.txtLastModifiedBy,
                            };
                lstBALUnfinishedInventory = result.ToList<BALUnfinishedInventory>();

            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALUnfinishedInventory;
    }
    
    public static List<BALUnfinishedInventory> GetAllUnfinishedInventory_NotUsed()
    {
        List<BALUnfinishedInventory> lstBALUnfinishedInventory = new List<BALUnfinishedInventory>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                var result = from t1 in HIMSEntity.HIMSTrnUnfinishedInventories
                             join t2 in HIMSEntity.HIMSMstVendors
                             on t1.intVendorID equals t2.intVendorID
                             join t3 in HIMSEntity.HIMSMstItems 
                             on t1.intItemID equals t3.intItemID 
                             orderby t1.intUIID descending
                             select new BALUnfinishedInventory
                             {
                                 UIID = t1.intUIID,
                                 VendorID = t1.intVendorID,
                                 ItemID = t1.intItemID,
                                 UINoOfPieces = t1.intUINoOfPieces,
                                 UIWeightReceived = t1.fltUIWeightReceived,
                                 UIReceivedBy = t1.txtUIReceivedBy,
                                 VendorDisplayName = t2.txtVendorName + " (" + t2.txtVendorOrgName + ")",   
                                 ItemDisplayName = t3.txtItemName, 
                                 CreatedOn = t1.dtCreatedOn,
                                 LastModifiedOn = t1.dtLastModifiedOn,
                                 LastModifiedBy = t1.txtLastModifiedBy
                             };
                //var finalrslt = result.AsEnumerable().ToList();
                lstBALUnfinishedInventory = result.ToList<BALUnfinishedInventory>();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALUnfinishedInventory;

    }

    public static double GetTotalUIReceivedFromVendor(int iVendorID)
    {
        double dblTotalUIReceived = 0.0;

        try
        {
            using (MyEntityDataModel.HIMSEntities HIMSEntity = new MyEntityDataModel.HIMSEntities())
            {
                dblTotalUIReceived = (from s in HIMSEntity.HIMSTrnUnfinishedInventories 
                                      where s.intVendorID == iVendorID
                                      select s.fltUIWeightReceived).Sum();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return Math.Round(dblTotalUIReceived, 2);
    } 

}