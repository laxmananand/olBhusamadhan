
<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.master" AutoEventWireup="true" CodeFile="Login_Default_new.aspx.cs" Inherits="Login_Default" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .carousel .carousel-indicators li {
            background-color: #fff;
            background-color: rgba(70,70,70,.25);
        }

        .carousel .carousel-indicators .active {
            background-color: #444;
        }


        h1 {
            margin: 60px auto;
            text-align: center;
        }

            h1 > small {
                color: #999;
            }

        img {
            width: 100%;
        }

        .carousel .carousel-caption {
            color: #999;
        }
    </style>
    <script>
        $('.carousel').carousel()
    </script>
    <script src="Styles/aes.js" type="text/javascript"></script>
     <script type="text/javascript">

         function refreshcaptcha() {
             
             //$.ajax(
             //    {
             //        type: "POST",
             //        contentType: "application/json;charset=utf-8",
             //        url: "login_default.aspx/captcha",
             //        //data: JSON.stringify({ DistrictId: districtCode, ThanaCode: ThanaCode }),
             //        dataType: "json",
             //        success: function (data) {
                         
             //        },
             //        error: function (XMLHttpRequest, textStatus, errorThrown) {
             //            if (XMLHttpRequest.status == 0) {
             //                alert(' Check Your Network.');
             //            } else if (XMLHttpRequest.status == 404) {
             //                alert('Requested URL not found.');
             //            } else if (XMLHttpRequest.status == 500) {
             //                alert('Internel Server Error.');
             //            } else {
             //                alert('Unknow Error.\n' + XMLHttpRequest.responseText);
             //            }
             //        }
             //    });
             //return false;
         }



         function refreshcaptcha(event) {
             // document.getElementById('my-image').setAttribute('src', '~/CreateCaptcha.aspx');
            // event.preventDefault();
            // document.getElementById('imgCaptcha').src = "~/CreateCaptcha.aspx";
             // return false;
             sessionStorage.setItem("password", document.getElementById('<%= txtPassword.ClientID %>').value);
             document.getElementById('<%= txtPassword.ClientID %>').value = '';
             document.getElementById('imgCaptcha').src = '<%= ResolveUrl("~/CreateCaptcha.aspx") %>';
             document.getElementById('<%= txtPassword.ClientID %>').value = sessionStorage.getItem("password");
             return false;
         }


    function SubmitsEncry(e, k , v) 
{
    var key = CryptoJS.enc.Utf8.parse(k);
    var iv = CryptoJS.enc.Utf8.parse(v);        
            var encryptedlogin = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(e), key,
                 {
                     keySize: 128 / 8,
                     iv: iv,
                     mode: CryptoJS.mode.CBC,
                     padding: CryptoJS.pad.Pkcs7
                });
        return encryptedlogin;
         }
         function GeneratePwd() {
             //debugger;
             if (validate()) {
                 if (document.getElementById('<%= txtPassword.ClientID %>').value != "") {
                     var str3 = '';
                     var step1 = Math.floor(Math.random() * 800000) + 100000;
                     var salted = step1 + document.getElementById('<%= txtPassword.ClientID %>').value;
                     document.getElementById('<%= txtPassword.ClientID %>').value = SubmitsEncry(salted, document.getElementById('<%= HidKey.ClientID %>').value, document.getElementById('<%= HidKey.ClientID %>').value);
                     try {

                         if (document.getElementById('<%= txtUserName.ClientID %>').style.display != 'none') {
                             document.getElementById('<%= txtUserName.ClientID %>').type = 'password';
                             var saltedu = step1 + document.getElementById('<%= txtUserName.ClientID %>').value;
                             document.getElementById('<%= txtUserName.ClientID %>').value = SubmitsEncry(saltedu, document.getElementById('<%= HidKey.ClientID %>').value, document.getElementById('<%= HidKey.ClientID %>').value);
                         }
                     }
                     catch
                     {
                         alert('Something went wrong');
                         
                         return false;
                     }
                 }
                 return true;
             }
             else
             {
                
                 return false;
             }
     }

     function validate()
     {
         try {
             if (document.getElementById('<%= txtUserName.ClientID %>').style.display != 'none') {
                 var len = document.getElementById('<%= txtUserName.ClientID %>').value;
                 if (len.length < 1) { alert('Username can not be blank'); return false; }
                 if (len.length > 15) { alert('Username length is more than 15 characters'); return false; }
             }            
           }
           catch(exception)
           {
            }
             var len1 = document.getElementById('<%= txtPassword.ClientID %>').value;
             if (len1.length < 1) { alert('Password should not be blank.'); return false; }
         if (len1.length < 4) { alert('Password length should not be less than 4 characters'); return false; }

        // var len2 = document.getElementById('<%= txtCaptha.ClientID %>').value;
         //if (len2.length <6) { alert('Captcha length is less than 4 characters'); return false; }
         //if (len2.length > 6) { alert('Captcha length is more than 5 characters'); return false; }
         return true;
     }
     </script>
    <style type="text/css">
        .img-hover-effect:hover {
        transform: scale(1.05);

    }
        table[id$="rblogintype"] td {
    padding-right: 20px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="HidKey" runat="server" />
  <asp:HiddenField ID="HidKey2" runat="server" />
<%--<asp:HiddenField ID="HidKey3" runat="server" />--%>

    <div class="container">
    <div class="row no-gutters mt-4">
        <%--<div class="col-md-1"></div>--%>
        <div class="col-md-7">
            <div class="card">
                <div class="card-body" style="overflow: hidden;">
                    <img src="images/SPB_1.png" style="min-height: 250px; width: 100%; transition: transform 0.3s ease; object-fit: cover; padding:5px;" class="img-hover-effect" />
                </div>
            </div>
        </div>
        <div class="col-md-4" runat="server" visible="false">
            <div class="card">
                <center style="padding:5px;">
                <asp:RadioButtonList ID="rblogintype" runat="server" AutoPostBack="true"
    RepeatDirection="Horizontal" TextAlign="Right"
    RepeatLayout="Table"
    OnSelectedIndexChanged="rblogintype_SelectedIndexChanged"
    onchange="Loginpanelhide();">
    <asp:ListItem Text="Official Login" Value="0"></asp:ListItem>
    <asp:ListItem Text="Public Login" Value="1" Selected="True"></asp:ListItem>
</asp:RadioButtonList>
                </center>
                <div class="card-body" id="dvdept" runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="Label1" runat="server" Style="color: darkblue" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control mb-3" placeholder="User ID*" AutoCompleteType="Disabled" AutoComplete="Off" oncopy="return false" ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="Label2" runat="server" Style="color: darkblue" Text="Password"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mb-3" TextMode="Password" placeholder="Password*" AutoCompleteType="Disabled" AutoComplete="Off" ></asp:TextBox>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-10">
                            <img src="CreateCaptcha.aspx" id="imgCaptcha" alt="Captcha Image" class="form-control" style="min-height: 70px;" />
                        </div>
                        <div class="col-md-2"><br />
                            <button id="reloadcaptcha" onclick="refreshcaptcha(event);" title="Refresh Captcha" style="border: none; background: none; cursor: pointer;">
                                <img src="images/refresh.png" alt="Refresh Captcha" height="36" width="36" />
                            </button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:TextBox ID="txtCaptha" runat="server" CssClass="form-control mt-3" placeholder="Enter Above Code*" AutoCompleteType="Disabled" AutoComplete="Off" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                        </div>
                    </div>
                    <br />
                    <center>
                        <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Red" NavigateUrl="ForgotPassword.aspx">Forgot Your password?</asp:HyperLink></center>
                    <center>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success" OnClick="btnLogin_Click"
                            OnClientClick=" return GeneratePwd();" /></center>
                    &nbsp;&nbsp;
                   
                      <span>
                          <asp:Label ID="lblmsgerror" runat="server" ForeColor="red"></asp:Label></span>
                </div>

               
            </div>
        </div>

                   <div class="col-md-1">
                    
                        <div id="dvpublic" runat="server" visible="true">
    
 <asp:Button ID="btnpubliclogin" runat="server" Text="Login with JanParichay" OnClick="btnpubliclogin_Click"  CssClass="btn btn-primary" 
            />
</div>
          
           </div>

        <%--<div class="col-md-1"></div>--%>
    </div>

    </div>

</asp:Content>

