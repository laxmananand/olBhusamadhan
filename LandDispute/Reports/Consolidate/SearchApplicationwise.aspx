<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="SearchApplicationwise.aspx.cs" Inherits="LandDispute_Reports_Consolidate_SearchApplicationwise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .myfont_size {
            font-size: medium !important;
        }
    </style>
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">Application Wise Consolidated Report</h4>
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
                        </span>
                    </div>
                </div>
                <br>

                <div class="row" style="padding: 05.px">
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

                    <div class="col-md-2 text-center"></div>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row" style="padding: 05.px">

                            <div class="col-md-1 text-center"></div>
                            <div class="col-md-2 text-center" runat="server" id="divddlRange">
                                <asp:DropDownList ID="ddlRange" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlRange_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center" runat="server" id="divddlCommissionary">
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
                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center">
                            </div>

                            <div class="col-md-2 text-center"></div>

                        </div>

                        <br />
                        <div class="row" style="padding-top: 0.5px;">
                            <div class="col-md-1"></div>
                            <div class="col-md-2 text-center">
                                Thana
                            </div>
                            <div class="col-md-2 text-center">
                                Form Date
                            </div>

                            <div class="col-md-2 text-center">
                                To Date
                            </div>

                            <div class="col-md-2 text-center">
                                <asp:Label ID="Label9" runat="server" Text="Page Size"></asp:Label>
                            </div>

                        </div>

                        <div class="row" style="padding-top: 0.5px;">
                            <div class="col-md-1"></div>
                            <div class="col-md-2 text-center">
                                <asp:DropDownList ID="ddlThana" runat="server" CssClass="form-control" Enabled="true"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2 text-center">
                                <div class="input-group">
                                    <asp:TextBox ID="txtfrmdate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <rjs:PopCalendar ID="popCalendarFrom" runat="server" Control="txtfrmdate" Format="dd mm yyyy" />
                                        <asp:RequiredFieldValidator runat="server" ID="RFVFromDate" ValidationGroup="a" ControlToValidate="txtfrmdate" ForeColor="Red" ErrorMessage="*" />
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2 text-center">
                                <div class="input-group">
                                    <asp:TextBox ID="txtTodate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <rjs:PopCalendar ID="popCalendarTo" runat="server" Control="txtTodate" Format="dd mm yyyy" />
                                        <asp:RequiredFieldValidator runat="server" ID="RFVToDate" ValidationGroup="a" ControlToValidate="txtTodate" ForeColor="Red" ErrorMessage="*" />
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-2  text-center">
                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control mb-2"
                                    Enabled="true">
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="70">70</asp:ListItem>
                                    <asp:ListItem Value="130">130</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <div class="row" style="padding-top: 0.5px;">
                    <div class="col-md-1"></div>
                    <div class="col-md-2  text-center">
                        Search By
                    </div>
                    <div class="col-md-2  text-center" id="divlblsearchType" runat="server" visible="false">
                        <asp:Label ID="lblsearchType" runat="server" Text="sfd"></asp:Label>
                    </div>
                </div>

                <div class="row" style="padding-top: 0.5px;">
                    <div class="col-md-1"></div>
                    <div class="col-md-2 text-center">
                        <asp:DropDownList ID="ddlSearchby" runat="server" CssClass="form-control" Enabled="true"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlSearchby_SelectedIndexChanged">
                            <asp:ListItem Value="0">--All--</asp:ListItem>
                            <asp:ListItem Value="1">Application No.</asp:ListItem>
                            <asp:ListItem Value="2">Vadi Name</asp:ListItem>
                            <asp:ListItem Value="3">Prativadi Name</asp:ListItem>
                            <asp:ListItem Value="4">Vadi Mobile Number</asp:ListItem>
                            <asp:ListItem Value="5">Prativadi Mobile Number</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 text-center" id="divtxtsearchType" runat="server" visible="false">
                        <asp:TextBox ID="txtsearch" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2 text-center">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                            CssClass="form-control btn btn-primary" />
                    </div>
                    <div class="col-md-2 text-center">
                        <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Export To Excel" OnClick="btnExpToExl_Click" />
                    </div>
                </div>

                <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="row" style="padding: 05.px">
                            <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x: auto; overflow-y: hidden;" Visible="true">
                                <asp:GridView ID="GridView1" OnRowDataBound="GridView1_RowDataBound" runat="server" DataKeyNames="a_id"
                                    AutoGenerateColumns="False" EnableTheming="False" Width="100%" AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="None" Style="width: 100%;" HeaderStyle-BackColor="Beige"
                                    ShowFooter="True" EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333">
                                    <AlternatingRowStyle BackColor="White" CssClass="alt" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%#Eval("slno")%>
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
                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="वादी द्वारा प्रस्तुत साक्ष्य" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%#Eval("Vadi_Khatiyaan")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("Vadi_Kevaala")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("Vadi_CopyOfJamabandi")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("Vadi_LagaanRaseed")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("Vadi_Vanshaavalee")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("Vadi_Batavaara")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("Vadi_Parcha")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("vadi_nyayaalay_aadesh")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("Vadi_Anya_sakshya")%>
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="वादी का &lt;/br&gt;दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="Image6" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("Vadi_sakshya_File")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("Vadi_sakshya_File"))%>' Width="50px" />
                                            </ItemTemplate>
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderStyle-Wrap="false" HeaderText="प्रतिवादी द्वारा प्रस्तुत साक्ष्य" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <%#Eval("prativadi_Khatiyaan")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("prativadi_Kevaala")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("prativadi_CopyOfJamabandi")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("prativadi_LagaanRaseed")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("prativadi_Vanshaavalee")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("prativadi_Batavaara")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("prativadi_Parcha")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("prativadi_nyayaalay_aadesh")%>
                                                <hr style='margin-bottom: 0px; margin-top: 0px; border-color: #c1c1c1;' />
                                                <%#Eval("pratiVadi_Anya_sakshya")%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="10%" Wrap="False" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="10%" HeaderStyle-Wrap="false" HeaderText="प्रतिवादी का &lt;/br&gt;दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="Image1" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("Prativadi_sakshya_File")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("Prativadi_sakshya_File"))%>' Width="50px" />
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
                                                <asp:ImageButton ID="Image2" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("pulis_padadhikar_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("pulis_padadhikar_Patr_file"))%>' Width="50px" />
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
                                                <asp:ImageButton ID="Image3" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("HalkaKarmchari_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("HalkaKarmchari_Patr_file"))%>' Width="50px" />
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
                                                <asp:ImageButton ID="Image4" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("vivaadit_bhukhand_Mapi_File")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_File"))%>' Width="50px" />
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
                                                <asp:ImageButton ID="Image5" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("ApplicationFile")%>' Style="cursor: pointer" Visible='<%# CheckNull(Eval("ApplicationFile"))%>' Width="50px" />
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
                        <div class="row mb-2">
                            <div class="form-group text-center" style="padding-top: 8px; padding-bottom: 8px; border: none;">
                                <div class="col-md-12">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                Enabled='<%#Eval("Enabled")%>'
                                                OnClick="Page_Changed" Style="padding: 5px; border-radius: 5px; color: black; text-decoration: none"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</asp:Content>

