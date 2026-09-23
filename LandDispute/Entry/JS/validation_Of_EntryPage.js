//-Prati-vadivalidate--step-1--
function ValidateVadiDetail() {
    //debugger;
    var txtNamePerAadhaar = document.getElementById('<%=txtNamePerAadhaar.ClientID%>');
    if (txtNamePerAadhaar != null) {
        if (txtNamePerAadhaar.value.trim() == "") {
            alert("कृपया वादी का नाम अंकित करें...!");
            txtNamePerAadhaar.focus();
            return false;
        }
    }

    var txtFName = document.getElementById('<%=txtFName.ClientID%>');
    if (txtFName.value.trim() == "") {
        alert("कृपया पिता/ पति का नाम अंकित करें...!");
        txtFName.focus();
        return false;
    }

    var ddlgender = document.getElementById('<%=ddlgender.ClientID%>');
    if (ddlgender != null) {
        if (ddlgender.selectedIndex == 0) {
            alert("कृपया लिंग चुनें...!");
            ddlgender.focus();
            return false;
        }
    }

    var ddlUserDist = document.getElementById('<%=ddlUserDist.ClientID%>');
    if (ddlUserDist.selectedIndex == 0) {
        alert("कृपया जिला चुनें...!");
        ddlUserDist.focus();
        return false;
    }

    var ddlUserSubdivision = document.getElementById('<%=ddlUserSubdivision.ClientID%>');
    if (ddlUserSubdivision.selectedIndex == 0) {
        alert("कृपया अनुमंडल चुनें...!");
        ddlUserSubdivision.focus();
        return false;
    }

    var ddlUserBlock = document.getElementById('<%=ddlUserBlock.ClientID%>');
    if (ddlUserBlock.selectedIndex == 0) {
        alert("कृपया अंचल चुनें...!");
        ddlUserBlock.focus();
        return false;
    }

    var ddlUserThana = document.getElementById('<%=ddlUserThana.ClientID%>');
    if (ddlUserThana.selectedIndex == 0) {
        alert("कृपया थाना चुनें...!");
        ddlUserThana.focus();
        return false;
    }

    var ddlUserAreatype = document.getElementById('<%=ddlUserAreatype.ClientID%>');
    if (ddlUserAreatype.selectedIndex == 0) {
        alert("कृपया क्षेत्र का प्रकार चुनें...!");
        ddlUserAreatype.focus();
        return false;
    }
 
    var ddlUserPanchyat = document.getElementById('<%=ddlUserPanchyat.ClientID%>');
    if (ddlUserPanchyat.selectedIndex == 0) {
        var labUVillage = document.getElementById('<%=labUVillage.ClientID%>');
        if (labUVillage != null)
        {
            if (labUVillage.textContent === "ग्राम पंचायत") {
                alert("कृपया ग्राम पंचायत चुनें...!");
                ddlUserPanchyat.focus();
                return false;
            }
            else {
                alert("कृपया नगर निकाय चुनें...!");
                ddlUserPanchyat.focus();
                return false;
            }
        }
    }

    if (ddlUserAreatype.selectedIndex == 1)
    {
        var ddlUserVillage = document.getElementById('<%=ddlUserVillage.ClientID%>');
        if (ddlUserVillage.selectedIndex == 0) {
            alert("कृपया राजस्व ग्राम चुनें...!");
            ddlUserVillage.focus();
            return false;
        }
    }
    

    debugger;
    if (ddlUserAreatype.selectedIndex == 2)
    {
        var ddlUserWard = document.getElementById('<%=ddlUserWard.ClientID%>');
        if (ddlUserWard.selectedIndex == 0) {
            alert("कृपया वार्ड चुनें...!");
            ddlUserWard.focus();
            return false;
        }
    }
          
   
    var txtUserMohalla = document.getElementById('<%=txtUserMohalla.ClientID%>');
    if (txtUserMohalla != null)
    {
        if (txtUserMohalla.value.trim() == "") {
            alert("कृपया मोहल्ला संख्या अंकित करें...!");
            txtUserMohalla.focus();
            return false;
        }
    }
         

    //var txtUserVillage_Anya = document.getElementById('<%=txtUserVillage_Anya.ClientID%>');
    //if (txtUserVillage_Anya != null) {
    //    if (txtUserVillage_Anya.value.trim() == "") {
    //        alert("कृपया अन्य ग्राम अंकित करें...!");
    //        txtUserVillage_Anya.focus();
    //        return false;
    //    }
    //}

    //var txtUserWard_Anya = document.getElementById('<%=txtUserWard_Anya.ClientID%>');
    //if (txtUserWard_Anya != null) {
    //    if (txtUserWard_Anya.value.trim() == "") {
    //        alert("कृपया अन्य वार्ड अंकित करें...!");
    //        txtUserWard_Anya.focus();
    //        return false;
    //    }
    //}

          var txtvadimobile = document.getElementById('<%=txtvadimobile.ClientID%>');
    if (txtvadimobile != null)
    {
        if (txtvadimobile.value.trim() == "") {
            alert("कृपया मोबाइल संख्या अंकित करें...!");
            txtvadimobile.focus();
            return false;
        }
    }

    //if ((txtvadimobile.value.trim()).length != 10) {
    //    alert("Please Enter valid mobile no...!");
    //    txtvadimobile.focus();
    //    return false;
    //}

    var dept = document.getElementById('<%=ddl_is_vadi_from_an_dept.ClientID%>');
    if (dept.selectedIndex == 0 || dept.selectedIndex == 1)
    {
        if (dept.selectedIndex == 0) {
            alert("क्या वादी किसी विभाग का प्रतिनिधि है कृपया चुनें...!");
            dept.focus();
            return false;
        }
        var ddlWvibhaag_naam = document.getElementById('<%=ddlWvibhaag_naam.ClientID%>');
        var txtWvibhaag_padanaam = document.getElementById('<%=txtWvibhaag_padanaam.ClientID%>');
        if (ddlWvibhaag_naam.selectedIndex == 0) {
            alert("कृपया विभाग का नाम चुनें...!");
            ddlWvibhaag_naam.focus();
            return false;
        }
    }

    var org = document.getElementById('<%=ddl_is_vadi_from_an_org.ClientID%>');
    if (org.selectedIndex == 0 || org.selectedIndex == 1) {
        if (org.selectedIndex == 0) {
            alert("क्या वादी किसी संस्था का प्रतिनिधि है कृपया चुनें...!");
            org.focus();
            return false;
        }
        var wp = document.getElementById('<%=ddlWsanstha_naam.ClientID%>');
        var ddlWsanshaanya_naam=document.getElementById('<%=ddlWsanshaanya_naam.ClientID%>');
        var txtWsanstha_naam = document.getElementById('<%=txtWsanstha_naam.ClientID%>');
            
        if (wp.selectedIndex == 0) {
            alert("कृपया संस्था का प्रकार चुनें...!");
            wp.focus();
            return false;
        }
        if (ddlWsanshaanya_naam.selectedIndex == 0) {
            alert("कृपया संस्था का सम्बन्ध चुनें...!");
            ddlWsanshaanya_naam.focus();
            return false;
        }
        if (txtWsanstha_naam.value.trim() == "") {
            alert("कृपया संस्था का नाम अंकित करें...!");
            txtWsanstha_naam.focus();
            return false;
        }
    }       
    return true;
}

