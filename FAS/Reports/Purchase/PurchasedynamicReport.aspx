<%@ Page Title="" Language="VB" MasterPageFile="~/Purchase.master" AutoEventWireup="false" CodeFile="PurchasedynamicReport.aspx.vb" Inherits="Reports_Purchase_PurchasedynamicReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div {
            color: black;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Purchase Dynamic Report</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="LabelError"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:RadioButtonList ID="rbtlCat" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="281px" AutoPostBack="True">
            <asp:ListItem Text="Accepted" Selected="True" Value="1"></asp:ListItem>
            <asp:ListItem Text="Rejected" Value="2"></asp:ListItem>
            <asp:ListItem Text="Excess" Value="3"></asp:ListItem>
            <asp:ListItem Text="Sink" Value="4"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order No"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlorder"></asp:DropDownList>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlSuppliers"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice "></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlInvoiceNo"></asp:DropDownList>
        </div>


    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* From "></asp:Label>
            <asp:TextBox ID="txtfrom" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* To"></asp:Label>
            <asp:TextBox ID="txtTo" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Brand"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCommodity"></asp:DropDownList>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Item"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlItem"></asp:DropDownList>
        </div>
    </div>



    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Vat"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtvat"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Cst"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtCst"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Excise"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtExcise"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Discount"></asp:Label>
            <asp:TextBox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="txtDiscount"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:ImageButton ID="imgbtnSearch" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" CausesValidation="false" Height="16px" />
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>
</asp:Content>
