
Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsPurchaseReport

    Dim objDb As New DBHelper
    Dim objFasGnrl As New clsFASGeneral

    Dim objGnrl As New clsGeneralFunctions
    Public Function loadCompanyVATORCST(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from Company_Accounting_Template where CMP_ID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCustDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select j.CUST_NAME,j.CUST_Comm_Address,j.CUST_Comm_Tel,j.CUST_Email,k.Cmp_Value As CVAT,l.Cmp_Value As CTAX,m.Cmp_Value As CPAN,n.Cmp_Value As CTAN,o.Cmp_Value As CTIN,p.Cmp_Value As CCIN "
            sSql = sSql & "From MST_Customer_master j "
            sSql = sSql & " Left Join Company_Accounting_Template k On j.CUST_ID=k.Cmp_ID And k.Cmp_Desc='VAT'"
            sSql = sSql & " Left Join Company_Accounting_Template l On j.CUST_ID=l.Cmp_ID And l.Cmp_Desc='TAX'"
            sSql = sSql & " Left Join Company_Accounting_Template m On j.CUST_ID=m.Cmp_ID And m.Cmp_Desc='PAN'"
            sSql = sSql & " Left Join Company_Accounting_Template n On j.CUST_ID=n.Cmp_ID And n.Cmp_Desc='TAN'"
            sSql = sSql & " Left Join Company_Accounting_Template o On j.CUST_ID=o.Cmp_ID And o.Cmp_Desc='TIN'"
            sSql = sSql & " Left Join Company_Accounting_Template p On j.CUST_ID=p.Cmp_ID And p.Cmp_Desc='CIN' Where j.Cust_ID=" & iCompID & " "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadCompanyDetails(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "" : sSql = "Select * from MST_CUSTOMER_MASTER where CUST_ID =" & iCompID & ""
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function loadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal icat As Integer, ByVal iorder As Integer, ByVal InvoiceID As Integer, ByVal isuuplier As Integer, ByVal iCommodity As Integer, ByVal iItem As Integer, ByVal ivat As String, ByVal iExcise As String, ByVal iCst As String, ByVal iDiscount As String, ByVal iFromDt As Date, ByVal iToDt As Date, ByVal iZone As Integer, ByVal iRegion As Integer, ByVal iArea As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dr As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim VAT As String = "", CST As String = "", Exise As String = ""
        Dim Cstval As String = ""
        Dim TotalAmt, Totaltax, BasicAmount As Double
        Dim gtQty, gtMRP, gtVAT, gtVATAmt, gtBasicAmount, gtCST, gtCSTAmt, gtExise, gtExiseAmt, gtdiscount, gtdiscountAmt, GrandTotal, pending, gtOrderqty, gtRecvdQty, gtExcessQty, gtRjctdQty, gtPendingQty As Double
        gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0 : pending = 0 : gtOrderqty = 0 : gtRecvdQty = 0 : gtExcessQty = 0 : gtRjctdQty = 0 : gtPendingQty = 0
        Dim flag2 As String = "" : Dim flag3 As String = "" : Dim OrderNo As String = 0 : Dim InvoiceDate As String = "" : Dim Orderdate As String = "" : Dim GinNo As String = 0 : Dim Supplier As String = 0 : Dim Description As String = 0 : Dim Commodity As String = 0
        Dim InvoiceNo As String = ""
        Dim gtAmount, gtGSTRate, gtGSTAmount As Double
        Try
            dt = LoadDetailsAll(sNameSpace, iCompID, icat, iorder, isuuplier, InvoiceID, iCommodity, iItem, ivat, iExcise, iCst, iDiscount, iFromDt, iToDt, iZone, iRegion, iArea, iBranch)
            If icat = 1 Then
                dt = LoadDetailsAll(sNameSpace, iCompID, icat, iorder, isuuplier, InvoiceID, iCommodity, iItem, ivat, iExcise, iCst, iDiscount, iFromDt, iToDt, iZone, iRegion, iArea, iBranch)
            End If
            Dim dview As New DataView(dt)
            If icat = 2 Then
                dview = dt.DefaultView
                dview.RowFilter = "RejectedQty<>'0.000' and RejectedQty<>'0' "
                dt = dview.ToTable
            End If

            If icat = 3 Then
                dview = dt.DefaultView
                dview.RowFilter = "Excess<>'0.000' and Excess<>'0'"
                dt = dview.ToTable
            End If
            If (iDiscount <> "") Then
                dview = dt.DefaultView
                dview.RowFilter = "Discount='" & iDiscount & "'"
                dt = dview.ToTable
            End If

            If (ivat <> "") Then
                dview = dt.DefaultView
                dview.RowFilter = "VAT='" & ivat & "'"
                dt = dview.ToTable
            End If

            If (iExcise <> "") Then
                dview = dt.DefaultView
                dview.RowFilter = "Exise='" & iExcise & "'"
                dt = dview.ToTable
            End If

            If (iCst <> "") Then
                dview = dt.DefaultView
                dview.RowFilter = "CST='" & iCst & "'"
                dt = dview.ToTable
            End If

            If (icat = 4) Then
                dt2.Columns.Clear()
                dview.Sort = " OrderNo, GinNo"
                dt = dview.ToTable()
                Totaltax = 0
                Dim OrderNos, GinNos, Odate, Splr As String
                Dim Total, tQty, tMRP, tVAT, tVATAmt, tCST, tCSTAmt, tBasicAmount, tExise, tExiseAmt, tdiscount, tdiscountAmt, tOrderqty, tRecvdQty, tExcessQty, tRjctdQty, tPendingQty As Double
                Dim tAmount, tGSTRate, tGSTAmount As Double
                dt2.Columns.Add("SlNo")
                dt2.Columns.Add("OrderNo")
                dt2.Columns.Add("GinNo")
                dt2.Columns.Add("PV_DocRefNo")
                dt2.Columns.Add("Commodity")
                dt2.Columns.Add("Description")
                dt2.Columns.Add("Orderdate")
                dt2.Columns.Add("Supplier")
                dt2.Columns.Add("AcceptedQntity")
                dt2.Columns.Add("Orderedqty")
                dt2.Columns.Add("ReceivedQnt")
                dt2.Columns.Add("Excess")
                dt2.Columns.Add("RejectedQty")
                dt2.Columns.Add("PendingQty")
                dt2.Columns.Add("Rate")
                dt2.Columns.Add("VAT")
                dt2.Columns.Add("VATAmt")
                dt2.Columns.Add("CST")
                dt2.Columns.Add("CSTAmt")
                dt2.Columns.Add("Exise")
                dt2.Columns.Add("ExiseAmt")
                dt2.Columns.Add("Discount")
                dt2.Columns.Add("DiscountAmt")
                dt2.Columns.Add("Amount")
                dt2.Columns.Add("GSTRate")
                dt2.Columns.Add("GSTAmount")
                dt2.Columns.Add("TotalAmount")
                dt2.Columns.Add("InvoiceDate")
                dt2.Columns.Add("BasicAmount")
                flag = "" : flag1 = ""
                For i = 0 To dt.Rows.Count - 1
                    Totaltax = 0
                    dRow = dt2.NewRow()
                    dRow("SlNo") = i + 1
                    If flag <> dt.Rows(i)("OrderNo") Then
                        If flag <> "" Then
                            flag2 = "a"
                            dr = dt2.NewRow()
                            dr("OrderNo") = Convert.ToString(OrderNos)
                            dr("GinNo") = GinNos
                            dr("PV_DocRefNo") = Convert.ToString(InvoiceNo)
                            dr("Orderdate") = Convert.ToString(Odate)
                            dr("Supplier") = Convert.ToString(Splr)
                            dr("AcceptedQntity") = gtQty
                            dr("Orderedqty") = gtOrderqty
                            dr("ReceivedQnt") = gtRecvdQty
                            dr("Excess") = gtExcessQty
                            dr("RejectedQty") = gtRjctdQty
                            dr("PendingQty") = gtPendingQty
                            dr("Rate") = "None"
                            dr("VAT") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
                            dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
                            dr("CST") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
                            dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
                            dr("Exise") = gtExise
                            dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
                            dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscount))
                            dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscountAmt))

                            dr("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(gtAmount))
                            dr("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(gtGSTRate))
                            dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(gtGSTAmount))

                            dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
                            dr("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(gtBasicAmount))
                            dr("InvoiceDate") = Convert.ToString(InvoiceDate)
                            dt2.Rows.Add(dr)
                            gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtCST = 0 : gtBasicAmount = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0 : pending = 0 : gtOrderqty = 0 : gtRecvdQty = 0 : gtExcessQty = 0 : gtRjctdQty = 0 : gtPendingQty = 0
                            gtAmount = 0 : gtGSTRate = 0 : gtGSTAmount = 0
                        End If
                        dRow("OrderNo") = dt.Rows(i)("OrderNo")
                        flag = dt.Rows(i)("OrderNo")
                    End If
                    If flag1 <> dt.Rows(i)("GinNo") Then
                        If flag1 <> "" And flag2 <> "a" Then
                            dr = dt2.NewRow()
                            dr("OrderNo") = OrderNos
                            dr("GinNo") = GinNos
                            dr("PV_DocRefNo") = Convert.ToString(InvoiceNo)
                            dr("Orderdate") = Convert.ToString(Odate)
                            dr("Supplier") = (Splr)
                            dr("AcceptedQntity") = tQty
                            dr("Orderedqty") = tOrderqty
                            dr("ReceivedQnt") = tRecvdQty
                            dr("Excess") = tExcessQty
                            dr("RejectedQty") = tRjctdQty
                            dr("PendingQty") = tPendingQty
                            dr("Rate") = "None"
                            dr("VAT") = String.Format("{0:0.00}", Convert.ToDecimal(tVATAmt))
                            dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(tVATAmt))
                            dr("CST") = String.Format("{0:0.00}", Convert.ToDecimal(tCSTAmt))
                            dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(tCSTAmt))
                            dr("Exise") = tExise
                            dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(tExiseAmt))
                            dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(tdiscount))
                            dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(tdiscountAmt))

                            dr("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(tAmount))
                            dr("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(tGSTRate))
                            dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(tGSTAmount))

                            dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(Total))
                            dr("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(tBasicAmount))
                            dr("InvoiceDate") = Convert.ToString(InvoiceDate)
                            Total = 0 : tQty = 0 : tMRP = 0 : tVAT = 0 : tVATAmt = 0 : tCST = 0 : tCSTAmt = 0 : tExise = 0 : tExiseAmt = 0 : tdiscount = 0 : tdiscountAmt = 0 : GrandTotal = 0 : pending = 0 : tOrderqty = 0 : tRecvdQty = 0 : tExcessQty = 0 : tRjctdQty = 0 : tPendingQty = 0
                            tAmount = 0 : tGSTRate = 0 : tGSTAmount = 0 : tBasicAmount = 0
                            dt2.Rows.Add(dr)
                        End If
                        dRow("GinNo") = dt.Rows(i)("GinNo")
                        flag1 = dt.Rows(i)("GinNo")
                    End If

                    OrderNos = dt.Rows(i)("OrderNo")
                    GinNos = dt.Rows(i)("GinNo")
                    InvoiceNo = dt.Rows(i)("PV_DocRefNo")
                    InvoiceDate = dt.Rows(i)("InvoiceDate")

                    Odate = dt.Rows(i)("Orderdate")
                    Splr = dt.Rows(i)("Supplier")
                    gtQty = gtQty + dt.Rows(i)("AcceptedQntity")
                    tQty = tQty + dt.Rows(i)("AcceptedQntity")
                    dRow("AcceptedQntity") = dt.Rows(i)("AcceptedQntity")
                    gtOrderqty = gtOrderqty + dt.Rows(i)("Orderedqty")
                    tOrderqty = tOrderqty + dt.Rows(i)("Orderedqty")

                    dRow("ReceivedQnt") = dt.Rows(i)("ReceivedQnt")
                    gtRecvdQty = gtRecvdQty + dt.Rows(i)("ReceivedQnt")
                    tRecvdQty = tRecvdQty + dt.Rows(i)("ReceivedQnt")

                    gtExcessQty = gtExcessQty + dt.Rows(i)("Excess")
                    tExcessQty = tExcessQty + dt.Rows(i)("Excess")

                    gtRjctdQty = gtRjctdQty + dt.Rows(i)("RejectedQty")
                    tRjctdQty = tRjctdQty + dt.Rows(i)("RejectedQty")

                    gtPendingQty = gtPendingQty + dt.Rows(i)("PendingQty")
                    tPendingQty = tPendingQty + dt.Rows(i)("PendingQty")

                    dRow("Rate") = dt.Rows(i)("Rate")
                    gtMRP = gtMRP + dt.Rows(i)("Rate")
                    tMRP = tMRP + dt.Rows(i)("Rate")

                    gtVAT = gtVAT + dt.Rows(i)("VAT")
                    tVAT = tVAT + dt.Rows(i)("VAT")

                    dRow("VATAmt") = dt.Rows(i)("VATAmt")
                    Totaltax = Totaltax + dt.Rows(i)("VATAmt")
                    gtVATAmt = gtVATAmt + dt.Rows(i)("VATAmt")
                    tVATAmt = tVATAmt + dt.Rows(i)("VATAmt")

                    dRow("CST") = dt.Rows(i)("CST")
                    gtCST = gtCST + dt.Rows(i)("CST")
                    tCST = tCST + dt.Rows(i)("CST")

                    gtCSTAmt = gtCSTAmt + dt.Rows(i)("CSTAmt")
                    tCSTAmt = tCSTAmt + dt.Rows(i)("CSTAmt")

                    gtBasicAmount = gtBasicAmount + dt.Rows(i)("BasicAmount")
                    tBasicAmount = tBasicAmount + dt.Rows(i)("BasicAmount")

                    Totaltax = Totaltax + dt.Rows(i)("CSTAmt")
                    dRow("Exise") = dt.Rows(i)("Exise")
                    gtExise = gtExise + dt.Rows(i)("Exise")
                    tExise = tExise + dt.Rows(i)("Exise")
                    dRow("ExiseAmt") = dt.Rows(i)("ExiseAmt")
                    gtExiseAmt = gtExiseAmt + dt.Rows(i)("ExiseAmt")
                    Totaltax = Totaltax + dt.Rows(i)("ExiseAmt")
                    tExiseAmt = tExiseAmt + dt.Rows(i)("ExiseAmt")
                    dRow("Discount") = dt.Rows(i)("Discount")
                    gtdiscount = gtdiscount + dt.Rows(i)("Discount")
                    tdiscount = tdiscount + dt.Rows(i)("Discount")
                    dRow("DiscountAmt") = dt.Rows(i)("DiscountAmt")
                    gtdiscountAmt = gtdiscountAmt + dt.Rows(i)("DiscountAmt")
                    tdiscountAmt = tdiscountAmt + dt.Rows(i)("DiscountAmt")

                    dRow("Amount") = dt.Rows(i)("Amount")
                    gtAmount = gtAmount + dt.Rows(i)("Amount")
                    tAmount = tAmount + dt.Rows(i)("Amount")

                    dRow("GSTRate") = dt.Rows(i)("GSTRate")
                    gtGSTRate = gtGSTRate + dt.Rows(i)("GSTRate")
                    tGSTRate = tGSTRate + dt.Rows(i)("GSTRate")

                    dRow("GSTAmount") = dt.Rows(i)("GSTAmount")
                    gtGSTAmount = gtGSTAmount + dt.Rows(i)("GSTAmount")
                    tGSTAmount = tGSTAmount + dt.Rows(i)("GSTAmount")

                    If icat = 1 Then
                        TotalAmt = Totaltax + (((dRow("Rate") * dRow("AcceptedQntity")) - dt.Rows(i)("DiscountAmt") + Convert.ToDecimal(dRow("GSTAmount"))))
                    End If
                    If icat = 2 Then
                        TotalAmt = (((dRow("Rate") * dRow("RejectedQty")) - (dRow("Discount") * dt.Rows(i)("RejectedQty"))) + Convert.ToDecimal(dRow("GSTAmount")))
                    End If
                    If icat = 3 Then
                        TotalAmt = (((dRow("VAT") * dRow("Excess")) + (dRow("CST") * dRow("Excess")) + (dRow("Exise") * dRow("Excess"))) + ((dRow("Rate") * dRow("Excess")) - (dRow("Discount") * dRow("Excess"))) + Convert.ToDecimal(dRow("GSTAmount")))
                    End If
                    If icat = 4 Then
                        TotalAmt = Totaltax + (((dRow("Rate") * dRow("AcceptedQntity")) - dt.Rows(i)("DiscountAmt")) + Convert.ToDecimal(dRow("GSTAmount")))
                    End If
                    GrandTotal = GrandTotal + dt.Rows(i)("TotalAmount")
                    Total = Total + TotalAmt
                Next
                If flag <> "" Then
                    dr = dt2.NewRow()
                    dr("OrderNo") = Convert.ToString(OrderNos)
                    dr("GinNo") = GinNos
                    dr("PV_DocRefNo") = Convert.ToString(InvoiceNo)
                    dr("Orderdate") = Convert.ToString(Odate)
                    dr("Supplier") = Convert.ToString(Splr)
                    dr("AcceptedQntity") = gtQty
                    dr("Orderedqty") = gtOrderqty
                    dr("ReceivedQnt") = gtRecvdQty
                    dr("Excess") = gtExcessQty
                    dr("RejectedQty") = gtRjctdQty
                    dr("PendingQty") = gtPendingQty
                    dr("Rate") = "None"
                    dr("VAT") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
                    dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
                    dr("CST") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
                    dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
                    dr("Exise") = gtExise
                    dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
                    dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscount))
                    dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscountAmt))

                    dr("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(gtAmount))
                    dr("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(gtGSTRate))
                    dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(gtGSTAmount))

                    dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
                    dr("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(gtBasicAmount))
                    dr("InvoiceDate") = Convert.ToString(InvoiceDate)
                    dt2.Rows.Add(dr)
                End If
            Else
                dview.Sort = " OrderNo, GinNo"
                dt = dview.ToTable()
                Totaltax = 0
                Dim Total, tQty, tMRP, tBasicAmount, tVAT, tVATAmt, tCST, tCSTAmt, tExise, tExiseAmt, tdiscount, tdiscountAmt, tOrderqty, tRecvdQty, tExcessQty, tRjctdQty, tPendingQty As Double
                Dim tAmount, tGSTRate, tGSTAmount As Double
                dt2.Columns.Add("SlNo")
                dt2.Columns.Add("OrderNo")
                dt2.Columns.Add("GinNo")
                dt2.Columns.Add("PV_DocRefNo")
                dt2.Columns.Add("Commodity")
                dt2.Columns.Add("Description")
                dt2.Columns.Add("Orderdate")
                dt2.Columns.Add("Supplier")
                dt2.Columns.Add("AcceptedQntity")
                dt2.Columns.Add("Orderedqty")
                dt2.Columns.Add("ReceivedQnt")
                dt2.Columns.Add("Excess")
                dt2.Columns.Add("RejectedQty")
                dt2.Columns.Add("PendingQty")
                dt2.Columns.Add("Rate")
                dt2.Columns.Add("VAT")
                dt2.Columns.Add("VATAmt")
                dt2.Columns.Add("CST")
                dt2.Columns.Add("CSTAmt")
                dt2.Columns.Add("Exise")
                dt2.Columns.Add("ExiseAmt")
                dt2.Columns.Add("Discount")
                dt2.Columns.Add("DiscountAmt")

                dt2.Columns.Add("Amount")
                dt2.Columns.Add("GSTRate")
                dt2.Columns.Add("GSTAmount")

                dt2.Columns.Add("TotalAmount")
                dt2.Columns.Add("BasicAmount")
                dt2.Columns.Add("InvoiceDate")
                flag = "" : flag1 = ""
                For i = 0 To dt.Rows.Count - 1
                    dRow = dt2.NewRow()
                    Totaltax = 0
                    dRow("SlNo") = i + 1
                    If flag <> dt.Rows(i)("OrderNo") Then
                        If flag <> "" Then
                            flag2 = "a"
                            dr = dt2.NewRow()
                            dr("OrderNo") = <b>Total</b>
                            dr("AcceptedQntity") = gtQty
                            dr("Orderedqty") = gtOrderqty
                            dr("ReceivedQnt") = gtRecvdQty
                            dr("Excess") = gtExcessQty
                            dr("RejectedQty") = gtRjctdQty
                            dr("PendingQty") = gtPendingQty
                            dr("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(gtMRP))
                            dr("VAT") = gtVAT
                            dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
                            dr("CST") = gtCST
                            dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
                            dr("Exise") = gtExise
                            dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
                            dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscount))
                            dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscountAmt))

                            dr("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(gtAmount))
                            dr("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(gtGSTRate))
                            dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(gtGSTAmount))

                            dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
                            dr("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(gtBasicAmount))
                            dr("InvoiceDate") = "" ' InvoiceDate
                            dt2.Rows.Add(dr)
                            gtQty = 0 : gtMRP = 0 : gtVAT = 0 : gtVATAmt = 0 : gtBasicAmount = 0 : gtCST = 0 : gtCSTAmt = 0 : gtExise = 0 : gtExiseAmt = 0 : gtdiscount = 0 : gtdiscountAmt = 0 : GrandTotal = 0 : pending = 0 : gtOrderqty = 0 : gtRecvdQty = 0 : gtExcessQty = 0 : gtRjctdQty = 0 : gtPendingQty = 0
                            gtAmount = 0 : gtGSTRate = 0 : gtGSTAmount = 0
                        End If
                        dRow("OrderNo") = dt.Rows(i)("OrderNo")
                        flag = dt.Rows(i)("OrderNo")
                    End If
                    If flag1 <> dt.Rows(i)("GinNo") Then
                        If flag1 <> "" And flag2 <> "a" Then
                            dr = dt2.NewRow()
                            dr("GinNo") = "Total"
                            dr("AcceptedQntity") = tQty
                            dr("Orderedqty") = tOrderqty
                            dr("ReceivedQnt") = tRecvdQty
                            dr("Excess") = tExcessQty
                            dr("RejectedQty") = tRjctdQty
                            dr("PendingQty") = tPendingQty
                            dr("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(tMRP))
                            dr("VAT") = tVAT
                            dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(tVATAmt))
                            dr("CST") = tCST
                            dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(tCSTAmt))
                            dr("Exise") = tExise
                            dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(tExiseAmt))
                            dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(tdiscount))
                            dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(tdiscountAmt))

                            dr("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(tAmount))
                            dr("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(tGSTRate))
                            dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(tGSTAmount))

                            dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(Total))
                            dr("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(tBasicAmount))
                            dr("InvoiceDate") = "" 'InvoiceDate
                            Total = 0 : tQty = 0 : tMRP = 0 : tVAT = 0 : tVATAmt = 0 : tCST = 0 : tBasicAmount = 0 : tCSTAmt = 0 : tExise = 0 : tExiseAmt = 0 : tdiscount = 0 : tdiscountAmt = 0 : GrandTotal = 0 : pending = 0 : tOrderqty = 0 : tRecvdQty = 0 : tExcessQty = 0 : tRjctdQty = 0 : tPendingQty = 0
                            tAmount = 0 : tGSTRate = 0 : tGSTAmount = 0
                            dt2.Rows.Add(dr)
                        End If
                        dRow("GinNo") = dt.Rows(i)("GinNo")
                        flag1 = dt.Rows(i)("GinNo")
                    End If
                    dRow("Commodity") = dt.Rows(i)("Commodity")
                    dRow("Description") = dt.Rows(i)("Description")
                    dRow("Orderdate") = dt.Rows(i)("Orderdate")

                    InvoiceDate = dt.Rows(i)("InvoiceDate")
                    dRow("InvoiceDate") = dt.Rows(i)("InvoiceDate")

                    dRow("Orderdate") = dt.Rows(i)("Orderdate")
                    dRow("Supplier") = dt.Rows(i)("Supplier")
                    dRow("AcceptedQntity") = dt.Rows(i)("AcceptedQntity")
                    dRow("PV_DocRefNo") = dt.Rows(i)("PV_DocRefNo")
                    gtQty = gtQty + dRow("AcceptedQntity")
                    tQty = tQty + dRow("AcceptedQntity")
                    dRow("Orderedqty") = dt.Rows(i)("Orderedqty")
                    gtOrderqty = gtOrderqty + dRow("Orderedqty")
                    tOrderqty = tOrderqty + dRow("Orderedqty")

                    dRow("ReceivedQnt") = dt.Rows(i)("ReceivedQnt")
                    gtRecvdQty = gtRecvdQty + dRow("ReceivedQnt")
                    tRecvdQty = tRecvdQty + dRow("ReceivedQnt")

                    dRow("BasicAmount") = dt.Rows(i)("BasicAmount")
                    gtBasicAmount = gtBasicAmount + dt.Rows(i)("BasicAmount")
                    tBasicAmount = tBasicAmount + dt.Rows(i)("BasicAmount")

                    dRow("Excess") = dt.Rows(i)("Excess")
                    gtExcessQty = gtExcessQty + dRow("Excess")
                    tExcessQty = tExcessQty + dRow("Excess")

                    dRow("RejectedQty") = dt.Rows(i)("RejectedQty")
                    gtRjctdQty = gtRjctdQty + dRow("RejectedQty")
                    tRjctdQty = tRjctdQty + dRow("RejectedQty")

                    dRow("PendingQty") = dt.Rows(i)("PendingQty")
                    gtPendingQty = gtPendingQty + dRow("PendingQty")
                    tPendingQty = tPendingQty + dRow("PendingQty")

                    dRow("Rate") = dt.Rows(i)("Rate")
                    gtMRP = gtMRP + dRow("Rate")
                    tMRP = tMRP + dRow("Rate")

                    dRow("VAT") = dt.Rows(i)("VAT")
                    gtVAT = "0.00" 'gtVAT + dRow("VAT")
                    tVAT = "0.00" 'tVAT + dRow("VAT")

                    dRow("VATAmt") = dt.Rows(i)("VATAmt")
                    Totaltax = Totaltax + dRow("VATAmt")
                    gtVATAmt = gtVATAmt + dRow("VATAmt")
                    tVATAmt = tVATAmt + dRow("VATAmt")
                    dRow("CST") = dt.Rows(i)("CST")
                    gtCST = gtCST + dRow("CST")
                    tCST = tCST + dRow("CST")
                    dRow("CSTAmt") = dt.Rows(i)("CSTAmt")
                    gtCSTAmt = gtCSTAmt + dRow("CSTAmt")
                    tCSTAmt = tCSTAmt + dRow("CSTAmt")
                    Totaltax = Totaltax + dRow("CSTAmt")
                    dRow("Exise") = dt.Rows(i)("Exise")
                    gtExise = gtExise + dRow("Exise")
                    tExise = tExise + dRow("Exise")
                    dRow("ExiseAmt") = dt.Rows(i)("ExiseAmt")
                    gtExiseAmt = gtExiseAmt + dRow("ExiseAmt")
                    Totaltax = Totaltax + dRow("ExiseAmt")
                    tExiseAmt = tExiseAmt + dRow("ExiseAmt")
                    dRow("Discount") = dt.Rows(i)("Discount")
                    gtdiscount = gtdiscount + dRow("Discount")
                    tdiscount = tdiscount + dRow("Discount")
                    dRow("DiscountAmt") = dt.Rows(i)("DiscountAmt")
                    gtdiscountAmt = gtdiscountAmt + dRow("DiscountAmt")
                    tdiscountAmt = tdiscountAmt + dRow("DiscountAmt")

                    dRow("Amount") = dt.Rows(i)("Amount")
                    gtAmount = gtAmount + dt.Rows(i)("Amount")
                    tAmount = tAmount + dt.Rows(i)("Amount")

                    dRow("GSTRate") = dt.Rows(i)("GSTRate")
                    gtGSTRate = gtGSTRate + dt.Rows(i)("GSTRate")
                    tGSTRate = tGSTRate + dt.Rows(i)("GSTRate")

                    dRow("GSTAmount") = dt.Rows(i)("GSTAmount")
                    gtGSTAmount = gtGSTAmount + dt.Rows(i)("GSTAmount")
                    tGSTAmount = tGSTAmount + dt.Rows(i)("GSTAmount")

                    If icat = 1 Then
                        TotalAmt = Totaltax + (((dRow("Rate") * dRow("AcceptedQntity")) - dRow("DiscountAmt")) + Convert.ToDecimal(dRow("GSTAmount")))
                    End If
                    If icat = 2 Then
                        TotalAmt = (((dRow("Rate") * dRow("RejectedQty")) - (dRow("Discount") * dRow("RejectedQty"))) + Convert.ToDecimal(dRow("GSTAmount")))
                    End If
                    If icat = 3 Then
                        TotalAmt = (((dRow("VAT") * dRow("Excess")) + (dRow("CST") * dRow("Excess")) + (dRow("Exise") * dRow("Excess"))) + ((dRow("Rate") * dRow("Excess")) - (dRow("Discount") * dRow("Excess"))) + Convert.ToDecimal(dRow("GSTAmount")))
                    End If
                    If icat = 4 Then
                        TotalAmt = Totaltax + (((dRow("Rate") * dRow("AcceptedQntity")) - dRow("DiscountAmt")) + Convert.ToDecimal(dRow("GSTAmount")))
                    End If
                    dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(TotalAmt))
                    GrandTotal = GrandTotal + dRow("TotalAmount")
                    Total = Total + dRow("TotalAmount")
                    dt2.Rows.Add(dRow)
                Next
                If flag <> "" Then
                    dr = dt2.NewRow()
                    dr("OrderNo") = "Total"
                    dr("AcceptedQntity") = gtQty
                    dr("Orderedqty") = gtOrderqty
                    dr("ReceivedQnt") = gtRecvdQty
                    dr("Excess") = gtExcessQty
                    dr("RejectedQty") = gtRjctdQty
                    dr("PendingQty") = gtPendingQty
                    dr("Rate") = String.Format("{0:0.00}", Convert.ToDecimal(gtMRP))
                    dr("VAT") = gtVAT
                    dr("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtVATAmt))
                    dr("CST") = gtCST
                    dr("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtCSTAmt))
                    dr("Exise") = gtExise
                    dr("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtExiseAmt))
                    dr("Discount") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscount))
                    dr("DiscountAmt") = String.Format("{0:0.00}", Convert.ToDecimal(gtdiscountAmt))

                    dr("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(gtAmount))
                    dr("GSTRate") = String.Format("{0:0.00}", Convert.ToDecimal(gtGSTRate))
                    dr("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(gtGSTAmount))

                    dr("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(GrandTotal))
                    dr("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(gtBasicAmount))
                    dr("InvoiceDate") = ""
                    dt2.Rows.Add(dr)
                End If
            End If
            Return dt2
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDetailsAll(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal icat As Integer, ByVal iorder As Integer, ByVal isuuplier As Integer, ByVal InvoiceId As Integer, ByVal iCommodity As Integer, ByVal iItem As Integer, ByVal ivat As String, ByVal iExcise As String, ByVal iCst As String, ByVal iDiscount As String, ByVal iFromDt As Date, ByVal iToDt As Date, ByVal iZone As Integer, ByVal iRegion As Integer, ByVal iArea As Integer, ByVal iBranch As Integer)
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable
        Dim objFasGnrl As New clsFASGeneral
        Dim flag2 As String = "" : Dim flag3 As String = "" : Dim OrderNo As String = 0 : Dim Orderdate As String = "" : Dim GinNo As String = 0 : Dim Supplier As String = 0 : Dim Description As String = 0 : Dim Commodity As String = 0
        Dim TotalAmt, BasicAmount As Double
        Dim pending As Double
        Try
            dt.Columns.Add("SlNo")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("PV_DocRefNo")
            dt.Columns.Add("OrderNo")
            dt.Columns.Add("Orderdate")
            dt.Columns.Add("GinNo")
            dt.Columns.Add("Supplier")
            dt.Columns.Add("AcceptedQntity")
            dt.Columns.Add("Orderedqty")
            dt.Columns.Add("ReceivedQnt")
            dt.Columns.Add("Excess")
            dt.Columns.Add("RejectedQty")
            dt.Columns.Add("PendingQty")
            dt.Columns.Add("Rate")
            dt.Columns.Add("VAT")
            dt.Columns.Add("VATAmt")
            dt.Columns.Add("CST")
            dt.Columns.Add("CSTAmt")
            dt.Columns.Add("Exise")
            dt.Columns.Add("ExiseAmt")
            dt.Columns.Add("Discount")
            dt.Columns.Add("DiscountAmt")
            dt.Columns.Add("Amount")
            dt.Columns.Add("GSTRate")
            dt.Columns.Add("GSTAmount")
            dt.Columns.Add("TotalAmount")
            dt.Columns.Add("InvoiceDate")
            dt.Columns.Add("BasicAmount")
            'sSql = "" : sSql = "select f.POM_ZoneID,f.POM_RegionID,f.POM_AreaID,f.POM_BranchID,PV_ID,PV_OrderNo,f.POM_OrderNo,PV_GinNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_DocRefNo,PV_BillNo,PGM_InvoiceDate,"
            'sSql = sSql & " b.PIA_ID,b.PIA_OrderID,b.PIA_GINID,b.PIA_Commodity,b.PIA_DescriptionID,b.PIA_HistoryID,b.PIA_UnitID,b.PIA_MRP as Rate,b.PIA_Status,"
            'sSql = sSql & " b.PIA_CompID,b.PIA_Delflag,b.PIA_Approver,c.Inv_Description,c.Inv_Code,c.Inv_Color,c.Inv_Size,"
            'sSql = sSql & " d.Inv_Description Commodity,"
            'sSql = sSql & " g.POD_MasterID,g.POD_HistoryID,g.POD_Rate,g.POD_RateAmount,g.POD_Discount as Discount,g.POD_DiscountAmount as DiscountAmount,g.POD_Excise as Excise,g.POD_ExciseAmount as ExciseAmount,"
            'sSql = sSql & " g.POD_VAT as Vat,g.POD_VATAmount as VATAmount,g.POD_CST as CST,g.POD_CSTAmount as CSTAmount,g.POD_CompID,g.POD_Status,f.POM_Supplier,POM_OrderDate,"
            'sSql = sSql & "  g.POD_Quantity,g.POD_GSTRate,b.PIA_AcceptedQnt as AcceptedQnt,h.PGD_ReceivedQnt,h.PGD_RejectedQnt,h.PGD_Accepted,h.PGD_Excess,b.PIA_Excess,i.PIR_RejectedQty"
            'sSql = sSql & "  from Purchase_verification"
            'sSql = sSql & "  join Purchase_Invoice_Accepted b On PV_GinNo=b.PIA_GINID"
            'sSql = sSql & " join Inventory_Master c On b.PIA_DescriptionID=c.Inv_ID"
            'sSql = sSql & "  join Inventory_Master d On b.PIA_Commodity=d.Inv_ID"
            'sSql = sSql & "  join Purchase_GIN_Master e On PV_GinNo=e.PGM_ID "
            'sSql = sSql & "  join Purchase_Order_Master f On PV_OrderNo =f.POM_ID"
            'sSql = sSql & "  join Purchase_Order_Details g On  PIA_HistoryID=g.POD_HistoryID And PIA_OrderID=g.POD_MAsterID  "
            'sSql = sSql & " join Purchase_GIN_Details h on  PV_GinNo=h.PGD_MasterID and PIA_HistoryID=h.PGD_HistoryID"
            'sSql = sSql & " left join Purchase_Invoice_Rejected i on PV_GinNo=i.PIR_GINID and PIA_HistoryID=i.PIR_HistoryID  where PIA_CompID=" & iCompID & ""

            sSql = "" : sSql = "Select Distinct(PV_ID),h.PGD_ReceivedQnt,h.PGD_RejectedQnt,h.PGD_Accepted,h.PGD_Excess,h.PGD_OrderQnt,
    PV_GinNo,f.POM_ZoneID,f.POM_RegionID,f.POM_AreaID,f.POM_BranchID,PV_OrderNo,f.POM_OrderNo,e.PGM_GIN_Number,PV_CompID,PV_Status,PV_DocRefNo,PV_BillNo,PGM_InvoiceDate,
    c.Inv_Description,c.Inv_Code,c.Inv_Color,c.Inv_Size, d.Inv_Description Commodity,
    g.POD_MasterID,g.POD_HistoryID,g.POD_Discount as Discount,g.POD_DiscountAmount as DiscountAmount,
    g.POD_Excise as Excise,g.POD_ExciseAmount as ExciseAmount, g.POD_VAT as Vat,g.POD_VATAmount as VATAmount,g.POD_CST as CST,g.POD_CSTAmount as CSTAmount,g.POD_Status,
    f.POM_Supplier,POM_OrderDate,b.PIA_Excess,i.PIR_RejectedQty,g.POD_GSTRate,b.PIA_MRP as Rate
    from Purchase_verification  
    join Purchase_Invoice_Accepted b On b.PIA_GINID =PV_GinNo
    join Inventory_Master c On c.Inv_ID=b.PIA_DescriptionID 
    join Inventory_Master d On d.Inv_ID=b.PIA_Commodity
    join Purchase_GIN_Master e On e.PGM_ID=b.PIA_GINID
    join Purchase_Order_Master f On f.POM_ID = PV_OrderNo 
    Left join Purchase_Order_Details g On g.POD_MAsterID = PV_OrderNo And g.POD_HistoryID=PIA_HistoryID    
    join Purchase_GIN_Details h on  PV_GinNo=h.PGD_MasterID and PIA_HistoryID=h.PGD_HistoryID 
    left join Purchase_Invoice_Rejected i on PV_GinNo=i.PIR_GINID and PIA_HistoryID=i.PIR_HistoryID  
                                where PIA_CompID=" & iCompID & " "
            If iZone > 0 Then
                sSql = sSql & " And f.POM_ZoneID =" & iZone & ""
            End If
            If iRegion > 0 Then
                sSql = sSql & " And f.POM_RegionID =" & iRegion & ""
            End If
            If iArea > 0 Then
                sSql = sSql & " And f.POM_AreaID =" & iArea & ""
            End If
            If iBranch > 0 Then
                sSql = sSql & " And f.POM_BranchID =" & iBranch & ""
            End If

            If (iorder > 0) Then
                sSql = sSql & " and PIA_OrderID=" & iorder & ""
            End If

            If (isuuplier > 0) Then
                sSql = sSql & " and PGM_Supplier=" & isuuplier & ""
            End If

            If (iItem > 0) Then
                sSql = sSql & " and PIA_DescriptionID=" & iItem & ""
            End If

            If (iCommodity > 0) Then
                sSql = sSql & " and PIA_Commodity=" & iCommodity & ""
            End If
            If (InvoiceId > 0) Then
                sSql = sSql & " and PGM_ID=" & InvoiceId & ""
            End If


            If iFromDt <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And PGM_InvoiceDate Between " & objFasGnrl.FormatDtForRDBMS(iFromDt, "Q") & " "
            End If

            If iToDt <> "#1/1/1900 12:00:00 AM#" Then
                sSql = sSql & "And " & objFasGnrl.FormatDtForRDBMS(iToDt, "Q") & ""
            End If

            'sSql = sSql & " order by b.PIA_ID,b.PIA_DescriptionID"

            dtDetails = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt.NewRow()
                If IsDBNull(dtDetails.Rows(i)("Commodity")) = False Then
                    dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                Else
                    dRow("Commodity") = "Unknown"
                End If

                If IsDBNull(dtDetails.Rows(i)("Inv_Code")) = False Then
                    dRow("SlNo") = i + 1
                    dRow("Description") = dtDetails.Rows(i)("Inv_Code")
                Else
                    dRow("Description") = "Unknown"
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_OrderNo")) = False Then
                    dRow("OrderNo") = dtDetails.Rows(i)("POM_OrderNo")
                Else
                    dRow("OrderNo") = "0"
                End If
                If IsDBNull(dtDetails.Rows(i)("PV_DocRefNo")) = False Then
                    dRow("PV_DocRefNo") = dtDetails.Rows(i)("PV_DocRefNo")
                Else
                    dRow("PV_DocRefNo") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("POM_OrderDate")) = False Then
                    dRow("Orderdate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("POM_OrderDate"), "D")
                Else
                    dRow("Orderdate") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("PGM_GIN_Number")) = False Then
                    dRow("GinNo") = dtDetails.Rows(i)("PGM_GIN_Number")
                Else
                    dRow("GinNo") = "0"
                End If
                If IsDBNull(dtDetails.Rows(i)("POM_Supplier")) = False Then
                    dRow("Supplier") = objDb.SQLGetDescription(sNameSpace, "Select CSM_NAME from customerSupplierMaster where CSM_ID =" & dtDetails.Rows(i)("POM_Supplier") & " and CSM_CompID = " & iCompID & "")
                Else
                    dRow("Supplier") = "Unknown"
                End If
                If IsDBNull(dtDetails.Rows(i)("PGD_OrderQnt")) = False Then
                    dRow("Orderedqty") = dtDetails.Rows(i)("PGD_OrderQnt")
                    'dtDetails.Rows(i)("POD_Quantity")
                Else
                    dRow("Orderedqty") = 0
                End If

                'dRow("Orderedqty") = objDb.SQLGetDescription(sNameSpace, "Select POD_Quantity From Purchase_Order_Details Where POD_MasterID=" & dtDetails.Rows(i)("PV_OrderNo") & " And POD_Commodity=" & dtDetails.Rows(i)("PIA_Commodity") & " And POD_DescriptionID=" & dtDetails.Rows(i)("PIA_DescriptionID") & " And POD_HistoryID=" & dtDetails.Rows(i)("PIA_HistoryID") & " ")

                If IsDBNull(dtDetails.Rows(i)("PGD_Accepted")) = False Then
                    dRow("AcceptedQntity") = Convert.ToDecimal(dtDetails.Rows(i)("PGD_Accepted"))
                Else
                    dRow("AcceptedQntity") = 0
                End If

                'dRow("AcceptedQntity") = objDb.SQLGetDescription(sNameSpace, "Select PGD_Accepted From Purchase_GIN_Details Where PGD_MasterID=" & dtDetails.Rows(i)("PV_GINno") & " And PGD_CommodityID=" & dtDetails.Rows(i)("PIA_Commodity") & " And PGD_DescriptionID=" & dtDetails.Rows(i)("PIA_DescriptionID") & " And PGD_HistoryID=" & dtDetails.Rows(i)("PIA_HistoryID") & "  ")

                If IsDBNull(dtDetails.Rows(i)("PGD_ReceivedQnt")) = False Then
                    dRow("ReceivedQnt") = Convert.ToDecimal(dtDetails.Rows(i)("PGD_ReceivedQnt"))
                Else
                    dRow("ReceivedQnt") = 0
                End If

                'dRow("ReceivedQnt") = objDb.SQLGetDescription(sNameSpace, "Select PGD_ReceivedQnt From Purchase_GIN_Details Where PGD_MasterID=" & dtDetails.Rows(i)("PV_GINno") & " And PGD_CommodityID=" & dtDetails.Rows(i)("PIA_Commodity") & " And PGD_DescriptionID=" & dtDetails.Rows(i)("PIA_DescriptionID") & " And PGD_HistoryID=" & dtDetails.Rows(i)("PIA_HistoryID") & "  ")

                If IsDBNull(dtDetails.Rows(i)("PIA_Excess")) = False Then
                    dRow("Excess") = Convert.ToDecimal(dtDetails.Rows(i)("PIA_Excess"))
                Else
                    dRow("Excess") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("PIR_RejectedQty")) = False Then
                    dRow("RejectedQty") = Convert.ToDecimal(dtDetails.Rows(i)("PIR_RejectedQty"))
                Else
                    dRow("RejectedQty") = 0
                End If
                pending = dRow("Orderedqty") - dRow("ReceivedQnt")
                If pending < 0 Then
                    dRow("PendingQty") = 0
                Else
                    dRow("PendingQty") = pending
                End If

                If IsDBNull(dtDetails.Rows(i)("Rate")) = False Then
                    dRow("Rate") = dtDetails.Rows(i)("Rate")
                    TotalAmt = String.Format("{0:0.00}", Convert.ToDecimal(dRow("Rate") * dRow("AcceptedQntity")))
                    BasicAmount = String.Format("{0:0.00}", Convert.ToDecimal(dRow("Rate") * dRow("AcceptedQntity")))
                Else
                    dRow("Rate") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("Discount")) = False Then
                    dRow("Discount") = dtDetails.Rows(i)("Discount")
                Else
                    dRow("Discount") = "0"
                End If


                If IsDBNull(dtDetails.Rows(i)("DiscountAmount")) = False And dtDetails.Rows(i)("DiscountAmount") <> "" Then
                    dRow("DiscountAmt") = String.Format("{0:0.00}", (Convert.ToDecimal(BasicAmount) * Convert.ToDecimal(dtDetails.Rows(i)("Discount")) / 100))
                Else
                    dRow("DiscountAmt") = "0"
                End If

                If IsDBNull(dtDetails.Rows(i)("Vat")) = False Then
                    dRow("VAT") = dtDetails.Rows(i)("Vat")
                Else
                    dRow("VAT") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("VATAmount")) = False Then
                    dRow("VATAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("Vat")) / 100))
                    'String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("VATAmount")))
                    TotalAmt = TotalAmt + dRow("VATAmt")
                Else
                    dRow("VATAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("CST")) = False And dtDetails.Rows(i)("CST") <> "" Then
                    dRow("CST") = dtDetails.Rows(i)("CST")
                Else
                    dRow("CST") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("CSTAmount")) = False Then
                    dRow("CSTAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("CST")) / 100))
                    'String.Format("{0:0.00}", Convert.ToDecimal(dtDetails.Rows(i)("CSTAmount")))
                    TotalAmt = TotalAmt + dRow("CSTAmt")
                Else
                    dRow("CSTAmt") = 0
                End If
                If IsDBNull(dtDetails.Rows(i)("Excise")) = False Then
                    dRow("Exise") = dtDetails.Rows(i)("Excise")
                Else
                    dRow("Exise") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("ExciseAmount")) = False Then
                    dRow("ExiseAmt") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dtDetails.Rows(i)("Excise")) / 100))
                    TotalAmt = TotalAmt + dRow("ExiseAmt")
                Else
                    dRow("ExiseAmt") = 0
                End If

                If IsDBNull(dtDetails.Rows(i)("PGM_InvoiceDate")) = False Then
                    dRow("InvoiceDate") = objFasGnrl.FormatDtForRDBMS(dtDetails.Rows(i)("PGM_InvoiceDate"), "D")
                Else
                    dRow("InvoiceDate") = "0"
                End If

                dRow("Amount") = String.Format("{0:0.00}", Convert.ToDecimal(BasicAmount - dRow("DiscountAmt")))

                If IsDBNull(dtDetails.Rows(i)("POD_GSTRate")) = False Then
                    dRow("GSTRate") = dtDetails.Rows(i)("POD_GSTRate")
                Else
                    dRow("GSTRate") = "0"
                End If
                'dRow("GSTRate") = objDb.SQLGetDescription(sNameSpace, "Select POD_GSTRate From Purchase_Order_Details Where POD_MasterID=" & dtDetails.Rows(i)("PV_OrderNo") & " And POD_Commodity=" & dtDetails.Rows(i)("PIA_Commodity") & " And POD_DescriptionID=" & dtDetails.Rows(i)("PIA_DescriptionID") & " And POD_HistoryID=" & dtDetails.Rows(i)("PIA_HistoryID") & " ")

                dRow("GSTAmount") = String.Format("{0:0.00}", Convert.ToDecimal(((BasicAmount - dRow("DiscountAmt")) * dRow("GSTRate")) / 100))

                dRow("TotalAmount") = String.Format("{0:0.00}", Convert.ToDecimal(BasicAmount - dRow("DiscountAmt")) + Convert.ToDecimal(dRow("GSTAmount")))
                dRow("BasicAmount") = String.Format("{0:0.00}", Convert.ToDecimal(BasicAmount) - Convert.ToDecimal(dRow("DiscountAmt")))

                dt.Rows.Add(dRow)
            Next
            ' sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " And CSM_DelFlag='A'"
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function LoadSuppliers(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select CSM_ID,CSM_Name from CustomerSupplierMaster where CSM_CompID=" & iCompID & " And CSM_DelFlag='A'"
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Commodity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select Inv_ID,Inv_Description,Inv_Parent from Inventory_Master where Inv_Parent=0 and Inv_CompID=" & iCompID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Public Function GetSupplierName(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iSupplier As Integer)
    '    Dim sSql As String = "", sCOde As String = ""
    '    Dim dr As OleDb.OleDbDataReader
    '    Try
    '        sSql = "Select CSM_NAME from customerSupplierMaster where CSM_ID =" & iSupplier & " and CSM_CompID = " & iCompID & ""
    '        Return objDb.SQLExecuteScalar(sNameSpace, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Public Function Item(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Try
            If iCommodity = 0 Then
                sSql = "select Inv_ID,Inv_Description,Inv_Parent from Inventory_Master where Inv_Parent!=0 and Inv_CompID=" & iCompID & ""
            Else
                sSql = "select Inv_ID,Inv_Description,Inv_Parent from Inventory_Master where Inv_Parent=" & iCommodity & " and Inv_CompID=" & iCompID & ""
            End If

            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Order(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select POM_ID,POM_OrderNo from Purchase_Order_Master where POM_Status='A' And POM_CompID=" & iCompID & " Order By POM_ID desc"

            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadInwardNo(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iTransactionID As Integer) As DataTable
        Dim sSql As String = ""
        Try

            sSql = "Select PGM_ID,PGM_DocumentRefNo from Purchase_GIN_Master where PGM_DocumentRefNo In"
            sSql = sSql & "(Select PV_DocRefNo From Purchase_verification where PV_CompID=1  And PV_OrderNo=" & iTransactionID & ")  "
            sSql = sSql & " And PGM_OrderID=" & iTransactionID & " And "
            sSql = sSql & "PGM_CompID=1 and PGM_YearID =" & iYearID & ""
            Return objDb.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Invoice(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "select PV_ID, PV_BillNo from Purchase_verification where PV_OrderNo=" & iorder & " and PV_CompID=" & iCompID & " Order By PV_ID "
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SupplierMaster(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iorder As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            sSql = "select * from Purchase_Order_Master Join customerSupplierMaster b on POM_Supplier=b.CSM_ID where  POM_ID=" & iorder & " And POM_CompID=" & iCompID & " "
            dt = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            Return dt

        Catch ex As Exception
            Throw
        End Try
    End Function

End Class