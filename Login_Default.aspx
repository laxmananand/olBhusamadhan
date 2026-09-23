<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.master" AutoEventWireup="true" CodeFile="Login_Default.aspx.cs" Inherits="Login_Default" %>

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
            // document.getElementById('my-image').setAttribute('src', '~/CreateCaptcha.aspx');
            sessionStorage.setItem("password", document.getElementById('<%= txtPassword.ClientID %>').value);
            document.getElementById('<%= txtPassword.ClientID %>').value = '';
            document.getElementById('imgCaptcha').src = '<%= ResolveUrl("~/CreateCaptcha.aspx") %>';
            document.getElementById('<%= txtPassword.ClientID %>').value = sessionStorage.getItem("password");
            return false;
        }
        //function SubmitsEncry(e, k, v) {
        //    var key = CryptoJS.enc.Utf8.parse(k);
        //    var iv = CryptoJS.enc.Utf8.parse(v);
        //    var encryptedlogin = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(e), key,
        //        {
        //            keySize: 128 / 8,
        //            iv: iv,
        //            mode: CryptoJS.mode.CBC,
        //            padding: CryptoJS.pad.Pkcs7
        //        });
        //    return encryptedlogin;
        //}

        function GeneratePwd() {
            if (validate()) {

                if (document.getElementById('<%= txtPassword.ClientID %>').value != "") {

            var step1 = Math.floor(Math.random() * 800000) + 100000;
                    var salted = step1 + document.getElementById('<%= txtPassword.ClientID %>').value;
                    var saltedpass = step1 + document.getElementById('<%= txtPassword.ClientID %>').value;
            var saltedu = step1 + document.getElementById('<%= txtUserName.ClientID %>').value;  
            // alert(newKey);
            document.getElementById('<%= HidKey.ClientID %>').value = encrypt(salted);
            document.getElementById('<%= HidKey2.ClientID %>').value = encrypt(saltedu);

                    document.getElementById('<%= txtPassword.ClientID %>').value = encrypt(saltedpass);
                    //alert(document.getElementById('<%= txtPassword.ClientID %>').value);
                }

                return true;
            }
            else
                return false;
        }

      <%--  function GeneratePwd() {
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
                             var hiddenField = document.getElementById('<%= HidKey.ClientID %>');
                             hiddenField.value = "";
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
            else {

                return false;
            }
        }--%>

        function validate() {
            try {
                if (document.getElementById('<%= txtUserName.ClientID %>').style.display != 'none') {
                 var len = document.getElementById('<%= txtUserName.ClientID %>').value;
                 if (len.length < 1) { alert('Username can not be blank'); return false; }
                 if (len.length > 15) { alert('Username length is more than 15 characters'); return false; }
             }
         }
         catch (exception) {
         }
         var len1 = document.getElementById('<%= txtPassword.ClientID %>').value;
         if (len1.length < 1) { alert('Password should not be blank.'); return false; }
         if (len1.length < 4) { alert('Password length should not be less than 4 characters'); return false; }

         var len2 = document.getElementById('<%= txtCaptha.ClientID %>').value;
            if (len2.length < 6) { alert('Captcha length is less than 6 characters'); return false; }
            if (len2.length > 6) { alert('Captcha length is more than 7 characters'); return false; }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:HiddenField ID="HidKey" runat="server" />
  <asp:HiddenField ID="HidKey2" runat="server" />
<%--<asp:HiddenField ID="HidKey3" runat="server" />--%>

    <div class="row no-gutters">
        <div class="col-md-9">
            <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
                <ol class="carousel-indicators">
                    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
                </ol>
                <div class="carousel-inner">
                   <div class="carousel-item active">
                        <img class="d-block w-100" src="images/Desktop1.jpg" alt="First slide">
                        <div class="carousel-caption d-none d-md-block">
                            <h5>भू-समाधान</h5>
                            <p>गृह-विभाग | बिहार सरकार</p>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img class="d-block w-100" src="images/Desktop1.jpg" alt="Second slide">
                        <div class="carousel-caption d-none d-md-block">
                            <h5>भू-समाधान</h5>
                            <p>गृह-विभाग | बिहार सरकार</p>
                        </div>
                    </div>
                    <div class="carousel-item">
                        <img class="d-block w-100" src="images/Desktop1.jpg" alt="Third slide">
                        <div class="carousel-caption d-none d-md-block">
                            <h5>भू-समाधान</h5>
                            <p>गृह-विभाग | बिहार सरकार</p>
                        </div>
                    </div>
                </div>

                <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
        </div>
                     
        <div class="col-md-3">
       <%--     <center style="padding:5px; margin-top:5px;">
                                    <asp:RadioButtonList ID="rblogintype" runat="server" AutoPostBack="true"
    RepeatDirection="Horizontal" TextAlign="Right"
    RepeatLayout="Table"
    OnSelectedIndexChanged="rblogintype_SelectedIndexChanged"
    onchange="Loginpanelhide();">
    <asp:ListItem Text="Official Login" Value="0"></asp:ListItem>
    <asp:ListItem Text="Public Login" Value="1"></asp:ListItem>
</asp:RadioButtonList>
                </center>--%>
            <div class="card" id="dvdept" runat="server" >
               <%-- start change--%>
    
               <%-- end change--%>
                <div class="card-title text-white text-center p-3" style="font-size: 17px; background-image: linear-gradient(to bottom, #19007c, #003997, #005392, #00687c, #08796b); padding: 10px; box-shadow: rgba(0, 0, 0, 0.4) 0px 2px 4px, rgba(0, 0, 0, 0.3) 0px 7px 13px -3px, rgba(0, 0, 0, 0.2) 0px -3px 0px inset;">Official Login</div>
                <%-- <asp:UpdatePanel ID="up1" runat="server">
                                <ContentTemplate>--%>

                <div class="card-body">
                    <asp:Label ID="Label1" runat="server" Style="color: darkblue" Text="Username"></asp:Label>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control mb-3" placeholder="User ID*" AutoCompleteType="Disabled" AutoComplete="Off" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Style="color: darkblue" Text="Password"></asp:Label>
                    
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control mb-3" TextMode="Password" placeholder="Password*" AutoCompleteType="Disabled" AutoComplete="Off" onpaste="return false" oncopy="return false" oncut="return false"></asp:TextBox>
                    <%--<asp:Label ID="Label3" runat="server" style="color:darkblue" Text="Enter Captcha"></asp:Label>--%>

                    <asp:TextBox ID="txtCaptha" runat="server" CssClass="form-control mb-3" placeholder="Enter Code*" AutoCompleteType="Disabled" AutoComplete="Off" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                    <%-- <cc1:CaptchaControl ID="ccJoin" runat="server" CaptchaBackgroundNoise="None" CaptchaLength="6"
                        CaptchaHeight="35" CaptchaWidth="250" CaptchaMinTimeout="5" CaptchaMaxTimeout="240" FontColor="#529E00" 
                         BorderColor="Black" BorderStyle="Solid" BackColor="White" CssClass="Captcha-control" />--%>
                 <%--   <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                            <center>
                                <asp:Image ID="imgCaptcha" runat="server" Width="230" Height="50" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" ImageUrl="CreateCaptcha.aspx" style="margin-bottom: 10px;" />


                                <asp:ImageButton ID="reloadcaptcha" ImageUrl="images/refresh.png" runat="server" Height="36" OnClientClick="refreshcaptcha();" ToolTip="Refresh Captcha" />
                                                             

                            </center>
                       <%-- </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <center>


                        <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Red" NavigateUrl="ForgotPassword.aspx">Forgot Your password?</asp:HyperLink></center>
                    <br />
                    <center>
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-outline-danger" OnClick="btnLogin_Click"
                            OnClientClick=" return GeneratePwd();" /></center>
                    <%--ForgotPass.aspx --%>                                                           
                        &nbsp;&nbsp;
                      <span>
                          <asp:Label ID="lblmsgerror" runat="server" ForeColor="red"></asp:Label></span>
                </div>

                <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>



</asp:Content>

