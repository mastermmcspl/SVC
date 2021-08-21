<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Accounts.master" CodeFile="OpeningBalance.aspx.vb" Inherits="Accounts_OpeningBalance" ValidateRequest="false" Debug="true" %>

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
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
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
            $('#<%=grdGL.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Opening Balance</b></h2>
            </div>
            <div class="form-group pull-right">
                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                <%--<asp:ImageButton ID="imgbtnReport" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Download24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />--%>
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
    <div class="col-sm-12 col-md-12">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12">
        <asp:Label ID="lblMsg" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True"
                    ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        </div>

    <div class="col-sm-4 col-md-4">
        <asp:Label ID="lblStartDate" runat="server" Text="* Opening Balance As on"></asp:Label>
        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        <asp:TextBox ID="txtStartDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>

        <cc1:CalendarExtender ID="cclStartDate" runat="server" PopupButtonID="txtStartDate" PopupPosition="BottomLeft"
            TargetControlID="txtStartDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
        </cc1:CalendarExtender>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
            <asp:DropDownList ID="ddlHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                <asp:ListItem Value="0">Head of Accounts</asp:ListItem>
                <asp:ListItem Value="1">Assets</asp:ListItem>
                <asp:ListItem Value="2">Income</asp:ListItem>
                <asp:ListItem Value="4">Liabilities</asp:ListItem>
                <asp:ListItem Value="3">Expenditure</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Group"></asp:Label>
            <asp:DropDownList ID="ddlGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="Sub Group"></asp:Label>
            <asp:DropDownList ID="ddlSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="grdGL" runat="server" AutoGenerateColumns="False" class="footable" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="Head" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHead" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Head") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="GL Code" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblGLCode" Text='<%# DataBinder.Eval(Container, "DataItem.GLCode") %>' Align="Right"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="GL Description" HeaderStyle-Width="40%">
                    <ItemTemplate>
                        <%--<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GLDescription") %>' Align="Right"></asp:Label>--%>
                        <asp:LinkButton ID="lnkbtnGLDescription" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GLDescription") %>'></asp:LinkButton>
                        <asp:Label runat="server" ID="lblGLID" Visible="true" Text='<%# DataBinder.Eval(Container, "DataItem.GLID") %>' Align="Right"></asp:Label>
                        <asp:Label runat="server" ID="lblAccHead" Visible="true" Text='<%# DataBinder.Eval(Container, "DataItem.AccHead") %>' Align="Right"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GLDescription") %>' Align="Right"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="Label1" runat="server" Text="Grand Total" Font-Bold="true"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Debit(Rs)">
                    <ItemTemplate>
                        <asp:TextBox ID="TxtDeb" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Debit") %>' onkeypress="javascript:return OnlyIntegers()" Width="120px" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDrGtotal" Text='<%# DataBinder.Eval(Container, "DataItem.DebitTotal") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="120px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sub-GL Debit(Rs)">
                    <ItemTemplate>
                        <asp:TextBox ID="TxtSubGLDeb" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubGLDebit") %>' onkeypress="javascript:return OnlyIntegers()" Width="130px" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>

                    <FooterTemplate>
                        <asp:TextBox ID="txtSubGLDrGtotal" Text='<%# DataBinder.Eval(Container, "DataItem.SubGLDebitTotal") %>' Style="text-align: right" runat="server" CssClass="aspxcontrols" Width="130px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Credit(Rs)">
                    <ItemTemplate>
                        <asp:TextBox ID="TxtCre" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Credit") %>' onkeypress="javascript:return OnlyIntegers()" Width="120px" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>

                    <FooterTemplate>
                        <asp:TextBox ID="txtCrGtotal" Text='<%# DataBinder.Eval(Container, "DataItem.CreditTotal") %>' Style="text-align: right" runat="server" CssClass="aspxcontrols" Width="120px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Sub-GL Credit(Rs)">
                    <ItemTemplate>
                        <asp:TextBox ID="TxtSubGLCre" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubGLCredit") %>' onkeypress="javascript:return OnlyIntegers()" Width="130px" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>

                    <FooterTemplate>
                        <asp:TextBox ID="txtSubGLCrGtotal" Text='<%# DataBinder.Eval(Container, "DataItem.SubGLCreditTotal") %>' Style="text-align: right" runat="server" CssClass="aspxcontrols" Width="130px" ReadOnly="True" Font-Bold="True"></asp:TextBox>
                    </FooterTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Balance" DataField="Balance" HeaderStyle-Width="15%"></asp:BoundField>
            </Columns>
        </asp:GridView>
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
                                <asp:Label ID="lblOpeningBalanceMsg" runat="server"></asp:Label></strong>
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

    <div class=" modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Break Up Details</h4>
                </div>
                 <div class="modal-body row">
                <div class="col-sm-12 col-md-12" runat="server">
                    <asp:GridView ID="GVDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                        <Columns>
                            <asp:TemplateField HeaderText="GLID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblGLID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BillNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblBillNo" runat="server" CommandName="VAT" Text='<%# DataBinder.Eval(Container.DataItem, "BillNo") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                <ItemTemplate>
                                    <asp:Label ID="lblDebit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Debit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Credit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Credit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
            </div>
        </div>
    </div>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
