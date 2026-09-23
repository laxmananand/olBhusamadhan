function BindBlock(districtCode, ThanaCode) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "Bhu-Dashboard-graph.aspx/GetBlock",
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
                //BindData($("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddlPoliceSation").val(), $("#ddSensivity").val(), $("#ddlaction").val(), $("#txtdatefrom").val(), $("#txtDateTo").val());
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
function BindThana(districtCode, BlockCode) {
    $.ajax(
        {
            type: "POST",
            contentType: "application/json;charset=utf-8",
            url: "Bhu-Dashboard-graph.aspx/GetThana",
            data: JSON.stringify({ DistrictId: districtCode, BlockCode: BlockCode }),
            dataType: "json",
            success: function (data) {
                $("#ddlPoliceSation").empty();
                $("#ddlPoliceSation").append($("<option></option>").val("0").html("All"));
                $.each(data.d, function (key, value) {
                    $("#ddlPoliceSation").append($("<option></option>").val(value.PS_Code).html(value.Police_Station));
                });
                if ($("#hdnThanaCode").val() != "0") {
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
            url: "Bhu-Dashboard-graph.aspx/GetPanchayat",
            data: JSON.stringify({ BlockCode: BlockCode }),
            dataType: "json",
            success: function (data) {
                $("#ddPanchayat").empty();
                $("#ddPanchayat").append($("<option class='panchyat'  data-areatype=''></option>").val("0").html("All"));
                $.each(data.d, function (key, value) {

                    $("#ddPanchayat").append($("<option class='panchyat' data-areatype='" + value.AreaType + "'></option>").val(value.PanchayatCode).html(value.PanchayatName));
                });
                // getSenAction();
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
function BindWardVillage(Panchayatcode, AreaType) {
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

    sessionStorage.setItem("Submenu", "");
    sessionStorage.setItem("Id", "1");
    sessionStorage.setItem("DataValue", "");

    BindBlock($("#ddDistrict").val(), $("#hdnThanaCode").val());

    $("#ddDistrict").change(function () {
        BindBlock($("#ddDistrict").val(), $("#hdnThanaCode").val());
    });
    $("#ddBlock").change(function () {
        if ($("#hdnThanaCode").val() == "0") {
            BindThana($("#ddDistrict").val(), $("#ddBlock").val());
        }

        BindPanchayat($("#ddBlock").val());
    });

    $("#btnSearch").click(function (event) {
        event.preventDefault();
        var rid = $(".active").attr("data-id");
        var rrid = $(".active").attr("data-rt");
        PiechartFun(rid, rrid);
        getBarGraph(rid, "");
        localStorage.setItem("step", rid);
        localStorage.setItem("filter", 0);
        sessionStorage.setItem("Submenu", "");
        if (rid == 5) {
            getSenAction(" and m.a_id in (select a_id from LandDetailsEntry)");
        }
        if (rid == 6) {
            getSenAction(" and m.a_id in (select a_id from Vadi_Evidence_Entry)");
        }
        if (rid == 7) {
            getSenAction(" and m.a_id in (select a_id from PratiVadi_Evidence_Entry)");

        }
        if (rid == 8) {
            getSenAction(" and m.a_id in (select a_id from LandDisputeDetailsEntry )");

        }
        if (rid == 9) {
            getSenAction(" and m.a_id in (select a_id from CourtDisputeDetailsEntry )");
        }
        else {
            getSenAction("");
        }
        DataforLineChart();
        if (rid == 1 || rid == 2) {
            $("#chartContainer2").removeClass("hidden");
        }
        else {
            $("#chartContainer2").addClass("hidden");
        }
        //line();
        //BarchartFun("10", "District Wise","usp_GetDistirctWiseDetailsBargraph");
        //BindData($("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddlPoliceSation").val(), $("#ddSensivity").val(), $("#ddlaction").val(), $("#txtdatefrom").val(), $("#txtDateTo").val());
        return false;

    });
    $("#btnClear").click(function (event) {
        event.preventDefault();
        document.getElementById("txtdatefrom").value = "";
        document.getElementById("txtDateTo").value = "";
        return false;

    });
    $("#btnBack").click(function (event) {
        event.preventDefault();
        sessionStorage.setItem("Submenu", "");
        if ($("#ddPanchayat").val() != 0) {
            $("#ddPanchayat").val(0);
            back();
        }
        else if ($("#ddBlock").val() != 0) {
            $("#ddPanchayat").val(0);
            $("#ddBlock").val(0);
            BindPanchayat("0");
            if ($("#hdnThanaCode").val() == "0") {
                BindThana($("#ddDistrict").val(), $("#ddBlock").val());
            }

            BindPanchayat($("#ddBlock").val());
            back();
        }
        else if ($("#ddDistrict").val() != 0) {
            $("#ddPanchayat").val(0);
            $("#ddBlock").val(0);
            $("#ddDistrict").val(0);
            BindBlock($("#ddDistrict").val(), $("#hdnThanaCode").val());
            back();
        }

        return false;

    });
    $(".viewdata").click(function (event) {
        event.preventDefault();
        var id = $(this).attr("data-id");
        var datavalue = $(this).attr("data-value");

        alert(id + " " + datavalue);
        return false;
    });
});
////sensivity
function back() {
    var rid = $(".active").attr("data-id");
    var rrid = $(".active").attr("data-rt");
    PiechartFun(rid, rrid);
    getBarGraph(rid, "");
    localStorage.setItem("step", rid);
    localStorage.setItem("filter", 0);

    if (rid == 5) {
        getSenAction(" and m.a_id in (select a_id from LandDetailsEntry)");
    }
    if (rid == 6) {
        getSenAction(" and m.a_id in (select a_id from Vadi_Evidence_Entry)");
    }
    if (rid == 7) {
        getSenAction(" and m.a_id in (select a_id from PratiVadi_Evidence_Entry)");

    }
    if (rid == 8) {
        getSenAction(" and m.a_id in (select a_id from LandDisputeDetailsEntry )");

    }
    if (rid == 9) {
        getSenAction(" and m.a_id in (select a_id from CourtDisputeDetailsEntry )");
    }
    else {
        getSenAction("");
    }
    DataforLineChart();
    if (rid == 1 || rid == 2) {
        $("#chartContainer2").removeClass("hidden");
    }
    else {
        $("#chartContainer2").addClass("hidden");
    }
    //line();
    //BarchartFun("10", "District Wise","usp_GetDistirctWiseDetailsBargraph");
    //BindData($("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), $("#ddlPoliceSation").val(), $("#ddSensivity").val(), $("#ddlaction").val(), $("#txtdatefrom").val(), $("#txtDateTo").val());

}
function DataforLineChart() {
    var datefrom, todate, legendTitle;
    datefrom = document.getElementById("txtdatefrom").value;
    todate = document.getElementById("txtDateTo").value;
    $(function () {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            url: 'Bhu-Dashboard-graph.aspx/GetForLineGraphDetails',
            data: JSON.stringify({ DistrictId: $("#ddDistrict").val(), BlockCode: $("#ddBlock").val(), PanchayatCode: $("#ddPanchayat").val(), thana_code: $("#ddlPoliceSation").val(), savedansheelta: $("#ddSensivity").val(), Matter_Status: $("#ddlaction").val(), FromDate: datefrom, ToDate: todate, step: 0 }),


            success: function (response) {
                var displya = "";



                lineGraph(response.d);
            },

            error: function () {
                alert("Error loading data! Please try again. bar");
            }
        });
    })
}
function lineGraph(dataValues) {


    var dataPointsamanay = [];
    var dataPointsavedansheel = [];
    var dataPointatisavedansheel = [];

    for (var i = 0; i < dataValues.length; i++) {


        dataPointsamanay.push({ x: new Date(parseInt(dataValues[i].yeard), parseInt(dataValues[i].monthd) - 1, 1), y: parseInt(dataValues[i].filedsamanay) });
        dataPointsavedansheel.push({ x: new Date(parseInt(dataValues[i].yeard), parseInt(dataValues[i].monthd) - 1, 1), y: parseInt(dataValues[i].filedsavedansheel) });
        dataPointatisavedansheel.push({ x: new Date(parseInt(dataValues[i].yeard), parseInt(dataValues[i].monthd) - 1, 1), y: parseInt(dataValues[i].filedatisavedansheel) });

    }

    var chart = new CanvasJS.Chart("chartContainer2", {
        title: {
            text: "Month Wise Application Report"
        },
        axisX: {
            valueFormatString: "MMM-YYYY"
        },
        //axisX: {
        //    //Try Changing to MMMM
        //    valueFormatString: "MMM-YYYY"
        //},
        axisY: [{
            title: "सामान्य *",
            lineColor: "#008000",
            tickColor: "#008000",
            labelFontColor: "#008000",
            titleFontColor: "#008000",
            includeZero: true

        },
        {
            title: "संवेदनशील **",
            lineColor: "#FFA500",
            tickColor: "#FFA500",
            labelFontColor: "#FFA500",
            titleFontColor: "#FFA500",
            includeZero: true

        },
        {
            title: "अतिसंवेदनशील ***",
            lineColor: "#FF0000",
            tickColor: "#FF0000",
            labelFontColor: "#FF0000",
            titleFontColor: "#FF0000",
            includeZero: true

        }
        ],
        //axisY2: {
        //    title: "अतिसंवेदनशील ***",
        //    lineColor: "#FF0000",
        //    tickColor: "#FF0000",
        //    labelFontColor: "#FF0000",
        //    titleFontColor: "#FF0000",
        //    includeZero: true

        //},
        toolTip: {
            shared: true,

            contentFormatter: function (e) {
                //console.log(e.entries[0].dataPoint.y[0]);

                return CanvasJS.formatDate(e.entries[0].dataPoint.x, "MMM-YYYY") + "<br/><span style='color:#008000'>सामान्य *</span> " + e.entries[0].dataPoint.y + "<br/> <span style='color:#FFA500'>संवेदनशील **</span> " + e.entries[1].dataPoint.y + "<br/> <span style='color:#FF0000'>अतिसंवेदनशील ***</span> " + e.entries[2].dataPoint.y;

            }
        },
        legend: {
            cursor: "pointer",
            // itemclick: toggleDataSeries
        },
        data: [{
            type: "line",
            name: "सामान्य *",
            color: "#008000",
            showInLegend: true,
            //axisYIndex: 1,
            dataPoints: dataPointsamanay,

        },
        {
            type: "line",
            name: "संवेदनशील **",
            color: "#FFA500",
            //axisYIndex: 0,
            showInLegend: true,
            dataPoints: dataPointsavedansheel,
            //dataPoints: 
            //    [
            //        { x: 'Sep-2022', y: 119 },
            //        { x: 'Oct-2022', y: 0 },
            //        { x: 'Nov-2022', y: 60 },
            //        { x: 'Dec-2022', y: 22 }
            //]
        },
        {
            type: "line",
            name: "अतिसंवेदनशील ***",
            color: "#FF0000",
            // axisYType: "secondary",
            showInLegend: true,
            dataPoints: dataPointatisavedansheel,

        }]
    });
    chart.render();

    function toggleDataSeries(e) {
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
        } else {
            e.dataSeries.visible = true;
        }
        e.chart.render();
    }
}
function reportLinkClick(e, evt, opt, step, rpt) {
    e.preventDefault();
    sessionStorage.setItem("Submenu", "");
    var elmts = document.getElementsByClassName("active");
    for (i = 0; i < elmts.length; i++) {
        elmts[i].className = "inactiveLink";
    }
    evt.className = "active";
    PiechartFun(step, rpt);


    if (step == 1 || step == 2) {
        $("#chartContainer2").removeClass("hidden");
    }
    else {
        $("#chartContainer2").addClass("hidden");
    }
    localStorage.setItem("step", step);
    localStorage.setItem("filter", 0);
    if (step == 1) {
        getBarGraph("0", "");
        getSenAction("");
    }
    if (step == 2) {
        getBarGraph("3", "");
        getSenAction("");
    }
    if (step == 3) {
        getBarGraph("3", "");
        getSenAction("");
    }
    if (step == 4) {
        getBarGraph("4", "");
        getSenAction("");
    }
    if (step == 5) {
        getBarGraph("5", "");
        getSenAction(" and m.a_id in (select a_id from LandDetailsEntry)");

    }
    if (step == 6) {
        getBarGraph("6", "");
        getSenAction(" and m.a_id in (select a_id from Vadi_Evidence_Entry)");

    }
    if (step == 7) {
        getBarGraph("7", "");
        getSenAction(" and m.a_id in (select a_id from PratiVadi_Evidence_Entry)");

    }
    if (step == 8) {
        getBarGraph("8", "");
        getSenAction(" and m.a_id in (select a_id from LandDisputeDetailsEntry )");

    }
    if (step == 9) {
        getBarGraph("9", "");
        getSenAction(" and m.a_id in (select a_id from CourtDisputeDetailsEntry )");

    }
}
//map
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
var chartTitle = '';
//var chartTitle = 'State:Bihar,<br/> district:patna, <br/> Block: patna';
var table;
var dataDistrict, txtdatefrom, txtdatefrom, Message;
Message = document.getElementById("Message");
const drilldown = async function (e) {
    sessionStorage.setItem("Submenu", "");
    $("#ddSensivity").val("0");
    if (e.point.ParameterType == 1) {
        $("#ddDistrict").val(e.point.value);
        BindBlock($("#ddDistrict").val(), $("#hdnThanaCode").val());
    }
    if (e.point.ParameterType == 2) {
        $("#ddBlock").val(parseInt(e.point.value));
        BindPanchayat(parseInt(e.point.value));
    }
    if (e.point.ParameterType == 3) {

        $("#ddPanchayat").val(parseInt(e.point.value));
    }



    // getSenAction("");

    var rid = $(".active").attr("data-id");
    var rrid = $(".active").attr("data-rt");
    PiechartFun(rid, rrid);
    getBarGraph(rid, "");
    if (rid == 5) {
        getSenAction(" and m.a_id in (select a_id from LandDetailsEntry)");
    }
    if (rid == 6) {
        getSenAction(" and m.a_id in (select a_id from Vadi_Evidence_Entry)");
    }
    if (rid == 7) {
        getSenAction(" and m.a_id in (select a_id from PratiVadi_Evidence_Entry)");

    }
    if (rid == 8) {
        getSenAction(" and m.a_id in (select a_id from LandDisputeDetailsEntry )");

    }
    if (rid == 9) {
        getSenAction(" and m.a_id in (select a_id from CourtDisputeDetailsEntry )");

    }
    else {
        getSenAction("");
    }
    DataforLineChart();

    if (rid == 1 || rid == 2) {
        $("#chartContainer2").removeClass("hidden");
    }
    else {
        $("#chartContainer2").addClass("hidden");
    }
    if (!e.seriesOptions) {
        //alert(e.point.ParameterType);
        point = e.point;
        const chart = this;
        const mapKey = '';
        var div = document.getElementById('direction');




        let fail = setTimeout(() => {
            if (!Highcharts.maps[mapKey]) {
                chart.showLoading("<i class=\"icon-frown\"></i> Failed loading ${e.point.name}");
                fail = setTimeout(() => {
                    chart.hideLoading();
                }, 1000);
            }
        }, 3000);

        //$("#ddDistrict").val(e.point.value);

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
};
function BindMapDataLoadNEW(distcode, savedansheelta, con) {
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
            'HandlerFilterGraph.ashx?Block=0&fromdate=' + dateFrom.value + '&todate=' + dateTo.value + '&BhumiVivadType=0&District_Code=' + distcode + '&panchayatcode=0&savedansheelta=' + savedansheelta + '&Matter_Status=' + $("#ddlaction").val() +'&VillageWard=0' + '&thanacode=0&con=' + con
        ).then(response => response.json());

       
        function getTemp(point, datapoint) {


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


        }


        var klSamanya = 0;
        var klSumvadansheel = 0;
        var klAtiSumvadansheel = 0;
        var p = "0";


        localStorage.setItem("BhumiVivadType", p);




        const topology = await fetch(
            'DistrictAllGraph.ashx?FromDate=' + dateFrom.value + '&ToDate=' + dateTo.value + '&BhumiVivadType=' + p + '&DistCode=' + distcode + '&savedansheelta=' + savedansheelta + '&Matter_Status=' + $("#ddlaction").val() + '&VillageWard=0' + '&thanacode=' + $("#ddlPoliceSation").val() + '&con=' + con
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
            d.drilldown = d.properties['DISTRICT'];
            d.DISTRICTNAME = d.properties['DISTRICTNAME'];
            d.value = d.properties['District_Code'];
            d.ParameterType = 1;
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




            dataDoughnut = dataDoughnut + "{ DISTRICTNAME : '" + d.properties['DISTRICTNAME'] + "', y : " + d.properties['DISTRICTNAME'] + "},";



        });

        var legend = '<b><span style="font- size: 9px; color: #008000; font- weight: bold">सामान्य : ' + klSamanya + '</span>' + '&nbsp;&nbsp; <span style="font- size: 9px; color: #FFA500; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>' + '&nbsp;&nbsp; <span style="font- size: 9px; color: #FF0000; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span></b>'
        $("#legendbind").html(legend);
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
                        var dataPoint = this.series[1];
                        //dataPoint.addPoint(newData);
                        newData.forEach(function (point) {
                            getTemp(point, dataPoint)
                        });

                    }
                }
            },
            title:
            {
                text: createchartmessgae(),
                style: {
                    fontSize: '15px',
                    //fontWeight: 'bold',
                    textAlign: 'low',
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
                    },
                    events: {
                        click: function (e) {

                            // console.log(e);
                            // console.log(e.point.Village);
                            // alert(e.FinalNirast);
                            //window.location.replace("DisplayData.aspx?PS_Code=" + e.point.PS_Code, '_blank');

                            if (e.point.Village != null && e.point.Village != "") {
                                var Bhumi_savedansheelta = 0;

                                Bhumi_savedansheelta = e.point.Samanya > 0 ? 1 :
                                    e.point.Sumvadansheel > 0 ? 2 :
                                        e.point.AtiSumvadansheel > 0 ? 3 : 0;



                                //window.open("DisplayData.aspx?Village=" + e.point.Village + "&AreaType=" + e.point.AreaType + "&FromDate=" + document.getElementById('txtdatefrom').value + "&ToDate=" + document.getElementById('txtDateTo').value, "_blank");
                                window.open("DisplayData.aspx?Village=" + e.point.Village + "&AreaType=" + e.point.AreaType + "&FromDate=" + document.getElementById('txtdatefrom').value + "&ToDate=" + document.getElementById('txtDateTo').value + "&Bhumi_savedansheelta=" + Bhumi_savedansheelta + "&Action=" + $("#ddlaction").val() + "&Id=" + sessionStorage.getItem("Id") + "&DataValue=" + sessionStorage.getItem("DataValue"), "_blank");
                            }

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

            },
            legend: {
                enabled: false
            },
        });



    })();
}


