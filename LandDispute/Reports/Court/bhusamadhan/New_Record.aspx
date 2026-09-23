<%@ Page Title="" Language="C#" MasterPageFile="~/EOC/MasterPage.master" AutoEventWireup="true"
    CodeFile="New_Record.aspx.cs" Inherits="EOC_New_Record" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container">
        <div class="row">
            <div class="col-md-12 text-center">
                <h4>
                    New Record</h4>
            </div>
        </div>
        <div class="row">
            <center>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
            </center>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                दैनिक बाढ़ प्रतिवेदन
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="UPanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--<hr style="margin-bottom: 0px; margin-top: 0px;" />--%>
                        <div class="row" style="padding-top: 7px;">
                        </div>
                        <div class="row" style="padding-top: 7px">
                            <div class="col-md-3">
                                दिनांक
                                <img src="../images/imp.gif" width="13px" />
                            </div>
                            <div class="col-md-3">
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtdate1" MaxLength="10" CssClass="form-control"
                                        placeholder="DD-MM-YYYY" onkeypress="return dateValidate(event)"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <rjs:popcalendar id="PopCalendar1" runat="server" control="txtdate1" format="dd MM yyyy" />
                                </div>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>
                        <div class="row" style="padding-top: 7px">
                            <div class="col-md-3">
                                चयनित ज़िला<img src="../images/imp.gif" width="13px" />
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control" Style="height: 30px"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                क्या यह चयनित ज़िला बाढ़ प्रभावित है?<img src="../images/imp.gif" width="13px" />
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" ID="ddlDistrict_affected" CssClass="form-control"
                                    Style="height: 30px" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_affected_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                    <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div id="divBlock" runat="server" visible="false" class="row" style="padding-top: 7px">
                            <div class="col-md-3">
                                अंचल का चयन करें<img src="../images/imp.gif" width="13px" />
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlBlock" CssClass="form-control"
                                    Style="height: 30px" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                क्या यह चयनित अंचल बाढ़ प्रभावित है?
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlBlock_affected" CssClass="form-control"
                                    Style="height: 30px" OnSelectedIndexChanged="ddlBlock_affected_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                    <asp:ListItem Value="Y">हाँ</asp:ListItem>
                                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </ContentTemplate>
                    <%--<Triggers>
                            <asp:PostBackTrigger ControlID="ddlBlock_affected"   />
                        </Triggers>--%>
                </asp:UpdatePanel>
            </div>
            <div id="tddetails" runat="server" visible="true">
                <div class="row" style="padding-bottom: 0.5%; padding-left: 0.5%">
                    <div class="col-lg-5 col-md-3 col-sm-3 ">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-3  ">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                            OnClientClick=" return confirm('Are you sure to save data?')" OnClick="Submit_Click" />
                    </div>
                    <div class="col-lg-5 col-md-2 col-sm-3">
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlPanchayt" runat="server" Visible="false">
            <div class="panel panel-primary">
                <div class="panel-heading" style="padding: 3px 20px">
                    Step 1
                </div>
                <div class="panel-body">
                    <div class="panel-heading" style="padding: 3px 20px">
                        दैनिक बाढ़ प्रतिवेदन
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--<hr style="margin-bottom: 0px; margin-top: 0px;" />--%>
                            <div class="row" style="padding-top: 7px;">
                            </div>
                            <%--<div class="row" style="padding-top: 7px">
                    
                    <div class="col-md-2">
                        दिनांक <img src="../images/imp.gif" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <div class="col-md-8">
                            <asp:TextBox runat="server" ID="txtdate2" MaxLength="10" CssClass="form-control" placeholder="DD-MM-YYYY"  onkeypress="return dateValidate(event)"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <rjs:PopCalendar ID="PopCalendar2" runat="server" Control="txtdate2" Format="dd MM yyyy" />
                        </div>
                    </div>
                       
                    <div class="col-md-3">
                         <div class="col-md-6">
                        चयनित ज़िला<img src="../images/imp.gif" width="13px" />
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="ddlDistrict_step1" CssClass="form-control" style="height:30px"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_step1_SelectedIndexChanged">                            
                        </asp:DropDownList>
                    </div>
                    </div>
                    <div class="col-md-4">
                       <div class="col-md-6">
                        अंचल का चयन करें<img src="../images/imp.gif" width="13px" />
                    </div>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlBlock_step1" CssClass="form-control" style="height:30px" OnSelectedIndexChanged="ddlBlock_step1_SelectedIndexChanged" >                            
                        </asp:DropDownList>
                    </div> 
                    </div>
                </div>--%>
                            <div class="row">
                                <div class="col-md-12" style="text-align: center">
                                    <asp:GridView runat="server" Width="100%" ID="panchayt_grid" AutoGenerateColumns="false"
                                        CssClass="table-responsive CSSTableGeneratorGrid" OnRowCommand="panchayt_grid_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PanchayatCode" HeaderText="Panchayat Code" ItemStyle-Width="1" />
                                            <asp:BoundField DataField="PanchayatName" HeaderText="Panchayat Name" ItemStyle-Width="120" />
                                            <asp:TemplateField HeaderText="Status" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Height="30px">
                                                        <asp:ListItem Value="-1">--चुने--</asp:ListItem>
                                                        <asp:ListItem Value="1">Only Agriclature</asp:ListItem>
                                                        <asp:ListItem Value="2">Only Population</asp:ListItem>
                                                        <asp:ListItem Value="3">Both</asp:ListItem>
                                                        <asp:ListItem Value="0" Selected="True">Unaffected</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="Action" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Button ID="btnRowDel" Text="Delete" CssClass="btn-danger" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="Remove" 
                                                    OnClientClick="return confirm('Are you sure you want to delete this data?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div id="Div2" runat="server" visible="true">
                    <div class="row" style="padding-bottom: 0.5%; padding-left: 0.5%">
                        <div class="col-lg-5 col-md-3 col-sm-3 ">
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-3  ">
                            <asp:Button ID="btnSaveNext1" runat="server" Text="Save & Next" CssClass="btn btn-primary"
                                OnClientClick=" return confirm('Are you sure to save data?')" />
                        </div>
                        <div class="col-lg-5 col-md-2 col-sm-3">
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelStep2" runat="server" Visible="true">
            <div class="panel panel-primary">
                <div class="panel-heading" style="padding: 3px 20px">
                    Step 2
                </div>
                <div class="panel-body">
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row form-group" style="margin-bottom: 0px;">
                                <div class="col-md-4">
                                    <h4>
                                        अंचल का प्रभावित जनसंख्या</h4>
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4" align="center">
                                        <h4>
                                            कल तक</h4>
                                    </div>
                                    <div class="col-md-4" align="center">
                                        <h4>
                                            आज</h4>
                                    </div>
                                    <div class="col-md-4" align="center">
                                        <h4>
                                            कुल</h4>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                            <div class="row form-group">
                                <div class="col-md-4">
                                    मनुष्य
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayHumanAffected" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtHumanAffected" runat="server" onkeypress="return validateNumberOnly(event)"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="प्रभावित मनुष्य !"
                                                ControlToValidate="txtHumanAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalHumanAffected" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    परिवार प्रभावित
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayFamilyAffected" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtFamilyAffected" runat="server" onkeypress="return validateNumberOnly(event)"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="परिवार प्रभावित !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalFamilyAffected" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 10px;" />
                            <div class="row form-group" style="margin-bottom: 0px;">
                                <div class="col-md-12">
                                    <h4>
                                        निष्क्रमित आबादी की संख्या
                                    </h4>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                            <div class="row form-group">
                                <div class="col-md-4">
                                    निष्क्रमित आबादी की संख्या
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayMigratedPopulation" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtMigratedPopulation" runat="server" onkeypress="return validateNumberOnly(event)"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage=" निष्क्रमित आबादी की संख्या !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalMigratedPopulation" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 10px;" />
                            <div class="row form-group" style="margin-bottom: 0px;">
                                <div class="col-md-12">
                                    <h4>
                                        प्रभावित क्षेत्रफल ( हेक्टेयर)
                                    </h4>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                            <div class="row form-group">
                                <div class="col-md-4">
                                    कृषि योग्य
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayAffectedAgriLand" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtAffectedAgriLand" runat="server" onkeypress="return validateNumberOnly(event)"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="कृषि योग्य प्रभावित क्षेत्रफल ( हेक्टेयर) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalAffectedAgriLand" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    ग़ैर कृषि योग्य
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayAffectedNonAgriLand" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtAffectedNonAgriLand" runat="server" onkeypress="return validateNumberOnly(event)"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage=" ग़ैर कृषि योग्य प्रभावित क्षेत्रफल ( हेक्टेयर) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalAffectedNonAgriLand" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    क्षतिग्रस्त फसल क्षेत्रफल (हेक्ट में)
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayDamagedCropArea" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtDamagedCropArea" runat="server" onkeypress="return validateNumberOnly(event)"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="क्षतिग्रस्त फसल क्षेत्रफल (हेक्ट में) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalDamagedCropArea" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    क्षतिग्रस्त फसलों का अनुमानित मूल्य (रु में )
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayDamagedCropValue" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtDamagedCropValue" runat="server" onkeypress="return validateNumberOnly(event)"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage=" क्षतिग्रस्त फसलों का अनुमानित मूल्य (रु में ) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalDamagedCropValue" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 10px;" />
                            <div class="row form-group" style="margin-bottom: 0px;">
                                <div class="col-md-12">
                                    <h4>
                                        क्षतिग्रस्त मकानों की संख्या
                                    </h4>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                            <div class="row form-group">
                                <div class="col-md-4">
                                    कच्चा घर (पूर्ण रूप से क्षतिग्रस्त)
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayFullyAffectedKutchaHouses" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtFullyAffectedKutchaHouses" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="कच्चा घर (पूर्ण रूप से क्षतिग्रस्त) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalFullyAffectedKutchaHouses" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    कच्चा घर (आंशिक रूप से क्षतिग्रस्त)
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayPartlyAffectedKutchaHouses" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtPartlyAffectedKutchaHouses" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage=" कच्चा घर (आंशिक रूप से क्षतिग्रस्त) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalPartlyAffectedKutchaHouses" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    पक्का घर (पूर्ण रूप से क्षतिग्रस्त)
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayFullyAffectedPakkaHouses" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtFullyAffectedPakkaHouses" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="पक्का घर (पूर्ण रूप से क्षतिग्रस्त) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalFullyAffectedPakkaHouses" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    पक्का घर (आंशिक रूप से क्षतिग्रस्त)
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayPartlyAffectedPakkaHouses" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtPartlyAffectedPakkaHouses" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="पक्का घर (आंशिक रूप से क्षतिग्रस्त) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalPartlyAffectedPakkaHouses" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    झोपडी
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayAffectedHuts" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtAffectedHuts" runat="server" onkeypress="return validateNumberOnly(event)"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="झोपडी क्षतिग्रस्त !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalAffectedHuts" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    पशु शेड
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayAffectedAnimalSheds" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtAffectedAnimalSheds" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="पशु शेड क्षतिग्रस्त !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalAffectedAnimalSheds" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    क्षतिग्रस्त मकानों का अनुमानित मूल्य (रु में)
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayDamagedHousesValue" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtDamagedHousesValue" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="क्षतिग्रस्त मकानों का अनुमानित मूल्य (रु में) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalDamagedHousesValue" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    क्षतिग्रस्त सार्वजनिक सम्पत्ति का अनुमानित मूल्य (रु में)
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayDamagedPublicPropVal" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtDamagedPublicPropVal" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="क्षतिग्रस्त सार्वजनिक सम्पत्ति का अनुमानित मूल्य (रु में) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalDamagedPublicPropVal" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            रु
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    कितने गांव पानी से घिरे है
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayWaterEncircledVillages" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            गांव
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtWaterEncircledVillages" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage=" पानी से घिरे गांव !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            गांव
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalWaterEncircledVillages" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            गांव
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 10px;" />
                            <div class="row form-group" style="margin-bottom: 0px;">
                                <div class="col-md-12">
                                    <h4>
                                        मृतकों की संख्या
                                    </h4>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                            <div class="row form-group">
                                <div class="col-md-4">
                                    मनुष्य
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayDeadPeoples" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtDeadPeoples" onkeypress="return validateNumberOnly(event)" runat="server"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="मृतकों की संख्या !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalDeadPeoples" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                        </div>
                    </div>
                    <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
                <div id="Div1" runat="server" visible="true">
                    <div class="row" style="padding-bottom: 0.5%; padding-left: 0.5%">
                        <div class="col-lg-5 col-md-3 col-sm-3 ">
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-3  ">
                            <asp:Button ID="btnSaveStep2" runat="server" Text="Save & Next" CssClass="btn btn-primary"
                                OnClientClick=" return confirm('Are you sure to save data?')" OnClick="btnSaveStep2_Click" />
                        </div>
                        <div class="col-lg-5 col-md-2 col-sm-3">
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="PanelStep3" runat="server" Visible="true">
            <div class="panel panel-primary">
                <div class="panel-heading" style="padding: 3px 20px">
                    Step 3
                </div>
                <div class="panel-body">
                    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row form-group" style="margin-bottom: 0px;">
                                <div class="col-md-4">
                                    <h4>
                                        पशु से संबंधित जानकारी</h4>
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4" align="center">
                                        <h4>
                                            कल तक</h4>
                                    </div>
                                    <div class="col-md-4" align="center">
                                        <h4>
                                            आज</h4>
                                    </div>
                                    <div class="col-md-4" align="center">
                                        <h4>
                                            कुल</h4>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                            <div class="row form-group">
                                <div class="col-md-4">
                                    प्रभावित पशुओं की संख्या
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayAffectedAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtAffectedAnimals" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage=" प्रभावित पशुओं की संख्या !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalAffectedAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    पशु कैंपो की संख्या
                                    <img alt="" src="../images/imp.gif" width="13px" /></div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayAnimalCamps" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtAnimalCamps" onkeypress="return validateNumberOnly(event)" runat="server"
                                                CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="पशु कैंपो की संख्या !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalAnimalCamps" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    उपचार किये गए पशुओं की संख्या
                                    <img alt="" src="../images/imp.gif" width="13px" /></div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayAnimalsTreated" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtAnimalsTreated" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="उपचार किये गए पशुओं की संख्या !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalAnimalsTreated" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 10px;" />
                            <div class="row form-group" style="margin-bottom: 0px;">
                                <div class="col-md-12">
                                    <h4>
                                        मृत पशुओं की संख्या
                                    </h4>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                            <div class="row form-group">
                                <div class="col-md-4">
                                    दुधारू पशु - छोटे
                                    <img alt="" src="../images/imp.gif" width="13px" /></div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDaySmallMilchAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtSmallMilchAnimals" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="मृत पशुओं की संख्या !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalSmallMilchAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    दुधारू पशु - बड़े
                                    <img alt="" src="../images/imp.gif" width="13px" /></div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayBigMilchAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtBigMilchAnimals" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="दुधारू पशु - बड़े !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalBigMilchAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    ग़ैर-दुधारू पशु - छोटे
                                    <img alt="" src="../images/imp.gif" width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDaySmallNonMilchAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtSmallNonMilchAnimals" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="ग़ैर-दुधारू पशु - छोटे !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalSmallNonMilchAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    ग़ैर-दुधारू पशु - बड़े
                                    <img alt="" src="../images/imp.gif" width="13px" /></div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayBigNonMilchAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtBigNonMilchAnimals" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ErrorMessage="ग़ैर-दुधारू पशु - बड़े !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalBigNonMilchAnimals" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 10px;" />
                            <div class="row form-group" style="margin-bottom: 0px;">
                                <div class="col-md-12">
                                    <h4>
                                        मछली जीरा फार्म से संबंधित जानकारी
                                    </h4>
                                </div>
                            </div>
                            <hr style="margin-top: 3px; margin-bottom: 15px;" />
                            <div class="row form-group">
                                <div class="col-md-4">
                                    मछली जीरा फार्म क्षतिग्रस्त <b>( हेक्टेयर)</b><img alt="" src="../images/imp.gif"
                                        width="13px" />
                                </div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayDamagedFishSeedFarms" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtDamagedFishSeedFarms" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="मछली जीरा फार्म क्षतिग्रस्त (हेक्टेयर) !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalDamagedFishSeedFarms" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                            हेक्टेयर
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    पूरी तरह से क्षतिग्रस्त नाव की संख्या
                                    <img alt="" src="../images/imp.gif" width="13px" /></div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayFullyDamagedBoats" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtFullyDamagedBoats" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="पूरी तरह से क्षतिग्रस्त नाव की संख्या !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalFullyDamagedBoats" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row form-group">
                                <div class="col-md-4">
                                    आंशिक रूप से नाव क्षतिग्रस्त की संख्या
                                    <img alt="" src="../images/imp.gif" width="13px" /></div>
                                <div class="col-md-8">
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTillPreDayPartlyDamagedBoats" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:TextBox ID="txtPartlyDamagedBoats" onkeypress="return validateNumberOnly(event)"
                                                runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ErrorMessage=" !"
                                                ControlToValidate="txtFamilyAffected" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: right;">
                                        <div class="col-md-9" style="text-align: right;">
                                            <asp:Label ID="lblTotalPartlyDamagedBoats" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <div class="col-md-3">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="Div3" runat="server" visible="true">
                        <div class="row" style="padding-bottom: 0.5%; padding-left: 0.5%">
                            <div class="col-lg-5 col-md-3 col-sm-3 ">
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3  ">
                                <asp:Button ID="btnSaveStep3" runat="server" Text="Save & Next" CssClass="btn btn-primary"
                                    OnClientClick=" return confirm('Are you sure to save data?')" OnClick="btnSaveStep3_Click" />
                            </div>
                            <div class="col-lg-5 col-md-2 col-sm-3">
                            </div>
                        </div>
                    </div>
                </div>
        </asp:Panel>
    </div>
</asp:Content>
