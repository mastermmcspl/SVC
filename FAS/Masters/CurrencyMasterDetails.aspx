<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="CurrencyMasterDetails.aspx.vb" Inherits="Masters_CurrencyMasterDetails" ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
        $('#<%=gvAgencycurrencyMaster.ClientID%>').DataTable({
            iDisplayLength: 5,
            aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
            order: [],
            columnDefs: [{ orderable: false, targets: [0, 1] }],
        });
    </script>

    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>6.4 Currency Master Details</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>
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
            <asp:Label ID="lblBank" runat="server" Text="* Bank"></asp:Label>
            <asp:DropDownList ID="ddlBankName" runat="server" AutoPostBack="true" CssClass="aspxcontrols" TabIndex="3">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblToCurrency" runat="server" Text="* Currency"></asp:Label>
            <asp:DropDownList ID="ddlToCurrency" runat="server" AutoPostBack="true" CssClass="aspxcontrols" TabIndex="3">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlToCurrency" runat="server" ControlToValidate="ddlToCurrency" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblfromCurrency" runat="server" Text="* Base Currency"></asp:Label>
            <asp:DropDownList ID="ddlfromcurrency" runat="server" CssClass="aspxcontrols" TabIndex="2">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblExchangeRate" runat="server" Text="* Unit of Foreign Currency"></asp:Label>
            <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="aspxcontrolsdisable" TabIndex="4" Enabled="true" Text="1"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:RadioButton ID="rboBank" runat="server" Text="Bank" GroupName="rbo" />
        </div>
        <div class="col-sm-6 col-md-6">
            <asp:RadioButton ID="rboWeb" runat="server" Text="Web" GroupName="rbo" />
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTTBuy" runat="server" Text="* TT Buy Rate"></asp:Label>
            <asp:TextBox ID="txtTTbuy" runat="server" CssClass="aspxcontrolsdisable" MaxLength="10" TabIndex="-1" Text="0"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTTSell" runat="server" Text="*  TT Sell Rate"></asp:Label>
            <asp:TextBox ID="txtTTsell" runat="server" CssClass="aspxcontrolsdisable" MaxLength="10" TabIndex="-1" Text="0"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblWTTBuy" runat="server" Text="* TT Buy Rate"></asp:Label>
            <asp:TextBox ID="txtWTTbuy" runat="server" CssClass="aspxcontrols" MaxLength="10" Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTTbuy" runat="server" ControlToValidate="txtWTTbuy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTTbuy" runat="server" ControlToValidate="txtWTTbuy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblWTTSell" runat="server" Text="*  TT Sell Rate"></asp:Label>
            <asp:TextBox ID="txtWTTsell" runat="server" CssClass="aspxcontrols" MaxLength="10" Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTTSell" runat="server" ControlToValidate="txtWTTsell" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTTSell" runat="server" ControlToValidate="txtWTTsell" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTBuy" runat="server" Text="* TC Buy Rate"></asp:Label>
            <asp:TextBox ID="txtBuy" runat="server" CssClass="aspxcontrolsdisable" MaxLength="10" TabIndex="-1" Text="0"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTSell" runat="server" Text="* TC Sell Rate"></asp:Label>
            <asp:TextBox ID="txtSell" runat="server" CssClass="aspxcontrolsdisable" MaxLength="10" TabIndex="-1" Text="0"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblWTBuy" runat="server" Text="* TC Buy Rate"></asp:Label>
            <asp:TextBox ID="txtWBuy" runat="server" CssClass="aspxcontrols" MaxLength="10" Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBuy" runat="server" ControlToValidate="txtWBuy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBuy" runat="server" ControlToValidate="txtWBuy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblWTSell" runat="server" Text="* TC Sell Rate"></asp:Label>
            <asp:TextBox ID="txtWSell" runat="server" CssClass="aspxcontrols" MaxLength="10" Text="0"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSell" runat="server" ControlToValidate="txtWSell" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSell" runat="server" ControlToValidate="txtWSell" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:RadioButton ID="rboAgency" runat="server" Text="Agency" GroupName="rbo" />
        </div>
        <div class="col-sm-6 col-md-6"></div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:GridView ID="gvAgencycurrencyMaster" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" AutoPostBack="true" OnCheckedChanged="ChkSelect_OnCheckedChanged" />
                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                        <asp:Label ID="lblFEID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "FEID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                <asp:BoundField DataField="AgencyName" HeaderText="Agency" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="26%"></asp:BoundField>
                <asp:TemplateField HeaderText="TT Buy" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblTTBuy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TTBuy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TT Sell" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblTTSell" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TTSell") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TC Buy" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblTBuy" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TBuy") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TC Sell" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblTSell" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TSell") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="BankName" HeaderText="Bank" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="22%"></asp:BoundField>
            </Columns>
        </asp:GridView>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

