<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="DocumentType.aspx.vb" Inherits="DigitalFilling_DocumentType" ValidateRequest="false" %>

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
            $('#<%=dgDocTypeDashBoard.ClientID%>').DataTable({
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
                <h2><b>2.2 Document Type</b></h2>
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
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="275px">
            </asp:DropDownList>
        </div>
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px; padding-right: 0px">
        <asp:GridView ID="dgDocTypeDashBoard" runat="server" Width="100%" class="footable" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                        <asp:Label ID="lblDocTypeID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocTypeID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" DataField="Name" HeaderText="Name" HeaderStyle-Width="33%"></asp:BoundField>
                <asp:BoundField DataField="Note" HeaderText="Note" Visible="false"></asp:BoundField>
                <asp:BoundField DataField="CrBy" HeaderText="Created By" Visible="false"></asp:BoundField>
                <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" DataField="CrOn" HeaderText="Created On" HeaderStyle-Width="12%"></asp:BoundField>
                <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" DataField="Status" HeaderText="Status" HeaderStyle-Width="14%"></asp:BoundField>
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
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Document Type details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-6 col-md-6" style="padding-left: 0px;">
                            <div class="form-group">
                                <asp:Label ID="lblDocType" runat="server" Text="* Document Type"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDocType" runat="server" SetFocusOnError="True" ControlToValidate="txtDocType" Display="Dynamic" ValidationGroup="ValidateDocType"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDocType" runat="server" ControlToValidate="txtDocType" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDocType"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtDocType" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-6 col-md-6" style="padding-left: 0px;">
                            <div class="form-group">
                                <asp:Label ID="lblNote" runat="server" Text="Notes"></asp:Label>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVNote" runat="server" ControlToValidate="txtNote" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDocType"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtNote" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:GridView ID="dgDisplay" runat="server" Width="100%" class="footable" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Available">
                                <Columns>
                                    <asp:TemplateField HeaderText="DescId" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descriptor" HeaderText="Descriptor"></asp:BoundField>
                                    <asp:TemplateField HeaderText="DataType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDataType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DataType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSize" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Size") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mandatory">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectMandatory" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Values">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Values") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Validator">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectValidator" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-6 col-md-6" style="padding-left: 0px;">
                            <div class="form-group">
                                <asp:Label ID="lblDescriptor" runat="server" Text="Descriptor"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescriptor" runat="server" SetFocusOnError="True" ControlToValidate="ddlDescriptor" Display="Dynamic" ValidationGroup="ValidateDesc"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlDescriptor" runat="server" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6" style="padding-right: 0px;">
                            <br />
                            <div class="form-group">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn-ok" Text="Add Descriptor" ValidationGroup="ValidateDesc" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDocTypeNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDocTypeSave" ValidationGroup="ValidateDocType"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDocTypeUpdate" ValidationGroup="ValidateDocType"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
