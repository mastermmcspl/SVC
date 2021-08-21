

Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Imports DatabaseLayer
Partial Class Reports_Purchase_PurchaseItemWise
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Report/Viewer/ReportVewer.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim sSession As New AllSession
    Dim objPo As New clsPurchaseOrder
    Dim objDB As New DBHelper
    Dim objOrderHR As New ClsPurchaseOrderHR
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iOrderID As Integer
        Dim dt As New DataTable
        Try
            imgbtnBack.ImageUrl = "~/Images/Backward24.png"
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

    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            'Response.Redirect(String.Format("~/Purchase/PurchaseOrder.aspx?"), False)
            Response.Redirect(String.Format("~/Purchase/OralOrder.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
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

    Private Sub showsetails(ByVal iOrderID As Integer)
        Dim totAmount As Decimal
        Dim totavat As Decimal
        Dim totalavat As Decimal, GrandVat As Decimal
        Dim totalCst As Decimal
        Dim Discount As Decimal
        Dim BasicAmount As Decimal = 0
        Dim Grandamount As Decimal = 0
        Dim CUST_COMM_ADDRESS As String = "", CUST_Name As String = "", CUST_EMAIL As String = "", CUST_COMM_TEL As String = "", CUST_CODE As String = "", CSM_Name As String = ""
        Dim CSM_Address As String = "", OrderNo As String = "", OrderDate As String = "", InvoiceNo As String = "", InvoiceDate As String = ""
        Dim CSM_MobileNo As String = ""
        Dim CSM_EmailID As String = ""
        Dim dtComp As New DataTable
        Dim dtVendor As New DataTable
        Dim Duniqe As New DataTable
        Dim dt As New DataTable
        Dim dtCalVat As New DataTable
        Dim dRow As DataRow
        Dim dtVatVBifrctn As New DataTable
        Dim NetAmount As Decimal
        Dim POM_DeliveryFromGSTNRegNo As String = "" : Dim POM_DeliveryGSTNRegNo As String = ""

        Try

            ReportViewer1.Reset()
            dt = objPo.GetAllDetails(sSession.AccessCode, sSession.AccessCodeID, iOrderID)
            For i = 0 To dt.Rows.Count - 1
                totAmount = totAmount + dt.Rows(i)("POD_RateAmount")
                NetAmount = NetAmount + dt.Rows(i)("POD_TotalAmount")
                totavat = totavat + dt.Rows(i)("POD_VATAMount")
                totalCst = totalCst + dt.Rows(i)("POD_CSTAmount")
                Discount = Discount + dt.Rows(i)("POD_DiscountAmount")
                CUST_COMM_ADDRESS = dt.Rows(i)("CUST_COMM_ADDRESS")
                CUST_EMAIL = dt.Rows(i)("CUST_EMAIL")
                CUST_COMM_TEL = dt.Rows(i)("CUST_COMM_TEL")
                CUST_CODE = dt.Rows(i)("CUST_CODE")
                CUST_Name = dt.Rows(i)("CUST_NAME")
                CSM_Name = dt.Rows(i)("CSM_Name")
                CSM_Address = dt.Rows(i)("CSM_Address")
                CSM_MobileNo = dt.Rows(i)("CSM_MobileNo")
                CSM_EmailID = dt.Rows(i)("CSM_EmailID")
                OrderDate = dt.Rows(i)("POM_OrderDate")
                OrderNo = dt.Rows(i)("POM_OrderNo")

                POM_DeliveryFromGSTNRegNo = dt.Rows(i)("POM_DeliveryFromGSTNRegNo")
                POM_DeliveryGSTNRegNo = dt.Rows(i)("POM_DeliveryGSTNRegNo")
            Next
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            dtCalVat = dt.Copy
            Duniqe = objPo.RemoveDublicate(dtCalVat)
            dtVatVBifrctn.Columns.Add("POD_Vat1")
            dtVatVBifrctn.Columns.Add("VATAmount")
            dtVatVBifrctn.Columns.Add("BasicAmount")
            dtVatVBifrctn.Columns.Add("GrandVat")
            dtVatVBifrctn.Columns.Add("Grandamount")
            'Vat Bifurcation********
            For j = 0 To Duniqe.Rows.Count - 1
                dRow = dtVatVBifrctn.NewRow()
                totalavat = 0
                BasicAmount = 0
                dRow("POD_VAT1") = Duniqe.Rows(j)("POD_VAT")
                For i = 0 To dt.Rows.Count - 1

                    If (Duniqe.Rows(j)("POD_VAT") = dt.Rows(i)("POD_VAT")) Then
                        totalavat = totalavat + dt.Rows(i)("POD_VATAmount")
                        BasicAmount = BasicAmount + dt.Rows(i)("POD_RateAmount")
                        If (Duniqe.Rows(j)("POD_VAT") = 0) Then
                            dRow("POD_VAT1") = "Exempted"
                        End If
                    End If
                    dRow("VATAmount") = totalavat
                    dRow("BasicAmount") = BasicAmount
                Next
                GrandVat = GrandVat + totalavat
                Grandamount = Grandamount + BasicAmount
                dRow("Grandamount") = Grandamount
                dRow("GrandVat") = totavat
                dtVatVBifrctn.Rows.Add(dRow)
            Next
            Dim rdsVat As New ReportDataSource("DataSet2", dtVatVBifrctn)
            ReportViewer1.LocalReport.DataSources.Add(rdsVat)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/RptPurchaseOrder.rdlc")
            ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
            ReportViewer1.ZoomPercent = 125
            '********************
            'Terms and Condtions
            If objDB.SQLCheckForRecord(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21") Then
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", objDB.SQLGetDescription(sSession.AccessCode, "select Mas_desc from ACC_General_Master where Mas_master=21"))}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            Else
                Dim TermsCondtions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsCondtions", " ")}
                ReportViewer1.LocalReport.SetParameters(TermsCondtions)
            End If
            '********************
            'Grand Vat & Grand Amount
            If NetAmount <> 0 Then
                NetAmount = (NetAmount)
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", GetInWords(String.Format("{0:0.00}", NetAmount), String.Format("{0:0.00}", NetAmount)))}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            Else
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", "0")}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            End If

            'If Grandamount <> 0 Then
            '    Dim Grandamounts As ReportParameter() = New ReportParameter() {New ReportParameter("Grandamounts", Grandamount)}
            '    ReportViewer1.LocalReport.SetParameters(Grandamounts)
            'Else
            '    Dim Grandamounts As ReportParameter() = New ReportParameter() {New ReportParameter("Grandamounts", "0.00")}
            '    ReportViewer1.LocalReport.SetParameters(Grandamounts)
            'End If
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
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", GetInWords(String.Format("{0:0.00}", totAmount), String.Format("{0:0.00}", totAmount)))}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            Else
                Dim NoToWord As ReportParameter() = New ReportParameter() {New ReportParameter("NoToWord", "0")}
                ReportViewer1.LocalReport.SetParameters(NoToWord)
            End If

            If OrderNo <> "" Then
                Dim OOrderNo As ReportParameter() = {New ReportParameter("OOrderNo", "" & OrderNo)}
                ReportViewer1.LocalReport.SetParameters(OOrderNo)

            Else
                Dim OOrderNo As ReportParameter() = New ReportParameter() {New ReportParameter("OOrderNo", " ")}
                ReportViewer1.LocalReport.SetParameters(OOrderNo)
            End If

            If OrderDate <> "" Then
                Dim OOrderDate As ReportParameter() = {New ReportParameter("OOrderDate", "" & OrderDate)}
                ReportViewer1.LocalReport.SetParameters(OOrderDate)
            Else
                Dim OOrderDate As ReportParameter() = New ReportParameter() {New ReportParameter("OOrderDate", " ")}
                ReportViewer1.LocalReport.SetParameters(OOrderDate)
            End If

            If CUST_CODE <> "" Then
                Dim CCUST_CODE As ReportParameter() = {New ReportParameter("CCUST_CODE", "" & CUST_CODE)}
                ReportViewer1.LocalReport.SetParameters(CCUST_CODE)
            Else
                Dim CCUST_CODE As ReportParameter() = New ReportParameter() {New ReportParameter("CCUST_CODE", " ")}
                ReportViewer1.LocalReport.SetParameters(CCUST_CODE)
            End If

            If CUST_Name <> "" Then
                Dim CCUST_Name As ReportParameter() = {New ReportParameter("CCUST_Name", "" & CUST_Name)}
                ReportViewer1.LocalReport.SetParameters(CCUST_Name)
            Else
                Dim CCUST_Name As ReportParameter() = New ReportParameter() {New ReportParameter("CCUST_Name", " ")}
                ReportViewer1.LocalReport.SetParameters(CCUST_Name)
            End If

            If CSM_Name <> "" Then
                Dim CCSM_Name As ReportParameter() = {New ReportParameter("CCSM_Name", "" & CSM_Name)}
                ReportViewer1.LocalReport.SetParameters(CCSM_Name)
            Else
                Dim CCSM_Name As ReportParameter() = New ReportParameter() {New ReportParameter("CCSM_Name", " ")}
                ReportViewer1.LocalReport.SetParameters(CCSM_Name)
            End If

            If POM_DeliveryFromGSTNRegNo <> "" Then
                Dim SPOM_DeliveryFromGSTNRegNo As ReportParameter() = {New ReportParameter("SPOM_DeliveryFromGSTNRegNo", "" & POM_DeliveryFromGSTNRegNo)}
                ReportViewer1.LocalReport.SetParameters(SPOM_DeliveryFromGSTNRegNo)
            Else
                Dim SPOM_DeliveryFromGSTNRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("SPOM_DeliveryFromGSTNRegNo", " ")}
                ReportViewer1.LocalReport.SetParameters(SPOM_DeliveryFromGSTNRegNo)
            End If

            If CSM_Address <> "" Then
                Dim CCSM_Address As ReportParameter() = {New ReportParameter("CCSM_Address", "" & CSM_Address)}
                ReportViewer1.LocalReport.SetParameters(CCSM_Address)
            Else
                Dim CCSM_Address As ReportParameter() = New ReportParameter() {New ReportParameter("CCSM_Address", " ")}
                ReportViewer1.LocalReport.SetParameters(CCSM_Address)
            End If

            If CUST_COMM_ADDRESS <> "" Then
                Dim CCUST_COMM_ADDRESS As ReportParameter() = {New ReportParameter("CCUST_COMM_ADDRESS", "" & CUST_COMM_ADDRESS)}
                ReportViewer1.LocalReport.SetParameters(CCUST_COMM_ADDRESS)
            Else
                Dim CCUST_COMM_ADDRESS As ReportParameter() = New ReportParameter() {New ReportParameter("CCUST_COMM_ADDRESS", " ")}
                ReportViewer1.LocalReport.SetParameters(CCUST_COMM_ADDRESS)
            End If

            If POM_DeliveryGSTNRegNo <> "" Then
                Dim SPOM_DeliveryGSTNRegNo As ReportParameter() = {New ReportParameter("SPOM_DeliveryGSTNRegNo", "" & POM_DeliveryGSTNRegNo)}
                ReportViewer1.LocalReport.SetParameters(SPOM_DeliveryGSTNRegNo)
            Else
                Dim SPOM_DeliveryGSTNRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("SPOM_DeliveryGSTNRegNo", " ")}
                ReportViewer1.LocalReport.SetParameters(SPOM_DeliveryGSTNRegNo)
            End If

            dtComp = objOrderHR.GetCompanyMasterTemplete(sSession.AccessCode, sSession.AccessCodeID)
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
            dtVendor = objOrderHR.GetVendorTemplete(sSession.AccessCode, sSession.AccessCodeID, iOrderID)
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
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RDLC/Purchase/RptPurchaseOrder.rdlc")
            ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "showsetails")
        End Try
    End Sub


    Public Function GetInWords(ByVal num As Long, ByVal num1 As String) As String
        On Error Resume Next
        Dim str As String
        Dim subnum As Long
        Dim Digits As New TextBox
        Dim aftrdecimalWord As String
        Dim aftrDecimal As Double
        str = ""
        Digits.Text = num.ToString

        If Len(Digits.Text) > 11 Then
            str = GetSubInWords(CLng(Mid(Digits.Text, 1, Len(Digits.Text) - 9)))

            Digits.Text = Mid(Digits.Text, Len(Digits.Text) - 9 + 1, 9)

        End If

        If Len(Digits.Text) = 11 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Billion "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Billion "
            End If
            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 9, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 10, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str
        End If
        If Len(Digits.Text) = 10 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Billion "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Billion "
            End If
            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 8, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 9, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                ' str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str
        End If
        If Len(Digits.Text) = 9 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 8, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                ' str += " Rupees only "
            End If
            str = str
        End If
        If Len(Digits.Text) = 8 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str
        End If
        If Len(Digits.Text) = 7 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str
        End If
        If Len(Digits.Text) = 6 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str
        End If
        If Len(Digits.Text) = 5 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str
        End If

        If Len(Digits.Text) = 4 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str

        End If
        If Len(Digits.Text) = 3 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str

        End If
        If Len(Digits.Text) = 2 Or Len(Digits.Text) = 1 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Rupees only "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Rupees only "
            End If
            str = str
        End If
        If Len(Digits.Text) = 0 Then
            str = ""
        End If


        If (num1.Contains(".")) Then
            Dim str1 As String() = Strings.Split(num1, ".")
            aftrDecimal = Convert.ToDouble(str(1))
            aftrdecimalWord = AfterDecimalfunction(aftrDecimal)
            If aftrdecimalWord = "zero" Then
                str += ""
            Else
                aftrdecimalWord += " Paise"
                str += " and " + aftrdecimalWord

            End If
        End If


        Return str

    End Function
    Public Function GetTens(ByVal num As Integer) As String
        On Error Resume Next
        Select Case (num)
            Case 0
                Return ("")
            Case 1
                Return ("One")
            Case 2
                Return ("Two")
            Case 3
                Return ("Three")
            Case 4
                Return ("Four")
            Case 5
                Return ("Five")
            Case 6
                Return ("Six")
            Case 7
                Return ("Seven")
            Case 8
                Return ("Eight")
            Case 9
                Return ("Nine")
            Case 10
                Return ("Ten")
            Case 11
                Return ("Eleven")
            Case 12
                Return ("Twelve")
            Case 13
                Return ("Thirteen")
            Case 14
                Return ("Fourteen")
            Case 15
                Return ("Fifteen")
            Case 16
                Return ("Sixteen")
            Case 17
                Return ("Seventeen")
            Case 18
                Return ("Eighteen")
            Case 19
                Return ("Nineteen")

        End Select

        Return ("")

    End Function
    Public Function GetTwenty(ByVal num As Integer) As String
        On Error Resume Next
        Select Case (num)
            Case 0
                Return ("")
            Case 1
                Return ("One")
            Case 2
                Return ("Twenty")
            Case 3
                Return ("Thirty")
            Case 4
                Return ("Forty")
            Case 5
                Return ("Fifty")
            Case 6
                Return ("Sixty")
            Case 7
                Return ("Seventy")
            Case 8
                Return ("Eighty")
            Case 9
                Return ("Ninety")

        End Select
        Return ("")
    End Function
    Public Function GetSubInWords(ByVal num As Long) As String
        On Error Resume Next
        Dim str As String
        Dim subnum As Long
        Dim Digits As New TextBox
        str = ""
        Digits.Text = num.ToString
        If Len(Digits.Text) = 11 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Billion "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Billion "
            End If
            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 9, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 10, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)

            ElseIf subnum > 0 Then
                str += GetTens(subnum)

            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 10 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Billion "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Billion "
            End If
            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 8, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 9, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)

            ElseIf subnum > 0 Then
                str += GetTens(subnum)

            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 9 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakhs "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakhs "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 8, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)

            ElseIf subnum > 0 Then
                str += GetTens(subnum)

            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 8 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Crores "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Crores "
            End If
            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 7, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)

            ElseIf subnum > 0 Then
                str += GetTens(subnum)

            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 7 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 6, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                ' str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 6 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)

                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Lakh "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Lakh "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 5, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 5 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 4, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If

        If Len(Digits.Text) = 4 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Thousand "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Thousand "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 3, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 3 Then
            subnum = CLng(Mid(Digits.Text, 1, 1))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                str = str + " Hundred "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                str += " Hundred "
            End If

            subnum = CLng(Mid(Digits.Text, 2, 2))

            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 2 Or Len(Digits.Text) = 1 Then
            subnum = CLng(Mid(Digits.Text, 1, 2))
            If subnum >= 20 Then
                str = str + GetTwenty(subnum \ 10)
                str = str + " " + GetTens(subnum Mod 10)
                'str = str + " Billions And "
            ElseIf subnum > 0 Then
                str += GetTens(subnum)
                'str += " Billions And "
            End If
            str += " Billions And "
        End If
        If Len(Digits.Text) = 0 Then
            str = ""
        End If

        Return str

    End Function

    Public Function AfterDecimalfunction(ByVal num As Decimal) As String
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

    Public Function NumberToWord(ByVal num1 As String) As String
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
End Class

