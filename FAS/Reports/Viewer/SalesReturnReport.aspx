<%@ Page Title="" Language="VB" MasterPageFile="~/Sales.master" AutoEventWireup="false" CodeFile="SalesReturnReport.aspx.vb" Inherits="Reports_Viewer_SalesReturnReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
                                
     </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Sales Return Report</b></h2>
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
        <div class="col-sm-6 col-md-6">
             <asp:Label ID="lblSalesReturn" Text="Sales Return No." runat="server"></asp:Label>
             <asp:DropDownList type="search" ID="ddlSalesReturn" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                </asp:DropDownList>
        </div>
        <div class="col-sm-6 col-md-6">

        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <rsweb:ReportViewer ID="ReportViewer1" Width="99%" Height="530px"  runat="server" ></rsweb:ReportViewer>
    </div>
</asp:Content>

