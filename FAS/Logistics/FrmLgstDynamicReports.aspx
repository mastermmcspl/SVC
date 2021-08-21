<%@ Page Language="VB" MasterPageFile="~/LogisticsMaster.master" AutoEventWireup="false" CodeFile="FrmLgstDynamicReports.aspx.vb" Inherits="Logistics_FrmLgstDynamicReports" ValidateRequest="false" %>

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
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlAvailability.ClientID%>').select2();
            $('#<%=ddlMeter.ClientID%>').select2();
            $('#<%=ddlReportType.ClientID%>').select2();
            $('#<%=ddlTimeStatus.ClientID%>').select2();
            $('#<%=ddlTripNo.ClientID%>').select2();
            $('#<%=ddlTripStatus.ClientID%>').select2();
        });
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Dynamic Report</b></h2>
            </div>

            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" Visible="false" />
                    <%--<asp:ImageButton ID="imgbtnReport" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Report" />   --%>
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" visible="true" /></span></a>
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
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="LabelError"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text=" * Report Type"></asp:Label>
            <asp:DropDownList ID="ddlReportType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        </div>
           <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Panel ID="pnlCustomers" runat="server" Visible="false">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Customers"></asp:Label>
                <asp:DropDownList ID="ddlCustomers" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRoute" runat="server" Visible="false">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Route"></asp:Label>
                <asp:DropDownList ID="ddlRoute" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </asp:Panel>
                    <asp:Panel ID="pnlDriver" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Select Driver"></asp:Label>
            <asp:DropDownList ID="ddlDriver" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
         <asp:Panel ID="pnlVehicleType" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Select Vehicle Type"></asp:Label>
            <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    </div>
    <asp:Panel ID="pnlFromToDt" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label1" runat="server" Text=" * From Date" Visible="true"></asp:Label>
            <asp:TextBox ID="txtFromDt" runat="server" Visible="true" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtFromDt" PopupPosition="BottomLeft" TargetControlID="txtFromDt" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label2" runat="server" Text=" * To Date" Visible="true"></asp:Label>
            <asp:TextBox ID="txtToDt" runat="server" Visible="true" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtToDt" PopupPosition="BottomLeft" TargetControlID="txtToDt" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
        </div>
    </asp:Panel>
        <asp:Panel ID="pnlTripNo" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Trip No"></asp:Label>
            <asp:DropDownList ID="ddlTripNo" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
     <div class="col-sm-12 col-md-12" style="padding-left: 0px">
    <asp:Panel ID="pnlStatus" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Status"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlTimeStatus" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Time Status"></asp:Label>
            <asp:DropDownList ID="ddlTimeStatus" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlTripStatus" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Trip Status"></asp:Label>
            <asp:DropDownList ID="ddlTripStatus" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMeterStatus" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Meter Status"></asp:Label>
            <asp:DropDownList ID="ddlMeter" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlAvailabity" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Availability"></asp:Label>
            <asp:DropDownList ID="ddlAvailability" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
         </div>
    <%--        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblDate" runat="server" Text=" * Date" Visible="false"></asp:Label>
            <asp:TextBox ID="txtDate" runat="server" Visible="false" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
            <cc1:CalendarExtender ID="cctxtDate" runat="server" PopupButtonID="txtDate" PopupPosition="BottomLeft" TargetControlID="txtDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
        </div>--%>


    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblTimeStatus" runat="server" class="legendbold" Text="Trip Time" Visible="false"></asp:Label>
        </fieldset>
    </div>
    <div class="clearfix divmargin"></div>
    <%--  <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="gvTimeStatus" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>

                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LTGM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="TripNo" HeaderText="Trip No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="RegistrationNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="AllottedTime" HeaderText="Allotted Time (in Hours)" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="TakenTime" HeaderText="Taken Time(in Hours)" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblIndent" runat="server" class="legendbold" Text="Indent" Visible="false"></asp:Label>
        </fieldset>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="GvIndent" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>

                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LTGDD_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="TripNo" HeaderText="Trip No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="PumpName" HeaderText="Pump Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="RegistrationNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="IndDate" HeaderText="Indent Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="DieselinLtrs" HeaderText="DieselinLtrs" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="DieselRatePerltr" HeaderText="DieselRatePerltr" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="OilInltr" HeaderText="OilInltr" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="OilAmountInLtr" HeaderText="OilAmountInLtr" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="DieselAmount" HeaderText="DieselAmount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="DriverAdvancGvnByPump" HeaderText="DriverAdvancGvnByPump" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblTrip" runat="server" class="legendbold" Text="Trip Status" Visible="false"></asp:Label>
        </fieldset>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="gvTrip" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label3" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LTGM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="TripNo" HeaderText="Trip No" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="Customer Name" HeaderStyle-Width="30%"></asp:BoundField>
                <asp:BoundField DataField="Route" HeaderText="Route Name" HeaderStyle-Width="20%"></asp:BoundField>
                <asp:BoundField DataField="StartDate" HeaderText="Start Date" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="VechicleNo" HeaderText="Vechicle No." HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="TripStatus" HeaderText="Trip Status" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblIns" runat="server" class="legendbold" Text="Meter Status" Visible="false"></asp:Label>
        </fieldset>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="GvMeterStatus" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label4" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LTGM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="TripNo" HeaderText="Trip No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="DistanceinKms" HeaderText="Trip Distance" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="MRStart" HeaderText="Meter Start" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="MREnd" HeaderText="Meter End" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Route" HeaderText="Route" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton3" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblTrucks" runat="server" class="legendbold" Text="Vehicle Availability" Visible="false"></asp:Label>
        </fieldset>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="GvVehicle" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblgvTruck" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LVM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="VehicleType" HeaderText="Vehicle Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton4" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <asp:Label ID="lblDriver" runat="server" class="legendbold" Text="Driver Availability" Visible="false"></asp:Label>
        </fieldset>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="gvDriver" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label5" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LDM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="DriverName" HeaderText="Driver Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="LicenseNo" HeaderText="License No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="ContactNo" HeaderText="Contact No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton4" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" Visible="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>--%>
        <%--    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:GridView ID="gvCustomerTrip" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" Visible="false">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="Label5" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LTGM_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%"></asp:BoundField>
                <asp:BoundField DataField="TripNo" HeaderText="Trip No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                  <asp:BoundField DataField="Customer" HeaderText="Customer" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Route" HeaderText="Route" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                   <asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="StopDate" HeaderText="Stop Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="TimeStatus" HeaderText="Time Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="MeterStatus" HeaderText="Meter Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="TripStatus" HeaderText="Trip Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>--%>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>
</asp:Content>


