<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" EnableEventValidation="false" CodeFile="BankReconciliationMaster.aspx.vb" Inherits="Accounts_BankReoconcilationMaster" %>

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

        .auto-style1 {
            font-size: 12px;
            color: #a94442;
            float: right;
            font-family: "Segoe UI", sans-serif;
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
            padding: 3px;
            background-color: #fff;
            background-image: none;
        }
         div {
            color: black;
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
            $('#<%=dgBankReconcilation.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-4 col-md-4 pull-left">
                <h2><b>Bank Reconcilation Dashboard</b></h2>
            </div>
            <div class="col-sm-5 col-md-5" style="visibility: hidden">
                <asp:Label ID="lblHeadingSearch" Text="Search by" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlSearch" runat="server" CssClass="aspxcontrols" Width="140px">
                </asp:DropDownList>
                <asp:TextBox autocomplete="off" ID="txtSearch" runat="server" Width="140px" CssClass="aspxcontrols" />
                <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
                <asp:RequiredFieldValidator CssClass="auto-style1" ID="RFVSearch" runat="server" ErrorMessage="Select Search by." SetFocusOnError="True" ControlToValidate="ddlSearch" ValidationGroup="Search" Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />                                 
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
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-4 col-md-4 divmargin" style="padding-left: 0px">
            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="auto-style2" Width="275px">
            </asp:DropDownList>
        </div>
        <div class="col-sm-8 col-md-8 divmargin" style="padding-right: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div id="div1" runat="server" style="overflow-y: auto; width: 100%;"> <%--gridview overflow prevention code--%>
        <asp:GridView ID="dgBankReconcilation" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                    </HeaderTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>                    
                   <asp:Label ID="BnkID" Visible="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BnkID") %>'></asp:Label>
                         </ItemTemplate>
                </asp:TemplateField>             
     <asp:TemplateField HeaderText="SrNo">
                        <ItemTemplate>
                            <asp:Label ID="SrNo" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "SrNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                
         <asp:TemplateField HeaderText="BankName">
                        <ItemTemplate>
                            <asp:Label ID="BankName" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "BankName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
      
                <asp:TemplateField HeaderText="fromDate">
                        <ItemTemplate>
                            <asp:Label ID="fromDate" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "fromDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Todate">
                        <ItemTemplate>
                            <asp:Label ID="Todate" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Todate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                
                    <asp:TemplateField HeaderText="VoucherType">
                        <ItemTemplate>
                            <asp:Label ID="TrType" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "TrType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                      
              <asp:TemplateField HeaderText="VoucherNo">
                        <ItemTemplate>                          
              <%--<asp:Label ID="SerialNo" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "SerialNo") %>'></asp:Label>--%>
              <asp:linkbutton ID="lnkSerialNo" class="green" CommandName="ShowDetails" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "SerialNo") %>'></asp:linkbutton>
                        </ItemTemplate>
                    </asp:TemplateField>             
                  
                      <asp:TemplateField HeaderText="ChequeDate">
                        <ItemTemplate>
                            <asp:Label ID="ChequeDate" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "ChequeDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                   <asp:TemplateField HeaderText="ValueDate">
                        <ItemTemplate>
                            <asp:Label ID="ABR_ValueDate" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "ABR_ValueDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                      <asp:TemplateField HeaderText="Debit">
                        <ItemTemplate>
                            <asp:Label ID="lblDebit" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Debit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Credit">
                        <ItemTemplate>
                            <asp:Label ID="lblCredit" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "Credit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                   <asp:TemplateField HeaderText="BDabit">
                        <ItemTemplate>
                            <asp:Label ID="lblCDabit" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "CDabit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                           <asp:TemplateField HeaderText="BCredit">
                        <ItemTemplate>
                            <asp:Label ID="lblCCredit" runat="server" Visible="true" Text='<%# DataBinder.Eval(Container.DataItem, "CCredit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                        

                 <asp:TemplateField HeaderText="Remark" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRemark" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Remark") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                  <asp:TemplateField HeaderText="chequeNo">
                        <ItemTemplate>
                            <asp:Label ID="lblchequeno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lblchequeno") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                   <asp:TemplateField HeaderStyle-HorizontalAlign="Left" visible="false" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" ToolTip="view JE"  visible="false" CommandName="EditRow" runat="server"  data-toggle="modal"  data-placement="bottom" AutoPostBack="true" CssClass="aspxcontrols" />
                    </ItemTemplate>
                </asp:TemplateField> 
            </Columns>
        </asp:GridView>

    </div>
    </div>
     <style>
          .green{ 
             
              text-decoration: underline green;
          }         
           </style>


<asp:textbox ID="txtboxCDebit" runat="server"  visible="false"></asp:textbox>
<asp:textbox ID="txtboxCredit" runat="server"  visible="false"></asp:textbox>

      <div id="myModal" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title"><b>Amount Adjustment for book</b></h4>
                    </div>
                    <div class="modal-body">                
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>                      
                        </div>
                      <div class="col-sm-4 col-md-4">
                              <asp:Label ID="lblDebit" runat="server" Text="Debit"></asp:Label>
                           </div>
                      <div class="col-sm-4 col-md-4">
                           <asp:Label ID="lblCredit" runat="server" Text="Credit"></asp:Label>                      
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
   <div class="col-sm-4 col-md-4">                 
                             <asp:textbox ID="txtDescription" runat="server" CssClass="aspxcontrols" TextMode="MultiLine"  height="50px"></asp:textbox>
                        </div>
       <div class="col-sm-4 col-md-4">
                          <asp:textbox ID="txtdebit" runat="server" CssClass="aspxcontrolsdisable" MaxLength="50"></asp:textbox>                           
                        </div>
           <div class="col-sm-4 col-md-4">                 
                           <asp:textbox ID="txtCredit" runat="server" CssClass="aspxcontrolsdisable" MaxLength="50"></asp:textbox>
                        </div>    
                    </div>
                   <div class="modal-footer">
 <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
         <div class="col-sm-4 col-md-4">
        <asp:CheckBox ID="chkbxJE" runat="server" CssClass="hvr-bounce-in" TextAlign="left"/>
        <asp:Label runat="server" Text="Pass JE"></asp:Label>
  </div>
      <div class="col-sm-4 col-md-4">
            <asp:button  ID="btnSave" runat="server" Text="Save" autopostback="true" CssClass="aspxcontrols"></asp:button>
           <asp:Label ID="lblBANKID" runat="server"   visible="false"></asp:Label>
  </div>
       <div class="col-sm-4 col-md-4">
        <asp:button ID="btnclose" runat="server" Text="close" CssClass="aspxcontrols"></asp:button>
          </div>   
  </div>


      </div>
                </div>
            </div>
        </div>
             </div>
    <div id="ModalEmpMasterValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblEmpMasterValidationMsg" runat="server"></asp:Label></strong>
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
