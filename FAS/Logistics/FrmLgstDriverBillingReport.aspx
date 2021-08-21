<%@ Page Title="" Language="VB" MasterPageFile="~/LogisticsMaster.master" AutoEventWireup="false" CodeFile="FrmLgstDriverBillingReport.aspx.vb" Inherits="Logistics_FrmLgstDriverBillingReport" %>

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
             $('#<%=ddlBillNo.ClientID%>').select2();
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
                <h2><b>Driver Billing Report</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="LabelError"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Driver"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlDriver"></asp:DropDownList>
           <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDriver" runat="server" ControlToValidate="ddlDriver" Display="Dynamic" ErrorMessage="Select Driver Name" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
             </div>
          <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Bill No"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlBillNo"></asp:DropDownList>
           <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillNo" runat="server" ControlToValidate="ddlBillNo" Display="Dynamic" ErrorMessage="Select Bill No" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
             </div>
           </div>
           <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px;align-items:center">
 <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px;align-items:center">
 </div>
    <div class="col-sm-10 col-md-10 form-group pull-left" style="padding: 0px;align-items:center">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
    </div> 
               <div class="col-sm-1 col-md-1 form-group pull-left" style="padding: 0px;align-items:center">
 </div>
           </div>    
                   
 </asp:Content>



