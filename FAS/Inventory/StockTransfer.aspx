<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Inventory.master" CodeFile="StockTransfer.aspx.vb" Inherits="Purchase_Purchase_Return" EnableEventValidation="false" ValidateRequest="false" %>

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

        function ValidateDateOrderedDate(inputText) {
            if (inputText == "") {
                alert("Please enter Ordered Date")
                return false;
            }
            else if (!inputText.match(/^(0[1-9]|[12][0-9]|3[01])[\- \/.](?:(0[1-9]|1[012])[\- \/.](19|20)[0-9]{2})$/)) {
                alert('Enter Ordered date in dd/MM/yyyy Format');
                document.getElementById('<%=txtTransferDate.ClientID %>').value = "";
                document.getElementById('<%=txtTransferDate.ClientID %>').focus();
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
            document.getElementById('<%=txtTransferDate.ClientID %>').value = "";
            document.getElementById('<%=txtTransferDate.ClientID %>').focus();
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
        }

        function ValidateCommodity() {
            if (document.getElementById('<%=txtTransferDate.ClientID %>').value == "") {
                    alert("Enter Order Date")
                    document.getElementById('<%=txtTransferDate.ClientID %>').focus()
                return false;
            }

            if (document.getElementById('<%=txtTransferDate.ClientID %>').value != "") {
                    var numd
                    numd = isDate(document.getElementById('<%=txtTransferDate.ClientID %>').value)
                if (numd == false) {
                    alert("Enter Valid Date.")
                    document.getElementById('<%=txtTransferDate.ClientID %>').value = ""
                    document.getElementById('<%=txtTransferDate.ClientID%>').focus()
                    return false;
                }
            }

                <%-- if (document.getElementById('<%=ddlSupplier.ClientID %>').selectedIndex == 0) {
                alert("Select Supplier Name")
                document.getElementById('<%=ddlSupplier.ClientID %>').focus()
                return false;
            }

            if (document.getElementById('<%=txtSupplierCode.ClientID %>').value == "") {
                alert("Enter Supplier Code")
                document.getElementById('<%=txtSupplierCode.ClientID %>').focus()
                return false;
            }--%>

          <%--  if (document.getElementById('<%=ddlVatClsfctn.ClientID %>').selectedIndex == 0) {
                alert("Select Method of Shipping")
                document.getElementById('<%=ddlVatClsfctn.ClientID %>').focus()
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
        <%--   function Calculate() {

         document.getElementById('<%=txtDiscount.ClientID %>').value = ""
            document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""

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
                   var Excise = document.getElementById('<%=txtExcise.ClientID %>').value
                   var ExciseAmount = (Rate * Excise) / 100
                   document.getElementById('<%=txtExciseAmount.ClientID %>').value = ExciseAmount.toFixed(2)
                document.getElementById("<%=hfExciseAmount.ClientID %>").value = ExciseAmount.toFixed(2)
                   // VAT
                   var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                   var VAT = document.getElementById('<%=txtVat.ClientID %>').value
                   var VATAmount = (Rate * VAT) / 100
                   document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)

                   // CST
                   var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                   var CST = document.getElementById('<%=txtCST.ClientID %>').value
                   var CSTAmount = (Rate * CST) / 100
                   document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)

                   // Total AMount
                   var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount
                   document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;
               }
               else {
                   var Rate = document.getElementById('<%=txtRate.ClientID %>').value
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
                   var VAT = document.getElementById('<%=txtVat.ClientID %>').value
                   var VATAmount = (Rate * VAT) / 100
                   document.getElementById('<%=txtVatAmount.ClientID %>').value = VATAmount.toFixed(2)
                document.getElementById("<%=hfVatAmount.ClientID %>").value = VATAmount.toFixed(2)

                   // CST
                   var Rate = document.getElementById('<%=txtRateAmount.ClientID %>').value
                   var CST = document.getElementById('<%=txtCST.ClientID %>').value
                   var CSTAmount = (Rate * CST) / 100
                   document.getElementById('<%=txtCSTAmount.ClientID %>').value = CSTAmount.toFixed(2)
                document.getElementById("<%=hfCSTAmount.ClientID %>").value = CSTAmount.toFixed(2)

                   // Total AMount
                   var TotalAmount = Total + ExciseAmount + VATAmount + CSTAmount
                   document.getElementById('<%=txtTotalAmount.ClientID %>').value = (TotalAmount - document.getElementById('<%=txtDiscountAmount.ClientID %>').value).toFixed(2)
                document.getElementById("<%=hfTotalAmount.ClientID %>").value = (TotalAmount - document.getElementById("<%=hfDiscountAmount.ClientID %>").value).toFixed(2)
                <%--document.getElementById('<%= btnSave.ClientID %>').disabled = false;
               }
                }

                }

        }--%>

       <%-- function CalculateDiscount() {
            var Rate, Discount, vat, cst, excise
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
                  if (document.getElementById('<%=txtExciseAmount.ClientID %>').value == "") {
                      excise = 0
                  } else {
                      excise = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
                  }
                  if (document.getElementById("<%=hfVatAmount.ClientID %>").value == "") {
                      vat = 0
                  } else {
                      vat = parseFloat(document.getElementById("<%=hfVatAmount.ClientID %>").value)
                  }

                  var DiscountAmount = (Rate * Discount) / 100
                  var Subtotal = parseFloat(document.getElementById('<%=txtRateAmount.ClientID %>').value)
                  var total = parseFloat(cst + excise + vat + Subtotal).toFixed(2)
                  document.getElementById('<%=txtDiscountAmount.ClientID %>').value = DiscountAmount.toFixed(2)
                  document.getElementById('<%=txtTotalAmount.ClientID %>').value = (total - DiscountAmount).toFixed(2)
                  document.getElementById("<%=hfDiscountAmount.ClientID %>").value = DiscountAmount.toFixed(2)
                  document.getElementById("<%=hfTotalAmount.ClientID %>").value = (total - DiscountAmount).toFixed(2)
              }
          }
