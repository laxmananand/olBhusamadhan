<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlAction.ascx.cs" Inherits="WebUserControlAction" %>
                  <asp:Repeater ID="parentRepeater" runat="server" OnItemDataBound="parentRepeater_ItemDataBound">  
                    <HeaderTemplate>                                                                  
                                                         
                    </HeaderTemplate>  
                    <ItemTemplate> 
                        <asp:HiddenField ID="lbla_idc" runat="server" value='<%#Eval("a_id") %>'></asp:HiddenField>
                         <%--अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण--%>
                                                        <br/>
                                                         <div class="row">
                                                               <div class="col-md-12">
                                                                    <div class="panel panel-default">
                                                                          <div class="panel-heading">
                                                                                  <div class="card-header text-black" style="background-color: #D8D8D8; height: 35px;font-size:20px">
                                                                                         <p>अंचलाधिकारी एवं थानाध्यक्ष द्वारा भूमि विवाद के निराकरण हेतु कृत कारवाई का विवरण</p>
                                                                               </div>
                                                                          </div>
                                                                          <div class="panel-body">
                                                                                <br/> 
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div7" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       विवाद की संवेदनशीलता
                                                                                          </div>
                                                                                         <div class="col-md-3"  style="font-size:12px">
                                                                                                    <asp:Label id="lblVivaadKiSanvedanasheelata" runat="server" Text=""></asp:Label>
                                                                                         </div>                                                                                  
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                     <div class="col-md-3" id="div12" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       बैठक की तिथि
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label id="lblBaithakKiTithi" runat="server" Text=""></asp:Label>
                                                                                         </div>
                                                                               </div>                                                                                
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div13" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       क्या वादी उपस्थित है ? 
                                                                                          </div>
                                                                                         <div class="col-md-3" style="font-size:12px">
                                                                                              <asp:Label id="lblkyaVaadeeUpasthitHai" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                <div>
                                                                                <div class="row">
                                                                                           <div class="col-md-3" id="div15" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                      क्या प्रतिवादी उपस्थित है ?
                                                                                          </div>
                                                                                         <div class="col-md-3"  style="font-size:12px">
                                                                                              <asp:Label id="lblKyaPrativaadeeUpasthitHai" runat="server" Text=""></asp:Label>
                                                                                         </div>
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                          <div class="col-md-3" id="div9" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                    बैठक का निष्कर्ष 
                                                                                           </div>
                                                                                          <div class="col-md-6" style="font-size:12px">
                                                                                               <asp:Label id="lblBaithakKaNishkarsh" runat="server"  Text=""></asp:Label>
                                                                                          </div>
                                                                                <div>
                                                                                 <div class="row" id="divtithi" runat="server" >
                                                                                          <div class="col-md-3" style="font-weight: bold;font-size:14px">
                                                                                                    <asp:Label id="lbltithi" runat="server" Text=""></asp:Label>
                                                                                           </div>
                                                                                          <div class="col-md-3"  style="font-size:12px">
                                                                                               <asp:Label id="lbltithivalue" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div>
                                                                                  <div class="row" id="divAsveekrtiKaKaaranLabel" runat="server">
                                                                                          <div class="col-md-3"  style="font-weight: bold;font-size:14px">
                                                                                                    अस्वीकृति का कारण
                                                                                           </div>
                                                                                          <div class="col-md-9" id="divAsveekrtiKaKaaran" runat="server" style="font-size:12px">
                                                                                               <asp:Label id="lblAsveekrtiKaKaaran" runat="server" Text=""></asp:Label>
                                                                                          </div>
                                                                                 </div>    
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div14" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       बैठक में लिया गया निर्णय
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label  id="lblBaithakMeinLiyaGayaNirnay" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                </div>
                                                                                    <div class="row">
                                                                                           <div class="col-md-3" id="div17" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       अंचलाधिकारी का मंतव्य 
                                                                                          </div>
                                                                                         <div class="col-md-9" style="font-size:12px">
                                                                                              <asp:Label id="lblAnchalaadhikaareeKaMantavy" runat="server"  Text=""></asp:Label>
                                                                                         </div>
                                                                                 </div>  
                                                                                 <div class="row">
                                                                                         <div class="col-md-3" id="div16" runat="server" style="font-weight: bold;font-size:14px">
                                                                                                       थानाध्यक्ष का मंतव्य 
                                                                                          </div>
                                                                                         <div class="col-md-9"  style="font-size:12px">
                                                                                              <asp:Label id="lblThaanaadhyakshKaMantavy" runat="server" Text=""></asp:Label>
                                                                                         </div>                                                                        
                                                                                 </div>                                                              
                                                                          </div>                                                                  
                                                                    </div>
                                                               </div>
                                                           </div>
                                                                         </div>
                                                                      </div>
                                                               </div>
                                                        </div>

                        </ItemTemplate>
                        </asp:Repeater>
