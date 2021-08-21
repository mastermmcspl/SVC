<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="Indexing.aspx.vb" Inherits="DigitalFiling_Indexing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--   <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>--%>
    <style>
         div {
            color: black;
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
    <%--  <div class="loader"></div>--%>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>6. File Upload</b></h2>
            </div>
            <div class="col-sm-9 col-md-9">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnIndex" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index File" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label ID ="lblUploadType" runat ="server" Text ="Select Upload Type"></asp:Label>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:RadioButton ID="rboRemote" runat="server" Text="Remote Data Transactions Upload" AutoPostBack="true" GroupName ="Select" />
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:RadioButton ID="rboNormal" runat="server" Text="Normal File Upload" AutoPostBack="true" GroupName ="Select" />
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <div class="col-sm-5 col-md-5" style="padding-left: 0px">
            <div class="form-group">
                <asp:FileUpload ID="txtfile" runat="server" Width="95%" CssClass="btn-ok" AllowMultiple="true" />
            </div>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:DropDownList ID ="ddlFileType" runat ="server" CssClass="aspxcontrols" AutoPostBack ="true" ></asp:DropDownList>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
        </div>
         <div class="col-sm-1 col-md-1">
            <asp:Button ID="btnRemoteIndex" runat="server" Text="Index" CssClass="btn-ok" />
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
         <div class="col-sm-2 col-md-2">
            <asp:Label ID ="lblCustomer" runat ="server" Text ="Company Name"></asp:Label>
            <asp:DropDownList ID ="ddlCustomer" runat ="server" CssClass="aspxcontrols" ></asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label ID ="lblTrType" runat ="server" Text ="Transaction Type"></asp:Label>
            <asp:DropDownList ID ="ddlTrType" AutoPostBack ="true" runat ="server" CssClass="aspxcontrols" ></asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label ID ="lblNoOfTr" runat ="server" Text ="No Of Transactions"></asp:Label>
            <asp:TextBox ID ="txtNoOfTr" runat ="server" CssClass ="aspxcontrols" ></asp:TextBox>
        </div>
         <div class="col-sm-2 col-md-2">
             <asp:Label ID ="lblDebitTotal" runat ="server" Text ="Debit Total"></asp:Label>
            <asp:TextBox ID ="txtDebitTotal" runat ="server" CssClass ="aspxcontrols" ></asp:TextBox>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label ID ="lblCreditTotal" runat ="server" Text ="Credit Total"></asp:Label>
            <asp:TextBox ID ="txtCreditTotal" runat ="server" CssClass ="aspxcontrols" ></asp:TextBox>
        </div>
         <div class="col-sm-2 col-md-2">
            <asp:Label ID ="lblBatchNo" runat ="server" Text ="BatchNo"></asp:Label>
            <asp:DropDownList ID ="ddlBatchNo" runat ="server" CssClass="aspxcontrols" ></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <asp:GridView ID="gvattach" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="1%">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
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
                <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalValidation" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsg" runat="server"></asp:Label></strong>
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

