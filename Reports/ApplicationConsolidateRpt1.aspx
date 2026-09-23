<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplicationConsolidateRpt1.aspx.cs" Inherits="Reports_ApplicationConsolidateRpt1" %>

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
                <h4 class="text-center text-white">Application Consolidated Report</h4>
            </div>
            <div class="card-body">
                <table class="table table-striped table-bordered table-responsive-md">
                    <thead>
                        <tr>
                            <th scope="col">Sl. No.</th>
                            <th scope="col">Police Station</th>
                            <th scope="col">कुल आवेदन</th>
                            <th scope="col">निस्तारित</th>
                            <th scope="col">प्रक्रियाधीनन</th>
                            <th scope="col">अस्वीकृत</th>
                            <th scope="col">मापी क़े लिए निर्धारित</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th scope="row">1</th>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td><i class="fa fa-eye" style="color:dodgerblue"></i>&nbsp; &nbsp;<i class="fa fa-edit" style="color:darkblue"></i>&nbsp; &nbsp; <i class="fa fa-trash" style="color:red"></i></td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

