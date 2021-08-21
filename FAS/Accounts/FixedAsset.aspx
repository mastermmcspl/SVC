<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="FixedAsset.aspx.vb" Inherits="Accounts_FixedAsset" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>Fixed Asset</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnUnBlock" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Unblock" />
                    <asp:ImageButton ID="imgbtnUnLock" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Unlock" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href ="#" data-toggle="dropdown" style="padding: 0px;"><span>
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

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px" >
        <div class="col-sm-4 col-md-4">
            <asp:Label ID="lblBranch" runat="server" Text="Select Branch"></asp:Label>
            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label ID="lblSelect" runat="server" Text="Select"></asp:Label>
            <asp:DropDownList ID="ddlSelect" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                <asp:ListItem Value="1">Gross Block</asp:ListItem>
                <asp:ListItem Value="2">Net Block</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="gvFixedAsset" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <Columns>
                <asp:BoundField DataField="GL" Visible="false"></asp:BoundField>
                <asp:BoundField DataField="glHead" Visible="false" />
                <asp:BoundField DataField="Description" />
                <asp:BoundField DataField="NoteNo"  />
                <asp:BoundField DataField="OpeningBalance" >
                <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Additions"  />
                <asp:BoundField DataField="Deductions"  />
                <asp:BoundField DataField="ClosingBlock" />
                <asp:BoundField DataField="Dep"  />
                <asp:BoundField DataField="Depreciation" />
                <asp:BoundField DataField="DepDeduction" />
                <asp:BoundField DataField="Upto" ></asp:BoundField>
                <asp:BoundField DataField="MarchF" />
                <asp:BoundField DataField="March2"  />
            </Columns>

        </asp:GridView>
    </div>
    <div id="ModalEmpMasterValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblEmpMasterValidationMsg" runat="server"></asp:Label></strong>
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

