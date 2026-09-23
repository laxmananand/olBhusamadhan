<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User_Credential.aspx.cs" Inherits="User_Credential" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="container-fluid">
        <div class="row mt-3">
 
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <h4 class="text-center text-dark" style="font-size:20px;"><b>User Credential</b></h4>
                        <div class="row mb-2">
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-2">
                                <asp:Label ID="Label1" runat="server" Text="Commissionary"></asp:Label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label3" runat="server" Text="Sub - Division"></asp:Label>
                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
   
                            <div class="col-md-2">
                                <asp:Label ID="Label4" runat="server" Text="Circle"></asp:Label>
                                <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label5" runat="server" Text="Police Station"></asp:Label>
                                <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:Label ID="Label6" runat="server" Text="Role"></asp:Label>
                                <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div> 
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-4">
                                <center><asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-outline-primary"/></center>                  
                            </div>
                            <div class="col-md-4">
                                <a id="#" class="btn btn-outline-danger" href="#" style="float:right">Print</a>
                            </div>
                        </div>   
                        </div>         
                </div>
            </div>          
        </div>
    </div>

    <br />
</asp:Content>

