using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MyEntityDataModel; 

/// <summary>
/// Summary description for DALFinishingProcess
/// </summary>
public class DALFinishingProcess
{
    public static List<BALFinishingProcess> AddFinishingProcess(BALFinishingProcess oBALFinishingProcess)
    {
        List<BALFinishingProcess> lstBALFinishingProcess = new List<BALFinishingProcess>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                HIMSTrnFinishingProcess tblHIMSTrnFinishingProcess = new HIMSTrnFinishingProcess();

                tblHIMSTrnFinishingProcess.intVendorID = oBALFinishingProcess.VendorID;
                tblHIMSTrnFinishingProcess.intItemID = oBALFinishingProcess.ItemID;
                tblHIMSTrnFinishingProcess.intFPNoOfPieces = oBALFinishingProcess.FPNoOfPieces;
                tblHIMSTrnFinishingProcess.fltFPTotalRate = oBALFinishingProcess.FPTotalRate;
                tblHIMSTrnFinishingProcess.txtFPGivenByName = oBALFinishingProcess.FPGivenByName;
                tblHIMSTrnFinishingProcess.dtCreatedOn = oBALFinishingProcess.CreatedOn;
                tblHIMSTrnFinishingProcess.dtLastModifiedOn = oBALFinishingProcess.LastModifiedOn;
                tblHIMSTrnFinishingProcess.txtLastModifiedBy = oBALFinishingProcess.LastModifiedBy;

                HIMSEntity.HIMSTrnFinishingProcesses.Add(tblHIMSTrnFinishingProcess);
                HIMSEntity.SaveChanges();
            }

            lstBALFinishingProcess = GetAllFinishingProcess();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALFinishingProcess;
    }

    public static List<BALFinishingProcess> UpdateFinishingProcess(BALFinishingProcess oBALFinishingProcess)
    {
        List<BALFinishingProcess> lstBALFinishingProcess = new List<BALFinishingProcess>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var tblRecordEntry = (from o in dbHIMSEntity.HIMSTrnFinishingProcesses
                                      where o.intFPID == oBALFinishingProcess.FPID
                                      select o).FirstOrDefault();

                if (tblRecordEntry != null)
                {
                    tblRecordEntry.intVendorID = oBALFinishingProcess.VendorID;
                    tblRecordEntry.intItemID = oBALFinishingProcess.ItemID;
                    tblRecordEntry.intFPNoOfPieces = oBALFinishingProcess.FPNoOfPieces;
                    tblRecordEntry.fltFPTotalRate = oBALFinishingProcess.FPTotalRate;
                    tblRecordEntry.txtFPGivenByName = oBALFinishingProcess.FPGivenByName;                   
                    tblRecordEntry.dtLastModifiedOn = oBALFinishingProcess.LastModifiedOn;
                    tblRecordEntry.txtLastModifiedBy = oBALFinishingProcess.LastModifiedBy;
                }

                dbHIMSEntity.SaveChanges();
            }

            lstBALFinishingProcess = GetAllFinishingProcess();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALFinishingProcess;
    }

    public static List<BALFinishingProcess> DeleteFinishingProcess(int iFPID)
    {
        List<BALFinishingProcess> lstBALFinishingProcess = new List<BALFinishingProcess>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                HIMSTrnFinishingProcess tblHIMSTrnFinishingProcess_SingleRecordEntry = dbHIMSEntity.HIMSTrnFinishingProcesses.Find(iFPID);
                dbHIMSEntity.HIMSTrnFinishingProcesses.Remove(tblHIMSTrnFinishingProcess_SingleRecordEntry);
                dbHIMSEntity.SaveChanges();
            }

            lstBALFinishingProcess = GetAllFinishingProcess();
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALFinishingProcess;

    }
    
    public static List<BALFinishingProcess> GetAllFinishingProcess()
    {
        List<BALFinishingProcess> lstBALFinishingProcess = new List<BALFinishingProcess>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                var result = from t1 in HIMSEntity.HIMSTrnFinishingProcesses
                             join t2 in HIMSEntity.HIMSMstVendors
                             on t1.intVendorID equals t2.intVendorID
                             join t3 in HIMSEntity.HIMSMstItems
                             on t1.intItemID equals t3.intItemID 
                             orderby t1.intFPID descending
                             select new BALFinishingProcess
                             {
                                 FPID = t1.intFPID,
                                 VendorID = t1.intVendorID,
                                 VendorDisplayName = t2.txtVendorName + " (" + t2.txtVendorOrgName + ")",
                                 ItemID = t1.intItemID,
                                 ItemDisplayName = t3.txtItemName, 
                                 FPNoOfPieces = t1.intFPNoOfPieces,
                                 FPTotalRate = t1.fltFPTotalRate,
                                 FPGivenByName = t1.txtFPGivenByName,
                                 CreatedOn = t1.dtCreatedOn,
                                 LastModifiedOn = t1.dtLastModifiedOn,
                                 LastModifiedBy = t1.txtLastModifiedBy
                             };
                //var finalrslt = result.AsEnumerable().ToList();
                lstBALFinishingProcess = result.ToList<BALFinishingProcess>();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALFinishingProcess;

    }

    public static int GetTotalSumOfItemPiecesGivenToVendor(int iVendorID, int iItemID)
    {
        int intTotalSIPiecesGiven = 0;

        try
        {
            using (MyEntityDataModel.HIMSEntities HIMSEntity = new MyEntityDataModel.HIMSEntities())
            {
                intTotalSIPiecesGiven = (from s in HIMSEntity.HIMSTrnFinishingProcesses
                                         where s.intVendorID == iVendorID && s.intItemID == iItemID
                                         select s.intFPNoOfPieces).Sum();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return intTotalSIPiecesGiven;
    } 

}