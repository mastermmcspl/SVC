Imports System
Imports System.Data
Imports BusinesLayer
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.Object
Partial Class DigitalFiling_Indexing
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFiling_Indexing"
    Private sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objIndex As New clsIndexing
    Private Shared iAttachID As Integer

    Private Shared iDocID As Integer
    Dim dt As New DataTable
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Private objclsModulePermission As New clsModulePermission
    Private Shared sPTAoD As String
    Private Shared sPTAP As String
    Private Shared sPTED As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnIndex.ImageUrl = "~/Images/Index24.png"
        imgbtnIndexSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Private Sub DigitalFiling_Indexing_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnIndex.Visible = False : btnRemoteIndex.Visible = False

                '//Preeti
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FU")
                sPTAoD = "NO" : sPTAP = "NO" : sPTED = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/DigitalFilingPermission.aspx", False) 'Permissions/DigitalFilingPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnIndex.Visible = False : btnRemoteIndex.Visible = False
                        sPTED = "YES"
                    End If
                End If

                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt
                BindCabinet()
                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                ddlFileType.Visible = False
                BindFileType() : VisibleFalse() : BindTrType() : LoadCustomers() : BindBatchNo(0, 0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DigitalFiling_Indexing_Load")
        End Try
    End Sub
    Private Sub BindCabinet()
        Try
            ddlCabinet.DataSource = objIndex.LoadCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            ddlCabinet.DataTextField = "CBN_NAME"
            ddlCabinet.DataValueField = "CBN_NODE"
            ddlCabinet.DataBind()
            ddlCabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnAddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Try
            lblError.Text = "" : iDocID = 0

            'Extra'
            If rboRemote.Checked = False And rboNormal.Checked = False Then
                lblError.Text = "Select Upload Type"
                Exit Sub
            End If
            'Extra'

            'Extra
            If rboRemote.Checked = True Then
                If ddlFileType.SelectedIndex = 0 Then
                    lblError.Text = "Select File Type"
                    Exit Sub
                End If
            End If
            'Extra

            Dim hfc As HttpFileCollection = Request.Files
            'Extra
            If ddlFileType.SelectedIndex = 1 Then
                If hfc.Count = 1 Then
                    lblError.Text = "1 file can not be make as batch,select more than 1"
                    Exit Sub
                End If
            End If
            'Extra
            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dt.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        dt = Session("Attachment")
                        If dt.Rows.Count = 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)

                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        ElseIf dt.Rows.Count > 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        End If
                    End If
                Next
            End If

            If dt.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            End If

            Session("Attachment") = dt
            gvattach.DataSource = dt
            gvattach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindBatchNo(ByVal iCustomerID As Integer, ByVal iTrType As Integer)
        Try
            ddlBatchNo.DataSource = objIndex.BindBatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iCustomerID, iTrType)
            ddlBatchNo.DataValueField = "BT_ID"
            ddlBatchNo.DataTextField = "BT_BatchNo"
            ddlBatchNo.DataBind()
            ddlBatchNo.Items.Insert(0, "--- Select BatchNo ---")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnIndex_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnIndex.Click
        Dim chkSelect As CheckBox
        Dim iCount As Integer = 0
        Try
            If gvattach.Rows.Count > 0 Then
                For i = 0 To gvattach.Rows.Count - 1
                    chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                    If chkSelect.Checked = True Then
                        iCount = iCount + 1
                    End If
                Next
                If iCount = 0 Then
                    lblValidationMsg.Text = "Select file to Index."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            Else
                lblValidationMsg.Text = "Attach file to Index."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnIndex_Click")
        End Try
    End Sub
    Private Sub gvattach_PreRender(sender As Object, e As EventArgs) Handles gvattach.PreRender
        Try
            If gvattach.Rows.Count > 0 Then
                gvattach.UseAccessibleHeader = True
                gvattach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvattach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach_PreRender")
        End Try
    End Sub
    Private Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlCabinet.SelectedIndex > 0 Then
                ddlSubcabinet.DataSource = objIndex.LoadSubCabinet(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue)
                ddlSubcabinet.DataTextField = "CBN_NAME"
                ddlSubcabinet.DataValueField = "CBN_NODE"
                ddlSubcabinet.DataBind()
                ddlSubcabinet.Items.Insert(0, "Select Sub-Cabinet")
            Else
                ddlSubcabinet.Items.Clear() : ddlFolder.Items.Clear() : ddlType.Items.Clear()
                lblError.Text = "Select Cabinet." : lblValidationMsg.Text = "Select Cabinet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlSubcabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubcabinet.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlCabinet.SelectedIndex > 0 And ddlSubcabinet.SelectedIndex > 0 Then
                ddlFolder.DataSource = objIndex.LoadFolder(sSession.AccessCode, sSession.AccessCodeID, ddlSubcabinet.SelectedValue)
                ddlFolder.DataTextField = "FOL_Name"
                ddlFolder.DataValueField = "Fol_FolID"
                ddlFolder.DataBind()
                ddlFolder.Items.Insert(0, "Select Folder")
            Else
                ddlFolder.Items.Clear() : ddlType.Items.Clear()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubcabinet_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlFolder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFolder.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlFolder.SelectedIndex > 0 Then
                ddlType.DataSource = objIndex.LoadDocumentType(sSession.AccessCode, sSession.AccessCodeID)
                ddlType.DataTextField = "DOT_DOCNAME"
                ddlType.DataValueField = "DOT_DOCTYPEID"
                ddlType.DataBind()
                ddlType.Items.Insert(0, "Select Document Type")
            Else
                ddlType.Items.Clear()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFolder_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Dim dt As New DataTable, dtKey As New DataTable
        Try
            lblError.Text = "" : lblModelError.Text = ""
            dt = objIndex.LoadDescriptors(sSession.AccessCode, sSession.AccessCodeID, ddlType.SelectedValue)
            gvDocumentType.DataSource = dt
            gvDocumentType.DataBind()

            dtKey = objIndex.LoadKeyWords()
            gvKeywords.DataSource = dtKey
            gvKeywords.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlType_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnIndexSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnIndexSave.Click
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
        Dim Arr() As String
        Dim dDate As Date
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        Try
            If ddlCabinet.SelectedIndex = 0 Then
                lblModelError.Text = "Select Cabinet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlCabinet.Focus()
                Exit Sub
            Else
                icabinetID = ddlCabinet.SelectedValue
            End If

            If ddlSubcabinet.SelectedIndex = 0 Then
                lblModelError.Text = "Select Sub Cabinet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlSubcabinet.Focus()
                Exit Sub
            Else
                iSubCabinet = ddlSubcabinet.SelectedValue
            End If

            If ddlFolder.SelectedIndex = 0 Then
                lblModelError.Text = "Select Folder."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlFolder.Focus()
                Exit Sub
            Else
                iFolder = ddlFolder.SelectedValue
            End If

            If ddlType.SelectedIndex = 0 Then
                lblModelError.Text = "Select Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlType.Focus()
                Exit Sub
            Else
                iType = ddlType.SelectedValue
            End If

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                If gvattach.Rows.Count > 0 Then
                    For i = 0 To gvattach.Rows.Count - 1
                        chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                        lblPath = gvattach.Rows(i).FindControl("lblPath")
                        If chkSelect.Checked = True Then
                            sPageExt = UCase(gvattach.Rows(i).Cells(3).Text)
                            sFilePath = lblPath.Text
                            sFileName = gvattach.Rows(i).Cells(2).Text
                            objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objclsFASGeneral.SafeSQL(txtTitle.Text.Trim)
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt
                            If gvKeywords.Rows.Count > 0 Then

                                For k = 0 To gvKeywords.Rows.Count - 1
                                    txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
                                    If txtKeywords.Text <> "" Then
                                        sKeywords = sKeywords & "," & txtKeywords.Text
                                    End If
                                Next
                            End If
                            If sKeywords.StartsWith(",") = True Then
                                sKeywords = sKeywords.Remove(0, 1)
                            End If
                            If sKeywords.EndsWith(",") = True Then
                                sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
                            End If
                            objIndex.sPGEKeyWORD = objclsFASGeneral.SafeSQL(sKeywords)
                            objIndex.sPGEOCRText = ""
                            objIndex.iPGESIZE = 0
                            objIndex.iPGECURRENT_VER = 0
                            Select Case UCase(sPageExt)
                                Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                                    objIndex.sPGEOBJECT = "IMAGE"
                                Case Else
                                    objIndex.sPGEOBJECT = "OLE"
                            End Select
                            objIndex.sPGESTATUS = "A"
                            objIndex.iPGESubCabinet = iSubCabinet
                            objIndex.iPgeUpdatedBy = sSession.UserID

                            objIndex.spgeDelflag = "A"
                            objIndex.iPGEQCUsrGrpId = 0
                            objIndex.sPGEFTPStatus = "F"
                            objIndex.iPGEbatchname = objIndex.iPGEBASENAME
                            objIndex.spgeOrignalFileName = objclsFASGeneral.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
                            objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)

                            If gvDocumentType.Rows.Count > 0 Then
                                For j = 0 To gvDocumentType.Rows.Count - 1
                                    lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                                    txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                                    If objIndex.iPGEBASENAME = iPageDetailsid Then
                                        objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If Arr(0) = "3" Then
                        lblValidationMsg.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnIndexSave_Click")
        End Try
    End Sub
    Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            If sFileInDB = "FALSE" Then
                sImagePath = objIndex.GetImagePath(sSession.AccessCode)
                sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
                sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                If System.IO.File.Exists(sImagePath) = False Then
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Public Sub BindFileType()
        Try
            ddlFileType.Items.Insert(0, "Select File Type")

            ddlFileType.Items.Insert(1, "Batch File")
            ddlFileType.Items.Insert(2, "Normal File")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub VisibleFalse()
        Try
            lblCustomer.Visible = False : ddlCustomer.Visible = False : lblBatchNo.Visible = False : ddlBatchNo.Visible = False
            lblTrType.Visible = False : lblNoOfTr.Visible = False : lblDebitTotal.Visible = False : lblCreditTotal.Visible = False
            ddlTrType.Visible = False : txtNoOfTr.Visible = False : txtDebitTotal.Visible = False : txtCreditTotal.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub VisibleTrue()
        Try
            lblCustomer.Visible = True : ddlCustomer.Visible = True : lblBatchNo.Visible = True : ddlBatchNo.Visible = True
            lblTrType.Visible = True : lblNoOfTr.Visible = True : lblDebitTotal.Visible = True : lblCreditTotal.Visible = True
            ddlTrType.Visible = True : txtNoOfTr.Visible = True : txtDebitTotal.Visible = True : txtCreditTotal.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlFileType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFileType.SelectedIndexChanged
        Try
            ddlTrType.SelectedIndex = 0 : txtNoOfTr.Text = "" : txtDebitTotal.Text = "" : txtCreditTotal.Text = 0
            If ddlFileType.SelectedIndex = 1 Then
                VisibleTrue()
            Else
                VisibleFalse()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindTrType()
        Try
            ddlTrType.Items.Insert(0, "Select Transaction Type")
            ddlTrType.Items.Insert(1, "Purchase")
            ddlTrType.Items.Insert(2, "Sales")
            ddlTrType.Items.Insert(3, "Payment")
            ddlTrType.Items.Insert(4, "Receipt")
            ddlTrType.Items.Insert(5, "Petty Cash")
            ddlTrType.Items.Insert(6, "Journal Entry")
            ddlTrType.Items.Insert(7, "GIN")
            ddlTrType.Items.Insert(8, "Purchase Return")
            ddlTrType.Items.Insert(9, "Cash Purchase")
            ddlTrType.Items.Insert(10, "Cash Sales")
            ddlTrType.Items.Insert(11, "Sales Dispatch")
            ddlTrType.Items.Insert(12, "Sales Invoice")
            ddlTrType.Items.Insert(13, "Sales Return")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCustomers()
        Try
            ddlCustomer.DataSource = objIndex.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomer.DataTextField = "MDA_CompanyName"
            ddlCustomer.DataValueField = "MDA_ID"

            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, "Select Company Name")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub AutomaticIndexing()
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
        Dim Arr() As String
        Dim dDate As Date
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        'Dim iCabinet As Integer
        'Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable
        Dim bCheckCabinet As Boolean
        Try
            If ddlCustomer.SelectedIndex = 0 Then
                lblModelError.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlCustomer.Focus()
                Exit Sub
            Else
                icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCustomer.SelectedItem.Text)
            End If

            If ddlTrType.SelectedIndex = 0 Then
                lblModelError.Text = "Select Transaction Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlTrType.Focus()
                Exit Sub
            Else
                iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, ddlTrType.SelectedItem.Text)
            End If

            If ddlBatchNo.SelectedIndex = 0 Then
                lblModelError.Text = "Select BatchNo."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                ddlBatchNo.Focus()
                Exit Sub
            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, ddlBatchNo.SelectedItem.Text)
            End If

            iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

            'If ddlType.SelectedIndex = 0 Then
            '    lblModelError.Text = "Select Type."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            '    ddlType.Focus()
            '    Exit Sub
            'Else
            '    iType = ddlType.SelectedValue
            'End If

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                If gvattach.Rows.Count > 0 Then
                    For i = 0 To gvattach.Rows.Count - 1
                        chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                        lblPath = gvattach.Rows(i).FindControl("lblPath")
                        If chkSelect.Checked = True Then
                            sPageExt = UCase(gvattach.Rows(i).Cells(3).Text)
                            sFilePath = lblPath.Text
                            sFileName = gvattach.Rows(i).Cells(2).Text
                            objIndex.iPGEBASENAME = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objclsFASGeneral.SafeSQL(txtTitle.Text.Trim)
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt
                            If gvKeywords.Rows.Count > 0 Then

                                For k = 0 To gvKeywords.Rows.Count - 1
                                    txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
                                    If txtKeywords.Text <> "" Then
                                        sKeywords = sKeywords & "," & txtKeywords.Text
                                    End If
                                Next
                            End If
                            If sKeywords.StartsWith(",") = True Then
                                sKeywords = sKeywords.Remove(0, 1)
                            End If
                            If sKeywords.EndsWith(",") = True Then
                                sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
                            End If
                            objIndex.sPGEKeyWORD = objclsFASGeneral.SafeSQL(sKeywords)
                            objIndex.sPGEOCRText = ""
                            objIndex.iPGESIZE = 0
                            objIndex.iPGECURRENT_VER = 0
                            Select Case UCase(sPageExt)
                                Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                                    objIndex.sPGEOBJECT = "IMAGE"
                                Case Else
                                    objIndex.sPGEOBJECT = "OLE"
                            End Select
                            objIndex.sPGESTATUS = "A"
                            objIndex.iPGESubCabinet = iSubCabinet
                            objIndex.iPgeUpdatedBy = sSession.UserID

                            objIndex.spgeDelflag = "A"
                            objIndex.iPGEQCUsrGrpId = 0
                            objIndex.sPGEFTPStatus = "F"
                            objIndex.iPGEbatchname = objIndex.iPGEBASENAME
                            objIndex.spgeOrignalFileName = objclsFASGeneral.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
                            objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)

                            If gvDocumentType.Rows.Count > 0 Then
                                For j = 0 To gvDocumentType.Rows.Count - 1
                                    lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                                    txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                                    If objIndex.iPGEBASENAME = iPageDetailsid Then
                                        objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If Arr(0) = "3" Then
                        lblValidationMsg.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AutomaticIndexing")
        End Try
    End Sub
    Private Sub ddlTrType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTrType.SelectedIndexChanged
        Try
            If ddlCustomer.SelectedIndex > 0 Then
                If ddlTrType.SelectedIndex > 0 Then
                    BindBatchNo(ddlCustomer.SelectedValue, ddlTrType.SelectedIndex)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub rboRemote_CheckedChanged(sender As Object, e As EventArgs) Handles rboRemote.CheckedChanged
        Try
            imgbtnIndex.Visible = False : btnRemoteIndex.Visible = True
            ddlFileType.SelectedIndex = 0
            ddlFileType.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub rboNormal_CheckedChanged(sender As Object, e As EventArgs) Handles rboNormal.CheckedChanged
        Try
            imgbtnIndex.Visible = True : btnRemoteIndex.Visible = False
            ddlFileType.SelectedIndex = 0
            VisibleFalse()
            ddlFileType.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnRemoteIndex_Click(sender As Object, e As EventArgs) Handles btnRemoteIndex.Click
        Dim objBatch As clsIndexing.BatchScan
        Dim Arr() As String
        Try
            If gvattach.Rows.Count > 0 Then
                If ddlFileType.SelectedIndex = 1 Then    'Batch Scan
                    If ddlCustomer.SelectedIndex = 0 Then
                        lblError.Text = "Select Customer"
                        Exit Sub
                    End If
                    If ddlTrType.SelectedIndex = 0 Then
                        lblError.Text = "Select Transaction Type"
                        Exit Sub
                    End If
                    'If txtNoOfTr.Text = "" Then
                    '    lblError.Text = "Enter No Of Transactions"
                    '    Exit Sub
                    'End If
                    'If txtDebitTotal.Text = "" Then
                    '    lblError.Text = "Enter Debit Total"
                    '    Exit Sub
                    'End If
                    'If txtCreditTotal.Text = "" Then
                    '    lblError.Text = "Enter Credit Total"
                    '    Exit Sub
                    'End If
                End If

                'Extra
                If ddlFileType.SelectedIndex = 1 Then
                    'Batch Scan
                    objBatch.BT_ID = 0
                    objBatch.BT_CustomerID = ddlCustomer.SelectedValue
                    objBatch.BT_BatchNo = objIndex.GenerateOrderCode(sSession.AccessCode, sSession.AccessCodeID)
                    objBatch.BT_TrType = ddlTrType.SelectedIndex
                    If txtNoOfTr.Text <> "" Then
                        objBatch.BT_NoOfTransaction = txtNoOfTr.Text
                    Else
                        objBatch.BT_NoOfTransaction = gvattach.Rows.Count
                    End If
                    If txtDebitTotal.Text <> "" Then
                        objBatch.BT_DebitTotal = txtDebitTotal.Text
                    Else
                        objBatch.BT_DebitTotal = 0
                    End If
                    If txtCreditTotal.Text <> "" Then
                        objBatch.BT_CreditTotal = txtCreditTotal.Text
                    Else
                        objBatch.BT_CreditTotal = 0
                    End If
                    objBatch.BT_Delflag = "W"
                    objBatch.BT_Status = "W"
                    objBatch.BT_CompID = sSession.AccessCodeID
                    objBatch.BT_YearID = sSession.YearID
                    objBatch.BT_CrBy = sSession.UserID
                    objBatch.BT_CrOn = Date.Today
                    objBatch.BT_Operation = "C"
                    objBatch.BT_IPAddress = sSession.IPAddress
                    Arr = objIndex.SaveBatchScanDetails(sSession.AccessCode, objBatch)

                    BindBatchNo(ddlCustomer.SelectedValue, ddlTrType.SelectedIndex)
                    ddlBatchNo.SelectedValue = Arr(1)

                End If
                'Extra

                AutomaticIndexing()
                ddlCustomer.SelectedIndex = 0 : ddlTrType.SelectedIndex = 0 : txtNoOfTr.Text = "" : txtDebitTotal.Text = "" : txtCreditTotal.Text = ""
            Else
                lblError.Text = "Add the files before index"
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
