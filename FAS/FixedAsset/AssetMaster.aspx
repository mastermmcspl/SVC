<%@ Page Title="" Language="VB" MasterPageFile="~/FixedAsset.master" AutoEventWireup="false" CodeFile="AssetMaster.aspx.vb" Inherits="FixedAsset_AssetMaster" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <style>
          div{
            color:black;
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


        .auto-style1 {
            resize: none;
            font-size: 12px;
            line-height: 1.42857143;
            color: #444444;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            font-family: "Segoe UI", sans-serif;
            border: 1px solid #ccc;
            margin-top: 1px;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }

        .auto-style2 {
            width: 100%;
            height: 26px;
            resize: none;
            font-size: 12px;
            line-height: 1.42857143;
            color: #444444;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            font-family: "Segoe UI", sans-serif;
            border: 1px solid #ccc;
            margin-top: 1px;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/bootstrap-multiselect.css" rel="stylesheet" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap-multiselect.js"></script>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />

    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(function () {
            $(":file").change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();
                    reader.onload = imageIsLoaded;
                    reader.readAsDataURL(this.files[0]);
                }
            });
        });

        function imageIsLoaded(e) {
            $('#myImg').attr('src', e.target.result);
        };


        $(function () {
            $(":file").change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();
                    reader.onload = imageIsLoadedS;
                    reader.readAsDataURL(this.files[0]);
                }
            });
        });

        function imageIsLoadedS(e) {
            $('#myImgS').attr('src', e.target.result);
        };


    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Asset Opening Balance</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" Style="height: 16px" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validatesave" />
                 <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
               
                    
                     </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px">
        <asp:Label ID="lblErrorUp" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="AssetType"></asp:Label>
            <asp:DropDownList ID="drpAstype" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdrpAstype" ControlToValidate="drpAstype" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the Assettype" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing ItemCode"></asp:Label>
            <asp:DropDownList ID="DrpItemCode" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>

        <br />
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Status:"></asp:Label>
            <asp:Label ID="lblstatus" runat="server" CssClass="Label"></asp:Label>
        </div>
    </div>


    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="AssetCode"></asp:Label>
            <asp:TextBox ID="txtbxAstCode" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxAstCode" ControlToValidate="txtbxAstCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the AssetCode" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="txtbxDscrptn" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDscrptn" ControlToValidate="txtbxDscrptn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Description" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>

        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="ItemCode"></asp:Label>
            <asp:TextBox ID="txtbxItmCode" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmCode" ControlToValidate="txtbxItmCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the description" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>

        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Item Description"></asp:Label>
            <asp:TextBox ID="txtbxItmDecrtn" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmDecrtn" ControlToValidate="txtbxItmDecrtn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the IteamDescription" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>

        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Quantity"></asp:Label>
            <asp:TextBox ID="txtbxQty" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxQty" ControlToValidate="txtbxQty" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Quantity" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>

        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Date Of Purchase"></asp:Label>
            <asp:TextBox ID="txtbxDteofPurchase" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxDteofPurchase_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteofPurchase" TargetControlID="txtbxDteofPurchase" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteofPurchase" ControlToValidate="txtbxDteofPurchase" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please Select the date of purchase" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteofPurchase1" runat="server" ControlToValidate="txtbxDteofPurchase" Display="Dynamic" ErrorMessage="please enter the date in dd/mm/yy formate" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>

        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Date Of Commission"></asp:Label>
            <asp:TextBox ID="txtbxDteCmmunictn" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxDteCmmunictn_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteCmmunictn" TargetControlID="txtbxDteCmmunictn" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteCmmunictn" ControlToValidate="txtbxDteCmmunictn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please Select the date of Commission" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteCmmunictn1" runat="server" ControlToValidate="txtbxDteCmmunictn" Display="Dynamic" ErrorMessage="please enter the date in dd/mm/yy formate" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>

        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Label runat="server" Text="Asset Age"></asp:Label>
            <asp:TextBox ID="txtbxAstAge" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="txtbxamount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtbxamount" Display="Dynamic" ErrorMessage="please enter the Amount" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" ControlToValidate="txtbxamount" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Amount" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
        </div>
    </div>

    <%--<div class="col-sm-12 col-md-12 pre-scrollableborder form-group">--%>
    <div class="col-sm-12 col-md-12">
        <%--<fieldset class="col-sm-12 col-md-12">--%>
        <%--<legend class="legendbold">Insurance Details</legend>--%>
        <%--</fieldset>--%>
        <h5><b>Insurance Details</b></h5>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Policy No"></asp:Label>
            <asp:TextBox ID="txtbxPlyNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator3" ControlToValidate="txtbxPlyNo" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Polycy Number" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="txtbxAmt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbxAmt" Display="Dynamic" ErrorMessage="please enter the Amount" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>--%>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator2" ControlToValidate="txtbxAmt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Amount" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="BrokerName"></asp:Label>
            <asp:TextBox ID="txtbxBrkName" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator5" ControlToValidate="txtbxBrkName" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the BrokerName" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtbxBrkName" Display="Dynamic" ErrorMessage="please enter the BrokerName" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,25}"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Company Name"></asp:Label>
            <asp:TextBox ID="txtCmpName" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator6" ControlToValidate="txtCmpName" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Company Name" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCmpName" Display="Dynamic" ErrorMessage="please the Company Name" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="From Date"></asp:Label>
            <asp:TextBox ID="txtbxfrmDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxfrmDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxfrmDate" TargetControlID="txtbxfrmDate" Format="dd/MM/yyyy" PopupPosition="Bottomright"></cc1:CalendarExtender>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator4" ControlToValidate="txtbxfrmDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please Select the Date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtbxfrmDate" Display="Dynamic" ErrorMessage="please enter the date in dd/mm/yy formate" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="To Date"></asp:Label>
            <asp:TextBox ID="txtbxtoDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxtoDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxtoDate" TargetControlID="txtbxtoDate" Format="dd/MM/yyyy" PopupPosition="Bottomright"></cc1:CalendarExtender>
        </div>
    </div>

    <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server" visible="True">
        <div id="div2" runat="server">
            <ul class="nav nav-tabs" role="tablist">
                <li id="lisupplier_Detls" runat="server">
                    <asp:LinkButton ID="lnkbtnSupDtls" Text="Supplier Details" runat="server" Font-Bold="true" /></li>

                <li id="lialortment_Detls" runat="server">
                    <asp:LinkButton ID="lnkbtnAlrtDtls" Text="Location Details" runat="server" Font-Bold="true" /></li>

                <li id="liWarantyAMC_Detls" runat="server">
                    <asp:LinkButton ID="lnkWrntyAMCDtls" Text="warranty/AMC Details" runat="server" Font-Bold="true" /></li>

                <li id="liAsset_detetion" runat="server">
                    <asp:LinkButton ID="lnkbtnDeletion" Text="Asset Deletion" runat="server" Font-Bold="true" /></li>

                <li id="liAsset_Loan" runat="server">
                    <asp:LinkButton ID="lnkbtnLoanAsst" Text="Taken any Loan Against Asset" runat="server" Font-Bold="true" /></li>
            </ul>
        </div>
        <div class="tab-content divmargin">

            <div runat="server" role="tabpanel" class="tab-pane" id="divSupDtls">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblSsipdtls" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Suplier Name"></asp:Label>
                        <asp:TextBox ID="txtbxSname" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator11" ControlToValidate="txtbxSname" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Suplier Name" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtbxSname" Display="Dynamic" ErrorMessage="please enter Suplier Name" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Contact Person"></asp:Label>
                        <asp:TextBox ID="txtbxConPerson" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator12" ControlToValidate="txtbxConPerson" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Contact Person" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtbxConPerson" Display="Dynamic" ErrorMessage="please enter Contact Person" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Address"></asp:Label>
                        <asp:TextBox ID="txtbxAddress" autocomplete="off" runat="server" CssClass="aspxcontrols" TextMode="MultiLine"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator13" ControlToValidate="txtbxAddress" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Address" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtbxAddress" Display="Dynamic" ErrorMessage="please enter Address" SetFocusOnError="True" ValidationExpression="^[0-9'.\s a-zA-Z'.\s]{1,200}"></asp:RegularExpressionValidator>--%>
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Phone No"></asp:Label>
                        <asp:TextBox ID="txtbxPhoneNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator14" ControlToValidate="txtbxPhoneNo" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter PhoneNo" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtbxPhoneNo" Display="Dynamic" ErrorMessage="please enter  PhoneNo" SetFocusOnError="True" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="FAX"></asp:Label>
                            <asp:TextBox ID="txtbxFax" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator16" ControlToValidate="txtbxFax" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter FaxNo" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtbxFax" Display="Dynamic" ErrorMessage="please enter FaxNo" SetFocusOnError="True" ValidationExpression="[0-9]{6}"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Email"></asp:Label>
                            <asp:TextBox ID="txtbxEmail" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator17" ControlToValidate="txtbxEmail" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter correct mailid" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator16" CssClass="ErrorMsgRight" runat="server" ErrorMessage="Enter the correct mailid" ControlToValidate="txtbxEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Text="Enter the correct mailid"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Website"></asp:Label>
                            <asp:TextBox ID="txtbxwebsite" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator18" ControlToValidate="txtbxwebsite" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter correct website" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>

                </div>
            </div>

            <div runat="server" role="tabpanel" class="tab-pane active" id="divAplertDtls">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblAlrtdtls" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Location"></asp:Label>
                        <asp:DropDownList ID="ddlLocatn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator7" ControlToValidate="ddlLocatn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the Location" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Department"></asp:Label>
                        <asp:DropDownList ID="ddlDeptmnt" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator19" ControlToValidate="ddlDeptmnt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the department" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Employee"></asp:Label>
                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator20" ControlToValidate="ddlEmployee" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the employee" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Employee Code"></asp:Label>
                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator10" ControlToValidate="txtEmpCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the employee Code" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                    </div>

                </div>
            </div>
            <div runat="server" role="tabpanel" class="tab-pane active" id="divWrntyAMCDtls">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label1" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Warranty Description"></asp:Label>
                        <asp:TextBox ID="txtWrntyDesc" autocomplete="off" runat="server" CssClass="aspxcontrols" TextMode="multiline"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator21" ControlToValidate="txtWrntyDesc" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Warranty Description" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator18" CssClass="ErrorMsgRight" runat="server" ErrorMessage="Enter the correct mailid" ControlToValidate="txtWrntyDesc" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter the Warranty Description"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Contact Person"></asp:Label>
                        <asp:TextBox ID="txtContperson" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator22" ControlToValidate="txtContperson" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Contact Person" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator19" CssClass="ErrorMsgRight" runat="server" ErrorMessage="please enter Contact Person" ControlToValidate="txtContperson" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter the Warranty Description"></asp:RegularExpressionValidator>--%>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="AMC CompanyName"></asp:Label>
                        <asp:TextBox ID="txtbxAMCompname" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator23" ControlToValidate="txtbxAMCompname" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter AMC CompanyName" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator20" CssClass="ErrorMsgRight" runat="server" ErrorMessage="please enter AMC CompanyName" ControlToValidate="txtbxAMCompname" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="please enter AMC CompanyName"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="AMC From Date"></asp:Label>
                        <asp:TextBox ID="txtbxAMCfrmDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtbxAMCfrmDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxAMCfrmDate" TargetControlID="txtbxAMCfrmDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator24" ControlToValidate="txtbxAMCfrmDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter From Date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="AMC To Date"></asp:Label>
                        <asp:TextBox ID="txtbxAMCtoDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtbxAMCtoDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxAMCtoDate" TargetControlID="txtbxAMCtoDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator25" ControlToValidate="txtbxAMCtoDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter AMC To Date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="AMC Contact Person"></asp:Label>
                        <asp:TextBox ID="txtbxContprsn" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator26" ControlToValidate="txtbxContprsn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter AMC Contact Person" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator21" CssClass="ErrorMsgRight" runat="server" ErrorMessage="please enter AMC Contact Person" ControlToValidate="txtbxContprsn" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="please enter AMC Contact Person"></asp:RegularExpressionValidator>--%>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="Phone"></asp:Label>
                        <asp:TextBox ID="txtbxPhno" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator27" ControlToValidate="txtbxPhno" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Phone" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtbxPhno" Display="Dynamic" ErrorMessage="please enter  Phone" SetFocusOnError="True" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>--%>
                    </div>

                </div>
            </div>
            <div runat="server" role="tabpanel" class="tab-pane active" id="divAssetDeletion">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label2" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Asset Deletion"></asp:Label>
                        <asp:DropDownList ID="ddlDeletion" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Date"></asp:Label>
                        <asp:TextBox ID="txtDlnDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDlnDate" TargetControlID="txtDlnDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator9" ControlToValidate="txtDlnDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Asset Deletion Date"></asp:Label>
                        <asp:TextBox ID="txtdeletionDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" runat="server" PopupButtonID="txtdeletionDate" TargetControlID="txtdeletionDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator8" ControlToValidate="txtdeletionDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Sale/Scrap Valve"></asp:Label>
                        <asp:TextBox ID="txtbxValue" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                    <%--  <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Reason for Deletion"></asp:Label>
                        <asp:dropdownlist ID="ddlReason" autocomplete="off" runat="server" autopostback="true" CssClass="aspxcontrols">                          
                        </asp:dropdownlist>
                    </div>--%>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Remarks"></asp:Label>
                        <asp:TextBox ID="txtremark" autocomplete="off" runat="server" Height="80px" Width="600px" TextMode="MultiLine" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div runat="server" role="tabpanel" class="tab-pane active" id="divLoanAsst">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label3" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="From whome"></asp:Label>
                        <asp:TextBox ID="txtloanWhome" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Address"></asp:Label>
                        <asp:TextBox ID="txtLoanAddress" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Amount"></asp:Label>
                        <asp:TextBox ID="txtloanAmount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Agreement No"></asp:Label>
                        <asp:TextBox ID="txtloanAgrmnt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Date"></asp:Label>
                        <asp:TextBox ID="txtloandate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtloandate_CalendarExtender2" CssClass="cal_Theme1" runat="server" PopupButtonID="txtloandate" TargetControlID="txtloandate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Imported foreign currency type"></asp:Label>
                        <asp:DropDownList ID="ddlCurrencytypeloan" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="100%"></asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="Exchange Date"></asp:Label>
                        <asp:TextBox ID="txtLoanExcngDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtLoanExcngDate_CalendarExtender4" CssClass="cal_Theme1" runat="server" PopupButtonID="txtLoanExcngDate" TargetControlID="txtLoanExcngDate" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="Exchange Amount" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtLnExchgeAmt" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="Amount in Ruppees"  Visible="false"></asp:Label>
                        <asp:TextBox ID="txtLnAmtRs" autocomplete="off" runat="server" CssClass="aspxcontrols"  Visible="false"></asp:TextBox>
                    </div>
                </div>
                   <div class="col-sm-12 col-md-12 divmargin">
                        <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Repaid"  Visible="false"></asp:Label>
                        <asp:TextBox ID="txtLnRepaid" autocomplete="off" runat="server" CssClass="aspxcontrols"  Visible="false"></asp:TextBox>
                    </div>
                         <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Balance to Date"  Visible="false"></asp:Label>
                        <asp:TextBox ID="txtBlnceTpaid" autocomplete="off" runat="server" CssClass="aspxcontrols"  Visible="false"></asp:TextBox>
                              <cc1:CalendarExtender ID="CalendarExtender3" CssClass="cal_Theme1" runat="server" PopupButtonID="txtBlnceTpaid" TargetControlID="txtBlnceTpaid" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                                     </div>
                       </div>
            </div>
        </div>
    </div>

    <asp:TextBox ID="txtmasterid" autocomplete="off" runat="server" Visible="false"></asp:TextBox>


    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
        </div>
    </div>
    <div class="col-sm-4 col-md-4" style="padding-left: 0px">
        <div class="col-sm-8 col-md-8">
            <div class="form-group">
                <asp:Label ID="lblCurrentStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
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


    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>

</asp:Content>

