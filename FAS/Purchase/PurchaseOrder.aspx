<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Purchase.master" CodeFile="PurchaseOrder.aspx.vb" Inherits="Purchase_PurchaseOrder" EnableEventValidation="false" ValidateRequest="false" %>

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
            margin-top: 1px;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }

        div {
            color: black;
        }
    </style>
    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <%-- <ajax:toolkitscript xmlns:ajax="#unknown">--%>
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
            $('#<%=dgPurchase.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlExistingOrder.ClientID%>').select2();
                 $('#<%=ddlCurrency.ClientID%>').select2();
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


       <%--function CalculateQuantity() {
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

            var ssplacedqty = document.getElementById('<%=txtQuantity.ClientID %>').value
            var ssMRP = parseFloat(document.getElementById('<%=txtRate.ClientID %>').value)
            var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
            document.getElementById('<%=txtRateAmount.ClientID %>').value = ssTotal
            document.getElementById('<%=txtTotalAmount.ClientID %>').value = ssTotal

            var ssGSTRate = parseFloat(document.getElementById('<%=txtGSTRate.ClientID %>').value)
            if (document.getElementById('<%=txtGSTRate.ClientID %>').value != "") {
                
            var ssTotalAmt = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)                       
            var sAmount = parseFloat(parseFloat(ssTotalAmt) + parseFloat(document.getElementById('<%=txtFreight.ClientID %>').value)).toFixed(2)
                
            var sGSTAmount = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)
            document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)
            
            var sNetAmt = parseFloat(parseFloat(sAmount) + parseFloat(sGSTAmount)).toFixed(2)
            document.getElementById('<%=txtTotalAmount.ClientID %>').value = sNetAmt

            }

            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
            
            var ssTotalAmt = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)                        
            var sDISCOUNT = document.getElementById('<%=txtDiscount.ClientID %>').value
            
            var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) + parseFloat(document.getElementById('<%=txtFreight.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
            document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount           

            var sAmount = parseFloat((parseFloat(ssTotalAmt) - parseFloat(ssDiscount)) + parseFloat(document.getElementById('<%=txtFreight.ClientID %>').value)).toFixed(2)
            
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

    }--%>


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

    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Purchase Order</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <%--<asp:ImageButton ID="imgbtnAddNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" ValidationGroup="Validate" />--%>
                    <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <%--<asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />--%>
                    <%-- <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />--%>

                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnPrint" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" style="left: 0px; top: 0px">
        <div class="col-sm-6 col-md-6">
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
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:DropDownList ID="ddlExistingOrder" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Currency"></asp:Label>
            <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div class="col-sm-6 col-md-6 pull-left">
            <asp:Label ID="lblStatus" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
        <%--<div class="col-sm-6 col-md-6">
            <asp:DropDownList ID="ddlExistingOrder" runat="server" CssClass="auto-style1" Width="240px" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div class="col-sm-6 col-md-6 pull-left">
            <asp:Label ID="lblStatus" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>--%>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Purchase Order No."></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtOrderCode"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order Date"></asp:Label>
            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtOrderDate" AutoPostBack="true" ValidateRequestMode="Disabled" placeholder="DD/MM/YY"></asp:TextBox>
            <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtOrderDate" PopupPosition="BottomLeft"
                TargetControlID="txtOrderDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>

            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="ReFVOdate" runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>

            <%--<asp:RangeValidator ID="rgvtxtOrderDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtOrderDate" SetFocusOnError="True" ></asp:RangeValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlSupplier"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplier" runat="server" ControlToValidate="ddlSupplier" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier Code"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtSprCode"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Delivery Schedule(No. of Weeks)"></asp:Label>
            <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlDSchedule"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDschdule" runat="server" ControlToValidate="ddlDSchedule" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Delivery Schdule" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Delivery Schedule(No. of Days)"></asp:Label>
            <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlNumberOfDays"></asp:DropDownList>
            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNofDays" runat="server" ControlToValidate="ddlNumberOfDays" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Number Of days" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Method of Shipping"></asp:Label>
            <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlModeOfShipping"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMshipping" runat="server" ControlToValidate="ddlModeOfShipping" Display="Dynamic" ErrorMessage="Select Mode of Shipping" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Mode of Payment"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlMPayment"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMpayment" runat="server" ControlToValidate="ddlMPayment" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Method Of Payment" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Payment Terms"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlPterms"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPterms" runat="server" ControlToValidate="ddlPterms" Display="Dynamic" ErrorMessage="Select Payment Terms" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Brand"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style2" ID="ddlCommodity"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCmdty" runat="server" ControlToValidate="ddlCommodity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Brand" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="*Company Type"></asp:Label>
            <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="*GSTN Category"></asp:Label>
            <asp:DropDownList ID="ddlGSTCategory" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:CheckBox ID="chkboxSameAdd" runat="server" AutoPostBack="true" Visible="false" />
            <asp:Label runat="server" Text="Same as Billing Address" Visible="false"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:CheckBox ID="chkboxFrom" runat="server" AutoPostBack="true" Text="Same as Supplier Billing Address" />
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" Enabled="false" Visible="false" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:CheckBox ID="chkboxTo" runat="server" AutoPostBack="true" Text="Same as Bill To Address" />
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier Billing Address"></asp:Label>
            <asp:TextBox ID="txtBillingAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
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
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Company Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>            --%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipping To"></asp:Label>
            <asp:TextBox ID="txtDeleveryAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeleveryAddress" runat="server" ControlToValidate="txtDeleveryAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>
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
            <asp:TextBox ID="txtDeliveryFromGSTNRegNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
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
            <asp:TextBox ID="txtDeliveryGSTNRegNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryGSTNRegNo" runat="server" ControlToValidate="txtDeliveryGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Shipping To GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliveryGSTNRegNo" runat="server" ControlToValidate="txtDeliveryGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid Shipping To GSTN Reg.No" SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
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
            <legend class="legendbold">Details of Purchase Order</legend>
        </fieldset>
        <div class="col-sm-6 col-md-6" style="padding-left: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                    <asp:TextBox autocomplete="off" ID="txtsearch" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="FilterItems(this.value)"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <asp:ListBox ID="chkCategory" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="290px" ValidationGroup="Validate" Style="left: 0px; top: 0px"></asp:ListBox>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="Delivery Required Date"></asp:Label>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRDate"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtRDate" PopupPosition="BottomLeft"
                    TargetControlID="txtRDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RevRdate" runat="server" ControlToValidate="txtRDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlUnit"></asp:DropDownList>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Rate"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlRate"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Rate Amount"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" AutoPostBack="true" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRate"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RefRate" runat="server" ControlToValidate="txtRate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtQuantity" ValidationGroup="ValidateQty"></asp:TextBox>
                    <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Quantity Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtRateAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfRateAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Discount"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtDiscount" ValidationGroup="ValidateDiscount"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="ReDiscount" runat="server" ControlToValidate="txtDiscount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDiscount"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Discount Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtDiscountAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Freight Amount" Visible="false"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtFreightAmount" Visible="false"></asp:TextBox>
                    <asp:HiddenField ID="hfFreightAmount" runat="server" />
                </div>
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Freight" Visible="false"></asp:Label>
                    <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtFreight" Visible="false"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="GST Rate %"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtGSTRate"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="GST Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtGSTAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfGSTAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="Total Amount"></asp:Label>
                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtTotalAmount"></asp:TextBox>
                <asp:HiddenField ID="hfTotalAmount" runat="server" />
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" Visible="false" ID="txtGSTID"></asp:TextBox>
        </div>

        <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
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

                    <%--<asp:BoundField DataField="ID" HeaderText="ID" Visible="false"></asp:BoundField>--%>
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
                    <asp:BoundField DataField="SGST" HeaderText="SGST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                    <asp:BoundField DataField="SGSTAmount" HeaderText="SGSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                    <asp:BoundField DataField="CGST" HeaderText="CGST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                    <asp:BoundField DataField="CGSTAmount" HeaderText="CGSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                    <asp:BoundField DataField="IGST" HeaderText="IGST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                    <asp:BoundField DataField="IGSTAmount" HeaderText="IGSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>

                    <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                    <asp:BoundField DataField="CCurrency" HeaderText="Currency" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                    <asp:BoundField DataField="FETotalAmount" HeaderText="Other Currency TotalAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
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
    </div>

    <asp:Label ID="lblDescID" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblScode" runat="server" Visible="False"></asp:Label>
    <asp:TextBox ID="txtPices" runat="server" Height="16px" Visible="False" Width="16px"></asp:TextBox>
    <asp:TextBox ID="txtHistoryID" runat="server" Height="16px" Width="17px" Visible="False"></asp:TextBox>
    <asp:HiddenField ID="hfTotalPieces" runat="server" Visible="False" />
    <asp:TextBox ID="txtMasterID" runat="server" Height="16px" Width="17px" Visible="False"></asp:TextBox>
    <asp:TextBox ID="txtDetailsID" runat="server" Visible="false"></asp:TextBox>

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
