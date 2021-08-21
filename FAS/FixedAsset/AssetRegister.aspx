<%@ Page Title="" Language="VB" MasterPageFile="~/FixedAsset.master" AutoEventWireup="false" CodeFile="AssetRegister.aspx.vb" Inherits="FixedAsset_AssetRegister" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
            div{
            color:black;
              }           
    
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

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
             $('#<%=dgRegister.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });       

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
   <div class="loader"></div>
   <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Asset Master Dashboard</b></h2>
            </div>
          
            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png"  runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" Visible="false" />
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" Visible="false"/>                   
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
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">        
       <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>

    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="AssetType"></asp:Label>
            <asp:DropDownList ID="ddlpAstype" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>

    </div>
 
     <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="dgRegister" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>              
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                        <asp:Label ID="lblAssetID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssetID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AssetCode" HeaderText="AssetCode" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="8%"></asp:BoundField>
                <asp:BoundField DataField="AssetDescription" HeaderText="Asset Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="18%"></asp:BoundField>
                <asp:BoundField DataField="PurchaseDate" HeaderText="Purchase Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="PurchaseAmount" HeaderText="Purchase Amt" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="06%"></asp:BoundField>
                <asp:BoundField DataField="Datecommission" HeaderText="Date Put to Use" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                 <asp:BoundField DataField="AssetAge" HeaderText="Asset age" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="01%"></asp:BoundField>               
                  <asp:BoundField DataField="Qty" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="01%"></asp:BoundField>               
                 <asp:BoundField DataField="DepMethod" HeaderText="Depreciation method" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="11%"></asp:BoundField>
                <asp:BoundField DataField="DepRate" HeaderText="Depreciation Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="CurrentStatus" HeaderText="CurrentStatus" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%"></asp:BoundField>                            
          <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="EditFREG" runat="server" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
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
</asp:Content>

