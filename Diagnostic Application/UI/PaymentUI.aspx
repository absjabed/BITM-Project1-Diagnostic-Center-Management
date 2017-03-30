﻿<%@ Page Language="C#" Title="Payment" MasterPageFile="../master.Master" AutoEventWireup="true" CodeBehind="PaymentUI.aspx.cs" Inherits="Diagnostic_Application.UI.PaymentUI" %>

<asp:Content ID="contentPayment" ContentPlaceHolderID="MainContent" runat="server">

    <form id="form1" class="form-horizontal" runat="server">
        <div class="row">
            <h3>Pay Bill</h3>
            <asp:Label ID="InfoMessageLabel" CssClass="infoMessage" runat="server" Visible="false" Text=""></asp:Label>
            <div class="col-lg-8">
                <!-- type setup -->
                <div class="type-setup">
                    <div class="row">
                        <div class="col-lg-9">
                            <div class="form-group">
                                <asp:Label ID="TestNameLabel" runat="server" CssClass="col-sm-3 control-label" Text="Bill No"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="BillNoTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                                <div>
                                <br/>
                                <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 control-label" Text="OR"></asp:Label>
                                <br/>    
                                </div>
                                    <asp:Label ID="MobileNoLabel" runat="server" CssClass="col-sm-3 control-label" Text="Mobile No"></asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="MobileNoTextBox" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-9">
                                    <asp:Button ID="SearchButton" runat="server" CssClass="btn btn-primary right" Text="Search" OnClick="SearchButton_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- type setup -->
            </div>
            <div class="col-lg-4"></div>
        </div>
        
        
        <br/>
        <hr/>
        <br/>
       
        <%--<div>
            <table class="table table-payment">
                <tbody>
                    <tr>
                        <td><label class="control-label">Bill Date</label></td>
                        <td><asp:Label ID="BillDateLabel" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    
                    <tr>
                        <td><label class="control-label">Total Fee</label></td>
                        <td><asp:Label ID="TotalFeeLabel" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    
                    <tr>
                        <td><label class="control-label">Paid Amount</label></td>
                        <td><asp:Label ID="PaidAmountLabel" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    
                    
                    <tr>
                        <td><label class="control-label">Due Amount</label></td>
                        <td><asp:Label ID="DueAmountLabel" runat="server" Text="N/A"></asp:Label></td>
                    </tr>
                    
                </tbody>
              </table>
        </div>--%>

        
        <div class="row">
            <asp:Label ID="Label1" CssClass="infoMessage" runat="server" Visible="false" Text=""></asp:Label>
            <div class="col-lg-8">
                <!-- type setup -->
                <div class="type-setup">
                    <div class="form-group">
                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 control-label" Text="Amount"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="AmountTextBox" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <asp:Label ID="Label" runat="server" CssClass="col-sm-3 control-label" Text="Due Date:"></asp:Label>
                        <div class="col-sm-9">
                            <asp:TextBox ID="DueDateTextBox" Enabled="False" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                

                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <asp:CheckBox ID="item_check" Text="Are you sure to pay the unpaid bill?" runat="server" OnCheckedChanged="item_check_CheckedChanged" AutoPostBack="True" ForeColor="Red" />
                            <asp:Button ID="PaymentButton" runat="server" CssClass="btn btn-success right" Text="Pay" OnClick="PaymentButton_Click" />
                            
                        </div>
                    </div>
                </div>
                <!-- type setup -->
            </div>
            <div class="col-lg-4"></div>
        </div>

    </form>
</asp:Content>