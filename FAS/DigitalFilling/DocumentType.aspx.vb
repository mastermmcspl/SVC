Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Data
Imports Microsoft.Reporting.WebForms
Partial Class DigitalFilling_DocumentType
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFilling_DocumentType"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsDocumentType As New clsDocumentType
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsSearch As New clsSearch
    Private Shared sSession As AllSession
    Private Shared iDocTypeID As Integer = 0
    Private Shared iDescID As Integer = 0
    Private Shared dtDocType As DataTable
    Private Shared dtDescriptor As DataTable
    Private Shared ObjStr As StrDocType
    Private Shared iConDocId As Integer = 0
    Private objclsModulePermission As New clsModulePermission
    Private Shared sPTAoD As String
    Private Shared sPTAP As String
    Private Shared sPTED As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnAdd.Visible = True : btnDocTypeSave.Visible = True : btnDocTypeUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False
                'imgbtnDeActivate.Visible = False
                '//Preeti
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DT")
                imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False
                imgbtnDeActivate.Visible = False : sPTAoD = "NO" : sPTAP = "NO" : sPTED = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/DigitalFilingPermission.aspx", False) 'Permissions/DigitalFilingPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        imgbtnDeActivate.Visible = True
                        sPTAoD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sPTAP = "YES"
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnAdd.Visible = True : btnDocTypeSave.Visible = True : btnDocTypeUpdate.Visible = False
                        sPTED = "YES"
                    End If
                End If
                BindStatus() : BindDescriptor() : LoadDocTypeDashboard() : LoadDescriptorDetails()
                RFVDocType.ControlToValidate = "txtDocType" : RFVDocType.ErrorMessage = "Enter Document Type."
                REVDocType.ErrorMessage = "Document Type exceeded maximum size(max 400 characters)." : REVDocType.ValidationExpression = "^[\s\S]{0,400}$"
                REVNote.ErrorMessage = "Note exceeded maximum size(max 600 characters)." : REVNote.ValidationExpression = "^[\s\S]{0,600}$"
                RFVDescriptor.InitialValue = "Select Descriptor" : RFVDescriptor.ErrorMessage = "Select Descriptor."
                iConDocId = iDocTypeID
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Waiting for Approval")
            ddlStatus.Items.Insert(3, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDescriptor()
        Try
            ddlDescriptor.DataSource = objclsDocumentType.LoadAllDescriptor(sSession.AccessCode)
            ddlDescriptor.DataTextField = "DESC_NAME"
            ddlDescriptor.DataValueField = "DES_ID"
            ddlDescriptor.DataBind()
            ddlDescriptor.Items.Insert(0, "Select Descriptor")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadDocTypeDashboard()
        Try
            imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
            End If
            dtDocType = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            dgDocTypeDashBoard.DataSource = dtDocType
            dgDocTypeDashBoard.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadDescriptorDetails()
        Dim sIsRequired As String = "", sValidate As String = ""
        Dim chkSelectMandatory As New CheckBox, chkSelectValidator As New CheckBox
        Try
            dtDescriptor = objclsDocumentType.LoadDescDetails(sSession.AccessCode, iDocTypeID)
            dgDisplay.DataSource = dtDescriptor
            dgDisplay.DataBind()
            If dtDescriptor.Rows.Count > 0 Then
                For j = 0 To dgDisplay.Rows.Count - 1
                    sIsRequired = dtDescriptor.Rows(j).Item("Mandatory")
                    sValidate = dtDescriptor.Rows(j).Item("Validator")
                    chkSelectMandatory = dgDisplay.Rows(j).FindControl("chkSelectMandatory")
                    chkSelectValidator = dgDisplay.Rows(j).FindControl("chkSelectValidator")
                    If sIsRequired = "Y" Then
                        chkSelectMandatory.Checked = True
                    Else
                        chkSelectMandatory.Checked = False
                    End If
                    If sValidate = "Y" Then
                        chkSelectValidator.Checked = True
                    Else
                        chkSelectValidator.Checked = False
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadDocTypeDetails(ByVal iID As Integer)
        Dim dtDesc As New DataTable
        Dim sStatus As String = ""
        Try
            ClearAll()
            dtDesc = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, iID, ddlStatus.SelectedIndex)
            iDocTypeID = dtDesc.Rows(0)("DocTypeID")
            If IsDBNull(dtDesc.Rows(0)("Name")) = False Then
                txtDocType.Text = objclsFASGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Name"))
            Else
                txtDocType.Text = ""
            End If
            If IsDBNull(dtDesc.Rows(0)("Note")) = False Then
                txtNote.Text = objclsFASGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Note"))
            Else
                txtNote.Text = ""
            End If
            If IsDBNull(dtDesc.Rows(0)("Status")) = False Then
                sStatus = objclsFASGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Status"))
                If sStatus = "Activated" Then
                    btnDocTypeSave.Visible = False : btnDocTypeUpdate.Visible = True
                ElseIf sStatus = "De-Activated" Then
                    btnDocTypeSave.Visible = False : btnDocTypeUpdate.Visible = False
                ElseIf sStatus = "Waiting for Approval" Then
                    btnDocTypeSave.Visible = True : btnDocTypeUpdate.Visible = False
                End If
            End If
            LoadDescriptorDetails()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            LoadDocTypeDashboard()
            If dtDocType.Rows.Count = 0 Then
                imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnReport.Visible = False
                lblError.Text = "No data to display."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub dgDocTypeDashBoard_PreRender(sender As Object, e As EventArgs) Handles dgDocTypeDashBoard.PreRender
        Try
            If dgDocTypeDashBoard.Rows.Count > 0 Then
                dgDocTypeDashBoard.UseAccessibleHeader = True
                dgDocTypeDashBoard.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDocTypeDashBoard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDocTypeDashBoard_PreRender")
        End Try
    End Sub
    Private Sub dgDisplay_PreRender(sender As Object, e As EventArgs) Handles dgDisplay.PreRender
        Try
            If dgDisplay.Rows.Count > 0 Then
                dgDisplay.UseAccessibleHeader = True
                dgDisplay.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDisplay.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDisplay_PreRender")
        End Try
    End Sub
    Private Sub dgDocTypeDashBoard_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgDocTypeDashBoard.RowCommand
        Dim lblDocTypeID As New Label, lblStatus As New Label
        Dim oDescID As Object
        Try
            lblError.Text = "" : lblModelError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDocTypeID = DirectCast(clickedRow.FindControl("lblDocTypeID"), Label)
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.UserID, lblDocTypeID.Text, "De-Activated")
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.UserID, lblDocTypeID.Text, "Activated")
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then 'Waiting for Approval
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.UserID, lblDocTypeID.Text, "Created")
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                End If
            End If
            If e.CommandName.Equals("EditRow") Then
                oDescID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblDocTypeID.Text)))
                iConDocId = Val(lblDocTypeID.Text)
                LoadDocTypeDetails(Val(lblDocTypeID.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
            LoadDocTypeDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDocTypeDashBoard_RowCommand")
        End Try
    End Sub
    Private Sub dgDocTypeDashBoard_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDocTypeDashBoard.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgDocTypeDashBoard.Columns(0).Visible = False
                dgDocTypeDashBoard.Columns(6).Visible = False
                dgDocTypeDashBoard.Columns(7).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgDocTypeDashBoard.Columns(0).Visible = True
                    dgDocTypeDashBoard.Columns(6).Visible = True
                    dgDocTypeDashBoard.Columns(7).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgDocTypeDashBoard.Columns(0).Visible = True
                    dgDocTypeDashBoard.Columns(6).Visible = True
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgDocTypeDashBoard.Columns(0).Visible = True
                    dgDocTypeDashBoard.Columns(6).Visible = True
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgDocTypeDashBoard.Columns(6).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDocTypeDashBoard_RowDataBound")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgDocTypeDashBoard.Rows.Count - 1
                    chkField = dgDocTypeDashBoard.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgDocTypeDashBoard.Rows.Count - 1
                    chkField = dgDocTypeDashBoard.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDocTypeID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDocType)
        Try
            lblError.Text = ""
            If dgDocTypeDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Activate','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to Activate','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                lblDocTypeID = dgDocTypeDashBoard.Rows(i).FindControl("lblDocTypeID")
                If chkSelect.Checked = True Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.UserID, lblDocTypeID.Text, "Activated")
                    DVZRBADetails.Sort = "DocTypeID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDocTypeID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtDocType = DVZRBADetails.ToTable
                End If
            Next
            lblError.Text = "Successfully Activated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
            LoadDocTypeDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDocTypeID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDocType)
        Try
            lblError.Text = ""
            If dgDocTypeDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activated','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Name to De-Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to De-Activated','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                lblDocTypeID = dgDocTypeDashBoard.Rows(i).FindControl("lblDocTypeID")
                If chkSelect.Checked = True Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.UserID, lblDocTypeID.Text, "De-Activated")
                    DVZRBADetails.Sort = "DocTypeID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDocTypeID.Text)
                    DVZRBADetails(iIndex)("Status") = "De-Activated"
                    dtDocType = DVZRBADetails.ToTable
                End If
            Next
            lblError.Text = "Successfully De-Activated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
            LoadDocTypeDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDocTypeID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDocType)
        Try
            lblError.Text = ""
            If dgDocTypeDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Name to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to Approve','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                lblDocTypeID = dgDocTypeDashBoard.Rows(i).FindControl("lblDocTypeID")
                If chkSelect.Checked = True Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.UserID, lblDocTypeID.Text, "Created")
                    DVZRBADetails.Sort = "DocTypeID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDocTypeID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtDocType = DVZRBADetails.ToTable
                End If
            Next
            lblError.Text = "Successfully Approved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
            LoadDocTypeDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub ClearAll()
        Try
            lblError.Text = "" : lblModelError.Text = "" : btnDocTypeSave.Visible = True : btnDocTypeUpdate.Visible = False
            txtDocType.Text = "" : txtNote.Text = "" : iDocTypeID = 0
            ddlDescriptor.SelectedIndex = 0
            LoadDescriptorDetails()
            dgDisplay.DataSource = Nothing
            dgDisplay.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            ClearAll()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub btnDocTypeNew_Click(sender As Object, e As EventArgs) Handles btnDocTypeNew.Click
        Try
            ClearAll()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDocTypeNew_Click")
        End Try
    End Sub
    Private Sub btnDocTypeSave_Click(sender As Object, e As EventArgs) Handles btnDocTypeSave.Click
        Dim iGlobal As Integer, iDocID As Integer
        Dim ddlDescriptor As New DropDownList
        Dim ObjStr As New StrDocType
        Dim objstrDocTypeDetails As New strDocType_Details
        Dim ObjstrEDT_DOCUMENT_TYPE As New strEDT_DOCUMENT_TYPE
        Dim ObjstrEDT_DOCTYPE_LINK As New strEDT_DOCTYPE_LINK
        Dim Arr() As String
        Dim chkSelectMandatory As New CheckBox, chkSelectValidator As New CheckBox
        Dim lblDescId As Label, lblDescriptor As New Label, lblDataType As Label, lblSize As New Label
        Dim txtValues As New TextBox
        Try
            lblError.Text = "" : lblModelError.Text = ""
            iDocID = iDocTypeID
            If objclsDocumentType.CheckAvailability(sSession.AccessCode, objclsFASGeneral.ReplaceSafeSQL(Trim(txtDocType.Text)), 0) = True Then

                ObjstrEDT_DOCUMENT_TYPE.iDOCTYPEID = 0
                ObjstrEDT_DOCUMENT_TYPE.sDOCNAME = objclsFASGeneral.SafeSQL(txtDocType.Text.Trim)
                ObjstrEDT_DOCUMENT_TYPE.sNOTE = objclsFASGeneral.SafeSQL(txtNote.Text.Trim)
                ObjstrEDT_DOCUMENT_TYPE.iPGROUP = 0
                ObjstrEDT_DOCUMENT_TYPE.iCRBY = sSession.UserID
                ObjstrEDT_DOCUMENT_TYPE.iDOTUPDATEDBY = sSession.UserID
                ObjstrEDT_DOCUMENT_TYPE.sOperation = "I"
                ObjstrEDT_DOCUMENT_TYPE.iOperationby = sSession.UserID
                ObjstrEDT_DOCUMENT_TYPE.iIsGlobal = iGlobal
                ObjstrEDT_DOCUMENT_TYPE.iDOTCompId = sSession.AccessCodeID
                ObjstrEDT_DOCUMENT_TYPE.sDOTIPAddress = sSession.IPAddress
                Arr = objclsDocumentType.SaveDocTypeDetails(sSession.AccessCode, ObjstrEDT_DOCUMENT_TYPE)
                iDocTypeID = Arr(1)
                For iRowCount = 0 To dgDisplay.Rows.Count - 1
                    lblDescId = dgDisplay.Rows(iRowCount).FindControl("lblDescId")
                    lblDataType = dgDisplay.Rows(iRowCount).FindControl("lblDataType")
                    lblSize = dgDisplay.Rows(iRowCount).FindControl("lblSize")
                    txtValues = dgDisplay.Rows(iRowCount).FindControl("txtValues")
                    chkSelectMandatory = dgDisplay.Rows(iRowCount).FindControl("chkSelectMandatory")
                    chkSelectValidator = dgDisplay.Rows(iRowCount).FindControl("chkSelectValidator")

                    ObjstrEDT_DOCTYPE_LINK.iPkID = 0
                    ObjstrEDT_DOCTYPE_LINK.iDOCTYPEID = iDocTypeID
                    ObjstrEDT_DOCTYPE_LINK.iDPTRID = Val(lblDescId.Text)
                    If chkSelectMandatory.Checked = True Then
                        ObjstrEDT_DOCTYPE_LINK.sISREQUIRED = "Y"
                    Else
                        ObjstrEDT_DOCTYPE_LINK.sISREQUIRED = "N"
                    End If
                    ObjstrEDT_DOCTYPE_LINK.iSize = lblSize.Text
                    If txtValues.Text <> "" Then
                        ObjstrEDT_DOCTYPE_LINK.sVALUES = objclsFASGeneral.SafeSQL(txtValues.Text)
                    Else
                        ObjstrEDT_DOCTYPE_LINK.sVALUES = ""
                    End If
                    If chkSelectValidator.Checked = True Then
                        ObjstrEDT_DOCTYPE_LINK.sVALIDATE = "Y"
                    Else
                        ObjstrEDT_DOCTYPE_LINK.sVALIDATE = "N"
                    End If
                    ObjstrEDT_DOCTYPE_LINK.iEDDCRBY = sSession.UserID
                    ObjstrEDT_DOCTYPE_LINK.iEDDUPDATEDBY = sSession.UserID
                    ObjstrEDT_DOCTYPE_LINK.iEDDCompId = sSession.AccessCodeID
                    ObjstrEDT_DOCTYPE_LINK.sEDDIPAddress = sSession.IPAddress
                    Arr = objclsDocumentType.SavePermissionDetails(sSession.AccessCode, ObjstrEDT_DOCTYPE_LINK)

                    If ddlDescriptor.SelectedIndex > 0 Then
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Document Type", "Saved", ddlDescriptor.SelectedValue, ddlDescriptor.SelectedItem.Text, 0, "", sSession.IPAddress)
                    Else
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Document Type", "Saved", 0, "", 0, "", sSession.IPAddress)
                    End If

                    ddlStatus.SelectedIndex = 2
                    ddlStatus_SelectedIndexChanged(sender, e)
                    lblError.Text = "Successfully Saved and Waiting for Approval."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved And Waiting For Approval.','', 'success');", True)
                Next
            Else
                lblModelError.Text = "Document Type already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDocTypeSave_Click")
        End Try
    End Sub
    Private Sub btnDocTypeUpdate_Click(sender As Object, e As EventArgs) Handles btnDocTypeUpdate.Click
        Dim iGlobal As Integer, iDocID As Integer
        ' Dim ddlDescriptor As New DropDownList
        Dim objstrDocTypeDetails As New strDocType_Details
        Dim ObjstrEDT_DOCUMENT_TYPE As New strEDT_DOCUMENT_TYPE
        Dim ObjstrEDT_DOCTYPE_LINK As New strEDT_DOCTYPE_LINK
        Dim Arr() As String
        Dim chkSelectMandatory As New CheckBox, chkSelectValidator As New CheckBox
        Dim lblDescId As Label, lblDescriptor As New Label, lblDataType As Label, lblSize As New Label
        Dim txtValues As New TextBox
        Try
            lblError.Text = "" : lblModelError.Text = ""
            iDocID = iDocTypeID
            If objclsDocumentType.CheckAvailability(sSession.AccessCode, objclsFASGeneral.ReplaceSafeSQL(Trim(txtDocType.Text)), iDocID) = True Then
                If iDocTypeID > 0 Then
                    objclsDocumentType.DeletePermission(sSession.AccessCode, iDocID)

                    ObjstrEDT_DOCUMENT_TYPE.iDOCTYPEID = iDocID
                    ObjstrEDT_DOCUMENT_TYPE.sDOCNAME = objclsFASGeneral.SafeSQL(txtDocType.Text.Trim)
                    ObjstrEDT_DOCUMENT_TYPE.sNOTE = objclsFASGeneral.SafeSQL(txtNote.Text.Trim)
                    ObjstrEDT_DOCUMENT_TYPE.iPGROUP = 0
                    ObjstrEDT_DOCUMENT_TYPE.iCRBY = sSession.UserID
                    ObjstrEDT_DOCUMENT_TYPE.iDOTUPDATEDBY = sSession.UserID
                    ObjstrEDT_DOCUMENT_TYPE.sOperation = "I"
                    ObjstrEDT_DOCUMENT_TYPE.iOperationby = sSession.UserID
                    ObjstrEDT_DOCUMENT_TYPE.iIsGlobal = iGlobal
                    ObjstrEDT_DOCUMENT_TYPE.iDOTCompId = sSession.AccessCodeID
                    ObjstrEDT_DOCUMENT_TYPE.sDOTIPAddress = sSession.IPAddress
                    Arr = objclsDocumentType.SaveDocTypeDetails(sSession.AccessCode, ObjstrEDT_DOCUMENT_TYPE)
                    iDocTypeID = Arr(1)

                    For iRowCount = 0 To dgDisplay.Rows.Count - 1
                        lblDescId = dgDisplay.Rows(iRowCount).FindControl("lblDescId")
                        lblDataType = dgDisplay.Rows(iRowCount).FindControl("lblDataType")
                        lblSize = dgDisplay.Rows(iRowCount).FindControl("lblSize")
                        txtValues = dgDisplay.Rows(iRowCount).FindControl("txtValues")
                        chkSelectMandatory = dgDisplay.Rows(iRowCount).FindControl("chkSelectMandatory")
                        chkSelectValidator = dgDisplay.Rows(iRowCount).FindControl("chkSelectValidator")

                        iDescID = Val(lblDescId.Text)
                        ObjstrEDT_DOCTYPE_LINK.iPkID = 0
                        ObjstrEDT_DOCTYPE_LINK.iDOCTYPEID = iDocTypeID
                        ObjstrEDT_DOCTYPE_LINK.iDPTRID = Val(lblDescId.Text)
                        If chkSelectMandatory.Checked = True Then
                            ObjstrEDT_DOCTYPE_LINK.sISREQUIRED = "Y"
                        Else
                            ObjstrEDT_DOCTYPE_LINK.sISREQUIRED = "Q"
                        End If
                        ObjstrEDT_DOCTYPE_LINK.iSize = lblSize.Text
                        If txtValues.Text <> "" Then
                            ObjstrEDT_DOCTYPE_LINK.sVALUES = objclsFASGeneral.SafeSQL(txtValues.Text)
                        Else
                            ObjstrEDT_DOCTYPE_LINK.sVALUES = ""
                        End If
                        If chkSelectValidator.Checked = True Then
                            ObjstrEDT_DOCTYPE_LINK.sVALIDATE = "Y"
                        Else
                            ObjstrEDT_DOCTYPE_LINK.sVALIDATE = "N"
                        End If
                        ObjstrEDT_DOCTYPE_LINK.iEDDCRBY = sSession.UserID
                        ObjstrEDT_DOCTYPE_LINK.iEDDUPDATEDBY = sSession.UserID
                        ObjstrEDT_DOCTYPE_LINK.iEDDCompId = sSession.AccessCodeID
                        ObjstrEDT_DOCTYPE_LINK.sEDDIPAddress = sSession.IPAddress
                        Arr = objclsDocumentType.SavePermissionDetails(sSession.AccessCode, ObjstrEDT_DOCTYPE_LINK)
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Document Type", "Updated", ddlDescriptor.SelectedValue, ddlDescriptor.SelectedItem.Text, 0, "", sSession.IPAddress)
                        lblError.Text = "Successfully Updated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
                    Next
                End If
            Else
                lblModelError.Text = "Document Type already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                Exit Sub
            End If
            LoadDocTypeDashboard() : LoadDescriptorDetails()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDocTypeUpdate_Click")
        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dt As New DataTable
        Dim dRow, drDesc As DataRow
        Dim chkSelectMandatory As New CheckBox, chkSelectValidator As New CheckBox
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlDescriptor.SelectedIndex > 0 Then
                Dim DVDescriptorDetails As New DataView(dtDescriptor)
                DVDescriptorDetails.RowFilter = "DescId=" & ddlDescriptor.SelectedValue & ""
                dt = DVDescriptorDetails.ToTable
                If dt.Rows.Count > 0 Then
                    lblModelError.Text = "Descriptor Name already exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    Exit Sub
                Else
                    dt = objclsDocumentType.LoadDescriptorGrid(sSession.AccessCode, ddlDescriptor.SelectedValue)
                    For Each dRow In dt.Rows
                        drDesc = dtDescriptor.NewRow
                        drDesc("DescId") = ddlDescriptor.SelectedValue
                        drDesc("Descriptor") = dRow("DESC_NAME")
                        drDesc("DataType") = dRow("Dt_Name")
                        drDesc("Size") = dRow("Desc_Size")
                        drDesc("Mandatory") = "Q"
                        drDesc("Values") = ""
                        drDesc("Validator") = "Q"
                        dtDescriptor.Rows.Add(drDesc)
                    Next
                End If
            End If
            dgDisplay.DataSource = dtDescriptor
            dgDisplay.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAdd_Click")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            ReportViewer1.Reset()
            dt = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFiling/DocumentType.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            If ddlDescriptor.SelectedIndex > 0 Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Document Type", "Excel", ddlDescriptor.SelectedValue, ddlDescriptor.SelectedItem.Text, 0, "", sSession.IPAddress)
            Else
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Document Type", "Excel", 0, "", 0, "", sSession.IPAddress)
            End If
            Response.AddHeader("content-disposition", "attachment; filename=DocumentType" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            ReportViewer1.Reset()
            dt = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Report/Masters/DocumentType.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            If ddlDescriptor.SelectedIndex > 0 Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Document Type", "Excel", ddlDescriptor.SelectedValue, ddlDescriptor.SelectedItem.Text, 0, "", sSession.IPAddress)
            Else
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Document Type", "Excel", 0, "", 0, "", sSession.IPAddress)
            End If
            Response.AddHeader("content-disposition", "attachment; filename=DocumentType" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
End Class
