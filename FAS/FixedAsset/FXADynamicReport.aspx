<%@ Page Title="" Language="VB" MasterPageFile="~/FixedAsset.master" AutoEventWireup="false" CodeFile="FXADynamicReport.aspx.vb" Inherits="FixedAsset_FXADynamicReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">

    Protected Sub rbtnStatus_SelectedIndexChanged1(sender As Object, e As EventArgs)

    End Sub
</script>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
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

    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Dynamic Report</b></h2>
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
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:RadioButtonList ID="rbtnStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="481px" AutoPostBack="True" >
                <asp:ListItem Text="Waiting for Approval"  Value="1" ></asp:ListItem>
                <asp:ListItem Text="Approved" Selected="True" Value="2"></asp:ListItem>
                <asp:ListItem Text="Deleted"  Value="3"></asp:ListItem>
                <asp:ListItem Text="Control Total" Value="4"></asp:ListItem>
                   <asp:ListItem Text="Asset Masters" Value="5"></asp:ListItem>
              <%--  <asp:ListItem Text="All" Selected="True" Value="5"></asp:ListItem>--%>
            </asp:RadioButtonList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* From Date"></asp:Label>
            <asp:TextBox ID="txtfrom" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* To Date"></asp:Label>
            <asp:TextBox ID="txtTo" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Asset Type "></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="drpAstype"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Item Code"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlAssetNo"></asp:DropDownList>
        </div>
    </div>
    <asp:Panel runat="server" ID="pnlAdditon" Visible="false">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Addition Reference No"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlRefno"></asp:DropDownList>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlSuppliers"></asp:DropDownList>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Addition Reasons"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlTrTypes"></asp:DropDownList>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Asset Transfer"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlAssetTrnfr"></asp:DropDownList>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Asset Age"></asp:Label>
                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtAssetAge"></asp:TextBox>
            </div>
              <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Depreciation Rate"></asp:Label>
            <asp:TextBox ID="txtdepRate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlDeletion" Visible="false">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Deletion Reference No"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlDelRefNo"></asp:DropDownList>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Deletion Reasons"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlDeletion"></asp:DropDownList>
            </div>
               <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Deletion Status"></asp:Label>
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlDelStatus"></asp:DropDownList>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="* Asset Age" Visible="false"></asp:Label>
                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtDelAsstAge" Visible="false"></asp:TextBox>
            </div>
        </div>
    </asp:Panel>
    <asp:panel runat="server" ID="pnlCommon" Visible="false">
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Asset Purchase From Date"></asp:Label>
            <asp:TextBox ID="txtPurchasefrom" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Asset Purchase To Date"></asp:Label>
            <asp:TextBox ID="txtpurchaseTo" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
    
          <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Item Description"></asp:Label>
            <asp:TextBox ID="txtItemDesc" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* ItemCode" Visible="false"></asp:Label>
            <asp:TextBox ID="txtItemcode" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
        </div>
      
    </div>
</asp:panel>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:ImageButton ID="imgbtnSearch" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Search" CausesValidation="false" Height="16px" />
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>

</asp:Content>

