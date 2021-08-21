<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="DataCaptureMaster.aspx.vb" Inherits="RemoteData_DataCaptureMaster" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
             $('#<%=dgJE.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });


        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
   <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Remote Data Master</b></h2>
            </div>
          
            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title=""  />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href ="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" visible ="false" /></span></a>
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
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">        
        <div class="col-sm-4 col-md-4 divmargin" style="padding-right: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="dgJE" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>                        
                        <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BatchID") %>'></asp:Label>
                        <asp:Label ID="lblDescName" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BatchNo") %>'></asp:Label>    
                        <asp:Label ID="lblCompanyID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompanyID") %>'></asp:Label>
                        <asp:Label ID="lblCustomerID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerID") %>'></asp:Label>
                        <asp:Label ID="lblTrTypeID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrTypeID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                              
                <asp:BoundField DataField="BatchID" HeaderText="BatchID" Visible="false"></asp:BoundField>
                <asp:BoundField DataField="TransactionNo" HeaderText="Transaction No" HeaderStyle-Width="12%"></asp:BoundField>
                <asp:BoundField DataField="Company" HeaderText="Company" HeaderStyle-Width="13%"></asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="Customer/Supplier" HeaderStyle-Width="13%"></asp:BoundField>
               <%-- <asp:BoundField DataField="TrType" HeaderText="Transaction Type" HeaderStyle-Width="13%"></asp:BoundField>--%>
                 <asp:TemplateField>
                    <ItemTemplate>                        
                        <asp:Label ID="lblTrType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>      
                <%--<asp:BoundField DataField="BatchNo" HeaderText="Batch No" HeaderStyle-Width="10%"></asp:BoundField>--%>
                <asp:TemplateField HeaderText="BatchNo" HeaderStyle-Width="40%">
                    <ItemTemplate>
                        <%--<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.GLDescription") %>' Align="Right"></asp:Label>--%>
                        <asp:LinkButton ID="lnkBatchNo" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BatchNo") %>'></asp:LinkButton>
                        
                     </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BatchNo") %>' Align="Right"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" HeaderStyle-Width="10%"></asp:BoundField>                
                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="20%"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" ImageUrl = "~/Images/Edit16.png" />
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
                    <h4 class="modal-title">Batch Details</h4>
                </div>
                 <div class="modal-body row">
                <div class="col-sm-12 col-md-12" runat="server">
                    <asp:GridView ID="GVDetails" runat="server" AutoGenerateColumns="true"  Width="100%" class="footable">                    
                    </asp:GridView>
                </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

