Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Partial Class Reports_Viewer_OralSalesHR
    Inherits System.Web.UI.Page
    Private sFormName As String = "Reports_Viewer_OralSalesHR"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralMaster
    Dim objGen As New clsFASGeneral
    Dim objGenFun As New clsGeneralFunctions
    Dim objOralHR As New clsOralSalesHR
    Private Shared sSession As AllSession
    Private objDBL As New DatabaseLayer.DBHelper
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iOrderID As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                iOrderID = Request.QueryString("ExistingOrder")
                If iOrderID > 0 Then
                    loadgrid(iOrderID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Protected Sub loadgrid(ByVal iOrderID As Integer)
        Dim dt As New DataTable
        Dim dt0 As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim dt4 As New DataTable
        Dim dRow As DataRow
        Dim partyid As Integer = 0
        'Dim Ctin As String = ""
        'Dim CPan As String = ""
        'Dim Ptin As String = ""
        'Dim PPan As String = ""
        'Dim tin As String = ""
        'Dim Pan As String = ""
        Dim Totalamt As Decimal
        Dim TotalinWord As String = ""
        Dim company As String = ""

        Dim VatH As String = "" : Dim CVatH As String = "" : Dim PVatH As String = ""
        Dim TaxH As String = "" : Dim CTaxH As String = "" : Dim PTaxH As String = ""
        Dim PanH As String = "" : Dim CPanH As String = "" : Dim PPanH As String = ""
        Dim TanH As String = "" : Dim CTanH As String = "" : Dim PTanH As String = ""
        Dim tinH As String = "" : Dim CtinH As String = "" : Dim PtinH As String = ""
        Dim CinH As String = "" : Dim CCinH As String = "" : Dim PCinH As String = ""

        Dim dtPrint As New DataTable

        Dim Totaltax, DiscountAmt, GrandTotal, dimGtotal, CstAmnt, vatAmnt, ExceAmnt, GrdTotal, dMRPTotal As Decimal

        Dim Tot As Decimal
        Dim iTQTY As Double

        Dim dMRPRate As Double

        Dim i0Qty As Double
        Dim i3Qty As Double
        Dim i4Qty As Double
        Dim i5Qty As Double
        Dim i6Qty As Double

        Dim i7Qty As Double
        Dim i8Qty As Double

        Dim i9Qty As Double
        Dim i10Qty As Double
        Dim i11Qty As Double
        Dim i12Qty As Double

        Dim sPartyCode As String = ""

        Dim sItemID As String = ""
        Dim intpart As Integer
        Dim decpart As Double
        Try
            company = objOralHR.GetAccessCode(sSession.AccessCode, sSession.AccessCodeID)
            dt0 = objOralHR.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID, company)
            dt1 = objOralHR.LoadGridOtherDetails(sSession.AccessCode, sSession.AccessCodeID)
            For i = 0 To dt1.Rows.Count - 1
                VatH = dt1.Rows(i).Item("Statutory Name")
                If VatH.Contains("VAT") Then
                    CVatH = dt1.Rows(i).Item("Statutory Value")
                End If
                TaxH = dt1.Rows(i).Item("Statutory Name")
                If TaxH.Contains("TAX") Then
                    CTaxH = dt1.Rows(i).Item("Statutory Value")
                End If
                PanH = dt1.Rows(i).Item("Statutory Name")
                If PanH.Contains("PAN") Then
                    CPanH = dt1.Rows(i).Item("Statutory Value")
                End If
                TanH = dt1.Rows(i).Item("Statutory Name")
                If TanH.Contains("PAN") Then
                    CTanH = dt1.Rows(i).Item("Statutory Value")
                End If
                tinH = dt1.Rows(i).Item("Statutory Name")
                If tinH.Contains("TIN") Then
                    CtinH = dt1.Rows(i).Item("Statutory Value")
                End If
                CinH = dt1.Rows(i).Item("Statutory Name")
                If CinH.Contains("CIN") Then
                    CCinH = dt1.Rows(i).Item("Statutory Value")
                End If

            Next
            dt2 = objOralHR.Party(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)
            If (dt2.Rows.Count > 0) Then
                partyid = dt2.Rows(0).Item("SPO_PartyName")
                dt3 = objOralHR.LoadGridStatutoryReferencesDetails(sSession.AccessCode, sSession.AccessCodeID, partyid)
                For i = 0 To dt3.Rows.Count - 1
                    VatH = dt3.Rows(i).Item("Statutory Name")
                    If VatH.Contains("VAT") Then
                        PVatH = dt3.Rows(i).Item("Statutory Value")
                    End If
                    TaxH = dt3.Rows(i).Item("Statutory Name")
                    If TaxH.Contains("TAX") Then
                        PTaxH = dt3.Rows(i).Item("Statutory Value")
                    End If
                    PanH = dt3.Rows(i).Item("Statutory Name")
                    If PanH.Contains("PAN") Then
                        PPanH = dt3.Rows(i).Item("Statutory Value")
                    End If
                    TanH = dt3.Rows(i).Item("Statutory Name")
                    If TanH.Contains("TAN") Then
                        PTanH = dt3.Rows(i).Item("Statutory Value")
                    End If
                    tinH = dt3.Rows(i).Item("Statutory Name")
                    If tinH.Contains("TIN") Then
                        PtinH = dt3.Rows(i).Item("Statutory Value")
                    End If
                    CinH = dt3.Rows(i).Item("Statutory Name")
                    If CinH.Contains("CIN") Then
                        PCinH = dt3.Rows(i).Item("Statutory Value")
                    End If
                Next
            End If


            dt4 = objOralHR.loadOralFormaDetails(sSession.AccessCode, sSession.AccessCodeID, iOrderID)

            'dt4 = clsSalesInvoice.loadOralDetails(sSession.AccessCode, sSession.AccessCodeID, ddlInvoice.SelectedValue)

            'For i = 0 To dt4.Rows.Count - 1
            '    If i = dt4.Rows.Count - 1 Then
            '        Totalamt = dt4.Rows(i)("Total Amount")
            '        TotalinWord = NumberToWord(String.Format("{0:0.00}", dt4.Rows(i)("Total Amount"))) & " Only"
            '    End If
            'Next
            'Dim dimGtotal, CstAmnt, vatAmnt, ExceAmnt As Decimal
            For i = 0 To dt4.Rows.Count - 1
                If i = dt4.Rows.Count - 1 Then
                    Totalamt = dt4.Rows(i)("Total Amount")
                    'GrdTotal = GrdTotal + dt4.Rows(i)("Total Amount")
                    'Tot = Tot + dt4.Rows(i)("Rate")'
                    Tot = Tot + dt4.Rows(i)("Total Amount")
                    'dimGtotal = dt4.Rows(i)("Total Amount")
                    dimGtotal = dt4.Rows(i)("Total Amount")

                    CstAmnt = CstAmnt + dt4.Rows(i)("CSTAmt")
                    vatAmnt = vatAmnt + dt4.Rows(i)("VATAmt")
                    ExceAmnt = ExceAmnt + dt4.Rows(i)("ExiseAmt")
                    DiscountAmt = DiscountAmt + dt4.Rows(i)("Discount")

                    i0Qty = i0Qty + dt4.Rows(i)("Total0")
                    i3Qty = i3Qty + dt4.Rows(i)("Total3")
                    i4Qty = i4Qty + dt4.Rows(i)("Total4")
                    i5Qty = i5Qty + dt4.Rows(i)("Total5")
                    i6Qty = i6Qty + dt4.Rows(i)("Total6")

                    i7Qty = i7Qty + dt4.Rows(i)("Total7")
                    i8Qty = i8Qty + dt4.Rows(i)("Total8")

                    i9Qty = i9Qty + dt4.Rows(i)("Total9")
                    i10Qty = i10Qty + dt4.Rows(i)("Total10")
                    i11Qty = i11Qty + dt4.Rows(i)("Total11")
                    i12Qty = i12Qty + dt4.Rows(i)("Total12")
                    dMRPTotal = dMRPTotal + dt4.Rows(i)("Rate")

                    iTQTY = iTQTY + dt4.Rows(i)("TotalQty")
                    'TotalinWord = NumberToWord(String.Format("{0:0.00}", dimGtotal)) & " Only"
                End If
            Next

            'Dim Totaltax, DiscountAmt, GrandTotal, dimGtotal, CstAmnt, vatAmnt, ExceAmnt As Decimal
            Dim dt5 As New DataTable
            Dim Duniqe As New DataTable
            Dim qty As Double

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
            dt.Columns.Add("t12")
            dt.Columns.Add("Total")
            dt.Columns.Add("MRP")
            dt.Columns.Add("Rate")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("DisAmt")
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
            dt.Columns.Add("CPIN")
            dt.Columns.Add("CPh")
            dt.Columns.Add("CEmail")
            dt.Columns.Add("CVat")
            dt.Columns.Add("CTax")
            dt.Columns.Add("CPan")
            dt.Columns.Add("CTan")
            dt.Columns.Add("CTin")
            dt.Columns.Add("CCin")
            dt.Columns.Add("InvoiceNO")
            dt.Columns.Add("OrderNo")
            dt.Columns.Add("OrderDate")
            dt.Columns.Add("Paname")
            dt.Columns.Add("Padd")
            dt.Columns.Add("PPIN")
            dt.Columns.Add("Pph")
            dt.Columns.Add("PEmail")
            dt.Columns.Add("PVat")
            dt.Columns.Add("PTax")
            dt.Columns.Add("PPan")
            dt.Columns.Add("PTan")
            dt.Columns.Add("PTin")
            dt.Columns.Add("PCin")

            dt.Columns.Add("Totalamt")
            dt.Columns.Add("TotalinWord")
            dt.Columns.Add("NetAmnt")
            dt.Columns.Add("GrandTotal")
            dt.Columns.Add("Shipping")
            dt.Columns.Add("TradeDiscount")
            dt.Columns.Add("TotalQty")
            dt.Columns.Add("Unit")
            dt.Columns.Add("t0")
            dt.Columns.Add("TradeDis")

            dt.Columns.Add("LastColAmount")

            dt.Columns.Add("Total0")
            dt.Columns.Add("Total3")
            dt.Columns.Add("Total4")
            dt.Columns.Add("Total5")
            dt.Columns.Add("Total6")
            dt.Columns.Add("Total7")
            dt.Columns.Add("Total8")
            dt.Columns.Add("Total9")
            dt.Columns.Add("Total10")
            dt.Columns.Add("Total11")
            dt.Columns.Add("Total12")
            dt.Columns.Add("RateTotal")
            dt.Columns.Add("TermsConditions")
            dt.Columns.Add("BuyerOrderNo")
            dt.Columns.Add("BuyerOrderDate")
            dt.Columns.Add("MRPPrice")

            dt5 = dt4.Copy

            Duniqe = objOralHR.RemoveDublicate(dt5)
            For j = 0 To Duniqe.Rows.Count - 2
                dRow = dt.NewRow()
                dRow("SlNo") = j + 1
                qty = 0
                Totaltax = 0
                Totalamt = 0
                For i = 0 To dt4.Rows.Count - 2
                    If dt4.Rows(i)("Commodity") = "<b>Total</b>" Then
                        dRow("SlNo") = ""
                    End If

                    dRow("Colour") = Duniqe.Rows(j)("Colour")
                    If (Duniqe.Rows(j)("Description") = dt4.Rows(i)("Description")) Then
                        If (dt4.Rows(i)("Description") <> "<b>Total</b>") Then
                            dRow("Description") = dt4.Rows(i)("Description")
                            dRow("Commodity") = dt4.Rows(i)("Commodity")

                            If (dt4.Rows(i)("0") <> 0) Then
                                dRow("t0") = dt4.Rows(i)("0")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("0"))
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("3") <> 0) Then
                                dRow("t3") = dt4.Rows(i)("3")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("3"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("4") <> 0) Then
                                dRow("t4") = dt4.Rows(i)("4")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("4"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("5") <> 0) Then
                                dRow("t5") = dt4.Rows(i)("5")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("5"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("6") <> 0) Then
                                dRow("t6") = dt4.Rows(i)("6")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("6"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("7") <> 0) Then
                                dRow("t7") = dt4.Rows(i)("7")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("7"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("8") <> 0) Then
                                dRow("t8") = dt4.Rows(i)("8")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("8"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("9") <> 0) Then
                                dRow("t9") = dt4.Rows(i)("9")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("9"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If
                            If (dt4.Rows(i)("10") <> 0) Then
                                dRow("t10") = dt4.Rows(i)("10")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("10"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("11") <> 0) Then
                                dRow("t11") = dt4.Rows(i)("11")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("11"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            If (dt4.Rows(i)("12") <> 0) Then
                                dRow("t12") = dt4.Rows(i)("12")
                                qty = qty + Convert.ToDouble(dt4.Rows(i)("12"))
                                'sItemID = sItemID & "," & dt4.Rows(i)("ItemID")
                                sItemID = dt4.Rows(i)("ItemID")
                            End If

                            Totaltax = Totaltax + Convert.ToDecimal(dt4.Rows(i)("VATAmt")) + Convert.ToDecimal(dt4.Rows(i)("CSTAmt")) + Convert.ToDecimal(dt4.Rows(i)("ExiseAmt"))
                            dRow("MRP") = dt4.Rows(i)("Rate")
                            dMRPRate = dt4.Rows(i)("Rate")
                            dRow("Rate") = dt4.Rows(i)("Rate")
                            dRow("DiscountAmt") = DiscountAmt
                            dRow("DisAmt") = dt4.Rows(i)("DiscountAmt")

                            Totalamt = dt4.Rows(i)("Total Amount")
                            'Totalamt = Convert.ToDecimal(dRow("Rate")) * qty

                            dRow("CSTAmt") = CstAmnt
                            dRow("ExiseAmt") = ExceAmnt
                            dRow("VATAmt") = vatAmnt
                            'dRow("NetAmnt") = dimGtotal - (vatAmnt + ExceAmnt + CstAmnt)

                            dRow("NetAmnt") = Tot

                        End If
                    End If

                    'dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(Totalamt))
                    'dRow("Total") = qty
                    'dRow("DiscountAmt") = DiscountAmt
                    'dRow("Discount") = dt4.Rows(i)("Discount")
                    'dRow("DisAmt") = dt4.Rows(0)("DiscountAmt")
                    'dRow("VAT") = dt4.Rows(i)("VAT")
                    'dRow("Exise") = dt4.Rows(i)("Exise")
                    'dRow("CName") = clsCustomerMaster.GetAccessCode(sSession.AccessCode)
                    'dRow("CAdd") = dt0.Rows(0).Item("CUST_COMM_ADDRESS")
                    'dRow("CPh") = "Ph  " & dt0.Rows(0).Item("CUST_COMM_TEL")
                    'dRow("CEmail") = "E-mail  " & dt0.Rows(0).Item("CUST_EMAIL")
                    'dRow("Ctin") = Ctin
                    'dRow("CPan") = CPan
                    'dRow("InvoiceNO") = ddlInvoice.SelectedItem.Text
                    'dRow("OrderNo") = ddlInvoice.SelectedItem.Text
                    'If (dt2.Rows.Count > 0) Then
                    '    dRow("Paname") = dt2.Rows(0).Item("BM_Name")
                    '    dRow("Padd") = dt2.Rows(0).Item("BM_Address")
                    '    dRow("Pph") = "Ph  " & dt2.Rows(0).Item("BM_LandLineNo")
                    '    dRow("PEmail") = "E-mail  " & dt2.Rows(0).Item("BM_EmailID")
                    '    dRow("Ptin") = Ptin
                    '    dRow("PPan") = PPan
                    'End If
                    'dRow("TotalAmt") = "Total Net Amount  Rs  " & Totalamt
                    'dRow("TotalinWord") = TotalinWord
                    ''dRow("NetAmnt") = dimGtotal - (vatAmnt + ExceAmnt + CstAmnt)

                    'dRow("NetAmnt") = Tot
                    'dRow("GrandTotal") = ((dimGtotal - dt4.Rows(i)("TradeDiscount")) + vatAmnt)
                    'dRow("Shipping") = dt4.Rows(i)("Shipping")
                    'dRow("TradeDiscount") = dt4.Rows(i)("TradeDiscount")

                Next

                'dRow("MRP") = String.Format("{0:0.00}", Convert.ToDecimal(qty * dMRPRate))
                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(Totalamt))
                dRow("Total") = qty
                dRow("DiscountAmt") = DiscountAmt
                dRow("Discount") = Duniqe.Rows(j)("Discount")

                sPartyCode = dt2.Rows(0).Item("BM_Code")

                If sItemID.StartsWith(",") Then
                    sItemID = sItemID.Remove(0, 1)
                End If
                If sItemID.EndsWith(",") Then
                    sItemID = sItemID.Remove(Len(sItemID) - 1, 1)
                End If

                If sPartyCode.StartsWith("C") Then
                    If CstAmnt > 0 Then
                        Dim sDisAmt As String = ""
                        sDisAmt = objOralHR.CalculateDiscountAmtMRPCST(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dMRPRate, sPartyCode, sItemID, Duniqe.Rows(j)("Discount"))
                        dRow("DisAmt") = String.Format("{0:0.00}", Convert.ToDecimal(qty * sDisAmt))
                    Else
                        Dim sDisAmt As String = ""
                        sDisAmt = objOralHR.CalculateDiscountAmtMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dMRPRate, sPartyCode, sItemID, Duniqe.Rows(j)("Discount"))
                        dRow("DisAmt") = String.Format("{0:0.00}", Convert.ToDecimal(qty * sDisAmt))
                    End If

                End If
                If sPartyCode.StartsWith("P") Then
                    Dim sDisAmt As String = ""
                    sDisAmt = objOralHR.CalculateDiscountAmt(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dMRPRate, sPartyCode, Duniqe.Rows(j)("Discount"))
                    dRow("DisAmt") = String.Format("{0:0.00}", Convert.ToDecimal(qty * sDisAmt))
                End If

                'dRow("DisAmt") = Duniqe.Rows(j)("DiscountAmt")
                dRow("VAT") = Duniqe.Rows(j)("VAT")
                dRow("CST") = Duniqe.Rows(j)("CST")
                dRow("Exise") = Duniqe.Rows(j)("Exise")
                dRow("CName") = dt0.Rows(0).Item("CUST_NAME")
                dRow("CAdd") = dt0.Rows(0).Item("CUST_COMM_ADDRESS")
                dRow("CPIN") = dt0.Rows(0).Item("CUST_PIN")
                dRow("CPh") = dt0.Rows(0).Item("CUST_COMM_TEL")
                dRow("CEmail") = dt0.Rows(0).Item("CUST_EMAIL")
                dRow("CVat") = CVatH
                dRow("CTax") = CTaxH
                dRow("CPan") = CPanH
                dRow("CTan") = CTanH
                dRow("CTin") = CtinH
                dRow("CCin") = CCinH
                dRow("InvoiceNO") = dt2.Rows(0).Item("SPO_OrderCode")
                dRow("OrderNo") = dt2.Rows(0).Item("SPO_OrderCode")
                dRow("OrderDate") = objGen.FormatDtForRDBMS(dt2.Rows(0).Item("SPO_OrderDate"), "D")
                If (dt2.Rows.Count > 0) Then
                    dRow("Paname") = dt2.Rows(0).Item("BM_Name")
                    dRow("Padd") = dt2.Rows(0).Item("BM_Address") & "," & dt2.Rows(0).Item("BM_Address1") & "," & dt2.Rows(0).Item("BM_Address2") & "," & dt2.Rows(0).Item("BM_Address3")
                    dRow("PPIN") = dt2.Rows(0).Item("BM_PinCode")
                    dRow("Pph") = dt2.Rows(0).Item("BM_MobileNo")
                    dRow("PEmail") = dt2.Rows(0).Item("BM_EmailID")
                    dRow("PVat") = PVatH
                    dRow("PTax") = PTaxH
                    dRow("PPan") = PPanH
                    dRow("PTan") = PTanH
                    dRow("PTin") = PtinH
                    dRow("PCin") = PCinH
                End If
                dRow("TotalAmt") = "Total Net Amount  Rs  " & Totalamt

                dRow("NetAmnt") = Tot

                Dim dRoundTotal As Double
                Dim sSFinalTotal As String = ""
                Dim dShipping As Double

                dShipping = objOralHR.GetChargeAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)

                'dRoundTotal = (((dimGtotal - Duniqe.Rows(j)("TradeDiscount")) + vatAmnt) + Duniqe.Rows(j)("Shipping"))
                If CstAmnt > 0 Then
                    dRoundTotal = ((((dimGtotal - Duniqe.Rows(j)("TradeDiscount")) + ExceAmnt) + CstAmnt) + dShipping)
                Else
                    dRoundTotal = ((((dimGtotal - Duniqe.Rows(j)("TradeDiscount")) + ExceAmnt) + vatAmnt) + dShipping)
                End If

                'dRow("GrandTotal") = (((dimGtotal - Duniqe.Rows(j)("TradeDiscount")) + vatAmnt) + Duniqe.Rows(j)("Shipping"))
                dRow("GrandTotal") = String.Format("{0:0.00}", Convert.ToDecimal(Math.Round(dRoundTotal)))

                'Round Off'
                intpart = CType(dRoundTotal, Integer)
                decpart = String.Format("{0:0.00}", Convert.ToDecimal(dRoundTotal - intpart))
                'Round Off'

                'dRoundTotal = dRow("GrandTotal")
                sSFinalTotal = Math.Round(dRoundTotal)
                'TotalinWord = NumberToWord(sSFinalTotal) & " Only"
                TotalinWord = GetInWords(sSFinalTotal)
                'TotalinWord = NumberToWord(String.Format("{0:0.00}", dRow("GrandTotal"))) & " Only"
                dRow("TotalinWord") = TotalinWord

                'dRow("Shipping") = Duniqe.Rows(j)("Shipping")
                dRow("Shipping") = dShipping

                dRow("TradeDiscount") = Duniqe.Rows(j)("TradeDiscount")
                dRow("TotalQty") = iTQTY
                dRow("Unit") = Duniqe.Rows(j)("Unit")
                dRow("TradeDis") = Duniqe.Rows(j)("TradeDis")

                If sPartyCode.StartsWith("C") Then
                    If CstAmnt > 0 Then
                        Dim sNetAmt As String = ""
                        sNetAmt = objOralHR.CalculateNetAmountMRPCST(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dMRPRate, sPartyCode, sItemID, Duniqe.Rows(j)("Discount"))
                        dRow("LastColAmount") = String.Format("{0:0.00}", Convert.ToDecimal(qty * sNetAmt))
                    Else
                        Dim sNetAmt As String = ""
                        sNetAmt = objOralHR.CalculateNetAmountMRP(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dMRPRate, sPartyCode, sItemID, Duniqe.Rows(j)("Discount"))
                        dRow("LastColAmount") = String.Format("{0:0.00}", Convert.ToDecimal(qty * sNetAmt))
                    End If

                End If
                If sPartyCode.StartsWith("P") Then
                    Dim sNetAmt As String = ""
                    sNetAmt = objOralHR.CalculateNetAmount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID, dMRPRate, sPartyCode, Duniqe.Rows(j)("Discount"))
                    dRow("LastColAmount") = String.Format("{0:0.00}", Convert.ToDecimal(qty * sNetAmt))
                End If

                dRow("Total0") = i0Qty
                dRow("Total3") = i3Qty
                dRow("Total4") = i4Qty
                dRow("Total5") = i5Qty
                dRow("Total6") = i6Qty
                dRow("Total7") = i7Qty
                dRow("Total8") = i8Qty
                dRow("Total9") = i9Qty
                dRow("Total10") = i10Qty
                dRow("Total11") = i11Qty
                dRow("Total12") = i12Qty
                dRow("RateTotal") = dMRPTotal

                dRow("TermsConditions") = objOralHR.GetTermsConditions(sSession.AccessCode, sSession.AccessCodeID)

                dRow("BuyerOrderNo") = dt2.Rows(0).Item("SPO_BuyerOrderNo")
                'dRow("BuyerOrderDate") = objGen.FormatDtForRDBMS(dt2.Rows(0).Item("SPO_BuyerOrderDate"), "D")
                If (objGen.FormatDtForRDBMS(dt2.Rows(0).Item("SPO_BuyerOrderDate"), "D") <> "30/12/1899") And (objGen.FormatDtForRDBMS(dt2.Rows(0).Item("SPO_BuyerOrderDate"), "D") <> "30-12-1899") Then
                    dRow("BuyerOrderDate") = objGen.FormatDtForRDBMS(dt2.Rows(0).Item("SPO_BuyerOrderDate"), "D")
                Else
                    dRow("BuyerOrderDate") = ""
                End If

                GrandTotal = GrandTotal + Totalamt

                If IsDBNull(Duniqe.Rows(j)("MRPPrice")) = False Then
                    dRow("MRPPrice") = Duniqe.Rows(j)("MRPPrice")
                Else
                    dRow("MRPPrice") = ""
                End If

                dt.Rows.Add(dRow)
                dMRPRate = 0
            Next

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/OralSalesHR.rdlc")

            Dim VAT As ReportParameter() = New ReportParameter() {New ReportParameter("VAT", UCase(objOralHR.GetALLVAT(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)))}
            ReportViewer1.LocalReport.SetParameters(VAT)

            Dim CST As ReportParameter() = New ReportParameter() {New ReportParameter("CST", UCase(objOralHR.GetALLCST(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)))}
            ReportViewer1.LocalReport.SetParameters(CST)

            Dim RoundOffDecimal As ReportParameter() = New ReportParameter() {New ReportParameter("RoundOffDecimal", decpart)}
            ReportViewer1.LocalReport.SetParameters(RoundOffDecimal)

            dtPrint = objOralHR.GetPrintData(sSession.AccessCode, sSession.AccessCodeID, "S")

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


            '---VAT & CST Bifercation---'
            Dim dtV As New DataTable
            dtV = objOralHR.GetVATBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)

            Dim rds3 As New ReportDataSource("DataSet2", dtV)
            ReportViewer1.LocalReport.DataSources.Add(rds3)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/OralSalesHR.rdlc")

            Dim dAmount, dVAAmount As Double
            If dtV.Rows.Count > 0 Then
                For j = 0 To dtV.Rows.Count - 1
                    dAmount = dAmount + dtV.Rows(j)("Amount")
                    dVAAmount = dVAAmount + dtV.Rows(j)("VATAmount")
                Next
            End If

            Dim AmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("AmtBifercation", dAmount)}
            ReportViewer1.LocalReport.SetParameters(AmtBifercation)

            Dim VATAmtBifercation As ReportParameter() = New ReportParameter() {New ReportParameter("VATAmtBifercation", dVAAmount)}
            ReportViewer1.LocalReport.SetParameters(VATAmtBifercation)


            'Dim dtC As New DataTable
            'dtC = objOralHR.GetCSTBifercation(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)

            'Dim rds4 As New ReportDataSource("DataSet3", dtC)
            'ReportViewer1.LocalReport.DataSources.Add(rds4)
            'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/OralSalesHR.rdlc")

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

            '---VAT & CST Bifercation---'

            Dim dtCharge As New DataTable
            dtCharge = objOralHR.GetCharges(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, iOrderID)

            Dim rds5 As New ReportDataSource("DataSet4", dtCharge)
            ReportViewer1.LocalReport.DataSources.Add(rds5)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/Sales/OralSalesHR.rdlc")

            ReportViewer1.LocalReport.Refresh()

        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadgrid")
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
