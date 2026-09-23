
function BindBlock(districtCode) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "MapvillagepointFilter.aspx/GetBlock",
            data: JSON.stringify({ DistrictId: districtCode }),
            dataType: "json",
            success: function (data) {
                $("#ddBlock").empty();
                $("#ddBlock").append($("<option></option>").val("0").html("All"));
                $.each(data.d, function (key, value) {

                    $("#ddBlock").append($("<option></option>").val(value.BlockCode).html(value.BlockName));
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

function BindPanchayat(BlockCode) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "MapvillagepointFilter.aspx/GetPanchayat",
            data: JSON.stringify({ BlockCode: BlockCode }),
            dataType: "json",
            success: function (data) {
                $("#ddPanchayat").empty();
                $("#ddPanchayat").append($("<option class='panchyat'  data-areatype=''></option>").val("0").html("All"));
                $.each(data.d, function (key, value) {

                    $("#ddPanchayat").append($("<option class='panchyat' data-areatype='" + value.AreaType +"'></option>").val(value.PanchayatCode).html(value.PanchayatName));
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
$(document).ready(function () {
    BindMapDataLoad("0", "0");
    BindBlock("0");
    BindPanchayat("0");
    BindWardVillage("0", "U");
    $("#ddDistrict").change(function () {
        BindBlock($("#ddDistrict").val());
        BindPanchayat($("#ddBlock").val());
        BindWardVillage($("#ddPanchayat").val(), "U");
       // BindDataAccordingToFilter("District", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_GetDistirctWiseDetailsFilter", $("#ddlaction").val());
        BindDataAccordingToFilter("Block", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getBlockMapFilter", $("#ddlaction").val());

        //BindMapData($("#ddDistrict").val(), $("#ddSensivity").val(), $("#ddlaction").val(), "0");
    }); 
    $("#ddBlock").change(function () {
       
        
        BindPanchayat($("#ddBlock").val());
        BindWardVillage($("#ddPanchayat").val(), "U");
       // BindDataAccordingToFilter("Block", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getBlockMapFilter", $("#ddlaction").val());
        BindDataAccordingToFilter("Panchayat", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getPanchayatMapFilter", $("#ddlaction").val());
    });
    $("#ddPanchayat").change(function () {
        var type = "";
        var element = $(this).find('option:selected');
        var areatype = element.attr("data-areatype");
        if (areatype == 'R') {
            document.getElementById('GramWard').innerHTML = 'ग्राम';
            type = 'ग्राम';
        }
        if (areatype == 'U') {
            document.getElementById('GramWard').innerHTML = 'वार्ड';
            type = 'वार्ड';
        }
        BindWardVillage($("#ddPanchayat").val(), areatype);
        //BindDataAccordingToFilter("Panchayat", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getPanchayatMapFilter", $("#ddlaction").val());
        BindDataAccordingToFilter(type, $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getVillageWardMapFilter", $("#ddlaction").val());
    });
    $("#ddGramAWard").change(function () {
        var type = "";
        var element = $("#ddPanchayat").find('option:selected');
        var areatype = element.attr("data-areatype");
        if (areatype == 'R') {
            type = 'ग्राम';
        }
        if (areatype == 'U') {
            type = 'वार्ड';
        }
       
       // BindDataAccordingToFilter(type, $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getVillageWardMapFilter", $("#ddlaction").val());
        BindDataAccordingToFilter(type, $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getVillageWardMapFilter", $("#ddlaction").val());
    });

    $("#ddSensivity").change(function () {
        getSenAction();
    });
    $("#ddlaction").change(function () {
        getSenAction();
    });
});
function getSenAction() {
    if ($("#ddGramAWard").val() != "0") {
        var type = "";
        var element = $("#ddPanchayat").find('option:selected');
        var areatype = element.attr("data-areatype");
        if (areatype == 'R') {
            type = 'ग्राम';
        }
        if (areatype == 'U') {
            type = 'वार्ड';
        }
        BindDataAccordingToFilter(type, $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getVillageWardMapFilter", $("#ddlaction").val());
    }
    else if ($("#ddPanchayat").val() != "0") {
        BindDataAccordingToFilter("Panchayat", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getPanchayatMapFilter", $("#ddlaction").val());
    }
    else if ($("#ddBlock").val() != "0") {
        BindDataAccordingToFilter("Block", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_getBlockMapFilter", $("#ddlaction").val());
    }
    else if ($("#ddDistrict").val() != "0") {
        BindDataAccordingToFilter("District", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddGramAWard").val(), $("#ddSensivity").val(), "usp_GetDistirctWiseDetailsFilter", $("#ddlaction").val());
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














        var klSamanya = 0;
        var klSumvadansheel = 0;
        var klAtiSumvadansheel = 0;
        var p = "0";
       

        localStorage.setItem("BhumiVivadType", p);


        var dateFrom = document.getElementById("txtdatefrom");
        var dateTo = document.getElementById("txtDateTo");

        const topology = await fetch(
            'District.ashx?FromDate=' + dateFrom.value + '&ToDate=' + dateTo.value + '&BhumiVivadType=' + p + '&DistCode=' + distcode + '&savedansheelta=' + savedansheelta
        ).then(response => response.json());
        const data = Highcharts.geojson(topology);
        dataDistrict = data;


        const Seriesdata0 = [];

        var dataDoughnut = "";
        if (table) {
            table.destroy();
        }
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

            Seriesdata0.push(d);

            klSamanya = klSamanya + parseInt(d.properties['Samanya']);
            klSumvadansheel = klSumvadansheel + parseInt(d.properties['Sumvadansheel']);
            klAtiSumvadansheel = klAtiSumvadansheel + parseInt(d.properties['AtiSumvadansheel']);


            // lblTotalAffected.innerHTML = count;
            // lbltotalDist.innerHTML = countTotal;
            //lblAffDist.innerHTML = "landispute";
            //lblDist.innerHTML = "landispute Districts";

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

            dataDoughnut = dataDoughnut + "{ DISTRICTNAME : '" + d.properties['DISTRICTNAME'] + "', y : " + d.properties['DISTRICTNAME'] + "},";



        });



        dataDoughnut = dataDoughnut.slice(0, -1);
        $('#LandDistibuteDataTable').append('</tbody>');
        //table = $("#LandDistibuteDataTable").DataTable({
        //    "paging": true,
        //    "lengthChange": true,
        //    "searching": true,
        //    "ordering": true,
        //    "info": true,
        //    "autoWidth": true,
        //    "sDom": 'lfrtip'
        //});
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
                        //var dataall = this.series[1];
                        //newData.forEach(function (elem) {
                        //    getTemp(elem, dataall);
                        //});

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

                                var datainfoData = '<b style="font-size:20px;">' + this.point.properties.DISTRICTNAME + '<br/>';

                                datainfoData = datainfoData + 'Total-<p style="font-size:10px;">' + this.point.properties.Total + '</p>';
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

        var table1 = document.getElementById('tabledata');
        localStorage.setItem("districttabledata", table1.innerHTML);

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
            'FilterData.ashx?District=' + district_code + '&BlockCode=' + BlockCode + '&panchayatcode=' + panchayatcode + '&VillageWard=' + VillageWard + '&savedansheelta=' + savedansheelta + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType") + '&Type=' + Type + '&Procedure=' + Procedure + '&Matter_Status=' + Matter_Status
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
            'HandlerFilter.ashx?Block=' + BlockCode + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType") + '&District_Code=' + district_code + '&panchayatcode=' + panchayatcode + '&savedansheelta=' + savedansheelta + '&Matter_Status=' + Matter_Status + '&VillageWard=' + VillageWard
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
        $('#LandDistibuteDataTable').empty();
        $('#LandDistibuteDataTable').append('<thead><tr class="bg-black text-white">' +
            '<th> क्रम संख्या </th>' +
            '<th> ' + type + ' </th>' +
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

            Seriesdata0.push(d);

            klSamanya = klSamanya + parseInt(d.properties['Samanya']);
            klSumvadansheel = klSumvadansheel + parseInt(d.properties['Sumvadansheel']);
            klAtiSumvadansheel = klAtiSumvadansheel + parseInt(d.properties['AtiSumvadansheel']);


            $('#LandDistibuteDataTable').append('<tr >' +
                '<td> ' + (parseInt(i) + 1) + ' </td>' +
                '<td> ' + d.properties['BlockName'] + ' </td>' +
                '<td> ' + d.properties['Samanya'] + ' </td>' +
                '<td> ' + d.properties['Sumvadansheel'] + ' </td>' +
                '<td> ' + d.properties['AtiSumvadansheel'] + ' </td>' +
                '<td> ' + d.properties['Nirast'] + ' </td>' +
                '<td> ' + d.properties['Ashwikrit'] + ' </td>' +
                '<td> ' + d.properties['Mapi_Nirdharit'] + ' </td>' +
                '<td> ' + d.properties['Prakriyadhin'] + ' </td>' +
                '<td> ' + d.properties['FinalNirast'] + ' </td>' +
                '</tr > ');

            dataDoughnut = dataDoughnut + "{ DISTRICTNAME : '" + d.properties['BlockName'] + "', y : " + d.properties['BlockName'] + "},";



        });



        dataDoughnut = dataDoughnut.slice(0, -1);
        $('#LandDistibuteDataTable').append('</tbody>');

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

                                var datainfoData = '<b style="font-size:20px;">' + this.point.properties.BlockName + '<br/>';

                                datainfoData = datainfoData + 'Total-<p style="font-size:10px;">' + this.point.properties.Total + '</p>';
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
                           

                            window.open("DisplayData.aspx?Village=" + e.point.Village + "&FromDate=" + document.getElementById('txtdatefrom').value + "&ToDate=" + document.getElementById('txtDateTo').value, "_blank");
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
        var table = document.getElementById('tabledata');

        //if (div == "1") {
        //    localStorage.setItem("blocktabledata", table.innerHTML);
        //}
        //else {
        //    localStorage.setItem("panchayattabledata", table.innerHTML);
        //}
    })();
}