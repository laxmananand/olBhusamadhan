
function BindBlock(districtCode, ThanaCode) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "Bhu-Dashboard.aspx/GetBlock",
            data: JSON.stringify({ DistrictId: districtCode, ThanaCode: ThanaCode }),
            dataType: "json",
            success: function (data) {
                $("#ddBlock").empty();
                $("#ddBlock").append($("<option></option>").val("0").html("All"));
                $.each(data.d, function (key, value) {
                    $("#ddBlock").append($("<option></option>").val(value.BlockCode).html(value.BlockName));
                });
                if ($("#hdnBlockCode").val() != "") {
                    $("#ddBlock").val($("#hdnBlockCode").val());
                    $("#ddBlock").prop("disabled", true);
                    //getSenAction();
                }
                BindThana($("#ddDistrict").val(), $("#ddBlock").val());
                BindPanchayat($("#ddBlock").val());
                BindData($("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddlPoliceSation").val(), $("#ddSensivity").val(), $("#ddlaction").val(), $("#txtdatefrom").val(), $("#txtDateTo").val());
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 0) {
                    alert(' Check Your Network.');
                } else if (XMLHttpRequest.status == 404) {
                    alert('Requested URL not found.');
                } else if (XMLHttpRequest.status == 500) {
                    alert('Internel Server Error.');
                } else {
                    alert('Unknow Error.\n' + XMLHttpRequest.responseText);
                }
            }
        });
    return false;
}
function BindThana(districtCode,BlockCode) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "Bhu-Dashboard.aspx/GetThana",
            data: JSON.stringify({ DistrictId: districtCode, BlockCode: BlockCode }),
            dataType: "json",
            success: function (data) {
                $("#ddlPoliceSation").empty();
                $("#ddlPoliceSation").append($("<option></option>").val("0").html("All"));
                $.each(data.d, function (key, value) {
                    $("#ddlPoliceSation").append($("<option></option>").val(value.PS_Code).html(value.Police_Station));
                });
                if ($("#hdnThanaCode").val() != "") {
                    $("#ddlPoliceSation").val($("#hdnThanaCode").val());
                    $("#ddBlock").prop("disabled", false);
                    $("#ddlPoliceSation").prop("disabled", true);
                   
                }
               
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 0) {
                    alert(' Check Your Network.');
                } else if (XMLHttpRequest.status == 404) {
                    alert('Requested URL not found.');
                } else if (XMLHttpRequest.status == 500) {
                    alert('Internel Server Error.');
                } else {
                    alert('Unknow Error.\n' + XMLHttpRequest.responseText);
                }
            }
        });
    return false;
}
function BindPanchayat(BlockCode) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "Bhu-Dashboard.aspx/GetPanchayat",
            data: JSON.stringify({ BlockCode: BlockCode }),
            dataType: "json",
            success: function (data) {
                $("#ddPanchayat").empty();
                $("#ddPanchayat").append($("<option class='panchyat'  data-areatype=''></option>").val("0").html("All"));
                $.each(data.d, function (key, value) {

                    $("#ddPanchayat").append($("<option class='panchyat' data-areatype='" + value.AreaType +"'></option>").val(value.PanchayatCode).html(value.PanchayatName));
                });
                getSenAction();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 0) {
                    alert(' Check Your Network.');
                } else if (XMLHttpRequest.status == 404) {
                    alert('Requested URL not found.');
                } else if (XMLHttpRequest.status == 500) {
                    alert('Internel Server Error.');
                } else {
                    alert('Unknow Error.\n' + XMLHttpRequest.responseText);
                }
            }
        });

    return false;
}

function BindWardVillage(Panchayatcode,AreaType) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "MapvillagepointFilter.aspx/GetWardVillage",
            data: JSON.stringify({ PanchayatCode: Panchayatcode, AreaType: AreaType }),
            dataType: "json",
            success: function (data) {
                $("#ddGramAWard").empty();
                $("#ddGramAWard").append($("<option class='panchyat' data-panchayatcode='' data-areatype=''></option>").val("0").html("All"));
                $.each(data.d, function (key, value) {

                    $("#ddGramAWard").append($("<option class='panchyat' data-panchayatcode='" + Panchayatcode + "' data-areatype='" + value.AreaType + "'></option>").val(value.Code).html(value.Name));
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 0) {
                    alert(' Check Your Network.');
                } else if (XMLHttpRequest.status == 404) {
                    alert('Requested URL not found.');
                } else if (XMLHttpRequest.status == 500) {
                    alert('Internel Server Error.');
                } else {
                    alert('Unknow Error.\n' + XMLHttpRequest.responseText);
                }
            }
        });

    return false;
}

