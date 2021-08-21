<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" EnableEventValidation="false" CodeFile="EmployeeMaster.aspx.vb" Inherits="Masters_EmployeeMaster" %>

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
              div{
            color:black;
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
            $('#<%=dgEmployeeDetails.ClientID%>').DataTable({
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
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>2.6 Employee Master</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <asp:Label ID="lblHeadingSearch" Text="Search by" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="aspxcontrols" Width="140px">
                </asp:DropDownList>
                <asp:TextBox autocomplete="off" ID="txtSearch" runat="server" Width="140px" CssClass="aspxcontrols" />
                <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSearch" runat="server" ErrorMessage="Select Search by." SetFocusOnError="True" ControlToValidate="ddlSearch" ValidationGroup="Search" Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnUnBlock" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Unblock" />
                    <asp:ImageButton ID="imgbtnUnLock" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Unlock" />
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
        <asp:GridView ID="dgEmployeeDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" >       
            <Columns>
                <asp:TemplateField>                   
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                        <asp:Label ID="lblEmpID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EmpID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                    </HeaderTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" Visible ="false"  HeaderStyle-Width="7%"></asp:BoundField>
                <asp:BoundField DataField="SAPCode" HeaderText="SAP Code" HeaderStyle-Width="7%"></asp:BoundField>
                <asp:BoundField DataField="EmpID" HeaderText="EmpID" Visible="False"> </asp:BoundField>
                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" HeaderStyle-Width="11%"></asp:BoundField>
                <asp:BoundField DataField="LoginName" HeaderText="Login Name" HeaderStyle-Width="11%"></asp:BoundField>
                <asp:BoundField DataField="Designation" HeaderText="Designation" HeaderStyle-Width="9%" ></asp:BoundField>
                <asp:BoundField DataField="Module" HeaderText="Module(Role)" HeaderStyle-Width="9%"></asp:BoundField>
                <asp:BoundField DataField="LastLogin" HeaderText="Last Login Date" HeaderStyle-Width="11%"></asp:BoundField>
                <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="7%"></asp:BoundField>
                <asp:BoundField DataField="Region" HeaderText="Region" HeaderStyle-Width="9%"></asp:BoundField>
                <asp:BoundField DataField="Area" HeaderText="Area" HeaderStyle-Width="9%"></asp:BoundField>
                <asp:BoundField DataField="Branch" HeaderText="Branch" HeaderStyle-Width="9%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" Visible="false" HeaderStyle-Width="7%"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" />
                    </ItemTemplate>                   
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="Edit" ImageUrl ="~/Images/Edit16.png" runat="server" CssClass="hvr-bounce-in" />
                    </ItemTemplate>                   
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalEmpMasterValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>GRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblEmpMasterValidationMsg" runat="server"></asp:Label></strong>
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
