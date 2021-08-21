<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="CurrencyMasterDashboard.aspx.vb" Inherits="Masters_CurrencyMasterDashboard" ValidateRequest="false" %>

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
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=gvcurrencyMaster.ClientID%>').DataTable({
                iDisplayLength: 5,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>5.4 Currency Master</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <asp:Label ID="lblAuditStartDate" runat="server" Text="From"></asp:Label>
                <asp:TextBox ID="txtStartDate" autocomplete="off" runat="server" Width="140px" CssClass="aspxcontrols" MaxLength="10" placeholder="dd/MM/yyyy" data-toggle="tooltip" data-placement="bottom" title="dd/MM/yyyy" Visible="True"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtStartDate" PopupPosition="TopLeft" TargetControlID="txtStartDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
                <asp:Label ID="lblAuditEndDate" runat="server" Text="To"></asp:Label>
                <asp:TextBox ID="txtEndDate" autocomplete="off" runat="server" Width="140px" CssClass="aspxcontrols" MaxLength="10" placeholder="dd/MM/yyyy" data-toggle="tooltip" data-placement="bottom" title="dd/MM/yyyy" Visible="True"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtEndDate" PopupPosition="TopLeft" TargetControlID="txtEndDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
                <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
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
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblToCurrency" runat="server" Text="Currency"></asp:Label>
            <asp:DropDownList ID="ddlToCurrency" runat="server" AutoPostBack="true" CssClass="aspxcontrols" TabIndex="3">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblfromCurrency" runat="server" Text="Base Currency"></asp:Label>
            <asp:DropDownList ID="ddlfromcurrency" runat="server" CssClass="aspxcontrols" TabIndex="2">
            </asp:DropDownList>
        </div>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="gvcurrencyMaster" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                        <asp:Label ID="lblCurrID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CurrencyID") %>'></asp:Label>
                        <asp:Label ID="lblCurrOpID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "OperatedOnID") %>'></asp:Label>
                        <asp:Label ID="lblBankID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "BankID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="3%" />
                <asp:BoundField DataField="OperateOn" HeaderText="Currency" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="23%"></asp:BoundField>
                <asp:BoundField DataField="Currency" HeaderText="Base Currency" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%"></asp:BoundField>
                <asp:BoundField DataField="TTBuy" HeaderText="TT Buy" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%"></asp:BoundField>
                <asp:BoundField DataField="TTSell" HeaderText="TT Sell" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%"></asp:BoundField>
                <asp:BoundField DataField="TBuy" HeaderText="TC Buy" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%"></asp:BoundField>
                <asp:BoundField DataField="TSell" HeaderText="TC Sell" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%"></asp:BoundField>
                <asp:BoundField DataField="BankName" HeaderText="Bank/Agency" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%"></asp:BoundField>
                <asp:TemplateField HeaderText="Method" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblMethod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Type") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Time" HeaderText="Time" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="6%"></asp:BoundField>
                <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="7%"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS Pro</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsg" runat="server"></asp:Label>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

