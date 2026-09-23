<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="EditProfile.aspx.cs" Inherits="LandDispute_Entry_Unfinalize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        divclss {
            -ms-overflow-style: none; /* for Internet Explorer, Edge */
            scrollbar-width: none; /* for Firefox */
            overflow-y: scroll;
        }

            divclss::-webkit-scrollbar {
                display: none; /* for Chrome, Safari, and Opera */
            }

        /* other styling */
        divclss {
            border: solid 5px black;
            border-radius: 5px;
            height: 300px;
            padding: 2px;
            width: 200px;
        }

            divclss.* {
                background-color: #EAF0F6;
                color: #2D3E50;
                font-family: 'Avenir';
                font-size: 26px;
                font-weight: bold;
            }

        #divImage {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 700px;
            width: 1200px;
            padding: 3px;
            border: solid 1px black;
        }

        .table-responsive {
            display: masonry !important;
        }
    </style>

    <script>
        function toggleDiv(event) {
            event.preventDefault();

            var toggleSection = document.getElementById("toggleSection");
            var toggleButton = document.getElementById("toggleButton");
            var toggleState = document.getElementById("<%= hfToggleState.ClientID %>");

            if (toggleSection.style.display === "none") {
                toggleSection.style.display = "block";
                toggleButton.textContent = "− Add Block";
                toggleState.value = "visible";
            } else {
                toggleSection.style.display = "none";
                toggleButton.textContent = "+ Add Block";
                toggleState.value = "hidden";
            }
        }

        // Restore the toggle section state after partial postback
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            var toggleState = document.getElementById("<%= hfToggleState.ClientID %>");
    var toggleSection = document.getElementById("toggleSection");
    var toggleButton = document.getElementById("toggleButton");

    if (toggleState.value === "visible") {
        toggleSection.style.display = "block";
        toggleButton.textContent = "− Add Block";
    } else {
        toggleSection.style.display = "none";
        toggleButton.textContent = "+ Add Block";
    }
});

    </script>
  
    <script>
        function isNumberKey(evt, element, decimalPoint) {
            var charCode = (evt.which) ? evt.which : evt.keyCode
            if (decimalPoint == 0) {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
            }
            else {
                if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
                    return false;
                else {
                    var len = $(element).val().length;
                    var index = $(element).val().indexOf('.');
                    if (index > 0 && charCode == 46) { return false; }
                    if (index > 0) {
                        var CharAfterdot = (len) - index;
                        if (CharAfterdot > decimalPoint) { return false; }
                    }
                }
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <h4 class="text-black text-center"><b>Edit Profile</b></h4>
        <div class="row">
            <center>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label>
            </center>
        </div>
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-2">
                        <label class="control-label" for="inputGroup">User Name </label>
                        <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter User Name" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label class="control-label" for="inputGroup">Name </label>
                        <asp:TextBox ID="txtName" runat="server" placeholder="Enter Name" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label class="control-label" for="inputGroup">Email </label>
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label for="inputEmail">
                                Mobile No
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="a" ControlToValidate="txtAuthorizedPersonMobileNo"
                                ForeColor="Red" Display="Dynamic" ErrorMessage="Required" />
                                <asp:RegularExpressionValidator ID="rgxMobile" runat="server" ControlToValidate="txtAuthorizedPersonMobileNo" ValidationExpression="(0|91)?[6-9][0-9]{9}" ErrorMessage="Invalid Mobile Number" ValidationGroup="a" ForeColor="Red"></asp:RegularExpressionValidator>
                            </label>
                            <div class="input-group mb-3">
                                <asp:TextBox ID="txtAuthorizedPersonMobileNo" runat="server" CssClass="form-control" placeholder="Enter 10-digit mobile number" MaxLength="10" autocomplete="off" onkeypress="return isNumberKey(event,this,0)"></asp:TextBox>
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="lnkSendOTPMob" runat="server" Text="Get OTP Mobile" CssClass="input-group-text" OnClick="lnkSendOTPMob_Click"></asp:LinkButton>
                                </div>
                            </div>
                            <!-- Timer Section -->
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Timer2" EventName="Tick" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:LinkButton ID="linkMobResendOTP" runat="server" Text="Re-Send OTP" Visible="false" CssClass="input-group-text" OnClick="linkMobResendOTP_Click"></asp:LinkButton>
                                        <asp:Label ID="lblMobileTime" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:Timer ID="Timer2" runat="server" Enabled="False" Interval="1000" OnTick="Timer2_Tick">
                                </asp:Timer>
                            </div>
                        </div>
                    </div>

                    <div class="form-group" id="divMob" runat="server" visible="false">
                        <label for="inputEmail">
                            Enter OTP(Mobile)       
                        </label>
                        <div class="input-group mb-3">
                            <asp:TextBox ID="txtMobOTP" runat="server" CssClass="form-control  form-control-sm text-uppercase" placeholder="4-digit OTP number" MaxLength="10" autocomplete="off" Style="height: 42px;"></asp:TextBox>
                            <div class="input-group-prepend">
                                <asp:LinkButton ID="lnkVerifyMobOTP" runat="server" Text="Verify OTP" CssClass="input-group-text" OnClick="lnkVerifyMobOTP_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <label class="control-label" for="inputGroup">Police Station </label>
                        <asp:TextBox ID="txtPolice_Station" runat="server" placeholder="Enter Police Station" CssClass="form-control"></asp:TextBox>
                    </div>
                    <asp:LinkButton ID="lbtnEdit" runat="server" Text="Update" CssClass="btn btn-success" Style="width: 120px; height: 40px; margin-top: 31px;" OnClick="lbtnEdit_Click"></asp:LinkButton>

                </div>
            </div>
        </div>
        <br />

        <div class="card">
            <div class="card-header">
                <button id="toggleButton" class="btn btn-primary" onclick="toggleDiv(event)">
                    + Add Block
                </button>
            </div>

            <div id="toggleSection" class="card-body" style="display: none;">
                <div class="row">
                    <div class="col-md-4">
                        <div class="row">

                            <asp:UpdatePanel ID="updatePanelDropdowns" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hfToggleState" runat="server" Value="hidden" />
                                    <div class="row" style="width: 150% !important;">
                                        <div class="col-md-6">
                                            <label class="control-label" for="ddlSubDivision">SubDivision </label>
                                            <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control" Enabled="true"
                                                OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-6">
                                            <label class="control-label" for="ddlBlock">Block </label>
                                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:LinkButton ID="lbtnSave" runat="server" Text="Save" CssClass="btn btn-success"
                                        Style="width: 100px; height: 40px; margin-top: 31px; margin-left: 127px;" OnClick="lbtnSave_Click"></asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                        <asp:Label ID="Label1" runat="server" Text="" Visible="false"></asp:Label>
                    </div>

                    <div class="col-md-8">
                        <br />
                        <asp:GridView runat="server" Width="100%" ID="gvShowAddBlockDetails" AutoGenerateColumns="false"
                            CssClass="table-responsive" EnableTheming="false" ShowFooter="false" AllowPaging="false"
                            PageSize="20" OnPageIndexChanging="gvShowAddBlockDetails_PageIndexChanging" EmptyDataText="No Record Found"
                            Visible="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 + (gvShowAddBlockDetails.PageIndex * gvShowAddBlockDetails.PageSize) %>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                        Font-Size="Small" Width="4%" />
                                    <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="6%" ForeColor="Black" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Subdivision Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("Subdivision_Name") %>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                        Font-Size="Small" HorizontalAlign="left" Width="6%" />
                                    <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Block Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"
                                    ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <%# Eval("BlockName") %>
                                    </ItemTemplate>
                                    <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                        Font-Size="Small" HorizontalAlign="left" Width="6%" />
                                    <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
