<%@ Page Language="VB" MasterPageFile="~/VehicleMaintanance.master" AutoEventWireup="false" CodeFile="LgstAddtnlMaster.aspx.vb" Inherits="VehicleMaintanance_VehicleAddtnlMaster" ValidateRequest="false" %>

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
          <%--  $('#<%=ddlExistingVehicleNo.ClientID%>').select2();--%>
            $('#<%=ddlVehicleType.ClientID%>').select2();
            });
        function validatePage() {
            //Executes all the validation controls associated with group1 validaiton Group1. 
            var flag = window.Page_ClientValidate('Validate');
            if (flag)
                return flag;

        }
    </script>

    <style>
        div {
            color: black;
        }
    </style>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>Vehicle Additional Master Details</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" Visible="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" Visible="false" />
                    <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;" Visible="false"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0" Visible="false"></asp:Label></span>
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
            <asp:Label runat="server" Text="Existing Additional Vehicle No(Detail)"></asp:Label>
            <asp:DropDownList ID="ddlexistingVehicleNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3"></div>
        <div class="col-sm-3 col-md-3"></div>
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
            <asp:Label runat="server" Text="Vehicle No"></asp:Label>
            <asp:DropDownList ID="ddlVehicleNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
                   <asp:Label runat="server" Text="* Registration No" Visible="false"></asp:Label>
            <asp:TextBox ID="txtRegistrationNo" runat="server" autocomplete="off" CssClass="aspxcontrols" Enabled="false" Visible="false"></asp:TextBox>
        <%--    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Vehicle Registration No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo" Display="Dynamic" ErrorMessage="Enter Valid Registration no." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[A-Z|a-z]{2}\s?[-][0-9]{1,2}\s?[-][A-Z|a-z]{0,3}\s?[-][0-9]{4}$"></asp:RegularExpressionValidator>
             <%--     <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Chassis No"></asp:Label>
            <asp:TextBox ID="txtChassisNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVChassisNo" runat="server" ControlToValidate="txtChassisNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Chassis No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <%--   <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVChassisNo" runat="server" ControlToValidate="txtChassisNo" Display="Dynamic" ErrorMessage="Enter Valid Chassis no." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^([A-z]{2}[A-z0-9]{5,16})$"></asp:RegularExpressionValidator>--%>
        <%--</div>--%>
        <%--        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Engine Code"></asp:Label>
            <asp:TextBox ID="txtEngineNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEngineNo" runat="server" ControlToValidate="txtEngineNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Engine No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <%--    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEngineNo" runat="server" ControlToValidate="txtEngineNo" Display="Dynamic" ErrorMessage="Enter Valid Engine no." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^([A-z]{2}[A-z0-9]{5,16})$"></asp:RegularExpressionValidator>--%>
        <%--   </div>--%>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Vehicle Type"></asp:Label>
            <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Enabled="false"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVVehicleType" runat="server" ControlToValidate="ddlVehicleType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Vehicle Type" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Vehicle Company Name"></asp:Label>
            <asp:TextBox ID="txtOwnerName" runat="server" autocomplete="off" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOwnerName" runat="server" ControlToValidate="txtOwnerName" Display="Dynamic" ErrorMessage="Enter Owner Name" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
    </div>
    <%--  <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text=" Service center Details"></asp:Label>
            <asp:TextBox ID="txtServiceCenter" runat="server" autocomplete="off" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text=" Details"></asp:Label>
            <asp:TextBox ID="txtDetails" runat="server" autocomplete="off" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
        </div>
    </div>--%>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Additional Details</legend>
            </fieldset>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Date of Purchase"></asp:Label>
            <asp:TextBox ID="txtDateofPurchase" runat="server" autocomplete="off" CssClass="aspxcontrols" placeholder="DD/MM/YYYY"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDateofPurchase" runat="server" ControlToValidate="txtDateofPurchase" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Date of Purchase" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDateofPurchase" PopupPosition="BottomLeft" TargetControlID="txtDateofPurchase" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Total Value of Meter"></asp:Label>
            <asp:TextBox ID="txtMtrRdng" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Invoice No"></asp:Label>
            <asp:TextBox ID="txtInvNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Value Of Vehicle"></asp:Label>
            <asp:TextBox ID="txtVehVal" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Dealer"></asp:Label>
            <asp:TextBox ID="txtDealer" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Manufacturer Of Vehicle"></asp:Label>
            <asp:TextBox ID="txtManfcturer" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Depreciation Value Of Vehicle"></asp:Label>
            <asp:TextBox ID="txtDeprctnVal" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Battery Details</legend>
            </fieldset>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Sr No"></asp:Label>
            <asp:TextBox ID="txtBattryNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
            <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Frequency(Time Period)"></asp:Label>
            <asp:TextBox ID="txtBattryFreq" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
           </div>
    <div class="col-sm-12 col-md-12 form-group">
        <asp:TextBox ID="txtTyrelId" runat="server" Visible="false"></asp:TextBox>
             <asp:TextBox ID="txtTyreAddId" runat="server" Visible="false"></asp:TextBox>

    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Tyre Details</legend>
            </fieldset>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Sr No"></asp:Label>
            <asp:TextBox ID="txtTyreNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Milage in K.M."></asp:Label>
            <asp:TextBox ID="txtTyreFreq" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
        <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="imgbtnAddTyre" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Tyres" ValidationGroup="ValidateAdd" />
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <asp:DataGrid ID="dgTyreDet" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="50%" class="footable" Visible="false">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="TyreID" HeaderText="TyreID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TyreNo" HeaderText="TyreNo">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="TyreFreq" HeaderText="Milage">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
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
        <asp:TextBox ID="txtComplianceId" runat="server" Visible="false"></asp:TextBox>
             <asp:TextBox ID="txtComplianceAddId" runat="server" Visible="false"></asp:TextBox>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Compliance Details</legend>
            </fieldset>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Compliance Type"></asp:Label>
            <asp:DropDownList ID="ddlCompliance" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Frequency in KM's"></asp:Label>
            <asp:TextBox ID="txtFreqinKM" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
              <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Frequency in Year's"></asp:Label>
            <asp:TextBox ID="txtFreqinYears" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
        </div>
              <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="imgbtnCompliances" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Compliances" ValidationGroup="ValidateAdd" />
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <asp:DataGrid ID="dgCompliance" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="60%" class="footable" Visible="false">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ComplianceName" HeaderText="Compliance Name">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FrequencyInKm" HeaderText="Frequency In Km">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                      <asp:BoundColumn DataField="FrequencyInYears" HeaderText="Frequency In Years">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                       <asp:BoundColumn DataField="ComplianceID" HeaderText="Compliance ID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                            <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Edit" CommandName="Edit" data-placement="bottom" ImageUrl="~\Images\Edit16.png" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" ImageUrl="~\Images\4delete.gif" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
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
                                <asp:Label ID="lblVehicleValidationMsg" runat="server"></asp:Label>
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

</asp:Content>



