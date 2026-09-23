<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddMettingApplication.aspx.cs" Inherits="Police_Station_add" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            // urlpdf = ("http://localhost:8080" + urlpdf).replace(' ', '');
            urlpdf = ("http://localhost:8080" + urlpdf);
            urlpdf = urlpdf.trim();
            $.ajax({
                type: "POST",
                url: "AddMettingApplication.aspx/Getpdf",
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

    <script type ="text/javascript">
        function SaveAnotherMetting()
        {
            debugger;
          
            var txtabhiyukt_anchaladhikari = document.getElementById('<%=txtabhiyukt_anchaladhikari.ClientID%>');
            var txtabhiyukt_thaanprabhaaree = document.getElementById('<%=txtabhiyukt_thaanprabhaaree.ClientID%>');
           
            var ddlaction = document.getElementById('<%=ddlaction.ClientID%>');
            if (ddlaction.selectedIndex == 5) {  
              
                if (txtabhiyukt_anchaladhikari.value.trim() == "") {
                    alert("अंचलाधिकारी का मंतव्य अंकित करें...!");
                    txtabhiyukt_anchaladhikari.focus();
                    return false;
                }
               
                   
                
            }
           
            if (ddlaction.selectedIndex == 5) {

               

                if (txtabhiyukt_thaanprabhaaree.value.trim()=="") {
                    alert("थानाध्यक्ष का मंतव्य अंकित करें...!");
                    txtabhiyukt_thaanprabhaaree.focus();
                    return false;
                }

            }

            
            //alert("Selected Index: " + ddlaction.selectedIndex);
           


            var ddlbhumivivadki_sanvedanshilta = document.getElementById('<%=ddlbhumivivadki_sanvedanshilta.ClientID%>')
            if (ddlbhumivivadki_sanvedanshilta.selectedIndex == 0) {
                alert("कृपया भूमि की संवेदनशीलता चुनें...!");
                ddlbhumivivadki_sanvedanshilta.focus();
                return false;
            }

            var txtbaithakDate = document.getElementById('<%=txtbaithakDate.ClientID%>');
            if (txtbaithakDate.value.trim() == "") {
                alert("कृपया बैठक की तिथि अंकित करें...!");
                txtbaithakDate.focus();
                return false;
            }

            var ddlIsVadiAvailable = document.getElementById('<%=ddlIsVadiAvailable.ClientID%>');
            if (ddlIsVadiAvailable.selectedIndex == 0) {
                alert("क्या वादी उपस्थित है ? हां/नहीं चुनें...!");
                ddlIsVadiAvailable.focus();
                return false;
            }

            var ddl_IsprativadiAvailable = document.getElementById('<%=ddl_IsprativadiAvailable.ClientID%>');
            if (ddl_IsprativadiAvailable.selectedIndex == 0) {
                alert("क्या प्रतिवादी उपस्थित है ? हां/नहीं चुनें...!");
                ddl_IsprativadiAvailable.focus();
                return false;
            }

          //  var ddlaction = document.getElementById('<%=ddlaction.ClientID%>');            
            if (ddlaction.selectedIndex == 0) {
                alert("कृपया बैठक का निष्कर्ष चुनें...!");
                ddlaction.focus();
                return false;
            }

            var txtAgalaDate = document.getElementById('<%=txtAgalaDate.ClientID%>');
            var txtvadkavars = document.getElementById('<%=txtvadkavars.ClientID%>');
            if (ddlaction.selectedIndex == 1) {
                if (txtAgalaDate.value.trim() == "") {
                    alert("कृपया निस्तारण की तिथि अंकित करें...!");
                    txtAgalaDate.focus();
                    return false;
                }
            }

            var txtCancelReason = document.getElementById('<%=txtCancelReason.ClientID%>')
            if (ddlaction.selectedIndex == 2) {
                if (txtCancelReason.value.trim() == "") {
                    alert("कृपया अस्वीकृति का कारण अंकित करें...!");
                    txtCancelReason.focus();
                    return false;
                }
            }

            var txtfalafal = document.getElementById('<%=txtfalafal.ClientID%>')           
            if (txtfalafal.value.trim() == "") {
                alert("कृपया बैठक में लिया गया निर्णय अंकित करें...!");
                txtfalafal.focus();
                return false;
            }

            if (ddlaction.selectedIndex == 3) {
                if (txtAgalaDate.value.trim() == "") {
                    alert("कृपया मापी की तिथि अंकित करें...!");
                    txtAgalaDate.focus();
                    return false;
                }
            }

            if (ddlaction.selectedIndex == 4) {
                if (txtAgalaDate.value.trim() == "") {
                    alert("कृपया अगली सुनवाई की तिथि अंकित करें...!");
                    txtAgalaDate.focus();
                    return false;
                }
            }

            if (ddlaction.selectedIndex == 5) {
                if (txtvadkavars.value.trim() == "") {
                    alert("वादी की वाद संख्या / वर्ष अंकित करें...!");
                    txtvadkavars.focus();
                    return false;
                }
               
            }

           


            var LandDoc = document.getElementById("<%=LandDoc.ClientID %>").value;
            if (LandDoc != '') {
                var valid_extensions = /(.pdf)$/i;
                if (!valid_extensions.test(LandDoc)) {
                    alert('Please Select only PDF File?');
                    return false;
                }                
            } 
           

            if (!confirm('Are you sure to save data?')) {
                return false;
            }

            return true;
        }


        function checkDate(sender, args) {
           
            var ddl = document.getElementById('<%=ddlaction.ClientID%>');
            if (ddl.selectedIndex == 3) {

            }
            else {
                if (sender._selectedDate > new Date()) {
                alert("You cannot select a day latter than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value("")
            }
               else if (sender._selectedDate.getDay() != 6) {
                    alert("You can only select Saturday!");
                    sender._selectedDate = new Date(); //set back to current date
                    sender._textbox.set_Value("");
                }
              
                
            }
            
        }
        function dateValidate(evt) {           
            alert(ddl);
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 45) {
                return true;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div class="card-header bg-primary text-white">
             </div>


        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                भूमि विवाद का विवरण
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
                                                                           प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी 
                                                                      </div>
                                                                      <div class="col-md-9" id="divPrativadiKabhumiVivaran" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblPrativadiKabhumiVivaran" runat="server"></asp:Label>
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
                                                                    <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                            प्रतिवादी द्वारा प्रस्तुत आवेदन
                                                                      </div>
                                                                      <div class="col-md-3">
                                                                            <asp:ImageButton ID="lnkPrativadiDoc" path="display"  class="getpdfdoc"  runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
                                            Height="50px" Style="cursor: pointer"  />
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
                अन्य विवरण
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="row">                                                                             
                                                                                 <div class="col-md-4" id="divprativadi_ka_suchit_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       प्रतिवादी को सूचित किया गया है या नहीं ?
                                                                                 </div>
                                                                                 <div class="col-md-2" id="divprativadi_ka_suchit" runat="server" style="font-size:12px">
                                                                                        <asp:Label ID="lblprativadi_ka_suchit" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Karan_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       कारण स्पष्ट करें
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Karan" runat="server" style="font-size:12px">
                                                                                        <asp:Label ID="lblprativadi_ka_Karan" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                                <div class="col-md-3" id="divprativadi_ka_madham_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       माध्यम
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_madham" runat="server" style="font-size:12px">
                                                                                        <asp:Label ID="lblprativadi_ka_madham" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                                 <div class="col-md-4" id="divprativadi_ka_Suchna_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?
                                                                                 </div>
                                                                                 <div class="col-md-2" id="divprativadi_ka_Suchna" runat="server" style="font-size:12px">
                                                                                     <asp:Label ID="lblprativadi_ka_Suchna" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Upashtith_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                                       प्रतिवादी उपस्थित हुआ है या नहीं ? 
                                                                                 </div>
                                                                                 <div class="col-md-3" id="divprativadi_ka_Upashtith" runat="server" style="font-size:12px">
                                                                                     <asp:Label ID="lblprativadi_ka_Upashtith" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                                                            </div>
                </div>
            </div>
        </div>
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                भूमि का खाता-खेसरा का विवरण
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
                प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>
            <div class="card-body">
                <div class="row">
                                                                                   <div class="col-md-12">
                                                                                      <asp:GridView runat="server" Width="100%" ID="gdPrativadiEvidence"  AutoGenerateColumns="false"
                                                                                          CssClass="table-responsive CSSTableGeneratorGrid" EmptyDataText="No Record Found!" OnRowDataBound="gdPrativadiEvidence_RowDataBound" >                                                                                 
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
                                                                                                    <asp:ImageButton ID="Image1"  path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />                                 
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
                राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण
            </div>
            <div class="card-body">
                <div class="row">                                                                             
                                                                                             <div class="col-md-4" id="div2" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                   पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी 
                                                                                             </div>
                                                                                             <div class="col-md-8" id="divPoliceAdhikari" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblPoliceAdhikari" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>  
                                                                                        
                                                                                   <div class="row" id="divHalkaKarmchari" runat="server">                                                                             
                                                                                             <div class="col-md-4" id="div4" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                   हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                                                                                             </div>
                                                                                             <div class="col-md-8" id="divHalkaKarmchariValue" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblHalkaKarmchariValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                                                                                    <div class="row" id="div1" runat="server">                                                                             
                                                                                             <div class="col-md-6"  style="font-weight: bold;font-size:14px">
                                                                                                 <div class="row">
                                                                                                     <div class="col-md-9">पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज</div>
                                                                                                     <div class="col-md-3">
                                                                                                         <asp:ImageButton runat="server" ID="lnkpulis_padadhikari_Patr_file" path="display" class="getpdfdoc"  ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                                     </div>
                                                                                                 </div>
                                                                                                   
                                                                                                 
                                                                                                 
                                                                                                  
                                                                                             </div>
                                                                                             <div class="col-md-6" style="font-weight: bold;font-size:14px">
                                                                                                 <div class="row">
                                                                                                     <div class="col-md-9"> हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी  </div>
                                                                                                     <div class="col-md-3">
                                                                                                         <asp:ImageButton runat="server" ID="lnkfile_halkakarmchari_praptr" path="display" class="getpdfdoc"  ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                                     </div>
                                                                                                 </div>
                                                                                                    
                                                                                                 
                                                                                             
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                                                                                   <div class="row" id="divVivaditBhukand" runat="server">                                                                             
                                                                                             <div class="col-md-4" id="div6" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  विवादित भू-खंड की मापी 
                                                                                             </div>
                                                                                             <div class="col-md-8" id="divVivaditBhukandValue" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblVivaditBhukandValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>   
                                                                                    
                                                                                   <div class="row" id="divMapi" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div8" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  मापी ? 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divMapiValue" runat="server" style="font-size:12px">
                                                                                                  <asp:Label ID="lblMapiValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                                                                                    <br/>
                                                                                   <div class="row" id="divVivaditBhukandKaMapi" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div10" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  विवादित भू-खंड की मापी नहीं होने का कारण 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divVivaditBhukandKaMapiValue" runat="server" style="font-size:12px">
                                                                                                 <asp:Label ID="lblVivaditBhukandKaMapiValue" runat="server"></asp:Label>
                                                                                             </div>  
                                                                                       
                                                                                       <div class="col-md-3" id="div7" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  विवादित भू-खंड की मापी का प्रतिवेदन
                                                                                             </div>
                                                                                             <div class="col-md-9" id="div9" runat="server" style="font-size:12px">
                                                                                                 <asp:ImageButton runat="server" ID="lnkfile_bhukand_prativedan" path="display" class="getpdfdoc"  ImageUrl="/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer"  />
                                                                                             </div> 
                                                                                   </div>    
                                                                                     <br/>
                                                                                   <div class="row" id="MapiKeNirdharnKiThithi" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div5" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                  मापी के लिए निर्धारित तिथि 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divMapiKeNirdharnKiThithiValue" runat="server" style="font-size:12px">
                                                                                                 <asp:Label ID="lblMapiKeNirdharnKiThithiValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                 </div>
        </div>
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                भूमि विवाद सें संबंधित घटना/ वारदात का विवरण
            </div>
            <div class="card-body">
                <div class="row">                                                                             
                                                                                                     <div class="col-md-3" id="div3" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                           प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ? :
                                                                                                     </div>
                                                                                                     <div class="col-md-3" id="div_Prathamik" runat="server" style="font-size:12px">
                                                                                                         <asp:Label ID="lblPrathamik" runat="server" Text=""></asp:Label>
                                                                                                     </div>                                                                                                                                                                                                                                            
                                                                                           </div> 
                                                                                            <br/>
                                                                                       <div class="row">
                                                                                                 <div class="col-md-12" >
                                                                                                                <asp:GridView runat="server" Width="100%" ID="grdbhumivivad"  AutoGenerateColumns="false"
                                                                                                    CssClass="table-responsive CSSTableGeneratorGrid" EmptyDataText="No Record Found!">                                                                                                  
                                                                                                    
                                                                                                                    <Columns>                                                                                                                      
                                                                                                                    <asp:TemplateField HeaderText="Sl. No.">
                                                                                                                                                <ItemTemplate>
                                                                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                                                                </ItemTemplate>
                                                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                                                            </asp:TemplateField>
                                                                                                                        <asp:BoundField DataField="Ghatna_Vardat_date" HeaderText="घटना की तिथि" ItemStyle-Width="80" />
                                                                                                                        <%--<asp:BoundField DataField="BriefDescriptionOfTheEvent" HeaderText="घटना की संक्षिप्त विवरण" ItemStyle-Width="220" />--%>
                                                                                                                        <asp:TemplateField HeaderText="घटना की संक्षिप्त विवरण" ItemStyle-Width="220">
                                                                                                                            <ItemTemplate>
                                                                                                                                <span id='<%# Eval("Ghatna_Short_vivran") %>' style="display: block;overflow: auto; height:25px;" >
                                                                                                                                    <%# Eval("Ghatna_Short_vivran") %>
                                                                                                                                </span>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:BoundField DataField="is_FIR_registered" HeaderText="प्राथमिकी" ItemStyle-Width="80" />
                                                                                                                        <asp:BoundField DataField="praathamiki_sankhya" HeaderText="प्राथमिकी संख्या" ItemStyle-Width="100" />
                                                                                                                        <%--<asp:BoundField DataField="DetailsOfFIR" HeaderText="प्राथमिकी/अप्राथमिकी का विवरण" ItemStyle-Width="220" />--%>
                                                                                                                        <asp:TemplateField HeaderText="प्राथमिकी का विवरण" ItemStyle-Width="220">
                                                                                                                            <ItemTemplate>
                                                                                                                                <span id='<%# Eval("praathamiki_ka_vivaran") %>' style="display: block;overflow: auto; height:25px;" >
                                                                                                                                    <%# Eval("praathamiki_ka_vivaran") %>
                                                                                                                                </span>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                        
                                                                                                                        <asp:BoundField DataField="is_complaint_filed" HeaderText="अप्राथमिकी" ItemStyle-Width="100" />
                                                                                                                        <asp:BoundField DataField="dhaara" HeaderText="धारा" ItemStyle-Width="80" />
                                                                                                                        <asp:BoundField DataField="apraathamiki_sankhya" HeaderText="अप्राथमिकी संख्या" ItemStyle-Width="100" />
                                                                                                                        <%--<asp:BoundField DataField="DetailsOfFIR" HeaderText="प्राथमिकी/अप्राथमिकी का विवरण" ItemStyle-Width="220" />--%>
                                                                                                                        <asp:TemplateField HeaderText="अप्राथमिकी का विवरण" ItemStyle-Width="220">
                                                                                                                            <ItemTemplate>
                                                                                                                                <span id='<%# Eval("apraathamiki_ka_vivaran") %>' style="display: block;overflow: auto; height:25px;" >
                                                                                                                                    <%# Eval("apraathamiki_ka_vivaran") %>
                                                                                                                                </span>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:BoundField DataField="is_Sanha_recorded" HeaderText="सनहा" ItemStyle-Width="80" />
                                                                                                                        <asp:BoundField DataField="sanha_sankhya" HeaderText="सनहा संख्या" ItemStyle-Width="100" />
                                                                                                                        <%--<asp:BoundField DataField="Accusation" HeaderText="वाद की अद्यतन स्थिति की अभियुक्ति" ItemStyle-Width="220" />--%>                            
                                                                                                                        <asp:TemplateField HeaderText="अभियुक्ति" ItemStyle-Width="220">
                                                                                                                            <ItemTemplate>
                                                                                                                                <span id='<%# Eval("Abhiyukt") %>' style="display: block;overflow: auto; height:25px;" >
                                                                                                                                    <%# Eval("Abhiyukt") %>
                                                                                                                                </span>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>                                     
                                                                                                                        </Columns>
                                                                                                                </asp:GridView>
                                                                                                 </div>
                                                                                      </div> 
                 </div>
        </div>
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                न्यायालय में प्रक्रियाधीन वाद का विवरण
            </div>
            <div class="card-body">
                <div class="row">                                                                             
                                                                                                  <div class="col-md-3" id="div11" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                           प्रक्रियाधीन वाद का विवरण उपलब्ध है ? :
                                                                                                   </div>
                                                                                                   <div class="col-md-3" id="divPrakiriyaVad" runat="server" style="font-size:12px">
                                                                                                         <asp:Label ID="lblPrakiriyaVad" runat="server" Text=""></asp:Label>
                                                                                                   </div>                                                                                                                                                                                                                                            
                                                                                           </div> 
                                                                                            <br/>
                                                                                       <div class="row">
                                                                                                 <div class="col-md-12"  >
                                                                                                                <asp:GridView runat="server" Width="100%" ID="grdnyayalay_vivran"
                                                                                                                                      AutoGenerateColumns="false" 
                                                                                                                  CssClass="table-responsive  CSSTableGeneratorGrid fontsize" 
                                                                                                                                     EmptyDataText="No Record Found!">                                                                                                              
                                                                                                                                  <Columns>                                                                                                                       
                                                                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                                                                                                    <ItemTemplate>
                                                                                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                                                                                    </ItemTemplate>
                                                                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                                                                                </asp:TemplateField>
                                                                                                                    <asp:BoundField DataField="court" HeaderText="न्यायालय" ItemStyle-Width="80" />
                                                                                                                    <asp:BoundField DataField="courtType" HeaderText="न्यायालय का प्रकार" ItemStyle-Width="100" /> 
                                                                                                                    <asp:BoundField DataField="Dst" HeaderText="जिला" ItemStyle-Width="80" />
                                                                                                                    <asp:BoundField DataField="SubDiv" HeaderText="अनुमंडल" ItemStyle-Width="80" />
                                                                                                                    <asp:BoundField DataField="Vibhag" HeaderText="विभाग" ItemStyle-Width="80" />                               
                                                                                                                    <asp:BoundField DataField="vaadi_ki_vaad_sankhya_varsh" HeaderText="वाद संख्या / वर्ष" ItemStyle-Width="80" />
                                
                                                                                                                    <asp:BoundField DataField="vadi_name" HeaderText="वादी का नाम" ItemStyle-Width="80" />
                                                                                                                    <asp:BoundField DataField="prativadi_name" HeaderText="प्रतिवादी का नाम" ItemStyle-Width="80" />
                                                                                                                    <asp:BoundField DataField="vaad_ki_addhatan_sthiti_vivaran" HeaderText="अद्धतन स्थिति का विवरण" ItemStyle-Width="80" />
                                               
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
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                
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
                                </Columns>
                            </asp:GridView>
                       
                    </div>
                </div>
                 </div>
        </div>

        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                मंतव्य की विवरणी
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                      
                             <asp:GridView ID="GridView1"  runat="server"
                                DataKeyNames="" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                BorderColor="#CCCCCC" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CssClass="CSSTableGeneratorGrid" AllowPaging="false" PageSize="25" Style="width: 100%;"
                                HeaderStyle-BackColor="Beige" OnRowDataBound="GridViewRemarks1_RowDataBound"
                                ShowHeaderWhenEmpty="True"  EmptyDataRowStyle-HorizontalAlign="Left" EmptyDataRowStyle-VerticalAlign="Middle"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="आवेदन संख्या" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <%#Eval("ApplicationNo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="मंतव्य" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <%#Eval("Remarks")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    
                                   
                                     <asp:TemplateField HeaderText="मंतव्य विवरण द्वारा" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("usernamee")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="दस्तावेज देखें" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                          
                                            <asp:ImageButton ID="Image1" class="getpdfdoc" Visible='<%# CheckImage(Eval("Remarks_file"))%>'  path='<%#Eval("Remarks_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" CommandArgument='<%# Container.DataItemIndex %>' CommandName="View"/>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                  
                                  
                                </Columns>
                            </asp:GridView>
                       
                    </div>
                </div>
                 </div>
        </div>


        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" EnableViewState="true" ViewStateMode="Enabled">
       <ContentTemplate>
        <div class="card mb-2">
            <div class="card-header bg-primary text-white">
                 >>> नई बैठक के अनुसार अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी जोड़ें >>>
            </div>
            <asp:HiddenField id="lastAction" runat="server" Value="0"/>
             <div class="card-body">                
                <div class ="row" >
                    <div class="col-md-3">
                              भूमि विवाद की सवेदनशीलता<img src="images/red_star_PNG44.png" width="13px" />
                               <asp:DropDownList AutoPostBack="true"  OnSelectedIndexChanged="ddlbhumivivadki_sanvedanshilta_SelectedIndexChanged" runat="server" 
                                   ID="ddlbhumivivadki_sanvedanshilta" CssClass="form-control" >
                                                            </asp:DropDownList>
                              </div>
                              <div class="col-md-3 text-center">
                                 <br />
                              <asp:Image Width="100px" ImageUrl="images/1.png" ID="onestar" runat="server" Visible="true"/>
                                                         <asp:Image Width="100px" ImageUrl="images/2.png" ID="twostar" runat="server" Visible="false"/>
                                                         <asp:Image Width="100px" ImageUrl="images/3.png" ID="threestar" runat="server" Visible="false"/>
                                                         <asp:Image Width="100px" ImageUrl="images/4.png" ID="fourstar" runat="server" Visible="false"/>
                              </div>
                    <div class="col-md-3"></div>
                    <div class="col-md-3"></div>
                </div>
                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                <div class="row" style="padding-top:7px;">
                        
                    </div>
                <div class="row" style="padding-top: 7px">
                    <div class="col-md-3">
                        बैठक की तिथि<img src="images/red_star_PNG44.png" width="13px" />
                    </div>
                     <div class="col-md-3">
                    
                         
                     
                    



                    <div class="input-group">
                              
                            

                            


                        <asp:TextBox ID="txtbaithakDate" runat="server" CssClass="form-control mb-2"
                                                         oncopy="return false" onpaste="return false" AutoComplete="off"
                                                        oncut="return false"  onkeypress="return dateValidate(event)"></asp:TextBox>

                                                        
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txtbaithakDate" Format="dd-MM-yyyy" CssClass="zindex">
                                                                            </cc1:CalendarExtender>





                            </div>

                          </div>
                       
                   
                    <div class="col-md-3">
                        क्या वादी उपस्थित है ?<img src="images/red_star_PNG44.png" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="ddlIsVadiAvailable" CssClass="form-control" >
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">हां</asp:ListItem>
                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row" style="padding-top: 7px">
                    <div class="col-md-3">
                        क्या प्रतिवादी उपस्थित है ?<img src="images/red_star_PNG44.png" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="ddl_IsprativadiAvailable" CssClass="form-control" >
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                            <asp:ListItem Value="Y">हां</asp:ListItem>
                            <asp:ListItem Value="N">नहीं</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        बैठक का निष्कर्ष(Action)<img src="images/red_star_PNG44.png" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlaction" CssClass="form-control"  OnSelectedIndexChanged="ddlaction_SelectedIndexChanged">
                            <asp:ListItem Value="0">--चुने--</asp:ListItem>
                             <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                            <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                            <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                            <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                            <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                            <asp:ListItem Value="6">न्यायालय में लंबित</asp:ListItem>
                        </asp:DropDownList>
                    </div> 
                </div>
                <div class="row" style="padding-top: 7px">
                    <div class="col-md-3">
                        अंचलाधिकारी का मंतव्य
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" Height="60px" ID="txtabhiyukt_anchaladhikari"
                            CssClass="form-control"></asp:TextBox>
                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                    </div>
                    <div class="col-md-6" id="divNextDateDef" runat="server" visible="true">
                      
                    </div>
                    <div class="col-md-3" id="divlabNextDate" runat="server" visible ="false">
                      <asp:Label ID="labNextDate"  Text="अगला/मापी की तिथि" runat="server"></asp:Label><img src="images/red_star_PNG44.png" width="13" />
                    </div>
                    <div class="col-md-3" id="divNextDate" runat="server" visible="false">
                                                   
                          
                         <div class="input-group">
                               <asp:TextBox ID="txtAgalaDate" runat="server" CssClass="form-control mb-2"
                                                         oncopy="return false" onpaste="return false" AutoComplete="off"
                                                        oncut="return false"  onkeypress='return dateValidate(event)'></asp:TextBox>                                      
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAgalaDate" Format="dd-MM-yyyy" CssClass="zindex">
                                                                            </cc1:CalendarExtender>
                            </div>
                    </div>
                    <div class="col-md-3" id="divvadkavars" runat="server" visible="false">
                                                   
                          
                         <div class="input-group">
                               <asp:TextBox ID="txtvadkavars" runat="server" CssClass="form-control mb-2"></asp:TextBox>                                      
                                                        
                            </div>
                    </div>



                     <div class="col-md-3" id="divCancelReason" runat="server" visible="false">                        
                            <asp:TextBox runat="server" ID="txtCancelReason" MaxLength="500" CssClass="form-control" TextMode="MultiLine" Height="60px"></asp:TextBox>
                     <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div> 
                     </div>

                </div>
                <div class="row" style="padding-top: 7px">
                    <%--
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-3">
                    </div> --%>
                    <div class="col-md-3">
                        थानाध्यक्ष का मंतव्य
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" Height="60px" ID="txtabhiyukt_thaanprabhaaree"
                            CssClass="form-control"></asp:TextBox>
                        <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                    </div>

                    <div class="col-md-3">
                        बैठक में लिया गया निर्णय<img src="images/red_star_PNG44.png" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtfalafal" MaxLength="500" TextMode="MultiLine" Height="60px" CssClass="form-control"></asp:TextBox>
                    <div style="text-align:right; color: #FF3300; font-weight: bold"> 
                                        अधिकतम 500 वर्ण
                                    </div>
                    </div>
                     
                </div>
                
                <div class="row" style="padding-top:7px;">                    
                    <div class="col-md-3">
                        थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="LandDoc" runat="server" CssClass="form-control" accept=".pdf" />
                        <asp:HiddenField ID="hdLandDoc" runat="server" />
                        <span style="color: #FF3300; font-weight: bold ;font-size:11px">
                            <asp:Label ID="Label4" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                    </div>



                    
                    <div class="col-md-3">
                        अंचलाधिकारी का मंतव्य पत्र
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="CircleOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />
                        <asp:HiddenField ID="hdCircleOfficer_letterofintent" runat="server" />
                        <span style="color: #FF3300; font-weight: bold;font-size:11px">
                            <asp:Label ID="Label8" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                       
                    </div>

                       <div class="col-md-3">
                        थानाध्यक्ष का मंतव्य पत्र
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="PoliceOfficer_letterOfIntent" runat="server" CssClass="form-control" accept=".pdf" />
                        <asp:HiddenField ID="hdPoliceOfficer_letterOfIntent" runat="server" />
                        <span style="color: #FF3300; font-weight: bold;font-size:11px">
                            <asp:Label ID="Label9" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                      
                    </div>                    
                    
                </div>                           
                         <div class="row" style="padding-top:7px;" id="lastActionMapi" runat="server" visible="false">                    
                    <div class="col-md-3">
                        मापी का प्रतिवेदन<img src="images/red_star_PNG44.png" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:FileUpload ID="lastActionMapiKaPrativadan" runat="server" CssClass="form-control" accept=".pdf" />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <span style="color: #FF3300; font-weight: bold ;font-size:11px">
                            <asp:Label ID="Label2" runat="server" Text="(Document केवल .pdf प्रारूप में 2 MB तक में अपलोड करे)"></asp:Label></span>
                    </div>



                    
                    <div class="col-md-3">
                        मापी क़े लिए निर्धारित तिथि<img src="images/red_star_PNG44.png" width="13px" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtMapikiNirdharitThiti" runat="server" CssClass="form-control mb-2"
                                                         oncopy="return false" onpaste="return false" AutoComplete="off"
                                                        oncut="return false"  onkeypress="return dateValidate(event)"></asp:TextBox>

                                                        
                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" OnClientDateSelectionChanged="checkDate" TargetControlID="txtMapikiNirdharitThiti" Format="dd-MM-yyyy" CssClass="zindex">
                                                                            </cc1:CalendarExtender>
                    </div>

                                      
                    
                </div>  
            </div>
            <div id="tddetails" runat="server" visible="true">
                <div class="row" style="padding-bottom: 0.5%; padding-left: 0.5%">
                    <div class="col-lg-4 col-md-3 col-sm-3 ">
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-3  ">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" onClientClick="return SaveAnotherMetting();"  OnClick="btnSave_Click" />
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-3  ">
                        <asp:Button ID="btnCancel" runat="server" Text="Go Back" CssClass="btn btn-danger" OnClientClick="JavaScript:window.history.back(1); return true;" OnClick="btnCancel_Click" />&nbsp;&nbsp;
                    </div>
                    <div class="col-lg-4 col-md-2 col-sm-3">
                        <asp:Button ID="btnDraft" CssClass="btn btn-info"  runat="server" Visible="false"
                                                Text="Send To Draft" />
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                    <center>
                        <asp:Label ID="lblMsg" runat="server"  ForeColor="Red" Font-Bold="True"></asp:Label>
                    </center>
                </div>
            </div>
        </div>
        </ContentTemplate>
             <Triggers>
                       <asp:PostBackTrigger ControlID="btnSave" />
                    </Triggers>
            </asp:UpdatePanel>

   

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
</asp:Content>

