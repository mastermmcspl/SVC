
<%@ Page Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="FrmLgstDriverAllowanceMaster.aspx.vb" Inherits="Masters_FrmLgstAllowancesMaster" ValidateRequest="false" %>

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
            $('#<%=ddlVehicleType.ClientID%>').select2();
            });

        function Validate() {
            if (document.getElementById('<%=ddlVehicleType.ClientID %>').value == "") {
                alert('Salect Vehicle Type.');
                document.getElementById('<%=ddlVehicleType.ClientID%>').focus()
                return false
            }
                                       }
    </script>

       <style>        
                div{
            color:black;
                      }        
        </style>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>Driver Allownaces Details</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save"  ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                   <asp:Label ID="lblID" runat="server" Visible ="false" ></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

<%--    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Driver"></asp:Label> 
            <asp:DropDownList ID="ddlExistingDriver" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3"></div>   
        <div class="col-sm-3 col-md-3"></div>   
        <div class="col-sm-3 col-md-3">  
            <br />
            <div class="form-group" runat ="server" >
            <asp:Label runat="server" Text="Status :-"></asp:Label>
            <asp:Label ID ="lblStatus" runat="server" Text=""></asp:Label>
            </div>          
        </div>
    </div>--%>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:Label runat="server" Text="* Vehicle Type"></asp:Label>
           <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        <asp:ListItem Value="0">Select Vehicle Type</asp:ListItem>
                        <asp:ListItem Value="1">Truck</asp:ListItem>
                        <asp:ListItem Value="2">Tata Ace</asp:ListItem>
                        <asp:ListItem Value="3">Eicher 19</asp:ListItem>
                        <asp:ListItem Value="4">Container Large</asp:ListItem>
                    </asp:DropDownList>
                 </div>       
        <div class="col-sm-6 col-md-6">
            <asp:Label runat="server" Text="* No of Days/Hours"></asp:Label>
           <asp:TextBox ID="txtLicenseNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLicenseNo" runat="server" ControlToValidate="txtLicenseNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter License No" ValidationGroup="ValidateDriver"></asp:RequiredFieldValidator>
            </div>
        </div>
      
    <div id="ModalFASAllowancesValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAllowancesValidationMsg" runat="server"></asp:Label>
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

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:TextBox ID ="txtGLID" runat ="server" Visible ="false" ></asp:TextBox>
    </div>
</asp:Content>




