<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="FrmLgstDieselMaster.aspx.vb" Inherits="Masters_FrmLgstDieselMaster" %>

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
             $('#<%=ddlExistingDieselNo.ClientID%>').select2();
             $('#<%=ddlCity.ClientID%>').select2();
            $('#<%=ddlState.ClientID%>').select2();
       });
        function validatePage() {
            //Executes all the validation controls associated with group1 validaiton Group1. 
            var flag = window.Page_ClientValidate('Validate');
            if (flag)
                return flag;

        }
    </script>

    <style>
        div {
            color: black;
        }
    </style>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>Diesel/Petrol Pump Master Details</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" Visible="false" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Diesel/Petrol Pump No"></asp:Label>
            <asp:DropDownList ID="ddlExistingDieselNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3"></div>
        <div class="col-sm-3 col-md-3"></div>
        <div class="col-sm-3 col-md-3">
            <br />
            <div class="form-group" runat="server">
                <asp:Label runat="server" Text="Status :-" Visible="false"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Diesel/Petrol Pump Name"></asp:Label>
            <asp:TextBox ID="txtDieselName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDieselName" runat="server" ControlToValidate="txtDieselName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Diesel/Petrol Pump Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Contact Person"></asp:Label>
            <asp:TextBox ID="txtContactPerson" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVContactPerson" runat="server" ControlToValidate="txtContactPerson" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Contact Person Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Mobile No."></asp:Label>
            <asp:TextBox ID="txtMobileNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
          <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMobile" runat="server" ControlToValidate="txtMobileNo" Display="Dynamic" ErrorMessage="Enter Valid Mobile No." SetFocusOnError="True" ValidationExpression="^[0-9\s]{10}$" ValidationGroup="Validate"></asp:RegularExpressionValidator>
      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMobileNo" runat="server" ControlToValidate="txtMobileNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Contact Number" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                 </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Registration"></asp:Label>
            <asp:TextBox ID="txtRegistrationNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRegistration" runat="server" ControlToValidate="txtRegistrationNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Registration No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text=" Details"></asp:Label>
            <asp:TextBox ID="txtDetails" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
     <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDetails" runat="server" ControlToValidate="txtDetails" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Details" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* GST No"></asp:Label>
            <asp:TextBox ID="txtGSTNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
       <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVGSTNo" runat="server" ControlToValidate="txtGSTNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter GSTN No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
       <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVGSTNo" runat="server" ValidationGroup="Validate" ControlToValidate="txtGSTNo" Display="Dynamic" ErrorMessage="Enter Valid GSTN Reg.No." SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>
                </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Address"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" autocomplete="off" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
           <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAddress" runat="server" ControlToValidate="txtAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Address"  ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="PinCode"></asp:Label>
            <asp:TextBox ID="txtPinCode" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        <%--    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPinCode" runat="server" ControlToValidate="txtPinCode" Display="Dynamic" ErrorMessage="Enter Valid PinCode." SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,6}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="City"></asp:Label>
            <asp:DropDownList ID="ddlCity" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="State"></asp:Label>
            <asp:DropDownList ID="ddlState" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
    </div>
    <div class="col-md-12">
        <div id="divBankDetails" runat="server" visible="false" data-toggle="collapse" data-target="#collapseBankDetails"><a href="#"><b><i>Click here to create Bank Details...</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseBankDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group">
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label runat="server" Text="Account No"></asp:Label>
                    <asp:TextBox ID="txtAccountNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAccountNo" runat="server" ControlToValidate="txtAccountNo" Display="Dynamic" ErrorMessage="Enter Valid Account No" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,15}$"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Bank Name"></asp:Label>
                    <asp:TextBox ID="txtBankName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBankName" runat="server" ControlToValidate="txtBankName" Display="Dynamic" ErrorMessage="Enter Valid Bank Name" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,50}$"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-4 col-md-4">
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group">
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label runat="server" Text="IFSC Code"></asp:Label>
                    <asp:TextBox ID="txtIFSCCode" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFSCCode" runat="server" ControlToValidate="txtIFSCCode" Display="Dynamic" ErrorMessage="Enter Valid IFSCCode" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,15}$"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Branch Name"></asp:Label>
                    <asp:TextBox ID="txtBranchName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBranchName" runat="server" ControlToValidate="txtBranchName" Display="Dynamic" ErrorMessage="Enter Valid BranchName" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,50}$"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnBankSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ImageUrl="~/Images/Save16.png" ValidationGroup="ValidateBank" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group">
                <asp:DataGrid ID="dgBankDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="75%" class="footable">
                    <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                    <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="AccountNo" HeaderText="Account No">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="BankName" HeaderText="Bank Name">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="IFSCCode" HeaderText="IFSC Code">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="BranchName" HeaderText="Branch Name">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Edit" CommandName="Edit" data-placement="bottom" ImageUrl="~\Images\Edit16.png" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" ImageUrl="~\Images\4delete.gif" data-placement="bottom" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateColumn>

                    </Columns>
                </asp:DataGrid>
            </div>

        </div>
    </div>
      <div class="col-sm-12 col-md-12 form-group">
        <asp:TextBox ID ="txtBankID" runat ="server" Visible ="false" ></asp:TextBox>
    </div> 
    <div id="ModalFASVehicleValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblDieselValidationMsg" runat="server"></asp:Label>
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
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:TextBox ID ="txtGLID" runat ="server" Visible ="false" ></asp:TextBox>
    </div>
</asp:Content>
