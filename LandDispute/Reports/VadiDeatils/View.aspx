<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="LandDispute_Dashboard_View"
     EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
 <%-- <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet"/>--%>
  <%--<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js"/></script>--%>

    <style type="text/css" media="screen">
        .print
        {
            background-color: #fff;
        }
        .printdraft img
        {
            visibility: hidden;
        }
    </style>
    <style type="text/css" media="print">
        .printdraft img
        {
            visibility: visible;
        }
    </style>
    <style type="text/css" media="all">
        .printfinal img
        {
            visibility: hidden;
        }
    </style>
    <style type="text/css" media="print">
        .pageBreak
        {
            page-break-before: always;
        }
    </style>
    <style type="text/css">
        .grid td, th
        {
            padding: 4px;
        }
        
        
        
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        .style1
        {
            width: 30%;
        }
        .style4
        {
            width: 29%;
        }
        .style5
        {
            width: 28%;
        }
        .style7
        {
            width: 15%;
        }
    </style>
    <title></title>

    
</head>
<body>
    <form id="form1" runat="server">  
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">  
        </asp:ScriptManager> 
        

        
       <%-- <asp:UpdatePanel ID="udpOrderDescription" runat="server">  
            <ContentTemplate>  --%>
                
              
        <table>

            <tr>
                <td style="width:15%"></td>
                <td style="width:70%">
                    <%--<asp:Button ID="btnPrint" CssClass="btn btn-success"   runat="server"
                                                Text="Print" OnClientClick="return PrintDiv();"/>--%>
                    <br />

                    <div  id="printdiv" runat="server" >
                        <asp:HiddenField ID="lblVillage" runat="server" value=''></asp:HiddenField>
                        <asp:HiddenField ID="lblform" runat="server" value=''></asp:HiddenField>
                        <asp:HiddenField ID="lblTo" runat="server" value=''></asp:HiddenField>
                <asp:Repeater ID="parentRepeater" runat="server" OnItemDataBound="parentRepeater_ItemDataBound">  
                    <HeaderTemplate>                                                                  
                                                         
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        
                            
                         <asp:Panel ID="pnlPrintForm" runat="server" CssClass="printdraft" HorizontalAlign="Center"  style="border-width:2px;border-style:solid;border-color:black;padding:10px 10px 10px 10px">
                             <table width="100%" style="text-align: left; top: 0; z-index: 1000;" align="center">
                                <tr>
                                    <td align="center" style="font-weight: bold">भू-समाधान</td>
                                </tr>
                                <tr>
                                    <td align="center" style="font-weight: bold">गृह विभाग, बिहार सरकार</td>
                                </tr>
                            </table>
                             <table width="100%" cellspacing="4" cellpadding="4" border="0" style="text-align: left;top: 0; z-index: 1000;" align="center">
                    
            <%--आवेदन विवरण--%>
            <tr>
                <td style="width: 100%">
                    <asp:Panel ID="Panel1" GroupingText="आवेदन विवरण" runat="server" Font-Bold="False">
                        <table width="100%">
                            <tr>
                                <td align="left" valign="top" height="20px" class="style5">
                                    <asp:Label ID="Label1" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="lblAppID" runat="server" Font-Bold="True" Text='<%#Eval("ApplicationNo") %>'></asp:Label>
                                     <asp:HiddenField ID="lbla_id" runat="server" value='<%#Eval("a_id") %>'></asp:HiddenField>
                                </td>
                                <td align="right" rowspan="1" valign="top" height="20px">
                                    आवेदन तिथि :
                                    <asp:Label ID="lblAppDate" runat="server" Font-Bold="True" Text='<%#Eval("AavedanKiTithi") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <%--वादी का विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="pnlWadi" GroupingText="वादी का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <asp:GridView ID="gvWadi" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"
                                CellPadding="4" Width="100%" CssClass="grid">
                                <HeaderStyle VerticalAlign="Middle" />
                                <RowStyle VerticalAlign="Top" Height="25px" />
                                <FooterStyle BackColor="ButtonFace" BorderColor="ButtonFace" />
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
                    </asp:Panel>
                </td>
            </tr>
            <%--भूमि विवाद का विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel2" GroupingText="भूमि विवाद का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <table width="100%">
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    जिला :
                                </td>
                                <td valign="top"  style="width: 25%">
                                    <asp:Label ID="lblDistrict" runat="server" Text=""></asp:Label>
                                </td>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   अनुमंडल :
                                </td>
                                <td valign="top"  style="width: 25%">
                                   <asp:Label ID="lblSubdivision" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    अंचल : 
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <asp:Label ID="lblBlock" runat="server"></asp:Label>
                                </td>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   थाना :
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <asp:Label ID="lblPolice_Station" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    क्षेत्र का प्रकार :
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <asp:Label ID="lblAreaType" runat="server"></asp:Label>
                                </td>
                                <td></td>
                                  <td></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                     <asp:Label ID="lblVillage" runat="server" ></asp:Label>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <asp:Label ID="lblPanchayatName" runat="server"></asp:Label>
                                </td>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   <div class="col-md-3" id="div_Vadi_Svarajaya_Label" runat="server"   style="font-weight: bold;font-size:14px">
                                                                          राजस्व ग्राम
                                   </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                   <div class="col-md-3" id="div_Vadi_Svarajaya" runat="server" style="font-size:12px">
                                                                             <asp:Label ID="lblVILLNAME" runat="server"></asp:Label>
                                                                      </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    वार्ड 
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <asp:Label ID="lblWARDNAME" runat="server"></asp:Label>
                                </td>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   विवाद का अद्यतन कारक
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div  id="div_vadi_Vivad_Ka_Vighatan" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblvadi_Vivad_Ka_Vighatan" runat="server"></asp:Label>
                                                                      </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    राजस्व थाना संख्या
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div  id="div_vadi_rajashv_sankhaya" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblvadi_rajashv_sankhaya" runat="server"></asp:Label>
                                                                      </div>
                                </td>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                  भूमि का प्रकार
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div  id="div_Vadi_BhumiKaPrakar" runat="server" style="font-size:12px">
                                                                           <asp:Label ID="lblVadi_BhumiKaPrakar" runat="server"></asp:Label>
                                                                      </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    <div id="div_vadi_sarkari_bhumi_ka_prakar_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                          सरकारी भूमि का प्रकार
                                                                      </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                   <div  id="div_Preview_vadi_sarkari_bhumi_ka_prakar" runat="server" style="font-size:12px">
                                                                          <asp:Label ID="lblvadi_sarkari_bhumi_ka_prakar_Label" runat="server"></asp:Label>
                                                                     </div> 
                                </td>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   <div  id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                          सरकारी भूमि का प्रकार (अगर अन्य है)
                                                                      </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                     <div  id="div_vadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server" style="font-size:12px">
                                                                          <asp:Label ID="lblvadi_Sarkari_bhumi_ka_Prakar_ager_anya" runat="server"></asp:Label>
                                                                     </div> 
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                भूमि विवाद का प्रकार
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div  id="div_BhumiKa_VivadPrakar" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblBhumiKa_VivadPrakar" runat="server"></asp:Label>
                                                                       </div>
                                </td>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    <div  id="div_Preview_vadi_Bhumivivad_Prakar_Anaya_Label" runat="server" style="font-weight: bold;font-size:14px">
                                                                             <asp:Label ID="Label2" runat="server" Text="भूमि विवाद का प्रकार (अगर अन्य है) :"></asp:Label>
                                                                      </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                   <div  id="div_Preview_vadi_Bhumivivad_Prakar_Anaya" runat="server" style="font-size:12px">
                                                                         <asp:Label ID="lblvadi_Bhumivivad_Prakar_Anaya" runat="server"></asp:Label>
                                                                    </div> 
                                </td>
                            </tr>
                            <tr>
                                
                                <td valign="top" height="20px" style="width: 25%;font-weight:bold">वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी </td>
                                <td valign="top" height="20px" style="width: 75%" colspan="3">
                                    <div  id="divVadiKabhumiVivaran" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblVadiKabhumiVivaran" runat="server"></asp:Label>
                                                                      </div> 
                                </td>
                                
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी
                                </td>
                                <td valign="top" height="20px" style="width: 75%" colspan="3">
                                    <div  id="divPrativadiKabhumiVivaran" runat="server" style="font-size:12px">
                                                                            <asp:Label ID="lblPrativadiKabhumiVivaran" runat="server"></asp:Label>
                                                                       </div>
                                </td>
                                
                            </tr>
                            
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <%--प्रतिवादी का विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel3" GroupingText="प्रतिवादी का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <asp:GridView ID="pratiWadi_grid" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"
                                CellPadding="4" Width="100%" CssClass="grid">
                                <HeaderStyle VerticalAlign="Middle" />
                                <RowStyle VerticalAlign="Top" Height="25px" />
                                <FooterStyle BackColor="ButtonFace" BorderColor="ButtonFace" />
                                <Columns>                                                                                       
                                                                                            <asp:TemplateField HeaderText="Sl. No.">
                                                                                                                    <ItemTemplate>
                                                                                                                        <%# Container.DataItemIndex + 1 %>
                                                                                                                    </ItemTemplate>
                                                                                                                    <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                            </asp:TemplateField>
                                                                                              <asp:BoundField DataField="pratiVadi_Name" HeaderText="प्रतिवादी" ItemStyle-Width="100" />
                                                                                            <asp:BoundField DataField="pratiVadi_Father_Husband_Name" HeaderText="पिता/ पति का नाम" ItemStyle-Width="120" />                                                                                                                                                                        
                                                                                            <asp:BoundField DataField="dist" HeaderText="जिला" ItemStyle-Width="80" />
                                                                                            <asp:BoundField DataField="sub_division" HeaderText="अनुमंडल" ItemStyle-Width="100" />
                                                                                            <asp:BoundField DataField="block" HeaderText="अंचल" ItemStyle-Width="100" />
                                                                                            <asp:BoundField DataField="thana" HeaderText="थाना" ItemStyle-Width="100" />
                                                                                            <asp:BoundField DataField="area_type" HeaderText="क्षेत्र का प्रकार" ItemStyle-Width="100" />
                                                                                            <asp:BoundField DataField="panchayt" HeaderText="ग्राम पंचायत" ItemStyle-Width="100" />
                                                                                             <asp:BoundField DataField="village" HeaderText="राजस्व ग्राम" ItemStyle-Width="100" />
                                                                                            <asp:BoundField DataField="WardNo" HeaderText="वार्ड" ItemStyle-Width="80" />
                                                                                            <asp:BoundField DataField="pratiVadi_MobileNo" HeaderText="मोबाइल संख्या" ItemStyle-Width="80" />
                                                                                                <asp:TemplateField HeaderText="संस्था का प्रतिनिधि" ItemStyle-Width="80">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblis_pratiVadi_from_an_org" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y") ? "हां" : "नहीं"   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <asp:TemplateField HeaderText="विभाग का प्रतिनिधि" ItemStyle-Width="100">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblis_pratiVadi_from_an_dept" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_dept")) == "Y") ? "हां": "नहीं"   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                                                                                                 
                                                                                             <asp:TemplateField HeaderText="विभाग/संस्था का नाम" ItemStyle-Width="100">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblOrgName"  runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y" ) ? Eval("pratiVadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                             <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम" ItemStyle-Width="100">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblPadName" runat="server" Text='<%# (Convert.ToString(Eval("is_pratiVadi_from_an_org")) == "Y") ? Eval("pratiVadi_org_pad_name") : Eval("pratiVadi_dept_pad_name")   %>'></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>                                                                                                             
                                                                                 </Columns>
                            </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <%--प्रतिवादी संबन्धित अन्य विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel4" GroupingText="प्रतिवादी संबन्धित अन्य विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <table width="100%">
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   <div  id="divprativadi_ka_suchit_Label" runat="server" >
                                                                                       प्रतिवादी को सूचित किया गया है या नहीं ?
                                                                                 </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div class="col-md-3" id="divprativadi_ka_suchit" runat="server" >
                                                                                        <asp:Label ID="lblprativadi_ka_suchit" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%;font-weight:bold">
                                     <div class="col-md-3" id="divprativadi_ka_Karan_Label" runat="server" >
                                                                                       कारण स्पष्ट करें
                                                                                 </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div class="col-md-3" id="divprativadi_ka_Karan" runat="server" >
                                                                                        <asp:Label ID="lblprativadi_ka_Karan" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" height="20px" style="width: 25%;font-weight:bold">
                                    <div class="col-md-3" id="divprativadi_ka_madham_Label" runat="server" >
                                                                                       माध्यम
                                                                                 </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div class="col-md-3" id="divprativadi_ka_madham" runat="server" >
                                                                                        <asp:Label ID="lblprativadi_ka_madham" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                </td>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    <div class="col-md-3" id="divprativadi_ka_Suchna_Label" runat="server" >
                                                                                       प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?
                                                                                 </div>
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div class="col-md-3" id="divprativadi_ka_Suchna" runat="server" >
                                                                                     <asp:Label ID="lblprativadi_ka_Suchna" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" height="20px" style="width: 25%;font-weight:bold"><div  id="divprativadi_ka_Upashtith_Label" runat="server" >
                                                                                       प्रतिवादी उपस्थित हुआ है या नहीं ? 
                                                                                 </div></td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <div class="col-md-3" id="divprativadi_ka_Upashtith" runat="server" >
                                                                                     <asp:Label ID="lblprativadi_ka_Upashtith" runat="server" Text=""></asp:Label>
                                                                                 </div>
                                </td>
                               <td></td>
                                <td></td>
                            </tr>
                           
                            
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <%--भूमि का खाता खेसरा का विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel5" GroupingText="भूमि का खाता खेसरा का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <asp:GridView ID="grd_bhumivivad" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"
                                CellPadding="4" Width="100%" CssClass="grid">
                            <HeaderStyle VerticalAlign="Middle" />
                            <RowStyle VerticalAlign="Top" Height="25px" />
                            <FooterStyle BackColor="ButtonFace" BorderColor="ButtonFace" />
                            <Columns>                                                                         
                                                                                        <asp:TemplateField HeaderText="Sl. No.">
                                                                                                                <ItemTemplate>
                                                                                                                    <%# Container.DataItemIndex + 1 %>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                                            </asp:TemplateField>
                                                                                        <asp:BoundField DataField="khataNo" HeaderText="खाता संख्या" ItemStyle-Width="80" />
                                                                                        <asp:BoundField DataField="khesraNo" HeaderText="खेसरा संख्या" ItemStyle-Width="100" />
                                                                                        <asp:BoundField DataField="Rakba" HeaderText="रकबा" ItemStyle-Width="100" />
                                                                                        <asp:BoundField DataField="LandTypesInKhatianDesc" HeaderText="जमीन की किस्म" ItemStyle-Width="120" />                                        
                                                                                        <%--<asp:BoundField DataField="जमीन का विवरण" HeaderText="ख़तियन में जमीन का विवरण" ItemStyle-Width="220" />--%>
                                                                                        
                                                                                <asp:BoundField DataField="LandDetailsInKhatian" HeaderText="ख़तियन में जमीन का विवरण" ItemStyle-Width="220" /> 
                                                                                         <asp:BoundField DataField="North_chauhaddee" HeaderText="उत्तर" ItemStyle-Width="100" />
                                                                                         <asp:BoundField DataField="South_chauhaddee" HeaderText="दक्षिण" ItemStyle-Width="100" />
                                                                                         <asp:BoundField DataField="East_chauhaddee" HeaderText="पूर्व" ItemStyle-Width="100" />
                                                                                         <asp:BoundField DataField="West_chauhaddee" HeaderText="पश्चिम" ItemStyle-Width="100" />                                                                                                                
                                                                                    </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <%--वादी द्वारा प्रस्तुत साक्ष्य की विवरणी--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel6" GroupingText="वादी द्वारा प्रस्तुत साक्ष्य की विवरणी" runat="server" Font-Bold="False" Font-Size="Small">
                        <asp:GridView ID="gdVadiEvidence" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"
                                CellPadding="4" Width="100%" CssClass="grid">
                            <HeaderStyle VerticalAlign="Middle" />
                            <RowStyle VerticalAlign="Top" Height="25px" />
                            <FooterStyle BackColor="ButtonFace" BorderColor="ButtonFace" />
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
                    </asp:Panel>
                </td>
            </tr>
            <%--प्रतिवादी द्वारा प्रस्तुत साक्ष्य की विवरणी--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel7" GroupingText="प्रतिवादी द्वारा प्रस्तुत साक्ष्य की विवरणी" runat="server" Font-Bold="False" Font-Size="Small">
                        <asp:GridView ID="gdPrativadiEvidence" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"
                                CellPadding="4" Width="100%" CssClass="grid">
                            <HeaderStyle VerticalAlign="Middle" />
                            <RowStyle VerticalAlign="Top" Height="25px" />
                            <FooterStyle BackColor="ButtonFace" BorderColor="ButtonFace" />
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
                                                                                                    <asp:ImageButton ID="Image1"  path='<%#Eval("FullfileName")%>' runat="server" ImageUrl="images/pdf.gif" Width="50px" Height="50px" Style="cursor: pointer" />                                 
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:TemplateField>                                                                                                                                                                                                                                                                                                                  
                                                                                            </Columns>
                        </asp:GridView>
                       
                    </asp:Panel>
                </td>
            </tr>
           <%--प्रस्तुत साक्ष्य की विवरणी--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel8" GroupingText="राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <table width="100%">
                            <tr>
                                <td valign="top" style="width: 40%;font-weight:bold">
                                    पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी 
                                </td>
                                <td valign="top" height="20px">
                                   <div  id="divPoliceAdhikari" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblPoliceAdhikari" runat="server"></asp:Label>
                                                                                             </div>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 40%;font-weight:bold">
                                     हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी
                                </td>
                                <td valign="top" height="20px">
                                    <div  id="divHalkaKarmchariValue" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblHalkaKarmchariValue" runat="server"></asp:Label>
                                                                                             </div>  
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 40%;font-weight:bold">
                                     विवादित भू-खंड की मापी 
                                </td>
                                <td valign="top" height="20px">
                                    <div id="divVivaditBhukandValue" runat="server" style="font-size:12px">
                                                                                                     <asp:Label ID="lblVivaditBhukandValue" runat="server"></asp:Label>
                                                                                             </div> 
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 100%" colspan="2">
                                    <div class="row" id="divMapi" runat="server">                                                                             
                                                                                             <div  id="div8" runat="server" style="font-weight: bold;width:20%; float:left">
                                                                                                  मापी ? 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divMapiValue" runat="server" style="width:80%">
                                                                                                  <asp:Label ID="lblMapiValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div>
                                </td>
                                
                            </tr>
                            <tr>
                                <td valign="top" style="width: 100%" colspan="2">
                                    <div class="row" id="divVivaditBhukandKaMapi" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div10" runat="server" style="font-weight: bold;width:20%;float:left">
                                                                                                  विवादित भू-खंड की मापी नहीं होने का कारण 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divVivaditBhukandKaMapiValue" runat="server" style="width:80%">
                                                                                                 <asp:Label ID="lblVivaditBhukandKaMapiValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                                </td>
                                
                            </tr>
                            <tr>
                                <td valign="top" style="width: 100%" colspan="2">
                                    <div class="row" id="MapiKeNirdharnKiThithi" runat="server">                                                                             
                                                                                             <div class="col-md-3" id="div5" runat="server" style="font-weight: bold;width:20%;float:left">
                                                                                                  मापी के लिए निर्धारित तिथि 
                                                                                             </div>
                                                                                             <div class="col-md-9" id="divMapiKeNirdharnKiThithiValue" runat="server" style="width:80%">
                                                                                                 <asp:Label ID="lblMapiKeNirdharnKiThithiValue" runat="server"></asp:Label>
                                                                                             </div>                                                                                                                                                                                                                                            
                                                                                   </div> 
                                </td>
                                
                            </tr>
                           
                        </table>
                    </asp:Panel>
                </td>
            </tr>
           <%--भूमि विवाद से संबंधित घटना/ वारदात का विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel9" GroupingText="भूमि विवाद सें संबंधित घटना/ वारदात का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <table width="100%">
                            <tr>
                                <td valign="top" style="width: 25%">
                                    प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ? :
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <asp:Label ID="lblPrathamik" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
           <%--विवाद सें संबंधित घटना/ वारदात का विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="pnlBhumiVivadGhatna" GroupingText="विवाद सें संबंधित घटना/ वारदात का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <asp:GridView ID="grdbhumivivad" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"
                                CellPadding="4" Width="100%" CssClass="grid">
                            <HeaderStyle VerticalAlign="Middle" />
                            <RowStyle VerticalAlign="Top" Height="25px" />
                            <FooterStyle BackColor="ButtonFace" BorderColor="ButtonFace" />
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
                    </asp:Panel>
                </td>
            </tr>
            <%--न्यायालय में प्रक्रियाधीन वाद का विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel11" GroupingText="न्यायालय में प्रक्रियाधीन वाद का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <table width="100%">
                            <tr>
                                <td valign="top" style="width: 25%">
                                    प्रक्रियाधीन वाद का विवरण उपलब्ध है ? :
                                </td>
                                <td valign="top" height="20px" style="width: 25%">
                                    <asp:Label ID="lblPrakiriyaVad" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <%--न्यायालय में दायर वाद का विवरण--%>
            <tr>
                <td>
                    <asp:Panel ID="pnlnyayalayvad" GroupingText="न्यायालय में दायर वाद का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <asp:GridView ID="grdnyayalay_vivran" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"
                                CellPadding="4" Width="100%" CssClass="grid">
                            <HeaderStyle VerticalAlign="Middle" />
                            <RowStyle VerticalAlign="Top" Height="25px" />
                            <FooterStyle BackColor="ButtonFace" BorderColor="ButtonFace" />
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
                    </asp:Panel>
                </td>
            </tr>
  <%--अंचलाधिकरी एवम्‌ थाना अध्यक्ष द्वारा भूमि विवाद क़े निराकरण हेतु कृत करवाई की विवरणी--%>
            <tr>
                <td>
                    <asp:Panel ID="Panel13" GroupingText="अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                        <table width="100%">
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    विवाद की संवेदनशीलता :
                                </td>
                                <td valign="top" height="20px" style="width: 75%">
                                    <asp:Label id="lblVivaadKiSanvedanasheelata" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    बैठक की तिथि :
                                </td>
                                <td valign="top" height="20px" style="width: 75%">
                                    <asp:Label id="lblBaithakKiTithi" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    क्या वादी उपस्थित है ? :
                                </td>
                                <td valign="top" height="20px" style="width: 75%">
                                    <asp:Label id="lblkyaVaadeeUpasthitHai" runat="server"  Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                     क्या प्रतिवादी उपस्थित है ?
                                </td>
                                <td valign="top" height="20px" style="width: 75%">
                                    <asp:Label id="lblKyaPrativaadeeUpasthitHai" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    बैठक का निष्कर्ष(Action) :
                                </td>
                                <td valign="top" height="20px" style="width: 75%">
                                    <asp:Label id="lblBaithakKaNishkarsh" runat="server"  Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%" colspan="2">
                                    <div  id="divtithi" runat="server" >
                                                                                          <div style="font-weight: bold;width:25%;float:left">
                                                                                                    <asp:Label id="lbltithi" runat="server" Text=""></asp:Label>
                                                                                           </div>
                                                                                          <div   style="font-size:12px;width:75%">
                                                                                               <asp:Label id="lbltithivalue" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div>
                                </td>
                                
                            </tr>
                            <tr>
                                <td valign="top" style="width: 100%" colspan="2">
                                    <div  id="divAsveekrtiKaKaaranLabel" runat="server">
                                                                                          <div   style="font-weight: bold;width:25%;float:left">
                                                                                                    अस्वीकृति का कारण
                                                                                           </div>
                                                                                          <div  id="divAsveekrtiKaKaaran" runat="server" style="font-size:12px;width:75%">
                                                                                               <asp:Label id="lblAsveekrtiKaKaaran" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div> 
                                </td>
                                
                            </tr>
                            
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   बैठक में लिया गया निर्णय
                                </td>
                                <td valign="top" height="20px" style="width: 75%">
                                    <asp:Label  id="lblBaithakMeinLiyaGayaNirnay" runat="server"  Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                    अंचलाधिकारी का मंतव्य 
                                </td>
                                <td valign="top" height="20px" style="width: 75%" >
                                    <asp:Label id="lblAnchalaadhikaareeKaMantavy" runat="server"  Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 25%;font-weight:bold">
                                   थानाध्यक्ष का मंतव्य 
                                </td>
                                <td valign="top"  style="width: 75%" >
                                    <asp:Label id="lblThaanaadhyakshKaMantavy" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                           
                        </table>
                    </asp:Panel>
                </td>
            </tr>
                                 </table>
                             </asp:Panel>
                            
                            
                    </ItemTemplate>  
                  
                </asp:Repeater>
                           </div>

                </td>
                <td style="width:15%"></td>
            </tr>
        </table>
                      
  
                           
          <%--  </ContentTemplate>  
        </asp:UpdatePanel> --%> 
    
    </form>
</body>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function PrintDiv() {    
            var divContents = document.getElementById("printdiv").innerHTML;
            var printWindow = window.open('', '', 'height=800,width=1500');
            printWindow.document.write('<html><head><title>Details</title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');           
            printWindow.document.close(); printWindow.print(); 
        }
       
      
        setTimeout(() => {
            PrintDiv();
           // window.open("DisplayData.aspx?Village=" + document.getElementById('lblVillage').value + "&FromDate=" + document.getElementById('lblform').value + "&ToDate=" + document.getElementById('lblTo').value);
            //window.stop();

        }, 1000)

    </script>
</html>
