<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="AssetMasters.aspx.vb" Inherits="Masters_AssetMasters" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
     <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>4.1 Other Master </b></h2>
            </div>
            <div class="pull-right">
<%--                <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />--%>
                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
<%--                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />--%>
<%--                <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />--%>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="pull-left">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <asp:TextBox ID="txtAsstid" autocomplete="off" runat="server" Visible="false"></asp:TextBox>


    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblMasterHead" runat="server" Text="Asset Types"></asp:Label>
                <asp:DropDownList ID="ddlAssetType"  AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        &nbsp;&nbsp;

        <div class="col-sm-3 col-md-3" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblDepRate" runat="server" Text="* Depreciation Rate Per Book"></asp:Label>
                <asp:TextBox ID="txtdeprcnrate" runat="server" AutoCompleteType="Disabled" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
         <div class="col-sm-3 col-md-3" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblItRate" runat="server" Text="* Depreciation Rate Per IncomeTax"></asp:Label>
                <asp:TextBox ID="TxtIncmTax" runat="server" AutoCompleteType="Disabled" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
    </div>
     <div id="ModalGeneralMasterDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblGeneralMasterDetailsValidationMsg" runat="server"></asp:Label>
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

