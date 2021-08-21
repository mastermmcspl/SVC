Imports System
Imports System.Data
Imports BusinesLayer
Partial Class HomePages_LogisticsMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "HomePages_RemoteData"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASPermission As New clsFASPermission
    Private Shared sSession As AllSession
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sModule As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sModule = objclsFASPermission.GetLoginUserModulePermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, 1)
                If sModule = "False" Then
                    ' Response.Redirect("~/Permissions/SysAdminPermissionModule.aspx", False) 'Permissions/SysAdminPermissionModule
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
End Class
