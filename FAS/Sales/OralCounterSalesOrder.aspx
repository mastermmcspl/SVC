<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Sales.master" CodeFile="OralCounterSalesOrder.aspx.vb" Inherits="Sales_OralCounterSalesOrder" ValidateRequest="false" %>

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
            $('#<%=dgExistingProFormaSalesOrder.ClientID%>').DataTable({
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
            $('#<%=ddlParty.ClientID%>').select2();
            $('#<%=ddlModeOfCommunication.ClientID%>').select2();
            $('#<%=ddlModeOfShipping.ClientID%>').select2();
            $('#<%=ddlPaymentType.ClientID%>').select2();
            $('#<%=ddlShippingCharges.ClientID%>').select2();
            $('#<%=ddlCommodity.ClientID%>').select2();
            $('#<%=ddlSalesMan.ClientID%>').select2();
        });

    </script>
    <script type="text/javascript" language="javascript" src="../JavaScripts/General.js"></script>
    <script type="text/javascript" language="javascript">
        function ValidateParty() {
            if (document.getElementById('<%=txtOrderDate.ClientID %>').value == "") {
                alert('Enter Date Of Order.');
                document.getElementById('<%=txtOrderDate.ClientID%>').focus();
                return false;
            }
            <%--if (document.getElementById('<%=txtOrderDate.ClientID %>').value != "") {
               var numd
               numd = isDate(document.getElementById('<%=txtOrderDate.ClientID %>').value)
               if (numd == false) {
               alert("Enter Valid Order Date.")
               document.getElementById('<%=txtOrderDate.ClientID %>').value = ""
               document.getElementById('<%=txtOrderDate.ClientID%>').focus()
               return false
              }
            }--%>
            if (document.getElementById('<%=ddlParty.ClientID %>').selectedIndex == 0) {
                alert('Select Party.')
                document.getElementById('<%=ddlParty.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert('Select Method Of shipping.')
                document.getElementById('<%=ddlModeOfShipping.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlPaymentType.ClientID %>').selectedIndex == 0) {
                alert('Select Payment Type.')
                document.getElementById('<%=ddlPaymentType.ClientID%>').focus()
                return false
            }
            else {
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

        }


        function ValidateApprove() {
            if (document.getElementById('<%=txtDispatchDate.ClientID %>').value == "") {
                alert('Select Invoice Date.');
                document.getElementById('<%=txtDispatchDate.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=txtDispatchDate.ClientID %>').value != "") {
               var numd
               numd = isDate(document.getElementById('<%=txtDispatchDate.ClientID %>').value)
               if (numd == false) {
               alert("Enter Valid Invoice Date.")
               document.getElementById('<%=txtDispatchDate.ClientID %>').value = ""
               document.getElementById('<%=txtDispatchDate.ClientID%>').focus()
               return false
              }
            }

        }

        function ValidateForm() {

            <%--if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert('Select Method Of shipping.')
                document.getElementById('<%=ddlModeOfShipping.ClientID%>').focus()
                return false
            }--%>
            if (document.getElementById('<%=txtOrderDate.ClientID %>').value == "") {
                alert('Enter Date Of Order.');
                document.getElementById('<%=txtOrderDate.ClientID%>').focus();
                return false;
            }
            <%--if (document.getElementById('<%=txtOrderDate.ClientID %>').value != "") {
               var numd
               numd = isDate(document.getElementById('<%=txtOrderDate.ClientID %>').value)
               if (numd == false) {
               alert("Enter Valid Order Date.")
               document.getElementById('<%=txtOrderDate.ClientID %>').value = ""
               document.getElementById('<%=txtOrderDate.ClientID%>').focus()
               return false
              }
            }--%>
            if (document.getElementById('<%=txtShippingDate.ClientID %>').value != "") {
                var numd
                numd = isDate(document.getElementById('<%=txtShippingDate.ClientID %>').value)
                if (numd == false) {
                    alert("Enter Valid Shipping Date.")
                    document.getElementById('<%=txtShippingDate.ClientID %>').value = ""
                    document.getElementById('<%=txtShippingDate.ClientID%>').focus()
                    return false
                }
            }
            if (document.getElementById('<%=ddlParty.ClientID %>').selectedIndex == 0) {
                alert('Select Party.')
                document.getElementById('<%=ddlParty.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtPartyNo.ClientID %>').value == "") {
                alert('Enter Party Code.')
                document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                return false
            }
            if (document.getElementById('<%=txtPartyNo.ClientID%>').value != "") {
                var namep;
                namep = document.getElementById('<%=txtPartyNo.ClientID %>').value
                if (namep.length > 200) {
                    alert('Party Code exceeded maximum size(Only 200 Characters).');
                    document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                    return false;
                }
                var re = /^\s*$/;
                if (re.test(namep)) {
                    alert("Enter Party Code spaces are not allowed.");
                    document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=txtContactNo.ClientID %>').value !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtContactNo.ClientID %>').value)
                if (num == false) {
                    alert("Enter valid Contact number.");
                    document.getElementById('<%=txtContactNo.ClientID %>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=txtInputBy.ClientID%>').value != "") {
                if (!alpha(document.getElementById('<%=txtInputBy.ClientID %>').value)) {
                    alert("Enter Valid Name For Inputed By.")
                    document.getElementById('<%=txtInputBy.ClientID %>').value = ""
                    document.getElementById('<%=txtInputBy.ClientID %>').focus()
                    return false
                }
                var namen;
                namen = document.getElementById('<%=txtInputBy.ClientID %>').value
                if (namen.length > 1000) {
                    alert('Inputed By exceeded maximum size(Only 200 Characters).');
                    document.getElementById('<%=txtInputBy.ClientID%>').focus();
                    return false;
                }
                var re = /^\s*$/;
                if (re.test(namen)) {
                    alert("Enter Inputed By spaces are not allowed.");
                    document.getElementById('<%=txtInputBy.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert('Select Method Of shipping.')
                document.getElementById('<%=ddlModeOfShipping.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlPaymentType.ClientID %>').selectedIndex == 0) {
                alert('Select Payment Type.')
                document.getElementById('<%=ddlPaymentType.ClientID%>').focus()
                return false
            }
            else {
                var ddlPT = document.getElementById("<%=ddlPaymentType.ClientID %>");
                var ddlPaymentText = ddlPT.options[ddlPT.selectedIndex].innerHTML;
                if (ddlPaymentText == "Cheque") {
                    if (document.getElementById('<%=txtChequeNo.ClientID %>').value == "") {
                        alert('Enter Cheque No.');
                        document.getElementById('<%=txtChequeNo.ClientID%>').focus()
                    return false;
                }
                if (document.getElementById('<%=txtChequeNo.ClientID%>').value != "") {
                        var name;
                        name = document.getElementById('<%=txtChequeNo.ClientID %>').value
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
            <%--if (document.getElementById('<%=txtBranch.ClientID %>').value == "") {
                        alert('Enter Branch.');
                        document.getElementById('<%=txtBranch.ClientID%>').focus()
                    return false;
                }--%>
                if (document.getElementById('<%=txtBranch.ClientID%>').value != "") {
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
        if (document.getElementById('<%=ddlCommodity.ClientID %>').selectedIndex == 0) {
                alert('Select Commodity.');
                document.getElementById('<%=ddlCommodity.ClientID%>').focus()
                return false;
            }
            var list = document.getElementById("<%=lstBoxDescription.ClientID %>");
            var selected_items = 0;
            for (var i = 0; i < list.length; i++) {
                if (list[i].selected) {
                    selected_items++;
                }
            }
            if (!(selected_items > 0)) {
                alert("Select at least one item in Item Description.");
                return false;
            }
            if ((selected_items > 1)) {
                alert("Select one item Description at a time.");
                return false;
            }

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert('Enter Quantity.');
                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                return false;
            }

        }

    function Validate() {
        if (confirm("Are You Sure, You Want To Cancel this item ?.")) {
            return true
        }
        else {
            return false
        }
    }

    function mouseOver() {
        if (document.getElementById("<%=txtSearch.ClientID%>").value == "Search by Party Code & Order No") {
                document.getElementById("<%=txtSearch.ClientID%>").value = ""
            }
        }

        function mouseOut() {
            if (document.getElementById("<%=txtSearch.ClientID%>").value == "") {
                document.getElementById("<%=txtSearch.ClientID%>").value = "Search by Party Code & Order No"
            }
            else if (document.getElementById("<%=txtSearch.ClientID%>").value == "Search by Party Code & Order No") {
                document.getElementById("<%=txtSearch.ClientID%>").value = "Search by Party Code & Order No"
             }
     }



     var ddlText, ddlValue, ddl, lblMesg;

     function CacheItems() {

         ddlText = new Array();

         ddlValue = new Array();

         ddl = document.getElementById("<%=lstBoxDescription.ClientID %>");

         for (var i = 0; i < ddl.options.length; i++) {

             ddlText[ddlText.length] = ddl.options[i].text;

             ddlValue[ddlValue.length] = ddl.options[i].value;

         }

     }

     window.onload = CacheItems;

     function FilterItems(value) {

         ddl.options.length = 0;
         var str = value.toLowerCase()
         for (var i = 0; i < ddlText.length; i++) {

             if (ddlText[i].toLowerCase().indexOf(str) != -1) {

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

        function RateAmount() {
            if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                document.getElementById('<%=txtAmount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=txtNetAmount.ClientID %>').value = ""

                document.getElementById('<%=hfAmount.ClientID %>').value = ""
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=hfNetAmount.ClientID %>').value = ""

                var num
                num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
                if (num == false) {
                    alert("Enter integers/Decimals for Quantity.")
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false
                }
                var re = /^\s*$/;
                if (re.test(num)) {
                    alert("Enter Quantity spaces are not allowed.");
                    document.getElementById('<%=txtQuantity.ClientID%>').focus();
                    return false;
                }

                <%--if ((document.getElementById('<%=hfAvailableQty.ClientID %>').value) < 1) {
                    alert("Out of Stock")
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    return false;
                }--%>


                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

                document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                var sTotal = parseFloat(sQuantity * sMRP).toFixed(2);
                
                var sGST = document.getElementById('<%=txtGSTRate.ClientID %>').value
                document.getElementById('<%=hfGSTAmount.ClientID %>').value = parseFloat((sTotal * sGST) / 100).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((sTotal * sGST) / 100).toFixed(2);
                var sGSTAMT = parseFloat((sTotal * sGST) / 100).toFixed(2);

                document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(parseFloat(sTotal) + parseFloat(sGSTAMT)).toFixed(2);
                document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(parseFloat(sTotal) + parseFloat(sGSTAMT)).toFixed(2);

                if (document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sTotal) * parseFloat(sDISCOUNT)) / 100).toFixed(2)

                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = parseFloat(ssDiscount).toFixed(2);
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = parseFloat(ssDiscount).toFixed(2);

                var sGST = document.getElementById('<%=txtGSTRate.ClientID %>').value
                document.getElementById('<%=hfGSTAmount.ClientID %>').value = parseFloat(((sTotal - ssDiscount) * sGST) / 100).toFixed(2);
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat(((sTotal - ssDiscount) * sGST) / 100).toFixed(2);
                var sGSTAMT = parseFloat(((sTotal - ssDiscount) * sGST) / 100).toFixed(2);

                document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sGSTAMT)).toFixed(2);
                document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sGSTAMT)).toFixed(2);
                }

            }
            else {
                document.getElementById('<%=txtAmount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = ""
                document.getElementById('<%=txtNetAmount.ClientID %>').value = ""


                document.getElementById('<%=hfAmount.ClientID %>').value = ""
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=hfGSTAmount.ClientID %>').value = ""
                document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
            }

        }

        

       function DiscountAmount() {
            if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                document.getElementById('<%=txtAmount.ClientID %>').value = ""
                document.getElementById('<%=txtNetAmount.ClientID %>').value = ""

                document.getElementById('<%=hfAmount.ClientID %>').value = ""
                document.getElementById('<%=hfNetAmount.ClientID %>').value = ""

                var num
                num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
                if (num == false) {
                    alert("Enter integers/Decimals for Quantity.")
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false
                }
                var re = /^\s*$/;
                if (re.test(num)) {
                    alert("Enter Quantity spaces are not allowed.");
                    document.getElementById('<%=txtQuantity.ClientID%>').focus();
                    return false;
                }

                <%--if ((document.getElementById('<%=hfAvailableQty.ClientID %>').value) < 1) {
                    alert("Out of Stock")
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    return false;
                }--%>


                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

                document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                if (document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {
                var sTotal = parseFloat(sQuantity * sMRP).toFixed(2);

                var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sTotal) * parseFloat(sDISCOUNT)) / 100).toFixed(2)

                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = parseFloat(ssDiscount).toFixed(2);
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = parseFloat(ssDiscount).toFixed(2);

                document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(parseFloat(sTotal) - parseFloat(ssDiscount)).toFixed(2);
                document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(parseFloat(sTotal) - parseFloat(ssDiscount)).toFixed(2);
                }
                else {

                    document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
                    document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""

                    document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                    document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                }
                

                } 
                else {
                    document.getElementById('<%=txtAmount.ClientID %>').value = ""
                    document.getElementById('<%=txtNetAmount.ClientID %>').value = ""

                    document.getElementById('<%=hfAmount.ClientID %>').value = ""
                    document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
                }
            
            }
        
     

       function CheckOrderDate() {
           if ((document.getElementById('<%=txtOrderDate.ClientID %>').value != "") && (document.getElementById('<%=lblStartDate.ClientID %>').innerHTML != "")) {
              var DV5 = DataValid(document.getElementById('<%=txtOrderDate.ClientID %>').value, document.getElementById('<%=lblStartDate.ClientID %>').innerHTML);
                if (DV5 == false) {
                    alert("The date should be always equal to or above the application date.");
                    document.getElementById('<%=txtOrderDate.ClientID %>').value = ""
                    document.getElementById('<%=txtOrderDate.ClientID%>').focus()
                    return false;
                }
            }
        }
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Counter/Cash Sales</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh/Clear" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="ValidateCustomer" ImageUrl="~/Images/Add24.png" />                    
                    <%--<asp:ImageButton ID="imgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />--%>
                    <asp:ImageButton ID="imgbtnDelete" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" />
                    <asp:ImageButton ID="imgbtnReport" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Report" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" AutoPostBack ="true" CssClass="aspxcontrols"></asp:DropDownList>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True"
                    ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Visible="false" Text="Search by Order No."></asp:Label>
            <asp:TextBox ID="txtSearch" Visible="false" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="200px"></asp:TextBox>
            <asp:ImageButton ID="btnSearch" Visible="false" runat="server" ImageUrl="~/Images/Search16.png" CssClass="hvr-bounce-in" data-toggle="tooltip" data-placement="bottom" title="Search" CausesValidation="False" />
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Visible="false" Text="Search by Customer Name/Customer Code"></asp:Label>
            <asp:TextBox ID="txtPartySearch" Visible="false" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="200px"></asp:TextBox>
            <asp:ImageButton ID="btnSearchParty" Visible="false" runat="server" ImageUrl="~/Images/Search16.png" CssClass="hvr-bounce-in" data-toggle="tooltip" data-placement="bottom" title="Search" CausesValidation="False" />
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lbltopgap" runat="server"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Order No."></asp:Label>
            <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order No."></asp:Label>
            <asp:TextBox ID="txtOrderCode" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order Date"></asp:Label>
            <div class="form-group">
                <asp:TextBox ID="txtOrderDate" AutoPostBack="true" CssClass="aspxcontrols" runat="server" Width="100px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtOrderDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtOrderDate" TargetControlID="txtOrderDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
                <asp:Label ID="lblGap" runat="server">&gt;=</asp:Label>
                <asp:Label ID="lblStartDate" runat="server" Text="" Width="80px"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="MRFVOrderDate" runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Order Date" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="MREVOrderDate" runat="server" ControlToValidate="txtOrderDate" Display="Dynamic" ErrorMessage="Enter Valid Order Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />            
            <asp:Label runat="server" Text="Status :-"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="* Customer Name"></asp:Label>
                    <asp:DropDownList ID="ddlParty" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="210px"></asp:DropDownList>
                    <asp:ImageButton ID="imgbtnCreateCustomer" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlParty" runat="server" ControlToValidate="ddlParty" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Party" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Customer Code"></asp:Label>
                    <asp:TextBox ID="txtPartyNo" CssClass="aspxcontrolsdisable" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblContactNo" runat="server" Text="Contact No"></asp:Label>
                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Category"></asp:Label>
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="*Company Type"></asp:Label>
                    <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="*GSTN Category"></asp:Label>
                    <asp:DropDownList ID="ddlGSTCategory" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="80px"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Payment Type"></asp:Label>
            <asp:DropDownList ID="ddlPaymentType" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPaymentType" runat="server" ControlToValidate="ddlPaymentType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Payment Type" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Mode of Communication"></asp:Label>
            <asp:DropDownList ID="ddlModeOfCommunication" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipping Chargers"></asp:Label>
            <asp:DropDownList ID="ddlShippingCharges" runat="server" CssClass="aspxcontrols">
                <asp:ListItem Value="0">Select Shipping Charges</asp:ListItem>
                <asp:ListItem Value="1">Payable on deleivery</asp:ListItem>
                <asp:ListItem Value="2">Paid recoverable</asp:ListItem>
                <asp:ListItem Value="3">Not recoverable</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Sales Man"></asp:Label>
            <asp:DropDownList ID="ddlSalesMan" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm3 col-md-3">
            <asp:Label runat="server" Text="Method of Shipping"></asp:Label>
            <asp:DropDownList ID="ddlModeOfShipping" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVModeOfShipping" runat="server" ControlToValidate="ddlModeOfShipping" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Method Of Shipping" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipping Date"></asp:Label>
            <asp:TextBox ID="txtShippingDate" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtShippingDate_CalendarExtender" runat="server" PopupButtonID="txtShippingDate" CssClass="cal_Theme1"
                TargetControlID="txtShippingDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
            </cc1:CalendarExtender>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVShippingDate" runat="server" ControlToValidate="txtShippingDate" Display="Dynamic" ErrorMessage="Enter Valid Shipping Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Commodity"></asp:Label>
            <asp:DropDownList ID="ddlCommodity" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCommodity" runat="server" ControlToValidate="ddlCommodity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Commodity" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Buyer Reference Reference No"></asp:Label>
            <asp:TextBox ID="txtBuyerPurOrderNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Buyer Reference Date"></asp:Label>
            <asp:TextBox ID="txtBuyerOrderDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtBuyerOrderDate_CalendarExtender" runat="server" PopupButtonID="txtBuyerOrderDate"
                TargetControlID="txtBuyerOrderDate" Format="dd/MM/yyyy" PopupPosition="TopLeft">
            </cc1:CalendarExtender>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBuyerOrderDate" runat="server" ControlToValidate="txtBuyerOrderDate" Display="Dynamic" ErrorMessage="Enter Valid Buyer Reference Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Inputed By"></asp:Label>
            <asp:TextBox ID="txtInputBy" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVInputBy" runat="server" ControlToValidate="txtInputBy" Display="Dynamic" ErrorMessage="Enter Valid Inputed By." SetFocusOnError="True" ValidationExpression="^[a-zA-Z\s]{1,100}$"></asp:RegularExpressionValidator>
        </div>
    </div>

     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
         <div class="col-sm-3 col-md-3">
             <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack ="true" Enabled ="false" Visible ="false" CssClass="aspxcontrols"></asp:DropDownList>   
         </div>
         <div class="col-sm-3 col-md-3">
             <asp:CheckBox ID ="chkboxFrom" runat ="server" AutoPostBack ="true"  Text ="Different Address" />
         </div>
         <div class="col-sm-3 col-md-3">
              
         </div>
         <div class="col-sm-3 col-md-3">
             <asp:CheckBox ID ="chkboxTo" runat ="server" AutoPostBack ="true" Text ="Different Address" />
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
            <asp:TextBox ID="txtDeliveryFromAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeleveryFromAddress" runat="server" ControlToValidate="txtDeliveryFromAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery From Address" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Bill To Address"></asp:Label>
            <asp:TextBox ID="txtBillingAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillingAddress" runat="server" ControlToValidate="txtBillingAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Billing Address" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Shipping To Address"></asp:Label>
            <asp:TextBox ID="txtDeleveryAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeleveryAddress" runat="server" ControlToValidate="txtDeleveryAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Delivery Address" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Company GSTN Reg.No"></asp:Label>
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

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:Label runat="server" Text="Remarks"></asp:Label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
        </div>
    </div>

    <div class="col-md-12">
        <div id="divcollapseChequeDetails" runat="server" visible="false" data-toggle="collapse" data-target="#collapseChequeDetails"><a href="#"><b><i>Click here to view Cheque Details...</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseChequeDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No"></asp:Label>
                    <asp:TextBox ID="txtChequeNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblChequeDate" runat="server" Text="Cheque Date"></asp:Label>
                    <asp:TextBox ID="txtChequeDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtChequeDate" TargetControlID="txtChequeDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblIFSCCode" runat="server" Text="IFSC Code"></asp:Label>
                    <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="aspxcontrols"></asp:TextBox>
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
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div id="divcollapseDispatchDetails" runat="server" visible="false" data-toggle="collapse" data-target="#collapseDispatchDetails"><a href="#"><b><i>Click here to view Dispatch Details...</i></b></a></div>
    </div>

    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseDispatchDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblDispatchDate" runat="server" Text="Invoice Date"></asp:Label>
                    <asp:TextBox ID="txtDispatchDate" AutoPostBack ="true" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtDispatchDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDispatchDate" TargetControlID="txtDispatchDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblDispatchRefNo" runat="server" Text="Invoice No"></asp:Label>
                    <asp:TextBox ID="txtDispatchRefNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblESugamNo" runat="server" Text="E-Sugam No"></asp:Label>
                    <asp:TextBox ID="txtESugamNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>

    <%--<div class="col-md-12">
        <div id="divcollapseCharges" runat="server" visible="false" data-toggle="collapse" data-target="#collapseCharges"><a href="#"><b><i>Click here to Add Charges...</i></b></a></div>
    </div>--%>
    <%--<div class="col-md-12" style="padding-left: 0px">
        <div id="collapseCharges" class="collapse">--%>
                        
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm- col-md-3">
                            <asp:Label ID="lblChargeType" runat="server" Text="* ChargeType"></asp:Label>
                            <asp:DropDownList ID="ddlChargeType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlChargeType" runat="server" ControlToValidate="ddlChargeType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Charge Type" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblShippingRate" runat="server" Text="* Amount"></asp:Label>
                            <asp:TextBox ID="txtShippingRate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Amount" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtShippingRate" runat="server" ControlToValidate="txtShippingRate" Display="Dynamic" ErrorMessage="Enter Valid Amount" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <br />
                            <asp:ImageButton ID="imgbtnAddCharge" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" ValidationGroup="ValidateAdd" />
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-16 col-md-6">
                            <asp:DataGrid ID="GvCharge" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable">
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
                                            <asp:ImageButton ID="ibtnCancel" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" ImageUrl ="~/Images/Trash16.png" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>

                            
                        </div>
                    </div>
               
       <%-- </div>
    </div>--%>

    <div class="col-sm-12 col-md-12 form-group">
        <br />
        <fieldset>
            <legend class="legendbold">Details of Sales Order</legend>
        </fieldset>
        <div class="col-sm-6 col-md-6" style="height: 200px; padding-left: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Label runat="server" Text="* Item Description"></asp:Label>
                    <asp:TextBox ID="txtSearchItem" onkeyup="FilterItems(this.value)" runat="server" CssClass="aspxcontrols" placeholder="Search By Description" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 pre-scrollableborder" style="padding: 0px">
                <asp:ListBox ID="lstBoxDescription" CssClass="aspxcontrols" AutoPostBack="true" runat="server" Enabled="False" Height="250px"
                    Font-Names="Verdana" Font-Size="Smaller"></asp:ListBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVlstBoxDescription" runat="server" ControlToValidate="lstBoxDescription" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select An Item" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList ID="ddlUnitOfMeassurement" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Quantity" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>                    
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    <asp:HiddenField ID="hfAmount" runat="server" />
                </div>
            </div>
             <div class="col-sm-12 col-md-12 form-group">
                <asp:Label ID="lblEffectDates" runat="server" Text="Effective Date From  " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblEffectiveDates" runat="server" Text="" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label ID="lblPCRate" runat="server" Text="* Rate"></asp:Label>
                    <asp:DropDownList ID="ddlRate" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox ID="txtMRP" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <asp:HiddenField ID="hfMRP" runat="server" />
                </div>
            </div>

            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Discount"></asp:Label>
                    <asp:DropDownList ID="ddlDiscount" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox ID="txtDiscountAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    <asp:HiddenField ID="hfDiscountAmount" runat="server" />
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
                <asp:TextBox ID="txtNetAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                <asp:HiddenField ID="hfNetAmount" runat="server" />
            </div>
            <div class="col-sm-12 col-md-12" style="padding-right: 0px">
                <div class="pull-right">
                    <asp:TextBox ID="txtMRPFromTable" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtOrderID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                    <asp:HiddenField ID="txtCode" runat="server" />
                    <asp:HiddenField ID="hfItemVAT" runat="server" />
                    <asp:HiddenField ID="hfAvailableQty" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="overflow: auto">
        <asp:GridView ID="dgExistingProFormaSalesOrder" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <%-- <asp:BoundField DataField="CommodityID" HeaderText="CommodityID" Visible="False" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="ItemID" HeaderText="ID" Visible="False" HeaderStyle-Width="5%"></asp:BoundField>--%>
                <asp:TemplateField HeaderText="CommodityID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HistoryID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UnitID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SlNo" HeaderText="Sl.No" Visible="False" HeaderStyle-Width="5%"></asp:BoundField>

                <asp:TemplateField HeaderText="Description Of Goods" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="30%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitOfMeassurement") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderedQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblMRP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRPAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Basic Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:Label ID="lblNetAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NetAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="4%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="16%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT" Visible ="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblVAT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VAT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT Amt" Visible ="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblVATAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VATAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CST" Visible ="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblCST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CST") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CST Amt" Visible ="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <asp:Label ID="lblCSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CSTAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Excise" Visible ="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                    <ItemTemplate>
                        <asp:Label ID="lblExcise" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Excise") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Excise Amt" Visible ="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="16%">
                    <ItemTemplate>
                        <asp:Label ID="lblExciseAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExciseAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTID" Visible ="false"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblGST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GST") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="1%">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnCancel" runat="server" CommandName="Cancel" Height="16px" ImageUrl="~/Images/Trash16.png" ToolTip="Delete/Cancel" Width="15px" />
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
                <asp:TextBox ID="txtGrandTotalAmt" Width="80%" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                <asp:ImageButton ID="ibtnInsert" runat="server" Height="16px" ImageUrl="~/Images/Add16.png" ToolTip="Save" />
            </div>
        </div>

    </asp:Panel>
    <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
           <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" Visible ="false" ID="txtGSTID"></asp:TextBox>
    </div>

    <asp:TextBox ID ="txtGLID" runat ="server" Visible ="false" ></asp:TextBox>  
    <asp:TextBox ID ="txtDetailIDTable" runat="server" Visible ="false" ></asp:TextBox>  

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
