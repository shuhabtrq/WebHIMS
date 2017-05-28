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
/// Summary description for DALRawMaterial
/// </summary>
public class DALRawMaterial
{
    public static List<BALRawMaterial> AddRawMaterial(BALRawMaterial oBALRawMaterial)
    {
        List<BALRawMaterial> lstBALRawMaterial = new List<BALRawMaterial>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                HIMSTrnRawMaterial tblHIMSTrnRawMaterial = new HIMSTrnRawMaterial();   
             
                tblHIMSTrnRawMaterial.fltRMWeight = oBALRawMaterial.RMWeight;
                tblHIMSTrnRawMaterial.fltRMPurchaseRate = oBALRawMaterial.RMPurchaseRate;
                tblHIMSTrnRawMaterial.txtRMOrigin = oBALRawMaterial.RMOrigin;
                tblHIMSTrnRawMaterial.txtRMReceivedBy = oBALRawMaterial.RMReceivedBy;
                tblHIMSTrnRawMaterial.dtCreatedOn = oBALRawMaterial.CreatedOn;
                tblHIMSTrnRawMaterial.dtLastModifiedOn = oBALRawMaterial.LastModifiedOn;
                tblHIMSTrnRawMaterial.txtLastModifiedBy = oBALRawMaterial.LastModifiedBy;                 
                //HIMSEntity.AddToHIMSTrnRawMaterials(tblHIMSTrnRawMaterial);                
                HIMSEntity.HIMSTrnRawMaterials.Add(tblHIMSTrnRawMaterial);   
                HIMSEntity.SaveChanges();                
            }

            lstBALRawMaterial = GetAllRawMaterial();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALRawMaterial;
    }

    public static List<BALRawMaterial> UpdateRawMaterial(BALRawMaterial oBALRawMaterial)
    {
        List<BALRawMaterial> lstBALRawMaterial = new List<BALRawMaterial>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var tblRecordEntry = (from o in dbHIMSEntity.HIMSTrnRawMaterials
                                      where o.intRMID == oBALRawMaterial.RMID 
                                      select o).FirstOrDefault();

                if (tblRecordEntry != null)
                {
                    tblRecordEntry.fltRMWeight = oBALRawMaterial.RMWeight;
                    tblRecordEntry.fltRMPurchaseRate = oBALRawMaterial.RMPurchaseRate;
                    tblRecordEntry.txtRMOrigin = oBALRawMaterial.RMOrigin;
                    tblRecordEntry.txtRMReceivedBy = oBALRawMaterial.RMReceivedBy;
                    tblRecordEntry.dtLastModifiedOn = oBALRawMaterial.LastModifiedOn;
                    tblRecordEntry.txtLastModifiedBy = oBALRawMaterial.LastModifiedBy;  
                }              

                dbHIMSEntity.SaveChanges();
            }

            lstBALRawMaterial = GetAllRawMaterial();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALRawMaterial;
    }

    public static List<BALRawMaterial> DeleteRawMaterial(int iRMID)
    {
        List<BALRawMaterial> lstBALRawMaterial = new List<BALRawMaterial>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                HIMSTrnRawMaterial tblHIMSTrnRawMaterial_SingleRecordEntry = dbHIMSEntity.HIMSTrnRawMaterials.Find(iRMID);
                dbHIMSEntity.HIMSTrnRawMaterials.Remove(tblHIMSTrnRawMaterial_SingleRecordEntry);
                dbHIMSEntity.SaveChanges(); 
            }

            lstBALRawMaterial = GetAllRawMaterial();
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALRawMaterial;

    }

    public static List<BALRawMaterial> GetAllRawMaterial()
    {
        List<BALRawMaterial> lstBALRawMaterial = new List<BALRawMaterial>();

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                var result = from s in HIMSEntity.HIMSTrnRawMaterials
                             orderby s.intRMID descending
                             select new BALRawMaterial
                             {      
                                 RMID = s.intRMID,
                                 RMWeight = s.fltRMWeight,
                                 RMPurchaseRate = s.fltRMPurchaseRate,
                                 RMOrigin = s.txtRMOrigin,
                                 RMReceivedBy = s.txtRMReceivedBy,
                                 CreatedOn = s.dtCreatedOn,
                                 LastModifiedOn = s.dtLastModifiedOn,
                                 LastModifiedBy = s.txtLastModifiedBy 
                             };
                //var finalrslt = result.AsEnumerable().ToList();
                lstBALRawMaterial = result.ToList<BALRawMaterial>();                
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALRawMaterial;

    }

    public static double GetSumOfTotalRawMaterialProcured()
    {
        double dblTotalRMProcured = 0.0;

        try
        {
            using (HIMSEntities HIMSEntity = new HIMSEntities())
            {
                dblTotalRMProcured = (from s in HIMSEntity.HIMSTrnRawMaterials
                                      select s.fltRMWeight).Sum();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return Math.Round(dblTotalRMProcured, 2);
    }    

}