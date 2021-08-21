<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="NormalScan.aspx.vb" Inherits="DigitalFilling_NormalScan" %>

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
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=gvScannedImages.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [3] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>7.Normal Scan Dashboard</b></h2>
            </div>
            <div class="col-sm-9 col-md-9">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="gvScannedImages" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="Folder Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="32%">
                    <ItemTemplate>
                        <asp:Label ID="lblFolderName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FolderName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="TotalPages" HeaderText="Total Pages" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="32%"></asp:BoundField>
                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="32%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnView" CssClass="hvr-bounce-in" CommandName="View" runat="server" ToolTip="View" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
