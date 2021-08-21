Imports System
Imports System.IO
Imports System.Net
Imports System.Web
Imports System.Diagnostics
Imports System.Net.Dns
Imports System.Security.Cryptography
Imports BusinesLayer
Imports System.Data
Partial Class LoginPage
    Inherits System.Web.UI.Page
    Private sFormName As String = "LoginPage"
    Private objErrorClass As New Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objEdictGen As New clsFASGeneral
    Private objLogin As New clsLogin
    Private objclsCPFP As New clsCPFP
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnLoginLog.ImageUrl = "Images/login_logofas11.png"
        imgbtnLogin.ImageUrl = "Images/Login_Button.png"
        Me.Form.DefaultButton = Me.imgbtnLogin.UniqueID
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try

            If IsPostBack = False Then
                btnOKtoCP.Attributes.Add("OnClick", "$('#ModalValidation').modal('hide');$('#ModalChangePassword').modal('show');")
                btnPEAYes.Attributes.Add("OnClick", "$('#ModalValidation').modal('hide');$('#ModalChangePassword').modal('show');")
                Try
                    If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                        Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                        Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
                    End If
                    Session.Clear() : Session.Abandon() : Session.RemoveAll()

                    Dim AcID As String = ""
                    AcID = Request.QueryString("AccID")
                    If AcID <> "" Then
                        AcID = objEdictGen.DecryptQueryString(Request.QueryString("AccID"))
                        txtAccessCode.Text = AcID
                    End If

                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim sConStr() As String, sPassword As String
        Dim iUserID As Integer, iAccessCodeID As Integer
        Dim sUserName As String, sIPAddress As String
        Try
            lblError.Text = ""
            sUserName = objEdictGen.SafeSQL(txtUserName.Text.Trim)
            sPassword = objEdictGen.SafeSQL(txtActualPassword.Value)
            sIPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            sConStr = System.Configuration.ConfigurationManager.AppSettings.GetValues(txtAccessCode.Text)
            If IsNothing(sConStr) = True Then
                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If
            If InStr(txtUserName.Text, "'", CompareMethod.Text) <> 0 Then
                txtUserName.Focus()
                lblValidationMsg.Text = "Enter valid Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Exit Sub
            ElseIf InStr(txtActualPassword.Value, "'", CompareMethod.Text) <> 0 Then
                txtPassword.Focus()
                lblValidationMsg.Text = "Enter valid Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtPassword').focus();", True)
                Exit Sub
            End If

            iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(txtAccessCode.Text)
            iUserID = objclsGeneralFunctions.GetUserIDFromLoginName(txtAccessCode.Text, iAccessCodeID, sUserName)

            If iUserID = 0 Then
                txtUserName.Text = String.Empty : txtPassword.Text = String.Empty
                objclsGeneralFunctions.SaveUserLogOperations(txtAccessCode.Text, iAccessCodeID, 0, sUserName, "Invalid login name.", sIPAddress, sPassword)
                txtUserName.Focus()
                lblValidationMsg.Text = "Invalid Login Name/Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Exit Sub
            Else
                EnterLogin(txtAccessCode.Text, iAccessCodeID, sUserName, iUserID, sPassword, sIPAddress)
            End If

        Catch ex As Exception
            If ex.Message.ToString.Contains("requested by the login. The login failed.") = True Then
                lblValidationMsg.Text = "Invalid database. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            ElseIf ex.Message.ToString.Contains("SQL Server does not exist or access denied.") = True Then
                lblValidationMsg.Text = "Invalid SQL server name. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            ElseIf ex.Message.ToString.Contains("Login failed for user") = True Then
                lblValidationMsg.Text = "Invalid SQL login/password. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            Else
                lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLogin_Click")
        End Try
    End Sub
    Private Sub EnterLogin(ByVal sAccessCode As String, ByVal iAccessCodeID As Integer, ByVal sUserName As String, ByVal iUserID As Integer, ByVal sPassword As String, ByVal sIPAddress As String)
        Dim iMinPassword As Integer, iMaxPassword As Integer
        Try
            Using rijAlg As New RijndaelManaged()
                rijAlg.Mode = CipherMode.CBC
                rijAlg.Padding = PaddingMode.PKCS7
                rijAlg.FeedbackSize = 128
                rijAlg.Key = Encoding.UTF8.GetBytes("8080808080808080")
                rijAlg.IV = Encoding.UTF8.GetBytes("8080808080808080")
                Dim decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV)
                Using msDecrypt As New MemoryStream(Convert.FromBase64String(sPassword))
                    Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                        Using srDecrypt As New StreamReader(csDecrypt)
                            sPassword = srDecrypt.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using
            sPassword = objEdictGen.EncryptPassword(sPassword)
            txtCurrentPasssword.Text = "" : txtConfirmPassword.Text = "" : txtNewPassword.Text = ""
            iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sAccessCode, iAccessCodeID, "Min")
            iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sAccessCode, iAccessCodeID, "Max")
            RegExpNewPwd.ValidationExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{" & iMinPassword & "," & iMaxPassword & "}"
            lblCONote.Text = "Password must contain minimum " & iMinPassword & " characters, maximum " & iMaxPassword & " characters, atleast 1 uppercase alphabet, 1 lowercase alphabet, 1 number, 1 special character."
            CVCurrentPasssword.ValueToCompare = objEdictGen.DecryptPassword(sPassword)
            sSession.AccessCode = sAccessCode
            sSession.AccessCodeID = iAccessCodeID
            sSession.EncryptPassword = sPassword
            sSession.YearID = 0
            sSession.IPAddress = sIPAddress
            sSession.UserID = iUserID
            sSession.UserLoginName = sUserName
            sSession.UserFullName = objclsGeneralFunctions.GetUserFullNameFromUserID(sAccessCode, sSession.AccessCodeID, iUserID)
            sSession.UserFullNameCode = objclsGeneralFunctions.GetUserNameAndCodeFromPKID(sAccessCode, iAccessCodeID, iUserID)
            'sSession.LastLoginDate = objLogin.GetLastLoginDate(sAccessCode, iAccessCodeID, iUserID)
            sSession.MaxPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")
            sSession.MinPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
            sSession.NoOfUnSucsfAtteptts = 0 'objclsLogin.GetNoOfUnSuccssfulAttempts(sAccessCode, iAccessCodeID, iUserID)
            sSession.FileSize = objclsGeneralFunctions.GetFASSettingValue(sAccessCode, iAccessCodeID, "FileSize")
            sSession.TimeOut = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, "TimeOut") * 60000
            sSession.TimeOutWarning = objclsGeneralFunctions.GetEDICTSettingValue(sAccessCode, "TimeOutWarning") * 60000
            sSession.Menu = "HOME"
            sSession.ScreenWidth = Val(txtScreenWidth.Value)
            sSession.ScreenHeight = Val(txtScreenHeight.Value)
            sSession.StartDate = "01/01/1900"
            sSession.EndDate = "01/01/1900"
            sSession.DefaultBranch = 0
            Session("AllSession") = sSession

            Dim sUserPwd As String = "" : Dim sPwd As String = ""
            sUserPwd = objclsGeneralFunctions.GetUserPwd(sAccessCode, sSession.AccessCodeID, iUserID)
            sPwd = objEdictGen.DecryptPassword(sUserPwd)
            If CVCurrentPasssword.ValueToCompare <> sPwd Then
                txtPassword.Text = String.Empty
                objclsGeneralFunctions.SaveUserLogOperations(sAccessCode, sSession.AccessCodeID, sSession.UserID, sUserName, "Invalid Password.", sIPAddress, sPassword)
                txtPassword.Focus()
                lblValidationMsg.Text = "Invalid Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtPassword').focus();", True)
                Exit Sub
            End If
            Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
            objclsGeneralFunctions.SaveUserLogOperations(txtAccessCode.Text, iAccessCodeID, sSession.UserID, sUserName, "Logged in Successfully.", sIPAddress, sPassword)
            'Else
            '    txtUserName.Text = String.Empty : txtPassword.Text = String.Empty
            '    txtUserName.Focus()
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Function BuildSearchCriteriaDT() As DataTable
        Dim dc As DataColumn
        Dim LocalDt As DataTable
        Try
            LocalDt = New DataTable
            dc = New DataColumn("CritId", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("CritName", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("CritSelName", GetType(String))
            LocalDt.Columns.Add(dc)
            dc = New DataColumn("CritSelID", GetType(String))
            LocalDt.Columns.Add(dc)
            Return LocalDt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Function
    Private Function BuildCritDtForGrid(ByVal dt As DataTable) As DataTable
        Dim Arr(10) As String
        Try
            Arr(0) = "FilePlan"
            Arr(1) = "SubCabinets"
            Arr(2) = "Folders"
            Arr(3) = "Date"
            Arr(4) = "Title"
            Arr(5) = "Keywords"
            Arr(6) = "OCRText"
            Arr(7) = "Format"
            Arr(8) = "Created by"
            Arr(9) = "Any Descriptor"
            Arr(10) = "DocumentTypes"
            dt = BuildRows(dt, Arr)
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Function

    Private Function BuildRows(ByVal dt As DataTable, ByVal Arr() As String) As DataTable
        Dim dr As DataRow
        Dim i As Int16
        Try
            dt.Clear()
            For i = 0 To UBound(Arr)
                dr = dt.NewRow
                Select Case UCase(Arr(i))
                    Case "FILEPLAN"
                        dr(0) = "CB"
                    Case "SUBCABINETS"
                        dr(0) = "SC"
                    Case "FOLDERS"
                        dr(0) = "FD"
                    Case "DOCUMENTTYPES"
                        dr(0) = "DT"
                    Case "DESCRIPTORS"
                        dr(0) = "DC"
                    Case "KEYWORDS"
                        dr(0) = "KW"
                    Case "DATE"
                        dr(0) = "DE"
                    Case "TITLE"
                        dr(0) = "TT"
                    Case "OCRTEXT"
                        dr(0) = "OC"
                    Case "ANY DESCRIPTOR"
                        dr(0) = "AD"
                    Case "FORMAT"
                        dr(0) = "FT"
                    Case "CREATED BY"
                        dr(0) = "CR"
                End Select
                dr(1) = Arr(i)
                dt.Rows.Add(dr)
            Next
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Function
    Protected Sub btnPEAYes_Click(sender As Object, e As EventArgs)
        'Try
        '    lblError.Text = ""
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalValidation').modal('hide');$('#ModalChangePassword').modal('show');", True)
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    ' Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPEANo_Click")
        'End Try
    End Sub
    Protected Sub btnPEANo_Click(sender As Object, e As EventArgs)
        'Try
        '    lblError.Text = ""
        '    Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    ' Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPEANo_Click")
        'End Try
    End Sub
    Protected Sub btnCPCancel_Click(sender As Object, e As EventArgs)
        Try
            txtCurrentPasssword.Text = "" : txtNewPassword.Text = "" : txtConfirmPassword.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');", True)
        Catch ex As Exception
            '  Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCPCancel_Click")
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub btnCPUpdate_Click(sender As Object, e As EventArgs)
        'Dim iMinPassword As Integer, iMaxPassword As Integer
        'Try
        '    lblError.Text = ""
        '    If txtNewPassword.Text.Equals(txtConfirmPassword.Text) Then
        '        If (objclsFASGeneral.DecryptPassword(sSession.EncryptPassword) <> txtCurrentPasssword.Text) Then
        '            txtCurrentPasssword.Focus()
        '            lblValidationMsg.Text = "Invalid Old Passsword."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
        '        End If

        '        iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
        '        iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")

        '        If iMinPassword > txtNewPassword.Text.Length Then
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Password must have at least " & iMinPassword & " characters."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        If iMaxPassword <txtNewPassword.Text.Length Then
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Password is less than " & iMaxPassword & " characters."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        If objclsCPFP.checkForPasswordAlreadyExit(sSession.AccessCode, sSession.AccessCodeID, objclsFASGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID) = False Then ' txtNewPwd Replaced with sPwd
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Enter New Passsword, different than your previous 5 passwords."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        objclsCPFP.SaveOldPwdHistory(sSession.AccessCode, sSession.AccessCodeID, objclsFASGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID)
        '        objclsLogin.UpdateLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress)
        '        objclsCPFP.UpdatedPasswordDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, objclsFASGeneral.EncryptPassword(txtNewPassword.Text), sSession.IPAddress)
        '        objclsGeneralFunctions.SaveUserLogOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, "Password Changed.", sSession.IPAddress, objclsFASGeneral.EncryptPassword(txtNewPassword.Text))
        '        lblValidationMsg.Text = "Password Successfully Changed."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        '    Else
        '        txtCurrentPasssword.Focus()
        '        lblValidationMsg.Text = "Invalid Old Passsword."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    'Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdateChagePwd_Click")
        'End Try
    End Sub
    Protected Sub btnGetPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetPassword.Click
        Dim iBret As Integer, iUserID As Integer
        Dim sIPAddress As String, sPassWord As String, sAccessCode As String, iAccessCodeID As Integer
        Try
            lblError.Text = "" : lblPWD.Text = ""
            sIPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            sAccessCode = objEdictGen.SafeSQL(txtAccessCode.Text.Trim)
            iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode)
            If iAccessCodeID = 0 Then

                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If
            iUserID = objclsGeneralFunctions.GetUserIDFromLoginName(sAccessCode, iAccessCodeID, objEdictGen.SafeSQL(lblFPLogin.Text.Trim))
            iBret = objclsCPFP.CheckAnswer(sAccessCode, iAccessCodeID, objEdictGen.EncryptPassword(objEdictGen.SafeSQL(txtAnswer.Text)), iUserID)
            If iBret = 0 Then
                lblPWD.Text = "" : txtAnswer.Text = "" : txtAnswer.Focus()
                lblValidationMsg.Text = "Invalid Answer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAnswer').focus();", True)
                Exit Sub
            ElseIf iBret = 1 Then
                sPassWord = objclsCPFP.GetQuestionPassWordStatus(sAccessCode, iAccessCodeID, iUserID, "PassWord")
                lblPWD.Text = objEdictGen.DecryptPassword(sPassWord) '
                objclsCPFP.UpdateLogin(sAccessCode, iAccessCodeID, iUserID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalForgotPassword').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            ' Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGetPassword_Click")
        End Try
    End Sub
    Protected Sub lnkbtnForgotPassword_Click(sender As Object, e As EventArgs)
        Dim sConStr() As String, sStatus As String
        Dim iUserID As Integer, iAccessCodeID As Integer
        Dim sAccessCode As String, sUserName As String, sIPAddress As String, sAnswer As String
        Try
            lblError.Text = "" : lblPWD.Text = "" : txtAnswer.Text = ""
            sAccessCode = objEdictGen.SafeSQL(txtAccessCode.Text.Trim)
            sUserName = objEdictGen.SafeSQL(txtUserName.Text.Trim)
            sIPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            sConStr = System.Configuration.ConfigurationManager.AppSettings.GetValues(sAccessCode)
            If IsNothing(sConStr) = True Then
                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If
            If InStr(txtUserName.Text, "'", CompareMethod.Text) <> 0 Then
                txtUserName.Focus()
                lblValidationMsg.Text = "Enter valid Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Exit Sub
            End If

            iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode)
            If iAccessCodeID = 0 Then
                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If

            iUserID = objclsGeneralFunctions.GetUserIDFromLoginName(sAccessCode, iAccessCodeID, sUserName)
            If iUserID = 0 Then
                txtUserName.Text = String.Empty : txtPassword.Text = String.Empty
                txtUserName.Focus()
                lblValidationMsg.Text = "Invalid Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Exit Sub
            Else
                'Check UserID(LoginID) is Approved or not.
                If objLogin.CheckUserApprovedOrNot(sAccessCode, iAccessCodeID, sUserName) = True Then
                    lblValidationMsg.Text = "Your Account not yet Approved. Please contact system admin."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                    Exit Sub
                Else
                    sStatus = objclsCPFP.GetQuestionPassWordStatus(sAccessCode, iAccessCodeID, iUserID, "UserStatus")
                    If sStatus = "D" Then
                        lblValidationMsg.Text = "Account De-Activated. Please contact system admin."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                        Exit Sub
                    ElseIf sStatus = "B" Then
                        lblValidationMsg.Text = "Account Blocked. Please contact system admin."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                        Exit Sub
                    ElseIf sStatus = "L" Then
                        lblValidationMsg.Text = "Account Locked. Please contact system admin."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                        Exit Sub
                    Else
                        lblFPLogin.Text = "" : lblPWD.Text = "" : lblQue.Text = "" : txtAnswer.Text = ""
                        lblQue.Text = objclsCPFP.GetQuestionPassWordStatus(sAccessCode, iAccessCodeID, iUserID, "Question")
                        If lblQue.Text = "" Then
                            lblValidationMsg.Text = "Security Questions not available. Please contact system admin."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
                            Exit Sub
                        End If
                        lblFPLogin.Text = sUserName
                        sAnswer = objclsCPFP.GetQuestionPassWordStatus(sAccessCode, iAccessCodeID, iUserID, "Answer")
                        CVAnswer.ValueToCompare = objEdictGen.DecryptPassword(sAnswer)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalForgotPassword').modal('show');$('#txtAnswer').focus();", True)
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.ToString.Contains("requested by the login. The login failed.") = True Then
                lblValidationMsg.Text = "Invalid database. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            ElseIf ex.Message.ToString.Contains("SQL Server does not exist or access denied.") = True Then
                lblValidationMsg.Text = "Invalid SQL server name. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            ElseIf ex.Message.ToString.Contains("Login failed for user") = True Then
                lblValidationMsg.Text = "Invalid SQL login/password. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            Else
                lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            End If
            ' Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnForgotPassword_Click")
        End Try
    End Sub

End Class
