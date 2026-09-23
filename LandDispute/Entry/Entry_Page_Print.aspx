<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Entry_Page_Print.aspx.cs" Inherits="LandDispute_Entry_Entry_Page_Print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
       <a href="Entry_Page.aspx">Back</a>
         <asp:Panel ID="pnlPrintForm" runat="server" CssClass="printdraft" HorizontalAlign="Center">
                <table width="960px" style="text-align: left; top: 0; z-index: 1000;" align="center">
                        <tr>
                            <td align="center" style="font-weight: bold">
                                भू-समाधान
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="font-weight: bold">
                                गृह विभाग, बिहार सरकार
                            </td>
                        </tr>
               </table>
         </asp:Panel>  
           <table width="960px" cellspacing="4" cellpadding="4" border="0" style="text-align: left;
            top: 0; z-index: 1000;" align="center">
                     <%--आवेदन विवरण--%>
                     <tr>
                          <td colspan="2" style="width: 100%">
                              <asp:Panel ID="Panel1" GroupingText="आवेदन विवरण" runat="server" Font-Bold="False">
                                   <table width="100%">
                                         <tr>
                                                <td align="left" valign="top" height="20px" class="style5">
                                                    <asp:Label ID="Label1" runat="server" Text="आवेदन संख्या : "></asp:Label>
                                                    &nbsp;
                                                    <asp:Label ID="lblAppID" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                                 <td align="right" rowspan="1" valign="top" height="20px">
                                                    आवेदन तिथि :
                                                    <asp:Label ID="lblAppDate" runat="server" Font-Bold="True"></asp:Label>
                                                </td>
                                         </tr>
                                   </table>
                              </asp:Panel>
                          </td>
                     </tr>
                     <tr>
                          <td colspan="2">
                              <asp:Panel ID="pnlWadi" GroupingText="वादी का विवरण" runat="server" Font-Bold="False" Font-Size="Small">
                                     <asp:GridView ID="gvWadi" runat="server" AutoGenerateColumns="false" EmptyDataText="No Record Found!"
                                CellPadding="4" Width="100%" CssClass="grid">
                                <HeaderStyle VerticalAlign="Middle" />
                                <RowStyle VerticalAlign="Top" Height="25px" />
                                <FooterStyle BackColor="ButtonFace" BorderColor="ButtonFace" />
                                <Columns>
                                        <asp:TemplateField HeaderText="क्र० सं०">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1+"." %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="वादी का नाम">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWName" runat="server" Text='<%#Eval("vadi_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="संस्था का प्रतिनिधि">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_is_org_pratinidhi" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विभाग का प्रतिनिधि">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_is_dep_pratinidhi" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_dept")) == "Y" ) ? "हां" : "नहीं"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="लिंग">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWgender" runat="server" Text='<%# (Convert.ToString(Eval("SexAsPerAadhaar")) == "F" ) ? "Female" : "Male"  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="जन्म वर्ष">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWage" runat="server" Text='<%#Eval("YearOfBirthAsPerAadhaar") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="पिता/ पति का नाम">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWHHname" runat="server" Text='<%#Eval("Vadi_Father_Husband_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विभाग/संस्था का नाम">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_dep_org_name" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y" ) ? Eval("vadi_org_name") : Eval("org_type")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विभाग/संस्था में पदनाम">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_dep_org_pdname" runat="server" Text='<%# (Convert.ToString(Eval("is_vadi_from_an_org")) == "Y") ? Eval("vadi_org_pad_name") : Eval("vadi_dept_pad_name")   %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="जिला">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_jila" runat="server" Text='<%#Eval("dist") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="अनुमंडल">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_anumandal" runat="server" Text='<%#Eval("sub_division") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="अंचल">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_anchal" runat="server" Text='<%#Eval("block") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="थाना">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_thana" runat="server" Text='<%#Eval("thana") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="क्षेत्र का प्रकार">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_chetra_prakar" runat="server" Text='<%#Eval("area_type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ग्राम पंचायत">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_panchayat" runat="server" Text='<%#Eval("panchayt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="राजस्व ग्राम">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_rajsv_gram" runat="server" Text='<%#Eval("village") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="वार्ड">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_ward" runat="server" Text='<%#Eval("WardNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="मोबाइल संख्या">
                                            <ItemTemplate>
                                                <asp:Label ID="lblW_mobile" runat="server" Text='<%#Eval("Vadi_MobileNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                              </asp:Panel>
                          </td>
                     </tr>
           </table>   
    </form>
</body>
</html>
