<%@ Page Title="" Language="VB" MasterPageFile="~/DigitalFilling.master" AutoEventWireup="false" CodeFile="NormalScanDetails.aspx.vb" Inherits="DigitalFilling_NormalScanDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.initiate.js"></script>
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.config.js"></script>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Normal Scan</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="pull-right">
                    <div class="form-group">
                        <input type="button" onclick="AcquireImage()" style="background: url(../Images/SearchImage/Scanner24.png) no-repeat; height: 30px; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Scan" />
                        <input type="button" onclick="LoadImage()" style="background: url(../Images/Upload24.png) no-repeat; height: 30px; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Upload" />
                        <input type="button" onclick="SaveWithFileDialog()" style="background: url(../Images/Save24.png) no-repeat; height: 30px; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Save" />
                        <input type="button" onclick="Setting()" style="background: url(../Images/Setting24.png) no-repeat; height: 30px; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Scanning Settings" />
                        <input type="button" onclick="BackToMain()" style="background: url(../Images/Backward24.png) no-repeat; height: 30px; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Back" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none">
        <asp:ListBox ID="lstImages" runat="server" Height="100px" Visible="true" Width="100px"></asp:ListBox>
        <asp:ImageButton ID="imgbtnDelete" runat="server" Height="0px" Visible="true" Width="0px" />
        <asp:ImageButton ID="imgbtnDeleteAll" runat="server" Height="0px" Visible="true" Width="0px" />
        <asp:Label ID="lblFolderName" runat="server" Height="0px" Visible="true" Width="0px"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="divmargin">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <div class="form-group">
                    <input type="button" onclick="btnFirstImage_onclick()" style="background: url(../Images/SearchImage/First16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="First" />
                    <input type="button" onclick="btnPreImage_onclick()" style="background: url(../Images/SearchImage/Previous16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Previous" />
                    <input type="text" size="2" id="DW_CurrentImage" readonly="readonly" value="0" class="aspxcontrols" style="width: 50px;" />
                    /
            <input type="text" size="2" id="DW_TotalImage" readonly="readonly" value="0" class="aspxcontrols" style="width: 50px;" />
                    <input type="button" onclick="btnNextImage_onclick()" style="background: url(../Images/SearchImage/Next16.png) no-repeat; width: 30px; border: hidden; margin-left: 12px" data-toggle="tooltip" data-placement="bottom" title="Next" />
                    <input type="button" onclick="btnLastImage_onclick()" style="background: url(../Images/SearchImage/Last16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Last" />
                    <input type="button" onclick="RotateLeft()" style="background: url(../Images/SearchImage/RotateLeft16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Rotate Left" />
                    <input type="button" onclick="RotateRight()" style="background: url(../Images/SearchImage/RotateRight16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Rotate Right" />
                    <input type="button" onclick="btnZoomin_onclick()" style="background: url(../Images/SearchImage/ZoomIn16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Zoom Out" />
                    <input type="button" onclick="btnZoomout_onclick()" style="background: url(../Images/SearchImage/ZoomOut16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Zoom In" />
                    <input type="button" onclick="FitScreen()" style="background: url(../Images/SearchImage/FitScreen16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Fit Screen" />
                    <input type="button" onclick="Mirror()" style="background: url(../Images/SearchImage/Mirror16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Mirror" />
                    <input type="button" onclick="Flip()" style="background: url(../Images/SearchImage/Flip16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Flip" />
                    <input type="button" onclick="btnRemoveSelectedImages_onclick()" style="background: url(../Images/Multiply16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Remove Selected Images" />
                    <input type="button" onclick="btnRemoveAllImages_onclick()" style="background: url(../Images/Trash16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Remove All Images" />
                    <input type="button" onclick="ShowImageEditor()" style="background: url(../Images/Edit16.png) no-repeat; width: 30px; border: hidden;" data-toggle="tooltip" data-placement="bottom" title="Show Image Editor" />
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <div class="pull-right">
                        Source
                    <select size="1" id="source" style="position: relative; width: 200px" class="aspxcontrols"></select>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-2 col-md-2" style="padding-left: 0px">
            <div style="display: block;">
                <!-- dwtcontrolContainer is the default div id for Dynamic Web TWAIN control.
        If you need to rename the id, you should also change the id in the dynamsoft.webtwain.config.js accordingly. -->
                <div id="dwtcontrolContainer"></div>
            </div>
        </div>
        <div class="col-sm-10 col-md-10" style="padding: 0px">
            <div style="display: block;">
                <!-- dwtcontrolContainer is the default div id for Dynamic Web TWAIN control.
        If you need to rename the id, you should also change the id in the dynamsoft.webtwain.config.js accordingly. -->
                <div id="dwtcontrolContainerLargeViewer"></div>
            </div>
        </div>
    </div>

    <div id="ModalScanningSettings" class="modalmsg fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Scan Settings</h4>
                </div>
                <div class="modal-body" style="height: 160px">
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-4 col-md-4">
                            <div class="row">
                                <asp:Label ID="lblHTypeImage" runat="server" Text="Type of Image" Font-Bold="true"></asp:Label>
                            </div>
                            <br />
                            <div class="row">
                                <label for="BW">
                                    <input type="radio" id="BW" name="PixelType">B&amp;W
                                </label>
                            </div>
                            <br />
                            <div class="row">
                                <label for="Gray">
                                    <input type="radio" id="Gray" name="PixelType">Gray</label>
                            </div>
                            <br />
                            <div class="row">
                                <label for="RGB">
                                    <input type="radio" id="RGB" name="PixelType" checked="checked">Color</label>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="row">
                                <asp:Label ID="lblHImageType" runat="server" Text="Image Type" Font-Bold="true"></asp:Label>
                            </div>
                            <br />
                            <div class="row">
                                <label>
                                    <input type="radio" value="jpg" name="ImageType" id="imgTypejpeg" />JPEG</label>
                            </div>
                            <br />
                            <div class="row">
                                <label>
                                    <input type="radio" value="tif" name="ImageType" id="imgTypetiff" />TIFF</label>
                            </div>
                            <br />
                            <div class="row">
                                <label>
                                    <input type="radio" value="pdf" name="ImageType" id="imgTypepdf" checked="checked" />PDF</label>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="row">
                                <asp:Label ID="lblResolution" runat="server" Text="Resolution" Font-Bold="true"></asp:Label>
                            </div>
                            <br />
                            <div class="row">
                                <select size="1" id="Resolution" class="aspxcontrols" style="width: 85px">
                                    <option value="100">100</option>
                                    <option value="150">150</option>
                                    <option value="200">200</option>
                                    <option value="300">300</option>
                                </select>
                            </div>
                            <br />
                            <div class="row">
                                <label>
                                    <input type="checkbox" id="ADF" checked="checked">Auto Feeder</label>
                            </div>
                            <br />
                            <div class="row">
                                <label>
                                    <input type="checkbox" id="ShowUI" checked="checked">Show UI<br />
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function Setting() {
            $('#ModalScanningSettings').modal('show');
        }

        function BackToMain() {
            window.location.href = "NormalScan.aspx";
        }

        function SettingValues() {
            if ('<%= Session("TypeOfImage") %>' == "BW") {
                document.getElementById("BW").checked = true;
                document.getElementById("Gray").checked = false;
                document.getElementById("RGB").checked = false;
            } else if ('<%= Session("TypeOfImage") %>' == "Gray") {
                document.getElementById("BW").checked = false;
                document.getElementById("Gray").checked = true;
                document.getElementById("RGB").checked = false;
            } else {
                document.getElementById("BW").checked = false;
                document.getElementById("Gray").checked = false;
                document.getElementById("RGB").checked = true;
            }

            if ('<%= Session("ImageFormat") %>' == "jpg") {
                document.getElementById("imgTypejpeg").checked = true;
                document.getElementById("imgTypetiff").checked = false;
                document.getElementById("imgTypepdf").checked = false;
            } else if ('<%= Session("ImageFormat") %>' == "tif") {
                document.getElementById("imgTypejpeg").checked = false;
                document.getElementById("imgTypetiff").checked = true;
                document.getElementById("imgTypepdf").checked = false;
            } else {
                document.getElementById("imgTypejpeg").checked = false;
                document.getElementById("imgTypetiff").checked = false;
                document.getElementById("imgTypepdf").checked = true;
            }

            document.getElementById("Resolution").value = '<%= Session("Resolution") %>';

            if ('<%= Session("AutoFeeder") %>' == "YES") {
                document.getElementById("ADF").checked = true;
            } else {
                document.getElementById("ADF").checked = false;
            }

            if ('<%= Session("ShowUI") %>' == "YES") {
                document.getElementById("ShowUI").checked = true;
            } else {
                document.getElementById("ShowUI").checked = false;
            }
        }

        var console = window['console'] ? window['console'] : { 'log': function () { } };

        Dynamsoft.WebTwainEnv.RegisterEvent('OnWebTwainReady', Dynamsoft_OnReady); // Register OnWebTwainReady event. This event fires as soon as Dynamic Web TWAIN is initialized and ready to be used

        var DWObject, DWObjectLargeViewer;
        function Dynamsoft_OnReady() {
            DWObject = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainer'); // Get the Dynamic Web TWAIN object that is embeded in the div with id 'dwtcontrolContainer'
            DWObjectLargeViewer = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainerLargeViewer');

            DWObject.Width = '100%'; // Set the width of the Dynamic Web TWAIN Object 
            DWObject.Height = window.innerHeight - 175;
            DWObject.SetViewMode(1, 5);

            DWObjectLargeViewer.Width = '100%';
            DWObjectLargeViewer.Height = window.innerHeight - 175;
            //DWObjectLargeViewer.SetViewMode(1, 1);
            DWObjectLargeViewer.MaxImagesInBuffer = 1;
            DWObjectLargeViewer.SetViewMode(-1, -1);
            DWObjectLargeViewer.IfFitWindow = true;

            if (DWObject) {
                LoadImages();
                SettingValues();
                var count = DWObject.SourceCount; // Get how many sources are installed in the system
                if (count == 0 && Dynamsoft.Lib.env.bMac) {
                    DWObject.CloseSourceManager();
                    DWObject.ImageCaptureDriverType = 0;
                    DWObject.OpenSourceManager();
                    count = DWObject.SourceCount;
                }

                for (var i = 0; i < count; i++)
                    document.getElementById("source").options.add(new Option(DWObject.GetSourceNameItems(i), i)); // Add the sources in a drop-down list

                // The event OnPostTransfer fires after each image is scanned and transferred
                DWObject.RegisterEvent("OnPostTransfer", function () {
                    updatePageInfo();
                });

                // The event OnPostLoad fires after the images from a local directory have been loaded into the control
                DWObject.RegisterEvent("OnPostLoad", function () {
                    updatePageInfo();
                });

                // The event OnMouseClick fires when the mouse clicks on an image on Dynamic Web TWAIN viewer
                DWObject.RegisterEvent("OnMouseClick", function () {
                    updatePageInfo();
                });

                // The event OnTopImageInTheViewChanged fires when the top image currently displayed in the viewer changes
                DWObject.RegisterEvent("OnTopImageInTheViewChanged", function (index) {
                    DWObject.CurrentImageIndexInBuffer = index;
                    updatePageInfo();
                });
            }
        }

        function AcquireImage() {
            if (DWObject) {
                var OnAcquireImageSuccess, OnAcquireImageFailure;
                OnAcquireImageSuccess = OnAcquireImageFailure = function () {
                    DWObject.CloseSource();
                };

                DWObject.SelectSourceByIndex(document.getElementById("source").selectedIndex);
                DWObject.OpenSource();
                DWObject.IfDisableSourceAfterAcquire = true;
                //Pixel type
                if (document.getElementById("BW").checked)
                    DWObject.PixelType = EnumDWT_PixelType.TWPT_BW;
                else if (document.getElementById("Gray").checked)
                    DWObject.PixelType = EnumDWT_PixelType.TWPT_GRAY;
                else if (document.getElementById("RGB").checked)
                    DWObject.PixelType = EnumDWT_PixelType.TWPT_RGB;
                //If auto feeder
                if (document.getElementById("ADF").checked)
                    DWObject.IfFeederEnabled = true;
                else
                    DWObject.IfFeederEnabled = false;
                //If show UI
                if (document.getElementById("ShowUI").checked)
                    DWObject.IfShowUI = true;
                else
                    DWObject.IfShowUI = false;
                //Resolution
                DWObject.Resolution = parseInt(document.getElementById("Resolution").value);

                if (document.getElementById("ADF").checked && DWObject.IfFeederEnabled == true)  // if paper is NOT loaded on the feeder
                {
                    if (DWObject.IfFeederLoaded != true && DWObject.ErrorCode == 0) {
                        swal("No paper detected! Please load papers and try again!", "", "error");
                        return;
                    }
                }
                DWObject.AcquireImage(OnAcquireImageSuccess, OnAcquireImageFailure);
            }
        }

        //Callback functions for async APIs
        function OnSuccess() {
            console.log('successful');
        }

        function OnFailure(errorCode, errorString) {
            swal(errorString, "", "error");
        }

        function OnHttpUploadSuccess() {
            console.log('successful');
        }

        function OnHttpUploadFailure(errorCode, errorString, sHttpResponse) {
            swal(errorString + sHttpResponse, "", "error");
        }

        function LoadImage() {
            if (DWObject) {
                DWObject.IfShowFileDialog = true; // Open the system's file dialog to load image
                DWObject.LoadImageEx("", EnumDWT_ImageType.IT_ALL, OnSuccess, OnFailure); // Load images in all supported formats (.bmp, .jpg, .tif, .png, .pdf). OnSuccess or OnFailure will be called after the operation
            }
        }

        function btnFirstImage_onclick() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer != 0 && DWObject.CurrentImageIndexInBuffer != 0) {
                    DWObject.CurrentImageIndexInBuffer = 0;
                    updatePageInfo();
                }
            }
        }

        function btnPreImage_onclick() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObject.CurrentImageIndexInBuffer = DWObject.CurrentImageIndexInBuffer - 1;
                    updatePageInfo();
                }
            }
        }

        function btnNextImage_onclick() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObject.CurrentImageIndexInBuffer = DWObject.CurrentImageIndexInBuffer + 1;
                    updatePageInfo();
                }
            }
        }

        function btnLastImage_onclick() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObject.CurrentImageIndexInBuffer = DWObject.HowManyImagesInBuffer - 1;
                    updatePageInfo();
                }
            }
        }

        function RotateLeft() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObject.RotateLeft(DWObject.CurrentImageIndexInBuffer);
                    DWObject.CopyToClipboard(document.getElementById("DW_CurrentImage").value - 1);
                    DWObjectLargeViewer.LoadDibFromClipboard();
                }
            }
        }

        function RotateRight() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObject.RotateRight(DWObject.CurrentImageIndexInBuffer);
                    DWObject.CopyToClipboard(document.getElementById("DW_CurrentImage").value - 1);
                    DWObjectLargeViewer.LoadDibFromClipboard();
                }
            }
        }

        function btnZoomin_onclick() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObjectLargeViewer.Zoom = DWObjectLargeViewer.Zoom * 0.9;
                }
            }
        }

        function btnZoomout_onclick() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObjectLargeViewer.Zoom = DWObjectLargeViewer.Zoom * 1.1;
                }
            }
        }

        function FitScreen() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObjectLargeViewer.IfFitWindow = true;
                }
            }
        }

        function Mirror() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObject.Mirror(DWObject.CurrentImageIndexInBuffer);
                    DWObject.CopyToClipboard(document.getElementById("DW_CurrentImage").value - 1);
                    DWObjectLargeViewer.LoadDibFromClipboard();
                }
            }
        }

        function Flip() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    DWObject.Flip(DWObject.CurrentImageIndexInBuffer);
                    DWObject.CopyToClipboard(document.getElementById("DW_CurrentImage").value - 1);
                    DWObjectLargeViewer.LoadDibFromClipboard();
                }
            }
        }

        function setlPreviewMode() {
            if (DWObject) {
                var o = parseInt(document.getElementById("DW_PreviewMode").selectedIndex + 1);
                DWObject.SetViewMode(o, o);
            }
        }

        function btnRemoveSelectedImages_onclick() {
            if (DWObject) {
                if (DWObject.HowManyImagesInBuffer > 0) {
                    swal({
                        title: "Are you sure?",
                        text: "You will not be able to recover this image!",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, delete it!",
                        cancelButtonText: "No, cancel plx!",
                        closeOnConfirm: false,
                        closeOnCancel: false
                    },
    function (isConfirm) {
        if (isConfirm) {
            swal("Deleted!", "Your image has been deleted.", "success");
            DWObject.RemoveAllSelectedImages();
            var lstImages = document.getElementById('<%= lstImages.ClientID %>');
            if (lstImages.options.length > 0) {
                $('#<%= imgbtnDelete.ClientID %>').click();
            }
            if (DWObject.HowManyImagesInBuffer == 0) {
                DWObjectLargeViewer.RemoveAllImages();
                document.getElementById("DW_CurrentImage").value = "0";
                document.getElementById("DW_TotalImage").value = "0";
            }
            else {
                updatePageInfo();
            }
        } else {
            swal("Cancelled", "Your image is safe.", "error");
        }
    });
}
}
}

