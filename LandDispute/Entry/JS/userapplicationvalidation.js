function isNumber(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function ValidateAlpha(evt) {
    var keyCode = (evt.which) ? evt.which : evt.keyCode
    if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && (keyCode != 32))
        return false;
    return true;
}

function ValidateNum(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    console.log(charCode);
    if (charCode > 31 && (charCode < 48 || charCode > 57) )
        return false;
    return true;  
}
function ValidateNumKhata(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    console.log(charCode);
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 44)
        return false;
    return true;
}
function ValidateMobile(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function Upper(ustr) {
    var str = ustr.value;
    ustr.value = str.toUpperCase();
}

