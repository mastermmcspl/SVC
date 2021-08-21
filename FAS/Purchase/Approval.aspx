<%@ Page Title="" Language="VB" MasterPageFile="~/Purchase.master" AutoEventWireup="false" CodeFile="Approval.aspx.vb" Inherits="Purchase_Approval" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }

        .auto-style2 {
            width: 100%;
            height: 26px;
            resize: none;
            font-size: 12px;
            line-height: 1.42857143;
            color: #444444;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            font-family: "Segoe UI", sans-serif;
            border: 1px solid #ccc;
            padding: 3px;
            background-color: #fff;
            background-image: none;
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
            $('#<%=dgViewExcess.ClientID%>').DataTable({
            iDisplayLength: -1,
            aLengthMenu: [[-1], ["All"]],
            order: [],
            columnDefs: [{ orderable: false, targets: [0] }],
        });

        $('[data-toggle="tooltip"]').tooltip();
        $('#<%=dgViewPR.ClientID%>').DataTable({
    iDisplayLength: -1,
    aLengthMenu: [[-1], ["All"]],
    order: [],
    columnDefs: [{ orderable: false, targets: [0] }],
});
           });

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('#<%=ddlorder.ClientID%>').select2();
});
        $(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    $('#<%=ddlinvoice.ClientID%>').select2();
        });
    </script>

     <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Approval</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                  
                    <%--<asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />--%>
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <%-- <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />--%><%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplier" runat="server" ControlToValidate="ddlSupplier" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                                        <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPrint" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" />
                
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-">
            <asp:Label runat="server" Text="Purchase Order No."></asp:Label>
            <asp:DropDownList runat="server" autocomplete="off" CssClass="aspxcontrols" ID="ddlorder" AutoPostBack="True"></asp:DropDownList>
        </div>
          <div class="col-sm-4 col-md-">
            <asp:Label runat="server" Text="Purchase Invoice No."></asp:Label>
            <asp:DropDownList runat="server" autocomplete="off" CssClass="aspxcontrols" ID="ddlinvoice" AutoPostBack="True"></asp:DropDownList>
        </div>

        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Select Category"></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCategory"></asp:DropDownList>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplier" runat="server" ControlToValidate="ddlSupplier" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
       
    </div>
      <div class="col-sm-12 col-md-12 divmargin">
      
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Remarks"></asp:Label>
            <asp:textbox runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="DropDownList1"></asp:textbox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplier" runat="server" ControlToValidate="ddlSupplier" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Supplier Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
    </div>

      <div class="col-sm-12 col-md-12" style="padding-right: 0px; overflow: auto">
                   <asp:GridView ID="dgViewPR" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in"  />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Invoice No" HeaderStyle-Width="10%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblInvoice_No" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice_No") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="CommodityID" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemID" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goods" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="StdUnit" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblStdUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StdUnit") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Pieces" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblPieces" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Pieces") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="ExpectedDate" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblExpectedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExpectedDate") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="ExiceDuty" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblExiceDuty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExiceDuty") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="Frieght" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblFrieght" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Frieght") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Total" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Vat" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblVat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Vat") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="CST" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblCST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CST") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="TAXAmount" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblTAXAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TAXAmount") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="RowTotal" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblRowTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RowTotal") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="HistoryID" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
   <div class="col-sm-12 col-md-12" style="padding-right: 0px; overflow: auto">
                   <asp:GridView ID="dgViewExcess" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in"  />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>
            
                     <asp:TemplateField HeaderText="Invoice No" HeaderStyle-Width="10%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblInvoice_No" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Invoice_No") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="CommodityID" HeaderStyle-Width="13%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ItemID" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goods" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="StdUnit" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblStdUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StdUnit") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Pieces" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblPieces" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Pieces") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="ExpectedDate" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblExpectedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExpectedDate") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="ExiceDuty" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblExiceDuty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExiceDuty") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>

                          <asp:TemplateField HeaderText="Frieght" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblFrieght" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Frieght") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>

                               <asp:TemplateField HeaderText="Total" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Vat" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblVat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Vat") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="CST" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblCST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CST") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                       <asp:TemplateField HeaderText="TAXAmount" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblTAXAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TAXAmount") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>

                           <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remarks") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                           <asp:TemplateField HeaderText="RowTotal" HeaderStyle-Width="20%" Visible="true">
                        <ItemTemplate>
                            <asp:label ID="lblRowTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RowTotal") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="HistoryID" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="Status" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkStatus" runat="server"  CommandName="waiting">Waiting For Approval</asp:LinkButton>
                            <asp:label ID="lblAccept" runat="server"  CommandName="waiting" Text="Approved">Waiting For Approval</asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Flag" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblFlag" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Flag") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Statuss" HeaderStyle-Width="20%" Visible="false">
                        <ItemTemplate>
                            <asp:label ID="lblStatuss" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Statuss") %>'></asp:label>
                        </ItemTemplate>
                    </asp:TemplateField>            
                </Columns>
                     
            </asp:GridView>
        </div>
      <div class="col-sm-12 col-md-12" style="padding-right: 0px">
            <asp:button ID="btnApprove" runat="server" Text="Approve"></asp:button>
  <asp:button ID="btnReject" runat="server" Text="Reject"></asp:button>
   </div>
</asp:Content>

