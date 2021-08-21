
Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsPhysicalStockUpdate
    Dim objDb As New DBHelper

    Public Function loadDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iCommodity As Integer) As DataTable
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
        Dim sdate As String = ""
        Dim s3, s4, s5, s6, s7, s8, s9, s10, s11, Total, qty, s0 As Double
        Dim flag3 As Integer = 0
        Try
            dt.Columns.Add("MRP")
            dt.Columns.Add("Commodity")
            dt.Columns.Add("Description")
            dt.Columns.Add("Colour")
            dt.Columns.Add("3")
            dt.Columns.Add("4")
            dt.Columns.Add("5")
            dt.Columns.Add("6")
            dt.Columns.Add("7")
            dt.Columns.Add("8")
            dt.Columns.Add("9")
            dt.Columns.Add("10")
            dt.Columns.Add("11")
            sSql = "" : sSql = "select a.Inv_Description as Description,c.SL_ID,c.SL_SaleQnty,a.Inv_ID,a.Inv_Size,a.Inv_Code,a.Inv_Color,a.Inv_Parent,a.Inv_Acode,b.INVH_MRP,b.InvH_ID,c.SL_ClosingBalanceQty,c.SL_ItemID,c.SL_CompID,c.SL_historyId,c.SL_OpeningBalanceQty,d.Inv_Description as Commodity  from Inventory_Master a"
            sSql = sSql & " full Join inventory_master_history b On b.InvH_INV_ID=a.Inv_ID"
            sSql = sSql & " full Join Stock_Ledger c on b.InvH_ID=c.SL_historyId"
            sSql = sSql & " Join Inventory_Master d On d.Inv_ID=a.Inv_Parent where c.SL_CompID=" & iCompID & " and SL_OrderID=0"
            If iCommodity <> 0 Then
                sSql = sSql & " And a.Inv_Parent = " & iCommodity & " "
            End If
            sSql = sSql & " order by c.SL_ID"
            dtDetails = objDb.SQLExecuteDataTable(sNameSpace, sSql)
            dt3 = dtDetails.Copy
            dt2.Columns.Add("Sl No")
            dt2.Columns.Add("SL_ID")
            dt2.Columns.Add("SL_slQty")
            dt2.Columns.Add("MRP")
            dt2.Columns.Add("Code")
            dt2.Columns.Add("StartDate")
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
            dt2.Columns.Add("Total")
            If IsDBNull(objDb.SQLExecuteScalar(sNameSpace, "select AS_StartDate from application_settings")) = False Then
                sdate = objDb.SQLExecuteScalar(sNameSpace, "select AS_StartDate from application_settings")
            Else
                sdate = ""
            End If
            For i = 0 To dtDetails.Rows.Count - 1
                dRow = dt2.NewRow()

                dRow("Sl No") = i + 1
                dRow("SL_ID") = dtDetails.Rows(i)("SL_ID")

                If IsDBNull(objDb.SQLExecuteScalar(sNameSpace, "Select sum(SLS_SaleQnt) from Stock_Ledger_SalesDetails where SLS_MasterID =" & dtDetails.Rows(i)("SL_ID") & " and SLS_CompID=" & dtDetails.Rows(i)("SL_CompID") & " ")) = False Then
                    dRow("SL_slQty") = objDb.SQLExecuteScalar(sNameSpace, " Select sum(SLS_SaleQnt) from Stock_Ledger_SalesDetails where SLS_MasterID =" & dtDetails.Rows(i)("SL_ID") & " and  SLS_CompID=" & dtDetails.Rows(i)("SL_CompID") & " ")
                Else
                    dRow("SL_slQty") = "0.000"
                End If
                dRow("StartDate") = sdate
                dRow("MRP") = dtDetails.Rows(i)("INVH_MRP")
                dRow("Commodity") = dtDetails.Rows(i)("Commodity")
                dRow("Description") = dtDetails.Rows(i)("Description")
                dRow("Code") = dtDetails.Rows(i)("Inv_Code")
                If IsDBNull(dtDetails.Rows(i)("Inv_Color")) = False Then
                    dRow("Colour") = dtDetails.Rows(i)("Inv_Color")
                Else
                    dRow("Colour") = ""
                End If
                If IsDBNull(dtDetails.Rows(i)("Inv_Size")) = False Then
                    If IsDBNull(dtDetails.Rows(i)("SL_OpeningBalanceQty")) = False Then
                        '  If (dtDetails.Rows(i)("SL_ClosingBalanceQty") <> 0) Then

                        If (dtDetails.Rows(i)("Inv_Size") = 3) Then
                            s3 = s3 + dtDetails.Rows(i)("SL_OpeningBalanceQty")
                            dRow("3") = s3 'dtDetails.Rows(i)("SL_ClosingBalanceQty")
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            'Else
                            '    dRow("3") = 0
                        End If

                        If (dtDetails.Rows(i)("Inv_Size") = 4) Then
                            s4 = s4 + dtDetails.Rows(i)("SL_OpeningBalanceQty")
                            dRow("4") = s4 'dtDetails.Rows(i)("SL_ClosingBalanceQty")
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            'Else
                            '    dRow("4") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 5) Or (dtDetails.Rows(i)("Inv_Size") = 39) Then
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            s5 = s5 + dtDetails.Rows(i)("SL_OpeningBalanceQty")
                            dRow("5") = s5 ' dtDetails.Rows(i)("SL_ClosingBalanceQty")

                            'Else
                            '    dRow("5") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 6) Or (dtDetails.Rows(i)("Inv_Size") = 40) Then
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            s6 = s6 + dtDetails.Rows(i)("SL_OpeningBalanceQty")
                            dRow("6") = s6 'dtDetails.Rows(i)("SL_ClosingBalanceQty")
                            'Else
                            '    dRow("6") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 7) Or (dtDetails.Rows(i)("Inv_Size") = 41) Then
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            s7 = s7 + dtDetails.Rows(i)("SL_OpeningBalanceQty")
                            dRow("7") = s7 'dtDetails.Rows(i)("SL_ClosingBalanceQty")
                            'Else
                            '    dRow("7") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 8) Or (dtDetails.Rows(i)("Inv_Size") = 42) Then
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            s8 = s8 + dtDetails.Rows(i)("SL_OpeningBalanceQty")
                            dRow("8") = s8 'dtDetails.Rows(i)("SL_ClosingBalanceQty")
                            'Else
                            '    dRow("8") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 9) Or (dtDetails.Rows(i)("Inv_Size") = 43) Then
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            s9 = s9 + dtDetails.Rows(i)("SL_OpeningBalanceQty")
                            dRow("9") = s9
                            'Else
                            '    dRow("9") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 10) Or (dtDetails.Rows(i)("Inv_Size") = 44) Then
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            s10 = s10 + dtDetails.Rows(i)("SL_OpeningBalanceQty")
                            dRow("10") = s10 'dtDetails.Rows(i)("SL_ClosingBalanceQty")

                            'Else
                            '    dRow("10") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 11) Or (dtDetails.Rows(i)("Inv_Size") = 45) Then
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            s11 = s11 + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            dRow("11") = s11 'dtDetails.Rows(i)("SL_ClosingBalanceQty")
                            'Else
                            '    dRow("11") = 0
                        End If
                        If (dtDetails.Rows(i)("Inv_Size") = 0) Or (dtDetails.Rows(i)("Inv_Size") = "") Then
                            qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            s0 = s0 + Convert.ToDouble(dtDetails.Rows(i)("SL_OpeningBalanceQty"))
                            dRow("Total") = s0 ' dtDetails.Rows(i)("SL_ClosingBalanceQty")
                            'Else
                            '    qty = qty + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                            '    s0 = s0 + Convert.ToDouble(dtDetails.Rows(i)("SL_ClosingBalanceQty"))
                            '    dRow("Total") = s0 ' dtDetails.Rows(i)("SL_ClosingBalanceQty")
                        End If
                    End If
                    dRow("Total") = qty
                    qty = 0
                End If
                'End If

                dt2.Rows.Add(dRow)
            Next

            Return dt2
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Commodity(ByVal sNameSpace As String, ByVal iCompID As Integer) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select Inv_ID,Inv_Description,Inv_Parent from Inventory_Master where Inv_Parent=0 And Inv_CompID=" & iCompID & ""
            Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStock(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal StockID As Integer, ByVal qty As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Stock_ledger Set SL_ClosingBalanceQty=" & Convert.ToDecimal(qty) & ",SL_OpeningBalanceQty=" & Convert.ToDecimal(qty) & " "
            sSql = sSql & " Where SL_ID=" & StockID & " And SL_CompID=" & iCompID & ""
            objDb.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
