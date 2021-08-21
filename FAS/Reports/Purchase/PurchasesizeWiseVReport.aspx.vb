
'Partial Class Reports_Purchase_PurchaseItemWiseVReport
'    Inherits System.Web.UI.Page

'End Class

Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Drawing


Partial Class Reports_Purchase_PurchasesizeWiseVReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Orders\PurchaseInvoiceViewer.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objGnrl As New clsFASGeneral
    Dim objPinVwr As New clsPurchaseInvoiceViewer
    Dim objPHR As New ClsPurchaseOrderHR
    Dim objdb As New DBHelper
    '  Dim objCstmr As New clsCustomerMaster
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iOrderID As Integer = 0
        Dim ibillNo As Integer = 0
        Try
            sSession = Session("AllSession")
            lblError.Text = ""
            If IsPostBack = False Then
                '  CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                LoadOrder()
                LoadInvoice()
                iOrderID = Request.QueryString("ExistingOrder")
                ibillNo = Request.QueryString("BillNo")
                If iOrderID > 0 And ibillNo > 0 Then
                    loaddetails(iOrderID, ibillNo)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub CheckAuidtPermission(ByVal sNameSpace As String, ByVal iUsrId As Integer)
        Dim sbret As String
        Try
            ' sbret = objGnrl.CheckUmsPermit(sNameSpace, sSession.AccessCodeID, iUsrId, "FasPIR", "ALL")
            If sbret = "False" Or sbret = "" Then
                Response.Redirect("~/Permissions/PurchasePermission.aspx")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub loaddetails(ByVal iOrderID As Integer, ByVal ibillNo As String)
        Dim dt As New DataTable
        Dim dt0 As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim dt4 As New DataTable
        Dim dt5 As New DataTable
        Dim Duniqe As New DataTable
        Dim dtComp, dtVendor As New DataTable
        Dim dRow As DataRow
        Dim ctin1 As String = "" : Dim Cpan1 As String = "" : Dim Span1 As String = "" : Dim Stin1 As String = "" : Dim company As String = "" : Dim suplierId As String = "" : Dim temp1 As String = ""
        Dim s3, s4, s5, s6, s7, s8, s9, s10, s11, Total, qty, s0 As Double
        Dim Totalamt As String = "" : Dim OrderNo, BillNo As Integer
        Dim TotalinWord As String = "" : Dim PGM_DocumentRefNo As String = "" : Dim PGM_InvoiceDate As String = "" : Dim POM_OrderDate As String = ""
        Dim Totaltax, DiscountAmt, GrandTotal, dimGtotal, CstAmnt, vatAmnt, ExceAmnt As Decimal
        'Dim Totalamt As Decimal
        Try
            company = objPinVwr.GetAccessCode(sSession.AccessCode)
            dt0 = objPinVwr.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, company)
            dt1 = objPinVwr.LoadGridOtherDetails(sSession.AccessCode, sSession.AccessCodeID)
            For i = 0 To dt1.Rows.Count - 1
                temp1 = "" : temp1 = dt1.Rows(i).Item("Statutory Name")
                If temp1.Contains("TIN") Then
                    ctin1 = dt1.Rows(i).Item("Statutory Name") & "-" & dt1.Rows(i).Item("Statutory Value")
                End If
                temp1 = "" : temp1 = dt1.Rows(i).Item("Statutory Name")
                If temp1.Contains("PAN") Then
                    Cpan1 = dt1.Rows(i).Item("Statutory Name") & "-" & dt1.Rows(i).Item("Statutory Value")
                End If
            Next
            If ddlorder.SelectedValue <> 0 Then
                OrderNo = ddlorder.SelectedValue
            Else
                OrderNo = iOrderID
                ddlorder.SelectedValue = iOrderID
                ddlorder.Visible = False
                lblOrderNo.Visible = False
            End If
            dt2 = objPinVwr.SupplierMaster(sSession.AccessCode, sSession.AccessCodeID, OrderNo)
            If (dt2.Rows.Count > 0) Then
                suplierId = dt2.Rows(0).Item("POM_Supplier")
                dt3 = objPinVwr.SupplierDetails(sSession.AccessCode, sSession.AccessCodeID, suplierId)
                For i = 0 To dt3.Rows.Count - 1
                    temp1 = "" : temp1 = dt3.Rows(i).Item("CST_Description")
                    If temp1.Contains("TIN") Then
                        Stin1 = dt3.Rows(i).Item("CST_Description") & "-" & dt3.Rows(i).Item("CST_Value")
                    End If
                    temp1 = "" : temp1 = dt3.Rows(i).Item("CST_Description")
                    If temp1.Contains("PAN") Then
                        Span1 = dt3.Rows(i).Item("CST_Description") & "-" & dt3.Rows(i).Item("CST_Value")
                    End If
                Next
            End If
            If ddlinvoice.SelectedValue <> 0 Then
                BillNo = ddlinvoice.SelectedValue
            Else
                BillNo = ibillNo
                ddlinvoice.SelectedValue = ibillNo
                ddlinvoice.Visible = False
                lblInvoiceNo.Visible = False
            End If
            dt4 = objPinVwr.loadDetails(sSession.AccessCode, sSession.AccessCodeID, OrderNo, BillNo)
            For i = 0 To dt4.Rows.Count - 1
                If i = dt4.Rows.Count - 1 Then
                    Totalamt = dt4.Rows(i)("TotalAmount")
                    dimGtotal = dt4.Rows(i)("TotalAmount")
                    CstAmnt = CstAmnt + dt4.Rows(i)("CSTAmt")
                    vatAmnt = vatAmnt + dt4.Rows(i)("VATAmt")
                    ExceAmnt = ExceAmnt + dt4.Rows(i)("ExiseAmt")
                    PGM_DocumentRefNo = dt4.Rows(i)("PGM_DocumentRefNo")
                    PGM_InvoiceDate = dt4.Rows(i)("PGM_InvoiceDate")
                    POM_OrderDate = dt4.Rows(i)("POM_OrderDate")
                    TotalinWord = NumberToWord(String.Format("{0:0.00}", dt4.Rows(i)("TotalAmount"))) & " Only"
                End If
            Next
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Colour")
            dt.Columns.Add("t0")
            dt.Columns.Add("t3")
            dt.Columns.Add("t4")
            dt.Columns.Add("t5")
            dt.Columns.Add("t6")
            dt.Columns.Add("t7")
            dt.Columns.Add("t8")
            dt.Columns.Add("t9")
            dt.Columns.Add("t10")
            dt.Columns.Add("t11")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("MRP")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("Amount")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("CName")
            dt.Columns.Add("CAdd")
            dt.Columns.Add("CPh")
            dt.Columns.Add("CEmail")
            dt.Columns.Add("Ctin")
            dt.Columns.Add("CPan")
            dt.Columns.Add("InvoiceNO")
            dt.Columns.Add("OrderNo")
            dt.Columns.Add("Saname")
            dt.Columns.Add("Sadd")
            dt.Columns.Add("Sph")
            dt.Columns.Add("SEmail")
            dt.Columns.Add("Stin")
            dt.Columns.Add("SPan")
            dt.Columns.Add("Totalamt")
            dt.Columns.Add("TotalinWord")
            dt.Columns.Add("NetAmnt")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("SuppGSTNno")
            dt.Columns.Add("CustGSTNno")

            dt5 = dt4.Copy
            Duniqe = objPinVwr.RemoveDublicate(dt5)
            For j = 0 To Duniqe.Rows.Count - 2
                dRow = dt.NewRow()
                qty = 0 : s0 = 0
                Totaltax = 0
                Totalamt = 0
                For i = 0 To dt4.Rows.Count - 2
                    dRow("SlNo") = j + 1
                    If dt4.Rows(i)("Commodity") = "<b>Total</b>" Then
                        dRow("SlNo") = ""
                    End If
                    dRow("Colour") = dt4.Rows(i)("Colour")
                    If (Duniqe.Rows(j)("Description") = dt4.Rows(i)("Description")) Then
                        If (dt4.Rows(i)("Description") <> "<b>Total</b>") Then
                            dRow("Description") = dt4.Rows(i)("Description")
                            dRow("Commodity") = dt4.Rows(i)("Commodity")

                            If ((dt4.Rows(i)("t0") <> 0) And (dt4.Rows(i)("t0").ToString() <> "")) Then
                                'dRow("t3") = 0
                                'Else
                                'dRow("t3") = Convert.ToInt32(dRow("t3")) + Convert.ToInt32(dt4.Rows(i)("t3"))
                                s0 = s0 + dt4.Rows(i)("t0")
                                dRow("t0") = s0
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t0"))
                            End If
                            If ((dt4.Rows(i)("t3") <> 0) And (dt4.Rows(i)("t3").ToString() <> "")) Then
                                'dRow("t3") = 0
                                'Else
                                'dRow("t3") = Convert.ToInt32(dRow("t3")) + Convert.ToInt32(dt4.Rows(i)("t3"))
                                dRow("t3") = dt4.Rows(i)("t3")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t3"))
                            End If

                            If ((dt4.Rows(i)("t4") <> 0 And dt4.Rows(i)("t4") <> "")) Then
                                '    dRow("t4") = 0
                                'Else
                                'dRow("t4") = Convert.ToInt32(dRow("t4")) + Convert.ToInt32(dt4.Rows(i)("t4"))
                                dRow("t4") = dt4.Rows(i)("t4")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t4"))
                            End If

                            If ((dt4.Rows(i)("t5") <> 0 And dt4.Rows(i)("t5") <> "")) Then
                                '    dRow("t5") = 0
                                'Else
                                'dRow("t5") = Convert.ToInt32(dRow("t5")) + Convert.ToInt32(dt4.Rows(i)("t5"))
                                dRow("t5") = dt4.Rows(i)("t5")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t5"))
                            End If

                            If ((dt4.Rows(i)("t6") <> 0 And dt4.Rows(i)("t6") <> "")) Then
                                '    dRow("t6") = 0
                                'Else
                                ' dRow("t6") = Convert.ToInt32(dRow("t6")) + Convert.ToInt32(dt4.Rows(i)("t6"))
                                dRow("t6") = dt4.Rows(i)("t6")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t6"))
                            End If

                            If ((dt4.Rows(i)("t7") <> 0 And dt4.Rows(i)("t7") <> "")) Then
                                '    dRow("t7") = 0
                                'Else
                                'dRow("t7") = Convert.ToInt32(dRow("t7")) + Convert.ToInt32(dt4.Rows(i)("t7"))
                                dRow("t7") = dt4.Rows(i)("t7")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t7"))
                            End If

                            If ((dt4.Rows(i)("t8") <> 0 And dt4.Rows(i)("t8") <> "")) Then
                                '    dRow("t8") = 0
                                'Else
                                'dRow("t8") = Convert.ToInt32(dRow("t8")) + Convert.ToInt32(dt4.Rows(i)("t8"))
                                dRow("t8") = dt4.Rows(i)("t8")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t8"))
                            End If

                            If ((dt4.Rows(i)("t9") <> 0 And dt4.Rows(i)("t9") <> "")) Then
                                '    dRow("t9") = 0
                                'Else
                                dRow("t9") = dt4.Rows(i)("t9")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t9"))
                            End If
                            If ((dt4.Rows(i)("t10") <> 0 And dt4.Rows(i)("t10") <> "")) Then
                                '    dRow("t10") = 0
                                'Else
                                'dRow("t10") = Convert.ToInt32(dRow("t10")) + Convert.ToInt32(dt4.Rows(i)("t10"))
                                dRow("t10") = dt4.Rows(i)("t10")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t10"))
                            End If

                            If ((dt4.Rows(i)("t11") <> 0 And dt4.Rows(i)("t11") <> "")) Then
                                dRow("t11") = dt4.Rows(i)("t11")
                                qty = qty + Convert.ToDecimal(dt4.Rows(i)("t11"))
                            End If
                            Totaltax = Totaltax + Convert.ToDecimal(dt4.Rows(i)("VATAmt")) + Convert.ToDecimal(dt4.Rows(i)("CSTAmt")) + Convert.ToDecimal(dt4.Rows(i)("ExiseAmt"))
                            dRow("MRP") = dt4.Rows(i)("Rate")
                            dRow("Rate") = dt4.Rows(i)("Rate")
                            DiscountAmt = DiscountAmt + Convert.ToDecimal(dt4.Rows(i)("DiscountAmt"))
                            Totalamt = Convert.ToDecimal(dRow("Rate")) * qty
                            dRow("CSTAmt") = CstAmnt
                            dRow("ExiseAmt") = ExceAmnt
                            dRow("VATAmt") = vatAmnt
                            dRow("NetAmnt") = dimGtotal - (vatAmnt + ExceAmnt + CstAmnt)
                            dRow("VAT") = dt4.Rows(i)("VAT")
                            dRow("Discount") = dt4.Rows(i)("Discount")
                        End If
                    End If

                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(Totalamt))
                    dRow("TotalQty") = qty
                    dRow("DiscountAmt") = DiscountAmt
                    dRow("VAT") = dt4.Rows(i)("VAT")
                    dRow("Exise") = dt4.Rows(i)("Exise")
                    dRow("CName") = dt0.Rows(0).Item("CUST_NAME")
                    dRow("CAdd") = dt0.Rows(0).Item("CUST_COMM_ADDRESS")
                    dRow("CPh") = "Ph  " & dt0.Rows(0).Item("CUST_COMM_TEL")
                    dRow("CEmail") = "E-mail  " & dt0.Rows(0).Item("CUST_EMAIL")
                    dRow("Ctin") = ctin1
                    dRow("CPan") = Cpan1
                    dRow("InvoiceNO") = ddlinvoice.SelectedItem.Text
                    dRow("OrderNo") = ddlorder.SelectedItem.Text
                    If (dt2.Rows.Count > 0) Then
                        dRow("Saname") = dt2.Rows(0).Item("CSM_Name")
                        dRow("Sadd") = dt2.Rows(0).Item("CSM_Address")
                        dRow("Sph") = "Ph  " & dt2.Rows(0).Item("CSM_LandLineNo")
                        dRow("SEmail") = "E-mail  " & dt2.Rows(0).Item("CSM_EmailID")
                        dRow("Stin") = Stin1
                        dRow("SPan") = Span1
                        dRow("SuppGSTNno") = dt2.Rows(0).Item("CSM_GSTNRegNo")
                    End If
                    If (dt0.Rows.Count > 0) Then
                        dRow("CustGSTNno") = dt0.Rows(0).Item("CUST_ProvisionalNo")
                    End If
                    dRow("Totalamt") = "Total Net Amount  Rs  " & Totalamt
                    dRow("TotalinWord") = TotalinWord
                    dRow("NetAmnt") = dimGtotal - (vatAmnt + ExceAmnt + CstAmnt)
                    dRow("GrandTotal") = dimGtotal
                Next
                GrandTotal = GrandTotal + Totalamt
                dt.Rows.Add(dRow)
            Next
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Purchase/rptPurchaseInvoiceViewer.rdlc")

            Dim Odate As ReportParameter() = {New ReportParameter("Odate", "" & POM_OrderDate)}
            ReportViewer1.LocalReport.SetParameters(Odate)

            Dim InVoiceNo As ReportParameter() = {New ReportParameter("InVoiceNo", "" & PGM_DocumentRefNo)}
            ReportViewer1.LocalReport.SetParameters(InVoiceNo)

            Dim InvoiceDate As ReportParameter() = {New ReportParameter("InvoiceDate", "" & PGM_InvoiceDate)}
            ReportViewer1.LocalReport.SetParameters(InvoiceDate)

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

            '********************
            'Terms and Condtions
            If objdb.SQLCheckForRecord(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21") Then
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", objdb.SQLGetDescription(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21"))}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            Else
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", " ")}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            End If
            '********************
            'dtVendor = ClsPurchaseOrderHR.GetVendorTemplete(sSession.AccessCode, sSession.AccessCodeID, iOrderID)
            'If dtVendor.Rows.Count > 0 Then
            '    For j = 0 To dtVendor.Rows.Count - 1

            '        If dtVendor.Rows(j)("CST_Description").ToString() = "CST" Then
            '            Dim VCST As ReportParameter() = New ReportParameter() {New ReportParameter("VCST", "<B>" & "CST : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
            '            ReportViewer1.LocalReport.SetParameters(VCST)
            '        Else
            '            Dim VCST As ReportParameter() = New ReportParameter() {New ReportParameter("VCST", "<B>" & "CST : " & "</B>" & "")}
            '            ReportViewer1.LocalReport.SetParameters(VCST)
            '        End If

            '        If dtVendor.Rows(j)("CST_Description").ToString() = "VAT" Then
            '            Dim VVAT As ReportParameter() = New ReportParameter() {New ReportParameter("VVAT", "<B>" & "VAT : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
            '            ReportViewer1.LocalReport.SetParameters(VVAT)
            '        Else
            '            Dim VVAT As ReportParameter() = New ReportParameter() {New ReportParameter("VVAT", "<B>" & "VAT : " & "</B>" & "")}
            '            ReportViewer1.LocalReport.SetParameters(VVAT)
            '        End If

            '        'If dtVendor.Rows(j)("CST_Description").ToString() = "TIN" Then
            '        '    Dim VTIN As ReportParameter() = New ReportParameter() {New ReportParameter("VTIN", "<B>" & "TIN : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
            '        '    ReportViewer1.LocalReport.SetParameters(VTIN)
            '        'Else
            '        '    Dim VTIN As ReportParameter() = New ReportParameter() {New ReportParameter("VTIN", "<B>" & "TIN : " & "</B>" & "")}
            '        '    ReportViewer1.LocalReport.SetParameters(VTIN)
            '        'End If

            '        'If dtVendor.Rows(j)("CST_Description").ToString() = "TAN" Then
            '        '    Dim VTAN As ReportParameter() = New ReportParameter() {New ReportParameter("VTAN", "<B>" & "TAN : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
            '        '    ReportViewer1.LocalReport.SetParameters(VTAN)
            '        'Else
            '        '    Dim VTAN As ReportParameter() = New ReportParameter() {New ReportParameter("VTAN", "<B>" & "TAN : " & "</B>" & "")}
            '        '    ReportViewer1.LocalReport.SetParameters(VTAN)
            '        'End If

            '        'If dtVendor.Rows(j)("CST_Description").ToString() = "PAN" Then
            '        '    Dim VPAN As ReportParameter() = New ReportParameter() {New ReportParameter("VPAN", "<B>" & "PAN : " & "</B>" & UCase(dtVendor.Rows(j)("CST_Value").ToString()))}
            '        '    ReportViewer1.LocalReport.SetParameters(VPAN)
            '        'Else
            '        '    Dim VPAN As ReportParameter() = New ReportParameter() {New ReportParameter("VPAN", "<B>" & "PAN : " & "</B>" & "")}
            '        '    ReportViewer1.LocalReport.SetParameters(VPAN)
            '        'End If
            '    Next

            'End If
            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loaddetails")
        End Try
    End Sub
    Private Sub LoadOrder()
        Try
            ddlorder.DataSource = objPinVwr.Order(sSession.AccessCode, sSession.AccessCodeID)
            ddlorder.DataTextField = "POM_OrderNo"
            ddlorder.DataValueField = "POM_ID"
            ddlorder.DataBind()
            ddlorder.Items.Insert(0, New ListItem("--- Select Order No. ---", "0"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub LoadInvoice()
        Try
            ddlinvoice.DataSource = objPinVwr.Invoice(sSession.AccessCode, sSession.AccessCodeID, ddlorder.SelectedValue)
            ddlinvoice.DataTextField = "PV_BillNo"
            ddlinvoice.DataValueField = "PV_ID"
            ddlinvoice.DataBind()
            ddlinvoice.Items.Insert(0, New ListItem("--- Select Invoice No. ---", "0"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlorder_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlorder.SelectedIndexChanged
        Try
            LoadInvoice()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlorder_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlinvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlinvoice.SelectedIndexChanged
        Try
            loaddetails(ddlorder.SelectedValue, ddlinvoice.SelectedValue)
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlinvoice_SelectedIndexChanged")
        End Try
    End Sub
    Public Shared Function NumberToWord(ByVal num1 As String) As String
        Dim words, strones(100), strtens(100), aftrdecimalWord As String
        Dim crore, lakhs, thousands, hundreds, tens, ssingle, aftrDecimal1, aftrDecimal, num As Double
        Try
            If (num1.Contains(".")) Then
                Dim str1 As String() = Strings.Split(num1, ".")
                num = Convert.ToDouble(str1(0))
            Else
                num = Convert.ToDouble(num1)
            End If
            aftrDecimal1 = num

            If num = 0 Then
                Return ""
            End If


            If num < 0 Then
                Return "Not supported"
            End If

            words = ""
            strones = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
            strtens = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
            crore = 0
            lakhs = 0
            thousands = 0
            hundreds = 0
            tens = 0
            ssingle = 0

            If (num > 10000000) Then

                If ((Convert.ToString(num / 10000000)).Contains(".")) Then
                    crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
                    num = num - (hundreds * 10000000)
                Else
                    crore = num / 100
                    num = num - (hundreds * 10000000)
                End If
            End If

            If (num > 100000) Then

                If ((Convert.ToString(num / 100000)).Contains(".")) Then
                    lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                    num = num - (hundreds * 100000)
                Else
                    lakhs = num / 100000
                    num = num - (hundreds * 100000)
                End If
            End If


            'lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))        
            If (num > 1000) Then

                If ((Convert.ToString(num / 1000)).Contains(".")) Then
                    thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                    num = num - (thousands * 1000)
                Else
                    thousands = num / 1000
                    num = num - (thousands * 1000)
                End If
            End If
            'thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))        

            If (num > 100) Then

                If ((Convert.ToString(num / 100)).Contains(".")) Then
                    hundreds = Convert.ToInt32((num / 100).ToString().Substring(0, (num / 100).ToString().IndexOf(".")))
                    num = num - (hundreds * 100)
                Else
                    hundreds = num / 100
                    num = num - (hundreds * 100)
                End If
            End If
            If num > 19 Then
                If ((Convert.ToString(num / 10)).Contains(".")) Then
                    tens = Convert.ToInt32((num / 10).ToString().Substring(0, (num / 10).ToString().IndexOf(".")))
                    num = num - (tens * 10)
                Else
                    tens = num / 10
                    num = num - (tens * 10)
                End If

            End If

            ssingle = num

            If crore > 0 Then
                If crore > 19 Then
                    words += NumberToWord(crore) + "Crore "
                Else
                    words += strones(crore - 1) + " Crore "
                End If
            End If
            If lakhs > 0 Then

                If lakhs > 19 Then
                    words += NumberToWord(lakhs) + "Lakh "
                Else
                    words += strones(lakhs - 1) + " Lakh "
                End If
            End If

            If thousands > 0 Then

                If thousands > 19 Then
                    words += NumberToWord(thousands) + "Thousand "
                Else
                    words += strones(thousands - 1) + " Thousand "
                End If
            End If


            If hundreds > 0 Then
                words += strones(hundreds - 1) + " Hundred "
            End If


            If tens > 0 Then
                words += strtens(tens - 2) + " "
            End If

            If ssingle > 0 Then
                words += strones(ssingle - 1) + " "
            End If

            If (num1.Contains(".")) Then
                Dim str As String() = Strings.Split(num1, ".")
                aftrDecimal = Convert.ToDouble(str(1))
                aftrdecimalWord = AfterDecimalfunction(aftrDecimal)
                If aftrdecimalWord = "zero" Then
                    words += ""
                Else
                    aftrdecimalWord += " Paise"
                    words += " and " + aftrdecimalWord

                End If
            End If
            Return words
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function AfterDecimalfunction(ByVal num As Decimal) As String
        Dim words, strones(100), strtens(100) As String
        Dim crore, lakhs, thousands, hundreds, tens, ssingle As Decimal
        Try
            If num = 0 Then
                Return "Zero"
            End If

            If num < 0 Then
                Return "Not supported"
            End If
            words = ""
            strones = {"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
            strtens = {"Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
            crore = 0
            lakhs = 0
            thousands = 0
            hundreds = 0
            tens = 0
            ssingle = 0

            If ((Convert.ToString(num / 10000000)).Contains(".")) Then
                crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
                num = num - (hundreds * 10000000)
            Else
                crore = num / 10000000
                num = num - (hundreds * 10000000)
            End If

            'crore = Convert.ToInt32((num / 10000000).ToString().Substring(0, (num / 10000000).ToString().IndexOf(".")))
            'num = num - (crore * 10000000)


            If ((Convert.ToString(num / 100000)).Contains(".")) Then
                lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))
                num = num - (hundreds * 100000)
            Else
                lakhs = num / 100000
                num = num - (hundreds * 100000)
            End If

            'lakhs = Convert.ToInt32((num / 100000).ToString().Substring(0, (num / 100000).ToString().IndexOf(".")))

            'num = num - (lakhs * 100000)

            If ((Convert.ToString(num / 1000)).Contains(".")) Then
                thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
                num = num - (thousands * 1000)
            Else
                thousands = num / 1000
                num = num - (thousands * 1000)
            End If

            thousands = Convert.ToInt32((num / 1000).ToString().Substring(0, (num / 1000).ToString().IndexOf(".")))
            num = num - (thousands * 1000)


            If ((Convert.ToString(num / 100)).Contains(".")) Then
                hundreds = Convert.ToInt32((num / 100).ToString().Substring(0, (num / 100).ToString().IndexOf(".")))
                num = num - (hundreds * 100)
            Else
                hundreds = num / 100
                num = num - (hundreds * 100)
            End If
            If num > 19 Then
                If ((Convert.ToString(num / 10)).Contains(".")) Then
                    tens = Convert.ToInt32((num / 10).ToString().Substring(0, (num / 10).ToString().IndexOf(".")))
                    num = num - (tens * 10)
                Else
                    tens = num / 10
                    num = num - (tens * 10)
                End If

            End If

            ssingle = num

            If crore > 0 Then
                If crore > 19 Then
                    words += NumberToWord(crore) + "Crore "
                Else
                    words += strones(crore - 1) + " Crore "
                End If
            End If
            If lakhs > 0 Then

                If lakhs > 19 Then
                    words += NumberToWord(lakhs) + "Lakh "
                Else
                    words += strones(lakhs - 1) + " Lakh "
                End If
            End If

            If thousands > 0 Then

                If thousands > 19 Then
                    words += NumberToWord(thousands) + "Thousand "
                Else
                    words += strones(thousands - 1) + " Thousand "
                End If
            End If

            If hundreds > 0 Then
                words += strones(hundreds - 1) + " Hundred "
            End If

            If tens > 0 Then
                words += strtens(tens - 2) + " "
            End If

            If ssingle > 0 Then
                words += strones(ssingle - 1) + " "
            End If
            Return words
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class

