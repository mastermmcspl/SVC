<%@ Page Title="" Language="VB" MasterPageFile="~/CustomerMaster/Home.master" AutoEventWireup="false" CodeFile="CustomerMaster.aspx.vb" Inherits="CustomerMaster_CustomerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <link rel="stylesheet" href="StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />


    <script src="JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>s
    <script src="JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="JavaScripts/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=gvRequestDetails.ClientID%>').DataTable({
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
                <h2><b>Customer Master</b> </h2>
            </div>
            <div class="col-sm-9 col-md-9">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New Details" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-6 col-md-6">
        <div class="divmargin">
            <asp:Label ID="lblProductInterest" runat="server" Text="* Search by"></asp:Label>
            <asp:DropDownList ID="ddlPDProductInterest" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                <asp:ListItem Value="0">Select Product</asp:ListItem>
                <asp:ListItem Value="1">TRACe PA</asp:ListItem>
                <asp:ListItem Value="2">TRACe Enterprise</asp:ListItem>
                <asp:ListItem Value="3">EDICT</asp:ListItem>
                <asp:ListItem Value="4">FAS Pro</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12">
        <asp:GridView ID="gvRequestDetails" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="footable">
            <Columns>
                <asp:TemplateField HeaderText="Sl.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:Label ID="lblSlNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SlNo") %>'></asp:Label>
                        <asp:Label ID="lblIDPKID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.PKID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="RegNo" HeaderText="Reg no." SortExpression="CD_Regno" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="CD_CompanyName" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="CompanyWebsite" HeaderText="Website" SortExpression="CD_CompanyWebsite" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="12%" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="EmailID" HeaderText="Email ID" SortExpression="CD_EmailID" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="TelephoneNo" HeaderText="Telephone no" SortExpression="CD_Telephoneno" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="MobileNumber" HeaderText="Mobile No" SortExpression="CD_MobileNumber" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="ContactPerson" HeaderText="Contact person" SortExpression="CD_ContactPerson" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="ProductInterest" HeaderText="Product" SortExpression="CD_ProductInterest" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="Reason" HeaderText="Reason" SortExpression="CD_Reason" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />

                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnView" CssClass="hvr-bounce-in" CommandName="View" runat="server" ToolTip="View" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

