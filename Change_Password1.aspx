<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Change_Password1.aspx.cs" Inherits="Change_Password1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
            <div class="card">
                <div class="card-header text-center bg-primary text-white">
                    Change Password
                </div>
                <div class="card-body">
                    <div class="row mb-3">
                    <div class="col-md-3">
                        <asp:Label ID="Label1" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/imp.gif" class="img-fluid" style="width:15px;height:auto"/>
                        <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label2" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/imp.gif" class="img-fluid" style="width:15px;height:auto"/>
                     <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label3" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/imp.gif" class="img-fluid" style="width:15px;height:auto"/>
                        <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="col-md-3"></div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-3">
                            <asp:Label ID="Label4" runat="server" Text="Username"></asp:Label>&nbsp;<img src="images/imp.gif" class="img-fluid" style="width:15px;height:auto"/>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label5" runat="server" Text="CO - Name"></asp:Label>&nbsp;<img src="images/imp.gif" class="img-fluid" style="width:15px;height:auto"/>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                     <div class="row mb-3">
                        <div class="col-md-3">
                            <asp:Label ID="Label6" runat="server" Text="Mobile Number"></asp:Label>&nbsp;<img src="images/imp.gif" class="img-fluid" style="width:15px;height:auto"/>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-1 mt-1"><br />
                            <a href="#" style="background-color:red;color:white;padding:5px;border-radius:3px;display:block;text-decoration:none;">Get OTP</a>                           
                        </div>
                         <div class="col-md-2"><br />
                              <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" placeholder="Enter OTP"></asp:TextBox>
                         </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label7" runat="server" Text="CO - Name"></asp:Label>&nbsp;<img src="images/imp.gif" class="img-fluid" style="width:15px;height:auto"/>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label8" runat="server" Text="Email Id"></asp:Label>&nbsp;<img src="images/imp.gif" class="img-fluid" style="width:15px;height:auto"/>
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <div class="col-md-3">
                            <asp:Label ID="Label9" runat="server" Text="New Password"></asp:Label>
                            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label10" runat="server" Text="Confirm Password"></asp:Label>
                            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label11" runat="server" Text="Enter Captcha"></asp:Label>
                            <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" placeholder="Enter Code"></asp:TextBox>
                        </div>
                        <div class="col-md-3"></div>
                    </div>

                     <div class="row mb-3">
                         <div class="col-md-12">
                             <center>
                             <asp:Button ID="Button1" runat="server" Text="Change Password" CssClass="btn btn-outline-success" />
                             <span>
                                 <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-outline-danger" />
                             </span>
                                 <span>
                                     <input id="Reset1" type="reset" value="reset" class="btn btn-outline-primary"/>
                                 </span>
                             </center>
                         </div>
                             
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

