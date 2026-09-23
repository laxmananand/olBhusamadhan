<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AllApplication.aspx.cs" 
    Inherits="LandDispute_THANA_Thana_Entry" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl" TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <%--<link rel="stylesheet" type="text/css" href="../../Frontpage/assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/font.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/li-scroller.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/slick.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/jquery.fancybox.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/theme.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/style.css" />--%>
        
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
        
        
    </style>

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
    </style>

    <style type="text/css">
        .grid th
        {
            padding: 4px;
            font-weight: bold;
            font-size: small;
            text-align: center;
        }
        
        .grid td, th
        {
            padding: 4px;
            font-size: small;
        }
        
        .grid tr:hover
        {
            background-color: #d8f9d3;
        }
        
        .grid td:hover
        {
            background-color: #ff2;
        }
        
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
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
  
    <style type="text/css">
.Background {
    background-color: Black;
    filter: alpha(opacity=90);
    opacity: 0.9;
}

.Popup {
    background-color: #FFFFFF;
    border-width: 3px;
    border-style: solid;
    border-color: black;
    padding-top: 10px;
    padding-left: 2px;
    width: 80%;
   
}

 .button-container {
   
    position: sticky; /* Make button container sticky */
    top: 0; /* Stick to the top of the container */
    background: #FFFFFF; /* Match modal background */
    z-index: 10; /* Ensure it stays above other content */
    text-align: right; /* Align button to the right */
    padding: 10px; /* Optional padding */
}

.container {
      max-width: 1535px !important;
    display: flex; /* Flexbox for the container */
    justify-content: center; /* Center items horizontally */
    align-items: center; /* Center items vertically */
    height: 100%; /* Full height of the modal */
}

.row {
    width: 100%; /* Ensure row takes full width */
}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            url: "ApplicationDistConsolidateRpt.aspx/Getpdf",
            data: "{'url':'" + urlpdf + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.open("../../IDoc.aspx?url=" + response.d, "_blank");
                // inlineFrameExample.src = response.d;
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


  
  

    function js(url) {
       

        
        return false;
    }
    function HideDiv() {
        var bcgDiv = document.getElementById("divBackground");
        var imgDiv = document.getElementById("divImage");
        var imgFull = document.getElementById("imgFull");
        imgDiv.style.display = "none";
        
    }
