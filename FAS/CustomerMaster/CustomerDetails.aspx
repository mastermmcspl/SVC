<%@ Page Title="" Language="VB" MasterPageFile="~/CustomerMaster/Home.master" AutoEventWireup="false" CodeFile="CustomerDetails.aspx.vb" Inherits="CustomerMaster_CustomerDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <link rel="stylesheet" href="StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/sweetalert.css" rel="stylesheet" type="text/css" />

    <script src="JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/sweetalert-dev.js" type="text/javascript"></script>
    <script src="JavaScripts/aes.js" type="text/javascript"></script>s
    <script src="JavaScripts/html5shiv.js" type="text/javascript"></script>
    <script src="JavaScripts/respond.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $('#btnSave').click(function () {
                if ($("#ddlPDProductInterest").val() == "") {
                    $('#lblValidationMsg').html("Select the Product.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
                if ($("#ddlPDReason").val() == "") {
                    $('#lblValidationMsg').html("Select Reason for looking at the Website.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
                return true;
            })
        });
    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn" style="text-align: center">
            <div class="col-sm-12 col-md-12">
                <h2><b>Customer Request Form</b></h2>
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Backward24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12">
        <h4><b>Fill the form for free Product demonstration</b></h4>
    </div>

    <div class="col-sm-12 col-md-12">
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCDCompanyName" runat="server" ControlToValidate="txtCDCompanyName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCDCompanyName" runat="server" ControlToValidate="txtCDCompanyName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:Label ID="lblCDCompanyName" runat="server" Text="* Company Name"></asp:Label>
                <asp:TextBox ID="txtCDCompanyName" runat="server" CssClass="aspxcontrols" TabIndex="1"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCDCompanyWebsite" runat="server" ControlToValidate="txtCDCompanyWebsite" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCDCompanyWebsite" runat="server" ControlToValidate="txtCDCompanyWebsite" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:Label ID="lblCDCompanyWebsite" runat="server" Text="* Website"></asp:Label>
                <asp:TextBox ID="txtCDCompanyWebsite" runat="server" CssClass="aspxcontrols" TabIndex="2"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCDCompanyEmailID" runat="server" ControlToValidate="txtCDCompanyEmailID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCDCompanyEmailID" runat="server" ControlToValidate="txtCDCompanyEmailID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:Label ID="lblCDCompanyEmailID" runat="server" Text="* Email ID"></asp:Label>
                <asp:TextBox ID="txtCDCompanyEmailID" runat="server" CssClass="aspxcontrols" TabIndex="3"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCDContactPerson" runat="server" ControlToValidate="txtCDContactPerson" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCDContactPerson" runat="server" ControlToValidate="txtCDContactPerson" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:Label ID="lblCDContactPerson" runat="server" Text="* Contact Person"></asp:Label>
                <asp:TextBox ID="txtCDContactPerson" runat="server" CssClass="aspxcontrols" TabIndex="4"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPDMobileno" runat="server" ControlToValidate="txtPDMobileno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPDMobileno" runat="server" ControlToValidate="txtPDMobileno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:Label ID="lblPDMobileno" runat="server" Text="* Mobile No."></asp:Label>
                <asp:TextBox ID="txtPDMobileno" runat="server" CssClass="aspxcontrols" MaxLength="10" TabIndex="5"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCDCompanyTelephoneno" runat="server" ControlToValidate="txtCDCompanyTelephoneno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:Label ID="lblCDCompanyTelephoneno" runat="server" Text="Telephone No."></asp:Label>
                <asp:TextBox ID="txtCDCompanyTelephoneno" runat="server" CssClass="aspxcontrols" TabIndex="6"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCDCompanyAddress" runat="server" ControlToValidate="txtCDCompanyAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:Label ID="lblCDCompanyAddress" runat="server" Text="Address"></asp:Label>
                <asp:TextBox ID="txtCDCompanyAddress" runat="server" CssClass="aspxcontrols" TabIndex="7" TextMode="MultiLine" Height="83px"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblProductInterest" runat="server" Text="* Product Interest"></asp:Label>
                <asp:DropDownList ID="ddlPDProductInterest" runat="server" CssClass="aspxcontrols" TabIndex="8">
                    <asp:ListItem Value="0">Select Product</asp:ListItem>
                    <asp:ListItem Value="1">TRACe PA</asp:ListItem>
                    <asp:ListItem Value="2">TRACe Enterprise</asp:ListItem>
                    <asp:ListItem Value="3">EDICT</asp:ListItem>
                    <asp:ListItem Value="4">FAS Pro</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblPDReason" runat="server" Text="* Reason for looking at the Website "></asp:Label>
                <asp:DropDownList ID="ddlPDReason" runat="server" CssClass="aspxcontrols" TabIndex="9">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="1">Professional interest</asp:ListItem>
                    <asp:ListItem Value="2">Company is looking for a similar Product</asp:ListItem>
                    <asp:ListItem Value="3">Consultant - Recommend to the clients</asp:ListItem>
                    <asp:ListItem Value="4">Marketing</asp:ListItem>
                    <asp:ListItem Value="5">Implementation support</asp:ListItem>
                    <asp:ListItem Value="6">Other</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblPDAboutUs" runat="server" Text="How did you find out about us?"></asp:Label>
                <asp:DropDownList ID="ddlPDAboutUs" runat="server" CssClass="aspxcontrols" TabIndex="10">
                    <asp:ListItem Value="0">Select Product</asp:ListItem>
                    <asp:ListItem Value="1">Advertisement</asp:ListItem>
                    <asp:ListItem Value="2">Article or blog post</asp:ListItem>
                    <asp:ListItem Value="3">Internet</asp:ListItem>
                    <asp:ListItem Value="4">Social media</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Button ID="btnSave" runat="server" Text="Confirm Request" CssClass="btn-ok" ValidationGroup="Validate" />
                <asp:Button ID="btnCancel" runat="server" Text="Reset" CssClass="btn-ok" />
            </div>
        </div>
    </div>

    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>MMCSPL</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-warning">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsg" runat="server"></asp:Label></strong>
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

