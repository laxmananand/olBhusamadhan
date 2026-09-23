<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" CodeFile="RemarksDistConsolidateRpt.aspx.cs" Inherits="LandDispute_Reports_Disputs_Details_DisputeDistConsolidateRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/animate.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />   
    <script src="../assets/js/jquery.min.js" type="text/javascript"></script>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
     <div class="container-fluid">      
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">
                            District Wise Remarks Consolidated Report</h4>
                    </div>
                </div>                   
                <div class="row">
                      <div class="col-md-3"></div>
                       <div class="col-md-7">                       
                         <span style="text-align:center">
                       <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" style="text-align: center; font-weight: bold;"></asp:Label>
                       </span>
                    </div>
                       <div class="col-md-3"></div>
                  </div>
                 <br/>
                 <div class="row">
                    
                      <div class="col-md-2"></div>
                     <div class="col-md-2">Form-Date</div>
                     <div class="col-md-2">To-Date</div>
                      <div class="col-md-2"></div>
                 </div>
                <div class="row">
                   
                      <div class="col-md-2">
                        <asp:Button ID="btnback" CssClass="form-control btn btn-danger " Text="Back" runat="server" OnClick="btnback_Click" 
                            Visible="false" />
                    </div>
                      <div class="col-md-2">
                        <div class="input-group">
                            <asp:TextBox ID="txtFromdate" runat="server" placeholder="From Date" ReadOnly="true" class="form-control w-20" Height="35px"></asp:TextBox>
                            <span class="input-group-addon">
                                <rjs:popcalendar ID="popCalendarFrom" runat="server" Control="txtFromdate" Format="dd mm yyyy" />
                                <%--<asp:RequiredFieldValidator runat="server" id="RFVFromDate" ValidationGroup="a" controltovalidate="txtFromdate" ForeColor="Red" errormessage="*" />--%>
                            </span>
                        </div>
                    </div>
                      <div class="col-md-2">
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
                            Text="Export To Excel" OnClick="btnExpToExl_Click"  />
                    </div>
                 </div>
                <br/>
               
          
                <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlDist" runat="server">                           
                            <asp:Label ID="lblDateTime1" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail1" runat="server" Text="*Click on the District Name to view Circle-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grd_District" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" OnRowCommand="grd_District_RowCommand"
                                EmptyDataRowStyle-ForeColor="Red" ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="DISTRICTCODE,Total">
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
                                            <asp:LinkButton ID="lnkTotalDistrict" runat="server" OnClick="lnkTotalDistrict_Click" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("Total")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                  
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DMOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDMOPT_District" runat="server" OnClick="lnkDMOPT_District_Click" 
                                                CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("DMOPT")%>'  Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("DMOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                  
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SSPOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSSPOPTDistrict" runat="server" OnClick="lnkSSPOPTDistrict_Click" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("SSPOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SSPOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="ADM" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkADMDistrict" runat="server" OnClick="lnkADMDistrict_Click" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("ADM")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("ADM")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DPGRO" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDPGRODistrict" runat="server" OnClick="lnkDPGRODistrict_Click" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("DPGRO")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("DPGRO")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                  
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SDOOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSDOOPTDistrict" runat="server" OnClick="lnkSDOOPTDistrict_Click" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("SDOOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SDOOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DSPOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDSPOPTDistrict" runat="server" OnClick="lnkDSPOPTDistrict_Click" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("DSPOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("DSPOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SDPGRO" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSDPGRODistrict" runat="server" OnClick="lnkSDPGRODistrict_Click" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("SDPGRO")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SDPGRO")%>
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
                            <asp:Label ID="lblDateTime2" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail2" runat="server" Text="*Click on the Block/Circle Name to view PoliceStation-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grdCircle" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" OnRowCommand="grdCircle_RowCommand"
                                ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Total">
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
                                            <asp:LinkButton ID="lnkTotalBlock" runat="server" OnClick="lnkTotalBlock_Click" CommandArgument='<%# Eval("BlockCode")+","+Eval("Total")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DMOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDMOPT_Block" runat="server" OnClick="lnkDMOPT_Block_Click" 
                                                CommandArgument='<%# Eval("BlockCode")+","+Eval("DMOPT")%>'  Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("DMOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SSPOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSSPOPTBlock" runat="server" OnClick="lnkSSPOPTBlock_Click" CommandArgument='<%# Eval("BlockCode")+","+Eval("SSPOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SSPOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="ADM" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkADMBlock" runat="server" OnClick="lnkADMBlock_Click" CommandArgument='<%# Eval("BlockCode")+","+Eval("ADM")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("ADM")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DPGRO" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDPGROBlock" runat="server" OnClick="lnkDPGROBlock_Click" CommandArgument='<%# Eval("BlockCode")+","+Eval("DPGRO")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("DPGRO")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SDOOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSDOOPTBlock" runat="server" OnClick="lnkSDOOPTBlock_Click" CommandArgument='<%# Eval("BlockCode")+","+Eval("SDOOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SDOOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DSPOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDSPOPTBlock" runat="server" OnClick="lnkDSPOPTBlock_Click" CommandArgument='<%# Eval("BlockCode")+","+Eval("DSPOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("DSPOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SDPGRO" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSDPGROBlock" runat="server" OnClick="lnkSDPGROBlock_Click" CommandArgument='<%# Eval("BlockCode")+","+Eval("SDPGRO")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SDPGRO")%>
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
                            <asp:Label ID="lblDateTime3" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail3" runat="server" Text="*Click on the PoliceStation Name to view Panchayat-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                             <asp:GridView ID="grdThana" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                OnRowCommand="grdThana_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Total">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small"  />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="1%" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                             />
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
                                            <asp:LinkButton ID="lnkTotalThana" runat="server" OnClick="lnkTotalThana_Click" CommandArgument='<%# Eval("PS_Code")+","+Eval("Total")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DMOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDMOPT_Thana" runat="server" OnClick="lnkDMOPT_Thana_Click" 
                                                CommandArgument='<%# Eval("PS_Code")+","+Eval("DMOPT")%>'  Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("DMOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SSPOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSSPOPTThana" runat="server" OnClick="lnkSSPOPTThana_Click" CommandArgument='<%# Eval("PS_Code")+","+Eval("SSPOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SSPOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="ADM" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkADMThana" runat="server" OnClick="lnkADMThana_Click" CommandArgument='<%# Eval("PS_Code")+","+Eval("ADM")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("ADM")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DPGRO" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDPGROThana" runat="server" OnClick="lnkDPGROThana_Click" CommandArgument='<%# Eval("PS_Code")+","+Eval("DPGRO")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("DPGRO")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SDOOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSDOOPTThana" runat="server" OnClick="lnkSDOOPTThana_Click" CommandArgument='<%# Eval("PS_Code")+","+Eval("SDOOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SDOOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DSPOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDSPOPTThana" runat="server" OnClick="lnkDSPOPTThana_Click" CommandArgument='<%# Eval("PS_Code")+","+Eval("DSPOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("DSPOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SDPGRO" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSDPGROThana" runat="server" OnClick="lnkSDPGROThana_Click" CommandArgument='<%# Eval("PS_Code")+","+Eval("SDPGRO")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SDPGRO")%>
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
                            <asp:Label ID="lblDateTime4" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail4" runat="server" Text="*Click on the Panchayat Name to view Village-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grdPanchayat" runat="server" AutoGenerateColumns="False" CssClass="table-responsive table-bordered"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                OnRowCommand="grdPanchayat_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Total">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small"  />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="1%" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Panchayat Name" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPanchayat" runat="server" CommandArgument='<%# Eval("PanchayatCode") +","+ Eval("PanchayatName")+","+Eval("Total") %>'
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
                                            <asp:LinkButton ID="lnkTotalpanchayat" runat="server" OnClick="lnkTotalpanchayat_Click" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("Total")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("Total")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DMOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDMOPT_panchayat" runat="server" OnClick="lnkDMOPT_panchayat_Click" 
                                                CommandArgument='<%# Eval("PanchayatCode")+","+Eval("DMOPT")%>'  Font-Underline="false" ForeColor="Blue" >
                                                <%# Eval("DMOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SSPOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSSPOPTpanchayat" runat="server" OnClick="lnkSSPOPTpanchayat_Click" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("SSPOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SSPOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="ADM" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkADMpanchayat" runat="server" OnClick="lnkADMpanchayat_Click" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("ADM")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("ADM")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DPGRO" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDPGROpanchayat" runat="server" OnClick="lnkDPGROpanchayat_Click" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("DPGRO")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("DPGRO")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SDOOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSDOOPTpanchayat" runat="server" OnClick="lnkSDOOPTpanchayat_Click" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("SDOOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SDOOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="DSPOPT" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDSPOPTpanchayat" runat="server" OnClick="lnkDSPOPTpanchayat_Click" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("DSPOPT")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("DSPOPT")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderStyle-Width="4%" HeaderText="SDPGRO" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkSDPGROpanchayat" runat="server" OnClick="lnkSDPGROpanchayat_Click" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("SDPGRO")%>'  Font-Underline="false" ForeColor="Blue" ><%# Eval("SDPGRO")%>
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
                        <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Visible="true">
                                  <asp:GridView ID="GridView1"  OnRowDataBound="GridView1_RowDataBound" runat="server" DataKeyNames="a_id"
                        AutoGenerateColumns="False" EnableTheming="False" Width="100%"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="None"
                        Style="width: 100%;" HeaderStyle-BackColor="Beige" 
                        ShowFooter="True" EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333">
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

                            <asp:TemplateField HeaderText="वादी का नाम " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                     <%#Eval("vadi_Name")%><br/><%#Eval("TotalVadi")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="प्रतिवादी का नाम" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                     <%#Eval("pratiVadi_Name")%><br/><%#Eval("TotalPratiVadi")%>
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
</asp:Content>