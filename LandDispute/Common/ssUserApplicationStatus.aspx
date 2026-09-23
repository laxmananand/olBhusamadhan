<%@ Page Language="C#" MasterPageFile="~/SOFT/Main.master" AutoEventWireup="true" CodeFile="ssUserApplicationStatus.aspx.cs" Inherits="LDHD_UserApplicationStatus"  EnableEventValidation="false" %>
<%@ Register Assembly="RJS.Web.WebControl.PopCalendar" Namespace="RJS.Web.WebControl"
    TagPrefix="rjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<%--<meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />--%>
   
     <link href="assets/css/bootstrap.min.css" rel="Stylesheet" type="text/css" />
     <script src="assets/js/jquery.min.js" type="text/javascript"></script>
     <script src="assets/js/bootstrap.min.js" type="text/javascript" ></script>
    <style type="text/css" media="screen">
        .print
        {
            background-color: #fff;
        }
        .printdraft img
        {
            visibility: hidden;
        }
    </style>
    <style type="text/css" media="print">
        .printdraft img
        {
            visibility: visible;
        }
    </style>
    <style type="text/css" media="all">
        .printfinal img
        {
            visibility: hidden;
        }
    </style>
    <style type="text/css" media="print">
        .pageBreak
        {
            page-break-before: always;
        }
    </style>
    <style type="text/css">
        .grid td, th
        {
            padding: 4px;
        }
        
        
        
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
        
        
        .textbox
        {
            
            margin-bottom:0px;
            margin-top:5px;
        }
        /*FIELD SET CLASS*/
        fieldset1
        {
            padding:3px;
        }
        fieldset.scheduler-border {
    border: 1px groove #ddd !important;
    /*padding: 0 1.4em 1.4em 1.4em !important;*/
    padding: .1em 1em 1em 1em !important;
    margin: 0 0 1.0em 0 !important;
    -webkit-box-shadow:  0px 0px 0px 0px #000;
            box-shadow:  0px 0px 0px 0px #000;
}

    legend.scheduler-border {
        font-size: 1.2em !important;
        font-weight: bold !important;
        text-align: left !important;
        width:auto;
        padding:0 2px;
        border-bottom:none;
        margin-bottom:2px;
    }
        
     .cert
     {
         padding:1px!Important;
     }   
     
     
