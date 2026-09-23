<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="ApplicationConsolidateBlockRpt.aspx.cs" Inherits="LandDispute_Report_ApplicationConsolidateBlockRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../vendors/font-awesome.min.css" rel="stylesheet" />
    <link href="vendors/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/ruang-admin.min.css" rel="stylesheet" />
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <script src="../vendor/jquery/jquery.min.js"></script>
    <script src="../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../vendor/chart.js/Chart.min.js"></script>
    <script src="../js/demo/chart-area-demo.js"></script>
    <script src="../vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <script src="../js/ruang-admin.min.js"></script>


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
                             Circle Wise Application Consolidated Report</h4>
                    </div>
                </div>  
                
                <div class="row">
                    <div class="col-md-10"></div>
                    <div class="col-md-2">
                     <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="btn btn-outline-danger"
                            Text="Export To Excel" /><%--OnClick="btnExpToExl_Click" --%>
                 </div>
                </div>            
                   
             <br />
         <div class="row">
        <div class="col-md-2">
            <asp:Label ID="Label1" runat="server" Text="Commissionary"></asp:Label>
            <asp:DropDownList ID="ddlCommissionary" runat="server" CssClass="form-control" Enabled="true"  
                         AutoPostBack="True"><%--OnSelectedIndexChanged="ddlCommissionary_SelectedIndexChanged"--%> 
                    </asp:DropDownList>
                </div>
                 <div class="col-md-2">
                     <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                      <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="true" 
                        AutoPostBack="True"><%--OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"--%> 
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="Sub-Division"></asp:Label>
                    <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control" Enabled="true" 
                        AutoPostBack="True"><%--OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged" --%>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" Text="Circle"></asp:Label>
                    <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" Enabled="true" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>
             <div class="col-md-2">
                   <asp:Label ID="Label5" runat="server" Text="Form Date"></asp:Label>
                   <asp:TextBox ID="txtfrmdate" runat="server" placeholder="Select Date" ReadOnly="true" cssClass="form-control"></asp:TextBox>
                <rjs:popcalendar ID="popCalendarFrom" runat="server" Control="txtfrmdate" Format="dd mm yyyy" />
                <asp:RequiredFieldValidator runat="server" id="RFVFromDate" ValidationGroup="a" controltovalidate="txtfrmdate" ForeColor="Red" errormessage="*" />
                </div>
             <div class="col-md-2">
                    <asp:Label ID="Label6" runat="server" Text="To Date"></asp:Label>
                    <asp:TextBox ID="txtTodate" runat="server" placeholder="Select Date" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                <rjs:popcalendar ID="popCalendarTo" runat="server" Control="txtTodate" Format="dd mm yyyy" />
                <asp:RequiredFieldValidator runat="server" id="RFVToDate" ValidationGroup="a" controltovalidate="txtTodate" ForeColor="Red" errormessage="*" />
                </div>
        </div>
                <br />

             <div class="row">
                 <div class="col-md-4"></div>
                 <div class="col-md-4">
                 <center><asp:Button ID="btnSearch" runat="server" Text="Search" 
                        CssClass="btn btn-outline-primary"  /></div><%--OnClick="btnSearch_Click" --%>
                 </center>
                 <div class="col-md-4"></div>
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
                                            <asp:LinkButton ID="lnkdis" runat="server" CommandArgument='<%# Eval("DistCode")+","+Eval("DistName")%>'
                                                CommandName="DstClick" ForeColor="Blue" Font-Underline="false" ToolTip=""><%# Eval("DistName")%>
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

                                     <asp:BoundField DataField="Nirast" HeaderText="निस्तारित" ItemStyle-HorizontalAlign="Center">
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
      <br />            
</asp:Content>

