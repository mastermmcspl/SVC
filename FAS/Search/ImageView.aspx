<%@ Page Title="" Language="VB" MasterPageFile="~/Search.master" AutoEventWireup="false" CodeFile="ImageView.aspx.vb" Inherits="Search_ImageView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/font-awesome.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome.min.css" rel="stylesheet" />
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
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
                        this.setMode('pencil'); }
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
            <div class="col-sm-10 col-md-10 pull-left">
                <h2><b>Document Viewer</b></h2>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-12 col-md-12">
                <div class="pull-left">
                    <asp:ImageButton ID="imgbtnDownload" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Download" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnIndex" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnNote" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Note" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnNavDocFastRewind" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnPreviousNavDoc" CssClass="hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Previous" CausesValidation="false" />
                    <asp:TextBox ID="txtNavDoc" runat="server" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:label ID="lblNavDoc" runat="server" Width="50px" CssClass ="aspxlabelbold"></asp:label>
                    <asp:ImageButton ID="imgbtnNextNavDoc" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnNavDocFastForword" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                </div>
                <div class="pull-left">
                    <asp:ImageButton ID="imgbtnFastRewind" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnPreviousNav" CssClass="hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Previous" CausesValidation="false" />
                    <asp:TextBox ID="txtNav" runat="server" Enabled="true" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:label ID="lblNav" runat="server" Width="50px" CssClass="aspxlabelbold"></asp:label>
                    <asp:ImageButton ID="imgbtnNextNav" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnFastForword" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnWidth" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Width" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnHeight" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Height" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnFitScreen" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Fit Screen" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnZoomOut" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Zoom Out" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnZoomIn" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Zoom In" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnRotate90" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Rotate 90°" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnRotate180" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Rotate 180°" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnRotate270" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Rotate 270°" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnMagnifier" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Magnifier" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnPrint" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Print" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnPencil" CssClass="activeIcons hvr-bounce-out wPaint-menu-icon wPaint-menu-icon-name-pencil" runat="server" data-toggle="tooltip" data-placement="bottom" title="Line" CausesValidation="false" OnClientClick="function(e)" />
                    <asp:ImageButton ID="imgbtnRectangle" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Rectangle" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnTriangle" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Triangle" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnExit" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Exit" CausesValidation="false" />
                    <asp:DropDownList ID="ddlSelect" runat="server" CssClass="aspxcontrols" Width="150px"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:TextBox ID="txtDetID" runat="server" Style="visibility: hidden"></asp:TextBox>
    </div>
    <asp:Panel ID="pnlImageView" runat="server" Visible="false">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <asp:Image ID="RetrieveImage" runat="server" Height="485px" Width="1100px" />
        </div>
    </asp:Panel>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblDocumentTypeH" runat="server" Text="Document Type" Visible="false"></asp:Label>
        <asp:Label ID="lblDoucmentType" runat="server" Font-Bold="true" CssClass="aspxlabelbold" Visible="false"></asp:Label>
    </div>
    <asp:Panel ID="pnlDocView" runat="server" Visible="false">
        <div class="col-sm-12 col-md-12" style="padding-left: 0px">
            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                <asp:DataGrid ID="dgIndex" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" PageSize="5000">
                    <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                    <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="Descriptor" HeaderText="Index Details">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
            <div class="col-sm-6 col-md-6">
                <asp:Label ID="lblFileTypeH" runat="server" Text="File Type :-"></asp:Label>
                <asp:Label ID="lblFileType" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                <br />
                <asp:Label ID="lblCreatedByH" runat="server" Text="Created By :-"></asp:Label>
                <asp:Label ID="lblCreatedBy" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                <br />
                <asp:Label ID="lblCreatedOnH" runat="server" Text="Created On :-"></asp:Label>
                <asp:Label ID="lblCreatedOn" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                <br />
                <asp:Label ID="lblSizeH" runat="server" Text="Size :-"></asp:Label>
                <asp:Label ID="lblSize" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                <br />
                <br />
                <asp:LinkButton ID="lnkOpenDocument" runat="server" Font-Italic="true">Open Document</asp:LinkButton>
            </div>
        </div>
    </asp:Panel>
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
</asp:Content>

