<%@ Page Title="" Language="VB" MasterPageFile="~/LogisticsMaster.master" AutoEventWireup="false" CodeFile="FrmLgstDriverBilling.aspx.vb" Inherits="Logistics_FrmLgstDriverBilling" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
             $('#<%=ddlDriver.ClientID%>').select2();
               $('#<%=ddlExstBillNo.ClientID%>').select2();
            $('#<%=dgDriverBillDetails.ClientID%>').DataTable({
                iDisplayLength: -1,
                aLengthMenu: [[-1], ["All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });
        function validatePage() {
            //Executes all the validation controls associated with group1 validaiton Group1. 
            var flag = window.Page_ClientValidate('Validate');
            if (flag)
                return flag;

        }
    </script>

    <style>
        div {
            color: black;
        }
    </style>


    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Driver Billing Details</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update"  Visible="false"/>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" Visible="false" title="Approve"/>
                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="LabelError"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Bill No."></asp:Label>
            <asp:DropDownList ID="ddlExstBillNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label1" runat="server" Text="* Bill No"></asp:Label>
            <asp:TextBox ID="txtBillNo" runat="server" CssClass="aspxcontrols" autocomplete="off" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
             <asp:Label ID="Label2" runat="server" Text="* Bill Date"></asp:Label>
            <asp:TextBox ID="txtBillDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="cclBillDate" runat="server" PopupButtonID="txtBillDate" PopupPosition="BottomLeft" TargetControlID="txtBillDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <div class="form-group" runat="server">
                <asp:Label runat="server" Text="Status :-"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Driver Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlDriver"></asp:DropDownList>
                   </div>
                 <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblFromdate" runat="server" Text="* From Date"></asp:Label>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtFromDate" PopupPosition="BottomLeft" TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFromDate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic" ErrorMessage="Select From Date" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTodate" runat="server" Text="* To Date"></asp:Label>
            <asp:TextBox ID="txtToDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true" ></asp:TextBox>
            <cc1:CalendarExtender ID="cclToDate" runat="server" PopupButtonID="txtToDate" PopupPosition="BottomLeft" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVToDate" runat="server" ControlToValidate="txtToDate" Display="Dynamic" ErrorMessage="Select to Date" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
            
         <div class="col-sm-1 col-md-1">
             </br>
             <asp:Button ID="btnGo" runat="server" Text="GO" Font-Bold="true" />
        </div>
                   </div>
      <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        

    </div>
   <div class="col-sm-12 col-md-12">
            <asp:DataGrid ID="dgDriverBillDetails"  runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                  <HeaderStyle Font-Bold="true" BackColor="#e1ffff" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                     <Columns>
           
                <asp:BoundColumn DataField="DriverName" HeaderText="Driver Name">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="14%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TotalAmount" HeaderText="Allotted Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="9%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                        <asp:BoundColumn DataField="AdvanceAmt" HeaderText="Advance Given Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="9%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="PendingAmount" HeaderText="Pending Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="11%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                           
            </Columns>
        </asp:DataGrid>
    </div>
    
      <div class="">
        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
    </div>

    <div id="ModalPumpBillValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblPumpBillValidationMsg" runat="server"></asp:Label>
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
     <asp:TextBox ID ="txtGLID" runat ="server" Visible ="false" ></asp:TextBox> 
</asp:Content>



