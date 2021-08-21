<%@ Page Title="" Language="VB" MasterPageFile="~/FixedAsset.master" AutoEventWireup="false" CodeFile="AssetAddlnDtls.aspx.vb" Inherits="FixedAsset_AssetAddlnDtls" %>

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

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlRefno.ClientID%>').select2();
        });


    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Asset Additional Details</b></h2>
            </div>

            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtReferesh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" ValidationGroup="Validate" />
             <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                 </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="clearfix divmargin">
    </div>

    <div class="col-sm-12 col-md-12">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Asset No"></asp:Label>
            <asp:DropDownList ID="ddlAssetNo" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Asset ReferanceNo"></asp:Label>
            <asp:DropDownList ID="ddlRefno" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>


    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Visible="false" Text="Search by AseetReferance"></asp:Label>
            <asp:TextBox ID="txtPartySearch" Visible="false" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="200px"></asp:TextBox>
            <asp:ImageButton ID="btnSearchRefNo" Visible="false" runat="server" ImageUrl="~/Images/Search16.png" CssClass="hvr-bounce-in" data-toggle="tooltip" data-placement="bottom" title="Search" CausesValidation="False" />
        </div>
    </div>

    <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server" visible="True">
        <div id="div2" runat="server">
            <ul class="nav nav-tabs" role="tablist">
                <li id="liDeviceDtls" runat="server">
                    <asp:LinkButton ID="lnkbtnDeviceDtls" Text="Device Details" runat="server" Font-Bold="true" /></li>
                <li id="liMaintence_Detls" runat="server">
                    <asp:LinkButton ID="lnkbtnMainDtls" Text="Maintenance Details" runat="server" Font-Bold="true" /></li>
                <li id="lisupplier_Detls" runat="server">
                    <asp:LinkButton ID="lnkbtnSupDtls" Text="Supplier Details" runat="server" Font-Bold="true" /></li>
                <li id="liinstaln_Detls" runat="server">
                    <asp:LinkButton ID="lnkbtnInstlnDtls" Text="Installation Details" runat="server" Font-Bold="true" /></li>
                <li id="liInsurencedtls" runat="server">
                    <asp:LinkButton ID="lnkbtnInsurenceDtls" Text="Insurance Details" runat="server" Font-Bold="true" /></li>
                <li id="liCust_details" runat="server">
                    <asp:LinkButton ID="lnkbtnCustodydtls" Text="Asset Issued to Employee" runat="server" Font-Bold="true" /></li>
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
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-right">
                            <asp:ImageButton ID="imgbtnSupplierSave" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save24.png" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                        </div>
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Suplier Name"></asp:Label>
                        <asp:TextBox ID="txtbxSname" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="Contact Person"></asp:Label>
                        <asp:TextBox ID="txtbxConPerson" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="Phone No"></asp:Label>
                        <asp:TextBox ID="txtbxPhoneNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="FAX"></asp:Label>
                        <asp:TextBox ID="txtbxFax" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Email"></asp:Label>
                        <asp:TextBox ID="txtbxEmail" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Website"></asp:Label>
                            <asp:TextBox ID="txtbxwebsite" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label runat="server" Text="Address"></asp:Label>
                            <asp:TextBox ID="txtbxAddress" autocomplete="off" Height="50px" TextMode="MultiLine" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                        </div>
                    </div>
                </div>
            </div>           
            <div runat="server" role="tabpanel" class="tab-pane" id="divInstallationDtls">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblInstldtls" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-right">
                            <asp:ImageButton ID="imgbtnInstallation" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save24.png" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                        </div>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Device No"></asp:Label>
                        <asp:DropDownList ID="ddlDeviceNo" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Software"></asp:Label>
                        <asp:DropDownList ID="ddlSW" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Version"></asp:Label>
                        <asp:TextBox ID="txtversion" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Date of installation"></asp:Label>
                        <asp:TextBox ID="txtDateofInstlln" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtDateofInstlln_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDateofInstlln" TargetControlID="txtDateofInstlln" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Un-installedOn"></asp:Label>
                            <asp:TextBox ID="txtUnintleddate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtUnintleddate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtUnintleddate" TargetControlID="txtUnintleddate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Reinstalled"></asp:Label>
                            <asp:TextBox ID="txtreintled" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtreintled_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtreintled" TargetControlID="txtreintled" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="installed By"></asp:Label>
                            <asp:TextBox ID="txtinstedBy" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Database Details"></asp:Label>
                            <asp:TextBox ID="txtdbdetails" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <asp:Label runat="server" Text="Description"></asp:Label>
                            <asp:TextBox ID="txtInsDescription" TextMode="multiline" Height="50px" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>

                    </div>

                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <fieldset>
                            <legend class="legendbold">Address</legend>
                        </fieldset>
                        <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>

                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Installation Place"></asp:Label>
                                        <asp:TextBox ID="txtinsplace" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Contact Person"></asp:Label>
                                        <asp:TextBox ID="txtConPerson" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Phone"></asp:Label>
                                        <asp:TextBox ID="txtinsdtlsPhno" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="FAX"></asp:Label>
                                        <asp:TextBox ID="txtInsdtlsFAX" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-7 col-md-7">
                                        <asp:Label runat="server" Text="Address"></asp:Label>
                                        <asp:TextBox ID="txtInsdtlsAddress" autocomplete="off" runat="server" Height="75px" TextMode="MultiLine" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-5 col-md-5">
                                        <asp:Label runat="server" Text="E-mail"></asp:Label>
                                        <asp:TextBox ID="txtInsdtlsEmail" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-5 col-md-5">
                                        <asp:Label runat="server" Text="Website"></asp:Label>
                                        <asp:TextBox ID="txtInsdtlsWebsite" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Maintained By"></asp:Label>
                                        <asp:TextBox ID="txtinsdtlsMaintainedby" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Contact Person"></asp:Label>
                                        <asp:TextBox ID="txtmainbyconperson" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="Phone"></asp:Label>
                                        <asp:TextBox ID="txtMainphno" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <asp:Label runat="server" Text="FAX"></asp:Label>
                                        <asp:TextBox ID="txtmainFAX" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-7 col-md-7">
                                        <asp:Label runat="server" Text="Address"></asp:Label>
                                        <asp:TextBox ID="txtMainaddress" autocomplete="off" Height="75px" TextMode="MultiLine" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-5 col-md-5">
                                        <asp:Label runat="server" Text="E-mail"></asp:Label>
                                        <asp:TextBox ID="txtMainEmail" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-5 col-md-5">
                                        <asp:Label runat="server" Text="Website"></asp:Label>
                                        <asp:TextBox ID="txtMainwebsite" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <div runat="server" role="tabpanel" class="tab-pane" id="divinsurenceDtls">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblinsrncedtls" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12">
                    <div class="pull-right">
                        <asp:ImageButton ID="imgbtninsurance" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save24.png" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Policy Type"></asp:Label>
                                <asp:DropDownList ID="ddlploicytype" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Policy No"></asp:Label>
                                <asp:TextBox ID="txtPolcyno" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Policy Amount"></asp:Label>
                                <asp:TextBox ID="txtPolicyAmt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Premium Paid"></asp:Label>
                                <asp:TextBox ID="txtPremiumpaid" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Term Date"></asp:Label>
                                <asp:TextBox ID="txtTermDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTermDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtTermDate" TargetControlID="txtTermDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="To Date"></asp:Label>
                                <asp:TextBox ID="txtTermToDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtTermToDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtTermToDate" TargetControlID="txtTermToDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Insurance Companyname"></asp:Label>
                                <asp:TextBox ID="txtinscomname" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Contact Person"></asp:Label>
                                <asp:TextBox ID="txtinsConPerns" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Phone No"></asp:Label>
                                <asp:TextBox ID="txtInsPhno" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-md-2">
                                <asp:Label runat="server" Text="FAX"></asp:Label>
                                <asp:TextBox ID="txtinsFAX" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Address"></asp:Label>
                                <asp:TextBox ID="txtInsAddress" autocomplete="off" Height="75px" TextMode="MultiLine" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Email"></asp:Label>
                                <asp:TextBox ID="txtinsEmail" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Website"></asp:Label>
                                <asp:TextBox ID="txtinsWebsite" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div runat="server" role="tabpanel" class="tab-pane" id="divCustDtls">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblCustDetails" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-right">
                            <asp:ImageButton ID="imgBtnCustdtls" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save24.png" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Employee :"></asp:Label>
                          <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                         <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Employee Code:"></asp:Label>
                       <asp:TextBox ID="txtEmpCOde" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Type of Asset"></asp:Label>
                            <asp:DropDownList ID="ddlAssetType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Serial No :"></asp:Label>
                            <asp:TextBox ID="txtSerialNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Approximate Value"></asp:Label>
                            <asp:TextBox ID="txtApprxmiateVal" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Issue Date"></asp:Label>
                            <asp:TextBox ID="txtCustIssueDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCustIssueDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtCustIssueDate" TargetControlID="txtCustIssueDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                        </div>
                         <div class="col-sm-12 col-md-12 divmargin">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Due Date"></asp:Label>
                            <asp:TextBox ID="txtCustDueDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender4" CssClass="cal_Theme1" runat="server" PopupButtonID="txtCustDueDate" TargetControlID="txtCustDueDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Recieved Date"></asp:Label>
                            <asp:TextBox ID="txtRecvdDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" runat="server" PopupButtonID="txtRecvdDate" TargetControlID="txtRecvdDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label runat="server" Text="Date of Return"></asp:Label>
                            <asp:TextBox ID="txtretDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtretDate" TargetControlID="txtretDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                    </div>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label runat="server" Text="Condition When Issued"></asp:Label>
                            <asp:TextBox ID="txtConditnIssued" autocomplete="off" runat="server" CssClass="aspxcontrols" Height="55px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label runat="server" Text="Condition on Recieved"></asp:Label>
                            <asp:TextBox ID="txtCondOnrecvd" autocomplete="off" runat="server" CssClass="aspxcontrols" Height="55px" TextMode="MultiLine"></asp:TextBox>
                        </div>
                         <div class="col-sm-4 col-md-">
                        <asp:Label runat="server" Text="Remarks"></asp:Label>
                        <asp:TextBox ID="txtLnRemarks" autocomplete="off" runat="server" Height="55px" TextMode="MultiLine" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
            
                    </div>
                       </div>
         

            <div runat="server" role="tabpanel" class="tab-pane" id="divMainteanceDtls">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblMaintenanceDtls" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12">
                    <div class="pull-right">
                        <asp:ImageButton ID="imgbtnmaintainance" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save24.png" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                    </div>
                </div>

                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Maintained By"></asp:Label>
                                <asp:TextBox ID="txtMaintained" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Contact Person"></asp:Label>
                                <asp:TextBox ID="txtMainconPerson" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Phone No"></asp:Label>
                                <asp:TextBox ID="txtManufacturedPhoneno" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="FAX"></asp:Label>
                                <asp:TextBox ID="txtManufacturedFAX" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Address"></asp:Label>
                                <asp:TextBox ID="txtManufacturedAddress" Height="75px" TextMode="MultiLine" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Email"></asp:Label>
                                <asp:TextBox ID="txtEmail" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Website"></asp:Label>
                                <asp:TextBox ID="txtWebsite" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="AMC CompanyName"></asp:Label>
                                <asp:TextBox ID="txtbxAMCompname" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="AMC From Date"></asp:Label>
                                <asp:TextBox ID="txtbxAMCfrmDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtbxAMCfrmDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxAMCfrmDate" TargetControlID="txtbxAMCfrmDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>

                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="AMC ToDate"></asp:Label>
                                <asp:TextBox ID="txtbxAMCtoDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtbxAMCtoDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxAMCtoDate" TargetControlID="txtbxAMCtoDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>

                            </div>
                            <div class="col-sm-2 col-md-2">
                                <asp:Label runat="server" Text="AMCAmount"></asp:Label>
                                <asp:TextBox ID="txtAMCAmount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                            </div>
                            <div class="col-sm-8 col-md-8">
                                <br />
                                <asp:Label ID="lblPaymenttype" runat="server" Text="AMC PaymentTerm:-"></asp:Label>
                                &nbsp;&nbsp;
             <asp:RadioButton ID="rbtnOnetime" Text="One Time" GroupName="Select" runat="server" />
                                &nbsp;&nbsp;  
                <asp:RadioButton ID="rbtnInstlmnt" Text="Installament" GroupName="Select" runat="server" />
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="No of Installment"></asp:Label>
                                <asp:TextBox ID="txtNoInstlmt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>

                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Installment Amount"></asp:Label>
                                <asp:TextBox ID="txtInstamount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Total Paid Installment Amount"></asp:Label>
                                <asp:TextBox ID="txtTotalPaidIstAmnt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-2 col-md-2">
                                <asp:Label runat="server" Text="Amount"></asp:Label>
                                <asp:TextBox ID="txtAmount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>
            <div runat="server" role="tabpanel" class="tab-pane" id="divLoanAsst">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label3" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                  <div class="col-sm-12 col-md-12">
                        <div class="pull-right">
                            <asp:ImageButton ID="imgBtnLoanAsst" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save24.png" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                        </div>
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
                        <asp:Label runat="server" Text="Exchange Amount"></asp:Label>
                        <asp:TextBox ID="txtLoanExchgeAmt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label runat="server" Text="Amount in Ruppees"></asp:Label>
                        <asp:TextBox ID="txtLoanAmtRs" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>
                <%--  <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Repaid"></asp:Label>
                        <asp:TextBox ID="txtLnRepaid" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Balance to Date"></asp:Label>
                        <asp:TextBox ID="txtBlnceTpaid" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" CssClass="cal_Theme1" runat="server" PopupButtonID="txtBlnceTpaid" TargetControlID="txtBlnceTpaid" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                    </div>
                </div>--%>
            </div>
            <div runat="server" role="tabpanel" class="tab-pane active" id="divDevicedtls">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label1" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12">
                    <div class="pull-right">
                        <asp:ImageButton ID="imgbtnDeviceDetails" CssClass="activeIcons hvr-bounce-out" runat="server" ImageUrl="~/Images/Save24.png" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Device type"></asp:Label>
                                <asp:DropDownList ID="ddldevicetype" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Device No"></asp:Label>
                                <asp:TextBox ID="txtxDeviceNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Model Name"></asp:Label>
                                <asp:TextBox ID="txtModelname" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Manufactured By"></asp:Label>
                                <asp:TextBox ID="txtmanufacturedby" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Date of Purchase"></asp:Label>
                                <asp:TextBox ID="txtdateofpurchase" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdateofpurchase_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtdateofpurchase" TargetControlID="txtdateofpurchase" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>

                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Warranty Expireson"></asp:Label>
                                <asp:TextBox ID="txtWarranty" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtWarranty_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtWarranty" TargetControlID="txtWarranty" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <asp:Label runat="server" Text="Employee Name"></asp:Label>
                                <asp:TextBox ID="txtempname" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <asp:Label runat="server" Text="Details..."></asp:Label>
                                <asp:TextBox ID="txtDetails" autocomplete="off" Height="50px" Width="500px" TextMode="MultiLine" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="col-sm-7 col-md-7">
                                <asp:RadioButton ID="rbtnstandalone" Text="Standalone" repeatedirection="Vertical" GroupName="Select" runat="server" />
                            </div>
                            <div class="col-sm-7 col-md-7">
                                <asp:RadioButton ID="rbtnServer" Text="Server" repeatedirection="Vertical" GroupName="Select" runat="server" />
                            </div>
                            <div class="col-sm-7 col-md-7">
                                <asp:RadioButton ID="rbtnAttachedServer" Text="Attached Toserver" repeatedirection="Vertical" GroupName="Select" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <br />
                            <div class="col-sm-6 col-md-6">
                                <asp:Label runat="server" Text="Description..."></asp:Label>
                                <asp:TextBox ID="txtDescription" autocomplete="off" Height="50px" Width="500px" TextMode="MultiLine" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
   </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
        </div>
    </div>
    <div id="ModalAdditionalDetails" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAssetAdditionDtlsMsg" runat="server"></asp:Label></strong>
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

</asp:Content>

