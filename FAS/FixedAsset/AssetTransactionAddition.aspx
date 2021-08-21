<%@ Page Title="" Language="VB" MasterPageFile="~/FixedAsset.master" AutoEventWireup="false" CodeFile="AssetTransactionAddition.aspx.vb" Inherits="FixedAsset_AssetTransaction" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        function Validate() {

            if (document.getElementById('<%=ddlAssetTrnfr.ClientID %>').selectedIndex == 0) {
                alert('Select Asset Transfer.');
                document.getElementById('<%=ddlAssetTrnfr.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlTrTypes.ClientID %>').selectedIndex == 0) {
                alert('Select Transaction Type.');
                document.getElementById('<%=ddlTrTypes.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlSupplier.ClientID %>').selectedIndex == 0) {
                alert('Select Supplier Name.');
                document.getElementById('<%=ddlSupplier.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=drpAstype.ClientID %>').selectedIndex == 0) {
                alert('Select Asset Type.');
                document.getElementById('<%=drpAstype.ClientID%>').focus()
                return false
            }
        }
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Asset Addition</b></h2>
            </div>

            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="ImgBtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                  
                    <asp:ImageButton ID="imgbtnsave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="Imgbtnphyvrfn" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Physical Verification" ValidationGroup="Validate" />
                      <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                   <asp:ImageButton ID="ImgbtnActivate" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" CausesValidation="false" />
                       <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="ValidateApprove" />
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="clearfix divmargin">
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblPermissionType1" runat="server" Text="Asset Addition Type:-" Visible="false"></asp:Label>
            &nbsp;&nbsp;
             <asp:RadioButton ID="rboNew" Text="NEW" GroupName="Select" runat="server" Checked="true" Visible="false" />
            &nbsp;&nbsp;  
                <asp:RadioButton ID="rboOld" Text="OLD" GroupName="Select" runat="server" Visible="false" />
        </div>
        <div class="col-sm-6 col-md-6" style="padding-left: 0; padding-right: 0">
            <div class="col-sm-3 col-md-3 pull-right ">
                <asp:LinkButton ID="AdditionalDtls" runat="server" ForeColor="#009900">AdditionalDetails</asp:LinkButton>
            </div>
        </div>
        <br />
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Status:"></asp:Label>
            <asp:Label ID="lblstatus" runat="server" CssClass="Label"></asp:Label>
        </div>

    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Existing TransactionNo"></asp:Label>
            <asp:DropDownList ID="ddlExtTrnNo" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVExtno" runat="server" ControlToValidate="ddlExtTrnNo" Display="Dynamic" SetFocusOnError="True"
                ValidationGroup="ValidateApprove"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Asset Transfer"></asp:Label>
            <asp:DropDownList ID="ddlAssetTrnfr" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAssetTrnfr" ControlToValidate="ddlAssetTrnfr" Display="Dynamic" SetFocusOnError="True" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Currency Types"></asp:Label>
            <asp:DropDownList ID="ddlCurencyType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCurencyType" ControlToValidate="ddlCurencyType" Display="Dynamic" SetFocusOnError="True" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="txtCurency" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:TextBox>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Date Of Addition"></asp:Label>
            <asp:TextBox ID="txtDtAddtn" runat="server" CssClass="aspxcontrols" AutoComplete="off"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDtAddtn" TargetControlID="txtDtAddtn" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
          <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDtAddtn" ControlToValidate="txtDtAddtn" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtDtAddtn" runat="server" ControlToValidate="txtDtAddtn" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>
       
              </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Depreciation Rate"></asp:Label>
            <asp:TextBox ID="txtDeprcn" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:TextBox>
        </div>

    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Actual Location"></asp:Label>
            <asp:TextBox ID="txtLocID" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLocation" runat="server" ControlToValidate="txtLocID" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Label runat="server" Text="Asset Age"></asp:Label>
            <asp:TextBox ID="txtbxAstAge" runat="server" CssClass="aspxcontrols"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Addition Reasons"></asp:Label>
            <asp:DropDownList ID="ddlTrTypes" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTRType" ControlToValidate="ddlTrTypes" Display="Dynamic" runat="server" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplierName" ControlToValidate="ddlSupplier" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Supplier Code"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtSprCode"></asp:TextBox>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Asset Type"></asp:Label>
            <asp:DropDownList ID="drpAstype" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdrpAstype" ControlToValidate="drpAstype" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="ItemCode"></asp:Label>
            <asp:DropDownList ID="txtbxItmCode" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmCode" ControlToValidate="txtbxItmCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter The Description" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="AssetNo"></asp:Label>
            <asp:TextBox ID="txtAssetNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Asset RefNo"></asp:Label>
            <asp:TextBox ID="txtAstNOSup" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAstRefNo" ControlToValidate="txtAstNOSup" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="txtbxDscrptn" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDscrptn" ControlToValidate="txtbxDscrptn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter The Description" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Item Description"></asp:Label>
            <asp:TextBox ID="txtbxItmDecrtn" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmDecrtn" ControlToValidate="txtbxItmDecrtn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter the ItemDescription" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Label runat="server" Text="Quantity"></asp:Label>
            <asp:TextBox ID="txtbxQty" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxQty" ControlToValidate="txtbxQty" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter The Quantity" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Date Of Purchase"></asp:Label>
            <asp:TextBox ID="txtbxDteofPurchase" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxDteofPurchase_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteofPurchase" TargetControlID="txtbxDteofPurchase" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteofPurchase" ControlToValidate="txtbxDteofPurchase" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteofPurchase1" runat="server" ControlToValidate="txtbxDteofPurchase" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Date Of Commission"></asp:Label>
            <asp:TextBox ID="txtbxDteCmmunictn" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxDteCmmunictn_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteCmmunictn" TargetControlID="txtbxDteCmmunictn" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteCmmunictn" ControlToValidate="txtbxDteCmmunictn" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteCmmunictn1" runat="server" ControlToValidate="txtbxDteCmmunictn" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-1 col-md-1">
            <asp:Label runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="txtbxamount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVamount" runat="server" ControlToValidate="txtbxamount" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVamount" ControlToValidate="txtbxamount" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>
    </div>
  <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

      </div>
    <div id="myModalAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtfile" runat="server" CssClass="btn-ok" Width="95%" AllowMultiple="true" />
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                    <asp:Button ID="btnScan" runat="server" Text="Scan" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                        Visible="false" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                        runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--    <div id="myModalAttch" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12 pull-left">
                        <asp:Label ID="lblMsg1" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12 pull-left">
                        <asp:Label ID="lblBrowse1" runat="server" Text="Click Browse and Select a File."></asp:Label>
                        <asp:Label ID="lblSize1" runat="server" Font-Bold="True" Text=""></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-4 col-md-4" style="padding: 0px;">
                            <div class="form-group">
                                <asp:FileUpload ID="txtfile1" runat="server" CssClass="btn-ok" Width="95%" AllowMultiple="true" />
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2" style="padding: 0px;">
                            <div class="form-group">
                                <asp:Button ID="btnAddAttch1" runat="server" Text="Add" CssClass="btn-ok" />
                                <asp:Button ID="btnRemoteIndex" runat="server" Text="Index" CssClass="btn-ok" />
                            </div>
                        </div>
                    </div>
                    <br />
                    &nbsp;&nbsp;

                     <div class="col-sm-12 col-md-12">
                         <div class="form-group">
                             <asp:GridView ID="gvattach1" runat="server" AutoGenerateColumns="False" class="footable" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Available">
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-Width="1%">
                                         <HeaderTemplate>
                                             <asp:CheckBox ID="chkSelectAll1" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged1" />
                                         </HeaderTemplate>
                                         <ItemTemplate>
                                             <asp:CheckBox ID="chkSelect1" runat="server" CssClass="hvr-bounce-in" />
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                                         <ItemTemplate>
                                             <asp:Label ID="lblPath1" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath1") %>' />
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="FileName1" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                                     <asp:BoundField DataField="Extension1" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                                     <asp:BoundField DataField="CreatedOn1" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
                                 </Columns>
                             </asp:GridView>
                         </div>
                     </div>
                </div>
                <div class="col-md-12">
                </div>
            </div>
        </div>
    </div>--%>


    <%-- <div class="col-sm-12 col-md-12">
        <div class="col-sm-2 col-md-2" style="padding: 0px;">
     <asp:Button ID="btnRemoteIndex" runat="server" visible="false" Text="Index" CssClass="btn-ok" />
