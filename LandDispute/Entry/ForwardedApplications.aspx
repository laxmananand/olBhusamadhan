<%@ Page Title="Forwarded Applications" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ForwardedApplications.aspx.cs" Inherits="LandDispute_Entry_ForwardedApplications" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .vf-grid th, .vf-grid td { padding: 6px 8px; vertical-align: middle; }
        .vf-detail-label { color: #6c757d; font-size: 13px; margin-bottom: 2px; }
        .vf-detail-value { font-weight: 600; margin-bottom: 12px; word-break: break-word; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- DM (DMOPT) / SP (SSPOPT): applications forwarded by the Home Department (ADMHOME) to this login's role in its district --%>
    <div class="container-fluid">
        <h4 class="text-black text-center"><b>Forwarded Applications</b></h4>

        <asp:Panel ID="pnlFilters" runat="server" CssClass="card mb-3" DefaultButton="btnSearch">
            <div class="card-body">
                <div class="row align-items-end">
                    <div class="col-md-4 mb-2">
                        <label class="control-label" for="<%= txtSearch.ClientID %>">Application No. / शिकायतकर्ता का नाम / मोबाइल संख्या</label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Search" MaxLength="100" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-2 mb-2">
                        <label class="control-label" for="<%= ddlPageSize.ClientID %>">Page Size</label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2">
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click"><i class="fa fa-search"></i>&nbsp;Search</asp:LinkButton>
                        <asp:LinkButton ID="btnReset" runat="server" CssClass="btn btn-outline-secondary ml-1" OnClick="btnReset_Click"><i class="fa fa-rotate-left"></i>&nbsp;Reset</asp:LinkButton>
                    </div>
                    <div class="col-md-3 mb-2 text-right">
                        <asp:Label ID="lblCount" runat="server" CssClass="text-muted"></asp:Label>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <%-- application details pop-up (filled on the server after clicking View, then opened by ShowModal) --%>
        <div class="modal fade" id="modalAppDetails" tabindex="-1" role="dialog" aria-labelledby="modalAppDetailsTitle" aria-hidden="true">
          <div class="modal-dialog modal-xl modal-dialog-scrollable" role="document">
            <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalAppDetailsTitle"><i class="fa fa-eye"></i>&nbsp;आवेदन संख्या: <asp:Label ID="lblDApplicationNo" runat="server"></asp:Label></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-3"><div class="vf-detail-label">शिकायतकर्ता का नाम</div><div class="vf-detail-value"><asp:Label ID="lblDName" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">पिता/ पति का नाम</div><div class="vf-detail-value"><asp:Label ID="lblDFather" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">लिंग</div><div class="vf-detail-value"><asp:Label ID="lblDGender" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">मोबाइल संख्या</div><div class="vf-detail-value"><asp:Label ID="lblDMobile" runat="server" /></div></div>

                    <div class="col-md-3"><div class="vf-detail-label">जिला</div><div class="vf-detail-value"><asp:Label ID="lblDDistrict" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">अनुमंडल</div><div class="vf-detail-value"><asp:Label ID="lblDSubdivision" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">अंचल</div><div class="vf-detail-value"><asp:Label ID="lblDBlock" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">थाना</div><div class="vf-detail-value"><asp:Label ID="lblDThana" runat="server" /></div></div>

                    <div class="col-md-3"><div class="vf-detail-label">क्षेत्र का प्रकार</div><div class="vf-detail-value"><asp:Label ID="lblDAreaType" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">ग्राम पंचायत / नगर निकाय</div><div class="vf-detail-value"><asp:Label ID="lblDPanchayat" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">राजस्व ग्राम</div><div class="vf-detail-value"><asp:Label ID="lblDVillage" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">वार्ड / मोहल्ला</div><div class="vf-detail-value"><asp:Label ID="lblDWard" runat="server" /></div></div>

                    <div class="col-md-3"><div class="vf-detail-label">पिनकोड</div><div class="vf-detail-value"><asp:Label ID="lblDPincode" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">अग्रेषण तिथि</div><div class="vf-detail-value"><asp:Label ID="lblDForwardedOn" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">अग्रेषित करने वाले</div><div class="vf-detail-value"><asp:Label ID="lblDForwardedBy" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">दस्तावेज़</div><div class="vf-detail-value"><asp:HyperLink ID="lnkDDocument" runat="server" Target="_blank" CssClass="btn btn-sm btn-outline-danger"><i class="fa fa-file-pdf"></i>&nbsp;View PDF</asp:HyperLink></div></div>

                    <div class="col-md-12"><div class="vf-detail-label">अग्रेषण टिप्पणी</div><div class="vf-detail-value" style="white-space: pre-wrap;"><asp:Label ID="lblDForwardRemarks" runat="server" /></div></div>
                    <div class="col-md-12"><div class="vf-detail-label">टिप्पणी (शिकायतकर्ता)</div><div class="vf-detail-value" style="white-space: pre-wrap;"><asp:Label ID="lblDRemarks" runat="server" /></div></div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Close</button>
            </div>
            </div>
          </div>
        </div>

        <script type="text/javascript">
            // opens a pop-up after a postback; jQuery/Bootstrap are loaded at the end of the master page, so wait for "load"
            function showADMHOMEModal(id) {
                window.addEventListener("load", function () { $("#" + id).modal("show"); });
            }
        </script>

        <div class="card">
            <div class="card-body">
                <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto">
                    <asp:GridView ID="gvApplications" runat="server" Width="100%" AutoGenerateColumns="false"
                        CssClass="table-responsive CSSTableGeneratorGrid fontsize vf-grid" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="gvApplications_PageIndexChanging" OnRowCommand="gvApplications_RowCommand"
                        EmptyDataText="कोई अग्रेषित आवेदन नहीं मिला।">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ApplicationNo" HeaderText="Application No." ItemStyle-Font-Bold="true" />
                            <asp:BoundField DataField="vadi_Name" HeaderText="शिकायतकर्ता का नाम" />
                            <asp:BoundField DataField="Vadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" />
                            <asp:BoundField DataField="Gender" HeaderText="लिंग" />
                            <asp:BoundField DataField="DistrictName" HeaderText="जिला" />
                            <asp:BoundField DataField="BlockName" HeaderText="अंचल" />
                            <asp:BoundField DataField="ThanaName" HeaderText="थाना" />
                            <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल संख्या" />
                            <asp:BoundField DataField="PinCode" HeaderText="पिनकोड" />
                            <asp:BoundField DataField="ForwardedOn" HeaderText="अग्रेषण तिथि" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                            <asp:BoundField DataField="ForwardRemarks" HeaderText="अग्रेषण टिप्पणी" ItemStyle-Width="200" />
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-primary btn-sm" CommandName="ViewApp"
                                        CommandArgument='<%# Eval("ApplicationNo") %>' ToolTip="View Application"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:HyperLink ID="lnkDoc" runat="server" CssClass="btn btn-outline-danger btn-sm" Target="_blank" ToolTip="View PDF"
                                        NavigateUrl='<%# "ADMHOME_ViewDocument.aspx?app=" + Server.UrlEncode(Convert.ToString(Eval("ApplicationNo"))) %>'><i class="fa fa-file-pdf"></i></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
