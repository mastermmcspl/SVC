Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Globalization
Imports System.Text.RegularExpressions
Partial Class Viewer_ImageView
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Seacrh ImageView"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsEDICTGeneral As New clsEDICTGeneral
    Private objclsSearch As New clsSearch
    Private objclsView As New clsView

    Private objclsCollation As New clsCollation
    Private Shared sSession As AllSession
    Private Shared iSelectedFirstID As Integer = 0
    Private Shared iNextID As Integer = 1
    Private Shared iPageNext As Integer
    Private Shared sImgFilePath As String = ""
    Private Shared iNextPage As Integer
    Private Shared sBaseName() As String
    Private Shared sSelectedChecksIDs As String = ""

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
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared iCheck As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnNavDocFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNavDoc.ImageUrl = "~/Images/SearchImage/Previous16.png"
        imgbtnNextNavDoc.ImageUrl = "~/Images/SearchImage/Next16.png"
        imgbtnNavDocFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnFastRewind.ImageUrl = "~/Images/SearchImage/Fast-Rewind16.png"
        imgbtnPreviousNav.ImageUrl = "~/Images/SearchImage/Preview16.png"
        imgbtnNextNav.ImageUrl = "~/Images/SearchImage/Nextt16.png"
        imgbtnFastForword.ImageUrl = "~/Images/SearchImage/Fast-Forward16.png"
        imgbtnAnnotation.ImageUrl = "~/Images/Annotation24.png"
        imgbtnDownload.ImageUrl = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dtDocument As New DataTable
        Try
            sSession = Session("AllSession")
            ' documentViewer.Document = String.Format("~/Images/SearchImage/NoImage.jpg")
            If IsPostBack = False Then
                imgbtnDownload.Visible = False
                sSelId = String.Empty : sSelectedChecksIDs = String.Empty
                sSelectedCabID = String.Empty : sSelectedSubCabID = String.Empty : sSelectedFolID = String.Empty
                sSelectedDocTypeID = String.Empty : sSelectedKWID = String.Empty : sSelectedDescID = String.Empty
                sSelectedFrmtID = String.Empty : sSelectedCrByID = String.Empty
                iSelectedIndexID = 0 : iSelectedFirstID = 0

                If Request.QueryString("ImagePath") IsNot Nothing Then
                    sImgFilePath = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ImagePath")))
                End If
                If Request.QueryString("SelectedFirstID") IsNot Nothing Then
                    iSelectedFirstID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFirstID")))
                End If
                If Request.QueryString("SelectedChecksIDs") IsNot Nothing Then
                    sSelectedChecksIDs = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedChecksIDs")))
                End If

                If Request.QueryString("SelectedCabID") IsNot Nothing Then
                    sSelectedCabID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedCabID")))
                End If
                If Request.QueryString("SelectedSubCabID") IsNot Nothing Then
                    sSelectedSubCabID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedSubCabID")))
                End If
                If Request.QueryString("SelectedFolID") IsNot Nothing Then
                    sSelectedFolID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFolID")))
                End If
                If Request.QueryString("SelectedDocTypeID") IsNot Nothing Then
                    sSelectedDocTypeID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedDocTypeID")))
                End If
                If Request.QueryString("SelectedKWID") IsNot Nothing Then
                    sSelectedKWID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedKWID")))
                End If
                If Request.QueryString("SelectedDescID") IsNot Nothing Then
                    sSelectedDescID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedDescID")))
                End If
                If Request.QueryString("SelectedFrmtID") IsNot Nothing Then
                    sSelectedFrmtID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedFrmtID")))
                End If
                If Request.QueryString("SelectedCrByID") IsNot Nothing Then
                    sSelectedCrByID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedCrByID")))
                End If
                If Request.QueryString("SelectedIndexID") IsNot Nothing Then
                    iSelectedIndexID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelectedIndexID")))
                End If

                If Request.QueryString("SelId") IsNot Nothing Then
                    sSelId = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SelId")))
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
                            iDocSelectedID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("DocumentSelectedID")))
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
                            iFileSelectedID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FileSelectedID")))
                            txtNav.Text = iFileSelectedID + 1
                            iPageNext = iFileSelectedID
                        Catch ex As Exception
                        End Try
                    End If
                    lstFiles.SelectedIndex = iFileSelectedID
                    lstFiles_SelectedIndexChanged(sender, e)
                End If

                'BindAnnotaionDetails(Val(lstDocument.SelectedItem.Text), Val(lstFiles.SelectedItem.Text))
                If Request.QueryString("AnnotaionVersion") IsNot Nothing Then
                    If objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AnnotaionVersion"))) = "YES" Then
                        ddlAnnotationVersion.SelectedIndex = ddlAnnotationVersion.Items.Count - 1
                        'ddlAnnotationVersion_SelectedIndexChanged(sender, e)
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                ' RetrieveImage.ImageUrl = ""
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name','', 'error');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub imgbtnPreviousNavDoc_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPreviousNavDoc.Click
        Dim dtFiles As New DataTable
        Dim ivalue As Integer = 0
        Try
            lblError.Text = "" : iPageNext = 0 : iCheck = 0
            If (iCheck = 1 Or lstDocument.Items.Count = 1) Then
                ivalue = Val(txtID.Text) - 1
            Else
                ivalue = Val(txtID.Text)
            End If
            If Val(ivalue) >= 1 Then
                If lstDocument.Items.Count <> -1 Then
                    iCheck = 0
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

                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                    Else
                        txtPreId.Text = Val(txtPreId.Text) - 1
                        'txtNavDoc.Text = "" & Val(txtPreId.Text) & " Of " & lstDocument.Items.Count
                        txtNavDoc.Text = Val(txtPreId.Text)
                        lblNavDoc.Text = "/" & lstDocument.Items.Count

                        txtID.Text = Val(txtID.Text) - 1

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

                        iPageNext = 0
                        If lstDocument.Items.Count <> 0 Then
                            lstDocument.SelectedIndex = Val(txtID.Text)
                            lstDocument_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                        If lstFiles.Items.Count <> 0 Then
                            lstFiles.SelectedIndex = 0
                            lstFiles_SelectedIndexChanged(sender, e)
                        Else
                            sImgFilePath = ""
                            lblError.Text = "No Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                        End If
                    End If
                End If
            Else
                txtID.Text = 0
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Invalid File Name','', 'error');", True)
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
                    iCheck = 1
                    lstFiles.Items.Clear()
                    If lstDocument.Items.Count >= txtID.Text Then
                        txtPreId.Text = Val(txtPreId.Text) + 1
                        'txtNavDoc.Text = "" & Val(txtID.Text) + 1 & " Of " & lstDocument.Items.Count
                        If txtNavDoc.Text > Val(txtID.Text) Then
                            txtNavDoc.Text = Val(txtNavDoc.Text)
                        Else
                            txtNavDoc.Text = Val(txtID.Text) + 1
                        End If

                        lblNavDoc.Text = "/" & lstDocument.Items.Count

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
                txtNavDoc.Text = ""
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
            lstFiles.SelectedIndex = iPageNext
            lstFiles_SelectedIndexChanged(sender, e)
            If lstFiles.Items.Count > 0 Then
                'txtNav.Text = "1" & " of " & lstFiles.Items.Count
                txtNav.Text = 1
                lblNav.Text = "/" & lstFiles.Items.Count
            End If
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
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
        Dim sFileName As String = ""
        Try
            'BindAnnotaionDetails(Val(lstDocument.SelectedItem.Text), Val(lstFiles.SelectedItem.Text))
            If lstFiles.Items.Count = -1 Then Exit Sub
            lblDocID.Text = lstFiles.SelectedItem.Text
            sFile = objclsView.GetPageFromEdict(sSession.AccessCode, lstFiles.SelectedItem.Text, sSession.UserID)
            sImgFilePath = sFile
            If Trim(sFile.Length) = 0 Then Exit Sub
            sExt = Path.GetExtension(sFile)
            sExt = sExt.Remove(0, 1)
            sFileName = objclsSearch.GetFileNames(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
            lblFileName.Text = sFileName
            lnkOpenDocument.Text = sFileName
            lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
            imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False
            Select Case UCase(sExt)
                Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                    imgbtnDownload.Visible = False
                    lnkOpenDocument.Visible = True : lblFileName.Visible = False : lnkOpenDocument.Visible = True
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                    Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                    documentViewer1.ImageUrl = imageDataURL
                    ' documentViewer.Document = sFile
                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False
                    lblFileType.Text = sExt
                    Dim fi As New IO.FileInfo(sFile)
                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                    dgIndex.DataBind()

                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False

                Case "PDF", "TIF", "TIFF"
                    imgbtnDownload.Visible = True
                    lnkOpenDocument.Visible = True : lblFileName.Visible = False : lnkOpenDocument.Visible = True
                    Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
                    'documentViewer.Document = sFile
                    documentViewer1.ImageUrl = imageDataURL
                    lblFileType.Text = sExt
                    Dim fi As New IO.FileInfo(sFile)
                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                    dgIndex.DataBind()

                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False

                Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PDF", "PPS", "XLR", "XML", "TIF", "TIFF"
                    imgbtnDownload.Visible = True
                    lnkOpenDocument.Visible = True : lblFileName.Visible = False : lnkOpenDocument.Visible = True
                    Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
                    documentViewer1.ImageUrl = imageDataURL
                    ' documentViewer.Document = sFile
                    lblFileType.Text = sExt
                    Dim fi As New IO.FileInfo(sFile)
                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
                    dgIndex.DataBind()

                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False
            End Select
        Catch ex As Exception
            If ex.Message.Contains("Could not find file ") = True Then
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstFiles_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
    '    Dim file As System.IO.FileInfo
    '    Try
    '        file = New System.IO.FileInfo(pstrFileNameAndPath)
    '        If file.Exists Then
    '            Response.Clear()
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
    '            Response.AddHeader("Content-Length", file.Length.ToString())
    '            Response.ContentType = "application/octet-stream"
    '            Response.WriteFile(file.FullName)
    '            Response.End()
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
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
                lblError.Text = "Invalid File Name." : lblSearchImageViewValidationMsg.Text = "Invalid File Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalSearchImageViewValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstDocument_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub lnkOpenDocument_Click(sender As Object, e As EventArgs) Handles lnkOpenDocument.Click
    '    Dim sFile As String, sExt As String, sTempPath As String = "", sDestPath As String = "", sFileName As String = ""
    '    Dim sGenPath As String = "", sTargetImage As String = ""
    '    Try
    '        sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")

    '        sExt = objclsGeneralFunctions.GetFileExt(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
    '        sFileName = objclsGeneralFunctions.GetFileOrigName(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
    '        sGenPath = objclsEDICTGeneral.GetOnlyDecPath(sTempPath, sSession.UserID)
    '        sTargetImage = sGenPath & Path.GetFileNameWithoutExtension(sFileName) & "." & sExt
    '        sGenPath = sGenPath & Path.GetFileNameWithoutExtension(sFileName) & "." & sExt

    '        sFile = objclsView.GetPageFrom(sSession.AccessCode, lblDocID.Text, sExt)

    '        sDestPath = objclsEDICTGeneral.GetDecPathView(sTempPath, sSession.UserID, sFile, Path.GetFileNameWithoutExtension(sFileName), sExt)
    '        DownloadMyFile(sTargetImage)
    '        'sTempPath = objclsGeneralFunctions.GetTempPath(sSession.AccessCode, sSession.AccessCodeID, "TempPath")
    '        'If sTempPath.EndsWith("\") = True Then
    '        '    sDestPath = sTempPath & "Temp\Downloads\"
    '        'Else
    '        '    sDestPath = sTempPath & "Temp\Downloads\"
    '        'End If
    '        'objclsGeneralFunctions.ClearBrowseDirectory(sDestPath)
    '        'sFileName = objclsSearch.GetFileNames(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
    '        'sDestPath = sDestPath & sFileName
    '        'sExt = objclsSearch.GetExtension(sSession.AccessCode, lblDocID.Text)
    '        ''To Get Original File Location
    '        'sFile = objclsSearch.GetPageFromEdict(sSession.AccessCode, lblDocID.Text, sExt)
    '        'System.IO.File.Copy(sFile, sDestPath, True)
    '        'DownloadMyFile(sDestPath)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkOpenDocument_Click")
    '    End Try
    'End Sub
    Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Dim str As String = ""
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                str = System.IO.Path.GetFileNameWithoutExtension(file.Name)
                Dim replacestr As String = Regex.Replace(str, "[^a-zA-Z0-9_]+", "")
                Response.AddHeader("Content-Disposition", "attachment; filename=" & replacestr & "." & System.IO.Path.GetExtension(file.Name))
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
                dtDoc = objclsSearch.SearchDocuments(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSelectedCabID, sSelectedSubCabID, sSelectedFolID, sSelectedDocTypeID, sSelectedKWID, sSelectedDescID, "", "", "", "", sSelectedFrmtID, "", sSelectedCrByID)
                If dtDoc.Rows.Count > 0 Then
                    sSession.dtDocoImageViewID = dtDoc
                    Session("AllSession") = sSession
                    oImageViewID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(2))
                    oSelectedIndexID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iSelectedIndexID))
                    oSelectedChecksIDs = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedChecksIDs))
                    oSelId = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelId))
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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Private Sub imgbtnDownload_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDownload.Click
        Try
            DownloadMyFile(sImgFilePath)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDownload_Click")
        End Try
    End Sub
    'Public Sub BindAnnotaionDetails(ByVal iDocumentID As Integer, ByVal iFileID As Integer)
    '    Try
    '        ddlAnnotationVersion.DataSource = objclsSearch.LoadAnnotaionSaved(sSession.AccessCode, sSession.AccessCodeID, iDocumentID, iFileID)
    '        ddlAnnotationVersion.DataTextField = "EAD_OriginalName"
    '        ddlAnnotationVersion.DataValueField = "EAD_PKID"
    '        ddlAnnotationVersion.DataBind()
    '        ddlAnnotationVersion.Items.Insert(0, "Original File")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Private Sub imgbtnAnnotation_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAnnotation.Click
    '    Dim oImgFilePath As New Object, oDocumentID As New Object, oFileID As New Object, oSelectedChecksIDs As Object, oBackToFormID As Object
    '    Dim oSelectedCabID As Object, oSelectedSubCabID As Object, oSelectedFolID As Object, oSelectedDocTypeID As Object, oSelectedKWID As Object, oSelectedDescID As Object
    '    Dim oSelectedFrmtID As Object, oSelectedCrByID As Object, oSelectedIndexID As Object, oSelId As Object, oDocumentSelectedID As Object, oFileSelectedID As Object
    '    Dim sImagePath As String
    '    Try
    '        sImagePath = objclsView.GetPageFromEdict(sSession.AccessCode, lstDocument.SelectedItem.Text, sSession.UserID)
    '        oImgFilePath = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(sImgFilePath))
    '        oDocumentID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(lstDocument.SelectedItem.Text))
    '        oFileID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(lstFiles.SelectedItem.Text))

    '        oSelectedChecksIDs = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedChecksIDs))
    '        oSelId = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelId))
    '        oSelectedCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedCabID))
    '        oSelectedSubCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedSubCabID))
    '        oSelectedFolID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedFolID))
    '        oSelectedDocTypeID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedDocTypeID))
    '        oSelectedKWID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedKWID))
    '        oSelectedDescID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedDescID))
    '        oSelectedFrmtID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedFrmtID))
    '        oSelectedCrByID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedCrByID))
    '        oSelectedIndexID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iSelectedIndexID))

    '        oDocumentSelectedID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(lstDocument.SelectedIndex))
    '        oFileSelectedID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(lstFiles.SelectedIndex))

    '        oBackToFormID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(1))

    '        Response.Redirect(String.Format("~/VSAnnotation/VSSearchAnnotation.aspx?SelId={0}&SelectedChecksIDs={1}&SelectedCabID={2}&SelectedSubCabID={3}&SelectedFolID={4}&SelectedDocTypeID={5}&SelectedKWID={6}&SelectedDescID={7}&SelectedFrmtID={8}&SelectedCrByID={9}&SelectedIndexID={10}&ImgFilePath={11}&DocumentID={12}&FileID={13}&DocumentSelectedID={14}&FileSelectedID={15}&BackToFormID={16}", oSelId, oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedDocTypeID, oSelectedKWID, oSelectedDescID, oSelectedFrmtID, oSelectedCrByID, oSelectedIndexID, oImgFilePath, oDocumentID, oFileID, oDocumentSelectedID, oFileSelectedID, oBackToFormID), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAnnotation_Click")
    '    End Try
    'End Sub
    'Private Sub ddlAnnotationVersion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAnnotationVersion.SelectedIndexChanged
    '    Dim sFile As String
    '    Dim sExt As String
    '    Dim sFileName As String = ""
    '    Try
    '        If ddlAnnotationVersion.SelectedIndex = 0 Then
    '            lstFiles_SelectedIndexChanged(sender, e)
    '        Else
    '            sFile = objclsSearch.GetAnnotaionPageFromEdict(sSession.AccessCode, ddlAnnotationVersion.SelectedValue)
    '            sImgFilePath = sFile
    '            sExt = Path.GetExtension(sFile)
    '            sExt = sExt.Remove(0, 1)
    '            sFileName = objclsSearch.GetFileNames(sSession.AccessCode, sSession.AccessCodeID, lblDocID.Text)
    '            lblFileName.Text = sFileName
    '            lnkOpenDocument.Text = sFileName
    '            Select Case UCase(sExt)
    '                Case "JPG", "JPEG", "BMP", "GIF", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
    '                    lnkOpenDocument.Visible = False : lblFileName.Visible = True : lnkOpenDocument.Visible = False
    '                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sFile)
    '                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
    '                    Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
    '                    documentViewer1.ImageUrl = imageDataURL
    '                    ' documentViewer.Document = sFile
    '                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
    '                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False
    '                    lblFileType.Text = sExt
    '                    Dim fi As New IO.FileInfo(sFile)
    '                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

    '                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
    '                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
    '                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
    '                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
    '                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
    '                    dgIndex.DataBind()

    '                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
    '                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False

    '                Case "PDF", "TIF", "TIFF"
    '                    lnkOpenDocument.Visible = True : lblFileName.Visible = False : lnkOpenDocument.Visible = True
    '                    Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
    '                    documentViewer1.ImageUrl = imageDataURL
    '                    'documentViewer.Document = sFile
    '                    lblFileType.Text = sExt
    '                    Dim fi As New IO.FileInfo(sFile)
    '                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

    '                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
    '                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
    '                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
    '                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
    '                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
    '                    dgIndex.DataBind()

    '                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
    '                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False

    '                Case "TXT", "DOC", "XLS", "XLSX", "PPT", "DOCX", "PPTX", "MSG", "INI", "PDF", "PPS", "XLR", "XML", "TIF", "TIFF"
    '                    lnkOpenDocument.Visible = True : lblFileName.Visible = False : lnkOpenDocument.Visible = True
    '                    Dim imageDataURL As String = String.Format("~/Images/SearchImage/NoImage.jpg")
    '                    documentViewer1.ImageUrl = imageDataURL
    '                    'documentViewer.Document = sFile
    '                    lblFileType.Text = sExt
    '                    Dim fi As New IO.FileInfo(sFile)
    '                    lblSize.Text = (Decimal.Truncate((fi.Length) / 1024)) & " KB"

    '                    lblCreatedBy.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "FullName")
    '                    lblCreatedOn.Text = objclsSearch.GetSearchCrON(sSession.AccessCode, lstFiles.SelectedItem.Text)
    '                    Dim iDocType As Integer = objclsSearch.GetDocTypeID(sSession.AccessCode, lstFiles.SelectedItem.Text)
    '                    lblDoucmentType.Text = objclsSearch.GetName(sSession.AccessCode, lstFiles.SelectedItem.Text, "DocName")
    '                    dgIndex.DataSource = objclsSearch.LoadIndexDetails(sSession.AccessCode, iDocType, lstFiles.SelectedItem.Text)
    '                    dgIndex.DataBind()

    '                    lblHVersion.Visible = False : imgbtnAnnotation.Enabled = False : ddlAnnotationVersion.Enabled = False
    '                    imgbtnAnnotation.Visible = False : ddlAnnotationVersion.Visible = False
    '            End Select
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAnnotationVersion_SelectedIndexChanged")
    '    End Try
    'End Sub

    'Protected Sub dgIndex_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgIndex.ItemDataBound
    '    Dim lblHDescriptor As New Label
    '    Try
    '        If e.Item.ItemType = ListItemType.Header Then
    '            lblHDescriptor = e.Item.FindControl("lblHDescriptor")

    '            lblHDescriptor.Text = "Index Details : " & lblDoucmentType.Text
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgIndex_ItemDataBound")
    '    End Try
    'End Sub
    '<System.Web.Services.WebMethod()>
    'Public Shared Function zxa(ByVal firstNumber As Integer, ByVal secondNumber As Integer) As Integer
    '    Dim objclsEdictGen As New clsEDICTGeneral
    '    Dim sMessage As String
    '    Try
    '        objclsEdictGen.DltDecryptFile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
    '    Catch ex As Exception
    '        sMessage = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "zxa")
    '        'Return sMessage
    '    End Try
    'End Function
End Class
