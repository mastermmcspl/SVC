<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Sales.master"  CodeFile="Allocation.aspx.vb" Inherits="Sales_Allocation" ValidateRequest="false" %>

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
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    
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
            $('#<%=dgAllocate.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

         $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlOrderNo.ClientID%>').select2();
            $('#<%=ddlSearch.ClientID%>').select2();
        });
        
        function validateRemarks() {
            if (document.getElementById('<%=ddlOrderNo.ClientID %>').selectedIndex == 0) {
                alert('Select Order No.');
                document.getElementById('<%=ddlOrderNo.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=ddlSearch.ClientID %>').selectedIndex == 0) {
                alert('Select Allocation No.');
                document.getElementById('<%=ddlSearch.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=txtRemarks.ClientID %>').value == "") {
                alert("Enter Remarks.");
                document.getElementById('<%=txtRemarks.ClientID %>').focus;
                return false;
            }
            if (document.getElementById('<%=txtRemarks.ClientID %>').length > 8000) {
                alert("Remarks exceeded maximum size(max 2000 characters).");
                document.getElementById('<%=txtRemarks.ClientID %>').focus;
                return false;
            }
            return true;
        }

        var decimalOnly = /^\s*-?[0-9]\d*(\.\d{1,2})?\s*$/;
        function GetClosingStock(sAvailableStck, sPlacedqty, sClosingStock, sMRP, sTotal, iOrderedQnt, sNetAmount, sPendingQty) {
            if (document.getElementById(sPlacedqty).value != "") {
                var num
                num = decimalOnly.test(document.getElementById(sPlacedqty).value)
                if (num == false) {
                    alert("Enter integers/Decimals for Placed Quantity.")
                    document.getElementById(sPlacedqty).value = ""
                    document.getElementById(sClosingStock).innerHTML = ""
                    document.getElementById(sPlacedqty).focus();
                    return false;
                }

                var ssplacedqty = document.getElementById(sPlacedqty).value
                var ssavailablestock = parseInt(document.getElementById(sAvailableStck).innerHTML)
                var ssOrderedQnt = parseFloat(document.getElementById(iOrderedQnt).innerHTML)
                if ((ssOrderedQnt < ssplacedqty)) {
                    alert("Out of Stock")
                    document.getElementById(sPlacedqty).value = parseInt(parseInt(ssavailablestock) - parseInt(document.getElementById(sClosingStock).innerText))
                    document.getElementById(sPlacedqty).focus();
                    return false;
                }

                //var ssPendingQty = parseInt(document.getElementById(sPendingQty).innerHTML)
                //if (ssPendingQty == 0) {
                //    alert("Pending Qty is 0, No qty to allocate.")
                //    document.getElementById(sPlacedqty).value = ""
                //    return false;
                //}

                //var ssPendingQty = parseInt(document.getElementById(sPendingQty).innerHTML)
                //if ((ssPendingQty < ssplacedqty)) {
                //    alert("Out of Stock")
                //    document.getElementById(sPlacedqty).value = ""
                //    document.getElementById(sPlacedqty).focus();
                //    return false;
                //}

                var ssPendingQty = parseInt(document.getElementById(sPendingQty).innerHTML)
                if (ssPendingQty > 0) {
                    if ((ssPendingQty < ssplacedqty)) {
                        alert("Qty should be lesser/equal to the pending qty.")
                        document.getElementById(sPlacedqty).value = ""
                        document.getElementById(sPlacedqty).focus();
                        return false;
                    }
                }

                document.getElementById(sNetAmount).innerText = ""

                var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
                var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                var ssclosestock = (ssavailablestock - ssplacedqty)
                document.getElementById(sTotal).innerText = ssTotal
                document.getElementById(sClosingStock).innerText = ssclosestock;
                document.getElementById(sNetAmount).innerText = ssTotal

                return true;
            }
            else {
                document.getElementById(sTotal).innerText = ""
                document.getElementById(sClosingStock).innerText = ""
                document.getElementById(sNetAmount).innerText = ""
            }
        }

        function validatePlaceOrder() {
            if (document.getElementById('<%=ddlOrderNo.ClientID %>').selectedIndex == 0) {
                alert('Select Order No.');
                document.getElementById('<%=ddlOrderNo.ClientID%>').focus()
                return false;
            }
        }

        function validateApprove() {
            if (document.getElementById('<%=ddlSearch.ClientID %>').selectedIndex == 0) {
                alert('Select Allocation No.');
                document.getElementById('<%=ddlSearch.ClientID%>').focus()
                return false;
            }
        }

        function digit(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            //comparing pressed keycodes
            if (!(keycode == 8 || keycode == 31) && (keycode < 48 || keycode > 57) && (keycode < 65 || keycode > 90) && (keycode < 97 || keycode > 122)) {
                return false;
            }
            else {
                //Condition to check textbox contains ten numbers or not

                return true;
            }
        }
                

        function CalculateVAT(sPlacedqty, sMRP, sTotal, sddlVAT, sVATAmount, sddlCST, sCSTAmount, sddlExice, sExiceAmount, sddlDiscount, sDiscountAmount, sNetAmount) {
            if (document.getElementById(sPlacedqty).value == "") {
                alert("Enter Quantity.")
                document.getElementById(sPlacedqty).focus();
                return false;
            }
            document.getElementById(sVATAmount).innerText = ""
            document.getElementById(sCSTAmount).innerText = ""
            document.getElementById(sExiceAmount).innerText = ""
            document.getElementById(sDiscountAmount).innerText = ""
            document.getElementById(sNetAmount).innerText = ""
                var num
                num = OnlyNumber(document.getElementById(sPlacedqty).value)
                if (num == false) {
                    alert("Enter only integers for Placed Quantity.")
                    document.getElementById(sPlacedqty).value = ""
                    document.getElementById(sClosingStock).innerHTML = ""
                    document.getElementById(sPlacedqty).focus();
                    return false;
                }

                var ssplacedqty = document.getElementById(sPlacedqty).value
                var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
                var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                document.getElementById(sTotal).innerText = ssTotal

                var ddlDiscountt = document.getElementById(sddlDiscount);
                var ddlvatt = document.getElementById(sddlVAT);
                var ddlcstt = document.getElementById(sddlCST);
                var ddlExicett = document.getElementById(sddlExice);


                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssDiscount = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {
                    var sNetTotalAmt = parseFloat(document.getElementById(sTotal).innerText).toFixed(2)
                    var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssDiscount = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((document.getElementById(sPlacedqty).value != "") && (ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ssplacedqty = document.getElementById(sPlacedqty).value
                    var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
                    var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                    document.getElementById(sTotal).innerText = ssTotal

                    document.getElementById(sNetAmount).innerText = ssTotal
                }


        }
                

        function CalculateCST(sPlacedqty, sMRP, sTotal, sddlVAT, sVATAmount, sddlCST, sCSTAmount, sddlExice, sExiceAmount, sDiscount, sDiscountAmount, sNetAmount) {
            if (document.getElementById(sPlacedqty).value == "") {
                alert("Enter Quantity.")
                document.getElementById(sPlacedqty).focus();
                return false;
            }
            document.getElementById(sVATAmount).innerText = ""
            document.getElementById(sCSTAmount).innerText = ""
            document.getElementById(sExiceAmount).innerText = ""
            document.getElementById(sDiscountAmount).innerText = ""
            document.getElementById(sNetAmount).innerText = ""

                var num
                num = OnlyNumber(document.getElementById(sPlacedqty).value)
                if (num == false) {
                    alert("Enter only integers for Placed Quantity.")
                    document.getElementById(sPlacedqty).value = ""
                    document.getElementById(sClosingStock).innerHTML = ""
                    document.getElementById(sPlacedqty).focus();
                    return false;
                }

                var ssplacedqty = document.getElementById(sPlacedqty).value
                var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
                var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                document.getElementById(sTotal).innerText = ssTotal

                var ddlvatt = document.getElementById(sddlVAT);
                var ddlcstt = document.getElementById(sddlCST);
                var ddlExicett = document.getElementById(sddlExice);

                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {
                    
                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount
                    
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssDiscount = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {
                    var sNetTotalAmt = parseFloat(document.getElementById(sTotal).innerText).toFixed(2)
                    var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssDiscount = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((document.getElementById(sPlacedqty).value != "") && (ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ssplacedqty = document.getElementById(sPlacedqty).value
                    var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
                    var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                    document.getElementById(sTotal).innerText = ssTotal

                    document.getElementById(sNetAmount).innerText = ssTotal
                }


        }
                

        function CalculateExice(sPlacedqty, sMRP, sTotal, sddlVAT, sVATAmount, sddlCST, sCSTAmount, sddlExice, sExiceAmount, sDiscount, sDiscountAmount, sNetAmount) {
            if (document.getElementById(sPlacedqty).value == "") {
                alert("Enter Quantity.")
                document.getElementById(sPlacedqty).focus();
                return false;
            }
            document.getElementById(sVATAmount).innerText = ""
            document.getElementById(sCSTAmount).innerText = ""
            document.getElementById(sExiceAmount).innerText = ""
            document.getElementById(sDiscountAmount).innerText = ""
            document.getElementById(sNetAmount).innerText = ""

                var num
                num = OnlyNumber(document.getElementById(sPlacedqty).value)
                if (num == false) {
                    alert("Enter only integers for Placed Quantity.")
                    document.getElementById(sPlacedqty).value = ""
                    document.getElementById(sClosingStock).innerHTML = ""
                    document.getElementById(sPlacedqty).focus();
                    return false;
                }

                var ssplacedqty = document.getElementById(sPlacedqty).value
                var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
                var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                document.getElementById(sTotal).innerText = ssTotal

                var ddlvatt = document.getElementById(sddlVAT);
                var ddlcstt = document.getElementById(sddlCST);
                var ddlExicett = document.getElementById(sddlExice);

                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var sVATAmt = parseFloat(((ssTotalAmt - sDiscountAMT) * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sVATAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssDiscount = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) + parseFloat(document.getElementById(sCSTAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex > 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlvatt = document.getElementById(sddlVAT);
                    var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                    var ssTotalAmt = parseFloat(document.getElementById(sTotal).innerText)
                    var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
                    document.getElementById(sVATAmount).innerText = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(sVATAmt)).toFixed(2)

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sVATAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex > 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ddlcstt = document.getElementById(sddlCST);
                    var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                    var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sCST)) / 100).toFixed(2);
                    document.getElementById(sCSTAmount).innerText = sCSTAmt

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) + parseFloat(document.getElementById(sCSTAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value == "") {
                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById(sTotal).innerText) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value != "") {
                    var sNetTotalAmt = parseFloat(document.getElementById(sTotal).innerText).toFixed(2)
                    var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount

                    var sNetAmt = parseFloat(parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex > 0) && document.getElementById(sDiscount).value != "") {

                    var ssDiscount = parseFloat((parseFloat(document.getElementById(sTotal).innerText) * parseFloat(document.getElementById(sDiscount).value)) / 100).toFixed(2)
                    document.getElementById(sDiscountAmount).innerText = ssDiscount
                    var sDiscountAMT = ssDiscount

                    var ddlExicett = document.getElementById(sddlExice);
                    var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                    var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById(sTotal).innerText) - sDiscountAMT) * parseFloat(sExice)) / 100).toFixed(2)
                    document.getElementById(sExiceAmount).innerText = sExiceAmt

                    var sNetAmt = parseFloat((parseFloat(document.getElementById(sTotal).innerText) - parseFloat(document.getElementById(sDiscountAmount).innerText)) - parseFloat(document.getElementById(sExiceAmount).innerText)).toFixed(2)
                    document.getElementById(sNetAmount).innerText = sNetAmt
                }
                if ((document.getElementById(sPlacedqty).value != "") && (ddlvatt.selectedIndex == 0) && (ddlcstt.selectedIndex == 0) && (ddlExicett.selectedIndex == 0) && document.getElementById(sDiscount).value == "") {
                    var ssplacedqty = document.getElementById(sPlacedqty).value
                    var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
                    var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                    document.getElementById(sTotal).innerText = ssTotal

                    document.getElementById(sNetAmount).innerText = ssTotal
                }

        }

        function CalculateGrandDiscount() {
            if (document.getElementById('<%=txtGrandDiscount.ClientID %>').value != "") {
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtGrandTotal.ClientID %>').value) * parseFloat(document.getElementById('<%=txtGrandDiscount.ClientID %>').value)) / 100).toFixed(2)
                document.getElementById('<%=txtGrandDiscountAmt.ClientID %>').value = ssDiscount
                document.getElementById('<%=txtGrandTotalAmt.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtGrandTotal.ClientID %>').value) - parseFloat(ssDiscount)).toFixed(2)
            }
            else {
                document.getElementById('<%=txtGrandDiscountAmt.ClientID %>').value = ""
                document.getElementById('<%=txtGrandTotalAmt.ClientID %>').value = parseFloat(document.getElementById('<%=txtGrandTotal.ClientID %>').value)
            }
            
        }

        
        


        
    </script>
    
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Allocation</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" ImageUrl="~/Images/Save24.png" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" />                    
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblErrorUp" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label ID="lblOrderNo" runat="server" Text="* Order No."></asp:Label>
                    <asp:DropDownList ID="ddlOrderNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Customer Name"></asp:Label>
                    <asp:DropDownList ID="ddlParty" runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Existing Allocation No."></asp:Label>
                    <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6">
                    <asp:Label ID="lblAllocationNo" runat="server" Text="* Allocation No"></asp:Label>
                    <asp:TextBox ID="txtOrderCode" autocomplete="off" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblRemarks" runat="server" Text="Remarks"></asp:Label>
            <asp:TextBox ID="txtRemarks" runat="server" autocomplete="off" CssClass="aspxcontrols" TextMode="MultiLine" Height="80px"></asp:TextBox>
        </div>
        <div class="col-sm-2 col-md-2" style="display:none;">
            <br />
            <asp:Label ID="lblStatus" runat="server" Text="Status :-"></asp:Label>
        </div>

    </div>


    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px; overflow: auto">
        <asp:GridView ID="dgAllocate" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>

                <asp:TemplateField HeaderText="ItemsID" Visible="false" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblItemsID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemsID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CommodityID" Visible="false" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemID" Visible="false" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HistoryID" Visible="false" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UnitID" Visible="false" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description Of Goods" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Unit" HeaderText="Unit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:TemplateField HeaderText="Available Stock" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblAvailableStock" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableStock") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MRP" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblMRP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRP") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderQuantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ordered Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderedAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderedAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRO Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblPRODiscount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PRODiscount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRO Discount Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblPRODiscountAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PRODiscountAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRO Total Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblPROTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PROTotalAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtplacedqty" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlacedQuantity") %>' ReadOnly="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderText="Net Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblNetAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Closing Stock" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblClosingStock" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClosingStock") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblPendingQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PendingQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <asp:Panel ID="pnlGrand" runat="server">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-8 col-md-8">
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label ID="lblTotal" runat="server" Text="Total Amount"></asp:Label>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:TextBox ID="txtGrandTotal" CssClass="aspxcontrols" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
            <div class="col-sm-8 col-md-8">
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label ID="lblDiscount" runat="server" Text="Discount"></asp:Label>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:TextBox ID="txtGrandDiscount" CssClass="aspxcontrols" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
            <div class="col-sm-8 col-md-8">
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:TextBox ID="txtGrandDiscountAmt" CssClass="aspxcontrols" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-8 col-md-8">
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label ID="lblGrandTotal" runat="server" Text="Grand Total"></asp:Label>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:TextBox ID="txtGrandTotalAmt" CssClass="aspxcontrols" Width="80%" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ibtnInsert" runat="server" ImageUrl="~/Images/Add16.png" ToolTip="Delete/Cancel" />
            </div>
        </div>

    </asp:Panel>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Button ID="btnReject" runat="server" CssClass="Button" Text="Reject" Width="90px" Visible="False" />
        <asp:HiddenField ID="txtCode" runat="server" /> 
        <asp:Label ID ="lblReAllocateID" runat ="server" Visible ="false" ></asp:Label>
    </div>

    <div class=" modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">customers ordered for this item</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <asp:GridView ID="dgPartyAllocation" runat="server" Width="100%" AutoGenerateColumns="False" class="footable">
                            <Columns>
                                <asp:BoundField DataField="Commodity" HeaderText="Commodity" HeaderStyle-Width="5%"></asp:BoundField>
                                <asp:BoundField DataField="Goods" HeaderText="Goods" HeaderStyle-Width="5%"></asp:BoundField>
                                <asp:BoundField DataField="PartyCode" HeaderText="PartyCode" HeaderStyle-Width="3%"></asp:BoundField>
                                <asp:BoundField DataField="PartyName" HeaderText="PartyName" HeaderStyle-Width="5%"></asp:BoundField>
                                <asp:BoundField DataField="OrderedQty" HeaderText="OrderedQty" HeaderStyle-Width="3%"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:ImageButton ID="imgNAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" CausesValidation="false" ValidationGroup="Validate" />
                    <asp:ImageButton ID="ImgClose" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="ImgNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Close" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <%--<br />
    <asp:Panel ID="PanelYN" runat="server" CssClass="modalPopup" align="Centre" Style="display: table"
        Width="610px" Height="350px">
        <table style="width: 100%">
            <tr >
                <td align="center" class="td_blue1b" >
                    
                </td>
            </tr>
            <tr >
                <td >
                    <asp:Button ID ="Button1" runat ="server" Text ="Ok" Width ="80px" CssClass ="Button "/>
                  </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnEClose" runat="server" CssClass="Button" Height="0px" Width="0px" />
                    <asp:Button ID="btnEShow" runat="server" Enabled="False" Height="0px" Width="0px" />
                </td>
            </tr>
        </table>
    </asp:Panel>--%>



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
