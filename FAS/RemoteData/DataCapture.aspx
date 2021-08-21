<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="DataCapture.aspx.vb" Inherits="RemoteData_DataCapture" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register TagPrefix="GleamTech" Namespace="GleamTech.ExamplesCore" Assembly="GleamTech.ExamplesCore" %>
<%@ Register TagPrefix="GleamTech" Namespace="GleamTech.DocumentUltimate.Web" Assembly="GleamTech.DocumentUltimate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/bootstrap-multiselect.css" rel="stylesheet" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap-multiselect.js"></script>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />

    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>

    <script lang="javascript" type="text/javascript">

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlAccZone.ClientID%>').select2();
            $('#<%=ddlAccRgn.ClientID%>').select2();
            $('#<%=ddlAccArea.ClientID%>').select2();
            $('#<%=ddlAccBrnch.ClientID%>').select2();
            $('#<%=ddlPaymentType.ClientID%>').select2();
            $('#<%=ddlTrType.ClientID%>').select2();
            $('#<%=ddlExisting.ClientID%>').select2();

        });

        $('#<%=lstUsers.ClientID%>').multiselect({
            includeSelectAllOption: true,
            allSelectedText: 'No option left ...',
            enableFiltering: true,
            filterPlaceholder: 'Search...',
        });


        var numbersOnly = /^\d+$/;
        var decimalOnly = /^\s*-?[0-9]\d*(\.\d{1,2})?\s*$/;
        var uppercaseOnly = /^[A-Z]+$/;
        var lowercaseOnly = /^[a-z]+$/;
        var stringOnly = /^[A-Za-z0-9]+$/;

        var ddlText, ddlValue, ddl, lblMesg;

        function checkDate(sender, args) {
            if (sender._selectedDate > new Date()) {
                alert("You cannot select a day grtaer than today!");
                document.getElementById('<%=txtOrderDate.ClientID %>').value = ""
                document.getElementById('<%=txtOrderDate.ClientID %>').focus()
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }

        }


    </script>
    <style>
         div {
            color: black;
        }
    </style>
    <script type="text/javascript" lang="javascript" src="../JavaScripts/General.js"></script>
    <style>
        .legendbold {
            font-size: 16px;
            font-weight: bold;
            color: #919191;
            margin-bottom: 10px;
        }
    </style>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Data Capture</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" />
                    <asp:ImageButton ID="imgbtnSendBackImage" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Send Mail" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-9 col-md-9">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label ID="lblExisting" runat="server" Text="Select Existing"></asp:Label>
            <asp:DropDownList ID="ddlExisting" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2">
            <br />
            <asp:Label ID="lblStatus" runat="server" Text="Status :- " Font-Bold="true"></asp:Label>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
            <label>* Company</label>
            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCompany" runat="server" ControlToValidate="ddlCompany" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label ID="lblTrType" runat="server" Text="Transaction Type"></asp:Label>
            <asp:DropDownList ID="ddlTrType" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlTrType" runat="server" ControlToValidate="ddlTrType" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Label ID="lblBatchNo" runat="server" Text="BatchNo"></asp:Label>
            <asp:DropDownList ID="ddlBatchNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlBatchNo" runat="server" ControlToValidate="ddlBatchNo" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label ID="lblParty" runat="server" Text="Select Customer/Supplier"></asp:Label>
            <asp:DropDownList ID="ddlParty" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlParty" runat="server" ControlToValidate="ddlParty" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <div class="form-group">
                <asp:Label ID="lblVoucherNo" runat="server" Text="Voucher"></asp:Label>
                <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtVoucherNo" runat="server" ControlToValidate="txtVoucherNo" Display="Dynamic" SetFocusOnError="True"
                    ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label ID="lblPayment" runat="server" Text="Payment Type"></asp:Label>
                <asp:DropDownList ID="ddlPaymentType" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlPaymentType" runat="server" ControlToValidate="ddlPaymentType" Display="Dynamic" SetFocusOnError="True"
                    ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-1 col-md-1">
            <div class="form-group">
                <asp:Label ID="lblTrNo" runat="server" Text="Tr No"></asp:Label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-1 col-md-1">
            <div class="form-group">
                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                <asp:TextBox ID="txtDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" SetFocusOnError="True"
                    ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                <cc1:CalendarExtender ID="txtDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDate" TargetControlID="txtDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
        </div>
        <div class="col-sm-2 col-md-2">
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <div class="col-sm-6 col-md-6 table-bordered" style="padding-left: 0px; padding-right: 0px;">
            <div class="col-sm-12 col-md-12 col-md-offset-1 form-group">
                <asp:ImageButton ID="imgbtnNavDocFastRewind" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPreviousNavDoc" CssClass="hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Previous" Style="margin-right: 10px;" CausesValidation="false" />
                <asp:TextBox ID="txtNavDoc" Text="1" runat="server" Enabled="false" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                <asp:Label ID="lblNavDoc" runat="server" Width="30px" CssClass="aspxlabelbold"></asp:Label>
                <asp:ImageButton ID="imgbtnNextNavDoc" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnNavDocFastForword" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                |<asp:ImageButton ID="imgbtnFastRewind" CssClass="activeIcons hvr-bounce-in" runat="server" Style="margin-left: 10px;" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPreviousNav" CssClass="hvr-bounce-in" runat="server" data-toggle="tooltip" Style="margin-right: 10px;" data-placement="bottom" title="Previous" CausesValidation="false" />
                <asp:TextBox ID="txtNav" Text="1" runat="server" Enabled="false" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                <asp:Label ID="lblNav" Text="/1" runat="server" Width="30px" CssClass="aspxlabelbold"></asp:Label>
                <asp:ImageButton ID="imgbtnNextNav" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnFastForword" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                <asp:ImageButton Visible="false" ID="imgbtnAnnotation" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Annotation" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnInvalidImage" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Invalid Image" CausesValidation="false" />
            </div>
            <asp:Panel ID="pnlImageView" runat="server" CssClass="col-sm-12 col-md-12" Style="padding: 0px">
                <GleamTech:DocumentViewer ID="documentViewer" runat="server" Height="500"
                Resizable="False" />
               <%-- <asp:Image ID="RetrieveImage" Height="680px" CssClass="col-sm-12 col-md-12" Style="padding-left: 0px; padding-right: 0px;" runat="server" />--%>
            </asp:Panel>
             

        </div>
        <div class="col-sm-6 col-md-6" style="padding-right: 0px">
            <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server" visible="True">
                <div id="div2" runat="server">

                    <ul class="nav nav-tabs" role="tablist">
                        <li id="liPurchase" runat="server">
                            <asp:LinkButton ID="lnkbtnPurchase" Text="Purchase" runat="server" Font-Bold="true" /></li>
                        <li id="liSales" runat="server">
                            <asp:LinkButton ID="lnkbtnSales" Text="Sales" runat="server" Font-Bold="true" /></li>
                        <li id="liRPJ" runat="server">
                            <asp:LinkButton ID="lnkbtnRPJ" Text="Receipt/PettyCash/JE" runat="server" Font-Bold="true" /></li>
                        <li id="liPayment" runat="server">
                            <asp:LinkButton ID="lnkbtnPayment" Text="Payment" runat="server" Font-Bold="true" /></li>
                    </ul>
                </div>

                <div class="tab-content divmargin">
                    <div runat="server" role="tabpanel" class="tab-pane active" id="divPurchase">
                        <%-- <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <asp:Label ID="lblPurchase" runat="server" Text="Purchase Print Settings" CssClass="h5" Font-Bold="true"></asp:Label>
                        </div>--%>
                        <div id="divPO" runat="server">
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                    <asp:DropDownList ID="ddlExistingOrder" runat="server" CssClass="aspxcontrols" Width="240px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-1 col-md-1">
                                    <asp:ImageButton ID="imgbtnRefreshPO" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                                </div>
                                <div class="col-sm-1 col-md-1">
                                    <asp:ImageButton ID="imgbtnSavePO" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="ValidatePO" />
                                </div>
                                <div class="col-sm-1 col-md-1">
                                    <asp:ImageButton ID="imgbtnPrintPo" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" />                                                                
                                      </div>
                                <div class="col-sm-3 col-md-3">
                              <asp:Label runat="server" ID="lblPO" ></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="Purchase Order No."></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtOrderCode"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Order Date"></asp:Label>
                                    <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtOrderDate" AutoPostBack="true" ValidateRequestMode="Disabled" placeholder="DD/MM/YY"  AutoCompleteType="Disabled"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtOrderDate" PopupPosition="BottomLeft"
                                        TargetControlID="txtOrderDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" OnClientDateSelectionChanged="checkDate">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="ReFVOdate" runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Payment Terms"></asp:Label>
                                    <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlPterms"></asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPterms" runat="server" ControlToValidate="ddlPterms" Display="Dynamic" ErrorMessage="Select Payment Terms" SetFocusOnError="True" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Payment"></asp:Label>
                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlMPayment"></asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMpayment" runat="server" ControlToValidate="ddlMPayment" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Method Of Payment" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlSupplier"></asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplier" runat="server" ControlToValidate="ddlSupplier" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier Name" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="*Company Type"></asp:Label>
                                    <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="*GSTN Category"></asp:Label>
                                    <asp:DropDownList ID="ddlGSTCategory" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Shipping"></asp:Label>
                                    <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlModeOfShipping"></asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMshipping" runat="server" ControlToValidate="ddlModeOfShipping" Display="Dynamic" ErrorMessage="Select Mode of Shipping" SetFocusOnError="True" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* (No.of Weeks)"></asp:Label>
                                    <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlDSchedule"></asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDschdule" runat="server" ControlToValidate="ddlDSchedule" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Delivery Schdule" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>

                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="(No.of Days)"></asp:Label>
                                    <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlNumberOfDays"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Brand"></asp:Label>
                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCommodity"></asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCmdty" runat="server" ControlToValidate="ddlCommodity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Brand" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                     <asp:Label runat="server" Text="Voucher.No"></asp:Label>
                                     <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtVoucherPO"></asp:TextBox>
                                </div>

                            </div>

                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <div class="col-sm-3 col-md-3">
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:CheckBox ID="chkboxFrom" runat="server" AutoPostBack="true" Text="Same" />
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" Enabled="false" Visible="false" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:CheckBox ID="chkboxTo" runat="server" AutoPostBack="true" Text="Same" />
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="SupplierBilling Address"></asp:Label>
                                    <asp:TextBox ID="txtBillingAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingAddress" runat="server" ControlToValidate="txtBillingAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="Delivered From"></asp:Label>
                                    <asp:TextBox ID="txtDeliveryFromAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryFromAddress" runat="server" ControlToValidate="txtDeliveryFromAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery From Address" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="Bill To"></asp:Label>
                                    <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Company Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>            --%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="Shipping To"></asp:Label>
                                    <asp:TextBox ID="txtDeleveryAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeleveryAddress" runat="server" ControlToValidate="txtDeleveryAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To Address" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="Supplier GSTN Reg.No"></asp:Label>
                                    <asp:TextBox ID="txtBillingGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingGSTNRegNo" runat="server" ControlToValidate="txtBillingGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillingGSTNRegNo" runat="server" ControlToValidate="txtBillingGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Billing GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
                                    <asp:TextBox ID="txtDeliveryFromGSTNRegNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryFromGSTNRegNo" runat="server" ControlToValidate="txtDeliveryFromGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery From GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliveryFromGSTNRegNo" runat="server" ControlToValidate="txtDeliveryFromGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Delivery From GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
                                    <asp:TextBox ID="txtCompanyGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyGSTNRegNo" runat="server" ControlToValidate="txtCompanyGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Company GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyGSTNRegNo" runat="server" ControlToValidate="txtCompanyGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Company GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
                                    <asp:TextBox ID="txtDeliveryGSTNRegNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryGSTNRegNo" runat="server" ControlToValidate="txtDeliveryGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliveryGSTNRegNo" runat="server" ControlToValidate="txtDeliveryGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Shipping To GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-5 col-md-5 form-group pull-left" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
                                        <div class="col-sm-5 col-md-5 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="* ChargeType"></asp:Label>
                                            <asp:DropDownList ID="ddlChargeType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlChargeType" runat="server" ControlToValidate="ddlChargeType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Charge Type" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-sm-5 col-md-5">
                                            <asp:Label runat="server" Text="* Amt"></asp:Label>
                                            <asp:TextBox ID="txtShippingRate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" ErrorMessage="Enter Valid Shipping Rate" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-sm-1 col-md-1">
                                            <br />
                                            <asp:ImageButton ID="imgbtnAddChargePo" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" ValidationGroup="ValidateAdd" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 col-md-7 form-group pull-right" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group">
                                        <asp:DataGrid ID="GvCharge" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable">
                                            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                                            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ChargeID" HeaderText="ChargeID" Visible="False">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="ChargeType" HeaderText="ChargeType">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="ChargeAmount" HeaderText="ChargeAmt">
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
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                                <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                                                <asp:TextBox autocomplete="off" ID="txtsearch" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="FilterItems(this.value)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                                            <asp:ListBox ID="chkCategory" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="180px" ValidationGroup="Validate" Style="left: 0px; top: 0px"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-6" style="padding: 0px">
                                        <%-- <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                        <asp:Label runat="server" Text="Delivery Required Date"></asp:Label>
                                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRDate"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtRDate" PopupPosition="BottomLeft"
                                            TargetControlID="txtRDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </cc1:CalendarExtender>
                                    </div>--%>
                                        <%-- <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                         <asp:Label runat="server" Text="* Rate"></asp:Label>
                                         <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlRate"></asp:DropDownList>
                                    </div>--%>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Unit"></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlUnit"></asp:DropDownList>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlUnit" runat="server" ControlToValidate="ddlUnit" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Unit" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Rate"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRate"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RefRate" runat="server" ErrorMessage="Enter Rate" ControlToValidate="txtRate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Quantity"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtQuantity" ValidationGroup="ValidatePO"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtQuantity" runat="server" ErrorMessage="Enter Quantity" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Amount(Qty*Rate)"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtRateAmount"></asp:TextBox>
                                                <asp:HiddenField ID="hfRateAmount" runat="server" />
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtRateAmount" runat="server" ErrorMessage="Enter Amount" ControlToValidate="txtRateAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="Discount"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtDiscount"></asp:TextBox>
                                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="ReDiscount" runat="server" ControlToValidate="txtDiscount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePO"></asp:RegularExpressionValidator>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Discount Amount"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtDiscountAmount"></asp:TextBox>
                                                <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                                            </div>
                                        </div>
                                        <%--<div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                        <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="Freight Amount" Visible="false"></asp:Label>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtFreightAmount" Visible="false"></asp:TextBox>
                                            <asp:HiddenField ID="hfFreightAmount" runat="server" />
                                        </div>
                                        <div class="col-sm-6 col-md-6">
                                            <asp:Label runat="server" Text="Freight" Visible="false"></asp:Label>
                                            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtFreight" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="GST Rate %"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtGSTRate"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="GST Amount"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtGSTAmount"></asp:TextBox>
                                                <asp:HiddenField ID="hfGSTAmount" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="Total Amount"></asp:Label>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtTotalAmount"></asp:TextBox>
                                            <asp:HiddenField ID="hfTotalAmount" runat="server" />
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtTotalAmount" runat="server" ErrorMessage="Enter Total Amount" ControlToValidate="txtTotalAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePO"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" Visible="false" ID="txtGSTID"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divGIN" runat="server">
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnRefreshGIN" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Reresh16.png" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                                </div>
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnSaveGIN" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save16.png" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="ValidateGIN" />
                                </div>
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnApproveGIN" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/CheckMark16.png" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:Label ID="lblStatusGIN" runat="server" Text="Status :-"></asp:Label>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlExistingInwardNo" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label ID="Label5" runat="server" Text="GIN No"></asp:Label>
                                    <asp:TextBox ID="txtOrderCodeGIN" CssClass="aspxcontrolsdisable" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-4 col-md-4">
                                    <asp:Label ID="Label4" runat="server" Text="Supplier Name"></asp:Label>
                                    <asp:DropDownList ID="ddlSupplierGIN" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlSupplierGIN" runat="server" ControlToValidate="ddlSupplierGIN" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label runat="server" Text="* Invoice Reference No."></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtDocRefNo"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDocRefNo" runat="server" ControlToValidate="txtDocRefNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Invoice Reference No" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm- col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* PO Order No."></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtOrderNoGIN"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOrderNoGIN" runat="server" ControlToValidate="txtOrderNoGIN" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Order No" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label runat="server" Text="* Order Date."></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtOrderDateGIN"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOrderDateGIN" runat="server" ControlToValidate="txtDocRefNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Order Date" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender13" runat="server" PopupButtonID="txtOrderDateGIN" PopupPosition="BottomLeft"
                                        TargetControlID="txtOrderDateGIN" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="E-Sugam No."></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtESugamNo"></asp:TextBox>
                                </div>
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label runat="server" Text="* Delivery challan No"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtDcNo"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDcNo" runat="server" ControlToValidate="txtDcNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery Challan No" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-4 col-md-4">
                                    <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtInvoiceDateGIN" placeholder="DD/MM/YY"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtInvoiceDateGIN" PopupPosition="BottomLeft"
                                        TargetControlID="txtInvoiceDateGIN" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtInvoiceDateGIN" runat="server" ControlToValidate="txtInvoiceDateGIN" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Invoice Date" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* Commodity"></asp:Label>
                                    <asp:DropDownList ID="ddlCommodityGIN" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                                <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                                                <asp:TextBox autocomplete="off" ID="txtsearchGIN" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="FilterItems(this.value)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                                            <asp:ListBox ID="chkCategoryGIN" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="280px" ValidationGroup="ValidateGIN" Style="left: 0px; top: 0px"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-6" style="padding: 0px">

                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Unit"></asp:Label>
                                                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlUnitGIN"></asp:DropDownList>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlUnitGIN" runat="server" ControlToValidate="ddlUnitGIN" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Unit" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Rate"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRateGIN"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtRateGIN" runat="server" ErrorMessage="Enter Rate" ControlToValidate="txtRateGIN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Ordered Qty"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtOrderedQty"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOrderedQty" runat="server" ErrorMessage="Enter Ordered Qty" ControlToValidate="txtOrderedQty" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Received Qty"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtReceivedQty"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField5" runat="server" />
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtReceivedQty" runat="server" ErrorMessage="Enter Received Qty" ControlToValidate="txtReceivedQty" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Accptd Qty"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtAcceptedQty"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtAcceptedQty" runat="server" ErrorMessage="Enter Accepted Qty" ControlToValidate="txtAcceptedQty" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateGIN"></asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Rejected Qty"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtRejectedQty"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField6" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Excess Qty"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtExcessQty"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Batch No"></asp:Label>
                                                <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtBatchNo"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Manufacture Date"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtManufactureDate"></asp:TextBox>
                                                <asp:HiddenField ID="hfFreightAmount" runat="server" />
                                                <cc1:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="txtManufactureDate" PopupPosition="BottomLeft"
                                                    TargetControlID="txtManufactureDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </cc1:CalendarExtender>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Expire Date"></asp:Label>
                                                <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtExpireDate"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="txtExpireDate" PopupPosition="BottomLeft"
                                                    TargetControlID="txtExpireDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </cc1:CalendarExtender>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" Visible="false" ID="txtGSTIDGIN"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divPR" runat="server">
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnRefreshPR" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Reresh16.png" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                                </div>
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnSavePR" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save16.png" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidatePR" />
                                </div>
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnApprovePR" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/CheckMark16.png" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:Label ID="lblStatusPR" runat="server" Text="Status :-"></asp:Label>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlExistingReturnNor" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label ID="Label7" runat="server" Text="Return No"></asp:Label>
                                    <asp:TextBox ID="txtOrderCodePR" CssClass="aspxcontrolsdisable" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-4 col-md-4">
                                    <asp:Label ID="Label8" runat="server" Text="Supplier Name"></asp:Label>
                                    <asp:DropDownList ID="ddlSupplierPR" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlSupplierPR" runat="server" ControlToValidate="ddlSupplierPR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label runat="server" Text="* Return Reference No."></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtReturnRefNo"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtReturnRefNo" runat="server" ControlToValidate="txtReturnRefNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Return ref No" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* PO Order No"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtOrderNoPR"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOrderNoPR" runat="server" ControlToValidate="txtOrderNoPR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Order No" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Order Date"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtOrderDatePR"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOrderDatePR" runat="server" ControlToValidate="txtOrderDatePR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Order Date" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender11" runat="server" PopupButtonID="txtOrderDatePR" PopupPosition="BottomLeft"
                                        TargetControlID="txtOrderDatePR" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Invoice No"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtInvoiceNoPR"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtInvoiceNoPR" runat="server" ControlToValidate="txtInvoiceNoPR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Invoice No" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtInvoiceDatePR"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtInvoiceDatePR" runat="server" ControlToValidate="txtInvoiceDatePR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Invoice Date" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender12" runat="server" PopupButtonID="txtInvoiceDatePR" PopupPosition="BottomLeft"
                                        TargetControlID="txtInvoiceDatePR" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* Return Date"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtReturnDate" placeholder="DD/MM/YY"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="txtReturnDate" PopupPosition="BottomLeft"
                                        TargetControlID="txtReturnDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtReturnDate" runat="server" ControlToValidate="txtReturnDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Return Date" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label runat="server" Text="* Commodity"></asp:Label>
                                    <asp:DropDownList ID="ddlCommodityPR" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4 col-md-4">
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-5 col-md-5 form-group pull-left" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
                                        <div class="col-sm-5 col-md-5 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="* ChargeType"></asp:Label>
                                            <asp:DropDownList ID="ddlChargeTypePR" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlChargeTypePR" runat="server" ControlToValidate="ddlChargeType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Charge Type" ValidationGroup="ValidateAddPR"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-sm-5 col-md-5">
                                            <asp:Label runat="server" Text="* Amt"></asp:Label>
                                            <asp:TextBox ID="txtShippingRatePR" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtShippingRatePR" runat="server" ControlToValidate="txtShippingRatePR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateAddPR"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtShippingRatePR" runat="server" ControlToValidate="txtShippingRatePR" Display="Dynamic" ErrorMessage="Enter Valid Shipping Rate" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-sm-1 col-md-1">
                                            <br />
                                            <asp:ImageButton ID="imgbtnAddChargePR" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Add16.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" ValidationGroup="ValidateAddPR" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 col-md-7 form-group pull-right" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group">
                                        <asp:DataGrid ID="GvChargePR" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable">
                                            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                                            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ChargeID" HeaderText="ChargeID" Visible="False">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="ChargeType" HeaderText="ChargeType">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="ChargeAmount" HeaderText="ChargeAmt">
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
                                </div>
                            </div>



                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                                <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                                                <asp:TextBox autocomplete="off" ID="txtsearchPR" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="FilterItems(this.value)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                                            <asp:ListBox ID="chkCategoryPR" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="305px" ValidationGroup="ValidatePR" Style="left: 0px; top: 0px"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-6" style="padding: 0px">

                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Unit"></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlUnitPR"></asp:DropDownList>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlUnitPR" runat="server" ControlToValidate="ddlUnitPR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Unit" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Rate"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRatePR"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtRatePR" runat="server" ErrorMessage="Enter Rate" ControlToValidate="txtRatePR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Return Qty"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtReturnQuantity" ValidationGroup="ValidatePR"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtReturnQuantity" runat="server" ErrorMessage="Enter Return Qty" ControlToValidate="txtReturnQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Amount(Qty*Rate)"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtRateAmountPR"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField8" runat="server" />
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtRateAmountPR" runat="server" ErrorMessage="Enter Amount" ControlToValidate="txtRateAmountPR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="Discount"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtDiscountPR"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Discount Amount"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtDiscountAmountPR"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField10" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="GST Rate %"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtGSTRatePR"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="GST Amount"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtGSTAmountPR"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField11" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="Total Amount"></asp:Label>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtTotalAmountPR"></asp:TextBox>
                                            <asp:HiddenField ID="HiddenField12" runat="server" />
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtTotalAmountPR" runat="server" ErrorMessage="Enter Total Amount" ControlToValidate="txtTotalAmountPR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="* Reason For Return"></asp:Label>
                                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlreturntype"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlreturntype" runat="server" ErrorMessage="Select Reason For Return" ControlToValidate="ddlreturntype" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePR"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="* Remarks"></asp:Label>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px" ID="txtRemarksPR"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" Visible="false" ID="txtGSTIDPR"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                        </div>


                        <div id="div3" runat="server">
                        </div>
                    </div>

                    <div runat="server" role="tabpanel" class="tab-pane" id="divSales">
                        <div id="divSO" runat="server">
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnRefreshSale" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                                </div>
                                <div class="col-sm-1 col-md-1">
                                    <asp:ImageButton ID="imgbtnAddSale" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="ValidateSales" />
                                </div>
                                <div class="col-sm-1 col-md-1">
                                    <asp:ImageButton ID="imgbtnPrintSale" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" />
                                </div>
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label ID ="lblStatusSO" runat ="server" ></asp:Label>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                     <asp:DropDownList ID="ddlExistingSalesNo" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* Order No"></asp:Label>
                                    <asp:TextBox ID="txtOrderCodeS" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Order Date"></asp:Label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtOrderDateS" AutoPostBack="true" CssClass="aspxcontrols" runat="server" Width="80px"></asp:TextBox>                                        
                                        <cc1:CalendarExtender ID="txtOrderDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtOrderDateS" TargetControlID="txtOrderDateS" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="MRFVOrderDateS" runat="server" ControlToValidate="txtOrderDateS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Order Date" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="MREVOrderDateS" runat="server" ControlToValidate="txtOrderDateS" Display="Dynamic" ErrorMessage="Enter Valid Order Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                        <%-- <asp:RangeValidator ID="rgvtxtOrderDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtOrderDate" SetFocusOnError="True"></asp:RangeValidator>--%>
                                    </div>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Payment Type"></asp:Label>
                                    <asp:DropDownList ID="ddlPaymentTypeS" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPaymentTypeS" runat="server" ControlToValidate="ddlPaymentTypeS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Payment Type" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-9 col-md-9 form-group pull-left" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                        <div class="col-sm-4 col-md-4 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="* Customer"></asp:Label>
                                            <asp:DropDownList ID="ddlPatryS" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPatryS" runat="server" ControlToValidate="ddlPatryS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Customer" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-1 col-md-1">
                                            <br />
                                            <asp:ImageButton ID="imgbtnCreateCustomer" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                                        </div>
                                        <div class="col-sm-3 col-md-3">
                                            <asp:Label runat="server" Text="Code"></asp:Label>
                                            <asp:TextBox ID="txtPartyNo" CssClass="aspxcontrolsdisable" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label ID="lblContactNo" runat="server" Text="Contact No"></asp:Label>
                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                                        <div class="col-sm-4 col-md-4 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label runat="server" Text="Shipping"></asp:Label>
                                            <asp:DropDownList ID="ddlShipping" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label runat="server" Text="Shipping Date"></asp:Label>
                                            <asp:TextBox ID="txtShippingDate" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtShippingDate_CalendarExtender" runat="server" PopupButtonID="txtShippingDate" CssClass="cal_Theme1"
                                                TargetControlID="txtShippingDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
                                            </cc1:CalendarExtender>
                                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVShippingDate" runat="server" ControlToValidate="txtShippingDate" Display="Dynamic" ErrorMessage="Enter Valid Shipping Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="90px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-9 col-md-9 form-group pull-left" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                        <div class="col-sm-4 col-md-4 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="Shipp Chargers"></asp:Label>
                                            <asp:DropDownList ID="ddlShippingCharges" runat="server" CssClass="aspxcontrols">
                                                <asp:ListItem Value="0">Select Shipping Charges</asp:ListItem>
                                                <asp:ListItem Value="1">Payable on deleivery</asp:ListItem>
                                                <asp:ListItem Value="2">Paid recoverable</asp:ListItem>
                                                <asp:ListItem Value="3">Not recoverable</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label runat="server" Text="Sales Person"></asp:Label>
                                            <asp:DropDownList ID="ddlSalesMan" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label runat="server" Text="Communication"></asp:Label>
                                            <asp:DropDownList ID="ddlModeOfCommunication" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                        </div>
                                    </div>


                                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                        <div class="col-sm-4 col-md-4 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="Buyer Ref.No"></asp:Label>
                                            <asp:TextBox ID="txtBuyerPurOrderNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label runat="server" Text="Buyer Ref.Date"></asp:Label>
                                            <asp:TextBox ID="txtBuyerOrderDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtBuyerOrderDate_CalendarExtender" runat="server" PopupButtonID="txtBuyerOrderDate"
                                                TargetControlID="txtBuyerOrderDate" Format="dd/MM/yyyy" PopupPosition="TopLeft">
                                            </cc1:CalendarExtender>
                                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBuyerOrderDate" runat="server" ControlToValidate="txtBuyerOrderDate" Display="Dynamic" ErrorMessage="Enter Valid Buyer Reference Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label runat="server" Text="Inputed By"></asp:Label>
                                            <asp:TextBox ID="txtInputBy" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVInputBy" runat="server" ControlToValidate="txtInputBy" Display="Dynamic" ErrorMessage="Enter Valid Inputed By." SetFocusOnError="True" ValidationExpression="^[a-zA-Z\s]{1,100}$"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="Remarks"></asp:Label>
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="aspxcontrols" Height="90px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-6 col-md-6 form-group pull-left " style="padding: 0px">
                                <asp:Label runat="server" Text="* Commodity"></asp:Label>
                                <asp:DropDownList ID="ddlCommodityS" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCommodityS" runat="server" ControlToValidate="ddlCommodityS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Commodity" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-6 col-md-6 form-group pull-left " style="padding: 0px">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <asp:Label runat="server" Text="* Item Description"></asp:Label>
                                            <asp:TextBox ID="txtSearchItem" onkeyup="FilterItems(this.value)" runat="server" CssClass="aspxcontrols" placeholder="Search By Description" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-12 col-md-12 pre-scrollableborder" style="padding: 0px">
                                            <asp:ListBox ID="lstBoxDescription" CssClass="aspxcontrols" AutoPostBack="true" runat="server" Enabled="False" Height="200px"
                                                Font-Names="Verdana" Font-Size="Smaller"></asp:ListBox>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVlstBoxDescription" runat="server" ControlToValidate="lstBoxDescription" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select An Item" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 col-md-6" style="padding: 0px">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                                            <asp:DropDownList ID="ddlUnitOfMeassurement" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlUnitOfMeassurement" runat="server" ControlToValidate="ddlUnitOfMeassurement" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Unit Of Meassurement" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label ID="lblPCRate" runat="server" Text="* Rate"></asp:Label>
                                            <asp:TextBox ID="txtMRP" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                            <asp:HiddenField ID="hfMRP" runat="server" />
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMRP" runat="server" ControlToValidate="txtMRP" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Rate" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Quantity"></asp:Label>
                                                <asp:TextBox ID="txtQuantitySales" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVQuantitySales" runat="server" ControlToValidate="txtQuantitySales" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Quantity" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <br />
                                                <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                                <asp:HiddenField ID="hfAmount" runat="server" />
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAmount" runat="server" ControlToValidate="txtAmount" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="Total Amount"></asp:Label>
                                            <asp:TextBox ID="txtNetAmount" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                            <asp:HiddenField ID="hfNetAmount" runat="server" />
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFvNetAmount" runat="server" ControlToValidate="txtNetAmount" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Total Amount" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-12 col-md-12" style="padding-right: 0px">
                                            <div class="pull-right">
                                                <asp:TextBox ID="txtMRPFromTable" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtOrderID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                                                <asp:HiddenField ID="txtCode" runat="server" />
                                                <asp:HiddenField ID="hfItemVAT" runat="server" />
                                                <asp:HiddenField ID="hfAvailableQty" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div id="divCS" runat="server">
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnRefreshCDI" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                                </div>
                                <div class="col-sm-1 col-md-1">
                                    <asp:ImageButton ID="imgbtnAddCDI" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="ValidateCDI" />
                                </div>
                                <div class="col-sm-1 col-md-1">
                                    <asp:ImageButton ID="imgbtnPrintcashS" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" />
                                </div>
                            </div>

                            <div id="divCash" runat="server">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                        <asp:Label runat="server" Text="* Order No"></asp:Label>
                                        <asp:TextBox ID="txtOrderCodeCS" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="* Order Date"></asp:Label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtOrderDateCS" AutoPostBack="true" CssClass="aspxcontrols" runat="server" Width="80px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender14" CssClass="cal_Theme1" runat="server" PopupButtonID="txtOrderDateCS" TargetControlID="txtOrderDateCS" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOrderDateCS" runat="server" ControlToValidate="txtOrderDateCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Order Date" ValidationGroup="ValidateSales"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtOrderDateCS" runat="server" ControlToValidate="txtOrderDateCS" Display="Dynamic" ErrorMessage="Enter Valid Order Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                            <%-- <asp:RangeValidator ID="rgvtxtOrderDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtOrderDate" SetFocusOnError="True"></asp:RangeValidator>--%>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Shipp Chargers"></asp:Label>
                                        <asp:DropDownList ID="ddlShippingChargesCS" runat="server" CssClass="aspxcontrols">
                                            <asp:ListItem Value="0">Select Shipping Charges</asp:ListItem>
                                            <asp:ListItem Value="1">Payable on deleivery</asp:ListItem>
                                            <asp:ListItem Value="2">Paid recoverable</asp:ListItem>
                                            <asp:ListItem Value="3">Not recoverable</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label ID ="lblStatusCash" runat ="server" Font-Bold ="true" ></asp:Label>
                                    </div>
                                </div>

                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                        <asp:Label runat="server" Text="Communication"></asp:Label>
                                        <asp:DropDownList ID="ddlModeOfCommunicationCS" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Inputed By"></asp:Label>
                                        <asp:TextBox ID="txtInputByCS" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtInputByCs" runat="server" ControlToValidate="txtInputByCS" Display="Dynamic" ErrorMessage="Enter Valid Inputed By." SetFocusOnError="True" ValidationExpression="^[a-zA-Z\s]{1,100}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Shipping Date"></asp:Label>
                                        <asp:TextBox ID="txtShippingDateCS" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender15" runat="server" PopupButtonID="txtShippingDateCS" CssClass="cal_Theme1"
                                            TargetControlID="txtShippingDateCS" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtShippingDateCS" runat="server" ControlToValidate="txtShippingDateCS" Display="Dynamic" ErrorMessage="Enter Valid Shipping Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                    </div>
                                     <div class="col-sm-3 col-md-3">
                                         <asp:DropDownList ID ="ddlExistingCashSaleNo" runat ="server" CssClass ="aspxcontrols"></asp:DropDownList>
                                     </div>
                                </div>
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                        <asp:Label runat="server" Text="Buyer Ref.No"></asp:Label>
                                        <asp:TextBox ID="txtBuyerPurOrderNoCS" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Buyer Ref.Date"></asp:Label>
                                        <asp:TextBox ID="txtBuyerOrderDateCS" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender16" runat="server" PopupButtonID="txtBuyerOrderDateCS"
                                            TargetControlID="txtBuyerOrderDateCS" Format="dd/MM/yyyy" PopupPosition="TopLeft">
                                        </cc1:CalendarExtender>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtBuyerOrderDateCS" runat="server" ControlToValidate="txtBuyerOrderDateCS" Display="Dynamic" ErrorMessage="Enter Valid Buyer Reference Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        
                                    </div>
                                </div>

                            </div>

                            <div id="divDispatch" runat="server">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                        <asp:Label runat="server" Text="* Dispatch No"></asp:Label>
                                        <asp:TextBox ID="txtDispatchNo" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    </div>
                                     <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Dispatch Ref.No"></asp:Label>
                                        <asp:TextBox ID="txtDispatchRefNo" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Dispatch Date"></asp:Label>
                                        <asp:TextBox ID="txtDispatchDate" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender17" runat="server" PopupButtonID="txtDispatchDate" CssClass="cal_Theme1"
                                            TargetControlID="txtDispatchDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label ID ="lblStatusDispatch" runat ="server" Font-Bold ="true" ></asp:Label>
                                    </div>
                                 </div>

                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                        <asp:Label runat="server" Text="* Order No"></asp:Label>
                                        <asp:TextBox ID="txtOrderNoDispatch" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>                                     
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Order Date"></asp:Label>
                                        <asp:TextBox ID="txtOrderDateDispatch" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender18" runat="server" PopupButtonID="txtOrderDateDispatch" CssClass="cal_Theme1"
                                            TargetControlID="txtOrderDateDispatch" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Allocation No"></asp:Label>
                                        <asp:TextBox ID="txtAllocationNoDispatch" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                     <div class="col-sm-3 col-md-3">
                                         <asp:DropDownList ID ="ddlExistingDispatchNo" runat ="server" CssClass ="aspxcontrols"></asp:DropDownList>
                                     </div>
                                 </div>
                            </div>

                            <div id="divInvoice" runat="server">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                        <asp:Label runat="server" Text="* Invoice No"></asp:Label>
                                        <asp:TextBox ID="txtsaleInvoiceNo" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    </div>
                                     <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Invoice Ref.No"></asp:Label>
                                        <asp:TextBox ID="txtSalesInvoiceRefNo" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Invoice Date"></asp:Label>
                                        <asp:TextBox ID="txtSalesInvoiceDate" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender21" runat="server" PopupButtonID="txtSalesInvoiceDate" CssClass="cal_Theme1"
                                            TargetControlID="txtSalesInvoiceDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-3 col-md-3">                                        
                                        <asp:Label ID ="lblStatusInvoice" runat ="server" Font-Bold ="true" ></asp:Label>
                                    </div>
                                 </div>

                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                        <asp:Label runat="server" Text="* Order No"></asp:Label>
                                        <asp:TextBox ID="txtOrderNoInvoice" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>                                     
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Order Date"></asp:Label>
                                        <asp:TextBox ID="txtOrderDateInvoice" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender20" runat="server" PopupButtonID="txtOrderDateInvoice" CssClass="cal_Theme1"
                                            TargetControlID="txtOrderDateInvoice" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                         <asp:Label runat="server" Text="* Dispatch No"></asp:Label>
                                        <asp:TextBox ID="txtDispatchNoInvoice" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>                                    
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Dispatch Date"></asp:Label>
                                        <asp:TextBox ID="txtDispatchDateInvoice" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender19" runat="server" PopupButtonID="txtDispatchDateInvoice" CssClass="cal_Theme1"
                                            TargetControlID="txtDispatchDateInvoice" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
                                        </cc1:CalendarExtender>
                                    </div>
                                 </div>   
                                 <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                     <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                         <asp:Label runat="server" Text="Allocation No"></asp:Label>
                                        <asp:TextBox ID="txtAllocationInvoice" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                     </div>
                                      <div class="col-sm-3 col-md-3">
                                         <asp:DropDownList ID ="ddlExistingSalesInvoiceNo" runat ="server" CssClass ="aspxcontrols"></asp:DropDownList>
                                    </div>  
                                 </div>
                                                            
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-9 col-md-9 form-group pull-left" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                        <div class="col-sm-4 col-md-4 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="* Customer"></asp:Label>
                                            <asp:DropDownList ID="ddlPatryCS" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlPatryCS" runat="server" ControlToValidate="ddlPatryCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Customer" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-1 col-md-1">
                                            <br />
                                            <asp:ImageButton ID="ImageButton4" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                                        </div>
                                        <div class="col-sm-3 col-md-3">
                                            <asp:Label runat="server" Text="Code"></asp:Label>
                                            <asp:TextBox ID="txtPartyNoCS" CssClass="aspxcontrolsdisable" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label ID="Label12" runat="server" Text="Contact No"></asp:Label>
                                            <asp:TextBox ID="txtContactNoCS" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                                        <div class="col-sm-4 col-md-4 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="ddlCategoryCS" runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                             <asp:Label runat="server" Text="*Company Type"></asp:Label>
                                            <asp:DropDownList ID="ddlCompanyTypeCS" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 col-md-4">
                                            <asp:Label runat="server" Text="*GSTN Category"></asp:Label>
                                            <asp:DropDownList ID="ddlGSTCategoryCS" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label ID="Label13" runat="server" Text="Address"></asp:Label>
                                    <asp:TextBox ID="txtAddressCS" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                        <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="Shipping"></asp:Label>
                                            <asp:DropDownList ID="ddlShippingCS" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-6 col-md-6">
                                            <asp:Label runat="server" Text="Sales Person"></asp:Label>
                                            <asp:DropDownList ID="ddlSalesManCS" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                        </div>                                        
                                    </div>

                                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                        <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="* Payment Type"></asp:Label>
                                            <asp:DropDownList ID="ddlPaymentTypeCS" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlPaymentTypeCS" runat="server" ControlToValidate="ddlPaymentTypeCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Payment Type" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                        </div>                                        
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:Label runat="server" Text="Remarks"></asp:Label>
                                    <asp:TextBox ID="txtRemarksCS" runat="server" CssClass="aspxcontrols" Height="80px"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlBranchCS" runat="server" AutoPostBack="true" Enabled="false" Visible="false" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:CheckBox ID="chkboxFromCS" runat="server" AutoPostBack="true" Text="Different" />
                                </div>
                                <div class="col-sm-3 col-md-3">
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:CheckBox ID="chkboxToCS" runat="server" AutoPostBack="true" Text="Different" />
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="Company Address"></asp:Label>
                                    <asp:TextBox ID="txtCompanyAddressCS" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingAddress" runat="server" ControlToValidate="txtBillingAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="Delivered From"></asp:Label>
                                    <asp:TextBox ID="txtDeliveryFromAddressCS" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDeliveryFromAddressCS" runat="server" ControlToValidate="txtDeliveryFromAddressCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery From Address" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="Bill To Address"></asp:Label>
                                    <asp:TextBox ID="txtBillingAddressCS" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Company Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>            --%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="Ship To Address"></asp:Label>
                                    <asp:TextBox ID="txtDeleveryAddressCS" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDeleveryAddressCS" runat="server" ControlToValidate="txtDeleveryAddressCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To Address" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
                                    <asp:TextBox ID="txtCompanyGSTNRegNoCS" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingGSTNRegNo" runat="server" ControlToValidate="txtBillingGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillingGSTNRegNo" runat="server" ControlToValidate="txtBillingGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Billing GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
                                    <asp:TextBox ID="txtDeliveryFromGSTNRegNoCS" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryFromGSTNRegNo" runat="server" ControlToValidate="txtDeliveryFromGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery From GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliveryFromGSTNRegNo" runat="server" ControlToValidate="txtDeliveryFromGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Delivery From GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
                                    <asp:TextBox ID="txtBillingGSTNRegNoCS" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyGSTNRegNo" runat="server" ControlToValidate="txtCompanyGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Company GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyGSTNRegNo" runat="server" ControlToValidate="txtCompanyGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Company GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
                                    <asp:TextBox ID="txtDeliveryGSTNRegNoCS" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryGSTNRegNo" runat="server" ControlToValidate="txtDeliveryGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliveryGSTNRegNo" runat="server" ControlToValidate="txtDeliveryGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Shipping To GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
                                </div>
                            </div>

                            <div class="col-sm-6 col-md-6 form-group pull-left " style="padding: 0px">
                                <asp:Label runat="server" Text="* Commodity"></asp:Label>
                                <asp:DropDownList ID="ddlCommodityCS" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCommodityCS" runat="server" ControlToValidate="ddlCommodityCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Commodity" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>--%>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-6 col-md-6 form-group pull-left " style="padding: 0px">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <asp:Label runat="server" Text="* Item Description"></asp:Label>
                                            <asp:TextBox ID="txtSearchItemCS" onkeyup="FilterItems(this.value)" runat="server" CssClass="aspxcontrols" placeholder="Search By Description" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-12 col-md-12 pre-scrollableborder" style="padding: 0px">
                                            <asp:ListBox ID="lstBoxDescriptionCS" CssClass="aspxcontrols" AutoPostBack="true" runat="server" Enabled="False" Height="200px"
                                                Font-Names="Verdana" Font-Size="Smaller"></asp:ListBox>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVlstBoxDescriptionCS" runat="server" ControlToValidate="lstBoxDescriptionCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select An Item" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-sm-6 col-md-6" style="padding: 0px">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                                            <asp:DropDownList ID="ddlUnitOfMeassurementCS" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlUnitOfMeassurementCS" runat="server" ControlToValidate="ddlUnitOfMeassurementCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Unit Of Meassurement" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label ID="Label14" runat="server" Text="* Rate"></asp:Label>
                                            <asp:TextBox ID="txtMRPCS" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                            <asp:HiddenField ID="HiddenField7" runat="server" />
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtMRPCS" runat="server" ControlToValidate="txtMRPCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Rate" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Quantity"></asp:Label>
                                                <asp:TextBox ID="txtQuantityCashSales" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtQuantityCashSales" runat="server" ControlToValidate="txtQuantityCashSales" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Quantity" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <br />
                                                <asp:TextBox ID="txtAmountCS" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField9" runat="server" />
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtAmountCS" runat="server" ControlToValidate="txtAmountCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="Discount"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlDiscountcs" CssClass="aspxcontrols" runat="server" data-toggle="tooltip" data-placement="top"></asp:DropDownList>

                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <br />
                                                <asp:TextBox ID="txtDiscountAmountCS" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField17" runat="server" />

                                            </div>
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="GST Rate"></asp:Label>
                                                <asp:TextBox ID="txtGSTCS" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>

                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <br />
                                                <asp:TextBox ID="txtGSTAmountCS" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField18" runat="server" />

                                            </div>
                                        </div>

                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="Total Amount"></asp:Label>
                                            <asp:TextBox ID="txtNetAmountCS" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                            <asp:HiddenField ID="HiddenField13" runat="server" />
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtNetAmountCS" runat="server" ControlToValidate="txtNetAmountCS" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Total Amount" ValidationGroup="ValidateCDI"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-12 col-md-12" style="padding-right: 0px">
                                            <div class="pull-right">
                                                <asp:TextBox ID="TextBox17" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtOrderIDCS" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField14" runat="server" />
                                                <asp:HiddenField ID="HiddenField15" runat="server" />
                                                <asp:HiddenField ID="HiddenField16" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divSR" runat="server">
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnRefreshSR" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Reresh16.png" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                                </div>
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnSaveSR" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save16.png" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateSR" />
                                </div>
                                <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnApproveSR" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/CheckMark16.png" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <asp:Label ID="lblStatusSR" runat="server" Text="Status :-"></asp:Label>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:DropDownList ID="ddlExistSalesReturn" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label ID="Label6" runat="server" Text="Return No"></asp:Label>
                                    <asp:TextBox ID="txtReturnNoSR" CssClass="aspxcontrolsdisable" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Return Reference No."></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtGoodsReturnRefNo"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtGoodsReturnRefNo" runat="server" ControlToValidate="txtGoodsReturnRefNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Return ref No" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Return Date"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtReturnDateSR" placeholder="DD/MM/YY"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender10" runat="server" PopupButtonID="txtReturnDateSR" PopupPosition="BottomLeft"
                                        TargetControlID="txtReturnDateSR" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtReturnDateSR" runat="server" ControlToValidate="txtReturnDateSR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Return Date" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                </div>
                                 <div class="col-sm-3 col-md-3">
                                     <asp:Label ID="Label9" runat="server" Text="Customer Name"></asp:Label>
                                    <asp:DropDownList ID="ddlCustomerSR" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCustomerSR" runat="server" ControlToValidate="ddlCustomerSR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Customer" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                 </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* Order No"></asp:Label>
                                    <asp:TextBox ID="txtOrderNoSR" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Dispatch No"></asp:Label>
                                    <asp:TextBox ID="txtDispatchNoSR" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Invoice No"></asp:Label>
                                    <asp:TextBox ID="txtInvoiceNoSR" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtInvoiceDateSR" placeholder="DD/MM/YY"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender22" runat="server" PopupButtonID="txtInvoiceDateSR" PopupPosition="BottomLeft"
                                        TargetControlID="txtInvoiceDateSR" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-5 col-md-5 form-group pull-left" style="padding: 0px">
                                    <asp:Label runat="server" Text="* Commodity"></asp:Label>
                                    <asp:DropDownList ID="ddlCommoditySR" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="col-sm-4 col-md-4">
                                    <asp:Label runat="server" Text=" Ship To"></asp:Label>
                                    <asp:TextBox ID="txtShipTo" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-5 col-md-5 form-group pull-left" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
                                        <div class="col-sm-5 col-md-5 form-group pull-left" style="padding: 0px">
                                            <asp:Label runat="server" Text="* ChargeType"></asp:Label>
                                            <asp:DropDownList ID="ddlChargeTypeSR" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlChargeTypeSR" runat="server" ControlToValidate="ddlChargeTypeSR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Charge Type" ValidationGroup="ValidateAddSR"></asp:RequiredFieldValidator>
                                        </div>

                                        <div class="col-sm-5 col-md-5">
                                            <asp:Label runat="server" Text="* Amt"></asp:Label>
                                            <asp:TextBox ID="txtShippingRateSR" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtShippingRateSR" runat="server" ControlToValidate="txtShippingRateSR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateAddSR"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtShippingRateSR" runat="server" ControlToValidate="txtShippingRateSR" Display="Dynamic" ErrorMessage="Enter Valid Shipping Rate" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="col-sm-1 col-md-1">
                                            <br />
                                            <asp:ImageButton ID="imgbtnAddChargeSR" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Add16.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" ValidationGroup="ValidateAddSR" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-7 col-md-7 form-group pull-right" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group">
                                        <asp:DataGrid ID="GVChargeSR" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable">
                                            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                                            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ChargeID" HeaderText="ChargeID" Visible="False">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="ChargeType" HeaderText="ChargeType">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                </asp:BoundColumn>

                                                <asp:BoundColumn DataField="ChargeAmount" HeaderText="ChargeAmt">
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
                                </div>
                            </div>



                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                    <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                                <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                                                <asp:TextBox autocomplete="off" ID="txtSearchItemSR" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="FilterItems(this.value)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                                            <asp:ListBox ID="lstBoxDescriptionSR" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="305px" ValidationGroup="ValidateSR" Style="left: 0px; top: 0px"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-6" style="padding: 0px">

                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Unit"></asp:Label>
                                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlUnitSR"></asp:DropDownList>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="EFVddlUnitSR" runat="server" ControlToValidate="ddlUnitSR" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Unit" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Rate"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRateSR"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtRateSR" runat="server" ErrorMessage="Enter Rate" ControlToValidate="txtRateSR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="* Return Qty"></asp:Label>
                                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtReturnQuantitySR" ValidationGroup="ValidateSR"></asp:TextBox>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtReturnQuantitySR" runat="server" ErrorMessage="Enter Return Qty" ControlToValidate="txtReturnQuantitySR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                                <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Amount(Qty*Rate)"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtAmountSR"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtAmountSR" runat="server" ErrorMessage="Enter Amount" ControlToValidate="txtAmountSR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="Discount"></asp:Label>
                                               <asp:DropDownList ID ="ddlDiscountSR" runat="server" ></asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="Discount Amount"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtDiscountAmountSR"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                            <div class="col-sm-6 col-md-6">
                                                <asp:Label runat="server" Text="GST Rate %"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtGSTRateSR"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                                <asp:Label runat="server" Text="GST Amount"></asp:Label>
                                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtGSTAmountSR"></asp:TextBox>
                                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="Total Amount"></asp:Label>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtTotalAmountSR"></asp:TextBox>
                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtTotalAmountSR" runat="server" ErrorMessage="Enter Total Amount" ControlToValidate="txtTotalAmountSR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="* Reason For Return"></asp:Label>
                                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlReturn">
                                                <asp:ListItem Value="0">Select Reason For Return</asp:ListItem>
                                                <asp:ListItem Value="1">Expired</asp:ListItem>
                                                <asp:ListItem Value="2">Damaged</asp:ListItem>
                                                <asp:ListItem Value="3">Price Difference</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlReturn" runat="server" ErrorMessage="Select Reason For Return" ControlToValidate="ddlReturn" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSR"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                            <asp:Label runat="server" Text="* Remarks"></asp:Label>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px" ID="txtRemarksSR"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" Visible="false" ID="TextBox15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>


                    <div runat="server" role="tabpanel" class="tab-pane" id="divRPJ">
                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px">
                                <asp:ImageButton ID="imgbtnAddRPJ" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" />
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <asp:ImageButton ID="imgbtnSaveRPJ" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" ImageUrl ="~/Images/Save16.png" title="Save" ValidationGroup="ValidateRPJ" />
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <asp:ImageButton ID="imgbtnApproveRPJ" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" CausesValidation="false" />
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblRPJ" runat="server" ></asp:Label>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblexttrn" runat="server" Text="* ExistingTrn No"></asp:Label>
                                <asp:DropDownList ID="ddlExistingTrnRPJ" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-3 col-md-3 form-group pull-left " style="padding: 0px">
                                <asp:Label ID="lblJEType" runat="server" Text="* JE Type"></asp:Label>
                                <asp:DropDownList ID="ddlJEType" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-3 col-md-3 form-group pull-left " style="padding: 0px">
                                <asp:Label ID="lblReceiptType" runat="server" Text="Receipt Type"></asp:Label>
                                <asp:DropDownList ID="ddlReceiptType" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                               <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlReceiptType" runat="server" ControlToValidate="ddlReceiptType" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Receipt Type." ValidationGroup="ValidateRPJ"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblReceiptTrType" runat="server" Text="* Tr Type"></asp:Label>
                              <%--  <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlReceiptTrType" runat="server" ControlToValidate="ddlReceiptTrType" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Transaction Type." ValidationGroup="ValidateRPJ"></asp:RequiredFieldValidator>--%>
                                <asp:DropDownList ID="ddlReceiptTrType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblReceiptVoucherType" runat="server" Text="* Voucher Type"></asp:Label>
                               <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlReceiptVoucherType" runat="server" ControlToValidate="ddlReceiptVoucherType" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Receipt Voucher Type." ValidationGroup="ValidateRPJ"></asp:RequiredFieldValidator>--%>
                                <asp:DropDownList ID="ddlReceiptVoucherType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblReceiptPaidAmt" runat="server" Text="* Paid Amount"></asp:Label>
                               <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtReceiptPaidAmt" runat="server" ControlToValidate="txtReceiptPaidAmt" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Enter Paid Amount." ValidationGroup="ValidateRPJ"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtReceiptPaidAmt" runat="server" ControlToValidate="txtReceiptPaidAmt" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidateRPJ"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtReceiptPaidAmt" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
                            <div class="col-sm-3 col-md-3 form-group pull-left " style="padding: 0px">
                                <asp:Label ID="lblReceiptInvoiceNo" runat="server" Text="Invoice No."></asp:Label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtReceiptInvoiceNo" runat="server" CssClass="aspxcontrols" Width="85%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblReceiptBillDate" runat="server" Text="Invoice Date"></asp:Label>
                                <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtReceiptBillDate" runat="server" ControlToValidate="txtReceiptBillDate" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidateRPJ"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtReceiptBillDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtBillDate" PopupPosition="BottomLeft"
                                    TargetControlID="txtReceiptBillDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblReceiptInvoiceAmt" runat="server" Text="Invoice Amount"></asp:Label>
                                <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtReceiptInvoiceAmt" runat="server" ControlToValidate="txtReceiptInvoiceAmt" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidateRPJ"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtReceiptInvoiceAmt" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <br />
                                <asp:Label ID="lblAmount" runat="server" CssClass="aspxlabelbold"></asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
                            <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                <asp:Label ID="lblReceiptOrderNo" runat="server" Text="Sales Order No."></asp:Label>
                                <asp:TextBox ID="txtReceiptSalesOrderNo" runat="server" CssClass="aspxcontrols" Width="85%"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblReceiptorderDate" runat="server" Text="Order Date"></asp:Label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtReceiptOrderDate" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                <asp:Label runat="server" Text="* Transaction No"></asp:Label>
                                <asp:TextBox ID="txtTrNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="* Date"></asp:Label>
                                <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="ccInvoiceDate" runat="server" PopupButtonID="txtInvoiceDate" PopupPosition="BottomLeft" TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Enter Date of JE." ValidationGroup="ValidateRPJ"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidateRPJ"></asp:RegularExpressionValidator>
                                <%--<asp:RangeValidator ID="rgvtxtInvoiceDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"></asp:RangeValidator>--%>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="* Cust/Sup/GL"></asp:Label>
                                <asp:DropDownList ID="ddlCustomerParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomerParty" runat="server" ControlToValidate="ddlCustomerParty" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Customer/Supplier/GL." ValidationGroup="ValidateRPJ"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="Label1" Text="* Customer" runat="server"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCSP" runat="server" ControlToValidate="ddlCSP" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Customer/Supplier/Party." ValidationGroup="ValidateRPJ"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlCSP" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                <asp:Label runat="server" Text="* Voucher Type" ID="lblBillTypeRPJ"></asp:Label>
                                <asp:DropDownList ID="ddlBillType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillType" runat="server" ControlToValidate="ddlBillType" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Voucher Type." ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="* Bill No." ID="lblBillNoRPJ"></asp:Label>
                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillNo" runat="server" ControlToValidate="txtBillNo" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Enter Bill No." ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="* Bill Date" ID="lblBillDateRPJ"></asp:Label>
                                <asp:TextBox ID="txtBillDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="cclBillDate" runat="server" PopupButtonID="txtBillDate" PopupPosition="BottomLeft"
                                    TargetControlID="txtBillDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Enter Bill Date." ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REFBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidateAdd"></asp:RegularExpressionValidator>
                                <%--<asp:RangeValidator ID="rgvtxtBillDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtBillDate" SetFocusOnError="True"></asp:RangeValidator>--%>
                            </div>
                            <div class="col-sm-2 col-md-2">
                                <asp:Label runat="server" Text="* Bill Amt" ID="lblBillAmountRPJ"></asp:Label>
                                <asp:TextBox ID="txtBillAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillAmouont" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Enter Bill Amount." ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEBillAmount" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidateAdd"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <br />
                                <asp:ImageButton ID="imgbtnAddBillAmt" runat="server" ValidationGroup="ValidateAdd"></asp:ImageButton>
                            </div>

                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
                            <asp:DataGrid ID="dgPetty" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable">
                                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                                <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="PettyID" HeaderText="PettyID" Visible="False">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="BillNo" HeaderText="BillNo">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="BillDate" HeaderText="BillDate">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundColumn>

                                    <asp:BoundColumn DataField="BillAmount" HeaderText="BillAmount">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:BoundColumn>

                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" ImageUrl="~/Images/Trash16.png" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-5 col-md-5 form-group pull-left " style="padding: 0px">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <legend class="legendbold">Debit Details</legend>

                                        </fieldset>
                                        <div class="form-group">
                                            <label>* Head of Accounts</label>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDrOtherHead" runat="server" ControlToValidate="ddlDrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlDrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>* General Ledger</label>
                                            <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherGL" runat="server" ControlToValidate="ddlDbOtherGL" Display="Dynamic" SetFocusOnError="True"
                                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlDbOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Sub General Ledger</label>
                                            <asp:DropDownList ID="ddlDbOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                                <label>Amount</label>
                                            </div>
                                            <div class="col-sm-11 col-md-11" style="padding: 0px">
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOtherDAmount" runat="server" ControlToValidate="txtOtherDAmount" Display="Dynamic" SetFocusOnError="True"
                                                    ErrorMessage="Enter Amount" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtOtherDAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            </div>

                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDrOtherHead" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherGL" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherSubGL" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="col-sm-1 col-md-1" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnDADD" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateDBAdd" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6 form-group pull-right " style="padding: 0px">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <legend class="legendbold">Credit Details</legend>
                                        </fieldset>
                                        <div class="form-group">
                                            <label>* Head of Accounts</label>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherHead" runat="server" ControlToValidate="ddlCrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>* General Ledger</label>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherGL" runat="server" ControlToValidate="ddlCrOtherGL" Display="Dynamic" SetFocusOnError="True"
                                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCrOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Sub General Ledger</label>
                                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                                            <asp:DropDownList ID="ddlCrOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                                <label>Amount</label>
                                            </div>
                                            <div class="col-sm-11 col-md-11" style="padding: 0px">
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOtherCAmount" runat="server" ControlToValidate="txtOtherCAmount" Display="Dynamic" SetFocusOnError="True"
                                                    ErrorMessage="Enter Amount" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtOtherCAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            </div>

                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherHead" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherGL" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherSubGL" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="col-sm-1 col-md-1" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnOtherCADD" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateCRAdd" />
                                </div>
                            </div>
                            <asp:Label runat="server" Text="Narration"></asp:Label>
                            <asp:TextBox ID="txtNarration" runat="server" Height="50px" CssClass="aspxcontrols" TextMode="MultiLine"></asp:TextBox>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div id="divcollapseChequeDetails" runat="server" visible="true" data-toggle="collapse" data-target="#collapseChequeDetails"><a href="#"><b><i>Click here for Cheque Details</i></b></a></div>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div id="collapseChequeDetails" class="collapse">
                                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                    <div class="col-sm-4 col-md-4">
                                        <asp:Label runat="server" Text="Cheque No."></asp:Label>
                                        <asp:TextBox ID="txtChequeNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:Label runat="server" Text="Cheque Date"></asp:Label>
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

                    </div>

                    <div runat="server" role="tabpanel" class="tab-pane" id="divPayment">
                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-1 col-md-1 form-group pull-left " style="padding: 0px">
                                <asp:ImageButton ID="imgbtnAddPay" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <asp:ImageButton ID="imgbtnSavePay" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save16.png" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidatePay" />
                            </div>
                            <%--<asp:ImageButton ID="imgbtnAttachPay" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="Label4" runat="server" Text="0"></asp:Label></span>--%>
                            <%--<asp:ImageButton ID="imgbtnViewPay" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />--%>
                            <div class="col-sm-1 col-md-1">
                                <asp:ImageButton ID="imgbtnApprovePay" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/CheckMark16.png" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <asp:Label ID="lblStatusPay" runat="server" Text="Status :- " Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:DropDownList ID="ddlExistPayment" runat="server" CssClass="aspxcontrols" ></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                <asp:Label runat="server" Text="* Transaction No"></asp:Label>
                                <asp:TextBox ID="txtTransactionNoPay" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="* Date"></asp:Label>
                                <asp:TextBox ID="txtInvoiceDatePay" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtInvoiceDatePay" PopupPosition="BottomLeft" TargetControlID="txtInvoiceDatePay" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtInvoiceDatePay" runat="server" ControlToValidate="txtInvoiceDatePay" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Enter Date of JE." ValidationGroup="ValidatePay"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtInvoiceDatePay" runat="server" ControlToValidate="txtInvoiceDatePay" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidatePay"></asp:RegularExpressionValidator>--%>
                                <%--<asp:RangeValidator ID="rgvtxtInvoiceDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"></asp:RangeValidator>--%>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Payment Type"></asp:Label>
                                <asp:DropDownList ID="ddlPaymentTypePay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlPaymentTypePay" runat="server" ControlToValidate="ddlPaymentTypePay" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Payment Type." ValidationGroup="ValidatePay"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="* Tr Type"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTransType" runat="server" ControlToValidate="ddlTransType" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Tr Type." ValidationGroup="ValidatePay"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlTransType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-2 col-md-2 form-group pull-left" style="padding: 0px">
                                <asp:Label runat="server" Text="* Pay Type"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlBillTypePay" runat="server" ControlToValidate="ddlBillTypePay" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Payment Voucher Type." ValidationGroup="ValidatePay"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlBillTypePay" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="* Cust/Sup/GL"></asp:Label>
                                <asp:DropDownList ID="ddlCustomerPartyPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCustomerPartyPay" runat="server" ControlToValidate="ddlCustomerPartyPay" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select Customer/Supplier/GL." ValidationGroup="ValidatePay"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="Label2" Text="* Customer" runat="server"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlPartyPay" runat="server" ControlToValidate="ddlPartyPay" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Select" ValidationGroup="ValidatePay"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlPartyPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>

                            <div class="col-sm-2 col-md-2">
                                <asp:Label runat="server" Text="*PaidAmt"></asp:Label>
                                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPaidAmouont" runat="server" ControlToValidate="txtPaidAmount" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Paid Amount." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEPaidAmount" runat="server" ControlToValidate="txtPaidAmount" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtPaidAmount" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-md-2">
                                <asp:Label runat="server" Text="Balance"></asp:Label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtBalanceAmt" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-3 col-md-3 form-group pull-left" style="padding: 0px">
                                <asp:Label ID="lblBillNo1" runat="server" Text="Bill No."></asp:Label>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillNo" runat="server" ControlToValidate="txtBillNo" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill No." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                <div class="form-group">
                                    <asp:TextBox ID="txtBillNoPay" runat="server" CssClass="aspxcontrols" Width="85%"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label ID="lblBillDate1" runat="server" Text="Bill Date"></asp:Label>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                <%-- <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtBillDatePay" runat="server" ControlToValidate="txtBillDatePay" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidatePay"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtBillDatePay" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtBillDatePay" PopupPosition="BottomLeft"
                                    TargetControlID="txtBillDatePay" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <%--<asp:RangeValidator ID="rgvtxtBillDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtBillDate" SetFocusOnError="True"></asp:RangeValidator>--%>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label ID="lblBillAmount1" runat="server" Text="Bill Amount as per supplier"></asp:Label>
                                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillAmouont" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill Amount." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtBillAmountPay" runat="server" ControlToValidate="txtBillAmountPay" Display="Dynamic"
                                    SetFocusOnError="True" ValidationGroup="ValidatePay"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtBillAmountPay" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:TextBox>
                            </div>

                        </div>

                        <div class="col-md-12">
                            <div id="DivcollapseChequeDetailsPay" runat="server" visible="false" data-toggle="collapse" data-target="#collapseChequeDetailsPay"><a href="#"><b><i>Click here to view Cheque Details...</i></b></a></div>
                        </div>
                        <div class="col-md-12" style="padding-left: 0px">
                            <div id="collapseChequeDetailsPay" class="collapse">
                                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                    <div class="col-sm-4 col-md-4">
                                        <asp:Label runat="server" Text="Cheque No."></asp:Label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtChequeNoPay" runat="server" ControlToValidate="txtChequeNoPay" Display="Dynamic"
                                            SetFocusOnError="True" ValidationGroup="ValidatePay"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtChequeNoPay" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:Label runat="server" Text="Cheque Date"></asp:Label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtChequeDatePay" runat="server" ControlToValidate="txtChequeDatePay" Display="Dynamic"
                                            SetFocusOnError="True" ValidationGroup="ValidatePay"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtChequeDatePay" runat="server" autocomplete="off" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtChequeDatePay" PopupPosition="BottomLeft"
                                            TargetControlID="txtChequeDatePay" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:Label runat="server" Text="IFSC Code"></asp:Label>
                                        <asp:TextBox ID="txtIFSCPay" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-12" style="padding: 0px">
                                    <div class="col-sm-4 col-md-4">
                                        <asp:Label runat="server" Text="Bank Name"></asp:Label>
                                        <asp:DropDownList ID="ddlBankPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <asp:Label runat="server" Text="Branch Name"></asp:Label>
                                        <asp:TextBox ID="txtBranchPay" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                            <div class="col-sm-5 col-md-5 form-group pull-left " style="padding: 0px">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <legend class="legendbold">Debit Details</legend>

                                        </fieldset>
                                        <div class="form-group">
                                            <label>* Head of Accounts</label>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDrOtherHeadPay" runat="server" ControlToValidate="ddlDrOtherHeadPay" Display="Dynamic" SetFocusOnError="True"
                                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlDrOtherHeadPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>* General Ledger</label>
                                            <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherGLPay" runat="server" ControlToValidate="ddlDbOtherGLPay" Display="Dynamic" SetFocusOnError="True"
                                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlDbOtherGLPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Sub General Ledger</label>
                                            <asp:DropDownList ID="ddlDbOtherSubGLPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                                <label>Amount</label>
                                            </div>
                                            <div class="col-sm-11 col-md-11" style="padding: 0px">
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtOtherDAmountPay" Display="Dynamic" SetFocusOnError="True"
                                                    ErrorMessage="Enter Amount" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtOtherDAmountPay" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            </div>

                                        </div>

                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDrOtherHeadPay" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherGLPay" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherSubGLPay" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="col-sm-1 col-md-1" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnDADDPay" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateDBAdd" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6 form-group pull-right " style="padding: 0px">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <fieldset>
                                            <legend class="legendbold">Credit Details</legend>
                                        </fieldset>
                                        <div class="form-group">
                                            <label>* Head of Accounts</label>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherHeadPay" runat="server" ControlToValidate="ddlCrOtherHeadPay" Display="Dynamic" SetFocusOnError="True"
                                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCrOtherHeadPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>* General Ledger</label>
                                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherGLPay" runat="server" ControlToValidate="ddlCrOtherGLPay" Display="Dynamic" SetFocusOnError="True"
                                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlCrOtherGLPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Sub General Ledger</label>
                                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                                            <asp:DropDownList ID="ddlCrOtherSubGLPay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                                <label>Amount</label>
                                            </div>
                                            <div class="col-sm-11 col-md-11" style="padding: 0px">
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOtherCAmountPay" runat="server" ControlToValidate="txtOtherCAmountPay" Display="Dynamic" SetFocusOnError="True"
                                                    ErrorMessage="Enter Amount" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtOtherCAmountPay" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                            </div>

                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherHeadPay" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherGLPay" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherSubGLPay" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="col-sm-1 col-md-1" style="padding: 0px">
                                    <asp:ImageButton ID="imgbtnOtherCADDPay" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateCRAdd" />
                                </div>
                            </div>
                            <asp:Label runat="server" Text="Narration"></asp:Label>
                            <asp:TextBox ID="txtNarrationPay" runat="server" Height="50px" CssClass="aspxcontrols" TextMode="MultiLine"></asp:TextBox>
                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 table-bordered" style="padding-left: 0px; padding-right: 0px;">
        <div class="tab-content divmargin">
            <div runat="server" role="tabpanel" class="tab-pane active" id="divPurchaseGrid">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:GridView ID="dgPurchase" runat="server" AutoGenerateColumns="False" class="footable">
                        <Columns>
                            <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="CommodityID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DescriptionID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescriptionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescriptionID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HistoryID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UnitsID" HeaderText="UnitsID" Visible="false"></asp:BoundField>

                            <asp:BoundField DataField="Slno" HeaderText="Slno" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                            <asp:TemplateField HeaderText="Goods" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Units" HeaderText="Units" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="Rate" HeaderText="Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="RateAmount" HeaderText="RateAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="Frieght" Visible="false" HeaderText="Frieght" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="Discount" HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="DiscountAmt" HeaderText="DiscountAmt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="ExciseDuty" Visible="false" HeaderText="ExciseDuty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="ExciseAmt" Visible="false" HeaderText="ExciseAmt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="VAT" Visible="false" HeaderText="VAT" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="VATAmt" Visible="false" HeaderText="VATAmt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="CST" Visible="false" HeaderText="CST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="CSTAmount" Visible="false" HeaderText="CSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>

                            <asp:BoundField DataField="GSTID" Visible="false" HeaderText="GSTID" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="GSTRate" HeaderText="GSTRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="GSTAmount" HeaderText="GSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="SGST" Visible="false" HeaderText="SGST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="SGSTAmount" Visible="false" HeaderText="SGSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="CGST" Visible="false" HeaderText="CGST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="CGSTAmount" Visible="false" HeaderText="CGSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="IGST" Visible="false" HeaderText="IGST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="IGSTAmount" Visible="false" HeaderText="IGSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>

                            <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete1" CssClass="hvr-bounce-in" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="Edit1" runat="server" CssClass="hvr-bounce-in" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:GridView ID="dgInward" runat="server" AutoGenerateColumns="False" class="footable">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ComodityId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblComodityId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ComodityId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HistoryID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnitId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitId") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Comodity" HeaderText="Commodity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>

                            <asp:TemplateField HeaderText="Descriptions" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Descriptions") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Units" HeaderText="Units" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="Mrp" HeaderText="Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                            <asp:BoundField DataField="OrderedQty" HeaderText="OrderedQty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="BatchNumber" HeaderText="BatchNumber" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="AccpetedQty" HeaderText="AccpetedQty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="RejectedQty" HeaderText="RejectedQty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="ExcessQty" HeaderText="ExcessQty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="MDate" HeaderText="MDate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                            <asp:BoundField DataField="EDate" HeaderText="EDate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Images/Trash16.png" CommandName="Delete" CssClass="hvr-bounce-in" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" ImageUrl="~/Images/Edit16.png" CommandName="Edit" runat="server" CssClass="hvr-bounce-in" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:GridView ID="dgPurchaseReturn" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CommodityID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HistoryID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnitID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="20%" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="10%" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Unit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="10%" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRateAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RateAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscountAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount") %>'></asp:Label>
                                    <asp:HiddenField ID="HFDiscountAmount" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Charges" HeaderStyle-Width="10%" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCharges" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Charges") %>'></asp:Label>
                                    <asp:HiddenField ID="HFCharges" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:Label>
                                    <asp:HiddenField ID="HFTotalAmount" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GSTID" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GSTRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTRate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GST Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>
                                    <asp:HiddenField ID="HFGSTAmount" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" HeaderStyle-Width="10%" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblFinalTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FinalTotal") %>'></asp:Label>
                                    <asp:HiddenField ID="HFFinalTotal" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" ImageUrl="~/Images/Edit16.png" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" Visible="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>


            </div>

            <div runat="server" role="tabpanel" class="tab-pane active" id="divSalesGrid">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:GridView ID="dgExistingProFormaSalesOrder" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                        <Columns>
                            <asp:TemplateField HeaderText="CommodityID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SlNo" HeaderText="Sl.no" HeaderStyle-Width="1%"></asp:BoundField>

                            <asp:TemplateField HeaderText="Description Of Goods" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="30%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="UnitOfMeassurement" HeaderText="Unit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="1%"></asp:BoundField>
                            <asp:BoundField DataField="MRPAmount" HeaderText="Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="NetAmount" HeaderText="Basic Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="8%"></asp:BoundField>
                            <asp:BoundField DataField="Discount" Visible="false" HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                            <asp:BoundField DataField="DiscountAmount" Visible="false" HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                            <asp:BoundField DataField="VAT" Visible="false" HeaderText="VAT" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                            <asp:BoundField DataField="VATAmount" Visible="false" HeaderText="VAT Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%"></asp:BoundField>
                            <asp:BoundField DataField="CST" Visible="false" HeaderText="CST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                            <asp:BoundField DataField="CSTAmount" Visible="false" HeaderText="CST Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                            <asp:BoundField DataField="Excise" Visible="false" HeaderText="Excise" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                            <asp:BoundField DataField="ExciseAmount" Visible="false" HeaderText="Excise Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%"></asp:BoundField>
                            <asp:BoundField DataField="Amount" HeaderText="Total Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="8%"></asp:BoundField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ibtnCancel" runat="server" CommandName="Cancel" Height="16px" ImageUrl="~/Images/Trash16.png" ToolTip="Delete/Cancel" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PKID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPKID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PKID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:GridView ID="grdDispatch" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                        <Columns>
                            <asp:TemplateField HeaderText="CommodityID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HistoryID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnitID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goods" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Unit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblMRP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtplacedqty" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlacedQuantity") %>' ReadOnly="false"></asp:TextBox>--%>
                                    <asp:Label ID="lblOrderedQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderedQty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GSTID" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GSTRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTRate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:GridView ID="grdDispatchDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                        <Columns>
                            <asp:TemplateField HeaderText="CommodityID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ItemID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HistoryID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UnitID" Visible="false" HeaderStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Commodity" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommodity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Commodity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goods" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="4%">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Unit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MRP" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblMRP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <%--<asp:TextBox ID="txtplacedqty" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlacedQuantity") %>' ReadOnly="false"></asp:TextBox>--%>
                                    <asp:Label ID="lblOrderedQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderedQty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlDiscount" CssClass="aspxcontrols" AutoPostBack="true" runat="server"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiscountAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount") %>'></asp:Label>
                                    <asp:HiddenField ID="HFDiscountAmount" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Charges" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCharges" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Charges") %>'></asp:Label>
                                    <asp:HiddenField ID="HFCharges" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                                    <asp:HiddenField ID="HFAmount" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GSTID" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GSTRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTRate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GST Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>
                                    <asp:HiddenField ID="HFGSTAmount" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Net Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblNetAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount") %>'></asp:Label>
                                    <asp:HiddenField ID="HFNetAmount" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:GridView ID="dgSRItemDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                    <asp:Label ID="lblMasterID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MasterID") %>'></asp:Label>
                                    <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommodityID") %>'></asp:Label>
                                    <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemID") %>'></asp:Label>
                                    <asp:Label ID="lblReasonID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReasonID") %>'></asp:Label>
                                    <asp:Label ID="lblUnitID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitID") %>'></asp:Label>
                                    <asp:Label ID="lblDiscountID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DiscountID") %>'></asp:Label>
                                    <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GSTID") %>'></asp:Label>
                                    <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.HistoryID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SlNo" HeaderText="Sl.No" HeaderStyle-Width="2%">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Commodity" HeaderText="Commodity" HeaderStyle-Width="10%">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Goods" HeaderText="Goods" HeaderStyle-Width="10%">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Unit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblQTY" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QTY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Rate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RReturn" HeaderText="Reason For Return" HeaderStyle-Width="7%">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Discount" HeaderText="Discount" HeaderStyle-Width="6%">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Total Discount" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotDiscount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotDiscount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Charges" HeaderStyle-Width="6%" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCharges" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Charges") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GST Rate %" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGst" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Gst") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GST Amount" HeaderStyle-Width="6%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GSTAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="7%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotAmt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="GrdEdit" runat="server" ToolTip="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>

            <div runat="server" role="tabpanel" class="tab-pane active" id="divRPJGrid">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                </div>
            </div>

        </div>
    </div>


    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgPaymentDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

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

                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="12%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="DebitOrCredit" HeaderText="DebitOrCredit" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" ImageUrl="~/Images/Trash16.png" Width="12%" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>

        <asp:DataGrid ID="dgPettyCashDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <%--  <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

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

                <%--   <asp:BoundColumn DataField="PaymentID" HeaderText="PaymentID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

                <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <%--    <asp:BoundColumn DataField="Type" HeaderText="Type">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="11%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

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
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="12%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Balance" HeaderText="Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDeletePetty" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="DeletePetty" data-placement="bottom" runat="server" ImageUrl="~/Images/Trash16.png" Width="12%" />
                    </ItemTemplate>
                </asp:TemplateColumn>

            </Columns>
        </asp:DataGrid>


        <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="txtPreId" runat="server" Visible="false"></asp:TextBox>
        <asp:ListBox ID="lstDocument" runat="server" Visible="false"></asp:ListBox>
        <asp:ListBox ID="lstFiles" runat="server" Visible="false"></asp:ListBox>
        <asp:Label ID="lblDocID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblFileID" runat="server" Visible="false"></asp:Label>
        <asp:DropDownList ID="ddlCabinet" runat="server" Visible="false"></asp:DropDownList>
        <asp:DropDownList ID="ddlSubCabinet" runat="server" Visible="false"></asp:DropDownList>
        <asp:DropDownList ID="ddlFolder" runat="server" Visible="false"></asp:DropDownList>
    </div>

    <div id="mySendMail" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="pull-left">
                                <h4 class="modal-title"><b>Send Mail</b></h4>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="pull-right">
                                <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblSendMailModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblEmailFrom" runat="server" Text="* From"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmailFrom" runat="server" ControlToValidate="txtEmailFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtEmailFrom" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmailFrom" runat="server" ControlToValidate="txtEmailFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblPassword" runat="server" Text="* Password"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVPassword" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPassword" autocomplete="off" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblEmailTo" runat="server" Text="* To"></asp:Label>
                                <asp:TextBox ID="txtEmailTo" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmailTo" runat="server" ControlToValidate="txtEmailTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblUsers" runat="server" Text="* Users"></asp:Label>
                                <br />
                                <asp:ListBox ID="lstUsers" runat="server" Width="100%" SelectionMode="Multiple" CssClass="aspxcontrols1"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblSubject" runat="server" Text="* Subject"></asp:Label>
                            <asp:RequiredFieldValidator ID="RFVSubject" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtSubject" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSubject" runat="server" ControlToValidate="txtSubject" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblBody" runat="server" Text="* Body"></asp:Label>
                            <asp:RequiredFieldValidator ID="RFVBody" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtBody" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtBody" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBody" runat="server" ControlToValidate="txtBody" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="Send Mail" class="btn-ok" ID="btnSendMail" ValidationGroup="Validate"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnCancelMail"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-6 col-md-6" style="padding-right: 0px">
        <div class="form-group">
        </div>
    </div>
    <div class="col-sm-6 col-md-6" style="padding-left: 0px">
        <div class="form-group">
        </div>
    </div>

    <div class=" modal fade" id="myAttchment" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Attachment</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblTax" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-5 col-md-5" style="padding-left: 0px">
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="95%" CssClass="btn-ok" AllowMultiple="true" />
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button ID="btnAttch" runat="server" Text="Add" CssClass="btn-ok" />
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button ID="btnIndex" runat="server" Text="Index" CssClass="btn-ok" />
                        </div>
                    </div>


                    <div class="col-sm-12 col-md-12" runat="server">
                        <asp:GridView ID="gvattach" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="1%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPath" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                                <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                </div>

            </div>
        </div>
    </div>


    <div id="myModalIndex" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Index Details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-12 col-md-12">
                            <div class="pull-left">
                                <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblcabinet" runat="server" Text="Cabinet"></asp:Label>
                                <asp:DropDownList ID="DropDownList17" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblSubcabinet" runat="server" Text="Sub cabinet"></asp:Label>
                                <asp:DropDownList ID="DropDownList18" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblFolder" runat="server" Text="Folder"></asp:Label>
                                <asp:DropDownList ID="DropDownList19" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblDocumentType" runat="server" Text="Document Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="Label3" runat="server" Text="Date"></asp:Label>
                                <asp:Label ID="lblDateDisplay" runat="server" CssClass="aspxlabelbold"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>

                        </div>

                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>

                                    <asp:TemplateField HeaderStyle-Width="1%" HeaderText="DescriptorID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescriptorID" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DescriptorID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descriptor" HeaderText="Descriptor" HeaderStyle-Width="40%"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvKeywords" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Keywords" HeaderStyle-Width="100%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Key") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:ImageButton ID="imgbtnIndexSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index" />
                </div>
            </div>
        </div>
    </div>
    
    <asp:Label ID="lblDescID" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblScode" runat="server" Visible="False"></asp:Label>
    <asp:TextBox ID="txtPices" runat="server" Height="16px" Visible="False" Width="16px"></asp:TextBox>
    <asp:TextBox ID="txtHistoryID" runat="server" Height="16px" Width="17px" Visible="False"></asp:TextBox>
    <asp:HiddenField ID="hfTotalPieces" runat="server" Visible="False" />
    <asp:TextBox ID="txtMasterID" runat="server" Height="16px" Width="17px" Visible="False"></asp:TextBox>
    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtSprCode" Visible="False"></asp:TextBox>
    <asp:TextBox ID ="txtGLID" runat ="server" Visible ="false" ></asp:TextBox>

    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsg" runat="server"></asp:Label></strong>
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

