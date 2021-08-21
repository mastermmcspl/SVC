<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Accounts.master" CodeFile="PaymentTransactionDetails.aspx.vb" Inherits="Accounts_PaymentTransactionDetails"
    ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
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
         div {
            color: black;
        }
    </style>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/bootstrap-multiselect.css" rel="stylesheet" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

           <%-- $('#<%=ddlBillNo.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });--%>
            $('#<%=ddlExistPayment.ClientID%>').select2();
            $('#<%=ddlParty.ClientID%>').select2();
            $('#<%=ddlCustomerParty.ClientID%>').select2();
            $('#<%=ddlTransType.ClientID%>').select2();
            $('#<%=ddlBillType.ClientID%>').select2();
            $('#<%=ddlDrOtherHead.ClientID%>').select2();
            $('#<%=ddlDbOtherGL.ClientID%>').select2();
            $('#<%=ddlDbOtherSubGL.ClientID%>').select2();
            $('#<%=ddlCrOtherHead.ClientID%>').select2();
            $('#<%=ddlCrOtherGL.ClientID%>').select2();
            $('#<%=ddlCrOtherSubGL.ClientID%>').select2();
            $('#<%=ddlCurrency.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })


        $(function () {
            $(":file").change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();
                    reader.onload = imageIsLoadedS;
                    reader.readAsDataURL(this.files[0]);
                }
            });
        });

        function imageIsLoadedS(e) {
            $('#myImgS').attr('src', e.target.result);
        };
        function Setting() {
            $('#myModal').modal('show');
        };

        //function disp_confirm() {
        //    $('#ModalBillAdjusment').modal('hide');
        //}

        //var decimalOnly = /^\s*-?[0-9]\d*(\.\d{1,2})?\s*$/;
        //function CalculateAmount(sPendingAmt, sAmountPaid) {
        //    var ssAmountPaid = document.getElementById(sAmountPaid).value
        //    var ssPendingAmt = document.getElementById(sPendingAmt).innerHTML
        //    alert(ssAmountPaid)
        //    alert(ssPendingAmt)

        //    if ((ssAmountPaid) != "") {
        //        var num
        //        num = decimalOnly.test(ssAmountPaid)
        //        if (num == false) {
        //            alert("Enter integers/Decimals for Placed Quantity.")
        //            document.getElementById(sAmountPaid).value = ""
        //            document.getElementById(sAmountPaid).focus();
        //            return false;
        //        }

        //        document.getElementById(sPendingAmt).innerText = parseFloat(ssPendingAmt - ssAmountPaid).toFixed(2);
        //        return true;
        //    }

        //}
    </script>

    <script type="text/javascript">
        function Closepopup() {
            $('#ModalBillAdjusment').modal('hide');

        }
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Payment Transaction Details</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnConfirm" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Confirm" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
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

    <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

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
                <asp:Label runat="server" Text="Existing Payment Voucher"></asp:Label>
                <asp:DropDownList ID="ddlExistPayment" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Date of Payment"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Date of Payment." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                <cc1:CalendarExtender ID="ccInvoiceDate" runat="server" PopupButtonID="txtInvoiceDate" PopupPosition="BottomLeft" TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%--<asp:RangeValidator ID="rgvtxtInvoiceDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"></asp:RangeValidator>--%>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Payment Type"></asp:Label>
                <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlPaymentType" runat="server" ControlToValidate="ddlPaymentType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Payment Type." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>

            <div class="col-sm-3 col-md-3 pull-right">
                <asp:Label runat="server" Text="Status:- " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
                <br />
                <asp:Label ID="lblAmount" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Transaction No"></asp:Label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Transaction Type"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTransType" runat="server" ControlToValidate="ddlTransType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Transaction Type." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlTransType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                </asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Payment Voucher Type"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillType" runat="server" ControlToValidate="ddlBillType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Payment Voucher Type." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlBillType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Customer/Supplier/GL"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomerParty" runat="server" ControlToValidate="ddlCustomerParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Customer/Supplier/GL." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlCustomerParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true" CausesValidation="false" ValidationGroup="Validate"></asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblParty" Text="* Customer" runat="server"></asp:Label>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVParty" runat="server" ControlToValidate="ddlParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Customer/Supplier/Party." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Paid Amount"></asp:Label>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPaidAmouont" runat="server" ControlToValidate="txtPaidAmount" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Paid Amount." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEPaidAmount" runat="server" ControlToValidate="txtPaidAmount" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                <asp:TextBox ID="txtPaidAmount" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:TextBox>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Balance Amount"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtBalanceAmt" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblCurrency" runat="server" Text="* Currency"></asp:Label>
                <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="true" CssClass="aspxcontrols" TabIndex="3">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblFEAmt" runat="server" Text="* FE Amount" Visible="false"></asp:Label>
                <asp:TextBox ID="txtFEAmt" runat="server" CssClass="aspxcontrolsdisable" Visible="false"></asp:TextBox>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblDiffAmount" runat="server" Text="* FE Differnce Amount" Visible="false"></asp:Label>
                <asp:TextBox ID="txtDiffAmount" runat="server" CssClass="aspxcontrolsdisable" Visible="false" Value="0"></asp:TextBox>
            </div>
            <div class="col-sm-3 col-md-3">
                <br />
                <asp:Label ID="lblFEStat" runat="server" Text="FE Status:- " Font-Bold="true" Visible="false"></asp:Label>
                <asp:Label ID="lblFEStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblBillNo" runat="server" Text="Bill No."></asp:Label>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillNo" runat="server" ControlToValidate="txtBillNo" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill No." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <div class="form-group">
                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="aspxcontrols" Width="85%"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnSearch" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Bill No" />
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label ID="lblBillDate" runat="server" Text="Bill Date"></asp:Label>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REFBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtBillDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                <cc1:CalendarExtender ID="cclBillDate" runat="server" PopupButtonID="txtBillDate" PopupPosition="BottomLeft"
                    TargetControlID="txtBillDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%--<asp:RangeValidator ID="rgvtxtBillDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtBillDate" SetFocusOnError="True"></asp:RangeValidator>--%>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label ID="lblBillAmount" runat="server" Text="Bill Amount as per supplier"></asp:Label>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillAmouont" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill Amount." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEBillAmount" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtBillAmount" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:TextBox>
            </div>

        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblOrderNo" runat="server" Text="Existing Order No."></asp:Label>
                <asp:DropDownList ID="ddlOrderNo" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblorderDate" runat="server" Text="Order Date"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtOrderDate" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                </div>
            </div>

        </div>
        <div class="col-sm-12 col-md-12 form-group">
            <div id="DivBill" runat="server" data-toggle="collapse" data-target="#collapseBill"><a href="#"><b><i>Bill Details...</i></b></a></div>
        </div>
        <div id="collapseBill" class="collapse">
            <div class="col-sm-11 col-md-11 form-group" style="padding: 0px">
                <asp:GridView ID="GVDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                                <asp:Label ID="lblBillID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PMID") %>'></asp:Label>
                                <asp:Label ID="lblBillName" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BillNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PMID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPMID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VoucherNO" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblVoucherNO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VoucherNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BillNO" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblBillDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-- <asp:TemplateField HeaderText="Amount Paid" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                <ItemTemplate>                                    
                                    <asp:TextBox ID="txtAmountPaid" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AmountPaid") %>' ReadOnly="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Pending Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblPending" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Pending") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnAddBillAmount" runat="server" Text="Ok" CssClass="btn-ok" />
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group">
            <div id="DivBillManual" runat="server" data-toggle="collapse" data-target="#collapseBillManual"><a href="#"><b><i>Bill Details...</i></b></a></div>
        </div>
        <div id="collapseBillManual" class="collapse">
            <div class="col-sm-11 col-md-11 form-group" style="padding: 0px">
                <asp:GridView ID="GVBillManual" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                                <asp:Label ID="lblBillID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PMID") %>'></asp:Label>
                                <asp:Label ID="lblBillName" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BillNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PMID" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPMID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="VoucherNO" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblVoucherNO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VoucherNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BillNO" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblBillDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount Paid" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:TextBox ID="txtAmountPaid" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AmountPaid") %>' ReadOnly="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pending Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                            <ItemTemplate>
                                <asp:Label ID="lblPending" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Pending") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnAddManualBillAMT" runat="server" Text="Ok" CssClass="btn-ok" />
            </div>
        </div>
    </div>

    <%-- <div class="col-sm-12 col-md-12 form-group">
        <div id="divcollapseRP" runat="server" data-toggle="collapse" data-target="#collapseRP"><a href="#"><b><i>Other Details...</i></b></a></div>
    </div>--%>
    <%--<div id="collapseRP" class="collapse">--%>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Debit Details</legend>
                        </fieldset>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDrOtherHead" runat="server" ControlToValidate="ddlDrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true" >
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherGL" runat="server" ControlToValidate="ddlDbOtherGL" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDbOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="140px"></asp:DropDownList>
                         <asp:ImageButton ID="imgbtnDrOtherGL" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherSubGL" runat="server" ControlToValidate="ddlDbOtherSubGL" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlDbOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="140px"></asp:DropDownList>
                        <asp:ImageButton ID="imgbtnDrOtherSubGL" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                        </div>

                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOtherDAmount" runat="server" ControlToValidate="txtOtherDAmount" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Enter Amount" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOtherDAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDrOtherHead" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherGL" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherSubGL" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
                    <asp:ImageButton ID="imgbtnDADD" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateDBAdd" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Credit Details</legend>
                        </fieldset>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherHead" runat="server" ControlToValidate="ddlCrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherGL" runat="server" ControlToValidate="ddlCrOtherGL" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCrOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="140px"></asp:DropDownList>
                            <asp:ImageButton ID="imgbtnCrOtherGL" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherSubGL" runat="server" ControlToValidate="ddlCrOtherSubGL" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Head of Accounts" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlCrOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="140px"></asp:DropDownList>
                            <asp:ImageButton ID="imgbtnCrOtherSGL" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOtherCAmount" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Enter Amount" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOtherCAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherHead" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherGL" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherSubGL" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
                    <asp:ImageButton ID="imgbtnOtherCADD" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateCRAdd" />
                </div>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Narration"></asp:Label>
            <asp:TextBox ID="txtNarration" runat="server" CssClass="aspxcontrols" Height="140px" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <%-- </div>--%>


    <div class="modal fade" id="myModalBillList" tabindex="-1" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Bill Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12 pull-left">
                        <asp:Label ID="Label1" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Bill No"></asp:Label>
                                <asp:CheckBoxList ID="chkBillNo" runat="server" Height="100px" CssClass="aspxcontrols"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnNewBill" runat="server" Text="New" CssClass="btn-ok" />
                    <asp:Button ID="btnOkBill" runat="server" Text="Ok" CssClass="btn-ok" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div id="divcollapseChequeDetails" runat="server" visible="false" data-toggle="collapse" data-target="#collapseChequeDetails"><a href="#"><b><i>Click here to view Cheque Details...</i></b></a></div>
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
   
    <div class="col-md-12">
        <div id="divcollapseFundDetails" runat="server" visible="false" data-toggle="collapse" data-target="#collapseFundDetails"><a href="#"><b><i>Click here to view Transfer Fund Details...</i></b></a></div>
    </div>
     <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseFundDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Fund Transfer Reference No."></asp:Label>
                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEFundNo" runat="server" ControlToValidate="txtFundTransferNo" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                    <asp:TextBox ID="txtFundTransferNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Fund Transfer Date"></asp:Label>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REFFundDate" runat="server" ControlToValidate="txtFundTransferDate" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtFundTransferDate" runat="server" autocomplete="off" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtFundTransferDate" PopupPosition="BottomLeft"
                        TargetControlID="txtFundTransferDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                    </cc1:CalendarExtender>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalFASPayment" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblPaymentValidataionMsg" runat="server"></asp:Label>
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
    <%--    </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlExistPayment" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlParty" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlTransType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBillType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPaymentType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddldbHead" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddldbGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddldbsUbGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCrHead" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCrGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCrSubGL" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>--%>
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
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" Width="5%" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
     
    
    <div class="col-md-12">
        <div id="DivAdvance" runat="server" visible="false" data-toggle="collapse" data-target="#collapseAdvance"><a href="#"><b><i>Click here to view Advance...</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseAdvance" class="collapse">
            <div class="col-sm-12 col-md-12">
                <asp:DataGrid ID="dgAdvance" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="100%" class="footable">
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
                        <asp:BoundColumn DataField="PO" HeaderText="PO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BillNo" HeaderText="Bill No">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>
    
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">General Ledger Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblErrorGl" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Head"></asp:Label>
                        <asp:DropDownList ID="ddlHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Group"></asp:Label>
                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="Sub Group"></asp:Label>
                        <asp:DropDownList ID="ddlSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCode" runat="server" ControlToValidate="txtCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Click on Add button" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" Text="Code"></asp:Label>
                        <asp:TextBox ID="txtCode" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Name"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVName" runat="server" ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEName" runat="server" ControlToValidate="txtName" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtName" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescription" runat="server" ControlToValidate="txtGlDesc" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtGlDesc" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                     
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Label runat="server" Text="" ID="lblModalGlId" Visible="false"></asp:Label>
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDescNew"></asp:Button>
                         <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDescUpdate"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDescSave" ></asp:Button>
                        <asp:Button runat="server" Text="Activate" class="btn-ok"  ID="btnDescActivate" ></asp:Button>
                        <asp:Button runat="server" Text="DeActivate" class="btn-ok"  ID="btnDescDeActivate" ></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalSGL" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Sub General Ledger Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblErrorSGL" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Head"></asp:Label>
                        <asp:DropDownList ID="ddlHeadSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Group"></asp:Label>
                        <asp:DropDownList ID="ddlGroupSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="Sub Group"></asp:Label>
                        <asp:DropDownList ID="ddlSubGroupSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                     <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="General Ledger"></asp:Label>
                        <asp:DropDownList ID="ddlGLSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCodeSgl" runat="server" ControlToValidate="txtCodeSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Click on Add button" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" Text="Code"></asp:Label>
                        <asp:TextBox ID="txtCodeSgl" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Name"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNameSgl" runat="server" ControlToValidate="txtNameSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVNameSgl1" runat="server" ControlToValidate="txtNameSgl" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtNameSgl" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescSgl" runat="server" ControlToValidate="txtDescSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDescSgl" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Label runat="server" Text="" ID="lblModalSglId" Visible="false"></asp:Label>
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnNewSgl"></asp:Button>
                       <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnUpdateSgl"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSaveSgl" ></asp:Button>
                        <asp:Button runat="server" Text="Activate" class="btn-ok"  ID="btnActivateSgl" ></asp:Button>
                        <asp:Button runat="server" Text="DeActivate" class="btn-ok"  ID="btnDeactivateSgl" ></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalCRGL" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">General Ledger Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblErrorCRGl" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Head"></asp:Label>
                        <asp:DropDownList ID="ddlCRGLHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Group"></asp:Label>
                        <asp:DropDownList ID="ddlCRGLGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="Sub Group"></asp:Label>
                        <asp:DropDownList ID="ddlCRGLSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCRGLCode" runat="server" ControlToValidate="txtCRGLCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Click on Add button" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" Text="Code"></asp:Label>
                        <asp:TextBox ID="txtCRGLCode" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Name"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVECRGLName" runat="server" ControlToValidate="txtCRGLName" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtCRGLName" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCRGLDescription" runat="server" ControlToValidate="txtCRGlDesc" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtCRGlDesc" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                     
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Label runat="server" Text="" ID="lblModalCRGlId" Visible="false"></asp:Label>
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDescNewCRGL"></asp:Button>
                         <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDescUpdateCRGL"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDescSaveCRGL" ></asp:Button>
                        <asp:Button runat="server" Text="Activate" class="btn-ok"  ID="btnDescActivateCRGL" ></asp:Button>
                        <asp:Button runat="server" Text="DeActivate" class="btn-ok"  ID="btnDescDeActivateCRGL" ></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalCRSGL" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Sub General Ledger Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblErrorCRSGL" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Head"></asp:Label>
                        <asp:DropDownList ID="ddlHeadCRSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Group"></asp:Label>
                        <asp:DropDownList ID="ddlGroupCRSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="Sub Group"></asp:Label>
                        <asp:DropDownList ID="ddlSubGroupCRSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                     <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="General Ledger"></asp:Label>
                        <asp:DropDownList ID="ddlGLCRSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REFVCodeCRSGL" runat="server" ControlToValidate="txtCodeCRSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Click on Add button" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" Text="Code"></asp:Label>
                        <asp:TextBox ID="txtCodeCRSgl" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Name"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REFVNameCRGL" runat="server" ControlToValidate="txtNameCRSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="txtNameCRSgl1" runat="server" ControlToValidate="txtNameCRSgl" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtNameCRSgl" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REFVDescCRSGL" runat="server" ControlToValidate="txtDescCRSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDescCRSgl" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Label runat="server" Text="" ID="lblModalCRSGLID" Visible="false"></asp:Label>
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnNewCRSgl"></asp:Button>
                       <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnUpdateCRSgl"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSaveCRSgl" ></asp:Button>
                        <asp:Button runat="server" Text="Activate" class="btn-ok"  ID="btnActivateCRSgl" ></asp:Button>
                        <asp:Button runat="server" Text="DeActivate" class="btn-ok"  ID="btnDeactivateCRSgl" ></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtfile" runat="server" CssClass="btn-ok" Width="95%" AllowMultiple="true" />
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                    <asp:Button ID="btnScan" runat="server" Text="Scan" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                        Visible="false" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                        runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12 form-group">
        <div id="divcollapseAttachments" runat="server" visible="false" data-toggle="collapse" data-target="#collapseAttachments"><a href="#"><b><i>Click here to view Attachments...</i></b></a></div>
    </div>
    <div id="collapseAttachments" class="col-sm-12 col-md-12 collapse form-group">
        <div class="col-sm-6 col-md-6" style="max-height: 138px; padding-left: 0px; padding-right: 0px;">
            <asp:DataGrid ID="dgAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="footable" OnRowDataBound="PickColor_RowDataBound">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                    </asp:BoundColumn>

                    <asp:TemplateColumn HeaderText="File Name">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="28%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                            <asp:Label ID="lblExt" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Ext") %>'></asp:Label>
                            <asp:Label ID="lblFile" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:Label>
                            <asp:LinkButton ID="File" CommandName="OPENPAGE" Font-Italic="true" runat="server" Visible="false" Font-Bold="False" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Created">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <b>By:-</b>
                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <b>On:-</b>
                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnView" data-toggle="tooltip" data-placement="bottom" title="View" runat="server" CommandName="VIEW" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Add Description" CommandName="ADDDESC" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDownload" data-toggle="tooltip" data-placement="bottom" title="DownLoad" CommandName="OPENPAGE" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div class="col-sm-6 col-md-6">
            <asp:Image ID="imgView" runat="server" Width="500px" Height="400px" />
        </div>
    </div>

    <div id="ModalBillAdjusment" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divBillAdjusment" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblBillAdjusment" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button ID="btnYes" Text="Yes" CssClass="btn-ok" runat="server" />
                    <asp:Button ID="btnNo" Text="No" CssClass="btn-ok" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <asp:TextBox ID="txtStartDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtEndDate" runat="server" Visible="false"></asp:TextBox>

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
                                <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblSubcabinet" runat="server" Text="Sub cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblFolder" runat="server" Text="Folder"></asp:Label>
                                <asp:DropDownList ID="ddlFolder" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblDocumentType" runat="server" Text="Document Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
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

</asp:Content>

