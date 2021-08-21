Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Data.SqlClient
Partial Class Reports_SalesReturnReport
    Inherits System.Web.UI.Page
    Private sFormName As String = "Reports/SalesReturnReport.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objclsSaleReturn As New clsSaleReturn
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iMasterID As Integer = 0
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                If Request.QueryString("MasterID") IsNot Nothing Then
                    iMasterID = objGen.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterID")))
                    LoadReports(iMasterID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadReports(ByVal iMasterID As Integer)
        Dim dt As New DataTable, dtSR As New DataTable, dtComp As New DataTable
        Try
            ReportViewer1.Reset()
            dt = objclsSaleReturn.LoadSalesReturnMasterDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iMasterID)
            If dt.Rows.Count > 0 Then
                dtSR = objclsSaleReturn.LoadSalesReturn(sSession.AccessCode, sSession.AccessCodeID, iMasterID)
                If dtSR.Rows.Count = 0 Then
                    lblSalesValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalSalesValidation').modal('show');", True)
                    Exit Sub
                End If
                Dim rds As New ReportDataSource("DataSet1", dtSR)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/SalesReturnReport.rdlc")

                Dim ReturnNo As ReportParameter() = New ReportParameter() {New ReportParameter("ReturnNo", dt.Rows(0)("ReturnNo").ToString())}
                ReportViewer1.LocalReport.SetParameters(ReturnNo)

                dtComp = objclsSaleReturn.LoadCustDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, dt.Rows(0)("CustID").ToString())
                If dtComp.Rows.Count > 0 Then
                    Dim CName As ReportParameter() = New ReportParameter() {New ReportParameter("CName", " " & dtComp.Rows(0)("CUST_NAME").ToString())}
                    ReportViewer1.LocalReport.SetParameters(CName)
                    Dim CAddress As ReportParameter() = New ReportParameter() {New ReportParameter("CAddress", " " & dtComp.Rows(0)("CUST_COMM_ADDRESS").ToString())}
                    ReportViewer1.LocalReport.SetParameters(CAddress)
                    Dim CustName As ReportParameter() = New ReportParameter() {New ReportParameter("CustName", " " & dtComp.Rows(0)("BM_Name").ToString())}
                    ReportViewer1.LocalReport.SetParameters(CustName)
                    Dim CustAddress As ReportParameter() = New ReportParameter() {New ReportParameter("CustAddress", " " & dtComp.Rows(0)("BM_Address").ToString())}
                    ReportViewer1.LocalReport.SetParameters(CustAddress)
                    Dim CGSTN As ReportParameter() = New ReportParameter() {New ReportParameter("CGSTN", " " & dtComp.Rows(0)("CUST_ProvisionalNo").ToString())}
                    ReportViewer1.LocalReport.SetParameters(CGSTN)
                    Dim SGSTN As ReportParameter() = New ReportParameter() {New ReportParameter("SGSTN", " " & dtComp.Rows(0)("BM_GSTNRegNo").ToString())}
                    ReportViewer1.LocalReport.SetParameters(SGSTN)
                End If

                Dim CVAT As ReportParameter() = New ReportParameter() {New ReportParameter("CVAT", "<B>" & "VAT : " & "</B>" & "")}
                ReportViewer1.LocalReport.SetParameters(CVAT)

                Dim CTIN As ReportParameter() = New ReportParameter() {New ReportParameter("CTIN", "<B>" & "TIN : " & "</B>" & "")}
                ReportViewer1.LocalReport.SetParameters(CTIN)

                Dim CTAN As ReportParameter() = New ReportParameter() {New ReportParameter("CTAN", "<B>" & "TAN : " & "</B>" & "")}
                ReportViewer1.LocalReport.SetParameters(CTAN)

                Dim CPAN As ReportParameter() = New ReportParameter() {New ReportParameter("CPAN", "<B>" & "PAN : " & "</B>" & "")}
                ReportViewer1.LocalReport.SetParameters(CPAN)

                Dim SVAT As ReportParameter() = New ReportParameter() {New ReportParameter("SVAT", "<B>" & "VAT : " & "</B>" & "")}
                ReportViewer1.LocalReport.SetParameters(SVAT)

                Dim STIN As ReportParameter() = New ReportParameter() {New ReportParameter("STIN", "<B>" & "TIN : " & "</B>" & "")}
                ReportViewer1.LocalReport.SetParameters(STIN)

                Dim STAN As ReportParameter() = New ReportParameter() {New ReportParameter("STAN", "<B>" & "TAN : " & "</B>" & "")}
                ReportViewer1.LocalReport.SetParameters(STAN)

                Dim SPAN As ReportParameter() = New ReportParameter() {New ReportParameter("SPAN", "<B>" & "PAN : " & "</B>" & "")}
                ReportViewer1.LocalReport.SetParameters(SPAN)

                Dim ReturnDate As ReportParameter() = New ReportParameter() {New ReportParameter("ReturnDate", dt.Rows(0)("ReturnDate").ToString())}
                ReportViewer1.LocalReport.SetParameters(ReturnDate)

                Dim InvoiceNo As ReportParameter() = New ReportParameter() {New ReportParameter("InvoiceNo", dt.Rows(0)("InvoiceNo").ToString())}
                ReportViewer1.LocalReport.SetParameters(InvoiceNo)

                Dim InvoiceDate As ReportParameter() = New ReportParameter() {New ReportParameter("InvoiceDate", dt.Rows(0)("InvoiceDate").ToString())}
                ReportViewer1.LocalReport.SetParameters(InvoiceDate)

                Dim OrderNo As ReportParameter() = New ReportParameter() {New ReportParameter("OrderNo", dt.Rows(0)("OrderNo").ToString())}
                ReportViewer1.LocalReport.SetParameters(OrderNo)

                Dim DispatchNo As ReportParameter() = New ReportParameter() {New ReportParameter("DispatchNo", dt.Rows(0)("DispatchNo").ToString())}
                ReportViewer1.LocalReport.SetParameters(DispatchNo)

                Dim ShipTo As ReportParameter() = New ReportParameter() {New ReportParameter("ShipTo", dt.Rows(0)("ShipTo").ToString())}
                ReportViewer1.LocalReport.SetParameters(ShipTo)
                ReportViewer1.LocalReport.Refresh()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