function btnRemoveAllImages_onclick() {
    if (DWObject) {
        if (DWObject.HowManyImagesInBuffer > 0) {
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover all images!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, delete it!",
                cancelButtonText: "No, cancel plx!",
                closeOnConfirm: false,
                closeOnCancel: false
            },
function (isConfirm) {
    if (isConfirm) {
        swal("Deleted!", "All your images has been deleted.", "success");
        DWObject.RemoveAllImages();
        DWObjectLargeViewer.RemoveAllImages();
        var lstImages = document.getElementById('<%= lstImages.ClientID %>');
        if (lstImages.options.length > 0) {
            $('#<%= imgbtnDeleteAll.ClientID %>').click();
            }
            document.getElementById("DW_TotalImage").value = "0";
            document.getElementById("DW_CurrentImage").value = "0";
        } else {
            swal("Cancelled", "All your images are safe.", "error");
        }
});
}
}
}

function ShowImageEditor() {
    if (DWObject) {
        if (DWObject.HowManyImagesInBuffer == 0)
            swal("There is no image in buffer", "", "error");
        else
            DWObject.ShowImageEditor();
    }
}

function updatePageInfo() {
    if (DWObject) {
        document.getElementById("DW_TotalImage").value = DWObject.HowManyImagesInBuffer;
        document.getElementById("DW_CurrentImage").value = DWObject.CurrentImageIndexInBuffer + 1;
        DWObject.CopyToClipboard(document.getElementById("DW_CurrentImage").value - 1);
        DWObjectLargeViewer.LoadDibFromClipboard();
        var lstImages = document.getElementById('<%= lstImages.ClientID %>');
        if (lstImages.options.length > 0) {
            lstImages.selectedIndex = document.getElementById("DW_CurrentImage").value - 1;
        }
    }
}

