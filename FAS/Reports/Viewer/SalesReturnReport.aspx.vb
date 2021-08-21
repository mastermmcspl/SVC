Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Reports_Viewer_SalesReturnReport
    Inherits System.Web.UI.Page
    Private sFormName As String = "Reports_Viewer_SalesReturnReport"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objSRR As New clsSalesReturnReport
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadSalesReturnNo()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub LoadSalesReturnNo()
        Try
            ddlSalesReturn.DataSource = objSRR.BindDispatchNo(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
            ddlSalesReturn.DataTextField = "SRM_ReturnOrderCode"
            ddlSalesReturn.DataValueField = "SRM_ID"
            ddlSalesReturn.DataBind()
            ddlSalesReturn.Items.Insert(0, New ListItem("--- Select Return No. ---", "0"))
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCommodity")
        End Try
    End Sub
    Private Sub ddlSalesReturn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSalesReturn.SelectedIndexChanged
        Try
            loadDetails(sSession.AccessCode, sSession.AccessCodeID, ddlSalesReturn.SelectedValue)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub loadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSalesReturnID As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim i As Integer

        Dim dAmountTot, dVATTot, dCSTTot, dExciseTot, dGrandDis, dGrandDiscountAmt, dShipping As Double
        Dim TotalinWord As String = ""
        Dim iTotalQty As Integer

        Dim intpart As Integer
        Dim decpart As Double
        Try
            dt.Columns.Add("CName")
            dt.Columns.Add("CAddress")
            dt.Columns.Add("CTel")
            dt.Columns.Add("CEmail")
            dt.Columns.Add("CVAT")
            dt.Columns.Add("CTAX")
            dt.Columns.Add("CPAN")
            dt.Columns.Add("CTAN")
            dt.Columns.Add("CTIN")
            dt.Columns.Add("CCIN")

            dt.Columns.Add("BName")
            dt.Columns.Add("BAddress")
            dt.Columns.Add("BTel")
            dt.Columns.Add("BEmail")
            dt.Columns.Add("BVAT")
            dt.Columns.Add("BTAX")
            dt.Columns.Add("BPAN")
            dt.Columns.Add("BTAN")
            dt.Columns.Add("BTIN")
            dt.Columns.Add("BCIN")

            dt.Columns.Add("OrderNo")
            dt.Columns.Add("OrderDate")
            dt.Columns.Add("DispatchNo")
            dt.Columns.Add("DispatchDate")
            dt.Columns.Add("DispatchedThrough")
            dt.Columns.Add("ESugamNo")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("BuyerOrderNo")
            dt.Columns.Add("BuyerOrderDate")
            dt.Columns.Add("DispatchRefNo")

            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Unit")
            dt.Columns.Add("Qty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("RateAmount")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("Amount")

            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmount")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmount")
            dt.Columns.Add("Excise")
            dt.Columns.Add("ExciseAmount")
            dt.Columns.Add("GrandDiscount")
            dt.Columns.Add("GrandDiscountAmt")
            dt.Columns.Add("Shipping")

            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("TotalinWord")

            dt.Columns.Add("ManufactureDate")
            dt.Columns.Add("ExpireDate")

            dt.Columns.Add("ReturnCode")
            dt.Columns.Add("ReturnDate")

            dt1 = objSRR.GetDataOral(sNameSpace, iCompID, iSalesReturnID)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()

                    dRow("CName") = dt1.Rows(i)("CUST_NAME")
                    dRow("CAddress") = dt1.Rows(i)("CUST_Comm_Address")
                    dRow("CTel") = dt1.Rows(i)("CUST_Comm_Tel")
                    dRow("CEmail") = dt1.Rows(i)("CUST_Email")

                    dRow("CVAT") = dt1.Rows(i)("CVAT")
                    dRow("CTAX") = dt1.Rows(i)("CTAX")
                    dRow("CPAN") = dt1.Rows(i)("CPAN")
                    dRow("CTAN") = dt1.Rows(i)("CTAN")
                    dRow("CTIN") = dt1.Rows(i)("CTIN")
                    dRow("CCIN") = dt1.Rows(i)("CCIN")

                    dRow("BName") = dt1.Rows(i)("BM_Name")
                    dRow("BAddress") = dt1.Rows(i)("BM_Address")
                    dRow("BTel") = dt1.Rows(i)("BM_MobileNo")
                    dRow("BEmail") = dt1.Rows(i)("BM_EmailID")

                    dRow("BVAT") = dt1.Rows(i)("BVAT")
                    dRow("BTAX") = dt1.Rows(i)("BTAX")
                    dRow("BPAN") = dt1.Rows(i)("BPAN")
                    dRow("BTAN") = dt1.Rows(i)("BTAN")
                    dRow("BTIN") = dt1.Rows(i)("BTIN")
                    dRow("BCIN") = dt1.Rows(i)("BCIN")

                    dRow("OrderNo") = dt1.Rows(i)("SPO_OrderCode")
                    dRow("OrderDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_OrderDate"), "D")
                    dRow("DispatchNo") = dt1.Rows(i)("SDM_Code")
                    dRow("DispatchDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SDM_DispatchDate"), "D")
                    dRow("DispatchedThrough") = dt1.Rows(i)("ModeOfDispatch")
                    If IsDBNull(dt1.Rows(i)("SDM_ESugamNo")) = False Then
                        dRow("ESugamNo") = dt1.Rows(i)("SDM_ESugamNo")
                    Else
                        dRow("ESugamNo") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("SRM_Narration")) = False Then
                        dRow("Remarks") = dt1.Rows(i)("SRM_Narration")
                    Else
                        dRow("Remarks") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("SPO_BuyerOrderNo")) = False Then
                        dRow("BuyerOrderNo") = dt1.Rows(i)("SPO_BuyerOrderNo")
                    Else
                        dRow("BuyerOrderNo") = ""
                    End If
                    If objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_BuyerOrderDate"), "D") <> "30/12/1899" Then
                        dRow("BuyerOrderDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SPO_BuyerOrderDate"), "D")
                    Else
                        dRow("BuyerOrderDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("SDM_DispatchRefNo")) = False Then
                        dRow("DispatchRefNo") = dt1.Rows(i)("SDM_DispatchRefNo")
                    Else
                        dRow("DispatchRefNo") = ""
                    End If


                    dRow("SlNo") = i + 1
                    dRow("Commodity") = dt1.Rows(i)("Commodity")
                    dRow("Description") = dt1.Rows(i)("INV_Code")
                    dRow("Unit") = dt1.Rows(i)("Unit")
                    dRow("Qty") = dt1.Rows(i)("SDD_Quantity")
                    iTotalQty = iTotalQty + dt1.Rows(i)("SDD_Quantity")

                    dRow("Rate") = dt1.Rows(i)("SDD_Rate")
                    dRow("RateAmount") = dt1.Rows(i)("SDD_RateAmount")
                    dRow("Discount") = dt1.Rows(i)("SDD_Discount")
                    dRow("DiscountAmt") = dt1.Rows(i)("SDD_DiscountAmount")
                    dRow("Amount") = dt1.Rows(i)("SDD_TotalAmount")
                    dAmountTot = dAmountTot + dt1.Rows(i)("SDD_TotalAmount")

                    dRow("VAT") = objSRR.GetVAT(sSession.AccessCode, sSession.AccessCodeID, dt1.Rows(i)("SDD_VAT"))
                    dRow("VATAmount") = dt1.Rows(i)("SDD_VATAmount")
                    dVATTot = dVATTot + dt1.Rows(i)("SDD_VATAmount")

                    dRow("CST") = objSRR.GetCST(sSession.AccessCode, sSession.AccessCodeID, dt1.Rows(i)("SDD_CST"))
                    dRow("CSTAmount") = dt1.Rows(i)("SDD_CSTAmount")
                    dCSTTot = dCSTTot + dt1.Rows(i)("SDD_CSTAmount")

                    dRow("Excise") = dt1.Rows(i)("SDD_Excise")
                    dRow("ExciseAmount") = dt1.Rows(i)("SDD_ExciseAmount")
                    dExciseTot = dExciseTot + dt1.Rows(i)("SDD_ExciseAmount")

                    If IsDBNull(dt1.Rows(i)("TradeDiscount")) = False Then
                        dRow("GrandDiscount") = dt1.Rows(i)("TradeDiscount")
                        dGrandDis = dt1.Rows(i)("TradeDiscount")
                    Else
                        dRow("GrandDiscount") = 0
                        dGrandDis = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("TradeDisAmt")) = False Then
                        dRow("GrandDiscountAmt") = dt1.Rows(i)("TradeDisAmt")
                        dGrandDiscountAmt = dt1.Rows(i)("TradeDisAmt")
                    Else
                        dRow("GrandDiscountAmt") = 0
                        dGrandDiscountAmt = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("SDM_ShippingRate")) = False Then
                        dRow("Shipping") = dt1.Rows(i)("SDM_ShippingRate")
                        dShipping = dt1.Rows(i)("SDM_ShippingRate")
                    Else
                        dRow("Shipping") = 0
                        dShipping = 0
                    End If

                    If IsDBNull(dt1.Rows(i)("PGD_ManufactureDate")) = False Then
                        dRow("ManufactureDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("PGD_ManufactureDate"), "D")
                    Else
                        dRow("ManufactureDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("PGD_ExpireDate")) = False Then
                        dRow("ExpireDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("PGD_ExpireDate"), "D")
                    Else
                        dRow("ExpireDate") = ""
                    End If

                    dRow("ReturnCode") = dt1.Rows(i)("SRM_ReturnOrderCode")

                    If objGen.FormatDtForRDBMS(dt1.Rows(i)("SRM_ReturnDate"), "D") <> "30/12/1899" Then
                        dRow("ReturnDate") = objGen.FormatDtForRDBMS(dt1.Rows(i)("SRM_ReturnDate"), "D")
                    Else
                        dRow("ReturnDate") = ""
                    End If

                    dt.Rows.Add(dRow)
                Next
            End If

            Dim dRoundTotal As Double
            Dim sSFinalTotal As String = ""

            If dCSTTot > 0 Then
                dRoundTotal = (((dAmountTot - dGrandDiscountAmt) + dExciseTot + dCSTTot) + dShipping)
            Else
                dRoundTotal = (((dAmountTot - dGrandDiscountAmt) + dExciseTot + dVATTot) + dShipping)
            End If

            dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dRoundTotal)))

            'Round Off'
            intpart = CType(dRoundTotal, Integer)
            decpart = String.Format("{0:0.00}", Convert.ToDecimal(dRoundTotal - intpart))
            'Round Off'

            sSFinalTotal = Math.Round(dRoundTotal)
            TotalinWord = GetInWords(sSFinalTotal)
            'TotalinWord = NumberToWord(String.Format("{0:0.00}", dRow("GrandTotal"))) & " Only"
            dRow("TotalinWord") = TotalinWord

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RPTSalesReturn.rdlc")

            Dim NetAmount As ReportParameter() = New ReportParameter() {New ReportParameter("NetAmount", dAmountTot)}
            ReportViewer1.LocalReport.SetParameters(NetAmount)

            Dim TradeDiscount As ReportParameter() = New ReportParameter() {New ReportParameter("TradeDiscount", dGrandDis)}
            ReportViewer1.LocalReport.SetParameters(TradeDiscount)

            Dim TradeDisAmt As ReportParameter() = New ReportParameter() {New ReportParameter("TradeDisAmt", dGrandDiscountAmt)}
            ReportViewer1.LocalReport.SetParameters(TradeDisAmt)

            Dim VATAmount As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmount", dVATTot)}
            ReportViewer1.LocalReport.SetParameters(VATAmount)

            Dim CSTAmount As ReportParameter() = New ReportParameter() {New ReportParameter("CSTAmount", dCSTTot)}
            ReportViewer1.LocalReport.SetParameters(CSTAmount)

            Dim ExciseAmount As ReportParameter() = New ReportParameter() {New ReportParameter("ExciseAmount", dExciseTot)}
            ReportViewer1.LocalReport.SetParameters(ExciseAmount)

            Dim Shipping As ReportParameter() = New ReportParameter() {New ReportParameter("Shipping", dShipping)}
            ReportViewer1.LocalReport.SetParameters(Shipping)

            Dim GrandTotal As ReportParameter() = New ReportParameter() {New ReportParameter("GrandTotal", String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dRoundTotal))))}
            ReportViewer1.LocalReport.SetParameters(GrandTotal)

            Dim TotalInWords As ReportParameter() = New ReportParameter() {New ReportParameter("TotalInWords", TotalinWord)}
            ReportViewer1.LocalReport.SetParameters(TotalInWords)

            Dim TermsConditions As ReportParameter() = New ReportParameter() {New ReportParameter("TermsConditions", UCase(objSRR.GetTermsConditions(sSession.AccessCode, sSession.AccessCodeID)))}
            ReportViewer1.LocalReport.SetParameters(TermsConditions)

            Dim TotalQty As ReportParameter() = New ReportParameter() {New ReportParameter("TotalQty", iTotalQty)}
            ReportViewer1.LocalReport.SetParameters(TotalQty)

            Dim SalesPersonName As ReportParameter() = New ReportParameter() {New ReportParameter("SalesPersonName", sSession.UserFullName)}
            ReportViewer1.LocalReport.SetParameters(SalesPersonName)

            Dim RoundOffDecimal As ReportParameter() = New ReportParameter() {New ReportParameter("RoundOffDecimal", decpart)}
            ReportViewer1.LocalReport.SetParameters(RoundOffDecimal)

            Dim dt2 As New DataTable
            dt2 = objSRR.GetVATBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iSalesReturnID)

            Dim rds1 As New ReportDataSource("DataSet2", dt2)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RPTSalesReturn.rdlc")

            Dim dAmount, dVATAmount As Double
            If dt2.Rows.Count > 0 Then
                For j = 0 To dt2.Rows.Count - 1
                    dAmount = dAmount + dt2.Rows(j)("Amount")
                    dVATAmount = dVATAmount + dt2.Rows(j)("VATAmount")
                Next
            End If

            Dim AmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("AmtBifercation", dAmount)}
            ReportViewer1.LocalReport.SetParameters(AmtBifercation)

            Dim VATAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmtBifercation", dVATAmount)}
            ReportViewer1.LocalReport.SetParameters(VATAmtBifercation)


            Dim dt3 As New DataTable
            dt3 = objSRR.GetCSTBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iSalesReturnID)

            Dim rds2 As New ReportDataSource("DataSet3", dt3)
            ReportViewer1.LocalReport.DataSources.Add(rds2)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/RPTSalesReturn.rdlc")

            Dim dCAmount, dCSTAmount As Double
            If dt3.Rows.Count > 0 Then
                For j = 0 To dt3.Rows.Count - 1
                    dCAmount = dCAmount + dt3.Rows(j)("CAmount")
                    dCSTAmount = dCSTAmount + dt3.Rows(j)("CSTAmount")
                Next
            End If

            Dim CAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("CAmtBifercation", dCAmount)}
            ReportViewer1.LocalReport.SetParameters(CAmtBifercation)

            Dim CSTAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("CSTAmtBifercation", dCSTAmount)}
            ReportViewer1.LocalReport.SetParameters(CSTAmtBifercation)

            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetInWords(ByVal num As Long) As String
        On Error Resume Next
        Dim str As String
        Dim subnum As Long
        Dim Digits As New TextBox
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
            str = str + " Rupees only "
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
            str = str + " Rupees only "
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
            str = str + " Rupees only "
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
            str = str + " Rupees only "
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
            str = str + " Rupees only "
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
            str = str + " Rupees only "
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
            str = str + " Rupees only "
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
            str = str + " Rupees only "

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
            str = str + " Rupees only "

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
            str = str + " Rupees only "
        End If
        If Len(Digits.Text) = 0 Then
            str = ""
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

End Class
