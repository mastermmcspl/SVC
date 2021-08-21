

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Imports DatabaseLayer
Partial Class Reports_Purchase_PurchaseSizeWise
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Report/Viewer/PuchaseOrderHR.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objPHR As New ClsPurchaseOrderHR
    Dim objDB As New DBHelper
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iOrderID As Integer
        Dim dt As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                iOrderID = Request.QueryString("ExistingOrder")
                If iOrderID > 0 Then
                    LoadPUrchaseOrderHR(iOrderID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    Private Sub LoadPUrchaseOrderHR(ByVal iOrderID As Integer)
        Dim dt As New DataTable, dtComp As New DataTable, dtVendor As New DataTable
        Dim totAmount As Double, totavat As Double, totalCst As Double
        Dim i As Integer = 0, j As Integer = 0
        Try
            ReportViewer1.Reset()
            dt = objPHR.GetPurchaseORderHR(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/purchase/RptPurchaseOrderHR.rdlc")
            For i = 0 To dt.Rows.Count - 1
                totAmount = totAmount + dt.Rows(i)("POD_RateAmount")
                totavat = totavat + dt.Rows(i)("POD_VATAMount")
                totalCst = totalCst + dt.Rows(i)("POD_CSTAmount")
            Next

            If totAmount <> 0 Then
                totAmount = totAmount + totavat + totalCst
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", objPHR.NumberToWord(String.Format("{0:0.00}", totAmount)) & " Only")}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            Else
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", "0")}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            End If

            dtComp = objPHR.GetCompanyMasterTemplete(sSession.AccessCode, sSession.AccessCodeID)
            Dim CCST As ReportParameter() = New ReportParameter() {New ReportParameter("CCST", "<B>" & "CST : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CCST)
            Dim CVAT As ReportParameter() = New ReportParameter() {New ReportParameter("CVAT", "<B>" & "VAT : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CVAT)
            Dim CTIN As ReportParameter() = New ReportParameter() {New ReportParameter("CTIN", "<B>" & "TIN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CTIN)
            Dim CTAN As ReportParameter() = New ReportParameter() {New ReportParameter("CTAN", "<B>" & "TAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CTAN)
            Dim CPAN As ReportParameter() = New ReportParameter() {New ReportParameter("CPAN", "<B>" & "PAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(CPAN)
            If dtComp.Rows.Count > 0 Then
                For i = 0 To dtComp.Rows.Count - 1
                    Select Case dtComp.Rows(i)("CMP_Desc").ToString()
                        Case "CST"
                            CCST = {New ReportParameter("CCST", "<B>" & "CST : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(CCST)
                        Case "VAT"
                            CVAT = {New ReportParameter("CVAT", "<B>" & "VAT : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(CVAT)
                        Case "TIN"
                            CTIN = {New ReportParameter("CTIN", "<B>" & "TIN : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(CTIN)
                        Case "TAN"
                            CTAN = {New ReportParameter("CTAN", "<B>" & "TAN : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(CTAN)
                        Case "PAN"
                            CPAN = {New ReportParameter("CPAN", "<B>" & "PAN : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(CPAN)
                    End Select
                Next
            End If

            dtVendor = objPHR.GetVendorTemplete(sSession.AccessCode, sSession.AccessCodeID, iOrderID)
            Dim VCST As ReportParameter() = New ReportParameter() {New ReportParameter("VCST", "<B>" & "CST : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(VCST)
            Dim VVAT As ReportParameter() = New ReportParameter() {New ReportParameter("VVAT", "<B>" & "VAT : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(VVAT)
            Dim VTIN As ReportParameter() = New ReportParameter() {New ReportParameter("VTIN", "<B>" & "TIN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(VTIN)
            Dim VTAN As ReportParameter() = New ReportParameter() {New ReportParameter("VTAN", "<B>" & "TAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(VTAN)
            Dim VPAN As ReportParameter() = New ReportParameter() {New ReportParameter("VPAN", "<B>" & "PAN : " & "</B>" & "")}
            ReportViewer1.LocalReport.SetParameters(VPAN)

            If dtVendor.Rows.Count > 0 Then
                For j = 0 To dtVendor.Rows.Count - 1
                    Select Case dtVendor.Rows(j)("CST_Description").ToString()
                        Case "CST"
                            VCST = {New ReportParameter("VCST", "<B>" & "CST : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(VCST)
                        Case "VAT"
                            VVAT = {New ReportParameter("VVAT", "<B>" & "VAT : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(VVAT)
                        Case "TIN"
                            VTIN = {New ReportParameter("VTIN", "<B>" & "TIN : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(VTIN)
                        Case "TAN"
                            VTAN = {New ReportParameter("VTAN", "<B>" & "TAN : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(VTAN)
                        Case "PAN"
                            VPAN = {New ReportParameter("VPAN", "<B>" & "PAN : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
                            ReportViewer1.LocalReport.SetParameters(VPAN)
                    End Select
                Next
            End If

            'Terms and Condtions
            If objDB.SQLCheckForRecord(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21") Then
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", objDB.SQLGetDescription(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21"))}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            Else
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", " ")}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            End If
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPUrchaseOrderHR")
        End Try
    End Sub
End Class

