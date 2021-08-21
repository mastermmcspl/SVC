<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="BankReconciliation.aspx.vb" Inherits="Accounts_BankReconciliation" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
         div {
            color: black;
        }
    </style>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />


    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScriptOnDataBounds/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgMatchedRows.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('#<%=dgBank.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('#<%=dgCbook.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        $('#<%=Uniquecompdt1.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $('#collapseRRIT').collapse({
            toggle: false
        })
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>
   
      <script lang="javascript" type="text/javascript">          
        function clientfunction() {

              if (document.getElementById('<%=ddlBank.ClientID %>').selectedIndex == 0) {
                 alert('Select Bank name.');
                 document.getElementById('<%=ddlBank.ClientID%>').focus()
                 return false
              }
            if (document.getElementById('<%=ddlBrnch.ClientID %>').selectedIndex == 0) {
                alert('Select Branch name.');
                 document.getElementById('<%=ddlBrnch.ClientID%>').focus()
                 return false
            }

              if (document.getElementById('<%=txtfrom.ClientID %>').value == "") {
                alert("Select From Date")
                document.getElementById('<%=txtfrom.ClientID %>').focus()
                return false;
            }
             
               if (document.getElementById('<%=txtto.ClientID %>').value == "") {
                alert("Select To Date")
                document.getElementById('<%=txtto.ClientID %>').focus()
                return false;
               }
             
        }

          function Compare() {

               <%-- if (document.getElementById('<%=FULoad.ClientID %>').valueOf.length == "") {
                alert('Select Browse option  to Attach File.');
                 document.getElementById('<%=FULoad.ClientID%>').focus()
                 return false
               }--%>

                if (document.getElementById('<%=ddlSheetName.ClientID %>').selectedIndex == -1) {
                alert('Select Sheet Name.');
                 document.getElementById('<%=ddlSheetName.ClientID%>').focus()
                 return false
                }
           
          }

           </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>Bank Reconciliation</b></h2>
            </div>
            <div class="pull-right col-sm-3 col-md-3">
                <div class="pull-right ">
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" Style="height: 16px" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="ImgbtnPrint" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="clearfix"></div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Reconciliation No."></asp:Label>
            <asp:DropDownList runat="server" AutoPostBack="true" CssClass="auto-style" Width="240px" ID="ddlExistinReconciliation"></asp:DropDownList>
        </div>

        <div class="col-sm-5 col-md-5">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft" ForeColor="Red"></asp:Label>
            <div class="col-sm-5 col-md-5">
                <asp:LinkButton ID="lnDown" runat="server">Download sample excel</asp:LinkButton>
            </div>
       </div>

        <div class="col-sm-3 col-md-3">
            <asp:LinkButton ID="BRSREport" Text="BRSReport" ForeColor="green" runat="server"></asp:LinkButton>
        </div>
        <br />

        <div class="col-sm-1 col-md-1">
            <asp:Button ID="BankManualAdd" Visible="false" runat="server" AutoPostBack="true" Text="BankBook" data-target="#myModal2"></asp:Button>
        </div>
    </div>


    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Bank Name"></asp:Label>
            <asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlBrnch" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label ID="lblFrom" runat="server" Text="From Date"></asp:Label>
            <asp:RequiredFieldValidator ID="reqfrom" runat="server" ControlToValidate="txtfrom" ValidationGroup="Validate" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regfrom" runat="server" ControlToValidate="txtfrom" ForeColor="#FF3300" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ValidationGroup="validate"></asp:RegularExpressionValidator>
            <asp:TextBox runat="server" autocomplete="off" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtfrom"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtfrom" PopupPosition=" BottomRight" TargetControlID="txtfrom" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label ID="lblto" runat="server" Text="To Date"></asp:Label>
            <asp:RequiredFieldValidator ID="reqto" runat="server" ControlToValidate="txtto" ValidationGroup="validate" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regto" runat="server" ControlToValidate="txtto" ForeColor="Red" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ValidationGroup="validate"></asp:RegularExpressionValidator>
            <asp:TextBox runat="server" autocomplete="off" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtto"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtto" PopupPosition=" BottomRight" TargetControlID="txtto" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
        </div>
        <div class="col-sm-2 col-md-2">
            <br />
            <%--<asp:Button CssClass="btn-ok" runat="server"  data-toggle="tooltip" data-placement="bottom" Text="Go" title="Go" ID="btnGo" />--%>
             <asp:Button  runat="server"  data-toggle="tooltip" data-placement="bottom" Text="Go" title="Go" ID="btnGo" />
        </div>
        
    </div>
    <div class="col-sm-3 col-md-3">
        <div class="form-group">
            <br />
            <asp:Label ID="lblSelectFile" runat="server" Text=""></asp:Label>
            <asp:FileUpload ID="FULoad" CssClass="aspxcontrols" runat="server" />
        </div>
        <asp:TextBox ID="txtPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" Style="height: 21px" />
    </div>
    <div class="col-sm-1 col-md-1">
        <div class="form-group">
            <div style="margin-top: 20px;"></div>
            <asp:Button ID="btnOk" runat="server" Text="OK"/>
        </div>
    </div>
    <div class="col-sm-4 col-md-4 pull-center" style="padding-right: 0">
        <div class="form-group">
            <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name"></asp:Label>

            <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-1 col-md-1">
        <div class="form-group">
            <br />
            <asp:Button ID="btnCompare" runat="server" Text="Compare" data-placement="bottom" AutoPostBack="true" CssClass="aspxcontrols" />
        </div>
    </div>
    <asp:TextBox ID="txtmasterID" runat="server" Visible="false"></asp:TextBox>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Label ID="lblErrorup" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    <asp:Label ID="lblErrorDown" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    <asp:Label ID="lblsrlno" runat="server" Visible="false"></asp:Label>



   <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
     <asp:Label runat="server" ID="lbl_CompBook" Text="Company Book :----" Style="height: 200px" forcolor="black" class="legendbold" Visible="false"> 
        </asp:Label>
