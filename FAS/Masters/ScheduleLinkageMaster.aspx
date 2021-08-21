<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Master.master" CodeFile="ScheduleLinkageMaster.aspx.vb" Inherits="Masters_ScheduleLinkageMaster"  ValidateRequest="false" Debug="true" %>

<%--<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>--%>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
            <div class="col-sm-4 col-md-4 pull-left">
                <h2><b>4.2 Schedule Linkage Master</b></h2>
            </div>
            <div class="form-group pull-right">
                <asp:ImageButton CssClass="activeIcons hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="" runat="server" />
                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                <ul class="nav navbar-nav navbar-right logoutDropdown">
                    <li class="dropdown">
                        <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                            <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
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
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
           <asp:Label ID="lblUpdate" runat="server" visible="false"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label runat="server" Text="Head"></asp:Label>
                <asp:DropDownList ID="ddlHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label runat="server" Text="Group"></asp:Label>
                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">             
               <asp:Label runat="server" Text="Sub Group"></asp:Label>
                <asp:DropDownList ID="ddlSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <fieldset>
                <legend class="legendbold">General Ledger</legend>  
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                    <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                        <asp:Label runat="server" Text="Search By General Ledger"></asp:Label>
                        <asp:TextBox autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                    </div>
                    <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                       <asp:Label runat="server" Text="* Red Color -  Mapped General Ledger" ForeColor="Red"></asp:Label><br/>
                        <asp:Label runat="server" Text="* Black Color -  UnMapped General Ledger" ForeColor="Black"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                   <asp:Panel ID="pnlLedger" runat="server" Height="300px" Width="525px" ScrollBars="Horizontal" BorderStyle="Solid" BorderWidth="1px">
                        <asp:CheckBoxList ID="chkLedger" runat="server"></asp:CheckBoxList>
                    </asp:Panel>
                </div>
                <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px;">
                    <div class="col-sm-4 col-md-4">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn-ok" />                        
                    </div>                    
                </div>                
            </fieldset>
        </div>
        <div class="col-sm-6 col-md-6">
            <fieldset>
                <legend class="legendbold">Linkage General Ledger</legend>
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                    <asp:Label runat="server" Text="Note No."></asp:Label>
                    <asp:TextBox ID="txtNote" autocomplete="off" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

                    <asp:ListBox ID="lstGL" runat="server" Height="300px" Width="525px" SelectionMode="Multiple" ></asp:ListBox>
                </div>
                <div class="col-sm-2 col-md-2 divmargin" style="padding: 0px;">
                    <div class="form-group">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn-ok" />
                    </div>
                </div>
            </fieldset>
        </div>

    </div>
    <div>
        <asp:TextBox autocomplete="off" ID="txtCurrentID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtDepthID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtParentID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtOrgStrCurrentLvlName" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtOrgStrNextLvlName" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
        <asp:TextBox autocomplete="off" ID="txtSaveOrUpdate" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
    </div>
    <div id="ModalValidationOrgStructurer" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsgOrgStructurer" runat="server"></asp:Label></strong>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

