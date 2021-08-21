Imports System
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Data
Public Class clsAccountDashBoard
    Private objDBL As New DatabaseLayer.DBHelper
    Private objGen As New BusinesLayer.clsFASGeneral

    Public Function LoadAccountDashBoardDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer, ByVal iHead As Integer)
        Dim sSql As String = "", aSql As String = ""
        Dim dc As New DataColumn
        Dim dt As New DataTable, dtDetails As New DataTable, dtOp As New DataTable
        Dim dtTrans As New DataTable
        Dim i As Integer = 0
        Dim dr As DataRow

        Dim dOpDebit As Double = 0, dOpCredit As Double = 0
        Dim dTransDebit As Double = 0, dTransCredit As Double = 0
        Dim dCloseDebit As Double = 0, dCloseCredit As Double = 0

        Dim dGrandTotalOpDebit As Double = 0, dGrandTotalOpCredit As Double = 0
        Dim dGrandTotalTransDebit As Double = 0, dGrandTotalTransCredit As Double = 0
        Dim dGrandTotalCloseDebit As Double = 0, dGrandTotalCloseCredit As Double = 0
        Try
            dc = New DataColumn("GLCode", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Description", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpDebit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpCredit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransDebit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransCredit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ClosingDebit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ClosingCredit", GetType(String))
            dt.Columns.Add(dc)

            If iHead = 0 Then
                sSql = "Select distinct(atd_Gl),b.gl_Desc,b.gl_Glcode from acc_Transactions_Details A join Chart_of_Accounts B on "
                sSql = sSql & "A.ATD_Status='A' and A.ATD_YearID =" & iYearID & " and "
                sSql = sSql & "A.ATD_CompID =" & iCompID & " and A.ATD_GL = b.gl_id order by A.ATD_GL"
            Else
                sSql = "Select distinct(atd_Gl),b.gl_Desc,b.gl_Glcode from acc_Transactions_Details A join Chart_of_Accounts B on "
                sSql = sSql & "A.ATD_Status='A' and A.ATD_YearID =" & iYearID & " and ATD_Head =" & iHead & " and "
                sSql = sSql & "A.ATD_CompID =" & iCompID & " and A.ATD_GL = b.gl_id order by A.ATD_GL"
            End If

            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1

                    dOpDebit = 0 : dOpCredit = 0
                    dTransDebit = 0 : dTransCredit = 0
                    dCloseDebit = 0 : dCloseCredit = 0

                    dr = dt.NewRow
                    dr("GLCode") = dtDetails.Rows(i)("gl_Glcode").ToString()
                    dr("Description") = dtDetails.Rows(i)("gl_Desc").ToString()

                    aSql = "" : aSql = "Select * from acc_OPening_Balance where Opn_GLID =" & dtDetails.Rows(i)("ATD_GL") & " and Opn_YearID =" & iYearID & " and Opn_CompID =" & iCompID & ""
                    dtOp = objDBL.SQLExecuteDataTable(sNameSpace, aSql)
                    If dtOp.Rows.Count > 0 Then

                        If dtOp.Rows(0)("Opn_DebitAmt").ToString() <> "" Then
                            dOpDebit = Convert.ToDecimal(dtOp.Rows(0)("Opn_DebitAmt").ToString()).ToString("#,##0.00")
                            dGrandTotalOpDebit = dGrandTotalOpDebit + dOpDebit
                            dr("OpDebit") = Convert.ToDecimal(dtOp.Rows(0)("Opn_DebitAmt").ToString()).ToString("#,##0.00")
                        Else
                            dr("OpDebit") = "0.00"
                        End If

                        If dtOp.Rows(0)("Opn_CreditAmount").ToString() <> "" Then
                            dOpCredit = Convert.ToDecimal(dtOp.Rows(0)("Opn_CreditAmount").ToString()).ToString("#,##0.00")
                            dGrandTotalOpCredit = dGrandTotalOpCredit + dOpCredit
                            dr("OpCredit") = Convert.ToDecimal(dtOp.Rows(0)("Opn_CreditAmount").ToString()).ToString("#,##0.00")
                        Else
                            dr("OpCredit") = "0.00"
                        End If
                    End If


                    aSql = "" : aSql = "Select Sum(ATD_Debit) as Debit,Sum(ATD_Credit) as Credit from acc_Transactions_Details where ATD_GL =" & dtDetails.Rows(i)("ATD_GL") & " and ATD_YearID =" & iYearID & " and ATD_CompID =" & iCompID & ""
                    dtTrans = objDBL.SQLExecuteDataTable(sNameSpace, aSql)
                    If dtTrans.Rows.Count > 0 Then

                        If dtTrans.Rows(0)("Debit").ToString() <> "" Then
                            dTransDebit = Convert.ToDecimal(dtTrans.Rows(0)("Debit").ToString()).ToString("#,##0.00")
                            dGrandTotalTransDebit = dGrandTotalTransDebit + dTransDebit
                            dr("TransDebit") = Convert.ToDecimal(dtTrans.Rows(0)("Debit").ToString()).ToString("#,##0.00")
                        Else
                            dr("TransDebit") = "0.00"
                        End If

                        If dtTrans.Rows(0)("Credit").ToString() <> "" Then
                            dTransCredit = Convert.ToDecimal(dtTrans.Rows(0)("Credit").ToString()).ToString("#,##0.00")
                            dGrandTotalTransCredit = dGrandTotalTransCredit + dTransCredit
                            dr("TransCredit") = Convert.ToDecimal(dtTrans.Rows(0)("Credit").ToString()).ToString("#,##0.00")
                        Else
                            dr("TransCredit") = "0.00"
                        End If
                    End If


                    Dim dClosingDborCr As Double = 0
                    dClosingDborCr = (dOpDebit + dTransDebit) - (dOpCredit + dTransCredit)
                    If dClosingDborCr > 0 Then

                        dCloseDebit = Convert.ToDecimal(dClosingDborCr).ToString("#,##0.00")
                        dGrandTotalCloseDebit = dGrandTotalCloseDebit + dCloseDebit
                        dr("ClosingDebit") = Convert.ToDecimal(dClosingDborCr).ToString("#,##0.00")
                        dr("ClosingCredit") = "0.00"
                    Else
                        dCloseCredit = Convert.ToDecimal(dClosingDborCr).ToString("#,##0.00")
                        dGrandTotalCloseCredit = dGrandTotalCloseCredit + dCloseCredit
                        dr("ClosingCredit") = Convert.ToDecimal(dClosingDborCr).ToString("#,##0.00")
                        dr("ClosingDebit") = "0.00"
                    End If
                    dt.Rows.Add(dr)
                Next

                dr = dt.NewRow
                dr("GLCode") = "GRAND TOTAL"
                dr("OpDebit") = Convert.ToDecimal(dGrandTotalOpDebit).ToString("#,##0.00")
                dr("OpCredit") = Convert.ToDecimal(dGrandTotalOpCredit).ToString("#,##0.00")

                dr("TransDebit") = Convert.ToDecimal(dGrandTotalTransDebit).ToString("#,##0.00")
                dr("TransCredit") = Convert.ToDecimal(dGrandTotalTransCredit).ToString("#,##0.00")

                dr("ClosingDebit") = Convert.ToDecimal(dGrandTotalCloseCredit).ToString("#,##0.00")
                dr("ClosingCredit") = Convert.ToDecimal(dGrandTotalCloseCredit).ToString("#,##0.00")
                dt.Rows.Add(dr)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadDashboardDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearID As Integer) As DataTable
        Dim dtDetails As New DataTable, dtOp As New DataTable, dt As New DataTable
        Dim dtPaymet As New DataTable, dtReceipt As New DataTable
        Dim dtPettyCash As New DataTable, dtJE As New DataTable
        Dim dc As New DataColumn
        Dim sSql As String = "", aSql As String = "", pSql As String = "", rSql As String = ""
        Dim pcSql As String = "", jSql As String = ""
        Dim dr As DataRow
        Dim isubGL As Integer = 0, i As Integer = 0

        Dim dTransDebit As Double = 0, dTransCredit As Double = 0
        Try
            dc = New DataColumn("Party", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("Date", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("BillNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("VoucherNo", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpDebit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("OpCredit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransDebit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("TransCredit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ClosingDebit", GetType(String))
            dt.Columns.Add(dc)
            dc = New DataColumn("ClosingCredit", GetType(String))
            dt.Columns.Add(dc)

            sSql = "Select * from acc_Transactions_Details where ATD_SubGL <> 0 and ATD_Status='A' and ATD_YearID =" & iYearID & " and "
            sSql = sSql & "ATD_CompID =" & iCompID & " and ATD_DbOrCr = 1 order by ATD_SubGL"
            dtDetails = objDBL.SQLExecuteDataTable(sNameSpace, sSql)
            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1

                    If (isubGL <> dtDetails.Rows(i)("ATD_SubGL").ToString()) Or (isubGL = 0) Then

                        If isubGL <> 0 Then
                            dr = dt.NewRow
                            dr("Party") = "TOTAL"
                            dr("TransDebit") = dTransDebit
                            dr("TransCredit") = dTransCredit
                            dt.Rows.Add(dr)

                            dr = dt.NewRow
                            dr("Party") = ""
                            dt.Rows.Add(dr)

                            dTransDebit = 0 : dTransCredit = 0
                        End If
                        dr = dt.NewRow
                        dr("Party") = objDBL.SQLExecuteScalar(sNameSpace, "Select gl_Desc from chart_of_Accounts where gl_id =" & dtDetails.Rows(i)("ATD_SubGL") & " and gl_CompID =" & iCompID & "")
                        aSql = "" : aSql = "Select * from acc_OPening_Balance where Opn_GLID =" & dtDetails.Rows(i)("ATD_SubGL") & " and Opn_YearID =" & iYearID & " and Opn_CompID =" & iCompID & ""
                        dtOp = objDBL.SQLExecuteDataTable(sNameSpace, aSql)
                        If dtOp.Rows.Count > 0 Then

                            If dtOp.Rows(0)("Opn_DebitAmt").ToString() <> "" Then
                                dr("OpDebit") = Convert.ToDecimal(dtOp.Rows(0)("Opn_DebitAmt").ToString()).ToString("#,##0.00")
                            Else
                                dr("OpDebit") = "0.00"
                            End If

                            If dtOp.Rows(0)("Opn_CreditAmount").ToString() <> "" Then
                                dr("OpCredit") = Convert.ToDecimal(dtOp.Rows(0)("Opn_CreditAmount").ToString()).ToString("#,##0.00")
                            Else
                                dr("OpCredit") = "0.00"
                            End If

                        End If
                        isubGL = dtDetails.Rows(i)("ATD_SubGL").ToString()
                        'If dtDetails.Rows.Count - 1 <> i Then
                        '    isubGL = dtDetails.Rows(i + 1)("ATD_SubGL").ToString()
                        'End If

                        dt.Rows.Add(dr)
                    End If


                    If dtDetails.Rows(i)("ATD_TrType").ToString() = "1" Then    'Payment
                        pSql = "" : pSql = "Select * from acc_Payment_Master where Acc_PM_ID = " & dtDetails.Rows(i)("ATD_BillID").ToString() & " and Acc_PM_CompID =" & iCompID & ""
                        dtPaymet = objDBL.SQLExecuteDataTable(sNameSpace, pSql)
                        If dtPaymet.Rows.Count > 0 Then
                            dr = dt.NewRow

                            If dtPaymet.Rows(0)("Acc_PM_BillDate").ToString() <> "" Then
                                dr("Date") = objGen.FormatDtForRDBMS(dtPaymet.Rows(0)("Acc_PM_BillDate").ToString(), "D")
                            Else
                                dr("Date") = ""
                            End If

                            If dtPaymet.Rows(0)("Acc_PM_BillNo").ToString() <> "" Then
                                dr("BillNo") = dtPaymet.Rows(0)("Acc_PM_BillNo").ToString()
                            Else
                                dr("BillNo") = ""
                            End If

                            If dtPaymet.Rows(0)("Acc_PM_TransactionNo").ToString() <> "" Then
                                dr("VoucherNo") = dtPaymet.Rows(0)("Acc_PM_TransactionNo").ToString()
                            Else
                                dr("VoucherNo") = ""
                            End If

                            If dtDetails.Rows(i)("ATD_Debit").ToString() <> "" Then
                                dTransDebit = dTransDebit + dtDetails.Rows(i)("ATD_Debit").ToString()
                                dr("TransDebit") = Convert.ToDecimal(dtDetails.Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                            Else
                                dr("TransDebit") = ""
                            End If

                            If dtDetails.Rows(i)("ATD_Credit").ToString() <> "" Then
                                dTransCredit = dTransCredit + dtDetails.Rows(i)("ATD_Credit").ToString()
                                dr("TransCredit") = Convert.ToDecimal(dtDetails.Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")
                            Else
                                dr("TransCredit") = ""
                            End If
                            dt.Rows.Add(dr)
                        End If
                    End If


                    If dtDetails.Rows(i)("ATD_TrType").ToString() = "3" Then    'Receipt
                        rSql = "" : rSql = "Select * from acc_Receipt_Master where Acc_RM_ID = " & dtDetails.Rows(i)("ATD_BillID").ToString() & " and Acc_RM_CompID =" & iCompID & ""
                        dtReceipt = objDBL.SQLExecuteDataTable(sNameSpace, rSql)
                        If dtReceipt.Rows.Count > 0 Then
                            dr = dt.NewRow

                            If dtReceipt.Rows(0)("Acc_RM_BillDate").ToString() <> "" Then
                                dr("Date") = objGen.FormatDtForRDBMS(dtReceipt.Rows(0)("Acc_RM_BillDate").ToString(), "D")
                            Else
                                dr("Date") = ""
                            End If

                            If dtReceipt.Rows(0)("Acc_RM_BillNo").ToString() <> "" Then
                                dr("BillNo") = dtReceipt.Rows(0)("Acc_RM_BillNo").ToString()
                            Else
                                dr("BillNo") = ""
                            End If

                            If dtReceipt.Rows(0)("Acc_RM_TransactionNo").ToString() <> "" Then
                                dr("VoucherNo") = dtReceipt.Rows(0)("Acc_RM_TransactionNo").ToString()
                            Else
                                dr("VoucherNo") = ""
                            End If

                            If dtDetails.Rows(i)("ATD_Debit").ToString() <> "" Then
                                dTransDebit = dTransDebit + dtDetails.Rows(i)("ATD_Debit").ToString()
                                dr("TransDebit") = Convert.ToDecimal(dtDetails.Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                            Else
                                dr("TransDebit") = ""
                            End If

                            If dtDetails.Rows(i)("ATD_Credit").ToString() <> "" Then
                                dTransCredit = dTransCredit + dtDetails.Rows(i)("ATD_Credit").ToString()
                                dr("TransCredit") = Convert.ToDecimal(dtDetails.Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")
                            Else
                                dr("TransCredit") = ""
                            End If
                            dt.Rows.Add(dr)
                        End If
                    End If


                    If dtDetails.Rows(i)("ATD_TrType").ToString() = "2" Then    'PettyCash
                        pcSql = "" : pcSql = "Select * from acc_PettyCash_Master where Acc_PCM_ID = " & dtDetails.Rows(i)("ATD_BillID").ToString() & " and Acc_PCM_CompID =" & iCompID & ""
                        dtPettyCash = objDBL.SQLExecuteDataTable(sNameSpace, pcSql)
                        If dtPettyCash.Rows.Count > 0 Then
                            dr = dt.NewRow

                            If dtPettyCash.Rows(0)("Acc_PCM_BillDate").ToString() <> "" Then
                                dr("Date") = objGen.FormatDtForRDBMS(dtPettyCash.Rows(0)("Acc_PCM_BillDate").ToString(), "D")
                            Else
                                dr("Date") = ""
                            End If

                            If dtPettyCash.Rows(0)("Acc_PCM_BillNo").ToString() <> "" Then
                                dr("BillNo") = dtPettyCash.Rows(0)("Acc_PCM_BillNo").ToString()
                            Else
                                dr("BillNo") = ""
                            End If

                            If dtPettyCash.Rows(0)("Acc_PCM_TransactionNo").ToString() <> "" Then
                                dr("VoucherNo") = dtPettyCash.Rows(0)("Acc_PCM_TransactionNo").ToString()
                            Else
                                dr("VoucherNo") = ""
                            End If

                            If dtDetails.Rows(i)("ATD_Debit").ToString() <> "" Then
                                dTransDebit = dTransDebit + dtDetails.Rows(i)("ATD_Debit").ToString()
                                dr("TransDebit") = Convert.ToDecimal(dtDetails.Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                            Else
                                dr("TransDebit") = ""
                            End If

                            If dtDetails.Rows(i)("ATD_Credit").ToString() <> "" Then
                                dTransCredit = dTransCredit + dtDetails.Rows(i)("ATD_Credit").ToString()
                                dr("TransCredit") = Convert.ToDecimal(dtDetails.Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")
                            Else
                                dr("TransCredit") = ""
                            End If
                            dt.Rows.Add(dr)
                        End If
                    End If


                    If dtDetails.Rows(i)("ATD_TrType").ToString() = "4" Then    'Journal Entry
                        jSql = "" : jSql = "Select * from acc_JE_Master where Acc_JE_ID = " & dtDetails.Rows(i)("ATD_BillID").ToString() & " and Acc_JE_CompID =" & iCompID & ""
                        dtJE = objDBL.SQLExecuteDataTable(sNameSpace, jSql)
                        If dtJE.Rows.Count > 0 Then
                            dr = dt.NewRow

                            If dtJE.Rows(0)("Acc_JE_BillDate").ToString() <> "" Then
                                dr("Date") = objGen.FormatDtForRDBMS(dtJE.Rows(0)("Acc_JE_BillDate").ToString(), "D")
                            Else
                                dr("Date") = ""
                            End If

                            If dtJE.Rows(0)("Acc_JE_BillNo").ToString() <> "" Then
                                dr("BillNo") = dtJE.Rows(0)("Acc_JE_BillNo").ToString()
                            Else
                                dr("BillNo") = ""
                            End If

                            If dtJE.Rows(0)("Acc_JE_TransactionNo").ToString() <> "" Then
                                dr("VoucherNo") = dtJE.Rows(0)("Acc_JE_TransactionNo").ToString()
                            Else
                                dr("VoucherNo") = ""
                            End If

                            If dtDetails.Rows(i)("ATD_Debit").ToString() <> "" Then
                                dTransDebit = dTransDebit + dtDetails.Rows(i)("ATD_Debit").ToString()
                                dr("TransDebit") = Convert.ToDecimal(dtDetails.Rows(i)("ATD_Debit").ToString()).ToString("#,##0.00")
                            Else
                                dr("TransDebit") = ""
                            End If

                            If dtDetails.Rows(i)("ATD_Credit").ToString() <> "" Then
                                dTransCredit = dTransCredit + dtDetails.Rows(i)("ATD_Credit").ToString()
                                dr("TransCredit") = Convert.ToDecimal(dtDetails.Rows(i)("ATD_Credit").ToString()).ToString("#,##0.00")
                            Else
                                dr("TransCredit") = ""
                            End If
                            dt.Rows.Add(dr)
                        End If
                    End If
                Next

                If (dtDetails.Rows.Count) = i Then
                    dr = dt.NewRow
                    dr("Party") = "TOTAL"
                    dr("TransDebit") = dTransDebit
                    dr("TransCredit") = dTransCredit
                    dt.Rows.Add(dr)
                    dTransDebit = 0 : dTransCredit = 0
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
