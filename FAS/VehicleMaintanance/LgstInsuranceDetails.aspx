<%@ Page Language="VB" MasterPageFile="~/VehicleMaintanance.master" AutoEventWireup="false" CodeFile="LgstInsuranceDetails.aspx.vb" Inherits="VehicleMaintanance_LgstInsuranceDetails" ValidateRequest="false" %>

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
               $('#<%=ddlExistingVehicleNo.ClientID%>').select2();
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
                <h2><b>Vehicle Insurance Details</b></h2>
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
            <asp:Label runat="server" Text="Existing Registration No"></asp:Label>
            <asp:DropDownList ID="ddlInsVehicleNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3"></div>
        <div class="col-sm-3 col-md-3"></div>
        <div class="col-sm-3 col-md-3">
            <br />
            <div class="form-group" runat="server">
                <asp:Label runat="server" Text="Status :-" Visible="false"></asp:Label>
                <asp:Label ID="lblStatus" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </div>
    </div>

        <div class="col-sm-12 col-md-12" style="padding: 0px">
                 <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text=" Registration No"></asp:Label>
            <asp:DropDownList ID="ddlExistingVehicleNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Type of Insurance"></asp:Label>
                <asp:DropDownList ID="ddlInsuranceType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                     <asp:ListItem Value="0">Select Insurance Type</asp:ListItem>
                        <asp:ListItem Value="1">1st Party</asp:ListItem>
                        <asp:ListItem Value="2">2nd Party</asp:ListItem>
                      <asp:ListItem Value="3">3rd Party</asp:ListItem>
                </asp:DropDownList>
               <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInsuranceType" runat="server" ControlToValidate="ddlInsuranceType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Insurance Type" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Insurance Policy no"></asp:Label>
                <asp:TextBox ID="txtPolicyNo" runat="server" Enabled="false" autocomplete="off" CssClass="aspxcontrols" AutoPostBack="true" OnTextChanged="txtPolicyNo_TextChanged"></asp:TextBox>
           <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPolicyNo" runat="server" ControlToValidate="txtPolicyNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Insurance Number" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
                              </div>
                   <div class="col-sm-12 col-md-12" style="padding: 0px">
                           <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Amount"></asp:Label>
                <asp:TextBox ID="txtPolicyAmt" Enabled="false"   runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Expiry Date"></asp:Label>
                <asp:TextBox ID="txtExpiryDate" Enabled="false"  runat="server" autocomplete="off" CssClass="aspxcontrols"  placeholder="DD/MM/YY"></asp:TextBox>
                 <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVExpiryDate" runat="server" ControlToValidate="txtExpiryDate" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Expiry Date" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                 <cc1:CalendarExtender ID="cclExpiryDate" runat="server" PopupButtonID="txtExpiryDate" PopupPosition="BottomLeft" TargetControlID="txtExpiryDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
               </div>
             <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Policy Details"></asp:Label>
                <asp:TextBox ID="TxtPolicyDetails" Enabled="false"  runat="server" TextMode="MultiLine" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPolicyDetails" runat="server" ControlToValidate="TxtPolicyDetails" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Policy Details" ValidationGroup="Validate"></asp:RequiredFieldValidator>   
                   </div>
    </div>
        <div class="col-sm-12 col-md-12 form-group">
        <asp:TextBox ID="txtInsId" runat="server" Visible="false"></asp:TextBox>
            </div>   
   <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Insurance Payment Details</legend>
            </fieldset>
        </div>
             <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Insurance Policy no"></asp:Label>
                <asp:TextBox ID="txtPaymentPolicyNo" runat="server"  autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            </div>
         <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Insurance Company"></asp:Label>
                <asp:TextBox ID="txtInsComp" runat="server"  autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            </div>
             <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* From Date"></asp:Label>
             <asp:TextBox ID="txtFromDt" runat="server" autocomplete="off" CssClass="aspxcontrols" placeholder="DD/MM/YYYY"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtFromDt" PopupPosition="BottomLeft" TargetControlID="txtFromDt" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
                       </div>
            <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* To Date"></asp:Label>
             <asp:TextBox ID="txtToDt" runat="server" autocomplete="off" CssClass="aspxcontrols" placeholder="DD/MM/YYYY"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtToDt" PopupPosition="BottomLeft" TargetControlID="txtToDt" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
                       </div>
            
        </div>
      <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Paid Date"></asp:Label>
             <asp:TextBox ID="txtInsPaidDt" runat="server" autocomplete="off" CssClass="aspxcontrols" placeholder="DD/MM/YYYY"></asp:TextBox>
           <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtInsPaidDt" PopupPosition="BottomLeft" TargetControlID="txtInsPaidDt" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
                       </div>    
                     <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Paid Amount"></asp:Label>
             <asp:TextBox ID="txtInsPaidAmt" runat="server" autocomplete="off" CssClass="aspxcontrols" AutoPostBack="true"></asp:TextBox>
           </div>
                <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text=" Interest Amount"></asp:Label>
             <asp:TextBox ID="txtInterestAmt" runat="server" autocomplete="off" CssClass="aspxcontrols" AutoPostBack="true" OnTextChanged="txtInterestAmt_TextChanged"></asp:TextBox>
           </div>
               <div class="col-sm-3 col-md-3">
                               <asp:Label runat="server" Text="* Total Amount"></asp:Label>
             <asp:TextBox ID="txttotalAmt" runat="server" autocomplete="off" CssClass="aspxcontrols" AutoPostBack="true" OnTextChanged="txttotalAmt_TextChanged"></asp:TextBox>
           </div>
        
    </div>
      <div class="col-sm-12 col-md-12" style="padding: 0px">
                 <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Reference details"></asp:Label>
             <asp:TextBox ID="txtReference" runat="server" autocomplete="off"  CssClass="aspxcontrols"></asp:TextBox>
                   </div>
                <div class="col-sm-3 col-md-3"></div>
             <div class="col-sm-1 col-md-1">
            <br />
            <asp:ImageButton ID="imgbtnAddAmt" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Amount" ValidationGroup="ValidateAdd" />
        </div> 
          </div>

    <div class="col-sm-12 col-md-12 form-group">
              <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-3 col-md-3"></div><br />
            <div class="col-sm-3 col-md-3"></div><br />
            <div class="col-sm-3 col-md-3"></div>
    
           </div>
        <asp:DataGrid ID="dgInsuranceDet" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="80%" class="footable" Visible="false">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                  <asp:BoundColumn DataField="InsID" HeaderText="InstallID" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                   <asp:BoundColumn DataField="InsPolicyNO" HeaderText="Policy No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                      <asp:BoundColumn DataField="InsCompany" HeaderText="Insurance Company" >
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                   <asp:BoundColumn DataField="FromDate" HeaderText="From Date">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ToDate" HeaderText="To Date">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="15%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="PaidDate" HeaderText="Paid Date">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="PaidAmt" HeaderText="Paid Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                     <asp:BoundColumn DataField="InterestAmt" HeaderText="Interest Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                     <asp:BoundColumn DataField="TotalAmt" HeaderText="Total Amount">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                       <asp:BoundColumn DataField="ReferenceDet" HeaderText="Reference Details">
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





