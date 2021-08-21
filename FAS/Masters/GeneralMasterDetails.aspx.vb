Imports System
Imports System.Data
Imports BusinesLayer
Partial Class Masters_GeneralMasterDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_GeneralMasterDetails"
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass

    Private Shared sSession As AllSession
    Private Shared sTypeName As String
    Private Shared sMasterName As String
    Private Shared iMasterID As Integer
    Private Shared iPKID As Integer
    Private Shared sTableName As String
    Private objclsModulePermission As New clsModulePermission
    Private Shared sGMSave As String
    Private Shared sGMFlag As String
    Private Shared sGMBackStatus As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Dim iMaxID As Integer
        Dim sMasterType As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnFormsSave.ImageUrl = "~/Images/Save16.png"
                imgbtnPeriodicitySave.ImageUrl = "~/Images/Save16.png"
                DivForm.Visible = False : DivPeriodicity.Visible = False
                'imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False


                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "GENM")
                imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                sGMSave = "NO"
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",New,") = True Then
                        imgbtnAdd.Visible = True
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnSave.Visible = True
                        sGMSave = "YES"
                    End If
                End If
                'sFormButtons = objclsFASPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MSGM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then

                '        sGMSave = "YES"
                '    End If
                'End If

                If Request.QueryString("StatusID") IsNot Nothing Then
                    sGMBackStatus = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("MasterName") IsNot Nothing Then
                    sMasterName = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterName")))
                    sMasterType = objMaster.GetMasterType(sSession.AccessCode, sSession.AccessCodeID, sMasterName)
                    iMasterID = sMasterName
                    lblMasterHead.Text = "Existing " & sMasterType
                    lblDesc.Text = "* Name" : sTypeName = sMasterType
                    BindDescDetails()
                End If

                If Request.QueryString("MasterID") IsNot Nothing Then
                    ddlDesc.SelectedValue = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterID")))
                    ddlDesc_SelectedIndexChanged(sender, e)
                End If
                REVDescName.ValidationGroup = False

                If UCase(ddlDesc.SelectedItem.Text) = UCase("Select Category Of TaxPayer") Then
                    DivForm.Visible = True : DivPeriodicity.Visible = True
                    BindForms() : BindPeriodicity()
                Else
                    DivForm.Visible = False : DivPeriodicity.Visible = False
                End If

                RFVDescName.ValidationGroup = True
                RFVDescName.ErrorMessage = "Enter " & sTypeName & " Name."
                RFVDescName.ValidationGroup = "Validate" : REVDescName.ValidationGroup = "Validate"
                REVNotes.ValidationGroup = "Validate"

                RFVDescName.ControlToValidate = "txtDesc" : REVDescName.ValidationExpression = "^(.{0,100})$" : REVDescName.ErrorMessage = sTypeName & " exceeded maximum size(max 100 character)."
                REVNotes.ControlToValidate = "txtNotes" : REVNotes.ValidationExpression = "^(.{0,100})$" : REVNotes.ErrorMessage = "Notes exceeded maximum size(max 100 character)."

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    Private Sub BindDescDetails()
        Dim dt As New DataTable
        Try
            dt = objMaster.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, sMasterName)
            ddlDesc.DataSource = dt
            ddlDesc.DataTextField = "Mas_Desc"
            ddlDesc.DataValueField = "Mas_Id"
            ddlDesc.DataBind()
            ddlDesc.Items.Insert(0, "Select " & sTypeName & "")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindForms()
        Dim dt As New DataTable
        Dim iDesID As Integer
        Try
            If ddlDesc.SelectedIndex > 0 Then
                iDesID = ddlDesc.SelectedValue
            Else
                iDesID = 0
            End If
            dt = objMaster.LoadForms(sSession.AccessCode, sSession.AccessCodeID, iDesID)
            ddlForms.DataSource = dt
            ddlForms.DataTextField = "Mas_Desc"
            ddlForms.DataValueField = "Mas_Id"
            ddlForms.DataBind()
            ddlForms.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindPeriodicity()
        Dim dt As New DataTable
        Dim iDesID As Integer
        Try
            If ddlDesc.SelectedIndex > 0 Then
                iDesID = ddlDesc.SelectedValue
            Else
                iDesID = 0
            End If
            dt = objMaster.LoadPeriodicity(sSession.AccessCode, sSession.AccessCodeID, iDesID)
            ddlPeriodicity.DataSource = dt
            ddlPeriodicity.DataTextField = "Mas_Desc"
            ddlPeriodicity.DataValueField = "Mas_Id"
            ddlPeriodicity.DataBind()
            ddlPeriodicity.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDesc.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblGeneralMasterStatus.Text = "" : txtDesc.Text = "" : txtNotes.Text = ""
            If ddlDesc.SelectedIndex > 0 Then

                If UCase(ddlDesc.Items(0).Text) = UCase("Select Category Of TaxPayer") Then
                    DivForm.Visible = True : DivPeriodicity.Visible = True
                    BindForms() : BindPeriodicity()
                Else
                    DivForm.Visible = False : DivPeriodicity.Visible = False
                End If


                dt = objMaster.GetDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue, sMasterName)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("Mas_Desc")) = False Then
                        txtDesc.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Desc")))
                    End If
                    If IsDBNull(dt.Rows(0).Item("Mas_Remarks")) = False Then
                        txtNotes.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Remarks")))
                    End If
                    If IsDBNull(dt.Rows(0).Item("Mas_DelFlag")) = False Then
                        sGMFlag = dt.Rows(0).Item("Mas_DelFlag")
                    End If
                End If

                If sGMFlag = "W" Then
                    lblGeneralMasterStatus.Text = "Waiting for Approval"
                    'If sGMSave = "YES" Then
                    imgbtnSave.Visible = False
                    If sGMSave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If
                    'End If
                ElseIf sGMFlag = "D" Then
                        lblGeneralMasterStatus.Text = "De-Activated"
                        imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                    Else
                        lblGeneralMasterStatus.Text = "Activated"
                    ' If sGMSave = "YES" Then
                    imgbtnSave.Visible = False
                    If sGMSave = "YES" Then
                        imgbtnUpdate.Visible = True
                    End If
                    'End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDesc_SelectedIndexChanged")
        End Try
    End Sub

    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        Dim iMaxID As Integer = 0
        Try
            lblError.Text = ""
            'imgbtnAdd.Visible = True
            imgbtnBack.Visible = True : imgbtnUpdate.Visible = False
            If sGMSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            ddlDesc.SelectedIndex = 0 : lblGeneralMasterStatus.Text = ""
            txtDesc.Text = "" : txtNotes.Text = ""
            iMaxID = objGenFun.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "ACC_General_Master", "Mas_iD", "Mas_CompID")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterID As Object
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objGen.EncryptQueryString(sGMBackStatus))
            oMasterID = HttpUtility.UrlEncode(objGen.EncryptQueryString(Val(iMasterID)))
            Response.Redirect(String.Format("~/Masters/GeneralMaster.aspx?StatusID={0}&MasterID={1}", oStatusID, oMasterID), False) 'Masters/GeneralMaster
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    Protected Sub imgbtnSave_Click(sender As Object, e As EventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""
            If txtDesc.Text.Trim = "" Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Enter " & sTypeName & "." : lblError.Text = "Enter " & sTypeName & "."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtDesc.Text.Trim.Length > 100 Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = sTypeName & " exceeded maximum size(Max 100 characters)." : lblError.Text = sTypeName & " exceeded maximum size(Max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If txtNotes.Text.Trim.Length > 100 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Note exceeded maximum size(max 100 characters)." : lblError.Text = "Note exceeded maximum size(max 100 characters)."
                txtNotes.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            bCheck = objMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objGen.SafeSQL(txtDesc.Text.Trim), iMasterID)
            If bCheck = True Then
                lblGeneralMasterDetailsValidationMsg.Text = "Entered " & sTypeName & " Name already exist." : lblError.Text = "Entered " & sTypeName & " Name already exist."
                txtDesc.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            sMasterName = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterName")))
            Arr = objMaster.SaveGeneralMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sMasterName, objGen.SafeSQL(txtDesc.Text.Trim), objGen.SafeSQL(txtNotes.Text.Trim), 0, sSession.IPAddress)

            'vijaylakshmi 20-04-19 (from bvr 03_08_21 dk)
            If (sMasterName = 2) Then
                objMaster.insertGstCategory(sSession.AccessCode, sSession.AccessCodeID, objGen.SafeSQL(txtDesc.Text.Trim), Arr(1))
            End If


            BindDescDetails()
            ddlDesc.SelectedValue = Arr(1)
            ddlDesc_SelectedIndexChanged(sender, e)

            If Arr(0) = "3" Then
                lblGeneralMasterDetailsValidationMsg.Text = "Successfully Saved & Waiting for Approval." : lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                sGMBackStatus = 2
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub

    Protected Sub imgbtnUpdate_Click(sender As Object, e As EventArgs) Handles imgbtnUpdate.Click
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""
            If txtDesc.Text.Trim = "" Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Enter " & sTypeName & "." : lblError.Text = "Enter " & sTypeName & "."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtDesc.Text.Trim.Length > 100 Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = sTypeName & " exceeded maximum size(Max 100 characters)." : lblError.Text = sTypeName & " exceeded maximum size(Max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If txtNotes.Text.Trim.Length > 100 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Note exceeded maximum size(Max 100 characters)." : lblError.Text = "Note exceeded maximum size(max 100 characters)."
                txtNotes.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            sMasterName = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterName")))

            If ddlDesc.SelectedIndex > 0 Then
                bCheck = objMaster.CheckDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue, sMasterName)

                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "De-Activated description cannot be updated." : lblError.Text = "De-Activated description cannot be updated."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            End If

            Arr = objMaster.SaveGeneralMaster(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sMasterName, objGen.SafeSQL(txtDesc.Text.Trim), objGen.SafeSQL(txtNotes.Text.Trim), ddlDesc.SelectedValue, sSession.IPAddress)
            BindDescDetails()
            ddlDesc.SelectedValue = Arr(1)
            ddlDesc_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                If sGMFlag = "W" Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Successfully Updated & Waiting for Approval." : lblError.Text = "Successfully Updated & Waiting for Approval."
                Else
                    lblGeneralMasterDetailsValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Private Sub imgbtnFormsSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnFormsSave.Click
        Try
            If ddlDesc.SelectedIndex > 0 Then
                If txtFormDesc.Text <> "" Then
                    If ddlForms.SelectedIndex > 0 Then
                        objMaster.SaveForms(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlDesc.SelectedValue, Trim(txtFormDesc.Text), ddlForms.SelectedValue)
                        lblError.Text = "Successfully Updated."
                        imgbtnFormsSave.ImageUrl = "~/Images/Save16.png"
                    Else
                        objMaster.SaveForms(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlDesc.SelectedValue, Trim(txtFormDesc.Text), 0)
                        lblError.Text = "Successfully Saved."
                    End If
                    txtFormDesc.Text = ""
                    BindForms()
                End If
            Else
                lblError.Text = "Select Category Of TaxPayer"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnFormsSave_Click")
        End Try
    End Sub
    Private Sub imgbtnPeriodicitySave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnPeriodicitySave.Click
        Try
            If ddlDesc.SelectedIndex > 0 Then
                If txtPeriodicityDesc.Text <> "" Then
                    If ddlPeriodicity.SelectedIndex > 0 Then
                        objMaster.SavePeriodicity(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlDesc.SelectedValue, Trim(txtPeriodicityDesc.Text), ddlPeriodicity.SelectedValue)
                        lblError.Text = "Successfully Updated."
                        imgbtnPeriodicitySave.ImageUrl = "~/Images/Save16.png"
                    Else
                        objMaster.SavePeriodicity(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.UserID, ddlDesc.SelectedValue, Trim(txtPeriodicityDesc.Text), 0)
                        lblError.Text = "Successfully Saved."
                    End If
                    txtPeriodicityDesc.Text = ""
                    BindPeriodicity()
                End If
            Else
                lblError.Text = "Select Category Of TaxPayer"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPeriodicitySave_Click")
        End Try
    End Sub
    Private Sub ddlForms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlForms.SelectedIndexChanged
        Try
            If ddlForms.SelectedIndex > 0 Then
                imgbtnFormsSave.ImageUrl = "~/Images/Update16.png"
                txtFormDesc.Text = objMaster.GetFormDesc(sSession.AccessCode, sSession.AccessCodeID, ddlForms.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlForms_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlPeriodicity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPeriodicity.SelectedIndexChanged
        Try
            If ddlPeriodicity.SelectedIndex > 0 Then
                imgbtnPeriodicitySave.ImageUrl = "~/Images/Update16.png"
                txtPeriodicityDesc.Text = objMaster.GetPeriodicityDesc(sSession.AccessCode, sSession.AccessCodeID, ddlPeriodicity.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPeriodicity_SelectedIndexChanged")
        End Try
    End Sub
End Class
