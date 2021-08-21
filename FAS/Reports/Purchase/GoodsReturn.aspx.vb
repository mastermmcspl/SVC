
'Partial Class Reports_Purchase_GoodsReturn
'    Inherits System.Web.UI.Page

'End Class
Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports System.Text
Imports Microsoft.Reporting.WebForms
Partial Class Reports_Purchase_GoodsReturn
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Report\Viewer\PurchaseReturn.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objPreturn As New clsPurchaseReturn
    Dim objDb As New DBHelper
    Dim objPO As New clsPurchaseOrder

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iPurchaseReturnID As Integer
        Try
            imgbtnBack.ImageUrl = "~/Images/Backward24.png"
            sSession = Session("AllSession")
            ' lblErrorUp.Text = ""
            If IsPostBack = False Then
                ' CheckAuidtPermission(sSession.AccessCode, sSession.UserID)
                '  LoadInvoice()
                iPurchaseReturnID = Request.QueryString("ExistingOrder")
                If iPurchaseReturnID > 0 Then
                    showsetails(iPurchaseReturnID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    'Private Sub LoadInvoice()
    '    Try
    '        ddlPurchaseReturnNo.DataSource = clsPurchaseReturnRPT.LoadPurchaseReturnOrders(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
    '        ddlPurchaseReturnNo.DataTextField = "PRM_ReturnOrderCode"
    '        ddlPurchaseReturnNo.DataValueField = "PRM_ID"
    '        ddlPurchaseReturnNo.DataBind()
    '        ddlPurchaseReturnNo.Items.Insert(0, New ListItem("--- Select Purchase Return No. ---", "0"))
    '    Catch ex As Exception
    '        lblErrorUp.Text = ex.Message
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCommodity")
    '    End Try
    'End Sub
    'Public Sub CheckAuidtPermission(ByVal sNameSpace As String, ByVal iUsrId As Integer)
    '    Dim sbret As String
    '    Try
    '        sbret = clsGeneralMaster.CheckUmsPermit(sNameSpace, sSession.AccessCodeID, iUsrId, "FasDORIN", "ALL")
    '        If sbret = "False" Or sbret = "" Then
    '            Response.Redirect("~/Permissions/SalesPermission.aspx")
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Protected Sub ReportViewer_OnLoad(sender As Object, e As EventArgs)
        'String exportOption1 = "Word";
        Dim exportOption1 As String = "Word"
        Dim exportOption As String = "Excel"
        Dim extension As RenderingExtension = ReportViewer1.LocalReport.ListRenderingExtensions().ToList().Find(Function(x) x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase))
        If extension IsNot Nothing Then
            Dim fieldInfo As System.Reflection.FieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
            fieldInfo.SetValue(extension, False)

            'Dim extension1 As RenderingExtension = ReportViewer1.LocalReport.ListRenderingExtensions().ToList().Find(Function(x) x.Name.Equals(extension1, StringComparison.CurrentCultureIgnoreCase))
            'If extension IsNot Nothing Then
            '    Dim fieldInfo1 As System.Reflection.FieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance Or System.Reflection.BindingFlags.NonPublic)
            '    fieldInfo.SetValue(extension, False)
            'End If
        End If
    End Sub
    Private Sub showsetails(ByVal iPurchaseReturnID As Integer)
        Try

            Dim totAmount As Decimal
            Dim totavat As Decimal
            Dim totalavat As Decimal, GrandVat As Decimal
            Dim totalCst As Decimal
            Dim Discount As Decimal
            Dim BasicAmount As Decimal = 0
            Dim Grandamount As Decimal = 0
            Dim CUST_COMM_ADDRESS As String = "", CUST_Name As String = "", CUST_EMAIL As String = "", CUST_COMM_TEL As String = "", CUST_CODE As String = "", CSM_Name As String = ""
            Dim CSM_Address As String = "", OrderNo As String = "", OrderDate As String = "", InvoiceNo As String = "", InvoiceDate As String = "", Remark As String = ""
            Dim CSM_MobileNo As String = ""
            Dim CSM_EmailID As String = ""
            Dim dtComp As New DataTable
            Dim dtVendor As New DataTable
            Dim Duniqe As New DataTable
            Dim dt As New DataTable
            Dim dtCalVat As New DataTable
            Dim dRow As DataRow
            Dim dtVatVBifrctn As New DataTable
            ReportViewer1.Reset()
            ReportViewer1.Reset()
            dt = objPreturn.GoodsReturnAllDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iPurchaseReturnID)
            'Dim rds As New ReportDataSource("DataSet1", dt)
            'ReportViewer1.LocalReport.DataSources.Add(rds)
            'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Purchase/RDLC/RptPurchaseReturn.rdlc")
            'ReportViewer1.LocalReport.Refresh()
            For i = 0 To dt.Rows.Count - 1
                totAmount = totAmount + dt.Rows(i)("PRD_RateAmount")
                totavat = totavat + dt.Rows(i)("PRD_VATAMount")
                totalCst = totalCst + dt.Rows(i)("PRD_CSTAmount")
                Discount = Discount + dt.Rows(i)("PRD_DiscountAmount")
                CUST_COMM_ADDRESS = dt.Rows(i)("CUST_COMM_ADDRESS")
                CUST_EMAIL = dt.Rows(i)("CUST_EMAIL")
                CUST_COMM_TEL = dt.Rows(i)("CUST_COMM_TEL")
                CUST_CODE = dt.Rows(i)("CUST_CODE")
                CUST_Name = dt.Rows(i)("CUST_NAME")
                CSM_Name = dt.Rows(i)("CSM_Name")
                CSM_Address = dt.Rows(i)("CSM_Address")
                CSM_MobileNo = dt.Rows(i)("CSM_MobileNo")
                CSM_EmailID = dt.Rows(i)("CSM_EmailID")
                OrderDate = dt.Rows(i)("PRM_OrderDate")
                OrderNo = dt.Rows(i)("PRM_ReturnNo")
                Remark = dt.Rows(i)("PRM_Remarks")
            Next
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            dtCalVat = dt.Copy
            'Duniqe = clsPurchaseOrder.RemoveDublicate(dtCalVat)
            'dtVatVBifrctn.Columns.Add("PRD_Vat1")
            'dtVatVBifrctn.Columns.Add("VATAmount")
            'dtVatVBifrctn.Columns.Add("BasicAmount")
            'dtVatVBifrctn.Columns.Add("GrandVat")
            'dtVatVBifrctn.Columns.Add("Grandamount")
            '   Vat Bifurcation ********
            'For j = 0 To Duniqe.Rows.Count - 1
            '    dRow = dtVatVBifrctn.NewRow()
            'totalavat = 0
            'BasicAmount = 0
            'dRow("PRD_VAT1") = Duniqe.Rows(j)("PRD_VAT")
            'For i = 0 To dt.Rows.Count - 1

            '    If (Duniqe.Rows(j)("PRD_VAT") = dt.Rows(i)("PRD_VAT")) Then
            '        totalavat = totalavat + dt.Rows(i)("PRD_VATAmount")
            '        BasicAmount = BasicAmount + dt.Rows(i)("PRD_RateAmount")
            '        If (Duniqe.Rows(j)("PRD_VAT") = 0) Then
            '            dRow("PRD_VAT1") = "Exempted"
            '        End If
            '    End If
            '    dRow("VATAmount") = totalavat
            '    dRow("BasicAmount") = BasicAmount
            'Next
            'GrandVat = GrandVat + totalavat
            'Grandamount = Grandamount + BasicAmount
            'dRow("Grandamount") = Grandamount
            'dRow("GrandVat") = totavat
            'dtVatVBifrctn.Rows.Add(dRow)
            'Next
            'Dim rdsVat As New ReportDataSource("DataSet2", dtVatVBifrctn)
            'ReportViewer1.LocalReport.DataSources.Add(rdsVat)

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/RptPurchaseReturn.rdlc")
            ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
            ReportViewer1.ZoomPercent = 125


            If Remark <> "" Then
                Dim Remarks As ReportParameter() = New ReportParameter() {New ReportParameter("Remarks", Remark)}
                ReportViewer1.LocalReport.SetParameters(Remarks)
            Else
                Dim Grandamounts As ReportParameter() = New ReportParameter() {New ReportParameter("Remarks", "0.00")}
                ReportViewer1.LocalReport.SetParameters(Grandamounts)
            End If
            '********************
            ' Terms And Condtions
            If objDb.SQLCheckForRecord(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21") Then
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", objDb.SQLGetDescription(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21"))}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            Else
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", " ")}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            End If
            '********************
            'Grand Vat & Grand Amount
            If Grandamount <> 0 Then
                Dim Grandamounts As ReportParameter() = New ReportParameter() {New ReportParameter("Grandamounts", Grandamount)}
                ReportViewer1.LocalReport.SetParameters(Grandamounts)
            Else
                Dim Grandamounts As ReportParameter() = New ReportParameter() {New ReportParameter("Grandamounts", "0.00")}
                ReportViewer1.LocalReport.SetParameters(Grandamounts)
            End If
            If totavat <> 0 Then
                Dim totavats As ReportParameter() = New ReportParameter() {New ReportParameter("totavats", totavat)}
                ReportViewer1.LocalReport.SetParameters(totavats)
            Else
                Dim totavats As ReportParameter() = New ReportParameter() {New ReportParameter("totavats", "0.00")}
                ReportViewer1.LocalReport.SetParameters(totavats)
            End If
            '**********************
            If totAmount <> 0 Then
                totAmount = (totAmount + totavat + totalCst)
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", objPreturn.NumberToWord(String.Format("{0:0.00}", totAmount)) & "Only")}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            Else
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", "0")}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            End If

            Dim OOrderNo As ReportParameter() = {New ReportParameter("OOrderNo", "" & OrderNo)}
            ReportViewer1.LocalReport.SetParameters(OOrderNo)

            Dim OOrderDate As ReportParameter() = {New ReportParameter("OOrderDate", "" & OrderDate)}
            ReportViewer1.LocalReport.SetParameters(OOrderDate)

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

            dtComp = objPreturn.GetCompanyMasterTemplete(sSession.AccessCode, sSession.AccessCodeID)
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
            dtVendor = objPreturn.GetVendorTemplete(sSession.AccessCode, sSession.AccessCodeID, iPurchaseReturnID)
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
            ReportViewer1.LocalReport.Refresh()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "showsetails")
        End Try
    End Sub

    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try

            Response.Redirect(String.Format("~/Purchase/PurchaseReturn.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
End Class
