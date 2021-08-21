<%@ Page Title="" Language="VB" MasterPageFile="~/Search.master" AutoEventWireup="false" CodeFile="Search.aspx.vb" Inherits="Search_Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/bootstrap-multiselect.css" rel="stylesheet" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=lstDesc.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });
        });
    </script>
    <style>
         div {
            color: black;
        }
    </style>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-8 col-md-8 pull-left">
                <h2><b>Search</b></h2>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-4 col-md-4" style="padding: 0px;">
            <div class="form-group">
                <asp:Label ID="lblIndex" runat="server" Text="Index Of" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ddlIndex" runat="server" CssClass="aspxcontrols" TabIndex="2" AutoPostBack="true">
                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Cabinets"></asp:ListItem>
                    <asp:ListItem Value="2" Text="SubCabinets"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Folders"></asp:ListItem>
                    <asp:ListItem Value="4" Text="DocumentTypes"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Keywords"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Descriptors"></asp:ListItem>
                    <asp:ListItem Value="7" Text="Format"></asp:ListItem>
                    <asp:ListItem Value="8" Text="Created by"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblDesc" runat="server" Text="Description" Font-Bold="true"></asp:Label>
                <asp:ListBox ID="lstDesc" runat="server" SelectionMode="Multiple" Width="55%" CssClass="aspxcontrols"></asp:ListBox>
            </div>
        </div>
        <div class="col-sm-5 col-md-5">
            <div class="form-group">
                <asp:Button runat="server" Text="Add to Query" class="btn-ok" ID="btnAddQuery"></asp:Button>
                <asp:Button runat="server" Text="Reset" class="btn-ok" ID="btnReset"></asp:Button>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px;">
        <div class="col-sm-4 col-md-4" style="padding: 0px; overflow: auto; height: 470px;">
            <asp:GridView ID="dgParam" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" PageSize="5000">
                <Columns>
                    <asp:BoundField DataField="Fields" HeaderText="Fields"></asp:BoundField>
                    <asp:TemplateField HeaderText="Add Parameter">
                        <ItemTemplate>
                            <asp:TextBox ID="txtParam" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container.DataItem, "SelectedName") %>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="False"></asp:BoundField>
                    <asp:BoundField DataField="SelectedID" HeaderText="SelectedID" Visible="False"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-sm-8 col-md-8" style="padding-right: 0px; overflow: auto; height: 465px;">
            <asp:GridView ID="dgViewSearchData" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" PageSize="5000">
                <Columns>
                    <asp:BoundField DataField="DetailsId" HeaderText="DetailsID" Visible="False"></asp:BoundField>
                    <asp:BoundField DataField="BaseID" Visible="False" HeaderText="BaseID"></asp:BoundField>
                    <asp:TemplateField ItemStyle-Width="5px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" AutoPostBack="True" runat="server" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Title">
                        <ItemTemplate>
                            <asp:Label ID="lblBaseName" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DetailsId") %>'></asp:Label>
                            <asp:Label ID="lblDetailsID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.BaseID") %>'></asp:Label>
                            <%--<asp:LinkButton ID="lnkTitle" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.Title") %>' CssClass="aspxlabelbold"></asp:LinkButton>--%>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CabName" HeaderText="Cabinet" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="SubCabName" HeaderText="Sub Cabinet" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="FolName" HeaderText="Folder" Visible="false"></asp:BoundField>
                    <asp:BoundField DataField="DocType" HeaderText="Document Types"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px;">
        <asp:GridView ID="dgSelectedData" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" PageSize="5000">
            <Columns>
                <asp:BoundField DataField="Details" HeaderText="Details"></asp:BoundField>
                <asp:BoundField DataField="Descriptors" HeaderText="Descriptors"></asp:BoundField>
                <asp:BoundField DataField="KeyWords" HeaderText="KeyWords">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                </asp:BoundField>
                <asp:BoundField DataField="ScanDocument" HeaderText="Scan Document"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalSearchValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>EDICT</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblSearchValidationMsg" runat="server"></asp:Label></strong>
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
    <div id="ModalSearchLinkValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>EDICT</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgLinkType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblSearchLinkValidationMsg" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" Text="Ok" class="btn-ok" ID="btnSearchLinkMsgOk"></asp:Button>
                    <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnCancel"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