function BindData( DistrictId,  BlockCode,  PanchayatCode,  thana_code,  savedansheelta,  Matter_Status,  FromDate,  ToDate) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "Bhu-Dashboard.aspx/GetDetails",
            data: JSON.stringify({ DistrictId: DistrictId, BlockCode: BlockCode, PanchayatCode: PanchayatCode, thana_code: thana_code, savedansheelta: savedansheelta, Matter_Status: Matter_Status, FromDate: FromDate, ToDate: ToDate }),
            dataType: "json",
            success: function (data) {                
                $.each(data.d, function (key, value) {
                   
                    $("#lbltotalapplication").text(value.Total);
                    $("#lblFinalize").text(value.Finalize);
                    $("#lblUnFinalize").text(value.Unfinalize);

                    $("#lblsaamaany").text(value.saamaany);
                    $("#lblsavedansheel").text(value.sanvedanasheel);
                    $("#lblatiSavedansheel").text(value.atisanvedanasheel);

                    $("#lblnispadan").text(value.Nirast);
                    $("#lblmapikenirdharit").text(value.Mapi_Nirdharit);
                    $("#lblprakreeyadheen").text(value.Prakriyadhin);
                    $("#lblashvikrit").text(value.Ashwikrit);
                    $("#lblFinaldisposal").text(value.FinalNirast);

                    $("#BhumiVivadType1").text(value.BhumiVivadType1);
                    $("#BhumiVivadType2").text(value.BhumiVivadType2);
                    $("#BhumiVivadType3").text(value.BhumiVivadType3);
                    $("#BhumiVivadType4").text(value.BhumiVivadType4);
                    $("#BhumiVivadType5").text(value.BhumiVivadType5);
                    $("#BhumiVivadType6").text(value.BhumiVivadType6);
                    $("#BhumiVivadType7").text(value.BhumiVivadType7);
                    $("#BhumiVivadType8").text(value.BhumiVivadType8);
                    $("#BhumiVivadType9").text(value.BhumiVivadType9);
                    $("#BhumiVivadType10").text(value.BhumiVivadType10);
                    $("#BhumiVivadType11").text(value.BhumiVivadType11);
                    $("#BhumiVivadType12").text(value.BhumiVivadType12);
                    $("#BhumiVivadType13").text(value.BhumiVivadType13);
                    $("#BhumiVivadType15").text(value.BhumiVivadType15);
                    $("#BhumiVivadType20").text(value.BhumiVivadType20);

                    $("#Landdesciption1").text(value.Landdesciption1);
                    $("#Landdesciption2").text(value.Landdesciption2);
                    $("#Landdesciption3").text(value.Landdesciption3);
                    $("#Landdesciption5").text(value.Landdesciption5);
                    $("#Landdesciption6").text(value.Landdesciption6);
                    $("#Landdesciption11").text(value.Landdesciption11);
                    $("#Landdesciption12").text(value.Landdesciption12);

                    $("#vadiEvidence1").text(value.vadiEvidence1);
                    $("#vadiEvidence2").text(value.vadiEvidence2);
                    $("#vadiEvidence3").text(value.vadiEvidence3);
                    $("#vadiEvidence4").text(value.vadiEvidence4);
                    $("#vadiEvidence5").text(value.vadiEvidence5);
                    $("#vadiEvidence6").text(value.vadiEvidence6);
                    $("#vadiEvidence7").text(value.vadiEvidence7);
                    $("#vadiEvidence8").text(value.vadiEvidence8);
                    $("#vadiEvidence9").text(value.vadiEvidence9);

                     $("#PrativadiEvidence1").text(value.PrativadiEvidence1);
                     $("#PrativadiEvidence2").text(value.PrativadiEvidence2);
                     $("#PrativadiEvidence3").text(value.PrativadiEvidence3);
                     $("#PrativadiEvidence4").text(value.PrativadiEvidence4);
                     $("#PrativadiEvidence5").text(value.PrativadiEvidence5);
                     $("#PrativadiEvidence6").text(value.PrativadiEvidence6);
                     $("#PrativadiEvidence7").text(value.PrativadiEvidence7);
                     $("#PrativadiEvidence8").text(value.PrativadiEvidence8);
                    $("#PrativadiEvidence9").text(value.PrativadiEvidence9);

                    $("#praathamikee").text(value.praathamikee);
                    $("#apraathamikee").text(value.apraathamikee);
                    $("#sanaha").text(value.sanaha);

                    $("#raajasv_nyaayaalay").text(value.raajasv_nyaayaalay);
                    $("#vyavahaara_nyaayaalay").text(value.vyavahaara_nyaayaalay);
                    $("#LokShikayat_Nivaran_nyaayaalay").text(value.LokShikayat_Nivaran_nyaayaalay);
                    $("#uchcha_nyaayaalay").text(value.uchcha_nyaayaalay);
                    $("#sarvochcha_nyaayaalay").text(value.sarvochcha_nyaayaalay);
                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 0) {
                    alert(' Check Your Network.');
                } else if (XMLHttpRequest.status == 404) {
                    alert('Requested URL not found.');
                } else if (XMLHttpRequest.status == 500) {
                    alert('Internel Server Error.');
                } else {
                    alert('Unknow Error.\n' + XMLHttpRequest.responseText);
                }
            }
        });
    return false;
}
$(document).ready(function () {
    BindBlock($("#ddDistrict").val(), $("#hdnThanaCode").val());
    //BindMapDataLoad("0", "0");
   //BindData("0", "0", "0", "0", "0", "0", "", "");
    $("#BtnSearch").click(function () {
        getSenAction();
        BindData($("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddlPoliceSation").val(), $("#ddSensivity").val(), $("#ddlaction").val(), $("#txtdatefrom").val(), $("#txtDateTo").val());
        return false;

    });
   
    
    
   // BindThana($("#ddDistrict").val(), $("#ddBlock").val());
    //BindWardVillage("0", "U");
    $("#ddDistrict").change(function () {
        BindBlock($("#ddDistrict").val());
        //BindPanchayat($("#ddBlock").val());
       // BindThana($("#ddDistrict").val(), $("#ddBlock").val());
    }); 
    $("#ddBlock").change(function () { 
        BindThana($("#ddDistrict").val(), $("#ddBlock").val());
        BindPanchayat($("#ddBlock").val());        
    });
    
});
function getSenAction() {
   
  
    if ($("#ddPanchayat").val() != "0") {
        BindDataAccordingToFilter("Panchayat", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), 0, $("#ddSensivity").val(), "usp_getPanchayatMapFilter", $("#ddlaction").val());
    }
    else if ($("#ddBlock").val() != "0") {  
       
        BindDataAccordingToFilter("Panchayat", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), 0, $("#ddSensivity").val(), "usp_getPanchayatMapFilter", $("#ddlaction").val());
    }
    else if ($("#ddDistrict").val() != "0") {
        BindDataAccordingToFilter("Block", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), 0, $("#ddSensivity").val(), "usp_getBlockMapFilter", $("#ddlaction").val());       
    }
    else if ($("#ddDistrict").val() == "0") {
        BindMapDataLoad("0", $("#ddSensivity").val());
    }
}

