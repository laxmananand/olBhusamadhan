<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MeriPehchan_Registration.aspx.cs" Inherits="MeriPehchan_Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        divclss {
            -ms-overflow-style: none; /* for Internet Explorer, Edge */
            scrollbar-width: none; /* for Firefox */
            overflow-y: scroll;
        }

            divclss::-webkit-scrollbar {
                display: none; /* for Chrome, Safari, and Opera */
            }

        /* other styling */
        divclss {
            border: solid 5px black;
            border-radius: 5px;
            height: 300px;
            padding: 2px;
            width: 200px;
        }

            divclss.* {
                background-color: #EAF0F6;
                color: #2D3E50;
                font-family: 'Avenir';
                font-size: 26px;
                font-weight: bold;
            }

        #divImage {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 700px;
            width: 1200px;
            padding: 3px;
            border: solid 1px black;
        }

        .table-responsive {
            display: masonry !important;
        }
    </style>

    <script type="text/javascript">


        function ValidateDetail() {
            debugger;

            var ddlDistrict = document.getElementById('<%=ddlDistrict.ClientID%>');
            if (ddlDistrict.options[ddlDistrict.selectedIndex].value == 0) {
                alert("कृपया जिला चुनें...!");
                ddlDistrict.focus();
                return false;
            }

            var ddlSubdivision = document.getElementById('<%=ddlSubdivision.ClientID%>');
            if (ddlSubdivision.options[ddlSubdivision.selectedIndex].value == 0) {
                alert("कृपया सब डिवीज़न चुनें...!");
                ddlSubdivision.focus();
                return false;
            }

            var ddlBlock = document.getElementById('<%=ddlBlock.ClientID%>');
            if (ddlBlock.options[ddlBlock.selectedIndex].value == 0) {
                alert("कृपया ब्लाक का चुनाव करें...!");
                ddlBlock.focus();
                return false;
            }

            var ddlPolice = document.getElementById('<%=ddlPolice.ClientID%>');
            if (ddlPolice.options[ddlPolice.selectedIndex].value == 0) {
                alert("कृपया पुलिस स्टेशन का चुनाव करें...!");
                ddlPolice.focus();
                return false;
            }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <h4 class="text-black text-center"><b>Edit Profile</b></h4>
        <div class="row">
            <center>
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True" Visible="false"></asp:Label>
            </center>
        </div>
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-2">
                        <label class="control-label" for="inputGroup">User Name </label>
                        <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter User Name" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label class="control-label" for="inputGroup">Name </label>
                        <asp:TextBox ID="txtName" runat="server" placeholder="Enter Name" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label class="control-label" for="inputGroup">Email </label>
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-2">
                        <label class="control-label" for="inputGroup">Mobile </label>
                        <asp:TextBox ID="txtmobile" runat="server" placeholder="Enter Mobile" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                    </div>

                    <%-- <div class="row mb-2">--%>

                    <div class="col-md-2">
                        <asp:Label ID="Label1" runat="server" Text="रेंज"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlRange" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="ddlRange_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select range..."
                            ControlToValidate="ddlRange" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                            InitialValue="0" ForeColor="Red">रेंज</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label22" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Select district..."
                            ControlToValidate="ddlDistrict" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                            InitialValue="0" ForeColor="Red">जिला</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label23" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlSubdivision" runat="server" CssClass="form-control mb-2" AutoPostBack="True"
                            Enabled="true" OnSelectedIndexChanged="ddlSubdivision_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Select अनुमंडल..."
                            ControlToValidate="ddlSubdivision" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                            InitialValue="0" ForeColor="Red">अनुमंडल</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label24" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control mb-2" AutoPostBack="true"
                            Enabled="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Select अनुमंडल..."
                            ControlToValidate="ddlBlock" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                            InitialValue="0" ForeColor="Red">अनुमंडल</asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label25" runat="server" Text="थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="ddlPolice" runat="server" CssClass="form-control mb-2" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Select थाना..."
                            ControlToValidate="ddlPolice" SetFocusOnError="true" Display="Dynamic" ValidationGroup="2"
                            InitialValue="0" ForeColor="Red">थाना</asp:RequiredFieldValidator>
                    </div>
                    <br />
                    <div class="row mb-2" style="margin-top: 23px;">
                        <div class="col-md-12">
                            <center>

                                <asp:Button ID="btnAddDetail" OnClick="btnAddDetail_Click" runat="server" Text="Save" CssClass="btn btn-primary" ValidationGroup="1" OnClientClick="return ValidateDetail();"></asp:Button>
                            </center>
                        </div>
                    </div>
                    <%-- </div>--%>
                </div>
            </div>
        </div>
        <br />



    </div>
</asp:Content>

