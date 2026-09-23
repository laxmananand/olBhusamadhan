<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="getData.aspx.cs" Inherits="LandDispute_Entry_getData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container-fluid">
         <div class="card mb-3">
             <br/>
              <div class="card-header text-center" style="font-size: 18px"><b><u>आवेदन का विवरण प्राप्त करे</u></b></div>
              <br/>
              <div class="row mb-2">
                  <div class="col-md-2"></div>
                  <div class="col-md-2"></div>
                    <div class="col-md-1"></div>
                    
                   <div class="col-md-2">
                       <center>                                                                     
                         <asp:Button ID="btnGetData" OnClick="btnGetData_Click"  runat="server" Text="Display your Application" CssClass="btn btn-success" >
                          </asp:Button>                          
                       </center>
                   </div>
                  <div class="col-md-1">   
                       <center>      
                          <asp:Button ID="btnCancel" OnClick="btnCancel_Click"  runat="server" Text="Go To Home Page" CssClass="btn btn-primary" >
                          </asp:Button>
                        </center>
                  </div>
                   
                  <div class="col-md-3">                   
                  </div>
              </div>
         </div>
     </div>
</asp:Content>

