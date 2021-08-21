
'Partial Class Reports_Purchase_PurchaseItemWiseVReport
'    Inherits System.Web.UI.Page

'End Class

Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Drawing


Partial Class Reports_Purchase_PurchaseItemWiseVReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Orders\PurchaseInvoiceViewer.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objPOHR As New ClsPurchaseOrderHR
    Dim objclsModulePermission As New clsModulePermission
    Dim objDb As New DBHelper
    Dim objFasGnrl As New clsFASGeneral
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String = True
        Dim iOrderID As Integer = 0
        Dim ibillNo As Integer = 0
        Try
            'imgbtnBack.ImageUrl = "~/Images/Backward24.png"
            sSession = Session("AllSession")
            sFormButtons = objclsModulePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "PIR")

            If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,,,," Then
                Response.Redirect("~/Permissions/PurchasePermission.aspx", False) 'Permissions/PurchasePermission
                Exit Sub
            Else
                If sFormButtons.Contains(",View,") = True Then
                End If
            End If
            'lblErrorUp.Text = ""
            If IsPostBack = False Then
                ' CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                LoadOrder()
                LoadInvoice()
                iOrderID = Request.QueryString("ExistingOrder")
                ibillNo = Request.QueryString("BillNo")
                If iOrderID > 0 And ibillNo > 0 Then
                    loaddetailsItemVerified(iOrderID, ibillNo)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub

    'Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
    '    Dim oStatus As Object
    '    Try
    '        'lblError.Text = ""
    '        'lblStatus.Text = ""
    '        'oStatus = HttpUtility.UrlEncode(objFasGnrl.EncryptQueryString(Val(sIKBBackStatus)))
    '        Response.Redirect(String.Format("~/HomePages/Home.aspx?"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
    '    End Try
    'End Sub
    Public Sub CheckAuidtPermission(ByVal sNameSpace As String, ByVal iUsrId As Integer)
        Dim sbret As String
        Try
            '   sbret = clsGeneralMaster.CheckUmsPermit(sNameSpace, sSession.AccessCodeID, iUsrId, "FasPIR", "ALL")
            If sbret = "False" Or sbret = "" Then
                Response.Redirect("~/Permissions/PurchasePermission.aspx")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub loaddetailsItemVerified(ByVal iOrderID As Integer, ByVal ibillNo As String)
        Dim dt As New DataTable
        Dim dt0 As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim dt4 As New DataTable
        Dim dtComp As New DataTable
        Dim dtVendor As New DataTable
        Dim Duniqe As New DataTable
        Dim dtCalVat As New DataTable
        Dim dtVatVBifrctn As New DataTable
        Dim dtCalCst As New DataTable
        Dim dtCstVBifrctn As New DataTable

        Dim BasicAmount As Decimal = 0
        Dim Grandamount As Decimal = 0
        Dim dRow As DataRow
        Dim CSM_MobileNo As String = ""
        Dim CSM_EmailID As String = ""
        Dim CUST_COMM_ADDRESS As String = "", CUST_Name As String = "", CUST_EMAIL As String = "", CUST_COMM_TEL As String = "", CUST_CODE As String = "", CSM_Name As String = ""
        Dim CSM_Address As String = "", OrderNo1 As String = "", OrderDate As String = ""
        Dim ctin1 As String = "" : Dim Cpan1 As String = "" : Dim Span As String = "" : Dim Stin As String = "" : Dim company As String = "" : Dim suplierId As String = "" : Dim temp1 As String = ""
        Dim Totalamt As String = "", SubTotal = "", CSTAmtTotal = "", TotalVat = "", GrandTotal = "", UnitId = "", AltUnit = "", ExciseAmtTotal = "" : Dim OrderNo, BillNo As Integer
        Dim TotalinWord As String = "" : Dim POM_OrderDate As String = "" : Dim PGM_DocumentRefNo As String = "" : Dim PGM_InvoiceDate As String = ""
        Dim totalavat As Double = 0
        Dim GrandVat As Double = 0
        Dim GrandCSTamount As Double = 0
        Dim GrandCst As Double = 0
        Dim gtdiscountAmt As Double = 0
        Try
            company = objPOHR.GetAccessCode(sSession.AccessCode)
            dt0 = objPOHR.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, company)
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
                'lblOrderNo.Visible = False
            End If
            dt2 = objPOHR.SupplierMaster(sSession.AccessCode, sSession.AccessCodeID, OrderNo)
            If (dt2.Rows.Count > 0) Then
                suplierId = dt2.Rows(0).Item("POM_Supplier")
                dt3 = objPOHR.SupplierDetails(sSession.AccessCode, sSession.AccessCodeID, suplierId)
                For i = 0 To dt3.Rows.Count - 1
                    temp1 = "" : temp1 = dt3.Rows(i).Item("CST_Description")
                    If temp1.Contains("TIN") Then
                        Stin = dt3.Rows(i).Item("CST_Description") & "-" & dt3.Rows(i).Item("CST_Value")
                    End If
                    temp1 = "" : temp1 = dt3.Rows(i).Item("CST_Description")
                    If temp1.Contains("PAN") Then
                        Span = dt3.Rows(i).Item("CST_Description") & "-" & dt3.Rows(i).Item("CST_Value")
                    End If
                Next
            End If
            If ddlinvoice.SelectedValue <> 0 Then
                BillNo = ddlinvoice.SelectedValue
            Else
                BillNo = ibillNo
                ddlinvoice.SelectedValue = ibillNo
                ddlinvoice.Visible = False
                '    lblInvoiceNo.Visible = False
            End If
            'dt4 = objPOHR.loadDetailsPOVerified(sSession.AccessCode, sSession.AccessCodeID, OrderNo, BillNo)
            dt4 = objPOHR.loadDetailsBGST(sSession.AccessCode, sSession.AccessCodeID, OrderNo, BillNo)
            For i = 0 To dt4.Rows.Count - 1
                If i = dt4.Rows.Count - 1 Then
                    Totalamt = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("TotalAmount")))
                    SubTotal = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("SubTotal")))
                    TotalVat = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("TotalVat")))
                    GrandTotal = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("GrandTotal")))
                    gtdiscountAmt = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("gtdiscountAmt")))
                    UnitId = dt4.Rows(i)("UnitId")
                    AltUnit = dt4.Rows(i)("AltUnit")
                    CSTAmtTotal = dt4.Rows(i)("CSTAmtTotal")
                    ExciseAmtTotal = dt4.Rows(i)("TotalExiseAmt")
                    PGM_DocumentRefNo = dt4.Rows(i)("PGM_DocumentRefNo")
                    PGM_InvoiceDate = dt4.Rows(i)("PGM_InvoiceDate")
                    POM_OrderDate = dt4.Rows(i)("POM_OrderDate")
                End If
            Next

            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("MRP")
            dt.Columns.Add("Rate")
            dt.Columns.Add("ChargePerItem")
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
            dt.Columns.Add("SubTotal")
            dt.Columns.Add("TotalVat")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("UnitId")
            dt.Columns.Add("AltUnit")
            dt.Columns.Add("CSTAmtTotal")
            dt.Columns.Add("INVH_MRP")
            dt.Columns.Add("INVH_Edate")
            dt.Columns.Add("INVH_Mdate")
            dt.Columns.Add("BatchNumber")
            dt.Columns.Add("gtdiscountAmt")
            dt.Columns.Add("ExciseAmtTotal")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("CalculateAmount")
            dt.Columns.Add("HSNCode")

            For i = 0 To dt4.Rows.Count - 1
                dRow = dt.NewRow()
                dRow("SlNo") = dt4.Rows(i)("SlNo")
                dRow("Commodity") = dt4.Rows(i)("Commodity")
                dRow("Description") = dt4.Rows(i)("Description")
                dRow("BatchNumber") = dt4.Rows(i)("BatchNumber")
                dRow("TotalQty") = String.Format("{0: 0.###}", Convert.ToDecimal(dt4.Rows(i)("TotalQty"))) 'dt4.Rows(i)("TotalQty")
                dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("Rate")))
                dRow("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("Rate")))
                dRow("ChargePerItem") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("ChargePerItem")))
                dRow("Discount") = dt4.Rows(i)("Discount")
                dRow("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("DiscountAmt")))
                dRow("VAT") = dt4.Rows(i)("VAT")
                dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("VATAmt")))
                dRow("CST") = dt4.Rows(i)("CST")
                dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("CSTAmt")))
                dRow("Exise") = dt4.Rows(i)("Exise")
                dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("ExiseAmt")))
                'dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("TotalAmount")))
                dRow("CName") = objPOHR.GetAccessCode(sSession.AccessCode)
                CUST_CODE = objPOHR.GetAccessCode(sSession.AccessCode)
                dRow("CAdd") = dt0.Rows(0).Item("CUST_COMM_ADDRESS")
                CUST_COMM_ADDRESS = dt0.Rows(0).Item("CUST_COMM_ADDRESS")
                dRow("CPh") = "Ph  " & dt0.Rows(0).Item("CUST_COMM_TEL")
                CUST_COMM_TEL = dt0.Rows(0).Item("CUST_COMM_TEL")
                dRow("CEmail") = "E-mail  " & dt0.Rows(0).Item("CUST_EMAIL")
                CUST_EMAIL = dt0.Rows(0).Item("CUST_EMAIL")
                CUST_Name = dt0.Rows(0).Item("CUST_NAME")
                dRow("Ctin") = ctin1
                dRow("CPan") = Cpan1
                dRow("InvoiceNO") = ddlinvoice.SelectedItem.Text
                dRow("OrderNo") = ddlorder.SelectedItem.Text
                OrderNo1 = ddlorder.SelectedItem.Text
                If (dt2.Rows.Count > 0) Then
                    dRow("Saname") = dt2.Rows(0).Item("CSM_Name")
                    CSM_Name = dt2.Rows(0).Item("CSM_Name")
                    dRow("Sadd") = dt2.Rows(0).Item("CSM_Address")
                    CSM_Address = dt2.Rows(0).Item("CSM_Address")
                    dRow("Sph") = "Ph  " & dt2.Rows(0).Item("CSM_LandLineNo")
                    dRow("SEmail") = "E-mail  " & dt2.Rows(0).Item("CSM_EmailID")
                    dRow("Stin") = Stin
                    dRow("SPan") = Span
                End If
                dRow("Totalamt") = Totalamt
                dRow("TotalinWord") = TotalinWord
                dRow("SubTotal") = SubTotal
                dRow("GrandTotal") = GrandTotal
                dRow("TotalVat") = TotalVat
                dRow("UnitId") = UnitId
                dRow("AltUnit") = AltUnit
                dRow("CSTAmtTotal") = CSTAmtTotal
                dRow("ExciseAmtTotal") = ExciseAmtTotal
                dRow("gtdiscountAmt") = gtdiscountAmt
                dRow("INVH_MRP") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("INVH_MRP")))
                If ((dt4.Rows(i)("INVH_Mdate") <> "01-01-0001") And (dt4.Rows(i)("INVH_Mdate") <> "01/01/0001")) Then
                    dRow("INVH_Mdate") = objFasGnrl.FormatDtForRDBMS(dt4.Rows(i)("INVH_Mdate"), "D")
                Else
                    dRow("INVH_Mdate") = ""
                End If

                If ((dt4.Rows(i)("INVH_Edate") <> "01-01-1900") And (dt4.Rows(i)("INVH_Edate") <> "01/01/1900")) Then
                    dRow("INVH_Edate") = objFasGnrl.FormatDtForRDBMS(dt4.Rows(i)("INVH_Edate"), "D")
                Else
                    dRow("INVH_Edate") = ""
                End If

                dRow("GSTRate") = dt4.Rows(i)("GSTRate")
                dRow("GSTAmount") = String.Format("{0: 0.###}", (Convert.ToDecimal(dt4.Rows(i)("GSTAmount"))))
                dRow("CalculateAmount") = String.Format("{0:0.00}", Convert.ToDecimal(dt4.Rows(i)("CalculateAmount")))
                dRow("TotalAmount") = String.Format("{0:0.00}", (Convert.ToDecimal(dt4.Rows(i)("TotalAmount"))))

                dRow("HSNCode") = dt4.Rows(i)("HSNCode")

                dt.Rows.Add(dRow)
            Next
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)

            'Vat Bifurcation
            dtCalVat = dt.Copy
            Duniqe = objPOHR.RemoveDublicateVAT(dtCalVat)
            dtVatVBifrctn.Columns.Add("POD_Vat1")
            dtVatVBifrctn.Columns.Add("VATAmount")
            dtVatVBifrctn.Columns.Add("GrandVat")
            dtVatVBifrctn.Columns.Add("Grandamount")
            dtVatVBifrctn.Columns.Add("BasicAmount")
            For j = 0 To Duniqe.Rows.Count - 1
                dRow = dtVatVBifrctn.NewRow()
                totalavat = 0
                BasicAmount = 0
                dRow("POD_VAT1") = Duniqe.Rows(j)("VAT")
                For i = 0 To dt.Rows.Count - 1
                    If (Duniqe.Rows(j)("VAT") = dt.Rows(i)("VAT")) Then
                        totalavat = totalavat + dt.Rows(i)("VATAmt")
                        BasicAmount = BasicAmount + dt.Rows(i)("TotalAmount")
                        If (Duniqe.Rows(j)("CST") = 0 And Duniqe.Rows(j)("VAT") = 0) Then
                            dRow("POD_VAT1") = "Exempted"
                        End If
                    End If
                    dRow("VATAmount") = String.Format("{0:0.00}", Convert.ToDecimal(totalavat))
                    dRow("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(BasicAmount))
                Next
                GrandVat = GrandVat + totalavat
                Grandamount = Grandamount + BasicAmount
                dRow("Grandamount") = String.Format("{0:0.00}", Convert.ToDecimal(Grandamount))
                dRow("GrandVat") = String.Format("{0:0.00}", Convert.ToDecimal(GrandVat))
                dtVatVBifrctn.Rows.Add(dRow)
            Next
            Dim rdsVat As New ReportDataSource("DataSet2", dtVatVBifrctn)
            ReportViewer1.LocalReport.DataSources.Add(rdsVat)
            'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptPurchaseInvoiceViewerB.rdlc")
            'ReportViewer1.LocalReport.Refresh()

            'CST Bifurcation
            dtCalCst = dt.Copy
            Duniqe = objPOHR.RemoveDublicateVAT(dtCalCst)
            dtCstVBifrctn.Columns.Add("POD_Cst1")
            dtCstVBifrctn.Columns.Add("CSTAmount")
            dtCstVBifrctn.Columns.Add("GrandCst")
            dtCstVBifrctn.Columns.Add("Grandamount")
            dtCstVBifrctn.Columns.Add("BasicAmount")
            For j = 0 To Duniqe.Rows.Count - 1
                dRow = dtCstVBifrctn.NewRow()
                totalavat = 0
                BasicAmount = 0
                dRow("POD_Cst1") = Duniqe.Rows(j)("CST")
                For i = 0 To dt.Rows.Count - 1
                    If (Duniqe.Rows(j)("CST") = dt.Rows(i)("CST")) Then
                        totalavat = totalavat + dt.Rows(i)("CSTAmt")
                        BasicAmount = BasicAmount + dt.Rows(i)("TotalAmount")
                        If (Duniqe.Rows(j)("CST") = 0 And Duniqe.Rows(j)("VAT") = 0) Then
                            dRow("POD_Cst1") = "Exempted"
                        End If
                    End If
                    dRow("CSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(totalavat))
                    dRow("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(BasicAmount))
                Next
                GrandCst = GrandCst + totalavat
                GrandCSTamount = GrandCSTamount + BasicAmount
                dRow("Grandamount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandCSTamount))
                dRow("GrandCst") = String.Format("{0:0.00}", Convert.ToDecimal(GrandCst))
                dtCstVBifrctn.Rows.Add(dRow)
            Next
            Dim rdsCst As New ReportDataSource("DataSet3", dtCstVBifrctn)
            ReportViewer1.LocalReport.DataSources.Add(rdsCst)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/rptPurchaseInvoiceViewerB.rdlc")
            ReportViewer1.LocalReport.Refresh()
            '********************
            'Grand Vat & Grand Amount
            If Grandamount <> 0 Then
                Dim Grandamounts As ReportParameter() = New ReportParameter() {New ReportParameter("Grandamounts", Grandamount)}
                ReportViewer1.LocalReport.SetParameters(Grandamounts)
            Else
                Dim Grandamounts As ReportParameter() = New ReportParameter() {New ReportParameter("Grandamounts", "0.00")}
                ReportViewer1.LocalReport.SetParameters(Grandamounts)
            End If

            If GrandVat <> 0 Then
                Dim totavats As ReportParameter() = New ReportParameter() {New ReportParameter("totavats", GrandVat)}
                ReportViewer1.LocalReport.SetParameters(totavats)
            Else
                Dim totavats As ReportParameter() = New ReportParameter() {New ReportParameter("totavats", "0.00")}
                ReportViewer1.LocalReport.SetParameters(totavats)
            End If
            '**********************

            '********************
            'Grand CST & Grand Amount
            If GrandCSTamount <> 0 Then
                Dim GrandCSTamounts As ReportParameter() = New ReportParameter() {New ReportParameter("GrandCSTamounts", GrandCSTamount)}
                ReportViewer1.LocalReport.SetParameters(GrandCSTamounts)
            Else
                Dim GrandCSTamounts As ReportParameter() = New ReportParameter() {New ReportParameter("GrandCSTamounts", "0.00")}
                ReportViewer1.LocalReport.SetParameters(GrandCSTamounts)
            End If

            If GrandCst <> 0 Then
                Dim GrandCsts As ReportParameter() = New ReportParameter() {New ReportParameter("GrandCsts", GrandCst)}
                ReportViewer1.LocalReport.SetParameters(GrandCsts)
            Else
                Dim GrandCsts As ReportParameter() = New ReportParameter() {New ReportParameter("GrandCsts", "0.00")}
                ReportViewer1.LocalReport.SetParameters(GrandCsts)
            End If
            '**********************

            'Number To Word
            If Totalamt <> 0 Then
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", objPOHR.NumberToWord(String.Format("{0:0.00}", GrandTotal)) & "Only")}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            Else
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", "0")}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            End If
            'Terms and Conditions
            If objDb.SQLCheckForRecord(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21") Then
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", objDb.SQLGetDescription(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21"))}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            Else
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", " ")}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            End If
            Dim Odate As ReportParameter() = {New ReportParameter("Odate", "" & POM_OrderDate)}
            ReportViewer1.LocalReport.SetParameters(Odate)

            Dim InVoiceNo As ReportParameter() = {New ReportParameter("InVoiceNo", "" & PGM_DocumentRefNo)}
            ReportViewer1.LocalReport.SetParameters(InVoiceNo)

            Dim InvoiceDate As ReportParameter() = {New ReportParameter("InvoiceDate", "" & PGM_InvoiceDate)}
            ReportViewer1.LocalReport.SetParameters(InvoiceDate)

            Dim OOrderNo As ReportParameter() = {New ReportParameter("OOrderNo", "" & OrderNo1)}
            ReportViewer1.LocalReport.SetParameters(OOrderNo)

            Dim CCUST_CODE As ReportParameter() = {New ReportParameter("CCUST_CODE", "" & CUST_CODE)}
            ReportViewer1.LocalReport.SetParameters(CCUST_CODE)

            Dim CCUST_Name As ReportParameter() = {New ReportParameter("CCUST_Name", "" & CUST_Name)}
            ReportViewer1.LocalReport.SetParameters(CCUST_Name)

            Dim CCSM_Name As ReportParameter() = {New ReportParameter("CCSM_Name", "" & CSM_Name)}
            ReportViewer1.LocalReport.SetParameters(CCSM_Name)

            Dim CCSM_Address As ReportParameter() = {New ReportParameter("CCSM_Address", "" & CSM_Address)}
            ReportViewer1.LocalReport.SetParameters(CCSM_Address)

            Dim CCUST_COMM_ADDRESS As ReportParameter() = {New ReportParameter("CCUST_COMM_ADDRESS", "" & CUST_COMM_ADDRESS)}
            ReportViewer1.LocalReport.SetParameters(CCUST_COMM_ADDRESS)

            dtComp = objPOHR.GetCompanyMasterTemplete(sSession.AccessCode, sSession.AccessCodeID)
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
            dtVendor = objPOHR.GetVendorTemplete(sSession.AccessCode, sSession.AccessCodeID, iOrderID)
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

            Dim dtURD As New DataTable
            dtURD = objPOHR.GetURD(sSession.AccessCode, sSession.AccessCodeID, OrderNo, BillNo)

            Dim rdsURD As New ReportDataSource("DataSet4", dtURD)
            ReportViewer1.LocalReport.DataSources.Add(rdsURD)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/rptPurchaseInvoiceViewerB.rdlc")
            ReportViewer1.LocalReport.Refresh()

            Dim dtHSN As New DataTable
            dtHSN = objPOHR.GetHSN(sSession.AccessCode, sSession.AccessCodeID, OrderNo, BillNo)

            Dim rdsHSN As New ReportDataSource("DataSet5", dtHSN)
            ReportViewer1.LocalReport.DataSources.Add(rdsHSN)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/rptPurchaseInvoiceViewerB.rdlc")
            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loaddetailsItemVerified")
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
        Dim ctin As String = "" : Dim Cpan As String = "" : Dim Span As String = "" : Dim Stin As String = "" : Dim company As String = "" : Dim suplierId As String = "" : Dim temp1 As String = ""
        Dim Totalamt As String = "" : Dim OrderNo, BillNo, qty As Integer
        Dim TotalinWord As String = ""
        Dim Totaltax, DiscountAmt, GrandTotal, dimGtotal, CstAmnt, vatAmnt, ExceAmnt As Decimal
        'Dim Totalamt As Decimal
        Try
            company = objPOHR.GetAccessCode(sSession.AccessCode)
            dt0 = objPOHR.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, company)
            dt1 = objPOHR.LoadGridOtherDetails(sSession.AccessCode, sSession.AccessCodeID)
            For i = 0 To dt1.Rows.Count - 1
                temp1 = "" : temp1 = dt1.Rows(i).Item("Statutory Name")
                If temp1.Contains("TIN") Then
                    ctin = dt1.Rows(i).Item("Statutory Name") & "-" & dt1.Rows(i).Item("Statutory Value")
                End If
                temp1 = "" : temp1 = dt1.Rows(i).Item("Statutory Name")
                If temp1.Contains("PAN") Then
                    Cpan = dt1.Rows(i).Item("Statutory Name") & "-" & dt1.Rows(i).Item("Statutory Value")
                End If
            Next
            If ddlorder.SelectedValue <> 0 Then
                OrderNo = ddlorder.SelectedValue
            Else
                OrderNo = iOrderID
                ddlorder.SelectedValue = iOrderID
                ddlorder.Visible = False
                '   lblOrderNo.Visible = False
            End If
            dt2 = objPOHR.SupplierMaster(sSession.AccessCode, sSession.AccessCodeID, OrderNo)
            If (dt2.Rows.Count > 0) Then
                suplierId = dt2.Rows(0).Item("POM_Supplier")
                dt3 = objPOHR.SupplierDetails(sSession.AccessCode, sSession.AccessCodeID, suplierId)
                For i = 0 To dt3.Rows.Count - 1
                    temp1 = "" : temp1 = dt3.Rows(i).Item("CST_Description")
                    If temp1.Contains("TIN") Then
                        Stin = dt3.Rows(i).Item("CST_Description") & "-" & dt3.Rows(i).Item("CST_Value")
                    End If
                    temp1 = "" : temp1 = dt3.Rows(i).Item("CST_Description")
                    If temp1.Contains("PAN") Then
                        Span = dt3.Rows(i).Item("CST_Description") & "-" & dt3.Rows(i).Item("CST_Value")
                    End If
                Next
            End If
            If ddlinvoice.SelectedValue <> 0 Then
                BillNo = ddlinvoice.SelectedValue
            Else
                BillNo = ibillNo
                ddlinvoice.SelectedValue = ibillNo
                ddlinvoice.Visible = False
                'lblInvoiceNo.Visible = False
            End If

            dt4 = objPOHR.loadDetails(sSession.AccessCode, sSession.AccessCodeID, OrderNo, BillNo)
            For i = 0 To dt4.Rows.Count - 1
                If i = dt4.Rows.Count - 1 Then
                    Totalamt = dt4.Rows(i)("TotalAmount")
                    dimGtotal = dt4.Rows(i)("TotalAmount")
                    CstAmnt = CstAmnt + dt4.Rows(i)("CSTAmt")
                    vatAmnt = vatAmnt + dt4.Rows(i)("VATAmt")
                    ExceAmnt = ExceAmnt + dt4.Rows(i)("ExiseAmt")
                    TotalinWord = NumberToWord(String.Format("{0:0.00}", dt4.Rows(i)("TotalAmount"))) & " Only"
                End If
            Next
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Colour")
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
            dt5 = dt4.Copy

            Duniqe = objPOHR.RemoveDublicateItemVerified(dt5)
            For j = 0 To Duniqe.Rows.Count - 2
                dRow = dt.NewRow()
                qty = 0
                Totaltax = 0
                Totalamt = 0
                For i = 0 To dt4.Rows.Count - 2
                    dRow("SlNo") = i + 1
                    If dt4.Rows(i)("Commodity") = "<b>Total</b>" Then
                        dRow("SlNo") = ""
                    End If
                    dRow("Colour") = dt4.Rows(i)("Colour")
                    If (Duniqe.Rows(j)("Description") = dt4.Rows(i)("Description")) Then
                        If (dt4.Rows(i)("Description") <> "<b>Total</b>") Then
                            dRow("Description") = dt4.Rows(i)("Description")
                            dRow("Commodity") = dt4.Rows(i)("Commodity")
                            If (dt4.Rows(i)("t3") <> 0) Or (dt4.Rows(i)("t3").ToString() <> "") Then
                                '    dRow("t3") = 0
                                'Else
                                'dRow("t3") = Convert.ToInt32(dRow("t3")) + Convert.ToInt32(dt4.Rows(i)("t3"))
                                dRow("t3") = dt4.Rows(i)("t3")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t3"))
                            End If

                            If (dt4.Rows(i)("t4") <> 0) Then
                                '    dRow("t4") = 0
                                'Else
                                'dRow("t4") = Convert.ToInt32(dRow("t4")) + Convert.ToInt32(dt4.Rows(i)("t4"))
                                dRow("t4") = dt4.Rows(i)("t4")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t4"))
                            End If

                            If (dt4.Rows(i)("t5") <> 0) Then
                                '    dRow("t5") = 0
                                'Else
                                'dRow("t5") = Convert.ToInt32(dRow("t5")) + Convert.ToInt32(dt4.Rows(i)("t5"))
                                dRow("t5") = dt4.Rows(i)("t5")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t5"))
                            End If

                            If (dt4.Rows(i)("t6") <> 0) Then
                                '    dRow("t6") = 0
                                'Else
                                ' dRow("t6") = Convert.ToInt32(dRow("t6")) + Convert.ToInt32(dt4.Rows(i)("t6"))
                                dRow("t6") = dt4.Rows(i)("t6")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t6"))
                            End If

                            If (dt4.Rows(i)("t7") <> 0) Then
                                '    dRow("t7") = 0
                                'Else
                                'dRow("t7") = Convert.ToInt32(dRow("t7")) + Convert.ToInt32(dt4.Rows(i)("t7"))
                                dRow("t7") = dt4.Rows(i)("t7")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t7"))
                            End If

                            If (dt4.Rows(i)("t8") <> 0) Then
                                '    dRow("t8") = 0
                                'Else
                                'dRow("t8") = Convert.ToInt32(dRow("t8")) + Convert.ToInt32(dt4.Rows(i)("t8"))
                                dRow("t8") = dt4.Rows(i)("t8")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t8"))
                            End If

                            If (dt4.Rows(i)("t9") <> 0) Then
                                '    dRow("t9") = 0
                                'Else
                                dRow("t9") = dt4.Rows(i)("t9")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t9"))
                            End If
                            If (dt4.Rows(i)("t10") <> 0) Then
                                '    dRow("t10") = 0
                                'Else
                                'dRow("t10") = Convert.ToInt32(dRow("t10")) + Convert.ToInt32(dt4.Rows(i)("t10"))
                                dRow("t10") = dt4.Rows(i)("t10")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t10"))
                            End If

                            If (dt4.Rows(i)("t11") <> 0) Then
                                dRow("t11") = dt4.Rows(i)("t11")
                                qty = qty + Convert.ToInt32(dt4.Rows(i)("t11"))
                            End If
                            Totaltax = Totaltax + Convert.ToDecimal(dt4.Rows(i)("VATAmt")) + Convert.ToDecimal(dt4.Rows(i)("CSTAmt")) + Convert.ToDecimal(dt4.Rows(i)("ExiseAmt"))
                            dRow("MRP") = dt4.Rows(i)("Rate")
                            dRow("Rate") = dt4.Rows(i)("Rate")
                            DiscountAmt = DiscountAmt + Convert.ToDecimal(dt4.Rows(i)("DiscountAmt"))
                            'If (dt4.Rows(i)("CSTAmt") <> 0) Then
                            '    'dRow("CST") = Convert.ToDecimal(dt4.Rows(i)("CST"))
                            '    'dRow("CST") = dt4.Rows(i)("CST")
                            '    CstAmnt = CstAmnt + Convert.ToDecimal(dt4.Rows(i)("CSTAmt"))
                            'End If

                            'If (dt4.Rows(i)("ExiseAmt") <> 0) Then
                            '    'dRow("ExiseAmt") = dt4.Rows(i)("ExiseAmt") 'Convert.ToDecimal(dt4.Rows(i)("ExiseAmt"))
                            '    ExceAmnt = ExceAmnt + Convert.ToDecimal(dt4.Rows(i)("ExiseAmt"))
                            'End If
                            'If (dt4.Rows(i)("VATAmt") <> 0) Then
                            '    'dRow("VATAmt") = dt4.Rows(i)("VATAmt")  'Convert.ToDecimal(dt4.Rows(i)("VATAmt"))
                            '    vatAmnt = vatAmnt + Convert.ToDecimal(dt4.Rows(i)("VATAmt"))

                            'End If
                            Totalamt = Convert.ToDecimal(dRow("Rate")) * qty
                            'dimGtotal = dimGtotal + Totalamt
                            dRow("CSTAmt") = CstAmnt
                            dRow("ExiseAmt") = ExceAmnt
                            dRow("VATAmt") = vatAmnt
                            dRow("NetAmnt") = dimGtotal - (vatAmnt + ExceAmnt + CstAmnt)
                        End If
                    End If

                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(Totalamt))
                    dRow("TotalQty") = qty
                    dRow("DiscountAmt") = DiscountAmt
                    dRow("Discount") = dt4.Rows(i)("Discount")
                    dRow("VAT") = dt4.Rows(i)("VAT")
                    dRow("Exise") = dt4.Rows(i)("Exise")
                    dRow("CName") = objPOHR.GetAccessCode(sSession.AccessCode)
                    dRow("CAdd") = dt0.Rows(0).Item("CUST_COMM_ADDRESS")
                    dRow("CPh") = "Ph  " & dt0.Rows(0).Item("CUST_COMM_TEL")
                    dRow("CEmail") = "E-mail  " & dt0.Rows(0).Item("CUST_EMAIL")
                    dRow("Ctin") = ctin
                    dRow("CPan") = Cpan
                    dRow("InvoiceNO") = ddlinvoice.SelectedItem.Text
                    dRow("OrderNo") = ddlorder.SelectedItem.Text
                    If (dt2.Rows.Count > 0) Then
                        dRow("Saname") = dt2.Rows(0).Item("CSM_Name")
                        dRow("Sadd") = dt2.Rows(0).Item("CSM_Address")
                        dRow("Sph") = "Ph  " & dt2.Rows(0).Item("CSM_LandLineNo")
                        dRow("SEmail") = "E-mail  " & dt2.Rows(0).Item("CSM_EmailID")
                        dRow("Stin") = Stin
                        dRow("SPan") = Span
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rptPurchaseInvoiceViewer.rdlc")

            'dtComp = ClsPurchaseOrderHR.GetCompanyMasterTemplete(sSession.AccessCode, sSession.AccessCodeID)
            'If dtComp.Rows.Count > 0 Then
            '    For i = 0 To dtComp.Rows.Count - 1

            '        If dtComp.Rows(i)("CMP_Desc").ToString() = "CST" Then
            '            Dim CCST As ReportParameter() = New ReportParameter() {New ReportParameter("CCST", "<B>" & "CST : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
            '            ReportViewer1.LocalReport.SetParameters(CCST)
            '        Else
            '            Dim CCST As ReportParameter() = New ReportParameter() {New ReportParameter("CCST", "<B>" & "CST : " & "</B>" & "")}
            '            ReportViewer1.LocalReport.SetParameters(CCST)
            '        End If

            '        If dtComp.Rows(i)("CMP_Desc").ToString() = "VAT" Then
            '            Dim CVAT As ReportParameter() = New ReportParameter() {New ReportParameter("CVAT", "<B>" & "VAT : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
            '            ReportViewer1.LocalReport.SetParameters(CVAT)
            '        Else
            '            Dim CVAT As ReportParameter() = New ReportParameter() {New ReportParameter("CVAT", "<B>" & "VAT : " & "</B>" & "")}
            '            ReportViewer1.LocalReport.SetParameters(CVAT)
            '        End If


            '        If dtComp.Rows(i)("CMP_Desc").ToString() = "TIN" Then
            '            Dim CCTIN As ReportParameter() = New ReportParameter() {New ReportParameter("CTIN", "<B>" & "TIN : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
            '            ReportViewer1.LocalReport.SetParameters(CCTIN)
            '        Else
            '            Dim CCTIN As ReportParameter() = New ReportParameter() {New ReportParameter("CTIN", "<B>" & "TIN : " & "</B>" & "")}
            '            ReportViewer1.LocalReport.SetParameters(CCTIN)
            '        End If

            '        If dtComp.Rows(i)("CMP_Desc").ToString() = "TAN" Then
            '            Dim CTAN As ReportParameter() = New ReportParameter() {New ReportParameter("CTAN", "<B>" & "TAN : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
            '            ReportViewer1.LocalReport.SetParameters(CTAN)
            '        Else
            '            Dim CTAN As ReportParameter() = New ReportParameter() {New ReportParameter("CTAN", "<B>" & "TAN : " & "</B>" & "")}
            '            ReportViewer1.LocalReport.SetParameters(CTAN)
            '        End If

            '        If dtComp.Rows(i)("CMP_Desc").ToString() = "PAN" Then
            '            Dim CCPAN As ReportParameter() = New ReportParameter() {New ReportParameter("CPAN", "<B>" & "PAN : " & "</B>" & UCase(dtComp.Rows(i)("CMP_Value").ToString()))}
            '            ReportViewer1.LocalReport.SetParameters(CCPAN)
            '        Else
            '            Dim CCPAN As ReportParameter() = New ReportParameter() {New ReportParameter("CPAN", "<B>" & "PAN : " & "</B>" & "")}
            '            ReportViewer1.LocalReport.SetParameters(CCPAN)
            '        End If
            '    Next
            'End If


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
            Throw
        End Try
    End Sub
    Private Sub LoadOrder()
        Try
            ddlorder.DataSource = objPOHR.Order(sSession.AccessCode, sSession.AccessCodeID)
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
            ddlinvoice.DataSource = objPOHR.Invoice(sSession.AccessCode, sSession.AccessCodeID, ddlorder.SelectedValue)
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlorder_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub ddlinvoice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlinvoice.SelectedIndexChanged
        Try
            loaddetailsItemVerified(ddlorder.SelectedValue, ddlinvoice.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
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

