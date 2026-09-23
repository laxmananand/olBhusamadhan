<%@ Page Language="C#"  MasterPageFile="~/LandDispute/BLKADM/Samvida.master" AutoEventWireup="true" CodeFile="User_Application.aspx.cs" Inherits="LandDispute_BLKADM_User_Application" %>

   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../../assets/js/jquery.min.js" ></script>
    
    <style type="text/css">
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }
        .lbl
        {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>

    <style type="text/css">
        .zindex {
            background-color: #FAF5EF;
            z-index:10001;
        }
    </style>
         
    <link rel="stylesheet" type="text/css" href="../../Frontpage/assets/css/bootstrap.min.css" />

   <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>

    <%--show pdf file.--%>
     <style type="text/css">
        .grid th
        {
            padding: 4px;
            font-weight: bold;
            font-size: small;
            text-align: center;
        }
        
        .grid td
        {
            font-size: smaller;
            padding: 4px;                     
        }      
        .grid tr:hover
        {
            background-color: #d8f9d3;
        }       
        .grid td:hover
        {
            background-color: #ff2;
        }        
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
         body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }
        .modal
        {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }
        #divImage
        {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 700px;
            width: 1200px;
            padding: 3px;
            border: solid 1px black;
        }
        .fontsize
        {
              font-size: 10px !important;
        }
        .labeltextsize{
            font-weight: bolder;
            padding-top:5px;
        }
         .CSSTableGeneratorGrid td, th{
    vertical-align: middle;
    border: 1px solid #014e9c;
    padding: 2px;
    font-size: 10pt !important;
    font-weight: normal;
    color: #000000;
    background-color: White;
}
    </style>
   
    <script type="text/javascript">
        function fnLinkbutton(objlinkbutton) {         
           
            var urlpdf = document.getElementById(objlinkbutton).getAttribute("path");

            getpdfdocument(urlpdf);
           
        return false;      
    }

        function getpdfdocument(urlpdf) {
            var imgDiv = document.getElementById("divImage");
            var inlineFrameExample = document.getElementById("inlineFrameExample");
            urlpdf = urlpdf.replace("~", "");
           // urlpdf = ("http://10.133.25.21/ImageServiceHome" + urlpdf).replace(' ', '');
            urlpdf = ("http://10.133.25.21/ImageServiceHome" + urlpdf);
            urlpdf = urlpdf.trim();
            $.ajax({
                type: "POST",
                url: "User_Application.aspx/Getpdf",
                data: "{'url':'" + urlpdf + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                   
                    inlineFrameExample.src = response.d;
                },
                failure: function (msg) {
                    alert(msg);
                }
            });



            var width = document.body.clientWidth;
            imgDiv.style.left = (width - 1200) / 2 + "px";
            imgDiv.style.top = "10px";
            imgDiv.style.display = "block";
        }
        $(document).ready(function () {
        $(".getpdfdoc").click(function (event) {
            event.preventDefault();
            var urlpdf = $(this).attr("path");
           
            getpdfdocument(urlpdf);
            return false;
        });
        });
    function js(url) {



        return false;
    }

    
    function HideDiv() {
        var bcgDiv = document.getElementById("divBackground");
        var imgDiv = document.getElementById("divImage");
        var imgFull = document.getElementById("imgFull");
        imgDiv.style.display = "none";

        }
        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && (keyCode != 32))

                return false;
            return true;
        }
    </script>
    <script language="javascript" type="text/javascript">
        function ValidateVadiDetail() {          
             var txtNamePerAadhaar = document.getElementById('<%=txtNamePerAadhaar.ClientID%>');
             if (txtNamePerAadhaar != null) {
                 if (txtNamePerAadhaar.value.trim() == "") {
                     alert("कृपया वादी का नाम अंकित करें...!");
                     txtNamePerAadhaar.focus();
                     return false;
                 }
             }

             var ddlgender = document.getElementById('<%=ddlgender.ClientID%>');
             if (ddlgender != null) {
                 if (ddlgender.selectedIndex == 0) {
                     alert("कृपया लिंग चुनें...!");
                     ddlgender.focus();
                     return false;
                 }
             }
          
             var dept = document.getElementById('<%=ddl_is_vadi_from_an_dept.ClientID%>');
             if (dept.selectedIndex == 0 || dept.selectedIndex == 1) {
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
                var txtWsanstha_naam = document.getElementById('<%=txtWsanstha_naam.ClientID%>');
                var txtWsanstha_padanaam = document.getElementById('<%=txtWsanstha_padanaam.ClientID%>');

                 if (wp.selectedIndex == 0) {
                     alert("कृपया संस्था का प्रकार चुनें...!");
                     wp.focus();
                     return false;
                 }
                 if (txtWsanstha_naam.value.trim() == "") {
                     alert("कृपया संस्था का नाम अंकित करें...!");
                     txtWsanstha_naam.focus();
                     return false;
                 }
                 if (txtWsanstha_padanaam.value.trim() == "") {
                     alert("कृपया संस्था में पदनाम अंकित करें...!");
                     txtWsanstha_padanaam.focus();
                     return false;
                 }
             }

             var txtFName = document.getElementById('<%=txtFName.ClientID%>');
                if (txtFName.value.trim() == "")
                {
                    alert("कृपया पिता/ पति का नाम अंकित करें...!");
                    txtFName.focus();
                    return false;
                }

                
                var ddlUserDist = document.getElementById('<%=ddlUserDist.ClientID%>');
                if (ddlUserDist.selectedIndex == 0) {
                    alert("कृपया जिला चुनें...!");
                    ddlUserDist.focus();
                    return false;
                }
                var ddlUserSubdivision = document.getElementById('<%=ddlUserSubdivision.ClientID%>');
                if (ddlUserSubdivision.selectedIndex==0) {
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
                    alert("कृपया पंचायत चुनें...!");
                    ddlUserPanchyat.focus();
                    return false;
            }


            //priya31082022
            if (ddlUserAreatype.selectedIndex == 2) {

                var ddlUserWard = document.getElementById('<%=ddlUserWard.ClientID%>');
                if (ddlUserWard.selectedIndex == 0) {
                    alert("कृपया वार्ड चुनें...!");
                   
                    ddlUserWard.focus();
                    return false;
                }
            }

               var ddlUserVillage = document.getElementById('<%=ddlUserVillage.ClientID%>');
                if (ddlUserVillage != null) {
                    if (ddlUserVillage.selectedIndex == 0) {
                        alert("कृपया राजस्व ग्राम चुनें...!");
                        ddlUserVillage.focus();
                        return false;
                    }
                }

                var txtUserVillage_Anya = document.getElementById('<%=txtUserVillage_Anya.ClientID%>');
                if (txtUserVillage_Anya != null) {
                        if (txtUserVillage_Anya.value.trim() == "") {
                            alert("कृपया अन्य ग्राम अंकित करें...!");
                            txtUserVillage_Anya.focus();
                            return false;
                        }
                    }

               var txtUserWard_Anya = document.getElementById('<%=txtUserWard_Anya.ClientID%>');
                if (txtUserWard_Anya != null) {
                        if (txtUserWard_Anya.value.trim() == "") {
                            alert("कृपया अन्य वार्ड अंकित करें...!");
                            txtUserWard_Anya.focus();
                            return false;
                        }
                    }

             var txtvadimobile = document.getElementById('<%=txtvadimobile.ClientID%>');
             if (txtvadimobile.value.trim() == "") {
                 alert("कृपया मोबाइल संख्या अंकित करें...!");
                 txtvadimobile.focus();
                 return false;
             }

            if ((txtvadimobile.value.trim()).length != 10) {
                alert("Please Enter valid mobile no...!");
                txtvadimobile.focus();
                return false;
            }

             return true;
        }

        function ValidatePratiVadiDetail() {
            //debugger;

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
                   if (txtPsanstha_padanaam.value.trim() == "") {
                       alert("कृपया संस्था में पदनाम अंकित करें...!");
                       txtPsanstha_padanaam.focus();
                       return false;
                   }
               }

               var txtPName = document.getElementById('<%=txtPName.ClientID%>');
               if (txtPName.value.trim() == "") {
                   alert("कृपया प्रतिवादी का नाम अंकित करें...!");
                   txtPName.focus();
                   return false;
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

        function ValidateBhumiKaVivaran() {

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

       
        function isNumber(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31
                && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        var validFiles = ["pdf", "PDF"]; //["bmp", "gif", "png", "jpg", "jpeg"];

        function ValidateVadiEvidenceDetail() {

            var ddlVadiEvidenceType = document.getElementById("<%=ddlVadiEvidenceType.ClientID%>");
            var txtVadiEvidenceType = document.getElementById("<%=txtVadiEvidenceType.ClientID%>");

            if (ddlVadiEvidenceType.selectedIndex == 0) {
                alert("कृपया साक्ष्य का प्रकार चुनें...!");
                ddlVadiEvidenceType.focus();
                return false;
            }

            if (txtVadiEvidenceType != null) {
                if (txtVadiEvidenceType.value.trim() == '') {
                    alert("कृपया अन्य साक्ष्य का प्रकार अंकित करें...!");
                    txtVadiEvidenceType.focus();
                    return false;
                }
            }

            var obj1 = document.getElementById("<%=file_vadi_dastavej_new.ClientID%>");
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

        function ValidatePrativadiEvidenceDetail() {

            debugger;

            var ddlPrativadiEvidenceType = document.getElementById("<%=ddlPrativadiEvidenceType.ClientID%>");
            var txtPrativadiEvidenceType = document.getElementById("<%=txtPrativadiEvidenceType.ClientID%>");

            if (ddlPrativadiEvidenceType.selectedIndex == 0) {
                alert("कृपया साक्ष्य का प्रकार चुनें...!");
                ddlPrativadiEvidenceType.focus();
                return false;
            }

            if (txtPrativadiEvidenceType != null) {
                if (txtPrativadiEvidenceType.value.trim() == '') {
                    alert("कृपया अन्य साक्ष्य का प्रकार अंकित करें...!");
                    txtPrativadiEvidenceType.focus();
                    return false;
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
        //priya31082022
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


        function PressOnlyNumber(event) {
            var key = window.event ? event.keyCode : event.which;

            if (event.keyCode == 8 || event.keyCode == 46
                || event.keyCode == 37 || event.keyCode == 39) {
                return true;
            }
            else if (key < 48 || key > 57) {
                return false;
            }
            else return true;
        };


    </script>
     <script type="text/javascript">
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
     </script>
     <script type="text/javascript">
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
     </script>

    <style type="text/css">
        .ActiveClass
        {
            background-color: green; color: white; padding: 10px; text-align: center; line-height: 1.5em; border-radius: 10px;
        }
        .RemoveClass{
            background-color: lightgray; color: white; padding: 10px; text-align: center; line-height: 1.5em; border-radius: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>




    

    <div class="container">             
          <div class="row">
                    <div class="col-md-12 text-center">
                        <h4>
                            आवेदन का विवरण</h4>
                    </div>
                </div>

          <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-3">
                        <h6 class="ActiveClass" id="hstep1" runat="server">वादी और भूमि विवाद
                            <br />
                            की विवरणी&nbsp;
                 <i class="fa fa-arrow-right" style="color: white"  aria-hidden="true"></i></h6>
                    </div>
                    <div class="col-md-3">
                        <h6 class='RemoveClass' id="hstep2" runat="server">प्रतिवादी और अन्य
                            <br />
                            विवरणी&nbsp;
                 <i class="fa fa-arrow-right" style="color: white" ></i></h6>
                    </div>
                    <div class="col-md-3">
                        <h6 class='RemoveClass' id="hstep3" runat="server">खाता-खेसरा
                            <br />
                            की विवरणी&nbsp;
                 <i class="fa fa-arrow-right" style="color: white"></i></h6>
                    </div>
                    <div class="col-md-3">
                        <h6 class='RemoveClass' id="hstep4" runat="server">वादी और प्रतिवादी द्वारा
                            <br />
                            साक्छ्य की विवरणी&nbsp;
                 <i class="fa fa-arrow-right" style="color: white"></i></h6>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <h6 class='RemoveClass' id="hstep5" runat="server">प्रस्तुत साक्छ्य
                            <br />
                            की विवरणी&nbsp;
                 <i class="fa fa-arrow-right" style="color: white"></i></h6>
                    </div>
                    <div class="col-md-3">
                        <h6 class='RemoveClass' id="hstep6" runat="server">घटना-वारदात और
                            <br />
                            न्यायलय की विवरणी&nbsp;
                 <i class="fa fa-arrow-right" style="color: white"></i></h6>
                    </div>
                    <div class="col-md-3">
                        <h6 class='RemoveClass' id="hstep7" runat="server">अंचलाधिकारी और थानाध्यक्ष
                            <br />
                            की बैठक&nbsp;
                 <i class="fa fa-arrow-right" style="color: white"></i></h6>
                    </div>
                    <%--<div class="col-md-3">
                        <h6 class='RemoveClass' id="hstep8" runat="server">आवेदन की प्रक्रिया
                            <br />
                            पूरी हुई, धन्यवाद</h6>
                    </div>--%>
                </div>
            </div>
        </div>
          <div class="row">
                    <center>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </center>
                </div>    

          <div id="step1" runat="server" visible="true">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                        वादी का विवरण</div>
                       <div class="panel-body">
                          <asp:UpdatePanel runat="server" ID="pnlupdate1">
                                         <ContentTemplate>                                                                   
                        <div class="row">                            
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                वादी का नाम <img src="../images/imp.gif" width="13px" />
                                <asp:TextBox ID="txtNamePerAadhaar" runat="server" MaxLength="100" CssClass="form-control"
                                    oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                    onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                                <asp:Label ID="DtxtNamePerAadhaar" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                            </div>
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                लिंग चुने<img src="../images/imp.gif" width="13px" alt="" /> 
                                <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control" style="height:30px">
                                    <asp:ListItem Text="--Select--" Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Text="MALE" Value="M">MALE</asp:ListItem>
                                    <asp:ListItem Text="FMALE" Value="F">FEMALE</asp:ListItem>
                                    <asp:ListItem Text="OTHER" Value="O">OTHER</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Dddlgender" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                            </div>
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                जन्म का वर्ष
                                <asp:DropDownList ID="ddlYear" runat="server" InitialValue="0" CssClass="form-control" style="height:30px"
                                    Enabled="true">
                                </asp:DropDownList>
                                <asp:Label ID="Dtxtdatebirth" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                            </div>
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                              पिता/ पति का नाम<img  src="../images/imp.gif" width="13" alt="" />
                                <asp:TextBox ID="txtFName" runat="server" MaxLength="50" CssClass="form-control"
                                    oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                    onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                            </div>

                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                जिला<img src="../images/imp.gif" width="13px" />
                                <asp:DropDownList ID="ddlUserDist" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlUserDist_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                अनुमंडल<img src="../images/imp.gif" width="13px" />
                                <asp:DropDownList ID="ddlUserSubdivision" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlUserSubdivision_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                अंचल<img src="../images/imp.gif" width="13px" />
                                <asp:DropDownList ID="ddlUserBlock" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlUserBlock_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>                                                
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                थाना<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:DropDownList ID="ddlUserThana" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                क्षेत्र का प्रकार<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:DropDownList runat="server" ID="ddlUserAreatype" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlUserAreatype_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="R"> Rural</asp:ListItem>
                                    <asp:ListItem Value="U"> Urban</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                <div class="row">
                                    <div id="divUserPanchyat" runat="server" class="col-md-12">
                                <asp:Label ID="labUVillage" runat="server" Text="ग्राम पंचायत"></asp:Label><img src="../images/imp.gif" width="13px" alt=""/>
                                <asp:DropDownList ID="ddlUserPanchyat" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlUserPanchyat_SelectedIndexChanged">
                                </asp:DropDownList>
                                        </div>
                                    
                                    </div>
                            </div>
                             <div id="divUserPanchyat_Anya" runat="server" class="col-md-3" visible="false" style="font-weight: bolder;padding-top:5px;">
                              पंचायत(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtUserPanchyat_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                             <div class="col-md-3" id="divUserVillageCol" runat="server" style="font-weight: bolder;padding-top:5px;">                                
                                   <div class="row">
                                       <div id="divUserVillage" runat="server" class="col-md-12">
                                राजस्व ग्राम<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:DropDownList ID="ddlUserVillage" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlUserVillage_SelectedIndexChanged" >
                                </asp:DropDownList>
                                   </div> 
                                
                                        </div>                                                                         
                            </div>                            
                                                                                 
                             <div id="divUserVillage_Anya" runat="server" class="col-md-3" visible="false" style="font-weight: bolder;padding-top:5px;">
                              ग्राम(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtUserVillage_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                <div class="row">
                                    <div id="divUserWard" runat="server" class="col-md-12">
                                         वार्ड
                                        <span id="UWard" runat="server" visible="true">
                                          <img alt="" src="../images/imp.gif" width="13px" />
                                        </span>                                
                                <asp:DropDownList ID="ddlUserWard" runat="server" CssClass="form-control" Enabled="true" style="height:30px" AutoPostBack="True" OnSelectedIndexChanged="ddlUserWard_SelectedIndexChanged" >
                                </asp:DropDownList>
                                        </div>
                                    
                                    </div>
                               
                            </div>
                             <div id="divUserWard_Anya" runat="server" class="col-md-3" visible="false" style="font-weight: bolder;padding-top:5px;">
                              वार्ड(अगर अन्य है)
                                <asp:TextBox runat="server" ID="txtUserWard_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                             <div id="divUserMohalla" runat="server" class="col-md-3" visible="false" style="font-weight: bolder;padding-top:5px;">
                              मोहल्ला 
                                <asp:TextBox runat="server" ID="txtUserMohalla" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                मोबाइल संख्या<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:TextBox runat="server" ID="txtvadimobile" MaxLength="10" CssClass="form-control" onkeypress="return PressOnlyNumber(event)"></asp:TextBox>
                            </div>                                                                                  
                        </div>

                        <div class="row" style="padding-top:10px;">
                            <div class="col-md-12" style="padding-top:10px; border: 1px solid #ffca99; background-color:#fff2e6; padding-bottom: 10px;">
                                <div class="col-md-3">                                
                               <br />
                               क्या वादी किसी विभाग का प्रतिनिधि है<img src="../images/imp.gif" width="13px" alt="" />
                                </div>
                                <div class="col-md-2">
                                     <br />
                                <asp:DropDownList runat="server" ID="ddl_is_vadi_from_an_dept" CssClass="form-control" style="height:30px" 
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_is_vadi_from_an_dept_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                            </div>  
                                <div class="col-md-7">
                                <div class="row" runat="server" id="divWVibhag_details" visible="false">                                
                            <div class="col-md-6">
                               विभाग का नाम<img src="../images/imp.gif" width="13px" alt="" />
                               
                                    <asp:DropDownList ID="ddlWvibhaag_naam" runat="server" CssClass="form-control" style="height:30px" ></asp:DropDownList>
                            </div> 
                             <div class="col-md-6">
                              विभाग में पदनाम 
                                 
                                <asp:TextBox ID="txtWvibhaag_padanaam" runat="server" MaxLength="100" CssClass="form-control" AutoComplete="off"
                                    onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                            </div> 
                                    </div>  
                            </div>
                            </div>
                        </div>

                        <div class="row" style="padding-top:10px;">
                            <div class="col-md-12" style="padding-top:10px; border: 1px solid #c0d8d8; background-color:#eff5f5; padding-bottom: 10px;  padding-left:0px;">
                            <div class="col-md-3">                                
                               <br />
                               क्या वादी किसी संस्था का प्रतिनिधि है<img src="../images/imp.gif" width="13px" alt="" />
                                </div>
                                <div class="col-md-2">
                                    <br />
                                <asp:DropDownList runat="server" ID="ddl_is_vadi_from_an_org" CssClass="form-control" style="height:30px" AutoPostBack="true" OnSelectedIndexChanged="ddl_is_vadi_from_an_org_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                            </div> 
                            <div class="col-md-7">
                                <div class="row" runat="server" id="divWSanstha_details" visible="false"> 
                                            <div class="col-md-3">
                                      संस्था का प्रकार<img src="../images/imp.gif" width="11px" alt="" />                                        
                                        <asp:DropDownList ID="ddlWsanstha_naam" runat="server"  CssClass="form-control" style="height:30px;"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlWsanstha_naam_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                            <div class="col-md-3">
                                      संस्था का सम्बन्ध                                      
                                        <asp:DropDownList ID="ddlWsanshaanya_naam" runat="server"  CssClass="form-control" style="height:30px;"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlWsanshaanya_naam_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                            <div class="col-md-3">
                                      संस्था का नाम<img src="../images/imp.gif" width="11px" alt="" />                               
                                        <asp:TextBox ID="txtWsanstha_naam" runat="server" MaxLength="100" CssClass="form-control" AutoComplete="off"
                                            onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                                    </div>
                                            <div class="col-md-3">
                                      संस्था में पदनाम<img src="../images/imp.gif" width="11px" alt="" />                                
                                        <asp:TextBox ID="txtWsanstha_padanaam" runat="server" MaxLength="100" CssClass="form-control" AutoComplete="off"
                                            onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                                    </div>  
                                    </div>   
                                </div>                       
                                </div>
                        </div>

                           </ContentTemplate>
                    </asp:UpdatePanel>

                          <div class="row" style="padding-top:2%;">
                                        <div class="col-md-5">
                                            </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAddVadiDetail" CssClass="btn btn-success" runat="server" OnClientClick="return ValidateVadiDetail();" Text="Save" OnClick="btnAddVadiDetail_Click"  />
                                        </div>
                                        
                                       
                                         
                                    </div>                                            
                        </div>                                                             
                       <div class="row">
                            <div class="col-md-12" style="text-align:center">
                                
                                <asp:GridView runat="server" Width="100%" ID="wadi_grid" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid fontsize" OnRowCommand="wadi_grid_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>                                        
                                        <asp:BoundField DataField="NameAsPerAadhaar" HeaderText="वादी का नाम" ItemStyle-Width="100" ControlStyle-Font-Size="Smaller" />

                                         <asp:TemplateField HeaderText="संस्था का प्रतिनिधि" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrgpratinidhi"  runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="विभाग का प्रतिनिधि" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeppratinidhi"  runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_dept")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="लिंग" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGender"  runat="server" Text='<%# (Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ) ? "Female" : "Male"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                   
                                        
                                        <asp:BoundField DataField="YearOfBirthAsPerAadhaar" HeaderText="उम्र" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="Vadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" ItemStyle-Width="120" />
                                        
                                        <asp:TemplateField HeaderText="विभाग/संस्था का नाम" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrgName"  runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? Eval("vadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPadName" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y") ? Eval("vadi_org_pad_name") : Eval("vadi_dept_pad_name")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="dist" HeaderText="जिला" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="block" HeaderText="अंचल" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="thana" HeaderText="थाना" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" ItemStyle-Width="100" />
                                         <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="WardNo" HeaderText="वार्ड" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल संख्या" ItemStyle-Width="80" />                                       
                                        
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove" 
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                                                                                        
                                    </Columns>
                                </asp:GridView>
                                  
                            </div>
                        </div>
                </div>
                <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                भूमि विवाद का विवरण</div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updatebhumi" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-3 labeltextsize">
                                जिला<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" Enabled="true" style="height:30px"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize">
                                अनुमंडल<img src="../images/imp.gif" width="13px" />
                                <asp:DropDownList ID="ddlSubdivision" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlSubdivision_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize">
                                अंचल<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" Enabled="true" style="height:30px" 
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize">
                                थाना<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:DropDownList ID="ddlPolice" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>                            
                        </div>
                       <div class="row">
                            <div class="col-md-3 labeltextsize" >
                                क्षेत्र का प्रकार<img src="../images/imp.gif" width="13px" alt=""/>
                                <asp:DropDownList runat="server" ID="ddlareatype" CssClass="form-control"  style="height:30px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlareatype_SelectedIndexChanged" >
                                     <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="R"> Rural</asp:ListItem>
                                    <asp:ListItem Value="U"> Urban</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div id="divPanchyat" runat="server" class="col-md-3 labeltextsize">
                                <asp:Label ID="labVillage" runat="server" Text="ग्राम पंचायत"></asp:Label>
                                <img src="../images/imp.gif" width="13px" alt="" />
                                <asp:DropDownList ID="ddlPanchyat" runat="server" CssClass="form-control" style="height:30px"
                                    AutoPostBack="true"  OnSelectedIndexChanged="ddlPanchyat_SelectedIndexChanged">
                                </asp:DropDownList>
                             </div>
                             <div id="divPanchyat_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                              पंचायत(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtPanchyat_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>                         
                            <div class="col-md-3 labeltextsize" id="divVillage" runat="server">
                                राजस्व ग्राम<img src="../images/imp.gif" width="13px" alt=""/>
                                <asp:DropDownList ID="ddlVillage" runat="server" CssClass="form-control" style="height:30px"
                                     AutoPostBack="True" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize" id="divVillage_Anya" runat="server"  visible="false">
                                ग्राम(अगर अन्य है)<img src="../images/imp.gif" width="13px" alt=""/>
                                <asp:TextBox runat="server" ID="txtVillage_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div id="divWard" runat="server" class="col-md-3 labeltextsize">
                                वार्ड<img src="../images/imp.gif" runat="server" id="imgWard_bhumiVivaran" width="13"  alt="" />
                                <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                     AutoPostBack="True" OnSelectedIndexChanged="ddlWard_SelectedIndexChanged">
                                </asp:DropDownList>
                                    </div>
                            <div id="divWard_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                              वार्ड(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtWard_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>                                                                                             
                            <div class="col-md-3 labeltextsize" style="display:none;">
                                मौजा<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:TextBox onkeypress="return ValidateAlpha(event)" runat="server" MaxLength="100" ID="txtmauja"
                                    CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                विवाद का अद्यतन कारक<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:DropDownList AutoPostBack="true" runat="server" ID="ddl_vivad_adyatan_sthiti" CssClass="form-control" style="height:30px">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize">
                                राजस्व थाना संख्या
                                <asp:TextBox runat="server" ID="txtrajaswa_sankhya" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3 labeltextsize">
                                भूमि का प्रकार<img src="../images/imp.gif" width="13px" />
                                <asp:DropDownList ID="ddlbhumitype" AutoPostBack="true" runat="server" CssClass="form-control" style="height:30px"
                                    Enabled="true" OnSelectedIndexChanged="ddlbhumitype_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize" id="divSarkaribhumitype" runat="server">
                               <asp:Label ID="labSarkaribhumitype" runat="server" Text="सरकारी भूमि का प्रकार"></asp:Label><img runat="server" alt="" id="ImgSarkaribhumitype" src="../images/imp.gif" width="13" />
                                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlsarkaribhumitype_SelectedIndexChanged" CssClass="form-control" ID="ddlsarkaribhumitype" style="height:30px"
                                    Enabled="true" >
                                </asp:DropDownList>
                            </div>                                                    
                            <div class="col-md-3 labeltextsize" id="divsarkaribhumitype_Anya" runat="server">
                             सरकारी भूमि का प्रकार (अगर अन्य है)<img  src="../images/imp.gif" width="13" />
                                <asp:TextBox runat="server" ID="txtsarkaribhumitype_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3 labeltextsize">
                                भूमि विवाद का प्रकार<img src="../images/imp.gif" width="13px" />
                                <asp:DropDownList AutoPostBack="true"  OnSelectedIndexChanged="ddlbhumivivadtype_SelectedIndexChanged" runat="server" ID="ddlbhumivivadtype" CssClass="form-control" style="height:30px">
                                    
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize" id="divBhumivivad_Anya" runat="server">
                             भूमि विवाद का प्रकार (अगर अन्य है)<img  src="../images/imp.gif" width="13" />
                                <asp:TextBox runat="server" ID="txtbhumivivad_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3 labeltextsize">
                              <asp:Label ID="Label1" runat="server" Text="आवेदन की तिथि"></asp:Label><img id="img1" runat="server" src="../images/imp.gif" width="13" />
                                <asp:TextBox runat="server" ID="txtAwadenKiTithi" MaxLength="100" CssClass="form-control" onkeypress="return dateValidate(event)"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txtAwadenKiTithi" Format="dd-MM-yyyy" CssClass="zindex">
                                                    </cc1:CalendarExtender>
                            </div>                                           
                        </div>
                        <div class="row">
                             <div class="col-md-9 labeltextsize" > 
                                  <asp:Label ID="Label10" runat="server" Text="वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी "></asp:Label><img src="../images/imp.gif" width="13px" alt=""/>                                                                                                                       
                                        <asp:TextBox runat="server"  Height="60px" ID="txtVadiVivarani" MaxLength="500" TextMode="MultiLine"
                                        CssClass="form-control"></asp:TextBox>
                                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                                                                         
                              </div>
                              <div class="col-md-3 labeltextsize" > 
                                  <asp:Label ID="Label2" runat="server" Text="वादी द्वारा प्रस्तुत आवेदन  "></asp:Label> 
                                            <asp:FileUpload ID="AppDoc" runat="server" CssClass="form-control" accept=".pdf" />
                                            <asp:HiddenField ID="hdAppDoc" runat="server" />
                                            <span style="color: #FF3300; font-weight: bold; font-size:11px">
                                                <asp:Label ID="Label11" runat="server" Text="(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>                               
                                                <a href="#" id="lnkAppDoc" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>                                                                         
                                            </div>
                                 <div class="col-md-9 labeltextsize" > 
                                  <asp:Label ID="Label12" runat="server" Text="प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी "></asp:Label>
                                            <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" Height="60px" ID="txtPrativadiVivarani"
                                        CssClass="form-control"></asp:TextBox>
                                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                                                                         
                              </div>
                                 <div class="col-md-3 labeltextsize" > 
                                  <asp:Label ID="Label3" runat="server" Text="प्रतिवादी द्वारा प्रस्तुत आवेदन  "></asp:Label>
                                                                                 
                                        
                                            <asp:FileUpload ID="PrativadiDoc" runat="server" CssClass="form-control" accept=".pdf" />
                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                            <span style="color: #FF3300; font-weight: bold;font-size:11px">
                                                <asp:Label ID="Label4" runat="server" Text="(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                                             <a href="#" id="lnkPrativadiDoc" runat="server" path="display"  class="getpdfdoc" visible="false">View Document</a>
                                                                         
                              </div>
                                </div>
                        <br />
                        
                                              
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

                  

        </div>

            </div>

          <div id="step2" runat="server" visible="false">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                        प्रतिवादी का विवरण</div>
                    <div class="panel-body">
                         <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                         <ContentTemplate>
                                             <div class="row">
                                                
                                                                                    
                            <div class="col-md-3 labeltextsize" >
                               प्रतिवादी का नाम<img src="../images/imp.gif" width="13px" alt="" />
                                <asp:TextBox ID="txtPName" runat="server" MaxLength="50" CssClass="form-control"
                                    oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                    onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                            </div>
                            <div class="col-md-3 labeltextsize">
                              पिता/ पति का नाम
                                <asp:TextBox ID="txtPFName" runat="server" MaxLength="50" CssClass="form-control"
                                    oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                    onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                            </div>
                                                                                             

                                
                            <div class="col-md-3 labeltextsize">
                                जिला
                                <asp:DropDownList ID="ddlPDistrict" runat="server" CssClass="form-control" style="height:30px"
                                    AutoPostBack="true"  Enabled="true">
                                </asp:DropDownList><%--OnSelectedIndexChanged="ddlPDistrict_SelectedIndexChanged" --%>
                            </div>
                      
                       
                            <div class="col-md-3 labeltextsize">
                                अनुमंडल
                                <asp:DropDownList ID="ddlPSubdivision" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPSubdivision_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize">
                                अंचल
                                <asp:DropDownList ID="ddlPBlock" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPBlock_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                             <div class="col-md-3 labeltextsize">
                                थाना
                                <asp:DropDownList ID="ddlPThana" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize">
                                क्षेत्र का प्रकार
                                <asp:DropDownList runat="server" ID="ddlPAreatype" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlPAreatype_SelectedIndexChanged">
                                     <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="R"> Rural</asp:ListItem>
                                    <asp:ListItem Value="U"> Urban</asp:ListItem>
                                </asp:DropDownList>
                            </div>                            
                       
                     

                              <div id="divPPanchyat" runat="server" class="col-md-3 labeltextsize" style="padding-bottom:5px;">
                              <asp:Label ID="labPVillage" runat="server" Text ="ग्राम पंचायत"></asp:Label>
                                <asp:DropDownList ID="ddlPPanchyat" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlPPanchyat_SelectedIndexChanged">
                                </asp:DropDownList>
                                </div>

                                 <div id="divPPanchyat_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                              पंचायत(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtPPanchyat_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>


                           



                            <div  id="divPVillageCol" runat="server" class="col-md-3">
                            <div id="divPVillage" runat="server" class="labeltextsize">
                                राजस्व ग्राम
                                <asp:DropDownList ID="ddlPVillage" runat="server" CssClass="form-control" style="height:30px"
                                     AutoPostBack="True" OnSelectedIndexChanged="ddlPVillage_SelectedIndexChanged">
                                </asp:DropDownList>
                                    </div>


                            
                           </div>
                            <div id="divPVillage_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                                ग्राम(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtPVillage_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div id="divPWard" runat="server" class="col-md-3 labeltextsize">
                                वार्ड
                                <asp:DropDownList ID="ddlPWard" runat="server" CssClass="form-control" Enabled="true" style="height:30px"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlPWard_SelectedIndexChanged">
                                </asp:DropDownList>
                                    </div>


                                <div id="divPWard_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                                 वार्ड(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtPWard_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                         </div>
                           

                            <div id="divPMohalla" runat="server" class="col-md-3 labeltextsize" visible="false">
                              मोहल्ला 
                                <asp:TextBox runat="server" ID="txtPMohalla" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                    </div>

                            <div class="col-md-3 labeltextsize">
                                मोबाइल संख्या
                                <asp:TextBox runat="server" CssClass="form-control" MaxLength="15" ID="txtprativadi_Mobile"></asp:TextBox>
                            </div>
                        


 </div>


                                            <div class="row" style="padding-top:10px;">
                                             <div class="col-md-12 labeltextsize" style="padding-top:10px; border: 1px solid #ffca99; background-color:#fff2e6; padding-bottom: 10px;">
                            <div class="col-md-3">                                
                               <br />
                               क्या प्रतिवादी किसी विभाग का प्रतिनिधि है<img src="../images/imp.gif" width="13px" alt="" />
                                </div>
                                <div class="col-md-2">
                                     <br />
                                <asp:DropDownList runat="server" ID="ddl_is_pratiVadi_from_an_dept" CssClass="form-control" style="height:30px" 
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_is_pratiVadi_from_an_dept_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                            </div>  
                            <div class="col-md-7">
                                <div class="row" runat="server" id="divPVibhag_details" visible="false">                                
                                                <div class="col-md-6">
                               विभाग का नाम<img src="../images/imp.gif" width="13px" alt="" />
                              
                                <asp:DropDownList ID="ddlPvibhaag_naam" runat="server" CssClass="form-control" style="height:30px" ></asp:DropDownList>
                            </div> 
                                                 <div class="col-md-6">
                               विभाग में पदनाम 
                                 
                                <asp:TextBox ID="txtPvibhaag_padanaam" runat="server" MaxLength="50" CssClass="form-control"
                                  AutoComplete="off"
                                    onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                            </div> 
                                    </div>  
                            </div>
                                </div>
                                                </div>
                                             
                                            <div class="row" style="padding-top:10px;">
                                            
                            <div class="col-md-12 labeltextsize" style="padding-top:10px; border: 1px solid #c0d8d8; background-color:#eff5f5; padding-bottom: 10px;">
                            <div class="col-md-3">                                
                               <br />
                               क्या प्रतिवादी किसी संस्था का प्रतिनिधि है<img src="../images/imp.gif" width="13px" alt="" />
                                </div>
                                <div class="col-md-2">
                                    <br />
                                <asp:DropDownList runat="server" ID="ddl_is_pratiVadi_from_an_org" CssClass="form-control" style="height:30px" 
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_is_pratiVadi_from_an_org_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                            </div> 
                            <div class="col-md-7">
                                <div class="row" runat="server" id="divPSanstha_details" visible="false"> 
                                    <div class="col-md-3">
                              संस्था का प्रकार<img src="../images/imp.gif" width="13px" alt="" />
                                        
                                <asp:DropDownList ID="ddlPsanstha_naam" runat="server"  CssClass="form-control" style="height:30px;"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlWsanstha_naam_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                             <div class="col-md-3">
                              संस्था का सम्बन्ध                                      
                                <asp:DropDownList ID="ddlPsanshaanya_naam" runat="server"  CssClass="form-control" style="height:30px;"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlPsanshaanya_naam_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                                    <div class="col-md-3">
                              संस्था का नाम<img src="../images/imp.gif" width="13px" alt="" />
                                
                                <asp:TextBox ID="txtPsanstha_naam" runat="server" MaxLength="100" CssClass="form-control"
                                     AutoComplete="off"
                                    onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                            </div>

                            <div class="col-md-3">
                               संस्था में पदनाम<img src="../images/imp.gif" width="13px" alt="" />
                                
                                <asp:TextBox ID="txtPsanstha_padanaam" runat="server" MaxLength="100" CssClass="form-control"
                                     AutoComplete="off"
                                    onkeypress="return ValidateAlpha(event)" onkeyup="upper(this)"></asp:TextBox>
                            </div>  
                                    </div>   
                                </div>                       
                                </div>
                        </div>
                                                
                                              </ContentTemplate>
                                            </asp:UpdatePanel>


                        



                          <div class="row" style="padding-top:2%;">
                                        <div class="col-md-5">
                                            </div>
                                        <div class="col-md-2">
                                             <asp:Button ID="btnAddPratiVadiDetail" CssClass="btn btn-success btnAddPratiVadiDetail" OnClientClick="return ValidatePratiVadiDetail();" runat="server" OnClick="btnAddPratiVadi_Click"
                                                Text="Save" />
                                        </div>
                                        
                                       
                                         
                                    </div>   


                         </div> 

                    <div class="row">
                            <div class="col-md-12" style="text-align:center">
                                <asp:GridView runat="server" Width="100%" ID="pratiWadi_grid" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="pratiWadi_grid_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                        <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी / विभाग का नाम" ItemStyle-Width="100" />

                                        <asp:TemplateField HeaderText="संस्था का प्रतिनिधि" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lblis_pratiVadi_from_an_org" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y") ? "हां" : "नहीं"   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="विभाग का प्रतिनिधि" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblis_pratiVadi_from_an_dept" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_dept")) == "Y") ? "हां": "नहीं"   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        
                                        <asp:BoundField DataField="pratiVadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" ItemStyle-Width="120" />
                                        
                                        



                                        <asp:TemplateField HeaderText="विभाग/संस्था का नाम" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrgName"  runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ) ? Eval("pratiVadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPadName" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y") ? Eval("pratiVadi_org_pad_name") : Eval("pratiVadi_dept_pad_name")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="dist" HeaderText="जिला" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="block" HeaderText="अंचल" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="thana" HeaderText="थाना" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" ItemStyle-Width="100" />
                                         <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="WardNo" HeaderText="वार्ड" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="pratiVadi_MobileNo" HeaderText="मोबाइल संख्या" ItemStyle-Width="80" />

                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                               
                                           
                                                <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove" 
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                                                                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>


                    </div>
             
         <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
               अन्य विवरण</div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="UPanel4" UpdateMode="Conditional">
                    <ContentTemplate>
                
                        <div class="row" style="padding-top: 10px">
                            <div class="col-md-5">
                                प्रतिवादी को सूचित किया गया है या नहीं ?<img src="../images/imp.gif" width="13px" />
                                </div>
                    <div class="col-md-2">
                        
                        <asp:DropDownList runat="server" ID="ddlwadi_pratiwadi_sunwai" CssClass="form-control" style="height:30px" 
                            AutoPostBack="true" OnSelectedIndexChanged="ddlwadi_pratiwadi_sunwai_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                    </div>
                            <div class="col-md-1" >
                                <asp:Label ID="labNotice" runat="server" Text=""></asp:Label>
                                </div>
                     <div class="col-md-3">
                        
                        
                            <asp:DropDownList runat="server" ID="ddlKiskeduwara_bhejagaya" CssClass="form-control" style="height:30px" Visible="false">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Telephone">दूरभाष के माध्यम से</asp:ListItem>
                                                <asp:ListItem Value="Watchman">चौकीदार के माध्यम से</asp:ListItem>
                                                <asp:ListItem Value="Watchman">पत्र के माध्यम से</asp:ListItem>
                                                <asp:ListItem Value="Other">अन्य</asp:ListItem>
                                            </asp:DropDownList>
                            
                        <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" Height="60px"  ID="txtsunwaiHetuNoticKaKaran" Visible="false"
                            CssClass="form-control"></asp:TextBox>
                         <div id="div_sunwaiHetuNoticKaKaran" runat="server" style="text-align:right" visible="false"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                    </div>
                            <div class="col-md-1">
                                </div>
                            </div>
                        <div class="row" id="divSuchana_ka_tamila" runat="server" style="padding-top: 10px" visible="false">
                            <div class="col-md-5">
                               प्रतिवादी को सूचना का तामिला प्राप्त है या नहीं ?<img src="../images/imp.gif" width="13px" />
                                </div>
                            <div class="col-md-2">
                        
                        <asp:DropDownList runat="server" ID="ddlSuchana_ka_tamila" CssClass="form-control" style="height:30px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSuchana_ka_tamila_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                    </div>
                            <div class="col-md-5">
                           
                        </div>
                            </div>

                        <div class="row" id="divSuchana_ka_upasthiti"  visible="false" runat="server" style="padding-top:10px" ">

                                 <div class="col-md-5">
                               प्रतिवादी उपस्थित हुआ है या नहीं ?<img src="../images/imp.gif" width="13px" />
                                </div>
                            <div class="col-md-2">
                        
                        <asp:DropDownList runat="server" ID="ddlSuchana_ka_upasthiti" CssClass="form-control" style="height:30px">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                         </div>
                            <div class="col-md-5">
                                </div>
                        </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
            </div>
         
         </div>

          <div id="step3" runat="server" visible="false">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                        भूमि का खाता खेसरा का विवरण भरे </div>
                    <div class="panel-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                         <ContentTemplate>                                                                    
                                    <div class="row" style="padding:1.5% 2.5% 1% 2.5%;">
                             <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                 खाता संख्या <img src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtkhatasankhya" MaxLength="50" CssClass="form-control"  onkeypress="return isNumber(event)" ></asp:TextBox>
                            </div>
                         <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                  खेसरा संख्या<img src="../images/imp.gif" width="13px" />
                                <asp:TextBox runat="server" ID="txtkhesarasankhya" MaxLength="50" CssClass="form-control txtkhesarasankhya"  onkeypress="return isNumber(event)" ></asp:TextBox>
                            </div>                                     
                                    </div>


                            <div class="panel panel-primary">
                                        <div class="panel-heading" style="padding: 3px 20px">
                                            रकबा</div>
                                        <div class="panel-body">
                                                 <div class="row">

                                                     <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                                    क्षेत्रफल<img src="../images/imp.gif" width="13px" />
                                                        <asp:TextBox runat="server" ID="txtrakabasankhya" MaxLength="15"  onkeypress="return isNumber(event)" CssClass="form-control txtrakabasankhya"  ></asp:TextBox>
                                                         <br />
                                                        <asp:TextBox runat="server" ID="txtrakabasankhya1" MaxLength="15"  onkeypress="return isNumber(event)"  CssClass="form-control txtrakabasankhya"></asp:TextBox>
                                                         <br />
                                                         <asp:TextBox runat="server" ID="txtrakabasankhya2" MaxLength="15"  onkeypress="return isNumber(event)"  CssClass="form-control txtrakabasankhya"></asp:TextBox>
                                                    </div> 



                                                     <div class="col-md-3" style="font-weight: bolder;padding-top:5px;">
                                                    यूनिट<img src="../images/imp.gif" width="13px" />

                                                          <asp:DropDownList runat="server" ID="ddlrakabasankhya" CssClass="form-control" style="height:30px"></asp:DropDownList>
                                                         <br />
                                                       
                                                        
                                                        <asp:DropDownList runat="server" ID="ddlrakabasankhya1" CssClass="form-control" style="height:30px"></asp:DropDownList>
                                                         <br />
                                                         <asp:DropDownList runat="server" ID="ddlrakabasankhya2" CssClass="form-control" style="height:30px"></asp:DropDownList>

                                                       <asp:DropDownList runat="server" ID="ddlBhumiUnit" CssClass="form-control"  style="display:none;">
                                                                <asp:ListItem Value="0">--यूनिट चुने--</asp:ListItem>
                                                                <asp:ListItem Value="Acre">एकड़</asp:ListItem>
                                                                <asp:ListItem Value="Decimal">डिसमिल</asp:ListItem>
                                                                <asp:ListItem Value="Bigha">बीघा</asp:ListItem>
                                                                <asp:ListItem Value="Katha">कट्ठा</asp:ListItem>
                                                                <asp:ListItem Value="Dhur">धुर</asp:ListItem>
                                                      </asp:DropDownList>                                                     
                                                    </div> 
                                                     <div class="col-md-12" style="font-weight: bolder;padding-top:5px;">
                                            <h6 style="font-weight: bolder;padding-top:5px;"><span style="color:#f00;">Note:-</span> सबसे बड़ी इकाई दर्ज करें फिर छोटी इकाई दर्ज करें फिर सबसे छोटी इकाई दर्ज करें अर्थात 0 हेक्टेयर, 0 एकड़ ,1.5 डेसिमल</h6>
                                           
                                                </div>
                                                 </div>       
                                            </div>
                                            </div>


                                   

                                    <div class="row" style="padding:0% 2.5% 1% 2.5%;">


                                         <div class="col-md-4" style="font-weight: bolder;padding-top:5px;">
                                                    खतियान में जमीन की किस्म का विवरण<img src="../images/imp.gif" width="13px" />
                                                        <asp:DropDownList runat="server" ID="ddlkhatiyan_me_jaminvivran" CssClass="form-control ddlkhatiyan_me_jaminvivran" style="height:30px">
                                            </asp:DropDownList>
                                                    </div> 
                                        <div class="col-md-8" style="font-weight: bolder;padding-top:5px;">
                                                    खतियान में जमीन का विवरण
                                                        <asp:TextBox runat="server" ID="txtkhatiyan_me_jaminvivran_text" MaxLength="500" TextMode="MultiLine" Height="60px" CssClass="form-control txtkhatiyan_me_jaminvivran_text"></asp:TextBox>
                                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                                                    </div> 
                                    </div>
                                   




                                                                <div class="panel panel-primary">
                                        <div class="panel-heading" style="padding: 3px 20px;">
                                            चौहद्दी का विवरण</div>
                                        <div class="panel-body">
                                                        <div class="row" style="padding:2% 2.5% 1% 2.5%;">
                                        <div class="col-md-6" style="font-weight: bolder;padding-top:5px;">
                                            उत्तर
                                            
                                            <asp:TextBox runat="server" MaxLength="100" ID="txtuttari_chohaddi"
                                                CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6" style="font-weight: bolder;padding-top:5px;">
                                            दक्षिण
                                            <asp:TextBox runat="server"  MaxLength="100" ID="txtdakshini_chohaddi"
                                                CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                                            </div>

                                             <div class="row" style="padding:2% 2.5% 1% 2.5%;">
                                        <div class="col-md-6" style="font-weight: bolder;padding-top:5px;">
                                            पूर्व
                                            <asp:TextBox runat="server"  MaxLength="100" ID="txtpurvi_chohaddi"
                                                CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="col-md-6" style="font-weight: bolder;padding-top:5px;">
                                            पश्चिम
                                            <asp:TextBox runat="server"  MaxLength="100" ID="txtpashchimi_chohaddi"
                                                CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                                 
                                    </div>
                                            </div>
                                            </div>

                                        </ContentTemplate>
                        </asp:UpdatePanel>


                        <div class="row">
                                        <div class="col-md-5">
                                            </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnsaveBhumiKaVivaran" OnClientClick="return ValidateBhumiKaVivaran();" OnClick="btnsaveBhumiKaVivaran_Click" CssClass="btn btn-success btnsaveBhumiKaVivaran"  runat="server"
                                                Text="Save" />
                                        </div>
                                        
                                       
                                         
                                    </div>                                            
                        </div>   

                    <div class="row">
                            <div class="col-md-12" style="text-align:center">
                                <asp:GridView runat="server" Width="100%" ID="grd_bhumivivad" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="grd_bhumivivad_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                           <asp:BoundField DataField="khataNo" HeaderText="खाता संख्या" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="khesraNo" HeaderText="खेसरा संख्या" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="Rakba" HeaderText="रकबा" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="LandTypesInKhatianDesc" HeaderText="जमीन की किस्म" ItemStyle-Width="120" />                                        
                                        <%--<asp:BoundField DataField="जमीन का विवरण" HeaderText="ख़तियन में जमीन का विवरण" ItemStyle-Width="220" />--%>
                                         <asp:TemplateField HeaderText="ख़तियन में जमीन का विवरण" ItemStyle-Width="220">
                                            <ItemTemplate>
                                                <span id='<%# Eval("LandDetailsInKhatian") %>' style="display: block;overflow: auto; height:25px;" >
                                                    <%# Eval("LandDetailsInKhatian") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="North_chauhaddee" HeaderText="उत्तर" ItemStyle-Width="100" />
                                         <asp:BoundField DataField="South_chauhaddee" HeaderText="दक्षिण" ItemStyle-Width="100" />
                                         <asp:BoundField DataField="East_chauhaddee" HeaderText="पूर्व" ItemStyle-Width="100" />
                                         <asp:BoundField DataField="West_chauhaddee" HeaderText="पश्चिम" ItemStyle-Width="100" />
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                
                                                 <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove" 
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        </div>
           </div>
         
          <div id="step4" runat="server" visible="false">
          <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                        वादी द्वारा प्रस्तुत साक्ष्य की विवरणी</div>
                    <div class="panel-body">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                         <ContentTemplate>
                        
                        <div class="row" style="padding:1.5% 2.5% 1% 2.5%;">
                              <div class="col-md-6 text-left">
                                    
                                    साक्ष्य का प्रकार<img src="../images/imp.gif" width="13px" />                                                                  
                                                                     
                              </div>
                              <div id="divddlVadiEvidenceType" runat="server" class="col-md-6">
                                  <asp:DropDownList ID="ddlVadiEvidenceType" runat="server" InitialValue="0" CssClass="form-control" style="height:30px"
                                        Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlEvidenceType_SelectedIndexChanged">
                                    </asp:DropDownList>  
                                  
                                  <div id="divtxtVadiEvidenceType" runat="server" visible="false" style="padding-top:7px;">
                                     <asp:TextBox ID="txtVadiEvidenceType" MaxLength="100" runat="server"  CssClass="form-control" ></asp:TextBox>
                                      </div>

                                  </div>
                         
                           
                              
                            </div>
                            <div class="row" style="padding:0% 2.5% 1% 2.5%;">
                            <div class="col-md-6 text-left">                               
                                वादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज <img src="../images/imp.gif" width="13px" />
                                </div>
                                 <div class="col-md-6">
                                <asp:FileUpload runat="server" CssClass="form-control" ID="file_vadi_dastavej_new" />
                                <div style="color: #FF3300; font-weight: bold; padding-top:7px;">
                                  <asp:Label ID="Label5" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></div>
                                </div>
                            </div> 
                                              
                           </ContentTemplate>
                    </asp:UpdatePanel>                  
                        <div class="row" style="padding-top:2%;">
                                        <div class="col-md-5">
                                            </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAddVadiEvidenceDetail"  OnClick="btnAddVadiEvidence_Click" OnClientClick="return ValidateVadiEvidenceDetail();" CssClass="btn btn-success" runat="server"  Text="Save"   />
                                        </div>
                                        
                                       
                                         
                                    </div>                            
                         </div> 
                    <div class="row">
                            <div class="col-md-12" style="text-align:center">
                                <asp:GridView runat="server" Width="100%" ID="gdVadiEvidence" OnRowDataBound="gdVadiEvidence_RowDataBound" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="gdVadiEvidence_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="साक्ष्य का प्रकार" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEvidenceType" Text='<%# (Convert.ToString(Eval("evidence_id")) != "9") ? Eval("evidence_name") : Eval("evidence_any_name")   %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="साक्ष्य का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:ImageButton ID="Image1"  path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" CommandArgument='<%# Container.DataItemIndex %>' CommandName="View"/>
                                  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                                      
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                

                                                <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove" 
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                                                                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                     </div>

          <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                        प्रतिवादी द्वारा प्रस्तुत साक्ष्य की विवरणी</div>
                    <div class="panel-body">

                          <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                                         <ContentTemplate>
                        
                        <div class="row" style="padding:1.5% 2.5% 1% 2.5%;">
                              <div class="col-md-6 text-left">
                                    
                                    साक्ष्य का प्रकार<img src="../images/imp.gif" width="13px" />                                                                  
                                                                     
                              </div>
                              <div id="divddlPrativadiEvidenceType" runat="server" class="col-md-6">
                                  <asp:DropDownList ID="ddlPrativadiEvidenceType" runat="server" InitialValue="0" CssClass="form-control" style="height:30px"
                                        Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlPrativadiEvidenceType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                  <div id="divtxtPrativadiEvidenceType" runat="server" visible="false" style="padding-top:7px;">
                                     <asp:TextBox ID="txtPrativadiEvidenceType"  MaxLength="100" runat="server" CssClass="form-control" ></asp:TextBox>
                                      </div>
                                  </div>
                            
                          
                               
                            </div>
                            <div class="row" style="padding:0% 2.5% 1% 2.5%;">
                            <div class="col-md-6 text-left">                               
                                प्रतिवादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज <img src="../images/imp.gif" width="13px" />
                                </div>
                                 <div class="col-md-6">
                                <asp:FileUpload runat="server" CssClass="form-control" ID="file_Prativadi_dastavej_new" />
                                <div style="color: #FF3300; font-weight: bold; padding-top:7px;">
                                  <asp:Label ID="Label6" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></div>
                                </div>
                            </div> 
                                              
                           </ContentTemplate>
                    </asp:UpdatePanel>


                        <div class="row" style="padding-top:2%;">
                                        <div class="col-md-5">
                                            </div>
                                        <div class="col-md-2">
                                            <asp:Button ID="btnAddPrativadiEvidenceDetail" OnClientClick="return ValidatePrativadiEvidenceDetail();"  OnClick="btnAddPrativadiEvidence_Click" CssClass="btn btn-success" runat="server"   Text="Save"/>
                                        </div>
                                        
                                       
                                         
                                    </div>     

                    
                         </div> 


              <div class="row">
                            <div class="col-md-12" style="text-align:center">
                                <asp:GridView runat="server" Width="100%" ID="gdPrativadiEvidence" OnRowDataBound="gdPrativadiEvidence_RowDataBound" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="gdPrativadiEvidence_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="साक्ष्य का प्रकार" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEvidenceType" Text='<%# (Convert.ToString(Eval("evidence_id")) != "9") ? Eval("evidence_name") : Eval("evidence_any_name")   %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="साक्ष्य का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:ImageButton ID="Image1"  path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                                                                 
                                        
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                

                                                 <asp:LinkButton ID="LinkButton1" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove" 
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                                                                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                     </div>

                    </div>

          <div id="step5" runat="server" visible="false">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                        प्रस्तुत साक्ष्य की विवरणी</div>
                    <div class="panel-body">
                        <asp:UpdatePanel runat="server" ID="UPanel3" UpdateMode="Conditional">
                            <ContentTemplate>

                    <div class="row" style=" padding-top:10px;"> 
                        
                        <div class="col-md-6 labeltextsize" >
                               पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                                <asp:TextBox runat="server" MaxLength="500" ID="txtpulis_padadhikari_vivarani" TextMode="MultiLine" Height="60px" 
                            CssClass="form-control"></asp:TextBox>
                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                            </div>

                        <div class="col-md-6 labeltextsize" >
                               पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज
                                <asp:FileUpload runat="server" CssClass="form-control" ID="pulis_padadhikari_Patr_file" />
                            <span style="color: #FF3300; font-weight: bold; font-size:11px">
                                                <asp:Label ID="Label13" runat="server" Text="(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                        <a href="#" id="lnkpulis_padadhikari_Patr_file" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                            </div>

                    
                    
                </div>
                <div class="row" style="padding-top:10px;">
                   
                     <div class="col-md-6 labeltextsize" >
                               हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                                <asp:TextBox runat="server" MaxLength="500" ID="txthalkakarmchari_prativedan" TextMode="MultiLine" Height="60px" 
                            CssClass="form-control"></asp:TextBox>
                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                        
                            </div>
                   
                     <div class="col-md-6 labeltextsize" >
                               हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज
                                <asp:FileUpload runat="server" CssClass="form-control" ID="file_halkakarmchari_praptr" />
                         <span style="color: #FF3300; font-weight: bold; font-size:11px">
                                                <asp:Label ID="Label14" runat="server" Text="(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                        <a href="#" id="lnkfile_halkakarmchari_praptr" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                            </div>

                    
                </div>                         

                <div class="row" style="padding-top: 10px">
                    

                    <div class="col-md-3 labeltextsize" >
                               विवादित भू-खंड की मापी<img src="../images/imp.gif" width="13px" />
                                <div id="divbhukhand_mapi" runat="server">
                                <asp:DropDownList runat="server"  CssClass="form-control"  style="height:30px"
                            OnSelectedIndexChanged="ddlbhukhand_mapi_SelectedIndexChanged" ID="ddlbhukhand_mapi" AutoPostBack="true">
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">मापी आवश्यक है</asp:ListItem>
                            <asp:ListItem Value="N">मापी आवश्यक नहीं है</asp:ListItem>            
                        </asp:DropDownList>
                        
                            </div>
                        </div>

                    <div class="col-md-3 labeltextsize" id="divbhukhand_Copy" runat="server" visible="false">
                        <br />
                        <asp:DropDownList runat="server" ID="ddlbhukhand_Copy" CssClass="form-control"  style="height:30px"
                            OnSelectedIndexChanged="ddlbhukhand_Copy_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">मापी हुई है</asp:ListItem>
                            <asp:ListItem Value="N">मापी नहीं हुई है</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    
                    <div class="col-md-3 labeltextsize" id="divMapiKeNirdharit_tithi" runat="server" visible="false">
                         मापी के लिए निर्धारित तिथि<img src="../images/imp.gif" width="13px" />
                        <asp:TextBox runat="server" ID="txtMapiKeNirdharit_tithi" onkeypress="return dateValidate(event)" MaxLength="10" placeholder="DD-MM-YYYY"  CssClass="form-control"   ></asp:TextBox>
                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtMapiKeNirdharit_tithi" Format="dd-MM-yyyy" CssClass="zindex">
                                                    </cc1:CalendarExtender>    
                    </div>

                      <div class="col-md-3 labeltextsize"  runat="server" >
                         <asp:Label  Style="font-size: 14px" ID="lblbhukand_prativedan" runat="server" Text=" विवादित भू-खंड की मापी का प्रतिवेदन"></asp:Label>
                        <asp:Label runat="server" Style="font-size: 14px" ID="lblbhukand_karan" Text="  विवादित भू-खंड की मापी नहीं होने का कारण"></asp:Label>
                        <asp:FileUpload runat="server" CssClass="form-control" ID="file_bhukand_prativedan" />
                          <span style="color: #FF3300; font-weight: bold; font-size:11px">
                                                <asp:Label ID="Label15" runat="server" Text="(पत्र केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                          <a href="#" id="lnkfile_bhukand_prativedan" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                        <asp:TextBox runat="server" ID="txtbhukhand_reason" MaxLength="500" TextMode="MultiLine" Height="60px" CssClass="form-control"></asp:TextBox>   
                    </div>

                </div>
                <div class="row" style="padding-top: 10px">
                   
                    <div class="col-md-3" style="visibility:hidden;">
                        चौकीदार द्वारा उपलब्ध कराई गई सूचना का विवरण
                    </div>
                    <div class="col-md-3" style="visibility:hidden;">
                        <asp:TextBox runat="server" ID="txtchokidar_vivran" MaxLength="500" TextMode="MultiLine"
                            Height="60px" CssClass="form-control"></asp:TextBox>
                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                    </div>
                </div>

                         </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>


                    </div>
              </div>

          <div id="step6" runat="server" visible="false">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                         भूमि विवाद सें संबंधित घटना/ वारदात का विवरण</div>
                   <div class="panel-body">
                       
                                <div class="row">
                             <div class="col-md-5 labeltextsize">
                                क्या भूमि विवाद सें संबंधित प्राथमिकी/अप्राथमिकी दर्ज है ?<img src="../images/imp.gif"
                                    width="13px" />
                             </div>
                            <div class="col-md-4 labeltextsize">
                                <asp:DropDownList runat="server" ID="dd_IsBhumiVivad" CssClass="form-control" AutoPostBack="true" style="height:30px"
                                    OnSelectedIndexChanged="dd_IsBhumiVivad_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                    <asp:ListItem Value="Y">हां</asp:ListItem>
                                    <asp:ListItem Value="N"> नहीं</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 labeltextsize"></div>
                        </div>
                         <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                            <ContentTemplate>
                    <div id="btnBhumiVivadVivran" runat="server" visible="false">
                        <div class="row" style="padding-top: 7px">
                                        <div class="col-md-12 text-left" style="background-color: Blue; color: White">
                                            <h4>
                                                विवाद सें संबंधित घटना/ वारदात का विवरण</h4>
                                        </div>
                                    </div>
                         <div class="row labeltextsize" style="padding:1.5% 2.5% 1% 2.5%;">
                             
                                        <div class="col-md-6 text-left">
                                            घटना / वारदात की तिथि
                                            <img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtghatanaDate" MaxLength="10" CssClass="form-control" placeholder="DD-MM-YYYY"  onkeypress="return dateValidate(event)" ></asp:TextBox>
                                            <cc1:CalendarExtender ID="calender2" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txtghatanaDate" Format="dd-MM-yyyy" CssClass="zindex">
                                                    </cc1:CalendarExtender>
                                           
                                        </div>
                                    </div>
                                    <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                       
                                        <div class="col-md-6 text-left">
                                            घटना की संक्षिप्त विवरण
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" Height="60px" ID="txtghatanavivran"
                                                CssClass="form-control"></asp:TextBox>
                                            <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                                        </div>
                                    </div>
                                    <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%; display:none;" >
                                        
                                        <div class="col-md-6 text-left">
                                            विवरण का प्रकार<img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList runat="server" ID="ddlDescriptionType" CssClass="form-control" style="height:30px">
                                                <asp:ListItem>--चुने--</asp:ListItem>
                                                <asp:ListItem Value="प्राथमिकी">प्राथमिकी</asp:ListItem>
                                                <asp:ListItem Value="अप्राथमिकी">अप्राथमिकी</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                        
                                        <div class="col-md-6 text-left">
                                            प्राथमिकी दर्ज है ?<img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList runat="server" ID="ddlPrathmiki_huyee_hai" AutoPostBack="true" OnSelectedIndexChanged="ddlPrathmiki_huyee_hai_SelectedIndexChanged"
                                                 CssClass="form-control" style="height:30px"
                                                >
                                                <asp:ListItem>--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row labeltextsize" id="divPrathmiki_sankhiyan" runat="server" visible="false" style="padding:0% 2.5% 1% 2.5%;">
                                        
                                        <div class="col-md-6 text-left">
                                            प्राथमिकी संख्या <img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtFIR_sankhya" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row labeltextsize" id="divPrathmiki_vivaran" runat="server" visible="false"  style="padding:0% 2.5% 1% 2.5%;">
                                       
                                        <div class="col-md-6 text-left">
                                            प्राथमिकी का विवरण
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtPrathmik_vivran" MaxLength="500" TextMode="MultiLine" Height="60px" CssClass="form-control"></asp:TextBox>
                                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                                        </div>
                                    </div>

                                    <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                       
                                        
                                        <div class="col-md-6 text-left">
                                            अप्राथमिकी दर्ज है ?<img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList runat="server" ID="ddlAprathmiki_huyee_hai" CssClass="form-control" style="height:30px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAprathmiki_huyee_hai_SelectedIndexChanged">
                                                <asp:ListItem>--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row labeltextsize" id="divDhara" runat="server" visible="false"  style="padding:0% 2.5% 1% 2.5%;">
                                       
                                        <div class="col-md-6 text-left">
                                            धारा<img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            
                                            <asp:CheckBox ID="chk107" runat="server" Text="107" />
                                            <asp:CheckBox ID="chk109" runat="server" Text="109" />
                                            <asp:CheckBox ID="chk110" runat="server" Text="110" />
                                            <asp:CheckBox ID="chk113" runat="server" Text="113" />
                                            <asp:CheckBox ID="chk116" runat="server" Text="116" />
                                            <asp:CheckBox ID="chk133" runat="server" Text="133" />
                                            <asp:CheckBox ID="chk144" runat="server" Text="144" />
                                            <asp:CheckBox ID="chk145" runat="server" Text="145" />
                                            <asp:CheckBox ID="chk147" runat="server" Text="147" />

                                        </div>
                                    </div>
                                    <div class="row labeltextsize" id="divAPrathmiki_sankhiyan" runat="server" visible="false"  style="padding:0% 2.5% 1% 2.5%;">
                                       
                                        <div class="col-md-6 text-left">
                                            अप्राथमिकी संख्या <img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtAFIR_sankhya" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row labeltextsize" id="divAPrathmiki_vivaran" runat="server" visible="false"  style="padding:0% 2.5% 1% 2.5%;">
                                        
                                        <div class="col-md-6 text-left">
                                            अप्राथमिकी का विवरण
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtAprathmik_vivran" MaxLength="500" TextMode="MultiLine" Height="60px" CssClass="form-control"></asp:TextBox>
                                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                                        </div>
                                    </div>
                                  
                                    <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                       
                                        <div class="col-md-6 text-left">
                                            सनहा दर्ज है ?<img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:DropDownList runat="server" ID="ddlSanhaStatus" CssClass="form-control" style="height:30px"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlSanhaStatus_SelectedIndexChanged">
                                                <asp:ListItem>--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row labeltextsize" id="divSanahaSankhiyan" runat="server" visible="false" style="padding:0% 2.5% 1% 2.5%;">
                                      
                                        <div class="col-md-6 text-left">
                                            सनहा संख्या <img src="../images/imp.gif" width="13px" />
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" ID="txtSanahaSankhiyan" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                      
                                        <div class="col-md-6 text-left">
                                            अभियुक्ति :
                                        </div>
                                        <div class="col-md-6">
                                            <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" Height="60px" ID="txtabhiyukt_vaad"
                                                CssClass="form-control"></asp:TextBox>
                                            <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                                        </div>
                                    </div>


                          
                    </div>
                                 
                         </ContentTemplate>
                      </asp:UpdatePanel>

                        <div class="row" style="padding-top:2%;">
                                        <div class="col-md-5">
                                            </div>
                                        <div class="col-md-2">
                                             <asp:Button ID="btnbhumivivad" CssClass="btn btn-success" OnClick="btnbhumivivad_Click" OnClientClick="return ValidateBhumiVivad();"  runat="server" 
                                                Text="Save" visible="false"/>
                                        </div>
                                        
                                       
                                         
                                    </div>


                  </div>
                    <div class="row">
                            <div class="col-md-12" style="text-align:center">
                                <asp:GridView runat="server" ID="grdbhumivivad" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="grdbhumivivad_RowCommand">
                                    <Columns>
                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                        <asp:BoundField DataField="Ghatna_Vardat_date" HeaderText="घटना की तिथि" ItemStyle-Width="80" />
                                        <%--<asp:BoundField DataField="BriefDescriptionOfTheEvent" HeaderText="घटना की संक्षिप्त विवरण" ItemStyle-Width="220" />--%>
                                        <asp:TemplateField HeaderText="घटना की संक्षिप्त विवरण" ItemStyle-Width="220">
                                            <ItemTemplate>
                                                <span id='<%# Eval("Ghatna_Short_vivran") %>' style="display: block;overflow: auto; height:25px;" >
                                                    <%# Eval("Ghatna_Short_vivran") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="is_FIR_registered" HeaderText="प्राथमिकी" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="praathamiki_sankhya" HeaderText="प्राथमिकी संख्या" ItemStyle-Width="100" />
                                        <%--<asp:BoundField DataField="DetailsOfFIR" HeaderText="प्राथमिकी/अप्राथमिकी का विवरण" ItemStyle-Width="220" />--%>
                                        <asp:TemplateField HeaderText="प्राथमिकी का विवरण" ItemStyle-Width="220">
                                            <ItemTemplate>
                                                <span id='<%# Eval("praathamiki_ka_vivaran") %>' style="display: block;overflow: auto; height:25px;" >
                                                    <%# Eval("praathamiki_ka_vivaran") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="is_complaint_filed" HeaderText="अप्राथमिकी" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="dhaara" HeaderText="धारा" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="apraathamiki_sankhya" HeaderText="अप्राथमिकी संख्या" ItemStyle-Width="100" />
                                        <%--<asp:BoundField DataField="DetailsOfFIR" HeaderText="प्राथमिकी/अप्राथमिकी का विवरण" ItemStyle-Width="220" />--%>
                                        <asp:TemplateField HeaderText="अप्राथमिकी का विवरण" ItemStyle-Width="220">
                                            <ItemTemplate>
                                                <span id='<%# Eval("apraathamiki_ka_vivaran") %>' style="display: block;overflow: auto; height:25px;" >
                                                    <%# Eval("apraathamiki_ka_vivaran") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="is_Sanha_recorded" HeaderText="सनहा" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="sanha_sankhya" HeaderText="सनहा संख्या" ItemStyle-Width="100" />

                                        <%--<asp:BoundField DataField="Accusation" HeaderText="वाद की अद्यतन स्थिति की अभियुक्ति" ItemStyle-Width="220" />--%>                            
                                        <asp:TemplateField HeaderText="अभियुक्ति" ItemStyle-Width="220">
                                            <ItemTemplate>
                                                <span id='<%# Eval("Abhiyukt") %>' style="display: block;overflow: auto; height:25px;" >
                                                    <%# Eval("Abhiyukt") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                               
                                           
                                                <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove" 
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                     </div>

                <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                         न्यायालय में प्रक्रियाधीन वाद का विवरण</div>
                      <div class="panel-body">
                        
                    <div class="row labeltextsize">
                    <div class="col-md-5">
                        क्या न्यायालय में प्रक्रियाधीन वाद का विवरण उपलब्ध है ?<img src="../images/imp.gif" width="13px" />
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddl_Isbhumi_Viviad_available" CssClass="form-control" style="height:30px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddl_Isbhumi_Viviad_available_SelectedIndexChanged">
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">उपलब्ध है</asp:ListItem>
                            <asp:ListItem Value="N">उपलब्ध नहीं है</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3"></div>
                </div>
                        <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="btnnyayalay" runat="server" visible="false">
                                     <div class="row labeltextsize" style="padding-top: 7px">
                                <div class="col-md-12 text-left" style="background-color: Blue; color: White">
                                    <h4>
                                        न्यायालय में दायर वाद का विवरण</h4>
                                </div>
                            </div>
                            <asp:UpdatePanel runat="server" ID="UPanel5" UpdateMode="Conditional">
                           <ContentTemplate>
                            <div class="row labeltextsize" style="padding:1.5% 2.5% 1% 2.5%;">
                                <div class="col-md-6 text-left">
                                    न्यायालय
                                    <img src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-6">
                                    <asp:DropDownList runat="server" ID="ddlnyayalaya" CssClass="form-control" AutoPostBack="true" style="height:30px"
                                        OnSelectedIndexChanged="ddlnyayalaya_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row labeltextsize" id="div_rajasw_vevhar_nyalay" runat="server" style="padding:0% 2.5% 1% 2.5%;" visible="false">
                                <div class="col-md-6 text-left">
                                   <asp:Label ID="labNyayalaya_type" Text="राजस्व/व्यवहार न्यायालय का प्रकार" runat="server"></asp:Label><img src="../images/imp.gif" alt="" width="13px" />
                                </div>
                                <div class="col-md-6 labeltextsize">
                                    <asp:DropDownList runat="server" ID="ddlnyayalaya_type" CssClass="form-control" style="height:30px" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlnyayalaya_type_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                </div>
                            </div>
                             
                            <div class="row labeltextsize" id="divDist_nyayalaya_type" runat="server" style="padding:0% 2.5% 1% 2.5%;" visible="false">
                                <div class="col-md-6 text-left">
                                    जिला<img src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-6">
                                    
                                    <asp:DropDownList runat="server" ID="ddlDist_nyayalaya_type" CssClass="form-control" style="height:30px"
                                        AutoPostBack="true" OnSelectedIndexChanged="ddlDist_nyayalaya_type_SelectedIndexChanged">
                                    </asp:DropDownList>
                                
                                </div>
                            </div> 
                            
                            <div class="row labeltextsize" id="divSubdivision_nyayalaya_type" runat="server" style="padding:0% 2.5% 1% 2.5%;" visible="false">
                                <div class="col-md-6 text-left">
                                    अनुमंडल<img src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-6">
                                    
                                    <asp:DropDownList runat="server" ID="ddlSubdivision_nyayalaya_type" CssClass="form-control" style="height:30px">
                                    </asp:DropDownList>
                                
                                </div>
                            </div> 

                        <div class="row labeltextsize" id="divVibhag_nyayalay_type" visible="false" runat="server" style="padding:0% 2.5% 1% 2.5%;" >
                                <div class="col-md-6 text-left">
                                    विभाग<img src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-6">
                                    
                                    <asp:DropDownList runat="server" ID="ddlVibhag_nyayalay_type" CssClass="form-control" style="height:30px">
                                    </asp:DropDownList>
                                
                                </div>
                            </div> 
                        </ContentTemplate>
                                </asp:UpdatePanel>                                                       
                            <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                <div class="col-md-6 text-left">
                                    वादी की वाद संख्या / वर्ष<img src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server"  ID="txtdayarvaadsankhya_nayalay"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%; display:none;">
                                <div class="col-md-6 text-left">
                                    वादी की वाद का वर्ष<img src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" onkeypress="return ValidateAlpha(event)" ID="txtdayaryear_nayayaly"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                <div class="col-md-6 text-left">
                                    वादी का नाम<img src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" onkeypress="return ValidateAlpha(event)" ID="txtvaadiname_nayaylay"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                <div class="col-md-6 text-left">
                                    प्रतिवादी का नाम<img src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" onkeypress="return ValidateAlpha(event)" ID="txtprativadi_nayaylay"
                                        CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row labeltextsize" style="padding:0% 2.5% 1% 2.5%;">
                                <div class="col-md-6 text-left">
                                    वाद की अद्यतन स्थिति का विवरण
                                </div>
                                <div class="col-md-6">
                                    <asp:TextBox runat="server" ID="txtwadKiAddhatan_Sthiti_nayayaly" TextMode="MultiLine" MaxLength="500"
                                       Height="60px" CssClass="form-control"></asp:TextBox>
                                    <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                                </div>
                            </div>


                                   
                    </div>

                                </div>
                         </ContentTemplate>
                      </asp:UpdatePanel>

                         <div class="row" style="padding-top:1%;padding-bottom:10px">
                                        <div class="col-md-5">
                                            </div>
                                        <div class="col-md-2">
                                             <asp:Button ID="btnnayaylaysave" CssClass="btn btn-success" OnClick="btnnayaylaysave_Click" OnClientClick="return ValidateNayaylayDetails();"  runat="server" 
                                                Text="Save" Visible="false" />
                                        </div>
                                        
                                       
                                         
                                    </div>  
                        
                  </div>
                    
                       <div class="row">
                    <div class="col-md-12" style="text-align: center">
                        <asp:GridView runat="server" ID="grdnyayalay_vivran" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="grdnyayalay_vivran_RowCommand">
                            <Columns>
                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                <asp:BoundField DataField="court" HeaderText="न्यायालय" ItemStyle-Width="80" />
                                <asp:BoundField DataField="courtType" HeaderText="न्यायालय का प्रकार" ItemStyle-Width="100" /> 
                                <asp:BoundField DataField="Dst" HeaderText="जिला" ItemStyle-Width="80" />
                                <asp:BoundField DataField="SubDiv" HeaderText="अनुमंडल" ItemStyle-Width="80" />
                                <asp:BoundField DataField="Vibhag" HeaderText="विभाग" ItemStyle-Width="80" />                               
                                <asp:BoundField DataField="vaadi_ki_vaad_sankhya_varsh" HeaderText="वाद संख्या / वर्ष" ItemStyle-Width="80" />
                                
                                <asp:BoundField DataField="vadi_name" HeaderText="वादी का नाम" ItemStyle-Width="80" />
                                <asp:BoundField DataField="prativadi_name" HeaderText="प्रतिवादी का नाम" ItemStyle-Width="80" />
                                <asp:BoundField DataField="vaad_ki_addhatan_sthiti_vivaran" HeaderText="अद्धतन स्थिति का विवरण" ItemStyle-Width="80" />
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                
                                            <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove" 
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                </Columns>
                        </asp:GridView>
                    </div>
                </div>

                     </div>
              
              </div>

          <div id="step7" runat="server" visible="false">
                <div class="panel panel-primary">
                    <div class="panel-heading" style="padding: 3px 20px">
                        अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी</div>
                    
                    <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="UPanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                <div class ="row" style="margin-bottom:auto;">
                    <div class="col-md-3">
                              भूमि विवाद की सवेदनशीलता<img src="../images/imp.gif" width="13px" />
                               <asp:DropDownList AutoPostBack="true"  OnSelectedIndexChanged="ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged" runat="server" 
                                   ID="ddlbhumivivadki_sanvedanshilta" CssClass="form-control" style="height:30px">
                                                            </asp:DropDownList>
                              </div>
                              <div class="col-md-3 text-center">
                                 <br />
                              <asp:Image Width="100px" ImageUrl="../images/1.png" ID="onestar" runat="server" />
                                  <asp:Image Width="100px" ImageUrl="../images/2.png" ID="twostar" runat="server" />
                                      <asp:Image Width="100px" ImageUrl="../images/3.png" ID="threestar" runat="server" />
                                          <asp:Image Width="100px" ImageUrl="../images/4.png" ID="fourstar" runat="server" />
                              </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                </div>
                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                <div class="row" style="padding-top:7px;">
                        
                    </div>
                <div class="row" style="padding-top: 7px">
                    <div class="col-md-3">
                        बैठक की तिथि<img src="../images/imp.gif" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtbaithakDate" MaxLength="10" CssClass="form-control" placeholder="DD-MM-YYYY"  onkeypress="return dateValidate(event)" ></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender3" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txtbaithakDate" Format="dd-MM-yyyy" CssClass="zindex">
                                                    </cc1:CalendarExtender>
                    </div>
                    <div class="col-md-3">
                        क्या वादी उपस्थित है ?<img src="../images/imp.gif" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="ddlIsVadiAvailable" CssClass="form-control" style="height:30px">
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">हां</asp:ListItem>
                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="padding-top: 7px">
                    <div class="col-md-3">
                        क्या प्रतिवादी उपस्थित है ?<img src="../images/imp.gif" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="ddl_IsprativadiAvailable" CssClass="form-control" style="height:30px">
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">हां</asp:ListItem>
                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        बैठक का निष्कर्ष(Action)<img src="../images/imp.gif" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlaction" CssClass="form-control" style="height:30px" OnSelectedIndexChanged="ddlaction_SelectedIndexChanged">
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                            <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                            <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                            <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                             <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                        </asp:DropDownList>
                    </div> 
                </div>
                <div class="row" style="padding-top: 7px">
                    <div class="col-md-3">
                        अंचलाधिकारी का मंतव्य
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" Height="60px" ID="txtabhiyukt_anchaladhikari"
                            CssClass="form-control"></asp:TextBox>
                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                    </div>
                    <div class="col-md-6" id="divNextDateDef" runat="server" visible="true">
                      
                    </div>
                    <div class="col-md-3" id="divlabNextDate" runat="server" visible ="false">
                      <asp:Label ID="labNextDate"  Text="अगला/मापी की तिथि" runat="server"></asp:Label><img src="../images/imp.gif" width="13" />
                    </div>
                    <div class="col-md-3" id="divNextDate" runat="server" visible="false">
                                                    <asp:TextBox runat="server" ID="txtAgalaDate" MaxLength="10" CssClass="form-control" placeholder="DD-MM-YYYY"  onkeypress="return dateValidate(event)" ></asp:TextBox>
                            <cc1:CalendarExtender ID="PopCalendar2" runat="server" TargetControlID="txtAgalaDate" Format="dd-MM-yyyy" CssClass="zindex">
                                                    </cc1:CalendarExtender>
                       

                    </div>
                     <div class="col-md-3" id="divCancelReason" runat="server" visible="false">                        
                            <asp:TextBox runat="server" ID="txtCancelReason" MaxLength="500" CssClass="form-control" TextMode="MultiLine" Height="60px"></asp:TextBox>
                     <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div> 
                     </div>

                </div>
                <div class="row" style="padding-top: 7px">
                    
                    <div class="col-md-3">
                        थानाध्यक्ष का मंतव्य
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" Height="60px" ID="txtabhiyukt_thaanprabhaaree"
                            CssClass="form-control"></asp:TextBox>
                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                    </div>

                    <div class="col-md-3">
                        बैठक में लिया गया निर्णय
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtfalafal" MaxLength="500" TextMode="MultiLine" Height="60px" CssClass="form-control"></asp:TextBox>
                    <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                    </div>
                     
                </div>
                <div class="row" style="padding-top:7px;">
                    <div class="col-md-3">
                        थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="LandDoc" runat="server" CssClass="form-control" accept=".pdf" />
                        <asp:HiddenField ID="hdLandDoc" runat="server" />
                        <span style="color: #FF3300; font-weight: bold;font-size:11px">
                            <asp:Label ID="Label7" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                        <a href="#" id="lnkLandDoc" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                    </div>


                    <div class="col-md-3">
                        अंचलाधिकारी का मंतव्य पत्र
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="CircleOfficer_letterOfIntent" accept=".pdf" runat="server" CssClass="form-control"  />
                        <asp:HiddenField ID="hdCircleOfficer_letterofintent" runat="server" />
                        <span style="color: #FF3300; font-weight: bold;font-size:11px">
                            <asp:Label ID="Label8" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                        <a href="#" id="lnkCircleOfficer_letterOfIntent" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                    </div>

                    
                                                       
                </div>

                          <div class="row" style="padding-top:7px;">
                              <div class="col-md-3">
                        थानाध्यक्ष का मंतव्य पत्र
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="PoliceOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />
                        <asp:HiddenField ID="hdPoliceOfficer_letterOfIntent" runat="server" />
                        <span style="color: #FF3300; font-weight: bold;font-size:11px">
                            <asp:Label ID="Label9" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                         <a href="#" id="lnkPoliceOfficer_letterOfIntent" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                    </div>
                              </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
            </div>

                    
                       
                    
                    </div>
             </div>

          <div id="tddetails" class="panel panel-primary" runat="server" visible="true">
                <div class="row labeltextsize" style="padding-top:0.5%; padding-left: 0.5%; padding-bottom:0.5%">
                    <asp:HiddenField ID="step" runat="server" Value="1" />
                    
                             
                    <div class="col-md-2">
                       
                         


                         <asp:LinkButton ID="btnBack" OnClick="btnBack_Click" Visible="false"  CssClass="btn btn-danger" runat="server" style="width:100%" 
                                                    ><i class="fa fa-arrow-left"  aria-hidden="true"></i>&nbsp;&nbsp;Back</asp:LinkButton>


                    </div>
                     
                    <%--<div class="col-md-2">
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger" style="width:100%"/>&nbsp;&nbsp;
                    </div>--%>
                    <div class="col-md-2">
                       
                         <asp:LinkButton ID="btnDraft"  runat="server" OnClick="btnDraft_Click" CssClass="btn btn-info"style="width:100%" 
                                                    ><i class="fa fa-home"  aria-hidden="true"></i>&nbsp;&nbsp;Go To Home</asp:LinkButton>
                    </div>
                     <div class="col-md-6">
                         </div>
                    <div class="col-md-2">
                        <%--<asp:Button ID="btnNext" runat="server" Text="Save & Next" CssClass="btn btn-success" onClientClick=" return confirm('Are you sure to save data?')" style="width:100%" OnClick="btnNext_Click"/>&nbsp;&nbsp;--%>
                    <asp:LinkButton ID="btnNext" onClientClick=" return confirm('Are you sure to save data?')"  OnClick="btnNext_Click"  runat="server"  CssClass="btn btn-success" style="width:100%"
                                                    >Save & Next&nbsp;&nbsp;<i class="fa fa-floppy-o"  aria-hidden="true"></i></asp:LinkButton>
                     
                     
                     </div>
                </div>
            </div>
       
    </div>
 
    
    <div id="divImage">
            <table style="height: 100%; width: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img id="imgLoader" alt="" src="../img/loadern.gif" />
                        <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
                        <iframe id="inlineFrameExample" title="Inline Frame Example"  width="99%" height="600px" src=""></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="bottom">
                        <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
                    </td>
                </tr>
            </table>
        </div>

</asp:Content>
