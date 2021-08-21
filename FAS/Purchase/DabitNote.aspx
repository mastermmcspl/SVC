<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Purchase.master" CodeFile="DabitNote.aspx.vb" Inherits="Purchase_DabitNote" EnableEventValidation="false" ValidateRequest="false" %>

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
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlExistingOrder.ClientID%>').select2();
        });


         function CheckType() {
      
             if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 0) {
                alert('Select Purchase Return reason.')
                document.getElementById('<%=ddlreturntype.ClientID%>').focus()
                return false
             }
        }
        function ValidateMasterData() {
             if (document.getElementById('<%=ddlOrderNo.ClientID %>').selectedIndex == 0) {
                alert('Select Purchase Order No.')
                document.getElementById('<%=ddlOrderNo.ClientID%>').focus()
                return false
             }
            if (document.getElementById('<%=txtReturnRefNo.ClientID %>').value == "") {
                alert('Enter Reference No.');
                document.getElementById('<%=txtReturnRefNo.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtReturnDate.ClientID %>').value == "") {
                alert('Enter Return Date.');
                document.getElementById('<%=txtReturnDate.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlSupplier.ClientID %>').selectedIndex == 0) {
                alert('Select Supplier.')
                document.getElementById('<%=ddlSupplier.ClientID%>').focus()
                return false
            }
<%--            if (document.getElementById('<%=ddlModeOfReturn.ClientID %>').selectedIndex == 0) {
                alert('Select Mode Of Return.')
                document.getElementById('<%=ddlModeOfReturn.ClientID%>').focus()
                return false
             }--%>
        }

        function GetAmount(sReturnQty, sMRP, sAmount){
            if (document.getElementById(sReturnQty).value != "") {
                var num
                num = OnlyNumber(document.getElementById(sReturnQty).value)
                if (num == false) {
                    alert("Enter only integers for Return Quantity.")
                    document.getElementById(sReturnQty).value = ""
                    document.getElementById(sAmount).innerHTML = ""
                    document.getElementById(sReturnQty).focus();
                    return false;
                }
                var ssReturnQty = document.getElementById(sReturnQty).value
                var ssMRP = parseFloat(document.getElementById(sMRP).innerHTML)
                var ssTotal = parseFloat((ssReturnQty) * (ssMRP)).toFixed(2)
                document.getElementById(sAmount).innerText = ssTotal
            }
        }
         var numbersOnly = /^\d+$/;
        var decimalOnly = /^\s*-?[0-9]\d*(\.\d{1,2})?\s*$/;
        var uppercaseOnly = /^[A-Z]+$/;
        var lowercaseOnly = /^[a-z]+$/;
        var stringOnly = /^[A-Za-z0-9]+$/;

        function ValidateDateOrderedDate(inputText) {
            if (inputText == "") {
                alert("Please enter Ordered Date")
                return false;
            }
            else if (!inputText.match(/^(0[1-9]|[12][0-9]|3[01])[\- \/.](?:(0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                alert('Enter Ordered date in dd/MM/yyyy Format');
                document.getElementById('<%=txtOrderDate.ClientID %>').value = "";
                document.getElementById('<%=txtOrderDate.ClientID %>').focus();
                return false;
            }
    }

    function ValidateDateInvoiceDate(inputText) {
        if (inputText == "") {
            alert("Please enter Ordered Date")
            return false;
        }
        else if (!inputText.match(/^(0[1-9]|[12][0-9]|3[01])[\- \/.](?:(0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
            alert('Enter Ordered date in dd/MM/yyyy Format');
            document.getElementById('<%=txtOrderDate.ClientID %>').value = "";
                document.getElementById('<%=txtOrderDate.ClientID %>').focus();
                return false;
            }

    }

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

            if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 2) {
                alert('Select Purchase Return reason.')
                document.getElementById('<%=ddlreturntype.ClientID%>').focus()
                return false
             }

    }

            function ValidateCommodity() {
                if (document.getElementById('<%=txtOrderDate.ClientID %>').value == "") {
                alert("Enter Order Date")
                document.getElementById('<%=txtOrderDate.ClientID %>').focus()
                return false;
            }
            if (document.getElementById('<%=txtOrderDate.ClientID %>').value != "") {
                var numd
                numd = isDate(document.getElementById('<%=txtOrderDate.ClientID %>').value)
                if (numd == false) {
                    alert("Enter Valid Date.")
                    document.getElementById('<%=txtOrderDate.ClientID %>').value = ""
                 document.getElementById('<%=txtOrderDate.ClientID%>').focus()
                    return false;
                }
            }
            if (document.getElementById('<%=ddlSupplier.ClientID %>').selectedIndex == 0) {
                alert("Select Supplier Name")
                document.getElementById('<%=ddlSupplier.ClientID %>').focus()
                return false;
            }
<%--            if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert("Select Method of Shipping")
                document.getElementById('<%=ddlModeOfShipping.ClientID %>').focus()
                return false;
            }
                 if (document.getElementById('<%=ddlDSchedule.ClientID %>').selectedIndex == 0) {
                alert("Select Delivery Schdule")
                document.getElementById('<%=ddlDSchedule.ClientID %>').focus()
                return false;
                 }
                         if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert("Select Mode of Shipping")
                document.getElementById('<%=ddlModeOfShipping.ClientID %>').focus()
                return false;
                         }
                                if (document.getElementById('<%=ddlPterms.ClientID %>').selectedIndex == 0) {
                alert("Select Payement Terms")
                document.getElementById('<%=ddlPterms.ClientID %>').focus()
                return false;
                                }

               if (document.getElementById('<%=ddlMPayment.ClientID %>').selectedIndex == 0) {
                alert("Select Method of Payement")
                document.getElementById('<%=ddlMPayment.ClientID %>').focus()
                return false;
            }--%>
        }
        function ValidateddlUnit() {
           
            if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 0) {
                alert("Select Unit Of Mesurement")
                document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                return false;
            }
            if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                alert("Enter Rate Field")
                document.getElementById('<%=txtRate.ClientID %>').focus()
                return false;
            }
        }

    

        function CalculateFromVat() {
 
            var Excise, Frieght, FrieghtAmount, DiscountAmount, Discount
            if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
               Discount = document.getElementById('<%=txtDiscount.ClientID %>').value
              }
              else {
                  Discount= 0
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
               
                   if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 3) {
                 
                        var ARateVal = document.getElementById('<%=txtRate.ClientID %>').value
                        var PRateVal = document.getElementById('<%=txtERate.ClientID %>').value
                        var Rate = PRateVal - ARateVal
                         document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)
                    }else if(document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 2)
                    {
                 
                            var ARateVal = document.getElementById('<%=txtRate.ClientID %>').value
                        var PRateVal = document.getElementById('<%=txtERate.ClientID %>').value
                        var Rate = ARateVal - PRateVal
                           document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)
                        
                    } else 
                    {
                        var Rate = document.getElementById('<%=txtRate.ClientID %>').value
                       
                       <%-- document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)--%>
                    }
                   if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 1) {
                       <%-- var Rate = document.getElementById('<%=txtRate.ClientID %>').value--%>
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
                    document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
                       document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
                       // VAT
                     var VATAmount=0
                       if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                           var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                           var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                           VATAmount = (((Rate - DiscountAmount) + ExciseAmount + FrieghtAmount) * VAT) / 100
                           document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                           document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                       }
                       // CST
                       var CSTAmount=0
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

                        
                    if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 3) {
                        var ARateVal = document.getElementById('<%=txtRate.ClientID %>').value
                        var PRateVal = document.getElementById('<%=txtERate.ClientID %>').value
                        var Rate = ARateVal -  PRateVal
                         document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)
                    }else if(document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 2)
                    {
                            var ARateVal = document.getElementById('<%=txtRate.ClientID %>').value
                        var PRateVal = document.getElementById('<%=txtERate.ClientID %>').value
                        var Rate = PRateVal - ARateVal
                           document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)
                        
                    } else 
                    {
                        Rate = document.getElementById('<%=txtRate.ClientID %>').value
                         document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)
                    }
           <%--   var Rate = document.getElementById('<%=txtRate.ClientID %>').value--%>
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
                    document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
                       document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
   
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

               if (document.getElementById('<%=txtFreight.ClientID %>').value != "") {
                   Freight = document.getElementById('<%=txtFreight.ClientID %>').value
            }
            else {
                   Freight = 0
               }
            
               if (document.getElementById('<%=txtExcise.ClientID %>').value != "") {
                   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
            }
            else {
                   var Excise = 0
               }



            if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex = 1
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
           if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 1) {
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
               document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
               document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)


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
               document.getElementById('<%=txtFreightAmount.ClientID %>').value = FrieghtAmount.toFixed(2)
               document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
              
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

                    
                    if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 3) {
                        var ARateVal = document.getElementById('<%=txtRate.ClientID %>').value
                        var PRateVal = document.getElementById('<%=txtERate.ClientID %>').value
                        var Rate = ARateVal -  PRateVal
                         document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)
                    }else if(document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 2)
                    {
                            var ARateVal = document.getElementById('<%=txtRate.ClientID %>').value
                        var PRateVal = document.getElementById('<%=txtERate.ClientID %>').value
                        var Rate = PRateVal -  ARateVal
                           document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)
                        
                    } else if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 1)
                    {
                        Rate = document.getElementById('<%=txtRate.ClientID %>').value
                         document.getElementById('<%=txtAppRate.ClientID %>').value = Rate.toFixed(2)
                    }


                if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 1) {
                        
                   var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                   var Total = Rate * Quantity
                   document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)

                   //'Excise DUty'
                   var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                   var ExciseAmount = (Rate * Excise) / 100
                   document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)
                   // VAT
                   var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                              var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                   var VATAmount = (Rate * VAT) / 100
                   document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)

                   // CST
                   var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                 var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                    var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                   var CSTAmount = (Rate * CST) / 100
                   document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                    document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                   // Total AMount
                   var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount
                   document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>
               }
               else {
                  <%-- var Rate = document.getElementById('<%=txtRate.ClientID %>').value--%>
                   var Quantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                   var iPices = parseInt(document.getElementById("<%=hfTotalPieces.ClientID %>").value)
                   var Total = ((iPices * Quantity) * Rate)
                   document.getElementById('<%=txtRateAmount.ClientID %>').value = Total.toFixed(2)
                document.getElementById("<%=hfRateAmount.ClientID %>").value = Total.toFixed(2)

                   //'Excise DUty'
                   var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                   var ExciseAmount = (Rate * Excise) / 100
                   document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)
                    // VAT

                   var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                
                      var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                   var VATAmount = (Rate * VAT) / 100
                   document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)

                   // CST
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                       var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                    var CST = ddlCST.options[ddlvatt.selectedIndex].innerHTML;
                   var CSTAmount = (Rate * CST) / 100
                   document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)

                   // Total AMount
                   var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount
                   document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;--%>

               }
                }

                }

        }

        function CalculateDiscount() {
            var Rate, Discount, vat, cst, excise, ExciseAmount, discountAmount, frieghtAmount,frieght
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
                frieghtAmount = (((Rate - discountAmount) + ExciseAmount) * frieght) / 100
                   document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)

                document.getElementById('<%=txtFreightAmount.ClientID %>').value = frieghtAmount.toFixed(2)
                document.getElementById("<%=hfFreightAmount.ClientID %>").value = frieghtAmount.toFixed(2)

                if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                    var DiscountAmount = (Rate * Discount) / 100
                    // VAT
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                    var VATAmount = (((Rate - discountAmount) + ExciseAmount + frieghtAmount) * VAT) / 100
                    document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                    document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                }
                // CST
                if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                    var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                    var CSTAmount = (((Rate - discountAmount) + ExciseAmount + frieghtAmount) * CST) / 100
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



        function CalculateExcise() {

            var Rate, Discount, vat, cst, excise, exciseAmount, DiscountAmount, Frieght, FrieghtAmount
            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                  alert("Fill All the Required fields")
                  return false
              } else {
                  if (document.getElementById('<%=txtRateAmount.ClientID %>').value == "") {
                      Rate = 0

                  } else {

                      Rate = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
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

                  if (document.getElementById('<%=txtExcise.ClientID %>').value == "") {
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
                      }
                  }
           
                  if (document.getElementById('<%=txtCSTAmount.ClientID %>').value == "") {
                      cst = 0
                  } else {
                      cst = parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)
                  }
               
                  if (document.getElementById("<%=hfVatAmount.ClientID %>").value == "") {
                      vat = 0
                  } else {
                      vat = parseFloat(document.getElementById("<%=hfVatAmount.ClientID %>").value)
                  }
                  if (document.getElementById("<%=txtDiscount.ClientID %>").value == "") {
                      Discount = 0
                  } else {
                      Discount = parseFloat(document.getElementById("<%=txtDiscount.ClientID %>").value)
                  }
                DiscountAmount = (Rate * Discount) / 100
                if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {
                    var exciseAmount = ((Rate - DiscountAmount) * excise) / 100
                    var FrieghtAmount = (((Rate - DiscountAmount) + exciseAmount) * Frieght) / 100
                    // VAT
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                    var VAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;
                    var VATAmount = (((Rate - DiscountAmount) + exciseAmount + FrieghtAmount) * VAT) / 100
                    document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                    document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)
                }
                // CST
                if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) {
                    var exciseAmount = ((Rate - DiscountAmount) * excise) / 100
                    var FrieghtAmount = (((Rate - DiscountAmount) + exciseAmount) * Frieght) / 100
                    var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                    var ddlCST = document.getElementById('<%=ddlCST.ClientID %>');
                    var CST = ddlCST.options[ddlCST.selectedIndex].innerHTML;
                    var CSTAmount = (((Rate - DiscountAmount) + exciseAmount + FrieghtAmount) * CST) / 100
                    document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                    document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)
                }

                var exciseAmount = ((Rate - DiscountAmount) * excise) / 100
                var FrieghtAmount = (((Rate - DiscountAmount) + exciseAmount) * Frieght) / 100
                  var Subtotal = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
                var total = parseFloat(CSTAmount + exciseAmount +FrieghtAmount + VATAmount + Subtotal).toFixed(2)
                  document.getElementById('<%=txtExciseAmount.ClientID %>').value = exciseAmount.toFixed(2)
                  document.getElementById('<%=txtTotalAmount.ClientID %>').value = (total - DiscountAmount).toFixed(2)

                document.getElementById("<%=hfFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)

                            document.getElementById("<%=txtFreightAmount.ClientID %>").value = FrieghtAmount.toFixed(2)
                document.getElementById("<%=hfExciseAmount.ClientID %>").value = exciseAmount.toFixed(2)
                  document.getElementById("<%=hfTotalAmount.ClientID %>").value = (total - DiscountAmount).toFixed(2)
              }
          }

        function CalculateFrieght() {
    
            var Rate, Discount, vat, cst, excise, exciseAmount, FrieghtAmount, DiscountAmount, Frieght
 
            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                  alert("Fill All the Required fields")
                  return false
              } else {
                  if (document.getElementById('<%=txtRateAmount.ClientID %>').value == "") {
                      Rate = 0

                  } else {

                      Rate = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
                  }

                  if (document.getElementById('<%=txtExcise.ClientID %>').value == "") {
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
                      }
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
           
                  if (document.getElementById('<%=txtCSTAmount.ClientID %>').value == "") {
                      cst = 0
                  } else {
                      cst = parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)
                  }
               
                  if (document.getElementById("<%=hfVatAmount.ClientID %>").value == "") {
                      vat = 0
                  } else {
                      vat = parseFloat(document.getElementById("<%=hfVatAmount.ClientID %>").value)
                  }
                  if (document.getElementById("<%=txtDiscount.ClientID %>").value == "") {
                      Discount = 0
                  } else {
                      Discount = parseFloat(document.getElementById("<%=txtDiscount.ClientID %>").value)
                  }
                DiscountAmount = (Rate * Discount) / 100
                if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) {

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
                }
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
          }

          
          function ValidatePurcahseOrder() {

           <%--   if (document.getElementById('<%=txtOrderCode.ClientID %>').value == "") {
                alert("Enter Order Code")
                document.getElementById('<%=txtOrderCode.ClientID %>').focus()
                return false
            }--%>
            if (document.getElementById('<%=ddlCommodity.ClientID %>').selectedIndex == 0) {
                alert("Select Commodity")
                document.getElementById('<%=ddlCommodity.ClientID %>').focus()
                return false;
            }
            if (document.getElementById('<%=chkCategory.ClientID %>').selectedIndex == -1) {
                alert("Select Item From ListBox")
                document.getElementById('<%=chkCategory.ClientID %>').focus()
                return false;
            }
            if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                if (Trim(document.getElementById('<%=txtRate.ClientID %>').value) = "") {
                     alert("Enter Rate")
                     document.getElementById('<%=txtRate.ClientID %>').focus()
                  return false;
              }
          }

        <%--  if (document.getElementById('<%=txtRequiredDate.ClientID %>').value != "") {
                var numd
                numd = isDate(document.getElementById('<%=txtRequiredDate.ClientID %>').value)
                if (numd == false) {
                    alert("Enter Valid Required Date.")
                    document.getElementById('<%=txtRequiredDate.ClientID %>').value = ""
                 document.getElementById('<%=txtRequiredDate.ClientID%>').focus()
                    return false
                }
            }--%>
           <%-- if ((document.getElementById('<%=txtOrderDate.ClientID %>').value != "") && (document.getElementById('<%=txtRequiredDate.ClientID %>').value != "")) {
                var DV5 = DataValid(document.getElementById('<%=txtOrderDate.ClientID %>').value, document.getElementById('<%=txtRequiredDate.ClientID %>').value);
                if (DV5 == false) {
                    alert("Required date(" + document.getElementById('<%=txtRequiredDate.ClientID %>').value + ") should be greater than or equal to Ordered date(" + document.getElementById('<%=txtOrderDate.ClientID %>').value + ").");
                    document.getElementById('<%=txtRequiredDate.ClientID %>').value = "";
                    return false;
                }
            }--%>

            if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 0) {
                alert("Select Unit of Measurement")
                document.getElementById('<%=ddlUnit.ClientID %>').focus()
                return false;
            }
                if (document.getElementById('<%=ddlTypeOfSale.ClientID %>').selectedIndex == 0) {
                alert("Select Purchase Type")
                document.getElementById('<%=ddlUnit.ClientID %>').focus()
                return false;
                }

              if (document.getElementById('<%=ddlTypeOfSale.ClientID %>').selectedIndex == 2)
              {
                  if (document.getElementById('<%=ddlCstCtgry.ClientID %>').selectedIndex == 0) {
                      alert("Select Cst Type")
                      document.getElementById('<%=ddlUnit.ClientID %>').focus()
                      return false;
                  }
              }

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Enter Quantity")
                document.getElementById('<%=txtQuantity.ClientID %>').focus()
               return false
           }

           var numr
           numr = isNaN(document.getElementById('<%=txtQuantity.ClientID %>').value)
                if (numr == true) {
                    alert("Enter only integers for Quantity.")
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    return false
                }
                if (document.getElementById('<%=txtDiscount.ClientID %>').value != "") {
                var numrate
                numrate = CheckDecimal(document.getElementById('<%=txtDiscount.ClientID %>').value)
                     if (numrate == false) {
                         alert("Alphabets are not allowed for Rate.")
                         document.getElementById('<%=txtDiscount.ClientID %>').value = ""
                         return false
                     }
                }

                        if (document.getElementById('<%=ddlSupplier.ClientID %>').selectedIndex == 0) {
                alert("Select Supplier Name")
                document.getElementById('<%=ddlSupplier.ClientID %>').focus()
                return false;
            }
