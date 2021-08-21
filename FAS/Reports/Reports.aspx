<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="Reports.aspx.vb" Inherits="Reports_Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
         div {
            color: black;
        }
    </style>
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

    <link href="../StyleSheet/buttons.1.4.2.dataTables-min.css" rel="stylesheet" />

    <script src="../JavaScripts/buttons-1.4.2-js-dataTables-buttons.js"></script>
    <script src="../JavaScripts/ajax-libs-jszip-2.5.0-jszip-min.js"></script>
    <script src="../JavaScripts/bpampuch-pdfmake-0.1.18-build-pdfmake-min.js"></script>
    <script src="../JavaScripts/bpampuch-pdfmake-0.1.18-build-vfs_fonts.js"></script>
    <script src="../JavaScripts/buttons-1.2.2-js-buttons.html5-min.js"></script>
    <script src="../JavaScripts/fixedcolumns-3.2.6-js-dataTables.fixedColumns-min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=GvPettyCashDetailsReport.ClientID%>').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'excel',
                ],
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });
    </script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlParty.ClientID%>').select2();
            $('#<%=ddlPBillNo.ClientID%>').select2();
            $('#<%=ddlGL.ClientID%>').select2();
            $('#<%=ddlPVoucherNo.ClientID%>').select2();
            $('#<%=ddlSubGL.ClientID%>').select2();
        });

        function Validate() {
            if (confirm("Are You Sure, You Want To Freez?.")) {
                return true
            }
            else {
                return false
            }
        }
    </script>

    <%-- <script type="text/javascript">
  document.attachEvent('onkeyup', KeysShortcut);

