<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ActionDetails.aspx.cs" Inherits="LandDispute_Entry_SearchAppForMetting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                window.open("../../IDoc.aspx?url=" + response.d, "_blank");
                //inlineFrameExample.src = response.d;
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
          <h4 class="text-black text-center"><b>बैठक का निष्कर्ष का विवरण</b></h4>

        
        <div class="row">
                    <center>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </center>
                </div> 
        <div class="card">
                                          
                                             <div class="card-body">
                                                      <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                             <div class="row mb-2">
                                                                    
                                                                    <div class="col-md-2">
                                                                            <asp:Label ID="Label1" runat="server" Text="Commissionary"></asp:Label>
                                                                            <asp:DropDownList ID="ddlCommissionary" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            OnSelectedIndexChanged="ddlCommissionary_SelectedIndexChanged" AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                            
                                                                    </div>  
                                                                 
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                                                                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                          
                                                                    </div>
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label3" runat="server" Text="Sub - Division"></asp:Label>
                                                                            <asp:DropDownList ID="ddlSubDivision" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            OnSelectedIndexChanged="ddlSubDivision_SelectedIndexChanged" AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                           
                                                                    </div>

                                                                  <div class="col-md-2">
                                                                            <asp:Label ID="Label4" runat="server" Text="Circle"></asp:Label>
                                                                             <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             OnSelectedIndexChanged="ddlBlock_OnSelectedIndexChanged" AutoPostBack="True">
                                                                             </asp:DropDownList>                                                                          
                                                                    </div>
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label5" runat="server" Text="Police Station"></asp:Label>
                                                                            <asp:DropDownList ID="ddlPoliceStation" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                         
                                                                    </div> 
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label6" runat="server" Text="Panchayat"></asp:Label>
                                                                            <asp:DropDownList ID="ddlPanchayat" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            OnSelectedIndexChanged="ddlPanchayat_OnSelectedIndexChanged" AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                          
                                                                    </div>
                                                             </div>
                                                           <div class="row mb-2">
                                                                    
                                                                     
                                                                 
                                                                 
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label7" runat="server" Text="Village"></asp:Label>
                                                                           <asp:DropDownList ID="ddlVillage" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                           
                                                                    </div>

                                                                  <div class="col-md-2">
                                                                            <asp:Label ID="Label8" runat="server" Text="Ward"></asp:Label>
                                                                             <asp:DropDownList ID="ddlWard" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                          
                                                                    </div>


                                                               <div class="col-md-2">
                                                                            <asp:Label ID="Label9" runat="server" Text="Sensivity"></asp:Label>
                                                                           <asp:DropDownList ID="ddlSensivity" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            AutoPostBack="True">
                                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1" id="s" >सामान्य *</asp:ListItem>
                                                                                <asp:ListItem Value="2" id="sv">संवेदनशील **</asp:ListItem>
                                                                                <asp:ListItem Value="3" id="sv2">अतिसंवेदनशील ***</asp:ListItem>
                                                                            
                                                                            </asp:DropDownList>                                                                           
                                                                    </div>

                                                                  <div class="col-md-2">
                                                                            <asp:Label ID="Label10" runat="server" Text="Action"></asp:Label>
                                                                             <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             AutoPostBack="True">
                                                                                 <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                                <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                                                                                <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                                                                                <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                                                                                <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                                                                                <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                                                                                 <asp:ListItem Value="6">न्यायालय में लंबित</asp:ListItem>
                                                                            </asp:DropDownList>                                                                          
                                                                    </div>
                                                               <div class="col-md-2">
                                                                            <asp:Label ID="Label11" runat="server" Text="Disputes Type"></asp:Label>
                                                                             <asp:DropDownList ID="ddlbhumivivadtype" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             >
                                                                            </asp:DropDownList>                                                                          
                                                                    </div>

                                                               <div class="col-md-2">
                                                                    Paze Size
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="true" OnSelectedIndexChanged="PageSize_Changed" CssClass="form-control mb-2">
                                                         <asp:ListItem Text="100" Value="100" />
                                                         <asp:ListItem Text="250" Value="250" />
                                                         <asp:ListItem Text="500" Value="500" />
                                                     </asp:DropDownList>
                            </div>
                                                               <div class="col-md-2">
                                                                        <br />
                                                                            <asp:Button ID="btnSearch" runat="server" Text="Search"  CssClass="btn btn-primary" onclick="btnSearch_Click" />                                                                      
                                                                    </div>  
                                                             </div>
                                                                

                                                                <div class="row mb-2">
                                                                    <asp:Panel ID="Pnldata" runat="server" Style="overflow-x:auto; overflow-y:hidden;"  Height="110%">
                    

                    
           <asp:GridView runat="server" Width="100%" ID="GridView1" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid fontsize" OnRowDataBound="GridView1_RowDataBound"
               DataKeyNames="a_id" EnableTheming="false"  ShowFooter="false" EmptyDataText="No Record Found" Visible="true" >
                 <Columns>
                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1+"." %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
    
     
                            <asp:TemplateField HeaderText="Application No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" >
                                <ItemTemplate>
                                    <%#Eval("ApplicationNo")%>
                                    <%--<asp:LinkButton ID="lnkApplicationNo" OnClientClick="openwindow(this);" runat="server" ForeColor="Blue"
                                            Text='Verify' CommandArgument='<%#Eval("a_id")%>' Font-Underline="false" OnClick="lnkView_Click"></asp:LinkButton>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                          
                          
                            <asp:TemplateField HeaderText="कमिश्नरी <hr style='margin-bottom: 0px; margin-top: 0px;' /> जिला <hr style='margin-bottom: 0px; margin-top: 0px;' /> सब डिवीज़न" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="7%" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <%#Eval("DIVISIONAME")%>
                                     <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                      <%#Eval("DISTRICTNAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Sd_Name_En")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="अंचल <hr style='margin-bottom: 0px; margin-top: 0px;' />थाना "
                                ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="7%">
                                <ItemTemplate>
                                    <%#Eval("BlockName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Police_Station")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="ग्राम पंचायत <hr style='margin-bottom: 0px; margin-top: 0px;' />राजस्व ग्राम<hr style='margin-bottom: 0px; margin-top: 0px;' />वार्ड"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%#Eval("PanchayatName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("VILLNAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("WARDNAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कुल वादी " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("TotalVadi")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कुल प्रतिवादी " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("TotalPratiVadi")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            

                            <asp:TemplateField HeaderText="भूमि का प्रकार" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("Bhumitype")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("SarkariBhumiType")%>                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />                                
                            </asp:TemplateField>           
      <asp:TemplateField HeaderText="भूमि विवाद का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("BhumiVivad")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

    <asp:TemplateField HeaderText="अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="40%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <div style="width:100%;height:300px; overflow:auto">
                                    <asp:GridView ID="grdAction" runat="server"
                                        AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid fontsize"
                EnableTheming="false"  ShowFooter="false" EmptyDataText="No Record Found" Visible="true" 
                                        >
                                        <Columns>
                                             <%--<asp:TemplateField HeaderText="भूमि विवाद की सवेदनशीलता" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Bhumi_savedansheelta")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>    
                            
                                             
                            
                            <asp:TemplateField HeaderText="बैठक का निष्कर्ष" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Description")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />

                            </asp:TemplateField> --%>    
                                            

                                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="भूमि विवाद की सवेदनशीलता" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <%#Eval("SensitivityType")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="बैठक की तिथि" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <%#Eval("Meeting_date", "{0:dd, MMM yyyy}")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="क्या वादी उपस्थित है ?" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("Is_Vadi_Present")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="क्या प्रतिवादी उपस्थित है ?" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <%#Eval("Is_PratiVadi_Present")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="बैठक का निष्कर्ष" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <%#Eval("Action")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>                                   

                                    <asp:TemplateField HeaderText="अंचलाधिकारी का मंतव्य" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("anchala_dhikari_mantavy")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="थानाध्यक्ष का मंतव्य" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("thana_prabhari_mantavy")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="15%">
                                        <ItemTemplate>                                            
                                            <asp:ImageButton ID="Image1" Visible='<%# CheckNull(Eval("Joint_report_SHO_Circle_Officer_file"))%>'  path='<%#Eval("Joint_report_SHO_Circle_Officer_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                 
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="बैठक में लिया गया निर्णय" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("conclusion_of_the_meeting")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>  





                                        </Columns>
                                    </asp:GridView>
                                        </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                           

                            
                                    
                           
                           

                            
                        </Columns>                                                                   
                                                                                </asp:GridView>
                </asp:Panel>
                                                                    </div>

                                                                <div class="row mb-2">
                     <div class="form-group text-center" style="padding-top: 8px; padding-bottom: 8px; border: none; ">
                                                     <div class="col-md-12">
                                                            <asp:Repeater ID="rptPager" runat="server" >
                                                                 <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' Enabled = '<%# Eval("Enabled") %>' OnClick = "Page_Changed" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"></asp:LinkButton>
                                                                 </ItemTemplate>
                                                             </asp:Repeater>
                                                     </div>
                                            </div>

                </div>
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