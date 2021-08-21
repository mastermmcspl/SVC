Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Partial Class Search_ImageView
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "ImageView.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsSearch As New clsSearch
    Private Shared sSession As AllSession
    Private Shared iSelectedFirstID As Integer = 0
    Private Shared iNextID As Integer = 1
    Private Shared iPageNext As Integer
    Private Shared sImgFilePath As String = ""
    Private Shared iNextPage As Integer
    Private Shared sBaseName() As String
    Private Shared sSelectedChecksIDs As String = ""
    Private Shared iSearchIndexID As Integer = 0

    Private Shared sSelectedCabID As String = ""
    Private Shared sSelectedSubCabID As String = ""
    Private Shared sSelectedFolID As String = ""
    Private Shared sSelectedDocTypeID As String = ""
    Private Shared sSelectedKWID As String = ""
    Private Shared sSelectedDescID As String = ""
    Private Shared sSelectedFrmtID As String = ""
    Private Shared sSelectedCrByID As String = ""
    Private Shared iSelectedIndexID As Integer = 0

    Private Shared sSelId As String = ""
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnDownload.ImageUrl = "~/Images/Download16.png"
        imgbtnIndex.ImageUrl = "~/Images/SearchImage/Index16.png"
        imgbtnNote.ImageUrl = "~/Images/Edit16.png"
        imgbtnNavDocFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNavDoc.ImageUrl = "~/Images/SearchImage/Previous16.png"
        imgbtnNextNavDoc.ImageUrl = "~/Images/SearchImage/Next16.png"
        imgbtnNavDocFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNav.ImageUrl = "~/Images/SearchImage/Preview16.png"
        imgbtnNextNav.ImageUrl = "~/Images/SearchImage/Nextt16.png"
        imgbtnFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnWidth.ImageUrl = "~/Images/SearchImage/Width16.png"
        imgbtnHeight.ImageUrl = "~/Images/SearchImage/Height16.png"
        imgbtnFitScreen.ImageUrl = "~/Images/SearchImage/FitScreen16.png"
        imgbtnZoomOut.ImageUrl = "~/Images/SearchImage/ZoomOut16.png"
        imgbtnZoomIn.ImageUrl = "~/Images/SearchImage/ZoomIn16.png"
        imgbtnRotate90.ImageUrl = "~/Images/SearchImage/Rotate90.png"
        imgbtnRotate180.ImageUrl = "~/Images/SearchImage/Rotate180.png"
        imgbtnRotate270.ImageUrl = "~/Images/SearchImage/Rotate270.png"
        imgbtnMagnifier.ImageUrl = "~/Images/SearchImage/Magnifier16.png"
        imgbtnPrint.ImageUrl = "~/Images/SearchImage/Print16.png"
        imgbtnPencil.ImageUrl = "~/Images/SearchImage/Pencil16.png"
        imgbtnRectangle.ImageUrl = "~/Images/SearchImage/Rectangle16.png"
        imgbtnTriangle.ImageUrl = "~/Images/SearchImage/Triangle16.png"
        imgbtnSave.ImageUrl = "~/Images/Save16.png"
        imgbtnExit.ImageUrl = "~/Images/SearchImage/Exit16.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dtDocument As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sSelId = String.Empty : sSelectedChecksIDs = String.Empty
                sSelectedCabID = String.Empty : sSelectedSubCabID = String.Empty : sSelectedFolID = String.Empty
                sSelectedDocTypeID = String.Empty : sSelectedKWID = String.Empty : sSelectedDescID = String.Empty
                sSelectedFrmtID = String.Empty : sSelectedCrByID = String.Empty
                iSelectedIndexID = 0 : iSelectedFirstID = 0

                If Request.QueryString("ImagePath") IsNot Nothing Then
                    sImgFilePath = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ImagePath")))
                End If
                If Request.QueryString("SelectedFirstID") IsNot Nothing Then
                    iSelectedFirstID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFirstID")))
                End If
                If Request.QueryString("SelectedChecksIDs") IsNot Nothing Then
                    sSelectedChecksIDs = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedChecksIDs")))
                End If

                If Request.QueryString("SelectedCabID") IsNot Nothing Then
                    sSelectedCabID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedCabID")))
                End If
                If Request.QueryString("SelectedSubCabID") IsNot Nothing Then
                    sSelectedSubCabID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedSubCabID")))
                End If
                If Request.QueryString("SelectedFolID") IsNot Nothing Then
                    sSelectedFolID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFolID")))
                End If
                If Request.QueryString("SelectedDocTypeID") IsNot Nothing Then
                    sSelectedDocTypeID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedDocTypeID")))
                End If
                If Request.QueryString("SelectedKWID") IsNot Nothing Then
                    sSelectedKWID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedKWID")))
                End If
                If Request.QueryString("SelectedDescID") IsNot Nothing Then
                    sSelectedDescID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedDescID")))
                End If
                If Request.QueryString("SelectedFrmtID") IsNot Nothing Then
                    sSelectedFrmtID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFrmtID")))
                End If
                If Request.QueryString("SelectedCrByID") IsNot Nothing Then
                    sSelectedCrByID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedCrByID")))
                End If
                If Request.QueryString("SelectedIndexID") IsNot Nothing Then
                    iSelectedIndexID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedIndexID")))
                End If

                If Request.QueryString("SelId") IsNot Nothing Then
                    sSelId = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelId")))
                End If

                'Files Details
                txtID.Text = "0" : txtPreId.Text = "1" : iPageNext = 0
                sBaseName = sSelectedChecksIDs.Split(",")
                For i = 0 To sBaseName.Length - 1
                    lstDocument.Items.Add(sBaseName(i))
                Next

                'txtNavDoc.Text = "1 Of " & lstDocument.Items.Count
                txtNavDoc.Text = 1
                lblNavDoc.Text = "/" & lstDocument.Items.Count
                If lstDocument.Items.Count = 1 Then
                    txtNavDoc.Enabled = False
                End If

                Dim iDocSelectedID As Integer = 0, iFileSelectedID As Integer = 0
                If lstDocument.Items.Count <> 0 Then
                    If Request.QueryString("DocumentSelectedID") IsNot Nothing Then
                        Try
                            iDocSelectedID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("DocumentSelectedID")))
                            txtNavDoc.Text = iDocSelectedID + 1
                        Catch ex As Exception
                        End Try
                    End If
                    lstDocument.SelectedIndex = iDocSelectedID
                    lstDocument_SelectedIndexChanged(sender, e)
                End If

                If lstFiles.Items.Count <> 0 Then
                    If Request.QueryString("FileSelectedID") IsNot Nothing Then
                        Try
                            iFileSelectedID = objclsFASGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FileSelectedID")))
                            txtNav.Text = iFileSelectedID + 1
                            iPageNext = iFileSelectedID
                        Catch ex As Exception
                        End Try
                    End If
                    lstFiles.SelectedIndex = iFileSelectedID
                    lstFiles_SelectedIndexChanged(sender, e)
                End If

                'If lstDocument.Items.Count <> 0 Then
                '    lstDocument.SelectedIndex = 0
                '    lstDocument_SelectedIndexChanged(sender, e)
                'End If

                'If lstFiles.Items.Count <> 0 Then
                '    lstDocument.SelectedIndex = 0
                '    lstFiles_SelectedIndexChanged(sender, e)
                'End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub imgbtnPreviousNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNavDoc.Click
        Dim dtFiles As New DataTable
        Try
            lblError.Text = "" : iPageNext = 0
            If lstDocument.Items.Count <> -1 Then
                lstFiles.Items.Clear()
                If lstDocument.Items.Count = txtID.Text Then
                    If Val(txtID.Text) = 0 Then
                        'txtNavDoc.Text = "1 Of " & lstDocument.Items.Count
                        txtNavDoc.Text = 1
                        lblNavDoc.Text = "/" & lstDocument.Items.Count
                    Else
                        txtPreId.Text = Val(txtPreId.Text) - 1
                        'txtNavDoc.Text = "" & Val(txtPreId.Text) & " Of " & lstDocument.Items.Count
                        txtNavDoc.Text = Val(txtPreId.Text)
                        lblNavDoc.Text = "/" & lstDocument.Items.Count
                    End If
                    txtID.Text = Val(txtID.Text) - 2
                    If lstDocument.SelectedIndex > 0 Then
                        dtFiles = objclsSearch.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                        If dtFiles.Rows.Count <> 0 Then
                            For i = 0 To dtFiles.Rows.Count - 1
                                lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                            Next
                            'txtNav.Text = "1 Of " & lstFiles.Items.Count
                            txtNav.Text = 1
                            lblNav.Text = "/" & lstFiles.Items.Count
                        Else
                            txtNav.Text = ""
                        End If
                    End If
                    iPageNext = 0
                    If lstDocument.Items.Count <> 0 Then
                        lstDocument.SelectedIndex = Val(txtID.Text)
                        lstDocument_SelectedIndexChanged(sender, e)
                    Else
                        sImgFilePath = ""
                        lblError.Text = "No Data." : lblSearchImageViewValidationMsg.Text = "No Data."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
                    End If
                    If lstFiles.Items.Count <> 0 Then
                        lstFiles.SelectedIndex = 0
                        lstFiles_SelectedIndexChanged(sender, e)
                    Else
                        sImgFilePath = ""
                        lblError.Text = "No Data." : lblSearchImageViewValidationMsg.Text = "No Data."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
                    End If
                Else
                    txtPreId.Text = Val(txtPreId.Text) - 1
                    'txtNavDoc.Text = "" & Val(txtPreId.Text) & " Of " & lstDocument.Items.Count
                    txtNavDoc.Text = Val(txtPreId.Text)
                    lblNavDoc.Text = "/" & lstDocument.Items.Count

                    txtID.Text = Val(txtID.Text) - 1
                    If lstDocument.SelectedIndex > 0 Then
                        dtFiles = objclsSearch.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                        If dtFiles.Rows.Count <> 0 Then
                            For i = 0 To dtFiles.Rows.Count - 1
                                lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                            Next
                            'txtNav.Text = "1 Of " & lstFiles.Items.Count
                            txtNav.Text = 1
                            lblNav.Text = "/" & lstFiles.Items.Count
                        Else
                            txtNav.Text = ""
                        End If
                    End If
                    iPageNext = 0
                    If lstDocument.Items.Count <> 0 Then
                        lstDocument.SelectedIndex = Val(txtID.Text)
                        lstDocument_SelectedIndexChanged(sender, e)
                    Else
                        sImgFilePath = ""
                        lblError.Text = "No Data." : lblSearchImageViewValidationMsg.Text = "No Data."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
                    End If
                    If lstFiles.Items.Count <> 0 Then
                        lstFiles.SelectedIndex = 0
                        lstFiles_SelectedIndexChanged(sender, e)
                    Else
                        sImgFilePath = ""
                        lblError.Text = "No Data." : lblSearchImageViewValidationMsg.Text = "No Data."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPreviousNavDoc_Click")
        End Try
    End Sub
    Private Sub imgbtnNextNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNextNavDoc.Click
        Dim dtFiles As New DataTable
        Try
            lblError.Text = "" : iPageNext = 0
            If lstDocument.Items.Count <= txtID.Text Then
                txtID.Text = lstDocument.Items.Count
                Exit Sub
            End If
            txtID.Text = Val(txtID.Text) + 1
            If lstDocument.Items.Count <> txtID.Text Then
                If lstDocument.Items.Count <> -1 Then
                    lstFiles.Items.Clear()
                    If lstDocument.Items.Count >= txtID.Text Then
                        txtPreId.Text = Val(txtPreId.Text) + 1
                        'txtNavDoc.Text = "" & Val(txtID.Text) + 1 & " Of " & lstDocument.Items.Count
                        txtNavDoc.Text = Val(txtID.Text) + 1
                        lblNavDoc.Text = "/" & lstDocument.Items.Count

                        If lstDocument.SelectedIndex > 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            dtFiles = objclsSearch.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                            If dtFiles.Rows.Count <> 0 Then
                                For i = 0 To dtFiles.Rows.Count - 1
                                    lstFiles.Items.Add(dtFiles.Rows(i)("pge_basename"))
                                Next
                                'txtNav.Text = "1 Of " & lstFiles.Items.Count
                                txtNav.Text = 1
                                lblNav.Text = "/" & lstFiles.Items.Count
                            Else
                                txtNav.Text = ""
                            End If
                        End If

                        If lstFiles.Items.Count = 1 Then
                            txtNav.Enabled = False
                        End If

                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data." : lblSearchImageViewValidationMsg.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data." : lblSearchImageViewValidationMsg.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalSearchImageViewValidation').modal('show');", True)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNextNavDoc_Click")
        End Try
    End Sub
    Private Sub imgbtnNavDocFastRewind_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNavDocFastRewind.Click
        Try
            lblError.Text = ""
            txtPreId.Text = 2
            txtID.Text = 1
            imgbtnPreviousNavDoc_Click(sender, e)
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNavDocFastRewind_Click")
        End Try
    End Sub
    Private Sub imgbtnNavDocFastForword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNavDocFastForword.Click
        Try
            lblError.Text = ""
            txtPreId.Text = lstDocument.Items.Count - 1
            txtID.Text = lstDocument.Items.Count - 2
            imgbtnNextNavDoc_Click(sender, e)
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNavDocFastForword_Click")
        End Try
    End Sub

    Private Sub imgbtnPreviousNav_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNav.Click
        Try
            lblError.Text = ""
            If lstFiles.Items.Count > 0 Then
                If lstFiles.Items.Count <> -1 And iPageNext > 0 Then
                    iPageNext = iPageNext - 1
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                    Dim iPage As Integer = iPageNext
                    'txtNav.Text = iPage + 1 & " of " & lstFiles.Items.Count
                    txtNav.Text = iPage + 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                Else
                    iPageNext = 0
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPreviousNav_Click")
        End Try
    End Sub
    Private Sub imgbtnNextNav_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNextNav.Click
        Try
            lblError.Text = ""
            If iNextPage <> 0 Then
                iPageNext = iNextPage
                iNextPage = 0
            End If
            If lstFiles.Items.Count = 0 Then
                Exit Sub
            End If
            If lstFiles.Items.Count > iPageNext Then
                iPageNext = iPageNext + 1
                If lstFiles.SelectedIndex <> -1 And lstFiles.Items.Count > iPageNext Then
                    lstFiles.SelectedIndex = iPageNext
                    lstFiles_SelectedIndexChanged(sender, e)
                    Dim iPage As Integer = iPageNext
                    'txtNav.Text = iPage + 1 & " of " & lstFiles.Items.Count
                    txtNav.Text = iPage + 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = "" : txtNavDoc.Text = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNextNav_Click")
        End Try
    End Sub

    Private Sub imgbtnFastRewind_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnFastRewind.Click
        Try
            lblError.Text = ""
            iPageNext = 0
            If lstFiles.SelectedIndex > 0 Then
                lstFiles.SelectedIndex = iPageNext
                lstFiles_SelectedIndexChanged(sender, e)
                If lstFiles.Items.Count > 0 Then
                    'txtNav.Text = "1" & " of " & lstFiles.Items.Count
                    txtNav.Text = 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnFastRewind_Click")
        End Try
    End Sub
    Private Sub imgbtnFastForword_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnFastForword.Click
        Try
            lblError.Text = ""
            If lstFiles.Items.Count > 0 Then
                iPageNext = lstFiles.Items.Count
                iPageNext = iPageNext - 1
                lstFiles.SelectedIndex = iPageNext
                lstFiles_SelectedIndexChanged(sender, e)
                'txtNav.Text = iPageNext + 1 & " of " & lstFiles.Items.Count
                txtNav.Text = iPageNext + 1
                lblNav.Text = "/" & lstFiles.Items.Count
            Else
                iPageNext = 0
                lstFiles.SelectedIndex = iPageNext
                lstFiles_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnFastForword_Click")
        End Try
    End Sub

    Private Sub lstFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFiles.SelectedIndexChanged
        Dim sFile As String
        Dim sExt As String
        Dim dt As New DataTable, dtIndexType As New DataTable
        Try
            pnlImageView.Visible = False : pnlDocView.Visible = False : lblDocumentTypeH.Visible = False : lblDoucmentType.Visible = False : RetrieveImage.Visible = False
            If lstFiles.Items.Count = -1 Then Exit Sub
            lblDocID.Text = lstFiles.SelectedItem.Text
            sFile = objclsSearch.GetPageFromEdict(sSession.AccessCode, lstFiles.SelectedItem.Text)
            sImgFilePath = sFile
            If Trim(sFile.Length) = 0 Then Exit Sub
            sExt = Path.GetExtension(sFile)
            sExt = sExt.Remove(0, 1)
            Dim iDocType As Integer
            Select Case UCase(sExt)
                Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                    pnlImageView.Visible = True : pnlDocView.Visible = False : RetrieveImage.Visible = True
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                    Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                    RetrieveImage.ImageUrl = imageDataURL
                Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PDF", "PPS", "XLR", "XML"
                    pnlImageView.Visible = False : pnlDocView.Visible = True : lblDocumentTypeH.Visible = True : lblDoucmentType.Visible = True : RetrieveImage.Visible = False
                    lblFileType.Text = sExt
                    lblSize.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "Size")
                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    iDocType = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                    dgIndex.DataBind()
            End Select
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstFiles_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub lstDocument_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDocument.SelectedIndexChanged
        Dim dtDocument As New DataTable
        Dim i As Integer
        Try
            If lstDocument.Items.Count <> -1 Then
                lstFiles.Items.Clear()
                dtDocument = objclsSearch.LoadListFiles(sSession.AccessCode, lstDocument.SelectedItem.Text)
                If dtDocument.Rows.Count <> 0 Then
                    For i = 0 To dtDocument.Rows.Count - 1
                        lstFiles.Items.Add(dtDocument.Rows(i)("pge_basename"))
                    Next
                    'txtNav.Text = "1 Of " & lstFiles.Items.Count
                    txtNav.Text = 1
                    lblNav.Text = "/" & lstFiles.Items.Count
                End If
                If lstFiles.Items.Count = 1 Then
                    txtNav.Enabled = False
                End If
                If lstFiles.Items.Count <> 0 Then
                    lstFiles.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstDocument_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub lnkOpenDocument_Click(sender As Object, e As EventArgs) Handles lnkOpenDocument.Click
        Dim sFile As String
        Dim sExt As String
        Dim sOpenFile As String
        Try
            Dim sVersionDir As String
            sVersionDir = "C:\Temp\MMCS\BITMAPS\VERSION\" & lblDocID.Text \ 301 & "\"
            If System.IO.File.Exists(sVersionDir) = False Then
                System.IO.Directory.CreateDirectory(sVersionDir)
            End If
            sExt = objclsSearch.GetExtension(sSession.AccessCode, lblDocID.Text)
            If sExt.StartsWith(".") Then
                sExt = sExt.Remove(0, 1)
            End If
            sOpenFile = sVersionDir & lblDocID.Text & "." & sExt

            'To Get Original File Location
            sFile = objclsSearch.GetPageFromEdict(sSession.AccessCode, lblDocID.Text, sExt)
            DownloadMyFile(sFile)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkOpenDocument_Click")
        End Try
    End Sub
    Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim dtDoc As New DataTable
        Dim oImageViewID As New Object, oSelectedIndexID As New Object, oSelectedChecksIDs As New Object, oSelId As Object
        Try
            lblError.Text = ""
            If sSelectedChecksIDs.Length > 0 Then
                dtDoc = objclsSearch.SearchDocuments(sSession.AccessCode, sSession.UserID, sSelectedCabID, sSelectedSubCabID, sSelectedFolID, sSelectedDocTypeID, sSelectedKWID, sSelectedDescID, "", "", "", "", sSelectedFrmtID, "", sSelectedCrByID)
                If dtDoc.Rows.Count > 0 Then
                    sSession.dtDocoImageViewID = dtDoc
                    Session("AllSession") = sSession
                    oImageViewID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(2))
                    oSelectedIndexID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(iSelectedIndexID))
                    oSelectedChecksIDs = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelectedChecksIDs))
                    oSelId = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(sSelId))
                    Response.Redirect(String.Format("~/Search/Search.aspx?ImageViewID={0}&SelectedIndexID={1}&SelectedChecksIDs={2}&SelId={3}", oImageViewID, oSelectedIndexID, oSelectedChecksIDs, oSelId), False)
                    Exit Sub
                Else
                    lblError.Text = "No documents found in this Collation."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No documents found in this Collation','', 'info');", True)
                End If
            Else
                lblError.Text = "No documents found in this Collation."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No documents found in this Collation','', 'info');", True)
            End If
            Response.Redirect(String.Format("~/Search/Search.aspx?ImageViewID={0}", oImageViewID), False)

            'If sSelectedChecksIDs.Length > 0 Then
            '    dtDoc = objclsSearch.SearchDocuments(sSession.AccessCode, sSession.UserID, "", "", "", "", "", "", "", "", "", "", "", sSelectedChecksIDs)
            '    If dtDoc.Rows.Count > 0 Then
            '        sSession.dtDocoImageViewID = dtDoc
            '        Session("AllSession") = sSession
            '    End If
            'End If
            'oImageViewID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(2))
            'oSearchIndexID = HttpUtility.UrlDecode(objclsFASGeneral.EncryptQueryString(iSearchIndexID))
            'Response.Redirect(String.Format("~/Search/Search.aspx?ImageViewID={0}&SearchIndexID={1}", oImageViewID, oSearchIndexID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub imgbtnRotate90_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRotate90.Click
        Try
            '        //get the path to the image
            'String Path = Server.MapPath(Image1.ImageUrl);

            '//create an image object from the image in that path
            'System.Drawing.Image img = System.Drawing.Image.FromFile(Path);

            '//rotate the image
            'img.RotateFlip(RotateFlipType.Rotate90FlipXY);

            '//save the image out to the file
            'img.Save(Path);

            '//release image file
            'img.Dispose();
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRotate90_Click")
        End Try
    End Sub
End Class
