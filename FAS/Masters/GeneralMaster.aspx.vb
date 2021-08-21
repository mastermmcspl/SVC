Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Office.Interop
Imports Microsoft.Reporting.WebForms
Partial Class Masters_GeneralMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_GeneralMaster"
    Dim objclsAdminMaster As New clsAdminMaster
    Private objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Private objclsFASPermission As New clsFASPermission
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsModulePermission As New clsModulePermission
    Private Shared sSession As AllSession
    Private Shared sSGMSave As String
    Private Shared sSGMAD As String
    Private Shared sSGMAP As String
    Private Shared sSGMRpt As String
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
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                lblHeadingSerachby.Visible = True : txtSearch.Visible = True : ddlSearch.Visible = True : imgbtnSearch.Visible = True

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "GENM")
                imgbtnAdd.Visible = True : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                sSGMSave = "NO" : sSGMAD = "NO" : sSGMRpt = "NO" : sSGMAP = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        'imgbtnAdd.Visible = True
                        sSGMSave = "YES"
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        imgbtnDeActivate.Visible = True
                        sSGMAD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sSGMAP = "YES"
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                        sSGMRpt = "YES"
                    End If
                End If

                'lblHeadingSerachby.Visible = False : txtSearch.Visible = False : ddlSearch.Visible = False : imgbtnSearch.Visible = False
                'imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                'imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                'sSGMSave = "NO" : sSGMAD = "NO" : sSGMRpt = "NO"
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasGGM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        sSGMSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Activate/DeActivate,") = True Then
                '        sSGMAD = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        sSGMRpt = "YES"
                '    End If
                'End If

                BindStatus() : BindSearch()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("MasterID") IsNot Nothing Then
                    ddlMainMaster.SelectedIndex = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterID")))
                    ddlMainMaster_SelectedIndexChanged(sender, e)
                End If
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
    Public Sub BindSearch()
        Try
            ddlSearch.Items.Insert(0, "Select")
            ddlSearch.Items.Insert(1, "Description")
            ddlSearch.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindGeneralMasterGridDetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer, ByVal sSearch As String, ByVal sType As Integer)
        Dim dt As New DataTable
        Try
            dgGeneralMaster.CurrentPageIndex = iPageIndex
            dt = objMaster.GetMasterDetails(sSession.AccessCode, sSession.AccessCodeID, iStatus, sSearch, sType)
            If dt.Rows.Count > dgGeneralMaster.PageSize Then
                dgGeneralMaster.AllowPaging = True
            Else
                dgGeneralMaster.AllowPaging = False
            End If
            dgGeneralMaster.DataSource = dt
            dgGeneralMaster.DataBind()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlMainMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMainMaster.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            lblHeadingSerachby.Visible = False : txtSearch.Visible = False : ddlSearch.Visible = False : imgbtnSearch.Visible = False
            imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
            imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
            If ddlMainMaster.SelectedIndex > 0 Then
                If ddlMainMaster.SelectedIndex > 0 Then
                    lblHeadingSerachby.Visible = True : txtSearch.Visible = True : ddlSearch.Visible = True : imgbtnSearch.Visible = True


                    'If sSGMSave = "YES" Then
                    imgbtnAdd.Visible = True
                    'End If
                    If sSGMRpt = "YES" Then
                        imgbtnReport.Visible = True
                    End If
                    If ddlStatus.SelectedIndex = 0 Then
                            If sSGMAD = "YES" Then
                                imgbtnDeActivate.Visible = True
                            End If
                        ElseIf ddlStatus.SelectedIndex = 1 Then
                            If sSGMAD = "YES" Then
                                imgbtnActivate.Visible = True
                            End If
                        ElseIf ddlStatus.SelectedIndex = 2 Then
                            If sSGMAP = "YES" Then
                                imgbtnWaiting.Visible = True
                            End If
                        End If
                    End If
                End If
            dgGeneralMaster.DataSource = Nothing
            dgGeneralMaster.DataBind()
            If ddlMainMaster.SelectedIndex > 0 Then
                BindGeneralMasterGridDetails(0, ddlStatus.SelectedIndex, objGen.SafeSQL(txtSearch.Text), ddlMainMaster.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMainMaster_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As Object, oMasterName As String
        Try
            lblError.Text = ""
            'If ddlMainMaster.SelectedIndex = 0 Then
            '    ddlMainMaster.Focus()
            '    lblGeneralMasterValidationMsg.Text = "Select Master Type." : lblError.Text = "Select Master Type."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
            '    Exit Sub
            'End If
            If ddlMainMaster.SelectedIndex = -1 Then
                ddlMainMaster.Focus()
                lblGeneralMasterValidationMsg.Text = "Select Master Type  and Category." : lblError.Text = "Select Master Type  and Category."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(3))
            End If
            oMasterName = HttpUtility.UrlEncode(objGen.EncryptQueryString(ddlMainMaster.SelectedValue))
            Response.Redirect(String.Format("~/Masters/GeneralMasterDetails.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            If ddlMainMaster.SelectedIndex > 0 Then
                sMainMaster = ddlMainMaster.SelectedValue
            Else
                ddlMainMaster.Focus()
                lblGeneralMasterValidationMsg.Text = "Select Master Type." : lblError.Text = "Select Master Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ddlMainMaster_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub dgGeneralMaster_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgGeneralMaster.PageIndexChanged
        Try
            lblError.Text = ""
            BindGeneralMasterGridDetails(e.NewPageIndex, ddlStatus.SelectedIndex, objGen.SafeSQL(txtSearch.Text), ddlMainMaster.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneralMaster_PageIndexChanged")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgGeneralMaster.Items.Count - 1
                    chkField = dgGeneralMaster.Items(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgGeneralMaster.Items.Count - 1
                    chkField = dgGeneralMaster.Items(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged")
        End Try
    End Sub
    Protected Sub dgGeneralMaster_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgGeneralMaster.ItemDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnStatus = CType(e.Item.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Item.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgGeneralMaster.Columns(0).Visible = False : dgGeneralMaster.Columns(5).Visible = False : dgGeneralMaster.Columns(6).Visible = True
                'dgGeneralMaster.Columns(6).Visible = False
                imgbtnEdit.Visible = True : imgbtnStatus.Visible = True

                If sSGMAD = "YES" Then
                    dgGeneralMaster.Columns(0).Visible = True
                End If

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    If sSGMAD = "YES" Then
                        dgGeneralMaster.Columns(5).Visible = True
                    End If
                    'If sSGMSave = "YES" Then
                    '    dgGeneralMaster.Columns(6).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    If sSGMAD = "YES" Then
                        dgGeneralMaster.Columns(5).Visible = True
                    End If
                    'If sSGMSave = "YES" Then
                    '    dgGeneralMaster.Columns(6).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    If sSGMAP = "YES" Then
                        dgGeneralMaster.Columns(0).Visible = True
                        dgGeneralMaster.Columns(5).Visible = True
                    End If
                    'If sSGMSave = "YES" Then
                    '    dgGeneralMaster.Columns(6).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgGeneralMaster.Columns(0).Visible = False : dgGeneralMaster.Columns(5).Visible = False : dgGeneralMaster.Columns(6).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneralMaster_ItemDataBound")
        End Try
    End Sub
    Protected Sub dgGeneralMaster_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgGeneralMaster.ItemCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            If ddlMainMaster.SelectedIndex > 0 Then
                sMainMaster = ddlMainMaster.SelectedValue
            End If
            lblDescID = e.Item.FindControl("lblDescID")
            If e.CommandName = "Edit" Then
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(lblDescID.Text)))
                oMasterName = HttpUtility.UrlEncode(objGen.EncryptQueryString(ddlMainMaster.SelectedValue))
                Response.Redirect(String.Format("~/Masters/GeneralMasterDetails.aspx?StatusID={0}&MasterID={1}&MasterName={2}", oStatusID, oMasterID, oMasterName), False) 'GeneralMasterDetails
            End If
            If e.CommandName = "Status" Then
                If ddlStatus.SelectedIndex = 0 Then
                    objMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress)
                    lblGeneralMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress)
                    lblGeneralMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress)
                    lblGeneralMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
                BindGeneralMasterGridDetails(0, ddlStatus.SelectedIndex, objGen.SafeSQL(txtSearch.Text), ddlMainMaster.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneralMaster_ItemCommand")
        End Try
    End Sub

    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If dgGeneralMaster.Items.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgGeneralMaster.Items.Count - 1
                chkSelect = dgGeneralMaster.Items(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to Activate." : lblError.Text = "Select Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
            End If
NextSave:   For i = 0 To dgGeneralMaster.Items.Count - 1
                chkSelect = dgGeneralMaster.Items(i).FindControl("chkSelect")
                lblDescID = dgGeneralMaster.Items(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "A", sSession.UserID, sSession.IPAddress)
                    lblGeneralMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            BindGeneralMasterGridDetails(0, ddlStatus.SelectedIndex, objGen.SafeSQL(txtSearch.Text), ddlMainMaster.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer = 0
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If dgGeneralMaster.Items.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgGeneralMaster.Items.Count - 1
                chkSelect = dgGeneralMaster.Items(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to De-Activate." : lblError.Text = "Select Name to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgGeneralMaster.Items.Count - 1
                chkSelect = dgGeneralMaster.Items(i).FindControl("chkSelect")
                lblDescID = dgGeneralMaster.Items(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "D", sSession.UserID, sSession.IPAddress)
                    lblGeneralMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            BindGeneralMasterGridDetails(0, ddlStatus.SelectedIndex, objGen.SafeSQL(txtSearch.Text), ddlMainMaster.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If dgGeneralMaster.Items.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to Approve." : lblError.Text = "No data To Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To dgGeneralMaster.Items.Count - 1
                chkSelect = dgGeneralMaster.Items(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to Approve." : lblError.Text = "Select Name to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgGeneralMaster.Items.Count - 1
                chkSelect = dgGeneralMaster.Items(i).FindControl("chkSelect")
                lblDescID = dgGeneralMaster.Items(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "W", sSession.UserID, sSession.IPAddress)
                    lblGeneralMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            ddlSearch.SelectedIndex = 0 : txtSearch.Text = ""
            BindGeneralMasterGridDetails(0, ddlStatus.SelectedIndex, objGen.SafeSQL(txtSearch.Text), ddlMainMaster.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub
    Protected Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Try
            lblError.Text = ""
            If ddlCategory.SelectedIndex = 0 Then
                ddlCategory.Focus()
                lblGeneralMasterValidationMsg.Text = "Select Category." : lblError.Text = "Select Category."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlMainMaster.SelectedIndex = 0 Then
                ddlMainMaster.Focus()
                lblGeneralMasterValidationMsg.Text = "Select Master Type." : lblError.Text = "Select Master Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            BindGeneralMasterGridDetails(0, ddlStatus.SelectedIndex, objGen.SafeSQL(txtSearch.Text), ddlMainMaster.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            If ddlCategory.SelectedIndex = 0 Then
                lblError.Text = "Select Category"
                Exit Sub
            End If
            If ddlMainMaster.SelectedIndex = 0 Then
                lblError.Text = "Select Master"
                Exit Sub
            End If
            If ddlMainMaster.SelectedValue = "DESG" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, "")
            ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, "")
            Else
                dtdetails = objclsAdminMaster.LoadGeneralMasterOTHERGridDetails(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", ddlStatus.SelectedIndex, "", ddlMainMaster.SelectedValue)
            End If
            If dtdetails.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GeneralMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Masters", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("GM(" + ddlMainMaster.SelectedItem.Text + ")", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            If ddlCategory.SelectedIndex = 0 Then
                lblError.Text = "Select Category"
                Exit Sub
            End If
            If ddlMainMaster.SelectedIndex = 0 Then
                lblError.Text = "Select Master"
                Exit Sub
            End If
            If ddlMainMaster.SelectedValue = "DESG" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, "")
            ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, "")
            Else
                dtdetails = objclsAdminMaster.LoadGeneralMasterOTHERGridDetails(sSession.AccessCode, sSession.AccessCodeID, "Acc_General_Master", ddlStatus.SelectedIndex, "", ddlMainMaster.SelectedValue)
            End If
            If dtdetails.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GeneralMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Masters", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("GM(" + ddlMainMaster.SelectedItem.Text + ")", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
    Private Sub ddlCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCategory.SelectedIndexChanged
        Try
            If ddlCategory.SelectedValue = 1 Then
                ddlMainMaster.DataSource = objMaster.LoadMasterType(sSession.AccessCode, sSession.AccessCodeID, 1)
                ddlMainMaster.DataTextField = "Mas_type"
                ddlMainMaster.DataValueField = "Mas_Id"
                ddlMainMaster.DataBind()
                ddlMainMaster.Items.Insert(0, "Select Types of Master")
            ElseIf ddlCategory.SelectedValue = 2 Then
                ddlMainMaster.DataSource = objMaster.LoadMasterType(sSession.AccessCode, sSession.AccessCodeID, 2)
                ddlMainMaster.DataTextField = "Mas_type"
                ddlMainMaster.DataValueField = "Mas_Id"
                ddlMainMaster.DataBind()
                ddlMainMaster.Items.Insert(0, "Select Types of Master")
            ElseIf ddlCategory.SelectedValue = 3 Then
                ddlMainMaster.DataSource = objMaster.LoadMasterType(sSession.AccessCode, sSession.AccessCodeID, 3)
                ddlMainMaster.DataTextField = "Mas_type"
                ddlMainMaster.DataValueField = "Mas_Id"
                ddlMainMaster.DataBind()
                ddlMainMaster.Items.Insert(0, "Select Types of Master")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCategory_SelectedIndexChanged")
        End Try
    End Sub
End Class
