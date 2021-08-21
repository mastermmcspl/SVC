<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Sales.master" CodeFile="DispatchDetails.aspx.vb" Inherits="Sales_DispatchDetails" ValidateRequest="false" %>

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
    <%-- <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
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
            $('#<%=grdDispatchDetails.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlSearch.ClientID%>').select2();
            $('#<%=ddldispatchNo.ClientID%>').select2();
            $('#<%=ddlPaymentType.ClientID%>').select2();
        });
    </script>

    <script language="javascript" src="../JavaScripts/General.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">

        function ValidatePaymentType() {
            if (document.getElementById('<%=ddlOrderNo.ClientID %>').selectedIndex == 0) {
                alert('Select Order No')
                document.getElementById('<%=ddlOrderNo.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=txtDispatchDate.ClientID %>').value == "") {
                alert('Enter Invoice Date.');
                document.getElementById('<%=txtDispatchDate.ClientID%>').focus()
                return false;
            }
            <%--if (document.getElementById('<%=ddlChargeType.ClientID %>').selectedIndex == 0) {
                alert('Add Charges.');
                document.getElementById('<%=ddlChargeType.ClientID%>').focus()
                return false;
            }--%>
            <%--if (document.getElementById('<%=ddlSalesType.ClientID %>').selectedIndex == 0) {
                alert('Select Sale Type.')
                document.getElementById('<%=ddlSalesType.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=ddlSalesType.ClientID %>').selectedIndex > 0) {
                var ddlPT = document.getElementById("<%=ddlSalesType.ClientID %>");
                var ddlText = ddlPT.options[ddlPT.selectedIndex].innerHTML;

                if (ddlText == "Inter State Bill") {
                    if (document.getElementById('<%=ddlOthers.ClientID %>').selectedIndex == 0) {
                        alert('Select Others Option.');
                        document.getElementById('<%=ddlOthers.ClientID%>').focus()
                        return false;
                    }
                }
            }--%>

            if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert('Select Mode Of shipping.')
                document.getElementById('<%=ddlModeOfShipping.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlPaymentType.ClientID %>').selectedIndex > 0) {
                var ddlPT = document.getElementById("<%=ddlPaymentType.ClientID %>");
                var ddlPaymentText = ddlPT.options[ddlPT.selectedIndex].innerHTML;
                if (ddlPaymentText == "Cheque") {
                    if (document.getElementById('<%=txtChequeNo.ClientID %>').value == "") {
                        alert('Enter Cheque No.');
                        document.getElementById('<%=txtChequeNo.ClientID%>').focus()
                        return false;
                    }
                    if (document.getElementById('<%=txtChequeNo.ClientID%>').value != "") {
                        var num
                        num = OnlyNumber(document.getElementById('<%=txtChequeNo.ClientID %>').value)
                        if (num == false) {
                            alert("Enter only integers for Cheque No.")
                            document.getElementById('<%=txtChequeNo.ClientID %>').value = ""
                            document.getElementById('<%=txtChequeNo.ClientID %>').focus()
                            return false
                        }
                        var name;
                        name = document.getElementById('<%=txtChequeNo.ClientID %>').value
                        if (name.length < 6) {
                            alert('Cheque No minimum size(6 Characters).');
                            document.getElementById('<%=txtChequeNo.ClientID%>').focus();
                        return false;
                    }
                    if (name.length > 6) {
                        alert('Cheque No exceeded maximum size(Only 6 Characters).');
                        document.getElementById('<%=txtChequeNo.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(name)) {
                        alert("Enter Cheque No spaces are not allowed.");
                        document.getElementById('<%=txtChequeNo.ClientID%>').focus();
                        return false;
                    }
                }
                if (document.getElementById('<%=txtChequeDate.ClientID %>').value == "") {
                        alert('Enter Cheque Date.');
                        document.getElementById('<%=txtChequeDate.ClientID%>').focus()
                        return false;
                    }
                    if (document.getElementById('<%=txtChequeDate.ClientID %>').value != "") {
                        var numd
                        numd = isDate(document.getElementById('<%=txtChequeDate.ClientID %>').value)
                        if (numd == false) {
                            alert("Enter Valid Cheque Date.")
                            document.getElementById('<%=txtChequeDate.ClientID %>').value = ""
                            document.getElementById('<%=txtChequeDate.ClientID%>').focus()
                            return false
                        }
                    }

                    if (document.getElementById('<%=txtIFSCCode.ClientID %>').value == "") {
                        alert('Enter IFSC Code.');
                        document.getElementById('<%=txtIFSCCode.ClientID%>').focus()
                        return false;
                    }
                    if (document.getElementById('<%=txtIFSCCode.ClientID%>').value != "") {

                        if (!/^[a-zA-Z0-9]+$/.test(document.getElementById('<%=txtIFSCCode.ClientID %>').value)) {
                            alert("Enter Valid IFSC Code(Only alphabets & integers).")
                            document.getElementById('<%=txtIFSCCode.ClientID %>').value = ""
                            document.getElementById('<%=txtIFSCCode.ClientID %>').focus()
                            return false
                        }
                        var name;
                        name = document.getElementById('<%=txtIFSCCode.ClientID %>').value
                        if (name.length < 11) {
                            alert('IFSC Code minimum size(11 Characters).');
                            document.getElementById('<%=txtIFSCCode.ClientID%>').focus();
                            return false;
                        }
                        if (name.length > 11) {
                            alert('IFSC Code exceeded maximum size(Only 11 Characters).');
                            document.getElementById('<%=txtIFSCCode.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(name)) {
                        alert("Enter IFSC Code spaces are not allowed.");
                        document.getElementById('<%=txtIFSCCode.ClientID%>').focus();
                        return false;
                    }
                }

           <%-- if (document.getElementById('<%=txtBranch.ClientID %>').value == "") {
                        alert('Enter Branch.');
                        document.getElementById('<%=txtBranch.ClientID%>').focus()
                    return false;
                }--%>
                    if (document.getElementById('<%=txtBranch.ClientID%>').value != "") {
                        var num
                        num = alpha(document.getElementById('<%=txtBranch.ClientID %>').value)
                        if (num == false) {
                            alert("Enter only alphabets for Branch Name.")
                            document.getElementById('<%=txtBranch.ClientID %>').value = ""
                            document.getElementById('<%=txtBranch.ClientID %>').focus()
                            return false
                        }
                        var name;
                        name = document.getElementById('<%=txtBranch.ClientID %>').value
                        if (name.length > 500) {
                            alert('Branch Name exceeded maximum size(Only 500 Characters).');
                            document.getElementById('<%=txtBranch.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(name)) {
                        alert("Enter Branch Name spaces are not allowed.");
                        document.getElementById('<%=txtBranch.ClientID%>').focus();
                        return false;
                    }
                }

            }
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
            } --%>           
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
                    alert("Enter Valid Delivery From GSTN Reg.No(Only 15 Charecters, Two integers,Five Capital alphabets, Four integers, One Capital alphabet, One integer, then Capital Z, One integer/Alphabet).")
                    document.getElementById('<%=txtDeliveryFromGSTNRegNo.ClientID %>').value = ""
                    document.getElementById('<%=txtDeliveryFromGSTNRegNo.ClientID %>').focus()
                    return false
                }               

            }--%>
            if (document.getElementById('<%=txtDeleveryAddress.ClientID %>').value == "") {
                alert('Enter Shipping To Address.');
                document.getElementById('<%=txtDeleveryAddress.ClientID%>').focus()
                return false;
            }
            <%--if (document.getElementById('<%=txtDeliveryGSTNRegNo.ClientID %>').value == "") {
                alert('Enter Shipping To GSTN Reg.No.');
                document.getElementById('<%=txtDeliveryGSTNRegNo.ClientID%>').focus()
                return false;
            }--%>
            <%--if (document.getElementById('<%=txtDeliveryGSTNRegNo.ClientID %>').value != "") {               
                var num
                num = GSTIN.test(document.getElementById('<%=txtDeliveryGSTNRegNo.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Shipping To GSTN Reg.No(Only 15 Charecters, Two integers,Five Capital alphabets, Four integers, One Capital alphabet, One integer, then Capital Z, One integer/Alphabet).")
                    document.getElementById('<%=txtDeliveryGSTNRegNo.ClientID %>').value = ""
                    document.getElementById('<%=txtDeliveryGSTNRegNo.ClientID %>').focus()
                    return false
                }               

            }--%>


     }


        function CalculateGST(sPlacedqty, sMRP, sTotal, sddlDiscount, sDiscountAmount, sCharges, sAmount, sGSTRate, sGSTAmount, sNetAmount, sHFDiscountAmount, sHFCharges, sHFAmount, sHFGSTAmount, sHFNetAmount, dChargeAmount, dItemsTotalFromDispatch) {
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

        if (document.getElementById(sPlacedqty).innerHTML == "") {
            alert("Enter Quantity.")
            document.getElementById(sPlacedqty).focus();
            return false;
        }
        document.getElementById(sGSTAmount).innerText = ""
        document.getElementById(sDiscountAmount).innerText = ""

        document.getElementById(sHFGSTAmount).value = ""
        document.getElementById(sHFDiscountAmount).value = ""

        var num
        num = CheckDecimal(document.getElementById(sPlacedqty).innerHTML)
        if (num == false) {
            alert("Enter integers/Decimals for Placed Quantity.")
            document.getElementById(sPlacedqty).value = ""
            document.getElementById(sClosingStock).innerHTML = ""
            document.getElementById(sPlacedqty).focus();
            return false;
        }
        var ssplacedqty = document.getElementById(sPlacedqty).innerHTML
        var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
        var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
        document.getElementById(sTotal).innerText = ssTotal

        var ddlDiscountt = document.getElementById(sddlDiscount);
        var ssGSTRate = parseFloat(document.getElementById(sGSTRate).innerText)

        if (ddlDiscountt.selectedIndex > 0) {

            var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)

            document.getElementById(sCharges).innerText = parseFloat(((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(dChargeAmount)) / dItemsTotalFromDispatch).toFixed(2)
            document.getElementById(sHFCharges).value = parseFloat(((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(dChargeAmount)) / dItemsTotalFromDispatch).toFixed(2)

            var ddlDiscountt = document.getElementById(sddlDiscount);
            var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

            var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
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

            document.getElementById(sCharges).innerText = parseFloat(((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(dChargeAmount)) / dItemsTotalFromDispatch).toFixed(2)
            document.getElementById(sHFCharges).value = parseFloat(((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(dChargeAmount)) / dItemsTotalFromDispatch).toFixed(2)

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
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Invoice Details</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ImageUrl="~/Images/Save24.png" ValidationGroup="ValidateDispatch" />
                    <asp:ImageButton ID="imgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" />
                    <asp:TextBox ID="txtMasterID" runat="server" CssClass="aspxcontrols" Visible="false" Width="50px"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>


    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblDispatch" runat="server" Text="Existing Invoice No" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Visible="false"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label ID="lblType" runat="server" Text="Order Type : " Font-Bold ="true" Font-Size ="Medium" ></asp:Label>
            <asp:Label ID="lblOrderType" runat="server" Font-Bold ="true" Font-Size ="Medium"  ></asp:Label>
        </div>
        <div class="col-sm-3 col-md-3">
             <br />
            <asp:Label ID="Label1" runat="server" Text="Status : "></asp:Label>
            <asp:Label ID="lblStatus" runat="server"></asp:Label>
        </div>

    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Dispatch No"></asp:Label>
            <asp:DropDownList ID="ddldispatchNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFvddldispatchNo" runat="server" ControlToValidate="ddldispatchNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Dispatch No" ValidationGroup="ValidateDispatch"></asp:RequiredFieldValidator>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order No"></asp:Label>
            <asp:DropDownList ID="ddlOrderNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlOrderNo" runat="server" ControlToValidate="ddlOrderNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Order No" ValidationGroup="ValidateDispatch"></asp:RequiredFieldValidator>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Allocation No"></asp:Label>
            <asp:DropDownList ID="ddlAllocationNo" runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlAllocationNo" runat="server" ControlToValidate="ddlAllocationNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Allocation No" ValidationGroup="ValidateDispatch"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label ID="lblCustCategoryLabel" runat="server" Text="Customer Category : "></asp:Label>
            <asp:Label ID="lblCustCategory" runat="server"></asp:Label>
        </div>        
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Order Date"></asp:Label>
            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Invoice No"></asp:Label>
            <asp:TextBox ID="txtDispatchNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="E-Sugam No"></asp:Label>
            <asp:TextBox ID="txtESugamNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Customer Name"></asp:Label>
            <asp:DropDownList ID="ddlParty" Enabled="false" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Sales Person"></asp:Label>
            <asp:DropDownList ID="ddlSalesMan" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Mode of Shipping"></asp:Label>
            <asp:DropDownList ID="ddlModeOfShipping" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlModeOfShipping" runat="server" ControlToValidate="ddlModeOfShipping" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Mode Of Shipping" ValidationGroup="ValidateDispatch"></asp:RequiredFieldValidator>
        </div>

         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Company Type"></asp:Label>
            <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>            
        </div>     

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="*GSTN Category"></asp:Label>
            <asp:DropDownList ID="ddlGSTCategory" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>            
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
            <asp:TextBox AutoPostBack="true" ID="txtDispatchDate" runat="server" CssClass="aspxcontrols" placeHolder="dd/MM/yyyy"></asp:TextBox>
            <cc1:CalendarExtender ID="txtDispatchDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDispatchDate" TargetControlID="txtDispatchDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDispatchDate" runat="server" ControlToValidate="txtDispatchDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Invoice Date" ValidationGroup="ValidateDispatch"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtDispatchDate" runat="server" ControlToValidate="txtDispatchDate" Display="Dynamic" ErrorMessage="Enter Valid Invoice Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
           <%-- <asp:RangeValidator ID="rgvtxtDispatchDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtDispatchDate" SetFocusOnError="True"></asp:RangeValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Invoice No"></asp:Label>
            <asp:TextBox ID="txtDispatchRefNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>

         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Expected No. Of Days"></asp:Label>
            <asp:TextBox ID="txtExpectedNoOfDays" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtExpectedNoOfDays" runat="server" ControlToValidate="txtExpectedNoOfDays" Display="Dynamic" ErrorMessage="Enter Valid Expected No. Of Days" SetFocusOnError="True" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
        </div>
        <%--<div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Sale Type"></asp:Label>
            <asp:DropDownList ID="ddlSalesType" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlSalesType" runat="server" ControlToValidate="ddlSalesType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Sale Type" ValidationGroup="ValidateDispatch"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Other Type"></asp:Label>
            <asp:DropDownList ID="ddlOthers" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>--%>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipping charges"></asp:Label>
            <asp:Label ID="lblShippingCharges" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Payment Type"></asp:Label>
            <asp:DropDownList ID="ddlPaymentType" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:HiddenField ID="txtCode" runat="server" />
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Remarks"></asp:Label>
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="aspxcontrols"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* ChargeType" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlChargeType" runat="server" CssClass="aspxcontrolsdisable" Visible="false"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlChargeType" runat="server" ControlToValidate="ddlChargeType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Charge Type" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Amount" Visible="false"></asp:Label>
            <asp:TextBox ID="txtShippingRate" runat="server" CssClass="aspxcontrolsdisable" Visible="false"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" ErrorMessage="Enter Valid Shipping Rate" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="imgbtnAddCharge" Visible ="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" ValidationGroup="ValidateAdd" />
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

                <asp:TemplateColumn Visible ="false" >
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
            <asp:Label runat="server" Text="Total Amount"></asp:Label>
            <asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Trade Discount"></asp:Label>
            <asp:Label ID="lblTradeDiscount" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Trade DiscountAmount"></asp:Label>
            <asp:Label ID="lblTradeDiscountAmount" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Grand Total"></asp:Label>
            <asp:Label ID="lblGrandTotalAmount" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
           
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:CheckBox ID ="chkboxSameAdd" runat ="server" AutoPostBack="true" Visible="false" />
            <asp:Label runat="server" Text="Same as Billing Address" Visible="false" ></asp:Label>            
        </div>
    </div>
        
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Company Address"></asp:Label>
            <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingPinCode" runat="server" ControlToValidate="txtBillingPinCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing PinCode" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillingPinCode" runat="server" ControlToValidate="txtBillingPinCode" Display="Dynamic" ErrorMessage="Enter Valid Billing PinCode" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,6}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Delivered From"></asp:Label>
            <asp:TextBox ID="txtDeliveryFromAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeleveryAddress" runat="server" ControlToValidate="txtDeleveryAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery Address" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Bill To Address"></asp:Label>
            <asp:TextBox ID="txtBillingAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingAddress" runat="server" ControlToValidate="txtBillingAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing Address" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>--%>
        </div>        
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipping To Address"></asp:Label>
            <asp:TextBox ID="txtDeleveryAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeleveryAddress" runat="server" ControlToValidate="txtDeleveryAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery Address" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>--%>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtCompanyGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryPinCode" runat="server" ControlToValidate="txtDeliveryPinCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery PinCode" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliveryPinCode" runat="server" ControlToValidate="txtDeliveryPinCode" Display="Dynamic" ErrorMessage="Enter Valid Delivery PinCode" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,6}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Delivered From GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtDeliveryFromGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingPinCode" runat="server" ControlToValidate="txtBillingPinCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing PinCode" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillingPinCode" runat="server" ControlToValidate="txtBillingPinCode" Display="Dynamic" ErrorMessage="Enter Valid Billing PinCode" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,6}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Bill To GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtBillingGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingPinCode" runat="server" ControlToValidate="txtBillingPinCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing PinCode" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillingPinCode" runat="server" ControlToValidate="txtBillingPinCode" Display="Dynamic" ErrorMessage="Enter Valid Billing PinCode" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,6}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipp To GSTN Reg.No"></asp:Label>
            <asp:TextBox ID="txtDeliveryGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliveryPinCode" runat="server" ControlToValidate="txtDeliveryPinCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery PinCode" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliveryPinCode" runat="server" ControlToValidate="txtDeliveryPinCode" Display="Dynamic" ErrorMessage="Enter Valid Delivery PinCode" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,6}$"></asp:RegularExpressionValidator>--%>
        </div>
    </div>


    <div class="col-md-12">
        <div id="divcollapseChequeDetails" runat="server" visible="false" data-toggle="collapse" data-target="#collapseChequeDetails"><a href="#"><b><i>Click here to view Cheque Details...</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseChequeDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No" Visible ="false" ></asp:Label>
                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="aspxcontrols" Visible ="false" ></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date" Visible ="false" ></asp:Label>
                    <asp:TextBox ID="txtChequeDate" runat="server" CssClass="aspxcontrols" Visible ="false" ></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtChequeDate" TargetControlID="txtChequeDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
                </div>
                
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblBankName" runat="server" Text="Bank Name"></asp:Label>
                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
                    <asp:TextBox ID="txtBranch" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblIFSCCode" runat="server" Text="IFSC Code"></asp:Label>
                    <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>


    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
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

                <%--<asp:TemplateField HeaderText="VAT" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="7%">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlVAT" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="8%">
                    <ItemTemplate>
                        <asp:Label ID="lblVATAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VATAmount") %>'></asp:Label>
                        <asp:HiddenField ID="HFVATAmount" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="CST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="7%">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlCST" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CST Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="8%">
                    <ItemTemplate>
                        <asp:Label ID="lblCSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CSTAmount") %>'></asp:Label>
                        <asp:HiddenField ID="HFCSTAmount" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Exise" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="8%">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlExice" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Exise Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblExiceAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExiceAmount") %>'></asp:Label>
                        <asp:HiddenField ID="HFExiceAmount" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Net Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblNetAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount") %>'></asp:Label>
                        <asp:HiddenField ID="HFNetAmount" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <%-- </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlChargeType" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel> --%>


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
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>


    <div id="ModalAllocationValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divAllocationType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAllocation" runat="server"></asp:Label>
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

    <div class="">
        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
    </div>

    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgJEDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable" Visible ="false">
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

    <asp:TextBox ID ="txtStartDate" runat ="server" Visible ="false" ></asp:TextBox>
    <asp:TextBox ID ="txtEndDate" runat ="server" Visible ="false" ></asp:TextBox>

    <asp:TextBox ID ="txtOrderType" runat ="server" Visible ="false" ></asp:TextBox>
</asp:Content>

