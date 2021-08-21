<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="CompanyMaster.aspx.vb" Inherits="Masters_CompanyMaster" ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <script src="../JavaScripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>

    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

       <style>        
                div{
            color:black;
                      }        
        </style>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>

    <script lang="javascript" type="text/javascript">ddlCompanyType
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        function Validate() {
            if (document.getElementById('<%=ddlCategory.ClientID %>').selectedIndex == 0) {
                    alert('Select GSTN Category.');
                    document.getElementById('<%=ddlCategory.ClientID%>').focus()
                    return false;
            }
            if (document.getElementById('<%=ddlCategory.ClientID %>').selectedIndex > 0) {
                var ddlPT = document.getElementById("<%=ddlCategory.ClientID %>");
                var ddlPaymentText = ddlPT.options[ddlPT.selectedIndex].innerHTML;
                if (ddlPaymentText == "UNRIGISTERED DEALER") {
                    
                }
                else {
                                       
                    var GSTIN = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}Z[(a-z)(A-Z)(0-9)]{1}?$/
                    if (document.getElementById('<%=txtProvisionNo.ClientID %>').value == "") {
                        alert('Enter Provision No.');
                        document.getElementById('<%=txtProvisionNo.ClientID%>').focus()
                        return false;
                    }
                    if (document.getElementById('<%=txtProvisionNo.ClientID %>').value != "") {               
                    var num
                    num = GSTIN.test(document.getElementById('<%=txtProvisionNo.ClientID %>').value)
                    if (num == false) {
                    alert("Enter Valid Delivery From GSTN Reg.No(Only 15 Charecters, Two integers,Five Capital alphabets, Four integers, One Capital alphabet, One integer, then Capital Z, One Integer/Alphabet).")
                    document.getElementById('<%=txtProvisionNo.ClientID %>').value = ""
                    document.getElementById('<%=txtProvisionNo.ClientID %>').focus()
                    return false
                    }               

                    }

                }

            }

        }

    </script>

    <script type="text/javascript" language="javascript" src="../JavaScripts/General.js"></script>
    <script language="javascript" type="text/javascript">
        <%--  function validationStatutoryRef() {
            if (Trim(document.getElementById('<%=txtName.ClientID %>').value) == "") {
                alert("Enter Statutory name");
                document.getElementById('<%=txtName.ClientID %>').focus();
                return false;
            }
            if (Trim(document.getElementById('<%=txtValue.ClientID %>').value) == "") {
                alert("Enter Statutory value");
                document.getElementById('<%=txtValue.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%=txtName.ClientID %>').value == "TIN" || document.getElementById('<%=txtName.ClientID %>').value == "tin" || document.getElementById('<%=txtName.ClientID %>').value == "VAT" || document.getElementById('<%=txtName.ClientID %>').value == "vat") {
                var a = isNaN(document.getElementById('<%=txtValue.ClientID %>').value);
                if (a == true) {
                    alert('Enter Only Numeric Value');
                    document.getElementById('<%=txtValue.ClientID %>').focus();
                    return false;
                }
                var b = document.getElementById('<%=txtValue.ClientID %>').value;

                if (b.length != 11) {
                    alert('TIN Number Length Should be 11 digit');
                    document.getElementById('<%=txtValue.ClientID %>').focus();
                    return false;
                }
            }


            if (document.getElementById('<%=txtName.ClientID %>').value == "PAN" || document.getElementById('<%=txtName.ClientID %>').value == "pan") {
                var pan = document.getElementById('<%=txtValue.ClientID %>').value;
                var regpan = /^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$/;

                if (regpan.test(pan) == false) {
                    alert('Permanent Account Number is not yet valid(First 5 letter Should be alphabets and next 4 should be numeric and last one is alphabets');
                    return false;
                }
            }


            if (document.getElementById('<%=txtName.ClientID %>').value == "TAN" || document.getElementById('<%=txtName.ClientID %>').value == "tan") {
                var tan = document.getElementById('<%=txtValue.ClientID %>').value;
                var regtan = /^([a-zA-Z]){4}([0-9]){5}([a-zA-Z]){1}?$/;
                if (regtan.test(tan) == false) {
                    alert('Tax deduction Account Number is not yet valid(First 4 letter Should be alphabets and next 5 should be numeric and last one is alphabets');
                    return false;
                }
            }
            if (document.getElementById('<%=txtName.ClientID %>').value == "CIN" || document.getElementById('<%=txtName.ClientID %>').value == "cin") {
                var cin = document.getElementById('<%=txtValue.ClientID %>').value;
                var regcin = /^([a-zA-Z]){1}([0-9]){5}([a-zA-Z]){2}([0-9]){4}([a-zA-Z]){3}([0-9]){6}?$/;
                if (regcin.test(cin) == false) {
                    alert('Tax Corporate Identity Number is not yet valid(First 1 letter Should be alphabets and next 5 should be numeric and next two should be alphabets and next four should be numeric folowed by 3 alphabets and six number');
                    return false;
                }
            }

            return true;
        }--%>


        <%--function validation() {

            if (document.getElementById('<%=ddlCompanyType.ClientID %>').selectedIndex == "0") {
                alert("Select Industries Type");
                document.getElementById('<%=ddlCompanyType.ClientID %>').focus();
                return false;
            }

            if (document.getElementById('<%=ddlSalesType.ClientID %>').selectedIndex == "0") {
                alert("Select Sales Type");
                document.getElementById('<%=ddlSalesType.ClientID %>').focus();
                return false;
            }

            if (Trim(document.getElementById('<%=txtCompanyCode.ClientID %>').value) == "") {
                alert("Enter Customer Code");
                document.getElementById('<%=txtCompanyCode.ClientID %>').focus();
                return false;
            }
            if (Trim(document.getElementById('<%=txtCompanyName.ClientID %>').value) == "") {
                alert("Enter Customer Abbreviation");
                document.getElementById('<%=txtCompanyName.ClientID %>').focus();
                return false;
            }

            if (Trim(document.getElementById('<%=txtEmail.ClientID %>').value) !== "") {
                var email
                email = CorrectEmail(document.getElementById('<%=txtEmail.ClientID %>').value)
                if (email == false) {
                    alert("Enter valid E-mail")
                    document.getElementById('<%=txtEmail.ClientID %>').value = ""
                    document.getElementById('<%=txtEmail.ClientID %>').focus()
                    return false;
                }
            }
            else
            {
                alert("Enter valid E-mail");
                document.getElementById('<%=txtEmail.ClientID %>').focus();
                return false;
            }


            if (Trim(document.getElementById('<%=txtBillAddress.ClientID %>').value) == "") {
                alert("Enter valid Contact Address")
                document.getElementById('<%=txtBillAddress.ClientID %>').focus()
                return false;
            }


            if (document.getElementById('<%=ddlBillCity.ClientID %>').selectedIndex == "0") {
                alert("Select City Type");
                document.getElementById('<%=ddlBillCity.ClientID %>').focus();
                return false;
            }

            if (Trim(document.getElementById('<%=txtBillPostalCode.ClientID %>').value) != "") {
                var a
                a = OnlyNumber(document.getElementById('<%=txtBillPostalCode.ClientID %>').value)
                if (a == false) {
                    alert('Enter Valid Postal Code');
                    document.getElementById('<%=txtBillPostalCode.ClientID %>').value = ""
                    document.getElementById('<%=txtBillPostalCode.ClientID %>').focus();
                    return false;
                }
            }

            if (document.getElementById('<%=ddlBillState.ClientID %>').selectedIndex == "0") {
                alert("Select State Type");
                document.getElementById('<%=ddlBillState.ClientID %>').focus();
                return false;
            }

            if (Trim(document.getElementById('<%=txtBillFax.ClientID%>').value) != "") {
                var a
                a = OnlyNumber(document.getElementById('<%=txtBillFax.ClientID %>').value)
                if (a == false) {
                    alert('Enter Valid Fax');
                    document.getElementById('<%=txtBillFax.ClientID %>').value = ""
                    document.getElementById('<%=txtBillFax.ClientID %>').focus();
                    return false;
                }
            }

            if (Trim(document.getElementById('<%=txtBillTelphone.ClientID %>').value) !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtBillTelphone.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Telephone number")
                    document.getElementById('<%=txtBillTelphone.ClientID %>').value = ""
                    document.getElementById('<%=txtBillTelphone.ClientID %>').focus()
                    return false;
                }
            }
            
            if (Trim(document.getElementById('<%=txtBillTelphone1.ClientID %>').value) !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtBillTelphone1.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Telephone No 1")
                    document.getElementById('<%=txtBillTelphone1.ClientID %>').value = ""
                    document.getElementById('<%=txtBillTelphone1.ClientID %>').focus()
                    return false;
                }
            }

            if (Trim(document.getElementById('<%=txtBillTelphone2.ClientID %>').value) !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtBillTelphone2.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Telephone No 2")
                    document.getElementById('<%=txtBillTelphone2.ClientID %>').value = ""
                    document.getElementById('<%=txtBillTelphone2.ClientID %>').focus()
                    return false;
                }
            }

            if (document.getElementById('<%=ddlDelCity.ClientID %>').selectedIndex == "0") {
                alert("Select Delivered Office City Type");
                document.getElementById('<%=ddlDelCity.ClientID %>').focus();
                return false;
            }

            if (Trim(document.getElementById('<%=txtDelPostalCode.ClientID%>').value) != "") {
                var a
                a = OnlyNumber(document.getElementById('<%=txtDelPostalCode.ClientID %>').value)
                if (a == false) {
                    alert('Enter Valid Delivered Office Postal Code');
                    document.getElementById('<%=txtDelPostalCode.ClientID %>').value = ""
                    document.getElementById('<%=txtDelPostalCode.ClientID %>').focus();
                    return false;
                }
            }

            if (document.getElementById('<%=ddlDelState.ClientID %>').selectedIndex == "0") {
                alert("Select Delivered Office State Type");
                document.getElementById('<%=ddlDelState.ClientID %>').focus();
                 return false;
             }

            if (Trim(document.getElementById('<%=txtDelFax.ClientID %>').value) !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtDelFax.ClientID %>').value)
                if (num == false) {
                    alert("Enter valid Delivered Office Fax number")
                    document.getElementById('<%=txtDelFax.ClientID %>').value = ""
                    document.getElementById('<%=txtDelFax.ClientID %>').focus()
                    return false;
                }
            }

            if (Trim(document.getElementById('<%=txtDelTelephone.ClientID %>').value) !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtDelTelephone.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Delivered Office Telephone number")
                    document.getElementById('<%=txtDelTelephone.ClientID %>').value = ""
                    document.getElementById('<%=txtDelTelephone.ClientID %>').focus()
                    return false;
                }
            }
            
            if (Trim(document.getElementById('<%=txtDelTelephone1.ClientID %>').value) !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtDelTelephone1.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Delivered Office Telephone No 1")
                    document.getElementById('<%=txtDelTelephone1.ClientID %>').value = ""
                    document.getElementById('<%=txtDelTelephone1.ClientID %>').focus()
                    return false;
                }
            }

            if (Trim(document.getElementById('<%=txtDelTelephone2.ClientID %>').value) !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtDelTelephone2.ClientID %>').value)
                if (num == false) {
                    alert("Enter Valid Delivered Office Telephone No 2")
                    document.getElementById('<%=txtDelTelephone2.ClientID %>').value = ""
                    document.getElementById('<%=txtDelTelephone2.ClientID %>').focus()
                    return false;
                }
            }

            return true;
        }--%>


        <%--   function Branchvalidation() {
            if (Trim(document.getElementById('<%=txtBranchName.ClientID %>').value) == "") {
                alert("Enter Branch Name.");
                document.getElementById('<%=txtBranchName.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlBranchCity.ClientID %>').selectedIndex == "0") {
                alert("Select City.");
                document.getElementById('<%=ddlBranchCity.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlBranchState.ClientID %>').selectedIndex == "0") {
                alert("Select State.");
                document.getElementById('<%=ddlBranchState.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=ddlBranchCountry.ClientID %>').selectedIndex == "0") {
                alert("Select Country.");
                document.getElementById('<%=ddlBranchCountry.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtBranchPostalCode.ClientID %>').value !== "") {
                var no;
                no = document.getElementById('<%=txtBranchPostalCode.ClientID %>').value
                if (no.length > 6) {
                    alert('PinCode exceeded maximum size( Only 6 Characters).');
                    document.getElementById('<%=txtBranchPostalCode.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(no)) {
                        alert("Enter PinCode spaces are not allowed.");
                        document.getElementById('<%=txtBranchPostalCode.ClientID%>').focus();
                    return false;
                }
            }
            if (Trim(document.getElementById('<%=txtContactPerson.ClientID %>').value) == "") {
                alert("Enter Contact Person.");
                document.getElementById('<%=txtContactPerson.ClientID %>').focus();
                return false;
            }
            if (Trim(document.getElementById('<%=txtBranchPhoneNo.ClientID %>').value) == "") {
                alert("Enter Phone no.");
                document.getElementById('<%=txtBranchPhoneNo.ClientID %>').focus();
                return false;
            }
            if (document.getElementById('<%=txtBranchFAX.ClientID %>').value !== "") {
                    var no;
                    no = document.getElementById('<%=txtBranchFAX.ClientID %>').value
                    if (no.length > 12) {
                        alert('FAX exceeded maximum size( Only 12 Characters).');
                        document.getElementById('<%=txtBranchFAX.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(no)) {
                        alert("Enter FAX spaces are not allowed.");
                        document.getElementById('<%=txtBranchFAX.ClientID%>').focus();
                        return false;
                    }
                }
            if (document.getElementById('<%=txtBranchAddress.ClientID%>').value != "") {
                var address;
                address = document.getElementById('<%=txtBranchAddress.ClientID %>').value
                if (address.length > 1000) {
                    alert('Address exceeded maximum size(Only 1000 Characters).');
                    document.getElementById('<%=txtBranchAddress.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(address)) {
                        alert("Enter Address spaces are not allowed.");
                        document.getElementById('<%=txtBranchAddress.ClientID%>').focus();
                    return false;
                }
            }
        }--%>
    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>1.2 Company Master</b></h2>
            </div>
            <div class="form-group pull-right">
                <%-- <asp:ImageButton ID="imgbtnAddOther" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Other Details" />
                <asp:ImageButton ID="imgbtnAddBranch" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Branch Details" />--%>
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateUP" />
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Company Code:- " Font-Bold="true"></asp:Label>
            <asp:Label ID="lblCompCode" runat="server" CssClass="aspxlabelbold"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Company Abbreviation"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Company Abbreviation" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtCompanyName" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="E-Mail"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVemail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True"
                    ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVemail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True"
                    ValidationGroup="ValidateUP"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Company Type"></asp:Label>
                <asp:DropDownList ID="ddlCompanyType" runat="server" AutoPostBack ="true"  CssClass="aspxcontrols"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyType" runat="server" ControlToValidate="ddlCompanyType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Company Type" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
            </div>
        </div>
        <%--<div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Sales Type"></asp:Label>
                <asp:DropDownList ID="ddlSalesType" runat="server" CssClass="aspxcontrols">
                    <asp:ListItem Value="0">Select Sales Type</asp:ListItem>
                    <asp:ListItem Value="1">Manufacturer</asp:ListItem>
                    <asp:ListItem Value="2">Trader</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSalesType" runat="server" ControlToValidate="ddlSalesType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Sales Type" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
            </div>
        </div>--%>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <fieldset>
                <legend class="legendbold">Billing Address</legend>
                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Address"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillAddress" runat="server" ControlToValidate="txtBillAddress" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Bill Address" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtBillAddress" CssClass="aspxcontrols" runat="server" TextMode="MultiLine" Height="64px"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Postal Code"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillPostalCode" runat="server" ControlToValidate="txtBillPostalCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Postal Code" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillPostalCode" runat="server" ControlToValidate="txtBillPostalCode" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="ValidateUP"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtBillPostalCode" MaxLength="6" CssClass="aspxcontrols" runat="server"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="City"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillCity" runat="server" ControlToValidate="ddlBillCity" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Billing City" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlBillCity" runat="server" CssClass="aspxcontrols"></asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="State"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillState" runat="server" ControlToValidate="ddlBillState" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Billing State" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlBillState" runat="server" CssClass="aspxcontrols"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Country"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillCountry" runat="server" ControlToValidate="ddlBillCountry" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Billing Country" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlBillCountry" runat="server" CssClass="aspxcontrols"></asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Fax"></asp:Label>
                        <asp:TextBox ID="txtBillFax" MaxLength="10" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Telephone"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillTelphone" runat="server" ControlToValidate="txtBillTelphone" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Telephone No." ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBillTelphone" runat="server" ControlToValidate="txtBillTelphone" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="ValidateUP"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtBillTelphone" MaxLength="11" CssClass=" aspxcontrols" runat="server"></asp:TextBox>

                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" Text="Telephone No 1"></asp:Label>
                        <asp:TextBox ID="txtBillTelphone1" MaxLength="11" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" Text="Telephone No 2"></asp:Label>
                        <asp:TextBox ID="txtBillTelphone2" MaxLength="11" CssClass="aspxcontrols " runat="server"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
        <div class="col-sm-6 col-md-6">
            <fieldset>
                <legend class="legendbold">Delivery Address
                    <asp:CheckBox ID="chkSameAddress" runat="server" Font-Size="12px" AutoPostBack="true" CssClass="pull-right" Text="Same as Contact Address" />
                </legend>
                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Address"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDelAddress" runat="server" ControlToValidate="txtDelAddress" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Bill Address" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDelAddress" CssClass="aspxcontrols" runat="server" TextMode="MultiLine" Height="64px"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Postal Code"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDelPostalCode" runat="server" ControlToValidate="txtDelPostalCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Postal Code" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDelPostalCode" runat="server" ControlToValidate="txtDelPostalCode" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="ValidateUP"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtDelPostalCode" MaxLength="6" CssClass="aspxcontrols" runat="server"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="City"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDelCity" runat="server" ControlToValidate="ddlDelCity" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Delivery City" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlDelCity" runat="server" CssClass="aspxcontrols"></asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="State"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDelState" runat="server" ControlToValidate="ddlDelState" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Delivery State" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlDelState" runat="server" CssClass="aspxcontrols"></asp:DropDownList>

                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Country"></asp:Label>
                        <asp:DropDownList ID="ddlDelCountry" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDelCountry" runat="server" ControlToValidate="ddlDelCountry" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Delivery Country" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Fax"></asp:Label>
                        <asp:TextBox ID="txtDelFax" MaxLength="10" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Telephone"></asp:Label>
                        <asp:TextBox ID="txtDelTelephone" MaxLength="11" CssClass=" aspxcontrols" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDelTelephone" runat="server" ControlToValidate="txtDelTelephone" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Telephone No." ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDelTelephone" runat="server" ControlToValidate="txtDelTelephone" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="ValidateUP"></asp:RegularExpressionValidator>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" Text="Telephone No 1"></asp:Label>
                        <asp:TextBox ID="txtDelTelephone1" MaxLength="11" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label runat="server" Text="Telephone No 2"></asp:Label>
                        <asp:TextBox ID="txtDelTelephone2" MaxLength="11" CssClass="aspxcontrols " runat="server"></asp:TextBox>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
       <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblCategory" Text ="GSTN Category" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlCategory" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                 <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCategory" runat="server" ControlToValidate="ddlCategory" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select GSTN Category" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>
            </div>
       </div>  
        <div class="col-sm-3 col-md-3" >
            <div class="form-group">
                <asp:Label ID="lblProvisionNo" Text ="GST Provisional No" runat="server"></asp:Label>
                <asp:TextBox ID ="txtProvisionNo" runat ="server" CssClass ="aspxcontrols"></asp:TextBox>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVProvisionNo" runat="server" ControlToValidate="txtProvisionNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Provisional No" ValidationGroup="ValidateUP"></asp:RequiredFieldValidator>--%>
                <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVProvisionNo" runat="server" ValidationGroup="ValidateUP" ControlToValidate="txtProvisionNo" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>--%>
            </div>
       </div>     
        <div class="col-sm-3 col-md-3" >
            <div class="form-group">
                <asp:Label ID="lblFinalNo" Text ="GST Final No" runat="server"></asp:Label>
                <asp:TextBox ID ="txtFinalNo" runat ="server" CssClass ="aspxcontrols"></asp:TextBox>
            </div>
       </div>    
      <%-- <div class="col-sm-3 col-md-3" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblForm" Text ="GSTR Forms" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlForm" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
       </div> 
        <div class="col-sm-3 col-md-3" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblPeriodicity" Text ="Periodicity" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlPeriodicity" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
       </div>  --%>                 
    </div>

    <div class="col-md-12">
        <div id="divcollapseOtherDetails" class="form-group" runat="server" visible="false" data-toggle="collapse" data-target="#collapseOtherDetails"><a href="#"><b><i>Click here to Enter Other Details</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseOtherDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-12 col-md-12">
                    <p>Create Other Details Such as VAT, TAX, PAN, TAN, TIN, CIN, ECC and Others</p>
                </div>
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                    <div class="col-sm-5 col-md-5">
                        <asp:TextBox ID="txtName" runat="server" placeholder="Statutory Name" class="aspxcontrols"></asp:TextBox>
                    </div>
                    <div class="col-sm-5 col-md-5" style="padding-left: 0px">
                        <asp:TextBox ID="txtValue" runat="server" placeholder="Statutory Value" class="aspxcontrols"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" class=" btn-ok" ValidationGroup="ValidateStatutory" />
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <asp:DataGrid ID="dgOtherDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="99%" class="footable">
                    <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" Visible="false">
                            <ItemStyle Width="0%" />
                            <HeaderStyle Width="0%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Name" HeaderText="Name">
                            <ItemStyle Width="20%" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Value" HeaderText="Value">
                            <ItemStyle Width="45%" />
                            <HeaderStyle Width="45%" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/Trash16.png" data-toggle="tooltip" data-placement="bottom" title="Delete" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                            <HeaderStyle Width="5%" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div id="divcollapseBranchDetails" class="form-group" runat="server" visible="false" data-toggle="collapse" data-target="#collapseBranchDetails"><a href="#"><b><i>Click here to Enter Branch Details</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseBranchDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Branch"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchName" runat="server" ControlToValidate="ddlBranch" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Branch" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <%--<asp:TextBox ID="txtBranchName" CssClass="aspxcontrols" runat="server"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="aspxcontrols" AutoPostBack ="true" ></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Country"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchCountry" runat="server" ControlToValidate="ddlBranchCountry" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Country" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlBranchCountry" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="CompanyType"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFvddlBranchCompanyType" runat="server" ControlToValidate="ddlBranchCompanyType" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Company Type" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlBranchCompanyType" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Contact Person"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBContactPerson" runat="server" ControlToValidate="txtContactPerson" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Contact Person" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtContactPerson" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="State"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchState" runat="server" ControlToValidate="ddlBranchState" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select State" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlBranchState" runat="server" CssClass="aspxcontrols"></asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Postal Code"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchPostalCode" runat="server" ControlToValidate="txtBranchPostalCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Postal Code" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBranchPostalCode" runat="server" ControlToValidate="txtBranchPostalCode" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="ValidateSave"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtBranchPostalCode" MaxLength="6" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Phone No"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchPhno" runat="server" ControlToValidate="txtBranchPhoneNo" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Phone No." ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBranchPhno" runat="server" ControlToValidate="txtBranchPhoneNo" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="ValidateSave"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtBranchPhoneNo" MaxLength="15" CssClass=" aspxcontrols" runat="server"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="City"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchCity" runat="server" ControlToValidate="ddlBranchCity" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select City" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlBranchCity" runat="server" CssClass="aspxcontrols"></asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Fax"></asp:Label>
                        <asp:TextBox ID="txtBranchFax" MaxLength="15" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="GSTN Category"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlBranchGSTNCategory" runat="server" ControlToValidate="ddlBranchGSTNCategory" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select GSTN Category" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlBranchGSTNCategory" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="GSTN Reg.No"></asp:Label>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBranchGSTNRegNo" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter GSTN Reg.No" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox ID="txtBranchGSTNRegNo" runat="server" CssClass="aspxcontrols "></asp:TextBox>

                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Address"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchAddress" runat="server" ControlToValidate="txtBranchAddress" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Branch Address" ValidationGroup="ValidateSave"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtBranchAddress" CssClass="aspxcontrols" Height="80px" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <asp:Button ID="btnBranchNew" runat="server" Text="New" class="btn-ok" />
                <asp:Button ID="btnBranchSave" runat="server" Text="Save" class="btn-ok" ValidationGroup="ValidateSave" />
            </div>
            <div class="col-sm-12 col-md-12">
                <asp:DataGrid ID="grdBranch" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" class="footable">
                    <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                    <Columns>
                        <asp:BoundColumn DataField="Branch_ID" HeaderText="Branch_ID" Visible="False">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Branch Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="BranchName" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Branch") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="400px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="ContactPerson" HeaderText="Contact Person">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="500px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Address" HeaderText="Address">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="700px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Bottom" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="City" HeaderText="City">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="200px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="State" HeaderText="State">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="200px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Pincode" HeaderText="Pin Code">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="200px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>

    <div class="col-md-12">
        <div id="divbankdetails" class="form-group" runat="server" visible="false" data-toggle="collapse" data-target="#collapsebankdetails"><a href="#"><b><i>Click here to Enter Bank Details</i></b></a></div>
    </div>

    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapsebankdetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Bank Name"></asp:Label>
                        <asp:DropDownList ID="ddlBnkName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REVddlBnkName" runat="server" ControlToValidate="ddlBnkName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Bank Name" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Account No"></asp:Label>
                        <asp:TextBox ID="txtAccNo" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REQFVAccNo" runat="server" ControlToValidate="txtAccNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Account No" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAccNo" runat="server" ControlToValidate="txtAccNo" Display="Dynamic" ErrorMessage="Enter number of (Max 12 length) , character/Spaces not allowed." SetFocusOnError="True" ValidationExpression="^[0-9]{12}" ValidationGroup="ValidateAdd"></asp:RegularExpressionValidator>  
                        <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAccNo" runat="server" ControlToValidate="txtAccNo" Display="Dynamic" ErrorMessage="Enter only integers (Max length 12), character/Strings not allowed." SetFocusOnError="True" ValidationExpression="^[0-9]{12}"></asp:RegularExpressionValidator>    --%>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="IFSC Code"></asp:Label>
                        <asp:TextBox ID="txtIFSCode" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REQFIFSCode" runat="server" ControlToValidate="txtIFSCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter IFSC Code" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Branch Name"></asp:Label>
                        <asp:TextBox ID="txtBrnchName" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REQFBrnchName" runat="server" ControlToValidate="txtBrnchName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Branch Name" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>          
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        &nbsp; &nbsp;
                        <br />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-ok" ValidationGroup="ValidateAdd" />
                    </div>
                </div>

            </div>
            <div class="col-sm-12 col-md-12">
                <asp:DataGrid ID="ddgBnkDtls" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="99%" class="footable">
                    <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                    <Columns>
                        <asp:BoundColumn DataField="BankID" Visible="false">
                            <ItemStyle Width="0%" />
                            <HeaderStyle Width="0%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BankName" HeaderText="Bank Name">
                            <ItemStyle Width="20%" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="AccountNo" HeaderText="Account No">
                            <ItemStyle Width="20%" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IFSCCode" HeaderText="IFSC Code">
                            <ItemStyle Width="20%" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="BranchName" HeaderText="Branch Name">
                            <ItemStyle Width="20%" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>

        </div>
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
                                <asp:Label ID="lblFASSCompanyValidationMsg" runat="server"></asp:Label></strong>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

