<%@ Page Title="" Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="PrintSettings.aspx.vb" Inherits="Masters_PrintSettings" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        div {
            color: black;
        }

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }

        .auto-style1 {
            resize: none;
            font-size: 12px;
            line-height: 1.42857143;
            color: #444444;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            font-family: "Segoe UI", sans-serif;
            border: 1px solid #ccc;
            margin-top: 1px;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }

        .auto-style2 {
            width: 100%;
            height: 26px;
            resize: none;
            font-size: 12px;
            line-height: 1.42857143;
            color: #444444;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            font-family: "Segoe UI", sans-serif;
            border: 1px solid #ccc;
            margin-top: 1px;
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/bootstrap-multiselect.css" rel="stylesheet" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap-multiselect.js"></script>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />

    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>

    <script lang="javascript" type="text/javascript">
        $(function () {
            $(":file").change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();
                    reader.onload = imageIsLoaded;
                    reader.readAsDataURL(this.files[0]);
                }
            });
        });

        function imageIsLoaded(e) {
            $('#myImg').attr('src', e.target.result);
        };


        $(function () {
            $(":file").change(function () {
                if (this.files && this.files[0]) {
                    var reader = new FileReader();
                    reader.onload = imageIsLoadedS;
                    reader.readAsDataURL(this.files[0]);
                }
            });
        });

        function imageIsLoadedS(e) {
            $('#myImgS').attr('src', e.target.result);
        };
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>1.5 Print Settings</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" />
                    <%--                    <asp:ImageButton ID="imgbtnSavePurchase" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" />--%>
                </div>
            </div>
        </div>
    </div>


    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12" style="padding-left: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server" visible="True">
        <div id="div2" runat="server">
            <ul class="nav nav-tabs" role="tablist">
                <li id="liPurchase" runat="server">
                    <asp:LinkButton ID="lnkbtnPurchase" Text="Purchase" runat="server" Font-Bold="true" /></li>
                <li id="liSales" runat="server">
                    <asp:LinkButton ID="lnkbtnSales" Text="Sales" runat="server" Font-Bold="true" /></li>
            </ul>
        </div>

        <div class="tab-content divmargin">
            <div runat="server" role="tabpanel" class="tab-pane active" id="divPurchase">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblPurchase" runat="server" Text="Purchase Print Settings" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>

                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Customer" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:CheckBox ID="chkPCAddress" runat="server" Text="Address" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPCPhNo" runat="server" Text="Phone No" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPCEmail" runat="server" Text="Email" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPCVAT" runat="server" Text="VAT" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPCTAX" runat="server" Text="TAX" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPCPAN" runat="server" Text="PAN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPCTAN" runat="server" Text="TAN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPCTIN" runat="server" Text="TIN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPCCIN" runat="server" Text="CIN" Width="150px" />
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Buyer" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:CheckBox ID="chkPBAddress" runat="server" Text="Address" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPBPhNo" runat="server" Text="Phone No" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPBEmail" runat="server" Text="Email" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPBVAT" runat="server" Text="VAT" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPBTAX" runat="server" Text="TAX" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPBPAN" runat="server" Text="PAN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPBTAN" runat="server" Text="TAN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPBTIN" runat="server" Text="TIN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkPBCIN" runat="server" Text="CIN" Width="150px" />
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:CheckBox ID="chkPSizeWiseReport" runat="server" Text="SizeWiseReport" Width="150px" />
                        <asp:CheckBox ID="chkPTerms" runat="server" Text="Terms&amp;Conditions" Width="150px" />
                        <asp:CheckBox ID="chkPReceivers" runat="server" Text="Receiver's Signature" Width="150px" />
                        <asp:CheckBox ID="chkPAuthorised" runat="server" Text="Authorised Signature" Width="180px" />
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:Image ID="myImgP" runat="server" Width="80px" Height="50px" />
                        <br />
                        <asp:FileUpload ID="FileUploadPurchase" runat="server" />
                    </div>
                </div>


            </div>


            <div runat="server" role="tabpanel" class="tab-pane" id="divSales">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblSales" runat="server" Text="Sales Print Settings" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>

                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Customer" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:CheckBox ID="chkCAddress" runat="server" Text="Address" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkCPhNo" runat="server" Text="Phone No" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkCEmail" runat="server" Text="Email" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkCVAT" runat="server" Text="VAT" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkCTAX" runat="server" Text="TAX" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkCPAN" runat="server" Text="PAN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkCTAN" runat="server" Text="TAN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkCTIN" runat="server" Text="TIN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkCCIN" runat="server" Text="CIN" Width="150px" />
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Buyer" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:CheckBox ID="chkBAddress" runat="server" Text="Address" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkBPhNo" runat="server" Text="Phone No" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkBEmail" runat="server" Text="Email" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkBVAT" runat="server" Text="VAT" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkBTAX" runat="server" Text="TAX" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkBPAN" runat="server" Text="PAN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkBTAN" runat="server" Text="TAN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkBTIN" runat="server" Text="TIN" Width="150px" />
                        <br />
                        <asp:CheckBox ID="chkBCIN" runat="server" Text="CIN" Width="150px" />
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:CheckBox ID="chkSizeWiseReport" runat="server" Text="SizeWiseReport" Width="150px" />
                        <asp:CheckBox ID="chkTerms" runat="server" Text="Terms&amp;Conditions" Width="150px" />
                        <asp:CheckBox ID="chkReceivers" runat="server" Text="Receiver's Signature" Width="150px" />
                        <asp:CheckBox ID="chkAuthorised" runat="server" Text="Authorised Signature" Width="180px" />
                    </div>

                    <div class="col-sm-3 col-md-3">
                        <asp:Image ID="myImgS" runat="server" Width="80px" Height="50px" />
                        <br />
                        <asp:FileUpload ID="FileUploadSales" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
        </div>
    </div>
</asp:Content>

