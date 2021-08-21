<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Master.master" CodeFile="InventoryLinkageMaster.aspx.vb" Inherits="Masters_InventoryLinkageMaster" ValidateRequest="false" Debug="true" %>

<%--<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
       <style>        
                div{
            color:black;
                      }        
        </style>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-4 col-md-4 pull-left">
                <h2><b>3.1 Inventory Linkage Master</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <asp:Label ID="Label1" Text="Search by" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="aspxcontrols" Width="140px">
                </asp:DropDownList>
                <asp:TextBox autocomplete="off" ID="txtSearch" runat="server" Width="140px" CssClass="aspxcontrols" />
                <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Search by." SetFocusOnError="True" ControlToValidate="ddlSearch" ValidationGroup="Search" Display="Static" InitialValue="Select"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton CssClass="activeIcons hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="New" runat="server" />
                    <asp:ImageButton CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
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
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label runat="server" Text="Group"></asp:Label>
                <asp:DropDownList runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label runat="server" Text="Sub Group"></asp:Label>
                <asp:DropDownList runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label runat="server" Text="General Ledger"></asp:Label>
                <asp:DropDownList runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <fieldset>
                <legend class="legendbold">Inventory Details</legend>
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                    <asp:Label runat="server" Text="Search By Inventory Category"></asp:Label>
                    <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
                <div class="col-sm-12 col-md-12 pre-scrollableborder" style="height: 300px">
                </div>
            </fieldset>
        </div>
        <div class="col-sm-6 col-md-6">

        </div>
    </div>
    <div>
        <asp:TextBox autocomplete="off" ID="txtCurrentID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtDepthID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtParentID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtOrgStrCurrentLvlName" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtOrgStrNextLvlName" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtSaveOrUpdate" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
    </div>
    <div id="ModalValidationOrgStructurer" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsgOrgStructurer" runat="server"></asp:Label></strong>
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