</script>
 
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
     <div class="container-fluid">
         <div class="card">
             <div class="card-body">
                  <div class="row">
                     <div class="col-md-12">
                        <h4 class="text-dark" style="text-align: center; font-weight: bold;">
                            Entry Count Thana wise Report</h4>
                    </div>
                 </div>
                   <div class="row">
                    <div class="col-md-3">                      
                    </div>
                    <div class="col-md-6">                    
                         <span style="text-align:center">
                       <asp:Label ID="lbltext" runat="server" Visible="false" CssClass="text-dark" style="text-align: center; font-weight: bold;"></asp:Label>
                       </span>
                    </div>
                </div>
                  <div class="row" style="padding:5px">
                   <div class="col-md-1 text-center"></div>
                 <div class="col-md-2 text-center">
                    Commissionary
                </div>
                 <div class="col-md-2 text-center">
                    District
                </div>
                 <div class="col-md-2 text-center">
                    Sub - Division
                </div>
                 <div class="col-md-2 text-center">
                    Circle
                </div>
                 <div class="col-md-2 text-center">
                    Thana
                </div>               
                 <div class="col-md-2 text-center"></div>
            </div>
                  <asp:UpdatePanel ID="up1" runat="server">
                      <ContentTemplate>                    
                  <div class="row" style="padding:5px">
                   <div class="col-md-1 text-center"></div>
                     <div class="col-md-2 text-center">
                        <asp:DropDownList ID="ddlCommissionary" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlCommissionary_SelectedIndexChanged"  
                             AutoPostBack="True">
                        </asp:DropDownList>
                     </div>
                     <div class="col-md-2 text-center">
                         <asp:DropDownList ID="ddldistrict" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddldistrict_SelectedIndexChanged"  
                             AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                     <div class="col-md-2 text-center">
                        <asp:DropDownList ID="ddlsubdivision" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlsubdivision_SelectedIndexChanged"  
                             AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                     <div class="col-md-2 text-center">
                        <asp:DropDownList ID="ddlcircle" runat="server" CssClass="form-control" Enabled="true" OnSelectedIndexChanged="ddlcircle_SelectedIndexChanged"  
                             AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                     <div class="col-md-2 text-center">
                       <asp:DropDownList ID="ddlthana" runat="server" CssClass="form-control" Enabled="true"
                             AutoPostBack="True">
                        </asp:DropDownList>
                    </div>               
                  <div class="col-md-2 text-center"></div>
               </div>  
                  </ContentTemplate>
                  </asp:UpdatePanel>                 
                   <div class="row" style="padding:5px">
                        <div class="col-md-1 text-center" ></div>
                         <div class="col-md-6 text-center" >
                               <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Always">
                                      <ContentTemplate>   
                                         <div class="row" style="padding:5px">
                                            
                                   <div class="col-md-4 text-center" >                        
                                     Count
                                      <br/>
                                        <asp:DropDownList ID="ddlcount" runat="server" CssClass="form-control" Enabled="true"                             
                                             AutoPostBack="True"
                                            >
                                            <asp:ListItem Value="0" Text="--Select--" Enabled="true"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="0" Enabled="true"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="1-5" Enabled="true"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="6-10" Enabled="true"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="11-15" Enabled="true"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="16-20" Enabled="true"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="20-30" Enabled="true"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Above-30" Enabled="true"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>                  
                                   <div class="col-md-4 text-center" >
                                            Form Date<br/>
                                            <div class="input-group">
                                               <asp:TextBox ID="txtfrmdate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control" ></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <rjs:popcalendar ID="popCalendarFrom" runat="server" Control="txtfrmdate" Format="dd mm yyyy" />
                                                     <asp:RequiredFieldValidator runat="server" id="RFVFromDate" ValidationGroup="a" controltovalidate="txtfrmdate" ForeColor="Red" errormessage="*" />
                                                </span>
                                           </div>
                                     </div>
                                   <div class="col-md-4 text-center" >
                                            To Date<br/>
                                              <div class="input-group">
                                                 <asp:TextBox ID="txtTodate" runat="server" placeholder="dd-mm-yyyy" ReadOnly="true" class="form-control"></asp:TextBox>
                                                 <span class="input-group-btn">
                                                     <rjs:popcalendar ID="popCalendarTo" runat="server" Control="txtTodate" Format="dd mm yyyy" />
                                                     <asp:RequiredFieldValidator runat="server" id="RFVToDate" ValidationGroup="a" controltovalidate="txtTodate" ForeColor="Red" errormessage="*" />
                                                 </span>
                                            </div>
                                      </div>                                  
                            </div>
                                     </ContentTemplate>
                                 </asp:UpdatePanel>   
                         </div>
                          <div class="col-md-2 text-center" >
                                 <br/>
                                 <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" 
                                CssClass="form-control btn btn-primary"  />
                         </div>  
                        <div class="col-md-2 text-center" >
                              <br/>
                               <asp:Button ID="btn_Export" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Export To Excel" OnClick="btnExpToExl_Click"  />
                         </div>  
                   </div>                                                   
                   
                    <div class="row">
                        <div id="divmsg" runat="server" Visible="false" class="col-md-12">
                            <div class="row">
                                
                           <div class="col-md-10 text-center" >
                              message
                               <asp:TextBox ID="msg" runat="server" CssClass="form-control" TextMode="MultiLine" ReadOnly="true" Text="भू-समाधान पोर्टल पर  #ThanaName थाना  द्वारा  दिनांक  #FromDate से दिनांक #Todate  तक #total मामलों की प्रविष्टि की गयी है, जो कि अपेक्षा से कम है। अतः थानों/अंचलों में प्राप्त  सभी भूमि विवाद संबंधी मामलों की प्रविष्टि करते हुए सभी मामलों को #days दिनों के अन्दर निष्पादन करने हेतु आवश्यक कार्रवाई सुनिश्चित की जाय। - गृह विभाग, बिहार सरकार">
                                   
                               </asp:TextBox>
                         </div>  
                        
                        <div class="col-md-2 text-center" >
                              <br/>
                               <asp:Button ID="btnSentMsg" Style="float: right;" runat="server" class="form-control btn btn-primary"
                            Text="Send Message" onclick="btnSentMsg_Click" />
                         </div>  

                                 <div class="col-md-10 text-center" >

                               <asp:Label ID="lbltotalthana" runat="server" CssClass="form-control" style="font-weight:bold" Text=" Total Police Station : 0">
                                   
                               </asp:Label>
                                 </div>
                                 </div>
                            <%-- <asp:Button ID="btnback" CssClass="btn btn-danger " Text="Back" runat="server" OnClick="btnback_Click"  Visible="false" />--%>
                            </div>
                        <div class="col-md-12">
                            <br />
                            </div>
                    <div class="col-md-12">
                            <asp:Panel ID="pnlgrid" runat="server" ScrollBars="Auto">
                                
                                 <asp:GridView ID="grvAllApplication" runat="server" AutoGenerateColumns="false" CssClass="table-responsive"  
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red" OnRowCommand="grvAllApplication_RowCommand"
                                Width="100%"  ShowHeaderWhenEmpty="true" ShowFooter="true" DataKeyNames="mobile,DIVISIONCODE, DISTRICTCODE, BlockCode, Sd_Code2, PS_Code">    
                                                                                                 
                                <Columns>

                                     <asp:TemplateField HeaderText="Sl. No." ItemStyle-HorizontalAlign="Center"  >
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" Font-Bold="true" Font-Names="Arial Unicode MS"
                                            Font-Size="14px" Width="2%" />
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Black" />   
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Left" Width="4%" />                                  
                                    </asp:TemplateField>

                                     <asp:BoundField DataField="DIVISIONAME" HeaderText="Division">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                             Font-Size="14px" HorizontalAlign="Left" Width="2%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Black"/>   
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Left" Width="4%" />                                      
                                    </asp:BoundField>
                                   
                                     <asp:BoundField DataField="DISTRICTNAME" HeaderText="District" >
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                           Font-Size="14px" HorizontalAlign="Left" Width="3%"/>
                                          <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Black" />   
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Left" Width="4%" />                                      
                                    </asp:BoundField>

                                     <asp:BoundField DataField="Sd_Name_En" HeaderText="Sub Division">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                           Font-Size="14px" HorizontalAlign="Left" Width="2%"/>
                                        <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Black" />   
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Left" Width="4%" />                                   
                                    </asp:BoundField>

                                     <asp:BoundField DataField="BlockName" HeaderText="Circle">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="14px" HorizontalAlign="Left" Width="3%"/>
                                         <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Black" />   
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Left" Width="4%" />                                   
                                    </asp:BoundField>

                                     <asp:BoundField DataField="Police_Station" HeaderText="Police Station">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="14px" HorizontalAlign="Left" Width="3%"/>
                                         <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Black" />   
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Right" Width="4%" />                                         
                                    </asp:BoundField>                                    
                                    
                                     <asp:TemplateField HeaderText="Total Entry" HeaderStyle-Width="8%">
                                         <ItemTemplate>
                                             <asp:LinkButton ID="lnkTotalEntry" runat="server"
                                                 Text='<%#Eval("TotalEntry") %>' CommandName="Entry" ForeColor="Blue"></asp:LinkButton>                                           
                                         </ItemTemplate>
                                         <HeaderStyle BackColor="#1C6794" ForeColor="White" />
                                     </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Finalize" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkFinalize" runat="server"
                                                Text='<%#Eval("Finalize") %>' CommandName="Finalize" ForeColor="Blue"></asp:LinkButton>                                           
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Unfinalize" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkUnfinalize" runat="server"
                                                Text='<%#Eval("Unfinalize") %>' CommandName="UnFinalize" ForeColor="Blue"></asp:LinkButton>                                           
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Total Metting" HeaderStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkTotalMetting" runat="server"
                                                Text='<%#Eval("TotalMetting") %>' CommandName="AllMeeting" ForeColor="Blue"></asp:LinkButton>                                           
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#1C6794" ForeColor="White" />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            </asp:Panel>
                        </div>

                     <div class="col-md-12" style="width: 100%;">
     <asp:Panel ID="Panel1" runat="server" Visible="false" ScrollBars="Auto">
          <asp:GridView ID="gvDisputeDetailsReport" OnRowDataBound="gvDisputeDetailsReport_RowDataBound" runat="server" DataKeyNames="a_id"
        AutoGenerateColumns="False" EnableTheming="False" Width="100%"  PagerStyle-CssClass="pgr"  AlternatingRowStyle-CssClass="alt" CssClass="mGrid" GridLines="None" Style="width: 100%;" HeaderStyle-BackColor="Beige"  
        ShowFooter="True" EmptyDataText="No Record Found" CellPadding="4" ForeColor="#333333">
      <AlternatingRowStyle BackColor="White" CssClass="alt" ForeColor="#284775" />
      <Columns>                  
             <asp:TemplateField HeaderText="Sl. No." ItemStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                   <ItemTemplate>
                       <%#Container.DataItemIndex+1+"." %>
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="Left" />
               </asp:TemplateField>
          <asp:TemplateField HeaderText="Application No." ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="6%">
              <ItemTemplate>
                  <asp:LinkButton ID="lnkApplicationNo" runat="server" CommandArgument='<%#Eval("a_id")%>' Font-Underline="false" ForeColor="Blue"  OnClientClick="openwindow(this);" Text='<%#Eval("ApplicationNo")%>'></asp:LinkButton>
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
                  <div id="div_pulis_padadhikari_vivarani" runat="server" class="divclss" visible='<%# CheckNull(Eval("police_padadhikari_vivarani"))%>'>
                      <%#Eval("police_padadhikari_vivarani")%>
                  </div>
              </ItemTemplate>
              <HeaderStyle Wrap="False" />
              <ItemStyle HorizontalAlign="Left" />
          </asp:TemplateField>
          <asp:TemplateField HeaderStyle-Wrap="false" HeaderText="दस्तावेज" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ItemStyle-Width="5%">
              <ItemTemplate>
                  <asp:ImageButton ID="Image2" runat="server" Height="50px" ImageUrl="~/images/pdf.gif" path='<%# Eval("police_padadhikar_Patr_file")%>' Style="cursor: pointer" Visible='<%# CheckImage(Eval("police_padadhikar_Patr_file"))%>' Width="50px" />
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

 <%--    <asp:LinkButton ID="btnShow" runat="server" Text="" Style="width: 0px; height: 0px;" />
 <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panl1" TargetControlID="btnShow"
     CancelControlID="Button" BackgroundCssClass="Background">
 </asp:ModalPopupExtender>
 <asp:Panel ID="Panl1" runat="server" CssClass="Popup" align="center" Style="display: none; " ScrollBars="Auto">
     <div class="container">
         <div class="row">
             <div class="col-md-12 button-container" align="right">
                 <asp:Button ID="Button" runat="server" CssClass="btn btn-danger" Text="X" align="right" />
             
             </div>   --%>
           
            
       <%-- </div>
    </div>
</asp:Panel>--%>
<br />

</asp:Content>