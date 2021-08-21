Imports System
Imports System.Data
Imports BusinesLayer
Imports System.IO

Partial Class Dashboard_TrailBalanceDashBoard
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Dashboard_TrailBalanceDashBoard"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsFASGeneral As New clsFASGeneral
    Private Shared sSession As AllSession
    Private objRpt As New clsReports
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dt As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                dt = objRpt.LoadTrialBalanceReport(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, 0, 0, 0)
                dgGLDashBoard.DataSource = dt
                dgGLDashBoard.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub dgGLDashBoard_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgGLDashBoard.RowCommand
        Dim lblID, lblGLCode As New Label : Dim lnkDescription As New LinkButton
        Try
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblGLCode = DirectCast(clickedRow.FindControl("lblGLCode"), Label)
                lnkDescription = DirectCast(clickedRow.FindControl("lnkDescription"), LinkButton)


                GVDetails.DataSource = objRpt.LoadTrialBalanceReportDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, 0, 0, 0, lblID.Text, lblGLCode.Text, lnkDescription.Text)
                GVDetails.DataBind()

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGLDashBoard_RowCommand")
        End Try
    End Sub
    Private Sub dgGLDashBoard_PreRender(sender As Object, e As EventArgs) Handles dgGLDashBoard.PreRender
        Dim dt As New DataTable
        Try
            If dgGLDashBoard.Rows.Count > 0 Then
                dgGLDashBoard.UseAccessibleHeader = True
                dgGLDashBoard.HeaderRow.TableSection = TableRowSection.TableHeader
                dgGLDashBoard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGLDashBoard_PreRender")
        End Try
    End Sub
    Private Sub GVDetails_PreRender(sender As Object, e As EventArgs) Handles GVDetails.PreRender
        Dim dt As New DataTable
        Try
            If GVDetails.Rows.Count > 0 Then
                GVDetails.UseAccessibleHeader = True
                GVDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                GVDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVDetails_PreRender")
        End Try
    End Sub
    Private Sub GVDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GVDetails.RowCommand
        Dim lblID, lblGLCode As New Label : Dim lnkDescription As New LinkButton
        Try
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblGLCode = DirectCast(clickedRow.FindControl("lblGLCode"), Label)
                lnkDescription = DirectCast(clickedRow.FindControl("lnkDescription"), LinkButton)

                GVTransactions.DataSource = objRpt.LoadTrialBalanceTransactions(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, 0, 0, 0, 0, lblID.Text, lblGLCode.Text, lnkDescription.Text)
                GVTransactions.DataBind()

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalT').modal('show');", True)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GVTransactions_PreRender(sender As Object, e As EventArgs) Handles GVTransactions.PreRender
        Dim dt As New DataTable
        Try
            If GVTransactions.Rows.Count > 0 Then
                GVTransactions.UseAccessibleHeader = True
                GVTransactions.HeaderRow.TableSection = TableRowSection.TableHeader
                GVTransactions.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVTransactions_PreRender")
        End Try
    End Sub
    Private Sub dgGLDashBoard_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgGLDashBoard.RowDataBound
        Dim lblGLCode As New Label
        Dim lnkDescription As New LinkButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblGLCode = e.Row.FindControl("lblGLCode")
                lnkDescription = e.Row.FindControl("lnkDescription")

                If lblGLCode.Text = "" Then
                    lnkDescription.Text = "<b>" & lnkDescription.Text & "</b>"
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGLDashBoard_RowDataBound")
        End Try
    End Sub
    Private Sub GVDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GVDetails.RowDataBound
        Dim lblGLCode As New Label
        Dim lnkDescription As New LinkButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblGLCode = e.Row.FindControl("lblGLCode")
                lnkDescription = e.Row.FindControl("lnkDescription")

                If lblGLCode.Text = "" Then
                    lnkDescription.Text = "<b>" & lnkDescription.Text & "</b>"
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVDetails_RowDataBound")
        End Try
    End Sub
    Private Sub GVTransactions_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GVTransactions.RowDataBound
        Dim lblGLCode As New Label
        Dim lnkDescription As New LinkButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblGLCode = e.Row.FindControl("lblGLCode")
                lnkDescription = e.Row.FindControl("lnkDescription")

                If lblGLCode.Text = "" Then
                    lnkDescription.Text = "<b>" & lnkDescription.Text & "</b>"
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVTransactions_RowDataBound")
        End Try
    End Sub
End Class
