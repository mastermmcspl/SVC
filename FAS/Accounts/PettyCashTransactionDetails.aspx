<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Accounts.master" CodeFile="PettyCashTransactionDetails.aspx.vb" Inherits="Accounts_PettyCashTransactionDetails" ValidateRequest="false" %>

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
    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlExisting.ClientID%>').select2();
            $('#<%=ddlParty.ClientID%>').select2();
            $('#<%=ddlCustomerParty.ClientID%>').select2();
            $('#<%=ddlBillType.ClientID%>').select2();
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

        <%-- function CheckAdvancePayment() {
            if (document.getElementById('<%=txtBillAmount.ClientID %>').value != "") {
                if (document.getElementById('<%=txtAdvancePayment.ClientID %>').value != "") {

                    var sBIllAmount = document.getElementById('<%=txtBillAmount.ClientID %>').value
                    var sAdvanceAmount = document.getElementById('<%=txtAdvancePayment.ClientID %>').value
                    var sBalanceAmount = sBIllAmount - sAdvanceAmount
                    if (sBalanceAmount <= 0) {
                        alert("Advance amount alway less than Bill Amount")
                        return false;
                    }
                    else {
                        document.getElementById('<%=txtBalanceAmount.ClientID %>').value = sBIllAmount - sAdvanceAmount
                    }
                }
            }
        }--%>
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Petty Cash Transaction Details</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
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

    <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Existing Petty Cash Voucher"></asp:Label>
                <asp:DropDownList ID="ddlExisting" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Date Of Payment"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Date Of Payment." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                <cc1:CalendarExtender ID="ccInvoiceDate" runat="server" PopupButtonID="txtInvoiceDate" PopupPosition="BottomLeft" TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%-- <asp:RangeValidator ID="rgvtxtInvoiceDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"></asp:RangeValidator>--%>
            </div>
            <div class="col-sm-3 col-md-3 pull-right">
                <asp:Label runat="server" Text="Status:- " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Transaction No"></asp:Label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            </div>


            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Customer/Supplier/GL"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomerParty" runat="server" ControlToValidate="ddlCustomerParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Customer/Supplier/GL." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlCustomerParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblParty" Text="* Customer" runat="server"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVParty" runat="server" ControlToValidate="ddlParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Customer/Supplier/Party." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>


            <div class="col-sm-3 col-md-3">
                <%--<asp:Label runat="server" Text="Frequently Used GL"></asp:Label>
                <asp:DropDownList ID="ddlFrequently" runat="server" CssClass="aspxcontrols"></asp:DropDownList>--%>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Bill Type"></asp:Label>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillType" runat="server" ControlToValidate="ddlBillType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Voucher Type." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList ID="ddlBillType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Bill No."></asp:Label>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillNo" runat="server" ControlToValidate="txtBillNo" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill No." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:TextBox ID="txtBillNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Bill Date"></asp:Label>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REFBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtBillDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                <cc1:CalendarExtender ID="cclBillDate" runat="server" PopupButtonID="txtBillDate" PopupPosition="BottomLeft"
                    TargetControlID="txtBillDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%--<asp:RangeValidator ID="rgvtxtBillDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtBillDate" SetFocusOnError="True"></asp:RangeValidator>--%>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Bill Amount"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillAmouont" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Bill Amount." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEBillAmount" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtBillAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
            <div class="col-sm-1 col-md-1">
                <br />
                <asp:ImageButton ID="imgbtnAddBillAmt" runat="server"></asp:ImageButton>
            </div>
        </div>


        <div class="col-sm-12 col-md-12">
            <asp:DataGrid ID="dgPetty" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="PettyID" HeaderText="PettyID" Visible="False">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="BillNo" HeaderText="BillNo">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="BillDate" HeaderText="BillDate">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:BoundColumn DataField="BillAmount" HeaderText="BillAmount">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>

                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" ImageUrl="~/Images/Trash16.png" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>

        <%-- <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
             <div class="col-sm-3 col-md-3">
                 <asp:Label runat="server" Text="Narration"></asp:Label>              
                <asp:TextBox ID="txtnarration" runat="server" TextMode ="MultiLine" Height ="80px" Width ="100px" CssClass="aspxcontrols"></asp:TextBox>
             </div>
         </div>--%>

        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <fieldset class="col-sm-12 col-md-12">
                    <legend class="legendbold">Debit Details</legend>
                </fieldset>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDbHead" runat="server" ControlToValidate="ddldbHead" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Select Head of Accounts." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddldbHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                        <asp:ListItem Value="1">Asset</asp:ListItem>
                        <asp:ListItem Value="2">Income</asp:ListItem>
                        <asp:ListItem Value="3">Expenditure</asp:ListItem>
                        <asp:ListItem Value="4">Liabilities</asp:ListItem>

                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                    <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdbGL" runat="server" ControlToValidate="ddldbGL" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Select General Ledger." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                    <asp:DropDownList ID="ddldbGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="140px"></asp:DropDownList>
                    <asp:ImageButton ID="imgbtnDrOtherGL" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Sub General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddldbsUbGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    <asp:ImageButton ID="imgbtnDrOtherSubGL" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                </div>

                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Debit Amount"></asp:Label>
                    <div class="form-group">
                        <asp:TextBox ID="txtDebitAmount" runat="server" CssClass="aspxcontrols" Width="70%"></asp:TextBox>
                        <asp:ImageButton ID="imgDebit" runat="server" Width="10%"></asp:ImageButton>
                    </div>
                </div>

            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <fieldset class="col-sm-12 col-md-12">
                    <legend class="legendbold">Credit Details</legend>
                </fieldset>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCrHead" runat="server" ControlToValidate="ddlCrHead" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Select Head of Accounts." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlCrHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                        <asp:ListItem Value="1">Asset</asp:ListItem>
                        <asp:ListItem Value="2">Income</asp:ListItem>
                        <asp:ListItem Value="3">Expenditure</asp:ListItem>
                        <asp:ListItem Value="4">Liabilities</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                    <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCrGL" runat="server" ControlToValidate="ddlCrGL" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Select General Ledger." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                    <asp:DropDownList ID="ddlCrGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="140px"></asp:DropDownList>
                    <asp:ImageButton ID="imgbtnCrOtherGL" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Sub General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddlCrSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="140px"></asp:DropDownList>
                    <asp:ImageButton ID="imgbtnCrOtherSGL" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                </div>

                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Credit Amount"></asp:Label>
                    <div class="form-group">
                        <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="aspxcontrols" Width="70%"></asp:TextBox>
                        <asp:ImageButton ID="imgCredit" runat="server" Width="10%"></asp:ImageButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Narration"></asp:Label>
            <asp:TextBox ID="txtNarration" runat="server" CssClass="aspxcontrols" Height="100px" Width="525px" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgPettyCashDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <%--  <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

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

                <%--   <asp:BoundColumn DataField="PaymentID" HeaderText="PaymentID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

                <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <%--    <asp:BoundColumn DataField="Type" HeaderText="Type">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="11%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
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

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>

            </Columns>
        </asp:DataGrid>
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


    <div id="myModalAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtfile" runat="server" CssClass="btn-ok" Width="95%" AllowMultiple="true" />
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                    <asp:Button ID="btnScan" runat="server" Text="Scan" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                        Visible="false" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                        runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12 form-group">
        <div id="divcollapseAttachments" runat="server" visible="false" data-toggle="collapse" data-target="#collapseAttachments"><a href="#"><b><i>Click here to view Attachments...</i></b></a></div>
    </div>
    <div id="collapseAttachments" class="col-sm-12 col-md-12 collapse form-group" style="max-height: 138px; padding: 0px">

        <div class="col-sm-6 col-md-6" style="max-height: 138px; padding-left: 0px; padding-right: 0px;">
            <asp:DataGrid ID="dgAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="footable" OnRowDataBound="PickColor_RowDataBound">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                    </asp:BoundColumn>

                    <asp:TemplateColumn HeaderText="File Name">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="28%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                            <asp:Label ID="lblExt" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Ext") %>'></asp:Label>
                            <asp:Label ID="lblFile" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:Label>
                            <asp:LinkButton ID="File" CommandName="OPENPAGE" Font-Italic="true" runat="server" Visible="false" Font-Bold="False" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Created">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <b>By:-</b>
                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <b>On:-</b>
                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnView" data-toggle="tooltip" data-placement="bottom" title="View" runat="server" CommandName="VIEW" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Add Description" CommandName="ADDDESC" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDownload" data-toggle="tooltip" data-placement="bottom" title="DownLoad" CommandName="OPENPAGE" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div class="col-sm-6 col-md-6">
            <asp:Image ID="imgView" runat="server" Width="500px" Height="400px" />
        </div>
    </div>

    <asp:TextBox ID="txtStartDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtEndDate" runat="server" Visible="false"></asp:TextBox>

    <div class=" modal fade" id="myAttchment" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Attachment</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblTax" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-5 col-md-5" style="padding-left: 0px">
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="95%" CssClass="btn-ok" AllowMultiple="true" />
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button ID="btnAttch" runat="server" Text="Add" CssClass="btn-ok" />
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button ID="btnIndex" runat="server" Text="Index" CssClass="btn-ok" />
                        </div>
                    </div>


                    <div class="col-sm-12 col-md-12" runat="server">
                        <asp:GridView ID="gvattach" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="1%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPath" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                                <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                </div>

            </div>
        </div>
    </div>
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">General Ledger Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblErrorGl" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Head"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCrHead" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Select Head of Accounts." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                        <asp:ListItem Value="1">Asset</asp:ListItem>
                        <asp:ListItem Value="2">Income</asp:ListItem>
                        <asp:ListItem Value="3">Expenditure</asp:ListItem>
                        <asp:ListItem Value="4">Liabilities</asp:ListItem>
                    </asp:DropDownList>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Group"></asp:Label>
                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="Sub Group"></asp:Label>
                        <asp:DropDownList ID="ddlSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCode" runat="server" ControlToValidate="txtCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Click on Add button" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" Text="Code"></asp:Label>
                        <asp:TextBox ID="txtCode" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Name"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVName" runat="server" ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEName" runat="server" ControlToValidate="txtName" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtName" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescription" runat="server" ControlToValidate="txtDescription" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtGlDesc" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Label runat="server" Text="" ID="lblModalGlId" Visible="false"></asp:Label>
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDescNew"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDescUpdate"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDescSave"></asp:Button>
                        <asp:Button runat="server" Text="Activate" class="btn-ok" ID="btnDescActivate"></asp:Button>
                        <asp:Button runat="server" Text="DeActivate" class="btn-ok" ID="btnDescDeActivate"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalSGL" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Sub General Ledger Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblErrorSGL" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Head"></asp:Label>
                        <asp:DropDownList ID="ddlHeadSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                            <asp:ListItem Value="1">Asset</asp:ListItem>
                            <asp:ListItem Value="2">Income</asp:ListItem>
                            <asp:ListItem Value="3">Expenditure</asp:ListItem>
                            <asp:ListItem Value="4">Liabilities</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Group"></asp:Label>
                        <asp:DropDownList ID="ddlGroupSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="Sub Group"></asp:Label>
                        <asp:DropDownList ID="ddlSubGroupSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="General Ledger"></asp:Label>
                        <asp:DropDownList ID="ddlGLSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Click on Add button" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" Text="Code"></asp:Label>
                        <asp:TextBox ID="txtCodeSgl" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Name"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtNameSgl" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDescription" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDescSgl" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Label runat="server" Text="" ID="lblModalSglId" Visible="false"></asp:Label>
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnNewSgl"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnUpdateSgl"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSaveSgl"></asp:Button>
                        <asp:Button runat="server" Text="Activate" class="btn-ok" ID="btnActivateSgl"></asp:Button>
                        <asp:Button runat="server" Text="DeActivate" class="btn-ok" ID="btnDeactivateSgl"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalCRGL" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">General Ledger Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblErrorCRGl" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Head"></asp:Label>
                        <asp:DropDownList ID="ddlCRGLHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                            <asp:ListItem Value="1">Asset</asp:ListItem>
                            <asp:ListItem Value="2">Income</asp:ListItem>
                            <asp:ListItem Value="3">Expenditure</asp:ListItem>
                            <asp:ListItem Value="4">Liabilities</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Group"></asp:Label>
                        <asp:DropDownList ID="ddlCRGLGroup" runat="server" CssClass="aspxcontrols"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="Sub Group"></asp:Label>
                        <asp:DropDownList ID="ddlCRGLSubGroup" runat="server" CssClass="aspxcontrols"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCRGLCode" runat="server"
                            ControlToValidate="txtCRGLCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Click on Add button" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" Text="Code"></asp:Label>
                        <asp:TextBox ID="txtCRGLCode" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Name"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator5" runat="server"
                            ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVECRGLName" runat="server"
                            ControlToValidate="txtCRGLName" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtCRGLName" autocomplete="off" runat="server" CssClass="aspxcontrols"
                            MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCRGLDescription" runat="server"
                            ControlToValidate="txtCRGlDesc" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtCRGlDesc" autocomplete="off" runat="server" CssClass="aspxcontrols"
                            MaxLength="150"></asp:TextBox>
                    </div>

                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Label runat="server" Text="" ID="lblModalCRGlId" Visible="false"></asp:Label>
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDescNewCRGL"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDescUpdateCRGL"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDescSaveCRGL"></asp:Button>
                        <asp:Button runat="server" Text="Activate" class="btn-ok" ID="btnDescActivateCRGL"></asp:Button>
                        <asp:Button runat="server" Text="DeActivate" class="btn-ok" ID="btnDescDeActivateCRGL"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalCRSGL" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Sub General Ledger Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblErrorCRSGL" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Head"></asp:Label>
                        <asp:DropDownList ID="ddlHeadCRSgl" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                            <asp:ListItem Value="1">Asset</asp:ListItem>
                            <asp:ListItem Value="2">Income</asp:ListItem>
                            <asp:ListItem Value="3">Expenditure</asp:ListItem>
                            <asp:ListItem Value="4">Liabilities</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Group"></asp:Label>
                        <asp:DropDownList ID="ddlGroupCRSgl" runat="server" CssClass="aspxcontrols"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="Sub Group"></asp:Label>
                        <asp:DropDownList ID="ddlSubGroupCRSgl" runat="server" CssClass="aspxcontrols"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="General Ledger"></asp:Label>
                        <asp:DropDownList ID="ddlGLCRSgl" runat="server" CssClass="aspxcontrols"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REFVCodeCRSGL" runat="server"
                            ControlToValidate="txtCodeCRSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Click on Add button" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:Label runat="server" Text="Code"></asp:Label>
                        <asp:TextBox ID="txtCodeCRSgl" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Name"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REFVNameCRGL" runat="server"
                            ControlToValidate="txtNameCRSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="txtNameCRSgl1" runat="server"
                            ControlToValidate="txtNameCRSgl" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtNameCRSgl" autocomplete="off" runat="server" CssClass="aspxcontrols"
                            MaxLength="150"></asp:TextBox>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REFVDescCRSGL" runat="server"
                            ControlToValidate="txtDescCRSgl" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDescCRSgl" autocomplete="off" runat="server" CssClass="aspxcontrols"
                            MaxLength="150"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Label runat="server" Text="" ID="lblModalCRSGLID" Visible="false"></asp:Label>
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnNewCRSgl"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnUpdateCRSgl"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSaveCRSgl"></asp:Button>
                        <asp:Button runat="server" Text="Activate" class="btn-ok" ID="btnActivateCRSgl"></asp:Button>
                        <asp:Button runat="server" Text="DeActivate" class="btn-ok" ID="btnDeactivateCRSgl"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myModalIndex" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Index Details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-12 col-md-12">
                            <div class="pull-left">
                                <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblcabinet" runat="server" Text="Cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblSubcabinet" runat="server" Text="Sub cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblFolder" runat="server" Text="Folder"></asp:Label>
                                <asp:DropDownList ID="ddlFolder" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblDocumentType" runat="server" Text="Document Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                <asp:Label ID="lblDateDisplay" runat="server" CssClass="aspxlabelbold"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>

                        </div>

                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>

                                    <asp:TemplateField HeaderStyle-Width="1%" HeaderText="DescriptorID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescriptorID" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DescriptorID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descriptor" HeaderText="Descriptor" HeaderStyle-Width="40%"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvKeywords" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Keywords" HeaderStyle-Width="100%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Key") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:ImageButton ID="imgbtnIndexSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
