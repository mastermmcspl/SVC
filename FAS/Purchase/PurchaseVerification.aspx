<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Purchase.master" CodeFile="PurchaseVerification.aspx.vb" Inherits="Purchase_PurchaseVerification" ValidateRequest="false" %>

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
            $('#<%=dgViewPI.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgViewExcess.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });

            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgViewPR.ClientID%>').DataTable({
            iDisplayLength: -1,
            aLengthMenu: [[-1], ["All"]],
            order: [],
            columnDefs: [{ orderable: false, targets: [0] }],
        });
        });

    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
        $('#<%=ddlInvoiceNo.ClientID%>').select2();
});
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Purchase Verification</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" Visible="false" />
                    <%-- <asp:ImageButton ID="ImageButton2" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />--%>
                    <%-- <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />--%>
                    <%-- <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />--%>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Existing Invoice No."></asp:Label>
            <asp:DropDownList ID="ddlInvoiceNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order No."></asp:Label>
            <asp:DropDownList ID="ddlExistingTransactions" runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable "></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Order Date"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtOrderDate"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label runat="server" ID="lblStatus" Text="Status :-"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Purchase Register No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable " ID="ddlGoodsInwardNo"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Goods Receipt Date"></asp:Label>
            <asp:TextBox runat="server" ID="txtReceiptDate" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Supplier Name"></asp:Label>
            <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="true" CssClass="aspxcontrolsdisable "></asp:DropDownList>
        </div>
        <%-- <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Payment Mode"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlPaymentMode" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Method of Shipping"></asp:Label>
            <asp:TextBox runat="server" ID="txtMShiping" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>--%>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Verification No." Visible="false"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlPurchaseInvoice" Visible="false"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding-right: 0px; overflow: auto">
        <asp:GridView ID="dgViewPI" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="CommodityID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HistoryID" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" HeaderStyle-Width="20%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblStdUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StdUnit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExpectedDate" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblExpectedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExpectedDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RateAmount" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRateAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RateAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Charges" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblCharges" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Charges") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTID" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTRate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FinalTotal" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblFinalTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FinalTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-right: 0px"></div>
    <div class="col-sm-12 col-md-12" style="padding-right: 0px">
        <asp:GridView ID="dgViewPR" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="CommodityID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HistoryID" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" HeaderStyle-Width="20%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblStdUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StdUnit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExpectedDate" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblExpectedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExpectedDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RateAmount" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRateAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RateAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Charges" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblCharges" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Charges") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTID" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTRate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FinalTotal" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblFinalTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FinalTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-right: 0px"></div>
    <div class="col-sm-12 col-md-12" style="padding-right: 0px"></div>
    <div class="col-sm-12 col-md-12" style="padding-right: 0px">
        <asp:GridView ID="dgViewExcess" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="CommodityID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="HistoryID" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" HeaderStyle-Width="20%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblStdUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StdUnit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ExpectedDate" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblExpectedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExpectedDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RateAmount" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRateAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RateAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Discount Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblDiscountAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Charges" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblCharges" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Charges") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTID" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GSTRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTRate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FinalTotal" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblFinalTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FinalTotal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
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

</asp:Content>