function ValidationBhumiVivad() {
    //debugger;
    var ddlDistrict = document.getElementById('<%=ddlDistrict.ClientID%>');
    if (ddlDistrict.options[ddlDistrict.selectedIndex].value == 0) {
        alert("कृपया जिला चुनें...!");
        ddlrakabasankhya.focus();
        return false;
    }
    var ddlSubdivision = document.getElementById('<%=ddlSubdivision.ClientID%>');
    if (ddlSubdivision.options[ddlSubdivision.selectedIndex].value == 0) {
        alert("कृपया सब डिवीज़न चुनें...!");
        ddlSubdivision.focus();
        return false;
    }
    var ddlBlock = document.getElementById('<%=ddlBlock.ClientID%>');
    if (ddlBlock.options[ddlBlock.selectedIndex].value == 0) {
        alert("कृपया ब्लाक का चुनाव करें...!");
        ddlBlock.focus();
        return false;
    }
    var ddlPolice = document.getElementById('<%=ddlPolice.ClientID%>');
    if (ddlPolice.options[ddlPolice.selectedIndex].value == 0) {
        alert("कृपया पुलिस स्टेशन का चुनाव करें...!");
        ddlPolice.focus();
        return false;
    }
    var ddlareatype = document.getElementById('<%=ddlareatype.ClientID%>');
    if (ddlareatype.options[ddlareatype.selectedIndex].value == 0) {
        alert("कृपया क्षेत्र का प्रकार का चुनाव करें...!");
        ddlareatype.focus();
        return false;
    }

    if (ddlareatype.options[ddlareatype.selectedIndex].value == 1) {
        var ddlPanchyat = document.getElementById('<%=ddlPanchyat.ClientID%>');
        if (ddlPanchyat.selectedIndex == 0) {
            alert("कृपया ग्राम पंचायत करें...!");
            ddlPanchyat.focus();
            return false;
        }
    }

    if (ddlareatype.options[ddlareatype.selectedIndex].value == 2) {
        var ddlPanchyat = document.getElementById('<%=ddlPanchyat.ClientID%>');
        if (ddlPanchyat.selectedIndex == 0) {
            alert("कृपया नगर निकाय करें...!");
            ddlPanchyat.focus();
            return false;
        }
    }

    if (ddlareatype.options[ddlareatype.selectedIndex].value == 1) {
        var ddlVillage = document.getElementById('<%=ddlVillage.ClientID%>');
        if (ddlVillage.selectedIndex == 0) {
            alert("कृपया गाँव का चुनाव करें...!");
            ddlVillage.focus();
            return false;
        }
    }

    if (ddlareatype.options[ddlareatype.selectedIndex].value == 1) {
        var ddlVillage = document.getElementById('<%=ddlVillage.ClientID%>');
        if (ddlVillage.selectedIndex == 0) {
            alert("कृपया गाँव का चुनाव करें...!");
            ddlVillage.focus();
            return false;
        }
    }

    if (ddlareatype.options[ddlareatype.selectedIndex].value == 2) {
        var ddlWard = document.getElementById('<%=ddlWard.ClientID%>');
        if (ddlWard.selectedIndex == 0) {
            alert("कृपया वार्ड का चुनाव करें...!");
            ddlWard.focus();
            return false;
        }
    }

    var ddl_vivad_adyatan_sthiti = document.getElementById('<%=ddl_vivad_adyatan_sthiti.ClientID%>');
    if (ddl_vivad_adyatan_sthiti.options[ddl_vivad_adyatan_sthiti.selectedIndex].vlaue == 0) {
        alert("कृपया विवाद का अद्यतन कारक का चुनाव करें...!");
        ddl_vivad_adyatan_sthiti.focus();
        return false;
    }

    var ddlbhumitype = document.getElementById('<%=ddlbhumitype.ClientID%>');
    if (ddlbhumitype.options[ddlbhumitype.selectedIndex].value == 0) {
        alert("कृपया भूमि का प्रकार चुनें...!");
        ddlbhumitype.focus();
        return false;
    }

    if (ddlbhumitype.options[ddlbhumitype.selectedIndex].value == 2) {
        var ddlsarkaribhumitype = document.getElementById('<%=ddlsarkaribhumitype.ClientID%>');

        if (ddlsarkaribhumitype.options[ddlsarkaribhumitype.selectedIndex].value == 0) {
            alert("कृपया सरकारी भूमि का प्रकार चुनें...!");
            ddlsarkaribhumitype.focus();
            return false;
        }
    }
    //debugger;
    if (ddlbhumitype.options[ddlbhumitype.selectedIndex].value == 2) {
        var ddlsarkaribhumitype = document.getElementById('<%=ddlsarkaribhumitype.ClientID%>');
        if (ddlsarkaribhumitype.options[ddlsarkaribhumitype.selectedIndex].value == 6) {
            var txtsarkaribhumitype_Anya = document.getElementById('<%=txtsarkaribhumitype_Anya.ClientID%>');
            var divSarkaribhumitype = document.getElementById('<%=divSarkaribhumitype.ClientID%>');
            if (divSarkaribhumitype.style.display = "block" && txtsarkaribhumitype_Anya.value == "") {
                alert("कृपया सरकारी भूमि का प्रकार (अगर अन्य है) अंकित करें...!");
                txtsarkaribhumitype_Anya.focus();
                return false;
            }
        }
    }


    var ddlbhumivivadtype = document.getElementById('<%=ddlbhumivivadtype.ClientID%>');
    if (ddlbhumivivadtype.options[ddlbhumivivadtype.selectedIndex].value == 0) {
        alert("कृपया भूमि के विवाद का प्रकार चुनें...!");
        ddlbhumivivadtype.focus();
        return false;
    }
    if (ddlbhumivivadtype.options[ddlbhumivivadtype.selectedIndex].value == 20) {
        var txtbhumivivad_Anya = document.getElementById('<%=txtbhumivivad_Anya.ClientID%>');
        var divBhumivivad_Anya = document.getElementById('<%=divBhumivivad_Anya.ClientID%>');
        if (divBhumivivad_Anya.style.display = "block" && txtbhumivivad_Anya.value == "") {
            alert("कृपया भूमि विवाद का प्रकार (अगर अन्य है) अंकित करें..");
            txtbhumivivad_Anya.focus();
            return false;
        }
    }


    var txtAwadenKiTithi = document.getElementById('<%=txtAwadenKiTithi.ClientID%>');
    if (txtAwadenKiTithi.value == "") {
        alert("कृपया आवेदन की तिथि अंकित करें..");
        txtAwadenKiTithi.focus();
        return false;
    }

    var txtVadiVivarani = document.getElementById('<%=txtVadiVivarani.ClientID%>');
    if (txtVadiVivarani.value == "") {
        alert("कृपया वादी द्वारा संक्षिप्त विवरणी अंकित करें..");
        txtVadiVivarani.focus();
        return false;
    }

    return true;
}

