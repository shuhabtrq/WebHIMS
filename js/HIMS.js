/* HIMS - Javascript File
   Author: Shuhab (www.shuhab.com) */

/* PopUp before deleting a row */
function fnDeleteConfirm() {

    if (confirm("Are you sure you want to delete this record?")) {
        return true;
    }
    else {
        return false;
    }
}
/* END */

$(document).ready(function () {
    $(".latest_img").fadeTo("slow", 0.3);
    $(".latest_img").hover(function () {
        $(this).fadeTo("slow", 1.0);
    }, function () {
        $(this).fadeTo("slow", 0.3);
    });
});

/* Vendor Allocation - Running out of stock (On Click of Add New in View1) */
function fnRMOutOfStockFirst() {

    var fltCurrentStock = parseFloat(document.getElementById('cntBody_lblRawMaterialLeftAfterAllocation').innerHTML);

    if (fltCurrentStock < 0.0) {

        if (confirm("Warning: You have run out of stock!\n" 
                  + "It is recommended that you add new stock before allocating it to vendors.\n\n"
                  + "Are you sure you still want to proceed with vendor allocation of raw material?")) {
            return true;
        }
        else {
            return false;
        }

    }

}

/* Vendor Allocation - Running out of stock (On Click of Add in View2) */
function fnRMOutOfStockSecond() {
    
    var fltCurrentStock = parseFloat(document.getElementById('cntBody_lblCurrentStockHidden').innerHTML);   

    if (fltCurrentStock < 0.0) {

        if (confirm("Warning: You have run out of stock!\nIt is recommended that you add new stock before allocating it to vendors.\n\nAre you sure you still want to proceed and allocate raw material to this vendor?")) {
            return true;
        }
        else {
            return false;
        }

    }

}

/* Unfinished Inventory */
function fnReceivedUIStockExceeded() {

    var drpVendorSelectedIndex = document.getElementById('cntBody_VendorsList_drpVendors').selectedIndex;

    if (drpVendorSelectedIndex <= 0) {
        alert("Please select a Vendor");
        return false;
    }

    var drpItemSelectedIndex = document.getElementById('cntBody_ItemsList_drpItems').selectedIndex;

    if (drpItemSelectedIndex <= 0) {
        alert("Please select an Item Category");
        return false;
    }


    var fltTotalRMAllocated = parseFloat(document.getElementById('cntBody_lblTotalRMAllocatedToVendor').innerHTML);

    if (isNaN(fltTotalRMAllocated)) {
        // Control reaches here when Vendor is not selected.
        return false;
    }

    if (fltTotalRMAllocated <= 0) {

        alert("This action cannot be completed as no Raw Material has been allocated to this vendor.");
        return false;
    }


    var fltBalanceStock = parseFloat(document.getElementById('cntBody_lblBalanceWeight').innerHTML);

    if (isNaN(fltBalanceStock)) {
        // Control reaches here when Vendor is not selected.
        return false;  
    }

    var fltNewReceivedUI = document.getElementById('cntBody_txtUIReceivedWeight').value;

    if (isNaN(fltNewReceivedUI)) {
        // Control reaches here when invalid UI Received Wt is entered.
        return false;
    }

    if ((fltBalanceStock - fltNewReceivedUI) < 0.0) {
        if (confirm("Warning: Received Unfinished Inventory Weight (kgs) cannot be more than the Allocated Raw Material Weight (kgs).\n"                  
                  + "\nAre you sure you want to ignore this check and proceed?")) {
            return true;
        }
        else {
            return false;
        }
    }
    
}

/* Finishing Process */
function fnValidateJSFinishingProcess() {

    var drpVendorSelectedIndex = document.getElementById('cntBody_VendorsList_drpVendors').selectedIndex;

    if (drpVendorSelectedIndex <= 0) {
        alert("Please select a Vendor");
        return false;
    }

    var drpItemSelectedIndex = document.getElementById('cntBody_ItemsList_drpItems').selectedIndex;

    if (drpItemSelectedIndex <= 0) {
        alert("Please select an Item Category");
        return false;
    }

    return true;
}

/* Finished Inventory */
function fnValidateJSFinishedInventory() {

    var drpVendorSelectedIndex = document.getElementById('cntBody_VendorsList_drpVendors').selectedIndex;

    if (drpVendorSelectedIndex <= 0) {
        alert("Please select a Vendor");
        return false;
    }

    var drpItemSelectedIndex = document.getElementById('cntBody_ItemsList_drpItems').selectedIndex;

    if (drpItemSelectedIndex <= 0) {
        alert("Please select an Item Category");
        return false;
    }

    var intTotalSIGiven = parseInt(document.getElementById('cntBody_lblTotalSIGivenToVendor').innerHTML);

    if (isNaN(intTotalSIGiven)) {
        // Control reaches here when Vendor is not selected.
        return false;
    }

    if (intTotalSIGiven <= 0) {

        alert("This action cannot be completed as no piece of item has been given for finishing to this vendor.");
        return false;
    }


    var intBalance = parseFloat(document.getElementById('cntBody_lblBalance').innerHTML);

    if (isNaN(intBalance)) {
        // Control reaches here when Vendor is not selected.
        return false;
    }

    var intNewReceivedPieces = document.getElementById('cntBody_txtPieceCount').value;

    if (isNaN(intNewReceivedPieces)) {
        // Control reaches here when invalid value is entered.
        return false;
    }

    if ((intBalance - intNewReceivedPieces) < 0) {
        if (confirm("Warning: Received number of item pieces cannot be more than the item pieces given to vendor for finishing process.\n"
                  + "\nAre you sure you want to ignore this check and proceed?")) {
            return true;
        }
        else {
            return false;
        }
    }
    
}