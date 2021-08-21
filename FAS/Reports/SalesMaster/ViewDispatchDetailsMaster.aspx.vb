Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Reports_SalesMaster_ViewDispatchDetailsMaster
    Inherits System.Web.UI.Page
    Private Shared sSession As AllSession
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dt As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                dt = Session("DispatchMasterData")
                If IsNothing(dt) = False Then
                    showsetails(dt)
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub showsetails(ByVal dt As DataTable)
        Dim dtPrint As New DataTable
        Try

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/SalesMaster/RPTDispatchDetailsMaster.rdlc")
            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
