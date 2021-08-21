Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Drawing
Partial Class Reports_AuxilaryReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Reports_AuxilaryReport"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Private objclsModulePermission As New clsModulePermission
    Dim ObjclsAuxilaryReport As New clsAuxilaryReport
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "AR")
                imgbtnReport.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                    If sFormButtons.Contains(",Report,") = True Then
                        imgbtnReport.Visible = True
                    End If
                End If
                BindReport()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub BindReport()
        Try
            ddlReports.DataSource = ObjclsAuxilaryReport.LoadReports(sSession.AccessCode)
            ddlReports.DataTextField = "Mas_Desc"
            ddlReports.DataValueField = "Mas_ID"
            ddlReports.DataBind()
            ddlReports.Items.Insert(0, "Select Types of Reports")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlReports_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReports.SelectedIndexChanged
        Try
            grdRPA.DataSource = Nothing
            grdRPA.DataBind()
            If ddlReports.SelectedIndex > 0 Then
                grdRPA.DataSource = ObjclsAuxilaryReport.GetReportDetails(sSession.AccessCode, sSession.AccessCodeID, ddlReports.SelectedValue)
                grdRPA.DataBind()
                grdRPA.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReports_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub grdRPA_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdRPA.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                For i As Integer = 0 To grdRPA.Rows.Count - 1
                    If (grdRPA.Rows(i).Cells(3).Text = "OWNED ASSETS:") OrElse (grdRPA.Rows(i).Cells(3).Text = "Owned assets leased out") OrElse (grdRPA.Rows(i).Cells(3).Text = "Owned assets (sub total-A)") OrElse (grdRPA.Rows(i).Cells(3).Text = "LEASED ASSETS:") OrElse (grdRPA.Rows(i).Cells(3).Text = "Asset taken on lease(Sub total-B)") OrElse (grdRPA.Rows(i).Cells(3).Text = "Total(A+B)") Then
                        grdRPA.Rows(i).Cells(3).ForeColor = Color.Brown
                        grdRPA.Rows(i).Cells(3).Font.Bold = True
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdRPA_RowDataBound")
        End Try
    End Sub
End Class
