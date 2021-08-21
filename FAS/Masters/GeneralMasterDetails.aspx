<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="GeneralMasterDetails.aspx.vb" 
    Inherits="Masters_GeneralMasterDetails" ValidateRequest ="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
       <style>        
                div{
            color:black;
                      }        
        </style>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>2.1 General Master Details</b></h2>
            </div>
            <div class="pull-right">
                <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />                
                <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin"  style="padding: 0px">
        <div class="pull-left">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-8 col-md-8" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblMasterHead" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlDesc" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4" style="padding-right:0px">
            <div class="form-group">
                <br />
                <asp:Label ID="Label2" runat="server" Text="Status :-"></asp:Label>
                <asp:Label ID="lblGeneralMasterStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
    </div>    

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-8 col-md-8" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblDesc" runat="server"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescName" runat="server" ControlToValidate="txtDesc" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDescName" runat="server" ControlToValidate="txtDesc" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtDesc" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
       <%-- <div class="col-sm-4 col-md-4" style="padding-right:0px">
            <div class="form-group">
                <asp:Label ID="lblCode" runat="server" Text="* Code"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCode" runat="server" ControlToValidate="txtCode" Display="Dynamic"  SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtCode" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
            </div>
        </div>--%>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-8 col-md-8" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVNotes" runat="server" ControlToValidate="txtNotes" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="aspxcontrols" Height="52px"></asp:TextBox>
            </div>
        </div>
    </div>
    <div id="ModalGeneralMasterDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblGeneralMasterDetailsValidationMsg" runat="server"></asp:Label>
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

    <div class="col-sm-12 col-md-12 form-group">
        <div id="DivForm" runat="server" data-toggle="collapse" data-target="#collapseForm"><a href="#"><b><i>Create Forms To be Used...</i></b></a></div>
    </div>
    <div id="collapseForm" class="collapse">
        <div class="col-sm-11 col-md-11 form-group" style="padding: 0px">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-8 col-md-8" style="padding-left: 0px">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlForms" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                </div>                
            </div>

            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-8 col-md-8" style="padding-left: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblForm" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFormDesc" runat="server" ControlToValidate="txtDesc" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFormDesc" runat="server" ControlToValidate="txtDesc" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtFormDesc" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div> 
                 <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                     <asp:ImageButton ID="imgbtnFormsSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateForm" />
                 </div>               
            </div>                      

        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group">
        <div id="DivPeriodicity" runat="server" data-toggle="collapse" data-target="#collapsePeriodicity"><a href="#"><b><i>Create Periodicity...</i></b></a></div>
    </div>
    <div id="collapsePeriodicity" class="collapse">
        <div class="col-sm-11 col-md-11 form-group" style="padding: 0px">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-8 col-md-8" style="padding-left: 0px">
                    <div class="form-group">
                        <asp:Label ID="Label5" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlPeriodicity" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                </div>                
            </div>

            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-8 col-md-8" style="padding-left: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblPeriodicity" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPeriodicity" runat="server" ControlToValidate="txtDesc" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPeriodicity" runat="server" ControlToValidate="txtDesc" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                        <asp:TextBox ID="txtPeriodicityDesc" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div> 
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                     <asp:ImageButton ID="imgbtnPeriodicitySave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidatePeriodicity" />
                 </div>                
            </div>

        </div>
    </div>
</asp:Content>
