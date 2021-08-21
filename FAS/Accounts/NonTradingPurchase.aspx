<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="NonTradingPurchase.aspx.vb" Inherits="Accounts_NonTradingPurchase" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
    </style>

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
            $('#<%=ddlExistPurchase.ClientID%>').select2();
            $('#<%=ddlParty.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Non Trading Purchase Transactions Details</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnSubmit" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Submit" />
                    <asp:ImageButton ID="imgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="ValidateApprove" />

                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnWord" Text="Download Word" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px; left: 0px; top: 0px;">
        <div class="col-sm-12 col-md-12" style="left: 0px; top: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>


    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Existing Purchase Voucher"></asp:Label>
                <asp:DropDownList ID="ddlExistPurchase" runat="server" CssClass="aspxcontrols" AutoPostBack="True" ValidationGroup="Validate"></asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Transaction No"></asp:Label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Date"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTransactionDate" runat="server" ControlToValidate="txtTransactionDate" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTransactionDate" runat="server" ControlToValidate="txtTransactionDate" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>

                <cc1:CalendarExtender ID="cclTransactionDate" runat="server" PopupButtonID="txtTransactionDate" PopupPosition="BottomLeft"
                    TargetControlID="txtTransactionDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%-- <asp:RangeValidator ID="rgvtxtReceiptDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtReceiptDate" SetFocusOnError="True"></asp:RangeValidator>--%>
            </div>

            <div class="col-sm-3 col-md-3 pull-right">
                <asp:Label runat="server" Text="Status:- " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
                <asp:Label ID="lblTransID" runat="server" CssClass="aspxcontrols" Visible="false"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblParty" Text="Supplier" runat="server"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVParty" runat="server" ControlToValidate="ddlParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Supplier" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Company Type"></asp:Label>
                <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="GSTN Category"></asp:Label>
                <asp:DropDownList ID="ddlGSTCategory" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Payment Type"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFvddlPaymentType" runat="server" ControlToValidate="ddlPaymentType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Payment Type" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="aspxcontrols" AutoPostBack ="true" ></asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* ChargeType"></asp:Label>
            <asp:DropDownList ID="ddlChargeType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlChargeType" runat="server" ControlToValidate="ddlChargeType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Charge Type" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Amount"></asp:Label>
            <asp:TextBox ID="txtShippingRate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" ErrorMessage="Enter Valid Shipping Rate" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="imgbtnAddCharge" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" ValidationGroup="ValidateAdd" />
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Other Charges" Visible="false"></asp:Label>
            <asp:TextBox ID="txtOtherCharge" Visible="false" runat="server" CssClass="aspxcontrolsdisable" AutoPostBack="false"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOtherCharges" runat="server" ControlToValidate="txtOtherCharge" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter Other Charges." ValidationGroup="TaxTypeValidate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Label runat="server" Text="Paid Amount" Visible="false"></asp:Label>
            <asp:TextBox ID="txtPaidAmount" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <asp:DataGrid ID="GvCharge" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="50%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="ChargeID" HeaderText="ChargeID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="ChargeType" HeaderText="ChargeType">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="ChargeAmount" HeaderText="ChargeAmount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnCancel" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" ImageUrl="~/Images/Trash16.png" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:CheckBox ID="chkboxFrom" runat="server" AutoPostBack="true" Text="Different Address" />
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" Enabled="false" Visible="false" CssClass="aspxcontrols"></asp:DropDownList>
            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlBranch" runat="server" ControlToValidate="ddlBranch" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:CheckBox ID="chkboxTo" runat="server" AutoPostBack="true" Text="Different Address" />
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier Billing Address"></asp:Label>
            <asp:TextBox ID="txtBillingAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px" ></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingAddress" runat="server" ControlToValidate="txtBillingAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Delivered From"></asp:Label>
            <asp:TextBox ID="txtDeliveryFromAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryFromAddress" runat="server" ControlToValidate="txtDeliveryFromAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery From Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Bill To"></asp:Label>
            <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Bill To Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipping To"></asp:Label>
            <asp:TextBox ID="txtReceiveAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReceiveAddress" runat="server" ControlToValidate="txtReceiveAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtBillingGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable" ></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingGSTNRegNo" runat="server" ControlToValidate="txtBillingGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillingGSTNRegNo" runat="server" ControlToValidate="txtBillingGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Billing GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Delivered From GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtDeliveryFromGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryFromGSTNRegNo" runat="server" ControlToValidate="txtDeliveryFromGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery From GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliveryFromGSTNRegNo" runat="server" ControlToValidate="txtDeliveryFromGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Delivery From GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Bill To GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtCompanyGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyGSTNRegNo" runat="server" ControlToValidate="txtCompanyGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Bill To GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyGSTNRegNo" runat="server" ControlToValidate="txtCompanyGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Company GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipp To GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtReceiveGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReceiveGSTNRegNo" runat="server" ControlToValidate="txtReceiveGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVReceiveGSTNRegNo" runat="server" ControlToValidate="txtReceiveGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Shipping To GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-sm-2 col-md-2">
                    <asp:Label runat="server" Text="Bill No."></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillNo" runat="server" ControlToValidate="txtBillNo" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Bill No." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>

                <div class="col-sm-2 col-md-2">
                    <asp:Label runat="server" Text="Bill Date"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Bill Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtBillDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                    <cc1:CalendarExtender ID="cclBillDate" runat="server" PopupButtonID="txtBillDate" PopupPosition="BottomLeft"
                        TargetControlID="txtBillDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                    </cc1:CalendarExtender>
                </div>


                <div class="col-sm-2 col-md-2">
                    <asp:Label ID="Label3" Text="Discount %" runat="server"></asp:Label>
                    <asp:TextBox ID="txtDis" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDis" runat="server" ControlToValidate="txtDis" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Amount." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDis" runat="server" ControlToValidate="txtDis" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
                </div>

                <div class="col-sm-2 col-md-2">
                    <asp:Label ID="Label1" Text="Dis Amt" runat="server"></asp:Label>
                    <asp:TextBox ID="txtDiscountAmt" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDiscountAmt" runat="server" ControlToValidate="txtDiscountAmt" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Amount." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDiscountAmt" runat="server" ControlToValidate="txtDiscountAmt" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
                </div>

                <div class="col-sm-1 col-md-1">
                    <asp:Label ID="Label5" Text="Charges" runat="server"></asp:Label>
                    <asp:TextBox ID="txtCharges" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtCharges" runat="server" ControlToValidate="txtCharges" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Charges." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtCharges" runat="server" ControlToValidate="txtCharges" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-1 col-md-1">
                    <asp:Label ID="Label2" Text="GST %" runat="server"></asp:Label>
                    <asp:TextBox ID="txtGST" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtGST" runat="server" ControlToValidate="txtGST" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter GST." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtGST" runat="server" ControlToValidate="txtGST" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
                </div>

                <div class="col-sm-2 col-md-2">
                    <asp:Label ID="Label4" Text="GST Amt" runat="server"></asp:Label>
                    <asp:TextBox ID="txtGSTAmt" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtGSTAmt" runat="server" ControlToValidate="txtGSTAmt" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Amount." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtGSTAmt" runat="server" ControlToValidate="txtGSTAmt" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
                </div>
            </ContentTemplate>
            <Triggers>
                <%-- <asp:AsyncPostBackTrigger ControlID="ddlTaxType" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="ddlTaxRate" EventName="SelectedIndexChanged" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

        <div class="col-sm-1 col-md-1">
            <asp:Label ID="Label6" Text="SGST" runat="server"></asp:Label>
            <asp:TextBox ID="txtSGST" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtSGST" runat="server" ControlToValidate="txtSGST" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter SGST." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtSGST" runat="server" ControlToValidate="txtSGST" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label ID="Label7" Text="SGST Amt" runat="server"></asp:Label>
            <asp:TextBox ID="txtSGSTAmt" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtSGSTAmt" runat="server" ControlToValidate="txtSGSTAmt" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter SGST Amount." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtSGSTAmt" runat="server" ControlToValidate="txtSGSTAmt" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label ID="Label8" Text="CGST" runat="server"></asp:Label>
            <asp:TextBox ID="txtCGST" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtCGST" runat="server" ControlToValidate="txtCGST" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter CGST." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtCGST" runat="server" ControlToValidate="txtCGST" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label ID="Label9" Text="CGST Amt" runat="server"></asp:Label>
            <asp:TextBox ID="txtCGSTAmt" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtCGSTAmt" runat="server" ControlToValidate="txtCGSTAmt" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter CGST Amount." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtCGSTAmt" runat="server" ControlToValidate="txtCGSTAmt" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Label ID="Label10" Text="IGST" runat="server"></asp:Label>
            <asp:TextBox ID="txtIGST" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtIGST" runat="server" ControlToValidate="txtIGST" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter IGST." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtIGST" runat="server" ControlToValidate="txtIGST" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label ID="Label11" Text="IGST Amt" runat="server"></asp:Label>
            <asp:TextBox ID="txtIGSTAmt" runat="server" CssClass="aspxcontrols" ></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtIGSTAmt" runat="server" ControlToValidate="txtIGSTAmt" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter IGST Amount." ValidationGroup="AddbtnValidate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtIGSTAmt" runat="server" ControlToValidate="txtIGSTAmt" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="AddbtnValidate"></asp:RegularExpressionValidator>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Bill Amt"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillAmouont" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter Bill Amt." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtBillAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgPurchase" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TaxTypeID" HeaderText="TaxTypeID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TaxRateID" HeaderText="TaxRateID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Amount" HeaderText="Basic Amt" Visible="True">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TradeDis" HeaderText="Trade Dis %" Visible="True">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TradeDisAmt" HeaderText="Trade DisAmt" Visible="True">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TotalNetAmt" HeaderText="Total Amt" Visible="True">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TaxType" HeaderText="Tax Type" Visible="True">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TaxRate" HeaderText="Rate of Tax" Visible="True">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TaxAmount" HeaderText="Tax Amount" Visible="True">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="12%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Total" HeaderText="Total">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Roundof" HeaderText="Round Off">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>


    <div class="col-md-12">
        <div id="DivAccountsDetails" visible="false" runat="server" data-toggle="collapse" data-target="#collapseAccountsDetails"><a href="#"><b><i>Click here to check Account Details...</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseAccountsDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group">
                <asp:GridView ID="GvAccountDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                    <Columns>

                        <asp:TemplateField HeaderText="GLID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblGLID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SubGLID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSubGLID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubGLID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="General Ledger" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left"
                            ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblGLCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLCode") %>' ForeColor="#cc33ff"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                        <ItemTemplate>
                            <asp:Label ID="lblGLDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Sub General Ledger" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top"
                            ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblSubGLCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubGLCode") %>' ForeColor="#0000ff"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                        <ItemTemplate>
                            <asp:Label ID="lblSubGLDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubGLDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblDebit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Debit") %>'></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lblDrGtotal" Text='<%# DataBinder.Eval(Container, "DataItem.DebitTotal") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="180px" ReadOnly="True" Font-Bold="True"></asp:Label>
                            </FooterTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Credit" HeaderStyle-HorizontalAlign="Right" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Credit") %>'></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lblCrGtotal" Text='<%# DataBinder.Eval(Container, "DataItem.CreditTotal") %>' Style="text-align: right" runat="server" CssClass="aspxcontrols" Width="180px" ReadOnly="True" Font-Bold="True"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:TextBox ID="txtGLID" runat="server" Visible="false"></asp:TextBox>
    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgJEDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable"  >
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="PaymentID" HeaderText="PaymentID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Type" HeaderText="Type">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="11%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="30%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance" Visible ="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="12%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible ="false" >
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>

    <div class="col-md-12">
        <div id="divcollapseChequeDetails" runat="server" data-toggle="collapse" data-target="#collapseChequeDetails"><a href="#"><b><i>Click here to view Cheque Details...</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseChequeDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Cheque No."></asp:Label>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEChequeNo" runat="server" ControlToValidate="txtChequeNo" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtChequeNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Cheque Date"></asp:Label>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REFChequeDate" runat="server" ControlToValidate="txtChequeDate" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtChequeDate" runat="server" autocomplete="off" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtChequeDate" PopupPosition="BottomLeft"
                        TargetControlID="txtChequeDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                    </cc1:CalendarExtender>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="IFSC Code"></asp:Label>
                    <asp:TextBox ID="txtIFSC" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Bank Name"></asp:Label>
                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Branch Name"></asp:Label>
                    <asp:TextBox ID="txtBranchName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalFASCompanyValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCustomerValidationMsg" runat="server"></asp:Label>
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

