<%@ Page Title="" Language="VB" MasterPageFile="~/Purchase.master" AutoEventWireup="false" CodeFile="PurchaseItemWiseVReport.aspx.vb" Inherits="Reports_Purchase_PurchaseItemWiseVReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <style>
        div {
            color: black;
        }
    </style>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Purchase Invoice</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <%--<asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />--%>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="LabelError"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Order No"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlorder"></asp:DropDownList>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice No"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlinvoice"></asp:DropDownList>
        </div>


    </div>

    <%--  <table>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblErrorUp" runat="server" CssClass="LabelError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="Label">
                <asp:Label ID="lblOrderNo" runat="server" Text="Order No" CssClass="Label" Width="150px"></asp:Label>
            </td>
            <td class="td_blue1b">
                <asp:DropDownList ID="ddlorder" runat="server" AutoPostBack="True" CssClass="DropDownList" Width="200px" style="height: 19px"></asp:DropDownList>
            </td>
            <td class="Label">
                <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No" CssClass="Label" Width="150px"></asp:Label>
            </td>
            <td class="td_blue1b">
                <asp:DropDownList ID="ddlinvoice" runat="server" AutoPostBack="True" CssClass="DropDownList" Width="200px"></asp:DropDownList>
            </td>
        </tr>
    </table>

    <table>
        <tr>
            <td>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
            </td>
        </tr>
    </table>--%>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>
</asp:Content>

