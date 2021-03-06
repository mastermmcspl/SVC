<%@ Page Title="" Language="VB" MasterPageFile="~/Sales.master" AutoEventWireup="false" CodeFile="SalesPartyUpload.aspx.vb" Inherits="Sales_SalesPartyUpload" %>

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
                <h2><b>Sales Customer Upload</b></h2>
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
        <div class="col-sm-8 col-md-8" style="padding-left: 0; padding-right: 0">
            <asp:Label ID="lblErrorup" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="col-sm-4 col-md-4 divmargin" style="padding-right: 0px">
            <asp:LinkButton runat="server" ID="LnkbtnExcel" Text="Customer Upload Excel" Font-Bold="False" Font-Underline="True" ForeColor="Blue" />
        </div>
        <div class="form-group divmargin"></div>
        <%--   <div id="collapseRRIT" class="collapse">--%>
        <div class="col-sm-12 col-md-12" style="padding: 0px;">
            <div class="form-group">
                <asp:GridView ID="dgUpload" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                    <Columns>
                        <asp:TemplateField HeaderText="PreFix" HeaderStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="lblPreFix" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PreFix") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code" HeaderStyle-Width="25%">
                            <ItemTemplate>
                                <asp:Label ID="lblCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblAddress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address1" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblAddress1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Address1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address2" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblAddress2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Address2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address3" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblAddress3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Address3") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PinCode" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblPinCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PinCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ContactPerson" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblContactPerson" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="OfficePhoneNo" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblOfficePhoneNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OfficePhoneNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile No" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Mobile No") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email ID" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblEmailID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Email ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "State") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fax" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblFax" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Fax") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TIN" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblTIN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TIN") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
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
    <%--<div class="col-md-12" style="padding-left: 0; padding-right: 0">
        <div id="divExcel" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; width: 100%;">
            <asp:DataGrid ID="dgGeneral" runat="server" AutoGenerateColumns="true" AllowPaging="false" class="footable">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" HorizontalAlign="Left" VerticalAlign="Top" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
            </asp:DataGrid>
        </div>
    </div>--%>
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

