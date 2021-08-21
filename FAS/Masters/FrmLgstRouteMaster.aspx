<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="FrmLgstRouteMaster.aspx.vb" Inherits="Masters_FrmLgstRouteMaster" %>

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
            $('#<%=ddlExistingRouteNo.ClientID%>').select2();
              $('#<%=ddlDestnPlace.ClientID%>').select2();
              $('#<%=ddlDiesel.ClientID%>').select2();
              $('#<%=ddlStartPlace.ClientID%>').select2();
               $('#<%=ddlVehicleType.ClientID%>').select2();
            });

    </script>

    <style>
        div {
            color: black;
        }
    </style>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>Route Master Details</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                   <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" Visible="false" />
                     <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
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
            <asp:Label runat="server" Text="Existing Route Master"></asp:Label>
            <asp:DropDownList ID="ddlExistingRouteNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3"></div>
        <div class="col-sm-3 col-md-3"></div>
        <div class="col-sm-3 col-md-3">
            <br />
            <div class="form-group" runat="server">
                <asp:Label runat="server" Text="Status :-" Visible="false"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Vehicle Type"></asp:Label>
            <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Start Place"></asp:Label>
            <asp:DropDownList ID="ddlStartPlace" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Destination Place"></asp:Label>
            <asp:DropDownList ID="ddlDestnPlace" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>

    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Distance in Kms."></asp:Label>
            <asp:TextBox ID="txtDistance" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDistance" runat="server" ControlToValidate="txtDistance" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Distance" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Rate in Rs."></asp:Label>
            <asp:TextBox ID="txtRate" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRate" runat="server" ControlToValidate="txtRate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Destination Place" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Driver Allowance in Amt"></asp:Label>
            <asp:TextBox ID="TxtDrvrAlwnceAmt" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDrvrAlwnceAmt" runat="server" ControlToValidate="TxtDrvrAlwnceAmt" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Driver Allowance Amount" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Petrol in Ltr"></asp:Label>
            <asp:TextBox ID="txtPtrlInLtr" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPtrlInLtr" runat="server" ControlToValidate="txtPtrlInLtr" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Petrol in Ltr" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Time Alloted for Trip in Hrs"></asp:Label>
            <asp:TextBox ID="txtTimeAlltFrTrip" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTimeAlltFrTrip" runat="server" ControlToValidate="txtTimeAlltFrTrip" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Time Allot for trip" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTimeAlltFrTrip" runat="server" ControlToValidate="txtTimeAlltFrTrip" Display="Dynamic" ErrorMessage="Enter Only Integer." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[0-9\s]{0,9}$"></asp:RegularExpressionValidator>
        </div>
    </div>
     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
       
        
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Diesel/Petrol Pump Details</legend>
                        </fieldset>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label runat="server" Text="* Diesel/Petrol Pump Name"></asp:Label>
                            <asp:DropDownList ID="ddlDiesel" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDiesel" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="imgbtnRoutePumpSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Charges" ValidationGroup="ValidateAdd" />
        </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group">
        <asp:DataGrid ID="dgPumpDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="40%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top"  />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="PumpName" HeaderText="Pump Name">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="30%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn Visible="false">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Edit" CommandName="Edit" data-placement="bottom" ImageUrl="~\Images\Edit16.png" runat="server" visible="false" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" ImageUrl="~\Images\4delete.gif" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Date" Visible="false"></asp:Label>
            <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtDate" Visible="false" AutoPostBack="true" ValidateRequestMode="Disabled" placeholder="DD/MM/YY"></asp:TextBox>
            <cc1:CalendarExtender ID="cclDate" runat="server" PopupButtonID="txtDate" PopupPosition="BottomLeft"
                TargetControlID="txtDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>

            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOdate" runat="server" ControlToValidate="txtDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="Login" Visible="false"></asp:Label>
            <asp:TextBox ID="txtLogin" Visible="false" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLogin" Visible="false" runat="server" ControlToValidate="txtLogin" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Login" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>

    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <asp:TextBox ID ="txtPumpID" runat ="server" Visible ="false" ></asp:TextBox>
    </div>
    <div id="ModalFASVehicleValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblRouteValidationMsg" runat="server"></asp:Label>
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
        <asp:TextBox ID ="txtGLID" runat ="server" Visible ="false" ></asp:TextBox>
    </div>
</asp:Content>

