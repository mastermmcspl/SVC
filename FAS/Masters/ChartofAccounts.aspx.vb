Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports PowerUp.Web.UI.WebTree
Partial Class Masters_ChartofAccounts
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters/ChartofAccounts.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGen As New clsFASGeneral
    Private objclsModulePermission As New clsModulePermission
    Dim objGenFun As New clsGeneralFunctions
    Dim objCOA As New clsChartOfAccounts
    Private Shared sCOASave As String
    Private Shared sCOAAD As String
    Private Shared sCOAAP As String
    Private tnSelectedNode As PowerUp.Web.UI.WebTree.TreeView
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                LoadTree(0)

                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "COA")
                imgbtnReport.Visible = False : sCOASave = "NO" : sCOAAD = "NO" : sCOAAP = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        sCOASave = "YES"
                    End If
                    If sFormButtons.Contains(",ActivateOrDeactivate,") = True Then
                        sCOAAD = "YES"
                    End If
                    If sFormButtons.Contains(",Approve,") = True Then
                        sCOAAP = "YES"
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If

                RFVName.ErrorMessage = "Enter Name." : RFVEName.ValidationExpression = "^(.{0,150})$"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadTree(ByVal iHead As Integer)
        Dim dt As New DataTable
        Try
            tvMCAccount.DataKeyField = "gl_id"
            tvMCAccount.DataParentField = "gl_parent"
            tvMCAccount.DataTextField = "gl_desc"
            dt = objCOA.LoadChartOfAccounts(sSession.AccessCode, sSession.AccessCodeID, iHead).Tables(0)
            tvMCAccount.DataSource = dt
            tvMCAccount.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlHead.SelectedIndex > 0 Then
                If sCOASave = "YES" Then
                    imgbtnAdd.Visible = True
                End If


                ddlGroup.DataSource = dt
                    ddlGroup.DataBind()

                    ddlSubGroup.DataSource = dt
                    ddlSubGroup.DataBind()

                    ddlGL.DataSource = dt
                    ddlGL.DataBind()

                    ddlSubGL.DataSource = dt
                    ddlSubGL.DataBind()

                    txtName.Text = "" : txtDescription.Text = "" : txtCode.Text = ""

                    imgbtnAdd.ToolTip = "Add Group"
                    LoadTree(ddlHead.SelectedValue)
                    ddlGroup.DataSource = objCOA.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue)
                    ddlGroup.DataTextField = "Description"
                    ddlGroup.DataValueField = "gl_id"
                    ddlGroup.DataBind()
                    ddlGroup.Items.Insert(0, "Select Group")
                Else
                    ddlGroup.DataSource = dt
                ddlGroup.DataBind()

                ddlSubGroup.DataSource = dt
                ddlSubGroup.DataBind()

                ddlGL.DataSource = dt
                ddlGL.DataBind()

                ddlSubGL.DataSource = dt
                ddlSubGL.DataBind()
                LoadTree(0)

                txtName.Text = "" : txtDescription.Text = "" : txtCode.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlHead_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlGroup.SelectedIndex > 0 Then
                imgbtnSave.Visible = False
                If sCOASave = "YES" Then
                    imgbtnUpdate.Visible = True : imgbtnAdd.Visible = True
                End If
                imgbtnAdd.ToolTip = "Add Sub Group"

                    LoadTree(ddlHead.SelectedValue)
                    ddlSubGroup.DataSource = objCOA.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)
                    ddlSubGroup.DataTextField = "Description"
                    ddlSubGroup.DataValueField = "gl_id"
                    ddlSubGroup.DataBind()
                    ddlSubGroup.Items.Insert(0, "Select Sub Group")

                    lblPath.Text = objCOA.GetchartofAccountPath(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)

                    dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                            txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                        Else
                            txtCode.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                            txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                        Else
                            txtName.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                            txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                        Else
                            txtDescription.Text = ""
                        End If


                    If dt.Rows(0)("gl_Status").ToString() = "W" Then
                        lblStatus.Text = "Waiting for Approval"
                        imgbtnSave.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False

                        If sCOAAP = "YES" Then
                            imgbtnWaiting.Visible = True
                        End If
                        If sCOASave = "YES" Then
                            imgbtnUpdate.Visible = True
                        End If
                        imgbtnAdd.Visible = True

                    ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                        lblStatus.Text = "De-Activated"
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                        imgbtnAdd.Visible = False
                        If sCOAAD = "YES" Then
                            imgbtnActivate.Visible = True
                        End If
                    ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                        lblStatus.Text = "Activated"
                            imgbtnSave.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False

                        If sCOAAD = "YES" Then
                            imgbtnDeActivate.Visible = True
                        End If
                        If sCOASave = "YES" Then
                            imgbtnUpdate.Visible = True
                            imgbtnAdd.Visible = True
                        End If
                    End If
                    End If
                Else
                    imgbtnAdd.ToolTip = "Add Group"
                txtCode.Text = "" : txtDescription.Text = "" : txtName.Text = ""
                ddlSubGroup.DataSource = dt
                ddlSubGroup.DataBind()

                ddlGL.DataSource = dt
                ddlGL.DataBind()

                ddlSubGL.DataSource = dt
                ddlSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlSubGroup_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubGroup.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSubGroup.SelectedIndex > 0 Then
                imgbtnSave.Visible = False

                If sCOASave = "YES" Then
                    imgbtnUpdate.Visible = True : imgbtnAdd.Visible = True
                End If
                imgbtnAdd.ToolTip = "Add General Ledger"

                    LoadTree(ddlHead.SelectedValue)
                    ddlGL.DataSource = objCOA.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                    ddlGL.DataTextField = "Description"
                    ddlGL.DataValueField = "gl_id"
                    ddlGL.DataBind()
                    ddlGL.Items.Insert(0, "Select General Ledger")

                    lblPath.Text = objCOA.GetchartofAccountPath(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)

                    dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                            txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                        Else
                            txtCode.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                            txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                        Else
                            txtName.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                            txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                        Else
                            txtDescription.Text = ""
                        End If

                        If dt.Rows(0)("gl_Status").ToString() = "W" Then
                            lblStatus.Text = "Waiting for Approval"
                            imgbtnSave.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False

                        If sCOAAP = "YES" Then
                            imgbtnWaiting.Visible = True
                        End If
                        If sCOASave = "YES" Then
                            imgbtnUpdate.Visible = True
                        End If
                        imgbtnAdd.Visible = False

                        ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                            lblStatus.Text = "De-Activated"
                            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                        imgbtnAdd.Visible = False
                        If sCOAAD = "YES" Then
                            imgbtnActivate.Visible = True
                        End If
                    ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                            lblStatus.Text = "Activated"
                            imgbtnSave.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False

                        If sCOAAD = "YES" Then
                            imgbtnDeActivate.Visible = True
                        End If
                        If sCOASave = "YES" Then
                            imgbtnUpdate.Visible = True
                            imgbtnAdd.Visible = True
                        End If
                    End If
                    End If
                Else
                    imgbtnAdd.ToolTip = "Add Sub Group"
                txtName.Text = "" : txtDescription.Text = "" : txtCode.Text = ""
                ddlGL.DataSource = dt
                ddlGL.DataBind()

                ddlSubGL.DataSource = dt
                ddlSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubGroup_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlGL.SelectedIndex > 0 Then
                imgbtnSave.Visible = False
                If sCOASave = "YES" Then
                    imgbtnUpdate.Visible = True : imgbtnAdd.Visible = True
                End If
                imgbtnAdd.ToolTip = "Add Sub General Ledger"
                    LoadTree(ddlHead.SelectedValue)
                    ddlSubGL.DataSource = objCOA.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)
                    ddlSubGL.DataTextField = "Description"
                    ddlSubGL.DataValueField = "gl_id"
                    ddlSubGL.DataBind()
                    ddlSubGL.Items.Insert(0, "Select Sub General Ledger")

                    lblPath.Text = objCOA.GetchartofAccountPath(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)

                    dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                            txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                        Else
                            txtCode.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                            txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                        Else
                            txtName.Text = ""
                        End If

                        If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                            txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                        Else
                            txtDescription.Text = ""
                        End If

                    If dt.Rows(0)("gl_Status").ToString() = "W" Then
                        lblStatus.Text = "Waiting for Approval"
                        imgbtnSave.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
                        imgbtnAdd.Visible = True

                        If sCOAAP = "YES" Then
                            imgbtnWaiting.Visible = True
                        End If
                        If sCOASave = "YES" Then
                            imgbtnUpdate.Visible = True
                        End If
                    ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                        lblStatus.Text = "De-Activated"
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                        imgbtnAdd.Visible = False
                        If sCOAAD = "YES" Then
                            imgbtnActivate.Visible = True
                        End If
                    ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                        lblStatus.Text = "Activated"
                            imgbtnSave.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False

                        If sCOAAD = "YES" Then
                            imgbtnDeActivate.Visible = True
                        End If
                        If sCOASave = "YES" Then
                            imgbtnUpdate.Visible = True
                            imgbtnAdd.Visible = True
                        End If
                    End If
                    End If
                Else
                imgbtnAdd.ToolTip = "Add General Ledger"
                txtName.Text = "" : txtDescription.Text = "" : txtCode.Text = ""

                ddlSubGL.DataSource = dt
                ddlSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            imgbtnUpdate.Visible = False
            If sCOASave = "YES" Then
                imgbtnSave.Visible = True : imgbtnAdd.Visible = True
            End If
            imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
            txtCode.Text = "" : txtName.Text = "" : txtDescription.Text = ""
            lblStatus.Text = "Not Started"

            'To Create New Group
            If (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex = 0) And (ddlSubGroup.SelectedIndex = -1) And (ddlGL.SelectedIndex = -1) And (ddlSubGL.SelectedIndex = -1) Then
                txtCode.Text = objCOA.GenerateGrpCode(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue)

                'To Create Sub Group
            ElseIf (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex > 0) And (ddlSubGroup.SelectedIndex = 0) And (ddlGL.SelectedIndex = -1) And (ddlSubGL.SelectedIndex = -1) Then
                txtCode.Text = objCOA.GenerateSubGrpCode(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue, ddlGroup.SelectedValue)

                'To Create GL
            ElseIf (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex > 0) And (ddlSubGroup.SelectedIndex > 0) And (ddlGL.SelectedIndex = 0) And (ddlSubGL.SelectedIndex = -1) Then
                txtCode.Text = objCOA.GenerateGLCode(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue, ddlSubGroup.SelectedValue)

                'To Create SubGL
            ElseIf (ddlHead.SelectedIndex > 0) And (ddlGroup.SelectedIndex > 0) And (ddlSubGroup.SelectedIndex > 0) And (ddlGL.SelectedIndex > 0) And (ddlSubGL.SelectedIndex = 0) Then
                txtCode.Text = objCOA.GenerateSubGLCode(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue, ddlGL.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub

    Private Sub ddlSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            If ddlSubGL.SelectedIndex > 0 Then
                imgbtnSave.Visible = False : imgbtnAdd.Visible = False
                If sCOASave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
                lblPath.Text = objCOA.GetchartofAccountPath(sSession.AccessCode, sSession.AccessCodeID, ddlSubGL.SelectedValue)

                dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlSubGL.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                        txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                    Else
                        txtCode.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                        txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                    Else
                        txtName.Text = ""
                    End If

                    If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                        txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                    Else
                        txtDescription.Text = ""
                    End If

                    If dt.Rows(0)("gl_Status").ToString() = "W" Then
                        lblStatus.Text = "Waiting for Approval"
                        imgbtnSave.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
                        imgbtnAdd.Visible = False

                        If sCOAAP = "YES" Then
                            imgbtnWaiting.Visible = True
                        End If
                        If sCOASave = "YES" Then
                            imgbtnUpdate.Visible = True
                        End If

                    ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                        lblStatus.Text = "De-Activated"
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                        imgbtnAdd.Visible = False
                        If sCOAAD = "YES" Then
                            imgbtnActivate.Visible = True
                        End If
                    ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                        lblStatus.Text = "Activated"
                        imgbtnSave.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False
                        imgbtnAdd.Visible = False
                        If sCOAAD = "YES" Then
                            imgbtnDeActivate.Visible = True
                        End If
                        If sCOASave = "YES" Then
                            imgbtnUpdate.Visible = True
                        End If
                    End If
                End If
            Else
                imgbtnAdd.Visible = True : imgbtnAdd.ToolTip = "Add Sub General Ledger"
                txtName.Text = "" : txtDescription.Text = "" : txtCode.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim iRet As Integer = 0
        Dim sRet As String
        Dim sArray As Array
        Try

            If ddlHead.SelectedIndex > 0 Then
                objCOA.igl_head = 0
                objCOA.igl_Parent = 0
            End If

            If ddlGroup.SelectedIndex > 0 Then
                objCOA.igl_head = 1
                objCOA.igl_Parent = ddlGroup.SelectedValue
            End If

            If ddlSubGroup.SelectedIndex > 0 Then
                objCOA.igl_head = 2
                objCOA.igl_Parent = ddlSubGroup.SelectedValue
            End If

            If ddlGL.SelectedIndex > 0 Then
                objCOA.igl_head = 3
                objCOA.igl_Parent = ddlGL.SelectedValue
            End If

            If ddlSubGL.SelectedIndex > 0 Then
                objCOA.igl_Parent = ddlSubGL.SelectedValue
            End If

            objCOA.igl_id = 0
            objCOA.sgl_glcode = txtCode.Text
            objCOA.sgl_Desc = objGen.SafeSQL(txtName.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescription.Text)
            objCOA.sgl_Delflag = "C"
            objCOA.igl_AccHead = ddlHead.SelectedValue
            objCOA.igl_Crby = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_Status = "W"
            objCOA.sgl_IPAddress = sSession.IPAddress
            sRet = objCOA.SaveChartofACC(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            sArray = sRet.Split(",")
            LoadTree(0)

            If sArray(1) = 0 Then
                lblValidationMsgCOA.Text = "Successfully Saved and Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)
            ElseIf sArray(1) = 1 Then
                lblValidationMsgCOA.Text = "The Name( " & txtName.Text & ") Already Exists. Enter New Name"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)
                txtName.Text = "" : txtDescription.Text = ""
                Exit Sub
            End If


            If objCOA.igl_head = 0 Then
                ddlGroup.DataSource = objCOA.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue)
                ddlGroup.DataTextField = "Description"
                ddlGroup.DataValueField = "gl_id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
                ddlGroup.SelectedValue = sArray(0)
                ddlGroup_SelectedIndexChanged(sender, e)
            End If

            If objCOA.igl_head = 1 Then
                ddlSubGroup.DataSource = objCOA.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)
                ddlSubGroup.DataTextField = "Description"
                ddlSubGroup.DataValueField = "gl_id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select Sub Group")
                ddlSubGroup.SelectedValue = sArray(0)
                ddlSubGroup_SelectedIndexChanged(sender, e)
            End If

            If objCOA.igl_head = 2 Then
                ddlGL.DataSource = objCOA.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                ddlGL.DataTextField = "Description"
                ddlGL.DataValueField = "gl_id"
                ddlGL.DataBind()
                ddlGL.Items.Insert(0, "Select General Ledger")
                ddlGL.SelectedValue = sArray(0)
                ddlGL_SelectedIndexChanged(sender, e)
            End If


            If objCOA.igl_head = 3 Then
                ddlSubGL.DataSource = objCOA.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)
                ddlSubGL.DataTextField = "Description"
                ddlSubGL.DataValueField = "gl_id"
                ddlSubGL.DataBind()
                ddlSubGL.Items.Insert(0, "Select Sub General Ledger")
                ddlSubGL.SelectedValue = sArray(0)
                ddlSubGL_SelectedIndexChanged(sender, e)
            End If

            If ddlHead.SelectedIndex > 0 Then
                LoadTree(ddlHead.SelectedValue)
            Else
                LoadTree(0)
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            If ddlGroup.SelectedIndex > 0 Then
                objCOA.igl_id = ddlGroup.SelectedValue
            End If

            If ddlSubGroup.SelectedIndex > 0 Then
                objCOA.igl_id = ddlSubGroup.SelectedValue
            End If

            If ddlGL.SelectedIndex > 0 Then
                objCOA.igl_id = ddlGL.SelectedValue
            End If

            If ddlSubGL.SelectedIndex > 0 Then
                objCOA.igl_id = ddlSubGL.SelectedValue
            End If

            objCOA.sgl_Desc = objGen.SafeSQL(txtName.Text)
            objCOA.sgl_reason_Creation = objGen.SafeSQL(txtDescription.Text)
            objCOA.igl_UpdatedBy = sSession.UserID
            objCOA.igl_CompId = sSession.AccessCodeID
            objCOA.sgl_IPAddress = sSession.IPAddress
            objCOA.UpdateChartofAcc(sSession.AccessCode, sSession.AccessCodeID, objCOA)
            LoadTree(0)
            lblValidationMsgCOA.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            If ddlHead.SelectedIndex > 0 Then
                ddlGroup.DataSource = objCOA.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, ddlHead.SelectedValue)
                ddlGroup.DataTextField = "Description"
                ddlGroup.DataValueField = "gl_id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
            End If

            If ddlGroup.SelectedIndex > 0 Then
                ddlSubGroup.DataSource = objCOA.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)
                ddlSubGroup.DataTextField = "Description"
                ddlSubGroup.DataValueField = "gl_id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select Sub Group")
            End If

            If ddlSubGroup.SelectedIndex > 0 Then
                ddlGL.DataSource = objCOA.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                ddlGL.DataTextField = "Description"
                ddlGL.DataValueField = "gl_id"
                ddlGL.DataBind()
                ddlGL.Items.Insert(0, "Select General Ledger")
            End If

            If ddlGL.SelectedIndex > 0 Then
                ddlSubGL.DataSource = objCOA.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)
                ddlSubGL.DataTextField = "Description"
                ddlSubGL.DataValueField = "gl_id"
                ddlSubGL.DataBind()
                ddlSubGL.Items.Insert(0, "--- Select Sub General Ledger ---")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub

    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Try
            If ddlGroup.SelectedIndex > 0 Then
                iGlID = ddlGroup.SelectedValue
            End If

            If ddlSubGroup.SelectedIndex > 0 Then
                iGlID = ddlSubGroup.SelectedValue
            End If

            If ddlGL.SelectedIndex > 0 Then
                iGlID = ddlGL.SelectedValue
            End If

            If ddlSubGL.SelectedIndex > 0 Then
                iGlID = ddlSubGL.SelectedValue
            End If

            objCOA.ApproveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGlID)
            lblValidationMsgCOA.Text = "Successfully Approved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGlID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCode.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescription.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    imgbtnSave.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False

                    If sCOAAP = "YES" Then
                        imgbtnWaiting.Visible = True
                    End If
                    If sCOASave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If
                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                        lblStatus.Text = "De-Activated"
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                    imgbtnAdd.Visible = False
                    If sCOAAD = "YES" Then
                        imgbtnActivate.Visible = True
                    End If
                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                        lblStatus.Text = "Activated"
                    imgbtnSave.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False

                    If sCOAAD = "YES" Then
                        imgbtnDeActivate.Visible = True
                    End If
                    If sCOASave = "YES" Then
                        imgbtnAdd.Visible = True
                        imgbtnUpdate.Visible = True
                    End If
                End If
                End If


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
        End Try
    End Sub

    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Try
            If ddlGroup.SelectedIndex > 0 Then
                iGlID = ddlGroup.SelectedValue
            End If

            If ddlSubGroup.SelectedIndex > 0 Then
                iGlID = ddlSubGroup.SelectedValue
            End If

            If ddlGL.SelectedIndex > 0 Then
                iGlID = ddlGL.SelectedValue
            End If

            If ddlSubGL.SelectedIndex > 0 Then
                iGlID = ddlSubGL.SelectedValue
            End If

            objCOA.ActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGlID)
            lblValidationMsgCOA.Text = "Successfully Activated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGlID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCode.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescription.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    imgbtnSave.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False

                    If sCOAAP = "YES" Then
                        imgbtnWaiting.Visible = True
                    End If
                    If sCOASave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If

                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                    imgbtnAdd.Visible = False
                    If sCOAAD = "YES" Then
                        imgbtnActivate.Visible = True
                    End If
                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    imgbtnSave.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False

                    If sCOAAD = "YES" Then
                        imgbtnDeActivate.Visible = True
                    End If
                    If sCOASave = "YES" Then
                        imgbtnAdd.Visible = True
                        imgbtnUpdate.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click")
        End Try
    End Sub

    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim iGlID As Integer = 0
        Dim dt As New DataTable
        Try
            If ddlGroup.SelectedIndex > 0 Then
                iGlID = ddlGroup.SelectedValue
            End If

            If ddlSubGroup.SelectedIndex > 0 Then
                iGlID = ddlSubGroup.SelectedValue
            End If

            If ddlGL.SelectedIndex > 0 Then
                iGlID = ddlGL.SelectedValue
            End If

            If ddlSubGL.SelectedIndex > 0 Then
                iGlID = ddlSubGL.SelectedValue
            End If

            objCOA.DeActiveChartOFAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iGlID)
            lblValidationMsgCOA.Text = "Successfully De-Activated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidationCOA').modal('show');", True)

            dt = objCOA.GetCOADetails(sSession.AccessCode, sSession.AccessCodeID, iGlID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("gl_glcode").ToString()) = False Then
                    txtCode.Text = dt.Rows(0)("gl_glcode").ToString()
                Else
                    txtCode.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                    txtName.Text = dt.Rows(0)("gl_Desc").ToString()
                Else
                    txtName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("gl_Reason_Creation").ToString()) = False Then
                    txtDescription.Text = dt.Rows(0)("gl_Reason_Creation").ToString()
                Else
                    txtDescription.Text = ""
                End If

                If dt.Rows(0)("gl_Status").ToString() = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    imgbtnSave.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False

                    If sCOAAP = "YES" Then
                        imgbtnWaiting.Visible = True
                    End If
                    If sCOASave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If
                ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                    lblStatus.Text = "De-Activated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                    imgbtnAdd.Visible = False
                    If sCOAAD = "YES" Then
                        imgbtnActivate.Visible = True
                    End If
                ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                    lblStatus.Text = "Activated"
                    imgbtnSave.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False

                    If sCOAAD = "YES" Then
                        imgbtnDeActivate.Visible = True
                    End If
                    If sCOASave = "YES" Then
                        imgbtnAdd.Visible = True
                        imgbtnUpdate.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click")
        End Try
    End Sub

    Private Sub tvMCAccount_NodeClick(sender As Object, args As TreeNodeEventArgs) Handles tvMCAccount.NodeClick
        Try
            lblError.Text = ""
            txtParentID.Text = args.Node.DataKey
            txtCurrentID.Text = args.Node.DataKey
            txtDepthID.Text = args.Node.Depth

            LoadOrgStructureDetails(Val(txtParentID.Text), Val(txtCurrentID.Text), Val(txtDepthID.Text))
            txtName.Focus()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "tvMCAccount_NodeClick")
        End Try
    End Sub

    Private Sub LoadOrgStructureDetails(ByVal iParentID As Integer, ByVal iCurrentID As Integer, ByVal iDepthID As Integer)
        Dim sPath As String = ""
        Dim dt As New DataTable, dtLoad As New DataTable
        Try
            txtCode.Enabled = False
            If sCOASave = "YES" Then
                imgbtnUpdate.Visible = True
            End If
            'Load CHartofAccounts Details
            dt = objCOA.GetChartOfAccountsDetails(sSession.AccessCode, sSession.AccessCodeID, iCurrentID)

            If IsDBNull(dt.Rows(0)("gl_glCode").ToString()) = False Then
                txtCode.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("gl_glCode").ToString())
            End If

            If IsDBNull(dt.Rows(0)("gl_Desc").ToString()) = False Then
                txtName.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("gl_Desc").ToString())
            End If

            If IsDBNull(dt.Rows(0)("gl_reason_Creation").ToString()) = False Then
                txtDescription.Text = objGen.ReplaceSafeSQL(dt.Rows(0)("gl_reason_Creation").ToString())
            End If

            If dt.Rows(0)("gl_Status").ToString() = "W" Then
                lblStatus.Text = "Waiting for Approval"
                imgbtnSave.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False

                If sCOAAP = "YES" Then
                    imgbtnWaiting.Visible = True
                End If
                If sCOASave = "YES" Then
                    imgbtnUpdate.Visible = True
                End If
            ElseIf dt.Rows(0)("gl_Status").ToString() = "D" Then
                lblStatus.Text = "De-Activated"
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False
                imgbtnAdd.Visible = False
                If sCOAAD = "YES" Then
                    imgbtnActivate.Visible = True
                End If
            ElseIf dt.Rows(0)("gl_Status").ToString() = "A" Then
                lblStatus.Text = "Activated"
                imgbtnSave.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False

                If sCOAAD = "YES" Then
                    imgbtnDeActivate.Visible = True
                End If
                If sCOASave = "YES" Then
                    imgbtnAdd.Visible = True
                    imgbtnUpdate.Visible = True
                End If
            End If

            Dim iPathParentID As Integer = txtParentID.Text
            sPath = objCOA.GetchartofAccountPath(sSession.AccessCode, sSession.AccessCodeID, iCurrentID)
            If sPath.EndsWith("/") = True Then
                sPath = sPath.Remove(Len(sPath) - 1, 1)
            End If
            lblPath.Text = sPath

            If iDepthID = 3 Then  'SubGL
                Dim iParent As Integer = 0
                ddlHead.SelectedValue = dt.Rows(0)("gl_AccHead").ToString()

                ddlSubGL.DataSource = objCOA.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_Parent").ToString())
                ddlSubGL.DataTextField = "Description"
                ddlSubGL.DataValueField = "gl_id"
                ddlSubGL.DataBind()
                ddlSubGL.Items.Insert(0, "Select Sub General Ledger")
                ddlSubGL.SelectedValue = iCurrentID

                ddlGL.DataSource = objCOA.LoadGL(sSession.AccessCode, sSession.AccessCodeID, objCOA.LoadParent(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_Parent").ToString()))
                ddlGL.DataTextField = "Description"
                ddlGL.DataValueField = "gl_id"
                ddlGL.DataBind()
                ddlGL.Items.Insert(0, "Select General Ledger")
                ddlGL.SelectedValue = dt.Rows(0)("gl_Parent").ToString()

                Dim iGLParent As Integer = 0
                iGLParent = objCOA.LoadParent(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)
                ddlSubGroup.DataSource = objCOA.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, objCOA.LoadParent(sSession.AccessCode, sSession.AccessCodeID, iGLParent))
                ddlSubGroup.DataTextField = "Description"
                ddlSubGroup.DataValueField = "gl_id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select Sub Group")
                ddlSubGroup.SelectedValue = iGLParent

                ddlGroup.DataSource = objCOA.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_AccHead").ToString())
                ddlGroup.DataTextField = "Description"
                ddlGroup.DataValueField = "gl_id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
                ddlGroup.SelectedValue = objCOA.LoadParent(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)

            ElseIf iDepthID = 2 Then ' GL
                ddlHead.SelectedValue = dt.Rows(0)("gl_AccHead").ToString()

                ddlGL.DataSource = objCOA.LoadGL(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_Parent").ToString())
                ddlGL.DataTextField = "Description"
                ddlGL.DataValueField = "gl_id"
                ddlGL.DataBind()
                ddlGL.Items.Insert(0, "Select General Ledger")
                ddlGL.SelectedValue = iCurrentID

                ddlSubGL.DataSource = objCOA.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlGL.SelectedValue)
                ddlSubGL.DataTextField = "Description"
                ddlSubGL.DataValueField = "gl_id"
                ddlSubGL.DataBind()
                ddlSubGL.Items.Insert(0, "Select Sub General Ledger")

                ddlSubGroup.DataSource = objCOA.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, objCOA.LoadParent(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_Parent").ToString()))
                ddlSubGroup.DataTextField = "Description"
                ddlSubGroup.DataValueField = "gl_id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select Sub Group")
                ddlSubGroup.SelectedValue = dt.Rows(0)("gl_Parent").ToString()

                ddlGroup.DataSource = objCOA.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_AccHead").ToString())
                ddlGroup.DataTextField = "Description"
                ddlGroup.DataValueField = "gl_id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
                ddlGroup.SelectedValue = objCOA.LoadParent(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)

            ElseIf iDepthID = 1 Then ' SubGroup

                ddlHead.SelectedValue = dt.Rows(0)("gl_AccHead").ToString()

                ddlGroup.DataSource = objCOA.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_AccHead").ToString())
                ddlGroup.DataTextField = "Description"
                ddlGroup.DataValueField = "gl_id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
                ddlGroup.SelectedValue = dt.Rows(0)("gl_Parent").ToString()

                ddlSubGroup.DataSource = objCOA.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_Parent").ToString())
                ddlSubGroup.DataTextField = "Description"
                ddlSubGroup.DataValueField = "gl_id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select Sub Group")
                ddlSubGroup.SelectedValue = iCurrentID

                ddlGL.DataSource = objCOA.LoadGL(sSession.AccessCode, sSession.AccessCodeID, ddlSubGroup.SelectedValue)
                ddlGL.DataTextField = "Description"
                ddlGL.DataValueField = "gl_id"
                ddlGL.DataBind()
                ddlGL.Items.Insert(0, "Select General Ledger")

                ddlSubGL.DataSource = dtLoad
                ddlSubGL.DataBind()

            ElseIf iDepthID = 0 Then ' Group
                ddlHead.SelectedValue = dt.Rows(0)("gl_AccHead").ToString()

                ddlGroup.DataSource = objCOA.LoadGroup(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("gl_AccHead").ToString())
                ddlGroup.DataTextField = "Description"
                ddlGroup.DataValueField = "gl_id"
                ddlGroup.DataBind()
                ddlGroup.Items.Insert(0, "Select Group")
                ddlGroup.SelectedValue = iCurrentID

                ddlSubGroup.DataSource = objCOA.LoadSubGroup(sSession.AccessCode, sSession.AccessCodeID, ddlGroup.SelectedValue)
                ddlSubGroup.DataTextField = "Description"
                ddlSubGroup.DataValueField = "gl_id"
                ddlSubGroup.DataBind()
                ddlSubGroup.Items.Insert(0, "Select Sub Group")

                ddlGL.DataSource = dtLoad
                ddlGL.DataBind()

                ddlSubGL.DataSource = dtLoad
                ddlSubGL.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOrgStructureDetails")
        End Try
    End Sub

    Private Sub lnkCOA_Click(sender As Object, e As EventArgs) Handles lnkCOA.Click
        Try
            TVStadardCOA.DataKeyField = "gl_id"
            TVStadardCOA.DataParentField = "gl_parent"
            TVStadardCOA.DataTextField = "gl_desc"
            TVStadardCOA.DataSource = objCOA.LoadStandardChartOfAccounts(sSession.AccessCode, sSession.AccessCodeID).Tables(0)
            TVStadardCOA.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkCOA_Click")
        End Try
    End Sub

    Private Sub TVStadardCOA_NodeClick(sender As Object, args As TreeNodeEventArgs) Handles TVStadardCOA.NodeClick
        Try
            lblError.Text = ""
            txtParentID.Text = args.Node.DataKey
            txtCurrentID.Text = args.Node.DataKey
            txtDepthID.Text = args.Node.Depth
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "TVStadardCOA_NodeClick")
        End Try
    End Sub

    Private Sub btnStandardOK_Click(sender As Object, e As EventArgs) Handles btnStandardOK.Click
        Try
            lblValidationMsgChart.Text = "Do you want to import standard Chart of Account?. Your Previous Data will be lost"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType1').addClass('alert alert-warning');$('#ModalValidationChart').modal('show');", True)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnStandardOK_Click")
        End Try
    End Sub
    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Dim dt As New DataTable
        Try
            objCOA.ImportCOA(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress)
            LoadTree(0)
            lblValidationMsgCOA.Text = "Successfully Imported."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidationCOA').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnYes_Click")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objCOA.ChartOfAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/RptChartOfAccounts.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objGenFun.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Chart Of Accounts", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ChartOfAccounts" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = objCOA.ChartOfAccounts(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Masters/RptChartOfAccounts.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objGenFun.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Chart Of Accounts", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ChartOfAccounts" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
        End Try
    End Sub
End Class