function BindMapDataLoad(distcode, savedansheelta, con) {
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
        //const newData = await fetch(
        //    'HandlerFilterGraph.ashx?Block=0&fromdate=' + dateFrom.value + '&todate=' + dateTo.value + '&BhumiVivadType=0&District_Code=' + distcode + '&panchayatcode=0&savedansheelta=' + savedansheelta + '&Matter_Status=' + $("#ddlaction").val() + '&VillageWard=0' + '&thanacode=' + $("#ddlPoliceSation").val() + '&con=' + con
        //).then(response => response.json());






        var klSamanya = 0;
        var klSumvadansheel = 0;
        var klAtiSumvadansheel = 0;
        var p = "0";


        localStorage.setItem("BhumiVivadType", p);




        const topology = await fetch(
            'DistrictAllGraph.ashx?FromDate=' + dateFrom.value + '&ToDate=' + dateTo.value + '&BhumiVivadType=' + p + '&DistCode=' + distcode + '&savedansheelta=' + savedansheelta + '&Matter_Status=' + $("#ddlaction").val() + '&VillageWard=0' + '&thanacode=' + $("#ddlPoliceSation").val() + '&con=' + con
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
            d.drilldown = d.properties['DISTRICT'];
            d.DISTRICTNAME = d.properties['DISTRICTNAME'];
            d.value = d.properties['District_Code'];
            d.ParameterType = 1;
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




            dataDoughnut = dataDoughnut + "{ DISTRICTNAME : '" + d.properties['DISTRICTNAME'] + "', y : " + d.properties['DISTRICTNAME'] + "},";



        });
        var legend = '<b><span style="font- size: 9px; color: #008000; font- weight: bold">सामान्य : ' + klSamanya + '</span>' + '&nbsp;&nbsp; <span style="font- size: 9px; color: #FFA500; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>' + '&nbsp;&nbsp; <span style="font- size: 9px; color: #FF0000; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span></b>'
        $("#legendbind").html(legend);

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

                }
            },
            title:
            {
                text: chartTitle,
                style: {
                    fontSize: '15px',
                    //fontWeight: 'bold',
                    textAlign: 'left',
                    color: 'black'
                },
                //AxisTitleAlignValue:low
                //AlignValue
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

            },
            legend: {
                enabled: false
            },
        });



    })();
}