<%--                if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert("Select Method of Shipping")
                document.getElementById('<%=ddlModeOfShipping.ClientID %>').focus()
                return false;
            }
     
                 if (document.getElementById('<%=ddlDSchedule.ClientID %>').selectedIndex == 0) {
                alert("Select Delivery Schdule")
                document.getElementById('<%=ddlDSchedule.ClientID %>').focus()
                return false;
                 }
                         if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert("Select Mode of Shipping")
                document.getElementById('<%=ddlModeOfShipping.ClientID %>').focus()
                return false;
                         }
                                if (document.getElementById('<%=ddlPterms.ClientID %>').selectedIndex == 0) {
                alert("Select Payement Terms")
                document.getElementById('<%=ddlPterms.ClientID %>').focus()
                return false;
                                }

               if (document.getElementById('<%=ddlMPayment.ClientID %>').selectedIndex == 0) {
                alert("Select Method of Payement")
                document.getElementById('<%=ddlMPayment.ClientID %>').focus()
                return false;
            }--%>

                 return true;
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

            if ( ddlText[i].toLowerCase().indexOf(value.toLowerCase()) != -1) {

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
    function checkPreDetermined(myfield, restrictionType) {
        if (myfield !== '') {
            if (restrictionType.test(myfield)) {
            }
            else {
                alert('Your Discount input data is invalid!');
                document.getElementById('<%=txtDiscount.ClientID %>').value = "";
                    document.getElementById('<%=txtDiscount.ClientID %>').focus();
                }
            }

            return;
        }
        function checkQnt(myfield, restrictionType) {
            if (myfield !== '') {
                if (restrictionType.test(myfield)) {
                }
                else {
                    alert('Your Quantity input data is invalid!');
                    document.getElementById('<%=txtQuantity.ClientID %>').value = "";
                    document.getElementById('<%=txtQuantity.ClientID %>').focus();
                }
            }

            return;
        }



        function checkRate(myfield, restrictionType) {
            if (myfield !== '') {
                if (restrictionType.test(myfield)) {
                }
                else {
                    alert('Your Rate input data is invalid!');
                    document.getElementById('<%=txtRate.ClientID %>').value = "";
                    document.getElementById('<%=txtRate.ClientID %>').focus();
                }
            }

            return;
        }

<%--        function ValidatePrint() {
            if (document.getElementById('<%=ddlExistingOrder.ClientID %>').selectedIndex == 0) {
              alert("Select Existing Order")
              document.getElementById('<%=ddlExistingOrder.ClientID %>').focus()
               return false;
           }
       }--%>

       function ValidateCancel() {
           if (confirm("Are You Sure, You Want To Cancel this item ?.")) {
               return true
           }
           else {
               return false
           }
       }

    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Dabit Note</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                                        <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="Validate" />

                    <%--<asp:ImageButton ID="imgbtnAddNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" ValidationGroup="Validate" />--%>
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <%--<asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />--%><%-- <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />--%>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
           <div class="col-sm-6 col-md-6">
        <asp:DropDownList ID="ddlExistingOrder" runat="server" CssClass="auto-style1" Width="210px" Height="18px" AutoPostBack="True">
        </asp:DropDownList>
    </div>
     <div class="col-sm-6 col-md-6 pull-left">
            <asp:Label ID="lblStatus" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
</div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Purchase Order No."></asp:Label>
            <asp:DropDownList runat="server" autocomplete="off" CssClass="aspxcontrols" ID="ddlOrderNo" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order Date"></asp:Label>
            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtOrderDate" ValidateRequestMode="Disabled"></asp:TextBox>
            <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtOrderDate" PopupPosition="BottomLeft"
                TargetControlID="txtOrderDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="ReFVOdate" runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlSupplier"></asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplier" runat="server" ControlToValidate="ddlSupplier" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier Code"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtSupplierCode"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* InvoiceNo"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style2" ID="ddlInvoiceNo"></asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDschdule" runat="server" ControlToValidate="ddlInvoiceNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Delivery Schdule" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
                        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtInvoiceDate"></asp:TextBox>
            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNofDays" runat="server" ControlToValidate="ddlNumberOfDays" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Number Of days" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Purchase Return No"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="purchaseReturnNo"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMshipping" runat="server" ControlToValidate="purchaseReturnNo" Display="Dynamic" ErrorMessage="Select Mode of Shipping" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Return Date"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtReturnDate"></asp:TextBox>
              <cc1:CalendarExtender ID="clReturnDate" runat="server" PopupButtonID="txtReturnDate" PopupPosition="BottomLeft"
                TargetControlID="txtReturnDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMpayment" runat="server" ControlToValidate="txtReturnDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Method Of Payment" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Return Reference No"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtReturnRefNo"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPterms" runat="server" ControlToValidate="ddlPterms" Display="Dynamic" ErrorMessage="Select Payment Terms" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Type of Purchase"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style2" ID="ddlTypeOfSale"></asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCmdty" runat="server" ControlToValidate="ddlTypeOfSale" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Brand" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Cst Category"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCstCtgry">
            </asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTypeOsale" runat="server" ControlToValidate="ddlCommodity" Display="Dynamic" ErrorMessage="Select Type Of Sale" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Brand"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCommodity">
            </asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCstgry" runat="server" ControlToValidate="ddlCstCtgry" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
    </div>
    
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <legend class="legendbold">Details of Purchase Return</legend>
        </fieldset>
        <div class="col-sm-6 col-md-6" style="padding-left: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                    <asp:TextBox autocomplete="off" ID="txtsearch" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="return SearchList();" ></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="pre-scrollableborder">
                    <asp:ListBox ID="chkCategory" runat="server" AutoPostBack="True" Height="439px" Width="391px"></asp:ListBox>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Existing Qty"></asp:Label>
                    <asp:textbox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtQty"></asp:textbox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="* Existing Rate"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtERate"></asp:TextBox>
                    <%--                      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
                </div>
            </div>

              <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Type of Purchase"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlreturntype"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="* Remarks"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtNarration"></asp:TextBox>
                    <%--                      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlUnit"></asp:DropDownList>
            </div>

            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Applied Rate"></asp:Label>
                    <asp:textbox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtRate"></asp:textbox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                      <asp:Label runat="server" Text="Rate"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtAppRate"></asp:TextBox>
                      <%--                      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RefRate" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtQuantity" ValidationGroup="ValidateQty"></asp:TextBox>
                    <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                                        
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtRateAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfRateAmount" runat="server" />
                </div>
            </div>

                     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Frieght"></asp:Label>
                    <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtFreight"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtFreightAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfFreightAmount" runat="server" />
                </div>
            </div> 
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Discount"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtDiscount" ValidationGroup="ValidateDiscount"></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="ReDiscount" runat="server" ControlToValidate="txtDiscount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDiscount"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtDiscountAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfDiscountAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Excise Duty"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtExcise"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
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
                    <br />
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
                    <br />
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtCSTAmount"></asp:TextBox>
                    <asp:HiddenField ID="hfCSTAmount" runat="server" />
                </div>
            </div>

   
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="Total Amount"></asp:Label>
                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtTotalAmount"></asp:TextBox>
                <asp:HiddenField ID="hfTotalAmount" runat="server" />
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding-right: 0px">
            <asp:GridView ID="dgPurchaseReturn" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="07%" Visible="false"></asp:BoundField>
                     <asp:TemplateField HeaderText="CommodityID" HeaderStyle-Width="10%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="DescriptionID" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblDescriptionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescriptionID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HistoryID" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:BoundField DataField="UnitsID" HeaderText="UnitsID" HeaderStyle-Width="15%" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="Slno" HeaderText="Slno" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:TemplateField HeaderText="Goods">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Units" HeaderText="Units" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="Rate" HeaderText="Rate" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="RateAmount" HeaderText="RateAmount" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="Discount" HeaderText="Discount" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="DiscountAmt" HeaderText="DiscountAmt" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="ExciseDuty" HeaderText="ExciseDuty" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="ExciseAmt" HeaderText="ExciseAmt" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="VAT" HeaderText="VAT" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="VATAmt" HeaderText="VATAmt" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="CST" HeaderText="CST" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="CSTAmount" HeaderText="CSTAmount" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" Visible="true" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" HeaderStyle-Width="15%" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="Edit" runat="server" CssClass="hvr-bounce-in" HeaderStyle-Width="15%" />
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
    lblScode
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
</asp:Content>
