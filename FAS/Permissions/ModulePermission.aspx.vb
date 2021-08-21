Imports System
Imports System.Data
Imports BusinesLayer
Partial Class Masters_ModulePermission
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_ModulePermission"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Private Shared dtAccess As New DataTable
    Private objGen As New clsFASGeneral
    'Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsGRACePermission As New clsFASPermission
    Private Shared sPerSave As String
    Private Shared dtTable As New DataTable
    Private Shared sPerm As String = ""
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                lblPermissionType.Visible = True : ddlPermission.Visible = True  'imgbtnReport.Visible = False
                rboUser.Visible = True : rboUser.Checked = True
                imgbtnUpdate.Visible = False

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PERM")
                imgbtnSave.Visible = False : sPerSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sPerSave = "YES"
                        imgbtnSave.Visible = True
                    End If
                End If

                'sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MARP")
                'If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,," Then
                '    'Response.Redirect("~/Permission/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    'Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then

                '    End If
                '    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                '        sPerSave = "YES"
                '        imgbtnSave.Visible = True
                '    End If

                '    If sFormButtons.Contains(",Report,") = True Then
                '        'imgbtnReport.Visible = True
                '    End If
                '    If sFormButtons = ",View,SaveOrUpdate,Approve,Report," Then
                '        sPerSave = "YES"
                '        imgbtnSave.Visible = True  'imgbtnReport.Visible = True
                '    End If
                'End If
                '
                rboRole.Visible = True : BindModuleDDL() : BindGrporUser()
                '    BindAccessRgt(0) :
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlPermission.SelectedValue = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If

                If rboUser.Checked = True Then
                    lblName.Text = "* Users"
                ElseIf rboRole.Checked = True Then
                    lblName.Text = "* Designation"
                End If


                'imgbtnSave.Visible = False
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasAP", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then
                '    End If
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                'imgbtnSave.Visible = True
                '    End If
                'End If

                If rboRole.Checked = True Then
                    RFVRole.ErrorMessage = "Select Role. " : RFVRole.InitialValue = "Select Role"
                Else
                    RFVRole.ErrorMessage = "Select User. " : RFVRole.InitialValue = "Select User"
                End If
                'BindModuleDDL() : BindRoleDDL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    Public Sub BindGrporUser()
        Try
            If rboUser.Checked = True Then
                ddlPermission.DataSource = objclsModulePermission.LoadUserDetails(sSession.AccessCode, sSession.AccessCodeID)
                ddlPermission.DataTextField = "FullName"
                ddlPermission.DataValueField = "usr_Id"
                ddlPermission.DataBind()
                ddlPermission.Items.Insert(0, "Select User")
            ElseIf rboRole.Checked = True Then
                ddlPermission.DataSource = objclsModulePermission.LoadActiveRole(sSession.AccessCode, sSession.AccessCodeID)
                ddlPermission.DataTextField = "mas_desc"
                ddlPermission.DataValueField = "Mas_ID"
                ddlPermission.DataBind()
                ddlPermission.Items.Insert(0, "Select Role")
            Else ddlPermission.SelectedValue = 0
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Public Sub BindModuleDDL()
        Try
            ddlModules.DataSource = objclsModulePermission.LoadModules(sSession.AccessCode, sSession.AccessCodeID)
            ddlModules.DataTextField = "Mod_Description"
            ddlModules.DataValueField = "Mod_id"
            ddlModules.DataBind()
            ddlModules.Items.Insert(0, "All Modules")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub BindRoleDDL()
    '    Try
    '        ddlPermission.DataSource = objclsModulePermission.LoadActiveRole(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlPermission.DataTextField = "mas_desc"
    '        ddlPermission.DataValueField = "Mas_ID"
    '        ddlPermission.DataBind()
    '        ddlPermission.Items.Insert(0, "Select Role")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    'Public Sub BindUserDDL()
    '    Try
    '        ddlPermission.DataSource = objclsModulePermission.LoadUserDetails(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlPermission.DataTextField = "FullName"
    '        ddlPermission.DataValueField = "usr_Id"
    '        ddlPermission.DataBind()
    '        ddlPermission.Items.Insert(0, "Select User")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Protected Sub ddlModules_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModules.SelectedIndexChanged
        Try
            lblError.Text = ""
            dgPermission.DataSource = Nothing
            dgPermission.DataBind()
            Session("Count") = 0
            If ddlPermission.SelectedIndex > 0 Then
                If ddlModules.SelectedIndex > 0 Then
                    BindAllSubModules(sSession.AccessCode, ddlModules.SelectedValue)
                Else
                    BindAllSubModules(sSession.AccessCode, 0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlModules_SelectedIndexChanged")
        End Try
    End Sub
    Public Sub BindAllSubModules(ByVal sAC As String, ByVal iMoudle As Integer)
        Dim dt As New DataTable, dtAR As New DataTable, dtAccessRights As New DataTable
        Try
            dt.Columns.Add("Mod_Id")
            dt.Columns.Add("Mod_Description")
            dt.Columns.Add("mod_Function")
            dt.Columns.Add("Mod_Buttons")
            objclsModulePermission.GetAllModule(sSession.AccessCode, sSession.AccessCodeID, iMoudle, dt)
            dgPermission.DataSource = dt
            dgPermission.DataBind()
            dtAR = dt.Copy()
            dtAccess = objclsModulePermission.CopyDataNewCol(dtAR)
            dtAccessRights = GetReportDetails(dtAccess)
            If dtAccessRights.Rows.Count > 0 Then
                dtTable = objclsModulePermission.GetAccessRightsDetails(dtAccess, dtAccessRights)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetReportDetails(ByVal RefDt As DataTable) As DataTable
        Dim dt As New DataTable
        Dim sChk As String, sPermission As String = ""
        Try
            If rboRole.Checked = True Then
                sChk = "R"
            Else
                sChk = "U"
            End If
            If dtAccess.Rows.Count > 0 Then
                sPermission = sPerm.Remove(0, 1)
                dt = objclsModulePermission.GetPermission(sSession.AccessCode, sSession.AccessCodeID, sPermission, ddlPermission.SelectedValue, sChk)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub rboRole_CheckedChanged(sender As Object, e As EventArgs) Handles rboRole.CheckedChanged
        Try
            lblError.Text = ""
            lblName.Text = "Role"
            BindGrporUser()
            dgPermission.DataSource = Nothing
            dgPermission.DataBind()
            If rboRole.Checked = True Then
                RFVRole.ErrorMessage = "Select Role. " : RFVRole.InitialValue = "Select Role"
            Else
                RFVRole.ErrorMessage = "Select User. " : RFVRole.InitialValue = "Select User"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboRole_CheckedChanged")
        End Try
    End Sub
    Private Sub rboUser_CheckedChanged(sender As Object, e As EventArgs) Handles rboUser.CheckedChanged
        Try
            lblError.Text = ""
            lblName.Text = "User list"
            BindGrporUser()
            dgPermission.DataSource = Nothing
            dgPermission.DataBind()
            If rboRole.Checked = True Then
                RFVRole.ErrorMessage = "Select Role. " : RFVRole.InitialValue = "Select Role"
            Else
                RFVRole.ErrorMessage = "Select User. " : RFVRole.InitialValue = "Select User"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboUser_CheckedChanged")
        End Try
    End Sub
    Private Sub ddlPermission_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermission.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnUpdate.Visible = False
            If sPerSave = "YES" Then
                imgbtnSave.Visible = True
            End If

            dgPermission.DataSource = Nothing
            dgPermission.DataBind()
            Session("Count") = 0
            If ddlPermission.SelectedIndex > 0 Then
                If ddlModules.SelectedIndex > 0 Then
                    BindAllSubModules(sSession.AccessCode, ddlModules.SelectedValue)
                Else
                    BindAllSubModules(sSession.AccessCode, 0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermission_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox
        Dim chkField As New CheckBoxList
        Dim IbChk As New ImageButton
        Dim i As Integer, j As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For i = 0 To dgPermission.Items.Count - 1
                    chkField = dgPermission.Items(i).FindControl("chkOperation")
                    For j = 0 To chkField.Items.Count - 1
                        chkField.Items(j).Selected = True
                    Next
                    IbChk = CType(dgPermission.Items.Item(i).FindControl("IbChk"), ImageButton)
                    IbChk.ImageUrl = "../Images/chkSelect.jpg"
                Next
            Else
                For i = 0 To dgPermission.Items.Count - 1
                    chkField = dgPermission.Items(i).FindControl("chkOperation")
                    For j = 0 To chkField.Items.Count - 1
                        chkField.Items(j).Selected = False
                    Next
                    IbChk = CType(dgPermission.Items.Item(i).FindControl("IbChk"), ImageButton)
                    IbChk.ImageUrl = "../Images/chk.jpg"
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkAll_CheckedChanged")
        End Try
    End Sub
    Private Sub dgPermission_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPermission.ItemCommand

    End Sub
    Private Sub dgPermission_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPermission.ItemDataBound
        Dim chkView As New CheckBox, chkNew As New CheckBox, chkSaveOrUpdate As New CheckBox, chkApprove As New CheckBox, chkActivateOrDeactivate As New CheckBox, chkReport As New CheckBox, chkDownload As New CheckBox, chkAnnotation As New CheckBox, chkException As New CheckBox
        Dim sChk As String
        Dim dt As New DataTable
        Dim ichkRole As String
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                If rboRole.Checked = True Then
                    sChk = "R"
                Else
                    sChk = "U"
                End If
                chkView = e.Item.FindControl("chkView")
                chkNew = e.Item.FindControl("chkNew")
                chkSaveOrUpdate = e.Item.FindControl("chkSaveOrUpdate")
                chkApprove = e.Item.FindControl("chkApprove")
                chkActivateOrDeactivate = e.Item.FindControl("chkActivateOrDeactivate")
                chkReport = e.Item.FindControl("chkReport")
                chkDownload = e.Item.FindControl("chkDownload")
                chkAnnotation = e.Item.FindControl("chkAnnotation")
                chkException = e.Item.FindControl("chkException")
                If e.Item.Cells(3).Text = "H" Then
                    e.Item.Cells(1).Font.Bold = True
                    e.Item.Cells(1).ForeColor = Drawing.Color.OrangeRed
                    e.Item.Cells(1).Font.Underline = True
                    chkView.Visible = False
                    chkNew.Visible = False
                    chkSaveOrUpdate.Visible = False
                    chkApprove.Visible = False
                    chkActivateOrDeactivate.Visible = False
                    chkReport.Visible = False
                    chkDownload.Visible = False
                    chkAnnotation.Visible = False
                    chkException.Visible = False
                End If
                If e.Item.Cells(3).Text = "N" Then
                    e.Item.Cells(1).Font.Bold = True
                    e.Item.Cells(1).ForeColor = Drawing.Color.Black
                    e.Item.Cells(1).Font.Underline = True

                    chkView.Visible = False
                    chkNew.Visible = False
                    chkSaveOrUpdate.Visible = False
                    chkApprove.Visible = False
                    chkActivateOrDeactivate.Visible = False
                    chkReport.Visible = False
                    chkDownload.Visible = False
                    chkAnnotation.Visible = False
                    chkException.Visible = False
                End If

                If e.Item.Cells(3).Text = "FN" Then
                    e.Item.Cells(1).Font.Bold = False
                    e.Item.Cells(1).ForeColor = Drawing.Color.Black
                    e.Item.Cells(1).Font.Underline = False

                    If e.Item.Cells(4).Text = "View" Then
                        chkView.Visible = True
                        chkNew.visible = False
                        chkSaveOrUpdate.Visible = False
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,SaveOrUpdate" Then
                        chkView.Visible = True
                        chkNew.visible = False
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,SaveOrUpdate,Approve,Report" Then
                        chkView.Visible = True
                        chkNew.visible = True
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,SaveOrUpdate,Approve" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,Approve" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = False
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,SaveOrUpdate,Approve" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,SaveOrUpdate,Report" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,SaveOrUpdate,Report" Then
                        chkView.Visible = True
                        chkNew.Visible = False
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,SaveOrUpdate,Approve,ActivateOrDeactivate,Report" Then
                        chkView.Visible = True
                        chkNew.Visible = False
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = True
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,SaveOrUpdate,Report" Then
                        chkView.Visible = True
                        chkNew.Visible = False
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,Report" Then
                        chkView.Visible = True
                        chkNew.Visible = False
                        chkSaveOrUpdate.Visible = False
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,SaveOrUpdate" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,SaveOrUpdate,ActivateOrDeactivate,Report" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = True
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,Report" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = False
                        chkApprove.Visible = False
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,SaveOrUpdate,Approve,ActivateOrDeactivate,Report" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = True
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,Approve" Then
                        chkView.Visible = True
                        chkNew.Visible = False
                        chkSaveOrUpdate.Visible = False
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,Approve,ActivateOrDeactivate" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = False
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = True
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                        chkException.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,New,SaveOrUpdate,Approve,Report,Exception" Then
                        chkView.Visible = True
                        chkNew.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkApprove.Visible = True
                        chkActivateOrDeactivate.Visible = False
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False

                        ichkRole = objclsModulePermission.getCurrentUserRole(sSession.UserID, sSession.AccessCode, sSession.AccessCodeID)
                        If UCase(ichkRole) = "OWNER" Then
                            chkException.Visible = True
                        Else
                            chkException.Visible = False
                        End If
                    End If


                        If ddlPermission.SelectedIndex > 0 Then
                        sPerm = sPerm & "," & e.Item.Cells(0).Text
                        dt = objclsModulePermission.GetCheckPermission(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlPermission.SelectedValue, sChk)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0)("SGP_View") = "1" Then
                                chkView.Checked = True
                            End If
                            If dt.Rows(0)("SGP_New") = "1" Then
                                chkNew.Checked = True
                            End If
                            If dt.Rows(0)("SGP_SaveOrUpdate") = "1" Then
                                chkSaveOrUpdate.Checked = True
                            End If
                            If dt.Rows(0)("SGP_Approve") = "1" Then
                                chkApprove.Checked = True
                            End If
                            If dt.Rows(0)("SGP_ActivateOrDeactivate") = "1" Then
                                chkActivateOrDeactivate.Checked = True
                            End If
                            If dt.Rows(0)("SGP_Report") = "1" Then
                                chkReport.Checked = True
                            End If
                            If dt.Rows(0)("SGP_Download") = "1" Then
                                chkDownload.Checked = True
                            End If
                            If dt.Rows(0)("SGP_Annotaion") = "1" Then
                                chkAnnotation.Checked = True
                            End If


                            ichkRole = objclsModulePermission.getCurrentUserRole(sSession.UserID, sSession.AccessCode, sSession.AccessCodeID)
                            If dt.Rows(0)("SGP_Exception") = "1" Then

                                If UCase(ichkRole) = "Owner" Then
                                    chkException.Checked = True
                                    chkException.Visible = True : chkException.Enabled = True
                                Else
                                    chkException.Checked = True
                                    chkException.Visible = True : chkException.Enabled = False
                                End If
                            Else
                                'If ichkRole = "Owner" Then
                                '    chkException.Checked = False
                                '    chkException.Visible = True : chkException.Enabled = True
                                'Else
                                '    chkException.Checked = False
                                '    chkException.Visible = False : chkException.Enabled = False
                                'End If
                            End If
                        End If



                        If dt.Rows.Count > 0 Then
                            If sPerSave = "YES" Then

                                If sPerSave = "YES" Then
                                    imgbtnUpdate.Visible = True
                                End If
                                imgbtnSave.Visible = False
                            End If
                            'Else
                            '    If sPerSave = "YES" Then
                            '        imgbtnUpdate.Visible = False : imgbtnSave.Visible = True
                            '        lblError.Text = "No Permission assigned"
                            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Permission assigned','', 'info');", True)
                            '    End If
                        End If
                    End If
                End If
                'If iExcept = 1 Then
                '    chkException.Visible = True
                'Else
                '    chkException.Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPermission_ItemDataBound")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim iRowCount As Integer, iRet As Integer
        Dim Arr() As String
        Dim dtGen As New DataTable
        Dim objPerm As New clsModulePermission
        Dim lblSgpid As New Label
        Dim chkSelCreate As New CheckBox, chkSelPermit As New CheckBox, chkSelReload As New CheckBox, chkSelDelete As New CheckBox, chkSelModify As New CheckBox, chkSelActivateOrDeactivate As New CheckBox, chkSelView As New CheckBox, chkSelPrint As New CheckBox, chkSelApprove As New CheckBox
        Dim chkAnnotation As New CheckBox, chkDownload As New CheckBox, chkException As New CheckBox
        Try

            If ddlPermission.SelectedIndex = 0 Then
                If rboRole.Checked = True Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Role.','', 'info');", True)
                    Exit Sub
                ElseIf rboUser.Checked = True Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Users.','', 'info');", True)
                    Exit Sub
                End If
            End If

            iRowCount = dgPermission.Items.Count
            If rboUser.Checked = True Then
                objPerm.sSGP_LevelGroup = "U"
                objPerm.iSGP_LevelGroupID = ddlPermission.SelectedValue
            Else
                objPerm.sSGP_LevelGroup = "R"
                objPerm.iSGP_LevelGroupID = ddlPermission.SelectedValue
            End If
            iRet = objclsModulePermission.CheckAvailability(sSession.AccessCode, objPerm.sSGP_LevelGroup, objPerm.iSGP_LevelGroupID)

            For i = 0 To dgPermission.Items.Count - 1
                objPerm.iSGP_ID = 0

                objPerm.iSGP_ModID = dgPermission.Items(i).Cells(0).Text
                If objclsModulePermission.IsPermissionSet(sSession.AccessCode, sSession.AccessCodeID, objPerm.sSGP_LevelGroup, ddlPermission.SelectedValue, objPerm.iSGP_ModID) = True Then
                    objclsModulePermission.DeletePermission(sSession.AccessCode, sSession.AccessCodeID, objPerm.sSGP_LevelGroup, ddlPermission.SelectedValue, objPerm.iSGP_ModID)
                End If

                chkSelPermit = dgPermission.Items(i).FindControl("chkView")
                If chkSelPermit.Checked = True Then
                    objPerm.iSGP_View = 1
                Else
                    objPerm.iSGP_View = 0
                End If

                chkSelReload = dgPermission.Items(i).FindControl("chkNew")
                If chkSelReload.Checked = True Then
                    objPerm.iSGP_New = 1
                Else
                    objPerm.iSGP_New = 0
                End If

                chkSelCreate = dgPermission.Items(i).FindControl("chkSaveOrUpdate")
                If chkSelCreate.Checked = True Then
                    objPerm.iSGP_SaveOrUpdate = 1
                Else
                    objPerm.iSGP_SaveOrUpdate = 0
                End If

                chkSelModify = dgPermission.Items(i).FindControl("chkApprove")
                If chkSelModify.Checked = True Then
                    objPerm.iSGP_Approve = 1
                Else
                    objPerm.iSGP_Approve = 0
                End If

                chkSelActivateOrDeactivate = dgPermission.Items(i).FindControl("chkActivateOrDeactivate")
                If chkSelActivateOrDeactivate.Checked = True Then
                    objPerm.iSGP_ActivateOrDeactivate = 1
                Else
                    objPerm.iSGP_ActivateOrDeactivate = 0
                End If

                chkSelDelete = dgPermission.Items(i).FindControl("chkReport")
                If chkSelDelete.Checked = True Then
                    objPerm.iSGP_Report = 1
                Else
                    objPerm.iSGP_Report = 0
                End If

                chkDownload = dgPermission.Items(i).FindControl("chkDownload")
                If chkDownload.Checked = True Then
                    objPerm.iSGP_Download = 1
                Else
                    objPerm.iSGP_Download = 0
                End If

                chkAnnotation = dgPermission.Items(i).FindControl("chkAnnotation")
                If chkAnnotation.Checked = True Then
                    objPerm.iSGP_Annotation = 1
                Else
                    objPerm.iSGP_Annotation = 0
                End If
                chkException = dgPermission.Items(i).FindControl("chkException")
                If chkException.Checked = True Then
                    objPerm.iSGP_Exception = 1
                Else
                    objPerm.iSGP_Exception = 0
                End If

                objPerm.iSGP_CreatedBy = sSession.UserID
                objPerm.iSGP_ApprovedBy = sSession.UserID
                objPerm.sSGP_Status = "A"
                objPerm.sSGP_DelFlag = "A"
                objPerm.iSGP_CompID = sSession.AccessCodeID
                Arr = objclsModulePermission.SaveOrUpdatePermission(sSession.AccessCode, objPerm)
            Next

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved','', 'success');", True)
            End If
            imgbtnSave.Visible = False
            If sPerSave = "YES" Then
                imgbtnUpdate.Visible = True
            End If
            ddlPermission_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
        'Dim sChk As String
        'Dim chkItem As CheckBoxList
        'Dim i As Integer
        'Dim blnCheck As Boolean
        'Dim sSelIDs As String = String.Empty
        'Try
        '    lblError.Text = ""
        '    If rboRole.Checked = True Then
        '        sChk = "R"
        '    Else
        '        sChk = "U"
        '    End If
        '    For i = 0 To dgPermission.Items.Count - 1
        '        objclsModulePermission.DeletePermission(sSession.AccessCode, sSession.AccessCodeID, sChk, ddlPermission.SelectedValue, dgPermission.Items(i).Cells(0).Text)
        '        chkItem = dgPermission.Items(i).Cells(3).FindControl("chkOperation")
        '        sSelIDs = ""
        '        For Each items As ListItem In chkItem.Items
        '            If items.Selected Then
        '                sSelIDs += items.Value + ";"
        '                blnCheck = True
        '            End If
        '        Next
        '        If blnCheck = True Then
        '            objclsModulePermission.SaveOrUpdatePermission(sSession.AccessCode, sSession.AccessCodeID, sChk, ddlPermission.SelectedValue, dgPermission.Items(i).Cells(0).Text, sSelIDs, sSession.UserID, sSession.IPAddress)
        '            blnCheck = False
        '        End If
        '    Next
        '    If dgPermission.Items.Count > 0 Then
        '        lblModulePermissionValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
        '        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Form Level Permissions", "Saved", 0, ddlModules.SelectedItem.Text, ddlPermission.SelectedValue, ddlPermission.SelectedItem.Text, sSession.IPAddress)
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalModulePermissionValidation').modal('show');", True)
        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        'End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim iRowCount As Integer, iRet As Integer
        Dim Arr() As String
        Dim dtGen As New DataTable
        Dim objPerm As New clsModulePermission
        Dim lblSgpid As New Label
        Dim chkSelCreate As New CheckBox, chkSelPermit As New CheckBox, chkSelReload As New CheckBox, chkSelDelete As New CheckBox, chkSelModify As New CheckBox, chkSelActivateOrDeactivate As New CheckBox, chkSelView As New CheckBox, chkSelPrint As New CheckBox, chkSelApprove As New CheckBox
        Dim chkAnnotation As New CheckBox, chkDownload As New CheckBox, chkException As New CheckBox
        Try

            If ddlPermission.SelectedIndex = 0 Then
                If rboRole.Checked = True Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Role.','', 'info');", True)
                    Exit Sub
                ElseIf rboUser.Checked = True Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Users.','', 'info');", True)
                    Exit Sub
                End If
            End If

            iRowCount = dgPermission.Items.Count
            If rboUser.Checked = True Then
                objPerm.sSGP_LevelGroup = "U"
                objPerm.iSGP_LevelGroupID = ddlPermission.SelectedValue
            Else
                objPerm.sSGP_LevelGroup = "R"
                objPerm.iSGP_LevelGroupID = ddlPermission.SelectedValue
            End If
            iRet = objclsModulePermission.CheckAvailability(sSession.AccessCode, objPerm.sSGP_LevelGroup, objPerm.iSGP_LevelGroupID)

            For i = 0 To dgPermission.Items.Count - 1
                objPerm.iSGP_ID = 0
                'If iRet = 0 Then
                '    objPerm.iSGP_ID = 0
                'Else
                '    objPerm.iSGP_ID = dgAccessRgt.Items(i).Cells(0).Text
                'End If

                objPerm.iSGP_ModID = dgPermission.Items(i).Cells(0).Text
                If objclsModulePermission.IsPermissionSet(sSession.AccessCode, sSession.AccessCodeID, objPerm.sSGP_LevelGroup, ddlPermission.SelectedValue, objPerm.iSGP_ModID) = True Then
                    objclsModulePermission.DeletePermission(sSession.AccessCode, sSession.AccessCodeID, objPerm.sSGP_LevelGroup, ddlPermission.SelectedValue, objPerm.iSGP_ModID)
                End If

                chkSelPermit = dgPermission.Items(i).FindControl("chkView")
                If chkSelPermit.Checked = True Then
                    objPerm.iSGP_View = 1
                Else
                    objPerm.iSGP_View = 0
                End If

                chkSelReload = dgPermission.Items(i).FindControl("chkNew")
                If chkSelReload.Checked = True Then
                    objPerm.iSGP_New = 1
                Else
                    objPerm.iSGP_New = 0
                End If

                chkSelCreate = dgPermission.Items(i).FindControl("chkSaveOrUpdate")
                If chkSelCreate.Checked = True Then
                    objPerm.iSGP_SaveOrUpdate = 1
                Else
                    objPerm.iSGP_SaveOrUpdate = 0
                End If

                chkSelModify = dgPermission.Items(i).FindControl("chkApprove")
                If chkSelModify.Checked = True Then
                    objPerm.iSGP_Approve = 1
                Else
                    objPerm.iSGP_Approve = 0
                End If

                chkSelActivateOrDeactivate = dgPermission.Items(i).FindControl("chkActivateOrDeactivate")
                If chkSelActivateOrDeactivate.Checked = True Then
                    objPerm.iSGP_ActivateOrDeactivate = 1
                Else
                    objPerm.iSGP_ActivateOrDeactivate = 0
                End If

                chkSelDelete = dgPermission.Items(i).FindControl("chkReport")
                If chkSelDelete.Checked = True Then
                    objPerm.iSGP_Report = 1
                Else
                    objPerm.iSGP_Report = 0
                End If


                chkDownload = dgPermission.Items(i).FindControl("chkDownload")
                If chkDownload.Checked = True Then
                    objPerm.iSGP_Download = 1
                Else
                    objPerm.iSGP_Download = 0
                End If

                chkAnnotation = dgPermission.Items(i).FindControl("chkAnnotation")
                If chkAnnotation.Checked = True Then
                    objPerm.iSGP_Annotation = 1
                Else
                    objPerm.iSGP_Annotation = 0
                End If
                chkException = dgPermission.Items(i).FindControl("chkException")
                If chkException.Checked = True Then
                    objPerm.iSGP_Exception = 1
                Else
                    objPerm.iSGP_Exception = 0
                End If
                objPerm.iSGP_CreatedBy = sSession.UserID
                objPerm.iSGP_ApprovedBy = sSession.UserID
                objPerm.sSGP_Status = "U"
                objPerm.sSGP_DelFlag = "A"
                objPerm.iSGP_CompID = sSession.AccessCodeID
                Arr = objclsModulePermission.SaveOrUpdatePermission(sSession.AccessCode, objPerm)
            Next
            ddlPermission_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
End Class
