<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="LandDispute_ForgotPassword" %>

<%--<%@ Page Title="" Language="C#" MasterPageFile="Mainsitemaster.master" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="LandDispute_ForgotPassword" %>--%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalPopupConfirm {
            background-color: #ffffff;
            width: 550px;
            border: 2px solid #031208;
            color: Black;
        }

            .modalPopupConfirm .head {
                background-color: #397bb5;
                height: 40px;
                color: White;
                line-height: 40px;
                text-align: center;
                font-weight: bold;
                width: 550px;
            }

            .modalPopupConfirm .body {
                min-height: 150px;
                line-height: 50px;
                font-weight: bold;
            }

            .modalPopupConfirm .footer {
                padding: 3px;
                background-color: #ffffff;
                color: Black;
            }

            .modalPopupConfirm .yes, .modalPopup .no {
                height: 23px;
                color: White;
                line-height: 23px;
                text-align: center;
                font-weight: bold;
                cursor: pointer;
            }

            .modalPopupConfirm .yes {
                background-color: #475EDE;
                border: 1px solid #ffffff;
            }

            .modalPopupConfirm .no {
                background-color: #475EDE;
                border: 1px solid #ffffff;
            }

        /* Add this CSS to your style sheet */
        .modalBackground {
            background-color: rgba(0, 0, 0, 0.5); /* Semi-transparent black */
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            z-index: 1000; /* Adjust z-index to ensure it appears behind the modal popup */
        }
    </style>
    <script src="Styles/aes.js"></script>
    <script src="resources/crypto-js.min.js"></script>
    <script src="resources/jsencrypt.min.js"></script>
    <script type="text/javascript">
        const key64 = `LS0tLS1CRUdJTiBQVUJMSUMgS0VZLS0tLS0NCk1JR2ZNQTBHQ1NxR1NJYjNEUUVCQVFVQUE0R05BRENCaVFLQmdRQ0ZKSW1LbFkxRXB0bC9VcXhLcVNuWk5acUcNCmlzREJZQTU0MVN1WGtWT2piUTY4Qm5kVGNTQ2wzUEFFbnhrUk15ck51WHRleWt0clFRek5FN1prRENrZUxWQVgNCkFISVVZSGNGQzZnT2ZONmNQa2JFZnQ3a2hJRkxOaUYwbkswb2ZIemFqSmdpY1FzQnVyQnB4MjV2OXh3U29OS3QNCitwNlJRM3VKTlhkNEZwSEt0d0lEQVFBQg0KLS0tLS1FTkQgUFVCTElDIEtFWS0tLS0t`;
        function base64Decode(strpem) {
            var input = strpem;
            try {
                return decoded = atob(input);

            } catch (e) {
                alert('Error');
            }
        }
        function encrypt(text) {
            try {
                let encrypt = new JSEncrypt();
                encrypt.setPublicKey(base64Decode(key64));
                let encrypted = encrypt.encrypt(text);
                //alert(encrypted.toString());
                return encrypted.toString();
            }
            catch (ee) {
                //alert(ee.message);
            }
        }
    </script>
    <script>

       
        function refreshcaptchaNew() {
          

            document.getElementById('<%= imgCaptcha.ClientID %>').src = '<%= ResolveUrl("~/CreateCaptcha.aspx") %>';
            <%-- document.getElementById('imgCaptcha').src = '<%= ResolveUrl("~/CreateCaptcha.aspx") %>';--%>
             
             return false;
         }
        function isAlphanumeric(event) {
            var charCode = event.which ? event.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || // Numbers 0-9
                (charCode >= 65 && charCode <= 90) || // Letters A-Z
                (charCode >= 97 && charCode <= 122)) { // Letters a-z
                return true;
            } else {
                return false;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false; // Stop input if it's not a number
            }
            return true;
        }

        function GeneratePwd() {
            if (_validate()) {
                debugger;
                //alert("HI1");
                if (document.getElementById('<%= txtNew.ClientID %>').value != "") {

                    var step1 = Math.floor(Math.random() * 800000) + 100000;
                    var salted = step1 + document.getElementById('<%= txtNew.ClientID %>').value;
                    var saltedu = step1 + document.getElementById('<%= TxtReNew.ClientID %>').value;
                    // alert(newKey);
                    //alert("HI12");
                    document.getElementById('<%= HidKey.ClientID %>').value = encrypt(salted);
                    document.getElementById('<%= HidKey2.ClientID %>').value = encrypt(saltedu);

                    document.getElementById('<%= txtNew.ClientID %>').value = encrypt(salted);
                    document.getElementById('<%= TxtReNew.ClientID %>').value = encrypt(saltedu);

                }

                return true;
            }
            else
                return false;
        }

     <%--   function GeneratePwd() {
            debugger;
            alert("hi1");
            console.log(_validate);
            if (_validate()) {
                var step1 = Math.floor(Math.random() * 800000) + 100000;

                salted = step1 + document.getElementById('<%= txtNew.ClientID %>').value;
                document.getElementById('<%= txtNew.ClientID %>').value = SubmitsEncry(salted, document.getElementById('<%= HidKeyCP.ClientID %>').value, document.getElementById('<%= HidKeyCP.ClientID %>').value);


                salted = step1 + document.getElementById('<%= TxtReNew.ClientID %>').value;
                document.getElementById('<%= TxtReNew.ClientID %>').value = SubmitsEncry(salted, document.getElementById('<%= HidKeyCP.ClientID %>').value, document.getElementById('<%= HidKeyCP.ClientID %>').value);
                var hiddenField = document.getElementById('<%= HidKeyCP.ClientID %>');
                hiddenField.value = "";
                return true;

            }
            else {

                return false;
            }

        }--%>


        function _validate() {
           
            try {
                if (document.getElementById('<%= txtNew.ClientID %>').style.display != 'none') {
                    var len = document.getElementById('<%= txtNew.ClientID %>').value;
                    if (len.length < 1) { alert('Username can not be blank'); return false; }
                    if (len.length > 15) { alert('Username length is more than 15 characters'); return false; }
                }
            }
            catch (exception) {
            }
            var len1 = document.getElementById('<%= TxtReNew.ClientID %>').value;
            if (len1.length < 1) { alert('Password should not be blank.'); return false; }
            if (len1.length > 15) { alert('Password length is more than 15 characters'); return false; }

            return true;
        }
    </script>


    <style>
        .main {
            padding: 0px 0px;
            margin: 0px 0px 0px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div class="container">
        <asp:HiddenField ID="HidKey" runat="server" />
        <asp:HiddenField ID="HidKey2" runat="server" />
       <%-- <asp:HiddenField ID="HidKey3" runat="server" />--%>
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6">
                <asp:Panel ID="Panel1" Visible="false" runat="server">
                    <div class="panel">
                        <div class="panel-heading bg-success" style="margin-bottom: -8px;">
                            <h4 style="color: #ffffff; text-align: center; height: 42px; padding-top: 7px;">Forgot Password</h4>
                        </div>
                        <div class="panel-body" style="box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;">
                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-2 col-md-2 col-sm-2">
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2">
                                    <asp:Label ID="Label1" runat="server" Text="User ID:"></asp:Label>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <asp:TextBox ID="txtUserID" runat="server" MaxLength="15" AutoCompleteType="Disabled" placeholder="Enter User ID"
                                        AutoComplete="Off" onpaste="return false;" onCopy="return false" onCut="return false"
                                        CssClass="form-control" onkeypress="return isAlphanumeric(event)"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2"></div>
                            </div>
                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-2 col-md-2 col-sm-2">
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2">
                                    <asp:Label ID="Label2" runat="server" Text="Mobile:"></asp:Label>
                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" AutoCompleteType="Disabled" placeholder="Enter Mobile No"
                                        AutoComplete="Off" onpaste="return false;" onCopy="return false" onCut="return false"
                                        CssClass="form-control" onkeypress="return isNumberKey(event)"></asp:TextBox>
                                    <asp:RegularExpressionValidator
                                        ID="revMobileNumber"
                                        runat="server"
                                        ControlToValidate="txtMobile"
                                        ErrorMessage="Please enter a valid mobile number."
                                        ValidationExpression="^\+?[0-9]{10,15}$">
                                    </asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                </div>

                                <div class="col-lg-3 col-md-3 col-sm-3">

                                    <asp:Image ID="imgCaptcha" runat="server" Width="175" Height="50" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="1px" ImageUrl="CreateCaptcha.aspx?New=1" />


                                </div>
                                <div class="col-lg-1 col-md-1 col-sm-1"></div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    &nbsp;&nbsp;<asp:ImageButton ID="reloadcaptcha" runat="server" Height="35" Style="height: 42px; width: 42px; margin-top: 3px;" OnClientClick="refreshcaptchaNew();" ToolTip="Refresh Captcha" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-2 col-md-2 col-sm-2">
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                    Enter Code:
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <asp:TextBox ID="txtCaptha" runat="server" MaxLength="6" CssClass="form-control" placeholder="Enter shown Code" Style="margin-left: -46px;"></asp:TextBox>
                                </div>
                                <div class="col-lg-3 col-md-3 col-sm-3">
                                </div>

                            </div>

                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-12 col-md-12 col-sm-12" align="center">
                                    <div class="col-lg-6 col-md-6 col-sm-6" align="center">
                                        <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary" Width="100px" OnClick="btnSend_Click" Style="background-color: #66bb6a; border-color: #66bb6a" />
                                    </div>
                                    <%--Style="width: 100px; background-color: #357ebd; padding-right: 11px; margin-right: -197px;"--%>
                                    <div class="col-lg-6 col-md-6 col-sm-6" align="center">
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-danger" Style="background-color: red" OnClick="btnBack_Click" />
                                    </div>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMSG" runat="server" Width="302px" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel ID="Panel2" Visible="false" runat="server">
                    <div class="panel">
                        <%--panel-success--%>
                        <div class="panel-heading bg-success" style="margin-bottom: -8px;">
                            <h4 style="color: #ffffff; text-align: center; height: 42px; padding-top: 7px;">Set Password</h4>

                        </div>

                        <div class="panel-body" style="box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;">
                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">

                                <div class="col-lg-1 col-md-1 col-sm-1">
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <asp:Label ID="Label3" runat="server" Text="User ID:"></asp:Label>
                                </div>

                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <asp:Label ID="txtUserId1" runat="server" AutoCompleteType="Disabled" ValidationGroup="P"
                                        ReadOnly="True" CssClass="form-control"></asp:Label>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2">
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-1 col-md-1 col-sm-1">
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <asp:Label ID="Label5" runat="server" Text=" New Password*:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtNew" ValidationGroup="P" Width="22px"
                                        Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <asp:TextBox ID="txtNew" runat="server" AutoCompleteType="Disabled" TextMode="Password"
                                        AutoComplete="Off" oncopy="return false" onpaste="return false" oncut="return false"
                                        ValidationGroup="P" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2">
                                    <asp:HiddenField ID="HidKeyCP" runat="server" />
                                </div>
                            </div>
                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-1">
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <asp:Label ID="Label6" runat="server" Text="Confirm Password*:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="TxtReNew" ValidationGroup="P" Width="22px"
                                        Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6">
                                    <asp:TextBox ID="TxtReNew" runat="server" AutoCompleteType="Disabled" TextMode="Password"
                                        AutoComplete="Off" oncopy="return false" onpaste="return false" oncut="return false"
                                        ValidationGroup="P" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtReNew"
                                        ErrorMessage="Please Enter Confirm Password" Display="None">*</asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Confirm New Password doesn't match"
                                        ControlToCompare="txtNew" ControlToValidate="TxtReNew" Display="None">*</asp:CompareValidator>
                                </div>
                            </div>
                           
                         
                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-12 col-md-12 col-sm-12" align="center">
                                    <asp:Button ID="btnChangePWD" runat="server" Text="Set Password" CssClass="btn btn-danger"
                                        ValidationGroup="P" OnClientClick="return GeneratePwd();" OnClick="btnChangePWD_Click" />&nbsp; 
                                </div>
                            </div>

                        </div>
                        <div class="panel-footer">
                            <span class="alert-info" style="color: none !important;">Note: New Password must be 8-10 characters
               long with At least one upper case english letter [A-Z], At least one lower case
               english letter[a-z], at least one numeric character [0-9] and one special Character[#
               ? ! @ $ % *].</span>
                            <br />
                            <asp:Label ID="Label7" runat="server" ForeColor="Maroon" Width="480px"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div class="col-md-3"></div>
        </div>
    </div>
    <div id="container">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpe" runat="server" PopupControlID="pnlPopup"
            TargetControlID="btnShowPopup" CancelControlID="btnNo" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopupConfirm" Style="display: none">
            <div class="head">
                Enter OTP to Retrieve your password
            </div>
            <div class="body">
                <div>
                </div>
                <div class="row" style="padding-top: 25px;">
                    <div class="col-lg-8 text-right" style="vertical-align: middle">
                        Enter the OTP sent to the registered Mobile No
                    </div>
                    <div class="col-lg-3" align="left">
                        <asp:TextBox ID="txtOTP_Mob" AutoCompleteType="Disabled" TextMode="Password" AutoComplete="Off"
                            oncopy="return false" onpaste="return false" oncut="return false" runat="server"
                            MaxLength="4" MinLength="4" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-lg-1" align="left">
                    </div>
                </div>
                <div class="row" style="padding-top: 25px;">
                    <div class="col-lg-12 text-center" style="vertical-align: middle">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Button ID="btnResent" runat="server" Text="Resend OTP" ValidationGroup="P1"
                                    CssClass="btn btn-success" OnClick="btnResent_Click" />
                                <asp:Label ID="lblTime" runat="server" ForeColor="Green" Font-Bold="true" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="1000" OnTick="Timer1_Tick">
                        </asp:Timer>
                        <asp:Button ID="btnSave" runat="server" Text="Send" ValidationGroup="P1" CssClass="btn btn-success"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnNo" runat="server" Text="Cancel" ValidationGroup="P1" CssClass="btn btn-primary" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

