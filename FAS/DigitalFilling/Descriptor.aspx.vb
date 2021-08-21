Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Data
Imports Microsoft.Reporting.WebForms
Partial Class DigitalFilling_Descriptor
    Inherits System.Web.UI.Page
    Private sFormName As String = "Descriptor"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsFASGeneral As New clsFASGeneral
    Private objclsDescriptor As New clsDescriptor
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared iDescID As Integer = 0
    Private Shared dtDescrip As DataTable
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
                'imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                '//Preeti
                ' imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DCR")
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
                        imgbtnAdd.Visible = True : btnDescSave.Visible = True : btnDescUpdate.Visible = False
                        sPTED = "YES"
                    End If
                End If
                BindDescType() : BindStatus()
                LoadDescDashboard()
                RFVDescName.ControlToValidate = "txtDescName" : RFVDescName.ErrorMessage = "Enter Descriptor Name."
                REVDescName.ErrorMessage = "Descriptor Name exceeded maximum size(max 100 characters)." : REVDescName.ValidationExpression = "^[\s\S]{0,100}$"
                RFVDescNote.ControlToValidate = "txtDescNote" : RFVDescNote.ErrorMessage = "Enter Descriptor Note."
                REVDescNote.ErrorMessage = "Descriptor Note exceeded maximum size(max 200 characters)." : REVDescNote.ValidationExpression = "^[\s\S]{0,200}$"
                RFVDescDataType.InitialValue = "Select Descriptor DataType" : RFVDescDataType.ErrorMessage = "Select Descriptor DataType."
                RFVDescSize.ControlToValidate = "txtDescSize" : RFVDescSize.ErrorMessage = "Enter Descriptor Size."
                REVDescSize.ErrorMessage = "Only Integer." : REVDescSize.ValidationExpression = "^[0-9]{0,3}$"
                REVDescValue.ErrorMessage = "Descriptor Values exceeded maximum size(max 250 characters)." : REVDescValue.ValidationExpression = "^[\s\S]{0,250}$"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindDescType()
        Try
            ddlDescDataType.DataSource = objclsDescriptor.LoadDescDataType(sSession.AccessCode)
            ddlDescDataType.DataTextField = "DT_DataType"
            ddlDescDataType.DataValueField = "DT_ID"
            ddlDescDataType.DataBind()
            ddlDescDataType.Items.Insert(0, "Select Descriptor DataType")
        Catch ex As Exception
            Throw
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
    Public Sub LoadDescDashboard()
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 Then
                sStatus = "De-Activated"
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 Then
                sStatus = "Waiting for Approval"
                imgbtnWaiting.Visible = True 'Waiting for Approval
            End If
            dtDescrip = objclsDescriptor.GetDescriptorsDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            If ddlStatus.SelectedIndex <= 2 Then
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtDescrip)
                DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                DVZRBADetails.Sort = "Name ASC"
                dt = DVZRBADetails.ToTable
            Else
                Dim DVZRBADetails As New DataView(dtDescrip)
                DVZRBADetails.Sort = "Name ASC"
                dt = DVZRBADetails.ToTable
            End If
            dgDescDashBoard.DataSource = dt
            dgDescDashBoard.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub LoadDescDetails(ByVal iID As Integer)
        Dim dtDesc As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dtDesc = objclsDescriptor.GetDescriptorsDetails(sSession.AccessCode, iID, ddlStatus.SelectedIndex)
            iDescID = dtDesc.Rows(0)("DescID")
            If IsDBNull(dtDesc.Rows(0)("Name")) = False Then
                txtDescName.Text = objclsFASGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Name"))
            Else
                txtDescName.Text = ""
            End If
            If IsDBNull(dtDesc.Rows(0)("Note")) = False Then
                txtDescNote.Text = objclsFASGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Note"))
            Else
                txtDescNote.Text = ""
            End If
            If IsDBNull(dtDesc.Rows(0)("DataType")) = False Then
                If dtDesc.Rows(0)("DataType") = "Number" Then
                    ddlDescDataType.SelectedValue = 1
                ElseIf dtDesc.Rows(0)("DataType") = "Varchar" Then
                    ddlDescDataType.SelectedValue = 2
                ElseIf dtDesc.Rows(0)("DataType") = "Date" Then
                    ddlDescDataType.SelectedValue = 3
                ElseIf dtDesc.Rows(0)("DataType") = "All" Then
                    ddlDescDataType.SelectedValue = 4
                End If
            Else
                ddlDescDataType.SelectedIndex = 0
            End If
            If IsDBNull(dtDesc.Rows(0)("Size")) = False Then
                txtDescSize.Text = objclsFASGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Size"))
            Else
                txtDescSize.Text = ""
            End If
            If IsDBNull(dtDesc.Rows(0)("DescValue")) = False Then
                txtDescValue.Text = objclsFASGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("DescValue"))
            Else
                txtDescValue.Text = ""
            End If
            If IsDBNull(dtDesc.Rows(0)("Status")) = False Then
                sStatus = objclsFASGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Status"))
                If sStatus = "Activated" Then
                    btnDescSave.Visible = False : btnDescUpdate.Visible = True
                ElseIf sStatus = "De-Activated" Then
                    btnDescSave.Visible = False : btnDescUpdate.Visible = False
                ElseIf sStatus = "Waiting for Approval" Then
                    btnDescSave.Visible = True : btnDescUpdate.Visible = False
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlDescDataType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDescDataType.SelectedIndexChanged
        Try
            lblError.Text = ""
            txtDescSize.Enabled = True : txtDescSize.Text = ""
            If UCase(ddlDescDataType.SelectedItem.Text) = "NUMBER" Then
                txtDescSize.Text = 100 : txtDescSize.Enabled = False
            ElseIf UCase(ddlDescDataType.SelectedItem.Text) = "VARCHAR" Then
                txtDescSize.Text = 100 : txtDescSize.Enabled = False
            ElseIf UCase(ddlDescDataType.SelectedItem.Text) = "DATE" Then
                txtDescSize.Text = 8 : txtDescSize.Enabled = False
            ElseIf UCase(ddlDescDataType.SelectedItem.Text) = "ALL" Then
                txtDescSize.Text = "" : txtDescSize.Enabled = True
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDescDataType_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDescrip)
        Try
            lblError.Text = ""
            If dgDescDashBoard.Rows.Count = 0 Then
                lblDescDashBoardValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDescDashBoardValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblDescDashBoardValidationMsg.Text = "Select Name to Activate." : lblError.Text = "Select Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDescDashBoardValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                lblDescID = dgDescDashBoard.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "Activated")
                    DVZRBADetails.Sort = "DescID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDescID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtDescrip = DVZRBADetails.ToTable
                End If
            Next
            lblDescDashBoardValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDescDashBoardValidation').modal('show');", True)
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDescrip)
        Try
            lblError.Text = ""
            If dgDescDashBoard.Rows.Count = 0 Then
                lblDescDashBoardValidationMsg.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDescDashBoardValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblDescDashBoardValidationMsg.Text = "Select Name to De-Activate." : lblError.Text = "Select Name to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDescDashBoardValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                lblDescID = dgDescDashBoard.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "De-Activated")
                    DVZRBADetails.Sort = "DescID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDescID.Text)
                    DVZRBADetails(iIndex)("Status") = "De-Activated"
                    dtDescrip = DVZRBADetails.ToTable
                End If
            Next
            lblDescDashBoardValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDescDashBoardValidation').modal('show');", True)
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDescrip)
        Try
            lblError.Text = ""
            If dgDescDashBoard.Rows.Count = 0 Then
                lblDescDashBoardValidationMsg.Text = "No data to Approve." : lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDescDashBoardValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblDescDashBoardValidationMsg.Text = "Select Name to Approve." : lblError.Text = "Select Name to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDescDashBoardValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDescDashBoard.Rows.Count - 1
                chkSelect = dgDescDashBoard.Rows(i).FindControl("chkSelect")
                lblDescID = dgDescDashBoard.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "Created")
                    DVZRBADetails.Sort = "DescID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDescID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtDescrip = DVZRBADetails.ToTable
                End If
            Next
            lblDescDashBoardValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDescDashBoardValidation').modal('show');", True)
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = "" : btnDescSave.Visible = True : btnDescUpdate.Visible = False
            txtDescName.Text = "" : txtDescNote.Text = "" : ddlDescDataType.SelectedIndex = 0 : txtDescSize.Text = "" : txtDescValue.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            lblError.Text = "" : lblModelError.Text = "" : btnDescSave.Visible = True : btnDescUpdate.Visible = False
            txtDescName.Text = "" : txtDescNote.Text = "" : ddlDescDataType.SelectedIndex = 0 : txtDescSize.Text = "" : txtDescValue.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNew_Click")
        End Try
    End Sub
    Protected Sub btnDescCancel_Click(sender As Object, e As EventArgs) Handles btnDescCancel.Click
        Try
            lblError.Text = "" : lblModelError.Text = ""
            txtDescName.Text = "" : txtDescNote.Text = "" : ddlDescDataType.SelectedIndex = 0 : txtDescSize.Text = "" : txtDescValue.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('hide');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescCancel_Click")
        End Try
    End Sub
    Protected Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click
        Dim objDescriptor As New strDesc_Details
        Dim Arr() As String
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If objclsDescriptor.CheckAvailabilityDescName(sSession.AccessCode, objclsFASGeneral.ReplaceSafeSQL(txtDescName.Text), 0) = True Then
                objDescriptor.sDescName = objclsFASGeneral.SafeSQL(txtDescName.Text)
                objDescriptor.sDescNote = objclsFASGeneral.SafeSQL(txtDescNote.Text)
                objDescriptor.sDescSize = objclsFASGeneral.SafeSQL(txtDescSize.Text)
                objDescriptor.iDescDType = ddlDescDataType.SelectedValue
                objDescriptor.sDescStatus = "C"
                objDescriptor.iDescCrBy = sSession.UserID
                objDescriptor.iDescUpdatedBy = sSession.UserID
                objDescriptor.iDescId = 0
                objDescriptor.sDescDefaultValue = txtDescValue.Text
                objDescriptor.iDescCompId = sSession.AccessCodeID
                objDescriptor.sDescIPAddress = sSession.IPAddress
                Arr = objclsDescriptor.SaveDescriptorDetails(sSession.AccessCode, objDescriptor)
                lblModelError.Text = "Successfully Saved and Waiting for Approval."
            Else
                lblModelError.Text = "Descriptor Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtDescName.Focus()
                Exit Sub
            End If
            ddlStatus.SelectedIndex = 2
            LoadDescDashboard()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescSave_Click")
        End Try
    End Sub
    Protected Sub btnDescUpdate_Click(sender As Object, e As EventArgs) Handles btnDescUpdate.Click
        Dim objDescriptor As New strDesc_Details
        Dim Arr() As String
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If objclsDescriptor.CheckAvailabilityDescName(sSession.AccessCode, objclsFASGeneral.ReplaceSafeSQL(txtDescName.Text), iDescID) = True Then
                objDescriptor.sDescName = objclsFASGeneral.SafeSQL(txtDescName.Text)
                objDescriptor.sDescNote = objclsFASGeneral.SafeSQL(txtDescNote.Text)
                objDescriptor.sDescSize = objclsFASGeneral.SafeSQL(txtDescSize.Text)
                objDescriptor.iDescDType = ddlDescDataType.SelectedValue
                objDescriptor.sDescStatus = "U"
                objDescriptor.iDescCrBy = sSession.UserID
                objDescriptor.iDescUpdatedBy = sSession.UserID
                objDescriptor.iDescId = iDescID
                objDescriptor.sDescDefaultValue = txtDescValue.Text
                objDescriptor.iDescCompId = sSession.AccessCodeID
                objDescriptor.sDescIPAddress = sSession.IPAddress
                Arr = objclsDescriptor.SaveDescriptorDetails(sSession.AccessCode, objDescriptor)
                lblModelError.Text = "Successfully Updated."
            Else
                lblModelError.Text = "Descriptor Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtDescName.Focus()
                Exit Sub
            End If
            LoadDescDashboard()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescUpdate_Click")
        End Try
    End Sub
    Private Sub dgDescDashBoard_PreRender(sender As Object, e As EventArgs) Handles dgDescDashBoard.PreRender
        Dim dt As New DataTable
        Try
            If dgDescDashBoard.Rows.Count > 0 Then
                dgDescDashBoard.UseAccessibleHeader = True
                dgDescDashBoard.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDescDashBoard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDescDashBoard_PreRender")
        End Try
    End Sub
    Private Sub dgDescDashBoard_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgDescDashBoard.RowCommand
        Dim lblDescID As New Label
        Dim oDescID As Object
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)
            If e.CommandName.Equals("EditRow") Then
                oDescID = HttpUtility.UrlEncode(objclsFASGeneral.EncryptQueryString(Val(lblDescID.Text)))
                LoadDescDetails(Val(lblDescID.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "De-Activated")
                    lblDescDashBoardValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDescDashBoardValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "Activated")
                    lblDescDashBoardValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDescDashBoardValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then 'Waiting for Approval
                    objclsDescriptor.DescApproveStatus(sSession.AccessCode, sSession.UserID, lblDescID.Text, "Created")
                    lblDescDashBoardValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDescDashBoardValidation').modal('show');", True)
                End If
            End If
            LoadDescDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDescDashBoard_ItemCommand")
        End Try
    End Sub
    Private Sub dgDescDashBoard_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDescDashBoard.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgDescDashBoard.Columns(0).Visible = False
                dgDescDashBoard.Columns(8).Visible = False
                dgDescDashBoard.Columns(9).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgDescDashBoard.Columns(0).Visible = True
                    dgDescDashBoard.Columns(8).Visible = True
                    dgDescDashBoard.Columns(9).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgDescDashBoard.Columns(0).Visible = True
                    dgDescDashBoard.Columns(8).Visible = True
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgDescDashBoard.Columns(0).Visible = True
                    dgDescDashBoard.Columns(8).Visible = True
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgDescDashBoard.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDescDashBoard_RowCreated")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgDescDashBoard.Rows.Count - 1
                    chkField = dgDescDashBoard.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgDescDashBoard.Rows.Count - 1
                    chkField = dgDescDashBoard.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Try
            ReportViewer1.Reset()
            dtDescrip = objclsDescriptor.GetDescriptorsDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            If dtDescrip.Rows.Count = 0 Then
                lblDescDashBoardValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDescDashBoardValidation').modal('show');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dtDescrip)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/Descriptor.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Descriptor", "Excel", ddlDescDataType.SelectedValue, ddlDescDataType.SelectedItem.Text, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=Descriptor" + ".xls")
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
        Try
            ReportViewer1.Reset()
            dtDescrip = objclsDescriptor.GetDescriptorsDetails(sSession.AccessCode, 0, ddlStatus.SelectedIndex)
            If dtDescrip.Rows.Count = 0 Then
                lblDescDashBoardValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDescDashBoardValidation').modal('show');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dtDescrip)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/Descriptor.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DigitalFilling", "Descriptor", "PDF", ddlDescDataType.SelectedValue, ddlDescDataType.SelectedItem.Text, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=Descriptor" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
End Class
