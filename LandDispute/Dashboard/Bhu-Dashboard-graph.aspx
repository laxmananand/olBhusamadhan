<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bhu-Dashboard-graph.aspx.cs" Inherits="Bhu_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bhu-Samadhan Dashboard</title>
     <link href="../../vendors/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script src="Vendors/bootstrap.min.js"></script>
  <%-- <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>--%>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.6.3/jquery.min.js"></script>--%>
     <script src="JavaScript.js"></script>
    <script src="vendors/bootstrap/js/bootstrap.min.js"></script>
    <script src="vendors/jQuery/jquery.min.js"></script>
    <script src="vendors/canvasjs.min.js"></script>
   

    <script src="highmaps.js"></script>
    <script src="data.js"></script>
    <script src="drilldown.js"></script>

    <script src="exporting.js"></script>
    <script src="offline-exporting.js"></script>
    <script src="accessibility.js"></script>
  <%--  <script src="../../vendors/loader.js"></script>--%>
    <style type="text/css">
        .sidebar {
            margin: 0;
            padding: 0;
            width: 200px;
            background-color: #f1f1f1;
            position: fixed;
            height: 100%;
            overflow: auto;
        }

            .sidebar a {
                display: block;
                color: black;
                padding: 16px;
                text-decoration: none;
            }

                .sidebar a.active {
                    background-color: #d9534f;
                    color: white;
                }

                .sidebar a:hover:not(.active) {
                    background-color: #555;
                    color: white;
                }

        div.content {
            margin-left: 200px;
            padding: 1px 16px;
            height: 900px;
        }


        activedata {
                    background-color: red;
                    color: white;
                }


        @media screen and (max-width: 700px) {
            .sidebar {
                width: 100%;
                height: auto;
                position: relative;
            }

                .sidebar a {
                    float: left;
                }

            div.content {
                margin-left: 0;
            }
        }

        @media screen and (max-width: 400px) {
            .sidebar a {
                text-align: center;
                float: none;
            }
        }
    </style>
    <style type="text/css">
        .effect:hover {
            color: red;
        }
    </style>

    <style type="text/css">
        .responsive-map {
            overflow: hidden;
            padding-bottom: 56.25%;
            position: relative;
            height: 0;
        }

            .responsive-map iframe {
                left: 0;
                top: 0;
                height: 100%;
                width: 100%;
                position: absolute;
            }
    </style>

    <style type="text/css">
        .quiz-chartTip {
            padding: 5px 10px;
            border: 1px solid rgba(0,0,0,.1);
            border-radius: 4px;
            background-color: rgba(255,255,255,.9);
            box-shadow: 3px 3px 10px rgba(0,0,0,.1);
            position: absolute;
            z-index: 50;
            max-width: 250px;
        }

        .quiz-graph {
            padding: 10px;
            height: 200px;
            width: 100%;
        }

            .quiz-graph .x-labels {
                text-anchor: middle;
            }

            .quiz-graph .y-labels {
                text-anchor: end;
            }

            .quiz-graph .quiz-graph-grid {
                stroke: #ccc;
                stroke-dasharray: 0;
                stroke-width: 1;
            }

        .label-title {
            text-anchor: middle;
            text-transform: uppercase;
            font-size: 12px;
            fill: gray;
        }

        .quiz-graph-dot, .quiz-graph-start-dot {
            fill: rgba(0,112,210,1);
            stroke-width: 2;
            stroke: white;
        }
    </style>
    <style type="text/css">
        div.scroll {
            height: 590px;
            overflow-x: auto;
            overflow-y: auto;
            text-align: justify;
        }

        .scroll::-webkit-scrollbar {
            display: none;
        }


        .scroll {
            -ms-overflow-style: none;
            scrollbar-width: none;
        }
    </style>





    <style>
        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: red;
            color: white;
            text-align: center;
        }
    </style>

    <style>
        .pop-out-card {
            float: left;
            display: flex;
            flex-direction: column;
            border: solid 1px #bbb;
            transition: all .3s;
            font: normal 16px calibri;
        }

            .pop-out-card:hover {
                box-shadow: 2px 12px 15px #999;
            }

            .pop-out-card.with-transform:hover {
                transform: translateY(-2px);
            }
            .greycolor{
                color: #ccc;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
       <%-- <img src="../../images/Bhu-Samadhan_Logo.png" />--%>
        <div class="sidebar">          
            <center id="logo"><img src="../../../../images/Bhu-Samadhan_Logo.png" class="img-responsive" style="width: 113px; height: auto" /></center>
            
            <%--<center id="logo"><img src="../../images/bihar_govt.png" class="img-responsive" style="width: 100px; height: auto" /></center>--%>
            <br />
            <a href="#" class="active" data-id="1"  data-rt="कुल आवेदन" onclick="reportLinkClick(event,this,'कुल आवेदन',1,'कुल आवेदन')">कुल आवेदन</a>
            <a href="#" class="inactiveLink" data-id="2" data-rt="सवेदनशीलता" onclick="reportLinkClick(event,this,'Sensitivity',2,'सवेदनशीलता')">सवेदनशीलता</a>
            <a href="#" class="inactiveLink" data-id="3" data-rt="बैठक का निष्कर्ष" onclick="reportLinkClick(event,this,'action',3,'बैठक का निष्कर्ष')">बैठक का निष्कर्ष</a>           
            <a href="#" class="inactiveLink" data-id="4" data-rt="भूमि विवाद का प्रकार" onclick="reportLinkClick(event,this,'landdispute',4,'भूमि विवाद का प्रकार')">भूमि विवाद का प्रकार</a>
            <a href="#" class="inactiveLink" data-id="5" data-rt="जमीन की क़िस्म का विवरण" onclick="reportLinkClick(event,this,'Landtype',5,'जमीन की क़िस्म का विवरण')">जमीन की क़िस्म का विवरण</a>
            <a href="#" class="inactiveLink" data-id="6" data-rt="साक्ष्य का प्रकार (वादी)" onclick="reportLinkClick(event,this,'EvidenceVadi',6,'साक्ष्य का प्रकार (वादी)')">साक्ष्य का प्रकार (वादी)</a>
            <a href="#" class="inactiveLink" data-id="7" data-rt="साक्ष्य का प्रकार (प्रतिवादी)" onclick="reportLinkClick(event,this,'EvidencePrativadi',7,'साक्ष्य का प्रकार (प्रतिवादी)')">साक्ष्य का प्रकार (प्रतिवादी)</a>
            <a href="#" class="inactiveLink" data-id="8" data-rt="वारदात का विवरण" onclick="reportLinkClick(event,this,'Wardaat',8,'वारदात का विवरण')">वारदात का विवरण</a>
            <a href="#" class="inactiveLink" data-id="9" data-rt="न्यायालय का विवरण" onclick="reportLinkClick(event,this,'Court',9,'न्यायालय का विवरण')">न्यायालय का विवरण</a>

            <%--<p class="navbar-fixed-bottom" style="margin-left: 25px;">
                <img src="../../images/line.png" /><br />
                Developed & Designed By<br />
                <img src="../../images/nic_logo.png" style="width: 100px; height: auto; margin-left: 30px" /><br />
            </p>--%>
        </div>
        <div class="content" style="height:10%">

            <div class="row" style="background: linear-gradient(to right, #1eb089 0%, #7c49ab 100%, #7bd9e8 100%) repeat scroll 0 0;color: white!important; color:white; padding:15px;">
                <div class="col-md-2">
                    <asp:HiddenField ID="hdnDistrict" runat="server"/>
                    <asp:HiddenField ID="hdnBlockCode" runat="server"/>
                    <asp:HiddenField ID="hdnThanaCode" runat="server"/>
                    District
                    <asp:DropDownList ID="ddDistrict" runat="server" CssClass="form-control form-check" placeholder="District">
                        
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Circle
                    <asp:DropDownList ID="ddBlock" runat="server" CssClass="form-control form-check" placeholder="District" Enabled="true">
                       
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Police Station
                    <asp:DropDownList ID="ddlPoliceSation" runat="server" CssClass="form-control form-check" placeholder="Police Station" Enabled="true">
                       
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Panchayat
                    <asp:DropDownList ID="ddPanchayat" runat="server" CssClass="form-control form-check" placeholder="Panchayat" Enabled="true">
                        <asp:ListItem Text="Panchayat" Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Sensivity
                    <asp:DropDownList ID="ddSensivity" runat="server" CssClass="form-control form-check" placeholder="Sensivity">
                        <asp:ListItem Value="0">All</asp:ListItem>
                                                                                <asp:ListItem Value="1" id="s" class="greycolor">सामान्य *</asp:ListItem>
                                                                                <asp:ListItem Value="2" id="sv">संवेदनशील **</asp:ListItem>
                                                                                <asp:ListItem Value="3" id="sv2">अतिसंवेदनशील ***</asp:ListItem>
                                                                            <asp:ListItem Value="4" id="sv4">संवेदनशील **+अतिसंवेदनशील ***</asp:ListItem>
                                                                                
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Action
                    <asp:DropDownList ID="ddlaction" runat="server" CssClass="form-control form-check" placeholder="District">
                       <asp:ListItem Value="0">All</asp:ListItem>
                                                                                <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                                                                                <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                                                                                <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                                                                                <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                                                                                <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                        <asp:ListItem Value="6">न्यायालय में लंबित</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>

            <div class="row" style="background-color:#cae1fc; padding:10px;box-shadow: rgba(0, 0, 0, 0.12) 0px 1px 3px, rgba(0, 0, 0, 0.24) 0px 1px 2px;">
               
               
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="Label2" runat="server" Text="Start Date" Style="color: black; float: right; margin-top: 7px;"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtdatefrom" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label3" runat="server" Text="End Date" Style="color: black; margin-top: 7px;"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                        </div>
                        
                        <div class="col-md-1">
                            <a href="#" id="btnSearch" class="btn btn-success"
                                role="button" aria-pressed="true" style="float: left;">Submit</a>                          
                        </div>
                        <div class="col-md-1">
                           <a href="#" id="btnClear" class="btn btn-danger"
                                role="button" aria-pressed="true" style="float: left">Clear Date</a>                                                     
                        </div>
                          <div class="col-md-1">
                            <a href="#" id="btnBack" class="btn btn-danger" runat="server"
                                role="button" aria-pressed="true" style="float: left;">Back</a>                          
                        </div>
                         
                    </div>
                </div>
                <div class="col-md-2">
                    </div>
                <div class="col-md-2">
                   
                    <a href="../../Default.aspx" class="btn btn-danger"
                        role="button" aria-pressed="true" style="float: right">Home</a>
                </div>
            </div>

              


            <br />
            <div class="row">
                <div class="col-md-2">
                    <div id="divPopulationData" class="col-md-12">
                        <div id="divtotal">

                    </div>
                   <%-- <center style="border: 1px solid white; border-radius: 5px; box-shadow: rgba(6, 24, 44, 0.4) 0px 0px 0px 2px, rgba(6, 24, 44, 0.65) 0px 4px 6px -1px, rgba(255, 255, 255, 0.08) 0px 1px 0px inset;"class="activemenu">
                    
                    <asp:Label ID="Label5" runat="server" Text="Total Application"></asp:Label><br />
                    <asp:Label ID="lblTotalApplication" runat="server" Text="0"></asp:Label><br />
                   
                   </center>--%>
                    <br />
                    <div id="divdynamic" style="width:100%;overflow-y: auto;height: 600px;">

                    </div>
                       
            </div>

                   
 
                </div>
                <div class="col-md-7">
                    <div id="titlebind">

                    </div>
                  
                    
                    <%--<span style="color:blue;font-weight:bold"><asp:Label ID="mapview" runat="server" ></asp:Label></span>--%>
                    <div  id="containermap" style="height: 600px">
                        
                    </div>
                    <div id="legendbind" style="text-align: center;">

                    </div>
                </div>
                <div class="col-md-3">
                    <span style="color:blue;font-weight:bold;font-size:18px;padding:10px,10px,10px,10px"><asp:Label ID="Label1" runat="server" ></asp:Label></span>
                    <div id="top_x_div" style=" height: 600px;"></div>
                </div>
            </div>


            <br/>
            <div class="row">                
                <div class="col-md-2">
                    
                    <div id="chartContainer1" style="height: 750px; width: 100%;"></div>
                  
                    


                        
                </div>

                <div class="col-md-7">
                    
                    <div id="chartContainer2" class="hidden" style="height: 370px; width: 100%;"></div>
                </div>
                <div class="col-md-3"></div>
            </div>
            
  </div>
    </form>
     
    <script src="bhudashboardgraph.js"></script>
    <script>
       


       
    </script>
</body>
</html>

