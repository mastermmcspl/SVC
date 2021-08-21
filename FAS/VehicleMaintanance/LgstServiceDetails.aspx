<%@ Page Language="VB" MasterPageFile="~/VehicleMaintanance.master" AutoEventWireup="false" CodeFile="LgstServiceDetails.aspx.vb" Inherits="VehicleMaintanance_LgstServiceDetails" ValidateRequest="false" %>

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
          <%--  $('#<%=ddlExistingVehicleNo.ClientID%>').select2();--%>
               $('#<%=ddlExistingVehicleNo.ClientID%>').select2();
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
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>Vehicle Service Details</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" Visible="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" Visible="false" />
                    <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;" Visible="false"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0" Visible="false"></asp:Label></span>
                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Registration No"></asp:Label>
            <asp:DropDownList ID="ddlExistingVehicleNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3"></div>
        <div class="col-sm-3 col-md-3"></div>
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
            <asp:Label runat="server" Text="* Service Center"></asp:Label>
           <asp:TextBox ID="txtServiceCntr" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Date of Giving the Vehicle for Service"></asp:Label>
            <asp:TextBox ID="txtServiceDT" runat="server" autocomplete="off" CssClass="aspxcontrols" placeholder="DD/MM/YYYY"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtServiceDT" PopupPosition="BottomLeft" TargetControlID="txtServiceDT" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
        </div>
           <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Start Meter Reading"></asp:Label>
            <asp:TextBox ID="txtStartMtr" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
           </div>
            <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* End Meter Reading"></asp:Label>
             <asp:TextBox ID="txtEndMtr" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
    </div>
   
    
    <div id="ModalFASVehicleValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblVehicleValidationMsg" runat="server"></asp:Label>
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

</asp:Content>






