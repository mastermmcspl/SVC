<%@ Page Language="VB" MasterPageFile="~/VehicleMaintanance.master" AutoEventWireup="false" CodeFile="LgstComplianceDashboard.aspx.vb" Inherits="VehicleMaintanance_ComplianceDashboard" ValidateRequest="false" %>

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
            margin-top: 0px;
        }
         div{
            color:black;
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
              $('#<%=GvTyreMaster.ClientID%>').DataTable({
                  iDisplayLength: 20,
                  aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                  order: [],
                  columnDefs: [{ orderable: false, targets: [0] }],
              });
          });
               $(document).ready(function () {
              $('[data-toggle="tooltip"]').tooltip();
              $('#<%=gvCompliance.ClientID%>').DataTable({
                  iDisplayLength: 20,
                  aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                  order: [],
                  columnDefs: [{ orderable: false, targets: [0] }],
              });
               });
                    $(document).ready(function () {
              $('[data-toggle="tooltip"]').tooltip();
              $('#<%=gvBatteryComp.ClientID%>').DataTable({
                  iDisplayLength: 20,
                  aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                  order: [],
                  columnDefs: [{ orderable: false, targets: [0] }],
              });
          });
          $(window).load(function () {
              $(".loader").fadeOut("slow");
          })
      </script>
       <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Compliance Details</b></h2>
            </div>

            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" Visible ="false"  />
                    <%--<asp:ImageButton ID="imgbtnReport" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Report" />   --%>
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" visible ="false"  /></span></a>
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
      <div class="col-sm-12 col-md-12" style="padding-left: 0px">
           <div class="col-sm-4 col-md-4 divmargin" style="padding-left: 0px">
        <asp:Label runat="server" Text="Compliance Type"></asp:Label>
            <asp:DropDownList ID="ddlCompliancetype" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4 divmargin" style="padding-left: 0px">
            <asp:Label ID="lblStatus" runat="server" Text="Status" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="275px" Visible="false">
            </asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2 divmargin" style="padding-right: 0px">
            <asp:Label ID="Label2" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
       <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblBattery" runat="server" class="legendbold" Text ="Battery Compliance" Visible="false"></asp:Label>
        </fieldset>
    </div>
         <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px" >
        <asp:GridView ID="gvBatteryComp" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>

                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LVAM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                   <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="BatteryNo" HeaderText="Battery No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                   <asp:BoundField DataField="PurchaseDt" HeaderText="Purchase Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Frequency" HeaderText="Frequency in(Year's)" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:TemplateField HeaderStyle-Width="5%">
                <ItemTemplate>
                <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                </ItemTemplate>
                </asp:TemplateField>
                         </Columns>
        </asp:GridView>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblTyre" runat="server" class="legendbold" Text ="Tyre Compliance" Visible="false"></asp:Label>
        </fieldset>
    </div>
     <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="GvTyreMaster" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" visible="false">
            <Columns>

                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LVTM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                   <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="TyreSLNo" HeaderText="Tyre No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                  <asp:BoundField DataField="MtrReading" HeaderText="Meter Reading" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
               <asp:BoundField DataField="TyreFreq" HeaderText="Frequency" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:TemplateField HeaderStyle-Width="5%">
                <ItemTemplate>
                <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                </ItemTemplate>
                </asp:TemplateField>   
                    </Columns>
        </asp:GridView>

    </div>
        <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblOther" runat="server" class="legendbold" Text ="Other Compliance" Visible="false"></asp:Label>
        </fieldset>
    </div>
       <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="gvCompliance" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label3" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LVCM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                   <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="ComplianceType" HeaderText="Compliance Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                  <asp:BoundField DataField="MtrReading" HeaderText="Meter Reading/Purchase Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
               <asp:BoundField DataField="Frequency" HeaderText="Frequency in Years/K.M" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:TemplateField HeaderStyle-Width="5%">
                <ItemTemplate>
                <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                </ItemTemplate>
                </asp:TemplateField> 
                    </Columns>
        </asp:GridView>

    </div>
      <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblIns" runat="server" class="legendbold" Text ="Insurance Due" Visible="false"></asp:Label>
        </fieldset>
    </div>
       <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="GvInsurance" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label4" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LVID_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                   <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="PolicyNo" HeaderText="Policy No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                  <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
               <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:TemplateField HeaderStyle-Width="5%">
                <ItemTemplate>
                <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                </ItemTemplate>
                </asp:TemplateField> 
                    </Columns>
        </asp:GridView>

    </div>
      <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblLoan" runat="server" class="legendbold" Text ="Loan Due" Visible="false"></asp:Label>
        </fieldset>
    </div>
       <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="GvLoan" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label5" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LVLM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                   <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="AccNo" HeaderText="Account No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                  <asp:BoundField DataField="InstallmentAmt" HeaderText="Installment Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
               <asp:BoundField DataField="DueDt" HeaderText="Due Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                    <asp:TemplateField HeaderStyle-Width="5%">
                <ItemTemplate>
                <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                </ItemTemplate>
                </asp:TemplateField>  
                   </Columns>
        </asp:GridView>

    </div>
</asp:Content>

