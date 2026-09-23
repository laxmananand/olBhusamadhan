<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Finalize_Application.aspx.cs" Inherits="Police_Station_Finalize_Application" %>

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
            <div class="card-body">
                <div class="row">
                    <div class="col-md-1">
                        <asp:Label ID="Label1" runat="server" Text="Paze Size"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Type="Number"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label2" runat="server" Text="वादी (मोबाइल संख्या) / Application No."></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-1">
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-outline-primary" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

