<%@ Page Language="C#" AutoEventWireup="true" CodeFile="priya.aspx.cs" Inherits="LandDispute_Default"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<head>     
    <script src="JS/userapplicationvalidation.js"></script>
     <style type="text/css">
        .zindex {
            background-color: #FAF5EF;
            z-index:10001;
        }
    </style>
  <script src="bhusamadhan/vendor/jquery/jquery.min.js"></script>
  <script src="bhusamadhan/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
  <script src="bhusamadhan/vendor/jquery-easing/jquery.easing.min.js"></script>
  <script src="bhusamadhan/vendor/chart.js/Chart.min.js"></script>
  <script src="bhusamadhan/js/demo/chart-area-demo.js"></script>
  <script src="bhusamadhan/vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
  <script src="bhusamadhan/js/ruang-admin.min.js"></script>
  <script src="JS/Jquerry2000min.js" type="text/javascript"></script>
       
          <script type="text/javascript">
         function fnLinkbutton(objlinkbutton) {
             var urlpdf = document.getElementById(objlinkbutton).getAttribute("path");
             getpdfdocument(urlpdf);
             return false;
              }                    
         function getpdfdocument(urlpdf) {            
             var imgDiv = document.getElementById("divImage");
             var inlineFrameExample = document.getElementById("inlineFrameExample");
             urlpdf = urlpdf.replace("~", "");
             //urlpdf = ("http://localhost:8080" + urlpdf).replace(' ', '');
             urlpdf = ("http://localhost:8080/LandDoc/Upload/test.pdf" );         
             urlpdf = urlpdf.trim();
             $.ajax({
                 type: "POST",
                 url: "priya.aspx/Getpdf",
                 data: "{'url':'" + urlpdf + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (response) {                   
                     window.open("LandDispute/IDoc.aspx?url=" + response.d, "_blank");
                 },
                 failure: function (msg) {
                     alert(msg);
                 }
             });
             //var width = document.body.clientWidth;
             //imgDiv.style.left = (width - 1200) / 2 + "px";
             //imgDiv.style.top = "10px";
             //imgDiv.style.display = "block";
              }


         $(document).ready(function () {
         
            
                 var urlpdf = "~/LandDoc/Upload/test.pdf";
                 getpdfdocument(urlpdf);
                 return false;
            
           
         });
         function js(url) {



             return false;
         }

             
         function HideDiv() {
             var bcgDiv = document.getElementById("divBackground");
             var imgDiv = document.getElementById("divImage");
             var imgFull = document.getElementById("imgFull");
             imgDiv.style.display = "none";
              }

         function ValidateAlpha(evt) {
             var keyCode = (evt.which) ? evt.which : evt.keyCode
             if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && (keyCode != 32))

                 return false;
             return true;
              }

          </script>
          <style type="text/css">
        .rightpadding{
                padding-right: 700px !important;
        }
    </style>
          <style type="text/css">
            .ActiveClass
            {
                color: darkblue;
                padding: 5px;
                border-radius: 5px;
                text-decoration: none;
                font-weight:500;
                background: linear-gradient(to right, #1B990E 0%, #B013CF 100%);
                -webkit-background-clip: text;
                -webkit-text-fill-color: transparent;
            }
            .RemoveClass{
                color: lightgray;
                padding: 5px;
                border-radius: 5px;
                text-decoration: none;
                font-weight:500;
            }
        </style>
          <style>
               div.Break_Word_next_Line{
   width: 600px !important;
    word-wrap: break-word !important;

}
            hr {
                margin-top: 0px;
                margin-bottom: 0px;
                color:yellow;
            }
            .labeltextsize {
                  padding: 5px;
            }
            .myfont1{
                font-size: unset !important;
            }
        </style>
          <style type="text/css">
            .Background
            {
                background-color: Black;
                filter: alpha(opacity=90);
                opacity: 0.8;
            }
            .Popup
            {
                background-color: #FFFFFF;
                border-width: 3px;
                border-style: solid;
                border-color: black;
                padding-top: 10px;
                padding-left: 10px;
                width: 400px;
                height: 350px;
            }
            .lbl
            {
                font-size:16px;
                font-style:italic;
                font-weight:bold;
            }
        </style>       
          <%--css for print--%>
          <style type="text/css" media="screen">
           .btnvadiprint
            {
                color: #686464 !important;
                opacity: inherit !important;
                background:#66bb6a !important;
                font-size: unset !important;             
            }
           #divImage
            {
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
          </style>
       
</head><body>
    <form id="frm" runat="server">
        <asp:Label ID="lbl" runat="server"></asp:Label>
    </form>
       </body>