function BindDataAccordingToFilter(Type, district_code, BlockCode, panchayatcode, VillageWard, savedansheelta, Procedure, Matter_Status, thanacode, step, con) {
    (async () => {
        var total = 0;
        datainfo = "";
        count = 0;
        countTotal = 0;

        var datefrom1, todate1, legendTitle;
        datefrom1 = document.getElementById("txtdatefrom").value;
        todate1 = document.getElementById("txtDateTo").value;


        const topology = await fetch(
            'FilterDataGraph.ashx?District=' + district_code + '&BlockCode=' + BlockCode + '&panchayatcode=' + panchayatcode + '&VillageWard=' + VillageWard + '&savedansheelta=' + savedansheelta + '&fromdate=' + datefrom1 + '&todate=' + todate1 + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType") + '&Type=' + Type + '&Procedure=' + Procedure + '&Matter_Status=' + Matter_Status + '&thanacode=' + thanacode + "&con=" + con
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
            'HandlerFilterGraph.ashx?Block=' + BlockCode + '&fromdate=' + datefrom1 + '&todate=' + todate1 + '&BhumiVivadType=' + localStorage.getItem("BhumiVivadType") + '&District_Code=' + district_code + '&panchayatcode=' + panchayatcode + '&savedansheelta=' + savedansheelta + '&Matter_Status=' + Matter_Status + '&VillageWard=' + VillageWard + '&thanacode=' + thanacode + "&con=" + con
        ).then(response => response.json());

        function getTemp_old(point, datapoint) {

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

        function getTemp(point, datapoint) {


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

            d.drilldown = d.properties['BlockCode'];
            d.DISTRICTNAME = d.properties['BlockName'];
            d.value = d.properties['BlockCode'];

            d.ParameterType = step;

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

        var legend = '<b><span style="font- size: 9px; color: #008000; font- weight: bold">सामान्य : ' + klSamanya + '</span>' + '&nbsp;&nbsp; <span style="font- size: 9px; color: #FFA500; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>' + '&nbsp;&nbsp; <span style="font- size: 9px; color: #FF0000; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span></b>'
        $("#legendbind").html(legend);
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
                text: createchartmessgae(),
                style: {
                    fontSize: '15px',
                    //fontWeight: 'bold',
                    textAlign: 'low',
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

                enabled: function () {

                    return false;
                },
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

                            // console.log(e);
                            // console.log(e.point.Village);
                            // alert(e.FinalNirast);
                            //window.location.replace("DisplayData.aspx?PS_Code=" + e.point.PS_Code, '_blank');

                            if (e.point.Village != null && e.point.Village != "") {
                                var Bhumi_savedansheelta = 0;
                                if (e.point.Samanya == 0) {
                                    Bhumi_savedansheelta = e.point.Samanya > 0 ? 1 :
                                        e.point.Sumvadansheel > 0 ? 2 :
                                            e.point.AtiSumvadansheel > 0 ? 3 : 0;

                                }

                                //window.open("DisplayData.aspx?Village=" + e.point.Village + "&AreaType=" + e.point.AreaType + "&FromDate=" + document.getElementById('txtdatefrom').value + "&ToDate=" + document.getElementById('txtDateTo').value, "_blank");
                                window.open("DisplayData.aspx?Village=" + e.point.Village + "&AreaType=" + e.point.AreaType + "&FromDate=" + document.getElementById('txtdatefrom').value + "&ToDate=" + document.getElementById('txtDateTo').value + "&Bhumi_savedansheelta=" + Bhumi_savedansheelta + "&Action=" + $("#ddlaction").val() + "&Id=" + sessionStorage.getItem("Id") + "&DataValue=" + sessionStorage.getItem("DataValue"), "_blank");
                            }

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

            },
            legend: {
                enabled: false
            },
        });


    })();
}

