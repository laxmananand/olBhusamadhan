$(document).ready(function () {
    $("#ctl00_ContentPlaceHolder1_LinkBtnPreview").click(function () {
        debugger;
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
                    //$("#prviewwadi").html($("#ctl00_ContentPlaceHolder1_wadi_grid").html());                      
                    $('#custom-modal-vadi').modal('show');
                }
            }
            else {
                alert("कृपया वादी जोड़ें 1..!");
            }

        }
        return false;
    });

    $("#btnclosedialog").click(function () {
        if ($("#ctl00_ContentPlaceHolder1_step1").is(":visible") == true) {
            if ($("#ctl00_ContentPlaceHolder1_hfwadiprint").val() == "Printstep1") {
                $("#ctl00_ContentPlaceHolder1_wadi_grid th:first-child").show();
                $("#ctl00_ContentPlaceHolder1_wadi_grid td:first-child").show();
            }
        }

    });
});