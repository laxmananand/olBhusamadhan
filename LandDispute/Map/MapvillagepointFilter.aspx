<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MapvillagepointFilter.aspx.cs" Inherits="MapvillagepointFilter" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <link href="css/map.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/font-awesome.min.css" rel="stylesheet" />
    <link href="css/sidebars.css" rel="stylesheet" />
    <link href="css/datatables.min.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <style>
        .hidden {
  display: none;
}
    </style>
</head>
    <body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
        <div class="container-fluid">
            <div class="row">
                <div class="col-2" style="background-color:#C2DED1">
                    <center><b>
                        <p style="color:#354259; font-size:17px; padding:10px;">भू - समाधान गृह विभाग , बिहार सरकार</p></b>
                    </center>
         
                    <div class="col-12 text-center" style="background-color:darkslategray">
                        <a  style="text-decoration: none; color:white; display:block; border-radius: 8px;" class="collapse-item btn" href="#" onclick="window.history.go(-1); return false;">HOME <i class="fa fa-home" aria-hidden="true"></i></a>
                    </div>
                    <br />
                    <%--<a href="../Default.aspx">../Default.aspx</a>--%>
                    <div class="col-12">
                        <fieldset style="color:#354259; font-size:15px; padding:10px;">
                             <div class="form-control">
                                <label for="ddDistrict" style="height:30px;width:20%">जिला : </label>
                                <asp:DropDownList ID="ddDistrict" runat="server"  Enabled="true" style="height:30px;width:100%">
                                </asp:DropDownList>
                                
                            </div>
                            <div class="form-control">
                                <label for="ddDistrict" style="height:30px;width:100%">अंचल : </label>
                                <asp:DropDownList ID="ddBlock" runat="server"  Enabled="true" style="height:30px;width:100%">
                                </asp:DropDownList>
                                
                            </div>
                            <div class="form-control">
                                <label for="ddDistrict" style="height:30px;width:100%">पंचायत : </label>
                                <asp:DropDownList ID="ddPanchayat" runat="server"  Enabled="true" style="height:30px;width:100%">
                                </asp:DropDownList>                            
                            </div>
                            <div class="form-control">
                                <asp:Label ID="GramWard" runat="server" style="height:30px;width:100%">ग्राम/वार्ड : </asp:Label>                               
                                <asp:DropDownList ID="ddGramAWard" runat="server"  Enabled="true" style="height:30px;width:100%">
                                </asp:DropDownList>                           
                            </div>
                            <div class="form-control">
                                <label for="ddDistrict" style="height:30px;width:100%">सवेदनशीलता : </label>
                                <asp:DropDownList ID="ddSensivity" runat="server"  Enabled="true" style="height:30px;width:100%">
                                </asp:DropDownList>                           
                            </div>
                            <div class="form-control">
                            <label for="ddDistrict" style="height:30px;width:100%">बैठक का निष्कर्ष(Action) : </label>
                            <asp:DropDownList runat="server" ID="ddlaction" style="height:30px;width:100%" >
                                <asp:ListItem Value="0">--All--</asp:ListItem>
                                <asp:ListItem Value="1">प्रारंभिक निष्पादन</asp:ListItem>
                                <asp:ListItem Value="4">अस्वीकृत</asp:ListItem>
                                <asp:ListItem Value="2">मापी क़े लिए निर्धारित</asp:ListItem>
                                <asp:ListItem Value="3">प्रक्रियाधीन</asp:ListItem>
                                <asp:ListItem Value="5">अंतिम निष्पादन</asp:ListItem>
                            </asp:DropDownList>
                            
                            </div>
                            <%--<legend style="color:#354259; font-size:17px; padding:10px;font-weight:bold">भूमि विवाद का प्रकार </legend>--%>
                           
                           <%-- <div>
                                <input type="checkbox" id="chk1" name="chkLandDispute" value="1" onchange="BindMapData();"/>
                                <label for="chk1">पर्चाधारी के बेदखली का मामला</label>
                            </div>

                            <div>
                                <input type="checkbox" id="chk2" name="chkLandDispute" value="2" onchange="BindMapData();"/>
                                <label for="chk2">सरकारी (गैरमजरूआ) भूमि अतिक्रमण / कब्ज़ा का विवाद</label>
                            </div>
                            <div>
                                <input type="checkbox" id="chk3" name="chkLandDispute" value="3" onchange="BindMapData();"/>
                                <label for="chk3">रैयती भूमि पर सीमांकन या सीमा का विवाद</label>
                            </div>
                            
                         <div>
                                <input type="checkbox" id="chk4" name="chkLandDispute" value="4" onchange="BindMapData();"/>
                                <label for="chk4">निजी रास्ता / नाली का विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk5" name="chkLandDispute" value="5" onchange="BindMapData();"/>
                                <label for="chk5">जल स्रोत का विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk6" name="chkLandDispute" value="6" onchange="BindMapData();"/>
                                <label for="chk6">पैतृक/ मौरूषी (बंटवारा सहित) भूमि से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk7" name="chkLandDispute" value="7" onchange="BindMapData();"/>
                                <label for="chk7">खेती से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk8" name="chkLandDispute" value="8" onchange="BindMapData();"/>
                                <label for="chk8">वास से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk9" name="chkLandDispute" value="9" onchange="BindMapData();"/>
                                <label for="chk9">लगान निर्धारण का विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk10" name="chk10" value="10" onchange="BindMapData();"/>
                                <label for="chk10">व्यावसायिक भूमि से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk11" name="chkLandDispute" value="11" onchange="BindMapData();"/>
                                <label for="chk11">बदलेन भूमि से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk12" name="chkLandDispute"  value="12" onchange="BindMapData();"/>
                                <label for="chk12">भू- अर्जन से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk13" name="chkLandDispute"  value="13" onchange="BindMapData();"/>
                                <label for="chk13">भू- हदबंदी (अधिशेष) से संबंधित विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk15" name="chkLandDispute"  value="15" onchange="BindMapData();"/>
                                <label for="chk15">रैयती भूमि पर कब्ज़ा का विवाद</label>
                            </div>
                            
                            <div>
                                <input type="checkbox" id="chk20" name="chkLandDispute"  value="16" onchange="BindMapData();"/>
                                <label for="chk20">अन्य</label>
                            </div>--%>
                            
                           
                        </fieldset>
                    </div>
                </div>
                <div class="col-10">
                    <div class="row">
                        <div class="col-2">
                            From
                            <asp:TextBox TextMode="Date" ID="txtdatefrom" runat="server" CssClass="form-control" Text="2022-01-01"></asp:TextBox>                             
                        </div>
                        <div class="col-2">
                            To
                           <asp:TextBox TextMode="Date" ID="txtDateTo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-1">
                            <br />
                            <button type="button" id="btnSubmit" class="btn btn-primary" OnClick="displaymap()" width="100%">Search</button>                           
                        </div>                       
                         <div class="col-6">
                             </div>
                        <br/>
                        <br/>
                        <br/>
                        <div class="col-12" style="color:blue" id="direction">                            
                             </div>
                    </div>
                    <hr />
                    <div class="container-fluid">
                    <div class="row">
                        <div class="col-12">                            
                                <div class="col-12">
                                    <div id="containermap">
                                    </div>                                   
                                </div>                                                            
                                <br />
                                <div class="col-12">
                                    <div style="overflow:scroll; width:100%;">
                                        <div class="container-fluid" id="tabledata">
                                            <table id="LandDistibuteDataTable" class="table table-responsive table-hover"></table>
                                        </div>
                                    </div>
                                </div>
                            </div>                       
                    </div>
                    </div>
                </div>
            </div>
        </div>

    </form>
   <script src="js/jquery-3.5.1.min.js"></script>
    <script src="js/popper.min.js"></script>
    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/sidebars.js"></script>
    <script src="js/highmaps.js"></script>
   
    <script src="js/data.js"></script>
    <script src="js/drilldown.js"></script>

    <script src="js/exporting.js"></script>
    <script src="js/offline-exporting.js"></script>
    <script src="js/accessibility.js"></script>

    <script src="js/datatables.min.js"></script>
    <script src="Mapvillage.js"></script>    
</body>
</html>
