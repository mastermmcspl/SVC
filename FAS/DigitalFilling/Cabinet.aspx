<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="Cabinet.aspx.vb" Inherits="DigitalFilling_Cabinet" ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgCabinet.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
        function Setting() {
            $('#myModal').modal('show');
        }
    </script>

    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>1. Cabinet</b></h2>
            </div>
            <div class="col-sm-9 col-md-9">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New Cabinet" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
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
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
        <div class="col-sm-8 col-md-8" style="padding: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="dgCabinet" runat="server" AutoGenerateColumns="False" class="footable">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="1%" />
                        <asp:Label ID="lblCBN_NODE" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CBN_NODE") %>'></asp:Label>
                        <asp:Label ID="lblCBN_NAME" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CBN_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CBN_NODE" HeaderText="ID" Visible="false"></asp:BoundField>
                <asp:TemplateField HeaderText="Cabinet Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="17%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkCabinetName" runat="server" CommandName="SelectCabinet" Text='<%# DataBinder.Eval(Container, "DataItem.CBN_NAME") %>' Font-Bold="true" Font-Italic="true"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="CBN_SCCount" HeaderText="Sub Cabinets" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="CBN_FolCount" HeaderText="Folders" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="CBN_CRON" HeaderText="Created On" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="cbn_DelStatus" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnStatus" CssClass="hvr-bounce-in" CommandName="Status" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="EditRow" runat="server" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content row">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cabinet Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class=" col-sm-12 col-md-12 form-group">
                        <asp:Label ID="lblCabName" runat="server" Text="* Cabinet Name"></asp:Label>
                        <asp:TextBox ID="txtCabName" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCabName" runat="server" SetFocusOnError="True" ControlToValidate="txtCabName" Display="Dynamic" ValidationGroup="ValidateCabinet"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCabName" runat="server" ControlToValidate="txtCabName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateCabinet"></asp:RegularExpressionValidator>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <asp:Label ID="lblCabNotes" runat="server" Text="Notes"></asp:Label>
                        <asp:TextBox ID="txtCabNotes" autocomplete="off" TextMode="MultiLine" runat="server" Height="84px" CssClass="aspxcontrols" />
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCabNotes" runat="server" ControlToValidate="txtCabNotes" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateCabinet"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDescNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDescSave" ValidationGroup="ValidateCabinet"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDescUpdate" ValidationGroup="ValidateCabinet" Visible="false"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="CabinetMasterValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCabinetEmpMasterValidationMsg" runat="server"></asp:Label></strong>
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
