<%@ Page Title="" Language="C#" MasterPageFile="~/LandDispute/Samvida.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="ApplicationDistConsolidateRpt.aspx.cs" Inherits="LandDispute_Report_ApplicationDistConsolidateRpt" %>
 <%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/animate.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <script src="../assets/js/jquery.min.js" type="text/javascript"></script>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
     <div class="container-fluid">
          <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">
                             District Wise Application Consolidated Report</h4>
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
                        <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-orange"
                            Text="Export To Excel" OnClick="btnExpToExl_Click"  />
                    </div>


                      
                      <div class="col-md-3">
                      <div class="input-group">
                               <asp:TextBox ID="txtFromdate" CssClass="form-control" placeholder="From Date" runat="server" ></asp:TextBox>
                            

                            <span class="input-group-addon">
                             <rjs:PopCalendar ID="PopCalendar4" runat="server" Control="txtFromdate" To-Today="false"
                                                    Format="dd MM yyyy" ></rjs:PopCalendar>
                                                <rjs:PopCalendarMessageContainer ID="PopCalendarMessageContainer4" runat="server" 
                                                    Calendar="PopCalendar1" />
                            </span>
                            </div>

                        </div>

                      <div class="col-md-3">
                      <div class="input-group">
                               <asp:TextBox ID="txTodate" CssClass="form-control" placeholder="To Date"  runat="server"></asp:TextBox>
                            

                            <span class="input-group-addon">
                            <rjs:PopCalendar ID="PopCalendar1" runat="server" Control="txTodate" Format="dd MM yyyy"
                                                    To-Today="false"></rjs:PopCalendar>
                                                <rjs:PopCalendarMessageContainer ID="PopCalendarMessageContainer1" runat="server"
                                                    Calendar="PopCalendar1" />
                            </span>
                            </div>

                        </div>


                    

                       <div class="col-md-3">
                       <asp:Button ID="btnSearch" Style="float: right;" runat="server" class="form-control btn btn-orange"
                            Text="View" OnClick="btnSearch_Click"  />
                        
                    </div>
                </div>
                 <div class="row">
                    <div class="col-md-12">
<asp:Panel ID="pnlDist" runat="server">
                            <asp:Label ID="lblPrintDate" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail" runat="server" Text="*Click on the District Name to view Circle/Block-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grd_District" runat="server" AutoGenerateColumns="false" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" DataKeyNames="DistCode" EmptyDataText="No Record(s) found"  
          OnRowCommand="grd_District_RowCommand" EmptyDataRowStyle-ForeColor="Red" ShowFooter="true"
                                Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivision" runat="server" CommandArgument='<%# Eval("DistCode")+","+Eval("DistName")%>'
                                                CommandName="DstClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Circle-Wise Application Status"><%# Eval("DistName")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Total" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>

                                    


                                     <asp:BoundField DataField="Nirast" HeaderText="प्रारंभिक निष्पादन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>



                                     <asp:BoundField DataField="FinalNirast" HeaderText="अंतिम निष्पादन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>




                                     <asp:BoundField DataField="Prakriyadhin" HeaderText="प्रक्रियाधीन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Ashwikrit" HeaderText="अस्वीकृत" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mapi_Nirdharit" HeaderText="मापी क़े लिए निर्धारित" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlCircle" runat="server">
                            <div class="col-md-3">
                               
                            </div>
                            <asp:Label ID="lblDateTime3" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail4" runat="server" Text="*Click on the Block/Circle Name to view PoliceStation-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grdCircle" runat="server" AutoGenerateColumns="false"
                                CssClass="table-responsive" OnRowCommand="grdCircle_RowCommand"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" 
                                EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="true" Width="100%" EmptyDataRowStyle-Font-Size="Large" 
                                ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" Width="1%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Circle/Block" HeaderStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkCircle" runat="server" CommandArgument='<%# Eval("BlockCode")+","+Eval("BlockName")%>'
                                                CommandName="CircleClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View PoliceStation-Wise Application Status"><%# Eval("BlockName")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Total" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>

                                     <asp:BoundField DataField="Nirast" HeaderText="प्रारंभिक निष्पादन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>



                                     <asp:BoundField DataField="FinalNirast" HeaderText="अंतिम निष्पादन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>


                                     <asp:BoundField DataField="Prakriyadhin" HeaderText="प्रक्रियाधीनन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Ashwikrit" HeaderText="अस्वीकृत" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mapi_Nirdharit" HeaderText="मापी क़े लिए निर्धारित" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="pnlthana" runat="server">
                            <div class="col-md-3">
                               
                            </div>
                            <asp:Label ID="lblDateTime4" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail5" runat="server" Text="*Click on the thana Name to view Panchayat-Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="grdThana" runat="server" AutoGenerateColumns="false" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="true" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="true"
                               OnRowCommand="grdThana_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" Width="4%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Center" Width="4%" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Center" Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PoliceStation" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkthana" runat="server" CommandArgument='<%# Eval("PS_Code")+","+Eval("PoliceStation")%>'
                                                CommandName="ThanaClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View PanchayatWise-Wise Application Status"><%# Eval("PoliceStation")%>
                                            </asp:LinkButton>                                           
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Total" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Nirast" HeaderText="प्रारंभिक निष्पादन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>



                                     <asp:BoundField DataField="FinalNirast" HeaderText="अंतिम निष्पादन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>


                                     <asp:BoundField DataField="Prakriyadhin" HeaderText="प्रक्रियाधीनन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Ashwikrit" HeaderText="अस्वीकृत" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mapi_Nirdharit" HeaderText="मापी क़े लिए निर्धारित" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                            <asp:Panel ID="Panel_Panchayats" runat="server">
                                <div class="col-md-3">
                                 </div>
                                <asp:Label ID="lblDateTime5" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail6" runat="server" Text="*Click on the thana Name to To View Wise Application Status"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                                 <asp:GridView ID="grdPanchayats" runat="server" AutoGenerateColumns="false" CssClass="table-responsive"
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="true" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="true"
                              OnRowCommand="grdPanchayats_RowCommand" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" Width="4%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Center" Width="4%" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Center" Width="4%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Panchayat Name" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkthana" runat="server" CommandArgument='<%# Eval("PanchayatCode")+","+Eval("PanchayatName")%>'
                                                CommandName="PanchayatClick" ForeColor="Blue" Font-Underline="false" ToolTip="Click here To View Wise Application Status"><%# Eval("PanchayatName")%>
                                            </asp:LinkButton>                                           
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:BoundField DataField="Total" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>

                                     


                                     <asp:BoundField DataField="Nirast" HeaderText="प्रारंभिक निष्पादन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>



                                     <asp:BoundField DataField="FinalNirast" HeaderText="अंतिम निष्पादन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>



                                     <asp:BoundField DataField="Prakriyadhin" HeaderText="प्रक्रियाधीनन" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="Ashwikrit" HeaderText="अस्वीकृत" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Mapi_Nirdharit" HeaderText="मापी क़े लिए निर्धारित" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Font-Bold="true" Font-Names="Arial Unicode MS" Font-Size="Small" Width="2%"
                                            BackColor="#1C6794" ForeColor="White" />
                                        <ItemStyle Font-Size="Small" HorizontalAlign="Right" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White"
                                            HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
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
                    </div></div>
                </div></div></div>
</asp:Content>