</div>
    <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px; max-height: 500px; overflow: auto">
            <asp:GridView ID="dgCbook" runat="server" CssClass="footable" AutoGenerateColumns="False" ShowFooter="True">
                <Columns>
                    <asp:TemplateField HeaderText="SrNo">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "SrNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SerialNo">
                        <ItemTemplate>
                            <asp:Label ID="lblSerialNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "SerialNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ATD_TrType">
                        <ItemTemplate>
                            <asp:Label ID="lblTrtype" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ATD_TrType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                  
                    <asp:TemplateField HeaderText="Party" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblParty" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_Party") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction No">
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_TransactionNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ATD_TransactionDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:Label ID="lblChequeNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_ChequeNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cheque Date">
                        <ItemTemplate>
                            <asp:Label ID="lblChequeDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_ChequeDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IFSC Code">
                        <ItemTemplate>
                            <asp:Label ID="lblIFSCCode" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_IFSCCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Name">
                        <ItemTemplate>
                            <asp:Label ID="lblBankName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_BankName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch Name">
                        <ItemTemplate>
                            <asp:Label ID="lblBranchName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_BranchName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ATD_Debit") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <table>
                                <tr>
                                    <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                        <asp:Label ID="lblComDEbitSUM1" Text='<%# DataBinder.Eval(Container, "DataItem.ComDEbitSUM1") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                        <asp:Label ID="lblclosingBalComp" Text='<%# DataBinder.Eval(Container, "DataItem.closingBal_Comp") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ATD_Credit") %>'></asp:Label>
                        </ItemTemplate>                       
                          <FooterTemplate>                     
                         <asp:Label ID="lblCOmpCreditSUm1" Text='<%# DataBinder.Eval(Container, "DataItem.COmpCreditSUm1") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>                                                        
                    </FooterTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Voucher Type">
                        <ItemTemplate>
                            <asp:Label ID="BillTypelbl" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_BillType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="EditRow" runat="server" Text="Edit" data-toggle="modal" data-target="#MyCompBook" data-placement="bottom" AutoPostBack="true" CssClass="aspxcontrols" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
          </div>

        <%-- exist in company data only Uniquecompdt1--%>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px; overflow: auto":" visible="false">
            <asp:GridView ID="Uniquecompdt1" runat="server" CssClass="footable" AutoGenerateColumns="False" Visible="false" ShowFooter="True">
                <Columns>
                    <asp:TemplateField HeaderText="SrNo">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "SrNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SerialNo">
                        <ItemTemplate>
                            <asp:Label ID="lblSerialNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "SerialNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ATD_TrType">
                        <ItemTemplate>
                            <asp:Label ID="lblTrtype" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ATD_TrType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField HeaderText="Bill Type">
                        <ItemTemplate>
                            <asp:Label ID="lblbilltype" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_BillType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Party" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblParty" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_Party") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Transaction No">
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_TransactionNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Transaction Date">
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ATD_TransactionDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cheque No">
                        <ItemTemplate>
                            <asp:Label ID="lblChequeNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_ChequeNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cheque Date">
                        <ItemTemplate>
                            <asp:Label ID="lblChequeDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_ChequeDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IFSC Code">
                        <ItemTemplate>
                            <asp:Label ID="lblIFSCCode" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_IFSCCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bank Name">
                        <ItemTemplate>
                            <asp:Label ID="lblBankName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_BankName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Branch Name">
                        <ItemTemplate>
                            <asp:Label ID="lblBranchName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_BranchName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ATD_Debit") %>'></asp:Label>
                        </ItemTemplate>
                          <FooterTemplate>                      
                                    <asp:Label ID="lblComDEbitSUM1" Text='<%# DataBinder.Eval(Container, "DataItem.ComDEbitSUM1") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>                          
                    </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ATD_Credit") %>'></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate>                      
                                    <asp:Label ID="lblCOmpCreditSUm1" Text='<%# DataBinder.Eval(Container, "DataItem.COmpCreditSUm1") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>                          
                    </FooterTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Voucher Type">
                        <ItemTemplate>
                            <asp:Label ID="BillTypelbl" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Acc_PM_BillType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="EditRow" runat="server" Text="Edit" data-toggle="modal" data-target="#MyCompBook" data-placement="bottom" AutoPostBack="true" CssClass="aspxcontrols" />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>


     <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
     <asp:Label runat="server" ID="lbl_BankBook" Text="Bank Statement:-----" Style="height: 200px" forcolor="black" class="legendbold" Visible="false"> 
        </asp:Label>
