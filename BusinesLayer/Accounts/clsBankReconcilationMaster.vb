Imports DatabaseLayer
Imports System
Imports System.Data
Imports BusinesLayer
Public Class clsBankReconcilationMaster
    Dim objDB As New DBHelper
    Dim FASGeneral As New clsFASGeneral
    Public Function LoadAllDetails(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sql As String
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim slno As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("BankName")
            dt.Columns.Add("fromDate")
            dt.Columns.Add("Todate")
            dt.Columns.Add("SerialNo")
            dt.Columns.Add("TrType")
            dt.Columns.Add("ChequeDate")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Status")
            dt.Columns.Add("BnkID")
            dt.Columns.Add("CCredit")
            dt.Columns.Add("CDabit")
            dt.Columns.Add("ABR_ValueDate")
            dt.Columns.Add("Remark")
            dt.Columns.Add("lblchequeno")

            sql = "select * from Acc_Bank_Reconcilation where ABR_CompID=" & iCompID & " and ABR_YearID=" & iYearId & ""
            dt1 = objDB.SQLExecuteDataSet(sNameSpace, sql).Tables(0)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    slno = slno + 1
                    dRow("SrNo") = slno
                    If IsDBNull(dt1.Rows(i)("ABR_ID")) = False Then
                        dRow("BnkID") = dt1.Rows(i)("ABR_ID")
                    Else
                        dRow("BnkID") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Bank")) = False Then
                        dRow("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt1.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & " ")
                    Else
                        dRow("BankName") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_FromDate")) = False Then
                        dRow("fromDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_FromDate"), "D")
                    Else
                        dRow("fromDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ToDate")) = False Then
                        dRow("Todate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ToDate"), "D")
                    Else
                        dRow("Todate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TransactionNo")) = False Then
                        dRow("SerialNo") = dt1.Rows(i)("ABR_TransactionNo")
                    Else
                        dRow("SerialNo") = ""
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_ValueDate")) = False Then
                        dRow("ABR_ValueDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ValueDate"), "D")
                    Else
                        dRow("ABR_ValueDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TrType")) = False Then
                        If dt1.Rows(i)("ABR_TrType") = 1 Then
                            dRow("TrType") = "PAYMENT"
                        ElseIf dt1.Rows(i)("ABR_TrType") = 3 Then
                            dRow("TrType") = "RECEIPT"
                        End If
                    Else
                        dRow("TrType") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ChequeDate")) = False Then
                        dRow("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ChequeDate"), "D")
                    Else
                        dRow("ChequeDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Debit")) = False Then
                        dRow("Debit") = Convert.ToDecimal(dt1.Rows(i)("ABR_Debit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("Debit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Credit")) = False Then
                        dRow("Credit") = Convert.ToDecimal(dt1.Rows(i)("ABR_Credit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("Credit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CCradit")) = False Then
                        dRow("CCredit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CCradit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CCredit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CDabit")) = False Then
                        dRow("CDabit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CDabit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CDabit") = "0.00"
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_Comment")) = False Then
                        dRow("Remark") = dt1.Rows(i)("ABR_Comment").ToString()
                    Else
                        dRow("Remark") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Status")) = False Then
                        If dt1.Rows(i)("ABR_Status").ToString() = "W" Then
                            dRow("Status") = "Amount Not Matched"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "C" Then
                            dRow("Status") = "amount not exist in Bank"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "B" Then
                            dRow("Status") = "amount not exist in company"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "A" Then
                            dRow("Status") = "Amount Matched"
                        End If
                    Else
                        dRow("ABR_Status") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ChequeNo")) = False Then
                        dRow("lblchequeno") = dt1.Rows(i)("ABR_ChequeNo").ToString()
                    Else
                        dRow("lblchequeno") = ""
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If

            sql = "select * from Acc_Bank_ReconcilationInAndOut where ABR_CompIDIO=" & iCompID & " and ABR_YearIDIO=" & iYearId & ""
            dt1 = objDB.SQLExecuteDataTable(sNameSpace, sql)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow

                    If IsDBNull(dt1.Rows(i)("ABR_IDIO")) = False Then
                        dRow("BnkID") = dt1.Rows(i)("ABR_IDIO")
                    Else
                        dRow("BnkID") = ""
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_TrTypeIO")) = False Then
                        If dt1.Rows(i)("ABR_TrTypeIO") = 1 Then
                            dRow("TrType") = "PAYMENT"
                        ElseIf dt1.Rows(i)("ABR_TrTypeIO") = 3 Then
                            dRow("TrType") = "RECEIPT"
                        End If
                    Else
                        dRow("TrType") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TransactionNoIO")) = False Then
                        dRow("SerialNo") = dt1.Rows(i)("ABR_TransactionNoIO")
                    Else
                        dRow("SerialNo") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_ValueDateIO")) = False Then
                        dRow("ABR_ValueDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ValueDateIO"), "D")
                    Else
                        dRow("ABR_ValueDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_ChequeDateIO")) = False Then
                        dRow("ChequeDate") = dt1.Rows(i)("ABR_ChequeDateIO")
                    Else
                        dRow("ChequeDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_ChequeNoIO")) = False Then
                        dRow("lblchequeno") = dt1.Rows(i)("ABR_ChequeNoIO").ToString()
                    Else
                        dRow("lblchequeno") = ""
                    End If
                    dRow("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt1.Rows(i)("ABR_BankIO") & " And gl_CompId=" & iCompID & "")
                    If IsDBNull(dt1.Rows(i)("ABR_FromDateIO")) = False Then
                        dRow("fromDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_FromDateIO"), "D")
                    Else
                        dRow("fromDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ToDateIO")) = False Then
                        dRow("Todate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ToDateIO"), "D")
                    Else
                        dRow("Todate") = ""
                    End If
                    dRow("Debit") = dt1.Rows(i)("ABR_DebitIO")
                    dRow("Credit") = dt1.Rows(i)("ABR_CreditIO")

                    If IsDBNull(dt1.Rows(i)("ABR_CCreditIO")) = False Then
                        dRow("CCredit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CCreditIO").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CCredit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CDebitIO")) = False Then
                        dRow("CDabit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CDebitIO").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CDabit") = "0.00"
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_StatusIO")) = False Then
                        If dt1.Rows(i)("ABR_StatusIO").ToString() = "W" Then
                            dRow("Status") = "Amount Not Matched"
                        ElseIf dt1.Rows(i)("ABR_StatusIO").ToString() = "C" Then
                            dRow("Status") = "amount not exist in Bank"
                        ElseIf dt1.Rows(i)("ABR_StatusIO").ToString() = "B" Then
                            dRow("Status") = "amount not exist in company"
                        ElseIf dt1.Rows(i)("ABR_StatusIO").ToString() = "A" Then
                            dRow("Status") = "Amount Matched"
                        End If
                    Else
                        dRow("ABR_Status") = ""
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If

            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateDescription(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankId As Integer, ByVal sComment As String, ByVal iJEID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "update Acc_Bank_Reconcilation set ABR_Comment='" & sComment & "',ABR_Status='A',ABR_JID=" & iJEID & " where ABR_CompID=" & iCompID & " and ABR_ID=" & iBankId & " "
            UpdateDescription = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function UpdateDescriptionAmountNotexist(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iBankId As Integer, ByVal sComment As String) As Integer
        Dim sSql As String
        Try
            sSql = "update Acc_Bank_Reconcilation set ABR_Comment='" & sComment & "',ABR_Status='A' where ABR_CompID=" & iCompID & " and ABR_ID=" & iBankId & "  "
            UpdateDescriptionAmountNotexist = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransactionType(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTransctionNo As String) As Integer
        Dim sSql As String
        Dim ID As Integer
        Try

            sSql = "select Acc_PM_ID from Acc_payment_master where Acc_PM_TransactionNo='" & sTransctionNo & "'"
            ID = objDB.SQLExecuteScalar(sNameSpace, sSql)
            sSql = "select ATD_TrType from Acc_transactions_details where ATD_BillId=" & ID & ""
            GetTransactionType = objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTransactionID(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sTransctionNo As String) As Integer
        Dim sSql As String
        Dim dbhelper As New DBHelper
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "" : sSql = "Select * from Acc_Payment_Master where Acc_PM_TransactionNo = '" & sTransctionNo & "'"
            dr = dbhelper.SQLDataReader(sNameSpace, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sSql = "select Acc_PM_ID from Acc_payment_master where Acc_PM_TransactionNo='" & sTransctionNo & "'"
                GetTransactionID = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Else
                sSql = "select Acc_RM_ID from acc_receipt_master where Acc_RM_TransactionNo='" & sTransctionNo & "'"
                GetTransactionID = objDB.SQLExecuteScalar(sNameSpace, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails2(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal Status As String) As DataTable
        Dim sql As String
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim slno As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("BankName")
            dt.Columns.Add("fromDate")
            dt.Columns.Add("Todate")
            dt.Columns.Add("SerialNo")
            dt.Columns.Add("TrType")
            dt.Columns.Add("ChequeDate")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Status")
            dt.Columns.Add("BnkID")
            dt.Columns.Add("CCredit")
            dt.Columns.Add("CDabit")
            dt.Columns.Add("ABR_ValueDate")
            dt.Columns.Add("Remark")
            dt.Columns.Add("lblchequeno")
            sql = "select * from Acc_Bank_Reconcilation where ABR_CompID=" & iCompID & " and ABR_YearID=" & iYearId & " and ABR_Status='" & Status & "'"
            dt1 = objDB.SQLExecuteDataSet(sNameSpace, sql).Tables(0)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    slno = slno + 1
                    dRow("SrNo") = slno
                    If IsDBNull(dt1.Rows(i)("ABR_ID")) = False Then
                        dRow("BnkID") = dt1.Rows(i)("ABR_ID")
                    Else
                        dRow("BnkID") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Bank")) = False Then
                        dRow("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt1.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & " ")
                    Else
                        dRow("BankName") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_FromDate")) = False Then
                        dRow("fromDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_FromDate"), "D")
                    Else
                        dRow("fromDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ToDate")) = False Then
                        dRow("Todate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ToDate"), "D")
                    Else
                        dRow("Todate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TransactionNo")) = False Then
                        dRow("SerialNo") = dt1.Rows(i)("ABR_TransactionNo")
                    Else
                        dRow("SerialNo") = ""
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_ValueDate")) = False Then
                        dRow("ABR_ValueDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ValueDate"), "D")
                    Else
                        dRow("ABR_ValueDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ChequeNo")) = False Then
                        dRow("lblchequeno") = dt1.Rows(i)("ABR_ChequeNo").ToString()
                    Else
                        dRow("lblchequeno") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_TrType")) = False Then
                        If dt1.Rows(i)("ABR_TrType") = 1 Then
                            dRow("TrType") = "PAYMENT"
                        ElseIf dt1.Rows(i)("ABR_TrType") = 3 Then
                            dRow("TrType") = "RECEIPT"
                        End If
                    Else
                        dRow("TrType") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ChequeDate")) = False Then
                        dRow("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ChequeDate"), "D")
                    Else
                        dRow("ChequeDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Debit")) = False Then
                        dRow("Debit") = Convert.ToDecimal(dt1.Rows(i)("ABR_Debit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("Debit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Credit")) = False Then
                        dRow("Credit") = Convert.ToDecimal(dt1.Rows(i)("ABR_Credit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("Credit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CCradit")) = False Then
                        dRow("CCredit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CCradit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CCredit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CDabit")) = False Then
                        dRow("CDabit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CDabit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CDabit") = "0.00"
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_Comment")) = False Then
                        dRow("Remark") = dt1.Rows(i)("ABR_Comment").ToString()
                    Else
                        dRow("Remark") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Status")) = False Then
                        If dt1.Rows(i)("ABR_Status").ToString() = "W" Then
                            dRow("Status") = "Amount Not Matched"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "C" Then
                            dRow("Status") = "amount not exist in Bank"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "B" Then
                            dRow("Status") = "amount not exist in company"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "A" Then
                            dRow("Status") = "Approved"
                        End If
                    Else
                        dRow("ABR_Status") = ""
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails3(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer, ByVal Status As String) As DataTable
        Dim sql As String
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim slno As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("BankName")
            dt.Columns.Add("fromDate")
            dt.Columns.Add("Todate")
            dt.Columns.Add("SerialNo")
            dt.Columns.Add("TrType")
            dt.Columns.Add("ChequeDate")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Status")
            dt.Columns.Add("BnkID")
            dt.Columns.Add("CCredit")
            dt.Columns.Add("CDabit")
            dt.Columns.Add("ABR_ValueDate")
            dt.Columns.Add("Remark")
            dt.Columns.Add("lblchequeno")
            sql = "select * from Acc_Bank_ReconcilationInAndOut where ABR_CompIDIO=" & iCompID & " and ABR_YearIDIO=" & iYearId & " and ABR_StatusIO='" & Status & "'"
            dt1 = objDB.SQLExecuteDataTable(sNameSpace, sql)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow
                    slno = slno + 1
                    dRow("SrNo") = slno
                    If IsDBNull(dt1.Rows(i)("ABR_IDIO")) = False Then
                        dRow("BnkID") = dt1.Rows(i)("ABR_IDIO")
                    Else
                        dRow("BnkID") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_TrTypeIO")) = False Then
                        If dt1.Rows(i)("ABR_TrTypeIO") = 1 Then
                            dRow("TrType") = "PAYMENT"
                        ElseIf dt1.Rows(i)("ABR_TrTypeIO") = 3 Then
                            dRow("TrType") = "RECEIPT"
                        End If
                    Else
                        dRow("TrType") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TransactionNoIO")) = False Then
                        dRow("SerialNo") = dt1.Rows(i)("ABR_TransactionNoIO")
                    Else
                        dRow("SerialNo") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_ValueDateIO")) = False Then
                        dRow("ABR_ValueDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ValueDateIO"), "D")
                    Else
                        dRow("ABR_ValueDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_ChequeDateIO")) = False Then
                        dRow("ChequeDate") = dt1.Rows(i)("ABR_ChequeDateIO")
                    Else
                        dRow("ChequeDate") = ""
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_ChequeNoIO")) = False Then
                        dRow("lblchequeno") = dt1.Rows(i)("ABR_ChequeNoIO").ToString()
                    Else
                        dRow("lblchequeno") = ""
                    End If
                    dRow("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt1.Rows(i)("ABR_BankIO") & " And gl_CompId=" & iCompID & "")
                    If IsDBNull(dt1.Rows(i)("ABR_FromDateIO")) = False Then
                        dRow("fromDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_FromDateIO"), "D")
                    Else
                        dRow("fromDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ToDateIO")) = False Then
                        dRow("Todate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ToDateIO"), "D")
                    Else
                        dRow("Todate") = ""
                    End If
                    dRow("Debit") = dt1.Rows(i)("ABR_DebitIO")
                    dRow("Credit") = dt1.Rows(i)("ABR_CreditIO")

                    If IsDBNull(dt1.Rows(i)("ABR_CCreditIO")) = False Then
                        dRow("CCredit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CCreditIO").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CCredit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CDebitIO")) = False Then
                        dRow("CDabit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CDebitIO").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CDabit") = "0.00"
                    End If

                    If IsDBNull(dt1.Rows(i)("ABR_StatusIO")) = False Then
                        If dt1.Rows(i)("ABR_StatusIO").ToString() = "W" Then
                            dRow("Status") = "Amount Not Matched"
                        ElseIf dt1.Rows(i)("ABR_StatusIO").ToString() = "C" Then
                            dRow("Status") = "amount not exist in Bank"
                        ElseIf dt1.Rows(i)("ABR_StatusIO").ToString() = "B" Then
                            dRow("Status") = "amount not exist in company"
                        ElseIf dt1.Rows(i)("ABR_StatusIO").ToString() = "A" Then
                            dRow("Status") = "Amount Matched"
                        End If
                    Else
                        dRow("ABR_Status") = ""
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllDetails1(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iYearId As Integer) As DataTable
        Dim sql As String
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dRow As DataRow
        Dim slno As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("BankName")
            dt.Columns.Add("fromDate")
            dt.Columns.Add("Todate")
            dt.Columns.Add("SerialNo")
            dt.Columns.Add("TrType")
            dt.Columns.Add("ChequeDate")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Status")
            dt.Columns.Add("BnkID")
            dt.Columns.Add("CCredit")
            dt.Columns.Add("CDabit")
            dt.Columns.Add("ABR_ValueDate")
            dt.Columns.Add("Remark")
            sql = "select * from Acc_Bank_Reconcilation where ABR_CompID=" & iCompID & " and ABR_YearID=" & iYearId & ""
            dt1 = objDB.SQLExecuteDataSet(sNameSpace, sql).Tables(0)

            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    slno = slno + 1
                    dRow("SrNo") = slno
                    If IsDBNull(dt1.Rows(i)("ABR_ID")) = False Then
                        dRow("BnkID") = dt1.Rows(i)("ABR_ID")
                    Else
                        dRow("BnkID") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Bank")) = False Then
                        dRow("BankName") = objDB.SQLExecuteScalar(sNameSpace, "Select gl_desc from  chart_of_Accounts Where gl_id=" & dt1.Rows(i)("ABR_Bank") & " And gl_CompId=" & iCompID & " ")
                    Else
                        dRow("BankName") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_FromDate")) = False Then
                        dRow("fromDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_FromDate"), "D")
                    Else
                        dRow("fromDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ToDate")) = False Then
                        dRow("Todate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ToDate"), "D")
                    Else
                        dRow("Todate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TransactionNo")) = False Then
                        dRow("SerialNo") = dt1.Rows(i)("ABR_TransactionNo")
                    Else
                        dRow("SerialNo") = ""
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_ValueDate")) = False Then
                        dRow("ABR_ValueDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ValueDate"), "D")
                    Else
                        dRow("ABR_ValueDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_TrType")) = False Then
                        If dt1.Rows(i)("ABR_TrType") = 1 Then
                            dRow("TrType") = "PAYMENT"
                        ElseIf dt1.Rows(i)("ABR_TrType") = 3 Then
                            dRow("TrType") = "RECEIPT"
                        End If
                    Else
                        dRow("TrType") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_ChequeDate")) = False Then
                        dRow("ChequeDate") = FASGeneral.FormatDtForRDBMS(dt1.Rows(i)("ABR_ChequeDate"), "D")
                    Else
                        dRow("ChequeDate") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Debit")) = False Then
                        dRow("Debit") = Convert.ToDecimal(dt1.Rows(i)("ABR_Debit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("Debit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Credit")) = False Then
                        dRow("Credit") = Convert.ToDecimal(dt1.Rows(i)("ABR_Credit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("Credit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CCradit")) = False Then
                        dRow("CCredit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CCradit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CCredit") = "0.00"
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_CDabit")) = False Then
                        dRow("CDabit") = Convert.ToDecimal(dt1.Rows(i)("ABR_CDabit").ToString()).ToString("#,##0.00")
                    Else
                        dRow("CDabit") = "0.00"
                    End If


                    If IsDBNull(dt1.Rows(i)("ABR_Comment")) = False Then
                        dRow("Remark") = dt1.Rows(i)("ABR_Comment").ToString()
                    Else
                        dRow("Remark") = ""
                    End If
                    If IsDBNull(dt1.Rows(i)("ABR_Status")) = False Then
                        If dt1.Rows(i)("ABR_Status").ToString() = "W" Then
                            dRow("Status") = "Amount Not Matched"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "C" Then
                            dRow("Status") = "amount not exist in Bank"
                        ElseIf dt1.Rows(i)("ABR_Status").ToString() = "B" Then
                            dRow("Status") = "amount not exist in company"
                        End If
                    Else
                        dRow("ABR_Status") = ""
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function check(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iId As Integer) As String
        Dim sSql As String
        Try
            sSql = "select ABR_Status from acc_bank_reconcilation where ABR_ID=" & iId & " and ABR_CompID=" & iCompID & ""
            check = objDB.SQLExecuteScalar(sNameSpace, sSql)
            Return check
        Catch ex As Exception
        End Try
    End Function
    Public Function DeleteCompanyData(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iId As Integer) As String
        Dim sSql As String
        Try
            'sSql = "update Acc_Transactions_Details set ATD_TransactionDate='" & FASGeneral.FormatDtForRDBMS(dTrnDate, "D") & "' where ATD_BillId=" & iId & " and ATD_CompID=" & iCompID & ""
            sSql = "delete from acc_bank_reconcilation where ABR_ID=" & iId & " and ABR_CompID=" & iCompID & ""
            objDB.SQLExecuteScalar(sNameSpace, sSql)
        Catch ex As Exception
        End Try
    End Function
End Class
