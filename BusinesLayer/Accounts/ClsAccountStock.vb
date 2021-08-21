Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class ClsAccountStock
    Dim objDB As New DBHelper
    Public Function loadDetailsReport(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim Duniqe As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim s As String
        Dim sites As String() = Nothing
        Dim values As String
        Dim s3, s4, s5, s6, s7, s8, s9, s10, s11, Total, qty, s0 As Double
        Dim flag3 As Integer = 0
        Try

            sSql = "" : sSql = "select a.Inv_Description as Description,a.Inv_ID,ISNULL(a.Inv_Size,0) as Inv_Size ,a.Inv_Code,a.Inv_Color,a.Inv_Parent,a.Inv_Acode,b.INVH_MRP,b.InvH_ID, ISNULL(c.SL_ClosingBalanceQty,0) as SL_ClosingBalanceQty,c.SL_ItemID,c.SL_historyId,d.Inv_Description as Commodity  from Inventory_Master a"
            sSql = sSql & " full Join inventory_master_history b On b.InvH_INV_ID=a.Inv_ID"
            sSql = sSql & " full Join Stock_Ledger c on b.InvH_ID=c.SL_historyId"
            sSql = sSql & " Join Inventory_Master d On d.Inv_ID=a.Inv_Parent where a.Inv_CompID=" & iCompID & ""


            'sSql = "" : sSql = "Select a.Inv_Description As Description,a.Inv_ID, ISNULL(a.Inv_Size,0) As Inv_Size ,a.Inv_Code,a.Inv_Color,a.Inv_Parent,a.Inv_Acode,b.INVH_MRP,"
            'sSql = sSql & " b.InvH_ID, ISNULL(c.SL_ClosingBalanceQty, 0) As SL_ClosingBalanceQty , c.SL_ItemID, c.SL_historyId, a.Inv_Description as Commodity  from Stock_Ledger c"
            'sSql = sSql & " Left Join inventory_master_history b on c.SL_HistoryID=b.InvH_ID And c.SL_ItemID=b.InvH_INV_ID left join Inventory_Master a on b.InvH_INV_ID =a.Inv_ID where  a.Inv_CompID=" & iCompID & ""
            If iCommodity <> 0 Then
                sSql = sSql & " And a.Inv_Parent = " & iCommodity & " "
            End If
            sSql = sSql & " order by a.Inv_ID"
            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            dt3 = dtDetails.Copy
            Duniqe = RemoveDublicateDescription(dt3)
            dt2.Columns.Add("SlNo")
            dt2.Columns.Add("MRP")
            dt2.Columns.Add("Commodity")
            dt2.Columns.Add("Description")
            dt2.Columns.Add("Colour")
            dt2.Columns.Add("three")
            dt2.Columns.Add("four")
            dt2.Columns.Add("five")
            dt2.Columns.Add("six")
            dt2.Columns.Add("seven")
            dt2.Columns.Add("eight")
            dt2.Columns.Add("nine")
            dt2.Columns.Add("ten")
            dt2.Columns.Add("leven")
            dt2.Columns.Add("Total")
            For j = 0 To Duniqe.Rows.Count - 1
                dRow = dt2.NewRow()
                dRow("SlNo") = j + 1
                qty = 0
                Total = 0 : s3 = 0 : s4 = 0 : s5 = 0 : s6 = 0 : s7 = 0 : s8 = 0 : s9 = 0 : s10 = 0 : s11 = 0 : s0 = 0
                For i = 0 To dtDetails.Rows.Count - 1

                    If dtDetails.Rows(i)("Commodity") = "<b>Total</b>" Then
                        dRow("SlNo") = ""
                    End If
                    If (Duniqe.Rows(j)("Description") = dtDetails.Rows(i)("Description")) Then
                        If (dtDetails.Rows(i)("Description") <> "<b>Total</b>") Then
                            dRow("MRP") = dtDetails.Rows(i)("INVH_MRP")
                            dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                            dRow("Description") = dtDetails.Rows(i)("Description")

                            If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                                dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                            Else
                                dRow("Colour") = ""
                            End If
                            If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                                If IsDBNull(dtDetails.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                    If (dtDetails.Rows(i)("SL_ClosingBalanceQty") <> 0) Then
                                        If (dtDetails.Rows(i)("Inv_Size") = 3) Then
                                            s3 = s3 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("three") = s3
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 4) Then
                                            s4 = s4 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("four") = s4
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 5) Or (dtDetails.Rows(i)("Inv_Size") = 39) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s5 = s5 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("five") = s5
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 6) Or (dtDetails.Rows(i)("Inv_Size") = 40) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s6 = s6 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("six") = s6
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 7) Or (dtDetails.Rows(i)("Inv_Size") = 41) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s7 = s7 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("seven") = s7
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 8) Or (dtDetails.Rows(i)("Inv_Size") = 42) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s8 = s8 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("eight") = s8
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 9) Or (dtDetails.Rows(i)("Inv_Size") = 43) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s9 = s9 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("nine") = s9
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 10) Or (dtDetails.Rows(i)("Inv_Size") = 44) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s10 = s10 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("ten") = s10
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 11) Or (dtDetails.Rows(i)("Inv_Size") = 45) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s11 = s11 + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            dRow("leven") = s11
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 0) Or (dtDetails.Rows(i)("Inv_Size") = "") Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s0 = s0 + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            dRow("Total") = s0

                                        Else
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s0 = s0 + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            dRow("Total") = s0
                                        End If
                                    End If
                                    dRow("Total") = qty
                                End If
                            End If
                        End If
                    End If
                Next
                dt2.Rows.Add(dRow)
            Next

            Return dt2
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function RemoveDublicateDescription(ByVal dt As DataTable) As DataTable
        Dim sSql As String = ""
        Dim hTable As New Hashtable
        Dim duplicateList As New ArrayList
        Try
            For Each DataRow As DataRow In dt.Rows
                If (hTable.Contains(DataRow("Description"))) Then
                    duplicateList.Add(DataRow)
                Else
                    hTable.Add(DataRow("Description"), String.Empty)
                End If
            Next
            For Each DataRow As DataRow In duplicateList
                dt.Rows.Remove(DataRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function Commodity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description,Inv_Parent from Inventory_Master where Inv_Parent=0 And Inv_CompID=" & iCompID & ""
            Return objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    Public Function UpdateStock(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal ItemID As Integer, ByVal StockID As Integer, ByVal qty As Decimal) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Update Stock_ledger Set SL_ClosingBalanceQty=" & qty & " "
            sSql = sSql & "Where SL_ID='" & StockID & "' And SL_CompID=" & iCompID & ""
            objDB.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function


    'Dim sSql As String = ""
    'Dim dt As New DataTable
    'Try
    '    sSql = "" : sSql = "Select * from Company_Accounting_Template where CMP_ID =" & iCompID & ""
    '    dt = DBHelper.ExecuteDataSet(sNameSpace, sSql).Tables(0)
    '    Return dt
    'Catch ex As Exception
    '    Throw
    'End Try
    ' End Function
    Public Function loadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer, ByVal iBranch As Integer) As DataTable
        Dim sSql As String = ""
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim Duniqe As New DataTable
        Dim dRow As DataRow
        Dim dtDetails As New DataTable
        Dim dtSales As New DataTable
        Dim flag As String = ""
        Dim flag1 As String = ""
        Dim s As String
        Dim sites As String() = Nothing
        Dim values As String
        Dim s3, s4, s5, s6, s7, s8, s9, s10, s11, Total, qty, s0 As Double
        Dim flag3 As Integer = 0

        Dim dOpnStock, dPurchasedQty, dSaleQty As Double
        Dim dOpnStockTotal, dPurchasedQtyTotal, dSaleQtyTotal, QtyTotal As Double
        Dim dtPurchase As New DataTable
        Try
            'Workin but no Sales Qty'

            sSql = "" : sSql = "select a.Inv_Description as Description,a.Inv_ID,ISNULL(a.Inv_Size,0) as Inv_Size ,a.Inv_Code,a.Inv_Color,a.Inv_Parent,a.Inv_Acode,b.INVH_MRP,b.InvH_ID, ISNULL(c.SL_ClosingBalanceQty,0) as SL_ClosingBalanceQty,c.SL_ID,c.SL_Commodity,c.SL_ItemID,c.SL_historyId,ISNULL(c.SL_PurchaseQty,0) As SL_PurchaseQty,ISNULL(SL_OpeningBalanceQty,0) As SL_OpeningBalanceQty,d.Inv_Description as Commodity  from Inventory_Master a"
            sSql = sSql & " full Join inventory_master_history b On b.InvH_INV_ID=a.Inv_ID"
            sSql = sSql & " full Join Stock_Ledger c on b.InvH_INV_ID=c.SL_ItemId and c.SL_HistoryID=0 And c.SL_OrderID=0 And c.SL_GINID=0"
            sSql = sSql & " Join Inventory_Master d On d.Inv_ID=a.Inv_Parent where a.Inv_CompID=" & iCompID & ""
            If iCommodity > 0 Then
                sSql = sSql & " And a.Inv_Parent = " & iCommodity & " "
            End If
            If iBranch > 0 Then
                sSql = sSql & " And c.SL_Branch = " & iBranch & " "
            End If
            sSql = sSql & " order by a.Inv_ID"

            'Working but no sales qty'

            'sSql = "" : sSql = "select a.Inv_Description as Description,a.Inv_ID,ISNULL(a.Inv_Size,0) as Inv_Size ,a.Inv_Code,a.Inv_Color,a.Inv_Parent,a.Inv_Acode,b.INVH_MRP,b.InvH_ID, ISNULL(c.SL_ClosingBalanceQty,0) as SL_ClosingBalanceQty,c.SL_ItemID,c.SL_historyId,ISNULL(c.SL_PurchaseQty,0) As SL_PurchaseQty,ISNULL(SL_OpeningBalanceQty,0) As SL_OpeningBalanceQty,d.Inv_Description as Commodity,ISNULL(e.SLS_SaleQnt,0) As SLS_SaleQnt from Inventory_Master a"
            'sSql = sSql & " full Join inventory_master_history b On b.InvH_INV_ID=a.Inv_ID "
            'sSql = sSql & " full Join Stock_Ledger c on b.InvH_ID=c.SL_historyId "
            'sSql = sSql & " Join Inventory_Master d On d.Inv_ID=a.Inv_Parent "
            'sSql = sSql & " Full Join Stock_Ledger_SalesDetails e On e.SLS_MasterID=c.SL_ID And e.SLS_Commodity=c.SL_Commodity And e.SLS_ItemID=c.SL_ItemID And e.SLS_HistoryID=c.SL_HistoryID "
            'sSql = sSql & " where a.Inv_CompID=" & iCompID & " "
            'If iCommodity <> 0 Then
            '    sSql = sSql & " And a.Inv_Parent = " & iCommodity & " "
            'End If
            'sSql = sSql & " order by a.Inv_ID"

            dtDetails = objDB.SQLExecuteDataTable(sNameSpace, sSql)
            dt3 = dtDetails.Copy
            Duniqe = RemoveDublicateDescription(dt3)
            dt2.Columns.Add("Sl No")
            dt2.Columns.Add("MRP")
            dt2.Columns.Add("Commodity")
            dt2.Columns.Add("Description")
            dt2.Columns.Add("Colour")
            dt2.Columns.Add("3")
            dt2.Columns.Add("4")
            dt2.Columns.Add("5")
            dt2.Columns.Add("6")
            dt2.Columns.Add("7")
            dt2.Columns.Add("8")
            dt2.Columns.Add("9")
            dt2.Columns.Add("10")
            dt2.Columns.Add("11")
            dt2.Columns.Add("OpnStock")
            dt2.Columns.Add("PurchaseQty")
            dt2.Columns.Add("SaleQty")
            dt2.Columns.Add("Total")
            For j = 0 To Duniqe.Rows.Count - 1
                dRow = dt2.NewRow()
                dRow("Sl No") = j + 1
                qty = 0
                Total = 0 : s3 = 0 : s4 = 0 : s5 = 0 : s6 = 0 : s7 = 0 : s8 = 0 : s9 = 0 : s10 = 0 : s11 = 0 : s0 = 0
                dOpnStock = 0 : dPurchasedQty = 0 : dSaleQty = 0
                'dOpnStockTotal = 0 : dPurchasedQtyTotal = 0 : dSaleQtyTotal = 0 : QtyTotal = 0
                'dOpnStockTotal = dOpnStockTotal + Duniqe.Rows(j)("SL_OpeningBalanceQty")

                'Sale Qty Total'
                If IsDBNull(Duniqe.Rows(j)("SL_Commodity")) = False And IsDBNull(Duniqe.Rows(j)("SL_ItemID")) = False And IsDBNull(Duniqe.Rows(j)("SL_HistoryID")) = False Then
                    'sSql = "" : sSql = "select sum(SLS_SaleQnt) As SLS_SaleQnt from Stock_Ledger_SalesDetails where SLS_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SLS_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & " And SLS_HistoryID = " & Duniqe.Rows(j)("SL_HistoryID") & " "
                    sSql = "" : sSql = "select sum(SLS_SaleQnt) As SLS_SaleQnt from Stock_Ledger_SalesDetails where SLS_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SLS_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & " And SLS_SaleOrderID=0 And SLS_DispatchID=0  "
                    dtSales = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                    If dtSales.Rows.Count > 0 Then
                        If IsDBNull(dtSales.Rows(0)("SLS_SaleQnt")) = False Then
                            dSaleQty = objDB.SQLGetDescription(sNameSpace, sSql)
                            dSaleQtyTotal = dSaleQtyTotal + dtSales.Rows(0)("SLS_SaleQnt")
                        Else
                            dSaleQty = 0
                            dSaleQtyTotal = dSaleQtyTotal + 0
                        End If
                    Else
                        dSaleQty = 0
                        dSaleQtyTotal = dSaleQtyTotal + 0
                    End If
                End If
                'Sale Qty Total'

                For i = 0 To dtDetails.Rows.Count - 1

                    If dtDetails.Rows(i)("Commodity") = "<b>Total</b>" Then
                        dRow("Sl No") = ""
                    End If
                    If (Duniqe.Rows(j)("Description") = dtDetails.Rows(i)("Description")) Then
                        If (dtDetails.Rows(i)("Description") <> "<b>Total</b>") Then
                            dRow("MRP") = dtDetails.Rows(i)("INVH_MRP")
                            dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                            dRow("Description") = dtDetails.Rows(i)("Description")

                            If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                                dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                            Else
                                dRow("Colour") = ""
                            End If

                            If IsDBNull(Duniqe.Rows(j)("SL_Commodity")) = False And IsDBNull(Duniqe.Rows(j)("SL_ItemID")) = False And IsDBNull(Duniqe.Rows(j)("SL_HistoryID")) = False Then
                                'sSql = "" : sSql = "select sum(SL_OpeningBalanceQty) As SL_OpeningBalanceQty from Stock_Ledger where SL_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SL_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & " And SL_HistoryID = " & Duniqe.Rows(j)("SL_HistoryID") & " "
                                sSql = "" : sSql = "select sum(SL_OpeningBalanceQty) As SL_OpeningBalanceQty from Stock_Ledger where SL_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SL_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & "  "
                                dtSales = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                                If dtSales.Rows.Count > 0 Then
                                    If IsDBNull(dtSales.Rows(0)("SL_OpeningBalanceQty")) = False Then
                                        dOpnStock = dtSales.Rows(0)("SL_OpeningBalanceQty")
                                        'objDB.SQLGetDescription(sNameSpace, sSql)
                                        'dOpnStockTotal = dOpnStockTotal + dtSales.Rows(0)("SL_OpeningBalanceQty")
                                    Else
                                        dOpnStock = 0
                                        'dOpnStockTotal = dOpnStockTotal + 0
                                    End If
                                Else
                                    dOpnStock = 0
                                    'dOpnStockTotal = dOpnStockTotal + 0
                                End If
                            End If

                            If IsDBNull(Duniqe.Rows(j)("SL_Commodity")) = False And IsDBNull(Duniqe.Rows(j)("SL_ItemID")) = False And IsDBNull(Duniqe.Rows(j)("SL_HistoryID")) = False Then
                                'sSql = "" : sSql = "select sum(SL_OpeningBalanceQty) As SL_OpeningBalanceQty from Stock_Ledger where SL_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SL_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & " And SL_HistoryID = " & Duniqe.Rows(j)("SL_HistoryID") & " "
                                sSql = "" : sSql = "select sum(SL_PurchaseQty) As SL_PurchaseQty from Stock_Ledger where SL_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SL_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & " And SL_HistoryID=0 And SL_OrderID=0 And SL_GINID=0  "
                                dtPurchase = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                                If dtPurchase.Rows.Count > 0 Then
                                    If IsDBNull(dtPurchase.Rows(0)("SL_PurchaseQty")) = False Then
                                        dPurchasedQty = dtPurchase.Rows(0)("SL_PurchaseQty")
                                        'objDB.SQLGetDescription(sNameSpace, sSql)
                                        'dPurchasedQtyTotal = dPurchasedQtyTotal + dtPurchase.Rows(0)("SL_PurchaseQty")
                                    Else
                                        dPurchasedQty = 0
                                        'dPurchasedQtyTotal = dPurchasedQtyTotal + 0
                                    End If
                                Else
                                    dPurchasedQty = 0
                                    'dPurchasedQtyTotal = dPurchasedQtyTotal + 0
                                End If
                            End If

                            'dOpnStockTotal = dOpnStockTotal + dOpnStock

                            'If IsDBNull(Duniqe.Rows(j)("SL_ID")) = False And IsDBNull(Duniqe.Rows(j)("SL_Commodity")) = False And IsDBNull(Duniqe.Rows(j)("SL_ItemID")) = False And IsDBNull(Duniqe.Rows(j)("SL_HistoryID")) = False Then
                            '    sSql = "" : sSql = "select sum(SLS_SaleQnt) As SLS_SaleQnt from Stock_Ledger_SalesDetails where SLS_MasterID = " & Duniqe.Rows(j)("SL_ID") & " And SLS_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SLS_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & " And SLS_HistoryID = " & Duniqe.Rows(j)("SL_HistoryID") & " "
                            '    dtSales = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                            '    If dtSales.Rows.Count > 0 Then
                            '        If IsDBNull(dtSales.Rows(0)("SLS_SaleQnt")) = False Then
                            '            dSaleQty = objDB.SQLGetDescription(sNameSpace, sSql)
                            '        Else
                            '            dSaleQty = 0
                            '        End If
                            '    Else
                            '        dSaleQty = 0
                            '    End If
                            'End If
                            If IsDBNull(Duniqe.Rows(j)("SL_Commodity")) = False And IsDBNull(Duniqe.Rows(j)("SL_ItemID")) = False And IsDBNull(Duniqe.Rows(j)("SL_HistoryID")) = False Then
                                'sSql = "" : sSql = "select sum(SLS_SaleQnt) As SLS_SaleQnt from Stock_Ledger_SalesDetails where SLS_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SLS_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & " And SLS_HistoryID = " & Duniqe.Rows(j)("SL_HistoryID") & " "
                                sSql = "" : sSql = "select sum(SLS_SaleQnt) As SLS_SaleQnt from Stock_Ledger_SalesDetails where SLS_Commodity = " & Duniqe.Rows(j)("SL_Commodity") & " And SLS_ItemID = " & Duniqe.Rows(j)("SL_ItemID") & " And SLS_SaleOrderID=0 And SLS_DispatchID=0  "
                                dtSales = objDB.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
                                If dtSales.Rows.Count > 0 Then
                                    If IsDBNull(dtSales.Rows(0)("SLS_SaleQnt")) = False Then
                                        dSaleQty = objDB.SQLGetDescription(sNameSpace, sSql)
                                        'dSaleQtyTotal = dSaleQtyTotal + dtSales.Rows(0)("SLS_SaleQnt")
                                    Else
                                        dSaleQty = 0
                                        'dSaleQtyTotal = dSaleQtyTotal + 0
                                    End If
                                Else
                                    dSaleQty = 0
                                    'dSaleQtyTotal = dSaleQtyTotal + 0
                                End If
                            End If


                            If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                                If IsDBNull(dtDetails.Rows(i)("SL_ClosingBalanceQty")) = False Then
                                    If (dtDetails.Rows(i)("SL_ClosingBalanceQty") <> 0) Then
                                        If (dtDetails.Rows(i)("Inv_Size") = 3) Then
                                            s3 = s3 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("3") = s3
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 4) Then
                                            s4 = s4 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("4") = s4
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 5) Or (dtDetails.Rows(i)("Inv_Size") = 39) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s5 = s5 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("5") = s5
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 6) Or (dtDetails.Rows(i)("Inv_Size") = 40) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s6 = s6 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("6") = s6
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 7) Or (dtDetails.Rows(i)("Inv_Size") = 41) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s7 = s7 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("7") = s7
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 8) Or (dtDetails.Rows(i)("Inv_Size") = 42) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s8 = s8 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("8") = s8
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 9) Or (dtDetails.Rows(i)("Inv_Size") = 43) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s9 = s9 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("9") = s9
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 10) Or (dtDetails.Rows(i)("Inv_Size") = 44) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s10 = s10 + dtDetails.Rows(i)("SL_ClosingBalanceQty")
                                            dRow("10") = s10
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            ' dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 11) Or (dtDetails.Rows(i)("Inv_Size") = 45) Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s11 = s11 + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            dRow("11") = s11
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                        If (dtDetails.Rows(i)("Inv_Size") = 0) Or (dtDetails.Rows(i)("Inv_Size") = "") Then
                                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            s0 = s0 + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                            dRow("Total") = s0
                                            'dOpnStock = dOpnStock + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                                            'dPurchasedQty = dPurchasedQty + Convert.ToDouble(dtDetails.Rows(i)("SL_PurchaseQty"))
                                            'dSaleQty = dSaleQty
                                            'dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty
                                        End If
                                    End If

                                    dOpnStockTotal = dOpnStockTotal + dOpnStock
                                    dPurchasedQtyTotal = dPurchasedQtyTotal + dPurchasedQty

                                    dRow("OpnStock") = dOpnStock
                                    dRow("PurchaseQty") = dPurchasedQty
                                    dRow("SaleQty") = dSaleQty

                                    dRow("Total") = ((dOpnStock + dPurchasedQty) - dSaleQty)
                                    'qty
                                    'If ((dtDetails.Rows(i)("SL_ClosingBalanceQty")).ToString).StartsWith("-") Then
                                    'Else
                                    QtyTotal = QtyTotal + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                                    'End If

                                End If
                            End If
                        End If
                    End If
                Next
                dt2.Rows.Add(dRow)
            Next

            dOpnStockTotal = 0 : dPurchasedQtyTotal = 0
            If dt2.Rows.Count > 0 Then
                For i = 0 To dt2.Rows.Count - 1
                    dOpnStockTotal = dOpnStockTotal + dt2.Rows(i)("OpnStock")
                    dPurchasedQtyTotal = dPurchasedQtyTotal + dt2.Rows(i)("PurchaseQty")
                Next
            End If

            'dRow = dt2.NewRow
            'dRow("Sl No") = <b>TOTAL</b>
            'dRow("MRP") = ""
            'dRow("Commodity") = ""
            'dRow("Description") = ""
            'dRow("Colour") = ""
            'dRow("3") = ""
            'dRow("4") = ""
            'dRow("5") = ""
            'dRow("6") = ""
            'dRow("7") = ""
            'dRow("8") = ""
            'dRow("9") = ""
            'dRow("10") = ""
            'dRow("11") = ""
            'dRow("OpnStock") = dOpnStockTotal
            'dRow("PurchaseQty") = dPurchasedQtyTotal
            'dRow("SaleQty") = dSaleQtyTotal
            'dRow("Total") = ((dOpnStockTotal + dPurchasedQtyTotal) - dSaleQtyTotal)
            ''QtyTotal
            'dt2.Rows.Add(dRow)

            Return dt2
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
