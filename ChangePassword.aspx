<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SamvidaChangePass.master" CodeFile="ChangePassword.aspx.cs" Inherits="CLG_ChangePass" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <%--<link rel="stylesheet" type="text/css" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/font.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/li-scroller.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/slick.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/jquery.fancybox.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/theme.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/style.css" /--%>


    <link href="bhusamadhan/vendor/fontawesome-free-6.1.1/css/all.min.css" rel="stylesheet" />
    <link href="bhusamadhan/css/ruang-admin.min.css" rel="stylesheet" />
    <link href="bhusamadhan/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />



    <script src="bhusamadhan/vendor/jquery/jquery.min.js"></script>
    <script src="bhusamadhan/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="bhusamadhan/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="bhusamadhan/vendor/chart.js/Chart.min.js"></script>
    <script src="bhusamadhan/js/demo/chart-area-demo.js"></script>
    <script src="bhusamadhan/vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <script src="bhusamadhan/js/ruang-admin.min.js"></script>
    <script src="skey1.js"></script>
    <script src="SKey.js"></script>

    <script src="sha256.js"></script>

    <script src="Styles/aes.js" type="text/javascript"></script>

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
            alert(ee.message);
        }
    }
</script>

    <script type="text/javascript">
        function refreshcaptcha() {

            sessionStorage.setItem("txtOldPassword", document.getElementById('<%= txtOldPassword.ClientID %>').value);
            document.getElementById('<%= txtOldPassword.ClientID %>').value = '';

            sessionStorage.setItem("txtNewPwd", document.getElementById('<%= txtNewPwd.ClientID %>').value);
            document.getElementById('<%= txtNewPwd.ClientID %>').value = '';

            sessionStorage.setItem("txtRePwd", document.getElementById('<%= txtRePwd.ClientID %>').value);
            document.getElementById('<%= txtRePwd.ClientID %>').value = '';


            document.getElementById('my-image').setAttribute('src', '~/CreateCaptcha.aspx');
            document.getElementById('<%= txtOldPassword.ClientID %>').value = sessionStorage.getItem("txtOldPassword");

            document.getElementById('<%= txtNewPwd.ClientID %>').value = sessionStorage.getItem("txtNewPwd");

            document.getElementById('<%= txtRePwd.ClientID %>').value = sessionStorage.getItem("txtRePwd");
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

        function GeneratePwd() {
            if (_validate()) {
                debugger;
                //alert("HI");
                if (document.getElementById('<%= txtOldPassword.ClientID %>').value != "") {

               var step1 = Math.floor(Math.random() * 800000) + 100000;
               var salted = step1 + document.getElementById('<%= txtNewPwd.ClientID %>').value;
               var saltedu = step1 + document.getElementById('<%= txtRePwd.ClientID %>').value;
               var saltedun = step1 + document.getElementById('<%= txtOldPassword.ClientID %>').value;
               //alert(salted);
               //alert(salted);
               document.getElementById('<%= HidKey.ClientID %>').value = encrypt(salted);
          document.getElementById('<%= HidKey2.ClientID %>').value = encrypt(saltedu);
          document.getElementById('<%= HidKey4.ClientID %>').value = encrypt(saltedun);

                    document.getElementById('<%= txtOldPassword.ClientID %>').value = encrypt(saltedun);
                    document.getElementById('<%= txtNewPwd.ClientID %>').value = encrypt(salted);
                    document.getElementById('<%= txtRePwd.ClientID %>').value = encrypt(saltedu);

                  }

                  return true;
              }
              else
                  return false;
          }


    

            function _validate() {

          var len1 = document.getElementById('<%= txtOldPassword.ClientID %>').value;
            if (len1.length <= 0) { alert('Old Password can not be blank'); return false; }


            var len2 = document.getElementById('<%= txtNewPwd.ClientID %>').value;
            if (len2.length < 8) { alert('New password must be at least 8 characters long'); return false; }
            if (len2.length > 10) { alert('New password can not be more than 10 characters'); return false; }

            var len3 = document.getElementById('<%= txtRePwd.ClientID %>').value;
            if (len3.length <= 0) { alert('Please Re-Type New password'); return false; }
            if (len2 != len3) { alert('New Password does not match'); return false; }

            var len4 = document.getElementById('<%= txtCaptha.ClientID %>').value;
            if (len4.length < 4) { alert('Please enter Captcha text correctly'); return false; }

            var passw = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,10}$/;
            if (!document.getElementById('<%= txtNewPwd.ClientID %>').value.match(passw)) {
                alert('New Password must be 8-10 characters long with Atleast one upper case english letter [A-Z], At least one lower case english letter[a-z], at least one numeric character [0-9] and one special Character[# ? ! @ $ % *].');
                return false;
            }

            return true;

        }

    </script>
    <style type="text/css">
        .baseBlock {
            background-color: #ffffff;
            margin: 0px 0px 15px 0px;
            padding: 0 0 15px 0px;
            border-radius: 5px;
            overflow: hidden;
            min-height: 70px;
            -moz-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            -o-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            box-shadow: 0 40px 40px rgba(0, 0, 0, 0.2);
        }

        .btnbox {
            box-shadow: 0 25px 30px rgb(0 0 0 / 50%);
        }
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="contentSection">
        <div class="row">
            <div class="col-lg-2"></div>
            <div class="col-lg-8 col-lg-offset-2">
                <div class=" sb-nav-fixed ">
                    <div class=" text-left" style="width: 100%; margin-left: 0%;">
                        <h4 style="padding-top: 10px; padding-left: 10px; text-align: center">Change Password </h4>
                        <div class="col-lg-12">
                            <hr />
                        </div>

                        <asp:HiddenField ID="HidKey" runat="server" />
 <asp:HiddenField ID="HidKey2" runat="server" />
