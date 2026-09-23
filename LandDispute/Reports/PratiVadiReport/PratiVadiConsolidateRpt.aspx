<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" 
    enableEventValidation ="false"
     CodeFile="PratiVadiConsolidateRpt.aspx.cs" 
    Inherits="LandDispute_Reports_PratiVadi_Report_PratiVadiConsolidateRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                   <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">
                            Prativadi Consolidated Report</h4>
                    </div>
                </div>
                   <div class="row">
                    <div class="col-md-3">
                        <asp:Button ID="btnback" CssClass="btn btn-danger " Text="Back" runat="server" OnClick="btnback_Click"
                            Visible="false" />
                    </div>
                    <div class="col-md-6">
                       
                         <span style="text-align:center">
                       <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" style="text-align: center; font-weight: bold;"></asp:Label>
                       </span>
                    </div>
                    <div class="col-md-3">
                        <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Export To Excel" OnClick="btnExpToExl_Click" />
                    </div>
                </div>
                   <div class="row">
                        <div class="col-md-12">
                                <asp:Panel ID="pnlDist" runat="server">
                                <asp:Label ID="lblPrintDateForDivision" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                                <asp:Label ID="lblDetailForDivision" runat="server" Text="*Click on the Divion Name to view District-Wise"
                                    ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                         <asp:Panel ID="PanelDivision" runat="server" ScrollBars="Auto">
                                     <asp:GridView ID="grd_Division" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                    HeaderStyle-BorderColor="White" DataKeyNames="DIVISIONCODE" EmptyDataText="No Record(s) found"
                                    OnRowCommand="grd_Division_RowCommand" EmptyDataRowStyle-ForeColor="Red" ShowFooter="True"
                                    Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="1%" HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                            <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                            <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Division" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDivision" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("DIVISIONAME")%>' CommandName="DivisionClick" 
                                                    Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View District-Wise"><%# Eval("DIVISIONAME")%>
                                                </asp:LinkButton>
                                                <asp:HiddenField ID="hfDivisionCode" runat="server"  Value='<%#Eval("DIVISIONCODE") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                            <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                            <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                 <asp:Label ID="lblDivisionTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                            <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                            <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="3%" HeaderText="General Prativadi" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivisionGeneral_Vadi" runat="server" Text='<%#Eval("General_Vadi")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                            <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                            <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Vibhag Prativadi" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDivisionVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>'
                                                    CommandArgument='<%#Eval("Vibhag_Vadi")%>' CommandName="Division_Vibhag_Vadi_Click" 
                                                    Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View data Vibhag-Vadi-Wise">
                                                </asp:LinkButton> 
                                                 <asp:Label ID="lblDivisionVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>' Visible="false"></asp:Label>                                      
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                            <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                            <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />                                     
                                         </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Institute" ItemStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDivisionInstitute" runat="server" CommandArgument='<%# Eval("institute")%>' CommandName="Division_InstituteClick" 
                                                    Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Raajasv_nyaayaalay-Wise"  Text='<%#Eval("institute")%>'>
                                                </asp:LinkButton> 
                                                 <asp:Label ID="lblDivisionInstitute" runat="server" Text='<%#Eval("institute")%>' Visible="false"></asp:Label>                                        
                                            </ItemTemplate>
                                            <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                            <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
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
                                </asp:panel>
                              </asp:panel>                       
                                <br/>
                                <asp:Panel ID="PanelVibhag_Vadi" runat="server" ScrollBars="Auto" Visible="false">
                                     <asp:GridView ID="grdVibhag_Vadi" runat="server" 
                                               AutoGenerateColumns="False" CssClass="table-responsive"
                                               HeaderStyle-BorderColor="White"  EmptyDataText="No Record(s) found"
                                               EmptyDataRowStyle-ForeColor ="Red"   ShowFooter="True"  ForeColor="#333333" GridLines="None"                                                              
                                               Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True" 
                                               CellPadding ="4" > 
                                          <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>                                                                 
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Department Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblDepartment_Name" runat="server" Text='<%#Eval("name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblDepartmentNameTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
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

                                <asp:Panel ID="PanelInstitute" runat="server" ScrollBars="Auto" Visible="false">
                                     <asp:GridView ID="grvInstitute" runat="server" 
                                               AutoGenerateColumns="False" CssClass="table-responsive"
                                               HeaderStyle-BorderColor="White"  EmptyDataText="No Record(s) found"
                                               EmptyDataRowStyle-ForeColo ="Red"   ShowFooter="True"  ForeColor="#333333" GridLines="None"                                                                      
                                               Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True" 
                                               CellPadding ="4" > 
                                         <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>                                                                          
                                        <Columns>
                                          <asp:TemplateField HeaderStyle-Width="1%" HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Institute Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblInstitute_Name" runat="server" Text='<%#Eval("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblInstitute_NameTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
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

                                <asp:Panel ID="pnlDistrict" runat="server">                          
                                    <asp:Label ID="lblPrintDateForDistrict" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                                    <asp:Label ID="lblDetailForDistrict" runat="server" Text="*Click on the District Name to view SubDivision-Wise Application Status"
                                        ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                    <asp:Panel ID="PanelDistrict" runat="server" ScrollBars="Auto">
                                           <asp:GridView ID="grdDistrict" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                        HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" OnRowCommand="grdDistrict_RowCommand"
                                        EmptyDataRowStyle-ForeColor="Red" ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large"
                                        ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="District" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDistrict" runat="server" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("DISTRICTNAME")%>' CommandName="DistrictClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View SubDivision-Wise"><%# Eval("DISTRICTNAME")%>
                                                    </asp:LinkButton>
                                                      <asp:HiddenField ID="hfDistrictCode" runat="server"  Value='<%#Eval("DISTRICTCODE") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblDistrictTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="General Prativadi" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDistrictGeneral_Vadi" runat="server" Text='<%#Eval("General_Vadi")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Vibhag Prativadi" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDistrictVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>'
                                                        CommandArgument='<%#Eval("Vibhag_Vadi")%>' CommandName="District_Vibhag_Vadi_Click" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View data Vibhag-Vadi-Wise">
                                                    </asp:LinkButton>  
                                                     <asp:Label ID="lblDistrictVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>' Visible="false"></asp:Label>                                                  
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />                                     
                                             </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Institute" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDistrictInstitute" runat="server" CommandArgument='<%# Eval("institute")%>' CommandName="District_InstituteClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Raajasv_nyaayaalay-Wise"  Text='<%#Eval("institute")%>'>
                                                    </asp:LinkButton>   
                                                     <asp:Label ID="lblDistrictInstitute" runat="server" Text='<%#Eval("institute")%>' Visible="false"></asp:Label>                                                  
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
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
                                            <asp:TemplateField HeaderStyle-Width="1%" HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Sub-Division" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSubDivision" runat="server" CommandArgument='<%# Eval("Sd_Code2")+","+Eval("Sd_Name_En")%>' CommandName="SubDivisionClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Block-Wise"><%# Eval("Sd_Name_En")%>
                                                    </asp:LinkButton>
                                                     <asp:HiddenField ID="hfSubDivision" runat="server"  Value='<%#Eval("Sd_Code2") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblSubDivisionTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="General Prativadi" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSubDivisionGeneral_Vadi" runat="server" Text='<%#Eval("General_Vadi")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Vibhag Prativadi" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSubDivisionVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>'
                                                        CommandArgument='<%#Eval("Vibhag_Vadi")%>' CommandName="SubDivision_Vibhag_Vadi_Click" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View data Vibhag-Vadi-Wise">
                                                    </asp:LinkButton>  
                                                    <asp:Label ID="lblSubDivisionVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>' Visible="false"></asp:Label>                                                  
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />                                     
                                             </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Institute" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkSubDivisionInstitute" runat="server" CommandArgument='<%# Eval("institute")%>' CommandName="SubDivision_InstituteClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Raajasv_nyaayaalay-Wise"  Text='<%#Eval("institute")%>'>
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblSubDivisioninstitute" runat="server" Text='<%#Eval("institute")%>' Visible="false"></asp:Label>                                                 
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
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
                                </asp:Panel>

                                <asp:Panel ID="pnlCircle" runat="server">                      
                                    <asp:Label ID="lblPrintDateForCircle" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                                    <asp:Label ID="lblDetailForCircle" runat="server" Text="*Click on the Block/Circle Name to view PoliceStation-Wise Application Status"
                                        ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                    <asp:Panel ID="PanelCircle" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="grdCircle" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                        HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                        ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" OnRowCommand="grdCircle_RowCommand"
                                        ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Block" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBlock" runat="server" CommandArgument='<%# Eval("BlockCode")+","+Eval("BlockName")%>' CommandName="BlockClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View PoliceStation-Wise"><%# Eval("BlockName")%>
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hfBlockCode" runat="server"  Value='<%#Eval("BlockCode") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblBlockTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="General Prativadi" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBlockGeneral_Vadi" runat="server" Text='<%#Eval("General_Vadi")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Vibhag Prativadi" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBlockVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>'
                                                        CommandArgument='<%#Eval("Vibhag_Vadi")%>' CommandName="Block_Vibhag_Vadi_Click" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View data Vibhag-Vadi-Wise">
                                                    </asp:LinkButton>  
                                                     <asp:Label ID="lblBlockVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>' Visible="false"></asp:Label>                                               
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />                                     
                                             </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Institute" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkBlockInstitute" runat="server" CommandArgument='<%# Eval("institute")%>' CommandName="Block_InstituteClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Institute-Wise"  Text='<%#Eval("institute")%>'>
                                                    </asp:LinkButton>  
                                                    <asp:Label ID="lblBlockInstitute" runat="server" Text='<%#Eval("institute")%>' Visible="false"></asp:Label>                                               
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
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
                                </asp:Panel>

                                <asp:Panel ID="pnlthana" runat="server">                          
                                    <asp:Label ID="lblPrintDateForPoliceStation" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                                    <asp:Label ID="lblDetailForPoliceStation" runat="server" Text="*Click on the PoliceStation Name to view Panchayat-Wise Application Status"
                                        ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                    <asp:Panel ID="PanelThana" runat="server" ScrollBars="Auto">
                                           <asp:GridView ID="grdThana" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                        HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                        ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                        OnRowCommand="grdThana_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Width="1%" HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Police Station" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkThana" runat="server" CommandArgument='<%# Eval("PS_Code")+","+Eval("Police_Station")%>' CommandName="PoliceStationClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Panchayat-Wise"><%# Eval("Police_Station")%>
                                                    </asp:LinkButton>
                                                    <asp:HiddenField ID="hfThanaCode" runat="server"  Value='<%#Eval("PS_Code") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblThanaTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="General Prativadi" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblThanaGeneral_Vadi" runat="server" Text='<%#Eval("General_Vadi")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Vibhag Prativadi" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkThanaVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>'
                                                        CommandArgument='<%#Eval("Vibhag_Vadi")%>' CommandName="Thana_Vibhag_Vadi_Click" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View data Vibhag-Vadi-Wise">
                                                    </asp:LinkButton>   
                                                    <asp:Label ID="lblThanaVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>' Visible="false"></asp:Label>                                             
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />                                     
                                             </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Institute" ItemStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkThanaInstitute" runat="server" CommandArgument='<%# Eval("institute")%>'
                                                         CommandName="Thana_InstituteClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Institute-Wise"  Text='<%#Eval("institute")%>'>
                                                    </asp:LinkButton> 
                                                      <asp:Label ID="lblThanaInstitute" runat="server" Text='<%#Eval("institute")%>' Visible="false"></asp:Label>                                               
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
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
                                </asp:Panel>

                                <asp:Panel ID="pnlpanchayat" runat="server">
                                    <asp:Label ID="lblPrintDateForPanchayat" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                                    <asp:Label ID="lblDetailForPanchayat" runat="server" Text="*Click on the Panchayat Name to view Village-Wise Application Status"
                                        ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                    <asp:Panel ID="PanelPanchayat" runat="server" ScrollBars="Auto">
                                             <asp:GridView ID="grdPanchayat" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                        HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                        ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                        OnRowCommand="grdPanchayat_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                                  <asp:TemplateField HeaderStyle-Width="1%" HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                                  <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Panchayat" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPanchayat" runat="server" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("PanchayatName")%>' CommandName="PanchayatClick" 
                                                            Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Viilage-Wise"><%# Eval("PanchayatName")%>
                                                        </asp:LinkButton>
                                                         <asp:HiddenField ID="hfPanchayatCode" runat="server"  Value='<%#Eval("PanchayatCode") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                    <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                    <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                                  </asp:TemplateField>                                       

                                                  <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Total" ItemStyle-HorizontalAlign="Left">
                                                  <ItemTemplate>
                                                     <asp:Label ID="lblPanchayatTotal" runat="server" Text='<%#Eval("Total")%>'></asp:Label>
                                                  </ItemTemplate>
                                                  <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                  <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                  <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                                  </asp:TemplateField>

                                                  <asp:TemplateField HeaderStyle-Width="3%" HeaderText="General Prativadi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPanchayatGeneral_Vadi" runat="server" Text='<%#Eval("General_Vadi")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                    <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Black"/>
                                                    <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                                  </asp:TemplateField>

                                                  <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Vibhag Prativadi" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPanchayatVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>'
                                                            CommandArgument='<%#Eval("Vibhag_Vadi")%>' CommandName="Panchayat_Vibhag_Vadi_Click" 
                                                            Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View data Vibhag-Vadi-Wise">
                                                        </asp:LinkButton>  
                                                         <asp:Label ID="lblPanchayatVibhag_Vadi" runat="server" Text='<%#Eval("Vibhag_Vadi")%>' Visible="false"></asp:Label>                                                     
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                    <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
                                                    <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />                                     
                                                 </asp:TemplateField>

                                                  <asp:TemplateField HeaderStyle-Width="3%" HeaderText="Institute" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkPanchayatInstitute" runat="server" CommandArgument='<%# Eval("institute")%>' CommandName="Panchayat_InstituteClick" 
                                                            Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View Institute-Wise"  Text='<%#Eval("institute")%>'>
                                                        </asp:LinkButton> 
                                                         <asp:Label ID="lblPanchayatInstitute" runat="server" Text='<%#Eval("institute")%>' Visible="false"></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                                    <ItemStyle Font-Size="Medium" HorizontalAlign="Left" foreColor="Blue"/>
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
                                </asp:Panel>  
                                              
                                <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Visible="false">
                                <asp:GridView ID="GridView1"  OnRowDataBound="GridView1_RowDataBound" runat="server" DataKeyNames="a_id"
                                AutoGenerateColumns="false" EnableTheming="false" Width="100%"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt"  
                                BackColor="White" BorderStyle="None" BorderWidth="0px" CssClass="mGrid" GridLines="None"
                                AllowPaging="true" PageSize="25" Style="width: 100%;" HeaderStyle-BackColor="Beige" OnPageIndexChanging="GridView1_PageIndexChanging" 
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
    <script src="../../../bhusamadhan/js/ruang-admin.min.js"></script>--%>
</asp:Content>

