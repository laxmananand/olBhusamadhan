<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bhu-Dashboard.aspx.cs" Inherits="Bhu_Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bhu-Samadhan Dashboard</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="vendors/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Vendors/bootstrap.min.css" rel="stylesheet" />
    <style>
        .button-29 {
            align-items: center;
            appearance: none;
            background-image: radial-gradient(100% 100% at 100% 0, #5adaff 0, #5468ff 100%);
            border: 0;
            border-radius: 6px;
            box-shadow: rgba(45, 35, 66, .4) 0 2px 4px,rgba(45, 35, 66, .3) 0 7px 13px -3px,rgba(58, 65, 111, .5) 0 -3px 0 inset;
            box-sizing: border-box;
            color: #fff;
            cursor: pointer;
            display: inline-flex;
            font-family: "JetBrains Mono",monospace;
            height: 30px;
            justify-content: center;
            line-height: 1;
            list-style: none;
            overflow: hidden;
            padding-left: 16px;
            padding-right: 16px;
            position: relative;
            text-align: left;
            text-decoration: none;
            transition: box-shadow .15s,transform .15s;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            white-space: nowrap;
            will-change: box-shadow,transform;
            font-size: 18px;
        }

            .button-29:focus {
                box-shadow: #3c4fe0 0 0 0 1.5px inset, rgba(45, 35, 66, .4) 0 2px 4px, rgba(45, 35, 66, .3) 0 7px 13px -3px, #3c4fe0 0 -3px 0 inset;
            }

            .button-29:hover {
                box-shadow: rgba(45, 35, 66, .4) 0 4px 8px, rgba(45, 35, 66, .3) 0 7px 13px -3px, #3c4fe0 0 -3px 0 inset;
                transform: translateY(-2px);
            }

            .button-29:active {
                box-shadow: #3c4fe0 0 3px 7px inset;
                transform: translateY(2px);
            }
    </style>
    <style>
        .button-30 {
            align-items: center;
            appearance: none;
            background-color: #c72108;
            border-radius: 4px;
            border-width: 0;
            box-shadow: rgba(45, 35, 66, 0.4) 0 2px 4px,rgba(45, 35, 66, 0.3) 0 7px 13px -3px,#c72108 0 -3px 0 inset;
            box-sizing: border-box;
            color: #36395A;
            cursor: pointer;
            display: inline-flex;
            font-family: "JetBrains Mono",monospace;
            height: 30px;
            justify-content: center;
            line-height: 1;
            list-style: none;
            overflow: hidden;
            padding-left: 16px;
            padding-right: 16px;
            position: relative;
            text-align: left;
            text-decoration: none;
            transition: box-shadow .15s,transform .15s;
            user-select: none;
            -webkit-user-select: none;
            touch-action: manipulation;
            white-space: nowrap;
            will-change: box-shadow,transform;
            font-size: 18px;
        }

            .button-30:focus {
                box-shadow: #c72108 0 0 0 1.5px inset, rgba(45, 35, 66, 0.4) 0 2px 4px, rgba(45, 35, 66, 0.3) 0 7px 13px -3px, #c72108 0 -3px 0 inset;
            }

            .button-30:hover {
                box-shadow: rgba(45, 35, 66, 0.4) 0 4px 8px, rgba(45, 35, 66, 0.3) 0 7px 13px -3px, #c72108 0 -3px 0 inset;
                transform: translateY(-2px);
            }

            .button-30:active {
                box-shadow: #c72108 0 3px 7px inset;
                transform: translateY(2px);
            }
    </style>
   
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
                    <div class="row mb-2" style="background-color: #0D4C92; color: #CFF5E7; padding: 5px;">
                        <div class="col-md-6">
                            <a href="../../Default.aspx" class="button-29" style="text-decoration:none">Home</a>&nbsp;<b>Bhu-Samadhan Dashboard</b>                                                   
                        </div>
                        <div class="col-md-6"></div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:HiddenField ID="hdnDistrict" runat="server"/>
                            <asp:HiddenField ID="hdnBlockCode" runat="server"/>
                            <asp:HiddenField ID="hdnThanaCode" runat="server"/>
                            <asp:Label ID="Label1" runat="server" Text="जिला"></asp:Label>
                            <asp:DropDownList ID="ddDistrict" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             >
                                                                            </asp:DropDownList>  
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" Text="अंचल"></asp:Label>
                            <asp:DropDownList ID="ddBlock" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             >
                                                                            </asp:DropDownList>   
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label3" runat="server" Text="Police Station"></asp:Label>
                            <asp:DropDownList ID="ddlPoliceSation" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            >
                                                                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="पंचायत"></asp:Label>
                             <asp:DropDownList ID="ddPanchayat" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                            >
                                                                            </asp:DropDownList> 
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label5" runat="server" Text="सवेदनशीलता"></asp:Label>
                            <asp:DropDownList ID="ddSensivity" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             >
                                                                            </asp:DropDownList>  
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label6" runat="server" Text="बैठक का निष्कर्ष(Action)"></asp:Label>
                            <asp:DropDownList ID="ddlaction" runat="server" CssClass="form-control mb-2" Enabled="true" 
                                                                             >
                                                                                <asp:ListItem Value="0">All</asp:ListItem>
                                                                                <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                                                                                <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                                                                                <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                                                                                <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                                                                                <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                                                                            </asp:DropDownList>  
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label7" runat="server" Text="Form Date:"></asp:Label>
                            <asp:TextBox ID="txtdatefrom" runat="server" Type="Date" CssClass="form-control mb-2"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label8" runat="server" Text="To Date:"></asp:Label>
                            <asp:TextBox ID="txtDateTo" runat="server" Type="Date" CssClass="form-control mb-2"></asp:TextBox>
                        </div>                       
                        <div class="col-md-2">
                            <br />
                            <asp:Button ID="BtnSearch" runat="server" Text="Search" class="button-29" onclick="BtnSearch_Click"/>
                            <%--<span>
                                <asp:Button ID="Button3" runat="server" Text="Export To Excel" class="button-30 text-white" />
                            </span>--%>
                        </div>
                    </div>                    
                    <div class="row" style="padding: 11px;">
                   <div class="col-md-3 bg-danger text-white" style="border-radius: 5px; border: 2px solid white">
                    <center>
                    <div class="row">
                         <div class="col-sm-4">                            
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" data-type="BhumiVivad" 
                            data-vivadtype="1">कुल&nbsp;<br/><b><asp:Label runat="server" ID="lbltotalapplication" Text=""></asp:Label></b></a></center>
                                                                                                         
                         </div>
                         <div class="col-sm-4">
                             <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                              data-type="BhumiVivad" data-vivadtype="2">अंतिम रूप&nbsp;<br/><b><asp:Label runat="server" ID="lblFinalize" Text=""></asp:Label></b></a></center>&nbsp;
                            
                             
                         </div>
                         <div class="col-sm-4">
                              <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                               data-type="BhumiVivad" data-vivadtype="3">अधूरा&nbsp;<br /><b><asp:Label runat="server" ID="lblUnFinalize" Text=""></asp:Label></b></a></center>&nbsp;
                                             
                         </div>
                     </div>
                         </center>
                </div>
                   <div class="col-md-6 bg-primary text-white" style="border-radius: 5px; border: 2px solid white">
                    <center>
                <div class="row">
                    
                    <div class="col-sm-3">
                        <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                        data-type="BhumiVivad" data-vivadtype="4">प्रारंभिक निष्पादन&nbsp;<br /><b><asp:Label runat="server" ID="lblnispadan" Text=""></asp:Label></b></a></center>&nbsp;
                        
                    </div>
                    <div class="col-sm-2">
                         <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                          data-type="BhumiVivad" data-vivadtype="5">अंतिम निष्पादन&nbsp;<br /><b><asp:Label runat="server" ID="lblFinaldisposal" Text=""></asp:Label></b></a></center>&nbsp;
                        
                    </div>
                    <div class="col-sm-2">
                         <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                         data-type="BhumiVivad" data-vivadtype="6">प्रक्रियाधीन&nbsp;<br /><b><asp:Label runat="server" ID="lblprakreeyadheen" Text=""></asp:Label></b></a></center>&nbsp;
                        
                    </div>
                    <div class="col-sm-3">
                         <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                          data-type="BhumiVivad" data-vivadtype="7">मापी के लिए निर्धारित&nbsp;<br /><b><asp:Label runat="server" ID="lblmapikenirdharit" Text=""></asp:Label></b></a></center>&nbsp;
                      
                    </div>
                    <div class="col-sm-2">
                         <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                         data-type="BhumiVivad" data-vivadtype="8">अस्वीकृत&nbsp;<br /><b><asp:Label runat="server" ID="lblashvikrit" Text=""></asp:Label></b></a></center>&nbsp;
                        
                    </div>
                    
                </div>
                    </center>
                </div>
                   <div class="col-md-3 bg-success text-white" style="border-radius: 5px; border: 2px solid white">
                    <div class="row">
                        <div class="col-md-4">
                             <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                             data-type="BhumiVivad" data-vivadtype="9">सामान्य&nbsp;<br /><b><asp:Label runat="server" ID="lblsaamaany" Text=""></asp:Label></b></a></center>&nbsp;
                           
                        </div>
                        <div class="col-md-4">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                             data-type="BhumiVivad" data-vivadtype="10">संवेदनशील&nbsp;<br /><b><asp:Label runat="server" ID="lblsavedansheel" Text=""></asp:Label></b></a></center>&nbsp;
                           
                            
                        </div>
                        <div class="col-md-4">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" 
                             data-type="BhumiVivad" data-vivadtype="11">अतिसंवेदनशील&nbsp;<br /><b><asp:Label runat="server" ID="lblatiSavedansheel" Text=""></asp:Label></b></a></center>&nbsp;                                              
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-6">
                    <h5 class="text-center">भूमि विवाद का प्रकार</h5>
                    <div class="row" style="padding: 15px; margin-top: -15px;">
                        <div class="col-md-3" style="background-color:#243763;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" data-type="BhumiVivadType" data-vivadtype="1">पर्चाधारी के बेदखली:</a></center>&nbsp;
                            <span style="color: #FF6E31;font-size:20px;float:right"><b><asp:Label runat="server" ID="BhumiVivadType1"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#227C70;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" data-type="BhumiVivadType" data-vivadtype="2">गैरमजरुआ:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px; float:right"><b><asp:Label runat="server" ID="BhumiVivadType2"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#460C68;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;line-height:1.5em;float:left;" data-type="BhumiVivadType" data-vivadtype="3">रैयती भूमि पर सीमांकन:</a></center>&nbsp;
                            <span style="color: #F56EB3;font-size:20px;float:right"><b><asp:Label runat="server" ID="BhumiVivadType3"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#355764;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="BhumiVivadType" data-vivadtype="4">निजी रास्ता/नाली:</a></center>&nbsp;
                            <span style="color: #FFEA11;font-size:20px;float:right"><b><asp:Label runat="server" ID="BhumiVivadType4"></asp:Label></b></span>
                        </div>
                    </div>

                    <div class="row" style="padding: 15px; margin-top: -25px;">
                        <div class="col-md-3" style="background-color:#243763;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" data-type="BhumiVivadType" data-vivadtype="5">जल स्रोत:</a></center>&nbsp;
                            <span style="color: #FF6E31;font-size:20px;float:right;"><b><asp:Label runat="server" ID="BhumiVivadType5"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#227C70;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px; float:left;" data-type="BhumiVivadType" data-vivadtype="6">पैतृक/मौरुषी बटवारा:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px;float:right"><b><asp:Label runat="server" ID="BhumiVivadType6"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#460C68;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;" data-type="BhumiVivadType" data-vivadtype="7">खेती से सम्बंधित:</a></center>&nbsp;
                            <span style="color: #F56EB3;font-size:20px;float:right;"><b><asp:Label runat="server" ID="BhumiVivadType7"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#355764;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;" data-type="BhumiVivadType" data-vivadtype="8">वास से सम्बंधित:</a></center>&nbsp;
                            <span style="color: #FFEA11;font-size:20px;float:right;"><b><asp:Label runat="server" ID="BhumiVivadType8"></asp:Label></b></span>
                        </div>
                    </div>
                    <div class="row" style="padding: 15px; margin-top: -25px;">
                        <div class="col-md-3" style="background-color:#243763;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;" data-type="BhumiVivadType" data-vivadtype="9">लगान निर्धारण:</a></center>&nbsp;
                            <span style="color: #FF6E31;font-size:20px;float:right;"><b><asp:Label runat="server" ID="BhumiVivadType9"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#227C70;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;" data-type="BhumiVivadType" data-vivadtype="10">व्यावसायिक भूमि:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px;float:right;"><b><asp:Label runat="server" ID="BhumiVivadType10"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#460C68;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;" data-type="BhumiVivadType" data-vivadtype="11">बदलेन से सम्बंधित:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px;float:right;"><b><asp:Label runat="server" ID="BhumiVivadType11"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#355764;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;" data-type="BhumiVivadType" data-vivadtype="12">भू-अर्जन से सम्बंधित:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px;float:right"><b><asp:Label runat="server" ID="BhumiVivadType12"></asp:Label></b></span>
                        </div>
                    </div>
                    <div class="row" style="padding: 15px; margin-top: -25px;">
                        <div class="col-md-3" style="background-color:#243763;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;" data-type="BhumiVivadType" data-vivadtype="13">भू-हदबंदी (अधिशेष):</a></center>&nbsp;
                            <span style="color: #FF6E31;font-size:20px;float:right"><b><asp:Label runat="server" ID="BhumiVivadType13"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#227C70;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left;" data-type="BhumiVivadType" data-vivadtype="15">रैयती भूमि पर कब्ज़ा:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px;float:right"><b><asp:Label runat="server" ID="BhumiVivadType15"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#460C68;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="BhumiVivadType" data-vivadtype="20">अन्य:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px;float:right"><b><asp:Label runat="server" ID="BhumiVivadType20"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#355764;"></div>
                    </div>

                    
                    <h5 class="text-center">जमीन की क़िस्म का विवरण</h5>
                    <div class="row" style="padding: 15px; margin-top: -20px;">
                        <div class="col-md-3" style="background-color:#355764;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 18px;float:left" data-type="Jameen_Ki_Qism_Ka_Vivaran" data-vivadtype="1">धनहर-1:</a></center>&nbsp;
                            <span style="color: #EAE7B1;font-size:20px;float:right"><b><asp:Label runat="server" ID="Landdesciption1"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#0D4C92;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Jameen_Ki_Qism_Ka_Vivaran" data-vivadtype="2">धनहर-2:</a></center>&nbsp;
                            <span style="color: #CFF5E7;font-size:20px;float:right"><b><asp:Label runat="server" ID="Landdesciption2"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3"  style="background-color:#850E35;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Jameen_Ki_Qism_Ka_Vivaran" data-vivadtype="3">धनहर-3:</a></center>&nbsp;
                            <span style="color: #FFF5E4;font-size:20px;float:right"><b><asp:Label runat="server" ID="Landdesciption3"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3"  style="background-color:#355764;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Jameen_Ki_Qism_Ka_Vivaran" data-vivadtype="5">भीठ-1:</a></center>&nbsp;
                            <span style="color: #FFEA11;font-size:20px;float:right"><b><asp:Label runat="server" ID="Landdesciption5"></asp:Label></b></span>
                        </div>
                       
                    </div>
                    <div class="row" style="padding: 15px; margin-top: -25px;">
                        <div class="col-md-3" style="background-color:#355764;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Jameen_Ki_Qism_Ka_Vivaran" data-vivadtype="6">भीठ-2:</a></center>&nbsp;
                            <span style="color: #EAE7B1;font-size:20px;float:right"><b><asp:Label runat="server" ID="Landdesciption6"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#0D4C92;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Jameen_Ki_Qism_Ka_Vivaran" data-vivadtype="11">आवासीय भूमि:</a></center>&nbsp;
                            <span style="color: #CFF5E7;font-size:20px;float:right"><b><asp:Label runat="server" ID="Landdesciption11"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3"style="background-color:#850E35;">
                            <center><a href="#" class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Jameen_Ki_Qism_Ka_Vivaran" data-vivadtype="12">व्यासायिक भूमि:</a></center>&nbsp;
                            <span style="color: #FFF5E4;font-size:20px;float:right"><b><asp:Label runat="server" ID="Landdesciption12"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#355764;"> </div>
                       
                    </div>

                    <h5 class="text-center">साक्ष्य का प्रकार (वादी)</h5>
                    <div class="row" style="padding: 15px; margin-top: -20px;">
                        <div class="col-md-2" style="background-color:#243763;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="1">खतियान:</a></center>&nbsp;
                            <span style="color: #FF6E31;font-size:20px;float:right"><b><asp:Label runat="server" ID="vadiEvidence1"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#227C70;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="2">केवाला/विक्रय पत्र:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px; float:right"><b><asp:Label runat="server" ID="vadiEvidence2"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#460C68;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="3">जमाबंदी का नक़ल:</a></center>&nbsp;
                            <span style="color: #F56EB3;font-size:20px;float:right"><b><asp:Label runat="server" ID="vadiEvidence3"></asp:Label></b></span>
                        </div>
                        <div class="col-md-2" style="background-color:#355764;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="4">लगान रसीद:</a></center>&nbsp;
                            <span style="color: #FFEA11;font-size:20px;float:right"><b><asp:Label runat="server" ID="vadiEvidence4"></asp:Label></b></span>
                        </div>
                        <div class="col-md-2 bg-dark text-white">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="5">वंशावली:</a></center>&nbsp;
                            <span style="color: white;font-size:20px;float:right"><b><asp:Label runat="server" ID="vadiEvidence5"></asp:Label></b></span>
                        </div>
                    </div>
                    <div class="row" style="padding: 15px; margin-top: -25px;">
                        <div class="col-md-2"style="background-color:#243763;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="6">बँटवारा</a></center>&nbsp;
                            <span style="color: #FF6E31;font-size:20px;float:right"><b><asp:Label runat="server" ID="vadiEvidence6"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#227C70;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="7">पर्चा(परवाना)</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px; float:right"><b><asp:Label runat="server" ID="vadiEvidence7"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#460C68;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="8">नायालय का आदेश</a></center>&nbsp;
                            <span style="color: #F56EB3;font-size:20px;float:right"><b><asp:Label runat="server" ID="vadiEvidence8"></asp:Label></b></span>
                        </div>
                        <div class="col-md-2"style="background-color:#355764;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Vadi" data-vivadtype="9">अन्य</a></center>&nbsp;
                            <span  style="color: #FFEA11;font-size:20px;float:right"><b><asp:Label runat="server" ID="vadiEvidence9"></asp:Label></b></span>
                        </div>
                         <div class="col-md-2 bg-dark text-white"></div>
                    </div>

                    <h5 class="text-center">साक्ष्य का प्रकार (प्रतिवादी)</h5>
                    <div class="row" style="padding: 15px; margin-top: -20px;">
                        <div class="col-md-2" style="background-color:#355764;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="1">खतियान:</a></center>&nbsp;
                            <span style="color: #EAE7B1;font-size:20px;float:right"><b><asp:Label runat="server" ID="PrativadiEvidence1"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#0D4C92;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="2">केवाला/विक्रय पत्र:</a></center>&nbsp;
                            <span style="color: #CFF5E7;font-size:20px;float:right"><b><asp:Label runat="server" ID="PrativadiEvidence2"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#850E35;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="3">जमाबंदी का नक़ल:</a></center>&nbsp;
                            <span style="color: #FFF5E4;font-size:20px;float:right"><b><asp:Label runat="server" ID="PrativadiEvidence3"></asp:Label></b></span>
                        </div>
                        <div class="col-md-2" style="background-color:#355764;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="4">लगान रसीद:</a></center>&nbsp;
                            <span style="color: #FFEA11;font-size:20px;float:right"><b><asp:Label runat="server" ID="PrativadiEvidence4"></asp:Label></b></span>
                        </div>
                        <div class="col-md-2 bg-dark text-white">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="5">वंशावली:&nbsp;
                                <span><b><asp:Label runat="server" ID="PrativadiEvidence5"></asp:Label></b></span></a></center>
                        </div>
                    </div>
                    <div class="row" style="padding: 15px; margin-top: -25px;">
                        <div class="col-md-2" style="background-color:#355764;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="6">बँटवारा:</a></center>&nbsp;
                            <span style="color: #EAE7B1;font-size:20px;float:right"><b><asp:Label runat="server" ID="PrativadiEvidence6"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3"  class="BhuDashboard" style="background-color:#0D4C92;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="7">पर्चा(परवाना):</a></center>&nbsp;
                            <span style="color: #CFF5E7;font-size:20px;float:right"><b><asp:Label runat="server" ID="PrativadiEvidence7"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#850E35;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="8">नायालय का आदेश:</a></center>&nbsp;
                            <span style="color: #FFF5E4;font-size:20px;float:right"><b><asp:Label runat="server" ID="PrativadiEvidence8"></asp:Label></b></span>
                        </div>
                        <div class="col-md-2" style="background-color:#355764;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Saakshy_Ka_Prakaar_Prativadi" data-vivadtype="9">अन्य:</a></center>&nbsp;
                            <span style="color: #FFEA11;font-size:20px;float:right"><b><asp:Label runat="server" ID="PrativadiEvidence9"></asp:Label></b></span>
                        </div>
                         <div class="col-md-2 bg-dark text-white">
                          
                        </div>
                    </div>

                   <h5 class="text-center">वारदात का विवरण</h5>
                    <div class="row" style="padding: 15px; margin-top: -20px;">
                        <div class="col-md-2" style="background-color:#243763;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Vaaradaat_Ka_Vivaran" data-vivadtype="Y" checktype="is_FIR_registered">प्राथमिकी:</a></center>&nbsp;
                            <span style="color: #FF6E31;font-size:20px;float:right"><b><asp:Label runat="server" ID="praathamikee"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#227C70;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Vaaradaat_Ka_Vivaran" data-vivadtype="Y" checktype="is_complaint_filed">अप्राथमिकी:</a></center>&nbsp;
                            <span style="color: #FFBF00;font-size:20px; float:right"><b><asp:Label runat="server" ID="apraathamikee"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#460C68;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Vaaradaat_Ka_Vivaran" data-vivadtype="Y" checktype="is_Sanha_recorded">सनहा:</a></center>&nbsp;
                            <span style="color: #F56EB3;font-size:20px;float:right"><b><asp:Label runat="server" ID="sanaha"></asp:Label></b></span>
                        </div>
                        <div class="col-md-2" style="background-color:#355764;">
                           
                        </div>
                        <div class="col-md-2 bg-dark text-white">
                            
                        </div>    
                    </div>   
                     
                    <h5 class="text-center">न्यायालय का विवरण</h5>
                    <div class="row" style="padding: 15px; margin-top: -20px;">
                        <div class="col-md-2" style="background-color:#355764;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Nyaayaalay_Ka_Vivaran" data-vivadtype="1">राजस्व न्यायालय:</a></center>&nbsp;
                            <span style="color: #EAE7B1;font-size:20px;float:right"><b><asp:Label runat="server" ID="raajasv_nyaayaalay"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#0D4C92;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Nyaayaalay_Ka_Vivaran" data-vivadtype="2">व्यवहार न्यायालय:</a></center>&nbsp;
                            <span style="color: #CFF5E7;font-size:20px;float:right"><b><asp:Label runat="server" ID="vyavahaara_nyaayaalay"></asp:Label></b></span>
                        </div>
                        <div class="col-md-3" style="background-color:#850E35;">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px; float:left" data-type="Nyaayaalay_Ka_Vivaran" data-vivadtype="4">लोक शिकायत निवारण:</a></center>&nbsp;
                            <span style="color: #FFF5E4;font-size:20px;float:right"><b><asp:Label runat="server" ID="LokShikayat_Nivaran_nyaayaalay"></asp:Label></b></span>
                        </div>
                        <div class="col-md-2" style="background-color:#355764;">       
                             <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;float:left" data-type="Nyaayaalay_Ka_Vivaran" data-vivadtype="5">उच्च न्यायालय:</a></center> &nbsp;
                            <span style="color: #FFEA11;font-size:20px;float:right"><b><asp:Label runat="server" ID="uchcha_nyaayaalay"></asp:Label></b></span>                 
                        </div>  
                        <div class="col-md-2 bg-dark text-white">
                            <center><a href="#"  class="BhuDashboard" style="color: white; font-size: 15px;" data-type="Nyaayaalay_Ka_Vivaran" data-vivadtype="6">सर्वोच्य न्यायालय:&nbsp;<span><b>
                                <asp:Label runat="server" ID="sarvochcha_nyaayaalay"></asp:Label></b></span></a></center>
                        </div>    
                    </div>                
                </div>
              
                <div class="col-md-6">
                    <div class="row w-100">
                        <div class="col-lg-12 my-4" id="containermap" style="height:600px">
                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
     <script src="Vendors/bootstrap.min.js"></script>
      <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>

    <script src="https://code.highcharts.com/maps/highmaps.js"></script>
    <script src="https://code.highcharts.com/maps/modules/data.js"></script>
    <script src="https://code.highcharts.com/maps/modules/drilldown.js"></script>

    <script src="https://code.highcharts.com/maps/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/maps/modules/offline-exporting.js"></script>
    <script src="https://code.highcharts.com/maps/modules/accessibility.js"></script>--%>

    <script src="JavaScript.js"></script>

   

    <script src="highmaps.js"></script>
    <script src="data.js"></script>
    <script src="drilldown.js"></script>

    <script src="exporting.js"></script>
    <script src="offline-exporting.js"></script>
    <script src="accessibility.js"></script>
    <script src="Mapvillage.js"></script>
   
</body>
</html>

