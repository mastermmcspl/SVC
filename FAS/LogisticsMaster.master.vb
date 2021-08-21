Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Partial Class LogisticsMaster
    Inherits System.Web.UI.MasterPage
    Private Shared sFormName As String = "Master Masterpage"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASGeneral As New clsFASGeneral
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnTRACeLog.ImageUrl = "Images/logo_CAFE.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim intSessionTimeOut As Integer, intSessionTimeOutWarning As Integer
        Try
            sSession = Session("AllSession")
            intSessionTimeOut = sSession.TimeOut
            intSessionTimeOutWarning = sSession.TimeOutWarning
            lblTimeOutWarning.Text = "Your FAS session will expire in " & (sSession.TimeOutWarning / 60000) & " mins! Please Save the data before the session expires."
            bdyProgramMaster.Attributes.Add("onload", "javascript:return checkTime(" + intSessionTimeOut.ToString + "," + intSessionTimeOutWarning.ToString + ");")
            lblUserName.Text = "Welcome" & " " & sSession.UserFullNameCode
            sSession.YearID = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
            sSession.YearName = objclsGeneralFunctions.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            If sSession.YearID > 0 Then
                lblFinancialYear.Text = sSession.YearName
            Else
                lblFinancialYear.Text = ""
            End If
            sSession.StartDate = objclsFASGeneral.FormatDtForRDBMS(objclsGeneralFunctions.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
            sSession.EndDate = objclsFASGeneral.FormatDtForRDBMS(objclsGeneralFunctions.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")

            RegExpNewPwd.ValidationExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{" & sSession.MinPasswordCharacter & "," & sSession.MaxPasswordCharacter & "}"
            lblCONote.Text = "Password must contain minimum " & sSession.MinPasswordCharacter & " characters, maximum " & sSession.MaxPasswordCharacter & " characters, atleast 1 uppercase alphabet, 1 lowercase alphabet, 1 number, 1 special character."
            CVCurrentPasssword.ValueToCompare = objclsFASGeneral.DecryptPassword(sSession.EncryptPassword)

            CVCheckPassword.ValueToCompare = objclsFASGeneral.DecryptPassword(sSession.EncryptPassword)

            REVMobNo.ErrorMessage = "Enter valid Mobile No." : REVMobNo.ValidationExpression = "^[0-9]{10}$"

            RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

            RFVSecurityQuestion.ErrorMessage = "Enter Security Question." : REVSecurityQuestion.ValidationExpression = "^(.{0,250})$"
            REVSecurityQuestion.ErrorMessage = "Security Question exceeded maximum size(max 250 character)."

            RFVAnswer.ErrorMessage = "Enter Answer." : REVAnswer.ValidationExpression = "^(.{0,250})$"
            REVAnswer.ErrorMessage = "Answer exceeded maximum size(max 250 character)."

            REVExperiencesummary.ValidationExpression = "^(.{0,8000})$" : REVExperiencesummary.ErrorMessage = "Experience Summary exceeded maximum size(max 8000 character)."

            REVOthers.ValidationExpression = "^(.{0,5000})$" : REVOthers.ErrorMessage = "Other qualification exceeded maximum size(max 5000 character)."

            lnkbtnMyProfile.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('show');$('#txtCheckPassword').focus();return false;")
            lnkbtnChangePassword.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('show');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');return false;")
            If sSession.Menu = "MASTER" Then
                GetSubMenuOpen()
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub lnkbtnInventory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnInventory.Click
        Try
            sSession.Menu = "Inventory" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Inventory.aspx", False) 'HomePages/Inventory
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInventory_Click")
        End Try
    End Sub
    'Private Sub GetSubMenuOpen()
    '    Try
    '        lnkbtnInventoryMaster.Font.Italic = False : lnkbtnInventoryMaster.Font.Bold = False
    '        lnkbtnInventoryMasterDetails.Font.Italic = False : lnkbtnInventoryMasterDetails.Font.Bold = False
    '        lnkPSupdate.Font.Italic = False : lnkPSupdate.Font.Bold = False
    '        lnkPReport.Font.Italic = False : lnkPReport.Font.Bold = False
    '        lnkStockAdjustment.Font.Italic = False : lnkStockAdjustment.Font.Bold = False
    '        lnkStockTranfer.Font.Italic = False : lnkStockTranfer.Font.Bold = False

    '        If sSession.SubMenu = "Inventory" Then
    '            liInventory.Attributes.Add("class", "open")
    '            If sSession.Form = "InventoryMaster" Then
    '                lnkbtnInventoryMaster.Font.Italic = True : lnkbtnInventoryMaster.Font.Bold = True
    '            ElseIf sSession.Form = "InventoryMasterDetails" Then
    '                lnkbtnInventoryMasterDetails.Font.Italic = True : lnkbtnInventoryMasterDetails.Font.Bold = True
    '            ElseIf sSession.Form = "PhysicalUpdate" Then
    '                lnkPSupdate.Font.Italic = True : lnkPSupdate.Font.Bold = True
    '            ElseIf sSession.Form = "PhysicalReport" Then
    '                lnkPReport.Font.Italic = True : lnkPReport.Font.Bold = True
    '            ElseIf sSession.Form = "StockAdjustment" Then
    '                lnkStockAdjustment.Font.Italic = True : lnkStockAdjustment.Font.Bold = True
    '            ElseIf sSession.Form = "StockTransfer" Then
    '                lnkStockTranfer.Font.Italic = True : lnkStockTranfer.Font.Bold = True
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub GetSubMenuOpen()
        Try
            lnkbtnTripDashboard.Font.Italic = False : lnkbtnTripDashboard.Font.Bold = False
            lnkbtnCustomerBilling.Font.Italic = False : lnkbtnCustomerBilling.Font.Bold = False

            If sSession.SubMenu = "Logistics" Then
                liLogistics.Attributes.Add("class", "open")
                If sSession.Form = "TripDashboard" Then
                    lnkbtnTripDashboard.Font.Italic = True : lnkbtnTripDashboard.Font.Bold = True
                ElseIf sSession.SubMenu = "TripDashboard" Then
                    liTripAccount.Attributes.Add("class", "open")
                    If sSession.Form = "CustomerBilling" Then
                        lnkbtnCustomerBilling.Font.Italic = True : lnkbtnCustomerBilling.Font.Bold = True
                    ElseIf sSession.Form = "DriverBilling" Then
                        lnkbtnDriverBilling.Font.Italic = True : lnkbtnDriverBilling.Font.Bold = True
                    ElseIf sSession.Form = "PumpBilling" Then
                        lnkbtnPumpBilling.Font.Italic = True : lnkbtnPumpBilling.Font.Bold = True
                    End If
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetClickedURL(ByVal sForm As String)

        Try
            If sForm = "TripDashboard" Then
                sSession.SubMenu = "TripDashboard" : sSession.Form = "TripDashboard"
                Response.Redirect("~/Logistics/FrmLgstTripGenDashboard.aspx", False)
            ElseIf sForm = "CustomerBilling" Then
                sSession.SubMenu = "TripDashboard" : sSession.Form = "CustomerBilling"
                Response.Redirect("~/Logistics/FrmLgstCustBillingDashboard.aspx", False)
            ElseIf sForm = "DriverBilling" Then
                sSession.SubMenu = "TripDashboard" : sSession.Form = "DriverBilling"
                Response.Redirect("~/Logistics/FrmLgstDrivBillingDashboard.aspx", False)
            ElseIf sForm = "PumpBilling" Then
                sSession.SubMenu = "TripDashboard" : sSession.Form = "PumpBilling"
                Response.Redirect("~/Logistics/FrmLgstPumpBillDashboard.aspx", False)
            ElseIf sForm = "CustomerBillingReport" Then
                sSession.SubMenu = "CustomerBillingReport" : sSession.Form = "CustomerBillingReport"
                Response.Redirect("~/Logistics/FrmLgstCustomerBillingReport.aspx", False)
            ElseIf sForm = "PmpBillingReport" Then
                sSession.SubMenu = "PmpBillingReport" : sSession.Form = "PmpBillingReport"
                Response.Redirect("~/Logistics/FrmLgstPumpBillReport.aspx", False)
            ElseIf sForm = "DriverBillingReport" Then
                sSession.SubMenu = "DriverBillingReport" : sSession.Form = "DriverBillingReport"
                Response.Redirect("~/Logistics/FrmLgstDriverBillingReport.aspx", False)
            ElseIf sForm = "DynReport" Then
                sSession.SubMenu = "DynReport" : sSession.Form = "DynReport"
                Response.Redirect("~/Logistics/FrmLgstDynamicReports.aspx", False)
            End If
            Session("AllSession") = sSession

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub lnkbtnTripDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnTripDashboard.Click
        Try
            GetClickedURL("TripDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnTripDashboard_Click")
        End Try
    End Sub

    Protected Sub lnkbtnHOME_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnHOME.Click
        Try
            sSession.Menu = "HOME" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHOME_Click")
        End Try
    End Sub
    Protected Sub lnkbtnMASTERS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnMASTERS.Click
        Try
            sSession.Menu = "MASTER" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Master.aspx", False) 'HomePages/Master
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnMASTERS_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPurchase_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPurchase.Click
        Try
            sSession.Menu = "Purchase" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Purchase.aspx", False) 'HomePages/Purchase
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPurchase_Click")
        End Try
    End Sub

    Protected Sub lnkbtnSales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSales.Click
        Try
            sSession.Menu = "Sales" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Sales.aspx", False) 'HomePages/Sales
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSales_Click")
        End Try
    End Sub

    Protected Sub lnkbtnAccounts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAccounts.Click
        Try
            sSession.Menu = "Accounts" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Accounts.aspx", False) 'HomePages/Accounts
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAccounts_Click")
        End Try
    End Sub

    Protected Sub lnkbtnDigitalFilling_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDigitalFilling.Click
        Try
            sSession.Menu = "DigitalFilling" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            'Response.Redirect("~/HomePages/DigitalFilling.aspx", False)
            Response.Redirect("~/DigitalFilling/DigitalFilingDashboard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDigitalFilling_Click")
        End Try
    End Sub

    Private Sub lnkbtnSearch_Click(sender As Object, e As EventArgs) Handles lnkbtnSearch.Click
        Try
            sSession.Menu = "Search" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/Search/Search.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSearch_Click")
        End Try
    End Sub

    Protected Sub lnkbtnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLogout.Click
        Try
            If (sSession.UserID) <> 0 Then
                '  objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.UserID)
            End If
            If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
            End If
            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLogout_Click")
        End Try
    End Sub
    Protected Sub btnLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If (sSession.UserID) <> 0 Then
                ' objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.UserID)
            End If
            If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-20)
            End If
            If IsNothing(Request.Cookies("AuthToken")) = False Then
                Response.Cookies("AuthToken").Value = String.Empty
                Response.Cookies("AuthToken").Expires = DateTime.Now.AddMonths(-20)
            End If
            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLogOut_Click")
        End Try
    End Sub


    Private Sub lnkbtnRemoteData_Click(sender As Object, e As EventArgs) Handles lnkbtnRemoteData.Click
        Try
            sSession.Menu = "RemoteData" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/RemoteData.aspx", False) 'HomePages/RemoteData
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRemoteData_Click")
        End Try
    End Sub
    Private Sub lnkbtnFixedAsset_Click(sender As Object, e As EventArgs) Handles lnkbtnFixedAsset.Click
        Try
            sSession.Menu = "Fixed Asset" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/FixedAsset.aspx", False) 'HomePages/Accounts
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFixedAsset_Click")
        End Try
    End Sub
    Private Sub lnkbtnLogistics_Click(sender As Object, e As EventArgs) Handles lnkbtnLogistics.Click
        Try
            sSession.Menu = "Logistics" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/LogisticsMaster.aspx", False) 'HomePages/Accounts
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLogistics_Click")
        End Try
    End Sub
    Protected Sub btnCheckPwd_Click(sender As Object, e As EventArgs)
        'Dim bFlag As Boolean
        'Try
        '    bFlag = objclsCPFP.CheckUserPWD(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, objclsFASGeneral.EncryptPassword(txtCheckPassword.Text))
        '    If bFlag = True Then
        '        BindExperience() : BindQualification() : LoadUserProfile()
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('show');$('#ModalPassword').modal('hide');", True)
        '    Else
        '        lblValidationMsg.Text = "Invalid Passsword."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
        '    End If
        'Catch ex As Exception
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCheckPwd_Click")
        'End Try
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
        'Dim iMinPassword As Integer, iMaxPassword As Integer
        'Try
        '    lblCPError.Text = "" : lblUPError.Text = ""
        '    If txtNewPassword.Text.Equals(txtConfirmPassword.Text) Then
        '        If (objclsFASGeneral.DecryptPassword(sSession.EncryptPassword) <> txtCurrentPasssword.Text) Then
        '            txtCurrentPasssword.Focus()
        '            lblValidationMsg.Text = "Invalid Old Passsword." : lblCPError.Text = "Invalid Old Passsword."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
        '            Exit Try
        '        End If

        '        iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
        '        iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")

        '        If iMinPassword > txtNewPassword.Text.Length Then
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Password must have at least " & iMinPassword & " characters." : lblCPError.Text = "Password must have at least " & iMinPassword & " characters."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        If iMaxPassword < txtNewPassword.Text.Length Then
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Password is less than " & iMaxPassword & " characters." : lblCPError.Text = "Password is less than " & iMaxPassword & " characters."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        If objclsCPFP.checkForPasswordAlreadyExit(sSession.AccessCode, sSession.AccessCodeID, objclsFASGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID) = False Then ' txtNewPwd Replaced with sPwd
        '            txtNewPassword.Focus()
        '            lblValidationMsg.Text = "Enter New Password, different than your previous 5 passwords." : lblCPError.Text = "Enter New Password, different than your previous 5 passwords."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
        '            Exit Try
        '        End If

        '        objclsCPFP.SaveOldPwdHistory(sSession.AccessCode, sSession.AccessCodeID, objclsFASGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID)
        '        objclsLogin.UpdateLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress)
        '        objclsCPFP.UpdatedPasswordDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, objclsFASGeneral.EncryptPassword(txtNewPassword.Text), sSession.IPAddress)
        '        objclsGeneralFunctions.SaveUserLogOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, "Password Changed.", sSession.IPAddress, objclsFASGeneral.EncryptPassword(txtNewPassword.Text))
        '        objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Change Password", "Password Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
        '        lblValidationMsg.Text = "Password Successfully Changed." : lblCPError.Text = "Password Successfully Changed."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        '    Else
        '        txtCurrentPasssword.Focus()
        '        lblValidationMsg.Text = "Invalid Old Passsword." : lblCPError.Text = "Invalid Old Passsword."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
        '    End If
        'Catch ex As Exception
        '    ' lblCPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdateChagePwd_Click")
        'End Try
    End Sub
    Protected Sub btnUpdateUserProfile_Click(sender As Object, e As EventArgs)
        'Dim sQual As String = "", sSecurityAnswer As String
        'Try
        '    lblCPError.Text = "" : lblUPError.Text = ""
        '    If txtMobNo.Text.Trim <> "" Then
        '        If txtMobNo.Text.Trim.Length > 10 Then
        '            txtMobNo.Focus()
        '            lblValidationMsg.Text = "Mobile No. exceeded maximum size(max 10 numbers)." : lblUPError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '            Exit Sub
        '        End If

        '        If txtMobNo.Text.Trim.Length <> 10 Then
        '            txtMobNo.Focus()
        '            lblValidationMsg.Text = "Enter valid 10 digits Mobile No." : lblUPError.Text = "Enter valid 10 digits Mobile No."
        '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '            Exit Sub
        '        End If
        '    End If
        '    If txtMail.Text.Trim = "" Then
        '        txtMail.Focus()
        '        lblValidationMsg.Text = "Enter E-Mail." : lblUPError.Text = "Enter E-Mail."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtSecurityQuestion.Text.Trim = "" Then
        '        txtSecurityQuestion.Focus()
        '        lblValidationMsg.Text = "Enter Security Question." : lblUPError.Text = "Enter Security Question."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtSecurityQuestion.Text.Trim.Length > 250 Then
        '        txtSecurityQuestion.Focus()
        '        lblValidationMsg.Text = "Security Question exceeded maximum size(max 250 characters)." : lblUPError.Text = "Security Question exceeded maximum size(max 250 characters)."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtAnswer.Text.Trim = "" Then
        '        txtAnswer.Focus()
        '        lblValidationMsg.Text = "Enter Answer." : lblUPError.Text = "Enter Answer."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtAnswer.Text.Trim.Length > 250 Then
        '        txtAnswer.Focus()
        '        lblValidationMsg.Text = "Answer exceeded maximum size(max 250 characters)." : lblUPError.Text = "Answer exceeded maximum size(max 250 characters)."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '        Exit Sub
        '    End If
        '    If txtExperiencesummary.Text.Trim.Length > 8000 Then
        '        txtExperiencesummary.Focus()
        '        lblValidationMsg.Text = "Experience Summary exceeded maximum size(max 8000 characters)." : lblUPError.Text = "Experience Summary exceeded maximum size(max 8000 characters)."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '        Exit Sub
        '    End If

        '    objUser = objclsCPFP.LoadUserprofile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
        '    txtLoginName.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_LoginName)
        '    sSecurityAnswer = objclsFASGeneral.EncryptPassword(Trim(txtAnswer.Text))
        '    For i = 0 To cblQualification.Items.Count - 1
        '        If cblQualification.Items(i).Selected = True Then
        '            sQual = sQual & "," & cblQualification.Items(i).Value
        '        End If
        '    Next

        '    If txtOthers.Text.Trim.Length > 5000 Then
        '        lblValidationMsg.Text = "Others Details exceeded maximum size(max 5000 characters)." : lblUPError.Text = "Others Details exceeded maximum size(max 5000 characters)."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
        '        txtOthers.Focus()
        '        Exit Sub
        '    End If
        '    objclsCPFP.UpdateUserProfile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objclsFASGeneral.SafeSQL(txtMobNo.Text), objclsFASGeneral.SafeSQL(txtExperiencesummary.Text), ddlExperience.SelectedIndex, sQual, objclsFASGeneral.SafeSQL(txtOthers.Text), objclsFASGeneral.SafeSQL(txtSecurityQuestion.Text), sSecurityAnswer, objclsFASGeneral.SafeSQL(txtMail.Text), sSession.IPAddress)
        '    objclsGeneralFunctions.SaveFASFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "User Profile", "Profile Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
        '    lblValidationMsg.Text = "Successfully Updated."
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        'Catch ex As Exception
        '    lblUPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateUserProfile_Click")
        'End Try
    End Sub

    Private Sub lnkbtnCustomerBilling_Click(sender As Object, e As EventArgs) Handles lnkbtnCustomerBilling.Click
        Try
            GetClickedURL("CustomerBilling")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerBilling_Click")
        End Try
    End Sub
    Private Sub lnkbtnDriverBilling_Click(sender As Object, e As EventArgs) Handles lnkbtnDriverBilling.Click
        Try
            GetClickedURL("DriverBilling")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDriverBilling_Click")
        End Try
    End Sub
    Private Sub lnkbtnPumpBilling_Click(sender As Object, e As EventArgs) Handles lnkbtnPumpBilling.Click
        Try
            GetClickedURL("PumpBilling")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPumpBilling_Click")
        End Try
    End Sub
    Private Sub lnkbtnCustomerBillingReport_Click(sender As Object, e As EventArgs) Handles lnkbtnCustomerBillingReport.Click
        Try
            GetClickedURL("CustomerBillingReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerBillingReport_Click")
        End Try
    End Sub

    Private Sub lnkbtnPumpBillingReport_Click(sender As Object, e As EventArgs) Handles lnkbtnPumpBillingReport.Click
        Try
            GetClickedURL("PmpBillingReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerBillingReport_Click")
        End Try
    End Sub

    Private Sub lnkbtnDriverBillingReport_Click(sender As Object, e As EventArgs) Handles lnkbtnDriverBillingReport.Click
        Try
            GetClickedURL("DriverBillingReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerBillingReport_Click")
        End Try
    End Sub
    Private Sub lnkbtnVehicleMaintanance_Click(sender As Object, e As EventArgs) Handles lnkbtnVehicleMaintanance.Click
        Try
            sSession.Menu = "VehicleMaintanance" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/VehicleMaintanance.aspx", False) 'HomePages/Accounts
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnVehicleMaintanance_Click")
        End Try
    End Sub

    Private Sub lnkDynReports_Click(sender As Object, e As EventArgs) Handles lnkDynReports.Click
        Try
            GetClickedURL("DynReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkDynReports_Click")
        End Try
    End Sub
End Class

