<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Accounts.master" CodeFile="ChallanDetails.aspx.vb" Inherits="Accounts_ChallanDetails" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/General.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Challan Register</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-4 col-md-4 divmargin" style="padding-left: 0px">
            <asp:Label ID="lblBankName" runat="server" Text="Select Bank"></asp:Label>
            <asp:DropDownList ID="ddlBankName" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="220px">
            </asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4 divmargin" style="padding-left: 0px">
            <asp:Label runat="server" Text="Date"></asp:Label>
            <asp:TextBox ID="txtChequeDate" runat="server" CssClass="aspxcontrols" Width="220px"></asp:TextBox>
            <cc1:CalendarExtender ID="cclChequeDate" runat="server" PopupButtonID="txtChequeDate" PopupPosition="BottomLeft"
                TargetControlID="txtChequeDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <asp:Button CssClass="btn-ok" runat="server" data-toggle="tooltip" data-placement="bottom" Text="OK" title="OK" ID="BtnOk" Width="50px" />
        </div>
        <div class="col-sm-4 col-md-4 divmargin" style="padding-right: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:DataGrid ID="dgChallanDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        <asp:Label ID="lblACMID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACM_ID") %>'></asp:Label>
                        <asp:Label ID="lblACM_ChequeNo" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACM_ChequeNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="1%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:TemplateColumn>
                
                <asp:BoundColumn DataField="ACM_ID" HeaderText="ACM_ID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="1%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ACM_ChequeNo" HeaderText="Cheque No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ACM_AccountNo" HeaderText="Account No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="lblChequeDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACM_ChequeDate") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>

                <asp:TemplateColumn HeaderText="Contact_No.">
                    <ItemTemplate>
                        <asp:TextBox ID="TxtContactNo" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACD_ContactNo") %>' onkeypress="javascript:return OnlyIntegers()" Width="160px" CssClass="aspxcontrols" MaxLength ="10"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:TemplateColumn>

                <asp:TemplateColumn HeaderText="Pan_No.">
                    <ItemTemplate>
                        <asp:TextBox ID="TxtPanNo" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACD_PANNo") %>' onkeypress="" Width="160px" CssClass="aspxcontrols" MaxLength ="10"></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="ACM_Pay" HeaderText="Name">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ACM_Amount" HeaderText="Amount(Rs)">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <div id="ModalFASPostDatedCheDetails" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblPostDatedCheDetails" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
