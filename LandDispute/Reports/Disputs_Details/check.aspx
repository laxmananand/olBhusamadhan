<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false"
    CodeFile="check.aspx.cs" 
    Inherits="LandDispute_Reports_Disputs_Details_DisputeConsolidateBlockRpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <style type="text/css">     
        .modalBackground {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: auto;
            height: auto;
        }
        
        .aligenLeft
        {
            text-align: left;
        }
        
        .aligenRight
        {
            text-align: right !important;
            padding-right: 5px;
        }
        .padNum
        {
            padding-right: 5px;
        }
        .form-groupManual
        {
            margin-bottom: 0 !important;
        }
        .hrManual
        {
            margin-top: 0 !important;
            margin-bottom: 5px !important;
        }
    </style>
    <style type="text/css">
        .mGrid { 
    width: 100%; 
    background-color: #fff; 
    margin: 5px 0 10px 0; 
    border: solid 1px #525252; 
    border-collapse:collapse; 
}
.mGrid td { 
    /*
    padding: 2px; 
    border: solid 1px #c1c1c1; 
    color: #717171; 
        */
    vertical-align: top;
    border: 1px solid #c1c1c1;
    padding: 2px;
    font-size: 12pt;
    font-weight: normal;
    color: #000000;
     background-color:White;
}

