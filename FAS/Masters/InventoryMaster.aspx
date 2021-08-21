<%@ Page Language="VB" MasterPageFile="~/Inventory.master" AutoEventWireup="false" CodeFile="InventoryMaster.aspx.vb" Inherits="Inventory_InventoryMaster" Title="FAS" ValidateRequest="false" Debug="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }

        div {
            color: black;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolKit" TagPrefix="cc1" %>
    <%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });

    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>1 Inventory Master</b></h2>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label ID="Label1" Text="Search by" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="aspxcontrols" Width="100px">
                </asp:DropDownList>
                <asp:TextBox autocomplete="off" ID="txtSearch" runat="server" Width="120px" CssClass="aspxcontrols" />
                <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="Label16" Text="HSNDescription" runat="server"></asp:Label>
                    <asp:TextBox autocomplete="off" ID="txtHSNDesc" runat="server" Width="100px" CssClass="aspxcontrols" />
                    <asp:ImageButton ID="imgbtnHSNDescSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbNewItem" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create New SubGroup" />
                    <asp:ImageButton ID="imgbtnGroup" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create New Group" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Clear All" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 " style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="Label17" Text="HSNCode" runat="server"></asp:Label>
                <asp:TextBox autocomplete="off" ID="txtHSNCode" runat="server" Width="100px" CssClass="aspxcontrols" />
                <asp:ImageButton ID="imgbtnHSTCodeSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="Label18" Text="Commodity" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlCommodity" runat="server" Width="130px" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>

    </div>
    <div class="col-sm-12 col-md-12">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 " style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Selected Path :-" Style="padding-left: 0px"></asp:Label>
                <asp:Label ID="lblPath" runat="server" Text="Selected path" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 " style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblAcode" runat="server" Text="Article No"></asp:Label>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtAcode"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Item Color" ID="lblColor"></asp:Label>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIColor" runat="server" ControlToValidate="txtColor" Display="Dynamic" ErrorMessage="Enter Valid Item Color." SetFocusOnError="True" ValidationExpression="^[A-z]+$"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtColor"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Size" ID="lblsize"></asp:Label>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtSize"></asp:TextBox>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSize" runat="server" ControlToValidate="txtSize" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Color Code" ID="lblCcode"></asp:Label>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtCcode"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="* Item Code" ID="lblCode"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCode" runat="server" ControlToValidate="txtCode" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Item Code." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtCode"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="* Description" ID="lblDescription"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescription" runat="server" ControlToValidate="txtDescription" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top" ID="txtDescription"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Created By" ID="lblCreatedby"></asp:Label>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtCreatedBy"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Created On" ID="lblCreatedOn"></asp:Label>
                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtCreatedOn"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 pre-scrollableborder form-group">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblHSNDeatails" runat="server" Text="HSN Details" Font-Bold="true" Font-Size="Medium"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-12 col-md-12">
                    <asp:Label runat="server" Text="Select Description" ID="Label10"></asp:Label>
                    <asp:DropDownList ID="ddlGSTSchedule" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Schedule Type" ID="Label11"></asp:Label>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtScheduleType"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* GST Rate %" ID="Label12"></asp:Label>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtGSTRate"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Sl.No Of the Schedule" ID="Label2"></asp:Label>
                        <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSlNO" runat="server" ControlToValidate="txtSLNo" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Sl.No Of the Schedule." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtSLNo"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Chapter/Heading/SubHeading" ID="Label3"></asp:Label>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVChapter" runat="server" ControlToValidate="txtChapter" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Chapter." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtCHST"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Chapter" ID="Label13"></asp:Label>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtChapter"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Heading" ID="Label14"></asp:Label>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtHeading"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Sub Heading" ID="Label15"></asp:Label>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtSubHeading"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Tarrif" ID="Label4"></asp:Label>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTarrif" runat="server" ControlToValidate="txtTarrif" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Tarrif." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtTarrif"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Sub Sl.No" ID="Label9"></asp:Label>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSubSlNo" runat="server" ControlToValidate="txtSubSlNo" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Sub Sl.No." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtSubSlNo"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* CESS" ID="Label5"></asp:Label>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCESS" runat="server" ControlToValidate="txtCESS" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter CESS." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtCESS"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-6 col-md-6">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Description of Goods" ID="Label8"></asp:Label>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVGoodDescription" runat="server" ControlToValidate="txtGoodDescription" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Goods Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox autocomplete="off" Height="78px" runat="server" TextMode="MultiLine" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtGoodDescription"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Notification No" ID="Label6"></asp:Label>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNotificationNo" runat="server" ControlToValidate="txtNotificationNo" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Enter Notification No." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtNotificationNo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Notification Date"></asp:Label>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNotificationDate" runat="server" ControlToValidate="txtNotificationDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Notification Date" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVNotificationDate" runat="server" ControlToValidate="txtNotificationDate" Display="Dynamic" ErrorMessage="Enter Valid Notification Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtNotificationDate" autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtNotificationDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtNotificationDate" TargetControlID="txtNotificationDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* File No" ID="Label7"></asp:Label>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFileNo" runat="server" ControlToValidate="txtFileNo" Display="Dynamic" SetFocusOnError="True"
                                    ErrorMessage="Enter FileNo." ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" data-toggle="tooltip" data-placement="top" ID="txtFileNo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* File Date"></asp:Label>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFileDate" runat="server" ControlToValidate="txtFileDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter File Date" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFileDate" runat="server" ControlToValidate="txtFileDate" Display="Dynamic" ErrorMessage="Enter Valid File Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtFileDate" autocomplete="off" runat="server" CssClass="aspxcontrolsdisable" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtFileDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtFileDate" TargetControlID="txtFileDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>

                </div>

            </div>


        </div>
        <div class="col-sm-12 col-md-12">
            <div id="div1" runat="server" style="padding-left: 0px; border-style: solid; border-color: inherit; border-width: medium; overflow: auto; width: 100%; height: 350px; background-color: #FFFFFF;">
                <asp:ListBox ID="lstBoxDescription" CssClass="aspxcontrols" AutoPostBack="true" runat="server" Height="300px"
                    Font-Names="Verdana" Font-Size="Smaller"></asp:ListBox>
            </div>
        </div>
        <div class="col-sm-12 col-md-12">
            <div id="divProcess" runat="server" style="padding-left: 0px; border-style: solid; border-color: inherit; border-width: medium; overflow: auto; width: 100%; height: 350px; background-color: #FFFFFF;">
                <wtv:TreeView ID="tvCategory" class="td_blue1b" runat="server" Animate="False" AutoPostBack="True" CssFile="../StyleSheet/TreeStyles.css" ForeColor="Black" Height="300px" NodeCssClass="MyNode" NodeCssClassLoading="MyNodeLoading" NodeCssClassOver="MyNodeOver" NodeCssClassSelected="MyNodeSelected" NodeImageUrl="../Images/TreeImages/FOLDER.gif" NodeImageUrlEmpty="../Images/TreeImages/greenfolder.gif" NodeImageUrlExpanded="../Images/TreeImages/folderopen.gif" PathToImages="../Images/TreeImages" PathToImagesValidate="False" PopulateOnDemand="true" ScrollBars="true" Style="z-index: 110; left: 2px;" Width="500px">
                </wtv:TreeView>
            </div>
        </div>
        <div>
            <asp:TextBox autocomplete="off" ID="txtCurrentID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
            <asp:TextBox autocomplete="off" ID="txtDepthID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
            <asp:TextBox autocomplete="off" ID="txtParentID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
            <asp:TextBox autocomplete="off" ID="txtOrgStrCurrentLvlName" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
            <asp:TextBox autocomplete="off" ID="txtOrgStrNextLvlName" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
            <asp:TextBox autocomplete="off" ID="txtSaveOrUpdate" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
            <asp:Label ID="lblNode" runat="server" Visible="False"></asp:Label>
        </div>
        <div id="ModalUserMasterDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modalmsg-dialog">
                <div class="modalmsg-content">
                    <div class="modalmsg-header">
                        <h4 class="modal-title"><b>FAS</b></h4>
                    </div>
                    <div class="modalmsg-body">
                        <div id="divMsgType" class="alert alert-info">
                            <p>
                                <strong>
                                    <asp:Label ID="lblUserMasterDetailsValidationMsg" runat="server"></asp:Label></strong>
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
        <rsweb:reportviewer id="ReportViewer1" runat="server" width="99%" height="10px" visible="false" pagecountmode="Actual" xmlns:rsweb="microsoft.reporting.webforms"></rsweb:reportviewer>
</asp:Content>
