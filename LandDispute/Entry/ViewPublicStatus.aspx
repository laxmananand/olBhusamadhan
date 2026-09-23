<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master"  CodeFile="ViewPublicStatus.aspx.cs" Inherits="LandDispute_Entry_Unfinalize" %>

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
        // urlpdf = ("http://localhost:8080" + urlpdf).replace(' ', '');
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
      <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    <div class="container-fluid">
          <h4 class="text-black text-center"><b>सार्वजनिक प्रविष्टि</b></h4>

        
        <div class="row">
                    <center>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </center>
                </div> 
        <div class="card">                                          
                                             <div class="card-body">                                                             
                                                                 <div class="row">                     
                    <div class="col-md-3">
                               <label class="control-label" for="inputGroup"> Paze Size </label>
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed" CssClass="form-control mb-2" >
                                                         <asp:ListItem Text="10" Value="10" />
                                                         <asp:ListItem Text="25" Value="25" />
                                                         <asp:ListItem Text="50" Value="50" />
                                                     </asp:DropDownList>
                            </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label" for="inputGroup">वादी (मोबाइल संख्या) / Application No. </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" CssClass="form-control" placeholder="Search" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>                            
                            <span class="input-group-addon">
                            <i class="fa fa-search"></i>
                            </span>
                            </div>
                        </div>
                    </div>
                    </div>

                 <div class="row"> 
                     <div class="col-md-12" style="font-weight: bolder;padding-top:5px;">
                         <asp:GridView runat="server" Width="100%" ID="grdMatterRegistration" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid fontsize" DataKeyNames="a_id" OnRowCommand="grdMatterRegistration_RowCommand"  >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>                                        
                                        
                                      
                                        
                                          <asp:TemplateField HeaderText="Application No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
      <ItemTemplate>
         <%#Eval("ApplicationNo")%>
      </ItemTemplate>
      <ItemStyle HorizontalAlign="Left" />
  </asp:TemplateField>


                             <asp:TemplateField HeaderText="जिला <hr style='margin-bottom: 0px; margin-top: 0px;' /> अनुमंडल <hr style='margin-bottom: 0px; margin-top: 0px;' /> अंचल" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <%#Eval("DISTRICTNAME")%>
                                     <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                      <%#Eval("Sd_Name_En")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("BlockName")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>



                                        <asp:TemplateField HeaderText="थाना <hr style='margin-bottom: 0px; margin-top: 0px;' /> ग्राम पंचायत  <hr style='margin-bottom: 0px; margin-top: 0px;' /> राजस्व ग्राम/मौजा" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <%#Eval("Police_Station")%>
                                     <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                      <%#Eval("PanchayatName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("VILLNAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                                         <asp:BoundField DataField="vadi_Name" HeaderText="वादी का नाम(कुल)" ItemStyle-Width="10%" />  
                                         <asp:BoundField DataField="Vadi_MobileNo" HeaderText="वादी का मोबाइल संख्या" ItemStyle-Width="10%" />  
                                         <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी का नाम(कुल)" ItemStyle-Width="12%" />                                      
                                         <asp:BoundField DataField="Bhumitype" HeaderText="भूमि का प्रकार" ItemStyle-Width="10%" />  
                                         <asp:BoundField DataField="vivadtype" HeaderText="भूमि विवाद का प्रकार" ItemStyle-Width="18%" />                                                                                  
                                           <asp:BoundField DataField="StatusChange" HeaderText="Status" ItemStyle-Width="18%" />                                                                                                                                                                                                                             
                                    </Columns>
                                </asp:GridView>
                         </div>
                     </div>
                <div class="row">
                     <div class="form-group text-center" style="padding-top: 8px; padding-bottom: 8px; border: none; ">
                                                     <div class="col-md-12">
                                                            <asp:Repeater ID="rptPager" runat="server" >
                                                                 <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' Enabled = '<%# Eval("Enabled") %>' OnClick = "Page_Changed"></asp:LinkButton>
                                                                 </ItemTemplate>
                                                             </asp:Repeater>
                                                     </div>
                                            </div>
                </div>


                                                                                           
                                                                                         
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