<%@ Page Title="" Language="VB" MasterPageFile="~/Search.master" AutoEventWireup="false" CodeFile="ImageView.aspx.vb" Inherits="Viewer_ImageView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/font-awesome.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script src="../JavaScripts/wPaint.menu.main.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(document).ready(function ($) {
            $.fn.wPaint.menus.main = {
                pencil: {
                    index: 21,
                    callback: (function () {
                        this.setMode('pencil');
                    }
                )
                }
            }

            $.fn.wPaint.extend({
                undoCurrent: -1,
                undoArray: [],
                setUndoFlag: true,

                _drawPencilDown: (function (e) {
                    this.ctx.lineJoin = 'round';
                    this.ctx.lineCap = 'round';
                    this.ctx.strokeStyle = this.options.strokeStyle;
                    this.ctx.fillStyle = this.options.strokeStyle;
                    this.ctx.lineWidth = this.options.lineWidth;

                    //draw single dot in case of a click without a move
                    this.ctx.beginPath();
                    this.ctx.arc(e.pageX, e.pageY, this.options.lineWidth / 2, 0, Math.PI * 2, true);
                    this.ctx.closePath();
                    this.ctx.fill();

                    //start the path for a drag
                    this.ctx.beginPath();
                    this.ctx.moveTo(e.pageX, e.pageY);
                }),

                _drawPencilMove: (function (e) {
                    this.ctx.lineTo(e.pageX, e.pageY);
                    this.ctx.stroke();
                }),

                _drawPencilUp: (function () {
                    this.ctx.closePath();
                    this._addUndo();
                })
            });
        })(jQuery);
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3">
                <h2><b>Document Viewer</b></h2>
            </div>
            <div class="col-sm-8 col-md-8">
                <asp:ImageButton ID="imgbtnNavDocFastRewind" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPreviousNavDoc" CssClass="hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Previous" Style="margin-right: 10px;" CausesValidation="false" />
                <asp:TextBox ID="txtNavDoc" runat="server" Enabled="false" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                <asp:Label ID="lblNavDoc" runat="server" Width="30px" CssClass="aspxlabelbold"></asp:Label>
                <asp:ImageButton ID="imgbtnNextNavDoc" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnNavDocFastForword" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                |
                <asp:ImageButton ID="imgbtnFastRewind" CssClass="activeIcons hvr-bounce-in" runat="server" Style="margin-left: 10px;" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPreviousNav" CssClass="hvr-bounce-in" runat="server" data-toggle="tooltip" Style="margin-right: 10px;" data-placement="bottom" title="Previous" CausesValidation="false" />
                <asp:TextBox ID="txtNav" runat="server" Enabled="false" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                <asp:Label ID="lblNav" runat="server" Width="30px" CssClass="aspxlabelbold"></asp:Label>
                <asp:ImageButton ID="imgbtnNextNav" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnFastForword" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnAnnotation" Visible="false" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Annotation" CausesValidation="false" />
                <asp:Label ID="lblHVersion" Visible="false" runat="server" Text="Version:"></asp:Label>
                <asp:DropDownList ID="ddlAnnotationVersion" Visible="false" runat="server" CssClass="aspxcontrols" Width="250px" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-1 col-md-1">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>

            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        <asp:TextBox ID="txtDetID" runat="server" Style="display: none;"></asp:TextBox>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-9 col-md-9">

            <%--   <embed src="C:\Users\MMCS 8\Desktop\EDICT_Standard_22-05-2018.pdf" style="width: 100%;height: 500%;border: none;"></embed>--%>
            <asp:Panel ID="pnlImageView" runat="server" CssClass="col-sm-8 col-md-8" Style="padding: 0px">
                <asp:Image ID="documentViewer1" CssClass="col-sm-12 col-md-12" runat="server" Width="1000px" Height="500px" />
            </asp:Panel>
          
            <%--<GleamTech:DocumentViewer ID="documentViewer" runat="server" Height="500"
                Resizable="False" />--%>
        </div>

        <asp:Panel ID="pnlDocView" runat="server" CssClass="col-sm-3 col-md-3" Style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group">
                <asp:Label ID="lblDoucmentType" runat="server" Font-Bold="true" CssClass="aspxlabelbold" Visible="false"></asp:Label>
            </div>
            <div class="col-sm-12 col-md-12 form-group">
                <div class="form-group">
                    <asp:Label ID="lblHFileName" runat="server" Text="File Name :"></asp:Label>
                    <asp:Label ID="lblFileName" runat="server" Font-Bold="true" CssClass="aspxlabelbold" Visible="false"></asp:Label>
                    <asp:LinkButton ID="lnkOpenDocument" Font-Bold="true" runat="server" Font-Italic="true" Visible="false"></asp:LinkButton>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblFileTypeH" runat="server" Text="File Type :"></asp:Label>
                    <asp:Label ID="lblFileType" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCreatedByH" runat="server" Text="Created By :"></asp:Label>
                    <asp:Label ID="lblCreatedBy" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCreatedOnH" runat="server" Text="Created On :"></asp:Label>
                    <asp:Label ID="lblCreatedOn" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblSizeH" runat="server" Text="Size :"></asp:Label>
                    <asp:Label ID="lblSize" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                </div>
                 <div class="form-group">
                    <asp:ImageButton ID ="imgbtnDownload" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Download" CausesValidation="false" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <asp:DataGrid ID="dgIndex" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" PageSize="5000">
                    <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                    <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblHDescriptor" runat="server" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblDescriptor" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Descriptor") %>' Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="100%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </asp:Panel>
    </div>

    <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtPreId" runat="server" Visible="false"></asp:TextBox>
    <asp:ListBox ID="lstDocument" runat="server" Visible="false"></asp:ListBox>
    <asp:ListBox ID="lstFiles" runat="server" Visible="false"></asp:ListBox>
    <asp:Label ID="lblDocID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblFileID" runat="server" Visible="false"></asp:Label>
    <div id="ModalSearchImageViewValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>EDICT</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblSearchImageViewValidationMsg" runat="server"></asp:Label></strong>
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
     <script>
        window.onbeforeunload = function (evt) {          

            $.ajax({
                type: "POST",
                url: "ImageView.aspx/zxa",
                data: "{ firstNumber: '" + parseInt(1) + "',secondNumber: '" + parseInt(2) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                success: onSucceed,
                Error: onError
            });           
        }
        // On Success
        function onSucceed(results, currentContext, methodName) {        
            if (results !== null && results.d !== null) {
                document.getElementById('lblError').innerHTML = results.d;
            }
        }
        // On Errors
        function onError(results, currentContext, methodName) {
            document.getElementById('lblError').innerHTML = results.d;
            console.log(results);
        }
    </script> 
</asp:Content>

