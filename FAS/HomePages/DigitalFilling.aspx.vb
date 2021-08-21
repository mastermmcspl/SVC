Imports System
Imports System.Data
Imports BusinesLayer
Partial Class HomePages_DigitalFilling
    Inherits System.Web.UI.Page
    Private sFormName As String = "HomePages_DigitalFilling"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACePermission As New clsFASPermission
    Private Shared sSession As AllSession
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sModule As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'sModule = objclsGRACePermission.GetLoginUserModulePermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, 3)
                'If sModule = "False" Then
                '    Response.Redirect("~/Permissions/DigitalFilingPermission.aspx", False) 'Permissions/AuditPermissionModule
                '    Exit Sub
                'End If
                'LoadFinalcialYear(sSession.AccessCode)
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
            Try
                If sSession.YearID = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFinancialYear.SelectedValue = iYearID
                    Else
                        ddlFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlFinancialYear.SelectedValue = sSession.YearID
                End If
                sSession.YearID = ddlFinancialYear.SelectedValue
                sSession.YearName = ddlFinancialYear.SelectedItem.Text
                Session("AllSession") = sSession
            Catch ex As Exception
            End Try
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            sSession.YearID = ddlFinancialYear.SelectedValue
            sSession.YearName = ddlFinancialYear.SelectedItem.Text
            Session("AllSession") = sSession
            Response.Redirect("~/HomePages/DigitalFilling.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged")
        End Try
    End Sub
End Class
