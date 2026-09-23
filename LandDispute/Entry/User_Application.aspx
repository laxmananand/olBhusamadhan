<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User_Application.aspx.cs" Inherits="Police_Station_New_Entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../vendors/font-awesome.min.css" rel="stylesheet" />
    <link href="vendors/font-awesome.min.css" rel="stylesheet" />
    <link href="../css/ruang-admin.min.css" rel="stylesheet" />
    <link href="../vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />

    <script src="../vendor/jquery/jquery.min.js"></script>
    <script src="../vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../vendor/jquery-easing/jquery.easing.min.js"></script>
    <script src="../vendor/chart.js/Chart.min.js"></script>
    <script src="../js/demo/chart-area-demo.js"></script>
    <script src="../vendor/fontawesome-free-6.1.1/js/all.min.js"></script>
    <script src="../js/ruang-admin.min.js"></script>--%>
    <script type="text/javascript">
        function ValidateAlpha(evt) {
            var keyCode = (evt.which) ? evt.which : evt.keyCode
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && (keyCode != 32))
                return false;
            return true;
        }

        function ValidateNum(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function ValidateMobile(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        function Upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        }
    </script>
    <style>
        hr {
            margin-top: 0px;
            margin-bottom: 0px;
            color:yellow;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <AjaxControlToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></AjaxControlToolkit:ToolkitScriptManager>
    <div class="container-fluid"><%--1--%>
        <h4 class="text-black text-center"><b>आवेदन का विवरण</b></h4>
        <div class="card mb-3">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <center>
                    <a href="#" style="color: darkblue; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:500;background: #1B990E; background: linear-gradient(to right, #1B990E 0%, #B013CF 100%);
                    -webkit-background-clip: text;
                    -webkit-text-fill-color: transparent;">वादी और भूमि विवाद&nbsp;<i class="fa fa-circle-arrow-right" style="color: darkblue"></i></a> &nbsp;&nbsp; 
                    
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:500;">प्रतिवादी और अन्य&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a> &nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:500;">खाता-खेसरा&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:500;">वादी और प्रतिवादी का साक्ष्य&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:500;">प्रस्तुत साक्ष्य&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:500;">घटना-वारदात और न्यायलय&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:500;">अंचलाधिकारी और थानाध्यक्ष की बैठक&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                  
                   
                            </center>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="card">
                    <div class="card-header text-center" style="font-size: 18px"><b><u>वादी का विवरण</u></b></div>
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="Label2" runat="server" Text="वादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtVadiName" runat="server" CssClass="form-control mb-2"
                                    placeholder="वादी का नाम" oncopy="return false" onpaste="return false"
                                    oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Enter वादी का नाम..."
                                    ControlToValidate="txtVadiName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red">वादी का नाम</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label1" runat="server" Text="लिंग चुने"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Value="0" Text="--Select--" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="M" Text="Male" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="F" Text="Female" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="O" Text="Other" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiDistrict" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">लिंग चुने</asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label3" runat="server" Text="जन्म का वर्ष"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control mb-2">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label4" runat="server" Text="पिता/ पति का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtVadiFather" runat="server" CssClass="form-control mb-2" placeholder="पिता/ पति का नाम" oncopy="return false" onpaste="return false"
                                    oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter वादी का नाम..."
                                    ControlToValidate="txtVadiFather" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red">पिता/ पति का नाम</asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="Label5" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiDistrict" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVadiDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiDistrict" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">जिला</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label6" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiSubDiv" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVadiSubDiv_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiSubDiv" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">अनुमंडल</asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label7" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiCircle" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVadiCircle_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiCircle" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">अंचल</asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label8" runat="server" Text="थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiPS" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVadiPS_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiPS" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">थाना</asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="Label9" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiAreaType" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVadiAreaType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="--Select--" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="R" Text="Rural" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="U" Text="Urban" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiAreaType" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">क्षेत्र का प्रकार</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblPan" runat="server" Text="ग्राम पंचायत"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiPanchayat" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVadiPanchayat_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiPanchayat" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">ग्राम पंचायत</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3" id="divVadiVill" runat="server">
                                <asp:Label ID="Label11" runat="server" Text="राजस्व ग्राम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiVillage" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVadiVillage_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiVillage" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">राजस्व ग्राम</asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label12" runat="server" Text="वार्ड"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiWard" runat="server" CssClass="form-control mb-2">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiWard" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">वार्ड</asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="row mb-2">
                            <div class="col-md-3" id="divVadiMoh" runat="server">
                                <asp:Label ID="Label120" runat="server" Text="मोहल्ला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtVadiMoh" runat="server" CssClass="form-control" placeholder="मोहल्ला का नाम" oncopy="return false" onpaste="return false"
                                    oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter मोहल्ला का नाम..."
                                    ControlToValidate="txtVadiMoh" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red">मोहल्ला का नाम</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label13" runat="server" Text="मोबाइल नंबर"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtVadiMob" runat="server" CssClass="form-control" MaxLength="10" onkeypress="return ValidateMobile(event)"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtVadiMob" ID="RegularExpressionValidator3"
                                    ValidationExpression="^[\s\S]{10,10}$" runat="server" ValidationGroup="1" ErrorMessage="10 numbers required."></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter मोहल्ला का नाम..."
                                    ControlToValidate="txtVadiMob" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red">मोबाइल नंबर</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6"></div>
                        </div>
                        <br />

                        <div class="row  text-white" style="background-color: dodgerblue">
                            <div class="col-md-3 p-1">
                                <asp:Label ID="Label14" runat="server" Text="क्या वादी किसी विभाग का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsVadiDept" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlIsVadiDept_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="--चुने--" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="Y" Text="हां" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="नहीं" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlIsVadiDept" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">क्या वादी किसी विभाग का प्रतिनिधि है?</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 p-1" id="divVadiDptName" runat="server" visible="false">
                                <asp:Label ID="Label15" runat="server" Text="विभाग का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlVadiDept" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlVadiDept_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlVadiDept" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">विभाग का नाम</asp:RequiredFieldValidator>

                            </div>
                            <div class="col-md-3 p-1" id="divVadiDesig" runat="server" visible="false">
                                <asp:Label ID="Label16" runat="server" Text="विभाग में पदनाम"></asp:Label>
                                <asp:TextBox ID="txtVadiDesig" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>


                        </div>
                        <div class="row mb-2 text-white" style="background-color: dodgerblue">
                            <div class="col-md-12 p-1">
                                <p class="p-1" style="background-color: dodgerblue"><b style="color: yellow;">नोट:</b> <u style="color: white; text-decoration: none;">यदि विभाग की कोई जमीन है तो उस स्थिति में वादी विभाग के प्रतिनिधि होंगे |</u> </p>
                            </div>
                        </div>


                        <div class="row  text-white" style="background-color: lightseagreen">
                            <div class="col-md-3 p-1">
                                <asp:Label ID="Label10" runat="server" Text="क्या वादी किसी संस्था का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsVadiSanstha" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlIsVadiSanstha_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="--चुने--" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="Y" Text="हां" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="नहीं" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlIsVadiSanstha" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">क्या वादी किसी विभाग का प्रतिनिधि है?</asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div id="divSanstha" runat="server" visible="false" class="row mb-2 text-white" style="background-color: lightseagreen">
                            <div class="col-md-3 mb-2 p-1">
                                <asp:Label ID="Label17" runat="server" Text="संस्था का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsVadiSanstha_type" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="Type" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlIsVadiSanstha_type" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">संस्था का प्रकार</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 mb-2 p-1">
                                <asp:Label ID="Label18" runat="server" Text="संस्था का सम्बन्ध"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsVadiSanstha_relation" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Select district..."
                                    ControlToValidate="ddlIsVadiSanstha_relation" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1"
                                    InitialValue="0" ForeColor="Red">संस्था का सम्बन्ध</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 mb-2 p-1">
                                <asp:Label ID="Label19" runat="server" Text="संस्था का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtVadiSansthaName" runat="server" CssClass="form-control" placeholder="वादी का नाम" oncopy="return false" onpaste="return false"
                                    oncut="return false" onkeyup="Upper(this)" onkeypress="return ValidateAlpha(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Enter वादी का नाम..."
                                    ControlToValidate="txtVadiSansthaName" SetFocusOnError="true" Display="Dynamic" ValidationGroup="1" ForeColor="Red">संस्था का नाम</asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3 mb-2 p-1">
                                <asp:Label ID="lbl" runat="server" Text="संस्था में पदनाम"></asp:Label>
                                <asp:TextBox ID="txtVadiSansthaDesig" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <center>
                    <%--<asp:Button ID=""Button12" runat="server" Text="Go to Home" CssClass="btn btn-primary" ValidationGroup="1" background-color:green/>--%>
                              <%--<asp:Button ID="Button1" runat="server" Text="Preview" CssClass="btn btn-primary" ValidationGroup="1" background-color:green/>--%>
                              <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="1" OnClick="btnSubmit_OnClick">
                            </asp:Button>
                </center>

                        <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="true" Font-Bold="True" ForeColor="Maroon" ValidationGroup="1" />--%>

                        <div class="row  text-white">
                            <div class="col-md-12 p-1">
                                <asp:GridView ID="gvFinal" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-hover"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <div style="text-align: left;">
                                                    क्र.सं.
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div style="text-align: center;">
                                                    <%#Container.DataItemIndex+1+"." %>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="Qualification" HeaderText="वादी का नाम" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Qualificationnew5" HeaderText="पिता/पति का नाम" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />--%>

                                        <asp:TemplateField HeaderText="वादी का नाम  <hr /> पिता / पति का नाम">
                                            <ItemTemplate>
                                                <div class="row" style="padding-bottom: 0.5%; padding-top: 0.5%">
                                                    <div class="col-lg-12 col-md-12 col-sm-12">
                                                        <%# Eval("Qualification") %>
                                                        <hr style="padding: 2px; margin: 2px;" />
                                                        <%# Eval("Qualification")%>
                                                    </div>
                                                    <br />
                                                    
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                       <%-- <asp:BoundField DataField="Qualificationnew" HeaderText="लिंग" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Qualificationnew4" HeaderText="जन्म का वर्ष" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        
                                        <asp:BoundField DataField="Qualificationnew6" HeaderText="जिला" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Qualificationnew7" HeaderText="अनुमंडल" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Qualificationnew8" HeaderText="अंचल" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Qualificationnew9" HeaderText="थाना" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="QualificationType45" HeaderText="क्षेत्र का प्रकार" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="QualificationType55" HeaderText="ग्राम पंचायत" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="QualificationType5556" HeaderText="वार्ड" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="QualificationType5556" HeaderText="मोहल्ला" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="QualificationType5556445" HeaderText="मोबाइल नंबर" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Marks" HeaderText="Marks" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}" />--%>
                                    </Columns>
                                    <HeaderStyle Wrap="False" BackColor="AliceBlue" Font-Bold="True" ForeColor="Black"
                                        BorderStyle="Solid" BorderWidth="1" HorizontalAlign="Center" Height="15px" />
                                </asp:GridView>
                            </div>
                        </div>


                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि विवाद का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label22" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputeDistrict" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label23" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputeSubdiv" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna Sadar" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label24" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputeCircle" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label25" runat="server" Text="थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputePS" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Kankarbagh" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label26" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputeAreaType" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Rural" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label27" runat="server" Text="ग्राम पंचायत "></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputePanchayat" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Neema" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label28" runat="server" Text="राजस्व ग्राम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputeVillage" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Bharra" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label29" runat="server" Text="वार्ड"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputeWard" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Ward-01" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label30" runat="server" Text="विवाद का अद्यतन कारक"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDisputeAdhtanKarak" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label31" runat="server" Text="राजस्व थाना संख्या "></asp:Label>
                        <asp:TextBox ID="txtRajswaThana" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label32" runat="server" Text="भूमि का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlLandType" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="सरकारी" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label33" runat="server" Text="सरकारी भूमि का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlGovtLandType" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label40" runat="server" Text="सरकारी भूमि का प्रकार (अगर अन्य है ?) "></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtGovtLandAnya" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label41" runat="server" Text="भूमि विवाद का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlLandDisputeType" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label42" runat="server" Text="भूमि विवाद का प्रकार (अगर अन्य है)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtGovtLandDisputeAnya" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label43" runat="server" Text="आवेदन की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtApplDate" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-9">
                        <asp:Label ID="Label34" runat="server" Text="वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी "></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtVadiDisputeDesc" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label35" runat="server" Text="वादी द्वारा प्रस्तुत आवेदन"></asp:Label>
                        <asp:FileUpload ID="txtVadiApp" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label36" runat="server" Text="केवल .pdf(2 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-9">
                        <asp:Label ID="Label37" runat="server" Text="प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी"></asp:Label>
                        <asp:TextBox ID="txtPvadiDisputeDesc" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label38" runat="server" Text="प्रतिवादी द्वारा प्रस्तुत आवेदन"></asp:Label>
                        <asp:FileUpload ID="txtPvadiApp" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label39" runat="server" Text="केवल .pdf(2 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>

                <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Save & Next</a>
                </center>
            </div>
        </div>
    </div><%-- 2--%>

    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="card">
                    <div class="card-header text-center" style="font-size: 18px"><b><u>प्रतिवादी का विवरण</u></b></div>
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="Label44" runat="server" Text="प्रतिवादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtPvadiName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label45" runat="server" Text="पिता/ पति का नाम"></asp:Label>
                                <asp:TextBox ID="txtPvadiFName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label46" runat="server" Text="जिला"></asp:Label>
                                <asp:DropDownList ID="ddlPvadiDist" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPvadiDistrict_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label47" runat="server" Text="अनुमंडल"></asp:Label>
                                <asp:DropDownList ID="ddlPvadiSub" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPvadiSubDiv_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="Label48" runat="server" Text="अंचल"></asp:Label>
                                <asp:DropDownList ID="ddlPvadiCircle" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPvadiCircle_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label49" runat="server" Text="थाना"></asp:Label>
                                <asp:DropDownList ID="ddlPvadiPS" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPvadiPS_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label50" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>
                                <asp:DropDownList ID="ddlPvadiAreaType" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPvadiAreaType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="--Select--" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="R" Text="Rural" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="U" Text="Urban" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="lblPvadiPan" runat="server" Text="ग्राम पंचायत"></asp:Label>
                                <asp:DropDownList ID="ddlPvadiPanch" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPvadiPanchayat_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-3" id="divPvadiVill" runat="server">
                                <asp:Label ID="Label52" runat="server" Text="राजस्व ग्राम"></asp:Label>
                                <asp:DropDownList ID="ddlPvadiVill" runat="server" CssClass="form-control mb-2" AutoPostBack="True" OnSelectedIndexChanged="ddlPvadiVillage_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label53" runat="server" Text="वार्ड"></asp:Label>
                                <asp:DropDownList ID="ddlPvadiWard" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-3" id="divPvadiMoh" runat="server">
                                <asp:Label ID="Label54" runat="server" Text="मोहल्ला"></asp:Label>
                                <asp:TextBox ID="txtPvadiMoh" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label121" runat="server" Text="मोबाइल नंबर"></asp:Label>
                                <asp:TextBox ID="txtPvadiMob" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>
                        <div class="col-md-3"></div>
                        <div class="row mb-2 text-white" style="background-color: dodgerblue">
                            <div class="col-md-3 p-1">
                                <asp:Label ID="Label55" runat="server" Text="क्या प्रतिवादी किसी विभाग का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsPvadiDept" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="Yes" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="No" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 p-1">
                                <asp:Label ID="Label56" runat="server" Text="विभाग का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlPvadiDept" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="नाम" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 p-1">
                                <asp:Label ID="Label57" runat="server" Text="विभाग में पदनाम"></asp:Label>
                                <asp:TextBox ID="txtPvadiDesig" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>

                            <p class="p-1"><b style="color: yellow">नोट:</b> यदि कोई व्यक्ति विभाग के जमीन पे दावा करता है तो उस स्थिति में प्रतिवादी विभाग के प्रतिनिधि होंगे | </p>
                        </div>
                        <div class="row mb-2 text-white" style="background-color: lightseagreen">
                            <div class="col-md-12 p-1">
                                <asp:Label ID="Label129" runat="server" Text="क्या प्रतिवादी किसी संस्था का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsPvadiSanstha" runat="server" CssClass="form-control w-25">
                                    <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="हाँ" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="नहीं" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 mb-2 p-1">
                                <asp:Label ID="Label130" runat="server" Text="संस्था का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlPvadiSansthaType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="Type" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 mb-2 p-1">
                                <asp:Label ID="Label131" runat="server" Text="संस्था का सम्बन्ध"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlPvadiSansthaRelation" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                                    <asp:ListItem Value="-1" Text="Type" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3 mb-2 p-1">
                                <asp:Label ID="Label132" runat="server" Text="संस्था का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="ddlPvadiSansthaName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3 mb-2 p-1">
                                <asp:Label ID="Label133" runat="server" Text="संस्था में पदनाम"></asp:Label>
                                <asp:TextBox ID="txtPvadiSansthaDesig" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <h5 class="text-black"><b>अन्य विवरण</b></h5>
                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="Label63" runat="server" Text="प्रतिवादी को सूचित किया गया है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlPvadiInform" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="चुने"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="हाँ"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="नही"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label64" runat="server" Text="माध्यम"></asp:Label>
                                <asp:DropDownList ID="ddlInformMedium" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="चुने"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label65" runat="server" Text="प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlPvadiTamila" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="चुने"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="हाँ"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="नही"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label66" runat="server" Text="प्रतिवादी उपस्थित हुआ है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlPvadiPres" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="चुने"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="हाँ"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="नही"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                        </div>
                        <br />
                        <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Save & Next</a>
                </center>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel> <%--3--%>

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि का खाता-खेसरा का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-2">
                        <asp:Label ID="Label67" runat="server" Text="खाता संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtKhataNum" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label68" runat="server" Text="खेसरा संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtKhesraNum" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <br />
                        <a href="#" style="color: dodgerblue;"><b><i class="fa fa-link"></i>&nbsp;खाता-खेसरा को सत्यापित करने के लिए यहाँ क्लिक करें </b></a>
                    </div>
                </div>
                <div class="card mb-2">
                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                        <p>रकबा</p>
                    </div>
                    <div class="card-body">
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <asp:Label ID="Label69" runat="server" Text="क्षेत्रफल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtAreaBHB" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                <asp:Label ID="Label58" runat="server" Text="क्षेत्रफल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtAreaKHBB" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                <asp:Label ID="Label59" runat="server" Text="क्षेत्रफल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtAreaDDBB" runat="server" CssClass="form-control mb-2"></asp:TextBox>

                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label70" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlAreaBHB" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="चुने" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="बीघा" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="हेक्टेयर" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="वर्ग मीटर" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label60" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlAreaKHBB" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="चुने" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="कट्ठा" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="एकड़" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="वर्ग फीट" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="वर्ग यार्ड" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label61" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlDDBB" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="चुने" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="धुर" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="डेसिमल" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="वर्ग इंच" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="वर्ग फीट" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6"></div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-12">
                                <p style="color: red;"><b>नोट</b>:-<span style="color: black">&nbsp;क्षेत्रफल में सबसे बड़ी इकाई दर्ज करें फिर छोटी इकाई दर्ज करें फिर सबसे छोटी इकाई   दर्ज करें अर्थात 0 हेक्टेयर, 0 एकड़ ,1.5 डेसिमल</span></p>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <asp:Label ID="Label75" runat="server" Text="खतियान में जमीन की किस्म का विवरण"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlKhatLandType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Label76" runat="server" Text="खतियान में जमीन का विवरण"></asp:Label>
                                <asp:TextBox ID="txtKhatLandDesc" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px !important;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                        <p>चौहद्दी का विवरण</p>
                    </div>
                    <div class="card-body">
                        <div class="row mb-3">
                            <div class="col-md-3">
                                <asp:Label ID="Label77" runat="server" Text="उत्तर"></asp:Label>
                                <asp:TextBox ID="txtNorth" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label78" runat="server" Text="दक्षिण"></asp:Label>
                                <asp:TextBox ID="txtSouth" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label79" runat="server" Text="पूर्व"></asp:Label>
                                <asp:TextBox ID="txtEast" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label80" runat="server" Text="पश्चिम"></asp:Label>
                                <asp:TextBox ID="txtWest" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Save & Next</a>
                </center>
            <br />
        </div>
    </div><%-- 4--%>
    <br />

    <div class="container-fluid">
        <div class="card">
            <br />
            <h5 class="text-center text-black-100" style="color: gray"><u>वादी एवं प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण</u></h5>
            <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                <p>वादी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
            </div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label62" runat="server" Text="वादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlIsVadiEvi" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label81" runat="server" Text="साक्ष्य का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlVadiEviType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label82" runat="server" Text="अगर अन्य हैं तो दस्तावेज का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtVadiEviName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label71" runat="server" Text="वादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज"></asp:Label>
                        <asp:FileUpload ID="txtVadiEviDoc" runat="server" CssClass="form-control" />
                        <p style="color: darkred">केवल .pdf(2 MB) प्रारूप में अपलोड करे</p>
                    </div>
                </div>
            </div>
            <center><asp:Button ID="Button8" runat="server" Text="Save" CssClass="btn btn-primary" /></center>
            <br />
            <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                <p>प्रतिवादी द्वारा प्रस्तुत साक्ष्य का विवरण</p>
            </div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label72" runat="server" Text="प्रतिवादी द्वारा साक्ष्य का दस्तावेज उपलब्ध है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlIsPvadiEvi" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label73" runat="server" Text="साक्ष्य का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddPvadiEviType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label74" runat="server" Text="अगर अन्य हैं तो दस्तावेज का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtPvadiEviName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label83" runat="server" Text="प्रतिवादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज"></asp:Label>
                        <asp:FileUpload ID="txtPvadiEviDoc" runat="server" CssClass="form-control" />
                        <p style="color: darkred">केवल .pdf(2 MB) प्रारूप में अपलोड करे</p>
                    </div>
                </div>
            </div>
            <center><asp:Button ID="Button9" runat="server" Text="Save" CssClass="btn btn-primary" /></center>
            <br />
            <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Save & Next</a>
                </center>
            <br />
        </div>
        <br />

        <br />
    </div> <%--5--%>


    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label85" runat="server" Text="पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी"></asp:Label>
                        <asp:TextBox ID="txtPolDesc" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label86" runat="server" Text="पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज"></asp:Label>
                        <asp:FileUpload ID="txtPolDoc" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label87" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                         
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label88" runat="server" Text="हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी"></asp:Label>
                        <asp:TextBox ID="txtCoDesc" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label89" runat="server" Text="हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज"></asp:Label>
                        <asp:FileUpload ID="txtCoDoc" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label90" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label91" runat="server" Text="विवादित भू-खंड की मापी"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddIsDispMeasure" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Please Select" Value="-1" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label135" runat="server" Text="मापी ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddMeasure" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Please Select" Value="-1" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <asp:Label ID="Label84" runat="server" Text="मापी के लिए निर्धारित तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="ddMeasureDate" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label92" runat="server" Text="विवादित भू-खंड की मापी का प्रतिवेदन"></asp:Label>
                        <asp:FileUpload ID="ddMeasureDoc" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label93" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label134" runat="server" Text="विवादित भू-खंड की मापी नहीं होने का कारण"></asp:Label>
                        <asp:TextBox ID="ddMeasureReason" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="col-md-9"></div>
                </div>
                <br />
                <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Save & Next</a>
                </center>
                <br />
            </div>
        </div>
    </div> <%--6--%>

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि विवाद सें संबंधित घटना/ वारदात का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-4">
                        <asp:Label ID="Label94" runat="server" Text="प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlIsFir" runat="server" CssClass="form-control">
                            <asp:ListItem Text="चुने" Selected="True" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label95" runat="server" Text="घटना / वारदात की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtVardatDate" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label96" runat="server" Text="घटना की संक्षिप्त विवरण"></asp:Label>
                        <asp:TextBox ID="txtVardatDesc" runat="server" CssClass="form-control" TextMode="Multiline" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="card">
                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                        प्राथमिकी
                    </div>
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="Label97" runat="server" Text="प्राथमिकी दर्ज है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsPratimiki" runat="server" CssClass="form-control">
                                     <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                      <asp:ListItem Value="Y">हां</asp:ListItem>
                                      <asp:ListItem Value="N">नहीं</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label98" runat="server" Text="प्राथमिकी संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtPratimikiNum" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label108" runat="server" Text="प्राथमिकी का विवरण"></asp:Label>
                                <asp:TextBox ID="txtPratimikiDesc" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="card">
                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                        अप्राथमिकी
                    </div>
                    <div class="card-body">
                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label runat="server" Text="अप्राथमिकी दर्ज है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsApratimiki" runat="server" CssClass="form-control">
                                     <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                      <asp:ListItem Value="Y">हां</asp:ListItem>
                                     <asp:ListItem Value="N">नहीं</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label99" runat="server" Text="धारा"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" /><br />
                                <asp:CheckBox ID="CheckBox1" runat="server" />&nbsp;107&nbsp;<asp:CheckBox ID="CheckBox2" runat="server" />&nbsp;109&nbsp;<asp:CheckBox ID="CheckBox3" runat="server" />&nbsp;110&nbsp;<asp:CheckBox ID="CheckBox4" runat="server" />
                                &nbsp;113&nbsp;<asp:CheckBox ID="CheckBox5" runat="server" />&nbsp;116&nbsp;<asp:CheckBox ID="CheckBox6" runat="server" />&nbsp;133&nbsp;<asp:CheckBox ID="CheckBox7" runat="server" />
                                &nbsp;144&nbsp;<asp:CheckBox ID="CheckBox8" runat="server" />&nbsp;145&nbsp;<asp:CheckBox ID="CheckBox9" runat="server" />&nbsp;147
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label100" runat="server" Text="अप्राथमिकी संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtApratimikiNum" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label109" runat="server" Text="अप्राथमिकी का विवरण"></asp:Label>
                                <asp:TextBox ID="txtApratimikiDesc" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="card">
                    <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;">
                        सनहा
                    </div>
                    <div class="card-body">
                        <div class="row mb-2">

                            <div class="col-md-3">
                                <asp:Label ID="Label110" runat="server" Text="सनहा दर्ज़ है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="ddlIsSanha" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">--चुने--</asp:ListItem>
                                   <asp:ListItem Value="Y">हां</asp:ListItem>
                                    <asp:ListItem Value="N">नहीं</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label112" runat="server" Text="सनहा संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="txtSanhaNum" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                सनहा का विवरण
                                    <asp:TextBox ID="txtSanhaDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 43px;" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row mb-2">
                            <div class="col-md-3">
                                <asp:Label ID="Label113" runat="server" Text="अभियुक्ति"></asp:Label>
                                <asp:TextBox ID="txtFirRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                            </div>
                            <div class="col-md-9"></div>
                        </div>

                        <center><asp:Button ID="Button10" runat="server" Text="Save" CssClass="btn btn-primary" /></center>
                    </div>
                </div>
                <br />
                <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Save & Next</a>
                </center>
                <br />
            </div>
        </div>
    </div> <%--7--%>

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>न्यायालय में प्रक्रियाधीन वाद का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label101" runat="server" Text="प्रक्रियाधीन वाद का विवरण उपलब्ध है ?"></asp:Label>
                        <asp:DropDownList ID="ddlIsVadAvail" runat="server" CssClass="form-control">
                            <asp:ListItem Text="चुने" Selected="True" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label136" runat="server" Text="न्यायालय"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlCourt" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label137" runat="server" Text="न्यायालय का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlCourtType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label138" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlCourtDist" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label139" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlCourtSub" runat="server" CssClass="form-control">
                            <asp:ListItem Text="चुने" Selected="True" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label140" runat="server" Text="वादी की वाद संख्या / वर्ष"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtVadSankhya" runat="server" CssClass="form-control" placeholder="उदाहरण: 1234/14"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label141" runat="server" Text="वादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtCourtVadiName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label142" runat="server" Text="प्रतिवादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtCourtPvadiName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label143" runat="server" Text="वाद की अद्यतन स्थिति का विवरण"></asp:Label>
                        <asp:TextBox ID="txtVadAdhtan" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-9"></div>
                </div>


            </div>
            <center><asp:Button ID="Button11" runat="server" Text="Save" CssClass="btn btn-primary"/></center>
            <br />
            <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Save & Next</a>
                </center>
            <br />
        </div>
    </div> <%--8--%>

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-3">
                    <div class="col-md-2">
                        <asp:Label ID="Label114" runat="server" Text="विवाद की संवेदनशीलता"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlSensType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <br />
                        <i class="fa fa-star"></i>&nbsp; <i class="fa fa-star"></i>&nbsp; <i class="fa fa-star"></i>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label115" runat="server" Text="बैठक की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtMeetDate" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label116" runat="server" Text="क्या वादी उपस्थित है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlIsVadiPres" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label117" runat="server" Text="क्या प्रतिवादी उपस्थित है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlIsPvadiPres" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-md-3">
                        <asp:Label ID="Label118" runat="server" Text="बैठक का निष्कर्ष (Action)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label119" runat="server" Text="अगली सुनवाई की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="ddlSolnDate" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-6">

                        <asp:Label ID="Label102" runat="server" Text="अस्वीकृति का कारण"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtDeclineReason" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-md-6">
                        <asp:Label ID="Label122" runat="server" Text="बैठक में लिया गया निर्णय"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtMeetDesi" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label123" runat="server" Text="थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन"></asp:Label>
                        <asp:FileUpload ID="FileUpload7" runat="server" CssClass="form-control" />
                        <asp:Label ID="txtMeetDoc" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label103" runat="server" Text="अंचलाधिकारी का मंतव्य"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtCoDesi" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label104" runat="server" Text="अंचलाधिकारी का मंतव्य पत्र"></asp:Label>
                        <asp:FileUpload ID="FileUpload11" runat="server" CssClass="form-control" />
                        <asp:Label ID="txtCoMeetDoc" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label106" runat="server" Text="थानाध्यक्ष का मंतव्य"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="txtShoDesi" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label107" runat="server" Text="थानाध्यक्ष का मंतव्य पत्र"></asp:Label>
                        <asp:FileUpload ID="FileUpload12" runat="server" CssClass="form-control" />
                        <asp:Label ID="txtShoMeetDoc" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>



            </div>
        </div>
    </div> <%--9--%>
    <br />
    <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Finalize</a>
                </center>
    <br />
</asp:Content>

