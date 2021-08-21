Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class Masters_FrmLgstVehicleDashBoard
    Inherits System.Web.UI.Page
    Private sFormName As String = "LgstVehicleDashBoard"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Private Shared sPTAP As String
    Private Shared sPTED As String
    Dim objVehicleMas As New clsVehicleMaster
    Dim objGen As New clsFASGeneral
    Private Shared dtDetails As New DataTable

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSearch.ImageUrl = "~/Images/Search24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                BindStatus()
                ddlStatus_SelectedIndexChanged(sender, e)
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If

                dtDetails = objVehicleMas.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A")
                GvVehicleMaster.DataSource = dtDetails
                GvVehicleMaster.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Waiting for Approval")
            ddlStatus.Items.Insert(1, "Activated")
            ddlStatus.Items.Insert(2, "Deactivated")
            ddlStatus.Items.Insert(3, "All")
            ddlStatus.SelectedIndex = 1
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                imgbtnWaiting.Visible = True
                imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
                dt = objVehicleMas.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W")
                GvVehicleMaster.DataSource = dt
                GvVehicleMaster.DataBind()
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnDeActivate.Visible = True
                imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
                dt = objVehicleMas.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A")
                GvVehicleMaster.DataSource = dt
                GvVehicleMaster.DataBind()
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnActivate.Visible = True
                imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                dt = objVehicleMas.LoadAllDetails1(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D")
                GvVehicleMaster.DataSource = dt
                GvVehicleMaster.DataBind()
            ElseIf ddlStatus.SelectedIndex = 3 Then
                imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
                dt = objVehicleMas.LoadAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                GvVehicleMaster.DataSource = dt
                GvVehicleMaster.DataBind()
            End If
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object
        Dim oMasterName As String = ""
        Try
            lblError.Text = ""
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(3))
            End If
            Session("dtDetails") = Nothing
            Response.Redirect(String.Format("~/Masters/FrmLgstVehicleMaster.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub GvVehicleMaster_PreRender(sender As Object, e As EventArgs) Handles GvVehicleMaster.PreRender
        Try
            If GvVehicleMaster.Rows.Count > 0 Then
                GvVehicleMaster.UseAccessibleHeader = True
                GvVehicleMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                GvVehicleMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvVehicleMaster_PreRender")
        End Try
    End Sub
    'Protected Sub chkSelectAll_CheckedChanged1(sender As Object, e As EventArgs)
    '    Dim chkField As New CheckBox, chkAll As New CheckBox
    '    Dim iIndx As Integer
    '    Try
    '        chkAll = CType(sender, CheckBox)
    '        If chkAll.Checked = True Then
    '            For iIndx = 0 To GvVehicleMaster.Rows.Count - 1
    '                chkField = GvVehicleMaster.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = True
    '            Next
    '        Else
    '            For iIndx = 0 To GvVehicleMaster.Rows.Count - 1
    '                chkField = GvVehicleMaster.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = False
    '            Next
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged1")
    '    End Try
    'End Sub

    'Private Sub GvVehicleMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvVehicleMaster.RowDataBound
    '    Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
    '    Try
    '        If e.Row.RowType = DataControlRowType.DataRow Then
    '            imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
    '            imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
    '            imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
    '            GvVehicleMaster.Columns(0).Visible = True
    '            imgbtnStatus.ImageUrl = "~/Images/Activate16.png"
    '            If ddlStatus.SelectedIndex = 0 Then
    '                GvVehicleMaster.Columns(8).Visible = True
    '            Else
    '                GvVehicleMaster.Columns(8).Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvVehicleMaster_RowDataBound")
    '    End Try
    'End Sub
    Private Sub GvVehicleMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvVehicleMaster.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                GvVehicleMaster.Columns(0).Visible = True : GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = True
                'imgbtnStatus.ImageUrl = "~/Images/Activate16.png"
                'If ddlStatus.SelectedIndex = 0 Then
                '    GvVehicleMaster.Columns(8).Visible = True
                'Else
                '    GvVehicleMaster.Columns(8).Visible = False
                'End If
                If ddlStatus.SelectedIndex = 0 Then

                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    GvVehicleMaster.Columns(8).Visible = True
                    GvVehicleMaster.Columns(7).Visible = True
                    'If sCMAD = "YES" Then
                    '    dgDiesel.Columns(7).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    GvVehicleMaster.Columns(8).Visible = True
                    'If sCMAD = "YES" Then
                    GvVehicleMaster.Columns(7).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    GvVehicleMaster.Columns(8).Visible = True
                    GvVehicleMaster.Columns(7).Visible = True
                    'If sCMAP = "YES" Then
                    '    dgDiesel.Columns(7).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    GvVehicleMaster.Columns(7).Visible = False : GvVehicleMaster.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvVehicleMaster_RowDataBound")
        End Try
    End Sub
    Private Sub GvVehicleMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvVehicleMaster.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)
            ' lblDescName = DirectCast(clickedRow.FindControl("lblDescName"), Label)
            If e.CommandName.Equals("Edit1") Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                'oMasterName = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescName.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                '  oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/Masters/FrmLgstVehicleMaster.aspx?MasterID={0}", oMasterID), False)
            End If
            'If e.CommandName.Equals("Status") Then
            '    If ddlStatus.SelectedIndex = 0 Then
            '        objVehicleMas.UpdateVehicleStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.UserID, sSession.IPAddress, sSession.YearID)
            '        lblError.Text = "Successfully Activated."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
            '    End If
            '    ddlStatus.SelectedIndex = 0
            '    ddlStatus_SelectedIndexChanged(sender, e)
            'End If
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objVehicleMas.UpdateVehicleStatusDashboard(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblDltnValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objVehicleMas.UpdateVehicleStatusDashboard(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblDltnValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objVehicleMas.UpdateVehicleStatusDashboard(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress, sSession.YearID)
                    lblDltnValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
                End If
                ' ddlStatus.SelectedIndex = 0
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvVehicleMaster_RowCommand")
        End Try
    End Sub
    Private Sub GvVehicleMaster_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles GvVehicleMaster.RowDeleting

    End Sub
    'Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
    '    Dim chkField As New CheckBox, chkAll As New CheckBox
    '    Dim iIndx As Integer
    '    Try
    '        lblError.Text = ""
    '        chkAll = CType(sender, CheckBox)
    '        If chkAll.Checked = True Then
    '            For iIndx = 0 To GvVehicleMaster.Rows.Count - 1
    '                chkField = GvVehicleMaster.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = True
    '            Next
    '        Else
    '            For iIndx = 0 To GvVehicleMaster.Rows.Count - 1
    '                chkField = GvVehicleMaster.Rows(iIndx).FindControl("chkSelect")
    '                chkField.Checked = False
    '            Next
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
    '    End Try
    'End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To GvVehicleMaster.Rows.Count - 1
                    chkField = GvVehicleMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To GvVehicleMaster.Rows.Count - 1
                    chkField = GvVehicleMaster.Rows(iIndx).FindControl("chkSelect")
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
        Dim lblDescID As New Label
        'Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If GvVehicleMaster.Rows.Count = 0 Then
                lblError.Text = "No data to Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Activate','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To GvVehicleMaster.Rows.Count - 1
                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To GvVehicleMaster.Rows.Count - 1
                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
                lblDescID = GvVehicleMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objVehicleMas.UpdateVehicleMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, sSession.IPAddress, lblDescID.Text)
                    lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next

            'If ddlStatus.SelectedIndex = 1 Then
            'objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "D", "A", sSession.UserID, sSession.IPAddress)
            'lblPaymentMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            'End If
            ddlStatus_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        'Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If GvVehicleMaster.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To GvVehicleMaster.Rows.Count - 1
                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to De-Activate.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To GvVehicleMaster.Rows.Count - 1
                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
                lblDescID = GvVehicleMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objVehicleMas.UpdateVehicleMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, sSession.IPAddress, lblDescID.Text)
                    lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
            Next

            'If ddlStatus.SelectedIndex = 0 Then
            '    objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "A", "D", sSession.UserID, sSession.IPAddress)
            '    lblPaymentMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            'End If
            ddlStatus_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        'Dim DVdt As New DataView(dt)
        Try
            lblError.Text = ""
            If GvVehicleMaster.Rows.Count = 0 Then
                lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To GvVehicleMaster.Rows.Count - 1
                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To GvVehicleMaster.Rows.Count - 1
                chkSelect = GvVehicleMaster.Rows(i).FindControl("chkSelect")
                lblDescID = GvVehicleMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objVehicleMas.UpdateVehicleMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "A", sSession.UserID, sSession.IPAddress, lblDescID.Text)
                    lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                End If
            Next

            'If ddlStatus.SelectedIndex = 2 Then
            '    objSPD.UpdatePartyMasterStatusWhole(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "W", "W", sSession.UserID, sSession.IPAddress)
            '    lblPaymentMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalPaymentValidation').modal('show');", True)
            'End If
            ddlStatus_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Private Sub GvVehicleMaster_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GvVehicleMaster.RowEditing

    End Sub
End Class
