Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Partial Class Master
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
            liSettings.Attributes.Remove("class") : liSuperMaster.Attributes.Remove("class") : liLogistics.Attributes.Remove("class") : liInventory.Attributes.Remove("class")
            liLinkage.Attributes.Remove("class") : liCurrencyMaster.Attributes.Remove("class")

            lnkbtnAppConfiguration.Font.Italic = False : lnkbtnAppConfiguration.Font.Bold = False
            lnkbtnHolidayMaster.Font.Italic = False : lnkbtnHolidayMaster.Font.Bold = False
            lnkbtnPermission.Font.Italic = False : lnkbtnPermission.Font.Bold = False
            lnkbtnOrgStructure.Font.Italic = False : lnkbtnOrgStructure.Font.Bold = False
            lnkbtnCompanyMaster.Font.Italic = False : lnkbtnCompanyMaster.Font.Bold = False
            lnkbtnEmployeeMaster.Font.Italic = False : lnkbtnEmployeeMaster.Font.Bold = False
            lnkbtnCustomerMaster.Font.Italic = False : lnkbtnCustomerMaster.Font.Bold = False
            lnkbtnSupplierMaster.Font.Italic = False : lnkbtnSupplierMaster.Font.Bold = False
            lnkbtnGeneralMasters.Font.Italic = False : lnkbtnGeneralMasters.Font.Bold = False
            lnkbtnChartofAccounts.Font.Italic = False : lnkbtnChartofAccounts.Font.Bold = False
            lnkbtnSettings.Font.Italic = False : lnkbtnSettings.Font.Bold = False
            lnkbtnExcelUpload.Font.Italic = False : lnkbtnExcelUpload.Font.Bold = False
            lnkbtnInventoryMaster.Font.Italic = False : lnkbtnInventoryMaster.Font.Bold = False
            lnkbtnInventoryMasterDetails.Font.Italic = False : lnkbtnInventoryMasterDetails.Font.Bold = False
            lnkPSupdate.Font.Italic = False : lnkPSupdate.Font.Bold = False
            lnkPReport.Font.Italic = False : lnkPReport.Font.Bold = False
            lnkStockAdjustment.Font.Italic = False : lnkStockAdjustment.Font.Bold = False
            lnkStockTranfer.Font.Italic = False : lnkStockTranfer.Font.Bold = False
            lnkbtnInvLinkageMaster.Font.Italic = False : lnkbtnInvLinkageMaster.Font.Bold = False
            lnkbtnScheduleLinkageMaster.Font.Italic = False : lnkbtnScheduleLinkageMaster.Font.Bold = False
            lnkbtnPrintSettings.Font.Italic = False : lnkbtnPrintSettings.Font.Bold = False
            lnkbtnAccSettings.Font.Italic = False : lnkbtnAccSettings.Font.Bold = False
            lnkbtnBankCurrency.Font.Italic = False : lnkbtnBankCurrency.Font.Bold = False
            lnkbtnCurrencyMaster.Font.Italic = False : lnkbtnCurrencyMaster.Font.Bold = False
            lnkbtnForeignCurrencyAgents.Font.Italic = False : lnkbtnForeignCurrencyAgents.Font.Bold = False
            lnkAgencyCurrencyMaster.Font.Italic = False : lnkAgencyCurrencyMaster.Font.Bold = False

            lnkbtnVehicleMaster.Font.Italic = False : lnkbtnVehicleMaster.Font.Bold = False
            lnkbtnPetrolMaster.Font.Italic = False : lnkbtnPetrolMaster.Font.Bold = False
            lnkbtnDriverMaster.Font.Italic = False : lnkbtnDriverMaster.Font.Bold = False
            lnkbtnDriverAllowanceMaster.Font.Italic = False : lnkbtnDriverAllowanceMaster.Font.Bold = False
            lnkbtnRouteMaster.Font.Italic = False : lnkbtnRouteMaster.Font.Bold = False

            If sSession.SubMenu = "Settings" Then
                liSettings.Attributes.Add("class", "open")
                If sSession.Form = "ApplicationConfiguration" Then
                    lnkbtnAppConfiguration.Font.Italic = True : lnkbtnAppConfiguration.Font.Bold = True
                ElseIf sSession.Form = "CompanyMaster" Then
                    lnkbtnCompanyMaster.Font.Italic = True : lnkbtnCompanyMaster.Font.Bold = True
                ElseIf sSession.Form = "HolidayMaster" Then
                    lnkbtnHolidayMaster.Font.Italic = True : lnkbtnHolidayMaster.Font.Bold = True
                ElseIf sSession.Form = "OrgStructure" Then
                    lnkbtnOrgStructure.Font.Italic = True : lnkbtnOrgStructure.Font.Bold = True

                ElseIf sSession.Form = "PrintSettings" Then
                    lnkbtnPrintSettings.Font.Italic = True : lnkbtnPrintSettings.Font.Bold = True
                ElseIf sSession.Form = "AccSettings" Then
                    lnkbtnAccSettings.Enabled = False
                    lnkbtnAccSettings.Font.Italic = True : lnkbtnAccSettings.Font.Bold = True
                End If

            ElseIf sSession.SubMenu = "SuperMaster" Then
                liSuperMaster.Attributes.Add("class", "open")
                If sSession.Form = "GeneralMaster" Then
                    lnkbtnGeneralMasters.Font.Italic = True : lnkbtnGeneralMasters.Font.Bold = True
                ElseIf sSession.Form = "CustomerMaster" Then
                    lnkbtnCustomerMaster.Font.Italic = True : lnkbtnCustomerMaster.Font.Bold = True
                ElseIf sSession.Form = "Supplier" Then
                    lnkbtnSupplierMaster.Font.Italic = True : lnkbtnSupplierMaster.Font.Bold = True
                ElseIf sSession.Form = "EmployeeMaster" Then
                    lnkbtnEmployeeMaster.Font.Italic = True : lnkbtnEmployeeMaster.Font.Bold = True
                ElseIf sSession.Form = "AppSettings" Then
                    lnkbtnSettings.Font.Italic = True : lnkbtnSettings.Font.Bold = True
                ElseIf sSession.Form = "Permission" Then
                    lnkbtnPermission.Font.Italic = True : lnkbtnPermission.Font.Bold = True
                ElseIf sSession.Form = "ChartofAccounts" Then
                    lnkbtnChartofAccounts.Font.Italic = True : lnkbtnChartofAccounts.Font.Bold = True
                ElseIf sSession.Form = "ExcelUpload" Then
                    lnkbtnExcelUpload.Font.Italic = True : lnkbtnExcelUpload.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "LogisticsMaster" Then
                liLogistics.Attributes.Add("class", "open")
                If sSession.Form = "VehicleMaster" Then
                    lnkbtnVehicleMaster.Font.Italic = True : lnkbtnVehicleMaster.Font.Bold = True
                ElseIf sSession.Form = "DieselPumpMaster" Then
                    lnkbtnPetrolMaster.Font.Italic = True : lnkbtnPetrolMaster.Font.Bold = True
                ElseIf sSession.Form = "DriverMater" Then
                    lnkbtnDriverMaster.Font.Italic = True : lnkbtnDriverMaster.Font.Bold = True
                ElseIf sSession.Form = "DriverAllowanceMaster" Then
                    lnkbtnDriverAllowanceMaster.Font.Italic = True : lnkbtnDriverAllowanceMaster.Font.Bold = True
                ElseIf sSession.Form = "RouteMaster" Then
                    lnkbtnRouteMaster.Font.Italic = True : lnkbtnRouteMaster.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "Inventory" Then
                liInventory.Attributes.Add("class", "open")
                If sSession.Form = "InventoryMaster" Then
                    lnkbtnInventoryMaster.Font.Italic = True : lnkbtnInventoryMaster.Font.Bold = True
                ElseIf sSession.Form = "InventoryMasterDetails" Then
                    lnkbtnInventoryMasterDetails.Font.Italic = True : lnkbtnInventoryMasterDetails.Font.Bold = True
                ElseIf sSession.Form = "PhysicalUpdate" Then
                    lnkPSupdate.Font.Italic = True : lnkPSupdate.Font.Bold = True
                ElseIf sSession.Form = "PhysicalReport" Then
                    lnkPReport.Font.Italic = True : lnkPReport.Font.Bold = True
                ElseIf sSession.Form = "StockAdjustment" Then
                    lnkStockAdjustment.Font.Italic = True : lnkStockAdjustment.Font.Bold = True
                ElseIf sSession.Form = "StockTransfer" Then
                    lnkStockTranfer.Font.Italic = True : lnkStockTranfer.Font.Bold = True
                End If

            ElseIf sSession.SubMenu = "LinkageMaster" Then
                liLinkage.Attributes.Add("class", "open")
                If sSession.Form = "InventoryLinkageMaster" Then
                    lnkbtnInvLinkageMaster.Font.Italic = True : lnkbtnInvLinkageMaster.Font.Bold = True
                ElseIf sSession.Form = "ScheduleLinkageMaster" Then
                    lnkbtnScheduleLinkageMaster.Font.Italic = True : lnkbtnScheduleLinkageMaster.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "CurrencyMaster" Then
                liCurrencyMaster.Attributes.Add("class", "open")
                If sSession.Form = "FCurrencyAgents" Then
                    lnkbtnForeignCurrencyAgents.Font.Italic = True : lnkbtnForeignCurrencyAgents.Font.Bold = True
                ElseIf sSession.Form = "AgentsCurrencyMaster" Then
                    lnkAgencyCurrencyMaster.Font.Italic = True : lnkAgencyCurrencyMaster.Font.Bold = True
                ElseIf sSession.Form = "BankCurrency" Then
                    lnkbtnBankCurrency.Font.Italic = True : lnkbtnBankCurrency.Font.Bold = True
                ElseIf sSession.Form = "CurrencyMaster" Then
                    lnkbtnCurrencyMaster.Font.Italic = True : lnkbtnCurrencyMaster.Font.Bold = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetClickedURL(ByVal sForm As String)
        Dim flag As String
        Dim objPO As New clsPurchaseOrder
        Try
            If sForm = "ApplicationConfiguration" Then
                sSession.SubMenu = "Settings" : sSession.Form = "ApplicationConfiguration"
                Response.Redirect("~/Masters/ApplicationConfiguration.aspx", False)
            ElseIf sForm = "HolidayMaster" Then
                sSession.SubMenu = "Settings" : sSession.Form = "HolidayMaster"
                Response.Redirect("~/Masters/HolidayMaster.aspx", False)
            ElseIf sForm = "Permission" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "Permission"
                Response.Redirect("~/Permissions/ModulePermission.aspx", False)
            ElseIf sForm = "OrgStructure" Then
                sSession.SubMenu = "Settings" : sSession.Form = "OrgStructure"
                Response.Redirect("~/Masters/OrganizationStructure.aspx", False)
            ElseIf sForm = "AppSettings" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "AppSettings"
                Response.Redirect("~/Masters/ApplicationSettings.aspx", False)
            ElseIf sForm = "GeneralMaster" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "GeneralMaster"
                Response.Redirect("~/Masters/GeneralMaster.aspx", False)
            ElseIf sForm = "CompanyMaster" Then
                sSession.SubMenu = "Settings" : sSession.Form = "CompanyMaster"
                Response.Redirect("~/Masters/CompanyMaster.aspx", False)
            ElseIf sForm = "CustomerMaster" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "CustomerMaster"
                Response.Redirect("~/Masters/CustomerMaster.aspx", False)
            ElseIf sForm = "Supplier" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "Supplier"
                Response.Redirect("~/Masters/SupplierMaster.aspx", False)
            ElseIf sForm = "EmployeeMaster" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "EmployeeMaster"
                Response.Redirect("~/Masters/EmployeeMaster.aspx", False)
            ElseIf sForm = "ChartofAccounts" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "ChartofAccounts"
                Response.Redirect("~/Masters/ChartofAccounts.aspx", False)
            ElseIf sForm = "ExcelUpload" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "ExcelUpload"
                Response.Redirect("~/Masters/ExcelUpload.aspx", False)
            ElseIf sForm = "VehicleMaster" Then
                sSession.SubMenu = "LogisticsMaster" : sSession.Form = "VehicleMaster"
                Response.Redirect("~/Masters/FrmLgstVehicleDashBoard.aspx", False)
            ElseIf sForm = "DieselPumpMaster" Then
                sSession.SubMenu = "LogisticsMaster" : sSession.Form = "DieselPumpMaster"
                Response.Redirect("~/Masters/FrmLgstDieselMasterDashboard.aspx", False)
            ElseIf sForm = "DriverMater" Then
                sSession.SubMenu = "LogisticsMaster" : sSession.Form = "DriverMater"
                Response.Redirect("~/Masters/FrmLgstDriverDashBoard.aspx", False)
            ElseIf sForm = "DriverAllowanceMaster" Then
                sSession.SubMenu = "LogisticsMaster" : sSession.Form = "DriverAllowanceMaster"
                Response.Redirect("~/Masters/FrmLgstDriverAllowanceMaster.aspx", False)
            ElseIf sForm = "RouteMaster" Then
                sSession.SubMenu = "LogisticsMaster" : sSession.Form = "RouteMaster"
                Response.Redirect("~/Masters/FrmLgstRouteMasterDashboard.aspx", False)
            ElseIf sForm = "InventoryMaster" Then
                sSession.SubMenu = "Inventory" : sSession.Form = "InventoryMaster"
                Response.Redirect("~/Masters/InventoryMaster.aspx", False)
            ElseIf sForm = "InventoryMasterDetails" Then
                sSession.SubMenu = "Inventory" : sSession.Form = "InventoryMasterDetails"
                Response.Redirect("~/Masters/InventoryMasterDetails.aspx", False)
            ElseIf sForm = "InventoryLinkageMaster" Then
                sSession.SubMenu = "LinkageMaster" : sSession.Form = "InventoryLinkageMaster"
                Response.Redirect("~/Masters/InventoryLinkageMaster.aspx", False)
            ElseIf sForm = "ScheduleLinkageMaster" Then
                sSession.SubMenu = "LinkageMaster" : sSession.Form = "ScheduleLinkageMaster"
                Response.Redirect("~/Masters/ScheduleLinkageMaster.aspx", False)
            ElseIf sForm = "PhysicalReport" Then
                flag = objPO.GetPrintFlagValue(sSession.AccessCode, sSession.AccessCodeID)
                sSession.SubMenu = "Inventory" : sSession.Form = "PhysicalReport"
                If (flag = 1) Then
                    Response.Redirect("~/Reports/PhysicalReport.aspx", False)
                Else
                    Response.Redirect("~/Reports/PhysicalReportItemWise.aspx", False)
                End If
            ElseIf sForm = "PhysicalUpdate" Then
                sSession.SubMenu = "Inventory" : sSession.Form = "PhysicalUpdate"
                Response.Redirect("~/Reports/PhysicalUpdate.aspx", False)
            ElseIf sForm = "YearMasterDetails" Then
                sSession.SubMenu = "YearMasterDetails" : sSession.Form = "YearMasterDetails"
                Response.Redirect("~/Masters/YearMasterDetails.aspx", False)
            ElseIf sForm = "UserUpload" Then
                sSession.SubMenu = "UserUpload" : sSession.Form = "UserUpload"
                Response.Redirect("~/Masters/UsersUpload.aspx", False)
            ElseIf sForm = "PrintSettings" Then
                sSession.SubMenu = "Settings" : sSession.Form = "PrintSettings"
                Response.Redirect("~/Masters/PrintSettings.aspx", False)
            ElseIf sForm = "AccSettings" Then
                sSession.SubMenu = "AccSettings" : sSession.Form = "AccSettings"
                Response.Redirect("~/Masters/AccountSettings.aspx", False)
            ElseIf sForm = "Inventory" Then
                sSession.SubMenu = "PhysicalUpload" : sSession.Form = "PhysicalUpload"
                Response.Redirect("~/Upload/PhysicalUpload.aspx", False)
            ElseIf sForm = "InventoryUpload" Then
                sSession.SubMenu = "InventoryUpload" : sSession.Form = "InventoryUpload"
                Response.Redirect("~/Upload/InventoryUpload.aspx", False)
            ElseIf sForm = "StockAdjustment" Then
                sSession.SubMenu = "Inventory" : sSession.Form = "StockAdjustment"
                Response.Redirect("~/Inventory/StockAdgetment.aspx", False)
            ElseIf sForm = "UserUpload" Then
                sSession.SubMenu = "UserUpload" : sSession.Form = "UserUpload"
                Response.Redirect("~/Masters/UsersUpload.aspx", False)
            ElseIf sForm = "StockTransfer" Then
                sSession.SubMenu = "Inventory" : sSession.Form = "StockTransfer"
                Response.Redirect("~/Inventory/StockTransfer.aspx", False)
            ElseIf sForm = "Test" Then
                sSession.SubMenu = "Test" : sSession.Form = "Test"
                Response.Redirect("~/Masters/SupplierMaster.aspx", False)
            ElseIf sForm = "AssetMaster" Then
                sSession.SubMenu = "OtherMasters" : sSession.Form = "AssetMaster"
                Response.Redirect("~/Masters/AssetMasters.aspx", False)
            ElseIf sForm = "FCurrencyAgents" Then
                sSession.SubMenu = "CurrencyMaster" : sSession.Form = "FCurrencyAgents"
                Response.Redirect("~/Masters/AgentsForeignExchange.aspx", False)
            ElseIf sForm = "AgentsCurrencyMaster" Then
                sSession.SubMenu = "CurrencyMaster" : sSession.Form = "AgentsCurrencyMaster"
                Response.Redirect("~/Masters/AgencyCurrency.aspx", False)
            ElseIf sForm = "BankCurrency" Then
                sSession.SubMenu = "CurrencyMaster" : sSession.Form = "BankCurrency"
                Response.Redirect("~/Masters/BankCurrency.aspx", False)
            ElseIf sForm = "CurrencyMaster" Then
                sSession.SubMenu = "CurrencyMaster" : sSession.Form = "CurrencyMaster"
                Response.Redirect("~/Masters/CurrencyMasterDashboard.aspx", False)
            End If
            Session("AllSession") = sSession
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub lnkbtnAccSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAccSettings.Click
        Try
            GetClickedURL("AccSettings")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAccSettings_Click")
        End Try
    End Sub
    Protected Sub lnkbtnAppConfiguration_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAppConfiguration.Click
        Try
            GetClickedURL("ApplicationConfiguration")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAppConfiguration_Click")
        End Try
    End Sub
    Protected Sub lnkbtnHolidayMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnHolidayMaster.Click
        Try
            GetClickedURL("HolidayMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHolidayMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPermission.Click
        Try
            GetClickedURL("Permission")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPermission_Click")
        End Try
    End Sub
    Protected Sub lnkbtnOrgStructure_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnOrgStructure.Click
        Try
            GetClickedURL("OrgStructure")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnOrgStructure_Click")
        End Try
    End Sub
    Protected Sub lnkbtnSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSettings.Click
        Try
            GetClickedURL("AppSettings")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSettings_Click")
        End Try
    End Sub
    Protected Sub lnkbtnCompanyMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnCompanyMaster.Click
        Try
            GetClickedURL("CompanyMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCompanyMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnEmployeeMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnEmployeeMaster.Click
        Try
            GetClickedURL("EmployeeMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmployeeMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnCurrencyMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnCurrencyMaster.Click
        Try
            GetClickedURL("CurrencyMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " lnkbtnCurrencyMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnBankCurrency_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnBankCurrency.Click
        Try
            GetClickedURL("BankCurrency")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " lnkbtnBankCurrency_Click")
        End Try
    End Sub
    Protected Sub lnkbtnForeignCurrencyAgents_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnForeignCurrencyAgents.Click
        Try
            GetClickedURL("FCurrencyAgents")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " lnkbtnForeignCurrencyAgents_Click")
        End Try
    End Sub
    Protected Sub lnkAgencyCurrencyMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAgencyCurrencyMaster.Click
        Try
            GetClickedURL("AgentsCurrencyMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " lnkAgencyCurrencyMaster_Click")
        End Try
    End Sub

    'Protected Sub lnkbtnCustomerMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnCustomerMaster.Click
    '    Try
    '        GetClickedURL("CustomerMaster")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerMaster_Click")
    '    End Try
    'End Sub

    Protected Sub lnkbtnGeneralMasters_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnGeneralMasters.Click
        Try
            GetClickedURL("GeneralMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnGeneralMasters_Click")
        End Try
    End Sub

    Protected Sub lnkbtnChartofAccounts_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnChartofAccounts.Click
        Try
            GetClickedURL("ChartofAccounts")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnChartofAccounts_Click")
        End Try
    End Sub

    Protected Sub lnkbtnExcelUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnExcelUpload.Click
        Try
            GetClickedURL("ExcelUpload")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcelUpload_Click")
        End Try
    End Sub

    Protected Sub lnkbtnInventoryMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnInventoryMaster.Click
        Try
            GetClickedURL("InventoryMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInventoryMaster_Click")
        End Try
    End Sub

    Protected Sub lnkbtnInventoryMasterDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnInventoryMasterDetails.Click
        Try
            GetClickedURL("InventoryMasterDetails")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInventoryMasterDetails_Click")
        End Try
    End Sub
    Protected Sub lnkbtnInvLinkageMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnInvLinkageMaster.Click
        Try
            GetClickedURL("InventoryLinkageMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInvLinkageMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnScheduleLinkageMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnScheduleLinkageMaster.Click
        Try
            GetClickedURL("ScheduleLinkageMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleLinkageMaster_Click")
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

    Private Sub lnkPReport_Click(sender As Object, e As EventArgs) Handles lnkPReport.Click
        Try
            GetClickedURL("PhysicalReport")
        Catch ex As Exception
            Throw
        End Try

    End Sub

    Private Sub lnkPSupdate_Click(sender As Object, e As EventArgs) Handles lnkPSupdate.Click
        Try
            GetClickedURL("PhysicalUpdate")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lnkbtnSupplierMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnSupplierMaster.Click
        Try
            GetClickedURL("Supplier")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSupplierMaster_Click")
        End Try
    End Sub

    Private Sub lnkbtnCustomerMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnCustomerMaster.Click
        Try
            GetClickedURL("CustomerMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerMaster_Click")
        End Try
    End Sub

    'Private Sub lnkbtnYearMasterDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnYearMasterDetails.Click

    '    Try
    '        GetClickedURL("YearMasterDetails")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPermission_Click")
    '    End Try
    'End Sub

    'Protected Sub lnkbtnUserUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnUserUpload.Click
    '    Try
    '        GetClickedURL("UserUpload")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnUserUpload_Click")
    '    End Try
    'End Sub
    Protected Sub lnkbtnPrintSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPrintSettings.Click
        Try
            GetClickedURL("PrintSettings")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPrintSettings_Click")
        End Try
    End Sub

    'Private Sub lnkPhysicalUpload_Click(sender As Object, e As EventArgs) Handles lnkPhysicalUpload.Click
    '    Try
    '        GetClickedURL("PhysicalUpload")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPermission_Click")
    '    End Try
    'End Sub

    'Private Sub lnkInventoryUpload_Click(sender As Object, e As EventArgs) Handles lnkInventoryUpload.Click
    '    Try
    '        GetClickedURL("InventoryUpload")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPermission_Click")
    '    End Try
    'End Sub

    Private Sub lnkStockAdjustment_Click(sender As Object, e As EventArgs) Handles lnkStockAdjustment.Click
        Try
            GetClickedURL("StockAdjustment")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lnkStockTranfer_Click(sender As Object, e As EventArgs) Handles lnkStockTranfer.Click
        Try
            GetClickedURL("StockTransfer")
        Catch ex As Exception
            Throw
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
    Private Sub lnkbtnAssetMasters_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetMasters.Click
        Try
            GetClickedURL("AssetMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetMasters_Click")
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
    Protected Sub lnkbtnVehicleMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnVehicleMaster.Click
        Try
            GetClickedURL("VehicleMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnVehicleMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnPetrolMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPetrolMaster.Click
        Try
            GetClickedURL("DieselPumpMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPetrolMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnDriverMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDriverMaster.Click
        Try
            GetClickedURL("DriverMater")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDriverMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnDriverAllowanceMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDriverAllowanceMaster.Click
        Try
            GetClickedURL("DriverAllowanceMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDriverAllowanceMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnRouteMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRouteMaster.Click
        Try
            GetClickedURL("RouteMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRouteMaster_Click")
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