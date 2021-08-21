<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="ModulePermission.aspx.vb" Inherits="Masters_ModulePermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/sweetalert.css" rel="stylesheet" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/sweetalert-dev.js" type="text/javascript"></script>

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
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>2.7 Permission</b></h2>
            </div>
            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="form-group divmargin">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label>

                <asp:DropDownList ID="ddlModules" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <br />
                <asp:Label ID="lblPermissionType" runat="server" Text="Permission Type:- " Width="110px"></asp:Label>
                <asp:RadioButton ID="rboRole" Text="Role Based" GroupName="Select" Checked="true" runat="server" AutoPostBack="true" Width="90px" />
                <asp:RadioButton ID="rboUser" Text="User Based" GroupName="Select" runat="server" Width="90px" AutoPostBack="true" />
            </div>
        </div>
        <div class="col-sm-4 col-md-4" style="padding-right: 0px">
            <div class="form-group">
                <asp:Label ID="lblName" runat="server" Text="* Role"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRole" runat="server" ControlToValidate="ddlPermission" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlPermission" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:DataGrid ID="dgPermission" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="Mod_Id" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Mod_Description" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Permission" >
                    <ItemTemplate>
                        <asp:CheckBox ID="chkView" Text="View" runat="server" CssClass="aspxradiobutton hvr-bounce-in" width="8%"/>
                        <asp:CheckBox ID="chkNew" Text="New" runat="server" CssClass="aspxradiobutton hvr-bounce-in" width="15%"/>
                        <asp:CheckBox ID="chkSaveOrUpdate" Text="Save/Update" runat="server" CssClass="aspxradiobutton hvr-bounce-in"  width="20%" />
                        <asp:CheckBox ID="chkApprove" Text="Approve" runat="server" CssClass="aspxradiobutton hvr-bounce-in"  width="17%" />
                        <asp:CheckBox ID="chkActivateOrDeactivate" Text="Activate/Deactivate" runat="server" CssClass="aspxradiobutton hvr-bounce-in"  width="17%" />
                        <asp:CheckBox ID="chkReport" Text="Report" runat="server" CssClass="aspxradiobutton hvr-bounce-in"  width="16%" />
                        <asp:CheckBox ID="chkDownload" Text="Download" runat="server" CssClass="aspxradiobutton hvr-bounce-in"   width="15%"/>
                        <asp:CheckBox ID="chkAnnotation" Text="Annotation" runat="server" CssClass="aspxradiobutton hvr-bounce-in"   width="15%"/>
                         <asp:CheckBox ID="chkException" Text="Exception" runat="server" CssClass="aspxradiobutton hvr-bounce-in"   width="15%"/>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="75%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
               
                <asp:BoundColumn DataField="mod_Function" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                 <asp:BoundColumn DataField="Mod_Buttons" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
    </div>

    <%--<div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="form-group">
            <asp:DataGrid ID="dgPermission" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable" onrowdatabound="PickColor_RowDataBound">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="ID" Visible="false">
                        <ItemStyle HorizontalAlign="Left" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
                            Font-Overline="False" Font-Bold="False" VerticalAlign="Top"></ItemStyle>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="SLNo">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Module" HeaderText="Module">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="35%" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <ItemTemplate>
                            <asp:CheckBoxList ID="chkOperation" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            </asp:CheckBoxList>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="65%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in"></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="IbChk" runat="server" ImageUrl="../Images/chk.jpg" CommandName="Select" CssClass="aspxradiobutton hvr-bounce-in"></asp:ImageButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Navigation" HeaderText="Navigation" Visible="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </div>--%>
    <div id="ModalModulePermissionValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>GRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModulePermissionValidationMsg" runat="server"></asp:Label></strong>
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
