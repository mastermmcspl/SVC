<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="ApplicationSettings.aspx.vb" Inherits="Masters_ApplicationSettings" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCGroup.ClientID%>').select2();
            $('#<%=ddlCSubGroup.ClientID%>').select2();
            $('#<%=ddlCGL.ClientID%>').select2();

            $('#<%=ddlCVatGroup.ClientID%>').select2();
            $('#<%=ddlCVatSubGroup.ClientID%>').select2();
            $('#<%=ddlcVatGL.ClientID%>').select2();
            $('#<%=ddlCVATSubGL.ClientID%>').select2();

            $('#<%=ddlcCSTGroup.ClientID%>').select2();
            $('#<%=ddlCCSTSubGroup.ClientID%>').select2();
            $('#<%=ddlCCSTGL.ClientID%>').select2();
            $('#<%=ddlcCSTSubGL.ClientID%>').select2();

            $('#<%=ddlCExciseGroup.ClientID%>').select2();
            $('#<%=ddlCExciseSubGroup.ClientID%>').select2();
            $('#<%=ddlCExciseGL.ClientID%>').select2();
            $('#<%=ddlCExciseSubGL.ClientID%>').select2();

            $('#<%=ddlCSalesGroup.ClientID%>').select2();
            $('#<%=ddlCSalesSubGroup.ClientID%>').select2();
            $('#<%=ddlCSalesGL.ClientID%>').select2();
            $('#<%=ddlCSalesSubGL.ClientID%>').select2();


            $('#<%=ddlSGroup.ClientID%>').select2();
            $('#<%=ddlSSubGroup.ClientID%>').select2();
            $('#<%=ddlSGL.ClientID%>').select2();

            $('#<%=ddlSVATGroup.ClientID%>').select2();
            $('#<%=ddlSVATSubGroup.ClientID%>').select2();
            $('#<%=ddlSVATGL.ClientID%>').select2();
            $('#<%=ddlSVATSubGL.ClientID%>').select2();

            $('#<%=ddlSCSTGroup.ClientID%>').select2();
            $('#<%=ddlSCSTSubGroup.ClientID%>').select2();
            $('#<%=ddlSCSTGL.ClientID%>').select2();
            $('#<%=ddlSCSTSubGL.ClientID%>').select2();

            $('#<%=ddlSExciseGroup.ClientID%>').select2();
            $('#<%=ddlSExciseSubGroup.ClientID%>').select2();
            $('#<%=ddlSExciseGL.ClientID%>').select2();
            $('#<%=ddlSExciseSubGL.ClientID%>').select2();

            $('#<%=ddlSPurchaseGroup.ClientID%>').select2();
            $('#<%=ddlSPurchaseSubGroup.ClientID%>').select2();
            $('#<%=ddlSPurchaseGL.ClientID%>').select2();
            $('#<%=ddlSPurchaseSubGL.ClientID%>').select2();

            $('#<%=ddlFEGGroup.ClientID%>').select2();
            $('#<%=ddlFEGSubGroup.ClientID%>').select2();
            $('#<%=ddlFEGGL.ClientID%>').select2();
            $('#<%=ddlFEGSubGL.ClientID%>').select2();

            $('#<%=ddlFELGroup.ClientID%>').select2();
            $('#<%=ddlFELSubGroup.ClientID%>').select2();
            $('#<%=ddlFELGL.ClientID%>').select2();
            $('#<%=ddlFELSubGL.ClientID%>').select2();
        });



    </script>
    <style>
        div {
            color: black;
        }
    </style>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>2.3 Account Settings</b></h2>
            </div>
            <div class="form-group pull-right">
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" TabIndex="18" ValidationGroup="Validate" />
                <ul class="nav navbar-nav navbar-right logoutDropdown">
                    <li class="dropdown">
                        <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                            <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" visible="false" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                            <li role="separator" class="divider"></li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-sm-12 col-md-12 divmargin">
                <fieldset>
                    <legend class="legendbold">Customer Settings</legend>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(Customer) Group"></asp:Label>
                                <asp:DropDownList ID="ddlCGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlCSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlCGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* (SGST) Group"></asp:Label>
                                <asp:DropDownList ID="ddlCVatGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlCVatSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlcVatGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCVATSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* (CGST) Group" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlcCSTGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCCSTSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCCSTGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlcCSTSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* (IGST) Group" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCExciseGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCExciseSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCExciseGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCExciseSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(Sales) Group"></asp:Label>
                                <asp:DropDownList ID="ddlCSalesGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlCSalesSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCSalesGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlCSalesSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>


            <div class="col-sm-12 col-md-12 form-group">
                <fieldset>
                    <legend class="legendbold">Supplier Settings</legend>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(Supplier) Group"></asp:Label>
                                <asp:DropDownList ID="ddlSGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlSSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlSGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* (SGST) Group"></asp:Label>
                                <asp:DropDownList ID="ddlSVATGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlSVATSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlSVATGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSVATSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(CGST) Group" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSCSTGroup" runat="server" Visible="false" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSCSTSubGroup" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSCSTGL" runat="server" Visible="false" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Visible="false" Text="* Sub General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlSCSTSubGL" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(IGST) Group" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSExciseGroup" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSExciseSubGroup" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSExciseGL" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSExciseSubGL" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(Purchase) Group"></asp:Label>
                                <asp:DropDownList ID="ddlSPurchaseGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlSPurchaseSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSPurchaseGL" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub General Ledger" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlSPurchaseSubGL" Visible="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>

            <div class="col-sm-12 col-md-12 form-group">
                <fieldset>
                    <legend class="legendbold">General Settings</legend>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(Cash) Group"></asp:Label>
                                <asp:DropDownList ID="ddlCashGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlCashSubgroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlCashGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*Sub General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlCashSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12 col-md-12" style="padding: 0px">

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(Bank) Group"></asp:Label>
                                <asp:DropDownList ID="ddlBankGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlBankSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlBankGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*Sub General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlBankSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </fieldset>
            </div>
            <div class="col-sm-12 col-md-12 form-group">
                <fieldset>
                    <legend class="legendbold">Foreign Exchange Settings</legend>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(Foreign Exchange Gain) Group"></asp:Label>
                                <asp:DropDownList ID="ddlFEGGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlFEGSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlFEGGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*Sub General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlFEGSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*(Foreign Exchange Loss) Group"></asp:Label>
                                <asp:DropDownList ID="ddlFELGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Sub Group"></asp:Label>
                                <asp:DropDownList ID="ddlFELSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlFELGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label runat="server" Text="*Sub General Ledger"></asp:Label>
                                <asp:DropDownList ID="ddlFELSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </fieldset>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlCGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlCVatGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCVatSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlcVatGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlcVatSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlcCSTGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCCSTSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCCSTGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCCSTSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlCExciseGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCExciseSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCExciseGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCExciseSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlCSalesGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCSalesSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCSalesGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCSalesSubGL" EventName="SelectedIndexChanged" />


            <asp:AsyncPostBackTrigger ControlID="ddlSGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlSVATGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSVATSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSVATGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSVATSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlSCSTGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSCSTSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSCSTGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSCSTSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlSExciseGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSExciseSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSExciseGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSExciseSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlSPurchaseGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSPurchaseSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSPurchaseGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSPurchaseSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlCashGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCashSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCashGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCashSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlBankGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBankSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBankGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlBankSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlFEGGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFEGSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFEGGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFEGSubGL" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="ddlFELGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFELSubGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFELGL" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlFELSubGL" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

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
