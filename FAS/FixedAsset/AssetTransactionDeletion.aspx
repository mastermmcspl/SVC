<%@ Page Title="" Language="VB" MasterPageFile="~/FixedAsset.master" AutoEventWireup="false" CodeFile="AssetTransactionDeletion.aspx.vb" Inherits="FixedAsset_AssetTransactionDeletion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div {
            color: black;
        }

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
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })


        function Validate() {
            if (document.getElementById('<%=DDlAsstType.ClientID %>').selectedIndex == 0) {
                alert('Select Asset type.');
                document.getElementById('<%=DDlAsstType.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlItemCode.ClientID %>').selectedIndex == 0) {
                alert('Select Asset No.');
                document.getElementById('<%=ddlItemCode.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlDeletion.ClientID %>').selectedIndex == 0) {
                alert('Select Asset Deletion.');
                document.getElementById('<%=ddlDeletion.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtDlnDate.ClientID %>').value == "") {
                alert('Select Date .');
                document.getElementById('<%=txtDlnDate.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtdeletionDate.ClientID %>').value == "") {
                alert('Select Asset Deletion Date .');
                document.getElementById('<%=txtdeletionDate.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtSScrap.ClientID %>').value == "") {
                alert('Enter the  sale Scrap Value .');
                document.getElementById('<%=txtSScrap.ClientID%>').focus()
                return false
            }
        }

    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Asset Deletion</b></h2>
            </div>

            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnDelete" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" ValidationGroup="Validate" />
             <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                <asp:ImageButton ID="ImgbtnActivate" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" CausesValidation="false" />
                       <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                                           </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div class="clearfix divmargin"></div>



    <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblPermissionType" runat="server" Text="Asset Deletion Type:-" Visible="false"></asp:Label>
            &nbsp;&nbsp;
             <asp:RadioButton ID="rboNew" Text="Partial Delete" GroupName="Select" runat="server" Checked="true" Visible="false" />
            &nbsp;&nbsp;  
                <asp:RadioButton ID="rboOld" Text="Fully Delete" GroupName="Select" runat="server" Visible="false" />
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Transaction No"></asp:Label>
            <asp:DropDownList ID="ddlTransNo" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAstNo" runat="server" ControlToValidate="ddlTransNo" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Asset Type"></asp:Label>
            <asp:DropDownList ID="DDlAsstType" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAstype" runat="server" ControlToValidate="DDlAsstType" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="ItemCode"></asp:Label>
            <asp:DropDownList ID="ddlItemCode" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmCode" ControlToValidate="ddlItemCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter The Description" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
      
          <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Deletion Transaction no"></asp:Label>
            <asp:TextBox ID="txtDelTransNo" autocomplete="off" runat="server" CssClass="aspxcontrols" enabled="false"></asp:TextBox>
        </div>
          <br />
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Status:"></asp:Label>
            <asp:Label ID="lblstatus" runat="server" CssClass="Label"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="txtbxDscrptn" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
            </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Item Description"></asp:Label>
            <asp:TextBox ID="txtbxItmDecrtn" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
             </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Quantity"></asp:Label>
            <asp:TextBox ID="txtbxQty" autocomplete="off" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
              </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Date Of Purchase"></asp:Label>
            <asp:TextBox ID="txtbxDteofPurchase" autocomplete="off" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxDteofPurchase_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteofPurchase" TargetControlID="txtbxDteofPurchase" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
             </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Date Of Commission"></asp:Label>
            <asp:TextBox ID="txtbxDteCmmunictn" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxDteCmmunictn_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteCmmunictn" TargetControlID="txtbxDteCmmunictn" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
              </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="txtbxamount" autocomplete="off" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
             </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Asset Deletion"></asp:Label>
            <asp:DropDownList ID="ddlDeletion" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeletion" runat="server" ControlToValidate="ddlDeletion" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Date"></asp:Label>
            <asp:TextBox ID="txtDlnDate" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender3" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDlnDate" TargetControlID="txtDlnDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDlnDate" runat="server" ControlToValidate="txtDlnDate" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDlnDate" runat="server" ControlToValidate="txtDlnDate" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Asset Deletion Date"></asp:Label>
            <asp:TextBox ID="txtdeletionDate" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender4" CssClass="cal_Theme1" runat="server" PopupButtonID="txtdeletionDate" TargetControlID="txtdeletionDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVdeletionDate" runat="server" ControlToValidate="txtdeletionDate" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdeletionDate" runat="server" ControlToValidate="txtdeletionDate" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
          </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Asset Value"></asp:Label>
            <asp:TextBox ID="txtSScrap" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVESScrap" runat="server" ControlToValidate="txtSScrap" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REVESScrap" runat="server" ControlToValidate="txtSScrap" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Quantity"></asp:Label>
            <asp:TextBox ID="txtQuantity" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lbllocation" runat="server" Text="Location" Visible="false"></asp:Label>
            <asp:TextBox ID="txtLocation" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblPaymnttype" runat="server" Text="Payment Type" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlPaymnttype" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
            </asp:DropDownList>
        </div>
        <div class="col-sm-9 col-md-9">
            <asp:Panel ID="PnlPaymntType" runat="server" Visible="false">
                <div class="col-sm-6 col-md-6">
                    <asp:Label ID="lblChqNo" runat="server" Text="Cheque No"></asp:Label>
                    <asp:TextBox ID="txtChqNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-6 col-md-6">
                    <asp:Label ID="lblChqRecvdDate" runat="server" Text="Cheque Recieved Date"></asp:Label>
                    <asp:TextBox ID="txtChqRecvdDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtChqRecvdDate" TargetControlID="txtChqRecvdDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                </div>
            </asp:Panel>
        </div>
    </div>
    <br />
    &nbsp;&nbsp;
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label runat="server" Text="Asset Deletion Description"></asp:Label>
            <asp:TextBox ID="txtdeldesc" runat="server" CssClass="aspxcontrols" Height="50px" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdeldesc" runat="server" ControlToValidate="txtdeldesc" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>

    <div id="ModalDeletionValidation1" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType1" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAssetdeletionValidationMsg1" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" ID="BtnYES" autopostback="true" Text="YES"></asp:Button>
                    <asp:Button runat="server" class="btn-OK" ID="BtnNo" Text="NO"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalDeletionValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAssetdeletionValidationMsg" runat="server"></asp:Label></strong>
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

    <asp:Panel Visible="false" runat="server" ID="PnlDebitCredit">
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Debit Details</legend>
                        </fieldset>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDrOtherHead" runat="server" ControlToValidate="ddlDrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="*General Ledger"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherGL" runat="server" ControlToValidate="ddlDbOtherGL" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDbOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                            <%--    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherSubGL" runat="server" ControlToValidate="ddlDbOtherSubGL" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlDbOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>

                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOtherDAmount" runat="server" ControlToValidate="txtOtherDAmount" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Enter Amount" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOtherDAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDrOtherHead" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherGL" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherSubGL" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnDADD" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateDBAdd" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Credit Details</legend>
                        </fieldset>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherHead" runat="server" ControlToValidate="ddlCrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherGL" runat="server" ControlToValidate="ddlCrOtherGL" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCrOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherSubGL" runat="server" ControlToValidate="ddlCrOtherSubGL" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlCrOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCRAmount" runat="server" ControlToValidate="txtOtherCAmount" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Enter Amount" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOtherCAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherHead" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherGL" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherSubGL" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnOtherCADD" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateCRAdd" />
                </div>
            </div>
        </div>
    </div>
        </asp:Panel>


    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgPaymentDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>


                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="DebitOrCredit" HeaderText="DebitOrCredit" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete1" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="DELETE" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>


       <div class=" modal fade" id="myAttchment" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Attachment</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblTax" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                     <div class="col-sm-12 col-md-12">
                            <div class="col-sm-5 col-md-5" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="95%" CssClass="btn-ok" AllowMultiple="true" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <asp:Button ID="btnAttch" runat="server" Text="Add" CssClass="btn-ok" />
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <asp:Button ID="btnIndex" runat="server" Text="Index" CssClass="btn-ok" />
                            </div>
                        </div>
                    

                    <div class="col-sm-12 col-md-12" runat="server">
                        <asp:GridView ID="gvattach" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="1%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in"  OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPath" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <%-- <asp:TemplateField  HeaderStyle-Width="40%" HeaderText="File Name">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="lblFilename" runat="server" CommandName="OPENPAGE" Font-Bold="False"  Text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                                    <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                                    <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                    
                </div>
                   
            </div>
        </div>
    </div>  
        
    
    <div id="myModalIndex" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Index Details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-12 col-md-12">
                            <div class="pull-left">
                                <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblcabinet" runat="server" Text="Cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblSubcabinet" runat="server" Text="Sub cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblFolder" runat="server" Text="Folder"></asp:Label>
                                <asp:DropDownList ID="ddlFolder" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblDocumentType" runat="server" Text="Document Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                <asp:Label ID="lblDateDisplay" runat="server" CssClass="aspxlabelbold"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>

                        </div>

                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>

                                    <asp:TemplateField HeaderStyle-Width="1%" HeaderText="DescriptorID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescriptorID" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DescriptorID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descriptor" HeaderText="Descriptor" HeaderStyle-Width="40%"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvKeywords" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Keywords" HeaderStyle-Width="100%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Key") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:ImageButton ID="imgbtnIndexSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index" />
                </div>
            </div>
        </div>
    </div> 

</asp:Content>

