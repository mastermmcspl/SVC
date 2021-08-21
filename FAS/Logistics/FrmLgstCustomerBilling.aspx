<%@ Page Title="" Language="VB" MasterPageFile="~/LogisticsMaster.master" AutoEventWireup="false" CodeFile="FrmLgstCustomerBilling.aspx.vb" Inherits="Logistics_FrmLgstCustomerBilling" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            $('#<%=ddlCustomers.ClientID%>').select2();
             $('#<%=ddlExistingInvoiceno.ClientID%>').select2();
             $('#<%=ddlRoute.ClientID%>').select2();
             $('#<%=grdCustAmountDetails.ClientID%>').DataTable({
                 iDisplayLength: -1,
                 aLengthMenu: [[-1], ["All"]],
                 order: [],
                 columnDefs: [{ orderable: false, targets: [0] }],
             });
         });
         function validatePage() {
             //Executes all the validation controls associated with group1 validaiton Group1. 
             var flag = window.Page_ClientValidate('Validate');
             if (flag)
                 return flag;

         }
    </script>

    <style>
        div {
            color: black;
        }
    </style>


    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Customer Billing Details</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="LabelError"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Invoice No."></asp:Label>
            <asp:DropDownList ID="ddlExistingInvoiceno" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label1" runat="server" Text="* Invoice No"></asp:Label>
            <asp:TextBox ID="txtInvNo" runat="server" CssClass="aspxcontrols" autocomplete="off" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label2" runat="server" Text="* Invoice Date"></asp:Label>
            <asp:TextBox ID="txtInvDate" runat="server" CssClass="aspxcontrols" placeholder="DD/MM/YY" autocomplete="off"></asp:TextBox>
            <cc1:CalendarExtender ID="cclInvDate" runat="server" PopupButtonID="txtInvDate" PopupPosition="BottomLeft" TargetControlID="txtInvDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <div class="form-group" runat="server">
                <asp:Label runat="server" Text="Status :-"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Customers"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCustomers"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomers" runat="server" ControlToValidate="ddlCustomers" Display="Dynamic" ErrorMessage="Select Customer" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Route"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlRoute"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCustomers" Display="Dynamic" ErrorMessage="Select Customer" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label4" runat="server" Text="* Aggreement"></asp:Label>
            <asp:TextBox ID="txtAggreement" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label3" runat="server" Text="* Customer's order ref"></asp:Label>
            <asp:TextBox ID="txtCustOrderRef" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
        </div>

    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label8" runat="server" Text="Company Address"></asp:Label>
            <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label5" runat="server" Text="Company GSTN RegNo"></asp:Label>
            <asp:TextBox ID="txtCompanyGSTN" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label6" runat="server" Text="Service Customer Address"></asp:Label>
            <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label7" runat="server" Text="Service Customer GSTN RegNo"></asp:Label>
            <asp:TextBox ID="txtCustGSTN" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>

    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label10" runat="server" Text="GST Rate Category"></asp:Label>
            <asp:TextBox ID="txtCustGstnCategory" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
            <asp:TextBox ID="txtCustGstnCategoryId" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblFromdate" runat="server" Text="* From Date"></asp:Label>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtFromDate" PopupPosition="BottomLeft" TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFromDate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic" ErrorMessage="Select From Date" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTodate" runat="server" Text="* To Date"></asp:Label>
            <asp:TextBox ID="txtToDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="cclToDate" runat="server" PopupButtonID="txtToDate" PopupPosition="BottomLeft" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVToDate" runat="server" ControlToValidate="txtToDate" Display="Dynamic" ErrorMessage="Select to Date" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblExc" runat="server" Text=" Escallation Amount"></asp:Label>
            <asp:TextBox ID="txtEscAmt" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
        </div>
    </div>
      <div class="col-sm-12 col-md-12 form-group pull-right" style="padding: 0px">
           <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblApprvdBy" runat="server" Text="Escallated Amount Approved By"></asp:Label>
            <asp:TextBox ID="txtApprvdBy" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
        </div>
                 <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblApprvdOn" runat="server" Text="Escallated Amount Approved On"></asp:Label>
            <asp:TextBox ID="txtApprvdOn" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
        </div>
              <div class="col-sm-1 col-md-1">
            <br />
             <asp:Button ID="btnGo" runat="server" Text="GO" Font-Bold="true" />
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
   <div class="col-sm-3 col-md-3">
           <asp:TextBox ID="txtTotEscAmt" runat="server" CssClass="aspxcontrols" Visible="false" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTotal" runat="server" Text="Total Amount" Visible="false"></asp:Label>
            <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="aspxcontrols" Visible="false" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label9" runat="server" Text="GST Rate" Visible="false"></asp:Label>
            <asp:TextBox ID="txtCustGSTRate" runat="server" CssClass="aspxcontrols" Visible="false" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>

    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:GridView ID="grdCustAmountDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                   <asp:TemplateField HeaderText=" Trip Amount" Visible="True" HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TripAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Escallated Amount" Visible="True" HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="lblEscAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalEscAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Trip Amount" Visible="True" HeaderStyle-Width="20%">
                    <ItemTemplate>
                        <asp:Label ID="lblTripAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalTripAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Rate" Visible="false" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTRate") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GST Amount" Visible="True" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GSTAmount") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST" Visible="true" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblSGST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SGST") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SGST Amount" Visible="true" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblSGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SGSTAmount") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGST" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblCGST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CGST") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGST Amount" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblCGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CGSTAmount") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblIGST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IGST") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IGST Amount" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txtplacedqty" CssClass="aspxcontrols" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlacedQuantity") %>' ReadOnly="false"></asp:TextBox>--%>
                        <asp:Label ID="lblIGSTAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IGSTAmount") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Grand Total" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblGrandTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GrandTotal") %>'></asp:Label>

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="">
        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
    </div>

    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgTipDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable" Visible="true">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SrNo" HeaderText="Sr No" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TripNo" HeaderText="Trip No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="11%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
       <asp:BoundColumn DataField="ContractAmount" HeaderText="Trip Rate">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                             <asp:BoundColumn DataField="TripRateAmount" HeaderText="Escallated Rate">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                        <asp:BoundColumn DataField="EscAmt" HeaderText="Escallation Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="12%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>         
                <asp:BoundColumn DataField="GSTRate" HeaderText="GST Rate">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                      <asp:BoundColumn DataField="SGST" HeaderText="SGST">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SGSTAmount" HeaderText="SGST Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CGST" HeaderText="CGST">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CGSTAmount" HeaderText="CGST Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IGST" HeaderText="IGST">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="IGSTAmount" HeaderText="IGST Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                       <asp:BoundColumn DataField="GSTAmount" HeaderText="GST Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="18%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
         
                        <asp:BoundColumn DataField="GrandTotal" HeaderText="Total Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>

    <div id="ModalFASDriverValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCustBillingValidationMsg" runat="server"></asp:Label>
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
        <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgINVDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable" Visible="false">
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
    <asp:TextBox ID="txtGLID" runat="server" Visible="false"></asp:TextBox>
</asp:Content>

