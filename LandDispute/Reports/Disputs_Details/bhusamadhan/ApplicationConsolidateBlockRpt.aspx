<%@ Page Title="" Language="C#" MasterPageFile="~/LandDispute/Samvida.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="ApplicationConsolidateBlockRpt.aspx.cs" Inherits="LandDispute_Report_ApplicationConsolidateBlockRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">
                             Circle Wise Application Consolidated Report</h4>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <%--<asp:Button ID="btnback" CssClass="btn btn-danger " Text="Back" runat="server" OnClick="btnback_Click" 
                            Visible="false" />--%>
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
                </div>
                 
                   
             
              <div class="row" style="padding:05.px">
        <div class="col-md-2"></div>
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
        </div>
             <div class="row" style="padding:05.px">
          <div class="col-md-2"></div>
                
                <div class="col-md-2 text-center">
                    <asp:DropDownList ID="ddlCommissionary" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlCommissionary_SelectedIndexChanged"  
                         AutoPostBack="True">
                    </asp:DropDownList>
                </div>

                 <div class="col-md-2 text-center">
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>

                <div class="col-md-2 text-center">
                    <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>

                <div class="col-md-2 text-center">
                   <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" Enabled="true" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>

        </div>
             <div class="row" style="padding-top: 0.5px;">
            <div class="col-md-2"></div>    
               
               
                
                
                <div class="col-md-2 text-center">
                    Form Date
                </div>

                <div class="col-md-2 text-center">
                   To Date
                </div>
                
                
            </div>
             <div class="row" style="padding-top: 0.5px;">
            <div class="col-md-2"></div>    
               
               
                
                
                <div class="col-md-2 text-center">
                  
               
                <div class="input-group">
                               <asp:TextBox ID="txtfrmdate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" ></asp:TextBox>
                            

                            <span class="input-group-addon">
                            <rjs:popcalendar ID="popCalendarFrom" runat="server" Control="txtfrmdate" Format="dd mm yyyy" />
                <asp:RequiredFieldValidator runat="server" id="RFVFromDate" ValidationGroup="a" controltovalidate="txtfrmdate" ForeColor="Red" errormessage="*" />
                            </span>
                            </div>
                
                
                
                </div>


                <div class="col-md-2 text-center">
                    <div class="input-group">

                   <asp:TextBox ID="txtTodate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true"></asp:TextBox>
                         <span class="input-group-addon">
                <rjs:popcalendar ID="popCalendarTo" runat="server" Control="txtTodate" Format="dd mm yyyy" />
                <asp:RequiredFieldValidator runat="server" id="RFVToDate" ValidationGroup="a" controltovalidate="txtTodate" ForeColor="Red" errormessage="*" />
                             </span>
                            </div>
                </div>


                 <div class="col-md-2 text-center">
                 <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" 
                        CssClass="btn btn-green"  /></div>
            </div>

                 <div class="row">
                    <div class="col-md-12">

                        
                        <asp:Panel ID="pnlgrid" runat="server">
                            <div class="col-md-3">
                               
                            </div>
                            <asp:Label ID="lblDateTime4" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail5" runat="server" Text="*"
                                ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="griddata" runat="server" AutoGenerateColumns="false" CssClass="table-responsive"  
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                ShowFooter="true" Width="100%" EmptyDataRowStyle-Font-Size="Large" ShowHeaderWhenEmpty="true"
                               >
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
                                    <asp:TemplateField HeaderText="Division " HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDivision" runat="server" CommandArgument='<%# Eval("DIVISIONCODE")+","+Eval("DIVISIONAME")%>'
                                                CommandName="DstClick" ForeColor="Blue" Font-Underline="false" ToolTip=""><%# Eval("DIVISIONAME")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="District" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                            <asp:LinkButton ID="lnkdis" runat="server" CommandArgument='<%# Eval("DISTRICTCODE")+","+Eval("DISTRICTNAME")%>'
                                                CommandName="DstClick" ForeColor="Blue" Font-Underline="false" ToolTip=""><%# Eval("DISTRICTNAME")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Division" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnksubdiv" runat="server" CommandArgument='<%# Eval("Sd_Code2")+","+Eval("Sd_Name_En")%>'
                                                CommandName="SubDivClick" ForeColor="Blue" Font-Underline="false" ToolTip=""><%# Eval("Sd_Name_En")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Circle" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkcir" runat="server" CommandArgument='<%# Eval("BlockCode")+","+Eval("BlockName")%>'
                                                CommandName="cirClick" ForeColor="Blue" Font-Underline="false" ToolTip=""><%# Eval("BlockName")%>
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
                        
                    </div></div>
                  </div> </div></div>
                  
</asp:Content>