function getSenAction(con) {
    // getTotalApplication();
    bindviewmap();
    chartTitle = createchartmessgae();
    if ($("#ddPanchayat").val() != "0") {
        BindDataAccordingToFilter("Panchayat", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), 0, $("#ddSensivity").val(), "usp_getPanchayatMapFilterGraph", $("#ddlaction").val(), $("#ddlPoliceSation").val(), 4, con);
    }
    else if ($("#ddBlock").val() != "0") {

        BindDataAccordingToFilter("Panchayat", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), 0, $("#ddSensivity").val(), "usp_getPanchayatMapFilterGraph", $("#ddlaction").val(), $("#ddlPoliceSation").val(), 3, con);
    }
    else if ($("#ddDistrict").val() != "0") {
        BindDataAccordingToFilter("Block", $("#ddDistrict").val(), $("#ddBlock").val(), $("#ddPanchayat").val(), 0, $("#ddSensivity").val(), "usp_getBlockMapFilterGraph", $("#ddlaction").val(), $("#ddlPoliceSation").val(), 2, con);
    }
    else if ($("#ddDistrict").val() == "0") {
        var step = localStorage.getItem("step")
        var filter = localStorage.getItem("filter");
        //if (step == 1 || step == 2 || step == 3) {
        if (step == 1 || step == 2) {
            if ($("#ddSensivity").val() == 0 || $("#ddSensivity").val() == 1) {
                BindMapDataLoad("0", $("#ddSensivity").val(), con);
            }
            else {
                BindMapDataLoadNEW("0", $("#ddSensivity").val(), con);
            }
        }
        else {
            if (filter != 0) {
                BindMapDataLoadNEW("0", $("#ddSensivity").val(), con);
            }
            else {
                BindMapDataLoad("0", $("#ddSensivity").val(), con);
            }
        }

    }


}

