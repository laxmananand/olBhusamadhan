<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AllApplication.aspx.cs" 
    Inherits="LandDispute_THANA_Thana_Entry"%>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function fnLinkbutton(objlinkbutton) {
    debugger
        //Access the link button here
        var imgDiv = document.getElementById("divImage");
        var inlineFrameExample = document.getElementById("inlineFrameExample");
        var lb1 = document.getElementById(objlinkbutton).getAttribute("path"); ;
        var bookingID = lb1;
        alert(lb1);
        //  var a = "/LDHOME/LandDispute/uploads/LD212095111/LD212124065/fuIdDocumentLD212124065.pdf";
           var a = lb1.substring(1)         
       // inlineFrameExample.src = '<%=ResolveUrl("' + a + '")%>';
        //inlineFrameExample.src = "" + a;
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
        debugger
        //Access the link button here
        var imgDiv = document.getElementById("divImage");
        var inlineFrameExample = document.getElementById("inlineFrameExample");
        var lb1 = document.getElementById(objlinkbutton).getAttribute("path"); ;
        var bookingID = lb1;
       // alert(lb1);
        //  var a = "/LDHOME/LandDispute/uploads/LD212095111/LD212124065/fuIdDocumentLD212124065.pdf";
        var a = lb1.substring(1)
        // inlineFrameExample.src = '<%=ResolveUrl("' + a + '")%>';
        //inlineFrameExample.src = "/LDHOME" + a;
        var base_Path = "/"+ window.location.pathname.split('/')[1];
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
                                             AutoPostBack="True" height="30px">
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
                    <div class="col-md-12">
                            <asp:Panel ID="pnlgrid" runat="server" ScrollBars="Auto">
                                 <asp:GridView ID="grvAllApplication" runat="server" AutoGenerateColumns="false" CssClass="table-responsive"  
                                HeaderStyle-BorderColor="White" EmptyDataText="No Record(s) found" EmptyDataRowStyle-ForeColor="Red"
                                Width="100%"  ShowHeaderWhenEmpty="true" ShowFooter="true">    
                                                                                                 
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
                                    
                                     <asp:BoundField DataField="TotalEntry" HeaderText="Total Entry">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="14px" HorizontalAlign="Left" Width="3%"/>
                                       <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Black" />   
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Left" Width="4%" />                                        
                                    </asp:BoundField>                                    

                                     <asp:BoundField DataField="Finalize" HeaderText="Finalize">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="14px" HorizontalAlign="Left" Width="3%"/>
                                         <ItemStyle Font-Size="Medium" HorizontalAlign="Left" ForeColor="Black" />   
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Left" Width="4%" />                               
                                    </asp:BoundField>

                                    <asp:BoundField DataField="Unfinalize" HeaderText="Unfinalize">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Left" Width="3%"/>
                                         <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="12px" ForeColor="White"
                                                                HorizontalAlign="Left" />                                      
                                    </asp:BoundField>

                                     <asp:BoundField DataField="TotalMetting" HeaderText="Total Metting">
                                        <HeaderStyle BackColor="#1C6794" Font-Bold="True" ForeColor="White" Font-Names="Arial Unicode MS"
                                            Font-Size="Small" HorizontalAlign="Left" Width="3%"/>
                                         <ItemStyle Font-Size="Medium" HorizontalAlign="Left" Width="4%" ForeColor="Black"  />
                                        <FooterStyle BackColor="#1C6794" Font-Bold="True" Font-Size="15px" ForeColor="White"
                                                                HorizontalAlign="Left" />                                   
                                    </asp:BoundField>
                                </Columns>
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
</asp:Content>
