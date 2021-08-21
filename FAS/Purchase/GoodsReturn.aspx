<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Purchase.master" CodeFile="GoodsReturn.aspx.vb" Inherits="Purchase_GoodsReturn" EnableEventValidation="false" ValidateRequest="false" %>

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

        .auto-style1 {
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
            margin-top: 1px;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }

        .auto-style3 {
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


        function CacheItems() {

            ddlText = new Array();

            ddlValue = new Array();

            ddl = document.getElementById("<%=chkCategory.ClientID %>");

            for (var i = 0; i < ddl.options.length; i++) {

                ddlText[ddlText.length] = ddl.options[i].text;

                ddlValue[ddlValue.length] = ddl.options[i].value;

            }

        }

        window.onload = CacheItems;

        function FilterItems(value) {

            ddl.options.length = 0;

            for (var i = 0; i < ddlText.length; i++) {

                if (ddlText[i].toLowerCase().indexOf(value.toLowerCase()) != -1) {

                    AddItem(ddlText[i], ddlValue[i]);
                }

            }

            if (ddl.options.length == 0) {

                AddItem("No items found.", "");
            }

        }

        function AddItem(text, value) {

            var opt = document.createElement("option");

            opt.text = text;

            opt.value = value;

            ddl.options.add(opt);

        }

        function SearchList() {
            var l = document.getElementById('<%= chkCategory.ClientID %>');
                 var tb = document.getElementById('<%= txtsearch.ClientID %>');
                 if (tb.value == "") {
                     ClearSelection(l);
                 }
                 else {
                     for (var i = 0; i < l.options.length; i++) {
                         if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                             l.options[i].selected = true;
                             return false;
                         }
                         else {
                             ClearSelection(l);
                         }
                     }
                 }
             }

             function ClearSelection(lb) {
                 lb.selectedIndex = -1;
             }
             $(document).ready(function () {

                 $('[data-toggle="tooltip"]').tooltip();
                 $('#<%=dgPurchaseReturn.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlExistingOrder.ClientID%>').select2();
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlOrderNo.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCommodity.ClientID%>').select2();
               });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlSupplier.ClientID%>').select2();
                      });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })


        function ValidateRate() {

            if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                alert("Enter Rate Field")
                document.getElementById('<%=txtRate.ClientID %>').focus()
                return false;
            }
            else {
                var numR
                numR = CheckDecimal(document.getElementById('<%=txtRate.ClientID %>').value)
                if (numR == false) {
                    alert("Enter only integers and Decimals for Rate.")
                    document.getElementById('<%=txtRate.ClientID %>').value = ""
                    document.getElementById('<%=txtRate.ClientID %>').focus()
                    return false
                }

                var re = /^\s*$/;
                if (re.test(numR)) {
                    alert("Enter Rate spaces are not allowed.");
                    document.getElementById('<%=txtRate.ClientID %>').value = ""
                    document.getElementById('<%=txtRate.ClientID%>').focus();
                    return false;
                }
            }
        }
        function CheckDecimal(param) {
            var decmal = /^\d+(\.\d{1,5})+$/;
            if (decmal.test(param))
                return true;
            var decmal = /^\d+$/;
            if (decmal.test(param))
                return true;
            else
                return false;
        }





        var ddlText, ddlValue, ddl, lblMesg;

        function CacheItems() {

            ddlText = new Array();

            ddlValue = new Array();

            ddl = document.getElementById("<%=chkCategory.ClientID %>");

            for (var i = 0; i < ddl.options.length; i++) {

                ddlText[ddlText.length] = ddl.options[i].text;

                ddlValue[ddlValue.length] = ddl.options[i].value;

            }

        }
        window.onload = CacheItems;

        function FilterItems(value) {

            ddl.options.length = 0;

            for (var i = 0; i < ddlText.length; i++) {

                if (ddlText[i].toLowerCase().indexOf(value.toLowerCase()) != -1) {

                    AddItem(ddlText[i], ddlValue[i]);
                }

            }

            if (ddl.options.length == 0) {

                AddItem("No items found.", "");
            }

        }

        function CalculateQuantity() {
            if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
                var num
                num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
                if (num == false) {
                    alert("Enter integers/Decimals for Placed Quantity.")
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                document.getElementById('<%=txtQuantity.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%=txtRateAmount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = ""
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = ""

                var ssplacedqty = parseFloat(document.getElementById('<%=txtQuantity.ClientID %>').value).toFixed(2)
                var ssMRP = parseFloat(document.getElementById('<%=txtRate.ClientID %>').value).toFixed(2)
                var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                document.getElementById('<%=txtRateAmount.ClientID %>').value = ssTotal
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = ssTotal

                var ssGSTRate = parseFloat(document.getElementById('<%=txtGSTRate.ClientID %>').value).toFixed(2)
                if (document.getElementById('<%=txtGSTRate.ClientID %>').value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value).toFixed(2)
                    var sAmount = parseFloat(parseFloat(ssTotalAmt)).toFixed(2)

                    var sGSTAmount = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)
                    document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)

                var sNetAmt = parseFloat(parseFloat(sAmount) + parseFloat(sGSTAmount)).toFixed(2)
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = sNetAmt

            }

            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value).toFixed(2)
                    var sDISCOUNT = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value).toFixed(2)

                    var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                    document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount

                var sAmount = parseFloat((parseFloat(ssTotalAmt) - parseFloat(ssDiscount))).toFixed(2)

                var sGSTAmount = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)

                var sNetAmt = parseFloat(parseFloat(sAmount) + parseFloat(sGSTAmount)).toFixed(2)
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = sNetAmt

            }


        }
        else {
            document.getElementById('<%=txtRateAmount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = ""
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = ""
            }

        }

        function validatePage() {
            //Executes all the validation controls associated with group1 validaiton Group1. 
            var flag = window.Page_ClientValidate('Validate');
            if (flag)
                return flag;

        }

        function Reset() {
            var i;
            for (i = 0; i < document.myform.chkCategory.length; i++) {
                document.myform.chkCategory.options[i].selected = false;
            }

        }

        function DeselectListBox() {
            var ListBoxObject = document.getElementById("chkCategory")

            for (var i = 0; i < ListBoxObject.length; i++) {
                if (ListBoxObject.options[i].selected) {
                    ListBoxObject.options[i].selected = false
                }
            }
        }



        function CalculateQuantityCheck(sAvailableQty, sReturnqty) {
            if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                var ssReturnqty = document.getElementById(sReturnqty).value
                var ssAvailableQty = parseFloat(document.getElementById(sAvailableQty).innerHTML)
                if ((ssAvailableQty < ssReturnqty)) {
                    alert("You can not enter qty greater than the Purchased qty")
                    document.getElementById('<%=txtQuantity.ClientID %>').value == ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus();

                    document.getElementById('<%=txtRateAmount.ClientID %>').value = ""
                    document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                    document.getElementById('<%=txtGSTAmount.ClientID %>').value = ""
                    document.getElementById('<%=txtTotalAmount.ClientID %>').value = ""
                    return false;
                }

                var num
                num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
                if (num == false) {
                    alert("Enter integers/Decimals for Placed Quantity.")
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                document.getElementById('<%=txtQuantity.ClientID %>').focus();
                    return false;
                }

                document.getElementById('<%=txtRateAmount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = ""
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = ""

                var ssplacedqty = parseFloat(document.getElementById('<%=txtQuantity.ClientID %>').value).toFixed(2)
                var ssMRP = parseFloat(document.getElementById('<%=txtRate.ClientID %>').value).toFixed(2)
                var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                document.getElementById('<%=txtRateAmount.ClientID %>').value = ssTotal
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = ssTotal

                var ssGSTRate = parseFloat(document.getElementById('<%=txtGSTRate.ClientID %>').value).toFixed(2)
                if (document.getElementById('<%=txtGSTRate.ClientID %>').value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value).toFixed(2)
                    var sAmount = parseFloat(parseFloat(ssTotalAmt)).toFixed(2)

                    var sGSTAmount = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)
                    document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)

                var sNetAmt = parseFloat(parseFloat(sAmount) + parseFloat(sGSTAmount)).toFixed(2)
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = sNetAmt

            }

            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value).toFixed(2)
                    var sDISCOUNT = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value).toFixed(2)

                    var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                    document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount

                var sAmount = parseFloat((parseFloat(ssTotalAmt) - parseFloat(ssDiscount))).toFixed(2)

                var sGSTAmount = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)

                var sNetAmt = parseFloat(parseFloat(sAmount) + parseFloat(sGSTAmount)).toFixed(2)
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = sNetAmt

            }


        }
        else {
            document.getElementById('<%=txtRateAmount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = ""
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = ""
            }

        }


        <%--$('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgPurchaseReturn.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });--%>
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Goods Return</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" ValidationGroup="false" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnPrint" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" Visible="false" />

                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Goods Return No."></asp:Label>
            <asp:DropDownList runat="server" autocomplete="off" CssClass="aspxcontrols" ID="ddlExistingOrder" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Purchase Order No."></asp:Label>
            <asp:DropDownList runat="server" autocomplete="off" CssClass="aspxcontrols" ID="ddlOrderNo" AutoPostBack="True"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="ReFVONo" runat="server" ControlToValidate="ddlOrderNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order Date"></asp:Label>
            <asp:TextBox CssClass="aspxcontrolsdisable" runat="server" ID="txtOrderDate" ValidateRequestMode="Disabled"></asp:TextBox>
            <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtOrderDate" PopupPosition="BottomLeft"
                TargetControlID="txtOrderDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="ReFVOdate" runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlSupplier" Enabled="False"></asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplier" runat="server" ControlToValidate="ddlSupplier" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier Code"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtSupplierCode"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice No"></asp:Label>

            <%-- <asp:DropDownList runat="server" autocomplete="off" CssClass="aspxcontrols" ID="DropDownList1" AutoPostBack="True"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlOrderNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>

            <asp:DropDownList runat="server" autocomplete="off" AutoPostBack="true" CssClass="aspxcontrolsdisable" ID="ddlInvoiceNo"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVINo" runat="server" ControlToValidate="ddlInvoiceNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>

        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtInvoiceDate"></asp:TextBox>
            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNofDays" runat="server" ControlToValidate="ddlNumberOfDays" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Number Of days" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Goods Return No"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable" ID="purchaseReturnNo"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMshipping" runat="server" ControlToValidate="purchaseReturnNo" Display="Dynamic" ErrorMessage="Select Mode of Shipping" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Return Date"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtReturnDate"></asp:TextBox>
            <cc1:CalendarExtender ID="clReturnDate" runat="server" PopupButtonID="txtReturnDate" PopupPosition="BottomLeft"
                TargetControlID="txtReturnDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRDate" runat="server" ControlToValidate="txtReturnDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Method Of Payment" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Return Reference No"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtReturnRefNo"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRRefNo" runat="server" ControlToValidate="txtReturnRefNo" Display="Dynamic" ErrorMessage="Select Payment Terms" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Brand"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCommodity">
            </asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCstgry" runat="server" ControlToValidate="ddlCstCtgry" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
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

    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <legend class="legendbold">Details of Goods Return</legend>
        </fieldset>
        <div class="col-sm-6 col-md-6" style="padding-left: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                    <asp:TextBox autocomplete="off" ID="txtsearch" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="FilterItems(this.value)"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <asp:ListBox ID="chkCategory" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="315px"></asp:ListBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCat" runat="server" ControlToValidate="chkCategory" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable" ID="ddlUnit"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6">
                    <br />
                    <asp:Label runat="server" Text="Purchased Qty"></asp:Label>
                    <asp:Label ID="lblPurchasedQty" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtQuantity" ValidationGroup="ValidateQty"></asp:TextBox>
                    <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Rate Amount"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtRate"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RefRate" runat="server" ControlToValidate="txtRate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Quantity Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtRateAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfRateAmount" runat="server" />
                </div>
            </div>

            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Discount"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtDiscount" ValidationGroup="ValidateDiscount"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="ReDiscount" runat="server" ControlToValidate="txtDiscount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDiscount"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Discount Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtDiscountAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <%--<asp:Label runat="server" Text="Freight"></asp:Label>
                    <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtFreight"></asp:TextBox>--%>
                    <asp:HiddenField ID="hfPurchaseStatus" runat="server" />
                    <asp:HiddenField ID="hfsStateCode" runat="server" />
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Charges" Visible="false"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtCharges" Visible="false"></asp:TextBox>
                    <asp:HiddenField ID="hfFreightAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="GST Rate %"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtGSTRate"></asp:TextBox>
                    <asp:HiddenField ID="hfGSTID" runat="server" />
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="GST Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtGSTAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfGSTAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Total Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtTotalAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfTotalAmount" runat="server" />
                    <asp:HiddenField ID="hfAmount" runat="server" />
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="* Reason To Return"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlreturntype"></asp:DropDownList>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReason" runat="server" ControlToValidate="ddlreturntype" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Remarks"></asp:Label>
                <asp:TextBox Height="50" autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtNarration" ReadOnly="false" TextMode="MultiLine"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRemarks" runat="server" ControlToValidate="txtNarration" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <asp:GridView ID="dgPurchaseReturn" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
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
                            <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" Visible="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="col-sm-12 col-md-12">
            <asp:TextBox ID="txtBillAmount" Visible="false" CssClass="aspxcontrols" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-12 col-md-12">
            <asp:DataGrid ID="dgJEDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable" Visible="false">
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
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="18%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
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
                </Columns>
            </asp:DataGrid>
        </div>
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

    <asp:TextBox ID="txtHistoryID" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
