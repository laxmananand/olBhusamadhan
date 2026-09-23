<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="LandDispute_Dashboard_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server" >
    </asp:ScriptManager>
    <div class="container-fluid">
         

        
        <div class="row">
                    <center>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
                    </center>
                </div> 
        <div class="card">
                                          
                                             <div class="card-body">
                                                      <asp:UpdatePanel runat="server" ID="pnlupdate1" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                             <div class="row mb-2" style="height:50px !important">
                                                                    
                                                                    <div class="col-md-2" >
                                                                            <asp:Label ID="Label1" runat="server" Text="जिला"></asp:Label>
                                                                            <asp:DropDownList ID="ddDistrict" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                            
                                                                    </div>  
                                                                <div class="col-md-2">
                                                                            <asp:Label ID="Label2" runat="server" Text="अंचल"></asp:Label>
                                                                            <asp:DropDownList ID="ddBlock" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                            
                                                                    </div>  
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label3" runat="server" Text="पंचायत"></asp:Label>
                                                                            <asp:DropDownList ID="ddPanchayat" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                            
                                                                    </div>  
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label4" runat="server" Text="Police Station"></asp:Label>
                                                                            <asp:DropDownList ID="ddlPoliceSation" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                            
                                                                    </div>  
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label5" runat="server" Text="सवेदनशीलता"></asp:Label>
                                                                            <asp:DropDownList ID="ddSensivity" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             AutoPostBack="True">
                                                                            </asp:DropDownList>                                                                            
                                                                    </div>  
                                                                 <div class="col-md-2">
                                                                            <asp:Label ID="Label6" runat="server" Text="बैठक का निष्कर्ष(Action) "></asp:Label>
                                                                            <asp:DropDownList ID="ddlaction" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             AutoPostBack="True">
                                                                                <asp:ListItem Value="0">--All--</asp:ListItem>
                                                                                <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                                                                                <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                                                                                <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                                                                                <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                                                                                <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                                                                            </asp:DropDownList>                                                                            
                                                                    </div>  
                                                             </div>
                                                           
                                                                

                                                                

                                                                
                                                             </ContentTemplate>                                      
                                                 </asp:UpdatePanel>
                                                     
                                                                                           
                                                                                         
                                             </div>
                                      </div>
        </div>
</asp:Content>