//Prati-vadivalidate--step-2
function ValidatePratiVadiDetail() {
    debugger;
    var txtPName = document.getElementById('<%=txtPName.ClientID%>');
    if (txtPName.value.trim() == "") {
        alert("कृपया प्रतिवादी का नाम अंकित करें...!");
        txtPName.focus();
        return false;
    }

    var dept = document.getElementById('<%=ddl_is_pratiVadi_from_an_dept.ClientID%>');
    if (dept.selectedIndex == 0 || dept.selectedIndex == 1) {
        if (dept.selectedIndex == 0) {
            alert("क्या प्रतिवादी किसी विभाग का प्रतिनिधि है कृपया चुनें...!");
            dept.focus();
            return false;
        }

        var ddlPvibhaag_naam = document.getElementById('<%=ddlPvibhaag_naam.ClientID%>');
        var txtPvibhaag_padanaam = document.getElementById('<%=txtPvibhaag_padanaam.ClientID%>');
        if (ddlPvibhaag_naam.selectedIndex == 0) {
            alert("कृपया विभाग का नाम चुनें...!");
            ddlPvibhaag_naam.focus();
            return false;
        }
    }

    var org = document.getElementById('<%=ddl_is_pratiVadi_from_an_org.ClientID%>');
    if (org.selectedIndex == 0 || org.selectedIndex == 1) {
        if (org.selectedIndex == 0) {
            alert("क्या प्रतिवादी किसी संस्था का प्रतिनिधि है कृपया चुनें...!");
            org.focus();
            return false;
        }
        var wp = document.getElementById('<%=ddlPsanstha_naam.ClientID%>');
        var txtPsanstha_naam = document.getElementById('<%=txtPsanstha_naam.ClientID%>');
        var txtPsanstha_padanaam = document.getElementById('<%=txtPsanstha_padanaam.ClientID%>');

        if (wp.selectedIndex == 0) {
            alert("कृपया संस्था का प्रकार चुनें...!");
            wp.focus();
            return false;
        }

        if (txtPsanstha_naam.value.trim() == "") {
            alert("कृपया संस्था का नाम अंकित करें...!");
            txtPsanstha_naam.focus();
            return false;
        }

        //if (txtPsanstha_padanaam.value.trim() == "") {
        //    alert("कृपया संस्था में पदनाम अंकित करें...!");
        //    txtPsanstha_padanaam.focus();
        //    return false;
        //}

    }

    var txtPPanchyat_Anya = document.getElementById('<%=txtPPanchyat_Anya.ClientID%>');
    if (txtPPanchyat_Anya != null) {
        if (txtPPanchyat_Anya.value.trim() == "") {
            alert("कृपया अन्य पंचायत अंकित करें...!");
            txtPPanchyat_Anya.focus();
            return false;
        }
    }

    var txtPVillage_Anya = document.getElementById('<%=txtPVillage_Anya.ClientID%>');
    if (txtPVillage_Anya != null) {
        if (txtPVillage_Anya.value.trim() == "") {
            alert("कृपया अन्य ग्राम अंकित करें...!");
            txtPVillage_Anya.focus();
            return false;
        }
    }

    var txtPWard_Anya = document.getElementById('<%=txtPWard_Anya.ClientID%>');
    if (txtPWard_Anya != null) {
        if (txtPWard_Anya.value.trim() == "") {
            alert("कृपया अन्य वार्ड अंकित करें...!");
            txtPWard_Anya.focus();
            return false;
        }
    }

    return true;
}

