$(".selectEvent").change(function () {
    var p = $(this).val();
    var id = $(this).attr('data-id');
    if (p == "Y") {
        $("#descriptionspan" + id).addClass("hidden");
    }
    else {
        $("#descriptionspan" + id).removeClass("hidden");
    }
    
});
$(".clickEvent").click(function () {
   
    var id = $(this).attr('data-id');
    var desc = $("#ctl00_ContentPlaceHolder1_txtDescription" + id).val();
   
    var p = $("#ctl00_ContentPlaceHolder1_ddlVerifyStatus" + id).val();
    if (p == "0") {
        alert("Please select Status");
        return false;
    }
    if (p == "N") {
       
        if (desc == "") {
            alert("Please Enter Description");
            return false;
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }

});