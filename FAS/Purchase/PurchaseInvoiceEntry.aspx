<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Purchase.master" CodeFile="PurchaseInvoiceEntry.aspx.vb" Inherits="Purchase_PurchaseInvoiceEntry" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            $('#<%=dgPurchaseRegistry.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });
        $(document).ready(function () {
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
                $('#<%=ddlExistRegisrtry.ClientID%>').select2();
            });
            $('[data-toggle="tooltip"]').tooltip();
        });
        $(document).ready(function () {
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
                $('#<%=ddlOrderNo.ClientID%>').select2();
            });
            $('[data-toggle="tooltip"]').tooltip();
        });

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


        function ValidateStringOrNot(Received, Rejected, Excess, Accept) {
            document.getElementById(Rejected).value = ""
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
            document.getElementById(SRejectedQty).value = sqty
            document.getElementById(sRjctdExcess).value = seqtz
            return true;
        }
    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Purchase Register</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <%--                    <button type="button" data-toggle="modal" data-target="#myModal"><img class="activeIcons hvr-bounce-out" src="../Images/Add24.png" /></button>--%>
                    <%--<asp:ImageButton ID="imgbtnNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />--%>
                    <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblStatus" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Registry No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style1" Width="240px" ID="ddlExistRegisrtry"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3" style="left: 0px; top: 0px">
            <asp:Label runat="server" Text="Registry  No."></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtPrchseRegNo"></asp:TextBox>
            <%-- <div class="pull-left">
                <br />
                <asp:CheckBox runat="server" Font-Size="12px" CssClass="pull-right" Text="Oral/Counter Order" />
            </div>--%>
        </div>
        <%-- <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label runat="server" Text="Status :-"></asp:Label>
        </div>--%>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Order No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlOrderNo"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Order Date"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtOrderDate"></asp:TextBox>

        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Invoice Reference No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style2" ID="ddlDocNo"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Delivery challan No"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtDcNo"></asp:TextBox>
        </div>


    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Reference No."></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtDocRefNo"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice Date"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtInvoiceDate" placeholder="DD/MM/YY"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtInvoiceDate" PopupPosition="BottomLeft"
                TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="E-Sugam No."></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtEsugam"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlSupplier"></asp:DropDownList>
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
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
        <asp:GridView ID="dgPurchaseRegistry" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
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
                        <asp:TextBox ID="txtMrp" Enabled="false" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mrp") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ordered Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblOrderQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderedQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Batch No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtBatchNumber" Enabled="false" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BatchNumber") %>'></asp:TextBox>
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
                        <asp:TextBox ID="txtMdate" Enabled="false" CssClass="aspxcontrols" placeholder="DD/MM/YY" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MDate") %>'></asp:TextBox>
                        <cc1:CalendarExtender ID="ccMdate" runat="server" PopupButtonID="txtMdate" PopupPosition="BottomLeft"
                            TargetControlID="txtMdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expire Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtEdate" Enabled="false" CssClass="aspxcontrols" runat="server" placeholder="DD/MM/YY" Text='<%# DataBinder.Eval(Container.DataItem, "EDate") %>'></asp:TextBox>
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
                    <button type="button" class="close" data-dismiss="modal">
                        &times;tton>
                    <h4 class="modal-title">Extra Item Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Brand"></asp:Label>
                                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group" style="margin-bottom: 15px">
                                <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                            </div>
                            <div class="col-sm-12 col-md-12 pre-scrollableborder" style="height: 270px">
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <asp:Label runat="server" Text="* Rate"></asp:Label>
                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                    <br />
                                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                    <br />
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <asp:Label runat="server" Text="Excise Duty"></asp:Label>
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                    <br />
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <asp:Label runat="server" Text="VAT"></asp:Label>
                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                    <br />
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <asp:Label runat="server" Text="CST"></asp:Label>
                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                    <br />
                                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <asp:Label runat="server" Text="Total Amount"></asp:Label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class=" btn-ok" data-dismiss="modal">New</button>
                    <button type="button" class=" btn-ok" data-dismiss="modal">Add</button>
                    <button type="button" class=" btn-ok" data-dismiss="modal">Close</button>
                </div>
            </div>
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
