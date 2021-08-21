<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="YearMasterDetails.aspx.vb" Inherits="Masters_YearMasterDetails" %>

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
    </script>
    <div class="loader">
    </div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>
                    <asp:Label ID="lblHead" runat="server"></asp:Label>
                </b></h2>
            </div>

            <div class="col-sm-9 col-md-9">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />

                </div>
            </div>
        </div>
    </div>

   
        <div class="col-sm-12 col-md-12" style="padding-left: 0px">
            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                <div class="form-group">
                    <asp:Label ID="lblSearch" runat="server" Text=""></asp:Label>
                    <asp:TextBox autocomplete="off" ID="txtFromYear" runat="server" CssClass="aspxcontrols" TabIndex="100" Width="90%"></asp:TextBox>

                </div>
            </div>
                     <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                    <asp:TextBox autocomplete="off" ID="txtToYear" runat="server" CssClass="aspxcontrols" TabIndex="100" Width="90%"></asp:TextBox>
                </div>
            </div>

            </div>

                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                    <asp:TextBox autocomplete="off" ID="TextBox2" runat="server" CssClass="aspxcontrols" TabIndex="100" Width="90%"></asp:TextBox>
                </div>
            </div>
                     <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                    <asp:TextBox autocomplete="off" ID="TextBox3" runat="server" CssClass="aspxcontrols" TabIndex="100" Width="90%"></asp:TextBox>
                </div>
            </div>
            </div>
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <asp:TextBox autocomplete="off" ID="TextBox1" runat="server" CssClass="aspxcontrols" TabIndex="100" Width="90%"></asp:TextBox>

            </div>
     
 
    <div id="ModalCustomerValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCustomerMasterValidationMsg" runat="server"></asp:Label></strong>
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

