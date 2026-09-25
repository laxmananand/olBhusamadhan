<%@ Page Title="View & Forward Application" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ADMHOME_ViewForward.aspx.cs" Inherits="LandDispute_Entry_ADMHOME_ViewForward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .vf-grid th, .vf-grid td { padding: 6px 8px; vertical-align: middle; }
        .vf-detail-label { color: #6c757d; font-size: 13px; margin-bottom: 2px; }
        .vf-detail-value { font-weight: 600; margin-bottom: 12px; word-break: break-word; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- needed for the UpdatePanel inside the Forward pop-up (the master page has no ScriptManager) --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <h4 class="text-black text-center"><b>View &amp; Forward Application</b></h4>

        <%-- filters: जिला / अंचल apply on change; the text boxes apply on Search (or Enter) --%>
        <asp:Panel ID="pnlFilters" runat="server" CssClass="card mb-3" DefaultButton="btnSearch">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-3 mb-2">
                        <label class="control-label" for="<%= ddlFilterDistrict.ClientID %>">जिला</label>
                        <asp:DropDownList ID="ddlFilterDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterDistrict_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2">
                        <label class="control-label" for="<%= ddlFilterBlock.ClientID %>">अंचल</label>
                        <asp:DropDownList ID="ddlFilterBlock" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFilterBlock_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2">
                        <label class="control-label" for="<%= txtFilterName.ClientID %>">शिकायतकर्ता का नाम</label>
                        <asp:TextBox ID="txtFilterName" runat="server" CssClass="form-control" placeholder="शिकायतकर्ता का नाम" MaxLength="100" AutoComplete="off"></asp:TextBox>
                    </div>
                    <div class="col-md-3 mb-2">
                        <label class="control-label" for="<%= txtSearch.ClientID %>">File No. / मोबाइल संख्या</label>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="HDSB10001 / 98XXXXXXXX" MaxLength="15" AutoComplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="row align-items-end">
                    <div class="col-md-2 mb-2">
                        <label class="control-label" for="<%= ddlPageSize.ClientID %>">Page Size</label>
                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                            <asp:ListItem Text="10" Value="10" />
                            <asp:ListItem Text="25" Value="25" />
                            <asp:ListItem Text="50" Value="50" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 mb-2">
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary" OnClick="btnSearch_Click"><i class="fa fa-search"></i>&nbsp;Search</asp:LinkButton>
                        <asp:LinkButton ID="btnReset" runat="server" CssClass="btn btn-outline-secondary ml-1" OnClick="btnReset_Click" CausesValidation="false"><i class="fa fa-rotate-left"></i>&nbsp;Reset</asp:LinkButton>
                    </div>
                    <div class="col-md-6 mb-2 text-right">
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
                <h5 class="modal-title" id="modalAppDetailsTitle"><i class="fa fa-eye"></i>&nbsp;फाइल संख्या: <asp:Label ID="lblDFileNo" runat="server"></asp:Label></h5>
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
                    <div class="col-md-3"><div class="vf-detail-label">आवेदन प्राप्ति की तिथि</div><div class="vf-detail-value"><asp:Label ID="lblDAwedanPraptiDate" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">फाइनल करने की तिथि</div><div class="vf-detail-value"><asp:Label ID="lblDCreatedOn" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">फाइनल करने वाले</div><div class="vf-detail-value"><asp:Label ID="lblDCreatedBy" runat="server" /></div></div>
                    <div class="col-md-3"><div class="vf-detail-label">दस्तावेज़</div><div class="vf-detail-value"><asp:HyperLink ID="lnkDDocument" runat="server" Target="_blank" CssClass="btn btn-sm btn-outline-danger"><i class="fa fa-file-pdf"></i>&nbsp;View PDF</asp:HyperLink></div></div>

                    <div class="col-md-12"><div class="vf-detail-label">टिप्पणी</div><div class="vf-detail-value" style="white-space: pre-wrap;"><asp:Label ID="lblDRemarks" runat="server" /></div></div>
                </div>
                <%-- where this application has been forwarded --%>
                <div class="vf-detail-label">अग्रेषण विवरण (Forward History)</div>
                <asp:GridView ID="gvForwardHistory" runat="server" Width="100%" AutoGenerateColumns="false"
                    CssClass="table-responsive CSSTableGeneratorGrid fontsize vf-grid" EmptyDataText="अभी तक अग्रेषित नहीं किया गया।">
                    <Columns>
                        <asp:BoundField DataField="DistrictName" HeaderText="जिला" />
                        <asp:BoundField DataField="RoleName" HeaderText="अग्रेषित किया गया (To)" />
                        <asp:BoundField DataField="ForwardedToUserID" HeaderText="Login" />
                        <asp:BoundField DataField="ForwardRemarks" HeaderText="टिप्पणी" />
                        <asp:BoundField DataField="ForwardedBy" HeaderText="अग्रेषित करने वाले" />
                        <asp:BoundField DataField="ForwardedOn" HeaderText="अग्रेषण तिथि" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Close</button>
            </div>
            </div>
          </div>
        </div>

        <%-- forward pop-up: district + DM and/or SP + remarks.
             The district change is a partial (UpdatePanel) postback so the pop-up stays open;
             Forward is a full postback so the grid refreshes afterwards. --%>
        <div class="modal fade" id="modalForward" tabindex="-1" role="dialog" aria-labelledby="modalForwardTitle" aria-hidden="true">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="modalForwardTitle"><i class="fa fa-share"></i>&nbsp;आवेदन अग्रेषित करें (Forward Application): <asp:Label ID="lblFwdFileNo" runat="server"></asp:Label></h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            </div>
            <div class="modal-body">
                <asp:HiddenField ID="hfFwdFileNo" runat="server" />
                <asp:UpdatePanel ID="upForward" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <div class="row">
                    <div class="col-md-4 mb-2">
                        <label class="control-label" for="<%= ddlFwdDistrict.ClientID %>">जिला</label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlFwdDistrict" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFwdDistrict_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-8 mb-2">
                        <label class="control-label">अग्रेषित करें (DM / SP / दोनों)</label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <div class="d-flex flex-wrap" style="gap: 24px; padding-top: 6px;">
                            <div>
                                <asp:CheckBox ID="chkFwdDM" runat="server" Text="&nbsp;DM (जिलाधिकारी)" />
                                <div><small><asp:Label ID="lblFwdDMInfo" runat="server" CssClass="text-muted"></asp:Label></small></div>
                            </div>
                            <div>
                                <asp:CheckBox ID="chkFwdSP" runat="server" Text="&nbsp;SP (पुलिस अधीक्षक)" />
                                <div><small><asp:Label ID="lblFwdSPInfo" runat="server" CssClass="text-muted"></asp:Label></small></div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 mb-2">
                        <label class="control-label" for="<%= txtFwdRemarks.ClientID %>">टिप्पणी (वैकल्पिक)</label>
                        <asp:TextBox ID="txtFwdRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" MaxLength="500"
                            placeholder="अधिकतम 500 अक्षर" Style="resize: vertical"></asp:TextBox>
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal"><i class="fa fa-times"></i>&nbsp;Cancel</button>
                <asp:LinkButton ID="btnFwdSubmit" runat="server" CssClass="btn btn-info" OnClick="btnFwdSubmit_Click"
                    OnClientClick="return confirmADMHOMEForward();"><i class="fa fa-share"></i>&nbsp;Forward</asp:LinkButton>
            </div>
            </div>
          </div>
            <script type="text/javascript">
                // at least one of DM / SP must be ticked (also checked on the server)
                function confirmADMHOMEForward() {
                    var dm = document.getElementById('<%= chkFwdDM.ClientID %>');
                    var sp = document.getElementById('<%= chkFwdSP.ClientID %>');
                    var dist = document.getElementById('<%= ddlFwdDistrict.ClientID %>');
                    if (dist && dist.value === "0") { alert("कृपया जिला चुनें...!"); return false; }
                    if (!(dm && dm.checked && !dm.disabled) && !(sp && sp.checked && !sp.disabled)) { alert("कृपया DM या SP (या दोनों) चुनें...!"); return false; }
                    return confirm("क्या आप यह आवेदन अग्रेषित करना चाहते हैं?");
                }
            </script>
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
                    <asp:GridView ID="gvApplications" runat="server" Width="100%" AutoGenerateColumns="false" DataKeyNames="FileNo"
                        CssClass="table-responsive CSSTableGeneratorGrid fontsize vf-grid" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="gvApplications_PageIndexChanging" OnRowCommand="gvApplications_RowCommand"
                        EmptyDataText="कोई फाइनल आवेदन नहीं मिला।">
                        <Columns>
                            <asp:TemplateField HeaderText="क्रम सं. (Sl. No.)">
                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="FileNo" HeaderText="फाइल संख्या (File No.)" ItemStyle-Font-Bold="true" />
                            <asp:BoundField DataField="vadi_Name" HeaderText="शिकायतकर्ता का नाम" />
                            <asp:BoundField DataField="Vadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" />
                            <asp:BoundField DataField="Gender" HeaderText="लिंग" />
                            <asp:BoundField DataField="DistrictName" HeaderText="जिला" />
                            <asp:BoundField DataField="SubDivisionName" HeaderText="अनुमंडल" />
                            <asp:BoundField DataField="BlockName" HeaderText="अंचल" />
                            <asp:BoundField DataField="ThanaName" HeaderText="थाना" />
                            <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल संख्या" />
                            <asp:BoundField DataField="PinCode" HeaderText="पिनकोड" />
                            <asp:BoundField DataField="AwedanPraptiDate" HeaderText="आवेदन प्राप्ति की तिथि" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="CreatedOn" HeaderText="फाइनल करने की तिथि" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                            <asp:BoundField DataField="ForwardedTo" HeaderText="अग्रेषित (Forwarded To)" />
                            <%-- Status W = forwarded (Yes), F = finalised but not yet forwarded (No) --%>
                            <asp:TemplateField HeaderText="अग्रेषित स्थिति (Forwarded Status)">
                                <ItemTemplate>
                                    <span class='badge p-2 <%# Convert.ToString(Eval("Status")) == "W" ? "badge-success" : "badge-secondary" %>'>
                                        <%# Convert.ToString(Eval("Status")) == "W" ? "Yes" : "No" %>
                                    </span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="देखें (View)">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnView" runat="server" CssClass="btn btn-primary btn-sm" CommandName="ViewApp"
                                        CommandArgument='<%# Eval("FileNo") %>' ToolTip="View Application"><i class="fa fa-eye"></i></asp:LinkButton>
                                    <asp:HyperLink ID="lnkDoc" runat="server" CssClass="btn btn-outline-danger btn-sm" Target="_blank" ToolTip="View PDF"
                                        NavigateUrl='<%# "ADMHOME_ViewDocument.aspx?file=" + Server.UrlEncode(Convert.ToString(Eval("FileNo"))) %>'><i class="fa fa-file-pdf"></i></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="भेजें (Forward)">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnForward" runat="server" CssClass="btn btn-info btn-sm" CommandName="ForwardApp"
                                        CommandArgument='<%# Eval("FileNo") %>' ToolTip="Forward to DM / SP"><i class="fa fa-share"></i>&nbsp;Forward</asp:LinkButton>
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
