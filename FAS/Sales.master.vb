Imports BusinesLayer
Imports System.Data
Imports System.IO
Partial Class Sales
    Inherits System.Web.UI.MasterPage
    Private Shared sFormName As String = "Sales Masterpage"
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
            If sSession.Menu = "SALES" Then
                GetSubMenuOpen()
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindExperience()
        Try
            ddlExperience.Items.Clear()
            ddlExperience.Items.Insert(0, "0")
            ddlExperience.Items.Insert(1, "1")
            ddlExperience.Items.Insert(2, "2")
            ddlExperience.Items.Insert(3, "3")
            ddlExperience.Items.Insert(4, "4")
            ddlExperience.Items.Insert(5, "5")
            ddlExperience.Items.Insert(6, "6")
            ddlExperience.Items.Insert(7, "7")
            ddlExperience.Items.Insert(8, "8")
            ddlExperience.Items.Insert(9, "9")
            ddlExperience.Items.Insert(10, "10")
            ddlExperience.Items.Insert(11, "11")
            ddlExperience.Items.Insert(12, "12")
            ddlExperience.Items.Insert(13, "13")
            ddlExperience.Items.Insert(14, "14")
            ddlExperience.Items.Insert(15, "15")
            ddlExperience.Items.Insert(16, "16")
            ddlExperience.Items.Insert(17, "17")
            ddlExperience.Items.Insert(18, "18")
            ddlExperience.Items.Insert(19, "19")
            ddlExperience.Items.Insert(20, "20")
            ddlExperience.Items.Insert(21, "21")
            ddlExperience.Items.Insert(22, "22")
            ddlExperience.Items.Insert(23, "23")
            ddlExperience.Items.Insert(24, "24")
            ddlExperience.Items.Insert(25, "25")
            ddlExperience.Items.Insert(26, "26")
            ddlExperience.Items.Insert(27, "27")
            ddlExperience.Items.Insert(28, "28")
            ddlExperience.Items.Insert(29, "29")
            ddlExperience.Items.Insert(30, "30")
            ddlExperience.Items.Insert(31, "31")
            ddlExperience.Items.Insert(32, "32")
            ddlExperience.Items.Insert(33, "33")
            ddlExperience.Items.Insert(34, "34")
            ddlExperience.Items.Insert(35, "35")
            ddlExperience.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindQualification()
        Try
            cblQualification.Items.Clear()
            cblQualification.Items.Add(New ListItem("Bachelor Degree", "1"))
            cblQualification.Items.Add(New ListItem("Master Degree", "2"))
            cblQualification.Items.Add(New ListItem("PG", "3"))
            cblQualification.Items.Add(New ListItem("Chartered Accountant", "4"))
            cblQualification.Items.Add(New ListItem("CIA Part1", "5"))
            cblQualification.Items.Add(New ListItem("CIA Part2", "6"))
            cblQualification.Items.Add(New ListItem("CIA Part3", "7"))
            cblQualification.Items.Add(New ListItem("ICWA", "8"))
            cblQualification.Items.Add(New ListItem("CISA", "9"))
            cblQualification.Items.Add(New ListItem("CISSP", "10"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetSubMenuOpen()
        Try
            'liSCU.Attributes.Remove("class")
            liSO.Attributes.Remove("class") : liOCSO.Attributes.Remove("class") : liAllocation.Attributes.Remove("class")
            liDD.Attributes.Remove("class") : liDD.Attributes.Remove("class") : liDD.Attributes.Remove("class")
            liSI.Attributes.Remove("class") : liSI.Attributes.Remove("class") : liSI.Attributes.Remove("class")
            liSR.Attributes.Remove("class") : liSR.Attributes.Remove("class") : liSR.Attributes.Remove("class")
            'liSRR.Attributes.Remove("class") : liSRR.Attributes.Remove("class") : liSRR.Attributes.Remove("class")
            liSDR.Attributes.Remove("class") : liSDR.Attributes.Remove("class") : liSDR.Attributes.Remove("class")
            liSOU.Attributes.Remove("class") : liSOU.Attributes.Remove("class") : liSOU.Attributes.Remove("class")
            liSSO.Attributes.Remove("class") : liDF.Attributes.Remove("class")

            lnkbtnSalesOrder.Font.Italic = False : lnkbtnSalesOrder.Font.Bold = False
            lnkbtnOCSalesOrder.Font.Italic = False : lnkbtnOCSalesOrder.Font.Bold = False
            lnkbtnAllocation.Font.Italic = False : lnkbtnAllocation.Font.Bold = False
            lnkbtnDispatchDetails.Font.Italic = False : lnkbtnDispatchDetails.Font.Bold = False
            lnkbtnSalesReturn.Font.Italic = False : lnkbtnSalesReturn.Font.Bold = False
            lnkBtnSalesInvoice.Font.Italic = False : lnkBtnSalesInvoice.Font.Bold = False
            lnkbtnSalesDynamicReport.Font.Italic = False : lnkbtnSalesDynamicReport.Font.Bold = False
            'lnkbtnSalesReturnReport.Font.Italic = False : lnkbtnSalesReturnReport.Font.Bold = False
            lnkbtnSaleOrderUpload.Font.Italic = False : lnkbtnSaleOrderUpload.Font.Bold = True
            'lnkbtnSalesCustomerUpload.Font.Italic = False : lnkbtnSalesCustomerUpload.Font.Bold = False
            lnkbtnSampleSaleOrder.Font.Italic = False : lnkbtnSampleSaleOrder.Font.Bold = False
            lnkbtnDispatchForm.Font.Italic = False : lnkbtnDispatchForm.Font.Bold = False

            If sSession.SubMenu = "SalesOrder" Then
                liSO.Attributes.Add("class", "open")
                If sSession.Form = "SalesOrder" Then
                    lnkbtnSalesOrder.Font.Italic = True : lnkbtnSalesOrder.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "OCSalesOrder" Then
                liOCSO.Attributes.Add("class", "open")
                If sSession.Form = "OCSalesOrder" Then
                    lnkbtnOCSalesOrder.Font.Italic = True : lnkbtnOCSalesOrder.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "Allocation" Then
                liAllocation.Attributes.Add("class", "open")
                If sSession.Form = "Allocation" Then
                    lnkbtnAllocation.Font.Italic = True : lnkbtnAllocation.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "DispatchDetails" Then
                liDD.Attributes.Add("class", "open")
                If sSession.Form = "DispatchDetails" Then
                    lnkbtnDispatchDetails.Font.Italic = True : lnkbtnDispatchDetails.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "DispatchForm" Then
                liDF.Attributes.Add("class", "open")
                If sSession.Form = "DispatchForm" Then
                    lnkbtnDispatchForm.Font.Italic = True : lnkbtnDispatchForm.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "SalesInvoice" Then
                liSI.Attributes.Add("class", "open")
                If sSession.Form = "SalesInvoice" Then
                    lnkBtnSalesInvoice.Font.Italic = True : lnkBtnSalesInvoice.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "SalesReturn" Then
                liSR.Attributes.Add("class", "open")
                If sSession.Form = "SalesReturn" Then
                    lnkbtnSalesReturn.Font.Italic = True : lnkbtnSalesReturn.Font.Bold = True
                End If
                'ElseIf sSession.SubMenu = "SalesReturnReport" Then
                '    liSRR.Attributes.Add("class", "open")
                '    If sSession.Form = "SalesReturnReport" Then
                '        lnkbtnSalesReturnReport.Font.Italic = True : lnkbtnSalesReturnReport.Font.Bold = True
                '    End If
            ElseIf sSession.SubMenu = "SalesDynamicReport" Then
                liSDR.Attributes.Add("class", "open")
                If sSession.Form = "SalesDynamicReport" Then
                    lnkbtnSalesDynamicReport.Font.Italic = True : lnkbtnSalesDynamicReport.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "SalesOrderUpload" Then
                liSOU.Attributes.Add("class", "open")
                If sSession.Form = "SalesOrderUpload" Then
                    lnkbtnSaleOrderUpload.Font.Italic = True : lnkbtnSaleOrderUpload.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "SampleSalesOrder" Then
                liSSO.Attributes.Add("class", "open")
                If sSession.Form = "SampleSalesOrder" Then
                    lnkbtnSampleSaleOrder.Font.Italic = True : lnkbtnSampleSaleOrder.Font.Bold = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetClickedURL(ByVal sForm As String)
        Try
            If sForm = "SalesPartyCustomer" Then
                sSession.SubMenu = "SalesPartyCustomer" : sSession.Form = "SalesPartyCustomer"
                Response.Redirect("~/Sales/SalesPartyDashboard.aspx", False)
            ElseIf sForm = "SalesPartyUpload" Then
                sSession.SubMenu = "SalesPartyUpload" : sSession.Form = "SalesPartyUpload"
                Response.Redirect("~/Sales/SalesPartyUpload.aspx", False)
            ElseIf sForm = "SalesOrder" Then
                sSession.SubMenu = "SalesOrder" : sSession.Form = "SalesOrder"
                Response.Redirect("~/Sales/SalesOrderMaster.aspx", False)
            ElseIf sForm = "OCSalesOrder" Then
                sSession.SubMenu = "OCSalesOrder" : sSession.Form = "OCSalesOrder"
                Response.Redirect("~/Sales/OralCounterMaster.aspx", False)
            ElseIf sForm = "Allocation" Then
                sSession.SubMenu = "Allocation" : sSession.Form = "Allocation"
                Response.Redirect("~/Sales/AllocationMaster.aspx", False)
            ElseIf sForm = "DispatchForm" Then
                sSession.SubMenu = "DispatchForm" : sSession.Form = "DispatchForm"
                Response.Redirect("~/Sales/DispatchFormMaster.aspx", False)
            ElseIf sForm = "DispatchDetails" Then
                sSession.SubMenu = "DispatchDetails" : sSession.Form = "DispatchDetails"
                Response.Redirect("~/Sales/DispatchDetailsMaster.aspx", False)
            ElseIf sForm = "SalesInvoice" Then
                sSession.SubMenu = "SalesInvoice" : sSession.Form = "SalesInvoice"
                Response.Redirect("~/Sales/DispatchNote.aspx", False)
                'ElseIf sForm = "SalesReturn" Then
                '    sSession.SubMenu = "SalesReturn" : sSession.Form = "SalesReturn"
                '    Response.Redirect("~/Sales/SalesReturnMaster.aspx", False)


                'Preethi Changes
            ElseIf sForm = "SalesReturn" Then
                sSession.SubMenu = "SalesReturn" : sSession.Form = "SalesReturn"
                Response.Redirect("~/Sales/SalesReturnDashboard.aspx", False)
            ElseIf sForm = "SalesReturnReport" Then
                sSession.SubMenu = "SalesReturnReport" : sSession.Form = "SalesReturnReport"
                Response.Redirect("~/Reports/Viewer/SalesReturnReport.aspx", False)
            ElseIf sForm = "SalesDynamicReport" Then
                sSession.SubMenu = "SalesDynamicReport" : sSession.Form = "SalesDynamicReport"
                Response.Redirect("~/Reports/Viewer/SalesDynamicReport.aspx", False)
            ElseIf sForm = "SalesOrderUpload" Then
                sSession.SubMenu = "SalesOrderUpload" : sSession.Form = "SalesOrderUpload"
                Response.Redirect("~/Sales/UploadSalesOrders.aspx", False)
            ElseIf sForm = "SampleSalesOrder" Then
                sSession.SubMenu = "SampleSalesOrder" : sSession.Form = "SampleSalesOrder"
                Response.Redirect("~/Sales/SampleSalesOrder.aspx", False)

            End If
            Session("AllSession") = sSession
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub lnkbtnSalesOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSalesOrder.Click
        Try
            GetClickedURL("SalesOrder")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSalesOrder_Click")
        End Try
    End Sub
    Protected Sub lnkbtnOCSalesOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnOCSalesOrder.Click
        Try
            GetClickedURL("OCSalesOrder")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnOCSalesOrder_Click")
        End Try
    End Sub
    Protected Sub lnkbtnAllocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAllocation.Click
        Try
            GetClickedURL("Allocation")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAllocation_Click")
        End Try
    End Sub
    Protected Sub lnkbtnDispatchForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDispatchForm.Click
        Try
            GetClickedURL("DispatchForm")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDispatchForm_Click")
        End Try
    End Sub
    Protected Sub lnkbtnDispatchDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDispatchDetails.Click
        Try
            GetClickedURL("DispatchDetails")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDispatchDetails_Click")
        End Try
    End Sub
    Protected Sub lnkBtnSalesInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBtnSalesInvoice.Click
        Try
            GetClickedURL("SalesInvoice")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnSalesInvoice_Click")
        End Try
    End Sub
    Protected Sub lnkbtnSalesReturn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSalesReturn.Click
        Try
            GetClickedURL("SalesReturn")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSalesReturn_Click")
        End Try
    End Sub
    'Protected Sub lnkbtnSalesReturnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSalesReturnReport.Click
    '    Try
    '        GetClickedURL("SalesReturnReport")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSalesReturnReport_Click")
    '    End Try
    'End Sub
    Protected Sub lnkbtnSalesDynamicReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSalesDynamicReport.Click
        Try
            GetClickedURL("SalesDynamicReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSalesDynamicReport_Click")
        End Try
    End Sub
    Protected Sub lnkbtnSaleOrderUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSaleOrderUpload.Click
        Try
            GetClickedURL("SalesOrderUpload")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSaleOrderUpload_Click")
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
    Public Sub LoadUserProfile()
        'Dim sArray As Array
        'Dim j As Integer
        'Try
        '    objUser = objclsCPFP.LoadUserprofile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
        '    txtLoginName.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_LoginName)
        '    txtSAPcode.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_Code)
        '    txtEmpName.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_fullName)
        '    txtMail.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_Email)
        '    If objUser.sUsr_MobileNo = "&nbsp;" Then
        '        txtMobNo.Text = ""
        '    Else
        '        txtMobNo.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_MobileNo)
        '    End If

        '    txtDesignation.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_Designation)
        '    If objUser.sUsr_GrpOrUserLvlPerm = 0 Then
        '        txtPermission.Text = "Role based"
        '    Else
        '        txtPermission.Text = "User based"
        '    End If
        '    txtRole.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_LevelGrp)
        '    txtSecurityQuestion.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_SecurityQuestion)

        '    If objUser.sUsr_Answer <> "" Then
        '        txtAnswer.Attributes.Add("value", objclsFASGeneral.DecryptPassword(objUser.sUsr_Answer))
        '    End If
        '    txtExperiencesummary.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_SkillSet)
        '    ddlExperience.SelectedIndex = objUser.iUsr_Experience
        '    txtOthers.Text = objclsFASGeneral.ReplaceSafeSQL(objUser.sUsr_Others)

        '    If objUser.sUsr_Qualification.Contains(",") = True Then
        '        sArray = objUser.sUsr_Qualification.Split(",")
        '        For i = 0 To sArray.Length - 1
        '            If sArray(i) <> "" Then
        '                For j = 0 To cblQualification.Items.Count - 1
        '                    If cblQualification.Items(j).Value = sArray(i) Then
        '                        cblQualification.Items(j).Selected = True
        '                    End If
        '                Next
        '            End If
        '        Next
        '    End If
        'Catch ex As Exception
        '    Throw
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

    Protected Sub lnkbtnSampleSaleOrder_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSampleSaleOrder.Click
        Try
            GetClickedURL("SampleSalesOrder")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSampleSaleOrder_Click")
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
    Protected Sub lnkbtnInventory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnInventory.Click
        Try
            sSession.Menu = "Inventory" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Inventory.aspx", False) 'HomePages/Inventory
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInventory_Click")
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnVehicleMaintanance_Click")
        End Try
    End Sub
End Class

