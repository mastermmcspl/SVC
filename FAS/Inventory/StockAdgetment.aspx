<%@ Page Title="" Language="VB" MasterPageFile="~/Inventory.master" AutoEventWireup="false" CodeFile="StockAdgetment.aspx.vb" Inherits="Inventory_StockAdgetment" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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


        function validate() {

            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert("Enter Quantity Field")
                document.getElementById('<%=txtQuantity.ClientID %>').focus()
                return false;
            }
            if (document.getElementById('<%=txtRate.ClientID %>').value == "") {
                alert("Enter Rate Field")
                document.getElementById('<%=txtRate.ClientID %>').focus()
                return false;
            }

            return true
        }

        function Calculate() {
            var avlblqty = document.getElementById('<%=txteQty.ClientID %>').value
            var entrdQty = document.getElementById('<%=txtQuantity.ClientID %>').value
            var values = document.getElementById('<%=txtRate.ClientID %>').value
            var RateAmount = 0
            var ajstdValue = 0
            var ajstdQty = 0
            if (parseFloat(avlblqty) > parseFloat(entrdQty)) {
                ajstdQty = avlblqty - entrdQty
                ajstdValue = values * ajstdQty
                RateAmount = entrdQty * values
                document.getElementById('<%=txtadjusted.ClientID %>').value = ajstdQty.toFixed(2)
              <%--  document.getElementById('<%=hfadjusted.ClientID %>').value = ajstdQty.toFixed(2)
               --%>
                document.getElementById('<%=txtadjustedamount.ClientID %>').value = ajstdValue.toFixed(2)
                document.getElementById('<%=hfadjustedAmount.ClientID %>').value = ajstdValue.toFixed(2)
                document.getElementById('<%=txtRateAmount.ClientID %>').value = RateAmount.toFixed(2)
                document.getElementById('<%=hfRateAmount.ClientID %>').value = RateAmount.toFixed(2)
                document.getElementById('<%=lblFlag.ClientID %>').innerText = "Negative"
                ajstdQty = 0
                return false;
            } else if (parseFloat(avlblqty) < parseFloat(entrdQty)) {
                ajstdQty = entrdQty - avlblqty
                ajstdValue = values * ajstdQty
                RateAmount = entrdQty * values
                document.getElementById('<%=txtadjusted.ClientID %>').value = ajstdQty.toFixed(2)
                document.getElementById('<%=txtadjustedamount.ClientID %>').value = ajstdValue.toFixed(2)
                document.getElementById('<%=hfadjustedAmount.ClientID %>').value = ajstdValue.toFixed(2)
                document.getElementById('<%=txtRateAmount.ClientID %>').value = RateAmount.toFixed(2)
                document.getElementById('<%=hfRateAmount.ClientID %>').value = RateAmount.toFixed(2)
                document.getElementById('<%=lblFlag.ClientID %>').innerText = "Positive"
                ajstdQty = 0
                return false;
            }
            else {
                document.getElementById('<%=txtadjusted.ClientID %>').value = "0"
                return false
            }
        return true
    }
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>5 Stock Adjustment</b></h2>
            </div>
            <div class="col-sm-3 col-md-3" style="left: 0px; top: 0px">
                <div class="pull-right">
                    <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" CausesValidation="false" />
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
            <asp:Label runat="server" Text="Existing Adjustment No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="240px" ID="ddlExistingOrder"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFEinwardNo" runat="server" ControlToValidate="ddlExistingOrder" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Inward No" ValidationGroup="ValidateInward"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:Label runat="server" Text="Adjustment No."></asp:Label>
            <asp:TextBox ID="txtAdjustedCode" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
            </asp:TextBox>
        </div>
        <div class="col-sm-6 col-md-6 pull-right">
            <asp:Label runat="server" Text="Date"></asp:Label>
            <asp:TextBox ID="txtadjustedDate" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
            </asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtadjustedDate" PopupPosition="BottomLeft"
                TargetControlID="txtadjustedDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

        <div class="col-sm-6 col-md-6">
            <asp:Label runat="server" Text="Reason"></asp:Label>
            <asp:TextBox ID="txtReason" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
            </asp:TextBox>
        </div>
        <div class="col-sm-6 col-md-6">
            <asp:Label runat="server" Text="Brand"></asp:Label>
            <asp:DropDownList runat="server" autocomplete="off" CssClass="aspxcontrols" ID="ddlCommodity" AutoPostBack="True"></asp:DropDownList>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <legend class="legendbold">Details of Stock Adjustment</legend>
        </fieldset>
        <div class="col-sm-6 col-md-6" style="padding-left: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Label runat="server" Text="* Description of Goods"></asp:Label>
                    <asp:TextBox autocomplete="off" ID="txtsearch" runat="server" CssClass="aspxcontrols" placeholder="Search By Item" data-toggle="tooltip" data-placement="top" onkeyup="FilterItems(this.value)"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <asp:ListBox ID="chkCategory" CssClass="col-sm-12 col-md-12 pre-scrollableborder" runat="server" AutoPostBack="True" Height="180px"></asp:ListBox>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Existing Quantity"></asp:Label>
                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txteQty" Enabled="False"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="* Existing Rate"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtEValue" Enabled="False"></asp:TextBox>
                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
                </div>
            </div>


            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlUnit"></asp:DropDownList>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Rate"></asp:Label>
                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtrate"></asp:TextBox>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtQuantity"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Rate"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtRateAmount" Enabled="False"></asp:TextBox>
                    <asp:HiddenField ID="hfRateAmount" runat="server" />
                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RefRate" runat="server" ControlToValidate="txtRate"  Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RequiredFieldValidator>--%>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Adjusted Quantity"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtadjusted" ValidationGroup="ValidateQty"></asp:TextBox>
                    <%--  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateQty"></asp:RegularExpressionValidator>--%>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <asp:Label runat="server" Text="Quantity Amount"></asp:Label>
                    <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtadjustedamount"></asp:TextBox>
                    <asp:HiddenField ID="hfadjustedAmount" runat="server" />
                </div>
            </div>
            <%--         <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <asp:Label ID="lblFlag" runat="server" Text="Label"></asp:Label>
                </div>--%>
        </div>
        <asp:Label ID="lblFlag" runat="server" Text="Label" Visible="false"></asp:Label>
        <div class="col-sm-12 col-md-12" style="padding-right: 0px">
            <asp:GridView ID="dgStockAdgestment" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="SLNO" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblSLNO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SLNO") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Goods" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Units" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblUnits" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Units") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RateAmount" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblRateAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RateAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DiscountAmt" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblDiscountAmt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VAT" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblVAT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VAT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="VATAmt" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblVATAmt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VATAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CST" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CST") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CSTAmount" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CSTAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TotalAmount" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="Edit" runat="server" CssClass="hvr-bounce-in" Visible="false" />
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
    <asp:Label ID="lblQty" runat="server"></asp:Label>
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
    <asp:Label ID="lblValue" runat="server"></asp:Label>

</asp:Content>