.mGrid th { 
    /*
    padding: 4px 2px; 
    color: #fff; 
    background: #424242 url(grd_head.png) repeat-x top; 
    border-left: solid 1px #525252; 
    border-right: solid 1px #525252; 
    font-size: 1.0em; */

    background: -o-linear-gradient(bottom, #187ab9 5%, #014e9c 100%);
    background: -webkit-gradient( linear, left top, left bottom, color-stop(0.05, #187ab9), color-stop(1, #014e9c) );
    background: -moz-linear-gradient( center top, #187ab9 5%, #014e9c 100% );
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr="#187ab9", endColorstr="#014e9c");
    background: -o-linear-gradient(top,#187ab9,014e9c);
    background-color: #009900;
    border: 0px solid #014e9c;
    text-align: center;
    border-width: 0px 0px 1px 1px;
    font-size: 14px;
    color: #ffffff;
}
.mGrid .alt { /* background: #fcfcfc url(grd_alt.png) repeat-x top;*/ }
.mGrid .pgr { background: #424242 url(grd_pgr.png) repeat-x top; }
.mGrid .pgr table { margin: 5px 0; }
.mGrid .pgr td { 
    border-width: 0; 
    padding: 0 6px; 
    border-left: solid 1px #666; 
    font-weight: bold; 
    color: #fff; 
    line-height: 12px; 
 }   
.mGrid .pgr a { color: #666; text-decoration: none; }
.mGrid .pgr a:hover { color: #000; text-decoration: none; }
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
        $(document).ready(function () {
            $(".btnDefectReject").click(function () {
                if ($(".ddlDefRejRemarks").css("visibility") == "visible") {
                    if ($(".ddlDefRejRemarks").val() == 0) {
                        alert("Please Select Reason!");
                        $(".ddlDefRejRemarks").focus();
                        return false;
                    }

                    if ($(".txtDefRejRemarks").val().length < 6) {
                        alert("Please Enter Reamrks For Rejection/Defective!");
                        $(".txtDefRejRemarks").focus();
                        $(".txtDefRejRemarks").select();
                        return false;
                    }
                    //return false;
                }
                else {
                    //alert("Not found ddl visisble");
                    //return false;
                }
            });
        })
    </script>

    <script type="text/javascript">




        $("#ctl00_ContentPlaceHolder1_btnSearch").click(function () {
            alert("dd");
            $("#load").addClass("spinner-border");
            $("#load").addClass("spinner-border-sm");
            return false;
        });


        function check() {


            $("#load").addClass("spinner-border");
            $("#load").addClass("spinner-border-sm");
            return false;
        }

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
                url: "check.aspx/Getpdf",
                data: "{'url':'" + urlpdf + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d);
                    inlineFrameExample.src = response.d;
                    window.open("../../IDoc.aspx?url=" + response.d, "_blank");
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <div class="container-fluid">
          <h4 class="text-black text-center"><b>Circle Wise Dispute Consolidated Report</b></h4>

        
        <div class="row">
                    <center>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                        <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" style="text-align: center; font-weight: bold;"></asp:Label>
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
                                                                             <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>                                                                        
                                                                    </div>
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label5" runat="server" Text="Police Station"></asp:Label>
                                                                            <asp:DropDownList ID="ddlThana" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                         
                                                                    </div>  
                                                                 
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label6" runat="server" Text="Disputes Type"></asp:Label>
                                                                             <asp:DropDownList ID="ddlDisputesType" runat="server" CssClass="form-control" Enabled="true"   
                                         AutoPostBack="True">
                                                        <asp:ListItem Value="0">--All--</asp:ListItem>
                                             <asp:ListItem Value="1">पर्चाधारी के बेदखली का मामला</asp:ListItem>
                                             <asp:ListItem Value="2">सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद</asp:ListItem>
                                             <asp:ListItem Value="3">रैयती भूमि पर सीमांकन या सीमा का विवाद</asp:ListItem>
                                             <asp:ListItem Value="4">निजी रास्ता / नाली का विवाद</asp:ListItem>
                                             <asp:ListItem Value="5">जल स्रोत का विवाद</asp:ListItem>
                                             <asp:ListItem Value="6">पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद</asp:ListItem>
                                             <asp:ListItem Value="7">खेती से संबंधित विवाद</asp:ListItem>
                                             <asp:ListItem Value="8">वास से संबंधित विवाद</asp:ListItem>
                                             <asp:ListItem Value="9">लगान निर्धारण का विवाद</asp:ListItem>
                                             <asp:ListItem Value="10">व्यावसायिक भूमि से संबंधित विवाद</asp:ListItem>
                                             <asp:ListItem Value="11">बदलेन भूमि से संबंधित विवाद</asp:ListItem>
                                             <asp:ListItem Value="12">भू- अर्जन से संबंधित विवाद</asp:ListItem>
                                             <asp:ListItem Value="13">भू- हदबंदी (अधिशेष) से संबंधित विवाद</asp:ListItem>
                                             <asp:ListItem Value="15">रैयती भूमि पर कब्ज़ा का विवाद</asp:ListItem>
                                             <asp:ListItem Value="20">अन्य</asp:ListItem>    
                                          </asp:DropDownList>                                                                        
                                                                    </div>
                                                             </div>
                                                            

                                                                
                                                             </ContentTemplate>                                      
                                                 </asp:UpdatePanel>
                                                      <div class="row mb-2">
                                                                      <div class="col-md-2">
                                                                            <asp:Label ID="Label9" runat="server" Text="बैठक का निष्कर्ष"></asp:Label>
                                                                           <asp:DropDownList ID="ddlbaithak" runat="server" CssClass="form-control" Enabled="true" 
                                 >
                            </asp:DropDownList>                                                                           
                                                                    </div>  
                                                                    
                                                                <div class="col-md-2">
                                                                            <asp:Label ID="Label7" runat="server" Text=" Form Date"></asp:Label>
                                                                     <div class="input-group">
                                                                              <asp:TextBox ID="txtfrmdate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control" ></asp:TextBox>
                                               <span class="input-group-btn">
                                               <rjs:popcalendar ID="popCalendarFrom" runat="server" Control="txtfrmdate" Format="dd mm yyyy" />
                                                <asp:RequiredFieldValidator runat="server" id="RFVFromDate" ValidationGroup="a" controltovalidate="txtfrmdate" ForeColor="Red" errormessage="*" />
                                            </span>    
                                                                         </div>
                                                                    </div>
                                                              <div class="col-md-2">
                                                                            <asp:Label ID="Label8" runat="server" Text="  To Date"></asp:Label>
                                                                   <div class="input-group">
                                                                             <asp:TextBox ID="txtTodate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control"></asp:TextBox>
                                                         <span class="input-group-btn">
                                                             <rjs:popcalendar ID="popCalendarTo" runat="server" Control="txtTodate" Format="dd mm yyyy" />
                                                             <asp:RequiredFieldValidator runat="server" id="RFVToDate" ValidationGroup="a" controltovalidate="txtTodate" ForeColor="Red" errormessage="*" />
                                                         </span>  
                                                                       </div>
                                                                    </div>
                                                                    
                                                          <div class="col-md-2">
                                                                     <br />
                                                                     <asp:Button ID="btnSearch" runat="server" Style="float: right;" Text="Search"  CssClass="form-control btn btn-primary" onclick="btnSearch_Click" />  
                                                                 </div>
                                                                    <div class="col-md-2">
                                                                        <br />
                                                                         
                                                                        <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-primary"
                           Text="Export To Excel" OnClick="btnExpToExl_Click"  />
                                                                                                                                                
                                                                    </div> 
                                                                 
                                                                 
                                                                 <div class="col-md-2">
                                                                     <br />
                                                                     <asp:Button ID="btnback" Style="float: right;" CssClass="form-control btn btn-danger" Text="Back" runat="server" OnClick="btnback_Click" 
                            Visible="false" /></div>
                                                                    </div>

                                                                <div class="row mb-2">
                                                                    <asp:Panel ID="Pnldata" runat="server" Style="overflow-x:auto; overflow-y:hidden;"  Height="110%">
                    

                    
           
                </asp:Panel>
                                                                    </div>
                                                     <div class="row">
                    <div class="col-md-12">
                        <asp:Panel ID="pnlgrid" runat="server">
                            <div class="col-md-3">
                            </div>
                            <asp:Label ID="lblDateTime4" runat="server" ForeColor="Green" Font-Size="X-Small"></asp:Label><br />
                            <asp:Label ID="lblDetail5" runat="server" Text="*" ForeColor="Green" Font-Size="X-Small"></asp:Label>
                            <asp:GridView ID="griddata" runat="server" AutoGenerateColumns="false" CssClass="table-responsive" HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red" ShowFooter="true" Width="100%"  ShowHeaderWhenEmpty="true">
                                <Columns>

                                    <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" Width="4%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="6%" ForeColor="Black" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true"  Font-Size="Medium"  ForeColor="White"
                                            HorizontalAlign="left" Width="4%" />
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="DIVISIONAME" HeaderText="Division">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="6%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="Medium" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="DISTRICTNAME" HeaderText="District">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="6%"/>
                                         <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="X-Large" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Sd_Name_En" HeaderText="Sub Division">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="6%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="10px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="BlockName" HeaderText="Circle">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="6%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="10px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Police_Station" HeaderText="Police Station">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="6%"/>
                                         <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="Medium" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:BoundField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="कुल आवेदन" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkTotal" runat="server"
                                           CommandArgument='<%#Eval("Total")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("Total")%>' OnClick="lnkTotal_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="पूर्ण प्रविष्टि" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictFinalize" runat="server"  CommandArgument='<%#Eval("Finalize")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkFinalize_Click"><%# Eval("Finalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="आंशिक प्रविष्टि" HeaderStyle-Width="9%">	
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDistrictUnFinalize" runat="server"  CommandArgument='<%#Eval("UnFinalize")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                 ForeColor="Blue" Font-Underline="false" OnClick="lnkUnFinalize_Click"><%# Eval("UnFinalize")%>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Center" />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            HorizontalAlign="left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="पर्चाधारी के बेदखली का मामला" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkParchadhariBedakli" runat="server"
                                              CommandArgument='<%#Eval("ParchadhariBedakli")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("ParchadhariBedakli")%>' OnClick="lnkParchadhariBedakli_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkSarkariBhumiKabza" runat="server"  
                                          CommandArgument='<%#Eval("SarkariBhumiKabza")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("SarkariBhumiKabza")%>' OnClick="lnkSarkariBhumiKabza_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="रैयती भूमि पर सीमांकन या सीमा का विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkRaitiBhumiSeema" runat="server"  
                                           CommandArgument='<%#Eval("RaitiBhumiSeema")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("RaitiBhumiSeema")%>' OnClick="lnkRaitiBhumiSeema_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="निजी रास्ता / नाली का विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkNijiRastaNali" runat="server"  
                                           CommandArgument='<%#Eval("NijiRastaNali")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("NijiRastaNali")%>' OnClick="lnkNijiRastaNali_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                      
                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="जल स्रोत का विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkJalStrot" runat="server"  
                                          CommandArgument='<%#Eval("JalStrot")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("JalStrot")%>' OnClick="lnkJalStrot_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkPatrikBhumiBatwara" runat="server"  
                                           CommandArgument='<%#Eval("PatrikBhumiBatwara")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("PatrikBhumiBatwara")%>' OnClick="lnkPatrikBhumiBatwara_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%"  HeaderText="खेती से संबंधित विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkKhetiVivad" runat="server"  
                                          CommandArgument='<%#Eval("KhetiVivad")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("KhetiVivad")%>' OnClick="lnkKhetiVivad_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="वास से संबंधित विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkVaasVivad" runat="server"  
                                          CommandArgument='<%#Eval("VaasVivad")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("VaasVivad")%>' OnClick="lnkVaasVivad_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="लगान निर्धारण का विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkLaganNirdharan" runat="server"  
                                          CommandArgument='<%#Eval("LaganNirdharan")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("LaganNirdharan")%>' OnClick="lnkLaganNirdharan_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="व्यावसायिक भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkVyawsaikBhumi" runat="server" 
                                              CommandArgument='<%#Eval("VyawsaikBhumi")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("VyawsaikBhumi")%>' OnClick="lnkVyawsaikBhumi_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="बदलेन भूमि से संबंधित विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkBadlenBhumi" runat="server" 
                                           CommandArgument='<%#Eval("BadlenBhumi")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("BadlenBhumi")%>' OnClick="lnkBadlenBhumi_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="भू- अर्जन से संबंधित विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkBhuArjan" runat="server" 
                                           CommandArgument='<%#Eval("BhuArjan")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("BhuArjan")%>' OnClick="lnkBhuArjan_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="भू- हदबंदी (अधिशेष) से संबंधित विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkBhuHdBandi" runat="server"  
                                          CommandArgument='<%#Eval("BhuHdBandi")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("BhuHdBandi")%>' OnClick="lnkBhuHdBandi_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderStyle-Width="3%" HeaderText="रैयती भूमि पर कब्ज़ा का विवाद" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkRaitiBhumiKabza" runat="server"  
                                          CommandArgument='<%#Eval("RaitiBhumiKabza")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("RaitiBhumiKabza")%>' OnClick="lnkRaitiBhumiKabza_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                   <asp:TemplateField HeaderStyle-Width="3%" HeaderText="अन्य" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:LinkButton ID="lnkAnya" runat="server" 
                                          CommandArgument='<%#Eval("Anya")+","+Eval("DISTRICTCODE")+","+Eval("Sd_Code2")+","+Eval("BlockCode")+","+Eval("PS_Code")+","+"1"%>' 
                                                Text='<%#Eval("Anya")%>' OnClick="lnkAnya_Click"
                                                Font-Underline="false" ForeColor="Blue"> 
                                             </asp:LinkButton>                                          
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="left" Width="3%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="true" Font-Size="Small" ForeColor="White" HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                </div>
                  <div class="row">
                    <div class="col-md-12">
                         <asp:Panel ID="Pnlsearch" runat="server" Style="overflow-x:auto; overflow-y:hidden;" Visible="false">
                                  <asp:GridView ID="GridView1"  OnRowDataBound="GridView1_RowDataBound" runat="server" DataKeyNames="a_id"
                        AutoGenerateColumns="False" EnableTheming="False" Width="100%"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="None"
                        Style="width: 100%;" HeaderStyle-BackColor="Beige" OnPageIndexChanging="GridView1_PageIndexChanging" 
                        ShowFooter="True" EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333">
                                      <AlternatingRowStyle BackColor="White" CssClass="alt" ForeColor="#284775" />
                                      <Columns>
                                          <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("slno")%>'></asp:Label>
                                              </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Application No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="6%">
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="lnkApplicationNo" runat="server" CommandArgument='<%#Eval("a_id")%>' Font-Underline="false" ForeColor="Blue" OnClick="lnkView_Click" OnClientClick="openwindow(this);" Text='<%#Eval("ApplicationNo")%>'></asp:LinkButton>
                                              </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="कमिश्नरी &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt; जिला &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt; सब डिवीज़न" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                              <ItemTemplate>
                                                  <%#Eval("DIVISIONAME")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                                  <%#Eval("DISTRICTNAME")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                                  <%#Eval("Sd_Name_En")%>
                                              </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="अंचल &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;थाना " ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                              <ItemTemplate>
                                                  <%#Eval("BlockName")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                                  <%#Eval("Police_Station")%>
                                              </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="ग्राम पंचायत &lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;राजस्व ग्राम&lt;hr style='margin-bottom: 0px; margin-top: 0px;' /&gt;वार्ड" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="15%">
                                              <ItemTemplate>
                                                  <%#Eval("PanchayatName")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                                  <%#Eval("VILLNAME")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                                  <%#Eval("WARDNAME")%>
                                              </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="वादी का नाम " ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("vadi_Name")%>
                                                  <br/>
                                                  <%#Eval("TotalVadi")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="प्रतिवादी का नाम" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("pratiVadi_Name")%>
                                                  <br/>
                                                  <%#Eval("TotalPratiVadi")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="भूमि का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("Bhumitype")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                                  <%#Eval("SarkariBhumiType")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="भूमि विवाद का प्रकार" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("BhumiVivad")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="भूमि विवाद की &lt;/br&gt; सवेदनशीलता" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("Bhumi_savedansheelta")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="बैठक की तिथि" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("Meeting_date")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="बैठक का निष्कर्ष" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("Description")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="(Action)" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <div id="div_Action" runat="server" class="divclss">
                                                      <%#Eval("disposal")%>
                                                  </div>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="विवाद का अद्यतन कारक" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("bhumi_vivad_ka_adyatan_sthiti")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="वादी का &lt;/br&gt;दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <asp:ImageButton ID="Image6" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" Visible='<%# CheckImage(Eval("Vadi_sakshya_File"))%>' path='<%#Eval("Vadi_sakshya_File")%>' Style="cursor: pointer"  Width="50px" />
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderStyle-Width="10%" HeaderStyle-Wrap="false" HeaderText="प्रतिवादी का &lt;/br&gt;दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="10%">
                                              <ItemTemplate>
                                                  <asp:ImageButton ID="Image1" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("Prativadi_sakshya_File")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("Prativadi_sakshya_File"))%>' Width="50px" />
                                              </ItemTemplate>
                                              <HeaderStyle Width="10%" Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="पुलिस पदाधिकारी द्वारा समर्पित &lt;/br&gt;जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%">
                                              <ItemTemplate>
                                                  <div id="div_pulis_padadhikari_vivarani" runat="server" class="divclss" visible='<%# CheckNull(Eval("pulis_padadhikari_vivarani"))%>'>
                                                      <%#Eval("pulis_padadhikari_vivarani")%>
                                                  </div>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <asp:ImageButton ID="Image2" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("pulis_padadhikar_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("pulis_padadhikar_Patr_file"))%>' Width="50px" />
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="हल्का कर्मचारी / अंचल निरीक्षक द्वारा समर्पित &lt;/br&gt;जाँच प्रतिवेदन की संक्षिप्त विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="20%">
                                              <ItemTemplate>
                                                  <div id="div_HalkaKarmchari_vivran" runat="server" class="divclss" visible='<%# CheckNull(Eval("HalkaKarmchari_vivran"))%>'>
                                                      <%#Eval("HalkaKarmchari_vivran")%>
                                                  </div>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <asp:ImageButton ID="Image3" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("HalkaKarmchari_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("HalkaKarmchari_Patr_file"))%>' Width="50px" />
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="विवादित भू-खंड मापी का विवरणी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("vivadit_bhukhand_Mapi_ki_avashyakta_hai")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                                  <%#Eval("vivadit_bhukhand_Mapi")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px; border-color:#c1c1c1;' />
                                                  <asp:Label ID="Label1" runat="server" Text="माप के लिए निर्धारित तिथि : " Visible='<%# CheckNull(Eval("maapee_ke_lie_nirdhaarit_tithi"))%>'></asp:Label>
                                                  <%#Eval("maapee_ke_lie_nirdhaarit_tithi","{0:dd/MM/yyyy}")%>
                                                  <hr style='margin-bottom: 0px; margin-top: 0px;' />
                                                  <asp:Label ID="Label2" runat="server" Text="मापी नहीं होने का कारण :" Visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'></asp:Label>
                                                  <div id="div_vivaadit_bhukhand_Mapi_Reason" runat="server" class="divclss" visible='<%# CheckNull(Eval("vivaadit_bhukhand_Mapi_Reason"))%>'>
                                                      <%#Eval("vivaadit_bhukhand_Mapi_Reason")%>
                                                  </div>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="मापी का दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <asp:ImageButton ID="Image4" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("vivaadit_bhukhand_Mapi_File")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("vivaadit_bhukhand_Mapi_File"))%>' Width="50px" />
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="विवाद का &lt;/br&gt;प्राथमिकी/अप्राथमिकी" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("bhumi_vivad_Vivran_Available")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="न्यायालय में &lt;/br&gt;प्रक्रियाधीननन वाद" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <%#Eval("dispute_in_court_available")%>
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="आवेदन" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
                                              <ItemTemplate>
                                                  <asp:ImageButton ID="Image5" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%#Eval("ApplicationFile")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("ApplicationFile"))%>' Width="50px" />
                                              </ItemTemplate>
                                              <HeaderStyle Wrap="False" />
                                              <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="" Visible="false">
                                              <ItemTemplate>
                                                  <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%#Eval("a_id")%>' CssClass="btn btn-success" Font-Underline="false" ForeColor="Blue" OnClick="lnkView_Click" Text="View" ToolTip="Click Edit"></asp:LinkButton>
                                              </ItemTemplate>
                                              <HeaderStyle BackColor="#5bc0de" ForeColor="Black" />
                                          </asp:TemplateField>
                                      </Columns>
                                      <EditRowStyle BackColor="#999999" />
                                      <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                      <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                      <PagerStyle BackColor="#284775" CssClass="pgr" ForeColor="White" HorizontalAlign="Center" />
                                      <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                      <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                      <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                      <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                      <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                      <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>                           
                            </asp:Panel>
                    </div>
                </div>
                  <div class="row mb-2">
                             <div class="form-group text-center" style="padding-top: 8px; padding-bottom: 8px; border: none; ">
                                     <div class="col-md-12">
                                           <asp:Repeater ID="rptPager" runat="server">
                                                     <ItemTemplate>
                                                           <asp:LinkButton ID="lnkPage" runat="server" Text = '<%#Eval("Text") %>' CommandArgument = '<%# Eval("Value") %>' 
                                                             Enabled = '<%#Eval("Enabled")%>' 
                                                              OnClick = "Page_Changed" style="padding:5px; border-radius:5px;color:black;text-decoration:none"></asp:LinkButton>
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








  
      
     <%--<script src="../../../bhusamadhan/vendor/jquery/jquery.min.js"></script>
    <script src="../../../bhusamadhan/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../../../bhusamadhan/vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../../../bhusamadhan/vendor/chart.js/Chart.min.js"></script>
    <script src="../../../bhusamadhan/js/demo/chart-area-demo.js"></script>
    <script src="../../../bhusamadhan/vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <script src="../../../bhusamadhan/js/ruang-admin.min.js"></script>--%>
</asp:Content>