//ValidateBhumiKaVivaran-vadivalidate--step-3
function ValidateBhumiKaVivaran() {
    //debugger;
    var txtkhatasankhya = document.getElementById('<%=txtkhatasankhya.ClientID%>');
    if (txtkhatasankhya.value.trim() == "") {
        alert("कृपया खाता संख्या अंकित करें...!");
        txtkhatasankhya.focus();
        return false;
    }

    var txtkhesarasankhya = document.getElementById('<%=txtkhesarasankhya.ClientID%>');
    if (txtkhesarasankhya.value.trim() == "") {
        alert("कृपया खेसरा संख्या अंकित करें...!");
        txtkhesarasankhya.focus();
        return false;
    }

    var txtrakabasankhya = document.getElementById('<%=txtrakabasankhya.ClientID%>');
    if (txtrakabasankhya.value.trim() == "") {
        alert("कृपया रकबा अंकित करें...!");
        txtrakabasankhya.focus();
        return false;
    }

    var ddlrakabasankhya = document.getElementById('<%=ddlrakabasankhya.ClientID%>');
    if (ddlrakabasankhya.selectedIndex == 0) {
        alert("कृपया रकबा का मात्रक चुनें...!");
        ddlrakabasankhya.focus();
        return false;
    }

    var txtrakabasankhya1 = document.getElementById('<%=txtrakabasankhya1.ClientID%>');
    if (txtrakabasankhya1.value.trim() == "") {
        alert("कृपया रकबा अंकित करें...!");
        txtrakabasankhya1.focus();
        return false;
    }

    var ddlrakabasankhya1 = document.getElementById('<%=ddlrakabasankhya1.ClientID%>');
    if (ddlrakabasankhya1.selectedIndex == 0) {
        alert("कृपया रकबा का मात्रक चुनें...!");
        ddlrakabasankhya1.focus();
        return false;
    }

    var txtrakabasankhya2 = document.getElementById('<%=txtrakabasankhya2.ClientID%>');
    if (txtrakabasankhya2.value.trim() == "") {
        alert("कृपया रकबा अंकित करें...!");
        txtrakabasankhya2.focus();
        return false;
    }

    var ddlrakabasankhya2 = document.getElementById('<%=ddlrakabasankhya2.ClientID%>');
    if (ddlrakabasankhya2.selectedIndex == 0) {
        alert("कृपया रकबा का मात्रक चुनें...!");
        ddlrakabasankhya2.focus();
        return false;
    }

    var ddlkhatiyan_me_jaminvivran = document.getElementById('<%=ddlkhatiyan_me_jaminvivran.ClientID%>');
    if (ddlkhatiyan_me_jaminvivran.selectedIndex == 0) {
        alert("कृपया खतियान में जमीन की किस्म का विवरण चुनें...!");
        ddlkhatiyan_me_jaminvivran.focus();
        return false;
    }
    return true;
}

