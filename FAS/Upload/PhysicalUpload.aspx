<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="PhysicalUpload.aspx.vb" Inherits="Purchase_SupplierUpload" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    </style>


    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
      <link  rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
      <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">
       $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgUpload.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
              });
        $('#collapseRRIT').collapse({
            toggle: false
        })

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    
    <div class="loader"></div>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>3.3 Physical Upload</b></h2>
            </div>
            <div class="pull-right col-sm-3 col-md-3">
                <div class="pull-right ">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" />

                </div>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
   <%-- <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        
    </div>--%>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        <div class="col-sm-8 col-md-8" style="padding-left: 0; padding-right: 0; left: 0px; top: 0px;">
            <asp:Label ID="lblErrorup" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="col-sm-4 col-md-4 divmargin" style="padding-right: 0px">
            <asp:LinkButton runat="server" ID="LnkbtnExcel" Text="Download Sales Order Excel" Font-Bold="False" Font-Underline="True" ForeColor="Blue" />
        </div>
        <div class="form-group divmargin"></div>
        <%--   <div id="collapseRRIT" class="collapse">--%>
        
        <%--</div>--%>
        <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
        </div>
        <div class="divmargin "></div>        
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblSelectFile" runat="server" Text="Select a file"></asp:Label>
                <asp:FileUpload ID="FULoad" CssClass="aspxcontrols" value="Browse" name="avatar" runat="server" />
            </div>
            <asp:TextBox ID="txtPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" />
        </div>
        <div class="col-sm-1 col-md-1">
            <div class="form-group">
                <div style="margin-top: 20px;"></div>
                <asp:Button ID="btnExcelSheetName" runat="server" Text="Ok" />
            </div>
        </div>
        <div class="col-sm-4 col-md-4" style="padding-right: 0">
            <div class="form-group">
                <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name"></asp:Label>
                <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>               
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px;">
            <div class="form-group">
                <asp:GridView ID="dgUpload" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                    <Columns>
                        <asp:TemplateField HeaderText="Commidity" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblCommidity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Commidity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description of Goods" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblDescriptionofGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description of Goods") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" HeaderStyle-Width="5%" Visible ="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="unit of Measurement" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblunitofMeasurement" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "unit of Measurement") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Alternative" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblAlternative" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Alternative") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Qty in Pieces" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyinPieces" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Qty in Pieces") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="VAT" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblVAT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VAT") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                           <asp:TemplateField HeaderText="Excise" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblExcise" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Excise") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="CST" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblCST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CST") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="MRP" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblMRP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRP") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Retail" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblRetail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Retail") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Effective From" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblEffectiveFrom" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Effective From") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Effective To" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblEffectiveTo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Effective To") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                          <asp:TemplateField HeaderText="Pre determined Price" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblPredeterminedPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Pre determined Price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        
                          <asp:TemplateField HeaderText="Others" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblOthers" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Others") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        
                          <asp:TemplateField HeaderText="color" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblcolor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "color") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        
                          <asp:TemplateField HeaderText="size" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblsize" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "size") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                           
                          <asp:TemplateField HeaderText="articleNo/ColorCode" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblarticleNoColorCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "article No/Color Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Physicalquantity" HeaderStyle-Width="5%" Visible ="true">
                            <ItemTemplate>
                                <asp:Label ID="lblPhysicalquantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Physical quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                
                    </Columns>
                </asp:GridView>
            </div>
        </div>
  
    <div id="ModalFASCompanyValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCustomerValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="btnOk">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