--%>

        function ValidatePurcahseOrder() {


            if (document.getElementById('<%=ddlCommodity.ClientID %>').selectedIndex == 0) {
               alert("Select Commodity")
               document.getElementById('<%=ddlCommodity.ClientID %>').focus()
                return false;
            }
            if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
               if (Trim(document.getElementById('<%=txtRate.ClientID %>').value) = "") {
                    alert("Enter Rate")
                    document.getElementById('<%=txtRate.ClientID %>').focus()
                     return false;
                 }
             }


             if (document.getElementById('<%=ddlUnit.ClientID %>').selectedIndex == 0) {
               alert("Select Unit of Measurement")
               document.getElementById('<%=ddlUnit.ClientID %>').focus()
                return false;
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
                if (document.getElementById('<%=txtFormNumber.ClientID %>').value != "") {
               var numrate
               numrate = CheckDecimal(document.getElementById('<%=txtFormNumber.ClientID %>').value)
                if (numrate == false) {
                    alert("Alphabets are not allowed for Rate.")
                    document.getElementById('<%=txtFormNumber.ClientID %>').value = ""
                         return false
                     }
                 }
                 if (document.getElementById('<%=txtNaretion.ClientID %>').value != "") {

               var re = /^\s*$/;
               if (re.test(document.getElementById('<%=txtNaretion.ClientID %>').value)) {
                    alert("Enter Address spaces are not allowed.");
                    document.getElementById('<%=txtNaretion.ClientID%>').focus();
                return false;
            }
        }


        return true;
    }