//ValidateBhumiKaVivaran-vadivalidate--step-4
function ValidateVadiEvidenceDetail() {
    //debugger;
    var ddlIsVadiEvi = document.getElementById("<%=ddlIsVadiEvi.ClientID%>");
    var ddlVadiEvidenceType = document.getElementById("<%=ddlVadiEvidenceType.ClientID%>");
    var txtVadiEvidenceType = document.getElementById("<%=txtVadiEvidenceType.ClientID%>");

    if (ddlIsVadiEvi.value.trim() == "0") {
        alert("कृपया वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?  चुनें...!");
        ddlIsVadiEvi.focus();
        return false;
    }

    if (ddlVadiEvidenceType.selectedIndex == 0 && ddlIsVadiEvi.value.trim() == "Y") {
        alert("कृपया साक्ष्य का प्रकार चुनें...!");
        ddlVadiEvidenceType.focus();
        return false;
    }

    if (ddlVadiEvidenceType.value == 9 && ddlIsVadiEvi.value.trim() == "Y") {
        if (txtVadiEvidenceType != null) {
            if (txtVadiEvidenceType.value.trim() == '') {
                alert("कृपया अन्य साक्ष्य का प्रकार अंकित करें...!");
                txtVadiEvidenceType.focus();
                return false;
            }
        }
    }

    var obj1 = document.getElementById("<%=file_vadi_dastavej_new.ClientID%>");
    var source1 = obj1.value;
    var ext1 = source1.substring(source1.lastIndexOf(".") + 1).toLowerCase();
    if (validFiles.indexOf(ext1) <= -1) {
        alert("Please Upload Document in Pdf File |");
        obj1.focus();
        return false;

    }
    else {
        if (obj1.files[0].size > (0.2 * 1024 * 1024 * 1024)) {
            alert("File size must be less than or equal to 2 MB |");
            obj1.focus();
            return false;

        }
    }
    return true;
}
function ValidatePrativadiEvidenceDetail() {

    // debugger;
    var ddlIsPvadiEvi = document.getElementById("<%=ddlIsPvadiEvi.ClientID%>");
    var ddlPrativadiEvidenceType = document.getElementById("<%=ddlPrativadiEvidenceType.ClientID%>");
    var txtPrativadiEvidenceType = document.getElementById("<%=txtPrativadiEvidenceType.ClientID%>");

    if (ddlIsPvadiEvi.value.trim() == "0") {
        alert("कृपया प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ? चुनें...!");
        ddlIsPvadiEvi.focus();
        return false;
    }

    if (ddlPrativadiEvidenceType.selectedIndex == 0 && ddlIsPvadiEvi.value.trim() == "Y") {
        alert("कृपया साक्ष्य का प्रकार चुनें...!");
        ddlPrativadiEvidenceType.focus();
        return false;
    }

    if (ddlPrativadiEvidenceType.selectedIndex == 9 && ddlIsPvadiEvi.value.trim() == "Y") {
        if (txtPrativadiEvidenceType != null) {
            if (txtPrativadiEvidenceType.value.trim() == '') {
                alert("कृपया अन्य साक्ष्य का प्रकार अंकित करें...!");
                txtPrativadiEvidenceType.focus();
                return false;
            }
        }
    }


    var obj1 = document.getElementById("<%=file_Prativadi_dastavej_new.ClientID%>");
    var source1 = obj1.value;
    var ext1 = source1.substring(source1.lastIndexOf(".") + 1).toLowerCase();
    if (validFiles.indexOf(ext1) <= -1) {
        alert("Please Upload Document in Pdf File |");
        obj1.focus();
        return false;

    } else {
        if (obj1.files[0].size > (0.2 * 1024 * 1024 * 1024)) {
            alert("File size must be less than or equal to 2 MB |");
            obj1.focus();
            return false;

        }

    }
    return true;
}

