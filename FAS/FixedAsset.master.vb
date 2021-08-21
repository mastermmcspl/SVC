Imports BusinesLayer
Imports System.Data
Imports System.IO
Partial Class FixedAsset
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
            lblUserName.Text = "Branch" & " " & sSession.UserFullNameCode
            lblUserName.Text = "Welcome" & " " & sSession.UserFullNameCode
            sSession.YearID = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
            sSession.YearName = objclsGeneralFunctions.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

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
            FXASTDashboard.Attributes.Remove("class")
            FXASTRegister.Attributes.Remove("class")
            FXASTTrns.Attributes.Remove("class")
            FXASTDepComp.Attributes.Remove("class")

            If sSession.SubMenu = "AccountDashboard" Then
                FXASTDashboard.Attributes.Add("class", "open")
                If sSession.Form = "AccountDashboard" Then
                    lnkbtnFxdAst.Font.Italic = True : lnkbtnFxdAst.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "AccountRegister" Then
                FXASTRegister.Attributes.Add("class", "open")
                If sSession.Form = "AccountRegister" Then
                    lnkbtnAstReg.Font.Italic = True : lnkbtnAstReg.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "DepreciationComputation" Then
                FXASTDepComp.Attributes.Add("class", "open")
                If sSession.Form = "DepreciationComputation" Then
                    lnkbtnDepComp.Font.Italic = True : lnkbtnDepComp.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "AssetTransaction" Then
                FXASTTrns.Attributes.Add("class", "open")
                If sSession.Form = "AssetTransactionAddition" Then
                    lnkbtnAssetTransactionadd.Font.Italic = True : lnkbtnAssetTransactionadd.Font.Bold = True
                ElseIf sSession.Form = "AssetTransactionDeletion" Then
                    lnkbtnAssetTransactionDel.Font.Italic = True : lnkbtnAssetTransactionDel.Font.Bold = True
                ElseIf sSession.Form = "AssetTranFileUpload" Then
                    lnkbtnAssetTranFileUpload.Font.Italic = True : lnkbtnAssetTranFileUpload.Font.Bold = True
                ElseIf sSession.Form = "AssetTranFileUploadView" Then
                    lnkbtnAssetFileUploadView.Font.Italic = True : lnkbtnAssetFileUploadView.Font.Bold = True
                ElseIf sSession.Form = "AssetAdditionalDetails" Then
                    lnkbtnAssetAddlnDtls.Font.Italic = True : lnkbtnAssetAddlnDtls.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "AssetOPeningBalExcelUplaod" Then
                FXAOPExcel.Attributes.Add("class", "open")
                If sSession.Form = "AssetOPeningBalExcelUplaod" Then
                    lnkbtnFXOPExcel.Font.Italic = True : lnkbtnFXOPExcel.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "AssetOPeningBalExcelView" Then
                FXAOPExcelView.Attributes.Add("class", "open")
                If sSession.Form = "AssetOPeningBalExcelUplaod" Then
                    lnkbtnFXOPExcelview.Font.Italic = True : lnkbtnFXOPExcelview.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "DynamicReport" Then
                FAXDReport.Attributes.Add("class", "open")
                If sSession.Form = "DynamicReport" Then
                    lnkbtnDynamicReport.Font.Italic = True : lnkbtnDynamicReport.Font.Bold = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetClickedURL(ByVal sForm As String)
        Try
            If sForm = "AssetTransactionAddition" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/AssetAdditionDashBoard.aspx", False)
            ElseIf sForm = "AssetTransactionDeletion" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetDltnDashboard"
                Response.Redirect("~/FixedAsset/AssetDeletionDashboard.aspx", False)
            ElseIf sForm = "AssetTransactionDeletion" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/AssetTransactionDeletion.aspx", False)
            ElseIf sForm = "AssetTransactionFileUpload" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/FXDAsstFileUpload.aspx", False)
            ElseIf sForm = "AssetTranFileUploadView" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/AssetFileUploadView.aspx", False)
            ElseIf sForm = "AssetAdditionalDetails" Then
                sSession.SubMenu = "AssetTransaction" : sSession.Form = "AssetTransactionAddition"
                Response.Redirect("~/FixedAsset/AssetAddlnDtls.aspx", False)
            End If
            Session("AllSession") = sSession
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lnkbtnFxdAst_Click(sender As Object, e As EventArgs) Handles lnkbtnFxdAst.Click
        Try
            sSession.Menu = "FixedAsset" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/AssetMaster.aspx", False)
        Catch ex As Exception
            Throw
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
            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLogOut_Click")
        End Try
    End Sub

    Private Sub lnkbtnFixedAsset_Click(sender As Object, e As EventArgs) Handles lnkbtnFixedAsset.Click
        Try
            sSession.Menu = "FixedAsset" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/FixedAsset.aspx", False) 'HomePages/Accounts
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFixedAsset_Click")
        End Try
    End Sub
    Protected Sub btnCPUpdate_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception

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
    Protected Sub btnCheckPwd_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception

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
    Protected Sub btnUpdateUserProfile_Click(sender As Object, e As EventArgs)
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub lnkbtnAstReg_Click(sender As Object, e As EventArgs) Handles lnkbtnAstReg.Click
        Try
            sSession.Menu = "AssetRegister" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/AssetRegister.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAstReg_Click")
        End Try
    End Sub
    Private Sub lnkbtnDepComp_Click(sender As Object, e As EventArgs) Handles lnkbtnDepComp.Click
        Try
            sSession.Menu = "DepreciationComputation" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/DepreciationComputation.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDepComp_Click")
        End Try
    End Sub
    Private Sub lnkbtnAssetTransactionadd_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetTransactionadd.Click
        Try
            GetClickedURL("AssetTransactionAddition")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetTransactionadd_Click")
        End Try
    End Sub
    Private Sub lnkbtnAssetTransactionDel_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetTransactionDel.Click
        Try
            GetClickedURL("AssetTransactionDeletion")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetTransactionDel_Click")
        End Try
    End Sub
    Private Sub lnkbtnAssetTranFileUpload_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetTranFileUpload.Click
        Try
            GetClickedURL("AssetTransactionFileUpload")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetTranFileUpload_Click")
        End Try
    End Sub
    Private Sub lnkbtnAssetFileUploadView_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetFileUploadView.Click
        Try
            GetClickedURL("AssetTranFileUploadView")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetFileUploadView_Click")
        End Try
    End Sub

    Private Sub lnkbtnFXOPExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnFXOPExcel.Click
        Try
            sSession.Menu = "AssetOPeningBalExcelUplaod" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/AssetOpeningBalExcelUpload.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFXOPExcel_Click")
        End Try
    End Sub
    Private Sub lnkbtnFXOPExcelview_Click(sender As Object, e As EventArgs) Handles lnkbtnFXOPExcelview.Click
        Try
            sSession.Menu = "AssetOPeningBalExcelView" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/PhysicalRPTVerification.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFXOPExcelview_Click")
        End Try
    End Sub
    Private Sub lnkbtnDynamicReport_Click(sender As Object, e As EventArgs) Handles lnkbtnDynamicReport.Click
        Try
            sSession.Menu = "DynamicReport" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/FXADynamicReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDynamicReport_Click")
        End Try
    End Sub

    Private Sub lnkbtnAssetAddlnDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetAddlnDtls.Click
        Try
            GetClickedURL("AssetAdditionalDetails")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetAddlnDtls_Click")
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
    Protected Sub lnkbtnDigitalFilling_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDigitalFilling.Click
        Try
            sSession.Menu = "DigitalFilling" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            'Response.Redirect("~/HomePages/DigitalFilling.aspx", False)
            Response.Redirect("~/DigitalFilling/DigitalFilingDashboard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDigitalFilling_Click")
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

