<%@ Page Title="" Language="VB" MasterPageFile="~/FixedAsset.master" AutoEventWireup="false" CodeFile="AssetOpeningBalExcelUpload.aspx.vb" Inherits="FixedAsset_AssetOpeningBalExcelUpload" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <style>
          div{
            color:black;
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

    <script lang="javascript" type="text/javascript">
      $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=GvOPExcel.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
          
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>
      <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-8 col-md-8 pull-left">
                <h2><b>Asset Opening Balance Excel Upload</b></h2>
            </div>     
            <div class="col-sm-3 col-md-3 pull-right">
                <div class="pull-right">
                      <asp:ImageButton ID="ImgBtnBack"  CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false"/>
                    <asp:ImageButton ID="imgbtUpload" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save Details"  />    
                      <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />                                                            
                </div>
            </div>
        </div>
    </div>

      <div class="col-sm-12 col-md-12" style="padding-left: 0px">        
       <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>

    <div class="clearfix divmargin"></div>

      <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0">
            <asp:Label ID="Label1" runat="server" CssClass="ErrorMsgLeft" ForeColor="Red"></asp:Label>
            <div class="col-sm-4 col-md-4 pull-right ">
                <asp:LinkButton ID="lnDown" runat="server">Download sample excel file</asp:LinkButton>
            </div>
        </div>
  
      <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic"  ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic"  ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>          
    </div>
   <div class="col-sm-3 col-md-3">
        <div class="form-group">
            <br />
            <asp:Label ID="lblSelectFile" runat="server" Text=""></asp:Label>
            <asp:FileUpload ID="FULoad" CssClass="aspxcontrols" runat="server" />
        </div>
        <asp:TextBox ID="txtPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" Style="height: 21px" />
    </div>
    <div class="col-sm-1 col-md-1">
        <div class="form-group">
            <div style="margin-top: 20px;"></div>
            <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOk_Click" />
        </div>
    </div>
    <div class="col-sm-3 col-md-3 pull-center" style="padding-right: 0">
        <div class="form-group">
            <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name"></asp:Label>
            <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
    </div> 
        <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto"">
            <asp:GridView ID="GvOPExcel" runat="server" CssClass="footable" AutoGenerateColumns="False">
                <Columns>             
                    <asp:TemplateField HeaderText="Slno">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Slno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AssetTransfer">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetTransfer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetTransfer") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Currency Types">
                        <ItemTemplate>
                            <asp:Label ID="lblCurrencyTypes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CurrencyTypes") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="currency Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblcurrencyAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "currencyAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                  
                      <asp:TemplateField HeaderText="Actual Location">
                        <ItemTemplate>
                            <asp:Label ID="lblActualLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualLocation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Asset Age">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetAge" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetAge") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Transaction Type">
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Supplier Name">
                        <ItemTemplate>
                            <asp:Label ID="lblSupplierName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="supplier Code">
                        <ItemTemplate>
                            <asp:Label ID="lblsupplierCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "supplierCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Asset Type">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                  
                     <asp:TemplateField HeaderText="Asset RefNo">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetRefNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetRefNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Item Description">
                        <ItemTemplate>
                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Date of Purchase">
                        <ItemTemplate>
                            <asp:Label ID="lblDateofPurchase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateofPurchase") %>'></asp:Label>                         
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Date Of Commission">
                        <ItemTemplate>
                            <asp:Label ID="lblDateOfCommission" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfCommission") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                      <asp:TemplateField HeaderText="Basic Value">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Depreciation">
                        <ItemTemplate>
                            <asp:Label ID="lblDeprcn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Depreciation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    
    <div id="ModalFASFXDOpExcel" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblFXOPBalExcelMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

