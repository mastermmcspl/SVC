<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="PurchaseSalesJEDetails.aspx.vb" Inherits="Accounts_PurchaseSalesJEDetails" %>

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
      <%-- $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=ddlBillNo.ClientID%>').multiselect({
            includeSelectAllOption: true,
            allSelectedText: 'No option left ...',
            enableFiltering: true,
            filterPlaceholder: 'Search...',              
        });

        });--%>

        function CalculateBalance() {
            if (document.getElementById('<%=txtBillAmount.ClientID %>').value != "") {
                if (document.getElementById('<%=txtAdvancePayment.ClientID %>').value != "") {

                    var sBIllAmount = document.getElementById('<%=txtBillAmount.ClientID %>').value
                    var sAdvanceAmount = document.getElementById('<%=txtAdvancePayment.ClientID %>').value

                    document.getElementById('<%=txtBalanceAmount.ClientID %>').value = sBIllAmount - sAdvanceAmount
                }
            }
        }

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlParty.ClientID%>').select2();
            $('#<%=ddlLocation.ClientID%>').select2();
            $('#<%=ddlBillType.ClientID%>').select2();
            $('#<%=ddlBillNo.ClientID%>').select2();
            $('#<%=ddlPaymentType.ClientID%>').select2();
            $('#<%=ddldbHead.ClientID%>').select2();
            $('#<%=ddldbGL.ClientID%>').select2();
            $('#<%=ddldbsUbGL.ClientID%>').select2();
            $('#<%=ddlCrHead.ClientID%>').select2();
            $('#<%=ddlCrGL.ClientID%>').select2();
            $('#<%=ddlCrSubGL.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>
                    <asp:Label ID="LabelHeading" runat="server"></asp:Label></b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="" CausesValidation="false" Visible="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="" ValidationGroup="Validate" Visible="false" />
                    <asp:ImageButton ID="imgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="ValidateApp" Visible="True" />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" visible="false" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnWord" Text="Download Word" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>
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

    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Existing Journal Voucher"></asp:Label>
                <asp:DropDownList ID="ddlExistJE" runat="server" CssClass="aspxcontrols" AutoPostBack="True" ValidationGroup="Validate"></asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3 pull-right">
                <br />
                <asp:Label ID="lblStatus" runat="server" Text="Status:- " Font-Bold="true"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Transaction No"></asp:Label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Select Customer/Supplier/Party"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVParty" runat="server" ControlToValidate="ddlParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Customer/Supplier/Party." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Location"></asp:Label>
                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <%-- <asp:Label runat="server" Text="Frequently Used GL"></asp:Label>
                <asp:DropDownList ID="ddlFrequently" runat="server" CssClass="aspxcontrols"></asp:DropDownList>--%>
                <asp:Label ID="lblTransID" runat="server" CssClass="aspxcontrols" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Bill No."></asp:Label>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillNo" runat="server" ControlToValidate="txtBillNo" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill No." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <%--<asp:TextBox ID="txtBillNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>--%>
                <%--<div class="form-group">
                 <asp:ListBox ID="ddlBillNo" runat="server"  Width="55%" AutoPostBack="true" SelectionMode="Multiple" CssClass="aspxcontrols"></asp:ListBox>
                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="aspxcontrols" Width="43%"></asp:TextBox>
                 </div>--%>
                <div class="form-group">
                    <asp:DropDownList ID="ddlBillNo" runat="server" Width="55%" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="aspxcontrols" Width="43%"></asp:TextBox>
                </div>
                <%--<asp:DropDownList ID="ddlBillNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>--%>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Bill Date"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REFBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtBillDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                <cc1:CalendarExtender ID="cclBillDate" runat="server" PopupButtonID="txtBillDate" PopupPosition="BottomLeft"
                    TargetControlID="txtBillDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%--<asp:RangeValidator ID="rgvtxtBillDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtBillDate" SetFocusOnError="True"></asp:RangeValidator>--%>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Bill Amount"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillAmouont" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill Amount." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEBillAmount" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtBillAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Payment Voucher Type" Visible="false"></asp:Label>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillType" runat="server" ControlToValidate="ddlBillType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Voucher Type." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList ID="ddlBillType" runat="server" CssClass="aspxcontrols" Visible="false"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Narration"></asp:Label>
                <asp:TextBox ID="txtNarration" runat="server" CssClass="aspxcontrols" Height="50px" Width="790px" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-12 col-md-12">
        <h4><asp:Label ID="Label1" Text ="Item Details" runat="server"></asp:Label></h4>
        </div>
        <div class="col-sm-12 col-md-12">
            <asp:DataGrid ID="dgItems" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" class="footable">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="PurchaseInvoiceNo" HeaderText="Purchase Invoice No">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="Item" HeaderText="Item">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="ItemCode" HeaderText="Item Code">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="ItemBasic" HeaderText="Item Basic Amt">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="GST" HeaderText="GST">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                </Columns>
            </asp:DataGrid>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <h4><asp:Label ID="Label2" Text="JE Details" runat="server"></asp:Label></h4>
    </div>
    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgJEDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable">
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
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="18%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" ImageUrl="~\Images\Trash16.png" CommandName="Delete" runat="server" Visible="false" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" Width="1%" />
                </asp:TemplateColumn>

                <asp:TemplateColumn Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" ImageUrl="~\Images\Edit16.png" runat="server" ToolTip="Edit" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>

            </Columns>
        </asp:DataGrid>
    </div>

    <!-- Cheque Details -->
    <div class="modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-md">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cheque Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12 pull-left">
                        <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Cheque No."></asp:Label>
                                <asp:TextBox ID="txtChequeNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Cheque Date"></asp:Label>
                                <asp:TextBox ID="txtChequeDate" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="IFSC Code"></asp:Label>
                                <asp:TextBox ID="txtIFSC" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Bank Name"></asp:Label>
                                <asp:TextBox ID="txtBankName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Branch Name"></asp:Label>
                                <asp:TextBox ID="txtBranchName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn-ok" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-ok" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn-ok" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <asp:Label ID="lblDbATRID" runat="server" Text="" Visible="false"></asp:Label>
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">
                    <asp:Label runat="server" Text="Debit Details" Visible="false"></asp:Label>
                </legend>
            </fieldset>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Head of Accounts" Visible="false"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDbHead" runat="server" ControlToValidate="ddldbHead" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Head of Accounts." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddldbHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                    <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                    <asp:ListItem Value="1">Asset</asp:ListItem>
                    <asp:ListItem Value="4">Liabilities</asp:ListItem>
                    <asp:ListItem Value="2">Income</asp:ListItem>
                    <asp:ListItem Value="3">Expenditure</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdbGL" runat="server" ControlToValidate="ddldbGL" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select General Ledger." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddldbGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false"></asp:DropDownList>
                <asp:TextBox ID="txtDBGLSearch" CssClass="aspxcontrols" runat="server" Width="200px" Visible="false"></asp:TextBox>
                <asp:ImageButton ID="imgbtnDBGLSearch" runat="server"  Visible="false" />
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddldbsUbGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false"></asp:DropDownList>
                <asp:TextBox ID="txtDBSubGLSearch" CssClass="aspxcontrols" runat="server" Width="200px" Visible="false"></asp:TextBox>
                <asp:ImageButton ID="imgbtnDBSubGLSearch" runat="server" Visible="false" />
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Debit Amount" Visible="false"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtDebitAmount" runat="server" CssClass="aspxcontrols" Width="70%" Visible="false"></asp:TextBox>
                    <asp:ImageButton ID="imgDebit" runat="server" Width="10%" Visible="false"></asp:ImageButton>
                </div>
            </div>

        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <asp:Label ID="lblCrATRID" runat="server" Text="" Visible="false"></asp:Label>
            <fieldset class="col-sm-12 col-md-12">
                <legend visible="false" class="legendbold">
                    <asp:Label runat="server" Text="Credit Details" Visible="false"></asp:Label>
                </legend>
            </fieldset>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Head of Accounts" Visible="false"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCrHead" runat="server" ControlToValidate="ddlCrHead" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Head of Accounts." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlCrHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                    <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                    <asp:ListItem Value="1">Asset</asp:ListItem>
                    <asp:ListItem Value="4">Liabilities</asp:ListItem>
                    <asp:ListItem Value="2">Income</asp:ListItem>
                    <asp:ListItem Value="3">Expenditure</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCrGL" runat="server" ControlToValidate="ddlCrGL" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select General Ledger." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlCrGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false"></asp:DropDownList>
                <asp:TextBox ID="txtCrGLSearch" CssClass="aspxcontrols" runat="server" Width="200px" Visible="false"></asp:TextBox>
                <asp:ImageButton ID="imgbtnCrGLSeach" runat="server"  Visible="false" />
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlCrSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false"></asp:DropDownList>
                <asp:TextBox ID="txtCrSubGLSearch" CssClass="aspxcontrols" runat="server" Width="200px" Visible="false"></asp:TextBox>
                <asp:ImageButton ID="imgbtnCrSubGLSearch" runat="server"  Visible="false" />
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Credit Amount." Visible="false"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="aspxcontrols" Width="70%" AutoPostBack="true" Visible="false"></asp:TextBox>
                    <asp:ImageButton ID="imgCredit" runat="server" Width="10%" Visible="false"></asp:ImageButton>
                </div>
            </div>
        </div>
    </div>

     <div visible="false" class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Visible="false" Text="* Journal Type"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPaymentType" runat="server" ControlToValidate="ddlPaymentType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Payment Type." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                    <%--<asp:ListItem Value="0">Journal Type</asp:ListItem>
                    <asp:ListItem Value="1">Advance Payment</asp:ListItem>
                    <asp:ListItem Value="3">Payment</asp:ListItem>
                    <asp:ListItem Value="4">Cheque Details</asp:ListItem>--%>
                </asp:DropDownList>
            </div>
            <div id="divAdvance" runat="server" class="col-sm-9 col-md-9" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblAdvance" Visible="false" runat="server" Text="* Advance Amount"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAdvance" runat="server" ControlToValidate="txtAdvancePayment" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Advance Amount." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAdvance" runat="server" ControlToValidate="txtAdvancePayment" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtAdvancePayment" Visible="false" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblBalance" Visible="false" runat="server" Text="Balance Amount"></asp:Label>
                    <asp:TextBox ID="txtBalanceAmount" Visible="false" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                </div>
            </div>
            <div id="divPayment" runat="server" class="col-sm-9 col-md-9" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Visible="false" Text="Net Amount"></asp:Label>
                    <asp:TextBox ID="txtNetAmount" Visible="false" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
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

    <asp:TextBox ID="txtStartDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtEndDate" runat="server" Visible="false"></asp:TextBox>

</asp:Content>