<%-- <asp:HiddenField ID="HidKey3" runat="server" />--%>
 <asp:HiddenField ID="HidKey4" runat="server" />


                        <div class="row" style="margin: 10px;">

                            <div class=" col-lg-4 ">
                                <b>Old Password[*] :</b>
                                <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" MaxLength="20" class=" form-control" TabIndex="2" Width="100%" AutoCompleteType="Disabled" AutoComplete="Off" oncopy="return false" onpaste="return false" oncut="return false" placeholder="Enter Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOldPassword"
                                    ErrorMessage="Enter Old Password !" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                            </div>


                            <div class=" col-lg-4 ">
                                <b>New Password[*] :</b>
                                <asp:TextBox ID="txtNewPwd" TextMode="Password" runat="server" MaxLength="20" class=" form-control" TabIndex="2" Width="100%" AutoCompleteType="Disabled" AutoComplete="Off" oncopy="return false" onpaste="return false" oncut="return false" placeholder="Enter Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPwd"
                                    ErrorMessage="Enter New Password !" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                            </div>

                            <div class=" col-lg-4 ">
                                <b>Confirm Password[*] :</b><br />
                                <asp:TextBox ID="txtRePwd" runat="server" TextMode="Password" MaxLength="20" class=" form-control" TabIndex="3" Width="100%" AutoCompleteType="Disabled" AutoComplete="Off" oncopy="return false" onpaste="return false" oncut="return false" placeholder="Enter confirm Password"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPwd"
                                    ControlToValidate="txtRePwd" ForeColor="#990099" ErrorMessage="New and RePassword does not match !"
                                    SetFocusOnError="True">*</asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter RePassword !"
                                    ControlToValidate="txtRePwd" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                            </div>

                            <div class=" col-lg-12 ">
                                <%-- <cc1:CaptchaControl ID="ccJoin" runat="server" CaptchaBackgroundNoise="None" CaptchaLength="4"
                                    CaptchaHeight="35" CaptchaWidth="110" CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                    CaptchaMaxTimeout="300" FontColor="#529E00" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="1px" Width="135px" BackColor="White" />--%>
                             <%--   <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                                    <ContentTemplate>--%>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <asp:Image ID="imgCaptcha" runat="server" Width="225" Height="50" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="1px" ImageUrl="~/CreateCaptcha.aspx" />
                                            </div>
                                            <div class="col-sm-1">
                                                <center>
                                                    <asp:ImageButton ID="reloadcaptcha" runat="server" Style="height: 40px; border-width: 0px; margin-left: -150px; margin-top: 7px;" CausesValidation="false" OnClientClick="refreshcaptcha();"  ToolTip="Refresh Captcha" />
                                                </center>
                                            </div>
                                            <div class="col-sm-8">
                                            </div>
                                            <br />
                                        </div>
                                  <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>

                            </div>

                            <div class=" col-lg-12 ">
                                <br />
                                <b>Enter Code[*]</b>


                                <asp:TextBox ID="txtCaptha" runat="server" MaxLength="6" CssClass="form-control" Width="50%" AutoCompleteType="Disabled"
                                    autocomplete="off" oncopy="return false" onpaste="return false" oncut="return false" placeholder="Enter the Above shown Captcha" onkeypress="return isAlphanumeric(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="revCaptcha" ControlToValidate="txtCaptha" runat="server"
                                    Style="color: #800000"></asp:RequiredFieldValidator>


                                <%-- <asp:TextBox ID="txtCaptha" runat="server" TabIndex="4" class=" form-control" Width="50%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Code !"
                                    ControlToValidate="txtCaptha" ForeColor="#990099">*</asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="col-lg-4">&nbsp;</div>
                            <div class="col-lg-4">
                                <asp:Button ID="btnSubmit" runat="server" Text="Change Password" OnClientClick="return GeneratePwd();" OnClick="btnSubmit_Click" TabIndex="5" CssClass="btnbox btn btn-success" Height="50" Width="100%" Font-Size="Larger" />
                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                            </div>
                            <div class="col-lg-4">
                                <asp:Button ID="btnClose" runat="server" Text="Close" ValidationGroup="no" OnClick="btnClose_Click" TabIndex="6" CssClass="btnbox btn btn-info" Height="50" Width="100%" Font-Size="Larger" />

                            </div>
                            <div class=" col-lg-12 ">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="s" />
                            </div>
                            <div class=" col-lg-12">
                                <label style="color: red">New Password must be 8-10 characters long with  At least one upper case english letter [A-Z], At least one lower case english letter[a-z], at least one numeric character [0-9] and one special Character[# ? ! @ $ % *].</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
