<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="HolidayMaster.aspx.vb" Inherits="Masters_HolidayMaster"
    EnableEventValidation="false" ValidateRequest="false" %>

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
       <style>        
                div{
            color:black;
                      }        
        </style>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>1.4 Holiday & Year Master</b></h2>
            </div>
            <div class="form-group pull-right">
                <asp:ImageButton ID="imgbtnAddDays" CssClass="activeIcons hvr-bounce-out" data-toggle="tooltip" data-target="#myModal" data-placement="bottom" title="Add Holiday" runat="server" CausesValidation="False" />
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
    <div class="col-sm-12 col-md-12" style="padding: 0px;">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblYears" runat="server" Text="Financial Year"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblHeadingFromDate" runat="server" Text="From Date"></asp:Label>
                <asp:TextBox ID="txtFromDate" runat="server" ReadOnly="true" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblHeadingTodate" runat="server" Text="To Date"></asp:Label>
                <asp:TextBox ID="txtToDate" runat="server" ReadOnly="true" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3" style="padding-right: 0px;">
            <div class="clearfix divmargin"></div>
            <asp:CheckBox ID="chkCurrentYear" runat="server" TextAlign="Right" AutoPostBack="true" /><asp:Label ID="lblCurrentYear" runat="server" Text="Current Year"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkWeeklyOff" runat="server" OnClick="lnkWeeklyOff_Click" Font-Italic="true">Add Weekly off</asp:LinkButton>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium;">
        <div class="form-group">
            <asp:DataGrid ID="grdHolidays" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="HolidayDate" HeaderText="Date ">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Occasion" HeaderText="Occasion">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="90%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ToolTip="Delete Date" CssClass="hvr-bounce-in" />
                            <asp:Label ID="lblDate" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.HDFormat") %>' />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </div>
    <div class="form-group pull-right">
        <asp:Label ID="lblFromDate" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblToDate" runat="server" Text="" Visible="false"></asp:Label>
    </div>
    <div id="ModalYMValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divYMMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblYMValidationMsg" runat="server"></asp:Label>
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
    <div id="ModalHMValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divHMMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblHMValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" Text="OK" class="btn-ok" ID="btnOkHM" OnClick="btnOkHM_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalDeleteconfirmation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divDeleteConfirm" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblConfirmDelete" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <div class="modal-footer">
                        <div class="pull-right">
                            <asp:Button runat="server" Text="Yes" class="btn-ok" ID="btnConfirmDelete" OnClick="btnConfirmDelete_Click"></asp:Button>
                            <asp:Button runat="server" Text="No" class="btn-ok" ID="btnCancelDelete" data-dismiss="modal"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalHoliday" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Add Holiday Details</b></h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label ID="lblHolidayDate" runat="server" Text="* Select Holiday Date"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSelDate" runat="server" ControlToValidate="txtSelDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Details"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtSelDate" runat="server" CssClass="aspxcontrols" MaxLength="10" placeholder="dd/MM/yyyy"></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSelDate" runat="server" ControlToValidate="txtSelDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Details"></asp:RegularExpressionValidator>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtTargetDate" PopupPosition="BottomLeft"
                            TargetControlID="txtSelDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblOccasion" runat="server" Text="* Occasion"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOccasion" runat="server" ControlToValidate="txtOccasion" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Details"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtOccasion" runat="server" CssClass="aspxcontrols" MaxLength="500"></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVOccasion" runat="server" ControlToValidate="txtOccasion" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Details"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblHMError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="Add Holiday" class="btn-ok" ID="btnSaveHolidays" ValidationGroup="Details" OnClick="btnSaveHolidays_Click"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnCancel" OnClick="btnCancel_Click" ValidationGroup="false"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
