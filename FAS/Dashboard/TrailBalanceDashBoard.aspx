<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="TrailBalanceDashBoard.aspx.vb" Inherits="Dashboard_TrailBalanceDashBoard" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
             $('#<%=dgGLDashBoard.ClientID%>').DataTable({
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
            <div class="col-sm-12 col-md-12 pull-left">
                <h2><b>Trail Balance - Dash Board</b></h2>
            </div>                     
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:Label ID ="lblError" CssClass ="label" runat ="server" ></asp:Label>
    </div>

   <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="dgGLDashBoard" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField Visible ="false" >
                    <ItemTemplate >
                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="GLCode" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%" >
                    <ItemTemplate >
                        <asp:Label ID="lblGLCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="GLCode" HeaderText="GL Code" HeaderStyle-Width="8%"></asp:BoundField>--%>
                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%">
                    <ItemTemplate >
                        <asp:LinkButton ID ="lnkDescription" runat ="server" CommandName ="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Right"></asp:BoundField>  --%>             
                <asp:BoundField DataField="OpDebit" HeaderText="OB Debit" HeaderStyle-Width="10%"></asp:BoundField>               
                <asp:BoundField DataField="OpCredit" HeaderText="OB Credit" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="TrDebit" HeaderText="Trans Debit" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="TrCredit" HeaderText="Trans Credit" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="ClDebit" HeaderText="Closing Debit" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="ClCredit" HeaderText="Closing Credit" HeaderStyle-Width="12%"></asp:BoundField>                
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalPaymentValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblPaymentMasterValidationMsg" runat="server"></asp:Label></strong>
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

    <div class=" modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">GL Details</h4>
                </div>
                 <div class="modal-body row">
                <div class="col-sm-12 col-md-12" runat="server">
                    <asp:GridView ID="GVDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GLCode" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGLCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDescription" CommandName ="Select" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OBDebit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblOBDebit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OBDebit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OBCredit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblOBCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OBCredit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="TrDebit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrDebit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TrDebit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TrCredit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TrCredit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="ClDebit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblClDebit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClDebit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ClCredit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblClCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ClCredit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
            </div>
        </div>
    </div>

    <div class=" modal fade" id="myModalT" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">GL Transactions</h4>
                </div>
                 <div class="modal-body row">
                <div class="col-sm-12 col-md-12" runat="server">
                    <asp:GridView ID="GVTransactions" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="GLCode" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%">
                                <ItemTemplate>
                                    <asp:Label ID="lblGLCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GLCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDescription" CommandName ="Select" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="SubGLCode" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubGLCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubGLCode") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="SubGLDescription" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubGLDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SubGLDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="TrDebit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrDebit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TrDebit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TrCredit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrCredit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TrCredit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                                       
                        </Columns>
                    </asp:GridView>
                </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

