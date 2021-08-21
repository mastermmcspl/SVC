
<%@ Page Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="FrmLgstDriverMaster.aspx.vb" Inherits="Masters_FrmLgstDriverMaster" ValidateRequest="false" %>

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
            $('#<%=ddlExistingDriver.ClientID%>').select2();
                       $('#<%=ddlInsuranceType.ClientID%>').select2();
            });
        function validatePage() {
            //Executes all the validation controls associated with group1 validaiton Group1. 
            var flag = window.Page_ClientValidate('Validate');
            if (flag)
                return flag;

        }
    <%--    function Validate() {
            if (document.getElementById('<%=txtDriverName.ClientID %>').value == "") {
                alert('Enter Driver Name.');
                document.getElementById('<%=txtDriverName.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtAdharNo.ClientID %>').value == "") {
                alert('Enter Adhar No.');
                document.getElementById('<%=txtAdharNo.ClientID%>').focus()
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
                <h2><b>Driver Master Details</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                  <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                         <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save"  ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" Visible="false" />
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
            <asp:Label runat="server" Text="Existing Driver"></asp:Label> 
            <asp:DropDownList ID="ddlExistingDriver" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
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
            <asp:Label runat="server" Text="* Driver Name"></asp:Label> 
            <asp:TextBox ID="txtDriverName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDriverName" runat="server" ControlToValidate="txtDriverName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Driver Name" ValidationGroup="Validate"></asp:RequiredFieldValidator>
          </div>        
        <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* License No"></asp:Label>
           <asp:TextBox ID="txtLicenseNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLicenseNo" runat="server" ControlToValidate="txtLicenseNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter License No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
           <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Aadhar No"></asp:Label>
            <asp:TextBox ID="txtAdharNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAdharNo" runat="server" ControlToValidate="txtAdharNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Adhar No" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAdharNo" runat="server" ControlToValidate="txtAdharNo" Display="Dynamic" ErrorMessage="Enter Valid Adhar No." SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[0-9\s]{12}$"></asp:RegularExpressionValidator>
                </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                      <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Contact No"></asp:Label>
            <asp:TextBox ID="TxtContactNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVContactNo" runat="server" ControlToValidate="TxtContactNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Contact No" ValidationGroup="Validate" ></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVContactNo" runat="server" ControlToValidate="TxtContactNo" Display="Dynamic" ErrorMessage="Enter Valid Mobile No." SetFocusOnError="True" ValidationExpression="^[0-9\s]{10}$" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                </div>
                    <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* City"></asp:Label>
            <asp:TextBox ID="txtCity" runat="server" autocomplete="off" CssClass="aspxcontrols"  ></asp:TextBox>
       <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCity" runat="server" ControlToValidate="txtCity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter City" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                  </div>
               <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text=" Pincode"></asp:Label>
            <asp:TextBox ID="txtPinCode" runat="server" autocomplete="off" CssClass="aspxcontrols" ></asp:TextBox>
   <%--  <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPinCode" runat="server" ControlToValidate="txtPinCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Pin Code" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                  <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPinCode" runat="server" ControlToValidate="txtPinCode" Display="Dynamic" ErrorMessage="Enter Valid PinCode." SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,12}$"></asp:RegularExpressionValidator>
                 --%> </div>
          </div>   
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Insurance Details</legend>
            </fieldset>
                </div>
                   <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text=" Type of Insurance"></asp:Label>
                <asp:DropDownList ID="ddlInsuranceType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                       <asp:ListItem Value="0">Select Insurance Type</asp:ListItem>
                        <asp:ListItem Value="1">Life Insurance</asp:ListItem>
                                 <asp:ListItem Value="2">Health Insurance</asp:ListItem>
                               </asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text=" Insurance Policy no"></asp:Label>
                <asp:TextBox ID="txtPolicyNo" runat="server" autocomplete="off" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
                   </div>
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text=" Amount"></asp:Label>
                <asp:TextBox ID="txtPolicyAmt" runat="server" autocomplete="off" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
                 </div>
                       </div>
                   <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <asp:Label runat="server" Text=" Expiry Date"></asp:Label>
                <asp:TextBox ID="txtExpiryDate" runat="server" autocomplete="off"  CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                 
            <cc1:CalendarExtender ID="ccExpiryDate" runat="server" PopupButtonID="txtExpiryDate" PopupPosition="BottomLeft" TargetControlID="txtExpiryDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
            </cc1:CalendarExtender>
         <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVExpiryDate" runat="server" ControlToValidate="txtExpiryDate" Display="Dynamic" SetFocusOnError="True"
                ErrorMessage="Enter Expiry Date." ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVExpiryDate" runat="server" ControlToValidate="txtExpiryDate" Display="Dynamic"
                SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                 </div>
             <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text=" Policy Details"></asp:Label>
                <asp:TextBox ID="TxtPolicyDetails" runat="server" TextMode="MultiLine" autocomplete="off" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
                      </div>
    </div>
            </div>  
          <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
          <div class="col-sm-4 col-md-4">
            <asp:Label runat="server" Text="* Address"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" autocomplete="off" CssClass="aspxcontrols"  TextMode="MultiLine" Height="50px"></asp:TextBox>
      <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAddress" runat="server" ControlToValidate="txtAddress" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Address" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                 </div>
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
                                <asp:Label ID="lblDriverValidationMsg" runat="server"></asp:Label>
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



