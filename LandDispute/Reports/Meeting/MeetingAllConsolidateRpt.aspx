<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="MeetingAllConsolidateRpt.aspx.cs" EnableEventValidation="false"
    Inherits="LandDispute_Reports_Meeting_Report_MeetingAllConsolidateRpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="../../../bhusamadhan/vendor/fontawesome-free-6.1.1/css/all.min.css" rel="stylesheet" />
    <link href="../../../bhusamadhan/css/ruang-admin.min.css" rel="stylesheet" />
    <link href="../../../bhusamadhan/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../bhusamadhan/gfg-style.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>--%>
    <style type="text/css">
        .modalBackground {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: auto;
            height: auto;
        }

        .aligenLeft {
            text-align: left;
        }

        .aligenRight {
            text-align: right !important;
            padding-right: 5px;
        }

        .padNum {
            padding-right: 5px;
        }


        .form-groupManual {
            margin-bottom: 0 !important;
        }

        .hrManual {
            margin-top: 0 !important;
            margin-bottom: 5px !important;
        }
    </style>

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

    <script type="text/javascript">
        $(document).ready(function () {
            $(".btnDefectReject").click(function () {
                if ($(".ddlDefRejRemarks").css("visibility") == "visible") {
                    if ($(".ddlDefRejRemarks").val() == 0) {
                        alert("Please Select Reason!");
                        $(".ddlDefRejRemarks").focus();
                        return false;
                    }

                    if ($(".txtDefRejRemarks").val().length < 6) {
                        alert("Please Enter Reamrks For Rejection/Defective!");
                        $(".txtDefRejRemarks").focus();
                        $(".txtDefRejRemarks").select();
                        return false;
                    }
                    //return false;
                }
                else {
                    //alert("Not found ddl visisble");
                    //return false;
                }
            });
        })


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
            // urlpdf = ("http://localhost:8080" + urlpdf).replace(' ', '');
            urlpdf = ("http://localhost:8080" + urlpdf);
            urlpdf = urlpdf.trim();
            $.ajax({
                type: "POST",
                url: "MeetingAllConsolidateRpt.aspx/Getpdf",
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">Meeting wise Report</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-6">
                        <span style="text-align: center">
                            <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" Style="text-align: center; font-weight: bold;"></asp:Label>
                        </span>
                    </div>
                </div>
                <asp:UpdatePanel ID="upPanel" runat="server">
                    <ContentTemplate>
                        <div class="row" style="padding: 5px">
                            <div class="col-md-1 text-center"></div>
                            <div class="col-md-2 text-center" runat="server" id="divLabRange">
                                Range
                            </div>
                            <div class="col-md-2 text-center" runat="server" id="divLabCommissionary">
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
                                Thana
                            </div>
                            <div class="col-md-2 text-center"></div>
                        </div>
                        <div class="row" style="padding: 5px">
                            <div class="col-md-1 text-center"></div>
                            <div class="col-md-2 text-center" runat="server" id="divddlRange">
                                <asp:DropDownList ID="ddlRange" runat="server" CssClass="form-control" Enabled="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center" runat="server" id="divddlCommissionary">
                                <asp:DropDownList ID="ddlCommissionary" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlCommissionary_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlsubdivision" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlsubdivision_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlcircle" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlcircle_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlthana" runat="server" CssClass="form-control" Enabled="true"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center"></div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row" style="padding: 5px">
                    <div class="col-md-1 text-center"></div>
                            <div class="col-md-2  text-center">
            Entry Mode
<asp:DropDownList ID="ddlentrymode" runat="server" CssClass="form-control mb-2"
    Enabled="true">
    <asp:ListItem Value="0">All</asp:ListItem>
    <asp:ListItem Value="3">Thana</asp:ListItem>
    <asp:ListItem Value="1">Public</asp:ListItem>
    <asp:ListItem Value="2">CO</asp:ListItem>
</asp:DropDownList>
        </div>
                    <div class="col-md-2 text-center">
                        Form Date<br />
                        <div class="input-group">
                            <asp:TextBox ID="txtfrmdate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control"></asp:TextBox>
                            <span class="input-group-btn">
                                <rjs:PopCalendar ID="popCalendarFrom" runat="server" Control="txtfrmdate" Format="dd mm yyyy" />
                                <asp:RequiredFieldValidator runat="server" ID="RFVFromDate" ValidationGroup="a" ControlToValidate="txtfrmdate" ForeColor="Red" ErrorMessage="*" />
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2 text-center">
                        To Date<br />
                        <div class="input-group">
                            <asp:TextBox ID="txtTodate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control"></asp:TextBox>
                            <span class="input-group-btn">
                                <rjs:PopCalendar ID="popCalendarTo" runat="server" Control="txtTodate" Format="dd mm yyyy" />
                                <asp:RequiredFieldValidator runat="server" ID="RFVToDate" ValidationGroup="a" ControlToValidate="txtTodate" ForeColor="Red" ErrorMessage="*" />
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2 text-center">
                        <br />
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                            CssClass="form-control btn btn-primary" />
                    </div>
                    <div class="col-md-2 text-center">
                        <br />
                        <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Export To Excel" OnClick="btnExpToExl_Click" />
                    </div>
                    <div class="col-md-2 text-center" id="div_btnback" runat="server" visible="false">
                        <br>
                        <asp:Button ID="btnback" CssClass="form-control btn btn-danger " Text="Back" runat="server" OnClick="btnback_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlgrid" runat="server" ScrollBars="Auto">
                            <asp:GridView ID="grvAllMeeting" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found"
                                EmptyDataRowStyle-ForeColor="Red" ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" OnRowCommand="grvAllMeeting_RowCommand"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" Width="2%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="DIVISIONAME" HeaderText="Division">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="2%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="DISTRICTNAME" HeaderText="District">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Sd_Name_En" HeaderText="Sub Division">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="2%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="BlockName" HeaderText="Circle">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Police_Station" HeaderText="Police Station">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:BoundField>


                                    <asp:BoundField DataField="Total_application" HeaderText="Total Application">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Total_Meeting" HeaderText="Total Meeting">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:BoundField>









                                    <asp:TemplateField HeaderStyle-Width="3%" HeaderText="" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTotal" runat="server" CommandArgument='<%#Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+  Eval("PS_Code")+","+"0"+","+Eval("Total_application")%>' CommandName="TotalClick"
                                                Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Data">view
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Blue" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>













                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <EmptyDataRowStyle Font-Size="Large" ForeColor="Red" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" BorderColor="White" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                    <div class="col-md-12">
                        <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x: auto; overflow-y: hidden;" Visible="false">
                            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server" DataKeyNames="a_id"
                                AutoGenerateColumns="false" EnableTheming="false" Width="100%" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                BackColor="White" BorderStyle="None" BorderWidth="0px" CssClass="mGrid" GridLines="None"
                                Style="width: 100%;" HeaderStyle-BackColor="Beige"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Application No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApplicationNo" OnClientClick="openwindow(this);" runat="server" ForeColor="Blue"
                                                Text='<%#Eval("ApplicationNo")%>' CommandArgument='<%#Eval("a_id")%>' Font-Underline="false" OnClick="lnkView_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="कमिश्नरी <hr style='margin-bottom: 0px; margin-top: 0px;' /> जिला <hr style='margin-bottom: 0px; margin-top: 0px;' /> सब डिवीज़न" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%" ItemStyle-VerticalAlign="Top">
                                        <ItemTemplate>
                                            <%#Eval("DIVISIONAME")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("DISTRICTNAME")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("Sd_Name_En")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अंचल <hr style='margin-bottom: 0px; margin-top: 0px;' />थाना "
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("BlockName")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("Police_Station")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ग्राम पंचायत <hr style='margin-bottom: 0px; margin-top: 0px;' />राजस्व ग्राम<hr style='margin-bottom: 0px; margin-top: 0px;' />वार्ड"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%#Eval("PanchayatName")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("VILLNAME")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("WARDNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="कुल वादी " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#Eval("TotalVadi")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="कुल प्रतिवादी " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#Eval("TotalPratiVadi")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="भूमि का प्रकार" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <%#Eval("Bhumitype")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("SarkariBhumiType")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


















                                    <asp:TemplateField HeaderText="वादी का </br>दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Image6" Visible='<%# CheckNull(Eval("Vadi_sakshya_File"))%>' path='<%#Eval("Vadi_sakshya_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="प्रतिवादी का </br>दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                        ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Image1" Visible='<%# CheckNull(Eval("Prativadi_sakshya_File"))%>' path='<%# Eval("Prativadi_sakshya_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="40%" HeaderStyle-Wrap="false">
                                        <ItemTemplate>
                                            <div style="width: 100%; height: 300px; overflow: auto">
                                                <asp:GridView ID="grdAction" runat="server"
                                                    AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid fontsize"
                                                    EnableTheming="false" ShowFooter="false" EmptyDataText="No Record Found" Visible="true">
                                                    <Columns>



                                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1+"." %>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="भूमि विवाद की सवेदनशीलता" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <%#Eval("SensitivityType")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="बैठक की तिथि" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                                            <ItemTemplate>
                                                                <%#Eval("Meeting_date", "{0:dd, MMM yyyy}")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="क्या वादी उपस्थित है ?" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <%#Eval("Is_Vadi_Present")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="क्या प्रतिवादी उपस्थित है ?" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <%#Eval("Is_PratiVadi_Present")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="बैठक का निष्कर्ष" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="8%">
                                                            <ItemTemplate>
                                                                <%#Eval("Action")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="अंचलाधिकारी का मंतव्य" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <%#Eval("anchala_dhikari_mantavy")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="थानाध्यक्ष का मंतव्य" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <%#Eval("thana_prabhari_mantavy")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="बैठक में लिया गया निर्णय" ItemStyle-HorizontalAlign="Left"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <%#Eval("conclusion_of_the_meeting")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>





                                                    </Columns>
                                                </asp:GridView>
                                            </div>
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
    <%-- <script src="../../../bhusamadhan/vendor/jquery/jquery.min.js"></script>
    <script src="../../../bhusamadhan/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../../../bhusamadhan/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../../../bhusamadhan/vendor/chart.js/Chart.min.js"></script>
    <script src="../../../bhusamadhan/js/demo/chart-area-demo.js"></script>
    <script src="../../../bhusamadhan/vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <script src="../../../bhusamadhan/js/ruang-admin.min.js"></script> --%>
</asp:Content>

