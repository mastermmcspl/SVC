<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Sales.master" CodeFile="SalesReturn.aspx.vb" Inherits="Sales_SalesReturn" ValidateRequest="false" %>

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
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlOrderNo.ClientID%>').select2();
            $('#<%=ddlInvoiceNo.ClientID%>').select2();
            $('#<%=ddlSearch.ClientID%>').select2();
            $('#<%=ddlreturntype.ClientID%>').select2();
        });

    </script>

    <script type="text/javascript" language="javascript" src="../JavaScripts/General.js"></script>
     <script type="text/javascript" language="javascript">
         function ValidateSave() {
            if (document.getElementById('<%=txtReturnDate.ClientID %>').value == "") {
                alert('Enter Date Of Return.');
                document.getElementById('<%=txtReturnDate.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtReturnDate.ClientID %>').value != "") {
                var numd
                numd = isDate(document.getElementById('<%=txtReturnDate.ClientID %>').value)
                if (numd == false) {
                alert("Enter Valid Return Date.")
                document.getElementById('<%=txtReturnDate.ClientID %>').value = ""
                document.getElementById('<%=txtReturnDate.ClientID%>').focus()
                return false
                }
            }
            if (document.getElementById('<%=txtReturnRefNo.ClientID %>').value == "") {
                alert('Enter Return Reference No.');
                document.getElementById('<%=txtReturnRefNo.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtReturnRefNo.ClientID%>').value != "") {
                var name;
                name = document.getElementById('<%=txtReturnRefNo.ClientID %>').value
                if (name.length > 200) {
                    alert('Return Reference No exceeded maximum size(Only 200 Characters).');
                    document.getElementById('<%=txtReturnRefNo.ClientID%>').focus();
                    return false;
                }
                var re = /^\s*$/;
                if (re.test(name)) {
                    alert("Enter Return Reference No spaces are not allowed.");
                    document.getElementById('<%=txtReturnRefNo.ClientID%>').focus();
                        return false;
                    }
                }
           
            if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert('Select Mode Of Return.')
                document.getElementById('<%=ddlModeOfShipping.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 0) {
                alert('Select Reason for Return.')
                document.getElementById('<%=ddlreturntype.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=ddlOrderNo.ClientID %>').selectedIndex == 0) {
                alert('Select Order No.')
                document.getElementById('<%=ddlOrderNo.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=ddlInvoiceNo.ClientID %>').selectedIndex == 0) {
                alert('Select Invoice No.')
                document.getElementById('<%=ddlInvoiceNo.ClientID%>').focus()
                return false;
            }

        }


         function ValidateData() {
            if (document.getElementById('<%=txtReturnDate.ClientID %>').value == "") {
                alert('Enter Date Of Return.');
                document.getElementById('<%=txtReturnDate.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtReturnDate.ClientID %>').value != "") {
                var numd
                numd = isDate(document.getElementById('<%=txtReturnDate.ClientID %>').value)
                if (numd == false) {
                alert("Enter Valid Return Date.")
                document.getElementById('<%=txtReturnDate.ClientID %>').value = ""
                document.getElementById('<%=txtReturnDate.ClientID%>').focus()
                return false
                }
            }
            if (document.getElementById('<%=txtReturnRefNo.ClientID %>').value == "") {
                alert('Enter Return Reference No.');
                document.getElementById('<%=txtReturnRefNo.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtReturnRefNo.ClientID%>').value != "") {
                var name;
                name = document.getElementById('<%=txtReturnRefNo.ClientID %>').value
                if (name.length > 200) {
                    alert('Return Reference No exceeded maximum size(Only 200 Characters).');
                    document.getElementById('<%=txtReturnRefNo.ClientID%>').focus();
                    return false;
                }
                var re = /^\s*$/;
                if (re.test(name)) {
                    alert("Enter Return Reference No spaces are not allowed.");
                    document.getElementById('<%=txtReturnRefNo.ClientID%>').focus();
                        return false;
                    }
                }
           
            if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert('Select Mode Of Return.')
                document.getElementById('<%=ddlModeOfShipping.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 0) {
                alert('Select Reason for Return.')
                document.getElementById('<%=ddlreturntype.ClientID%>').focus()
                return false;
            }
         }


    function RateAmount() {
            if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                document.getElementById('<%=txtAmount.ClientID %>').value = ""
            document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
            document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
            document.getElementById('<%=txtVATAmount.ClientID %>').value = ""
            document.getElementById('<%=txtCSTAmount.ClientID %>').value = ""
            <%--document.getElementById('<%=txtExciseAmount.ClientID %>').value = ""--%>

            document.getElementById('<%=hfAmount.ClientID %>').value = ""
            document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
            document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
            document.getElementById('<%=hfVATAmount.ClientID %>').value = ""
            document.getElementById('<%=hfCSTAmount.ClientID %>').value = ""
            <%--document.getElementById('<%=hfExciseAmount.ClientID %>').value = ""--%>


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
                    

                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
            var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

            document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
            document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

            document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
            document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
            
            
             if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		
 
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 

			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);

		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);
		document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);

		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			
		}
                

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(BasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
            
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
            
                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
		

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(sVATAMOUNT)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)


			var sVATAmt = parseFloat((((ssTotalAmt - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}


                var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var sVATAmt = parseFloat((((ssTotalAmt - parseFloat(ssDiscount) + sExiceAmt)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			
                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt



		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value =  parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
		}
                

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);



			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById(sNetAmount).innerText = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

		}
                   

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var sNetTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                   var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

                   document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                   document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                   document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
               }
               if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
                   

                   var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
                   

		        if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }		
		        if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }
                   document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
               }


        }
        else {
            document.getElementById('<%=txtAmount.ClientID %>').value = ""
                <%--document.getElementById('<%=txtDiscount.ClientID %>').value = ""--%>
            document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
            document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
                <%--document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex = 0--%>
            document.getElementById('<%=txtVATAmount.ClientID %>').value = ""
                <%--document.getElementById('<%=ddlCST.ClientID %>').selectedIndex = 0--%>
            document.getElementById('<%=txtCSTAmount.ClientID %>').value = ""

            document.getElementById('<%=hfAmount.ClientID %>').value = ""
            document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
            document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
            document.getElementById('<%=hfVATAmount.ClientID %>').value = ""
            document.getElementById('<%=hfCSTAmount.ClientID %>').value = ""
        }

    }


        function CalculateFinalAmount() {
        var list = document.getElementById("<%=lstBoxDescription.ClientID %>");
            var selected_items = 0;
            for (var i = 0; i < list.length; i++) {
                if (list[i].selected) {
                    selected_items++;
                }
            }
            if (!(selected_items > 0)) {
                alert("Select at least one item.");
                return false;
            }

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Enter Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').focus()
            return false
        }
        if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                document.getElementById('<%=txtAmount.ClientID %>').value = ""
               document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
               document.getElementById('<%=txtVATAmount.ClientID %>').value = ""
               document.getElementById('<%=txtCSTAmount.ClientID %>').value = ""
               <%--document.getElementById('<%=txtExciseAmount.ClientID %>').value = ""--%>

               document.getElementById('<%=hfAmount.ClientID %>').value = ""
               document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
               document.getElementById('<%=hfVATAmount.ClientID %>').value = ""
               document.getElementById('<%=hfCSTAmount.ClientID %>').value = ""
               <%--document.getElementById('<%=hfExciseAmount.ClientID %>').value = ""--%>

               var num
               num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
            if (num == false) {
                alert("Enter integers/Decimals for Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false
                }

                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
               var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

               document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
               document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

               if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		
 
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 

			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);

		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);
		document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);

		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			
		}
                

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(BasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
		

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(sVATAMOUNT)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)


			var sVATAmt = parseFloat((((ssTotalAmt - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}


                var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var sVATAmt = parseFloat((((ssTotalAmt - parseFloat(ssDiscount) + sExiceAmt)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			
                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt



		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value =  parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
		}
                

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);



			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById(sNetAmount).innerText = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

		}
                   

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var sNetTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                   var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

                   document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                   document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                   document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
               }
               if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
                   

                   var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
                   

		        if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }		
		        if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }
                   document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
               }

           }

        }




        function CalculateFinalAmountVAT() {
        var list = document.getElementById("<%=lstBoxDescription.ClientID %>");
            var selected_items = 0;
            for (var i = 0; i < list.length; i++) {
                if (list[i].selected) {
                    selected_items++;
                }
            }
            if (!(selected_items > 0)) {
                alert("Select at least one item.");
                return false;
            }

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Enter Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').focus()
            return false
        }
        if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                document.getElementById('<%=txtAmount.ClientID %>').value = ""
               document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
               document.getElementById('<%=txtVATAmount.ClientID %>').value = ""
               document.getElementById('<%=txtCSTAmount.ClientID %>').value = ""
               <%--document.getElementById('<%=txtExciseAmount.ClientID %>').value = ""--%>

               document.getElementById('<%=hfAmount.ClientID %>').value = ""
               document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
               document.getElementById('<%=hfVATAmount.ClientID %>').value = ""
               document.getElementById('<%=hfCSTAmount.ClientID %>').value = ""
               <%--document.getElementById('<%=hfExciseAmount.ClientID %>').value = ""--%>

               var num
               num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
            if (num == false) {
                alert("Enter integers/Decimals for Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false
                }

                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
               var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

               document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
            document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                if (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0){
                document.getElementById('<%=ddlCST.ClientID %>').selectedIndex = 0;
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = "";
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = ""
            }


               if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		
 
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 

			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);

		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);
		document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);

		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			
		}
                

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(BasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
		

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(sVATAMOUNT)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)


			var sVATAmt = parseFloat((((ssTotalAmt - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}


                var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var sVATAmt = parseFloat((((ssTotalAmt - parseFloat(ssDiscount) + sExiceAmt)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			
                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt



		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value =  parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
		}
                

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);



			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById(sNetAmount).innerText = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

		}
                   

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var sNetTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                   var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

                   document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                   document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                   document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
               }
               if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
                   

                   var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
                   

		        if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }		
		        if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }
                   document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
               }

           }

        }


        function CalculateFinalAmountCST() {
        var list = document.getElementById("<%=lstBoxDescription.ClientID %>");
            var selected_items = 0;
            for (var i = 0; i < list.length; i++) {
                if (list[i].selected) {
                    selected_items++;
                }
            }
            if (!(selected_items > 0)) {
                alert("Select at least one item.");
                return false;
            }

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Enter Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').focus()
            return false
        }
        if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                document.getElementById('<%=txtAmount.ClientID %>').value = ""
               document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
               document.getElementById('<%=txtVATAmount.ClientID %>').value = ""
               document.getElementById('<%=txtCSTAmount.ClientID %>').value = ""
               <%--document.getElementById('<%=txtExciseAmount.ClientID %>').value = ""--%>

               document.getElementById('<%=hfAmount.ClientID %>').value = ""
               document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
               document.getElementById('<%=hfVATAmount.ClientID %>').value = ""
               document.getElementById('<%=hfCSTAmount.ClientID %>').value = ""
               <%--document.getElementById('<%=hfExciseAmount.ClientID %>').value = ""--%>

               var num
               num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
            if (num == false) {
                alert("Enter integers/Decimals for Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false
                }

                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
               var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

               document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
            document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);


            if (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0){
                document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex = 0;
                document.getElementById('<%=txtVATAmount.ClientID %>').value = "";
                document.getElementById('<%=hfVATAmount.ClientID %>').value = ""
            }

               if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		
 
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 

			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);

		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);
		document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);

		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			
		}
                

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(BasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value) 

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
		

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(sVATAMOUNT)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)


			var sVATAmt = parseFloat((((ssTotalAmt - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}


                var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var sVATAmt = parseFloat((((ssTotalAmt - parseFloat(ssDiscount) + sExiceAmt)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			
                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt



		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value =  parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
		}
                

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);



			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById(sNetAmount).innerText = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

		}
                   

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var sNetTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value 
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)
		sExiceAmt = parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)

		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                   var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

                   document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                   document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                   document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
               }
               if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
                   

                   var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
                   

		        if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }		
		        if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }
                   document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
               }

           }

        }




        function CalculateFinalAmountExcise() {
        var list = document.getElementById("<%=lstBoxDescription.ClientID %>");
            var selected_items = 0;
            for (var i = 0; i < list.length; i++) {
                if (list[i].selected) {
                    selected_items++;
                }
            }
            if (!(selected_items > 0)) {
                alert("Select at least one item.");
                return false;
            }

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Enter Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').focus()
            return false
        }
        if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                document.getElementById('<%=txtAmount.ClientID %>').value = ""
               document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
               document.getElementById('<%=txtVATAmount.ClientID %>').value = ""
               document.getElementById('<%=txtCSTAmount.ClientID %>').value = ""
               document.getElementById('<%=txtExciseAmount.ClientID %>').value = ""

               document.getElementById('<%=hfAmount.ClientID %>').value = ""
               document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
               document.getElementById('<%=hfVATAmount.ClientID %>').value = ""
               document.getElementById('<%=hfCSTAmount.ClientID %>').value = ""
               document.getElementById('<%=hfExciseAmount.ClientID %>').value = ""

               var num
               num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
            if (num == false) {
                alert("Enter integers/Decimals for Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false
                }

                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
               var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

               document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
               document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

               if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);

		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);
		document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);

		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			
		}
                

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(BasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
		

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(sVATAMOUNT)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}


                var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
                   

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
		}
                

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);



			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById(sNetAmount).innerText = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
                   

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var sNetTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                   var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

                   document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                   document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                   document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
               }
               if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
                   

                   var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
                   

		        if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }		
		        if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }
                   document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
               }

           }

        }


        function CalculateFinalAmountEX() {
        var list = document.getElementById("<%=lstBoxDescription.ClientID %>");
            var selected_items = 0;
            for (var i = 0; i < list.length; i++) {
                if (list[i].selected) {
                    selected_items++;
                }
            }
            if (!(selected_items > 0)) {
                alert("Select at least one item.");
                return false;
            }

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Enter Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').focus()
            return false
        }
        if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                document.getElementById('<%=txtAmount.ClientID %>').value = ""
               document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
               document.getElementById('<%=txtVATAmount.ClientID %>').value = ""
               document.getElementById('<%=txtCSTAmount.ClientID %>').value = ""
               document.getElementById('<%=txtExciseAmount.ClientID %>').value = ""

               document.getElementById('<%=hfAmount.ClientID %>').value = ""
               document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
               document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
               document.getElementById('<%=hfVATAmount.ClientID %>').value = ""
               document.getElementById('<%=hfCSTAmount.ClientID %>').value = ""
               document.getElementById('<%=hfExciseAmount.ClientID %>').value = ""

               var num
               num = CheckDecimal(document.getElementById('<%=txtQuantity.ClientID %>').value)
            if (num == false) {
                alert("Enter integers/Decimals for Quantity.")
                document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus()
                    return false
                }

                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
               var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

               document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
               document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

               if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt

		    var sVATAmt = parseFloat(((ssTotalAmt + parseFloat(sExiceAmt)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);

		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);
		document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((BasicAmount - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2);

		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;

                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			
		}
                

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(BasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt

			
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat(((ssTotalAmt + sExiceAmt) * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(sExiceAmt)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);


			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(ssTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
		

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(sVATAMOUNT)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAMOUNT)) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt

		    var sVATAmt = parseFloat((((ssTotalAmt + parseFloat(sExiceAmt)) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)


                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}


                var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

			var sVATAmt = parseFloat(((ssTotalAmt - parseFloat(ssDiscount)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sCST)) / 100).toFixed(2);

                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                
		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

		var BasicAmount = parseFloat(parseFloat(ssTotalAmt) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2);
		document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)
                document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sVAT)) / 100).toFixed(2)

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(BasicAmount) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(ssTotalAmt)) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount


                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt

			var sVATAmt = parseFloat(((parseFloat(ssTotalAmt - parseFloat(ssDiscount)) + parseFloat(sExiceAmt)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(BasicAmount) - parseFloat(sDiscountAMT)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sDiscountAMT)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt

		    var sVATAmt = parseFloat(((ssTotalAmt + parseFloat(sExiceAmt)) * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + sExiceAmt) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}


                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sBasicAmount = parseFloat(sVATAmt).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);

			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount

                var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount

                
                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt

			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) + parseFloat(sExiceAmt)) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt

		}
                   

                var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(((parseFloat(sBasicAmount) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var ddlvatt = document.getElementById('<%=ddlVAT.ClientID %>');
                var sVAT = ddlvatt.options[ddlvatt.selectedIndex].innerHTML;

                var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
                var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(sVATAmt)).toFixed(2)
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var sVATAmt = parseFloat((ssTotalAmt * parseFloat(sVAT)) / (100)).toFixed(2);
			document.getElementById('<%=txtVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
                	document.getElementById('<%=hfVATAmount.ClientID %>').value = parseFloat(sVATAmt).toFixed(2)
		}
                

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtVATAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex > 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){

			var ssTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)
			var sVAT = document.getElementById('<%=hfItemVAT.ClientID %>').value
			var sVATAmt = parseFloat((ssTotalAmt * 100) / (parseFloat(sVAT) + 100)).toFixed(2);
			var sVATAMOUNT = parseFloat(ssTotalAmt - sVATAmt).toFixed(2);



			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlcstt = document.getElementById('<%=ddlCST.ClientID %>');
                var sCST = ddlcstt.options[ddlcstt.selectedIndex].innerHTML;
                var sCSTAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sCST)) / 100).toFixed(2);
                document.getElementById('<%=txtCSTAmount.ClientID %>').value = sCSTAmt
                document.getElementById('<%=hfCSTAmount.ClientID %>').value = sCSTAmt
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtCSTAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - sVATAMOUNT).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById(sNetAmount).innerText = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
                   

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                   var sNetTotalAmt = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(sNetTotalAmt) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}

                var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex > 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - ssDiscount) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
                var sDiscountAMT = ssDiscount

                var ddlExicett = document.getElementById('<%=ddlExcise.ClientID %>');
                var sExice = ddlExicett.options[ddlExicett.selectedIndex].innerHTML;
                var sExiceAmt = parseFloat(parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(ssDiscount)) * parseFloat(sExice)) / 100).toFixed(2)
                document.getElementById('<%=txtExciseAmount.ClientID %>').value = sExiceAmt
                document.getElementById('<%=hfExciseAmount.ClientID %>').value = sExiceAmt
		}
                   

                var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)) + parseFloat(document.getElementById('<%=txtExciseAmount.ClientID %>').value)).toFixed(2)
                

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}		
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
		var sNetAmt = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value))).toFixed(2)
		}
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
            }
            if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex == 0) {

                   var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                   var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value

                   document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                   document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                   document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value).toFixed(2);
               }
               if ((document.getElementById('<%=txtQuantity.ClientID %>').value != "") && (document.getElementById('<%=ddlVAT.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlCST.ClientID %>').selectedIndex == 0) && (document.getElementById('<%=ddlExcise.ClientID %>').selectedIndex == 0) && document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

		if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
		if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                   	var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                   	var ssDiscount = parseFloat((parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) * parseFloat(sDISCOUNT)) / 100).toFixed(2)
                	document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ssDiscount
                   	document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ssDiscount
		}
                   

                   var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
                   

		        if (document.getElementById('<%=txtCode.ClientID %>').value == "C"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }		
		        if (document.getElementById('<%=txtCode.ClientID %>').value == "P"){
			        var sNetAmt = parseFloat(parseFloat(document.getElementById('<%=txtAmount.ClientID %>').value) - parseFloat(document.getElementById('<%=txtDiscountAmount.ClientID %>').value)).toFixed(2)
		        }
                   document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                   document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt
               }

           }

        }


        function CalculatePrice() {
            if (document.getElementById('<%=txtEnterPrice.ClientID %>').value != "") {
                if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 2) {
                     var ssDiscount = (parseFloat(document.getElementById('<%=lblRate.ClientID %>').innerHTML) - parseFloat(document.getElementById('<%=txtEnterPrice.ClientID %>').value)).toFixed(2)
                    document.getElementById('<%=txtMRP.ClientID %>').value = ssDiscount
                }
                else if (document.getElementById('<%=ddlreturntype.ClientID %>').selectedIndex == 4){
                     var ssDiscount = (parseFloat(document.getElementById('<%=lblRate.ClientID %>').innerHTML) + parseFloat(document.getElementById('<%=txtEnterPrice.ClientID %>').value)).toFixed(2)
                    document.getElementById('<%=txtMRP.ClientID %>').value = ssDiscount
                }
               
            }
            else {
                <%--document.getElementById('<%=txtGrandDiscountAmt.ClientID %>').value = ""
                document.getElementById('<%=txtGrandTotalAmt.ClientID %>').value = parseFloat(document.getElementById('<%=txtGrandTotal.ClientID %>').value)--%>
            }
            
        }

    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Sales Return</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
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
            <asp:Label runat="server" Visible ="false" Text="Search By Sales Return No."></asp:Label>
           <div class ="form-group" >
            <asp:TextBox ID="txtSearch" Visible ="false"  autocomplete="off" runat="server" CssClass="aspxcontrols" Width="200px" ></asp:TextBox>
            <asp:ImageButton ID="btnSearch" Visible ="false" runat="server" ImageUrl="~/Images/Search16.png"  CssClass="hvr-bounce-in" data-toggle="tooltip" data-placement="bottom" title="Search" CausesValidation="False" />
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Existing Sales Return No."></asp:Label>
            <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Sales Return No."></asp:Label>
            <asp:TextBox ID="txtSaleReturnCode" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Return Date"></asp:Label>
           <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReturnDate" runat="server" ControlToValidate="txtReturnDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Return Date" ValidationGroup="ValidateReturn"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVReturnDate" runat="server" ControlToValidate="txtReturnDate" Display="Dynamic" ErrorMessage="Enter Valid Return Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>--%>
            <asp:TextBox ID="txtReturnDate" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtReturnDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtReturnDate" TargetControlID="txtReturnDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft" ></cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label runat="server" Text="Status :-"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Return Reference No"></asp:Label>
            <asp:TextBox ID="txtReturnRefNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Mode Of Return"></asp:Label>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlModeOfShipping" runat="server" ControlToValidate="ddlModeOfShipping" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Mode of return" ValidationGroup="ValidateReturn"></asp:RequiredFieldValidator>--%>
            <asp:DropDownList ID="ddlModeOfShipping" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Reason For Return"></asp:Label>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlreturntype" runat="server" ControlToValidate="ddlreturntype" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Reason For Return" ValidationGroup="ValidateReturn"></asp:RequiredFieldValidator>--%>
            <asp:DropDownList ID="ddlreturntype" runat="server" CssClass="aspxcontrols">
                 <asp:ListItem Value="0">Select Reason For Return</asp:ListItem>
                    <asp:ListItem Value="1">Excess Qty</asp:ListItem>
                    <asp:ListItem Value="2">Excess Rate</asp:ListItem>
                    <asp:ListItem Value="3">Shortage of Qty</asp:ListItem>
                    <asp:ListItem Value="4">Shortage of Rate</asp:ListItem>
            </asp:DropDownList>
        </div>
     </div>

     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order No"></asp:Label>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlOrderNo" runat="server" ControlToValidate="ddlOrderNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Order No" ValidationGroup="ValidateReturn"></asp:RequiredFieldValidator>--%>
            <asp:DropDownList ID="ddlOrderNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice No"></asp:Label>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlInvoiceNo" runat="server" ControlToValidate="ddlInvoiceNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Invoice No" ValidationGroup="ValidateReturn"></asp:RequiredFieldValidator>--%>
            <asp:DropDownList ID="ddlInvoiceNo" runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable"></asp:DropDownList>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Dispatch Number"></asp:Label>
            <asp:TextBox ID="txtDispatchRefNo" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="E-Sugam No"></asp:Label>
            <asp:TextBox ID="txtESugamNo" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
     </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Order Date"></asp:Label>
            <asp:TextBox ID="txtOrderDate" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Invoice Date"></asp:Label>
            <asp:TextBox ID="txtDispatchDate" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Customer"></asp:Label>
            <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Customer code"></asp:Label>
            <asp:TextBox ID="txtPartyCode" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
     </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Category"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Payment Type"></asp:Label>
            <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="aspxcontrolsdisable"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Sales Type"></asp:Label>
            <asp:DropDownList ID="ddlSalesType" runat="server" CssClass="aspxcontrolsdisable">
                <asp:ListItem Value="0">Select Sale Type</asp:ListItem>
                    <asp:ListItem Value="1">Local Bill</asp:ListItem>
                    <asp:ListItem Value="2">Inter State Bill</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Other Type"></asp:Label>
            <asp:DropDownList ID="ddlOthers" runat="server" CssClass="aspxcontrolsdisable">
                <asp:ListItem Value="0">Select Others</asp:ListItem>
                    <asp:ListItem Value="1">Normal</asp:ListItem>
                    <asp:ListItem Value="2">CST 2% against C-Form</asp:ListItem>
                    <asp:ListItem Value="3">E - 1 FORM</asp:ListItem>
                    <asp:ListItem Value="4">F FORM - DIRECT EXPORT</asp:ListItem>
                    <asp:ListItem Value="5">H FORM - DEEMED EXPORT</asp:ListItem>
                    <asp:ListItem Value="6">I FORM - IMPORT</asp:ListItem>
            </asp:DropDownList>
        </div>
     </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
             <asp:Label runat="server" Text="Remarks"></asp:Label>
            <asp:TextBox ID="txtRemarks" runat="server" TextMode ="MultiLine" Height="80px" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Brand"></asp:Label>
            <asp:DropDownList ID="ddlCommodity" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <legend class="legendbold">Details of Sales Return</legend>
        </fieldset>
        <div class="col-sm-6 col-md-6" style="height: 200px ; padding-left: 0px">            
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">            
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Label runat="server" Text="* Item Description"></asp:Label>
                    <asp:TextBox ID="txtSearchItem" onkeyup="FilterItems(this.value)" runat="server" CssClass="aspxcontrols" placeholder="Search By Description" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 pre-scrollableborder" style="padding: 0px ">
               <asp:ListBox ID="lstBoxDescription" CssClass="aspxcontrols" AutoPostBack="true" runat="server" Height="450px" 
               Font-Names="Verdana" Font-Size="Smaller"></asp:ListBox>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVlstBoxDescription" runat="server" ControlToValidate="lstBoxDescription" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select An Item" ValidationGroup="ValidateReturn"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <br />
                <div class="col-sm-6 col-md-6">                    
                   <asp:Label runat="server" Text="Dispatched Qty"></asp:Label>
                   <asp:Label ID ="lblQty" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">                    
                    <asp:Label runat="server" Text="Dispatched Rate"></asp:Label>
                    <asp:Label ID ="lblRate" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList ID="ddlUnitOfMeassurement" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Enter Price"></asp:Label>
                    <asp:TextBox ID="txtEnterPrice" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox ID="txtMRP" autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <asp:HiddenField ID="hfMRP" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    <asp:HiddenField ID="hfAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label ID ="lblPCRate" runat="server" Text="* Rate" Visible ="false" ></asp:Label>
                    <asp:DropDownList ID="ddlRate" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Visible ="false" ></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    
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
                    <asp:Label runat="server" Text="Excise Duty"></asp:Label>
                     <asp:DropDownList ID="ddlExcise" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox ID="txtExciseAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    <asp:HiddenField ID="hfExciseAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="VAT"></asp:Label>
                    <asp:DropDownList ID ="ddlVAT" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox ID ="txtVATAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    <asp:HiddenField ID="hfVATAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="CST"></asp:Label>
                    <asp:DropDownList ID ="ddlCST" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:TextBox ID ="txtCSTAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    <asp:HiddenField ID="hfCSTAmount" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="Total Amount"></asp:Label>
                <asp:TextBox ID ="txtNetAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
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

    <div class="col-sm-12 col-md-12">
        <asp:GridView ID="dgSaleReturn" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="CommodityID" Visible="false" >
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DescID" Visible="false" >
                    <ItemTemplate>
                        <asp:Label ID="lblDescID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DescID") %>'></asp:Label>
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
                <asp:BoundField DataField="SlNo" HeaderText="Sl.No" Visible="False"></asp:BoundField>
                <asp:TemplateField HeaderText="Description Of Goods" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="25%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkGoods" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UnitOfMeassurement" HeaderText="Unit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:TemplateField HeaderText="ReturnQty" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblReturnQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReturnQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Rate" HeaderText="Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="4%"></asp:BoundField>
                <asp:BoundField DataField="Amount" HeaderText="Rate Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Discount" HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="4%"></asp:BoundField>
                <asp:BoundField DataField="DiscountAmount" HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="VAT" HeaderText="VAT" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="VATAmount" HeaderText="VATAmt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="CST" HeaderText="CST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="CSTAmount" HeaderText="CSTAmt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Excise" HeaderText="Excise" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="ExciseAmount" HeaderText="Excise Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="7%"></asp:BoundField>
                <asp:BoundField DataField="NetAmount" HeaderText="Total Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Images/4delete.gif" ToolTip="Delete/Cancel"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

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
