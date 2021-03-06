<%@ Page Language="VB" MasterPageFile="~/Inventory.master" AutoEventWireup="false" CodeFile="InventoryMasterDetails.aspx.vb" Inherits="Inventory_InventoryMasterDetails" ValidateRequest="false" Debug="true" %>

<%--<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>--%>
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

        .auto-style1 {
            resize: none;
            font-size: 12px;
            line-height: 1.42857143;
            color: #444444;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            font-family: "Segoe UI", sans-serif;
            border: 1px solid #ccc;
            margin-top: 1px;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }

        .auto-style2 {
            width: 100%;
            height: 26px;
            resize: none;
            font-size: 12px;
            line-height: 1.42857143;
            color: #444444;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            font-family: "Segoe UI", sans-serif;
            border: 1px solid #ccc;
            margin-top: 1px;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }

        div {
            color: black;
        }
    </style>
    <%--<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/bootstrap-multiselect.css" rel="stylesheet" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap-multiselect.js"></script>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />

    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgHistory.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCtgry.ClientID%>').select2();
            $('#<%=ddlCommodity.ClientID%>').select2();
            $('#<%=ddlItem.ClientID%>').select2();
        });

    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>2 Inventory Price Details</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="pull-right">

                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateCustomer" />
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

    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Commodity"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCommodity" runat="server" ControlToValidate="ddlCommodity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Commodity" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlCommodity" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Item"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVItem" runat="server" ControlToValidate="ddlItem" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Item" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlItem" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="Sub Category" Visible="false"></asp:Label>
            <asp:DropDownList runat="server" CssClass="aspxcontrols" Visible="false"></asp:DropDownList>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 pre-scrollableborder form-group">
        <div class="col-sm-12 col-md-12">
            <div class="col-sm-8 col-md-8" style="padding-left: 0px">
                <div class="form-group">
                    <asp:Label runat="server" Text="Selected Path :-"></asp:Label>
                    <asp:Label ID="lblPath" runat="server" CssClass="aspxlabelbold"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" Text="Item Code :-"></asp:Label>
                    <asp:Label ID="lblCode" runat="server" CssClass="aspxlabelbold"></asp:Label>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="Price Category"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCtgry" runat="server" ControlToValidate="ddlCtgry" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Price Category" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlCtgry" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlUnit" runat="server" ControlToValidate="ddlUnit" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Unit of Measurement" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlUnit" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="* Alternative Unit"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlAlternative" runat="server" ControlToValidate="ddlAlternative" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Alternative Unit" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlAlternative" runat="server" CssClass="aspxcontrols"></asp:DropDownList>

                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="* No. Of Units"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPerPieces" runat="server" ControlToValidate="txtPerPieces" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter No Of Units" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPerPieces" runat="server" ControlToValidate="txtPerPieces" Display="Dynamic" ErrorMessage="Enter Valid No Of Units" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,100}$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtPerPieces" autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>

                </div>
            </div>
        </div>

        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="MRP"></asp:Label>
                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtMRP" runat="server" ControlToValidate="txtMRP" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter MRP" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMRP" runat="server" ControlToValidate="txtMRP" Display="Dynamic" ErrorMessage="Enter Valid MRP Rate" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtMRP" autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="Effective From"></asp:Label>
                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="MRFVEffeFrom" runat="server" ControlToValidate="MtxtEffeFrom" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Effective From Date" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="MREVEffeFrom" runat="server" ControlToValidate="MtxtEffeFrom" Display="Dynamic" ErrorMessage="Enter Valid Effective From Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="MtxtEffeFrom" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <cc1:CalendarExtender ID="MtxtEffeFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="MtxtEffeFrom" TargetControlID="MtxtEffeFrom" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                    <%--<asp:RangeValidator ID="rgvMtxtEffeFrom" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="MtxtEffeFrom" SetFocusOnError="True"></asp:RangeValidator>--%>
                </div>
            </div>

            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="Effective To"></asp:Label>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="MREVEffeTo" runat="server" ControlToValidate="MtxtEffeTo" Display="Dynamic" ErrorMessage="Enter Valid Effective To Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="MtxtEffeTo" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <cc1:CalendarExtender ID="MtxtEffeTo_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="MtxtEffeTo" TargetControlID="MtxtEffeTo" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                    <%--<asp:RangeValidator ID="rgvMtxtEffeTo" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="MtxtEffeTo" SetFocusOnError="True"></asp:RangeValidator>--%>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="* Retail"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtRetail" runat="server" ControlToValidate="txtRetail" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Retail Rate" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRetail" runat="server" ControlToValidate="txtRetail" Display="Dynamic" ErrorMessage="Enter Valid Retail Rate" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtRetail" autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="* Effective From"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator8" runat="server" ControlToValidate="RtxtEffeFrom" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Effective From Date" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RREVEffeFrom" runat="server" ControlToValidate="RtxtEffeFrom" Display="Dynamic" ErrorMessage="Enter Valid Effective From Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="RtxtEffeFrom" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <cc1:CalendarExtender ID="RtxtEffeFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="RtxtEffeFrom" TargetControlID="RtxtEffeFrom" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                    <%--<asp:RangeValidator ID="rgvRtxtEffeFrom" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="RtxtEffeFrom" SetFocusOnError="True"></asp:RangeValidator>--%>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="Effective To"></asp:Label>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RREVEffeTo" runat="server" ControlToValidate="RtxtEffeTo" Display="Dynamic" ErrorMessage="Enter Valid Effective To Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="RtxtEffeTo" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <cc1:CalendarExtender ID="RtxtEffeTo_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="RtxtEffeTo" TargetControlID="RtxtEffeTo" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                    <%--<asp:RangeValidator ID="rgvRtxtEffeTo" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="RtxtEffeTo" SetFocusOnError="True"></asp:RangeValidator>--%>
                </div>
            </div>

        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="Pre-Determined Purchase Price"></asp:Label>
                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtpreDeterminePrice" runat="server" ControlToValidate="txtpreDeterminePrice" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Pre-Determined Purchase Price" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVpreDeterminePrice" runat="server" ControlToValidate="txtpreDeterminePrice" Display="Dynamic" ErrorMessage="Enter Valid Pre-Determined Purchase Price" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtpreDeterminePrice" autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="Effective From"></asp:Label>
                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator9" runat="server" ControlToValidate="PtxtEffeFrom" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Effective From Date" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="PREVEffeFrom" runat="server" ControlToValidate="PtxtEffeFrom" Display="Dynamic" ErrorMessage="Enter Valid Effective From Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="PtxtEffeFrom" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <cc1:CalendarExtender ID="PtxtEffeFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="PtxtEffeFrom" TargetControlID="PtxtEffeFrom" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                    <%--<asp:RangeValidator ID="rgvPtxtEffeFrom" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="PtxtEffeFrom" SetFocusOnError="True"></asp:RangeValidator>--%>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="Effective To"></asp:Label>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="PREVEffeTo" runat="server" ControlToValidate="PtxtEffeTo" Display="Dynamic" ErrorMessage="Enter Valid Effective To Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="PtxtEffeTo" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    <cc1:CalendarExtender ID="PtxtEffeTo_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="PtxtEffeTo" TargetControlID="PtxtEffeTo" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                    <%--<asp:RangeValidator ID="rgvPtxtEffeTo" CssClass="ErrorMsgRight" runat="server" Type="Date" ControlToValidate="PtxtEffeTo" SetFocusOnError="True"></asp:RangeValidator>--%>
                </div>
            </div>
        </div>
    </div>


    <div>
        <asp:Label ID="lblNode" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblExistMRP" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblhistoryID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblTAXHistoryID" runat="server" Visible="False"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12" style="overflow: auto; padding: 0px">
        <asp:GridView ID="dgHistory" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MRP" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblMRP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRP") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Effective From Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblMRPEffeFromDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRPEffeFromDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Effective To Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblMRPEffeToDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRPEffeToDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Retail" HeaderText="Retail Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:TemplateField HeaderText="Effective From Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblRetailEffeFromDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RetailEffeFromDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Effective To Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblRetailEffeToDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RetailEffeToDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PRate" HeaderText="Purchase Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:TemplateField HeaderText="Effective From Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchaseEffeFromDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseEffeFromDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Effective To Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="lblPurchaseEffeToDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseEffeToDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="UnitOfMsrmnt" Visible="false" HeaderText="Unit Of Measurement" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                <asp:BoundField DataField="AlterUnit" Visible="false" HeaderText="Alternative Unit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                <asp:BoundField DataField="NumOfUnits" Visible="false" HeaderText="Alternative Unit (Quantity)" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                <asp:BoundField DataField="Type" Visible="false" HeaderText="Category" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Price Category" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>

                <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" Height="16px" ImageUrl="~/Images/4delete.gif" ToolTip="Delete/Cancel" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="1%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" ImageUrl="~/Images/Edit16.png" CssClass="hvr-bounce-in" CommandName="EditRow" runat="server" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="1%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnTax" ImageUrl="~/Images/Tax.png" CssClass="hvr-bounce-in" CommandName="Tax" runat="server" ToolTip="Tax" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class=" modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Details of Tax</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblTax" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label runat="server" Text="* Select VAT"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlVAT" runat="server" ControlToValidate="ddlVAT" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select VAT" ValidationGroup="ValidateTaxDetails"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlVAT" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Effective From"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVVATtxtEffeFrom" runat="server" ControlToValidate="VATtxtEffeFrom" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Effective From Date" ValidationGroup="ValidateTaxDetails"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVVATtxtEffeFrom" runat="server" ControlToValidate="VATtxtEffeFrom" Display="Dynamic" ErrorMessage="Enter Valid Effective From Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="VATtxtEffeFrom" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                <cc1:CalendarExtender ID="VATtxtEffeFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="VATtxtEffeFrom" TargetControlID="VATtxtEffeFrom" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Effective To"></asp:Label>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVVATtxtEffeTo" runat="server" ControlToValidate="VATtxtEffeTo" Display="Dynamic" ErrorMessage="Enter Valid Effective To Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="VATtxtEffeTo" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                <cc1:CalendarExtender ID="VATtxtEffeTo_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="VATtxtEffeTo" TargetControlID="VATtxtEffeTo" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label runat="server" Text="* Select CST"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCST" runat="server" ControlToValidate="ddlCST" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select CST" ValidationGroup="ValidateTaxDetails"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCST" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>

                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Effective From"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="CSTtxtEffeFrom" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Effective From Date" ValidationGroup="ValidateTaxDetails"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCSTtxtEffeFrom" runat="server" ControlToValidate="CSTtxtEffeFrom" Display="Dynamic" ErrorMessage="Enter Valid Effective From Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="CSTtxtEffeFrom" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                <cc1:CalendarExtender ID="CSTtxtEffeFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="CSTtxtEffeFrom" TargetControlID="CSTtxtEffeFrom" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Effective To"></asp:Label>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCSTtxtEffeTo" runat="server" ControlToValidate="CSTtxtEffeTo" Display="Dynamic" ErrorMessage="Enter Valid Effective To Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="CSTtxtEffeTo" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                <cc1:CalendarExtender ID="CSTtxtEffeTo_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="CSTtxtEffeTo" TargetControlID="CSTtxtEffeTo" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label runat="server" Text="* Select Excise"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlExcise" runat="server" ControlToValidate="ddlExcise" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Excise Duty" ValidationGroup="ValidateTaxDetails"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlExcise" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="* Effective From"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator3" runat="server" ControlToValidate="ExcisetxtEffeFrom" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Effective From Date" ValidationGroup="ValidateTaxDetails"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVExcisetxtEffeFrom" runat="server" ControlToValidate="ExcisetxtEffeFrom" Display="Dynamic" ErrorMessage="Enter Valid Effective From Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="ExcisetxtEffeFrom" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                <cc1:CalendarExtender ID="ExcisetxtEffeFrom_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="ExcisetxtEffeFrom" TargetControlID="ExcisetxtEffeFrom" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Effective To"></asp:Label>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVExcisetxtEffeTo" runat="server" ControlToValidate="ExcisetxtEffeTo" Display="Dynamic" ErrorMessage="Enter Valid Effective To Date" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="ExcisetxtEffeTo" autocomplete="off" runat="server" CssClass="aspxcontrols" placeholder="dd/mm/yyyy" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                                <cc1:CalendarExtender ID="ExcisetxtEffeTo_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="ExcisetxtEffeTo" TargetControlID="ExcisetxtEffeTo" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" runat="server">
                        <asp:GridView ID="GVDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                            <Columns>
                                <asp:TemplateField HeaderText="INVHID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblINVHID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "INVHID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblVat" runat="server" CommandName="VAT" Text='<%# DataBinder.Eval(Container.DataItem, "VAT") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective From Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVATEffeFromDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VATEffeFromDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective To Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVATEffeToDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VATEffeToDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CST" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCST" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CST") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective From Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTEffeFromDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CSTEffeFromDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective To Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCSTEffeToDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CSTEffeToDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Excise" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExcise" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Excise") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective From Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExciseEffeFromDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExciseEffeFromDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Effective To Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExciseEffeToDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExciseEffeToDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="1%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" Height="16px" ImageUrl="~/Images/4delete.gif" ToolTip="Delete/Cancel" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnAddTaxDetails" Text="Add" title="Add" runat="server" CssClass="btn-ok" ValidationGroup="ValidateTaxDetails" />
                    <asp:Button ID="btnUpdateTaxDetails" Text="Update" title="Update" runat="server" CssClass="btn-ok" ValidationGroup="ValidateTaxDetails" />
                    <asp:Label ID="lblTaxationID" runat="server" Visible="false"></asp:Label>
                </div>

            </div>
        </div>
    </div>

    <div id="ModalFASCompanyValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCustomerValidationMsg" runat="server"></asp:Label>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>

    <asp:TextBox ID="txtStartDate" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtEndDate" runat="server" Visible="false"></asp:TextBox>

</asp:Content>
