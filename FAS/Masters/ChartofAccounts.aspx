<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Master.master" CodeFile="ChartofAccounts.aspx.vb" Inherits="Masters_ChartofAccounts" ValidateRequest="false" Debug="true" %>

<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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

         div {
             color: black;
         }
     </style>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link  rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlHead.ClientID%>').select2();
            $('#<%=ddlGroup.ClientID%>').select2();
            $('#<%=ddlSubGroup.ClientID%>').select2();
            $('#<%=ddlGL.ClientID%>').select2();
            $('#<%=ddlSubGL.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
     <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>2.2 Chart of Accounts</b></h2>
            </div>
           
            <div class="col-sm-6 col-md-6">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Add24.png" runat="server" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Activate24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/DeActivate24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
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
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-8 col-md-8">
                <div class="form-group">
                    <asp:Label runat="server" Text="Selected Path :-"></asp:Label>
                    <asp:Label ID="lblPath" runat="server" CssClass="aspxlabelbold" ></asp:Label>
                </div>
            </div>

           
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="Status:- " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
                </div>
            </div>
            
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                    <asp:DropDownList ID="ddlHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        <asp:ListItem Value="0">Head of Accounts</asp:ListItem>
                        <asp:ListItem Value="1">Assets</asp:ListItem>
                        <asp:ListItem Value="2">Income</asp:ListItem>
                        <asp:ListItem Value="4">Liabilities</asp:ListItem>
                        <asp:ListItem Value="3">Expenditure</asp:ListItem>
                       <%-- <asp:ListItem Value="5">Special/Exceptional/Extraordinary Items</asp:ListItem>
                        <asp:ListItem Value="6">Taxes</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="col-sm-6 col-md-6" style="text-align:right"">
                        <div class="form-group">
                            <asp:LinkButton ID="lnkCOA" runat="server" Text="Standard Chart of Accounts"></asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label runat="server" Text="* Group"></asp:Label>
                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label runat="server" Text="Sub Group"></asp:Label>
                    <asp:DropDownList ID ="ddlSubGroup" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label runat="server" Text="General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddlGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddlSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack ="true"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="pre-scrollableborder">
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                     <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCode" runat="server" ControlToValidate="txtCode" Display="Dynamic" SetFocusOnError="True" 
                       ErrorMessage="Click on Add button"  ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:Label runat="server" Text="Code"></asp:Label>
                    <asp:TextBox ID="txtCode" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label runat="server" Text="* Name"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVName" runat="server" ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Name." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVEName" runat="server" ControlToValidate="txtName" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtName" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Description"></asp:Label>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescription" runat="server" ControlToValidate="txtDescription" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Enter Description." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtDescription" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="150"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
      

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div runat="server" style="padding-left: 0px; border-style: solid; border-color: inherit; border-width: medium; overflow: auto; width: 100%; height: 200px; background-color: #FFFFFF;">
            <wtv:treeview ID="tvMCAccount" runat="server" ForeColor="Black" AutoPostBack="True"
            NodeImageUrl="../Images/TreeImages/FOLDER.GIF" NodeImageUrlExpanded="../Images/TreeImages/folderopen.gif"
            NodeImageUrlEmpty="../Images/TreeImages/greenfolder.gif" 
            NodeCssClassLoading="MyNodeLoading" NodeCssClass="MyNode" NodeCssClassOver="MyNodeOver"
            NodeCssClassSelected="MyNodeSelected" PathToImagesValidate="False" PopulateOnDemand="true"
            NodeWrap="True" PathToImages="../Images/TreeImages" Animate="False"  Style="z-index: -100;"
            Height="300px" Width="100%">
        </wtv:treeview>
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


            <div id="myModal" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">

                <div class="modal-dialog">
                    <div class="modal-content row">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"><b>Standard Chart of Accounts</b></h4>
                        </div>
                        <div class="modal-body row">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-sm-12 col-md-12">
                                        <div runat="server" style="padding-left: 0px; border-style: solid; border-color: inherit; border-width: medium; overflow: auto; width: 99%; height: 200px; background-color: #FFFFFF;">
                                            <wtv:TreeView ID="TVStadardCOA" runat="server" ForeColor="Black" AutoPostBack="True"
                                                NodeImageUrl="../Images/TreeImages/FOLDER.GIF" NodeImageUrlExpanded="../Images/TreeImages/folderopen.gif"
                                                NodeImageUrlEmpty="../Images/TreeImages/greenfolder.gif"
                                                NodeCssClassLoading="MyNodeLoading" NodeCssClass="MyNode" NodeCssClassOver="MyNodeOver"
                                                NodeCssClassSelected="MyNodeSelected" PathToImagesValidate="False" PopulateOnDemand="true"
                                                NodeWrap="True" PathToImages="../Images/TreeImages" Animate="False" Style="z-index: -100;"
                                                Height="300px" Width="99%">
                                            </wtv:TreeView>
                                        </div>
                                </ContentTemplate>
                                  <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="TVStadardCOA" EventName="NodeClick" />
                                <asp:AsyncPostBackTrigger ControlID="btnStandardOK" EventName="Click" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <div class="pull-right">
                                <asp:Button runat="server" Text="Import" class="btn-ok" ID="btnStandardOK" ValidationGroup="ValidateDescriptor"></asp:Button>
                                <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnStandardCancel" ValidationGroup="ValidateDescriptor"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>


         <div id="ModalValidationChart" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType1" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsgChart" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                   <asp:Button runat="server" Text="Yes" class="btn-ok" ID="btnYes"></asp:Button>
                        <asp:Button runat="server" Text="No" class="btn-ok" ID="btnNo"></asp:Button>
                </div>
            </div>

        </div>
    </div>

        <div id="ModalValidationCOA" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modalmsg-dialog">
                <div class="modalmsg-content">
                    <div class="modalmsg-header">
                        <h4 class="modal-title"><b>FAS</b></h4>
                    </div>
                    <div class="modalmsg-body">
                        <div id="divMsgType" class="alert alert-info">
                            <p>
                                <strong>
                                    <asp:Label ID="lblValidationMsgCOA" runat="server"></asp:Label></strong>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

