<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="VendorDynamicReports.aspx.vb" Inherits="Reports_VendorDynamicReports" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
     <div class="loader"></div>
       <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Vendor Reconcilation Dynamic Report</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />   
                     </div>
            </div>
        </div>
    </div>
    
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
     <div class="clearfix divmargin"></div>
    
     <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:RadioButtonList ID="rbtnStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="281px" AutoPostBack="True">
                <asp:ListItem Text="Matched" Value="1"></asp:ListItem>
                <asp:ListItem Text="Unmatched"  Value="2"></asp:ListItem>              
                <asp:ListItem Text="All" Selected="True" Value="3"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>

       <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="*Reference No"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlRefno"></asp:DropDownList>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="*Supplier Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlSuppliers"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Transaction Type "></asp:Label>
            <asp:textbox runat="server" AutoPostBack="true" CssClass="aspxcontrols" id="txtTrtype"></asp:textbox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Voucher No "></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlVoucherNo"></asp:DropDownList>
        </div>
    </div>
   
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Transaction Created From "></asp:Label>
            <asp:TextBox ID="txtfrom" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Transaction Created To"></asp:Label>
            <asp:TextBox ID="txtTo" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Debit"></asp:Label>
            <asp:TextBox ID="txtDebit" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Credit"></asp:Label>
            <asp:TextBox ID="txtcredit"  runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
     
    </div>

       <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:ImageButton ID="imgbtnSearch" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Search" CausesValidation="false" Height="16px" />
    </div>
     <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>

</asp:Content>