function bindviewmap() {
    var msg = "";
    var msg2 = "";
    var rrid = $(".active").attr("data-rt");
    if ($("#ddDistrict").val() == "0") {
        msg = "Bihar";
        $("#s").addClass("greycolor");
        msg2 = "ज़िलावार";

    }
    if ($("#ddDistrict").val() != "0") {
        msg = $("#ddDistrict option:selected").text() + " District";
        $("#s").removeClass("greycolor");
        msg2 = "अंचलवार";

    }
    if ($("#ddBlock").val() != "0") {
        msg = msg + " / " + $("#ddBlock option:selected").text() + " Circle";
        msg2 = "पंचायतवार";
    }
    if ($("#ddPanchayat").val() != "0") {
        msg = msg + " / " + $("#ddPanchayat option:selected").text() + " Panchayat";
        msg2 = "पंचायतवार "
    }
    if (sessionStorage.getItem("Submenu") != "") {
        msg2 = msg2 + " " + rrid + "-" + sessionStorage.getItem("Submenu");
    }
    else {
        msg2 = msg2 + " " + rrid;
    }
    //$("#mapview").text(msg);
    $("#Label1").html(msg2);
    // alert(rrid);

}

//piecghart
//google.charts.load('current', { 'packages': ['bar'] });

function PiechartFun(Sstep, rpt) {
    //debugger;
    google.charts.load("current", { packages: ["corechart"] });
    google.charts.setOnLoadCallback(DataforPieChart(Sstep, rpt));
}

function DataforPieChart(Sstep, rpt) {
    var datefrom, todate, legendTitle;
    datefrom = document.getElementById("txtdatefrom").value;
    todate = document.getElementById("txtDateTo").value;
    $(function () {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            url: 'Bhu-Dashboard-graph.aspx/GetDetails',
            data: JSON.stringify({ DistrictId: $("#ddDistrict").val(), BlockCode: $("#ddBlock").val(), PanchayatCode: $("#ddPanchayat").val(), thana_code: $("#ddlPoliceSation").val(), savedansheelta: $("#ddSensivity").val(), Matter_Status: $("#ddlaction").val(), FromDate: datefrom, ToDate: todate, step: Sstep }),

            success: function (response) {
                var displya = "";
                var i = 0;
                $.each(response.d, function (key, value) {
                    var cl = "btn-primary";
                    if (i == 0) {
                        cl = "btn-danger";
                    }
                    displya = displya + '<a href="#" onclick=' + 'reportview(event,this,"' + Sstep + '","' + i + '")' + '><center style="border: 1px solid white; border-radius: 5px; box-shadow: rgba(6, 24, 44, 0.4) 0px 0px 0px 2px, rgba(6, 24, 44, 0.65) 0px 4px 6px -1px, rgba(255, 255, 255, 0.08) 0px 1px 0px inset;" class="' + cl + '" id="' + Sstep + i + 'menu">'
                    displya = displya + '<label ID="' + Sstep + i + 'lblfieldname">' + value.filedname + '</label><br/>';
                    displya = displya + '<label ID="' + Sstep + i + 'lblfieldValue">' + value.filedvalue + '</label><br/></center></a><br/>';
                    i = i + 1;
                });
                $('#divdynamic').html(displya);
                drawChart(response.d, Sstep, rpt);
            },
            error: function () {
                alert("Error loading data! Please try again pie");
            }
        });
    })
}

