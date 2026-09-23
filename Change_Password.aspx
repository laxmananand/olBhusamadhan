<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Change_Password.aspx.cs" Inherits="Change_Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="row mt-3">            
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                            <h4 class="text-center text-dark" style="font-size:20px;"> <b>Change Your Password</b></h4>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control mb-3" placeholder="Enter Name"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label2" runat="server" Text="Mobile Number:"></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control mb-3" placeholder="Enter Mobile Number"></asp:TextBox>
                            </div>
                                          
                            <div class="col-md-3">
                                <asp:Label ID="Label3" runat="server" Text="New Password:"></asp:Label>
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control mb-3" TextMode="Password" placeholder="Enter New Password"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label4" runat="server" Text="Confirm Password:"></asp:Label>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control mb-3" TextMode="Password" placeholder="Enter Confirm Password"></asp:TextBox>
                            </div>
                      </div>
                        <div class="row">
                            <div class="col-md-3">
                                <asp:Label ID="Label5" runat="server" Text="Enter Captcha:"></asp:Label>
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control mb-3" placeholder="Enter Captcha"></asp:TextBox>
                            </div>
                            <div class="col-md-3"></div>
                            <div class="col-md-3">
                                <br>
                                <asp:Button ID="Button1" runat="server" Text="Change Password" CssClass="btn btn-primary" />
                            </div>
                            <div class="col-md-3"></div>
                        </div>
                    </div>
                </div>
            </div>
 
   </div>
    </div>
   
    <br />
</asp:Content>

