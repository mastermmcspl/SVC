

Imports BusinesLayer
Imports System.Data
Imports System.IO
Partial Class CustomerMaster_Home
    Inherits System.Web.UI.MasterPage
    Private Shared sFormName As String = "Home Masterpage"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    ' Private objclsGeneralFunctions As New clsGeneralFunctions
    ' Private objclsGRACeGeneral As New clsEDICTGeneral
    Private objclsLogin As New clsLogin
    ' Private objclsCPFP As New clsCPFP
    Private Shared sSession As AllSession

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Dim intSessionTimeOut As Integer, intSessionTimeOutWarning As Integer
        Try
            'sSession = Session("AllSession")
            ''mainForm.Action = Request.RawUrl
            'intSessionTimeOut = sSession.TimeOut
            'intSessionTimeOutWarning = sSession.TimeOutWarning
            'lblTimeOutWarning.Text = "Your GRACe session will expire in " & (sSession.TimeOutWarning / 60000) & " mins! Please Save the data before the session expires."
            'bdyProgramMaster.Attributes.Add("onload", "javascript:return checkTime(" + intSessionTimeOut.ToString + "," + intSessionTimeOutWarning.ToString + ");")
            'lblUserName.Text = "Welcome" & " " & sSession.UserFullName

            ''If objclsLogin.GetUserIsLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, sSession.BrowserName) = False Then
            ''    Response.Redirect("~/ConcurrentLogin.aspx", False)
            ''    Exit Sub
            ''End If

            'RegExpNewPwd.ValidationExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{" & sSession.MinPasswordCharacter & "," & sSession.MaxPasswordCharacter & "}"
            'lblCONote.Text = "Password must contain minimum " & sSession.MinPasswordCharacter & " Characters, maximum " & sSession.MaxPasswordCharacter & " Characters, atleast 1 uppercase alphabet, 1 lowercase alphabet, 1 number, 1 special Character."
            ''CVCurrentPasssword.ValueToCompare = objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword)

            ''CVCheckPassword.ValueToCompare = objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword)

            'RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

            'RFVSecurityQuestion.ErrorMessage = "Enter Security Question." : REVSecurityQuestion.ValidationExpression = "^(.{0,250})$"
            'REVSecurityQuestion.ErrorMessage = "Security Question exceeded maximum size(max 250 Character)."

            'RFVAnswer.ErrorMessage = "Enter Answer." : REVAnswer.ValidationExpression = "^(.{0,250})$"
            'REVAnswer.ErrorMessage = "Answer exceeded maximum size(max 250 Character)."

            'lnkbtnMyProfile.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('show');$('#txtCheckPassword').focus();return false;")
            'lnkbtnChangePassword.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('show');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');return false;")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub btnCheckPwd_Click(sender As Object, e As EventArgs)
        Dim bFlag As Boolean
        Try
            'bFlag = objclsCPFP.CheckUserPWD(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, objclsGRACeGeneral.EncryptPassword(txtCheckPassword.Text))
            'If bFlag = True Then
            '    LoadUserProfile()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('show');$('#ModalPassword').modal('hide');", True)
            'Else
            '    lblValidationMsg.Text = "Invalid Passsword."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
            'End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCheckPwd_Click")
        End Try
    End Sub
    Protected Sub btnCheckCancel_Click(sender As Object, e As EventArgs)
        Try
            txtCheckPassword.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCheckCancel_Click")
        End Try
    End Sub
    Protected Sub btnCPCancel_Click(sender As Object, e As EventArgs)
        Try
            txtCurrentPasssword.Text = "" : txtNewPassword.Text = "" : txtConfirmPassword.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCPCancel_Click")
        End Try
    End Sub
    Protected Sub btnCPUpdate_Click(sender As Object, e As EventArgs)
        Dim iMinPassword As Integer, iMaxPassword As Integer
        'Try
        '    lblCPError.Text = "" : lblUPError.Text = ""
        '    If txtNewPassword.Text.Equals(txtConfirmPassword.Text) Then
        '        If (objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword) <> txtCurrentPasssword.Text) Then
        '            txtCurrentPasssword.Focus()
        '            lblValidationMsg.Text = "Invalid Old Passsword." : lblCPError.Text = "Invalid Old Passsword."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
        '            Exit Try
        '        End If

        '        iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
        '        iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")

        '        If iMinPassword > txtNewPassword.Text.Length Then
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Password must have at least " & iMinPassword & " Characters." : lblCPError.Text = "Password must have at least " & iMinPassword & " Characters."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        If iMaxPassword < txtNewPassword.Text.Length Then
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Password is less than " & iMaxPassword & " Characters." : lblCPError.Text = "Password is less than " & iMaxPassword & " Characters."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        If objclsCPFP.checkForPasswordAlreadyExit(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID) = False Then ' txtNewPwd Replaced with sPwd
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Enter New Password, different than your previous 5 passwords." : lblCPError.Text = "Enter New Password, different than your previous 5 passwords."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        objclsCPFP.SaveOldPwdHistory(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID)
        '        'objclsLogin.UpdateLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress)
        '        objclsCPFP.UpdatedPasswordDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.IPAddress)
        '        objclsGeneralFunctions.SaveUserLogOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, "Password Changed.", sSession.IPAddress, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text))
        '        objclsGeneralFunctions.SaveEDICTFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Change Password", "Password Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
        '        lblValidationMsg.Text = "Password Successfully Changed." : lblCPError.Text = "Password Successfully Changed."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        '    Else
        '        txtCurrentPasssword.Focus()
        '        lblValidationMsg.Text = "Invalid Old Passsword." : lblCPError.Text = "Invalid Old Passsword."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
        '    End If
        'Catch ex As Exception
        '    lblCPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdateChagePwd_Click")
        'End Try
    End Sub
    Public Sub LoadUserProfile()
        'Dim j As Integer, iParentID = 0, iDept = 0
        'Dim dtDepartment As New DataTable
        'Dim lblDeptID As New Label
        'Dim chkDept As New CheckBox, chkDeptHead As New CheckBox
        'Try
        '    objstrUserDetails = objclsCPFP.LoadUserprofile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
        '    iParentID = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.iUsr_GovernmentID)
        '    iDept = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.iUsr_DepartmentID)
        '    txtCountry.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_Country)
        '    txtGovernment.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_Government)
        '    txtDepartment.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_Department)
        '    txtSection.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_Section)
        '    txtLoginName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_LoginName)
        '    txtEmpName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_fullName)
        '    txtMail.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_Email)
        '    txtDesignation.Text = objstrUserDetails.sUsr_Designation
        '    txtUserType.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_UserType)
        '    txtMemberType.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_MemberType)
        '    txtSecurityQuestion.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_SecurityQuestion)
        '    If objstrUserDetails.sUsr_Answer <> "" Then
        '        txtAnswer.Attributes.Add("value", objclsGRACeGeneral.DecryptPassword(objstrUserDetails.sUsr_Answer))
        '    End If
        '    If iParentID > 0 Then
        '        dgDept.DataSource = objclsUsers.LoadUserOtherDeptDetails(sSession.AccessCode, sSession.AccessCodeID, iParentID, iDept)
        '        dgDept.DataBind()
        '    End If
        '    For j = 0 To dgDept.Items.Count - 1
        '        chkDept = dgDept.Items(j).FindControl("chkDept")
        '        chkDeptHead = dgDept.Items(j).FindControl("chkDeptHead")
        '        chkDept.Checked = False : chkDeptHead.Checked = False
        '    Next
        '    dtDepartment = objclsUsers.LoadUserInOtherDeptDetails(sSession.AccessCode, sSession.UserID)
        '    For i = 0 To dtDepartment.Rows.Count - 1
        '        For j = 0 To dgDept.Items.Count - 1
        '            chkDept = dgDept.Items(j).FindControl("chkDept")
        '            chkDeptHead = dgDept.Items(j).FindControl("chkDeptHead")
        '            lblDeptID = dgDept.Items(j).FindControl("lblDeptID")
        '            If Val(lblDeptID.Text) = dtDepartment.Rows(i)("SUO_DeptID").ToString() Then
        '                chkDept.Checked = True
        '                If dtDepartment.Rows(i)("SUO_IsDeptHead").ToString() = "1" Then
        '                    chkDeptHead.Checked = True
        '                Else
        '                    chkDeptHead.Checked = False
        '                End If
        '            End If
        '        Next
        '    Next
        'Catch ex As Exception
        '    Throw
        'End Try
    End Sub
    Protected Sub btnUpdateUserProfile_Click(sender As Object, e As EventArgs)
        'Dim sQual As String = "", sSecurityAnswer As String
        'Try
        '    lblCPError.Text = "" : lblUPError.Text = ""
        '    If txtMail.Text.Trim = "" Then
        '        txtMail.Focus()
        '        lblUPError.Text = "Enter E-Mail."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myProfileModal').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtSecurityQuestion.Text.Trim = "" Then
        '        txtSecurityQuestion.Focus()
        '        lblUPError.Text = "Enter Security Question."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myProfileModal').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtSecurityQuestion.Text.Trim.Length > 250 Then
        '        txtSecurityQuestion.Focus()
        '        lblUPError.Text = "Security Question exceeded maximum size(max 250 Characters)."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myProfileModal').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtAnswer.Text.Trim = "" Then
        '        txtAnswer.Focus()
        '        lblUPError.Text = "Enter Answer."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myProfileModal').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtAnswer.Text.Trim.Length > 250 Then
        '        txtAnswer.Focus()
        '        lblUPError.Text = "Answer exceeded maximum size(max 250 Characters)."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myProfileModal').modal('show');", True)
        '        Exit Sub
        '    End If
        '    objstrUserDetails = objclsCPFP.LoadUserprofile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
        '    txtLoginName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objstrUserDetails.sUsr_LoginName)
        '    sSecurityAnswer = objclsGRACeGeneral.EncryptPassword(Trim(txtAnswer.Text))
        '    objclsCPFP.UpdateUserProfile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objclsGRACeGeneral.SafeSQL(txtEmpName.Text), objclsGRACeGeneral.SafeSQL(txtSecurityQuestion.Text), sSecurityAnswer, objclsGRACeGeneral.SafeSQL(txtMail.Text), sSession.IPAddress)
        '    objclsGeneralFunctions.SaveEDICTFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "User Profile", "Profile Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
        '    LoadUserProfile()
        '    lblValidationMsg.Text = "Successfully Updated." : lblUPError.Text = "Successfully Updated."
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myProfileModal').modal('show');", True)
        'Catch ex As Exception
        '    lblUPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateUserProfile_Click")
        'End Try
    End Sub
    'Protected Sub lnkbtnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLogout.Click
    '    Try
    '        If (sSession.UserID) <> 0 Then
    '            'objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.UserID)
    '        End If
    '        If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
    '            Response.Cookies("ASP.NET_SessionId").Value = String.Empty
    '            Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
    '        End If

    '        If IsNothing(Request.Cookies("AuthToken")) = False Then
    '            Response.Cookies("AuthToken").Value = String.Empty
    '            Response.Cookies("AuthToken").Expires = DateTime.Now.AddMonths(-20)
    '        End If


    '        Session.Clear() : Session.Abandon() : Session.RemoveAll()
    '        Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLogout_Click")
    '    End Try
    'End Sub
    Protected Sub btnLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If (sSession.UserID) <> 0 Then
                'objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.UserID)
            End If
            If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
            End If
            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLogOut_Click")
        End Try
    End Sub
    'Protected Sub lnkbtnHOME_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnHOME.Click
    '    Try
    '        sSession.Menu = "HOME" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/HomePages/HomePage.aspx", False) 'HomePages/Home
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHOME_Click")
    '    End Try
    'End Sub
    'Protected Sub lnkbtnAdministration_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAdministration.Click
    '    Try
    '        sSession.Menu = "Administration" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/HomePages/MasterPage.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAdministration_Click")
    '    End Try
    'End Sub
    'Protected Sub lnkbtnDigitalFiling_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDigitalFiling.Click
    '    Try
    '        sSession.Menu = "DigitalFiling" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/HomePages/DigitalFilingPage.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDigitalFiling_Click")
    '    End Try
    'End Sub
    'Protected Sub lnkbtnScanning_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnScanning.Click
    '    Try
    '        sSession.Menu = "Search" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/HomePages/ScanningPage.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScanning_Click")
    '    End Try
    'End Sub
    'Protected Sub lnkbtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSearch.Click
    '    Try
    '        sSession.Menu = "Search" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Search/Search.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSearch_Click")
    '    End Try
    'End Sub
    'Protected Sub lnkbtnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnView.Click
    '    Try
    '        sSession.Menu = "View" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Search/View.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnView_Click")
    '    End Try
    'End Sub

    'Protected Sub lnkbtnWorkFlow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnWorkflow.Click
    '    Try
    '        sSession.Menu = "View" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Workflow/InwardDashboard.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnWorkFlow_Click")
    '    End Try
    'End Sub
    'Private Sub GetSubMenuOpen()
    '    Try
    '        liCustomerRegistartion.Attributes.Remove("class")
    '        lnkbtnServer.Attributes.Remove("class")
    '        lnkbtnCustomer.Font.Italic = False : lnkbtnCustomer.Font.Bold = False

    '        If sSession.SubMenu = "Server" Then
    '            lnkbtnServer.Attributes.Add("class", "open")
    '            If sSession.Form = "Server" Then
    '                lnkbtnServer.Font.Italic = True : lnkbtnServer.Font.Bold = True
    '            End If
    '        ElseIf sSession.SubMenu = "Customer" Then
    '            liCustomerRegistartion.Attributes.Add("class", "open")
    '            If sSession.Form = "Customer" Then
    '                lnkbtnCustomer.Font.Italic = True : lnkbtnCustomer.Font.Bold = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Private Sub GetClickedURL(ByVal sForm As String)
        Try
            If sForm = "CustomerMaster" Then
                sSession.SubMenu = "CustomerMaster" : sSession.Form = "CustomerMaster"
                Response.Redirect("~/CustomerMaster/CustomerMaster.aspx", False)
            ElseIf sForm = "ServerMaster" Then
                sSession.SubMenu = "ServerMaster" : sSession.Form = "ServerMaster"
                Response.Redirect("~/CustomerMaster/ServerDetails.aspx", False)
            End If
            Session("AllSession") = sSession
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lnkbtnCustomerMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnCustomerMaster.Click
        Try
            GetClickedURL("CustomerMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerMaster.Click")
        End Try
    End Sub
    Private Sub lnkbtnServerMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnServerMaster.Click
        Try
            GetClickedURL("ServerMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnServerMaster.Click")
        End Try
    End Sub
    Private Sub lnkbtnLogout_Click(sender As Object, e As EventArgs) Handles lnkbtnLogout.Click
        Try
            If (sSession.UserID) <> 0 Then

            End If
            If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
            End If
            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Login.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLogOut_Click")
        End Try
    End Sub
    'Private Sub lnkbtnCustomer_Click(sender As Object, e As EventArgs) Handles lnkbtnCustomer.Click
    '    Try
    '        GetClickedURL("Customer")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomer_Click")
    '    End Try
    'End Sub

End Class

