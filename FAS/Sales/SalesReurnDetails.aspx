<%@ Page Title="" Language="VB" MasterPageFile="~/Sales.master" AutoEventWireup="false" CodeFile="SalesReurnDetails.aspx.vb" Inherits="Sales_SalesReurnDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
         div {
            color: black;
        }
    </style>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script type="text/javascript" language="javascript" src="../JavaScripts/General.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgSRItemDetails.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('#<%=ddlExistSales.ClientID%>').select2();
            $('#<%=ddlInvoiceNo.ClientID%>').select2();
            $('#<%=ddlOrderNo.ClientID%>').select2();
            $('#<%=ddlDispatchNo.ClientID%>').select2();
            $('#<%=ddlCustomer.ClientID%>').select2();
            $('#<%=ddlCommodity.ClientID%>').select2();
            $('#<%=ddlReturn.ClientID%>').select2();
        });


        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

    </script>
    <script type="text/javascript" language="javascript">
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
     function RateAmount() {
         if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
             document.getElementById('<%=txtAmount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
               <%-- document.getElementById('<%=txtCharges.ClientID %>').value = ""--%>

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
                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value
                
                var sCharges
               <%-- var sCharges = document.getElementById('<%=txtCharges.ClientID %>').value--%>
                var sAmt = document.getElementById('<%=txtAmt.ClientID %>').value
                var sSAmt = document.getElementById('<%=txtSAmt.ClientID %>').value


                document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                var sTotAmt = document.getElementById('<%=txtAmount.ClientID %>').value;
                //sCharges = parseFloat((sTotAmt * sAmt) / sSAmt).toFixed(2);
                sCharges = 0
               <%-- document.getElementById('<%=txtCharges.ClientID %>').value = 0--%>

                var sTotal = parseFloat(sQuantity * sMRP).toFixed(2);

                var sGST = document.getElementById('<%=txtGSTRate.ClientID %>').value

                var sGSTAmt = parseFloat(((parseFloat(sTotal) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);
                document.getElementById('<%=hfGSTAmount.ClientID %>').value = sGSTAmt
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = sGSTAmt
                var sGSTAMT = parseFloat(((parseFloat(sTotal) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);

                var sNetAmt = parseFloat(parseFloat(sTotal) + parseFloat(sCharges) + parseFloat(sGSTAmt)).toFixed(2);
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt

                if (document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                    var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                    var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                    var ssDiscount = parseFloat((parseFloat(sTotal) * parseFloat(sDISCOUNT)) / 100).toFixed(2)

                    document.getElementById('<%=hfDiscountAmount.ClientID %>').value = parseFloat(ssDiscount).toFixed(2);
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = parseFloat(ssDiscount).toFixed(2);

                    var sGST = document.getElementById('<%=txtGSTRate.ClientID %>').value
                    document.getElementById('<%=hfGSTAmount.ClientID %>').value = parseFloat((((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);
                    document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);
                    var sGSTAMT = parseFloat(((parseFloat(sTotal) - parseFloat(ssDiscount) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);

                    document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sCharges)) + parseFloat(sGSTAMT)).toFixed(2);
                document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sCharges)) + parseFloat(sGSTAMT)).toFixed(2);
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


        function RateAmountQtyCheck(sAvailableQty, sReturnqty) {
            if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {

                var ssReturnqty = document.getElementById(sReturnqty).value
                var ssAvailableQty = parseFloat(document.getElementById(sAvailableQty).innerHTML)
                if ((ssAvailableQty < ssReturnqty)) {
                    alert("You can not enter qty greater than the Sold qty")
                    document.getElementById('<%=txtQuantity.ClientID %>').value == ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus();

                    document.getElementById('<%=txtAmount.ClientID %>').value = ""
                    document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                    document.getElementById('<%=txtGSTAmount.ClientID %>').value = ""
                    document.getElementById('<%=txtNetAmount.ClientID %>').value = ""

                    document.getElementById('<%=hfAmount.ClientID %>').value = ""
                    document.getElementById('<%=hfDiscountAmount.ClientID %>').value = ""
                    document.getElementById('<%=hfGSTAmount.ClientID %>').value = ""
                    document.getElementById('<%=hfNetAmount.ClientID %>').value = ""
                    return false;
                }

             document.getElementById('<%=txtAmount.ClientID %>').value = ""
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = ""
                document.getElementById('<%=txtNetAmount.ClientID %>').value = ""
               <%-- document.getElementById('<%=txtCharges.ClientID %>').value = ""--%>

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
                var sQuantity = document.getElementById('<%=txtQuantity.ClientID %>').value
                var sMRP = document.getElementById('<%=txtMRP.ClientID %>').value
                
                var sCharges
               <%-- var sCharges = document.getElementById('<%=txtCharges.ClientID %>').value--%>
                var sAmt = document.getElementById('<%=txtAmt.ClientID %>').value
                var sSAmt = document.getElementById('<%=txtSAmt.ClientID %>').value


                document.getElementById('<%=hfAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);
                document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sQuantity * sMRP).toFixed(2);

                var sTotAmt = document.getElementById('<%=txtAmount.ClientID %>').value;
                //sCharges = parseFloat((sTotAmt * sAmt) / sSAmt).toFixed(2);
                sCharges = 0
               <%-- document.getElementById('<%=txtCharges.ClientID %>').value = 0--%>

                var sTotal = parseFloat(sQuantity * sMRP).toFixed(2);

                var sGST = document.getElementById('<%=txtGSTRate.ClientID %>').value

                var sGSTAmt = parseFloat(((parseFloat(sTotal) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);
                document.getElementById('<%=hfGSTAmount.ClientID %>').value = sGSTAmt
                document.getElementById('<%=txtGSTAmount.ClientID %>').value = sGSTAmt
                var sGSTAMT = parseFloat(((parseFloat(sTotal) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);

                var sNetAmt = parseFloat(parseFloat(sTotal) + parseFloat(sCharges) + parseFloat(sGSTAmt)).toFixed(2);
                document.getElementById('<%=txtNetAmount.ClientID %>').value = sNetAmt
                document.getElementById('<%=hfNetAmount.ClientID %>').value = sNetAmt

                if (document.getElementById('<%=ddlDiscount.ClientID %>').selectedIndex > 0) {

                    var ddlDiscountt = document.getElementById('<%=ddlDiscount.ClientID %>');
                    var sDISCOUNT = ddlDiscountt.options[ddlDiscountt.selectedIndex].innerHTML;
                    var ssDiscount = parseFloat((parseFloat(sTotal) * parseFloat(sDISCOUNT)) / 100).toFixed(2)

                    document.getElementById('<%=hfDiscountAmount.ClientID %>').value = parseFloat(ssDiscount).toFixed(2);
                document.getElementById('<%=txtDiscountAmount.ClientID %>').value = parseFloat(ssDiscount).toFixed(2);

                    var sGST = document.getElementById('<%=txtGSTRate.ClientID %>').value
                    document.getElementById('<%=hfGSTAmount.ClientID %>').value = parseFloat((((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);
                    document.getElementById('<%=txtGSTAmount.ClientID %>').value = parseFloat((((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);
                    var sGSTAMT = parseFloat(((parseFloat(sTotal) - parseFloat(ssDiscount) + parseFloat(sCharges)) * sGST) / 100).toFixed(2);

                    document.getElementById('<%=txtNetAmount.ClientID %>').value = parseFloat(((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sCharges)) + parseFloat(sGSTAMT)).toFixed(2);
                document.getElementById('<%=hfNetAmount.ClientID %>').value = parseFloat(((parseFloat(sTotal) - parseFloat(ssDiscount)) + parseFloat(sCharges)) + parseFloat(sGSTAMT)).toFixed(2);
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
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Sales Return Details</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh/Clear" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save/Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnReport" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Report" CausesValidation="false" />
                    <%--<ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>--%>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px; left: 0px; top: 0px;">
        <div class="col-sm-12 col-md-12" style="left: 0px; top: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Return No"></asp:Label>
            <asp:DropDownList ID="ddlExistSales" runat="server" CssClass="aspxcontrols" AutoPostBack="True" ValidationGroup="Validate"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Return No"></asp:Label>
            <asp:TextBox ID="txtReturnNo" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Return Date"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReturnDate" runat="server" ControlToValidate="txtReturnDate" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter Return Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REFReturnDate" runat="server" ControlToValidate="txtReturnDate" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtReturnDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
            <cc1:CalendarExtender ID="cclReturnDate" runat="server" PopupButtonID="txtReturnDate" PopupPosition="BottomLeft"
                TargetControlID="txtReturnDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label runat="server" Text="Status:- " Font-Bold="true"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice No"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceNo" runat="server" ControlToValidate="ddlInvoiceNo" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Select Invoice No." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlInvoiceNo" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Invoice Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order No"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOrderNo" runat="server" ControlToValidate="ddlOrderNo" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Select Order No." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlOrderNo" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Dispatch No"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDispatchNo" runat="server" ControlToValidate="ddlDispatchNo" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Select Dispatch No." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlDispatchNo" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:DropDownList>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Customer"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomer" runat="server" ControlToValidate="ddlCustomer" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Select Customer." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text=" Ship To"></asp:Label>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVShipTo" runat="server" ControlToValidate="txtShipTo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Ship To." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
            <asp:TextBox ID="txtShipTo" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="70"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Goods Return Ref No"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVGoodsReturnRefNo" runat="server" ControlToValidate="txtGoodsReturnRefNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Return Ref No." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtGoodsReturnRefNo" runat="server" CssClass="aspxcontrols" TabIndex="-1"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Commodity"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCommodity" runat="server" ControlToValidate="ddlCommodity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Commodity" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlCommodity" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
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
            <legend class="legendbold">Details of Sales Return</legend>
        </fieldset>
    </div>
    <div class="col-sm-6 col-md-6">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <asp:Label runat="server" Text="* Item Description"></asp:Label>
            <asp:TextBox ID="txtSearchItem" onkeyup="FilterItems(this.value)" runat="server" CssClass="aspxcontrols" placeholder="Search By Description" data-toggle="tooltip" data-placement="top"></asp:TextBox>
        </div>
        <div class="col-sm-12 col-md-12 pre-scrollableborder form-group" style="padding: 0px">
            <asp:ListBox ID="lstBoxDescription" CssClass="aspxcontrols" AutoPostBack="true" runat="server" Height="185px"
                Font-Names="Verdana" Font-Size="Smaller"></asp:ListBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVlstBoxDescription" runat="server" ControlToValidate="lstBoxDescription" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select An Item" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <asp:Label runat="server" Text="Remarks"></asp:Label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="aspxcontrols" Height="55px" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-6 col-md-6" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-6 col-md-6" >
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList ID="ddlUnitOfMeassurement" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:DropDownList>
            </div>
            <div class="col-sm-6 col-md-6 form-group" style="padding-right: 0px">
                <br />
                <asp:Label runat="server" Text="Sold Qty"></asp:Label>
                <asp:Label ID="lblInvoiceQty" runat="server"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-6 col-md-6">
                <asp:Label runat="server" Text="* Quantity"></asp:Label>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Quantity" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                <br />
                <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                <asp:HiddenField ID="hfAmount" runat="server" />
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-6 col-md-6">
                <asp:Label ID="lblRate" runat="server" Text="* Rate"></asp:Label>
                <asp:TextBox ID="txtMRP" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                <asp:HiddenField ID="hfMRP" runat="server" />
            </div>
            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Reason For Return"></asp:Label>
                <asp:DropDownList ID="ddlReturn" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    <asp:ListItem Value="0">Select Reason For Return</asp:ListItem>
                    <asp:ListItem Value="1">Expired</asp:ListItem>
                    <asp:ListItem Value="2">Damaged</asp:ListItem>
                    <asp:ListItem Value="3">Price Difference</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReturn" runat="server" ControlToValidate="ddlReturn" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Reason For Return" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-6 col-md-6">
                <asp:Label runat="server" Text="Discount"></asp:Label>
                <asp:DropDownList ID="ddlDiscount" runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:DropDownList>
            </div>
            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                <br />
                <asp:TextBox ID="txtDiscountAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                <asp:HiddenField ID="hfDiscountAmount" runat="server" />
            </div>
        </div>       
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-6 col-md-6">
                <asp:Label runat="server" Text="GST Rate %"></asp:Label>
                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtGSTRate" TabIndex="-1"></asp:TextBox>
            </div>
            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                <asp:Label runat="server" Text="GST Amount"></asp:Label>
                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtGSTAmount" TabIndex="-1"></asp:TextBox>
                <asp:HiddenField ID="hfGSTAmount" runat="server" />
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
            <asp:Label runat="server" Text="Total Amount"></asp:Label>
            <asp:TextBox ID="txtNetAmount" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
            <asp:HiddenField ID="hfNetAmount" runat="server" />
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:GridView ID="dgSRItemDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                        <asp:Label ID="lblMasterID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MasterID") %>'></asp:Label>
                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CommodityID") %>'></asp:Label>
                        <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemID") %>'></asp:Label>
                        <asp:Label ID="lblReasonID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReasonID") %>'></asp:Label>
                        <asp:Label ID="lblUnitID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitID") %>'></asp:Label>
                        <asp:Label ID="lblDiscountID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DiscountID") %>'></asp:Label>
                        <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GSTID") %>'></asp:Label>
                        <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.HistoryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SlNo" HeaderText="Sl.No" HeaderStyle-Width="2%">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="Commodity" HeaderText="Commodity" HeaderStyle-Width="10%">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>
                <asp:BoundField DataField="Goods" HeaderText="Goods" HeaderStyle-Width="10%">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Unit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblQTY" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.QTY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Rate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblAmt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RReturn" HeaderText="Reason For Return" HeaderStyle-Width="7%">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="Discount" HeaderText="Discount" HeaderStyle-Width="6%">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Total Discount" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblTotDiscount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotDiscount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Charges" HeaderStyle-Width="6%" Visible ="false" >
                    <ItemTemplate>
                        <asp:Label ID="lblCharges" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Charges") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Rate %" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblGst" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Gst") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Amount" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GSTAmt") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="7%">
                    <ItemTemplate>
                        <asp:Label ID="lblTotAmt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotAmt") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="GrdEdit" runat="server" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalSalesValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblSalesValidationMsg" runat="server"></asp:Label></strong>
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
    <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" Visible="false" ID="txtGSTID"></asp:TextBox>
        <asp:TextBox Width="50px" runat="server" autocomplete="off" ID="txtAmt" Style="visibility: hidden;"></asp:TextBox>
        <asp:TextBox Width="50px" runat="server" autocomplete="off" ID="txtSAmt" Style="visibility: hidden;"></asp:TextBox>

        <asp:TextBox ID ="txtAvailableQty" runat ="server" Visible ="false" ></asp:TextBox>
        
    </div>

     <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
            <asp:Label runat="server" Text="* Charges" Visible ="false" ></asp:Label>
            <asp:TextBox ID="txtCharges" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1" Visible ="false"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCharges" runat="server" ControlToValidate="txtCharges" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter Charges." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCharges" runat="server" ControlToValidate="txtCharges" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
        </div>

    
</asp:Content>

