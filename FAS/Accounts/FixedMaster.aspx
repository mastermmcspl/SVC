<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="FixedMaster.aspx.vb" Inherits="Accounts_FixedMaster" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="reportDetailsMN">
         <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3 pull-left">
                <h2><b>Fixed Asset Master</b></h2>
            </div>
               <div class="col-sm-3 col-md-3 pull-right">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" />
                </div>
            </div>
         </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
     <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px" >
         <div class="col-sm-4 col-md-4">
             <asp:Label ID="lblSelect" runat="server" Text="Select"></asp:Label>
             <asp:DropDownList ID="ddlDepreciationType" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                <asp:ListItem Value="0">--- Select ---</asp:ListItem>
                <asp:ListItem Value="1">Straight Line</asp:ListItem>
                <asp:ListItem Value="2">Written Down</asp:ListItem>
                <asp:ListItem Value="3">2nd Shift & 3rd Shift</asp:ListItem>
            </asp:DropDownList>
         </div>
         <div class="col-sm-4 col-md-4">
             <asp:Label ID="lblDesc" runat="server" Text="Select"></asp:Label>
             <asp:DropDownList ID="ddlglDesc" runat="server" AutoPostBack="true" CssClass="aspxcontrols">              
            </asp:DropDownList>
         </div>
         <div class="col-sm-4 col-md-4">
             <asp:Label ID="Label1" runat="server" Text="Select"></asp:Label>
             <asp:TextBox ID ="txtDepreciationRate" runat ="server" CssClass="aspxcontrols "></asp:TextBox>
         </div>
     </div>

    <div class="col-sm-12 col-md-12">
        <asp:GridView ID="dgDetails" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns >
                <asp:TemplateField HeaderText="ID" Visible="false" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Depreciation Type" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblDepType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DepType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="gl Description" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblglDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "glDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Depreciation Rate" HeaderStyle-Width="3%">
                    <ItemTemplate>
                        <asp:Label ID="lblDepRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DepRate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>        
    </div>
</asp:Content>