function drawChart(dataValues, s, rpt) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'filedname');
    data.addColumn('number', 'filedvalue');

    for (var i = 1; i < dataValues.length; i++) {

        data.addRow([dataValues[i].filedname, parseInt(dataValues[i].filedvalue)]);

    }
    var options = {
        responsive: true,
        title: rpt,
        chartArea: {
            left: 0,
            height: 120,
            width: 270
        },
        height: 210,
        width: 270,
        animation: {
            duration: 1000,
            easing: 'in',
            startup: true
        },
        titleTextStyle: {
            fontSize: 14
        },
        titlePosition: 'bottom',
        pieHole: 0.6,
        pieSliceText: 'value',
        legend: {
            position: 'bottom', alignment: 'center', orientation: 'vertical',
            maxLines: 1,
            labeledValueText: 'both',
            textStyle: {
                bold: true,
                italic: true,
                fontSize: 11
            },
            scrollArrows: 'none'
        },
        pieSliceTextStyle: {
            bold: true
        }

    };
    var chart = new google.visualization.PieChart(document.getElementById('chartContainer1'));
    chart.draw(data, options);
}

function getTotalApplication(proc) {
    var datefrom, todate, legendTitle;
    datefrom = document.getElementById("txtdatefrom").value;
    todate = document.getElementById("txtDateTo").value;
    $(function () {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            url: 'Bhu-Dashboard-graph.aspx/GetTotalDetails',
            data: JSON.stringify({ DistrictId: $("#ddDistrict").val(), BlockCode: $("#ddBlock").val(), PanchayatCode: $("#ddPanchayat").val(), thana_code: $("#ddlPoliceSation").val(), savedansheelta: $("#ddSensivity").val(), Matter_Status: $("#ddlaction").val(), FromDate: datefrom, ToDate: todate, step: proc }),
            success: function (data) {

                var displya = "";
                $.each(data.d, function (key, value) {




                    displya = displya + '<center style="cursor: pointer; border: 1px solid white; border-radius: 5px; box-shadow: rgba(6, 24, 44, 0.4) 0px 0px 0px 2px, rgba(6, 24, 44, 0.65) 0px 4px 6px -1px, rgba(255, 255, 255, 0.08) 0px 1px 0px inset;"  class="btn-default" id="totalapplication">'
                    displya = displya + '<label ID="lblfieldnamet">Total Application</label><br />';
                    displya = displya + '<label ID="lblfieldValuet">' + value.filedvalue + '</label><br /></center>';
                });
                $('#divtotal').html(displya);

                //$.each(data.d, function (key, value) {                  
                //    $("#lblTotalApplication").text(value.filedvalue);
                //});
            },

            error: function () {
                //  alert("Error loading data! Please try again. test");
            }
        });
    })
}

//bargraph
function BarchartFun(Sstep, rpt, proc, condi) {
    //debugger;
    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(DataforBarGraph(Sstep, rpt, proc, condi));
}

function DataforBarGraph(Sstep, rpt, proc, condi) {
    var datefrom, todate, legendTitle;
    datefrom = document.getElementById("txtdatefrom").value;
    todate = document.getElementById("txtDateTo").value;
    $(function () {
        $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            url: 'Bhu-Dashboard-graph.aspx/GetForBarDetails',
            data: JSON.stringify({ DistrictId: $("#ddDistrict").val(), BlockCode: $("#ddBlock").val(), PanchayatCode: $("#ddPanchayat").val(), thana_code: $("#ddlPoliceSation").val(), savedansheelta: $("#ddSensivity").val(), Matter_Status: $("#ddlaction").val(), FromDate: datefrom, ToDate: todate, step: Sstep, proc: proc, condi: condi }),
            success: function (response) {
                drawBarChart(response.d, Sstep, rpt);
            },
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    })
}

function drawBarChart(dataValues, s, rpt) {
    var dataArray = [['District', 'Total']];

    for (var n = 0; n < dataValues.length; n++) {
        dataArray.push([dataValues[n].filedname, parseInt(dataValues[n].filedvalue)]);
    }

    var data1 = new google.visualization.arrayToDataTable(dataArray);

    var view = new google.visualization.DataView(data1);
    view.setColumns([0, 1, {
        calc: 'stringify',
        role: 'annotation',
        sourceColumn: 1,
        type: 'string'
    }]);

    var options = {
        is3D: true,
        title: 'Total Entry',
        vAxis: {
            textStyle: {
                fontSize: 10 // or the number you want
            }

        },
        chartArea: { top: 10 },
        width: 400,
        height: 800,


        legend: { position: 'none' },
        //  bars: 'horizontal',
        axes: {
            x: {
                0: { side: 'top', label: rpt }
            },
            y: {
                0: { label: '', textPosition: 'none' }
            },
            textStyle: {
                bold: true,
                fontSize: 12
            }
        },

        bar: { groupWidth: "75%" },
    };

    var chart = new google.visualization.BarChart(document.getElementById('top_x_div'));
    chart.draw(view, options);
}

