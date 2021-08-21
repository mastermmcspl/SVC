<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Accounts.master" CodeFile="ChequeDetails.aspx.vb" Inherits="Accounts_ChequeDetails" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/General.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        function CheckChqDate(sender, args) {
            if ((document.getElementById('<%=txtChequeDate.ClientID%>').value != "") && (document.getElementById('<%=txtToDate.ClientID%>').value != "")) {

                var DV = DataValid(document.getElementById('<%=txtToDate.ClientID%>').value, document.getElementById('<%=txtChequeDate.ClientID%>').value)
                if (DV == false) {
                    alert("Cheque to date(" + document.getElementById('<%=txtChequeDate.ClientID %>').value + ") should be greater than or equal to Today's date(" + document.getElementById('<%=txtToDate.ClientID %>').value + ").");
                    document.getElementById('<%=txtToDate.ClientID%>').value = '';
                    document.getElementById('<%=txtToDate.ClientID%>').focus();
                    return false;
                }
            }
        }
        function CheckCltDate(sender, args) {

            if ((document.getElementById('<%=txtCollectedDate.ClientID%>').value != "") && (document.getElementById('<%=txtToDate.ClientID%>').value != "")) {

                var DV = DataValid(document.getElementById('<%=txtToDate.ClientID%>').value, document.getElementById('<%=txtCollectedDate.ClientID%>').value)
                if (DV == false) {
                    alert("Cheque to date(" + document.getElementById('<%=txtCollectedDate.ClientID %>').value + ") should be greater than or equal to Today's date(" + document.getElementById('<%=txtToDate.ClientID %>').value + ").");
                    document.getElementById('<%=txtCollectedDate.ClientID%>').value = '';
                    document.getElementById('<%=txtCollectedDate.ClientID%>').focus();
                    return false;
                }
            }
        }

        function CheckProDate(sender, args) {
            if ((document.getElementById('<%=txtProDate.ClientID%>').value != "") && (document.getElementById('<%=txtToDate.ClientID%>').value != "")) {

               var DV = DataValid(document.getElementById('<%=txtToDate.ClientID%>').value, document.getElementById('<%=txtProDate.ClientID%>').value)
               if (DV == false) {
                   alert("Cheque to date(" + document.getElementById('<%=txtProDate.ClientID %>').value + ") should be greater than or equal to Today's date(" + document.getElementById('<%=txtToDate.ClientID %>').value + ").");
                    document.getElementById('<%=txtProDate.ClientID%>').value = '';
                    document.getElementById('<%=txtProDate.ClientID%>').focus();
                    return false;
                }
            }
        }
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Post Dated Cheque Register</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnDelete" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="Existing Cheque No."></asp:Label>
                <asp:DropDownList ID="ddlExeChequeNo" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="Serial No."></asp:Label>
                <asp:TextBox ID="txtSerialNo" runat="server" CssClass="aspxcontrols" ReadOnly="true"></asp:TextBox>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="Party Code"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartyCode" runat="server" ControlToValidate="ddlPartyCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlPartyCode" runat="server" CssClass="aspxcontrols">
                    <asp:ListItem Value="0">Select Party Code</asp:ListItem>
                    <asp:ListItem Value="1">01</asp:ListItem>
                    <asp:ListItem Value="2">02</asp:ListItem>
                    <asp:ListItem Value="3">03</asp:ListItem>
                    <asp:ListItem Value="4">04</asp:ListItem>
                    <asp:ListItem Value="5">05</asp:ListItem>
                    <asp:ListItem Value="6">06</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="Today's Date."></asp:Label>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="aspxcontrols" Visible="true" ReadOnly="true"></asp:TextBox>
            </div>

        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Cheque No."></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVChequeNo" runat="server" ControlToValidate="txtChequeNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVChequeNo" runat="server" ControlToValidate="txtChequeNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtChequeNo" runat="server" CssClass="aspxcontrols" MaxLength="6"></asp:TextBox>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Cheque Date"></asp:Label>
                <asp:TextBox ID="txtChequeDate"  runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVChequeDate" runat="server" ControlToValidate="txtChequeDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVChequeDate" runat="server" ControlToValidate="txtChequeDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <cc1:CalendarExtender ID="cclChequeDate" runat="server" PopupButtonID="txtChequeDate" PopupPosition="BottomLeft"
                    TargetControlID="txtChequeDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* RTGS/NEFT/IFSC code"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIFSCcode" runat="server" ControlToValidate="txtIFCScode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFSccode" runat="server" ControlToValidate="txtIFCScode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtIFCScode" runat="server" CssClass="aspxcontrols" MaxLength="11"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Account No."></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccountNo" runat="server" ControlToValidate="txtAccountNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAccountNo" runat="server" ControlToValidate="txtAccountNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="aspxcontrols" MaxLength="17"></asp:TextBox>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* MICR Code"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMicrCode" runat="server" ControlToValidate="txtMicrCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMicrCode" runat="server" ControlToValidate="txtMicrCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtMicrCode" runat="server" CssClass="aspxcontrols" MaxLength="9"></asp:TextBox>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Leaf No."></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLeafNo" runat="server" ControlToValidate="txtLeafNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVLeafNo" runat="server" ControlToValidate="txtLeafNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtLeafNo" runat="server" CssClass="aspxcontrols" MaxLength="3"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Bank Name"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBankName" runat="server" ControlToValidate="ddlBankName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlBankName" runat="server" CssClass="aspxcontrols">
                    <asp:ListItem Value="0">Select Bank Name</asp:ListItem>
                    <asp:ListItem Value="1">SBI</asp:ListItem>
                    <asp:ListItem Value="2">Canara Bank</asp:ListItem>
                    <asp:ListItem Value="3">ICICI Bank</asp:ListItem>
                    <asp:ListItem Value="4">Axis Bank</asp:ListItem>
                    <asp:ListItem Value="5">Vijaya Bank</asp:ListItem>
                    <asp:ListItem Value="6">Indian Bank</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="Branch Name"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchName" runat="server" ControlToValidate="ddlBranchName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="aspxcontrols">
                    <asp:ListItem Value="0">Select Branch Name</asp:ListItem>
                    <asp:ListItem Value="1">Branch 1</asp:ListItem>
                    <asp:ListItem Value="2">Branch 2</asp:ListItem>
                    <asp:ListItem Value="3">Branch 3</asp:ListItem>
                    <asp:ListItem Value="4">Branch 4</asp:ListItem>
                    <asp:ListItem Value="5">Branch 5</asp:ListItem>
                    <asp:ListItem Value="6">Branch 6</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Pay"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPayto" runat="server" ControlToValidate="txtPayto" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPayto" runat="server" ControlToValidate="txtPayto" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtPayto" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Rupees(In Words)"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRupees" runat="server" ControlToValidate="txtRupees" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRupees" runat="server" ControlToValidate="txtRupees" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtRupees" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Amount"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAmount" runat="server" ControlToValidate="txtAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAmount" runat="server" ControlToValidate="txtAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtAmount" runat="server" CssClass="aspxcontrols" MaxLength="9"></asp:TextBox>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Type of Account"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccountType" runat="server" ControlToValidate="ddlAccountType" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="aspxcontrols">
                    <asp:ListItem Value="0">Select Account Type</asp:ListItem>
                    <asp:ListItem Value="1">Savings Account</asp:ListItem>
                    <asp:ListItem Value="2">Current Account</asp:ListItem>
                    <asp:ListItem Value="3">Others</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-8 col-md-8" style="padding: 0px">
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                    <fieldset class="col-sm-12 col-md-12">
                        <legend class="legendbold">Other Details</legend>
                    </fieldset>
                    <div class="col-sm-6 col-md-6">
                        <asp:Label runat="server" Text="Sales Person"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSalesPerson" runat="server" ControlToValidate="ddlSalesPerson" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlSalesPerson" runat="server" CssClass="aspxcontrols">
                            <asp:ListItem Value="0">Select Sales Person</asp:ListItem>
                            <asp:ListItem Value="1">SP 1</asp:ListItem>
                            <asp:ListItem Value="2">SP 2</asp:ListItem>
                            <asp:ListItem Value="3">SP 3</asp:ListItem>
                            <asp:ListItem Value="4">SP 4</asp:ListItem>
                            <asp:ListItem Value="5">SP 5</asp:ListItem>
                            <asp:ListItem Value="6">SP 6</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <asp:Label runat="server" Text="Route No."></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRouteNo" runat="server" ControlToValidate="txtRouteNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRouteNo" runat="server" ControlToValidate="txtRouteNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtRouteNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-6 col-md-6">
                        <asp:Label runat="server" Text="Collected Date"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVColDate" runat="server" ControlToValidate="txtCollectedDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVColDate" runat="server" ControlToValidate="txtCollectedDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtCollectedDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" ></asp:TextBox>
                        <cc1:CalendarExtender ID="ClcCollDate" runat="server" PopupButtonID="txtCollectedDate" PopupPosition="BottomLeft"
                            TargetControlID="txtCollectedDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <asp:Label runat="server" Text="Produced Date"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVProDate" runat="server" ControlToValidate="txtProDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVProDate" runat="server" ControlToValidate="txtProDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtProDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                        <cc1:CalendarExtender ID="clcProDate" runat="server" PopupButtonID="txtProDate" PopupPosition="BottomLeft"
                            TargetControlID="txtProDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="Remarks"></asp:Label>
                <asp:TextBox ID="mtxtSummary" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="112px"></asp:TextBox>
            </div>
        </div>
    </div>
    <div id="myModalAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtfile" runat="server" CssClass="btn-ok" Width="95%" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-7 col-md-7" style="padding: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                        Visible="false" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                        runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:DataGrid ID="dgAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="footable" OnRowDataBound="PickColor_RowDataBound">
                                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                                <Columns>
                                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="File Name">
                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Width="40%"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Description">
                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Width="28%"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Created">
                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Width="23%"></HeaderStyle>
                                        <ItemTemplate>
                                            <b>By:-</b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On:-</b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Add Description" CommandName="ADDDESC" runat="server" CssClass="hvr-bounce-in" /><br />
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
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
