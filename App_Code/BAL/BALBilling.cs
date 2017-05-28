using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BALBilling
/// </summary>
public class BALBilling
{
    public int BillingID { get; set; }
    public int VendorID { get; set; }    
    public double AmountPaid { get; set; }
    public double Discount { get; set; }
    public double WriteOff { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; }       
}