function getBarGraph(proc, condi) {
    if (proc == "0") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterTotalBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterTotalBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterTotalMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsTotalBarGraph", condi);
        }
    }
    else if (proc == "1") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterCompleteBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterCompleteBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterCompleteMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsCompleteBarGraph", condi);
        }
    }
    else if (proc == "2") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterInCompleteBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterInCompleteBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterInCompleteMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsInCompleteBarGraph", condi);
        }
    }
    else if (proc == "3") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsBarGraph", condi);
        }
    }
    else if (proc == "4") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterTotalBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterTotalBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterTotalMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsTotalBarGraph", condi);
        }
    }
    else if (proc == "5") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterLandTypesInKhatianBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterLandTypesInKhatianBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterLandTypesInKhatianMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsLandTypesInKhatianBarGraph", condi);
        }
    }
    else if (proc == "6") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterVadiEvidanceBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterVadiEvidanceBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterVadiEvidanceMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsVadiEvidanceBarGraph", condi);
        }
    }
    else if (proc == "7") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterPrativadiEvidanceBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterPrativadiEvidanceBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterPrativadiEvidanceMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsPrativadiEvidanceBarGraph", condi);
        }
    }
    else if (proc == "8") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterLandDisputeBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterLandDisputeBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterLandDisputeMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsLandDisputeBarGraph", condi);
        }
    }
    else if (proc == "9") {
        if ($("#ddPanchayat").val() != "0") {
            BarchartFun(1, "Panchayat Wise", "usp_getPanchayatMapFilterCourtDisputeBarGraph", condi);
        }
        else if ($("#ddBlock").val() != "0") {
            BarchartFun(2, "Panchayat Wise", "usp_getPanchayatMapFilterCourtDisputeBarGraph", condi);
        }
        else if ($("#ddDistrict").val() != "0") {
            BarchartFun(3, "Block Wise", "usp_getBlockMapFilterCourtDisputeMapBarGraph", condi);
        }
        else if ($("#ddDistrict").val() == "0") {
            BarchartFun(4, "District Wise", "usp_GetDistirctWiseDetailsCourtDisputeBarGraph", condi);
        }
    }
}


function reportview(e, evt, id, datavalue) {
    e.preventDefault();
    $("center").removeClass("btn-danger");
    $("center").addClass("btn-primary");
    $("#" + id + datavalue + "menu").removeClass("btn-primary");
    $("#" + id + datavalue + "menu").addClass("btn-danger");
    $("#totalapplication").removeClass("btn-primary");
    $("#totalapplication").addClass("btn-default");
    $("#logo").removeClass("btn-primary");
    var p = "";
    if (datavalue != 0) {
        p = "<span style='color:red'>" + $("#" + id + datavalue + "lblfieldname").text() + "</span>";
    }

    if (id == 1 || id == 2) {
        $("#chartContainer2").removeClass("hidden");
    }
    else {
        $("#chartContainer2").addClass("hidden");
    }
    localStorage.setItem("step", id);
    localStorage.setItem("filter", datavalue);
    sessionStorage.setItem("Submenu", p);
    sessionStorage.setItem("Id", id);
    sessionStorage.setItem("DataValue", "");

    if (id == 1) {

        var con = " ";
        if (datavalue == 1) {
            con = " and isnull( m.Final,1)=1 ";
        }
        if (datavalue == 2) {
            con = " and isnull( m.Final,1)=0 ";
        }
        sessionStorage.setItem("DataValue", con);
        getSenAction(con);
        // getBarGraph("2", "");
        //DataforLineChart();
        getBarGraph(datavalue, con);
    }
    if (id == 2) {

        $("#ddSensivity").val(datavalue);
        getSenAction(" and isnull( m.Final,1)=1");
        getBarGraph("3", "");
        DataforLineChart();
    }
    if (id == 3) {
        $("#ddlaction").val(datavalue);
        getSenAction(" and isnull( m.Final,1)=1");
        getBarGraph("3", "");

    }
    if (id == 4) {
        var con = " and m.BhumiVivadType=" + datavalue;
        if (datavalue == 0) {
            con = "";
        }
        else if (datavalue == 14) {
            con = " and m.BhumiVivadType=15";
        }
        else if (datavalue == 15) {
            con = " and m.BhumiVivadType=20";
        }
        getSenAction(con);
        getBarGraph("4", con);
        sessionStorage.setItem("DataValue", con);

    }
    if (id == 5) {
        var con = " and lan.LandTypesInKhatian=" + datavalue;
        var con2 = " and m.a_id in (select a_id from LandDetailsEntry where LandTypesInKhatian=" + datavalue + ")";
        if (datavalue == 0) {
            con = "";
            con2 = " and m.a_id in (select a_id from LandDetailsEntry)";
        }
        else if (datavalue == 4) {
            con = " and lan.LandTypesInKhatian=5";
            con2 = " and m.a_id in (select a_id from LandDetailsEntry where LandTypesInKhatian=12)";
        }
        else if (datavalue == 5) {
            con = " and lan.LandTypesInKhatian=6";
            con2 = " and m.a_id in (select a_id from LandDetailsEntry where LandTypesInKhatian=12)";
        }
        else if (datavalue == 6) {
            con = " and lan.LandTypesInKhatian=11";
            con2 = " and m.a_id in (select a_id from LandDetailsEntry where LandTypesInKhatian=12)";
        }
        else if (datavalue == 7) {
            con = " and lan.LandTypesInKhatian=12";
            con2 = " and m.a_id in (select a_id from LandDetailsEntry where LandTypesInKhatian=12)";
        }
        sessionStorage.setItem("DataValue", con2);
        getSenAction(con2);
        getBarGraph("5", con);

    }
    if (id == 6) {
        var con = " and v.evidence_id=" + datavalue;
        var con2 = " and m.a_id in (select a_id from Vadi_Evidence_Entry where evidence_id=" + datavalue + ")";
        if (datavalue == 0) {
            con = "";
            con2 = " and m.a_id in (select a_id from Vadi_Evidence_Entry)";
        }
        sessionStorage.setItem("DataValue", con2);
        getSenAction(con2);
        getBarGraph("6", con);

    }
    if (id == 7) {
        var con = " and v.evidence_id=" + datavalue;
        var con2 = " and m.a_id in (select a_id from PratiVadi_Evidence_Entry where evidence_id=" + datavalue + ")";
        if (datavalue == 0) {
            con = "";
            con2 = " and m.a_id in (select a_id from PratiVadi_Evidence_Entry)";
        }
        sessionStorage.setItem("DataValue", con2);
        getSenAction(con2);
        getBarGraph("7", con);

    }
    if (id == 8) {
        var con = "";
        var con2 = " and m.a_id in (select a_id from LandDisputeDetailsEntry )";
        if (datavalue == 1) {
            con = " and ld.is_FIR_registered=''Y''";
            con2 = " and m.a_id in (select a_id from LandDisputeDetailsEntry where is_FIR_registered=''Y'')";
            sessionStorage.setItem("DataValue", "and m.a_id in (select a_id from LandDisputeDetailsEntry where is_FIR_registered='Y')");
        }
        if (datavalue == 2) {
            con = " and ld.is_complaint_filed=''Y''";
            con2 = " and m.a_id in (select a_id from LandDisputeDetailsEntry where is_complaint_filed=''Y'')";
            sessionStorage.setItem("DataValue", "and m.a_id in (select a_id from LandDisputeDetailsEntry where is_complaint_filed='Y')");
        }
        if (datavalue == 3) {
            con = " and ld.is_Sanha_recorded=''Y''";
            con2 = " and m.a_id in (select a_id from LandDisputeDetailsEntry where is_Sanha_recorded=''Y'')";
            sessionStorage.setItem("DataValue", "and m.a_id in (select a_id from LandDisputeDetailsEntry where is_Sanha_recorded='Y')");
        }

        getSenAction(con2);
        getBarGraph("8", con);

    }
    if (id == 9) {
        var con = "";
        var con2 = " and m.a_id in (select a_id from CourtDisputeDetailsEntry )";
        if (datavalue == 1) {
            con = " and cd.courtID=1 and cd.courtTypeID<>0";
            con2 = " and m.a_id in (select a_id from CourtDisputeDetailsEntry where courtID=1 and courtTypeID<>0)";
        }
        if (datavalue == 2) {
            con = " and cd.courtID=2 and cd.courtTypeID<>0";
            con2 = " and m.a_id in (select a_id from CourtDisputeDetailsEntry where courtID=2 and courtTypeID<>0)";
        }
        if (datavalue == 3) {
            con = " and  cd.courtID=4";
            con2 = " and m.a_id in (select a_id from CourtDisputeDetailsEntry where courtID=4)";
        }
        if (datavalue == 4) {
            con = " and cd.courtID=5";
            con2 = " and m.a_id in (select a_id from CourtDisputeDetailsEntry where courtID=5)";
        }
        if (datavalue == 5) {
            con = " and cd.courtID=6";
            con2 = " and m.a_id in (select a_id from CourtDisputeDetailsEntry where courtID=6)";
        }
        sessionStorage.setItem("DataValue", con2);
        getSenAction(con2);
        getBarGraph("9", con);

    }

    bindviewmap();
    chartTitle = createchartmessgae();
}

