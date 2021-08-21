Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Masters_ApplicationConfiguration
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_ApplicationCOnfiguration"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objSettings As New clsApplicationSettings
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Private objclsGRACePermission As New clsFASPermission
    Private Shared sPSave As String
    Private Shared dtPswd As New DataTable
    Private Shared dtES As New DataTable
    Dim objDepComp As New ClsDepreciationComputation
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""

        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnUpdate.Visible = False
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "ACO")

                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                        imgbtnUpdate.Visible = True
                    End If
                    'If sFormButtons.Contains(",Report,") = True Then
                    '    imgbtnReport.Visible = True
                    'End If
                End If
                'imgbtnUpdate.Visible = False : imgbtnReport.Visible = False
                'sPSave = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FasACon1", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then
                '    End If
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnUpdate.Visible = True
                '        sPSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If
                RFVSenerEID.ErrorMessage = "Enter Sender E-Mail ID."
                REVSenerEID.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" '"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?){0,100}$"
                REVSenerEID.ErrorMessage = "Enter valid Sender E-Mail ID."

                RFVSMS.ErrorMessage = "Enter SMS Sender ID."
                REVSMS.ValidationExpression = "\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                REVSMS.ErrorMessage = "Enter valid SMS Sender ID."

                If sSession.YearID = 0 Then
                    lblError.Text = "Set The Year For Application in Holiday & Year Master."
                    Exit Sub
                End If
                GetAppStartDate()
                LoadCurrencyTypeDB()
                LoadChkPasswordContains()
                dtPswd = objSettings.GetPasswordSettings(sSession.AccessCode, sSession.AccessCodeID)
                dtES = objSettings.GetEmailsettings(sSession.AccessCode, sSession.AccessCodeID)
                GetAppSettings() : GetPasswordDetails() : GetEmailSettings()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub GetAppStartDate()
        Dim dt As New DataTable
        Dim i As Integer
        Try
            dt = objSettings.GetAppSatartDate(sSession.AccessCode, sSession.AccessCodeID)
            For i = 0 To dt.Rows.Count - 1
                If IsDBNull(dt.Rows(i)("AS_StartDate")) = False Then
                    txtStartDate.Text = objGen.FormatDtForRDBMS(dt.Rows(i)("AS_StartDate"), "D")
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetAppStartDate")
        End Try
    End Sub
    Public Sub LoadChkPasswordContains()
        Dim i As Integer
        Try
            chkPasswordContain.Items.Add(New ListItem("Capital Letter(A-Z)", "1"))
            chkPasswordContain.Items.Add(New ListItem("Small Letter(a-z)", "2"))
            chkPasswordContain.Items.Add(New ListItem("Special Symbol", "3"))
            chkPasswordContain.Items.Add(New ListItem("Integer(0-9)", "4"))

            For i = 0 To chkPasswordContain.Items.Count - 1
                chkPasswordContain.Items(i).Selected = True
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadCurrencyTypeDB()
        Dim dt As New DataTable
        Try
            dt = objSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCurrency.DataSource = dt
            ddlCurrency.DataTextField = "CUR_CODE"
            ddlCurrency.DataValueField = "CUR_ID"
            ddlCurrency.DataBind()
            ddlCurrency.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub imgbtnUpdate_Click(sender As Object, e As EventArgs) Handles imgbtnUpdate.Click
        Dim sdate As Date
        Dim iyearid As Integer
        Dim sPasswordContains As String = ""
        Try
            If ddlFileSize.SelectedValue = 0 Then
                ddlFileSize.Focus()
                lblFASSettingsValidationMsg.Text = "Select File Size."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlCurrency.SelectedValue = 0 Then
                ddlCurrency.Focus()
                lblFASSettingsValidationMsg.Text = "Select Currency Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlSessionTimeOutWarning.SelectedValue = 0 Then
                ddlSessionTimeOutWarning.Focus()
                lblFASSettingsValidationMsg.Text = "Select Session Time Out Warning."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            If txtFileInDBPath.Text.Trim = "" Then
                txtFileInDBPath.Focus()
                lblFASSettingsValidationMsg.Text = "Enter Attachment File Path."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            'Password Management
            If txtMaxPassword.Text.Trim = "" Then
                lblFASSettingsValidationMsg.Text = "Enter Max Password Character."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show'); $('#txtMaxPassword').focus();", True)
                txtMaxPassword.Focus()
                Exit Sub
            ElseIf IsNumeric(txtMaxPassword.Text) = False Then
                lblFASSettingsValidationMsg.Text = "Enter valid Max Password Character(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtMaxPassword').focus();", True)
                txtMaxPassword.Focus()
                Exit Sub
            ElseIf txtMaxPassword.Text = "0" Then
                lblFASSettingsValidationMsg.Text = "Max Password Character should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtMaxPassword').focus();", True)
                txtMaxPassword.Focus()
                Exit Sub
            ElseIf Val(txtMaxPassword.Text) > 100 Then
                lblFASSettingsValidationMsg.Text = "Max Password Character should be less than or equal 100."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtMaxPassword').focus();", True)
                txtMaxPassword.Focus()
                Exit Sub
            Else
                objSettings.iMPS_MaximumChar = objGen.SafeSQL(txtMaxPassword.Text.Trim)
            End If

            If txtMinPassword.Text.Trim = "" Then
                lblFASSettingsValidationMsg.Text = "Enter Min Password Character."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show'); $('#txtMinPassword').focus();", True)
                txtMinPassword.Focus()
                Exit Sub
            ElseIf IsNumeric(txtMinPassword.Text) = False Then
                lblFASSettingsValidationMsg.Text = "Enter valid Min Password Character(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtMinPassword').focus();", True)
                txtMinPassword.Focus()
                Exit Sub
            ElseIf txtMinPassword.Text < 4 Then
                lblFASSettingsValidationMsg.Text = "Min Password Character should be greater than or equal 4."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtMinPassword').focus();", True)
                txtMinPassword.Focus()
                Exit Sub
            ElseIf Val(txtMinPassword.Text) > 10 Then
                lblFASSettingsValidationMsg.Text = "Min Password Character should be less than or equal 10."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtMinPassword').focus();", True)
                txtMinPassword.Focus()
                Exit Sub
            Else
                objSettings.iMPS_MinimumChar = objGen.SafeSQL(txtMinPassword.Text.Trim)
            End If

            If Val(txtMinPassword.Text) > Val(txtMaxPassword.Text) Then
                lblFASSettingsValidationMsg.Text = "Max Password Character should be greater than Minimum Password Character."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtMaxPassword').focus();", True)
                txtMaxPassword.Focus()
                Exit Sub
            End If

            If txtRecoveryAttempt.Text.Trim = "" Then
                lblFASSettingsValidationMsg.Text = "Enter No. of Recovery Attempts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show'); $('#txtRecoveryAttempt').focus();", True)
                txtRecoveryAttempt.Focus()
                Exit Sub
            ElseIf IsNumeric(txtRecoveryAttempt.Text) = False Then
                lblFASSettingsValidationMsg.Text = "Enter valid No. of Recovery Attempts(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtRecoveryAttempt').focus();", True)
                txtRecoveryAttempt.Focus()
                Exit Sub
            ElseIf txtRecoveryAttempt.Text = "0" Then
                lblFASSettingsValidationMsg.Text = "No. of Recovery Attempts should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtRecoveryAttempt').focus();", True)
                txtRecoveryAttempt.Focus()
                Exit Sub
            ElseIf Val(txtRecoveryAttempt.Text) > 10 Then
                lblFASSettingsValidationMsg.Text = "No. of Recovery Attempts should be less than or equal 10."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtRecoveryAttempt').focus();", True)
                txtRecoveryAttempt.Focus()
                Exit Sub
            Else
                objSettings.iMPS_RecoveryAttempts = objGen.SafeSQL(txtRecoveryAttempt.Text.Trim)
            End If

            If txtUnsuccessAttempts.Text.Trim = "" Then
                lblFASSettingsValidationMsg.Text = "Enter Unsuccessful Attempts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show'); $('#txtUnsuccessAttempts').focus();", True)
                txtUnsuccessAttempts.Focus()
                Exit Sub
            ElseIf IsNumeric(txtUnsuccessAttempts.Text) = False Then
                lblFASSettingsValidationMsg.Text = "Enter valid Unsuccessful Attempts(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtUnsuccessAttempts').focus();", True)
                txtUnsuccessAttempts.Focus()
                Exit Sub
            ElseIf txtUnsuccessAttempts.Text = "0" Then
                lblFASSettingsValidationMsg.Text = "Unsuccessful Attempts should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtUnsuccessAttempts').focus();", True)
                txtUnsuccessAttempts.Focus()
                Exit Sub
            ElseIf Val(txtUnsuccessAttempts.Text) > 10 Then
                lblFASSettingsValidationMsg.Text = "Unsuccessful Attempts should be less than or equal 10."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtUnsuccessAttempts').focus();", True)
                txtUnsuccessAttempts.Focus()
                Exit Sub
            Else
                objSettings.iMPS_UnsuccessfulAttempts = objGen.SafeSQL(txtUnsuccessAttempts.Text.Trim)
            End If

            If txtExpiryDay.Text.Trim = "" Then
                txtExpiryDay.Focus()
                lblFASSettingsValidationMsg.Text = "Enter Password Expiry Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            If txtExpiryDay.Text.Trim = "" Then
                lblFASSettingsValidationMsg.Text = "Enter Password Expiry Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show'); $('#txtExpiryDay').focus();", True)
                txtExpiryDay.Focus()
                Exit Sub
            ElseIf IsNumeric(txtExpiryDay.Text.Trim) = False Then
                lblFASSettingsValidationMsg.Text = "Enter valid Password Expiry Days (only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtExpiryDay').focus();", True)
                txtExpiryDay.Focus()
                Exit Sub
            ElseIf txtExpiryDay.Text.Trim = "0" Then
                lblFASSettingsValidationMsg.Text = "Password Expiry Days should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtExpiryDay').focus();", True)
                txtExpiryDay.Focus()
                Exit Sub
            ElseIf Val(txtExpiryDay.Text.Trim) > 500 Then
                lblFASSettingsValidationMsg.Text = "Password Expiry Days should be less than or equal 500."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtExpiryDay').focus();", True)
                txtExpiryDay.Focus()
                Exit Sub
            Else
                objSettings.iMPS_PasswordExpiryDays = objGen.SafeSQL(txtExpiryDay.Text.Trim)
            End If

            If txtExpiryAlertDay.Text.Trim = "" Then
                lblFASSettingsValidationMsg.Text = "Enter Password Expiry Alert Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show'); $('#txtExpiryAlertDay').focus();", True)
                txtExpiryAlertDay.Focus()
                Exit Sub
            ElseIf IsNumeric(txtExpiryAlertDay.Text.Trim) = False Then
                lblFASSettingsValidationMsg.Text = "Enter Password Expiry Alert Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtExpiryAlertDay').focus();", True)
                txtExpiryAlertDay.Focus()
                Exit Sub
            ElseIf txtExpiryAlertDay.Text.Trim = "0" Then
                lblFASSettingsValidationMsg.Text = "Password Expiry Alert Days should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtExpiryAlertDay').focus();", True)
                txtExpiryAlertDay.Focus()
                Exit Sub
            ElseIf Val(txtExpiryAlertDay.Text.Trim) > Val(txtExpiryDay.Text.Trim) Then
                lblFASSettingsValidationMsg.Text = "Password Expiry Alert Days should be less than or equal Password Expiry Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtExpiryAlertDay').focus();", True)
                txtExpiryAlertDay.Focus()
                Exit Sub
            Else
                objSettings.iMPS_PasswordExpiryAlertDays = objGen.SafeSQL(txtExpiryAlertDay.Text.Trim)
            End If

            If txtNumberofLogin.Text.Trim = "" Then
                lblFASSettingsValidationMsg.Text = "Enter Dormant(Not Login) Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show'); $('#txtNumberofLogin').focus();", True)
                txtNumberofLogin.Focus()
                Exit Sub
            ElseIf IsNumeric(txtNumberofLogin.Text.Trim) = False Then
                lblFASSettingsValidationMsg.Text = "Enter valid Dormant(Not Login) Days(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtNumberofLogin').focus();", True)
                txtNumberofLogin.Focus()
                Exit Sub
            ElseIf txtNumberofLogin.Text.Trim = "0" Then
                lblFASSettingsValidationMsg.Text = "Password Dormant(Not Login) Days should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtNumberofLogin').focus();", True)
                txtNumberofLogin.Focus()
                Exit Sub
            ElseIf Val(txtNumberofLogin.Text.Trim) > 500 Then
                lblFASSettingsValidationMsg.Text = "Password Dormant(Not Login) Days should be less than or equal 500."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASSettingValidation').modal('show'); $('#txtNumberofLogin').focus();", True)
                txtNumberofLogin.Focus()
                Exit Sub
            Else
                objSettings.iMPS_NotLoginDays = objGen.SafeSQL(txtNumberofLogin.Text.Trim)
            End If
            If txtSMTP.Text.Trim = "" Then
                txtSMTP.Focus()
                lblFASSettingsValidationMsg.Text = "Enter SMTP Address."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            If txtSenderEmail.Text.Trim = "" Then
                txtSenderEmail.Focus()
                lblFASSettingsValidationMsg.Text = "Enter Sender E-Mail Id."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            ElseIf txtSenderEmail.Text.Trim.Length > 200 Then
                lblFASSettingsValidationMsg.Text = "Sender E-Mail ID exceeded maximum size(only 200 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            If txtPort.Text.Trim = "" Then
                txtPort.Focus()
                lblFASSettingsValidationMsg.Text = "Enter Port Number."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            If txtSMS.Text.Trim = "" Then
                lblFASSettingsValidationMsg.Text = "Enter SMS Sender ID."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                txtSMS.Focus()
                Exit Sub
            ElseIf txtSMS.Text.Trim.Length > 15 Then
                lblFASSettingsValidationMsg.Text = "SMS Sender ID exceeded maximum size(only 15 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                txtSMS.Focus()
                Exit Sub
            End If

            If txtStartDate.Text.Trim = "" Then
                txtStartDate.Focus()
                lblFASSettingsValidationMsg.Text = "Enter Application Start Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASSettingValidation').modal('show');", True)
                Exit Sub
            End If

            For i = 0 To chkPasswordContain.Items.Count - 1
                If chkPasswordContain.Items(i).Selected = True Then
                    sPasswordContains = sPasswordContains & "," & chkPasswordContain.Items(i).Value
                End If
            Next
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "ImgPath", objGen.SafeSQL(Trim(txtImgPath.Text)))
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "ErrorLog", objGen.SafeSQL(Trim(txtErrorLog.Text)))
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "ExcelPath", objGen.SafeSQL(Trim(txtTempDir.Text)))
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "FtpServer", objGen.SafeSQL(Trim(txtFTPServer.Text)))
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "HTP", objGen.SafeSQL(Trim(txtHTP.Text)))
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "Currency", ddlCurrency.SelectedValue)
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "DateFormat", ddlDateFormat.SelectedValue)
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "FileSize", ddlFileSize.SelectedValue)
            ' objSettings.SaveSettings(sSession.AccessCode, sSession.AccessCodeID, "TimeOut", ddlSessionTimeOut.SelectedValue, sSession.IPAddress, sSession.UserID)
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "TimeOutWarning", ddlSessionTimeOutWarning.SelectedValue)
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "FilesInDB", ddlFilesDB.SelectedItem.Text)
            objSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "FileInDBPath", objGen.SafeSQL(Trim(txtFileInDBPath.Text)))

            sSession.FileSize = ddlFileSize.SelectedValue
            sSession.TimeOut = ddlSessionTimeOut.SelectedValue * 60000
            sSession.TimeOutWarning = ddlSessionTimeOutWarning.SelectedIndex * 60000
            objSettings.sMPS_Password_Contains = sPasswordContains
            objSettings.iMPS_CompID = sSession.AccessCodeID
            objSettings.SavePasswordDetails(sSession.AccessCode, objSettings)


            objSettings.sConf_IPAddress = objGen.SafeSQL(txtSMTP.Text.Trim)
            objSettings.iconf_Port = objGen.SafeSQL(txtPort.Text.Trim)
            objSettings.sConf_From = objGen.SafeSQL(txtSenderEmail.Text.Trim)
            objSettings.sconf_SenderID = objGen.SafeSQL(txtSMS.Text.Trim)
            objSettings.iconf_UpdatedBy = sSession.UserID
            objSettings.iconf_CompID = sSession.AccessCodeID
            objSettings.sConf_Status = "U"
            objSettings.sConf_INS_IPAddress = sSession.IPAddress
            objSettings.SaveEmailSettings(sSession.AccessCode, objSettings)

            sSession.MaxPasswordCharacter = Val(txtMaxPassword.Text)
            sSession.MinPasswordCharacter = Val(txtMinPassword.Text)
            sSession.TimeOut = (ddlSessionTimeOut.SelectedValue) * 60000
            sSession.TimeOutWarning = (ddlSessionTimeOutWarning.SelectedValue) * 60000

            If (txtStartDate.Text = "") Then
                sdate = ""
            Else
                sdate = Date.ParseExact(Trim(txtStartDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            'clsFixedAsset settings

            If ddlMethod.SelectedIndex > 0 Then
                objDepComp.FixedAssetSetting(sSession.AccessCode, sSession.AccessCodeID, ddlMethod.SelectedIndex)
            End If

            objSettings.SaveAppSettings(sSession.AccessCode, sSession.AccessCodeID, sSession.IPAddress, sSession.YearID, sdate)
            sSession.YearID = objGenFun.GetYearID(sSession.AccessCode, sSession.AccessCodeID)
            Session("AllSession") = sSession

            lblFASSettingsValidationMsg.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASSettingValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub

    Public Sub GetAppSettings()
        Dim dt As New DataTable
        Dim i As Integer
        Try
            dt = objSettings.GetApllicationSettingsDetails(sSession.AccessCode, sSession.AccessCodeID)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("sad_Config_Key") = "ImgPath" Then
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        txtImgPath.Text = objGen.ReplaceSafeSQL(dt.Rows(i)("sad_Config_Value"))
                    End If
                End If

                If dt.Rows(i)("sad_Config_Key") = "ExcelPath" Then    'EXCEL PATH
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        txtTempDir.Text = objGen.ReplaceSafeSQL(dt.Rows(i)("sad_Config_Value"))
                    End If
                End If

                If dt.Rows(i)("sad_Config_Key") = "FilesInDB" Then    'FilesInDB
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        If UCase(dt.Rows(i)("sad_Config_Value")) = "TRUE" Then
                            ddlFilesDB.SelectedIndex = 0
                            txtFileInDBPath.Enabled = False
                        ElseIf UCase(dt.Rows(i)("sad_Config_Value")) = "FALSE" Then
                            ddlFilesDB.SelectedIndex = 1
                            txtFileInDBPath.Enabled = True
                        End If
                    Else
                        ddlFilesDB.SelectedIndex = 0
                        txtFileInDBPath.Enabled = False
                    End If
                End If

                If dt.Rows(i)("sad_Config_Key") = "HTP" Then    'HTP
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        txtHTP.Text = objGen.ReplaceSafeSQL(dt.Rows(i)("sad_Config_Value"))
                    End If
                End If

                If dt.Rows(i)("sad_Config_Key") = "FtpServer" Then    'FtpServer
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        txtFTPServer.Text = objGen.ReplaceSafeSQL(dt.Rows(i)("sad_Config_Value"))
                    End If
                End If


                If dt.Rows(i)("sad_Config_Key") = "Currency" Then    'Currency
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        ddlCurrency.SelectedValue = dt.Rows(i)("sad_Config_Value")
                    End If
                End If

                If dt.Rows(i)("sad_Config_Key") = "ErrorLog" Then    'ErrorLog
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        txtErrorLog.Text = objGen.ReplaceSafeSQL(dt.Rows(i)("sad_Config_Value"))
                    End If
                End If

                If dt.Rows(i)("sad_Config_Key") = "DateFormat" Then    'Date Format
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        ddlDateFormat.SelectedValue = dt.Rows(i)("sad_Config_Value")
                    End If
                End If
                If dt.Rows(i)("sad_Config_Key") = "FileSize" Then    'File Size
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        ddlFileSize.SelectedValue = dt.Rows(i)("sad_Config_Value")
                    End If
                End If

                'If dt.Rows(i)("sad_Config_Key") = "TimeOut" Then    'Session Time Out
                '    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                '        ddlSessionTimeOut.SelectedValue = dt.Rows(i)("sad_Config_Value")
                '    End If
                'End If

                If dt.Rows(i)("sad_Config_Key") = "TimeOutWarning" Then    'Session Time Out Warning
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        ddlSessionTimeOutWarning.SelectedValue = dt.Rows(i)("sad_Config_Value")
                    End If
                End If

                If dt.Rows(i)("sad_Config_Key") = "FileInDBPath" Then    'FilesInDBPath
                    If IsDBNull(dt.Rows(i)("sad_Config_Value")) = False Then
                        txtFileInDBPath.Text = objGen.ReplaceSafeSQL(dt.Rows(i)("sad_Config_Value"))
                    End If
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetAppSettings")
        End Try
    End Sub
    Public Sub GetPasswordDetails()
        Dim DVPSWDDetails As New DataView(dtPswd)
        Dim sSplitAry() As String
        Try
            If dtPswd.Rows.Count > 0 Then
                txtMinPassword.Text = DVPSWDDetails(0)("MPS_MinimumChar")
                txtMaxPassword.Text = DVPSWDDetails(0)("MPS_MaximumChar")
                txtRecoveryAttempt.Text = DVPSWDDetails(0)("MSP_RecoveryAttempts")
                txtUnsuccessAttempts.Text = DVPSWDDetails(0)("MPS_UnSuccessfulAttempts")
                txtExpiryDay.Text = DVPSWDDetails(0)("MPS_PasswordExpiryDays")
                txtNumberofLogin.Text = DVPSWDDetails(0)("MPS_NotLoginDays")
                txtExpiryAlertDay.Text = DVPSWDDetails(0)("MPS_PasswordExpiryAlertDays")
                For j = 0 To chkPasswordContain.Items.Count - 1
                    sSplitAry = DVPSWDDetails(0)("MPS_Password_Contains").Split(",")
                    For iIndxAry = 0 To sSplitAry.Length - 1
                        If (chkPasswordContain.Items.Item(j).Value = sSplitAry(iIndxAry)) Then
                            chkPasswordContain.Items(j).Selected = True
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetPasswordDetails")
        End Try
    End Sub
    Public Sub GetEmailSettings()
        Dim DVESDetails As New DataView(dtES)
        Dim dt As New DataTable
        Try
            If dtES.Rows.Count > 0 Then
                txtSMTP.Text = DVESDetails(0)("conf_IPAddress")
                txtPort.Text = DVESDetails(0)("Conf_port")
                txtSenderEmail.Text = DVESDetails(0)("Conf_From")
                txtSMS.Text = DVESDetails(0)("Conf_SenderID")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetEmailSettings")
        End Try
    End Sub

    Private Sub ddlFilesDB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilesDB.SelectedIndexChanged
        Try
            If ddlFilesDB.SelectedIndex = 0 Then
                txtFileInDBPath.Enabled = False
            Else
                txtFileInDBPath.Enabled = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFilesDB")
        End Try
    End Sub
    Private Sub LoadFixedAssetSetting()
        Try
            ddlMethod.SelectedValue = objDepComp.LoadFixedAsesetSetting(sSession.AccessCode, sSession.AccessCodeID)
            If ddlMethod.SelectedValue > 0 Then
                ddlMethod.SelectedValue = ("AS_DepMethod")
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
