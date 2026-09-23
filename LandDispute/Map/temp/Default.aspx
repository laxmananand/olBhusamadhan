<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<!DOCTYPE html>
<script runat="server">

    
    protected void ddlFacility_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <style>
        body {
            min-height: 100vh;
            min-height: -webkit-fill-available;
        }

        html {
            height: -webkit-fill-available;
        }

        .DataLabelCss tspan {
            fill: blue;
            stroke-width: 0px !important;
            font-size: 10px !important;
            font-style: normal !important;
            text-transform: capitalize !important;
            text-shadow:none !important;
            

        }
 
        
        #containermap {
            height: 85vh;
            width: 100%;
            margin: 0px;
            padding: 0px;
        }


        #containerDoungnout {
            height: 30vh;
            width: 100%;
            margin: 0px;
            padding: 0px;
        }
    </style>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="css/sidebars.css" rel="stylesheet" />
    <link href="css/datatables.min.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-2" style="background-color:#C2DED1">
                    <center><b><p style="color:#354259; font-size:25px; padding:10px;">Flood Data Map</p></b></center>

         
                    <div class="col-12 text-center" style="background-color:darkslategray">
                        <a  style="text-decoration: none; color:white; display:block; border-radius: 8px;" class="collapse-item btn" href="#" onclick="window.history.go(-1); return false;">HOME</a>
                    </div>
                    <br />
                    <div class="col-12 mb-3">
                        <p style="font-size:25px;">Filter</p>
                                <asp:DropDownList ID="ddlFacility" runat="server" AutoPostBack="true" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="Flood Affected Area Map"></asp:ListItem>
                                    <%--<asp:ListItem Value="1" Text="Relief Center"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Community Kitchen"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Total Deaths"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Total Migated Population"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Total Flood Entry"></asp:ListItem>--%>
                                </asp:DropDownList>
                    </div>
                    <div class="col-12">
                        <fieldset>
                            <legend>Features:</legend>

                            <div>
                                <input type="checkbox" id="chkReliefCenter" name="chk" onchange="BindMapData();"/>
                                <label for="chkReliefCenter">Relief Center</label>
                            </div>

                            <div>
                                <input type="checkbox" id="chkCommunityKitchen" name="chk" onchange="BindMapData();"/>
                                <label for="chkCommunityKitchen">Community Kitchen</label>
                            </div>
                            <div>
                                <input type="checkbox" id="chkMigatedPeople" name="chk" onchange="BindMapData();"/>
                                <label for="chkMigatedPeople">Population Evacuated</label>
                            </div>
                             <div>
                                <input type="checkbox" id="chkFloodDeaths" name="chk" onchange="BindMapData();"/>
                                <label for="chkFloodDeaths">Deaths</label>
                            </div>
                             <div>
                                <input type="checkbox" id="chkHealthCenter" name="chk" onchange="BindMapData();"/>
                                <label for="chkHealthCenter">Health Center</label>
                            </div>
                             <div>
                                <input type="checkbox" id="chkHouseDamage" name="chk" onchange="BindMapData();"/>
                                <label for="chkHouseDamage">Damaged HousesValue</label>
                            </div>
                            <div>
                                <input type="checkbox" id="chkGovBoats" name="chk" onchange="BindMapData();"/>
                                <label for="chkGovBoats">Gov. Boats</label>
                            </div>
                               <div>
                                <input type="checkbox" id="chkMotorBoats" name="chk" onchange="BindMapData();"/>
                                <label for="chkMotorBoats">Motor Boats</label>
                            </div>
                            <div>
                                <input type="checkbox" id="chkPvtBoats" name="chk" onchange="BindMapData();"/>
                                <label for="chkPvtBoats">Pvt. Boats</label>
                            </div>
                              <div>
                                <input type="checkbox" id="chkPolythene" name="chk" onchange="BindMapData();"/>
                                <label for="chkPolythene">Polythene Sheet Dist.</label>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div class="col-10">
                    <div class="row" style="padding-top:15px;">
                        <div class="col-1" style="text-align:right;">
                            From
                       </div>
                        <div class="col-2">
                            
                            <asp:TextBox TextMode="Date" ID="txtdatefrom" runat="server" CssClass="form-control" Text="2022-01-01"></asp:TextBox>
                             
                        </div>
                        <div class="col-1" style="text-align:right;">
                             To
                       </div>
                        <div class="col-2">
                          
                           <asp:TextBox TextMode="Date" ID="txtDateTo" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="col-2">
                           
                            <asp:Button ID="btnSubmit" Text="Search" runat="server" CssClass="btn btn-primary" OnClientClick="myFunction();" />
                        </div>
                         <div class="col-5">
                             </div>
                    </div>
                    <hr style="padding:0px;" />
                    <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">
                            
                                <div class="col-12">
                                    <div id="containermap">
                                    </div>
                                </div>
                                <div class="text-center">
                                <asp:Label ID="lblDist" runat="server" Text="Total Affectd District:"></asp:Label>
                               <%--<asp:TextBox  ID="lblTotalAffected" runat="server" Text="0" CssClass="form-control"></asp:TextBox>--%>
                            <asp:Label ID="lblTotalAffected" runat="server" Text="0"></asp:Label>
                            /<asp:Label ID="lblTotalDistrict" runat="server" Text="38"></asp:Label>
                                </div>
                            <div class="text-left">
                                <input type="checkbox" id="chkAffected" name="chkAffected" onchange="BindMapData();"/>
                                 <asp:Label ID="lblAffDist" runat="server" Text="Affectd District:"></asp:Label>
                               <%-- <label for="chkAffected">Only Affected</label>--%>
                            </div>
                            
                            <br />
                                <div class="col-12">
                                    <div style="overflow:scroll; width:100%;">
                                    <div class="container-fluid">
                                        <table id="FloodDataTable" class="table table-responsive table-hover"></table>
                                    </div>
                                        </div>
                                </div>
                            </div>
                       
                    </div>
                        </div>
                </div>
            </div>
        </div>

    </form>
    <script src="js/jquery-3.5.1.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/sidebars.js"></script>

    <script src="https://code.highcharts.com/maps/highmaps.js"></script>
    <script src="https://code.highcharts.com/maps/modules/data.js"></script>
    <script src="https://code.highcharts.com/maps/modules/drilldown.js"></script>

    <script src="https://code.highcharts.com/maps/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/maps/modules/offline-exporting.js"></script>
    <script src="https://code.highcharts.com/maps/modules/accessibility.js"></script>

    <script src="js/datatables.min.js"></script>
    <script>
        var datainfo = "";
        var point;
        var x = 1;
        var count = 0,countTotal=0;
        var datefrom, todate,legendTitle ;
        datefrom = document.getElementById("txtdatefrom").value;
        todate = document.getElementById("txtDateTo").value;
        var lblAffectedDist = document.getElementById("<%=lblTotalAffected.ClientID %>");
        var lbltotalDist = document.getElementById("<%=lblTotalDistrict.ClientID %>");
        var lblDist = document.getElementById("<%=lblDist.ClientID %>");
        var lblAffDist = document.getElementById("<%=lblAffDist.ClientID %>");
        
        var dataOnDate = 'Data On date: (' + document.getElementById("txtdatefrom").value + ') to (' + document.getElementById("txtDateTo").value + ')';
        function myFunction() {

            //document.getElementById("demo").innerHTML = "You selected: " + x;
            // alert(x);
        }
        var someFlag = true;
        var chartTitle = 'EOC - Disaster Management Department Government of Bihar ( Flood Affected District  Report ) ';
        var table;
        var dataDistrict, txtdatefrom, txtdatefrom, Message;
        Message = document.getElementById("Message");
        const drilldown = async function (e) {

            if (!e.seriesOptions) {
                point = e.point;
                const chart = this;
                const mapKey = '';
                const BSeriesdata0 = [];
                const BSeriesdata0_20 = [];
                const BSeriesdata21_50 = [];
                const BSeriesdata51_80 = [];
                const BSeriesdata81_100 = [];
                const BSeriesdataAll = [];
                // Handle error, the timeout is cleared on success
                let fail = setTimeout(() => {
                    if (!Highcharts.maps[mapKey]) {
                        chart.showLoading("<i class=\"icon-frown\"></i> Failed loading ${e.point.name}");
                        fail = setTimeout(() => {
                            chart.hideLoading();
                        }, 1000);
                    }
                }, 3000);

                // Show the Font Awesome spinner
                chart.showLoading('<i class="icon-spinner icon-spin icon-3x"></i>');
               
             
                // Load the drilldown map

                const topology = await fetch(
                    'BlockData.ashx?District=' + e.point.value + '&fromdate=' + datefrom + '&todate=' + todate
                ).then(response => response.json());
                const dataDrilldown = Highcharts.geojson(topology);

                table.destroy();
                $("#FloodDataTable").html("");

                $('#FloodDataTable').append('<thead><tr class="bg-black text-white">' +
                    '<th> Sl. No </th>' +
                    '<th> BLOCK </th>' +
                    '<th> Total Panchayat </th>' +
                    '<th> Total Affected Panchayat </th>' +
                    '<th> Total Kitchens </th>' +
                    '<th> Total Relief Centres </th>' +
                    '<th> Total Deaths </th>' +
                    '<th> Total Migrated People </th>' +
                    '<th> Total Flood Entry </th>' +                    
                    '<th> Total Damaged Houses </th>' +
                    '<th> Total Gov. Boats </th>' +
                    '<th> Total Motor Boats </th>' +                   
                    '<th> total Pvt. Boats </th>' +
                    '<th> Total PolytheneSheet Dist. </th>' +
                    '<th>  %age  <br>(Aff.Panchayat*100) /Tot.Panchayat </th>' +
                    '</tr></thead>');
                $('#FloodDataTable').append('<tbody>');
                count = 0;
                
                countTotal = 0;
                // Set a non-random bogus value
                dataDrilldown.forEach((d, i) => {
                    d.name = d.properties['BLK_NAME'];
                    d.value = d.properties['BLK_C_2011'];
                    d.Per = d.properties['Per'];
                    d.color = d.properties['color'];
                    d.TotalKitchens = d.properties['TotalKitchens'];
                    d.TotalReliefCentres = d.properties['TotalReliefCentres'];
                    d.TotalKitchens = d.properties['TotalDeaths'];
                    d.TotalReliefCentres = d.properties['MigratedPopulation'];
                    d.totalFloodEntry = d.properties['totalFloodEntry'];
                    d.EntryPer = d.properties['EntryPer'];
                    d.Entrycolor = d.properties['Entrycolor'];
                    d.totalDamagedHousesValue = d.properties['totalDamagedHousesValue'];
                    d.totalGovtBoatToday = d.properties['totalGovtBoatToday'];
                    d.totalMotorBoatToday = d.properties['totalMotorBoatToday'];
                    d.totalPolytheneSheetDist = d.properties['totalPolytheneSheetDist'];
                    d.totalPvtBoatToday = d.properties['totalPvtBoatToday'];

                    if (parseInt(d.properties['Per']) != 0)
                    {
                        count++;
                       

                    }
                    countTotal++;
                    if (parseInt(d.properties['Per']) == 0) {
                        BSeriesdata0.push(d);
                    }
                    if (parseInt(d.properties['Per']) > 0 && parseInt(d.properties['Per']) <= 20) {
                        BSeriesdata0_20.push(d);
                    }
                    if (parseInt(d.properties['Per']) > 20 && parseInt(d.properties['Per']) <= 50) {
                        BSeriesdata21_50.push(d);
                    }
                    if (parseInt(d.properties['Per']) > 50 && parseInt(d.properties['Per']) <= 80) {
                        BSeriesdata51_80.push(d);
                    }
                    if (parseInt(d.properties['Per']) > 80) {
                        BSeriesdata81_100.push(d);
                    }
                    if (chkAffected.checked == true) {
                        if (parseInt(d.properties['Per']) != 0) {
                            $('#FloodDataTable').append('<tr style="background-color:' + d.properties['color'] + '">' +
                                '<td> ' + (parseInt(i) + 1) + ' </td>' +
                                '<td> ' + d.properties['BLK_NAME'] + ' </td>' +
                                '<td> ' + d.properties['TotalPanchayat'] + ' </td>' +
                                '<td> ' + d.properties['TotalEffectedPanchayat'] + ' </td>' +
                                '<td> ' + d.properties['TotalKitchens'] + ' </td>' +
                                '<td> ' + d.properties['TotalReliefCentres'] + ' </td>' +
                                '<td> ' + d.properties['TotalDeaths'] + ' </td>' +
                                '<td> ' + d.properties['MigratedPopulation'] + ' </td>' +
                                '<td> ' + d.properties['totalFloodEntry'] + ' </td>' +                               
                                '<td> ' + d.properties['totalDamagedHousesValue'] + ' </td>' +
                                '<td> ' + d.properties['totalGovtBoatToday'] + ' </td>' +
                                '<td> ' + d.properties['totalMotorBoatToday'] + ' </td>' +                               
                                '<td> ' + d.properties['totalPvtBoatToday'] + ' </td>' +
                                '<td> ' + d.properties['totalPolytheneSheetDist'] + ' </td>' +
                                '<td> ' + d.properties['Per'] + ' </td>' +
                                '</tr > ');

                        }
                    }
                    else
                    {
                        $('#FloodDataTable').append('<tr style="background-color:' + d.properties['color'] + '">' +
                            '<td> ' + (parseInt(i) + 1) + ' </td>' +
                            '<td> ' + d.properties['BLK_NAME'] + ' </td>' +
                            '<td> ' + d.properties['TotalPanchayat'] + ' </td>' +
                            '<td> ' + d.properties['TotalEffectedPanchayat'] + ' </td>' +
                            '<td> ' + d.properties['TotalKitchens'] + ' </td>' +
                            '<td> ' + d.properties['TotalReliefCentres'] + ' </td>' +
                            '<td> ' + d.properties['TotalDeaths'] + ' </td>' +
                            '<td> ' + d.properties['MigratedPopulation'] + ' </td>' +
                            '<td> ' + d.properties['totalFloodEntry'] + ' </td>' +                           
                            '<td> ' + d.properties['totalDamagedHousesValue'] + ' </td>' +
                            '<td> ' + d.properties['totalGovtBoatToday'] + ' </td>' +
                            '<td> ' + d.properties['totalMotorBoatToday'] + ' </td>' +                           
                            '<td> ' + d.properties['totalPvtBoatToday'] + ' </td>' +
                            '<td> ' + d.properties['totalPolytheneSheetDist'] + ' </td>' +
                            '<td> ' + d.properties['Per'] + ' </td>' +
                            '</tr > ');

                    }                   

                });
                $('#FloodDataTable').append('</tbody>');
                table = $("#FloodDataTable").DataTable({
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": true,
                    "sDom": 'lfrtip'
                });

                // Apply the recommended map view if any
                //chart.mapView.update(
                //    Highcharts.merge(
                //        { insets: undefined },
                //        topology.objects.default['hc-recommended-mapview']
                //    ),
                //    false
                //);

                // Hide loading and add series
                chart.hideLoading();
                clearTimeout(fail);
                BSeriesdataAll.push(BSeriesdata0);
                BSeriesdataAll.push(BSeriesdata0_20)
                BSeriesdataAll.push(BSeriesdata21_50)
                BSeriesdataAll.push(BSeriesdata51_80)
                BSeriesdataAll.push(BSeriesdata81_100)


                legendTitle = "BLock";
                chart.addSingleSeriesAsDrilldown(e.point,
                    {
                        name: '0 % (Not Affected)',
                        data: BSeriesdata0,
                        color: '#3EC70B',
                        dataLabels:
                        {
                            enabled: true,
                            shadow: true,
                            className: 'DataLabelCss',
                            style: {
                                color: 'Black',
                                textOutline: false
                            }


                        },
                        borderColor: 'black',
                        borderWidth: 1
                    });

                chart.addSingleSeriesAsDrilldown(e.point,
                    {
                        name: '1-20 % Affected',
                        data: BSeriesdata0_20,
                        color: '#FCD900',
                        dataLabels:
                        {
                            enabled: true,
                            shadow: true,
                            className: 'DataLabelCss',
                            style: {
                                color: 'Black',
                                textOutline: false
                            }


                        },
                        borderColor: 'black',
                        borderWidth: 1
                    });

                chart.addSingleSeriesAsDrilldown(e.point,
                    {
                        name: '21-50 % Affected',
                        data: BSeriesdata21_50,
                        color: '#F77E21',
                        dataLabels:
                        {
                            enabled: true,
                            shadow: true,
                            className: 'DataLabelCss',
                            style: {
                                color: 'Black',
                                textOutline: false
                            }


                        },
                        borderColor: 'black',
                        borderWidth: 1
                    });

                chart.addSingleSeriesAsDrilldown(e.point,
                    {
                        name: '51-80 % Affected',
                        data: BSeriesdata51_80,
                        color: '#FA4753',
                        dataLabels:
                        {
                            enabled: true,
                            shadow: true,
                            className: 'DataLabelCss',
                            style: {
                                color: 'Black',
                                textOutline: false
                            }


                        },
                        borderColor: 'black',
                        borderWidth: 1
                    });

                chart.addSingleSeriesAsDrilldown(e.point,
                    {
                        name: '> 80% Affected',
                        data: BSeriesdata81_100,
                        color: 'red',
                        dataLabels:
                        {
                            enabled: true,
                            shadow: true,
                            className: 'DataLabelCss',
                            style: {
                                color: 'Black',
                                textOutline: false
                            }


                        },
                        borderColor: 'black',
                        borderWidth: 1
                    });
                
                if (x == 1) {
                    $(this.container).find('.highcharts-title').text('EOC - Disaster Management Department Government of Bihar ( Flood Affected Block  Report )');
                    legendTitle = "Flood Affected district";
                    x = 0;
                }
              
               
                chart.legend.title.attr({ text: '% age Flood Affected Panchayat<br/> <span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>' });



                lblAffectedDist.innerHTML = count;
                lbltotalDist.innerHTML = countTotal
                lblAffDist.innerHTML = "Only Affected BLocks";
                lblDist.innerHTML = "Affected BLocks";

                chart.applyDrilldown();


            }
        };
        var UNDF;
        // On drill up, reset to the top-level map view
        const drillup = function (e) {
            if (e.seriesOptions.custom && e.seriesOptions.custom.mapView) {
                e.target.mapView.update(e.seriesOptions.custom.mapView, false);
               
            }
            this.legend.title.attr({ text: ' % age Flood Affected Block<br/> <span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>' });
            //alert('sfsdfs');
               // legendTitle = "Flood Affected District";
            
           
            if (x == 0) {
                $(this.container).find('.highcharts-title').text('EOC - Disaster Management Department Government of Bihar ( Flood Affected District  Report ) ');
             
                // $(this.container).find(' .highcharts - subtitle').text(chartTitle + 'District');

                x = 1;
            }

            //console.log(d.properties['color']);
           
            table.destroy();
           
            $('#FloodDataTable').empty();
            $('#FloodDataTable').append('<thead><tr class="bg-black text-white">' +
                '<th> Sl. No </th>' +
                '<th> DISTRICT </th>' +
                '<th> Total Block </th>' +
                '<th> Total Affected Block </th>' +
                '<th> Total Affected Panchayat </th>' +
                '<th> Total Deaths </th>' +
                '<th> Total Evacuated People </th>' +
                '<th> Total Flood Entry </th>' +                
                '<th> Total Damaged Houses </th>' +
                '<th> Total Gov. Boats </th>' +
                '<th> Total Motor Boats </th>' +                
                '<th> total Pvt. Boats </th>' +
                '<th> Total PolytheneSheet Dist. </th>' +
                '<th>  %age  <br>(Aff.Panchayat*100) /Tot.Panchayat </th>' +
               
                '</tr></thead>');
            $('#FloodDataTable').append('<tbody>');
            dataDistrict.forEach((d, i) => {
                
                $('#FloodDataTable').append('<tr style="background-color:' + d.properties['color'] + '">' +
                    '<td> ' + (parseInt(i) + 1) + ' </td>' +
                    '<td> ' + d.properties['DISTRICT'] + ' </td>' +
                    '<td> ' + d.properties['TotalBlock'] + ' </td>' +
                    '<td> ' + d.properties['TotalEffectedBlock'] + ' </td>' +
                    '<td> ' + d.properties['TotalEffectedPanchayat'] + ' </td>' +
                    '<td> ' + d.properties['TotalDeaths'] + ' </td>' +
                    '<td> ' + d.properties['MigratedPopulation'] + ' </td>' +
                    '<td> ' + d.properties['totalFloodEntry'] + ' </td>' +
                    '<td> ' + d.properties['totalFloodEntry'] + ' </td>' +
                    '<td> ' + d.properties['totalFloodEntry'] + ' </td>' +
                    '<td> ' + d.properties['totalFloodEntry'] + ' </td>' +
                    '<td> ' + d.properties['Per'] + ' </td>' +
                    '<td> ' + d.properties['totalDamagedHousesValue'] + ' </td>' +
                    '<td> ' + d.properties['totalGovtBoatToday'] + ' </td>' +
                    '<td> ' + d.properties['totalMotorBoatToday'] + ' </td>' +
                    '<td> ' + d.properties['totalPolytheneSheetDist'] + ' </td>' +
                    '<td> ' + d.properties['totalPvtBoatToday'] + ' </td>' +
                    '</tr > ');

            });
            $('#FloodDataTable').append('</tbody>');
            table = $("#FloodDataTable").DataTable({
                "paging": true,
                "lengthChange": true,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true,
                "sDom": 'lfrtip'
            });



        };
        function BindMapData() {
            (async () => {

                var total = 0;

                var chkReliefCenter = document.getElementById("chkReliefCenter");
                var chkCommunityKitchen = document.getElementById("chkCommunityKitchen");
                var chkMigatedPeople = document.getElementById("chkMigatedPeople");
                var chkFloodDeaths = document.getElementById("chkFloodDeaths");
                var chkHealthCenter = document.getElementById("chkHealthCenter");
                var chkAffected = document.getElementById("chkAffected");

                var cboxes = document.getElementsByName('chk');
               
               
                var len = cboxes.length;
                
                datainfo = "";
                count = 0;
                countTotal = 0;
               
               
                    for (var i = 0; i < len; i++) {

                        // alert(i + (cboxes[i].checked ? ' checked ' : ' unchecked ') + cboxes[i].value);
                        if (cboxes[i].checked) {
                            total = total + 1;
                            
                           
                            if (i == 0 && total <= 3)
                            {
                                datainfo = "RC-Relief Center";
                            }

                            if (i == 1 && total <= 3) {
                                datainfo = datainfo + "<br/>CK-Community Kitchen";

                            }
                            if (i == 2 && total <= 3) {
                                datainfo = datainfo + "<br/>PE-Population Evacuated";

                            }
                            if (i == 3 && total <= 3) {
                                datainfo = datainfo + "<br/>DE-Deaths";


                            }
                            if (i == 4 && total <= 3) {
                                datainfo = datainfo + "<br/>HC-Health Center";

                            }
                            if (i == 5 && total <= 3) {
                                datainfo = datainfo + "<br/>HD-House Damage";

                            }
                            if (i == 6 && total <= 3) {
                                datainfo = datainfo + "<br/>GV-Gov. Boat";
                            }
                            if (i == 7 && total <= 3) {
                                datainfo = datainfo + "<br/>MB-Motor Boat";

                            }
                            if (i == 8  && total <= 3) {
                                datainfo = datainfo + "<br/>PB-Pvt. Boat";

                            }
                            if (i == 9 && total <= 3) {
                                datainfo = datainfo + "<br/>PS-Polythene Sheet";

                            }
                            
                            

                        }
                        if (total > 3) {
                            alert("Please Select Any three Only ")
                            cboxes[i].checked = false;
                            return false;
                        }
                       
                    }
                
              
               
              
                 
               
               
               


                
               
               
               
               

                var ddlFacility = document.getElementById("ddlFacility");
                var dateFrom = document.getElementById("txtdatefrom");
                var dateTo = document.getElementById("txtDateTo");
                
                const topology = await fetch(
                    'DistrictData.ashx?FromDate=' + dateFrom.value + '&ToDate=' + dateTo.value
                ).then(response => response.json());
                const data = Highcharts.geojson(topology);
                dataDistrict = data;

                //const mapView = topology.objects.default['hc-recommended-mapview'];
                // Set drilldown pointers

                const Seriesdata0 = [];
                const Seriesdata0_20 = [];
                const Seriesdata21_50 = [];
                const Seriesdata51_80 = [];
                const Seriesdata81_100 = [];
                var dataDoughnut = "";
                if (table)
                {
                    table.destroy();
                }
                $('#FloodDataTable').empty();
                $('#FloodDataTable').append('<thead><tr class="bg-black text-white">' +
                    '<th> S. No </th>' +
                    '<th> DISTRICT </th>' +
                    '<th> Total Block </th>' +
                    '<th> Total Affected Block </th>' +
                    '<th> Total Affected Panchayat </th>' +
                    '<th> Total Kitchens </th>' +
                    '<th> Total Relief Centres </th>' +
                    '<th> Total Deaths </th>' +
                    '<th> Total Evacuated People </th>' +
                    '<th> Total Flood Entry </th>' +
                   
                    '<th> Total Damaged Houses </th>' +
                    '<th> Total Gov. Boats </th>' +
                    '<th> Total Motor Boats </th>' +                    
                    '<th> total Pvt. Boats </th>' +
                    '<th> Total PolytheneSheet Dist. </th>' +
                    '<th>    %age  <br>(Aff.Block*100) /Tot.BLock </th>' +
                    '</tr></thead>');
                $('#FloodDataTable').append('<tbody>');
                data.forEach((d, i) => {
                    d.drilldown = d.properties['DISTRICT'];
                    d.name = d.properties['DISTRICT'] + " BLOCK WISE REPORT";
                    d.value = d.properties['DIST_CODE'];
                    d.Per = d.properties['Per'];
                    d.color = d.properties['color'];
                    d.TotalKitchens = d.properties['TotalKitchens'];
                    d.TotalReliefCentres = d.properties['TotalReliefCentres'];
                    d.TotalDeaths = d.properties['TotalDeaths'];
                    d.MigratedPopulation = d.properties['MigratedPopulation'];
                    d.totalFloodEntry = d.properties['totalFloodEntry'];
                    d.EntryPer = d.properties['EntryPer'];
                    d.Entrycolor = d.properties['Entrycolor'];
                    d.totalDamagedHousesValue = d.properties['totalDamagedHousesValue'];
                    d.totalGovtBoatToday = d.properties['totalGovtBoatToday'];
                    d.totalMotorBoatToday = d.properties['totalMotorBoatToday'];
                    d.totalPolytheneSheetDist = d.properties['totalPolytheneSheetDist'];
                    d.totalPvtBoatToday = d.properties['totalPvtBoatToday'];
                    if (d.properties['Entrycolor'] !='#3EC70B')
                    {
                        count++;
                        
                    }
                    countTotal++;

                    if (ddlFacility.value == 5) {
                        d.Per = d.properties['EntryPer'];
                        d.color = d.properties['Entrycolor'];
                        if (parseInt(d.properties['EntryPer']) == 0) {
                            Seriesdata0.push(d);
                        }
                        if (parseInt(d.properties['EntryPer']) > 0 && parseInt(d.properties['EntryPer']) <= 20) {
                            Seriesdata0_20.push(d);
                        }
                        if (parseInt(d.properties['EntryPer']) > 20 && parseInt(d.properties['EntryPer']) <= 50) {
                            Seriesdata21_50.push(d);
                        }
                        if (parseInt(d.properties['EntryPer']) > 50 && parseInt(d.properties['EntryPer']) <= 80) {
                            Seriesdata51_80.push(d);
                        }
                        if (parseInt(d.properties['EntryPer']) > 80) {
                            Seriesdata81_100.push(d);
                        }
                    }
                    else {
                        if (parseInt(d.properties['Per']) == 0) {
                            Seriesdata0.push(d);
                        }
                        if (parseInt(d.properties['Per']) > 0 && parseInt(d.properties['Per']) <= 20) {
                            Seriesdata0_20.push(d);
                        }
                        if (parseInt(d.properties['Per']) > 20 && parseInt(d.properties['Per']) <= 50) {
                            Seriesdata21_50.push(d);
                        }
                        if (parseInt(d.properties['Per']) > 50 && parseInt(d.properties['Per']) <= 80) {
                            Seriesdata51_80.push(d);
                        }
                        if (parseInt(d.properties['Per']) > 80) {
                            Seriesdata81_100.push(d);
                        }

                    }

                    
                    lblTotalAffected.innerHTML = count;
                    lbltotalDist.innerHTML = countTotal;
                    lblAffDist.innerHTML = "Only Affected Districts";
                    lblDist.innerHTML = "Affected Districts";
                    dataDoughnut = dataDoughnut + "{ name : '" + d.properties['DISTRICT'] + "', y : " + d.properties['Per'] + "},";
                    if (chkAffected.checked == true) {
                        if (parseInt(d.properties['Per']) != 0) {
                            $('#FloodDataTable').append('<tr style="background-color:' + d.properties['color'] + '">' +
                                '<td> ' + (parseInt(i) + 1) + ' </td>' +
                                '<td> ' + d.properties['DISTRICT'] + ' </td>' +
                                '<td> ' + d.properties['TotalBlock'] + ' </td>' +
                                '<td> ' + d.properties['TotalEffectedBlock'] + ' </td>' +
                                '<td> ' + d.properties['TotalEffectedPanchayat'] + ' </td>' +
                                '<td> ' + d.properties['TotalKitchens'] + ' </td>' +
                                '<td> ' + d.properties['TotalReliefCentres'] + ' </td>' +
                                '<td> ' + d.properties['TotalDeaths'] + ' </td>' +
                                '<td> ' + d.properties['MigratedPopulation'] + ' </td>' +
                                '<td> ' + d.properties['totalFloodEntry'] + ' </td>' +                                
                                '<td> ' + d.properties['totalDamagedHousesValue'] + ' </td>' +
                                '<td> ' + d.properties['totalGovtBoatToday'] + ' </td>' +
                                '<td> ' + d.properties['totalMotorBoatToday'] + ' </td>' +                               
                                '<td> ' + d.properties['totalPvtBoatToday'] + ' </td>' +
                                '<td> ' + d.properties['totalPolytheneSheetDist'] + ' </td>' +
                                '<td> ' + d.properties['Per'] + ' </td>' +
                                '</tr > ');
                        }
                    }
                    else
                    {
                        $('#FloodDataTable').append('<tr style="background-color:' + d.properties['color'] + '">' +
                            '<td> ' + (parseInt(i) + 1) + ' </td>' +
                            '<td> ' + d.properties['DISTRICT'] + ' </td>' +
                            '<td> ' + d.properties['TotalBlock'] + ' </td>' +
                            '<td> ' + d.properties['TotalEffectedBlock'] + ' </td>' +
                            '<td> ' + d.properties['TotalEffectedPanchayat'] + ' </td>' +
                            '<td> ' + d.properties['TotalKitchens'] + ' </td>' +
                            '<td> ' + d.properties['TotalReliefCentres'] + ' </td>' +
                            '<td> ' + d.properties['TotalDeaths'] + ' </td>' +
                            '<td> ' + d.properties['MigratedPopulation'] + ' </td>' +
                            '<td> ' + d.properties['totalFloodEntry'] + ' </td>' +                            
                            '<td> ' + d.properties['totalDamagedHousesValue'] + ' </td>' +
                            '<td> ' + d.properties['totalGovtBoatToday'] + ' </td>' +
                            '<td> ' + d.properties['totalMotorBoatToday'] + ' </td>' +                           
                            '<td> ' + d.properties['totalPvtBoatToday'] + ' </td>' +
                            '<td> ' + d.properties['totalPolytheneSheetDist'] + ' </td>' +
                            '<td> ' + d.properties['Per'] + ' </td>' +
                            '</tr > ');
                    }
                   
 

                });
             
                //if (x == 1) {
                //    legendTitle = "Flood Affected district";

                //}
                //else {
                //    legendTitle = "Flood Affected block";

                //}
               
                dataDoughnut = dataDoughnut.slice(0, -1);
                $('#FloodDataTable').append('</tbody>');
                table = $("#FloodDataTable").DataTable({
                    "paging": true,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": true,
                    "sDom": 'lfrtip'
                });


                // Instantiate the map
                Highcharts.mapChart('containermap', {
                    chart:
                    {
                        events:
                        {

                            drilldown, 
                            drillup,
                            load: function () {
                               
                                
                                
                                //var point = this.series[0].points[0].graphic.getBBox();
                                //var x = point.x;
                                //var plotLeft = this.plotRight;
                                //var width = point.height;

                                //var y = point.y;
                                //var plotTop=this.plotBottom;


                                //this.renderer.text('test : 912283',  this.plotWidth+100, 100)
                                //    .attr({
                                //        align: 'right',
                                //        zIndex: 3
                                //    })
                                //    .css({
                                //        color: '#4572A7',
                                //        fontSize: '16px',
                                //        fill:'black'
                                //    })
                                //    .add();




                            }
                        }
                    },
                    title:
                    {

                        text: chartTitle
                       // text: 'EOC - Disaster Management Department, Government of Bihar(Flood Data)'
                    },
                    //event:
                    //{
                    //    drilldown: function (e) {
                    //        chart.setTitle({ text: drilldownTitle + e.point.name });
                    //    },
                    //    drillup: function (e) {
                    //        chart.setTitle({ text: defaultTitle });
                    //    }
                    //},
                    subtitle:
                    {
                        text: dataOnDate
                    },
                    mapNavigation:
                    {
                        enabled: true,
                        buttonOptions:
                        {
                            verticalAlign: 'top', horizontalAlign: 'right'
                        }
                    },
                    tooltip:
                    {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        formatter: function () {
                            if (this.series.chart.drilldownLevels !== UNDF && this.series.chart.drilldownLevels.length > 0)
                            {

                                var datainfoData = '<b style="font-size:20px;">' + this.point.properties.BLK_NAME + '<Br>' + this.point.properties.Per + ' % Panchayat  Affected</b><br/>';
                                datainfoData = datainfoData + '<b style="font-size:15px;">Panchayat - ' + this.point.properties.TotalEffectedPanchayat + ' / ' + this.point.properties.TotalPanchayat + '</b><br/>';
                                                                
                                if (chkReliefCenter.checked == true) {
                                    datainfoData = datainfoData + 'Relief Centre-<p style="font-size:15px;">' + this.point.properties.TotalReliefCentres + '</p>';
                                }
                               

                                if (chkCommunityKitchen.checked == true) {
                                    datainfoData = datainfoData + '<br/>Community Kitchen-<p style="font - size: 15px; ">' + this.point.properties.TotalKitchens + '</p>';
                                }
                                if (chkMigatedPeople.checked == true) {
                                    datainfoData = datainfoData + '<br/>Population Evacuated-<p style="font - size: 15px; ">' + this.point.properties.MigratedPopulation + '</p>';
                                }
                                if (chkFloodDeaths.checked == true) {
                                    datainfoData = datainfoData + '<br/>Deaths-<p style="font - size: 15px; ">' + this.point.properties.TotalDeaths + '</p>';

                                }
                                if (chkHealthCenter.checked == true) {
                                    datainfoData = datainfoData + '<br/>Health Centers-<p style="font - size: 15px; ">' + this.point.properties.MigratedPopulation + '</p>';
                                }
                                if (chkHouseDamage.checked == true) {
                                    datainfoData = datainfoData + '<br/>House Damage-<p style="font-size:15px;">' + this.point.properties.totalDamagedHousesValue + '</p>';
                                }
                                if (chkMotorBoats.checked == true) {
                                    datainfoData = datainfoData + '<br/>Motor Boat-<p style="font-size:15px;">' + this.point.properties.totalMotorBoatToday + '</p>';
                                }
                                if (chkPvtBoats.checked == true) {
                                    datainfoData = datainfoData + '<br/>Pvt.Boat-<p style="font-size:15px;">' + this.point.properties.totalPvtBoatToday + '</p>';
                                }
                                if (chkPolythene.checked == true) {
                                    datainfoData = datainfoData + '<br/>Polythene Sheet-<p style="font-size:15px;">' + this.point.properties.totalPolytheneSheetDist + '</p>';
                                }
                                if (chkGovBoats.checked == true) {
                                    datainfoData = datainfoData + '<br/>Gov.Boat-<p style="font-size:15px;">' + this.point.properties.totalGovtBoatToday + '</p>';
                                }

                                return datainfoData;
                            }
                            else
                            {
                                var datainfoData = '<b style="font-size:20px;">' + this.point.properties.DISTRICT + '<Br>' + this.point.properties.Per + ' % Block Affected</b><br/>';
                                datainfoData = datainfoData + '<b style="font-size:15px;">Block - ' + this.point.properties.TotalEffectedBlock + ' / ' + this.point.properties.TotalBlock + '</b><br/>';
                                if (chkReliefCenter.checked == true)
                                {
                                    datainfoData = datainfoData + 'Relief Centre-<p style="font-size:15px;">' + this.point.properties.TotalReliefCentres + '</p>';
                                }

                                if (chkCommunityKitchen.checked == true) {
                                    datainfoData = datainfoData + '<br/>Community Kitchen-<p style="font - size: 15px; ">' + this.point.properties.TotalKitchens + '</p>';
                                }
                                if (chkMigatedPeople.checked == true) {
                                    datainfoData = datainfoData + '<br/>Population Evacuated-<p style="font - size: 15px; ">' + this.point.properties.MigratedPopulation + '</p>';
                                }
                                if (chkFloodDeaths.checked == true) {
                                    datainfoData = datainfoData + '<br/>Deaths-<p style="font - size: 15px; ">' + this.point.properties.TotalDeaths + '</p>';

                                }
                                if (chkHealthCenter.checked == true) {
                                    datainfoData = datainfoData + '<br/>Health Centers-<p style="font - size: 15px; ">' + this.point.properties.MigratedPopulation + '</p>';
                                }
                                if (chkHouseDamage.checked == true) {
                                    datainfoData = datainfoData + '<br/>House Damage-<p style="font-size:15px;">' + this.point.properties.totalDamagedHousesValue + '</p>';
                                }
                                if (chkMotorBoats.checked == true) {
                                    datainfoData = datainfoData + '<br/>Motor Boat-<p style="font-size:15px;">' + this.point.properties.totalMotorBoatToday + '</p>';
                                }
                                if (chkPvtBoats.checked == true) {
                                    datainfoData = datainfoData + '<br/>Pvt.Boat-<p style="font-size:15px;">' + this.point.properties.totalPvtBoatToday + '</p>';
                                }
                                if (chkPolythene.checked == true) {
                                    datainfoData = datainfoData + '<br/>Polythene Sheet-<p style="font-size:15px;">' + this.point.properties.totalPolytheneSheetDist + '</p>';
                                }
                                if (chkGovBoats.checked == true) {
                                    datainfoData = datainfoData + '<br/>Gov.Boat-<p style="font-size:15px;">' + this.point.properties.totalGovtBoatToday + '</p>';
                                }

                                return datainfoData;
                            }
                        }
                    }
                    ,
                    plotOptions:
                    {

                        map:
                        {
                            joinBy: ['DISTRICT', 'DISTRICT'],
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
                            text: '%age Flood Affected Block <br/><span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo+'</span>',
                            style:
                            {
                                fontStyle: 'italic'
                            }
                        },
                        useHTML: true,
                        reversed: true,
                        labelFormatter: function ()
                        {
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
                                       
                                        var datainfoData = '<b style="font-size:20px;">' + this.point.properties.BLK_NAME +  ' </b><br/>';
                                        datainfoData = datainfoData + '<b style="font-size:15px;">Panchayat - ' + this.point.properties.TotalEffectedPanchayat + ' / ' + this.point.properties.TotalPanchayat + '</b><br/>';
                                        if (chkReliefCenter.checked == true) {
                                            datainfoData = datainfoData + 'RC-<p style="font-size:15px; ">' + this.point.properties.TotalReliefCentres + '</p>';
                                        }

                                        if (chkCommunityKitchen.checked == true) {
                                            datainfoData = datainfoData + ', CK-<p style="font - size: 15px; ">' + this.point.properties.TotalKitchens + '</p>';
                                        }
                                        if (chkMigatedPeople.checked == true) {
                                            datainfoData = datainfoData + ', PE-<p style="font - size: 15px; ">' + this.point.properties.MigratedPopulation + '</p>';
                                        }
                                        if (chkFloodDeaths.checked == true) {
                                            datainfoData = datainfoData + '<br/>DE-<p style="font - size: 15px; ">' + this.point.properties.TotalDeaths + '</p>';

                                        }
                                        if (chkHealthCenter.checked == true) {
                                            datainfoData = datainfoData + ', HC-<p style="font - size: 15px; ">' + this.point.properties.MigratedPopulation + '</p>';
                                        }
                                        if (chkHouseDamage.checked == true) {
                                            datainfoData = datainfoData + 'HD-<p style="font-size:15px;">' + this.point.properties.totalDamagedHousesValue + '</p>';
                                        }
                                        if (chkMotorBoats.checked == true) {
                                            datainfoData = datainfoData + 'MB-<p style="font-size:15px;">' + this.point.properties.totalMotorBoatToday + '</p>';
                                        }
                                        if (chkPvtBoats.checked == true) {
                                            datainfoData = datainfoData + '<br> PB-<p style="font-size:15px;">' + this.point.properties.totalPvtBoatToday + '</p>';
                                        }
                                        if (chkPolythene.checked == true) {
                                            datainfoData = datainfoData + ', PS-<p style="font-size:15px;">' + this.point.properties.totalPolytheneSheetDist + '</p>';
                                        }
                                        if (chkGovBoats.checked == true) {
                                            datainfoData = datainfoData + ', GB-<p style="font-size:15px;">' + this.point.properties.totalGovtBoatToday + '</p>';
                                        }
                                        return datainfoData;
                                    }
                                    else {
                                        
                                        var datainfoData = '<b style="font-size:20px;">' + this.point.properties.DISTRICT + '</b><br/>';
                                        datainfoData = datainfoData + '<b style="font-size:15px;">Block - ' + this.point.properties.TotalEffectedBlock + ' / ' + this.point.properties.TotalBlock + '</b><br/>';
                                        if (chkReliefCenter.checked == true) {
                                            datainfoData = datainfoData + 'RC-<p style="font-size:15px;">' + this.point.properties.TotalReliefCentres + '</p>';
                                        }

                                        if (chkCommunityKitchen.checked == true) {
                                            datainfoData = datainfoData + ', CK-<p style="font - size: 15px; ">' + this.point.properties.TotalKitchens + '</p>';
                                        }
                                        if (chkMigatedPeople.checked == true) {
                                            datainfoData = datainfoData + ', PE-<p style="font - size: 15px; ">' + this.point.properties.MigratedPopulation + '</p>';
                                        }
                                        if (chkFloodDeaths.checked == true) {
                                            datainfoData = datainfoData + '<br/>DE-<p style="font - size: 15px; ">' + this.point.properties.TotalDeaths + '</p>';

                                        }
                                        if (chkHealthCenter.checked == true) {
                                            datainfoData = datainfoData + ', HC-<p style="font - size: 15px; ">' + this.point.properties.MigratedPopulation + '</p>';
                                        }
                                        if (chkHouseDamage.checked == true) {
                                            datainfoData = datainfoData + ', HD-<p style="font-size:15px;">' + this.point.properties.totalDamagedHousesValue + '</p>';
                                        }
                                        if (chkMotorBoats.checked == true) {
                                            datainfoData = datainfoData + ', MB-<p style="font-size:15px;">' + this.point.properties.totalMotorBoatToday + '</p>';
                                        }
                                        if (chkPvtBoats.checked == true) {
                                            datainfoData = datainfoData + '<br> PB-<p style="font-size:15px;">' + this.point.properties.totalPvtBoatToday + '</p>';
                                        }
                                        if (chkPolythene.checked == true) {
                                            datainfoData = datainfoData + ', PS-<p style="font-size:15px;">' + this.point.properties.totalPolytheneSheetDist + '</p>';
                                        }
                                        if (chkGovBoats.checked == true) {
                                            datainfoData = datainfoData + ', GB-<p style="font-size:15px;">' + this.point.properties.totalGovtBoatToday + '</p>';
                                        }
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
                                name: "0% Not Affected",
                                dataLabels:
                                {
                                    enabled: true,
                                    //format: '<b>{point.properties.DISTRICT}</b><br/><p style="color:black;">Block- {point.properties.TotalEffectedBlock} / {point.properties.TotalBlock} ( {point.properties.Per} %)</p>',
                                    shadow: true,
                                    className: 'DataLabelCss'


                                },
                                color: '#3EC70B',
                                borderColor: 'black',
                                borderWidth: 1

                            },
                            {
                                data: Seriesdata0_20,
                                name: "0-20% Affected",
                                dataLabels:
                                {
                                    enabled: true,

                                    //  format: '<b>{point.properties.DISTRICT}</b><br/><p style="color:black;">Block- {point.properties.TotalEffectedBlock} / {point.properties.TotalBlock} ( {point.properties.Per} %)</p>',
                                    shadow: true,
                                    className: 'DataLabelCss'


                                },
                                color: '#FCD900',
                                borderColor: 'black',
                                borderWidth: 1

                            }
                            ,
                            {
                                data: Seriesdata21_50,
                                name: "21-50% Affected",
                                dataLabels:
                                {
                                    enabled: true,
                                    //  format: '<b>{point.properties.DISTRICT}</b><br/><p style="color:black;">Block- {point.properties.TotalEffectedBlock} / {point.properties.TotalBlock} ( {point.properties.Per} %)</p>',
                                    shadow: true,
                                    className: 'DataLabelCss'


                                },
                                color: '#F77E21',
                                borderColor: 'black',
                                borderWidth: 1

                            }
                            ,
                            {
                                data: Seriesdata51_80,
                                name: "51-80% Affected",
                                dataLabels:
                                {
                                    enabled: true,
                                    //  format: '<b>{point.properties.DISTRICT}</b><br/><p style="color:black;">Block- {point.properties.TotalEffectedBlock} / {point.properties.TotalBlock} ( {point.properties.Per} %)</p>',
                                    shadow: true,
                                    className: 'DataLabelCss'


                                },
                                color: '#FA4753',
                                borderColor: 'black',
                                borderWidth: 1

                            },
                            {
                                data: Seriesdata81_100,
                                name: "> 80% Affected",
                                dataLabels:
                                {
                                    enabled: true,
                                    // format: '<b>{point.properties.DISTRICT}</b><br/><p style="color:black;">Block- {point.properties.TotalEffectedBlock} / {point.properties.TotalBlock} ( {point.properties.Per} %)</p>',
                                    shadow: true,
                                    className: 'DataLabelCss'


                                },
                                color: 'red',
                                borderColor: 'black',
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

            })();
        }
        BindMapData();


    </script>
</body>
</html>