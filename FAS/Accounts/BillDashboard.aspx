<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="BillDashboard.aspx.vb" Inherits="Accounts_BillDashboard" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            margin-top: 0px;
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
            $('#<%=dgBD.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
             $('#<%=dgPurchase.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('#<%=dgInward.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('#<%=dgViewPI.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
        <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>1.5 Bill Dashboard</b></h2>
            </div>

            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                    <%--<asp:ImageButton ID="imgbtnReport" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Report" />   --%>
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
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
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="dgBD" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                        <asp:Label ID="lblPOID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "POID") %>'></asp:Label>
                        <asp:Label ID="lblGNID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GNID") %>'></asp:Label>
                        <asp:Label ID="lblINID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SLNo" HeaderText="SLNo" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:TemplateField HeaderText="Purchase Order" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPO" Font-Italic="true" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PO") %>' CommandName="ShowPO">LinkButton</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GinNumber" HeaderStyle-Width="15%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkGinNumber" Font-Italic="true" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GinNumber") %>' CommandName="ShowGIN">LinkButton</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Rejection" HeaderText="Rejection" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:TemplateField HeaderText="Invoice No" HeaderStyle-Width="20%">
                    <ItemTemplate>                        
                        <asp:LinkButton ID="lnkInvoiceNo" Font-Italic="true" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo") %>' CommandName="ShowIN">LinkButton</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Advance" HeaderText="Advance" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="10%"></asp:BoundField>
                <%--<asp:BoundField DataField="PaidNot" HeaderText="Paid/Not" HeaderStyle-Width="10%"></asp:BoundField>--%>
                <%--<asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" ImageUrl="~/Images/Edit16.png" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>        
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
        <asp:GridView ID="dgPurchase" runat="server" AutoGenerateColumns="False" class="footable">
            <Columns>
                <asp:BoundField DataField="SLNO" HeaderText="SLno" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                <asp:BoundField DataField="Commodity" HeaderText="Commodity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="25%"></asp:BoundField>
                <asp:BoundField DataField="Goods" HeaderText="Goods" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="25%"></asp:BoundField>
                <asp:BoundField DataField="Units" HeaderText="Units" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="8%"></asp:BoundField>
                <asp:BoundField DataField="Rate" HeaderText="Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="RateAmount" HeaderText="RateAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Discount" HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="DiscountAmt" HeaderText="DiscountAmt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="GSTRate" HeaderText="GSTRate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="GSTAmount" HeaderText="GSTAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="TotalAmount" HeaderText="TotalAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="dgInward" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>   
                <asp:BoundField DataField="SLNO" HeaderText="SLNo" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="2%"></asp:BoundField>   
                <asp:BoundField DataField="Comodity" HeaderText="Brand" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="15%"></asp:BoundField>           
                <asp:BoundField DataField="Descriptions" HeaderText="Description" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="15%"></asp:BoundField>  
                <asp:BoundField DataField="Units" HeaderText="Units" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="10%"></asp:BoundField>  
                <asp:BoundField DataField="Mrp" HeaderText="MRP" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="8%"></asp:BoundField>
                <asp:BoundField DataField="OrderedQty" HeaderText="Ordered Quantity" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="ReceivedQty" HeaderText="Received Quantity" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="AccpetedQty" HeaderText="Accepted Quantity" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="RejectedQty" HeaderText="Rejected Quantity" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="ExcessQty" HeaderText="Excess Quantity" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"  HeaderStyle-Width="10%"></asp:BoundField>            
            </Columns>
        </asp:GridView>
        <asp:GridView ID="dgViewPI" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:BoundField DataField="SLNO" HeaderText="SLNo" HeaderStyle-Width="2%"></asp:BoundField>
                <asp:BoundField DataField="Commodity" HeaderText="Commodity" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Goods" HeaderText="Description" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Unit" HeaderText="Unit" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Rate" HeaderText="Rate" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="RateAmount" HeaderText="Total" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Discount" HeaderText="Discount" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="DiscountAmount" HeaderText="Discount Amount" HeaderStyle-Width="13%"></asp:BoundField>
                <asp:BoundField DataField="Charges" HeaderText="Charges" HeaderStyle-Width="5%"></asp:BoundField>  
                <asp:BoundField DataField="TotalAmount" HeaderText="Amount" HeaderStyle-Width="5%"></asp:BoundField>  
                <asp:BoundField DataField="GSTRate" HeaderText="GST Rate" HeaderStyle-Width="5%"></asp:BoundField>  
                <asp:BoundField DataField="GSTAmount" HeaderText="GST Amt" HeaderStyle-Width="5%"></asp:BoundField>  
                <asp:BoundField DataField="FinalTotal" HeaderText="Total" HeaderStyle-Width="10%"></asp:BoundField>             
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalPaymentValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblPaymentMasterValidationMsg" runat="server"></asp:Label></strong>
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
    </span></a>
</asp:Content>

