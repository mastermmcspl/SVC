Imports BusinesLayer
Imports System.Data
Imports System.IO
Partial Class Accounts
    Inherits System.Web.UI.MasterPage
    Private Shared sFormName As String = "Accounts Masterpage"
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
            If sSession.Menu = "ACCOUNTS" Then
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
            liOB.Attributes.Remove("class") : liTrans.Attributes.Remove("class")
            'liFA.Attributes.Remove("class")
            liSalesTransaction.Attributes.Remove("class")
            liPurchaseTransaction.Attributes.Remove("class") : liNon.Attributes.Remove("class")

            lnkbtnOpeningBalance.Font.Italic = False : lnkbtnOpeningBalance.Font.Bold = False
            lnkbtnPSJE.Font.Italic = False : lnkbtnPSJE.Font.Bold = False
            lnkbtnPaymentTransaction.Font.Italic = False : lnkbtnPaymentTransaction.Font.Bold = False
            lnkbtnPCTransaction.Font.Italic = False : lnkbtnPCTransaction.Font.Bold = False
            LnkbtnPCDayBook.Font.Italic = False : LnkbtnPCDayBook.Font.Bold = False
            lnkbtnReceiptTransaction.Font.Italic = False : lnkbtnReceiptTransaction.Font.Bold = False
            lnkbtnJETransaction.Font.Italic = False : lnkbtnJETransaction.Font.Bold = False
            'lnkbtnFixedAsset.Font.Italic = False : lnkbtnFixedAsset.Font.Bold = False
            'lnkbtnFixedAssetJE.Font.Italic = False : lnkbtnFixedAssetJE.Font.Bold = False
            lnkBank.Font.Italic = False : lnkBank.Font.Bold = False
            lnkbtnReports.Font.Italic = False : lnkbtnReports.Font.Bold = False
            lnkbtnVendorDynamicReport.Font.Italic = False : lnkbtnVendorDynamicReport.Font.Bold = False
            'lnkAuxilaryReport.Font.Italic = False : lnkAuxilaryReport.Font.Bold = False
            lnkSalesTransaction.Font.Italic = False : lnkSalesTransaction.Font.Bold = False
            lnkPurchaseTransaction.Font.Italic = False : lnkPurchaseTransaction.Font.Bold = False
            'lnkbtnFixedMaster.Font.Italic = False : lnkbtnFixedMaster.Font.Bold = False
            lnkbtnBillDashboard.Font.Italic = False : lnkbtnBillDashboard.Font.Bold = False
            lnkbtnNonTrading.Font.Italic = False : lnkbtnNonTrading.Font.Bold = False
            lnkTrailBal.Font.Italic = False : lnkTrailBal.Font.Bold = False
            lnkbtnStockReport.Font.Italic = False : lnkbtnStockReport.Font.Bold = False
            lnkbtnValueReport.Font.Italic = False : lnkbtnValueReport.Font.Bold = False

            If sSession.SubMenu = "OpeningBalance" Then
                liOB.Attributes.Add("class", "open")
                If sSession.Form = "OpeningBalance" Then
                    lnkbtnOpeningBalance.Font.Italic = True : lnkbtnOpeningBalance.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "PartyDashboard" Then
                liDashBoard.Attributes.Add("class", "open")
                If sSession.Form = "PartyDashboard" Then
                    lnkbtnPartyDB.Font.Italic = True : lnkbtnPartyDB.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "AccountDashboard" Then
                liDashBoard.Attributes.Add("class", "open")
                If sSession.Form = "AccountDashboard" Then
                    lnkbtnAccDB.Font.Italic = True : lnkbtnAccDB.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "TrailBal" Then
                liDashBoard.Attributes.Add("class", "open")
                If sSession.Form = "TrailBal" Then
                    lnkTrailBal.Font.Italic = True : lnkTrailBal.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "Transactions" Then
                liTrans.Attributes.Add("class", "open")
                If sSession.Form = "PurchaseSalesJETransactions" Then
                    lnkbtnPSJE.Font.Italic = True : lnkbtnPSJE.Font.Bold = True
                ElseIf sSession.Form = "PaymentTransaction" Then
                    lnkbtnPaymentTransaction.Font.Italic = True : lnkbtnPaymentTransaction.Font.Bold = True
                ElseIf sSession.Form = "PCTransaction" Then
                    lnkbtnPCTransaction.Font.Italic = True : lnkbtnPCTransaction.Font.Bold = True
                ElseIf sSession.Form = "PCDayBook" Then
                    LnkbtnPCDayBook.Font.Italic = True : LnkbtnPCDayBook.Font.Bold = True
                ElseIf sSession.Form = "ReceiptTransaction" Then
                    lnkbtnReceiptTransaction.Font.Italic = True : lnkbtnReceiptTransaction.Font.Bold = True
                ElseIf sSession.Form = "JETransaction" Then
                    lnkbtnJETransaction.Font.Italic = True : lnkbtnJETransaction.Font.Bold = True
                ElseIf sSession.Form = "BillDashboard" Then
                    lnkbtnBillDashboard.Font.Italic = True : lnkbtnBillDashboard.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "Reports" Then
                liReports.Attributes.Add("class", "open")
                If sSession.Form = "Reports" Then
                    lnkbtnReports.Font.Italic = True : lnkbtnReports.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "VendordynamicReports" Then
                liVendorReport.Attributes.Add("class", "open")
                If sSession.Form = "VendordynamicReports" Then
                    lnkbtnVendorDynamicReport.Font.Italic = True : lnkbtnVendorDynamicReport.Font.Bold = True
                End If
                'ElseIf sSession.SubMenu = "Auxilary Report" Then
                '    liAuxilaryReport.Attributes.Add("class", "open")
                '    If sSession.Form = "Auxilar yReport" Then
                '        lnkAuxilaryReport.Font.Italic = True : lnkAuxilaryReport.Font.Bold = True
                '    End If
                'ElseIf sSession.SubMenu = "Fixed Asset" Then
                '    liFA.Attributes.Add("class", "open")
                '    If sSession.Form = "Fixed Assets" Then
                '        lnkbtnFixedAsset.Font.Italic = True : lnkbtnFixedAsset.Font.Bold = True
                '    ElseIf sSession.Form = "FAJE" Then
                '        lnkbtnFixedAssetJE.Font.Italic = True : lnkbtnFixedAssetJE.Font.Bold = True
                '    ElseIf sSession.Form = "FixedMaster" Then
                '        lnkbtnFixedMaster.Font.Italic = True : lnkbtnFixedMaster.Font.Bold = True
                '    End If
            ElseIf sSession.SubMenu = "Bank Reconcilation" Then
                liFA.Attributes.Add("class", "open")
                If sSession.Form = "Bank Reconcilation" Then
                    lnkBank.Font.Italic = True : lnkBank.Font.Bold = True
                End If
                'ElseIf sSession.SubMenu = "Post Dated Cheque Registry" Then
                '    liFA.Attributes.Add("class", "open")
                '    If sSession.Form = "Post Dated Cheque Registry" Then
                '        lnkCheque.Font.Italic = True : lnkCheque.Font.Bold = True
                '    End If
            ElseIf sSession.SubMenu = "PurchaseTransaction" Then
                liPurchaseTransaction.Attributes.Add("class", "open")
                If sSession.Form = "Purchase Transaction" Then
                    lnkPurchaseTransaction.Font.Italic = True : lnkPurchaseTransaction.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "SalesTransaction" Then
                liSalesTransaction.Attributes.Add("class", "open")
                If sSession.Form = "Sales Transaction" Then
                    lnkSalesTransaction.Font.Italic = True : lnkSalesTransaction.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "NonTradingPurchase" Then
                liNon.Attributes.Add("class", "open")
                If sSession.Form = "NonTrading Purchase Transaction" Then
                    lnkbtnNonTrading.Font.Italic = True : lnkbtnNonTrading.Font.Bold = True
                End If

            ElseIf sSession.SubMenu = "StockReport" Then
                liTrans.Attributes.Add("class", "open")
                If sSession.Form = "StockReport" Then
                    lnkbtnStockReport.Font.Italic = True : lnkbtnStockReport.Font.Bold = True
                End If

            ElseIf sSession.SubMenu = "ValueReport" Then
                liTrans.Attributes.Add("class", "open")
                If sSession.Form = "ValueReport" Then
                    lnkbtnValueReport.Font.Italic = True : lnkbtnValueReport.Font.Bold = True
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetClickedURL(ByVal sForm As String)
        Try
            If sForm = "OpeningBalance" Then
                sSession.SubMenu = "OpeningBalance" : sSession.Form = "OpeningBalance"
                Response.Redirect("~/Accounts/OpeningBalance.aspx", False)

            ElseIf sForm = "Dashboard" Then
                sSession.SubMenu = "Dashboard" : sSession.Form = "Dashboard"
                Response.Redirect("~/Dashboard/AccountsDashboard.aspx", False)

            ElseIf sForm = "TrailBal" Then
                sSession.SubMenu = "TrailBal" : sSession.Form = "TrailBal"
                Response.Redirect("~/Dashboard/TrailBalanceDashBoard.aspx", False)

            ElseIf sForm = "PurchaseSalesJETransactions" Then
                sSession.SubMenu = "Transactions" : sSession.Form = "PurchaseSalesJETransactions"
                Response.Redirect("~/Accounts/PurchaseSalesJE.aspx", False)

            ElseIf sForm = "PaymentTransaction" Then
                sSession.SubMenu = "Transactions" : sSession.Form = "PaymentTransaction"
                Response.Redirect("~/Accounts/PaymentTransaction.aspx", False)

            ElseIf sForm = "PCTransaction" Then
                sSession.SubMenu = "Transactions" : sSession.Form = "PCTransaction"
                Response.Redirect("~/Accounts/PettyCashTransaction.aspx", False)

            ElseIf sForm = "PCDayBook" Then
                sSession.SubMenu = "Transactions" : sSession.Form = "PCDayBook"
                Response.Redirect("~/Accounts/PettyCashDayBook.aspx", False)


            ElseIf sForm = "ReceiptTransaction" Then
                sSession.SubMenu = "Transactions" : sSession.Form = "ReceiptTransaction"
                Response.Redirect("~/Accounts/ReceiptTransaction.aspx", False)

            ElseIf sForm = "JETransaction" Then
                sSession.SubMenu = "Transactions" : sSession.Form = "JETransaction"
                Response.Redirect("~/Accounts/JETransaction.aspx", False)

            ElseIf sForm = "BillDashboard" Then
                sSession.SubMenu = "BillDashboard" : sSession.Form = "BillDashboard"
                Response.Redirect("~/Accounts/BillDashboard.aspx", False)

            ElseIf sForm = "Fixed Assets" Then
                sSession.SubMenu = "Fixed Assetss" : sSession.Form = "Fixed Assets"
                Response.Redirect("~/Accounts/FixedAsset.aspx", False) 'Accounts/Fixed Asset

            ElseIf sForm = "FAJE" Then
                sSession.SubMenu = "Fixed Assetss" : sSession.Form = "FAJE"
                Response.Redirect("~/Accounts/FixedAssetJEEntry.aspx", False) 'Accounts/Fixed Asset JE

            ElseIf sForm = "FixedMaster" Then
                sSession.SubMenu = "FixedMaster" : sSession.Form = "FixedMaster"
                Response.Redirect("~/Accounts/FixedMaster.aspx", False) 'Accounts/Fixed Master

            ElseIf sForm = "Bank Reconcilation" Then
                sSession.SubMenu = "Bank Reconcilation" : sSession.Form = "Bank Reconcilation"
                Response.Redirect("~/Accounts/BankReconciliation.aspx", False) 'Accounts/BankReconciliation

            ElseIf sForm = "Post Dated Cheque Registry" Then
                sSession.SubMenu = "Post Dated Cheque Registry" : sSession.Form = "Post Dated Cheque Registry"
                Response.Redirect("~/Accounts/PostDatedCheDetails.aspx", False) 'Accounts/Post Dated Cheque Registry

            ElseIf sForm = "Reports" Then
                sSession.SubMenu = "Reports" : sSession.Form = "Reports"
                Response.Redirect("~/Reports/Reports.aspx", False)

            ElseIf sForm = "AuxilaryReport" Then
                sSession.SubMenu = "AuxilaryReport" : sSession.Form = "AuxilaryReport"
                Response.Redirect("~/Reports/AuxilaryReport.aspx", False) 'Accounts/Post Dated Cheque Registry

            ElseIf sForm = "PurchaseTransaction" Then
                sSession.SubMenu = "PurchaseTransaction" : sSession.Form = "Purchase Transaction"
                Response.Redirect("~/Accounts/PurchaseTransactionDashboard.aspx", False)

            ElseIf sForm = "SalesTransaction" Then
                sSession.SubMenu = "SalesTransaction" : sSession.Form = "Sales Transaction"
                Response.Redirect("~/Accounts/SalesTransactionDashboard.aspx", False) 'Accounts/Post Dated Cheque Registry

            ElseIf sForm = "NonTradingPurchase" Then
                sSession.SubMenu = "NonTradingPurchase" : sSession.Form = "NonTradingPurchase Transaction"
                Response.Redirect("~/Accounts/NonTradingPurchaseDashboard.aspx", False)

            ElseIf sForm = "StockReport" Then
                sSession.SubMenu = "StockReport" : sSession.Form = "StockReport"
                Response.Redirect("~/Reports/AccountsStock.aspx", False)

            ElseIf sForm = "ValueReport" Then
                sSession.SubMenu = "ValueReport" : sSession.Form = "ValueReport"
                Response.Redirect("~/Reports/ValueReport.aspx", False)

            End If
            Session("AllSession") = sSession
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub lnkbtnValueReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnValueReport.Click
        Try
            GetClickedURL("ValueReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnStockReport_Click")
        End Try
    End Sub
    Protected Sub lnkbtnStockReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnStockReport.Click
        Try
            GetClickedURL("StockReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnStockReport_Click")
        End Try
    End Sub
    Protected Sub lnkbtnBillDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnBillDashboard.Click
        Try
            GetClickedURL("BillDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBillDashboard_Click")
        End Try
    End Sub
    'Protected Sub lnkbtnFixedMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFixedMaster.Click
    '    Try
    '        GetClickedURL("FixedMaster")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFixedMaster_Click")
    '    End Try
    'End Sub
    Protected Sub lnkPurchaseTransaction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPurchaseTransaction.Click
        Try
            GetClickedURL("PurchaseTransaction")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkPurchaseTransaction_Click")
        End Try
    End Sub
    Protected Sub lnkSalesTransaction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSalesTransaction.Click
        Try
            GetClickedURL("SalesTransaction")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkSalesTransaction_Click")
        End Try
    End Sub
    Protected Sub lnkbtnOpeningBalance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnOpeningBalance.Click
        Try
            GetClickedURL("OpeningBalance")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnOpeningBalance_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPSJE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPSJE.Click
        Try
            GetClickedURL("PurchaseSalesJETransactions")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPSJE_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPaymentTransaction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPaymentTransaction.Click
        Try
            GetClickedURL("PaymentTransaction")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPaymentTransaction_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPCTransaction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPCTransaction.Click
        Try
            GetClickedURL("PCTransaction")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPCTransaction_Click")
        End Try
    End Sub
    Protected Sub lnkbtnReceiptTransaction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnReceiptTransaction.Click
        Try
            GetClickedURL("ReceiptTransaction")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnReceiptTransaction_Click")
        End Try
    End Sub
    Protected Sub lnkbtnJETransaction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnJETransaction.Click
        Try
            GetClickedURL("JETransaction")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnJETransaction_Click")
        End Try
    End Sub
    Protected Sub lnkTrailBal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkTrailBal.Click
        Try
            GetClickedURL("TrailBal")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkTrailBal_Click")
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
    'Protected Sub lnkbtnFixedAsset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFixedAsset.Click
    '    Try
    '        sSession.Menu = "Fixed Asset" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Accounts/FixedAsset.aspx", False) 'Accounts/Fixed Asset
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFixedAsset_Click")
    '    End Try
    'End Sub
    'Protected Sub lnkbtnFixedAssetJE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFixedAssetJE.Click
    '    Try
    '        GetClickedURL("FAJE")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFixedAssetJE_Click")
    '    End Try
    'End Sub
    'Private Sub lnkBank_Click(sender As Object, e As EventArgs) Handles lnkBank.Click
    '    Try
    '        sSession.Menu = "Bank Reconcilation" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Accounts/BankReconciliation.aspx", False) 'Accounts/BankReconciliation
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBank_Click")
    '    End Try
    'End Sub
    'Private Sub lnkCheque_Click(sender As Object, e As EventArgs) Handles lnkCheque.Click
    '    Try
    '        sSession.Menu = "Post Dated Cheque Registry" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Accounts/PostDatedCheDetails.aspx", False) 'Accounts/ChequeDetails
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkCheque_Click")
    '    End Try
    'End Sub
    Protected Sub lnkbtnReports_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnReports.Click
        Try
            sSession.Menu = "Reports" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/Reports/Reports.aspx", False) 'Reports/Reports
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnReports_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPartyDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPartyDB.Click
        Try
            sSession.Menu = "PartyDashboard" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/Dashboard/PartyDashboard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDashboard_Click")
        End Try
    End Sub
    Protected Sub lnkbtnAccDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAccDB.Click
        Try
            sSession.Menu = "AccountDashboard" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/Dashboard/AccountDashboard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAccDB_Click")
        End Try
    End Sub

    'Protected Sub AuxilaryReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAuxilaryReport.Click
    '    Try
    '        GetClickedURL("AuxilaryReport")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AuxilaryReport_Click")
    '    End Try
    'End Sub
    Protected Sub lnkbtnNonTrading_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnNonTrading.Click
        Try
            GetClickedURL("NonTradingPurchase")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkPurchaseTransaction_Click")
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
    Private Sub lnkBank_Click(sender As Object, e As EventArgs) Handles lnkBank.Click
        Try
            'sSession.Menu = "Bank Reconcilation" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            'Response.Redirect("~/Accounts/BankReconciliation.aspx", False) 'Accounts/BankReconciliation
            sSession.Menu = "Bank Reconcilation" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/Accounts/BankReconciliationMaster.aspx", False) 'Accounts/BankReconciliation
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBank_Click")
        End Try
    End Sub
    Private Sub lnkbtnVendorDynamicReport_Click(sender As Object, e As EventArgs) Handles lnkbtnVendorDynamicReport.Click
        Try
            sSession.Menu = "VendordynamicReports" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/Reports/VendorDynamicReports.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnVendorDynamicReport_Click")
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
    Protected Sub LnkbtnPCDayBook_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkbtnPCDayBook.Click
        Try
            GetClickedURL("PCDayBook")
        Catch ex As Exception
            'GetLineNumber(ex)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LnkbtnPCDayBook_Click") ', GetLineNumber(ex)
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
    Private Sub lnkbtnVehicleMaintanance_Click(sender As Object, e As EventArgs) Handles lnkbtnVehicleMaintanance.Click
        Try
            sSession.Menu = "VehicleMaintanance" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/VehicleMaintanance.aspx", False) 'HomePages/Accounts
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnVehicleMaintanance_Click")
        End Try
    End Sub
End Class

