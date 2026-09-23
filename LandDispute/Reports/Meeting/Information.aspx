<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Information.aspx.cs" 
    Inherits="LandDispute_Dashboard_Information" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
  <%--<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"/></script>--%>
      

    <script src="vendors/jQuery/jquery.min.js"></script>

    <style type="text/css">
        @media  {
            body {
                /* background: #e5e5e5;*/
                background: white;
                font-size: .80em;
                font-family: "Helvetica Neue", "Lucida Grande", "Segoe UI", Arial, Helvetica, Verdana, sans-serif;
                margin: 10px auto;
                padding: 0px;
                /*color: #696969;*/
                color: #3f3f3f;
            }

            a:visited a:link {
                color: #FFFFFF;
            }

            a:hover {
                color: #08D1D5;
                text-decoration: none;
            }

            a:active {
                color: #08F990;
            }

            p {
                margin-bottom: 10px;
                line-height: 1.6em;
                color: black;
            }

            /* PRIMARY LAYOUT ELEMENTS  ----------------------------*/
            .page {
                width: 1100px;
                background-color: #fff;
                margin: 20px auto 0px auto;
            }

            .footer {
                color: #4e5766;
                padding: 8px 0px 0px 0px;
                margin: 0px auto;
                width: 1250px;
                text-align: center;
                line-height: normal;
            }

            .floatRight {
                display: block;
                float: right;
                vertical-align: top;
                width: 220px;
            }

            .center {
                margin-left: 350px;
                margin-right: auto;
            }

            #main {
                /*to display the block level elements to the center of the page such as the <p> <div> or <h1> tags*/
                margin-left: auto;
                margin-right: auto;
                width: 960px;
            }

            /* MISC -------------------------------*/
            .clear {
                clear: both;
            }

            .title {
                display: block;
                float: left;
                text-align: left;
                width: auto;
            }

                .title a {
                    color: #778899;
                }

            /***********Page Div, TextBox,Dropdownlist,GridView Styling********/
            /************Button Styling*************/
            .addbtn {
                background: url(image/btnbg.png) no-repeat;
                padding: 2px 0px;
                color: #FFFFFF;
                font-weight: bold;
                border: 0;
                width: 56px;
                font-size: 12px;
                text-align: center;
                cursor: pointer;
            }

                .addbtn:hover {
                    background: url(image/btnbg.png) no-repeat bottom;
                }

            .plainButton {
                border: 1px solid #777777;
                /*background: #6e9e2d;*/
                background: silver;
                color: white;
                font: bold 11px 'Trebuchet MS';
                padding: 4px;
                cursor: pointer;
                -moz-border-radius: 4px;
                -webkit-border-radius: 4px;
            }

            .blueButton {
                background: #5B74A8;
                background: -moz-linear-gradient(top, #5B74A8 0%, #5B74A8 100%);
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #5B74A8), color-stop(100%, #5B74A8));
                background: -webkit-linear-gradient(top, #5B74A8 0%, #5B74A8 100%);
                background: -o-linear-gradient(top, #5B74A8 0%, #5B74A8 100%);
                background: -ms-linear-gradient(top, #5B74A8 0%, #5B74A8 100%);
                background: linear-gradient(top, #5B74A8 0%, #5B74A8 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#5B74A8', endColorstr='#5B74A8', GradientType=0);
                padding: 2px 6px;
                color: #fff;
                font-family: 'Helvetica', sans-serif;
                font-size: 11px;
                cursor: hand;
                border-radius: 0;
                -moz-border-radius: 0;
                -webkit-border-radius: 0;
                border: 1px solid #1A356E;
            }

            /************End of Button Styling*************/
            /********GridView********/
            .fortd .grid1 th.txtgreen2 {
                color: #009315;
            }

            .fortd .grid1 th.txtblue2 {
                color: #326BD2;
            }

            .backGround02 {
                background: #f9f9f9;
            }

            .grid1 {
                margin: 0;
                width: 100%;
                height: auto;
                border: 1px solid #d4d4d4 !important;
                border-bottom: 0px !important;
            }

                .grid1 th {
                    background: url(image/thbg.png) repeat-x bottom;
                    font-size: 11px;
                    font-weight: bold;
                    color: #333;
                    padding: 10px 10px;
                    border: 0px !important;
                }

                .grid1 td {
                    border: 0px !important;
                    border-bottom: 1px solid #d4d4d4 !important;
                    color: #666;
                    padding: 6px 10px 6px 10px;
                    text-align: center;
                    vertical-align: middle;
                }

                    .grid1 td a {
                        font-size: 11px;
                        font-weight: bold;
                        color: #3c94dc;
                        text-decoration: underline;
                    }

                        .grid1 td a:hover {
                            color: #333;
                        }

            /***************End of GridView**************/
            /***********Tabular Div Styling**************/
            .parentDiv {
                width: 100%;
                float: left;
            }

            .gqmn03 {
                float: left;
                width: 100%;
                padding-bottom: 3px;
                display: none;
            }

            .divDivider {
                float: left;
                width: 100%;
                padding-bottom: 3px;
                margin-bottom: 0px;
            }

            .gqmn01sub {
                float: left;
                width: 100%;
                padding-bottom: 7px;
            }

            .gqmn01_lt {
                float: left;
                font-size: 11px;
                padding: 3px 10px 0 8px;
                padding-bottom: 10px;
            }

                .gqmn01_lt label {
                    padding: 0 10px 0 0;
                }

            .gqmn01_rt {
                float: left;
            }

            .additionalInfo .gqmn01sub {
                width: 470px;
                float: left;
                padding: 0 0 0 0;
            }

            .subElements {
                width: 550px;
                float: left;
                padding: 0 0 0 0;
            }

            .gqmn03sub {
                width: 100%;
                float: left;
                padding: 0 0 0 0;
            }

            .additionalInfo .subText {
                width: 100%;
                padding-bottom: 7px;
            }

            .additionalInfo .subTextLabel {
                width: 190px;
            }

            .additionalInfo .txtbg02 {
                float: left;
            }

            .biggerTxt {
                width: 100%;
                padding-bottom: 7px;
                height: 30px;
            }

            .subText {
                float: left;
            }

                .subText label b {
                    color: #CC0000;
                    font-size: 10px;
                    font-weight: normal;
                }

            .subTextLabel {
                width: 128px;
                padding-left: 8px;
                font-weight: normal;
                float: left;
                text-align: right;
                color: #666666;
                padding: 4px 8px 0 0;
            }

            /******End Of Tabular Div Styling*************/
            /************Form Elements    TextBox Styles*************/
            .txtArea {
                border-radius: 5px;
            }

            .txtbg3 {
                border: 1px solid #c6c6c6;
                color: #333333;
                font-size: 11px;
                font-family: Arial, Helvetica, sans-serif;
                padding: 2px;
                width: 186px;
                float: left;
                border-radius: 5px;
            }

            .txtbg02 {
                width: 217px;
                height: 23px;
                border: 0;
                padding: 0 5px;
                line-height: 21px;
                border-radius: 5px;
            }

            /******End Of TextBox Styling***********/
            /***End of Page Div, TextBox,Dropdownlist,GridView Styling*****/
            /*************Width Section*************/
            .w100 {
                width: 100%;
                height: 35px;
            }

                .w100 > p {
                    width: 30%;
                    float: left;
                    text-align: center;
                }

            .w90 {
                width: 90%;
            }

            .w80 {
                width: 80%;
            }

            .w60 {
                width: 60%;
            }

            .w45 {
                width: 45%;
            }

            .w30 {
                width: 30%;
            }

            .w10 {
                width: 10%;
            }

            /*************Box Styling************/
            .h2txt {
                font-family: Arial, Helvetica, sans-serif;
                color: #333333;
                font-size: 13px;
                font-weight: bold;
            }

                .h2txt img {
                    vertical-align: middle;
                    cursor: pointer;
                    float: left;
                    margin-right: 8px;
                }

            .box1 {
                width: 1050px;
                padding: 5px 15px;
                background-color: Silver;
                color: white;
                margin-bottom: 20px;
                margin-top: 10px;
                border-radius: 5px;
            }

            .box2 {
                width: 790px;
                padding: 4px 15px;
                margin: 4px auto auto 5px;
                height: 20px;
                background-color: silver;
                /*#ED8029*/
                color: white;
                border-radius: 5px;
            }

            .transitionColor {
                -webkit-transition: background-color 5s;
                -moz-transition: background-color 5s;
                -o-transition: background-color 5s;
                -ms-transition: background-color 5s;
                transition: background-color 5s;
                background-color: #A7B526;
            }

            /**************End of Box Styling**********/
            /*************Height Section***************/
            .h100 {
                height: 100%;
            }

            .h50 {
                height: 50%;
            }

            .h30 {
                height: 30%;
            }

            /**************End of Height Section********************/
            /*******************Margin Section********************/
            .mt30 {
                margin-top: 30px;
            }

            .mt5 {
                margin-top: 5px;
            }

            .mtp5 {
                margin-top: 5%;
            }

            .w100 p {
                float: left;
                width: 30%;
                text-align: center;
            }

            .mtp10 {
                margin-top: 10%;
            }

            .mtp30 {
                margin-top: 30%;
            }

            .mlf5 {
                margin-left: 5%;
            }

            .mlf10 {
                margin-left: 10%;
            }

            .mlf25 {
                margin-left: 25%;
            }

            .left5 {
                float: left;
                margin-left: 5%;
            }

            /************End Of Margin Section****************/
            .amount {
                font-family: Arial, Helvetica, sans-serif;
                font-size: 20px;
                font-weight: normal;
                color: #006BB2;
                padding: 0px;
            }

                .amount A {
                    color: #006BB2;
                    text-decoration: none;
                }

            .PRICE A:hover {
                color: #006BB2;
                text-decoration: underline;
            }

            .amount2 {
                font-family: Arial, Helvetica, sans-serif;
                font-size: 12px;
                font-weight: normal;
                color: #1F1F99;
                padding: 0px;
                padding-right: 5px;
                vertical-align: bottom;
                margin-right: 5px;
                font-weight: bold;
            }

                .amount2 A {
                    color: #1F1F99;
                    text-decoration: none;
                }

                    .amount2 A:hover {
                        color: #1F1F99;
                        text-decoration: underline;
                    }
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
        </style>
    <style type="text/css">
        .grid td, th
        {
            padding: 4px;
        }
        
        
        
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        .style1
        {
            width: 30%;
        }
        .style4
        {
            width: 29%;
        }
        .style5
        {
            width: 28%;
        }
        .style7
        {
            width: 15%;
        }
        hr.new1 {
  border-top: 2px solid black;
}
    </style>
    <title></title>

    <script type="text/javascript">
    function fnLinkbutton1(objlinkbutton) {

        var urlpdf = document.getElementById(objlinkbutton).getAttribute("path");
        var urlpdfOr = jQuery.trim(urlpdf)
        getpdfdocument(urlpdfOr);

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
            url: "Information.aspx/Getpdf",
            data: "{'url':'" + urlpdf + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.open("../../IDoc.aspx?url=" + response.d, "_blank");
               // inlineFrameExample.src = response.d;
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

    function HideDiv() {
        var bcgDiv = document.getElementById("divBackground");
        var imgDiv = document.getElementById("divImage");
        var imgFull = document.getElementById("imgFull");
        imgDiv.style.display = "none";
        
        }
        $(document).ready(function () {

            $(".getpdfdoc").click(function (event) {


                event.preventDefault();
                var urlpdf = $(this).attr("path");

                getpdfdocument(urlpdf);
                return false;
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">  
    <div class="page">  
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">  
        </asp:ScriptManager> --%>
        <asp:Button ID="btnPrint" CssClass="btn btn-success"   runat="server"
                                                Text="Print" OnClick="btnPrint_Click"/>

        <%--<input type="button" id="btnPrint" value="Print" />--%>
       <%-- <asp:UpdatePanel ID="udpOrderDescription" runat="server">  
            <ContentTemplate>  --%>
                
              

                       <div id="printdiv" runat="server">

                <asp:Repeater ID="parentRepeater" runat="server" OnItemDataBound="parentRepeater_ItemDataBound">  
                    <HeaderTemplate>                                                                  
                                                         
                    </HeaderTemplate>  
                    <ItemTemplate> 
                         
                        
                            <div  style="border-width:2px;border-style:solid;border-color:black;padding:10px 10px 10px 10px">
                                 <div class="">
                                    <table width="90%" style="text-align: left; top: 0; z-index: 1000;" align="center">
                                                    <tr>
                                                                <td align="center" style="font-weight: bold">
                                                                   <h3> भू-समाधान</h3>
                                                                    
                                                                </td>
                                                    </tr>
                                                   <tr>
                                                            <td align="center" style="font-weight: bold">
                                                                <h4>गृह विभाग, बिहार सरकार</h4>
                                                            </td>
                                                 </tr>
                                     </table>  
                                    </div>  
                                        <div class="">
                                                <div class="" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                           <p>आवेदन विवरण</p>
                                                </div>  
                                        </div>
                                
                                        <div class="panel-body">
                                                    <br/> 
                                                     <%--आवेदन का विवरण--%>                                                            
                                                     <div  style='--bs-gutter-x: 1.5rem;--bs-gutter-y: 0;display: flex;flex-wrap: wrap;margin-top: calc(-1 * var(--bs-gutter-y));margin-right: calc(-.5 * var(--bs-gutter-x));margin-left: calc(-.5 * var(--bs-gutter-x));'>
                                                        
                                                                    <div  style='vertical-align: top!important;flex: 0 0 auto;width: 83.33333333%;'>
                                                                            &nbsp;&nbsp;<asp:Label ID="Label13"  runat="server" Text="आवेदन संख्या : "></asp:Label>
                                                                                &nbsp;
                                                                            <asp:Label ID="lblAppID" runat="server" Font-Bold="True" Text='<%#Eval("ApplicationNo") %>'></asp:Label>
                                                                       </div>
                                                                      
                                                                
                                                                     <div style='vertical-align: top!important;flex: 0 0 auto;width: 16.66666667%;' >                                                                        
                                                                          <asp:Label ID="Label27" runat="server" Text=" आवेदन तिथि : "></asp:Label>
                                                                                &nbsp;                                                                        
                                                                            <asp:Label ID="lblAppDate" runat="server" Font-Bold="True" Text='<%#Eval("AavedanKiTithi") %>'></asp:Label>
                                                                            <asp:HiddenField ID="lbla_id" runat="server" value='<%#Eval("a_id") %>'></asp:HiddenField>
                                                                 
                                                                       </div>
                                                                 </div> 
                                                       <br/>
                                                      <%--वादी का विवरण--%>   
                                                     <div class="row">
                                                            <div class="col-md-12">
                                                                    <div class="panel panel-default">
                                                                            <div class="panel-heading">
                                                                                <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                        <p>वादी का विवरण</p>
                                                                              </div>  
                                                                            </div>
                                                                            <div class="panel-body">
                                                                                <br/>
                                                                                   <div class="row">
                                                                                        <div class="col-md-12">
                                                                                                 <asp:GridView runat="server" Width="100%" ID="gvWadi"
                                                                                                      AutoGenerateColumns="false" 
                                                                                  CssClass="table-responsive  CSSTableGeneratorGrid fontsize" 
                                                                                                     EmptyDataText="No Record Found!"> 
                                                                                    
                                                                                    <Columns>
                                                                                           
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
                                                                                                <asp:Label ID="lblGender"  runat="server" Text='<%# (Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ) ? "Female" : "Male"  %>'></asp:Label>
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
                                                                                                <asp:Label ID="lblDeppratinidhi"  runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_dept")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                   
                                                                                          <asp:TemplateField HeaderText="संस्था का प्रतिनिधि" ItemStyle-Width="80">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblOrgpratinidhi"  runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
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
                                                                                    </Columns>
                                                                                </asp:GridView>   
                                                                                        </div>

                                                                                   </div>
                                                                            </div>
                                                                  </div>   
                                                            </div>
                                                     </div>
                                                     <br />
                                                      <%--भूमि विवाद का विवरण--%>   
                                                     <div class="row">
                                                            <div class="col-md-12">
                                                                     <div class="panel panel-default">
                                                          <div class="panel-heading">
                                                               <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                        <p>भूमि विवाद का विवरण</p>
                                                                              </div>                                                              
                                                          </div>
                                                          <div class="panel-body">
                                                                <br/> 
                                                              <div class="row">
                                                                  <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                      जिला
                                                                  </div>
                                                                  <div class="col-md-3" style="font-size:12px">
                                                                        <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                                                                  </div>
                                                                  <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                      अनुमंडल
                                                                  </div>
                                                                  <div class="col-md-3"  style="font-size:12px">
                                                                       <asp:Label ID="lblSubdivision" runat="server" Text=""></asp:Label>
                                                                  </div>
                                                              </div> 
                                                              <div class="row">
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            अंचल
                                                                      </div>
                                                                      <div class="col-md-3" style="font-size:12px">
                                                                          <asp:Label ID="lblBlock" runat="server"></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            थाना
                                                                      </div>
                                                                      <div class="col-md-3"  style="font-size:12px">
                                                                          <asp:Label ID="lblPolice_Station" runat="server"></asp:Label>
                                                                      </div>
                                                                 </div>  
                                                              <div class="row">
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            क्षेत्र का प्रकार
                                                                      </div>
                                                                      <div class="col-md-3" style="font-size:12px">
                                                                           <asp:Label ID="lblAreaType" runat="server"></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                           <asp:Label ID="lblVillage" runat="server" ></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3"  style="font-size:12px">
                                                                           <asp:Label ID="lblPanchayatName" runat="server"></asp:Label>
                                                                      </div>
                                                              </div> 
                                                              <div class="row">
                                                                     <div class="col-md-3" id="div_Vadi_Svarajaya_Label" runat="server"   style="font-weight: bold;font-size:14px">
                                                                          राजस्व ग्राम
                                                                      </div>
                                                                      <div class="col-md-3" id="div_Vadi_Svarajaya" runat="server" style="font-size:12px">
                                                                             <asp:Label ID="lblVILLNAME" runat="server"></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            वार्ड 
                                                                      </div>
                                                                      <div class="col-md-3" id="div_Vadi_Ward" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblWARDNAME" runat="server"></asp:Label>
                                                                      </div>
                                                                       <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                           विवाद का अद्यतन कारक
                                                                      </div>
                                                                      <div class="col-md-3" id="div_vadi_Vivad_Ka_Vighatan" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblvadi_Vivad_Ka_Vighatan" runat="server"></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            राजस्व थाना संख्या
                                                                      </div>
                                                                      <div class="col-md-3" id="div_vadi_rajashv_sankhaya" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblvadi_rajashv_sankhaya" runat="server"></asp:Label>
                                                                      </div>
                                                                     <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                           भूमि का प्रकार
                                                                      </div>
                                                                      <div class="col-md-3" id="div_Vadi_BhumiKaPrakar" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblVadi_BhumiKaPrakar" runat="server"></asp:Label>
                                                                      </div>
                                                                        <div class="col-md-3" id="div_vadi_sarkari_bhumi_ka_prakar_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                          सरकारी भूमि का प्रकार
                                                                      </div>
                                                                     <div class="col-md-3" id="div_Preview_vadi_sarkari_bhumi_ka_prakar" runat="server" style="font-size:12px">
                                                                          <asp:Label ID="lblvadi_sarkari_bhumi_ka_prakar_Label" runat="server"></asp:Label>
                                                                     </div>  
                                                                     <div class="col-md-3" id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                          सरकारी भूमि का प्रकार (अगर अन्य है)
                                                                      </div>
                                                                     <div class="col-md-3" id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server" style="font-size:12px">
                                                                          <asp:Label ID="lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server"></asp:Label>
                                                                     </div> 
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            भूमि विवाद का प्रकार
                                                                      </div>
                                                                      <div class="col-md-3" id="div_BhumiKa_VivadPrakar" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblBhumiKa_VivadPrakar" runat="server"></asp:Label>
                                                                       </div>
                                                                     <div class="col-md-3" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                             <asp:Label ID="Label1" runat="server" Text="भूमि विवाद का प्रकार (अगर अन्य है) :"></asp:Label>
                                                                      </div>
                                                                    <div class="col-md-3" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya" runat="server" style="font-size:12px">
                                                                         <asp:Label ID="lblvadi_Bhumivivad_Prakar_Anaya" runat="server"></asp:Label>
                                                                    </div>                                                                     
                                                                </div>
                                                              <div class="row">
                                                                        <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी 
                                                                      </div>
                                                                      <div class="col-md-9" id="divVadiKabhumiVivaran" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblVadiKabhumiVivaran" runat="server"></asp:Label>
                                                                      </div>                                                                   
                                                                </div>
                                                              <div class="row">
                                                                     <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                           प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी 
                                                                      </div>
                                                                      <div class="col-md-9" id="divPrativadiKabhumiVivaran" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblPrativadiKabhumiVivaran" runat="server"></asp:Label>
                                                                       </div>
                                                                </div>
                                                              <div class="row" id="div37" runat="server" Visible="false">
                                                                        <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            वादी द्वारा प्रस्तुत आवेदन
                                                                      </div>
                                                                      <div class="col-md-9" id="div29" runat="server" style="font-size:12px">
                                                                            <asp:ImageButton ID="lnkAppDoc" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                      </div>                                                                   
                                                                </div>
                                                              <div class="row" id="div38" runat="server" Visible="false">
                                                                     <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                           प्रतिवादी द्वारा प्रस्तुत आवेदन 
                                                                      </div>
                                                                      <div class="col-md-9" id="div30" runat="server" style="font-size:12px">
                                                                             <asp:ImageButton ID="lnkPrativadiDoc" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                       </div>
                                                                </div>
                                                          </div>                                                                  
                                                    </div>
                                                   </div>
                                           </div>
                                                    <br/> 
                                                     <%--प्रतिवादी का विवरण--%>   
                                                     <div class="row">
                                                             <div class="col-md-12">
                                                                 <div class="panel panel-default">
                                                                          <div class="panel-heading">
                                                                               <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                         <p>प्रतिवादी का विवरण</p>
                                                                               </div>
                                                                          </div>
                                                                          <div class="panel-body">
                                                                                <br/> 
                                                                                 <div class="row">
                                                                                       <div class="col-md-12" id="divPratiwadi" runat="server" style="width:100%;border-collapse:collapse" >
                                                                                                  <asp:GridView runat="server" Width="100%" ID="pratiWadi_grid" AutoGenerateColumns="false" 
                                                                                                    CssClass="table-responsive CSSTableGeneratorGrid" EmptyDataText="No Record Found!">
                                                                                   <Columns>                                                                                       
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
                                                                                                    <asp:Label ID="lblOrgName"  runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ) ? Eval("pratiVadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम" ItemStyle-Width="100">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPadName" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y") ? Eval("pratiVadi_org_pad_name") : Eval("pratiVadi_dept_pad_name")   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                                                                                             
                                                                                 </Columns>
                                                                        </asp:GridView>
                                                                                       </div>
                                                                                 </div>                                                                 
                                                                          </div>                                                                  
                                                                </div>
                                               </div>
                                                     </div>
                                                        <br/> 
                                                     <%--प्रतिवादी का अन्य विवरण--%> 
                                                     <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="panel panel-default">
                                                                     <div class="panel-heading">
                                                                           <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                     <p>अन्य विवरण</p>
                                                                           </div>
                                                                      </div>
                                                                    <div class="panel-body">
                                                                           <br/> 
                                                                            <div class="row">                                                                             
                                                                                 <div class="col-md-3" id="divprativadi_ka_suchit_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       प्रतिवादी को सूचित किया गया है या नहीं ?
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_suchit" runat="server" style="font-size:12px">
                                                                                        <asp:Label ID="lblprativadi_ka_suchit" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Karan_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       कारण स्पष्ट करें
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Karan" runat="server" style="font-size:12px">
                                                                                        <asp:Label ID="lblprativadi_ka_Karan" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                                <div class="col-md-3" id="divprativadi_ka_madham_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       माध्यम
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_madham" runat="server" style="font-size:12px">
                                                                                        <asp:Label ID="lblprativadi_ka_madham" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Suchna_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Suchna" runat="server" style="font-size:12px">
                                                                                     <asp:Label ID="lblprativadi_ka_Suchna" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Upashtith_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       प्रतिवादी उपस्थित हुआ है या नहीं ? 
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Upashtith" runat="server" style="font-size:12px">
                                                                                     <asp:Label ID="lblprativadi_ka_Upashtith" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                            </div>
                                                                    </div>  
                                                               </div>              
                                                    </div>
                                                     </div> 
                                                        <br/>
                                                     <%--  भूमि का खाता-खेसरा का विवरण--%>
                                                        <div class="row">
                                                           <div class="col-md-12">
                                                                <div class="panel panel-default">
                                                                      <div class="panel-heading">
                                                                            <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                       <p>भूमि का खाता-खेसरा का विवरण</p>
                                                                            </div>  
                                                                      </div>
                                                                      <div class="panel-body">
                                                                            <br/> 
                                                                             <div class="row">
                                                                                   <div class="col-md-12" id="divbhumikhata_shekher_ka_vivaran" runat="server" style="width:100%;border-collapse:collapse" >
                                                                                             <asp:GridView runat="server" Width="100%" ID="grd_bhumivivad" 
                                                                                                 AutoGenerateColumns="false" EmptyDataText="No Record Found!"> 
                                                                      
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
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                   </div>
                                                                             </div>                                                                 
                                                                      </div>                                                                  
                                                                </div>
                                                           </div>
                                                       </div>
                                                          <br/>
                                                     <%-- वादी द्वारा प्रस्तुत साक्ष्य का विवरण--%>
                                                          <br/>
                                                     <div class="row">
                                                           <div class="col-md-12">
                                                                <div class="panel panel-default">
                                                                      <div class="panel-heading">
                                                                            <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                       <p>वादी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
                                                                            </div>  
                                                                      </div>
                                                                      <div class="panel-body">
                                                                            <br/> 
                                                                             <div class="row">
                                                                                   <div class="col-md-12">
                                                                                               <asp:GridView runat="server" Width="100%" ID="gdVadiEvidence"  AutoGenerateColumns="false"
                                                                                                    CssClass="table-responsive CSSTableGeneratorGrid" EmptyDataText="No Record Found!">
                                                                                                   
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
                                                                                                <asp:ImageButton ID="Image1"  path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" CommandArgument='<%# Container.DataItemIndex %>' CommandName="View" CssClass="getpdfdoc"/>
                                  
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>                                                                                                                      
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                  </div>  
                                                                             </div>                                                                 
                                                                      </div>                                                                  
                                                                </div>
                                                           </div>
                                                       </div>
                                                    <%-- प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण--%>
                                                            <br/>
                                                       <div class="row">
                                                           <div class="col-md-12">
                                                                <div class="panel panel-default">
                                                                      <div class="panel-heading">
                                                                            <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                       <p>प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
                                                                            </div>  
                                                                      </div>
                                                                      <div class="panel-body">
                                                                            <br/> 
                                                                             <div class="row">
                                                                                   <div class="col-md-12">
                                                                                      <asp:GridView runat="server" Width="100%" ID="gdPrativadiEvidence"  AutoGenerateColumns="false"
                                                                                          CssClass="table-responsive CSSTableGeneratorGrid" EmptyDataText="No Record Found!">                                                                                 
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
                                                                                                    <asp:ImageButton ID="Image1"  path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" CssClass="getpdfdoc"/>                                 
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:TemplateField>                                                                                                                                                                                                                                                                                                                  
                                                                                            </Columns>
                                                                                        </asp:GridView>
                                                                                  </div>  
                                                                             </div>                                                                 
                                                                      </div>                                                                  
                                                                </div>
                                                           </div>
                                                       </div>

                                                    <%-- पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी --%>
                                                        <br/>
                                                       <div class="row">
                                                        <div class="col-md-12">
                                                              <div class="panel panel-default">
                                                                      <div class="panel-heading">
                                                                           <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                     <p>राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
                                                                           </div>
                                                                      </div>
                                                                      <div class="panel-body">
                                                                                 <br/> 
                                                                                   <div class="row">                                                                             
                                                                                             <div class="col-md-3" id="div2" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                   पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divPoliceAdhikari" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblPoliceAdhikari" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>  
                                                                                        <br/>
                                                                                   <div class="row" id="divHalkaKarmchari" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div4" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                   हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divHalkaKarmchariValue" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblHalkaKarmchariValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                                                                                     <br/>
                                                                                   <div class="row" id="divVivaditBhukand" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div6" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  विवादित भू-खंड की मापी 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divVivaditBhukandValue" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblVivaditBhukandValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>   
                                                                                     <br/>  
                                                                                   <div class="row" id="divMapi" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div8" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  मापी ? 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divMapiValue" runat="server" style="font-size:12px">
                                                                                                  <asp:Label ID="lblMapiValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                                                                                    <br/>
                                                                                   <div class="row" id="divVivaditBhukandKaMapi" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div10" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  विवादित भू-खंड की मापी नहीं होने का कारण 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divVivaditBhukandKaMapiValue" runat="server" style="font-size:12px">
                                                                                                 <asp:Label ID="lblVivaditBhukandKaMapiValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>    
                                                                                     <br/>
                                                                                   <div class="row" id="MapiKeNirdharnKiThithi" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div5" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  मापी के लिए निर्धारित तिथि 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divMapiKeNirdharnKiThithiValue" runat="server" style="font-size:12px">
                                                                                                 <asp:Label ID="lblMapiKeNirdharnKiThithiValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                                                                           <div class="row" id="Div20" runat="server" Visible="false">                                                                             
                                                                                             <div class="col-md-3" id="div21" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                                                                                             </div>
                                                                                             <div class="col-md-9" id="div22" runat="server" style="font-size:12px">
                                                                                                <asp:ImageButton ID="lnkpulis_padadhikari_Patr_file" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>   
                                                                          <div class="row" id="Div23" runat="server" Visible="false">                                                                             
                                                                                             <div class="col-md-3" id="div24" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज
                                                                                             </div>
                                                                                             <div class="col-md-9" id="div25" runat="server" style="font-size:12px">
                                                                                                 <asp:ImageButton ID="lnkfile_halkakarmchari_praptr" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>   
                                                                          <div class="row" id="Div26" runat="server" Visible="false">                                                                             
                                                                                             <div class="col-md-3" id="div27" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  विवादित भू-खंड की मापी का प्रतिवेदन 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="div28" runat="server" style="font-size:12px">
                                                                                                <asp:ImageButton ID="lnkfile_bhukand_prativedan" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>  
                                                                      </div>                                                                  
                                                             </div>
                                                        </div>
                                                 </div>

                                                      <%-- भूमि विवाद सें संबंधित घटना/ वारदात का विवरण --%>
                                                        <br/>

                                                        <div class="row">
                                                                   <div class="col-md-12">
                                                                        <div class="panel panel-default">
                                                                              <div class="panel-heading">
                                                                                   <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                             <p>भूमि विवाद सें संबंधित घटना/ वारदात का विवरण</p>
                                                                                   </div>
                                                                              </div>
                                                                              <div class="panel-body">
                                                                                    <br/> 
                                                                                     <div class="row">                                                                             
                                                                                                     <div class="col-md-3" id="div3" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                           प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ? :
                                                                                                     </div>
                                                                                                     <div class="col-md-3" id="div_Prathamik" runat="server" style="font-size:12px">
                                                                                                         <asp:Label ID="lblPrathamik" runat="server" Text=""></asp:Label>
                                                                                                     </div>                                                                                                                                                                                                                                            
                                                                                           </div> 
                                                                                            <br/>
                                                                                       <div class="row">
                                                                                                 <div class="col-md-12" >
                                                                                                                <asp:GridView runat="server" Width="100%" ID="grdbhumivivad"  AutoGenerateColumns="false"
                                                                                                    CssClass="table-responsive CSSTableGeneratorGrid" EmptyDataText="No Record Found!">                                                                                                  
                                                                                                    
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
                                                                                                                        </Columns>
                                                                                                                </asp:GridView>
                                                                                                 </div>
                                                                                      </div>                                                                    
                                                                              </div>                                                                  
                                                                        </div>
                                                                   </div>
                                                               </div>
                                                      <%-- न्यायालय में प्रक्रियाधीन वाद का विवरण--%>
                                                        <br/>

                                                       <div class="row">
                                                                   <div class="col-md-12">
                                                                        <div class="panel panel-default">
                                                                              <div class="panel-heading">
                                                                                       <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                             <p>न्यायालय में प्रक्रियाधीन वाद का विवरण</p>
                                                                                   </div>
                                                                              </div>
                                                                              <div class="panel-body">
                                                                                    <br/> 
                                                                                     <div class="row">                                                                             
                                                                                                  <div class="col-md-3" id="div11" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                           प्रक्रियाधीन वाद का विवरण उपलब्ध है ? :
                                                                                                   </div>
                                                                                                   <div class="col-md-3" id="divPrakiriyaVad" runat="server" style="font-size:12px">
                                                                                                         <asp:Label ID="lblPrakiriyaVad" runat="server" Text=""></asp:Label>
                                                                                                   </div>                                                                                                                                                                                                                                            
                                                                                           </div> 
                                                                                            <br/>
                                                                                       <div class="row">
                                                                                                 <div class="col-md-12"  >
                                                                                                                <asp:GridView runat="server" Width="100%" ID="grdnyayalay_vivran"
                                                                                                                                      AutoGenerateColumns="false" 
                                                                                                                  CssClass="table-responsive  CSSTableGeneratorGrid fontsize" 
                                                                                                                                     EmptyDataText="No Record Found!">                                                                                                              
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
                                               
                                                                                                                    </Columns>
                                                                                                                </asp:GridView>
                                                                                                 </div>
                                                                                      </div>                                                                    
                                                                              </div>                                                                  
                                                                        </div>
                                                                   </div>
                                                              </div>


                                             <%--अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण--%>
                                                        <br/>
                                                         <div class="row">
                                                               <div class="col-md-12">
                                                                    <div class="panel panel-default">
                                                                          <div class="panel-heading">
                                                                                  <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                         <p>अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण</p>
                                                                               </div>
                                                                              <br />
                                                                          </div>
                                                                         <div class="panel-body">
                                                                         <asp:Repeater ID="childRepeater" runat="server" OnItemDataBound="childRepeater_ItemDataBound">  
                                                                        <HeaderTemplate> </HeaderTemplate>  
                                                                        <ItemTemplate> 
                                                                        <asp:HiddenField ID="lbla_idc" runat="server" value='<%#Eval("id") %>'></asp:HiddenField>
                                                                         
                                                                               
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div7" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       विवाद की संवेदनशीलता
                                                                                          </div>
                                                                                         <div class="col-md-3"  style="font-size:12px">
                                                                                                    <asp:Label id="lblVivaadKiSanvedanasheelata" runat="server" Text=""></asp:Label>
                                                                                         </div>                                                                                  
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                     <div class="col-md-3" id="div12" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       बैठक की तिथि
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label id="lblBaithakKiTithi" runat="server" Text=""></asp:Label>
                                                                                         </div>
                                                                               </div>                                                                                
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div13" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       क्या वादी उपस्थित है ? 
                                                                                          </div>
                                                                                         <div class="col-md-3" style="font-size:12px">
                                                                                              <asp:Label id="lblkyaVaadeeUpasthitHai" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                <div>
                                                                                <div class="row">
                                                                                           <div class="col-md-3" id="div15" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                      क्या प्रतिवादी उपस्थित है ?
                                                                                          </div>
                                                                                         <div class="col-md-3"  style="font-size:12px">
                                                                                              <asp:Label id="lblKyaPrativaadeeUpasthitHai" runat="server" Text=""></asp:Label>
                                                                                         </div>
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                          <div class="col-md-3" id="div9" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                    बैठक का निष्कर्ष 
                                                                                           </div>
                                                                                          <div class="col-md-6" style="font-size:12px">
                                                                                               <asp:Label id="lblBaithakKaNishkarsh" runat="server"  Text=""></asp:Label>
                                                                                          </div>
                                                                                <div>
                                                                                 <div class="row" id="divtithi" runat="server" >
                                                                                          <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                                                    <asp:Label id="lbltithi" runat="server" Text=""></asp:Label>
                                                                                           </div>
                                                                                          <div class="col-md-3"  style="font-size:12px">
                                                                                               <asp:Label id="lbltithivalue" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div>
                                                                                  <div class="row" id="divAsveekrtiKaKaaranLabel" runat="server">
                                                                                          <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                    अस्वीकृति का कारण
                                                                                           </div>
                                                                                          <div class="col-md-9" id="divAsveekrtiKaKaaran" runat="server" style="font-size:12px">
                                                                                               <asp:Label id="lblAsveekrtiKaKaaran" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div> 
                                                                                    <div class="row" id="divvadikavarsh" runat="server">
                                                                                          <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                     वादी की वाद संख्या / वर्ष
                                                                                           </div>
                                                                                          <div class="col-md-9"  style="font-size:12px">
                                                                                               <asp:Label id="lblvadikavarsh" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div> 

                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div14" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       बैठक में लिया गया निर्णय
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label  id="lblBaithakMeinLiyaGayaNirnay" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                </div>
                                                                                    <div class="row">
                                                                                           <div class="col-md-3" id="div17" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       अंचलाधिकारी का मंतव्य 
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label id="lblAnchalaadhikaareeKaMantavy" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div16" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       थानाध्यक्ष का मंतव्य 
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:Label id="lblThaanaadhyakshKaMantavy" runat="server" Text=""></asp:Label>
                                                                                         </div>                                                                        
                                                                                 </div> 
                                                                                   <div class="row" id="div1" runat="server" Visible="false">
                                                                                         <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                       थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन 
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:ImageButton ID="lnkLandDoc" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             
                                                                                         </div>                                                                        
                                                                                 </div>
                                                                                    <div class="row" id="div18" runat="server" Visible="false">
                                                                                         <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                       अंचलाधिकारी का मंतव्य पत्र
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:ImageButton ID="lnkCircleOfficer_letterOfIntent" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             
                                                                                         </div>                                                                        
                                                                                 </div>  
                                                                                    <div class="row" id="div19" runat="server" Visible="false">
                                                                                         <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                       थानाध्यक्ष का मंतव्य
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:ImageButton ID="lnkPoliceOfficer_letterOfIntent" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             
                                                                                         </div>                                                                        
                                                                                 </div>  

                                                                          </div>                                                                  
                                                                    </div>
                                                               </div>
                                                           </div>
                                                                              <hr class="new1">
                                                                         
                        </ItemTemplate>
                        </asp:Repeater>
                                                                             </div>
                                                                      </div>
                                                               </div>
                                                        </div>
                                               <br/>
                                                         <div class="row">
                                                               <div class="col-md-12">
                                                                    <div class="panel panel-default">
                                                                          <div class="panel-heading">
                                                                                  <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                         <p> मंतव्य की विवरणी</p>
                                                                               </div>
                                                                          </div>
                                                                          <div class="panel-body">
                                                                                <br/> 
                                                                                 <div class="row">
                    <div class="col-md-12">
                      
                            <asp:GridView ID="grdRemarks" runat="server"
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                CssClass="w3-table w3-striped" AllowPaging="false"  Style="width: 100%;"                            
                                ShowHeaderWhenEmpty="True"  EmptyDataRowStyle-HorizontalAlign="Left" EmptyDataRowStyle-VerticalAlign="Middle"
                                 EmptyDataText="No Record Found" Visible="true">
                              
                                 <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                       <%--<asp:TemplateField HeaderText="आवेदन संख्या" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%#Eval("ApplicationNo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
                                      <asp:TemplateField HeaderText="मंतव्य" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <%#Eval("Remarks")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                   
                                     <asp:TemplateField HeaderText="मंतव्य विवरण द्वारा" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("UserName")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="दस्तावेज देखें" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                          
                                            <asp:ImageButton ID="Image1" Visible='<%# CheckImage(Eval("Remarks_file"))%>' path='<%#Eval("Remarks_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" CommandArgument='<%# Container.DataItemIndex %>' CommandName="View"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                  
                                  
                                </Columns>

                                    

                                    
                               
                            </asp:GridView>
                       
                    </div>
                </div>
                                                                         </div>
                                                                      </div>
                                                               </div>
                                                        </div>

                                                  <%--अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण--%>
                                                      <%--  <br/>
                                                         <div class="row">
                                                               <div class="col-md-12">
                                                                    <div class="panel panel-default">
                                                                          <div class="panel-heading">
                                                                                  <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                         <p>अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण</p>
                                                                               </div>
                                                                          </div>
                                                                          <div class="panel-body">
                                                                                <br/> 
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div7" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       विवाद की संवेदनशीलता
                                                                                          </div>
                                                                                         <div class="col-md-3"  style="font-size:12px">
                                                                                                    <asp:Label id="lblVivaadKiSanvedanasheelata" runat="server" Text=""></asp:Label>
                                                                                         </div>                                                                                  
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                     <div class="col-md-3" id="div12" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       बैठक की तिथि
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label id="lblBaithakKiTithi" runat="server" Text=""></asp:Label>
                                                                                         </div>
                                                                               </div>                                                                                
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div13" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       क्या वादी उपस्थित है ? 
                                                                                          </div>
                                                                                         <div class="col-md-3" style="font-size:12px">
                                                                                              <asp:Label id="lblkyaVaadeeUpasthitHai" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                <div>
                                                                                <div class="row">
                                                                                           <div class="col-md-3" id="div15" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                      क्या प्रतिवादी उपस्थित है ?
                                                                                          </div>
                                                                                         <div class="col-md-3"  style="font-size:12px">
                                                                                              <asp:Label id="lblKyaPrativaadeeUpasthitHai" runat="server" Text=""></asp:Label>
                                                                                         </div>
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                          <div class="col-md-3" id="div9" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                    बैठक का निष्कर्ष 
                                                                                           </div>
                                                                                          <div class="col-md-6" style="font-size:12px">
                                                                                               <asp:Label id="lblBaithakKaNishkarsh" runat="server"  Text=""></asp:Label>
                                                                                          </div>
                                                                                <div>
                                                                                 <div class="row" id="divtithi" runat="server" >
                                                                                          <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                                                    <asp:Label id="lbltithi" runat="server" Text=""></asp:Label>
                                                                                           </div>
                                                                                          <div class="col-md-3"  style="font-size:12px">
                                                                                               <asp:Label id="lbltithivalue" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div>
                                                                                  <div class="row" id="divAsveekrtiKaKaaranLabel" runat="server">
                                                                                          <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                    अस्वीकृति का कारण
                                                                                           </div>
                                                                                          <div class="col-md-9" id="divAsveekrtiKaKaaran" runat="server" style="font-size:12px">
                                                                                               <asp:Label id="lblAsveekrtiKaKaaran" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div> 
                                                                                    <div class="row" id="divvadikavarsh" runat="server">
                                                                                          <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                     वादी की वाद संख्या / वर्ष
                                                                                           </div>
                                                                                          <div class="col-md-9"  style="font-size:12px">
                                                                                               <asp:Label id="lblvadikavarsh" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div> 


                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div14" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       बैठक में लिया गया निर्णय
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label  id="lblBaithakMeinLiyaGayaNirnay" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                </div>
                                                                                    <div class="row">
                                                                                           <div class="col-md-3" id="div17" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       अंचलाधिकारी का मंतव्य 
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label id="lblAnchalaadhikaareeKaMantavy" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div16" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       थानाध्यक्ष का मंतव्य 
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:Label id="lblThaanaadhyakshKaMantavy" runat="server" Text=""></asp:Label>
                                                                                         </div>                                                                        
                                                                                 </div>  
                                                                                    

                                                                                    <div class="row" id="div1" runat="server" Visible="false">
                                                                                         <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                       थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन 
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:ImageButton ID="lnkLandDoc" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             
                                                                                         </div>                                                                        
                                                                                 </div>
                                                                                    <div class="row" id="div18" runat="server" Visible="false">
                                                                                         <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                       अंचलाधिकारी का मंतव्य पत्र
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:ImageButton ID="lnkCircleOfficer_letterOfIntent" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             
                                                                                         </div>                                                                        
                                                                                 </div>  
                                                                                    <div class="row" id="div19" runat="server" Visible="false">
                                                                                         <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                       थानाध्यक्ष का मंतव्य
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:ImageButton ID="lnkPoliceOfficer_letterOfIntent" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             
                                                                                         </div>                                                                        
                                                                                 </div>  

                                                                          </div>                                                                  
                                                                    </div>
                                                               </div>
                                                           </div>
                                                                         </div>
                                                                      </div>
                                                               </div>
                                                        </div>--%>
                                                                                                        
                          </div>
                                </div>
                    </ItemTemplate>  
                    <%--<FooterTemplate>  
                        <div class="divDivider">  
                            <asp:Button ID="btnFirst" runat="server" Text="<<" CssClass="left5" OnClick="CommonButton_Click" />  
                            <asp:Button ID="btnPrevious" runat="server" Text="<" CssClass="left5" OnClick="CommonButton_Click" />  
                            <asp:Button ID="btnNext" runat="server" Text=">" CssClass="left5" OnClick="CommonButton_Click" />  
                            <asp:Button ID="btnLast" runat="server" Text=">>" CssClass="left5" OnClick="CommonButton_Click" />  
                        </div>  
                        </div>  
                    </FooterTemplate> --%> 
                </asp:Repeater>
                           </div> 
                           
          <%--  </ContentTemplate>  
        </asp:UpdatePanel> --%> 
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

    </form>
</body>
     
    
</html>
