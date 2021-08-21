<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="FixedAssetJEEntry.aspx.vb" Inherits="Accounts_FixedAssetJEEntry" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />


    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Journal Entry Transaction</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
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

    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Existing Payment Voucher"></asp:Label>
                <asp:DropDownList ID="ddlExistPayment" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Transaction No"></asp:Label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Party"></asp:Label>
                <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Location"></asp:Label>
                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Fixed Asset Block"></asp:Label>
                <asp:DropDownList ID="ddlFixedAssetBlock" runat="server" CssClass="aspxcontrols">
                    <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                    <asp:ListItem Value="1">Addition</asp:ListItem>
                    <asp:ListItem Value="2">Deduction</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Amount"></asp:Label>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <fieldset class="col-sm-12 col-md-12">
                    <legend class="legendbold">Debit Details</legend>
                </fieldset>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                    <asp:DropDownList ID="ddldbHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        <asp:ListItem Value="0">--- Select Head of Account ---</asp:ListItem>
                        <asp:ListItem Value="1">Asset</asp:ListItem>
                        <asp:ListItem Value="4">Liabilities</asp:ListItem>
                        <asp:ListItem Value="2">Income</asp:ListItem>
                        <asp:ListItem Value="3">Expenditure</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddldbGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Sub General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddldbsUbGL" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Amount"></asp:Label>
                    <asp:TextBox ID="txtDbAmount" runat="server"></asp:TextBox>
                </div>
                <asp:ImageButton ID="ibtnDbAdd" runat="server" ImageUrl ="~/Images/Add16.png" />
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <fieldset class="col-sm-12 col-md-12">
                    <legend class="legendbold">Credit Details</legend>
                </fieldset>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                    <asp:DropDownList ID="ddlCrHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        <asp:ListItem Value="0">--- Select Head of Account ---</asp:ListItem>
                        <asp:ListItem Value="1">Asset</asp:ListItem>
                        <asp:ListItem Value="4">Liabilities</asp:ListItem>
                        <asp:ListItem Value="2">Income</asp:ListItem>
                        <asp:ListItem Value="3">Expenditure</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddlCrGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Sub General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddlCrSubGL" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-2 col-md-2">
                    <asp:Label runat="server" Text="Amount"></asp:Label>
                    <asp:TextBox ID="txtCrAmount" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-1 col-md-1">
                    <asp:Label ID ="lblGapCr" runat="server" ></asp:Label>
                    <asp:ImageButton ID="ibtnCrAdd" runat="server" ImageUrl ="~/Images/Add16.png"  />
                </div>
                
            </div>
            <div class="col-sm-3 col-md-3 form-group">
                <asp:Label runat="server" Text="Narration"></asp:Label>
                <asp:TextBox ID="txtNarration" runat="server" CssClass="aspxcontrols" Height="70px" Width="805px" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:DataGrid ID="dgAccount"  runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable">
             <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="Sl.No" HeaderText="Sl.No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Head" HeaderText="Head" Visible ="false" >
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GlCode" HeaderText="GL Code" Visible ="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GLDescription" HeaderText="GL Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SubGlCode" HeaderText="Sub Gl Code" Visible ="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Sub GL Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DebitAmt(Dr.)" HeaderText="Debit Amt">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CreditAmt(Cr.)" HeaderText="Credit Amt">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Balance" HeaderText="Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDelete" CommandName ="Delete" runat="server" ImageUrl="~/Images/Trash16.png" ToolTip ="Delete" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgEdit" CommandName="Edit" runat="server" ImageUrl="~/Images/Edit16.png" ToolTip ="Edit" />
                        <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit">Edit</asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="PaymentType" HeaderText="PaymentType" Visible="False"></asp:BoundColumn>
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
    
</asp:Content>

