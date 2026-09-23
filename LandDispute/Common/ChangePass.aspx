<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LandDispute/Samvida.master"  CodeFile="ChangePass.aspx.cs" Inherits="CLG_ChangePass" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/font.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/li-scroller.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/slick.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/jquery.fancybox.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/theme.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/style.css" />

    <script language="javascript" type="text/javascript">
        function checkmobno() {
            var txt = document.getElementById("<%=txtMobileNo.ClientID %>").value;
             var mobile_prefix = txt.substring(0, 2);
             var lnth = txt.length;
             if (lnth >= 2) {

                 if (mobile_prefix == 62 || mobile_prefix == 70 || mobile_prefix == 71 || mobile_prefix == 72 || mobile_prefix == 73 || mobile_prefix == 74 || mobile_prefix == 75 || mobile_prefix == 76 || mobile_prefix == 77
             || mobile_prefix == 78 || mobile_prefix == 79 || mobile_prefix == 80 || mobile_prefix == 81 || mobile_prefix == 82 || mobile_prefix == 83 || mobile_prefix == 84 || mobile_prefix == 85
             || mobile_prefix == 86 || mobile_prefix == 87 || mobile_prefix == 88 || mobile_prefix == 89 || mobile_prefix == 90 || mobile_prefix == 91 || mobile_prefix == 92 || mobile_prefix == 93
             || mobile_prefix == 94 || mobile_prefix == 95 || mobile_prefix == 96 || mobile_prefix == 97 || mobile_prefix == 98 || mobile_prefix == 99) {
                 }
                 else {
                     alert('Enter Valid Mobile Number.');
                     document.getElementById("<%=txtMobileNo.ClientID %>").value = '';
                 }
             }
         }
        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && (keyCode != 32))

                return false;
            return true;
        }

         function validate(evt) {

             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
         }
    </script>
    <style type="text/css">
         .baseBlock
        {
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
         .btnbox
         {
             box-shadow: 0 25px 30px rgb(0 0 0 / 50%);
         }
    </style>
</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="contentSection">        
        <div class="row">
            <div class="col-lg-8 col-lg-offset-2">
            <div class=" sb-nav-fixed ">                     
                <div class=" text-left"  style="width: 100%; margin-left: 0%;">
                    <div class="card-header text-center text-danger baseBlock"><h2>Change Password </h2></div>
                    <div class="col-lg-12"><hr /></div>
                    <div class="row" style="margin: 10px;">
                        <div class=" col-lg-6 ">
                            <b>User Name[*] :</b>
                            <br />
                            <b id="UserName" runat="server" class=" form-control"></b>
                        </div>

                        <div class=" col-lg-6 ">
                            <b>Name[*] :</b>
                            <br />
                            <asp:TextBox ID="txtName" runat="server" MaxLength="50" class=" form-control text-capitalize" TabIndex="1" Width="100%" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Name !"
                                ControlToValidate="txtName" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="row" style="margin: 10px;">

                        <div class=" col-lg-6 col-md-6 ">
                            <b>Mobile Number[*] :</b><br />
                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" onkeypress="return validate(event)" class=" form-control" TabIndex="1" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Mobile Number !"
                                ControlToValidate="txtMobileNo" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                        </div>
                        <div class=" col-lg-6 ">
                            <br />
                            <asp:Button ID="MGetOtpBtn" runat="server" Text="Get OTP" CssClass="btnbox btn btn-danger" Font-Size="Small" OnClick="MGetOtpBtn_Click" />
                            <asp:Panel ID="MpnlVerify" runat="server" Visible="false">
                                <div class="row">
                                    <div class=" col-lg-6 col-md-6">
                                        <asp:TextBox ID="MtxtOTP" runat="server" TextMode="Number" MaxLength="6" placeholder="OTP" Width="100%" class=" form-control"></asp:TextBox>
                                        <asp:LinkButton ID="MlnkResend" runat="server" CssClass=" float-lg-right"  OnClick="MlnkResend_Click">Resend OTP</asp:LinkButton>
                                    </div>
                                    <div class=" col-lg-6 col-md-6">
                                        <asp:Button ID="btnVerify" runat="server" Text="Verify" CssClass="btnbox btn btn-success"
                                            Font-Size="Small" OnClick="btnVerify_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="MpnlEdit" runat="server" Visible="false">
                                <div class="row">
                                    <div class=" col-lg-6 col-md-6">

                                        <asp:Label ID="MVerifyStatus" runat="server" Text="" CssClass="  btn-success form-control font-italic font-weight-bold" Font-Size="Small"></asp:Label>

                                    </div>
                                </div>
                            </asp:Panel>

                        </div>

                    </div>
                    <div class="row" style="margin: 10px;">
                        <div class=" col-lg-6 col-md-6 ">
                            <b>Email ID[*] :</b><br />
                            <asp:TextBox ID="txtEmailID" runat="server" MaxLength="50" class=" form-control" TabIndex="1" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Enter Emial ID !"
                                ControlToValidate="txtEmailID" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="ExpValidatorTo" runat="server" ErrorMessage="Email is invalid"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" Font-Bold="true" ValidationGroup="a" ControlToValidate="txtEmailID">  
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class=" col-lg-6 col-md-6">
                            <br />
                            <asp:Button ID="EGetOtpBtn" runat="server" Text="Get OTP" CssClass="btnbox btn btn-danger" Font-Size="Small" ValidationGroup="a" OnClick="EGetOtpBtn_Click" />
                            <asp:Panel ID="EpnlVerify" runat="server" Visible="false">
                                <div class="row">
                                    <div class=" col-lg-6 col-md-6">
                                        <asp:TextBox ID="EtxtOTP" runat="server" TextMode="Number" MaxLength="6" placeholder="OTP" Width="100%" class=" form-control"></asp:TextBox>
                                        <asp:LinkButton ID="ElnkResend" runat="server" CssClass=" float-lg-right" OnClick="ElnkResend_Click">Resend OTP</asp:LinkButton>
                                    </div>
                                    <div class=" col-lg-6 col-md-6">
                                        <asp:Button ID="EbtnVerify" runat="server" Text="Verify" OnClick="EbtnVerify_Click" CssClass="btnbox btn btn-success" Font-Size="Small" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="EpnlEdit" runat="server" Visible="false">
                                <div class="row">
                                    <div class=" col-lg-6 col-md-6">
                                        <asp:Label ID="EVerifyStatus" runat="server" Text="" CssClass="  btn-success form-control font-italic font-weight-bold"></asp:Label>

                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row" style="margin: 10px;">

                        <div class=" col-lg-6 ">
                            <b>New Password[*] :</b>
                            <asp:TextBox ID="txtNewPwd" runat="server"  MaxLength="20" class=" form-control" TabIndex="2" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPwd"
                                ErrorMessage="Enter New Password !" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                        </div>

                        <div class=" col-lg-6 ">
                            <b>Confirm Password[*] :</b><br />
                            <asp:TextBox ID="txtRePwd" runat="server" TextMode="Password" MaxLength="20" class=" form-control" TabIndex="3" Width="100%"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPwd"
                                ControlToValidate="txtRePwd" ForeColor="#990099" ErrorMessage="New and RePassword does not match !"
                                SetFocusOnError="True" ValidationGroup="s">*</asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter RePassword !"
                                ControlToValidate="txtRePwd" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                        </div>

                        <div class=" col-lg-12 ">
                            <cc1:CaptchaControl ID="ccJoin" runat="server" CaptchaBackgroundNoise="None" CaptchaLength="4"
                                CaptchaHeight="35" CaptchaWidth="110" CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                CaptchaMaxTimeout="300" FontColor="#529E00" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="1px" Width="135px" BackColor="White" />
                        </div>

                        <div class=" col-lg-12 ">
                            <b>Enter Code[*]</b>
                            <asp:TextBox ID="txtCaptha" runat="server" TabIndex="4" class=" form-control" Width="50%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Code !"
                                ControlToValidate="txtCaptha" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-lg-4">&nbsp;</div>
                        <div class="col-lg-4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Change Password" ValidationGroup="s" OnClick="btnSubmit_Click" TabIndex="5" CssClass="btnbox btn btn-success" Height="50" Width="100%" Font-Size="Larger" />
                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Maroon"></asp:Label>
                        </div>
                        <div class=" col-lg-12 ">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="s" />
                        </div>
                    </div>
                </div>
            </div>
                </div>
       </div>
                
        </div>   
    </asp:Content> 