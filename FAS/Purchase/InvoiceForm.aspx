<%@ Page Title="" Language="VB" MasterPageFile="~/Purchase.master" AutoEventWireup="false" CodeFile="InvoiceForm.aspx.vb" Inherits="Purchase_InvoiceForm" %>

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

        .auto-style2 {
            width: 100%;
            height: 26px;
            resize: none;
            font-size: 12px;
            line-height: 1.42857143;
            color: #444444;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            font-family: "Segoe UI", sans-serif;
            border: 1px solid #ccc;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }

        div {
            color: black;
        }
    </style>
    <%-- <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgViewPI.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });


            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
                $('#<%=ddlPurchaseOrder.ClientID%>').select2();
            });

        })


        function Validate() {
            if (document.getElementById('<%=ddlCompanyType.ClientID %>').selectedIndex == 0) {
                alert('Select Company Type.')
                document.getElementById('<%=ddlCompanyType.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlGSTCategory.ClientID %>').selectedIndex == 0) {
                alert('Select GSTN Category.')
                document.getElementById('<%=ddlGSTCategory.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlPurchaseOrder.ClientID %>').selectedIndex == 0) {
                alert('Select Purchase Order.')
                document.getElementById('<%=ddlPurchaseOrder.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlPurchaseRegister.ClientID %>').selectedIndex == 0) {
                alert('Select Reference No.')
                document.getElementById('<%=ddlPurchaseRegister.ClientID%>').focus()
                return false
            }
            var GSTIN = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}Z[(a-z)(A-Z)(0-9)]{1}?$/
            <%--if (document.getElementById('<%=txtBillingAddress.ClientID %>').value == "") {
                alert('Enter Billing Address.');
                document.getElementById('<%=txtBillingAddress.ClientID%>').focus()
                return false;
            }--%>
            <%--if (document.getElementById('<%=txtBillingGSTNRegNo.ClientID %>').value == "") {
                alert('Enter Billing GSTN Reg.No.');
                document.getElementById('<%=txtBillingGSTNRegNo.ClientID%>').focus()
                return false;
            }  --%>
            if (document.getElementById('<%=txtDeliveryFromAddress.ClientID %>').value == "") {
                alert('Enter Delivery From Address.');
                document.getElementById('<%=txtDeliveryFromAddress.ClientID%>').focus()
                return false;
            }
            <%--if (document.getElementById('<%=txtDeliveryFromGSTNRegNo.ClientID %>').value == "") {
                alert('Enter Delivery From GSTN Reg.No.');
                document.getElementById('<%=txtDeliveryFromGSTNRegNo.ClientID%>').focus()
                return false;
            }--%>
            <%--if (document.getElementById('<%=txtDeliveryFromGSTNRegNo.ClientID %>').value != "") {               
                var num
                num = GSTIN.test(document.getElementById('<%=txtDeliveryFromGSTNRegNo.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Delivery From GSTN Reg.No(Only 15 Charecters, Two integers,Five Capital alphabets, Four integers, One Capital alphabet, One integer, then Capital Z, One integer).")
                    document.getElementById('<%=txtDeliveryFromGSTNRegNo.ClientID %>').value = ""
                    document.getElementById('<%=txtDeliveryFromGSTNRegNo.ClientID %>').focus()
                    return false
                }               

            }--%>
            if (document.getElementById('<%=txtReceiveAddress.ClientID %>').value == "") {
                alert('Enter Receive Address.');
                document.getElementById('<%=txtReceiveAddress.ClientID%>').focus()
                return false;
            }
            <%--if (document.getElementById('<%=txtReceiveGSTNRegNo.ClientID %>').value == "") {
                alert('Enter Receive GSTN Reg.No.');
                document.getElementById('<%=txtReceiveGSTNRegNo.ClientID%>').focus()
                return false;
            }--%>
            <%--if (document.getElementById('<%=txtReceiveGSTNRegNo.ClientID %>').value != "") {               
                var num
                num = GSTIN.test(document.getElementById('<%=txtReceiveGSTNRegNo.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Receive GSTN Reg.No(Only 15 Charecters, Two integers,Five Capital alphabets, Four integers, One Capital alphabet, One integer, then Capital Z, One integer).")
                    document.getElementById('<%=txtReceiveGSTNRegNo.ClientID %>').value = ""
                    document.getElementById('<%=txtReceiveGSTNRegNo.ClientID %>').focus()
                    return false
                }               

            }--%>
            var decimalOnly = /^\s*-?[0-9]\d*(\.\d{1,2})?\s*$/;
            if (document.getElementById('<%=txtManualBillAmt.ClientID %>').value == "") {
                alert('Enter Mannual Bill Amt');
                document.getElementById('<%=txtManualBillAmt.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=txtManualBillAmt.ClientID %>').value != "") {
                var num
                num = decimalOnly.test(document.getElementById('<%=txtManualBillAmt.ClientID %>').value)
                if (num == false) {
                    alert("Enter integers/Decimals for Mannual Bill Amt.")
                    document.getElementById('<%=txtManualBillAmt.ClientID %>').value = ""
                    return false;
                }
            }
            if (document.getElementById('<%=txtManualGST.ClientID %>').value == "") {
                alert('Enter Mannual GST.');
                document.getElementById('<%=txtManualGST.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=txtManualGST.ClientID %>').value != "") {
                var num
                num = decimalOnly.test(document.getElementById('<%=txtManualGST.ClientID %>').value)
                if (num == false) {
                    alert("Enter integers/Decimals for Mannual GST.")
                    document.getElementById('<%=txtManualGST.ClientID %>').value = ""
                    return false;
                }
            }

        }

        function CalculateGST(sPlacedqty, sMRP, sTotal, stxtDiscount, sDiscountAmount, sCharges, sAmount, sGSTRate, sGSTAmount, sNetAmount, sHFDiscountAmount, sHFCharges, sHFAmount, sHFGSTAmount, sHFNetAmount, dChargeAmount, dItemsTotalFromDispatch) {
            <%--if (document.getElementById('<%=txtDispatchDate.ClientID %>').value == "") {
                alert('Enter Invoice Date.');
                var ddlDiscountt = document.getElementById(sddlDiscount);
                ddlDiscountt.selectedIndex = 0
                document.getElementById(sDiscountAmount).innerText = ""
                document.getElementById('<%=txtDispatchDate.ClientID%>').focus();
        return false;
    }--%>
    <%--if (document.getElementById('<%=ddlChargeType.ClientID %>').selectedIndex == 0) {
                alert('Enter Charge Amount.')
                var ddlDiscountt = document.getElementById(sddlDiscount);

                ddlDiscountt.selectedIndex = 0

                document.getElementById(sDiscountAmount).innerText = ""
                document.getElementById('<%=ddlChargeType.ClientID%>').focus()
            return false;
        }--%>

            //if (document.getElementById(sPlacedqty).innerHTML == "") {
            //    alert("Enter Quantity.")
            //    document.getElementById(sPlacedqty).focus();
            //    return false;
            //}
            document.getElementById(sGSTAmount).innerText = ""
            document.getElementById(sDiscountAmount).innerText = ""

            document.getElementById(sHFGSTAmount).value = ""
            document.getElementById(sHFDiscountAmount).value = ""

            //var num
            //num = CheckDecimal(document.getElementById(sPlacedqty).innerHTML)
            //if (num == false) {
            //    alert("Enter integers/Decimals for Placed Quantity.")
            //    document.getElementById(sPlacedqty).value = ""
            //    document.getElementById(sClosingStock).innerHTML = ""
            //    document.getElementById(sPlacedqty).focus();
            //    return false;
            //}
            var ssplacedqty = document.getElementById(sPlacedqty).innerHTML
            var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
            var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
            document.getElementById(sTotal).innerText = ssTotal

            //var ddlDiscountt = document.getElementById(sddlDiscount);
            var ssGSTRate = parseFloat(document.getElementById(sGSTRate).innerText)

            var sstxtDiscount = document.getElementById(stxtDiscount).value
            if (sstxtDiscount > 0) {

                var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)

                document.getElementById(sCharges).innerText = parseFloat(((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(dChargeAmount)) / parseFloat(dItemsTotalFromDispatch)).toFixed(2)
                document.getElementById(sHFCharges).value = parseFloat(((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(dChargeAmount)) / parseFloat(dItemsTotalFromDispatch)).toFixed(2)

                //var ddlDiscountt = document.getElementById(sddlDiscount);
                //var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sstxtDiscount)) / 100).toFixed(2)
                document.getElementById(sDiscountAmount).innerText = ssDiscount
                document.getElementById(sHFDiscountAmount).value = ssDiscount
                var sDiscountAMT = ssDiscount

                document.getElementById(sAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(ssDiscount)) + parseFloat(document.getElementById(sCharges).innerText)).toFixed(2)
                document.getElementById(sHFAmount).value = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(ssDiscount)) + parseFloat(document.getElementById(sCharges).innerText)).toFixed(2)

                document.getElementById(sGSTAmount).innerText = parseFloat(((parseFloat(document.getElementById(sAmount).innerText)) * parseFloat(ssGSTRate)) / 100).toFixed(2)
                document.getElementById(sHFGSTAmount).value = parseFloat(((parseFloat(document.getElementById(sAmount).innerText)) * parseFloat(ssGSTRate)) / 100).toFixed(2)

                var sNetAmt = parseFloat(parseFloat(document.getElementById(sAmount).innerText) + parseFloat(document.getElementById(sGSTAmount).innerText)).toFixed(2)
                document.getElementById(sNetAmount).innerText = sNetAmt
                document.getElementById(sHFNetAmount).value = sNetAmt

            }
            else {
                var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)

                document.getElementById(sCharges).innerText = parseFloat(((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(dChargeAmount)) / parseFloat(dItemsTotalFromDispatch)).toFixed(2)
                document.getElementById(sHFCharges).value = parseFloat(((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(dChargeAmount)) / parseFloat(dItemsTotalFromDispatch)).toFixed(2)

                document.getElementById(sAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText)) + parseFloat(document.getElementById(sCharges).innerText)).toFixed(2)
                document.getElementById(sHFAmount).value = parseFloat((parseFloat(document.getElementById(sTotal).innerText)) + parseFloat(document.getElementById(sCharges).innerText)).toFixed(2)

                document.getElementById(sGSTAmount).innerText = parseFloat(((parseFloat(document.getElementById(sAmount).innerText)) * parseFloat(ssGSTRate)) / 100).toFixed(2)
                document.getElementById(sHFGSTAmount).value = parseFloat(((parseFloat(document.getElementById(sAmount).innerText)) * parseFloat(ssGSTRate)) / 100).toFixed(2)

                var sNetAmt = parseFloat(parseFloat(document.getElementById(sAmount).innerText) + parseFloat(document.getElementById(sGSTAmount).innerText)).toFixed(2)
                document.getElementById(sNetAmount).innerText = sNetAmt
                document.getElementById(sHFNetAmount).value = sNetAmt
            }

        }

    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Invoice Form</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Existing Invoice No."></asp:Label>
            <asp:DropDownList ID="ddlExistingInvoiceNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Company Type."></asp:Label>
            <asp:DropDownList ID="ddlCompanyType" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="GSTN Category"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style2" ID="ddlGSTCategory"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Purchase Order No."></asp:Label>
            <asp:DropDownList ID="ddlPurchaseOrder" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Order Date"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtOrderDate"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Invoice No"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtInvoiceNo"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label runat="server" ID="lblStatus" Text="Status :-" Font-Bold="true"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Reference No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style2" ID="ddlPurchaseRegister"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Invoice Date"></asp:Label>
            <asp:TextBox runat="server" ID="txtInvoiceDate" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtInvoiceDate" PopupPosition="BottomLeft"
                TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <%--<asp:RangeValidator ID="rgvtxtInvoiceDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"></asp:RangeValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style2" ID="ddlSupplier"></asp:DropDownList>
        </div>
        <%-- <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Payment Mode"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlPaymentMode" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Method of Shipping"></asp:Label>
            <asp:TextBox runat="server" ID="txtMShiping" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>--%>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier Billing Address"></asp:Label>
            <asp:TextBox ID="txtBillingAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingAddress" runat="server" ControlToValidate="txtBillingAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Delivered From"></asp:Label>
            <asp:TextBox ID="txtDeliveryFromAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryFromAddress" runat="server" ControlToValidate="txtDeliveryFromAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery From Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Bill To"></asp:Label>
            <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Company Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>            --%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipping To"></asp:Label>
            <asp:TextBox ID="txtReceiveAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReceiveAddress" runat="server" ControlToValidate="txtReceiveAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtBillingGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
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
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyGSTNRegNo" runat="server" ControlToValidate="txtCompanyGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Company GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
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
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* ChargeType" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlChargeType" runat="server" CssClass="aspxcontrols" Visible="false"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlChargeType" runat="server" ControlToValidate="ddlChargeType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Charge Type" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Amount" Visible="false"></asp:Label>
            <asp:TextBox ID="txtShippingRate" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" ErrorMessage="Enter Valid Shipping Rate" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="imgbtnAddCharge" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" ValidationGroup="ValidateAdd" />
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Mannual Bill Amount" Visible="false"></asp:Label>
            <asp:TextBox ID="txtManualBillAmt" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVManualBillAmt" runat="server" ControlToValidate="txtManualBillAmt" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Manual Bill Amt" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVManualBillAmt" runat="server" ControlToValidate="txtManualBillAmt" Display="Dynamic" ErrorMessage="Enter Valid Manual Bill Amt" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Mannual GST" Visible="false"></asp:Label>
            <asp:TextBox ID="txtManualGST" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVManualGST" runat="server" ControlToValidate="txtManualGST" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Manual GST" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVManualGST" runat="server" ControlToValidate="txtManualGST" Display="Dynamic" ErrorMessage="Enter Valid Manual GST" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:TextBox ID="txtBreakup" runat="server" CssClass="aspxcontrols " Visible="false" />
            <asp:Button ID="btnBreakUp" runat="server" Text="BreakUp" CssClass="aspxcontrols " Visible="false" />
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

                <asp:TemplateColumn Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnCancel" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" ImageUrl="~/Images/Trash16.png" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>

    <div class="col-sm-12 col-md-12" style="padding-right: 0px; overflow: auto">
        <asp:GridView ID="dgViewPI" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
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
                        <%--<asp:DropDownList ID="ddlDiscount" CssClass="aspxcontrols" AutoPostBack="true" runat="server"></asp:DropDownList>--%>
                        <asp:TextBox ID="txtDiscount" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount") %>'></asp:Label>
                        <asp:HiddenField ID="HFDiscountAmount" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Charges" HeaderStyle-Width="10%" Visible="true">
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
                <asp:TemplateField HeaderText="GST Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>
                        <asp:HiddenField ID="HFGSTAmount" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="SGST" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblSGST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SGST") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST Amt" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblSGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SGSTAmount") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="CGST" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblCGST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CGST") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGST Amt" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblCGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTAmount") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="IGST" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblIGST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IGST") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST Amt" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblIGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IGSTAmount") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Total" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblFinalTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FinalTotal") %>'></asp:Label>
                        <asp:HiddenField ID="HFFinalTotal" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div id="ModalUserMasterDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblUserMasterDetailsValidationMsg" runat="server"></asp:Label></strong>
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
    <div class="col-sm-12 col-md-12">
        <asp:TextBox ID="txtBillAmount" Visible="false" CssClass="aspxcontrols" runat="server"></asp:TextBox>
    </div>
    <div class="col-sm-12 col-md-12">
        <asp:TextBox ID="txtMasterID" Visible="false" CssClass="aspxcontrols" runat="server"></asp:TextBox>
    </div>

    <asp:TextBox ID="txtStartDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtEndDate" runat="server" Visible="false"></asp:TextBox>

    <asp:TextBox ID="txtGLID" runat="server" Visible="false"></asp:TextBox>
</asp:Content>

