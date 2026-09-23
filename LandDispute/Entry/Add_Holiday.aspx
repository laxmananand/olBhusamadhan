<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Add_Holiday.aspx.cs" Inherits="LandDispute_Entry_Add_Holiday" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #btnSend:hover {
            background: linear-gradient(90deg, #2f8f8b, #5a5f9c);
            color: #ffffff;
        }
    </style>
    <style>
        .form-control-custom {
            border: 1px solid #cfd8dc;
            border-radius: 4px;
            height: 38px;
            padding: 6px 10px;
            font-size: 14px;
        }

            .form-control-custom:focus {
                border-color: #5a5f9c;
                box-shadow: 0 0 5px rgba(90, 95, 156, 0.5);
                outline: none;
            }

        .label-custom {
            font-weight: 500;
            color: #444;
            margin-bottom: 4px;
            display: block;
            text-align: left;
        }
    </style>
    <div class="container">

        <%-- <asp:HiddenField ID="HidKey3" runat="server" />--%>
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-6">
                <asp:Panel ID="Panel1" runat="server">
                    <div class="panel">
                        <div class="panel-heading"
                            style="margin-bottom: -8px; background: linear-gradient(90deg, #3aa7a3, #6b6fae);">
                            <h4 style="color: #ffffff; text-align: center; height: 42px; padding-top: 7px;">Add Holiday
                            </h4>
                        </div>

                        <%-- <div class="panel-heading bg-success" style="margin-bottom: -8px;">
                        <h4 style="color: #ffffff; text-align: center; height: 42px; padding-top: 7px;">Add Holiday</h4>
                    </div>--%>
                        <div class="panel-body" style="box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;">
                            <%--start--%>
                            <div class="row" style="padding: 15px 10px;">
                                <div class="col-lg-12 col-md-12 col-sm-12">

                                    <!-- Holiday Date -->
                                    <div class="form-group">
                                        <label class="label-custom">Holiday Date</label>
                                        <asp:TextBox ID="txtHolidayDate" runat="server"
                                            CssClass="form-control form-control-custom"
                                            Placeholder="Enter Holiday Date (DD/MM/YYYY)"
                                            TextMode="Date">
                                        </asp:TextBox>
                                    </div>

                                    <!-- Remark -->
                                    <div class="form-group" style="margin-top: 12px;">
                                        <label class="label-custom">Reason for Holiday</label>
                                        <asp:TextBox ID="txtRemark" runat="server"
                                            CssClass="form-control form-control-custom"
                                            Placeholder="Reason for Holiday"
                                            TextMode="MultiLine"
                                            Rows="3">
                                        </asp:TextBox>
                                    </div>

                                </div>
                            </div>

                            <%--  end--%>

                            <div class="row" style="padding-bottom: 1.5%; padding-top: 1.5%; padding-left: 0.5%">
                                <div class="col-lg-12 col-md-12 col-sm-12" align="center">
                                    <div class="col-lg-6 col-md-6 col-sm-6" align="center">
                                        <asp:Button ID="btnSend" runat="server"
                                            Text="AddHoliday"
                                            CssClass="btn"
                                            Width="100px"
                                            OnClick="btnSend_Click"
                                            Style="background: linear-gradient(90deg, #3aa7a3, #6b6fae); border: none; color: #ffffff; font-weight: 500;" />
                                        <%--<asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary" Width="100px" OnClick="btnSend_Click" Style="background-color: #66bb6a; border-color: #66bb6a" />--%>
                                    </div>
                                    <%--Style="width: 100px; background-color: #357ebd; padding-right: 11px; margin-right: -197px;"--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </asp:Panel>


            </div>
            <div class="col-md-3"></div>
        </div>
    </div>
</asp:Content>