function createchartmessgae_old() {
    var p = "राज्य : बिहार <br/> ज़िला : ";
    if ($("#ddDistrict").val() != "0") {
        p = p + $("#ddDistrict option:selected").text() + "<br/>अंचल : ";
    }
    else {
        p = p + " सभी जिला<br/>अंचल : ";
    }
    if ($("#ddBlock").val() != "0") {
        p = p + $("#ddBlock option:selected").text() + "<br/>पंचायत :";
    }
    else {
        p = p + " सभी अंचल<br/>पंचायत : ";
    }
    if ($("#ddPanchayat").val() != "0") {
        p = p + $("#ddPanchayat option:selected").text() + "<br/> ";
    }
    else {
        p = p + " सभी पंचायत<br/>  ";
    }
    if ($("#ddSensivity").val() != "0") {
        p = p + " सवेदनशीलता : " + $("#ddSensivity option:selected").text() + "<br/>बैठक का निष्कर्ष ";
    }
    else {
        p = p + " सवेदनशीलता : सभी सवेदनशीलता <br/>बैठक का निष्कर्ष : ";
    }
    if ($("#ddlaction").val() != "0") {
        p = p + $("#ddlaction option:selected").text() + "<br/>अवस्थिति : ";
    }
    else {
        p = p + " सभी बैठक का निष्कर्ष <br/>अवस्थिति : ";
    }
    p = p + $(".active").attr("data-rt");
    if (sessionStorage.getItem("Submenu") != "") {
        console.log(sessionStorage.getItem("Submenu"));
        p = p + " " + sessionStorage.getItem("Submenu");
    }
    return p;
}


function createchartmessgae() {

    var th = "<table style='border-collapse: collapse; width: 100%;'><tr style='font-weight:bold'><td>ज़िला</td><td>अंचल</td><td>पंचायत</td><td>सवेदनशीलता</td><td>बैठक का निष्कर्ष </td><td>अवस्थिति</td></tr><tr style='color:#337ab7'>"

    var p = " ज़िला : ";
    if ($("#ddDistrict").val() != "0") {
        th = th + "<td>" + $("#ddDistrict option:selected").text() + "</td>";
    }
    else {
        th = th + "<td> सभी जिला</td> ";
    }
    if ($("#ddBlock").val() != "0") {
        th = th + "<td>" + $("#ddBlock option:selected").text() + "</td>";
    }
    else {
        th = th + " <td>सभी अंचल </td> ";
    }
    if ($("#ddPanchayat").val() != "0") {
        th = th + "<td>" + $("#ddPanchayat option:selected").text() + "</td>" + " ";
    }
    else {
        th = th + "<td>" + "सभी पंचायत </td>";
    }
    if ($("#ddSensivity").val() != "0") {
        th = th + "<td>" + $("#ddSensivity option:selected").text() + "</td>";
    }
    else {
        th = th + " <td>सभी सवेदनशीलता</td>";
    }
    if ($("#ddlaction").val() != "0") {
        th = th + "<td>" + $("#ddlaction option:selected").text() + "</td>";
    }
    else {
        th = th + "<td> सभी बैठक का निष्कर्ष </td>";
    }
    th = th + "<td>" + $(".active").attr("data-rt");
    if (sessionStorage.getItem("Submenu") != "") {
        console.log(sessionStorage.getItem("Submenu"));
        th = th + " " + sessionStorage.getItem("Submenu") + "";
    }
    th = th + "</td></tr></table>";
    // alert(th);
    $("#titlebind").html(th);
    return "";
}