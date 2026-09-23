<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ViewApplicationDetails.aspx.cs" Inherits="HQ_ApplicationDetails" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../../../assets/css/TableCSSCode.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../../../Frontpage/assets/css/bootstrap.min.css" />   
    <link rel="stylesheet" type="text/css" href="../assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/font.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/li-scroller.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/slick.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/jquery.fancybox.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/theme.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/style.css" />
    <style type="text/css">
        .grid th
        {
            padding: 4px !important;
            font-weight: bold !important;
            font-size: small !important;
            text-align: center !important;
        }
        
        .grid td, th
        {
            padding: 4px !important;
            font-size: small !important;
        }
        
        .grid tr:hover
        {
            background-color: #d8f9d3 !important;
        }
        
        .grid td:hover
        {
            background-color: #ff2 !important;
        }
        
        .modalBackground
        {
            background-color: Gray !important;
            filter: alpha(opacity=80) !important;
            opacity: 0.8 !important;
            z-index: 10000 !important;
        }
         body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }
        .modal
        {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
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
        function fnLinkbutton(objlinkbutton) {
            //debugger;
            //Access the link button here
            var imgDiv = document.getElementById("divImage");
            var inlineFrameExample = document.getElementById("inlineFrameExample");
            var lb1 = document.getElementById(objlinkbutton).getAttribute("path");;
            var bookingID = lb1;
            alert(lb1);
            //  var a = "/LDHOME/LandDispute/uploads/LD212095111/LD212124065/fuIdDocumentLD212124065.pdf";
            var a = lb1.substring(1)
            // inlineFrameExample.src = '<%=ResolveUrl("' + a + '")%>';
            //inlineFrameExample.src = "/landdispute" + a;
            var base_Path = "/" + window.location.pathname.split('/')[1];
            inlineFrameExample.src = base_Path + a;
            var width = document.body.clientWidth;
            imgDiv.style.left = (width - 1200) / 2 + "px";
            imgDiv.style.top = "10px";

            imgDiv.style.display = "block";
            return false;
            //alert(lb1);
        }
        function fnLinkbutton1(objlinkbutton) {
            //debugger;
            //Access the link button here
            var imgDiv = document.getElementById("divImage");
            var inlineFrameExample = document.getElementById("inlineFrameExample");
            var lb1 = document.getElementById(objlinkbutton).getAttribute("path");;
            var bookingID = lb1;
            // alert(lb1);
            //  var a = "/LDHOME/LandDispute/uploads/LD212095111/LD212124065/fuIdDocumentLD212124065.pdf";
            var a = lb1.substring(1)
            // inlineFrameExample.src = '<%=ResolveUrl("' + a + '")%>';
            //inlineFrameExample.src = "/landdispute" + a;
            var base_Path = "/" + window.location.pathname.split('/')[1];
            inlineFrameExample.src = base_Path + a;
        var width = document.body.clientWidth;
        imgDiv.style.left = (width - 1200) / 2 + "px";
        imgDiv.style.top = "10px";

        imgDiv.style.display = "block";
        return false;
        //alert(lb1);
    }


    function js(url) {



        return false;
    }

    /*
function HideDiv() {
    var bcgDiv = document.getElementById("divBackground");
    var imgDiv = document.getElementById("divImage");
    var imgFull = document.getElementById("imgFull");
    imgDiv.style.display = "none";

}*/

            </script>

     <script type="text/javascript">

         function LoadDiv(url) {
             //debugger;
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
             //inlineFrameExample.src = "/landdispute" + a;
             var base_Path = "/" + window.location.pathname.split('/')[1];
             inlineFrameExample.src = base_Path + a;
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
         function HideDiv() {
             var bcgDiv = document.getElementById("divBackground");
             var imgDiv = document.getElementById("divImage");
             var imgFull = document.getElementById("imgFull");
             if (bcgDiv != null) {
                 bcgDiv.style.display = "none";
                 imgDiv.style.display = "none";
                 imgFull.style.display = "none";
             }
         }


         function SelectSinglebutton(rdBtnID) {
             document.getElementById(rdBtnID).setAttribute("Text", "redButton");
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
       <ContentTemplate>--%>
    <div id="contentSection">
        <div class="panel panel-primary">
                <div class="panel-heading" style="padding: 3px 20px">
                भूमि विवाद का विवरण
            </div>
                <div class="panel-body">
                 <div class="row" style="padding-top:5px;margin-left:10px">
                         <div class="col-md-3">
                               <asp:Label ID="lblDistrictname" runat="server" Text=" जिला:"></asp:Label>
                               <span><strong> <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label></strong></span>
                         </div>  
                         <div class="col-md-3">
                            <asp:Label ID="Label1" runat="server" Text="सब डिवीज़न :"></asp:Label>
                            <span><strong><asp:Label ID="lblSubdivision" runat="server" Text=""></asp:Label></strong></span>
                        </div>
                         <div class="col-md-3">
                            <asp:Label ID="Label2" runat="server" Text="अंचल :"></asp:Label>
                            <span><strong><asp:Label ID="lblBlock" runat="server" Text=""></asp:Label></strong></span>
                        </div>    
                         <div class="col-md-3">
                         <asp:Label ID="Label3" runat="server" Text="थाना :"></asp:Label>
                        <span><strong><asp:Label ID="lblPolice" runat="server" Text=""></asp:Label></strong></span>
                    </div>
                                                                
                  </div>     
                 <div class="row" style="padding-top:5px;margin-left:10px">
                     <div class="col-md-3">
                               <asp:Label ID="Label4" runat="server" Text="क्षेत्र का प्रकार :"></asp:Label>
                               <span><strong> <asp:Label ID="lblareatype" runat="server" Text=""></asp:Label></strong></span>
                      </div> 
                    <div class="col-md-3">
                            <asp:Label ID="Label5" runat="server" Text="ग्राम पंचायत :"></asp:Label>
                            <span><strong><asp:Label ID="lblPanchyat" runat="server" Text=""></asp:Label></strong></span>
                        </div>
                    <div class="col-md-3">
                            <asp:Label ID="Label6" runat="server" Text="राजस्व ग्राम/मौजा :"></asp:Label>
                            <span><strong><asp:Label ID="lblVillage" runat="server" Text=""></asp:Label></strong></span>
                        </div>  
                     <div class="col-md-3">
                         <asp:Label ID="Label7" runat="server" Text="वार्ड :"></asp:Label>
                        <span><strong><asp:Label ID="lblWard" runat="server" Text=""></asp:Label></strong></span>
                    </div>
                </div>           
                 <div class="row" style="padding-top:5px;margin-left:10px">
                     <div class="col-md-3">
                         <asp:Label ID="Label8" runat="server" Text=" राजस्व थाना संख्या :"></asp:Label>
                        <span><strong><asp:Label ID="lblrajaswa_sankhya" runat="server" Text=""></asp:Label></strong></span>
                    </div>
                     <div class="col-md-3">
                            <asp:Label ID="Label9" runat="server" Text="भूमि का प्रकार :"></asp:Label>
                            <span>
                                <strong><asp:Label ID="lblbhumitype" runat="server" Text=""></asp:Label> </strong>
                                <strong><asp:Label ID="lblsarkaribhumitype" runat="server"  Text=""></asp:Label> </strong>
                            </span>
                        </div>
                     <div class="col-md-3">
                         <asp:Label ID="Label10" runat="server" Text=" विवाद का अद्यतन कारक :"></asp:Label>
                         <span><strong><asp:Label ID="lblvivadKakarak" runat="server" Text="" Style="height: 50px;width:300px;overflow: scroll;" 
                            maximunsize="300px" autosize="true"></asp:Label></strong></span>
                    </div>
                     <div class="col-md-3">
                            <asp:Label ID="Label11" runat="server" Text="भूमि विवाद का प्रकार :"></asp:Label>
                            <span><strong><asp:Label ID="lblbhumivivadtype" runat="server" Style="height: 50px;width:300px;overflow: scroll;" maximunsize="300px" autosize="true" Text=""></asp:Label></strong></span>
                        </div>
                     
                 </div>
                 <div class="row" style="padding-top:5px;margin-left:10px">
                      <div class="col-md-3">
                            <asp:Label ID="Label12" runat="server" Text="आवेदन :"></asp:Label>
                            <span><strong><asp:ImageButton ID="Image1" path="" runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
                                            Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this);" /></strong></span>
                        </div>                       
                 </div>                 
             </div>                           
                <hr />
                <div class="row">
                 <div class="col-md-12" style="padding-top:5px;margin-left:10px">
                               <asp:Label ID="Label13" runat="server" Text=" भूमि का विवरण"></asp:Label>
                               
                 </div> 
            </div>                  
                <br />
                <div class="row" style="padding: 1.5% 2.5% 1% 2.5%;">
                    <div class="col-md-12">
                        <asp:Panel ID="PnlBhumiVivran" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Height="110%" ScrollBars="Both">
                            <asp:GridView ID="GVBhumiVivran" OnRowCommand="GVBhumiVivran_RowCommand" runat="server"
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                BorderColor="#CCCCCC" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CssClass="CSSTableGeneratorGrid" AllowPaging="false" PageSize="25" Style="width: 100%;"
                                HeaderStyle-BackColor="Beige" OnPageIndexChanging="GVBhumiVivran_PageIndexChanging" OnRowDataBound="GDV_RowDataBound"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                          
                                    <asp:TemplateField HeaderText="खाता संख्या" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("khataNo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" खेसरा संख्या" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("khesraNo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="रकबा (क्षेत्रफल)" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("RakbaNo1")%>
                                            <%#Eval("RakbaNo2")%>
                                            <%#Eval("RakbaNo3")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>                                     
                                     
                                    <asp:TemplateField HeaderText="खतियान में जमीन की किस्म का विवरण" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("LandTypesInKhatian")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="खतियान में जमीन का विवरण" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("LandDetailsInKhatian")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="पूर्व" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("East_chauhaddee")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="पश्चिम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("West_chauhaddee")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="उत्तर" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("North_chauhaddee")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="दक्षिण" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <%#Eval("South_chauhaddee")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    

                                    <%--<asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" Text='View' CssClass="btn btn-success"
                                                CommandArgument='<%#Eval("a_id")%>' ForeColor="Blue" Font-Underline="false" ToolTip="Click Edit"></asp:LinkButton>
                                             <asp:LinkButton ID="LinkDelete" runat="server"  CommandArgument='<%#Eval("BeneficieryId")%>' onclick="LinkDelete_Click" ForeColor="Blue" Font-Underline="false">Remove</asp:LinkButton>                                                                           
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>       
        <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                वादी का विवरण</div>
            <div class="panel-body">
                <div class="row" style="padding: 1.5% 2.5% 1% 2.5%;">
                    <div class="col-md-12">
                        <asp:Panel ScrollBars="Both" ID="pnlVadi" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Height="110%">
                            <asp:GridView ID="GVVadi" OnRowCommand="GVBhumiVivran_RowCommand" runat="server"
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                BorderColor="#CCCCCC" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CssClass="CSSTableGeneratorGrid" AllowPaging="false" PageSize="25" Style="width: 100%;"
                                HeaderStyle-BackColor="Beige" OnPageIndexChanging="GVBhumiVivran_PageIndexChanging" OnRowDataBound="GDV_RowDataBound"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="वादी की प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vadi_type")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="वादी का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("NameAsPerAadhaar")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="आधार संख्या" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("AadharNo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="लिंग चुने" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("SexAsPerAadhaar")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="उम्र" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("YearOfBirthAsPerAadhaar")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                               <asp:TemplateField HeaderText="क्या वादी किसी विभाग का प्रतिनिधि है" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("is_vadi_from_an_dept")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="विभाग का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vadi_dept_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="विभाग में पदनाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vadi_dept_pad_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="क्या वादी किसी संस्था का प्रतिनिधि है" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("is_vadi_from_an_org")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="संस्था का प्रकार" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vadi_org_type")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="संस्था का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vadi_org_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="संस्था में पदनाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vadi_org_pad_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                          
     
                                    <asp:TemplateField HeaderText="पिता/ पति का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <%#Eval("Vadi_Father_Husband_Name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="जिला" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("DISTRICTNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="सब डिवीज़न" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Sd_Name_En")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अंचल" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("BlockName")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="थाना" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Police_Station")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="क्षेत्र का प्रकार" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Vadi_AreaType")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ग्राम पंचायत" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("PanchayatName")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="राजस्व ग्राम" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("VILLNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वार्ड" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("WARDNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="मोबाइल संख्या" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Vadi_MobileNo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                           <%-- <asp:LinkButton ID="lnkView" runat="server" Text='View' CssClass="btn btn-success"
                                                CommandArgument='<%#Eval("a_id")%>' ForeColor="Blue" Font-Underline="false" ToolTip="Click Edit"></asp:LinkButton>--%>
                                            <%-- <asp:LinkButton ID="LinkDelete" runat="server"  CommandArgument='<%#Eval("BeneficieryId")%>' onclick="LinkDelete_Click" ForeColor="Blue" Font-Underline="false">Remove</asp:LinkButton>                                                                           --%>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                प्रतिवादी का विवरण</div>
            <div class="panel-body">
                <div class="row" style="padding: 1.5% 2.5% 1% 2.5%;">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlPratiVadi" ScrollBars="Both" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Height="110%">
                            <asp:GridView ID="GvpratiVadi" OnRowCommand="GVBhumiVivran_RowCommand" runat="server"
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                BorderColor="#CCCCCC" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CssClass="CSSTableGeneratorGrid" AllowPaging="false" PageSize="25" Style="width: 100%;"
                                HeaderStyle-BackColor="Beige" OnPageIndexChanging="GVBhumiVivran_PageIndexChanging" OnRowDataBound="GDV_RowDataBound"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                            <asp:TemplateField HeaderText="क्या प्रतिवादी किसी विभाग का प्रतिनिधि है" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("is_pratiVadi_from_an_dept")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="विभाग का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_dept_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="विभाग में पदनाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_dept_pad_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="क्या प्रतिवादी किसी संस्था का प्रतिनिधि है" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("is_pratiVadi_from_an_org")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="संस्था का प्रकार" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_org_type")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="संस्था का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_org_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="संस्था में पदनाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_org_pad_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                                     
                                    <asp:TemplateField HeaderText="प्रतिवादी का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>

                                            <%#Eval("pratiVadi_Name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="पिता/ पति का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_Father_Husband_Name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="जिला" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("DISTRICTNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="सब डिवीज़न" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Sd_Name_En")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अंचल" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("BlockName")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="थाना" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Police_Station")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="क्षेत्र का प्रकार" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_AreaType")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ग्राम पंचायत" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("PanchayatName")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="राजस्व ग्राम" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("VILLNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वार्ड" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("WARDNAME")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="मोबाइल संख्या" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("pratiVadi_MobileNo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                           <%-- <asp:LinkButton ID="lnkView" runat="server" Text='View' CssClass="btn btn-success"
                                                CommandArgument='<%#Eval("a_id")%>' ForeColor="Blue" Font-Underline="false" ToolTip="Click Edit"></asp:LinkButton>
--%>                                            <%-- <asp:LinkButton ID="LinkDelete" runat="server"  CommandArgument='<%#Eval("BeneficieryId")%>' onclick="LinkDelete_Click" ForeColor="Blue" Font-Underline="false">Remove</asp:LinkButton>                                                                           --%>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                 अन्य विवरण</div>
            <div class="panel-body">
                <div class="row" style="padding:0.5px;">
                <div class="col-md-3">
                <div class="col-md-10">
                 प्रतिवादी को सूचित किया गया है या नहीं ?
                </div>
                <div class="col-md-2">
                <strong>
                                        <asp:Label ID="lblIsPrativadiInformed" runat="server" Text=""></asp:Label></strong>
                </div>
                </div>
                 <div class="col-md-3">
                <div class="col-md-6">
                 माध्यम
                </div>
                <div class="col-md-6">
                <strong>
                                         <asp:Label ID="lblMadhayam" runat="server" Text=""></asp:Label></strong>
                </div>
                </div>
                 <div class="col-md-3">
                <div class="col-md-10">
                प्रतिवादी को सूचना का तामिला प्राप्त है या नहीं ?
                </div>
                <div class="col-md-2">
                <strong>
                                         <asp:Label ID="lblPrativadiTaamil" runat="server" Text=""></asp:Label></strong>
                </div>
                </div>
                 <div class="col-md-3">
                <div class="col-md-10">
                 प्रतिवादी उपस्थित हुआ है या नहीं ?
                </div>
                <div class="col-md-2">
                <strong>
                <asp:Label ID="lblPrativadipresent" runat="server" Text=""></asp:Label>
                 </strong>
                </div>
                </div>
                </div>

                <%-- 
                <div class="row" style="padding:0.5px;">
                <div class="col-md-4">
                 वादी और प्रतिवादी को सुनवाई मे उपस्थित हेतु नोटिस दी गई है ?</div>
                 <div class="col-md-2"><strong><asp:Label ID="lblNotice" runat="server" Text=""></asp:Label></strong></div>
                <div class="col-md-4">  नोटिस / सूचना का तामिला प्राप्त है ?</div>
                <div class="col-md-2"><strong><asp:Label ID="lblNoticePrapt" runat="server" Text=""></asp:Label></strong></div>
                </div>--%>
                 

                    
               
            </div>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                प्रस्तुत साक्ष्य की विवरणी</div>
            <div class="panel-body">
            <div class="row" style="padding:0.5px;">
            <div class="col-md-12"><strong>वादी द्वारा प्रस्तुत साक्ष्य का विवरण:</strong></div>
            </div>
            <div class="row" style="padding:0.5px;">
            <div class="col-md-1">खतियान :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblVadiKhatiyan" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> केवाला/विक्रय पत्र :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblVadikewala" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> जमाबंदी का नक़ल :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblVadiJamabandi" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1">लगान रसीद :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblVadiRasid" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1">वंशावली :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblVadiVanshawali" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> बटवारा (पारिवारिक या कोर्ट द्वारा) :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblVadiBatwara" runat="server"
                                        Text=""></asp:Label></strong></div>

            </div>
               <div class="row" style="padding:0.5px;">
            <div class="col-md-1">पर्चा (परवाना ) :</div>
            <div class="col-md-1"><strong>
                                        <asp:Label ID="lblVadiParcha" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> न्यायालय का आदेश : </div>
            <div class="col-md-1"><strong><asp:Label ID="lblVadiNyayalayKaAadesh" runat="server"
                                        Text=""></asp:Label></strong></div>
            <div class="col-md-2">वादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज : </div>
            <div class="col-md-1"><strong><asp:Label ID="lblVadiPrastutSakshya"
                                        runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> <asp:ImageButton ID="img1" path="" runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
                                            Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this);" />
                
                </div>
            <div class="col-md-1"></div>
            <div class="col-md-1"></div>
            <div class="col-md-1"></div>
            <div class="col-md-1"> </div>
            

            </div>

             <div class="row" style="padding:0.5px;">
            <div class="col-md-12"> <strong>प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण:</strong></div>
            </div>
            <div class="row" style="padding:0.5px;">
            <div class="col-md-1">खतियान :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadikhatiyan" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> केवाला/विक्रय पत्र :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadiKewala" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> जमाबंदी का नक़ल :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadiJamabandi" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1">लगान रसीद :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadiLanan" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1">वंशावली :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadiVanshawali" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> बटवारा (पारिवारिक या कोर्ट द्वारा) :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadiBatwara" runat="server" Text=""></asp:Label></strong></div>
            </div>
               <div class="row" style="padding:0.5px;">
            <div class="col-md-1">पर्चा (परवाना ) :</div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadiParcha" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> न्यायालय का आदेश : </div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadiNyayalayAadesh" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-2">प्रतिवादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज : </div>
            <div class="col-md-1"><strong><asp:Label ID="lblPrativadiPrastutSakshya" runat="server" Text=""></asp:Label></strong></div>
            <div class="col-md-1"> <asp:ImageButton ID="img2" path="" runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
                                            Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this);" /> </div>
            <div class="col-md-1"></div>
            <div class="col-md-1"></div>
            <div class="col-md-1"></div>
            <div class="col-md-1"> </div>
            

            </div>
             <hr />
            <div class="row" style="padding:0.5px;">
            <div class="col-md-4">पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन <br/> की संक्षिप्त विवरणी</div>
            <div class="col-md-2"><%--<div style="width: 300px; height: auto; overflow: scroll;">--%>
              <strong><asp:Label ID="lblPulisPadadhikariPrathamDristya" runat="server" Text=""></asp:Label></strong><%--</div>--%> </div>            
            
            <div class="col-md-4"> पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन <br/> का दस्तावेज</div>
            <div class="col-md-2"><strong> <%--<asp:Label ID="lblPulisPadadhikariJanch" runat="server" Text=""></asp:Label>--%>
            <%--<asp:Button ID="Button2" path="" Text="View Pdf  Document " runat="server" OnClientClick="return LoadDiv(this);" />--%>
            <asp:ImageButton ID="Image2" path="" runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
                                            Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this);" />
            </strong></div>            
            </div>
            <div class="row" style="padding:0.5px;">
            <div class="col-md-4"> हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित जाँच प्रतिवेदन <br/> की संक्षिप्त विवरणी<br />
                                    </div>
            <div class="col-md-2"><strong><asp:Label ID="lblHalkaKramchariViverani" runat="server" Text=""></asp:Label></strong></div>            
            
            <div class="col-md-4">हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित जाँच प्रतिवेदन <br/> का दस्तावेज</div>
            <div class="col-md-2"><strong><%--<asp:Label ID="lblHalkaKaramchariPrativedan" runat="server" Text=""></asp:Label>--%>
            <%--<asp:Button ID="Button3" path="" Text="View Pdf  Document " runat="server" OnClientClick="return LoadDiv(this);" />--%>
            <asp:ImageButton ID="Image3" path="" runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
                                            Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this);" />
            </strong></div>            
            
            </div>
            <div class="row" style="padding:0.5px;">
            <div class="col-md-3"> विवादित भू-खंड की मापी</div>
            <div class="col-md-7"><strong><asp:Label ID="lblVivaditBhukhandkiMapi" runat="server" Text=""></asp:Label></strong></div> 
                <div class="col-md-2">
                    <asp:ImageButton ID="Image4" path="" runat="server" ImageUrl="~/images/pdf.gif" Width="50px"
                                            Height="50px" Style="cursor: pointer" OnClientClick="return LoadDiv(this);" />
                </div>           
            </div>

            </div>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी</div>
            <div class="panel-body">
                <div class="row" style="padding: 1.5% 2.5% 1% 2.5%;">
                    <div class="col-md-12">
                        <asp:Panel ID="PnlAnchalaDhakari" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Height="110%">
                            <asp:GridView ID="GVAnchalaDhakari" OnRowCommand="GVBhumiVivran_RowCommand" runat="server"
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                BorderColor="#CCCCCC" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CssClass="CSSTableGeneratorGrid" AllowPaging="false" PageSize="25" Style="width: 100%;"
                                HeaderStyle-BackColor="Beige" OnPageIndexChanging="GVBhumiVivran_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="भूमि विवाद की सवेदनशीलता" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("SensitivityType")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="बैठक की तिथि" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Meeting_date", "{0:dd, MMM yyyy}")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="क्या वादी उपस्थित है ?" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Is_Vadi_Present")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="क्या प्रतिवादी उपस्थित है ?" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Is_PratiVadi_Present")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="बैठक का निष्कर्ष" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Action")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>  
                                    <asp:TemplateField HeaderText="बैठक में लिया गया निर्णय" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("conclusion_of_the_meeting")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अंचलाधिकारी का मंतव्य" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("anchala_dhikari_mantavy")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%#Eval("Police_Station")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>  --%>
                                    <asp:TemplateField HeaderText="थानाध्यक्ष का मंतव्य" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("thana_prabhari_mantavy")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>                                            
                                            <asp:ImageButton ID="Image1" Visible='<%# CheckNull(Eval("Joint_report_SHO_Circle_Officer_file"))%>' path='<%#Eval("Joint_report_SHO_Circle_Officer_file")%>' runat="server" ImageUrl="~/images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />
                                 <%--<asp:LinkButton ID="lb4"  path='<%#Eval("Prativadi_sakshya_File")%>' Text="DFdfdf"  runat="server">View Pdf</asp:LinkButton>--%>
                                 <%--   <%#Eval("Prativadi_sakshya_File")%>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--- <asp:TemplateField HeaderText="">
                                     <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" Text='View' CssClass="btn btn-success"
                                                        CommandArgument='<%#Eval("a_id")%>' ForeColor="Blue" Font-Underline="false"
                                                        ToolTip="Click Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkDelete" runat="server"  CommandArgument='<%#Eval("BeneficieryId")%>' onclick="LinkDelete_Click" ForeColor="Blue" Font-Underline="false">Remove</asp:LinkButton> 
                                                     </ItemTemplate>
                                                            <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                              </asp:TemplateField>
                                    --%>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                भूमि विवाद सें संबंधित घटना/ वारदात का विवरण</div>
            <div class="panel-body">
                <div class="row" style="padding: 1.5% 2.5% 1% 2.5%;">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlBhumiVivadGhatna" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Height="110%">
                            <asp:GridView ID="GVBhumiVivadGhatna" OnRowCommand="GVBhumiVivran_RowCommand" runat="server"
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                BorderColor="#CCCCCC" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CssClass="CSSTableGeneratorGrid" AllowPaging="false" PageSize="25" Style="width: 100%;"
                                HeaderStyle-BackColor="Beige" OnPageIndexChanging="GVBhumiVivran_PageIndexChanging" OnRowDataBound="GDV_RowDataBound"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="घटना / वारदात की तिथि" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Ghatna_Vardat_date", "{0:dd, MMM yyyy}")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="घटना की संक्षिप्त विवरण" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("Ghatna_Short_vivran")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="प्राथमिकी दर्ज है ?" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("is_FIR_registered")%>
                                            <hr id="Hr1" runat="server" style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' visible='<%# CheckNull(Eval("praathamiki_sankhya"))%>' />
                                            <div id="div_praathamiki_sankhya" runat="server"  visible='<%# CheckNull(Eval("praathamiki_sankhya"))%>'>
                                            प्राथमिकी संख्या: <%#Eval("praathamiki_sankhya")%>
                                                </div>
                                            <hr id="Hr2" runat="server" style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' visible='<%# CheckNull(Eval("praathamiki_ka_vivaran"))%>' />                                            
                                            <div id="div_praathamiki_ka_vivaran" runat="server" class="divclss"  visible='<%# CheckNull(Eval("praathamiki_ka_vivaran"))%>'>                                 
                                                प्राथमिकी का विवरण:
                                                <%#Eval("praathamiki_ka_vivaran")%>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="अप्राथमिकी दर्ज है ?" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("is_complaint_filed")%>
                                            <hr id="Hr3" runat="server" style="margin-bottom:0px; margin-top:0px; border-color:#c1c1c1;" visible='<%# CheckNull(Eval("dhaara"))%>' />
                                            <div id="div_dhaara" runat="server"  visible='<%# CheckNull(Eval("dhaara"))%>'>
                                            धारा: <%#Eval("dhaara")%>
                                                </div>
                                            <hr id="Hr4" runat="server" style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' visible='<%# CheckNull(Eval("apraathamiki_sankhya"))%>' />
                                             <div id="div_apraathamiki_sankhya" runat="server"  visible='<%# CheckNull(Eval("apraathamiki_sankhya"))%>'>
                                            अप्राथमिकी संख्या: <%#Eval("apraathamiki_sankhya")%>
                                                </div>
                                            <hr id="Hr5" runat="server" style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' visible='<%# CheckNull(Eval("apraathamiki_ka_vivaran"))%>' />                                            
                                            <div id="div_apraathamiki_ka_vivaran" runat="server" class="divclss"  visible='<%# CheckNull(Eval("apraathamiki_ka_vivaran"))%>'>                                 
                                                अप्राथमिकी का विवरण:
                                                <%#Eval("apraathamiki_ka_vivaran")%>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="अभियुक्ति :" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div id="div_Abhiyukt" runat="server" class="divclss"  visible='<%# CheckNull(Eval("Abhiyukt"))%>'>                                 
                                            <%#Eval("Abhiyukt")%>
                                                </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%--- <asp:TemplateField HeaderText="">
                                     <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" Text='View' CssClass="btn btn-success"
                                                        CommandArgument='<%#Eval("a_id")%>' ForeColor="Blue" Font-Underline="false"
                                                        ToolTip="Click Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkDelete" runat="server"  CommandArgument='<%#Eval("BeneficieryId")%>' onclick="LinkDelete_Click" ForeColor="Blue" Font-Underline="false">Remove</asp:LinkButton> 
                                                     </ItemTemplate>
                                                            <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                              </asp:TemplateField>
                                    --%>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-primary">
            <div class="panel-heading" style="padding: 3px 20px">
                न्यायालय में वाद का विवरण
            </div>
            <div class="panel-body">
                <div class="row" style="padding: 1.5% 2.5% 1% 2.5%;">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlCourtVad" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Height="110%">
                            <asp:GridView ID="GVCourtVad" OnRowCommand="GVBhumiVivran_RowCommand" runat="server"
                                DataKeyNames="a_id" AutoGenerateColumns="false" EnableTheming="false" Width="100%"
                                BorderColor="#CCCCCC" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                CssClass="CSSTableGeneratorGrid" AllowPaging="false" PageSize="25" Style="width: 100%;"
                                HeaderStyle-BackColor="Beige" OnPageIndexChanging="GVBhumiVivran_PageIndexChanging" OnRowDataBound="GDV_RowDataBound"
                                ShowFooter="true" EmptyDataText="No Record Found" Visible="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1+"." %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="न्यायालय" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("CourtName")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="राजस्व न्यायालय का प्रकार" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("CourtTypeName")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="जिला/अनुमंडल" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("DISTRICTNAME")%><%#Eval("Sd_Name_En")%>
                                            
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वादी की वाद संख्या / वर्ष" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vaadi_ki_vaad_sankhya_varsh")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वादी का नाम" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("vadi_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="प्रतिवादी का नाम" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <%#Eval("prativadi_name")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="वाद की अद्यतन <br/> स्थिति का विवरण:" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <div id="div_Abhiyukt" runat="server" class="divclss"  visible='<%# CheckNull(Eval("vaad_ki_addhatan_sthiti_vivaran"))%>'>                                 
                                            <%#Eval("vaad_ki_addhatan_sthiti_vivaran")%>
                                                </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>


            </div>
        </div>      
            <div class="panel-body">
                <div class="row" style="padding-bottom: 0.5%; padding-left: 0.5%">
                    <div class="col-lg-4 col-md-4 col-sm-4 ">
                    </div>
                    
                    <div class="col-lg-8 col-md-8 col-sm-8  ">
                        <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn btn-danger" OnClientClick="JavaScript:window.history.back(1); return false;" />&nbsp;&nbsp;
                    </div>
                    
                </div>
               
        </div>
        </div>
    <div id="divBackground" class="modal">
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
   
    
    <%--</ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="RowCommand" />
     <asp:AsyncPostBackTrigger ControlID="lnkregister" EventName="Click"/> 
    </Triggers>
      </asp:UpdatePanel>--%>
</asp:Content>