<%--  var ddlText, ddlValue, ddl, lblMesg;
           function CacheItems() {

                 ddlText = new Array();

                 ddlValue = new Array();

                 ddl = document.getElementById("<%=chkCategory.ClientID %>");

        for (var i = 0; i < ddl.options.length; i++) {

            ddlText[ddlText.length] = ddl.options[i].text;

            ddlValue[ddlValue.length] = ddl.options[i].value;

        }

    }--%>

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
        <%--function checkPreDetermined(myfield, restrictionType) {
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
        }--%>
        <%--function checkQnt(myfield, restrictionType) {
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
        }--%>


        function Calculate() {


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
                    }
                }
            }
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

        function ValidatePrint() {
            if (document.getElementById('<%=ddlExistingNote.ClientID %>').selectedIndex == 0) {
                alert("Select Existing Order")
                document.getElementById('<%=ddlExistingNote.ClientID %>').focus()
              return false;
          }
      }

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
                <h2><b>6 Stock Transfer</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">

                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMpayment" runat="server" ControlToValidate="txtReturnDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Method Of Payment" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                    <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" ValidationGroup="Validate" />

                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPterms" runat="server" ControlToValidate="ddlPterms" Display="Dynamic" ErrorMessage="Select Payment Terms" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%><%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCmdty" runat="server" ControlToValidate="ddlTypeOfSale" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Brand" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnPrint" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" />

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
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Purchase Return No."></asp:Label>
            <asp:DropDownList ID="ddlExistingNote" runat="server" CssClass="auto-style1" Width="240px" AutoPostBack="True">
            </asp:DropDownList>
        </div>
        <div class="col-sm-9 col-md-9 pull-right">
            <asp:Label ID="lblStatus" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Code"></asp:Label>
            <asp:TextBox CssClass="aspxcontrolsdisable" runat="server" ID="txtOrderCode" ValidateRequestMode="Disabled"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Date"></asp:Label>
            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtTransferDate" ValidateRequestMode="Disabled"></asp:TextBox>
            <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtTransferDate" PopupPosition="BottomLeft"
                TargetControlID="txtTransferDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTypeOsale" runat="server" ControlToValidate="ddlCommodity" Display="Dynamic" ErrorMessage="Select Type Of Sale" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Document Reference No"></asp:Label>
            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtReferenceNo" ValidateRequestMode="Disabled"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCstgry" runat="server" ControlToValidate="ddlCstCtgry" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="E_Sugam No"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtEsugamNo"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Branch Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style3" ID="ddlBname"></asp:DropDownList>
            <%--                      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="*Adress"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtAdress"></asp:TextBox>
            <%--                      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Phone No"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtPhone" Enabled="False"></asp:TextBox>
            <%--                      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Return Date"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtCPerson"></asp:TextBox>

            <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text=" Narration"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtNaretion"></asp:TextBox>
            <%--                      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Form To Receive"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style3" ID="ddlFToReceive"></asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RefRate" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Form Number"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtFormNumber"></asp:TextBox>
            <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Brand"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCommodity">
            </asp:DropDownList>
            <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <legend class="legendbold">Details of Stock Transfer</legend>
        </fieldset>
        <div class="col-sm-6 col-md-6" style="padding-left: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                    <asp:TextBox autocomplete="off" ID="txtsearch" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="FilterItems(this.value)"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <asp:ListBox ID="chkCategory" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="140px"></asp:ListBox>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Existing Quantity"></asp:Label>
                    <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtavailQty" Enabled="False"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="* Existing Rate"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtERate" Enabled="False"></asp:TextBox>
                    <%--                      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
                </div>
            </div>


            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlUnit"></asp:DropDownList>
            </div>

            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Rate"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlRate" Enabled="False"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Rate"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRate" Enabled="False"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RefRate" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
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
        </div>
        <div class="col-sm-12 col-md-12" style="padding-right: 0px">
            <asp:GridView ID="dgPurchase" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="10%" Visible="false">
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
                    <asp:TemplateField HeaderText="UnitsID" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblUnitsID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitsID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Slno" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSlno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Slno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Goods">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Units" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Units") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="RateAmount" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRateAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RateAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TotalAmount" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

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
