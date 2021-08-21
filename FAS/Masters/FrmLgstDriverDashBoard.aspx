<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="FrmLgstDriverDashBoard.aspx.vb" Inherits="Masters_FrmLgstDriverDashBoard" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
            $('#<%=GvDriverMaster.ClientID%>').DataTable({
                iDisplayLength: 20,
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
            <div class="col-sm-4 col-md-4 pull-left">
                <h2><b>Driver Master DashBoard</b></h2>
            </div>
            <div class="col-sm-5 col-md-5" style="visibility: hidden">
                <asp:Label ID="lblHeadingSearch" Text="Search by" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="aspxcontrols" Width="140px">
                </asp:DropDownList>
                <asp:TextBox autocomplete="off" ID="txtSearch" runat="server" Width="140px" CssClass="aspxcontrols" />
                <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
                <asp:RequiredFieldValidator CssClass="auto-style1" ID="RFVSearch" runat="server" ErrorMessage="Select Search by." SetFocusOnError="True" ControlToValidate="ddlSearch" ValidationGroup="Search" Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />

                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-4 col-md-4 divmargin" style="padding-left: 0px">
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="275px">
            </asp:DropDownList>
        </div>
        <div class="col-sm-8 col-md-8 divmargin" style="padding-right: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>



    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="GvDriverMaster" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
         <asp:TemplateField HeaderStyle-Width="2%">

                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LDM_ID") %>'></asp:Label>
                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="DriverName" HeaderText="Driver Name" Visible="true" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="LicenseNo" HeaderText="License No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="AadharNo" HeaderText="Aadhar No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="19%"></asp:BoundField>
                <asp:TemplateField  HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnStatus" CssClass="hvr-bounce-in" CommandName="Status" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="5%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit1" runat="server" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>


    <div id="ModalasserDriverValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblDltnValidationMsg" runat="server"></asp:Label></strong>
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