//ValidateBhumiKaVivaran-vadivalidate--step-6

function ValidateBhumiVivad() {

    //debugger;
    var txtghatanaDate = document.getElementById('<%=txtghatanaDate.ClientID%>');
    if (txtghatanaDate.value.trim() == "") {
        alert("कृपया घटना / वारदात की तिथि अंकित करें...!");
        txtghatanaDate.focus();
        return false;
    }

    var ddlPrathmiki_huyee_hai = document.getElementById('<%=ddlPrathmiki_huyee_hai.ClientID%>');
    if (ddlPrathmiki_huyee_hai.selectedIndex == 0) {
        alert("क्या प्राथमिकी दर्ज है ? हां/नहीं चुनें...!");
        ddlPrathmiki_huyee_hai.focus();
        return false;
    }

    if (ddlPrathmiki_huyee_hai.selectedIndex == 1) {
        var txtFIR_sankhya = document.getElementById('<%=txtFIR_sankhya.ClientID%>');
        if (txtFIR_sankhya.value.trim() == "") {
            alert("कृपया प्राथमिकी संख्या अंकित करें...!");
            txtFIR_sankhya.focus();
            return false;
        }
    }
    var ddlAprathmiki_huyee_hai = document.getElementById('<%=ddlAprathmiki_huyee_hai.ClientID%>');
    if (ddlAprathmiki_huyee_hai.selectedIndex == 0) {
        alert("क्या अप्राथमिकी दर्ज है ? हां/नहीं चुनें...!");
        ddlAprathmiki_huyee_hai.focus();
        return false;
    }

    if (ddlAprathmiki_huyee_hai.selectedIndex == 1) {
        var chk107 = document.getElementById('<%=chk107.ClientID%>');
        var chk109 = document.getElementById('<%=chk109.ClientID%>');
        var chk110 = document.getElementById('<%=chk110.ClientID%>');
        var chk113 = document.getElementById('<%=chk113.ClientID%>');
        var chk116 = document.getElementById('<%=chk116.ClientID%>');
        var chk133 = document.getElementById('<%=chk133.ClientID%>');
        var chk144 = document.getElementById('<%=chk144.ClientID%>');
        var chk145 = document.getElementById('<%=chk145.ClientID%>');
        var chk147 = document.getElementById('<%=chk147.ClientID%>');
        if (chk107.checked == false && chk109.checked == false && chk110.checked == false && chk113.checked == false && chk116.checked == false
            && chk133.checked == false && chk144.checked == false && chk145.checked == false && chk147.checked == false) {
            alert("कृपया धारा चुनें...!");
            chk107.focus();
            return false;
        }

        var txtAFIR_sankhya = document.getElementById('<%=txtAFIR_sankhya.ClientID%>');
        if (txtAFIR_sankhya.value.trim() == "") {
            alert("कृपया अप्राथमिकी संख्या अंकित करें...!");
            txtAFIR_sankhya.focus();
            return false;
        }

        var ddlSanhaStatus = document.getElementById('<%=ddlSanhaStatus.ClientID%>');
        if (ddlSanhaStatus.selectedIndex == 0) {
            alert("क्या सनहा दर्ज है ? हां/नहीं चुनें...!");
            ddlSanhaStatus.focus();
            return false;
        }

        if (ddlSanhaStatus.selectedIndex == 1) {
            var txtSanahaSankhiyan = document.getElementById('<%=txtSanahaSankhiyan.ClientID%>');
            if (txtSanahaSankhiyan.value.trim() == "") {
                alert("कृपया सनहा संख्या अंकित करें...!");
                txtSanahaSankhiyan.focus();
                return false;
            }
        }
    }

    return true;
}
function ValidateNayaylayDetails() {

    //debugger;

    var ddlnyayalaya = document.getElementById('<%=ddlnyayalaya.ClientID%>');
    if (ddlnyayalaya.selectedIndex == 0) {
        alert("कृपया न्यायालय चुनें...!");
        ddlnyayalaya.focus();
        return false;
    }

    var ddlnyayalaya_type = document.getElementById('<%=ddlnyayalaya_type.ClientID%>');
    if (ddlnyayalaya_type != null) {
        if (ddlnyayalaya_type.selectedIndex == 0) {
            alert("कृपया न्यायालय का प्रकार चुनें...!");
            ddlnyayalaya_type.focus();
            return false;
        }
    }

    var ddlDist_nyayalaya_type = document.getElementById('<%=ddlDist_nyayalaya_type.ClientID%>');
    if (ddlDist_nyayalaya_type != null) {
        if (ddlDist_nyayalaya_type.selectedIndex == 0) {
            alert("कृपया जिला चुनें...!");
            ddlDist_nyayalaya_type.focus();
            return false;
        }
    }

    var ddlSubdivision_nyayalaya_type = document.getElementById('<%=ddlSubdivision_nyayalaya_type.ClientID%>');
    if (ddlSubdivision_nyayalaya_type != null) {
        if (ddlSubdivision_nyayalaya_type.selectedIndex == 0) {
            alert("कृपया अनुमंडल चुनें...!");
            ddlSubdivision_nyayalaya_type.focus();
            return false;
        }
    }

    var ddlVibhag_nyayalay_type = document.getElementById('<%=ddlVibhag_nyayalay_type.ClientID%>');
    if (ddlVibhag_nyayalay_type != null) {
        if (ddlVibhag_nyayalay_type.selectedIndex == 0) {
            alert("कृपया विभाग चुनें...!");
            ddlVibhag_nyayalay_type.focus();
            return false;
        }
    }

    var txtdayarvaadsankhya_nayalay = document.getElementById('<%=txtdayarvaadsankhya_nayalay.ClientID%>');
    if (txtdayarvaadsankhya_nayalay.value.trim() == "") {
        alert("कृपया वादी की वाद संख्या / वर्ष अंकित करें...!");
        txtdayarvaadsankhya_nayalay.focus();
        return false;
    }

    var txtvaadiname_nayaylay = document.getElementById('<%=txtvaadiname_nayaylay.ClientID%>');
    if (txtvaadiname_nayaylay.value.trim() == "") {
        alert("कृपया वादी का नाम अंकित करें...!");
        txtvaadiname_nayaylay.focus();
        return false;
    }

    var txtprativadi_nayaylay = document.getElementById('<%=txtprativadi_nayaylay.ClientID%>');
    if (txtprativadi_nayaylay.value.trim() == "") {
        alert("कृपया प्रतिवादी का नाम अंकित करें...!");
        txtprativadi_nayaylay.focus();
        return false;
    }
    return true;
}

//date validation
function checkDate(sender, args) {
    if (sender._selectedDate > new Date()) {
        alert("You cannot select a day latter than today!");
        sender._selectedDate = new Date();
        // set the date back to the current date
        sender._textbox.set_Value("")
    }
}
function dateValidate(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 45) {
        return true;
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    return true;
}
