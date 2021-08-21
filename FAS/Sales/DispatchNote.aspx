<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Sales.master" CodeFile="DispatchNote.aspx.vb" Inherits="Sales_DispatchNote" ValidateRequest="false" %>

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
                <h2><b>Sales Invoice Report</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    
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
        <div class="col-sm-4 col-md-4">
            <asp:Label ID="lblInvoice" Text="Order No." runat="server"></asp:Label>
              <asp:DropDownList type="search" ID="ddlInvoice" runat="server"  CssClass="aspxcontrols" AutoPostBack="True">
                </asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
             <asp:Label ID="lblDispatch" Text="Invoice No." runat="server"></asp:Label>
             <asp:DropDownList type="search" ID="ddlDispatch" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                </asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4"></div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <rsweb:ReportViewer ID="ReportViewer1" Width="100%" Height="530px"  runat="server" ></rsweb:ReportViewer>
    </div>
</asp:Content>
