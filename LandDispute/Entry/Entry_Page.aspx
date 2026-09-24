<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Entry_Page.aspx.cs" Inherits="LandDispute_Entry_Entry_Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="JS/userapplicationvalidation.js"></script>
    <style type="text/css">
        .zindex {
            background-color: #FAF5EF;
            z-index: 10001;
        }
                .dharaType input[type="radio"] {
    margin-right: 6px;
    cursor: pointer;
}

.dharaType label {
    margin-right: 25px;
    font-weight: 500;
    cursor: pointer;
}
    </style>

    <script src="JS/Jquerry2000min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function checkDate(sender, args) {
            //if (sender._selectedDate > new Date()) {
            //    alert("You cannot select a day latter than today!");
            //    sender._selectedDate = new Date();
            //    // set the date back to the current date
            //    sender._textbox.set_Value("")
            //}
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
    </script>
    <script>
        function saveIPCSelection() {
            var list = document.getElementById('<%= ddldhara1.ClientID %>');
        var selected = [];

        for (var i = 0; i < list.options.length; i++) {
            if (list.options[i].selected) {
                selected.push(list.options[i].value);
            }
        }

        document.getElementById('<%= hdnSelectedIPC.ClientID %>').value = selected.join(',');
    }

    $(document).ready(function () {
        $('#<%= ddlbsn_dhara_hai.ClientID %>').on('change', function () {
            saveIPCSelection();
        });
    });
    </script>

    <%--for preview-vadivalidate--step-7--%>
    <script type="text/javascript">
        function validationStep7() {
            var ddlbhumivivadki_sanvedanshilta = document.getElementById('<%=ddlbhumivivadki_sanvedanshilta.ClientID%>');
            if (ddlbhumivivadki_sanvedanshilta.options[ddlbhumivivadki_sanvedanshilta.selectedIndex].value == 0) {
                alert("कृपया विवाद की संवेदनशीलता चुनें...!");
                ddlbhumivivadki_sanvedanshilta.focus();
                return false;
            }
            var txtbaithakDate = document.getElementById('<%=txtbaithakDate.ClientID%>');
            if (txtbaithakDate.value == "") {
                alert("कृपया बैठक की तिथि अंकित करें..");
                txtbaithakDate.focus();
                return false;
            }
            var ddlIsVadiAvailable = document.getElementById('<%=ddlIsVadiAvailable.ClientID%>');
            if (ddlIsVadiAvailable.options[ddlIsVadiAvailable.selectedIndex].value == 0) {
                alert("कृपया क्या वादी उपस्थित है ? चुनें...!");
                ddlIsVadiAvailable.focus();
                return false;
            }
            var ddl_IsprativadiAvailable = document.getElementById('<%=ddl_IsprativadiAvailable.ClientID%>');
            if (ddl_IsprativadiAvailable.options[ddl_IsprativadiAvailable.selectedIndex].value == 0) {
                alert("कृपया क्या प्रतिवादी उपस्थित है ? चुनें...!");
                ddl_IsprativadiAvailable.focus();
                return false;
            }
            var ddlaction = document.getElementById('<%=ddlaction.ClientID%>');
            if (ddlaction.options[ddlaction.selectedIndex].value == 0) {
                alert("कृपया बैठक का निष्कर्ष (Action) चुनें...!");
                ddlaction.focus();
                return false;
            }
            if (ddlaction.options[ddlaction.selectedIndex].value == 4) {
                var txtCancelReason = document.getElementById('<%=txtCancelReason.ClientID%>');
                if (txtCancelReason.value == "") {
                    alert("कृपया अस्वीकृति का कारण  करें..");
                    txtCancelReason.focus();
                    return false;
                }
            }
            else if (ddlaction.options[ddlaction.selectedIndex].value != 0) {
                var txtAgalaDate = document.getElementById('<%=txtAgalaDate.ClientID%>');
                if (txtAgalaDate.value == "") {
                    alert("कृपया तिथि अंकित करें..");
                    txtAgalaDate.focus();
                    return false;
                }
            }
            var txtfalafal = document.getElementById('<%=txtfalafal.ClientID%>');
            if (txtfalafal.value == "") {
                alert("कृपया बैठक में लिया गया निर्णय  करें..");
                txtfalafal.focus();
                return false;
            }
            var txtabhiyukt_anchaladhikari = document.getElementById('<%=txtabhiyukt_anchaladhikari.ClientID%>');
            if (txtabhiyukt_anchaladhikari.value == "") {
                alert("कृपया अंचलाधिकारी का मंतव्य करें..");
                txtabhiyukt_anchaladhikari.focus();
                return false;
            }
            var txtabhiyukt_thaanprabhaaree = document.getElementById('<%=txtabhiyukt_thaanprabhaaree.ClientID%>');
            if (txtabhiyukt_thaanprabhaaree.value == "") {
                alert("कृपया थानाध्यक्ष का मंतव्य करें..");
                txtabhiyukt_thaanprabhaaree.focus();
                return false;
            }
            return true;
        }
    </script>
    <%--ValidateBhumiKaVivaran-vadivalidate--step-6--%>
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

    <%--ValidateBhumiKaVivaran-vadivalidate--step-5--%>
    <script type="text/javascript">
        function RajAdhikariPoliceAdhikari() {
            <%-- var Textpulis_padadhikari_vivarani = document.getElementById('<%=txtpulis_padadhikari_vivarani.ClientID%>');
             if (Textpulis_padadhikari_vivarani.value == "")
              {
                 alert("कृपया पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी करें..");
                  txtpulis_padadhikari_vivarani.focus();
                  return false;
             }

             var Texthalkakarmchari_prativedan = document.getElementById('<%=txthalkakarmchari_prativedan.ClientID%>');
             if (Texthalkakarmchari_prativedan.value == "")
              {
                  alert("कृपया हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी करें..");
                  txthalkakarmchari_prativedan.focus();
                  return false;
             }--%>

             var ddlbhukhand_mapi = document.getElementById('<%=ddlbhukhand_mapi.ClientID%>');
             if (ddlbhukhand_mapi.options[ddlbhukhand_mapi.selectedIndex].value == 0) {
                 alert("कृपया विवादित भू-खंड की मापी चुनें...!");
                 ddlbhukhand_mapi.focus();
                 return false;
             }
             if (ddlbhukhand_mapi.options[ddlbhukhand_mapi.selectedIndex].value == "Y") {
                 var ddlbhukhand_Copy = document.getElementById('<%=ddlbhukhand_Copy.ClientID%>');
                 if (ddlbhukhand_Copy.options[ddlbhukhand_Copy.selectedIndex].value == 0) {
                     alert("कृपया मापी ? चुनें...!");
                     ddlbhukhand_Copy.focus();
                     return false;
                 }

                 if (ddlbhukhand_Copy.options[ddlbhukhand_Copy.selectedIndex].value == "N") {
                     var TextMapiKeNirdharit_tithi = document.getElementById('<%=txtMapiKeNirdharit_tithi.ClientID%>');
                      if (TextMapiKeNirdharit_tithi.value == "") {
                          alert("कृपया मापी के लिए निर्धारित तिथि करें..");
                          txtMapiKeNirdharit_tithi.focus();
                          return false;
                      }

                         <%-- var Textbhukhand_reason = document.getElementById('<%=txtbhukhand_reason.ClientID%>');
                         if (Textbhukhand_reason.value == "")
                          {
                              alert("कृपया विवादित भू-खंड की मापी नहीं होने का कारण  करें..");
                              txtbhukhand_reason.focus();
                              return false;
                          }--%>
                }
            }

            return true;
        }
    </script>

    <%--ValidateBhumiKaVivaran-vadivalidate--step-4--%>
    <script type="text/javascript">
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
                if (obj1.files[0].size > (0.3 * 1024 * 1024 * 1024)) {
                    alert("File size must be less than or equal to 3 MB |");
                    obj1.focus();
                    return false;

                }
            }
            return true;
        }
    </script>
    <script type="text/javascript">
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
                if (obj1.files[0].size > (0.3 * 1024 * 1024 * 1024)) {
                    alert("File size must be less than or equal to 3 MB |");
                    obj1.focus();
                    return false;

                }

            }
            return true;
        }
    </script>

    <%--ValidateBhumiKaVivaran-vadivalidate--step-3--%>
    <script type="text/javascript">          
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
    </script>

    <%--Prati-vadivalidate--step-2--%>
    <script type="text/javascript">
        function ValidatePratiVadiDetail() {
            // debugger;
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
    </script>
    <script type="text/javascript">
        function AnyaVivaran() {
            //debugger;
            var ddlwadi_pratiwadi_sunwaiId = document.getElementById('<%=ddlwadi_pratiwadi_sunwai.ClientID%>');
              if (ddlwadi_pratiwadi_sunwaiId.options[ddlwadi_pratiwadi_sunwaiId.selectedIndex].value == 0) {
                  alert("कृपया प्रतिवादी को सूचित किया गया है या नहीं ?  चुनें...!");
                  ddlwadi_pratiwadi_sunwaiId.focus();
                  return false
              }
              if (ddlwadi_pratiwadi_sunwaiId.options[ddlwadi_pratiwadi_sunwaiId.selectedIndex].value == "Y") {
                  var ddlKiskeduwara_bhejagayaID = document.getElementById('<%=ddlKiskeduwara_bhejagaya.ClientID%>');
                  if (ddlKiskeduwara_bhejagayaID.options[ddlKiskeduwara_bhejagayaID.selectedIndex].value == 0) {
                      alert("कृपया माध्यम चुनें...!");
                      ddlKiskeduwara_bhejagaya.focus();
                      return false;
                  }

                  var ddlSuchana_ka_tamilaId = document.getElementById('<%=ddlSuchana_ka_tamila.ClientID%>');
                  if (ddlSuchana_ka_tamilaId.options[ddlSuchana_ka_tamilaId.selectedIndex].value == 0) {
                      alert("कृपया प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ? चुनें...!");
                      ddlSuchana_ka_tamila.focus();
                      return false;
                  }

                  if (ddlSuchana_ka_tamilaId.options[ddlSuchana_ka_tamilaId.selectedIndex].value == "Y" || ddlSuchana_ka_tamilaId.options[ddlSuchana_ka_tamilaId.selectedIndex].value == "N") {
                      var ddlSuchana_ka_upasthitiID = document.getElementById('<%=ddlSuchana_ka_upasthiti.ClientID%>');
                      if (ddlSuchana_ka_upasthitiID.options[ddlSuchana_ka_upasthitiID.selectedIndex].value == 0) {
                          alert("कृपया प्रतिवादी उपस्थित हुआ है या नहीं ? चुनें...!");
                          ddlSuchana_ka_upasthiti.focus();
                          return false;
                      }
                  }
              }
              if (ddlwadi_pratiwadi_sunwaiId.options[ddlwadi_pratiwadi_sunwaiId.selectedIndex].value == "N") {
                  var txtsunwaiHetuNoticKaKaranID = document.getElementById('<%=txtsunwaiHetuNoticKaKaran.ClientID%>');
                if (txtsunwaiHetuNoticKaKaranID.value == "") {
                    alert("कृपया कारण स्पष्ट करें चुनें...!");
                    txtsunwaiHetuNoticKaKaran.focus();
                    return false;
                }
            }
            return true;
        }
    </script>

    <%--Prati-vadivalidate--step-1--%>
    <script type="text/javascript">       
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
                  if (labUVillage != null) {
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

              if (ddlUserAreatype.selectedIndex == 1) {
                  var ddlUserVillage = document.getElementById('<%=ddlUserVillage.ClientID%>');
                  if (ddlUserVillage.selectedIndex == 0) {
                      alert("कृपया राजस्व ग्राम चुनें...!");
                      ddlUserVillage.focus();
                      return false;
                  }
              }


              //debugger;
              if (ddlUserAreatype.selectedIndex == 2) {
                  var ddlUserWard = document.getElementById('<%=ddlUserWard.ClientID%>');
                  if (ddlUserWard.selectedIndex == 0) {
                      alert("कृपया वार्ड चुनें...!");
                      ddlUserWard.focus();
                      return false;
                  }
              }


              var txtUserMohalla = document.getElementById('<%=txtUserMohalla.ClientID%>');
               <%--if (txtUserMohalla != null)
              {
                  if (txtUserMohalla.value.trim() == "") {
                      alert("कृपया मोहल्ला संख्या अंकित करें...!");
                      txtUserMohalla.focus();
                      return false;
                  }
              }--%>


       <%-- var txtUserVillage_Anya = document.getElementById('<%=txtUserVillage_Anya.ClientID%>');
        if (txtUserVillage_Anya != null) {
            if (txtUserVillage_Anya.value.trim() == "") {
                alert("कृपया अन्य ग्राम अंकित करें...!");
                txtUserVillage_Anya.focus();
                return false;
            }
        }--%>

        <%--var txtUserWard_Anya = document.getElementById('<%=txtUserWard_Anya.ClientID%>');
        if (txtUserWard_Anya != null) {
            if (txtUserWard_Anya.value.trim() == "") {
                alert("कृपया अन्य वार्ड अंकित करें...!");
                txtUserWard_Anya.focus();
                return false;
            }
        }--%>

              var txtvadimobile = document.getElementById('<%=txtvadimobile.ClientID%>');
              if (txtvadimobile != null) {
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
                var ddlWsanshaanya_naam = document.getElementById('<%=ddlWsanshaanya_naam.ClientID%>');
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

    </script>
    <script type="text/javascript">
        function ValidationBhumiVivad() {
            debugger;

            var ddlDistrict = document.getElementById('<%=ddlDistrict.ClientID%>');
              if (ddlDistrict.options[ddlDistrict.selectedIndex].value == 0) {
                  alert("कृपया जिला चुनें...!");
                  ddlDistrict.focus();
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
                  alert("कृपया भूमि के विवाद का प्रकार चुनें...!"); ge
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

    </script>
    <script type="text/javascript">
        function isNumberdecimalKey(evt, obj) {
            // debugger
            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function isNumberwithoperator(evt, obj) {
            debugger
            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;

            var dotcontainsSmallclose = value.indexOf(")") != -1;
            if (dotcontainsSmallclose)
                if (charCode == 41) return false;
            if (charCode == 41) return true;

            var dotcontainsSmallOpen = value.indexOf("(") != -1;
            if (dotcontainsSmallOpen)
                if (charCode == 40) return false;
            if (charCode == 40) return true;

            var dotcontainshyphen = value.indexOf("-") != -1;
            if (dotcontainshyphen)
                if (charCode == 45) return false;
            if (charCode == 45) return true;

            var dotcontainsforwardslash = value.indexOf("/") != -1;
            if (dotcontainsforwardslash)
                if (charCode == 47) return false;
            if (charCode == 47) return true;

            if (charCode >= 48 && charCode <= 57)
                return true;
            return false;
        }
        function isNumberSahana(evt, obj) {
            debugger;
            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;

            var dotcontainsSmallclose = value.indexOf(")") != -1;
            if (dotcontainsSmallclose)
                if (charCode == 41) return false;
            if (charCode == 41) return true;

            var dotcontainsSmallOpen = value.indexOf("(") != -1;
            if (dotcontainsSmallOpen)
                if (charCode == 40) return false;
            if (charCode == 40) return true;

            var dotcontainsforwardslash = value.indexOf("/") != -1;
            if (dotcontainsforwardslash)
                if (charCode == 47) return false;
            if (charCode == 47) return true;

            if (charCode >= 48 && charCode <= 57)
                return true;
            return false;
        }
    </script>
    <%--Jquerry calling--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_LinkBtnPreview").click(function () {
                debugger;
                //vadi preview
                if ($("#ctl00_ContentPlaceHolder1_step1").is(":visible") == true) {
                    if ($("#ctl00_ContentPlaceHolder1_hfwadiprint").val() == "Printstep1") {
                        if (ValidationBhumiVivad() == true) {
                            $("#ctl00_ContentPlaceHolder1_wadi_grid th:first-child").hide();
                            $("#ctl00_ContentPlaceHolder1_wadi_grid td:first-child").hide();
                            $("#prviewwadi").html($("#ctl00_ContentPlaceHolder1_wadi_grid_div").html());

                            var DistrictText = $('#<%=ddlDistrict.ClientID%>').find('option:selected').text();
                            $('#<%=div_PreViewVadi_district.ClientID%>').text(DistrictText);

                            var subdivisionText = $('#<%=ddlSubdivision.ClientID%>').find('option:selected').text();
                            $('#<%=div_PreViewVadi_Sub_division.ClientID%>').text(subdivisionText);

                            var blockText = $('#<%=ddlBlock.ClientID%>').find("option:selected").text();
                            $('#<%=div_PreViewVadiBlock.ClientID%>').text(blockText);

                            var PoliceText = $('#<%=ddlPolice.ClientID%>').find("option:selected").text();
                            $('#<%=div_PreViewThana.ClientID%>').text(PoliceText);

                            var Areatypetext = $('#<%=ddlareatype.ClientID%>').find("option:selected").text();
                            $('#<%=div_PreViewVadi_Area.ClientID%>').text(Areatypetext);

                            if ($('#<%=ddlareatype.ClientID%>').find("option:selected").val() == "R") {
                                $('#<%=div_previewvadipanchayt.ClientID%>').text("ग्राम पंचायत");

                                $('#<%=div_PreviewVadi_Svarajaya_Label.ClientID%>').show();
                                $('#<%=div_PreViewVadi_Svarajaya.ClientID%>').show();
                                var Villagetext = $('#<%=ddlVillage.ClientID%>').find("option:selected").text();
                                $('#<%=div_PreViewVadi_Svarajaya.ClientID%>').text(Villagetext);
                            }
                            else {
                                $('#<%=div_PreviewVadi_Svarajaya_Label.ClientID%>').hide();
                                $('#<%=div_PreViewVadi_Svarajaya.ClientID%>').hide();
                                $('#<%=div_previewvadipanchayt.ClientID%>').text("नगर निकाय");
                            }
                            var Panchyattext = $('#<%=ddlPanchyat.ClientID%>').find("option:selected").text();
                            $('#<%=div_PreViewVadi_GramPanchayat_GramNikaya.ClientID%>').text(Panchyattext);

                            var Wardtext = $('#<%=ddlWard.ClientID%>').find("option:selected").text();
                            $('#<%=div_PreViewVadi_Ward.ClientID%>').text(Wardtext);

                            var vivad_adyatan_sthiti_text = $('#<%=ddl_vivad_adyatan_sthiti.ClientID%>').find("option:selected").text();
                            $('#<%=div_Preview_vadi_Vivad_Ka_Vighatan.ClientID%>').text(vivad_adyatan_sthiti_text);

                            var Rajaswa_sankhyaText = $('#<%=txtrajaswa_sankhya.ClientID%>').val();
                            $('#<%=div_PreView_vadi_rajashv_sankhaya.ClientID%>').text(Rajaswa_sankhyaText);

                            var bhumitypeText = $('#<%=ddlbhumitype.ClientID%>').find("option:selected").text();
                            $('#<%=div_PreviewVadi_BhumiKaPrakar.ClientID%>').text(bhumitypeText);

                            if ($('#<%=ddlbhumitype.ClientID%>').find("option:selected").val() == 2) {
                                $('#<%=div_Preview_vadi_sarkari_bhumi_ka_prakar_Label.ClientID%>').show();
                                $('#<%=div_Preview_vadi_sarkari_bhumi_ka_prakar.ClientID%>').show();

                                var sarkaribhumitypetext = $('#<%=ddlsarkaribhumitype.ClientID%>').find("option:selected").text();
                                $('#<%=div_Preview_vadi_sarkari_bhumi_ka_prakar.ClientID%>').text(sarkaribhumitypetext);

                                if ($('#<%=ddlsarkaribhumitype.ClientID%>').find("option:selected").val() == 6) {
                                    $('#<%=div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label.ClientID%>').show();
                                    $('#<%=div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya.ClientID%>').show();

                                    var Text_sarkaribhumitype_Anya = $('#<%=txtsarkaribhumitype_Anya.ClientID%>').val();
                                    $('#<%=div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya.ClientID%>').text(Text_sarkaribhumitype_Anya);
                                }
                                else {
                                    $('#<%=div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label.ClientID%>').hide();
                                    $('#<%=div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya.ClientID%>').hide();
                                }
                            }
                            else {
                                $('#<%=div_Preview_vadi_sarkari_bhumi_ka_prakar_Label.ClientID%>').hide();
                                $('#<%=div_Preview_vadi_sarkari_bhumi_ka_prakar.ClientID%>').hide();
                                $('#<%=div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label.ClientID%>').hide();
                                $('#<%=div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya.ClientID%>').hide();
                            }

                            var bhumivivadtypeText = $('#<%=ddlbhumivivadtype.ClientID%>').find("option:selected").text();
                            $('#<%=div_PreViewVadi_BhumiKa_VivadPrakar.ClientID%>').text(bhumivivadtypeText);

                            if ($('#<%=ddlbhumivivadtype.ClientID%>').find("option:selected").val() == 20) {
                                $('#<%=div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label.ClientID%>').show();
                                $('#<%=div_Preview_vadi_Bhumivivad_Prakar_Anaya.ClientID%>').show();

                                var Textbhumivivad_Anya = $('#<%=txtbhumivivad_Anya.ClientID%>').val();
                                $('#<%=div_Preview_vadi_Bhumivivad_Prakar_Anaya.ClientID%>').text(Textbhumivivad_Anya);
                            }
                            else {
                                $('#<%=div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label.ClientID%>').hide();
                                $('#<%=div_Preview_vadi_Bhumivivad_Prakar_Anaya.ClientID%>').hide();
                            }
                            $('#<%=lblPreview_vadi_date.ClientID%>').text($('#<%=txtAwadenKiTithi.ClientID%>').val());

                            var VadiVivaraniText = $('#<%=txtVadiVivarani.ClientID%>').val();
                            $('#<%=divVadiKabhumiVivaran.ClientID%>').text(VadiVivaraniText);

                            var PrativadiVivaraniText = $('#<%=txtPrativadiVivarani.ClientID%>').val();
                            $('#<%=divPrativadiKabhumiVivaran.ClientID%>').text(PrativadiVivaraniText);

                            //$("#prviewwadi").html($("#ctl00_ContentPlaceHolder1_wadi_grid").html());                      
                            $('#step_first_preview').modal('show');
                        }
                    }
                    else {
                        alert("कृपया वादी जोड़ें ..!");
                    }

                }
                //prativadi preview

                if ($('#<%=step2.ClientID%>').is(":visible") == true) {
                    var countGridViewRow = $('#<%=pratiWadi_grid.ClientID%>').length;
                    if (countGridViewRow > 0) {
                        if (AnyaVivaran() == true) {
                            $("#<%=pratiWadi_grid.ClientID%> th:first-child").hide();
                            $("#<%=pratiWadi_grid.ClientID%> td:first-child").hide();
                            $('#<%=divpreviewPratiwadi.ClientID%>').html($('#<%=panel_pratiWadi_grid.ClientID%>').html());

                            var ddlwadi_pratiwadi_sunwaiText = $("#<%=ddlwadi_pratiwadi_sunwai.ClientID%>").find("option:selected").text();
                            $("#<%=divprativadi_ka_suchit.ClientID%>").text(ddlwadi_pratiwadi_sunwaiText);
                            if ($("#<%=ddlwadi_pratiwadi_sunwai.ClientID%>").find("option:selected").val() == "N") {
                                $("#<%=divprativadi_ka_Karan_Label.ClientID%>").show();
                                $("#<%=divprativadi_ka_Karan.ClientID%>").show();

                                $("#<%=divprativadi_ka_madham_Label.ClientID%>").hide();
                                $("#<%=divprativadi_ka_madham.ClientID%>").hide();

                                $("#<%=divprativadi_ka_Suchna_Label.ClientID%>").hide();
                                $("#<%=divprativadi_ka_Suchna.ClientID%>").hide();

                                $("#<%=divprativadi_ka_Upashtith_Label.ClientID%>").hide();
                                $("#<%=divprativadi_ka_Upashtith.ClientID%>").hide();

                                var txtsunwaiHetuNoticKaKaranText = $("#<%=txtsunwaiHetuNoticKaKaran.ClientID%>").val();
                                $("#<%=divprativadi_ka_Karan.ClientID%>").text(txtsunwaiHetuNoticKaKaranText);
                            }
                            else {
                                $("#<%=divprativadi_ka_Karan_Label.ClientID%>").hide();
                                $("#<%=divprativadi_ka_Karan.ClientID%>").hide();

                                $("#<%=divprativadi_ka_madham_Label.ClientID%>").show();
                                $("#<%=divprativadi_ka_madham.ClientID%>").show();
                                $("#<%=divprativadi_ka_Suchna_Label.ClientID%>").show();
                                $("#<%=divprativadi_ka_Suchna.ClientID%>").show();
                                $("#<%=divprativadi_ka_Upashtith_Label.ClientID%>").show();
                                $("#<%=divprativadi_ka_Upashtith.ClientID%>").show();

                                var ddlKiskeduwara_bhejagayaText = $("#<%=ddlKiskeduwara_bhejagaya.ClientID%>").find("option:selected").text();
                                $("#<%=divprativadi_ka_madham.ClientID%>").text(ddlKiskeduwara_bhejagayaText);

                                var ddlSuchana_ka_tamilaText = $("#<%=ddlSuchana_ka_tamila.ClientID%>").find("option:selected").text();
                                $("#<%=divprativadi_ka_Suchna.ClientID%>").text(ddlSuchana_ka_tamilaText);

                                var ddlSuchana_ka_upasthitiText = $("#<%=ddlSuchana_ka_upasthiti.ClientID%>").find("option:selected").text();
                                $("#<%=divprativadi_ka_Upashtith.ClientID%>").text(ddlSuchana_ka_upasthitiText);
                            }

                            $('#<%=lblPreViewPrativadiAppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());;
                            $('#<%=lblPreview_Prativadi_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                            $('#step_second_preview').modal('show');
                        }
                    }
                    else {
                        alert("कृपया प्रतिवादी जोड़ें ..!");
                    }
                }
                // Preview भूमि का खाता-खेसरा का विवरण
                if ($('#<%=step3.ClientID%>').is(":visible") == true) {
                    var countGridViewRow = $('#<%=grd_bhumivivad.ClientID%>').length;
                    if (countGridViewRow > 0) {
                        $("#<%=grd_bhumivivad.ClientID%> th:first-child").hide();
                        $("#<%=grd_bhumivivad.ClientID%> td:first-child").hide();
                        $('#<%=divbhumikhata_shekher_ka_vivaran.ClientID%>').html($('#<%=Panelgrd_bhumivivad.ClientID%>').html());
                        $('#<%=lblPreView_setpthird_AppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());;
                        $('#<%=lblPreview_setpthird_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                        $('#step_third_preview').modal('show');
                    }
                    else {
                        alert("कृपया भूमि का खाता-खेसरा जोड़ें ..!");
                    }
                }
                // Preview वादी एवं प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण step4
                if ($('#<%=step4.ClientID%>').is(":visible") == true) {
                    var countGridViewRow1 = $('#<%=gdVadiEvidence.ClientID%>').length;
                    var countGridViewRow2 = $('#<%=gdPrativadiEvidence.ClientID%>').length;
                    if (countGridViewRow1 > 0 && countGridViewRow2 > 0) {
                        //वादी द्वारा प्रस्तुत साक्ष्य का विवरण
                        var IsVadiEviText = $('#<%=ddlIsVadiEvi.ClientID%>').find('option:selected').text();
                        $('#<%=divVadi_dvaara_saakshy_ka_dastaavej_upalabdh.ClientID%>').text(IsVadiEviText);

                        $("#<%=gdVadiEvidence.ClientID%> th:nth-child(3)").hide();
                        $("#<%=gdVadiEvidence.ClientID%> td:nth-child(3)").hide();
                        $("#<%=gdVadiEvidence.ClientID%> th:nth-child(4)").hide();
                        $("#<%=gdVadiEvidence.ClientID%> td:nth-child(4)").hide();
                        $('#<%=divVadi_ka_Saakshy.ClientID%>').html($('#<%=PanelgdVadiEvidence.ClientID%>').html());

                        //प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण
                        var IsPvadiEvi = $('#<%=ddlIsPvadiEvi.ClientID%>').find('option:selected').text();
                        $('#<%=divPrativadi_dvaara_saakshy_ka_dastaavej_upalabdh.ClientID%>').text(IsPvadiEvi);

                        $("#<%=gdPrativadiEvidence.ClientID%> th:nth-child(3)").hide();
                        $("#<%=gdPrativadiEvidence.ClientID%> td:nth-child(3)").hide();
                        $("#<%=gdPrativadiEvidence.ClientID%> th:nth-child(4)").hide();
                        $("#<%=gdPrativadiEvidence.ClientID%> td:nth-child(4)").hide();
                        $('#<%=divPrativadi_ka_Saakshy.ClientID%>').html($('#<%=PanelgdPrativadiEvidence.ClientID%>').html());


                        $('#<%=lblPreView_setpfour_AppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());;
                        $('#<%=lblPreview_setpfour_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                        $('#step_fourth_preview').modal('show');
                    }
                    else {
                        alert("कृपया वादी द्वारा प्रस्तुत साक्ष्य का विवरण या प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण जोड़ें ..!");
                    }
                }
                // Preview वादी एवं प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण step5
                if ($('#<%=step5.ClientID%>').is(":visible") == true) {
                    if (RajAdhikariPoliceAdhikari() == true) {
                        var pulis_padadhikari_vivaraniText = $('#<%=txtpulis_padadhikari_vivarani.ClientID%>').val();
                                $('#<%=divPoliceAdhikari.ClientID%>').text(pulis_padadhikari_vivaraniText);

                                var halkakarmchari_prativedanText = $('#<%=txthalkakarmchari_prativedan.ClientID%>').val();
                                $('#<%=divHalkaKarmchariValue.ClientID%>').text(halkakarmchari_prativedanText);

                                var bhukhand_mapiText = $('#<%=ddlbhukhand_mapi.ClientID%>').find('option:selected').text();
                                $('#<%=divVivaditBhukandValue.ClientID%>').text(bhukhand_mapiText);

                                if ($('#<%=ddlbhukhand_mapi.ClientID%>').find('option:selected').val() == "Y") {

                                    $("#<%=divMapi.ClientID%>").show();

                                var bhukhand_CopyText = $('#<%= ddlbhukhand_Copy.ClientID%>').find('option:selected').text();
                                $('#<%=divMapiValue.ClientID%>').text(bhukhand_CopyText);

                                if ($('#<%= ddlbhukhand_Copy.ClientID%>').find('option:selected').val() == "N") {
                                    $("#<%=divVivaditBhukandKaMapi.ClientID%>").show();
                                    $("#<%=MapiKeNirdharnKiThithi.ClientID%>").show();

                                    var halkakarmchari_prativedanText = $('#<%=txtbhukhand_reason.ClientID%>').val();
                                    $('#<%=divVivaditBhukandKaMapiValue.ClientID%>').text(halkakarmchari_prativedanText);

                                    var MapiKeNirdharit_tithiText = $('#<%=txtMapiKeNirdharit_tithi.ClientID%>').val();
                                    $('#<%=divMapiKeNirdharnKiThithiValue.ClientID%>').text(MapiKeNirdharit_tithiText);

                                }
                                else {
                                    $("#<%=divVivaditBhukandKaMapi.ClientID%>").hide();
                                    $("#<%=MapiKeNirdharnKiThithi.ClientID%>").hide();
                                }
                            }
                            else {
                                $("#<%=divMapi.ClientID%>").hide();
                                $("#<%=divVivaditBhukandKaMapi.ClientID%>").hide();
                                $("#<%=MapiKeNirdharnKiThithi.ClientID%>").hide();
                                }
                                $('#<%=lblPreView_setpfive_AppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());;
                                $('#<%=lblPreview_setpfive_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                        $('#step_fith_preview').modal('show');
                    }
                }
                //Preview  step6
                if ($('#<%=step6.ClientID%>').is(":visible") == true) {
                    var countGridViewRow1 = $('#<%=grdbhumivivad.ClientID%>').length;
                    var countGridViewRow2 = $('#<%=grdnyayalay_vivran.ClientID%>').length;
                    if (countGridViewRow1 > 0 && countGridViewRow2 > 0) {
                        if ($('#<%=dd_IsBhumiVivad.ClientID%>').find('option:selected').val() != 0 ||
                            $('#<%=ddl_Isbhumi_Viviad_available.ClientID%>').find('option:selected').val() != 0) {
                            var IsBhumiVivadText = $('#<%=dd_IsBhumiVivad.ClientID%>').find('option:selected').text();
                            $('#<%=div_Prathamik.ClientID%>').text(IsBhumiVivadText);

                            var Isbhumi_Viviad_availableText = $('#<%=ddl_Isbhumi_Viviad_available.ClientID%>').find('option:selected').text();
                            $('#<%=divPrakiriyaVad.ClientID%>').text(Isbhumi_Viviad_availableText);

                            var countGridViewRow1 = $('#<%=grdbhumivivad.ClientID%>').length;
                            var countGridViewRow2 = $('#<%=grdnyayalay_vivran.ClientID%>').length;
                            if (countGridViewRow1 > 0 && countGridViewRow2 > 0) {

                                $("#<%=grdbhumivivad.ClientID%> th:nth-child(1)").hide();
                                $("#<%=grdbhumivivad.ClientID%> td:nth-child(1)").hide();

                                $('#<%=divbhumivivadPrivewGrid.ClientID%>').html($('#<%=Panelgrdbhumivivad.ClientID%>').html());

                                $("#<%=grdnyayalay_vivran.ClientID%> th:nth-child(1)").hide();
                                $("#<%=grdnyayalay_vivran.ClientID%> td:nth-child(1)").hide();

                                $('#<%=divNyaayaalayPrivewGrid.ClientID%>').html($('#<%=Panelgrdnyayalay_vivran.ClientID%>').html());
                                $('#step_six_preview').modal('show');
                            }
                            $('#<%=lblPreView_setpsix_AppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());;
                            $('#<%=lblPreview_setpsix_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                            $('#step_six_preview').modal('show');
                        }
                    }
                    else {
                        alert(" कृपया प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ?  या न्यायालय में प्रक्रियाधीन वाद का विवरण जोड़ें ..!");
                    }
                }
                //Preview  step7
                if ($('#<%=step7.ClientID%>').is(":visible") == true) {
                    if (validationStep7() == true) {
                        var bhumivivadki_sanvedanshiltaText = $('#<%=ddlbhumivivadki_sanvedanshilta.ClientID%>').find('option:selected').text();
                        $('#<%=divVivaadKiSanvedanasheelata.ClientID%>').text(bhumivivadki_sanvedanshiltaText);

                        var baithakDateText = $('#<%=txtbaithakDate.ClientID%>').val();
                        $('#<%=divBaithakKiTithi.ClientID%>').text(baithakDateText);

                        var ddlIsVadiAvailableText = $('#<%=ddlIsVadiAvailable.ClientID%>').find('option:selected').text();
                        $('#<%=divkyaVaadeeUpasthitHai.ClientID%>').text(ddlIsVadiAvailableText);

                        var ddl_IsprativadiAvailableText = $('#<%=ddl_IsprativadiAvailable.ClientID%>').find('option:selected').text();
                        $('#<%=divKyaPrativaadeeUpasthitHai.ClientID%>').text(ddl_IsprativadiAvailableText);

                        var ddlactionText = $('#<%=ddlaction.ClientID%>').find('option:selected').text();
                        $('#<%=divBaithakKaNishkarsh .ClientID%>').text(ddlactionText);

                        if ($('#<%=ddlaction.ClientID%>').find('option:selected').val() == 4) {
                            $('#<%=divAsveekrtiKaKaaranLabel.ClientID%>').show();
                        $('#<%=divAsveekrtiKaKaaran.ClientID%>').show();
                        $('#<%=divtithi.ClientID%>').hide();
                        $('#<%=divtithivalue.ClientID%>').hide();

                        var CancelReasonText = $('#<%=txtCancelReason.ClientID%>').val();
                        $('#<%=divAsveekrtiKaKaaran.ClientID%>').text(CancelReasonText);
                    }
                    else {
                        $('#<%=divAsveekrtiKaKaaranLabel.ClientID%>').hide();
                        $('#<%=divAsveekrtiKaKaaran.ClientID%>').hide();
                        $('#<%=divtithi.ClientID%>').show();
                        $('#<%=divtithivalue.ClientID%>').show();

                        var labNextDateText = $('#<%=labNextDate.ClientID%>').text();
                        $('#<%=divtithi.ClientID%>').text(labNextDateText);

                        var AgalaDateText = $('#<%=txtAgalaDate.ClientID%>').val();
                        $('#<%=divtithivalue.ClientID%>').text(AgalaDateText);

                        }
                        var falafalText = $('#<%=txtfalafal.ClientID%>').val();
                        $('#<%=divBaithakMeinLiyaGayaNirnay.ClientID%>').text(falafalText);

                        var abhiyukt_anchaladhikariText = $('#<%=txtabhiyukt_anchaladhikari.ClientID%>').val();
                        $('#<%=divAnchalaadhikaareeKaMantavy.ClientID%>').text(abhiyukt_anchaladhikariText);

                        var abhiyukt_thaanprabhaareeText = $('#<%=txtabhiyukt_thaanprabhaaree.ClientID%>').val();
                        $('#<%=divThaanaadhyakshKaMantavy.ClientID%>').text(abhiyukt_thaanprabhaareeText);
                        $('#<%=lblPreView_setpseven_AppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());;
                        $('#<%=lblPreview_setptseven_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                        $('#step_seven_preview').modal('show');
                    }
                }
                return false;
            });

                //Preview vadi click button event step1
                $("#btnclosedialogVadi").click(function () {
                    if ($("#ctl00_ContentPlaceHolder1_step1").is(":visible") == true) {
                        if ($("#ctl00_ContentPlaceHolder1_hfwadiprint").val() == "Printstep1") {
                            $("#ctl00_ContentPlaceHolder1_wadi_grid th:first-child").show();
                            $("#ctl00_ContentPlaceHolder1_wadi_grid td:first-child").show();
                        }
                    }

                });

                //preview btnclosedialogPrativadi click step2

                $("#<%=btnclosedialogPrativadi.ClientID%>").click(function () {
                    $("#<%=pratiWadi_grid.ClientID%> th:first-child").show();
                $("#<%=pratiWadi_grid.ClientID%> td:first-child").show();
            });

                //Preview भूमि का खाता-खेसरा  click button event step3
                $("#<%=btnbhumikhata_shekher_ka_vivaran.ClientID%>").click(function () {
                    $("#<%=grd_bhumivivad.ClientID%> th:first-child").show();
                 $("#<%=grd_bhumivivad.ClientID%> td:first-child").show();
             });

                //Preview वादी एवं प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण click button event step4
                $("#<%=btnstep_fourth_preview.ClientID%>").click(function () {
                    $("#<%=gdVadiEvidence.ClientID%> th:nth-child(3)").show();
                 $("#<%=gdVadiEvidence.ClientID%> td:nth-child(3)").show();
                 $("#<%=gdVadiEvidence.ClientID%> th:nth-child(4)").show();
                 $("#<%=gdVadiEvidence.ClientID%> td:nth-child(4)").show();

                 $("#<%=gdPrativadiEvidence.ClientID%> th:nth-child(3)").show();
                 $("#<%=gdPrativadiEvidence.ClientID%> td:nth-child(3)").show();
                 $("#<%=gdPrativadiEvidence.ClientID%> th:nth-child(4)").show();
                 $("#<%=gdPrativadiEvidence.ClientID%> td:nth-child(4)").show();
             });
                //Preview  step6 click
                $("#<%=btnstep_six_preview.ClientID%>").click(function () {
                    $("#<%=grdbhumivivad.ClientID%> th:nth-child(1)").show();
                 $("#<%=grdbhumivivad.ClientID%> td:nth-child(1)").show();

                 $("#<%=grdnyayalay_vivran.ClientID%> th:nth-child(1)").show();
                 $("#<%=grdnyayalay_vivran.ClientID%> td:nth-child(1)").show();
             });
            });
    </script>
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
            urlpdf = ("http://localhost:8080" + urlpdf);
            urlpdf = urlpdf.trim();

            $.ajax({
                type: "POST",
                url: "Entry_Page.aspx/Getpdf",
                data: "{'url':'" + urlpdf + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    // inlineFrameExample.src = response.d;
                    window.open("../IDoc.aspx?url=" + response.d, "_blank");
                },
                failure: function (msg) {
                    alert(msg);
                }
            });
            //var width = document.body.clientWidth;
            //imgDiv.style.left = (width - 1200) / 2 + "px";
            //imgDiv.style.top = "10px";
            //imgDiv.style.display = "block";
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
    <style type="text/css">
        .rightpadding {
            padding-right: 700px !important;
        }
    </style>
    <style type="text/css">
        .ActiveClass {
            color: darkblue;
            padding: 5px;
            border-radius: 5px;
            text-decoration: none;
            font-weight: 500;
            background: linear-gradient(to right, #1B990E 0%, #B013CF 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
        }

        .RemoveClass {
            color: lightgray;
            padding: 5px;
            border-radius: 5px;
            text-decoration: none;
            font-weight: 500;
        }
    </style>
    <style>
        div.Break_Word_next_Line {
            width: 600px !important;
            word-wrap: break-word !important;
        }

        hr {
            margin-top: 0px;
            margin-bottom: 0px;
            color: yellow;
        }

        .labeltextsize {
            padding: 5px;
        }

        .myfont1 {
            font-size: unset !important;
        }
    </style>
    <style type="text/css">
        .Background {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .Popup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 400px;
            height: 350px;
        }

        .lbl {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
    <%--css for print--%>
    <style type="text/css" media="screen">
        .btnvadiprint {
            color: #686464 !important;
            opacity: inherit !important;
            background: #66bb6a !important;
            font-size: unset !important;
        }

        #divImage {
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></AjaxControlToolkit:ToolkitScriptManager>
    <div class="container-fluid">
        <h4 class="text-black text-center"><b>आवेदन का विवरण</b></h4>
        <%-- step tabs: hidden for ADMHOME login --%>
        <div class="card mb-3" id="divStepTabs" runat="server">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <center>
                            <a href="#" id="hstep1" runat="server" class="ActiveClass">वादी और भूमि विवाद&nbsp;<i id="hIstep1" runat="server" class="fa fa-circle-arrow-right" style="color: darkblue"></i></a> &nbsp;&nbsp; 
                                            <a href="#" id="hstep2" runat="server" class="RemoveClass">प्रतिवादी और अन्य&nbsp;<i id="hIstep2" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a> &nbsp;&nbsp;
                                            <a href="#" id="hstep3" runat="server" class="RemoveClass">खाता-खेसरा&nbsp;<i id="hIstep3" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                                            <a href="#" id="hstep4" runat="server" class="RemoveClass">वादी और प्रतिवादी का साक्ष्य&nbsp;<i id="hIstep4" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                                            <a href="#" id="hstep5" runat="server" class="RemoveClass">प्रस्तुत साक्ष्य&nbsp;<i id="hIstep5" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                                            <a href="#" id="hstep6" runat="server" class="RemoveClass">घटना-वारदात और न्यायलय&nbsp;<i id="hIstep6" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                                            <a href="#" id="hstep7" runat="server" class="RemoveClass">अंचलाधिकारी और थानाध्यक्ष की बैठक&nbsp;<i id="hIstep7" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <center>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
            </center>
        </div>
        <div class="row">
            <div class="col-3">
                <asp:HiddenField ID="pathfile" runat="server" />
                <asp:HiddenField runat="server" ID="hfGetdateAvadan" />
                <asp:HiddenField runat="server" ID="hfGetApplicationno" />
            </div>
        </div>
        <div id="step1" runat="server" visible="true">
            <div class="card">

                <div class="card-header text-center" style="font-size: 18px"><b><u>वादी का विवरण</u></b></div>
                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%-- for ADMHOME login the mobile column is moved into this row (see OnInit) --%>
                            <div class="row mb-2" id="rowVadiBasic" runat="server">
                                <div class="col-md-3">
                                    <asp:Label ID="Label2" runat="server" Text="वादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtNamePerAadhaar" runat="server" CssClass="form-control mb-2"
                                        placeholder="वादी का नाम" oncopy="return false" onpaste="return false" AutoComplete="off"
                                        oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                    <asp:Label ID="DtxtNamePerAadhaar" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Enter वादी का नाम..."
                                        ControlToValidate="txtNamePerAadhaar" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label4" runat="server" Text="पिता/ पति का नाम"></asp:Label>&nbsp;<img id="starFName" runat="server" src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtFName" runat="server" CssClass="form-control mb-2" placeholder="पिता/ पति का नाम" oncopy="return false" onpaste="return false"
                                        oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter पिता/ पति का नाम..."
                                        ControlToValidate="txtFName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label1" runat="server" Text="लिंग चुने"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlgender" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="0" Text="--Select--" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Value="M" Text="Male" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Value="F" Text="Female" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Value="O" Text="Other" Enabled="true"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Dddlgender" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Select लिंग चुने..."
                                        ControlToValidate="ddlgender" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <%-- birth year: hidden for ADMHOME login --%>
                                <div class="col-md-3" id="divVadiBirthYear" runat="server">
                                    <asp:Label ID="Label3" runat="server" Text="जन्म का वर्ष"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control mb-2">
                                    </asp:DropDownList>
                                    <asp:Label ID="Dtxtdatebirth" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label5" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlUserDist" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlUserDist_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select जिला..."
                                        ControlToValidate="ddlUserDist" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label6" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlUserSubdivision" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlUserSubdivision_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select अनुमंडल..."
                                        ControlToValidate="ddlUserSubdivision" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label7" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlUserBlock" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlUserBlock_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select अंचल..."
                                        ControlToValidate="ddlUserBlock" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label8" runat="server" Text="थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlUserThana" runat="server" CssClass="form-control mb-2" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select थाना..."
                                        ControlToValidate="ddlUserThana" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label9" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>&nbsp;<img id="starUserAreatype" runat="server" src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlUserAreatype" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlUserAreatype_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--Select--" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Value="R" Text="Rural" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Value="U" Text="Urban" Enabled="true"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select क्षेत्र का प्रकार..."
                                        ControlToValidate="ddlUserAreatype" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divUserPanchyat" runat="server">
                                    <asp:Label ID="labUVillage" runat="server" Text="ग्राम पंचायत"></asp:Label>&nbsp;<img id="starUserPanchyat" runat="server" src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlUserPanchyat" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlUserPanchyat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select ग्राम पंचायत..."
                                        ControlToValidate="ddlUserPanchyat" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div id="divUserPanchyat_Anya" runat="server" class="col-md-3" visible="false">
                                    पंचायत(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                    <asp:TextBox runat="server" ID="txtUserPanchyat_Anya" MaxLength="100" CssClass="form-control" placeholder="पंचायत(अगर अन्य है)"></asp:TextBox>
                                </div>
                                <div class="col-md-3" id="divUserVillageCol" runat="server">
                                    <div class="row mb-2">
                                        <div class="col-md-12" id="divUserVillage" runat="server">
                                            <asp:Label ID="Label11" runat="server" Text="राजस्व ग्राम"></asp:Label>&nbsp;<img id="starUserVillage" runat="server" src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlUserVillage" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlUserVillage_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select राजस्व ग्राम..."
                                                ControlToValidate="ddlUserVillage" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                                InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div id="divUserVillage_Anya" runat="server" class="col-md-3" visible="false" style="font-weight: bolder; padding-top: 5px;">
                                    ग्राम(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                    <asp:TextBox runat="server" ID="txtUserVillage_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3" id="divUserWard" runat="server">
                                    <asp:Label ID="lbluserward" runat="server" Text="वार्ड"></asp:Label>&nbsp; <span id="UWard" runat="server" visible="true">
                                        <img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    </span>
                                    <asp:DropDownList ID="ddlUserWard" runat="server" CssClass="form-control mb-2" OnSelectedIndexChanged="ddlUserWard_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div id="divUserWard_Anya" runat="server" class="col-md-3" visible="false">
                                    वार्ड(अगर अन्य है)
                                                                        <asp:TextBox runat="server" ID="txtUserWard_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3" id="divUserMohalla" runat="server" visible="false">
                                    <asp:Label ID="Label120" runat="server" Text="मोहल्ला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtUserMohalla" runat="server" CssClass="form-control" placeholder="मोहल्ला का नाम" oncopy="return false" onpaste="return false"
                                        oncut="return false" onkeyup="Upper(this)" MaxLength="30"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Enter मोहल्ला का नाम..."
                                        ControlToValidate="txtUserMohalla" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2" id="rowVadiMobile" runat="server">
                                <div class="col-md-3" id="divVadiMobile" runat="server">
                                    <asp:Label ID="Label14" runat="server" Text="मोबाइल नंबर"></asp:Label>&nbsp;<img id="starVadiMobile" runat="server" src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtvadimobile" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return ValidateMobile(event)" placeholder="मोबाइल नंबर"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtvadimobile" ID="RegularExpressionValidator3"
                                        ValidationExpression="^[\s\S]{10,10}$" runat="server" ValidationGroup="1" ErrorMessage="10 numbers required."></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Enter मोबाइल नंबर..."
                                        ControlToValidate="txtvadimobile" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>
                            </div>
                            <%-- department / organisation questions: hidden for ADMHOME login --%>
                            <asp:PlaceHolder ID="phVadiDeptOrg" runat="server">
                            <br />
                            <div class="row  text-white" style="background-color: dodgerblue">
                                <div class="col-md-3 p-1">
                                    <asp:Label ID="lblis_vadi_from_an_dept" runat="server" Text="क्या वादी किसी विभाग का प्रतिनिधि है?"></asp:Label>
                                    &nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddl_is_vadi_from_an_dept" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddl_is_vadi_from_an_dept_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--चुने--"></asp:ListItem>
                                        <asp:ListItem Value="Y" Text="हां"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="नहीं"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Select क्या वादी किसी विभाग का प्रतिनिधि है?..."
                                        ControlToValidate="ddl_is_vadi_from_an_dept" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divWVibhag_details" runat="server" visible="false">
                                    <asp:Label ID="Label16" runat="server" Text="विभाग का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlWvibhaag_naam" runat="server" CssClass="form-control mb-2" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Select विभाग का नाम..."
                                        ControlToValidate="ddlWvibhaag_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divWvibhaag_padanaam" runat="server" visible="false">
                                    <asp:Label ID="Label17" runat="server" Text="विभाग में पदनाम"></asp:Label>
                                    <asp:TextBox ID="txtWvibhaag_padanaam" runat="server" CssClass="form-control" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" placeholder="विभाग में पदनाम"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-2 text-white" style="background-color: dodgerblue">
                                <div class="col-md-12 p-1">
                                    <p class="p-1" style="background-color: dodgerblue"><b style="color: yellow;">नोट:</b> <u style="color: white; text-decoration: none;">यदि विभाग की कोई जमीन है तो उस स्थिति में वादी विभाग के प्रतिनिधि होंगे |</u> </p>
                                </div>
                            </div>
                            <div class="row  text-white" style="background-color: lightseagreen">
                                <div class="col-md-3 p-1">
                                    <asp:Label ID="Label18" runat="server" Text="क्या वादी किसी संस्था का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddl_is_vadi_from_an_org" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddl_is_vadi_from_an_org_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="--चुने--"></asp:ListItem>
                                        <asp:ListItem Value="Y" Text="हां"></asp:ListItem>
                                        <asp:ListItem Value="N" Text="नहीं"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Select क्या वादी किसी संस्था का प्रतिनिधि है?..."
                                        ControlToValidate="ddl_is_vadi_from_an_org" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div id="divWSanstha_details" runat="server" visible="false" class="row mb-2 text-white" style="background-color: lightseagreen">
                                <div class="col-md-3 mb-2 p-1">
                                    <asp:Label ID="Label19" runat="server" Text="संस्था का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlWsanstha_naam" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Select संस्था का प्रकार..."
                                        ControlToValidate="ddlWsanstha_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">
                                    <asp:Label ID="Label20" runat="server" Text="संस्था का सम्बन्ध"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlWsanshaanya_naam" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Select संस्था का सम्बन्ध..."
                                        ControlToValidate="ddlWsanshaanya_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">
                                    <asp:Label ID="Label21" runat="server" Text="संस्था का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtWsanstha_naam" runat="server" CssClass="form-control" placeholder="संस्था का नाम" oncopy="return false" onpaste="return false"
                                        oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Enter संस्था का नाम..."
                                        ControlToValidate="txtWsanstha_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">
                                    <asp:Label ID="lbl" runat="server" Text="संस्था में पदनाम"></asp:Label>
                                    <asp:TextBox ID="txtWsanstha_padanaam" runat="server" CssClass="form-control" AutoComplete="off" placeholder="संस्था में पदनाम"
                                        oncopy="return false" onpaste="return false" oncut="return false" onkeyup="Upper(this)"
                                        onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                </div>
                            </div>
                            </asp:PlaceHolder>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- pincode + document upload + remarks: shown only for ADMHOME login.
                         Kept outside the UpdatePanel so the selected file survives dropdown postbacks --%>
                    <asp:PlaceHolder ID="phADMHOMEExtra" runat="server" Visible="false">
                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="lblVadiPincode" runat="server" Text="पिनकोड"></asp:Label>
                                <asp:TextBox ID="txtVadiPincode" runat="server" CssClass="form-control" MaxLength="6" onkeypress="return ValidateMobile(event)" placeholder="6 अंकों का पिनकोड" AutoComplete="off"
                                    inputmode="numeric" oninput="onADMHOMEPincodeInput(this);" onblur="checkADMHOMEPincodeNow();"></asp:TextBox>
                                <%-- optional, but when entered it must be exactly 6 digits and not start with 0.
                                     The message below the box is set by validateADMHOMEPincode (live on blur, and on Save) --%>
                                <asp:CustomValidator ID="cvVadiPincode" runat="server" ControlToValidate="txtVadiPincode" ClientValidationFunction="validateADMHOMEPincode"
                                    ValidateEmptyText="false" Display="Dynamic" SetFocusOnError="true" ValidationGroup="ADMHOME" ForeColor="Red"
                                    ErrorMessage="पिनकोड ठीक 6 अंकों का होना चाहिए."></asp:CustomValidator>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblADMHOMEDoc" runat="server" Text="दस्तावेज़ अपलोड करें"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:FileUpload ID="fuADMHOMEDoc" runat="server" CssClass="form-control" accept=".pdf" onchange="return checkADMHOMEFileSize(this);" />
                                <small style="color: darkred">केवल .pdf, अधिकतम 5 MB</small>
                                <asp:RequiredFieldValidator ID="rfvADMHOMEDoc" runat="server" ControlToValidate="fuADMHOMEDoc"
                                    Display="Dynamic" SetFocusOnError="true" ValidationGroup="ADMHOME" ForeColor="Red" ErrorMessage="कृपया दस्तावेज़ अपलोड करें..."></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="lblADMHOMERemarks" runat="server" Text="टिप्पणी"></asp:Label>
                                <asp:TextBox ID="txtADMHOMERemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"
                                    placeholder="टिप्पणी लिखें (अधिकतम 500 शब्द)" oninput="limitADMHOMERemarksWords(this);" Style="resize: vertical"></asp:TextBox>
                                <div class="text-right"><small id="ADMHOMERemarksCount" class="text-muted">0 / 500 शब्द</small></div>
                            </div>
                        </div>
                        <%-- ADMHOME Save: adds the वादी to the temporary list below (full postback so the PDF is sent) --%>
                        <div class="row mb-2">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnAddVadiADMHOME" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="ADMHOME" OnClick="btnAddVadiADMHOME_Click" />
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-12" style="text-align: center">
                                <asp:Panel ID="pnlVadiADMHOMEGrid" runat="server" ScrollBars="Auto">
                                    <asp:GridView runat="server" Width="100%" ID="gvVadiADMHOME" AutoGenerateColumns="false"
                                        CssClass="table-responsive CSSTableGeneratorGrid fontsize" OnRowCommand="gvVadiADMHOME_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRowDelADMHOME" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove"
                                                        OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="vadi_Name" HeaderText="वादी का नाम" />
                                            <asp:BoundField DataField="Vadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" />
                                            <asp:BoundField DataField="gender" HeaderText="लिंग" />
                                            <asp:BoundField DataField="dist" HeaderText="जिला" />
                                            <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" />
                                            <asp:BoundField DataField="block" HeaderText="अंचल" />
                                            <asp:BoundField DataField="thana" HeaderText="थाना" />
                                            <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" />
                                            <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" />
                                            <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" />
                                            <asp:BoundField DataField="WardNo" HeaderText="वार्ड" />
                                            <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल संख्या" />
                                            <asp:BoundField DataField="Pincode" HeaderText="पिनकोड" />
                                            <asp:BoundField DataField="DocName" HeaderText="दस्तावेज़" />
                                            <asp:BoundField DataField="Remarks" HeaderText="टिप्पणी" ItemStyle-Width="200" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                        <script type="text/javascript">
                            // पिनकोड: Indian pincode = exactly 6 digits, first digit 1-9
                            function getADMHOMEPincodeError(value) {
                                if (value.length === 0) return "";
                                if (!/^[0-9]+$/.test(value)) return "पिनकोड में केवल अंक (0-9) होने चाहिए...";
                                if (value.length < 6) return "पिनकोड ठीक 6 अंकों का होना चाहिए (अभी " + value.length + " अंक हैं).";
                                if (value.length > 6) return "पिनकोड 6 अंकों से अधिक नहीं हो सकता...";
                                if (value.charAt(0) === "0") return "पिनकोड 0 से शुरू नहीं हो सकता...";
                                return "";
                            }

                            // CustomValidator client function (cvVadiPincode)
                            function validateADMHOMEPincode(sender, args) {
                                var msg = getADMHOMEPincodeError(args.Value.trim());
                                if (msg !== "") sender.innerHTML = msg;
                                args.IsValid = (msg === "");
                            }

                            function checkADMHOMEPincodeNow() {
                                var v = document.getElementById('<%= cvVadiPincode.ClientID %>');
                                if (v && typeof ValidatorValidate === "function") ValidatorValidate(v);
                            }

                            // keep only digits (also for pasted text) and at most 6 of them;
                            // re-check while typing only if an error is already shown, so it clears as soon as it is fixed
                            function onADMHOMEPincodeInput(box) {
                                var clean = box.value.replace(/[^0-9]/g, "").substring(0, 6);
                                if (box.value !== clean) box.value = clean;
                                var v = document.getElementById('<%= cvVadiPincode.ClientID %>');
                                if (v && v.style.display !== "none" && v.style.visibility !== "hidden") checkADMHOMEPincodeNow();
                            }

                            var ADMHOME_MAX_FILE_BYTES = 5 * 1024 * 1024;
                            var ADMHOME_MAX_REMARK_WORDS = 500;

                            function checkADMHOMEFileSize(input) {
                                if (input.files && input.files.length > 0) {
                                    var file = input.files[0];
                                    if (!/\.pdf$/i.test(file.name)) {
                                        alert("कृपया केवल .pdf फाइल अपलोड करें...!");
                                        input.value = "";
                                        return false;
                                    }
                                    if (file.size > ADMHOME_MAX_FILE_BYTES) {
                                        alert("फाइल का आकार 5 MB से अधिक नहीं होना चाहिए...!");
                                        input.value = "";
                                        return false;
                                    }
                                }
                                return true;
                            }

                            function limitADMHOMERemarksWords(box) {
                                var words = box.value.trim().split(/\s+/).filter(function (w) { return w.length > 0; });
                                if (words.length > ADMHOME_MAX_REMARK_WORDS) {
                                    // cut back to the first 500 words
                                    var re = new RegExp("^\\s*(\\S+\\s+){" + (ADMHOME_MAX_REMARK_WORDS - 1) + "}\\S+");
                                    var m = box.value.match(re);
                                    if (m) box.value = m[0];
                                    words = words.slice(0, ADMHOME_MAX_REMARK_WORDS);
                                }
                                document.getElementById("ADMHOMERemarksCount").innerText = words.length + " / " + ADMHOME_MAX_REMARK_WORDS + " शब्द";
                            }
                        </script>
                    </asp:PlaceHolder>
                    <br />
                    <div class="row mb-2" id="divVadiSave" runat="server">
                        <div class="col-md-12">
                            <center>
                                <asp:HiddenField ID="hfwadiprint" runat="server" />
                                <asp:Button ID="btnAddVadiDetail" OnClick="btnAddVadiDetail_Click" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="1" OnClientClick="return ValidateVadiDetail();"></asp:Button>
                            </center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Panel ID="wadi_grid_div" runat="server" ScrollBars="Auto">
                                <asp:GridView runat="server" Width="100%" ID="wadi_grid" AutoGenerateColumns="false"
                                    CssClass="table-responsive CSSTableGeneratorGrid fontsize"
                                    OnRowCommand="wadi_grid_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove"
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NameAsPerAadhaar" HeaderText="वादी का नाम" ItemStyle-Width="100" ControlStyle-Font-Size="Smaller" />
                                        <asp:BoundField DataField="Vadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" ItemStyle-Width="120" />
                                        <asp:TemplateField HeaderText="लिंग" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGender" runat="server" Text='<%# (Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ) ? "Female" : "Male"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="YearOfBirthAsPerAadhaar" HeaderText="उम्र का वर्ष" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="dist" HeaderText="जिला" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="block" HeaderText="अंचल" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="thana" HeaderText="थाना" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="WardNo" HeaderText="वार्ड" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल संख्या" ItemStyle-Width="80" />
                                        <asp:TemplateField HeaderText="विभाग का प्रतिनिधि" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDeppratinidhi" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_dept")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="संस्था का प्रतिनिधि" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrgpratinidhi" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विभाग/संस्था का नाम" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrgName" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? Eval("vadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPadName" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y") ? Eval("vadi_org_pad_name") : Eval("vadi_dept_pad_name")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>

            <%-- land dispute details: hidden for ADMHOME login --%>
            <asp:PlaceHolder ID="phBhumiVivad" runat="server">
            <br />
            <div class="card">
                <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि विवाद का विवरण</u></b></div>
                <div class="card-body">
                    <asp:UpdatePanel ID="UPBhumivivad_ka_vivarn" runat="server">
                        <ContentTemplate>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label22" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control mb-2" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Select district..."
                                        ControlToValidate="ddlDistrict" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red">जिला</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label23" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlSubdivision" runat="server" CssClass="form-control mb-2" AutoPostBack="True"
                                        Enabled="true" OnSelectedIndexChanged="ddlSubdivision_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Select अनुमंडल..."
                                        ControlToValidate="ddlSubdivision" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red">अनुमंडल</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label24" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control mb-2" AutoPostBack="true"
                                        Enabled="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Select अनुमंडल..."
                                        ControlToValidate="ddlBlock" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red">अनुमंडल</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label25" runat="server" Text="थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlPolice" runat="server" CssClass="form-control mb-2" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Select थाना..."
                                        ControlToValidate="ddlPolice" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red">थाना</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label26" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlareatype" runat="server" CssClass="form-control mb-2" AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlareatype_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                        <asp:ListItem Value="R"> Rural</asp:ListItem>
                                        <asp:ListItem Value="U"> Urban</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="Select क्षेत्र का प्रकार..."
                                        ControlToValidate="ddlareatype" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divPanchyat" runat="server">
                                    <asp:Label ID="labVillage" runat="server" Text="ग्राम पंचायत"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlPanchyat" runat="server" CssClass="form-control mb-2" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlPanchyat_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ErrorMessage="Select थाना..."
                                        ControlToValidate="ddlPanchyat" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div id="divPanchyat_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                                    पंचायत(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                    <asp:TextBox runat="server" ID="txtPanchyat_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3" id="divVillage" runat="server" visible="true">
                                    <asp:Label ID="Label29" runat="server" Text="राजस्व ग्राम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlVillage" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage="Select राजस्व ग्राम..."
                                        ControlToValidate="ddlVillage" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 labeltextsize" id="divVillage_Anya" runat="server" visible="false">
                                    ग्राम(अगर अन्य है)<img src="../images/imp.gif" width="13px" alt="" />
                                    <asp:TextBox runat="server" ID="txtVillage_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div id="divWard" runat="server" class="col-md-3">
                                    <asp:Label ID="Label31" runat="server" Text="वार्ड"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" runat="server" id="imgWard_bhumiVivaran" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlWard_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div id="divWard_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                                    वार्ड(अगर अन्य है)<img alt="" src="../images/imp.gif" width="13px" />
                                    <asp:TextBox runat="server" ID="txtWard_Anya" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label33" runat="server" Text="विवाद का अद्यतन कारक"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddl_vivad_adyatan_sthiti" runat="server" CssClass="form-control mb-2" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Select विवाद का अद्यतन कारक..."
                                        ControlToValidate="ddl_vivad_adyatan_sthiti" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label34" runat="server" Text="राजस्व थाना संख्या"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtrajaswa_sankhya" CssClass="form-control mb-2" onkeypress="return ValidateNum(event)" placeholder="राजस्व थाना संख्या"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label35" runat="server" Text=" भूमि का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlbhumitype" runat="server" CssClass="form-control mb-2"
                                        AutoPostBack="True" Enabled="true" OnSelectedIndexChanged="ddlbhumitype_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="Select  भूमि का प्रकार..."
                                        ControlToValidate="ddlbhumitype" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divSarkaribhumitype" runat="server" visible="false">
                                    <asp:Label ID="labSarkaribhumitype" runat="server" Text="सरकारी भूमि का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" id="ImgSarkaribhumitype" runat="server" visible="true" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlsarkaribhumitype" OnSelectedIndexChanged="ddlsarkaribhumitype_SelectedIndexChanged" runat="server" CssClass="form-control mb-2" AutoPostBack="True"
                                        Enabled="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select सरकारी भूमि का प्रकार..."
                                        ControlToValidate="ddlsarkaribhumitype" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divsarkaribhumitype_Anya" runat="server" visible="false">
                                    <asp:Label ID="Label10" runat="server" Text="सरकारी भूमि का प्रकार (अगर अन्य है)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox runat="server" ID="txtsarkaribhumitype_Anya" MaxLength="100" CssClass="form-control mb-2" placeholder="सरकारी भूमि का प्रकार (अगर अन्य है)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Select  सरकारी भूमि का प्रकार (अगर अन्य है)..."
                                        ControlToValidate="txtsarkaribhumitype_Anya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblbhumivivadtype" runat="server" Text="भूमि विवाद का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlbhumivivadtype" runat="server" CssClass="form-control mb-2" AutoPostBack="True"
                                        Enabled="true" OnSelectedIndexChanged="ddlbhumivivadtype_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ErrorMessage="Select भूमि विवाद का प्रकार..."
                                        ControlToValidate="ddlbhumivivadtype" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divBhumivivad_Anya" runat="server" visible="false">
                                    <asp:Label ID="Label12" runat="server" Text="भूमि विवाद का प्रकार (अगर अन्य है)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox runat="server" ID="txtbhumivivad_Anya" MaxLength="100" CssClass="form-control mb-2" placeholder="भूमि विवाद का प्रकार (अगर अन्य है)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Select भूमि विवाद का प्रकार (अगर अन्य है)..."
                                        ControlToValidate="txtbhumivivad_Anya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblAwadenKiTithi" runat="server" Text="आवेदन की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid"
                                        style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtAwadenKiTithi" runat="server" CssClass="form-control mb-2"
                                        placeholder="आवेदन की तिथि" oncopy="return false" onpaste="return false" AutoComplete="off"
                                        oncut="return false" onkeypress="return dateValidate(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ErrorMessage="Enter आवेदन की तिथि..."
                                        ControlToValidate="txtAwadenKiTithi" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txtAwadenKiTithi" Format="dd-MM-yyyy" CssClass="zindex">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label36" runat="server" Text="वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtVadiVivarani" runat="server" CssClass="form-control" onkeyup="Upper(this)" TextMode="MultiLine" MaxLength="500" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ErrorMessage="Enter वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी..."
                                        ControlToValidate="txtVadiVivarani" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label37" runat="server" Text="वादी द्वारा प्रस्तुत आवेदन"></asp:Label>
                                    <asp:FileUpload ID="AppDoc" runat="server" CssClass="form-control" />

                                    <asp:Label ID="Label38" runat="server" Text="केवल .pdf(3 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                                    <a href="#" id="lnkAppDoc" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label39" runat="server" Text="प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी"></asp:Label>
                                    <asp:TextBox ID="txtPrativadiVivarani" runat="server" CssClass="form-control" onkeyup="Upper(this)" TextMode="MultiLine" MaxLength="500" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label40" runat="server" Text="प्रतिवादी द्वारा प्रस्तुत आवेदन"></asp:Label>
                                    <asp:FileUpload ID="PrativadiDoc" accept=".pdf" runat="server" CssClass="form-control" />

                                    <asp:Label ID="Label41" runat="server" Text="केवल .pdf(3 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                                    <a href="#" id="lnkPrativadiDoc" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            </asp:PlaceHolder>
        </div>
        <div id="step2" runat="server" visible="false">
            <div class="card">
                <div class="card-header text-center" style="font-size: 18px"><b><u>प्रतिवादी का विवरण</u></b></div>
                <div class="card-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label44" runat="server" Text="प्रतिवादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtPName" runat="server" CssClass="form-control"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" placeholder="प्रतिवादी का नाम"
                                        oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ErrorMessage="Enter प्रतिवादी का नाम..."
                                        ControlToValidate="txtPName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label45" runat="server" Text="पिता/ पति का नाम"></asp:Label>
                                    <asp:TextBox ID="txtPFName" runat="server" MaxLength="50" placeholder="पिता/पति का नाम"
                                        oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label46" runat="server" Text="जिला"></asp:Label>
                                    <asp:DropDownList ID="ddlPDistrict" runat="server" CssClass="form-control mb-2"
                                        AutoPostBack="true" Enabled="true" OnSelectedIndexChanged="ddlPDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label47" runat="server" Text="अनुमंडल"></asp:Label>
                                    <asp:DropDownList ID="ddlPSubdivision" runat="server" CssClass="form-control mb-2"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlPSubdivision_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label48" runat="server" Text="अंचल"></asp:Label>
                                    <asp:DropDownList ID="ddlPBlock" runat="server" CssClass="form-control mb-2" OnSelectedIndexChanged="ddlPBlock_SelectedIndexChanged" AutoPostBack="True" Enabled="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label49" runat="server" Text="थाना"></asp:Label>
                                    <asp:DropDownList ID="ddlPThana" runat="server" CssClass="form-control mb-2" AutoPostBack="True" Enabled="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label50" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>
                                    <asp:DropDownList ID="ddlPAreatype" runat="server" CssClass="form-control mb-2" OnSelectedIndexChanged="ddlPAreatype_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem Value="0" Text="--Select--" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Value="R" Text="Rural" Enabled="true"></asp:ListItem>
                                        <asp:ListItem Value="U" Text="Urban" Enabled="true"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3" id="divPPanchyat" runat="server">
                                    <asp:Label ID="labPVillage" runat="server" Text="ग्राम पंचायत"></asp:Label>
                                    <asp:DropDownList ID="ddlPPanchyat" runat="server" CssClass="form-control mb-2" OnSelectedIndexChanged="ddlPPanchyat_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <div id="divPPanchyat_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                                    <asp:Label ID="lblPPanchyat_Anya" runat="server" Text="पंचायत(अगर अन्य है)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtPPanchyat_Anya" runat="server" MaxLength="50"
                                        oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ErrorMessage="Enter पंचायत(अगर अन्य है)..."
                                        ControlToValidate="txtPPanchyat_Anya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red">पंचायत(अगर अन्य है)</asp:RequiredFieldValidator>
                                </div>
                                <div id="divPVillage_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                                    <asp:Label ID="lblPVillage_Anya" runat="server" Text=" ग्राम(अगर अन्य है)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtPVillage_Anya" runat="server" MaxLength="50"
                                        oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ErrorMessage="Enter  ग्राम(अगर अन्य है)..."
                                        ControlToValidate="txtPVillage_Anya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red"> ग्राम(अगर अन्य है)</asp:RequiredFieldValidator>
                                </div>
                                <div id="divPWard_Anya" runat="server" class="col-md-3 labeltextsize" visible="false">
                                    <asp:Label ID="Label43" runat="server" Text="वार्ड(अगर अन्य है)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtPWard_Anya" runat="server" MaxLength="50"
                                        oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ErrorMessage="Enter  वार्ड(अगर अन्य है)..."
                                        ControlToValidate="txtPWard_Anya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red">वार्ड(अगर अन्य है)</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div id="divPVillageCol" runat="server" class="col-md-3" visible="true">
                                    <asp:Label ID="Label42" runat="server" Text=" राजस्व ग्राम"></asp:Label>
                                    <asp:DropDownList ID="ddlPVillage" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPVillage_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div id="divPWard" runat="server" class="col-md-3">
                                    <asp:Label ID="lblPWard" runat="server" Text="वार्ड"></asp:Label>
                                    <asp:DropDownList ID="ddlPWard" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPWard_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div id="divPMohalla" runat="server" class="col-md-3" visible="false">
                                    <asp:Label ID="Label51" runat="server" Text="मोहल्ला"></asp:Label>
                                    <asp:TextBox ID="txtPMohalla" MaxLength="100" runat="server" placeholder="मोहल्ला"
                                        oncopy="return false" onpaste="return false" oncut="return false" AutoComplete="off"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label121" runat="server" Text="मोबाइल नंबर"></asp:Label>
                                    <asp:TextBox MaxLength="10" onkeypress="return ValidateMobile(event)" ID="txtprativadi_Mobile" runat="server" CssClass="form-control" placeholder="मोबाइल नंबर"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row  text-white" style="background-color: dodgerblue">
                                <div class="col-md-3 p-1">
                                    <asp:Label ID="lblis_pratiVadi_from_an_dept" runat="server" Text="क्या प्रतिवादी किसी विभाग का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddl_is_pratiVadi_from_an_dept" runat="server" CssClass="form-control mb-2" OnSelectedIndexChanged="ddl_is_pratiVadi_from_an_dept_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">हां</asp:ListItem>
                                        <asp:ListItem Value="N">नहीं</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ErrorMessage="select क्या प्रतिवादी किसी विभाग का प्रतिनिधि है?..."
                                        ControlToValidate="ddl_is_pratiVadi_from_an_dept" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red" InitialValue="0">क्या प्रतिवादी किसी विभाग का प्रतिनिधि है?</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 p-1" runat="server" id="divPVibhag_details" visible="false">
                                    <asp:Label ID="lblPvibhaag_naam" runat="server" Text="विभाग का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlPvibhaag_naam" runat="server" CssClass="form-control mb-2">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ErrorMessage="Enter  विभाग का नाम..."
                                        ControlToValidate="ddlPvibhaag_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 p-1" runat="server" id="divPVibhag_details2" visible="false">
                                    <asp:Label ID="Label57" runat="server" Text="विभाग में पदनाम"></asp:Label>
                                    <asp:TextBox ID="txtPvibhaag_padanaam" runat="server" CssClass="form-control" AutoComplete="off" placeholder="विभाग का नाम"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-2 text-white" style="background-color: dodgerblue">
                                <div class="col-md-12 p-1">
                                    <p class="p-1" style="background-color: dodgerblue"><b style="color: yellow;">नोट:</b> <u style="color: white; text-decoration: none;">यदि विभाग की कोई जमीन है तो उस स्थिति में वादी विभाग के प्रतिनिधि होंगे |</u> </p>
                                </div>
                            </div>
                            <div class="row text-white" style="background-color: lightseagreen">
                                <div class="col-md-12 p-1">
                                    <asp:Label ID="lbl_is_pratiVadi_from_an_org" runat="server" Text="क्या प्रतिवादी किसी संस्था का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddl_is_pratiVadi_from_an_org" runat="server" OnSelectedIndexChanged="ddl_is_pratiVadi_from_an_org_SelectedIndexChanged" CssClass="form-control w-25" AutoPostBack="true">
                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">हां</asp:ListItem>
                                        <asp:ListItem Value="N">नहीं</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" ErrorMessage="select क्या प्रतिवादी किसी संस्था का प्रतिनिधि है?..."
                                        ControlToValidate="ddl_is_pratiVadi_from_an_org" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red" InitialValue="0">क्या प्रतिवादी किसी विभाग का प्रतिनिधि है?</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div id="divPSanstha_details" runat="server" visible="false" class="row mb-2 text-white" style="background-color: lightseagreen">
                                <div class="col-md-3 mb-2 p-1">
                                    <asp:Label ID="Label130" runat="server" Text="संस्था का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlPsanstha_naam" runat="server" CssClass="form-control" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ErrorMessage="select संस्था का प्रकार..."
                                        ControlToValidate="ddlPsanstha_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red" InitialValue="0">संस्था का प्रकार</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">
                                    <asp:Label ID="Label131" runat="server" Text="संस्था का सम्बन्ध"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlPsanshaanya_naam" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPsanshaanya_naam_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ErrorMessage="select संस्था का प्रकार..."
                                        ControlToValidate="ddlPsanshaanya_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red" InitialValue="0">संस्था का प्रकार</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">
                                    <asp:Label ID="Label132" runat="server" Text="संस्था का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtPsanstha_naam" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="100"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)" placeholder="संस्था का नाम"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" ErrorMessage="select संस्था का नाम..."
                                        ControlToValidate="txtPsanstha_naam" SetFocusOnError="true" Display="Dynamic" ValidationGroup="3" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3 mb-2 p-1">
                                    <asp:Label ID="Label133" runat="server" Text="संस्था में पदनाम"></asp:Label>
                                    <asp:TextBox ID="txtPsanstha_padanaam" runat="server" CssClass="form-control" AutoComplete="off" MaxLength="100" placeholder="संस्था में पदनाम"
                                        onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                </div>
                            </div>
                            </div>    
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row mb-2">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="btnAddPratiVadiDetail" OnClick="btnAddPratiVadi_Click" runat="server" CssClass="btn btn-primary" ValidationGroup="3" OnClientClick="return ValidatePratiVadiDetail();" Text="Save"></asp:Button>
                            </center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">


                            <asp:Panel ID="panel_pratiWadi_grid" runat="server" ScrollBars="Auto">
                                <asp:GridView runat="server" Width="100%" ID="pratiWadi_grid" AutoGenerateColumns="false"
                                    CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="pratiWadi_grid_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove"
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sl. No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="pratiVadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" ItemStyle-Width="120" />
                                        <asp:BoundField DataField="dist" HeaderText="जिला" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="block" HeaderText="अंचल" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="thana" HeaderText="थाना" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="WardNo" HeaderText="वार्ड" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="pratiVadi_MobileNo" HeaderText="मोबाइल संख्या" ItemStyle-Width="80" />
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
                                        <asp:TemplateField HeaderText="विभाग/संस्था का नाम" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrgName" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ) ? Eval("pratiVadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPadName" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y") ? Eval("pratiVadi_org_pad_name") : Eval("pratiVadi_dept_pad_name")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                    <br />
                    <div class="card-body">
                        <h5 class="text-black"><b>अन्य विवरण</b></h5>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row mb-2">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblwadi_pratiwadi_sunwai" runat="server" Text="प्रतिवादी को सूचित किया गया है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                        <asp:DropDownList ID="ddlwadi_pratiwadi_sunwai" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlwadi_pratiwadi_sunwai_SelectedIndexChanged">
                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हां</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" ErrorMessage="select प्रतिवादी को सूचित किया गया है या नहीं ?..."
                                            ControlToValidate="ddlwadi_pratiwadi_sunwai" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2" ForeColor="Red" InitialValue="0">प्रतिवादी को सूचित किया गया है या नहीं ?</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Label ID="labNotice" runat="server" Text=""></asp:Label>
                                        <asp:DropDownList runat="server" ID="ddlKiskeduwara_bhejagaya" CssClass="form-control" Visible="false">
                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Telephone">दूरभाष के माध्यम से</asp:ListItem>
                                            <asp:ListItem Value="Watchman">चौकीदार के माध्यम से</asp:ListItem>
                                            <asp:ListItem Value="Letter">पत्र के माध्यम से</asp:ListItem>
                                            <asp:ListItem Value="Other">अन्य</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" onkeyup="Upper(this)" ID="txtsunwaiHetuNoticKaKaran" Visible="false"
                                            CssClass="form-control mb-2"></asp:TextBox>
                                        <div id="div_sunwaiHetuNoticKaKaran" runat="server" style="text-align: right" visible="false">
                                            <asp:Label ID="Label52" runat="server" Text="अधिकतम 500 वर्ण "></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="divSuchana_ka_tamila" runat="server" visible="false">
                                        <asp:Label ID="lblSuchana_ka_tamila" runat="server" Text="प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                        <asp:DropDownList runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSuchana_ka_tamila_SelectedIndexChanged" AutoPostBack="true" ID="ddlSuchana_ka_tamila">
                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हां</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" ErrorMessage="select प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?..."
                                            ControlToValidate="ddlSuchana_ka_tamila" SetFocusOnError="true" Display="Dynamic" ForeColor="Red"
                                            InitialValue="0">प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-3" id="divSuchana_ka_upasthiti" visible="false" runat="server">
                                        <asp:Label ID="Label66" runat="server" Text="प्रतिवादी उपस्थित हुआ है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                        <asp:DropDownList ID="ddlSuchana_ka_upasthiti" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                            <asp:ListItem Value="Y">हां</asp:ListItem>
                                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" ErrorMessage="select प्रतिवादी उपस्थित हुआ है या नहीं ?..."
                                            ControlToValidate="ddlSuchana_ka_upasthiti" SetFocusOnError="true" Display="Dynamic" ForeColor="Red"
                                            InitialValue="0">प्रतिवादी उपस्थित हुआ है या नहीं ?</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
        <div id="step3" runat="server" visible="false">
            <div class="card">
                <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि का खाता-खेसरा का विवरण</u></b></div>
                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row mb-2">
                                <div class="col-md-2">
                                    <asp:Label ID="lblkhatasankhya" runat="server" Text="खाता संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtkhatasankhya" runat="server" CssClass="form-control" onkeypress="return ValidateNum(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" ErrorMessage="select खाता संख्या..."
                                        ControlToValidate="txtkhatasankhya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red">खाता संख्या</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="Label68" runat="server" Text="खेसरा संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtkhesarasankhya" runat="server" CssClass="form-control" onkeypress="return ValidateNumKhata(event)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" ErrorMessage="select खेसरा संख्या..."
                                        ControlToValidate="txtkhesarasankhya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red">खेसरा संख्या</asp:RequiredFieldValidator>

                                </div>

                                <div class="row mb-3">
                                    <div class="col-md-12">
                                        <br />
                                        <a href="http://land.bihar.gov.in/Ror/RoR.aspx" style="color: dodgerblue;" target="_blank"><b><i class="fa fa-link"></i>&nbsp;खाता-खेसरा को सत्यापित करने के लिए यहाँ क्लिक करें </b></a>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <p style="color: red;"><b>नोट</b>:-<span style="color: black">&nbsp;खेसरा संख्या एक से अधिक होने पर बीच में कॉमा(,) डाले</span></p>

                                </div>
                            </div>

                            <div class="card mb-2">
                                <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                    <p>रकबा</p>
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblrakabasankhya" runat="server" Text="क्षेत्रफल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:TextBox ID="txtrakabasankhya" MaxLength="15" onkeypress="return isNumberdecimalKey(event,this)" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" ErrorMessage="Enter क्षेत्रफल..."
                                                ControlToValidate="txtrakabasankhya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red">क्षेत्रफल</asp:RequiredFieldValidator>

                                            <asp:Label ID="lblrakabasankhya1" runat="server" Text="क्षेत्रफल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:TextBox ID="txtrakabasankhya1" MaxLength="15" onkeypress="return isNumberdecimalKey(event,this)" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" ErrorMessage="Enter क्षेत्रफल..."
                                                ControlToValidate="txtrakabasankhya1" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red">क्षेत्रफल</asp:RequiredFieldValidator>
                                            <asp:Label ID="lblrakabasankhya2" runat="server" Text="क्षेत्रफल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:TextBox ID="txtrakabasankhya2" MaxLength="15" onkeypress="return isNumberdecimalKey(event,this)" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" ErrorMessage="Enter क्षेत्रफल..."
                                                ControlToValidate="txtrakabasankhya2" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red">क्षेत्रफल</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblddlrakabasankhya" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlrakabasankhya" runat="server" CssClass="form-control mb-2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ErrorMessage="select यूनिट..."
                                                ControlToValidate="ddlrakabasankhya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">यूनिट</asp:RequiredFieldValidator>

                                            <asp:Label ID="Label60" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlrakabasankhya1" runat="server" CssClass="form-control mb-2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" ErrorMessage="select यूनिट..."
                                                ControlToValidate="ddlrakabasankhya1" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">यूनिट</asp:RequiredFieldValidator>

                                            <asp:Label ID="Label61" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlrakabasankhya2" runat="server" CssClass="form-control mb-2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" ErrorMessage="select यूनिट..."
                                                ControlToValidate="ddlrakabasankhya2" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">यूनिट</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6"></div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-12">
                                            <p style="color: red;"><b>नोट</b>:-<span style="color: black">&nbsp;क्षेत्रफल में सबसे बड़ी इकाई दर्ज करें फिर छोटी इकाई दर्ज करें फिर सबसे छोटी इकाई दर्ज करें अर्थात 0 हेक्टेयर, 0 एकड़ ,1.5 डेसिमल</span></p>
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <div class="col-md-3">
                                            <asp:Label ID="lblkhatiyan_me_jaminvivran" runat="server" Text="खतियान में जमीन की किस्म का विवरण"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlkhatiyan_me_jaminvivran" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" ErrorMessage="Enter खतियान में जमीन की किस्म का विवरण..."
                                                ControlToValidate="ddlkhatiyan_me_jaminvivran" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">खतियान में जमीन की किस्म का विवरण</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-9">
                                            <asp:Label ID="lblkhatiyan_me_jaminvivran_text" runat="server" Text="खतियान में जमीन का विवरण"></asp:Label>
                                            <asp:TextBox ID="txtkhatiyan_me_jaminvivran_text" runat="server" CssClass="form-control" TextMode="MultiLine" MaxLength="500" placeholder="अधिकतम 500 शब्द" Style="height: 43px !important;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card">
                                <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                    <p>चौहद्दी का विवरण</p>
                                </div>
                                <div class="card-body">
                                    <div class="row mb-3">
                                        <div class="col-md-3">
                                            <asp:Label ID="lbluttari_chohaddi" runat="server" Text="उत्तर"></asp:Label>
                                            <asp:TextBox MaxLength="100" ID="txtuttari_chohaddi" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lbldakshini_chohaddi" runat="server" Text="दक्षिण"></asp:Label>
                                            <asp:TextBox MaxLength="100" ID="txtdakshini_chohaddi" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblpurvi_chohaddi" runat="server" Text="पूर्व"></asp:Label>
                                            <asp:TextBox MaxLength="100" ID="txtpurvi_chohaddi" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lblpashchimi_chohaddi" runat="server" Text="पश्चिम"></asp:Label>
                                            <asp:TextBox MaxLength="100" ID="txtpashchimi_chohaddi" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="btnsaveBhumiKaVivaran" OnClick="btnsaveBhumiKaVivaran_Click" OnClientClick="return ValidateBhumiKaVivaran();" runat="server" CssClass="btn btn-primary" ValidationGroup="4" Text="Save"></asp:Button>
                            </center>
                        </div>
                    </div>
                    <br />
                    <div class="row  mb-2">
                        <div class="col-md-12 p-1">
                            <asp:Panel ID="Panelgrd_bhumivivad" runat="server" ScrollBars="Auto">
                                <asp:GridView runat="server" Width="100%" ID="grd_bhumivivad" AutoGenerateColumns="false"
                                    OnRowCommand="grd_bhumivivad_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove"
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                                <span id='<%# Eval("LandDetailsInKhatian") %>' style="display: block; overflow: auto; height: 80px;">
                                                    <%# Eval("LandDetailsInKhatian") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="North_chauhaddee" HeaderText="उत्तर" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="South_chauhaddee" HeaderText="दक्षिण" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="East_chauhaddee" HeaderText="पूर्व" ItemStyle-Width="100" />
                                        <asp:BoundField DataField="West_chauhaddee" HeaderText="पश्चिम" ItemStyle-Width="100" />
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="step4" runat="server" visible="false">
            <div class="card">
                <br />
                <h5 class="text-center text-black-100" style="color: gray"><u>वादी एवं प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण</u></h5>
                <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                    <p>वादी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
                </div>
                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label62" runat="server" Text="वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlIsVadiEvi" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlIsVadiEvi_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">हां</asp:ListItem>
                                        <asp:ListItem Value="N">नहीं</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ErrorMessage="Select वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?..."
                                        ControlToValidate="ddlIsVadiEvi" SetFocusOnError="true" Display="Dynamic" ValidationGroup="5" ForeColor="Red" InitialValue="0">वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2" id="divVadiEvidenceType" runat="server" visible="false">
                                    <asp:Label ID="Label81" runat="server" Text="साक्ष्य का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlVadiEvidenceType" runat="server" CssClass="form-control"
                                        Enabled="true" AutoPostBack="True" OnSelectedIndexChanged="ddlEvidenceType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" ErrorMessage="Select साक्ष्य का प्रकार..."
                                        ControlToValidate="ddlVadiEvidenceType" SetFocusOnError="true" Display="Dynamic" ValidationGroup="5" ForeColor="Red" InitialValue="0">साक्ष्य का प्रकार</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divtxtVadiEvidenceType" runat="server" visible="false">
                                    <asp:Label ID="Label82" runat="server" Text="अगर अन्य हैं तो दस्तावेज का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtVadiEvidenceType" MaxLength="100" placeholder="अगर अन्य हैं तो दस्तावेज का नाम" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" ErrorMessage="Select अगर अन्य हैं तो दस्तावेज का नाम..."
                                        ControlToValidate="txtVadiEvidenceType" SetFocusOnError="true" Display="Dynamic" ValidationGroup="5" ForeColor="Red">अगर अन्य हैं तो दस्तावेज का नाम</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" id="divvadi_dastavej" runat="server" visible="false">
                                    <asp:Label ID="Label71" runat="server" Text="वादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज"></asp:Label>
                                    <asp:FileUpload ID="file_vadi_dastavej_new" accept=".pdf" runat="server" CssClass="form-control" />
                                    <asp:Label ID="Label84" runat="server" Text="केवल .pdf(3 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                                    <a href="#" id="lnkvadikashachhDoc" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="btnAddVadiEvidenceDetail" OnClick="btnAddVadiEvidence_Click" OnClientClick="return ValidateVadiEvidenceDetail();" runat="server" CssClass="btn btn-primary" Text="Save"></asp:Button>
                            </center>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Panel ID="PanelgdVadiEvidence" runat="server" ScrollBars="Auto">
                                <asp:GridView runat="server" Width="100%" ID="gdVadiEvidence" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowDataBound="gdVadiEvidence_RowDataBound" OnRowCommand="gdVadiEvidence_RowCommand">
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
                                                <asp:ImageButton ID="Image1" path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" CommandArgument='<%# Container.DataItemIndex %>' CommandName="View" />

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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                    <p>प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
                </div>
                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label72" runat="server" Text="प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlIsPvadiEvi" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlIsPvadiEvi_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">हां</asp:ListItem>
                                        <asp:ListItem Value="N">नहीं</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ErrorMessage="Select प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?..."
                                        ControlToValidate="ddlIsPvadiEvi" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2" id="divPrativadiEvidence" runat="server" visible="false">
                                    <asp:Label ID="Label73" runat="server" Text="साक्ष्य का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlPrativadiEvidenceType" Enabled="true" AutoPostBack="True" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPrativadiEvidenceType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" ErrorMessage="Select प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?..."
                                        ControlToValidate="ddlPrativadiEvidenceType" SetFocusOnError="true" Display="Dynamic" ValidationGroup="5" ForeColor="Red" InitialValue="0">साक्ष्य का प्रकार</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divtxtPrativadiEvidenceType" runat="server" visible="false">
                                    <asp:Label ID="Label74" runat="server" Text="अगर अन्य हैं तो दस्तावेज का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtPrativadiEvidenceType" MaxLength="100" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" ErrorMessage="Select अगर अन्य हैं तो दस्तावेज का नाम..."
                                        ControlToValidate="txtPrativadiEvidenceType" SetFocusOnError="true" Display="Dynamic" ValidationGroup="5" ForeColor="Red" InitialValue="0">अगर अन्य हैं तो दस्तावेज का नाम</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4" id="divPrativadi_dastavej_new" runat="server" visible="false">
                                    <asp:Label ID="Label83" runat="server" Text="प्रतिवादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज"></asp:Label>
                                    <asp:FileUpload ID="file_Prativadi_dastavej_new" accept=".pdf" runat="server" CssClass="form-control" />
                                    <asp:Label ID="Label86" runat="server" Text="केवल .pdf(3 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                                    <a href="#" id="lnkPrativadiKashachhDoc" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-12">
                            <center>
                                <asp:Button ID="btnAddPrativadiEvidenceDetail" OnClick="btnAddPrativadiEvidence_Click" OnClientClick="return ValidatePrativadiEvidenceDetail();" runat="server" CssClass="btn btn-primary" Text="Save"></asp:Button>
                            </center>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Panel ID="PanelgdPrativadiEvidence" runat="server" ScrollBars="Auto">
                                <asp:GridView runat="server" Width="100%" ID="gdPrativadiEvidence" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="gdPrativadiEvidence_RowCommand" OnRowDataBound="gdPrativadiEvidence_RowDataBound">
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
                                                <asp:ImageButton ID="Image1" path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="step5" runat="server" visible="false">
            <div class="card">
                <div class="card-header text-center" style="font-size: 18px"><b><u>राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण</u></b></div>
                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="UPanel3" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row mb-2">
                                <div class="col-md-6">
                                    <asp:Label ID="Label85" runat="server" Text="पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी"></asp:Label>
                                    <asp:TextBox MaxLength="500" ID="txtpulis_padadhikari_vivarani" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblpulis_padadhikari_Patr_file" accept=".pdf" runat="server" Text="पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज"></asp:Label>
                                    <asp:FileUpload ID="pulis_padadhikari_Patr_file" runat="server" CssClass="form-control" />
                                    <asp:Label ID="Label87" runat="server" Text="केवल .pdf प्रारूप में (3 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                                    <a href="#" id="lnkpulis_padadhikari_Patr_file" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-6">
                                    <asp:Label ID="Label88" runat="server" Text="हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी"></asp:Label>
                                    <asp:TextBox MaxLength="500" ID="txthalkakarmchari_prativedan" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblfile_halkakarmchari_praptr" runat="server" Text="हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज"></asp:Label>
                                    <asp:FileUpload ID="file_halkakarmchari_praptr" accept=".pdf" runat="server" CssClass="form-control" />
                                    <asp:Label ID="Label90" runat="server" Text="केवल .pdf प्रारूप में (3 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                                    <a href="#" id="lnkfile_halkakarmchari_praptr" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-3" id="divbhukhand_mapi" runat="server">
                                    <asp:Label ID="Label91" runat="server" Text="विवादित भू-खंड की मापी"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlbhukhand_mapi" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlbhukhand_mapi_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">मापी आवश्यक है</asp:ListItem>
                                        <asp:ListItem Value="N">मापी आवश्यक नहीं है</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" ErrorMessage="Select विवादित भू-खंड की मापी..."
                                        ControlToValidate="ddlbhukhand_mapi" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">विवादित भू-खंड की मापी</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divbhukhand_Copy" runat="server" visible="false">
                                    <asp:Label ID="Label135" runat="server" Text="मापी ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlbhukhand_Copy" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlbhukhand_Copy_SelectedIndexChanged">
                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                        <asp:ListItem Value="Y">मापी हुई है</asp:ListItem>
                                        <asp:ListItem Value="N">मापी नहीं हुई है</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator69" runat="server" ErrorMessage="Select मापी ?..."
                                        ControlToValidate="ddlbhukhand_Copy" SetFocusOnError="true" Display="Dynamic" ValidationGroup="6" ForeColor="Red" InitialValue="0">मापी ?</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="divMapiKeNirdharit_tithi" runat="server" visible="false">
                                    <asp:Label ID="lblMapiKeNirdharit_tithi" runat="server" Text="मापी के लिए निर्धारित तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />

                                    <asp:TextBox ID="txtMapiKeNirdharit_tithi" MaxLength="10" runat="server" CssClass="form-control"
                                        placeholder="dd-yy-mm" oncopy="return false" onpaste="return false" AutoComplete="off"
                                        oncut="return false" onkeypress="return dateValidate(event)"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtMapiKeNirdharit_tithi" Format="dd-MM-yyyy" CssClass="zindex">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lblbhukand_prativedan" runat="server" Text="विवादित भू-खंड की मापी का प्रतिवेदन"></asp:Label>
                                    <asp:FileUpload ID="file_bhukand_prativedan" runat="server" accept=".pdf" CssClass="form-control" />
                                    <asp:Label ID="Label15" runat="server" Text="केवल .pdf प्रारूप में (3 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                                    <a href="#" id="lnkfile_bhukand_prativedan" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                                </div>

                            </div>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="lblbhukand_karan" runat="server" Text="विवादित भू-खंड की मापी नहीं होने का कारण"></asp:Label>
                                    <asp:TextBox ID="txtbhukhand_reason" MaxLength="500" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <div class="col-md-9"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="step6" runat="server" visible="false">
            <div class="card">
                <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि विवाद सें संबंधित घटना/ वारदात का विवरण</u></b></div>
                <div class="card-body">

                    <div class="row mb-2">
                        <div class="col-md-4">
                            <asp:Label ID="Label94" runat="server" Text="प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList ID="dd_IsBhumiVivad" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="dd_IsBhumiVivad_SelectedIndexChanged">
                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" ErrorMessage="Select प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ?..."
                                ControlToValidate="dd_IsBhumiVivad" SetFocusOnError="true" Display="Dynamic" ValidationGroup="7" ForeColor="Red" InitialValue="0">प्राथमिकी/अप्राथमिकी/सनहा दर्ज है ?</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-2" id="btnBhumiVivadVivran1" runat="server" visible="false">
                            <asp:Label ID="Label95" runat="server" Text="घटना/वारदात की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:TextBox ID="txtghatanaDate" runat="server" CssClass="form-control mb-2"
                                placeholder="dd-yy-mm" oncopy="return false" onpaste="return false" AutoComplete="off"
                                oncut="return false" onkeypress="return dateValidate(event)"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" OnClientDateSelectionChanged="checkDate"
                                TargetControlID="txtghatanaDate" Format="dd-MM-yyyy" CssClass="zindex">
                            </cc1:CalendarExtender>

                        </div>
                        <div class="col-md-6" id="btnBhumiVivadVivran2" runat="server" visible="false">
                            <asp:Label ID="Label96" runat="server" Text="घटना की संक्षिप्त विवरण"></asp:Label>
                            <asp:TextBox MaxLength="500" ID="txtghatanavivran" runat="server" CssClass="form-control" TextMode="Multiline" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="card" id="btnBhumiVivadVivran3" runat="server" visible="false">
                                <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                    प्राथमिकी
                                </div>
                                <div class="card-body">
                                    <div class="row mb-2">
                                        <div class="col-md-3" style="padding: 0% 2.5% 1% 2.5%; display: none;">
                                            <div class="col-md-6 text-left">
                                                विवरण का प्रकार<img src="../images/imp.gif" width="13px" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList runat="server" ID="ddlDescriptionType" CssClass="form-control" Style="height: 30px">
                                                    <asp:ListItem>--चुने--</asp:ListItem>
                                                    <asp:ListItem Value="प्राथमिकी">प्राथमिकी</asp:ListItem>
                                                    <asp:ListItem Value="अप्राथमिकी">अप्राथमिकी</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="Label97" runat="server" Text="प्राथमिकी दर्ज है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlPrathmiki_huyee_hai" AutoPostBack="true" OnSelectedIndexChanged="ddlPrathmiki_huyee_hai_SelectedIndexChanged" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator73" runat="server" ErrorMessage="Select प्राथमिकी दर्ज है ?..."
                                                ControlToValidate="ddlPrathmiki_huyee_hai" SetFocusOnError="true" Display="Dynamic" ValidationGroup="7" ForeColor="Red" InitialValue="0">प्राथमिकी दर्ज है ?</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3" id="divPrathmiki_sankhiyan" visible="false" runat="server">
                                            <asp:Label ID="Label98" runat="server" Text="प्राथमिकी संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:TextBox ID="txtFIR_sankhya" runat="server" CssClass="form-control" placeholder="प्राथमिकी संख्या" onkeypress="return isNumberwithoperator(event,this)" oncopy="return false" onpaste="return false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" ErrorMessage="Select प्राथमिकी संख्या..."
                                                ControlToValidate="txtFIR_sankhya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="7" ForeColor="Red">प्राथमिकी संख्या</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6" id="divPrathmiki_vivaran" visible="false" runat="server">
                                            <asp:Label ID="Label108" runat="server" Text="प्राथमिकी का विवरण"></asp:Label>
                                            <asp:TextBox ID="txtPrathmik_vivran" MaxLength="500" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <%-- change start--%>
                            <asp:UpdatePanel ID="update1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="card" id="btnBhumiVivadVivran4" runat="server" visible="false">
                                        <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                            अप्राथमिकी
                                        </div>
                                        <div class="card-body">
                                            <div class="row mb-2">
                                                <div class="col-md-3">
                                                    <asp:Label runat="server" Text="अप्राथमिकी दर्ज है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                                    <asp:DropDownList ID="ddlAprathmiki_huyee_hai" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlAprathmiki_huyee_hai_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                        <asp:ListItem Value="Y">हां</asp:ListItem>
                                                        <asp:ListItem Value="N">नहीं</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" ErrorMessage="Select अप्राथमिकी दर्ज है ?..."
                                                        ControlToValidate="ddlAprathmiki_huyee_hai" SetFocusOnError="true" Display="Dynamic" ValidationGroup="7" ForeColor="Red" InitialValue="0">अप्राथमिकी दर्ज है ?</asp:RequiredFieldValidator>
                                                </div>



                                                <div class="col-md-3 dharaType" id="divdharabsn" runat="server" visible="false">

                                                    <img src="images/red_star_PNG44.png"
                                                        class="img-fluid mb-1"
                                                        style="width: 15px; height: auto" />



                                                    <asp:RadioButton ID="rdoOld" runat="server"
                                                        Text="Old IPC"
                                                        GroupName="dhara"
                                                        AutoPostBack="true"
                                                        OnCheckedChanged="DharaChanged" />

                                                    <asp:RadioButton ID="rdoNew" runat="server"
                                                        Text="New IPC"
                                                        GroupName="dhara"
                                                        AutoPostBack="true"
                                                        OnCheckedChanged="DharaChanged" />



                                                </div>
                                                <%-- checkboxend--%>
                                                <div class="col-md-3" id="divDhara" runat="server" visible="false">
                                                    <asp:Label ID="Label99" runat="server" Text="धारा"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" /><br />
                                                    <asp:CheckBox ID="chk107" runat="server" Text="107" />&nbsp;&nbsp;<asp:CheckBox ID="chk109" runat="server" Text="109" />&nbsp;&nbsp;<asp:CheckBox ID="chk110" runat="server" Text="110" />&nbsp;&nbsp;<asp:CheckBox ID="chk113" runat="server" Text="113" />
                                                    &nbsp;&nbsp;<asp:CheckBox ID="chk116" runat="server" Text="116" />&nbsp;&nbsp;<asp:CheckBox ID="chk133" runat="server" Text="133" />&nbsp;&nbsp;<asp:CheckBox ID="chk144" runat="server" Text="144" />
                                                    &nbsp;&nbsp;<asp:CheckBox ID="chk145" runat="server" Text="145" />&nbsp;&nbsp;<asp:CheckBox ID="chk147" runat="server" Text="147" />&nbsp;
                                                </div>
                                                <div class="col-md-3" id="divAPrathmiki_sankhiyan" runat="server" visible="false">
                                                    <asp:Label ID="Label100" runat="server" Text="अप्राथमिकी संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                                    <asp:TextBox ID="txtAFIR_sankhya" runat="server" placeholder="अप्राथमिकी संख्या" oncopy="return false"
                                                        onpaste="return false" CssClass="form-control" onkeypress="return isNumberwithoperator(event,this)"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server" ErrorMessage="Select अप्राथमिकी संख्या..."
                                                        ControlToValidate="txtAFIR_sankhya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="7" ForeColor="Red" InitialValue="0">अप्राथमिकी संख्या</asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-md-3" id="divAPrathmiki_vivaran" runat="server" visible="false">
                                                    <asp:Label ID="Label109" runat="server" Text="अप्राथमिकी का विवरण"></asp:Label>
                                                    <asp:TextBox ID="txtAprathmik_vivran" MaxLength="500" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                                                </div>

                                                <%-- change start--%>
                                                <div class="col-md-3" id="divbsn" runat="server" visible="false">
                                                    <asp:Label runat="server" Text="BNS"></asp:Label>
                                                    &nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px;" />

                                                    <asp:ListBox ID="ddlbsn_dhara_hai"
                                                        runat="server"
                                                        CssClass="form-control select2" OnSelectedIndexChanged="ddlbsn_dhara_hai_SelectedIndexChanged"
                                                        SelectionMode="Multiple" AutoPostBack="true" 
                                                        Width="100%" onchange="saveIPCSelection();"  ></asp:ListBox>

                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                                        runat="server"
                                                        ErrorMessage="Select bsn ?..."
                                                        ControlToValidate="ddlbsn_dhara_hai"
                                                        ValidationGroup="7"
                                                        ForeColor="Red"
                                                        Display="Dynamic">
        BNS दर्ज है ?
                                                    </asp:RequiredFieldValidator>
                                                </div>

                                               
                                                <div class="col-md-3" id="divdhara1" runat="server" visible="false">

                                                    <asp:Label runat="server" Text="IPC"></asp:Label>
                                                    &nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />

                                                    <asp:ListBox
                                                        ID="ddldhara1"
                                                        runat="server"
                                                        CssClass="form-control select2"
                                                        SelectionMode="Multiple"></asp:ListBox>
                                                    <asp:HiddenField ID="hdnSelectedIPC" runat="server" />
                                                     
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator29"
                                                        runat="server"
                                                        ErrorMessage="Select dhara ?..."
                                                        ControlToValidate="ddldhara1"
                                                        SetFocusOnError="true"
                                                        Display="Dynamic"
                                                        ValidationGroup="7"
                                                        ForeColor="Red"
                                                        InitialValue="0">

       IPC dhara दर्ज है ?
                                                    </asp:RequiredFieldValidator>

                                                </div>
                                                <div class="col-md-3" id="div_tbnm" runat="server" visible="false">
                                                    <asp:Label ID="lblnm" runat="server" Text="Add BNS"></asp:Label>
                                                    <asp:TextBox ID="txtbnm" runat="server" CssClass="form-control" onkeypress="return isNumberwithoperator(event,this)" Style="height: 43px;"></asp:TextBox>
                                                </div>
                                                <div class="col-md-3" id="div_tdhara" runat="server" visible="false">
                                                    <asp:Label ID="lbldhara" runat="server" Text="Add IPC "></asp:Label>
                                                    <asp:TextBox ID="txtdhara" runat="server" onkeypress="return isNumberwithoperator(event,this)" CssClass="form-control" Style="height: 43px;"></asp:TextBox>
                                                </div>



                                               
                                            </div>
                                        </div>
                                    </div>
                                    
                                </ContentTemplate>
                                <Triggers>
                                    
                                    <asp:AsyncPostBackTrigger ControlID="ddlbsn_dhara_hai"  EventName="SelectedIndexChanged" />

                                </Triggers>
                            </asp:UpdatePanel>
                            <br />
                            <div class="card" id="btnBhumiVivadVivran5" runat="server" visible="false">
                                <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                    सनहा
                                </div>
                                <div class="card-body">

                                    <div class="row mb-2">
                                        <div class="col-md-3">
                                            <asp:Label ID="Label110" runat="server" Text="सनहा दर्ज़ है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlSanhaStatus" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSanhaStatus_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" ErrorMessage="Select सनहा दर्ज़ है ?..."
                                                ControlToValidate="ddlSanhaStatus" SetFocusOnError="true" Display="Dynamic" ValidationGroup="7" ForeColor="Red" InitialValue="0">सनहा दर्ज़ है ?</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3" id="divSanahaSankhiyan1" runat="server" visible="false">
                                            <asp:Label ID="Label112" runat="server" Text="सनहा संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />

                                            <asp:TextBox ID="txtSanahaSankhiyan" runat="server" oncopy="return false" onpaste="return false" CssClass="form-control"
                                                placeholder="सनहा संख्या" onkeypress="return isNumberwithoperator(event,this)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" ErrorMessage="Select सनहा संख्या..."
                                                ControlToValidate="txtSanahaSankhiyan" SetFocusOnError="true" Display="Dynamic" ValidationGroup="7" ForeColor="Red">सनहा संख्या</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-6" id="divSanahaSankhiyan2" runat="server" visible="false">
                                            सनहा का विवरण
                                                        <asp:TextBox ID="txtSanhaDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 43px;" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-3">
                                            <asp:Label ID="Label113" runat="server" Text="अभियुक्ति"></asp:Label>
                                            <asp:TextBox ID="txtabhiyukt_vaad" MaxLength="500" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                        </div>
                                        <div class="col-md-9"></div>
                                    </div>

                                </div>
                            </div>

                            <div id="btnBhumiVivadVivran6" runat="server" visible="false">
                                <div class="row mb-2">
                                    <div class="col-md-12">
                                        <center>
                                            <asp:Button ID="btnbhumivivad" OnClientClick="return ValidateBhumiVivad();"
                                                runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnbhumivivad_Click" visible="false" />
                                        </center>
                                    </div>
                                </div>
                                <div class="row mb-2">
                                    <div class="col-md-12" style="text-align: center">
                                        <asp:Panel ID="Panelgrdbhumivivad" runat="server" ScrollBars="Auto">
                                            <asp:GridView runat="server" ID="grdbhumivivad" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="grdbhumivivad_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove"
                                                                OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
                                                            <span id='<%# Eval("Ghatna_Short_vivran") %>' style="display: block; overflow: auto; height: 80px;">
                                                                <%# Eval("Ghatna_Short_vivran") %>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="is_FIR_registered" HeaderText="प्राथमिकी" ItemStyle-Width="80" />
                                                    <asp:BoundField DataField="praathamiki_sankhya" HeaderText="प्राथमिकी संख्या" ItemStyle-Width="100" />
                                                    <%--<asp:BoundField DataField="DetailsOfFIR" HeaderText="प्राथमिकी/अप्राथमिकी का विवरण" ItemStyle-Width="220" />--%>
                                                    <asp:TemplateField HeaderText="प्राथमिकी का विवरण" ItemStyle-Width="220">
                                                        <ItemTemplate>
                                                            <span id='<%# Eval("praathamiki_ka_vivaran") %>' style="display: block; overflow: auto; height: 80px;">
                                                                <%# Eval("praathamiki_ka_vivaran") %>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:BoundField DataField="is_complaint_filed" HeaderText="अप्राथमिकी" ItemStyle-Width="100" />

                                                    <asp:BoundField DataField="dhaara" HeaderText="धारा (Old)" ItemStyle-Width="100" />


                                                    <asp:TemplateField HeaderText="BNS (New)" ItemStyle-Width="120">
                                                        <ItemTemplate>
                                                            <%# string.IsNullOrEmpty(Eval("bnm").ToString()) ? "--" : Eval("bnm") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="New IPC" ItemStyle-Width="120">
                                                        <ItemTemplate>
                                                            <%# string.IsNullOrEmpty(Eval("newdhara").ToString()) ? "--" : Eval("newdhara") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="BNS Other" ItemStyle-Width="120">
                                                        <ItemTemplate>
                                                            <%# string.IsNullOrEmpty(Eval("bnm1").ToString()) ? "--" : Eval("bnm1") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IPC Other" ItemStyle-Width="120">
                                                        <ItemTemplate>
                                                            <%# string.IsNullOrEmpty(Eval("newdhara1").ToString()) ? "--" : Eval("newdhara1") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="apraathamiki_sankhya" HeaderText="अप्राथमिकी संख्या" ItemStyle-Width="100" />
                                                    <%--<asp:BoundField DataField="DetailsOfFIR" HeaderText="प्राथमिकी/अप्राथमिकी का विवरण" ItemStyle-Width="220" />--%>
                                                    <asp:TemplateField HeaderText="अप्राथमिकी का विवरण" ItemStyle-Width="220">
                                                        <ItemTemplate>
                                                            <span id='<%# Eval("apraathamiki_ka_vivaran") %>' style="display: block; overflow: auto; height: 80px;">
                                                                <%# Eval("apraathamiki_ka_vivaran") %>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="is_Sanha_recorded" HeaderText="सनहा" ItemStyle-Width="80" />
                                                    <asp:BoundField DataField="sanha_sankhya" HeaderText="सनहा संख्या" ItemStyle-Width="100" />
                                                    <%--<asp:BoundField DataField="Accusation" HeaderText="वाद की अद्यतन स्थिति की अभियुक्ति" ItemStyle-Width="220" />--%>
                                                    <asp:TemplateField HeaderText="अभियुक्ति" ItemStyle-Width="220">
                                                        <ItemTemplate>
                                                            <span id='<%# Eval("Abhiyukt") %>' style="display: block; overflow: auto; height: 80px;">
                                                                <%# Eval("Abhiyukt") %>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <br />
                <div class="card-body">
                    <div class="card-header text-center" style="font-size: 18px"><b><u>न्यायालय में प्रक्रियाधीन वाद का विवरण</u></b></div>
                    <div class="row mb-2">
                        <div class="col-md-3">
                            <asp:Label ID="Label101" runat="server" Text="प्रक्रियाधीन वाद का विवरण उपलब्ध है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList AutoPostBack="true" ID="ddl_Isbhumi_Viviad_available" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_Isbhumi_Viviad_available_SelectedIndexChanged">
                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                <asp:ListItem Value="Y">उपलब्ध है</asp:ListItem>
                                <asp:ListItem Value="N">उपलब्ध नहीं है</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" ErrorMessage="Select प्रक्रियाधीन वाद का विवरण उपलब्ध है ..."
                                ControlToValidate="ddl_Isbhumi_Viviad_available" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">प्रक्रियाधीन वाद का विवरण उपलब्ध है </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-9">
                            <asp:UpdatePanel runat="server" ID="UPanel5" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="row mb-2">
                                        <div class="col-md-3" id="btnnyayalay1" runat="server" visible="false">
                                            <asp:Label ID="Label136" runat="server" Text="न्यायालय"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList AutoPostBack="true" ID="ddlnyayalaya" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlnyayalaya_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server" ErrorMessage="Select न्यायालय..."
                                                ControlToValidate="ddlnyayalaya" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">न्यायालय </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3" id="div_rajasw_vevhar_nyalay" runat="server" visible="false">
                                            <asp:Label ID="labNyayalaya_type" runat="server" Text="न्यायालय का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlnyayalaya_type" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlnyayalaya_type_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" ErrorMessage="Select न्यायालय का प्रकार..."
                                                ControlToValidate="ddlnyayalaya_type" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">न्यायालय का प्रकार </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3" id="divDist_nyayalaya_type" runat="server" visible="false">
                                            <asp:Label ID="Label138" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList AutoPostBack="true" ID="ddlDist_nyayalaya_type" runat="server" OnSelectedIndexChanged="ddlDist_nyayalaya_type_SelectedIndexChanged" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator82" runat="server" ErrorMessage="Select जिला ..."
                                                ControlToValidate="ddlDist_nyayalaya_type" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">जिला </asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3" id="divSubdivision_nyayalaya_type" runat="server" visible="false">
                                            <asp:Label ID="Label139" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                            <asp:DropDownList ID="ddlSubdivision_nyayalaya_type" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" ErrorMessage="Select अनुमंडल ..."
                                                ControlToValidate="ddlSubdivision_nyayalaya_type" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">अनुमंडल </asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <br />
                    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row mb-2">
                                <div class="row labeltextsize" id="divVibhag_nyayalay_type" visible="false" runat="server" style="padding: 0% 2.5% 1% 2.5%;">
                                    <div class="col-md-6 text-left">
                                        विभाग<img src="../images/imp.gif" width="13px" />
                                    </div>
                                    <div class="col-md-6">

                                        <asp:DropDownList runat="server" ID="ddlVibhag_nyayalay_type" CssClass="form-control" Style="height: 30px">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-3" id="btnnyayalay3" runat="server" visible="false">
                                    <asp:Label ID="Label140" runat="server" Text="वादी की वाद संख्या / वर्ष"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtdayarvaadsankhya_nayalay" runat="server" CssClass="form-control" placeholder="वादी की वाद संख्या/वर्ष"
                                        onkeypress="return isNumberSahana(event,this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator84" runat="server" ErrorMessage="Select वादी की वाद संख्या / वर्ष ..."
                                        ControlToValidate="txtdayarvaadsankhya_nayalay" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red">वादी की वाद संख्या / वर्ष</asp:RequiredFieldValidator>
                                </div>
                                <div class="row labeltextsize" style="padding: 0% 2.5% 1% 2.5%; display: none;">
                                    <div class="col-md-6 text-left">
                                        वादी की वाद का वर्ष<img src="../images/imp.gif" width="13px" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox runat="server" onkeypress="return isNumberwithoperator(event,this)" ID="txtdayaryear_nayayaly"
                                            CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" id="btnnyayalay4" runat="server" visible="false">
                                    <asp:Label ID="Label141" runat="server" Text="वादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox onkeypress="return ValidateAlpha(event)" placeholder="वादी का नाम" ID="txtvaadiname_nayaylay" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator85" runat="server" ErrorMessage="Select वादी का नाम ..."
                                        ControlToValidate="txtvaadiname_nayaylay" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red">वादी का नाम</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3" id="btnnyayalay5" runat="server" visible="false">
                                    <asp:Label ID="Label142" runat="server" Text="प्रतिवादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox onkeypress="return ValidateAlpha(event)" placeholder="प्रतिवादी का नाम" ID="txtprativadi_nayaylay" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator86" runat="server" ErrorMessage="Select प्रतिवादी का नाम ..."
                                        ControlToValidate="txtprativadi_nayaylay" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red">प्रतिवादी का नाम</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row mb-2" id="btnnyayalay6" runat="server" visible="false">
                        <div class="col-md-3">
                            <asp:Label ID="Label143" runat="server" Text="वाद की अद्यतन स्थिति का विवरण"></asp:Label>
                            <asp:TextBox MaxLength="500" ID="txtwadKiAddhatan_Sthiti_nayayaly" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                        </div>
                        <div class="col-md-9"></div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-md-12" id="btnnyayalay7" runat="server" visible="false">
                            <center>
                                <asp:Button ID="btnnayaylaysave" OnClick="btnnayaylaysave_Click" OnClientClick="return ValidateNayaylayDetails();" runat="server" Text="Save" CssClass="btn btn-primary" />
                            </center>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="text-align: center">
                            <asp:Panel ID="Panelgrdnyayalay_vivran" runat="server" ScrollBars="Auto" Style="padding-left: 100px">
                                <asp:GridView runat="server" ID="grdnyayalay_vivran" AutoGenerateColumns="false"
                                    OnRowCommand="grdnyayalay_vivran_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="btnRowDel" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove"
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');"><i class="fa fa-trash" aria-hidden="true" style="font-size:20px;"></i></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                        <asp:TemplateField HeaderText="अद्धतन स्थिति का विवरण" ItemStyle-Width="220">
                                            <ItemTemplate>
                                                <span id='<%# Eval("vaad_ki_addhatan_sthiti_vivaran") %>' style="display: block; overflow: auto; height: 80px;">
                                                    <%# Eval("vaad_ki_addhatan_sthiti_vivaran") %>
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="step7" runat="server" visible="false">
            <div class="card">
                <div class="card-header text-center" style="font-size: 18px"><b><u>अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण</u></b></div>
                <div class="card-body">
                    <%--<asp:UpdatePanel ID="up23" runat="server" UpdateMode="Conditional">
                                         <ContentTemplate>--%>
                    <div class="row mb-3">
                        <div class="col-md-2">
                            <asp:Label ID="Label114" runat="server" Text="विवाद की संवेदनशीलता"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList AutoPostBack="true" ID="ddlbhumivivadki_sanvedanshilta" runat="server" OnSelectedIndexChanged="ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged" CssClass="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator87" runat="server" ErrorMessage="Select विवाद की संवेदनशीलता ..."
                                ControlToValidate="ddlbhumivivadki_sanvedanshilta" SetFocusOnError="true" Display="Dynamic" ForeColor="Red">विवाद की संवेदनशीलता</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1">
                            <br />
                            <asp:Image Width="100px" ImageUrl="images/1.png" ID="onestar" runat="server" Visible="true" />
                            <asp:Image Width="100px" ImageUrl="images/2.png" ID="twostar" runat="server" Visible="false" />
                            <asp:Image Width="100px" ImageUrl="images/3.png" ID="threestar" runat="server" Visible="false" />
                            <asp:Image Width="100px" ImageUrl="images/4.png" ID="fourstar" runat="server" Visible="false" />
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label115" runat="server" Text="बैठक की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:TextBox ID="txtbaithakDate" MaxLength="10" onkeypress="return dateValidate(event)" runat="server" CssClass="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" OnClientDateSelectionChanged="checkDate"
                                TargetControlID="txtbaithakDate" Format="dd-MM-yyyy" CssClass="zindex">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label116" runat="server" Text="क्या वादी उपस्थित है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList ID="ddlIsVadiAvailable" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator89" runat="server" ErrorMessage="Select क्या वादी उपस्थित है ?..."
                                ControlToValidate="ddlIsVadiAvailable" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" InitialValue="0">क्या वादी उपस्थित है ?</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label117" runat="server" Text="क्या प्रतिवादी उपस्थित है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList ID="ddl_IsprativadiAvailable" runat="server" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                <asp:ListItem Value="Y">हां</asp:ListItem>
                                <asp:ListItem Value="N">नहीं</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator90" runat="server" ErrorMessage="Select क्या प्रतिवादी उपस्थित है ?..."
                                ControlToValidate="ddl_IsprativadiAvailable" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" InitialValue="0">क्या प्रतिवादी उपस्थित है ?</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-3">
                            <asp:Label ID="Label118" runat="server" Text="बैठक का निष्कर्ष (Action)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList AutoPostBack="true" ID="ddlaction" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlaction_SelectedIndexChanged">
                                <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                                <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                                <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                                <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                                <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                                <asp:ListItem Value="6">न्यायालय में लंबित</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator91" runat="server" ErrorMessage="Select बैठक का निष्कर्ष..."
                                ControlToValidate="ddlaction" SetFocusOnError="true" Display="Dynamic" ValidationGroup="4" ForeColor="Red" InitialValue="0">बैठक का निष्कर्ष</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3" id="divNextDate" runat="server" visible="false">
                            <asp:Label ID="labNextDate" runat="server" Text="अगली सुनवाई की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:TextBox ID="txtAgalaDate" onkeypress="return dateValidate(event)" MaxLength="10" runat="server" CssClass="form-control" placeholder="dd-mm-yyyy"></asp:TextBox>
                            <cc1:CalendarExtender ID="PopCalendar2" runat="server" TargetControlID="txtAgalaDate" Format="dd-MM-yyyy" CssClass="zindex">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="col-md-3" id="divvadkavars" runat="server" visible="false">


                            <asp:Label ID="Label30" runat="server" Text="वादी की वाद संख्या / वर्ष"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:TextBox ID="txtvadkavars" runat="server" CssClass="form-control mb-2"></asp:TextBox>


                        </div>
                        <div class="col-md-6" id="divCancelReason" runat="server" visible="false">
                            <asp:Label ID="Label102" runat="server" Text="अस्वीकृति का कारण"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:TextBox ID="txtCancelReason" MaxLength="500" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator93" runat="server" ErrorMessage="Enter अस्वीकृति का कारण..."
                                ControlToValidate="txtCancelReason" SetFocusOnError="true" Display="Dynamic" ForeColor="Red">अस्वीकृति का कारण</asp:RequiredFieldValidator>
                        </div>




                    </div>
                    <div class="row mb-4">
                        <div class="col-md-6">
                            <asp:Label ID="Label122" runat="server" Text="बैठक में लिया गया निर्णय"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:TextBox ID="txtfalafal" MaxLength="500" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator94" runat="server" ErrorMessage="Select बैठक में लिया गया निर्णय..."
                                ControlToValidate="txtfalafal" SetFocusOnError="true" Display="Dynamic" ForeColor="Red">बैठक में लिया गया निर्णय</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label123" runat="server" Text="थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन"></asp:Label>
                            <asp:HiddenField ID="hdLandDoc" runat="server" />
                            <asp:FileUpload ID="LandDoc" runat="server" CssClass="form-control" accept=".pdf" />
                            <asp:Label ID="txtMeetDoc" runat="server" Text="केवल .pdf प्रारूप में (3 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                            <a href="#" id="lnkLandDoc" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                        </div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-md-6">
                            <asp:Label ID="Label103" runat="server" Text="अंचलाधिकारी का मंतव्य"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:TextBox ID="txtabhiyukt_anchaladhikari" MaxLength="500" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator95" runat="server" ErrorMessage="Select अंचलाधिकारी का मंतव्य..."
                                ControlToValidate="txtabhiyukt_anchaladhikari" SetFocusOnError="true" Display="Dynamic" ForeColor="Red">अंचलाधिकारी का मंतव्य</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-1" id="divNextDateDef" runat="server" visible="false">
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label104" runat="server" Text="अंचलाधिकारी का मंतव्य पत्र"></asp:Label>
                            <asp:FileUpload ID="CircleOfficer_letterOfIntent" accept=".pdf" runat="server" CssClass="form-control" />
                            <asp:HiddenField ID="hdCircleOfficer_letterofintent" runat="server" />
                            <asp:Label ID="txtCoMeetDoc" runat="server" Text="केवल .pdf प्रारूप में (3 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                            <a href="#" id="lnkCircleOfficer_letterOfIntent" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                        </div>
                    </div>
                    <div class="row mb-2">
                        <div class="col-md-6">
                            <asp:Label ID="Label106" runat="server" Text="थानाध्यक्ष का मंतव्य"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:TextBox ID="txtabhiyukt_thaanprabhaaree" MaxLength="500" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator96" runat="server" ErrorMessage="Select थानाध्यक्ष का मंतव्य..."
                                ControlToValidate="txtabhiyukt_thaanprabhaaree" SetFocusOnError="true" Display="Dynamic" ForeColor="Red">थानाध्यक्ष का मंतव्य</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label107" runat="server" Text="थानाध्यक्ष का मंतव्य पत्र"></asp:Label>
                            <asp:FileUpload ID="PoliceOfficer_letterOfIntent" accept=".pdf" runat="server" CssClass="form-control" />
                            <asp:HiddenField ID="hdPoliceOfficer_letterOfIntent" runat="server" />
                            <asp:Label ID="txtShoMeetDoc" runat="server" Text="केवल .pdf प्रारूप में (3 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                            <a href="#" id="lnkPoliceOfficer_letterOfIntent" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                        </div>
                    </div>
                    <%-- </ContentTemplate>
                                     </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
        <%--print--%>
        <br />
        <div class="card">
            <div class="card-body">
                <asp:HiddenField ID="step" runat="server" Value="1" />
                <center>
                    <asp:LinkButton ID="btnBack" OnClick="btnBack_Click" Visible="false" runat="server" Style="padding: 5px; border-radius: 5px; background-color: darkred; color: white; text-decoration: none"><i class="fa fa-arrow-left"></i>&nbsp;Back</asp:LinkButton>
                    &nbsp; &nbsp;
                                   <asp:LinkButton ID="btnDraft" runat="server" OnClick="btnDraft_Click" Style="padding: 5px; border-radius: 5px; background-color: dodgerblue; color: white; text-decoration: none">&nbsp;Go To Home<i class="fa fa-home"></i></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkBtnPreview" runat="server" Style="padding: 5px; border-radius: 5px; background-color: coral; color: white; text-decoration: none">&nbsp;Preview<i class="fa fa-eye"></i></asp:LinkButton>&nbsp;                           
                                    &nbsp;<asp:LinkButton ID="btnNext" OnClientClick="return confirm('Are you sure to save data?')" OnClick="btnNext_Click" runat="server"
                                        Style="padding: 5px; border-radius: 5px; background-color: green; color: white; text-decoration: none">Save & Next&nbsp;&nbsp;<i class="fa fa-save"></i></asp:LinkButton>
                </center>
            </div>
        </div>
    </div>
    <div class="modal fade rightpadding" tabindexrole="dialog" id="step_first_preview">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content" style="width: 1200px;">
                <!-- modal-header -->
                <div class="modal-header" id="modal-head_vadi">
                </div>
                <!-- modal-body -->
                <div class="modal-body" id="modal-body_vadi">
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 600px">
                            भू-समाधान
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 550px">
                            गृह विभाग, बिहार सरकार
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>आवेदन विवरण</p>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6 style5 align-top text-left">
                                            <asp:Label ID="Label13" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                            &nbsp;
                                                                            <asp:Label ID="lblPrevieVadiAppID" runat="server" Font-Bold="True" Text="1234"></asp:Label>
                                        </div>
                                        <div class="col-md-6 style5 align-top text-right">
                                            <asp:Label ID="Label27" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                            &nbsp;                                                                        
                                                                            <asp:Label ID="lblPreview_vadi_date" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>वादी का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" id="prviewwadi" style="font-size: 12px; width: 100%; border-collapse: collapse">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>भूमि विवाद का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            जिला:
                                        </div>
                                        <div class="col-md-3" id="div_PreViewVadi_district" runat="server" style="font-size: 12px">
                                            <%-- set  जिला text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            अनुमंडल:
                                        </div>
                                        <div class="col-md-3" id="div_PreViewVadi_Sub_division" runat="server" style="font-size: 12px">
                                            <%-- set  अनुमंडल text by jquerry lits lable--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            अंचल:
                                        </div>
                                        <div class="col-md-3" id="div_PreViewVadiBlock" runat="server" style="font-size: 12px">
                                            <%-- set  अंचल text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            थाना:
                                        </div>
                                        <div class="col-md-3" id="div_PreViewThana" runat="server" style="font-size: 12px">
                                            <%-- set  थाना text by jquerry lits lable--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            क्षेत्र का प्रकार:
                                        </div>
                                        <div class="col-md-3" id="div_PreViewVadi_Area" runat="server" style="font-size: 12px">
                                            <%-- set क्षेत्र का प्रकार text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" id="div_previewvadipanchayt" runat="server" style="font-weight: bold; font-size: 14px">
                                            <%-- set panchayt label by jquerry--%>
                                        </div>
                                        <div class="col-md-3" id="div_PreViewVadi_GramPanchayat_GramNikaya" runat="server" style="font-size: 12px">
                                            <%-- set panchayt text by jquerry  lable--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" id="div_PreviewVadi_Svarajaya_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            राजस्व ग्राम:
                                        </div>
                                        <div class="col-md-3" id="div_PreViewVadi_Svarajaya" runat="server" style="font-size: 12px">
                                            <%-- set  राजस्व ग्राम text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            वार्ड :
                                        </div>
                                        <div class="col-md-3" id="div_PreViewVadi_Ward" runat="server" style="font-size: 12px">
                                            <%-- set  वार्ड text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            विवाद का अद्यतन कारक:
                                        </div>
                                        <div class="col-md-3" id="div_Preview_vadi_Vivad_Ka_Vighatan" runat="server" style="font-size: 12px">
                                            <%-- set  विवाद का अद्यतन कारक text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            राजस्व थाना संख्या:
                                        </div>
                                        <div class="col-md-3" id="div_PreView_vadi_rajashv_sankhaya" runat="server" style="font-size: 12px">
                                            <%-- set  राजस्व थाना संख्या text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            भूमि का प्रकार:
                                        </div>
                                        <div class="col-md-3" id="div_PreviewVadi_BhumiKaPrakar" runat="server" style="font-size: 12px">
                                            <%-- set  भूमि का प्रकार text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" id="div_Preview_vadi_sarkari_bhumi_ka_prakar_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            सरकारी भूमि का प्रकार:
                                        </div>
                                        <div class="col-md-3" id="div_Preview_vadi_sarkari_bhumi_ka_prakar" runat="server" style="font-size: 12px">
                                            <%-- set  भूमि  सरकारी भूमि का प्रकार lable text by jquerry --%>
                                        </div>
                                        <div class="col-md-3" id="div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            सरकारी भूमि का प्रकार (अगर अन्य है) :
                                        </div>
                                        <div class="col-md-3" id="div_PreView_vadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server" style="font-size: 12px">
                                            <%-- set   सरकारी भूमि का प्रकार (अगर अन्य है) lable text by jquerry --%>
                                        </div>
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                                            भूमि विवाद का प्रकार:
                                        </div>
                                        <div class="col-md-3" id="div_PreViewVadi_BhumiKa_VivadPrakar" runat="server" style="font-size: 12px">
                                            <%-- set  भूमि विवाद का प्रकार text by jquerry lits lable--%>
                                        </div>
                                        <div class="col-md-3" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            भूमि विवाद का प्रकार (अगर अन्य है) :
                                        </div>
                                        <div class="col-md-3" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya" runat="server" style="font-size: 12px">
                                            <%-- set  भूमि विवाद का प्रकार (अगर अन्य है) : text by jquerry lits lable--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px;">
                                            वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी 
                                        </div>
                                        <div class="col-md-9 Break_Word_next_Line" id="divVadiKabhumiVivaran" runat="server" style="font-size: 12px">
                                            <%-- set  भूमि विवाद का प्रकार text by jquerry lits lable--%>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" style="font-weight: bold; font-size: 14px;">
                                            प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी
                                        </div>
                                        <div class="col-md-9 Break_Word_next_Line" id="divPrativadiKabhumiVivaran" runat="server" style="font-size: 12px">
                                            <%-- set  भूमि विवाद का प्रकार text by jquerry lits lable--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal-footer -->
                <div class="modal-footer" id="modal-footer_vadi" style="justify-content: center !important;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="close btn-danger btnvadiprint" type="button" data-dismiss="modal" id="btnclosedialogVadi">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade rightpadding" tabindexrole="dialog" id="step_second_preview">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content" style="width: 1200px;">
                <!-- modal-header -->
                <div class="modal-header" id="modal-head_prativadi">
                </div>
                <!-- modal-body -->
                <div class="modal-body" id="modal-body_prativadi">
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 600px">
                            भू-समाधान
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 550px">
                            गृह विभाग, बिहार सरकार
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>आवेदन विवरण</p>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6 style5 align-top text-left">
                                            <asp:Label ID="Label28" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                            &nbsp;
                                                                            <asp:Label ID="lblPreViewPrativadiAppID" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                        <div class="col-md-6 style5 align-top text-right">
                                            <asp:Label ID="Label32" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                            &nbsp;                                                                        
                                                                            <asp:Label ID="lblPreview_Prativadi_date" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>प्रतिवादी का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" id="divpreviewPratiwadi" runat="server" style="width: 100%; border-collapse: collapse">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>प्रतिवादी का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" id="divprativadi_ka_suchit_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            प्रतिवादी को सूचित किया गया है या नहीं ?:
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_suchit" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_Karan_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            कारण स्पष्ट करें:
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_Karan" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_madham_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            माध्यम:
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_madham" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_Suchna_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?:
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_Suchna" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_Upashtith_Label" runat="server" style="font-weight: bold; font-size: 14px">
                                            प्रतिवादी उपस्थित हुआ है या नहीं ? :
                                        </div>
                                        <div class="col-md-3" id="divprativadi_ka_Upashtith" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal-footer -->
                <div class="modal-footer" id="modal-footer_prativadi" style="justify-content: center !important;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="close btn-danger btnvadiprint" runat="server" type="button" data-dismiss="modal" id="btnclosedialogPrativadi">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade rightpadding" tabindexrole="dialog" id="step_third_preview">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content" style="width: 1200px;">
                <!-- modal-header -->
                <div class="modal-header" id="modal-head_bhumikhata_shekher_ka_vivaran">
                </div>
                <!-- modal-body -->
                <div class="modal-body" id="modal-body_bhumikhata_shekher_ka_vivaran">
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 600px">
                            भू-समाधान
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 550px">
                            गृह विभाग, बिहार सरकार
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>आवेदन विवरण</p>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6 style5 align-top text-left">
                                            <asp:Label ID="Label53" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                            &nbsp;
                                                                            <asp:Label ID="lblPreView_setpthird_AppID" runat="server" Font-Bold="True" Text="1234"></asp:Label>
                                        </div>
                                        <div class="col-md-6 style5 align-top text-right">
                                            <asp:Label ID="Label54" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                            &nbsp;                                                                        
                                                                            <asp:Label ID="lblPreview_setpthird_date" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>भूमि का खाता-खेसरा का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" id="divbhumikhata_shekher_ka_vivaran" runat="server" style="width: 100%; border-collapse: collapse">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal-footer -->
                <div class="modal-footer" id="modal-footer_bhumikhata_shekher_ka_vivaran" style="justify-content: center !important;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="close btn-danger btnvadiprint" runat="server" type="button" data-dismiss="modal" id="btnbhumikhata_shekher_ka_vivaran">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade rightpadding" tabindexrole="dialog" id="step_fourth_preview">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content" style="width: 1200px;">
                <!-- modal-header -->
                <div class="modal-header" id="modal-head_step_fourth_preview">
                </div>
                <!-- modal-body -->
                <div class="modal-body" id="modal-body_step_fourth_preview">
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 600px">
                            भू-समाधान
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 550px">
                            गृह विभाग, बिहार सरकार
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>आवेदन विवरण</p>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6 style5 align-top text-left">
                                            <asp:Label ID="Label56" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                            &nbsp;
                                                                            <asp:Label ID="lblPreView_setpfour_AppID" runat="server" Font-Bold="True" Text="1234"></asp:Label>
                                        </div>
                                        <div class="col-md-6 style5 align-top text-right">
                                            <asp:Label ID="Label59" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                            &nbsp;                                                                        
                                                                            <asp:Label ID="lblPreview_setpfour_date" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>वादी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" id="div1" runat="server" style="font-weight: bold; font-size: 14px">
                                            वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ? :
                                        </div>
                                        <div class="col-md-3" id="divVadi_dvaara_saakshy_ka_dastaavej_upalabdh" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" id="divVadi_ka_Saakshy" runat="server" style="width: 100%; border-collapse: collapse; display: contents !important;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" id="div" runat="server" style="font-weight: bold; font-size: 14px">
                                            प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ? :
                                        </div>
                                        <div class="col-md-3" id="divPrativadi_dvaara_saakshy_ka_dastaavej_upalabdh" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" id="divPrativadi_ka_Saakshy" runat="server" style="width: 100%; border-collapse: collapse; display: contents !important;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal-footer -->
                <div class="modal-footer" id="modal-footer_step_fourth_preview" style="justify-content: center !important;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="close btn-danger btnvadiprint" runat="server" type="button" data-dismiss="modal" id="btnstep_fourth_preview">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade rightpadding" tabindexrole="dialog" id="step_fith_preview">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content" style="width: 1200px;">
                <!-- modal-header -->
                <div class="modal-header" id="modal-head_step_fith_preview">
                </div>
                <!-- modal-body -->
                <div class="modal-body" id="modal-body_step_fith_preview">
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 600px">
                            भू-समाधान
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 550px">
                            गृह विभाग, बिहार सरकार
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>आवेदन विवरण</p>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6 style5 align-top text-left">
                                            <asp:Label ID="Label64" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                            &nbsp;
                                                                            <asp:Label ID="lblPreView_setpfive_AppID" runat="server" Font-Bold="True" Text="1234"></asp:Label>
                                        </div>
                                        <div class="col-md-6 style5 align-top text-right">
                                            <asp:Label ID="Label67" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                            &nbsp;                                                                        
                                                                            <asp:Label ID="lblPreview_setpfive_date" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" id="div2" runat="server" style="font-weight: bold; font-size: 14px">
                                            पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी :
                                        </div>
                                        <div class="col-md-9" id="divPoliceAdhikari" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="divHalkaKarmchari" runat="server">
                                        <div class="col-md-3" id="div4" runat="server" style="font-weight: bold; font-size: 14px">
                                            हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी:
                                        </div>
                                        <div class="col-md-9" id="divHalkaKarmchariValue" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="divVivaditBhukand" runat="server">
                                        <div class="col-md-3" id="div6" runat="server" style="font-weight: bold; font-size: 14px">
                                            विवादित भू-खंड की मापी :
                                        </div>
                                        <div class="col-md-9" id="divVivaditBhukandValue" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="divMapi" runat="server">
                                        <div class="col-md-3" id="div8" runat="server" style="font-weight: bold; font-size: 14px">
                                            मापी ? :
                                        </div>
                                        <div class="col-md-9" id="divMapiValue" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="divVivaditBhukandKaMapi" runat="server">
                                        <div class="col-md-3" id="div10" runat="server" style="font-weight: bold; font-size: 14px">
                                            विवादित भू-खंड की मापी नहीं होने का कारण :
                                        </div>
                                        <div class="col-md-9" id="divVivaditBhukandKaMapiValue" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row" id="MapiKeNirdharnKiThithi" runat="server">
                                        <div class="col-md-3" id="div5" runat="server" style="font-weight: bold; font-size: 14px">
                                            मापी के लिए निर्धारित तिथि :
                                        </div>
                                        <div class="col-md-9" id="divMapiKeNirdharnKiThithiValue" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- modal-footer -->
                <div class="modal-footer" id="modal-footer_step_fith_preview" style="justify-content: center !important;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="close btn-danger btnvadiprint" runat="server" type="button" data-dismiss="modal" id="btnstep_fith_preview">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade rightpadding" tabindexrole="dialog" id="step_six_preview">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content" style="width: 1200px;">
                <!-- modal-header -->
                <div class="modal-header" id="modal-head_step_six_preview">
                </div>
                <!-- modal-body -->
                <div class="modal-body" id="modal-body_step_fith_preview">
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 600px">
                            भू-समाधान
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 550px">
                            गृह विभाग, बिहार सरकार
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>आवेदन विवरण</p>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6 style5 align-top text-left">
                                            <asp:Label ID="Label69" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                            &nbsp;
                                                                            <asp:Label ID="lblPreView_setpsix_AppID" runat="server" Font-Bold="True" Text="1234"></asp:Label>
                                        </div>
                                        <div class="col-md-6 style5 align-top text-right">
                                            <asp:Label ID="Label75" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                            &nbsp;                                                                        
                                                                            <asp:Label ID="lblPreview_setpsix_date" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>भूमि विवाद सें संबंधित घटना/ वारदात का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" id="div3" runat="server" style="font-weight: bold; font-size: 14px">
                                            प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ? :
                                        </div>
                                        <div class="col-md-3" id="div_Prathamik" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" id="divbhumivivadPrivewGrid" runat="server" style="width: 100%; border-collapse: collapse; display: contents !important;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>न्यायालय में प्रक्रियाधीन वाद का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" id="div11" runat="server" style="font-weight: bold; font-size: 14px">
                                            प्रक्रियाधीन वाद का विवरण उपलब्ध है ? :
                                        </div>
                                        <div class="col-md-3" id="divPrakiriyaVad" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12" id="divNyaayaalayPrivewGrid" runat="server" style="width: 100%; border-collapse: collapse; display: contents !important;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal-footer -->
                <div class="modal-footer" id="modal-footer_step_six_preview" style="justify-content: center !important;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="close btn-danger btnvadiprint" runat="server" type="button" data-dismiss="modal" id="btnstep_six_preview">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade rightpadding" tabindexrole="dialog" id="step_seven_preview">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content" style="width: 1200px;">
                <!-- modal-header -->
                <div class="modal-header" id="modal-head_step_seven_preview">
                </div>
                <!-- modal-body -->
                <div class="modal-body" id="modal-body_step_seven_preview">
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 600px">
                            भू-समाधान
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12;" style="font-weight: bold; padding-left: 550px">
                            गृह विभाग, बिहार सरकार
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>आवेदन विवरण</p>
                                    </div>

                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6 style5 align-top text-left">
                                            <asp:Label ID="Label77" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                            &nbsp;
                                                                            <asp:Label ID="lblPreView_setpseven_AppID" runat="server" Font-Bold="True" Text="1234"></asp:Label>
                                        </div>
                                        <div class="col-md-6 style5 align-top text-right">
                                            <asp:Label ID="Label79" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                            &nbsp;                                                                        
                                                                            <asp:Label ID="lblPreview_setptseven_date" runat="server" Font-Bold="True"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                                        <p>अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण</p>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3" id="div7" runat="server" style="font-weight: bold; font-size: 14px">
                                            विवाद की संवेदनशीलता:
                                        </div>
                                        <div class="col-md-3" id="divVivaadKiSanvedanasheelata" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="div12" runat="server" style="font-weight: bold; font-size: 14px">
                                            बैठक की तिथि:
                                        </div>
                                        <div class="col-md-3" id="divBaithakKiTithi" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" id="div13" runat="server" style="font-weight: bold; font-size: 14px">
                                            क्या वादी उपस्थित है ? :
                                        </div>
                                        <div class="col-md-3" id="divkyaVaadeeUpasthitHai" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="div15" runat="server" style="font-weight: bold; font-size: 14px">
                                            क्या प्रतिवादी उपस्थित है ?:
                                        </div>
                                        <div class="col-md-3" id="divKyaPrativaadeeUpasthitHai" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" id="div9" runat="server" style="font-weight: bold; font-size: 14px">
                                            बैठक का निष्कर्ष 
                                        </div>
                                        <div class="col-md-3" id="divBaithakKaNishkarsh" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="divtithi" runat="server" style="font-weight: bold; font-size: 14px">
                                            <%-- this value set by jquery--%>
                                        </div>
                                        <div class="col-md-3" id="divtithivalue" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="divAsveekrtiKaKaaranLabel" runat="server" style="font-weight: bold; font-size: 14px">
                                            अस्वीकृति का कारण
                                        </div>
                                        <div class="col-md-3" id="divAsveekrtiKaKaaran" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" id="div14" runat="server" style="font-weight: bold; font-size: 14px">
                                            बैठक में लिया गया निर्णय:
                                        </div>
                                        <div class="col-md-3" id="divBaithakMeinLiyaGayaNirnay" runat="server" style="font-size: 12px">
                                        </div>
                                        <div class="col-md-3" id="div17" runat="server" style="font-weight: bold; font-size: 14px">
                                            अंचलाधिकारी का मंतव्य :
                                        </div>
                                        <div class="col-md-3" id="divAnchalaadhikaareeKaMantavy" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3" id="div16" runat="server" style="font-weight: bold; font-size: 14px">
                                            थानाध्यक्ष का मंतव्य :
                                        </div>
                                        <div class="col-md-3" id="divThaanaadhyakshKaMantavy" runat="server" style="font-size: 12px">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- modal-footer -->
                <div class="modal-footer" id="modal-footer_step_seven_preview" style="justify-content: center !important;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="close btn-danger btnvadiprint" runat="server" type="button" data-dismiss="modal" id="btnstep_seven_preview">Close</button>
                        </div>
                    </div>
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
                    <iframe id="inlineFrameExample" title="Inline Frame Example" width="99%" height="600px" src=""></iframe>
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

