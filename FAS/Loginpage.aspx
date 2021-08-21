<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Loginpage.aspx.vb" Inherits="Loginpage" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <title>FAS</title>
    <link rel="stylesheet" href="StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/login.css" type="text/css" />
    <script src="JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="JavaScripts/aes.js" type="text/javascript"></script>
    <script src="JavaScripts/html5shiv.js" type="text/javascript"></script>

    <script src="JavaScripts/respond.min.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $("#txtUserName").focus();
            $('#btnOk').click(function () {
                $('#ModalValidation').modal('hide');
                $('#ModalYesNo').modal('hide');
                $('#ModalChangePassword').modal('hide');
                $('#ModalOKtoCP').modal('hide');
                $('#ModalPEAYesNo').modal('hide');
                $('#ModalForgotPassword').modal('hide');

                if ($("#txtAccessCode").val() == "") {
                    $("#txtAccessCode").focus();
                    return false;
                }
                if ($("#txtUserName").val() == "") {
                    $("#txtUserName").focus();
                    return false;
                }
                if ($("#txtPassword").val() == "") {
                    $("#txtPassword").focus();
                    return false;
                }
            })

            $('#lnkbtnForgotPassword').click(function () {
                $('#ModalYesNo').modal('hide');
                $('#ModalChangePassword').modal('hide');
                $('#ModalOKtoCP').modal('hide');
                $('#ModalPEAYesNo').modal('hide');
                $('#ModalForgotPassword').modal('hide');

                if ($("#txtAccessCode").val() == "") {
                    $('#lblValidationMsg').html("Enter access code.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
                if ($("#txtUserName").val() == "") {
                    $('#lblValidationMsg').html("Enter user name.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
            })

            $('#imgbtnLogin').click(function () {
                document.getElementById('<%=txtScreenWidth.ClientID %>').value = $(window).width();
                document.getElementById('<%=txtScreenHeight.ClientID %>').value = $(window).height();
                $('#ModalYesNo').modal('hide');
                $('#ModalChangePassword').modal('hide');
                $('#ModalOKtoCP').modal('hide');
                $('#ModalPEAYesNo').modal('hide');
                $('#ModalForgotPassword').modal('hide');

                if ($("#txtAccessCode").val() == "") {
                    $('#lblValidationMsg').html("Enter access code.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
                if ($("#txtUserName").val() == "") {
                    $('#lblValidationMsg').html("Enter user name.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
                if ($("#txtPassword").val() == "") {
                    $('#lblValidationMsg').html("Enter password.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }

                var key = CryptoJS.enc.Utf8.parse('8080808080808080');
                var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
                var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#txtPassword").val()), key,
                {
                    keySize: 128,
                    iv: iv,
                    mode: CryptoJS.mode.CBC,
                    padding: CryptoJS.pad.Pkcs7
                });
                document.getElementById('<%=txtActualPassword.ClientID %>').value = encrypted
                document.getElementById('<%=txtPassword.ClientID %>').value = ''
                document.getElementById('<%=txtPassword.ClientID %>').type = 'text'
                return true;
            })
        });
    </script>
</head>
<body>
    <div id="container" class="img-fluid login">
        <div class="col-md-3 col-md-offset-4 col-sm-4 col-sm-offset-3">
            <form role="form" runat="server">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <asp:ImageButton ID="imgbtnLoginLog" runat="server" Height="55" Width="170" Style="margin: 0px 0px 0px 25px" />
                    </div>
                    <div class="panel-body" style="padding-top: 20px" id="loginform">
                        <div class="alert alert-danger col-sm-12" id="login-alert" style="display: none"></div>
                        <div class="input-group" style="margin-bottom: 15px">
                            <span class="input-group-addon"><span class="glyphicon-access"></span></span>
                            <asp:TextBox autocomplete="off" AutoCompleteType="Disabled" ID="txtAccessCode" runat="server" placeholder="Access Code" class="form-control"></asp:TextBox>
                        </div>
                        <div class="input-group" style="margin-bottom: 15px">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user1"></i></span>
                            <asp:TextBox autocomplete="off" AutoCompleteType="Disabled" ID="txtUserName" runat="server" placeholder="User Name" value="Sa" class="form-control" onpaste="return false" oncopy="return false"></asp:TextBox>
                        </div>
                        <div class="input-group" style="margin-bottom: 15px">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-password"></i></span>
                            <asp:TextBox autocomplete="off" AutoCompleteType="Disabled" ID="txtPassword" runat="server" placeholder="Password" class="form-control" TextMode="Password" onpaste="return false" oncopy="return false"></asp:TextBox>
                        </div>
                        <div style="margin-top: 15px;">
                            <asp:ImageButton ID="imgbtnLogin" runat="server" OnClick="btnLogin_Click" Width="280px" />
                            <div class="clearfix"></div>
                        </div>
                        <div class="forgotPwd">
                            <asp:LinkButton runat="server" ID="lnkbtnForgotPassword" Text="Forgot password?" OnClick="lnkbtnForgotPassword_Click"></asp:LinkButton>
                        </div>
                        <div>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div>
                            <asp:HiddenField ID="txtActualPassword" runat="server" />
                            <asp:HiddenField ID="txtScreenWidth" runat="server" />
                            <asp:HiddenField ID="txtScreenHeight" runat="server" />
                        </div>
                    </div>
                </div>

                <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>FAS</b></h4>
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

                <div id="ModalOKtoCP" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>FAS</b></h4>
                            </div>
                            <div class="modalmsg-body">
                                <div id="divOKtoCP" class="alert alert-warning">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblOKtoCP" runat="server"></asp:Label></strong>
                                    </p>
                                </div>
                            </div>
                            <div class="modalmsg-footer">
                                <button data-dismiss="modal" runat="server" class="btn-ok" id="btnOKtoCP">
                                    OK
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalYesNo" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>FAS</b></h4>
                            </div>
                            <div class="modalmsg-body">
                                <div id="divYesNoMsgType" class="alert alert-warning">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblYesNoMsg" runat="server"></asp:Label></strong>
                                    </p>
                                </div>
                            </div>
                            <div class="modalmsg-footer">
                                <button runat="server" class="btn-ok" id="btnYES">
                                    Yes
                                </button>
                                <button data-dismiss="modal" runat="server" class="btn-ok" id="btnNO">
                                    No
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalPEAYesNo" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>FAS</b></h4>
                            </div>
                            <div class="modalmsg-body">
                                <div id="divPEAYesNoMsgType" class="alert alert-warning">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblPEAYesNoMsg" runat="server"></asp:Label></strong>
                                    </p>
                                </div>
                            </div>
                            <div class="modalmsg-footer">
                                <asp:Button runat="server" Text="Yes" class="btn-ok" ID="btnPEAYes" OnClick="btnPEAYes_Click"></asp:Button>
                                <asp:Button runat="server" Text="No" class="btn-ok" ID="btnPEANo" OnClick="btnPEANo_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalChangePassword" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title"><b>Change Password</b></h4>
                                <asp:Label ID="lblCPError" runat="server" data-backdrop="static" data-keyboard="false" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <asp:Label ID="lblCurrentPasssword" runat="server" Text="* Old Password"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCurrentPasssword" runat="server" ControlToValidate="txtCurrentPasssword" Display="Static" ErrorMessage="Enter Old Password." SetFocusOnError="True" ValidationGroup="pwd"></asp:RequiredFieldValidator>
                                    <asp:TextBox autocomplete="off" AutoCompleteType="Disabled" ID="txtCurrentPasssword" runat="server" CssClass="aspxcontrols" TextMode="Password" onpaste="return false" />
                                    <asp:CompareValidator CssClass="ErrorMsgLeft" runat="server" ID="CVCurrentPasssword" ControlToValidate="txtCurrentPasssword" Operator="Equal" Type="String" ErrorMessage="Invalid Old Password." ValidationGroup="pwd" />
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Label ID="lblNewPassword" runat="server" Text="New Password"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNewPasssword" runat="server" ControlToValidate="txtNewPassword" Display="Static" ErrorMessage="Enter New Password." SetFocusOnError="True" ValidationGroup="pwd"></asp:RequiredFieldValidator>
                                    <asp:TextBox autocomplete="off" AutoCompleteType="Disabled" ID="txtNewPassword" runat="server" CssClass="aspxcontrols" TextMode="Password" onpaste="return false" />
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="RegExpNewPwd" ControlToValidate="txtNewPassword" ValidationExpression="sRegExpNewPwd" runat="server" ErrorMessage="Follow Password policy." ValidationGroup="pwd"></asp:RegularExpressionValidator>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Static" ErrorMessage="Confirm Password." SetFocusOnError="True" ValidationGroup="pwd"></asp:RequiredFieldValidator>
                                    <asp:TextBox autocomplete="off" AutoCompleteType="Disabled" ID="txtConfirmPassword" runat="server" CssClass="aspxcontrols" TextMode="Password" onpaste="return false" />
                                    <asp:CompareValidator CssClass="ErrorMsgLeft" runat="server" ID="CVConfirmPassword" ControlToValidate="txtNewPassword" ControlToCompare="txtConfirmPassword" Operator="Equal" Type="String" ErrorMessage="Passwords does not match." ValidationGroup="pwd" />
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Label ID="lblCPNoteHeading" runat="server" Text="Note:- "></asp:Label>
                                    <asp:Label ID="lblCONote" runat="server" Text="" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="form-group pull-right">
                                    <asp:Button runat="server" Text="Change" class="btn-ok" ID="btnCPUpdate" ValidationGroup="pwd" OnClick="btnCPUpdate_Click"></asp:Button>
                                    <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnCPCancel" OnClick="btnCPCancel_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalForgotPassword" class="modal" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title"><b>Password Retrive</b></h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <asp:Label ID="lblFPNoteHeading" runat="server" CssClass="aspxlabelbold" Text="If you've forgotten the password to your account, please confirm your security answer and we will provide your password."></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblHeadinglogin" runat="server" Text="Login Name :- "></asp:Label>
                                    <asp:Label ID="lblFPLogin" runat="server" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingQuestion" runat="server" Text="Security Question :- "></asp:Label>
                                    <asp:Label ID="lblQue" runat="server" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingAnswer" runat="server" Text="Security Answer"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAnswer" runat="server" ControlToValidate="txtAnswer" Display="Dynamic" ErrorMessage="Enter valid Answer." SetFocusOnError="True" ValidationGroup="Ans"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator CssClass="ErrorMsgRight" runat="server" ID="CVAnswer" ControlToValidate="txtAnswer" Type="String" ErrorMessage="Invalid answer." ValidationGroup="Ans" />
                                    <asp:TextBox autocomplete="off" AutoCompleteType="Disabled" ID="txtAnswer" runat="server" CssClass="aspxcontrols" onpaste="return false"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnGetPassword" runat="server" CssClass="btn-ok pull-right" Text="Get Password" ValidationGroup="Ans"></asp:Button>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingPassword" runat="server" Text="Your Password :- "></asp:Label>
                                    <asp:Label ID="lblPWD" runat="server" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
