<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchApplication.aspx.cs" Inherits="SearchApplication" %>

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
        <div class="card">
            <div class="card-header bg-primary">
                <h4 class="text-center text-white">आवेदन का विवरण</h4>
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-md-1"></div>
                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="Commissionary"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Enabled="false">
                            <asp:ListItem Text="Munger" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label2" runat="server" Text="District"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" Enabled="false">
                            <asp:ListItem Text="Begusarai" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label3" runat="server" Text="Sub-Division"></asp:Label>
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" Enabled="false">
                            <asp:ListItem Text="Begusarai" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label4" runat="server" Text="Circle"></asp:Label>
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" Enabled="false">
                            <asp:ListItem Text="Begusarai" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label5" runat="server" Text="Police Station"></asp:Label>
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control" Enabled="false">
                            <asp:ListItem Text="Samho" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1"></div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-1"></div>
                    <div class="col-md-2">
                        <asp:Label ID="Label6" runat="server" Text="Panchayat"></asp:Label>
                        <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control">
                            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label7" runat="server" Text="Village"></asp:Label>
                        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control">
                            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label8" runat="server" Text="Ward"></asp:Label>
                        <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control">
                            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label9" runat="server" Text="Sensitivity Type"></asp:Label>
                        <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control">
                            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label10" runat="server" Text="Search Criteria"></asp:Label>
                        <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Samho" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1"></div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-3"></div>
                    <div class="col-md-4 mb-2">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="Button1" runat="server" Text="Saerch" CssClass="btn btn-outline-primary" />
                    </div>
                    <div class="col-md-3"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

