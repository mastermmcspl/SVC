<%@ Page Title="" Language="VB" MasterPageFile="~/CustomerMaster/Home.master" AutoEventWireup="false" CodeFile="ServerDB.aspx.vb" Inherits="CustomerMaster_ServerDB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <link rel="stylesheet" href="StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/sweetalert.css" rel="stylesheet" type="text/css" />

    <script src="JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="JavaScripts/html5shiv.js" type="text/javascript"></script>
    <script src="JavaScripts/respond.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>Server Detail</b></h2>
            </div>
            <div class="col-sm-2 col-md-2 pull-right">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnConnect" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Connect" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnCreate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" ValidationGroup="DBValidate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Backward24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
            <%-- <div class="col-sm-2 col-md-2 pull-right">
                <div class="pull-right" >
                    <asp:ImageButton ID="imgbtnCreate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" ValidationGroup="DBValidate" />
                </div>
            </div>--%>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 15px">
        <div class="form-group" style="padding: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:Panel ID="Panel1" runat="server" GroupingText="Connect to Server" Width="99%">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblServerName" runat="server" Text="* Server Name"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVServername" runat="server" SetFocusOnError="True" ControlToValidate="txtServername" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtServername" runat="server" CssClass="aspxcontrols" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblLogin" runat="server" Text="* Login"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLogin" runat="server" SetFocusOnError="True" ControlToValidate="txtLogin" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtLogin" runat="server" CssClass="aspxcontrols" Text=""></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblPassword" runat="server" Text="* Password"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPassword" runat="server" SetFocusOnError="True" ControlToValidate="txtPassword" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="aspxcontrols" TextMode="Password"></asp:TextBox>
                    <asp:TextBox ID="txtAlterPassword" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:Panel ID="Panel2" runat="server" GroupingText="Database Creation" Width="99%">
            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblProduct" runat="server" Text="* Product"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVProduct" runat="server" ControlToValidate="ddlProduct" Display="Dynamic" SetFocusOnError="True" ValidationGroup="DBValidate"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlProduct" Enabled="false" runat="server" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblDatabase" runat="server" Text="* Database Name"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDatabase" runat="server" SetFocusOnError="True" ControlToValidate="txtDatabase" Display="Dynamic" ValidationGroup="DBValidate"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtDatabase" runat="server" CssClass="aspxcontrols" AutoPostBack="True" OnTextChanged="txtDatabase_TextChanged" onblur="myFunction()" Text=""></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblAccessCode" runat="server" Text="* Access Code"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccessCode" runat="server" SetFocusOnError="True" ControlToValidate="txtAccessCode" Display="Dynamic" ValidationGroup="DBValidate"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtAccessCode" runat="server" CssClass="aspxcontrols" Text=""></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblCompanyName" runat="server" Text="* Company Name"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyName" runat="server" SetFocusOnError="True" ControlToValidate="txtCompanyName" Display="Dynamic" ValidationGroup="DBValidate"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="aspxcontrols" Text=""></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
    </div>


    <div id="ModalEmpMasterDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>MMCSPL</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblEmpMasterDetailsValidationMsg" runat="server"></asp:Label>
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
    <script>
        function myFunction() {

            __doPostBack("<%=txtDatabase%>", txtDatabase_TextChanged);

        }</script>
</asp:Content>

