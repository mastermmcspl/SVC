<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Master.master" CodeFile="ApplicationConfiguration.aspx.vb" Inherits="Masters_ApplicationConfiguration" ValidateRequest="false" Debug="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
      <style>        
                div{
            color:black;
                      }        
        </style>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        <%--function ValidateFilesDB() {
            if (document.getElementById('<%=ddlFilesDB.ClientID %>').selectedIndex == "0") {
                document.getElementById('<%=lblAttachmentfilepath.ClientID %>').innerHTML = 'Attachment File Path'
                document.getElementById('<%=txtFileInDBPath.ClientID%>').disabled = true;
                document.getElementById('<%=txtFileInDBPath.ClientID %>').value = '';
                return false;
            }
            if (document.getElementById('<%=ddlFilesDB.ClientID %>').selectedIndex == "1") {
                document.getElementById('<%=txtFileInDBPath.ClientID%>').disabled = false;
                document.getElementById('<%=txtFileInDBPath.ClientID %>').value = ''
                document.getElementById('<%=lblAttachmentfilepath.ClientID %>').innerHTML = '* Attachment File Path'
                document.getElementById('<%=txtFileInDBPath.ClientID %>').focus();
                return false;
            }
            return true;
        }--%>
      
      
    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>1.1 Application Configuration</b></h2>
            </div>
            <div class="form-group pull-right">
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" TabIndex="18" ValidationGroup="Validate" />
            </div>
        </div>
    </div>
    <%--<div class="col-sm-12 col-md-12">
       <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            <asp:CompareValidator CssClass="ErrorMsgLeft" runat="server" ID="CVMaxNoPwdChar" ControlToValidate="txtMaxNoPwdChar" ControlToCompare="txtMinNoPwdChar" Operator="GreaterThan" SetFocusOnError="true" Type="Integer" ValidationGroup="pwd" />
    </div>--%>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <fieldset>
            <legend class="legendbold">Application Settings</legend>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Image Path"></asp:Label>
                        <asp:TextBox ID="txtImgPath" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Error Log Path"></asp:Label>
                        <asp:TextBox ID="txtErrorLog" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Application Temp Directory"></asp:Label>
                        <asp:TextBox ID="txtTempDir" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="FTP Server"></asp:Label>
                        <asp:TextBox ID="txtFTPServer" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Max File Size"></asp:Label>
                        <asp:DropDownList ID="ddlFileSize" runat="server" CssClass="aspxcontrols" Visible="true">
                            <asp:ListItem Value="1">1 MB</asp:ListItem>
                            <asp:ListItem Value="2">2 MB</asp:ListItem>
                            <asp:ListItem Value="3">3 MB</asp:ListItem>
                            <asp:ListItem Value="4">4 MB</asp:ListItem>
                            <asp:ListItem Value="5">5 MB</asp:ListItem>
                            <asp:ListItem Value="6">6 MB</asp:ListItem>
                            <asp:ListItem Value="7">7 MB</asp:ListItem>
                            <asp:ListItem Value="8">8 MB</asp:ListItem>
                            <asp:ListItem Value="9">9 MB</asp:ListItem>
                            <asp:ListItem Value="10">10 MB</asp:ListItem>
                            <asp:ListItem Value="11">11 MB</asp:ListItem>
                            <asp:ListItem Value="12">12 MB</asp:ListItem>
                            <asp:ListItem Value="13">13 MB</asp:ListItem>
                            <asp:ListItem Value="14">14 MB</asp:ListItem>
                            <asp:ListItem Value="15">15 MB</asp:ListItem>
                            <asp:ListItem Value="16">16 MB</asp:ListItem>
                            <asp:ListItem Value="17">17 MB</asp:ListItem>
                            <asp:ListItem Value="18">18 MB</asp:ListItem>
                            <asp:ListItem Value="19">19 MB</asp:ListItem>
                            <asp:ListItem Value="20">20 MB</asp:ListItem>
                            <asp:ListItem Value="21">21 MB</asp:ListItem>
                            <asp:ListItem Value="22">22 MB</asp:ListItem>
                            <asp:ListItem Value="23">23 MB</asp:ListItem>
                            <asp:ListItem Value="24">24 MB</asp:ListItem>
                            <asp:ListItem Value="25">25 MB</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Currency Type"></asp:Label>
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Session Time Out"></asp:Label>
                        <asp:DropDownList ID="ddlSessionTimeOut" runat="server" CssClass="aspxcontrols" Visible="true">
                            <asp:ListItem Value="10">10 min</asp:ListItem>
                            <asp:ListItem Value="15">15 min</asp:ListItem>
                            <asp:ListItem Value="20">20 min</asp:ListItem>
                            <asp:ListItem Value="25">25 min</asp:ListItem>
                            <asp:ListItem Value="30">30 min</asp:ListItem>
                            <asp:ListItem Value="35">35 min</asp:ListItem>
                            <asp:ListItem Value="40">40 min</asp:ListItem>
                            <asp:ListItem Value="45">45 min</asp:ListItem>
                            <asp:ListItem Value="50">50 min</asp:ListItem>
                            <asp:ListItem Value="55">55 min</asp:ListItem>
                            <asp:ListItem Value="60">60 min</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Time Out Warning Before"></asp:Label>
                        <asp:DropDownList ID="ddlSessionTimeOutWarning" runat="server" CssClass="aspxcontrols" Visible="true">
                            <asp:ListItem Value="1">1 min</asp:ListItem>
                            <asp:ListItem Value="2">2 min</asp:ListItem>
                            <asp:ListItem Value="3">3 min</asp:ListItem>
                            <asp:ListItem Value="4">4 min</asp:ListItem>
                            <asp:ListItem Value="5">5 min</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="HTTP"></asp:Label>
                        <asp:TextBox ID="txtHTP" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Date Format"></asp:Label>
                        <asp:DropDownList ID="ddlDateFormat" runat="server" CssClass="aspxcontrolsdisable" Enabled="false">
                            <asp:ListItem Value="1">dd/mm/yyyy</asp:ListItem>
                            <asp:ListItem Value="2">mm/dd/yyyy</asp:ListItem>
                            <asp:ListItem Value="3">yyyy/mm/dd</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* File in DB"></asp:Label>
                        <asp:DropDownList ID="ddlFilesDB" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
			                <asp:ListItem Value="0">True</asp:ListItem>
			                <asp:ListItem Value="1">False</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Attachment File Path"></asp:Label>
                        <asp:TextBox ID="txtFileInDBPath" runat="server" autocomplete="off" CssClass="aspxcontrols" Enabled="False" MaxLength="105"></asp:TextBox>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <legend class="legendbold">Password Management</legend>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Min Password Character"></asp:Label>
                        <asp:TextBox ID="txtMinPassword" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Max Password Character"></asp:Label>
                        <asp:TextBox ID="txtMaxPassword" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* No. of Recovery Attempts"></asp:Label>
                        <asp:TextBox ID="txtRecoveryAttempt" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Unsuccessful Attempts"></asp:Label>
                        <asp:TextBox ID="txtUnsuccessAttempts" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Password Expiry Days"></asp:Label>
                        <asp:TextBox ID="txtExpiryDay" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="3"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Password Expiry Alert Days"></asp:Label>
                        <asp:TextBox ID="txtExpiryAlertDay" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblDormantdays" runat="server" Text="* Dormant(Not Login) Days"></asp:Label>
                        <asp:TextBox ID="txtNumberofLogin" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Password Contains"></asp:Label>
                        <asp:CheckBoxList ID="chkPasswordContain" runat="server" Enabled="False" RepeatColumns="2" CssClass="myCheckbox"></asp:CheckBoxList>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
    <div class="col-sm-12 col-md-12">
        <fieldset>
            <legend class="legendbold">E-Mail Settings</legend>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* SMTP Address"></asp:Label>
                        <asp:TextBox ID="txtSMTP" runat="server" autocomplete="off" MaxLength="15" CssClass="aspxcontrols" TabIndex="14"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Sender E-Mail ID"></asp:Label>
                        <asp:TextBox ID="txtSenderEmail" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVSenerEID" runat="server" ControlToValidate="txtSenderEmail" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVSenerEID" runat="server" ControlToValidate="txtSenderEmail" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Port Number"></asp:Label>
                        <asp:TextBox ID="txtPort" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="4" data-toggle="tooltip" data-placement="top" title="Only numbers"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* SMS Sender ID"></asp:Label>
                        <asp:TextBox ID="txtSMS" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVSMS" runat="server" ControlToValidate="txtSMS" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVSMS" runat="server" ControlToValidate="txtSMS" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>

    <div class="col-sm-12 col-md-12">
        <fieldset>
            <legend class="legendbold">Application Start Date</legend>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Start Date"></asp:Label>
                        <asp:TextBox runat="server" AutoPostBack="false" CssClass="aspxcontrols" ID="txtStartDate"></asp:TextBox>
                        <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtStartDate" PopupPosition="TopRight"
                            TargetControlID="txtStartDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </div>
                </div>
                 <div class="col-sm-3 col-md-3">
                     
                 </div>
            </div>
        </fieldset>
    </div>

    <div class="col-sm-12 col-md-12 form-group">
                <fieldset>
                    <legend class="legendbold">FixedAsset Setting</legend>

                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Depreciation Method"></asp:Label>
                        <asp:DropDownList ID="ddlMethod" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="SLM" Value="1"></asp:ListItem>
                            <asp:ListItem Text="WDV" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </fieldset>
            </div>

    <div id="ModalFASSettingValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblFASSettingsValidationMsg" runat="server"></asp:Label></strong>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
