<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="Descriptor.aspx.vb" Inherits="DigitalFilling_Descriptor" ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
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
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgDescDashBoard.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>4. Descriptor</b></h2>
            </div>
            <div class="col-sm-3 col-md-3 pull-right">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" />
                                </li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin ">
        <div>
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 ">
        <div class="col-sm-6 col-md-6" style="padding-left: 0px">
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="275px">
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:GridView ID="dgDescDashBoard" runat="server" Width="100%" class="footable" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Available">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                        <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="16%"></asp:BoundField>
                <asp:BoundField DataField="Note" HeaderText="Note" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%"></asp:BoundField>
                <asp:BoundField DataField="DataType" HeaderText="Data Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%"></asp:BoundField>
                <asp:BoundField DataField="Size" HeaderText="Size" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="7%"></asp:BoundField>
                <asp:BoundField DataField="CrBy" HeaderText="Created By" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%"></asp:BoundField>
                <asp:BoundField DataField="CrOn" HeaderText="Created On" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnStatus" CssClass="hvr-bounce-in" CommandName="Status" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CssClass="hvr-bounce-in" CommandName="EditRow" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="myModal" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Descriptor details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblDescName" runat="server" Text="* Descriptor Name"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescName" runat="server" SetFocusOnError="True" ControlToValidate="txtDescName" Display="Dynamic" ValidationGroup="ValidateDescriptor"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDescName" runat="server" ControlToValidate="txtDescName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDescriptor"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtDescName" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblDescNote" runat="server" Text="* Descriptor Note"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescNote" runat="server" SetFocusOnError="True" ControlToValidate="txtDescNote" Display="Dynamic" ValidationGroup="ValidateDescriptor"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDescNote" runat="server" ControlToValidate="txtDescNote" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDescriptor"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtDescNote" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblDescDataType" runat="server" Text="* Descriptor DataType"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescDataType" runat="server" SetFocusOnError="True" ControlToValidate="ddlDescDataType" Display="Dynamic" ValidationGroup="ValidateDescriptor"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDescDataType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblDescSize" runat="server" Text="* Descriptor Size"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescSize" runat="server" SetFocusOnError="True" ControlToValidate="txtDescSize" Display="Dynamic" ValidationGroup="ValidateDescriptor"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDescSize" runat="server" ControlToValidate="txtDescSize" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDescriptor"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtDescSize" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="3" />
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblDescValue" runat="server" Text="Descriptor Values"></asp:Label>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDescValue" runat="server" ControlToValidate="txtDescValue" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDescriptor"></asp:RegularExpressionValidator>
                            <asp:TextBox ID="txtDescValue" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDescNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDescSave" ValidationGroup="ValidateDescriptor"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDescUpdate" ValidationGroup="ValidateDescriptor"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnDescCancel"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalDescDashBoardValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>GRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblDescDashBoardValidationMsg" runat="server"></asp:Label></strong>
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

