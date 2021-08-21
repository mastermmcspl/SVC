<%@ Page Language="VB" MasterPageFile="~/Master.master" AutoEventWireup="false" CodeFile="CustomerMasterDetails.aspx.vb" Inherits="Masters_CustomerMasterDetails" ValidateRequest="false" %>

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
            $('#<%=ddlExistingCustomer.ClientID%>').select2();
            $('#<%=ddlCity.ClientID%>').select2();
            $('#<%=ddlCategory.ClientID%>').select2();
            $('#<%=ddlState.ClientID%>').select2();
         });

        function Validate() {
            if (document.getElementById('<%=txtSupplierName.ClientID %>').value == "") {
                alert('Enter Supplier Name.');
                document.getElementById('<%=txtSupplierName.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtConatctPerson.ClientID %>').value == "") {
                alert('Enter Contact Person.');
                document.getElementById('<%=txtConatctPerson.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlCategory.ClientID %>').selectedIndex == 0) {
                 alert('Select Category.');
                 document.getElementById('<%=ddlCategory.ClientID%>').focus()
                 return false
            }
            if (document.getElementById('<%=ddlCategory.ClientID %>').selectedIndex > 0) {
                var ddlPT = document.getElementById("<%=ddlCategory.ClientID %>");
                var ddlPaymentText = ddlPT.options[ddlPT.selectedIndex].innerHTML;
                if (ddlPaymentText == "Customer") {
                    
                }
                else {
                    if (document.getElementById('<%=ddlCompanyType.ClientID %>').selectedIndex == 0) {
                    alert('Select Company Type.');
                    document.getElementById('<%=ddlCompanyType.ClientID%>').focus()
                    return false;
                    }
                    if (document.getElementById('<%=ddlGSTCategory.ClientID %>').selectedIndex == 0) {
                    alert('Select GSTN Category.');
                    document.getElementById('<%=ddlGSTCategory.ClientID%>').focus()
                    return false;
                    }                   
                    if (document.getElementById('<%=ddlGSTCategory.ClientID %>').selectedIndex > 0) {
                var ddlPT = document.getElementById("<%=ddlGSTCategory.ClientID %>");
                var ddlPaymentText = ddlPT.options[ddlPT.selectedIndex].innerHTML;
                if (ddlPaymentText == "UNRIGISTERED DEALER") {
                    
                }
                else {
                                       
                    var GSTIN = /^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[0-9]{1}Z[(a-z)(A-Z)(0-9)]{1}?$/
                    if (document.getElementById('<%=txtGSTNRegNo.ClientID %>').value == "") {
                        alert('Enter GSTNRegNo No.');
                        document.getElementById('<%=txtGSTNRegNo.ClientID%>').focus()
                        return false;
                    }
                    if (document.getElementById('<%=txtGSTNRegNo.ClientID %>').value != "") {               
                    var num
                    num = GSTIN.test(document.getElementById('<%=txtGSTNRegNo.ClientID %>').value)
                    if (num == false) {
                    alert("Enter Valid Delivery From GSTN Reg.No(Only 15 Charecters, Two integers,Five Capital alphabets, Four integers, One Capital alphabet, One integer, then Capital Z, One Integer/Alphabet).")
                    document.getElementById('<%=txtGSTNRegNo.ClientID %>').value = ""
                    document.getElementById('<%=txtGSTNRegNo.ClientID %>').focus()
                    return false
                    }               

                    }

                }

            }

                }

            }
            

        }
    </script>

       <style>        
                div{
            color:black;
                      }        
        </style>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>2.4 Customer Master Details</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save"  ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateCustomer" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <%--<asp:ImageButton ID="imgbtnDelete" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" ValidationGroup="Validate" />--%>
                    <%--<asp:Button ID ="btnDelete" Text ="Delete" runat ="server" />--%>
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
            <asp:Label runat="server" Text="Existing Customer Name"></asp:Label> 
            <asp:DropDownList ID="ddlExistingCustomer" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
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
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Customer Name"></asp:Label> 
            <asp:TextBox ID="txtSupplierName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomerName" runat="server" ControlToValidate="txtSupplierName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Customer Name" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSupplierName" runat="server" ControlToValidate="txtSupplierName" Display="Dynamic" ErrorMessage="Enter Valid Customer Name.(Maximum size 100)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$"></asp:RegularExpressionValidator>--%>
        </div>        
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Category"></asp:Label>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="aspxcontrols" AutoPostBack ="true" ></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Customer Code"></asp:Label>
            <asp:TextBox ID="txtSupplierCode" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomerCode" runat="server" ControlToValidate="txtSupplierCode" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Customer Code" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSupplierCode" runat="server" ControlToValidate="txtSupplierCode" Display="Dynamic" ErrorMessage="Enter Valid Customer Code.(Maximum size 100)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9-\s]{1,100}$"></asp:RegularExpressionValidator>--%>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Contact Person"></asp:Label>
            <asp:TextBox ID="txtConatctPerson" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
           <%-- <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVContactPerson" runat="server" ControlToValidate="txtConatctPerson" Display="Dynamic" ErrorMessage="Enter Valid Contact Person." SetFocusOnError="True" ValidationExpression="^[a-zA-Z\s]{1,100}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Enter Valid Email" SetFocusOnError="True" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Mobile No"></asp:Label>
            <asp:TextBox ID="txtMobile" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMobile" runat="server" ControlToValidate="txtMobile" Display="Dynamic" ErrorMessage="Enter Valid Mobile No." SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,10}$"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="LandLine"></asp:Label>
            <asp:TextBox ID="txtLandLine" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVLandLine" runat="server" ControlToValidate="txtLandLine" Display="Dynamic" ErrorMessage="Enter Valid LandLine No." SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,15}$"></asp:RegularExpressionValidator>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Fax No"></asp:Label>
            <asp:TextBox ID="txtFax" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFax" runat="server" ControlToValidate="txtFax" Display="Dynamic" ErrorMessage="Enter Valid FAX." SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,12}$"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="PinCode"></asp:Label>
            <asp:TextBox ID="txtPinCode" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPinCode" runat="server" ControlToValidate="txtPinCode" Display="Dynamic" ErrorMessage="Enter Valid PinCode." SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,6}$"></asp:RegularExpressionValidator>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="City"></asp:Label>
            <asp:DropDownList ID="ddlCity" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="State"></asp:Label>
            <asp:DropDownList ID="ddlState" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Address"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
             <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddress" runat="server" ControlToValidate="txtAddress" Display="Dynamic" ErrorMessage="Address Maximum size 1000" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9/'@&#.,\s]{1,1000}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Address1"></asp:Label>
            <asp:TextBox ID="txtAddress1" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
             <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddress1" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="Address1 Maximum size 1000" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9/'@&#.,\s]{1,1000}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Address2"></asp:Label>
            <asp:TextBox ID="txtAddress2" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
             <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddress2" runat="server" ControlToValidate="txtAddress2" Display="Dynamic" ErrorMessage="Address2 Maximum size 1000" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9/'@&#.,\s]{1,1000}$"></asp:RegularExpressionValidator>--%>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Address3"></asp:Label>
            <asp:TextBox ID="txtAddress3" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
             <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddress3" runat="server" ControlToValidate="txtAddress3" Display="Dynamic" ErrorMessage="Address3 Maximum size 1000" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9/'@&#.,\s]{1,1000}$"></asp:RegularExpressionValidator>--%>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">      
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Company Type"></asp:Label>
            <asp:DropDownList ID="ddlCompanyType" AutoPostBack ="true"  runat="server" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="GSTN Category"></asp:Label>
            <asp:DropDownList ID="ddlGSTCategory" runat="server" AutoPostBack ="true"  CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="GSTN Register No"></asp:Label>
            <asp:TextBox ID="txtGSTNRegNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass = "ErrorMsgRight" ID="RFVGSTNRegNo" runat="server" ControlToValidate="txtGSTNRegNo" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter GSTN Reg.No" ValidationGroup="ValidateCustomer"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVGSTNRegNo" runat="server" ValidationGroup="ValidateCustomer" ControlToValidate="txtGSTNRegNo" Display="Dynamic" ErrorMessage="Enter Valid GSTN Reg.No." SetFocusOnError="True" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(a-z)(A-Z)(0-9)]{1}"></asp:RegularExpressionValidator>--%>
        </div>
    </div>


    <div class="col-md-12">
        <div id="divcollapseTAXDetails" runat="server" visible="false" data-toggle="collapse" data-target="#collapseTAXDetails"><a href="#"><b><i>Click here to Create Custom fields such as VAT, TAX, PAN, TAN, TIN, CIN and others...</i></b></a></div>
    </div>

    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseTAXDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group">
                <asp:GridView ID="dgOtherDetails" runat="server" Visible="true" AutoGenerateColumns="False" class="footable">
                    <Columns>
                                <asp:TemplateField HeaderText="sID" Visible="false" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lblsID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Statutory Name" HeaderStyle-Width="46%">
                            <ItemTemplate>
                                <asp:Label ID="lblStatutoryName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Statutory Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Statutory Value" HeaderStyle-Width="46%">
                            <ItemTemplate>
                                <asp:Label ID="lblStatutoryValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Statutory Value") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ToolTip="Delete" ImageUrl="~/Images/Trash16.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="StatutoryName"></asp:Label>
                    <asp:TextBox ID="txtStatutoryName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStatutoryName" runat="server" ControlToValidate="txtStatutoryName" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Statutory Name" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStatutoryName" runat="server" ControlToValidate="txtStatutoryName" Display="Dynamic" ErrorMessage="Enter Valid Statutory Name.(Maximum size 3)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z\s]{1,3}$"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="StatutoryValue"></asp:Label>
                    <asp:TextBox ID="txtStatutoryValue" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStatutoryValue" runat="server" ControlToValidate="txtStatutoryValue" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Statutory Value" ValidationGroup="ValidateAdd"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStatutoryValue" runat="server" ControlToValidate="txtStatutoryValue" Display="Dynamic" ErrorMessage="Enter Valid Statutory Value.(Maximum size 50)" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,50}$"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-3 col-md-3">
                    <br />
                    <asp:Button ID="btnStatutoryAdd" runat="server" Text="Add" CssClass="btn-ok" ValidationGroup="ValidateAdd" />
                </div>
            </div>

        </div>
    </div>

    <div class="col-md-12">
        <div id="divBankDetails" runat="server" visible="false" data-toggle="collapse" data-target="#collapseBankDetails"><a href="#"><b><i>Click here to create Bank Details...</i></b></a></div>
    </div>
    <div class="col-md-12" style="padding-left: 0px">
        <div id="collapseBankDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group">
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label runat="server" Text="Account No"></asp:Label>
                    <asp:TextBox ID="txtAccountNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>                    
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAccountNo" runat="server" ControlToValidate="txtAccountNo" Display="Dynamic" ErrorMessage="Enter Valid Account No" SetFocusOnError="True" ValidationExpression="^[0-9\s]{1,15}$"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Bank Name"></asp:Label>
                    <asp:TextBox ID="txtBankName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>                    
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBankName" runat="server" ControlToValidate="txtBankName" Display="Dynamic" ErrorMessage="Enter Valid Bank Name" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,50}$"></asp:RegularExpressionValidator>
                </div>  
                <div class="col-sm-4 col-md-4">

                </div>              
            </div>
            <div class="col-sm-12 col-md-12 form-group">
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label runat="server" Text="IFSC Code"></asp:Label>
                    <asp:TextBox ID="txtIFSCCode" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>                    
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFSCCode" runat="server" ControlToValidate="txtIFSCCode" Display="Dynamic" ErrorMessage="Enter Valid IFSCCode" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,15}$"></asp:RegularExpressionValidator>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Branch Name"></asp:Label>
                    <asp:TextBox ID="txtBranchName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>                    
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBranchName" runat="server" ControlToValidate="txtBranchName" Display="Dynamic" ErrorMessage="Enter Valid BranchName" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s]{1,50}$"></asp:RegularExpressionValidator>
                </div>                
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnBankSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ImageUrl = "~/Images/Save16.png" ValidationGroup="ValidateBank" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group">
                <asp:DataGrid ID="dgBankDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="75%" class="footable">
                    <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                    <PagerStyle CssClass="gripagination" Mode="NumericPages" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top"/>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="AccountNo" HeaderText="Account No">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="BankName" HeaderText="Bank Name">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="IFSCCode" HeaderText="IFSC Code">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:BoundColumn DataField="BranchName" HeaderText="Branch Name">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundColumn>

                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Edit" CommandName="Edit" data-placement="bottom" ImageUrl ="~\Images\Edit16.png" runat="server"/>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="Delete" ImageUrl ="~\Images\4delete.gif" data-placement="bottom" runat="server"/>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateColumn>

                    </Columns>
                </asp:DataGrid>
            </div>

        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group">
        <asp:TextBox ID ="txtBankID" runat ="server" Visible ="false" ></asp:TextBox>
    </div> 
    
    <div id="ModalFASCompanyValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
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

