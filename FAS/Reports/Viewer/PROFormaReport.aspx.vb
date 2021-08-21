Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Reports_Viewer_PROFormaReport
    Inherits System.Web.UI.Page
    Private sFormName As String = "Reports_Viewer_PROFormaReport"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objProForma As New clsPROFormaSalesOrder
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iOrderID As Integer
        Dim dt As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                iOrderID = Request.QueryString("ExistingOrder")
                If iOrderID > 0 Then
                    showsetails(iOrderID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub showsetails(ByVal iOrderID As Integer)
        Dim dtPrint As New DataTable
        Dim dt, dt2 As New DataTable

        Dim iTotalAmt As Double : Dim iExiceAmt As Double : Dim iGrandDiscountAmt As Double : Dim iCSTAmt As Double
        Dim dFinalTotal As Double : Dim dCSTAmount As Double : Dim dVATAmount As Double
        Try

            ReportViewer1.Reset()
            dt = objProForma.GetAllDetails(sSession.AccessCode, sSession.AccessCodeID, iOrderID)

            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/PROForma.rdlc")

            Dim VAT As ReportParameter() = New ReportParameter() {New ReportParameter("VAT", UCase(objProForma.GetALLVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)))}
            ReportViewer1.LocalReport.SetParameters(VAT)

            Dim VATAmount As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmount", UCase(objProForma.GetALLVATAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)))}
            ReportViewer1.LocalReport.SetParameters(VATAmount)

            Dim CST As ReportParameter() = New ReportParameter() {New ReportParameter("CST", UCase(objProForma.GetALLCST(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)))}
            ReportViewer1.LocalReport.SetParameters(CST)

            Dim CSTAmount As ReportParameter() = New ReportParameter() {New ReportParameter("CSTAmount", UCase(objProForma.GetALLCSTAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)))}
            ReportViewer1.LocalReport.SetParameters(CSTAmount)


            dtPrint = objProForma.GetPrintData(sSession.AccessCode, sSession.AccessCodeID, "S")
            If dtPrint.Rows.Count > 0 Then

                Dim CAddress As ReportParameter() = New ReportParameter() {New ReportParameter("CAddress", UCase(dtPrint.Rows(0)("PS_CAddress").ToString))}
                ReportViewer1.LocalReport.SetParameters(CAddress)

                Dim CPhoneNo As ReportParameter() = New ReportParameter() {New ReportParameter("CPhoneNo", UCase(dtPrint.Rows(0)("PS_CPhoneNo").ToString))}
                ReportViewer1.LocalReport.SetParameters(CPhoneNo)

                Dim CEmail As ReportParameter() = New ReportParameter() {New ReportParameter("CEmail", UCase(dtPrint.Rows(0)("PS_CEmail").ToString))}
                ReportViewer1.LocalReport.SetParameters(CEmail)

                Dim CVAT As ReportParameter() = New ReportParameter() {New ReportParameter("CVAT", UCase(dtPrint.Rows(0)("PS_CVAT").ToString))}
                ReportViewer1.LocalReport.SetParameters(CVAT)

                Dim CTAX As ReportParameter() = New ReportParameter() {New ReportParameter("CTAX", UCase(dtPrint.Rows(0)("PS_CTAX").ToString))}
                ReportViewer1.LocalReport.SetParameters(CTAX)

                Dim CPAN As ReportParameter() = New ReportParameter() {New ReportParameter("CPAN", UCase(dtPrint.Rows(0)("PS_CPAN").ToString))}
                ReportViewer1.LocalReport.SetParameters(CPAN)

                Dim CTAN As ReportParameter() = New ReportParameter() {New ReportParameter("CTAN", UCase(dtPrint.Rows(0)("PS_CTAN").ToString))}
                ReportViewer1.LocalReport.SetParameters(CTAN)

                Dim CTIN As ReportParameter() = New ReportParameter() {New ReportParameter("CTIN", UCase(dtPrint.Rows(0)("PS_CTIN").ToString))}
                ReportViewer1.LocalReport.SetParameters(CTIN)

                Dim CCIN As ReportParameter() = New ReportParameter() {New ReportParameter("CCIN", UCase(dtPrint.Rows(0)("PS_CCIN").ToString))}
                ReportViewer1.LocalReport.SetParameters(CCIN)

                Dim BAddress As ReportParameter() = New ReportParameter() {New ReportParameter("BAddress", UCase(dtPrint.Rows(0)("PS_BAddress").ToString))}
                ReportViewer1.LocalReport.SetParameters(BAddress)

                Dim BPhoneNo As ReportParameter() = New ReportParameter() {New ReportParameter("BPhoneNo", UCase(dtPrint.Rows(0)("PS_BPhoneNo").ToString))}
                ReportViewer1.LocalReport.SetParameters(BPhoneNo)

                Dim BEmail As ReportParameter() = New ReportParameter() {New ReportParameter("BEmail", UCase(dtPrint.Rows(0)("PS_BEmail").ToString))}
                ReportViewer1.LocalReport.SetParameters(BEmail)

                Dim BVAT As ReportParameter() = New ReportParameter() {New ReportParameter("BVAT", UCase(dtPrint.Rows(0)("PS_BVAT").ToString))}
                ReportViewer1.LocalReport.SetParameters(BVAT)

                Dim BTAX As ReportParameter() = New ReportParameter() {New ReportParameter("BTAX", UCase(dtPrint.Rows(0)("PS_BTAX").ToString))}
                ReportViewer1.LocalReport.SetParameters(BTAX)

                Dim BPAN As ReportParameter() = New ReportParameter() {New ReportParameter("BPAN", UCase(dtPrint.Rows(0)("PS_BPAN").ToString))}
                ReportViewer1.LocalReport.SetParameters(BPAN)

                Dim BTAN As ReportParameter() = New ReportParameter() {New ReportParameter("BTAN", UCase(dtPrint.Rows(0)("PS_BTAN").ToString))}
                ReportViewer1.LocalReport.SetParameters(BTAN)

                Dim BTIN As ReportParameter() = New ReportParameter() {New ReportParameter("BTIN", UCase(dtPrint.Rows(0)("PS_BTIN").ToString))}
                ReportViewer1.LocalReport.SetParameters(BTIN)

                Dim BCIN As ReportParameter() = New ReportParameter() {New ReportParameter("BCIN", UCase(dtPrint.Rows(0)("PS_BCIN").ToString))}
                ReportViewer1.LocalReport.SetParameters(BCIN)

                Dim STerms As ReportParameter() = New ReportParameter() {New ReportParameter("STerms", UCase(dtPrint.Rows(0)("PS_Terms").ToString))}
                ReportViewer1.LocalReport.SetParameters(STerms)

                Dim SReceiver As ReportParameter() = New ReportParameter() {New ReportParameter("SReceiver", UCase(dtPrint.Rows(0)("PS_Receiver").ToString))}
                ReportViewer1.LocalReport.SetParameters(SReceiver)

                Dim SAuthorised As ReportParameter() = New ReportParameter() {New ReportParameter("SAuthorised", UCase(dtPrint.Rows(0)("PS_Authorise").ToString))}
                ReportViewer1.LocalReport.SetParameters(SAuthorised)

            End If

            Dim dBuyerOrderDate As ReportParameter() = New ReportParameter() {New ReportParameter("dBuyerOrderDate", objProForma.GetBuyerOrderDtae(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID))}
            ReportViewer1.LocalReport.SetParameters(dBuyerOrderDate)

            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iGrandDiscountAmt = dt.Rows(i)("SPO_GrandDiscountAmt")
                    iTotalAmt = iTotalAmt + dt.Rows(i)("SPOD_TotalAmount")
                    iExiceAmt = iExiceAmt + dt.Rows(i)("ExciseAmount")
                    iCSTAmt = iCSTAmt + dt.Rows(i)("SPOD_CSTAmount")
                Next
            End If

            dVATAmount = objProForma.GetALLVATAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            dCSTAmount = objProForma.GetALLCSTAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            If iCSTAmt > 0 Then
                dFinalTotal = Math.Round((((iTotalAmt - iGrandDiscountAmt) + iExiceAmt) + dCSTAmount))
            Else
                dFinalTotal = Math.Round((((iTotalAmt - iGrandDiscountAmt) + iExiceAmt) + dVATAmount))
            End If

            Dim dFinalDisplayAmt As ReportParameter() = New ReportParameter() {New ReportParameter("dFinalDisplayAmt", dFinalTotal)}
            ReportViewer1.LocalReport.SetParameters(dFinalDisplayAmt)

            dt2 = objProForma.GetVendorDetails(sSession.AccessCode, sSession.AccessCodeID, iOrderID)

            Dim rds2 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/PROForma.rdlc")


            ''---VAT & CST Bifercation---'
            'Dim dtV As New DataTable
            'dtV = objProForma.GetVATBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)

            'Dim rds3 As New ReportDataSource("DataSet3", dtV)
            'ReportViewer1.LocalReport.DataSources.Add(rds3)
            'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/PROForma.rdlc")

            'Dim dAmount, dVAAmount As Double
            'If dtV.Rows.Count > 0 Then
            '    For j = 0 To dtV.Rows.Count - 1
            '        dAmount = dAmount + dtV.Rows(j)("Amount")
            '        dVAAmount = dVAAmount + dtV.Rows(j)("VATAmount")
            '    Next
            'End If

            'Dim AmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("AmtBifercation", dAmount)}
            'ReportViewer1.LocalReport.SetParameters(AmtBifercation)

            'Dim VATAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmtBifercation", dVAAmount)}
            'ReportViewer1.LocalReport.SetParameters(VATAmtBifercation)


            'Dim dtC As New DataTable
            'dtC = objProForma.GetCSTBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)

            'Dim rds4 As New ReportDataSource("DataSet4", dtC)
            'ReportViewer1.LocalReport.DataSources.Add(rds4)
            'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/PROForma.rdlc")

            'Dim dCAmount, dCSAmount As Double
            'If dtC.Rows.Count > 0 Then
            '    For k = 0 To dtC.Rows.Count - 1
            '        dCAmount = dCAmount + dtC.Rows(k)("CAmount")
            '        dCSAmount = dCSAmount + dtC.Rows(k)("CSTAmount")
            '    Next
            'End If

            'Dim CAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("CAmtBifercation", dCAmount)}
            'ReportViewer1.LocalReport.SetParameters(CAmtBifercation)

            'Dim CSTAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("CSTAmtBifercation", dCSAmount)}
            'ReportViewer1.LocalReport.SetParameters(CSTAmtBifercation)

            ''---VAT & CST Bifercation---'

            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