@media print {
  a[href]:after {
    content: none !important;
  }
}     
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        <div class="container">
        <%--<form id="form1" runat="server">--%>
            

             <hr />

            <asp:Panel ID="PnlGrid" runat="server" Visible="true" >
             <div class="row">
            <div class="col-lg-12">
           <%--<asp:Panel ID="PnlGrid" runat="server" Visible="true" Height="500px" Width="100%">--%>

        <div class="row">
            
            <fieldset class="scheduler-border fieldset1">
                <legend class="scheduler-border">Personal Details</legend>
                <div class="control-group  mt-1">
                <div class="row">
                    <div class="col-sm-4">
                        <label class="control-label input-label textbox" for="startTime">Applicant's Name</label>
                        <div id="dvStudentName" runat="server"></div>
                    </div>
                    <div class="col-sm-4">
                        <label class="control-label input-label textbox" for="startTime">Father's/Husband Name</label>
                        <div id="dvFather" runat="server"></div>
                    </div>
                    <div class="col-sm-4">
                    <label class="control-label input-label textbox" for="startTime">Mother's Name</label>
                        <div  id="dvMother" runat="server"></div>
                    </div>
                </div>
                 <div class="row">
                    <div class="col-sm-4">
                        <label class="textbox"  for="startTime">Date Of Birth</label>
                        <div id="dvDob" runat="server"></div>
                    </div>
                    <div class="col-sm-4">
                        <label class="control-label input-label textbox" for="startTime">Gender</label>
                        <div id="dvGender" runat="server"></div>
                    </div>
                     <%--<div class="col-sm-4">
                   <label class="control-label input-label textbox" for="startTime">Category</label>
                        <div  id="dvCategory" runat="server"></div>
                    </div>--%>
                     <div class="col-sm-2">
                        <label class="control-label input-label textbox" for="startTime">Mobile Number</label>
                        <div id="dvMobile" runat="server"></div>
                    </div>
                </div>
                 <div class="row">
                     <div class="col-sm-2">
                    <label class="control-label input-label textbox" for="startTime">Aadhar Number</label>
                        <div id="dvAadhar" runat="server"></div>
                    </div>
                    <div class="col-sm-4">
                        <%--<label class="control-label input-label textbox" for="startTime">Email Id</label>
                        <div  id="dvEmail" runat="server"></div>--%>
                    </div>
                    <div class="col-sm-2">
                        <%--<label class="control-label input-label textbox" for="startTime">Mobile Number</label>
                        <div id="dvMobile" runat="server"></div>--%>
                    </div>
                    
                   <div class="col-sm-2">

                     <%--<label class="control-label input-label textbox" for="startTime">Is Disabled</label>
                        <div id="dvHandicap" runat="server"></div>--%>
                    </div>
                    <div class="col-sm-2">
                    <%--<label class="control-label input-label textbox" for="startTime">Stay in Institution Hostel</label>
                        <div id="dvHostel" runat="server"></div>--%>
                    </div>
                </div>
                </div>
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="scheduler-border fieldset1">
                <legend class="scheduler-border">Address Details</legend>
                <div class="control-group  mt-1">
                <div class="row">
                 <div class="col-sm-4">
                        <div><b>District:</b>&nbsp;<span  id="dvHomeDist" runat="server"></span></div>
                        <div><b>Block:</b>&nbsp;<span  id="dvHomeBlock" runat="server"></span></div>
                        <div><b>Police Station:</b>&nbsp;<span  id="dvPoliceStation" runat="server"></span></div>
                    </div>
                    <div class="col-sm-4">
                        
                        <div><b>Panchayat Name:</b>&nbsp;<span  id="dvPanchayatName" runat="server"> </span></div>
                        <div><b>Area Type:</b>&nbsp;<span  id="dvAreaType" runat="server"></span></div>
                        <div><b>Village:</b>&nbsp;<span  id="dvVillage" runat="server"></span></div>
                    </div>
                    <div class="col-sm-4">
                        <div><b>Ward Name:</b>&nbsp;<span  id="dvWard" runat="server"></span></div>
                        <label class="control-label input-label textbox" for="startTime">Address</label>
                        <div  id="dvPerAdd" runat="server"></div>
                    </div>
                   
                </div>
               
                </div>
            </fieldset>
        </div>
                <%--
        <div class="row">
            <fieldset class="scheduler-border fieldset1">
                <legend class="scheduler-border">Institution Details</legend>
                <div class="control-group  mt-1">
                <div class="row">
                 <div class="col-sm-4">
                        <div><b>State:</b>&nbsp;<span  id="dvInstState" runat="server"></span></div>
                        <div><b>District:</b>&nbsp;<span  id="dvInstDist" runat="server"></span></div>
                        <div><b>Block:</b>&nbsp;<span  id="dvInstBlock" runat="server"></span></div>
                        <div><b>Pincode:</b>&nbsp;<span  id="dvInstPin" runat="server"></span></div>
                        <div><b>College Compulsory Fee:</b>&nbsp;<span  id="dvCollegeCompFee" runat="server"></span></div>
                          <div><b>Qualified Last Year Exam<br /> as Per Exam Norms(Credit System):</b>&nbsp;<span  id="dvCreditSystem" runat="server"></span></div>
                    </div>
                    <div class="col-sm-4">
                        <label class="control-label input-label textbox" for="startTime">Institute</label>
                        <div id="dvCollege" runat="server"></div>
                       <div><b>Address</b></div>
                       <div id="dvCollegeAdd" runat="server"></div>
                         <div><b> Course Fee in Govt.<br /> College of state: </b>&nbsp;<span  id="dvCourseFeeGovtCollege" runat="server"></span></div>
                    </div>
                    <div class="col-sm-4">
                        <div><b>Reg. No: </b>&nbsp;<span  id="dvStdReg" runat="server"></span></div>
                        <div><b>Section: </b>&nbsp;<span  id="dvStdSection" runat="server"></span></div>
                        <div><b>Course: </b>&nbsp;<span  id="dvStdCourse" runat="server"></span></div>
                        <div><b>Acedemic Year: </b>&nbsp;<span  id="dvStdFyear" runat="server"></span></div>
                          <div><b>Stay in Institution Hostel:</b>&nbsp;<span  id="dvStayInHostel" runat="server"></span></div>
                    </div>
                   
                </div>
               
                </div>
            </fieldset>
        </div>--%>
        <div class="row">
            <fieldset class="scheduler-border fieldset1">
                <legend class="scheduler-border">Land Details</legend>
               
                <div class="row">
                    <div class="col-sm-4">
                        <div><b>khata No: </b>&nbsp;<span  id="dvkhataNo" runat="server"></span></div>
                        <div><b>khesra No: </b>&nbsp;<span  id="dvkhesraNo" runat="server"></span></div>
                    </div>
                    <div class="col-sm-4">
                        <div><b>Land Dispute Type: </b></div>
                        <div  id="dvLandDisputeType" runat="server"></div>
                        <div><b>Sensitivity: </b>&nbsp;<span  id="dvSensitivity" runat="server"></span></div>
                    </div>
                    <div class="col-sm-4">
                        <label class="control-label input-label textbox" for="startTime">Land Dispute Description:</label>
                        <div  id="dvLandDisputeDescription" runat="server"></div>
                    </div>
                </div>
              
               
            </fieldset>
        </div>
        <div class="row">
            <fieldset class="scheduler-border fieldset1">
                <legend class="scheduler-border">Application Status</legend>
               
                 <div class="row">
                    <div class="col-sm-4">
                       <div><b>Application ID: </b>&nbsp;<span  id="DvApplicationId" runat="server"></span></div>
                    </div>
              
                    <%--<div class="col-sm-3">
                         <div><b>Matter Status: </b>&nbsp;<span  id="dvInstStatus" runat="server"></span></div>
                         <div><b>Remarks: </b>&nbsp;<span  id="dvInstStatusRemarks" runat="server"></span></div>
                    </div>--%>
                    <div class="col-sm-4">
                        <div><b>Application Status: </b>&nbsp;<span  id="dvMatterStatus" runat="server"></span></div>
                        <div><b><span  id="dvdateOnTitle" runat="server">Resolved On: </span></b>&nbsp;&nbsp;<span  id="dvOndate" runat="server"></span></div>
                       <div><b>Remarks: </b>&nbsp;<span  id="dvStatusRemarks" runat="server"></span></div>
                       
                    </div>
                    <div class="col-sm-4">
                      <div><b>Application On: </b>&nbsp;<span  id="dvSentTo" runat="server"></span></div>
                      <%--<div><b>Remarks: </b>&nbsp;<span  id="dvVerifierRemarks" runat="server"></span></div>--%>
                    </div>
                    </div>
                      
                      
                    <%--<div class="row">
                        <div class="col-sm-4">
                            <div><b>Committee Status: </b>&nbsp;<span  id="dvCommitteeStatus" runat="server"></span></div>
                            <div><b>Remarks: </b>&nbsp;<span  id="dvCommitteeStatusRemarks" runat="server"></span></div>
                        </div>
                    </div>--%>
              
               
            </fieldset>
        </div>

        <%--<div class="row">
            <fieldset class="scheduler-border fieldset1">
                <legend class="scheduler-border">Application Status</legend>
               
                 <div class="row">
                    <div class="col-sm-4">
                         <div><b>Application Id: </b>&nbsp;<span  id="DvApplicationId" runat="server"></span></div>
                    </div>
              
                    <div class="col-sm-4">
                         <div><b>Institution Status: </b>&nbsp;<span  id="dvCollegeStatus" runat="server"></span></div>
                    </div>
                    <div class="col-sm-4">
                        <div><b>Physical Verification: </b>&nbsp;<span  id="dvTPAStatus" runat="server"></span></div>
                       
                    </div>
                    </div>
                      <div class="row">
                    <div class="col-sm-4">
                    <div><b>Committee Status: </b>&nbsp;<span  id="dvCommitteeStatus" runat="server"></span></div>
                  
                    </div>
                </div>
              
               
            </fieldset>
        </div>--%>

        <div class="row">
            <fieldset class="scheduler-border fieldset1  mt-1">
                <legend class="scheduler-border">Uploaded Documents Details</legend>
               
                <div class="row">
                    <div class="col-sm-2"  id="dlnkTenthCertNo" runat="server" style="display:none;margin:2px;" >
                        <a  id="lnkTenthCertNo" runat="server" href="#" class="btn btn-primary cert"  visible="false">10th Certificate</a>
                    </div>       
                    <div class="col-sm-2"  id="dlnkTwelfthDoc" runat="server"  style="display:none;margin:2px;">
                        <a  id="lnkTwelfthDoc" runat="server" href="#" class="btn btn-primary cert" visible="false">12th Certificate</a>
                    </div>       
                    <div class="col-sm-2"  id="dlnkGraduationDoc" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkGraduationDoc" runat="server" href="#" class="btn btn-primary cert" visible="false">Graduate Certificate</a>   
                    </div>       
                    <div class="col-sm-2" id="dlnkPostGraduationDoc" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkPostGraduationDoc" runat="server" href="#" class="btn btn-primary cert " visible="false">Post Graduate Certificate</a>  
                    </div>       

                    <div class="col-sm-2" id="dlnkDiplomaDoc" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkDiplomaDoc" runat="server" href="#" class="btn btn-primary cert" visible="false">Diploma Certificate</a>  
                    </div>       
                    <div class="col-sm-2"  id="dlnkPGDiplomaDoc" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkPGDiplomaDoc" runat="server" href="#" class="btn btn-primary cert " visible="false">PG Diploma Certificate</a> 
                    </div>       
                    <div class="col-sm-2" id="dlnkDomicalCertificate" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkDomicalCertificate" runat="server" href="#" class="btn btn-primary cert" visible="false">Domicile Certificate</a> 
                    </div>       
                    <div class="col-sm-2"  id="dlnkIncomeCertificate" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkIncomeCertificate" runat="server" href="#" class="btn btn-primary cert" visible="false">Income Certificate</a> 
                    </div>       
                    <div class="col-sm-2" id="dlnkCastCertificate" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkCastCertificate" runat="server" href="#" class="btn btn-primary cert" visible="false">Cast Certificate</a> 
                    </div>       
                    <div class="col-sm-2" id="dlnkBonafideDoc" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkBonafideDoc" runat="server" href="#" class="btn btn-primary cert" visible="false">Bonafide Certificate</a> 
                    </div>
                    <div class="col-sm-2" id="dlnkFeeReceiptDoc" runat="server" style="display:none;margin:2px;">
                        <a  id="lnkFeeReceiptDoc" runat="server" href="#" class="btn btn-primary cert" visible="false">Fee Receipt Certificate</a> 
                    </div>       
                    <div class="col-sm-2" id="dlnkLastExamPassCertificateDoc" runat="server" style="display:none; margin:2px;">
                        <a  id="lnkLastExamPassCertificateDoc" runat="server" href="#" class="btn btn-primary cert"  visible="false">Last Exam Certificate</a> 
                    </div>       

                    
              
                    
                </div>
              
               
            </fieldset>
        </div>
       <div class="row m-0">
        <a href="#" class="btn btn-success" onclick="javascript:this.style.visibility ='hidden'; window.print();this.style.visibility ='visible';">Print   </a>
       </div>
       
        <%--</form>--%>
        
    <%--</asp:Panel>--%>
            </div>
            </div>
            </asp:Panel>
            </div>
</asp:Content>
