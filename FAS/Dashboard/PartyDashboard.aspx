<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="PartyDashboard.aspx.vb" Inherits="Dashboard_AccountsDashboard" %>

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
             $('#<%=dgDashBoard.ClientID%>').DataTable({
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
                <h2><b>Party Wise - Dash Board</b></h2>
            </div>                     
        </div>
    </div>

   <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="dgDashBoard" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:BoundField DataField="Party" HeaderText="Party" HeaderStyle-Width="20%"></asp:BoundField>
                <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-Width="7%"></asp:BoundField>
                <asp:BoundField DataField="BillNo" HeaderText="Bill No" HeaderStyle-Width="8%"></asp:BoundField>
                <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" HeaderStyle-Width="9%"></asp:BoundField>
                <asp:BoundField DataField="OpDebit" HeaderText="OB Debit" HeaderStyle-Width="8%"></asp:BoundField>
                <asp:BoundField DataField="OpCredit" HeaderText="OB Credit" HeaderStyle-Width="8%"></asp:BoundField>
                <asp:BoundField DataField="TransDebit" HeaderText="Trans Debit" HeaderStyle-Width="9%"></asp:BoundField>
                <asp:BoundField DataField="TransCredit" HeaderText="Trans Credit" HeaderStyle-Width="9%"></asp:BoundField>
                <asp:BoundField DataField="ClosingDebit" HeaderText="Closing Debit" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="ClosingCredit" HeaderText="Closing Credit" HeaderStyle-Width="10%"></asp:BoundField>                
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalPaymentValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblPaymentMasterValidationMsg" runat="server"></asp:Label></strong>
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

