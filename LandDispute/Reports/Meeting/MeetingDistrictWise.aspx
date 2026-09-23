<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
     CodeFile="MeetingDistrictWise.aspx.cs" EnableEventValidation="false" 
    Inherits="LandDispute_Reports_Meeting_Report_MeetingDistrictWise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="../../../bhusamadhan/vendor/fontawesome-free-6.1.1/css/all.min.css" rel="stylesheet" />
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
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                    <div class="row">
                     <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">
                            Meeting District Consolidated Report</h4>
                    </div>
                </div>
                    <br/>
                   <div class="row">
                          <div class="col-md-3">

                          </div>
                          <div class="col-md-6">                       
                                 <span style="text-align:center">
                               <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" style="text-align: center; font-weight: bold;"></asp:Label>
                               </span>
                         </div>
                         <div class="col-md-3">

                          </div>
                   </div>
                  <br/>
                  <br/>
                   <div class="row">
                    <div class="col-md-1"></div>                   
                    <div class="col-md-2">
                        <br/>
                        <asp:Button ID="btnback" CssClass="form-control btn btn-danger" Text="Back" runat="server" OnClick="btnback_Click"
                            Visible="false" />
                    </div>
                    <div class="col-md-2 text-center" >
                                Form Date<br/>
                                <div class="input-group">
                                   <asp:TextBox ID="txtfrmdate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control" ></asp:TextBox>
                                    <span class="input-group-btn">
                                        <rjs:popcalendar ID="popCalendarFrom" runat="server" Control="txtfrmdate" Format="dd mm yyyy" />
                                         <asp:RequiredFieldValidator runat="server" id="RFVFromDate" ValidationGroup="a" controltovalidate="txtfrmdate" ForeColor="Red" errormessage="*" />
                                    </span>
                               </div>
                         </div>
                    <div class="col-md-2 text-center" >
                                To Date<br/>
                                  <div class="input-group">
                                     <asp:TextBox ID="txtTodate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control"></asp:TextBox>
                                     <span class="input-group-btn">
                                         <rjs:popcalendar ID="popCalendarTo" runat="server" Control="txtTodate" Format="dd mm yyyy" />
                                         <asp:RequiredFieldValidator runat="server" id="RFVToDate" ValidationGroup="a" controltovalidate="txtTodate" ForeColor="Red" errormessage="*" />
                                     </span>
                                </div>
                          </div>
                    <div class="col-md-2">
                         <br/>
                        <asp:Button ID="btnSearch" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Search" OnClick="btnSearch_Click" />
                    </div>
                    <div class="col-md-2">
                          <br/>
                        <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Export To Excel" OnClick="btnExpToExl_Click" />
                    </div>
                    
                </div>
                   <div class="row">
                        <div class="col-md-12">
                                <asp:Panel ID="pnlDistrict" runat="server" Visible="false">                           
                                    <asp:Label ID="lblPrintDateForDistrict" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                                    <asp:Label ID="lblDetailForDistrict" runat="server" Text="*Click on the District Name to view SubDivision-Wise Application Status"
                                        ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                       <asp:Panel ID="panel2" runat="server" ScrollBars="Auto">
                                                        <asp:GridView ID="grdDistrict" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                                        HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" OnRowCommand="grdDistrict_RowCommand"
                                                        EmptyDataRowStyle-ForeColor="Red" ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large"
                                                        ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                                 <ItemTemplate>
                                                     <%# Container.DataItemIndex+1 %>
                                                 </ItemTemplate>
                                                 <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" Width="1%" />
                                                 <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                                 <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                             </asp:TemplateField>

                                                            <asp:TemplateField HeaderStyle-Width="4%" HeaderText="District" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDistrict" runat="server" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("DISTRICTNAME")%>' CommandName="DstClick" 
                                                        Font-Underline="false" ForeColor="Blue" ToolTip="Click here To View SubDivison-Wise"><%# Eval("DISTRICTNAME")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                                                <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total application" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Left">
                                      <ItemTemplate>
                                         <asp:Label ID="lblTotal_application" runat="server" Text='<%# Eval("Total_application")%>' style="color: #1c1c1e;"></asp:Label>                            
                                      </ItemTemplate>
                                      <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left"  ForeColor="Black"/>
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                      </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total Meeting" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                       <ItemTemplate>
                                           <asp:Label ID="lblTotal_Meeting" runat="server" Text='<%# Eval("Total_Meeting")%>'></asp:Label>                            
                                       </ItemTemplate>
                                       <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                         Font-Size="Small" />
                                       <ItemStyle Font-Size="Medium" HorizontalAlign="Left"  ForeColor="Black" />
                                       <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
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
                         
                                <asp:Panel ID="pnlCircle" runat="server" Visible="false">                           
                                    <asp:Label ID="lblPrintDateForCircle" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                                    <asp:Label ID="lblDetailFroCircle" runat="server" Text="*Click on the Block/Circle Name to view PoliceStation-Wise Application Status"
                                        ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                     <asp:Panel ID="panel4" runat="server" ScrollBars="Auto">
                                                 <asp:GridView ID="grdCircle" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                        HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                        ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" OnRowCommand="grdCircle_RowCommand"
                                        ShowHeaderWhenEmpty="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                         <Columns>
                                                                <asp:TemplateField>
                                                            <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                          <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" Width="1%" />
                                                          <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="1%" />
                                                          <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" />
                                                 </asp:TemplateField>
                                                     
                                                                <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Circle" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCircle" runat="server" CommandArgument='<%# Eval("BlockCode")+","+Eval("BlockName")%>' 
                                                        CommandName="BlockClick" Font-Underline="false" 
                                                        ForeColor="Blue" ToolTip="Click here To View Circle/Block-Wise">
                                                        <%# Eval("BlockName")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                                                <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                                     
                                                                <asp:TemplateField HeaderText="Total application" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Left">
                                      <ItemTemplate>
                                         <asp:Label ID="lblTotal_application" runat="server" Text='<%# Eval("Total_application")%>' style="color: #1c1c1e;"></asp:Label>                            
                                      </ItemTemplate>
                                      <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left"  ForeColor="Black"/>
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                      </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Total Meeting" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                       <ItemTemplate>
                                           <asp:Label ID="lblTotal_Meeting" runat="server" Text='<%# Eval("Total_Meeting")%>'></asp:Label>                            
                                       </ItemTemplate>
                                       <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                         Font-Size="Small" />
                                       <ItemStyle Font-Size="Medium" HorizontalAlign="Left"  ForeColor="Black" />
                                       <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Left" />
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

                                <asp:Panel ID="pnlthana" runat="server" Visible="false">                          
                                    <asp:Label ID="lblPrintDateForThana" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                                    <asp:Label ID="lblDetailForThana" runat="server" Text="*Click on the PoliceStation Name to view Panchayat-Wise Application Status"
                                        ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                     <asp:Panel ID="panel5" runat="server" ScrollBars="Auto">
                                                  <asp:GridView ID="grdThana" runat="server" AutoGenerateColumns="False" CssClass="table-responsive"
                                        HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                        ShowFooter="True" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="True"
                                        OnRowCommand="grdThana_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                                 <asp:TemplateField>
                                                            <ItemTemplate>
                                                                        <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                          <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" Width="1%" />
                                                          <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="1%" />
                                                          <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" />
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderStyle-Width="4%" HeaderText="Thana" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkCircle" runat="server" CommandArgument='<%# Eval("PS_Code")+","+Eval("Police_Station")%>' 
                                                        CommandName="ThanaClick" Font-Underline="false" 
                                                        ForeColor="Blue" ToolTip="Click here To View Circle/Block-Wise">
                                                        <%# Eval("Police_Station")%>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle BackColor="#1C6794" Font-Bold="True" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" HorizontalAlign="Center" />
                                                <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White" HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Total application" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Left">
                                      <ItemTemplate>
                                         <asp:Label ID="lblTotal_application" runat="server" Text='<%# Eval("Total_application")%>' style="color: #1c1c1e;"></asp:Label>                            
                                      </ItemTemplate>
                                      <HeaderStyle BackColor="#1C6794" Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" ForeColor="White" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left"  ForeColor="Black"/>
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                      </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Total Meeting" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblTotal_Meeting" runat="server" Text='<%# Eval("Total_Meeting")%>'></asp:Label>                            
                                                   </ItemTemplate>
                                                   <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                                     Font-Size="Small" />
                                                   <ItemStyle Font-Size="Medium" HorizontalAlign="Left"  ForeColor="Black" />
                                                   <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                                        HorizontalAlign="Left" />
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
                         </div>
                   </div>
            </div>
        </div>
    </div>
     <%--<script src="../../../bhusamadhan/vendor/jquery/jquery.min.js"></script>
    <script src="../../../bhusamadhan/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../../../bhusamadhan/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../../../bhusamadhan/vendor/chart.js/Chart.min.js"></script>
    <script src="../../../bhusamadhan/js/demo/chart-area-demo.js"></script>
    <script src="../../../bhusamadhan/vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <script src="../../../bhusamadhan/js/ruang-admin.min.js"></script> --%>
</asp:Content>

