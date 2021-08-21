Imports System
Imports System.Data
Imports BusinesLayer
Partial Class HomePages_Home
    Inherits System.Web.UI.Page
    Private sFormName As String = "HomePages_Home"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                If sSession.YearID = 0 Then
                    sSession.YearID = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    'sSession.YearName = objclsGeneralFunctions.Get2DigitFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    sSession.YearName = objclsGeneralFunctions.GetYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                End If
                Session("AllSession") = sSession
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
End Class
