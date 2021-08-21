Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Dashboard_AccountsDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "Dashboard/PartyDashboard"
    Dim objGen As New clsFASGeneral
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsModulePermission As New clsModulePermission
    Dim objDashBoard As New clsAccountDashBoard
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                BinDashboardDetails()
                sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PWD")
                'imgbtnReport.Visible = False
                If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                    Response.Redirect("~/Permissions/AccountPermission.aspx", False) 'Permissions/PurchasePermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",View,") = True Then
                    End If
                End If
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    Public Sub BinDashboardDetails()
        Try
            dgDashBoard.DataSource = objDashBoard.LoadDashboardDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            dgDashBoard.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dgDashBoard_PreRender(sender As Object, e As EventArgs) Handles dgDashBoard.PreRender
        Dim dt As New DataTable
        Try
            If dgDashBoard.Rows.Count > 0 Then
                dgDashBoard.UseAccessibleHeader = True
                dgDashBoard.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDashBoard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDashBoard_PreRender")
        End Try
    End Sub

    Private Sub dgDashBoard_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDashBoard.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If (e.Row.Cells(0).Text <> "") And (e.Row.Cells(0).Text <> "&nbsp;") Then

                    If e.Row.Cells(0).Text = "TOTAL" Then

                        e.Row.Cells(0).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(0).Font.Bold = True

                        e.Row.Cells(1).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(1).Font.Bold = True

                        e.Row.Cells(2).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(2).Font.Bold = True

                        e.Row.Cells(3).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(3).Font.Bold = True

                        e.Row.Cells(4).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(4).Font.Bold = True

                        e.Row.Cells(5).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(5).Font.Bold = True

                        e.Row.Cells(6).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(6).Font.Bold = True

                        e.Row.Cells(7).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(7).Font.Bold = True

                        e.Row.Cells(8).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(8).Font.Bold = True

                        e.Row.Cells(9).BackColor = Drawing.ColorTranslator.FromHtml("#ffdde3")
                        e.Row.Cells(9).Font.Bold = True

                    Else
                        e.Row.Cells(0).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(0).Font.Bold = True

                        e.Row.Cells(1).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(1).Font.Bold = True

                        e.Row.Cells(2).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(2).Font.Bold = True

                        e.Row.Cells(3).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(3).Font.Bold = True

                        e.Row.Cells(4).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(4).Font.Bold = True

                        e.Row.Cells(5).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(5).Font.Bold = True

                        e.Row.Cells(6).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(6).Font.Bold = True

                        e.Row.Cells(7).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(7).Font.Bold = True

                        e.Row.Cells(8).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(8).Font.Bold = True

                        e.Row.Cells(9).BackColor = Drawing.ColorTranslator.FromHtml("#afeeee")
                        e.Row.Cells(9).Font.Bold = True
                    End If


                End If
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDashBoard_RowDataBound")
        End Try
    End Sub
End Class
