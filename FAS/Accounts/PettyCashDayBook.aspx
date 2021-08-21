<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Accounts.master" CodeFile="PettyCashDayBook.aspx.vb" Inherits="Accounts_PettyCashDayBook" ValidateRequest="false" %>

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
            $('#<%=ddlCrHead.ClientID%>').select2();
            $('#<%=ddlCrGL.ClientID%>').select2();
            $('#<%=ddlCrSubGL.ClientID%>').select2();

        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Petty Cash Day Book</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                    <asp:ImageButton ID="ImgBtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" CausesValidation="false" />
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
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" Visible="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-3 col-md-3 pull-right">
            <asp:Label runat="server" Text="Status:- " Font-Bold="true"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
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
                <asp:Label runat="server" Text="Existing Voucher Number"></asp:Label>
                <asp:DropDownList ID="ddlExisting" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>


            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Transaction No"></asp:Label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            </div>


            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblParty" Text="* General Ledger" runat="server"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVParty" runat="server" ControlToValidate="ddlParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Customer/Supplier/Party." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Date"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Date Of Cash Recieved." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVInvoiceDate" runat="server" ControlToValidate="txtInvoiceDate" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" AutoComplete="off"></asp:TextBox>
                <cc1:CalendarExtender ID="ccInvoiceDate" runat="server" PopupButtonID="txtInvoiceDate" PopupPosition="BottomLeft" TargetControlID="txtInvoiceDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                <%-- <asp:RangeValidator ID="rgvtxtInvoiceDate" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="txtInvoiceDate" SetFocusOnError="True"></asp:RangeValidator>--%>
            </div>
        </div>
        <br />
        <br />
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Cash Details</legend>
            </fieldset>
            <div class="col-sm-0 col-md-0">
                <asp:Label runat="server" Text="* Head of Accounts" Visible="false"></asp:Label>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCrHead" runat="server" ControlToValidate="ddlCrHead" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Select Head of Accounts." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList ID="ddlCrHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                    <asp:ListItem Value="0">Select Head of Account</asp:ListItem>
                    <asp:ListItem Value="1">Asset</asp:ListItem>
                    <asp:ListItem Value="2">Income</asp:ListItem>
                    <asp:ListItem Value="3">Expenditure</asp:ListItem>
                    <asp:ListItem Value="4">Liabilities</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-0 col-md-0">
                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCrGL" runat="server" ControlToValidate="ddlCrGL" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Select General Ledger." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList ID="ddlCrGL" runat="server" CssClass="aspxcontrols" Visible="false" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-0 col-md-0">
                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlCrSubGL" runat="server" CssClass="aspxcontrols" Visible="false" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Cash"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="aspxcontrols" AutoCompleteType="Disabled"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-1 col-md-1">
                <br/>
               <asp:ImageButton ID="imgCash" runat="server" Width="35%"></asp:ImageButton>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">

            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Debit Details</legend>
            </fieldset>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="*Sub General Ledger"></asp:Label>
                <asp:DropDownList ID="ddldbsUbGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>

            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text=" Voucher No"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtVoucherNo" AutoComplete="off" runat="server" CssClass="aspxcontrols" Width="90%"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Amount"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtDebitAmount" runat="server" CssClass="aspxcontrols" AutoCompleteType="Disabled" ></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Narration"></asp:Label>
                <asp:TextBox ID="txtNarration" runat="server" CssClass="aspxcontrols" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="col-sm-1 col-md-1">
                <br/>
                <asp:ImageButton ID="imgDebit" runat="server" Width="35%"></asp:ImageButton>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
    </div>
    <br />
    <br />
    <br />


    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <asp:GridView ID="GvPettyCashDetails" runat="server" AutoGenerateColumns="False" CssClass="footable" OnRowDeleting="GvPettyCashDetails_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="0%" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SrNo") %>'></asp:Label>
                            <asp:Label ID="lblPkId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PkID") %>'></asp:Label>
                            <asp:Label ID="lblmasterID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MasterID") %>'></asp:Label>
                            <asp:Label ID="lblDate" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date") %>'></asp:Label>
                            <asp:Label ID="lblParticulars" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Particulars") %>'></asp:Label>
                            <asp:Label ID="lblParticularsId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ParticularsId") %>'></asp:Label>
                            <asp:Label ID="lblCashRecieved" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CashRecieved") %>'></asp:Label>
                            <asp:Label ID="lblVoucherNo" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VoucherNo") %>'></asp:Label>
                            <asp:Label ID="lblamount" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>'></asp:Label>
                             <asp:Label ID="lblNarration" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Narration") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SrNo" HeaderText="Sr No" ItemStyle-Width="5%" />
                    <asp:BoundField DataField="PkID" HeaderText="PkID" ItemStyle-Width="10%" Visible="false" />
                    <asp:BoundField DataField="MasterID" HeaderText="MasterID" ItemStyle-Width="10%" Visible="false" />
                    <asp:BoundField DataField="CashRecieved" HeaderText="Cash Recieved" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="Particulars" HeaderText="Particulars" ItemStyle-Width="15%" />
                    <asp:BoundField DataField="ParticularsId" HeaderText="ParticularsId" ItemStyle-Width="15%" Visible="false" />
                    
                    <%--  <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="10%">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlParticulars" runat="server" CssClass="aspxcontrols" Width="100%" Height="50px"></asp:DropDownList>
                    <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ddlParticulars") %>' CssClass="aspxlabel" Visible="False" Height="100%" Width="100px"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Wrap="true" />
            </asp:TemplateField>--%>
                    <%--    <asp:TemplateField HeaderText="Voucher No" ItemStyle-Width="0%">
                <ItemTemplate>
                    <asp:TextBox ID="txtVoucherNo" runat="server" ItemStyle-Width="15%" Width="100%" Height="40px" Text='<%# DataBinder.Eval(Container, "DataItem.VoucherNo") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>--%>
                    <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" ItemStyle-Width="10%" />
                    <%-- <asp:TemplateField HeaderText="Amount" ItemStyle-Width="0%">
                <ItemTemplate>
                    <asp:TextBox ID="txtAmount" runat="server" ItemStyle-Width="15%" Width="100%" Height="40px" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>--%>
                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="10%" />
                    <%-- <asp:TemplateField ItemStyle-Width="6%">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtndgEdit" CommandName="Edit" CssClass="activeIcons hvr-bounce-out" Visible="true" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit" Style="padding-right: 0px;"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="Narration" HeaderText="Narration" ItemStyle-Width="15%" />
                    <asp:TemplateField ItemStyle-Width="6%">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDeActivate" CommandName="Delete" CssClass="activeIcons hvr-bounce-out" Visible="true" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" Style="padding-right: 0px;"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
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
