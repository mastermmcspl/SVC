<%@ Page Title="" Language="VB" MasterPageFile="~/Sales.master" AutoEventWireup="false" CodeFile="SalesDynamicReport.aspx.vb" Inherits="Reports_Viewer_SalesDynamicReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

    </script>
    <style>
     div {
            color: black;
        }
    </style>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Sales Dynamic Report</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnSearch" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnSink" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Sink" ValidationGroup="Validate" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
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
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblOrderNo" runat="server" Text="Order Number"></asp:Label>
            <asp:DropDownList ID="ddlorder" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblDispatch" runat="server" Text="Dispatch Number"></asp:Label>
            <asp:DropDownList ID="ddlDispatchNo" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblParty" runat="server" Text="Party"></asp:Label>
            <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
         <div class="col-sm-3 col-md-3 form-group">
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblFrom" runat="server" Text="From"></asp:Label>
            <asp:TextBox ID="txtfrom" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
            <asp:TextBox ID="txtTo" TextMode="Date" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
        </div>
        <div class="col-sm-3 col-md-3 form-group">
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblCommodity" runat="server" Text="Commodity"></asp:Label>
            <asp:DropDownList ID="ddlCommodity" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblItem" runat="server" Text="Item"></asp:Label>
            <asp:DropDownList ID="ddlItem" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
        </div>
        <div class="col-sm-3 col-md-3 form-group">
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblDiscount" runat="server" Text="Discount"></asp:Label>
            <asp:TextBox ID="txtDiscount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblVat" runat="server" Text="VAT"></asp:Label>
            <asp:TextBox ID="txtvat" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblCST" runat="server" Text="CST"></asp:Label>
            <asp:TextBox ID="txtCst" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3 form-group">
            <asp:Label ID="lblExcise" runat="server" Text="Excise"></asp:Label>
            <asp:TextBox ID="txtExcise" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Panel ID="pnlRpt" runat="server" ScrollBars="Horizontal" Width="1300px">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1290px">
            </rsweb:ReportViewer>
        </asp:Panel>
    </div>
</asp:Content>