</div>

        <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto"">
            <asp:GridView ID="dgBank" runat="server" CssClass="footable" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="SrNo" HeaderText="SrNo" HeaderStyle-Width="07%" Visible="false"></asp:BoundField>
                    <asp:TemplateField HeaderText="SerialNo">
                        <ItemTemplate>
                            <asp:Label ID="SerialNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "SerialNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TrType">
                        <ItemTemplate>
                            <asp:Label ID="TrType" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "TrType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TransactionNo">
                        <ItemTemplate>
                            <asp:Label ID="TransactionNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TxnDate">
                        <ItemTemplate>
                            <asp:Label ID="TxnDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "TxnDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ChequeNo">
                        <ItemTemplate>
                            <asp:Label ID="ChequeNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ChequeNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ChequeDate">
                        <ItemTemplate>
                            <asp:Label ID="ChequeDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ChequeDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Value date">
                        <ItemTemplate>
                            <asp:Label ID="lblBNKIFSCCODE" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "IFSCCODEBNK") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="IFSCCode">
                        <ItemTemplate>
                            <asp:Label ID="ValueDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ValueDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="BankName">
                        <ItemTemplate>
                            <asp:Label ID="BankName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "BankName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BranchName">
                        <ItemTemplate>
                            <asp:Label ID="BranchName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "BranchName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="Debit" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Debit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="Credit" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Credit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <asp:Label ID="Description" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="RefNo">
                        <ItemTemplate>
                            <asp:Label ID="RefNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "RefNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BranchCode">
                        <ItemTemplate>
                            <asp:Label ID="BranchCode" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "BranchCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Balance">
                        <ItemTemplate>
                            <asp:Label ID="Balance" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Balance") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DebitDiff" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="DabitDiff" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "DabitDiff") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CreditDiff" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="CreditDiff" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CreditDiff") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CompanyDebit" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCDebit" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CDebit") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCompDebitSum" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CompDebitSum") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="180px" Font-Bold="True"></asp:Label>
                        </FooterTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CompanyCredit" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCCredit" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CCredit") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblCompCreditSum" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CompCreditSum") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="180px" Font-Bold="True"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BankDebit" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBDebit" Visible="false" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "BDebit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BankCredit" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBCredit" Visible="false" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "BCredit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Voucher Type">
                        <ItemTemplate>
                            <asp:Label ID="lblbilltype1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                     <asp:TemplateField HeaderText="Opening Balance">
                        <ItemTemplate>
                            <asp:Label ID="lblOpeningBal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, " Opening_Bal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
   
    <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
        <br />
        <asp:Label runat="server" ID="lblReconcilation" Text="After Reconcilation" Style="height: 100px" class="legendbold" Visible="false"> 
        </asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;                                                        
              <asp:Label runat="server" ID="lblgreen" Style="height: 50px" class="green" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp; 
                  <asp:Label runat="server" Text="->Matched" Visible="false" ID="lblgreen1" class="legendbold"> </asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp; 
              <asp:Label runat="server" ID="lblRed" Style="height: 50px" class="Red" Visible="false"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp; 
                   <asp:Label runat="server" Text="->Not Matched" ID="lblRed1" Visible="false" class="legendbold"> </asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;                     
    </div>
    <style>
        .green {
            border: 3px outset green;
            background-color: green;
        }

        .Red {
            border: 3px outset red;
            background-color: red;
        }
    </style>



  <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
       <asp:Label runat="server" ID="lblGreenDesc" Text="ChequeNo,Debit,Credit all fields are matched then only Color Green will Appear" ForeColor ="Green" Style="height: 50px"  Visible="false"> 
        </asp:Label>
      <br/>
       <asp:Label runat="server" ID="lblRedDesc" Text="ChequeNo,Debit,Credit any one of the fields not matched then  Color Red will Appear" ForeColor ="red" Style="height: 50px"  Visible="false"> 
        </asp:Label>
      <br/>
      <asp:Label runat="server" ID="lblNoColr" Text="ChequeNo Not matched then only No Background Color will Appear" ForeColor="black" Style="height: 50px"  Visible="false"> 
        </asp:Label>
    </div>

  
    <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto">
        <asp:GridView ID="dgMatchedRows" runat="server" CssClass="footable" AutoGenerateColumns="False" ShowFooter="True">
            <Columns>
               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" CssClass="hvr-bounce-in" />
                    </ItemTemplate>                   
                </asp:TemplateField>--%>
                <asp:BoundField DataField="SrNo" HeaderText="SrNo" HeaderStyle-Width="07%" Visible="false"></asp:BoundField>
                <asp:TemplateField HeaderText="SerialNo">
                    <ItemTemplate>
                        <asp:Label ID="SerialNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "SerialNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TrType">
                    <ItemTemplate>
                        <asp:Label ID="TrType" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "TrType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TransactionNo">
                    <ItemTemplate>
                        <asp:Label ID="TransactionNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "TransactionNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TxnDate">
                    <ItemTemplate>
                        <asp:Label ID="TxnDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "TxnDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ChequeNo">
                    <ItemTemplate>
                        <asp:Label ID="ChequeNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ChequeNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ChequeDate">
                    <ItemTemplate>
                        <asp:Label ID="ChequeDate" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "ChequeDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="value date">
                    <ItemTemplate>
                        <asp:Label ID="lblBNKIFSCCODE" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "IFSCCODEBNK") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BankName">
                    <ItemTemplate>
                        <asp:Label ID="BankName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "BankName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BranchName">
                    <ItemTemplate>
                        <asp:Label ID="BranchName" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "BranchName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Debit">
                    <ItemTemplate>
                        <asp:Label ID="Debit" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Debit") %>'></asp:Label>
                    </ItemTemplate>                 
                    <FooterTemplate>
                        <table>
                            <tr>
                                <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                    <asp:Label ID="lblBNKDebitSum" Text='<%# DataBinder.Eval(Container, "DataItem.BNKDebitSum") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                    <asp:Label ID="lblDebitresult" Text='<%# DataBinder.Eval(Container, "DataItem.Debitresult") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit">
                    <ItemTemplate>
                        <asp:Label ID="Credit" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Credit") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <table>
                            <tr>
                                <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                    <asp:Label ID="lblBNKCreditSum" Text='<%# DataBinder.Eval(Container, "DataItem.BNKCreditSum") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                    <asp:Label ID="lblCreditresult" Text='<%# DataBinder.Eval(Container, "DataItem.Creditresult") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="Description" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RefNo">
                    <ItemTemplate>
                        <asp:Label ID="RefNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "RefNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BranchCode">
                    <ItemTemplate>
                        <asp:Label ID="BranchCode" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "BranchCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Balance">
                    <ItemTemplate>
                        <asp:Label ID="Balance" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Balance") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DebitDiff" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="DabitDiff" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "DabitDiff") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CreditDiff" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="CreditDiff" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CreditDiff") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CompanyDebit" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCDebit" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CDebit") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <table>
                            <tr>
                                <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                    <asp:Label ID="lblCompDebitSum" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CompDebitSum") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                    <asp:Label ID="lblCompDebitresult" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CompDebitresult") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CompanyCredit" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCCredit" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CCredit") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <table>
                            <tr>
                                <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                    <asp:Label ID="lblCompCreditSum" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CompCreditSum") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="1" style="border-right: #ccc 1px solid; text-align: right;">
                                    <asp:Label ID="lblCompCreditresult" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CompCreditresult") %>' runat="server" Style="text-align: right" CssClass="aspxcontrols" Width="100px" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BankDebit" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblBDebit" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "BDebit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="BankCredit" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblBCredit" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "BCredit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                   
                 <asp:TemplateField HeaderText="Voucher Type">
                        <ItemTemplate>
                            <asp:Label ID="lblbilltype1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BillType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:TemplateField HeaderText="Opening Balance">
                        <ItemTemplate>
                            <asp:Label ID="lblOpeningBal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Opening_Bal") %>'></asp:Label>
                        </ItemTemplate>                    
                    </asp:TemplateField>
                                    
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" ToolTip="view JE" CommandName="EditRow" runat="server" data-toggle="modal"  data-placement="bottom" AutoPostBack="true" CssClass="aspxcontrols" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:button ID="btnpost" CommandName="POST" text="POST" ToolTip="Post" runat="server" AutoPostBack="true" CssClass="aspxcontrols" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>

    <%--        showing only EAN status of --%>

    <div id="myModal" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Amount Adjustment</b></h4>
                </div>
                <div class="modal-body">
                    <%--  bank debit credit--%>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lbDebit" runat="server" Text="Debit"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lbCredit" runat="server" Text="Credit"></asp:Label>
                        </div>

                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtdebit" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtCredit" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                    <%--  company debit credit--%>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblcompdebit" runat="server" Text="Company Debit"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblcompcredit" runat="server" Text="Company Credit"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtcompdebit" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtCompCredit" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>


                    <%--  Difference of debit credit--%>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblbedidiff" runat="server" Text="Debit Difference"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblcredbdiff" runat="server" Text="Credit Difference"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtbedidiff" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtcredbdiff" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-4 col-md-4">
                                <asp:CheckBox ID="chkbxJE" runat="server" CssClass="hvr-bounce-in" TextAlign="left" />
                                <asp:Label runat="server" Text="Pass JE"></asp:Label>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Button ID="btnSave" runat="server" Text="Save" autopostback="true" data-target="#myModalBRS" CssClass="aspxcontrols"></asp:Button>
                                <asp:Label ID="lblBANKID" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblTrtypes" runat="server" Visible="false"></asp:Label>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Button ID="btnclose" runat="server" Text="close" CssClass="aspxcontrols"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%-- BRS Scenario lists--%>

    <div id="myModalBRS" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>After BRS Amount</b></h4>
                </div>
                <div class="modal-body">


                    <%--<div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lbmt" runat="server" Text="Matched"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lbnt" runat="server" Text="Not Matched"></asp:Label>
                        </div>
                    </div>

 <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtmthd" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtnthd" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>--%>

                    <%--       <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="Label1" runat="server" Text="Amount Not Reflected In Bank"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="Label2" runat="server" Text="Amount Not Reflected In Company"></asp:Label>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtntBooks" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtntComp" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>--%>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblb" runat="server" Text="Balance As Per Book"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblc" runat="server" Text="Balance As Per Bank"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lbladj" runat="server" Text="Adjusted Amount In Company "></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtBCbk" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtBBBK" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtAdjComp" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lbltotal" runat="server" Text="Total Balance in Company After Adjustment"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txttotal" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-sm-2 col-md-2">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:TextBox ID="txtJEID" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>

    <%--       Edit for Companybook--%>
    <div id="MyCompBook" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Company Book</b></h4>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblDesc" runat="server" Text="Description"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblChqueno" runat="server" Text="ChequeNo"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblCIFSCCode" runat="server" Text="IFSCCode"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtCheque" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtifsc" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblTrnDate" runat="server" Text="TransactionDate"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblParty" runat="server" Text="PartyCode"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtTrnDate" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtTrnDate" PopupPosition=" BottomRight" TargetControlID="txtTrnDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtParty" runat="server" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>


                    <div class="modal-footer">
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-4 col-md-4">
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Button ID="btnCompUpdate" Text="Update" runat="server" autopostback="true" CssClass="aspxcontrols"></asp:Button>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Button ID="btnClose1" Text="Close" runat="server" CssClass="aspxcontrols"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--///Report--%>

    <div id="myModal1" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Print Report</b></h4>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <ul class="nav navbar-nav navbar-right logoutDropdown">
                                <li class="dropdown">
                                    <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                        <asp:ImageButton class="dropdown-toggle hvr-bounce-out" ID="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" ImageUrl="~/Images/Download24.png" title="Matched Rows" Visible="false" /></span></a>
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
                        <div class="col-sm-4 col-md-4">
                            <ul class="nav navbar-nav navbar-right logoutDropdown">
                                <li class="dropdown">
                                    <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                        <asp:ImageButton class="dropdown-toggle hvr-bounce-out" ID="ImageButton1" runat="server" ImageUrl="~/Images/Download24.png" data-toggle="tooltip" data-placement="top" title="Un Matched" Visible="false" /></span></a>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkbtnNotmatched_PDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                        <li role="separator" class="divider"></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkbtnNotmatched_Excel" Text="Download Excel" Style="margin: 0px;" /></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <ul class="nav navbar-nav navbar-right logoutDropdown">
                                <li class="dropdown">
                                    <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                        <asp:ImageButton class="dropdown-toggle hvr-bounce-out" ID="ImageButton2" runat="server" ImageUrl="~/Images/Download24.png" data-toggle="tooltip" data-placement="top" title="Not Exist" Visible="false" /></span></a>
                                    <ul class="dropdown-menu">
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkbtn_notexit_excel" Text="Download Excel" Style="margin: 0px;" /></li>
                                        <li role="separator" class="divider"></li>
                                        <li>
                                            <asp:LinkButton runat="server" ID="lnkbtn_notexit_Pdf" Text="Download PDF" Style="margin: 0px;" /></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblcmpbk" runat="server" Text="CompanyBook" class="legendbold"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblBnkbk" runat="server" Text="BankBook" class="legendbold"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:DropDownList ID="ddlCbook" runat="server" AutoPostBack="false" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:DropDownList ID="ddlBbook" runat="server" AutoPostBack="false" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-sm-6 col-md-6">
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Button ID="btnGenerate" runat="server" AutoPostBack="True" Text="Generate" CssClass="aspxcontrols"></asp:Button>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Button ID="btnCls" runat="server" Text="Close" CssClass="aspxcontrols"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="myModal2" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">               
          <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Manual Entry for BankBook</b></h4>
                </div>
                <div class="modal-body">               
                     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblSlnoBK1" runat="server" Text="SlNo"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblTrtypeBK1" runat="server" Text="transaction Type"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblTrNoBK1" runat="server" Text="TransactionNo"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtSlnoBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtTrtypeBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtTrNoBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblTrnDteBK1" runat="server" Text="TransactionDate"></asp:Label>                             
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblchenoBK1" runat="server" Text="ChequeNo"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblchqdtBK1" runat="server" Text="Cheque Date"></asp:Label>
                        </div>
                    </div>
                       <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtTrnDteBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                             <cc1:CalendarExtender ID="ccTrnDteBK1" runat="server" PopupButtonID="txtTrnDteBK1" PopupPosition="BottomLeft" TargetControlID="txtTrnDteBK1" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtchenoBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtchqdtBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
   <cc1:CalendarExtender ID="txtchqdtBK1_CalendarExtender4" runat="server" PopupButtonID="txtchqdtBK1" PopupPosition="BottomLeft" TargetControlID="txtchqdtBK1" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>

                        </div>
                    </div>

                     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblvalDteBK1" runat="server" Text="Value Date"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblBankNBK1" runat="server" Text="Bank Name"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblBrnnBK1" runat="server" Text="Branch Name"></asp:Label>
                        </div>
                    </div>
                     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtvalDteBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                             <cc1:CalendarExtender ID="txtvalDteBK1_CalendarExtender4" runat="server" PopupButtonID="txtvalDteBK1" PopupPosition="BottomLeft" TargetControlID="txtvalDteBK1" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtBankNBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtBrnnBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lbldebitBK1" runat="server" Text="Debit"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblcrebitBK1" runat="server" Text="Credit"></asp:Label>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblDescBK1" runat="server" Text="Description"></asp:Label>
                        </div>
                    </div>
                     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtdebitBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtcrebitBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:TextBox ID="txtDescBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                      <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblRefBK1" runat="server" Text="Reference"></asp:Label>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblbrncodBK1" runat="server" Text="Branch Code"></asp:Label>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblBalBK1" runat="server" Text="Balance"></asp:Label>
                        </div>                      
                          <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblvtypBK1" runat="server" Text="VoucherType"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtRefBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                       <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtbrncodBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                       <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtBalBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtvtypBK1" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblOpengBal" runat="server" Text="Opening Balance"></asp:Label>
                        </div>
                    </div>
                     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtOpeningBal" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
                        </div>
                          </div>

                    <div class="modal-footer">
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-02 col-md-02">
                                <asp:Button ID="btnSAveManualBank" runat="server" Text="Save" autopostback="true"></asp:Button>
                            </div>
                            <div class="col-sm-02 col-md-02">
                                <asp:Button ID="btnokclose" runat="server" Text="Ok" CssClass="btn-ok" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            </div>
     
    <asp:TextBox ID="txtboxCDebit" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtboxCredit" runat="server" Visible="false"></asp:TextBox>    
     <asp:TextBox ID="txtclosingBal" runat="server" Visible="false"></asp:TextBox>    
  
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
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>

    <div id="ModalFASBankValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCustomerValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button2">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>




