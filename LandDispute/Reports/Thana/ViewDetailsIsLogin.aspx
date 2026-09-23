<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewDetailsIsLogin.aspx.cs" Inherits="LandDispute_THANA_ViewDetailsIsLogin" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <%--<link rel="stylesheet" type="text/css" href="../../Frontpage/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/font.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/li-scroller.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/slick.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/jquery.fancybox.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/theme.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/style.css" />--%>

    <style type="text/css">
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td {
                /*
    padding: 2px; 
    border: solid 1px #c1c1c1; 
    color: #717171; 
        */
                vertical-align: top;
                border: 1px solid #c1c1c1;
                padding: 2px;
                font-size: 12pt;
                font-weight: normal;
                color: #000000;
                background-color: White;
            }

            .mGrid th {
                /*
    padding: 4px 2px; 
    color: #fff; 
    background: #424242 url(grd_head.png) repeat-x top; 
    border-left: solid 1px #525252; 
    border-right: solid 1px #525252; 
    font-size: 1.0em; */
                background: -o-linear-gradient(bottom, #187ab9 5%, #014e9c 100%);
                background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #187ab9), color-stop(1, #014e9c) );
                background: -moz-linear-gradient( center top, #187ab9 5%, #014e9c 100% );
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#187ab9", endColorstr="#014e9c");
                background: -o-linear-gradient(top,#187ab9,014e9c);
                background-color: #009900;
                border: 0px solid #014e9c;
                text-align: center;
                border-width: 0px 0px 1px 1px;
                font-size: 14px;
                color: #ffffff;
            }

            .mGrid .alt { /* background: #fcfcfc url(grd_alt.png) repeat-x top;*/
            }

            .mGrid .pgr {
                background: #424242 url(grd_pgr.png) repeat-x top;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>

    <style type="text/css">
        divclss {
            -ms-overflow-style: none; /* for Internet Explorer, Edge */
            scrollbar-width: none; /* for Firefox */
            overflow-y: scroll;
        }

            divclss::-webkit-scrollbar {
                display: none; /* for Chrome, Safari, and Opera */
            }

        /* other styling */
        divclss {
            border: solid 5px black;
            border-radius: 5px;
            height: 300px;
            padding: 2px;
            width: 200px;
        }

            divclss.* {
                background-color: #EAF0F6;
                color: #2D3E50;
                font-family: 'Avenir';
                font-size: 26px;
                font-weight: bold;
            }
    </style>

    <style type="text/css">
        .grid th {
            padding: 4px;
            font-weight: bold;
            font-size: small;
            text-align: center;
        }

        .grid td, th {
            padding: 4px;
            font-size: small;
        }

        .grid tr:hover {
            background-color: #d8f9d3;
        }

        .grid td:hover {
            background-color: #ff2;
        }

        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        body {
            margin: 0;
            padding: 0;
            height: 100%;
        }

        .modal {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function myFunctionConfirm() {

            let text = "Are you sure you want to delete thana ? You can not make any change after delete.";
            if (confirm(text) == true) {
                return true;
            } else {
                window.location.reload();//fro load page
                return false;
            }
        }
    </script>
     <script type="text/javascript">
        function myload() {
                window.location.reload() ;         
         }
         
    </script>
    <script type="text/javascript">

        function fnLinkbutton(objlinkbutton) {
            debugger
            //Access the link button here
            var imgDiv = document.getElementById("divImage");
            var inlineFrameExample = document.getElementById("inlineFrameExample");
            var lb1 = document.getElementById(objlinkbutton).getAttribute("path");;
            var bookingID = lb1;
            alert(lb1);
            //  var a = "/LDHOME/LandDispute/uploads/LD212095111/LD212124065/fuIdDocumentLD212124065.pdf";
            var a = lb1.substring(1)
       // inlineFrameExample.src = '<%=ResolveUrl("' + a + '")%>';
            //inlineFrameExample.src = "" + a;
            var base_Path = "/" + window.location.pathname.split('/')[1];
            inlineFrameExample.src = base_Path + a;
            var width = document.body.clientWidth;
            imgDiv.style.left = (width - 1200) / 2 + "px";
            imgDiv.style.top = "10px";

            imgDiv.style.display = "block";
            return false;
            //alert(lb1);
        }
        function fnLinkbutton1(objlinkbutton) {
            debugger
            //Access the link button here
            var imgDiv = document.getElementById("divImage");
            var inlineFrameExample = document.getElementById("inlineFrameExample");
            var lb1 = document.getElementById(objlinkbutton).getAttribute("path");;
            var bookingID = lb1;
            // alert(lb1);
            //  var a = "/LDHOME/LandDispute/uploads/LD212095111/LD212124065/fuIdDocumentLD212124065.pdf";
            var a = lb1.substring(1)
        // inlineFrameExample.src = '<%=ResolveUrl("' + a + '")%>';
            //inlineFrameExample.src = "/LDHOME" + a;
            var base_Path = "/" + window.location.pathname.split('/')[1];
            inlineFrameExample.src = base_Path + a;
            var width = document.body.clientWidth;
            imgDiv.style.left = (width - 1200) / 2 + "px";
            imgDiv.style.top = "10px";

            imgDiv.style.display = "block";
            return false;
            //alert(lb1);
        }


        function js(url) {



            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            imgDiv.style.display = "none";

        }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="panel panel-primary">
        <div class="panel-heading">
            <h4>
                <br />
                 </h4>

        </div>
      
                <div class="panel-body">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                    <div class="row" style="padding: 05.px">
                        <div class="col-md-1"></div>
                        <div class="col-md-2 text-center">
                            District
                        </div>
                        <div class="col-md-2 text-center">
                            Sub - Division
                        </div>
                        <div class="col-md-2 text-center">
                            Circle
                        </div>
                        <div class="col-md-2 text-center">
                            Thana Name
                        </div>
                        <div class="col-md-2 text-center">
                            Is-Login
                        </div>
                    </div>
                    <div class="row" style="padding: 05.px">
                        <div class="col-md-1"></div>
                        <div class="col-md-2 text-center">
                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="true" Style="height: 30px;"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-center">
                            <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control" Enabled="true" Style="height: 30px;"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2 text-center">

                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" Enabled="true" Style="height: 30px;"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2 text-center">
                            <asp:DropDownList ID="ddlThana" runat="server" CssClass="form-control" Enabled="true" Style="height: 30px;"
                                AutoPostBack="True">
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2 text-center">
                            <asp:DropDownList ID="ddlIsLogin" runat="server" CssClass="form-control" Enabled="true" Style="height: 30px;"
                                AutoPostBack="True">
                                <asp:ListItem Value="y">Yes</asp:ListItem>
                                <asp:ListItem Value="N">No</asp:ListItem>
                            </asp:DropDownList>

                        </div>

                    </div>                  
                      </ContentTemplate>
        </asp:UpdatePanel>
                     <div class="row" style="padding: 13px">
                        <div class="col-md-4">


                            <span style="text-align: center">
                                <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" Style="text-align: center; font-weight: bold;"></asp:Label>
                            </span>


                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search"
                                CssClass="btn btn-primary" Style="width: 100px" OnClick="btnSearch_Click" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnExport" runat="server" Text="Export-To-Excel"
                                CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                        </div>
                    </div>
                    <br />
                    <asp:Panel ID="pnlviewDetails" runat="server" ScrollBars="Both">

                        <asp:GridView runat="server" Width="100%" ID="grd_viewDetails" AutoGenerateColumns="false"
                            >
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="DISTRICTNAME" HeaderText="District" ItemStyle-Width="120" />
                                <asp:BoundField DataField="Sd_Name_En" HeaderText="Sub-Division" ItemStyle-Width="120" />
                                <asp:BoundField DataField="BlockName" HeaderText="Block/Circle" ItemStyle-Width="120" />
                                <asp:BoundField DataField="Police_Station" HeaderText="Police-Station" ItemStyle-Width="120" />
                                <%--<asp:BoundField DataField="PS_Code" HeaderText="Police-code" ItemStyle-Width="120" />--%>
                                <asp:BoundField DataField="islogin" HeaderText="Is-Login" ItemStyle-Width="120" />
                                <asp:TemplateField Visible="false" ItemStyle-Width="100">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldiscode" runat="server" Text='<%#Eval("DISTRICTCODE")%>'></asp:Label>
                                        <asp:Label ID="lblsubcode" runat="server" Text='<%#Eval("Sd_Code2")%>'></asp:Label>
                                        <asp:Label ID="lblblcode" runat="server" Text='<%#Eval("BlockCode")%>'></asp:Label>
                                        <asp:Label ID="lblPS_Code" runat="server" Text='<%#Eval("PS_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </asp:Panel>
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
