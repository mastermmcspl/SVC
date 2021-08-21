<%@ Page Title="" Language="VB" MasterPageFile="~/CustomerMaster/Home.master" AutoEventWireup="false" CodeFile="ServerDetails.aspx.vb" Inherits="CustomerMaster_ServerDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  <link rel="stylesheet" href="StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />
   

    <script src="JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>


    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=gvServerDetails.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });

    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
             <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>Server Master</b> </h2>
          </div>
            <div class="col-sm-9 col-md-9">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create New Database" />
                </div>
            </div>
        </div>
    </div>

    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12">
        <asp:GridView ID="gvServerDetails" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="footable">
            <Columns>
                <asp:TemplateField HeaderText="Sl.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:Label ID="lblSlNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SlNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SD_DatabaseName" HeaderText="Database Name" SortExpression="SD_DatabaseName" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="SD_CompanyName" HeaderText="Company Name" SortExpression="SD_CompanyName" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:TemplateField HeaderText="Access Code" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%">
                    <ItemTemplate>
                        <asp:LinkButton ID="SD_AccessCode" CommandName ="FAS" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SD_AccessCode") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SD_CreatedOn" HeaderText="Created On" SortExpression="SD_CreatedOn" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