function SaveWithFileDialog() {
    if (DWObject) {
        var Current = new Date();
        var month = new Array();
        month[0] = "Jan";
        month[1] = "Feb";
        month[2] = "Mar";
        month[3] = "Apr";
        month[4] = "May";
        month[5] = "Jun";
        month[6] = "Jul";
        month[7] = "Aug";
        month[8] = "Sept";
        month[9] = "Oct";
        month[10] = "Nov";
        month[11] = "Dec";
        var sDeleteFolder
        var lblFolderName = document.getElementById('<%= lblFolderName.ClientID %>');
                if (lblFolderName.innerHTML.toString() == '') {
                    var FolderName = Current.getDate().toString() + '-' + month[Current.getMonth()].toString() + '-' + Current.getFullYear().toString() + '@' + Current.getHours().toString() + Current.getMinutes().toString() + Current.getSeconds().toString();
                    sDeleteFolder = 'NO';
                } else {
                    var FolderName = lblFolderName.innerHTML.toString();
                    sDeleteFolder = 'YES'
                }
                // If no image in buffer, return the function
                if (DWObject.HowManyImagesInBuffer == 0)
                    return;
                for (i = 0; i < DWObject.HowManyImagesInBuffer; i++) {
                    DWObject.CurrentImageIndexInBuffer = i;
                    var strHTTPServer = location.hostname; //The name of the HTTP server. For example: "www.dynamsoft.com";
                    var CurrentPathName = unescape(location.pathname);
                    var CurrentPath = CurrentPathName.substring(0, CurrentPathName.lastIndexOf("/") + 1);
                    var strActionPage = CurrentPath + "UploadToServer.aspx?Delete=" + sDeleteFolder + "&ForLoop=" + i;
                    DWObject.IfSSL = false; // Set whether SSL is used
                    DWObject.HTTPPort = location.port == "" ? 80 : location.port;

                    var CurrentTime = new Date();
                    var uploadfilename = FolderName.toString() + '#' + CurrentTime.getMilliseconds(); // Uses milliseconds according to local time as the file name

                    // Upload the image(s) to the server asynchronously
                    if (document.getElementById("imgTypejpeg").checked == true) {
                        //If the current image is B&W
                        //1 is B&W, 8 is Gray, 24 is RGB
                        if (DWObject.GetImageBitDepth(DWObject.CurrentImageIndexInBuffer) == 1)
                            //If so, convert the image to Gray
                            DWObject.ConvertToGrayScale(DWObject.CurrentImageIndexInBuffer);
                        //Upload image in JPEG
                        DWObject.HTTPUploadThroughPost(strHTTPServer, DWObject.CurrentImageIndexInBuffer, strActionPage, uploadfilename + ".jpg", OnHttpUploadSuccess, OnHttpUploadFailure);
                    }
                    else if (document.getElementById("imgTypetiff").checked == true) {
                        DWObject.HTTPUploadAllThroughPostAsMultiPageTIFF(strHTTPServer, strActionPage, uploadfilename + ".tif", OnHttpUploadSuccess, OnHttpUploadFailure);
                    }
                    else if (document.getElementById("imgTypepdf").checked == true) {
                        DWObject.HTTPUploadAllThroughPostAsPDF(strHTTPServer, strActionPage, uploadfilename + ".pdf", OnHttpUploadSuccess, OnHttpUploadFailure);
                    }
                }
                swal({
                    title: "Successfully Saved.",
                    text: "",
                    type: "success",
                    confirmButtonText: "Ok",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
        function (isConfirm) {
            if (isConfirm) {
                window.location.href = "NormalScan.aspx";
            }
        });
            }
        }

        function LoadImages() {
            var DWObject = Dynamsoft.WebTwainEnv.GetWebTwain('dwtcontrolContainer');
            if (DWObject) {
                var lstImages = document.getElementById('<%= lstImages.ClientID %>');
                for (var i = 0; i < lstImages.options.length; i++) {
                    var strHTTPServer = location.hostname; //The name of the HTTP server. For example: "www.dynamsoft.com";
                    //var file = "/NormalScan/6/16-Feb-2017@17368_1/1_595.jpg";
                    var file = lstImages.options[i].text;
                    var downloadfilename = location.pathname.substring(0, location.pathname.lastIndexOf('/')) + file;
                    DWObject.HTTPPort = location.port == "" ? 80 : location.port;
                    DWObject.HTTPDownload(strHTTPServer, downloadfilename, OnSuccess, OnFailure);
                }
            }
        }
    </script>
</asp:Content>
