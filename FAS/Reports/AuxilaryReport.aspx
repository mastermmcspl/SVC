<%@ Page Title="" Language="VB" MasterPageFile="~/Accounts.master" AutoEventWireup="false" CodeFile="AuxilaryReport.aspx.vb" Inherits="Reports_AuxilaryReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Reports</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnWord" Text="Download Word" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px">
        <div class="col-sm-6 col-md-6">
            <asp:Label runat="server" Text="Types of Reports"></asp:Label>
            <asp:DropDownList ID="ddlReports" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:GridView ID="grdRPA" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                <asp:BoundField DataField="MasterGLID" HeaderText="MasterGLID" Visible="False" />
                <asp:BoundField DataField="GLCode" HeaderText="GLCode" Visible="False" />
                <asp:BoundField DataField="Particulars" HeaderText="Particulars" />
                <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance" />
                <asp:BoundField DataField="Additions" HeaderText="Additions" />
                <asp:BoundField DataField="Transfer" HeaderText="Transfor" />
                <asp:BoundField DataField="Reduction" HeaderText="Reduction" />
                <asp:BoundField DataField="Sold" HeaderText="Sold" />
                <asp:BoundField DataField="RTransfer" HeaderText="Transfor" />
                <asp:BoundField DataField="RReduction" HeaderText="Reduction" />
                <asp:BoundField DataField="RRateOff" HeaderText="Rate off" />
                <asp:BoundField DataField="ROpnBal" HeaderText="Opn Balance" />
                <asp:BoundField DataField="DFortheYear" HeaderText="For the year"></asp:BoundField>
                <asp:BoundField DataField="DDeduction" HeaderText="Deduction" />
                <asp:BoundField DataField="DClsBal" HeaderText="Close Balance" />
                <asp:BoundField DataField="MOpnBal" HeaderText="Opn Balance" />
                <asp:BoundField DataField="MClsBal" HeaderText="Close Balance" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
