<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="ApplicationConsolidateRpt.aspx.cs" Inherits="LandDispute_Report_ApplicationConsolidateRpt" %>
 <%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="../../../bhusamadhan/vendor/fontawesome-free-6.1.1/css/all.min.css" rel="stylesheet" />
     <link href="../../../bhusamadhan/css/ruang-admin.min.css" rel="stylesheet" />
     <link href="../../../bhusamadhan/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
     <link href="../../../bhusamadhan/gfg-style.css" rel="stylesheet" />
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>--%>

     <style type="text/css">
        .modalBackground
        {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: auto;
            height: auto;
        }        
        .aligenLeft
        {
            text-align: left;
        }        
        .aligenRight
        {
            text-align: right !important;
            padding-right: 5px;
        }
        .padNum
        {
            padding-right: 5px;
        }       
        .form-groupManual
        {
            margin-bottom: 0 !important;
        }
        .hrManual
        {
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
    border-collapse:collapse; 
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
     background-color:White;
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
.mGrid .alt { /* background: #fcfcfc url(grd_alt.png) repeat-x top;*/ }
.mGrid .pgr { background: #424242 url(grd_pgr.png) repeat-x top; }
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { 
    border-width: 0; 
    padding: 0 6px; 
    border-left: solid 1px #666; 
    font-weight: bold; 
    color: #fff; 
    line-height: 12px; 
 }   
.mGrid .pgr a { color: #666; text-decoration: none; }
.mGrid .pgr a:hover { color: #000; text-decoration: none; }
       

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
    <script src="JS/Jquerry2000min.js"></script>
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
    </script>
    <script type="text/javascript">
        function DisableBackButton() {
            debugger;
            history.go(-1);
            //window.history.forward();
        }
    </script>
    <script type="text/javascript">




        $("#ctl00_ContentPlaceHolder1_btnSearch").click(function () {
            alert("dd");
            $("#load").addClass("spinner-border");
            $("#load").addClass("spinner-border-sm");
            return false;
        });


        function check() {


            $("#load").addClass("spinner-border");
            $("#load").addClass("spinner-border-sm");
            return false;
        }

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
                url: "ApplicationConsolidateRpt.aspx/Getpdf",
                data: "{'url':'" + urlpdf + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    window.open("../../IDoc.aspx?url=" + response.d, "_blank");
                    //inlineFrameExample.src = response.d;
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
        $(document).ready(function () {
            $(".getpdfdoc").click(function (event) {
                event.preventDefault();
                var urlpdf = $(this).attr("path");

                getpdfdocument(urlpdf);
                return false;
            });
        });
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            imgDiv.style.display = "none";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <asp:ScriptManager ID="scriptid" runat="server"></asp:ScriptManager>--%>
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">
                            Application Consolidated Report</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                       
                    </div>
                    <div class="col-md-7">
                       
                         <span style="text-align:center">
                       <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" style="text-align: center; font-weight: bold;"></asp:Label>
                       </span>
                    </div>
                    <div class="col-md-2">
                       
                    </div>
                </div>  
                <div class="row">
                     <div class="col-md-2"></div>
                      <div class="col-md-2">From-Date</div>
                      <div class="col-md-2">To-Date</div>
                    <div class="col-md-2">Entry Mode</div>
                      <div class="col-md-2"></div>
                      <div class="col-md-2"></div>
                </div>
                <div class="row">
                     <div class="col-md-2">
                        <asp:Button ID="btnback" CssClass="btn btn-danger " Text="Back" runat="server" OnClick="btnback_Click" 
                            Visible="false" />
                    </div>
                     <div class="col-md-2">                         
                        <div class="input-group">
                               <asp:TextBox ID="txtFromdate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control w-50" ></asp:TextBox>
                            <span class="input-group-btn">
                            <rjs:popcalendar ID="popCalendarFrom" runat="server" Control="txtFromdate" Format="dd mm yyyy" />
                                 <asp:RequiredFieldValidator runat="server" id="RFVFromDate" ValidationGroup="a" controltovalidate="txtFromdate" ForeColor="Red" errormessage="*" />
                            </span>
                           </div>
                      </div>
                     <div class="col-md-2">
                        <div class="input-group">
                            <asp:TextBox ID="txTodate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control w-50"></asp:TextBox>
                             <span class="input-group-btn">
                                 <rjs:popcalendar ID="popCalendarTo" runat="server" Control="txTodate" Format="dd mm yyyy" />
                                 <asp:RequiredFieldValidator runat="server" id="RFVToDate" ValidationGroup="a" controltovalidate="txTodate" ForeColor="Red" errormessage="*" />
                             </span>
                      </div>
                        </div>
                    <div class="col-md-2  text-center">
                       
            <asp:DropDownList ID="ddlentrymode" runat="server" CssClass="form-control mb-2"
                Enabled="true">
                <asp:ListItem Value="0">All</asp:ListItem>
                <asp:ListItem Value="3">Thana</asp:ListItem>
                <asp:ListItem Value="1">Public</asp:ListItem>
                <asp:ListItem Value="2">CO</asp:ListItem>
            </asp:DropDownList>
                    </div>
                     <div class="col-md-2">
                           <asp:Button ID="btnSearch" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Search" OnClick="btnsearch_Click"  />               
                    </div>
                     <div class="col-md-2">
                        <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Export To Excel" OnClick="btnExpToExl_Click" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">                    
                        <asp:Panel ID="pnlDist" runat="server">
                            <asp:Label ID="lblPrintDateforDivision" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetailforDivision" runat="server" Text="*Click on the Divion Name to view District-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:Panel ID="PanelDivision" runat="server" ScrollBars="Auto">
                                         <asp:GridView ID="grd_Division" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" DataKeyNames="DIVISIONCODE" EmptyDataText="No Record(s) found"
                                OnRowCommand="grd_Division_RowCommand" EmptyDataRowStyle-ForeColor="Red" ShowFooter="True"
                                Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                             <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Division" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivision" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("DIVISIONAME")+","+Eval("Total")%>'
                                                CommandName="DivisionClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View District-Wise Application Status"><%# Eval("DIVISIONAME")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderText="कुल आवेदन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionTotal" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("Total")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionTotal_Click"><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionFinalize" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="आंशिक प्रविष्टि">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionUnFinalize" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रारंभिक निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionNirast" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("Nirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionNirast_Click"><%# Eval("Nirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अंतिम निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionFinalNirast" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("FinalNirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionFinalNirast_Click"><%# Eval("FinalNirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रक्रियाधीन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionPrakriyadhin" runat="server" CommandArgument='<%# Eval("DivisionCODE")+","+Eval("Prakriyadhin")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionPrakriyadhin_Click"><%# Eval("Prakriyadhin")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अस्वीकृत">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionAshwikrit" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("Ashwikrit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionAshwikrit_Click"><%# Eval("Ashwikrit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField  HeaderText="मापी क़े लिए निर्धारित">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionMapi_Nirdharit" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("Mapi_Nirdharit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionMapi_Nirdharit_Click"><%# Eval("Mapi_Nirdharit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField  HeaderText="न्यायालय में लंबित" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivisionvaadi_ki_vaad_sankhya_varsh" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("CourtInPending")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionvaadi_ki_vaad_sankhya_varsh_Click"><%# Eval("CourtInPending")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
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
                        </asp:Panel>
                        <asp:Panel ID="pnlDistrict" runat="server">                           
                            <asp:Label ID="lblPrintDateforDistrict" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetailforDistrict" runat="server" Text="*Click on the District Name to view SubDivision-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                             <asp:Panel ID="PanelDistrict" runat="server" ScrollBars="Auto">
                                       <asp:GridView ID="grdDistrict" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" OnRowCommand="grdDistrict_RowCommand"
                                EmptyDataRowStyle-ForeColor="Red" ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="District" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrict" runat="server" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("DISTRICTNAME")+","+Eval("Total")%>'
                                                CommandName="DistrictClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Sub-Division-Wise Application Status"><%# Eval("DISTRICTNAME")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="कुल आवेदन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictTotal" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("Total")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictTotal_Click"><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="आंशिक प्रविष्टि">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictUnFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रारंभिक निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("Nirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictNirast_Click"><%# Eval("Nirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अंतिम निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictFinalNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("FinalNirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictFinalNirast_Click"><%# Eval("FinalNirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रक्रियाधीन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictPrakriyadhin" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("Prakriyadhin")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictPrakriyadhin_Click"><%# Eval("Prakriyadhin")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अस्वीकृत">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictAshwikrit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("Ashwikrit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictAshwikrit_Click"><%# Eval("Ashwikrit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField  HeaderText="मापी क़े लिए निर्धारित">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictMapi_Nirdharit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("Mapi_Nirdharit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictMapi_Nirdharit_Click"><%# Eval("Mapi_Nirdharit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField  HeaderText="न्यायालय में लंबित" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictvaadi_ki_vaad_sankhya_varsh" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("DISTRICTCODE")+","+Eval("CourtInPending")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictvaadi_ki_vaad_sankhya_varsh_Click"><%# Eval("CourtInPending")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
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
                        </asp:Panel>
                        <asp:Panel ID="pnlSubDivision" runat="server">                           
                            <asp:Label ID="lblPrintDateforSubDivision" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetailforSubDivision" runat="server" Text="*Click on the SubDivision Name to view Circle/Block-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                             <asp:Panel ID="PanelSubDivision" runat="server" ScrollBars="Auto">
                                      <asp:GridView ID="grdSubDivision" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" OnRowCommand="grdSubDivision_RowCommand"
                                EmptyDataRowStyle-ForeColor="Red" ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sub-Division" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivision" runat="server" CommandArgument='<%#Eval("Sd_Code2")+","+Eval("Sd_Name_En")+","+Eval("Total")%>'
                                                CommandName="SubDivisionClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Block-Wise Application Status"><%# Eval("Sd_Name_En")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="कुल आवेदन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionTotal" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Total")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionTotal_Click"><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="आंशिक प्रविष्टि">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionUnFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रारंभिक निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Nirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionNirast_Click"><%# Eval("Nirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अंतिम निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionFinalNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("FinalNirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionFinalNirast_Click"><%# Eval("FinalNirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रक्रियाधीन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionPrakriyadhin" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Prakriyadhin")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionPrakriyadhin_Click"><%# Eval("Prakriyadhin")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अस्वीकृत">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionAshwikrit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Ashwikrit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionAshwikrit_Click"><%# Eval("Ashwikrit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField  HeaderText="मापी क़े लिए निर्धारित">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionMapi_Nirdharit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Mapi_Nirdharit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionMapi_Nirdharit_Click"><%# Eval("Mapi_Nirdharit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField  HeaderText="न्यायालय में लंबित" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionvaadi_ki_vaad_sankhya_varsh" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("CourtInPending")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionvaadi_ki_vaad_sankhya_varsh_Click"><%# Eval("CourtInPending")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
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
                        </asp:Panel>
                        <asp:Panel ID="pnlCircle" runat="server">                         
                            <asp:Label ID="lblPrintDateforCircle" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetailforCircle" runat="server" Text="*Click on the Block/Circle Name to view PoliceStation-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                             <asp:Panel ID="PanelCircle" runat="server" ScrollBars="Auto">
                                 <asp:GridView ID="grdCircle" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" OnRowCommand="grdCircle_RowCommand"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                      <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Block" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlock" runat="server" CommandArgument='<%# Eval("BlockCode")+","+Eval("BlockName")+","+Eval("Total")%>'
                                                CommandName="BlockClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Thana-Wise Application Status"><%# Eval("BlockName")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="कुल आवेदन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockTotal" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Total")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockTotal_Click"><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>
                                  
                                      <asp:TemplateField HeaderText="आंशिक प्रविष्टि">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockUnFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>
                                  
                                      <asp:TemplateField HeaderText="प्रारंभिक निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Nirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockNirast_Click"><%# Eval("Nirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="अंतिम निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockFinalNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("FinalNirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockFinalNirast_Click"><%# Eval("FinalNirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="प्रक्रियाधीन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockPrakriyadhin" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Prakriyadhin")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockPrakriyadhin_Click"><%# Eval("Prakriyadhin")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="अस्वीकृत">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockAshwikrit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Ashwikrit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockAshwikrit_Click"><%# Eval("Ashwikrit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField  HeaderText="मापी क़े लिए निर्धारित">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockMapi_Nirdharit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Mapi_Nirdharit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockMapi_Nirdharit_Click"><%# Eval("Mapi_Nirdharit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>


                                   <asp:TemplateField  HeaderText="न्यायालय में लंबित" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlock_ki_vaad_sankhya_varsh" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("CourtInPending")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlock_ki_vaad_sankhya_varsh_Click"><%# Eval("CourtInPending")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
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
                        </asp:Panel>
                        <asp:Panel ID="pnlthana" runat="server">                         
                            <asp:Label ID="lblPrintDateforThana" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetailforThana" runat="server" Text="*Click on the PoliceStation Name to view Panchayat-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                              <asp:Panel ID="PanelThana" runat="server" ScrollBars="Auto">
                                          <asp:GridView ID="grdThana" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                OnRowCommand="grdThana_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                      <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Police Station" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPoliceStation" runat="server" CommandArgument='<%# Eval("PS_Code")+","+Eval("Police_Station")+","+Eval("Total")%>'
                                                CommandName="PoliceStationClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Panchayat-Wise Application Status"><%# Eval("Police_Station")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="कुल आवेदन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkThanaTotal" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("Total")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaTotal_Click"><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkThanaFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("Finalize")%>'
                                                     ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaFinalize_Click"><%# Eval("Finalize")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>
                                     <asp:TemplateField HeaderText="आंशिक प्रविष्टि">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkThanaUnFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="प्रारंभिक निष्पादन">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkThanaNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("Nirast")%>'
                                                     ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaNirast_Click"><%# Eval("Nirast")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="अंतिम निष्पादन">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkThanaFinalNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("FinalNirast")%>'
                                                     ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaFinalNirast_Click"><%# Eval("FinalNirast")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="प्रक्रियाधीन">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkThanaPrakriyadhin" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("Prakriyadhin")%>'
                                                     ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaPrakriyadhin_Click"><%# Eval("Prakriyadhin")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="अस्वीकृत">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkThanaAshwikrit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("Ashwikrit")%>'
                                                     ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaAshwikrit_Click"><%# Eval("Ashwikrit")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField  HeaderText="मापी क़े लिए निर्धारित">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkThanaMapi_Nirdharit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("Mapi_Nirdharit")%>'
                                                     ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaMapi_Nirdharit_Click"><%# Eval("Mapi_Nirdharit")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>
                                 

                                       <asp:TemplateField  HeaderText="न्यायालय में लंबित" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkThana_ki_vaad_sankhya_varsh" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("CourtInPending")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkThana_ki_vaad_sankhya_varsh_Click"><%# Eval("CourtInPending")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
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
                        </asp:Panel>
                        <asp:Panel ID="pnlpanchayat" runat="server">
                            <asp:Label ID="lblPrintDateforPanchayat" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetailforPanchayat" runat="server" Text="*Click on the Panchayat Name to view Village-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:Panel ID="PanelPanchayat" runat="server" ScrollBars="Auto">
                                <asp:GridView ID="grdPanchayat" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                OnRowCommand="grdPanchayat_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                          <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Panchayat" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayat" runat="server" CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("PanchayatName")+","+Eval("Total")%>'
                                                CommandName="PanchayatClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Village-Wise Application Status"><%# Eval("PanchayatName")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="कुल आवेदन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsTotal" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("Total")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsTotal_Click"><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="पूर्ण प्रविष्टि">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="आंशिक प्रविष्टि">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsUnFinalize" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="प्रारंभिक निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("Nirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsNirast_Click"><%# Eval("Nirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अंतिम निष्पादन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsFinalNirast" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("FinalNirast")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsFinalNirast_Click"><%# Eval("FinalNirast")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="प्रक्रियाधीन">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsPrakriyadhin" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("Prakriyadhin")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsPrakriyadhin_Click"><%# Eval("Prakriyadhin")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अस्वीकृत">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsAshwikrit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("Ashwikrit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsAshwikrit_Click"><%# Eval("Ashwikrit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField  HeaderText="मापी क़े लिए निर्धारित">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsMapi_Nirdharit" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("Mapi_Nirdharit")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsMapi_Nirdharit_Click"><%# Eval("Mapi_Nirdharit")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField  HeaderText="न्यायालय में लंबित" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayats_ki_vaad_sankhya_varsh" runat="server" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode")+","+Eval("CourtInPending")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayats_ki_vaad_sankhya_varsh_Click"><%# Eval("CourtInPending")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
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
                        </asp:Panel> 
                        <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Visible="false">
                        <asp:GridView ID="GridView1"  OnRowDataBound="GridView1_RowDataBound" runat="server" DataKeyNames="a_id"
                        AutoGenerateColumns="false" EnableTheming="false" Width="100%"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  
                        BackColor="White" BorderStyle="None" BorderWidth="0px" CssClass="mGrid" GridLines="None"
                        Style="width: 100%;" HeaderStyle-BackColor="Beige" 
                        ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                <ItemTemplate>
                                   <asp:Label ID="lblslno" runat="server" Text='<%#Eval("slno") %>'></asp:Label>
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
                                     <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                      <%#Eval("DISTRICTNAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Sd_Name_En")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="अंचल <hr style='margin-bottom: 0px; margin-top: 0px;' />थाना "
                                ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%#Eval("BlockName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Police_Station")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="ग्राम पंचायत <hr style='margin-bottom: 0px; margin-top: 0px;' />राजस्व ग्राम<hr style='margin-bottom: 0px; margin-top: 0px;' />वार्ड"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%#Eval("PanchayatName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("VILLNAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
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
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("SarkariBhumiType")%>                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />                                
                            </asp:TemplateField>           
      
  
                            

                            <asp:TemplateField HeaderText="भूमि विवाद का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("BhumiVivad")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="भूमि विवाद की </br> सवेदनशीलता" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Bhumi_savedansheelta")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>    
                            
                            <asp:TemplateField HeaderText="बैठक की तिथि" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Meeting_date")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                     
                            
                            <asp:TemplateField HeaderText="बैठक का निष्कर्ष" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Description")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                             
                            
                            <asp:TemplateField HeaderText="(Action)" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <div id="div_Action" runat="server" class="divclss" >
                                    <%#Eval("disposal")%>
                                        </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  

                                                      
                            
                            <asp:TemplateField HeaderText="विवाद का अद्यतन कारक" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("bhumi_vivad_ka_adyatan_sthiti")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />


                            

                            </asp:TemplateField>
                                      <asp:TemplateField HeaderText="वादी द्वारा प्रस्तुत साक्ष्य" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("Vadi_Khatiyaan")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Vadi_Kevaala")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                      <%#Eval("Vadi_CopyOfJamabandi")%>
                                      <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                               <%#Eval("Vadi_LagaanRaseed")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Vadi_Vanshaavalee")%>
                                      <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                     <%#Eval("Vadi_Batavaara")%>
                                       <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                       <%#Eval("Vadi_Parcha")%>
                                         <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("vadi_nyayaalay_aadesh")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Vadi_Anya_sakshya")%>

                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left"  />
                            </asp:TemplateField>
                                                                                
                            <asp:TemplateField HeaderText="वादी का </br>दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:ImageButton ID="Image6" class="getpdfdoc" Visible='<%# CheckImage(Eval("Vadi_sakshya_File"))%>' path='<%#Eval("Vadi_sakshya_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="प्रतिवादी द्वारा प्रस्तुत साक्ष्य" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                               ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%"  HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <%#Eval("prativadi_Khatiyaan")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("prativadi_Kevaala")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("prativadi_CopyOfJamabandi")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("prativadi_LagaanRaseed")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                     <%#Eval("prativadi_Vanshaavalee")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                     <%#Eval("prativadi_Batavaara")%>
                                     <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                        <%#Eval("prativadi_Parcha")%>
                                        <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                        <%#Eval("prativadi_nyayaalay_aadesh")%>
                                    <hr style='margin-bottom:0px; margin-top:0px; border-color:#c1c1c1;' />
                                    <%#Eval("pratiVadi_Anya_sakshya")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="प्रतिवादी का </br>दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Wrap="false"
                                ItemStyle-Width="10%"  HeaderStyle-Width="10%">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image1" class="getpdfdoc" Visible='<%# CheckImage(Eval("Prativadi_sakshya_File"))%>' path='<%# Eval("Prativadi_sakshya_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="पुलिस पदाधिकारी द्वारा समर्पित </br>जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                <div id="div_pulis_padadhikari_vivarani" runat="server" class="divclss" visible='<%# CheckImage(Eval("pulis_padadhikari_vivarani"))%>'>
                                    <%#Eval("pulis_padadhikari_vivarani")%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="दस्तावेज"  ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image2" Visible='<%# CheckImage(Eval("pulis_padadhikar_Patr_file"))%>' path='<%# Eval("pulis_padadhikar_Patr_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                               
                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित </br>जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="20%">
                                <ItemTemplate>
                                <div id="div_HalkaKarmchari_vivran" runat="server" class="divclss"  visible='<%# CheckImage(Eval("HalkaKarmchari_vivran"))%>'>
                                 
                                    <%#Eval("HalkaKarmchari_vivran")%>
                                  </div>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image3" class="getpdfdoc" Visible='<%# CheckImage(Eval("HalkaKarmchari_Patr_file"))%>' path='<%#Eval("HalkaKarmchari_Patr_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="विवादित भू-खंड मापी का विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                              
                                    <%#Eval("vivadit_bhukhand_Mapi_ki_avashyakta_hai")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("vivadit_bhukhand_Mapi")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <asp:Label ID="Label1" runat="server" Text="माप के लिए निर्धारित तिथि : " Visible='<%# CheckNull(Eval("maapee_ke_lie_nirdhaarit_tithi"))%>' ></asp:Label><%#Eval("maapee_ke_lie_nirdhaarit_tithi","{0:dd/MM/yyyy}")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px;' /> 
                                    <asp:Label ID="Label2" Text="मापी नहीं होने का कारण :" runat="server" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'></asp:Label>                                    
                                    <div id="div_vivaadit_bhukhand_Mapi_Reason" runat="server" class="divclss getpdfdoc"   visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'>                                   
                                    <%#Eval("vivaadit_bhukhand_Mapi_Reason")%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="मापी का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:ImageButton ID="Image4" class="getpdfdoc" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_File"))%>' path='<%#Eval("vivaadit_bhukhand_Mapi_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                               
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>              
                            <asp:TemplateField HeaderText="विवाद का </br>प्राथमिकी/अप्राथमिकी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("bhumi_vivad_Vivran_Available")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="न्यायालय में </br>प्रक्रियाधीननन वाद" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("dispute_in_court_available")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="आवेदन" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image5"  class="getpdfdoc" Visible='<%# CheckNull(Eval("ApplicationFile"))%>' path='<%#Eval("ApplicationFile")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="" Visible="false">
                                     <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" Text='View' CssClass="btn btn-success"
                                                        CommandArgument='<%#Eval("a_id")%>' ForeColor="Blue" Font-Underline="false"
                                                        ToolTip="Click Edit" OnClick="lnkView_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                           <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                             </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>   
                        <%--   <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x: auto; overflow-y: hidden;" Visible="false">
                            <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server" DataKeyNames="a_id"
                                AutoGenerateColumns="False" EnableTheming="False" Width="100%" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="None"
                                Style="width: 100%;" HeaderStyle-BackColor="Beige" 
                                ShowFooter="True" EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333">
                                <AlternatingRowStyle BackColor="White" CssClass="alt" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblslno" runat="server" Text='<%#Eval("slno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Application No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="6%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkApplicationNo" runat="server" CommandArgument='<%#Eval("a_id")%>' Font-Underline="false" ForeColor="Blue" OnClick="lnkView_Click" OnClientClick="openwindow(this);" Text='<%#Eval("ApplicationNo")%>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="कमिश्नरी &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt; जिला &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt; सब डिवीज़न" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("DIVISIONAME")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("DISTRICTNAME")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("Sd_Name_En")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अंचल &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;थाना " ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("BlockName")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("Police_Station")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ग्राम पंचायत &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;राजस्व ग्राम&lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;वार्ड" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%#Eval("PanchayatName")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("VILLNAME")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("WARDNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="वादी का नाम " ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vadi_Name")%>
                                            <br />
                                            <%#Eval("TotalVadi")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="प्रतिवादी का नाम" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_Name")%>
                                            <br />
                                            <%#Eval("TotalPratiVadi")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="भूमि का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Bhumitype")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("SarkariBhumiType")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="भूमि विवाद का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("BhumiVivad")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="भूमि विवाद की &lt;/br&gt; सवेदनशीलता" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Bhumi_savedansheelta")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="बैठक की तिथि" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Meeting_date")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="बैठक का निष्कर्ष" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Description")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="(Action)" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div id="div_Action" runat="server" class="divclss">
                                                <%#Eval("disposal")%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="विवाद का अद्यतन कारक" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("bhumi_vivad_ka_adyatan_sthiti")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="वादी का &lt;/br&gt;दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Image6" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" Visible='<%# CheckImage(Eval("Vadi_sakshya_File"))%>' path='<%#Eval("Vadi_sakshya_File")%>' Style="cursor: pointer" Width="50px" />
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="10%" HeaderStyle-Wrap="false" HeaderText="प्रतिवादी का &lt;/br&gt;दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Image1" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("Prativadi_sakshya_File")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("Prativadi_sakshya_File"))%>' Width="50px" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="पुलिस पदाधिकारी द्वारा समर्पित &lt;/br&gt;जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div id="div_pulis_padadhikari_vivarani" runat="server" class="divclss" visible='<%# CheckNull(Eval("pulis_padadhikari_vivarani"))%>'>
                                                <%#Eval("pulis_padadhikari_vivarani")%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Image2" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("pulis_padadhikar_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("pulis_padadhikar_Patr_file"))%>' Width="50px" />
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित &lt;/br&gt;जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <div id="div_HalkaKarmchari_vivran" runat="server" class="divclss" visible='<%# CheckNull(Eval("HalkaKarmchari_vivran"))%>'>
                                                <%#Eval("HalkaKarmchari_vivran")%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Image3" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("HalkaKarmchari_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("HalkaKarmchari_Patr_file"))%>' Width="50px" />
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="विवादित भू-खंड मापी का विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vivadit_bhukhand_Mapi_ki_avashyakta_hai")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <%#Eval("vivadit_bhukhand_Mapi")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                            <asp:Label ID="Label1" runat="server" Text="माप के लिए निर्धारित तिथि : " Visible='<%# CheckNull(Eval("maapee_ke_lie_nirdhaarit_tithi"))%>'></asp:Label>
                                            <%#Eval("maapee_ke_lie_nirdhaarit_tithi","{0:dd/MM/yyyy}")%>
                                            <hr style='margin-bottom: 0px; margin-top: 0px;' />
                                            <asp:Label ID="Label2" runat="server" Text="मापी नहीं होने का कारण :" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'></asp:Label>
                                            <div id="div_vivaadit_bhukhand_Mapi_Reason" runat="server" class="divclss" visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'>
                                                <%#Eval("vivaadit_bhukhand_Mapi_Reason")%>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="मापी का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Image4" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("vivaadit_bhukhand_Mapi_File")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("vivaadit_bhukhand_Mapi_File"))%>' Width="50px" />
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="विवाद का &lt;/br&gt;प्राथमिकी/अप्राथमिकी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("bhumi_vivad_Vivran_Available")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="न्यायालय में &lt;/br&gt;प्रक्रियाधीननन वाद" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("dispute_in_court_available")%>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="आवेदन" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="Image5" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("ApplicationFile")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("ApplicationFile"))%>' Width="50px" />
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="False" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("a_id")%>' CssClass="btn btn-success" Font-Underline="false" ForeColor="Blue" OnClick="lnkView_Click" Text="View" ToolTip="Click Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" CssClass="pgr" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </asp:Panel>--%>
                    </div>
                </div>
                <div class="row mb-2">
                        <div class="form-group text-center" style="padding-top: 8px; padding-bottom: 8px; border: none; ">
                                        <div class="col-md-12">
                                           <asp:Repeater ID="rptPager" runat="server">
                                              <ItemTemplate>
                                                   <asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' 
                                                        Enabled = '<%#Eval("Enabled")%>' 
                                                        OnClick = "Page_Changed" style="padding:5px; border-radius:5px;color:black;text-decoration:none"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                </div>
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
    <script src="../../../bhusamadhan/js/ruang-admin.min.js"></script>  --%>


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
</asp:Content>
