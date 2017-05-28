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
/// Summary description for DALItems
/// </summary>
public class DALItems
{
    public static List<BALItems> AddItem(BALItems oBALItem)
    {
        List<BALItems> lstBALItems = new List<BALItems>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                HIMSMstItem tblHIMSMstItems = new HIMSMstItem();

                tblHIMSMstItems.txtItemName = oBALItem.ItemName;
                tblHIMSMstItems.txtItemDescription = oBALItem.ItemDescription;
                tblHIMSMstItems.flgItemIsActive = oBALItem.ItemIsActive;

                dbHIMSEntity.HIMSMstItems.Add(tblHIMSMstItems);
                dbHIMSEntity.SaveChanges();
            }

            lstBALItems = GetAllItems();
        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALItems;
    }

    public static List<BALItems> UpdateItem(BALItems oBALItem)
    {
        List<BALItems> lstBALItems = new List<BALItems>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var tblRecordEntry = (from o in dbHIMSEntity.HIMSMstItems
                                      where o.intItemID == oBALItem.ItemID
                                      select o).FirstOrDefault();

                if (tblRecordEntry != null)
                {
                    tblRecordEntry.txtItemName = oBALItem.ItemName;
                    tblRecordEntry.txtItemDescription = oBALItem.ItemDescription;
                    tblRecordEntry.flgItemIsActive = oBALItem.ItemIsActive;
                }

                dbHIMSEntity.SaveChanges();
            }

            lstBALItems = GetAllItems();

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALItems;
    }

    public static List<BALItems> SoftDeleteItem(int iItemID)
    {
        List<BALItems> lstBALItems = new List<BALItems>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                HIMSMstItem tblHIMSMstItems_SingleRecordEntry = dbHIMSEntity.HIMSMstItems.Find(iItemID);                
                tblHIMSMstItems_SingleRecordEntry.flgItemIsActive = false;   // Soft Deleting Item
                dbHIMSEntity.SaveChanges();
            }

            lstBALItems = GetAllItems();
        }

        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALItems;

    }

    public static List<BALItems> GetAllItems()
    {
        List<BALItems> lstBALItems = new List<BALItems>();

        try
        {
            using (HIMSEntities dbHIMSEntity = new HIMSEntities())
            {
                var result = from s in dbHIMSEntity.HIMSMstItems where s.flgItemIsActive == true
                             orderby s.intItemID descending
                             select new BALItems
                             {
                                 ItemID = s.intItemID,
                                 ItemName = s.txtItemName,
                                 ItemDescription = s.txtItemDescription
                             };

                lstBALItems = result.ToList<BALItems>();
            }

        }
        catch (Exception ex)
        {
            ErrorHandler.WriteError(ex.Message);
        }

        return lstBALItems;

    }

}