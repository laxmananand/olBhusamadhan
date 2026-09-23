<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add_Meeting.aspx.cs" Inherits="Police_Station_Add_Meeting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../vendors/font-awesome.min.css" rel="stylesheet" />
    <link href="vendors/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/ruang-admin.min.css" rel="stylesheet" />
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <script src="../vendor/jquery/jquery.min.js"></script>
    <script src="../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../vendor/chart.js/Chart.min.js"></script>
    <script src="../js/demo/chart-area-demo.js"></script>
    <script src="../vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <script src="../js/ruang-admin.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <h4 class="text-center text-black">आवेदन का विवरण</h4>
        <div class="card">
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label1" runat="server" Text="Commissionary"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Munger" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Begusarai" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label3" runat="server" Text="Sub-Division"></asp:Label>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Begusarai" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label4" runat="server" Text="Circle"></asp:Label>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Begusarai" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                 <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label5" runat="server" Text="Police Station"></asp:Label>
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Samho" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label6" runat="server" Text="District"></asp:Label>
                        <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="All" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label7" runat="server" Text="Village"></asp:Label>
                        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="All" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label8" runat="server" Text="Ward"></asp:Label>
                        <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="0" Text="All" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>                
                </div>
                <div class="row mb-2">
                    <div class="col-md-12">
                        <center>
                <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-outline-primary"/>
                </center>
                    </div>
                </div>
        </div>
    </div>
</div>
    <br />
</asp:Content>

