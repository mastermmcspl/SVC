<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="SalesPartyMaster.aspx.vb" Inherits="Sales_SalesPartyMaster" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            $('#<%=grdMaster.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
              });
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>2.3 Sales Customer Master</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save"  ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <%--<asp:ImageButton ID="imgbtnDelete" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" ValidationGroup="Validate" />--%>
                    <%--<asp:Button ID ="btnDelete" Text ="Delete" runat ="server" />--%>
                    <asp:Label ID="lblID" runat="server" Visible ="false" ></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblErrorUp" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Customer Name"></asp:Label> 
            <asp:TextBox ID="txtSupplierName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomerName" runat="server" ControlToValidate="txtSupplierName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Customer Name" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSupplierName" runat="server" ControlToValidate="txtSupplierName" Display="Dynamic" ErrorMessage="Enter Valid Customer Name.(Maximum size 100)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Customer Code"></asp:Label>
            <asp:TextBox ID="txtSupplierCode" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomerCode" runat="server" ControlToValidate="txtSupplierCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Customer Code" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSupplierCode" runat="server" ControlToValidate="txtSupplierCode" Display="Dynamic" ErrorMessage="Enter Valid Customer Code.(Maximum size 100)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9-\s]{1,100}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Category"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label runat="server" Text="Status :-"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Contact Person"></asp:Label>
            <asp:TextBox ID="txtConatctPerson" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVContactPerson" runat="server" ControlToValidate="txtConatctPerson" Display="Dynamic" ErrorMessage="Enter Valid Contact Person.(Maximum size 100)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z\s]{1,100}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Enter Valid Email" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Mobile No"></asp:Label>
            <asp:TextBox ID="txtMobile" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMobile" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="Enter Valid Mobile No.(Maximum size 10)" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,10}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="LandLine"></asp:Label>
            <asp:TextBox ID="txtLandLine" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVLandLine" runat="server" ControlToValidate="txtLandLine" Display="Dynamic" ErrorMessage="Enter Valid LandLine No.(Maximum size 15)" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,15}$"></asp:RegularExpressionValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Fax No"></asp:Label>
            <asp:TextBox ID="txtFax" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFax" runat="server" ControlToValidate="txtFax" Display="Dynamic" ErrorMessage="Enter Valid FAX.(Maximum size 12)" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,12}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="PinCode"></asp:Label>
            <asp:TextBox ID="txtPinCode" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPinCode" runat="server" ControlToValidate="txtPinCode" Display="Dynamic" ErrorMessage="Enter Valid PinCode.(Maximum size 6)" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,6}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="City"></asp:Label>
            <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="State"></asp:Label>
            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Address"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
             <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddress" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="Address Maximum size 1000" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9/'@&#.,\s]{1,1000}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Address1"></asp:Label>
            <asp:TextBox ID="txtAddress1" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
             <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddress1" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="Address1 Maximum size 1000" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9/'@&#.,\s]{1,1000}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Address2"></asp:Label>
            <asp:TextBox ID="txtAddress2" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
             <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddress2" runat="server" ControlToValidate="txtAddress2" Display="Dynamic" ErrorMessage="Address2 Maximum size 1000" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9/'@&#.,\s]{1,1000}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Address3"></asp:Label>
            <asp:TextBox ID="txtAddress3" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
             <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddress3" runat="server" ControlToValidate="txtAddress3" Display="Dynamic" ErrorMessage="Address3 Maximum size 1000" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9/'@&#.,\s]{1,1000}$"></asp:RegularExpressionValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:ImageButton ID="imgbtnCreateVAT" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create Customer VAT,TIN,TAN,PAN etc" />
    </div>

    <%--<div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Panel ID="pnlDetails" runat="server" CssClass="td_blue1b">
            <asp:Label runat="server" Text="Create Custom fields such as VAT, TAX, PAN, TAN, TIN, CIN and others"></asp:Label>
            <asp:GridView ID="dgOtherDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                <Columns>
                    <asp:TemplateField HeaderText="sID" Visible="false" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblsID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Statutory Name" Visible="false" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblStatutoryName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Statutory Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Statutory Value" Visible="false" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblStatutoryValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Statutory Value") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Delete" Visible="false" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ToolTip="Delete" ImageUrl="~/Images/4delete.gif" />
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
            </asp:GridView>
             <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="StatutoryName"></asp:Label>
                    <asp:TextBox ID="txtStatutoryName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="StatutoryValue"></asp:Label>
                    <asp:TextBox ID="txtStatutoryValue" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Button ID="btnStatutoryAdd" runat="server" Text="Add" Width="42px" />
                </div>
            </div>
        </asp:Panel>
        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender4" runat="server" CollapseControlID="pnlOtherDetails"
            Collapsed="true" ExpandControlID="pnlOtherDetails" TextLabelID="lblMessage"
            CollapsedText="Show" ExpandedText="Hide" ImageControlID="imgbtnDetails" CollapsedImage="../Images/Collapse.jpg"
            ExpandedImage="../Images/Expand.jpg" ExpandDirection="Vertical" TargetControlID="pnlDetails"
            ScrollContents="false">
        </cc1:CollapsiblePanelExtender>
    </div>--%>



    <div class=" modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Create Custom fields such as VAT, TAX, PAN, TAN, TIN, CIN and others</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <asp:GridView ID="dgOtherDetails" runat="server" Visible ="true"  AutoGenerateColumns="False" class="footable">
                            <Columns>
                                <asp:TemplateField HeaderText="sID" Visible="false" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Statutory Name" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatutoryName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Statutory Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Statutory Value" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatutoryValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Statutory Value") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ToolTip="Delete" ImageUrl="~/Images/4delete.gif" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="StatutoryName"></asp:Label>
                            <asp:TextBox ID="txtStatutoryName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStatutoryName" runat="server" ControlToValidate="txtStatutoryName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Statutory Name" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStatutoryName" runat="server" ControlToValidate="txtStatutoryName" Display="Dynamic" ErrorMessage="Enter Valid Statutory Name.(Maximum size 3)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z\s]{1,3}$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="StatutoryValue"></asp:Label>
                            <asp:TextBox ID="txtStatutoryValue" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStatutoryValue" runat="server" ControlToValidate="txtStatutoryValue" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Statutory Value" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStatutoryValue" runat="server" ControlToValidate="txtStatutoryValue" Display="Dynamic" ErrorMessage="Enter Valid Statutory Value.(Maximum size 50)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,50}$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Button ID="btnStatutoryAdd" runat="server" Text="Add" Width="42px" ValidationGroup="ValidateAdd" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:ImageButton ID="imgNAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" CausesValidation="false" ValidationGroup="Validate" />
                    <asp:ImageButton ID="ImgClose" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="ImgNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Close" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Panel ID="pnlSalesSupplier" runat="server" ScrollBars="Auto">
            <asp:GridView ID="grdMaster" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                <Columns>
                    <asp:TemplateField HeaderText="CSM_ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCSM_ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CSM_ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Party Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:LinkButton ID="Description" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="35%">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Address") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="State" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblState" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "State") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pincode" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblPincode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Pincode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblFlagVal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FlagVal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
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

