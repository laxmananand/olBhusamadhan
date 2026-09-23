<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" 
    CodeFile="DisputeDivConsolidateRpt.aspx.cs" Inherits="LandDispute_Reports_Disputs_Details_DisputeDivConsolidateRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">     
        .modalBackground {
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
            // urlpdf = ("http://10.133.25.21/ImageServiceHome" + urlpdf).replace(' ', '');
            urlpdf = ("http://localhost:8080" + urlpdf);
            urlpdf = urlpdf.trim();
            $.ajax({
                type: "POST",
                url: "DisputeDivConsolidateRpt.aspx/Getpdf",
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

        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            imgDiv.style.display = "none";

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">
                            Dispute Consolidated Report</h4>
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
                <div class="row" style="padding-top: 0.3px;">
                    <div class="col-md-2">
                        <asp:Button ID="btnback" CssClass="btn btn-danger " Text="Back" runat="server" OnClick="btnback_Click" 
                            Visible="false" />
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                            <asp:TextBox ID="txtFromdate" runat="server" placeholder="From Date" ReadOnly="true" class="form-control w-20" Height="35px"></asp:TextBox>
                            <span class="input-group-addon">
                                <rjs:popcalendar ID="popCalendarFrom" runat="server" Control="txtFromdate" Format="dd mm yyyy" />
                                <%--<asp:RequiredFieldValidator runat="server" id="RFVFromDate" ValidationGroup="a" controltovalidate="txtFromdate" ForeColor="Red" errormessage="*" />--%>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                            <asp:TextBox ID="txTodate" runat="server" placeholder="To Date" ReadOnly="true" class="form-control w-20" Height="35px"></asp:TextBox>
                            <span class="input-group-addon">
                                <rjs:popcalendar ID="popCalendar1" runat="server" Control="txTodate" Format="dd mm yyyy" />
                                <%--<asp:RequiredFieldValidator runat="server" id="RFVToDate" ValidationGroup="a" controltovalidate="txTodate" ForeColor="Red" errormessage="*" />--%>
                            </span>
                        </div>
                    </div>
                    <div class="col-md-2">
                       <asp:Button ID="btnSearch" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="View" OnClick="btnSearch_Click"  />                        
                    </div>
                    <div class="col-md-2">                       
                        <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Export To Excel" OnClick="btnExpToExl_Click" />
                    </div>
                </div> 
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlDivision" runat="server">
                            <asp:Label ID="lblPrintDate" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail" runat="server" Text="*Click on the Division Name to view District-Wise Dispute Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:Panel ID="gvd_Divisionpnl" runat="server" ScrollBars="Both">
                                <asp:GridView ID="grd_Division" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                    HeaderStyle-BorderColor="White" DataKeyNames="DIVISIONCODE,Total" EmptyDataText="No Record(s) found"
                                    OnRowCommand="grd_Division_RowCommand" EmptyDataRowStyle-ForeColor="Red" ShowFooter="True"
                                    Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
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

                                        <asp:TemplateField HeaderText="Division" HeaderStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDivision" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("DIVISIONAME")+","+Eval("Total")%>'
                                                    CommandName="DivClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View District-Wise Application Status"><%# Eval("DIVISIONAME")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTotalDivision" runat="server" OnClick="lnkTotalDivision_Click"
                                                    CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("Total")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("Total")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="पूर्ण प्रविष्टि" HeaderStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDivisionFinalize" runat="server" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("Finalize")%>'
                                                    ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionFinalize_Click"><%# Eval("Finalize")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="आंशिक प्रविष्टि" HeaderStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDivisionUnFinalize" runat="server" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("UnFinalize")%>'
                                                    ForeColor="Blue" Font-Underline="false" OnClick="lnkDivisionUnFinalize_Click"><%# Eval("UnFinalize")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पर्चाधारी के बेदखली का मामला" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkParchadhariBedakli_Division" runat="server" OnClick="lnkParchadhariBedakli_Division_Click"
                                                    CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("ParchadhariBedakli")%>' Font-Underline="false" ForeColor="Blue">
                                                <%# Eval("ParchadhariBedakli")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkSarkariBhumiKabzaDivision" runat="server" OnClick="lnkSarkariBhumiKabzaDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("SarkariBhumiKabza")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("SarkariBhumiKabza")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर सीमांकन या सीमा का विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRaitiBhumiSeemaDivision" runat="server" OnClick="lnkRaitiBhumiSeemaDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("RaitiBhumiSeema")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("RaitiBhumiSeema")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="निजी रास्ता / नाली का विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkNijiRastaNaliDivision" runat="server" OnClick="lnkNijiRastaNaliDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("NijiRastaNali")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("NijiRastaNali")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="जल स्रोत का विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkJalStrotDivision" runat="server" OnClick="lnkJalStrotDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("JalStrot")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("JalStrot")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPatrikBhumiBatwaraDivision" runat="server" OnClick="lnkPatrikBhumiBatwaraDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("PatrikBhumiBatwara")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("PatrikBhumiBatwara")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="खेती से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkKhetiVivadDivision" runat="server" OnClick="lnkKhetiVivadDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("KhetiVivad")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("KhetiVivad")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="वास से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkVaasVivadDivision" runat="server" OnClick="lnkVaasVivadDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("VaasVivad")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("VaasVivad")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="लगान निर्धारण का विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkLaganNirdharanDivision" runat="server" OnClick="lnkLaganNirdharanDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("LaganNirdharan")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("LaganNirdharan")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="व्यावसायिक भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkVyawsaikBhumiDivision" runat="server" OnClick="lnkVyawsaikBhumiDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("VyawsaikBhumi")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("VyawsaikBhumi")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="बदलेन भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBadlenBhumiDivision" runat="server" OnClick="lnkBadlenBhumiDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("BadlenBhumi")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("BadlenBhumi")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- अर्जन से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBhuArjanDivision" runat="server" OnClick="lnkBhuArjanDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("BhuArjan")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("BhuArjan")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- हदबंदी (अधिशेष) से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkBhuHdBandiDivision" runat="server" OnClick="lnkBhuHdBandiDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("BhuHdBandi")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("BhuHdBandi")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRaitiBhumiKabzaDivision" runat="server" OnClick="lnkRaitiBhumiKabzaDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("RaitiBhumiKabza")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("RaitiBhumiKabza")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="4%" HeaderText="अन्य" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkAnyaDivision" runat="server" OnClick="lnkAnyaDivision_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("Anya")%>' Font-Underline="false" ForeColor="Blue"><%# Eval("Anya")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
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
                            <asp:Label ID="lblDateTime1" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail1" runat="server" Text="*Click on the District Name to view Circle-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grd_District" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" OnRowCommand="grd_District_RowCommand"
                                EmptyDataRowStyle-ForeColor="Red" ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                      <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" Width="1%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="District" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrict" runat="server" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("DISTRICTNAME")+","+Eval("Total")%>' CommandName="DstClick" Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View SubDivison-Wise Application Status"><%# Eval("DISTRICTNAME")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTotalDistrict" runat="server" OnClick="lnkTotalDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("DISTRICTCODE")+","+Eval("Total")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="पूर्ण प्रविष्टि" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictFinalize" runat="server" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("DISTRICTCODE")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="आंशिक प्रविष्टि" HeaderStyle-Width="9%">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictUnFinalize" runat="server" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("DISTRICTCODE")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkDistrictUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पर्चाधारी के बेदखली का मामला" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkParchadhariBedakli_District" runat="server" OnClick="lnkParchadhariBedakli_District_Click" 
                                                CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("DISTRICTCODE")+","+Eval("ParchadhariBedakli")%>'  Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("ParchadhariBedakli")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSarkariBhumiKabzaDistrict" runat="server" OnClick="lnkSarkariBhumiKabzaDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("DISTRICTCODE")+","+Eval("SarkariBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SarkariBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर सीमांकन या सीमा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiSeemaDistrict" runat="server" OnClick="lnkRaitiBhumiSeemaDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("DISTRICTCODE")+","+Eval("RaitiBhumiSeema")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiSeema")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="निजी रास्ता / नाली का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkNijiRastaNaliDistrict" runat="server" OnClick="lnkNijiRastaNaliDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+Eval("DISTRICTCODE")+","+Eval("NijiRastaNali")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("NijiRastaNali")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                  
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="जल स्रोत का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkJalStrotDistrict" runat="server" OnClick="lnkJalStrotDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("JalStrot")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("JalStrot")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPatrikBhumiBatwaraDistrict" runat="server" OnClick="lnkPatrikBhumiBatwaraDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("PatrikBhumiBatwara")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("PatrikBhumiBatwara")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="खेती से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkKhetiVivadDistrict" runat="server" OnClick="lnkKhetiVivadDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("KhetiVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("KhetiVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                              
                                      <asp:TemplateField HeaderStyle-Width="4%"  HeaderText="वास से संबंधित विवाद"  ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVaasVivadDistrict" runat="server" OnClick="lnkVaasVivadDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("VaasVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VaasVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="लगान निर्धारण का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkLaganNirdharanDistrict" runat="server" OnClick="lnkLaganNirdharanDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("LaganNirdharan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("LaganNirdharan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                 
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="व्यावसायिक भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVyawsaikBhumiDistrict" runat="server" OnClick="lnkVyawsaikBhumiDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("VyawsaikBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VyawsaikBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                   
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="बदलेन भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBadlenBhumiDistrict" runat="server" OnClick="lnkBadlenBhumiDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("BadlenBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BadlenBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- अर्जन से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuArjanDistrict" runat="server" OnClick="lnkBhuArjanDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("BhuArjan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuArjan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- हदबंदी (अधिशेष) से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuHdBandiDistrict" runat="server" OnClick="lnkBhuHdBandiDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("BhuHdBandi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuHdBandi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiKabzaDistrict" runat="server" OnClick="lnkRaitiBhumiKabzaDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("RaitiBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
 
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="अन्य" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAnyaDistrict" runat="server" OnClick="lnkAnyaDistrict_Click" CommandArgument='<%#Eval("DIVISIONCODE")+","+ Eval("DISTRICTCODE")+","+Eval("Anya")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Anya")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
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

                        <asp:Panel ID="pnlSubDivision" runat="server">
                            <asp:Label ID="lblDateTime2" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail3" runat="server" Text="*Click on the SubDivision Name to view Circle/Block-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grdSubDivision" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" OnRowCommand="grdSubDivision_RowCommand"
                                EmptyDataRowStyle-ForeColor="Red" ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" Width="1%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SubDivision" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivision" runat="server" 
                                                CommandArgument='<%# Eval("Sd_Code2")+","+Eval("Sd_Name_En")+","+Eval("Total")%>'
                                                CommandName="sdClick" Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View SubDivison-Wise Application Status"><%# Eval("Sd_Name_En")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTotalSubDivision" runat="server" OnClick="lnkTotalSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Total")%>' 
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="पूर्ण प्रविष्टि" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionFinalize" runat="server" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="आंशिक प्रविष्टि" HeaderStyle-Width="9%">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSubDivisionUnFinalize" runat="server" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkSubDivisionUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पर्चाधारी के बेदखली का मामला" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkParchadhariBedakli_SubDivision" runat="server" OnClick="lnkParchadhariBedakli_SubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("ParchadhariBedakli")%>'  
                                                Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("ParchadhariBedakli")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSarkariBhumiKabzaSubDivision" runat="server" OnClick="lnkSarkariBhumiKabzaSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("SarkariBhumiKabza")%>'  
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("SarkariBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर सीमांकन या सीमा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiSeemaSubDivision" runat="server" OnClick="lnkRaitiBhumiSeemaSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("RaitiBhumiSeema")%>' 
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiSeema")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="निजी रास्ता / नाली का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkNijiRastaNaliSubDivision" runat="server" OnClick="lnkNijiRastaNaliSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("NijiRastaNali")%>' 
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("NijiRastaNali")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                  
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="जल स्रोत का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkJalStrotSubDivision" runat="server" OnClick="lnkJalStrotSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("JalStrot")%>' 
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("JalStrot")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPatrikBhumiBatwaraSubDivision" runat="server" OnClick="lnkPatrikBhumiBatwaraSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("PatrikBhumiBatwara")%>' 
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("PatrikBhumiBatwara")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="खेती से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkKhetiVivadSubDivision" runat="server" OnClick="lnkKhetiVivadSubDivision_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("KhetiVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("KhetiVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                              
                                      <asp:TemplateField HeaderStyle-Width="4%"  HeaderText="वास से संबंधित विवाद"  ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVaasVivadSubDivision" runat="server" OnClick="lnkVaasVivadSubDivision_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("VaasVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VaasVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="लगान निर्धारण का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkLaganNirdharanSubDivision" runat="server" OnClick="lnkLaganNirdharanSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("LaganNirdharan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("LaganNirdharan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                 
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="व्यावसायिक भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVyawsaikBhumiSubDivision" runat="server" OnClick="lnkVyawsaikBhumiSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("VyawsaikBhumi")%>'  
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("VyawsaikBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                   
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="बदलेन भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBadlenBhumiSubDivision" runat="server" OnClick="lnkBadlenBhumiSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("BadlenBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BadlenBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- अर्जन से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuArjanSubDivision" runat="server" OnClick="lnkBhuArjanSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("BhuArjan")%>' 
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuArjan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- हदबंदी (अधिशेष) से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuHdBandiSubDivision" runat="server" OnClick="lnkBhuHdBandiSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("BhuHdBandi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuHdBandi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiKabzaSubDivision" runat="server" OnClick="lnkRaitiBhumiKabzaSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("RaitiBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
 
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="अन्य" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAnyaSubDivision" runat="server" OnClick="lnkAnyaSubDivision_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sd_Code2")+","+Eval("Anya")%>'  
                                                Font-Underline="false" ForeColor="Blue" ><%# Eval("Anya")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
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

                        <asp:Panel ID="pnlCircle" runat="server">
                            <div class="col-md-3">
                            </div>
                            <asp:Label ID="lblDateTime3" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail4" runat="server" Text="*Click on the Block/Circle Name to view PoliceStation-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grdCircle" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" OnRowCommand="grdCircle_RowCommand"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" Width="1%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Circle/Block" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCircle" runat="server" CommandArgument='<%# Eval("BlockCode")+","+Eval("BlockName")+","+Eval("Total")%>' CommandName="CircleClick" Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View PoliceStation-Wise Application Status"><%# Eval("BlockName")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                   
                                     <asp:TemplateField HeaderStyle-Width="4%" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTotalBlock" runat="server" OnClick="lnkTotalBlock_Click" CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Total")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>


                                      <asp:TemplateField HeaderText="पूर्ण प्रविष्टि" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockFinalize" runat="server" CommandArgument='<%#  Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>
                                  
                                      <asp:TemplateField HeaderText="आंशिक प्रविष्टि" HeaderStyle-Width="9%">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBlockUnFinalize" runat="server" CommandArgument='<%#  Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkBlockUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पर्चाधारी के बेदखली का मामला" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkParchadhariBedakli_Block" runat="server" OnClick="lnkParchadhariBedakli_Block_Click" 
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("ParchadhariBedakli")%>'  Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("ParchadhariBedakli")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSarkariBhumiKabzaBlock" runat="server" OnClick="lnkSarkariBhumiKabzaBlock_Click" 
                                                CommandArgument='<%#  Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("SarkariBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SarkariBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर सीमांकन या सीमा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiSeemaBlock" runat="server" OnClick="lnkRaitiBhumiSeemaBlock_Click"
                                                CommandArgument='<%#  Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("RaitiBhumiSeema")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiSeema")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="निजी रास्ता / नाली का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkNijiRastaNaliBlock" runat="server" OnClick="lnkNijiRastaNaliBlock_Click"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("NijiRastaNali")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("NijiRastaNali")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="जल स्रोत का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkJalStrotBlock" runat="server" OnClick="lnkJalStrotBlock_Click" 
                                                CommandArgument='<%#  Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("JalStrot")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("JalStrot")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPatrikBhumiBatwaraBlock" runat="server" OnClick="lnkPatrikBhumiBatwaraBlock_Click"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("PatrikBhumiBatwara")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("PatrikBhumiBatwara")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="खेती से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkKhetiVivadBlock" runat="server" OnClick="lnkKhetiVivadBlock_Click"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("KhetiVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("KhetiVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                              
                                    <asp:TemplateField HeaderStyle-Width="4%"  HeaderText="वास से संबंधित विवाद"  ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVaasVivadBlock" runat="server" OnClick="lnkVaasVivadBlock_Click" 
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("VaasVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VaasVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="लगान निर्धारण का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkLaganNirdharanBlock" runat="server" OnClick="lnkLaganNirdharanBlock_Click"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("LaganNirdharan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("LaganNirdharan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                 
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="व्यावसायिक भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVyawsaikBhumiBlock" runat="server" OnClick="lnkVyawsaikBhumiBlock_Click" 
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("VyawsaikBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VyawsaikBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="बदलेन भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBadlenBhumiBlock" runat="server" OnClick="lnkBadlenBhumiBlock_Click" 
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("BadlenBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BadlenBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- अर्जन से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuArjanBlock" runat="server" OnClick="lnkBhuArjanBlock_Click"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("BhuArjan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuArjan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- हदबंदी (अधिशेष) से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuHdBandiBlock" runat="server" OnClick="lnkBhuHdBandiBlock_Click" 
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("BhuHdBandi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuHdBandi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiKabzaBlock" runat="server" OnClick="lnkRaitiBhumiKabzaBlock_Click"
                                                CommandArgument='<%#  Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("RaitiBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
 
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="अन्य" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAnyaBlock" runat="server" OnClick="lnkAnyaBlock_Click"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("BlockCode")+","+Eval("Anya")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Anya")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
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

                        <asp:Panel ID="pnlthana" runat="server">
                            <div class="col-md-3">
                            </div>
                            <asp:Label ID="lblDateTime4" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail5" runat="server" Text="*Click on the PoliceStation Name to view Panchayat-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grdThana" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                OnRowCommand="grdThana_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="1%" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Thana Name" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkThana" runat="server" CommandArgument='<%# Eval("PS_Code")+","+Eval("ThanaName")+","+Eval("Total")%>'
                                                CommandName="ThanaClick" Font-Underline="false" ForeColor="Blue"
                                                ToolTip="Click here To View PanchayatWise Application Status"><%# Eval("ThanaName")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>
                                   
                                     <asp:TemplateField HeaderStyle-Width="4%" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTotalThana" runat="server" OnClick="lnkTotalThana_Click" 
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("Total")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                       <asp:TemplateField HeaderText="पूर्ण प्रविष्टि" HeaderStyle-Width="9%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkThanaFinalize" runat="server"
                                                    CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("Finalize")%>'
                                                     ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaFinalize_Click"><%# Eval("Finalize")%>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                                Font-Size="Small" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                HorizontalAlign="left" />
                                        </asp:TemplateField>

                                     <asp:TemplateField HeaderText="आंशिक प्रविष्टि" HeaderStyle-Width="9%">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkThanaUnFinalize" runat="server"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkThanaUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पर्चाधारी के बेदखली का मामला" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkParchadhariBedakli_Thana" runat="server" OnClick="lnkParchadhariBedakli_Thana_Click" 
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("ParchadhariBedakli")%>'  Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("ParchadhariBedakli")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSarkariBhumiKabzaThana" runat="server" OnClick="lnkSarkariBhumiKabzaThana_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("SarkariBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SarkariBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर सीमांकन या सीमा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiSeemaThana" runat="server" OnClick="lnkRaitiBhumiSeemaThana_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("RaitiBhumiSeema")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiSeema")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="निजी रास्ता / नाली का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkNijiRastaNaliThana" runat="server" OnClick="lnkNijiRastaNaliThana_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("NijiRastaNali")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("NijiRastaNali")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="जल स्रोत का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkJalStrotThana" runat="server" OnClick="lnkJalStrotThana_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("JalStrot")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("JalStrot")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPatrikBhumiBatwaraThana" runat="server" OnClick="lnkPatrikBhumiBatwaraThana_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("PatrikBhumiBatwara")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("PatrikBhumiBatwara")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="खेती से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkKhetiVivadThana" runat="server" OnClick="lnkKhetiVivadThana_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("KhetiVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("KhetiVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                              
                                    <asp:TemplateField HeaderStyle-Width="4%"  HeaderText="वास से संबंधित विवाद"  ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVaasVivadThana" runat="server" OnClick="lnkVaasVivadThana_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("VaasVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VaasVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="लगान निर्धारण का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkLaganNirdharanThana" runat="server" OnClick="lnkLaganNirdharanThana_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("LaganNirdharan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("LaganNirdharan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                 
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="व्यावसायिक भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVyawsaikBhumiThana" runat="server" OnClick="lnkVyawsaikBhumiThana_Click"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("VyawsaikBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VyawsaikBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="बदलेन भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBadlenBhumiThana" runat="server" OnClick="lnkBadlenBhumiThana_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("BadlenBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BadlenBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- अर्जन से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuArjanThana" runat="server" OnClick="lnkBhuArjanThana_Click" 
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("BhuArjan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuArjan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- हदबंदी (अधिशेष) से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuHdBandiThana" runat="server" OnClick="lnkBhuHdBandiThana_Click"
                                                CommandArgument='<%# Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("PS_Code")+","+Eval("BhuHdBandi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuHdBandi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiKabzaThana" runat="server" OnClick="lnkRaitiBhumiKabzaThana_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("RaitiBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
 
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="अन्य" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAnyaThana" runat="server" OnClick="lnkAnyaThana_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+ Eval("PS_Code")+","+Eval("Anya")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Anya")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
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

                        <asp:Panel ID="pnlpanchayat" runat="server">
                            <asp:Label ID="lblDateTime5" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail6" runat="server" Text="*Click on the Panchayat Name to view Village-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grdPanchayat" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                OnRowCommand="grdPanchayat_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" >
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="1%" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Panchayat Name" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCircle" runat="server"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+Eval("PanchayatCode") + ","+Eval("PanchayatName")+","+Eval("Total")%>'
                                                CommandName="PanchayatClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View PanchayatWise Application Status"><%# Eval("PanchayatName")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTotalpanchayat" runat="server" OnClick="lnkTotalpanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("Total")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="पूर्ण प्रविष्टि" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsFinalize" runat="server" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("Finalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="आंशिक प्रविष्टि" HeaderStyle-Width="9%">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayatsUnFinalize" runat="server" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("UnFinalize")%>'
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkPanchayatsUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पर्चाधारी के बेदखली का मामला" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkParchadhariBedakli_panchayat" runat="server" OnClick="lnkParchadhariBedakli_panchayat_Click" 
                                              CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("ParchadhariBedakli")%>'  Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("ParchadhariBedakli")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSarkariBhumiKabzapanchayat" runat="server" OnClick="lnkSarkariBhumiKabzapanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("SarkariBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SarkariBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर सीमांकन या सीमा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiSeemapanchayat" runat="server" OnClick="lnkRaitiBhumiSeemapanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("RaitiBhumiSeema")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiSeema")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="निजी रास्ता / नाली का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkNijiRastaNalipanchayat" runat="server" OnClick="lnkNijiRastaNalipanchayat_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("NijiRastaNali")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("NijiRastaNali")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="जल स्रोत का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkJalStrotpanchayat" runat="server" OnClick="lnkJalStrotpanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("JalStrot")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("JalStrot")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPatrikBhumiBatwarapanchayat" runat="server" OnClick="lnkPatrikBhumiBatwarapanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("PatrikBhumiBatwara")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("PatrikBhumiBatwara")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="खेती से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkKhetiVivadpanchayat" runat="server" OnClick="lnkKhetiVivadpanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("KhetiVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("KhetiVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                              
                                    <asp:TemplateField HeaderStyle-Width="4%"  HeaderText="वास से संबंधित विवाद"  ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVaasVivadpanchayat" runat="server" OnClick="lnkVaasVivadpanchayat_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("VaasVivad")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VaasVivad")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="लगान निर्धारण का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkLaganNirdharanpanchayat" runat="server" OnClick="lnkLaganNirdharanpanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("LaganNirdharan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("LaganNirdharan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                 
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="व्यावसायिक भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVyawsaikBhumipanchayat" runat="server" OnClick="lnkVyawsaikBhumipanchayat_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("VyawsaikBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("VyawsaikBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="बदलेन भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBadlenBhumipanchayat" runat="server" OnClick="lnkBadlenBhumipanchayat_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("BadlenBhumi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BadlenBhumi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- अर्जन से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuArjanpanchayat" runat="server" OnClick="lnkBhuArjanpanchayat_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("BhuArjan")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuArjan")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="भू- हदबंदी (अधिशेष) से संबंधित विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkBhuHdBandipanchayat" runat="server" OnClick="lnkBhuHdBandipanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("BhuHdBandi")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("BhuHdBandi")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="रैयती भूमि पर कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRaitiBhumiKabzapanchayat" runat="server" OnClick="lnkRaitiBhumiKabzapanchayat_Click"
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("RaitiBhumiKabza")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("RaitiBhumiKabza")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
 
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="अन्य" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkAnyapanchayat" runat="server" OnClick="lnkAnyapanchayat_Click" 
                                                CommandArgument='<%#Eval("Comm_Code")+","+Eval("District_Code")+","+Eval("Sub_DivCode")+","+Eval("Block_Code")+","+Eval("Thana_code")+","+ Eval("PanchayatCode")+","+Eval("Anya")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Anya")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
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
                       <%-- <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Visible="false">
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
                                <asp:ImageButton ID="Image6" Visible='<%# CheckNull(Eval("Vadi_sakshya_File"))%>' path='<%#Eval("Vadi_sakshya_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                 
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
                                 <asp:ImageButton ID="Image1" Visible='<%# CheckNull(Eval("Prativadi_sakshya_File"))%>' path='<%# Eval("Prativadi_sakshya_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="पुलिस पदाधिकारी द्वारा समर्पित </br>जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                <div id="div_pulis_padadhikari_vivarani" runat="server" class="divclss" visible='<%# CheckNull(Eval("pulis_padadhikari_vivarani"))%>'>
                                    <%#Eval("pulis_padadhikari_vivarani")%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="दस्तावेज"  ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image2" Visible='<%# CheckNull(Eval("pulis_padadhikar_Patr_file"))%>' path='<%# Eval("pulis_padadhikar_Patr_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                               
                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित </br>जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="20%">
                                <ItemTemplate>
                                <div id="div_HalkaKarmchari_vivran" runat="server" class="divclss"  visible='<%# CheckNull(Eval("HalkaKarmchari_vivran"))%>'>
                                 
                                    <%#Eval("HalkaKarmchari_vivran")%>
                                  </div>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image3" Visible='<%# CheckNull(Eval("HalkaKarmchari_Patr_file"))%>' path='<%#Eval("HalkaKarmchari_Patr_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                
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
                                    <div id="div_vivaadit_bhukhand_Mapi_Reason" runat="server"  class="divclss" visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'>                                   
                                    <%#Eval("vivaadit_bhukhand_Mapi_Reason")%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="मापी का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:ImageButton ID="Image4" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_File"))%>' path='<%#Eval("vivaadit_bhukhand_Mapi_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                               
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
                                 <asp:ImageButton ID="Image5" Visible='<%# CheckNull(Eval("ApplicationFile"))%>' path='<%#Eval("ApplicationFile")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                  
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
                    
           
                </asp:Panel>  --%> 
                        <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x: auto; overflow-y: hidden;" Visible="false">
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
                        </asp:Panel>

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
   <%--  <script src="../../../bhusamadhan/vendor/jquery/jquery.min.js"></script>
    <script src="../../../bhusamadhan/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../../../bhusamadhan/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../../../bhusamadhan/vendor/chart.js/Chart.min.js"></script>
    <script src="../../../bhusamadhan/js/demo/chart-area-demo.js"></script>
    <script src="../../../bhusamadhan/vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <script src="../../../bhusamadhan/js/ruang-admin.min.js"></script> --%>
</asp:Content>