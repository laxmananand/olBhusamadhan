<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="New_Entry.aspx.cs" Inherits="Police_Station_New_Entry" %>

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
        <h4 class="text-black text-center"><b>आवेदन का विवरण</b></h4>
        <div class="card mb-3">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-12">
                        <center>
                    <a href="#" style="color: darkblue; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:700;background: #1B990E; background: linear-gradient(to right, #1B990E 0%, #B013CF 100%);
                    -webkit-background-clip: text;
                    -webkit-text-fill-color: transparent;">वादी और भूमि विवाद&nbsp;<i class="fa fa-circle-arrow-right" style="color: darkblue"></i></a> &nbsp;&nbsp; 
                    
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:700;">प्रतिवादी और अन्य&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a> &nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:700;">खाता-खेसरा&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:700;">वादी और प्रतिवादी का साक्ष्य&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:700;">प्रस्तुत साक्ष्य&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:700;">घटना-वारदात और न्यायलय&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:700;">अंचलाधिकारी और थानाध्यक्ष की बैठक&nbsp;<i class="fa fa-circle-arrow-right" style="color: lightgray"></i></a>&nbsp;&nbsp;
                    <a href="#" style="color: lightgray; padding: 5px; border-radius: 5px; text-decoration: none;font-weight:700;">आवेदन की प्रक्रिया पूरी हुई
                    </a>
                            </center>
                    </div>
                </div>
            </div>
        </div>

        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>वादी का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label2" runat="server" Text="वादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label1" runat="server" Text="लिंग चुने"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Male" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Female" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Other" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label3" runat="server" Text="जन्म का वर्ष"></asp:Label>
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="2022" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label4" runat="server" Text="पिता/ पति का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label5" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label6" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna Sadar" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label7" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna Anchal" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label8" runat="server" Text="थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna Thana" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label9" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label10" runat="server" Text="ग्राम पंचायत"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna Sadar" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label11" runat="server" Text="राजस्व ग्राम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna Anchal" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label12" runat="server" Text="वार्ड"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna Thana" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label13" runat="server" Text="मोबाइल नंबर"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-9"></div>
                </div>
                <br />
                <div class="row mb-2 text-white" style="background-color: dodgerblue">
                    <div class="col-md-3 p-1">
                        <asp:Label ID="Label14" runat="server" Text="क्या वादी किसी विभाग का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList11" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Yes" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="No" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 p-1">
                        <asp:Label ID="Label15" runat="server" Text="विभाग का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList12" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="नाम" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 p-1">
                        <asp:Label ID="Label16" runat="server" Text="विभाग में पदनाम"></asp:Label>
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <p class="p-1"><b style="color: yellow">नोट:</b> यदि विभाग की कोई जमीन है तो उस स्थिति में वादी विभाग के प्रतिनिधि होंगे | </p>
                </div>

                <div class="row mb-2 text-white" style="background-color: lightseagreen">
                    <div class="col-md-12 p-1">
                        <asp:Label ID="Label17" runat="server" Text="क्या वादी किसी संस्था का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList13" runat="server" CssClass="form-control w-25">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="हाँ" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="नहीं" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2 p-1">
                        <asp:Label ID="Label18" runat="server" Text="संस्था का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList14" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Type" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2 p-1">
                        <asp:Label ID="Label19" runat="server" Text="संस्था का सम्बन्ध"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList15" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Type" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2 p-1">
                        <asp:Label ID="Label20" runat="server" Text="संस्था का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 mb-2 p-1">
                        <asp:Label ID="Label21" runat="server" Text="संस्था में पदनाम"></asp:Label>
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Save & Next</a>
                </center>

            </div>
        </div>
    </div>

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि विवाद का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label22" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList18" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label23" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList16" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna Sadar" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label24" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList17" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Patna" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label25" runat="server" Text="थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList19" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Kankarbagh" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label26" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList20" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Rural" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label27" runat="server" Text="ग्राम पंचायत "></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList21" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Neema" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label28" runat="server" Text="राजस्व ग्राम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList22" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Bharra" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label29" runat="server" Text="वार्ड"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList23" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Ward-01" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label30" runat="server" Text="विवाद का अद्यतन कारक"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList24" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label31" runat="server" Text="राजस्व थाना संख्या "></asp:Label>
                        <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label32" runat="server" Text="भूमि का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList26" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="सरकारी" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label33" runat="server" Text="सरकारी भूमि का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList27" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label40" runat="server" Text="सरकारी भूमि का प्रकार (अगर अन्य है?)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList25" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label41" runat="server" Text="भूमि विवाद का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList30" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label42" runat="server" Text="भूमि विवाद का प्रकार (अगर अन्य है)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label43" runat="server" Text="आवेदन की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox11" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-9">
                        <asp:Label ID="Label34" runat="server" Text="वादी द्वारा भूमि विवाद का संक्षिप्त विवरणी "></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label35" runat="server" Text="वादी द्वारा प्रस्तुत आवेदन"></asp:Label>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label36" runat="server" Text="केवल .pdf(2 MB) प्रारूप में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-9">
                        <asp:Label ID="Label37" runat="server" Text="प्रतिवादी द्वारा भूमि विवाद का संक्षिप्त विवरणी"></asp:Label>
                        <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label38" runat="server" Text="प्रतिवादी द्वारा प्रस्तुत आवेदन"></asp:Label>
                        <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
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
    </div>

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>प्रतिवादी का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label44" runat="server" Text="प्रतिवादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label45" runat="server" Text="पिता/ पति का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label46" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList28" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label47" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList29" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label48" runat="server" Text="अंचल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList31" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label49" runat="server" Text="थाना"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList32" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label50" runat="server" Text="क्षेत्र का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList33" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label51" runat="server" Text="ग्राम पंचायत"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList34" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row mb-3">
                    <div class="col-md-3">
                        <asp:Label ID="Label52" runat="server" Text="राजस्व ग्राम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList35" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label53" runat="server" Text="वार्ड"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList36" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label54" runat="server" Text="मोबाइल नंबर"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox14" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3"></div>
                </div>
                <div class="row mb-2 text-white" style="background-color: dodgerblue">
                    <div class="col-md-3 p-1">
                        <asp:Label ID="Label55" runat="server" Text="क्या प्रतिवादी किसी विभाग का प्रतिनिधि है?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList37" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Yes" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="No" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 p-1">
                        <asp:Label ID="Label56" runat="server" Text="विभाग का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList38" runat="server" CssClass="form-control mb-2">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="नाम" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 p-1">
                        <asp:Label ID="Label57" runat="server" Text="विभाग में पदनाम"></asp:Label>
                        <asp:TextBox ID="TextBox50" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <p class="p-1"><b style="color: yellow">नोट:</b> यदि कोई व्यक्ति विभाग के जमीन पे दावा करता है तो उस स्थिति में प्रतिवादी विभाग के प्रतिनिधि होंगे | </p>
                </div>

                <div class="row mb-2 text-white" style="background-color: lightseagreen">
                    <div class="col-md-12 p-1">
                        <asp:Label ID="Label129" runat="server" Text="क्या प्रतिवादी किसी संस्था का प्रतिनिधि है?"></asp:Label>
                        <asp:DropDownList ID="DropDownList39" runat="server" CssClass="form-control w-25">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="हाँ" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="नहीं" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2 p-1">
                        <asp:Label ID="Label130" runat="server" Text="संस्था का प्रकार"></asp:Label>
                        <asp:DropDownList ID="DropDownList64" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Type" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2 p-1">
                        <asp:Label ID="Label131" runat="server" Text="संस्था का सम्बन्ध"></asp:Label>
                        <asp:DropDownList ID="DropDownList65" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="Type" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 mb-2 p-1">
                        <asp:Label ID="Label132" runat="server" Text="संस्था का नाम"></asp:Label>
                        <asp:TextBox ID="TextBox51" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 mb-2 p-1">
                        <asp:Label ID="Label133" runat="server" Text="संस्था में पदनाम"></asp:Label>
                        <asp:TextBox ID="TextBox52" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <h5 class="text-black"><b>अन्य विवरण</b></h5>
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label63" runat="server" Text="प्रतिवादी को सूचित किया गया है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList45" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="चुने"></asp:ListItem>
                            <asp:ListItem Value="0" Text="हाँ"></asp:ListItem>
                            <asp:ListItem Value="1" Text="नही"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label64" runat="server" Text="माध्यम"></asp:Label>
                        <asp:DropDownList ID="DropDownList46" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="चुने"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label65" runat="server" Text="प्रतिवादी को सूचना तामिला प्राप्त है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList47" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="चुने"></asp:ListItem>
                            <asp:ListItem Value="0" Text="हाँ"></asp:ListItem>
                            <asp:ListItem Value="1" Text="नही"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label66" runat="server" Text="प्रतिवादी उपस्थित हुआ है या नहीं ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList48" runat="server" CssClass="form-control">
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

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि का खाता-खेसरा का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-2">
                        <asp:Label ID="Label67" runat="server" Text="खाता संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label68" runat="server" Text="खेसरा संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control"></asp:TextBox>
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
                                <asp:TextBox ID="TextBox17" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                <asp:Label ID="Label58" runat="server" Text="क्षेत्रफल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                                <asp:Label ID="Label59" runat="server" Text="क्षेत्रफल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="TextBox19" runat="server" CssClass="form-control mb-2"></asp:TextBox>

                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label70" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="DropDownList49" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="चुने" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="बीघा" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="हेक्टेयर" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="वर्ग मीटर" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label60" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="DropDownList50" runat="server" CssClass="form-control mb-2">
                                    <asp:ListItem Text="चुने" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="कट्ठा" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="एकड़" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="वर्ग फीट" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="वर्ग यार्ड" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label61" runat="server" Text="यूनिट"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:DropDownList ID="DropDownList51" runat="server" CssClass="form-control mb-2">
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
                                <asp:DropDownList ID="DropDownList40" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-9">
                                <asp:Label ID="Label76" runat="server" Text="खतियान में जमीन का विवरण"></asp:Label>
                                <asp:TextBox ID="TextBox21" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px !important;"></asp:TextBox>
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
                                <asp:TextBox ID="TextBox22" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label78" runat="server" Text="दक्षिण"></asp:Label>
                                <asp:TextBox ID="TextBox23" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label79" runat="server" Text="पूर्व"></asp:Label>
                                <asp:TextBox ID="TextBox24" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label80" runat="server" Text="पश्चिम"></asp:Label>
                                <asp:TextBox ID="TextBox25" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
    </div>
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
                        <asp:Label ID="Label62" runat="server" Text="वादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज उपलब्ध है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList52" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label81" runat="server" Text="साक्ष्य का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList41" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label82" runat="server" Text="अगर अन्य हैं तो दस्तावेज का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox27" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label71" runat="server" Text="वादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज"></asp:Label>
                        <asp:FileUpload ID="FileUpload3" runat="server" CssClass="form-control" />
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
                        <asp:Label ID="Label72" runat="server" Text="प्रतिवादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज उपलब्ध है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList42" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="-1" Text="अन्य" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label73" runat="server" Text="साक्ष्य का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList43" runat="server" CssClass="form-control">
                            <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label74" runat="server" Text="अगर अन्य हैं तो दस्तावेज का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox20" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label83" runat="server" Text="प्रतिवादी द्वारा प्रस्तुत साक्ष्य का दस्तावेज"></asp:Label>
                        <asp:FileUpload ID="FileUpload4" runat="server" CssClass="form-control" />
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
    </div>


    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>राजस्व अधिकारी / पुलिस पदाधिकारी / हल्का कर्मचारी द्वारा प्रस्तुत साक्ष्य का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label85" runat="server" Text="पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी"></asp:Label>
                        <asp:TextBox ID="TextBox30" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label86" runat="server" Text="पुलिस पदाधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज"></asp:Label>
                        <asp:FileUpload ID="FileUpload5" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label87" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-6">
                        <asp:Label ID="Label88" runat="server" Text="हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन की संक्षिप्त विवरणी"></asp:Label>
                        <asp:TextBox ID="TextBox31" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label89" runat="server" Text="हल्का कर्मचारी / राजस्व अधिकारी द्वारा समर्पित जाँच प्रतिवेदन का दस्तावेज"></asp:Label>
                        <asp:FileUpload ID="FileUpload6" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label90" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label91" runat="server" Text="विवादित भू-खंड की मापी"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList54" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Please Select" Value="-1" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label135" runat="server" Text="मापी ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:DropDownList ID="DropDownList55" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Please Select" Value="-1" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3">
                        <asp:Label ID="Label84" runat="server" Text="मापी के लिए निर्धारित तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox26" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <asp:Label ID="Label92" runat="server" Text="विवादित भू-खंड की मापी का प्रतिवेदन"></asp:Label>
                        <asp:FileUpload ID="FileUpload10" runat="server" CssClass="form-control" />
                        <asp:Label ID="Label93" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" Style="color: darkred"></asp:Label>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-md-3">
                        <asp:Label ID="Label134" runat="server" Text="विवादित भू-खंड की मापी नहीं होने का कारण"></asp:Label>
                        <asp:TextBox ID="TextBox28" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
    </div>

    <br />

    <div class="container-fluid">
        <div class="card">
            <div class="card-header text-center" style="font-size: 18px"><b><u>भूमि विवाद सें संबंधित घटना/ वारदात का विवरण</u></b></div>
            <div class="card-body">
                <div class="row mb-2">
                    <div class="col-md-4">
                        <asp:Label ID="Label94" runat="server" Text="क्या भूमि विवाद सें संबंधित प्राथमिकी / अप्राथमिकी / सनहा दर्ज है ?"></asp:Label>
                        <asp:DropDownList ID="DropDownList57" runat="server" CssClass="form-control">
                            <asp:ListItem Text="चुने" Selected="True" Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:Label ID="Label95" runat="server" Text="घटना / वारदात की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                        <asp:TextBox ID="TextBox32" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <asp:Label ID="Label96" runat="server" Text="घटना की संक्षिप्त विवरण"></asp:Label>
                        <asp:TextBox ID="TextBox33" runat="server" CssClass="form-control" TextMode="Multiline" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
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
                                <asp:DropDownList ID="DropDownList58" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="चुने" Selected="True" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label98" runat="server" Text="प्राथमिकी संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                <asp:TextBox ID="TextBox39" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label108" runat="server" Text="प्राथमिकी का विवरण"></asp:Label>
                                <asp:TextBox ID="TextBox40" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
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
                                <asp:DropDownList ID="DropDownList70" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
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
                                <asp:TextBox ID="TextBox34" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:Label ID="Label109" runat="server" Text="अप्राथमिकी का विवरण"></asp:Label>
                                <asp:TextBox ID="TextBox41" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
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
                                    <asp:DropDownList ID="DropDownList71" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1" Text="चुने"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label112" runat="server" Text="सनहा संख्या"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox42" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">सनहा का विवरण
                                    <asp:TextBox ID="TextBox29" runat="server" CssClass="form-control" TextMode="MultiLine" Style="height: 43px;" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                </div>
                               
                            </div>

                            <div class="row mb-2">
                                 <div class="col-md-3">
                                    <asp:Label ID="Label113" runat="server" Text="अभियुक्ति"></asp:Label>
                                    <asp:TextBox ID="TextBox43" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
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
        </div>
   
                <br />

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-header text-center" style="font-size: 18px"><b><u>न्यायालय में प्रक्रियाधीन वाद का विवरण</u></b></div>
                        <div class="card-body">
                            <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label101" runat="server" Text="क्या न्यायालय में प्रक्रियाधीन वाद का विवरण उपलब्ध है ?"></asp:Label>
                                    <asp:DropDownList ID="DropDownList61" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="चुने" Selected="True" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label136" runat="server" Text="न्यायालय"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="DropDownList44" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="col-md-3">
                                    <asp:Label ID="Label137" runat="server" Text="न्यायालय का प्रकार"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="DropDownList53" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                 <div class="col-md-3">
                                    <asp:Label ID="Label138" runat="server" Text="जिला"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="DropDownList56" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                                   <div class="row mb-2">
                                <div class="col-md-3">
                                    <asp:Label ID="Label139" runat="server" Text="अनुमंडल"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="DropDownList66" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="चुने" Selected="True" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label140" runat="server" Text="वादी की वाद संख्या / वर्ष"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox53" runat="server" CssClass="form-control" placeholder="उदाहरण: 1234/14"></asp:TextBox>
                                </div>
                                 <div class="col-md-3">
                                    <asp:Label ID="Label141" runat="server" Text="वादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                     <asp:TextBox ID="TextBox54" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                 <div class="col-md-3">
                                    <asp:Label ID="Label142" runat="server" Text="प्रतिवादी का नाम"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox55" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row mb-2">
                                 <div class="col-md-3">
                                    <asp:Label ID="Label143" runat="server" Text="वाद की अद्यतन स्थिति का विवरण"></asp:Label>
                                    <asp:TextBox ID="TextBox56" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
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
                </div>

                <br />

                <div class="container-fluid">
                    <div class="card">
                        <div class="card-header text-center" style="font-size: 18px"><b><u>अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण</u></b></div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-2">
                                    <asp:Label ID="Label114" runat="server" Text="भूमि विवाद की संवेदनशीलता"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="DropDownList72" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1">
                                    <br />
                                    <i class="fa fa-star"></i>&nbsp; <i class="fa fa-star"></i>&nbsp; <i class="fa fa-star"></i>
                                </div>
                                   <div class="col-md-3">
                                    <asp:Label ID="Label115" runat="server" Text="बैठक की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox45" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label116" runat="server" Text="क्या वादी उपस्थित है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="DropDownList73" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label117" runat="server" Text="क्या प्रतिवादी उपस्थित है ?"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="DropDownList59" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                
                            </div>

                            <div class="row mb-3">
                             
                                <div class="col-md-3">
                                    <asp:Label ID="Label118" runat="server" Text="बैठक का निष्कर्ष (Action)"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:DropDownList ID="DropDownList60" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="-1" Text="Please Select"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label119" runat="server" Text="अगली सुनवाई की तिथि"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox46" runat="server" CssClass="form-control" Type="Date"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    
                                    <asp:Label ID="Label102" runat="server" Text="अस्वीकृति का कारण"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox35" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द" Style="height: 43px;"></asp:TextBox>
                                </div>
                           
                            </div>

                            <div class="row mb-4">
                                  <div class="col-md-6">
                                    <asp:Label ID="Label122" runat="server" Text="बैठक में लिया गया निर्णय"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox49" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label123" runat="server" Text="थानाध्यक्ष एवं अंचलाधिकारी का संयुक्त प्रतिवेदन"></asp:Label>
                                    <asp:FileUpload ID="FileUpload7" runat="server" CssClass="form-control" />
                                    <asp:Label ID="Label124" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" style="color:darkred"></asp:Label>
                                </div>
                  
                              
                            </div>
                            <div class="row mb-2">
                                <div class="col-md-6">
                                    <asp:Label ID="Label103" runat="server" Text="अंचलाधिकारी का मंतव्य"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox36" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label104" runat="server" Text="अंचलाधिकारी का मंतव्य पत्र"></asp:Label>
                                    <asp:FileUpload ID="FileUpload11" runat="server" CssClass="form-control" />
                                    <asp:Label ID="Label105" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" style="color:darkred"></asp:Label>
                                </div>
                            </div>


                               <div class="row mb-2">
                                <div class="col-md-6">
                                    <asp:Label ID="Label106" runat="server" Text="थानाध्यक्ष का मंतव्य"></asp:Label>&nbsp;<img src="images/red_star_PNG44.png" class="img-fluid" style="width: 15px; height: auto" />
                                    <asp:TextBox ID="TextBox37" runat="server" CssClass="form-control" TextMode="MultiLine" placeholder="अधिकतम 500 शब्द"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label107" runat="server" Text="थानाध्यक्ष का मंतव्य पत्र"></asp:Label>
                                    <asp:FileUpload ID="FileUpload12" runat="server" CssClass="form-control" />
                                    <asp:Label ID="Label111" runat="server" Text="केवल .pdf प्रारूप में (2 MB) तक में अपलोड करे" style="color:darkred"></asp:Label>
                                </div>
                            </div>

                            

                        </div>
                    </div>
                </div>
                <<br />
                            <center>
                    <a href="#" style="padding:5px; border-radius:5px;background-color:darkred; color:white; text-decoration:none"><i class="fa fa-arrow-left"></i>&nbsp;Back</a>&nbsp; &nbsp;
                    <a href="#" style="padding:5px; border-radius:5px;background-color:dodgerblue; color:white; text-decoration:none"><i class="fa fa-home"></i>&nbsp;Go to Home</a>
                     &nbsp; <a href="#" style="padding:5px; border-radius:5px;background-color:coral; color:white;text-decoration:none"><i class="fa fa-eye"></i>&nbsp;Preview</a>&nbsp;
                    &nbsp;<a href="#" style="padding:5px; border-radius:5px;background-color:green; color:white;text-decoration:none"><i class="fa fa-save"></i>&nbsp;Finalize</a>
                </center>
                <br />
</asp:Content>