var datainfo = "";
var point;
var x = 1;
var count = 0, countTotal = 0;
var datefrom, todate, legendTitle;
datefrom = document.getElementById("txtdatefrom").value;
todate = document.getElementById("txtDateTo").value;


var dataOnDate = 'Data On date: (' + document.getElementById("txtdatefrom").value + ') to (' + document.getElementById("txtDateTo").value + ')';
if (document.getElementById("txtdatefrom").value == "") {
    dataOnDate = "";
}




var someFlag = true;
var chartTitle = 'भू - समाधान गृह विभाग, बिहार सरकार';
var table;
var dataDistrict, txtdatefrom, txtdatefrom, Message;
Message = document.getElementById("Message");



const drilldown = async function (e) {

    if (!e.seriesOptions) {
        //alert(e.point.ParameterType);
        point = e.point;
        const chart = this;
        const mapKey = '';
        var div = document.getElementById('direction');



        document.getElementById("back").classList.remove("hidden");
        let fail = setTimeout(() => {
            if (!Highcharts.maps[mapKey]) {
                chart.showLoading("<i class=\"icon-frown\"></i> Failed loading ${e.point.name}");
                fail = setTimeout(() => {
                    chart.hideLoading();
                }, 1000);
            }
        }, 3000);
        if (e.point.ParameterType == "1") {
            div.innerHTML = e.point.DISTRICTNAME;
            //script.stop;
            const topology = await fetch(
                'Block.ashx?District=' + e.point.value + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType")
            ).then(response => response.json());


            BindMapDataBlock(topology, "अंचल", 0, 0, e.point.value, 1);

        }
        else if (e.point.ParameterType == "2") {
            div.innerHTML += ' / ' + e.point.DISTRICTNAME;
            const topology = await fetch(
                'Panchayat.ashx?Block=' + e.point.value + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType")
            ).then(response => response.json());
            BindMapDataBlock(topology, "पंचायत", 0, e.point.value, "0", 2);

        }
        else if (e.point.ParameterType == "3") {
            div.innerHTML += ' / ' + e.point.DISTRICTNAME;
            const topology = await fetch(
                'VillageWard.ashx?Block=' + e.point.value + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType")
            ).then(response => response.json());
            BindMapDataBlock(topology, "Village", e.point.value, "0", "0", 3);

        }
    }
};
var UNDF;
var UNDF;

function Bindget() {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "MapvillagepointFilter.aspx/get",           
            data: JSON.stringify({ DistrictId: 230 }),
            dataType: "json",
            success: function (data) {
              
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                if (XMLHttpRequest.status == 0) {
                    alert(' Check Your Network.');
                } else if (XMLHttpRequest.status == 404) {
                    alert('Requested URL not found.');
                } else if (XMLHttpRequest.status == 500) {
                    alert('Internel Server Error.');
                } else {
                    alert('Unknow Error.\n' + XMLHttpRequest.responseText);
                }
            }
        });

    return false;
}



