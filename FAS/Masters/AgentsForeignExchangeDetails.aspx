<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="AgentsForeignExchangeDetails.aspx.vb" Inherits="Masters_AgentsForeignExchangeDetails" %>

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
                div{
            color:black;
                      }    
    </style>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>6.1 Foreign Currency Agents Details</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblAgency" runat="server" Text="Existing Agency"></asp:Label>
                        <asp:DropDownList ID="ddlExistingAgency" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Agency Name"></asp:Label>
                        <asp:TextBox ID="txtAgencyName" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" CssClass="aspxcontrols"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAgencyName" runat="server" ControlToValidate="txtAgencyName" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Agency Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>                     
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Contact Person"></asp:Label>
                        <asp:TextBox ID="txtContactPerson" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" CssClass="aspxcontrols"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVContactPerson" runat="server" ControlToValidate="txtAgencyName" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Contact Person" ValidationGroup="Validate"></asp:RequiredFieldValidator>                       
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Address"></asp:Label>
                        <asp:TextBox ID="txtAddress" CssClass="aspxcontrols" runat="server" TextMode="MultiLine" Height="82px"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAddress" runat="server" ControlToValidate="txtAddress" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>                   
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Country"></asp:Label>
                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCountry" runat="server" ControlToValidate="ddlCountry" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Country" ValidationGroup="Validate"></asp:RequiredFieldValidator>                       
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Fax"></asp:Label>
                        <asp:TextBox ID="txtFax" MaxLength="10" CssClass="aspxcontrols" runat="server" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Postal Code"></asp:Label>
                        <asp:TextBox ID="txtPostalCode" MaxLength="6" CssClass="aspxcontrols" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPostalCode" runat="server" ControlToValidate="txtPostalCode" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Postal Code" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPostalCode" runat="server" ControlToValidate="txtPostalCode" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>                   
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Email"></asp:Label>
                        <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" CssClass="aspxcontrols"></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True"
                            ValidationGroup="Validate"></asp:RegularExpressionValidator>                       
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* City"></asp:Label>
                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCity" runat="server" ControlToValidate="ddlCity" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select City" ValidationGroup="Validate"></asp:RequiredFieldValidator>                 
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* State"></asp:Label>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVState" runat="server" ControlToValidate="ddlState" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select State" ValidationGroup="Validate"></asp:RequiredFieldValidator>                  
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="* Website"></asp:Label>
                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVWebsite" runat="server" ControlToValidate="txtWebsite" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Website" ValidationGroup="Validate"></asp:RequiredFieldValidator>                  
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Mobile No."></asp:Label>
                        <asp:TextBox ID="txtMobNo" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" CssClass="aspxcontrols"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMob" runat="server" ControlToValidate="txtMobNo" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Mobile No." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMob" runat="server" ControlToValidate="txtMobNo" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Telephone"></asp:Label>
                        <asp:TextBox ID="txtTelphone" MaxLength="11" CssClass=" aspxcontrols" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTelphone" runat="server" ControlToValidate="txtTelphone" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Telephone No." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTelphone" runat="server" ControlToValidate="txtTelphone" Display="Dynamic"
                            SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>                        
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* GSTN Category"></asp:Label>
                        <asp:DropDownList ID="ddlGSTNCategory" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVGSTNCategory" runat="server" ControlToValidate="ddlGSTNCategory" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select GSTN Category" ValidationGroup="Validate"></asp:RequiredFieldValidator>                    
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* GSTN Reg.No"></asp:Label>
                        <asp:TextBox ID="txtGSTNRegNo" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVGSTNRegNo" runat="server" ControlToValidate="txtGSTNRegNo" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter GSTN Reg.No" ValidationGroup="Validate"></asp:RequiredFieldValidator>                     
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Bank Name"></asp:Label>
                        <asp:TextBox ID="txtBankName" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBankName" runat="server" ControlToValidate="txtBankName" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Bank Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>                  
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Account No"></asp:Label>
                        <asp:TextBox ID="txtAccNo" CssClass="aspxcontrols" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REQFVAccNo" runat="server" ControlToValidate="txtAccNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Account No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAccNo" runat="server" ControlToValidate="txtAccNo" Display="Dynamic" ErrorMessage="Enter number of (Max 25 length) , character/Spaces not allowed." SetFocusOnError="True" ValidationExpression="^[0-9]{0,25}" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAccNo" runat="server" ControlToValidate="txtAccNo" Display="Dynamic" ErrorMessage="Enter only integers (Max length 12), character/Strings not allowed." SetFocusOnError="True" ValidationExpression="^[0-9]{12}"></asp:RegularExpressionValidator>    --%>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* IFSC Code"></asp:Label>
                        <asp:TextBox ID="txtIFSCode" CssClass="aspxcontrols" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REQFIFSCode" runat="server" ControlToValidate="txtIFSCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter IFSC Code" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Branch Name"></asp:Label>
                        <asp:TextBox ID="txtBranchName" CssClass="aspxcontrols" runat="server" autocomplete="off"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REQFBranchName" runat="server" ControlToValidate="txtBranchName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Branch Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-3 col-md-3" style="padding: 0px">
            <div class="form-group">
                <asp:Label ID="lblCurrency" runat="server" Text="* Currency"></asp:Label>
                <div class="aspxcontrols" style="height: 310px; overflow:scroll">
                    <asp:CheckBoxList ID="ChkCurrency" runat="server" CssClass="myCheckbox"></asp:CheckBoxList>
                </div>
            </div>
        </div>
   </div>
    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS Pro</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsg" runat="server"></asp:Label>
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
