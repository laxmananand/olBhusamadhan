<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddStatusApplicationPublic.aspx.cs" Inherits="Police_Station_add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #divImage {
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
            z-index: 10001;
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
                    <div class="col-md-3" style="font-weight: bold; font-size: 18px">

                        <asp:Label ID="lblApplicationNo" runat="server" Text="आवेदन संख्या "></asp:Label>
                    </div>
                    <div class="col-md-6"></div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 18px">

                        <asp:Label ID="lblAppDate" runat="server" Text="आवेदन की तिथि "></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        जिला
                    </div>
                    <div class="col-md-3" style="font-size: 12px">
                        <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        अनुमंडल
                    </div>
                    <div class="col-md-3" style="font-size: 12px">
                        <asp:Label ID="lblSubdivision" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        अंचल
                    </div>
                    <div class="col-md-3" style="font-size: 12px">
                        <asp:Label ID="lblBlock" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        थाना
                    </div>
                    <div class="col-md-3" style="font-size: 12px">
                        <asp:Label ID="lblPolice_Station" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        क्षेत्र का प्रकार
                    </div>
                    <div class="col-md-3" style="font-size: 12px">
                        <asp:Label ID="lblAreaType" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        <asp:Label ID="lblVillage" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-size: 12px">
                        <asp:Label ID="lblPanchayatName" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" id="div_Vadi_Svarajaya_Label" runat="server" style="font-weight: bold; font-size: 14px">
                        राजस्व ग्राम
                    </div>
                    <div class="col-md-3" id="div_Vadi_Svarajaya" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblVILLNAME" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        वार्ड 
                    </div>
                    <div class="col-md-3" id="div_Vadi_Ward" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblWARDNAME" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        विवाद का अद्यतन कारक
                    </div>
                    <div class="col-md-3" id="div_vadi_Vivad_Ka_Vighatan" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblvadi_Vivad_Ka_Vighatan" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        राजस्व थाना संख्या
                    </div>
                    <div class="col-md-3" id="div_vadi_rajashv_sankhaya" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblvadi_rajashv_sankhaya" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        भूमि का प्रकार
                    </div>
                    <div class="col-md-3" id="div_Vadi_BhumiKaPrakar" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblVadi_BhumiKaPrakar" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" id="div_vadi_sarkari_bhumi_ka_prakar_Label" runat="server" style="font-weight: bold; font-size: 14px">
                        सरकारी भूमि का प्रकार
                    </div>
                    <div class="col-md-3" id="div_Preview_vadi_sarkari_bhumi_ka_prakar" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblvadi_sarkari_bhumi_ka_prakar_Label" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label" runat="server" style="font-weight: bold; font-size: 14px">
                        सरकारी भूमि का प्रकार (अगर अन्य है)
                    </div>
                    <div class="col-md-3" id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        भूमि विवाद का प्रकार
                    </div>
                    <div class="col-md-3" id="div_BhumiKa_VivadPrakar" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblBhumiKa_VivadPrakar" runat="server"></asp:Label>
                    </div>
                    <div class="col-md-3" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label" runat="server" style="font-weight: bold; font-size: 14px">
                        <asp:Label ID="Label1" runat="server" Text="भूमि विवाद का प्रकार (अगर अन्य है) :"></asp:Label>
                    </div>
                    <div class="col-md-3" id="div_Preview_vadi_Bhumivivad_Prakar_Anaya" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblvadi_Bhumivivad_Prakar_Anaya" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी 
                    </div>
                    <div class="col-md-9" id="divVadiKabhumiVivaran" runat="server" style="font-size: 12px">
                        <asp:Label ID="lblVadiKabhumiVivaran" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3" style="font-weight: bold; font-size: 14px">
                        वादी द्वारा प्रस्तुत आवेदन 
                    </div>
                    <div class="col-md-3">
                        <asp:ImageButton ID="lnkAppDoc" path="display" class="getpdfdoc" runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
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
                                    <asp:Label ID="lblGender" runat="server" Text='<%# (Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ) ? "Female" : "Male"  %>'></asp:Label>
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
                                    <asp:Label ID="lblDeppratinidhi" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_dept")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="संस्था का प्रतिनिधि" ItemStyle-Width="80">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrgpratinidhi" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="विभाग/संस्था का नाम" ItemStyle-Width="100">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrgName" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? Eval("vadi_org_name") : Eval("org_type")   %>'></asp:Label>
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
                                    <asp:Label ID="lblOrgName" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ) ? Eval("pratiVadi_org_name") : Eval("org_type")   %>'></asp:Label>
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
                भूमि का खाता-खेसरा का विवरण
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12" id="divbhumikhata_shekher_ka_vivaran" runat="server">

                        <asp:GridView runat="server" ID="grd_bhumivivad"
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
                                        <span id='<%# Eval("LandDetailsInKhatian") %>' style="display: block; overflow: auto; height: 25px;">
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
                        <asp:GridView runat="server" Width="100%" ID="gdVadiEvidence" AutoGenerateColumns="false"
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
                                        <asp:ImageButton ID="Image1" path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" CommandArgument='<%# Container.DataItemIndex %>' CommandName="View" />

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
                        >>> Add status >>>
                    </div>
                    <asp:HiddenField ID="lastAction" runat="server" Value="0" />
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                Status<img src="images/red_star_PNG44.png" width="13px" />
                                <asp:DropDownList AutoPostBack="true" runat="server"
                                    ID="ddlStatus" CssClass="form-control" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Accepted</asp:ListItem>
                                    <asp:ListItem Value="2">Rejected</asp:ListItem>
                                    <asp:ListItem Value="3">Return</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 text-center">
                                <br />

                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3"></div>
                        </div>
                        <hr style="margin-bottom: 0px; margin-top: 0px;" />
                        <div class="row" style="padding-top: 7px;" id="divRejected" runat="server" visible="false">
                            <div class="col-md-3">
                                Reason<img src="images/red_star_PNG44.png" width="13px" />
                                <asp:DropDownList AutoPostBack="true" runat="server"
                                    ID="ddlReason" CssClass="form-control" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1">Application already entered</asp:ListItem>
                                    <asp:ListItem Value="2">Under Trial in court</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3" id="divApplication" runat="server" visible="false">
                                Application No<img src="images/red_star_PNG44.png" width="13px" />
                                <asp:TextBox AutoPostBack="true" runat="server"
                                    ID="txtApplicationNo" CssClass="form-control" >
                                    
                                </asp:TextBox>
                            </div>

                        </div>
                        <div class="row" style="padding-top: 7px;" id="divReturn" runat="server" visible="false">
                            <div class="col-md-3">
                                Remarks<img src="images/red_star_PNG44.png" width="13px" />
                                <asp:TextBox AutoPostBack="true" runat="server"
                                    ID="txtRemarks" CssClass="form-control">
    
                                </asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div id="tddetails" runat="server" visible="true">
                        <div class="row" style="padding-bottom: 0.5%; padding-left: 0.5%">
                            <div class="col-lg-4 col-md-3 col-sm-3 ">
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3  ">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClientClick="return SaveAnotherMetting();" OnClick="btnSave_Click" />
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-3  ">
                                <asp:Button ID="btnCancel" runat="server" Text="Go Back" CssClass="btn btn-danger" OnClientClick="JavaScript:window.history.back(1); return true;" OnClick="btnCancel_Click" />&nbsp;&nbsp;
                            </div>
                            <div class="col-lg-4 col-md-2 col-sm-3">
                                <asp:Button ID="btnDraft" CssClass="btn btn-info" runat="server" Visible="false"
                                    Text="Send To Draft" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <center>
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
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
                        <iframe id="inlineFrameExample" title="Inline Frame Example" width="99%" height="600px" src=""></iframe>
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