</div>
</div>
    <br />&nbsp;&nbsp;--%>

    <%--<div class="col-sm-12 col-md-12">
        <asp:GridView ID="gvattach1" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="1%">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll1" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged1" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect1" runat="server" CssClass="hvr-bounce-in" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblPath1" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath1") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FileName1" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                <asp:BoundField DataField="Extension1" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                <asp:BoundField DataField="CreatedOn1" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>--%>

    <%--    <div class="col-md-12 form-group">
        <div id="divcollapseAttachments" runat="server" visible="false" data-toggle="collapse" data-target="#collapseAttachments"><a href="#"><b><i>Click here to view Attachments...</i></b></a></div>
    </div>
    <div id="collapseAttachments" class="col-sm-12 col-md-12 collapse form-group">
        <div class="col-sm-6 col-md-6" style="max-height: 138px; padding-left: 0px; padding-right: 0px;">
            <asp:DataGrid ID="dgAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="footable" OnRowDataBound="PickColor_RowDataBound">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                    </asp:BoundColumn>

                    <asp:TemplateColumn HeaderText="File Name">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="28%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                            <asp:Label ID="lblExt" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Ext") %>'></asp:Label>
                            <asp:Label ID="lblFile" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:Label>
                            <asp:LinkButton ID="File" CommandName="OPENPAGE" Font-Italic="true" runat="server" Visible="false" Font-Bold="False" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Created">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <b>By:-</b>
                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <b>On:-</b>
                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnView" data-toggle="tooltip" data-placement="bottom" title="VIEW" runat="server" CommandName="VIEW" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Add Description" CommandName="ADDDESC" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDownload" data-toggle="tooltip" data-placement="bottom" title="DownLoad" CommandName="OPENPAGE" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Image ID="imgView" runat="server" Width="250px" Height="200px" />
        </div>
    </div>--%>


    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Debit Details</legend>
                        </fieldset>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDrOtherHead" runat="server" ControlToValidate="ddlDrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="*General Ledger"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherGL" runat="server" ControlToValidate="ddlDbOtherGL" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDbOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                            <%--    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherSubGL" runat="server" ControlToValidate="ddlDbOtherSubGL" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlDbOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>

                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOtherDAmount" runat="server" ControlToValidate="txtOtherDAmount" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Enter Amount" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOtherDAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDrOtherHead" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherGL" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherSubGL" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnDADD" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateDBAdd" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Credit Details</legend>
                        </fieldset>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherHead" runat="server" ControlToValidate="ddlCrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherGL" runat="server" ControlToValidate="ddlCrOtherGL" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCrOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherSubGL" runat="server" ControlToValidate="ddlCrOtherSubGL" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlCrOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCRAmount" runat="server" ControlToValidate="txtOtherCAmount" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Enter Amount" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOtherCAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherHead" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherGL" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherSubGL" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnOtherCADD" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateCRAdd" />
                </div>
            </div>
        </div>
    </div>


    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgPaymentDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>


                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="DebitOrCredit" HeaderText="DebitOrCredit" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete1" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="DELETE" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>



    <div class="col-sm-12 col-md-12 pull-left ">
        <div class="col-sm-3 col-md-3 pull-left ">
            <asp:LinkButton ID="lnkBtnPrvsTrans" runat="server"><h5><b><u>Previous Transaction</u></b></h5></asp:LinkButton>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 pull-right ">
    </div>
    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgPrevTransDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <%-- <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                --%>

                <asp:BoundColumn DataField="AssetNo" HeaderText="Transaction No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <%--      <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

                <%--                <asp:BoundColumn DataField="DebitOrCredit" HeaderText="DebitOrCredit" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

                <%--  <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete1" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="DELETE" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>--%>
            </Columns>
        </asp:DataGrid>
    </div>


    <div id="myModalPhyvrn" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Physical Verification details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblVerfdby" runat="server" Text="VerifiedBy"></asp:Label>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblVerfiedon" runat="server" Text="VeriedOn"></asp:Label>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblApprobedby" runat="server" Text="ApprovedBy"></asp:Label>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblapprovedon" runat="server" Text="ApprovedOn"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtVrfdby" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtVerfiedon" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                            <cc1:CalendarExtender ID="CalenderVerfiedon1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtVerfiedon" TargetControlID="txtVerfiedon" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtappedby" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtapprovedon" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                            <cc1:CalendarExtender ID="Calendarapprovedon1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtapprovedon" TargetControlID="txtapprovedon" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-6 col-md-6">
                            <asp:Label ID="lblRemark" runat="server" Text="Remarks"></asp:Label>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:Label ID="lblremarks" runat="server" Text="Remarks"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-6 col-md-6">
                            <asp:TextBox ID="txtvrfremark" Height="50px" autocomplete="off" TextMode="MultiLine" runat="server" CssClass="aspxcontrols" />
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:TextBox ID="txtAppremarks" autocomplete="off" Height="50px" TextMode="MultiLine" runat="server" CssClass="aspxcontrols" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="Update" ID="btnUpdatePhyvrn" autopostback="true" ValidationGroup="ValidateApprove"></asp:Button>
                        <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                            No
                        </button>
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
    <div id="ModalAdditionValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAssetAdditionValidationMsg" runat="server"></asp:Label></strong>
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

    <div class=" modal fade" id="myAttchment" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Attachment</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblTax" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-5 col-md-5" style="padding-left: 0px">
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="95%" CssClass="btn-ok" AllowMultiple="true" />
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button ID="btnAttch" runat="server" Text="Add" CssClass="btn-ok" />
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button ID="btnIndex" runat="server" Text="Index" CssClass="btn-ok" />
                        </div>
                    </div>


                    <div class="col-sm-12 col-md-12" runat="server">
                        <asp:GridView ID="gvattach" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="1%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPath" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField  HeaderStyle-Width="40%" HeaderText="File Name">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="lblFilename" runat="server" CommandName="OPENPAGE" Font-Bold="False"  Text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                                <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                </div>

            </div>
        </div>
    </div>


    <div id="myModalIndex" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Index Details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-12 col-md-12">
                            <div class="pull-left">
                                <asp:Label ID="Label1" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblcabinet" runat="server" Text="Cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblSubcabinet" runat="server" Text="Sub cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblFolder" runat="server" Text="Folder"></asp:Label>
                                <asp:DropDownList ID="ddlFolder" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblDocumentType" runat="server" Text="Document Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                <asp:Label ID="lblDateDisplay" runat="server" CssClass="aspxlabelbold"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>

                        </div>

                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>

                                    <asp:TemplateField HeaderStyle-Width="1%" HeaderText="DescriptorID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescriptorID" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DescriptorID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descriptor" HeaderText="Descriptor" HeaderStyle-Width="40%"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvKeywords" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Keywords" HeaderStyle-Width="100%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Key") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:ImageButton ID="imgbtnIndexSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index" />
                </div>
            </div>
        </div>
    </div>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

