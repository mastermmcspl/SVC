<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="DigitalFilling.aspx.vb" Inherits="HomePages_DigitalFilling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
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
    </style>
      <style>
      div{
          color:black;
      }
      </style> 
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
              $('#<%=ddlFinancialYear.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 col-lg-12">
        <div class="pull-right divmargin ">
            <asp:Label ID="lblHeadingFY" Text="Financial year" runat="server" Visible ="false" ></asp:Label>
            <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100px" Visible ="false" >
            </asp:DropDownList>
        </div>
    </div>
</asp:Content>

