<%@ Page Title="" Language="VB" MasterPageFile="~/Sales.master" AutoEventWireup="false" CodeFile="SampleSalesOrder.aspx.vb" Inherits="Sales_SampleSalesOrder" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
         div {
            color: black;
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
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgSampleSalesOrder.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
              });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlSearch.ClientID%>').select2();
            $('#<%=ddlParty.ClientID%>').select2();
            $('#<%=ddlModeOfCommunication.ClientID%>').select2();
            $('#<%=ddlModeOfShipping.ClientID%>').select2();            
            $('#<%=ddlCommodity.ClientID%>').select2();  
            $('#<%=ddlIssuedBy.ClientID%>').select2();  
        });
                                
     </script>
     <script type="text/javascript" language="javascript" src="../JavaScripts/General.js"></script>
    <script type="text/javascript" language="javascript">
        function Validate() {
            if (confirm("Are You Sure, You Want To Cancel this item ?.")) {
                return true
            }
            else {
                return false
            }
        }

        function ValidateQty() {
            if (document.getElementById('<%=txtQuantity.ClientID %>').value != "") {
                var num
                num = OnlyNumber(document.getElementById('<%=txtQuantity.ClientID %>').value)
                if (num == false) {
                    alert("Enter only integers for Quantity.")
                    document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                    document.getElementById('<%=txtQuantity.ClientID %>').focus();
                    return false;
                }
                var ssplacedqty = document.getElementById('<%=txtQuantity.ClientID %>').value
                var ssavailablestock = parseInt( document.getElementById('<%=lblAvailableStock.ClientID %>').innerHTML)
                if ((ssavailablestock < ssplacedqty)) {
                    alert("Out of Stock")
                     document.getElementById('<%=txtQuantity.ClientID %>').value = ""
                     document.getElementById('<%=txtQuantity.ClientID %>').focus();
                    return false;
                }
            }
        }

        function ValidateMasterData() {
            if (document.getElementById('<%=txtSampleNo.ClientID %>').value == "") {
                alert('Enter Order Code.');
                document.getElementById('<%=txtSampleNo.ClientID%>').focus();
                return false;
             }
            if (document.getElementById('<%=txtSampleNo.ClientID%>').value != "") {
                var name;
                name = document.getElementById('<%=txtSampleNo.ClientID %>').value
                if (name.length > 200) {
                    alert('Order Code exceeded maximum size(Only 200 Characters).');
                    document.getElementById('<%=txtSampleNo.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(name)) {
                        alert("Enter Order Code spaces are not allowed.");
                        document.getElementById('<%=txtSampleNo.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=txtSampleDate.ClientID %>').value == "") {
                alert('Enter Date Of Order.');
                document.getElementById('<%=txtSampleDate.ClientID%>').focus();
                return false;
            }
            if (document.getElementById('<%=txtSampleDate.ClientID %>').value != "") {
                var numd
                numd = isDate(document.getElementById('<%=txtSampleDate.ClientID %>').value)
                if (numd == false) {
                 alert("Enter Valid Date.")
                 document.getElementById('<%=txtSampleDate.ClientID %>').value = ""
                      document.getElementById('<%=txtSampleDate.ClientID%>').focus()
                 return false
                }
            }
            if (document.getElementById('<%=ddlParty.ClientID %>').selectedIndex == 0) {
                alert('Select Party.')
                document.getElementById('<%=ddlParty.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtPartyNo.ClientID %>').value == "") {
                alert('Enter Party Code.')
                document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                return false
            }
            if (document.getElementById('<%=txtPartyNo.ClientID%>').value != "") {
                var namep;
                namep = document.getElementById('<%=txtPartyNo.ClientID %>').value
                if (namep.length > 200) {
                    alert('Party Code exceeded maximum size(Only 200 Characters).');
                    document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(namep)) {
                        alert("Enter Party Code spaces are not allowed.");
                        document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=txtContactNo.ClientID %>').value != "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtContactNo.ClientID %>').value)
                if (num == false) {
                    alert("Enter valid Contact number.");
                    document.getElementById('<%=txtContactNo.ClientID %>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=ddlIssuedBy.ClientID %>').selectedIndex == 0) {
                alert('Select Inputed/Issued By.');
                document.getElementById('<%=ddlIssuedBy.ClientID%>').focus()
                return false;
            }
            if (document.getElementById('<%=txtShippingDate.ClientID %>').value != "") {
                var numd
                numd = isDate(document.getElementById('<%=txtShippingDate.ClientID %>').value)
                if (numd == false) {
                 alert("Enter Valid Shipping Date.")
                 document.getElementById('<%=txtShippingDate.ClientID %>').value = ""
                      document.getElementById('<%=txtShippingDate.ClientID%>').focus()
                 return false
                }
            }

            if ((document.getElementById('<%=txtSampleDate.ClientID %>').value != "") && (document.getElementById('<%=txtShippingDate.ClientID %>').value != "")) {
                var DV5 = DataValid(document.getElementById('<%=txtSampleDate.ClientID %>').value, document.getElementById('<%=txtShippingDate.ClientID %>').value);
                if (DV5 == false) {
                    alert("Shipping date(" + document.getElementById('<%=txtShippingDate.ClientID %>').value + ") should be greater than or equal to Ordered date(" + document.getElementById('<%=txtSampleDate.ClientID %>').value + ").");
                    document.getElementById('<%=txtShippingDate.ClientID %>').value = ""
                    document.getElementById('<%=txtShippingDate.ClientID%>').focus()
                    return false;
                }
            }
            
        }


        var ddlText, ddlValue, ddl, lblMesg;

    function CacheItems() {

        ddlText = new Array();

        ddlValue = new Array();

        ddl = document.getElementById("<%=lstBoxDescription.ClientID %>");

        for (var i = 0; i < ddl.options.length; i++) {

            ddlText[ddlText.length] = ddl.options[i].text;

            ddlValue[ddlValue.length] = ddl.options[i].value;

        }

    }

    window.onload = CacheItems;

    function FilterItems(value) {

        ddl.options.length = 0;
        var str = value.toLowerCase()
        for (var i = 0; i < ddlText.length; i++) {

            if (ddlText[i].toLowerCase().indexOf(str) != -1) {

                AddItem(ddlText[i], ddlValue[i]);

            }

        }

        if (ddl.options.length == 0) {

            AddItem("No items found.", "");

        }

    }

    function AddItem(text, value) {

        var opt = document.createElement("option");

        opt.text = text;

        opt.value = value;

        ddl.options.add(opt);

    }

    function ValidateForm() {

            <%--if (document.getElementById('<%=ddlModeOfShipping.ClientID %>').selectedIndex == 0) {
                alert('Select Method Of shipping.')
                document.getElementById('<%=ddlModeOfShipping.ClientID%>').focus()
                return false
            }--%>
            if (document.getElementById('<%=txtShippingDate.ClientID %>').value != "") {
                var numd
                numd = isDate(document.getElementById('<%=txtShippingDate.ClientID %>').value)
                if (numd == false) {
                 alert("Enter Valid Shipping Date.")
                 document.getElementById('<%=txtShippingDate.ClientID %>').value = ""
                      document.getElementById('<%=txtShippingDate.ClientID%>').focus()
                 return false
                }
            }

            if ((document.getElementById('<%=txtSampleDate.ClientID %>').value != "") && (document.getElementById('<%=txtShippingDate.ClientID %>').value != "")) {
                var DV5 = DataValid(document.getElementById('<%=txtSampleDate.ClientID %>').value, document.getElementById('<%=txtShippingDate.ClientID %>').value);
                if (DV5 == false) {
                    alert("Shipping date(" + document.getElementById('<%=txtShippingDate.ClientID %>').value + ") should be greater than or equal to Ordered date(" + document.getElementById('<%=txtSampleDate.ClientID %>').value + ").");
                    document.getElementById('<%=txtShippingDate.ClientID %>').value = ""
                    document.getElementById('<%=txtShippingDate.ClientID%>').focus()
                    return false;
                }
            }

            
            <%--if (document.getElementById('<%=ddlModeOfCommunication.ClientID %>').selectedIndex == 0) {
                alert('Select Mode Of Communication.')
                document.getElementById('<%=ddlModeOfCommunication.ClientID%>').focus()
                return false
            }--%>
            if (document.getElementById('<%=ddlParty.ClientID %>').selectedIndex == 0) {
                alert('Select Party.')
                document.getElementById('<%=ddlParty.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=txtPartyNo.ClientID %>').value == "") {
                alert('Enter Party Code.')
                document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                return false
            }
            if (document.getElementById('<%=txtPartyNo.ClientID%>').value != "") {
                var namep;
                namep = document.getElementById('<%=txtPartyNo.ClientID %>').value
                if (namep.length > 200) {
                    alert('Party Code exceeded maximum size(Only 200 Characters).');
                    document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                        return false;
                    }
                    var re = /^\s*$/;
                    if (re.test(namep)) {
                        alert("Enter Party Code spaces are not allowed.");
                        document.getElementById('<%=txtPartyNo.ClientID%>').focus();
                    return false;
                }
            }
            if (document.getElementById('<%=txtContactNo.ClientID %>').value !== "") {
                var num
                num = isValidPhoneNumber(document.getElementById('<%=txtContactNo.ClientID %>').value)
                if (num == false) {
                    alert("Enter valid Contact number.");
                    document.getElementById('<%=txtContactNo.ClientID %>').focus();
                    return false;
                }
            }
            
            if (document.getElementById('<%=ddlIssuedBy.ClientID %>').selectedIndex == 0) {
                alert('Select Inputed/Issued By.');
                document.getElementById('<%=ddlIssuedBy.ClientID%>').focus()
                return false;
            }

            if (document.getElementById('<%=ddlCommodity.ClientID %>').selectedIndex == 0) {
                alert('Select Commodity.');
                document.getElementById('<%=ddlCommodity.ClientID%>').focus()
                return false;
            }
            var list = document.getElementById("<%=lstBoxDescription.ClientID %>");
            var selected_items = 0;
            for (var i = 0; i < list.length; i++) {
                if (list[i].selected) {
                    selected_items++;
                }
            }
            if (!(selected_items > 0)) {
                alert("Select at least one item in Item Description.");
                return false;
            }
            if ((selected_items > 1)) {
                alert("Select one item Description at a time.");
                return false;
            }
           
            if (document.getElementById('<%=txtQuantity.ClientID %>').value == "") {
                alert('Enter Quantity.');
                document.getElementById('<%=txtQuantity.ClientID%>').focus();
                return false;
            }            
        }

    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Sample Sales Order</b></h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh/Clear" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnDelete" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" />
                   <%-- <asp:ImageButton ID="imgbtnReport" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Report" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" />--%>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblErrorUp" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Existing Order No."></asp:Label>
            <asp:DropDownList ID="ddlSearch" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Sample No."></asp:Label>
            <asp:TextBox ID="txtSampleNo" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable"></asp:TextBox>
        </div>

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Sample Date"></asp:Label>
            <div class="form-group">
                <asp:TextBox ID="txtSampleDate" CssClass="aspxcontrols" runat="server" Width="100px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtSampleDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtSampleDate" TargetControlID="txtSampleDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft"></cc1:CalendarExtender>
                <asp:Label ID="lblGap" runat="server">&gt;=</asp:Label>
                <asp:Label ID="lblStartDate" runat="server" Text="" Width="50px"></asp:Label>
            </div>
        </div>

        <div class="col-sm-3 col-md-3">
            <br />
            <asp:Label runat="server" Text="Status :-"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" ></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="* Customer Name"></asp:Label>
                    <asp:DropDownList ID="ddlParty" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="210px"></asp:DropDownList>
                    <asp:ImageButton ID="imgbtnCreateCustomer" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Create" />
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Customer Code"></asp:Label>
                    <asp:TextBox ID="txtPartyNo" CssClass="aspxcontrolsdisable" runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblContactNo" runat="server" Text="Contact No"></asp:Label>
                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Mode Of Communication"></asp:Label>
                    <asp:DropDownList ID="ddlModeOfCommunication" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="* Mode Of Shipping"></asp:Label>
                    <asp:DropDownList ID="ddlModeOfShipping" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Shipping Date"></asp:Label>
                    <asp:TextBox ID="txtShippingDate" placeholder="dd/MM/yyyy" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtShippingDate_CalendarExtender" runat="server" PopupButtonID="txtShippingDate" CssClass="cal_Theme1"
                        TargetControlID="txtShippingDate" Format="dd/MM/yyyy" PopupPosition="BottomLeft">
                    </cc1:CalendarExtender>
                </div>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server" CssClass="aspxcontrolsdisable" TextMode="MultiLine" Height="80px"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Commodity"></asp:Label>
            <asp:DropDownList ID="ddlCommodity" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Inputed/Issued By"></asp:Label>
            <asp:DropDownList ID="ddlIssuedBy" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
        </div>        
    </div>


     <div class="col-sm-12 col-md-12 form-group">
        <fieldset>
            <legend class="legendbold">Details of Sales Order</legend>
        </fieldset>
        <div class="col-sm-6 col-md-6" style="height: 200px; padding-left: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Label runat="server" Text="* Item Description"></asp:Label>
                    <asp:TextBox ID="txtSearchItem" onkeyup="FilterItems(this.value)" runat="server" CssClass="aspxcontrols" placeholder="Search By Description" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 pre-scrollableborder" style="padding: 0px">
                <asp:ListBox ID="lstBoxDescription" CssClass="aspxcontrols" AutoPostBack="true" runat="server" Enabled="False" Height="150px"
                    Font-Names="Verdana" Font-Size="Smaller"></asp:ListBox>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding-right: 0px">
                <asp:Label runat="server" Text="* Unit of Measurement"></asp:Label>
                <asp:DropDownList ID="ddlUnitOfMeassurement" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                    <br />
                    <asp:Label runat="server" Text="Available Stock"></asp:Label>
                    <asp:Label ID="lblAvailableStock" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6 col-md-6">
                    <asp:Label runat="server" Text="* Quantity"></asp:Label>
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="top"></asp:TextBox>
                </div>               
            </div>            
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding-right: 0px">
        <div class="pull-right">
            
            <asp:TextBox ID="txtOrderID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
            
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="overflow: auto">
        <asp:GridView ID="dgSampleSalesOrder" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CommodityID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodityID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CommodityID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ItemID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblItemID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="HistoryID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblHistoryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HistoryID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UnitID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SlNo" HeaderText="Sl.No" Visible="False" HeaderStyle-Width="5%"></asp:BoundField>
                <asp:TemplateField HeaderText="Commodity">
                    <ItemTemplate>
                        <asp:Label ID="lblCommodity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Commodity") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Goods">
                    <ItemTemplate>
                        <asp:Label ID="lblGoods" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Goods") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Unit">
                    <ItemTemplate>
                        <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Unit") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="TotalAmount">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>               
                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnCancel" runat="server" CommandName="Cancel" Height="16px" ImageUrl="~/Images/4delete.gif" ToolTip="Delete/Cancel" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
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
</asp:Content> 