const drillup = function (e) {
    if (e.seriesOptions.custom && e.seriesOptions.custom.mapView) {
        e.target.mapView.update(e.seriesOptions.custom.mapView, false);
    }
    this.legend.title.attr({ text: 'भू - समाधान गृह विभाग, बिहार सरकार<br /> <span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>' });
    if (x == 0) {
        $(this.container).find('.highcharts-title').text('भू - समाधान गृह विभाग, बिहार सरकार');
        x = 1;
    }
    table.destroy();
    $('#LandDistibuteDataTable').empty();
    $('#LandDistibuteDataTable').append('<thead><tr class="bg-black text-white">' +
        '<th> क्रम संख्या </th>' +
        '<th> जिला </th>' +
        '<th> सामान्य </th>' +
        '<th> संवेदनशील </th>' +
        '<th> अतिसंवेदनशील  </th>' +
        '<th> प्रारंभिक निष्पादन </th>' +
        '<th> अस्वीकृत </th>' +
        '<th> मापी क़े लिए निर्धारित </th>' +
        '<th> प्रक्रियाधीन </th>' +
        '<th> अंतिम निष्पादन </th>' +

        '</tr></thead>');
    $('#LandDistibuteDataTable').append('<tbody>');
    dataDistrict.forEach((d, i) => {

        $('#LandDistibuteDataTable').append('<tr >' +
            '<td> ' + (parseInt(i) + 1) + ' </td>' +
            '<td> ' + d.properties['DISTRICTNAME'] + ' </td>' +
            '<td> ' + d.properties['Samanya'] + ' </td>' +
            '<td> ' + d.properties['Sumvadansheel'] + ' </td>' +
            '<td> ' + d.properties['AtiSumvadansheel'] + ' </td>' +
            '<td> ' + d.properties['Nirast'] + ' </td>' +
            '<td> ' + d.properties['Ashwikrit'] + ' </td>' +
            '<td> ' + d.properties['Mapi_Nirdharit'] + ' </td>' +
            '<td> ' + d.properties['Prakriyadhin'] + ' </td>' +
            '<td> ' + d.properties['FinalNirast'] + ' </td>' +

            '</tr > ');

    });
    $('#LandDistibuteDataTable').append('</tbody>');
    table = $("#LandDistibuteDataTable").DataTable({
        "paging": true,
        "lengthChange": true,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": true,
        "sDom": 'lfrtip'
    });
};
function BindMapDataLoad(distcode, savedansheelta) {
    (async () => {

        var total = 0;
        var cboxes = document.getElementsByName('chkLandDispute');


        var len = cboxes.length;

        datainfo = "";
        count = 0;
        countTotal = 0;










        function getJSON(url, cb) {
            const request = new XMLHttpRequest();
            request.open('GET', url, true);

            request.onload = function () {
                if (this.status < 400) {
                    return cb(JSON.parse(this.response));
                }
            };

            request.send();
        }

        var dateFrom = document.getElementById("txtdatefrom");
        var dateTo = document.getElementById("txtDateTo");
        const newData = await fetch(
            'HandlerFilter.ashx?Block=0&fromdate=' + dateFrom.value + '&todate=' + dateTo.value + '&BhumiVivadType=0&District_Code=' + distcode + '&panchayatcode=0&savedansheelta=' + savedansheelta + '&Matter_Status=0&VillageWard=0'//+ '&thanacode=' + thanacode
        ).then(response => response.json());

        function getTemp(point, datapoint) {

            const url = 'https://api.met.no/weatherapi/locationforecast/2.0/?lat=' +
                parseFloat(point[1]) + '&lon=' + parseFloat(point[2]);

            const callBack = json => {

                //const temp = json.properties.timeseries[0].data.instant.details
                //    .air_temperature;                                            
                const pointdata = {
                    name: point[3],
                    Vill_Name: point[0],
                    lat: parseFloat(point[1]),
                    lon: parseFloat(point[2]),
                    Total: parseFloat(point[4]),
                    Sd_Name_En: point[5],
                    BlockName: point[6],
                    Samanya: point[7],
                    Sumvadansheel: point[8],
                    AtiSumvadansheel: point[9],
                    Nirast: point[10],
                    Prakriyadhin: point[11],
                    Ashwikrit: point[12],
                    Mapi_Nirdharit: point[13],
                    FinalNirast: point[14],
                    color: point[15],
                    Police_Station: point[17],
                    PanchayatName: point[18],
                    AreaType: point[19],
                    Village: point[20],
                };
                datapoint.addPoint(pointdata);

            };

            getJSON(url, callBack);
        }




        var klSamanya = 0;
        var klSumvadansheel = 0;
        var klAtiSumvadansheel = 0;
        var p = "0";
       

        localStorage.setItem("BhumiVivadType", p);


       

        const topology = await fetch(
            'DistrictAll.ashx?FromDate=' + dateFrom.value + '&ToDate=' + dateTo.value + '&BhumiVivadType=' + p + '&DistCode=' + distcode + '&savedansheelta=' + savedansheelta
        ).then(response => response.json());
        const data = Highcharts.geojson(topology);
        dataDistrict = data;


        const Seriesdata0 = [];

        var dataDoughnut = "";
        if (table) {
            table.destroy();
        }

        var TotalSamanya = 0;
        var TotalSumvadansheel = 0;
        var TotalAtiSumvadansheel = 0;
        var TotalNirast = 0;
        var TotalAshwikrit = 0;
        var TotalMapi_Nirdharit = 0;
        var TotalPrakriyadhin = 0;
        var TotalFinalNirast = 0;

        
        data.forEach((d, i) => {
           // d.drilldown = d.properties['District_Code'];
            d.DISTRICTNAME = d.properties['DISTRICTNAME'];
            d.value = d.properties['District_Code'];
            d.ParameterType = "1";
            d.Per = d.properties['Per'];

            d.Total = d.properties['Total'];
            d.Samanya = d.properties['Samanya'];
            d.Sumvadansheel = d.properties['Sumvadansheel'];
            d.AtiSumvadansheel = d.properties['AtiSumvadansheel'];
            d.Nirast = d.properties['Nirast'];
            d.Ashwikrit = d.properties['Ashwikrit'];
            d.Mapi_Nirdharit = d.properties['Mapi_Nirdharit'];
            d.Prakriyadhin = d.properties['Prakriyadhin'];
            d.FinalNirast = d.properties['FinalNirast'];
            //BSeriesdata0.push(d);      
            TotalSamanya = TotalSamanya + parseInt(d.properties['Samanya']);
            TotalSumvadansheel = TotalSumvadansheel + parseInt( d.properties['Sumvadansheel']);
            TotalAtiSumvadansheel = TotalAtiSumvadansheel + parseInt( d.properties['AtiSumvadansheel']);
            TotalNirast = TotalNirast + parseInt( d.properties['Nirast']);
            TotalAshwikrit = TotalAshwikrit + parseInt(d.properties['Ashwikrit']);
            TotalMapi_Nirdharit = TotalMapi_Nirdharit + parseInt( d.properties['Mapi_Nirdharit']);
            TotalPrakriyadhin = TotalPrakriyadhin + parseInt(d.properties['Prakriyadhin']);
            TotalFinalNirast = TotalFinalNirast + parseInt(d.properties['FinalNirast']);
            Seriesdata0.push(d);

            klSamanya = klSamanya + parseInt(d.properties['Samanya']);
            klSumvadansheel = klSumvadansheel + parseInt(d.properties['Sumvadansheel']);
            klAtiSumvadansheel = klAtiSumvadansheel + parseInt(d.properties['AtiSumvadansheel']);


           

            dataDoughnut = dataDoughnut + "{ DISTRICTNAME : '" + d.properties['DISTRICTNAME'] + "', y : " + d.properties['DISTRICTNAME'] + "},";



        });

       
        dataDoughnut = dataDoughnut.slice(0, -1);
      
        // Instantiate the map
        Highcharts.mapChart('containermap', {
            chart:
            {
                map: topology,
                animation: false,
                events:
                {

                    drilldown,
                    drillup,
                    load: function () {
                        var dataall = this.series[1];
                        newData.forEach(function (elem) {
                            getTemp(elem, dataall);
                        });

                    }
                }
            },
            title:
            {
                text: chartTitle,
                style: {
                    fontSize: '20px',
                    fontWeight: 'bold',
                    color: 'black'
                }
            },
            subtitle:
            {
                text: dataOnDate,
                style: {
                    fontSize: '15px',
                    fontWeight: 'bold',
                    color: 'black'
                }
            },
            mapNavigation:
            {
                enabled: true,
                buttonOptions:
                {
                    verticalAlign: 'top', horizontalAlign: 'right'
                }
            },

            tooltip: {
                enabled: false
                //headerFormat: '',
               // pointFormat: '<b>District : {point.name}<br/>SubDivision : {point.Sd_Name_En}<br/>Circle : {point.BlockName}<br/>Police Station : {point.Police_Station}<br/>Panchayat : {point.PanchayatName}<br/>{point.AreaType} : {point.Vill_Name}<br/>Total : {point.Total}</b>'

            },

            plotOptions:
            {

                map:
                {
                    joinBy: ['DISTRICTNAME', 'DISTRICTNAME'],
                    states:
                    {
                        hover:
                        {
                            color: '#EEDD66'
                        }
                    }

                },

            },
            legend:
            {
                title:
                {
                    //' + legendTitle+'
                    text: '<span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>',
                    style:
                    {
                        fontStyle: 'italic'
                    }
                },
                useHTML: true,
                reversed: true,
                labelFormatter: function () {
                    return `<span style="color:${this.color};">${this.name}</span>`;
                },
                layout: 'vertical',
                borderWidth: 0,
                align: 'right',
                y: -150,
                x: -50
            },
            plotOptions: {
                series: {
                    //textDecoration: 'none',
                    //textOutline: false,

                    dataLabels: {

                        formatter: function () {
                            if (this.series.chart.drilldownLevels !== UNDF && this.series.chart.drilldownLevels.length > 0) {
                                var datainfoData = "";



                                if (this.point.properties.BlockName != undefined) {
                                    datainfoData = '<b style="font-size:20px;">' + this.point.properties.BlockName + '<br/>';
                                }
                                else if (this.point.properties.PanchayatName != undefined) {
                                    datainfoData = '<b style="font-size:20px;">' + this.point.properties.PanchayatName + '<br/>';
                                }

                                datainfoData = datainfoData + 'Total -<p style="font-size:10px;">' + this.point.properties.Total + '</p>';
                                return datainfoData;
                            }
                            else {

                                var datainfoData = '<b style="font-size:8px;">' + this.point.properties.DISTRICTNAME + '<br/>';

                                datainfoData = datainfoData + 'Total-<p style="font-size:6px;">' + this.point.properties.Total + '</p>';
                                return datainfoData;

                            }
                        }
                    }
                }
            },
            series:
                [
                    {
                        data: Seriesdata0,
                        name: '<span style="font- size: 9px; color: #008000; font- weight: bold">सामान्य : ' + klSamanya + '</span>' + '<br/><span style="font- size: 9px; color: #FFA500; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>' + '<br/><span style="font- size: 9px; color: #FF0000; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span>',
                        dataLabels:
                        {
                            enabled: true,
                            shadow: true,
                            className: 'DataLabelCss'
                        },
                        color: '#F2F3F5',

                        borderColor: 'red',
                        borderWidth: 1
                    },
                    {
                        name: 'Data',
                        type: 'mappoint',
                        showInLegend: false,
                        marker: {
                            lineWidth: 1,
                            lineColor: '#000'
                        },
                        dataLabels: {

                            crop: true,
                            formatter: function () {

                                return "";
                            }
                        },
                        accessibility: {
                            point: {
                                //valueDescriptionFormat: '{xDescription}, {point.temp}°C.'
                                valueDescriptionFormat: 'jhbjhbj'
                            }
                        },

                    }




                ],
            drilldown:
            {
                breadcrumbs:
                {
                    showFullPath: true,
                    formatter: function (level) {
                        if (level.levelOptions.drilldown !== UNDF && level.levelOptions.drilldown.length > 0) {
                            return level.levelOptions.name
                        }
                        else {
                            return "DISTRICT WISE"
                        }
                    }
                },

                activeDataLabelStyle:
                {
                    color: 'Black',


                    textDecoration: 'none',

                    textOutline: false
                },
                drillUpButton: {
                    relativeTo: 'spacingBox',

                    position: {
                        y: 0,
                        x: 10
                    },
                    theme: {
                        fill: 'white',
                        'stroke-width': 1,
                        stroke: 'silver',
                        r: 0,
                        states: {
                            hover: {
                                fill: '#a4edba'
                            },
                            select: {
                                stroke: '#039',
                                fill: '#a4edba'
                            }
                        }
                    }

                },
                series: []

            }

        });

       

    })();
}



