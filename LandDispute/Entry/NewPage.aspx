<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="NewPage.aspx.cs" Inherits="LandDispute_Entry_AddMettingApplication" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <style type="text/css" >
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
       <script type="text/javascript">
    function fnLinkbutton1(objlinkbutton) {

        var urlpdf = document.getElementById(objlinkbutton).getAttribute("path");
        var urlpdfOr = jQuery.trim(urlpdf)
        getpdfdocument(urlpdfOr);

        return false;
    }

    function getpdfdocument(urlpdf) {
        var imgDiv = document.getElementById("divImage");
        var inlineFrameExample = document.getElementById("inlineFrameExample");
        urlpdf = urlpdf.replace("~", "");
        // urlpdf = ("http://10.133.25.21/ImageServiceHome" + urlpdf).replace(' ', '');
        urlpdf = ("http://localhost:8080" + urlpdf);
        urlpdf = urlpdf.trim();
        $.ajax({
            type: "POST",
            url: "SearchAppForMetting.aspx/Getpdf",
            data: "{'url':'" + urlpdf + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                inlineFrameExample.src = response.d;
            },
            failure: function (msg) {
                alert(msg);
            }
        });



        var width = document.body.clientWidth;
        imgDiv.style.left = (width - 1200) / 2 + "px";
        imgDiv.style.top = "10px";
        imgDiv.style.display = "block";
    }

    function HideDiv() {
        var bcgDiv = document.getElementById("divBackground");
        var imgDiv = document.getElementById("divImage");
        var imgFull = document.getElementById("imgFull");
        imgDiv.style.display = "none";
        
    }
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <AjaxControlToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></AjaxControlToolkit:ToolkitScriptManager> 
    
    <div class="container-fluid">
          <h4 class="text-black text-center"><b>आवेदन का विवरण</b></h4>       
        <div class="row">
             <center>
                 <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
             </center>
       </div> 
                <div class="card">                                         
                        <div class="card-body">
                                  <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                                       <ContentTemplate>                                                             
                                       </ContentTemplate>                                      
                                  </asp:UpdatePanel>                                                                                         
                        </div>
                </div>
        </div>

    <div id="divImage">
            <table style="height: 100%; width: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img id="imgLoader" alt="" src="../img/loadern.gif" />
                        <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
                        <iframe id="inlineFrameExample" title="Inline Frame Example"  width="99%" height="600px" src=""></iframe>
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="bottom">
                        <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>