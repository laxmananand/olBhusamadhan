<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DefaultHome.aspx.cs" Inherits="RaC_DefaultHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h4 class="text-center">Dashboard</h4>
                </div>
            </div>
            <h4 style="text-align:center; background-color:#6C9BCF; padding:5px; color:white;box-shadow: rgba(0, 0, 0, 0.12) 0px 1px 3px, rgba(0, 0, 0, 0.24) 0px 1px 2px;">आवेदन</h4>
            <div class="row">
                <div class="col-md-4">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">कुल आवेदन</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">                            
                              <center><a href="~/LandDispute/Reports/Consolidate/SearchApplicationwise.aspx" id="TotalApplication1" runat="server"><asp:Label ID="lbltotalapplication1" runat="server" Text="458" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                              <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateDashboard.aspx" id="TotalApplication2" runat="server"><asp:Label ID="lbltotalapplication2" runat="server" Text="458" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">पूर्ण प्रविष्टि</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            
                              <center><a href="~/LandDispute/Entry/Finalize.aspx" id="Finalize1" runat="server"><asp:Label ID="lblFinalize1" runat="server" Text="458" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                              <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateDashboard.aspx" id="Finalize2" runat="server"><asp:Label ID="lblFinalize2" runat="server" Text="458" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">आंशिक प्रविष्टि</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            
                             <center><a href="~/LandDispute/Entry/Unfinalize.aspx" id="UnFinalize1" runat="server"><asp:Label ID="lblUnFinalize1" runat="server" Text="458" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                              <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateDashboard.aspx" id="UnFinalize2" runat="server"><asp:Label ID="lblUnFinalize2" runat="server" Text="458" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <h4 style="text-align:center; background-color:#6C9BCF; padding:5px; color:white;box-shadow: rgba(0, 0, 0, 0.12) 0px 1px 3px, rgba(0, 0, 0, 0.24) 0px 1px 2px;">संवेदनशीलता</h4>
            <div class="row">
                <div class="col-md-4">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">अति संवेदनशील</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/DistrictSensitivityType.aspx"><asp:Label ID="lblatiSavedansheel" runat="server" Text="125" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">संवेदनशील</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/DistrictSensitivityType.aspx"><asp:Label ID="lblsavedansheel" runat="server" Text="458" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">सामान्य</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/DistrictSensitivityType.aspx"><asp:Label ID="lblsamanya" runat="server" Text="365" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <h4 style="text-align:center; background-color:#6C9BCF; padding:5px; color:white; box-shadow: rgba(0, 0, 0, 0.12) 0px 1px 3px, rgba(0, 0, 0, 0.24) 0px 1px 2px;">स्थिति</h4>
            <div class="row">
                
                <div class="col-md-2">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">प्रारंभिक निष्पादन</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx"><asp:Label ID="lblnispadan" runat="server" Text="125" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">अंतिम निष्पादन</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx"><asp:Label ID="lblFinaldisposal" runat="server" Text="458" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">प्रक्रियाधीन</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx"><asp:Label ID="lblprakreeyadheen" runat="server" Text="365" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">मापी के लिए निर्धारित</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx"><asp:Label ID="lblmapikenirdharit" runat="server" Text="365" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">अस्वीकृत</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx"><asp:Label ID="lblashvikrit" runat="server" Text="365" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="card mb-2">
                            <p class="text-center" style="color:black; font-size:15px; font-weight:700">न्यायालय में लंबित</p>
                        <div class="card-body" style="box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;">
                            <center><a href="LandDispute/Reports/Consolidate/ApplicationDistConsolidateRpt.aspx"><asp:Label ID="lblNaylayNilambit" runat="server" Text="365" style="font-size:35px;font-weight:700;color:darkblue"></asp:Label></a></center>
                        </div>
                    </div>
                </div>


            </div>
        </div>
    <br />
</asp:Content>