function BindDataAccordingToFilter(Type, district_code, BlockCode, panchayatcode, VillageWard, savedansheelta, Procedure, Matter_Status) {
    (async () => {
       

        var total = 0;
        var cboxes = document.getElementsByName('chkLandDispute');


        var len = cboxes.length;
       
        datainfo = "";
        count = 0;
        countTotal = 0;
        const topology = await fetch(
            'FilterData.ashx?District=' + district_code + '&BlockCode=' + BlockCode + '&panchayatcode=' + panchayatcode + '&VillageWard=' + VillageWard + '&savedansheelta=' + savedansheelta + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType") + '&Type=' + Type + '&Procedure=' + Procedure + '&Matter_Status=' + Matter_Status //+ '&thanacode=' + thanacode
        ).then(response => response.json());


        var type = Type;
        //localStorage.setItem("displaydivmap", div);

        function getJSON(url, cb) {
            const request = new XMLHttpRequest();
            request.open('GET', url, true);

            request.onload = function () {
                if (this.status < 400) {
                    return cb(JSON.parse(this.response));
                }
            };

            request.send();
        }


        const newData = await fetch(
            'HandlerFilter.ashx?Block=' + BlockCode + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType") + '&District_Code=' + district_code + '&panchayatcode=' + panchayatcode + '&savedansheelta=' + savedansheelta + '&Matter_Status=' + Matter_Status + '&VillageWard=' + VillageWard //+ '&thanacode=' + thanacode
        ).then(response => response.json());

        function getTemp(point, datapoint) {

            const url = 'https://api.met.no/weatherapi/locationforecast/2.0/?lat=' +
                parseFloat(point[1]) + '&lon=' + parseFloat(point[2]);

            const callBack = json => {

                //const temp = json.properties.timeseries[0].data.instant.details
                //    .air_temperature;                                            
                const pointdata = {
                    name: point[3],
                    Vill_Name: point[0],
                    lat: parseFloat(point[1]),
                    lon: parseFloat(point[2]),
                    Total: parseFloat(point[4]),
                    Sd_Name_En: point[5],
                    BlockName: point[6],
                    Samanya: point[7],
                    Sumvadansheel: point[8],
                    AtiSumvadansheel: point[9],
                    Nirast: point[10],
                    Prakriyadhin: point[11],
                    Ashwikrit: point[12],
                    Mapi_Nirdharit: point[13],
                    FinalNirast: point[14],
                    color: point[15],
                    Police_Station: point[17],
                    PanchayatName: point[18],
                    AreaType: point[19],
                    Village: point[20],
                };
                datapoint.addPoint(pointdata);

            };

            getJSON(url, callBack);
        }



        var klSamanya = 0;
        var klSumvadansheel = 0;
        var klAtiSumvadansheel = 0;
        var p = "0";


        localStorage.setItem("BhumiVivadType", p);


        var dateFrom = document.getElementById("txtdatefrom");
        var dateTo = document.getElementById("txtDateTo");


        const data = Highcharts.geojson(topology);
        dataDistrict = data;


        const Seriesdata0 = [];

        var dataDoughnut = "";
        if (table) {
            table.destroy();
        }
        var TotalSamanya = 0;
        var TotalSumvadansheel = 0;
        var TotalAtiSumvadansheel = 0;
        var TotalNirast = 0;
        var TotalAshwikrit = 0;
        var TotalMapi_Nirdharit = 0;
        var TotalPrakriyadhin = 0;
        var TotalFinalNirast = 0;
       
        data.forEach((d, i) => {
           // d.drilldown = d.properties['BlockCode'];
            d.DISTRICTNAME = d.properties['BlockName'];
            d.value = d.properties['BlockCode'];
            //if (div == "1") {
            //    d.ParameterType = "2";
            //}
            //else if (div == "2") {
            //    d.ParameterType = "3";
            //}
            //else if (div == "3") {
            //    d.ParameterType = "4";
            //}
            d.Per = d.properties['Per'];

            d.Total = d.properties['Total'];
            d.Samanya = d.properties['Samanya'];
            d.Sumvadansheel = d.properties['Sumvadansheel'];
            d.AtiSumvadansheel = d.properties['AtiSumvadansheel'];
            d.Nirast = d.properties['Nirast'];
            d.Ashwikrit = d.properties['Ashwikrit'];
            d.Mapi_Nirdharit = d.properties['Mapi_Nirdharit'];
            d.Prakriyadhin = d.properties['Prakriyadhin'];
            d.FinalNirast = d.properties['FinalNirast'];


            TotalSamanya = TotalSamanya + parseInt(d.properties['Samanya']);
            TotalSumvadansheel = TotalSumvadansheel + parseInt(d.properties['Sumvadansheel']);
            TotalAtiSumvadansheel = TotalAtiSumvadansheel + parseInt(d.properties['AtiSumvadansheel']);
            TotalNirast = TotalNirast + parseInt(d.properties['Nirast']);
            TotalAshwikrit = TotalAshwikrit + parseInt(d.properties['Ashwikrit']);
            TotalMapi_Nirdharit = TotalMapi_Nirdharit + parseInt(d.properties['Mapi_Nirdharit']);
            TotalPrakriyadhin = TotalPrakriyadhin + parseInt(d.properties['Prakriyadhin']);
            TotalFinalNirast = TotalFinalNirast + parseInt(d.properties['FinalNirast']);

            Seriesdata0.push(d);

            klSamanya = klSamanya + parseInt(d.properties['Samanya']);
            klSumvadansheel = klSumvadansheel + parseInt(d.properties['Sumvadansheel']);
            klAtiSumvadansheel = klAtiSumvadansheel + parseInt(d.properties['AtiSumvadansheel']);


          
            dataDoughnut = dataDoughnut + "{ DISTRICTNAME : '" + d.properties['BlockName'] + "', y : " + d.properties['BlockName'] + "},";



        });


        dataDoughnut = dataDoughnut.slice(0, -1);
       

        Highcharts.mapChart('containermap', {
            chart:
            {
                map: topology,
                animation: false,
                events:
                {

                    drilldown,
                    drillup,
                    load: function () {
                        var dataall = this.series[1];
                        newData.forEach(function (elem) {
                            getTemp(elem, dataall);
                        });

                    }

                }
            },
            title:
            {
                text: chartTitle,
                style: {
                    fontSize: '20px',
                    fontWeight: 'bold',
                    color: 'black'
                }
            },
            subtitle:
            {
                text: dataOnDate,
                style: {
                    fontSize: '15px',
                    fontWeight: 'bold',
                    color: 'black'
                }
            },
            mapNavigation:
            {
                enabled: true,
                buttonOptions:
                {
                    verticalAlign: 'top', horizontalAlign: 'right'
                }
            },

            tooltip: {
                headerFormat: '',
                pointFormat: '<b>District : {point.name}<br/>SubDivision : {point.Sd_Name_En}<br/>Circle : {point.BlockName}<br/>Police Station : {point.Police_Station}<br/>Panchayat : {point.PanchayatName}<br/>{point.AreaType} : {point.Vill_Name}<br/>Total : {point.Total}</b>'

            },

            plotOptions:
            {

                map:
                {
                    joinBy: ['BlockName', 'BlockName'],
                    states:
                    {
                        hover:
                        {
                            color: '#EEDD66'
                        }
                    }

                },

            },
            legend:
            {
                title:
                {
                    //' + legendTitle+'
                    text: '<span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>',
                    style:
                    {
                        fontStyle: 'italic'
                    }
                },
                useHTML: true,
                reversed: true,
                labelFormatter: function () {
                    return `<span style="color:${this.color};">${this.name}</span>`;
                },
                layout: 'vertical',
                borderWidth: 0,
                align: 'right',
                y: -150,
                x: -50
            },
            plotOptions: {
                series: {
                    //textDecoration: 'none',
                    //textOutline: false,

                    dataLabels: {

                        formatter: function () {
                            if (this.series.chart.drilldownLevels !== UNDF && this.series.chart.drilldownLevels.length > 0) {
                                var datainfoData = "";



                                if (this.point.properties.BlockName != undefined) {
                                    datainfoData = '<b style="font-size:20px;">' + this.point.properties.BlockName + '<br/>';
                                }
                                else if (this.point.properties.PanchayatName != undefined) {
                                    datainfoData = '<b style="font-size:20px;">' + this.point.properties.PanchayatName + '<br/>';
                                }

                                datainfoData = datainfoData + 'Total -<p style="font-size:10px;">' + this.point.properties.Total + '</p>';
                                return datainfoData;
                            }
                            else {

                                var datainfoData = '<b style="font-size:8px;">' + this.point.properties.BlockName + '<br/>';

                                datainfoData = datainfoData + 'Total-<p style="font-size:6px;">' + this.point.properties.Total + '</p>';
                                return datainfoData;

                            }
                        }
                    },
                    events: {
                        click: function (e) {

                            console.log(e);
                            console.log(e.point.Village);
                           // alert(e.FinalNirast);
                            //window.location.replace("DisplayData.aspx?PS_Code=" + e.point.PS_Code, '_blank');
                           

                           // window.open("DisplayData.aspx?Village=" + e.point.Village + "&AreaType=" + e.point.AreaType +"&FromDate=" + document.getElementById('txtdatefrom').value + "&ToDate=" + document.getElementById('txtDateTo').value, "_blank");
                        }
                    },
                }
            },
            series:
                [
                    {
                        data: Seriesdata0,
                        name: '<span style="font- size: 9px; color: #008000; font- weight: bold">सामान्य : ' + klSamanya + '</span>' + '<br/><span style="font- size: 9px; color: #FFA500; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>' + '<br/><span style="font- size: 9px; color: #FF0000; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span>',
                        dataLabels:
                        {
                            enabled: true,
                            shadow: true,
                            className: 'DataLabelCss'
                        },
                        color: '#F2F3F5',

                        borderColor: 'red',
                        borderWidth: 1
                    },
                    {
                        name: 'Data',
                        type: 'mappoint',
                        showInLegend: false,
                        marker: {
                            lineWidth: 1,
                            lineColor: '#000'
                        },
                        dataLabels: {

                            crop: true,
                            formatter: function () {

                                return "";
                            }
                        },
                        accessibility: {
                            point: {
                                //valueDescriptionFormat: '{xDescription}, {point.temp}°C.'
                                valueDescriptionFormat: 'jhbjhbj'
                            }
                        },

                    }


                ],
            drilldown:
            {
                breadcrumbs:
                {
                    showFullPath: true,
                    formatter: function (level) {
                        if (level.levelOptions.drilldown !== UNDF && level.levelOptions.drilldown.length > 0) {
                            return level.levelOptions.name
                        }
                        else {
                            return "DISTRICT WISE"
                        }
                    }
                },

                activeDataLabelStyle:
                {
                    color: 'Black',


                    textDecoration: 'none',

                    textOutline: false
                },
                drillUpButton: {
                    relativeTo: 'spacingBox',

                    position: {
                        y: 0,
                        x: 10
                    },
                    theme: {
                        fill: 'white',
                        'stroke-width': 1,
                        stroke: 'silver',
                        r: 0,
                        states: {
                            hover: {
                                fill: '#a4edba'
                            },
                            select: {
                                stroke: '#039',
                                fill: '#a4edba'
                            }
                        }
                    }

                },
                series: []

            }

        });
     

        //if (div == "1") {
        //    localStorage.setItem("blocktabledata", table.innerHTML);
        //}
        //else {
        //    localStorage.setItem("panchayattabledata", table.innerHTML);
        //}
    })();
}