<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Purchase.master" CodeFile="OralOrder.aspx.vb" Inherits="Purchase_OralOrder" EnableEventValidation="false" ValidateRequest="false" %>

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

        function RejectedQty() {
            if ((document.getElementById('<%= txtQuantity.ClientID %>').value != "") && (document.getElementById('<%= txtReceivedQty.ClientID %>').value != "")) {
                if (parseFloat(document.getElementById('<%= txtQuantity.ClientID %>').value) > parseFloat(document.getElementById('<%= txtReceivedQty.ClientID %>').value)) {
                   alert("Accepted Quantity is Greater than Received Qty");
                   document.getElementById('<%= txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%= txtQuantity.ClientID %>').focus()
                    return false;
                }
            }

            if (document.getElementById('<%= txtReceivedQty.ClientID %>').value == "") {
                alert("Enter Received Quantity.")
                document.getElementById('<%= txtReceivedQty.ClientID %>').focus()
                return false;
            }

            var accptQty = document.getElementById('<%=txtQuantity.ClientID %>').value
            var rsvdQty = document.getElementById('<%=txtReceivedQty.ClientID %>').value

            var sqty = 0
            if (parseFloat(rsvdQty) > parseFloat(accptQty)) {

                sqty = (parseFloat(rsvdQty) - parseFloat(accptQty))
                //alert(sqty)
            }
            else {
                sqty = 0
            }
            if (sqty < 0) {
                sqty = 0
            }
            document.getElementById('<%= txtRejectedQty.ClientID %>').value = sqty;
              recievedAmount()
              return true;
          }

          function recievedAmount() {
              if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
                var ssplacedqty = parseFloat(document.getElementById('<%=txtQuantity.ClientID %>').value).toFixed(2)
                var ssMRP = parseFloat(document.getElementById('<%=txtRate.ClientID %>').value).toFixed(2)
                var ssTotal = parseFloat((ssplacedqty) * (ssMRP)).toFixed(2)
                document.getElementById('<%=txtRateAmount.ClientID %>').value = ssTotal
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = ssTotal
                var ssGSTRate = parseFloat(document.getElementById('<%=txtGST.ClientID %>').value).toFixed(2)
            }
            if (document.getElementById('<%=txtGST.ClientID %>').value != "") {

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value).toFixed(2)
                    var sAmount = parseFloat(parseFloat(ssTotalAmt)).toFixed(2)

                    var sGSTAmount = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)
                    document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((parseFloat(sAmount) * parseFloat(ssGSTRate)) / 100).toFixed(2)


                    var sNetAmt = parseFloat(parseFloat(sAmount) + parseFloat(sGSTAmount)).toFixed(2)
                    document.getElementById('<%=txtTotalAmount.ClientID %>').value = sNetAmt
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

        function CalculateFromVat() {
            var Excise, Frieght, FrieghtAmount, DiscountAmount, Discount
            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
                Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
            }
            else {
                Discount = 0
            }
            <%--  if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                document.getElementById('<%=ddlCst.ClientID %>').selectedIndex = 1
            }
            if (document.getElementById('<%=txtExcise.ClientID %>').value != "") {
                Excise = document.getElementById('<%=txtExcise.ClientID %>').value
              }
              else {
                  Excise = 0
              }--%>
             <%-- if (document.getElementById('<%=txtFreight.ClientID %>').value != "") {
                Frieght = document.getElementById('<%=txtFreight.ClientID %>').value
             }
             else {
                 var Frieght = 0
              }--%>




            if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex > 0) {
                if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                     alert("Enter Rate Field")
                     document.getElementById('<%=txtRate.ClientID %>').focus()
                     return false;
                 }
                 else {
                     if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
                        var num
                        num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
                        if (num == false) {
                            alert("Enter only integers for Quantity.")
                            document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                           document.getElementById('<%=txtQuantity.ClientID %>').focus()
                           return false
                       }
                       var re = /^\s*$/;
                       if (re.test(num)) {
                           alert("Enter Quantity spaces are not allowed.");
                           document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                            document.getElementById('<%=txtQuantity.ClientID %>').focus();
                            return false;
                        }
                    }
                    if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 1) {
                        var Rate = document.getElementById('<%=txtRate.ClientID %>').value
                            var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                            var Total = Rate * Quantity
                            document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
              document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
                            var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                            DiscountAmount = ((Rate) * Discount) / 100
                            document.getElementById('<%=txtDiscountAmount.ClientID %>').value = DiscountAmount.toFixed(2)
              document.getElementById("<%=hfDiscountAmount.ClientID %>").value = DiscountAmount.toFixed(2)
                            //'Excise DUty
           <%--   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
              DiscountAmount = ((Rate) * Discount) / 100
              var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
              document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                       document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)--%>
                            //Frieght
                            FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
                            document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
              document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
                            // VAT
                            <%-- var VATAmount = 0
              if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                           var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }--%>
                            // CST
                     <%--  var CSTAmount = 0
                       if (document.getElementById('<%=ddlCst.ClientID %>').selectedIndex > 0) {
                           var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                           var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                           CSTAmount = (Rate * CST) / 100
                           CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }--%>
                            // Total AMount
                            var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount + FrieghtAmount
                            document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                       document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
                        }
                        else {

                            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
                                var Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
              }
              else {
                  var Discount = 0
              }
              var Rate = document.getElementById('<%=txtRate.ClientID %>').value
                            var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                            var iPices = parseInt(document.getElementById("<%=hfTotalPieces.ClientID %>").value)
                            var Total = ((iPices * Quantity) * Rate)
                            document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
              document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
                            var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                            DiscountAmount = ((Rate) * Discount) / 100
                            //'Excise DUty'
                            <%--                    var Excise = document.getElementById('<%=txtExcise.ClientID %>').value--%>
                            <%--  var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
              document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                       document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)--%>

                            <%--  //   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
              FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
              document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
                       document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)--%>

                            <%-- if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {--%>
                            // VAT
                            <%-- var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           var VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }--%>
                            // CST
                      <%-- if (document.getElementById('<%=ddlCst.ClientID %>').selectedIndex > 0) {
                  var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                           var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                           var CST = ddlCST.options[ddlvatt.selectedIndex].innerHTML;
                           var CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }--%>
                            // Total AMount
                            var TotalAmount = Total + ExciseAmount + FrieghtAmount + VATAmount + CSTAmount
                            document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                       document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)

                        }
                    }
                }
            }
            function CalculateUsingRate() {
                document.getElementById('<%=txtDiscount.ClientID %>').value = ""
    document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""

    if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex > 0) {
        if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                     alert("Enter Quantity Field")
                     document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false;
                }
                else {
                    if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
                         var num
                         num = OnlyNumber(document.getElementById('<%=txtQuantity.ClientID %>').value)
                        if (num == false) {
                            alert("Enter only integers for Quantity.")
                            document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                            document.getElementById('<%=txtQuantity.ClientID %>').focus()
                            return false
                        }
                        var re = /^\s*$/;
                        if (re.test(num)) {
                            alert("Enter Quantity spaces are not allowed.");
                            document.getElementById('<%=txtQuantity.ClientID%>').value = ""
                   document.getElementById('<%=txtQuantity.ClientID%>').focus();
                   return false;
               }
           }
           if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 1) {
                         var Rate = document.getElementById('<%=txtRate.ClientID %>').value
               var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
               var Total = Rate * Quantity
               document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                    document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
               //'Excise DUty'
               <%-- var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                    var ExciseAmount = (Rate * Excise) / 100
                    document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                   document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)--%>
               // VAT
               <%--var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                    var VATAmount = (Rate * VAT) / 100
                    document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                   document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)--%>
               // CST
                   <%-- var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                    var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                    var CSTAmount = (Rate * CST) / 100
                    document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                   document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)--%>
               // Total AMount
               var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount
               document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                    document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
           }
           else {
               var Rate = document.getElementById('<%=txtRate.ClientID %>').value
               var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
               var iPices = parseInt(document.getElementById("<%=hfTotalPieces.ClientID %>").value)
               var Total = ((iPices * Quantity) * Rate)
               document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                    document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)

               //'Excise DUty'
               <%--var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                    var ExciseAmount = (Rate * Excise) / 100
                    document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                   document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)--%>
               // VAT
               <%-- var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                    var VATAmount = (Rate * VAT) / 100
                    document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                   document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)--%>
               // CST
                    <%--var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                    var CST = ddlCST.options[ddlvatt.selectedIndex].innerHTML;
                    var CSTAmount = (Rate * CST) / 100
                    document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                   document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)--%>
               // Total AMount
               var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount
               document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                    document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>

           }
                 }
             }




}
        //*******************preeti

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

            var ssGSTRate = parseFloat(document.getElementById('<%=txtGST.ClientID %>').value).toFixed(2)
            if (document.getElementById('<%=txtGST.ClientID %>').value != "") {

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
                //alert("GSTAmount" + sGSTAmount)

                document.getElementById('<%=txtGSTAmount.ClientID %>').value = sGSTAmount

                var sNetAmt = parseFloat(parseFloat(sAmount) + parseFloat(sGSTAmount)).toFixed(2)
                //alert("NetAmt" + sNetAmt)
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = sNetAmt
             
 <%-- if (document.getElementById('<%=txtFreight.ClientID %>').value != "")
            {
                    var qty = parseFloat(document.getElementById('<%=txtQuantity.ClientID %>').value).toFixed(2)
                    var MRP = parseFloat(document.getElementById('<%=txtRate.ClientID %>').value).toFixed(2)
                    var Total = parseFloat((qty) * (MRP)).toFixed(2)
                    document.getElementById('<%=txtRateAmount.ClientID %>').value = Total //quantityamount
                    var DISCOUNTRate = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value).toFixed(2)
                    var ssDiscountAmount = parseFloat(((parseFloat(Total)) * parseFloat(DISCOUNTRate)) / 100).toFixed(2)
                    document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscountAmount
                    sGSTRatee = parseFloat(document.getElementById('<%=txtGST.ClientID %>').value).toFixed(2)
                    var sAmounte = parseFloat((parseFloat(Total) - parseFloat(ssDiscountAmount))).toFixed(2)
                    var sGSTAmountee = parseFloat((parseFloat(sAmounte) * parseFloat(sGSTRatee)) / 100).toFixed(2)
                    document.getElementById('<%=txtGSTAmount.ClientID %>').value = sGSTAmountee
                    sfrieghtRatee = parseFloat(document.getElementById('<%=txtFreight.ClientID %>').value).toFixed(2)
                    var sfrieghtAmounte = parseFloat((parseFloat(sAmounte) * parseFloat(sfrieghtRatee)) / 100).toFixed(2)
                    document.getElementById('<%=txtFreightAmount.ClientID %>').value = sfrieghtAmounte
                    var totalamte = parseFloat(parseFloat(sGSTAmountee) + parseFloat(sfrieghtAmounte)).toFixed(2)
                    alert("frieght and gst amount :" + totalamte)
                    alert(" Total and discout amount minused amount :" + sAmounte)
                    var stotalofamt_QGF = (parseFloat(totalamte) + parseFloat(sAmounte)).toFixed(2)
                    alert(" Total amount is :" + stotalofamt_QGF)
                    document.getElementById('<%=txtTotalAmount.ClientID %>').value = stotalofamt_QGF
           }--%>
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



    //************************
    function CalculateDiscount() {
        var Rate = 0, Discount = 0, vat = 0, cst = 0, excise = 0, ExciseAmount = 0, discountAmount = 0, frieghtAmount = 0, frieght = 0, VATAmount = 0, CSTAmount = 0
        if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Fill All the Required fields")
                return false
            } else {
                if (document.getElementById('<%=txtRateAmount.ClientID %>').value == "") {
                    Rate = 0

                } else {

                    Rate = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
                 }
                 if (document.getElementById('<%=txtDiscount.ClientID %>').value == "") {
                    Discount = 0
                } else {
                    if (document.getElementById('<%=txtDiscount.ClientID %>').value < 0) {
                          alert("Discount Cannot be negative number")
                          document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                          document.getElementById("<%=hfDiscountAmount.ClientID %>").value = ""
                        document.getElementById('<%=txtDiscount.ClientID %>').value = ""
                        document.getElementById('<%=txtDiscount.ClientID %>').focus()
                        return false
                    }
                    if (document.getElementById('<%=txtDiscount.ClientID %>').value > 100) {
                          alert("Discount Cannot be greater than 100")
                          document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                          document.getElementById("<%=hfDiscountAmount.ClientID %>").value = ""
                          document.getElementById('<%=txtDiscount.ClientID %>').value = ""
                          document.getElementById('<%=txtDiscount.ClientID %>').focus()
                          return false
                      }
                      else {
                          Discount = parseFloat(document.getElementById('<%=txtDiscount.ClientID %>').value)
                      }

                  }

                  <%--if (document.getElementById('<%=txtCSTAmount.ClientID %>').value == "") {
                    cst = 0
                } else {
                    cst = parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)
                  }
                  if (document.getElementById('<%=txtExcise.ClientID %>').value == "") {
                    Excise = 0
                } else {
                    Excise = parseFloat(document.getElementById('<%=txtExcise.ClientID %>').value)
                  }
                  if (document.getElementById("<%=hfVatAmount.ClientID %>").value == "") {
                    vat = 0
                } else {
                    vat = parseFloat(document.getElementById("<%=hfVatAmount.ClientID %>").value)
                  }
                  if (document.getElementById("<%=hfExciseAmount.ClientID %>").value == "") {
                    ExciseAmount = 0
                } else {
                    ExciseAmount = parseFloat(document.getElementById("<%=hfExciseAmount.ClientID %>").value)
                  }--%>
                if (document.getElementById("<%=hfFreightAmount.ClientID %>").value == "") {
                    frieghtAmount = 0
                } else {
                    frieghtAmount = parseFloat(document.getElementById("<%=hfFreightAmount.ClientID %>").value)
                  }

                  if (document.getElementById("<%=txtFreight.ClientID %>").value == "") {
                    frieght = 0
                } else {
                    frieght = parseFloat(document.getElementById("<%=txtFreight.ClientID %>").value)
                   }

                   if (document.getElementById("<%=txtDiscount.ClientID %>").value == "") {
                    discount = 0
                } else {
                    discount = parseFloat(document.getElementById("<%=txtDiscount.ClientID %>").value)
                    }
                //'Excise DUty'
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                discountAmount = (Rate * Discount) / 100

                var ExciseAmount = ((Rate - discountAmount) * Excise) / 100

                //Frieght
                <%--frieghtAmount = (((Rate - discountAmount) + ExciseAmount) * frieght) / 100
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)

                document.getElementById('<%=txtFreightAmount.ClientID %>').value = frieghtAmount.toFixed(2)
                document.getElementById("<%=hfFreightAmount.ClientID %>").value = frieghtAmount.toFixed(2)--%>

                <%--if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                    var DiscountAmount = (Rate * Discount) / 100
                    // VAT
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                    var VATAmount = (((Rate - discountAmount) + ExciseAmount + frieghtAmount) * VAT) / 100
                    document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                    document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                }--%>
                // CST
                <%--if (document.getElementById('<%=ddlCst.ClientID %>').selectedIndex > 0) {
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                    var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                    var CSTAmount = (((Rate - discountAmount) + ExciseAmount + frieghtAmount) * CST) / 100
                    document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                    document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                }--%>

                var Subtotal = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
                var total = parseFloat(CSTAmount + ExciseAmount + VATAmount + Subtotal).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = discountAmount.toFixed(2)
                document.getElementById('<%=txtTotalAmount.ClientID %>').value = (total - discountAmount).toFixed(2)
                document.getElementById("<%=hfDiscountAmount.ClientID %>").value = discountAmount.toFixed(2)
                document.getElementById("<%=hfTotalAmount.ClientID %>").value = (total - discountAmount).toFixed(2)
            }
        }


        function CalculateFromVat() {

            var Excise, Frieght, FrieghtAmount, DiscountAmount, Discount
            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
                Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
            }
            else {
                Discount = 0
            }

            <%-- if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                document.getElementById('<%=ddlCst.ClientID %>').selectedIndex = 1
              }--%>

              <%--if (document.getElementById('<%=txtExcise.ClientID %>').value != "") {

                Excise = document.getElementById('<%=txtExcise.ClientID %>').value
             }
             else {
                 Excise = 0
             }--%>

            if (document.getElementById('<%=txtFreight.ClientID %>').value != "") {

                Frieght = document.getElementById('<%=txtFreight.ClientID %>').value
             }
             else {
                 var Frieght = 0
             }

             if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex > 0) {
                if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                     alert("Enter Rate Field")
                     document.getElementById('<%=txtRate.ClientID %>').focus()
                    return false;
                }
                else {
                    if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
                        var num
                        num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
                       if (num == false) {
                           alert("Enter only integers for Quantity.")
                           document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                            document.getElementById('<%=txtQuantity.ClientID %>').focus()
                            return false
                        }
                        var re = /^\s*$/;
                        if (re.test(num)) {
                            alert("Enter Quantity spaces are not allowed.");
                            document.getElementById('<%=txtQuantity.ClientID%>').value = ""
                                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                                return false;
                            }
                        }

                        if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 1) {
                        var Rate = document.getElementById('<%=txtRate.ClientID %>').value
              var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
              var Total = Rate * Quantity
              document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                       document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
              var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value

              DiscountAmount = ((Rate) * Discount) / 100
              document.getElementById('<%=txtDiscountAmount.ClientID %>').value = DiscountAmount.toFixed(2)
                       document.getElementById("<%=hfDiscountAmount.ClientID %>").value = DiscountAmount.toFixed(2)

              //'Excise DUty
                      <%-- var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                       DiscountAmount = ((Rate) * Discount) / 100
                       var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                       document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                    document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)--%>
              //Frieght
              FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
              document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
                       document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
              // VAT
                       <%--var VATAmount = 0
                       if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                         var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }
                       // CST
                       var CSTAmount = 0
                       if (document.getElementById('<%=ddlCst.ClientID %>').selectedIndex > 0) {
                           var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                           var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                           CSTAmount = (Rate * CST) / 100
                           CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }--%>
              // Total AMount

              var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount + FrieghtAmount

              document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                       document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
          }
          else {

              if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
                  var Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
                       }
                       else {
                           var Discount = 0
                       }
                       var Rate = document.getElementById('<%=txtRate.ClientID %>').value
              var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
              var iPices = parseInt(document.getElementById("<%=hfTotalPieces.ClientID %>").value)
              var Total = ((iPices * Quantity) * Rate)
              document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                       document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
              var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
              DiscountAmount = ((Rate) * Discount) / 100
              //'Excise DUty'
              <%--                    var Excise = document.getElementById('<%=txtExcise.ClientID %>').value--%>
              <%-- var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                       document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                       document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)--%>

                      <%-- //   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                       FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
                       document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
                       document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)

                       if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                           // VAT
                           var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           var VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }
                       // CST
                       if (document.getElementById('<%=ddlCst.ClientID %>').selectedIndex > 0) {
                           var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                           var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                           var CST = ddlCST.options[ddlvatt.selectedIndex].innerHTML;
                           var CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }--%>
              // Total AMount
              var TotalAmount = Total + ExciseAmount + FrieghtAmount + VATAmount + CSTAmount
              document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                       document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                    <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
          }
                }
             }


            var accptQty = document.getElementById('<%= txtQuantity.ClientID %>').value
            var rsvdQty = document.getElementById('<%= txtReceivedQty.ClientID %>').value
            var sqty = 0
            if (rsvdQty > accptQty) {
                sqty = (parseFloat(rsvdQty) - parseFloat(accptQty))
            }
            else {
                sqty = 0
            }
            if (sqty < 0) {
                sqty = 0
            }

            document.getElementById('<%= txtRejectedQty.ClientID %>').value = sqty
        }



        function CalculateFromCST() {
            var Excise, Frieght, FrieghtAmount, DiscountAmount, Discount
            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
                Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
            }
            else {
                Discount = 0
            }

       <%--     if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
        document.getElementById('<%=ddlCst.ClientID %>').selectedIndex = 1
              }

              if (document.getElementById('<%=txtExcise.ClientID %>').value != "") {

        Excise = document.getElementById('<%=txtExcise.ClientID %>').value
             }
             else {
                 Excise = 0
             }--%>

            if (document.getElementById('<%=txtFreight.ClientID %>').value != "") {

                Frieght = document.getElementById('<%=txtFreight.ClientID %>').value
             }
             else {
                 var Frieght = 0
             }

             if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex > 0) {
                if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                     alert("Enter Rate Field")
                     document.getElementById('<%=txtRate.ClientID %>').focus()
                    return false;
                }
                else {
                    if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
                var num
                num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
                       if (num == false) {
                           alert("Enter only integers for Quantity.")
                           document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                            document.getElementById('<%=txtQuantity.ClientID %>').focus()
                            return false
                        }
                        var re = /^\s*$/;
                        if (re.test(num)) {
                            alert("Enter Quantity spaces are not allowed.");
                            document.getElementById('<%=txtQuantity.ClientID%>').value = ""
                                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                                return false;
                            }
                        }

                        if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 1) {
                var Rate = document.getElementById('<%=txtRate.ClientID %>').value
              var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
              var Total = Rate * Quantity
              document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                       document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
              var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value

              DiscountAmount = ((Rate) * Discount) / 100
              document.getElementById('<%=txtDiscountAmount.ClientID %>').value = DiscountAmount.toFixed(2)
                       document.getElementById("<%=hfDiscountAmount.ClientID %>").value = DiscountAmount.toFixed(2)

              //'Excise DUty
                     <%--  var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                       DiscountAmount = ((Rate) * Discount) / 100
                       var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                       document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                    document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)
                       //Frieght
                       FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
                       document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
                       document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
                       // VAT
                       var VATAmount = 0
                       if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                         var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }
                       // CST
                       var CSTAmount = 0
                       if (document.getElementById('<%=ddlCst.ClientID %>').selectedIndex > 0) {
                           var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                           var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                           CSTAmount = (Rate * CST) / 100
                           CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }--%>
              // Total AMount

              var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount + FrieghtAmount

              document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                       document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
          }
          else {

              if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
                  var Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
                       }
                       else {
                           var Discount = 0
                       }
                       var Rate = document.getElementById('<%=txtRate.ClientID %>').value
              var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
              var iPices = parseInt(document.getElementById("<%=hfTotalPieces.ClientID %>").value)
              var Total = ((iPices * Quantity) * Rate)
              document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                       document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
              var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
              DiscountAmount = ((Rate) * Discount) / 100
              //'Excise DUty'
              <%--                    var Excise = document.getElementById('<%=txtExcise.ClientID %>').value--%>
                      <%-- var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                       document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                       document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)

                       //   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                       FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
                       document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
                       document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)

                       if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                           // VAT
                           var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           var VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }
                       // CST
                       if (document.getElementById('<%=ddlCst.ClientID %>').selectedIndex > 0) {
                           var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                           var ddlCST = document.getElementById('<%=ddlCst.ClientID %>');
                           var CST = ddlCST.options[ddlvatt.selectedIndex].innerHTML;
                           var CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }--%>
              // Total AMount
              var TotalAmount = Total + ExciseAmount + FrieghtAmount + VATAmount + CSTAmount
              document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                       document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                    <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
          }
        }
             }
        }
        function Amount() {

            if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=txtReceivedQty.ClientID %>').value != "")) {
                if (parseFloat(document.getElementById(sQuantity).value) > parseFloat(document.getElementById(sRecQty).value)) {
                    alert("Accepted Quantity is Greater than Received Qty");
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false;
                }
            }

            if (document.getElementById('<%=txtReceivedQty.ClientID %>').value == "") {
                alert("Enter Received Quantity.")
                document.getElementById('<%=txtReceivedQty.ClientID %>').focus()
                return false
            }

            var sOrderQty = document.getElementById('<%=txtReceivedQty.ClientID %>').value
            var sRecQty1 = parseFloat(document.getElementById('<%=txtQuantity.ClientID %>').value)
            if (sOrderQty > sRecQty1) {
                var sqty = (parseFloat(document.getElementById('<%=txtReceivedQty.ClientID %>').value) - parseFloat(sRecQty1))
            }
            else {
                var sqty = (parseFloat(document.getElementById(sOrder).innerHTML) - parseFloat(sRecQty1))
            }
            if (sqty < 0) {
                sqty = 0
            }
            document.getElementById('<%=txtRejectedQty.ClientID %>').value = sqty
            return true;
        }

        function CalculateFrieght() {

            var Rate = 0, Discount = 0, vat = 0, cst = 0, excise = 0, exciseAmount = 0, FrieghtAmount = 0, DiscountAmount = 0, Frieght = 0, VATAmount = 0, CSTAmount = 0

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Fill All the Required fields")
                return false
            } else {
                if (document.getElementById('<%=txtRateAmount.ClientID %>').value == "") {
                    Rate = 0

                } else {

                    Rate = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
                }

                 <%-- if (document.getElementById('<%=txtExcise.ClientID %>').value == "") {
                    excise = 0
                } else {
                    if (document.getElementById('<%=txtExcise.ClientID %>').value < 0) {
                          alert("Excise Cannot be negative number")
                          document.getElementById('<%=txtExciseAmount.ClientID %>').value = ""
                          document.getElementById("<%=hfExciseAmount.ClientID %>").value = ""
                          document.getElementById('<%=txtExcise.ClientID %>').value = ""
                          document.getElementById('<%=txtExcise.ClientID %>').focus()
                          return false
                      }
                      if (document.getElementById('<%=txtExcise.ClientID %>').value > 100) {
                          alert("Excise Cannot be greater than 100")
                          document.getElementById('<%=txtExciseAmount.ClientID %>').value = ""
                          document.getElementById("<%=hfExciseAmount.ClientID %>").value = ""
                          document.getElementById('<%=txtExcise.ClientID %>').value = ""
                          document.getElementById('<%=txtExcise.ClientID %>').focus()
                          return false
                      }
                      else {
                          excise = parseFloat(document.getElementById('<%=txtExcise.ClientID %>').value)
                      }--%>
            }
            if (document.getElementById('<%=txtFreight.ClientID %>').value == "") {
                Frieght = 0
            } else {
                if (document.getElementById('<%=txtFreight.ClientID %>').value < 0) {
                          alert("Excise Cannot be negative number")
                          document.getElementById('<%=txtFreightAmount.ClientID %>').value = ""
                    document.getElementById("<%=hfFreightAmount.ClientID %>").value = ""
                        document.getElementById('<%=txtFreight.ClientID %>').value = ""
                        document.getElementById('<%=txtFreight.ClientID %>').focus()
                        return false
                    }
                    if (document.getElementById('<%=txtFreight.ClientID %>').value > 100) {
                          alert("Excise Cannot be greater than 100")
                          document.getElementById('<%=txtFreightAmount.ClientID %>').value = ""
                    document.getElementById("<%=hfFreightAmount.ClientID %>").value = ""
                          document.getElementById('<%=txtFreight.ClientID %>').value = ""
                          document.getElementById('<%=txtFreight.ClientID %>').focus()
                          return false
                      }
                      else {
                          Frieght = parseFloat(document.getElementById('<%=txtFreight.ClientID %>').value)
                      }
                  }

                  <%--if (document.getElementById('<%=txtCSTAmount.ClientID %>').value == "") {
                    cst = 0
                } else {
                    cst = parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)
                  }

                  if (document.getElementById("<%=hfVatAmount.ClientID %>").value == "") {
                    vat = 0
                } else {
                    vat = parseFloat(document.getElementById("<%=hfVatAmount.ClientID %>").value)
                  }--%>
            if (document.getElementById("<%=txtDiscount.ClientID %>").value == "") {
                Discount = 0
            } else {
                Discount = parseFloat(document.getElementById("<%=txtDiscount.ClientID %>").value)
                  }
                  DiscountAmount = (Rate * Discount) / 100
            <%--if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {

                    // VAT
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var exciseAmount = ((Rate - DiscountAmount) * excise) / 100
                    var FrieghtAmount = (((Rate - DiscountAmount) + exciseAmount) * Frieght) / 100
                    var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                    var VATAmount = (((Rate - DiscountAmount) + exciseAmount + FrieghtAmount) * VAT) / 100
                    document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                    document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                }
                // CST
                if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {

                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var exciseAmount = ((Rate - DiscountAmount) * excise) / 100
                    var FrieghtAmount = (((Rate - DiscountAmount) + exciseAmount) * Frieght) / 100
                    var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                    var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                    var CSTAmount = (((Rate - DiscountAmount) + exciseAmount + FrieghtAmount) * CST) / 100
                    document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                    document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                }--%>

            var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
            var exciseAmount = ((Rate - DiscountAmount) * excise) / 100
            var FrieghtAmount = (((Rate - DiscountAmount) + exciseAmount) * Frieght) / 100

            var Subtotal = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
                var total = parseFloat(CSTAmount + exciseAmount + FrieghtAmount + VATAmount + Subtotal).toFixed(2)
                document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)

                document.getElementById('<%=txtTotalAmount.ClientID %>').value = (total - DiscountAmount).toFixed(2)
            document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
            document.getElementById("<%=hfTotalAmount.ClientID %>").value = (total - DiscountAmount).toFixed(2)
        }
        // }

        ////***************preeti
      <%--  function CalculateeeFrieght() {

            var Rate = 0, Discount = 0,  FrieghtAmount = 0, DiscountAmount = 0, Frieght = 0,GSTamt=0

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Fill All the Required fields")
                return false
            } else {
                if (document.getElementById('<%=txtRateAmount.ClientID %>').value == "") {
                    Rate = 0

                } else {

                    Rate = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
                  }

                 
                  }
                  if (document.getElementById('<%=txtFreight.ClientID %>').value == "") {
                    Frieght = 0
                } else {
                    if (document.getElementById('<%=txtFreight.ClientID %>').value < 0) {
                        alert("FreightAmount Cannot be negative number")
                    document.getElementById('<%=txtFreightAmount.ClientID %>').value = ""
                          document.getElementById("<%=hfFreightAmount.ClientID %>").value = ""
                          document.getElementById('<%=txtFreight.ClientID %>').value = ""
                          document.getElementById('<%=txtFreight.ClientID %>').focus()
                          return false
                      }
                      if (document.getElementById('<%=txtFreight.ClientID %>').value > 100) {
                          alert("FreightAmount Cannot be greater than 100")
                    document.getElementById('<%=txtFreightAmount.ClientID %>').value = ""
                          document.getElementById("<%=hfFreightAmount.ClientID %>").value = ""
                          document.getElementById('<%=txtFreight.ClientID %>').value = ""
                          document.getElementById('<%=txtFreight.ClientID %>').focus()
                          return false
                      }
                      else {
                          Frieght = parseFloat(document.getElementById('<%=txtFreight.ClientID %>').value)

                      }
                  }
             
                  if (document.getElementById("<%=txtDiscount.ClientID %>").value == "") {
                    Discount = 0
                } else {
                    Discount = parseFloat(document.getElementById("<%=txtDiscount.ClientID %>").value)
                  }
                  DiscountAmount = (Rate * Discount) / 100         
                  var GSTamt= document.getElementById('<%=txtGSTAmount.ClientID %>').value
            var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value         
            var FrieghtAmount = (((Rate - DiscountAmount)) * Frieght) / 100
              document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)         
            var total = (parseFloat(GSTamt) + parseFloat(FrieghtAmount) + parseFloat(Rate)).toFixed(2)      
            document.getElementById('<%=txtTotalAmount.ClientID %>').value = (parseFloat(total) - parseFloat(DiscountAmount)).toFixed(2)        
            document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
            document.getElementById("<%=hfTotalAmount.ClientID %>").value = result
          
            }--%>

        //////////////////*******preeti

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




        function CheckEDate() {

            if (document.getElementById('<%=txtEdate.ClientID %>').value != "") {
                     var inputText = document.getElementById('<%=txtEdate.ClientID %>').value
                     if (!inputText.match(/^(0[1-9]|[12][0-9]|3[01])[\- \/.](?:(0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                         alert('Enter Expire date in dd/MM/yyyy Format');
                         document.getElementById('<%=txtEdate.ClientID %>').value = "";
                    document.getElementById('<%=txtEdate.ClientID %>').focus();
                    return false;
                }
            }
            if ((document.getElementById('<%=txtOrderDate.ClientID %>').innerHTML != "") && (document.getElementById('<%=txtEdate.ClientID %>').value != "")) {
                     var DV5 = DataValid(document.getElementById('<%=txtOrderDate.ClientID %>').innerHTML, document.getElementById('<%=txtEdate.ClientID %>').value);
                if (DV5 == false) {
                    alert("Expire date(" + document.getElementById('<%=txtEdate.ClientID %>').value + ") should be greater than or equal to Order date(" + document.getElementById('<%=txtOrderDate.ClientID %>').innerHTML + ").");
                    document.getElementById('<%=txtEdate.ClientID %>').value = "";
                    return false;
                }

            }

            if ((document.getElementById('<%=txtmdate.ClientID %>').value != "") && (document.getElementById('<%=txtEdate.ClientID %>').value != "")) {
                     var DV5 = DataValid(document.getElementById('<%=txtmdate.ClientID %>').value, document.getElementById('<%=txtEdate.ClientID %>').value);
                if (DV5 == false) {
                    alert("Expire date(" + document.getElementById('<%=txtEdate.ClientID %>').value + ") should be greater than or equal to Manufacture Date (" + document.getElementById('<%=txtmdate.ClientID %>').value + ").");
                    document.getElementById('<%=txtEdate.ClientID %>').value = "";
                    return false;
                }

            }
        }

        function CheckMDate() {

            if (document.getElementById('<%=txtmdate.ClientID %>').value != "") {
                var inputText = document.getElementById('<%=txtmdate.ClientID %>').value
                if (!inputText.match(/^(0[1-9]|[12][0-9]|3[01])[\- \/.](?:(0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                    alert('Enter Manufacture date in dd/MM/yyyy Format');
                    document.getElementById('<%=txtmdate.ClientID %>').value = "";
                    document.getElementById('<%=txtmdate.ClientID %>').focus();
                    return false;
                }
            }
            if ((document.getElementById('<%=txtmdate.ClientID %>').value != "") && (document.getElementById('<%=txtOrderDate.ClientID %>').value != "")) {
                var DV5 = DataValid(document.getElementById('<%=txtmdate.ClientID %>').value, document.getElementById('<%=txtOrderDate.ClientID %>').vaue);
                if (DV5 == false) {
                    alert("Order date(" + document.getElementById('<%=txtOrderDate.ClientID %>').value + ") should be greater than or equal to Manufacture date(" + document.getElementById('<%=txtmdate.ClientID %>').value + ").");
                    document.getElementById(mDate).value = "";
                    return false;
                }
            }

            if ((document.getElementById('<%=txtmdate.ClientID %>').value != "") && (document.getElementById('<%=txtInvoiceDate.ClientID %>').value != "")) {
                var DV5 = DataValid(document.getElementById(mDate).value, document.getElementById('<%=txtInvoiceDate.ClientID %>').value);
                if (DV5 == false) {
                    alert("InvoiceDate date(" + document.getElementById('<%=txtInvoiceDate.ClientID %>').value + ") should be greater than or equal to Manufacture date(" + document.getElementById('<%=txtmdate.ClientID %>').value + ").");
                    document.getElementById('<%=txtmdate.ClientID %>').value = "";
                    return false;
                }
            }
        }

        function checkInvoiceDate() {
            if (document.getElementById('<%=txtInvoiceDate.ClientID %>').value != "") {
                var inputText = document.getElementById('<%=txtInvoiceDate.ClientID %>').value
                if (!inputText.match(/^(0[1-9]|[12][0-9]|3[01])[\- \/.](?:(0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                    alert('Enter Invoice date in dd/MM/yyyy Format');
                    document.getElementById('<%=txtInvoiceDate.ClientID %>').value = "";
                    document.getElementById('<%=txtInvoiceDate.ClientID %>').focus();
                    return false;
                }
            }
            if ((document.getElementById('<%=txtOrderDate.ClientID %>').value != "") && (document.getElementById('<%=txtInvoiceDate.ClientID %>').value != "")) {
                var DV5 = DataValid(document.getElementById('<%=txtOrderDate.ClientID %>').value, document.getElementById('<%=txtInvoiceDate.ClientID %>').value);
                if (DV5 == false) {
                    alert("Invoice date(" + document.getElementById('<%=txtInvoiceDate.ClientID %>').value + ") should be greater than or equal to Ordered Date (" + document.getElementById('<%=txtOrderDate.ClientID %>').value + ").");
                    document.getElementById('<%=txtInvoiceDate.ClientID %>').value = "";
                    return false;
                }

            }
            return;
        }
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Oral Order/Counter Purchase</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refersh" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Approve" />
                    <%-- <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />--%>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnPrint" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" />

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
            <asp:DropDownList ID="ddlAccBrnch" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>



    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:DropDownList ID="ddlExistingOrder" runat="server" CssClass="auto-style1" Width="240px" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div class="col-sm-6 col-md-6 pull-left">
            <asp:Label ID="lblStatus" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Purchase Order No."></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtOrderCode"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order Date"></asp:Label>
            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtOrderDate" ValidateRequestMode="Disabled"></asp:TextBox>
            <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtOrderDate" PopupPosition="BottomLeft"
                TargetControlID="txtOrderDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="ReFVOdate" runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Order Date" ValidationGroup="Validate"></asp:RequiredFieldValidator>
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
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlDSchedule"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDschdule" runat="server" ControlToValidate="ddlDSchedule" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Delivery Schdule" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Delivery Schedule(No. of Days)"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlNumberOfDays"></asp:DropDownList>
            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNofDays" runat="server" ControlToValidate="ddlNumberOfDays" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Number Of days" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Method of Shipping"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlModeOfShipping"></asp:DropDownList>
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
            <asp:Label runat="server" Text="* Type Of Purchase" Visible="false"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlTypeOfSale" Visible="false">
            </asp:DropDownList>
            <%--            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTypeOsale" runat="server" ControlToValidate="ddlTypeOfSale" Display="Dynamic" ErrorMessage="Select Type Of Sale" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Category" Visible="false"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCstCtgry" Visible="false">
            </asp:DropDownList>
            <%--            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCstgry" runat="server" ControlToValidate="ddlCstCtgry" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
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
            <asp:Label runat="server" Text="* Invoice Ref"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtInvoiceRef"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceRef" runat="server" ControlToValidate="txtInvoiceRef" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Invoice ReferenceNo" ValidationGroup="Approve"></asp:RequiredFieldValidator>
            <%--            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceRef" runat="server" ControlToValidate="txtInvoiceRef" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Invoice ReferenceNo" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* E-SugamNo"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtEsugamNo"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEsugamNo" runat="server" ControlToValidate="txtEsugamNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter EsugamNo" ValidationGroup="Approve"></asp:RequiredFieldValidator>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEsugamNo" runat="server" ControlToValidate="txtEsugamNo" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Delivery Chalan No"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtDcNo"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDcNo" runat="server" ControlToValidate="txtDcNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery Chalan No" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>

            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDcNo" runat="server" ControlToValidate="txtDcNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery Chalan No" ValidationGroup="Approve"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtinvoiceDate" ValidationGroup="Approve"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtinvoiceDate" PopupPosition="BottomLeft"
                TargetControlID="txtinvoiceDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceDate" runat="server" ControlToValidate="txtinvoiceDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter invoice Date" ValidationGroup="Approve"></asp:RequiredFieldValidator>
            <%--                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceDate" runat="server" ControlToValidate="txtinvoiceDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter invoice Date" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <br />
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:CheckBox ID="chkboxFrom" runat="server" AutoPostBack="true" Text="Different Address" />
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" Enabled="false" Visible="false" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:CheckBox ID="chkboxTo" runat="server" AutoPostBack="true" Text="Different Address" />
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
            <asp:TextBox ID="txtDeliveryGSTNRegNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
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
        <div class="col-sm-2 col-md-2">
            <br />
            <asp:Button ID="btnCalculate" runat="server" Width="120px" CssClass="btn-ok" Text="Calculate Charge" />
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Other Charges" Visible="false"></asp:Label>
            <asp:TextBox ID="TextBox1" Visible="false" runat="server" CssClass="aspxcontrolsdisable" AutoPostBack="false"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOtherCharge" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter Other Charges." ValidationGroup="TaxTypeValidate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Label runat="server" Text="Paid Amount" Visible="false"></asp:Label>
            <asp:TextBox ID="TextBox2" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
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
                <asp:ListBox ID="chkCategory" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="430px"></asp:ListBox>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="Delivery Required Date"></asp:Label>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRDate"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtRDate" PopupPosition="BottomLeft"
                    TargetControlID="txtRDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%--                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RevRdate" runat="server" ControlToValidate="txtRDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlUnit"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVUnits" runat="server" ControlToValidate="ddlUnit" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateItemAdd"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Rate"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlRate"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Rate Amount"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRate"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RefRate" runat="server" ControlToValidate="txtRate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select the Goods in Items " ValidationGroup="ValidateItemAdd"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Received Quantity"></asp:Label>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtReceivedQty"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReceivedQty" runat="server" ControlToValidate="txtReceivedQty" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Received Quantity" ValidationGroup="ValidateItemAdd"></asp:RequiredFieldValidator>

            </div>

            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="*Accepted Quantity"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtQuantity" ValidationGroup="ValidateItemAdd"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Accepted Quantity" ValidationGroup="ValidateItemAdd"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Quantity Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtRateAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfRateAmount" runat="server" />
                </div>
            </div>

            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Rejected Quantity"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRejectedQty"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Batch Number"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtBatchNumber"></asp:TextBox>
                    <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                </div>
            </div>
            <%-- <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtAccept" ValidationGroup="ValidateQty"></asp:TextBox>
                    <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
            <%--    <asp:HiddenField ID="hfAccept" runat="server" />--%>
            <%--  </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtReject"></asp:TextBox>
                    <asp:HiddenField ID="hfReject" runat="server" />
                </div>
            </div>--%>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Discount"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtDiscount"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="ReDiscount" runat="server" ControlToValidate="txtDiscount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateItemAdd"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Discount Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtDiscountAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                </div>
            </div>
            <%--  <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Excise Duty"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtExcise"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Excise Duty Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtExciseAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfExciseAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="VAT"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlVat"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="VAT Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtVatAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfVatAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="CST"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCst"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="CST Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtCSTAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfCSTAmount" runat="server" />
                </div>
            </div>--%>

            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="GST"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtGST"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="GST Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtGSTAmount"></asp:TextBox>
                    <%--  <asp:HiddenField ID="hfGSTAmount" runat="server" />--%>
                </div>
            </div>



            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Freight" Visible="false"></asp:Label>
                    <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtFreight" Visible="false"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Freight Amount" Visible="false"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtFreightAmount" Visible="false"></asp:TextBox>
                    <asp:HiddenField ID="hfFreightAmount" runat="server" />
                </div>
            </div>


            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px" visible="false">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Manufacture Date"></asp:Label>
                    <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtmdate"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtmdate" PopupPosition="BottomLeft"
                        TargetControlID="txtmdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                    </cc1:CalendarExtender>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Expiry Date"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtEdate"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtEdate" PopupPosition="BottomLeft"
                        TargetControlID="txtEdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                    </cc1:CalendarExtender>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Total Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtTotalAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfTotalAmount" runat="server" />
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:Button ID="btnItemAdd" runat="server" CssClass="btn-ok" Text="Add" ValidationGroup="ValidateItemAdd"></asp:Button>
                </div>
            </div>

        </div>

        <%-- <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
         <div class="col-sm- col-md-3">
             <asp:Label runat="server" Text="* ChargeType"></asp:Label>
             <asp:DropDownList ID="ddlChargeType" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
         </div>
     <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Amount"></asp:Label>
            <asp:TextBox ID="txtShippingRate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
         <div class="col-sm-1 col-md-1">
             <asp:ImageButton ID="imgbtnAddCharge" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" />
         </div>
    </div>--%>

        <%-- <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:GridView ID="GvCharge" runat="server" AutoGenerateColumns="False" Width="50%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="ChargeID" Visible="false" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblChargeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ChargeID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ChargeType" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblChargeType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ChargeType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="ChargeAmount" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblChargeAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ChargeAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnCancel" runat="server" CommandName="Delete" Height="16px" Visible ="false" ImageUrl="~/Images/4delete.gif" ToolTip="Delete/Cancel" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>--%>

        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtGST_ID" Visible="false"></asp:TextBox>

        <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
            <asp:GridView ID="dgPurchase" runat="server" AutoGenerateColumns="False" class="footable">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="07%" Visible="false"></asp:BoundField>--%>
                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CommodityID" HeaderStyle-Width="10%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DescriptionID" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblDescriptionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescriptionID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HistoryID" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="UnitsI" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitsID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "UnitsID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--                    <asp:BoundField DataField="UnitsID" HeaderText="UnitsID" HeaderStyle-Width="15%" Visible="false"></asp:BoundField>--%>
                    <asp:BoundField DataField="Slno" HeaderText="Slno" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:TemplateField HeaderText="Goods">
                        <ItemTemplate>
                            <asp:Label ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Units">
                        <ItemTemplate>
                            <asp:Label ID="Units" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Units") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Rate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="Quantity" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="RateAmount">
                        <ItemTemplate>
                            <asp:Label ID="RateAmount" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "RateAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Discount">
                        <ItemTemplate>
                            <asp:Label ID="Discount" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DiscountAmt">
                        <ItemTemplate>
                            <asp:Label ID="DiscountAmt" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--////*********preeti--%>

                    <asp:TemplateField HeaderText="Charge">
                        <ItemTemplate>
                            <asp:Label ID="Charge" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Charge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="GSTRate">
                        <ItemTemplate>
                            <asp:Label ID="GSTRate" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "GSTRate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="GSTAmount">
                        <ItemTemplate>
                            <asp:Label ID="GSTAmount" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SGST" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="SGST" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "SGST") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SGSTAmount" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="SGSTAmount" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "SGSTAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CGST" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="CGST" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "CGST") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CGSTAmount" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="CGSTAmount" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--/////**************--%>
                    <asp:TemplateField HeaderText="ExciseDuty" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="ExciseDuty" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "ExciseDuty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ExciseAmt" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="ExciseAmt" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "ExciseAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VAT" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="VAT" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "VAT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VATAmt" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="VATAmt" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "VATAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="CST" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="CST" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "CST") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CSTAmount" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="CSTAmount" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "CSTAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TotalAmount">
                        <ItemTemplate>
                            <asp:Label ID="TotalAmount" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="RQty" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblReceivedQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POD_ReceivedQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="RejectedQty" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRejectedQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POD_RejectedQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="AcceptedQty" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAcceptedQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POD_AcceptedQty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="POD_BatchNumber" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPOD_BatchNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POD_BatchNumber") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="POD_ExpiryDate" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPOD_ExpiryDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POD_ExpiryDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="POD_ManufactureDate" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPOD_ManufactureDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POD_ManufactureDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" HeaderStyle-Width="15%" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="Edit1" runat="server" CssClass="hvr-bounce-in" HeaderStyle-Width="15%" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDelete" ToolTip="Delete" CommandName="Delete" runat="server" CssClass="hvr-bounce-in" HeaderStyle-Width="15%" />
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

    <asp:TextBox ID="txtGLID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtItemTableID" runat="server" Visible="false"></asp:TextBox>
</asp:Content>
