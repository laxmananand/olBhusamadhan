<%@ Page Title="" Language="C#" MasterPageFile="~/LandDispute/Samvida.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="LandDispute_Common_ChangePassword" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <script src="../menu/jquery.min.js" type="text/javascript"></script>
<script src="../menu/script.js"></script>
<script src="../js/menu.js"></script>





    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/font.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/li-scroller.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/slick.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/jquery.fancybox.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/theme.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/style.css" />

    

<style type="text/css">
        fieldset
        {
            border: 1px solid #0f2c62;
            border-radius: 3px;
            padding: 10px;
            background-color: White;
        }
        legend
        {
            background-color: #014e9c;
            color: #fff;
            padding: 3px 6px;
            font-weight: 100;
            text-align: center;
        }
        .grid th
        {
            background-color: #014e9c;
            color: White;
            border-collapse: collapse;
            padding: 10px;
            border-color: Black;
            text-align: left;
        }
        .grid tr
        {
            background-color: White;
            color: Black;
            border-collapse: collapse;
            padding: 10px;
        }
        .grid td
        {
            text-align: left;
        }
        
        .grid th:first-child
        {
            background-color: #014e9c;
            -moz-border-radius-topleft: 14px;
            -webkit-border-top-left-radius: 14px;
            border-top-left-radius: 14px;
            border-collapse: collapse;
            cellpadding: 0px;
            cellspacing: 0px;
        }
        .grid th:last-child
        {
            background-color: #014e9c;
            -moz-border-radius-topright: 14px;
            -webkit-border-top-right-radius: 14px;
            border-top-right-radius: 14px;
        }
        .grid tr:hover
        {
            background-color: #EEE5CE;
        }
        
        .grid tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        .grid tr:hover
        {
            background-color: #ddd;
        }
    </style>
   
    <style type="text/css">
        .dropdown-menu > li > a
        {
            color: Black !important;
            background-color: White !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <div id="contentSection">        
        <div class="row">
            <div class="col-lg-2 col-md-2 col-sm-2">
            </div>
            <div class="col-lg-8 col-md-8 col-sm-8">
            <div style="background-color:white;">
            <div class=" sb-nav-fixed ">
                <br />   
                 <div class="panel panel-primary">
      <div class="panel-heading"><div class="card-header text-center">Change Password </div></div>
      <div class="panel-body">
       <div class="card text-left"  style="width: 100%; margin-left: 0%;">
                    
                    
                    <div class="row" style="margin: 10px;">
                        

                        <div class=" col-lg-6 ">
                            <b>Name[*] :</b>
                            <br />
                            
                            <asp:TextBox ID="txtName" runat="server" MaxLength="50" class=" form-control text-capitalize" TabIndex="1" Width="100%" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Name !"
                                ControlToValidate="txtName" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                        </div>
                         <div class=" col-lg-6 col-md-6 ">
                            <b>Mobile Number[*] :</b><br />
                           
                            <asp:TextBox ID="txtMobileNo"  runat="server" MaxLength="10" Text="" onkeypress="return validate(event)" class=" form-control" TabIndex="1" Width="100%"></asp:TextBox>
                          
                        </div>
                    </div>
                 
                    <div class="row" style="margin: 10px;">

                        <div class=" col-lg-6 ">
                            <b>New Password[*] :</b>
                            <asp:TextBox ID="txtNewPwd" runat="server" Text="" TextMode="Password" MaxLength="20" class=" form-control" TabIndex="2" Width="100%"></asp:TextBox>
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
 <div class=" col-lg-6">
                            <b>Captcha</b><cc1:CaptchaControl ID="ccJoin" runat="server" CaptchaBackgroundNoise="None" CaptchaLength="4"
                                CaptchaHeight="35" CaptchaWidth="110" CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                CaptchaMaxTimeout="300" FontColor="#529E00" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="1px" Width="135px" BackColor="White" />
                        </div>

                        <div class=" col-lg-6 ">
                            <b>Enter Code[*]</b>
                            <asp:TextBox ID="txtCaptha" runat="server" TabIndex="4" class=" form-control" Width="50%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Code !"
                                ControlToValidate="txtCaptha" ForeColor="#990099" ValidationGroup="1">*</asp:RequiredFieldValidator>
                        </div>

                      
                        <div class="col-lg-4">&nbsp;</div>
                        <div class="col-lg-4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Change Password" ValidationGroup="s"
                                 TabIndex="5" CssClass=" btn btn-success" Height="50" Width="100%" 
                                Font-Size="Larger" onclick="btnSubmit_Click" />
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
                 </div>
            </div>
        </div> 
  <script type="text/javascript">
      function rcaptcha() { document.getElementById('Captcha').src = "../captcha.ashx?id=" + Math.random(); } rcaptcha();</script> 
</asp:Content>

