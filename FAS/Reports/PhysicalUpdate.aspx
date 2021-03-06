<%@ Page Title="" Language="VB" MasterPageFile="~/Inventory.master" AutoEventWireup="false" CodeFile="PhysicalUpdate.aspx.vb" Inherits="Reports_PhysicalReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }

        div {
            color: black;
        }
    </style>


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
            $('#<%=dgReport.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCmdty.ClientID%>').select2();
        });
    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>3 Initial Stock Update</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" Visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label runat="server" Text="Brand"></asp:Label>
        <asp:DropDownList runat="server" AutoPostBack="true" CssClass="aspxcontrols" ID="ddlCmdty"></asp:DropDownList>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:GridView ID="dgReport" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField>
                    <%--  <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                        </HeaderTemplate>--%>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sl No" HeaderStyle-Width="7%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblSlNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Sl No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="lblStartDate" HeaderStyle-Width="10%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="MRP" HeaderStyle-Width="7%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblMRP" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "MRP") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Commodity" HeaderStyle-Width="18%" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Commodity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDescription" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Code") %>'></asp:Label>
                        <%--<asp:LinkButton ID="lnkCode" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Code") %>'></asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="3" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk3" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "3") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="4" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk4" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "4") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="5" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk5" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "5") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="6" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk6" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "6") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="7" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk7" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "7") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="8" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk8" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "8") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="9" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk9" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "9") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="10" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk10" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "10") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="11" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk11" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "11") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:Label>
                        <%-- <asp:LinkButton ID="lnkTotal" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Update" Visible="true">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTotal" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Update" Visible="true">
                    <ItemTemplate>
                        <asp:Button ID="btnUpdate" runat="server" CommandName="Update1" Text="Update"></asp:Button>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SL_ID" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblslID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SL_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sale Qty" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="txtslQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SL_slQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

