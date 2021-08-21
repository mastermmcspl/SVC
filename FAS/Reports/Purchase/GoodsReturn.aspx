<%@ Page Title="" Language="VB" MasterPageFile="~/Purchase.master" AutoEventWireup="false" CodeFile="GoodsReturn.aspx.vb" Inherits="Reports_Purchase_GoodsReturn" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
       <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Goods Return Memo</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
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
              
                 <rsweb:ReportViewer ID="ReportViewer1" runat="server" width="100%" Height="547px" InternalBorderColor="Blue" LinkDisabledColor="Blue"  OnLoad="ReportViewer_OnLoad" PageCountMode="Actual" ToolBarItemBorderColor="DarkRed">
                    <LocalReport ReportPath="PurchaseReturn.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet1" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="PurchaseOrderTableAdapters.DataTable1TableAdapter"></asp:ObjectDataSource>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="DataSetTableAdapters.DataTable1TableAdapter"></asp:ObjectDataSource>
           </div>
</asp:Content>

