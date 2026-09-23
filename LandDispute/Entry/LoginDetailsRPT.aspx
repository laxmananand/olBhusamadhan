<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="LoginDetailsRPT.aspx.cs" Inherits="LandDispute_BLKADM_test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />


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
            inlineFrameExample.src = "" + a;
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
            inlineFrameExample.src = "/LDHOME" + a;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">LOGIN DETAILS REPORT
                        </h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <%--<asp:Button ID="btnback" CssClass="btn btn-danger " Text="Back" runat="server" OnClick="btnback_Click" 
                            Visible="false" />--%>
                    </div>
                    <div class="col-md-6">
                        <span style="text-align: center">
                            <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" Style="text-align: center; font-weight: bold;"></asp:Label>
                            <asp:HiddenField id="hdn1" runat="server" Value=""/>
                        </span>
                    </div>
                </div>
                <br>
                <div class="row" style="padding: 5px">
                    <div class="col-md-1 text-center"></div>
                    <div class="col-md-2 text-center">
                        Commissionary
                    </div>
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
                        Police Station
                    </div>
                    <div class="col-md-2 text-center"></div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row" style="padding: 5px">

                            <div class="col-md-1 text-center"></div>

                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlCommissionary" OnSelectedIndexChanged="ddlCommissionary_SelectedIndexChanged" runat="server" CssClass="form-control" Enabled="true"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlDistrict" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" runat="server" CssClass="form-control" Enabled="true"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlSubDivision" OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged" runat="server" CssClass="form-control" Enabled="true"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlBlock" OnSelectedIndexChanged="ddlBlock_OnSelectedIndexChanged" runat="server" CssClass="form-control" Enabled="true"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlPoliceStation" runat="server" CssClass="form-control" Enabled="true"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-2 text-center"></div>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row" style="padding: 5px">
                    <div class="col-md-1 text-center"></div>
                    <div class="col-md-2 text-center">
                        Role
                             <br />
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" Enabled="true"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 text-center">
                        <br />
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Search"
                            CssClass="form-control btn btn-primary" />
                    </div>
                    <div class="col-md-2 text-center">
                        <br />
                        <asp:Button ID="btn_Export" OnClick="btnExpToExl_Click" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Export To Excel" />
                    </div>
                    <div class="col-md-2 text-center"></div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="Pnldata" runat="server" ScrollBars="Auto">
                            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound"
                                runat="server" DataKeyNames="UserID"
                                AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                BackColor="White" BorderStyle="None" BorderWidth="0px" CssClass="mGrid" GridLines="None"
                                AllowPaging="false" Style="width: 100%;"
                                HeaderStyle-BackColor="Beige" OnPageIndexChanging="GridView1_PageIndexChanging"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Division"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <%#Eval("DIVISIONAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <%#Eval("DISTRICTNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Sub-Division"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>

                                            <%#Eval("Sd_Name_En")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Block "
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%#Eval("BlockName")%>
                                           
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>





                                    <asp:TemplateField HeaderText="Police Station "
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                           
                                            <%#Eval("Police_Station")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="User Name "
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%#Eval("UserName")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Role Description"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("RoleDesc")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="User ID "
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%#Eval("UserID")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Password " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#Eval("pwd")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mobile No. " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#Eval("Mobile")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Email ID" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="12%" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#Eval("Email")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Is Login" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#Eval("IsPassChange")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
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


