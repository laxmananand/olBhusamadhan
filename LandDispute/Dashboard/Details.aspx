<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" 
    Inherits="LandDispute_Entry_SearchAppForMetting" %>
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
.button_set {
    align-items: center;
    appearance: none;
    background-image: radial-gradient(100% 100% at 100% 0, #5adaff 0, #5468ff 100%);
    border: 0;
    border-radius: 6px;
    box-shadow: rgb(45 35 66 / 40%) 0 2px 4px, rgb(45 35 66 / 30%) 0 7px 13px -3px, rgb(58 65 111 / 50%) 0 -3px 0 inset;
    box-sizing: border-box;
    color: #fff;
    cursor: pointer;
    display: inline-flex;
    font-family: "JetBrains Mono",monospace;
    height: 35px;
    justify-content: center;
    line-height: 1;
    list-style: none;
    overflow: hidden;
    padding-left: 16px;
    padding-right: 16px;
    position: relative;
    text-align: left;
    text-decoration: none;
    transition: box-shadow .15s,transform .15s;
    user-select: none;
    -webkit-user-select: none;
    touch-action: manipulation;
    white-space: nowrap;
    will-change: box-shadow,transform;
    font-size: 18px;}
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
            url: "Details.aspx/Getpdf",
            data: "{'url':'" + urlpdf + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                //inlineFrameExample.src = response.d;
                window.open("../IDoc.aspx?url=" + response.d, "_blank");
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
          <h4 class="text-black text-center"><b><asp:Label ID="lblDetailtype" runat="server"></asp:Label></b></h4>

        
                <div class="row">
                    <center>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </center>
                </div> 
             
               <div class="card">
                    <div class="card-body">
                           <div class="row">
                                    <div class="col-md-2">
                                        <asp:Label ID="Label9" runat="server" Text="Page Size"></asp:Label>
                                        <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control mb-2" 
                                            Enabled="true">                                                                            
                                        <asp:ListItem Value="50">50</asp:ListItem>
                                        <asp:ListItem Value="70">70</asp:ListItem>
                                        <asp:ListItem Value="130">130</asp:ListItem>                          
                                   </asp:DropDownList>  
                            </div>
                                    <div class="col-md-2">
                                    <br/>
                                        <asp:Button ID="BtnSearch" runat="server" Text="Search" class="button_set" onclick="BtnSearch_Click"/>                        
                                    </div>
                            </div>
                          <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                             
                                                        
                                                                    
                                                                 

                                                                  

                                                                <div class="row mb-2">
                                                                    <asp:Panel ID="Pnldata" runat="server" Style="overflow-x:auto; overflow-y:hidden;"  Height="110%">
                                       
           <asp:GridView runat="server" Width="100%" ID="GridView1" AutoGenerateColumns="false" CssClass="table-responsive CSSTableGeneratorGrid fontsize" OnRowDataBound="GridView1_RowDataBound"
               DataKeyNames="a_id" EnableTheming="false"  ShowFooter="true" 
                EmptyDataText="No Record Found" Visible="true"
               >
                 <Columns>
                            <asp:TemplateField HeaderText="Sl. No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1+"." %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
    
     
                            
                           <%-- <asp:TemplateField HeaderText="Application No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="6%" >
                                <ItemTemplate>
                                    <%#Eval("ApplicationNo")%>
                                    <asp:LinkButton ID="lnkApplicationNo" OnClientClick="openwindow(this);" runat="server" ForeColor="Blue"
                                            Text='Add Metting' CommandArgument='<%#Eval("a_id")%>' Font-Underline="false" OnClick="lnkView_Click"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>--%>
                          
                            <asp:TemplateField HeaderText="कमिश्नरी <hr style='margin-bottom: 0px; margin-top: 0px;' /> जिला <hr style='margin-bottom: 0px; margin-top: 0px;' /> सब डिवीज़न" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" ItemStyle-VerticalAlign="Top">
                                <ItemTemplate>
                                    <%#Eval("DIVISIONAME")%>
                                     <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                      <%#Eval("DISTRICTNAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Sd_Name_En")%> 
                                     <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />                               
                                      <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("a_id")%>' 
                                       Font-Underline="false"  ForeColor="Blue" OnClick="lnkView_Click" Text="View"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="अंचल <hr style='margin-bottom: 0px; margin-top: 0px;' />थाना "
                                ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <%#Eval("BlockName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("Police_Station")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                           <asp:TemplateField HeaderText="ग्राम पंचायत <hr style='margin-bottom: 0px; margin-top: 0px;' />राजस्व ग्राम<hr style='margin-bottom: 0px; margin-top: 0px;' />वार्ड"
                                ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <%#Eval("PanchayatName")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("VILLNAME")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("WARDNAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कुल <br>वादी " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("TotalVadi")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="कुल<br> प्रतिवादी " ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("TotalPratiVadi")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            

                            <asp:TemplateField HeaderText="भूमि का <br>प्रकार" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("Bhumitype")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("SarkariBhumiType")%>                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />                                
                            </asp:TemplateField>           
      
  
                           

                            <asp:TemplateField HeaderText="भूमि विवाद<br> का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("BhumiVivad")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="भूमि विवाद की </br> सवेदनशीलता" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Bhumi_savedansheelta")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>    
                            
                            <asp:TemplateField HeaderText="बैठक<br> की तिथि" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Meeting_date")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                     
                            
                            <asp:TemplateField HeaderText="बैठक <br>का निष्कर्ष" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Description")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                             
                            
                            <asp:TemplateField HeaderText="(Action)" ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <div id="div_Action" runat="server" class="divclss" >
                                    <%#Eval("disposal")%>
                                        </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  

                           
                            
                            
                            <asp:TemplateField HeaderText="विवाद का<br> अद्यतन कारक" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("bhumi_vivad_ka_adyatan_sthiti")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                           

                            </asp:TemplateField>
                                      <asp:TemplateField HeaderText="वादी द्वारा<br> प्रस्तुत साक्ष्य" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                    <%#Eval("TotalVadiEvidenceDocument")%>
                                    

                                </ItemTemplate>
                                <ItemStyle  HorizontalAlign="Left"  />
                            </asp:TemplateField>
                                                                                
                            <asp:TemplateField HeaderText="वादी का </br>दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:ImageButton ID="Image6" Visible='<%# CheckImage(Eval("Vadi_sakshya_File"))%>' path='<%#Eval("Vadi_sakshya_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="प्रतिवादी द्वारा प्रस्तुत साक्ष्य" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" 
                               ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%"  HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <%#Eval("TotalPrativadiEvidenceDocument")%>
                                   
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="प्रतिवादी का </br>दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Wrap="false"
                                ItemStyle-Width="10%"  HeaderStyle-Width="10%">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image1" Visible='<%# CheckImage(Eval("Prativadi_sakshya_File"))%>' path='<%# Eval("Prativadi_sakshya_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                 
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="पुलिस पदाधिकारी द्वारा समर्पित </br>जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                <div id="div_pulis_padadhikari_vivarani" runat="server" class="divclss" visible='<%# CheckNull(Eval("pulis_padadhikari_vivarani"))%>'>
                                    <%#Eval("pulis_padadhikari_vivarani")%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="दस्तावेज"  ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="5%" HeaderStyle-Wrap="false">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image2" Visible='<%# CheckImage(Eval("pulis_padadhikar_Patr_file"))%>' path='<%# Eval("pulis_padadhikar_Patr_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                
                                    
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="हल्का कर्मचारी / <br>अंचल निरीक्षक द्वारा समर्पित </br>जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="20%">
                                <ItemTemplate>
                                <div id="div_HalkaKarmchari_vivran" runat="server" class="divclss"  visible='<%# CheckNull(Eval("HalkaKarmchari_vivran"))%>'>
                                 
                                    <%#Eval("HalkaKarmchari_vivran")%>
                                  </div>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image3" Visible='<%# CheckImage(Eval("HalkaKarmchari_Patr_file"))%>' path='<%#Eval("HalkaKarmchari_Patr_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                               
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="विवादित भू-खंड <br>मापी का विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                              
                                    <%#Eval("vivadit_bhukhand_Mapi_ki_avashyakta_hai")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <%#Eval("vivadit_bhukhand_Mapi")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                    <asp:Label runat="server" Text="माप के लिए निर्धारित तिथि : " Visible='<%# CheckNull(Eval("maapee_ke_lie_nirdhaarit_tithi"))%>' ></asp:Label><%#Eval("maapee_ke_lie_nirdhaarit_tithi","{0:dd/MM/yyyy}")%>
                                    <hr style='margin-bottom: 0px; margin-top: 0px;' /> 
                                    <asp:Label Text="मापी नहीं होने का कारण :" runat="server" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'></asp:Label>                                    
                                    <div id="div_vivaadit_bhukhand_Mapi_Reason" runat="server"  class="divclss" visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'>                                   
                                    <%#Eval("vivaadit_bhukhand_Mapi_Reason")%>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="मापी का<br> दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                <asp:ImageButton ID="Image4" Visible='<%# CheckImage(Eval("vivaadit_bhukhand_Mapi_File"))%>' path='<%#Eval("vivaadit_bhukhand_Mapi_File")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                                                        
                            <asp:TemplateField HeaderText="विवाद का </br>प्राथमिकी/अप्राथमिकी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("bhumi_vivad_Vivran_Available")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="न्यायालय में </br>प्रक्रियाधीन वाद" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("dispute_in_court_available")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="आवेदन" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                ItemStyle-Width="5%">
                                <ItemTemplate>
                                 <asp:ImageButton ID="Image5" Visible='<%# CheckImage(Eval("ApplicationFile"))%>' path='<%#Eval("ApplicationFile")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="" Visible="false">
                                     <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" Text='View' CssClass="btn btn-success"
                                                        CommandArgument='<%#Eval("a_id")%>' ForeColor="Blue" Font-Underline="false"
                                                        ToolTip="Click Edit" OnClick="lnkView_Click"></asp:LinkButton>
                                    
                                                            </ItemTemplate>
                                                            <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                                                        </asp:TemplateField>--%>


                            
                        </Columns>                                                                   
                                                                                </asp:GridView>
                </asp:Panel>
                                                                    </div>

                                                                 <div class="row mb-2">
                     <div class="form-group text-center" style="padding-top: 8px; padding-bottom: 8px; border: none; ">
                                                     <div class="col-md-12">
                                                            <asp:Repeater ID="rptPager" runat="server" >
                                                                 <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' 
                                                                            Enabled = '<%#Eval("Enabled")%>' 
                                                                             OnClick = "Page_Changed" style="padding:5px; border-radius:5px;/*background-color:#1eb089; */color:black;text-decoration:none"></asp:LinkButton>
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