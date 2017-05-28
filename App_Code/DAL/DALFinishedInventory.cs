using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyEntityDataModel; 

/// <summary>
/// Summary description for DALFinishedInventory
/// </summary>
public class DALFinishedInventory
{
    public static List<BALFinishedInventory> AddFinishedInventory(BALFinishedInventory oBALFinishedInventory)
    {
        List<BALFinishedInventory> lstBALFinishedInventory = new List<BALFinishedInventory>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                HIMSTrnFinishedInventory tblHIMSTrnFinishedInventory = new HIMSTrnFinishedInventory();

                tblHIMSTrnFinishedInventory.intVendorID = oBALFinishedInventory.VendorID;
                tblHIMSTrnFinishedInventory.intItemID = oBALFinishedInventory.ItemID;
                tblHIMSTrnFinishedInventory.intFINoOfPieces = oBALFinishedInventory.FINoOfPieces;               
                tblHIMSTrnFinishedInventory.txtFIReceivedBy = oBALFinishedInventory.FIReceivedBy;
                tblHIMSTrnFinishedInventory.dtCreatedOn = oBALFinishedInventory.CreatedOn;
                tblHIMSTrnFinishedInventory.dtLastModifiedOn = oBALFinishedInventory.LastModifiedOn;
                tblHIMSTrnFinishedInventory.txtLastModifiedBy = oBALFinishedInventory.LastModifiedBy;

                HIMSEntity.HIMSTrnFinishedInventories.Add(tblHIMSTrnFinishedInventory);
                HIMSEntity.SaveChanges();
            }

            lstBALFinishedInventory = GetAllFinishedInventory();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALFinishedInventory;
    }

    public static List<BALFinishedInventory> UpdateFinishedInventory(BALFinishedInventory oBALFinishedInventory)
    {
        List<BALFinishedInventory> lstBALFinishedInventory = new List<BALFinishedInventory>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var tblRecordEntry = (from o in dbHIMSEntity.HIMSTrnFinishedInventories
                                      where o.intFIID == oBALFinishedInventory.FIID
                                      select o).FirstOrDefault();

                if (tblRecordEntry != null)
                {
                    tblRecordEntry.intVendorID = oBALFinishedInventory.VendorID;
                    tblRecordEntry.intItemID = oBALFinishedInventory.ItemID;
                    tblRecordEntry.intFINoOfPieces = oBALFinishedInventory.FINoOfPieces;                    
                    tblRecordEntry.txtFIReceivedBy = oBALFinishedInventory.FIReceivedBy;
                    tblRecordEntry.dtLastModifiedOn = oBALFinishedInventory.LastModifiedOn;
                    tblRecordEntry.txtLastModifiedBy = oBALFinishedInventory.LastModifiedBy;
                }

                dbHIMSEntity.SaveChanges();
            }

            lstBALFinishedInventory = GetAllFinishedInventory();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALFinishedInventory;
    }

    public static List<BALFinishedInventory> DeleteFinishedInventory(int iFIID)
    {
        List<BALFinishedInventory> lstBALFinishedInventory = new List<BALFinishedInventory>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                HIMSTrnFinishedInventory tblHIMSTrnFinishedInventory_SingleRecordEntry = dbHIMSEntity.HIMSTrnFinishedInventories.Find(iFIID);
                dbHIMSEntity.HIMSTrnFinishedInventories.Remove(tblHIMSTrnFinishedInventory_SingleRecordEntry);
                dbHIMSEntity.SaveChanges();
            }

            lstBALFinishedInventory = GetAllFinishedInventory();
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALFinishedInventory;

    }

    public static List<BALFinishedInventory> GetAllFinishedInventory()
    {
        List<BALFinishedInventory> lstBALFinishedInventory = new List<BALFinishedInventory>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                var result = from t1 in HIMSEntity.HIMSTrnFinishedInventories
                             join t2 in HIMSEntity.HIMSMstVendors
                             on t1.intVendorID equals t2.intVendorID
                             join t3 in HIMSEntity.HIMSMstItems
                             on t1.intItemID equals t3.intItemID
                             orderby t1.intFIID descending
                             select new BALFinishedInventory
                             {
                                 FIID = t1.intFIID,
                                 VendorID = t1.intVendorID,
                                 VendorDisplayName = t2.txtVendorName + " (" + t2.txtVendorOrgName + ")",
                                 ItemID = t1.intItemID,
                                 ItemDisplayName = t3.txtItemName,
                                 FINoOfPieces = t1.intFINoOfPieces,                                 
                                 FIReceivedBy = t1.txtFIReceivedBy,
                                 CreatedOn = t1.dtCreatedOn,
                                 LastModifiedOn = t1.dtLastModifiedOn,
                                 LastModifiedBy = t1.txtLastModifiedBy
                             };
                //var finalrslt = result.AsEnumerable().ToList();
                lstBALFinishedInventory = result.ToList<BALFinishedInventory>();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALFinishedInventory;

    }

    public static int GetTotalSumOfItemPiecesReceivedFromVendor(int iVendorID, int iItemID)
    {
        int intTotalSIPiecesReceived = 0;

        try
        {
            using (MyEntityDataModel.HIMSEntities HIMSEntity = new MyEntityDataModel.HIMSEntities())
            {
                intTotalSIPiecesReceived = (from s in HIMSEntity.HIMSTrnFinishedInventories
                                         where s.intVendorID == iVendorID && s.intItemID == iItemID
                                         select s.intFINoOfPieces).Sum();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return intTotalSIPiecesReceived;
    } 
      
}