// Now we need to implement the KeysShortcut
function KeysShortcut ()
{
    if (event.keyCode == 49)
    {
      document.getElementById('<%= btnGo.ClientID %>').click();
    }
}
  </script>--%>


    <%--<script >
        var isCtrl = false;
        document.onkeyup = function (e) {
            if (e.which == 17) isCtrl = false;
        };document.onkeydown=function(e){
            if(e.which == 17) isCtrl=true;
            if(e.which == 13 && isCtrl == true) {
                alert('Keyboard shortcuts are cool!');
                return false;
            }
        }
    </script>--%>

    <%-- <script >
        var isCtrl = false;
        document.attachEvent('onkeyup', KeyUpHandler);
        document.attachEvent('onkeydown', KeyDownHandler);

        function KeyUpHandler()
        {
            if (event.keyCode == 83) {
                __doPostBack('btnGo', 'OnClick');
            }
        }

        function KeyDownHandler()
        {
            if (event.keyCode == 83)
            {
                __doPostBack('btnGo', 'OnClick');                 
            }
            else if (event.keyCode == 67 && isCtrl == true)
            {
                 document.getElementById(btnClose).click();
            }
        }
    </script>
    
    <script src="//code.jquery.com/jquery-2.1.4.min.js"></script>
	<script src="keyboardShortcut.js"></script>--%>

    <%-- <script>
        $(document).ready(function(){
            $(document).keydown(function( e ){

                keyboardShortcut(
                  {
                      selector: e,
                      key: 'a'
                  },function() {
                      alert('Alerta do atalho')
                  }
                ) //close keyboardShortcut
            }) //close keydown
        }) 
    </script>--%>


    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Reports</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <%-- <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnWord" Text="Download Word" Style="margin: 0px;" /></li>
                            </ul>
                        </li>--%>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblMesg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Types of Reports" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlReports" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-sm-9 col-md-9">
            <asp:Label runat="server" Text="Select Duration" Font-Bold="true"></asp:Label>
            <asp:RadioButtonList ID="rbtDuration" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="850px" AutoPostBack="True">
                <asp:ListItem Text=" Yearly" Value="1"></asp:ListItem>
                <asp:ListItem Text=" Half Yearly" Value="2"></asp:ListItem>
                <asp:ListItem Text=" Quarterly" Value="3"></asp:ListItem>
                <asp:ListItem Text=" Monthly" Value="4"></asp:ListItem>
                <asp:ListItem Text=" Weekly" Value="5"></asp:ListItem>
                <asp:ListItem Text=" Customised" Value="6"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </div>
 
   
    <asp:Panel ID="PnlDurationMonthly" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Months" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlDurationmonth" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
     <asp:Panel ID="PnlWeekly" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Weeks" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlDurationweek" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlQuarterly" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Quarterly" Font-Bold="true" ></asp:Label>
            <asp:DropDownList ID="ddlDurationQuarter" runat="server" CssClass="aspxcontrols" AutoPostBack="true" >
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlHalfYearly" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Half-yearly" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlDurationhalfyear" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlyear" runat="server" Visible="false">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Financial-year" Font-Bold="true" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlDurationYear" runat="server" CssClass="aspxcontrols" Visible="false" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </asp:Panel>

    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px;">

        <asp:Panel ID="pnlBankDaybook" runat="server">
            <div class="col-sm-2 col-md-2">
                <asp:Label ID="lblFromdate" runat="server" Text="* From Date" Font-Bold="true"></asp:Label>

                <asp:TextBox ID="txtFromDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtFromDate" PopupPosition="BottomLeft" TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label ID="lblTodate" runat="server" Text="* To Date" Font-Bold="true"></asp:Label>
                <asp:TextBox ID="txtToDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                <cc1:CalendarExtender ID="cclToDate" runat="server" PopupButtonID="txtToDate" PopupPosition="BottomLeft" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
            </div>
            <div class="col-sm-1 col-md-1">
                <br />
                <asp:Button ID="btnGo" runat="server" Text="GO" Font-Bold="true" Visible="false" />
            </div>
            </asp:Panel>
            <asp:Panel ID="pnlfreeze" runat="server">
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="Label1" runat="server" Text="* Report Duration" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ddlFRReport" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-1 col-md-1">
                <br />
                <asp:Button ID="btnFreeze" Text="Freeze" runat="server" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlPurchase" runat="server">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Existing Purchase Voucher" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ddlExistPurchase" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSales" runat="server">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Existing Sales Voucher" Font-Bold="true"></asp:Label>
                <asp:DropDownList ID="ddlExistSales" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
            </div>
        </asp:Panel>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Zone" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Region" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Area" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>
    <asp:Panel ID="PnlMonth" runat="server">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
           
                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
                </asp:DropDownList>
            
            <div class="col-sm-3 col-md-3">
                <asp:DropDownList ID="ddlCustomerParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                </asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Button CssClass="btn-ok" runat="server" data-toggle="tooltip" data-placement="bottom" Text="OK" title="OK" ID="btnOk" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="PnlPayment" runat="server">
        <%--<div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Bill From Date"></asp:Label>
                    <asp:TextBox ID="txtPBillFromDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="ccPBillFromDate" runat="server" PopupButtonID="txtPBillFromDate" PopupPosition="BottomLeft" TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Bill To Date"></asp:Label>
                    <asp:TextBox ID="txtPBillToDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                     <cc1:CalendarExtender ID="ccPBillToDate" runat="server" PopupButtonID="txtPBillToDate" PopupPosition="BottomLeft" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
                </div>

                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Payment From Date"></asp:Label>
                    <asp:TextBox ID="txtPPaymentFromDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="ccPPaymentFromDate" runat="server" PopupButtonID="txtPPaymentFromDate" PopupPosition="BottomLeft" TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
                </div>

                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Payment To Date"></asp:Label>
                    <asp:TextBox ID="txtPPaymentToDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="ccPPaymentToDate" runat="server" PopupButtonID="txtPPaymentToDate" PopupPosition="BottomLeft" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
                </div>
            </div>
        </div>--%>

        <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Bill No" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlPBillNo" runat="server" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Voucher No" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlPVoucherNo" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label ID="lblvouchertype" runat="server" Text="Voucher Type" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlRVoucherType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <%--<div class="col-sm-3 col-md-3">
                     <asp:Button CssClass="btn-ok" runat="server" data-toggle="tooltip" data-placement="bottom" Text="OK" title="OK" ID="btnPayment" />   
                </div> --%>
            </div>
        </div>
    </asp:Panel>


    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <asp:Panel ID="pnlGL" runat="server">
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="General Ledger" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlGL" runat="server" CssClass="aspxcontrols" Width="70%" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="Sub General Ledger" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlSubGL" runat="server" CssClass="aspxcontrols" Width="70%" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </asp:Panel>
        </div>
    </div>
    <%-- <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
          <div class="col-sm-12 col-md-12 col-lg-12" style="padding-left: 0px">
        <asp:GridView ID="GvPettyCashDetailsReport" runat="server" AutoGenerateColumns="True" Class="footable">
            <Columns>
           </Columns>
                 </asp:GridView>
    </div>
        </div>--%>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px; overflow-y: scroll; overflow-x: scroll;">
        <asp:GridView ID="GvPettyCashDetailsReport" runat="server" AutoGenerateColumns="True" CssClass="footable" Width="100%" class="footable" ShowHeaderWhenEmpty="true">
            <Columns>
                <asp:TemplateField FooterText="" Visible="false"></asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>
</asp:Content>