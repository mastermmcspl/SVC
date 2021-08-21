<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="BankCurrencyDetails.aspx.vb" Inherits="Masters_BankCurrencyDetails" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }

        div {
            color: black;
        }
    </style>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>6.3 Bank Currency Master Details</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label ID="lblBank" runat="server" Text="Bank :-"></asp:Label>
            <asp:Label ID="lblBankName" runat="server" CssClass="aspxlabelbold"></asp:Label>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblToCurrency" runat="server" Text="* Currency"></asp:Label>
            <asp:DropDownList ID="ddlToCurrency" runat="server" AutoPostBack="true" CssClass="aspxcontrols" TabIndex="3">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlToCurrency" runat="server" ControlToValidate="ddlToCurrency" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblfromCurrency" runat="server" Text="* Base Currency"></asp:Label>
            <asp:DropDownList ID="ddlfromcurrency" runat="server" Enabled="false" CssClass="aspxcontrols" TabIndex="2">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblExchangeRate" runat="server" Text="* Unit of Foreign Currency"></asp:Label>
            <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="aspxcontrolsdisable" TabIndex="4" Enabled="true" Text="1"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTTBuy" runat="server" Text="* TT Buy Rate"></asp:Label>
            <asp:TextBox ID="txtTTbuy" runat="server" CssClass="aspxcontrols" MaxLength="10" TabIndex="5" Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTTbuy" runat="server" ControlToValidate="txtTTbuy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTTbuy" runat="server" ControlToValidate="txtTTbuy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTTSell" runat="server" Text="*  TT Sell Rate"></asp:Label>
            <asp:TextBox ID="txtTTsell" runat="server" CssClass="aspxcontrols" MaxLength="10" TabIndex="6" Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTTSell" runat="server" ControlToValidate="txtTTsell" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTTSell" runat="server" ControlToValidate="txtTTsell" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTBuy" runat="server" Text="* TC Buy Rate"></asp:Label>
            <asp:TextBox ID="txtBuy" runat="server" CssClass="aspxcontrols" MaxLength="10" TabIndex="7" Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBuy" runat="server" ControlToValidate="txtBuy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBuy" runat="server" ControlToValidate="txtBuy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTSell" runat="server" Text="* TC Sell Rate"></asp:Label>
            <asp:TextBox ID="txtSell" runat="server" CssClass="aspxcontrols" MaxLength="10" TabIndex="8" Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSell" runat="server" ControlToValidate="txtSell" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSell" runat="server" ControlToValidate="txtSell" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS Pro</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="btnOk">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

