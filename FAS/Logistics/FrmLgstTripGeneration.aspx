<%@ Page Title="" Language="VB" MasterPageFile="~/LogisticsMaster.master" AutoEventWireup="false" CodeFile="FrmLgstTripGeneration.aspx.vb" Inherits="Logistics_FrmLgstTRDashboard" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            $('#<%=ddlRoute.ClientID%>').select2();
            $('#<%=ddlDriver.ClientID%>').select2();
            $('#<%=ddlDestinCustomer.ClientID%>').select2();
            $('#<%=ddlDieselPump.ClientID%>').select2();
            $('#<%=ddlExistingTripDetails.ClientID%>').select2();
            $('#<%=ddlStartCustomer.ClientID%>').select2();
            $('#<%=ddlVehicleNo.ClientID%>').select2();
            $('#<%=ddlVehicleType.ClientID%>').select2();
        });

        function Validate() {
            if (document.getElementById('<%=ddlRoute.ClientID %>').value == "") {
                alert('Select Route.');
                document.getElementById('<%=ddlRoute.ClientID%>').focus()
                return false
            }
        }
    </script>

    <style>
        div {
            color: black;
        }
    </style>
  <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>Trip Generation</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" CausesValidation="false" title="Approve" ValidationGroup="Validate" Visible="false" />
                    <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Trip Details"></asp:Label>
            <asp:DropDownList ID="ddlExistingTripDetails" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
             <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Transaction No"></asp:Label>
            <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="" Visible="false"></asp:Label>
            <asp:Label ID="lblOwner" runat="server" Text="" Visible="false"></asp:Label>
        </div>
        <div class="col-sm-3 col-md-3">
            <br />
            <div class="form-group" runat="server">
                <asp:Label runat="server" Text="Status :-"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Route"></asp:Label>
            <asp:DropDownList ID="ddlRoute" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Vehicle Type"></asp:Label>
            <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Enabled="false"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVVehicleType" runat="server" ControlToValidate="ddlVehicleType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Truck Type" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Distance in KM's"></asp:Label>
            <asp:TextBox ID="TxtDstanceKM" runat="server" autocomplete="off" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDstanceKM" runat="server" ControlToValidate="TxtDstanceKM" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Distance" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Rate" Width="120px"></asp:Label>
            <asp:Label runat="server" Text="* Total Diesel in Ltrs" Width="120px"></asp:Label>
            <asp:TextBox ID="txtRate" runat="server" autocomplete="off" CssClass="aspxcontrols" Width="120px" Enabled="false"></asp:TextBox>
            <asp:TextBox ID="txtPtrlinLtrs" runat="server" autocomplete="off" CssClass="aspxcontrols" Width="120px" Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRate" runat="server" ControlToValidate="txtRate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Rate" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPtrl" runat="server" ControlToValidate="txtPtrlinLtrs" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Petrol in Ltrs" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Start City"></asp:Label>
            <asp:TextBox ID="txtStartCity" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStartCity" runat="server" ControlToValidate="txtStartCity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Start City" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Destination City"></asp:Label>
            <asp:TextBox ID="txtDestinCity" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDestinCity" runat="server" ControlToValidate="txtDestinCity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Destination City" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Vehicle No"></asp:Label>
            <asp:DropDownList ID="ddlVehicleNo" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Trip Status"></asp:Label>
            <asp:DropDownList ID="ddlTripStatus" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                <asp:ListItem Value="0">Select Trip Status</asp:ListItem>
                <asp:ListItem Value="1">Start</asp:ListItem>
                <asp:ListItem Value="2">End</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Start Customer"></asp:Label>
            <asp:DropDownList ID="ddlStartCustomer" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Service Given to Customer"></asp:Label>
            <asp:DropDownList ID="ddlDestinCustomer" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* SVC(TS) No."></asp:Label>
            <asp:TextBox ID="txtSVCNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSVCNo" runat="server" ControlToValidate="txtSVCNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter SVC(TS) No." ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Client Ref No."></asp:Label>
            <asp:TextBox ID="txtCRN" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCRN" runat="server" ControlToValidate="txtCRN" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Client Ref No." ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text=" Remarks"></asp:Label>
            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Height="50px" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Start Date And Time"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" Width="100px" AutoPostBack="true"></asp:TextBox>
            <asp:TextBox ID="txtStartTime" runat="server" autocomplete="off" TextMode="Time" CssClass="aspxcontrols" Width="100px" type="text"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStartTime" runat="server" ControlToValidate="txtStartTime" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Start Time" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
            <cc1:CalendarExtender ID="ccStartDate" runat="server" PopupButtonID="txtStartDate" PopupPosition="BottomLeft" TargetControlID="txtStartDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter Start Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Stop Date And Time"></asp:Label>
            <asp:TextBox ID="txtStopDate" runat="server" CssClass="aspxcontrols" autocomplete="off" placeholder="dd/MM/yyyy" Width="100px" AutoPostBack="true"></asp:TextBox>
            <asp:TextBox ID="txtStopTime" runat="server" autocomplete="off" TextMode="Time" AutoPostBack="true" CssClass="aspxcontrols" Width="100px"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStopTime" runat="server" ControlToValidate="txtStopTime" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Stop Time" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
            <cc1:CalendarExtender ID="ccStopDate" runat="server" PopupButtonID="txtStopDate" PopupPosition="BottomLeft" TargetControlID="txtStopDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStopDate" runat="server" ControlToValidate="txtStopDate" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter Start Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStopDate" runat="server" ControlToValidate="txtStopDate" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="E-Way Bill No."></asp:Label>
            <asp:TextBox ID="txtEwayBillNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEwayBillNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter E-Way Bill No." ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label8" runat="server" Text="Company Address"></asp:Label>
            <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label5" runat="server" Text="Company GSTN RegNo"></asp:Label>
            <asp:TextBox ID="txtCompanyGSTN" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label6" runat="server" Text="Service Customer Address"></asp:Label>
            <asp:TextBox ID="txtCustomerAddress" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label7" runat="server" Text="Service Customer GSTN RegNo"></asp:Label>
            <asp:TextBox ID="txtCustGSTN" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>

    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label10" runat="server" Text="GST Rate Category"></asp:Label>
            <asp:TextBox ID="txtCustGstnCategory" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
            <asp:TextBox ID="txtCustGstnCategoryId" runat="server" CssClass="aspxcontrols" autocomplete="off" Enabled="false" AutoPostBack="true" Visible="false"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label9" runat="server" Text="GST Rate" Visible="false"></asp:Label>
            <asp:TextBox ID="txtCustGSTRate" runat="server" CssClass="aspxcontrols" Visible="false" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblTotal" runat="server" Text="Total Amount" Visible="false"></asp:Label>
            <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="aspxcontrols" Visible="false" autocomplete="off" Enabled="false" AutoPostBack="true"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Time Allotted for trip."></asp:Label>
            <asp:TextBox ID="txtTimeAlltdFrTrip" Enabled="false" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Time Taken by trip."></asp:Label>
            <asp:TextBox ID="txtTimeTknTrip" runat="server" autocomplete="off" TextMode="Number" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-0 col-md-0">
            <asp:Label runat="server" ID="lbltripTimeStatus" Text="" Visible="false"></asp:Label>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Meter Reading Starting Point."></asp:Label>
            <asp:TextBox ID="txtMRStart" runat="server" autocomplete="off" TextMode="Number" CssClass="aspxcontrols" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Meter Reading Ending Point."></asp:Label>
            <asp:TextBox ID="txtMREnd" runat="server" autocomplete="off" TextMode="Number" CssClass="aspxcontrols" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-0 col-md-0">
            <asp:Label runat="server" ID="lblMeterStatus" Text="" Visible="false"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Driver Details</legend>
            </fieldset>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Select Driver"></asp:Label>
                <asp:DropDownList ID="ddlDriver" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Amount for driver"></asp:Label>
                <asp:TextBox ID="txtelgblAmt" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVelgblAmt" runat="server" ControlToValidate="txtelgblAmt" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Eligible amount of driver" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Diesel/Petrol Pump Details</legend>
            </fieldset>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Petrol Pump Name"></asp:Label>
            <asp:DropDownList ID="ddlDieselPump" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Date"></asp:Label>
            <asp:TextBox ID="txtDieselDt" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtDieselDt" PopupPosition="BottomLeft" TargetControlID="txtDieselDt" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* No of liters"></asp:Label>
            <asp:TextBox ID="txtDieselinLtrs" runat="server" TextMode="Number" autocomplete="off" CssClass="aspxcontrols" OnTextChanged="txtDieselinLtrs_TextChanged" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Rate/Ltr"></asp:Label>
            <asp:TextBox ID="txtRatePrLtr" AutoPostBack="true" runat="server" OnTextChanged="txtRatePrLtr_TextChanged" TextMode="Number" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>

    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="txtDieselAmount" runat="server" autocomplete="off" TextMode="Number" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Advance Paid to driver"></asp:Label>
            <asp:TextBox ID="txtAdvncsPaidDriver" runat="server" autocomplete="off" CssClass="aspxcontrols" AutoPostBack="true" OnTextChanged="txtAdvncsPaidDriver_TextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAdvncsPaidDriver" runat="server" ControlToValidate="txtAdvncsPaidDriver" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Advance Amount" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text=" Other Expenses (if any)"></asp:Label>
            <asp:TextBox ID="txtOtherExp" runat="server" autocomplete="off" CssClass="aspxcontrols" AutoPostBack="true"></asp:TextBox>
            <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOtherExp" runat="server" ControlToValidate="txtAdvncsPaidDriver" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Advance Amount" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>--%>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Other Expenses Remarks"></asp:Label>
            <asp:TextBox ID="txtOtherRemarks" runat="server" autocomplete="off" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Add Blue Oil In Liters"></asp:Label>
            <asp:TextBox ID="txtOilinLtr" runat="server" autocomplete="off" CssClass="aspxcontrols" AutoPostBack="true"></asp:TextBox>
            <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOtherExp" runat="server" ControlToValidate="txtAdvncsPaidDriver" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Advance Amount" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text=" Oil  Rate/Liters"></asp:Label>
            <asp:TextBox ID="txtOilRate" runat="server" autocomplete="off" CssClass="aspxcontrols" AutoPostBack="true" OnTextChanged="txtOilRate_TextChanged"></asp:TextBox>
            <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOtherExp" runat="server" ControlToValidate="txtAdvncsPaidDriver" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Advance Amount" ValidationGroup="ValidateTrip"></asp:RequiredFieldValidator>--%>
        </div>
        <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="ImgbtnDieselSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group">
        <asp:DataGrid ID="dgDiesel" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable" Visible="false">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="0%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SrNo" HeaderText="Sr No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="2%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DieselName" HeaderText="Pump Name">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Date" HeaderText="Date">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="6%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DieselinLtrs" HeaderText="Diesel in Ltrs">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="RateperLtrs" HeaderText="Rate per Ltrs">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="right" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="right" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AdvancePaidToDriver" HeaderText="Advance Paid To Driver">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="right" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OtherExp" HeaderText="Other Expenses">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="right" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OtherRemarks" HeaderText="Remarks">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OilInLtr" HeaderText="Oil In Ltr">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OilRateLtr" HeaderText="Oil Rate Per Ltr">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="8%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>
                         <asp:BoundColumn DataField="PumpID" HeaderText="Pump ID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                         <asp:BoundColumn DataField="CreatedBy" HeaderText="Create By">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Edit" CommandName="Edit" data-placement="bottom" ImageUrl="~\Images\Edit16.png" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnView" CssClass="dropdown-toggle hvr-bounce-out" data-toggle="tooltip" title="View" CommandName="Report" ImageUrl="~/Images/View16.png" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" ImageUrl="~\Images\4delete.gif" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <asp:TextBox ID="txtDieselId" runat="server" Visible="false"></asp:TextBox>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Hop-Ons Details</legend>
            </fieldset>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select In Date And Time" Width="100%"></asp:Label>
            <asp:TextBox ID="txtInDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" Width="100px"></asp:TextBox>
            <asp:TextBox ID="txtInTime" runat="server" autocomplete="off" TextMode="Time" CssClass="aspxcontrols" Width="100px"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtInDate" PopupPosition="BottomLeft" TargetControlID="txtInDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Select Out Date And Time"></asp:Label>
            <asp:TextBox ID="txtOutDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" Width="100px"></asp:TextBox>
            <asp:TextBox ID="txtOutTime" runat="server" autocomplete="off" TextMode="Time" CssClass="aspxcontrols" Width="100px"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtOutDate" PopupPosition="BottomLeft" TargetControlID="txtOutDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="Hop-on Remarks"></asp:Label>
            <asp:TextBox ID="txtHopOnRemark" runat="server" autocomplete="off" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
        </div>
        <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="imgbtnAddHopOn" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group">
        <asp:DataGrid ID="dgHopOn" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="50%" class="footable" Visible="false">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="HoponID" HeaderText="HopOnID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="InDate" HeaderText="InDate">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="InTime" HeaderText="InTime">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OutDate" HeaderText="OutDate">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OutTime" HeaderText="OutTime">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Remarks" HeaderText="Remarks">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="40%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Edit" CommandName="Edit" data-placement="bottom" ImageUrl="~\Images\Edit16.png" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" ImageUrl="~\Images\4delete.gif" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <asp:TextBox ID="txtHopOnID" runat="server" Visible="false"></asp:TextBox>
    </div>

    <div class="">
        <asp:TextBox ID="txtBillAmount" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
    </div>

    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgINVDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable" Visible="false">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="PaymentID" HeaderText="PaymentID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="3%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Type" HeaderText="Type">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="11%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="18%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="12%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Balance" HeaderText="Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

            </Columns>
        </asp:DataGrid>
    </div>

    <div id="ModalFASDriverValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblTGValidationMsg" runat="server"></asp:Label>
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

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:TextBox ID="txtGLID" runat="server" Visible="false"></asp:TextBox>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>
</asp:Content>





