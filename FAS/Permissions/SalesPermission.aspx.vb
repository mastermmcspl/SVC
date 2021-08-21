Imports System
Imports System.Data
Imports BusinesLayer
Partial Class Permissions_SalesPermission
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Permissions_SalesPermission.aspx"
    Private Shared sSession As AllSession
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            If IsPostBack = False Then
                sSession = Session("AllSession")
                lblMsg.Text = sSession.UserFullName & " does not have permission to view this page."
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
End Class
