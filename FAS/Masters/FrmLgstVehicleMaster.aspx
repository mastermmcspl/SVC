
<%@ Page Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="FrmLgstVehicleMaster.aspx.vb" Inherits="Masters_FrmLgstVehicleMaster" ValidateRequest="false" %>

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
              $('#<%=ddlExistingVehicleNo.ClientID%>').select2();
             });
        function validatePage() {
            //Executes all the validation controls associated with group1 validaiton Group1. 
            var flag = window.Page_ClientValidate('Validate');
            if (flag)
                return flag;

        }
     <%--   function Validate() {
            if (document.getElementById('<%=txtRegistrationNo.ClientID %>').value == "") {
                alert('Enter vehicle No.');
                document.getElementById('<%=txtRegistrationNo.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtChassisNo.ClientID %>').value == "") {
                alert('Enter chasis No.');
                document.getElementById('<%=txtChassisNo.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlVehicleType.ClientID %>').selectedIndex == 0) {
                 alert('Select Vehicle Type.');
                 document.getElementById('<%=ddlVehicleType.ClientID%>').focus()
                 return false
            }               
        }--%>
    </script>

       <style>        
                div{
            color:black;
                      }        
        </style>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>Vehicle Master Details</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false"  />
                  <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save"  ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" visible="false"/>
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" Visible="false" />
                  <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;" Visible="false"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0" Visible="false"></asp:Label></span>
                   <asp:Label ID="lblID" runat="server" Visible ="false" ></asp:Label>
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
            <asp:DropDownList ID="ddlExistingVehicleNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3"></div>   
        <div class="col-sm-3 col-md-3"></div>   
        <div class="col-sm-3 col-md-3">  
            <br />
            <div class="form-group" runat ="server" >
            <asp:Label runat="server" Text="Status :-"></asp:Label>
            <asp:Label ID ="lblStatus" runat="server" Text=""></asp:Label>
            </div>          
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Registration No        (AA-00-AA-0000 Format)"></asp:Label> 
            <asp:TextBox ID="txtRegistrationNo" runat="server" autocomplete="off"  CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Vehicle Registration No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRegistrationNo" runat="server" ControlToValidate="txtRegistrationNo" Display="Dynamic" ErrorMessage="Enter Valid Registration no." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[A-Z|a-z]{2}\s?[-][0-9]{1,2}\s?[-][A-Z|a-z]{0,3}\s?[-][0-9]{4}$"></asp:RegularExpressionValidator>
              </div>        
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Chassis No"></asp:Label>
           <asp:TextBox ID="txtChassisNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVChassisNo" runat="server" ControlToValidate="txtChassisNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Chassis No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
     <%--   <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVChassisNo" runat="server" ControlToValidate="txtChassisNo" Display="Dynamic" ErrorMessage="Enter Valid Chassis no." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^([A-z]{2}[A-z0-9]{5,16})$"></asp:RegularExpressionValidator>--%>
                 </div>
         <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Engine Code"></asp:Label>
            <asp:TextBox ID="txtEngineNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEngineNo" runat="server" ControlToValidate="txtEngineNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Engine No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
     <%--    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEngineNo" runat="server" ControlToValidate="txtEngineNo" Display="Dynamic" ErrorMessage="Enter Valid Engine no." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^([A-z]{2}[A-z0-9]{5,16})$"></asp:RegularExpressionValidator>--%>
               </div>
        </div>
     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

         <div class="col-sm-4 col-md-4">
             <asp:Label runat="server" Text="* Vehicle Type"></asp:Label>
             <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVVehicleType" runat="server" ControlToValidate="ddlVehicleType" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Select Vehicle Type" ValidationGroup="Validate"></asp:RequiredFieldValidator>

         </div>
           <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Vehicle Company Name"></asp:Label>
            <asp:TextBox ID="txtOwnerName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOwnerName" runat="server" ControlToValidate="txtOwnerName" Display="Dynamic" ErrorMessage="Enter Owner Name" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
             </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Insurance Details</legend>
            </fieldset>
                </div>
                   <div class="col-sm-12 col-md-12" style="padding: 0px">
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
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Insurance Policy no"></asp:Label>
                <asp:TextBox ID="txtPolicyNo" runat="server" Enabled="false" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
           <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPolicyNo" runat="server" ControlToValidate="txtPolicyNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Insurance Number" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text="* Amount"></asp:Label>
                <asp:TextBox ID="txtPolicyAmt" Enabled="false"   runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
           <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPolicyAmt" runat="server" ControlToValidate="txtPolicyAmt" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Insurance Amount" ValidationGroup="Validate"></asp:RequiredFieldValidator>
       <%--    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPolicyAmt" runat="server" ControlToValidate="txtPolicyAmt" Display="Dynamic" ErrorMessage="Enter Valid Amount." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[\d]+[\.][\d]{2}$"></asp:RegularExpressionValidator>--%>
                     </div>
                       </div>
                   <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
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
            </div>
                  <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                           <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text=" Service center Details"></asp:Label>
            <asp:TextBox ID="txtServiceCenter" runat="server" autocomplete="off" CssClass="aspxcontrols"  TextMode="MultiLine" Height="50px"></asp:TextBox>
         </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text=" Details"></asp:Label>
            <asp:TextBox ID="txtDetails" runat="server" autocomplete="off" CssClass="aspxcontrols"  TextMode="MultiLine" Height="50px"></asp:TextBox>
         </div>
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

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:TextBox ID ="txtGLID" runat ="server" Visible ="false" ></asp:TextBox>
    </div>
</asp:Content>


