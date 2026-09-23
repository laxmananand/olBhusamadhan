<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPagePublic.master" AutoEventWireup="true" CodeFile="PublicStatus_AllDetails.aspx.cs" Inherits="LandDispute_Entry_PublicStatus_AllDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
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
 <style type="text/css">
    .zindex {
        background-color: #FAF5EF;
        z-index:10001;
    }
</style>
<script src="JS/Jquerry2000min.js"></script>
<script type="text/javascript">
    function fnLinkbutton1(objlinkbutton) {
        var urlpdf = document.getElementById(objlinkbutton).getAttribute("path");
        getpdfdocument(urlpdf);
        return false;
    }
    function getpdfdocument(urlpdf) {
        var imgDiv = document.getElementById("divImage");
        var inlineFrameExample = document.getElementById("inlineFrameExample");
        urlpdf = urlpdf.replace("~", "");
         //urlpdf = ("http://localhost:8080" + urlpdf).replace(' ', '');
        urlpdf = ("http://10.133.20.157:85" + urlpdf);
        urlpdf = urlpdf.trim();
        $.ajax({
            type: "POST",
            url: "AddMettingApplicationPublic.aspx/Getpdf",
            data: "{'url':'" + urlpdf + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
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
    $(document).ready(function () {
        $(".getpdfdoc").click(function (event) {
            event.preventDefault();
            var urlpdf = $(this).attr("path");

            getpdfdocument(urlpdf);
            return false;
        });
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

 <script type="text/javascript">

     function LoadDiv(url) {

         var img = new Image();
         var bcgDiv = document.getElementById("divBackground");
         var imgDiv = document.getElementById("divImage");
         var imgFull = document.getElementById("imgFull");
         var imgLoader = document.getElementById("imgLoader");
         var inlineFrameExample = document.getElementById("inlineFrameExample");
         var lnkProgress = document.getElementById(url.id);
         var bookingID = lnkProgress.getAttribute("Path");
         //  alert(bookingID);
         var a = bookingID.substring(1);
         // alert(bookingID);
         // inlineFrameExample.src = '<%=ResolveUrl("' + a + '")%>';
         inlineFrameExample.src = "/LDHOME" + a;
         //inlineFrameExample.src = bookingID;

         imgLoader.style.display = "block";
         img.onload = function () {
             imgFull.src = img.src;

             imgFull.style.display = "block";
             imgLoader.style.display = "none";
         };
         img.src = url;
         var width = document.body.clientWidth;
         if (document.body.clientHeight > document.body.scrollHeight) {
             bcgDiv.style.height = document.body.clientHeight + "px";
         }
         else {
             bcgDiv.style.height = document.body.scrollHeight + "px";
         }
         imgDiv.style.left = (width - 1200) / 2 + "px";
         imgDiv.style.top = "10px";
         bcgDiv.style.width = "100%";

         bcgDiv.style.display = "block";
         imgDiv.style.display = "block";
         return false;
     }
     


     function SelectSinglebutton(rdBtnID) {
         document.getElementById(rdBtnID).setAttribute("Text", "redButton");
     }
</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="container-fluid">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div class="card-header bg-primary text-white">
             </div>


        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                भूमि विवाद का प्रकार
            </div>
            <div class="card-body">

                <div class="row">
                    <div class="col-md-3" style="font-weight: bold;font-size:18px">
                       
                        <asp:Label ID="lblApplicationNo" runat="server" Text="आवेदन संख्या "></asp:Label>
                    </div>
                    <div class="col-md-6"></div>
                    <div class="col-md-3" style="font-weight: bold;font-size:18px">
                       
                        <asp:Label ID="lblAppDate" runat="server" Text="आवेदन की तिथि "></asp:Label>
                    </div>
                    </div>
                 <div class="row">
                                                                  <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                      जिला
                                                                  </div>
                                                                  <div class="col-md-3" style="font-size:12px">
                                                                        <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                                                                  </div>
                                                                  <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                      अनुमंडल
                                                                  </div>
                                                                  <div class="col-md-3"  style="font-size:12px">
                                                                       <asp:Label ID="lblSubdivision" runat="server" Text=""></asp:Label>
                                                                  </div>
                                                              </div> 
                                                              <div class="row">
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            अंचल
                                                                      </div>
                                                                      <div class="col-md-3" style="font-size:12px">
                                                                          <asp:Label ID="lblBlock" runat="server"></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            थाना
                                                                      </div>
                                                                      <div class="col-md-3"  style="font-size:12px">
                                                                          <asp:Label ID="lblPolice_Station" runat="server"></asp:Label>
                                                                      </div>
                                                                 </div>  
                                                              <div class="row">
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            क्षेत्र का प्रकार
                                                                      </div>
                                                                      <div class="col-md-3" style="font-size:12px">
                                                                           <asp:Label ID="lblAreaType" runat="server"></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                           <asp:Label ID="lblVillage" runat="server" ></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3"  style="font-size:12px">
                                                                           <asp:Label ID="lblPanchayatName" runat="server"></asp:Label>
                                                                      </div>
                                                              </div> 
                                                              <div class="row">
                                                                     <div class="col-md-3" id="div_Vadi_Svarajaya_Label" runat="server"   style="font-weight: bold;font-size:14px">
                                                                          राजस्व ग्राम
                                                                      </div>
                                                                      <div class="col-md-3" id="div_Vadi_Svarajaya" runat="server" style="font-size:12px">
                                                                             <asp:Label ID="lblVILLNAME" runat="server"></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            वार्ड 
                                                                      </div>
                                                                      <div class="col-md-3" id="div_Vadi_Ward" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblWARDNAME" runat="server"></asp:Label>
                                                                      </div>
                                                                       <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                           विवाद का अद्यतन कारक
                                                                      </div>
                                                                      <div class="col-md-3" id="div_vadi_Vivad_Ka_Vighatan" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblvadi_Vivad_Ka_Vighatan" runat="server"></asp:Label>
                                                                      </div>
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            राजस्व थाना संख्या
                                                                      </div>
                                                                      <div class="col-md-3" id="div_vadi_rajashv_sankhaya" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblvadi_rajashv_sankhaya" runat="server"></asp:Label>
                                                                      </div>
                                                                     <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                           भूमि का प्रकार
                                                                      </div>
                                                                      <div class="col-md-3" id="div_Vadi_BhumiKaPrakar" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblVadi_BhumiKaPrakar" runat="server"></asp:Label>
                                                                      </div>
                                                                        <div class="col-md-3" id="div_vadi_sarkari_bhumi_ka_prakar_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                          सरकारी भूमि का प्रकार
                                                                      </div>
                                                                     <div class="col-md-3" id="div_Preview_vadi_sarkari_bhumi_ka_prakar" runat="server" style="font-size:12px">
                                                                          <asp:Label ID="lblvadi_sarkari_bhumi_ka_prakar_Label" runat="server"></asp:Label>
                                                                     </div>  
                                                                     <div class="col-md-3" id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                          सरकारी भूमि का प्रकार (अगर अन्य है)
                                                                      </div>
                                                                     <div class="col-md-3" id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server" style="font-size:12px">
                                                                          <asp:Label ID="lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server"></asp:Label>
                                                                     </div> 
                                                                      <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            भूमि विवाद का प्रकार
                                                                      </div>
                                                                      <div class="col-md-3" id="div_BhumiKa_VivadPrakar" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblBhumiKa_VivadPrakar" runat="server"></asp:Label>
                                                                       </div>
                                                                     <div class="col-md-3" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                             <asp:Label ID="Label1" runat="server" Text="भूमि विवाद का प्रकार (अगर अन्य है) :"></asp:Label>
                                                                      </div>
                                                                    <div class="col-md-3" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya" runat="server" style="font-size:12px">
                                                                         <asp:Label ID="lblvadi_Bhumivivad_Prakar_Anaya" runat="server"></asp:Label>
                                                                    </div>                                                                     
                                                                </div>
                                                              <div class="row">
                                                                        <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी 
                                                                      </div>
                                                                      <div class="col-md-9" id="divVadiKabhumiVivaran" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblVadiKabhumiVivaran" runat="server"></asp:Label>
                                                                      </div>                                                                   
                                                                </div>
                                                             
                                                                <div class="row">
                                                                        <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            वादी द्वारा प्रस्तुत आवेदन 
                                                                      </div>
                                                                      <div class="col-md-3">
                                                                            <asp:ImageButton ID="lnkAppDoc" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
                                            Height="50px" Style="cursor: pointer" />
                                                                      </div>  
                                                                    
                                                                </div>
            </div>
        </div>
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                वादी का विवरण
            </div>
            <div class="row">
                <div class="col-md-12">
                                                                                                 <asp:GridView runat="server" Width="100%" ID="gvWadi"
                                                                                                      AutoGenerateColumns="false" 
                                                                                  CssClass="table-responsive CSSTableGeneratorGrid" 
                                                                                                     EmptyDataText="No Record Found!"> 
                                                                                    
                                                                                    <Columns>
                                                                                           
                                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                                                                <ItemTemplate>
                                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>                                        
                                                                                        <asp:BoundField DataField="NameAsPerAadhaar" HeaderText="वादी का नाम" ItemStyle-Width="100" ControlStyle-Font-Size="Smaller" />
                                                                                        <asp:BoundField DataField="Vadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" ItemStyle-Width="120" />
                                                                                        <asp:TemplateField HeaderText="लिंग" ItemStyle-Width="80">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblGender"  runat="server" Text='<%# (Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ) ? "Female" : "Male"  %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                                                        
                                                                                        <asp:BoundField DataField="YearOfBirthAsPerAadhaar" HeaderText="उम्र का वर्ष" ItemStyle-Width="80" />                                                                                                                                    
                                                                                        <asp:BoundField DataField="dist" HeaderText="जिला" ItemStyle-Width="80" />
                                                                                        <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" ItemStyle-Width="100" />
                                                                                        <asp:BoundField DataField="block" HeaderText="अंचल" ItemStyle-Width="100" />
                                                                                        <asp:BoundField DataField="thana" HeaderText="थाना" ItemStyle-Width="100" />
                                                                                        <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" ItemStyle-Width="100" />
                                                                                        <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" ItemStyle-Width="100" />
                                                                                         <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" ItemStyle-Width="100" />
                                                                                        <asp:BoundField DataField="WardNo" HeaderText="वार्ड" ItemStyle-Width="80" />
                                                                                        <asp:BoundField DataField="Vadi_MobileNo" HeaderText="मोबाइल संख्या" ItemStyle-Width="80" />    
                                                                                         <asp:TemplateField HeaderText="विभाग का प्रतिनिधि" ItemStyle-Width="80">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblDeppratinidhi"  runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_dept")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                   
                                                                                          <asp:TemplateField HeaderText="संस्था का प्रतिनिधि" ItemStyle-Width="80">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblOrgpratinidhi"  runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="विभाग/संस्था का नाम" ItemStyle-Width="100">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblOrgName"  runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? Eval("vadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम" ItemStyle-Width="100">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblPadName" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y") ? Eval("vadi_org_pad_name") : Eval("vadi_dept_pad_name")   %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>                                                                                                                                                                                                  
                                                                                    </Columns>
                                                                                </asp:GridView>   
                                                                                        </div>

            </div>
        </div>
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                प्रतिवादी का विवरण
            </div>
            <div class="row">
                                                                                       <div class="col-md-12" id="divPratiwadi" runat="server">
                                                                                                 
                                                                                   <asp:GridView runat="server" Width="100%" ID="pratiWadi_grid"
                                                                                                      AutoGenerateColumns="false" 
                                                                                  CssClass="table-responsive CSSTableGeneratorGrid" 
                                                                                                     EmptyDataText="No Record Found!"> 
                                                                                                      
                                                                                                      
                                                                                                      <Columns>                                                                                       
                                                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                              <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी" ItemStyle-Width="5%" />
                                                                                            <asp:BoundField DataField="pratiVadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" ItemStyle-Width="5%" />                                                                                                                                                                        
                                                                                            <asp:BoundField DataField="dist" HeaderText="जिला" ItemStyle-Width="5%" />
                                                                                            <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" ItemStyle-Width="5%" />
                                                                                            <asp:BoundField DataField="block" HeaderText="अंचल" ItemStyle-Width="5%" />
                                                                                            <asp:BoundField DataField="thana" HeaderText="थाना" ItemStyle-Width="5%" />
                                                                                            <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" ItemStyle-Width="5%" />
                                                                                            <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" ItemStyle-Width="5%" />
                                                                                             <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" ItemStyle-Width="5%" />
                                                                                            <asp:BoundField DataField="WardNo" HeaderText="वार्ड" ItemStyle-Width="5%" />
                                                                                            <asp:BoundField DataField="pratiVadi_MobileNo" HeaderText="मोबाइल संख्या" ItemStyle-Width="5%" />
                                                                                                <asp:TemplateField HeaderText="संस्था का प्रतिनिधि" ItemStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblis_pratiVadi_from_an_org" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y") ? "हां" : "नहीं"   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <asp:TemplateField HeaderText="विभाग का प्रतिनिधि" ItemStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblis_pratiVadi_from_an_dept" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_dept")) == "Y") ? "हां": "नहीं"   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                                                                                                 
                                                                                             <asp:TemplateField HeaderText="विभाग/संस्था का नाम" ItemStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblOrgName"  runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ) ? Eval("pratiVadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम" ItemStyle-Width="10%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPadName" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y") ? Eval("pratiVadi_org_pad_name") : Eval("pratiVadi_dept_pad_name")   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                                                                                             
                                                                                 </Columns>
                                                                        </asp:GridView>
                                                                                       </div>
                                                                                 </div>
        </div>
      
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                विवादित भूमि का विवरण
            </div>
            <div class="card-body">
               <div class="row">
                                                                                   <div class="col-md-12" id="divbhumikhata_shekher_ka_vivaran" runat="server" >
                                                                                             
                                                                      <asp:GridView runat="server"  ID="grd_bhumivivad"
                                                                                                      AutoGenerateColumns="false" 
                                                                                  CssClass="table-responsive CSSTableGeneratorGrid" 
                                                                                                     EmptyDataText="No Record Found!"> 
                                                                                    <Columns>                                                                         
                                                                                         <asp:TemplateField HeaderText="Sl. No.">
                                                                                                                <ItemTemplate>
                                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                         </asp:TemplateField>
                                                                                        <asp:BoundField DataField="khataNo" HeaderText="खाता संख्या" ItemStyle-Width="10%" />
                                                                                        <asp:BoundField DataField="khesraNo" HeaderText="खेसरा संख्या" ItemStyle-Width="10%" />
                                                                                        <asp:BoundField DataField="Rakba" HeaderText="रकबा" ItemStyle-Width="10%" />
                                                                                        <asp:BoundField DataField="LandTypesInKhatianDesc" HeaderText="जमीन की किस्म" ItemStyle-Width="10%" />                                        
                                                                                         <asp:TemplateField HeaderText="ख़तियन में जमीन का विवरण" ItemStyle-Width="15%">
                                                                                            <ItemTemplate>
                                                                                                <span id='<%# Eval("LandDetailsInKhatian") %>' style="display: block;overflow: auto; height:25px;" >
                                                                                                    <%# Eval("LandDetailsInKhatian") %>
                                                                                                </span>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                         <asp:BoundField DataField="North_chauhaddee" HeaderText="उत्तर" ItemStyle-Width="10%" />
                                                                                         <asp:BoundField DataField="South_chauhaddee" HeaderText="दक्षिण" ItemStyle-Width="10%" />
                                                                                         <asp:BoundField DataField="East_chauhaddee" HeaderText="पूर्व" ItemStyle-Width="10%" />
                                                                                         <asp:BoundField DataField="West_chauhaddee" HeaderText="पश्चिम" ItemStyle-Width="10%" />                                                                                                                
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                   </div>
                                                                             </div>
            </div>
        </div>
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                वादी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>
             <div class="card-body">
                 <div class="row">
                                                                                   <div class="col-md-12">
                                                                                               <asp:GridView runat="server" Width="100%" ID="gdVadiEvidence"  AutoGenerateColumns="false"
                                                                                                    CssClass="table-responsive CSSTableGeneratorGrid" EmptyDataText="No Record Found!" OnRowDataBound="gdVadiEvidence_RowDataBound">
                                                                                                   
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                                                                                <ItemTemplate>
                                                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                                                </ItemTemplate>
                                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                            </asp:TemplateField>                                                                                                                   
                                                                                                        <asp:TemplateField HeaderText="साक्ष्य का प्रकार" ItemStyle-Width="150">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblEvidenceType" Text='<%# (Convert.ToString(Eval("evidence_id")) != "9") ? Eval("evidence_name") : Eval("evidence_any_name")   %>' runat="server"></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="साक्ष्य का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                                                                                ItemStyle-Width="5%">
                                                                                                <ItemTemplate>
                                                                                                <asp:ImageButton ID="Image1"  path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" CommandArgument='<%# Container.DataItemIndex %>' CommandName="View"/>
                                                                                            
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>                                                                                                                      
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                  </div>  
                                                                             </div> 
                 </div>

        </div>
        
      
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                      
                            <asp:GridView ID="GVAnchalaDhakari" runat="server"
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%" OnRowDataBound="GVAnchalaDhakari_RowDataBound"
                                
                                CssClass="table-responsive CSSTableGeneratorGrid" AllowPaging="false" PageSize="25" Style="width: 100%;"
                               
                                ShowHeaderWhenEmpty="True"  EmptyDataRowStyle-HorizontalAlign="Left" EmptyDataRowStyle-VerticalAlign="Middle"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                              
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
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
                                    <asp:TemplateField HeaderText="थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="15%">
                                        <ItemTemplate>                                            
                                            <asp:ImageButton ID="Image1" Visible='<%# CheckImage(Eval("Joint_report_SHO_Circle_Officer_file"))%>'   path='<%#Eval("Joint_report_SHO_Circle_Officer_file")%>' class="getpdfdoc" runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                 
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="बैठक में लिया गया निर्णय" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("conclusion_of_the_meeting")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="साक्ष्य का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false" ItemStyle-Width="5%">
    <ItemTemplate>
        <asp:ImageButton ID="Image3" 
            runat="server" 
            ImageUrl="images/pdf.gif" 
            Width="50px" Height="50px" 
            Style="cursor: pointer"
            CommandArgument='<%# Container.DataItemIndex %>' 
            CommandName="View"
            Visible='<%# !string.IsNullOrEmpty(Eval("CircleOfficer_letterOfIntent").ToString()) %>'
            path='<%#Eval("CircleOfficer_letterOfIntent")%>' />
    </ItemTemplate>
    <ItemStyle HorizontalAlign="Left" />
</asp:TemplateField>
                                             
                                </Columns>
                            </asp:GridView>
                       
                    </div>
                </div>
                 </div>
        </div>




        

   

    <br />
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
            </div>
</asp:Content>

