<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Master.master" CodeFile="ExcelUpload.aspx.vb" Inherits="Masters_ExcelUpload" ValidateRequest="false" Debug="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                div{
            color:black;
                      }      
    </style>


    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
      <link  rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
      <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlMasterName.ClientID%>').select2();
        });
        $('#collapseRRIT').collapse({
            toggle: false
        })

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>2.8 Excel Upload</b></h2>
            </div>
            <div class="pull-right col-sm-3 col-md-3">
                <div class="pull-right ">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Upload" ValidationGroup="Validate" />                    
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
   <%-- <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        
    </div>--%>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        <div class="col-sm-8 col-md-8" style="padding-left: 0; padding-right: 0">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="col-sm-8 col-md-8" style="padding-left: 0; padding-right: 0">
            <asp:Label ID="lblMsg" runat="server" ForeColor ="Blue" Font-Bold ="true" ></asp:Label>
        </div>
        <div class="col-sm-4 col-md-4 divmargin" style="padding-right: 0px">
            <asp:LinkButton runat="server" ID="LnkbtnExcel" Font-Bold="False" Font-Underline="True" ForeColor="Blue" />
        </div>
        <div class="form-group divmargin"></div>
        <div id="collapseRRIT" class="collapse">
            <div class="col-sm-12 col-md-12" style="padding: 0px;">
                <div class="form-group">
                    <asp:DataGrid ID="dgSampleFormat" runat="server" AutoGenerateColumns="true" Width="100%" class="footable">
                        <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>
        <div class="divmargin "></div>

        <div id ="divZone" runat ="server">
        <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0;">
        <div class="col-sm-3 col-md-3" style="padding-left: 0;">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" SetFocusOnError="True"
                     ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
         <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True"
                    ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
        </div>
        </div>
            </div>

        <div class="col-sm-4 col-md-4" style="padding-left: 0;">
            <div class="form-group">
                <label>Master Type</label>
                <asp:DropDownList ID="ddlMasterName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">  
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblSelectFile" runat="server" Text="Select a file"></asp:Label>
                <asp:FileUpload ID="FULoad" CssClass="aspxcontrols" value="Browse" name="avatar" runat="server" />
            </div>
            <asp:TextBox ID="txtPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" />
        </div>
        <div class="col-sm-1 col-md-1">
            <div class="form-group">
                <div style="margin-top: 20px;"></div>
                <asp:Button ID="btnOk" runat="server" Text="Ok" />
            </div>
        </div>
        <div class="col-sm-4 col-md-4 " >
            <div class="form-group">
                <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name"></asp:Label>
                <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>               
            </div>
        </div>
    </div>

    <div class="col-sm-4 col-md-4" style="padding-left: 0;">
        <div class="form-group">           
            <asp:RadioButton ID ="rboWithGST" runat ="server" Width ="150px" GroupName ="select" Text ="With GST Rates" />
            <asp:RadioButton ID ="rboWithoutGST" runat ="server" Width ="150px" GroupName ="select" Text ="With Out GST Rates" />
        </div>
    </div>

    <div id="DivOpBreak" runat="server">
        <div class="col-sm-12 col-md-12" style="padding: 0px;">
            <div class="col-sm-4 col-md-4" style="padding-left: 0;">
                <div class="form-group">
                    <label>Select SubLedgers</label>
                    <asp:DropDownList ID="ddlSubLedger" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-3 col-md-3" style="padding-left: 0;">
                <div class="form-group">
                    <br />                    
                    <asp:Label ID="Label1" runat="server" Text ="Op.Bal Debit - " Font-Bold ="true" ForeColor ="Black" ></asp:Label>
                    <asp:Label ID="lblOpBalDebit" runat="server" Font-Bold ="true" ForeColor ="Black" ></asp:Label>
                </div>
            </div>
            <div class="col-sm-3 col-md-3" style="padding-left: 0;">
                <div class="form-group">
                    <br />                    
                    <asp:Label ID="Label2" Text ="Op.Bal Credit - " runat="server" Font-Bold ="true" ForeColor ="Black" ></asp:Label>
                    <asp:Label ID="lblOpBalCreadit" runat="server" Font-Bold ="true" ForeColor ="Black" ></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-12" style="padding-left: 0; padding-right: 0">
        <div id="divExcel" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; width: 100%;">
            <asp:DataGrid ID="dgGeneral" runat="server" AutoGenerateColumns="true" AllowPaging="false" class="footable">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" HorizontalAlign="Left" VerticalAlign="Top" />
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
            </asp:DataGrid>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px;">
        <div class="form-group">
           <asp:GridView ID="dgUpload" runat="server" AutoGenerateColumns="true"  Width="100%" class="footable">                    
           </asp:GridView>
       </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>

    <div id="ModalExcelValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divExcelMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblExcelValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
