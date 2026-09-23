<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Map - Copy.aspx.cs" Inherits="EOC_dmd_floodmapdata_Map" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link href="css/map.css" rel="stylesheet" />
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
                    <center><b><p style="color:#354259; font-size:25px; padding:10px;">भू - समाधान</p></b></center>

         
                    <div class="col-12 text-center" style="background-color:darkslategray">
                        <a  style="text-decoration: none; color:white; display:block; border-radius: 8px;" class="collapse-item btn" href="#" onclick="window.history.go(-1); return false;">HOME <i class="fa fa-home" aria-hidden="true"></i></a>
                    </div>
                    <br />
                   
                    <div class="col-12">
                        <fieldset>
                            <legend>भूमि विवाद का प्रकार </legend>

                            <div>
                                <input type="checkbox" id="chk1" name="chkLandDispute" value="1" onchange="BindMapData();"/>
                                <label for="chk1">पर्चाधारी के बेदखली का मामला</label>
                            </div>

                            <div>
                                <input type="checkbox" id="chk2" name="chkLandDispute" value="2" onchange="BindMapData();"/>
                                <label for="chk2">सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद</label>
                            </div>
                            <div>
                                <input type="checkbox" id="chk3" name="chkLandDispute" value="3" onchange="BindMapData();"/>
                                <label for="chk3">रैयती भूमि पर सीमांकन या सीमा का विवाद</label>
                            </div>
                            
                         <div>
                                <input type="checkbox" id="chk4" name="chkLandDispute" value="4" onchange="BindMapData();"/>
                                <label for="chk4">निजी रास्ता / नाली का विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk5" name="chkLandDispute" value="5" onchange="BindMapData();"/>
                                <label for="chk5">जल स्रोत का विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk6" name="chkLandDispute" value="6" onchange="BindMapData();"/>
                                <label for="chk6">पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk7" name="chkLandDispute" value="7" onchange="BindMapData();"/>
                                <label for="chk7">खेती से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk8" name="chkLandDispute" value="8" onchange="BindMapData();"/>
                                <label for="chk8">वास से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk9" name="chkLandDispute" value="9" onchange="BindMapData();"/>
                                <label for="chk9">लगान निर्धारण का विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk10" name="chk10" value="10" onchange="BindMapData();"/>
                                <label for="chk10">व्यावसायिक भूमि से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk11" name="chkLandDispute" value="11" onchange="BindMapData();"/>
                                <label for="chk11">बदलेन भूमि से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk12" name="chkLandDispute"  value="12" onchange="BindMapData();"/>
                                <label for="chk12">भू- अर्जन से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk13" name="chkLandDispute"  value="13" onchange="BindMapData();"/>
                                <label for="chk13">भू- हदबंदी (अधिशेष) से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk15" name="chkLandDispute"  value="15" onchange="BindMapData();"/>
                                <label for="chk15">रैयती भूमि पर कब्ज़ा का विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk20" name="chkLandDispute"  value="16" onchange="BindMapData();"/>
                                <label for="chk20">अन्य</label>
                            </div>
                            
                           
                        </fieldset>
                    </div>
                </div>
                <div class="col-10">
                    <div class="row">

                        <div class="col-2">
                            From
                            <asp:TextBox TextMode="Date" ID="txtdatefrom" runat="server" CssClass="form-control" Text="2022-01-01"></asp:TextBox>
                             
                        </div>
                        <div class="col-2">
                            To
                           <asp:TextBox TextMode="Date" ID="txtDateTo" runat="server" CssClass="form-control"></asp:TextBox>

                        </div>
                        <div class="col-1">
                            <br />
                            <asp:Button ID="btnSubmit" Text="Search" runat="server" CssClass="btn btn-primary" OnClientClick="myFunction();" />
                        </div>
                         <div class="col-7">
                             </div>
                    </div>
                    <hr />
                    <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">
                            
                                <div class="col-12">
                                    <div id="containermap">
                                    </div>
                                </div>
                                <%--<div class="text-center">
                                    <asp:Label ID="lblDist" runat="server" Text="Total Affectd District:"></asp:Label>
                                 
                                <asp:Label ID="lblTotalAffected" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lblTotalDistrict" runat="server" Text="38"></asp:Label>
                                    </div>
                                <div class="text-left">
                                    <input type="checkbox" id="chkAffected" name="chkAffected" onchange="BindMapData();"/>
                                     <asp:Label ID="lblAffDist" runat="server" Text="Affectd District:"></asp:Label>
                                 
                                </div>--%>
                            
                                <br />
                                <div class="col-12">
                                    <div style="overflow:scroll; width:100%;">
                                        <div class="container-fluid">
                                            <table id="LandDistibuteDataTable" class="table table-responsive table-hover"></table>
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
       var count = 0, countTotal = 0;
       var datefrom, todate, legendTitle;
       datefrom = document.getElementById("txtdatefrom").value;
       todate = document.getElementById("txtDateTo").value;
       

       var dataOnDate = 'Data On date: (' + document.getElementById("txtdatefrom").value + ') to (' + document.getElementById("txtDateTo").value + ')';
       function myFunction() {
           
           //document.getElementById("demo").innerHTML = "You selected: " + x;
           // alert(x);
       }
       var someFlag = true;
       var chartTitle = 'भू - समाधान Home Department Government of Bihar ';
       var table;
       var dataDistrict, txtdatefrom, txtdatefrom, Message;
       Message = document.getElementById("Message");


       const drilldownBlock = async function (e) {
           debugger;
           if (!e.seriesOptions) {
               point = e.point;
               const chart = this;
               const mapKey = '';
               const BSeriesdata0 = [];

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
                   'Block.ashx?District=' + e.point.value + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=0'
               ).then(response => response.json());
               const dataDrilldown = Highcharts.geojson(topology);

               table.destroy();
               $("#LandDistibuteDataTable").html("");

               $('#LandDistibuteDataTable').append('<thead><tr class="bg-black text-white">' +
                   '<th> क्रम संख्या </th>' +
                   '<th> Block </th>' +
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
               count = 0;

               countTotal = 0;
               // Set a non-random bogus value

               var klSamanya = 0;
               var klSumvadansheel = 0;
               var klAtiSumvadansheel = 0;

               dataDrilldown.forEach((d, i) => {
                   d.drilldownBlock = d.properties['BlockCode'];
                   d.BlockName = d.properties['BlockName'];
                   d.Samanya = d.properties['Samanya'];
                   d.Sumvadansheel = d.properties['Sumvadansheel'];
                   d.AtiSumvadansheel = d.properties['AtiSumvadansheel'];
                   d.Nirast = d.properties['Nirast'];
                   d.Ashwikrit = d.properties['Ashwikrit'];
                   d.Mapi_Nirdharit = d.properties['Mapi_Nirdharit'];
                   d.Prakriyadhin = d.properties['Prakriyadhin'];
                   d.FinalNirast = d.properties['FinalNirast'];

                   klSamanya = klSamanya + parseInt(d.properties['Samanya']);
                   klSumvadansheel = klSumvadansheel + parseInt(d.properties['Sumvadansheel']);
                   klAtiSumvadansheel = klAtiSumvadansheel + parseInt(d.properties['AtiSumvadansheel']);


                   BSeriesdata0.push(d);
                   $('#LandDistibuteDataTable').append('<tr style="background-color:' + d.properties['color'] + '">' +
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



               // Hide loading and add series
               chart.hideLoading();
               clearTimeout(fail);
               BSeriesdataAll.push(BSeriesdata0);
               legendTitle = "district landisbute";
               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: green; font- weight: bold">सामान्य : ' + klSamanya + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });
               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: yellow; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });

               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: red; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });







               if (x == 1) {
                   $(this.container).find('.highcharts-title').text('भू - समाधान Home Department Government of Bihar ');
                   legendTitle = "Vivad ";
                   x = 0;
               }


               chart.legend.title.attr({ text: 'भू - समाधान Home Department Government of Bihar test<br /> <span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>' });



               // lblAffectedDist.innerHTML = count;
               // lbltotalDist.innerHTML = countTotal
               // lblAffDist.innerHTML = "भू - समाधान";
               //  lblDist.innerHTML = "भू - समाधान data";

               chart.applyDrilldown();


           }
       };
       const drilldownPanchayata = async function (e) {
           debugger;
           if (!e.seriesOptions) {
               point = e.point;
               const chart = this;
               const mapKey = '';
               const BSeriesdata0 = [];

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
                   'Block.ashx?District=' + e.point.value + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=0'
               ).then(response => response.json());
               const dataDrilldown = Highcharts.geojson(topology);

               table.destroy();
               $("#LandDistibuteDataTable").html("");

               $('#LandDistibuteDataTable').append('<thead><tr class="bg-black text-white">' +
                   '<th> क्रम संख्या </th>' +
                   '<th> Block </th>' +
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
               count = 0;

               countTotal = 0;
               // Set a non-random bogus value

               var klSamanya = 0;
               var klSumvadansheel = 0;
               var klAtiSumvadansheel = 0;

               dataDrilldown.forEach((d, i) => {
                   d.drilldown = d.properties['BlockCode'];
                   d.BlockName = d.properties['BlockName'];
                   d.Samanya = d.properties['Samanya'];
                   d.Sumvadansheel = d.properties['Sumvadansheel'];
                   d.AtiSumvadansheel = d.properties['AtiSumvadansheel'];
                   d.Nirast = d.properties['Nirast'];
                   d.Ashwikrit = d.properties['Ashwikrit'];
                   d.Mapi_Nirdharit = d.properties['Mapi_Nirdharit'];
                   d.Prakriyadhin = d.properties['Prakriyadhin'];
                   d.FinalNirast = d.properties['FinalNirast'];

                   klSamanya = klSamanya + parseInt(d.properties['Samanya']);
                   klSumvadansheel = klSumvadansheel + parseInt(d.properties['Sumvadansheel']);
                   klAtiSumvadansheel = klAtiSumvadansheel + parseInt(d.properties['AtiSumvadansheel']);


                   BSeriesdata0.push(d);
                   $('#LandDistibuteDataTable').append('<tr style="background-color:' + d.properties['color'] + '">' +
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



               // Hide loading and add series
               chart.hideLoading();
               clearTimeout(fail);
               BSeriesdataAll.push(BSeriesdata0);
               legendTitle = "district landisbute";
               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: green; font- weight: bold">सामान्य : ' + klSamanya + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });
               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: yellow; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });

               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: red; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       events:
                       {

                           drilldownPanchayata,
                           drillup,
                           load: function () {


                           }
                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });







               if (x == 1) {
                   $(this.container).find('.highcharts-title').text('भू - समाधान Home Department Government of Bihar ');
                   legendTitle = "Vivad ";
                   x = 0;
               }


               chart.legend.title.attr({ text: 'भू - समाधान Home Department Government of Bihar test<br /> <span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>' });



               // lblAffectedDist.innerHTML = count;
               // lbltotalDist.innerHTML = countTotal
               // lblAffDist.innerHTML = "भू - समाधान";
               //  lblDist.innerHTML = "भू - समाधान data";

               chart.applyDrilldown();


           }
       };

       const drilldown = async function (e) {
           debugger;
           if (!e.seriesOptions) {
               point = e.point;
               const chart = this;
               const mapKey = '';
               const BSeriesdata0 = [];
             
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
                   'Block.ashx?District=' + e.point.value + '&fromdate=' + datefrom + '&todate=' + todate + '&BhumiVivadType=0'
               ).then(response => response.json());
               const dataDrilldown = Highcharts.geojson(topology);

               table.destroy();
               $("#LandDistibuteDataTable").html("");

               $('#LandDistibuteDataTable').append('<thead><tr class="bg-black text-white">' +
                   '<th> क्रम संख्या </th>' +
                   '<th> Block </th>' +
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
               count = 0;

               countTotal = 0;
               // Set a non-random bogus value

               var klSamanya = 0;
               var klSumvadansheel = 0;
               var klAtiSumvadansheel = 0;

               dataDrilldown.forEach((d, i) => {
                   d.drilldown = d.properties['BlockCode'];
                   d.BlockName = d.properties['BlockName'];
                   d.Samanya = d.properties['Samanya'];
                   d.Sumvadansheel = d.properties['Sumvadansheel'];
                   d.AtiSumvadansheel = d.properties['AtiSumvadansheel'];
                   d.Nirast = d.properties['Nirast'];
                   d.Ashwikrit = d.properties['Ashwikrit'];
                   d.Mapi_Nirdharit = d.properties['Mapi_Nirdharit'];
                   d.Prakriyadhin = d.properties['Prakriyadhin'];
                   d.FinalNirast = d.properties['FinalNirast'];

                   klSamanya = klSamanya + parseInt(d.properties['Samanya']);
                   klSumvadansheel = klSumvadansheel + parseInt(d.properties['Sumvadansheel']);
                   klAtiSumvadansheel = klAtiSumvadansheel + parseInt(d.properties['AtiSumvadansheel']);


                   BSeriesdata0.push(d);                                                                       
                       $('#LandDistibuteDataTable').append('<tr style="background-color:' + d.properties['color'] + '">' +
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

              

               // Hide loading and add series
               chart.hideLoading();
               clearTimeout(fail);
               BSeriesdataAll.push(BSeriesdata0);              
               legendTitle = "district landisbute";
               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: green; font- weight: bold">सामान्य : ' + klSamanya + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       events:
                       {

                           drilldownPanchayata,
                           drillup,
                           load: function () {


                           }
                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });
               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: yellow; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       events:
                       {

                           drilldownPanchayata,
                           drillup,
                           load: function () {


                           }
                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });

               chart.addSingleSeriesAsDrilldown(e.point,
                   {
                       name: '<span style="font- size: 9px; color: red; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span>',
                       data: BSeriesdata0,
                       color: '#3EC70B',
                       dataLabels:
                       {
                           enabled: true,
                           shadow: true,
                           className: 'DataLabelCss',
                           style: {
                               color: 'red',
                               textOutline: false
                           }


                       },
                       events:
                       {

                           drilldownPanchayata,
                           drillup,
                           load: function () {


                           }
                       },
                       borderColor: 'red',
                       borderWidth: 1
                   });

              
               
               

              

               if (x == 1) {
                   $(this.container).find('.highcharts-title').text('भू - समाधान Home Department Government of Bihar ');
                   legendTitle = "Vivad ";
                   x = 0;
               }


               chart.legend.title.attr({ text: 'भू - समाधान Home Department Government of Bihar test<br /> <span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>' });



              // lblAffectedDist.innerHTML = count;
              // lbltotalDist.innerHTML = countTotal
              // lblAffDist.innerHTML = "भू - समाधान";
             //  lblDist.innerHTML = "भू - समाधान data";

               chart.applyDrilldown();


           }
       };
       var UNDF;
       // On drill up, reset to the top-level map view
       const drillup = function (e) {
           debugger;
           if (e.seriesOptions.custom && e.seriesOptions.custom.mapView) {
               e.target.mapView.update(e.seriesOptions.custom.mapView, false);

           }
           this.legend.title.attr({ text: ' भू - समाधान Home Department Government of Bihar test 3<br /> <span style="font-size: 9px; color: #666; font-weight: normal">' + datainfo + '</span>' });
           //alert('sfsdfs');
           // legendTitle = "Flood Affected District";


           if (x == 0) {
               $(this.container).find('.highcharts-title').text('भू - समाधान Home Department Government of Bihar');

               // $(this.container).find(' .highcharts - subtitle').text(chartTitle + 'District');

               x = 1;
           }

           //console.log(d.properties['color']);

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
       function BindMapData() {
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
               for (var i = 0; i < len; i++) {                   
                   if (cboxes[i].checked) {
                       total = total + 1;                   
                       if (i == 0 && total <= 3) {
                           datainfo = "पर्चाधारी के बेदखली का मामला";
                           p = p + "," + cboxes[i].value;
                       }

                       if (i == 1 && total <= 3) {
                           datainfo = datainfo + "<br/>सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद";
                           p = p + "," + cboxes[i].value;

                       }
                       if (i == 2 && total <= 3) {
                           datainfo = datainfo + "<br/>रैयती भूमि पर सीमांकन या सीमा का विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 3 && total <= 3) {
                           datainfo = datainfo + "<br/>निजी रास्ता / नाली का विवाद";
                           p = p + "," + cboxes[i].value;

                       }
                       if (i == 4 && total <= 3) {
                           datainfo = datainfo + "<br/>जल स्रोत का विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 5 && total <= 3) {
                           datainfo = datainfo + "<br/>पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 6 && total <= 3) {
                           datainfo = datainfo + "<br/>खेती से संबंधित विवाद";
                       }
                       if (i == 7 && total <= 3) {
                           datainfo = datainfo + "<br/>वास से संबंधित विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 8 && total <= 3) {
                           datainfo = datainfo + "<br/>लगान निर्धारण का विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 9 && total <= 3) {
                           datainfo = datainfo + "<br/>व्यावसायिक भूमि से संबंधित विवाद";
                           p = p + "," + cboxes[i].value;
                       }

                       if (i == 10 && total <= 3) {
                           datainfo = datainfo + "<br/>बदलेन भूमि से संबंधित विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 11 && total <= 3) {
                           datainfo = datainfo + "<br/>भू- अर्जन से संबंधित विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 12 && total <= 3) {
                           datainfo = datainfo + "<br/>भू- हदबंदी (अधिशेष) से संबंधित विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 13 && total <= 3) {
                           datainfo = datainfo + "<br/>रैयती भूमि पर कब्ज़ा का विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 14 && total <= 3) {
                           datainfo = datainfo + "<br/>अन्य";
                           p = p + "," + cboxes[i].value;
                       }
                       if (i == 15 && total <= 3) {
                           datainfo = datainfo + "<br/>व्यावसायिक भूमि से संबंधित विवाद";
                           p = p + "," + cboxes[i].value;
                       }
                       


                   }
                   if (total > 3) {
                       alert("Please Select Any three Only ")
                       cboxes[i].checked = false;
                       return false;
                   }

               }
               var dateFrom = document.getElementById("txtdatefrom");
               var dateTo = document.getElementById("txtDateTo");

               const topology = await fetch(
                   'District.ashx?FromDate=' + dateFrom.value + '&ToDate=' + dateTo.value + '&BhumiVivadType=' + p
               ).then(response => response.json());
               const data = Highcharts.geojson(topology);
               dataDistrict = data;

               //const mapView = topology.objects.default['hc-recommended-mapview'];
               // Set drilldown pointers

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
                   d.drilldown = d.properties['District_Code'];
                   d.DISTRICTNAME = d.properties['DISTRICTNAME'];
                   d.value = d.properties['District_Code'];
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
               table = $("#LandDistibuteDataTable").DataTable({
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
                   tooltip:
                   {
                       headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                       formatter: function () {
                           if (this.series.chart.drilldownLevels !== UNDF && this.series.chart.drilldownLevels.length > 0) {

                               var datainfoData = '<b style="font-size:15px;">' + this.point.properties.BlockName + '<br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;color:green">सामान्य - ' + this.point.properties.Samanya + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;color:yellow">संवेदनशील - ' + this.point.properties.Sumvadansheel + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;color:red">अतिसंवेदनशील - ' + this.point.properties.AtiSumvadansheel + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">प्रारंभिक निष्पादन - ' + this.point.properties.Nirast + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">अस्वीकृत - ' + this.point.properties.Ashwikrit + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">मापी क़े लिए निर्धारित  - ' + this.point.properties.Mapi_Nirdharit + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">प्रक्रियाधीन - ' + this.point.properties.Prakriyadhin + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">अंतिम निष्पादन - ' + this.point.properties.FinalNirast + '</b><br/>';
                              

                               return datainfoData;
                           }
                           else {
                               var datainfoData = '<b style="font-size:15px;">' + this.point.properties.DISTRICTNAME + '<br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;color:green">सामान्य - ' + this.point.properties.Samanya + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;color:yellow">संवेदनशील - ' + this.point.properties.Sumvadansheel + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;color:red">अतिसंवेदनशील - ' + this.point.properties.AtiSumvadansheel + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">प्रारंभिक निष्पादन - ' + this.point.properties.Nirast + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">अस्वीकृत - ' + this.point.properties.Ashwikrit + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">मापी क़े लिए निर्धारित  - ' + this.point.properties.Mapi_Nirdharit + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">प्रक्रियाधीन - ' + this.point.properties.Prakriyadhin + '</b><br/>';
                               datainfoData = datainfoData + '<b style="font-size:12px;">अंतिम निष्पादन - ' + this.point.properties.FinalNirast + '</b><br/>';
                               
                               return datainfoData;
                           }
                       }
                   }
                   ,
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


                                       var datainfoData = '<b style="font-size:20px;">' + this.point.properties.BlockName + '<br/>';

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
                               name: '<span style="font- size: 9px; color: green; font- weight: bold">सामान्य : ' + klSamanya +'</span>' ,
                               dataLabels:
                               {
                                   enabled: true,                                   
                                   shadow: true,
                                   className: 'DataLabelCss'
                               },
                               color: '#3EC70B',
                              
                               borderColor: 'red',
                               borderWidth: 1
                           },
                           {
                               data: Seriesdata0,
                               name: '<span style="font- size: 9px; color: yellow; font- weight: bold">संवेदनशील : ' + klSumvadansheel + '</span>',
                               dataLabels:
                               {
                                   enabled: true,
                                   shadow: true,
                                   className: 'DataLabelCss'
                               },
                               color: '#3EC70B',

                               borderColor: 'red',
                               borderWidth: 1
                           },
                          
                           {
                               data: Seriesdata0,
                               name: '<span style="font- size: 9px; color: red; font- weight: bold">अतिसंवेदनशील : ' + klAtiSumvadansheel + '</span>',
                               dataLabels:
                               {
                                   enabled: true,
                                   shadow: true,
                                   className: 'DataLabelCss'
                               },
                               color: '#3EC70B',

                               borderColor: 'red',
                               borderWidth: 1
                           },
                          
                           

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
