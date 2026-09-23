<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePublic.master" AutoEventWireup="true" CodeFile="Public_Entry_Page.aspx.cs" Inherits="LandDispute_Entry_Entry_Page" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="JS/userapplicationvalidation.js"></script>
    <style type="text/css">
        .zindex {
            background-color: #FAF5EF;
            z-index: 10001;
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
    <%--Jquerry calling--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_LinkBtnPreview").click(function () {
                debugger;
                //vadi preview
                if ($("#ctl00_ContentPlaceHolder1_step1").is(":visible") == true) {
                    if ($("#ctl00_ContentPlaceHolder1_hfwadiprint").val() == "Printstep1") {

                        $('#<%=lblPrevieVadiAppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());
                        $('#<%=lblPreview_vadi_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                        //lblPreview_vadi_date
                        $("#ctl00_ContentPlaceHolder1_wadi_grid th:first-child").hide();
                        $("#ctl00_ContentPlaceHolder1_wadi_grid td:first-child").hide();
                        $("#prviewwadi").html($("#ctl00_ContentPlaceHolder1_wadi_grid_div").html());




                        //$("#prviewwadi").html($("#ctl00_ContentPlaceHolder1_wadi_grid").html());                      
                        $('#step_first_preview').modal('show');

                    }
                    else {
                        alert("कृपया वादी जोड़ें ..!");
                    }

                }

                //prativadi preview

                if ($('#<%=step2.ClientID%>').is(":visible") == true) {
                    var countGridViewRow = $('#<%=pratiWadi_grid.ClientID%>').length;
                    if (countGridViewRow > 0) {
                        $("#<%=pratiWadi_grid.ClientID%> th:first-child").hide();
                        $("#<%=pratiWadi_grid.ClientID%> td:first-child").hide();
                        $('#<%=divpreviewPratiwadi.ClientID%>').html($('#<%=panel_pratiWadi_grid.ClientID%>').html());

                        $('#<%=lblPreViewPrativadiAppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());
                        $('#<%=lblPreview_Prativadi_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                        $('#step_second_preview').modal('show');
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
                        $('#<%=lblPreView_setpSecond_AppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());;
                        $('#<%=lblPreview_setptSecond_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());

                        if (ValidationBhumiVivad() == true) {


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




                            //$("#prviewwadi").html($("#ctl00_ContentPlaceHolder1_wadi_grid").html());                      

                        }








                        $('#step_third_preview').modal('show');
                    }
                    else {
                        alert("कृपया भूमि का खाता-खेसरा जोड़ें ..!");
                    }
                }

                if ($('#<%=step4.ClientID%>').is(":visible") == true) {
                    var countGridViewRow1 = $('#<%=gdVadiEvidence.ClientID%>').length;

                    if (countGridViewRow1 > 0 && countGridViewRow2 > 0) {
                        //वादी द्वारा प्रस्तुत साक्ष्य का विवरण
                        var IsVadiEviText = $('#<%=ddlIsVadiEvi.ClientID%>').find('option:selected').text();
                        $('#<%=divVadi_dvaara_saakshy_ka_dastaavej_upalabdh.ClientID%>').text(IsVadiEviText);
                        $("#<%=gdVadiEvidence.ClientID%> th:nth-child(3)").hide();
                        $("#<%=gdVadiEvidence.ClientID%> td:nth-child(3)").hide();
                        $("#<%=gdVadiEvidence.ClientID%> th:nth-child(4)").hide();
                        $("#<%=gdVadiEvidence.ClientID%> td:nth-child(4)").hide();
                        $('#<%=divVadi_ka_Saakshy.ClientID%>').html($('#<%=PanelgdVadiEvidence.ClientID%>').html());




                        $('#<%=lblPreView_setpfour_AppID.ClientID%>').text($('#<%=hfGetApplicationno.ClientID%>').val());;
                        $('#<%=lblPreview_setpfour_date.ClientID%>').text($('#<%=hfGetdateAvadan.ClientID%>').val());
                        $('#step_fourth_preview').modal('show');
                    }
                    else {
                        alert("कृपया वादी द्वारा प्रस्तुत साक्ष्य का विवरण या प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण जोड़ें ..!");
                    }
                }



                return false;
            });


        });
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></AjaxControlToolkit:ToolkitScriptManager>
    <div class="container-fluid">
        <h4 class="text-black text-center"><b>आवेदन का विवरण</b></h4>
        <div class="card mb-3">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <center>
                            <a href="#" id="hstep1" runat="server" class="ActiveClass">वादी &nbsp;<i id="hIstep1" runat="server" class="fa fa-circle-arrow-right" style="color: darkblue"></i></a> &nbsp;&nbsp; 
                                            <a href="#" id="hstep2" runat="server" class="RemoveClass">प्रतिवादी&nbsp;<i id="hIstep2" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a> &nbsp;&nbsp;
                                            <a href="#" id="hstep3" runat="server" class="RemoveClass">खाता-खेसरा&nbsp;<i id="hIstep3" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                                            <a href="#" id="hstep4" runat="server" class="RemoveClass">वादी का साक्ष्य&nbsp;<i id="hIstep4" runat="server" class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                                            
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
                            <div class="row mb-2">
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
                                    <asp:Label ID="Label4" runat="server" Text="पिता/ पति का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
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
                                <div class="col-md-3">
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
                                    <asp:Label ID="Label8" runat="server" Text="पुलिस थाना"> </asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlUserThana" runat="server" CssClass="form-control mb-2" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select थाना..."
                                        ControlToValidate="ddlUserThana" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                        InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label9" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
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
                                    <asp:Label ID="labUVillage" runat="server" Text="ग्राम पंचायत"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
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
                                            <asp:Label ID="Label11" runat="server" Text="राजस्व ग्राम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
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
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label14" runat="server" Text="मोबाइल नंबर"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="txtvadimobile" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return ValidateMobile(event)" placeholder="मोबाइल नंबर"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtvadimobile" ID="RegularExpressionValidator3"
                                        ValidationExpression="^[\s\S]{10,10}$" runat="server" ValidationGroup="1" ErrorMessage="10 numbers required."></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Enter मोबाइल नंबर..."
                                        ControlToValidate="txtvadimobile" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label67" runat="server" Text="पोस्ट ऑफ़िस"></asp:Label>
                                    <asp:TextBox ID="txtPostOffice" runat="server" CssClass="form-control" placeholder="पोस्ट ऑफ़िस"></asp:TextBox>

                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label69" runat="server" Text="पिन कोड"></asp:Label>
                                    <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" placeholder="पिन कोड"></asp:TextBox>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="row mb-2">
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
                                        <asp:BoundField DataField="thana" HeaderText="पुलिस थाना" ItemStyle-Width="100" />
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

                                         <asp:BoundField DataField="PostOffice" HeaderText="पोस्ट ऑफ़िस" ItemStyle-Width="80" />
                                         <asp:BoundField DataField="PinCode" HeaderText="पिन कोड" ItemStyle-Width="80" />

                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>

                        </div>
                    </div>
                </div>
            </div>

            <br />

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
                                    <asp:Label ID="Label49" runat="server" Text="पुलिस थाना "></asp:Label>
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
                                        <asp:BoundField DataField="thana" HeaderText="पुलिस थाना" ItemStyle-Width="100" />
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
                    <div class="card-body" style="display: none">
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
                <div class="card-header text-center" style="font-size: 18px"><b><u>विवादित भूमि का विवरण</u></b></div>

                <div class="card-body">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
    <ContentTemplate>

                    <%--start--%>
                    <div class="row mb-2">
                        <div class="col-md-3">
                            <asp:Label ID="Label30" runat="server" Text="ज़िला का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList ID="ddldistrict_old" runat="server" CssClass="form-control mb-2" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="ddldistrict_old_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ControlToValidate="ddlDistrict_old" SetFocusOnError="true" Display="Dynamic"
                        InitialValue="0" ForeColor="Red">जिला</asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label40" runat="server" Text="अंचल का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList ID="ddl_anchalold" runat="server" CssClass="form-control mb-2" AutoPostBack="True"
                                Enabled="true" OnSelectedIndexChanged="ddl_anchalold_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" 
                        ControlToValidate="ddl_anchalold" SetFocusOnError="true" Display="Dynamic"
                        InitialValue="0" ForeColor="Red">अंचल</asp:RequiredFieldValidator>--%>
                        </div>


                        <div class="col-md-3">
                            <asp:Label ID="Label41" runat="server" Text="हल्का का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList ID="ddlhalka_old" runat="server" CssClass="form-control mb-2" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="ddlhalka_old_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" 
                        ControlToValidate="ddlhalka_old" SetFocusOnError="true" Display="Dynamic"
                        InitialValue="0" ForeColor="Red">हल्का</asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label53" runat="server" Text="मौजा का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                            <asp:DropDownList ID="ddlmowja_old" runat="server" CssClass="form-control mb-2" AutoPostBack="True"
                                Enabled="true" OnSelectedIndexChanged="ddlmowja_old_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" 
                        ControlToValidate="ddl_anchalold" SetFocusOnError="true" Display="Dynamic"
                        InitialValue="0" ForeColor="Red">मौजा</asp:RequiredFieldValidator>--%>
                        </div>

                    </div>
        </ContentTemplate>
                        </asp:UpdatePanel>
                    <%--end--%>
                </div>
            </div>
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

                                        <asp:BoundField DataField="District" HeaderText="ज़िला" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="Block" HeaderText="अंचल" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="Halka" HeaderText="हल्का" ItemStyle-Width="80" />
                                        <asp:BoundField DataField="Mauza" HeaderText="मौजा" ItemStyle-Width="80" />



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
            <div class="card">
                <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि विवाद का प्रकार</u></b></div>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Select अंचल..."
                                        ControlToValidate="ddlBlock" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red">अंचल</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label25" runat="server" Text="पुलिस थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="ddlPolice" runat="server" CssClass="form-control mb-2" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Select थाना..."
                                        ControlToValidate="ddlPolice" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                                        InitialValue="0" ForeColor="Red">पुलिस थाना</asp:RequiredFieldValidator>
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
                                    <asp:FileUpload ID="AppDoc" runat="server" CssClass="form-control" ValidateRequestMode="Disabled" />

                                    <asp:Label ID="Label38" runat="server" Text="केवल .pdf(3 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                                    <a href="#" id="lnkAppDoc" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>

        <div id="step4" runat="server" visible="false">




            <div class="card">
                <br />
                <h5 class="text-center text-black-100" style="color: gray"><u>वादी द्वारा प्रस्तुत साक्ष्य का विवरण</u></h5>
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
                                                <asp:ImageButton ID="Image1" CommandArgument='<%# Eval("FullfileName") %>' runat="server" ImageUrl="images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  CommandName="ViewDoc" />

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

            </div>

            <div class="card">
                <br />
                <h5 class="text-center text-black-100" style="color: gray"><u>विवादित भूमि का फोटो अपलोड करें</u></h5>   
                <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
<p style="font-size:13px;">
  विवादित भूमि का फोटो अपलोड करें 
  <span style="color:red;">(*)</span>&nbsp;
  <span style="font-size:11px; color:#333;">
    (नोट: कम से कम एक फोटो अपलोड करना अनिवार्य है)
  </span>
</p>
                </div>
                <div class="card-body">


                    <div class="row">
                        <div class="col-md-3">
                            <asp:Label ID="Label55" runat="server" Text="फोटो 1"></asp:Label>
                            <asp:FileUpload ID="filePhoto1" runat="server" CssClass="form-control" />

                            <asp:Label ID="Label58" runat="server" Text="केवल .jpeg,.png,.jpg (3 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                            <a href="#" id="pic1" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label54" runat="server" Text="फोटो 2"></asp:Label>
                            <asp:FileUpload ID="filePhoto2" runat="server" CssClass="form-control" />

                            <asp:Label ID="Label63" runat="server" Text="केवल .jpeg,.png,.jpg (3 MB)प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                            <a href="#" id="pic2" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label64" runat="server" Text="फोटो 3"></asp:Label>
                            <asp:FileUpload ID="filePhoto3" runat="server" CssClass="form-control" />

                            <asp:Label ID="Label65" runat="server" Text="केवल .jpeg,.png,.jpg (3 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                            <a href="#" id="pic3" runat="server" path="display" class="getpdfdoc" visible="false">View Document</a>
                        </div>

                    </div>
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


        <br />

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
                    <div class="row" style="display: none">
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
                                            <asp:Label ID="Label15" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                            &nbsp;
                                                                        <asp:Label ID="lblPreView_setpSecond_AppID" runat="server" Font-Bold="True" Text="1234"></asp:Label>
                                        </div>
                                        <div class="col-md-6 style5 align-top text-right">
                                            <asp:Label ID="Label39" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                            &nbsp;                                                                        
                                                                        <asp:Label ID="lblPreview_setptSecond_date" runat="server" Font-Bold="True"></asp:Label>
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
                                            पुलिस थाना:
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
                <div class="modal-footer" id="modal-footer_bhumikhata_shekher_ka_vivaran" style="justify-content: center !important;">
                    <div class="row">
                        <div class="col-md-12">
                            <button class="close btn-danger btnvadiprint" runat="server" type="button" data-dismiss="modal" id="Button1">Close</button>
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

