<%@ Page Title="" Language="VB" MasterPageFile="~/Purchase.master" AutoEventWireup="false" CodeFile="InwardNote.aspx.vb" Inherits="Purchase_InwardNote" EnableEventValidation="false" ValidateRequest="false" %>

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
            $('#<%=dgInward.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlExistingInwardNo.ClientID%>').select2();
        });

        var numbersOnly = /^\d+$/;
        var decimalOnly = /^\s*-?[0-9]\d*(\.\d{1,2})?\s*$/;
        var uppercaseOnly = /^[A-Z]+$/;
        var lowercaseOnly = /^[a-z]+$/;
        var stringOnly = /^[A-Za-z0-9]+$/;
        function CheckEDate(mDate, eDate) {

            if (document.getElementById(eDate).value != "") {
                var inputText = document.getElementById(eDate).value
                if (!inputText.match(/^(0[1-9]|[12][0-9]|3[01])[\- \/.](?:(0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                    alert('Enter Expire date in dd/MM/yyyy Format');
                    document.getElementById(eDate).value = "";
                    document.getElementById(eDate).focus();
                    return false;
                }
            }
            if ((document.getElementById('<%=txtOrderDate.ClientID %>').value != "") && (document.getElementById(eDate).value != "")) {
                var DV5 = DataValid(document.getElementById('<%=txtOrderDate.ClientID %>').value, document.getElementById(eDate).value);
                if (DV5 == false) {
                    alert("Expire date(" + document.getElementById(eDate).value + ") should be greater than or equal to Order date(" + document.getElementById('<%=txtOrderDate.ClientID %>').value + ").");
                    document.getElementById(eDate).value = "";
                    return false;
                }

            }

            if ((document.getElementById(mDate).value != "") && (document.getElementById(eDate).value != "")) {
                var DV5 = DataValid(document.getElementById(mDate).value, document.getElementById(eDate).value);
                if (DV5 == false) {
                    alert("Expire date(" + document.getElementById(eDate).value + ") should be greater than or equal to Manufacture Date (" + document.getElementById(mDate).value + ").");
                    document.getElementById(eDate).value = "";
                    return false;
                }

            }
        }
        function CheckMDate(mDate) {

            if (document.getElementById(mDate).value != "") {
                var inputText = document.getElementById(mDate).value
                if (!inputText.match(/^(0[1-9]|[12][0-9]|3[01])[\- \/.](?:(0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                    alert('Enter Manufacture date in dd/MM/yyyy Format');
                    document.getElementById(mDate).value = "";
                    document.getElementById(mDate).focus();
                    return false;
                }
            }
            if ((document.getElementById(mDate).value != "") && (document.getElementById('<%=txtOrderDate.ClientID %>').value != "")) {
                var DV5 = DataValid(document.getElementById(mDate).value, document.getElementById('<%=txtOrderDate.ClientID %>').value);
                if (DV5 == false) {
                    alert("Order date(" + document.getElementById('<%=txtOrderDate.ClientID %>').value + ") should be greater than or equal to Manufacture date(" + document.getElementById(mDate).value + ").");
                    document.getElementById(mDate).value = "";
                    return false;
                }
            }

            if ((document.getElementById(mDate).value != "") && (document.getElementById('<%=txtInvoiceDate.ClientID %>').value != "")) {
                var DV5 = DataValid(document.getElementById(mDate).value, document.getElementById('<%=txtInvoiceDate.ClientID %>').value);
                if (DV5 == false) {
                    alert("InvoiceDate date(" + document.getElementById('<%=txtInvoiceDate.ClientID %>').value + ") should be greater than or equal to Manufacture date(" + document.getElementById(mDate).value + ").");
                    document.getElementById(mDate).value = "";
                    return false;
                }
            }
        }



        function CalculateDiscount() {

            var Rate, Discount, vat, cst, excise, ExciseAmount, discountAmount, frieghtAmount, frieght
            frieghtAmount = 0
            frieght = 0
            var VATAmount = 0, CSTAmount = 0
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
                if (document.getElementById('<%=txtCSTAmount.ClientID %>').value == "") {
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
                frieghtAmount = (((Rate - discountAmount) + ExciseAmount) * frieght) / 100
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)

                if (document.getElementById('<%=ddlVat.ClientID %>').selectedIndex > 0) {
                    var DiscountAmount = (Rate * Discount) / 100
                    // VAT
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                    VATAmount = (((Rate - discountAmount) + ExciseAmount + frieghtAmount) * VAT) / 100
                    document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                    document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                }
                // CST
                if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                    var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                    CSTAmount = (((Rate - discountAmount) + ExciseAmount + frieghtAmount) * CST) / 100
                    document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                    document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                }

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
            if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                document.getElementById('<%=ddlCST.ClientID %>').selectedIndex = 1
            }

            if (document.getElementById('<%=txtExcise.ClientID %>').value != "") {
                Excise = document.getElementById('<%=txtExcise.ClientID %>').value
            }
            else {
                Excise = 0
            }
            if (document.getElementById('<%=ddlNUnit.ClientID %>').value != "") {
                Frieght = 0
            }
            else {
                var Frieght = 0
            }
            if (document.getElementById('<%=ddlNUnit.ClientID %>').selectedIndex > 0) {
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
                           document.getElementById('<%=txtQuantity.ClientID%>').focus()
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
                    if (document.getElementById('<%=ddlNUnit.ClientID %>').selectedIndex == 1) {
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
                            var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                            DiscountAmount = ((Rate) * Discount) / 100
                            var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                            document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
              document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)
                            //Frieght
                            FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
                  <%-- //document.getElementById('<%=txt.ClientID %>').value = FrieghtAmount.toFixed(2)
                       //document.getElementById("<%=hfrieghtAmount.ClientID %>").value = FrieghtAmount.toFixed(2)--%>
                            // VAT
                            var VATAmount = 0
                            if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                  var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                  var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                  VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                  document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
              }
                            // CST
              var CSTAmount = 0
              if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                           var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                           var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                           CSTAmount = (Rate * CST) / 100
                           CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }
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
                            var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                            document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
              document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)

                            //   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                            FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
                 <%--  // document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
                       //document.getElementById("<%=hfrieghtAmount.ClientID %>").value = FrieghtAmount.toFixed(2)--%>

                            if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                                // VAT
                                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                  var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                  var VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                  document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
              }
                            // CST
              if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                                var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                           var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                           var CST = ddlCST.options[ddlvatt.selectedIndex].innerHTML;
                           var CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }
                            // Total AMount
                       var TotalAmount = Total + ExciseAmount + FrieghtAmount + VATAmount + CSTAmount
                       document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                       document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                    <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
                        }
                }
            }
        }

        function CalculateFromCST() {

            var Freight, Excise, DiscountAmount, Discount
            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {

                Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
            }
            else {
                Discount = 0
            }
            if (document.getElementById('<%=txtExcise.ClientID %>').value != "") {
                Excise = document.getElementById('<%=txtExciseAmount.ClientID %>').value
            }
            else {
                ExciseAmount = 0
            }
            Freight = 0
            if (document.getElementById('<%=txtExcise.ClientID %>').value != "") {
                var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
            }
            else {
                var Excise = 0
            }
            if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex = 1
                         }
                         if (document.getElementById('<%=ddlNUnit.ClientID %>').selectedIndex > 0) {
                if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                       alert("Enter Rate Field")
                       document.getElementById('<%=txtRate.ClientID %>').focus()
                    return false;
                }
                else {
                    if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
                        var num
                        num = OnlyNumber(document.getElementById('<%=txtQuantity.ClientID %>').value)
                         if (num == false) {
                             alert("Enter only integers for Quantity.")
                             document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                            document.getElementById('<%=txtQuantity.ClientID%>').focus()
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
                    if (document.getElementById('<%=ddlNUnit.ClientID %>').selectedIndex == 1) {
                        var Rate = document.getElementById('<%=txtRate.ClientID %>').value
                        var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                        var Total = Rate * Quantity
                        document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
               document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
                        //'Excise DUty'
                        var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                        DiscountAmount = ((Rate) * Discount) / 100
                        var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                        document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
               document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)
                        //'Frieght
                        var FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Freight) / 100
               <%-- //   document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
              // document.getElementById("<%=hfrieghtAmount.ClientID %>").value = FrieghtAmount.toFixed(2)--%>
                        if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                            // VAT
                            var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                   var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                   var VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                   document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                            document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
               }
               if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                            // CST
                            var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                            var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                            var CSTAmount = (Rate * CST) / 100
                            var CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                            document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                   document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                        }
                        // Total AMount
                        var TotalAmount = Total + ExciseAmount + FrieghtAmount + VATAmount + CSTAmount
                        document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
               document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
                    }
                    else {
                        var Rate = document.getElementById('<%=txtRate.ClientID %>').value
                        var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                        var iPices = parseInt(document.getElementById("<%=hfTotalPieces.ClientID %>").value)
                        var Total = ((iPices * Quantity) * Rate)
                        if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {

                   var DiscountAmount = document.getElementById('<%=txtDiscountAmount.ClientID %>').value
               }
               else {
                   var DiscountAmount = 0
               }

               document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                        document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
                        //'Excise DUty'
                        var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                        DiscountAmount = ((Rate) * Discount) / 100
                        var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                        document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)
               document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                        //'Frieght
                        var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                        var FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Freight) / 100
             <%-- // document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
             //  document.getElementById("<%=hfrieghtAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
              --%>
                        if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                            // VAT
                            var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                   var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                   var VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                   document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                            document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
               }
               if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                            // CST
                            var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                            var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                            var CST = ddlCST.options[ddlvatt.selectedIndex].innerHTML;
                            var CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                            document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                   document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                        }
                        // Total AMount
                        var TotalAmount = Total + ExciseAmount + FrieghtAmount + VATAmount + CSTAmount
                        document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
               document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                    <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
                    }
                }

               }

        }

        function ValidateStringOrNot(Received, Rejected, Excess, Accept) {
            //document.getElementById(Rejected).value = ""
            if (document.getElementById(Rejected).value != "") {
                var recvd = 0
                var Exces = 0
                var Accpt = 0
                var numr
                numr = isNaN(document.getElementById(Rejected).value)
                if (numr == true) {
                    alert("Enter only integers for Rejected Quantity.")
                    document.getElementById(Rejected).value = ""
                    return true
                }
                if (document.getElementById(Received).value == "") {
                    recvd = 0
                } else {
                    recvd = document.getElementById(Received).value
                }
                if (document.getElementById(Excess).value == "") {
                    Exces = 0
                } else {
                    Exces = document.getElementById(Excess).value
                }
                if (document.getElementById(Accept).value == "") {
                    Accpt = 0
                } else {
                    Accpt = document.getElementById(Accept).value
                }

                document.getElementById(Rejected).value = (parseFloat(recvd) - (parseFloat(Exces) + parseFloat(Accpt))).toFixed(2)
                return false
            }
            else {
                var recvd = 0
                var Exces = 0
                var Accpt = 0
                if (document.getElementById(Received).value == "") {
                    recvd = 0
                } else {
                    recvd = document.getElementById(Received).value
                }
                if (document.getElementById(Excess).value == "") {
                    Exces = 0
                } else {
                    Exces = document.getElementById(Excess).value
                }
                if (document.getElementById(Accept).value == "") {
                    Accpt = 0
                }
                else {
                    Accpt = document.getElementById(Accept).value
                }


                document.getElementById(Rejected).value = (parseFloat(recvd) - (parseFloat(Exces) + parseFloat(Accpt))).toFixed(2)
            }
            return false
        }
        function CheckExcess(Excess) {
            if (document.getElementById(Excess).value != "") {
                var numr
                numr = CheckDecimal(document.getElementById(Excess).value)
                if (numr == true) {
                    alert("Enter only integers for Excess Quantity.")
                    document.getElementById(Excess).value = ""
                    return false
                }
            }
        }
        function ConfirmMessage(OrderedQuentity, ReceivedQuentity, sQuantity, SRejectedQty, sRjctdExcess, Pending) {
            //  var RecQty = CheckDecimal(document.getElementById(ReceivedQuentity).value)
            document.getElementById(sQuantity).value = ""
            document.getElementById(SRejectedQty).value = ""
            document.getElementById(sRjctdExcess).value = ""
            var pending = parseInt(document.getElementById(OrderedQuentity).innerHTML)
            var Received = parseInt(document.getElementById(ReceivedQuentity).value)
            if (pending < Received) {
                alert("Received Quantity Greater than Ordered Quantity")
                document.getElementById(sRjctdExcess).value = Received - pending
                document.getElementById(lbl).style.display = 'none'
                document.getElementById(linkButton).style.display = 'inline'
                return false;
            }
            return true;
        }

        function CalculateFromVat() {
            var Excise, Frieght, FrieghtAmount, DiscountAmount, Discount
            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
                Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
            }
            else {
                Discount = 0
            }
            if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                document.getElementById('<%=ddlCST.ClientID %>').selectedIndex = 1
            }
            if (document.getElementById('<%=txtExcise.ClientID %>').value != "") {
                Excise = document.getElementById('<%=txtExcise.ClientID %>').value
            }
            else {
                Excise = 0
            }
            var Frieght = 0
            FrieghtAmount = 0
            if (document.getElementById('<%=ddlNUnit.ClientID %>').selectedIndex > 0) {
                  if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                      alert("Enter Rate Field")
                      document.getElementById('<%=txtRate.ClientID %>').focus()
                     return false;
                 }
                 else {
                     if (document.getElementById('<%=txtAccepted.ClientID %>').value != "") {

                         var num
                         num = decimalOnly.test(document.getElementById('<%=txtAccepted.ClientID %>').value)

                             if (num == false) {
                                 alert("Enter only integers for Quantity.")
                                 document.getElementById('<%=txtAccepted.ClientID %>').value = ""
                           document.getElementById('<%=txtAccepted.ClientID%>').focus()
                           return false
                       }
                       var re = /^\s*$/;
                       if (re.test(num)) {
                           alert("Enter Quantity spaces are not allowed.");
                           document.getElementById('<%=txtAccepted.ClientID%>').value = ""
                            document.getElementById('<%=txtAccepted.ClientID%>').focus();
                            return false;
                        }
                    }
                    if ((document.getElementById('<%=txtAccepted.ClientID %>').value != "") && (document.getElementById('<%=txtQuantity.ClientID %>').value != "")) {
                         if (parseFloat(document.getElementById('<%=txtAccepted.ClientID %>').value) > parseFloat(document.getElementById('<%=txtQuantity.ClientID %>').value)) {
                                alert("Accepted Quantity is Greater than Received Qty");
                                document.getElementById(txtAccepted).value = ""
                                document.getElementById(txtAccepted).focus()
                                return false;
                            }
                            else {
                                var rqty = document.getElementById('<%=txtQuantity.ClientID %>').value
                                 var aqty = document.getElementById('<%=txtAccepted.ClientID %>').value
                                 var rjqty = parseFloat(rqty) - parseFloat(aqty)
                                 document.getElementById('<%=txtRejected.ClientID %>').value = rjqty.toFixed(2)
                       }
                   }
                   if (document.getElementById('<%=ddlNUnit.ClientID %>').selectedIndex == 1) {
                         var Rate = document.getElementById('<%=txtRate.ClientID %>').value
                       var Quantity = document.getElementById('<%=txtAccepted.ClientID %>').value
                       var Total = Rate * Quantity
                       document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                       document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
                       var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                       DiscountAmount = ((Rate) * Discount) / 100
                       document.getElementById('<%=txtDiscountAmount.ClientID %>').value = DiscountAmount.toFixed(2)
                       document.getElementById("<%=hfDiscountAmount.ClientID %>").value = DiscountAmount.toFixed(2)
                       //'Excise DUty
                       var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                       DiscountAmount = ((Rate) * Discount) / 100
                       var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                       document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                       document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)
                       //Frieght
                       FrieghtAmount = (((Rate - DiscountAmount) + ExciseAmount) * Frieght) / 100
                       var VATAmount = 0
                       if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                           var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }
                       // CST
                       var CSTAmount = 0
                       if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                           var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                           var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                           CSTAmount = (Rate * CST) / 100
                           CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }
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
                       var Quantity = document.getElementById('<%=txtAccepted.ClientID %>').value
                       var iPices = parseInt(document.getElementById("<%=hfTotalPieces.ClientID %>").value)
                       var Total = ((iPices * Quantity) * Rate)
                       document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                       document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)
                       var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                       DiscountAmount = ((Rate) * Discount) / 100
                       var ExciseAmount = ((Rate - DiscountAmount) * Excise) / 100
                       document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                       document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)

                       if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                           // VAT
                           var ddlvatt = document.getElementById('<%=ddlVat.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           var VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }
                       // CST
                       if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                           var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                           var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                           var CST = ddlCST.options[ddlvatt.selectedIndex].innerHTML;
                           var CSTAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * CST) / 100
                           document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                           document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                       }
                       // Total AMount
                       var TotalAmount = Total + ExciseAmount + FrieghtAmount + VATAmount + CSTAmount
                       document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                       document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                    <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
                   }
                 }
              }
        }
        function Amount(sOrder, sQuantity, sRecQty, SRejectedQty, sRjctdExcess) {

            if ((document.getElementById(sQuantity).value != "") && (document.getElementById(sRecQty).value != "")) {
                if (parseFloat(document.getElementById(sQuantity).value) > parseFloat(document.getElementById(sRecQty).value)) {
                    alert("Accepted Quantity is Greater than Received Qty");
                    document.getElementById(sQuantity).value = ""
                    document.getElementById(sQuantity).focus()
                    return false;
                }
            }
            if ((document.getElementById(sQuantity).value != "") && (document.getElementById(sOrder).innerHTML != "")) {

                if (parseFloat(document.getElementById(sQuantity).value) > parseFloat(document.getElementById(sOrder).innerHTML)) {
                    alert("Accepted Quantity is Greater than Ordered Qty");
                    document.getElementById(sQuantity).value = ""
                    document.getElementById(sQuantity).focus()
                    return false;
                }
            }
            if (document.getElementById(sRecQty).value == "") {
                alert("Enter Received Quantity.")
                document.getElementById(sRecQty).focus()
                return false
            }
            var ssQuantity = document.getElementById(sQuantity).value
            var sOrderQty = document.getElementById(sOrder).innerHTML
            var sRecQty1 = parseFloat(document.getElementById(sRecQty).value)
            if (sOrderQty > sRecQty1) {
                var sqty = (parseFloat(document.getElementById(sRecQty).value) - parseFloat(ssQuantity))
                var seqtz = 0
            }
            else {
                var sqty = (parseFloat(document.getElementById(sOrder).innerHTML) - parseFloat(ssQuantity))
                var seqtz = (parseFloat(document.getElementById(sRecQty).value) - parseFloat(document.getElementById(sOrder).innerHTML))
            }
            if (sqty < 0) {
                sqty = 0
            }
            if (seqtz < 0) {
                seqtz = 0
            }
            if (document.getElementById(sQuantity).value != "") {
                document.getElementById(SRejectedQty).value = sqty
            }
            else {
                document.getElementById(SRejectedQty).value = ""
            }
            document.getElementById(sRjctdExcess).value = seqtz
            return true;
        }
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Goods Inward Note</b></h2>
            </div>
            <div class="col-sm-3 col-md-3" style="left: 0px; top: 0px">
                <div class="pull-right">
                    <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <%--<asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />--%>
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="ValidateInward" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="col-sm-6 col-md-6 pull-left">
            <asp:Label ID="lblStatus" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Goods Inward No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style1" Width="240px" ID="ddlExistingInwardNo"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFEinwardNo" runat="server" ControlToValidate="ddlExistingInwardNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Inward No" ValidationGroup="ValidateInward"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Existing Order No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style2" ID="ddlOrderNo"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlOrderNo" runat="server" ControlToValidate="ddlOrderNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Order No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Order Date"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtOrderDate"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable" ID="ddlSupplier"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Goods Inward No."></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtOrderCode"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Reference No."></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtDocRefNo"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDocRefNo" runat="server" ControlToValidate="txtDocRefNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Order No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtInvoiceDate" placeholder="DD/MM/YY"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtInvoiceDate" PopupPosition="BottomLeft"
                TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Invoice Date" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <%--<asp:RangeValidator ID="rgvtxtInvoiceDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"></asp:RangeValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="E-Sugam No."></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtESugamNo"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Delivery challan No"></asp:Label>
            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtDcNo"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDcNo" runat="server" ControlToValidate="txtDcNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery Challan No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>

    <%--  <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Method of Shipping"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlModeOfShipping"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Method of Payment"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlMPayment"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Payment Terms"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlPterms"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Delivery Schedule"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlDSchedule"></asp:DropDownList>
        </div>
    </div>--%>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Invoice Reference No." Visible="false"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlExistingDocRef" Visible="false"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
        <asp:GridView ID="dgInward" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" ImageUrl="~\Images\Edit16.png" runat="server" ToolTip="Edit" />
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

                <asp:TemplateField HeaderText="Brand" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblComodity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Comodity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Descriptions") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Units" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblUnits" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Units") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MRP" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblMrp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mrp") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ordered Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderedQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Batch No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtBatchNumber" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BatchNumber") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Received Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtReceivedQty" Enabled="false" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReceivedQty") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRQty" runat="server" ControlToValidate="txtReceivedQty" Display="Dynamic" ErrorMessage="Please enter Only decimal or Intiger" SetFocusOnError="True" ValidationExpression="^\s*-?[0-9]\d*(\.\d{1,3})?\s*$"></asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Accepted Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAcceptedQty" Enabled="false" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AccpetedQty") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RVEAcceptedQty" runat="server" ControlToValidate="txtAcceptedQty" Display="Dynamic" ErrorMessage="Please enter Only decimal or Intiger" SetFocusOnError="True" ValidationExpression="^\s*-?[0-9]\d*(\.\d{1,3})?\s*$"></asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rejected Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRejected" Enabled="false" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RejectedQty") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAcceptedQty" Display="Dynamic" ErrorMessage="Please enter Only decimal or Intiger" SetFocusOnError="True" ValidationExpression="^\s*-?[0-9]\d*(\.\d{1,3})?\s*$"></asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Excess Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtExcessQty" Enabled="false" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExcessQty") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtExcessQty" Display="Dynamic" ErrorMessage="Please enter Only decimal or Intiger" SetFocusOnError="True" ValidationExpression="^\s*-?[0-9]\d*(\.\d{1,3})?\s*$"></asp:RegularExpressionValidator>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Manufacture Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtMdate" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MDate") %>'></asp:TextBox>
                        <cc1:CalendarExtender ID="ccMdate" runat="server" PopupButtonID="txtMdate" PopupPosition="BottomLeft"
                            TargetControlID="txtMdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expire Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtEdate" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EDate") %>'></asp:TextBox>
                        <cc1:CalendarExtender ID="ccEdate" runat="server" PopupButtonID="txtEdate" PopupPosition="BottomLeft"
                            TargetControlID="txtEdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pending Item" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblPending" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PendingItem") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- GI Details Modal -->
    <div class=" modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Extra Item Details</b></h4>
                </div>
                <div class="modal-body row">

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Type of Purchase"></asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlTypeOfSale"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* CST Category"></asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCstCtgry"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Brand"></asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlNBrand"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Description Of Goods"></asp:Label>
                            <asp:DropDownList runat="server" ID="ddlNItems" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlNUnit"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Rate"></asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlRate"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Rate"></asp:Label>
                            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtRate"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Received Quantity"></asp:Label>
                            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtQuantity"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Accepted Quantity"></asp:Label>
                            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtAccepted"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Rejected Quantity"></asp:Label>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtRejected"></asp:TextBox>
                            <asp:HiddenField ID="hfRejectedQty" runat="server" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Rate Amount"></asp:Label>
                            <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRateAmount"></asp:TextBox>
                            <asp:HiddenField ID="hfRateAmount" runat="server" />
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Discount"></asp:Label>
                            <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtDiscount"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Discount Amount"></asp:Label>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtDiscountAmount"></asp:TextBox>
                            <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Vat"></asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="false" CssClass="aspxcontrols" ID="ddlVat"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="VAT Amount"></asp:Label>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtVatAmount"></asp:TextBox>
                            <asp:HiddenField ID="hfVatAmount" runat="server" />
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* CST"></asp:Label>
                            <asp:DropDownList runat="server" AutoPostBack="false" CssClass="aspxcontrols" ID="ddlCst"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="CST Amount"></asp:Label>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtCSTAmount"></asp:TextBox>
                            <asp:HiddenField ID="hfCSTAmount" runat="server" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Excise Duty"></asp:Label>
                            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtExcise"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Excise Duty Amount"></asp:Label>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtExciseAmount"></asp:TextBox>
                            <asp:HiddenField ID="hfExciseAmount" runat="server" />
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Manufacture Date"></asp:Label>
                            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtMdateNew"> </asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtMdateNew" PopupPosition="BottomLeft"
                                TargetControlID="txtMdateNew" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Expire Date"></asp:Label>
                            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtEdateNew"></asp:TextBox>
                            <cc1:CalendarExtender ID="ccEdate" runat="server" PopupButtonID="txtEdateNew" PopupPosition="BottomLeft"
                                TargetControlID="txtEdateNew" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Total Amount"></asp:Label>
                            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtTotalAmount"></asp:TextBox>
                            <asp:HiddenField ID="hfTotalAmount" runat="server" />
                        </div>
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
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-9 col-md-9 pull-left">
        </div>
        <div class="col-sm-3 col-md-3">
            <%--  <div class="pull-right">
                <button type="button" data-toggle="modal" data-target="#myModal" >
                    <img class="activeIcons hvr-bounce-out" src="../Images/Add24.png" /></button>
                <asp:ImageButton ID="ImgbtnAddNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" Visible ="false" />
            </div>--%>
            <asp:TextBox ID="txtHistoryID" runat="server" Visible="False" Width="31px"></asp:TextBox>
            <asp:TextBox ID="txtMasterID" runat="server" Width="31px" Visible="False"></asp:TextBox>
            <asp:TextBox ID="txtPices" runat="server" Width="31px" Visible="False"></asp:TextBox>
            <asp:Label ID="lblDescID" runat="server" Visible="False"></asp:Label>
            <asp:HiddenField ID="hfTotalPieces" runat="server" Visible="False" />
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

    <asp:TextBox ID="txtStartDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtEndDate" runat="server" Visible="false"></asp:TextBox>
</asp:Content>

